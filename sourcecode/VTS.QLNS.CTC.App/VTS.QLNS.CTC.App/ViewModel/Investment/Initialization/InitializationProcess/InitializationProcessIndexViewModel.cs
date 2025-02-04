using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.Initialization.InitializationProcess;
using VTS.QLNS.CTC.App.View.Investment.Initialization.InitializationProject;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.Initialization.InitializationProcess
{
    public class InitializationProcessIndexViewModel : GridViewModelBase<InitializationProcessModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private IVdtKtKhoiTaoDuLieuService _vdtKtKhoiTaoService;
        private IVdtKtKhoiTaoDuLieuChiTietThanhToanService _detailContractService;
        private InitializationProcessDetail view;
        private ICollectionView _dataIndexFilter;
        private IExportService _exportService;
        private IVdtKtKhoiTaoDuLieuChiTietService _chungTuChiTietService;
        private InitializationProcessImport _importView;

        public override string FuncCode => NSFunctionCode.INVESTMENT_INITIALIZATION_PROJECT_INDEX;
        public override string Name => "DANH SÁCH KHỞI TẠO THÔNG TIN";
        public override string Title => "Danh sách khởi tạo thông tin";
        public override string Description => "Danh sách khởi tạo thông tin";

        public override Type ContentType => typeof(View.Investment.Initialization.InitializationProcess.InitializationProcessIndex);
        public override PackIconKind IconKind => PackIconKind.ViewList;
        public InitializationProcessDialogViewModel InitializationProcessDialogViewModel;
        public InitializationProcessDetailViewModel InitializationProcessDetailViewModel;
        public InitializationProcessImportViewModel InitializationProcessImportViewModel;

        private string _tenDonViSearch;
        public string TenDonViSearch
        {
            get => _tenDonViSearch;
            set => SetProperty(ref _tenDonViSearch, value);
        }

        private int? _namKhoiTao;
        public int? NamKhoiTao
        {
            get => _namKhoiTao;
            set => SetProperty(ref _namKhoiTao, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand SelectionDoubleClickCommand { get; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ImportCommand { get; }

        public InitializationProcessIndexViewModel(
          IMapper mapper,
          ISessionService sessionService,
          IExportService exportService,
          IVdtKtKhoiTaoDuLieuChiTietService chungTuChiTietService,
          IVdtKtKhoiTaoDuLieuChiTietThanhToanService detailContractService,
          ILog logger,
          IVdtKtKhoiTaoDuLieuService vdtKtKhoiTaoService,
          InitializationProcessDialogViewModel initializationProcessDialogViewModel,
          InitializationProcessImportViewModel initializationProcessImportViewModel,
          InitializationProcessDetailViewModel initializationProcessDetailViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _vdtKtKhoiTaoService = vdtKtKhoiTaoService;
            _chungTuChiTietService = chungTuChiTietService;
            _detailContractService = detailContractService;
            _exportService = exportService;

            InitializationProcessDialogViewModel = initializationProcessDialogViewModel;
            InitializationProcessDetailViewModel = initializationProcessDetailViewModel;
            InitializationProcessImportViewModel = initializationProcessImportViewModel;

            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            SelectionDoubleClickCommand = new RelayCommand(o => OnShowDetail((InitializationProcessModel)o, true));
            ExportCommand = new RelayCommand(obj => OnExport());
            ImportCommand = new RelayCommand(obj => OnImport());
        }

        private void OnImport()
        {
            try
            {
                InitializationProcessImportViewModel.Init();
                InitializationProcessImportViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                    OnShowDetail((InitializationProcessModel)obj);
                };
                _importView = new InitializationProcessImport
                {
                    DataContext = InitializationProcessImportViewModel
                };
                _importView.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnExport()
        {
            if (Items == null || Items.Where(n => n.Selected).Count() == 0)
                return;

            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KT, ExportFileName.EPT_VDT_TONGHOPKHOITAO);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    foreach (InitializationProcessModel item in Items.Where(n => n.Selected).ToList())
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        List<KhoiTaoDuLieuChiTietQuery> list = _chungTuChiTietService.FindDataKhoiTaoChiTiet(item.Id.ToString()).ToList();
                        List<InitializationProcessDetailModel> listData = _mapper.Map<List<InitializationProcessDetailModel>>(list);
                        data.Add("Items", listData);

                        fileNamePrefix = string.Format("eptKhoiTaoDuAn_{0}_{1}", item.IIdMaDonVi, item.NamKhoiTao);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<InitializationProcessDetailModel>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
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

        public override void Init()
        {
            try
            {
                LoadData();
                InitializationProcessDetailViewModel.ClosePopup += RefreshAfterClosePopup;
                InitializationProcessImportViewModel.OpenDetail += OpenDetailAfterImport;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OpenDetailAfterImport(object sender, EventArgs e)
        {
            try
            {
                _importView.Close();
                LoadData();
                OnShowDetail((InitializationProcessModel)sender);
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
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            try
            {
                InitializationProcessDialogViewModel.Model = new InitializationProcessModel();
                InitializationProcessDialogViewModel.Init();
                InitializationProcessDialogViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                    OnShowDetail(obj);
                };
                var view = new InitializationProcessDialog
                {
                    DataContext = InitializationProcessDialogViewModel
                };
                DialogHost.Show(view, "RootDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnUpdate()
        {
            try
            {
                if (SelectedItem != null)
                {
                    InitializationProcessDialogViewModel.Model = new InitializationProcessModel();
                    this.InitializationProcessDialogViewModel.Model.Id = SelectedItem.Id;
                    InitializationProcessDialogViewModel.Init();
                    InitializationProcessDialogViewModel.SavedAction = obj =>
                    {
                        this.LoadData();
                        OnShowDetail(obj);
                    };
                    var view = new InitializationProcessDialog
                    {
                        DataContext = InitializationProcessDialogViewModel
                    };
                    DialogHost.Show(view, "RootDialog");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnShowDetail(InitializationProcessModel itemDetail, bool bIsDetail = false)
        {
            try
            {
                if (itemDetail == null)
                    return;
                InitializationProcessDetailViewModel.BIsDetail = bIsDetail;
                InitializationProcessDetailViewModel.Model = itemDetail;
                InitializationProcessDetailViewModel.Init();
                InitializationProcessDetailViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                };
                view = new VTS.QLNS.CTC.App.View.Investment.Initialization.InitializationProcess.InitializationProcessDetail
                {
                    DataContext = InitializationProcessDetailViewModel
                };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var currentIdDonVi = _sessionService.Current.IdDonVi;
                IEnumerable<KhoiTaoDuLieuQuery> data = _vdtKtKhoiTaoService.FindByCondition(_sessionService.Current.YearOfWork);
                Items = _mapper.Map<ObservableCollection<Model.InitializationProcessModel>>(data);
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

        protected override void OnDelete()
        {
            try
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(Resources.ConfirmDeleteUsers, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (SelectedItem != null)
                    {
                        _detailContractService.DeleteByKhoiTaoDuLieuId(SelectedItem.Id);
                        _vdtKtKhoiTaoService.Delete(SelectedItem.Id);
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSearch()
        {
            _dataIndexFilter.Refresh();
            OnPropertyChanged(nameof(Items));
        }

        private void OnResetFilter()
        {
            TenDonViSearch = string.Empty;
            NamKhoiTao = null;
        }

        private bool DataFilter(object obj)
        {
            bool result = true;
            var item = (InitializationProcessModel)obj;
            if (NamKhoiTao != null && NamKhoiTao != 0)
                result = result && item.NamKhoiTao == NamKhoiTao;
            if (!string.IsNullOrEmpty(TenDonViSearch))
                result = result && !string.IsNullOrEmpty(item.TenDonVi) && item.TenDonVi.ToLower().Contains(TenDonViSearch.Trim().ToLower());
            return result;
        }
    }
}
