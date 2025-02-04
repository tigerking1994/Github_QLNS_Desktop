using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class RptDieuChinhKeHoachQuery
    {
        public string sXauNoiChuoi { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public Guid? iID_LoaiCongTrinhID { get; set; }
        public Guid? iID_CapPheDuyetID { get; set; }
        public Guid? iID_DuAnID { get; set; }
        public string sTenDuAn { get; set; }
        public string sSoQuyetDinh { get; set; }
        public DateTime? dNgayQuyetDinh { get; set; }
        public string LNS { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public double? fTang { get; set; }
        public double? fGiam { get; set; }
        public double? fChiTieuDauNam { get; set; }
        public string sTenDonVi { get; set; }
        public string sTenLoaiCongTrinh { get; set; }
        public string sTenPhanCap { get; set; }

    }
}
