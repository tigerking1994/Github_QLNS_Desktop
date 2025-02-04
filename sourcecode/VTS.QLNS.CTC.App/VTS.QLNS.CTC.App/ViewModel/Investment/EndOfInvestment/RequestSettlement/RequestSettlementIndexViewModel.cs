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
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.AnnualSettlement;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.RequestSettlement;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using System.Windows;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.RequestSettlement
{
    public class RequestSettlementIndexViewModel : GridViewModelBase<DeNghiQuyetToanModel>
    {
        private IVdtDaDuAnService _iVdtDaDuAnService;
        private IMapper _mapper;
        private ICollectionView _dataIndexFilter;
        private readonly ISessionService _sessionService;
        private IVdtDeNghiQuyetToanService _iVdtDeNghiQuyetToanService;
        private readonly ILog _logger;
        private IExportService _exportService;
        private IVdtDaDuToanService _duToanService;
        private IApproveProjectService _quyetDinhDauTuService;
        private readonly IVdtQtDeNghiQuyetToanNguonVonService _vdtQtDeNghiQuyetToanNguonVonService;
        private VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.RequestSettlement.RequestSettlementDetail view;
        private IVdtQtDeNghiQuyetToanChiTietService _qtDeNghiQuyetToanChiTietService;
        private readonly INsDonViService _nsDonViService;

        public override string FuncCode => NSFunctionCode.INVESTMENT_END_OF_INVESTMENT_REQUEST_SETTLEMENT_INDEX;
        //public override string Name => "Quản lý đề nghị quyết toán dự án hoàn thành";
        public override string GroupName => MenuItemContants.GROUP_FINISH_SETTLEMENT;
        public override string Name => "Đề nghị quyết toán dự án hoàn thành";
        public override string Title => "Đề nghị quyết toán dự án hoàn thành";
        public override string Description => "Danh đề nghị quyết toán dự án hoàn thành";
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.RequestSettlement.RequestSettlementIndex);
        public bool IsLock => SelectedItem != null && SelectedItem.IsLock && VoucherTabIndex == VoucherTabIndex.VOUCHER;
        public bool IsEdit => SelectedItem != null && !SelectedItem.IsLock && VoucherTabIndex == VoucherTabIndex.VOUCHER;

        private string _soBaoCaoSearch;
        public string SoBaoCaoSearch
        {
            get => _soBaoCaoSearch;
            set => SetProperty(ref _soBaoCaoSearch, value);
        }

        private string _tenDuAnSearch;
        public string TenDuAnSearch
        {
            get => _tenDuAnSearch;
            set => SetProperty(ref _tenDuAnSearch, value);
        }

        private string _maDuAnSearch;
        public string MaDuAnSearch
        {
            get => _maDuAnSearch;
            set => SetProperty(ref _maDuAnSearch, value);
        }

        private double _giaTriDeNghiTuSearch;
        public double GiaTriDeNghiTuSearch
        {
            get => _giaTriDeNghiTuSearch;
            set => SetProperty(ref _giaTriDeNghiTuSearch, value);
        }

        private double _giaTriDeNghiDenSearch;
        public double GiaTriDeNghiDenSearch
        {
            get => _giaTriDeNghiDenSearch;
            set => SetProperty(ref _giaTriDeNghiDenSearch, value);
        }

        private string _chuDauTuSearch;
        public string ChuDauTuSearch
        {
            get => _chuDauTuSearch;
            set => SetProperty(ref _chuDauTuSearch, value);
        }

        public RequestSettlementDialogViewModel RequestSettlementDialogViewModel { get; set; }
        public RequestSettlementDetailViewModel RequestSettlementDetailViewModel { get; set; }
        public RequestSettlementImportViewModel RequestSettlementImportViewModel { get; set; }
        public ReportFormSettlementViewModel ReportFormSettlementViewModel { get; set; }
        public RequestSettlementTongHopDialogViewModel RequestSettlementTongHopDialogViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand RefeshCommand { get; }
        public RelayCommand ImportCommand { get; }
        public RelayCommand SelectionDoubleClickCommand { get; }
        public RelayCommand PrintCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand LockUnLockCommand { get; }
        public RelayCommand TongHopCommand { get; }

        private ObservableCollection<DeNghiQuyetToanModel> _ListDeNghiTongHop;
        public ObservableCollection<DeNghiQuyetToanModel> ListDeNghiTongHop 
        {
            get => _ListDeNghiTongHop;
            set => SetProperty(ref _ListDeNghiTongHop, value);
        }

        private DeNghiQuyetToanModel _selectedTongHopItem;
        public DeNghiQuyetToanModel SelectedTongHopItem 
        {
            get => _selectedTongHopItem;
            set => SetProperty(ref _selectedTongHopItem, value);
        }

        private VoucherTabIndex _voucherTabIndex;
        public VoucherTabIndex VoucherTabIndex 
        {
            get => _voucherTabIndex;
            set => SetProperty(ref _voucherTabIndex, value);
        }

        public RequestSettlementIndexViewModel(IVdtDaDuAnService iVdtDaDuAnService,
                                               IVdtDeNghiQuyetToanService iVdtDeNghiQuyetToanService,
                                               IMapper mapper,
                                               ISessionService sessionService,
                                               IExportService exportService,
                                               IVdtDaDuToanService duToanService,
                                               INsDonViService nsDonViService,
                                               IApproveProjectService quyetDinhDauTuService,
                                               IVdtQtDeNghiQuyetToanNguonVonService vdtQtDeNghiQuyetToanNguonVonService,
                                               IVdtQtDeNghiQuyetToanChiTietService qtDeNghiQuyetToanChiTietService,
                                               ILog logger,
                                               RequestSettlementDetailViewModel requestSettlementDetailViewModel,
                                               RequestSettlementImportViewModel requestSettlementImportViewModel,
                                               ReportFormSettlementViewModel reportFormSettlementViewModel,
                                               RequestSettlementDialogViewModel requestSettlementDialogViewModel,
                                               RequestSettlementTongHopDialogViewModel requestSettlementTongHopDialogViewModel)
        {
            _iVdtDaDuAnService = iVdtDaDuAnService;
            RequestSettlementDetailViewModel = requestSettlementDetailViewModel;
            RequestSettlementDialogViewModel = requestSettlementDialogViewModel;
            RequestSettlementImportViewModel = requestSettlementImportViewModel;
            RequestSettlementTongHopDialogViewModel = requestSettlementTongHopDialogViewModel;

            ReportFormSettlementViewModel = reportFormSettlementViewModel;
            _iVdtDeNghiQuyetToanService = iVdtDeNghiQuyetToanService;
            _sessionService = sessionService;
            _logger = logger;
            _exportService = exportService;
            _duToanService = duToanService;
            _quyetDinhDauTuService = quyetDinhDauTuService;
            _vdtQtDeNghiQuyetToanNguonVonService = vdtQtDeNghiQuyetToanNguonVonService;
            _qtDeNghiQuyetToanChiTietService = qtDeNghiQuyetToanChiTietService;
            _nsDonViService = nsDonViService;

            SelectionDoubleClickCommand = new RelayCommand(obj =>
            {
                if (VoucherTabIndex == VoucherTabIndex.VOUCHER)
                    OnShowDetail((DeNghiQuyetToanModel)obj, true);
                else
                    OnSelectionDoubleClick(null);
            });
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            RefeshCommand = new RelayCommand(obj => LoadData());
            ImportCommand = new RelayCommand(obj => OnImport());
            PrintCommand = new RelayCommand(obj => OnShowPopupReport());
            ExportCommand = new RelayCommand(obj => OnExport());
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock());
            TongHopCommand = new RelayCommand(o => OnTongHopDeNghiTT());
            _mapper = mapper;
        }

        public override void Init()
        {
            try
            {
                VoucherTabIndex = VoucherTabIndex.VOUCHER;
                LoadData();
                RequestSettlementDetailViewModel.ClosePopup += RefreshAfterClosePopup;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSelectionDoubleClick(object obj)
        {
            //base.OnSelectionDoubleClick(obj);
            //var data = (VdtTtDeNghiThanhToanModel)obj;
            //OnOpenDisbursementPaymentDetail(data);
            if (SelectedTongHopItem.BTongHop.HasValue && SelectedTongHopItem.BTongHop.Value && VoucherTabIndex == VoucherTabIndex.VOUCHER_AGREGATE)
            {
                RequestSettlementTongHopDialogViewModel.Model = SelectedTongHopItem;
                RequestSettlementTongHopDialogViewModel.Init();
                RequestSettlementTongHopDialogViewModel.IsDisabled = true;
                RequestSettlementTongHopDialogViewModel.VoucherAgregates = new ObservableCollection<DeNghiQuyetToanModel>(Items.Where(t => SelectedTongHopItem.Id.Equals(t.ParentId)));
                var v = new RequestSettlementTongHopDialog { DataContext = RequestSettlementTongHopDialogViewModel };
                DialogHost.Show(v, "RootDialog");
            }
        }

        private void OnLockUnLock()
        {
            try
            {
                if (SelectedItem == null)
                    return;
                if (IsLock)
                {
                    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                    {
                        System.Windows.MessageBox.Show(Resources.MsgRoleUnlock, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                else
                {
                    if (SelectedItem.NguoiTao != _sessionService.Current.Principal)
                    {
                        System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedItem.NguoiTao), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                string msgConfirm = string.Format(IsLock ? Resources.MsgUnLock : Resources.MsgLock, Environment.NewLine, Environment.NewLine);
                string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    OnLockHandler(SelectedItem, msgDone);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnLockHandler(DeNghiQuyetToanModel obj, string msgDone)
        {
            _iVdtDeNghiQuyetToanService.LockOrUnLock(obj.Id, !obj.IsLock);
            //System.Windows.MessageBox.Show(msgDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
        }

        private void RefreshAfterClosePopup(object sender, EventArgs e)
        {
            view.Close();
            LoadData();
        }

        private List<ReportTongHopQuyetToanDuAnHoanThanhModel> GetDataNguonVonByDuAnId(DeNghiQuyetToanModel itemExport, Guid duanId, string tenDuAn, string maDuAn)
        {
            string duToanId = _duToanService.GetDuToanIdByDuAnId(duanId);
            if (string.IsNullOrEmpty(duToanId))
            {
                return new List<ReportTongHopQuyetToanDuAnHoanThanhModel>();
            }
            List<NguonVonQuyetToanKeHoachQuery> listDuToanNguonVonQuery = _vdtQtDeNghiQuyetToanNguonVonService.GetNguonVonByDuToanId(duToanId).ToList();

            List<ReportTongHopQuyetToanDuAnHoanThanhModel> listResult = new List<ReportTongHopQuyetToanDuAnHoanThanhModel>();
            foreach (NguonVonQuyetToanKeHoachQuery item in listDuToanNguonVonQuery)
            {
                listResult.Add(new ReportTongHopQuyetToanDuAnHoanThanhModel
                {
                    IdNguonVon = item.IIdNguonVonId,
                    DieuChinhCuoi = item.GiaTriPheDuyet,
                    NoiDung = item.TenNguonVon
                });
            }

            if (itemExport != null && itemExport.Id != Guid.Empty)
            {
                List<VdtQtDeNghiQuyetToanNguonvon> listNguonVon = _vdtQtDeNghiQuyetToanNguonVonService.FindByDeNghiQuyetToanId(itemExport.Id);
                if (listNguonVon != null)
                {
                    foreach (ReportTongHopQuyetToanDuAnHoanThanhModel item in listResult)
                    {
                        item.DaThanhToan = (listNguonVon.Where(n => n.IIdNguonVonId.HasValue && n.IIdNguonVonId.Value == item.IdNguonVon).FirstOrDefault() != null
                            && listNguonVon.Where(n => n.IIdNguonVonId.HasValue && n.IIdNguonVonId.Value == item.IdNguonVon).FirstOrDefault().FTienToTrinh.HasValue) ?
                           listNguonVon.Where(n => n.IIdNguonVonId.HasValue && n.IIdNguonVonId.Value == item.IdNguonVon).FirstOrDefault().FTienToTrinh.Value : 0;
                    }
                }
            }
            listResult.Select(n => { n.TenDuAn = tenDuAn; n.MaDuAn = maDuAn; return n; }).ToList();
            return listResult;
        }

        public List<DeNghiQuyetToanChiTietModel> LoadHangMuc(DeNghiQuyetToanModel exportItem, DeNghiQuyetToanChiTietModel chiphi)
        {
            VdtQtDeNghiQuyetToan deNghiQuyetToanModel = _iVdtDeNghiQuyetToanService.Find(exportItem.Id);
            List<DuToanDetailQuery> listData = new List<DuToanDetailQuery>();
            //Lúc ban đầu sẽ lấy hạng mục ở phê duyệt dự án => check tồn tại Dutoan trong bang DuToanHangMuc
            List<VdtDaDuToan> duToan = _duToanService.FindListByDuAnId(deNghiQuyetToanModel.IIdDuAnId.Value);
            if (duToan == null || duToan.Count == 0)
            {
                return new List<DeNghiQuyetToanChiTietModel>();
            }
            bool checkExitsDuToanHangMuc = _duToanService.CheckExistInDuToanHangMuc(string.Join(",", duToan.Select(n => n.Id).ToList()), chiphi.ChiPhiId);
            if (checkExitsDuToanHangMuc)
            {
                listData = _duToanService.FindListDetail(string.Join(",", duToan.Select(n => n.Id).ToList()), chiphi.ChiPhiId).ToList();
            }
            else
            {
                VdtDaQddauTu qdDauTu = _quyetDinhDauTuService.FindByDuAnId(deNghiQuyetToanModel.IIdDuAnId.Value);
                if (qdDauTu != null)
                {
                    listData = _duToanService.ListHangMucInitial(qdDauTu.Id, chiphi.ChiPhiId).ToList();
                }
            }
            listData.Select(n => { n.Id = Guid.NewGuid(); n.MaOrDer = chiphi.MaOrderDb + "_" + n.MaOrDer; return n; }).ToList();
            List<DeNghiQuyetToanChiTietModel> listResult = _mapper.Map<List<Model.DeNghiQuyetToanChiTietModel>>(listData);
            listResult.Select(n => { n.ChiPhiIdParentOfHangMuc = chiphi.ChiPhiId; return n; }).ToList();
            listResult.Where(n => !string.IsNullOrEmpty(n.MaHangMuc)).Select(n => { n.MaChiPhi = string.Format("HM-{0}", n.MaHangMuc); return n; }).ToList();
            return listResult;
        }

        private List<ExportChiPhiQuyetToanDuAnHoanThanhModel> GetDataChiPhi(Guid duanId, DeNghiQuyetToanModel deNghi, string tenDuAn, string maDuAn)
        {
            string duToanId = _duToanService.GetDuToanIdByDuAnId(duanId);
            if (string.IsNullOrEmpty(duToanId))
            {
                return new List<ExportChiPhiQuyetToanDuAnHoanThanhModel>();
            }

            List<VdtDaDuToanChiPhiDataQuery> listDuToanChiPhi = _qtDeNghiQuyetToanChiTietService.FindListDuToanChiPhiByDuAn(duanId);
            List<DeNghiQuyetToanChiTietModel> listDeNghiQuyetToan = _mapper.Map<List<Model.DeNghiQuyetToanChiTietModel>>(listDuToanChiPhi);
            List<VdtQtDeNghiQuyetToanChiTiet> ListDbData = _qtDeNghiQuyetToanChiTietService.FindByDeNghiQuyetToanId(deNghi.Id);
            List<DeNghiQuyetToanChiTietModel> listHangMucShow = new List<DeNghiQuyetToanChiTietModel>();

            listDeNghiQuyetToan.Where(n => !string.IsNullOrEmpty(n.MaChiPhi)).Select(n => { n.MaChiPhi = string.Format("CP-{0}", n.MaChiPhi); return n; }).ToList();

            foreach (DeNghiQuyetToanChiTietModel item in listDeNghiQuyetToan)
            {
                item.ListHangMuc = LoadHangMuc(deNghi, item);
                //int indexInsert = ListDeNghiQuyetToan.IndexOf(item);
                if (ListDbData != null && ListDbData.Count > 0)
                {
                    foreach (DeNghiQuyetToanChiTietModel data in item.ListHangMuc)
                    {
                        VdtQtDeNghiQuyetToanChiTiet entity = ListDbData.Where(n => n.IIdHangMucId == data.HangMucId).FirstOrDefault();
                        if (entity != null)
                        {
                            data.IsShow = true;
                            data.IdChiPhiDuAnParent = item.ChiPhiId;
                            listHangMucShow.Add(data);
                            data.FGiaTriAB = entity.FGiaTriQuyetToanAB.HasValue ? entity.FGiaTriQuyetToanAB.Value : 0;
                            data.FGiaTriKiemToan = entity.FGiaTriKiemToan.HasValue ? entity.FGiaTriKiemToan.Value : 0;
                            data.FGiaTriDeNghiQuyetToan = entity.FGiaTriDeNghiQuyetToan.HasValue ? entity.FGiaTriDeNghiQuyetToan.Value : 0;
                            //indexInsert++;
                        }
                    }
                }
            }

            if (ListDbData != null && ListDbData.Count > 0)
            {
                foreach (DeNghiQuyetToanChiTietModel data in listDeNghiQuyetToan)
                {
                    VdtQtDeNghiQuyetToanChiTiet entity = ListDbData.Where(n => n.IIdChiPhiId == data.ChiPhiId).FirstOrDefault();
                    if (entity != null)
                    {
                        data.FGiaTriAB = entity.FGiaTriQuyetToanAB.HasValue ? entity.FGiaTriQuyetToanAB.Value : 0;
                        data.FGiaTriKiemToan = entity.FGiaTriKiemToan.HasValue ? entity.FGiaTriKiemToan.Value : 0;
                        data.FGiaTriDeNghiQuyetToan = entity.FGiaTriDeNghiQuyetToan.HasValue ? entity.FGiaTriDeNghiQuyetToan.Value : 0;
                    }
                }
            }
            listDeNghiQuyetToan.AddRange(listHangMucShow);
            listDeNghiQuyetToan = listDeNghiQuyetToan.OrderBy(n => n.MaOrderDb).ToList();
            List<ExportChiPhiQuyetToanDuAnHoanThanhModel> listResult = new List<ExportChiPhiQuyetToanDuAnHoanThanhModel>();
            foreach (DeNghiQuyetToanChiTietModel item in listDeNghiQuyetToan)
            {
                listResult.Add(new ExportChiPhiQuyetToanDuAnHoanThanhModel
                {
                    TenDuAn = tenDuAn,
                    MaDuAn = maDuAn,
                    Ma = item.MaChiPhi,
                    QuyetToanAB = item.FGiaTriAB,
                    KetQuaThanhTraKiemToan = item.FGiaTriKiemToan,
                    TheoQuyetDinhPheDuyet = item.GiaTriPheDuyet.HasValue ? item.GiaTriPheDuyet.Value : 0,
                    DeNghiQuyetToan = item.FGiaTriDeNghiQuyetToan,
                    TangGiamSoVoiDuToan = (item.GiaTriPheDuyet.HasValue ? item.GiaTriPheDuyet.Value : 0) - item.FGiaTriDeNghiQuyetToan,
                    NoiDung = item.TenChiPhi
                });
            }
            return listResult;
        }

        public void OnExport()
        {
            if (Items == null || Items.Where(n => n.Selected).Count() == 0)
                return;

            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ReportTongHopQuyetToanDuAnHoanThanhModel> result = new List<ReportTongHopQuyetToanDuAnHoanThanhModel>();
                    List<ExportChiPhiQuyetToanDuAnHoanThanhModel> chiPhi = new List<ExportChiPhiQuyetToanDuAnHoanThanhModel>();
                    List<ExportChiPhiKhacQuyetToanDuAnHoanThanhModel> listChiPhiKhac = new List<ExportChiPhiKhacQuyetToanDuAnHoanThanhModel>();
                    List<ExportTaiSanQuyetToanDuAnHoanThanhModel> listTaiSan = new List<ExportTaiSanQuyetToanDuAnHoanThanhModel>();

                    int count = 1;
                    foreach (DeNghiQuyetToanModel item in Items.Where(n => n.Selected).ToList())
                    {
                        VdtDaQddauTu quyetDinhDauTu = new VdtDaQddauTu();
                        VdtQtDeNghiQuyetToan entity = _iVdtDeNghiQuyetToanService.Find(item.Id);
                        double giaTriDuToan = 0;
                        if (entity.IIdDuAnId.HasValue)
                        {
                            quyetDinhDauTu = _quyetDinhDauTuService.FindByDuAnId(entity.IIdDuAnId.Value);
                            giaTriDuToan = _duToanService.GetGiaTriDuToanIdByDuAnId(entity.IIdDuAnId.Value);
                            result.AddRange(GetDataNguonVonByDuAnId(item, entity.IIdDuAnId.Value, item.TenDuAn, item.MaDuAn));
                            chiPhi.AddRange(GetDataChiPhi(entity.IIdDuAnId.Value, item, item.TenDuAn, item.MaDuAn));
                        }
                        item.SttExport = count;
                        listTaiSan.AddRange(GetListTaiSan(item));
                        listChiPhiKhac.AddRange(GetListChiPhiKhac(item));
                        count++;
                    }
                    result.Select(n => { n.Stt = (result.IndexOf(n) + 1).ToString(); return n; }).ToList();
                    chiPhi.Select(n => { n.Stt = (chiPhi.IndexOf(n) + 1).ToString(); return n; }).ToList();
                    listTaiSan.Select(n => { n.Stt = (listTaiSan.IndexOf(n) + 1); return n; }).ToList();
                    listChiPhiKhac.Select(n => { n.Stt = (listChiPhiKhac.IndexOf(n) + 1); return n; }).ToList();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("ChungTu", Items.Where(n => n.Selected).ToList());
                    data.Add("Items", result);
                    data.Add("ChiPhiKhac", listChiPhiKhac);
                    data.Add("MLNS", chiPhi);
                    data.Add("TaiSan", listTaiSan);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KTDT, ExportFileName.EPT_VDT_TONGHOPDENGHIQUYETTOAN);
                    string fileNamePrefix = "eptQuyetToanDuAnHoanThanh";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<DeNghiQuyetToanModel, ReportTongHopQuyetToanDuAnHoanThanhModel, ExportChiPhiKhacQuyetToanDuAnHoanThanhModel,
                            ExportChiPhiQuyetToanDuAnHoanThanhModel, ExportTaiSanQuyetToanDuAnHoanThanhModel>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, ExportType.EXCEL);
                        }
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

        public List<ExportChiPhiKhacQuyetToanDuAnHoanThanhModel> GetListChiPhiKhac(DeNghiQuyetToanModel item)
        {
            List<ExportChiPhiKhacQuyetToanDuAnHoanThanhModel> result = new List<ExportChiPhiKhacQuyetToanDuAnHoanThanhModel>();
            result.Add(new ExportChiPhiKhacQuyetToanDuAnHoanThanhModel
            {
                TenDuAn = item.TenDuAn,
                MaDuAn = item.MaDuAn,
                NoiDung = "Chi phí thiệt hại do các nguyên nhân bất khả kháng",
                GiaTri = item.ChiPhiThietHai
            });
            result.Add(new ExportChiPhiKhacQuyetToanDuAnHoanThanhModel
            {
                TenDuAn = item.TenDuAn,
                MaDuAn = item.MaDuAn,
                NoiDung = "Chi phí không tạo nên tài sản",
                GiaTri = item.ChiPhiKhongTaoNenTaiSan
            });
            return result;
        }

        public List<ExportTaiSanQuyetToanDuAnHoanThanhModel> GetListTaiSan(DeNghiQuyetToanModel item)
        {
            List<ExportTaiSanQuyetToanDuAnHoanThanhModel> result = new List<ExportTaiSanQuyetToanDuAnHoanThanhModel>();
            result.Add(new ExportTaiSanQuyetToanDuAnHoanThanhModel
            {
                TenDuAn = item.TenDuAn,
                MaDuAn = item.MaDuAn,
                Nhom = "Tài sản dài hạn (cố định)",
                CDTQuanLy = item.TaiSanDaiHanThuocCDTQuanLy,
                DonViKhacQuanLy = item.TaiSanDaiHanDonViKhacQuanLy
            });
            result.Add(new ExportTaiSanQuyetToanDuAnHoanThanhModel
            {
                TenDuAn = item.TenDuAn,
                MaDuAn = item.MaDuAn,
                Nhom = "Tài sản ngắn hạn",
                CDTQuanLy = item.TaiSanNganHanThuocCDTQuanLy,
                DonViKhacQuanLy = item.TaiSanNganHanDonViKhacQuanLy
            });
            return result;
        }

        public void OnShowDetail(DeNghiQuyetToanModel quyetToanItem, bool isDoubleClick)
        {
            try
            {
                if (quyetToanItem == null)
                    return;
                RequestSettlementDetailViewModel.Model = quyetToanItem;
                RequestSettlementDetailViewModel.IsDoubleClick = isDoubleClick;
                RequestSettlementDetailViewModel.Init();
                view = new VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.RequestSettlement.RequestSettlementDetail
                {
                    DataContext = RequestSettlementDetailViewModel
                };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool DataFilter(object obj)
        {
            bool result = true;
            var item = (DeNghiQuyetToanModel)obj;
            if (!string.IsNullOrEmpty(SoBaoCaoSearch))
                result = result && !string.IsNullOrEmpty(item.SoBaoCao) && item.SoBaoCao.ToLower().Contains(SoBaoCaoSearch.Trim().ToLower());
            if (!string.IsNullOrEmpty(TenDuAnSearch))
                result = result && !string.IsNullOrEmpty(item.TenDuAn) && item.TenDuAn.ToLower().Contains(TenDuAnSearch.Trim().ToLower());
            if (!string.IsNullOrEmpty(MaDuAnSearch))
                result = result && !string.IsNullOrEmpty(item.MaDuAn) && item.MaDuAn.ToLower().Contains(MaDuAnSearch.Trim().ToLower());
            if (!string.IsNullOrEmpty(ChuDauTuSearch))
                result = result && !string.IsNullOrEmpty(item.TenChuDauTu) && item.TenChuDauTu.ToLower().Contains(ChuDauTuSearch.Trim().ToLower());
            if (GiaTriDeNghiTuSearch > 0)
                result = result && item.GiaTriDeNghiQuyetToan >= GiaTriDeNghiTuSearch;
            if (GiaTriDeNghiDenSearch > 0)
                result = result && item.GiaTriDeNghiQuyetToan <= GiaTriDeNghiDenSearch;
            return result;
        }

        private void OnResetFilter()
        {
            SoBaoCaoSearch = string.Empty;
            TenDuAnSearch = string.Empty;
            MaDuAnSearch = string.Empty;
            GiaTriDeNghiTuSearch = 0;
            GiaTriDeNghiDenSearch = 0;
        }

        private void OnSearch()
        {
            _dataIndexFilter.Refresh();
            OnPropertyChanged(nameof(Items));
        }

        private void LoadData()
        {
            try
            {
                IEnumerable<DeNghiQuyetToanQuery> data = _iVdtDaDuAnService.FindAllDeNghiQuyetToan(_sessionService.Current.YearOfWork, _sessionService.Current.Principal);
                Items = _mapper.Map<ObservableCollection<Model.DeNghiQuyetToanModel>>(data);
                Items.Select(n => { n.Stt = (Items.IndexOf(n) + 1); return n; }).ToList();
                _dataIndexFilter = CollectionViewSource.GetDefaultView(Items);
                _dataIndexFilter.Filter = DataFilter;
                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault();
                }
                OnPropertyChanged(nameof(IsEdit));
                List<VdtQtDeNghiQuyetToan> listTongHop = _iVdtDeNghiQuyetToanService.FindDeNghiTongHop();
                var listTongHopItems = _mapper.Map<List<DeNghiQuyetToanModel>>(listTongHop);
                listTongHopItems = listTongHopItems.Select(n => { n.SRowIndex = (listTongHopItems.IndexOf(n) + 1) + ""; n.IsHangCha = true; return n; }).ToList();
                ListDeNghiTongHop = _mapper.Map<ObservableCollection<DeNghiQuyetToanModel>>(listTongHopItems);
                for (int i = 0; i < ListDeNghiTongHop.Count; i++)
                {
                    var ele = ListDeNghiTongHop.ElementAt(i);
                    ele.HasChildren = ele.BTongHop.HasValue ? ele.BTongHop.Value : false;
                    ele.PropertyChanged += DeNghiQuyetToanModelPropertyChanged;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            try
            {
                RequestSettlementDialogViewModel.Model = new RequestSettlementDialogModel();
                RequestSettlementDialogViewModel.Entity = null;
                RequestSettlementDialogViewModel.Init();
                RequestSettlementDialogViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                    OnShowDetail((DeNghiQuyetToanModel)obj, false);
                };
                var view = new RequestSettlementDialog
                {
                    DataContext = RequestSettlementDialogViewModel
                };
                DialogHost.Show(view, "RootDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected void OnImport()
        {
            try
            {
                //RequestSettlementImportViewModel.Model = new ChungTuDeNghiQuyetToanImportModel();
                //RequestSettlementImportViewModel.Entity = null;
                RequestSettlementImportViewModel.Init();
                RequestSettlementImportViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                };
                var view = new RequestSettlementImport
                {
                    DataContext = RequestSettlementImportViewModel
                };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected void OnShowPopupReport()
        {
            try
            {
                ReportFormSettlementViewModel.ListDeNghi = Items.ToList();
                ReportFormSettlementViewModel.Init();
                var view = new ReportFormSettlement
                {
                    DataContext = ReportFormSettlementViewModel
                };
                DialogHost.Show(view, "RootDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnUpdate()
        {
            try
            {
                if (SelectedItem == null)
                    return;
                if (SelectedItem.NguoiTao != _sessionService.Current.Principal)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleUpdate, SelectedItem.NguoiTao), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                RequestSettlementDialogViewModel.Model = new RequestSettlementDialogModel();
                RequestSettlementDialogViewModel.Model.Id = SelectedItem.Id;
                RequestSettlementDialogViewModel.Init();
                RequestSettlementDialogViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                    OnShowDetail((DeNghiQuyetToanModel)obj, false);
                };
                var view = new RequestSettlementDialog
                {
                    DataContext = RequestSettlementDialogViewModel
                };
                DialogHost.Show(view, "RootDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnDelete()
        {
            try
            {
                if (SelectedItem == null || SelectedItem.IsLock)
                    return;
                if (SelectedItem.NguoiTao != _sessionService.Current.Principal)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleDelete, SelectedItem.NguoiTao), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(Resources.ConfirmDeleteUsers, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (SelectedItem != null)
                    {
                        _iVdtDeNghiQuyetToanService.Delete(SelectedItem.Id);
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnTongHopDeNghiTT()
        {
            RequestSettlementTongHopDialogViewModel.Model = new DeNghiQuyetToanModel();
            RequestSettlementTongHopDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
            };
            RequestSettlementTongHopDialogViewModel.Init();
            RequestSettlementTongHopDialogViewModel.Model.Id = Guid.NewGuid();
            RequestSettlementTongHopDialogViewModel.Model.BTongHop = true;
            RequestSettlementTongHopDialogViewModel.VoucherAgregates = new ObservableCollection<DeNghiQuyetToanModel>(Items.Where(t => t.Selected && DataFilter(t)));
            var validBTongHop = RequestSettlementTongHopDialogViewModel.VoucherAgregates.All(t => !t.ParentId.HasValue);
            if (!validBTongHop)
            {
                MessageBoxHelper.Info("Không thể tổng hợp bản ghi đã tổng hợp");
                return;
            }
            var view = new RequestSettlementTongHopDialog { DataContext = RequestSettlementTongHopDialogViewModel };
            DialogHost.Show(view, "RootDialog");
        }

        private void DeNghiQuyetToanModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(DeNghiQuyetToanModel.IsShowChildren)))
            {
                DeNghiQuyetToanModel model = sender as DeNghiQuyetToanModel;
                if (model.IsShowChildren)
                {
                    OnExpand();
                }
                else
                {
                    OnCollapse();
                }
            }
        }

        private void OnExpand()
        {
            int currentIndex = ListDeNghiTongHop.IndexOf(SelectedTongHopItem);
            SelectedTongHopItem.IsShowChildren = true;
            IEnumerable<DeNghiQuyetToanModel> children = new List<DeNghiQuyetToanModel>(Items.Where(t => SelectedTongHopItem.Id.Equals(t.ParentId)));
            foreach (var item in children)
            {
                //item.Stt = SelectedItem.Stt + "_" + ++stt;
                item.AncestorIds = new HashSet<Guid>();
                item.AncestorIds.Add(SelectedTongHopItem.Id);
                ListDeNghiTongHop.Insert(++currentIndex, item);
            }
        }

        private void OnCollapse()
        {
            SelectedTongHopItem.IsShowChildren = false;
            ListDeNghiTongHop = new ObservableCollection<DeNghiQuyetToanModel>(ListDeNghiTongHop.Where(t => t.AncestorIds == null || !t.AncestorIds.Contains(SelectedTongHopItem.Id)));
        }
    }
}
