using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetupDatabase
{
    public partial class YearOfWork : Form
    {
        public YearOfWork()
        {
            InitializeComponent();
        }

        private const string DbTypeFile = @"YearOfWork.txt";
        private readonly Session _session;
        private readonly string logPath;

        public YearOfWork(Session session)
        {
            _session = session;
            var installPath = _session["INSTALLFOLDER"];
            logPath = Path.Combine(installPath, "tracelog.txt");
            InitializeComponent();
            this.CenterToScreen();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                CustomActions.WriteLog(logPath, "choose year of work");
                var _installPath = _session["INSTALLFOLDER"];
                string dbType = this.comboBox1.SelectedItem.ToString();
                CreateAndWriteToFile(Path.Combine(_installPath, DbTypeFile), dbType);
                this.DialogResult = DialogResult.Yes;
            }
            catch (Exception ex)
            {
                CustomActions.WriteLog(logPath, nameof(DBForm) + "---" + nameof(YearOfWork.btnNext_Click) + ex.Message);
            }
        }

        public static void CreateAndWriteToFile(string filePath, string text)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (FileStream fs = File.Create(filePath))
            {
                // Add some text to file    
                byte[] bytes = new UTF8Encoding(true).GetBytes(text);
                fs.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
