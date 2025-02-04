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
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTForexContractInfo;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTForexContractInfo
{
    public class ForexContractInfoItemDialogViewModel : DetailViewModelBase<NhDaHopDongChiPhiModel, NhDaHopDongHangMucModel>
    {
        private readonly IMapper _mapper;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private ObservableCollection<NhDaHopDongHangMucModel> _originItems;

        public override string Name => "Hợp đồng ngoại thương - Thông tin danh mục";
        public override string Title => "Hợp đồng ngoại thương - Thông tin danh mục";
        public override string Description => "Hợp đồng ngoại thương - Thông tin danh mục";
        public override Type ContentType => typeof(ForexContractInfoItemDialog);
        public bool IsDetail { get; set; }
        public bool IsAdded { get; set; }
        public bool IsEnableSoCuChuongTrinh { get; set; }
        public bool HasChanged => !ObjectCopier.ToJsonString(Items).Equals(ObjectCopier.ToJsonString(_originItems));
        private IEnumerable<NhDmTiGiaChiTiet> _lstTiGiaChiTiet;
        public IEnumerable<NhDmTiGiaChiTiet> LstTiGiaChiTiet
        {
            get => _lstTiGiaChiTiet;
            set => SetProperty(ref _lstTiGiaChiTiet, value);
        }
        private string _sMaTienTeGoc;
        public string SMaTienTeGoc
        {
            get => _sMaTienTeGoc;
            set => SetProperty(ref _sMaTienTeGoc, value);
        }

        private string _sMaTienTeQuyDoi;
        public string SMaTienTeQuyDoi
        {
            get => _sMaTienTeQuyDoi;
            set => SetProperty(ref _sMaTienTeQuyDoi, value);
        }

        private List<ComboboxItem> _itemsThanhToanBang;
        public List<ComboboxItem> ItemsThanhToanBang
        {
            get => _itemsThanhToanBang;
            set => SetProperty(ref _itemsThanhToanBang, value);
        }

        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set
            {
                if (SetProperty(ref _fTiGiaNhap, value))
                {
                    if (Items != null)
                    {
                        CalculateHangMuc();
                    }
                }
            }
        }

        public List<NhDaHopDongHangMucModel> NhDaGoiThauHangMucs { get; set; }
        public Action<object> CurrencyExchangeAction { get; set; }

        public RelayCommand AddGoiThauHangMucCommand { get; }
        public RelayCommand DeleteGoiThauHangMucCommand { get; }

        public ForexContractInfoItemDialogViewModel(IMapper mapper , INhDmTiGiaService nhDmTiGiaService)
        {
            _mapper = mapper;
            _nhDmTiGiaService = nhDmTiGiaService;
            SaveCommand = new RelayCommand(obj => OnSave(), obj => HasChanged);
            AddGoiThauHangMucCommand = new RelayCommand(obj => OnAddGoiThauHangMuc(obj), obj => (bool)obj || (SelectedItem != null));
            DeleteGoiThauHangMucCommand = new RelayCommand(obj => OnDeleteGoiThauHangMuc(), obj => SelectedItem != null);
        }

        public override void Init()
        {
            LoadNoiDungChi();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            Items = new ObservableCollection<NhDaHopDongHangMucModel>();
            var data = _mapper.Map<IEnumerable<NhDaHopDongHangMucModel>>(Model.HopDongHangMucs).OrderBy(s => s.SMaOrder).ToList();
            data.ForEach(x =>
            {
                x.ItemsThanhToanBang = ItemsThanhToanBang;
                x.SelectThanhToanBang = ItemsThanhToanBang.Where(y => y.ValueItem == x.SThanhToanBang || y.ValueItem == (x.SelectThanhToanBang != null ? x.SelectThanhToanBang.ValueItem : "-1")).FirstOrDefault();
                x.PropertyChanged += HangMuc_PropertyChanged;
            });
            Items = _mapper.Map<ObservableCollection<NhDaHopDongHangMucModel>>(data);
            UpdateTreeItems();
            CalculateHangMuc();
            _originItems = ObjectCopier.Clone(Items);
            _originItems = new ObservableCollection<NhDaHopDongHangMucModel>(_originItems.OrderBy(i => i.SMaOrder));
        }

        private void LoadNoiDungChi()
        {
            ComboboxItem USD = new ComboboxItem("Chi bằng USD", "USD");
            ComboboxItem VND = new ComboboxItem("Chi bằng VND", "VND");
            ItemsThanhToanBang = new List<ComboboxItem>() { USD, VND };
            if (SMaTienTeGoc != null && SMaTienTeGoc != "USD")
            {
                ComboboxItem NTK = new ComboboxItem("Chi bằng "+ SMaTienTeGoc.ToUpper(), "NTK");
                ItemsThanhToanBang.Add(NTK);
            }else if (SMaTienTeQuyDoi != null && SMaTienTeQuyDoi != "USD")
            {
                ComboboxItem NTK = new ComboboxItem("Chi bằng " + SMaTienTeQuyDoi.ToUpper(), "NTK");
                ItemsThanhToanBang.Add(NTK);
            }
            OnPropertyChanged(nameof(ItemsThanhToanBang));
        }

        private void OnAddGoiThauHangMuc(object obj)
        {
            if (Items == null) Items = new ObservableCollection<NhDaHopDongHangMucModel>();

            NhDaHopDongHangMucModel sourceItem = SelectedItem;
            NhDaHopDongHangMucModel targetItem = new NhDaHopDongHangMucModel();
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
                targetItem.IIdHopDongChiPhiId = sourceItem.IIdHopDongChiPhiId;
            }
            else
                targetItem.IIdHopDongChiPhiId = Model.Id;
            targetItem.IsAdded = true;
            targetItem.Id = Guid.NewGuid();
            targetItem.ItemsThanhToanBang = ItemsThanhToanBang;
            targetItem.PropertyChanged += HangMuc_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);

            OrderItems(targetItem.IIdParentId);
            UpdateTreeItems();
            OnPropertyChanged(nameof(Items));
            CalculateHangMuc();
        }

        public void OnAddGoiThauHangMuc(
            NhDaHopDongHangMucModel entity,
            bool isParent = true)
        {
            if (Items == null) Items = new ObservableCollection<NhDaHopDongHangMucModel>();

            NhDaHopDongHangMucModel sourceItem = SelectedItem;
            NhDaHopDongHangMucModel targetItem = new NhDaHopDongHangMucModel();
            int currentRow = -1;
            sourceItem.FGiaTriUsd = null;
            sourceItem.FGiaTriVnd = null;
            sourceItem.FGiaTriEur = null;
            sourceItem.FGiaTriNgoaiTeKhac = null;
            sourceItem.FDonGia = null;
            sourceItem.ISoLuong = null;
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
                targetItem.IIdHopDongChiPhiId = sourceItem.IIdHopDongChiPhiId;
            }
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            //targetItem.IIdQdDauTuHangMucId = entity.IIdQdDauTuHangMucId;
            //targetItem.IsSuggestion = entity.IsSuggestion;
            targetItem.SMaHangMuc = entity.SMaHangMuc;
            targetItem.SMaOrder = entity.SMaOrder;
            targetItem.STenHangMuc = entity.STenHangMuc;
            targetItem.ItemsThanhToanBang = ItemsThanhToanBang;
            targetItem.PropertyChanged += HangMuc_PropertyChanged;

            Items.Insert(currentRow + 1, targetItem);
            Items = new ObservableCollection<NhDaHopDongHangMucModel>(Items.OrderBy(h => h.SMaHangMuc));
            
            OrderItems(targetItem.IIdParentId);
            UpdateTreeItems();
            OnPropertyChanged(nameof(Items));
        }
        // bấm tại dòng
        public void HangMuc_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedItem = (NhDaHopDongHangMucModel)e.Row.Item;
            if (SelectedItem.IsHangCha)
            {
                var lstChilren = Items.Where(x => x.IIdParentId == SelectedItem.Id && x.IsDeleted != true);
                if (lstChilren.Count() != 0)
                {
                    if (e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FDonGia)) ||
                    e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.ISoLuong)) ||
                    e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.SDonViTinh)))
                    {
                        e.Cancel = true;
                    }
                }

            }
        }
        // bấm khác dòng
        private void HangMuc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NhDaHopDongHangMucModel objectSender = (NhDaHopDongHangMucModel)sender;
            if (e.PropertyName == nameof(NhDaHopDongHangMucModel.FDonGia) ||
                e.PropertyName == nameof(NhDaHopDongHangMucModel.ISoLuong) ||
                e.PropertyName == nameof(NhDaHopDongHangMucModel.SelectThanhToanBang))
            {
                CalculateHangMuc();
            }
            OnPropertyChanged(nameof(HasChanged));
        }

        private void OnDeleteGoiThauHangMuc()
        {
            if (SelectedItem != null)
            {
                DeleteTreeItems(SelectedItem, !SelectedItem.IsDeleted);
            }
            CalculateHangMuc();
        }

        private void DeleteTreeItems(NhDaHopDongHangMucModel currentItem, bool status)
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

        private int CountTreeChildItems(NhDaHopDongHangMucModel currentItem)
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
                //if (item.IsSuggestion
                //    && (item?.FGiaTriUsd == null || item?.FGiaTriUsd == 0
                //    || item?.FGiaTriEur == null || item?.FGiaTriEur == 0
                //    || item?.FGiaTriVnd == null || item?.FGiaTriVnd == 0
                //    || item?.FGiaTriNgoaiTeKhac == null || item?.FGiaTriNgoaiTeKhac == 0
                //    ))
                //    Items.RemoveAt(i);
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

        public void CalculateHangMuc()
        {
            // || !Items.Any(y => y.Id == x.IIdParentId)
            var parents = Items.Where(x => x.IIdParentId == null);
            if (parents.Count() > 0)
            {
                foreach (var item in parents)
                {
                    var childs = Items.Where(x => x.IIdParentId == item.Id && !x.IsDeleted);
                    if (childs.Count() > 0)
                    {
                        CalculateHangMuc(item);
                    }
                    else 
                    {
                        item.IsChirenl = false;
                        if (item.FDonGia == null || item.ISoLuong == null)
                        {
                            item.FGiaTriUsd = 0;
                            item.FGiaTriVnd = 0;
                            item.FGiaTriNgoaiTeKhac = 0;
                        }
                        else
                        {
                            if (item.SelectThanhToanBang != null)
                            {
                                if (item.SelectThanhToanBang.ValueItem == "VND")
                                {
                                    item.FGiaTriVnd = item.ISoLuong * item.FDonGia;
                                    item.FGiaTriUsd = null;
                                    item.FGiaTriNgoaiTeKhac = null;
                                }
                                else
                                {
                                    var listTiGiaChiTiet = LstTiGiaChiTiet;
                                    string rootCurrency = SMaTienTeGoc;
                                    string sourceCurrency;
                                    string otherCurrency = SMaTienTeQuyDoi;
                                    double value = 0;
                                    switch (item.SelectThanhToanBang.ValueItem)
                                    {
                                        case "USD":
                                            sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                                            value = item.ISoLuong.Value * item.FDonGia.Value;
                                            item.FGiaTriUsd = value;
                                            if (FTiGiaNhap != null && FTiGiaNhap != 0)
                                            {
                                                item.FGiaTriNgoaiTeKhac = item.FGiaTriUsd / FTiGiaNhap;
                                            }
                                            break;
                                        default:
                                            sourceCurrency = otherCurrency;
                                            value = item.ISoLuong.Value * item.FDonGia.Value;
                                            item.FGiaTriNgoaiTeKhac = value;
                                            if (FTiGiaNhap != null)
                                            {
                                                item.FGiaTriUsd = item.FGiaTriNgoaiTeKhac * FTiGiaNhap;
                                            }
                                            break;
                                    }
                                    item.FGiaTriVnd = null;
                                }
                            }
                        }

                    }
                }
            }
        }

        private void CalculateHangMuc(NhDaHopDongHangMucModel parentItem)
        {
            var childs = Items.Where(x => x.IIdParentId == parentItem.Id && !x.IsDeleted);
            if (childs.Count() > 0)
            {
                foreach (var item in childs)
                {
                    CalculateHangMuc(item);
                    if (item.FDonGia == null || item.ISoLuong == null)
                    {
                        item.FGiaTriUsd = 0;
                        item.FGiaTriVnd = 0;
                        item.FGiaTriNgoaiTeKhac = 0;
                    }
                    else
                    {
                        if (item.SelectThanhToanBang != null)
                        {
                            if (item.SelectThanhToanBang.ValueItem == "VND")
                            {
                                item.FGiaTriVnd = item.ISoLuong * item.FDonGia;
                                item.FGiaTriUsd = null;
                                item.FGiaTriNgoaiTeKhac = null;
                            }
                            else if (LstTiGiaChiTiet != null)
                            {
                                var listTiGiaChiTiet = LstTiGiaChiTiet;
                                string rootCurrency = SMaTienTeGoc;
                                string sourceCurrency;
                                string otherCurrency = SMaTienTeQuyDoi;
                                double value = 0;
                                switch (item.SelectThanhToanBang.ValueItem)
                                {
                                    case "USD":
                                        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                                        value = item.ISoLuong.Value * item.FDonGia.Value;
                                        item.FGiaTriUsd = value;
                                        if (FTiGiaNhap != null && FTiGiaNhap != 0)
                                        {
                                            item.FGiaTriNgoaiTeKhac = item.FGiaTriUsd / FTiGiaNhap;
                                        }
                                        break;
                                    default:
                                        sourceCurrency = otherCurrency;
                                        value = item.ISoLuong.Value * item.FDonGia.Value;
                                        item.FGiaTriNgoaiTeKhac = value;
                                        if (FTiGiaNhap != null)
                                        {
                                            item.FGiaTriUsd = item.FGiaTriNgoaiTeKhac * FTiGiaNhap;
                                        }
                                        break;
                                }
                                item.FGiaTriVnd = null;
                            }
                        }
                    }
                }
                parentItem.IsChirenl = true;
                parentItem.FGiaTriUsd = childs.Sum(x => x.FGiaTriUsd);
                parentItem.FGiaTriEur = childs.Sum(x => x.FGiaTriEur);
                parentItem.FGiaTriVnd = childs.Sum(x => x.FGiaTriVnd);
                parentItem.FGiaTriNgoaiTeKhac = childs.Sum(x => x.FGiaTriNgoaiTeKhac);
            }
            else
            {
                parentItem.IsChirenl = false;
            }
        }

        public override void OnSave()
        {
            if (!ValidationData()) return;
            SaveItems();
            DialogHost.Close("ForexContractInfoItems");
        }

        public override void OnClose(object obj)
        {         
            if (HasChanged)
            {
                var result = MessageBoxHelper.Confirm("Đồng chí có muốn lưu thông tin danh mục đã được nhập không?");
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    if (!ValidationData()) return;
                    SaveItems();
                }
            }
            DialogHost.Close("ForexContractInfoItems");
        }

        private void SaveItems()
        {
            
            //RemoveSusgestionItem();
            var data = _mapper.Map<IEnumerable<NhDaHopDongHangMucModel>>(Items).ToList();
            data.ForEach(x =>
            {
                x.SThanhToanBang = x.SelectThanhToanBang != null ? x.SelectThanhToanBang.ValueItem : string.Empty;
                x.FDonGia = x.IsChirenl ? null : x.FDonGia;
                x.ISoLuong = x.IsChirenl ? null : x.ISoLuong;
                x.SDonViTinh = x.IsChirenl ? null : x.SDonViTinh;
                x.PropertyChanged -= HangMuc_PropertyChanged;
            });
            SavedAction?.Invoke(data);
        }

        public override void OnCellEditEnding(object obj)
        {
            CalculateHangMuc();
            base.OnCellEditEnding(obj);
            CurrencyExchangeAction?.Invoke(obj);
        }

        private bool ValidationData()
        {
            List<string> lstError = new List<string>();
            int Count = 0;
            foreach (var item in Items.Where(x => x.IsDeleted != true))
            {
                Count++;
                if (item.STenHangMuc == null)
                {
                    lstError.Add(string.Format("Dòng " + Count + ": Chưa nhập tên chi phí !"));
                }
                if (item.ISoLuong == null && !item.IsChirenl)
                {
                    lstError.Add(string.Format("Dòng " + Count + ": Chưa nhập số lượng !"));
                }
                if (item.FDonGia == null && !item.IsChirenl)
                {
                    lstError.Add(string.Format("Dòng " + Count + ": Chưa nhập đơn giá !"));
                }
                if (item.SDonViTinh == null && !item.IsChirenl)
                {
                    lstError.Add(string.Format("Dòng " + Count + ": Chưa nhập đơn vị tính !"));
                }
            }
            if (lstError.Count() > 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }
            return true;
        }
    }
}
