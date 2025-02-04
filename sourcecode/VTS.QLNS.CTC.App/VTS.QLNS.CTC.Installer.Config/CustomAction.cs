using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Deployment.WindowsInstaller;
using Newtonsoft.Json;

namespace SetupDatabase
{
    public class CustomActions
    {
        private const string DbTypeFile = @"DBType.txt";
        // step 2
        [CustomAction]
        public static ActionResult SetupDatabase(Session session)
        {
            string installPath = session["INSTALLFOLDER"];
            string dbType = ReadFileContent(Path.Combine(installPath, DbTypeFile));
            if (dbType.Equals("LocalDB"))
            {
                return ActionResult.Success;
            }
            frmDatabaseInfo frmInfo = new frmDatabaseInfo(installPath);
            if (frmInfo.ShowDialog() == DialogResult.Cancel)
                return ActionResult.UserExit;

            return ActionResult.Success;
        }

        // step 1
        [CustomAction]
        public static ActionResult SelectDBType(Session session)
        {
            DBForm frmInfo = new DBForm(session);
            if (frmInfo.ShowDialog() == DialogResult.Cancel)
                return ActionResult.UserExit;
            return ActionResult.Success;
        }

        // step 3
        [CustomAction]
        public static ActionResult SelectYearOfWork(Session session)
        {
            YearOfWork yearOfWorkForm = new YearOfWork(session);
            if (yearOfWorkForm.ShowDialog() == DialogResult.Cancel)
                return ActionResult.UserExit;
            return ActionResult.Success;
        }

        // step 4
        [CustomAction]
        public static ActionResult SetupSDK(Session session)
        {
            string installPath = session["INSTALLFOLDER"];
            string logPath = Path.Combine(installPath, "tracelog.txt");
            try
            {
                string dbType = ReadFileContent(Path.Combine(installPath, DbTypeFile));
                if (!string.IsNullOrEmpty(dbType) && dbType.Equals("LocalDB"))
                {
                    UpdateDbConfig(session);
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.FileName = @"cmd.exe";
                    string args = "/C" + Path.Combine(installPath, "SqlLocalDB.msi");
                    startInfo.Arguments = @args;
                    startInfo.WorkingDirectory = installPath;
                    startInfo.Verb = "runas";
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    process.Close();
                }
                // delete mdf file if connect to server db
                if (!string.IsNullOrEmpty(dbType) && !dbType.Equals("LocalDB"))
                {
                    string mdfFile = Path.Combine(Path.Combine(installPath, "AppData"), "QLNS_DV.mdf");
                    if (File.Exists(mdfFile))
                    {
                        File.Delete(mdfFile);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(logPath, nameof(SetupSDK) + "---" + ex.Message);
            }

            return ActionResult.Success;
        }

        public static string ReadFileContent(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return string.Empty;
            }
            using (StreamReader sr = File.OpenText(filePath))
            {
                string result = "";
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    result += s;
                }
                return result;
            }
        }

        public static void WriteLog(string filePath, string text)
        {
            string time = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            File.AppendAllText(filePath, time + " --- " + text + Environment.NewLine);
        }

        private static void UpdateDbConfig(Session session)
        {
            string installPath = session["INSTALLFOLDER"];
            string logPath = Path.Combine(installPath, "tracelog.txt");
            string DbConfigFile = Path.Combine(installPath, "AppData/_configs/dbconfig.json");
            try
            {
                string sqlLocalDbFileName = string.Format("QLNS_{0}", DateTime.Now.ToString("yyyyMMddhhmmss"));
                ConnectionStrings connectionStrings = new ConnectionStrings
                {
                    SqlServer = "Server=.;Database=QLNS_DV;User ID=ctc;Password=123456a@;",
                    LocalDb = string.Format("Server=(LocalDB)\\v11.0; Database={0}; Trusted_Connection=True; AttachDbFileName=|DataDirectory|AppData\\{0}.mdf", sqlLocalDbFileName),
                };
                DbSettings dbSettings = new DbSettings
                {
                    ConnectionType = "LocalDB"
                };
                object jsonObject = new { ConnectionStrings = connectionStrings, DbSettings = dbSettings };
                string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                System.IO.File.WriteAllText(DbConfigFile, json);
            }
            catch (Exception ex)
            {
                WriteLog(logPath, nameof(UpdateDbConfig) + "----" + ex.Message);
            }
        }
    }
}
