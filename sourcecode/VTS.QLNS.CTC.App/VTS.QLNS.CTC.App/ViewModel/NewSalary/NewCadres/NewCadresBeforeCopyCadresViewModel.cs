using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewCadres
{
    public class NewCadresBeforeCopyCadresViewModel : DialogViewModelBase<CadresNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlDmCanBoNq104Service _cadresNq104Service;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private readonly ITlDmPhuCapNq104Service _iTlDmPhuCapService;
        private readonly ISysAuditLogService _sysAuditLogService;

        private ICollectionView _dataCopyCarderView;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_CADRES_COPY;
        public override string Title => "Sao chép cán bộ sang lương mới";
        public override string Description => "Sao chép cán bộ sang lương mới theo tháng";
        public override Type ContentType => typeof(View.NewSalary.NewCadres.NewCadredBeforeCopyCadres);

        private ObservableCollection<TlDmDonViNq104Model> _itemsDonVi;
        public ObservableCollection<TlDmDonViNq104Model> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private TlDmDonViNq104Model _selectedDonVi;
        public TlDmDonViNq104Model SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                if (SetProperty(ref _selectedDonVi, value) && _dataCopyCarderView != null)
                {
                    LoadData();
                }
            }
        }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> ItemsMonth
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _selectedFromMonth;
        public ComboboxItem SelectedFromMonth
        {
            get => _selectedFromMonth;
            set
            {
                if (SetProperty(ref _selectedFromMonth, value) && _dataCopyCarderView != null)
                {
                    LoadData();
                }
            }
        }

        private ComboboxItem _selectedToMonth;
        public ComboboxItem SelectedToMonth
        {
            get => _selectedToMonth;
            set => SetProperty(ref _selectedToMonth, value);
        }



        private List<ComboboxItem> _years;
        public List<ComboboxItem> ItemsYear
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _selectedFromYear;
        public ComboboxItem SelectedFromYear
        {
            get => _selectedFromYear;
            set
            {
                if (SetProperty(ref _selectedFromYear, value) && _dataCopyCarderView != null && _selectedFromYear != null)
                {
                    LoadData();
                }
            }
        }

        private ComboboxItem _selectedToYear;
        public ComboboxItem SelectedToYear
        {
            get => _selectedToYear;
            set => SetProperty(ref _selectedToYear, value);
        }

        private ObservableCollection<CadresModel> _itemsCadres;
        public ObservableCollection<CadresModel> ItemsCadres
        {
            get => _itemsCadres;
            set => SetProperty(ref _itemsCadres, value);
        }

        private CadresModel _selectedCarder;
        public CadresModel SelectedCarder
        {
            get => _selectedCarder;
            set => SetProperty(ref _selectedCarder, value);
        }

        private bool _selectedAllCadres;
        public bool SelectedAllCadres
        {
            get => ItemsCadres.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectedAllCadres, value);
                foreach (var item in ItemsCadres) item.IsSelected = _selectedAllCadres;
            }
        }

        public string LabelSelectedCountCadres
        {
            get
            {
                var totalCount = ItemsCadres.Count;
                var totalSelected = ItemsCadres.Count(x => x.IsSelected);
                return $"ĐỐI TƯỢNG ({totalSelected}/{totalCount})";
            }
        }

        private FormViewState _viewState;
        public FormViewState ViewState
        {
            get => _viewState;
            set
            {
                SetProperty(ref _viewState, value);
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        public string ComboboxDisplayMemberPathDonVi => nameof(SelectedDonVi.TenDonVi);
        public bool IsReadOnly => ViewState == FormViewState.ADD;

        public NewCadresBeforeCopyCadresViewModel(
            IMapper mapper,
            ILog logger,
            ISessionService sessionService,
            ITlDmCanBoService cadresService,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlDmPhuCapNq104Service iTlDmPhuCapService,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapService,
            ITlDmCanBoNq104Service tlDmCanBoNq104Service,
            ISysAuditLogService sysAuditLogService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _iTlDmPhuCapService = iTlDmPhuCapService;
            _cadresService = cadresService;
            _tlDmDonViService = tlDmDonViService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _cadresNq104Service = tlDmCanBoNq104Service;
            _sysAuditLogService = sysAuditLogService;
        }

        public override void Init()
        {
            base.Init();
            LoadMonths();
            LoadYear();
            LoadDonVi();
            LoadData();
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (var i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            OnPropertyChanged(nameof(ItemsMonth));
            SelectedFromMonth = _months.FirstOrDefault(x => x.ValueItem == Model.Thang.ToString());
        }

        private void LoadDonVi()
        {
            TlDmDonViNq104Model donViDefault = new TlDmDonViNq104Model();
            donViDefault.Id = Guid.Empty;
            donViDefault.TenDonVi = "-- Tất cả --";

            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            _itemsDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
            _itemsDonVi.Insert(0, donViDefault);
            SelectedDonVi = donViDefault;
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadData()
        {
            try
            {
                var predicate = PredicateBuilder.True<TlDmCanBo>();
                predicate = predicate.And(x => x.Nam == int.Parse(_selectedFromYear.ValueItem));
                predicate = predicate.And(x => x.Thang == int.Parse(_selectedFromMonth.ValueItem));
                //predicate = predicate.And(x => x.IsDelete == true);
                if (!SelectedDonVi.Id.Equals(Guid.Empty))
                {
                    predicate = predicate.And(x => x.Parent == SelectedDonVi.MaDonVi);
                }
                var data = _cadresService.FindByCondition(predicate).OrderBy(x => x.TenCanBo.Split(" ").Last());
                ItemsCadres = _mapper.Map<ObservableCollection<CadresModel>>(data);
                _dataCopyCarderView = CollectionViewSource.GetDefaultView(ItemsCadres);
                _dataCopyCarderView.Filter = CadresFilter;
                SelectedAllCadres = true;
                foreach (var org in ItemsCadres)
                {
                    org.PropertyChanged += (sender, args) =>
                    {
                        OnPropertyChanged(nameof(LabelSelectedCountCadres));
                        OnPropertyChanged(nameof(SelectedAllCadres));
                    };
                }
                OnPropertyChanged(nameof(LabelSelectedCountCadres));
                OnPropertyChanged(nameof(SelectedAllCadres));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool CadresFilter(object obj)
        {
            bool result = true;
            var item = (CadresModel)obj;
            if (SelectedFromMonth != null)
            {
                result = result && item.Thang == int.Parse(SelectedFromMonth.ValueItem);
            }
            if (!SelectedDonVi.Id.Equals(Guid.Empty))
            {
                result = result && item.Parent == SelectedDonVi.MaDonVi;
            }
            return result;
        }

        private void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                var year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            OnPropertyChanged(nameof(ItemsYear));
            SelectedFromYear = _years.FirstOrDefault(x => x.ValueItem == Model.Nam.ToString());
            SelectedToYear = _years.FirstOrDefault(x => x.ValueItem == Model.Nam.ToString());
        }

        public override void OnSave()
        {
            if (!Validate()) return;

            MessageBoxResult dialog = MessageBoxHelper.Confirm("Đ/c có muốn chuyển các cán bộ lương cũ sang lương mới không?");
            bool isCopyValue = dialog == MessageBoxResult.Yes;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                int i = 0;
                int fromYear = int.Parse(SelectedFromYear.ValueItem);
                int fromMonth = int.Parse(SelectedFromMonth.ValueItem);

                // Sao chép cán bộ
                var lstCanBoEntities = new List<TlDmCanBoNq104>();
                var lstCanBoSelect = ItemsCadres.Where(x => x.IsSelected).ToList();

                var predicate = PredicateBuilder.True<TlDmCanBo>();
                predicate = predicate.And(x => x.Nam == int.Parse(_selectedFromYear.ValueItem));
                predicate = predicate.And(x => x.Thang == int.Parse(_selectedFromMonth.ValueItem));
                //predicate = predicate.And(x => x.Parent == SelectedDonVi.MaDonVi);

                var lstCanBoExits = _cadresService.FindByCondition(predicate);
                ObservableCollection<CadresNq104Model> cadresModels = new ObservableCollection<CadresNq104Model>();
                var data = _cadresNq104Service.FindAllState();
                cadresModels = _mapper.Map<ObservableCollection<CadresNq104Model>>(data);

                foreach (var item in lstCanBoExits)
                {
                    TlDmCanBoNq104 copyItem = new TlDmCanBoNq104();
                    CadresModel cadresModel = new CadresModel();

                    int max = 0;
                    if (cadresModels != null && cadresModels.Count() > 0)
                    {
                        max = cadresModels.Max(x => int.Parse(x.MaHieuCanBo));
                    }

                    if (max == 0)
                    {
                        cadresModel.MaHieuCanBo = (1 + i).ToString();
                    }
                    else
                    {
                        int maHieu = max + (1 + i);
                        cadresModel.MaHieuCanBo = maHieu.ToString();
                    }

                    string month = Int32.Parse(SelectedToMonth.ValueItem) < 10 ? ("0" + SelectedToMonth.ValueItem) : SelectedToMonth.ValueItem;
                    string maCanBo = SelectedToYear.ValueItem + month + cadresModel.MaHieuCanBo;
                    cadresModel.MaCanBo = cadresModel.MaHieuCanBo;

                    copyItem.MaCanBo = maCanBo;
                    copyItem.MaHieuCanBo = cadresModel.MaHieuCanBo;
                    copyItem.MaTangGiam = item.MaTangGiam;
                    copyItem.TenCanBo = item.TenCanBo;
                    copyItem.TenDonVi = item.TenDonVi;
                    copyItem.SoNguoiPhuThuoc = item.SoNguoiPhuThuoc;
                    copyItem.ThangTnn = item.ThangTnn;
                    copyItem.NgayNn = item.NgayNn;
                    copyItem.NgayXn = item.NgayXn;
                    copyItem.NgayTn = item.NgayTn;
                    copyItem.NamTn = item.NamTn;
                    copyItem.TenKhoBac = item.TenKhoBac;
                    copyItem.SoTaiKhoan = item.SoTaiKhoan;
                    copyItem.Tm = item.Tm;
                    copyItem.KhongLuong = item.KhongLuong;
                    copyItem.bKhongTinhNTN = item.bKhongTinhNTN;
                    copyItem.Cccd = item.Cccd;
                    copyItem.DienThoai = item.DienThoai;
                    copyItem.NoiCongTac = item.NoiCongTac;
                    copyItem.MaSoVat = item.MaSoVat;
                    copyItem.ITrangThai = item.ITrangThai;
                    //copyItem.MaCanBo = toYear.ToString("D4") + toMonth.ToString("D2") + item.MaHieuCanBo;
                    copyItem.IsNangLuongCb = false;
                    copyItem.IsNangLuongCvd = false;
                    copyItem.Nam = Int32.Parse(SelectedToYear.ValueItem);
                    copyItem.Thang = Int32.Parse(SelectedToMonth.ValueItem);
                    copyItem.Parent = item.Parent;
                    copyItem.IsLock = false;
                    copyItem.BTinhBHXH = false;
                    copyItem.IsDelete = true;

                    i++;
                    lstCanBoEntities.Add(_mapper.Map<TlDmCanBoNq104>(copyItem));
                }

                // Sao chép cán bộ phụ cấp luong cu sang luong moi
                _cadresNq104Service.AddRange(lstCanBoEntities);
                _sysAuditLogService.WriteLog(Resources.ApplicationName, Title, (int)TypeExecute.Insert, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);

            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    // Thành công
                    MessageBoxHelper.Info("Sao chép thành công");
                    SavedAction?.Invoke(null);
                    DialogHost.Close("RootDialog");
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });

        }

        private bool Validate()
        {
            if (!ItemsCadres.Any(x => x.IsSelected))
            {
                MessageBoxHelper.Warning(Resources.MsgNullCadres);
                return false;
            }
            if (SelectedFromYear != null && SelectedFromYear != null)
            {

                int fromYear = int.Parse(SelectedFromYear.ValueItem);
                int fromMonth = int.Parse(SelectedFromMonth.ValueItem);

                //if (fromYear > toYear || (fromYear == toYear && fromMonth >= toMonth))
                //{
                //    MessageBoxHelper.Warning(Resources.MsgCopyCadres);
                //    return false;
                //}
                return true;
            }
            else
            {
                MessageBoxHelper.Warning(Resources.MsgCopyNull);
                return false;
            }
        }
    }
}
