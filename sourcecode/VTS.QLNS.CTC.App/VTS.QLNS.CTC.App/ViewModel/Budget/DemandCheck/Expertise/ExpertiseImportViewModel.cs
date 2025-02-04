using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility.Exceptions;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Expertise
{
    public class ExpertiseImportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IDanhMucService _danhMucService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly ISktNganhThamDinhService _nganhThamDinhService;
        private readonly ISktNganhThamDinhChiTietService _thamDinhChiTietService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly INsDonViService _nSDonViService;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler OpenDetail;

        public override string Name => "Import dữ liệu ngành thẩm định";
        public override Type ContentType => typeof(View.Budget.DemandCheck.Expertise.ExpertiseImport);
        public override string Description => "Import dữ liệu ngành thẩm định";
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private List<NsSktMucLuc> _nsSktMucLucs;
        private List<ExpertiseImportModel> _demandVoucherDetailProcess;

        private ObservableCollection<ExpertiseImportModel> _demandVoucherDetails;
        public ObservableCollection<ExpertiseImportModel> DemandVoucherDetails
        {
            get => _demandVoucherDetails;
            set => SetProperty(ref _demandVoucherDetails, value);
        }

        private ExpertiseImportModel _selectedItem;
        public ExpertiseImportModel SelectedItem
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

        private ObservableCollection<ComboboxItem> _dataPhanLoai;
        public ObservableCollection<ComboboxItem> DataPhanLoai
        {
            get => _dataPhanLoai;
            set => SetProperty(ref _dataPhanLoai, value);
        }

        private ComboboxItem _selectedPhanLoai;
        public ComboboxItem SelectedPhanLoai
        {
            get => _selectedPhanLoai;
            set
            {
                if (SetProperty(ref _selectedPhanLoai, value) && _selectedPhanLoai != null)
                {
                    LoadDonVi();
                }
            }
        }

        public bool IsSaveData
        {
            get
            {
                if (DemandVoucherDetails.Count > 0)
                    return !DemandVoucherDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        private ObservableCollection<ComboboxItem> _listDonVi = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }

        private ComboboxItem _donViSelected;

        public ComboboxItem DonViSelected
        {
            get => _donViSelected;
            set
            {
                SetProperty(ref _donViSelected, value);
            }
        }

        private ObservableCollection<SktMucLucModel> _existedMlskt;
        public ObservableCollection<SktMucLucModel> ExistedMlskt
        {
            get => _existedMlskt;
            set => SetProperty(ref _existedMlskt, value);
        }

        private ObservableCollection<SktMucLucModel> _importedMlskt;
        public ObservableCollection<SktMucLucModel> ImportedMlskt
        {
            get => _importedMlskt;
            set => SetProperty(ref _importedMlskt, value);
        }

        private List<SktMucLucModel> _mergeItems;
        public bool IsEnableSaveMLSKT => _mergeItems.Count > 0;

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        public bool IsEnabledMergeBtn
        {
            get => ImportedMlskt.Where(i => i.IsSelected).Count() > 0 && ExistedMlskt.Where(i => i.IsSelected).Count() == 1;
        }

        public bool IsEnabledUnmergeCommand
        {
            get => ExistedMlskt.Where(i => i.IsModified && i.IsSelected).Count() > 0;
        }

        private SktMucLucModel _selectedParent;
        public SktMucLucModel SelectedParent
        {
            get => _selectedParent;
            set
            {
                SetProperty(ref _selectedParent, value);
                OnPropertyChanged(nameof(IsEnabledMergeBtn));
            }
        }

        private ObservableCollection<ComboboxItem> _dataLoaiNganSach;
        public ObservableCollection<ComboboxItem> DataLoaiNganSach
        {
            get => _dataLoaiNganSach;
            set => SetProperty(ref _dataLoaiNganSach, value);
        }

        private ComboboxItem _selectedLoaiNganSach;
        public ComboboxItem SelectedLoaiNganSach
        {
            get => _selectedLoaiNganSach;
            set
            {
                SetProperty(ref _selectedLoaiNganSach, value);
                OnPropertyChanged(nameof(ShowColNSBD));
                OnPropertyChanged(nameof(ShowColNSSD));
                OnPropertyChanged(nameof(TitleColumn));
            }
        }

        public Visibility ShowColNSBD => SelectedLoaiNganSach != null && VoucherType.NSBD_Key == SelectedLoaiNganSach.ValueItem ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowColNSSD => SelectedLoaiNganSach != null && VoucherType.NSSD_Key == SelectedLoaiNganSach.ValueItem ? Visibility.Visible : Visibility.Collapsed;

        public string TitleColumn => (SelectedLoaiNganSach != null && VoucherType.NSBD_Key == SelectedLoaiNganSach.ValueItem) ?
            "Mua hàng cấp hiện vật" : "Tự chi";

        public DateTime NgayChungTu { get; set; }
        public string MoTa { get; set; }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand AddMLSKTCommand { get; }
        public RelayCommand MergeCommand { get; }
        public RelayCommand UnmergeCommand { get; }
        public RelayCommand SaveMLSKTCommand { get; }

        public ExpertiseImportViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            ISktChungTuService sktChungTuService,
            ISktMucLucService sktMucLucService,
            INsDonViService nSDonViService,
            ISktNganhThamDinhService nganhThamDinhService,
            IDanhMucService danhMucService,
            ISktNganhThamDinhChiTietService thamDinhChiTietService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;

            _sktMucLucService = sktMucLucService;
            _nSDonViService = nSDonViService;
            _nganhThamDinhService = nganhThamDinhService;
            _thamDinhChiTietService = thamDinhChiTietService;
            _danhMucService = danhMucService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            AddMLSKTCommand = new RelayCommand(obj => OnAddMLSKT());
            MergeCommand = new RelayCommand(obj => OnMerge());
            UnmergeCommand = new RelayCommand(obj => OnUnMerge());
            SaveMLSKTCommand = new RelayCommand(obj => OnSaveMLSKT());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            OnResetData();
            LoadPhanLoai();
            LoadDonVi();
            LoadLoaiNganSach();
            NgayChungTu = DateTime.Now;
        }

        private void LoadPhanLoai()
        {
            DataPhanLoai = new ObservableCollection<ComboboxItem>();
            DataPhanLoai.Add(new ComboboxItem { ValueItem = LoaiNganhThamDinh.CTCTCDN.ToString(), DisplayItem = LoaiNganhThamDinh.TEN_CTCTCDN });
            DataPhanLoai.Add(new ComboboxItem { ValueItem = LoaiNganhThamDinh.CTNTD.ToString(), DisplayItem = LoaiNganhThamDinh.TEN_CTNTD });
            SelectedPhanLoai = DataPhanLoai.FirstOrDefault();
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            _demandVoucherDetails = new ObservableCollection<ExpertiseImportModel>();
            _importErrors = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            _nsSktMucLucs = _sktMucLucService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(x => x.SKyHieu).ToList();
            _mergeItems = new List<SktMucLucModel>();
            _importedMlskt = new ObservableCollection<SktMucLucModel>();
            _existedMlskt = new ObservableCollection<SktMucLucModel>(_mapper.Map<ObservableCollection<SktMucLucModel>>(_nsSktMucLucs));
            if (DataPhanLoai != null && DataPhanLoai.Count > 0)
            {
                SelectedPhanLoai = DataPhanLoai.FirstOrDefault();
            }
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(DemandVoucherDetails));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(SelectedPhanLoai));
        }

        private Expression<Func<DonVi, bool>> CreatePredicate()
        {
            var session = _sessionService.Current;
            var predicate = PredicateBuilder.True<DonVi>();
            //List<string> nguoiDungDonVi = GetListNguoiDungDonVi();
            if (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == LoaiNganhThamDinh.CTCTCDN.ToString())
            {
                if (CheckDonViCondition())
                {
                    predicate = predicate.And(x => (x.Loai == LoaiDonVi.ROOT) && x.BCoNSNganh);
                }
                else
                {
                    predicate = predicate.And(x => false);
                }
            }
            else
            {
                if (CheckDonViThamDinhCondition())
                {
                    predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO) && x.BCoNSNganh);
                }
                else
                {
                    predicate = predicate.And(x => (x.Loai == LoaiDonVi.ROOT) && x.BCoNSNganh);
                }
            }
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            return predicate;
        }

        public bool CheckDonViThamDinhCondition()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO));
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            List<DonVi> listDonVi = _nSDonViService.FindByCondition(predicate).ToList();
            if (listDonVi != null && listDonVi.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckDonViCondition()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT));
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            List<DonVi> listDonVi = _nSDonViService.FindByCondition(predicate).ToList();
            if (listDonVi != null && listDonVi.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadLoaiNganSach()
        {
            DataLoaiNganSach = new ObservableCollection<ComboboxItem>();
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSSD_Key, DisplayItem = VoucherType.NSSD_Value });
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSBD_Key, DisplayItem = VoucherType.NSBD_Value });
            SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault();
        }

        public void LoadDonVi()
        {
            ListDonVi = new ObservableCollection<ComboboxItem>();
            var predicate = CreatePredicate();
            IEnumerable<DonVi> listDonVi = _nSDonViService.FindByCondition(predicate).ToList();
            List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => !string.IsNullOrEmpty(n.SGiaTri)).ToList();
            listDonVi = listDonVi.Where(n => listDanhMuc.Where(t => !string.IsNullOrEmpty(t.SGiaTri)).Select(x => x.SGiaTri).ToList().Contains(n.IIDMaDonVi)).ToList();
            ListDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);
            if (ListDonVi != null && ListDonVi.Count > 0)
            {
                DonViSelected = ListDonVi.ElementAt(0);
            }
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
            int rowIndex = _demandVoucherDetails.IndexOf(SelectedItem);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            MessageBoxHelper.Info(message);
        }

        private List<DonVi> GetListDonVi()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT) && x.BCoNSNganh);
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            List<DonVi> listDonVi = _donViService.FindByCondition(predicate).ToList();
            return listDonVi;
        }

        private void OnProcessFile()
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(FileName))
            {
                message = Resources.MsgChooseFileImport;
                goto ShowError;
            }

            if (DonViSelected == null)
            {
                message = Resources.MsgCheckDonVi;
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
                _importErrors = new List<ImportErrorItem>();
                ImportResult<ExpertiseImportModel> _voucherDetailResult;
                //xử lý chứng từ chi tiết
                if (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem.EndsWith(LoaiNganhThamDinh.CTNTD.ToString()))
                {
                    ImportResult<ExpertiseNTDImportModel> detailNtd = _importService.ProcessDataUnique<ExpertiseNTDImportModel>(FileName);
                    _voucherDetailResult = _mapper.Map<ImportResult<ExpertiseImportModel>>(detailNtd);
                }
                else
                {
                    _voucherDetailResult = _importService.ProcessDataUnique<ExpertiseImportModel>(FileName);
                }
                _demandVoucherDetailProcess = new List<ExpertiseImportModel>();

                //xác định cha con trong data import
                foreach (var item in _voucherDetailResult.Data)
                {
                    if (!item.IsErrorMLNS)
                    {
                        var mlns = _nsSktMucLucs.Where(x => x.SKyHieu == item.KyHieu).FirstOrDefault();
                        if (mlns != null)
                            item.BHangCha = mlns.BHangCha;
                    }
                    var childs = _voucherDetailResult.Data.Where(x => x.KyHieu.Contains(item.KyHieu) && (x != item && x.MaDonVi == item.MaDonVi)).ToList();
                    if (childs.Count > 0)
                        item.BHangCha = true;
                }

                //kiểm tra loại ngân sách con thỏa mãn điều kiện để import
                List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => !string.IsNullOrEmpty(n.SGiaTri)).ToList();
                foreach (var item in _voucherDetailResult.Data)
                {
                    if (!item.ImportStatus && item.IsErrorMLNS)
                    {
                        var parents = _demandVoucherDetailProcess.Where(x => item.KyHieu.Contains(x.KyHieu) && x != item).ToList();
                        if (parents.Count > 0)
                        {
                            int columnIndexOrigin = 0;
                            ExpertiseImportModel parent = new ExpertiseImportModel();
                            foreach (var p in parents)
                            {
                                int maxColumn = p.KyHieu.Split("-").Count();
                                if (maxColumn > columnIndexOrigin)
                                {
                                    columnIndexOrigin = maxColumn;
                                    parent = p;
                                }
                            }
                            int columnIndexImport = item.KyHieu.Split("-").Count();
                            if (columnIndexOrigin < columnIndexImport)
                            {
                                if (parent.ListKyHieuChild == null)
                                    parent.ListKyHieuChild = new List<string>();
                                parent.ListKyHieuChild.Add(item.KyHieu);
                                item.KyHieuParent = parent.KyHieu;

                                var parentOrigin = _nsSktMucLucs.Where(x => x.SKyHieu == parent.KyHieu).FirstOrDefault();
                                if (parentOrigin != null && !parentOrigin.BHangCha)
                                {
                                    item.IsWarning = true;
                                }
                                if (parent.IsWarning)
                                    item.IsWarning = true;
                            }
                        }
                    }
                    _demandVoucherDetailProcess.Add(item);
                }

                CalculateData();
                _demandVoucherDetails = new ObservableCollection<ExpertiseImportModel>(_demandVoucherDetailProcess);
                foreach (var item in _demandVoucherDetails)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(ExpertiseImportModel.ImportStatus)
                            && args.PropertyName != nameof(ExpertiseImportModel.KyHieu)
                            && args.PropertyName != nameof(ExpertiseImportModel.IsErrorMLNS))
                        {
                            var voucherDetail = (ExpertiseImportModel)sender;
                            int rowIndex = _demandVoucherDetails.IndexOf(voucherDetail);
                            var listError = _importService.ValidateItem<ExpertiseImportModel>(voucherDetail, rowIndex);
                            if (listError.Count > 0)
                            {
                                List<string> errors = listError.Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
                                string message = string.Join(Environment.NewLine, errors);
                                MessageBoxHelper.Info(message);
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

                    int index = _demandVoucherDetails.IndexOf(item);

                    if (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == LoaiNganhThamDinh.CTCTCDN.ToString())
                    {
                        List<DonVi> listDonVi = GetListDonVi();
                        listDanhMuc = listDanhMuc.Where(n => listDonVi.Select(n => n.IIDMaDonVi).ToList().Contains(n.SGiaTri)).ToList();
                        if (!(listDanhMuc.Select(m => m.IIDMaDanhMuc).ToList().Contains(item.Nganh) || item.BHangCha))
                        {
                            item.ImportStatus = false;
                            if (_importErrors.Where(n => n.Row == index && n.Error == Resources.MsgImportErrorNganh).ToList().Count == 0)
                                _importErrors.Add(new ImportErrorItem { Row = index, Error = Resources.MsgImportErrorNganh });
                        }
                    }
                    else if (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == LoaiNganhThamDinh.CTNTD.ToString())
                    {
                        listDanhMuc = listDanhMuc.Where(n => n.SGiaTri == DonViSelected.ValueItem).ToList();
                        if (!(listDanhMuc.Select(m => m.IIDMaDanhMuc).ToList().Contains(item.Nganh) || item.BHangCha))
                        {
                            item.ImportStatus = false;
                            if (_importErrors.Where(n => n.Row == index && n.Error == Resources.MsgImportErrorNganh).ToList().Count == 0)
                                _importErrors.Add(new ImportErrorItem { Row = index, Error = Resources.MsgImportErrorNganh });
                        }
                    }
                }


                OnPropertyChanged(nameof(DemandVoucherDetails));
                if (_voucherDetailResult.ImportErrors.Count > 0)
                    _importErrors.AddRange(_voucherDetailResult.ImportErrors);

                if (_demandVoucherDetails == null || _demandVoucherDetails.Count <= 0)
                {
                    MessageBoxHelper.Warning(Resources.MsgFileImportEmpty);
                }
                if (DemandVoucherDetails.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    MessageBoxHelper.Warning(Resources.AlertDataError);
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                if (ex is WrongReportException)
                {
                    System.Windows.MessageBox.Show(Resources.WrongReportFormat, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckFormatFileImport);
                }
            }
        }

        private void CalculateData()
        {
            foreach (var item in _demandVoucherDetailProcess.Where(x => x.BHangCha))
            {
                if (!string.IsNullOrEmpty(item.TuChi))
                    item.TuChi = "0";
                if (!string.IsNullOrEmpty(item.SuDungTonKho))
                    item.TuChi = "0";
                if (!string.IsNullOrEmpty(item.ChiDacThuNganhPhanCap))
                    item.TuChi = "0";

            }
            foreach (var item in _demandVoucherDetailProcess.Where(x => !x.BHangCha))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(ExpertiseImportModel currentItem, ExpertiseImportModel selfItem)
        {
            List<ExpertiseImportModel> parents = new List<ExpertiseImportModel>();
            if (!currentItem.IsWarning)
                parents = _demandVoucherDetailProcess.Where(x => currentItem.KyHieu.Contains(x.KyHieu)
                           && x.BHangCha && currentItem.KyHieu != x.KyHieu).OrderByDescending(x => x.KyHieu).ToList();
            else parents = _demandVoucherDetailProcess.Where(x => currentItem.KyHieu.Contains(x.KyHieu)
                && currentItem.KyHieu != x.KyHieu).OrderByDescending(x => x.KyHieu).ToList();
            if (parents.Count > 0)
            {
                int columnIndexOrigin = 0;
                ExpertiseImportModel parent = new ExpertiseImportModel();
                foreach (var p in parents)
                {
                    int maxColumn = p.KyHieu.Split("-").Count();
                    if (maxColumn > columnIndexOrigin)
                    {
                        columnIndexOrigin = maxColumn;
                        parent = p;
                    }
                }
                double parentValue, childValue = 0;
                if (Double.TryParse(parent.TuChi, out parentValue) && Double.TryParse(selfItem.TuChi, out childValue))
                    parent.TuChi = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.SuDungTonKho, out parentValue) && Double.TryParse(selfItem.SuDungTonKho, out childValue))
                    parent.SuDungTonKho = (parentValue + childValue).ToString();
                if (Double.TryParse(parent.ChiDacThuNganhPhanCap, out parentValue) && Double.TryParse(selfItem.ChiDacThuNganhPhanCap, out childValue))
                    parent.ChiDacThuNganhPhanCap = (parentValue + childValue).ToString();

                CalculateParent(parent, selfItem);
            }
            else return;
        }

        private bool CheckCreateCTC(Guid id)
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            if (id != Guid.Empty)
            {
                predicate = predicate.And(x => x.Id != id);
            }
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTCTCDN);
            NsSktNganhThamDinh chungTuDeNghi = _nganhThamDinhService.FindByCondition(predicate).FirstOrDefault();
            if (chungTuDeNghi != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckCreateNTD(string idDonVi, Guid id)
        {
            var loaiChungTu = SelectedLoaiNganSach != null ? int.Parse(SelectedLoaiNganSach.ValueItem) : 1;
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            if (id != Guid.Empty)
            {
                predicate = predicate.And(x => x.Id != id);
            }
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD);
            predicate = predicate.And(x => x.ILoaiChungTu == loaiChungTu);
            predicate = predicate.And(x => x.IIdMaDonVi == idDonVi);
            NsSktNganhThamDinh chungTuDeNghi = _nganhThamDinhService.FindByCondition(predicate).FirstOrDefault();
            if (chungTuDeNghi != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void OnSaveData()
        {
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;

            if (DonViSelected == null)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }
            if (SelectedPhanLoai.ValueItem == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTCTCDN.ToString())
            {
                if (!CheckCreateCTC(Guid.Empty))
                {
                    MessageBoxHelper.Warning(Resources.MsgExistVoucher);
                    return;
                }
            }
            else if (SelectedPhanLoai.ValueItem == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD.ToString())
            {
                if (!CheckCreateNTD(DonViSelected.ValueItem, Guid.Empty))
                {
                    MessageBoxHelper.Warning(Resources.MsgExistVoucherAgency);
                    return;
                }
            }

            int soChungTuIndex = _nganhThamDinhService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
            string SoChungTu = "THD-" + soChungTuIndex.ToString("D3");
            int NamLamViec = _sessionInfo.YearOfWork;

            _nsSktMucLucs = _sktMucLucService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork).ToList();
            NsSktNganhThamDinh chungTu = new NsSktNganhThamDinh();
            chungTu.SSoChungTu = SoChungTu;
            chungTu.ISoChungTuIndex = soChungTuIndex;
            chungTu.IIdMaDonVi = DonViSelected.ValueItem;
            chungTu.DNgayChungTu = NgayChungTu == null ? DateTime.Now : NgayChungTu;
            chungTu.SMoTa = MoTa;
            if (SelectedPhanLoai != null && !string.IsNullOrEmpty(SelectedPhanLoai.ValueItem))
            {
                chungTu.ILoai = int.Parse(SelectedPhanLoai.ValueItem);
            }
            if (SelectedLoaiNganSach != null)
            {
                chungTu.ILoaiChungTu = int.Parse(SelectedLoaiNganSach.ValueItem);
            }
            chungTu.INamLamViec = NamLamViec;
            chungTu.INamNganSach = yearOfBudget;
            chungTu.IIdMaNguonNganSach = budgetSource;
            chungTu.DNgayTao = DateTime.Now;
            chungTu.SNguoiTao = _sessionInfo.Principal;
            _nganhThamDinhService.Add(chungTu);

            List<NsSktNganhThamDinhChiTiet> chungTuChiTiets = new List<NsSktNganhThamDinhChiTiet>();
            List<ExpertiseImportModel> listDetailImport = _demandVoucherDetails.Where(x => x.ImportStatus && !x.IsWarning && _nsSktMucLucs.Any(y => y.SKyHieu == x.KyHieu && !y.BHangCha)).ToList();
            foreach (var item in listDetailImport)
            {
                NsSktMucLuc mucLuc = _nsSktMucLucs.FirstOrDefault(x => x.SKyHieu.Equals(item.KyHieu) && x.INamLamViec == NamLamViec && x.ITrangThai == StatusType.ACTIVE);
                if (item.KyHieu.Equals("") || mucLuc == null)
                {
                    continue;
                }
                NsSktNganhThamDinhChiTiet itemChiTiet = new NsSktNganhThamDinhChiTiet();
                itemChiTiet.IIdCtnganhThamDinh = chungTu.Id;
                itemChiTiet.IIdMaDonVi = item.MaDonVi;
                itemChiTiet.STenDonVi = item.TenDonVi;
                itemChiTiet.IIdMucLuc = mucLuc.IIDMLSKT;
                itemChiTiet.SM = mucLuc.SM;
                itemChiTiet.SMoTa = mucLuc.SMoTa;
                //itemChiTiet.FTuChiCtc = item.TuChiCTCValue;
                itemChiTiet.FTuChi = item.TuChiValue;
                if (_selectedLoaiNganSach != null && _selectedLoaiNganSach.ValueItem.Equals(VoucherType.NSBD_Key))
                {
                    itemChiTiet.FSuDungTonKho = item.SuDungTonKhoValue;
                    itemChiTiet.FChiDacThuNganhPhanCap = item.ChiDacThuNganhPhanCapValue;
                }
                itemChiTiet.INamLamViec = NamLamViec;
                itemChiTiet.DNgayTao = DateTime.Now;
                itemChiTiet.INamNganSach = yearOfBudget;
                itemChiTiet.IIdMaNguonNganSach = budgetSource;
                itemChiTiet.SNguoiTao = _sessionInfo.Principal;
                chungTuChiTiets.Add(itemChiTiet);
            }
            _thamDinhChiTietService.AddRange(chungTuChiTiets);
            // update tong chung tu
            UpdateDemandExpertiseTotal(chungTu, chungTuChiTiets);
            MessageBoxHelper.Info(Resources.MsgImportSuccess);
            ExpertiseModel voucher = _mapper.Map<ExpertiseModel>(chungTu);
            DialogHost.CloseDialogCommand.Execute(voucher, null);
            SavedAction?.Invoke(voucher);
        }

        public void UpdateDemandExpertiseTotal(NsSktNganhThamDinh chungTu, List<NsSktNganhThamDinhChiTiet> lstChiTiet)
        {
            if (chungTu != null)
            {
                var sktThamDinhUpdate = _nganhThamDinhService.FindByCondition(x => x.Id.Equals(chungTu.Id)).FirstOrDefault();
                if (sktThamDinhUpdate != null)
                {
                    sktThamDinhUpdate.FTongTuChiCtc = lstChiTiet.Sum(item => item.FTuChi);
                    sktThamDinhUpdate.FTongTuChiNganh = lstChiTiet.Sum(item => item.FTuChi);
                    sktThamDinhUpdate.FTongHienVatCtc = lstChiTiet.Sum(item => item.FTuChi);
                    sktThamDinhUpdate.FTongHienVatNganh = lstChiTiet.Sum(item => item.FTuChi);
                    _nganhThamDinhService.Update(sktThamDinhUpdate);
                }
            }
        }

        private void OnAddMLSKT()
        {
            TabIndex = ImportTabIndex.MLNS;
            SktMucLucModel importItem = new SktMucLucModel();
            if (ImportedMlskt.Contains(importItem))
                return;

            bool isImportGroup = false;
            if (!_importedMlskt.Any(x => x.SKyHieu.Contains(SelectedItem.KyHieu))
                || !_existedMlskt.Any(x => x.SKyHieu.Contains(SelectedItem.KyHieu)))
            {
                List<ExpertiseImportModel> data = new List<ExpertiseImportModel>();
                data.Add(SelectedItem);
                if (SelectedItem.BHangCha)
                    data.AddRange(_demandVoucherDetailProcess.Where(x => SelectedItem.ListKyHieuChild.Contains(x.KyHieu)).ToList());
                else
                {
                    var it = _demandVoucherDetailProcess.Where(x => x.KyHieu == SelectedItem.KyHieuParent)
                        .FirstOrDefault();
                    if (it != null)
                    {
                        data.Add(it);
                    }
                }
                if (data.Count > 1)
                    isImportGroup = true;
                data = data.OrderBy(x => x.KyHieu).ToList();
                foreach (var item in data)
                {
                    if (!_nsSktMucLucs.Any(x => x.SKyHieu == item.KyHieu))
                        _importedMlskt.Add(new SktMucLucModel()
                        {
                            IIDMLSKT = Guid.NewGuid(),
                            SKyHieu = item.KyHieu,
                            SNg = item.Nganh,
                            SSTT = item.STT,
                            SMoTa = item.MoTa,
                            SNGCha = item.NganhCha,
                            BHangCha = item.BHangCha,
                            ITrangThai = StatusType.ACTIVE,
                            INamLamViec = _sessionInfo.YearOfWork,
                            SM = "",
                            SLoaiNhap = "1,2",
                            IsModified = true
                        });
                }
            }
            foreach (SktMucLucModel model in _importedMlskt.ToList())
            {
                SktMucLucModel parent = null;
                if (isImportGroup && !model.BHangCha)
                    parent = _importedMlskt.Where(x => model.SKyHieu.Contains(x.SKyHieu) && model.SKyHieu != x.SKyHieu).FirstOrDefault();
                if (parent == null)
                    parent = FindParent(model, _existedMlskt);
                if (parent != null)
                {
                    int index = _existedMlskt.IndexOf(parent);
                    _existedMlskt.Insert(index + 1, model);
                    //_importedMlns.Remove(model);
                    model.IIDMLSKTCha = parent.IIDMLSKT;
                    model.BHangCha = model.BHangCha;
                    model.ITrangThai = 1;
                    model.DNguoiTao = _sessionInfo.Principal;
                    model.SM = "";
                    model.DNgayTao = DateTime.Now;
                    _mergeItems.Add(model);
                    OnPropertyChanged(nameof(IsEnableSaveMLSKT));
                }
            }
            _importedMlskt = new ObservableCollection<SktMucLucModel>();
            OnPropertyChanged(nameof(ExistedMlskt));
            OnPropertyChanged(nameof(ImportedMlskt));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnSelectionChanged();
        }

        private void OnSelectionChanged()
        {
            foreach (var i in _existedMlskt)
            {
                i.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(ModelBase.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsEnabledMergeBtn));
                        OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                    }
                };
            }
            foreach (var i in _importedMlskt.Where(x => x.IsModified))
            {
                i.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(ModelBase.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsEnabledMergeBtn));
                        OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
                    }
                };
            }
        }

        public SktMucLucModel FindParent(SktMucLucModel model, IEnumerable<SktMucLucModel> ExistedMlns)
        {
            IEnumerable<SktMucLucModel> ancestors = _existedMlskt.Where(i => !Guid.Empty.Equals(i.Id) && !model.SKyHieu.Equals(i.SKyHieu) &&
                                                                            model.SKyHieu.StartsWith(i.SKyHieu + "-") && model.INamLamViec == i.INamLamViec)
                .OrderByDescending(i => i.SKyHieu.Length);
            return ancestors.FirstOrDefault();
        }

        private void OnMerge()
        {
            if (SelectedParent == null)
                return;
            int index = _existedMlskt.ToList().FindIndex(x => x.IsSelected);
            _mergeItems = _importedMlskt.Where(i => i.IsSelected && i.IsModified).ToList();
            foreach (var item in _mergeItems)
            {
                item.IIDMLSKTCha = SelectedParent.IIDMLSKT;
                item.BHangCha = false;
                item.ITrangThai = 1;
                item.DNguoiTao = _sessionInfo.Principal;
                item.DNgayTao = DateTime.Now;
            }

            List<SktMucLucModel> nsMuclucSktModels = _existedMlskt.ToList();
            nsMuclucSktModels.InsertRange(index + 1, _mergeItems);
            _existedMlskt = new ObservableCollection<SktMucLucModel>(nsMuclucSktModels);
            _importedMlskt = new ObservableCollection<SktMucLucModel>(ImportedMlskt.Where(i => !i.IsSelected || !i.IsModified));
            OnPropertyChanged(nameof(ExistedMlskt));
            OnPropertyChanged(nameof(ImportedMlskt));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnPropertyChanged(nameof(IsEnableSaveMLSKT));
            OnSelectionChanged();
        }

        private void OnUnMerge()
        {
            IEnumerable<SktMucLucModel> unmergeItems = _existedMlskt.Where(i => i.IsSelected && i.IsModified).ToList();
            foreach (var item in unmergeItems)
            {
                _mergeItems.Remove(item);
            }
            List<SktMucLucModel> nsMuclucSktModels = ImportedMlskt.ToList();
            nsMuclucSktModels.AddRange(unmergeItems);
            _importedMlskt = new ObservableCollection<SktMucLucModel>(nsMuclucSktModels);
            _existedMlskt = new ObservableCollection<SktMucLucModel>(_existedMlskt.Where(i => !i.IsSelected || !i.IsModified));
            OnPropertyChanged(nameof(ExistedMlskt));
            OnPropertyChanged(nameof(ImportedMlskt));
            OnPropertyChanged(nameof(IsEnabledMergeBtn));
            OnPropertyChanged(nameof(IsEnabledUnmergeCommand));
            OnPropertyChanged(nameof(IsEnableSaveMLSKT));
            OnSelectionChanged();
        }

        private void OnSaveMLSKT()
        {
            var result = MessageBoxHelper.Confirm(Resources.ConfirmAddMLNS);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    List<NsSktMucLuc> listMLSKT = _mapper.Map<List<NsSktMucLuc>>(_mergeItems);
                    listMLSKT.Select(n => { n.SSttBC = n.SKyHieu.Replace("-", string.Empty); return n; }).ToList();
                    _sktMucLucService.AddRange(listMLSKT);
                    _existedMlskt.Where(x => x.IsModified).Select(x => { x.IsModified = false; x.IsSelected = false; return x; }).ToList();
                    _mergeItems = new List<SktMucLucModel>();
                    OnPropertyChanged(nameof(ExistedMlskt));
                    OnPropertyChanged(nameof(IsEnableSaveMLSKT));
                    MessageBoxHelper.Info(Resources.MsgSaveDone);

                    foreach (var item in listMLSKT)
                    {
                        var importItem = _demandVoucherDetails.Where(x => x.KyHieu == item.SKyHieu).FirstOrDefault();
                        var listError = _importService.ValidateItem<ExpertiseImportModel>(importItem, _demandVoucherDetails.IndexOf(importItem));
                        if (listError.Count == 0)
                        {
                            importItem.ImportStatus = true;
                            importItem.IsErrorMLNS = false;
                            TabIndex = ImportTabIndex.Data;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBoxHelper.Warning(Resources.MsgSaveError);
                }
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}

