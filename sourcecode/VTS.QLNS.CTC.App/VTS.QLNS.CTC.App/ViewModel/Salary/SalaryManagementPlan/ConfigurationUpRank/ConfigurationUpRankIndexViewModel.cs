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

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.ConfigurationUpRank
{
    public class ConfigurationUpRankIndexViewModel : GridViewModelBase<TlDmCapBacKeHoachModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmCapBacService _tlDmCapBacService;
        private readonly ITlDmCapBacKeHoachService _tlDmCapBacKeHoachService;
        private readonly ITlDmHslKeHoachService _tlDmHslKeHoachService;

        public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH_CAU_HINH_THOI_HAN_TANG_QUAN_HAM_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Title => "Cấu hình thời hạn tăng quân hàm";
        public override string Name => "Cấu hình thời hạn tăng quân hàm";
        public override string Description => "Tổng số bản ghi: " + TlDmCapBacKeHoachItems.Count();
        public override PackIconKind IconKind => PackIconKind.ChevronTripleUp;
        public override Type ContentType => typeof(View.Salary.SalaryManagementPlan.ConfigurationUpRank.ConfigurationUpRank);

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

        private ObservableCollection<ComboboxItem> _lstHslHienTai;
        public ObservableCollection<ComboboxItem> LstHslHienTai
        {
            get => _lstHslHienTai;
            set => SetProperty(ref _lstHslHienTai, value);
        }

        private ObservableCollection<ComboboxItem> _lstHslTran;
        public ObservableCollection<ComboboxItem> LstHslTran
        {
            get => _lstHslTran;
            set => SetProperty(ref _lstHslTran, value);
        }

        private ObservableCollection<TlDmCapBacKeHoachModel> _tlDmCapBacKeHoachItems;
        public ObservableCollection<TlDmCapBacKeHoachModel> TlDmCapBacKeHoachItems
        {
            get => _tlDmCapBacKeHoachItems;
            set
            {
                SetProperty(ref _tlDmCapBacKeHoachItems, value);
                OnPropertyChanged(nameof(Description));
            }
        }

        private TlDmCapBacKeHoachModel _selectedCapBacKeHoach;
        public TlDmCapBacKeHoachModel SelectedCapBacKeHoach
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

        public ConfigurationUpRankIndexViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmCapBacService tlDmCapBacService,
            ITlDmCapBacKeHoachService tlDmCapBacKeHoachService,
            ITlDmHslKeHoachService tlDmHslKeHoachService)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDmCapBacKeHoachService = tlDmCapBacKeHoachService;
            _tlDmHslKeHoachService = tlDmHslKeHoachService;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(10);
            LoadData();
            LoadNgachCapBac();
            LoadMaCapBac();
            LoadListNhom();
        }

        private void LoadNgachCapBac()
        {
            try
            {
                var data = _tlDmCapBacService.FindParent().OrderBy(x => x.MaCb);
                var dataModel = _mapper.Map<ObservableCollection<TlDmCapBacModel>>(data);
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
                var dataModel = _mapper.Map<ObservableCollection<TlDmCapBacModel>>(data);
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
                TlDmCapBacKeHoachItems = _mapper.Map<ObservableCollection<TlDmCapBacKeHoachModel>>(data);
                var dataCapBac = _tlDmCapBacService.FindAll();
                var dataCapBacModel = _mapper.Map<ObservableCollection<TlDmCapBacModel>>(dataCapBac).ToList();
                var dataHsl = _tlDmHslKeHoachService.FindAll();
                var dataHslModel = _mapper.Map<ObservableCollection<TlDmHslKeHoachModel>>(dataHsl);
                foreach (var item in TlDmCapBacKeHoachItems)
                {
                    //var lstData = dataCapBacModel.Where(x => x.Parent == item.Parent).OrderBy(x => x.MaCb);
                    var lstData = GetListChildCapBac(dataCapBacModel, item.Parent).OrderBy(x => x.MaCb);
                    item.ListCapBac = _mapper.Map<ObservableCollection<ComboboxItem>>(lstData).ToList();
                    item.ListCapBacKeHoach = _mapper.Map<ObservableCollection<ComboboxItem>>(lstData).ToList();
                    var lstHslData = dataHslModel.Where(x => x.Ngach == item.Parent).OrderBy(x => x.MaCb);
                    item.LstHslKeHoach = _mapper.Map<ObservableCollection<ComboboxItem>>(lstHslData).ToList();
                    item.LstHslHienTai = _mapper.Map<ObservableCollection<ComboboxItem>>(lstHslData).ToList();
                    item.LstHslTran = _mapper.Map<ObservableCollection<ComboboxItem>>(lstHslData).ToList();
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

        private List<TlDmCapBacModel> GetListChildCapBac(List<TlDmCapBacModel> data, string maCb)
        {
            var listCapBac = new List<TlDmCapBacModel>();
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

        private void LoadListNhom()
        {
            ItemsNhom = new List<ComboboxItem>();
            _itemsNhom.Add(new ComboboxItem(NhomQncn.SOCAPN1, NhomQncn.SOCAPN1));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.SOCAPN2, NhomQncn.SOCAPN2));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.TRUNGCAPN1, NhomQncn.TRUNGCAPN1));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.TRUNGCAPN2, NhomQncn.TRUNGCAPN2));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.CAOCAPN1, NhomQncn.CAOCAPN1));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.CAOCAPN2, NhomQncn.CAOCAPN2));
        }

        protected override void OnAdd()
        {
            if (TlDmCapBacKeHoachItems.Count == 0 || SelectedCapBacKeHoach == null)
            {
                TlDmCapBacKeHoachModel tlDmCapBacKeHoachModel = new TlDmCapBacKeHoachModel();
                tlDmCapBacKeHoachModel.PropertyChanged += Detail_PropertyChanged;
                TlDmCapBacKeHoachItems.Add(tlDmCapBacKeHoachModel);
            }
            else
            {
                TlDmCapBacKeHoachModel item = SelectedCapBacKeHoach;
                TlDmCapBacKeHoachModel target = ObjectCopier.Clone(item);

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
            List<TlDmCapBacKeHoachModel> listAdd = TlDmCapBacKeHoachItems.Where(x => x.IsModified && !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null)).ToList();
            List<TlDmCapBacKeHoachModel> listEdit = TlDmCapBacKeHoachItems.Where(x => x.IsModified && !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            List<TlDmCapBacKeHoachModel> listDelete = TlDmCapBacKeHoachItems.Where(x => x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();

            if (listAdd != null && listAdd.Count > 0)
            {
                var lstTlDmCapBacKeHoach = _mapper.Map<List<TlDmCapBacKeHoach>>(listAdd);
                _tlDmCapBacKeHoachService.AddRang(lstTlDmCapBacKeHoach);
            }
            if (listEdit != null && listEdit.Count > 0)
            {
                foreach (var item in listEdit)
                {
                    var lstTlDmCapBacKeHoach = _mapper.Map<TlDmCapBacKeHoach>(item);
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
            TlDmCapBacKeHoachModel tlDmCapBacKeHoachModel = (TlDmCapBacKeHoachModel)sender;
            var data = _tlDmCapBacService.FindAll();
            var dataModel = _mapper.Map<ObservableCollection<TlDmCapBacModel>>(data);
            var dataHsl = _mapper.Map<ObservableCollection<TlDmHslKeHoachModel>>(_tlDmHslKeHoachService.FindAll());
            if (args.PropertyName == nameof(tlDmCapBacKeHoachModel.Parent))
            {

                tlDmCapBacKeHoachModel.Parent = NgachCapBacItems.FirstOrDefault(x => x.DisplayItem == tlDmCapBacKeHoachModel.Parent).DisplayItem;
                var dataMaCapBacItems = dataModel.Where(x => x.Parent == tlDmCapBacKeHoachModel.Parent).OrderBy(x => x.MaCb).ToList();
                var dataHslItem = dataHsl.Where(x => x.Ngach == tlDmCapBacKeHoachModel.Parent).OrderBy(x => x.MaCb).ToList();
                MaCapBacItems = _mapper.Map<ObservableCollection<ComboboxItem>>(dataMaCapBacItems);
                MaCapBacKHItems = _mapper.Map<ObservableCollection<ComboboxItem>>(dataMaCapBacItems);
                LstHslKeHoach = _mapper.Map<ObservableCollection<ComboboxItem>>(dataHslItem);
                LstHslHienTai = _mapper.Map<ObservableCollection<ComboboxItem>>(dataHslItem);
                LstHslTran = _mapper.Map<ObservableCollection<ComboboxItem>>(dataHslItem);
                tlDmCapBacKeHoachModel.ListCapBac = MaCapBacItems.ToList();
                tlDmCapBacKeHoachModel.ListCapBacKeHoach = MaCapBacKHItems.ToList();
                tlDmCapBacKeHoachModel.LstHslKeHoach = LstHslKeHoach.ToList();
                tlDmCapBacKeHoachModel.LstHslHienTai = LstHslHienTai.ToList();
                tlDmCapBacKeHoachModel.LstHslTran = LstHslHienTai.ToList();
            }
            if (args.PropertyName == nameof(tlDmCapBacKeHoachModel.IdHslKeHoach) && tlDmCapBacKeHoachModel.IdHslKeHoach != null)
            {
                var hslKeHoachCbbModel = tlDmCapBacKeHoachModel.LstHslKeHoach.FirstOrDefault(x => x.ValueItem.Equals(tlDmCapBacKeHoachModel.IdHslKeHoach.ToString()));
                tlDmCapBacKeHoachModel.HsLuongKeHoach = decimal.Parse(hslKeHoachCbbModel.HiddenValue);
                var hslKeHoachModel = dataHsl.FirstOrDefault(x => hslKeHoachCbbModel.ValueItem.Equals(x.Id.ToString()));
                if (hslKeHoachModel != null)
                {
                    tlDmCapBacKeHoachModel.MaCbKeHoach = hslKeHoachModel.MaCb;
                    tlDmCapBacKeHoachModel.MoTaKeHoach = hslKeHoachModel.MoTa;
                }
            }
            if (args.PropertyName == nameof(tlDmCapBacKeHoachModel.IdHslTran))
            {
                if (tlDmCapBacKeHoachModel.IdHslTran != null)
                {
                    var hslKeHoachCbbModel = tlDmCapBacKeHoachModel.LstHslTran.FirstOrDefault(x => x.ValueItem.Equals(tlDmCapBacKeHoachModel.IdHslTran.ToString()));
                    tlDmCapBacKeHoachModel.HsLuongTran = decimal.Parse(hslKeHoachCbbModel.HiddenValue);
                    var hslKeHoachModel = dataHsl.FirstOrDefault(x => hslKeHoachCbbModel.ValueItem.Equals(x.Id.ToString()));
                    if (hslKeHoachModel != null)
                    {
                        tlDmCapBacKeHoachModel.MaCbTran = hslKeHoachModel.MaCb;
                        tlDmCapBacKeHoachModel.MoTaLuongTran = hslKeHoachModel.MoTa;
                    }
                }
                else
                {
                    tlDmCapBacKeHoachModel.IdHslTran = null;
                    tlDmCapBacKeHoachModel.HsLuongTran = null;
                    tlDmCapBacKeHoachModel.MaCbTran = null;
                    tlDmCapBacKeHoachModel.MoTaLuongTran = null;
                }
            }
            if (args.PropertyName == nameof(tlDmCapBacKeHoachModel.IdHslHienTai) && tlDmCapBacKeHoachModel.IdHslHienTai != null)
            {
                var hslKeHoachCbbModel = tlDmCapBacKeHoachModel.LstHslHienTai.FirstOrDefault(x => x.ValueItem.Equals(tlDmCapBacKeHoachModel.IdHslHienTai.ToString()));
                tlDmCapBacKeHoachModel.LhtHs = decimal.Parse(hslKeHoachCbbModel.HiddenValue);
            }
            if (args.PropertyName == nameof(tlDmCapBacKeHoachModel.MaCb))
            {
                if (tlDmCapBacKeHoachModel.MaCb != null)
                {
                    tlDmCapBacKeHoachModel.MaCb = MaCapBacItems.FirstOrDefault(x => x.DisplayItem == tlDmCapBacKeHoachModel.MaCb).DisplayItem;
                    tlDmCapBacKeHoachModel.TenCb = MaCapBacItems.FirstOrDefault(x => x.DisplayItem == tlDmCapBacKeHoachModel.MaCb).ValueItem;
                    tlDmCapBacKeHoachModel.MoTa = MaCapBacItems.FirstOrDefault(x => x.DisplayItem == tlDmCapBacKeHoachModel.MaCb).HiddenValue;
                    TlDmCapBacModel dmcapBac = dataModel.FirstOrDefault(x => x.MaCb == tlDmCapBacKeHoachModel.MaCb);
                    tlDmCapBacKeHoachModel.BhxhCq = dmcapBac.BhxhCq;
                    tlDmCapBacKeHoachModel.BhxhCn = dmcapBac.HsBhxh;
                    tlDmCapBacKeHoachModel.BhytCq = dmcapBac.BhytCq;
                    tlDmCapBacKeHoachModel.BhytCn = dmcapBac.HsBhyt;
                    tlDmCapBacKeHoachModel.BhtnCq = dmcapBac.BhtnCq;
                    tlDmCapBacKeHoachModel.BhtnCn = dmcapBac.HsBhtn;
                    tlDmCapBacKeHoachModel.KpcdCn = dmcapBac.KpcdCq;
                    tlDmCapBacKeHoachModel.KpcdCn = dmcapBac.HsKpcd;
                }
            }
            if (args.PropertyName == nameof(tlDmCapBacKeHoachModel.MaCbKeHoach))
            {
                if (tlDmCapBacKeHoachModel.MaCbKeHoach != null)
                {
                    tlDmCapBacKeHoachModel.MaCbKeHoach = MaCapBacKHItems.FirstOrDefault(x => x.DisplayItem == tlDmCapBacKeHoachModel.MaCbKeHoach).DisplayItem;
                    tlDmCapBacKeHoachModel.TenCbKeHoach = MaCapBacKHItems.FirstOrDefault(x => x.DisplayItem == tlDmCapBacKeHoachModel.MaCbKeHoach).ValueItem;
                    tlDmCapBacKeHoachModel.MoTaKeHoach = MaCapBacKHItems.FirstOrDefault(x => x.DisplayItem == tlDmCapBacKeHoachModel.MaCbKeHoach).HiddenValue;
                }
            }

            if (args.PropertyName == nameof(tlDmCapBacKeHoachModel.IsNhomEnabled))
            {
                if (tlDmCapBacKeHoachModel.IsNhomEnabled == false)
                {
                    tlDmCapBacKeHoachModel.Nhom = null;
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
