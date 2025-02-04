using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class AppVersionModel : ModelBase
    {
        private string _version;
        public string Version 
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }

        private string _description;
        public string Description 
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public byte[] Filestream { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        private long _fileSize;
        public long FileSize 
        {
            get => _fileSize;
            set
            {
                SetProperty(ref _fileSize, value);
                OnPropertyChanged(nameof(FileSizeToMb));
            }
        }

        private string _fileName;
        public string FileName 
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        public string FileSizeToMb => FileSize / 1000000 + " MB";
    }
}
