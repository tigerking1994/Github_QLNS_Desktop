using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.Budget.Allocation;
using VTS.QLNS.CTC.App.View.Budget.Allocation.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.IO;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SystemAdmin.Utilities;
using AutoMapper.Configuration;
using System.Windows.Forms;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.Utilities
{
    public class GetLogFileViewModel : ViewModelBase
    {
        public override string Name => "Lấy file log";
        public override string Description => "Lấy file log";
        public override string Title => "Lấy file log";
        public override Type ContentType => typeof(GetLogFile);
        public override PackIconKind IconKind => PackIconKind.DatabaseCogOutline;
        private readonly IConfiguration _configuration;
        private readonly string _logPath;

        public RelayCommand GetFileCommand { get; set; }

        public GetLogFileViewModel()
        {
            GetFileCommand = new RelayCommand(obj => OnGetFile());
        }

        private void OnGetFile()
        {
            try
            {
                string currentPath = AppDomain.CurrentDomain.BaseDirectory;
                string pathFolder = Path.Combine(currentPath, "logs");
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    return;
                }
                DirectoryInfo d = new DirectoryInfo(pathFolder);

                FileInfo[] listFile = d.GetFiles("*.log");
                if (listFile != null && listFile.Count() > 0)
                {
                    string fileNew = listFile.OrderByDescending(n => n.Name).FirstOrDefault().Name;
                    using (var folder = new FolderBrowserDialog())
                    {
                        DialogResult result = folder.ShowDialog();

                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folder.SelectedPath))
                        {
                            System.IO.File.Copy(Path.Combine(pathFolder, fileNew), Path.Combine(folder.SelectedPath, fileNew), true);

                            IOExtensions.OpenFolder(folder.SelectedPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public override void Init()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}
