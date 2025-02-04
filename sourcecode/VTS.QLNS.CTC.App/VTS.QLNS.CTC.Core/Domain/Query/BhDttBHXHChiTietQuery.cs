using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDttBHXHChiTietQuery
    {
        [Column("iID_DTT_BHXH_ChiTiet")]
        [Key]
        public Guid Id { get; set; }
        [Column("iID_DTT_BHXH")]
        public Guid DttBHXHId { get; set; }
        [Column("iID_LoaiDoiTuong")]
        public Guid? IIDLoaiDoiTuong { get; set; }
        [Column("sLoaiDoiTuong")]
        public string SLoaiDoiTuong { get; set; }
        [Column("fThu_BHXH_NLD")]
        public double? FThuBHXHNguoiLaoDong { get; set; }
        [Column("fThu_BHXH_NSD")]
        public double? FThuBHXHNguoiSuDungLaoDong { get; set; }
        [Column("fTongThuBHXH")]
        public double? FTongThuBHXH { get; set; }
        [Column("fThu_BHYT_NLD")]
        public double? FThuBHYTNguoiLaoDong { get; set; }
        [Column("fThu_BHYTN_NSD")]
        public double? FThuBHYTNguoiSuDungLaoDong { get; set; }
        [Column("fTongThuBHYT")]
        public double? FTongThuBHYT { get; set; }
        [Column("fThu_BHTN_NLD")]
        public double? FThuBHTNNguoiLaoDong { get; set; }
        [Column("fThu_BHTN_NSD")]
        public double? FThuBHTNNguoiSuDungLaoDong { get; set; }
        [Column("fTongThuBHTN")]
        public double? FTongThuBHTN { get; set; }
        [Column("fTongCong")]
        public double? FTongCong { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("iID_MLNS")]
        public Guid? IIdMlns { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? IIdMlnsCha { get; set; }
        [Column("sLNS")]
        public string SLns { get; set; }
        [Column("sL")]
        public string SL { get; set; }
        [Column("sK")]
        public string SK { get; set; }
        [Column("sM")]
        public string SM { get; set; }
        [Column("sTM")]
        public string STm { get; set; }
        [Column("sTTM")]
        public string STtm { get; set; }
        [Column("sNG")]
        public string SNg { get; set; }
        [Column("sTNG")]
        public string STng { get; set; }
        [Column("sTNG1")]
        public string STng1 { get; set; }
        [Column("sTNG2")]
        public string STng2 { get; set; }
        [Column("sTNG3")]
        public string STng3 { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        public int INamLamViec { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }
        public Guid? IdParent { get; set; }
        public bool IsAdd { get; set; }
        public bool IsAuToFillTuChi { get; set; }
        public string STenBhMLNS { get; set; }
        public bool? IsHangCha { get; set; }
    }
}
