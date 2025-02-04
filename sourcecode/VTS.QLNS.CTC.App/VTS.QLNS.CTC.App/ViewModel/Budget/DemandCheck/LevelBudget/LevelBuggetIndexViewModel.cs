using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.LevelBudget;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.LevelBudget;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck
{
    public class LevelBuggetIndexViewModel : ViewModelBase
    {
        private ILbChungTuService _chungTuService;
        private IMapper _mapper;
        private ILbChungTuChiTietService _chungTuChiTietService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private ILbChungTuChiTietPhanCapService _soLieuChiTietPhanCapService;
        private ISessionService _sessionService;
        private IExportService _exportService;
        private INsDonViService _nsDonViService;
        private VTS.QLNS.CTC.App.View.Budget.DemandCheck.LevelBudget.LevelBuggetDetail view;
        private readonly ILog _logger;
        private ICollectionView _dataIndexFilter;
        private IDanhMucService _danhMucService;
        private LevelBuggetImport _importView;

        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Phân cấp ngân sách ngành";
        public override Type ContentType => typeof(View.Budget.DemandCheck.LevelBudget.LevelBuggetIndex);
        public override PackIconKind IconKind => PackIconKind.ViewList;
        public override string Title => "Danh sách chứng từ phân cấp";
        public override string Description => "Danh sách chứng từ phân cấp";

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.LevelBuggetModel> _dataChungTu;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.LevelBuggetModel> DataChungTu
        {
            get => _dataChungTu;
            set => SetProperty(ref _dataChungTu, value);
        }

        private VTS.QLNS.CTC.App.Model.LevelBuggetModel _selectedChungTu;
        public VTS.QLNS.CTC.App.Model.LevelBuggetModel SelectedChungTu
        {
            get => _selectedChungTu;
            set
            {
                SetProperty(ref _selectedChungTu, value);
                OnPropertyChanged(nameof(IsEdit));
                OnPropertyChanged(nameof(IsLock));
                OnPropertyChanged(nameof(IsEnableLock));
            }
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                return DataChungTu.All(item => item.Selected);
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, DataChungTu);
                    OnPropertyChanged();
                }
            }
        }


        private ObservableCollection<ComboboxItem> _dataNguonNganSach;
        public ObservableCollection<ComboboxItem> DataNguonNganSach
        {
            get => _dataNguonNganSach;
            set => SetProperty(ref _dataNguonNganSach, value);
        }

        private ComboboxItem _selectedNguonNganSach;
        public ComboboxItem SelectedNguonNganSach
        {
            get => _selectedNguonNganSach;
            set
            {
                if (SetProperty(ref _selectedNguonNganSach, value) && _selectedNguonNganSach != null && _dataIndexFilter != null)
                {
                    _dataIndexFilter.Refresh();
                    OnPropertyChanged(nameof(IsNganSachQP));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _lockStatus = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> LockStatus
        {
            get => _lockStatus;
            set => SetProperty(ref _lockStatus, value);
        }

        private ComboboxItem _lockStatusSelected;

        public ComboboxItem LockStatusSelected
        {
            get => _lockStatusSelected;
            set
            {
                SetProperty(ref _lockStatusSelected, value);
                LoadData();
            }
        }

        public bool IsEnableButtonExport => DataChungTu != null && DataChungTu.Where(n => n.Selected).Count() > 0;
        public bool IsNganSachQP => (_selectedNguonNganSach != null && _selectedNguonNganSach.ValueItem == NguonNganSach.NSQP.ToString());

        private static void SelectAll(bool select, IEnumerable<VTS.QLNS.CTC.App.Model.LevelBuggetModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        public bool IsLock => DataChungTu.Where(x => x.Selected).All(x => x.IsLocked) || LockStatusSelected.ValueItem == "1";
        public bool IsEnableLock => DataChungTu.Where(x => x.Selected).Select(x => x.IsLocked).Distinct().Count() == 1;
        public bool IsEdit => SelectedChungTu != null && !SelectedChungTu.IsLocked;

        public RelayCommand SortCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand RefeshCommand { get; }
        public RelayCommand LockUnLockCommand { get; }
        public RelayCommand ShowPopupAddCommand { get; }
        public RelayCommand ShowPopupEditCommand { get; }
        public RelayCommand SelectionDoubleClickCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ExportDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }

        public LevelBuggetDialogViewModel LevelBuggetDialogViewModel { get; }
        public LevelBuggetDetailViewModel LevelBuggetDetailViewModel { get; }
        public LevelBuggetImportViewModel LevelBuggetImportViewModel { get; }

        public LevelBuggetIndexViewModel(ILbChungTuService chungTuService,
            IMapper mapper,
            ISessionService sessionService,
            ILbChungTuChiTietService chungTuChiTietService,
            INsMucLucNganSachService mucLucNganSachService,
            IExportService exportService,
            ILog logger,
            INsDonViService nsDonViService,
            IDanhMucService danhMucService,
            ILbChungTuChiTietPhanCapService soLieuChiTietPhanCapService,
            LevelBuggetDialogViewModel levelBuggetDialogViewModel,
            LevelBuggetDetailViewModel levelBuggetDetailViewModel,
            LevelBuggetImportViewModel levelBuggetImportViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _chungTuService = chungTuService;
            _sessionService = sessionService;
            _chungTuChiTietService = chungTuChiTietService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;
            _nsDonViService = nsDonViService;
            _danhMucService = danhMucService;
            _soLieuChiTietPhanCapService = soLieuChiTietPhanCapService;

            LevelBuggetDialogViewModel = levelBuggetDialogViewModel;
            LevelBuggetDetailViewModel = levelBuggetDetailViewModel;
            LevelBuggetImportViewModel = levelBuggetImportViewModel;
            LevelBuggetDialogViewModel.ParentPage = this;
            SelectionDoubleClickCommand = new RelayCommand(o => OnShowDetail((LevelBuggetModel)o));
            ShowPopupAddCommand = new RelayCommand(o => OnShowPopupAdd());
            ShowPopupEditCommand = new RelayCommand(o => OnShowPopupEdit());
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock());
            DeleteCommand = new RelayCommand(o => OnDelete());
            RefeshCommand = new RelayCommand(o => OnRefesh());
            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            ExportDataCommand = new RelayCommand(obj => OnExportData());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
        }

        private void OnImportData()
        {
            try
            {
                LevelBuggetImportViewModel.Init();
                _importView = new LevelBuggetImport { DataContext = LevelBuggetImportViewModel };
                _importView.Show();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_PCNSN, ExportFileName.EPT_NS_PHANCAPNGANSACHNGANH_CHUNGTU);

                    List<LevelBuggetModel> listExport = DataChungTu.Where(x => x.Selected).ToList();
                    foreach (LevelBuggetModel item in listExport)
                    {
                        DonVi donViChild = _nsDonViService.FindByIdDonVi(item.IdDonVi, _sessionService.Current.YearOfWork);
                        DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);

                        List<LevelBuggetDetailModel> listDetail = GetDetailDataExport(item);
                        List<LevelBuggetDetailChildModel> listDetailChild = GetDataPhanCap(item);
                        int namLamViec = _sessionService.Current.YearOfWork;
                        List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(namLamViec).ToList();
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Header2", donViChild != null ? donViChild.TenDonVi : string.Empty);
                        data.Add("Header1", donViParent != null ? donViParent.TenDonVi : string.Empty);
                        data.Add("NamLamViec", _sessionService.Current.YearOfWork);
                        data.Add("Items", listDetail);
                        data.Add("Items2", listDetailChild);
                        data.Add("MLNS", mucLucNganSaches);

                        var xlsFile = _exportService.Export<LevelBuggetDetailModel, LevelBuggetDetailChildModel, NsMucLucNganSach>(templateFileName, data);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        string fileNamePrefix = string.Format("rptPhanCapNganSachNganh_{0}", item.SoChungTu);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
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

        private List<LevelBuggetDetailChildModel> GetDataPhanCap(LevelBuggetModel chungTu)
        {
            List<NsNganhChungTuChiTiet> chiTiet = _chungTuChiTietService.FindByChungTuId(chungTu.Id);
            string xauNoiMa = string.Empty;
            string listXauNoiMa = string.Empty;
            string idChiTiet = string.Empty;
            if (chiTiet != null && chiTiet.Count() > 0)
            {
                List<string> listParent = new List<string>();
                if (chungTu != null && chungTu.LoaiChungTu.HasValue && chungTu.LoaiChungTu.ToString() == NguonNganSach.NSQP.ToString())
                {
                    xauNoiMa = string.Join(",", chiTiet.Select(n => { n.SXauNoiMa.Replace("1040100", "1020100"); return n; }).ToList());


                    foreach (NsNganhChungTuChiTiet item in chiTiet)
                    {
                        listParent.AddRange(StringUtils.SplitXauNoiMaParent(item.SXauNoiMa.Replace("1040100", "1020100")));
                    }
                    listParent = listParent.Distinct().ToList();

                    listXauNoiMa = string.Join(",", listParent);
                }
                else
                {
                    xauNoiMa = string.Join(",", chiTiet.Select(n => n.SXauNoiMa).ToList());
                    foreach (NsNganhChungTuChiTiet item in chiTiet)
                    {
                        listParent.AddRange(StringUtils.SplitXauNoiMaParent(item.SXauNoiMa));
                    }
                    listParent = listParent.Distinct().ToList();
                    listXauNoiMa = string.Join(",", listParent);
                }
                idChiTiet = string.Join(",", chiTiet.Select(n => n.Id.ToString()).ToList());
                List<LbChiTietPhanCapQuery> data = new List<LbChiTietPhanCapQuery>();
                data = _soLieuChiTietPhanCapService.GetSoLieuChiTietPhanCapExport(_sessionService.Current.YearOfWork, xauNoiMa, listXauNoiMa, idChiTiet).ToList();

                List<Model.LevelBuggetDetailChildModel> result = _mapper.Map<List<Model.LevelBuggetDetailChildModel>>(data);

                CalculateDataPhanCap(ref result);
                return result;
            }
            else
            {
                return new List<LevelBuggetDetailChildModel>();
            }
        }

        private void CalculateDataPhanCap(ref List<LevelBuggetDetailChildModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x => { x.PhanCap = 0; return x; }).ToList();
            foreach (var item in listData.Where(x => !x.IsHangCha && (x.PhanCap != 0)))
            {
                CalculateParentPhanCap(ref listData, item, item);
            }
        }

        private void CalculateParentPhanCap(ref List<LevelBuggetDetailChildModel> listData, LevelBuggetDetailChildModel currentItem, LevelBuggetDetailChildModel selfItem)
        {
            var parentItem = listData.Where(x => x.MucLucID == currentItem.MucLucParentId).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.PhanCap += selfItem.PhanCap;
            CalculateParentPhanCap(ref listData, parentItem, selfItem);
        }

        private List<LevelBuggetDetailModel> GetDetailDataExport(LevelBuggetModel chungTu)
        {
            List<LevelBuggetDetailModel> resultDetail = new List<LevelBuggetDetailModel>();
            AllocationDetailCriteria _searchCondition = new AllocationDetailCriteria
            {
                VoucherId = chungTu.Id.ToString(),
                LNS = chungTu.Lns,
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                Type = chungTu.LoaiChungTu.Value.ToString(),
                BudgetSource = chungTu.NguonNganSach.HasValue ? chungTu.NguonNganSach.Value : 0,
                AgencyId = chungTu.IdDonVi,
                VoucherDate = chungTu.NgayChungTu,
                UserName = _sessionService.Current.Principal
            };

            List<LbChungTuChiTietQuery> data = _chungTuChiTietService.FindChungTuChiTietByCondition(_searchCondition).ToList();
            if (data != null && data.Count > 0)
            {
                data.Select(n => { n.IdDonVi = chungTu.IdDonVi; n.TenDonVi = chungTu.TenDonVi; return n; }).ToList();
            }

            if (chungTu != null && chungTu.LoaiChungTu.HasValue && chungTu.LoaiChungTu.ToString() == NguonNganSach.NSQP.ToString())
            {
                List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => n.SGiaTri == chungTu.IdDonVi).ToList();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    List<LbChungTuChiTietQuery> listChild = data.Where(n => listDanhMuc.Select(m => m.IIDMaDanhMuc).ToList().Contains(n.Ng)).ToList();
                    List<string> listParentXauNoiMa = StringUtils.GetListXauNoiMaParent(listChild.Select(n => n.XauNoiMa).ToList());
                    data = data.Where(n => listParentXauNoiMa.Contains(n.XauNoiMa)).ToList();
                }
            }
            resultDetail = _mapper.Map<List<Model.LevelBuggetDetailModel>>(data);
            CalculateData(ref resultDetail);
            return resultDetail.Where(n => (n.TuChi != 0 || n.PhanCap != 0 || n.ChuaPhanCap != 0 || n.HangNhap != 0 || n.HangMua != 0)).ToList();
        }

        private void CalculateData(ref List<LevelBuggetDetailModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x => { x.TuChi = 0; x.PhanCap = 0; x.ChuaPhanCap = 0; x.HangNhap = 0; x.HangMua = 0; return x; }).ToList();
            foreach (var item in listData.Where(x => x.IsFilter && !x.IsHangCha && !x.IsDeleted && (x.TuChi != 0 || x.PhanCap != 0 || x.ChuaPhanCap != 0 || x.HangNhap != 0 || x.HangMua != 0)))
            {
                CalculateParent(ref listData, item, item);
            }
        }

        private void CalculateParent(ref List<LevelBuggetDetailModel> listData, LevelBuggetDetailModel currentItem, LevelBuggetDetailModel selfItem)
        {
            var parentItem = listData.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.PhanCap += selfItem.PhanCap;
            parentItem.ChuaPhanCap += selfItem.ChuaPhanCap;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            CalculateParent(ref listData, parentItem, selfItem);
        }

        private async void OnShowPopupAdd()
        {
            try
            {
                LevelBuggetDialogViewModel.Model = new Model.LevelBuggetModel();
                LevelBuggetDialogViewModel.IsEditProcess = false;
                LevelBuggetDialogViewModel.Init();
                LevelBuggetDialogViewModel.SavedAction = obj =>
                {
                    LevelBuggetModel objValue = (LevelBuggetModel)obj;
                    if (objValue != null && objValue.LoaiChungTu.HasValue && objValue.LoaiChungTu == NguonNganSach.NSQP)
                    {
                        SelectedNguonNganSach = DataNguonNganSach.FirstOrDefault(n => n.ValueItem == NguonNganSach.NSQP.ToString());
                    }
                    else
                    {
                        SelectedNguonNganSach = DataNguonNganSach.FirstOrDefault();
                    }
                    this.OnRefesh();
                    OnShowDetail((LevelBuggetModel)obj);
                };
                var view = new LevelBuggetDialog
                {
                    DataContext = LevelBuggetDialogViewModel
                };
                var result = await DialogHost.Show(view, "RootDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadNganSach()
        {
            DataNguonNganSach = new ObservableCollection<ComboboxItem>();
            DataNguonNganSach.Add(new ComboboxItem { ValueItem = NguonNganSach.NSK.ToString(), DisplayItem = NguonNganSach.TEN_NSK });
            DataNguonNganSach.Add(new ComboboxItem { ValueItem = NguonNganSach.NSQP.ToString(), DisplayItem = NguonNganSach.TEN_NSQQ });
            SelectedNguonNganSach = DataNguonNganSach.FirstOrDefault();
        }

        private void LoadLockStatus()
        {
            var lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        private async void OnShowPopupEdit()
        {
            try
            {
                if (SelectedChungTu != null)
                {
                    if (SelectedChungTu.UserCreator != _sessionService.Current.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedChungTu.UserCreator));
                        return;
                    }

                    this.LevelBuggetDialogViewModel.Model = SelectedChungTu;
                    LevelBuggetDialogViewModel.IsEditProcess = true;
                    LevelBuggetDialogViewModel.Init();
                    LevelBuggetDialogViewModel.SavedAction = obj =>
                    {
                        LevelBuggetModel objValue = (LevelBuggetModel)obj;
                        if (objValue != null && objValue.LoaiChungTu.HasValue && objValue.LoaiChungTu == NguonNganSach.NSQP)
                        {
                            SelectedNguonNganSach = DataNguonNganSach.FirstOrDefault(n => n.ValueItem == NguonNganSach.NSQP.ToString());
                        }
                        else
                        {
                            SelectedNguonNganSach = DataNguonNganSach.FirstOrDefault();
                        }
                        this.OnRefesh();
                        //OnShowDetail((LevelBuggetModel)obj);
                    };
                    var view = new LevelBuggetDialog
                    {
                        DataContext = LevelBuggetDialogViewModel
                    };
                    var result = await DialogHost.Show(view, "RootDialog", null, null);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnLockUnLock()
        {
            try
            {
                if (IsLock)
                {
                    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                    {
                        MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                        return;
                    }
                }
                else
                {
                    if (SelectedChungTu.UserCreator != _sessionService.Current.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, SelectedChungTu.UserCreator));
                        return;
                    }
                }
                string msgConfirm = string.Format(IsLock ? Resources.MsgUnLock : Resources.MsgLock, Environment.NewLine, Environment.NewLine);
                string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;

                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(msgConfirm); ;
                if (dialogResult == MessageBoxResult.Yes)
                {
                    OnLockHandler(msgDone);
                    LockStatusSelected = LockStatus.ElementAt(0);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnLockHandler(string msgDone)
        {
            bool isLock = IsLock;
            DataChungTu.Where(x => x.Selected).Select(x => x.IsLocked = !isLock).ToList();
            var listChecked = _mapper.Map<ObservableCollection<NsNganhChungTu>>(DataChungTu.Where(x => x.Selected));
            _chungTuService.UpdateRange(listChecked);
            //_chungTuService.LockOrUnLock(SelectedChungTu.Id, !SelectedChungTu.IsLocked);
            MessageBoxHelper.Info(msgDone);
            OnRefesh();
        }

        private void OnDelete()
        {
            try
            {
                if (SelectedChungTu.UserCreator != _sessionService.Current.Principal)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedChungTu.UserCreator));
                    return;
                }
                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(Resources.MsgDeleteRecord);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    if (_chungTuChiTietService.CheckExitsByChungTuId(SelectedChungTu.Id))
                    {
                        MessageBoxResult dialogConfirm = MessageBoxHelper.ConfirmCancel(string.Format("{0}{1}{2}{3}{4}{5}{6}",
                                                                    Resources.MsgConfirmDelete, Environment.NewLine, Resources.MsgConfirmDeleteYes, Environment.NewLine,
                                                                    Resources.MsgConfirmDeleteNo, Environment.NewLine, Resources.MsgConfirmDeleteCancel));
                        if (dialogConfirm == MessageBoxResult.Yes)
                        {
                            DeleteChungTuChiTiet(SelectedChungTu.Id);
                            _chungTuService.Delete(SelectedChungTu.Id);
                            OnRefesh();
                        }
                        else if (dialogConfirm == MessageBoxResult.No)
                        {
                            _chungTuService.UpdateStatusDisable(SelectedChungTu.Id);
                            OnRefesh();
                        }
                    }
                    else
                    {
                        DeleteChungTuChiTiet(SelectedChungTu.Id);
                        _chungTuService.Delete(SelectedChungTu.Id);
                        OnRefesh();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void DeleteChungTuChiTiet(Guid idChungTu)
        {
            var predicate = PredicateBuilder.True<NsNganhChungTuChiTiet>();
            predicate = predicate.And(x => x.IIdCtnganh == idChungTu);
            List<NsNganhChungTuChiTiet> list = _chungTuChiTietService.FindByCondition(predicate).ToList();
            if (list != null && list.Count > 0)
            {
                foreach (NsNganhChungTuChiTiet item in list)
                {
                    _chungTuChiTietService.Delete(item.Id);
                }
            }
        }

        private void OnRefesh()
        {
            LoadData();
        }

        public void OnShowDetail(LevelBuggetModel itemDetail)
        {
            try
            {
                if (itemDetail == null)
                    return;
                LevelBuggetDetailViewModel.Model = itemDetail;
                LevelBuggetDetailViewModel.Init();
                view = new VTS.QLNS.CTC.App.View.Budget.DemandCheck.LevelBudget.LevelBuggetDetail
                {
                    DataContext = LevelBuggetDetailViewModel
                };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void RefreshAfterClosePopup(object sender, EventArgs e)
        {
            try
            {
                view.Close();
                OnRefesh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void Init()
        {
            try
            {
                LoadNganSach();
                LoadLockStatus();
                LoadData();
                LevelBuggetDetailViewModel.ClosePopup += RefreshAfterClosePopup;
                LevelBuggetImportViewModel.OpenDetail += OpenDetailAfterImport;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OpenDetailAfterImport(object sender, EventArgs e)
        {
            try
            {
                _importView.Close();
                OnRefesh();
                OnShowDetail((LevelBuggetModel)sender);
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
                var currentIdDonVi = _sessionService.Current.IdDonVi;
                IEnumerable<LbChungTuQuery> data = _chungTuService.FindByCondition(_sessionService.Current.YearOfWork, _sessionService.Current.Budget,
                    _sessionService.Current.IdDonVi, _sessionService.Current.YearOfBudget, _sessionService.Current.Principal);

                if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
                {
                    data = data.Where(x => x.IsLocked).ToList();
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
                {
                    data = data.Where(x => x.IsLocked == false).ToList();
                }

                DataChungTu = _mapper.Map<ObservableCollection<Model.LevelBuggetModel>>(data);
                _dataIndexFilter = CollectionViewSource.GetDefaultView(DataChungTu);
                _dataIndexFilter.Filter = DataFilter;
                if (DataChungTu != null && DataChungTu.Count > 0)
                {
                    SelectedChungTu = DataChungTu.FirstOrDefault();
                }
                foreach (LevelBuggetModel model in DataChungTu)
                {
                    model.PropertyChanged += DetailModel_PropertyChanged;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool DataFilter(object obj)
        {
            bool result = true;
            var item = (LevelBuggetModel)obj;

            if (SelectedNguonNganSach != null)
                result = result && item.LoaiChungTu.HasValue && item.LoaiChungTu.Value.ToString() == SelectedNguonNganSach.ValueItem.ToString();
            return result;
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(LevelBuggetModel.Selected))
            {
                OnPropertyChanged(nameof(IsEnableButtonExport));
                OnPropertyChanged(nameof(IsEnableLock));
                OnPropertyChanged(nameof(IsLock));
            }
        }
    }
}
