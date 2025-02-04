using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcqCtctGtTroCapQuery
    {
        [Column("iiD_QTC_Quy_CTCT_GiaiThichTroCap")]
        public Guid Id { get; set; }
        public Guid IID_QTC_Quy_ChungTu { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public int IQuy { get; set; }
        public int INamLamViec { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string SMaHieuCanBo { get; set; }
        public string STenCanBo { get; set; }
        public string SMaCapBac { get; set; }
        public string ID_MaPhanHo { get; set; }
        public int? ISoNgayHuong { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public double? FSoTien { get; set; }
        public string SXauNoiMa { get; set; }
        public string STenPhanHo { get; set; }
        public string ID_MaDonVi { get; set; }
        public string STenCapBac { get; set; }
        public string SSoSoBHXH { get; set; }
        public DateTime? DTuNgay { get; set; }
        public DateTime? DDenNgay { get; set; }
        public double? FTienLuongThangDongBHXH { get; set; }
        public int? ISoNgayTruyLinh { get; set; }
        public Double? FTienTruyLinh { get; set; }
    }
}
