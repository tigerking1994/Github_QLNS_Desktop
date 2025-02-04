using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TnQtChungTuModel : BindableBase
    {
        public Guid Id { get; set; }
        public string SoChungTu { get; set; }
        public int? SoChungTuIndex { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public string MoTa { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string IdPhongBan { get; set; }
        public string Noidung { get; set; }
        public int IThangQuyLoai { get; set; }
        public int MoTaChiTiet { get; set; }
        public int NamNganSach { get; set; }
        public int NguonNganSach { get; set; }
        public int? NamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public double TongSoThuSum { get; set; }
        public double TongSoChiPhiSum { get; set; }

        private bool _isLocked;
        public bool IsLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }
        public string IdDonViTao { get; set; }

        private int? _iThangQuy;
        public int? IThangQuy
        {
            get => _iThangQuy;
            set => SetProperty(ref _iThangQuy, value);
        }

        public string Lns { get; set; }

        private double _totalTongSoThu;
        public double TotalTongSoThu
        {
            get => _totalTongSoThu;
            set => SetProperty(ref _totalTongSoThu, value);
        }

        private double _totalChiPhi;
        public double TotalChiPhi
        {
            get => _totalChiPhi;
            set => SetProperty(ref _totalChiPhi, value);
        }

        private double _totalSoQtns;
        public double TotalSoQtns
        {
            get => _totalSoQtns;
            set => SetProperty(ref _totalSoQtns, value);
        }

        private double _totalKhauHaoTscđ;
        public double TotalKhauHaoTscđ
        {
            get => _totalKhauHaoTscđ;
            set => SetProperty(ref _totalKhauHaoTscđ, value);
        }

        private double _totalTienLuong;
        public double TotalTienLuong
        {
            get => _totalTienLuong;
            set => SetProperty(ref _totalTienLuong, value);
        }

        private double _totalQtnsKhac;
        public double TotalQtnsKhac
        {
            get => _totalQtnsKhac;
            set => SetProperty(ref _totalQtnsKhac, value);
        }

        private double _totalChiPhiKhac;
        public double TotalChiPhiKhac
        {
            get => _totalChiPhiKhac;
            set => SetProperty(ref _totalChiPhiKhac, value);
        }

        private double _totalNopNsnn;
        public double TotalNopNsnn
        {
            get => _totalNopNsnn;
            set => SetProperty(ref _totalNopNsnn, value);
        }

        private double _totalThueGtgt;
        public double TotalThueGtgt
        {
            get => _totalThueGtgt;
            set => SetProperty(ref _totalThueGtgt, value);
        }

        private double _totalThueTndn;
        public double TotalThueTndn
        {
            get => _totalThueTndn;
            set => SetProperty(ref _totalThueTndn, value);
        }

        private double _totalPhiLePhi;
        public double TotalPhiLePhi
        {
            get => _totalChiPhi;
            set => SetProperty(ref _totalChiPhi, value);
        }

        private double _totalNsnnKhac;
        public double TotalNsnnKhac
        {
            get => _totalNsnnKhac;
            set => SetProperty(ref _totalNsnnKhac, value);
        }

        private double _totalNsnnKhacBqp;
        public double TotalNsnnKhacBqp
        {
            get => _totalNsnnKhacBqp;
            set => SetProperty(ref _totalNsnnKhacBqp, value);
        }

        private double _totalChenhLech;
        public double TotalChenhLech
        {
            get => _totalChenhLech;
            set => SetProperty(ref _totalChenhLech, value);
        }

        private double _totalNopNSQP;
        public double TotalNopNSQP
        {
            get => _totalNopNSQP;
            set => SetProperty(ref _totalNopNSQP, value);
        }

        private double _totalBoSungKinhPhi;
        public double TotalBoSungKinhPhi
        {
            get => _totalBoSungKinhPhi;
            set => SetProperty(ref _totalBoSungKinhPhi, value);
        }

        private double _totalTrichCacQuy;
        public double TotalTrichCacQuy
        {
            get => _totalTrichCacQuy;
            set => SetProperty(ref _totalTrichCacQuy, value);
        }

        private double _totalChuaPhanPhoi;
        public double TotalChuaPhanPhoi
        {
            get => _totalChuaPhanPhoi;
            set => SetProperty(ref _totalChuaPhanPhoi, value);
        }

        public int? IKiemDuyet { get; set; }
        public string ITongHop { get; set; }
        public string IThangQuyMoTa { get; set; }

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
    }
}
