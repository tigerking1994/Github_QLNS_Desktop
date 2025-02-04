using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtTtPheDuyetThanhToanQuery
    {
        public Guid Id { get; set; }
        public Guid iID_DeNghiThanhToan { get; set; }
        public string sSoDeNghi { get; set; }
        public string sSoQuyetDinh { get; set; }
        public DateTime? dNgayQuyetDinh { get; set; }
        public Guid? iID_DonViQuanLyID { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public string sTenDonVi { get; set; }
        public Guid? iID_NhomQuanLyID { get; set; }
        public string sNguoiLap { get; set; }
        public string SGhiChu { get; set; }
    }
}
