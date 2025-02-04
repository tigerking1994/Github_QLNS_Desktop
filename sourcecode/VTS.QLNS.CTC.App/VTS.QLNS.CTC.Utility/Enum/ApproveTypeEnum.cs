using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class ApproveTypeEnum
    {
        public enum Type
        {
            DA_DUYET = 1,
            CHUA_DUYET = 0
        }
        public struct TypeName
        {
            public const string DA_DUYET = "Đã duyệt";
            public const string CHUA_DUYET = "Chưa duyệt";
        }

        public static string GetApproveTypeName(int type)
        {
            switch (type)
            {
                case (int)Type.DA_DUYET:
                    return TypeName.DA_DUYET;
                case (int)Type.CHUA_DUYET:
                    return TypeName.CHUA_DUYET;
            }
            return string.Empty;
        }
    }
}
