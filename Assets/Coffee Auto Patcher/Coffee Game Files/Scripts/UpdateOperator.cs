using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateOperator : MonoBehaviour
{


    public bool checkForUpdates = true;

    public bool useFastFileChecking = true;
    //If set to false, will immediately close client and launch Launcher to update if an update is necessary. Otherwise will check if a launcher update is necessary.
    public bool checkForLauncherUpdates;

    public bool showDebugText = false;

    //Full URL for your Server File List (Should be in root directory of your CDN Bucket example s3.patchbucket.com/fileList.txt)
    public string serverFileListURL = "";

    //Full Application Name for your Game including the .exe.  For Mac builds this is the file that is in the game.app/Contents/MacOS/ folder, it will not have any extension.
    [Tooltip("Full application name for your Game including the .exe i.e. Game.exe")]
    public string gameFullExe = "";

    //Full Application Name for your Patcher including the .exe.  For Mac builds this is the file that is in the patcher.app/Contents/MacOS/ folder, it will not have any extension.
    [Tooltip("Full relative path to the patcher.exe.  Should be example PatcherFolder/Patcher.exe if on Unity 2017+, otherwise just Patcher.exe")]
    public string patcherFullExe = "";

    public bool deleteOldPatcherInGameDirectory = false;


    public RectTransform ActivitySpinner;
    float spinnerRotationSpeed = 30f;
    string patcherNoExe = "";
    string patcherNameNoExe = "";
    string patcherFolderName = "";
    string serverVersionFile = "fileList.txt";

    string serverDirectory = "";

    string rootDirectory = "";
    bool needsToUpdate = false;
    bool updatingLauncher = false;
    bool updatingLauncherFiles = false;
    bool doneCheckingFiles = false;

    int downloadFailures = 0;

    string launcher = "";
    string launcherMD5 = "";

    Dictionary<string, string> missingLauncherFiles = new Dictionary<string, string>();


    int TotalFilesCount = 0;
    int FileCount = 0;
    string CurrentFileName = "";

    bool hasInternetConnection = true;
    bool upToDate = false;

    public enum OperatingSystem
    {
        Windows,
        Mac,
        Linux
    }

    public OperatingSystem buildOperatingSystem;

    public Text debugText;
    string debugTextValue = "";
    public ScrollRect debugScrollRect;

    // Use this for initialization
    void Start()
    {
        //Disable this if your game is online only.
        if (Process.GetCurrentProcess().ToString() != "System.Diagnostics.Process (Unity)")
            CheckInternetUnity();


        serverVersionFile = Path.Combine(Environment.CurrentDirectory, serverVersionFile).Replace(@"\", "/");

        //Ensures that it does not check for updates if you are running the game from the Editor.
        if (checkForUpdates && hasInternetConnection && serverFileListURL != "" && gameFullExe != "" && patcherFullExe != "" && Process.GetCurrentProcess().ToString() != "System.Diagnostics.Process (Unity)")
        {

            patcherFullExe = patcherFullExe.Replace(@"\", "/");

            if (patcherFullExe.Contains(".exe"))
            {
                buildOperatingSystem = OperatingSystem.Windows;
                patcherNoExe = patcherFullExe.Replace(".exe", "");
            }
            else if (patcherFullExe.Contains(".app"))
            {
                buildOperatingSystem = OperatingSystem.Mac;
                patcherNoExe = patcherFullExe.Replace(".app", "");
            }
            else if (patcherFullExe.Contains(".x86"))
            {
                buildOperatingSystem = OperatingSystem.Linux;
                patcherNoExe = patcherFullExe.Replace(".x86", "");
            }
            else
                patcherNoExe = patcherFullExe;

            if (patcherNoExe.Contains(@"/"))
            {
                var patcherSplit = patcherNoExe.Split('/');
                patcherFolderName = patcherSplit[0];
                patcherNameNoExe = patcherSplit[1];
            }
            else
            {
                patcherNameNoExe = patcherNoExe;
            }

            if (File.Exists(gameFullExe))
                rootDirectory = Environment.CurrentDirectory;
            else
                rootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();


            if (deleteOldPatcherInGameDirectory)
            {
                string[] patcherSplit = patcherFullExe.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                if (patcherSplit.Length > 1)
                {
                   
                    if (File.Exists(Path.Combine(rootDirectory, gameFullExe)) && File.Exists(Path.Combine(rootDirectory, patcherSplit[1])))
                    {
                        File.Delete(Path.Combine(rootDirectory, patcherSplit[1]));
                        AddMessage("Deleted old patcher to avoid issues.");

                    }

                }
            }
            //Starts Update Check in Background Thread.
            ThreadStart threadStart = delegate
            {
                CheckForUpdates();
            };

            new Thread(threadStart).Start();
        }
        else
        {
            SceneManager.LoadScene(1);
        }

        if (showDebugText && debugText && debugScrollRect)
            debugScrollRect.transform.localScale = Vector3.one;
        else if (debugScrollRect)
            debugScrollRect.transform.localScale = Vector3.zero;

    }


    public bool CheckInternetConnection()
    {
        try
        {
            System.Net.NetworkInformation.Ping myPing = new System.Net.NetworkInformation.Ping();
            string host = "google.com";
            byte[] buffer = new byte[32];
            int timeout = 1000;
            System.Net.NetworkInformation.PingOptions pingOptions = new System.Net.NetworkInformation.PingOptions();
            System.Net.NetworkInformation.PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
            return (reply.Status == System.Net.NetworkInformation.IPStatus.Success);
        }
        catch
        {
            return false;
        }
    }

    public IEnumerator CheckInternetUnity()
    {
        WWW www = new WWW("http://www.google.com");
        yield return www;

        if (string.IsNullOrEmpty(www.error)) hasInternetConnection = true;
        else hasInternetConnection = false;
    }

    void Update()
    {
        if (ActivitySpinner)
        {
            if (ActivitySpinner.gameObject.activeSelf)
            {
                Vector3 newRotation = new Vector3(0, 0, spinnerRotationSpeed) * Time.deltaTime;

                ActivitySpinner.transform.rotation *= Quaternion.Euler(newRotation);
            }

            if (showDebugText)
                debugText.text = debugTextValue;
        }

        if(upToDate)
        {
            upToDate = false;
            SceneManager.LoadScene(1);
        }
    }

    void AddMessage(string message)
    {
        if (debugText)
            debugTextValue += message + "\r\n";
        UnityEngine.Debug.Log(message);
    }

    void CheckForUpdates()
    {

        serverDirectory = serverFileListURL.Replace("fileList.txt", "");

        upToDate = false;

        upToDate = CheckFiles();



        if (!upToDate)
        {    
            AddMessage("Failed to check for updates and patch successfully.");
        }

    }

    public bool CheckFiles()
    {
        bool _status = false;

        //Accepts all SSL Certificates
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

        using (WebClient wc = new WebClient())
        {

            AddMessage("Downloading server file md5 list " + serverFileListURL + " to " + serverVersionFile);

            try
            {
                wc.DownloadFile(serverFileListURL, serverVersionFile);
            }
            catch (Exception e)
            {
                AddMessage("failed to download server file list due to " + e);
            }
        }

        string[] _files = WriteSafeReadAllLines(serverVersionFile);
        TotalFilesCount = (_files.Length - 1);
        //string _serverVersion = _files[0];
        //string _currentVersion = "";
        string fullGameExeName = gameFullExe;

        if (buildOperatingSystem == OperatingSystem.Mac)
        {
            fullGameExeName = fullGameExeName.Replace(".app", "");
            patcherFullExe = patcherFullExe.Replace(".app", "");

            if (File.Exists(fullGameExeName + @".app/Contents/MacOS/" + fullGameExeName))
            {
                fullGameExeName = fullGameExeName + @".app/Contents/MacOS/" + fullGameExeName;
            }

            if(File.Exists(patcherFullExe + @".app/Contents/MacOS/" + patcherFullExe))
            patcherFullExe = patcherFullExe + @".app/Contents/MacOS/" + patcherFullExe;
        }

        string fullGamePath = Path.Combine(rootDirectory, fullGameExeName).Replace(@"\", "/");
        AddMessage("Full game path is " + fullGamePath + " patcherNoExe is " + patcherNoExe);

        AddMessage("Looking through " + _files.Length + " files for updates.");



        for (int i = 1; i < _files.Length; i++)
        {



            string[] _md5split = _files[i].Split('\t');

            _md5split[0] = _md5split[0].Replace(@"\", "/");

            FileCount = i;
            if (_md5split[0].Length > 4 && !_md5split[0].Contains(fullGameExeName))
            {
               
                if (!File.Exists(Path.Combine(rootDirectory, _md5split[0])) && _md5split[0].Contains(patcherFullExe))
                {
                    AddMessage("Downloading Launcher");
                    updatingLauncher = true;
                    launcher = _md5split[0];
                    launcherMD5 = _md5split[1];

                }
                if (!File.Exists(Path.Combine(rootDirectory, _md5split[0])) && !_md5split[0].Contains(patcherNoExe) && !_md5split[0].Contains(patcherNameNoExe))
                {
                    AddMessage("Updating due to not finding " + _md5split[0]);
                    needsToUpdate = true;
                    if (!checkForLauncherUpdates)
                        GetFile();
                }
                if (File.Exists(Path.Combine(rootDirectory, _md5split[0])))
                {

                    if (useFastFileChecking && _md5split.Length > 2)
                    {
                        DateTime serverFileTime = DateTime.Parse(_md5split[2]);

                        if (serverFileTime > File.GetLastWriteTimeUtc(Path.Combine(rootDirectory, _md5split[0])))
                        {
                            if (_md5split[0].Contains(patcherNoExe))
                            {
                                updatingLauncherFiles = true;
                                missingLauncherFiles.Add(_md5split[0], _md5split[1]);
                            }
                            else
                                needsToUpdate = true;
                        }
                    }
                    else
                    {
                        using (var md5 = MD5.Create())
                        {
                            using (var stream = new FileStream(Path.Combine(rootDirectory, _md5split[0]), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                string _mymd5 = (BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower());

                                if (_mymd5 != _md5split[1])
                                {
                                    if (_md5split[0].Contains(patcherFullExe))
                                    {
                                        AddMessage("Updating Launcher Executable");
                                        updatingLauncher = true;
                                        launcher = _md5split[0];
                                        launcherMD5 = _md5split[1];
                                        AddMessage("Updating Launcher is " + updatingLauncher);
                                    }
                                    else if (_md5split[0].Contains(patcherNoExe) || _md5split[0].Contains(patcherNameNoExe) || (_md5split[0].Contains(patcherFolderName) && patcherFolderName.Length > 2))
                                    {
                                        AddMessage("Updating Launcher Files due to mismatch md5");
                                        updatingLauncherFiles = true;
                                        AddMessage("Updating " + _md5split[0] + " " + _md5split[1]);
                                        missingLauncherFiles.Add(_md5split[0], _md5split[1]);
                                        AddMessage("Missing Launcher File count is " + missingLauncherFiles.Count + " updatingLauncherFiles is " + updatingLauncherFiles);
                                    }
                                    else
                                    {
                                        AddMessage("Updating due to updated " + _md5split[0]);
                                        needsToUpdate = true;
                                        if (!checkForLauncherUpdates && !updatingLauncherFiles)
                                            GetFile();
                                    }
                                }

                            }
                        }
                    }
                }
                else if (!File.Exists(Path.Combine(rootDirectory, _md5split[0])) && (_md5split[0].Contains(patcherNoExe) || _md5split[0].Contains(patcherNameNoExe) || (_md5split[0].Contains(patcherFolderName) && patcherFolderName.Length > 2)))
                {
                    AddMessage("Updating Launcher Files due to them being missing.");
                    updatingLauncherFiles = true;
                    AddMessage("Updating " + _md5split[0] + " " + _md5split[1]);
                    missingLauncherFiles.Add(_md5split[0], _md5split[1]);
                    AddMessage("Missing Launcher File count is " + missingLauncherFiles.Count + " updatingLauncherFiles is " + updatingLauncherFiles);
                }

            }
        }
        if (updatingLauncher)
        {
            AddMessage("Downloading Launcher executable");
            DownloadFile(launcher, launcherMD5);

        }

        if (updatingLauncherFiles)
        {
            AddMessage("Attempting to update launcher files now.");
            foreach (KeyValuePair<string, string> kvp in missingLauncherFiles)
            {
                AddMessage("Attempting to update " + kvp.Key + " " + kvp.Value);
                DownloadFile(kvp.Key, kvp.Value);
            }

            updatingLauncherFiles = false;
            updatingLauncher = false;
        }

        doneCheckingFiles = true;
        if (needsToUpdate && !updatingLauncher && !updatingLauncherFiles)
            GetFile();

        _status = true;

        return _status;
    }

    //If you change the name of the Launcher, you must adjust the new exe name here as well.
    public void GetFile()
    {
        AddMessage("Updating Game.");



        string fullPatcherPath = Path.Combine(rootDirectory, patcherFullExe).Replace(@"\", "/");

        AddMessage("Full Patcher path is " + fullPatcherPath);

        if (buildOperatingSystem == OperatingSystem.Mac)
        {
            Process.Start(new ProcessStartInfo(fullPatcherPath, "--no-first-run")
            { UseShellExecute = false });
        }
        else
            ExecuteAsAdmin(fullPatcherPath, serverFileListURL + " " + gameFullExe);


        Environment.Exit(0);

    }

    public void DownloadFile(string _fileName, string _serverMD5)
    {
        AddMessage("Starting Download for " + _fileName);

        try
        {
            if (Process.GetProcesses().Any(p => p.ProcessName == patcherNoExe))
            {
                try
                {
                    Process.GetProcessesByName(patcherNoExe).FirstOrDefault().Kill();
                }
                catch
                {

                }

            }

            Process.GetProcessesByName(patcherNoExe).FirstOrDefault().Kill();
        }
        catch
        {

        }

        using (WebClient wc = new WebClient())
        {
            string _file = (serverDirectory + _fileName).Replace(@"\", "/");

            CurrentFileName = "Downloading file: " + _fileName;

            string fullFilePath = Path.Combine(rootDirectory, _fileName);
            fullFilePath = fullFilePath.Replace(@"\", "/");
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(fullFilePath).Replace(@"\", "/")))
                    Directory.CreateDirectory(Path.GetDirectoryName(fullFilePath).Replace(@"\", "/"));
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log("filename is " + _fileName + " " + ex);
            }
            try
            {
                AddMessage("Downloading from " + _file + " to " + fullFilePath);

                DownloadFileWC(new Uri(_file, UriKind.RelativeOrAbsolute), fullFilePath);

            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log("failed to download file due to " + e);
                AddMessage("Failed to download file due to " + e);
            }

        }

        using (var md5 = MD5.Create())
        {
            using (var stream = new FileStream(Path.Combine(rootDirectory, _fileName), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                string _mymd5 = (BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower());
                AddMessage("[UPDATE] Newly downloaded mymd5: " + _mymd5 + " serverMD5: " + _serverMD5);
                if (downloadFailures < 10)
                {
                    if (_mymd5 != _serverMD5)
                    {
                        downloadFailures++;
                        DownloadFile(Path.Combine(rootDirectory, _fileName), _serverMD5);
                    }
                    else if (needsToUpdate && !updatingLauncherFiles)
                        GetFile();
                }
                else if (needsToUpdate && !updatingLauncherFiles)
                    GetFile();
            }
        }


    }


    public void DownloadFileWC(Uri uri, string destination)
    {
        using (var wc = new WebClient())
        {
            wc.DownloadProgressChanged += HandleDownloadProgress;
            wc.DownloadFileCompleted += HandleDownloadComplete;

            try
            {
                var syncObj = new System.Object();
                lock (syncObj)
                {
                    AddMessage("Downloading from " + uri.ToString() + " to " + destination);
                    wc.DownloadFileAsync(uri, destination, syncObj);
                    //This would block the thread until download completes
                    Monitor.Wait(syncObj);
                }
            }
            catch (Exception ex)
            {
                AddMessage("Failed to download file due to " + ex);
            }
        }

    }

    public void HandleDownloadComplete(object sender, AsyncCompletedEventArgs e)
    {
        lock (e.UserState)
        {
            AddMessage("Download complete, moving to next file");
            //releases blocked thread
            Monitor.Pulse(e.UserState);
        }
    }


    public void HandleDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
    {

    }






    public string[] WriteSafeReadAllLines(String path)
    {
        using (var csv = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (var sr = new StreamReader(csv))
        {
            List<string> file = new List<string>();
            while (!sr.EndOfStream)
            {
                file.Add(sr.ReadLine());
            }

            return file.ToArray();
        }
    }

    public void ExecuteAsAdmin(string fileName, string args)
    {
        Process proc = new Process();
        proc.StartInfo.FileName = fileName;
        proc.StartInfo.UseShellExecute = true;
        proc.StartInfo.Verb = "runas";
        proc.StartInfo.Arguments = args;
        proc.Start();
    }
}
