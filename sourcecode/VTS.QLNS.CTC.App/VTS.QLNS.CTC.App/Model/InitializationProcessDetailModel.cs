using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class InitializationProcessDetailModel : DetailModelBase
    {
        private Guid? _idDb;
        public Guid? IdDb
        {
            get => _idDb;
            set => SetProperty(ref _idDb, value);
        }

        private Guid _iID_KhoiTaoDuLieuID;
        public Guid IID_KhoiTaoDuLieuID
        {
            get => _iID_KhoiTaoDuLieuID;
            set => SetProperty(ref _iID_KhoiTaoDuLieuID, value);
        }

        private Guid _iID_DuAnID;
        public Guid IID_DuAnID
        {
            get => _iID_DuAnID;
            set => SetProperty(ref _iID_DuAnID, value);
        }

        private int? _iCoQuanThanhToan;
        public int? ICoQuanThanhToan
        {
            get => _iCoQuanThanhToan;
            set => SetProperty(ref _iCoQuanThanhToan, value);
        }

        private int? _iIDNguonVonID;
        public int? IIDNguonVonID
        {
            get => _iIDNguonVonID;
            set => SetProperty(ref _iIDNguonVonID, value);
        }

        private Guid? _iIdLoaiCongTrinh;
        public Guid? IIdLoaiCongTrinh
        {
            get => _iIdLoaiCongTrinh;
            set => SetProperty(ref _iIdLoaiCongTrinh, value);
        }

        public string SMaLoaiCongTrinh { get; set; }

        private string _sMaDuAn;
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sMaDuAnParent;
        public string SMaDuAnParent
        {
            get => _sMaDuAnParent;
            set => SetProperty(ref _sMaDuAnParent, value);
        }

        public string TenDuAn { get; set; }

        private double? _fKHVN_VonBoTriHetNamTruoc;
        public double? FKHVN_VonBoTriHetNamTruoc
        {
            get => _fKHVN_VonBoTriHetNamTruoc;
            set => SetProperty(ref _fKHVN_VonBoTriHetNamTruoc, value);
        }

        private double? _fKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc;
        public double? FKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc
        {
            get => _fKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc;
            set => SetProperty(ref _fKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc, value);
        }

        private double? _fKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi;
        public double? FKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi
        {
            get => _fKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi;
            set => SetProperty(ref _fKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi, value);
        }

        private double? _fKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc;
        public double? FKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc
        {
            get => _fKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc;
            set => SetProperty(ref _fKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc, value);
        }

        private double? _fKHVN_KeHoachVonKeoDaiSangNam;
        public double? FKHVN_KeHoachVonKeoDaiSangNam
        {
            get => _fKHVN_KeHoachVonKeoDaiSangNam;
            set => SetProperty(ref _fKHVN_KeHoachVonKeoDaiSangNam, value);
        }

        private double? _fKHUT_VonBoTriHetNamTruoc;
        public double? FKHUT_VonBoTriHetNamTruoc
        {
            get => _fKHUT_VonBoTriHetNamTruoc;
            set => SetProperty(ref _fKHUT_VonBoTriHetNamTruoc, value);
        }

        private double? _fKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc;
        public double? FKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc
        {
            get => _fKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc;
            set => SetProperty(ref _fKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc, value);
        }

        private double? _fKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi;
        public double? FKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi
        {
            get => _fKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi;
            set => SetProperty(ref _fKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi, value);
        }

        private double? _fKHUT_KeHoachUngTruocKeoDaiSangNam;
        public double? FKHUT_KeHoachUngTruocKeoDaiSangNam
        {
            get => _fKHUT_KeHoachUngTruocKeoDaiSangNam;
            set => SetProperty(ref _fKHUT_KeHoachUngTruocKeoDaiSangNam, value);
        }

        private double? _fKHUT_KeHoachUngTruocChuaThuHoi;
        public double? FKHUT_KeHoachUngTruocChuaThuHoi
        {
            get => _fKHUT_KeHoachUngTruocChuaThuHoi;
            set => SetProperty(ref _fKHUT_KeHoachUngTruocChuaThuHoi, value);
        }

        private List<VdtKtKhoiTaoDuLieuChiTietThanhToanModel> _lstDetail;
        public List<VdtKtKhoiTaoDuLieuChiTietThanhToanModel> LstDetail
        {
            get => _lstDetail;
            set => SetProperty(ref _lstDetail, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> ItemsLoaiCongTrinh
        {
            get => _itemsLoaiCongTrinh;
            set => SetProperty(ref _itemsLoaiCongTrinh, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDuAn;
        public ObservableCollection<ComboboxItem> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }
    }
}
