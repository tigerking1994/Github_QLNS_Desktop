using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Utility;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAPlanImport;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAPlanImport
{
    public class DANHPhuongAnNhapKhauItemDialogViewModel : DetailViewModelBase<NhDaGoiThauChiPhiModel, NhDaGoiThauHangMucModel>
    {
        private readonly IMapper _mapper;
        private ObservableCollection<NhDaGoiThauHangMucModel> _originItems;

        public override string Name => "Phương án nhập khẩu - Chi tiết hạng mục";
        public override string Title => "Phương án nhập khẩu - Chi tiết hạng mục";
        public override string Description => "Phương án nhập khẩu - Chi tiết hạng mục";
        public override Type ContentType => typeof(DANHPhuongAnNhapKhauItemDialog);
        public bool IsDetail { get; set; }
        public bool IsAdded { get; set; }
        public bool IsEnableSoCuChuongTrinh { get; set; }
        public bool IsEnableSoCuQdDauTu { get; set; }
        public bool HasChanged => !ObjectCopier.ToJsonString(Items).Equals(ObjectCopier.ToJsonString(_originItems));

        public List<NhDaGoiThauHangMucModel> NhDaGoiThauHangMucs { get; set; }
        public Action<object> CurrencyExchangeAction { get; set; }

        public RelayCommand AddGoiThauHangMucCommand { get; }
        public RelayCommand DeleteGoiThauHangMucCommand { get; }

        public DANHPhuongAnNhapKhauItemDialogViewModel(IMapper mapper)
        {
            _mapper = mapper;

            SaveCommand = new RelayCommand(obj => OnSave(), obj => HasChanged);
            AddGoiThauHangMucCommand = new RelayCommand(obj => OnAddGoiThauHangMuc(obj), obj => (bool)obj || (SelectedItem != null));
            DeleteGoiThauHangMucCommand = new RelayCommand(obj => OnDeleteGoiThauHangMuc(), obj => SelectedItem != null);
        }

        public override void Init()
        {
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            Items = new ObservableCollection<NhDaGoiThauHangMucModel>();
            var data = _mapper.Map<IEnumerable<NhDaGoiThauHangMucModel>>(Model.GoiThauHangMucs).OrderBy(s => s.SMaOrder).ToList();
            data.ForEach(x =>
            {
                x.PropertyChanged += HangMuc_PropertyChanged;
            });
            Items = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(data);
            UpdateTreeItems();
            _originItems = ObjectCopier.Clone(Items);
            _originItems = new ObservableCollection<NhDaGoiThauHangMucModel>(_originItems.OrderBy(i => i.SMaOrder));
        }

        private void OnAddGoiThauHangMuc(object obj)
        {
            if (Items == null) Items = new ObservableCollection<NhDaGoiThauHangMucModel>();

            NhDaGoiThauHangMucModel sourceItem = SelectedItem;
            NhDaGoiThauHangMucModel targetItem = new NhDaGoiThauHangMucModel();
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
                targetItem.IIdGoiThauChiPhiId = sourceItem.IIdGoiThauChiPhiId;
            }
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.PropertyChanged += HangMuc_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);

            OrderItems(targetItem.IIdParentId);
            UpdateTreeItems();
            OnPropertyChanged(nameof(Items));
        }

        public void OnAddGoiThauHangMuc(
            NhDaGoiThauHangMucModel entity,
            bool isParent = true)
        {
            if (Items == null) Items = new ObservableCollection<NhDaGoiThauHangMucModel>();

            NhDaGoiThauHangMucModel targetItem = new NhDaGoiThauHangMucModel();
            int currentRow = -1;
            targetItem.Id = entity.Id;
            targetItem.IIdParentId = entity.IIdParentId;
            targetItem.IsAdded = true;
            targetItem.IIdQdDauTuHangMucId = entity.IIdQdDauTuHangMucId;
            targetItem.IsSuggestion = entity.IsSuggestion;
            targetItem.SMaHangMuc = entity.SMaHangMuc;
            targetItem.SMaOrder = entity.SMaOrder;
            targetItem.STenHangMuc = entity.STenHangMuc;
            targetItem.FTienGoiThauUsd = entity.FGiaTriUsd;
            targetItem.FTienGoiThauVnd = entity.FGiaTriVnd;
            targetItem.FTienGoiThauEur = entity.FGiaTriEur;
            targetItem.FTienGoiThauNgoaiTeKhac = entity.FGiaTriNgoaiTeKhac;
            targetItem.PropertyChanged += HangMuc_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);
            Items = new ObservableCollection<NhDaGoiThauHangMucModel>(Items.OrderBy(h => h.SMaHangMuc));
            OrderItems(targetItem.IIdParentId);
            UpdateTreeItems();         
        }

        public void HangMuc_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedItem = (NhDaGoiThauHangMucModel)e.Row.Item;
            if (e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriUsd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriEur)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriVnd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriNgoaiTeKhac)))
            {
                e.Cancel = !SelectedItem.CanEditValue;
            }
        }

        private void HangMuc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NhDaGoiThauHangMucModel objectSender = (NhDaGoiThauHangMucModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriUsd)) ||
                e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriEur)) ||
                e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriVnd)) ||
                e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriNgoaiTeKhac)))
            {
                CalculateHangMuc();
            }
            if (!e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.IsHangCha)) &&
                !e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.CanEditValue)))
            {
                objectSender.IsModified = true;
            }
            OnPropertyChanged(nameof(HasChanged));
        }

        private void OnDeleteGoiThauHangMuc()
        {
            if (SelectedItem != null)
            {
                DeleteTreeItems(SelectedItem, !SelectedItem.IsDeleted);
            }
        }

        private void DeleteTreeItems(NhDaGoiThauHangMucModel currentItem, bool status)
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

        private int CountTreeChildItems(NhDaGoiThauHangMucModel currentItem)
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

        private void CalculateHangMuc()
        {
            var parents = Items.Where(x => x.IIdParentId.IsNullOrEmpty() || !Items.Any(y => y.Id == x.IIdParentId));
            foreach (var item in parents)
            {
                CalculateHangMuc(item);
            }
        }

        private void CalculateHangMuc(NhDaGoiThauHangMucModel parentItem)
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

        public override void OnSave()
        {
            SaveItems();
            DialogHost.Close("NHPhuongAnNhapKhauItemDialog");
        }

        public override void OnClose(object obj)
        {         
            if (HasChanged)
            {
                var result = MessageBoxHelper.Confirm("Đồng chí có muốn lưu thông tin hạng mục đã được nhập không?");
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    SaveItems();
                }
            }
            DialogHost.Close("NHPhuongAnNhapKhauItemDialog");
        }

        private void SaveItems()
        {
            //RemoveSusgestionItem();
            var data = _mapper.Map<IEnumerable<NhDaGoiThauHangMucModel>>(Items).ToList();
            data.ForEach(x =>
            {
                x.PropertyChanged -= HangMuc_PropertyChanged;
            });
            SavedAction?.Invoke(data);
        }

        public override void OnCellEditEnding(object obj)
        {
            base.OnCellEditEnding(obj);
            CurrencyExchangeAction?.Invoke(obj);
        }
    }
}
