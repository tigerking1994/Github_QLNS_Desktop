using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexAllocation.ForexDeNghiThanhToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexDeNghiThanhToan
{
    public class SelectMLNSDialogViewModel: ViewModelBase
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhQtChuyenQuyetToanService _service;
        private readonly INsMucLucNganSachService _serviceMLNS;
        private readonly INsDonViService _nsDonViService;
        private ICollectionView _itemsCollectionView;

        public override string Name => "Mục lục ngân sách";
        public override string Title => "Chọn mục lục ngân sách";
        public override string Description => "Chi tiết mục lục ngân sách";
        public override Type ContentType => typeof(SelectMLNSDialog);

        private NsMucLucNganSach _itemsMLNSFilter;
        public NsMucLucNganSach ItemsMLNSFilter
        {
            get => _itemsMLNSFilter;
            set => SetProperty(ref _itemsMLNSFilter, value);
        }
        private List<NsMucLucNganSach> _items;
        public List<NsMucLucNganSach> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        private NsMucLucNganSach _selectedMLNS;
        public NsMucLucNganSach SelectedMLNS
        {
            get => _selectedMLNS;
            set => SetProperty(ref _selectedMLNS, value);
        }
        public RelayCommand SearchCommand { get; }

        public SelectMLNSDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhQtChuyenQuyetToanService service,
            INsMucLucNganSachService serviceMLNS)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _service = service;
            _serviceMLNS = serviceMLNS;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());

        }

        public override void Init()
        {
            LoadDefault();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsMLNSFilter = new NsMucLucNganSach();
        }


        public override void LoadData(params object[] args)
        {
            try
            {
                Items = new List<NsMucLucNganSach>();
                Items = _serviceMLNS.FindMLNSByNamLamViec(_sessionService.Current.YearOfWork).ToList();

                _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                _itemsCollectionView.Filter = Items_Filter;
                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NsMucLucNganSach item)
            {
                bool result = true;
                if (ItemsMLNSFilter != null)
                {
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.Lns)) result = result && item.Lns.ToLower().Contains(ItemsMLNSFilter.Lns.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.L)) result = result && item.L.ToLower().Contains(ItemsMLNSFilter.L.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.K)) result = result && item.K.ToLower().Contains(ItemsMLNSFilter.K.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.M)) result = result && item.M.ToLower().Contains(ItemsMLNSFilter.M.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.Tm)) result = result && item.Tm.ToLower().Contains(ItemsMLNSFilter.Tm.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.Ttm)) result = result && item.Ttm.ToLower().Contains(ItemsMLNSFilter.Ttm.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.Ng)) result = result && item.Ng.ToLower().Contains(ItemsMLNSFilter.Ng.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.Tng)) result = result && item.Tng.ToLower().Contains(ItemsMLNSFilter.Tng.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.MoTa)) result = result && item.MoTa.ToLower().Contains(ItemsMLNSFilter.MoTa.Trim().ToLower());
                }
                return result;
            }
            return false;
        }

        public override void OnClosing()
        {
            // Clear items
            if (!Items.IsEmpty()) Items.Clear();
        }
        public override void OnSave(object obj)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                e.Result = SelectedMLNS;
            }, (s, e) =>
            {
                IsLoading = false;

                if (e.Error == null)
                {
                    // Reload data
                    SelectedMLNS = _mapper.Map<NsMucLucNganSach>(e.Result);
                    SavedAction?.Invoke(SelectedMLNS);

                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);

                    var view = obj as Window;
                    if (view != null) view.Close();
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }
        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }
    }
}
