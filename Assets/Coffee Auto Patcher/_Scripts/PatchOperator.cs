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
using UnityEngine.UI;
using System.Runtime.InteropServices;

/// <summary>
/// All Patching Framework for the Coffee Auto Patcher to check for file updates via md5 hash and download updated files from Amazon S3, an FTP or CDN
/// </summary>

public class PatchOperator : MonoBehaviour
{
    bool showDebugText = false;

    int _fileCount = 2;
    int _totalFilesCount = 0;

    public bool useFastFileChecking = true;

    public string serverFileListURL = "";

    [Tooltip("Full relative path to the game.exe.  Should be example ../Game.exe if on Unity 2017+, otherwise just Game.exe")]
    public string fullGameExeName = "";
    public string fullPatcherExeName = "";
    public bool patcherNotInGameDirectory = true;
    string serverDir = "";
    string serverVersionFile = "fileList.txt";
    string changelogFile = "changelog.txt";
    bool checkedAllFiles = false;
    bool updateComplete = false;
    int downloadFailures = 0;
    int totalDownloads = 0;

    public Text errorText;
    public Text fileNameText;
    public Text updateStatusText;
    public Slider FileProgressBarControl;
    public Slider TotalProgressBarControl;
    public Text totalFilesCountText;
    public UnityEngine.UI.Button PlayButton;
    public Image ActivitySpinner;
    public Toggle autoPlayToggle;
    public Text changelogText;
    int autoPlayToggled;
    public float spinnerRotationSpeed = 1f;


    //UI Values
    public float progressBarMax = 1;
    public float progressBarValue = 0;
    public float fileProgressBarValue = 0;
    string currentUpdateStatusText = "";
    string currentfileNameText = "";
    bool changelogTextExists = false;

    bool playTimerStarted = false;
    float playTimerStartTime = 0;
    bool gameStarted = false;

    string launcherExe = "";
    string launcherNoExe = "";
    string rootDirectory = "";

    public bool useTransparentWindow = false;
    public TransparentWindow transparentWindow;
    public GameObject TransparentTitleBar;

    public Vector2 nonTransparentWindowSize = new Vector2(700, 400);
    public Vector2 transparentWindowSize = new Vector2(800, 600);

    public enum OperatingSystem
    {
        Windows,
        Mac,
        Linux,
        Linux64bit
    }

    public OperatingSystem buildOperatingSystem;

    public Dictionary<OperatingSystem, string> operatingSystemFileExtensions = new Dictionary<OperatingSystem, string>();

    int FileCount
    {
        get
        {
            return _fileCount;
        }
        set
        {
            _fileCount = value;

        }
    }


    int TotalFilesCount
    {
        get
        {
            return _totalFilesCount;
        }

        set
        {
            _totalFilesCount = value;
        }
    }

    string CurrentfileNameText
    {
        get
        {
            return currentfileNameText;
        }
        set
        {
            currentfileNameText = value;

        }
    }

    string UpdateStatusText
    {
        get
        {
            return currentUpdateStatusText;
        }
        set
        {
            currentUpdateStatusText = value;
        }
    }



