using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class KeHoachVonModel : BindableBase
    {
        private Guid _id;
        public Guid Id 
        { 
            get => _id; 
            set => SetProperty(ref _id, value); 
        }

        private string _sSoQuyetDinh;
        public string SSoQuyetDinh 
        { 
            get => _sSoQuyetDinh; 
            set => SetProperty(ref _sSoQuyetDinh, value); 
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh 
        { 
            get => _dNgayQuyetDinh; 
            set => SetProperty(ref _dNgayQuyetDinh, value); 
        }

        private int _iNamKeHoach;
        public int INamKeHoach 
        { 
            get => _iNamKeHoach; 
            set => SetProperty(ref _iNamKeHoach, value); 
        }

        private Guid? _iID_DonViQuanLyID;
        public Guid? IIDDonViQuanLyID 
        { 
            get => _iID_DonViQuanLyID; 
            set => SetProperty(ref _iID_DonViQuanLyID, value); 
        }

        private string _iID_MaDonViQuanLy;
        public string IIDMaDonViQuanLy 
        { 
            get => _iID_MaDonViQuanLy; 
            set => SetProperty(ref _iID_MaDonViQuanLy, value); 
        }

        private int _iID_NguonVonID;
        public int IIDNguonVonID 
        { 
            get => _iID_NguonVonID; 
            set => SetProperty(ref _iID_NguonVonID, value); 
        }

        private int _phanLoai;
        public int PhanLoai 
        { 
            get => _phanLoai; 
            set => SetProperty(ref _phanLoai, value); 
        }

        //private double? _lenhChi;
        //public double? LenhChi 
        //{ 
        //    get => _lenhChi; 
        //    set => SetProperty(ref _lenhChi, value); 
        //}

        public double? FLuyKeThanhToan { get; set; }

        private double? _fTongGiaTri;
        public double? FTongGiaTri
        {
            get => _fTongGiaTri;
            set => SetProperty(ref _fTongGiaTri, value);
        }

        private string _tenLoai;
        public string TenLoai 
        { 
            get => _tenLoai; 
            set => SetProperty(ref _tenLoai, value); 
        }

        private string _sMaNguonCha;
        public string SMaNguonCha 
        { 
            get => _sMaNguonCha; 
            set => SetProperty(ref _sMaNguonCha, value); 
        }

        private bool _isChecked;
        public bool IsChecked 
        { 
            get => _isChecked; 
            set => SetProperty(ref _isChecked, value); 
        }

        private string _ngayQuyetDinhDisplay;
        public string NgayQuyetDinhDisplay
        {
            get => _ngayQuyetDinhDisplay;
            set => SetProperty(ref _ngayQuyetDinhDisplay, value);
        }

        private string _sSoKeHoachDropDown;
        public string SSoKeHoachDropDown
        {
            get => _sSoKeHoachDropDown;
            set => SetProperty(ref _sSoKeHoachDropDown, value);
        }
    }
}
