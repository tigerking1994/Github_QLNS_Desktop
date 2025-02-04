using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhQuyetDinhDamPhamModel : ModelBase
    {
        [Validate("Số quyết định", Utility.Enum.DATA_TYPE.String, 50, true)]
        public string SSoQuyetDinh { get; set; }

        [Validate("Ngày quyết định", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTaChiTietQuyetDinh { get; set; }

        //[Validate("Tỷ giá", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }

        [Validate("Nhiệm vụ chi", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdKhTongTheNhiemVuChiId { get; set; }

        //[Validate("Dự án", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDuAnId { get; set; }
        [Validate("Phương án nhập khẩu", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdPhuongAnNhapKhauId { get; set; }
        public int ILoaiQuyetDinh { get; set; }

        //[Validate("Đơn vị thực hiện", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDonViThucHien { get; set; }

        [Validate("Đơn vị", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDonViQuanLy { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdGocId { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public bool? BIsXoa { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdParentAdjustId { get; set; }
        public int? ILanDieuChinh { get; set; }

        private double? _fGiaTriNgoaiTeKhac;
        public double? FGiaTriNgoaiTeKhac
        {
            get => _fGiaTriNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriNgoaiTeKhac, value);
        }

        private double? _fGiaTriUsd;
        public double? FGiaTriUsd
        {
            get => _fGiaTriUsd;
            set => SetProperty(ref _fGiaTriUsd, value);
        }

        private double? _fGiaTriVnd;
        public double? FGiaTriVnd
        {
            get => _fGiaTriVnd;
            set => SetProperty(ref _fGiaTriVnd, value);
        }

        private double? _fGiaTriEur;
        public double? FGiaTriEur
        {
            get => _fGiaTriEur;
            set => SetProperty(ref _fGiaTriEur, value);
        }

        public int TotalFiles { get; set; }
        public string STiGiaText { get; set; }
        public string SLoaiQuyetDinhText { get; set; }
        public string SLoaiNhiemVuChiText { get; set; }
        public string DieuChinhTu { get; set; }
        public int? ILoai { get; set; }

        // Another properties
        public Guid? IIdKhTongTheId { get; set; }
        public string SPhuongAnNhapKhau { get; set; }
        public string STenChuongTrinh { get; set; }
        public string STenDonVi { get; set; }
        public string STenDuAn { get; set; }
        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set => SetProperty(ref _fTiGiaNhap, value);
        }
        private string _sNgoaiTeGoc;
        public string SNgoaiTeGoc
        {
            get => _sNgoaiTeGoc;
            set => SetProperty(ref _sNgoaiTeGoc, value);
        }
    }
}