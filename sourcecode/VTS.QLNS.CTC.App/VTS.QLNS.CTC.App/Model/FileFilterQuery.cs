using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class FileFilterQuery
    {

        private Dictionary<string, string> dictTimeModule = new Dictionary<string, string>()
        {
            ["1,2,3"] = "I",
            ["4,5,6"] = "II",
            ["7,8,9"] = "III",
            ["10,11,12"] = "IV",
            ["I"] = "I",
            ["II"] = "II",
            ["III"] = "III",
            ["IV"] = "IV",
        };
        private string GetHash(string input)
        {
            SHA256 sha256Hash = SHA256.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString(0, 8);
        }

        public Guid Id { get; set; }
        public int Total { get; set; }
        public long Ordinal { get; set; }
        public string AgencyName { get; set; }
        public string AgencyCode { get; set; }
        public string AgencyDepartment { get; set; }
        public string FileDepartment { get; set; }
        public string FileDescription { get; set; }
        public string FileName { get; set; }
        public string SubModule { get; set; }
        public string TimeModule { get; set; }
        public string TimeModuleDisplay
        {
            get
            {
                if (int.TryParse(TimeModule, out var i))
                {
                    return $"Tháng {i}";
                }
                else if (dictTimeModule.TryGetValue(TimeModule, out string value))
                {
                    return $"Quý {value}";
                }
                else
                {
                    return TimeModule;
                }
            }
        }
        public string FileTokenKey { get; set; }
        public Guid FileId { get; set; }
        public string QuarterMonth { get; set; }
        public string FolderPath { get; set; }
        public string FilePath { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedFormat
        {
            get { return LastModified.Value.ToString("dd/MM/yyyy HH:mm:ss"); }
        }
        public string HashTokenKey => GetHash(FileTokenKey ?? string.Empty);
        public string Extension => Path.GetExtension(FilePath ?? string.Empty);
    }
}