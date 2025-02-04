using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class StorageTypeEnum
    {
        public enum Type
        {
            FTP_SERVER = 1,
            LOCAL = 2
        }

        public struct TypeValue
        {
            public const string FTP_SERVER = "Ftp";
            public const string LOCAL = "Local";
        }

        public static string ValueOf(Type type)
        {
            switch (type)
            {
                case Type.FTP_SERVER:
                    return TypeValue.FTP_SERVER;
                case Type.LOCAL:
                    return TypeValue.LOCAL;
            }
            return string.Empty;
        }

        public static Type TypeOf(int type)
        {
            switch (type)
            {
                case (int)Type.FTP_SERVER:
                    return Type.FTP_SERVER;
                case (int)Type.LOCAL:
                    return Type.LOCAL;
                default:
                    throw new Exception("Not supported.");
            }
        }
    }
}