    void Awake()
    {

        try
        {

            #if UNITY_EDITOR

                        useTransparentWindow = false;
            #endif

            if(useTransparentWindow && Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                                //If you are using a TransparentWindow and having issues with windows getting stuck with a white background, comment out these 2 lines of code.
                                if (Screen.width < transparentWindowSize.x - 5 && Screen.height < transparentWindowSize.y - 5)
                                    Screen.SetResolution(Mathf.RoundToInt(transparentWindowSize.x), Mathf.RoundToInt(transparentWindowSize.y), false);
        
            }
            else
            {
                if (transparentWindow)
                    transparentWindow.enabled = false;
                if (TransparentTitleBar)
                    TransparentTitleBar.SetActive(false);
                Screen.SetResolution(Mathf.RoundToInt(nonTransparentWindowSize.x), Mathf.RoundToInt(nonTransparentWindowSize.y), false);
            }

            //Doesn't have a correct path to patch.
            if (fullGameExeName == "" || serverFileListURL == "")
            {
                errorText.text = "Error.  Please launch game directly to update and correct. If issue persists, reinstall the game.";
                return;
            }

            serverDir = serverFileListURL.Replace("fileList.txt", "");


            //Builds dictionaries for the extension for each Operating System type.
            operatingSystemFileExtensions.Add(OperatingSystem.Windows, ".exe");
            operatingSystemFileExtensions.Add(OperatingSystem.Mac, "");
            operatingSystemFileExtensions.Add(OperatingSystem.Linux, ".x86");
            operatingSystemFileExtensions.Add(OperatingSystem.Linux64bit, ".x86_64");


            if (fullGameExeName.Contains(".exe"))
            {
                buildOperatingSystem = OperatingSystem.Windows;

            }
            else if (fullGameExeName.Contains(".x86_64"))
            {
                buildOperatingSystem = OperatingSystem.Linux64bit;

            }
            else if (fullGameExeName.Contains(".x86"))
            {
                buildOperatingSystem = OperatingSystem.Linux;

            }
            else
            {
                buildOperatingSystem = OperatingSystem.Mac;
            }


            if (PlayerPrefs.HasKey("AutoPlay"))
            {
                autoPlayToggled = PlayerPrefs.GetInt("AutoPlay");
                if (autoPlayToggled > 0)
                    autoPlayToggle.isOn = true;
                else
                    autoPlayToggle.isOn = false;
            }
            else
            {
                PlayerPrefs.SetInt("AutoPlay", 1);
                PlayerPrefs.Save();
            }
        }
        catch (Exception ex)
        {
            AddMessage("failed to run awake changes due to " + ex);
        }
    }



    void Start()
    {


        if (buildOperatingSystem == OperatingSystem.Windows)
            launcherExe = Process.GetCurrentProcess().ProcessName + ".exe";
        else if (buildOperatingSystem == OperatingSystem.Mac)
            launcherExe = Process.GetCurrentProcess().ProcessName;
        else if (buildOperatingSystem == OperatingSystem.Linux)
            launcherExe = Process.GetCurrentProcess().ProcessName + ".x86";
        else if (buildOperatingSystem == OperatingSystem.Linux64bit)
            launcherExe = Process.GetCurrentProcess().ProcessName + ".x86_64";

        if (buildOperatingSystem == OperatingSystem.Windows)
            launcherNoExe = Process.GetCurrentProcess().ProcessName;

        changelogTextExists = changelogText;

        try
        {
            //wait for Game to shutdown.
            string gameName = fullGameExeName;

            if (buildOperatingSystem != OperatingSystem.Mac)
                gameName = fullGameExeName.Replace(operatingSystemFileExtensions[buildOperatingSystem], "");

            if (Process.GetProcesses().Any(p => p.ProcessName == gameName))
            {
                Process.GetProcessesByName(gameName).First().Kill();


                AddMessage("Killed Game process");
            }
        }
        catch
        {

        }

        AddMessage("Continuing with patching...");

        //Parameters are not correct to do a full patch.
        if (fullGameExeName == "" || serverFileListURL == "" || serverDir == "")
        {
            AddMessage("Cancelling start due to " + fullGameExeName + " ... " + serverFileListURL + " ... " + serverDir);
            return;
        }

        if (fullPatcherExeName != "" && buildOperatingSystem != OperatingSystem.Mac)
        {
            launcherExe = fullPatcherExeName;
            launcherNoExe = fullPatcherExeName.Replace(operatingSystemFileExtensions[buildOperatingSystem], "");
        }
        else
        {
            launcherExe = fullPatcherExeName;
            launcherNoExe = launcherExe.Replace(".app", "");
        }

        AddMessage("About to check root directory.");

        rootDirectory = Directory.GetCurrentDirectory();

        if (patcherNotInGameDirectory && !File.Exists(fullGameExeName))
        {
            rootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
        }


        AddMessage("Root Directory is " + rootDirectory + " starting update check now.");

        if(changelogTextExists)
        StartCoroutine("GetChangeLog");

        StartUpdateCheck();

    }


