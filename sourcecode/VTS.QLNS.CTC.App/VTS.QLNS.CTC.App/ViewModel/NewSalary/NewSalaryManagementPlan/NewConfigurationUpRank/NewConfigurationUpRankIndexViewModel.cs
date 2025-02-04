using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewConfigurationUpRank
{
    public class NewConfigurationUpRankIndexViewModel : GridViewModelBase<TlDmCapBacKeHoachNq104Model>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmCapBacNq104Service _tlDmCapBacService;
        private readonly ITlDmCapBacKeHoachNq104Service _tlDmCapBacKeHoachService;
        private readonly ITlDmHslKeHoachService _tlDmHslKeHoachService;
        private readonly ITlDmCapBacLuongNq104Service _tlDmCapBacLuongService;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_QUAN_LY_LUONG_KE_HOACH_CAU_HINH_THOI_HAN_TANG_QUAN_HAM_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Title => "Cấu hình thời hạn tăng quân hàm";
        public override string Name => "Cấu hình thời hạn tăng quân hàm";
        public override string Description => "Tổng số bản ghi: " + TlDmCapBacKeHoachItems.Count();
        public override PackIconKind IconKind => PackIconKind.ChevronTripleUp;
        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagementPlan.NewConfigurationUpRank.NewConfigurationUpRank);

        private ObservableCollection<ComboboxItem> _ngachCapBacItems;
        public ObservableCollection<ComboboxItem> NgachCapBacItems
        {
            get => _ngachCapBacItems;
            set => SetProperty(ref _ngachCapBacItems, value);
        }

        private ObservableCollection<ComboboxItem> _maCapBacItem;
        public ObservableCollection<ComboboxItem> MaCapBacItems
        {
            get => _maCapBacItem;
            set => SetProperty(ref _maCapBacItem, value);
        }

        private ObservableCollection<ComboboxItem> _maCapBacKHItem;
        public ObservableCollection<ComboboxItem> MaCapBacKHItems
        {
            get => _maCapBacKHItem;
            set => SetProperty(ref _maCapBacKHItem, value);
        }

        private ObservableCollection<ComboboxItem> _lstHslKeHoach;
        public ObservableCollection<ComboboxItem> LstHslKeHoach
        {
            get => _lstHslKeHoach;
            set => SetProperty(ref _lstHslKeHoach, value);
        }

        private ObservableCollection<ComboboxItem> _LstBacLuongHienTai;
        public ObservableCollection<ComboboxItem> LstBacLuongHienTai
        {
            get => _LstBacLuongHienTai;
            set => SetProperty(ref _LstBacLuongHienTai, value);
        }

        private ObservableCollection<ComboboxItem> _LstBacLuongKeHoach;
        public ObservableCollection<ComboboxItem> LstBacLuongKeHoach
        {
            get => _LstBacLuongKeHoach;
            set => SetProperty(ref _LstBacLuongKeHoach, value);
        }

        private ObservableCollection<ComboboxItem> _LstBacLuongTran;
        public ObservableCollection<ComboboxItem> LstBacLuongTran
        {
            get => _LstBacLuongTran;
            set => SetProperty(ref _LstBacLuongTran, value);
        }

        private ObservableCollection<ComboboxItem> _lstHslTran;
        public ObservableCollection<ComboboxItem> LstHslTran
        {
            get => _lstHslTran;
            set => SetProperty(ref _lstHslTran, value);
        }

        private ObservableCollection<TlDmCapBacKeHoachNq104Model> _tlDmCapBacKeHoachItems;
        public ObservableCollection<TlDmCapBacKeHoachNq104Model> TlDmCapBacKeHoachItems
        {
            get => _tlDmCapBacKeHoachItems;
            set
            {
                SetProperty(ref _tlDmCapBacKeHoachItems, value);
                OnPropertyChanged(nameof(Description));
            }
        }

        private TlDmCapBacKeHoachNq104Model _selectedCapBacKeHoach;
        public TlDmCapBacKeHoachNq104Model SelectedCapBacKeHoach
        {
            get => _selectedCapBacKeHoach;
            set
            {
                SetProperty(ref _selectedCapBacKeHoach, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private List<ComboboxItem> _itemsNhom;
        public List<ComboboxItem> ItemsNhom
        {
            get => _itemsNhom;
            set => SetProperty(ref _itemsNhom, value);
        }

        private int currentRow = -1;
        public bool IsEnabled => SelectedCapBacKeHoach != null;
        public bool IsSaveData => TlDmCapBacKeHoachItems.Any(x => x.IsModified || x.IsDeleted);

        public NewConfigurationUpRankIndexViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmCapBacNq104Service tlDmCapBacService,
            ITlDmCapBacKeHoachNq104Service tlDmCapBacKeHoachService,
            ITlDmHslKeHoachService tlDmHslKeHoachService,
            ITlDmCapBacLuongNq104Service tlDmCapBacLuongService)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDmCapBacKeHoachService = tlDmCapBacKeHoachService;
            _tlDmHslKeHoachService = tlDmHslKeHoachService;
            _tlDmCapBacLuongService = tlDmCapBacLuongService;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(10);
            LoadData();
            LoadNgachCapBac();
            LoadMaCapBac();
            //LoadListNhom();
        }

        private void LoadNgachCapBac()
        {
            try
            {
                var data = _tlDmCapBacService.FindParent().OrderBy(x => x.MaCb);
                var dataModel = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(data);
                NgachCapBacItems = _mapper.Map<ObservableCollection<ComboboxItem>>(dataModel);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadMaCapBac()
        {
            try
            {
                var data = _tlDmCapBacService.FindAll();
                var dataModel = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(data);
                MaCapBacItems = _mapper.Map<ObservableCollection<ComboboxItem>>(dataModel);
                MaCapBacKHItems = _mapper.Map<ObservableCollection<ComboboxItem>>(dataModel);
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
                var data = _tlDmCapBacKeHoachService.FindAll().OrderBy(x => x.MaCb).ToList();
                TlDmCapBacKeHoachItems = _mapper.Map<ObservableCollection<TlDmCapBacKeHoachNq104Model>>(data);
                var dataCapBac = _tlDmCapBacService.FindAll();
                var dataCapBacModel = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(dataCapBac).ToList();
                foreach (var item in TlDmCapBacKeHoachItems)
                {
                    var xauNoima = item.Parent + "-" + item.LoaiNhom;
                    var dataBacLuong = _tlDmCapBacLuongService.FindAllByXauNoiMa(xauNoima, _sessionService.Current.YearOfWork);
                    if (dataBacLuong.Any())
                    {
                        var dataBLModel = _mapper.Map<ObservableCollection<TlDmCapBacLuongNq104Model>>(dataBacLuong);
                        item.LstBacLuongHienTai = _mapper.Map<ObservableCollection<ComboboxItem>>(dataBLModel).ToList();
                        item.MaBacLuong = dataBLModel.FirstOrDefault(x => x.MaDm == item.MaBacLuong)?.MaDm ?? null;
                        item.LstBacLuongKeHoach = _mapper.Map<ObservableCollection<ComboboxItem>>(dataBLModel).ToList();
                        item.MaBacLuongKeHoach = dataBLModel.FirstOrDefault(x => x.MaDm == item.MaBacLuongKeHoach)?.MaDm ?? null;
                        item.LstBacLuongTran = _mapper.Map<ObservableCollection<ComboboxItem>>(dataBLModel).ToList();
                        item.MaBacLuongTran = dataBLModel.FirstOrDefault(x => x.MaDm == item.MaBacLuongTran)?.MaDm ?? null;
                    }
                    LoadListNhom(item.Parent);
                    var lstData = GetListChildCapBac(dataCapBacModel, item.Parent).OrderBy(x => x.MaCb);
                    item.ListCapBac = _mapper.Map<ObservableCollection<ComboboxItem>>(lstData).ToList();
                    item.ListCapBacKeHoach = _mapper.Map<ObservableCollection<ComboboxItem>>(lstData).ToList();
                    item.ListLoaiNhom = ItemsNhom;
                    item.NamLamViec = _sessionService.Current.YearOfWork;
                }
                if (TlDmCapBacKeHoachItems.Count > 0)
                {
                    foreach (var item in TlDmCapBacKeHoachItems)
                    {
                        item.PropertyChanged += Detail_PropertyChanged;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private List<TlDmCapBacNq104Model> GetListChildCapBac(List<TlDmCapBacNq104Model> data, string maCb)
        {
            var listCapBac = new List<TlDmCapBacNq104Model>();
            var listChild = data.Where(x => x.Parent == maCb);
            if (listChild.Count() > 0)
            {
                foreach (var child in listChild)
                {
                    listCapBac.Add(child);
                    listCapBac.AddRange(GetListChildCapBac(data, child.MaCb));
                }
            }

            return listCapBac;
        }

        private void LoadListNhom(string sNgach)
        {
            ItemsNhom = new List<ComboboxItem>();

            var listLoaiNhom = _tlDmCapBacLuongService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork).Where(x =>
                                sNgach != null
                                && !string.IsNullOrEmpty(x.LoaiDoiTuong)
                                && x.LoaiDoiTuong.Split(',').Contains(sNgach)
                                && (x.Loai == 1 || x.Loai == 2))
                        .OrderBy(x => x.XauNoiMa)
                        .ToList();

            var listLoai = listLoaiNhom.Where(x => x.Loai == 1).ToList();
            var listNhom = listLoaiNhom.Where(x => x.Loai == 2).ToList();

            var listData = from loai in listLoai
                           join nhom in listNhom on loai.MaDm equals nhom.MaDmCha
                           into gj
                           from full in gj.DefaultIfEmpty()
                           select new
                           {
                               Data = loai,
                               TenNhom = full?.TenDm,
                               MaNhom = full?.MaDm,
                               XauNoiMaNhom = full?.XauNoiMa
                           };
            var listEnd = ObjectCopier.Clone(listData).Select(x =>
            {
                if (!string.IsNullOrEmpty(x.TenNhom))
                {
                    x.Data.LoaiNhom = $"{x.Data.TenDm} - {x.TenNhom}";
                    x.Data.MaNhom = x.MaNhom;
                    x.Data.MaLoai = x.Data.MaDm;
                    x.Data.XauNoiMaNhom = x.XauNoiMaNhom;
                }
                else
                {
                    x.Data.LoaiNhom = x.Data.TenDm;
                }
                return x.Data;
            }).ToList();
            var lstDataModel = _mapper.Map<ObservableCollection<TlDmCapBacLuongNq104Model>>(listEnd);
            ItemsNhom = _mapper.Map<ObservableCollection<ComboboxItem>>(lstDataModel).ToList();
        }

        protected override void OnAdd()
        {
            if (TlDmCapBacKeHoachItems.Count == 0 || SelectedCapBacKeHoach == null)
            {
                TlDmCapBacKeHoachNq104Model tlDmCapBacKeHoachModel = new TlDmCapBacKeHoachNq104Model();
                tlDmCapBacKeHoachModel.PropertyChanged += Detail_PropertyChanged;
                TlDmCapBacKeHoachItems.Add(tlDmCapBacKeHoachModel);
            }
            else
            {
                TlDmCapBacKeHoachNq104Model item = SelectedCapBacKeHoach;
                TlDmCapBacKeHoachNq104Model target = ObjectCopier.Clone(item);

                currentRow = TlDmCapBacKeHoachItems.IndexOf(SelectedCapBacKeHoach);

                target.Id = Guid.Empty;
                target.IsModified = true;

                target.PropertyChanged += Detail_PropertyChanged;
                TlDmCapBacKeHoachItems.Insert(currentRow + 1, target);
            }
            OnPropertyChanged(nameof(TlDmCapBacKeHoachItems));
        }

        public override void OnSave()
        {
            List<TlDmCapBacKeHoachNq104Model> listAdd = TlDmCapBacKeHoachItems.Where(x => x.IsModified && !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null)).ToList();
            List<TlDmCapBacKeHoachNq104Model> listEdit = TlDmCapBacKeHoachItems.Where(x => x.IsModified && !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            List<TlDmCapBacKeHoachNq104Model> listDelete = TlDmCapBacKeHoachItems.Where(x => x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();

            if (listAdd != null && listAdd.Count > 0)
            {
                var lstTlDmCapBacKeHoach = _mapper.Map<List<TlDmCapBacKeHoachNq104>>(listAdd);
                _tlDmCapBacKeHoachService.AddRang(lstTlDmCapBacKeHoach);
            }
            if (listEdit != null && listEdit.Count > 0)
            {
                foreach (var item in listEdit)
                {
                    var lstTlDmCapBacKeHoach = _mapper.Map<TlDmCapBacKeHoachNq104>(item);
                    _tlDmCapBacKeHoachService.UpDate(lstTlDmCapBacKeHoach);
                }
            }
            if (listDelete != null && listDelete.Count > 0)
            {
                foreach (var item in listDelete)
                {
                    _tlDmCapBacKeHoachService.Delete(item.Id);
                }
            }
            LoadData();
        }

        protected override void OnDelete()
        {
            if (TlDmCapBacKeHoachItems != null && TlDmCapBacKeHoachItems.Count > 0 && SelectedCapBacKeHoach != null)
            {
                SelectedCapBacKeHoach.IsDeleted = !SelectedCapBacKeHoach.IsDeleted;
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private void Detail_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlDmCapBacKeHoachNq104Model tlDmCapBacKeHoachModel = (TlDmCapBacKeHoachNq104Model)sender;
            var data = _tlDmCapBacService.FindAll();
            var dataModel = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(data);
            var dataBacLuong = _tlDmCapBacLuongService.FindAllByXauNoiMa(tlDmCapBacKeHoachModel.Parent + "-" + tlDmCapBacKeHoachModel.LoaiNhom, _sessionService.Current.YearOfWork);
            var dataBLModel = _mapper.Map<ObservableCollection<TlDmCapBacLuongNq104Model>>(dataBacLuong);
            if (args.PropertyName == nameof(tlDmCapBacKeHoachModel.Parent))
            {
                LoadListNhom(tlDmCapBacKeHoachModel.Parent);
                tlDmCapBacKeHoachModel.Parent = NgachCapBacItems.FirstOrDefault(x => x.ValueItem == tlDmCapBacKeHoachModel.Parent).ValueItem;
                var dataMaCapBacItems = dataModel.Where(x => x.Parent == tlDmCapBacKeHoachModel.Parent).OrderBy(x => x.MaCb).ToList();
                
                if (dataBacLuong.Any())
                {
                    LstBacLuongHienTai = _mapper.Map<ObservableCollection<ComboboxItem>>(dataBLModel);
                    LstBacLuongKeHoach = _mapper.Map<ObservableCollection<ComboboxItem>>(dataBLModel);
                    LstBacLuongTran = _mapper.Map<ObservableCollection<ComboboxItem>>(dataBLModel);
                }

                MaCapBacItems = _mapper.Map<ObservableCollection<ComboboxItem>>(dataMaCapBacItems);
                MaCapBacKHItems = _mapper.Map<ObservableCollection<ComboboxItem>>(dataMaCapBacItems);
                tlDmCapBacKeHoachModel.ListCapBac = MaCapBacItems.ToList();
                tlDmCapBacKeHoachModel.ListCapBacKeHoach = MaCapBacKHItems.ToList();
                tlDmCapBacKeHoachModel.ListLoaiNhom = ItemsNhom;
                tlDmCapBacKeHoachModel.LstBacLuongHienTai = LstBacLuongHienTai.ToList();
                tlDmCapBacKeHoachModel.LstBacLuongKeHoach = LstBacLuongKeHoach.ToList();
                tlDmCapBacKeHoachModel.LstBacLuongTran = LstBacLuongTran.ToList();
            }
            if (args.PropertyName == nameof(tlDmCapBacKeHoachModel.MaCb))
            {
                if (tlDmCapBacKeHoachModel.MaCb != null)
                {
                    tlDmCapBacKeHoachModel.MaCb = MaCapBacItems.FirstOrDefault(x => x.ValueItem == tlDmCapBacKeHoachModel.MaCb).ValueItem;
                    tlDmCapBacKeHoachModel.TenCb = MaCapBacItems.FirstOrDefault(x => x.ValueItem == tlDmCapBacKeHoachModel.MaCb).DisplayItem;
                    tlDmCapBacKeHoachModel.MoTa = MaCapBacItems.FirstOrDefault(x => x.ValueItem == tlDmCapBacKeHoachModel.MaCb).DisplayItem;
                    TlDmCapBacNq104Model dmcapBac = dataModel.FirstOrDefault(x => x.MaCb == tlDmCapBacKeHoachModel.MaCb);
                }
            }
            if (args.PropertyName == nameof(tlDmCapBacKeHoachModel.MaCbKeHoach))
            {
                if (tlDmCapBacKeHoachModel.MaCbKeHoach != null)
                {
                    tlDmCapBacKeHoachModel.MaCbKeHoach = MaCapBacKHItems.FirstOrDefault(x => x.ValueItem == tlDmCapBacKeHoachModel.MaCbKeHoach).ValueItem;
                    tlDmCapBacKeHoachModel.TenCbKeHoach = MaCapBacKHItems.FirstOrDefault(x => x.ValueItem == tlDmCapBacKeHoachModel.MaCbKeHoach).DisplayItem;
                    tlDmCapBacKeHoachModel.MoTaKeHoach = MaCapBacKHItems.FirstOrDefault(x => x.ValueItem == tlDmCapBacKeHoachModel.MaCbKeHoach).DisplayItem;
                }
            }
            if (args.PropertyName == nameof(tlDmCapBacKeHoachModel.LoaiNhom))
            {
                if (!string.IsNullOrEmpty(tlDmCapBacKeHoachModel.LoaiNhom))
                {
                    var arrLoaiNhom = tlDmCapBacKeHoachModel.LoaiNhom.Split("-");
                    tlDmCapBacKeHoachModel.Loai = arrLoaiNhom[0];
                    tlDmCapBacKeHoachModel.Nhom = arrLoaiNhom[1];

                    LstBacLuongHienTai = _mapper.Map<ObservableCollection<ComboboxItem>>(dataBLModel);
                    LstBacLuongKeHoach = _mapper.Map<ObservableCollection<ComboboxItem>>(dataBLModel);
                    LstBacLuongTran = _mapper.Map<ObservableCollection<ComboboxItem>>(dataBLModel);
                    tlDmCapBacKeHoachModel.LstBacLuongHienTai = LstBacLuongHienTai.ToList();
                    tlDmCapBacKeHoachModel.LstBacLuongKeHoach = LstBacLuongKeHoach.ToList();
                    tlDmCapBacKeHoachModel.LstBacLuongTran = LstBacLuongTran.ToList();
                }
            }

            tlDmCapBacKeHoachModel.IsModified = true;
            OnPropertyChanged(nameof(SelectedCapBacKeHoach));
            OnPropertyChanged(nameof(TlDmCapBacKeHoachItems));
        }

        protected override void OnItemsChanged()
        {

        }

        protected override void OnRefresh()
        {
            LoadData();
        }
    }
}
