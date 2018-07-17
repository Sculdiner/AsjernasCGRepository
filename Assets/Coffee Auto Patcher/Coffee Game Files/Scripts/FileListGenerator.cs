using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

[ExecuteInEditMode]
public class FileListGenerator : MonoBehaviour {


    
    public string fullGameExeName = "";
    public string gameBuildPath = "";
    public string serverFileListURL = "";
    string localFileListPath = "";
    public bool openFileListOnComplete;

    public enum OperatingSystem
    {
        Windows,
        Mac,
        Linux
    }

    public OperatingSystem buildOperatingSystem;

    public void AttemptFileListGeneration()
    {
        if(gameBuildPath == "" || fullGameExeName == "")
        {
            UnityEngine.Debug.Log("Verify Game Build Path and Full Exe Name (ex. Game.exe)");
            return;
        }

        ThreadStart threadStart = delegate
        {
            GenerateFileList();
        };

        //if(serverFileListURL == "")
        new Thread(threadStart).Start();
       // else
        //GenerateFileList();

    }

    void GenerateFileList()
    {
        bool downloadedServerFileList = false;

        Dictionary<string, string> serverFiles = new Dictionary<string, string>();

        if (serverFileListURL != "")
        {

            //Accepts all SSL Certificates
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            using (WebClient wc = new WebClient())
            {

                
                try
                {

                    string _serverPath = System.IO.Path.Combine(gameBuildPath, "serverfileList.txt");

                    wc.DownloadFile(serverFileListURL, _serverPath);
                    downloadedServerFileList = true;

                    string[] _files = WriteSafeReadAllLines(_serverPath);

                    for(int i = 1; i < _files.Length; i++)
                    {
                        _files[i] = _files[i].Replace(@"\", "/");
                        string[] _md5split = _files[i].Split('\t');
                        serverFiles.Add(_md5split[0], _md5split[1]);
                    }

                    File.Delete(_serverPath);
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.Log("Failed to download server fileList due to " + e.ToString());
                    downloadedServerFileList = false;
                }
            }

            


        }

        localFileListPath = Path.Combine(gameBuildPath, "fileList.txt");
        string updatedFilesPath = System.IO.Path.Combine(gameBuildPath, "updatedfileList.txt");

        string[] _AllFiles = Directory.GetFiles(gameBuildPath, "*", SearchOption.AllDirectories);

        TextWriter tw = new StreamWriter(localFileListPath, false);
        TextWriter twUpdatedFiles = new StreamWriter(updatedFilesPath, false);

        string _exePath = System.IO.Path.Combine(gameBuildPath, fullGameExeName);

        if(buildOperatingSystem == OperatingSystem.Mac)
            _exePath = System.IO.Path.Combine(gameBuildPath, fullGameExeName + @".app/Contents/MacOS/" + fullGameExeName);

        using (var md5 = MD5.Create())
        {
            UnityEngine.Debug.Log("Exepath is " + _exePath);
            using (var stream = File.OpenRead(_exePath))
            {
                string _md5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                tw.WriteLine(_md5);
            }
        }

        twUpdatedFiles.WriteLine("Files necessary to upload to the server.");
        twUpdatedFiles.WriteLine("fileList.txt");
       // long startGenerationTime = DateTime.UtcNow.Ticks;
        //UnityEngine.Debug.Log("Start Generation Time is " + startGenerationTime);
        foreach (string s in _AllFiles)
        {
            string t = s.Replace(gameBuildPath + @"\", null);

            t = t.Replace(@"\", "/");
            //Add Exceptions if you have items in build output folder that you do not want in final. Uncomment if statement and add your exceptions.
            //Example !t.StartsWith(@"Logs\") && !t.EndsWith("Thumbs.db")
            if (!t.Contains("fileList.txt") && !t.Contains("output_log.txt"))
            {

                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(s))
                    {
                        string _md5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                        string creationDateString = File.GetLastWriteTimeUtc(s).ToUniversalTime().ToString("MM/dd/yyyy HH:mm:ss");
                        tw.WriteLine(t + "\t" + _md5 + "\t" + creationDateString);



                        if (serverFiles.ContainsKey(t) && downloadedServerFileList)
                        {
                            if (_md5 != serverFiles[t])
                            {
                                UnityEngine.Debug.Log(t + " must be uploaded to server.");
                                twUpdatedFiles.WriteLine(t);
                            }
                        }
                        else if (downloadedServerFileList)
                        {
                            twUpdatedFiles.WriteLine(t);
                            UnityEngine.Debug.Log(t + " must be uploaded to server.");
                        }

                 

                    }
                }
            }
        }
        //UnityEngine.Debug.Log("End Generation time is " + DateTime.UtcNow.Ticks);
        //TimeSpan elapsed = new TimeSpan(DateTime.UtcNow.Ticks - startGenerationTime);
        //UnityEngine.Debug.Log("Total Generation time for DateTime Check is " +  elapsed.TotalSeconds + " seconds.");

        tw.Close();
        twUpdatedFiles.Close();

        UnityEngine.Debug.Log("File List Created in Build Output Folder");

        if (openFileListOnComplete)
            Process.Start(localFileListPath);

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

}
