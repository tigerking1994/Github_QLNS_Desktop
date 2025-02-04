using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Distribution.ImportExpertise;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Distribution.ImportExpertise
{
    public class ImportExpertiseDistributionViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;

        public override string Name => "";
        public override Type ContentType => typeof(View.Budget.DemandCheck.Distribution.ImportExpertise.ImportExpertiseDistribution);
        public override string Description => "";
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }
        private List<ExpertiseDistributionDetailImportModel> _expertiseVoucherDetailProcess;

        private ObservableCollection<ExpertiseDistributionDetailImportModel> _expertiseVoucherDetails;
        public ObservableCollection<ExpertiseDistributionDetailImportModel> ExpertiseVoucherDetails
        {
            get => _expertiseVoucherDetails;
            set => SetProperty(ref _expertiseVoucherDetails, value);
        }

        private ExpertiseDistributionDetailImportModel _selectedItem;
        public ExpertiseDistributionDetailImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
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
                if (ExpertiseVoucherDetails.Count > 0)
                    return !ExpertiseVoucherDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        private List<NsSktMucLuc> _nsSktMucLucs;
        public List<NsSktMucLuc> NsSktMucLucs
        {
            get => _nsSktMucLucs;
            set => SetProperty(ref _nsSktMucLucs, value);
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
        public RelayCommand ShowErrorCommand { get; }
        public ImportExpertiseDistribution ImportExpertiseDistribution { get; set; }

        public ImportExpertiseDistributionViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            ISktChungTuService sktChungTuService,
            ISktMucLucService sktMucLucService,
            ISktChungTuChiTietService sktChungTuChiTietService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _sktChungTuService = sktChungTuService;
            _sktMucLucService = sktMucLucService;
            _sktChungTuChiTietService = sktChungTuChiTietService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ShowErrorCommand = new RelayCommand(obj => ShowError());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            OnResetData();
            LoadAllSktMucLuc();
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            _expertiseVoucherDetails = new ObservableCollection<ExpertiseDistributionDetailImportModel>();
            _importErrors = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(ExpertiseVoucherDetails));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void LoadAllSktMucLuc()
        {
            var predicate = PredicateBuilder.True<NsSktMucLuc>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            _nsSktMucLucs = _sktMucLucService.FindByCondition(predicate).ToList();
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

        private void ShowError()
        {
            int rowIndex = _expertiseVoucherDetails.IndexOf(SelectedItem);
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
                //xử lý chứng từ chi tiết
                ImportResult<ExpertiseDistributionDetailImportModel> _voucherDetailResult = _importService.ProcessData<ExpertiseDistributionDetailImportModel>(FileName);

                //CalculateData();
                _expertiseVoucherDetails = new ObservableCollection<ExpertiseDistributionDetailImportModel>(_voucherDetailResult.Data);
                foreach (var item in _expertiseVoucherDetails)
                {
                    if (string.IsNullOrEmpty(item.MaDonVi))
                    {
                        item.BHangCha = true;
                    }
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(ExpertiseDistributionDetailImportModel.ImportStatus)
                            && args.PropertyName != nameof(ExpertiseDistributionDetailImportModel.IsErrorMLNS))
                        {
                            var voucherDetail = (ExpertiseDistributionDetailImportModel)sender;
                            int rowIndex = _expertiseVoucherDetails.IndexOf(voucherDetail);
                            var listError = _importService.ValidateItem<ExpertiseDistributionDetailImportModel>(voucherDetail, rowIndex);
                            if (listError.Count > 0)
                            {
                                List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                                string message = string.Join(Environment.NewLine, errors);
                                System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
                                _importErrors.AddRange(listError);
                                voucherDetail.ImportStatus = false;
                                if (listError.Any(x => x.IsErrorMLNS))
                                    voucherDetail.IsErrorMLNS = true;
                            }
                            else
                            {
                                voucherDetail.ImportStatus = true;
                                voucherDetail.IsErrorMLNS = false;
                                _importErrors.RemoveAll(x => x.Row == rowIndex);
                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        }
                    };
                }

                OnPropertyChanged(nameof(ExpertiseVoucherDetails));
                if (_voucherDetailResult.ImportErrors.Count > 0)
                    errors.AddRange(_voucherDetailResult.ImportErrors);

                if (_expertiseVoucherDetails == null || _expertiseVoucherDetails.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (ExpertiseVoucherDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                if (errors.Count > 0)
                {
                    _importErrors = new ObservableCollection<ImportErrorItem>(errors).ToList();
                    OnPropertyChanged(nameof(ImportErrors));
                }
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                if (ex is Utility.Exceptions.WrongReportException)
                {
                    System.Windows.MessageBox.Show(Resources.WrongReportFormat, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void OnSaveData()
        {
            List<NsSktNganhThamDinhChiTietSktModel> results = new List<NsSktNganhThamDinhChiTietSktModel>();
            List<ExpertiseDistributionDetailImportModel> listDetailImport = _expertiseVoucherDetails.Where(x => x.ImportStatus && !x.IsWarning && !string.IsNullOrEmpty(x.MaDonVi)).ToList();
            foreach (var item in listDetailImport)
            {
                NsSktMucLuc mucLuc = _nsSktMucLucs.FirstOrDefault(x => !string.IsNullOrEmpty(item.KyHieu) && x.SKyHieu.Equals(item.KyHieu));
                if (item.KyHieu.Equals("") || mucLuc == null)
                {
                    continue;
                }
                NsSktNganhThamDinhChiTietSktModel tdChiTiet = new NsSktNganhThamDinhChiTietSktModel();
                tdChiTiet.IIdMucLuc = mucLuc.IIDMLSKT;
                tdChiTiet.IIdMucLucParent = mucLuc.IIDMLSKTCha;
                tdChiTiet.IIdMaDonVi = item.MaDonVi;
                tdChiTiet.STenDonVi = item.TenDonVi;
                tdChiTiet.SM = mucLuc.SM;
                tdChiTiet.SMoTa = mucLuc.SMoTa;
                tdChiTiet.SKyHieu = mucLuc.SKyHieu;
                tdChiTiet.SStt = mucLuc.SSTT;
                tdChiTiet.INamLamViec = _sessionService.Current.YearOfWork;
                tdChiTiet.INamNganSach = _sessionService.Current.YearOfBudget;
                tdChiTiet.IIdMaNguonNganSach = _sessionService.Current.Budget;
                tdChiTiet.FTuChi = !string.IsNullOrEmpty(item.TuChi) ? double.Parse(item.TuChi) : 0;
                results.Add(tdChiTiet);
            }
            ImportExpertiseDistribution?.SavedAction?.Invoke(results.ToList());

        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

    }
}
