using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlBienDongLuongThangModel : ModelBase
    {
        public int Thang { get; set; }
        public decimal? NamTruoc { get; set; }
        public decimal? NamKeHoach { get; set; }
        public decimal? ChenhLech { get; set; }
    }
}
