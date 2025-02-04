using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtTtDeNghiThanhToanModel : ModelBase
    {
        private int _iRowIndex;
        public int iRowIndex
        {
            get => _iRowIndex;
            set => SetProperty(ref _iRowIndex, value);
        }

        private string _sRowIndex;
        public string SRowIndex
        {
            get => _sRowIndex;
            set => SetProperty(ref _sRowIndex, value);
        }

        private Guid _id;
        public override Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string sKeHoachVon { get; set; }
        private string _sThongTinCanCu;
        public string sThongTinCanCu { get => _sThongTinCanCu; set => SetProperty(ref _sThongTinCanCu, value); }


        public string SLoaiKeHoachVon { get; set; }

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

        private DateTime? _dNgayPheDuyet;
        public DateTime? dNgayPheDuyet
        {
            get => _dNgayPheDuyet;
            set => SetProperty(ref _dNgayPheDuyet, value);
        }

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

        private Guid _iID_NhomQuanLyID;
        public Guid iID_NhomQuanLyID
        {
            get => _iID_NhomQuanLyID;
            set => SetProperty(ref _iID_NhomQuanLyID, value);
        }

        private string _sNguoiLap;
        public string sNguoiLap
        {
            get => _sNguoiLap;
            set => SetProperty(ref _sNguoiLap, value);
        }

        private int _iNamKeHoach;
        public int iNamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }

        private int _iID_NguonVonID;
        public int iID_NguonVonID
        {
            get => _iID_NguonVonID;
            set => SetProperty(ref _iID_NguonVonID, value);
        }

        private Guid? _iID_LoaiNguonVonID;
        public Guid? iID_LoaiNguonVonID
        {
            get => _iID_LoaiNguonVonID;
            set => SetProperty(ref _iID_LoaiNguonVonID, value);
        }

        private double _fGiaTriThanhToanTN;
        public double fGiaTriThanhToanTN
        {
            get => _fGiaTriThanhToanTN;
            set
            {
                SetProperty(ref _fGiaTriThanhToanTN, value);
                FSoTraDonViThuHuong = (fGiaTriThanhToanTN + fGiaTriThanhToanNN) - (fGiaTriThuHoiTN + fGiaTriThuHoiNN + FGiaTriThuHoiUngTruocTn + FGiaTriThuHoiUngTruocNn + FChuyenTienBaoHanh);
                OnPropertyChanged(nameof(FSoTraDonViThuHuong));
            }
        }

        private double _fGiaTriThanhToanTNDuocDuyet;
        public double fGiaTriThanhToanTNDuocDuyet
        {
            get => _fGiaTriThanhToanTNDuocDuyet;
            set
            {
                SetProperty(ref _fGiaTriThanhToanTNDuocDuyet, value);
                FSoTraDonViThuHuong = (fGiaTriThanhToanTN + fGiaTriThanhToanNN) - (fGiaTriThuHoiTN + fGiaTriThuHoiNN + FGiaTriThuHoiUngTruocTn + FGiaTriThuHoiUngTruocNn);
                OnPropertyChanged(nameof(FSoTraDonViThuHuong));
            }
        }

        private double _fGiaTriThanhToanNNDuocDuyet;
        public double fGiaTriThanhToanNNDuocDuyet
        {
            get => _fGiaTriThanhToanNNDuocDuyet;
            set
            {
                SetProperty(ref _fGiaTriThanhToanNNDuocDuyet, value);
                FSoTraDonViThuHuong = (fGiaTriThanhToanTN + fGiaTriThanhToanNN) - (fGiaTriThuHoiTN + fGiaTriThuHoiNN + FGiaTriThuHoiUngTruocTn + FGiaTriThuHoiUngTruocNn);
                OnPropertyChanged(nameof(FSoTraDonViThuHuong));
            }
        }

        private bool _bHoanTraUngTruoc;
        public bool BHoanTraUngTruoc
        {
            get => _bHoanTraUngTruoc;
            set => SetProperty(ref _bHoanTraUngTruoc, value);
        }

        public string SGiaTriThanhToanTN
        {
            get
            {
                return fGiaTriThanhToanTN.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
            }
        }

        private double _fGiaTriThanhToanNN;
        public double fGiaTriThanhToanNN
        {
            get => _fGiaTriThanhToanNN;
            set
            {
                SetProperty(ref _fGiaTriThanhToanNN, value);
                FSoTraDonViThuHuong = (fGiaTriThanhToanTN + fGiaTriThanhToanNN) - (fGiaTriThuHoiTN + fGiaTriThuHoiNN + FGiaTriThuHoiUngTruocTn + FGiaTriThuHoiUngTruocNn + FChuyenTienBaoHanh);
                OnPropertyChanged(nameof(FSoTraDonViThuHuong));
            }
        }

        public string SGiaTriThanhToanNN
        {
            get
            {
                return fGiaTriThanhToanNN.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
            }
        }

        private double _fGiaTriThuHoiTN;
        public double fGiaTriThuHoiTN
        {
            get => _fGiaTriThuHoiTN;
            set
            {
                SetProperty(ref _fGiaTriThuHoiTN, value);
                FSoTraDonViThuHuong = (fGiaTriThanhToanTN + fGiaTriThanhToanNN) - (fGiaTriThuHoiTN + fGiaTriThuHoiNN + FGiaTriThuHoiUngTruocTn + FGiaTriThuHoiUngTruocNn + FChuyenTienBaoHanh);
                OnPropertyChanged(nameof(FSoTraDonViThuHuong));
            }
        }

        public string SGiaTriThuHoiTN
        {
            get
            {
                return fGiaTriThuHoiTN.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
            }
        }

        public Guid? iID_ThongTriThanhToanID { get; set; }

        private double _fGiaTriThuHoiNN;
        public double fGiaTriThuHoiNN
        {
            get => _fGiaTriThuHoiNN;
            set
            {
                SetProperty(ref _fGiaTriThuHoiNN, value);
                FSoTraDonViThuHuong = (fGiaTriThanhToanTN + fGiaTriThanhToanNN) - (fGiaTriThuHoiTN + fGiaTriThuHoiNN + FGiaTriThuHoiUngTruocTn + FGiaTriThuHoiUngTruocNn + FChuyenTienBaoHanh);
                OnPropertyChanged(nameof(FSoTraDonViThuHuong));
            }
        }

        public string SGiaTriThuHoiNN
        {
            get
            {
                return fGiaTriThuHoiNN.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
            }
        }

        public string SGiaTriThuHoiUngTruocTN
        {
            get
            {
                return FGiaTriThuHoiUngTruocTn.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
            }
        }

        public string SGiaTriThuHoiUngTruocNN
        {
            get
            {
                return FGiaTriThuHoiUngTruocNn.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
            }
        }

        private double _fGiaTriThuHoiUngTruocTn;
        public double FGiaTriThuHoiUngTruocTn
        {
            get => _fGiaTriThuHoiUngTruocTn;
            set
            {
                SetProperty(ref _fGiaTriThuHoiUngTruocTn, value);
                FSoTraDonViThuHuong = (fGiaTriThanhToanTN + fGiaTriThanhToanNN) - (fGiaTriThuHoiTN + fGiaTriThuHoiNN + FGiaTriThuHoiUngTruocTn + FGiaTriThuHoiUngTruocNn + FChuyenTienBaoHanh);
                OnPropertyChanged(nameof(FSoTraDonViThuHuong));
            }
        }

        private double _fGiaTriThuHoiUngTruocNn;
        public double FGiaTriThuHoiUngTruocNn
        {
            get => _fGiaTriThuHoiUngTruocNn;
            set
            {
                SetProperty(ref _fGiaTriThuHoiUngTruocNn, value);
                FSoTraDonViThuHuong = (fGiaTriThanhToanTN + fGiaTriThanhToanNN) - (fGiaTriThuHoiTN + fGiaTriThuHoiNN + FGiaTriThuHoiUngTruocTn + FGiaTriThuHoiUngTruocNn + FChuyenTienBaoHanh);
                OnPropertyChanged(nameof(FSoTraDonViThuHuong));
            }
        }

        private double _fThueGiaTriGiaTang;
        public double FThueGiaTriGiaTang
        {
            get => _fThueGiaTriGiaTang;
            set => SetProperty(ref _fThueGiaTriGiaTang, value);
        }

        private double _fChuyenTienBaoHanh;
        public double FChuyenTienBaoHanh
        {
            get => _fChuyenTienBaoHanh;
            set
            {
                SetProperty(ref _fChuyenTienBaoHanh, value);
                FSoTraDonViThuHuong = (fGiaTriThanhToanTN + fGiaTriThanhToanNN) - (fGiaTriThuHoiTN + fGiaTriThuHoiNN + FGiaTriThuHoiUngTruocTn + FGiaTriThuHoiUngTruocNn + FChuyenTienBaoHanh);
                OnPropertyChanged(nameof(FSoTraDonViThuHuong));
            }
        }

        private double _fThueGiaTriGiaTangDuocDuyet;
        public double FThueGiaTriGiaTangDuocDuyet
        {
            get => _fThueGiaTriGiaTangDuocDuyet;
            set => SetProperty(ref _fThueGiaTriGiaTangDuocDuyet, value);
        }

        private double _fChuyenTienBaoHanhDuocDuyet;
        public double FChuyenTienBaoHanhDuocDuyet
        {
            get => _fChuyenTienBaoHanhDuocDuyet;
            set => SetProperty(ref _fChuyenTienBaoHanhDuocDuyet, value);
        }

        private string _sGhiChu;
        public string sGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
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

        private int _iLoaiThanhToan;
        public int iLoaiThanhToan
        {
            get => _iLoaiThanhToan;
            set => SetProperty(ref _iLoaiThanhToan, value);
        }

        public string sLoaiThanhToan
        {
            get
            {
                if (BTongHop.HasValue) return string.Empty; 
                return iLoaiThanhToan == (int)PaymentTypeEnum.Type.THANH_TOAN ? PaymentTypeEnum.TypeName.THANH_TOAN : PaymentTypeEnum.TypeName.TAM_UNG;
            }
        }

        private Guid? _iID_DuAnId;
        public Guid? iID_DuAnId
        {
            get => _iID_DuAnId;
            set => SetProperty(ref _iID_DuAnId, value);
        }

        private Guid? _iID_HopDongId;
        public Guid? iID_HopDongId
        {
            get => _iID_HopDongId;
            set => SetProperty(ref _iID_HopDongId, value);
        }

        private Guid? _iID_NhaThauId;
        public Guid? iID_NhaThauId
        {
            get => _iID_NhaThauId;
            set => SetProperty(ref _iID_NhaThauId, value);
        }

        private Guid? _iID_PhanBoVonID;
        public Guid? iID_PhanBoVonID
        {
            get => _iID_PhanBoVonID;
            set => SetProperty(ref _iID_PhanBoVonID, value);
        }

        private string _sNguonVon;
        public string sNguonVon
        {
            get => _sNguonVon;
            set => SetProperty(ref _sNguonVon, value);
        }

        private string _sLoaiNguonVon;
        public string sLoaiNguonVon
        {
            get => _sLoaiNguonVon;
            set => SetProperty(ref _sLoaiNguonVon, value);
        }

        private string _sTenDonVi;
        public string sTenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private string _sTenDuAn;
        public string sTenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        public string sMaDuAn { get; set; }

        private string _sSoHopDong;
        public string sSoHopDong
        {
            get => _sSoHopDong;
            set => SetProperty(ref _sSoHopDong, value);
        }

        private DateTime? _dNgayHopDong;
        public DateTime? dNgayHopDong
        {
            get => _dNgayHopDong;
            set => SetProperty(ref _dNgayHopDong, value);
        }

        private double? _fGiaTriHopDong;
        public double? fGiaTriHopDong
        {
            get => _fGiaTriHopDong;
            set => SetProperty(ref _fGiaTriHopDong, value);
        }

        private string _sMaNhaThau;
        public string sMaNhaThau
        {
            get => _sMaNhaThau;
            set => SetProperty(ref _sMaNhaThau, value);
        }

        private int? _iCoQuanThanhToan;
        public int? iCoQuanThanhToan
        {
            get => _iCoQuanThanhToan;
            set => SetProperty(ref _iCoQuanThanhToan, value);
        }

        private string _sGhiChuPheDuyet;
        public string SGhiChuPheDuyet
        {
            get => _sGhiChuPheDuyet;
            set => SetProperty(ref _sGhiChuPheDuyet, value);
        }

        private string _sLyDoTuChoi;
        public string SLyDoTuChoi
        {
            get => _sLyDoTuChoi;
            set => SetProperty(ref _sLyDoTuChoi, value);
        }

        public string CoQuanThanhToan => (iCoQuanThanhToan.HasValue && iCoQuanThanhToan.Value == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ?
            CoQuanThanhToanEnum.TypeName.KHO_BAC : (iCoQuanThanhToan.HasValue && iCoQuanThanhToan.Value == (int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI) ? CoQuanThanhToanEnum.TypeName.TONKHOAN_DONVI : ((loaiCoQuanTaiChinh == 0) ? CoQuanThanhToanEnum.TypeName.CQTC : CoQuanThanhToanEnum.TypeName.CTC);

        public bool IsEdit { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private bool _isLuuChuyen;
        public bool IsLuuChuyen
        {
            get => _isLuuChuyen;
            set => SetProperty(ref _isLuuChuyen, value);
        }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        private string _sSoBangKlht;
        public string SSoBangKlht
        {
            get => _sSoBangKlht;
            set => SetProperty(ref _sSoBangKlht, value);
        }

        private DateTime? _dNgayBangKlht;
        public DateTime? DNgayBangKlht
        {
            get => _dNgayBangKlht;
            set => SetProperty(ref _dNgayBangKlht, value);
        }

        private string _sTenDonViThuHuong;
        public string STenDonViThuHuong
        {
            get => _sTenDonViThuHuong;
            set => SetProperty(ref _sTenDonViThuHuong, value);
        }

        private string _sSoTaiKhoanNhaThau;
        public string SSoTaiKhoanNhaThau
        {
            get => _sSoTaiKhoanNhaThau;
            set => SetProperty(ref _sSoTaiKhoanNhaThau, value);
        }

        private string _sMaNganHang;
        public string SMaNganHang
        {
            get => _sMaNganHang;
            set => SetProperty(ref _sMaNganHang, value);
        }

        private double _fLuyKeGiaTriNghiemThuKlht;
        public double FLuyKeGiaTriNghiemThuKlht
        {
            get => _fLuyKeGiaTriNghiemThuKlht;
            set => SetProperty(ref _fLuyKeGiaTriNghiemThuKlht, value);
        }

        private double _fSoTraDonViThuHuong;
        public double FSoTraDonViThuHuong
        {
            get => _fSoTraDonViThuHuong;
            set => SetProperty(ref _fSoTraDonViThuHuong, value);
        }

        private Guid? _iIdChiPhiId;
        public Guid? IIdChiPhiId
        {
            get => _iIdChiPhiId;
            set => SetProperty(ref _iIdChiPhiId, value);
        }

        private string _maChiPhi;
        public string MaChiPhi
        {
            get => _maChiPhi;
            set => SetProperty(ref _maChiPhi, value);
        }

        private bool _bThanhToanTheoHopDong;
        public bool BThanhToanTheoHopDong
        {
            get => _bThanhToanTheoHopDong;
            set => SetProperty(ref _bThanhToanTheoHopDong, value);
        }

        public Guid? ParentId { get; set; }
        public bool? BTongHop { get; set; }

        private bool _isShowChildren;
        public bool IsShowChildren 
        {
            get => _isShowChildren;
            set => SetProperty(ref _isShowChildren, value);
        }

        public HashSet<Guid> AncestorIds { get; internal set; }
        public bool HasChildren { get; internal set; }

        public string SSoHDChiPhi => BThanhToanTheoHopDong ? sSoHopDong : MaChiPhi;

        public string SThanhToanTheoHopDong => BThanhToanTheoHopDong ? "true" : "false";

        private string _sTenHopDong;
        public string sTenHopDong
        {
            get => _sTenHopDong;
            set => SetProperty(ref _sTenHopDong, value);
        }

        private double _fLuyKeThanhToanTN;
        public double fLuyKeThanhToanTN
        {
            get => _fLuyKeThanhToanTN;
            set => SetProperty(ref _fLuyKeThanhToanTN, value);
        }

        private double _fLuyKeThanhToanNN;
        public double fLuyKeThanhToanNN
        {
            get => _fLuyKeThanhToanNN;
            set => SetProperty(ref _fLuyKeThanhToanNN, value);
        }

        private double _fLuyKeTUChuaThuHoiNN;
        public double fLuyKeTUChuaThuHoiNN
        {
            get => _fLuyKeTUChuaThuHoiNN;
            set => SetProperty(ref _fLuyKeTUChuaThuHoiNN, value);
        }

        private double _fLuyKeTUChuaThuHoiTN;
        public double fLuyKeTUChuaThuHoiTN
        {
            get => _fLuyKeTUChuaThuHoiTN;
            set => SetProperty(ref _fLuyKeTUChuaThuHoiTN, value);
        }

        private double _fLuyKeTUChuaThuHoiKhacTN;
        public double fLuyKeTUChuaThuHoiKhacTN
        {
            get => _fLuyKeTUChuaThuHoiKhacTN;
            set => SetProperty(ref _fLuyKeTUChuaThuHoiKhacTN, value);
        }

        private double _fLuyKeTUChuaThuHoiKhacNN;
        public double fLuyKeTUChuaThuHoiKhacNN
        {
            get => _fLuyKeTUChuaThuHoiKhacNN;
            set => SetProperty(ref _fLuyKeTUChuaThuHoiKhacNN, value);
        }

        private int _loaiCoQuanTaiChinh;
        public int loaiCoQuanTaiChinh
        {
            get => _loaiCoQuanTaiChinh;
            set => SetProperty(ref _loaiCoQuanTaiChinh, value);
        }

        private Guid _ID_DuAn_HangMuc;
        public Guid ID_DuAn_HangMuc
        {
            get => _ID_DuAn_HangMuc;
            set => SetProperty(ref _ID_DuAn_HangMuc, value);
        }
    }
}
