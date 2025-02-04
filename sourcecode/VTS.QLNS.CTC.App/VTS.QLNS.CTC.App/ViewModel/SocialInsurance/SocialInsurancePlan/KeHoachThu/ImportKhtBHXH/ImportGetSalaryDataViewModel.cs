using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu.ImportKhtBHXH
{
    public class ImportGetSalaryDataViewModel : DialogViewModelBase<BhKhtBHXHModel>
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

        public override Type ContentType => typeof(View.SocialInsurance.SocialInsurancePlan.KeHoachThu.ImportKhtBHXH.ImportGetSalaryData);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private List<TlBangLuongKeHoachExportQuery> _dataImport;
        public List<TlBangLuongKeHoachExportQuery> DataImport
        {
            get => _dataImport;
            set => SetProperty(ref _dataImport, value);
        }

        private TlBangLuongKeHoachExportQuery _seletedItem;
        public TlBangLuongKeHoachExportQuery SeletedItem
        {
            get => _seletedItem;
            set => SetProperty(ref _seletedItem, value);
        }
        
        private ObservableCollection<TlBangLuongKeHoachImportModel> _dataViewImport;
        public ObservableCollection<TlBangLuongKeHoachImportModel> DataViewImport
        {
            get => _dataViewImport;
            set => SetProperty(ref _dataViewImport, value);
        }

        private TlBangLuongKeHoachImportModel _seletedDataView;
        public TlBangLuongKeHoachImportModel SeletedDataView
        {
            get => _seletedDataView;
            set => SetProperty(ref _seletedDataView, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }


        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorBangLuongCommand { get; }
        public RelayCommand ShowErrorBangLuongDetailCommand { get; }

        public ImportGetSalaryDataViewModel(ISessionService sessionService,
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
            SaveCommand = new RelayCommand(obj => OnSaveData(obj));
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

        }



        private void OnResetData()
        {
            _fileName = string.Empty;
            DataImport = new List<TlBangLuongKeHoachExportQuery>();
            DataViewImport = new ObservableCollection<TlBangLuongKeHoachImportModel>();
            _importErrors = new List<ImportErrorItem>();
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(ImportErrors));
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
            int rowIndex = DataViewImport.IndexOf(SeletedDataView);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowErrorBangluongDetail()
        {
            int rowIndex = DataViewImport.IndexOf(SeletedDataView);
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

                var fileExtension = Path.GetExtension(FileName).ToLower();
                if (!(fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx")))
                {
                    System.Windows.MessageBox.Show(Resources.FileImportWrongExtensionExcel, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                //lấy thông tin bảng lương chi tiết
                ImportResult<TlBangLuongKeHoachImportModel> dataImport = _importService.ProcessData<TlBangLuongKeHoachImportModel>(FileName);
                DataViewImport = new ObservableCollection<TlBangLuongKeHoachImportModel>(dataImport.Data);

                if (dataImport.ImportErrors.Count > 0)
                    errors.AddRange(dataImport.ImportErrors);

                if (DataViewImport.IsEmpty())
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    DataImport = _mapper.Map<List<TlBangLuongKeHoachExportQuery>>(DataViewImport);
                    DataViewImport.ForAll(f =>
                    {
                        f.BHangCha = (int)NumberUtils.ConvertTextToDouble(f.ILevel) < 2;
                    });
                }
                if (DataViewImport.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                OnPropertyChanged(nameof(Model));
                OnPropertyChanged(nameof(DataViewImport));

            }
            catch (Exception ex)
            {
                if (ex.Message == "Sai template")
                {
                    System.Windows.MessageBox.Show(Resources.FileImportWrongTemplate, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        //private bool ValidateDataModel()
        //{
        //    var predicate = PredicateBuilder.True<TlQuanLyThuNopBhxh>();
        //    predicate = predicate.And(x => x.IThang.Equals(Model.IThang));
        //    predicate = predicate.And(x => x.INam.Equals(Model.INam));
        //    predicate = predicate.And(x => x.IIdMaDonVi.Equals(Model.IIdMaDonVi));
        //    var isCheckExit = _tlQuanLyThuNopBhxhService.FindByCondition(predicate);
        //    return isCheckExit.IsEmpty();
        //}

        private void OnSaveData(object obj)
        {
            var messageBox = MessageBoxHelper.Confirm("Đồng chí có muốn nhận dữ liệu không?");
            if (messageBox == MessageBoxResult.Yes)
            {
                SavedAction?.Invoke(DataImport);
                OnCloseWindow(obj);
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}
