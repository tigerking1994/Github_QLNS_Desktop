using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.DeNghiQTDAHT;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.DeNghiQTDAHT
{
    public class DeNghiQTDAHTDialogDetailViewModel : DialogAttachmentViewModelBase<NhQtQuyetToanDahtModel>
    {
        private readonly INhDaQdDauTuService _nhDaQdDauTuService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly ISessionService _sessionService;
        private readonly INhQtQuyetToanDahtService _nhQtQuyetToanDahtService;

        public override string Title => "Quản lý đề nghị quyết toán dự án hoàn thành";
        public override string Name => "Quản lý đề nghị quyết toán dự án hoàn thành";
        public override string Description => "Danh sách đề nghị quyết toán dự án hoàn thành";

        public override Type ContentType => typeof(DeNghiQTDAHTDialogDetail);

        private NhQtQuyetToanDahtModel _nhQtQuyetToanDahtModel;
        public NhQtQuyetToanDahtModel NhQtQuyetToanDahtModel
        {
            get => _nhQtQuyetToanDahtModel;
            set => SetProperty(ref _nhQtQuyetToanDahtModel, value);
        }

        private ObservableCollection<NHDAQDDauTuChiPhiHangMucModel> _items;
        public ObservableCollection<NHDAQDDauTuChiPhiHangMucModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private NHDAQDDauTuChiPhiHangMucModel _selectedQDDauTuChiPhiHangMuc;
        public NHDAQDDauTuChiPhiHangMucModel SelectedQDDauTuChiPhiHangMuc
        {
            get => _selectedQDDauTuChiPhiHangMuc;
            set => SetProperty(ref _selectedQDDauTuChiPhiHangMuc, value);
        }

        private List<NHDAQDDauTuChiPhiHangMucModel> _finalResult { get; set; }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        public int Year { get; set; }
        public bool IsAdd { get; set; }

        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTiet { get; set; }
        public NhDmTiGiaModel SelectedTiGia { get; set; }
        public NhDmTiGiaChiTietModel SelectedTiGiaChiTiet { get; set; }

        public DeNghiQTDAHTDialogDetailViewModel(
            IMapper mapper, 
            IStorageServiceFactory storageServiceFactory, 
            IAttachmentService attachService,
            INhDaQdDauTuService nhDaQdDauTuService,
            ISessionService sessionService,
            INhDmTiGiaService nhDmTiGiaService,
            INhQtQuyetToanDahtService nhQtQuyetToanDahtService) : base(mapper, storageServiceFactory, attachService)
        {
            _nhDaQdDauTuService = nhDaQdDauTuService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _sessionService = sessionService;
            _nhQtQuyetToanDahtService = nhQtQuyetToanDahtService;
        }

        public override void Init()
        {
            base.Init();
            Year = _sessionService.Current.YearOfWork;
            IEnumerable<NHDAQDDauTuChiPhiHangMuc> data = _nhQtQuyetToanDahtService.GetChiPhiHangMucByDuAnId(NhQtQuyetToanDahtModel.IIdDuAnId.Value, IsAdd ? Guid.Empty : NhQtQuyetToanDahtModel.Id);
            Items = _mapper.Map<ObservableCollection<NHDAQDDauTuChiPhiHangMucModel>>(data);
            _finalResult = OrderChiPhi(Items);
            for (int i = 0; i < _finalResult.Count; i++)
            {
                if (_finalResult.ElementAt(i).IType == NHDAQDDauTuChiPhiHangMucModel_Loai.CP)
                {
                    AddHmToCp(i, _finalResult.ElementAt(i));
                }
            }
            var sortingListQueries = new List<SortingListQuery<NHDAQDDauTuChiPhiHangMucModel>>();
            sortingListQueries = SortingHierarchicalNumber<NHDAQDDauTuChiPhiHangMucModel>.GetSortingLists(_finalResult.Select(x => new SortingListQuery<NHDAQDDauTuChiPhiHangMucModel>() { Key = x, SortKey = x.STT }).ToList());
            Items = new ObservableCollection<NHDAQDDauTuChiPhiHangMucModel>(sortingListQueries.Select(x => x.Key));
            SetSumChiPhi();
            foreach (var item in Items)
            {
                item.PropertyChanged += ChiPhi_PropertyChanged;
            }
            OnPropertyChanged(nameof(Items));
        }

        private List<NHDAQDDauTuChiPhiHangMucModel> OrderChiPhi(IEnumerable<NHDAQDDauTuChiPhiHangMucModel> datas)
        {
            int index = 1;
            List<NHDAQDDauTuChiPhiHangMucModel> results = new List<NHDAQDDauTuChiPhiHangMucModel>();
            var cp = datas.Where(n => n.IType == NHDAQDDauTuChiPhiHangMucModel_Loai.CP);
            foreach (var item in cp.Where(n => n.ParentId == null || !n.ParentId.HasValue))
            {
                item.STT = index.ToString();
                index++;
                results.AddRange(RecusiveChiPhi(item, cp.ToList()));
            }
            return results;
        }

        private List<NHDAQDDauTuChiPhiHangMucModel> RecusiveChiPhi(NHDAQDDauTuChiPhiHangMucModel item, List<NHDAQDDauTuChiPhiHangMucModel> datas)
        {
            List<NHDAQDDauTuChiPhiHangMucModel> results = new List<NHDAQDDauTuChiPhiHangMucModel>();
            List<NHDAQDDauTuChiPhiHangMucModel> rootItems = datas.Where(n => n.ParentId == item.Id).ToList();
            if (rootItems.Any())
            {
                item.IsHangCha = true;
            }
            results.Add(item);
            for (int j = 0; j < rootItems.Count; j++)
            {
                var child = rootItems.ElementAt(j);
                child.STT = item.STT + "." + (j + 1);
                results.AddRange(RecusiveChiPhi(child, datas));
            }
            return results;
        }

        private void AddHmToCp(int index, NHDAQDDauTuChiPhiHangMucModel cp)
        {
            var hm = Items.Where(n => n.IType == NHDAQDDauTuChiPhiHangMucModel_Loai.HM && n.ChiPhiId == cp.Id).ToList();
            if (hm.Any())
            {
                cp.IsHangCha = true;
            }
            int i = 0;
            foreach (var item in hm.Where(n => !n.ParentId.HasValue))
            {
                item.STT = cp.STT + "." + (i + 1);
                _finalResult.InsertRange(index + 1, RecusiveHM(item, hm));
                i++;
            }
        }

        private List<NHDAQDDauTuChiPhiHangMucModel> RecusiveHM(NHDAQDDauTuChiPhiHangMucModel item, List<NHDAQDDauTuChiPhiHangMucModel> datas)
        {
            List<NHDAQDDauTuChiPhiHangMucModel> results = new List<NHDAQDDauTuChiPhiHangMucModel>();
            var hangMucChilds = datas.Where(n => n.ParentId == item.Id).ToList();
            if (hangMucChilds.Any())
            {
                item.IsHangCha = true;
            }
            results.Add(item);
            int i = 0;
            foreach (var child in hangMucChilds)
            {
                child.STT = item.STT + "." + (i + 1);
                results.AddRange(RecusiveHM(child, datas));
                i++;
            }
            return results;
        }

        private void SetSumChiPhi()
        {
            var chiPhis = Items.Where(n => n.IType == NHDAQDDauTuChiPhiHangMucModel_Loai.CP && (n.ParentId == null || !n.ParentId.HasValue));
            if (chiPhis != null && chiPhis.Any())
            {
                NhQtQuyetToanDahtModel.FTongUSDDT = chiPhis.Sum(x => x.USDDT);
                NhQtQuyetToanDahtModel.FTongVNDDT = chiPhis.Sum(x => x.VNDDT);
                NhQtQuyetToanDahtModel.FTongEURODT = chiPhis.Sum(x => x.EURODT);
                NhQtQuyetToanDahtModel.FTongNgoaiTeDT = chiPhis.Sum(x => x.NgoaiTeDT);
                NhQtQuyetToanDahtModel.FTongUSDQT = chiPhis.Sum(x => x.USDQT);
                NhQtQuyetToanDahtModel.FTongVNDQT = chiPhis.Sum(x => x.VNDQT);
                NhQtQuyetToanDahtModel.FTongEUROQT = chiPhis.Sum(x => x.EUROQT);
                NhQtQuyetToanDahtModel.FTongNgoaiTeQT = chiPhis.Sum(x => x.NgoaiTeQT);
                NhQtQuyetToanDahtModel.FTongUSDKT = chiPhis.Sum(x => x.USDKT);
                NhQtQuyetToanDahtModel.FTongVNDKT = chiPhis.Sum(x => x.VNDKT);
                NhQtQuyetToanDahtModel.FTongEUROKT = chiPhis.Sum(x => x.EUROKT);
                NhQtQuyetToanDahtModel.FTongNgoaiTeKT = chiPhis.Sum(x => x.NgoaiTeKT);
                NhQtQuyetToanDahtModel.FTongUSDCDT = chiPhis.Sum(x => x.USDCDT);
                NhQtQuyetToanDahtModel.FTongVNDCDT = chiPhis.Sum(x => x.VNDCDT);
                NhQtQuyetToanDahtModel.FTongEUROCDT = chiPhis.Sum(x => x.EUROCDT);
                NhQtQuyetToanDahtModel.FTongNgoaiTeCDT = chiPhis.Sum(x => x.NgoaiTeCDT);
                NhQtQuyetToanDahtModel.FTongUSDSSDT = chiPhis.Sum(x => x.USDSSDT);
                NhQtQuyetToanDahtModel.FTongVNDSSDT = chiPhis.Sum(x => x.VNDSSDT);
                NhQtQuyetToanDahtModel.FTongEUROSSDT = chiPhis.Sum(x => x.EUROSSDT);
                NhQtQuyetToanDahtModel.FTongNgoaiTeSSDT = chiPhis.Sum(x => x.NgoaiTeSSDT);
                NhQtQuyetToanDahtModel.FTongUSDSSQT = chiPhis.Sum(x => x.USDSSQT);
                NhQtQuyetToanDahtModel.FTongVNDSSQT = chiPhis.Sum(x => x.VNDSSQT);
                NhQtQuyetToanDahtModel.FTongEUROSSQT = chiPhis.Sum(x => x.EUROSSQT);
                NhQtQuyetToanDahtModel.FTongNgoaiTeSSQT = chiPhis.Sum(x => x.NgoaiTeSSQT);
                NhQtQuyetToanDahtModel.FTongUSDSSKT = chiPhis.Sum(x => x.USDSSKT);
                NhQtQuyetToanDahtModel.FTongVNDSSKT = chiPhis.Sum(x => x.VNDSSKT);
                NhQtQuyetToanDahtModel.FTongEUROSSKT = chiPhis.Sum(x => x.EUROSSKT);
                NhQtQuyetToanDahtModel.FTongNgoaiTeSSKT = chiPhis.Sum(x => x.NgoaiTeSSKT);
            }
        }

        public void ChiPhiHangMuc_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedQDDauTuChiPhiHangMuc = (NHDAQDDauTuChiPhiHangMucModel)e.Row.Item;

            if (e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.USDDT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.VNDDT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.EURODT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.NgoaiTeDT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.USDQT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.VNDQT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.EUROQT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.NgoaiTeQT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.USDKT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.VNDKT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.EUROKT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.NgoaiTeKT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.USDCDT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.VNDCDT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.EUROCDT))
                || e.Column.SortMemberPath.Equals(nameof(NHDAQDDauTuChiPhiHangMucModel.NgoaiTeCDT)))
            {
                if (SelectedQDDauTuChiPhiHangMuc != null && SelectedQDDauTuChiPhiHangMuc.IsHangCha)
                {
                    e.Cancel = true;
                }
            }
        }

        private void ChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var obj = (NHDAQDDauTuChiPhiHangMucModel)sender;
            foreach (var item in Items)
            {
                item.PropertyChanged -= ChiPhi_PropertyChanged;
            }
            var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
            string rootCurrency = SelectedTiGia.SMaTienTeGoc;
            string sourceCurrency;
            string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
            double value;
            switch (args.PropertyName)
            {
                case nameof(NHDAQDDauTuChiPhiHangMucModel.VNDDT):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                    value = obj.VNDDT.GetValueOrDefault();
                    obj.VNDDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EURODT= _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDDT), nameof(obj.EURODT), nameof(obj.USDDT), nameof(obj.NgoaiTeDT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDDT), nameof(obj.EURODT), nameof(obj.USDDT), nameof(obj.NgoaiTeDT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.EURODT):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                    value = obj.EURODT.GetValueOrDefault();
                    obj.VNDDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EURODT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDDT), nameof(obj.EURODT), nameof(obj.USDDT), nameof(obj.NgoaiTeDT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDDT), nameof(obj.EURODT), nameof(obj.USDDT), nameof(obj.NgoaiTeDT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.NgoaiTeDT):
                    sourceCurrency = otherCurrency;
                    value = obj.NgoaiTeDT.GetValueOrDefault();
                    obj.VNDDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EURODT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDDT), nameof(obj.EURODT), nameof(obj.USDDT), nameof(obj.NgoaiTeDT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDDT), nameof(obj.EURODT), nameof(obj.USDDT), nameof(obj.NgoaiTeDT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.USDDT):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    value = obj.USDDT.GetValueOrDefault();
                    obj.VNDDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EURODT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDDT), nameof(obj.EURODT), nameof(obj.USDDT), nameof(obj.NgoaiTeDT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDDT), nameof(obj.EURODT), nameof(obj.USDDT), nameof(obj.NgoaiTeDT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.VNDQT):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                    value = obj.VNDQT.GetValueOrDefault();
                    obj.VNDQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EUROQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDQT), nameof(obj.EUROQT), nameof(obj.USDQT), nameof(obj.NgoaiTeQT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDQT), nameof(obj.EUROQT), nameof(obj.USDQT), nameof(obj.NgoaiTeQT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.EUROQT):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                    value = obj.EUROQT.GetValueOrDefault();
                    obj.VNDQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EUROQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDQT), nameof(obj.EUROQT), nameof(obj.USDQT), nameof(obj.NgoaiTeQT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDQT), nameof(obj.EUROQT), nameof(obj.USDQT), nameof(obj.NgoaiTeQT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.NgoaiTeQT):
                    sourceCurrency = otherCurrency;
                    value = obj.NgoaiTeQT.GetValueOrDefault();
                    obj.VNDQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EUROQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDQT), nameof(obj.EUROQT), nameof(obj.USDQT), nameof(obj.NgoaiTeQT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDQT), nameof(obj.EUROQT), nameof(obj.USDQT), nameof(obj.NgoaiTeQT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.USDQT):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    value = obj.USDQT.GetValueOrDefault();
                    obj.VNDQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EUROQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeQT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDQT), nameof(obj.EUROQT), nameof(obj.USDQT), nameof(obj.NgoaiTeQT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDQT), nameof(obj.EUROQT), nameof(obj.USDQT), nameof(obj.NgoaiTeQT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.VNDKT):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                    value = obj.VNDKT.GetValueOrDefault();
                    obj.VNDKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EUROKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDKT), nameof(obj.EUROKT), nameof(obj.USDKT), nameof(obj.NgoaiTeKT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDKT), nameof(obj.EUROKT), nameof(obj.USDKT), nameof(obj.NgoaiTeKT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.EUROKT):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                    value = obj.EUROKT.GetValueOrDefault();
                    obj.VNDKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EUROKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDKT), nameof(obj.EUROKT), nameof(obj.USDKT), nameof(obj.NgoaiTeKT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDKT), nameof(obj.EUROKT), nameof(obj.USDKT), nameof(obj.NgoaiTeKT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.NgoaiTeKT):
                    sourceCurrency = otherCurrency;
                    value = obj.NgoaiTeKT.GetValueOrDefault();
                    obj.VNDKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EUROKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDKT), nameof(obj.EUROKT), nameof(obj.USDKT), nameof(obj.NgoaiTeKT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDKT), nameof(obj.EUROKT), nameof(obj.USDKT), nameof(obj.NgoaiTeKT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.USDKT):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    value = obj.USDKT.GetValueOrDefault();
                    obj.VNDKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EUROKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeKT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDKT), nameof(obj.EUROKT), nameof(obj.USDKT), nameof(obj.NgoaiTeKT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDKT), nameof(obj.EUROKT), nameof(obj.USDKT), nameof(obj.NgoaiTeKT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.VNDCDT):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                    value = obj.VNDCDT.GetValueOrDefault();
                    obj.VNDCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EUROCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDCDT), nameof(obj.EUROCDT), nameof(obj.USDCDT), nameof(obj.NgoaiTeCDT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDCDT), nameof(obj.EUROCDT), nameof(obj.USDCDT), nameof(obj.NgoaiTeCDT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.EUROCDT):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                    value = obj.EUROCDT.GetValueOrDefault();
                    obj.VNDCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EUROCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDCDT), nameof(obj.EUROCDT), nameof(obj.USDCDT), nameof(obj.NgoaiTeCDT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDCDT), nameof(obj.EUROCDT), nameof(obj.USDCDT), nameof(obj.NgoaiTeCDT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.NgoaiTeCDT):
                    sourceCurrency = otherCurrency;
                    value = obj.NgoaiTeCDT.GetValueOrDefault();
                    obj.VNDCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EUROCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDCDT), nameof(obj.EUROCDT), nameof(obj.USDCDT), nameof(obj.NgoaiTeCDT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDCDT), nameof(obj.EUROCDT), nameof(obj.USDCDT), nameof(obj.NgoaiTeCDT));
                    SetSumChiPhi();
                    break;
                case nameof(NHDAQDDauTuChiPhiHangMucModel.USDCDT):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    value = obj.USDCDT.GetValueOrDefault();
                    obj.VNDCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    obj.EUROCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    obj.USDCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    obj.NgoaiTeCDT = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    UpdateParentData(obj, nameof(obj.VNDCDT), nameof(obj.EUROCDT), nameof(obj.USDCDT), nameof(obj.NgoaiTeCDT));
                    UpdateCPDataOfHM(obj, nameof(obj.VNDCDT), nameof(obj.EUROCDT), nameof(obj.USDCDT), nameof(obj.NgoaiTeCDT));
                    SetSumChiPhi();
                    break;
            }
            foreach (var item in Items)
            {
                item.PropertyChanged += ChiPhi_PropertyChanged;
            }
        }

        private void UpdateParentData(NHDAQDDauTuChiPhiHangMucModel obj, params string[] args)
        {
            var parent = Items.FirstOrDefault(t => t.Id.Equals(obj.ParentId));
            if (parent == null)
                return;
            List<NHDAQDDauTuChiPhiHangMucModel> children = Items.Where(t => t.ParentId.Equals(parent.Id)).ToList();
            foreach (string arg in args)
            {
                double? sum = CalculateSum(arg, children);
                PropertyInfo property = parent.GetType().GetProperty(arg);
                property.SetValue(parent, sum, null);
            }
            UpdateParentData(parent, args);
        }

        private void UpdateCPDataOfHM(NHDAQDDauTuChiPhiHangMucModel obj, params string[] args)
        {
            var chiphi = Items.FirstOrDefault(t => t.Id.Equals(obj.ChiPhiId));
            if (chiphi == null)
                return;
            List<NHDAQDDauTuChiPhiHangMucModel> children = Items.Where(t => t.ChiPhiId.Equals(chiphi.Id) && !t.ParentId.HasValue).ToList();
            foreach (string arg in args)
            {
                double? sum = CalculateSum(arg, children);
                PropertyInfo property = chiphi.GetType().GetProperty(arg);
                property.SetValue(chiphi, sum, null);
            }
            UpdateParentData(chiphi, args);
        }

        private double? CalculateSum(string arg, IEnumerable<NHDAQDDauTuChiPhiHangMucModel> items)
        {
            double? sum = 0;
            foreach (var item in items)
            {
                PropertyInfo property = item.GetType().GetProperty(arg);
                double? value = (double?)property.GetValue(item, null);
                if (!value.HasValue)
                    value = 0;
                sum += value;
            }
            return sum == 0 ? null : sum;
        }

        public override void OnSave(object obj)
        {
            base.OnSave(obj);
            List<NhQtQuyetToanDahtChiTiet> entities = new List<NhQtQuyetToanDahtChiTiet>();
            foreach (var item in Items)
            {
                NhQtQuyetToanDahtChiTiet entity = new NhQtQuyetToanDahtChiTiet();
                entity.IIdDeNghiQuyetToanDahtId = NhQtQuyetToanDahtModel.Id;
                entity.IIDHMCP = item.Id;
                entity.FGiaTriQuyetToanAbUsd = item.USDQT;
                entity.FGiaTriQuyetToanAbVnd = item.VNDQT;
                entity.FGiaTriQuyetToanAbEur = item.EUROQT;
                entity.FGiaTriQuyetToanAbNgoaiTeKhac = item.NgoaiTeQT;
                entity.FKetQuaKiemToanUsd = item.USDKT;
                entity.FKetQuaKiemToanVnd = item.VNDKT;
                entity.FKetQuaKiemToanEur = item.EUROKT;
                entity.FKetQuaKiemToanNgoaiTeKhac = item.NgoaiTeKT;
                entity.FDeNghiQuyetToanUsd = item.USDCDT;
                entity.FDeNghiQuyetToanVnd = item.VNDCDT;
                entity.FDeNghiQuyetToanEur = item.EUROCDT;
                entity.FDeNghiQuyetToanNgoaiTeKhac = item.NgoaiTeCDT;

                entity.FDeNghiSoVoiQuyetToanAbUsd = item.USDSSQT;
                entity.FDeNghiSoVoiQuyetToanAbEur = item.EUROSSQT;
                entity.FDeNghiSoVoiQuyetToanAbVnd = item.VNDSSQT;
                entity.FDeNghiSoVoiQuyetToanAbNgoaiTeKhac = item.NgoaiTeSSQT;

                entity.FDeNghiSoVoiKetQuaKiemToanUsd = item.USDSSKT;
                entity.FDeNghiSoVoiKetQuaKiemToanEur = item.EUROSSKT;
                entity.FDeNghiSoVoiKetQuaKiemToanVnd = item.VNDSSKT;
                entity.FDeNghiSoVoiKetQuaKiemToanNgoaiTeKhac = item.NgoaiTeSSKT;
                entity.IType = item.IType;
                entities.Add(entity);
            }
            _nhQtQuyetToanDahtService.SaveNhQtQuyetToanDahtChiTiet(entities, NhQtQuyetToanDahtModel.Id);
            MessageBoxHelper.Info("Lưu dữ liệu thành công");
            /*NHDAQDDauTuChiPhiHangMucModel summary = new NHDAQDDauTuChiPhiHangMucModel();
            summary.USDCDT = Items.Sum(t => t.USDCDT);
            summary.EUROCDT = Items.Sum(t => t.EUROCDT);
            summary.VNDCDT = Items.Sum(t => t.VNDCDT);
            summary.NgoaiTeCDT = Items.Sum(t => t.NgoaiTeCDT);
            SavedAction.Invoke(summary);*/
            var view = obj as Window;
            view.Close();
        }
    }
}