    IEnumerator GetChangeLog()
    {
        WWW updated_changelog = new WWW(Path.Combine(serverDir, "changelog.txt"));

        yield return updated_changelog;

        changelogText.text = updated_changelog.text;

    }

    //Uncomment this if you want messages going to changelog window instead of displaying a changelog.
    void AddMessage(string message)
    {

        if (showDebugText)
            changelogText.text += message + "\r\n";

        UnityEngine.Debug.Log(message);

    }

    void Update()
    {


        if (playTimerStarted)
        {
            ActivitySpinner.gameObject.SetActive(false);
            CurrentfileNameText = "Update Complete.";
            TotalProgressBarControl.value = TotalProgressBarControl.maxValue;
            FileProgressBarControl.value = FileProgressBarControl.maxValue;

            if (totalFilesCountText)
                totalFilesCountText.text = "";

            //Updates just completed, this starts the playTimer. 
            if (playTimerStartTime == 0)
                playTimerStartTime = Time.time;

            PlayButton.transform.localScale = Vector3.one;

            if (Time.time > playTimerStartTime + 10 && !gameStarted)
            {
                gameStarted = true;

                //Starts Game
                StartGame();
            }
            else
            {
                updateStatusText.text = "Playing in " + (Mathf.Floor(((playTimerStartTime + 3) - Time.time)) >= 0 ? Mathf.Floor(((playTimerStartTime + 3) - Time.time)) : 0);
            }
        }
        else
        {

            TotalProgressBarControl.maxValue = progressBarMax;
            TotalProgressBarControl.value = progressBarValue;
            FileProgressBarControl.value = fileProgressBarValue;
            updateStatusText.text = UpdateStatusText;
            fileNameText.text = CurrentfileNameText;

            if (totalFilesCountText)
                totalFilesCountText.text = (progressBarValue <= progressBarMax ? progressBarValue : progressBarMax) + "/" + progressBarMax;

            if (ActivitySpinner.gameObject.activeSelf)
            {
                Vector3 newRotation = new Vector3(0, 0, spinnerRotationSpeed) * Time.deltaTime;

                ActivitySpinner.transform.rotation *= Quaternion.Euler(newRotation);
            }
        }


        if (updateComplete && PlayButton.transform.localScale == Vector3.zero)
        {
            ActivitySpinner.gameObject.SetActive(false);
            PlayButton.transform.localScale = Vector3.one;
        }

    }


    public void StartGame()
    {

        if (autoPlayToggle.isOn)
        {
            autoPlayToggled = 1;
            PlayerPrefs.SetInt("AutoPlay", autoPlayToggled);

        }
        else
        {
            autoPlayToggled = 0;
            PlayerPrefs.SetInt("AutoPlay", autoPlayToggled);
        }
        PlayerPrefs.Save();

        if (buildOperatingSystem == OperatingSystem.Mac)
        {
            fullGameExeName = fullGameExeName.Replace(".app", "");


            fullGameExeName = fullGameExeName + @".app/Contents/MacOS/" + fullGameExeName;

        }

        string fullGamePath = Path.Combine(rootDirectory, fullGameExeName).Replace(@"\", "/");
        AddMessage("Full game Path is " + fullGamePath);

        if (buildOperatingSystem == OperatingSystem.Mac)
        {
            Process.Start(new ProcessStartInfo(fullGamePath, "--no-first-run")
            { UseShellExecute = false });
        }
        else
            ExecuteAsAdmin(fullGamePath);

        if (Process.GetCurrentProcess().ToString() != "System.Diagnostics.Process (Unity)")
            Process.GetCurrentProcess().Kill();
    }

    //Called when Toggling Auto Play.
    public void CancelAutoStartGame()
    {

        if (playTimerStarted && !autoPlayToggle.isOn)
            playTimerStarted = false;
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

    private void CheckAllFiles()
    {

        //Accepts all SSL Certificates
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };


        AddMessage("Check all files has begun.");

        if (File.Exists(serverVersionFile))
        {
            DateTime _creation = File.GetCreationTime(serverVersionFile);
            if (_creation.Minute != DateTime.Now.Minute)
            {
                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        AddMessage("Downloading server file md5 list as it is old");
                        wc.DownloadFile(serverFileListURL, serverVersionFile);
                    }
                    catch (Exception e)
                    {

                        AddMessage("Error: " + e);
                    }
                }
            }
        }
        if (!File.Exists(serverVersionFile))
        {
            using (WebClient wc = new WebClient())
            {

                AddMessage("Downloading server file md5 list");
                try
                {
                    wc.DownloadFile(serverFileListURL, serverVersionFile);
                }
                catch (Exception e)
                {

                    AddMessage("Error: " + e);
                }
            }
        }
        string[] _files = WriteSafeReadAllLines(serverVersionFile);
        TotalFilesCount = (_files.Length - 1);
        List<string> _missingFiles = new List<string>();

