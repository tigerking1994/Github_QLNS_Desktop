using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSCNTForexContractInfo;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSCNTForexContractInfo
{
    public class MSCNTForexContractHangMucDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhHdnkCacQuyetDinhChiPhiModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhHdnkCacQuyetDinhChiPhiHangMucService _nhHdnkCacQuyetDinhChiPhiHangMucService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;

        public override string Title => "HỢP ĐỒNG";
        public override string Description => "Thông tin Hợp đồng phụ lục - hạng mục";
        public override Type ContentType => typeof(ForexContractHangMucDialog);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public Action<object> CurrencyExchangeAction { get; set; }

        private double? _FTienHopDongNgoaiTeKhac;
        public double? FTienHopDongNgoaiTeKhac
        {
            get => _FTienHopDongNgoaiTeKhac;
            set => SetProperty(ref _FTienHopDongNgoaiTeKhac, value);
        }

        private double? _FTienHopDongUSD;
        public double? FTienHopDongUSD
        {
            get => _FTienHopDongUSD;
            set => SetProperty(ref _FTienHopDongUSD, value);
        }

        private double? _FTienHopDongVND;
        public double? FTienHopDongVND
        {
            get => _FTienHopDongVND;
            set => SetProperty(ref _FTienHopDongVND, value);
        }

        private double? _FTienHopDongEUR;
        public double? FTienHopDongEUR
        {
            get => _FTienHopDongEUR;
            set => SetProperty(ref _FTienHopDongEUR, value);
        }

        public bool IsNotQuyetDinhChiTrongNuoc { get; set; }
        public bool IsReadOnly { get; set; }

        private ObservableCollection<NhDaHopDongHangMucModel> _items;
        public ObservableCollection<NhDaHopDongHangMucModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private NhDaHopDongHangMucModel _selectedItems;
        public NhDaHopDongHangMucModel SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTiet { get; set; }

        public MSCNTForexContractHangMucDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhHdnkCacQuyetDinhChiPhiHangMucService nhHdnkCacQuyetDinhChiPhiHangMucService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService)
        : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nhHdnkCacQuyetDinhChiPhiHangMucService = nhHdnkCacQuyetDinhChiPhiHangMucService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
        }

        public override void Init()
        {
            InitData();
            LoadData();
        }

        private void InitData()
        {
            FTienHopDongNgoaiTeKhac = 0;
            FTienHopDongUSD = 0;
            FTienHopDongVND = 0;
            FTienHopDongEUR = 0;
            SelectedItems = null;
            Items = new ObservableCollection<NhDaHopDongHangMucModel>();
        }

        public override void LoadData(params object[] args)
        {
            if (Model.ListHopDongHangMuc != null && Model.ListHopDongHangMuc.Count > 0)
            {
                var lstHangMuc = Model.ListHopDongHangMuc;
                Items = _mapper.Map<ObservableCollection<NhDaHopDongHangMucModel>>(lstHangMuc.OrderBy(n => n.SMaOrder));
                //SetIdAndParentId();
                SetStatusHangMuc();
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
            SavedAction?.Invoke(_mapper.Map<List<NhDaHopDongHangMucModel>>(Items));
            ((Window)obj).Close();
        }

        private void SetIdAndParentId()
        {
            foreach (var item in Items)
            {
                item.Id = Guid.NewGuid();
                var lstChildHangMuc = FindChildByIdQuyetDinhChiPhiHangMuc(item.IIdHangMucId);
                if (lstChildHangMuc != null && lstChildHangMuc.Count > 0)
                {
                    lstChildHangMuc.ForEach(hangMuc => hangMuc.IIdParentId = item.Id);
                }
            }
        }

        private void SetStatusHangMuc()
        {
            foreach (var item in Items)
            {
                var lstChildHangMuc = FindListChildHangMuc(item.IIdHangMucId);
                if (lstChildHangMuc != null && lstChildHangMuc.Count > 0) item.IsHangCha = true;
                else
                {
                    if (item.IIdHangMucParentId == null)
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

        public List<NhDaHopDongHangMucModel> FindListChildHangMuc(Guid parentId)
        {
            List<NhDaHopDongHangMucModel> inner = new List<NhDaHopDongHangMucModel>();
            foreach (var t in Items.Where(item => item.IIdHangMucParentId == parentId))
            {
                inner.Add(t);
                inner = inner.Union(FindListChildHangMuc(t.IIdHangMucId)).ToList();
            }
            return inner;
        }

        public List<NhDaHopDongHangMucModel> FindChildByIdQuyetDinhChiPhiHangMuc(Guid parentId)
        {
            List<NhDaHopDongHangMucModel> inner = new List<NhDaHopDongHangMucModel>();
            foreach (var t in Items.Where(item => item.IIdHangMucParentId == parentId))
            {
                inner.Add(t);
                inner = inner.Union(FindChildByIdQuyetDinhChiPhiHangMuc(t.IIdHangMucId)).ToList();
            }
            return inner;
        }

        private void HangMuc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var objectSender = (NhDaHopDongHangMucModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaHopDongHangMucModel.IsSelected)) && objectSender.IIdHangMucParentId != null)
            {
                var listChild = FindListChildHangMuc(objectSender.IIdHangMucId);
                if (listChild != null && listChild.Count > 0)
                {
                    listChild.ForEach(hangMuc => hangMuc.IsSelected = objectSender.IsSelected);
                }
                if (objectSender.IIdHangMucParentId != null)
                {
                    var parentItem = Items.FirstOrDefault(n => n.IIdHangMucId == objectSender.IIdHangMucParentId);
                    if (parentItem != null)
                    {
                        var lstParentChild = Items.Where(x => x.IIdHangMucParentId == parentItem.IIdHangMucId).ToList();
                        var lstParentChildChecked = Items.Where(x => x.IIdHangMucParentId == parentItem.IIdHangMucId && x.IsSelected).ToList();
                        if (lstParentChild != null && lstParentChildChecked != null && lstParentChild.Count() == lstParentChildChecked.Count())
                        {
                            parentItem.IsSelected = true;
                        }
                    }
                }
            }
            CalculateDataHangMuc(objectSender);
        }

        private void CalculateDataHangMuc(NhDaHopDongHangMucModel selectedHangMuc)
        {
            var parentItem = Items.FirstOrDefault(n => n.IIdHangMucId == selectedHangMuc.IIdHangMucParentId);
            if (parentItem != null)
            {
                var listChild = Items.Where(x => x.IIdHangMucParentId == parentItem.IIdHangMucId && x.IsSelected).ToList();
                if (listChild != null && listChild.Count > 0)
                {
                    parentItem.FGiaTriNgoaiTeKhac = listChild.Sum(x => x.FGiaTriNgoaiTeKhac);
                    parentItem.FGiaTriEur = listChild.Sum(x => x.FGiaTriEur);
                    parentItem.FGiaTriUsd = listChild.Sum(x => x.FGiaTriUsd);
                    parentItem.FGiaTriVnd = listChild.Sum(x => x.FGiaTriVnd);
                } else
                {
                    parentItem.FGiaTriNgoaiTeKhac = 0;
                    parentItem.FGiaTriEur = 0;
                    parentItem.FGiaTriUsd = 0;
                    parentItem.FGiaTriVnd = 0;
                } 
                if (!parentItem.IIdHangMucParentId.IsNullOrEmpty())
                {
                    CalculateDataHangMuc(parentItem);
                }
            }
            CalculateTongHangMuc();
        }

        private void CalculateTongHangMuc()
        {
            var listHangMuc = Items.Where(x => (x.IIdHangMucParentId == null && (x.IsSelected || HasChildSelected(x)))).ToList();
            if (listHangMuc != null && listHangMuc.Count > 0)
            {
                FTienHopDongNgoaiTeKhac = listHangMuc.Sum(n => n.FGiaTriNgoaiTeKhac);
                FTienHopDongEUR = listHangMuc.Sum(n => n.FGiaTriEur);
                FTienHopDongUSD = listHangMuc.Sum(n => n.FGiaTriUsd);
                FTienHopDongVND = listHangMuc.Sum(n => n.FGiaTriVnd);
            } else
            {
                FTienHopDongNgoaiTeKhac = 0;
                FTienHopDongEUR = 0;
                FTienHopDongUSD = 0;
                FTienHopDongVND = 0;
            }
        }

        public bool HasChildSelected(NhDaHopDongHangMucModel hangmuc)
        {
            var lstChild = FindListChildHangMuc(hangmuc.IIdHangMucId);
            foreach(var item in lstChild)
            {
                if (item.IsSelected == true)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckHangMucCanEditGiatri()
        {
            var item = SelectedItems;
            if (item == null) return false;
            var lstChild = FindListChildHangMuc(item.IIdHangMucId);
            if (lstChild == null || lstChild.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
