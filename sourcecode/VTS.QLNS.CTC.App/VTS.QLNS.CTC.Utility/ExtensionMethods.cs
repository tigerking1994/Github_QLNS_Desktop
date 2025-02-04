using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace VTS.QLNS.CTC.Utility
{
    public static class ExtensionMethods
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static List<string> GetMaPhuCapByFormula(string sFormula)
        {
            var delimiters = new char[] { '+', '-', '*', '/', '(', ')' };
            if (string.IsNullOrEmpty(sFormula)) return new List<string>();
            return sFormula.Split(delimiters).ToList();
        }

        public static IEnumerable<T> CheckPassElementOrGetDefault10Element<T>(this List<T> lstInput) where T : class, new()

        {
            if (lstInput.IsEmpty() || lstInput.Count() < (int)ConstantNumber.TEN)
            {
                if (lstInput == null) lstInput = new List<T>();
                while ((int)ConstantNumber.TEN > lstInput.Count())
                {
                    lstInput.Add(new T());
                }
            }
            return lstInput;
        }
    }
}
