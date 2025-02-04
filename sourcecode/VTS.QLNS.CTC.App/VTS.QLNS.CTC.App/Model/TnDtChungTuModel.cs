using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TnDtChungTuModel : BindableBase
    {
        public Guid Id { get; set; }
        public string SoChungTu { get; set; }
        public int? SoChungTuIndex { get; set; }

        private DateTime? _ngayChungTu;
        public DateTime? NgayChungTu
        {
            get => _ngayChungTu;
            set => SetProperty(ref _ngayChungTu, value);
        }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string Lns { get; set; }
        public int ILoai { get; set; }
        public int? NamNganSach { get; set; }
        public int? NguonNganSach { get; set; }
        public int? LoaiNganSach { get; set; }
        public int? NamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }

        private double _tuChiSum;
        public double TuChiSum
        {
            get => _tuChiSum;
            set => SetProperty(ref _tuChiSum, value);
        }

        private bool _isLocked;
        public bool IsLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }

        public string MoTaChiTiet { get; set; }
        public int IDot { get; set; }
        public string IdDonViTao { get; set; }
        public int? IGuiNhan { get; set; }
        public string IdDotNhan { get; set; }
        public int? LoaiChungTu { get; set; }
        public string SoLieuNhap { get; set; }
        public int? IKiemDuyet { get; set; }
        public string ITongHop { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private bool _isFilter;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }

        public string TenDotNhan => $"{SoQuyetDinh} - {NgayQuyetDinh.Value:dd/MM/yyyy} ({SoChungTu})";
    }
}
