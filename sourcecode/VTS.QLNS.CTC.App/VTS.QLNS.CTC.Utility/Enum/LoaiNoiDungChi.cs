using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class LoaiNoiDungChi
    {
        public enum Type
        {
            CHI_BANG_NGOAI_TE = 1,
            CHI_BANG_NOI_TE = 2,
        }

        public struct TypeName
        {
            public const string CHI_BANG_NGOAI_TE = "Chi bằng ngoại tệ";
            public const string CHI_BANG_NOI_TE = "Chi bằng nội tệ";
        }

        public static string Get(int? type)
        {
            switch (type)
            {
                case (int)Type.CHI_BANG_NGOAI_TE:
                    return TypeName.CHI_BANG_NGOAI_TE;
                case (int)Type.CHI_BANG_NOI_TE:
                    return TypeName.CHI_BANG_NOI_TE;
            }
            return string.Empty;
        }
    }

}
