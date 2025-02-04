using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Model.Import;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaHopDongModel : CurrencyDetailModelBase
    {
        [Validate("Số hợp đồng", Utility.Enum.DATA_TYPE.String, 100, true)]
        [DisplayName("Mã hợp đồng")]
        [DisplayDetailInfo("Mã hợp đồng")]
        public string SSoHopDong { get; set; }

        [DisplayName("Tên hợp đồng")]
        [DisplayDetailInfo("Tên hợp đồng")]
        [Validate("Tên hợp đồng", Utility.Enum.DATA_TYPE.String, 300, true)]
        public string STenHopDong { get; set; }

        [DisplayName("Hình thức hợp đồng")]
        [DisplayDetailInfo("Hình thức hợp đồng")]
        [Validate("Hình thức hợp đồng", Utility.Enum.DATA_TYPE.String, 300, false)]
        public string SHinhThucHopDong { get; set; }
        public Guid? IIdParentAdjustId { get; set; }

        public Guid? IIdLoaiHopDongId { get; set; }

        public Guid? IIdCacQuyetDinhId { get; set; }
        public Guid? IIdKhTongTheNhiemVuChiId { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIHopDongNguonVonId { get; set; }
        public Guid? IIdNhaThauThucHienId { get; set; }

        public Guid? IIdTiGiaId { get; set; }
        public Guid? IIdKeHoachDatHangId { get; set; }

        public Guid? IIdDuAnId { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string SLoaiHopDong { get; set; }
        private string _sTenGoiThau;
        public string STenGoiThau
        {
            get => _sTenGoiThau;
            set => SetProperty(ref _sTenGoiThau, value);
        }

        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set => SetProperty(ref _iIdParentId, value);
        }

        private bool _bIsActive;
        public bool BIsActive
        {
            get => _bIsActive;
            set => SetProperty(ref _bIsActive, value);
        }

        public bool BIsGoc { get; set; }

        private bool _bIsKhoa;
        public bool BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }

        public int ILanDieuChinh { get; set; }
        public int? ILoai { get; set; }
        public int? IThuocMenu { get; set; }
        public string DieuChinhTu { get; set; }
        public DateTime? DNgayHopDong { get; set; }
        public DateTime? DKhoiCongDuKien { get; set; }
        public DateTime? DKetThucDuKien { get; set; }

        public string SoKHTTBQP { get; set; }
        public string DonVi { get; set; }
        public string STenChuongTrinh { get; set; }
        public string SMaKeHoachDatHang { get; set; }
        public string DuAn { get; set; }
        public string GoiThau { get; set; }

        //private double? _fGiaTriUSD;
        //public double? FGiaTriUSD
        //{
        //    get => _fGiaTriUSD;
        //    set => SetProperty(ref _fGiaTriUSD, value);
        //}
        //private double? _fGiaTriVND;
        //public double? FGiaTriVND
        //{
        //    get => _fGiaTriVND;
        //    set => SetProperty(ref _fGiaTriVND, value);
        //}
        //private double? _fGiaTriEUR;
        //public double? FGiaTriEUR
        //{
        //    get => _fGiaTriEUR;
        //    set => SetProperty(ref _fGiaTriEUR, value);
        //}
        //private double? _fGiaTriNgoaiTeKhac;
        //public double? FGiaTriNgoaiTeKhac
        //{
        //    get => _fGiaTriNgoaiTeKhac;
        //    set => SetProperty(ref _fGiaTriNgoaiTeKhac, value);
        //}
        public string SMaNgoaiTeKhac { get; set; }

        private double? _fGiaTriHopDongUSD;
        public double? FGiaTriHopDongUSD
        {
            get => _fGiaTriHopDongUSD;
            set => SetProperty(ref _fGiaTriHopDongUSD, value);
        }
        private double? _fGiaTriHopDongNgoiTeKhac;
        public double? FGiaTriHopDongNgoaiTeKhac
        {
            get => _fGiaTriHopDongNgoiTeKhac;
            set => SetProperty(ref _fGiaTriHopDongNgoiTeKhac, value);
        }
        private double? _fGiaTriHopDongEUR;
        public double? FGiaTriHopDongEUR
        {
            get => _fGiaTriHopDongEUR;
            set => SetProperty(ref _fGiaTriHopDongEUR, value);
        }
        private double? _fGiaTriHopDongVND;
        public double? FGiaTriHopDongVND
        {
            get => _fGiaTriHopDongVND;
            set => SetProperty(ref _fGiaTriHopDongVND, value);
        }

        private double? _fGiaTriUSDTotalHopDongNguonVon;
        public double? FGiaTriUSDTotalHopDongNguonVon
        {
            get => _fGiaTriUSDTotalHopDongNguonVon;
            set => SetProperty(ref _fGiaTriUSDTotalHopDongNguonVon, value);
        }
        private double? _fGiaTriEURTotalHopDongNguonVon;
        public double? FGiaTriEURTotalHopDongNguonVon
        {
            get => _fGiaTriEURTotalHopDongNguonVon;
            set => SetProperty(ref _fGiaTriEURTotalHopDongNguonVon, value);
        }
        private double? _fGiaTriVNDTotalHopDongNguonVon;
        public double? FGiaTriVNDTotalHopDongNguonVon
        {
            get => _fGiaTriVNDTotalHopDongNguonVon;
            set => SetProperty(ref _fGiaTriVNDTotalHopDongNguonVon, value);
        }
        private double? _fGiaTriNgoaiTeKhacTotalHopDongNguonVon;
        public double? FGiaTriNgoaiTeKhacTotalHopDongNguonVon
        {
            get => _fGiaTriNgoaiTeKhacTotalHopDongNguonVon;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacTotalHopDongNguonVon, value);
        }

        private double? _fGiaTriUSDTotalHopDongChiPhi;
        public double? FGiaTriUSDTotalHopDongChiPhi
        {
            get => _fGiaTriUSDTotalHopDongChiPhi;
            set => SetProperty(ref _fGiaTriUSDTotalHopDongChiPhi, value);
        }
        private double? _fGiaTriEURTotalHopDongChiPhi;
        public double? FGiaTriEURTotalHopDongChiPhi
        {
            get => _fGiaTriEURTotalHopDongChiPhi;
            set => SetProperty(ref _fGiaTriEURTotalHopDongChiPhi, value);
        }
        private double? _fGiaTriVNDTotalHopDongChiPhi;
        public double? FGiaTriVNDTotalHopDongChiPhi
        {
            get => _fGiaTriVNDTotalHopDongChiPhi;
            set => SetProperty(ref _fGiaTriVNDTotalHopDongChiPhi, value);
        }
        private double? _fGiaTriNgoaiTeKhacTotalHopDongChiPhi;
        public double? FGiaTriNgoaiTeKhacTotalHopDongChiPhi
        {
            get => _fGiaTriNgoaiTeKhacTotalHopDongChiPhi;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacTotalHopDongChiPhi, value);
        }

        private double? _fGiaTriUSDTotalHopDongConLai;
        public double? FGiaTriUSDTotalHopDongConLai
        {
            get => _fGiaTriUSDTotalHopDongConLai;
            set => SetProperty(ref _fGiaTriUSDTotalHopDongConLai, value);
        }
        private double? _fGiaTriEURTotalHopDongConLai;
        public double? FGiaTriEURTotalHopDongConLai
        {
            get => _fGiaTriEURTotalHopDongConLai;
            set => SetProperty(ref _fGiaTriEURTotalHopDongConLai, value);
        }
        private double? _fGiaTriVNDTotalHopDongConLai;
        public double? FGiaTriVNDTotalHopDongConLai
        {
            get => _fGiaTriVNDTotalHopDongConLai;
            set => SetProperty(ref _fGiaTriVNDTotalHopDongConLai, value);
        }
        private double? _fGiaTriNgoaiTeKhacTotalHopDongConLai;
        public double? FGiaTriNgoaiTeKhacTotalHopDongConLai
        {
            get => _fGiaTriNgoaiTeKhacTotalHopDongConLai;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacTotalHopDongConLai, value);
        }
        public string SMaTienTeUsdVnd1 { get; set; }
        public string SMaTienTeUsdVnd2 { get; set; }
        public string SMaTienTeEuroUsd1 { get; set; }
        public string SMaTienTeEuroUsd2 { get; set; }
        public string SMaTienTeNgoaiTeKhacUsd1 { get; set; }
        public string SMaTienTeNgoaiTeKhacUsd2 { get; set; }
        public int TotalFiles { get; set; }
        // anothor properties
        public string SLoaiNhiemVuChi { get; set; }
        public string STenDuAn { get; set; }
        public string STenDonVi { get; set; }
        public string SSoQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public Guid? IIdDonViId { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }

        public int? IThoiGianThucHien { get; set; }
        public Guid? IIdKhTongTheId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }

        public string TenHopDongDisplay => string.Concat(SSoHopDong, " - ", STenHopDong);
        private ObservableCollection<NhDaHopDongNguonVonModel> _hopDongNguonVons;
        public ObservableCollection<NhDaHopDongNguonVonModel> HopDongNguonVons
        {
            get => _hopDongNguonVons;
            set => SetProperty(ref _hopDongNguonVons, value);
        }

        private ObservableCollection<NhDaHopDongHangMucModel> _hopDongHangMucs;
        public ObservableCollection<NhDaHopDongHangMucModel> HopDongHangMucs
        {
            get => _hopDongHangMucs;
            set => SetProperty(ref _hopDongHangMucs, value);
        }

        private ObservableCollection<NhDaHopDongChiPhiModel> _hopDongChiPhis;
        public ObservableCollection<NhDaHopDongChiPhiModel> HopDongChiPhis
        {
            get => _hopDongChiPhis;
            set => SetProperty(ref _hopDongChiPhis, value);
        }

        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }

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
        public string STT { get; set; }
        public string SMaLoaiHopDong { get; set; }
        public string SMaNhaThauThucHien { get; set; }
        public string SMaDuAn { get; set; }
    }
}