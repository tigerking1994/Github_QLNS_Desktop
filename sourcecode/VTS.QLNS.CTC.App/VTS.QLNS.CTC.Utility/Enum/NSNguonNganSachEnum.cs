using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public static class NSNguonNganSachEnum
    {
        public enum Type
        {
            NGAN_SACH_QP = 1,
            NGAN_SACH_NN = 2,
            NGAN_SACH_DP = 3,
            NGAN_SACH_DB = 5
        }

        public struct TypeCode
        {
            public const string NGAN_SACH_QP = "0214";
            public const string NGAN_SACH_NN = "0219";
            public const string NGAN_SACH_DP = "0220";
        }

        public static string GetCode(int type)
        {
            switch (type)
            {
                case (int)Type.NGAN_SACH_QP:
                    return TypeCode.NGAN_SACH_QP;
                case (int)Type.NGAN_SACH_NN:
                    return TypeCode.NGAN_SACH_NN;
                case (int)Type.NGAN_SACH_DP:
                    return TypeCode.NGAN_SACH_DP;
                default:
                    return string.Empty;
            }
        }
    }
}
