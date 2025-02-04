using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtQtBcquyetToanNienDoModel : BindableBase
    {
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public int IRowIndex { get; set; }
        private Guid _Id;
        public Guid Id
        {
            get => _Id;
            set => SetProperty(ref _Id, value);
        }

        private string _sSoDeNghi;
        public string SSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        private DateTime? _dNgayDeNghi;
        public DateTime? DNgayDeNghi
        {
            get => _dNgayDeNghi;
            set => SetProperty(ref _dNgayDeNghi, value);
        }

        private int? _iNamKeHoach;
        public int? INamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }

        private int? _iID_NguonVonID;
        public int? IIDNguonVonID
        {
            get => _iID_NguonVonID;
            set => SetProperty(ref _iID_NguonVonID, value);
        }

        private int? _iLoaiThanhToan;
        public int? ILoaiThanhToan
        {
            get => _iLoaiThanhToan;
            set => SetProperty(ref _iLoaiThanhToan, value);
        }

        private string _iID_MaDonViQuanLy;
        public string IIDMaDonViQuanLy
        {
            get => _iID_MaDonViQuanLy;
            set => SetProperty(ref _iID_MaDonViQuanLy, value);
        }

        private Guid? _iID_DonViQuanLyID;
        public Guid? IIDDonViQuanLyID
        {
            get => _iID_DonViQuanLyID;
            set => SetProperty(ref _iID_DonViQuanLyID, value);
        }

        private int? _iCoQuanThanhToan;
        public int? ICoQuanThanhToan
        {
            get => _iCoQuanThanhToan;
            set => SetProperty(ref _iCoQuanThanhToan, value);
        }

        public string STenDonVi { get; set; }
        public string STenNguonVon { get; set; }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
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

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        public string STongHop { get; set; }
        public string SUserCreate { get; set; }

        public Visibility IsShowCollapse => !string.IsNullOrEmpty(STongHop) ? Visibility.Visible : Visibility.Hidden;

        public string SLoaiThanhToan => (ILoaiThanhToan ?? 2) == (int)PaymentTypeEnum.Type.THANH_TOAN ? "Báo cáo quyết toán nguồn vốn đầu tư năm" : "Báo cáo kế hoạch và thanh toán vốn đầu tư - ứng trước kế hoạch vốn năm sau";
    }
}