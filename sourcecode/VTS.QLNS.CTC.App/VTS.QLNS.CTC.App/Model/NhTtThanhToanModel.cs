using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhTtThanhToanModel : ModelBase
    {
        [Validate("Đơn vị cấp trên", DATA_TYPE.Guid)]
        public Guid? IIdDonViCapTren { get; set; }
        [Validate("Mã đơn vị cấp trên")]
        public string IIdMaDonViCapTren { get; set; }

        [Validate("Đơn vị", DATA_TYPE.Guid, true)]
        public Guid? IIdDonVi { get; set; }
        [Validate("Mã đơn vị", DATA_TYPE.String, true)]
        public string IIdMaDonVi { get; set; }

        [Validate("Số đề nghị", DATA_TYPE.String, 50, false)]
        public string SSoDeNghi { get; set; }

        [Validate("Ngày đề nghị", DATA_TYPE.Date, false)]
        public DateTime? DNgayDeNghi { get; set; }

        [Validate("Kính gửi", DATA_TYPE.String, 100)]
        public string SKinhGui { get; set; }

        [Validate("Kế hoạch tổng thể BQP", DATA_TYPE.Guid, true)]
        public Guid? IIdKhtongTheId { get; set; }

        [Validate("Nhiệm vụ chi", DATA_TYPE.Guid, true)]
        public Guid? IIdNhiemVuChiId { get; set; }

        [Validate("Chủ đầu tư", DATA_TYPE.Guid, false)]
        public Guid? IIdChuDauTuId { get; set; }
        [Validate("Mã chủ đầu tư", DATA_TYPE.String, 50, false)]
        public string IIdMaChuDauTu { get; set; }

        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdHopDongId { get; set; }

        [Validate("Căn cứ")]
        public string SCanCu { get; set; }

        public int _iLoaiDeNghi;

        [Validate("Loại đề nghị", DATA_TYPE.Int, true)]
        public int ILoaiDeNghi
        {
            get => _iLoaiDeNghi;
            set => SetProperty(ref _iLoaiDeNghi, value);
        }

        public int? _iNamKeHoach;

        [Validate("Năm kế hoạch", DATA_TYPE.Int, true)]
        public int? INamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }

        [Validate("Quý kế hoạch", DATA_TYPE.Int, true)]
        public int? IQuyKeHoach { get; set; }

        [Validate("Năm ngân sách", DATA_TYPE.Int)]
        public int? INamNganSach { get; set; }

        [Validate("Nguồn vốn", DATA_TYPE.Int)]
        public int? IIdNguonVonId { get; set; }

        [Validate("Tỉ giá", DATA_TYPE.Guid)]
        public Guid? IIdTiGiaId { get; set; }

        [Validate("Mã ngoại tệ khác", DATA_TYPE.String, 50)]
        public string SMaNgoaiTeKhac { get; set; }

        [Validate("Loại nội dung chi", DATA_TYPE.Int)]
        public int? ILoaiNoiDungChi { get; set; }

        [Validate("Đơn vị thụ hưởng", DATA_TYPE.Guid)]
        public Guid? IIdNhaThauId { get; set; }

        public Guid? IIdNhaThauNguoiNhanId { get; set; }
        public Guid? IIdNhaThauNganHangId { get; set; }
        public string STruongPhong { get; set; }
        public string SThuTruongDonVi { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiXoa { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public bool BIsKhoa { get; set; }
        public bool BIsXoa { get; set; }
        private double? _fTongDeNghiKyNayUsd;
        public double? FTongDeNghiKyNayUsd
        {
            get => _fTongDeNghiKyNayUsd;
            set => SetProperty(ref _fTongDeNghiKyNayUsd, value);
        }
        private double? _fTongDeNghiKyNayVnd;
        public double? FTongDeNghiKyNayVnd
        {
            get => _fTongDeNghiKyNayVnd;
            set => SetProperty(ref _fTongDeNghiKyNayVnd, value);
        }
        private double? _fTongDeNghiKyNayEur;
        public double? FTongDeNghiKyNayEur
        {
            get => _fTongDeNghiKyNayEur;
            set => SetProperty(ref _fTongDeNghiKyNayEur, value);
        }
        private double? _fTongDeNghiKyNayNgoaiTeKhac;
        public double? FTongDeNghiKyNayNgoaiTeKhac
        {
            get => _fTongDeNghiKyNayNgoaiTeKhac;
            set => SetProperty(ref _fTongDeNghiKyNayNgoaiTeKhac, value);
        }
        private double? _fTongDeNghiBangSo;
        public double? FTongDeNghiBangSo
        {
            get => _fTongDeNghiBangSo;
            set => SetProperty(ref _fTongDeNghiBangSo, value);
        }

        private string _sTongDeNghiBangChu;
        public string STongDeNghiBangChu
        {
            get => _sTongDeNghiBangChu;
            set => SetProperty(ref _sTongDeNghiBangChu, value);
        }
        private double? _fThuHoiTamUngBangSo;
        public double? FThuHoiTamUngBangSo
        {
            get => _fThuHoiTamUngBangSo;
            set => SetProperty(ref _fThuHoiTamUngBangSo, value);
        }
        private string _fThuHoiTamUngBangChu;
        public string FThuHoiTamUngBangChu
        {
            get => _fThuHoiTamUngBangChu;
            set => SetProperty(ref _fThuHoiTamUngBangChu, value);
        }
        private double? _fTraDonViThuHuongBangSo;
        public double? FTraDonViThuHuongBangSo
        {
            get => _fTraDonViThuHuongBangSo;
            set => SetProperty(ref _fTraDonViThuHuongBangSo, value);
        }
        private string _fTraDonViThuHuongBangChu;
        public string FTraDonViThuHuongBangChu
        {
            get => _fTraDonViThuHuongBangChu;
            set => SetProperty(ref _fTraDonViThuHuongBangChu, value);
        }
        private double? _fChuyenKhoanBangSo;
        public double? FChuyenKhoanBangSo
        {
            get => _fChuyenKhoanBangSo;
            set => SetProperty(ref _fChuyenKhoanBangSo, value);
        }
        private string _sChuyenKhoanBangChu;
        [Validate("Chuyển khoản bằng chữ")]
        public string SChuyenKhoanBangChu
        {
            get => _sChuyenKhoanBangChu;
            set => SetProperty(ref _sChuyenKhoanBangChu, value);
        }
        private double? _fTienMatBangSo;
        public double? FTienMatBangSo
        {
            get => _fTienMatBangSo;
            set => SetProperty(ref _fTienMatBangSo, value);
        }
        private string _sTienMatBangChu;
        public string STienMatBangChu
        {
            get => _sTienMatBangChu;
            set => SetProperty(ref _sTienMatBangChu, value);
        }
        private double? _fSoDuTamUng;
        public double? FSoDuTamUng
        {
            get => _fSoDuTamUng;
            set => SetProperty(ref _fSoDuTamUng, value);
        }
        private double? _fTongPheDuyetCapKyNayUsd;
        public double? FTongPheDuyetCapKyNayUsd
        {
            get => _fTongPheDuyetCapKyNayUsd;
            set => SetProperty(ref _fTongPheDuyetCapKyNayUsd, value);
        }
        private double? _fTongPheDuyetCapKyNayVnd;
        public double? FTongPheDuyetCapKyNayVnd
        {
            get => _fTongPheDuyetCapKyNayVnd;
            set => SetProperty(ref _fTongPheDuyetCapKyNayVnd, value);
        }
        private double? _fTongPheDuyetCapKyNayEur;
        public double? FTongPheDuyetCapKyNayEur
        {
            get => _fTongPheDuyetCapKyNayEur;
            set => SetProperty(ref _fTongPheDuyetCapKyNayEur, value);
        }
        private double? _fTongPheDuyetCapKyNayNgoaiTeKhac;
        public double? FTongPheDuyetCapKyNayNgoaiTeKhac
        {
            get => _fTongPheDuyetCapKyNayNgoaiTeKhac;
            set => SetProperty(ref _fTongPheDuyetCapKyNayNgoaiTeKhac, value);
        }

        [Validate("Trạng thái", DATA_TYPE.Int)]
        public int? ITrangThai { get; set; }
        public string STenDonViMaDonVi { get; set; }
        public string STenNhiemVuChi { get; set; }
        public string STenHopDongSoHopDong { get; set; }

        [Validate("Cơ quan thanh toán", DATA_TYPE.Int)]
        public int? ICoQuanThanhToan { get; set; }

        public Guid? IIdTiGiaPheDuyetId { get; set; }
        public DateTime? DNgayPheDuyet { get; set; }
        private double? _fTongPheDuyetBangSo;
        public double? FTongPheDuyetBangSo
        {
            get => _fTongPheDuyetBangSo;
            set => SetProperty(ref _fTongPheDuyetBangSo, value);
        }
        private string _sTongPheDuyetBangChu;
        public string STongPheDuyetBangChu
        {
            get => _sTongPheDuyetBangChu;
            set => SetProperty(ref _sTongPheDuyetBangChu, value);
        }
        private double? _fThuHoiTamUngPheDuyetBangSo;
        public double? FThuHoiTamUngPheDuyetBangSo
        {
            get => _fThuHoiTamUngPheDuyetBangSo;
            set => SetProperty(ref _fThuHoiTamUngPheDuyetBangSo, value);
        }
        private string _fThuHoiTamUngPheDuyetBangChu;
        public string FThuHoiTamUngPheDuyetBangChu
        {
            get => _fThuHoiTamUngPheDuyetBangChu;
            set => SetProperty(ref _fThuHoiTamUngPheDuyetBangChu, value);
        }
        private double? _fTraDonViThuHuongPheDuyetBangSo;
        public double? FTraDonViThuHuongPheDuyetBangSo
        {
            get => _fTraDonViThuHuongPheDuyetBangSo;
            set => SetProperty(ref _fTraDonViThuHuongPheDuyetBangSo, value);
        }
        private string _fTraDonViThuHuongPheDuyetBangChu;
        public string FTraDonViThuHuongPheDuyetBangChu
        {
            get => _fTraDonViThuHuongPheDuyetBangChu;
            set => SetProperty(ref _fTraDonViThuHuongPheDuyetBangChu, value);
        }
        private double? _fTuChoiThanhToanBangSo;
        public double? FTuChoiThanhToanBangSo
        {
            get => _fTuChoiThanhToanBangSo;
            set => SetProperty(ref _fTuChoiThanhToanBangSo, value);
        }
        public string SLyDoTuChoi { get; set; }
        public string SGhiChu { get; set; }
        public string LoaiDeNghiString => NhLoaiDeNghi.Get(ILoaiDeNghi);
        public string LoaiNoiDungChiString => ILoaiNoiDungChi.HasValue ? LoaiNoiDungChi.Get(ILoaiNoiDungChi.Value) : "";
        public string QuyKeHoachString => IQuyKeHoach.HasValue ? LoaiQuyEnum.Get(IQuyKeHoach.Value, false) : "";
        public string TenNguonVon { get; set; }
        public int TotalFiles { get; set; }

        private string _sSoTaiKhoan;
        [Validate("Số tài khoản", DATA_TYPE.String, 100)]
        public string SSoTaiKhoan
        {
            get => _sSoTaiKhoan;
            set => SetProperty(ref _sSoTaiKhoan, value);
        }

        private string _sNganHang;
        [Validate("Ngân hàng", DATA_TYPE.String, 100)]
        public string SNganHang
        {
            get => _sNganHang;
            set => SetProperty(ref _sNganHang, value);
        }

        private string _sNguoiLienHe;
        [Validate("Người liên hệ", DATA_TYPE.String, 300)]
        public string SNguoiLienHe
        {
            get => _sNguoiLienHe;
            set => SetProperty(ref _sNguoiLienHe, value);
        }

        private string _sSoCmnd;
        [Validate("Số CMT/CCCD", DATA_TYPE.String, 50)]
        public string SSoCmnd
        {
            get => _sSoCmnd;
            set => SetProperty(ref _sSoCmnd, value);
        }

        private string _sNoiCapCmnd;
        [Validate("Nơi cấp CMT/CCCD", DATA_TYPE.String, 255)]
        public string SNoiCapCmnd
        {
            get => _sNoiCapCmnd;
            set => SetProperty(ref _sNoiCapCmnd, value);
        }

        private DateTime? _dNgayCapCmnd;
        [Validate("Ngày cấp CMT/CCCD", DATA_TYPE.Date)]
        public DateTime? DNgayCapCmnd
        {
            get => _dNgayCapCmnd;
            set => SetProperty(ref _dNgayCapCmnd, value);
        }
        public ObservableCollection<NhTtThanhToanChiTietModel> NhTtThanhToanChiTiets { get; set; }

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

        //Các trường bổ sung
        [Validate("Thanh toán theo", DATA_TYPE.Int, true)]
        public int? IThanhToanTheo { get; set; }
        public double? FTongPheDuyetVND { get; set; }
        public double? FTongPheDuyetUSD { get; set; }
        public double? FTongPheDuyetEUR { get; set; }
        public double? FTongPheDuyetNgoaiTeKhac { get; set; }
        public double? FTongDeNghiVND { get; set; }
        public double? FTongDeNghiUSD { get; set; }
        public double? FTongDeNghiEUR { get; set; }
        public double? FTongDeNghiNgoaiTeKhac { get; set; }
        public double? FLuyKeUSD { get; set; }
        public double? FLuyKeVND { get; set; }
        public double? FLuyKeEUR { get; set; }
        public double? FLuyKeNgoaiTeKhac { get; set; }
        public double? FTongPheDuyetBangSoUSD { get; set; }
        public double? FTongPheDuyetBangSoVND { get; set; }
        public double? FThuhoiTamUngPheDuyetBangSoUSD { get; set; }
        public double? FThuhoiTamUngPheDuyetBangSoVND { get; set; }
        public double? FTraDonViThuHuongPheDuyetBangSoUSD { get; set; }
        public double? FTraDonViThuHuongPheDuyetBangSoVND { get; set; }
        public string sTenDonVi { get; set; }
        public string sTenChuDauTu { get; set; }
        public double? FTongDuocDuyetVND { get; set; }
        public double? FTongDuocDuyetUSD { get; set; }
        public double? FTongDuocDuyetEUR { get; set; }
        public double? FTongDuocDuyetNgoaiTeKhac { get; set; }
        public double? FTongDuocCapKyTruocVND { get; set; }
        public double? FTongDuocCapKyTruocUSD { get; set; }
        public double? FTongDuocCapKyTruocEUR { get; set; }
        public double? FTongDuocCapKyTruocNgoaiTeKhac { get; set; }
        public double? FTongLuyKePheDuyetVND { get; set; }
        public double? FTongLuyKePheDuyetUSD { get; set; }
        public double? FTongLuyKePheDuyetEUR { get; set; }
        public double? FTongLuyKePheDuyetNgoaiTeKhac { get; set; }
        public Guid? IIdQuyetDinhKhacId { get; set; }
        public Guid? IIdQuyetDinhKhacChiPhiId { get; set; }
        public Guid? IIdHopDongChiPhiId { get; set; }
        public Guid? IIdDuAnChiPhiId { get; set; }
    }
}
