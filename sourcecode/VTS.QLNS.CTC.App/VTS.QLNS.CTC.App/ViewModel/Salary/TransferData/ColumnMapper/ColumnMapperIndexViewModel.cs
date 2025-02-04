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

namespace VTS.QLNS.CTC.App.ViewModel.Salary.TransferData.ColumnMapper
{
    public class ColumnMapperIndexViewModel : GridViewModelBase<TlMapColumnConfigModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlMapColumnConfigService _tlMapColumnConfigService;
        private readonly ITlDmPhuCapService _tlDmPhuCapService;
        private ICollectionView _columnMapperView;
        private int currentRow = -1;

        public override string FuncCode => NSFunctionCode.SALARY_CHUYEN_DOI_DU_LIEU_ANH_XA_COT_DU_LIEU;
        public override string GroupName => MenuItemContants.GROUP_TRANSFER_FROM_FOXPRO;
        public override string Title => "Ánh xạ cột dữ liệu";
        public override string Name => "Ánh xạ cột dữ liệu";
        public override string Description => "Khai báo ánh xạ cột";
        public override PackIconKind IconKind => PackIconKind.CogTransferOutline;
        public override Type ContentType => typeof(View.Salary.TransferData.ColumnMapper.ColumnMapperIndex);

        private ObservableCollection<ComboboxItem> _lstCotCu;
        public ObservableCollection<ComboboxItem> LstCotCu
        {
            get => _lstCotCu;
            set => SetProperty(ref _lstCotCu, value);
        }

        private ObservableCollection<ComboboxItem> _types;
        public ObservableCollection<ComboboxItem> Types
        {
            get => _types;
            set => SetProperty(ref _types, value);
        }

        private ComboboxItem _selectedType;
        public ComboboxItem SelectedType
        {
            get => _selectedType;
            set {
                SetProperty(ref _selectedType, value);
                LoadData();
            }
        }

        private ObservableCollection<ComboboxItem> _lstCotMoi;
        public ObservableCollection<ComboboxItem> LstCotMoi
        {
            get => _lstCotMoi;
            set => SetProperty(ref _lstCotMoi, value);
        }

        private string _searchColumn;
        public string SearchColumn
        {
            get => _searchColumn;
            set => SetProperty(ref _searchColumn, value);
        }

        public RelayCommand ResetCommand { get; }
        public RelayCommand SearchCommand { get; }

        public ColumnMapperIndexViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            ITlMapColumnConfigService tlMapColumnConfigService,
            ITlDmPhuCapService tlDmPhuCapService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;

            _tlMapColumnConfigService = tlMapColumnConfigService;
            _tlDmPhuCapService = tlDmPhuCapService;

