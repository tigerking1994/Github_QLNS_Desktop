using System;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKhvKeHoach5NamModel : ModelBase
    {
        public Guid IIdDonViId { get; set; }
        public Guid? IIdNhomId { get; set; }
        public string IIdMaDonVi { get; set; }

        private int _iGiaiDoanTu;
        public int IGiaiDoanTu
        {
            get => _iGiaiDoanTu;
            set => SetProperty(ref _iGiaiDoanTu, value);
        }

        private int _iGiaiDoanDen;
        public int IGiaiDoanDen
        {
            get => _iGiaiDoanDen;
            set => SetProperty(ref _iGiaiDoanDen, value);
        }

        public Guid? IIdLoaiNguonVonId { get; set; }
        public Guid? IIdLoaiNganSachId { get; set; }
        public Guid? IIdKhoanNganSachId { get; set; }

        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }

        private Guid? _iIdParent;
        public Guid? IIdParentId
        {
            get => _iIdParent;
            set => SetProperty(ref _iIdParent, value);
        }

        private double? _fGiaTriKeHoach;
        public double? FGiaTriKeHoach
        {
            get => _fGiaTriKeHoach;
            set => SetProperty(ref _fGiaTriKeHoach, value);
        }

        public bool BActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BLaThayThe { get; set; }
        public string SLoaiDieuChinh { get; set; }
        public string STrangThai { get; set; }
        public Guid IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public Guid IIdTienTeId { get; set; }
        public double FTiGia { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateDelete { get; set; }
        public string SUserDelete { get; set; }
        public int? IIdNguonVonId { get; set; }
        public int? ILoai { get; set; }
        public int? NamLamViec { get; set; }
        public string GiaiDoan { get; set; }

        private string _nguonVon;
        public string NguonVon
        {
            get => _nguonVon;
            set => SetProperty(ref _nguonVon, value);
        }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        public string LoaiNganSach { get; set; }
        public string STenDonVi { get; set; }
        public string SoLanDC { get; set; }
        public string MoTaChiTiet { get; set; }
        public Guid? IIdKhthDeXuat { get; set; }
        public string SKeHoachDeXuat { get; set; }
        public string DieuChinhTu { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
        public string LoaiDuAnStr => LoaiDuAnEnum.Get(ILoai);
        public int TotalFiles { get; set; }

        public string HeaderPlan1 { get; set; }
        public string HeaderPlan2 { get; set; }
        public string HeaderPlan3 { get; set; }
        public string HeaderPlan4 { get; set; }
        public string HeaderPlan5 { get; set; }
        public string HeaderAfterYearPlan { get; set; }
        public string HeaderAfterYear { get; set; }
        public string HeaderAfterYearModified { get; set; }
        public string HeaderBeforYear { get; set; }
        public string HeaderBeforYearModified { get; set; }
        public string HeaderSumViaPeriod { get; set; }
        public string HeaderSumViaPeriodModified { get; set; }
        public string HeaderGroupVonBoTri { get; set; }
        public string HeaderVonTuNamDenNamSauDieuChinh { get; set; }
        public string HeaderVonBoTriSauDieuChinh { get; set; }
        public string HeaderVonDaGiao { get; set; }
        public string HeaderVonDaGiaoOrigin { get; set; }
        public string HeaderVonBoTriTuNamDenNam { get; set; }
        public string HeaderVonBoTriTuNamDenNamOrigin { get; set; }
        public string HeaderVonBoTriSauNam { get; set; }
        public string HeaderVonBoTriSauNamOrigin { get; set; }
        public bool IsViewDetail { get; set; }
    }
}
