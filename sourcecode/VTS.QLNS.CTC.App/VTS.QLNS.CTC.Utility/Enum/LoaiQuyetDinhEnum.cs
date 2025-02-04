using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public static class LoaiQuyetDinhEnum
    {
        public enum Type
        {
            DAM_PHAM = 1,
            CHI_TRONG_NUOC = 2,
            CHI_DOAN_RA = 3
        }

        public struct TypeName
        {
            public const string DAM_PHAM = "Quyết định phê duyệt kết quả đàm phán";
            public const string CHI_TRONG_NUOC = "Quyết định phê duyệt chi trong nước";
            public const string CHI_DOAN_RA = "Quyết định phê duyệt chi đoàn ra";
        }

        public static string Get(int? type)
        {
            switch (type)
            {
                case (int)Type.DAM_PHAM:
                    return TypeName.DAM_PHAM;
                case (int)Type.CHI_TRONG_NUOC:
                    return TypeName.CHI_TRONG_NUOC;
                case (int)Type.CHI_DOAN_RA:
                    return TypeName.CHI_DOAN_RA;
            }
            return string.Empty;
        }

    }
}
