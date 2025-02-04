using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class BaoHiemDuToanTypeEnum
    {
        public enum Type
        {
            HANG_CHA = 1,
            SO_CHUA_PHAN_BO = 2,
            HANG_CON = 3
        }

        public enum RowType
        {
            HANG_CHA = 0,
            SO_NHAN_PB = 1,
            SO_CHUA_PHAN_BO = 2,
            HANG_CON = 3
        }

        public enum LoaiChungTu
        {
            DAU_NAM = 1,
            BO_SUNG = 3,
            DIEU_CHINH = 4
        }
    }
}
