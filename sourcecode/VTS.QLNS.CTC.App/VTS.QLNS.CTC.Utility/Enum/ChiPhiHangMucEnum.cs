using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public static class ChiPhiHangMucEnum
    {
        public enum Type
        {
            CHI_PHI = 1,
            HANG_MUC = 2,
            NGUON_VON = 3
        }

        public struct TypeName
        {
            public const string CHI_PHI = "Chi phí";
            public const string HANG_MUC = "Hạng mục";
            public const string NGUON_VON = "Nguồn vốn";
        }

        public static string Get(int type)
        {
            switch (type)
            {
                case (int)Type.CHI_PHI:
                    return TypeName.CHI_PHI;
                case (int)Type.HANG_MUC:
                    return TypeName.HANG_MUC;
                case (int)Type.NGUON_VON:
                    return TypeName.NGUON_VON;
            }
            return string.Empty;
        }
    }
}
