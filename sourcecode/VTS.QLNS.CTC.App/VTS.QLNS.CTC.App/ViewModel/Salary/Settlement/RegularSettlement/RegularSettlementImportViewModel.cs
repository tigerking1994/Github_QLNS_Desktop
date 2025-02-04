using AutoMapper;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.RegularSettlement
{
    public class RegularSettlementImportViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private string _importFolder;
        private string _fileName;
        private IConfiguration _configuration;
        private INsMucLucNganSachService _nsMucLucNganSachService;
        private List<NsMucLucNganSach> _mucLucNganSachs;
        private List<ImportErrorItem> _listErrChungTuChiTiet;

        public override string Name => "Import số liệu dự toán";
        public override string Description => "Import số liệu dự toán";
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public override Type ContentType => typeof(View.Salary.Settlement.RegularSettlement.RegularSettlementImport);

        public IEnumerable<NsMucLucNganSach> ListNsMucLucNganSach = new List<NsMucLucNganSach>();

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public bool _isSelectedFile;
        public bool IsSelectedFile
        {
            get => !string.IsNullOrEmpty(_filePath);
            set => SetProperty(ref _isSelectedFile, value);
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        private ObservableCollection<DivisionDetailImportModel> _divisionDetails = new ObservableCollection<DivisionDetailImportModel>();
        public ObservableCollection<DivisionDetailImportModel> DivisionDetails
        {
            get => _divisionDetails;
            set => SetProperty(ref _divisionDetails, value);
        }

        private DivisionDetailImportModel _selectedDivisionDetail;
        public DivisionDetailImportModel SelectedDivisionDetail
        {
            get => _selectedDivisionDetail;
            set => SetProperty(ref _selectedDivisionDetail, value);
        }

        public bool _isSaveData;
        public bool IsSaveData
        {
            get => DivisionDetails.Count > 0 && !DivisionDetails.Any(x => !x.ImportStatus) && !IsValidateExists;
            set => SetProperty(ref _isSaveData, value);
        }

        public bool _isValidateExists;
        public bool IsValidateExists
        {
            get => _isValidateExists;
            set
            {
                SetProperty(ref _isValidateExists, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private ObservableCollection<NsMuclucNgansachModel> _existedMlns;
        public ObservableCollection<NsMuclucNgansachModel> ExistedMlns
        {
            get => _existedMlns;
            set => SetProperty(ref _existedMlns, value);
        }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand SaveDataCommand { get; }

        public RegularSettlementImportViewModel(
            IConfiguration configuration,
            ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            INsMucLucNganSachService nsMucLucNganSachService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _configuration = configuration;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            SaveDataCommand = new RelayCommand(obj => OnSaveData(obj));
        }

        public override void Init()
        {
            base.Init();
            _importFolder = _configuration.GetSection("ImportFolder").Value;
            OnResetData();
        }

        private void OnResetData()
        {
            _filePath = string.Empty;

            _divisionDetails = new ObservableCollection<DivisionDetailImportModel>();
            _mucLucNganSachs = _nsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork, 1).ToList();
            _existedMlns = new ObservableCollection<NsMuclucNgansachModel>(_mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(_mucLucNganSachs));
            _listErrChungTuChiTiet = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(DivisionDetails));
            OnPropertyChanged(nameof(IsSaveData));

            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(ExistedMlns));
        }

        private void OnUploadFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Chọn file excel",
                RestoreDirectory = true,
                DefaultExt = StringUtils.EXCEL_EXTENSION
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FilePath = openFileDialog.FileName;
            _fileName = openFileDialog.SafeFileName;
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
        }

        private void OnProcessFile()
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(FilePath))
            {
                message = Resources.ErrorFileEmpty;
                goto ShowError;
            }
        ShowError:
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Warning(message);
                return;
            }

            try
            {
                var sheetDetailAttribute = (SheetAttribute)Attribute.GetCustomAttribute(typeof(DivisionDetailImportModel), typeof(SheetAttribute));
                var importDivisionDetailResult = _importService.ProcessData<DivisionDetailImportModel>(FilePath);

                var yearOfWork = _sessionService.Current.YearOfWork;
                ListNsMucLucNganSach = _nsMucLucNganSachService.FindByXauNoiMaAndNamLamViec(importDivisionDetailResult.Data.Select(x => x.XauNoiMa), yearOfWork, 1).ToList();

                var listXauNoiMaHangCha = ListNsMucLucNganSach.Where(x => x.BHangCha).Select(x => x.XauNoiMa).ToHashSet();
                var nsMucLucNganSachGroupByXauNoiMa = ListNsMucLucNganSach.GroupBy(x => x.XauNoiMa)
                    .ToDictionary(x => x.Key, x => x.First());

                var dictErr = importDivisionDetailResult.ImportErrors.ToLookup(x => x.Row)
                    .ToDictionary(x => x.Key, x => x.ToList());
                var numberRecordExclude = 0;
                var divisionDetailsImport = importDivisionDetailResult.Data.Where((x, i) =>
                {
                    var mlns = ListNsMucLucNganSach.Where(z => z.XauNoiMa == x.XauNoiMa).FirstOrDefault();
                    var isHangCha = listXauNoiMaHangCha.Contains(x.XauNoiMa) && ListNsMucLucNganSach.Any(z => z.MlnsIdParent == mlns.MlnsId);
                    if (!isHangCha)
                    {
                        x.MoTa = nsMucLucNganSachGroupByXauNoiMa.GetValueOrDefault(x.XauNoiMa, new NsMucLucNganSach()).MoTa;
                        var listErrRepairIndex = dictErr.GetValueOrDefault(i, new List<ImportErrorItem>()).ToList();
                        listErrRepairIndex.ForEach(err => err.Row = i - numberRecordExclude);
                        _listErrChungTuChiTiet.AddRange(listErrRepairIndex);
                    }
                    else
                    {
                        numberRecordExclude++;
                    }

                    return !isHangCha;
                }).ToList();

                _divisionDetails = new ObservableCollection<DivisionDetailImportModel>(divisionDetailsImport);
                OnPropertyChanged(nameof(DivisionDetails));

                if (!_divisionDetails.Any())
                {
                    message = string.Format(Resources.MsgSheetErrorDataEmpty, sheetDetailAttribute.SheetName);
                    System.Windows.MessageBox.Show(message, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    OnPropertyChanged(nameof(IsSaveData));
                    return;
                }

                foreach (var item in _divisionDetails)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(DivisionDetailImportModel.ImportStatus)
                            && args.PropertyName != nameof(DivisionDetailImportModel.XauNoiMa)
                            && args.PropertyName != nameof(DivisionDetailImportModel.IsErrorMLNS))
                        {
                            var entityDetail = (DivisionDetailImportModel)sender;
                            var rowIndex = _divisionDetails.IndexOf(entityDetail);
                            var listError = _importService.ValidateItem(entityDetail, rowIndex);
                            if (listError.Count > 0)
                            {
                                var messageOfRow = listError.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToList();
                                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, messageOfRow), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                                _listErrChungTuChiTiet.AddRange(listError);
                                entityDetail.ImportStatus = false;
                                if (listError.Any(x => x.IsErrorMLNS))
                                {
                                    entityDetail.IsErrorMLNS = true;
                                }
                            }
                            else
                            {
                                entityDetail.ImportStatus = true;
                                entityDetail.IsErrorMLNS = false;
                                _listErrChungTuChiTiet = _listErrChungTuChiTiet.Where(x => x.Row != rowIndex).ToList();
                            }
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                }

                for (var i = 0; i < _divisionDetails.Count; i++)
                {
                    var item = _divisionDetails[i];
                    if (item.ImportStatus)
                    {
                        var errorsXauNoiMa = GetErrorXauNoiMa(sheetDetailAttribute.SheetName, i, item.XauNoiMa, item.MoTa);
                        if (errorsXauNoiMa.Any())
                        {
                            item.ImportStatus = false;
                            _listErrChungTuChiTiet.AddRange(errorsXauNoiMa);
                        }
                    }
                }

                OnPropertyChanged(nameof(DivisionDetails));
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                if (ex is Utility.Exceptions.WrongReportException)
                {
                    System.Windows.MessageBox.Show(Resources.WrongReportFormat, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private List<ImportErrorItem> GetErrorXauNoiMa(string sheetName, int row, string xauNoiMa, string message)
        {
            var errors = new List<ImportErrorItem>();
            if (!ListNsMucLucNganSach.Any(x => x.XauNoiMa.Equals(xauNoiMa)))
            {
                errors.Add(new ImportErrorItem
                {
                    Row = row,
                    Error = $"Không có quyền thao tác với loại ngân sách: {message}.",
                    SheetName = sheetName
                });
            }
            return errors;
        }

        private void OnSaveData(object obj)
        {
            base.OnSave();
            SavedAction?.Invoke(DivisionDetails);
            MessageBoxHelper.Info("Lấy dữ liệu thành công");
            var window = obj as Window;
            window.Close();
        }
    }
}
