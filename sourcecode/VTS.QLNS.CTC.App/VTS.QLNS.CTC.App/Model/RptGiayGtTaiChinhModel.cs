using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class RptGiayGtTaiChinhModel : ModelBase
    {
        public string sMaCanBo { get; set; }
        public string sTenCanBo { get; set; }
        public int iThang { get; set; }
        public int iNam { get; set; }
        public string sTenCv { get; set; }
        public DateTime? sNgayNn { get; set; }
        public DateTime? sNgayXn { get; set; }
        public DateTime? sNgayTn { get; set; }
        public string sSoSoLuong { get; set; }
        public string sNoiChuyenDen { get; set; }
        public string sSoQd { get; set; }
        public DateTime? sNgayKyQd { get; set; }
        public int iCapPhatTiepThang { get; set; }
        public int iCapPhatTiepNam { get; set; }
        public int? fLoPhiDuocCap { get; set; }
        public int? fLoPhiThanhToan { get; set; }
        public DateTime? sNgayKy { get; set; }
        public string sTenCapBac { get; set; }
        public decimal? fLhtTt { get; set; }
        public decimal? fPccvTt { get; set; }
        public decimal? fPctnTt { get; set; }
        public decimal? fPcTraSum { get; set; }
        public decimal? fPcDacThuSum { get; set; }
        public decimal? fPcKhacSum { get; set; }
        public decimal? fPcThuhutTt { get; set; }
        public decimal? fTaDg { get; set; }
        public decimal? fPctnvkTt { get; set; }
        public decimal? fPckvTt { get; set; }
        public decimal? fPcthdTt { get; set; }
        public decimal? fPccovTt { get; set; }
        public decimal? fBuGiaGao { get; set; }
        public string sSoTaiKhoan { get; set; }
        public string sNganHang { get; set; }
    }
}
