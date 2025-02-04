using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.ImportSalary
{
    public class ImportFeeCollectionBhxhViewModel : DialogViewModelBase<TlQuanLyThuNopBhxhModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IImportExcelService _importService;
        private readonly ITlDmDonViService _dmDonViService;
        private readonly ITlDmCanBoService _dmCanBoService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly IMapper _mapper;
        private readonly ITlQuanLyThuNopBhxhChiTietService _tlQuanLyThuNopBhxhChiTietService;
        private readonly ITlQuanLyThuNopBhxhService _tlQuanLyThuNopBhxhService;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(View.Salary.ImportSalary.ImportFeeCollectionBhxh);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private ObservableCollection<SalaryMonthImportModel> _salaryMonthImportModels;
        public ObservableCollection<SalaryMonthImportModel> SalaryMonthImportModels
        {
            get => _salaryMonthImportModels;
            set => SetProperty(ref _salaryMonthImportModels, value);
        }

        private SalaryMonthImportModel _seletedBangLuong;
        public SalaryMonthImportModel SeletedBangLuong
        {
            get => _seletedBangLuong;
            set => SetProperty(ref _seletedBangLuong, value);
        }

        private ObservableCollection<FeeCollectionBhxhImportModel> _dataDetailImportModels;
        public ObservableCollection<FeeCollectionBhxhImportModel> DataDetailImportModels
        {
            get => _dataDetailImportModels;
            set => SetProperty(ref _dataDetailImportModels, value);
        }

        private FeeCollectionBhxhImportModel _seletedItem;
        public FeeCollectionBhxhImportModel SelectedItem
        {
            get => _seletedItem;
            set => SetProperty(ref _seletedItem, value);
        }

        private ObservableCollection<TlQuanLyThuNopBhxhModel> _dataViewImport;
        public ObservableCollection<TlQuanLyThuNopBhxhModel> DataViewImport
        {
            get => _dataViewImport;
            set => SetProperty(ref _dataViewImport, value);
        }

        private ObservableCollection<TlDmCanBoImportModel> _dmCanBoImportModels;
        public ObservableCollection<TlDmCanBoImportModel> DmCanBoImportModels
        {
            get => _dmCanBoImportModels;
            set => SetProperty(ref _dmCanBoImportModels, value);
        }

        private ObservableCollection<TlDmDonViImportModel> _dmDonViImportModel;
        public ObservableCollection<TlDmDonViImportModel> DmDonViImportModel
        {
            get => _dmDonViImportModel;
            set => SetProperty(ref _dmDonViImportModel, value);
        }

        private ObservableCollection<TlDmCanBoPhuCapImportModel> _dmCanBoPhuCapImportModel;
        public ObservableCollection<TlDmCanBoPhuCapImportModel> DmCanBoPhuCapImportModel
        {
            get => _dmCanBoPhuCapImportModel;
            set => SetProperty(ref _dmCanBoPhuCapImportModel, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (DataDetailImportModels.Count > 0)
                    return !DataDetailImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
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
                SetProperty(ref _monthSelected, value);
                LoadTenDsBangLuong();
                LoadDataChange();
                OnPropertyChanged(nameof(Model));
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
                SetProperty(ref _yearSelected, value);
                LoadTenDsBangLuong();
                LoadDataChange();
            }
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorBangLuongCommand { get; }
        public RelayCommand ShowErrorBangLuongDetailCommand { get; }

        public ImportFeeCollectionBhxhViewModel(ISessionService sessionService,
            ITlDmCanBoService dmCanBoService,
            ITlDmDonViService dmDonViService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            IMapper mapper,
            IImportExcelService importService,
            ITlQuanLyThuNopBhxhChiTietService tlQuanLyThuNopBhxhChiTietService,
            ITlQuanLyThuNopBhxhService tlQuanLyThuNopBhxhService)
        {
            _sessionService = sessionService;
            _dmDonViService = dmDonViService;
            _dmCanBoService = dmCanBoService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _mapper = mapper;
            _importService = importService;
            _tlQuanLyThuNopBhxhChiTietService = tlQuanLyThuNopBhxhChiTietService;
            _tlQuanLyThuNopBhxhService = tlQuanLyThuNopBhxhService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ShowErrorBangLuongCommand = new RelayCommand(obj => ShowErrorBangLuong());
            ShowErrorBangLuongDetailCommand = new RelayCommand(obj => ShowErrorBangluongDetail());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            OnResetData();
            LoadMonths();
            LoadYear();
            LoadTenDsBangLuong();
        }

        private void LoadTenDsBangLuong()
        {
            if (MonthSelected != null && YearSelected != null)
            {
                Model.DTuNgay = new DateTime(int.Parse(YearSelected.ValueItem), int.Parse(MonthSelected.ValueItem), 1);
                Model.DDenNgay = new DateTime(int.Parse(YearSelected.ValueItem), int.Parse(MonthSelected.ValueItem), 1).AddMonths(1).AddDays(-1);
            }
            else
            {
                Model.DTuNgay = new DateTime((int)Model.INam, (int)Model.IThang, 1);
                Model.DDenNgay = new DateTime((int)Model.INam, (int)Model.IThang, 1).AddMonths(1).AddDays(-1);
            }
            var TenDs = string.Format("Chứng từ quản lý thu nộp BHXH tổng hợp tháng {0} - năm {1} - {2} ", Model.IThang, Model.INam, Model.STenDonVi);
            Model.STen = TenDs;
            OnPropertyChanged(TenDs);
            OnPropertyChanged(nameof(Model));
        }

        private void LoadDataChange()
        {
            if (MonthSelected != null && YearSelected != null)
            {
                if (!DataDetailImportModels.IsEmpty())
                    DataDetailImportModels.ForAll(x => { x.IThang = MonthSelected.ValueItem; x.INam = YearSelected.ValueItem; });
                Model.IThang = int.Parse(MonthSelected.ValueItem);
                Model.INam = int.Parse(YearSelected.ValueItem);
            }
            else if (MonthSelected != null)
            {
                if (!DataDetailImportModels.IsEmpty())
                    DataDetailImportModels.ForAll(x => { x.IThang = MonthSelected.ValueItem; });
                Model.IThang = int.Parse(MonthSelected.ValueItem);
            }
            else if (YearSelected != null)
            {
                if (!DataDetailImportModels.IsEmpty())
                    DataDetailImportModels.ForAll(x => { x.INam = YearSelected.ValueItem; });
                Model.INam = int.Parse(YearSelected.ValueItem);
            }
            OnPropertyChanged(nameof(DataViewImport));
            OnPropertyChanged(nameof(DataDetailImportModels));
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(Months));
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == Model.IThang.ToString());
        }

        public void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == Model.INam.ToString());
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            DataDetailImportModels = new ObservableCollection<FeeCollectionBhxhImportModel>();
            DataViewImport = new ObservableCollection<TlQuanLyThuNopBhxhModel>();
            SalaryMonthImportModels = new ObservableCollection<SalaryMonthImportModel>();
            _importErrors = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file excel");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FileName = openFileDialog.FileName;
        }

        private void ShowErrorBangLuong()
        {
            int rowIndex = _salaryMonthImportModels.IndexOf(SeletedBangLuong);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowErrorBangluongDetail()
        {
            int rowIndex = _dataDetailImportModels.IndexOf(SelectedItem);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnProcessFile()
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(FileName))
            {
                message = Resources.ErrorFileEmpty;
                goto ShowError;
            }
        ShowError:
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();

                //lấy thông tin bảng lương chi tiết
                ImportResult<FeeCollectionBhxhImportModel> salaryMonthDetail = _importService.ProcessData<FeeCollectionBhxhImportModel>(FileName);
                DataDetailImportModels = new ObservableCollection<FeeCollectionBhxhImportModel>(salaryMonthDetail.Data);

                if (salaryMonthDetail.ImportErrors.Count > 0)
                    errors.AddRange(salaryMonthDetail.ImportErrors);

                if (DataDetailImportModels.IsEmpty())
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var data = _dmDonViService.FindAll().OrderBy(x => x.XauNoiMa);
                    Model.IIdMaDonVi = DataDetailImportModels.FirstOrDefault().IIdMaDonVi;
                    Model.STenDonVi = data.Any(x => x.MaDonVi.Equals(Model.IIdMaDonVi)) ? data.FirstOrDefault(x => x.MaDonVi.Equals(Model.IIdMaDonVi)).TenDonVi : string.Empty;
                    Model.SMaCachTl = DataDetailImportModels.FirstOrDefault().SMaCachTinhLuong;
                    DataDetailImportModels.ForAll(x =>
                    {
                        x.IIdParentId = Model.Id;
                        x.IThang = Model.IThang.ToString();
                        x.INam = Model.INam.ToString();

                    });
                }
                if (_dataDetailImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                _dataViewImport.Clear();
                _dataViewImport.Add(Model);
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(Model));

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);        
            }
        }

        private bool ValidateDataModel()
        {
            var predicate = PredicateBuilder.True<TlQuanLyThuNopBhxh>();
            predicate = predicate.And(x => x.IThang.Equals(Model.IThang));
            predicate = predicate.And(x => x.INam.Equals(Model.INam));
            predicate = predicate.And(x => x.IIdMaDonVi.Equals(Model.IIdMaDonVi));
            var isCheckExit = _tlQuanLyThuNopBhxhService.FindByCondition(predicate);
            return isCheckExit.IsEmpty();
        }

        private void OnSaveData()
        {
            if (!ValidateDataModel())
            {
                MessageBoxHelper.Warning(string.Format(Resources.FeeCollectionBhxhExist, Model.IThang, Model.INam, Model.STenDonVi));
                return;
            }

            var messageBox = MessageBoxHelper.Confirm("Đồng chí có muốn nhận dữ liệu cán bộ không?");
            if (messageBox == MessageBoxResult.Yes)
            {
                // Insert Model
                Model.Status = true;
                Model.BIsKhoa = false;
                Model.SNguoiTao = _sessionInfo.Principal;
                var entity = _mapper.Map<TlQuanLyThuNopBhxh>(Model);
                var dataImports = _mapper.Map<List<TlQuanLyThuNopBhxhChiTiet>>(DataDetailImportModels);
                _tlQuanLyThuNopBhxhService.SaveEntitiesAndDetails(new List<TlQuanLyThuNopBhxh>() { entity }, dataImports);
                //_tlQuanLyThuNopBhxhChiTietService.BulkInsert(dataImports);
                SavedAction?.Invoke(_mapper.Map<TlQuanLyThuNopBhxhModel>(entity));

            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}
