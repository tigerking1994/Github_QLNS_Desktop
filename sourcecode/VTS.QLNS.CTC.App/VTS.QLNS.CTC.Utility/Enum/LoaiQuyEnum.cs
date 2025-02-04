using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public static class LoaiQuyEnum
    {
        public enum Type
        {
            QUY_1 = 1,
            QUY_2 = 2,
            QUY_3 = 3,
            QUY_4 = 4
        }

        public struct TypeName
        {
            public const string QUY_1 = "Quý I";
            public const string QUY_2 = "Quý II";
            public const string QUY_3 = "Quý III";
            public const string QUY_4 = "Quý IV";
        }

        public static string Get(int? type, bool useQuyStr = true)
        {
            switch (type)
            {
                case (int)Type.QUY_1:
                    return useQuyStr ? TypeName.QUY_1 : TypeName.QUY_1.Substring(4);
                case (int)Type.QUY_2:
                    return useQuyStr? TypeName.QUY_2 : TypeName.QUY_2.Substring(4);
                case (int)Type.QUY_3:
                    return useQuyStr? TypeName.QUY_3 : TypeName.QUY_3.Substring(4);
                case (int)Type.QUY_4:
                    return useQuyStr? TypeName.QUY_4: TypeName.QUY_4.Substring(4);
            }
            return string.Empty;
        }
    }
}
