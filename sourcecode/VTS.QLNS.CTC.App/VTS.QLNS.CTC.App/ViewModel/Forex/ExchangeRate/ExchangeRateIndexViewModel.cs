using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanSuggestions;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Domain;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanManager;
using VTS.QLNS.CTC.App.Model.Control;
using System.Windows;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.QLDuAn;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ExchangeRate
{
    public class ExchangeRateIndexViewModel : GridViewModelBase<NhDmTiGiaModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhDmTiGiaService _service;

        private ICollectionView _itemsCollectionView;
        public override string Name => "Danh mục tỉ giá";
        public override string Title => "Danh mục tỉ giá";
        public override string Description => "Danh sách tỉ giá";
        public override Type ContentType => typeof(View.Forex.ExchangeRate.ExchangeRateIndex);
        public override PackIconKind IconKind => PackIconKind.Category;
        public override string FuncCode => NSFunctionCode.CATEGORY_FOREX_EXCHANGE_RATE;
        private NhDmTiGiaModel _itemsFilter;
        public NhDmTiGiaModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public ExchangeRateDialogViewModel ExchangeRateDialogViewModel { get; set; }

        public ExchangeRateIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INhDmTiGiaService nhDmTiGiaService,
            ExchangeRateDialogViewModel exchangeRateDialogViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _service = nhDmTiGiaService;

            SearchCommand = new RelayCommand(obj => { _itemsCollectionView.Refresh(); });
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate());
            ExchangeRateDialogViewModel = exchangeRateDialogViewModel;
        }

        public override void Init()
        {
            LoadDefault();
            LoadData();
        }

        private void LoadData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                Items = new ObservableCollection<NhDmTiGiaModel>();
                e.Result = _service.FindAll().OrderByDescending(x => x.DNgayTao).ToList();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(e.Result);
                    // Process when run completed. e.Result
                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }
                    _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                    _itemsCollectionView.Filter = Items_Filter;
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null) return;

            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                _service.Delete(SelectedItem.Id);
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            ExchangeRateDialogViewModel.Model = new NhDmTiGiaModel();
            ExchangeRateDialogViewModel.Init();
            ExchangeRateDialogViewModel.SavedAction = obj => this.OnRefresh();
            ExchangeRateDialogViewModel.ShowDialogHost();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                ExchangeRateDialogViewModel.Model = SelectedItem;
                ExchangeRateDialogViewModel.Init();
                ExchangeRateDialogViewModel.SavedAction = obj => this.OnRefresh();
                ExchangeRateDialogViewModel.ShowDialogHost();
            }
        }

        protected void OnViewDetail()
        {
            if (SelectedItem != null)
            {
                ExchangeRateDialogViewModel.Model = SelectedItem;
                ExchangeRateDialogViewModel.Init();
                ExchangeRateDialogViewModel.SavedAction = obj => this.OnRefresh();
                ExchangeRateDialogViewModel.ShowDialogHost();
            }
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhDmTiGiaModel item)
            {
                bool result = true;
                if (ItemsFilter != null)
                {
                    if (ItemsFilter.DNgayTao != null)
                    {
                        result &= item.DNgayTao.HasValue && item.DNgayTao.Value.Date == ItemsFilter.DNgayTao.Value.Date;
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.SMaTiGia))
                    {
                        result &= item.SMaTiGia != null && item.SMaTiGia.Contains(ItemsFilter.SMaTiGia, StringComparison.OrdinalIgnoreCase);
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.STenTiGia))
                    {
                        result &= item.STenTiGia != null && item.STenTiGia.Contains(ItemsFilter.STenTiGia, StringComparison.OrdinalIgnoreCase);
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.SMoTaTiGia))
                    {
                        result &= item.SMoTaTiGia != null && item.SMoTaTiGia.Contains(ItemsFilter.SMoTaTiGia, StringComparison.OrdinalIgnoreCase);
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.SMaTienTeGoc))
                    {
                        result &= item.SMaTienTeGoc != null && item.SMaTienTeGoc.Contains(ItemsFilter.SMaTienTeGoc, StringComparison.OrdinalIgnoreCase);
                    }
                }
                return result;
            }
            return false;
        }

        protected override void OnRefresh()
        {
            Init();
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhDmTiGiaModel();
        }

        private void OnRemoveFilter()
        {
            ItemsFilter = new NhDmTiGiaModel();
            LoadData();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (SelectedItem != null)
            {
                OnViewDetail();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
        }

        public override void Dispose()
        {
            base.Dispose();

            // Clear data
            _itemsCollectionView = null;
            Items.Clear();
        }
    }
}
