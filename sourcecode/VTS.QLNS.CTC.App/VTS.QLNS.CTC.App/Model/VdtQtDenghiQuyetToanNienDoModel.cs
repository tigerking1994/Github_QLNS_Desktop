using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtQtDenghiQuyetToanNienDoModel : BindableBase
    {
        public int iRowIndex { get; set; }
        private Guid _Id;
        public Guid Id
        {
            get => _Id;
            set => SetProperty(ref _Id, value);
        }

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

        private Guid? _iID_DonViDeNghiID;
        public Guid? iID_DonViDeNghiID
        {
            get => _iID_DonViDeNghiID;
            set => SetProperty(ref _iID_DonViDeNghiID, value);
        }

        private string _iID_MaDonViDeNghi;
        public string iID_MaDonViDeNghi
        {
            get => _iID_MaDonViDeNghi;
            set => SetProperty(ref _iID_MaDonViDeNghi, value);
        }

        private string _sNguoiDeNghi;
        public string sNguoiDeNghi
        {
            get => _sNguoiDeNghi;
            set => SetProperty(ref _sNguoiDeNghi, value);
        }

        private int? _iNamKeHoach;
        public int? iNamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }

        private Guid? _iID_LoaiNguonVonID;
        public Guid? iID_LoaiNguonVonID
        {
            get => _iID_LoaiNguonVonID;
            set => SetProperty(ref _iID_LoaiNguonVonID, value);
        }

        private string _sUserCreate;
        public string sUserCreate
        {
            get => _sUserCreate;
            set => SetProperty(ref _sUserCreate, value);
        }

        private DateTime? _dDateCreate;
        public DateTime? dDateCreate
        {
            get => _dDateCreate;
            set => SetProperty(ref _dDateCreate, value);
        }

        private string _sUserUpdate;
        public string sUserUpdate
        {
            get => _sUserUpdate;
            set => SetProperty(ref _sUserUpdate, value);
        }

        private DateTime? _dDateUpdate;
        public DateTime? dDateUpdate
        {
            get => _dDateUpdate;
            set => SetProperty(ref _dDateUpdate, value);
        }

        private string _sUserDelete;
        public string sUserDelete
        {
            get => _sUserDelete;
            set => SetProperty(ref _sUserDelete, value);
        }

        private DateTime? _dDateDelete;
        public DateTime? dDateDelete
        {
            get => _dDateDelete;
            set => SetProperty(ref _dDateDelete, value);
        }

        private int? _iID_NguonVonID;
        public int? iID_NguonVonID
        {
            get => _iID_NguonVonID;
            set => SetProperty(ref _iID_NguonVonID, value);
        }

        private string _sTenDonVi;
        public string sTenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private string _sTenNguonVon;
        public string sTenNguonVon
        {
            get => _sTenNguonVon;
            set => SetProperty(ref _sTenNguonVon, value);
        }

        private string _sLoaiNguonVon;
        public string sLoaiNguonVon
        {
            get => _sLoaiNguonVon;
            set => SetProperty(ref _sLoaiNguonVon, value);
        }

        private double? _fGiaTriNamNay;
        public double? fGiaTriNamNay
        {
            get => _fGiaTriNamNay;
            set => SetProperty(ref _fGiaTriNamNay, value);
        }

        private double? _fGiaTriNamTruoc;
        public double? fGiaTriNamTruoc
        {
            get => _fGiaTriNamTruoc;
            set => SetProperty(ref _fGiaTriNamTruoc, value);
        }
    }
}
