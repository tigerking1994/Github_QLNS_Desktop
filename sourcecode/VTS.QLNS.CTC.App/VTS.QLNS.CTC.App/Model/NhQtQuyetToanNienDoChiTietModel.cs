using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public partial class NhQtQuyetToanNienDoChiTietModel : DetailModelBase
    {
        public Guid? IIdQuyetToanNienDoId { get; set; }
        public Guid? IIdKhttNhiemVuChiId { get; set; }

        public Guid? IID_DuAnID;

        private Guid? _iIdHopDongId;
        public Guid? IIdHopDongId
        {
            get => _iIdHopDongId;
            set => SetProperty(ref _iIdHopDongId, value);
        }

        private double? _fHopDongUsd; // 1
        public double? FHopDongUsd
        {
            get => _fHopDongUsd;
            set => SetProperty(ref _fHopDongUsd, value);
        }

        private double? _fHopDongVnd; // 2
        public double? FHopDongVnd
        {
            get => _fHopDongVnd;
            set => SetProperty(ref _fHopDongVnd, value);
        }

        private double? _fKeHoachTtcpUsd; // 3
        public double? FKeHoachTtcpUsd
        {
            get => _fKeHoachTtcpUsd;
            set => SetProperty(ref _fKeHoachTtcpUsd, value);
        }

        private double? _fKeHoachTtcpVnd;
        public double? FKeHoachTtcpVnd
        {
            get => _fKeHoachTtcpVnd;
            set => SetProperty(ref _fKeHoachTtcpVnd, value);
        }

        private double? _fKeHoachBqpUsd; // 4
        public double? FKeHoachBqpUsd
        {
            get => _fKeHoachBqpUsd;
            set => SetProperty(ref _fKeHoachBqpUsd, value);
        }

        private double? _fKeHoachBqpVnd;
        public double? FKeHoachBqpVnd
        {
            get => _fKeHoachBqpVnd;
            set => SetProperty(ref _fKeHoachBqpVnd, value);
        }

        private double? _fQtKinhPhiDuyetCacNamTruocUsd; // 5
        public double? FQtKinhPhiDuyetCacNamTruocUsd
        {
            get => _fQtKinhPhiDuyetCacNamTruocUsd;
            set => SetProperty(ref _fQtKinhPhiDuyetCacNamTruocUsd, value);
        }

        private double? _fQtKinhPhiDuyetCacNamTruocVnd; // 6
        public double? FQtKinhPhiDuyetCacNamTruocVnd
        {
            get => _fQtKinhPhiDuyetCacNamTruocVnd;
            set => SetProperty(ref _fQtKinhPhiDuyetCacNamTruocVnd, value);
        }

        private double? _fQtKinhPhiDuyetCacNamTruocEur;
        public double? FQtKinhPhiDuyetCacNamTruocEur
        {
            get => _fQtKinhPhiDuyetCacNamTruocEur;
            set => SetProperty(ref _fQtKinhPhiDuyetCacNamTruocEur, value);
        }

        private double? _fQtKinhPhiDuyetCacNamTruocNgoaiTeKhac;
        public double? FQtKinhPhiDuyetCacNamTruocNgoaiTeKhac
        {
            get => _fQtKinhPhiDuyetCacNamTruocNgoaiTeKhac;
            set => SetProperty(ref _fQtKinhPhiDuyetCacNamTruocNgoaiTeKhac, value);
        }

        //public double? FQtKinhPhiDuocCapTongSoUsd => (FQtKinhPhiDuocCapNamTruocChuyenSangUsd ?? 0) + (FQtKinhPhiDuocCapNamNayUsd ?? 0); // 7=9+11
        //public double? FQtKinhPhiDuocCapTongSoVnd => (FQtKinhPhiDuocCapNamTruocChuyenSangVnd ?? 0) + (FQtKinhPhiDuocCapNamNayVnd ?? 0); // 8=10+12
        //public double? FQtKinhPhiDuocCapTongSoEur => (FQtKinhPhiDuocCapNamTruocChuyenSangEur ?? 0) + (FQtKinhPhiDuocCapNamNayEur ?? 0);
        //public double? FQtKinhPhiDuocCapTongSoNgoaiTeKhac => (FQtKinhPhiDuocCapNamTruocChuyenSangNgoaiTeKhac ?? 0) + (FQtKinhPhiDuocCapNamNayNgoaiTeKhac ?? 0);
        public double? _fQtKinhPhiDuocCapTongSoUsd;
        public double? FQtKinhPhiDuocCapTongSoUsd
        {
            get => _fQtKinhPhiDuocCapTongSoUsd;
            set => SetProperty(ref _fQtKinhPhiDuocCapTongSoUsd, value);
        }
        public double? _fQtKinhPhiDuocCapTongSoVnd;
        public double? FQtKinhPhiDuocCapTongSoVnd
        {
            get => _fQtKinhPhiDuocCapTongSoVnd;
            set => SetProperty(ref _fQtKinhPhiDuocCapTongSoVnd, value);
        }
        public double? _fQtKinhPhiDuocCapTongSoEur;
        public double? FQtKinhPhiDuocCapTongSoEur
        {
            get => _fQtKinhPhiDuocCapTongSoEur;
            set => SetProperty(ref _fQtKinhPhiDuocCapTongSoEur, value);
        }
        public double? _fQtKinhPhiDuocCapTongSoNgoaiTeKhac;
        public double? FQtKinhPhiDuocCapTongSoNgoaiTeKhac
        {
            get => _fQtKinhPhiDuocCapTongSoNgoaiTeKhac;
            set => SetProperty(ref _fQtKinhPhiDuocCapTongSoNgoaiTeKhac, value);
        }

        private double? _fQtKinhPhiDuocCapNamTruocChuyenSangUsd; // 9
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangUsd
        {
            get => _fQtKinhPhiDuocCapNamTruocChuyenSangUsd;
            set => SetProperty(ref _fQtKinhPhiDuocCapNamTruocChuyenSangUsd, value);
        }

        private double? _fQtKinhPhiDuocCapNamTruocChuyenSangVnd; // 10
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangVnd
        {
            get => _fQtKinhPhiDuocCapNamTruocChuyenSangVnd;
            set => SetProperty(ref _fQtKinhPhiDuocCapNamTruocChuyenSangVnd, value);

        }

        private double? _fQtKinhPhiDuocCapNamTruocChuyenSangEur;
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangEur
        {
            get => _fQtKinhPhiDuocCapNamTruocChuyenSangEur;
            set => SetProperty(ref _fQtKinhPhiDuocCapNamTruocChuyenSangEur, value);

        }

        private double? _fQtKinhPhiDuocCapNamTruocChuyenSangNgoaiTeKhac;
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangNgoaiTeKhac
        {
            get => _fQtKinhPhiDuocCapNamTruocChuyenSangNgoaiTeKhac;
            set => SetProperty(ref _fQtKinhPhiDuocCapNamTruocChuyenSangNgoaiTeKhac, value);
        }

        private double? _fQtKinhPhiDuocCapNamNayUsd; // 11
        public double? FQtKinhPhiDuocCapNamNayUsd
        {
            get => _fQtKinhPhiDuocCapNamNayUsd;
            set => SetProperty(ref _fQtKinhPhiDuocCapNamNayUsd, value);
        }

        private double? _fQtKinhPhiDuocCapNamNayVnd; // 12
        public double? FQtKinhPhiDuocCapNamNayVnd
        {
            get => _fQtKinhPhiDuocCapNamNayVnd;
            set => SetProperty(ref _fQtKinhPhiDuocCapNamNayVnd, value);
        }

        private double? _fQtKinhPhiDuocCapNamNayEur;
        public double? FQtKinhPhiDuocCapNamNayEur
        {
            get => _fQtKinhPhiDuocCapNamNayEur;
            set => SetProperty(ref _fQtKinhPhiDuocCapNamNayEur, value);
        }

        private double? _fQtKinhPhiDuocCapNamNayNgoaiTeKhac;
        public double? FQtKinhPhiDuocCapNamNayNgoaiTeKhac
        {
            get => _fQtKinhPhiDuocCapNamNayNgoaiTeKhac;
            set => SetProperty(ref _fQtKinhPhiDuocCapNamNayNgoaiTeKhac, value);
        }

        private double? _fDeNghiQtNamNayUsd; // 13
        public double? FDeNghiQtNamNayUsd
        {
            get => _fDeNghiQtNamNayUsd;
            set
            {
                if (SetProperty(ref _fDeNghiQtNamNayUsd, value))
                {
                    OnPropertyChanged(nameof(FDeNghiQtNamNayUsd));
                }
            }
        }

        private double? _fDeNghiQtNamNayVnd; // 14
        public double? FDeNghiQtNamNayVnd
        {
            get => _fDeNghiQtNamNayVnd;
            set
            {
                if (SetProperty(ref _fDeNghiQtNamNayVnd, value))
                {
                    OnPropertyChanged(nameof(FDeNghiQtNamNayVnd));
                }
            }
        }

        private double? _fDeNghiQtNamNayEur;
        public double? FDeNghiQtNamNayEur
        {
            get => _fDeNghiQtNamNayEur;
            set
            {
                if (SetProperty(ref _fDeNghiQtNamNayEur, value))
                {
                    OnPropertyChanged(nameof(FDeNghiQtNamNayEur));
                }
            }
        }

        private double? _fDeNghiQtNamNayNgoaiTeKhac;
        public double? FDeNghiQtNamNayNgoaiTeKhac
        {
            get => _fDeNghiQtNamNayNgoaiTeKhac;
            set
            {
                if (SetProperty(ref _fDeNghiQtNamNayNgoaiTeKhac, value))
                {
                    OnPropertyChanged(nameof(FDeNghiQtNamNayNgoaiTeKhac));
                }
            }
        }

        private double? _fDeNghiChuyenNamSauUsd; // 15
        public double? FDeNghiChuyenNamSauUsd
        {
            get => _fDeNghiChuyenNamSauUsd;
            set
            {
                if (SetProperty(ref _fDeNghiChuyenNamSauUsd, value))
                {
                    OnPropertyChanged(nameof(FDeNghiChuyenNamSauUsd));
                }
            }
        }

        private double? _fDeNghiChuyenNamSauVnd; // 16
        public double? FDeNghiChuyenNamSauVnd
        {
            get => _fDeNghiChuyenNamSauVnd;
            set
            {
                if (SetProperty(ref _fDeNghiChuyenNamSauVnd, value))
                {
                    OnPropertyChanged(nameof(FDeNghiChuyenNamSauVnd));
                }
            }
        }

        private double? _fDeNghiChuyenNamSauEur;
        public double? FDeNghiChuyenNamSauEur
        {
            get => _fDeNghiChuyenNamSauEur;
            set
            {
                if (SetProperty(ref _fDeNghiChuyenNamSauEur, value))
                {
                    OnPropertyChanged(nameof(FThuaThieuKinhPhiTrongNamEur));
                }
            }
        }

        private double? _fDeNghiChuyenNamSauNgoaiTeKhac;
        public double? FDeNghiChuyenNamSauNgoaiTeKhac
        {
            get => _fDeNghiChuyenNamSauNgoaiTeKhac;
            set
            {
                if (SetProperty(ref _fDeNghiChuyenNamSauNgoaiTeKhac, value))
                {
                    OnPropertyChanged(nameof(FThuaThieuKinhPhiTrongNamNgoaiTeKhac));
                }
            }
        }
        //public double? FThuaThieuKinhPhiTrongNamUsd => FQtKinhPhiDuocCapTongSoUsd - FDeNghiQtNamNayUsd - FDeNghiChuyenNamSauUsd; // 17=7-13-15
        //public double? FThuaThieuKinhPhiTrongNamVnd => FQtKinhPhiDuocCapTongSoVnd - FDeNghiQtNamNayVnd - FDeNghiChuyenNamSauVnd; // 18=8-14-16
        //public double? FThuaThieuKinhPhiTrongNamEur => FQtKinhPhiDuocCapTongSoEur - FDeNghiQtNamNayEur - FDeNghiChuyenNamSauEur;
        //public double? FThuaThieuKinhPhiTrongNamNgoaiTeKhac => FQtKinhPhiDuocCapTongSoNgoaiTeKhac - FDeNghiQtNamNayNgoaiTeKhac - FDeNghiChuyenNamSauNgoaiTeKhac;
        public double? _fThuaThieuKinhPhiTrongNamUsd;
        public double? FThuaThieuKinhPhiTrongNamUsd
        {
            get => _fThuaThieuKinhPhiTrongNamUsd;
            //set => SetProperty(ref _fThuaNopNsnnUsd, value);
            set => SetProperty(ref _fThuaThieuKinhPhiTrongNamUsd, value);
        }
        public double? _fThuaThieuKinhPhiTrongNamVnd;
        public double? FThuaThieuKinhPhiTrongNamVnd
        {
            get => _fThuaThieuKinhPhiTrongNamVnd;
            //set => SetProperty(ref _fThuaNopNsnnUsd, value);
            set => SetProperty(ref _fThuaThieuKinhPhiTrongNamVnd, value);
        }
        public double? _fThuaThieuKinhPhiTrongNamEur;
        public double? FThuaThieuKinhPhiTrongNamEur
        {
            get => _fThuaThieuKinhPhiTrongNamEur;
            //set => SetProperty(ref _fThuaNopNsnnUsd, value);
            set => SetProperty(ref _fThuaThieuKinhPhiTrongNamEur, value);
        }
        public double? _fThuaThieuKinhPhiTrongNamNgoaiTeKhac;
        public double? FThuaThieuKinhPhiTrongNamNgoaiTeKhac
        {
            get => _fThuaThieuKinhPhiTrongNamNgoaiTeKhac;
            //set => SetProperty(ref _fThuaNopNsnnUsd, value);
            set => SetProperty(ref _fThuaThieuKinhPhiTrongNamNgoaiTeKhac, value);
        }
        private double? _fThuaNopNsnnUsd; // 19
        public double? FThuaNopNsnnUsd
        {
            get => _fThuaNopNsnnUsd;
            //set => SetProperty(ref _fThuaNopNsnnUsd, value);
            set
            {
                if (SetProperty(ref _fThuaNopNsnnUsd, value))
                {
                    OnPropertyChanged(nameof(FThuaNopNsnnUsd));
                }
            }
        }

        private double? _fThuaNopNsnnVnd; // 20
        public double? FThuaNopNsnnVnd
        {
            get => _fThuaNopNsnnVnd;
            //set => SetProperty(ref _fThuaNopNsnnVnd, value);
            set
            {
                if (SetProperty(ref _fThuaNopNsnnVnd, value))
                {
                    OnPropertyChanged(nameof(FThuaNopNsnnVnd));
                }
            }
        }

        private double? _fThuaNopNsnnEur;
        public double? FThuaNopNsnnEur
        {
            get => _fThuaNopNsnnEur;
            set => SetProperty(ref _fThuaNopNsnnEur, value);
        }

        private double? _fThuaNopNsnnNgoaiTeKhac;
        public double? FThuaNopNsnnNgoaiTeKhac
        {
            get => _fThuaNopNsnnNgoaiTeKhac;
            set => SetProperty(ref _fThuaNopNsnnNgoaiTeKhac, value);
        }

        private double? _fLuyKeKinhPhiDuocCapUsd; // 21
        public double? FLuyKeKinhPhiDuocCapUsd
        {
            get => _fLuyKeKinhPhiDuocCapUsd;
            set => SetProperty(ref _fLuyKeKinhPhiDuocCapUsd, value);
        }

        private double? _fLuyKeKinhPhiDuocCapVnd; // 22
        public double? FLuyKeKinhPhiDuocCapVnd
        {
            get => _fLuyKeKinhPhiDuocCapVnd;
            set => SetProperty(ref _fLuyKeKinhPhiDuocCapVnd, value);
        }

        private double? _fLuyKeKinhPhiDuocCapEur;
        public double? FLuyKeKinhPhiDuocCapEur
        {
            get => _fLuyKeKinhPhiDuocCapEur;
            set => SetProperty(ref _fLuyKeKinhPhiDuocCapEur, value);
        }

        private double? _fLuyKeKinhPhiDuocCapNgoaiTeKhac;
        public double? FLuyKeKinhPhiDuocCapNgoaiTeKhac
        {
            get => _fLuyKeKinhPhiDuocCapNgoaiTeKhac;
            set => SetProperty(ref _fLuyKeKinhPhiDuocCapNgoaiTeKhac, value);
        }

        private double? _fKeHoachChuaGiaiNganUsd; // 23
        public double? FKeHoachChuaGiaiNganUsd
        {
            get => _fKeHoachChuaGiaiNganUsd;
            set => SetProperty(ref _fKeHoachChuaGiaiNganUsd, value);
        }

        private double? _fKeHoachChuaGiaiNganVnd;
        public double? FKeHoachChuaGiaiNganVnd
        {
            get => _fKeHoachChuaGiaiNganVnd;
            set => SetProperty(ref _fKeHoachChuaGiaiNganVnd, value);
        }
        public Guid? IIdMucLucNganSachId { get; set; }
        public Guid? IIdMlnsId { get; set; }
        public Guid? IIdParentId { get; set; }
        private bool? _bIsSaveTongHop;
        public bool? BIsSaveTongHop
        {
            get => _bIsSaveTongHop;
            set => SetProperty(ref _bIsSaveTongHop, value);
        }

        // Another properties
        // Mục lục ngân sách
        private string _lns;
        public string LNS
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        public string TTM
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        public string NG
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLNS;
        public ObservableCollection<ComboboxItem> ItemsLNS
        {
            get => _itemsLNS;
            set => SetProperty(ref _itemsLNS, value);
        }

        private ObservableCollection<ComboboxItem> _itemsL;
        public ObservableCollection<ComboboxItem> ItemsL
        {
            get => _itemsL;
            set => SetProperty(ref _itemsL, value);
        }

        private ObservableCollection<ComboboxItem> _itemsK;
        public ObservableCollection<ComboboxItem> ItemsK
        {
            get => _itemsK;
            set => SetProperty(ref _itemsK, value);
        }

        private ObservableCollection<ComboboxItem> _itemsM;
        public ObservableCollection<ComboboxItem> ItemsM
        {
            get => _itemsM;
            set => SetProperty(ref _itemsM, value);
        }

        private ObservableCollection<ComboboxItem> _itemsTM;
        public ObservableCollection<ComboboxItem> ItemsTM
        {
            get => _itemsTM;
            set => SetProperty(ref _itemsTM, value);
        }

        private ObservableCollection<ComboboxItem> _itemsTTM;
        public ObservableCollection<ComboboxItem> ItemsTTM
        {
            get => _itemsTTM;
            set => SetProperty(ref _itemsTTM, value);
        }

        private ObservableCollection<ComboboxItem> _itemsNG;
        public ObservableCollection<ComboboxItem> ItemsNG
        {
            get => _itemsNG;
            set => SetProperty(ref _itemsNG, value);
        }
        public string STT { get; set; }
        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SLevel { get; set; }
        public string STenHopDong { get; set; }
        public string STenDuAn { get; set; }
        public string STenNhiemVuChi { get; set; }
        public string STenNoiDungChi { get; set; }
        public int? ILoaiNoiDungChi { get; set; }
        public Guid? IId_ThanhToan_ChiTietID { get; set; }
        public bool IsData { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public bool IsChiKhac { get; set; }

        public double? _fLuyKeKinhPhiDaGiaiNganTrongNamNayUsd;
        public double? FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd
        {
            get => _fLuyKeKinhPhiDaGiaiNganTrongNamNayUsd;
            set => SetProperty(ref _fLuyKeKinhPhiDaGiaiNganTrongNamNayUsd, value);
        }

        public double? fLuyKeKinhPhiDaGiaiNganTrongNamNayVnd;
        public double? FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd
        {
            get => fLuyKeKinhPhiDaGiaiNganTrongNamNayVnd;
            set => SetProperty(ref fLuyKeKinhPhiDaGiaiNganTrongNamNayVnd, value);
        }

        public double? _fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd;
        public double? FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd
        {
            get => _fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd;
            set => SetProperty(ref _fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd, value);
        }

        public double? _fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd;
        public double? FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd
        {
            get => _fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd;
            set => SetProperty(ref _fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd, value);
        }

        public double? _fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd;
        public double? FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd
        {
            get => _fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd;
            set => SetProperty(ref _fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd, value);
        }

        public double? _fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd;
        public double? FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd
        {
            get => _fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd;
            set => SetProperty(ref _fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd, value);
        }

        public double? _fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd;
        public double? FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd
        {
            get => _fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd;
            set => SetProperty(ref _fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd, value);
        }

        public double? _fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd;
        public double? FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd
        {
            get => _fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd;
            set => SetProperty(ref _fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd, value);
        }
    }
}
