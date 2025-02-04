using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class ChungTuCanCuModel : BindableBase
    {
        public Guid Id { get; set; }
        public string SoChungTu { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public int? LoaiDuToan { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private double _tuChi;
        public double TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        private double _hangNhap;
        public double HangNhap
        {
            get => _hangNhap;
            set => SetProperty(ref _hangNhap, value);
        }

        private double _hangMua;
        public double HangMua
        {
            get => _hangMua;
            set => SetProperty(ref _hangMua, value);
        }

        private double _phanCap;
        public double PhanCap
        {
            get => _phanCap;
            set => SetProperty(ref _phanCap, value);
        }

        private double? _muaHangHienVat;
        public double? MuaHangHienVat
        {
            get => _muaHangHienVat;
            set => SetProperty(ref _muaHangHienVat, value);
        }

        private double? _dacThu;
        public double? DacThu
        {
            get => _dacThu;
            set => SetProperty(ref _dacThu, value);
        }

        public string LoaiDuToanString => VoucherType.BudgetTypeDict.GetValueOrDefault(LoaiDuToan.GetValueOrDefault(-1), string.Empty);

        public string Month { get; set; }

        public string NgayQuyetDinhString => NgayQuyetDinh.HasValue ? NgayQuyetDinh.Value.ToString("dd/MM/yyyy") : "";
        public string NgayChungTuString => NgayChungTu.HasValue ? NgayChungTu.Value.ToString("dd/MM/yyyy") : "";
    }
}
