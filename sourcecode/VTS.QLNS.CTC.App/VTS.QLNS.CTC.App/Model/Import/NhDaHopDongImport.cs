using System;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [Sheet(5, "DS Hợp đồng", 4, 0)]
    public class NhDaHopDongImport : BindableBase
    {
        public Guid IIdHopDongID { get; set; } = Guid.NewGuid();
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }
        private string _soHopDong;
        [Column("Số hợp đồng", 1, ValidateType.IsString, true)]
        public string SoHopDong
        {
            get => _soHopDong;
            set => SetProperty(ref _soHopDong, value);
        }
        private string _tenHopDong;
        [Column("Tên hợp đồng", 2, ValidateType.IsString, true)]
        public string TenHopDong
        {
            get => _tenHopDong;
            set => SetProperty(ref _tenHopDong, value);
        }
        private string _ngayHopDong;
        [Column("Ngày hợp đồng", 3)]
        public string NgayHopDong
        {
            get => _ngayHopDong;
            set => SetProperty(ref _ngayHopDong, value);
        }
        public DateTime? DNgayHopDong
        {
            get
            {
                return DateUtils.CheckDateFormatAndConverter(this.NgayHopDong);
            }
        }
        private string _maLoaiHopDong;
        //[Column("Loại hợp đồng", 3)]
        public string MaLoaiHopDong
        {
            get => _maLoaiHopDong;
            set => SetProperty(ref _maLoaiHopDong, value);
        }
        private string _maNhaThauDaiDien;
        //[Column("Nhà thầu đại diện", 4, ValidateType.IsString, true)]
        public string MaNhaThauDaiDien
        {
            get => _maNhaThauDaiDien;
            set => SetProperty(ref _maNhaThauDaiDien, value);
        }
        private string _thoiGianThucHienTu;
        //[Column("Ngày khởi công dự kiến", 5)]
        public string SKhoiCong
        {
            get => _thoiGianThucHienTu;
            set => SetProperty(ref _thoiGianThucHienTu, value);
        }
        private string _thoiGianThucHienDen;
        //[Column("Ngày kết thúc dự kiến", 6)]
        public string SKetThuc
        {
            get => _thoiGianThucHienDen;
            set => SetProperty(ref _thoiGianThucHienDen, value);
        }
        private string _thoiGianThucHIen;
        //[Column("Thời gian thực hiện", 7)]
        public string IThoiGianThucHien
        {
            get => _thoiGianThucHIen;
            set => SetProperty(ref _thoiGianThucHIen, value);
        }
        private string _sThuocMenu;
        [Column("Menu", 4)]
        public string SThuocMenu
        {
            get => _sThuocMenu;
            set => SetProperty(ref _sThuocMenu, value);
        }
        public int? ILoai { get; set; }

        public int IThuocMenu
        {
            get
            {
                if (this.SThuocMenu.ToLower().Trim().Equals(NhTongHopConstants.SMENU_MS_HD_NT.ToLower().Trim()))
                {
                    this.ILoai = NHConstants.ILOAI_HD_NT;
                    return NHConstants.IMENU_MS_HD_NT;
                }
                else if (SThuocMenu.ToLower().Trim().Equals(NhTongHopConstants.SMENU_MS_HD_TN.ToLower().Trim()))
                {
                    this.ILoai = NHConstants.ILOAI_HD_TN;
                    return NHConstants.IMENU_MS_HD_TN;
                }
                else if (this.SThuocMenu.ToLower().Trim().Equals(NhTongHopConstants.SMENU_MS_GT_HD_TN.ToLower().Trim()))
                {
                    this.ILoai = NHConstants.ILOAI_HD_TN;
                    return NHConstants.IMENU_MS_GT_HD_TN;
                }
                else if (this.SThuocMenu.ToLower().Trim().Equals(NhTongHopConstants.SMENU_MS_KGT_HD_TN.ToLower().Trim()))
                {
                    this.ILoai = NHConstants.ILOAI_HD_TN;
                    return NHConstants.IMENU_MS_KGT_HD_TN;
                }
                else if (this.SThuocMenu.ToLower().Trim().Equals(NhTongHopConstants.SMENU_MS_DA_HD_TN.ToLower().Trim()))
                {
                    this.ILoai = NHConstants.ILOAI_HD_TN;
                    return NHConstants.IMENU_MS_DA_HD_TN;
                }
                else if (this.SThuocMenu.ToLower().Trim().Equals(NhTongHopConstants.SMENU_MS_DA_HD_NT.ToLower().Trim()))
                {
                    this.ILoai = NHConstants.ILOAI_HD_NT;
                    return NHConstants.IMENU_MS_DA_HD_NT;
                }
                else if (this.SThuocMenu.ToLower().Trim().Equals(NhTongHopConstants.SMENU_DA_HD_NT.ToLower().Trim()))
                {
                    this.ILoai = NHConstants.ILOAI_HD_NT;
                    return NHConstants.IMENU_DA_HD_NT;
                }
                else if (this.SThuocMenu.ToLower().Trim().Equals(NhTongHopConstants.SMENU_DA_HD_TN.ToLower().Trim()))
                {
                    this.ILoai = NHConstants.ILOAI_HD_TN;
                    return NHConstants.IMENU_DA_HD_TN;
                }
                else
                {
                    this.ILoai = NHConstants.ZERO;
                    return 0;
                }
            }
        }
    }
}