        AddMessage("files Length is " + (_files.Length - 1));

        progressBarMax = _files.Length;

        for (int i = 1; i < _files.Length; i++)
        {

            progressBarValue = i;

            string[] _md5split = _files[i].Split('\t');


            fileNameTextUpdate("Checking " + _md5split[0]);
            if (i == _files.Length)
                fileNameTextUpdate("Downloading updates, please wait...");


            if (!File.Exists(Path.Combine(rootDirectory, _md5split[0])) && !_md5split[0].Contains(launcherNoExe) && !_md5split[0].Contains(launcherExe))
            {

                AddMessage("Missing file " + _md5split[0]);

                _missingFiles.Add(_files[i]);
            }
            if (File.Exists(Path.Combine(rootDirectory, _md5split[0])) && !_md5split[0].Contains(launcherNoExe) && !_md5split[0].Contains(launcherExe))
            {
                if (useFastFileChecking && _md5split.Length > 2)
                {
                    DateTime serverFileTime = DateTime.Parse(_md5split[2]);

                    if(serverFileTime > File.GetLastWriteTimeUtc(Path.Combine(rootDirectory, _md5split[0])))
                    {
                        _missingFiles.Add(_files[i]);
                    }
                }
                else
                {
                    using (var md5 = MD5.Create())
                    {
                        try
                        {
                            using (var stream = new FileStream(Path.Combine(rootDirectory, _md5split[0]), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                string _mymd5 = (BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower());
                                AddMessage("my md5 " + _mymd5 + " server md5 " + _md5split[1]);
                                if (_mymd5 != _md5split[1])
                                {

                                    AddMessage("md5 mismatch on " + _md5split[0] + " " + _mymd5 + " " + _md5split[1]);
                                    _missingFiles.Add(_files[i]);
                                }
                                else
                                {
                                    FileCount++;
                                }
                            }
                        }
                        catch
                        {
                            _missingFiles.Add(_files[i]);
                        }
                    }
                }
            }

        }

        fileNameTextUpdate("Updating files, please wait...");
        GetFiles(_missingFiles);


        checkedAllFiles = true;


        if (checkedAllFiles == true)
        {

            UpdateStatusText = "Play Now.";
            CurrentfileNameText = "Update Complete.";

            if (autoPlayToggle.isOn)
                StartPlayButtonTimer();
            else
            {


                CurrentfileNameText = "Update Complete.";
                progressBarMax = TotalProgressBarControl.maxValue;
                fileProgressBarValue = FileProgressBarControl.maxValue;
                updateComplete = true;


            }
        }
    }




    public void GetFiles(List<string> _missing)
    {
        totalDownloads = _missing.Count;
        CurrentfileNameText = "Downloading " + _missing.Count + " file updates.";
        AddMessage("Downloading " + _missing.Count + " file updates.");
        progressBarMax = totalDownloads;

        foreach (string _fileName in _missing)
        {
            string[] _fileSplit = _fileName.Split('\t');

            CurrentfileNameText = "Updating " + _fileSplit[0];

            DownloadFile(_fileSplit[0], _fileSplit[1]);
        }
    }

