using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.App.View.NewSalary.NewSettlement.NewRegularSettlement;
using System.Windows;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewReport;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewRegularSettlement
{
    public class NewRegularSettlementDetailViewModel : DetailViewModelBase<TlQtChungTuNq104Model, TlQtChungTuChiTietNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlQtChungTuChiTietNq104Service _tlQtChungTuChiTietService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlQtChungTuNq104Service _tlQtChungTuService;
        private readonly IExportService _exportService;
        private readonly ISessionService _sessionService;
        private ICollectionView _chungTuView;
        private readonly ITlQtChungTuChiTietGiaiThichNq104Service _tlQtChungTuChiTietGiaiThichService;
        private readonly ITlDmCanBoNq104Service _tlDmCanBoService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDtChungTuService _estimationService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;

        private List<NsDtChungTuChiTiet> _nsDtChungTuChiTiets;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_SETTLEMENT_REGULAR_SETTLEMENT_DETAIL;
        public override Type ContentType => typeof(View.NewSalary.NewSettlement.NewRegularSettlement.NewRegularSettlementDetail);
        public bool IsReadOnly => SelectedCachTinhLuong != null && !SelectedCachTinhLuong.ValueItem.Equals("0");
        public bool IsTongHop => !Model.STongHop.IsEmpty();
        public DtChungTuModel DtChungTuModel { get; set; }

        private List<ComboboxItem> _itemsTypeExport;
        public List<ComboboxItem> ItemsTypeExport
        {
            get => _itemsTypeExport;
            set => SetProperty(ref _itemsTypeExport, value);
        }

        private ComboboxItem _selectedTypeExport;
        public ComboboxItem SelectedTypeExport
        {
            get => _selectedTypeExport;
            set
            {
                if (SetProperty(ref _selectedTypeExport, value) && _chungTuView != null)
                {
                    _chungTuView.Refresh();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsCachTinhLuong;
        public ObservableCollection<ComboboxItem> ItemsCachTinhLuong
        {
            get => _itemsCachTinhLuong;
            set => SetProperty(ref _itemsCachTinhLuong, value);
        }

        private ComboboxItem _selectedCachTinhLuong;
        public ComboboxItem SelectedCachTinhLuong
        {
            get => _selectedCachTinhLuong;
            set
            {
                SetProperty(ref _selectedCachTinhLuong, value);
                LoadData();
                OnPropertyChanged(nameof(IsReadOnly));
                OnPropertyChanged(nameof(IsShowDuToan));
            }
        }

        private NsMuclucNgansachModel _detailFilter;
        public NsMuclucNgansachModel DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private ObservableCollection<TlQtChungTuChiTietNq104Model> _itemsChungTuChiTiet;
        public ObservableCollection<TlQtChungTuChiTietNq104Model> ItemsChungTuChiTiet
        {
            get => _itemsChungTuChiTiet;
            set => SetProperty(ref _itemsChungTuChiTiet, value);
        }

        private TlQtChungTuChiTietNq104Model _selectedItemChungTu;
        public TlQtChungTuChiTietNq104Model SelectedItemChungTu
        {
            get => _selectedItemChungTu;
            set => SetProperty(ref _selectedItemChungTu, value);
        }

        private TlQtChungTuChiTietGiaiThichNq104Model _tlRegularDataIntertation;
        public TlQtChungTuChiTietGiaiThichNq104Model TlRegularDataIntertation
        {
            get => _tlRegularDataIntertation;
            set => SetProperty(ref _tlRegularDataIntertation, value);
        }

        public Visibility IsShowDuToan => SelectedCachTinhLuong != null && SelectedCachTinhLuong.ValueItem.Equals("0") ? Visibility.Visible : Visibility.Hidden;

        public bool DataLock => Model.BKhoa;

        public NewBaoCaoQttxTheoCachTinhLuongViewModel BaoCaoQttxTheoCachTinhLuongViewModel { get; set; }
        public NewRegularDataIntertationViewModel RegularDataIntertationViewModel { get; set; }
        public NewRegularGetEstimatesDialogViewModel RegularGetEstimatesDialogViewModel { get; set; }
        public NewRegularSettlementPrintDialogViewModel RegularSettlementPrintDialogViewModel { get; set; }
        public NewRegularSettlementImportViewModel RegularSettlementImportViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetSearchCommand { get; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ReloadDataCommand { get; }
        public RelayCommand GiaiThichSoLieuCommand { get; }
        public RelayCommand PrintGiaiThichSolieuCommand { get; }
        public RelayCommand GetDataDuToanCommand { get; }
        public RelayCommand ImportDataDuToanCommand { get; }
        public RelayCommand DeleteEstimateDataCommand { get; }
        private SessionInfo _sessionInfo;

        public NewRegularSettlementDetailViewModel(
            ILog logger,
            IMapper mapper,
            ITlQtChungTuChiTietNq104Service tlQtChungTuChiTietService,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlQtChungTuNq104Service tlQtChungTuService,
            IExportService exportService,
            ISessionService sessionService,
            ITlQtChungTuChiTietGiaiThichNq104Service tlQtChungTuChiTietGiaiThichService,
            ITlDmCanBoNq104Service tlDmCanBoService,
            INsDtChungTuChiTietService nsDtChungTuChiTietService,
            INsDtChungTuService nsDtChungTuService,
            IDanhMucService danhMucService,
            INsDonViService donViService,
            NewBaoCaoQttxTheoCachTinhLuongViewModel baoCaoQttxTheoCachTinhLuongViewModel,
            NewRegularDataIntertationViewModel regularDataIntertationViewModel,
            NewRegularGetEstimatesDialogViewModel regularGetEstimatesDialogViewModel,
            NewRegularSettlementPrintDialogViewModel regularSettlementPrintDialogViewModel,
            NewRegularSettlementImportViewModel regularSettlementImportViewModel)
        {
            _mapper = mapper;
            _logger = logger;

            _tlQtChungTuChiTietService = tlQtChungTuChiTietService;
            _tlDmDonViService = tlDmDonViService;
            _tlQtChungTuService = tlQtChungTuService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _donViService = donViService;
            _sessionService = sessionService;
            _tlQtChungTuChiTietGiaiThichService = tlQtChungTuChiTietGiaiThichService;
            _tlDmCanBoService = tlDmCanBoService;
            _dtChungTuChiTietService = nsDtChungTuChiTietService;
            _estimationService = nsDtChungTuService;

            BaoCaoQttxTheoCachTinhLuongViewModel = baoCaoQttxTheoCachTinhLuongViewModel;
            RegularDataIntertationViewModel = regularDataIntertationViewModel;
            RegularGetEstimatesDialogViewModel = regularGetEstimatesDialogViewModel;
            RegularSettlementPrintDialogViewModel = regularSettlementPrintDialogViewModel;
            RegularSettlementImportViewModel = regularSettlementImportViewModel;

            SearchCommand = new RelayCommand(obj => { _chungTuView.Refresh(); });
            ResetSearchCommand = new RelayCommand(OnResetSearch);
            PrintCommand = new RelayCommand(OnPrint);
            GetDataDuToanCommand = new RelayCommand(o => OnOpenGetEstimatesDialog());
            ImportDataDuToanCommand = new RelayCommand(o => OnOpenImportWindow());
            DeleteEstimateDataCommand = new RelayCommand(o => OnDeleteEstimateData());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            DetailFilter = new NsMuclucNgansachModel();
            LoadTypeExport();
            LoadCachTinhLuong();
        }

        private void LoadData()
        {
            string idChungTu = Model.Id.ToString();
            var data = new List<TlQtChungTuChiTietNq104Query>();
            if (SelectedCachTinhLuong != null)
            {
                if (SelectedCachTinhLuong.ValueItem.Equals("0"))
                {
                    data = _tlQtChungTuChiTietService.GetDataChungTuChiTietNq104(idChungTu, Model.Nam, string.Empty).ToList();
                }
                else if (SelectedCachTinhLuong.ValueItem.Equals("1"))
                {
                    data = _tlQtChungTuChiTietService.GetDataChungTuChiTietNq104(idChungTu, Model.Nam, CachTinhLuong.CACH0).ToList();
                }
                else if (SelectedCachTinhLuong.ValueItem.Equals("2"))
                {
                    data = _tlQtChungTuChiTietService.GetDataChungTuChiTietNq104(idChungTu, Model.Nam, CachTinhLuong.CACH5).ToList();
                }
            }

            if (!string.IsNullOrEmpty(Model.IidChungTuDuToan))
            {
                var lstDtChungTuChiTiet1 = _dtChungTuChiTietService.FindByIdChungTu(Model.IidChungTuDuToan.Replace(";", ","));
                foreach (var item1 in lstDtChungTuChiTiet1)
                {
                    var chungTu = data.Where(x => item1.SXauNoiMa.Equals(x.XauNoiMa) && !x.Id.HasValue && x.MaDonVi==item1.IIdMaDonVi).FirstOrDefault();
                    if (chungTu != null && chungTu.Id != Guid.Empty)
                    {
                        chungTu.DDuToan = chungTu.DDuToan == null ? (decimal?)item1.FTuChi : (chungTu.DDuToan + (decimal?)item1.FTuChi);
                    }
                }

            }

            _itemsChungTuChiTiet = _mapper.Map<ObservableCollection<TlQtChungTuChiTietNq104Model>>(data);
            CalculateData(_itemsChungTuChiTiet.ToList());
            foreach (var item in _itemsChungTuChiTiet)
            {
                if (item.BHangCha == false)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }
            }

            _chungTuView = CollectionViewSource.GetDefaultView(_itemsChungTuChiTiet);
            _chungTuView.Filter = ChungTuFilter;

            OnPropertyChanged(nameof(ItemsChungTuChiTiet));
        }

        private void LoadTypeExport()
        {
            ItemsTypeExport = new List<ComboboxItem>();
            _itemsTypeExport.Add(new ComboboxItem("Hiển thị tất cả", "1"));
            _itemsTypeExport.Add(new ComboboxItem("Có dữ liệu", "2"));

            _selectedTypeExport = ItemsTypeExport.Where(x => x.ValueItem == "2").FirstOrDefault();
            OnPropertyChanged(nameof(ItemsTypeExport));
            OnPropertyChanged(nameof(SelectedTypeExport));
        }

        private void LoadCachTinhLuong()
        {
            _itemsCachTinhLuong = new ObservableCollection<ComboboxItem>();
            _itemsCachTinhLuong.Add(new ComboboxItem("Cách tính lương chuẩn", "1"));
            _itemsCachTinhLuong.Add(new ComboboxItem("Cách tính lương truy lĩnh", "2"));
            _itemsCachTinhLuong.Add(new ComboboxItem("Tất cả", "0"));

            SelectedCachTinhLuong = ItemsCachTinhLuong.FirstOrDefault(x => x.ValueItem == "0");
            OnPropertyChanged(nameof(ItemsCachTinhLuong));
        }

        private void OnPrint(object param)
        {
            var salaryPrintType = (SalaryPrintType)(int)param;
            switch (salaryPrintType)
            {
                case SalaryPrintType.BC_CHI_TIET_QUYET_TOAN_TX_SO_LIEU:
                    ExportChiTietQtThuongXuyenSoLieu();
                    break;
                case SalaryPrintType.BC_CHI_TIET_QUYET_TOAN_TX_THEO_COT:
                    ExportChiTietQtThuongXuyenTheoCot();
                    break;
                case SalaryPrintType.BC_CHI_TIET_QUYET_TOAN_TX_THEO_CACH_TINH_LUONG:
                    OnOpenExportDialog();
                    break;
                case SalaryPrintType.BC_QUYET_TOAN_LUONG_PHU_CAP:
                    OnExport(salaryPrintType);
                    break;
                case SalaryPrintType.BC_QUYET_TOAN_TIEN_AN:
                    OnExport(salaryPrintType);
                    break;
                case SalaryPrintType.BC_QUYET_TOAN_BAO_HIEM:
                    OnExport(salaryPrintType);
                    break;
                case SalaryPrintType.BC_QUYET_TOAN_RA_QUAN:
                    OnExport(salaryPrintType);
                    break;
                default:
                    break;
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            this.LoadData();
        }

        private void OnOpenExportDialog()
        {
            try
            {
                BaoCaoQttxTheoCachTinhLuongViewModel.ChungTuModel = Model;
                BaoCaoQttxTheoCachTinhLuongViewModel.Title = "Báo cáo quyết toán thường xuyên theo cách tính lương";
                BaoCaoQttxTheoCachTinhLuongViewModel.Description = "Báo cáo quyết toán thường xuyên theo cách tính lương";
                BaoCaoQttxTheoCachTinhLuongViewModel.Init();
                BaoCaoQttxTheoCachTinhLuongViewModel.ShowDialogHost("RegularSettlementDetail");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenImportWindow()
        {
            RegularSettlementImportViewModel.Init();
            RegularSettlementImportViewModel.SavedAction = obj =>
            {
                _nsDtChungTuChiTiets = new List<NsDtChungTuChiTiet>();
                var lstImport = RegularSettlementImportViewModel.DivisionDetails.Where(x => x.ImportStatus);
                _nsDtChungTuChiTiets = _mapper.Map<List<NsDtChungTuChiTiet>>(lstImport);
                SaveDataDuToanImport();
                this.OnRefresh();
            };
            RegularSettlementImportViewModel.ShowDialog();
        }

        private void OnOpenGetEstimatesDialog()
        {
            DtChungTuModel = new DtChungTuModel();
            RegularGetEstimatesDialogViewModel.tlQtChungTuModel = Model;
            RegularGetEstimatesDialogViewModel.Init();
            RegularGetEstimatesDialogViewModel.ChooseAction = obj =>
            {
                this.DtChungTuModel = RegularGetEstimatesDialogViewModel.SelectedDtChungTu;
                SaveDataDuToan();
                this.OnRefresh();
            };
            RegularGetEstimatesDialogViewModel.ShowDialogHost("RegularSettlementDetail");
        }

        private void OnDeleteEstimateData()
        {
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm("Bạn có chắc chắn muốn xóa dữ liệu dự toán?");
            if (dialogResult == MessageBoxResult.Yes)
            {
                var lstCtCach0 = _tlQtChungTuChiTietService.FindAll(x => Model.Id.Equals(x.IdChungTu) && (string.Empty.Equals(x.MaCachTl)));
                var lstCtHasDuToan = _tlQtChungTuChiTietService.FindAll(x => Model.Id.Equals(x.IdChungTu) && x.MaCachTl == null && x.DDuToan != 0);
                lstCtCach0.Select(x =>
                {
                    x.DDuToan = null;
                    return x;
                }).ToList();
                _tlQtChungTuChiTietService.UpdateRange(lstCtCach0);
                foreach (var item in lstCtHasDuToan)
                {
                    _tlQtChungTuChiTietService.Delete(item);
                }

                if (!string.IsNullOrEmpty(Model.IidChungTuDuToan))
                {
                    var lstIdDuToan = Model.IidChungTuDuToan.Split(";", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in lstIdDuToan)
                    {
                        var ctDuToan = _estimationService.FindById(Guid.Parse(item));
                        //ctDuToan.BLuongNhanDuLieu = false;
                        if (!ctDuToan.SDonViNhanDuLieu.IsEmpty())
                        {
                            ctDuToan.SDonViNhanDuLieu = String.Join(",", ctDuToan.SDonViNhanDuLieu.Split(',').Where(x => x != Model.MaDonVi));
                        }
                        _estimationService.Update(ctDuToan);
                    }
                }

                Model.IidChungTuDuToan = string.Empty;
                _tlQtChungTuService.Update(_mapper.Map<TlQtChungTuNq104>(Model));

                OnRefresh();
                MessageBoxHelper.Info("Xóa dữ liệu dự toán thành công.");
            }
        }

        private void SaveDataDuToan()
        {
            var lstCtCach0 = _tlQtChungTuChiTietService.FindAll(x => Model.Id.Equals(x.IdChungTu) && string.Empty.Equals(x.MaCachTl));

            var lstDtChungTuChiTiet = _dtChungTuChiTietService.FindByIdChungTu(DtChungTuModel.Id.ToString());
            if (!_donViService.IsDonViCha(Model.MaDonVi, Model.Nam))
            {
                lstDtChungTuChiTiet = lstDtChungTuChiTiet.Where(n => n.IIdMaDonVi == Model.MaDonVi);
            }
            List<NsDtChungTuChiTiet> lstDuToanAdd = new List<NsDtChungTuChiTiet>();
            foreach (var item in lstDtChungTuChiTiet)
            {
                //var chungTu = lstCtCach0.Where(x => item.SXauNoiMa.Equals(x.XauNoiMa)).FirstOrDefault();
                //if (chungTu != null && chungTu.Id != Guid.Empty)
                //{
                //    chungTu.DDuToan = chungTu.DDuToan == null ? (decimal?)item.FTuChi : (chungTu.DDuToan + (decimal?)item.FTuChi);
                //}
                //else
                //{
                //    lstDuToanAdd.Add(item);
                //}
                bool bSuaChungTu = false;
                foreach (var x in lstCtCach0)
                {
                    if (x.XauNoiMa.Equals(item.SXauNoiMa))
                    {
                        x.DDuToan = x.DDuToan == null ? (decimal?)item.FTuChi : (x.DDuToan + (decimal?)item.FTuChi);
                        bSuaChungTu = true;
                        break;
                    }
                }
                if (!bSuaChungTu)
                {
                    lstDuToanAdd.Add(item);
                }
            }

            DtChungTuModel.SDonViNhanDuLieu += string.Format(",{0}", Model.MaDonVi);
            _estimationService.Update(_mapper.Map<NsDtChungTu>(DtChungTuModel));
            if (Model.IidChungTuDuToan == null)
            {
                Model.IidChungTuDuToan = string.Empty;
            }

            Model.IidChungTuDuToan = Model.IidChungTuDuToan + ";" + DtChungTuModel.Id.ToString();
            _tlQtChungTuService.Update(_mapper.Map<TlQtChungTuNq104>(Model));

            _tlQtChungTuChiTietService.UpdateRange(lstCtCach0);
            if (lstDuToanAdd == null || lstDuToanAdd.Count == 0 || !lstDuToanAdd.Any(n => n.FTuChi != 0)) return;
            var lstInsert = lstDuToanAdd.Where(n => n.FTuChi != 0).Select(n => new TlQtChungTuChiTietNq104()
            {
                Id = Guid.NewGuid(),
                BHangCha = n.BHangCha,
                DDuToan = (decimal)n.FTuChi,
                DateCreated = DateTime.Now,
                IdChungTu = Model.Id,
                IdDonVi = Model.MaDonVi,
                Lns = n.SLns,
                L = n.SL,
                K = n.SK,
                M = n.SM,
                Tm = n.STm,
                Ttm = n.STtm,
                Ng = n.SNg,
                Tng = n.STng,
                Tng1 = n.STng1,
                Tng2 = n.STng2,
                Tng3 = n.STng3,
                TenDonVi = Model.TenDonVi,
                MlnsId = (n.IIdMlns ?? Guid.Empty),
                MlnsIdParent = n.IIdMlnsCha,
                MoTa = n.SMoTa,
                NamLamViec = n.INamLamViec ?? 0,
                NamNganSach = n.INamNganSach ?? 0,
                NguonNganSach = n.IIdMaNguonNganSach ?? 0,
                XauNoiMa = n.SXauNoiMa,
                IsAdded = true
            });
            _tlQtChungTuChiTietService.AddRange(lstInsert);
        }

        private void SaveDataDuToanImport()
        {
            var lstCtCach0 = _tlQtChungTuChiTietService.FindAll(x => Model.Id.Equals(x.IdChungTu) && string.Empty.Equals(x.MaCachTl));

            foreach (var item in _nsDtChungTuChiTiets)
            {
                var ct = lstCtCach0.FirstOrDefault(x => item.SXauNoiMa.Equals(x.XauNoiMa));
                if (ct != null)
                {
                    ct.DDuToan = ct.DDuToan == null ? (decimal?)item.FTuChi : (ct.DDuToan + (decimal?)item.FTuChi);
                }
            }

            _tlQtChungTuChiTietService.UpdateRange(lstCtCach0);
        }

        private void ExportChiTietQtThuongXuyenSoLieu()
        {
            var lstChungTu = new List<TlQtChungTuNq104Model>();
            lstChungTu.Add(Model);
            var chungTu = lstChungTu.First();
            if (chungTu == null) return;
            RegularSettlementPrintDialogViewModel.BIsDetailView = true;
            RegularSettlementPrintDialogViewModel.ItemsChungTu = lstChungTu;
            RegularSettlementPrintDialogViewModel.ChungTuModel = chungTu;
            RegularSettlementPrintDialogViewModel.InDetailView = true;
            RegularSettlementPrintDialogViewModel.Thang = chungTu.Thang;
            RegularSettlementPrintDialogViewModel.Nam = chungTu.Nam;
            RegularSettlementPrintDialogViewModel.SNam = chungTu.Nam.ToString();
            RegularSettlementPrintDialogViewModel.MaDonVi = Model.MaDonVi;
            RegularSettlementPrintDialogViewModel.TenDonVi = Model.TenDonVi;
            if (string.IsNullOrEmpty(Model.STongHop))
            {
                RegularSettlementPrintDialogViewModel.BIsSummary = false;
            }
            else
            {
                RegularSettlementPrintDialogViewModel.BIsSummary = true;
            }
            RegularSettlementPrintDialogViewModel.Init();
            RegularSettlementPrintDialogViewModel.ShowDialogHost("RegularSettlementDetail");
        }

        private void ExportChiTietQtThuongXuyenTheoCot()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    var data = GetDataQttxTheoCot(Model);
                    List<TlQtChungTuChiTietNq104Model> listData = new List<TlQtChungTuChiTietNq104Model>();
                    listData = _mapper.Map<List<TlQtChungTuChiTietNq104Model>>(data);
                    if (listData != null)
                    {
                        foreach (var it in listData)
                        {
                            it.IdDonVi = Model.MaDonVi;
                        }
                    }
                    List<Guid> lstMucLucId = listData != null ? listData.Select(x => x.MlnsId).Distinct().ToList() : new List<Guid>();
                    List<TlDmDonViNq104> listDonViCon = new List<TlDmDonViNq104>();
                    if (string.IsNullOrEmpty(Model.STongHop))
                    {
                        listDonViCon = _tlDmDonViService.FindByDonViCon(Model.MaDonVi).ToList();
                    }
                    else
                    {
                        var ctCon = _tlQtChungTuService.FindAll(x => Model.STongHop.Contains(x.Id.ToString()));
                        var lstDonViCon = ctCon.Select(x => x.MaDonVi).ToList();
                        listDonViCon = _tlDmDonViService.FindByCondition(x => lstDonViCon.Contains(x.MaDonVi)).ToList();
                    }

                    foreach (var donVi in listDonViCon)
                    {
                        var tlQtChungTu = _tlQtChungTuService.FindAll().FirstOrDefault(x => x.MaDonVi == donVi.MaDonVi && x.Thang == Model.Thang && x.Nam == Model.Nam);
                        if (tlQtChungTu != null)
                        {
                            var listDataCon = GetDataQttxTheoCot(_mapper.Map<TlQtChungTuNq104Model>(tlQtChungTu));
                            var listDataConModel = _mapper.Map<List<TlQtChungTuChiTietNq104Model>>(listDataCon);
                            if (listDataConModel != null)
                            {
                                foreach (var it in listDataConModel)
                                {
                                    it.IdDonVi = donVi.MaDonVi;
                                }
                            }
                            listData.AddRange(listDataConModel);
                        }
                    }
                    CalculateDataDonVi(listData);

                    if (listDonViCon.Count > 0)
                    {
                        var listDonViConSplits = SplitList(listDonViCon, 4).ToList();
                        for (int i = 0; i < listDonViConSplits.Count(); i++)
                        {
                            List<TLChiTietQuyetToanTXNq104TheoCotModel> results = new List<TLChiTietQuyetToanTXNq104TheoCotModel>();
                            foreach (var mlId in lstMucLucId)
                            {
                                TLChiTietQuyetToanTXNq104TheoCotModel it = new TLChiTietQuyetToanTXNq104TheoCotModel();
                                var itParent = listData.FirstOrDefault(x => x.IdDonVi.Equals(Model.MaDonVi) && x.MlnsId.Equals(mlId));
                                if (itParent != null)
                                {
                                    it.Lns = itParent.Lns;
                                    it.L = itParent.L;
                                    it.K = itParent.K;
                                    it.M = itParent.M;
                                    it.Tm = itParent.Tm;
                                    it.Ttm = itParent.Ttm;
                                    it.Ng = itParent.Ng;
                                    it.Tng = itParent.Tng;
                                    it.Tng1 = itParent.Tng1;
                                    it.Tng2 = itParent.Tng2;
                                    it.Tng3 = itParent.Tng3;
                                    it.MoTa = itParent.MoTa;
                                    it.BHangCha = itParent.BHangCha != null ? itParent.BHangCha : false;
                                    it.TongCong = itParent.TongCong;
                                    it.LstGiaTri = new List<TlQtChungTuChiTietNq104Model>();
                                    foreach (var dvCon in listDonViConSplits[i])
                                    {
                                        var itCon = listData.FirstOrDefault(x => x.IdDonVi.Equals(dvCon.MaDonVi) && x.MlnsId.Equals(mlId));
                                        if (itCon != null)
                                        {
                                            it.LstGiaTri.Add(itCon);
                                        }
                                        else
                                        {
                                            it.LstGiaTri.Add(new TlQtChungTuChiTietNq104Model());
                                        }
                                    }
                                }
                                results.Add(it);
                            }
                            results = new List<TLChiTietQuyetToanTXNq104TheoCotModel>(results.OrderBy(x => x.XauNoiMa));
                            results.Where(n => !n.BHangCha.GetValueOrDefault()).Select(n => { n.Lns = string.Empty; n.L = string.Empty; n.K = string.Empty; n.M = string.Empty; n.Tm = string.Empty; return n; }).ToList();
                            results.Where(n => n.BHangCha.GetValueOrDefault() && !string.IsNullOrEmpty(n.M)).Select(n => { n.L = string.Empty; n.K = string.Empty; n.Lns = string.Empty; return n; }).ToList();
                            results.Where(n => n.BHangCha.GetValueOrDefault() && !string.IsNullOrEmpty(n.Tm)).Select(n => { n.M = string.Empty; return n; }).ToList();

                            var items = results.Where(x => x.TongCong != 0 && x.TongCong != null);

                            Dictionary<string, object> dataDic = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                            dataDic.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                            dataDic.Add("Cap2", GetHeader2Report());
                            dataDic.Add("FormatNumber", formatNumber);
                            dataDic.Add("TieuDe", string.Format("Tháng {0} - Năm {1} - {2}", Model.Thang, Model.Nam, Model.TenDonVi));
                            dataDic.Add("ListData", items);
                            dataDic.Add("ListHeaders", listDonViConSplits[i]);

                            if (i == 0)
                            {
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QT_THUONGXUYEN_THEO_COT_TRANG_1_NEW);
                            }
                            else
                            {
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QT_THUONGXUYEN_THEO_COT_TRANG_2_NEW);
                            }
                            fileNamePrefix = string.Format("rpt_Quyet_Toan_Thuong_Xuyen_Theo_Cot_{0}_{1}_{2}_Trang_{3}", Model.Thang, Model.Nam, Model.TenDonVi, i + 1);
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<TLChiTietQuyetToanTXNq104TheoCotModel, TlDmDonViNq104>(templateFileName, dataDic);
                            exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    else
                    {
                        List<TLChiTietQuyetToanTXNq104TheoCotModel> results = new List<TLChiTietQuyetToanTXNq104TheoCotModel>();
                        foreach (var mlId in lstMucLucId)
                        {
                            TLChiTietQuyetToanTXNq104TheoCotModel it = new TLChiTietQuyetToanTXNq104TheoCotModel();
                            var itParent = listData.FirstOrDefault(x => x.IdDonVi.Equals(Model.MaDonVi) && x.MlnsId.Equals(mlId));
                            if (itParent != null)
                            {
                                it.Lns = itParent.Lns;
                                it.L = itParent.L;
                                it.K = itParent.K;
                                it.M = itParent.M;
                                it.Tm = itParent.Tm;
                                it.Ttm = itParent.Ttm;
                                it.Ng = itParent.Ng;
                                it.Tng = itParent.Tng;
                                it.Tng1 = itParent.Tng1;
                                it.Tng2 = itParent.Tng2;
                                it.Tng3 = itParent.Tng3;
                                it.MoTa = itParent.MoTa;
                                it.BHangCha = itParent.BHangCha != null ? itParent.BHangCha : false;
                                it.TongCong = itParent.TongCong;
                                it.LstGiaTri = new List<TlQtChungTuChiTietNq104Model>();
                            }
                            results.Add(it);
                        }
                        results = new List<TLChiTietQuyetToanTXNq104TheoCotModel>(results.OrderBy(x => x.XauNoiMa));
                        results.Where(n => !n.BHangCha.GetValueOrDefault()).Select(n => { n.Lns = string.Empty; n.L = string.Empty; n.K = string.Empty; n.M = string.Empty; n.Tm = string.Empty; return n; }).ToList();
                        results.Where(n => n.BHangCha.GetValueOrDefault() && !string.IsNullOrEmpty(n.M)).Select(n => { n.L = string.Empty; n.K = string.Empty; n.Lns = string.Empty; return n; }).ToList();
                        results.Where(n => n.BHangCha.GetValueOrDefault() && !string.IsNullOrEmpty(n.Tm)).Select(n => { n.M = string.Empty; return n; }).ToList();

                        var items = results.Where(x => x.TongCong != 0 && x.TongCong != null);

                        Dictionary<string, object> dataDic = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        dataDic.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        dataDic.Add("Cap2", GetHeader2Report());
                        dataDic.Add("FormatNumber", formatNumber);
                        dataDic.Add("TieuDe", string.Format("Tháng {0} - Năm {1} - {2}", Model.Thang, Model.Nam, Model.TenDonVi));
                        dataDic.Add("ListData", items);

                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QT_THUONGXUYEN_THEO_COT_NEW);
                        fileNamePrefix = string.Format("rpt_Quyet_Toan_Thuong_Xuyen_Theo_Cot_{0}_{1}_{2}{3}", Model.Thang, Model.Nam, Model.TenDonVi, 1);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<TLChiTietQuyetToanTXNq104TheoCotModel, TlDmDonViNq104>(templateFileName, dataDic);
                        exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = exportResults;
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

        public List<ReportQttxTheoCotNq104Query> GetDataQttxTheoCot(TlQtChungTuNq104Model chungTu)
        {
            string idChungTu = chungTu.Id.ToString();

            var data = new List<ReportQttxTheoCotNq104Query>();
            if (SelectedCachTinhLuong != null)
            {
                if (SelectedCachTinhLuong.ValueItem.Equals("0"))
                {
                    data = _tlQtChungTuChiTietService.GetDataChungTuChiTietTheoCotNq104(idChungTu, chungTu.Nam, CachTinhLuong.CACH0 + "," + CachTinhLuong.CACH5).ToList();
                }
                else if (SelectedCachTinhLuong.ValueItem.Equals("1"))
                {
                    data = _tlQtChungTuChiTietService.GetDataChungTuChiTietTheoCotNq104(idChungTu, chungTu.Nam, CachTinhLuong.CACH0).ToList();
                }
                else if (SelectedCachTinhLuong.ValueItem.Equals("2"))
                {
                    data = _tlQtChungTuChiTietService.GetDataChungTuChiTietTheoCotNq104(idChungTu, chungTu.Nam, CachTinhLuong.CACH5).ToList();
                }
            }

            return data;
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlQtChungTuChiTietNq104Model item = (TlQtChungTuChiTietNq104Model)sender;
            if (args.PropertyName == nameof(TlQtChungTuChiTietNq104Model.GhiChu)
                || args.PropertyName == nameof(TlQtChungTuChiTietNq104Model.DieuChinh)
                || args.PropertyName == nameof(TlQtChungTuChiTietNq104Model.DDuToan)
                || args.PropertyName == nameof(TlQtChungTuChiTietNq104Model.SoNgay)
                || args.PropertyName == nameof(TlQtChungTuChiTietNq104Model.SoNguoi))
            {
                if (args.PropertyName == nameof(TlQtChungTuChiTietNq104Model.DieuChinh))
                {
                    CalculateDieuChinhData();
                }
                if (args.PropertyName == nameof(TlQtChungTuChiTietNq104Model.DDuToan))
                {
                    CalculateDuToanData();
                }
                item.IsModified = true;
            }
        }

        private bool ChungTuFilter(object obj)
        {
            bool result = true;
            var item = (TlQtChungTuChiTietNq104Model)obj;
            if (!string.IsNullOrEmpty(DetailFilter.Lns))
                result = result && item.Lns.ToLower().StartsWith(DetailFilter.Lns.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.M.ToLower().StartsWith(DetailFilter.M.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.Tm.ToLower().StartsWith(DetailFilter.TM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.Ttm.ToLower().StartsWith(DetailFilter.TTM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.Ng.ToLower().StartsWith(DetailFilter.NG.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG))
                result = result && item.Tng.ToLower().StartsWith(DetailFilter.TNG.ToLower());
            if (SelectedTypeExport != null && SelectedTypeExport.ValueItem.Equals("2"))
            {
                result &= (item.TongCong != null && item.TongCong != 0)
                    || (item.DieuChinh != null && item.DieuChinh != 0)
                    || (item.DDuToan != null && item.DDuToan != 0);
            }
            item.IsFilter = result;
            return result;
        }

        public void OnResetSearch(object obj)
        {
            DetailFilter = new NsMuclucNgansachModel();
            _chungTuView.Refresh();
        }

        public override void OnClose(object obj)
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public override void OnSave()
        {
            base.OnSave();

            if (SelectedCachTinhLuong.ValueItem.Equals("0"))
            {
                var lstSave = _itemsChungTuChiTiet.Where(x => x.IsModified);
                var lstUpdate = new List<TlQtChungTuChiTietNq104>();
                var lstAdd = new List<TlQtChungTuChiTietNq104>();
                foreach (var item in lstSave)
                {
                    var tlqtCtChiTiet = _tlQtChungTuChiTietService.FindAll(x => x.Id.Equals(item.Id)).FirstOrDefault();
                    if (tlqtCtChiTiet != null)
                    {
                        tlqtCtChiTiet.DDuToan = item.DDuToan;
                        tlqtCtChiTiet.DieuChinh = item.DieuChinh;
                        tlqtCtChiTiet.SoNgay = item.SoNgay;
                        tlqtCtChiTiet.SoNguoi = item.SoNguoi;
                        tlqtCtChiTiet.GhiChu = item.GhiChu;
                        tlqtCtChiTiet.DateModified = DateTime.Now;
                        tlqtCtChiTiet.UserModifier = _sessionService.Current.Principal;
                        lstUpdate.Add(tlqtCtChiTiet);
                    }
                    else if (tlqtCtChiTiet == null)
                    {
                        var newCt = _mapper.Map<TlQtChungTuChiTietNq104>(item);
                        newCt.IdChungTu = Model.Id;
                        newCt.IdDonVi = Model.MaDonVi;
                        newCt.TenDonVi = Model.TenDonVi;
                        newCt.DateCreated = DateTime.Now;
                        newCt.UserCreator = _sessionService.Current.Principal;
                        newCt.MaCachTl = string.Empty;
                        lstAdd.Add(newCt);
                    }
                }

                _tlQtChungTuChiTietService.UpdateRange(lstUpdate);
                _tlQtChungTuChiTietService.AddRange(lstAdd);
                System.Windows.Forms.MessageBox.Show("Lưu dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnRefresh();
                //var lstSave = _itemsChungTuChiTiet.Where(x => x.IsModified);
                //var lstUpdate = new List<TlQtChungTuChiTiet>();
                //var lstAdd = new List<TlQtChungTuChiTiet>();

                //foreach (var item in lstSave)
                //{
                //    if ((item.TongCong != null && item.TongCong != 0) || (item.DieuChinh != null && item.DieuChinh != 0))
                //    {
                //        var ct = _tlQtChungTuChiTietService.FindAll(x => Model.Id.Equals(x.IdChungTu) && item.XauNoiMa.Equals(x.XauNoiMa)).FirstOrDefault();
                //        if (ct != null)
                //        {
                //            ct.DateModified = DateTime.Now;
                //            ct.UserModifier = _sessionService.Current.Principal;
                //            lstUpdate.Add(ct);
                //        }
                //    }
                //    else
                //    {
                //        //var newCt = _mapper.Map<TlQtChungTuChiTiet>(item);
                //        //newCt.IdChungTu = Model.Id;
                //        //newCt.IdDonVi = Model.MaDonVi;
                //        //newCt.TenDonVi = Model.TenDonVi;
                //        //newCt.DateCreated = DateTime.Now;
                //        //newCt.UserCreator = _sessionService.Current.Principal;
                //        //newCt.MaCachTl = CachTinhLuong.CACH0;
                //        //lstAdd.Add(newCt);
                //    }
                //}

                //_tlQtChungTuChiTietService.UpdateRange(lstUpdate);
                //_tlQtChungTuChiTietService.AddRange(lstAdd);
                //System.Windows.Forms.MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //OnRefresh();
            }

        }

        private void CalculateParent(TlQtChungTuChiTietNq104Model currentItem, TlQtChungTuChiTietNq104Model selfItem)
        {
            var parentItem = _itemsChungTuChiTiet.FirstOrDefault(x => x.MlnsId == currentItem.MlnsIdParent);
            if (parentItem == null) return;
            parentItem.TongCong += selfItem.TongCong;
            CalculateParent(parentItem, selfItem);
        }

        private void CalculateDuToanData()
        {
            _itemsChungTuChiTiet.Where(x => (bool)x.BHangCha)
                .Select(x =>
                {
                    x.DDuToan = 0;
                    return x;
                }).ToList();
            foreach (var item in _itemsChungTuChiTiet.Where(x => x.DDuToan != 0 && x.DDuToan != null))
            {
                CalculateDuToanParent(item, item);
            }
        }

        private void CalculateDuToanParent(TlQtChungTuChiTietNq104Model currentItem, TlQtChungTuChiTietNq104Model selfItem)
        {
            var parentItem = _itemsChungTuChiTiet.FirstOrDefault(x => x.MlnsId == currentItem.MlnsIdParent);
            if (parentItem == null) return;
            parentItem.DDuToan += selfItem.DDuToan;
            CalculateDuToanParent(parentItem, selfItem);
        }

        private void CalculateDieuChinhData()
        {
            _itemsChungTuChiTiet.Where(x => (bool)x.BHangCha)
                .Select(x =>
                {
                    x.DieuChinh = 0;
                    return x;
                }).ToList();
            foreach (var item in _itemsChungTuChiTiet.Where(x => x.DieuChinh != 0 && x.DieuChinh != null))
            {
                CalculateDieuChinhParent(item, item);
            }
        }

        private void CalculateDieuChinhParent(TlQtChungTuChiTietNq104Model currentItem, TlQtChungTuChiTietNq104Model selfItem)
        {
            var parentItem = _itemsChungTuChiTiet.FirstOrDefault(x => x.MlnsId == currentItem.MlnsIdParent);
            if (parentItem == null) return;
            parentItem.DieuChinh += selfItem.DieuChinh;
            CalculateDieuChinhParent(parentItem, selfItem);
        }

        private void CalculateData(List<TlQtChungTuChiTietNq104Model> lstQtChungTuChiTiet)
        {
            lstQtChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false) && _tlQtChungTuChiTietService.GetDataMucLucNG(x.XauNoiMa).ToList().Count == 0)
            //lstQtChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TongCong = 0;
                    x.DieuChinh = 0;
                    x.DDuToan = 0;
                    x.SoNguoi = 0;
                    x.SoNgay = 0;
                    return x;
                }).ToList();

            var temp = lstQtChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false)
            && (x.TongCong != null && x.TongCong != 0)
                || (x.DieuChinh != null && x.DieuChinh != 0)
                || (x.DDuToan != null && x.DDuToan != 0));

            foreach (var item in temp)
            {
                CalculateParent(item.MlnsIdParent, item, lstQtChungTuChiTiet);
            }

            foreach (var item in temp)
            {
                if (item.DieuChinh == null)
                {
                    var listChild = lstQtChungTuChiTiet.Where(x => x.MlnsIdParent == item.MlnsId);
                    var model = lstQtChungTuChiTiet.FirstOrDefault(x => x.MlnsId == item.MlnsId);
                    decimal dieuchinh = 0;
                    foreach (var itemList in listChild)
                    {
                        dieuchinh += itemList.DieuChinh ?? 0;
                    }
                    model.DieuChinh = dieuchinh;
                }
                if (item.TongCong == null)
                {
                    var listChild = lstQtChungTuChiTiet.Where(x => x.MlnsIdParent == item.MlnsId);
                    var model = lstQtChungTuChiTiet.FirstOrDefault(x => x.MlnsId == item.MlnsId);
                    decimal TongCong = 0;
                    foreach (var itemList in listChild)
                    {
                        TongCong += itemList.TongCong ?? 0;
                    }
                    model.TongCong = TongCong;
                }
            }


        }


        private void CalculateParent(Guid? idParent, TlQtChungTuChiTietNq104Model item, List<TlQtChungTuChiTietNq104Model> lstQtChungTuChiTiet)
        {
            var model = lstQtChungTuChiTiet.FirstOrDefault(x => x.MlnsId == idParent);
            if (model == null) return;
            if (model.TongCong == null) model.TongCong = 0;
            if (model.DieuChinh == null) model.DieuChinh = 0;
            if (model.DDuToan == null) model.DDuToan = 0;

            model.TongCong += item.TongCong ?? 0;
            model.DieuChinh += item.DieuChinh ?? 0;
            model.DDuToan += item.DDuToan ?? 0;
            CalculateParent(model.MlnsIdParent, item, lstQtChungTuChiTiet);
        }

        private void CalculateDataDonVi(List<TlQtChungTuChiTietNq104Model> lstQtChungTuChiTiet)
        {
            lstQtChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TongCong = 0;
                    x.DieuChinh = 0;
                    return x;
                }).ToList();
            var temp = lstQtChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false) && x.TongCong != null && x.TongCong != 0);
            foreach (var item in temp)
            {
                CalculateParentDonVi(item.MlnsIdParent, item.IdDonVi, item, lstQtChungTuChiTiet);
            }
        }

        private void CalculateParentDonVi(Guid? idParent, string idDonVi, TlQtChungTuChiTietNq104Model item, List<TlQtChungTuChiTietNq104Model> lstQtChungTuChiTiet)
        {
            var model = lstQtChungTuChiTiet.FirstOrDefault(x => x.MlnsId == idParent && x.IdDonVi.Equals(idDonVi));
            if (model == null) return;
            model.TongCong += item.TongCong;
            model.DieuChinh += item.DieuChinh;
            CalculateParentDonVi(model.MlnsIdParent, idDonVi, item, lstQtChungTuChiTiet);
        }

        protected override void OnModelChanged()
        {
            base.OnModelChanged();
            OnPropertyChanged(nameof(DataLock));
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        private void OnExport(SalaryPrintType printType)
        {
            var lstChungTu = new List<TlQtChungTuNq104Model>();
            lstChungTu.Add(Model);
            var chungTu = lstChungTu.First();
            if (chungTu == null) return;
            RegularSettlementPrintDialogViewModel.BIsDetailView = true;
            RegularSettlementPrintDialogViewModel.ItemsChungTu = lstChungTu;
            RegularSettlementPrintDialogViewModel.ChungTuModel = chungTu;
            RegularSettlementPrintDialogViewModel.InDetailView = true;
            RegularSettlementPrintDialogViewModel.Thang = chungTu.Thang;
            RegularSettlementPrintDialogViewModel.Nam = chungTu.Nam;
            RegularSettlementPrintDialogViewModel.SNam = chungTu.Nam.ToString();
            RegularSettlementPrintDialogViewModel.MaDonVi = Model.MaDonVi;
            RegularSettlementPrintDialogViewModel.TenDonVi = Model.TenDonVi;
            RegularSettlementPrintDialogViewModel.Init();
            RegularSettlementPrintDialogViewModel.LoadData();
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    ExportResult results = RegularSettlementPrintDialogViewModel.ExportQtThuongXuyen(printType, ExportType.PDF);
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        _exportService.Open(result, ExportType.PDF);
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
                System.Windows.Forms.MessageBox.Show(Resources.ErrorExportReport, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}