using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTC_PhanBoDuToanChi")]
    public partial class BhPbdtcBHXH : EntityBase
    {
        [Column("ID")]
        [Key]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int INamChungTu { get; set; }
        public int ILoaiDotNhanPhanBo { get; set; }
        public string SMoTa { get; set; }
        public string SLNS { get; set; }
        public Double? FTongTien { get; set; }
        public Double? FTongTienTuChi { get; set; }
        public Double? FTongTienHienVat { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public bool BIsKhoa { get; set; }
        public string SDotNhan { get; set; }
        public string SID_MaDonVi { get; set; }
        public string IID_DotNhan { get; set; }

        public int ILoaiChungTu { get; set; }

        public Guid? IIDLoaiDanhMucChi { get; set; }
        public string SMaLoaiChi { get; set; }
        [NotMapped]
        public string SNgayQuyetDinh => DNgayQuyetDinh?.ToString("dd/MM/yyyy");

    }
}
