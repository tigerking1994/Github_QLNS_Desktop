using log4net;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ILog _loger;
        public DatabaseService(ILog loger)
        {
            _loger = loger;
        }

        //public void BackupLocal(string connectionString, string fileName, string filePath)
        //{
        //    SqlConnection.ClearAllPools();
        //    var builder = new SqlConnectionStringBuilder(connectionString);
        //    string fullPath = builder.AttachDBFilename;
        //    string schema = builder.InitialCatalog.IsEmpty() ? builder.AttachDBFilename : builder.InitialCatalog;
        //    string strSqlOffDB = " USE [master]; ALTER DATABASE [" + schema + "] SET OFFLINE WITH ROLLBACK IMMEDIATE;";
        //    string strSqlSetOnline = "USE [master];  ALTER DATABASE [" + schema + "] SET ONLINE;";
        //    SqlConnection connection = new SqlConnection(connectionString);

        //    try
        //    {
        //        using (connection)
        //        {
        //            connection.Open();
        //            //if (fullPath.IsEmpty())
        //            //{
        //            using (SqlCommand command = new SqlCommand())
        //            {
        //                command.CommandType = System.Data.CommandType.Text;
        //                command.Connection = connection;
        //                command.CommandText = "select physical_name from sys.database_files where type = 0";
        //                fullPath = (string)command.ExecuteScalar();
        //            }
        //            //}

        //            using (SqlCommand command = new SqlCommand())
        //            {
        //                command.CommandText = strSqlOffDB;
        //                command.CommandType = System.Data.CommandType.Text;
        //                command.Connection = connection;
        //                command.ExecuteNonQuery();
        //            }

        //            try
        //            {
        //                using (ZipArchive zip = ZipFile.Open(filePath, ZipArchiveMode.Create))
        //                {
        //                    //zip.CreateEntryFromFile(fullPath, schema.Split('\\').Last<string>().Replace(".mdf", "").Replace(".MDF", ""));
        //                    if (schema.ToLower().Contains(".mdf"))
        //                        zip.CreateEntryFromFile(fullPath, schema.Split('\\').Last<string>());
        //                    else
        //                        zip.CreateEntryFromFile(fullPath, schema.Split('\\').Last<string>() + ".mdf");
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                _loger.Error(e);
        //            }
        //            finally
        //            {
        //                using (SqlCommand command = new SqlCommand())
        //                {
        //                    command.CommandText = strSqlSetOnline;
        //                    command.CommandType = System.Data.CommandType.Text;
        //                    command.Connection = connection;
        //                    command.ExecuteNonQuery();
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _loger.Error(e);
        //    } 
        //}

        public void BackupLocal(string connectionString, string fileName, string filePath)
        {
            SqlConnection.ClearAllPools();
            var builder = new SqlConnectionStringBuilder(connectionString);
            string dirPath = Path.GetDirectoryName(filePath);
            string schema = string.Empty;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "select db_name() as [current database]";
                        command.CommandType = System.Data.CommandType.Text;
                        command.Connection = connection;
                        command.CommandTimeout = 300;
                        schema = (string)command.ExecuteScalar();
                    }

                    string dbName = schema.Split('\\').Last<string>().Replace(".mdf", "").Replace(".MDF", "");
                    string backUpFileName = string.Concat(dbName, "_", DateTime.Now.ToString("ddMMyyyyhhmmss"));
                    string backUpFilePath = string.Concat(dirPath, "\\", backUpFileName);

                    string sqlBackUp = string.Format("USE [master]; BACKUP DATABASE [{0}] TO DISK = '{1}'", schema, backUpFilePath);

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = sqlBackUp;
                        command.CommandType = System.Data.CommandType.Text;
                        command.Connection = connection;
                        command.CommandTimeout = 900;
                        command.ExecuteNonQuery();
                    }

                    try
                    {
                        using (ZipArchive zip = ZipFile.Open(filePath, ZipArchiveMode.Create))
                        {
                            zip.CreateEntryFromFile(backUpFilePath, backUpFileName);
                        }
                    }
                    catch (Exception e)
                    {
                        _loger.Error(e);
                    }

                    File.Delete(backUpFilePath);
                }
            }
            catch (Exception e)
            {
                _loger.Error(e);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool RestoreLocal(string connectionString, string filePath, string mdfFileName)
        {
            SqlConnection.ClearAllPools();
            bool isSuccess = true;
            var builder = new SqlConnectionStringBuilder(connectionString);
            string fullPath = string.Empty;
            string dirPath = Path.GetDirectoryName(filePath);
            string restoreFileName = string.Empty;
            string restoreFilePath = string.Empty;
            string schema = string.Empty;
            string logicalName = string.Empty;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    // get current database name
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "select db_name() as [current database]";
                        command.CommandType = System.Data.CommandType.Text;
                        command.Connection = connection;
                        command.CommandTimeout = 300;
                        schema = (string)command.ExecuteScalar();
                    }

                    // unzip restore file
                    using (ZipArchive zipArchive = ZipFile.OpenRead(filePath))
                    {
                        zipArchive.ExtractToDirectory(dirPath);
                        restoreFileName = zipArchive.Entries[0].FullName;
                        restoreFilePath = Path.Combine(dirPath, restoreFileName);
                    }

                    // get physical mdf path
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.Connection = connection;
                        command.CommandText = "select physical_name from sys.database_files where type = 0";
                        command.CommandTimeout = 300;
                        fullPath = (string)command.ExecuteScalar();
                    }

                    // get logical file name
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.Connection = connection;
                        command.CommandText = string.Format("DECLARE @Table TABLE (LogicalName varchar(128),[PhysicalName] varchar(128), [Type] varchar, [FileGroupName] varchar(128), [Size] varchar(128), " +
                            "[MaxSize] varchar(128), [FileId] varchar(128), [CreateLSN] varchar(128), [DropLSN] varchar(128), [UniqueId] varchar(128), [ReadOnlyLSN] varchar(128), [ReadWriteLSN] varchar(128), " +
                            "[BackupSizeInBytes] varchar(128), [SourceBlockSize] varchar(128), [FileGroupId] varchar(128), [LogGroupGUID] varchar(128), [DifferentialBaseLSN] varchar(128), [DifferentialBaseGUID] varchar(128), [IsReadOnly] varchar(128), [IsPresent] varchar(128), [TDEThumbprint] varchar(128) " + 
                            ") " + 
                            "DECLARE @Path varchar(1000) = '{0}' " +
                            "DECLARE @LogicalNameData varchar(128),@LogicalNameLog varchar(128) " +
                            "INSERT INTO @table " +
                            "EXEC(' " +
                            "RESTORE FILELISTONLY " +
                            "FROM DISK = ''' +@Path+ ''' " +
                            "') " +
                            "SET @LogicalNameData = (SELECT LogicalName FROM @Table WHERE Type = 'D') " +
                            "SELECT @LogicalNameData", restoreFilePath);
                        command.CommandTimeout = 900;
                        logicalName = (string)command.ExecuteScalar();
                    }

                    // set single user
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", schema);
                        command.CommandType = System.Data.CommandType.Text;
                        command.Connection = connection;
                        command.CommandTimeout = 900;
                        command.ExecuteNonQuery();
                    }

                    // restore database 
                    string sqlRestore = string.Format("USE [master]; RESTORE DATABASE [{0}] FROM DISK = '{1}' WITH REPLACE, ", schema, restoreFilePath);
                    string currentLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    sqlRestore += string.Format("move '{0}' to '{1}', ", logicalName, fullPath);
                    sqlRestore += string.Format("move '{0}_log' to '{1}' ", logicalName, fullPath.Replace(".mdf", "_log.ldf"));

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = sqlRestore;
                        command.CommandType = System.Data.CommandType.Text;
                        command.Connection = connection;
                        command.CommandTimeout = 900;
                        command.ExecuteNonQuery();
                    }

                    // set multi user
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = string.Format("ALTER DATABASE [{0}] SET MULTI_USER", schema);
                        command.CommandType = System.Data.CommandType.Text;
                        command.Connection = connection;
                        command.CommandTimeout = 900;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                isSuccess = false;
                _loger.Error(e);
            }
            finally
            {
                File.Delete(restoreFilePath);
                connection.Close();
            }

            return isSuccess;
        }

        public void BackupSqlServer(string connectionString, string fileName, string filePath)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            string fileNameBak = fileName.Replace(".zip", ".bak");
            string filePathBak = filePath.Replace(".zip", ".bak");
            string sql = string.Format("BACKUP DATABASE {0} TO DISK = '{1}'", (object)connectionStringBuilder.InitialCatalog, (object)filePathBak);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
                connection.Close();
                using (ZipArchive zip = ZipFile.Open(filePath, ZipArchiveMode.Create))
                {
                    zip.CreateEntryFromFile(filePathBak, fileNameBak);
                }
                File.Delete(filePathBak);
            }
        }

        public bool CheckConnection(string connectionString)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception e)
            {
                _loger.Error(e);
                return false;
            }
            return true;
        }

        public string CreateSqlFileName()
        {
            return string.Format("ns_query_{0}.sql", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        }

        public string CreateTableExcelFileName()
        {
            return string.Format("Table_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        }

        public DataTable ExecuteQuery(string query, string connectionString)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                    adapter.Dispose();
                }
                connection.Close();
            }
            return dt;
        }

        public void ExecuteNonQuery(string query, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }


        public string GetBackupFilename(string connectionString, string databaseType)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            return string.Format("{0}_{1}_v{2}_{3}.zip", (object)(string.IsNullOrEmpty(connectionStringBuilder.AttachDBFilename) ? connectionStringBuilder.InitialCatalog.Split('\\').Last<string>().Replace(".mdf", "").Replace(".MDF", "") : connectionStringBuilder.AttachDBFilename.Split('\\').Last<string>().Replace(".mdf", "").Replace(".MDF", "")), databaseType, "1.0", (object)DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        }

        //public bool RestoreLocal(string connectionString, string filePath, string mdfFileName)
        //{
        //    bool isSuccess = true;
        //    SqlConnection.ClearAllPools();
        //    var builder = new SqlConnectionStringBuilder(connectionString);
        //    string schema = builder.InitialCatalog.IsEmpty() ? builder.AttachDBFilename : builder.InitialCatalog;
        //    string fullPath = builder.AttachDBFilename;
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            connection.Open();

        //            using (SqlCommand command = new SqlCommand())
        //            {
        //                command.CommandType = System.Data.CommandType.Text;
        //                command.Connection = connection;
        //                command.CommandText = "select physical_name from sys.database_files where type = 0";
        //                fullPath = (string)command.ExecuteScalar();
        //            }

        //            string dbFoler = fullPath.Substring(0, fullPath.LastIndexOf('\\'));

        //            using (SqlCommand command = new SqlCommand())
        //            {
        //                string sql = string.Format(" USE [master]; ALTER DATABASE [{0}] SET OFFLINE WITH ROLLBACK IMMEDIATE ", schema);
        //                command.CommandText = sql;
        //                command.CommandType = System.Data.CommandType.Text;
        //                command.Connection = connection;
        //                command.ExecuteNonQuery();
        //            }

        //            try
        //            {
        //                using (ZipArchive zip = ZipFile.Open(string.Concat(dbFoler, "\\", 
        //                                                     schema.Split('\\').Last<string>().Replace(".mdf", ""), 
        //                                                     "_Backup_", 
        //                                                     DateTime.Now.ToString("yyyyMMddhhmmss"), 
        //                                                     ".zip"), 
        //                                                     ZipArchiveMode.Create))
        //                {
        //                    if (schema.ToLower().Contains(".mdf"))
        //                        zip.CreateEntryFromFile(fullPath, schema.Split('\\').Last<string>());
        //                    else
        //                        zip.CreateEntryFromFile(fullPath, schema.Split('\\').Last<string>() + ".mdf");
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                _loger.Error(e);
        //            }

        //            if (File.Exists(fullPath.Replace(".mdf", "_log.ldf"))) File.Delete(fullPath.Replace(".mdf", "_log.ldf"));
        //            if (File.Exists(fullPath)) File.Delete(fullPath);

        //            using (ZipArchive zipArchive = ZipFile.OpenRead(filePath))
        //            {
        //                zipArchive.ExtractToDirectory(dbFoler);
        //                if (!zipArchive.Entries[0].FullName.Equals(fullPath.Split('\\').Last()))
        //                    File.Move(dbFoler + '\\' + zipArchive.Entries[0].FullName, fullPath);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            isSuccess = false;
        //            _loger.Error(ex);
        //        }
        //        finally
        //        {
        //            using (SqlCommand command = new SqlCommand())
        //            {
        //                string sql = string.Format(" USE [master]; ALTER DATABASE [{0}] SET ONLINE", schema);
        //                command.CommandText = sql;
        //                command.CommandType = System.Data.CommandType.Text;
        //                command.Connection = connection;
        //                command.ExecuteNonQuery();
        //            }
        //            connection.Close();
        //        }
        //    }

        //    return isSuccess;
        //}

        public void RestoreSqlServer(string connectionString, string database, string filePath)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                new SqlCommand(string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", (object)database), connection).ExecuteNonQuery();
                new SqlCommand(string.Format("USE MASTER RESTORE DATABASE [{0}] FROM DISK='{1}' WITH REPLACE;", (object)database, (object)filePath), connection).ExecuteNonQuery();
                new SqlCommand(string.Format("ALTER DATABASE [{0}] SET MULTI_USER", (object)database), connection).ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
