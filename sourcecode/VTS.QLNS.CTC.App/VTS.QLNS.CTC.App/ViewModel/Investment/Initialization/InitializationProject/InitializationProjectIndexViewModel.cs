using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.Initialization.InitializationProject;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.Initialization.InitializationProject
{
    public class InitializationProjectIndexViewModel : GridViewModelBase<InitializationProjectModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private IVdtKtKhoiTaoService _vdtKtKhoiTaoService;
        private InitializationProjectDetail view;
        private ICollectionView _dataIndexFilter;

        public override string FuncCode => NSFunctionCode.INVESTMENT_INITIALIZATION_PROJECT_INDEX;
        public override string Name => "DANH SÁCH KHỞI TẠO THÔNG TIN DỰ ÁN";
        public override string Title => "Danh sách khởi tạo thông tin dự án";
        public override string Description => "Danh sách khởi tạo thông tin dự án";

        public override Type ContentType => typeof(View.Investment.Initialization.InitializationProject.InitializationProjectIndex);
        public override PackIconKind IconKind => PackIconKind.ViewList;
        public InitializationProjectDialogViewModel InitializationProjectDialogViewModel;
        public InitializationProjectDetailViewModel InitializationProjectDetailViewModel;

        private string _tenDuAnSearch;
        public string TenDuAnSearch
        {
            get => _tenDuAnSearch;
            set => SetProperty(ref _tenDuAnSearch, value);
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

        public InitializationProjectIndexViewModel(
          IMapper mapper,
          ISessionService sessionService,
          ILog logger,
          IVdtKtKhoiTaoService vdtKtKhoiTaoService,
          InitializationProjectDetailViewModel initializationProjectDetailViewModel,
          InitializationProjectDialogViewModel initializationProjectDialogViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _vdtKtKhoiTaoService = vdtKtKhoiTaoService;
            InitializationProjectDialogViewModel = initializationProjectDialogViewModel;
            InitializationProjectDetailViewModel = initializationProjectDetailViewModel;
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            SelectionDoubleClickCommand = new RelayCommand(o => OnShowDetail((InitializationProjectModel)o));
        }

        public override void Init()
        {
            try
            {
                LoadData();
                InitializationProjectDetailViewModel.ClosePopup += RefreshAfterClosePopup;
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
                InitializationProjectDialogViewModel.Model = new InitializationProjectDialogModel();
                //InitializationProjectDialogViewModel.Entity = null;
                InitializationProjectDialogViewModel.Init();
                InitializationProjectDialogViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                    OnShowDetail(obj);
                };
                var view = new InitializationProjectDialog
                {
                    DataContext = InitializationProjectDialogViewModel
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
                    InitializationProjectDialogViewModel.Model = new InitializationProjectDialogModel();
                    this.InitializationProjectDialogViewModel.Model.Id = SelectedItem.Id;
                    InitializationProjectDialogViewModel.Init();
                    InitializationProjectDialogViewModel.SavedAction = obj =>
                    {
                        this.LoadData();
                        OnShowDetail(obj);
                    };
                    var view = new InitializationProjectDialog
                    {
                        DataContext = InitializationProjectDialogViewModel
                    };
                    DialogHost.Show(view, "RootDialog");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnShowDetail(InitializationProjectModel itemDetail)
        {
            try
            {
                if (itemDetail == null)
                    return;
                InitializationProjectDetailViewModel.Model = itemDetail;
                InitializationProjectDetailViewModel.Init();
                InitializationProjectDetailViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                };
                view = new VTS.QLNS.CTC.App.View.Investment.Initialization.InitializationProject.InitializationProjectDetail
                {
                    DataContext = InitializationProjectDetailViewModel
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
                IEnumerable<KhoiTaoQuery> data = _vdtKtKhoiTaoService.FindByCondition(_sessionService.Current.YearOfWork);
                Items = _mapper.Map<ObservableCollection<Model.InitializationProjectModel>>(data);
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
            TenDuAnSearch = string.Empty;
            NamKhoiTao = null;
        }

        private bool DataFilter(object obj)
        {
            bool result = true;
            var item = (InitializationProjectModel)obj;
            if (NamKhoiTao != null && NamKhoiTao != 0)
                result = result && item.NamKhoiTao == NamKhoiTao;
            if (!string.IsNullOrEmpty(TenDuAnSearch))
                result = result && !string.IsNullOrEmpty(item.TenDuAn) && item.TenDuAn.ToLower().Contains(TenDuAnSearch.ToLower());
            return result;
        }
    }
}
