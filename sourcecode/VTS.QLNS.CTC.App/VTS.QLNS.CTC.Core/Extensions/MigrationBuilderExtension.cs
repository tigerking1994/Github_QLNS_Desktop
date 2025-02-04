using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace VTS.QLNS.CTC.Core.Extensions
{
    public static class MigrationBuilderExtension
    {
        public static void RunSqlScript(this MigrationBuilder source, string script)
        {
            List<MigrationStatement> ms = new List<MigrationStatement>();
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(executableLocation, script);
            using var stream = File.OpenRead(path);
            using var reader = new StreamReader(stream);
            var sqlResult = reader.ReadToEnd();
            StatementBatch(ms, sqlResult);
            foreach (var item in ms)
            {
                if (!string.IsNullOrEmpty(item.Sql))
                    source.Sql(item.Sql);
            }
        }

        static void StatementBatch(List<MigrationStatement> ms, string sqlBatch, bool suppressTransaction = false)
        {
            string BatchTerminator = "GO";

            // Handle backslash utility statement (see http://technet.microsoft.com/en-us/library/dd207007.aspx)
            sqlBatch = Regex.Replace(sqlBatch, @"\\(\r\n|\r|\n)", "");

            // Handle batch splitting utility statement (see http://technet.microsoft.com/en-us/library/ms188037.aspx)
            var batches = Regex.Split(sqlBatch,
                String.Format(CultureInfo.InvariantCulture, @"^\s*({0}[ \t]+[0-9]+|{0})(?:\s+|$)", BatchTerminator),
                RegexOptions.IgnoreCase | RegexOptions.Multiline);

            for (int i = 0; i < batches.Length; ++i)
            {
                // Skip batches that merely contain the batch terminator
                if (batches[i].StartsWith(BatchTerminator, StringComparison.OrdinalIgnoreCase) ||
                    (i == batches.Length - 1 && string.IsNullOrWhiteSpace(batches[i])))
                {
                    continue;
                }

                // Include batch terminator if the next element is a batch terminator
                if (batches.Length > i + 1 &&
                    batches[i + 1].StartsWith(BatchTerminator, StringComparison.OrdinalIgnoreCase))
                {
                    int repeatCount = 1;

                    // Handle count parameter on the batch splitting utility statement
                    if (!batches[i + 1].ToLower().Equals(BatchTerminator.ToLower()))
                    {
                        repeatCount = int.Parse(Regex.Match(batches[i + 1], @"([0-9]+)").Value, CultureInfo.InvariantCulture);
                    }

                    for (int j = 0; j < repeatCount; ++j)
                        ms.Add(new MigrationStatement { Sql = batches[i], SuppressTransaction = suppressTransaction, BatchTerminator = BatchTerminator });
                }
                else
                {
                    ms.Add(new MigrationStatement { Sql = batches[i], SuppressTransaction = suppressTransaction });
                }
            }
        }

        public class MigrationStatement
        {
            /// <summary>
            /// Gets or sets the SQL to be executed to perform this migration operation.
            ///
            /// Entity Framework Migrations APIs are not designed to accept input provided by untrusted sources 
            /// (such as the end user of an application). If input is accepted from such sources it should be validated 
            /// before being passed to these APIs to protect against SQL injection attacks etc.
            /// </summary>
            public string Sql { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this statement should be performed outside of
            /// the transaction scope that is used to make the migration process transactional.
            /// If set to true, this operation will not be rolled back if the migration process fails.
            /// </summary>
            public bool SuppressTransaction { get; set; }

            /// <summary>
            /// Gets or sets the batch terminator for the database provider.
            ///
            /// Entity Framework Migrations APIs are not designed to accept input provided by untrusted sources 
            /// (such as the end user of an application). If input is accepted from such sources it should be validated 
            /// before being passed to these APIs to protect against SQL injection attacks etc.
            /// </summary>
            /// <value>
            /// The batch terminator for the database provider.
            /// </value>
            public string BatchTerminator { get; set; }
        }
    }
}
