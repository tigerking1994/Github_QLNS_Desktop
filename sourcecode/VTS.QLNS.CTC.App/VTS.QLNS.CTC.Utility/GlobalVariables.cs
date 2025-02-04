using System.Collections.Generic;

namespace VTS.QLNS.CTC.Utility
{
    public class GlobalVariables
    {
        private static readonly object _syncRoot = new object();
        private static Dictionary<string, string> _data;

        public static string GetItemsByTag(string tag)
        {
            lock (_syncRoot)
            {
                if (_data == null) _data = LoadItemsByTag();
                if (_data.ContainsKey(tag))
                {
                    return _data[tag];
                }
                else return "0";
            }
        }

        public static void AddItemsByTag(string tag, string value)
        {
            lock (_syncRoot)
            {
                if (_data == null) _data = LoadItemsByTag();
                if (_data.ContainsKey(tag))
                {
                    _data[tag] = value;
                }
                else
                {
                    _data.Add(tag, value);
                }
            }
        }

        private static Dictionary<string, string> LoadItemsByTag()
        {
            var result = new Dictionary<string, string>();
            return result;
        }
    }
}
