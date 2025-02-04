using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNThietKeKyThuatTongDuToan;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNThietKeKyThuatTongDuToan
{
    public class MSTNThietKeKyThuatTongDuToanItemDialogViewModel : DetailViewModelBase<NhDaDuToanChiPhiModel, NhDaDuToanHangMucModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhDmLoaiCongTrinhService _nhDmLoaiCongTrinhService;
        private ObservableCollection<NhDaDuToanHangMucModel> _originItems;

        public override string Name => "THIẾT KẾ KỸ THUẬT VÀ TỔNG DỰ TOÁN CHI TIẾT";
        public override string Title => (idDuToan == 1) ? "Quản lý dự toán mua sắm được duyệt" : "Quản lý dự toán đặt hàng được duyệt ";
        public string Description { get; set; }

        public int idDuToan { get; set; }

        public override Type ContentType => typeof(ThietKeKyThuatTongDuToanItemDialog);
        public bool IsEnableQuyetDinhDauTuPheDuyet { get; set; }
        public bool IsDetail { get; set; }
        public bool IsAddHangMucRowChild { get; set; }
        public bool HasChanged => !ObjectCopier.ToJsonString(Items).Equals(ObjectCopier.ToJsonString(_originItems));
        public Action<object, string> CurrencyExchangeAction { get; set; }

        private ObservableCollection<ComboboxItem> _itemsLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> ItemsLoaiCongTrinh
        {
            get => _itemsLoaiCongTrinh;
            set => SetProperty(ref _itemsLoaiCongTrinh, value);
        }
        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set
            {
                if (SetProperty(ref _fTiGiaNhap, value))
                {
                }
            }
        }
        public RelayCommand AddHangMucCommand { get; }
        public RelayCommand DeleteHangMucCommand { get; }
        public RelayCommand DevideHangMucCommand { get; }
        public RelayCommand ReOrderDuToanHangMucCommand { get; }

        public MSTNThietKeKyThuatTongDuToanHangMucDivideViewModel DivideHangMucViewModel { get; }

        public MSTNThietKeKyThuatTongDuToanItemDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INhDmLoaiCongTrinhService nhDmLoaiCongTrinhService,
            MSTNThietKeKyThuatTongDuToanHangMucDivideViewModel divideHangMucViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nhDmLoaiCongTrinhService = nhDmLoaiCongTrinhService;

            AddHangMucCommand = new RelayCommand(obj => OnAddHangMuc(obj));
            DeleteHangMucCommand = new RelayCommand(obj => OnDeleteHangMuc());
            DevideHangMucCommand = new RelayCommand(obj => OnDevideHangMuc());
            ReOrderDuToanHangMucCommand = new RelayCommand(obj => OnReOrderDuToanHangMuc());

            DivideHangMucViewModel = divideHangMucViewModel;
        }

        public override void Init()
        {
            Description = "Nguồn vốn: " + Model.STenNguonVon + "  -  Chi phí: " + Model.STenChiPhi;
            IsAddHangMucRowChild = false;
            OnPropertyChanged(nameof(IsAddHangMucRowChild));
            LoadLoaiCongTrinh();
            LoadData();
            CalculatorHangMuc();
        }

        public override void LoadData(params object[] args)
        {
            Items = new ObservableCollection<NhDaDuToanHangMucModel>();
            var data = _mapper.Map<IEnumerable<NhDaDuToanHangMucModel>>(Model.DuToanHangMucs).ToList();         
            data.ForEach(x =>
            {
                x.IIdDuToanChiPhiId = Model.Id;
                x.IsModified = false;
                x.FGiaTriQdDauTuVnd = Model.FGiaTriQdDauTuVnd;
                x.FGiaTriQdDauTuUsd = Model.FGiaTriQdDauTuUsd;
                //x.FGiaTriQdDauTuEur = Model.FGiaTriQdDauTuEur;
                //x.FGiaTriQdDauTuNgoaiTeKhac = Model.FGiaTriQdDauTuNgoaiTeKhac;
                x.PropertyChanged += DuToanHangMuc_PropertyChanged;
            });
            Items = _mapper.Map<ObservableCollection<NhDaDuToanHangMucModel>>(data);
            _originItems = ObjectCopier.Clone(Items);
            if (!Items.IsEmpty())
            {
                if (!IsAddHangMucRowChild)
                {
                    IsAddHangMucRowChild = true;
                    OnPropertyChanged(nameof(IsAddHangMucRowChild));
                }
            }
        }

        private void LoadLoaiCongTrinh()
        {
            var data = _nhDmLoaiCongTrinhService.FindAll();
            _itemsLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            OnPropertyChanged(nameof(ItemsLoaiCongTrinh));
        }

        private void OnAddHangMuc(object obj)
        {
            if (!IsAddHangMucRowChild)
            {
                IsAddHangMucRowChild = true;
                OnPropertyChanged(nameof(IsAddHangMucRowChild));
            }
            if (Items == null) Items = new ObservableCollection<NhDaDuToanHangMucModel>();

            NhDaDuToanHangMucModel sourceItem = SelectedItem;
            NhDaDuToanHangMucModel targetItem = new NhDaDuToanHangMucModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!Items.IsEmpty())
            {
                if (sourceItem != null)
                {
                    currentRow = Items.IndexOf(sourceItem);
                    if (isParent)
                    {
                        currentRow += CountTreeChildItems(sourceItem);
                    }
                }
            }

            if (sourceItem != null)
            {
                targetItem.IIdParentId = isParent ? sourceItem.IIdParentId : sourceItem.Id;
            }
            targetItem.Id = Guid.NewGuid();
            targetItem.IIdDuToanChiPhiId = Model.Id;
            targetItem.IsAdded = true;
            targetItem.PropertyChanged += DuToanHangMuc_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);

            OrderItems(targetItem.IIdParentId);
            OnPropertyChanged(nameof(Items));
        }

        private void OnDeleteHangMuc()
        {
            if (SelectedItem != null)
            {
                DeleteTreeItems(SelectedItem, !SelectedItem.IsDeleted);
            }
        }

        private void OnDevideHangMuc()
        {
            // Method intentionally left empty.
        }

        private void OnReOrderDuToanHangMuc()
        {
            foreach (var item in Items)
            {
                var parent = Items.FirstOrDefault(x => x.Id == item.IIdParentId);
                if (parent == null) item.IIdParentId = null;
            }
            OrderItems();
        }

        public override void OnSave(object obj)
        {
            if (obj is Window window)
            {
                SaveItems();
                window.Close();
            }
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                if (HasChanged)
                {
                    var result = MessageBoxHelper.Confirm("Đồng chí có muốn lưu thông tin hạng mục đã được nhập không?");
                    if (result == System.Windows.MessageBoxResult.Yes)
                    {
                        SaveItems();
                    }
                }
                window.Close();
            }
        }

        private void DuToanHangMuc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NhDaDuToanHangMucModel objectSender = (NhDaDuToanHangMucModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaDuToanHangMucModel.FGiaTriVnd))
            //if (e.PropertyName.Equals(nameof(NhDaDuToanHangMucModel.FGiaTriNgoaiTeKhac))
                || e.PropertyName.Equals(nameof(NhDaDuToanHangMucModel.FGiaTriUsd)))
            //|| e.PropertyName.Equals(nameof(NhDaDuToanHangMucModel.FGiaTriEur))
            //|| e.PropertyName.Equals(nameof(NhDaDuToanHangMucModel.FGiaTriNgoaiTeKhac)))
            {
                //    CurrencyExchangeAction?.Invoke(sender, e.PropertyName);
            }
            //Tính hang muc cha
            var isUsd = false;
            var hangMuc = Items.Where(n => n.Id == objectSender.IIdParentId).FirstOrDefault();
            var parents = Items.Where(n => n.Id == objectSender.Id);
            switch (e.PropertyName)
            {
                case nameof(NhDaDuToanHangMucModel.FGiaTriVnd):
                    ChangeValueByRate(parents, false);
                    break;
                case nameof(NhDaDuToanHangMucModel.FGiaTriUsd):
                    isUsd = true;
                    ChangeValueByRate(parents, true);
                    break;
                default:
                    isUsd = true;
                    ChangeValueByRate(parents, true);
                    break;
            }
            if (hangMuc != null)
            {
                if (isUsd)
                    hangMuc.FGiaTriUsd = Items.Where(n => n.IIdParentId == hangMuc.Id).Sum(n => n.FGiaTriUsd);
                else
                    hangMuc.FGiaTriVnd = Items.Where(n => n.IIdParentId == hangMuc.Id).Sum(n => n.FGiaTriVnd);

            }
            objectSender.IsModified = true;
            OnPropertyChanged(nameof(HasChanged));
        }

        private void ChangeValueByRate(IEnumerable<NhDaDuToanHangMucModel> ItemsHangMuc = null, bool isUsD = true)
        {
            if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
            {
                if (isUsD)
                {
                    if (!ItemsHangMuc.IsEmpty())
                        ItemsHangMuc.ForAll(item =>
                        {
                            item.FGiaTriVnd = (item.FGiaTriUsd != null && item.FGiaTriUsd.HasValue) ? (item.FGiaTriUsd.Value * FTiGiaNhap.Value) : 0;
                        });
                }
                else
                {
                    if (!ItemsHangMuc.IsEmpty())
                        ItemsHangMuc.ForAll(item =>
                        {
                            item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                        });
                }
            }
        }
        public void CalculatorHangMuc()
        {
            var parents = Items;
            foreach (var item in parents)
            {
                if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                {
                    item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                }
                else
                {
                    item.FGiaTriUsd = 0;
                }
            }
        }
        private void SaveItems()
        {
            var data = _mapper.Map<IEnumerable<NhDaDuToanHangMucModel>>(Items).ToList();
            //data.ForEach(x =>
            //{
            //    x.PropertyChanged -= DuToanHangMuc_PropertyChanged;
            //});
            SavedAction?.Invoke(data);
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

        private int CountTreeChildItems(NhDaDuToanHangMucModel currentItem)
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

        private void DeleteTreeItems(NhDaDuToanHangMucModel currentItem, bool status)
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
    }
}