    public void DownloadFile(string _fileName, string _md5)
    {
        if (_fileName == "" || _md5 == "")
        {
            AddMessage("invalid file to download " + _fileName + " md5 " + _md5);
            return;
        }

        using (WebClient wc = new WebClient())
        {
            string _file = (serverDir + _fileName).Replace(@"\", "/");

            CurrentfileNameText = "Downloading file: " + _fileName;

            string fullFilePath = Path.Combine(rootDirectory, _fileName);
            fullFilePath = fullFilePath.Replace(@"\", "/");
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(fullFilePath)) && Path.GetDirectoryName(fullFilePath) != Path.GetDirectoryName(fullGameExeName))
                    Directory.CreateDirectory(Path.GetDirectoryName(fullFilePath).Replace(@"\", "/"));
            }
            catch (Exception ex)
            {

                AddMessage("filename is " + _fileName + " " + ex);
            }
            try
            {

                AddMessage("Downloading from " + _file + " to " + fullFilePath);

                DownloadFileWC(new Uri(_file, UriKind.RelativeOrAbsolute), fullFilePath);

            }
            catch (Exception e)
            {

                AddMessage("failed to download file due to " + e);

            }

        }
        using (var md5 = MD5.Create())
        {
            string _mymd5 = "";

            string fullFilePath = Path.Combine(rootDirectory, _fileName);
            fullFilePath = fullFilePath.Replace(@"\", "/");

            using (var stream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                _mymd5 = (BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower());
            }
            if (downloadFailures < 5)
            {
                if (_mymd5 != _md5)
                {
                    downloadFailures++;


                    AddMessage("md5 mismatch on " + _fileName + " " + _mymd5 + " " + _md5);

                    downloadFailures += 1;
                    DownloadFile(fullFilePath, _md5);
                    return;
                }

            }

            FileCount += 1;
        }
        progressBarValue = FileCount;


    }

    public void DownloadFileSync(Uri uri, string destination, string tempDestination)
    {
        using (var wc = new WebClient())
        {


            AddMessage("Downloading from " + uri.ToString() + " to " + destination + " syncronized");
            wc.DownloadFile(uri, destination);
            //This would block the thread until download completes


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
        fileProgressBarValue = e.ProgressPercentage;
        UpdateStatusText = ConvertBytesToSize(e.BytesReceived) + " / " + ConvertBytesToSize(e.TotalBytesToReceive);
    }


    private void UpdateText(string _update)
    {
        UpdateStatusText = _update;
    }

    private void fileNameTextUpdate(string _fileName)
    {
        CurrentfileNameText = _fileName;
    }


    private void StartPlayButtonTimer()
    {
        playTimerStarted = true;
    }



    private void StartUpdateCheck()
    {
        updateStatusText.text = "Checking for updates...";
        ActivitySpinner.gameObject.SetActive(true);


        AddMessage("Checking for updates now...");

        ThreadStart threadStart = delegate
        {
            CheckAllFiles();
        };

        new Thread(threadStart).Start();
        //CheckAllFiles();
    }

    public string ConvertBytesToSize(long byteSize)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = byteSize;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }

        // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
        // show a single decimal place, and no space.
        string result = String.Format("{0:0.##} {1}", Math.Round(len * 100) / 100, sizes[order]);

        return result;
    }


    public void ExecuteAsAdmin(string fileName)
    {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();

    }

    //Work in progress, will be out in future patches.
    /* private void PseudoTitlebar_MouseDown(object sender, MouseButtonEventArgs e)
     {
         //  Console.WriteLine("psuedotitlebar mouse down");
         if (e.ChangedButton == MouseButton.Left)
         {
             //  Console.WriteLine("should be dragging now");

             this.DragMove();
         }
     } */

}
