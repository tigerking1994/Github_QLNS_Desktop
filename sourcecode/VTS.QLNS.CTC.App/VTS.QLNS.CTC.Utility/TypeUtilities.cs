using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace VTS.QLNS.CTC.Utility
{
    public static class TypeUtilities
    {
        public static List<T> GetAllPublicConstantValues<T>(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => (T)x.GetRawConstantValue())
                .ToList();
        }

        public static List<T> DataTableToListObject<T>(DataTable dataTable)
        {
            List<T> rs = new List<T>();
            if (dataTable.Rows.Count > 0)
            {
                var serializedMyObjects = JsonConvert.SerializeObject(dataTable);
                // Here you get the object
                rs = (List<T>)JsonConvert.DeserializeObject(serializedMyObjects, typeof(List<T>));
            }
            return rs;
        }

        public static T StringToObJect<T>(string input)
        {
            var result = JsonConvert.DeserializeObject<T>(input);
            return result;
        }
    }
}
