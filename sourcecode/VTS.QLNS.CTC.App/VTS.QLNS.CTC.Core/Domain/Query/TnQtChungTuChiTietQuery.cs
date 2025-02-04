using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TnQtChungTuChiTietQuery
    {
        public Guid Id { get; set; }
        public Guid? IdChungTu { get; set; }
        public Guid IdMaLoaiHinh { get; set; }
        public Guid? IdMaLoaiHinhCha { get; set; }
        public string Noidung { get; set; }
        public int IThangQuyLoai { get; set; }
        public int? IThangQuy { get; set; }
        public double? TongSoThu { get; set; }
        public bool BLaHangCha { get; set; }
        public int NamNganSach { get; set; }
        public int NguonNganSach { get; set; }
        public int? NamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public string ILoai { get; set; }
        public double? TongSoChiPhi { get; set; }
        public double? QtTongSoQtns { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string IdPhongBan { get; set; }
        public string IdPhongBanDich { get; set; }
        public double QtKhauHaoTscđ { get; set; }
        public double QtTienLuong { get; set; }
        public double QtQtnskhac { get; set; }
        public double ChiPhiKhac { get; set; }
        public double TongnopNsnn { get; set; }
        public double ThueGtgt { get; set; }
        public string GhiChu { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public string IdDonViTao { get; set; }
        public int? IGuiNhan { get; set; }
        public double? ThueTndn { get; set; }
        public double? ThueTndnBqp { get; set; }
        public double? PhiLePhi { get; set; }
        public double? NsnnKhac { get; set; }
        public double? NsnnKhacBqp { get; set; }
        public double? ChenhLech { get; set; }
        public double? PpNopNsqp { get; set; }
        public double? PpBoSungKinhPhi { get; set; }
        public double? PpTrichCacQuy { get; set; }
        public double? PpSoChuaPhanPhoi { get; set; }
        public bool? BThoaiThu { get; set; }
        public string Lns { get; set; }
    }
}
