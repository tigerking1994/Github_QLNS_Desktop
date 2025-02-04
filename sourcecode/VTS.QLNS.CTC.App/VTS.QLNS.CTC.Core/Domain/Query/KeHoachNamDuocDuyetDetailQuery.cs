using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class KeHoachNamDuocDuyetDetailQuery
    {
        public Guid iID_DuAnID { get; set; }
        public string sTenDuAn { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string sGhiChu { get; set; }
        public double? fCapPhatTaiKhoBac { get; set; }
        public double? fCapPhatBangLenhChi { get; set; }
        public Guid? iID_LoaiCongTrinh { get; set; }
        public string sTenLoaiCongTrinh { get; set; }
    }
}
