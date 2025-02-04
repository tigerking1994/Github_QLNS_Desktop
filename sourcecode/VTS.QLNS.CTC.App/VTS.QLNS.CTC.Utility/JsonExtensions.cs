using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace VTS.QLNS.CTC.Utility
{
    public static class JsonExtensions
    {
        #region Read Json
        public static List<T> ConvertJsonToList<T>(string sJson) where T : class
        {
            JsonTextReader rd = new JsonTextReader(new StringReader(sJson));
            List<JsonData> arr = new List<JsonData>();
            List<T> results = new List<T>();
            while (rd.Read())
            {
                arr.Add(new JsonData() { Token = rd.TokenType, Value = rd.Value });
            }
            if (!arr.Any()) return results;
            results = GetDataJson<T>(arr);
            return results;
        }

        private static List<T> GetDataJson<T>(List<JsonData> arr)
        {
            List<T> results = new List<T>();
            T obj = (T)Activator.CreateInstance(typeof(T));
            int i = -1;
            int iTotal = arr.Count;
            bool isInArr = false;
            while (i < (iTotal - 1))
            {
                i++;
                if (arr[i].Token == JsonToken.PropertyName && Convert.ToString(arr[i].Value) == "Entity")
                {
                    i++;
                    results.Add((T)GetObject(ref i, iTotal, typeof(T), arr, ref isInArr));
                }
            }
            return results;
        }

        private static object GetObject(ref int i, int iTotal, Type type, List<JsonData> arr, ref bool isInArr)
        {
            Dictionary<string, string> dicObj = new Dictionary<string, string>();
            var obj = Activator.CreateInstance(type);
            object objArr = null;
            string propName = string.Empty;
            Type typeArr = null;
            PropertyInfo propArr = null;
            List<string> lstTypeExclude = GetListTypeNameIsList(type);
            while (i <= iTotal)
            {
                switch (arr[i].Token)
                {
                    case JsonToken.StartObject:
                        obj = Activator.CreateInstance(type);
                        break;
                    case JsonToken.EndObject:
                        obj.SetValueToObject(type, dicObj);
                        return obj;
                    case JsonToken.StartArray:
                        typeArr = GetTypeArray(Convert.ToString(arr[i - 1].Value), type.GetProperties(), ref propArr);
                        if (typeArr != null)
                        {
                            ++i;
                            objArr = GetList(ref i, iTotal, typeArr, arr);
                            propArr.SetValue(obj, objArr, null);
                            ++i;
                            continue;
                        }
                        break;
                    case JsonToken.EndArray:
                        isInArr = false;
                        return null;
                    default:
                        if (arr[i].Token == JsonToken.PropertyName)
                        {
                            propName = Convert.ToString(arr[i].Value);
                            if (lstTypeExclude.IndexOf(propName) != -1) { i++; continue; }
                            dicObj.Add(Convert.ToString(arr[i].Value), null);
                        }
                        else
                        {
                            dicObj[propName] = Convert.ToString(arr[i].Value);
                        }
                        break;
                }
                ++i;
            }
            return obj;
        }

        private static object GetList(ref int i, int iTotal, Type typeArr, List<JsonData> arr)
        {
            IList result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(typeArr));
            bool bIsList = true;
            while (bIsList)
            {
                var obj = GetObject(ref i, iTotal, typeArr, arr, ref bIsList);
                if (obj != null)
                {
                    result.Add(obj);
                }
                else
                {
                    break;
                }
                ++i;
            }

            return result;
        }

        private static object SetValueToObject(this object obj, Type type, Dictionary<string, string> dicObj)
        {
            PropertyInfo[] props = type.GetProperties();
            foreach (var prop in props)
            {
                try
                {
                    Type t = prop.PropertyType;
                    if (!dicObj.ContainsKey(prop.Name)) continue;
                    prop.SetValue(obj, ConvertStringToType(dicObj[prop.Name], t), null);
                }
                catch (Exception)
                {

                }
            }
            return obj;
        }

        private static Type GetTypeArray(string typeName, PropertyInfo[] propParent, ref PropertyInfo propCurrentList)
        {
            if (string.IsNullOrEmpty(typeName)) return null;
            foreach (var prop in propParent)
            {
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    if (prop.CustomAttributes.Any(x => x.AttributeType.Name == "CustomNameJsonAttribute"))
                    {
                        var customName = prop.GetCustomAttributes(true).FirstOrDefault(x => x.GetType().Equals(typeof(CustomNameJsonAttribute)));
                        if (customName is CustomNameJsonAttribute custom && custom.Name == typeName)
                        {
                            propCurrentList = prop;
                            return prop.PropertyType.GetGenericArguments().Single();
                        }
                    }
                    else if (prop.PropertyType.GetGenericArguments().Single().Name == typeName)
                    {
                        propCurrentList = prop;
                        return prop.PropertyType.GetGenericArguments().Single();
                    } 
                }
            }
            return null;
        }

        private static List<string> GetListTypeNameIsList(Type type)
        {
            List<string> lst = new List<string>();
            foreach (var prop in type.GetProperties())
            {
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    lst.Add(prop.PropertyType.GetGenericArguments().Single().Name);
            }
            return lst;
        }
        #endregion

        #region Write Json
        public static StringBuilder ConvertListToJson<T>(List<T> data) where T : class
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            var props = typeof(T).GetProperties();
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartArray();
                foreach (var item in data)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("Entity");
                    writer.WriteObject<T>(item, props);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
            return sb;
        }

        private static JsonWriter WriteObject<T>(this JsonWriter writer, T obj, PropertyInfo[] props)
        {
            writer.WriteStartObject();
            foreach (var prop in props)
            {
                Type t = prop.PropertyType;
                var value = prop.GetValue(obj);
                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>))
                {
                    if (value == null) continue;
                    //bIsHaveChild = true;
                    //propChild = prop;
                    Type tChild = prop.PropertyType.GetGenericArguments().Single();
                    if (prop.CustomAttributes.Any(x => x.AttributeType.Name == "CustomNameJsonAttribute"))
                    {
                        var customName = prop.GetCustomAttributes(true).FirstOrDefault(x => x.GetType().Equals(typeof(CustomNameJsonAttribute)));
                        if (customName is CustomNameJsonAttribute custom)
                        {
                            writer.WritePropertyName(custom.Name);
                        }
                    }
                    else
                    {
                        writer.WritePropertyName(tChild.Name);
                    }
                    writer.WriteStartArray();
                    foreach (var child in value as IEnumerable)
                    {
                        writer.WriteObject(child, tChild.GetProperties());
                    }
                    writer.WriteEndArray();
                }
                else
                {
                    writer.WritePropertyName(prop.Name);
                    writer.WriteValue(value);
                }
            }
            writer.WriteEndObject();
            return writer;
        }
        #endregion

        private static object ConvertStringToType(string value, Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType == null)
            {
                switch (type.Name)
                {
                    case "DateTime":
                        return Convert.ChangeType(value, type, CultureInfo.CurrentUICulture.DateTimeFormat);
                    case "Guid":
                        return Convert.ChangeType(Guid.Parse(value), type, CultureInfo.CurrentUICulture);
                    default:
                        return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
                }
            }
            switch (underlyingType.Name)
            {
                case "DateTime":
                    return string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, underlyingType, CultureInfo.CurrentUICulture.DateTimeFormat);
                case "Guid":
                    return string.IsNullOrEmpty(value) ? null : Convert.ChangeType(Guid.Parse(value), underlyingType, CultureInfo.CurrentUICulture.DateTimeFormat);
                default:
                    return string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
            }
        }
    }

    public class JsonData
    {
        public JsonToken Token { get; set; }
        public object Value { get; set; }
    }
}
