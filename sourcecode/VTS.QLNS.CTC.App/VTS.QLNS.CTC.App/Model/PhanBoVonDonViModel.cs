using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class PhanBoVonDonViModel : ModelBase
    {
        public int iRowIndex { get; set; }
        public Guid Id { get; set; }
        public string sSoQuyetDinh { get; set; }
        public DateTime? dNgayQuyetDinh { get; set; }

        private string _sNguoiLap;
        public string sNguoiLap
        {
            get => _sNguoiLap;
            set => SetProperty(ref _sNguoiLap, value);
        }

        private string _sTruongPhong;
        public string sTruongPhong
        {
            get => _sTruongPhong;
            set => SetProperty(ref _sTruongPhong, value);
        }

        private int _iNamKeHoach;
        public int iNamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }

        private int _iId_NguonVonId;
        public int iId_NguonVonId
        {
            get => _iId_NguonVonId;
            set => SetProperty(ref _iId_NguonVonId, value);
        }

        public string sTenNguonVon { get; set; }

        private Guid _iID_DonViQuanLyID;
        public Guid iID_DonViQuanLyID
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
        public string sTenDonVi { get; set; }
        public Guid? iID_ParentId { get; set; }
        public bool? bIsCanBoDuyet { get; set; }
        public bool? bIsDuyet { get; set; }

        private double? _fThuHoiVonUngTruoc;
        public double? fThuHoiVonUngTruoc
        {
            get => _fThuHoiVonUngTruoc;
            set => SetProperty(ref _fThuHoiVonUngTruoc, value);
        }

        private double? _fThanhToan;
        public double? fThanhToan
        {
            get => _fThanhToan;
            set => SetProperty(ref _fThanhToan, value);
        }
        public bool IsEdit { get; set; }
        public bool IsAdjust { get; set; }
        public Guid? IdAdjust { get; set; }
        public int ActionState => IsEdit ? (int)TypeExecute.Update : IsAdjust ? (int)TypeExecute.Adjust : (int)TypeExecute.Insert;
        public string SUserCreate { get; set; }
        public string STongHop { get; set; }
        
        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        private bool _isTongHop;
        public bool IsTongHop
        {
            get => _isTongHop;
            set => SetProperty(ref _isTongHop, value);
        }

        public string sCanBoDuyet
        {
            get
            {
                return bIsCanBoDuyet ?? false ? ApproveTypeEnum.TypeName.DA_DUYET : ApproveTypeEnum.TypeName.CHUA_DUYET;
            }
        }
        public string sDuyet
        {
            get
            {
                return bIsDuyet ?? false ? ApproveTypeEnum.TypeName.DA_DUYET : ApproveTypeEnum.TypeName.CHUA_DUYET;
            }
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public string SSoLanDieuChinh { get; set; }
        public string DieuChinhTu { get; set; }
        public bool BActive { get; set; }
        public List<Guid?> LstDuAnId { get; set; }
        public bool IsChooseDuAn { get; set; }
        public bool IsViewDetail { get; set; }
    }
}
