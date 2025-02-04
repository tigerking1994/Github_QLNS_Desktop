using Microsoft.Extensions.Configuration;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.App.ViewModel.Setting;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.Impl
{
    public abstract class StorageService
    {
        protected string _uploadStoragePath;
        protected string _ftpHost;
        protected string _ftpUser;
        protected string _ftpPassword;
        private long _maxFileLengthInMb = 10;

        protected string EncodeFileName(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            return CreateMD5(string.Format("{0}_{1}", fileName, DateTime.Now.ToStringTimeStamp())) + extension;
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public bool ValidateMaxlength(params string[] sourceFilePaths)
        {
            foreach(var path in sourceFilePaths)
            {
                FileInfo fileInfo = new FileInfo(path);
                if (!fileInfo.Exists)
                {
                    MessageBox.Show(Resources.ErrorFileNotExists, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                if (fileInfo.Length > _maxFileLengthInMb * 1000000)
                {
                    MessageBox.Show(string.Format(Resources.ErrorFileSize, _maxFileLengthInMb), Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            return true;
        }

        protected bool ValidateMaxlength(List<AttachmentModel> attachments)
        {
            foreach (var item in attachments)
            {
                FileInfo fileInfo = new FileInfo(item.FilePath);
                if (!fileInfo.Exists)
                {
                    MessageBox.Show(Resources.ErrorFileNotExists, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                if (fileInfo.Length > _maxFileLengthInMb * 1000000)
                {
                    MessageBox.Show(string.Format(Resources.ErrorFileSize, _maxFileLengthInMb), Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            return true;
        }

        protected void ShowMessageNotExistFile()
        {
            MessageBox.Show("Tệp không tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
