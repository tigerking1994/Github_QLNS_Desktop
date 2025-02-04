using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class LevelBuggetModel : BindableBase
    {
        public Guid Id { get; set; }
        public string SoChungTu { get; set; }
        public int? SoChungTuIndex { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public string NgayChungTuString { get; set; }
        //public string SoCongVan { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public string NgayQuyetDinhString { get; set; }
        public string MoTa { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string TenDonViHienThi
        {
            get => string.Format("{0}-{1}", IdDonVi, TenDonVi);
        }
        public string Lns { get; set; }
        public int IType { get; set; }
        public string ITypeMoTa { get; set; }
        public string ILoai { get; set; }
        public string ILoaiMoTa { get; set; }
        public int? NamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }

        private bool _isLocked;
        public bool IsLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }
        public int? NguonNganSach { get; set; }
        public int? NamNganSach { get; set; }
        public string IdDonViTao { get; set; }
        public string IGuiNhan { get; set; }

        public string TenLoai { get; set; }
        public double? SoTuChi { get; set; }
        public double? HangNhap { get; set; }
        public double? HangMua { get; set; }
        public double? ChuaPhanCap { get; set; }

        private double _tongTuChi;
        public double TongTuChi
        {
            get => _tongTuChi;
            set => SetProperty(ref _tongTuChi, value);
        }

        private double _tongPhanCap;
        public double TongPhanCap
        {
            get => _tongPhanCap;
            set => SetProperty(ref _tongPhanCap, value);
        }

        private double _tongSoChuaPhan;
        public double TongSoChuaPhan
        {
            get => _tongSoChuaPhan;
            set => SetProperty(ref _tongSoChuaPhan, value);
        }

        private double _tongChuaPhanCap;
        public double TongChuaPhanCap
        {
            get => _tongChuaPhanCap;
            set => SetProperty(ref _tongChuaPhanCap, value);
        }

        private double _tongHangNhap;
        public double TongHangNhap
        {
            get => _tongHangNhap;
            set => SetProperty(ref _tongHangNhap, value);
        }

        private double _tongHangMua;
        public double TongHangMua
        {
            get => _tongHangMua;
            set => SetProperty(ref _tongHangMua, value);
        }

        private int? _loaiChungTu;
        public int? LoaiChungTu
        {
            get => _loaiChungTu;
            set => SetProperty(ref _loaiChungTu, value);
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
    }
}
