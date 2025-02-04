using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtNcNhuCauChiModel : BindableBase
    {
        public int iRowIndex { get; set; }
        public Guid Id { get; set; }

        private string _sSoDeNghi;
        public string sSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        private DateTime? _dNgayDeNghi;
        public DateTime? dNgayDeNghi
        {
            get => _dNgayDeNghi;
            set => SetProperty(ref _dNgayDeNghi, value);
        }

        private Guid? _iID_DonViQuanLyID;
        public Guid? iID_DonViQuanLyID
        {
            get => _iID_DonViQuanLyID;
            set => SetProperty(ref _iID_DonViQuanLyID, value);
        }

        private string _iID_MaDonViQuanLy;
        public string iID_MaDonViQuanLy
        {
            get => _iID_MaDonViQuanLy;
            set => SetProperty(ref _iID_MaDonViQuanLy, value);
        }

        private int? _iNamKeHoach;
        public int? iNamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }

        private int? _iID_NguonVonID;
        public int? iID_NguonVonID
        {
            get => _iID_NguonVonID;
            set => SetProperty(ref _iID_NguonVonID, value);
        }

        public int _iQuy;
        public int iQuy
        {
            get => _iQuy;
            set => SetProperty(ref _iQuy, value);
        }

        private string _sNguoiLap;
        public string sNguoiLap
        {
            get => _sNguoiLap;
            set => SetProperty(ref _sNguoiLap, value);
        }

        private string _sNoiDung;
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        public string sTenDonVi { get; set; }
        public string sTenNguonVon { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }
    }
}
