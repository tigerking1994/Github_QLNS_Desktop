using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Criteria
{
    public class ProjectAdjustementSearch
    {
        public int NguonVonId { get; set; }
        public int NamKeHoach { get; set; }
        public Guid? LoaiNguonVon { get; set; }
        public string UserName { get; set; }
        //public bool IsAdmin { get; set; }
    }
}
