using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace VTS.QLNS.CTC.Utility
{
    public static class IOExtensions
    {
        public static string ApplicationPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string TempPath
        {
            get
            {
                string path = Path.Combine(Path.GetTempPath(), "QLNS.DV");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        public static string CreateTempFile(string path, string extension)
        {
            return IOExtensions.CreateTempFile(path, extension, string.Empty);
        }

        public static string CreateTempFile(string path, string extension, string fileNameWithoutExtension)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = fileNameWithoutExtension.IsEmpty() ? Guid.NewGuid().ToString() + extension : fileNameWithoutExtension + extension;
            return Path.Combine(path, fileName);
        }

        public static string CreateTempFile2(string path, string extension, string fileNameWithoutExtension)
        {
            string fileName = fileNameWithoutExtension.IsEmpty() ? Guid.NewGuid().ToString() + extension : fileNameWithoutExtension + extension;
            return Path.Combine(path, fileName);
        }

        public static void OpenFiles(string filepath) => Process.Start("explorer.exe", string.Format("/select,{0}", filepath));
        public static void OpenFolder(string folder) => Process.Start("explorer.exe", string.Format("{0}", folder));

        public static string FileDialogFilterByExtension(string ext)
        {
            string filter = string.Format("All types (*.*)|*.*");
            switch (ext)
            {
                case FileExtensionFormats.Xlsx:
                    filter = string.Format("Microsoft Office Excel Worksheet (*{0})|*{0}|All types (*.*)|*.*", ext);
                    break;
                case FileExtensionFormats.Pdf:
                    filter = string.Format("PDF document (*{0})|*{0}|All types (*.*)|*.*", ext);
                    break;
                default:
                    break;
            }
            return filter;
        }

        public static void ClearForlder(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public static void CopyFilesRecursively(string sourcePath, string targetPath, string ignorePath)
        {
            // Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                if (!dirPath.Equals(ignorePath.Replace("/", "\\")))
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                }
            }

            // Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        public static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static Byte[] ToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            byte[] chunk = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(chunk, 0, chunk.Length)) > 0)
            {
                ms.Write(chunk, 0, bytesRead);
            }

            return ms.ToArray();
        }

        public static void WriteLogChangeDatabase(string sFromFile, string sToFile)
        {
            try
            {
                string sLogPath = Path.Combine(ApplicationPath, "AppData", "LogChangeDb");
                if (!Directory.Exists(sLogPath))
                {
                    Directory.CreateDirectory(sLogPath);
                }
                using (StreamWriter stream = new StreamWriter(Path.Combine(sLogPath, "LogChangeDb.txt"), true))
                {
                    stream.WriteLine(string.Format("[{0}] : chuyển từ database [{1}] sang database [{2}] .",
                            DateTime.Now.ToString("dd/MM/yyyy HH:mm"), sFromFile, sToFile));
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
