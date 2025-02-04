using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcqGiaiThichTroCapQuery
    {
        [Column("IsHangCha")]
        public bool IsHangCha { get; set; }
        [Column("LoaiTC")]
        public string LoaiTC { get; set; }
        [Column("RowNum")]
        public string RowNum { get; set; }
        [Column("STT")]
        public string STT { get; set; }
        [Column("DoiTuong")]
        public string DoiTuong { get; set; }
        [Column("LoaiDoiTuong")]
        public string LoaiDoiTuong { get; set; }
        [Column("sMa_Hieu_Can_Bo")]
        public string SMaHieuCanBo { get; set; }
        [Column("sTenCanBo")]
        public string STenCanBo { get; set; }
        [Column("sTenCapBac")]
        public string STenCapBac { get; set; }
        [Column("sSoSoBHXH")]
        public string SSoSoBHXH { get; set; }
        [Column("fTienLuongThangDongBHXH")]
        public double? FTienLuongThangDongBHXH { get; set; }
        [Column("dTuNgay")]
        public DateTime? DTuNgay { get; set; }
        [Column("dDenNgay")]
        public DateTime? DDenNgay { get; set; }
        [Column("iSoNgayHuong")]
        public int? ISoNgayHuong { get; set; }
        [Column("fSoTien")]
        public double? FSoTien { get; set; }
        [Column("bHasData")]
        public bool? BHasData { get; set; }
        public string STuNgay => DTuNgay?.ToString("dd/MM/yyyy");
        public string SDenNgay => DDenNgay?.ToString("dd/MM/yyyy");

    }
}
