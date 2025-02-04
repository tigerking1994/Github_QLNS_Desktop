using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhKhtmBHYTChiTietQuery
    {
        [Column("iID_KHTM_BHYT_ChiTiet")]
        [Key]
        public Guid Id { get; set; }
        [Column("iID_KHTM_BHYT")]
        public Guid IdKhtmBHYT { get; set; }
        [Column("iID_NoiDung")]
        public Guid IIDNoiDung { get; set; }
        [Column("sTenNoiDung")]
        public string STenNoiDung { get; set; }
        [Column("iSoNguoi")]
        public int? ISoNguoi { get; set; }
        [Column("iSoThang")]
        public int? ISoThang { get; set; }
        [Column("fDinhMuc")]
        public double? FDinhMuc { get; set; }
        [Column("fThanhTien")]
        public double? FThanhTien { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("sLNS")]
        public string SLNS { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        public string Stt { get; set; }
        public string SMoTa { get; set; }
        public string SM { get; set; }
        public Guid? IdParent { get; set; }
        public bool IsAdd { get; set; }
        public bool IsAuToFillTuChi { get; set; }
        public bool IsHangCha { get; set; }
        public bool? BHangCha { get; set; }
        public decimal? DHeSoLCS { get; set; }
        public decimal? DHeSoBHYT { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        public int Type { get; set;}
    }
}
