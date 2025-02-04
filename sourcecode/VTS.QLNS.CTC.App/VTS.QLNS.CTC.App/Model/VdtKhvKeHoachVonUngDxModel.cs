using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKhvKeHoachVonUngDxModel : BindableBase
    {
        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public int IRowIndex { get; set; }
        public Guid Id { get; set; }

        private string _sSoDeNghi;
        public string SSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        public DateTime? _dNgayDeNghi;
        public DateTime? DNgayDeNghi
        {
            get => _dNgayDeNghi;
            set => SetProperty(ref _dNgayDeNghi, value);
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

        private Guid? _iID_NhomQuanLyID;
        public Guid? IIDNhomQuanLyID
        {
            get => _iID_NhomQuanLyID;
            set => SetProperty(ref _iID_NhomQuanLyID, value);
        }

        private double _fGiaTriUng;
        public double FGiaTriUng
        {
            get => _fGiaTriUng;
            set => SetProperty(ref _fGiaTriUng, value);
        }

        public Guid? IIDDonViTienTeID { get; set; }
        public Guid? IIDTienTeID { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string SUserCreate { get; set; }

        private int? _iID_NguonVonID;
        public int? IIDNguonVonID
        {
            get => _iID_NguonVonID;
            set => SetProperty(ref _iID_NguonVonID, value);
        }

        private string _sTongHop;
        public string STongHop
        {
            get => _sTongHop;
            set => SetProperty(ref _sTongHop, value);
        }

        private bool _bIsTongHop;
        public bool BIsTongHop
        {
            get => _bIsTongHop;
            set => SetProperty(ref _bIsTongHop, value);
        }

        private bool _bIsShowChild;
        public bool BIsShowChild
        {
            get => _bIsShowChild;
            set => SetProperty(ref _bIsShowChild, value);
        }

        public string STenDonViQuanLy { get; set; }
        public string STenNguonVon { get; set; }
        public List<Guid> LstDuAnId { get; set; }

        private List<VdtKhvKeHoachVonUngDxModel> _lstTongHop;
        public List<VdtKhvKeHoachVonUngDxModel> LstTongHop 
        {
            get => _lstTongHop;
            set => SetProperty(ref _lstTongHop, value);
        }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        public Visibility IsShowCollapse => !string.IsNullOrEmpty(STongHop) ? Visibility.Visible : Visibility.Hidden;

        public Guid? IIdParentId { get; set; }

        private bool _bActive;
        public bool BActive
        {
            get => _bActive;
            set => SetProperty(ref _bActive, value);
        }

        private bool? _bIsGoc;
        public bool? BIsGoc
        {
            get => _bIsGoc;
            set => SetProperty(ref _bIsGoc, value);
        }

        public string SSoLanDieuChinh { get; set; }
    }
}
