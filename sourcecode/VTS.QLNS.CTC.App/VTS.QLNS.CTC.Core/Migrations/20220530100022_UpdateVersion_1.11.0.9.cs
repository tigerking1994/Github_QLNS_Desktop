using Microsoft.EntityFrameworkCore.Migrations;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11109 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (ExistsData() == 0)
                migrationBuilder.RunSqlScript("AppData/_db/07_script_2023.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }

        private static int ExistsData()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AppData\\_configs\\dbconfig.json");
            string data = ReadFileContent(filePath);

            DbConfig result = TypeUtilities.StringToObJect<DbConfig>(data);
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = result.DbSettings.ConnectionType.ToLower() == "LocalDb".ToLower() ? result.ConnectionStrings.LocalDb : result.ConnectionStrings.SqlServer;
                conn.Open();
                using (SqlCommand queryCmd = new SqlCommand())
                {
                    queryCmd.Connection = conn;
                    queryCmd.CommandText = "select count(*) from DanhMuc where sType = 'NS_NamLamViec' and sGiaTri = '2023'";
                    queryCmd.CommandTimeout = 120;

                    return (int)queryCmd.ExecuteScalar();
                }
            }
        }

        private static string ReadFileContent(string filePath)
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
    }

    public class ConnectionStrings
    {
        public string SqlServer { get; set; }
        public string LocalDb { get; set; }
    }

    public class DbConfig
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public DbSettings DbSettings { get; set; }
    }

    public class DbSettings
    {
        public string ConnectionType { get; set; }
    }
}