            ResetCommand = new RelayCommand(o => OnReset());
            SearchCommand = new RelayCommand(o => _columnMapperView.Refresh());
        }

        public override void Init()
        {
            base.Init();
            LoadListColumn();
            LoadListTypes();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var data = _tlMapColumnConfigService.FindAll().Where(x => x.Mau.ToString().Equals(SelectedType.ValueItem)).OrderBy(x => x.OldColumn);
                Items = _mapper.Map<ObservableCollection<TlMapColumnConfigModel>>(data);
                if (Items.Count > 0 && Items != null)
                {
                    SelectedItem = Items.FirstOrDefault();
                }
                foreach (var item in Items)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }
                _columnMapperView = CollectionViewSource.GetDefaultView(Items);
                _columnMapperView.Filter = ColumnMapperFiter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ColumnMapperFiter(object obj)
        {
            bool result = true;
            var item = (TlMapColumnConfigModel)obj;

            if (SearchColumn != null)
            {
                if (!string.IsNullOrEmpty(item.NewColumn))
                {
                    result &= item.OldColumn.ToLower().Contains(SearchColumn.ToLower())
                    || item.NewColumn.ToLower().Contains(SearchColumn.ToLower());
                }
                else
                {
                    result &= item.OldColumn.ToLower().Contains(SearchColumn.ToLower());
                }
            }
            if (SelectedType != null)
            {
                result &= item.Mau.ToString().Equals(SelectedType.ValueItem);
            }
            return result;
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TlMapColumnConfigModel tlMapColumnConfigModel = (TlMapColumnConfigModel)sender;
            if (e.PropertyName == nameof(tlMapColumnConfigModel.IsMapPhuCap))
            {
                if ((bool)tlMapColumnConfigModel.IsMapPhuCap)
                {
                    tlMapColumnConfigModel.IsMapValue = !tlMapColumnConfigModel.IsMapPhuCap;
                    tlMapColumnConfigModel.UsePhuCapValue = !tlMapColumnConfigModel.IsMapPhuCap;
                }
            }
            tlMapColumnConfigModel.IsModified = true;
            OnPropertyChanged(nameof(SelectedItem));
            OnPropertyChanged(nameof(Items));
        }

        private void LoadListColumn()
        {
            List<string> lstOldColumn = typeof(OldColumnName).GetAllPublicConstantValues<string>();
            LstCotCu = new ObservableCollection<ComboboxItem>();
            LstCotMoi = new ObservableCollection<ComboboxItem>();
            foreach (var item in lstOldColumn)
            {
                LstCotCu.Add(new ComboboxItem(item, item));
            }

            TlDmCanBo tlDmCanBo = new TlDmCanBo();
            var lstProperty = tlDmCanBo.GetType().GetProperties();
            foreach (var item in lstProperty)
            {
                LstCotMoi.Add(new ComboboxItem(item.Name, item.Name));
            }
            var lstPhuCap = _tlDmPhuCapService.FindAll();
            var lstMaPhuCap = lstPhuCap.Select(x => x.MaPhuCap).ToList();
            foreach (var item in lstMaPhuCap)
            {
                LstCotMoi.Add(new ComboboxItem(item, item));
            }
        }

        private void LoadListTypes()
        {
            Types = new ObservableCollection<ComboboxItem>();
            Types.Add(new ComboboxItem("Mẫu 1", "1"));
            Types.Add(new ComboboxItem("Mẫu 2", "2"));
            SelectedType = Types.First();
        }

        protected override void OnAdd()
        {
            base.OnAdd();
            if (Items.Count == 0 || SelectedItem == null)
            {
                TlMapColumnConfigModel tlMapColumnConfigModel = new TlMapColumnConfigModel();
                tlMapColumnConfigModel.PropertyChanged += DetailModel_PropertyChanged;
                Items.Add(tlMapColumnConfigModel);
                SelectedItem = tlMapColumnConfigModel;
            }
            else
            {
                TlMapColumnConfigModel sourceItem = SelectedItem;
                TlMapColumnConfigModel targetItem = ObjectCopier.Clone(sourceItem);

                currentRow = Items.IndexOf(SelectedItem);
                targetItem.Id = Guid.Empty;
                targetItem.IsModified = true;
                targetItem.PropertyChanged += DetailModel_PropertyChanged;
                Items.Insert(currentRow + 1, targetItem);
                SelectedItem = targetItem;
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
            List<TlMapColumnConfigModel> lstAdd = Items.Where(x => x.IsModified && !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null)).ToList();
            List<TlMapColumnConfigModel> lstEdit = Items.Where(x => x.IsModified && !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            List<TlMapColumnConfigModel> lstDelete = Items.Where(x => x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();

            if (lstAdd != null && lstAdd.Count > 0)
            {
                var lstPcMapAdd = _mapper.Map<List<TlMapColumnConfig>>(lstAdd);
                _tlMapColumnConfigService.AddRange(lstPcMapAdd);
            }

            if (lstEdit != null && lstEdit.Count > 0)
            {
                foreach (var item in lstEdit)
                {
                    TlMapColumnConfig tlDmMapPcDetail = _mapper.Map<TlMapColumnConfig>(item);
                    _tlMapColumnConfigService.Update(tlDmMapPcDetail);
                }
            }

            if (lstDelete != null && lstDelete.Count > 0)
            {
                foreach (var item in lstDelete)
                {
                    _tlMapColumnConfigService.Delete(item.Id);
                }
            }

            LoadData();
        }

        private void OnReset()
        {
            SelectedItem.OldColumn = null;
            SelectedItem.NewColumn = null;
            SelectedItem.IsMapPhuCap = false;
            SelectedItem.IsMapValue = false;
            SelectedItem.UsePhuCapValue = false;
            SelectedItem.MapExpression = null;
            SelectedItem.IsModified = true;
            OnPropertyChanged(nameof(SelectedItem));
        }
    }
}
