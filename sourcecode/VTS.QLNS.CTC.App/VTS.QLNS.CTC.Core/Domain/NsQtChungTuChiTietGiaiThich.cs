using System;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsQtChungTuChiTietGiaiThich : EntityBase
    {
        [Column("iID_QTCTCTGiaiThich")]
        public override Guid Id { get; set; }
        public Guid IIdQtchungTu { get; set; }
        public string IIdGiaiThich { get; set; }
        public int IThangQuy { get; set; }
        public int IThangQuyLoai { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SMoTaTinhHinh { get; set; }
        public string SMoTaKienNghi { get; set; }
        public string SMoTa { get; set; }
        public double FLuongSiQuan { get; set; }
        public double FLuongSiQuanTru { get; set; }
        public double FLuongSiQuanQt { get; set; }
        public double FLuongQncn { get; set; }
        public double FLuongQncnTru { get; set; }
        public double FLuongQncnQt { get; set; }
        public double FLuongCnvqp { get; set; }
        public double FLuongCnvqpTru { get; set; }
        public double FLuongCnvqpQt { get; set; }
        public double FLuongHd { get; set; }
        public double FLuongHdTru { get; set; }
        public double FLuongHdQt { get; set; }
        public double FPhuCapSiQuan { get; set; }
        public double FPhuCapSiQuanTru { get; set; }
        public double FPhuCapSiQuanQt { get; set; }
        public double FPhuCapQncn { get; set; }
        public double FPhuCapQncnTru { get; set; }
        public double FPhuCapQncnQt { get; set; }
        public double FPhuCapCnvqp { get; set; }
        public double FPhuCapCnvqpTru { get; set; }
        public double FPhuCapCnvqpQt { get; set; }
        public double FPhuCapHd { get; set; }
        public double FPhuCapHdTru { get; set; }
        public double FPhuCapHdQt { get; set; }
        public double FNgayAn { get; set; }
        public double FNgayAnCong { get; set; }
        public double FNgayAnTru { get; set; }
        public double FNgayAnQt { get; set; }
        public double FRaQuanSiQuanNguoiXuatNgu { get; set; }
        public double FRaQuanSiQuanTienXuatNgu { get; set; }
        public double FRaQuanSiQuanNguoiHuu { get; set; }
        public double FRaQuanSiQuanTienHuu { get; set; }
        public double FRaQuanSiQuanNguoiThoiViec { get; set; }
        public double FRaQuanSiQuanTienThoiViec { get; set; }
        public double FRaQuanQncnNguoiXuatNgu { get; set; }
        public double FRaQuanQncnTienXuatNgu { get; set; }
        public double FRaQuanQncnNguoiHuu { get; set; }
        public double FRaQuanQncnTienHuu { get; set; }
        public double FRaQuanQncnNguoiThoiViec { get; set; }
        public double FRaQuanQncnTienThoiViec { get; set; }
        public double FRaQuanCnvqpNguoiXuatNgu { get; set; }
        public double FRaQuanCnvqpTienXuatNgu { get; set; }
        public double FRaQuanCnvqpNguoiHuu { get; set; }
        public double FRaQuanCnvqpTienHuu { get; set; }
        public double FRaQuanCnvqpNguoiThoiViec { get; set; }
        public double FRaQuanCnvqpTienThoiViec { get; set; }
        public double FRaQuanHsqcsNguoiXuatNgu { get; set; }
        public double FRaQuanHsqcsTienXuatNgu { get; set; }
        public double FRaQuanHsqcsNguoiHuu { get; set; }
        public double FRaQuanHsqcsTienHuu { get; set; }
        public double FRaQuanHsqcsNguoiThoiViec { get; set; }
        public double FRaQuanHsqcsTienThoiViec { get; set; }
        public int INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public double FLuongBhxhSiQuanTru { get; set; }
        public double FLuongBhxhQncnTru { get; set; }
        public double FLuongBhxhCnvqpTru { get; set; }
        public double FLuongBhxhHdTru { get; set; }
        public double FPhuCapBhxhSiQuanTru { get; set; }
        public double FPhuCapBhxhQncnTru { get; set; }
        public double FPhuCapBhxhCnvqpTru { get; set; }
        public double FPhuCapBhxhHdTru { get; set; }
        public double FKinhPhiLuongPcKhac { get; set; }
        public double FKinhPhiPhuCapHsqbs { get; set; }
        public double FKinhPhiAn { get; set; }
    }
}
