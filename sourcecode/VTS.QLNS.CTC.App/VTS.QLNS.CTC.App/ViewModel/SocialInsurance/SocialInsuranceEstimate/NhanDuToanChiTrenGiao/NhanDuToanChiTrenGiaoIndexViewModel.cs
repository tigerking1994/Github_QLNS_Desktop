using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao.ImportNdtBHXH;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao.ImportNdtBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao
{
    public class NhanDuToanChiTrenGiaoIndexViewModel : GridViewModelBase<BhDtctgBHXHModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly INdtctgBHXHService _ndtctgBHXHService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly INdtctgBHXHChiTietService _ndtctgBHXHChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IBhdtcnpbMapBHXHService _dtChungTuMapService;
        private readonly IBhDtCtctKPQLService _bhDtCtctKPQLService;
        private SessionInfo _sessionInfo;

        private ICollectionView _bhDanhMucLoaiChiModelView;
        private ImportNdtctgBHXH _importNdtctgBHXH;
        public override Type ContentType => typeof(NhanDuToanChiTrenGiaoIndex);
        public override string GroupName => MenuItemContants.GROUP_CHI;
        public override string Description => "Danh sách đợt nhận dự toán chi";
        public override string Name => "Nhận DT chi trên giao";

        public string ComboboxDisplayMemberPath => nameof(SelecteBhDanhMucLoaiChi.STenDanhMucLoaiChi);
        private List<BhDtctgBHXHModel> _lstChungTuOrigin;
        public List<BhDtctgBHXHModel> LstChungTuOrigin
        {
            get => _lstChungTuOrigin;
            set
            {
                SetProperty(ref _lstChungTuOrigin, value);
            }
        }
        private ICollectionView _bhChungTuModelsView;
        public RelayCommand SelectionChangedCommand { get; }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        private bool _iDelete;
        public bool IsDelete
        {
            get => _iDelete;
            set => SetProperty(ref _iDelete, value);
        }
        private bool _isEnableLock;
        public bool IsEnableLock
        {
            get => _isEnableLock;
            set
            {
                SetProperty(ref _isEnableLock, value);
                var listItemSelected = Items.Where(n => n.Selected).ToList();
                if (listItemSelected.Count > 0)
                {
                    _isEnableLock = true;
                }
                else
                {
                    var lstSelectedKhoa = listItemSelected.Where(x => x.BIsKhoa).ToList();
                    var lstSelectedMo = listItemSelected.Where(x => !x.BIsKhoa).ToList();
                    if (lstSelectedKhoa.Count() > 0 && lstSelectedMo.Count() > 0)
                    {
                        _isEnableLock = true;
                    }
                    else if (lstSelectedKhoa.Count() > 0)
                    {
                        IsLock = true;
                    }
                    else if (lstSelectedMo.Count() > 0)
                    {
                        IsLock = false;
                    }
                }
            }

        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        private bool _isExportAggregateData;
        public bool IsExportAggregateData
        {
            get => _isExportAggregateData;
            set => SetProperty(ref _isExportAggregateData, value);
        }

        private BhDtctgBHXHModel _selectedBhNhanDuToanChiModel;
        public BhDtctgBHXHModel SelectedBhNhanDuToanChiModel
        {
            get => _selectedBhNhanDuToanChiModel;
            set
            {
                SetProperty(ref _selectedBhNhanDuToanChiModel, value);
                if (_selectedBhNhanDuToanChiModel != null)
                {
                    IsEdit = true;
                    IsDelete = true;
                    IsEnableLock = true;
                    IsExportAggregateData = true;

                }
                else
                {
                    IsEdit = false;
                    IsDelete = false;
                    IsEnableLock = false;
                    IsExportAggregateData = false;
                }

                OnPropertyChanged(nameof(IsExportAggregateData));
            }
        }

        private BhDanhMucLoaiChiModel _selecteBhDanhMucLoaiChi;
        public BhDanhMucLoaiChiModel SelecteBhDanhMucLoaiChi
        {
            get => _selecteBhDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selecteBhDanhMucLoaiChi, value);
                SearchData();
            }
        }

        private ObservableCollection<BhDanhMucLoaiChiModel> _bhDanhMucLoaiChiItems;
        public ObservableCollection<BhDanhMucLoaiChiModel> BhDanhMucLoaiChiItems
        {
            get => _bhDanhMucLoaiChiItems;
            set => SetProperty(ref _bhDanhMucLoaiChiItems, value);
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items);
                    OnPropertyChanged();
                }
            }
        }

        private void SelectAll(bool select, IEnumerable<BhDtctgBHXHModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        public RelayCommand PrintCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }

        public NhanDuToanChiTrenGiaoDialogViewModel NhanDuToanChiTrenGiaoDialogViewModel { get; set; }
        public NhanDuToanChiTrenGiaoDetailViewModel NhanDuToanChiTrenGiaoDetailViewModel { get; set; }
        public PrintReportNhanDuToanChiTrenGiaoViewModel PrintReportNhanDuToanChiTrenGiaoViewModel { get; set; }
        public ImportNdtctgBHXHViewModel ImportNdtctgBHXHViewModel { get; set; }
        public NhanDuToanChiTrenGiaoIndexViewModel(
            ILog logger,
            IMapper mapper,
            INdtctgBHXHService ndtctgBHXHService,
            ISessionService sessionService,
            INsDonViService donViService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            INdtctgBHXHChiTietService ndtctgBHXHChiTietService,
            IBhdtcnpbMapBHXHService bhdtcnpbMapBHXHService,
            ISysAuditLogService log,
            IDanhMucService danhMucService,
            IExportService exportService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            NhanDuToanChiTrenGiaoDialogViewModel nhanDuToanChiTrenGiaoDialogViewModel,
            NhanDuToanChiTrenGiaoDetailViewModel nhanDuToanChiTrenGiaoDetailViewModel,
            PrintReportNhanDuToanChiTrenGiaoViewModel printReportNhanDuToanChiTrenGiaoViewModel,
            ImportNdtctgBHXHViewModel importNdtctgBHXHViewModel,
            IBhDtCtctKPQLService bhDtCtctKPQLService)
        {
            _logger = logger;
            _mapper = mapper;
            _ndtctgBHXHService = ndtctgBHXHService;
            _ndtctgBHXHChiTietService = ndtctgBHXHChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _dtChungTuMapService = bhdtcnpbMapBHXHService;
            _sessionService = sessionService;
            _donViService = donViService;
            _danhMucService = danhMucService;
            _exportService = exportService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _log = log;

            NhanDuToanChiTrenGiaoDialogViewModel = nhanDuToanChiTrenGiaoDialogViewModel;
            NhanDuToanChiTrenGiaoDetailViewModel = nhanDuToanChiTrenGiaoDetailViewModel;
            PrintReportNhanDuToanChiTrenGiaoViewModel = printReportNhanDuToanChiTrenGiaoViewModel;
            ImportNdtctgBHXHViewModel = importNdtctgBHXHViewModel;

            NhanDuToanChiTrenGiaoDialogViewModel.ParentPage = this;
            NhanDuToanChiTrenGiaoDetailViewModel.ParentPage = this;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            RefreshCommand = new RelayCommand(OnRefresh);
            DeleteCommand = new RelayCommand(OnDelete);
            PrintCommand = new RelayCommand(obj => OnOpenReport(obj));
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            _bhDtCtctKPQLService = bhDtCtctKPQLService;
            //LockCommand = new RelayCommand()

        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadDanhMucLoaiChi();
            LoadData();
            NhanDuToanChiTrenGiaoDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;

        }

        public void LoadData()
        {
            _sessionInfo = _sessionService.Current;
            IsEdit = false;
            IsDelete = false;
            LoadNdtctgBHXH();
        }

        private void LoadDanhMucLoaiChi()
        {
            var listDmLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
            BhDanhMucLoaiChiItems = _mapper.Map<ObservableCollection<BhDanhMucLoaiChiModel>>(listDmLoaiChi);
            _bhDanhMucLoaiChiModelView = CollectionViewSource.GetDefaultView(BhDanhMucLoaiChiItems);
            _bhDanhMucLoaiChiModelView.SortDescriptions.Add(new SortDescription(nameof(BhDanhMucLoaiChiModel.SMaLoaiChi),
                ListSortDirection.Ascending));
            _bhDanhMucLoaiChiModelView.SortDescriptions.Add(new SortDescription(nameof(BhDanhMucLoaiChiModel.STenDanhMucLoaiChi),
                ListSortDirection.Ascending));
        }

        public void LoadNdtctgBHXH()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            var listChungTu = _ndtctgBHXHService.FindByYear(yearOfWork);
            Items = _mapper.Map<ObservableCollection<BhDtctgBHXHModel>>(listChungTu);

            _bhChungTuModelsView = CollectionViewSource.GetDefaultView(Items);
        }

        protected override void OnRefresh()
        {
            LoadNdtctgBHXH();
        }


        private void SearchData()
        {
            LoadNdtctgBHXH();
        }


        protected override void OnDelete()
        {
            if (SelectedBhNhanDuToanChiModel == null) return;
            //check quyền được chỉnh sửa
            if (SelectedBhNhanDuToanChiModel.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedBhNhanDuToanChiModel.SNguoiTao));
                return;
            }

            //check quyền được chỉnh sửa
            var dtNhanPhanBoMaps = _dtChungTuMapService.FindByIdNhanDuToan(SelectedItem.Id).ToList();
            if (dtNhanPhanBoMaps.Count > 0)
            {
                MessageBox.Show(Resources.AlertDeleteSocialInsuranceVoucher, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuKhtBHXH, SelectedBhNhanDuToanChiModel.SSoChungTu, SelectedBhNhanDuToanChiModel.DNgayChungTu);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void OnDeleteHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            DateTime dtNow = DateTime.Now;
            if (SelectedBhNhanDuToanChiModel != null)
            {
                var yearOfWork = _sessionInfo.YearOfWork;
                var chungtu = _ndtctgBHXHService.FindById(SelectedBhNhanDuToanChiModel.Id);
                if (chungtu != null)
                {
                    _ndtctgBHXHService.Delete(chungtu);

                    //Xóa chi tiết chứng từ BHXH
                    var lstDetail = _ndtctgBHXHChiTietService.FindByCondition(SelectedBhNhanDuToanChiModel.Id).ToList();
                    var lstDtCtctKPQL = _bhDtCtctKPQLService.FindByCondition(SelectedBhNhanDuToanChiModel.Id).ToList();
                    _ndtctgBHXHChiTietService.RemoveRange(lstDetail);
                    _bhDtCtctKPQLService.RemoveRange(lstDtCtctKPQL);
                    OnRefresh();
                }
            }
        }

        private void OnSelectedChange(object obj)
        {
            SelectedBhNhanDuToanChiModel = (BhDtctgBHXHModel)obj;
            if (SelectedBhNhanDuToanChiModel != null && SelectedBhNhanDuToanChiModel.BIsKhoa)
            {
                IsLock = true;
                IsDelete = false;
                IsEdit = false;
            }
            else
            {
                IsLock = false;
                IsDelete = true;
                IsEdit = true;
            }

        }
        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }


        protected override void OnAdd()
        {
            NhanDuToanChiTrenGiaoDialogViewModel.Name = "THÊM MỚI";
            NhanDuToanChiTrenGiaoDialogViewModel.Description = "Thêm mới đợt nhân phân bổ dự toán";
            NhanDuToanChiTrenGiaoDialogViewModel.IsEdit = false;
            NhanDuToanChiTrenGiaoDialogViewModel.BhDtctgBHXHModel = new BhDtctgBHXHModel();
            NhanDuToanChiTrenGiaoDialogViewModel.Init();
            NhanDuToanChiTrenGiaoDialogViewModel.SavedAction = obj =>
            {
                var dtctgChungTu = (BhDtctgBHXHModel)obj;

                this.LoadData();
                OpenDetailDialog(dtctgChungTu);
            };
            var exportView = new NhanDuToanChiTrenGiaoDialog() { DataContext = NhanDuToanChiTrenGiaoDialogViewModel };
            DialogHost.Show(exportView, DemandCheckScreen.ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            BhDtctgBHXHModel model = new BhDtctgBHXHModel();
            var bhxh = _ndtctgBHXHService.FindById(SelectedBhNhanDuToanChiModel.Id);
            _mapper.Map(bhxh, model);

            NhanDuToanChiTrenGiaoDialogViewModel.Name = "CẬP NHẬT";
            NhanDuToanChiTrenGiaoDialogViewModel.Description = "Cập nhật nhận dự toán trên giao";
            NhanDuToanChiTrenGiaoDialogViewModel.BhDtctgBHXHModel = model;
            NhanDuToanChiTrenGiaoDialogViewModel.IsEdit = true;
            NhanDuToanChiTrenGiaoDialogViewModel.Init();
            NhanDuToanChiTrenGiaoDialogViewModel.SavedAction = obj =>
            {
                var khtChungTu = (BhDtctgBHXHModel)obj;
                this.LoadData();
                OpenDetailDialog(khtChungTu);
            };
            var exportView = new NhanDuToanChiTrenGiaoDialog() { DataContext = NhanDuToanChiTrenGiaoDialogViewModel };
            DialogHost.Show(exportView, DemandCheckScreen.ROOT_DIALOG);
        }

        private void OpenDetailDialog(BhDtctgBHXHModel bhDtctgBHXHModel, params bool[] isNew)
        {
            NhanDuToanChiTrenGiaoDetailViewModel.Model = bhDtctgBHXHModel;
            NhanDuToanChiTrenGiaoDetailViewModel.Init();
            var view = new NhanDuToanChiTrenGiaoDetail() { DataContext = NhanDuToanChiTrenGiaoDetailViewModel };
            view.ShowDialog();
        }

        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhDtctgBHXHModel)obj, false);
        }

        private void OnOpenReport(object param)
        {

            var ndtcheckPrintType = (NdtcheckPrintType)((int)param);
            object content;
            switch (ndtcheckPrintType)
            {
                case NdtcheckPrintType.NDTCCTNS:
                    PrintReportNhanDuToanChiTrenGiaoViewModel.NdtcheckPrintType = ndtcheckPrintType;
                    PrintReportNhanDuToanChiTrenGiaoViewModel.Name = "In nhận dự toán chi trên giao";
                    PrintReportNhanDuToanChiTrenGiaoViewModel.Description = "In nhận dự toán chi trên giao";
                    PrintReportNhanDuToanChiTrenGiaoViewModel.Init();

                    content = new PrintReportNhanDuToanChiTrenGiao
                    {
                        DataContext = PrintReportNhanDuToanChiTrenGiaoViewModel
                    };

                    break;


                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, DivisionScreen.ROOT_DIALOG, null, null);
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            //OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnableLock));
        }
        private void OnImportData()
        {
            try
            {
                ImportNdtctgBHXHViewModel.Init();
                ImportNdtctgBHXHViewModel.SavedAction = obj =>
                {
                    _importNdtctgBHXH.Close();
                    this.LoadData();
                    //OnPropertyChanged(nameof(IsCensorship));
                    this.OnRefresh();
                    IsAllItemsSelected = false;
                    OpenDetailDialog((BhDtctgBHXHModel)obj);
                };

                _importNdtctgBHXH = new ImportNdtctgBHXH { DataContext = ImportNdtctgBHXHViewModel };
                _importNdtctgBHXH.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        protected override void OnLockUnLock()
        {
            if (IsLock)
            {
                //chỉ có đơn vị cha mới được mở khóa chứng từ
                var listItemKhoa = string.Join(", ", Items.Where(n => n.IsChecked && n.BIsKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBox.Show(string.Format("Đồng chí không được mở khóa chứng từ {0} do không có quyền tổng hợp", listItemKhoa), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                var listItemInvalid = string.Join(", ", Items.Where(n => n.Selected && !n.BIsKhoa && n.SNguoiTao != _sessionInfo.Principal).Select(n => n.SSoChungTu));
                if (!string.IsNullOrEmpty(listItemInvalid))
                {
                    MessageBox.Show(string.Format("Đồng chí không có quyền khóa chứng từ {0} do không phải người tạo", listItemInvalid), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult result = MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            var lstChungTuChon = Items.Where(n => n.Selected).ToList();

            if (result == MessageBoxResult.Yes)
            {
                foreach (var SelectedItemElement in lstChungTuChon)
                {
                    var dtctg = _ndtctgBHXHService.FindById(SelectedItemElement.Id);
                    if (dtctg != null)
                    {
                        dtctg.BIsKhoa = !SelectedItemElement.BIsKhoa;
                        _ndtctgBHXHService.Update(dtctg);
                        SelectedItemElement.BIsKhoa = !SelectedItemElement.BIsKhoa;
                        IsLock = !IsLock;
                        IsEdit = !IsLock;
                        IsDelete = !IsLock;
                        OnPropertyChanged(nameof(IsLock));
                        OnPropertyChanged(nameof(IsEdit));
                        OnPropertyChanged(nameof(IsDelete));
                    }
                }

                MessageBoxHelper.Info(msgDone);
            }

        }

        private string GetValueSLNS(string SLNS)
        {
            var lstSLNS = SLNS.Split(",");
            string sLNS = SLNS;
            if (lstSLNS.Contains(LNSValue.LNS_9010001) || lstSLNS.Contains(LNSValue.LNS_9010002))
            {
                sLNS += "," + LNSValue.LNS_9_901;
            }

            if (lstSLNS.Contains(LNSValue.LNS_9010003) || lstSLNS.Contains(LNSValue.LNS_9010004)
                || lstSLNS.Contains(LNSValue.LNS_9010006) || lstSLNS.Contains(LNSValue.LNS_9010008)
                || lstSLNS.Contains(LNSValue.LNS_9010009) || lstSLNS.Contains(LNSValue.LNS_9010010))
            {
                sLNS += "," + LNSValue.LNS_9;
            }
            return sLNS;
        }

        private void OnExportAggregateData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                string chiTietToi = "NG";
                double? Total = 0;
                DanhMuc danhMucChiTietToi = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                if (danhMucChiTietToi != null)
                    chiTietToi = danhMucChiTietToi.SGiaTri;
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ExportResult> results = new List<ExportResult>();

                string templateFile = "rpt_BH_NhanDuToanChi.xlsx";

                var namLamViec = _sessionService.Current.YearOfWork;

                var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == namLamViec);


                List<DonVi> listNsDonVi = new List<DonVi>();
                listNsDonVi = _donViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Join(StringUtils.COMMA, new string[] { LoaiDonVi.NOI_BO, LoaiDonVi.ROOT })).ToList();

                if (listNsDonVi.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    var predicate = PredicateBuilder.True<DonVi>();
                    predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                    //predicate = predicate.And(x => x.Loai == SoChungTuType.EstimateDivision.ToString());
                    predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);

                    listNsDonVi = _donViService.FindByCondition(predicate).ToList();
                }
                var itemsExport = Items.Where(x => x.Selected);

                foreach (var item in itemsExport)
                {
                    foreach (var itemMaLoaiChi in item.SMaLoaiChi.Split(','))
                    {
                        string lstSLNS = _bhDanhMucLoaiChiService.FindAll().Where(x => x.SMaLoaiChi == itemMaLoaiChi && x.INamLamViec == item.INamLamViec).FirstOrDefault().SLNS;
                        string sLNS = GetValueSLNS(lstSLNS);
                        var dataExportDetail = _ndtctgBHXHChiTietService.GetListNhanDuToanChiTrenGiaoChiTiet(item.Id, sLNS, _sessionService.Current.YearOfWork, item.IID_MaDonVi,item.ILoaiDotNhanPhanBo).ToList();
                        var donvi = listNsDonVi.Where(x => x.IIDMaDonVi == item.IID_MaDonVi).FirstOrDefault();

                        var listMucLucNganSach = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => sLNS.Split(",").Contains(x.SLNS)).OrderBy(x => x.SXauNoiMa).ToList();
                        //lstDataPrent.AddRange(lstDataChildbyDonVi);

                        List<BhDtctgBHXHChiTietModel> lstData = new List<BhDtctgBHXHChiTietModel>();
                        lstData = _mapper.Map(dataExportDetail, lstData);

                        Total = lstData?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienTuChi);
                        lstData = lstData.Where(x => (x.FTienTuChi ?? 0) != 0).OrderBy(x => x.SXauNoiMa).ToList();

                        var data = new Dictionary<string, object>();
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        data.Add("TitleFirst", $"DỰ TOÁN CHI NGÂN SÁCH NĂM {_sessionService.Current.YearOfWork}");
                        data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");

                        data.Add("HeaderTenDonVi", $"Đơn vị: {donvi?.IIDMaDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{donvi?.TenDonVi}");
                        data.Add("TenDonVi", $"{donvi?.IIDMaDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{donvi?.TenDonVi}");
                        data.Add("SoChungTu", item.SSoChungTu);
                        data.Add("NgayChungTu", DateUtils.Format(item.DNgayChungTu));
                        data.Add("SoQuyetDinh", item.SSoQuyetDinh);
                        data.Add("NgayQuyetDinh", DateUtils.Format(item.DNgayQuyetDinh));
                        data.Add("MoTa", item.SMoTa);


                        data.Add("Items", lstData);
                        data.Add("MLNS", listMucLucNganSach);


                        data.Add("Total", string.Format(StringUtils.FORMAT_ZERO, Total));
                        string templateFileName = Path.Combine(ExportPrefix.PATH_BH_NDT, templateFile);
                        string fileNamePrefix = string.Format("{0}_{1}_{2}", item.SSoChungTu, item.SSoQuyetDinh, StringUtils.ConvertVN(donvi?.TenDonVi));
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhDtctgBHXHChiTietModel, BhDmMucLucNganSach>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
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

        private void CalculateData(List<BhDtctgBHXHChiTietModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    //x.FTienHienVat = 0;
                    x.FTienTuChi = 0;
                    //x.FTongTien = 0;
                    return x;
                }).ToList();

            var dictByMlns = listData.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = listData.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (var item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }
        }

        private void CalculateParent(Guid? idParent, BhDtctgBHXHChiTietModel item, Dictionary<Guid?, BhDtctgBHXHChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienTuChi = model.FTienTuChi.GetValueOrDefault(0) + item.FTienTuChi.GetValueOrDefault(0);

            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
        }

    }
}
