using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class BackupFileModel : BindableBase
    {
        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private string _databaseName;
        public string DatabaseName
        {
            get => _databaseName;
            set => SetProperty(ref _databaseName, value);
        }

        private string _databaseType;
        public string DatabaseType
        {
            get => _databaseType;
            set => SetProperty(ref _databaseType, value);
        }

        private string _appVersion;
        public string AppVersion
        {
            get => _appVersion;
            set => SetProperty(ref _appVersion, value);
        }

        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get => _dateCreated;
            set => SetProperty(ref _dateCreated, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private string _fileSize;
        public string FileSize
        {
            get => _fileSize;
            set => SetProperty(ref _fileSize, value);
        }
    }
}
