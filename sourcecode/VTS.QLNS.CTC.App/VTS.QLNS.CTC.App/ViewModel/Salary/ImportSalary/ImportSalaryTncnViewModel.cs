using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.ImportSalary
{
    public class ImportSalaryTncnViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ILog _logger;
        private readonly IImportExcelService _importService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(View.Salary.ImportSalary.ImportSalaryTncn);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private ObservableCollection<SalaryMonthTncnImportModel> _salaryMonthTncnImportModels;
        public ObservableCollection<SalaryMonthTncnImportModel> SalaryMonthTncnImportModels
        {
            get => _salaryMonthTncnImportModels;
            set => SetProperty(ref _salaryMonthTncnImportModels, value);
        }

        private SalaryMonthTncnImportModel _seletedBangLuong;
        public SalaryMonthTncnImportModel SeletedBangLuong
        {
            get => _seletedBangLuong;
            set => SetProperty(ref _seletedBangLuong, value);
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
                if (SalaryMonthTncnImportModels.Count > 0)
                    return !SalaryMonthTncnImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }


        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorBangLuongCommand { get; }

        public ImportSalaryTncnViewModel(ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IImportExcelService importService,
            ITlCanBoPhuCapService tlCanBoPhuCapService)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _importService = importService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ShowErrorBangLuongCommand = new RelayCommand(obj => ShowErrorBangLuong());
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
            SalaryMonthTncnImportModels = new ObservableCollection<SalaryMonthTncnImportModel>();
            _importErrors = new List<ImportErrorItem>();
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
            int rowIndex = _salaryMonthTncnImportModels.IndexOf(SeletedBangLuong);
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
                //lấy thông tin thuế tncn
                ImportResult<SalaryMonthTncnImportModel> salaryMonth = _importService.ProcessData<SalaryMonthTncnImportModel>(FileName);
                SalaryMonthTncnImportModels = new ObservableCollection<SalaryMonthTncnImportModel>(salaryMonth.Data);

                if (salaryMonth.ImportErrors.Count > 0)
                    errors.AddRange(salaryMonth.ImportErrors);

                if (SalaryMonthTncnImportModels == null || SalaryMonthTncnImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (SalaryMonthTncnImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);

                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnSaveData()
        {
            try
            {
                List<IncomeTaxModel> listEdit = _mapper.Map<List<IncomeTaxModel>>(SalaryMonthTncnImportModels);
                List<string> lstPhucCap = new List<string>();
                lstPhucCap.Add(PhuCap.THUONG_TT);
                lstPhucCap.Add(PhuCap.THUEDANOP_TT);
                lstPhucCap.Add(PhuCap.GIAMTHUE_TT);
                lstPhucCap.Add(PhuCap.THUNHAPKHAC_TT);

                var lstMaCanBoUpdate = listEdit.Select(x => x.MaCanBo).Distinct().ToList();
                var dataAllPhuCap = _tlCanBoPhuCapService
                    .FindAll(x => lstMaCanBoUpdate.Contains(x.MaCbo) && lstPhucCap.Contains(x.MaPhuCap)).ToList();
                List<TlCanBoPhuCap> canboPhuCap = new List<TlCanBoPhuCap>();
                if (listEdit != null && listEdit.Count > 0)
                {
                    foreach (var item in listEdit)
                    {
                        var dataPhuCap = dataAllPhuCap.Where(x => x.MaCbo.Equals(item.MaCanBo)).ToList();
                        var thuong = dataPhuCap.FirstOrDefault(x =>
                            x.MaCbo == item.MaCanBo && x.MaPhuCap == PhuCap.THUONG_TT);
                        thuong.GiaTri = item.TienThuong.GetValueOrDefault();
                        var thueDaNop = dataPhuCap.FirstOrDefault(x =>
                            x.MaCbo == item.MaCanBo && x.MaPhuCap == PhuCap.THUEDANOP_TT);
                        thueDaNop.GiaTri = item.ThueTNCNDaNop.GetValueOrDefault();
                        var giamThue = dataPhuCap.FirstOrDefault(x =>
                            x.MaCbo == item.MaCanBo && x.MaPhuCap == PhuCap.GIAMTHUE_TT);
                        giamThue.GiaTri = item.TienThueDuocGiam.GetValueOrDefault();
                        var thuNhapKhac = dataPhuCap.FirstOrDefault(x =>
                            x.MaCbo == item.MaCanBo && x.MaPhuCap == PhuCap.THUNHAPKHAC_TT);
                        thuNhapKhac.GiaTri = item.LoiIchKhac.GetValueOrDefault();
                        canboPhuCap.Add(thuong);
                        canboPhuCap.Add(thueDaNop);
                        canboPhuCap.Add(giamThue);
                        canboPhuCap.Add(thuNhapKhac);
                    }
                    _tlCanBoPhuCapService.BulkUpdate(canboPhuCap);

                    MessageBoxHelper.Info("Lưu dữ liệu thành công.");
                }
                SavedAction?.Invoke(_mapper.Map<IncomeTaxModel>(new IncomeTaxModel()));
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}
