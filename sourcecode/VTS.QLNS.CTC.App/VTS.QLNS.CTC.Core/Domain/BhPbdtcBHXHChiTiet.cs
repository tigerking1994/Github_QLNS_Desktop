using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTC_PhanBoDuToanChi_ChiTiet")]
    public partial class BhPbdtcBHXHChiTiet : EntityBase
    {
        [Column("ID")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IID_DTC_PhanBoDuToanChi { get; set; }
        public Guid IID_MucLucNganSach { get; set; }
        public Guid IID_DTC_DuToanChiTrenGiao { get; set; }
        public string SLNS { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string SNoiDung { get; set; }
        public string SXauNoiMa { get; set; }
        public Guid IID_DonVi { get; set; }
        public string IID_MaDonVi { get; set; }
        public Guid IID_LoaiCap { get; set; }
        public Double? FTongTien { get; set; }
        public Double? FTienTuChi { get; set; }
        public Double? FTienHienVat { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public int? INamLamViec { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        [NotMapped]
        public string STenDonVi { get; set; }
        public string SMaLoaiChi { get; set; }
    }
}
