using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKDecision;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKDecision
{
    public class DAHDNKDecisionHangMucDetailDialogViewModel : DialogViewModelBase<NhHdnkCacQuyetDinhChiPhiModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhHdnkCacQuyetDinhChiPhiHangMucService _nhHdnkCacQuyetDinhChiPhiHangMucService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;

        public override string Title => "QUYẾT ĐỊNH PHÊ DUYỆT CHI TIẾT";
        public override string Description => "Thông tin phụ lục - hạng mục";
        public override Type ContentType => typeof(DAHDNKDecisionHangMucDetailDialog);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private int currentRow = -1;

        public NhDmTiGiaModel SelectedTiGia { get; set; }
        public NhDmTiGiaChiTietModel SelectedTiGiaChiTiet { get; set; }

        public bool IsLoaiNhiemVuChiDuAn { get; set; }

        private double? _fGiaTriNgoaiTeKhac;
        public double? FGiaTriNgoaiTeKhac
        {
            get => _fGiaTriNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriNgoaiTeKhac, value);
        }

        private double? _fGiaTriUSD;
        public double? FGiaTriUSD
        {
            get => _fGiaTriUSD;
            set => SetProperty(ref _fGiaTriUSD, value);
        }

        private double? _fGiaTriVND;
        public double? FGiaTriVND
        {
            get => _fGiaTriVND;
            set => SetProperty(ref _fGiaTriVND, value);
        }

        private double? _fGiaTriEUR;
        public double? FGiaTriEUR
        {
            get => _fGiaTriEUR;
            set => SetProperty(ref _fGiaTriEUR, value);
        }

        public bool IsReadOnly { get; set; }

        private ObservableCollection<NhHdnkCacQuyetDinhChiPhiHangMucModel> _items;
        public ObservableCollection<NhHdnkCacQuyetDinhChiPhiHangMucModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private NhHdnkCacQuyetDinhChiPhiHangMucModel _selectedItems;
        public NhHdnkCacQuyetDinhChiPhiHangMucModel SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        public RelayCommand AddDetailCommand { get; }
        public RelayCommand AddChildCommand { get; }
        public RelayCommand DeleteDetailCommand { get; }

        public DAHDNKDecisionHangMucDetailDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhHdnkCacQuyetDinhChiPhiHangMucService nhHdnkCacQuyetDinhChiPhiHangMucService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nhHdnkCacQuyetDinhChiPhiHangMucService = nhHdnkCacQuyetDinhChiPhiHangMucService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;

            AddDetailCommand = new RelayCommand(obj => OnAddHangMuc());
            AddChildCommand = new RelayCommand(obj => OnAddHangMucChild());
            DeleteDetailCommand = new RelayCommand(obj => OnDeleteHangMuc());
        }

        public override void Init()
        {
            InitData();
            LoadData();
        }

        private void InitData()
        {
            FGiaTriNgoaiTeKhac = 0;
            FGiaTriUSD = 0;
            FGiaTriVND = 0;
            FGiaTriEUR = 0;
            SelectedItems = null;
            Items = new ObservableCollection<NhHdnkCacQuyetDinhChiPhiHangMucModel>();
        }

        public override void LoadData(params object[] args)
        {
            if (Model.ListHangMuc != null && Model.ListHangMuc.Count > 0)
            {
                var lstHangMuc = Model.ListHangMuc.Select(item => item.Clone()).ToList();
                Items = _mapper.Map<ObservableCollection<NhHdnkCacQuyetDinhChiPhiHangMucModel>>(lstHangMuc.OrderBy(n => n.SMaOrder));
                if (IsLoaiNhiemVuChiDuAn)
                {
                    SetIdAndParentId();
                }
                UpdateStatusHangMuc();
                CalculateTongHangMuc();
            }
            foreach (var model in Items)
            {
                model.PropertyChanged += HangMuc_PropertyChanged;
            }
        }

        public override void OnClose(object obj)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public override void OnSave(object obj)
        {
            if (!ValidateData()) return;
            SavedAction?.Invoke(_mapper.Map<List<NhHdnkCacQuyetDinhChiPhiHangMucModel>>(Items));
            ((Window)obj).Close();
        }

        protected void OnAddHangMuc()
        {
            NhHdnkCacQuyetDinhChiPhiHangMucModel targetItem = new NhHdnkCacQuyetDinhChiPhiHangMucModel()
            {
                Id = Guid.NewGuid(),
                SMaOrder = GetSTTHangMuc(false),
                IsHangCha = true,
                IsNewRecord = true,
                IIdCacQuyetDinhChiPhiId = Model.Id 
            };
            if (Items != null && Items.Count > 0 && SelectedItems != null)
            {
                NhHdnkCacQuyetDinhChiPhiHangMucModel sourceItem = SelectedItems;
                targetItem = ObjectCopier.Clone(sourceItem);
                if (!sourceItem.IsHangCha)
                {
                    targetItem.IsHangCha = false;
                    targetItem.IIdParentId = sourceItem.IIdParentId;
                }
                else
                {
                    targetItem.IIdParentId = null;
                }
                targetItem.STenHangMuc = null;
                targetItem.FGiaTriNgoaiTeKhac = 0;
                targetItem.FGiaTriUsd = 0;
                targetItem.FGiaTriVnd = 0;
                targetItem.FGiaTriEur = 0;
                targetItem.Id = Guid.NewGuid();
                targetItem.SMaOrder = GetSTTHangMuc(false);
                targetItem.IsNewRecord = true;
            }
            targetItem.PropertyChanged += HangMuc_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(Items));
        }

        protected void OnAddHangMucChild()
        {
            if (Items == null || Items.Count == 0 || SelectedItems == null)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgNotSelectHangMucParent, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NhHdnkCacQuyetDinhChiPhiHangMucModel sourceItem = SelectedItems;
            NhHdnkCacQuyetDinhChiPhiHangMucModel targetItem = ObjectCopier.Clone(sourceItem);
            sourceItem.IsHangCha = true;
            sourceItem.FGiaTriNgoaiTeKhac = 0;
            sourceItem.FGiaTriUsd = 0;
            sourceItem.FGiaTriVnd = 0;
            sourceItem.FGiaTriEur = 0;

            targetItem.Id = Guid.NewGuid();
            targetItem.IIdParentId = sourceItem.Id;
            targetItem.IsHangCha = false;
            targetItem.SMaOrder = GetSTTHangMuc(true);
            targetItem.STenHangMuc = null;
            targetItem.IsNewRecord = true;
            targetItem.FGiaTriNgoaiTeKhac = 0;
            targetItem.FGiaTriUsd = 0;
            targetItem.FGiaTriVnd = 0;
            targetItem.FGiaTriEur = 0;
            targetItem.PropertyChanged += HangMuc_PropertyChanged;

            Items.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(Items));
        }

        private string GetSTTHangMuc(bool isAddChild = false)
        {
            string sttHangMuc = string.Empty;
            int inDexSTTHangMucLast = 1;
            if (SelectedItems == null && !isAddChild)
            {
                if (Items.Count < 1)
                {
                    sttHangMuc = "1";
                    currentRow = -1;
                }
                else
                {
                    var hangMucItemLast = Items.Last(x => x.IIdParentId == null);
                    inDexSTTHangMucLast = Int32.Parse(hangMucItemLast.SMaOrder);
                    sttHangMuc = (inDexSTTHangMucLast + 1).ToString();
                    currentRow = Items.IndexOf(Items.Last());
                }
            }
            if (SelectedItems != null && !isAddChild)
            {
                // tìm giá trị ngang hàng cuối cùng trong list => giá trị thêm mới được copy từ giá trị ngang hàng cuối cùng
                if (SelectedItems.IIdParentId.IsNullOrEmpty())
                {
                    var hangMucLast = Items.Last(x => x.IIdParentId.IsNullOrEmpty() && x.SMaOrder.Length == 1);
                    inDexSTTHangMucLast = Int32.Parse(hangMucLast.SMaOrder);
                    sttHangMuc = (inDexSTTHangMucLast + 1).ToString();
                    currentRow = Items.Count - 1;
                }
                else
                {
                    var hangMucLast = Items.Last(x => x.IIdParentId == SelectedItems.IIdParentId);
                    string sTTHangMucLast = hangMucLast.SMaOrder;
                    inDexSTTHangMucLast = Int32.Parse(sTTHangMucLast.Substring(sTTHangMucLast.Length - 1));
                    sttHangMuc = sTTHangMucLast.Substring(0, (sTTHangMucLast.Length - 1)) + (inDexSTTHangMucLast + 1).ToString();
                    currentRow = Items.IndexOf(hangMucLast);
                }
            }
            if (SelectedItems != null && isAddChild)
            {
                var listChild = Items.Where(x => x.IIdParentId == SelectedItems.Id).ToList();
                if (listChild == null || listChild.Count == 0)
                {
                    sttHangMuc = SelectedItems.SMaOrder + "_1";
                    currentRow = Items.IndexOf(SelectedItems);
                }
                else
                {
                    var hangMucChildLast = Items.Last(x => x.IIdParentId == SelectedItems.Id);
                    string sTTHangMucLast = hangMucChildLast.SMaOrder;
                    if (string.IsNullOrEmpty(sTTHangMucLast))
                    {
                        sttHangMuc = SelectedItems.SMaOrder + "_1";
                    }
                    List<string> arrayMaOrDer = sTTHangMucLast.Split("_").ToList();
                    if (arrayMaOrDer.Count > 0)
                    {
                        string maOrderOld = arrayMaOrDer.Last();
                        inDexSTTHangMucLast = Int32.Parse(maOrderOld) + 1;
                        arrayMaOrDer.RemoveAt(arrayMaOrDer.Count - 1);
                        arrayMaOrDer.Add(inDexSTTHangMucLast.ToString());
                        sttHangMuc = string.Join("_", arrayMaOrDer);
                    }

                    //tìm vị trí của dòng con cuối cùng của hạng mục ngang hàng cuối cùng
                    var listChildOfHangMucLast = FindListChildHangMuc(hangMucChildLast.Id);
                    if (listChildOfHangMucLast == null || listChildOfHangMucLast.Count == 0)
                    {
                        currentRow = Items.IndexOf(hangMucChildLast);
                    }
                    else
                    {
                        currentRow = Items.IndexOf(listChildOfHangMucLast.Last());
                    }
                }
            }
            return sttHangMuc;
        }

        protected void OnDeleteHangMuc()
        {
            if (Items != null && Items.Count > 0 && SelectedItems != null)
            {
                var listDelete = new List<NhHdnkCacQuyetDinhChiPhiHangMucModel>();
                if (!SelectedItems.Id.IsNullOrEmpty()) listDelete = FindListChildHangMuc(SelectedItems.Id);
                listDelete.Add(SelectedItems);
                foreach (var item in listDelete)
                {
                    item.IsDeleted = !SelectedItems.IsDeleted;
                }
                UpdateStatusHangMuc();
            }
        }

        private void SetIdAndParentId()
        {
            foreach (var item in Items.Where(n => n.IIdQdDauTuHangMucId.HasValue))
            {
                var lstChildHangMuc = FindChildByIdQdDauTuHangMuc(item.IIdQdDauTuHangMucId.Value);
                if (lstChildHangMuc != null && lstChildHangMuc.Count > 0)
                {
                    lstChildHangMuc.ForEach(hangMuc => hangMuc.IIdParentId = item.Id);
                }
            }
        }

        private void UpdateStatusHangMuc()
        {
            foreach (var item in Items)
            {
                var lstChildHangMuc = FindListChildHangMuc(item.Id);
                if (lstChildHangMuc != null && lstChildHangMuc.Count > 0) item.IsHangCha = true;
                else 
                {
                    if (item.IIdParentId == null)
                    {
                        item.IsHangCha = true;
                    }
                    else
                    {
                        item.IsHangCha = false;
                    }
                } 
            }
            OnPropertyChanged(nameof(Items));
        }

        public List<NhHdnkCacQuyetDinhChiPhiHangMucModel> FindListChildHangMuc(Guid parentId)
        {
            List<NhHdnkCacQuyetDinhChiPhiHangMucModel> inner = new List<NhHdnkCacQuyetDinhChiPhiHangMucModel>();
            foreach (var t in Items.Where(item => item.IIdParentId == parentId && !item.IsDeleted))
            {
                inner.Add(t);
                inner = inner.Union(FindListChildHangMuc(t.Id)).ToList();
            }
            return inner;
        }
        public List<NhHdnkCacQuyetDinhChiPhiHangMucModel> FindChildByIdQdDauTuHangMuc(Guid parentId)
        {
            var inner = new List<NhHdnkCacQuyetDinhChiPhiHangMucModel>();
            foreach (var t in Items.Where(item => item.IIdQdDauTuHangMucParentId == parentId))
            {
                inner.Add(t);
                inner = inner.Union(FindChildByIdQdDauTuHangMuc(t.IIdQdDauTuHangMucId.Value)).ToList();
            }
            return inner;
        }
        private void HangMuc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhHdnkCacQuyetDinhChiPhiHangMucModel objectSender = (NhHdnkCacQuyetDinhChiPhiHangMucModel)sender;
            if (e.PropertyName.Equals(nameof(NhHdnkCacQuyetDinhChiPhiHangMucModel.IsDeleted))
                || e.PropertyName.Equals(nameof(NhHdnkCacQuyetDinhChiPhiHangMucModel.FGiaTriNgoaiTeKhac))
                || e.PropertyName.Equals(nameof(NhHdnkCacQuyetDinhChiPhiHangMucModel.FGiaTriUsd))
                || e.PropertyName.Equals(nameof(NhHdnkCacQuyetDinhChiPhiHangMucModel.FGiaTriEur))
                || e.PropertyName.Equals(nameof(NhHdnkCacQuyetDinhChiPhiHangMucModel.FGiaTriVnd)))
            {
                if (SelectedTiGia != null)
                {
                    var listTiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                    string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                    string sourceCurrency;
                    string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                    double value;
                    switch (e.PropertyName)
                    {
                        case nameof(NhHdnkCacQuyetDinhChiPhiModel.FGiaTriVnd):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                            value = objectSender.FGiaTriVnd.Value;
                            break;
                        case nameof(NhHdnkCacQuyetDinhChiPhiModel.FGiaTriEur):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                            value = objectSender.FGiaTriEur.Value;
                            break;
                        case nameof(NhHdnkCacQuyetDinhChiPhiModel.FGiaTriNgoaiTeKhac):
                            sourceCurrency = otherCurrency;
                            value = objectSender.FGiaTriNgoaiTeKhac;
                            break;
                        default:
                            sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                            value = objectSender.FGiaTriUsd.Value;
                            break;
                    }
                    objectSender.FGiaTriVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    objectSender.FGiaTriEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    objectSender.FGiaTriUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    objectSender.FGiaTriNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                }
                CalculateDataHangMuc(objectSender);
            }
        }

        private void CalculateDataHangMuc(NhHdnkCacQuyetDinhChiPhiHangMucModel selectedHangMuc)
        {
            var parentItem = Items.FirstOrDefault(n => n.Id == selectedHangMuc.IIdParentId);
            if (parentItem != null)
            {
                var listChild = Items.Where(x => x.IIdParentId == parentItem.Id && !x.IsDeleted).ToList();
                if (listChild != null && listChild.Count > 0)
                {
                    parentItem.FGiaTriNgoaiTeKhac = listChild.Sum(x => x.FGiaTriNgoaiTeKhac);
                    parentItem.FGiaTriEur = listChild.Sum(x => x.FGiaTriEur);
                    parentItem.FGiaTriUsd = listChild.Sum(x => x.FGiaTriUsd);
                    parentItem.FGiaTriVnd = listChild.Sum(x => x.FGiaTriVnd);
                }
                else
                {
                    parentItem.FGiaTriNgoaiTeKhac = 0;
                    parentItem.FGiaTriEur = 0;
                    parentItem.FGiaTriUsd = 0;
                    parentItem.FGiaTriVnd = 0;
                }
                if (!parentItem.IIdParentId.IsNullOrEmpty())
                {
                    CalculateDataHangMuc(parentItem);
                }
            }
            CalculateTongHangMuc();
        }

        private void CalculateTongHangMuc()
        {
            var listHangMuc = Items.Where(x => x.IIdParentId == null && !x.IsDeleted).ToList();
            if (listHangMuc != null && listHangMuc.Count > 0)
            {
                FGiaTriNgoaiTeKhac = listHangMuc.Sum(n => (n.FGiaTriNgoaiTeKhac));
                FGiaTriUSD = listHangMuc.Sum(n => n.FGiaTriUsd);
                FGiaTriVND = listHangMuc.Sum(n => n.FGiaTriVnd);
                FGiaTriEUR = listHangMuc.Sum(n => n.FGiaTriEur);
            }
        }
        public bool CheckHangMucCanEditGiatri()
        {
            var item = SelectedItems;
            if (item == null) return false;

            var lstChild = FindListChildHangMuc(item.Id);
            if (lstChild == null || lstChild.Count == 0)
            {
                return true;
            }
            return false;
        }
        private bool ValidateData()
        {
            List<string> lstError = new List<string>();
            if (Items.Where(n => !n.IsDeleted).Any(n => n.STenHangMuc.IsEmpty()))
            {
                lstError.Add(string.Format(Resources.MsgCheckHangMucDauTu));
            }
            if (Items.Where(n => !n.IsDeleted).Any(n => n.FGiaTriVnd == 0 || n.FGiaTriVnd == null))
            {
                lstError.Add(string.Format(Resources.MsgCheckGiaTriDuocDuyet));
            }
            if (lstError.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstError),
                    Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
