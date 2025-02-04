using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.Explanation;
using System.Windows.Forms;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.Explanation
{
    public class QuyetToanThuGiaiThichSoLieuViewModel : DetailViewModelBase<BhQttBHXHModel, BhQttBHXHChiTietGiaiThichModel>
    {
        private IMapper _mapper;
        private IQttBHXHChiTietGiaiThichService _chungTuChiTietGiaiThichService;
        private IQttBHXHService _chungTuService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;

        public override string Name => "Quyết toán - Chứng từ chi tiết - Giải thích số liệu";
        public override Type ContentType => typeof(GiaiThichSoLieu);

        public BhQttBHXHModel SettlementVoucher;
        public Guid ExplainId;
        public string AgencyId;
        public int QuarterYear;
        public int QuarterYearType;
        public string QuarterYearDescription;
        public int STT;
        private bool _isCreate;
        public bool IsEnableSelfPayTab { get; set; }

        private SumQttBHXHChiTietGiaiThichModel _sumGiaiThichTruyThuModel;
        public SumQttBHXHChiTietGiaiThichModel SumGiaiThichTruyThuModel
        {
            get => _sumGiaiThichTruyThuModel;
            set => SetProperty(ref _sumGiaiThichTruyThuModel, value);
        }

        private SumQttBHXHChiTietGiaiThichSoSanhModel _sumGiaiThichSoSanhModel;
        public SumQttBHXHChiTietGiaiThichSoSanhModel SumGiaiThichSoSanhModel
        {
            get => _sumGiaiThichSoSanhModel;
            set => SetProperty(ref _sumGiaiThichSoSanhModel, value);
        }

        private ObservableCollection<BhQttBHXHChiTietGiaiThichModel> _giaiThichSoSanhs;
        public ObservableCollection<BhQttBHXHChiTietGiaiThichModel> GiaiThichSoSanhs
        {
            get => _giaiThichSoSanhs;
            set => SetProperty(ref _giaiThichSoSanhs, value);
        }

        private SumQttBHXHChiTietGiaiThichGiamDongModel _sumGiaiThichGiamDongModel;
        public SumQttBHXHChiTietGiaiThichGiamDongModel SumGiaiThichGiamDongModel
        {
            get => _sumGiaiThichGiamDongModel;
            set => SetProperty(ref _sumGiaiThichGiamDongModel, value);
        }

        private ObservableCollection<BhQttBHXHChiTietGiaiThichModel> _giaiThichGiamDongs;
        public ObservableCollection<BhQttBHXHChiTietGiaiThichModel> GiaiThichGiamDongs
        {
            get => _giaiThichGiamDongs;
            set => SetProperty(ref _giaiThichGiamDongs, value);
        }

        private BhQttBHXHChiTietGiaiThichModel _selectedGiaiThichSoSanh;
        public BhQttBHXHChiTietGiaiThichModel SelectedGiaiThichSoSanh
        {
            get => _selectedGiaiThichSoSanh;
            set => SetProperty(ref _selectedGiaiThichSoSanh, value);
        }

        private BhQttBHXHChiTietGiaiThichModel _selectedGiaiThichGiamDong;
        public BhQttBHXHChiTietGiaiThichModel SelectedGiaiThichGiamDong
        {
            get => _selectedGiaiThichGiamDong;
            set => SetProperty(ref _selectedGiaiThichGiamDong, value);
        }

        private BhQttBHXHChiTietGiaiThichModel _detailExplainModel;
        public BhQttBHXHChiTietGiaiThichModel DetailExplainModel
        {
            get => _detailExplainModel;
            set => SetProperty(ref _detailExplainModel, value);
        }

        private int _selectedTab;
        public int SelectedTab
        {
            get => _selectedTab;
            set
            {
                SetProperty(ref _selectedTab, value);
            }
        }

        private BhQttBHXHChiTietGiaiThichModel _selectedItem;
        public BhQttBHXHChiTietGiaiThichModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public Visibility IsVisibilityExplain => SettlementVoucher.IQuyNamLoai.Equals(2) ? Visibility.Visible : Visibility.Collapsed;

        public RelayCommand SaveAndUpdateCommand { get; }
        public RelayCommand AddRowCommand { get; }
        public RelayCommand DeleteRowCommand { get; }
        public RelayCommand ReloadDataCommand { get; }
        public RelayCommand DeleteAllCommand { get; }
        public RelayCommand CloseCommand { get; }

        public QuyetToanThuGiaiThichSoLieuViewModel(IMapper mapper,
            IQttBHXHChiTietGiaiThichService chungTuChiTietGiaiThichService,
            IQttBHXHService chungTuService,
            ISessionService sessionService)
        {
            _mapper = mapper;
            _chungTuChiTietGiaiThichService = chungTuChiTietGiaiThichService;
            _chungTuService = chungTuService;
            _sessionService = sessionService;

            DeleteAllCommand = new RelayCommand(obj => OnDeleteAll());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
        }

        public override void Init()
        {
            base.Init();
            _selectedTab = 0;
            _sessionInfo = _sessionService.Current;
            LoadDataTruyThu();
            LoadDataSoSanh();
            LoadDataGiamDong();
        }

        //load dữ liệu giải thích số
        private void LoadDataTruyThu()
        {
            BhQttBHXHChiTietGiaiThichCriteria condition = new BhQttBHXHChiTietGiaiThichCriteria
            {
                VoucherId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id,
                ExplainId = SettlementVoucher == null ? ExplainId : SettlementVoucher.Id,
                AgencyId = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIDMaDonVi,
                YearOfWork = _sessionInfo.YearOfWork,
                ExplainType = (int)ExplainType.GIAITHICH_TRUYTHU,
                VoucherType = SettlementVoucher.IQuyNamLoai
            };
            //var existChiTietGTTT = _chungTuChiTietGiaiThichService.FindByCondition(condition);

            var chiTietGiaiThichTruyThu = _chungTuChiTietGiaiThichService.GetChiTietGiaiThichTruyThu(condition).ToList();
            Items = _mapper.Map<ObservableCollection<BhQttBHXHChiTietGiaiThichModel>>(chiTietGiaiThichTruyThu);
            foreach (var item in Items)
            {
                item.IsFilter = true;
                if (!item.BHangCha)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FLuongChinh) || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FPCChucVu)
                            || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FPCTNNghe) || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FPCTNVuotKhung)
                            || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FNghiOm) || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FHSBL)
                            || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FTruyThuBHXHNLD) || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FTruyThuBHXHNSD)
                            || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FTruyThuBHYTNLD) || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FTruyThuBHYTNSD)
                            || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FTruyThuBHTNNLD) || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FTruyThuBHTNNSD))
                        {
                            BhQttBHXHChiTietGiaiThichModel chitiet = (BhQttBHXHChiTietGiaiThichModel)sender;
                            item.IsModified = true;
                            chitiet.IsModified = true;
                            CalculateData();
                        }
                    };
                }
            }
            CalculateData();
        }

        private void LoadDataSoSanh()
        {
            BhQttBHXHChiTietGiaiThichCriteria condition = new BhQttBHXHChiTietGiaiThichCriteria
            {
                VoucherId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id,
                ExplainId = SettlementVoucher == null ? ExplainId : SettlementVoucher.Id,
                AgencyId = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIDMaDonVi,
                YearOfWork = _sessionInfo.YearOfWork,
                ExplainType = (int)ExplainType.GIAITHICH_TONGHOP_SOSANH
            };
            var existingItems = _chungTuChiTietGiaiThichService.GetChiTietGiaiThichTongHopSoSanhTonTai(condition).ToList();
            var chiTietGiaiThichSoSanh = _chungTuChiTietGiaiThichService.GetChiTietGiaiThichTongHopSoSanh(condition).ToList();
            GiaiThichSoSanhs = _mapper.Map<ObservableCollection<BhQttBHXHChiTietGiaiThichModel>>(chiTietGiaiThichSoSanh);
            UpdateCompareTotal();
            STT = 0;
            foreach (var item in GiaiThichSoSanhs)
            {
                ++STT;
                item.ISTT = STT;
                item.IsFilter = true;
                var itemCompare = existingItems.FirstOrDefault(x => x.Id.Equals(item.Id));
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FSoPhaiThuNop) || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FSoDaNopTrongNam)
                            || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FSoDaNopSau3112))
                    {
                        BhQttBHXHChiTietGiaiThichModel chitiet = (BhQttBHXHChiTietGiaiThichModel)sender;
                        item.IsModified = true;
                        chitiet.IsModified = true;
                        UpdateCompareTotal();
                    }
                };

                if ((itemCompare != null && item.FSoPhaiThuNop.Equals(itemCompare.FSoPhaiThuNop)) || item.FSoPhaiThuNop.GetValueOrDefault() == 0)
                    item.IsModified = false;
                else item.IsModified = true;
            }
        }

        private void LoadDataGiamDong()
        {
            BhQttBHXHChiTietGiaiThichCriteria condition = new BhQttBHXHChiTietGiaiThichCriteria
            {
                VoucherId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id,
                ExplainId = SettlementVoucher == null ? ExplainId : SettlementVoucher.Id,
                AgencyId = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIDMaDonVi,
                YearOfWork = _sessionInfo.YearOfWork,
                ExplainType = (int)ExplainType.GIAITHICH_GIAMDONG
            };

            var chiTietGiaiThichSoSanh = _chungTuChiTietGiaiThichService.GetChiTietGiaiThichTongHopSoSanh(condition).ToList();
            GiaiThichGiamDongs = _mapper.Map<ObservableCollection<BhQttBHXHChiTietGiaiThichModel>>(chiTietGiaiThichSoSanh);
            UpdateGiamDongTotal();
            STT = 0;
            foreach (var item in GiaiThichGiamDongs)
            {
                ++STT;
                item.ISTT = STT;
                //item.DTuNgay = item?.DTuNgay ?? DateTime.Now;
                //item.DDenNgay = item?.DDenNgay ?? DateTime.Now;
                item.IsFilter = true;
                item.PropertyChanged += (sender, args) =>
                {
                    {
                        BhQttBHXHChiTietGiaiThichModel chitiet = (BhQttBHXHChiTietGiaiThichModel)sender;
                        item.IsModified = true;
                        chitiet.IsModified = true;
                        UpdateGiamDongTotal();
                    }
                };
            }
        }

        public override void OnSave()
        {
            //Giải thích truy thu
            if (_selectedTab == 0)
            {
                Func<BhQttBHXHChiTietGiaiThichModel, bool> isAdd = x => x.IsModified && x.Id == Guid.Empty && !x.BHangCha;
                Func<BhQttBHXHChiTietGiaiThichModel, bool> isUpdate = x => x.IsModified && x.Id != Guid.Empty && !x.BHangCha;

                var detailsAdd = Items.Where(isAdd).ToList();
                var detailsUpdate = Items.Where(isUpdate).ToList();

                //Trường hợp tạo mới
                if (detailsAdd.Count > 0)
                {
                    var addItems = new List<BhQttBHXHChiTietGiaiThich>();
                    _mapper.Map(detailsAdd, addItems);

                    foreach (var item in addItems)
                    {
                        item.Id = Guid.NewGuid();
                        item.QttBHXHId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id;
                        item.IIDMaDonVi = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIDMaDonVi;
                        item.INamLamViec = _sessionInfo.YearOfWork;
                        item.IQuyNam = SettlementVoucher == null ? QuarterYear : Convert.ToInt32(SettlementVoucher.IQuyNam);
                        item.IQuyNamLoai = SettlementVoucher == null ? QuarterYearType : SettlementVoucher.IQuyNamLoai;
                        item.SQuyNamMoTa = SettlementVoucher == null ? QuarterYearDescription : SettlementVoucher.SQuyNamMoTa;
                        item.SNguoiTao = _sessionInfo.Principal;
                        item.DNgayTao = DateTime.Now;
                        item.ILoaiGiaiThich = (int)ExplainType.GIAITHICH_TRUYTHU;
                    }
                    _chungTuChiTietGiaiThichService.AddRange(addItems);
                    _isCreate = false;
                    Items.Where(isAdd).Select(x =>
                    {
                        x.IsModified = false;
                        x.IsAdd = false;
                        return x;
                    }).ToList();
                }
                //Trường hợp cập nhật
                if (detailsUpdate.Count > 0)
                {
                    var updateItems = new List<BhQttBHXHChiTietGiaiThich>();
                    _mapper.Map(detailsUpdate, updateItems);

                    updateItems = detailsUpdate.Select(x => new BhQttBHXHChiTietGiaiThich
                    {
                        Id = x.Id,
                        SNguoiTao = x.SNguoiTao,
                        DNgayTao = x.DNgayTao,
                        DNgaySua = DateTime.Now,
                        SNguoiSua = _sessionInfo.Principal,
                        QttBHXHId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id,
                        INamLamViec = _sessionService.Current.YearOfWork,
                        IQuyNam = SettlementVoucher == null ? QuarterYear : Convert.ToInt32(SettlementVoucher.IQuyNam),
                        IQuyNamLoai = SettlementVoucher == null ? QuarterYearType : SettlementVoucher.IQuyNamLoai,
                        SQuyNamMoTa = SettlementVoucher == null ? QuarterYearDescription : SettlementVoucher.SQuyNamMoTa,
                        SLNS = x.SLNS,
                        IIDMLNS = x.IIDMLNS,
                        BHangCha = x.BHangCha,
                        IIDMaDonVi = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIDMaDonVi,
                        SNoiDung = x.SNoiDung,
                        SXauNoiMa = x.SXauNoiMa,
                        SL = x.SL,
                        SK = x.SK,
                        SM = x.SM,
                        STM = x.STM,

                        ILoaiGiaiThich = (int)ExplainType.GIAITHICH_TRUYTHU,
                        FTruyThuBHXHNLD = x.FTruyThuBHXHNLD,
                        FTruyThuBHXHNSD = x.FTruyThuBHXHNSD,
                        FTruyThuBHXHTongCong = x.FTruyThuBHXHTongCong,
                        FTruyThuBHYTNLD = x.FTruyThuBHYTNLD,
                        FTruyThuBHYTNSD = x.FTruyThuBHYTNSD,
                        FTruyThuBHYTTongCong = x.FTruyThuBHYTTongCong,
                        FTruyThuBHTNNLD = x.FTruyThuBHTNNLD,
                        FTruyThuBHTNNSD = x.FTruyThuBHTNNSD,
                        FTruyThuBHTNTongCong = x.FTruyThuBHTNTongCong,
                        FTongTruyThuBHXH = x.FTruyThuBHXHTongCong + x.FTruyThuBHYTTongCong + x.FTruyThuBHTNTongCong,
                        FLuongChinh = x.FLuongChinh,
                        FPCChucVu = x.FPCChucVu,
                        FPCTNNghe = x.FPCTNNghe,
                        FPCTNVuotKhung = x.FPCTNVuotKhung,
                        FNghiOm = x.FNghiOm,
                        FHSBL = x.FHSBL
                    }).ToList();

                    foreach (var item in updateItems)
                    {
                        _chungTuChiTietGiaiThichService.Update(item);
                    }
                    Items.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
                }
            }
            //Giải thích tổng hợp so sánh
            else if (_selectedTab == 1)
            {
                Func<BhQttBHXHChiTietGiaiThichModel, bool> isGTSSAdd = x => x.IsModified && x.Id == Guid.Empty;
                Func<BhQttBHXHChiTietGiaiThichModel, bool> isGTSSUpdate = x => x.IsModified && x.Id != Guid.Empty;

                var detailsAdd = GiaiThichSoSanhs.Where(isGTSSAdd).ToList();
                var detailsUpdate = GiaiThichSoSanhs.Where(isGTSSUpdate).ToList();

                //Trường hợp tạo mới
                if (detailsAdd.Count > 0)
                {
                    var addItems = new List<BhQttBHXHChiTietGiaiThich>();
                    _mapper.Map(detailsAdd, addItems);

                    foreach (var item in addItems)
                    {
                        item.Id = Guid.NewGuid();
                        item.QttBHXHId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id;
                        item.IIDMaDonVi = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIDMaDonVi;
                        item.INamLamViec = _sessionInfo.YearOfWork;
                        item.IQuyNam = SettlementVoucher == null ? QuarterYear : Convert.ToInt32(SettlementVoucher.IQuyNam);
                        item.IQuyNamLoai = SettlementVoucher == null ? QuarterYearType : SettlementVoucher.IQuyNamLoai;
                        item.SQuyNamMoTa = SettlementVoucher == null ? QuarterYearDescription : SettlementVoucher.SQuyNamMoTa;
                        item.SNguoiTao = _sessionInfo.Principal;
                        item.DNgayTao = DateTime.Now;
                        item.ILoaiGiaiThich = (int)ExplainType.GIAITHICH_TONGHOP_SOSANH;
                    }
                    _chungTuChiTietGiaiThichService.AddRange(addItems);
                    _isCreate = false;
                    GiaiThichSoSanhs.Where(isGTSSAdd).Select(x =>
                    {
                        x.IsModified = false;
                        x.IsAdd = false;
                        return x;
                    }).ToList();
                }
                //Trường hợp cập nhật
                if (detailsUpdate.Count > 0)
                {
                    var updateItems = new List<BhQttBHXHChiTietGiaiThich>();
                    _mapper.Map(detailsUpdate, updateItems);

                    updateItems = detailsUpdate.Select(x => new BhQttBHXHChiTietGiaiThich
                    {
                        Id = x.Id,
                        SNguoiTao = x.SNguoiTao,
                        DNgayTao = x.DNgayTao,
                        DNgaySua = DateTime.Now,
                        SNguoiSua = _sessionInfo.Principal,
                        QttBHXHId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id,
                        INamLamViec = _sessionService.Current.YearOfWork,
                        IQuyNam = SettlementVoucher == null ? QuarterYear : Convert.ToInt32(SettlementVoucher.IQuyNam),
                        IQuyNamLoai = SettlementVoucher == null ? QuarterYearType : SettlementVoucher.IQuyNamLoai,
                        SQuyNamMoTa = SettlementVoucher == null ? QuarterYearDescription : SettlementVoucher.SQuyNamMoTa,
                        SLNS = x.SLNS,
                        IIDMLNS = x.IIDMLNS,
                        BHangCha = x.BHangCha,
                        IIDMaDonVi = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIDMaDonVi,
                        ILoaiGiaiThich = (int)ExplainType.GIAITHICH_TONGHOP_SOSANH,
                        SNoiDung = x.SNoiDung,
                        SXauNoiMa = x.SXauNoiMa,
                        SL = x.SL,
                        SK = x.SK,
                        SM = x.SM,
                        STM = x.STM,

                        FSoPhaiThuNop = x.FSoPhaiThuNop,
                        FSoDaNopTrongNam = x.FSoDaNopTrongNam,
                        FSoDaNopSau3112 = x.FSoDaNopSau3112,
                        FTongSoDaNop = x.FSoDaNopTrongNam + x.FSoDaNopSau3112,
                        FSoConPhaiNop = x.FSoConPhaiNop,

                    }).ToList();

                    foreach (var item in updateItems)
                    {
                        _chungTuChiTietGiaiThichService.Update(item);
                    }
                    GiaiThichSoSanhs.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
                }
            }
            //Giải thích giảm đóng
            else if (_selectedTab == 2)
            {
                Func<BhQttBHXHChiTietGiaiThichModel, bool> isGTGDAdd = x => x.IsModified && x.Id == Guid.Empty;
                Func<BhQttBHXHChiTietGiaiThichModel, bool> isGTGDUpdate = x => x.IsModified && x.Id != Guid.Empty;

                var detailsAdd = GiaiThichGiamDongs.Where(isGTGDAdd).ToList();
                var detailsUpdate = GiaiThichGiamDongs.Where(isGTGDUpdate).ToList();

                //Trường hợp tạo mới
                if (detailsAdd.Count > 0)
                {
                    var addItems = new List<BhQttBHXHChiTietGiaiThich>();
                    _mapper.Map(detailsAdd, addItems);

                    foreach (var item in addItems)
                    {
                        item.Id = Guid.NewGuid();
                        item.QttBHXHId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id;
                        item.IIDMaDonVi = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIDMaDonVi;
                        item.INamLamViec = _sessionInfo.YearOfWork;
                        item.IQuyNam = SettlementVoucher == null ? QuarterYear : Convert.ToInt32(SettlementVoucher.IQuyNam);
                        item.IQuyNamLoai = SettlementVoucher == null ? QuarterYearType : SettlementVoucher.IQuyNamLoai;
                        item.SQuyNamMoTa = SettlementVoucher == null ? QuarterYearDescription : SettlementVoucher.SQuyNamMoTa;
                        item.SNguoiTao = _sessionInfo.Principal;
                        item.DNgayTao = DateTime.Now;
                        item.ILoaiGiaiThich = (int)ExplainType.GIAITHICH_GIAMDONG;
                    }
                    _chungTuChiTietGiaiThichService.AddRange(addItems);
                    _isCreate = false;
                    GiaiThichGiamDongs.Where(isGTGDAdd).Select(x =>
                    {
                        x.IsModified = false;
                        x.IsAdd = false;
                        return x;
                    }).ToList();
                }
                //Trường hợp cập nhật
                if (detailsUpdate.Count > 0)
                {
                    var updateItems = new List<BhQttBHXHChiTietGiaiThich>();
                    _mapper.Map(detailsUpdate, updateItems);

                    updateItems = detailsUpdate.Select(x => new BhQttBHXHChiTietGiaiThich
                    {
                        Id = x.Id,
                        SNguoiTao = x.SNguoiTao,
                        DNgayTao = x.DNgayTao,
                        DNgaySua = DateTime.Now,
                        SNguoiSua = _sessionInfo.Principal,
                        QttBHXHId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id,
                        INamLamViec = _sessionService.Current.YearOfWork,
                        IQuyNam = SettlementVoucher == null ? QuarterYear : Convert.ToInt32(SettlementVoucher.IQuyNam),
                        IQuyNamLoai = SettlementVoucher == null ? QuarterYearType : SettlementVoucher.IQuyNamLoai,
                        SQuyNamMoTa = SettlementVoucher == null ? QuarterYearDescription : SettlementVoucher.SQuyNamMoTa,
                        SLNS = x.SLNS,
                        IIDMLNS = x.IIDMLNS,
                        BHangCha = x.BHangCha,
                        IIDMaDonVi = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIDMaDonVi,
                        ILoaiGiaiThich = (int)ExplainType.GIAITHICH_GIAMDONG,
                        SNoiDung = x.SNoiDung,
                        SXauNoiMa = x.SXauNoiMa,
                        SL = x.SL,
                        SK = x.SK,
                        SM = x.SM,
                        STM = x.STM,

                        DTuNgay = x.DTuNgay,
                        DDenNgay = x.DDenNgay,
                        IQuanSo = x.IQuanSo,
                        FQuyTienLuongCanCu = x.FQuyTienLuongCanCu,
                        FSoTienGiamDong = x.FSoTienGiamDong,

                    }).ToList();

                    foreach (var item in updateItems)
                    {
                        _chungTuChiTietGiaiThichService.Update(item);
                    }
                    GiaiThichGiamDongs.Where(x => !x.BHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
                }
            }
            OnRefresh();
            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        protected void OnDeleteAll()
        {
            var result = System.Windows.MessageBox.Show(Resources.DeleteAllChungTuChiTiet, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                var lstChiTiet = _chungTuChiTietGiaiThichService.FindByQttId(SettlementVoucher.Id).ToList();
                //Xóa chi tiết giải thích
                _chungTuChiTietGiaiThichService.RemoveRange(lstChiTiet);
                System.Windows.MessageBox.Show(Resources.MsgDeleteSuccess, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                OnRefresh();
            }
        }

        //protected override void OnSelectedItemChanged()
        //{
        //    base.OnSelectedItemChanged();
        //}

        protected override void OnRefresh()
        {
            LoadDataTruyThu();
            LoadDataSoSanh();
            LoadDataGiamDong();
        }

        private void CalculateData()
        {
            Items.Where(x => x.BHangCha)
                .ForAll(x =>
                {
                    x.FTruyThuBHXHNLD = 0;
                    x.FTruyThuBHXHNSD = 0;
                    x.FTruyThuBHYTNLD = 0;
                    x.FTruyThuBHYTNSD = 0;
                    x.FTruyThuBHTNNLD = 0;
                    x.FTruyThuBHTNNSD = 0;
                    x.FLuongChinh = 0;
                    x.FPCChucVu = 0;
                    x.FPCTNNghe = 0;
                    x.FPCTNVuotKhung = 0;
                    x.FNghiOm = 0;
                    x.FHSBL = 0;
                });

            var temp = Items.Where(x => !x.BHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IIDMLNSCha, item, dictByMlns);
            }

            UpdateTotal();
        }

        private void CalculateParent(Guid idParent, BhQttBHXHChiTietGiaiThichModel item, Dictionary<Guid?, BhQttBHXHChiTietGiaiThichModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTruyThuBHXHNLD += item.FTruyThuBHXHNLD;
            model.FTruyThuBHXHNSD += item.FTruyThuBHXHNSD;
            model.FTruyThuBHYTNLD += item.FTruyThuBHYTNLD;
            model.FTruyThuBHYTNSD += item.FTruyThuBHYTNSD;
            model.FTruyThuBHTNNLD += item.FTruyThuBHTNNLD;
            model.FTruyThuBHTNNSD += item.FTruyThuBHTNNSD;
            model.FLuongChinh += item.FLuongChinh;
            model.FPCChucVu += item.FPCChucVu;
            model.FPCTNNghe += item.FPCTNNghe;
            model.FPCTNVuotKhung += item.FPCTNVuotKhung;
            model.FNghiOm += item.FNghiOm;
            model.FHSBL += item.FHSBL;

            CalculateParent(model.IIDMLNSCha, item, dictByMlns);
        }

        private void UpdateTotal()
        {
            _sumGiaiThichTruyThuModel = new SumQttBHXHChiTietGiaiThichModel();
            SumGiaiThichTruyThuModel.FTongTruyThuBHXHNLD = 0;
            SumGiaiThichTruyThuModel.FTongTruyThuBHXHNSD = 0;
            SumGiaiThichTruyThuModel.FTongTruyThuBHXHTongCong = 0;
            SumGiaiThichTruyThuModel.FTongTruyThuBHYTNLD = 0;
            SumGiaiThichTruyThuModel.FTongTruyThuBHYTNSD = 0;
            SumGiaiThichTruyThuModel.FTongTruyThuBHYTTongCong = 0;
            SumGiaiThichTruyThuModel.FTongTruyThuBHTNNLD = 0;
            SumGiaiThichTruyThuModel.FTongTruyThuBHTNNSD = 0;
            SumGiaiThichTruyThuModel.FTongTruyThuBHTNTongCong = 0;
            SumGiaiThichTruyThuModel.FTongTongTruyThuBHXH = 0;
            SumGiaiThichTruyThuModel.FTongLuongChinh = 0;
            SumGiaiThichTruyThuModel.FTongPCChucVu = 0;
            SumGiaiThichTruyThuModel.FTongPCTNNghe = 0;
            SumGiaiThichTruyThuModel.FTongPCTNVuotKhung = 0;
            SumGiaiThichTruyThuModel.FTongNghiOm = 0;
            SumGiaiThichTruyThuModel.FTongHSBL = 0;
            SumGiaiThichTruyThuModel.FTongQuyLuong = 0;

            var roots = Items.Where(t => !t.BHangCha).ToList();
            if (roots.Count > 0)
            {
                foreach (var item in roots)
                {
                    _sumGiaiThichTruyThuModel.FTongTruyThuBHXHNLD += item.FTruyThuBHXHNLD;
                    _sumGiaiThichTruyThuModel.FTongTruyThuBHXHNSD += item.FTruyThuBHXHNSD;
                    _sumGiaiThichTruyThuModel.FTongTruyThuBHXHTongCong += item.FTruyThuBHXHTongCong;
                    _sumGiaiThichTruyThuModel.FTongTruyThuBHYTNLD += item.FTruyThuBHYTNLD;
                    _sumGiaiThichTruyThuModel.FTongTruyThuBHYTNSD += item.FTruyThuBHYTNSD;
                    _sumGiaiThichTruyThuModel.FTongTruyThuBHYTTongCong += item.FTruyThuBHYTTongCong;
                    _sumGiaiThichTruyThuModel.FTongTruyThuBHTNNLD += item.FTruyThuBHTNNLD;
                    _sumGiaiThichTruyThuModel.FTongTruyThuBHTNNSD += item.FTruyThuBHTNNSD;
                    _sumGiaiThichTruyThuModel.FTongTruyThuBHTNTongCong += item.FTruyThuBHTNTongCong;
                    _sumGiaiThichTruyThuModel.FTongTongTruyThuBHXH = _sumGiaiThichTruyThuModel.FTongTruyThuBHXHTongCong + _sumGiaiThichTruyThuModel.FTongTruyThuBHYTTongCong
                        + _sumGiaiThichTruyThuModel.FTongTruyThuBHTNTongCong;
                    _sumGiaiThichTruyThuModel.FTongLuongChinh += item.FLuongChinh;
                    _sumGiaiThichTruyThuModel.FTongPCChucVu += item.FPCChucVu;
                    _sumGiaiThichTruyThuModel.FTongPCTNNghe += item.FPCTNNghe;
                    _sumGiaiThichTruyThuModel.FTongPCTNVuotKhung += item.FPCTNVuotKhung;
                    _sumGiaiThichTruyThuModel.FTongNghiOm += item.FNghiOm;
                    _sumGiaiThichTruyThuModel.FTongHSBL += item.FHSBL;
                    _sumGiaiThichTruyThuModel.FTongQuyLuong = _sumGiaiThichTruyThuModel.FTongLuongChinh + _sumGiaiThichTruyThuModel.FTongPCChucVu + _sumGiaiThichTruyThuModel.FTongPCTNNghe + _sumGiaiThichTruyThuModel.FTongPCTNVuotKhung + _sumGiaiThichTruyThuModel.FTongNghiOm + _sumGiaiThichTruyThuModel.FTongHSBL;
                }
                OnPropertyChanged(nameof(SumGiaiThichTruyThuModel));
            }
        }
        private void UpdateCompareTotal()
        {
            _sumGiaiThichSoSanhModel = new SumQttBHXHChiTietGiaiThichSoSanhModel();

            foreach (var item in _giaiThichSoSanhs)
            {
                _sumGiaiThichSoSanhModel.FTongSoPhaiThuNop += item.FSoPhaiThuNop;
                _sumGiaiThichSoSanhModel.FTongSoDaNopTrongNam += item.FSoDaNopTrongNam;
                _sumGiaiThichSoSanhModel.FTongSoDaNopSau3112 += item.FSoDaNopSau3112;
                _sumGiaiThichSoSanhModel.FTongCongSoDaNop += item.FTongSoDaNop;
                _sumGiaiThichSoSanhModel.FTongSoConPhaiNop += item.FSoConPhaiNop;
            }
            OnPropertyChanged(nameof(SumGiaiThichSoSanhModel));
        }

        private void UpdateGiamDongTotal()
        {
            _sumGiaiThichGiamDongModel = new SumQttBHXHChiTietGiaiThichGiamDongModel();

            foreach (var item in _giaiThichGiamDongs)
            {
                _sumGiaiThichGiamDongModel.ITongQuanSo += item.IQuanSo;
                _sumGiaiThichGiamDongModel.FTongQuyTienLuongCanCu += item.FQuyTienLuongCanCu;
                _sumGiaiThichGiamDongModel.FTongSoTienGiamDong += item.FSoTienGiamDong;
            }
            OnPropertyChanged(nameof(SumGiaiThichGiamDongModel));
        }
    }
}
