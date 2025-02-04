using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IDatabaseService
    {
        DataTable ExecuteQuery(string query, string connectionString);
        void ExecuteNonQuery(string query, string connectionString);
        void BackupSqlServer(string connectionString, string fileName, string filePath);
        void RestoreSqlServer(string connectionString, string database, string filePath);
        string GetBackupFilename(string connectionString, string databaseType);
        string CreateSqlFileName();
        string CreateTableExcelFileName();
        bool CheckConnection(string connectionString);
        void BackupLocal(string connectionString, string fileName, string filePath);
        bool RestoreLocal(string connectionString, string filePath, string mdfFileName);
    }
}
