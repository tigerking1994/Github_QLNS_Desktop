using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTC_DieuChinhDuToanChi")]
    public class BhDtcDcdToanChi : EntityBase
    {
        [Column("iID_BH_DTC")]
        [Key]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? INamLamViec { get; set; }
        [Column("iID_DonVi")]
        public Guid? IIdDonViId { get; set; }
        public string IID_MaDonVi { get; set; }
        public string SMoTa { get; set; }
        public string SLNS { get; set; }
        public Guid IID_LoaiCap { get; set; }
        public string SMaLoaiChi { get;set; }
        public double? FTienDuToanDuocGiao { get; set; }
        public double? FTienThucHien06ThangDauNam { get; set; }
        public double? FTienUocThucHien06ThangCuoiNam { get; set; }
        public double? FTienUocThucHienCaNam { get; set; }
        public double? FTienSoSanhTang { get; set; }
        public double? FTienSoSanhGiam { get; set; }
        public string STongHop { get; set; }
        public Guid? IID_TongHopID { get; set; }
        public int ILoaiTongHop { get; set; }
        public bool BDaTongHop { get;set; }
        public bool BIsKhoa { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
    }
}
