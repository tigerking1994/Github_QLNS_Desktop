using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ExchangeRate;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ExchangeRate
{
    public class ExchangeRateDialogViewModel : DialogViewModelBase<NhDmTiGiaModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly INhDmLoaiTienTeService _nhDmLoaiTienTeService;

        public override string FuncCode => NSFunctionCode.CATEGORY_FOREX_EXCHANGE_RATE;
        public override string Name
        {
            get
            {
                if (Model.Id == Guid.Empty)
                {
                    return "THÊM TỈ GIÁ";
                }
                else
                {
                    return "CẬP NHẬT TỈ GIÁ";
                }
            }
        }
        public override string Description => "Tỉ giá";
        public override Type ContentType => typeof(ExchangeRateDialog);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public bool IsEditable => Model.Id == Guid.Empty;

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTiet;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTiet
        {
            get => _itemsTiGiaChiTiet;
            set => SetProperty(ref _itemsTiGiaChiTiet, value);
        }

        private NhDmTiGiaChiTietModel _selectedTiGiaChiTiet;
        public NhDmTiGiaChiTietModel SelectedTiGiaChiTiet
        {
            get => _selectedTiGiaChiTiet;
            set => SetProperty(ref _selectedTiGiaChiTiet, value);
        }

        private ComboboxItem _selectedLoaiTienTe;
        public ComboboxItem SelectedLoaiTienTe
        {
            get => _selectedLoaiTienTe;
            set
            {
                if (value != null)
                {
                    SetProperty(ref _selectedLoaiTienTe, value);

                    ItemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
                    foreach (var item in ItemsLoaiTienTe)
                    {
                        if (item.DisplayItem != _selectedLoaiTienTe.DisplayItem)
                        {
                            var itemTiGia = new NhDmTiGiaChiTietModel();
                            itemTiGia.SMaTienTeQuyDoi = item.DisplayItem;
                            ItemsTiGiaChiTiet.Add(itemTiGia);
                        }
                    }
                }
            }
        }
        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set
            {
                if (value != null)
                {
                    SetProperty(ref _monthSelected, value);
                    LoadMonthYearChange();
                }
            }
        }
        private int _iNamTiGia;
        public int INamTiGia
        {
            get => _iNamTiGia;
            set
            {
                SetProperty(ref _iNamTiGia, value);
                OnPropertyChanged(nameof(INamTiGia));
                LoadMonthYearChange();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiTienTe;
        public ObservableCollection<ComboboxItem> ItemsLoaiTienTe
        {
            get => _itemsLoaiTienTe;
            set => SetProperty(ref _itemsLoaiTienTe, value);
        }

        public RelayCommand AddTiGiaDetailCommand { get; }
        public RelayCommand DeleteTiGiaDetailCommand { get; }

        public ExchangeRateDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmLoaiTienTeService nhDmLoaiTienTeService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmLoaiTienTeService = nhDmLoaiTienTeService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;

            AddTiGiaDetailCommand = new RelayCommand(obj => OnAddTiGiaDetail());
            DeleteTiGiaDetailCommand = new RelayCommand(obj => OnDeleteTiGiaDetail());
        }

        public override void Init()
        {
            try
            {
                LoadLoaiTienTe();
                LoadData();
                LoadMonths();
                LoadMasterDataModel();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadMasterDataModel()
        {
            if (Model.Id == null || Model.Id == Guid.Empty)
                _iNamTiGia = DateTime.Now.Year;
            else
                _iNamTiGia = (int)Model.INamTiGia;
        }

        private void LoadMonthYearChange()
        {
            Model.IThangTiGia = int.Parse(MonthSelected.ValueItem);
            Model.INamTiGia = INamTiGia;
            Model.STenTiGia = Model.STenTiGiaFormat;
        }

        private void LoadLoaiTienTe()
        {
            var drpData = _nhDmLoaiTienTeService.FindAll()
                .Select(n => new ComboboxItem() { Id = n.Id, DisplayItem = n.SMaTienTe, ValueItem = n.SMaTienTe });
            ItemsLoaiTienTe = _mapper.Map<ObservableCollection<ComboboxItem>>(drpData);
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                if (Model.Id == Guid.Empty)
                {
                    Model.DNgayTao = DateTime.Now;
                    if (ItemsLoaiTienTe != null && ItemsLoaiTienTe.Count() > 0)
                    {
                        if (ItemsLoaiTienTe.Select(n => n.DisplayItem.ToUpper()).Contains("USD"))
                        {
                            SelectedLoaiTienTe = ItemsLoaiTienTe.Where(n => n.DisplayItem.ToUpper().Equals("USD")).FirstOrDefault();
                        }
                        else
                        {
                            SelectedLoaiTienTe = ItemsLoaiTienTe.FirstOrDefault();
                        }
                        ItemsLoaiTienTe = _mapper.Map<ObservableCollection<ComboboxItem>>(ItemsLoaiTienTe.Where(x => x.DisplayItem.ToUpper().Contains(LoaiTienTeEnum.TypeCode.USD)));
                    }
                }
                else
                {
                    ItemsLoaiTienTe = _mapper.Map<ObservableCollection<ComboboxItem>>(ItemsLoaiTienTe.Where(x => x.DisplayItem.ToUpper().Contains(LoaiTienTeEnum.TypeCode.USD)));
                    SelectedLoaiTienTe = ItemsLoaiTienTe.FirstOrDefault(x => x.Id == Model.IIdTienTeGocId);
                    LoadDataTiGiaChiTietByTiGia();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDataTiGiaChiTietByTiGia()
        {
            List<NhDmTiGiaChiTiet> listData = _nhDmTiGiaChiTietService.FindByTiGiaId(Model.Id).ToList();
            ItemsTiGiaChiTiet = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(listData);

            var listTiGia = ItemsTiGiaChiTiet.Select(n => n.SMaTienTeQuyDoi);
            foreach (var item in ItemsLoaiTienTe)
            {
                if (item.DisplayItem != _selectedLoaiTienTe.DisplayItem && !listTiGia.Contains(item.DisplayItem))
                {
                    var itemTiGia = new NhDmTiGiaChiTietModel();
                    itemTiGia.SMaTienTeQuyDoi = item.DisplayItem;
                    ItemsTiGiaChiTiet.Add(itemTiGia);
                }
            }
        }

        public override void OnSave()
        {
            try
            {
                if (SelectedLoaiTienTe != null)
                {
                    Model.IIdTienTeGocId = SelectedLoaiTienTe.Id;
                    Model.SMaTienTeGoc = SelectedLoaiTienTe.ValueItem;
                }
                if (!ValidateViewModelHelper.Validate(Model)) return;
                if (ValidationData()) return;

                NhDmTiGia entity;
                if (Model.Id == Guid.Empty)
                {
                    // Thêm mới
                    entity = new NhDmTiGia();
                    _mapper.Map(Model, entity);

                    entity.Id = Guid.NewGuid();
                    entity.IIdTienTeGocId = _selectedLoaiTienTe.Id;
                    entity.SMaTienTeGoc = _selectedLoaiTienTe.ValueItem;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    _nhDmTiGiaService.Add(entity);
                    Model.Id = entity.Id;
                    SaveTiGiaChiTiet();
                }
                else
                {
                    // Cập nhật
                    entity = _nhDmTiGiaService.FindById(Model.Id);
                    _mapper.Map(Model, entity);

                    entity.IIdTienTeGocId = _selectedLoaiTienTe.Id;
                    entity.SMaTienTeGoc = _selectedLoaiTienTe.ValueItem;
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    _nhDmTiGiaService.Update(entity);
                    SaveTiGiaChiTiet();
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<NhDmTiGiaModel>(entity));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ValidationData()
        {
            StringBuilder messageBuilder = new StringBuilder();

            //if (_selectedLoaiTienTe == null)
            //{
            //    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Loại tiền tệ");
            //}
            //if (string.IsNullOrEmpty(Model.SMaTiGia))
            //{
            //    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Mã tỉ giá");
            //}
            //if (string.IsNullOrEmpty(Model.STenTiGia))
            //{
            //    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Tên tỉ giá");
            //}
            if (string.IsNullOrEmpty(_monthSelected.ValueItem))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Tháng");
            }
            else
            {
                var predicate = PredicateBuilder.True<NhDmTiGia>();
                predicate = predicate.And(x => x.INamTiGia == Model.INamTiGia);
                predicate = predicate.And(x => x.IThangTiGia == Model.IThangTiGia);
                predicate = predicate.And(x => x.Id != Model.Id);
                var isCheck = _nhDmTiGiaService.FindByCondition(predicate).Any();
                if (isCheck)
                {
                    messageBuilder.AppendFormat(Resources.InvalidMonthExchangeRate, MonthSelected.ValueItem);
                }
            }

            if (!Model.INamTiGia.HasValue || Model.INamTiGia == 0)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Năm");
            }

            if (ItemsTiGiaChiTiet.Any(x => x.SMaTienTeQuyDoi.Contains(LoaiTienTeEnum.TypeCode.VND) && NumberUtils.DoubleIsNullOrZero(x.FTiGia)))
            {
                messageBuilder.AppendFormat(Resources.ExhangeRateInValid);
            }

            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                return true;
            }
            return false;
        }

        public override void OnClose(object obj)
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnAddTiGiaDetail()
        {
            NhDmTiGiaChiTietModel newItem = new NhDmTiGiaChiTietModel();
            ItemsTiGiaChiTiet.Insert(ItemsTiGiaChiTiet.Count, newItem);
             OnPropertyChanged(nameof(ItemsTiGiaChiTiet));
        }

        private void OnDeleteTiGiaDetail()
        {
            if (SelectedTiGiaChiTiet != null)
            {
                SelectedTiGiaChiTiet.IsDeleted = !SelectedTiGiaChiTiet.IsDeleted;
            }
            OnPropertyChanged(nameof(ItemsTiGiaChiTiet));
        }

        private void SaveTiGiaChiTiet()
        {
            var lstInsert = ItemsTiGiaChiTiet.Where(x => x.FTiGia != null && x.FTiGia != 0 && x.Id == Guid.Empty).ToList();
            var lstUpdate = ItemsTiGiaChiTiet.Where(x => x.FTiGia != null && x.FTiGia != 0 && x.Id != Guid.Empty).ToList();
            var lstDelete = ItemsTiGiaChiTiet.Where(x => (x.FTiGia == null || x.FTiGia == 0) && x.Id != Guid.Empty).ToList();

            if (lstInsert != null && lstInsert.Count > 0)
            {
                AddTiGiaChiTietSave(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count > 0)
            {
                UpdateTiGiaChiTietSave(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count > 0)
            {
                DeleteTiGiaChiTietSave(lstDelete);
            }
        }

        private void DeleteTiGiaChiTietSave(List<NhDmTiGiaChiTietModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _nhDmTiGiaChiTietService.Delete(item.Id);
            }
        }

        private void AddTiGiaChiTietSave(List<NhDmTiGiaChiTietModel> listAdd)
        {
            foreach (var item in listAdd)
            {
                item.SMaTienTeQuyDoi = item.SMaTienTeQuyDoi;
                item.IIdTiGiaId = Model.Id;
            }
            var tiGiaChiTiet = _mapper.Map<List<NhDmTiGiaChiTiet>>(listAdd);
            foreach (var item in tiGiaChiTiet)
            {
                item.Id = Guid.NewGuid();
                _nhDmTiGiaChiTietService.Add(item);
            }
        }

        private void UpdateTiGiaChiTietSave(List<NhDmTiGiaChiTietModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                NhDmTiGiaChiTiet tiGiaChiTiet = _nhDmTiGiaChiTietService.FindById(item.Id);
                if (tiGiaChiTiet != null)
                {
                    tiGiaChiTiet.IIdTienTeId = item.IIdTienTeId;
                    tiGiaChiTiet.SMaTienTeQuyDoi = item.SMaTienTeQuyDoi;
                    tiGiaChiTiet.FTiGia = item.FTiGia;
                    _nhDmTiGiaChiTietService.Update(tiGiaChiTiet);
                }
            }
        }

        /// <summary>
        /// Tạo data cho combobox tháng
        /// </summary>
        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();

            for (int i = 1; i <= 12; i++)
            {
                _months.Add(new ComboboxItem("Tháng " + i, i.ToString()));
            }

            if (Model.Id == Guid.Empty)
                MonthSelected = _months.FirstOrDefault(x => x.ValueItem.Equals(DateTime.Now.Month.ToString()));
            else
                MonthSelected = _months.FirstOrDefault(x => x.ValueItem.Equals(Model.IThangTiGia.ToString()));
        }
    }
}
