using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.AllocationDetailViewModel;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Cadres.CadresBHXH
{
    public class CadresBHXHViewModel : DialogViewModelBase<TlCanBoCheDoBHXHChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ITlDmCanBoService _tlDmCanBoService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private IExportService _exportService;
        private List<TlDmCanBo> _lstDmCanBos;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;
        private readonly ITlCanBoCheDoBHXHChiTietService _canBoCheDoBHXHChiTietService;
        private readonly ITlDmNgayNghiService _tTlDmNgayNghiService;

        public override Type ContentType => typeof(View.Salary.Cadres.CadresBHXH.CadresBHXH);
        public override PackIconKind IconKind => PackIconKind.AccountDetails;
        public override string Name => "Chi tiết chế độ BHXH";

        public List<DateTime?> LstHoliday { get; set; }
        public string SMaCanBo { get; set; }
        public int IThang { get; set; }
        public int INam { get; set; }
        public event DataChangedEventHandler UpdateParentWindowEventHandler;

        public TlCanBoCheDoBHXHModel TlCanBoCheDoBHXHModel { get; set; }

        private ObservableCollection<TlCanBoCheDoBHXHChiTietModel> _dataCanBoCheDoBHXHChiTiet;
        public ObservableCollection<TlCanBoCheDoBHXHChiTietModel> DataCanBoCheDoBHXHChiTiet
        {
            get => _dataCanBoCheDoBHXHChiTiet;
            set => SetProperty(ref _dataCanBoCheDoBHXHChiTiet, value);
        }

        private TlCanBoCheDoBHXHChiTietModel _selectedCanBoCheDoBHXHChiTiet;
        public TlCanBoCheDoBHXHChiTietModel SelectedCanBoCheDoBHXHChiTiet
        {
            get => _selectedCanBoCheDoBHXHChiTiet;
            set
            {
                SetProperty(ref _selectedCanBoCheDoBHXHChiTiet, value);
            }
        }

        public RelayCommand AddCommand { get; }
        public RelayCommand SaveDetailDataCommand { get; }
        public RelayCommand DeleteCheDoBHXHCommand { get; }

        public CadresBHXHViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmCanBoService tlDmCanBoService,
            IExportService exportService,
            IDanhMucService danhMucService,
            INsDonViService donViService,
            ITlDmDonViService tlDmDonViService,
            ITlDmNgayNghiService tTlDmNgayNghiService,
            ITlCanBoCheDoBHXHChiTietService tTlCanBoCheDoBHXHChiTietService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _tlDmCanBoService = tlDmCanBoService;
            _exportService = exportService;
            _tlDmDonViService = tlDmDonViService;
            _danhMucService = danhMucService;
            _donViService = donViService;
            _canBoCheDoBHXHChiTietService = tTlCanBoCheDoBHXHChiTietService;
            _tTlDmNgayNghiService = tTlDmNgayNghiService;

            AddCommand = new RelayCommand(obj => OnAdd(obj));
            SaveDetailDataCommand = new RelayCommand(obj => OnSaveData());
            DeleteCheDoBHXHCommand = new RelayCommand(obj => OnDeleteCheDoBHXH());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
            GetHolidays();
        }

        private void LoadData()
        {
            var maCheDo = TlCanBoCheDoBHXHModel.SMaCheDo;
            var listData = _canBoCheDoBHXHChiTietService.GetCanBoCheDoChiTietIndex(SMaCanBo, maCheDo, IThang, INam).ToList();
            DataCanBoCheDoBHXHChiTiet = _mapper.Map<ObservableCollection<TlCanBoCheDoBHXHChiTietModel>>(listData);

            foreach (var item in DataCanBoCheDoBHXHChiTiet)
            {
                item.PropertyChanged += DetailCanBoCheDoBHXHModel_PropertyChanged;
            }
        }

        private void OnSaveData()
        {
            if (DataCanBoCheDoBHXHChiTiet != null)
            {
                List<TlCanBoCheDoBHXHChiTietModel> listCheDoAdd = DataCanBoCheDoBHXHChiTiet.Where(x => x.IsModified && x.IsAdded).ToList();
                List<TlCanBoCheDoBHXHChiTietModel> listCheDoEdit = DataCanBoCheDoBHXHChiTiet.Where(x => x.IsModified && !x.IsAdded).ToList();
                List<TlCanBoCheDoBHXHChiTietModel> listCheDoDelete = DataCanBoCheDoBHXHChiTiet.Where(x => x.IsDeleted && x.Id != null && x.Id != Guid.Empty).ToList();

                if (listCheDoAdd.Count > 0)
                {
                    foreach (var item in listCheDoAdd)
                    {
                        item.Id = Guid.NewGuid();
                        item.SMaCanBo = SMaCanBo;
                        item.IThang = IThang;
                        item.INam = INam;
                    }
                    var lstEntities = _mapper.Map<List<TlCanBoCheDoBHXHChiTiet>>(listCheDoAdd);
                    _canBoCheDoBHXHChiTietService.AddRangeCBCDChiTiet(lstEntities);
                }

                if (listCheDoEdit.Count > 0)
                {
                    foreach (var item in listCheDoEdit)
                    {
                        var cheDoBHXH = _canBoCheDoBHXHChiTietService.FindCBCDChiTiet(item.Id);
                        if (cheDoBHXH != null)
                        {
                            _mapper.Map(item, cheDoBHXH);
                            _canBoCheDoBHXHChiTietService.UpdateCBCDChiTiet(cheDoBHXH);
                        }
                    }
                }

                if (listCheDoDelete.Count > 0)
                {
                    foreach (var item in listCheDoDelete)
                    {
                        _canBoCheDoBHXHChiTietService.DeleteCBCDChiTiet(item.Id);
                    }
                }

                UpdateParentWindowEventHandler?.Invoke(TlCanBoCheDoBHXHModel, new EventArgs());
                var message = Resources.MsgSaveDone;
                MessageBoxHelper.Info(message);
                DialogHost.Close("CadresDetail");
            }
        }

        private void OnAdd(object obj)
        {
            try
            {
                TlCanBoCheDoBHXHChiTietModel targetItem = new TlCanBoCheDoBHXHChiTietModel();
                int currentRow = -1;
                if (DataCanBoCheDoBHXHChiTiet != null && DataCanBoCheDoBHXHChiTiet.Count > 0)
                {
                    currentRow = 0;
                    if (SelectedCanBoCheDoBHXHChiTiet != null)
                    {
                        currentRow = DataCanBoCheDoBHXHChiTiet.IndexOf(SelectedCanBoCheDoBHXHChiTiet);
                    }
                    else
                    {
                        currentRow = DataCanBoCheDoBHXHChiTiet.IndexOf(DataCanBoCheDoBHXHChiTiet.Last());
                    }

                    TlCanBoCheDoBHXHChiTietModel sourceItem = DataCanBoCheDoBHXHChiTiet.ElementAt(currentRow);
                    targetItem = ObjectCopier.Clone(sourceItem);
                    targetItem.IsDeleted = false;
                }
                targetItem.PropertyChanged += DetailCanBoCheDoBHXHModel_PropertyChanged;
                targetItem.SMaCheDo = TlCanBoCheDoBHXHModel.SMaCheDo;
                targetItem.STenCheDo = TlCanBoCheDoBHXHModel.STenCheDo;
                targetItem.DTuNgay = null;
                targetItem.DDenNgay = null;
                targetItem.FSoNgayHuongBHXH = null;
                DataCanBoCheDoBHXHChiTiet.Insert(currentRow + 1, targetItem);
                OnPropertyChanged(nameof(DataCanBoCheDoBHXHChiTiet));
                targetItem.IsAdded = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailCanBoCheDoBHXHModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlCanBoCheDoBHXHChiTietModel objectSender = (TlCanBoCheDoBHXHChiTietModel)sender;
            if (args.PropertyName == nameof(TlCanBoCheDoBHXHChiTietModel.DTuNgay)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHChiTietModel.DDenNgay)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHChiTietModel.FSoNgayHuongBHXH))
            {
                List<TlCanBoCheDoBHXHChiTietModel> listCheDoBHXH = DataCanBoCheDoBHXHChiTiet.Where(x => !x.IsDeleted).ToList();
                OnPropertyChanged(nameof(DataCanBoCheDoBHXHChiTiet));
                OnPropertyChanged(nameof(Model));
                if (objectSender.DTuNgay != null && objectSender.DDenNgay != null)
                {
                    objectSender.FSoNgayHuongBHXH = CountDayOfBHXH(objectSender);
                }
            }
            objectSender.IsModified = true;
        }

        private double CountDayOfBHXH(TlCanBoCheDoBHXHChiTietModel item)
        {
            double soNgayHuong;
            int sunDays = 0;
            var period = (item.DDenNgay.GetValueOrDefault() - item.DTuNgay.GetValueOrDefault()).Days + 1;
            for (int i = 0; i < period; i++)
            {
                DateTime currDay = item.DTuNgay.GetValueOrDefault().AddDays(i);
                if (currDay.DayOfWeek == DayOfWeek.Sunday)
                {
                    sunDays++;
                }
            }
            var holidays = CountHolidays(item.DTuNgay, item.DDenNgay, LstHoliday);
            soNgayHuong = period - sunDays - holidays;
            return soNgayHuong;
        }

        private int CountHolidays(DateTime? startDate, DateTime? endDate, List<DateTime?> lstHoliday)
        {
            int holidays = 0;
            DateTime? currDate = startDate.GetValueOrDefault().Date;

            while (currDate <= endDate.GetValueOrDefault().Date)
            {
                if (lstHoliday.Contains(currDate))
                {
                    holidays++;
                }
                currDate = currDate.GetValueOrDefault().AddDays(1);
            }

            return holidays;
        }

        private void GetHolidays()
        {
            LstHoliday = new List<DateTime?>();
            var holidays = _tTlDmNgayNghiService.FindAll();
            if (holidays != null)
            {
                foreach (var typeH in holidays)
                {
                    DateTime? currDate = typeH.DTuNgay.GetValueOrDefault().Date;
                    while (currDate <= typeH.DDenNgay.GetValueOrDefault().Date)
                    {
                        LstHoliday.Add(currDate);
                        currDate = currDate.GetValueOrDefault().AddDays(1);
                    }
                }
            }
        }

        protected void OnDeleteCheDoBHXH()
        {
            if (DataCanBoCheDoBHXHChiTiet != null && DataCanBoCheDoBHXHChiTiet.Count > 0 && SelectedCanBoCheDoBHXHChiTiet != null)
            {
                SelectedCanBoCheDoBHXHChiTiet.IsDeleted = !SelectedCanBoCheDoBHXHChiTiet.IsDeleted;
                OnPropertyChanged(nameof(DataCanBoCheDoBHXHChiTiet));
            }
        }
    }
}
