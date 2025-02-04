using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class SettlementVoucherDetailExplainSubtractModel : BindableBase
    {
        private bool _isModified;
        public bool IsModified
        {
            get => _isModified;
            set => SetProperty(ref _isModified, value);
        }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }
        public Guid Id { get; set; }
        public Guid CustomId { get; set; }
        public Guid IIdQtchungTu { get; set; }
        public string IIdGiaiThich { get; set; }
        public int IThangQuy { get; set; }
        public int IThangQuyLoai { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SMoTa { get; set; }

        private string _iIdDoiTuong;
        public string IIdDoiTuong
        {
            get => _iIdDoiTuong;
            set => SetProperty(ref _iIdDoiTuong, value);
        }

        private string _sHoTen;
        public string SHoTen
        {
            get => _sHoTen;
            set => SetProperty(ref _sHoTen, value);
        }

        public double FLuongThang
        {
            get => _fLuongCapBac + _fLuongThamNien + _fLuongPhuCapCongVu + _fLuongPhuCapKhacBh + _fLuongPhuCapKhac;
        }

        private double _fNgayNghi;
        public double FNgayNghi
        {
            get => _fNgayNghi;
            set => SetProperty(ref _fNgayNghi, value);
        }

        private double _fSoNguoi;
        public double FSoNguoi
        {
            get => _fSoNguoi;
            set => SetProperty(ref _fSoNguoi, value);
        }

        private double _fLuongCapBac;
        public double FLuongCapBac
        {
            get => _fLuongCapBac;
            set
            {
                SetProperty(ref _fLuongCapBac, value);
                OnPropertyChanged(nameof(FLuongThang));
                OnPropertyChanged(nameof(FTongBaoHiem));
            }
        }

        private double _fLuongThamNien;
        public double FLuongThamNien
        {
            get => _fLuongThamNien;
            set
            {
                SetProperty(ref _fLuongThamNien, value);
                OnPropertyChanged(nameof(FLuongThang));
                OnPropertyChanged(nameof(FTongBaoHiem));
            }
        }

        private double _fLuongPhuCapCongVu;
        public double FLuongPhuCapCongVu
        {
            get => _fLuongPhuCapCongVu;
            set
            {
                SetProperty(ref _fLuongPhuCapCongVu, value);
                OnPropertyChanged(nameof(FLuongThang));
                OnPropertyChanged(nameof(FTongBaoHiem));
            }
        }

        private double _fLuongPhuCapKhacBh;
        public double FLuongPhuCapKhacBh
        {
            get => _fLuongPhuCapKhacBh;
            set
            {
                SetProperty(ref _fLuongPhuCapKhacBh, value);
                OnPropertyChanged(nameof(FLuongThang));
                OnPropertyChanged(nameof(FTongBaoHiem));
            }
        }

        private double _fLuongPhuCapKhac;
        public double FLuongPhuCapKhac
        {
            get => _fLuongPhuCapKhac;
            set
            {
                SetProperty(ref _fLuongPhuCapKhac, value);
                OnPropertyChanged(nameof(FLuongThang));
            }
        }

        public double FTongBaoHiem
        {
            get => _fLuongCapBac + _fLuongThamNien + _fLuongPhuCapCongVu + _fLuongPhuCapKhacBh;
        }

        public int INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public int IStatus { get; set; }
        public bool HasData => FLuongThang != 0 || FTongBaoHiem != 0 || !string.IsNullOrEmpty(IIdDoiTuong) || !string.IsNullOrEmpty(SHoTen);

        public SettlementVoucherDetailExplainSubtractModel() {
            CustomId = Guid.NewGuid();
        }

        public SettlementVoucherDetailExplainSubtractModel(string idDonVi)
        {
            CustomId = Guid.NewGuid();
            IIdMaDonVi = idDonVi;
        }
    }
}
