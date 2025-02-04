using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class NHThanhToanTheo
    {
        public enum Type
        {
            CHI_THEO_HOP_DONG = 1,
            CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG = 2,
            CHI_KHONG_THEO_DU_AN_HOP_DONG = 3
        }
        public struct TypeName
        {
            public const string CHI_THEO_HOP_DONG = "Nội dung chi theo hợp đồng";
            public const string CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG = "Nội dung chi theo dự án, không hình thành hợp đồng";
            public const string CHI_KHONG_THEO_DU_AN_HOP_DONG = "Nội dung chi không theo dự án, không hình thành hợp đồng";
        }

        public static string Get(int? type)
        {
            switch(type)
            {
                case (int)Type.CHI_THEO_HOP_DONG:
                    return TypeName.CHI_THEO_HOP_DONG;
                case (int)Type.CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG:
                    return TypeName.CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG;
                case (int)Type.CHI_KHONG_THEO_DU_AN_HOP_DONG:
                    return TypeName.CHI_KHONG_THEO_DU_AN_HOP_DONG;
            }
            return string.Empty;
        }
    }
}
