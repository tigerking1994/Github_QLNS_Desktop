using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace VTS.QLNS.CTC.Core.Extensions
{
    public static class SqlMapperExtension
    {

        private static void WriteLineQuery(string sql, params object[] parameters)
        {
            if (!sql.ToLower().Contains("execute"))
                return;
            string table = sql.Split(' ')[1].Replace("dbo.", "").Replace("DBO.", "");
            StringBuilder stringClear = new StringBuilder();
            stringClear.AppendLine($@"
                ---------------------------------------------------------
                DECLARE	@return_value int
                EXEC @return_value = [dbo].[{table}]");
            try
            {
                if (parameters.Length > 0)
                {
                    foreach (object item in parameters)
                    {
                        if (item is SqlParameter p)
                        {
                            if (p.Value == DBNull.Value || p.Value == null)
                            {
                                stringClear.AppendLine(@$"                {p.ParameterName} = NULL,");
                            }
                            else if (new List<SqlDbType> { SqlDbType.NText, SqlDbType.NVarChar, SqlDbType.NChar }.Contains(p.SqlDbType))
                            {
                                stringClear.AppendLine(@$"                {p.ParameterName} = N'{p.Value.ToString()}',");
                            }
                            else if (new List<SqlDbType> { SqlDbType.UniqueIdentifier, SqlDbType.VarChar, SqlDbType.Date, SqlDbType.DateTime, SqlDbType.DateTime2, SqlDbType.Time, SqlDbType.Text, SqlDbType.Char }.Contains(p.SqlDbType))
                            {
                                stringClear.AppendLine(@$"                {p.ParameterName} = '{p.Value.ToString()}',");
                            }
                            else
                            {
                                stringClear.AppendLine(@$"                {p.ParameterName} = {p.Value.ToString()},");
                            }
                        }
                    }
                    stringClear.Append($@"SELECT 'Return Value' = @return_value
                ---------------------------------------------------------");
                    string newLine = Environment.NewLine;
                    Console.WriteLine(stringClear.Replace($",{newLine}SELECT", $"{newLine}                SELECT"));
                }

            }
            catch (Exception)
            {
                return;
                //throw;
            }
            return;
        }
        public static IEnumerable<T> FromSqlRaw<T>(this ApplicationDbContext source, string sql, params object[] parameters)
        {
            ColumnMapper.Mapper<T>();
            using (DbConnection connection = source.Database.GetDbConnection())
            {
                DynamicParameters param = new DynamicParameters();
                if (parameters.Length > 0)
                {
                    foreach (object item in parameters)
                    {
                        if (item is SqlParameter p)
                        {
                            param.Add(p.ParameterName, p.Value != DBNull.Value ? p.Value : null);
                        }
                    }
                }
#if DEBUG
                WriteLineQuery(sql, parameters);
#endif
                return connection.Query<T>(sql, param);
            }
        }

        public static DataTable FromSqlCommand(this ApplicationDbContext source, string sql, params object[] parameters)
        {
            return source.FromSqlCommand(sql, CommandType.StoredProcedure, 600, parameters);
        }

        public static DataTable FromSqlCommand(this ApplicationDbContext source, string sql, CommandType commandType, params object[] parameters)
        {
            return source.FromSqlCommand(sql, commandType, 600, parameters);
        }

        public static DataTable FromSqlCommand(this ApplicationDbContext source, string sql, CommandType commandType, int timeout, params object[] parameters)
        {
            using (DbConnection connection = source.Database.GetDbConnection())
            {
                DbCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);
                cmd.CommandTimeout = timeout;

                DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(connection);
                using (DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter())
                {
                    DataTable rs = new DataTable();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(rs);
                    return rs;
                }
            }
        }

        public static DataTable GetDataTable(this ApplicationDbContext source, string sql, CommandType commandType, int timeout = 600)
        {
            DbConnection connection = source.Database.GetDbConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = commandType;
            cmd.CommandTimeout = timeout;

            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(connection);
            using (DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter())
            {
                DataTable rs = new DataTable();
                adapter.SelectCommand = cmd;
                adapter.Fill(rs);
                return rs;
            }
        }

        public static class ColumnMapper
        {
            public static void Mapper<T>()
            {
                CustomPropertyTypeMap map = new CustomPropertyTypeMap(typeof(T), (type, columnName)
                  => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName.ToLower()));
                SqlMapper.SetTypeMap(typeof(T), map);
            }

            static string GetDescriptionFromAttribute(MemberInfo member)
            {
                if (member == null) return null;

                ColumnAttribute attrib = (ColumnAttribute)Attribute.GetCustomAttribute(member, typeof(ColumnAttribute), false);
                return (attrib == null ? member.Name : attrib.Name).ToLower();
            }
        }
    }
}
