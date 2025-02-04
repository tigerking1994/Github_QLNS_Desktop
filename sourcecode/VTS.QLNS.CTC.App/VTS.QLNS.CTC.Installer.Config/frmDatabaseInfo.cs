using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SetupDatabase
{
    public partial class frmDatabaseInfo : Form
    {
        private string _installPath;
        private const string DbTypeFile = @"DBType.txt";
        private string DbConfigFile = @"dbconfig.json";
        private readonly string logPath;

        public frmDatabaseInfo(string installPath)
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            this.TopMost = true;
            this.CenterToScreen();
            _installPath = installPath;
            DbConfigFile = Path.Combine(_installPath, DbConfigFile);
            logPath = Path.Combine(installPath, "tracelog.txt");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            UpdateDbConfig();
            bool valid = false;
            if (!String.IsNullOrEmpty(txtServer.Text) && !String.IsNullOrEmpty(txtDatabase.Text))
                valid = VerifyLicenseInfo(txtServer.Text, txtDatabase.Text);

            if (!valid)
            {
                MessageBox.Show("You license information does not appear to be valid. Please try again.", "Invalid info");
            }
            else
            {
                this.DialogResult = DialogResult.Yes;
            }
        }

        private bool VerifyLicenseInfo(string registeredName, string key)
        {
            // Connect to License server or run algorithm check to 
            // verify license key.
            return true;
        }

        private void UpdateDbConfig()
        {
            try
            {
                string dbType = CustomActions.ReadFileContent(Path.Combine(_installPath, DbTypeFile));
                ConnectionStrings connectionStrings = new ConnectionStrings
                {
                    SqlServer = "Server=" + this.txtServer.Text.Trim() + ";Database=" + this.txtDatabase.Text.Trim() + ";User ID=" + this.textUsername.Text.Trim() + ";Password=" + this.textPassword.Text + ";",
                    LocalDb = "Server=OS_SPGP_GPDN207\\SQLEXPRESS; Integrated Security=true;AttachDbFileName=D:\\Backup\\QLNS_DV.mdf",
                };
                DbSettings dbSettings = new DbSettings
                {
                    ConnectionType = "SqlServer"
                };
                object jsonObject = new { ConnectionStrings = connectionStrings, DbSettings = dbSettings };
                string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                System.IO.File.WriteAllText(DbConfigFile, json);
            }
            catch (Exception ex)
            {
                CustomActions.WriteLog(logPath, nameof(frmDatabaseInfo) + "---" + nameof(UpdateDbConfig) + "----" + ex.Message);
            }
        }
    }
}
