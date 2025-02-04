using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewTransferData.NewAllowenceMapper
{
    public class NewAllowenceMapperIndexViewModel : GridViewModelBase<TlDmMapPcDetailModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmMapPcDetailService _tlDmMapPcDetailService;
        private readonly ITlDmPhuCapNq104Service _tlDmPhuCapService;
        private ICollectionView _pcMapperView;
        private int currentRow = -1;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_CHUYEN_DOI_DU_LIEU_ANH_XA_PHU_CAP;
        public override string GroupName => MenuItemContants.GROUP_TRANSFER_FROM_FOXPRO;
        public override string Title => "Ánh xạ phụ cấp";
        public override string Name => "Ánh xạ phụ cấp";
        public override string Description => "Khai báo ánh xạ phụ cấp";
        public override PackIconKind IconKind => PackIconKind.CogTransferOutline;
        public override Type ContentType => typeof(View.NewSalary.NewTransferData.NewAllowenceMapper.NewAllowenceMapper);

        private ObservableCollection<ComboboxItem> _phuCapItems;
        public ObservableCollection<ComboboxItem> PhuCapItems
        {
            get => _phuCapItems;
            set => SetProperty(ref _phuCapItems, value);
        }

        private string _searchPhuCapMap;
        public string SearchPhuCapMap
        {
            get => _searchPhuCapMap;
            set => SetProperty(ref _searchPhuCapMap, value);
        }

        public RelayCommand SearchCommand { get; }

        public NewAllowenceMapperIndexViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            ITlDmPhuCapNq104Service tlDmPhuCapService,
            ITlDmMapPcDetailService tlDmMapPcDetailService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;

            _tlDmMapPcDetailService = tlDmMapPcDetailService;
            _tlDmPhuCapService = tlDmPhuCapService;

            SearchCommand = new RelayCommand(o => _pcMapperView.Refresh());
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(10);
            SearchPhuCapMap = string.Empty;
            LoadPhuCap();
            LoadData();
        }

        private void LoadPhuCap()
        {
            try
            {
                var data = _tlDmPhuCapService.FindAll().OrderBy(x => x.MaPhuCap);
                PhuCapItems = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadData()
        {
            try
            {
                var data = _tlDmMapPcDetailService.FindAll().OrderBy(x => x.OldValue);
                Items = _mapper.Map<ObservableCollection<TlDmMapPcDetailModel>>(data);
                if (Items.Count > 0 && Items != null)
                {
                    SelectedItem = Items.FirstOrDefault();
                }
                foreach (var item in Items)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }
                _pcMapperView = CollectionViewSource.GetDefaultView(Items);
                _pcMapperView.Filter = PhuCapMapFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool PhuCapMapFilter(object obj)
        {
            bool result = true;
            var item = (TlDmMapPcDetailModel)obj;

            if (SearchPhuCapMap != null)
            {
                result &= item.OldValue.ToLower().Contains(SearchPhuCapMap.ToLower())
                    || item.MaPhuCap.ToLower().Contains(SearchPhuCapMap.ToLower())
                    || item.TenPhuCap.ToLower().Contains(SearchPhuCapMap.ToLower());
            }

            return result;
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlDmMapPcDetailModel tlDmMapPcDetailModel = (TlDmMapPcDetailModel)sender;
            if (args.PropertyName == nameof(tlDmMapPcDetailModel.IdPhuCap))
            {
                tlDmMapPcDetailModel.MaPhuCap = PhuCapItems.FirstOrDefault(x => x.Id == tlDmMapPcDetailModel.IdPhuCap).DisplayItem;
                tlDmMapPcDetailModel.TenPhuCap = PhuCapItems.FirstOrDefault(x => x.Id == tlDmMapPcDetailModel.IdPhuCap).HiddenValue;
            }
            tlDmMapPcDetailModel.IsModified = true;
            OnPropertyChanged(nameof(SelectedItem));
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnAdd()
        {
            base.OnAdd();
            if (Items.Count == 0 || SelectedItem == null)
            {
                TlDmMapPcDetailModel tlDmMapPcDetailModel = new TlDmMapPcDetailModel();
                tlDmMapPcDetailModel.PropertyChanged += DetailModel_PropertyChanged;
                Items.Add(tlDmMapPcDetailModel);
            }
            else
            {
                TlDmMapPcDetailModel sourceItem = SelectedItem;
                TlDmMapPcDetailModel targetItem = ObjectCopier.Clone(sourceItem);

                currentRow = Items.IndexOf(SelectedItem);
                targetItem.Id = Guid.Empty;
                targetItem.IsModified = true;
                targetItem.PropertyChanged += DetailModel_PropertyChanged;
                Items.Insert(currentRow + 1, targetItem);
            }
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            if (Items != null && Items.Count > 0 && SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
        }

        public override void OnSave()
        {
            base.OnSave();
            List<TlDmMapPcDetailModel> lstAdd = Items.Where(x => x.IsModified && !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null)).ToList();
            List<TlDmMapPcDetailModel> lstEdit = Items.Where(x => x.IsModified && !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            List<TlDmMapPcDetailModel> lstDelete = Items.Where(x => x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();

            if (lstAdd != null && lstAdd.Count > 0)
            {
                var lstPcMapAdd = _mapper.Map<List<TlDmMapPcDetail>>(lstAdd);
                _tlDmMapPcDetailService.AddRange(lstPcMapAdd);
            }

            if (lstEdit != null && lstEdit.Count > 0)
            {
                foreach (var item in lstEdit)
                {
                    TlDmMapPcDetail tlDmMapPcDetail = _mapper.Map<TlDmMapPcDetail>(item);
                    _tlDmMapPcDetailService.Update(tlDmMapPcDetail);
                } 
            }

            if (lstDelete != null && lstDelete.Count > 0)
            {
                foreach (var item in lstDelete)
                {
                    _tlDmMapPcDetailService.Delete(item.Id);
                }
            }

            LoadData();
        }
    }
}
