using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Salary.ImportSalary;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Properties;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Cadres.IncomeTax
{
    public class IncomeTaxViewModel : GridViewModelBase<IncomeTaxModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly IExportService _exportService;
        private ICollectionView _dtCadresView;

        public override string FuncCode => NSFunctionCode.SALARY_CADRES_CAP_NHAT_LUONG_THUONG_THUE_TNCN_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Cập nhật thông tin lương thưởng, thuế TNCN";
        public override Type ContentType => typeof(View.Salary.Cadres.IncomeTax.IncomeTaxIndex);
        public override PackIconKind IconKind => PackIconKind.FormatListBulleted;
        public override string Title => "Thông tin lương thưởng, thuế TNCN";
        public override string Description => string.Format("Danh sách đối tượng (Tổng số bản ghi: {0})", TlInComeTaxItems == null ? 0 : TlInComeTaxItems.Count());
        public ImportSalaryTncnViewModel ImportSalaryTncnViewModel { get; }
        public CadresUpdateMultiAllowenceViewModel UpdateMultiAllowenceCadresViewModel { get; }

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
                if (SetProperty(ref _monthSelected, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
                }
            }
        }

        private ObservableCollection<TlDmDonViModel> _donViItems;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViModel _selectedDonViItems;
        public TlDmDonViModel SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                if (SetProperty(ref _selectedDonViItems, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
                }
            }
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _yearSelected;
        public ComboboxItem YearSelected
        {
            get => _yearSelected;
            set
            {
                if (SetProperty(ref _yearSelected, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
                }
            }
        }

        private ObservableCollection<IncomeTaxModel> _tlInComeTaxItems;
        public ObservableCollection<IncomeTaxModel> TlInComeTaxItems
        {
            get => _tlInComeTaxItems;
            set
            {
                SetProperty(ref _tlInComeTaxItems, value);
                OnPropertyChanged(nameof(Description));
            }
        }

        private IncomeTaxModel _selectedIncomeTax;
        public IncomeTaxModel SelectedIncomeTax
        {
            get => _selectedIncomeTax;
            set
            {
                SetProperty(ref _selectedIncomeTax, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private string _searchCanBo;
        public string SearchCanBo
        {
            get => _searchCanBo;
            set => SetProperty(ref _searchCanBo, value);
        }

        public string ComboboxDisplayMemberPathCapBac => nameof(SelectedDonViItems.MaTenDonVi);
        
        public bool IsEnabled => SelectedIncomeTax != null;
        public bool IsEnableExportData => TlInComeTaxItems != null && TlInComeTaxItems.Count > 0;

        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportThueTncnCommand { get; }
        public RelayCommand ImportThueTncnCommand { get; }
        public RelayCommand OpenUpdateMultiAllowenceCommand { get; }

        public IncomeTaxViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            INsDonViService nsDonViService,
            ITlDmCanBoService cadresService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            CadresUpdateMultiAllowenceViewModel updateMultiAllowenceCadresViewModel,
            IExportService exportService,
            ImportSalaryTncnViewModel importSalaryTncnViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _tlDmDonViService = tlDmDonViService;
            _cadresService = cadresService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _exportService = exportService;
            ImportSalaryTncnViewModel = importSalaryTncnViewModel;
            UpdateMultiAllowenceCadresViewModel = updateMultiAllowenceCadresViewModel;
            SearchCommand = new RelayCommand(o => _dtCadresView.Refresh());
            ExportThueTncnCommand = new RelayCommand(o => OnExportData());
            ImportThueTncnCommand = new RelayCommand(o => OnImportData());
            OpenUpdateMultiAllowenceCommand = new RelayCommand(obj => OnOpenUpdateMulti());
        }

        public override void Init()
        {
            base.Init();
            SearchCanBo = string.Empty;
            LoadDonVi();
            LoadMonths();
            LoadYear();
            LoadData();
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                var month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(Months));
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == thang.ToString());
        }

        private void LoadDonVi()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            var lstDonVi = new List<TlDmDonViModel>();

            TlDmDonViModel tlDmDonViModel = new TlDmDonViModel();
            tlDmDonViModel.TenDonVi = "-- Tất cả --";
            tlDmDonViModel.Id = Guid.Empty;

            lstDonVi.Add(tlDmDonViModel);
            lstDonVi.AddRange(_mapper.Map<ObservableCollection<TlDmDonViModel>>(data).ToList());

            SelectedDonViItems = tlDmDonViModel;

            _donViItems = new ObservableCollection<TlDmDonViModel>(lstDonVi);
        }

        private void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        private void LoadData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    var _listDonVi = _nsDonViService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == 1).ToList();
                    if (_listDonVi.Any(n => _sessionService.Current.IdsDonViQuanLy.Contains(n.IIDMaDonVi) && n.Loai == "0") || _sessionService.Current.Principal.Equals("admin")) {
                        var data = _cadresService.FindCanBoThueTncn();
                        e.Result = data;
                    } else
                    {
                        var data = _cadresService.FindCanBoThueTncn().Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.MaDonVi));
                        e.Result = data;
                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        TlInComeTaxItems = _mapper.Map<ObservableCollection<IncomeTaxModel>>(e.Result);
                        if (TlInComeTaxItems.Count > 0)
                        {
                            foreach (var item in TlInComeTaxItems)
                            {
                                item.PropertyChanged += Detail_PropertyChanged;
                            }
                        }
                        _dtCadresView = CollectionViewSource.GetDefaultView(TlInComeTaxItems);
                        _dtCadresView.Filter = IncomeTaxFilter;
                        OnPropertyChanged(nameof(IsEnableExportData));
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool IncomeTaxFilter(object obj)
        {
            bool result = true;
            var item = (IncomeTaxModel)obj;
            if (MonthSelected != null)
            {
                result &= item.Thang == int.Parse(MonthSelected.ValueItem);
            }
            if (YearSelected != null)
            {
                result &= item.Nam == int.Parse(YearSelected.ValueItem);
            }
            if (SelectedDonViItems != null && !SelectedDonViItems.Id.Equals(Guid.Empty))
            {
                result &= item.MaDonVi == SelectedDonViItems.MaDonVi;
            }
            if (SearchCanBo != null)
            {
                result &= (item.TenCb.ToLower().Contains(SearchCanBo.ToLower()))
                    || item.MaCanBo.ToLower().Contains(SearchCanBo.ToLower());
            }
            return result;
        }

        private void OnOpenUpdateMulti()
        {
            try
            {
                //CadresModel cadres = new CadresModel();
                AllowenceModel allowenceModel = new AllowenceModel();
                allowenceModel.SelectedMonth = int.Parse(MonthSelected.ValueItem);
                allowenceModel.SelectedYear = int.Parse(YearSelected.ValueItem);
                UpdateMultiAllowenceCadresViewModel.Model = allowenceModel;
                UpdateMultiAllowenceCadresViewModel.IsHsq = false;
                UpdateMultiAllowenceCadresViewModel.MenuType = UpdateMultiMenuType.THUE;
                UpdateMultiAllowenceCadresViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                UpdateMultiAllowenceCadresViewModel.Init();
                UpdateMultiAllowenceCadresViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void Detail_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IncomeTaxModel taxModel = (IncomeTaxModel)sender;
            taxModel.IsModified = true;
            OnPropertyChanged(nameof(SelectedIncomeTax));
            OnPropertyChanged(nameof(TlInComeTaxItems));
        }

        public override void OnSave()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<IncomeTaxModel> listEdit = TlInComeTaxItems.Where(x => x.IsModified).ToList();
                if (listEdit != null && listEdit.Count > 0)
                {
                    foreach (var item in listEdit)
                    {
                        var dataPhuCap = _tlCanBoPhuCapService.FindByMaCanBo(item.MaCanBo);
                        var thuong = dataPhuCap.FirstOrDefault(x => x.MaCbo == item.MaCanBo && x.MaPhuCap == "THUONG_TT");
                        thuong.GiaTri = item.TienThuong;
                        var thueDaNop = dataPhuCap.FirstOrDefault(x => x.MaCbo == item.MaCanBo && x.MaPhuCap == "THUEDANOP_TT");
                        thueDaNop.GiaTri = item.ThueTNCNDaNop;
                        var giamThue = dataPhuCap.FirstOrDefault(x => x.MaCbo == item.MaCanBo && x.MaPhuCap == "GIAMTHUE_TT");
                        giamThue.GiaTri = item.TienThueDuocGiam;
                        var thuNhapKhac = dataPhuCap.FirstOrDefault(x => x.MaCbo == item.MaCanBo && x.MaPhuCap == "THUNHAPKHAC_TT");
                        thuNhapKhac.GiaTri = item.LoiIchKhac;
                        List<TlCanBoPhuCap> canboPhuCap = new List<TlCanBoPhuCap> { thuong, thueDaNop, giamThue, thuNhapKhac };
                        _tlCanBoPhuCapService.UpdateRang(canboPhuCap);
                    }
                }
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    MessageBoxHelper.Info("Lưu dữ liệu thành công.");
                    LoadData();
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_TNCN_IMPORT_EXPORT);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    List<IncomeTaxModel> items = new List<IncomeTaxModel>();
                    if (TlInComeTaxItems != null && TlInComeTaxItems.Count > 0)
                    {
                        //if (SelectedDonViItems != null && !SelectedDonViItems.Id.IsNullOrEmpty())
                        //{
                        //    items = TlInComeTaxItems.Where(x => x.MaDonVi.Equals(SelectedDonViItems.MaDonVi)).ToList();
                        //}
                        //else
                        //{
                        //    items = TlInComeTaxItems.OrderBy(x => x.MaDonVi).ThenBy(x => x.MaCb).ThenBy(x => x.TenCb)
                        //        .ToList();
                        //}

                        items = _dtCadresView.Cast<IncomeTaxModel>().OrderBy(x => x.MaDonVi).ThenBy(x => x.MaCb).ThenBy(x => x.TenCb).ToList();
                    }

                    int stt = 1;
                    foreach (var it in items)
                    {
                        it.Stt = stt++;
                    }
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Items", items);
                    fileNamePrefix = string.Format("Bang_Luong_Tncn_Import_{0}_{1}_{2}",
                        SelectedDonViItems != null && !SelectedDonViItems.Id.IsNullOrEmpty() ? SelectedDonViItems.TenDonVi : "", MonthSelected != null ? int.Parse(MonthSelected.ValueItem) : DateTime.Now.Month, YearSelected != null ? int.Parse(YearSelected.ValueItem) : DateTime.Now.Year);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<IncomeTaxModel>(templateFileName, data);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>) e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }

                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnImportData()
        {
            if (TlInComeTaxItems == null
                || MonthSelected == null
                || TlInComeTaxItems.Count == 0
                || !TlInComeTaxItems.Any(n => n.Thang == int.Parse(MonthSelected.ValueItem)))
            {
                MessageBoxHelper.Error(Resources.MsgErrorMonthImportNotHavePeople);
                return;
            }
            ImportSalaryTncnViewModel.Init();
            ImportSalaryTncnViewModel.SavedAction = obj =>
            {
                this.LoadData();
            };
            ImportSalaryTncnViewModel.ShowDialog();
        }
    }
}
