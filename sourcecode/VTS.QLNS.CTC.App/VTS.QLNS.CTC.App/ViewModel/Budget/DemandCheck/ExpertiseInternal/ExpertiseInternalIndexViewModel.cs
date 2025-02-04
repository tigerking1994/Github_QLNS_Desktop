using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Expertise;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.ExpertiseInternal
{
    public class ExpertiseInternalIndexViewModel : GridViewModelBase<ExpertiseModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ISktNganhThamDinhService _sktThamDinhService;
        private readonly ISktNganhThamDinhChiTietService _sktThamDinhChiTietService;
        private readonly INsDonViService _donViService;
        private ICollectionView _sktChungTuModelsView;
        private readonly ILog _logger;
        private IDanhMucService _danhMucService;
        private VTS.QLNS.CTC.App.View.Budget.DemandCheck.Expertise.ExpertiseDetail view;
        private readonly ISktMucLucService _sktMucLucService;
        private IExportService _exportService;
        private ExpertiseImport _importView;
        private ICollectionView _dataIndexFilter;

        public override string FuncCode => NSFunctionCode.BUDGET_EXPERTISE;
        public override Type ContentType => typeof(ExpertiseIndex);
        public override string Description => "Chứng từ ngành thẩm định";
        public override string Name => "Ngành thẩm định";
        public override PackIconKind IconKind => PackIconKind.RhombusOutline;
        public string TitleTuChi => (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD.ToString()) ? "Tự chi ngành thẩm định" : "Tự chi";

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnableLock));
        }

        public bool IsLock => SelectedItem != null && SelectedItem.IsLocked;
        public bool IsEdit => SelectedItem != null && !SelectedItem.IsLocked;
        public bool IsEnableLock => SelectedItem != null;
        public bool IsEnableButtonAggregate => CheckShowButtonAggregate() && SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD.ToString();

        public ExpertiseInternalDialogViewModel ExpertiseDialogViewModel { get; }
        public ExpertiseInternalDetailViewModel ExpertiseDetailViewModel { get; }


        private ObservableCollection<ComboboxItem> _dataPhanLoai;
        public ObservableCollection<ComboboxItem> DataPhanLoai
        {
            get => _dataPhanLoai;
            set => SetProperty(ref _dataPhanLoai, value);
        }

        private ComboboxItem _selectedPhanLoai;
        public ComboboxItem SelectedPhanLoai
        {
            get => _selectedPhanLoai;
            set
            {
                if (SetProperty(ref _selectedPhanLoai, value) && _selectedPhanLoai != null && _dataIndexFilter != null)
                {
                    LoadData();
                    OnPropertyChanged(nameof(TitleTuChi));
                    OnPropertyChanged(nameof(IsEnableButtonAggregate));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _lockStatus = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> LockStatus
        {
            get => _lockStatus;
            set => SetProperty(ref _lockStatus, value);
        }

        private ComboboxItem _lockStatusSelected;

        public ComboboxItem LockStatusSelected
        {
            get => _lockStatusSelected;
            set
            {
                SetProperty(ref _lockStatusSelected, value);
                LoadData();
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiNganSach;
        public ObservableCollection<ComboboxItem> DataLoaiNganSach
        {
            get => _dataLoaiNganSach;
            set => SetProperty(ref _dataLoaiNganSach, value);
        }

        private ComboboxItem _selectedLoaiNganSach;
        public ComboboxItem SelectedLoaiNganSach
        {
            get => _selectedLoaiNganSach;
            set
            {
                if (SetProperty(ref _selectedLoaiNganSach, value) && _selectedLoaiNganSach != null & _dataIndexFilter != null)
                {
                    LoadData();
                    OnPropertyChanged(nameof(TitleTuChi));
                    OnPropertyChanged(nameof(IsEnableButtonAggregate));
                }
            }
        }

        public RelayCommand ShowPopupAddCommand { get; }
        public RelayCommand ShowPopupEditCommand { get; }
        public RelayCommand ExportDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ShowPopupPrintCommand { get; }
        public RelayCommand AggregateCommand { get; set; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ShowPopupPrintSKTCommand { get; }

        public ExpertiseInternalIndexViewModel(ISktNganhThamDinhService sktThamDinhService,
        IMapper mapper,
        ISessionService sessionService,
        ISktNganhThamDinhChiTietService sktThamDinhChiTietService,
        ISktMucLucService sktMucLucService,
        IExportService exportService,
        INsDonViService donViService,
        IDanhMucService danhMucService,
        ExpertiseInternalDialogViewModel expertiseDialogViewModel,
        ExpertiseInternalDetailViewModel expertiseDetailViewModel,
        ILog logger)
        {
            _logger = logger;
            _mapper = mapper;
            _sktThamDinhService = sktThamDinhService;
            _sessionService = sessionService;
            _sktThamDinhChiTietService = sktThamDinhChiTietService;
            _donViService = donViService;
            _sktMucLucService = sktMucLucService;
            _exportService = exportService;
            _danhMucService = danhMucService;

            ExpertiseDialogViewModel = expertiseDialogViewModel;
            ExpertiseDetailViewModel = expertiseDetailViewModel;

            ExpertiseDialogViewModel.ParentPage = this;
            ExpertiseDetailViewModel.ParentPage = this;

            ShowPopupAddCommand = new RelayCommand(o => OnShowPopupAdd());
            ShowPopupEditCommand = new RelayCommand(o => OnShowPopupEdit());
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock());
            DeleteCommand = new RelayCommand(o => OnDelete());
            ExportDataCommand = new RelayCommand(o => OnExport());
            AggregateCommand = new RelayCommand(obj => OnAggregate());
            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
        }

        private bool CheckShowButtonAggregate()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO) && x.BCoNSNganh);
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            List<DonVi> listDonVi = _donViService.FindByCondition(predicate).ToList();
            if (listDonVi != null && listDonVi.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnAggregate()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.Loai == LoaiDonVi.ROOT);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            DonVi donVi0 = _donViService.FindByCondition(predicate).FirstOrDefault();
            if (donVi0 != null)
            {
                var predicateChungTu = PredicateBuilder.True<NsSktNganhThamDinh>();
                predicateChungTu = predicateChungTu.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                predicateChungTu = predicateChungTu.And(x => x.ILoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD);
                predicateChungTu = predicateChungTu.And(x => x.IIdMaDonVi == donVi0.IIDMaDonVi);
                NsSktNganhThamDinh chungTuTongHopHistory = _sktThamDinhService.FindByCondition(predicateChungTu).FirstOrDefault();
                if (chungTuTongHopHistory != null)
                {
                    _sktThamDinhService.Delete(chungTuTongHopHistory.Id);
                }
                NsSktNganhThamDinh chungTuTongHop = new NsSktNganhThamDinh();
                int soChungTuIndex = _sktThamDinhService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                chungTuTongHop.IIdMaDonVi = donVi0.IIDMaDonVi;
                chungTuTongHop.DNgayChungTu = DateTime.Now;
                chungTuTongHop.ILoai = VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD;
                chungTuTongHop.INamLamViec = _sessionService.Current.YearOfWork;
                chungTuTongHop.DNgayTao = DateTime.Now;
                chungTuTongHop.SNguoiTao = _sessionService.Current.Principal;
                chungTuTongHop.ISoChungTuIndex = soChungTuIndex;
                chungTuTongHop.INamNganSach = _sessionService.Current.YearOfBudget;
                chungTuTongHop.IIdMaNguonNganSach = _sessionService.Current.Budget;
                chungTuTongHop.SSoChungTu = "THD-" + soChungTuIndex.ToString("D3");
                _sktThamDinhService.Add(chungTuTongHop);
                _sktThamDinhService.CreateVoucherAggregate(chungTuTongHop.Id.ToString(), donVi0.IIDMaDonVi, donVi0.TenDonVi, _sessionService.Current.YearOfWork, _sessionService.Current.Principal,
                    _sessionService.Current.YearOfBudget, _sessionService.Current.Budget);
                MessageBoxHelper.Info(Resources.MsgCreateVoucherSuccess);
                LoadData();
            }
        }



        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        private void OnExport()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == LoaiNganhThamDinh.CTNTD.ToString())
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CHUNGTU_TONGHOP_NTD);
                        if (_selectedLoaiNganSach != null && _selectedLoaiNganSach.ValueItem.Equals(VoucherType.NSBD_Key))
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CHUNGTU_TONGHOP_NSBD_NTD);
                        }
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CHUNGTU_TONGHOP);
                        if (_selectedLoaiNganSach != null && _selectedLoaiNganSach.ValueItem.Equals(VoucherType.NSBD_Key))
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CHUNGTU_TONGHOP_NSBD);
                        }
                    }

                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    int yearOfWork = _sessionService.Current.YearOfWork;
                    var predicate = PredicateBuilder.True<NsSktMucLuc>();
                    predicate = predicate.And(x => x.INamLamViec == yearOfWork);
                    predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
                    List<NsSktMucLuc> sktMucLucs = _sktMucLucService.FindByCondition(predicate).ToList();
                    var sktMucLucsOrder = from sktMucLuc in sktMucLucs orderby sktMucLuc.SKyHieu select sktMucLuc;

                    foreach (ExpertiseModel item in Items.Where(n => n.Selected))
                    {
                        List<ExpertiseModelDetailModel> listDetail = new List<ExpertiseModelDetailModel>();
                        if (_selectedLoaiNganSach != null && _selectedLoaiNganSach.ValueItem.Equals(VoucherType.NSSD_Key))
                        {
                            listDetail = GetDataDetail(item);
                        }
                        else
                        {
                            listDetail = GetDataDetailNSBD(item);
                        }
                        if (item.ILoai.HasValue && item.ILoai.Value == LoaiNganhThamDinh.CTCTCDN)
                        {
                            if (listDetail != null && listDetail.Count > 0)
                            {
                                List<string> listNganh = listDetail.Where(n => n.TuChi != 0 && !string.IsNullOrEmpty(n.Nganh)).Select(n => n.Nganh).Distinct().ToList();
                                List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => !string.IsNullOrEmpty(n.SGiaTri)).ToList();
                                List<string> listDonVi = listDanhMuc.Where(n => listNganh.Contains(n.IIDMaDanhMuc)).Select(n => n.SGiaTri).Distinct().ToList();
                                foreach (string donViChiTiet in listDonVi)
                                {
                                    List<string> listNganhChiTiet = listDanhMuc.Where(n => n.SGiaTri == donViChiTiet).Select(n => n.IIDMaDanhMuc).Distinct().ToList();
                                    List<ExpertiseModelDetailModel> listDonViData = listDetail.Where(n => listNganhChiTiet.Contains(n.Nganh) || n.IsHangCha).ToList();
                                    FormatDataExport(ref listDonViData);
                                    Dictionary<string, object> data = new Dictionary<string, object>();
                                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                                    data.Add("Cap2", GetHeader2Report());
                                    data.Add("TenDonVi", item.TenDonVi);
                                    data.Add("ListData", listDonViData);
                                    data.Add("SKTML", sktMucLucsOrder);

                                    fileNamePrefix = item.SoChungTu;
                                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix) + "_" + donViChiTiet;
                                    var xlsFile = _exportService.Export<ExpertiseModelDetailModel, NsSktMucLuc>(templateFileName, data);
                                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                                }
                            }
                        }
                        else if (item.ILoai.HasValue && item.ILoai.Value == LoaiNganhThamDinh.CTNTD)
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                            data.Add("Cap2", GetHeader2Report());
                            data.Add("TenDonVi", item.TenDonVi);
                            data.Add("ListData", listDetail);
                            data.Add("SKTML", sktMucLucsOrder);

                            fileNamePrefix = item.SoChungTu;
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ExpertiseModelDetailModel, NsSktMucLuc>(templateFileName, data);
                            var nameRange = xlsFile.GetNamedRange(1);
                            nameRange.Comment = "Workbook";
                            xlsFile.SetNamedRange(nameRange);
                            xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private Expression<Func<DonVi, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT) && x.BCoNSNganh);
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            return predicate;
        }

        private List<DonVi> GetListDonVi()
        {
            var predicate = CreatePredicate();
            List<DonVi> listDonVi = _donViService.FindByCondition(predicate).ToList();
            return listDonVi;
        }

        private List<ExpertiseModelDetailModel> GetDataDetail(ExpertiseModel chungTu)
        {
            List<ExpertiseModelDetailModel> resultDetail = new List<ExpertiseModelDetailModel>();
            List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => !string.IsNullOrEmpty(n.SGiaTri)).ToList();
            if (chungTu == null || chungTu.Id == Guid.Empty || listDanhMuc == null || listDanhMuc.Count == 0)
                return resultDetail;
            List<ThDChungTuChiTietQuery> data = _sktThamDinhChiTietService.FindByCondition(_sessionService.Current.YearOfWork, chungTu.Id.ToString(),
                _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();

            List<string> listKyHieu = new List<string>();
            foreach (ThDChungTuChiTietQuery item in data.Where(n => !n.IsHangCha))
            {
                listKyHieu.AddRange(StringUtils.SplitKyHieuParent(item.KyHieu));
            }
            data = data.Where(n => listKyHieu.Contains(n.KyHieu)).ToList();
            resultDetail = _mapper.Map<List<Model.ExpertiseModelDetailModel>>(data);

            if (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == LoaiNganhThamDinh.CTNTD.ToString())
            {
                List<ThDChungTuChiTietQuery> dataDeNghi = GetValueTuChiPrev(chungTu);
                if (dataDeNghi != null && dataDeNghi.Count > 0)
                {
                    foreach (ExpertiseModelDetailModel item in resultDetail)
                    {
                        ThDChungTuChiTietQuery valueItem = dataDeNghi.Where(n => n.IdMucLuc == item.IdMucLuc && n.IdDonVi == item.IdDonVi).FirstOrDefault();
                        if (valueItem != null)
                        {
                            item.TuChiPrev = valueItem.TuChi;
                            item.SuDungTonKhoPrev = valueItem.SuDungTonKho;
                            item.ChiDacThuNganhPhanCapPrev = valueItem.ChiDacThuNganhPhanCap;
                        }
                    }
                }
            }

            FormatDataExport(ref resultDetail);
            return resultDetail;
        }

        public List<ThDChungTuChiTietQuery> GetValueTuChiPrev(ExpertiseModel chungTu)
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTCTCDN);

            if (chungTu.ILoaiChungTu.HasValue && chungTu.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key)
            {
                predicate = predicate.And(x => x.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key);
            }
            else
            {
                predicate = predicate.And(x => x.ILoaiChungTu.Value.ToString() == VoucherType.NSSD_Key);
            }

            NsSktNganhThamDinh chungTuDeNghi = _sktThamDinhService.FindByCondition(predicate).FirstOrDefault();
            if (chungTuDeNghi == null)
            {
                return new List<ThDChungTuChiTietQuery>();
            }
            else
            {
                List<ThDChungTuChiTietQuery> data = new List<ThDChungTuChiTietQuery>();
                if (chungTu.ILoaiChungTu.HasValue && chungTu.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key)
                {
                    data = _sktThamDinhChiTietService.FindByConditionNSBD(_sessionService.Current.YearOfWork, chungTuDeNghi.Id.ToString(),
                        _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();
                }
                else
                {
                    data = _sktThamDinhChiTietService.FindByCondition(_sessionService.Current.YearOfWork, chungTuDeNghi.Id.ToString(),
                        _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();
                }
                return data.Where(n => n.TuChi != 0 || n.ChiDacThuNganhPhanCap != 0 || n.SuDungTonKho != 0).ToList();
            }
        }

        private List<ExpertiseModelDetailModel> GetDataDetailNSBD(ExpertiseModel chungTu)
        {
            List<ExpertiseModelDetailModel> resultDetail = new List<ExpertiseModelDetailModel>();
            List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => !string.IsNullOrEmpty(n.SGiaTri)).ToList();
            if (chungTu == null || chungTu.Id == Guid.Empty || listDanhMuc == null || listDanhMuc.Count == 0)
                return resultDetail;
            List<ThDChungTuChiTietQuery> data = _sktThamDinhChiTietService.FindByConditionNSBD(_sessionService.Current.YearOfWork, chungTu.Id.ToString(),
                _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();

            List<string> listKyHieu = new List<string>();
            foreach (ThDChungTuChiTietQuery item in data.Where(n => !n.IsHangCha))
            {
                listKyHieu.AddRange(StringUtils.SplitKyHieuParent(item.KyHieu));
            }
            data = data.Where(n => listKyHieu.Contains(n.KyHieu)).ToList();
            resultDetail = _mapper.Map<List<Model.ExpertiseModelDetailModel>>(data);

            if (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == LoaiNganhThamDinh.CTNTD.ToString())
            {
                List<ThDChungTuChiTietQuery> dataDeNghi = GetValueTuChiPrev(chungTu);
                if (dataDeNghi != null && dataDeNghi.Count > 0)
                {
                    foreach (ExpertiseModelDetailModel item in resultDetail)
                    {
                        ThDChungTuChiTietQuery valueItem = dataDeNghi.Where(n => n.IdMucLuc == item.IdMucLuc && n.IdDonVi == item.IdDonVi).FirstOrDefault();
                        if (valueItem != null)
                        {
                            item.TuChiPrev = valueItem.TuChi;
                            item.SuDungTonKhoPrev = valueItem.SuDungTonKho;
                            item.ChiDacThuNganhPhanCapPrev = valueItem.ChiDacThuNganhPhanCap;
                        }
                    }
                }
            }

            FormatDataExport(ref resultDetail);
            return resultDetail;
        }

        private List<ExpertiseModelDetailModel> FormatDataExport(ref List<ExpertiseModelDetailModel> data)
        {
            CalculateData(ref data);
            data = data.Where(n => n.TuChiCTC != 0 || n.TuChiNganh != 0
                                                   || n.TuChi != 0 || n.SuDungTonKho != 0 || n.ChiDacThuNganhPhanCap != 0).ToList();
            return data;
        }

        private void CalculateData(ref List<ExpertiseModelDetailModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.TuChi = 0;
                    x.SuDungTonKho = 0;
                    x.ChiDacThuNganhPhanCap = 0;
                    x.TuChiCTC = 0;
                    x.TuChiNganh = 0;
                    x.HuyDongCTC = 0;
                    x.HuyDongNganh = 0;
                    x.TuChiPrev = 0;
                    x.SuDungTonKhoPrev = 0;
                    x.ChiDacThuNganhPhanCapPrev = 0;
                    x.Tang = 0;
                    x.Giam = 0;
                    return x;
                }).ToList();
            foreach (var item in listData.Where(x => x.IsFilter && !x.IsHangCha && !x.IsDeleted && (x.TuChi != 0 || x.TuChiCTC != 0 || x.TuChiNganh != 0 || x.HuyDongCTC != 0 || x.HuyDongNganh != 0)))
            {
                CalculateParent(ref listData, item, item);
            }
        }

        private void CalculateParent(ref List<ExpertiseModelDetailModel> listData, ExpertiseModelDetailModel currentItem, ExpertiseModelDetailModel selfItem)
        {
            var parentItem = listData.Where(x => x.IdMucLuc == currentItem.IdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.SuDungTonKho += selfItem.SuDungTonKho;
            parentItem.ChiDacThuNganhPhanCap += selfItem.ChiDacThuNganhPhanCap;
            parentItem.TuChiCTC += selfItem.TuChiCTC;
            parentItem.TuChiNganh += selfItem.TuChiNganh;
            parentItem.HuyDongCTC += selfItem.HuyDongCTC;
            parentItem.HuyDongNganh += selfItem.HuyDongNganh;
            parentItem.TuChiPrev += selfItem.TuChiPrev;
            parentItem.SuDungTonKhoPrev += selfItem.SuDungTonKhoPrev;
            parentItem.ChiDacThuNganhPhanCapPrev += selfItem.ChiDacThuNganhPhanCapPrev;
            parentItem.Tang += selfItem.Tang;
            parentItem.Giam += selfItem.Giam;
            CalculateParent(ref listData, parentItem, selfItem);
        }

        private void OnDelete()
        {
            try
            {
                if (SelectedItem.UserCreator != _sessionService.Current.Principal)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedItem.UserCreator));
                    return;
                }
                string msgConfirm = string.Format(Resources.MsgDeleteConfirm, SelectedItem.SoChungTu, SelectedItem.SoQuyetDinh,
                    SelectedItem.NgayQuyetDinh.HasValue ? SelectedItem.NgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty);
                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(msgConfirm);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    if (_sktThamDinhService.CheckExitsByChungTuId(SelectedItem.Id))
                    {
                        MessageBoxResult dialogConfirm = MessageBoxHelper.ConfirmCancel(string.Format("{0}{1}{2}{3}{4}{5}{6}",
                                                                    Resources.MsgConfirmDelete, Environment.NewLine, Resources.MsgConfirmDeleteYes, Environment.NewLine,
                                                                    Resources.MsgConfirmDeleteNo, Environment.NewLine, Resources.MsgConfirmDeleteCancel));
                        if (dialogConfirm == MessageBoxResult.Yes)
                        {
                            DeleteChungTuChiTiet(SelectedItem.Id);
                            _sktThamDinhService.Delete(SelectedItem.Id);
                            OnRefresh(null);
                        }
                        else if (dialogConfirm == MessageBoxResult.No)
                        {
                            _sktThamDinhService.UpdateStatusDisable(SelectedItem.Id);
                            OnRefresh(null);
                        }
                    }
                    else
                    {
                        DeleteChungTuChiTiet(SelectedItem.Id);
                        _sktThamDinhService.Delete(SelectedItem.Id);
                        OnRefresh(null);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnLockUnLock()
        {
            try
            {
                if (IsLock)
                {
                    List<DonVi> userAgency = _donViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                    {
                        MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                        return;
                    }
                }
                else
                {
                    if (SelectedItem.UserCreator != _sessionService.Current.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, SelectedItem.UserCreator));
                        return;
                    }
                }
                string msgConfirm = string.Format(IsLock ? Resources.MsgUnLock : Resources.MsgLock, Environment.NewLine, Environment.NewLine);
                string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(msgConfirm);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    OnLockHandler(msgDone);
                    LockStatusSelected = LockStatus.ElementAt(0);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnLockHandler(string msgDone)
        {
            _sktThamDinhService.LockOrUnLock(SelectedItem.Id, !SelectedItem.IsLocked);
            MessageBoxHelper.Info(msgDone);
            OnRefresh(null);
        }

        public void DeleteChungTuChiTiet(Guid idChungTu)
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinhChiTiet>();
            predicate = predicate.And(x => x.IIdCtnganhThamDinh == idChungTu);
            List<NsSktNganhThamDinhChiTiet> list = _sktThamDinhChiTietService.FindByCondition(predicate).ToList();
            if (list != null && list.Count > 0)
            {
                foreach (NsSktNganhThamDinhChiTiet item in list)
                {
                    _sktThamDinhChiTietService.Delete(item.Id);
                }
            }
        }

        public void OnShowDetail(ExpertiseModel itemDetail)
        {
            try
            {
                if (itemDetail == null || !itemDetail.ILoai.HasValue)
                    return;
                ExpertiseDetailViewModel.Model = itemDetail;
                ExpertiseDetailViewModel.PhanLoai = itemDetail.ILoai.Value;
                ExpertiseDetailViewModel.Init();
                //ExpertiseDetailViewModel.SavedAction = obj =>
                //{
                //    this.OnRefresh(obj);
                //    OnShowDetail((ExpertiseModel)obj);
                //};
                view = new VTS.QLNS.CTC.App.View.Budget.DemandCheck.Expertise.ExpertiseDetail
                {
                    DataContext = ExpertiseDetailViewModel
                };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }





        private async void OnShowPopupAdd()
        {
            try
            {
                ExpertiseDialogViewModel.Model = new Model.ExpertiseModel();
                ExpertiseDialogViewModel.Init();
                ExpertiseDialogViewModel.SavedAction = obj =>
                {
                    ExpertiseModel objValue = (ExpertiseModel)obj;
                    if (objValue != null && objValue.ILoai.HasValue && objValue.ILoai.Value == LoaiNganhThamDinh.CTNTD)
                    {
                        SelectedPhanLoai = DataPhanLoai.FirstOrDefault(n => n.ValueItem == LoaiNganhThamDinh.CTNTD.ToString());
                    }
                    else
                    {
                        SelectedPhanLoai = DataPhanLoai.FirstOrDefault();
                    }

                    if (objValue != null && objValue.ILoaiChungTu.HasValue && objValue.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key)
                    {
                        SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault(n => n.ValueItem == VoucherType.NSBD_Key.ToString());
                    }
                    else
                    {
                        SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault();
                    }
                    this.OnRefresh(obj);
                    OnShowDetail((ExpertiseModel)obj);
                };
                var view = new View.Budget.DemandCheck.Expertise.ExpertiseDialog
                {
                    DataContext = ExpertiseDialogViewModel
                };
                var result = await DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private async void OnShowPopupEdit()
        {
            try
            {
                if (SelectedItem != null)
                {
                    if (SelectedItem.UserCreator != _sessionService.Current.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedItem.UserCreator));
                        return;
                    }
                    this.ExpertiseDialogViewModel.Model = SelectedItem;
                    ExpertiseDialogViewModel.Init();
                    ExpertiseDialogViewModel.SavedAction = obj =>
                    {
                        ExpertiseModel objValue = (ExpertiseModel)obj;
                        if (objValue != null && objValue.ILoai.HasValue && objValue.ILoai.Value == LoaiNganhThamDinh.CTNTD)
                        {
                            SelectedPhanLoai = DataPhanLoai.FirstOrDefault(n => n.ValueItem == LoaiNganhThamDinh.CTNTD.ToString());
                        }
                        else
                        {
                            SelectedPhanLoai = DataPhanLoai.FirstOrDefault();
                        }
                        if (objValue != null && objValue.ILoaiChungTu.HasValue && objValue.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key)
                        {
                            SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault(n => n.ValueItem == VoucherType.NSBD_Key.ToString());
                        }
                        else
                        {
                            SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault();
                        }
                        this.OnRefresh(obj);
                        //OnShowDetail((ExpertiseModel)obj);
                    };
                    var view = new ExpertiseDialog
                    {
                        DataContext = ExpertiseDialogViewModel
                    };
                    var result = await DialogHost.Show(view, "RootDialog", null, null);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnRefresh(object obj)
        {
            LoadData();
        }

        public override void Init()
        {
            try
            {
                LoadPhanLoai();
                LoadLoaiNganSach();
                LoadLockStatus();
                LoadData();
                ExpertiseDetailViewModel.ClosePopup += RefreshAfterClosePopup;
                OnPropertyChanged(nameof(SelectedPhanLoai));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadLoaiNganSach()
        {
            DataLoaiNganSach = new ObservableCollection<ComboboxItem>();
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSSD_Key, DisplayItem = VoucherType.NSSD_Value });
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSBD_Key, DisplayItem = VoucherType.NSBD_Value });
            SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault();
        }

        private void LoadLockStatus()
        {
            var lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        private void LoadPhanLoai()
        {
            DataPhanLoai = new ObservableCollection<ComboboxItem>();
            DataPhanLoai.Add(new ComboboxItem { ValueItem = LoaiNganhThamDinh.CTCTCDN.ToString(), DisplayItem = LoaiNganhThamDinh.TEN_CTCTCDN });
            DataPhanLoai.Add(new ComboboxItem { ValueItem = LoaiNganhThamDinh.CTNTD.ToString(), DisplayItem = LoaiNganhThamDinh.TEN_CTNTD });
            SelectedPhanLoai = DataPhanLoai.FirstOrDefault();
        }

        private void OpenDetailAfterImport(object sender, EventArgs e)
        {
            try
            {
                _importView.Close();
                OnRefresh(null);
                OnShowDetail((ExpertiseModel)sender);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void RefreshAfterClosePopup(object sender, EventArgs e)
        {
            try
            {
                view.Close();
                OnRefresh(null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool DataFilter(object obj)
        {
            bool result = true;
            var item = (ExpertiseModel)obj;
            if (SelectedPhanLoai != null)
                result = result && item.ILoai.HasValue && item.ILoai.Value.ToString() == SelectedPhanLoai.ValueItem.ToString();
            return result;
        }

        private void LoadData()
        {
            try
            {
                var currentIdDonVi = _sessionService.Current.IdDonVi;
                int loai = LoaiNganhThamDinh.CTCTCDN;
                int loaiNganSach = int.Parse(VoucherType.NSSD_Key);
                if (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == LoaiNganhThamDinh.CTNTD.ToString())
                {
                    loai = LoaiNganhThamDinh.CTNTD;
                }
                if (SelectedLoaiNganSach != null && SelectedLoaiNganSach.ValueItem == VoucherType.NSBD_Key.ToString())
                {
                    loaiNganSach = int.Parse(VoucherType.NSBD_Key);
                }
                IEnumerable<ThDChungTuQuery> data = _sktThamDinhService.FindByNamLamViec(_sessionService.Current.YearOfWork,
                    _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, _sessionService.Current.Principal, loai, loaiNganSach);

                if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
                {
                    data = data.Where(x => x.IsLocked).ToList();
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
                {
                    data = data.Where(x => x.IsLocked == false).ToList();
                }

                Items = _mapper.Map<ObservableCollection<Model.ExpertiseModel>>(data);
                _dataIndexFilter = CollectionViewSource.GetDefaultView(Items);
                _dataIndexFilter.Filter = DataFilter;
                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
