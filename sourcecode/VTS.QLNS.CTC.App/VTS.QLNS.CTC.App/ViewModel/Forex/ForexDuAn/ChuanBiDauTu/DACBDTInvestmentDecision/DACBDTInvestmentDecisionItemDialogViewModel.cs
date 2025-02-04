using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.View.Forex.ForexDuAn.ChuanBiDauTu.DACBDTInvestmentDecision;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.Windows.Controls;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.ChuanBiDauTu.DACBDTInvestmentDecision
{
    public class DACBDTInvestmentDecisionItemDialogViewModel : DetailViewModelBase<NhDaQdDauTuChiPhiModel, NhDaQdDauTuHangMucModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly INhDmLoaiCongTrinhService _nhDmLoaiCongTrinhService;
        private ObservableCollection<NhDaQdDauTuHangMucModel> _originItems;

        public override string Name => "Quyết định đầu tư - Chi tiết hạng mục";
        public override string Title => "Quyết định đầu tư - Chi tiết hạng mục";
        public override string Description => "Quyết định đầu tư - Chi tiết hạng mục";
        public override Type ContentType => typeof(DACBDTInvestmentDecisionItemDialog);
        public bool IsDetail { get; set; }
        public bool HasChanged => !ObjectCopier.ToJsonString(Items).Equals(ObjectCopier.ToJsonString(_originItems));
        public List<NhDaChuTruongDauTuHangMucModel> ChuTruongDauTuHangMucs { get; set; }
        public Action<object> CurrencyExchangeAction { get; set; }

        private ObservableCollection<ComboboxItem> _itemsLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> ItemsLoaiCongTrinh
        {
            get => _itemsLoaiCongTrinh;
            set => SetProperty(ref _itemsLoaiCongTrinh, value);
        }

        public RelayCommand AddQdDauTuHangMucCommand { get; }
        public RelayCommand DeleteQdDauTuHangMucCommand { get; }
        public RelayCommand ReOrderQdDauTuHangMucCommand { get; }

        public DACBDTInvestmentDecisionItemDialogViewModel(
            ILog logger,
            IMapper mapper,
            INhDmLoaiCongTrinhService nhDmLoaiCongTrinhService)
        {
            _logger = logger;
            _mapper = mapper;
            _nhDmLoaiCongTrinhService = nhDmLoaiCongTrinhService;

            SaveCommand = new RelayCommand(obj => OnSave(), obj => HasChanged);
            AddQdDauTuHangMucCommand = new RelayCommand(obj => OnAddQdDauTuHangMuc(obj), obj => (bool)obj || (SelectedItem != null));
            DeleteQdDauTuHangMucCommand = new RelayCommand(obj => OnDeleteQdDauTuHangMuc(), obj => SelectedItem != null);
            ReOrderQdDauTuHangMucCommand = new RelayCommand(obj => OnReOrderQdDauTuHangMuc(), obj => !Items.IsEmpty());
        }

        public override void Init()
        {
            LoadLoaiCongTrinh();
            LoadData();
        }

        private void LoadLoaiCongTrinh()
        {
            IEnumerable<NhDmLoaiCongTrinh> listLoaiCongTrinh = _nhDmLoaiCongTrinhService.FindAll();
            _itemsLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(listLoaiCongTrinh);
            OnPropertyChanged(nameof(ItemsLoaiCongTrinh));
        }

        public override void LoadData(params object[] args)
        {
            Items = new ObservableCollection<NhDaQdDauTuHangMucModel>();
            if (!ChuTruongDauTuHangMucs.IsEmpty())
            {
                if (Model.QdDauTuHangMucs.IsEmpty())
                {
                    // Nếu là thêm mới và chưa có hạng mục nào thì lấy danh sách của chủ trương đầu tư
                    Items = _mapper.Map<ObservableCollection<NhDaQdDauTuHangMucModel>>(ChuTruongDauTuHangMucs);
                    var refItemDictionary = Items.ToDictionary(x => x.Id, x => Guid.NewGuid());
                    foreach (var item in Items)
                    {
                        item.IIdQdDauTuChiPhiId = Model.Id;
                        item.IIdChuTruongDauTuHangMucId = item.Id;
                        item.Id = refItemDictionary[item.Id];
                        if (!item.IIdParentId.IsNullOrEmpty() && refItemDictionary.ContainsKey(item.IIdParentId.Value))
                        {
                            item.IIdParentId = refItemDictionary[item.IIdParentId.Value];
                        }
                        else
                        {
                            item.IIdParentId = null;
                        }
                        item.IsAdded = true;
                        item.IsModified = false;
                        item.PropertyChanged += QdDauTuHangMuc_PropertyChanged;
                    }
                }
                else
                {
                    // Trường hợp đã có hạng mục, những vẫn còn hạng mục chủ trương đầu tư chưa phân hết. Confirm xem là có nên hiển thị vào đây không?
                }
                OrderItems();
            }
            else
            {
                var data = _mapper.Map<IEnumerable<NhDaQdDauTuHangMucModel>>(Model.QdDauTuHangMucs).ToList();
                data.ForEach(x =>
                {
                    x.PropertyChanged += QdDauTuHangMuc_PropertyChanged;
                });
                var dataMap = _mapper.Map<List<NhDaQdDauTuHangMucModel>>(data).Select(x =>
                {
                    x.STenNguonVon = Model.STenNguonVon;
                    x.STenChiPhi = Model.STenChiPhi;
                    return x;
                }).ToList();
                Items = new ObservableCollection<NhDaQdDauTuHangMucModel>(dataMap);
            }

            UpdateTreeItems();
            _originItems = ObjectCopier.Clone(Items);
        }

        private void OnAddQdDauTuHangMuc(object obj)
        {
            if (Items == null) Items = new ObservableCollection<NhDaQdDauTuHangMucModel>();

            NhDaQdDauTuHangMucModel sourceItem = SelectedItem;
            NhDaQdDauTuHangMucModel targetItem = new NhDaQdDauTuHangMucModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!Items.IsEmpty())
            {
                if (sourceItem != null)
                {
                    currentRow = Items.IndexOf(sourceItem) + CountTreeChildItems(sourceItem);
                }
                else
                {
                    // Thêm vào cuối danh sách
                    currentRow = Items.Count() - 1;
                }
            }

            if (sourceItem != null)
            {
                targetItem.IIdParentId = isParent ? sourceItem.IIdParentId : sourceItem.Id;
            }
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.STenNguonVon = Model.STenNguonVon;
            targetItem.STenChiPhi = Model.STenChiPhi;
            targetItem.PropertyChanged += QdDauTuHangMuc_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);

            OrderItems(targetItem.IIdParentId);
            UpdateTreeItems();
            OnPropertyChanged(nameof(Items));
        }

        public void OnAddQdDauTuHangMuc(
            NhDaQdDauTuHangMucModel entity, 
            bool isParent = true)
        {
            if (Items == null) Items = new ObservableCollection<NhDaQdDauTuHangMucModel>();

            NhDaQdDauTuHangMucModel sourceItem = SelectedItem;
            NhDaQdDauTuHangMucModel targetItem = new NhDaQdDauTuHangMucModel();
            int currentRow = -1;
            if (!Items.IsEmpty())
            {
                if (sourceItem != null)
                {
                    currentRow = Items.IndexOf(sourceItem) + CountTreeChildItems(sourceItem);
                }
                else
                {
                    // Thêm vào cuối danh sách
                    currentRow = Items.Count() - 1;
                }
            }

            //KhaiPD
            foreach (var item in Items)
            {
                if(item.IIdGocId == entity.IIdParentId && entity.IIdParentId != null)
                {
                    targetItem.IIdParentId = item.Id;
                    break;
                }
            }

            //if (sourceItem != null)
            //{
            //    targetItem.IIdParentId = isParent ? sourceItem.IIdParentId : sourceItem.Id;
            //}
            targetItem.Id = Guid.NewGuid();
            targetItem.IIdGocId = entity.IIdGocId;
            targetItem.IsAdded = true;
            targetItem.IIdLoaiCongTrinhId = entity.IIdLoaiCongTrinhId;
            targetItem.IsSuggestion = entity.IsSuggestion;
            targetItem.SMaHangMuc = entity.SMaHangMuc;
            targetItem.SMaOrder = entity.SMaOrder;
            targetItem.STenHangMuc = entity.STenHangMuc;
            targetItem.STenNguonVon = Model.STenNguonVon;
            targetItem.STenChiPhi = Model.STenChiPhi;
            targetItem.PropertyChanged += QdDauTuHangMuc_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);

            OrderItems(targetItem.IIdParentId);
            UpdateTreeItems();
            OnPropertyChanged(nameof(Items));
        }

        private void OnDeleteQdDauTuHangMuc()
        {
            if (SelectedItem != null)
            {
                DeleteTreeItems(SelectedItem, !SelectedItem.IsDeleted);
            }
        }

        private void OnReOrderQdDauTuHangMuc()
        {
            foreach (var item in Items)
            {
                var parent = Items.FirstOrDefault(x => x.Id == item.IIdParentId);
                if (parent == null) item.IIdParentId = null;
            }
            OrderItems();
        }

        public override void OnSave()
        {
            SaveItems();
            DialogHost.Close("ForexInvestmentDecisionDialog");
        }

        public override void OnClose(object obj)
        {
            RemoveSusgestionItem();

            if (HasChanged)
            {
                var result = MessageBoxHelper.Confirm("Đồng chí có muốn lưu thông tin hạng mục đã được nhập không?");
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    SaveItems();
                }
            }
            DialogHost.Close("ForexInvestmentDecisionDialog");
        }

        public override void OnCellEditEnding(object obj)
        {
            base.OnCellEditEnding(obj);
            CurrencyExchangeAction?.Invoke(obj);
        }

        public void QdDauTuHangMuc_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedItem = (NhDaQdDauTuHangMucModel)e.Row.Item;
            if (e.Column.SortMemberPath.Equals(nameof(NhDaQdDauTuHangMucModel.FGiaTriUsd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaQdDauTuHangMucModel.FGiaTriEur)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaQdDauTuHangMucModel.FGiaTriVnd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaQdDauTuHangMucModel.FGiaTriNgoaiTeKhac)))
            {
                e.Cancel = !SelectedItem.CanEditValue;
            }
        }

        private void QdDauTuHangMuc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NhDaQdDauTuHangMucModel objectSender = (NhDaQdDauTuHangMucModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaQdDauTuHangMucModel.FGiaTriUsd)) ||
                e.PropertyName.Equals(nameof(NhDaQdDauTuHangMucModel.FGiaTriEur)) ||
                e.PropertyName.Equals(nameof(NhDaQdDauTuHangMucModel.FGiaTriVnd)) ||
                e.PropertyName.Equals(nameof(NhDaQdDauTuHangMucModel.FGiaTriNgoaiTeKhac)))
            {
                CalculateHangMuc();
            }
            if (!e.PropertyName.Equals(nameof(NhDaQdDauTuHangMucModel.IsHangCha)) &&
                !e.PropertyName.Equals(nameof(NhDaQdDauTuHangMucModel.CanEditValue)))
            {
                objectSender.IsModified = true;
            }
            OnPropertyChanged(nameof(HasChanged));
        }

        private void SaveItems()
        {
            //RemoveSusgestionItem();
            var data = _mapper.Map<IEnumerable<NhDaQdDauTuHangMucModel>>(Items).ToList();
            data.ForEach(x =>
            {
                x.PropertyChanged -= QdDauTuHangMuc_PropertyChanged;
            });
            SavedAction?.Invoke(data);
        }

        private void RemoveSusgestionItem()
        {
            int totalValue = Items?.Count() ?? 0;
            for (int i = totalValue - 1; i >= 0; i--)
            {
                var item = Items[i];
                if (item.IsSuggestion
                    && (item?.FGiaTriUsd == null || item?.FGiaTriUsd == 0
                    || item?.FGiaTriEur == null || item?.FGiaTriEur == 0
                    || item?.FGiaTriVnd == null || item?.FGiaTriVnd == 0
                    || item?.FGiaTriNgoaiTeKhac == null || item?.FGiaTriNgoaiTeKhac == 0
                    ))
                    Items.RemoveAt(i);
            }
        }

        private void OrderItems(Guid? parentId = null)
        {
            var childs = Items.Where(x => x.IIdParentId == parentId);
            if (!childs.IsEmpty())
            {
                var parent = Items.FirstOrDefault(x => x.Id == parentId);
                int index = 1;
                foreach (var child in childs)
                {
                    if (parent != null)
                    {
                        child.SMaOrder = string.Format("{0}-{1}", parent.SMaOrder, index.ToString("D2"));
                    }
                    else
                    {
                        child.SMaOrder = index.ToString("D2");
                    }
                    child.SMaHangMuc = StringUtils.ConvertMaOrder(child.SMaOrder);
                    OrderItems(child.Id);
                    index++;
                }
            }
        }

        private int CountTreeChildItems(NhDaQdDauTuHangMucModel currentItem)
        {
            int count = 0;
            var childs = Items.Where(x => x.IIdParentId == currentItem.Id);
            if (!childs.IsEmpty())
            {
                count += childs.Count();
                foreach (var item in childs)
                {
                    count += CountTreeChildItems(item);
                }
            }
            return count;
        }

        private void DeleteTreeItems(NhDaQdDauTuHangMucModel currentItem, bool status)
        {
            if (currentItem != null)
            {
                var items = Items;
                currentItem.IsDeleted = status;
                var childs = items.Where(x => x.IIdParentId == currentItem.Id);
                if (!childs.IsEmpty())
                {
                    foreach (var item in childs)
                    {
                        DeleteTreeItems(item, status);
                    }
                }
            }
        }

        private void UpdateTreeItems()
        {
            if (!Items.IsEmpty())
            {
                Items.ForAll(s => s.CanEditValue = !Items.Any(y => y.IIdParentId == s.Id));
                Items.ForAll(x => 
                {
                    // Là hàng cha nếu thỏa mãn một trong các điều kiện sau
                    // 1. Có parent id là null hoặc ko nhận phần tử nào là cha
                    // 2. Có phần tử con. CanEditValue = false
                    // 3. Có phần tử cùng cấp là hàng cha
                    if (x.IIdParentId.IsNullOrEmpty() || !Items.Any(y => y.Id == x.IIdParentId)) x.IsHangCha = true;
                    if (!x.CanEditValue) x.IsHangCha = true;
                    else if (Items.Any(y => y.IIdParentId == x.IIdParentId && !y.CanEditValue)) x.IsHangCha = true;
                });
            }
        }

        private void CalculateHangMuc()
        {
            var parents = Items.Where(x => x.IIdParentId.IsNullOrEmpty() || !Items.Any(y => y.Id == x.IIdParentId));
            foreach (var item in parents)
            {
                CalculateHangMuc(item);
            }
        }

        private void CalculateHangMuc(NhDaQdDauTuHangMucModel parentItem)
        {
            var childs = Items.Where(x => x.IIdParentId == parentItem.Id && !x.IsDeleted);
            if (!childs.IsEmpty())
            {
                foreach (var item in childs)
                {
                    CalculateHangMuc(item);
                }
                parentItem.FGiaTriUsd = childs.Sum(x => x.FGiaTriUsd);
                parentItem.FGiaTriEur = childs.Sum(x => x.FGiaTriEur);
                parentItem.FGiaTriVnd = childs.Sum(x => x.FGiaTriVnd);
                parentItem.FGiaTriNgoaiTeKhac = childs.Sum(x => x.FGiaTriNgoaiTeKhac);
            }
        }
    }
}
