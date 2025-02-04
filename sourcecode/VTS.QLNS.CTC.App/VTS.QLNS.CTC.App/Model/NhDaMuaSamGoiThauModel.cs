using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaMuaSamGoiThauModel : ModelBase
    {
        public Guid? IIdGoiThauGocId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdParentAdjustId { get; set; }

        private Guid? _iIdNhaThauId;
        public Guid? IIdNhaThauId
        {
            get => _iIdNhaThauId;
            set => SetProperty(ref _iIdNhaThauId, value);
        }

        public int? ILoai { get; set; }
        public int? IThuocMenu { get; set; }
        public Guid? IIdCacQuyetDinhId { get; set; }
        public Guid? IIdKhlcnhaThau { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public Guid? IIdTiGiaUsdEurid { get; set; }
        public Guid? IIdTiGiaUsdVndid { get; set; }

        private Guid? _iIdHinhThucChonNhaThauId;
        public Guid? IIdHinhThucChonNhaThauId 
        { 
            get => _iIdHinhThucChonNhaThauId; 
            set => SetProperty(ref _iIdHinhThucChonNhaThauId, value); 
        }

        private Guid? _iIdPhuongThucDauThauId;
        public Guid? IIdPhuongThucDauThauId 
        { 
            get => _iIdPhuongThucDauThauId; 
            set => SetProperty(ref _iIdPhuongThucDauThauId, value); 
        }

        private Guid? _iIdLoaiHopDongId;
        public Guid? IIdLoaiHopDongId 
        { 
            get => _iIdLoaiHopDongId; 
            set => SetProperty(ref _iIdLoaiHopDongId, value); 
        }
        [ValidateAttribute("Số quyết định", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoQuyetDinh { get; set; }
        [ValidateAttribute("Ngày quyết định", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMaGoiThau { get; set; }

        private string _sTenGoiThau;
        public string STenGoiThau 
        { 
            get => _sTenGoiThau; 
            set => SetProperty(ref _sTenGoiThau, value); 
        }

        public string LoaiGoiThau { get; set; }
        public DateTime? DBatDauChonNhaThau { get; set; }
        public DateTime? DKetThucChonNhaThau { get; set; }

        private int? _iThoiGianThucHien;
        public int? IThoiGianThucHien 
        { 
            get => _iThoiGianThucHien; 
            set => SetProperty(ref _iThoiGianThucHien, value); 
        }

        private double? _fGiaGoiThauEur;
        public double? FGiaGoiThauEur 
        { 
            get => _fGiaGoiThauEur; 
            set => SetProperty(ref _fGiaGoiThauEur, value); 
        }

        private double? _fGiaGoiThauNgoaiTeKhac;
        public double? FGiaGoiThauNgoaiTeKhac 
        { 
            get => _fGiaGoiThauNgoaiTeKhac; 
            set => SetProperty(ref _fGiaGoiThauNgoaiTeKhac, value); 
        }

        private double? _fGiaGoiThauUsd;
        public double? FGiaGoiThauUsd 
        { 
            get => _fGiaGoiThauUsd; 
            set => SetProperty(ref _fGiaGoiThauUsd, value); 
        }

        private double? _fGiaGoiThauVnd;
        public double? FGiaGoiThauVnd 
        { 
            get => _fGiaGoiThauVnd; 
            set => SetProperty(ref _fGiaGoiThauVnd, value); 
        }

        public bool? BIsGoc { get; set; }
        public bool? BActive { get; set; }
        public int? ILanDieuChinh { get; set; }
        public bool BIsKhoa { get; set; }
        public bool BIsXoa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public string STenHinhThucChonNhaThau { get; set; }
        public string STenPhuongThucChonNhaThau { get; set; }
        public string DBatDauChonNhaThauString { get; set; }
        public string DKetThucChonNhaThauString { get; set; }
        public string DNgayQuyetDinhString { get; set; }

        public string STenDonVi { get; set; }
        public string STenDuAn { get; set; }
        public string STenChuongTrinh { get; set; }
        public string STenChuDauTu { get; set; }
        public string SDiaDiem { get; set; }
        public double? FTongPheDuyetUSD { get; set; }
        public double? FTongPheDuyetVND { get; set; }
        public double? FTongPheDuyetEUR { get; set; }
        public double? FTongPheDuyetNgoaiTeKhac { get; set; }
        public string STenHopDong { get; set; }
        public string SMoTa { get; set; }
        public int? TotalFiles { get; set; }
        public string STenTiGia { get; set; }
        public string DieuChinhTu { get; set; }
        public string STenLoaiHopDong { get; set; }

        private NhDaGoiThauModel _selectedGoiThauParent;
        public NhDaGoiThauModel SelectedGoiThauParent
        {
            get => _selectedGoiThauParent;
            set => SetProperty(ref _selectedGoiThauParent, value);
        }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdPhuongAnNhapKhauId { get; set; }
        public Guid? IIdQuyetDinhChiTietId { get; set; }
        public Guid? IIdKhTongTheNhiemVuChiId { get; set; }

        private Guid? _iIdDonViTienTeId;
        public Guid? IIdDonViTienTeId
        {
            get => _iIdDonViTienTeId;
            set => SetProperty(ref _iIdDonViTienTeId, value);
        }

        private double? _fGiaQuyetDinhChiTietUsd;
        public double? FGiaQuyetDinhChiTietUsd
        {
            get => _fGiaQuyetDinhChiTietUsd;
            set => SetProperty(ref _fGiaQuyetDinhChiTietUsd, value);
        }

        private double? _fGiaQuyetDinhChiTietVnd;
        public double? FGiaQuyetDinhChiTietVnd
        {
            get => _fGiaQuyetDinhChiTietVnd;
            set => SetProperty(ref _fGiaQuyetDinhChiTietVnd, value);
        }

        private double? _fGiaQuyetDinhChiTietEur;
        public double? FGiaQuyetDinhChiTietEur
        {
            get => _fGiaQuyetDinhChiTietEur;
            set => SetProperty(ref _fGiaQuyetDinhChiTietEur, value);
        }

        private double? _fGiaQuyetDinhChiTietNgoaiTeKhac;
        public double? FGiaQuyetDinhChiTietNgoaiTeKhac
        {
            get => _fGiaQuyetDinhChiTietNgoaiTeKhac;
            set => SetProperty(ref _fGiaQuyetDinhChiTietNgoaiTeKhac, value);
        }

        // Another properties
        private ObservableCollection<NhDaGoiThauNguonVonModel> _goiThauNguonVons = new ObservableCollection<NhDaGoiThauNguonVonModel>();
        public ObservableCollection<NhDaGoiThauNguonVonModel> GoiThauNguonVons
        {
            get => _goiThauNguonVons;
            set => SetProperty(ref _goiThauNguonVons, value);
        }
    }
}
