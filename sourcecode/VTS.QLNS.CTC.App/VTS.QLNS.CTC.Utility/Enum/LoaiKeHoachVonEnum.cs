using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class LoaiKeHoachVonEnum
    {
        public enum Type
        {
            KE_HOACH_VON_NAM_NAY = 1,
            KE_HOACH_VON_UNG_NAM_NAY = 2,
            KE_HOACH_VON_NAM_TRUOC_CHUYEN_SANG = 3,
            KE_HOACH_VON_UNG_NAM_TRUOC_CHUYEN_SANG = 4
        }

        public struct TypeName
        {
            public const string KE_HOACH_VON_NAM_NAY = "Kế hoạch vốn năm nay";
            public const string KE_HOACH_VON_UNG_NAM_NAY = "Kế hoạch vốn ứng năm nay";
            public const string KE_HOACH_VON_NAM_TRUOC_CHUYEN_SANG = "Kế hoạch vốn năm trước chuyển sang";
            public const string KE_HOACH_VON_UNG_NAM_TRUOC_CHUYEN_SANG = "Kế hoạch vốn ứng năm trước chuyển sang";
        }
    }
}
