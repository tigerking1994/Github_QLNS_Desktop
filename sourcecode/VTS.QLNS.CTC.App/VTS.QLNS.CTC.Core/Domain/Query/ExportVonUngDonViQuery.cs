using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ExportVonUngDonViQuery
    {
        public Guid? IIDDuAnID { get; set; }
        public Guid? iID_KeHoachUngID { get; set; }
        public string sTenDuAn { get; set; }
        public string sMaDuAn { get; set; }
        public double? FGiaTriDeNghi { get; set; }
        public double? fTongMucDauTuPheDuyet { get; set; }
        public string SGhiChu { get; set; }
        public string IIDMaDonViQuanLy { get; set; }
        public double? fCapPhatBangLenhChi { get; set; }
        public double? fCapPhatTaiKhoBac { get; set; }
        public string sLNS { get; set; }
        public string sL { get; set; }
        public string sK { get; set; }
        public string sM { get; set; }
        public string sTM { get; set; }
        public string sTTM { get; set; }
        public string sNG { get; set; }
        public string sTenDonVi { get; set; }

        [NotMapped]
        public string iStt { get; set; }

        [NotMapped]
        public bool IsHangCha { get; set; }

    }
}
