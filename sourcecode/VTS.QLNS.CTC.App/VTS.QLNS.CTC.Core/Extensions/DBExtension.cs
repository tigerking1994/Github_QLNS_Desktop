using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace VTS.QLNS.CTC.Core.Extensions
{
    public static class DBExtension
    {
        public static DataTable ConvertDataToTableDefined<T>(string sTypeName, List<T> lstData) where T: class
        {
            DataTable dt = new DataTable();
            dt.TableName = sTypeName;
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach(var item in properties)
            {
                if (item.CustomAttributes != null && ((IEnumerable<CustomAttributeData>)item.CustomAttributes).Any()) continue;
                dt.Columns.Add(item.Name, Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType);
            }
            foreach (T item in lstData) {
                DataRow dr = dt.NewRow();
                foreach(var prop in properties)
                {
                    if (dr.Table.Columns.IndexOf(prop.Name) == -1) continue;
                    dr[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataTable ConvertDataToGuidTable(List<Guid> lstdata)
        {
            DataTable dt = new DataTable();
            dt.TableName = "t_tbl_uniqueidentifier";
            dt.Columns.Add("Id", Nullable.GetUnderlyingType(typeof(Guid)) ?? typeof(Guid));
            foreach(var item in lstdata)
            {
                DataRow dr = dt.NewRow();
                dr["Id"] = item;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataTable ConvertDataToStringTable(List<string> lstdata)
        {
            DataTable dt = new DataTable();
            dt.TableName = "t_tbl_string";
            dt.Columns.Add("sId", Nullable.GetUnderlyingType(typeof(string)) ?? typeof(string));
            foreach (var item in lstdata)
            {
                DataRow dr = dt.NewRow();
                dr["sId"] = item;
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
