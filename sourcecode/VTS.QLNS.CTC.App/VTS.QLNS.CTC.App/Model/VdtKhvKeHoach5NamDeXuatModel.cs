using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKhvKeHoach5NamDeXuatModel : ModelBase
    {
        public Guid IIdDonViId { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        
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

        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set => SetProperty(ref _iIdParentId, value);
        }
        
        public bool BActive { get; set; }
        public bool? BIsGoc { get; set; }
        public string STrangThai { get; set; }
        public double? FGiaTriKeHoach { get; set; }
        public int? ILoai { get; set; }
        public int? NamLamViec { get; set; }
        public string MoTaChiTiet { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateDelete { get; set; }
        public string SUserDelete { get; set; }
        public string SoLanDC { get; set; }
        public string DieuChinhTu { get; set; }
        public string LoaiDuAnStr => LoaiDuAnEnum.Get(ILoai);
        public string DisplayItem => string.Format("{0} - {1} - {2}", SSoQuyetDinh, IGiaiDoanTu, IGiaiDoanDen);
        public string GiaiDoan { get; set; }
        public int TotalFiles { get; set; }
        public string STongHop { get; set; }
        public Guid? IIdTongHop { get; set; }

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
        public List<Guid> LstIdDuAn { get; set; }
        public string HeaderPlan1 { get; set; }
        public string HeaderPlan2 { get; set; }
        public string HeaderPlan3 { get; set; }
        public string HeaderPlan4 { get; set; }
        public string HeaderPlan5 { get; set; }
        public string HeaderAfterYearPlan { get; set; }
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Header3 { get; set; }
        public string Header4 { get; set; }
        public string Header5 { get; set; }
        public string HeaderAfterYear { get; set; }
        public string HeaderModified1 { get; set; }
        public string HeaderModified2 { get; set; }
        public string HeaderModified3 { get; set; }
        public string HeaderModified4 { get; set; }
        public string HeaderModified5 { get; set; }
        public string HeaderAfterYearModified { get; set; }
        public string HeaderBeforYear { get; set; }
        public string HeaderBeforYearModified { get; set; }
        public string HeaderSumViaPeriod { get; set; }
        public string HeaderSumViaPeriodModified { get; set; }
        public string HeaderGroupVonBoTri { get; set; }
        public string HeaderVonNSQP { get; set; }
        public string HeaderVonNSQPLuyKe { get; set; }
        public bool IsViewDetail { get; set; }
    }
}
