using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Setting
{
    public class DatabaseConfigurationModel : BindableBase
    {
        private string _server;
        public string Server
        {
            get => _server;
            set => SetProperty(ref _server, value);
        }

        private string _userId;
        public string UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _dbName;
        public string DbName
        {
            get => _dbName;
            set => SetProperty(ref _dbName, value);
        }

        private string _dbPath;
        public string DbPath
        {
            get => _dbPath;
            set => SetProperty(ref _dbPath, value);
        }

        private string _connectionString;
        public string ConnectionString
        {
            get => _connectionString;
            set => SetProperty(ref _connectionString, value);
        }
    }
}
