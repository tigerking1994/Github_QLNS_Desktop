using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace VTS.QLNS.CTC.Update
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker _worker = new BackgroundWorker();
        private int _copied = 0;
        private int _totalFiles = 0;
        private int _totalTemplate = 0;
        private HashSet<string> _templateUpdates;
        private int _currentFileLength = 0;
        private int _copiedFileLength = 0;
        private string _appVersion;
        private string _sourceFilePath;
        private string _destinationPath;
        private string _mainExecuteFilePath;
        private const int UPDATE_TOTAL_CURRENT_FILE_LENGTH = 0;
        private const int UPDATE_PROGRESS = 1;
        private const int UPDATE_COPIED_FILE_LENGTH = 2;
        private const string EXTRACT_FOLDER = "target";
        private const string TEMPLATE_FOLDER = "AppData/Template";
        string[] _ignoreFolders = { "_etc" };
        string[] _ignoreFiles = { "dbconfig.json" };
        //string[] _ignoreExtensionFile = { ".mdf", ".ldf" };
        string[] _ignoreExtensionFile = { ".ldf" };
        string[] _excelExtensionFile = { ".xlsx", ".xls" };

        public MainWindow(string[] args)
        {
            InitializeComponent();
            _mainExecuteFilePath = args[1];
            _appVersion = (args.Length == 3) ? args[2] : "1.11.0.0";

            string currentLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _sourceFilePath = Path.Combine(currentLocation, EXTRACT_FOLDER);
            _destinationPath = Path.GetDirectoryName(_mainExecuteFilePath);
            _totalFiles = Directory.GetFiles(_sourceFilePath, "*.*", SearchOption.AllDirectories).Length;
            _totalTemplate = Directory.GetFiles(Path.Combine(_sourceFilePath, TEMPLATE_FOLDER), "*.*", SearchOption.AllDirectories).Length;

            var text = File.ReadAllText(@"./TemplateNote.json");
            var updateModel = JsonConvert.DeserializeObject<List<UpdateModel>>(text).ToList();
            var updateVersion = updateModel.Where(x => _appVersion.CompareTo(x.Version) < 0).ToList();
            _templateUpdates = new HashSet<string>();
            foreach (var item in updateVersion)
            {
                foreach (var prop in item.Module.GetType().GetProperties())
                {
                    var templates = prop.GetValue(item.Module, null);
                    if (templates is List<string> template)
                    {
                        foreach (var perTemp in template)
                            _templateUpdates.Add(perTemp);
                    }
                }
            }

            ToTalFiles.Text = (_totalFiles - _totalTemplate + _templateUpdates.Count).ToString();
            pgbar2.Maximum = _totalFiles - _totalTemplate + _templateUpdates.Count;

            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.ProgressChanged += Worker_ProgressChanged;
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            _worker.RunWorkerAsync();
        }

        private async void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            await DialogHost.Show(new CompletedDialog(), "RootDialog");

            Process mainProcess = new Process();
            mainProcess.StartInfo.FileName = _mainExecuteFilePath;
            mainProcess.StartInfo.WorkingDirectory = _destinationPath;
            mainProcess.Start();
            Application.Current.Shutdown();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DirectoryCopy(_sourceFilePath, _destinationPath, true);
            MoveConfigs();
            ClearFolder();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                int userState = (int)e.UserState;
                if (userState == UPDATE_TOTAL_CURRENT_FILE_LENGTH)
                {
                    _currentFileLength = e.ProgressPercentage;
                    CopiedLength.Text = "0 %";
                }
                else if (userState == UPDATE_COPIED_FILE_LENGTH)
                {
                    _copiedFileLength = e.ProgressPercentage;
                    CopiedLength.Text = (_copiedFileLength * 100 / _currentFileLength).ToString() + " %";
                }
                else if (userState == UPDATE_PROGRESS)
                {
                    pgbar1.Value = e.ProgressPercentage;
                    if (pgbar1.Value == 100)
                    {
                        if (_copied < _totalFiles - 1)
                            pgbar1.Value = 0;
                        _copied++;
                        pgbar2.Value = _copied;
                        CopiedFiles.Text = _copied.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CopyFile(string src, string dest, HashSet<string> templateUpdates)
        {
            try
            {
                // Không cop file ignore               
                if (_ignoreFiles.Contains(Path.GetFileName(src)))
                    return;

                // Không copy file nằm trong list tệp mở rộng
                if (_ignoreExtensionFile.Contains(Path.GetExtension(src)))
                    return;


                if (_excelExtensionFile.Contains(Path.GetExtension(src))
                    && !src.Contains("BackupTemplate")
                    && _appVersion == "1.11.0.0"
                    && Path.GetFileNameWithoutExtension(src).Contains("_update"))
                    return;

                if (_excelExtensionFile.Contains(Path.GetExtension(src))
                    && !src.Contains("BackupTemplate")
                    && _appVersion.CompareTo("1.11.0.0") > 0
                    && !templateUpdates.Contains(Path.GetFileNameWithoutExtension(src)))
                    return;


                //if (_excelExtensionFile.Contains(Path.GetExtension(src))
                //    && _appVersion == "1.11.0.0"
                //    && Path.GetFileNameWithoutExtension(src).Contains("_update")) return;

                //if (_excelExtensionFile.Contains(Path.GetExtension(src)) && _appVersion != "1.11.0.0")
                //{
                //    if (Path.GetFileNameWithoutExtension(src).Contains("_update"))
                //    {
                //        int indexVersion = Path.GetFileNameWithoutExtension(src).IndexOf("_update");
                //        string stringVersion = Path.GetFileNameWithoutExtension(src).Substring(indexVersion);
                //        string version = stringVersion.Substring(8);
                //        if (_appVersion.CompareTo(version) <= 0) dest = dest.Replace(stringVersion, "");
                //        else return;
                //    }
                //    else return;
                //};

                FileStream fsOut = new FileStream(dest, FileMode.Create);
                FileStream fsIn = new FileStream(src, FileMode.Open);
                byte[] bt = new byte[1048756];
                int readByte;

                while ((readByte = fsIn.Read(bt, 0, bt.Length)) > 0)
                {
                    fsOut.Write(bt, 0, readByte);
                    _worker.ReportProgress((int)(fsIn.Position * 100 / fsIn.Length), UPDATE_PROGRESS);
                    _worker.ReportProgress((int)fsIn.Position, UPDATE_COPIED_FILE_LENGTH);
                }
                fsIn.Close();
                fsOut.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            try
            {
                if (_ignoreFolders.Contains(Path.GetFileName(sourceDirName)))
                    return;

                // Get the subdirectories for the specified directory.
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);

                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourceDirName);
                }

                DirectoryInfo[] dirs = dir.GetDirectories();

                // If the destination directory doesn't exist, create it.       
                Directory.CreateDirectory(destDirName);

                // Get the files in the directory and copy them to the new location.
                FileInfo[] files = dir.GetFiles();
     
                foreach (FileInfo file in files)
                {
                    string tempPath = System.IO.Path.Combine(destDirName, file.Name);
                    _worker.ReportProgress((int)file.Length, UPDATE_TOTAL_CURRENT_FILE_LENGTH);
                    CopyFile(file.FullName, tempPath, _templateUpdates);
                }

                // If copying subdirectories, copy them and their contents to new location.
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string tempPath = System.IO.Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MoveConfigs()
        {
            string[] moveFiles = { "appsettings.json", "dbconfig.json", "log4net.config" };
            foreach (var file in moveFiles)
            {
                string sourceFile = Path.Combine(_destinationPath, file);
                if (File.Exists(sourceFile))
                {
                    string configsPath = Path.Combine(_destinationPath, "AppData/_configs");
                    if (!Directory.Exists(configsPath))
                    {
                        Directory.CreateDirectory(configsPath);
                    }
                    string destinationFile = Path.Combine(_destinationPath, configsPath, file);
                    File.Move(sourceFile, destinationFile);
                }
            }
        }

        private void ClearFolder()
        {
            var files = new string[] { };
            foreach (var file in files)
            {
                DeleteFile(file);
            }
            var folders = new string[]
            {
                "zh-Hant",
                "zh-Hans",
                "runtimes",
                "ru",
                "pt-BR",
                "ko",
                "ja",
                "it",
                "fr",
                "es" };
            foreach (var folder in folders)
            {
                DeleteFolder(folder);
            }
        }

        private void DeleteFile(string file)
        {
            try
            {
                string filePath = Path.Combine(_destinationPath, file);
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DeleteFolder(string folder)
        {
            try
            {
                string folderPath = Path.Combine(_destinationPath, folder);
                Directory.Delete(folderPath, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
