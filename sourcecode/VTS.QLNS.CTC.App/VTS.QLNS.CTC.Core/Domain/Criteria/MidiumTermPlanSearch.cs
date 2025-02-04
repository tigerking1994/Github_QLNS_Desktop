using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class MidiumTermPlanSearch
    {
        public DateTime DNgayLap { get; set; }
        public Guid IDKeHoach5NamID { get; set; }
        public string IDMaDonViQuanLy { get; set; }
        public int IGiaiDoanTu { get; set; }
        public int IGiaiDoanDen { get; set; }
        public int Type { get; set; }
        public int YearOfWork { get; set; }
    }
}
