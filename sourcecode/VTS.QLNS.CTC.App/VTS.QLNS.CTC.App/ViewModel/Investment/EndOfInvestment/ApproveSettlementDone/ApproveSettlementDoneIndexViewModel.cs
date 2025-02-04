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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.AnnualSettlement;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.ApproveSettlementDone;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.RequestSettlement;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using System.Windows;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.ApproveSettlementDone
{
    public class ApproveSettlementDoneIndexViewModel : GridViewModelBase<PheDuyetQuyetToanModel>
    {
        private IMapper _mapper;
        private ICollectionView _dataIndexFilter;
        private IVdtQtQuyetToanService _vdtQtQuyetToanService;
        private VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.ApproveSettlementDone.ApproveSettlementDoneApproveProcess view;
        private readonly ILog _logger;
        private IExportService _exportService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private readonly IVdtDeNghiQuyetToanService _vdtDeNghiQuyetToanService;
        private readonly IPheDuyetQuyetToanService _vdtPheDuyetQuyetToanService;
        private IVdtQtDeNghiQuyetToanChiTietService _qtDeNghiQuyetToanChiTietService;
        private readonly IVdtQtQuyetToanChiTietService _qtQuyetToanChiTietService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;

        public override string FuncCode => NSFunctionCode.INVESTMENT_END_OF_INVESTMENT_APPROVE_SETTLEMENT_DONE_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FINISH_SETTLEMENT;  

        //public override string Name => "Quản lý phê duyệt quyết toán dự án hoàn thành";
        public override string Name => "Phê duyệt quyết toán dự án hoàn thành";
        public override string Title => "Phê duyệt quyết toán dự án hoàn thành";
        public override string Description => "Danh sách phê duyệt quyết toán dự án hoàn thành";
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.ApproveSettlementDone.ApproveSettlementDoneIndex);
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;
        public bool IsEdit => SelectedItem != null && !SelectedItem.BKhoa;

        private string _tenDuAnSearch;
        public string TenDuAnSearch
        {
            get => _tenDuAnSearch;
            set => SetProperty(ref _tenDuAnSearch, value);
        }

        private string _soBaoCaoSearch;
        public string SoBaoCaoSearch
        {
            get => _soBaoCaoSearch;
            set => SetProperty(ref _soBaoCaoSearch, value);
        }

        private DateTime? _ngayDuyetTuSearch;
        public DateTime? NgayDuyetTuSearch
        {
            get => _ngayDuyetTuSearch;
            set => SetProperty(ref _ngayDuyetTuSearch, value);
        }

        private DateTime? _ngayDuyetDenSearch;
        public DateTime? NgayDuyetDenSearch
        {
            get => _ngayDuyetDenSearch;
            set => SetProperty(ref _ngayDuyetDenSearch, value);
        }

        private double? _giaTriQuyetToanTuSearch;
        public double? GiaTriQuyetToanTuSearch
        {
            get => _giaTriQuyetToanTuSearch;
            set => SetProperty(ref _giaTriQuyetToanTuSearch, value);
        }

        private double? _giaTriQuyetToanDenSearch;
        public double? GiaTriQuyetToanDenSearch
        {
            get => _giaTriQuyetToanDenSearch;
            set => SetProperty(ref _giaTriQuyetToanDenSearch, value);
        }

        public ApproveSettlementDoneDialogViewModel ApproveSettlementDoneDialogViewModel { get; }
        public ApproveSettlementDoneApproveProcessViewModel ApproveSettlementDoneApproveProcessViewModel { get; }
        public ApproveSettlementDoneImportViewModel ApproveSettlementDoneImportViewModel { get; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand RefeshCommand { get; }
        public RelayCommand SelectionDoubleClickCommand { get; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ImportCommand { get; }
        public RelayCommand LockUnLockCommand { get; }

        public ApproveSettlementDoneIndexViewModel(IVdtQtQuyetToanService vdtQtQuyetToanService,
            IExportService exportService,
            IMapper mapper,
            ILog logger,
            IVdtDaDuToanService vdtDaDuToanService,
            IVdtDeNghiQuyetToanService vdtDeNghiQuyetToanService,
            IPheDuyetQuyetToanService vdtPheDuyetQuyetToanService,
            IVdtQtDeNghiQuyetToanChiTietService qtDeNghiQuyetToanChiTietService,
            IVdtQtQuyetToanChiTietService qtQuyetToanChiTietService,
            IApproveProjectService approveProjectService,
            INsDonViService nsDonViService,
            ISessionService sessionService,
            ApproveSettlementDoneDialogViewModel approveSettlementDoneDialogViewModel,
            ApproveSettlementDoneApproveProcessViewModel approveSettlementDoneApproveProcessViewModel,
            ApproveSettlementDoneImportViewModel approveSettlementDoneImportViewModel,
            ApproveSettlementDoneDetailViewModel approveSettlementDoneDetailViewModel)
        {
            _vdtQtQuyetToanService = vdtQtQuyetToanService;
            _exportService = exportService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _vdtDeNghiQuyetToanService = vdtDeNghiQuyetToanService;
            _vdtPheDuyetQuyetToanService = vdtPheDuyetQuyetToanService;
            _qtDeNghiQuyetToanChiTietService = qtDeNghiQuyetToanChiTietService;
            _approveProjectService = approveProjectService;
            _qtQuyetToanChiTietService = qtQuyetToanChiTietService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;

            ApproveSettlementDoneDialogViewModel = approveSettlementDoneDialogViewModel;
            ApproveSettlementDoneApproveProcessViewModel = approveSettlementDoneApproveProcessViewModel;
            ApproveSettlementDoneImportViewModel = approveSettlementDoneImportViewModel;

            _mapper = mapper;
            _logger = logger;
            SelectionDoubleClickCommand = new RelayCommand(obj => OnShowDetail((PheDuyetQuyetToanModel)obj, true));
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            RefeshCommand = new RelayCommand(obj => LoadData());
            ExportCommand = new RelayCommand(obj => OnExport());
            ImportCommand = new RelayCommand(obj => OnImport());
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock());
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
                    if (SelectedItem.UserCreate != _sessionService.Current.Principal)
                    {
                        System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedItem.UserCreate), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void OnLockHandler(PheDuyetQuyetToanModel obj, string msgDone)
        {
            _vdtQtQuyetToanService.LockOrUnLock(obj.Id, !obj.BKhoa);
            //System.Windows.MessageBox.Show(msgDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();
        }

        public override void Init()
        {
            try
            {
                LoadData();
                //ApproveSettlementDoneDetailViewModel.ClosePopup += RefreshAfterClosePopup;
                ApproveSettlementDoneApproveProcessViewModel.ClosePopup += RefreshAfterClosePopup;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
        }

        protected void OnImport()
        {
            try
            {
                ApproveSettlementDoneImportViewModel.Init();
                ApproveSettlementDoneImportViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                };
                var view = new ApproveSettlementDoneImport
                {
                    DataContext = ApproveSettlementDoneImportViewModel
                };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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

                    List<ExportNguonVonPheDuyetQuyetToanDuAnHoanThanhModel> listNguonVon = new List<ExportNguonVonPheDuyetQuyetToanDuAnHoanThanhModel>();
                    List<ExportChiPhiPheDuyetQuyetToanDuAnHoanThanhModel> chiPhi = new List<ExportChiPhiPheDuyetQuyetToanDuAnHoanThanhModel>();
                    List<ExportChiPhiKhacQuyetToanDuAnHoanThanhModel> listChiPhiKhac = new List<ExportChiPhiKhacQuyetToanDuAnHoanThanhModel>();
                    List<ExportTaiSanQuyetToanDuAnHoanThanhModel> listTaiSan = new List<ExportTaiSanQuyetToanDuAnHoanThanhModel>();

                    int count = 1;
                    foreach (PheDuyetQuyetToanModel item in Items.Where(n => n.Selected).ToList())
                    {
                        if (item.IdDuAn.HasValue)
                        {
                            listNguonVon.AddRange(LoadDataNguonVon(item, item.IdDuAn.Value, item.TenDuAn, item.MaDuAn));
                            chiPhi.AddRange(GetDataChiPhi(item));
                        }
                        item.SttExport = count;
                        listTaiSan.AddRange(GetListTaiSan(item));
                        listChiPhiKhac.AddRange(GetListChiPhiKhac(item));
                        count++;
                    }
                    listNguonVon.Select(n => { n.Stt = (listNguonVon.IndexOf(n) + 1).ToString(); return n; }).ToList();
                    chiPhi.Select(n => { n.Stt = (chiPhi.IndexOf(n) + 1).ToString(); return n; }).ToList();
                    listTaiSan.Select(n => { n.Stt = (listTaiSan.IndexOf(n) + 1); return n; }).ToList();
                    listChiPhiKhac.Select(n => { n.Stt = (listChiPhiKhac.IndexOf(n) + 1); return n; }).ToList();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("ChungTu", Items.Where(n => n.Selected).ToList());
                    data.Add("Items", listNguonVon);
                    data.Add("ChiPhiKhac", listChiPhiKhac);
                    data.Add("MLNS", chiPhi);
                    data.Add("TaiSan", listTaiSan);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KTDT, ExportFileName.EPT_VDT_TONGHOPPHEDUYETQUYETTOAN);
                    string fileNamePrefix = "eptPheDuyetQuyetToanDuAnHoanThanh";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<PheDuyetQuyetToanModel, ExportNguonVonPheDuyetQuyetToanDuAnHoanThanhModel, ExportChiPhiKhacQuyetToanDuAnHoanThanhModel,
                            ExportChiPhiPheDuyetQuyetToanDuAnHoanThanhModel, ExportTaiSanQuyetToanDuAnHoanThanhModel>(templateFileName, data);
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

        public List<ExportTaiSanQuyetToanDuAnHoanThanhModel> GetListTaiSan(PheDuyetQuyetToanModel item)
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

        public List<ExportChiPhiKhacQuyetToanDuAnHoanThanhModel> GetListChiPhiKhac(PheDuyetQuyetToanModel item)
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

        private List<ExportChiPhiPheDuyetQuyetToanDuAnHoanThanhModel> GetDataChiPhi(PheDuyetQuyetToanModel deNghi)
        {
            List<ExportChiPhiPheDuyetQuyetToanDuAnHoanThanhModel> listResult = new List<ExportChiPhiPheDuyetQuyetToanDuAnHoanThanhModel>();
            VdtQtQuyetToan QuyetToan = _vdtPheDuyetQuyetToanService.FindQuyetToanByIdQt(deNghi.Id);
            VdtQtDeNghiQuyetToan deNghiQuyetToan = _vdtDeNghiQuyetToanService.FindByDuAnId(deNghi.IdDuAn.Value);
            if (QuyetToan == null)
            {
                return new List<ExportChiPhiPheDuyetQuyetToanDuAnHoanThanhModel>();
            }

            List<VdtDaDuToanChiPhiDataQuery> listDuToanChiPhi = _qtDeNghiQuyetToanChiTietService.FindListDuToanChiPhiByDuAnNew(deNghiQuyetToan.iID_QuyetDinh.Value);
            List<DeNghiQuyetToanChiTietModel> listDeNghiQuyetToan = _mapper.Map<List<DeNghiQuyetToanChiTietModel>>(listDuToanChiPhi);

            listDeNghiQuyetToan.Where(n => !string.IsNullOrEmpty(n.MaChiPhi)).Select(n => { n.MaChiPhi = string.Format("CP-{0}", n.MaChiPhi); return n; }).ToList();
            List<VdtQtDeNghiQuyetToanChiTiet> listDbDeNghiQuyetToan = _qtDeNghiQuyetToanChiTietService.FindByDeNghiQuyetToanId(QuyetToan.IIdDenghiQuyetToanId);

            List<DeNghiQuyetToanChiTietModel> listHangMucShow = new List<DeNghiQuyetToanChiTietModel>();
            foreach (DeNghiQuyetToanChiTietModel item in listDeNghiQuyetToan)
            {
                item.ListHangMuc = LoadHangMuc(deNghiQuyetToan, item);
                //int indexInsert = ListDeNghiQuyetToan.IndexOf(item);
                if (listDbDeNghiQuyetToan != null && listDbDeNghiQuyetToan.Count > 0)
                {
                    foreach (DeNghiQuyetToanChiTietModel data in item.ListHangMuc)
                    {
                        VdtQtDeNghiQuyetToanChiTiet entity = listDbDeNghiQuyetToan.Where(n => n.IIdHangMucId == data.HangMucId).FirstOrDefault();
                        if (entity != null)
                        {
                            data.IsShow = true;
                            data.IdChiPhiDuAnParent = item.ChiPhiId;
                            data.MaOrderDb = entity.SMaOrder;
                            listHangMucShow.Add(data);
                            data.FGiaTriKiemToan = entity.FGiaTriKiemToan.HasValue ? entity.FGiaTriKiemToan.Value : 0;
                            data.FGiaTriDeNghiQuyetToan = entity.FGiaTriDeNghiQuyetToan.HasValue ? entity.FGiaTriDeNghiQuyetToan.Value : 0;
                            //indexInsert++;
                        }
                    }
                }
            }

            if (listDbDeNghiQuyetToan != null && listDbDeNghiQuyetToan.Count > 0)
            {
                foreach (DeNghiQuyetToanChiTietModel data in listDeNghiQuyetToan)
                {
                    VdtQtDeNghiQuyetToanChiTiet entity = listDbDeNghiQuyetToan.Where(n => n.IIdChiPhiId == data.ChiPhiId).FirstOrDefault();
                    if (entity != null)
                    {
                        data.IsShow = true;
                        data.MaOrderDb = entity.SMaOrder;
                        data.FGiaTriKiemToan = entity.FGiaTriKiemToan.HasValue ? entity.FGiaTriKiemToan.Value : 0;
                        data.FGiaTriDeNghiQuyetToan = entity.FGiaTriDeNghiQuyetToan.HasValue ? entity.FGiaTriDeNghiQuyetToan.Value : 0;
                    }
                    VdtDaDuToanChiPhiDataQuery entity2 = listDuToanChiPhi.Where(n => n.ChiPhiId == data.ChiPhiId).FirstOrDefault();
                    if (entity2 != null)
                    {
                        data.GiaTriPheDuyet = entity2.GiaTriPheDuyet;
                        data.MaChiPhi = entity2.MaChiPhi;
                    }
                }
            }
            listDeNghiQuyetToan.AddRange(listHangMucShow);
            listDeNghiQuyetToan = listDeNghiQuyetToan.OrderBy(n => n.MaOrderDb).ToList();
            foreach (DeNghiQuyetToanChiTietModel item in listDeNghiQuyetToan)
            {
                listResult.Add(new ExportChiPhiPheDuyetQuyetToanDuAnHoanThanhModel
                {
                    TenDuAn = deNghi.TenDuAn,
                    MaDuAn = deNghi.MaDuAn,
                    Ma = item.MaChiPhi,
                    NoiDung = item.TenChiPhi,
                    ChiPhiId = item.ChiPhiId,
                    HangMucId = item.HangMucId,
                    GiaTriPheDuyet = item.GiaTriPheDuyet.HasValue ? item.GiaTriPheDuyet.Value : 0,
                    KetQuaThanhTraKiemToan = item.FGiaTriKiemToan,
                    TheoQuyetDinhPheDuyet = item.GiaTriPheDuyet.HasValue ? item.GiaTriPheDuyet.Value : 0,
                    DeNghiQuyetToan = item.FGiaTriDeNghiQuyetToan,
                    TangGiamSoVoiDuToan = (item.GiaTriPheDuyet.HasValue ? item.GiaTriPheDuyet.Value : 0) - item.FGiaTriDeNghiQuyetToan,

                });
            }
            //Get Db value
            List<VdtQtQuyetToanChiTiet> entitys = _qtQuyetToanChiTietService.FindByQuyetToanId(deNghi.Id);
            if (entitys != null && entitys.Count > 0)
            {
                foreach (VdtQtQuyetToanChiTiet entity in entitys)
                {
                    if (entity.IIdChiPhiId != Guid.Empty)
                    {
                        ExportChiPhiPheDuyetQuyetToanDuAnHoanThanhModel map = listResult.Where(n => n.ChiPhiId == entity.IIdChiPhiId).FirstOrDefault();
                        if (map != null)
                        {
                            map.GiaTriThamTra = entity.FGiaTriThamTra.HasValue ? entity.FGiaTriThamTra.Value : 0;
                            map.GiaTriQuyetToan = entity.FGiaTriQuyetToan.HasValue ? entity.FGiaTriQuyetToan.Value : 0;
                        }
                    }
                    else if (entity.IIdHangMucId != Guid.Empty)
                    {
                        ExportChiPhiPheDuyetQuyetToanDuAnHoanThanhModel map = listResult.Where(n => n.HangMucId == entity.IIdHangMucId).FirstOrDefault();
                        if (map != null)
                        {
                            map.GiaTriThamTra = entity.FGiaTriThamTra.HasValue ? entity.FGiaTriThamTra.Value : 0;
                            map.GiaTriQuyetToan = entity.FGiaTriQuyetToan.HasValue ? entity.FGiaTriQuyetToan.Value : 0;
                        }
                    }
                }
            }
            return listResult;
        }

        public List<DeNghiQuyetToanChiTietModel> LoadHangMuc(VdtQtDeNghiQuyetToan deNghiQuyetToanModel, DeNghiQuyetToanChiTietModel chiphi)
        {
            List<DuToanDetailQuery> listData = new List<DuToanDetailQuery>();
            List<VdtDaDuToan> duToan = _vdtDaDuToanService.FindListByDuAnId(deNghiQuyetToanModel.IIdDuAnId.Value);
            if (duToan == null || duToan.Count == 0)
            {
                return new List<DeNghiQuyetToanChiTietModel>();
            }
            bool checkExitsDuToanHangMuc = _vdtDaDuToanService.CheckExistInDuToanHangMuc(string.Join(",", duToan.Select(n => n.Id).ToList()), chiphi.ChiPhiId);
            if (checkExitsDuToanHangMuc)
            {
                listData = _vdtDaDuToanService.FindListDetail(string.Join(",", duToan.Select(n => n.Id).ToList()), chiphi.ChiPhiId).ToList();
            }
            else
            {
                VdtDaQddauTu qdDauTu = _approveProjectService.FindByDuAnId(deNghiQuyetToanModel.IIdDuAnId.Value);
                if (qdDauTu != null)
                {
                    listData = _vdtDaDuToanService.ListHangMucInitial(qdDauTu.Id, chiphi.ChiPhiId).ToList();
                }
            }
            listData.Select(n => { n.Id = Guid.NewGuid(); n.MaOrDer = chiphi.MaOrderDb + "_" + n.MaOrDer; return n; }).ToList();
            List<DeNghiQuyetToanChiTietModel> listResult = _mapper.Map<List<DeNghiQuyetToanChiTietModel>>(listData);
            listResult.Select(n => { n.ChiPhiIdParentOfHangMuc = chiphi.ChiPhiId; return n; }).ToList();
            listResult.Where(n => !string.IsNullOrEmpty(n.MaHangMuc)).Select(n => { n.MaChiPhi = string.Format("HM-{0}", n.MaHangMuc); return n; }).ToList();
            return listResult;
        }

        private List<ExportNguonVonPheDuyetQuyetToanDuAnHoanThanhModel> LoadDataNguonVon(PheDuyetQuyetToanModel itemExport, Guid duanId, string tenDuAn, string maDuAn)
        {
            string duToanId = _vdtDaDuToanService.GetDuToanIdByDuAnId(duanId);
            if (string.IsNullOrEmpty(duToanId))
            {
                return new List<ExportNguonVonPheDuyetQuyetToanDuAnHoanThanhModel>();
            }
            List<ExportNguonVonPheDuyetQuyetToanDuAnHoanThanhModel> listResult = new List<ExportNguonVonPheDuyetQuyetToanDuAnHoanThanhModel>();
            VdtQtDeNghiQuyetToan deNghiQuyetToan = _vdtDeNghiQuyetToanService.FindByDuAnId(duanId);
            string deNghiQuyetToanId = string.Empty;
            if (deNghiQuyetToan != null)
            {
                deNghiQuyetToanId = deNghiQuyetToan.Id.ToString();
            }

            List<NguonVonQuyetToanQuery> listDuToanNguonVonQuery = _vdtQtQuyetToanService.GetNguonVonByDuToanIdDeNghiQuyetToanId(duToanId, deNghiQuyetToanId).ToList();

            foreach (NguonVonQuyetToanQuery item in listDuToanNguonVonQuery)
            {
                listResult.Add(new ExportNguonVonPheDuyetQuyetToanDuAnHoanThanhModel
                {
                    IdNguonVon = item.IdNguonVon,
                    NoiDung = item.TenNguonVon,
                    GiaTriPheDuyet = item.GiaTriPheDuyet,
                    TienDeNghi = item.TienDeNghi,
                    TenDuAn = tenDuAn,
                    MaDuAn = maDuAn
                });
            }

            if (itemExport != null && itemExport.Id != Guid.Empty)
            {
                List<VdtQtQuyetToanNguonvon> listNguonVon = _vdtPheDuyetQuyetToanService.FindByQuyetToanId(itemExport.Id);
                if (listNguonVon != null)
                {
                    foreach (ExportNguonVonPheDuyetQuyetToanDuAnHoanThanhModel item in listResult)
                    {
                        item.TienQuyetToan = listNguonVon.Where(n => n.IIdNguonVonId == item.IdNguonVon).FirstOrDefault() != null ?
                           listNguonVon.Where(n => n.IIdNguonVonId == item.IdNguonVon).FirstOrDefault().FTienPheDuyet.Value : 0;
                    }
                }
            }
            return listResult;
        }

        private void RefreshAfterClosePopup(object sender, EventArgs e)
        {
            view.Close();
            LoadData();
        }

        private void LoadData()
        {
            IEnumerable<VdtQtQuyetToanQuery> data = _vdtQtQuyetToanService.FindAllPheDuyetQuyetToan(_sessionService.Current.YearOfWork, _sessionService.Current.Principal).ToList();
            Items = new ObservableCollection<PheDuyetQuyetToanModel>();
            Items = _mapper.Map<ObservableCollection<Model.PheDuyetQuyetToanModel>>(data);
            Items.Select(n => { n.Stt = (Items.IndexOf(n) + 1); return n; }).ToList();
            _dataIndexFilter = CollectionViewSource.GetDefaultView(Items);
            _dataIndexFilter.Filter = DataFilter;
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
            OnPropertyChanged(nameof(IsEdit));
        }

        private void OnSearch()
        {
            _dataIndexFilter.Refresh();
            OnPropertyChanged(nameof(Items));
        }

        private void OnResetFilter()
        {
            TenDuAnSearch = string.Empty;
            SoBaoCaoSearch = string.Empty;
            NgayDuyetTuSearch = null;
            NgayDuyetDenSearch = null;
            GiaTriQuyetToanTuSearch = null;
            GiaTriQuyetToanDenSearch = null;
        }

        private bool DataFilter(object obj)
        {
            bool result = true;
            var item = (PheDuyetQuyetToanModel)obj;
            if (!string.IsNullOrEmpty(TenDuAnSearch))
                result = result && !string.IsNullOrEmpty(item.TenDuAn) && item.TenDuAn.ToLower().Contains(TenDuAnSearch.ToLower());
            if (!string.IsNullOrEmpty(SoBaoCaoSearch))
                result = result && !string.IsNullOrEmpty(item.SoQuyetDinh) && item.SoQuyetDinh.ToLower().Contains(SoBaoCaoSearch.ToLower());
            if (NgayDuyetTuSearch != null)
                result = result && item.NgayQuyetDinh != null && item.NgayQuyetDinh.Value.Date >= NgayDuyetTuSearch.Value.Date;
            if (NgayDuyetDenSearch != null)
                result = result && item.NgayQuyetDinh != null && item.NgayQuyetDinh.Value.Date <= NgayDuyetDenSearch.Value.Date;
            if (GiaTriQuyetToanTuSearch != null)
                result = result && item.TienQuyetToanPheDuyet >= GiaTriQuyetToanTuSearch;
            if (GiaTriQuyetToanDenSearch != null && GiaTriQuyetToanDenSearch != 0)
                result = result && item.TienQuyetToanPheDuyet <= GiaTriQuyetToanDenSearch;
            return result;
        }

        protected override void OnAdd()
        {
            try
            {
                ApproveSettlementDoneDialogViewModel.Model = new ApproveSettlementDoneDialogModel();
                ApproveSettlementDoneDialogViewModel.Entity = null;
                ApproveSettlementDoneDialogViewModel.Init();
                ApproveSettlementDoneDialogViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                    OnShowDetail((PheDuyetQuyetToanModel)obj, false);
                };
                var view = new ApproveSettlementDoneDialog
                {
                    DataContext = ApproveSettlementDoneDialogViewModel
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
                if (SelectedItem == null || SelectedItem.BKhoa)
                    return;
                if (SelectedItem.UserCreate != _sessionService.Current.Principal)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleUpdate, SelectedItem.UserCreate), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                ApproveSettlementDoneDialogViewModel.Model = new ApproveSettlementDoneDialogModel();
                ApproveSettlementDoneDialogViewModel.Model.Id = SelectedItem.Id;
                ApproveSettlementDoneDialogViewModel.Init();
                ApproveSettlementDoneDialogViewModel.SavedAction = obj =>
                {
                    this.LoadData();
                    OnShowDetail((PheDuyetQuyetToanModel)obj, false);
                };
                var view = new ApproveSettlementDoneDialog
                {
                    DataContext = ApproveSettlementDoneDialogViewModel
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
                if (SelectedItem == null || SelectedItem.BKhoa)
                    return;
                if (SelectedItem.UserCreate != _sessionService.Current.Principal)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleDelete, SelectedItem.UserCreate), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(Resources.ConfirmDeleteUsers, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (SelectedItem != null)
                    {
                        _vdtQtQuyetToanService.Delete(SelectedItem.Id);
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnShowDetail(PheDuyetQuyetToanModel quyetToanItem, bool isDoubleClick)
        {
            try
            {
                if (quyetToanItem == null)
                    return;
                ApproveSettlementDoneApproveProcessViewModel.Model = quyetToanItem;
                ApproveSettlementDoneApproveProcessViewModel.IsDoubleClick = isDoubleClick;
                ApproveSettlementDoneApproveProcessViewModel.Init();
                view = new VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.ApproveSettlementDone.ApproveSettlementDoneApproveProcess
                {
                    DataContext = ApproveSettlementDoneApproveProcessViewModel
                };
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
