using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDtTmBHYTTNQuery
    {
        [Column("iID_DTTM_BHYT_ThanNhan")]
        public Guid Id { get; set; }
        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }
        [Column("dNgayChungTu")]
        public DateTime? DNgayChungTu { get; set; }
        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }
        [Column("dNgayQuyetDinh")]
        public DateTime? DNgayQuyetDinh { get; set; }
        [Column("iLoaiDuToan")]
        public int? ILoaiDuToan { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("sDSLNS")]
        public string SDSLNS { get; set; }
        [Column("fDuToan")]
        public double? FDuToan { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("bIsKhoa")]
        public bool? BIsKhoa { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }
        [Column("iID_DonVi")]
        public Guid? IID_DonVi { get; set; }
        [Column("dNguoiTao")]
        public DateTime? DNguoiTao { get; set; }
        [Column("dNguoiSua")]
        public DateTime? DNguoiSua { get; set; }
        [Column("sTenDonVi")]
        public string sTenDonVi { get; set; }

        [Column("fSoPhanBo")]
        public Double? FSoPhanBo { get; set; }
        [Column("fDaPhanBo")]
        public Double? FDaPhanBo { get; set; }
        [Column("fSoChuaPhanBo")]
        public Double? FSoChuaPhanBo { get; set; }

    }
}
