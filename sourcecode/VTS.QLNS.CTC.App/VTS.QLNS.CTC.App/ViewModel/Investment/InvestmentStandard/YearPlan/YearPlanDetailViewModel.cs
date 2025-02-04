using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.DateTimeExtension.TimeConst;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.YearPlan
{
    public class YearPlanDetailViewModel : DetailViewModelBase<PhanBoVonModel, PhanBoVonChiTietModel>
    {
        private readonly IVdtKhvPhanBoVonChiTietService _phanBoVonChiTietService;
        private static string _sServiceName = "Chứng từ chi tiết kế hoạch vốn năm được duyệt ";
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IMucLucNganSachService _mlMucLucNganSach;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISysAuditLogService _log;
        private readonly IVdtKhvPhanBoVonDonViPheDuyetService _iVdtKhvPhanBoVonDonViPheDuyetService;
        private readonly ILog _logger;
        private ICollectionView _phanBoVonView;
        private IMapper _mapper;
        private static Dictionary<string, Guid> _dicMucLucNganSach = new Dictionary<string, Guid>();

        public override string Title => "Dự toán được giao";
        public override string Name => (Model != null && Model.IsViewDetail) ? "XEM DỰ TOÁN ĐƯỢC GIAO CHI TIẾT" : "DỰ TOÁN ĐƯỢC GIAO CHI TIẾT";
        public override string Description => string.Format("Số quyết định: {0} - Ngày quyết định: {1} - Năm kế hoạch: {2} - Đơn vị: {3}",
                                                Model.sSoQuyetDinh, Model.dNgayQuyetDinh.Value.ToString("dd/MM/yyyy"),
                                                Model.iNamKeHoach,
                                                _nsDonViService.FindByIdDonVi(Model.iID_MaDonViQuanLy, Model.iNamKeHoach.Value) != null ?
                                                _nsDonViService.FindByIdDonVi(Model.iID_MaDonViQuanLy, Model.iNamKeHoach.Value).TenDonVi : string.Empty);
        public Visibility HidenAdjust => !Model.IsAdjust ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowAdjust => Model.IsAdjust ? Visibility.Visible : Visibility.Collapsed;
        public bool IsAdjust => Model.IsAdjust;
        public bool IsEnableProject => Model != null && Model.BActive && !BIsDetail;

        private bool _bIsDetail;
        public bool BIsDetail
        {
            get => _bIsDetail;
            set => SetProperty(ref _bIsDetail, value);
        }

        #region Item tong
        private PhanBoVonChiTietModel _summaryItems = new PhanBoVonChiTietModel();
        public PhanBoVonChiTietModel SummaryItems
        {
            get => _summaryItems;
            set => SetProperty(ref _summaryItems, value);
        }
        #endregion

        #region data combobox
        private ObservableCollection<ComboboxItem> _cbxLoaiDieuChinh;
        public ObservableCollection<ComboboxItem> CbxLoaiDieuChinh
        {
            get => _cbxLoaiDieuChinh;
            set => SetProperty(ref _cbxLoaiDieuChinh, value);
        }

        private ObservableCollection<ComboboxItem> _drpDuAn;
        public ObservableCollection<ComboboxItem> DrpDuAn
        {
            get => _drpDuAn;
            set => SetProperty(ref _drpDuAn, value);
        }

        private ComboboxItem _drpDuAnSelected;
        public ComboboxItem DrpDuAnSelected
        {
            get => _drpDuAnSelected;
            set
            {
                SetProperty(ref _drpDuAnSelected, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _drpDonViThucHienDuAns;
        public ObservableCollection<ComboboxItem> DrpDonViThucHienDuAns
        {
            get => _drpDonViThucHienDuAns;
            set => SetProperty(ref _drpDonViThucHienDuAns, value);
        }

        private ComboboxItem _drpDonViThucHienDuAnSelected;
        public ComboboxItem DrpDonViThucHienDuAnSelected
        {
            get => _drpDonViThucHienDuAnSelected;
            set
            {
                SetProperty(ref _drpDonViThucHienDuAnSelected, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _drpLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> DrpLoaiCongTrinh
        {
            get => _drpLoaiCongTrinh;
            set => SetProperty(ref _drpLoaiCongTrinh, value);
        }

        private ComboboxItem _drpLoaiCongTrinhSelected;
        public ComboboxItem DrpLoaiCongTrinhSelected
        {
            get => _drpLoaiCongTrinhSelected;
            set
            {
                SetProperty(ref _drpLoaiCongTrinhSelected, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _drpLoaiDuAn;
        public ObservableCollection<ComboboxItem> DrpLoaiDuAn
        {
            get => _drpLoaiDuAn;
            set => SetProperty(ref _drpLoaiDuAn, value);
        }

        private ComboboxItem _drpLoaiDuAnSelected;
        public ComboboxItem DrpLoaiDuAnSelected
        {
            get => _drpLoaiDuAnSelected;
            set
            {
                SetProperty(ref _drpLoaiDuAnSelected, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _drpVoucherSuggestionAgregates;
        public ObservableCollection<ComboboxItem> DrpVoucherSuggestionAgregates
        {
            get => _drpVoucherSuggestionAgregates;
            set => SetProperty(ref _drpVoucherSuggestionAgregates, value);
        }

        private ComboboxItem _drpVoucherSuggestionSelected;
        public ComboboxItem DrpVoucherSuggestionSelected
        {
            get => _drpVoucherSuggestionSelected;
            set
            {
                SetProperty(ref _drpVoucherSuggestionSelected, value);
                if (value != null)
                {
                    LoadData();
                }
            }
        }

        public Visibility VisibilityModified
        {
            get => Model.iId_ParentId != null ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool IsReadOnly => Model.iId_ParentId.HasValue ? true : false;

        #endregion

        public RelayCommand SaveDataCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }

        public YearPlanDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtKhvPhanBoVonChiTietService phanBoVonChiTietService,
            ITongHopNguonNSDauTuService tonghopService,
            INsMucLucNganSachService mucLucNganSachService,
            IMucLucNganSachService mlMucLucNganSach,
            IVdtKhvPhanBoVonDonViPheDuyetService iVdtKhvPhanBoVonDonViPheDuyetService,
            ILog logger,
            ISysAuditLogService log)
        {
            _phanBoVonChiTietService = phanBoVonChiTietService;
            _mucLucNganSachService = mucLucNganSachService;
            _iVdtKhvPhanBoVonDonViPheDuyetService = iVdtKhvPhanBoVonDonViPheDuyetService;
            _logger = logger;
            _tonghopService = tonghopService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mlMucLucNganSach = mlMucLucNganSach;
            _mapper = mapper;
            _log = log;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => onResetFilter());
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new System.Windows.Thickness(10);
                DrpVoucherSuggestionSelected = null;
                LoadDataDeXuat();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        #region Relay Command
        private void LoadDataDeXuat()
        {
            try
            {
                var predicate = PredicateBuilder.True<VdtKhvPhanBoVonDonViPheDuyet>();
                var namKH = Model.ILoaiDuToan == (int)LoaiDuToan.Type.NAM_TRUOC_CHUYEN_SANG ? Model.iNamKeHoach - 1 : Model.iNamKeHoach;
                predicate = predicate.And(x => x.INamKeHoach == namKH);
                predicate = predicate.And(x => x.IIdMaDonViQuanLy == Model.iID_MaDonViQuanLy);
                predicate = predicate.And(x => x.IIdNguonVonId == Model.iId_NguonVonId);

                var itemQuery = _iVdtKhvPhanBoVonDonViPheDuyetService.FindByCondition(predicate).ToList();

                DrpVoucherSuggestionAgregates = _mapper.Map<ObservableCollection<ComboboxItem>>(itemQuery);

                if (DrpVoucherSuggestionAgregates.Count() > 0 && DrpVoucherSuggestionSelected == null)
                {
                    DrpVoucherSuggestionSelected = DrpVoucherSuggestionAgregates.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<PhanBoVonChiTietQuery> lstDuAn = new List<PhanBoVonChiTietQuery>();
                    List<PhanBoVonChiTietQuery> lstDuAnEdit = new List<PhanBoVonChiTietQuery>();
                    GetMucLucNganSachByParent();
                    if(BIsDetail || Model.IsEdit)
                    {
                        lstDuAn = _phanBoVonChiTietService.GetAllDuAnInPhanBoVonByEdit((DrpVoucherSuggestionSelected != null ? DrpVoucherSuggestionSelected.ValueItem : string.Empty), Model.iId_NguonVonId.Value).ToList();
                        lstDuAnEdit = _phanBoVonChiTietService.GetPhanBoVonChiTietByParentId(Model.Id);
                        if (Model.IsAdjust && !BIsDetail)
                        {
                            lstDuAnEdit = lstDuAnEdit.Select(n => { n.fChiTieuNganSach = n.fChiTieuGoc; return n; }).ToList();
                        }
                    }
                    else
                    {
                        lstDuAn = _phanBoVonChiTietService.GetAllDuAnInPhanBoVonByEdit((DrpVoucherSuggestionSelected != null ? DrpVoucherSuggestionSelected.ValueItem : string.Empty), Model.iId_NguonVonId.Value).ToList();
                        if (Model.iId_ParentId.HasValue)
                            lstDuAnEdit = _phanBoVonChiTietService.GetPhanBoVonChiTietByParentId(Guid.Parse(Model.iId_ParentId.Value.ToString()));
                        if (Model.IsAdjust && !BIsDetail)
                        {
                            lstDuAnEdit = lstDuAnEdit.Select(n => { n.fChiTieuNganSach = n.fChiTieuGoc; return n; }).ToList();
                            foreach (var item in lstDuAnEdit)
                            {
                                if (item.IdChungTuParent.HasValue)
                                {
                                    item.FCapPhatTaiKhoBac = item.FCapPhatTaiKhoBacDc;
                                    item.FCapPhatTaiKhoBacDc = 0;
                                    item.FCapPhatBangLenhChi = item.FCapPhatBangLenhChiDc;
                                    item.FCapPhatBangLenhChiDc = 0;
                                    item.FTonKhoanTaiDonVi = item.FTonKhoanTaiDonViDc;
                                    item.FTonKhoanTaiDonViDc = 0;
                                    item.FGiaTriThuHoiNamTruocKhoBac = item.FGiaTriThuHoiNamTruocKhoBacDc;
                                    item.FGiaTriThuHoiNamTruocKhoBacDc = 0;
                                    item.FGiaTriThuHoiNamTruocLenhChi = item.FGiaTriThuHoiNamTruocLenhChiDc;
                                    item.FGiaTriThuHoiNamTruocLenhChiDc = 0;
                                }
                            }
                        }
                    }
                    //else if (Model.iId_ParentId.HasValue)
                    //{
                    //    var itemlstDuAnDieuChinh = _phanBoVonChiTietService.GetPhanBoVonChiTietDieuChinhByParentId(Model.Id);
                    //    lstDuAnEdit = _mapper.Map<List<PhanBoVonChiTietQuery>>(itemlstDuAnDieuChinh);
                    //}

                    lstDuAnEdit = UpdateFThanhToanDeXuatForListDuAn(lstDuAnEdit, lstDuAn);

                    if (lstDuAnEdit != null && lstDuAnEdit.Count != 0)
                    {
                        lstDuAn = lstDuAn.Where(n => !lstDuAnEdit.Any(m => m.iID_DuAnID == n.iID_DuAnID && m.iID_LoaiCongTrinhID == n.iID_LoaiCongTrinhID &&  m.IID_DuAn_HangMucID == n.IID_DuAn_HangMucID)).ToList();
                        if (BIsDetail || Model.IsEdit || Model.IsAdjust)
                        {
                            lstDuAn = lstDuAnEdit;
                        }
                        else
                        {
                            lstDuAn.AddRange(lstDuAnEdit);
                        }
                    }

                    Items = _mapper.Map<ObservableCollection<PhanBoVonChiTietModel>>(lstDuAn);

                    Items.Select(x => { x.BActive = Model.BActive && !Model.IsViewDetail && !Model.BKhoa; return x; }).ToList();

                    CreateDropdownDuAn();
                    CreateDropdownCapPheDuyet();
                    CreateDropdownLoaiCongTrinh();
                    CreateDropDownDonViThucHienDuAn();
                    SumTotalItem();
                }, (s, e) =>
                {
                    IsLoading = false;
                    _phanBoVonView = CollectionViewSource.GetDefaultView(Items);
                    _phanBoVonView.Filter = VdtKhPhanBoVonFilter;
                    foreach (var item in Items)
                    {
                        item.PropertyChanged += DetailModel_PropertyChanged;
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Update trường giá trị thanh toán đề xuất cho các phần tử trong lstDuAnEdit, vì list này lấy từ bảng đề xuất nên k có trường fThanhToanDeXuat
        /// </summary>
        /// <param name="lstDuAnEdit"></param>
        /// <param name="lstDuAn"></param>
        private List<PhanBoVonChiTietQuery> UpdateFThanhToanDeXuatForListDuAn(List<PhanBoVonChiTietQuery> lstDuAnEdit, List<PhanBoVonChiTietQuery> lstDuAn)
        {
            lstDuAn.ForEach(da =>
            {
                PhanBoVonChiTietQuery duan = lstDuAnEdit.FirstOrDefault(d => d.iID_DuAnID == da.iID_DuAnID && d.iID_LoaiCongTrinhID == da.iID_LoaiCongTrinhID && d.IID_DuAn_HangMucID == da.IID_DuAn_HangMucID);
                if(duan != null)
                {
                    lstDuAnEdit = lstDuAnEdit.Where(d => d.iID_DuAnID != da.iID_DuAnID || d.iID_LoaiCongTrinhID != da.iID_LoaiCongTrinhID || d.IID_DuAn_HangMucID != da.IID_DuAn_HangMucID).ToList();
                    duan.fThanhToanDeXuat = da.fThanhToanDeXuat;
                    lstDuAnEdit.Add(duan);
                }                
            });
            return lstDuAnEdit;
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnAdd()
        {
            try
            {
                if (SelectedItem != null)
                {
                    int currentRow = Items.IndexOf(SelectedItem);
                    int targetRow = Items.ToList().FindIndex(currentRow, x => x.IsEditable);
                    var sourceItem = Items.ElementAt(targetRow);
                    PhanBoVonChiTietModel newItem = ObjectCopier.Clone(sourceItem);
                    newItem.sK = string.Empty;
                    newItem.sL = string.Empty;
                    newItem.sM = string.Empty;
                    newItem.sNG = string.Empty;
                    newItem.sTM = string.Empty;
                    newItem.sTTM = string.Empty;
                    newItem.fChiTieuNganSach = 0;
                    newItem.sGhiChu = string.Empty;
                    newItem.PropertyChanged += DetailModel_PropertyChanged;
                    if (Model.IsAdjust)
                    {
                        newItem.CbxLoaiDieuChinh = CbxLoaiDieuChinh;
                    }
                    Items.Insert(currentRow + 1, newItem);
                    OnPropertyChanged(nameof(Items));
                    _phanBoVonView = CollectionViewSource.GetDefaultView(Items);
                    _phanBoVonView.Filter = VdtKhPhanBoVonFilter;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnDelete()
        {
            if (!IsEnableProject) return;
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
            OnPropertyChanged(nameof(Items));
        }

        public void OnSaveData()
        {
            try
            {
                if (!IsEnableProject) return;
                DateTime dStartDate = DateTime.Now;
                StringBuilder messageBuilder = new StringBuilder();
                List<PhanBoVonChiTietModel> lstDataNew = new List<PhanBoVonChiTietModel>();

                if (Model.iId_ParentId.HasValue)
                {
                    Model.IsEdit = true;
                    lstDataNew = Items.Where(n => ((n.FGiaTriThuHoiNamTruocKhoBacDc.HasValue && n.FGiaTriThuHoiNamTruocKhoBacDc != null && n.FGiaTriThuHoiNamTruocKhoBacDc != 0)
                                                    || (n.FGiaTriThuHoiNamTruocLenhChiDc.HasValue && n.FGiaTriThuHoiNamTruocLenhChiDc.Value != 0)
                                                    || (n.FCapPhatTaiKhoBacDc.HasValue && n.FCapPhatTaiKhoBacDc != null && n.FCapPhatTaiKhoBacDc.Value != 0)
                                                    || (n.FTonKhoanTaiDonViDc.HasValue && n.FTonKhoanTaiDonViDc != null && n.FTonKhoanTaiDonViDc.Value != 0)
                                                    || (n.FCapPhatBangLenhChiDc.HasValue && n.FCapPhatBangLenhChiDc != null && n.FCapPhatBangLenhChiDc.Value != 0)) && !n.IsDeleted).ToList();
                }
                else
                {
                    lstDataNew = Items.Where(n => ((n.FGiaTriThuHoiNamTruocKhoBac.HasValue && n.FGiaTriThuHoiNamTruocKhoBac != null && n.FGiaTriThuHoiNamTruocKhoBac.Value != 0)
                                                    || (n.FGiaTriThuHoiNamTruocLenhChi.HasValue && n.FGiaTriThuHoiNamTruocLenhChi != null && n.FGiaTriThuHoiNamTruocLenhChi.Value != 0)
                                                    || (n.FCapPhatTaiKhoBac.HasValue && n.FCapPhatTaiKhoBac != null && n.FCapPhatTaiKhoBac.Value != 0)
                                                    || (n.FTonKhoanTaiDonVi.HasValue && n.FTonKhoanTaiDonVi != null && n.FTonKhoanTaiDonVi.Value != 0)
                                                    || (n.FCapPhatBangLenhChi.HasValue && n.FCapPhatBangLenhChi != null && n.FCapPhatBangLenhChi.Value != 0)) && !n.IsDeleted).ToList();
                }

                if (lstDataNew == null || lstDataNew.Count == 0)
                {
                    messageBuilder.Append(Resources.MsgErrorDataEmpty);
                    MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                    return;
                }
                List<string> lstXauNoiChuoi;
                List<PhanBoVonChiTietInsertQueryNew> lstData = new List<PhanBoVonChiTietInsertQueryNew>();
                foreach (var item in lstDataNew)
                {
                    lstXauNoiChuoi = new List<string>();
                    lstXauNoiChuoi.Add(item.sL ?? string.Empty); lstXauNoiChuoi.Add(item.sK ?? string.Empty); lstXauNoiChuoi.Add(item.sM ?? string.Empty);
                    lstXauNoiChuoi.Add(item.sTM ?? string.Empty); lstXauNoiChuoi.Add(item.sTTM ?? string.Empty); lstXauNoiChuoi.Add(item.sNG ?? string.Empty);
                    if (string.IsNullOrEmpty(string.Join("", lstXauNoiChuoi)))
                    {
                        messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Mục lục ngân sách");
                        break;
                    }
                    if (lstXauNoiChuoi.IndexOf(string.Empty) != -1 &&
                        lstXauNoiChuoi.IndexOf(string.Empty) < lstXauNoiChuoi.LastIndexOf(lstXauNoiChuoi.LastOrDefault(n => !string.IsNullOrEmpty(n))))
                    {
                        messageBuilder.AppendFormat(Resources.MsgErrorFormat, "Mục lục ngân sách");
                        break;
                    }
                    lstData.Add(ConvertDataInsert(item));
                }
                if (messageBuilder.Length != 0)
                {
                    MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                    return;
                }

                //bool isSucess = _phanBoVonChiTietService.CreatePhanBoVonChiTiet(Model.Id, (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet, lstData, _sessionService.Current.Principal, Model.IsEdit);
                bool isSucess = _phanBoVonChiTietService.CreatePhanBoVonChiTietNew(Model.Id, (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet, lstData, _sessionService.Current.Principal, Model.IsEdit);
                if (!isSucess)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorFormat, "Mục lục ngân sách");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }
                _log.WriteLog(Resources.ApplicationName, _sServiceName, (int)TypeExecute.Update, dStartDate, TransactionStatus.Success, _sessionService.Current.Principal);
                ProcessXuLyDuLieu();
                MessageBox.Show(Resources.MsgSaveDone);
                if (!Model.IsEdit && !Model.IsAdjust)
                {
                    Model.IsEdit = true;
                }
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ProcessXuLyDuLieu()
        {
            try
            {
                List<TongHopNguonNSDauTuQuery> lstDataInsert = new List<TongHopNguonNSDauTuQuery>();
                if (Model.IsAdjust)
                {
                    _tonghopService.InsertTongHopNguonDauTu_Tang(LOAI_CHUNG_TU.KE_HOACH_VON_NAM, (int)TypeExecute.Adjust, Model.Id, Model.iId_ParentId);
                }
                else
                {
                    _tonghopService.InsertTongHopNguonDauTu_Tang(LOAI_CHUNG_TU.KE_HOACH_VON_NAM, (int)TypeExecute.Update, Model.Id);
                }

                foreach (var item in
                    Items.Where(n => !n.IsDeleted && ((n.FGiaTriThuHoiNamTruocKhoBac ?? 0) != 0 || (n.FGiaTriThuHoiNamTruocLenhChi ?? 0) != 0)))
                {
                    if ((item.FGiaTriThuHoiNamTruocKhoBac ?? 0) != 0)
                    {
                        TongHopNguonNSDauTuQuery data = GetMucLucNganSachByPhanBoVon(item);
                        data.iID_ChungTu = Model.Id;
                        data.fGiaTri = (item.FGiaTriThuHoiNamTruocKhoBac ?? 0);
                        data.bIsLog = false;
                        data.iID_DuAnID = item.iID_DuAnID;
                        data.sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU;
                        data.sMaDich = LOAI_CHUNG_TU.KHVU_KHOBAC;
                        data.sMaNguonCha = LOAI_CHUNG_TU.KHVN_KHOBAC;
                        data.BKeHoach = true;
                        lstDataInsert.Add(data);
                    }
                    if ((item.FGiaTriThuHoiNamTruocLenhChi ?? 0) != 0)
                    {
                        TongHopNguonNSDauTuQuery data = GetMucLucNganSachByPhanBoVon(item);
                        data.iID_ChungTu = Model.Id;
                        data.fGiaTri = (item.FGiaTriThuHoiNamTruocLenhChi ?? 0);
                        data.bIsLog = false;
                        data.iID_DuAnID = item.iID_DuAnID;
                        data.sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU;
                        data.sMaDich = LOAI_CHUNG_TU.KHVU_LENHCHI;
                        data.sMaNguonCha = LOAI_CHUNG_TU.KHVN_LENHCHI;
                        data.BKeHoach = true;
                        lstDataInsert.Add(data);
                    }
                }
                if (lstDataInsert.Count != 0)
                {
                    _tonghopService.InsertTongHopNguonDauTu_KHVN_Giam(Model.iNamKeHoach ?? 0, LOAI_CHUNG_TU.KE_HOACH_VON_NAM, (int)TypeExecute.Update, Model.Id, lstDataInsert);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                if (args.PropertyName == nameof(PhanBoVonChiTietModel.FCapPhatBangLenhChi)
                || args.PropertyName == nameof(PhanBoVonChiTietModel.FCapPhatBangLenhChiDc)
                || args.PropertyName == nameof(PhanBoVonChiTietModel.FCapPhatTaiKhoBac)
                || args.PropertyName == nameof(PhanBoVonChiTietModel.FCapPhatTaiKhoBacDc)
                || args.PropertyName == nameof(PhanBoVonChiTietModel.FTonKhoanTaiDonVi)
                || args.PropertyName == nameof(PhanBoVonChiTietModel.FTonKhoanTaiDonViDc)
                || args.PropertyName == nameof(PhanBoVonChiTietModel.FGiaTriThuHoiNamTruocKhoBac)
                || args.PropertyName == nameof(PhanBoVonChiTietModel.FGiaTriThuHoiNamTruocKhoBacDc)
                || args.PropertyName == nameof(PhanBoVonChiTietModel.FGiaTriThuHoiNamTruocLenhChi)
                || args.PropertyName == nameof(PhanBoVonChiTietModel.FGiaTriThuHoiNamTruocLenhChiDc))
                {
                    PhanBoVonChiTietModel item = (PhanBoVonChiTietModel)sender;
                    item.IsModified = true;
                    SumTotalItem();
                }

                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnSearch()
        {
            _phanBoVonView.Refresh();
            SumTotalItem();
        }

        private void onResetFilter()
        {
            DrpLoaiCongTrinhSelected = null;
            DrpLoaiDuAnSelected = null;
            DrpDuAnSelected = null;
            DrpDonViThucHienDuAnSelected = null;
            OnPropertyChanged(nameof(DrpLoaiCongTrinhSelected));
            OnPropertyChanged(nameof(DrpLoaiDuAnSelected));
            OnPropertyChanged(nameof(DrpDuAnSelected));
            OnPropertyChanged(nameof(DrpDonViThucHienDuAnSelected));
            OnSearch();
        }
        #endregion

        #region Helper
        private bool VdtKhPhanBoVonFilter(object obj)
        {
            try
            {
                if (!(obj is PhanBoVonChiTietModel temp)) return true;
                var bCondition = true;
                if (DrpLoaiCongTrinhSelected != null)
                {
                    bCondition &= (temp.iID_LoaiCongTrinhID.HasValue && temp.iID_LoaiCongTrinhID == Guid.Parse(DrpLoaiCongTrinhSelected.ValueItem));
                }
                if (DrpLoaiDuAnSelected != null)
                {
                    bCondition &= (temp.ILoaiDuAn.HasValue && temp.ILoaiDuAn == Int32.Parse(DrpLoaiDuAnSelected.ValueItem));
                }
                if (DrpDuAnSelected != null)
                {
                    bCondition &= (temp.iID_DuAnID == Guid.Parse(DrpDuAnSelected.ValueItem));
                }
                if (DrpDonViThucHienDuAnSelected != null)
                {
                    bCondition &= (temp.IIdMaDonViThucHienDuAn == DrpDonViThucHienDuAnSelected.ValueItem);
                }

                temp.IsFilter = bCondition;
                return bCondition;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return true;
            }
        }

        private void SumTotalItem()
        {
            try
            {
                _summaryItems.FCapPhatTaiKhoBac = Items.Where(x => x.IsFilter).Sum(x => x.FCapPhatTaiKhoBac);
                _summaryItems.FCapPhatTaiKhoBacDc = Items.Where(x => x.IsFilter).Sum(x => x.FCapPhatTaiKhoBacDc);
                _summaryItems.FCapPhatBangLenhChi = Items.Where(x => x.IsFilter).Sum(x => x.FCapPhatBangLenhChi);
                _summaryItems.FCapPhatBangLenhChiDc = Items.Where(x => x.IsFilter).Sum(x => x.FCapPhatBangLenhChiDc);
                _summaryItems.FTonKhoanTaiDonVi = Items.Where(x => x.IsFilter).Sum(x => x.FTonKhoanTaiDonVi);
                _summaryItems.FTonKhoanTaiDonViDc = Items.Where(x => x.IsFilter).Sum(x => x.FTonKhoanTaiDonViDc);
                _summaryItems.FGiaTriThuHoiNamTruocKhoBac = Items.Where(x => x.IsFilter).Sum(x => x.FGiaTriThuHoiNamTruocKhoBac);
                _summaryItems.FGiaTriThuHoiNamTruocKhoBacDc = Items.Where(x => x.IsFilter).Sum(x => x.FGiaTriThuHoiNamTruocKhoBacDc);
                _summaryItems.FGiaTriThuHoiNamTruocLenhChi = Items.Where(x => x.IsFilter).Sum(x => x.FGiaTriThuHoiNamTruocLenhChi);
                _summaryItems.FGiaTriThuHoiNamTruocLenhChiDc = Items.Where(x => x.IsFilter).Sum(x => x.FGiaTriThuHoiNamTruocLenhChiDc);

                OnPropertyChanged(nameof(SummaryItems));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void GetInfoDuAn()
        {
            try
            {
                //string sXauNoiChuoi = SelectedItem.sLNS + "-" + SelectedItem.sL + "-" + SelectedItem.sK + "-" + SelectedItem.sM + "-" + SelectedItem.sTM + "-" + SelectedItem.sTTM + "-" + SelectedItem.sNG;
                string sXauNoiChuoi = SelectedItem.sLNS + "-" + SelectedItem.sL + "-" + SelectedItem.sK + "-" + SelectedItem.sM + "-" + SelectedItem.sTM;
                if (!_dicMucLucNganSach.ContainsKey(sXauNoiChuoi))
                {
                    MessageBox.Show(Resources.MsgErrorMucLucNganSachNotExist);
                    SelectedItem.IsDeleted = true;
                    return;
                }
                int iItemExist = Items.Where(n => n.iID_DuAnID == SelectedItem.iID_DuAnID && n.sL == SelectedItem.sL && n.sK == SelectedItem.sK && n.sM == SelectedItem.sM && n.sTM == SelectedItem.sTM && n.sTTM == SelectedItem.sTTM && n.sNG == SelectedItem.sNG && !n.IsDeleted).Count();
                if (iItemExist != 1)
                {
                    MessageBox.Show(Resources.MsgErrorDataExist);
                    SelectedItem.IsDeleted = true;
                    return;
                }
                if (SelectedItem.fChiTieuNganSach != 0) return;

                Dictionary<string, Guid> data = _mlMucLucNganSach.GetLoaiNganSachByNamLamViec(Model.iNamKeHoach.Value).GroupBy(x => x.Lns).ToDictionary(x => x.Key, x => x.Select(x => x.Id).FirstOrDefault());

                if (data.Keys.Contains(SelectedItem.sLNS))
                {
                    var duAnInfo = _phanBoVonChiTietService.GetVonDaBoTriByDuAnIdAnMucLucNganSach((int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet, SelectedItem.iID_DuAnID, Model.iID_MaDonViQuanLy, Model.iNamKeHoach.Value,
                    Model.dNgayQuyetDinh.Value, data[SelectedItem.sLNS], Model.iId_NguonVonId ?? 0, SelectedItem.sL, SelectedItem.sK, SelectedItem.sM, SelectedItem.sTM, SelectedItem.sTTM, SelectedItem.sNG);
                    SelectedItem.fVonDaBoTri = duAnInfo.fVonDaBoTri;
                    SelectedItem.fChiTieuDauNam = duAnInfo.fChiTieuDauNam;
                    SelectedItem.fVonConLai = SelectedItem.fGiaTriDauTu - duAnInfo.fVonDaBoTri;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private PhanBoVonChiTietInsertQueryNew ConvertDataInsert(PhanBoVonChiTietModel item)
        {
            try
            {
                PhanBoVonChiTietInsertQueryNew data = new PhanBoVonChiTietInsertQueryNew();
                data.fGiaTrDeNghi = null;
                data.fGiaTriThuHoi = null;
                data.fTiGia = (Double)(Model.fTiGiaDonVi ?? 0);
                data.fTiGiaDonVi = (Double)(Model.fTiGiaDonVi ?? 0);
                data.iID_DonViTienTeID = Model.iId_DonViTienTeId;
                data.iID_DuAnID = item.iID_DuAnID;
                data.iID_LoaiNguonVonID = Model.iId_LoaiNguonVonId;
                data.iID_PhanBoVonID = Model.Id;
                if (Model.iId_ParentId.HasValue)
                {
                    data.fCapPhatTaiKhoBac = (Double)(item.FCapPhatTaiKhoBac.HasValue ? item.FCapPhatTaiKhoBac.Value : 0);
                    data.fCapPhatTaiKhoBacDc = (Double)(item.FCapPhatTaiKhoBacDc.HasValue ? item.FCapPhatTaiKhoBacDc.Value : 0);
                    data.fCapPhatBangLenhChi = (Double)(item.FCapPhatBangLenhChi.HasValue ? item.FCapPhatBangLenhChi.Value : 0);
                    data.fCapPhatBangLenhChiDc = (Double)(item.FCapPhatBangLenhChiDc.HasValue ? item.FCapPhatBangLenhChiDc.Value : 0);

                    data.fTonKhoanTaiDonVi = (Double)(item.FTonKhoanTaiDonVi.HasValue ? item.FTonKhoanTaiDonVi.Value : 0);
                    data.fTonKhoanTaiDonViDc = (Double)(item.FTonKhoanTaiDonViDc.HasValue ? item.FTonKhoanTaiDonViDc.Value : 0);

                    data.fGiaTriThuHoiNamTruocKhoBac = (Double)(item.FGiaTriThuHoiNamTruocKhoBac.HasValue ? item.FGiaTriThuHoiNamTruocKhoBac.Value : 0);
                    data.fGiaTriThuHoiNamTruocKhoBacDc = (Double)(item.FGiaTriThuHoiNamTruocKhoBacDc.HasValue ? item.FGiaTriThuHoiNamTruocKhoBacDc.Value : 0);
                    data.fGiaTriThuHoiNamTruocLenhChi = (Double)(item.FGiaTriThuHoiNamTruocLenhChi.HasValue ? item.FGiaTriThuHoiNamTruocLenhChi.Value : 0);
                    data.fGiaTriThuHoiNamTruocLenhChiDc = (Double)(item.FGiaTriThuHoiNamTruocLenhChiDc.HasValue ? item.FGiaTriThuHoiNamTruocLenhChiDc.Value : 0);                    
                    data.IIdParent = item.IIdParent;
                }
                else
                {
                    data.fGiaTrPhanBo = (Double)item.fChiTieuNganSach;
                    data.fCapPhatTaiKhoBac = item.FCapPhatTaiKhoBac.HasValue ? (Double)item.FCapPhatTaiKhoBac : 0;
                    data.fCapPhatBangLenhChi = item.FCapPhatBangLenhChi.HasValue ? (Double)item.FCapPhatBangLenhChi : 0;
                    data.fTonKhoanTaiDonVi = item.FTonKhoanTaiDonVi.HasValue ? (Double)item.FTonKhoanTaiDonVi : 0;
                    data.fGiaTriThuHoiNamTruocLenhChi = item.FGiaTriThuHoiNamTruocLenhChi.HasValue ? (Double)item.FGiaTriThuHoiNamTruocLenhChi : 0;
                    data.fGiaTriThuHoiNamTruocKhoBac = item.FGiaTriThuHoiNamTruocKhoBac.HasValue ? (Double)item.FGiaTriThuHoiNamTruocKhoBac : 0;
                }
                data.ILoaiDuAn = item.ILoaiDuAn;
                data.iID_TienTeID = Model.iId_TienTeId;
                data.sLNS = item.sLNS;
                data.sK = item.sK;
                data.sL = item.sL;
                data.sM = item.sM;
                data.sNG = item.sNG;
                data.sTM = item.sTM;
                data.sTrangThaiDuAnDangKy = item.sTrangThaiDuAn;
                data.sTTM = item.sTTM;
                List<string> lstXauNoiMa = new List<string>();
                lstXauNoiMa.Add(data.sLNS);
                lstXauNoiMa.Add(data.sL);
                lstXauNoiMa.Add(data.sK);
                lstXauNoiMa.Add(data.sM);
                lstXauNoiMa.Add(data.sTM);
                if(!string.IsNullOrEmpty(data.sTTM))
                {
                    lstXauNoiMa.Add(data.sTTM);
                }
                if (!string.IsNullOrEmpty(data.sNG))
                {
                    lstXauNoiMa.Add(data.sNG);
                }

                data.sXauNoiMa = string.Join("-", lstXauNoiMa.ToArray());


                data.sGhiChu = item.sGhiChu;
                data.IIdLoaiCongTrinh = item.iID_LoaiCongTrinhID;
                data.fThanhToanDeXuat = (Double)(item.fThanhToanDeXuat.HasValue ? item.fThanhToanDeXuat.Value : 0);
                data.IID_DuAn_HangMucID = item.IID_DuAn_HangMucID;
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new PhanBoVonChiTietInsertQueryNew();
            }
        }

        private void GetMucLucNganSachByParent()
        {
            try
            {
                var data = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
                _dicMucLucNganSach = new Dictionary<string, Guid>();
                if (data != null && data.Any())
                {
                    foreach (var n in data)
                    {
                        //string sKey = n.Lns + "-" + n.L + "-" + n.K + "-" + n.M + "-" + n.Tm + "-" + n.Ttm + "-" + n.Ng;
                        string sKey = n.Lns + "-" + n.L + "-" + n.K + "-" + n.M + "-" + n.Tm;
                        if (!_dicMucLucNganSach.ContainsKey(sKey))
                            _dicMucLucNganSach.Add(sKey, n.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateDropdownDuAn()
        {
            try
            {
                var lstData = Items.Select(n => new SelectedItemModel() { Value = n.iID_DuAnID.ToString(), DisplayName = string.Format("{0} - {1}", n.sMaDuAn, n.sTenDuAn) }).Distinct().ToList();
                lstData = lstData.GroupBy(n => new { n.DisplayName, n.Value }).Select(g => g.First()).ToList();
                DrpDuAn = new ObservableCollection<ComboboxItem>(_mapper.Map<List<ComboboxItem>>(lstData));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateDropdownLoaiCongTrinh()
        {
            try
            {
                var lstData = Items.Where(n => n.iID_LoaiCongTrinhID.HasValue).Select(n => new SelectedItemModel() { Value = n.iID_LoaiCongTrinhID.ToString(), DisplayName = n.sTenLoaiCongTrinh }).Distinct().ToList();
                lstData = lstData.GroupBy(n => new { n.DisplayName, n.Value }).Select(g => g.First()).ToList();
                DrpLoaiCongTrinh = new ObservableCollection<ComboboxItem>(_mapper.Map<List<ComboboxItem>>(lstData));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateDropdownCapPheDuyet()
        {
            try
            {
                var lstData = Items.Where(n => n.ILoaiDuAn.HasValue).Select(n => new SelectedItemModel()
                {
                    Value = n.ILoaiDuAn.ToString(),
                    DisplayName = n.ILoaiDuAn.Equals((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI) ? LoaiDuAnEnum.TypeName.KHOI_CONG_MOI : LoaiDuAnEnum.TypeName.CHUYEN_TIEP
                }).Distinct().ToList();
                lstData = lstData.GroupBy(n => new { n.DisplayName, n.Value }).Select(g => g.First()).ToList();
                DrpLoaiDuAn = new ObservableCollection<ComboboxItem>(_mapper.Map<List<ComboboxItem>>(lstData));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateDropDownDonViThucHienDuAn()
        {
            try
            {
                var lstData = Items.Where(n => !string.IsNullOrEmpty(n.IIdMaDonViThucHienDuAn)).Select(n => new SelectedItemModel() { Value = n.IIdMaDonViThucHienDuAn, DisplayName = string.Format("{0} - {1}", n.IIdMaDonViThucHienDuAn, n.STenDonViThucHienDuAn) }).Distinct().ToList();
                lstData = lstData.GroupBy(n => new { n.DisplayName, n.Value }).Select(g => g.First()).ToList();
                DrpDonViThucHienDuAns = new ObservableCollection<ComboboxItem>(_mapper.Map<List<ComboboxItem>>(lstData));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private TongHopNguonNSDauTuQuery GetMucLucNganSachByPhanBoVon(PhanBoVonChiTietModel item)
        {
            TongHopNguonNSDauTuQuery data = new TongHopNguonNSDauTuQuery();
            string sKey = item.sLNS + "-" + item.sL + "-" + item.sK + "-" + item.sM + "-" + item.sTM + "-" + item.sTTM + "-" + item.sNG;
            if (_dicMucLucNganSach.ContainsKey(sKey))
            {
                if (!string.IsNullOrEmpty(item.sNG))
                {
                    data.IIDNganhID = _dicMucLucNganSach[sKey];
                }
                else if (!string.IsNullOrEmpty(item.sTTM))
                {
                    data.IIDTietMucID = _dicMucLucNganSach[sKey];
                }
                else if (!string.IsNullOrEmpty(item.sTM))
                {
                    data.IIDTieuMucID = _dicMucLucNganSach[sKey];
                }
                else if (!string.IsNullOrEmpty(item.sM))
                {
                    data.IIDMucID = _dicMucLucNganSach[sKey];
                }
            }
            return data;
        }

        public override void OnClose(object obj)
        {
            try
            {
                var window = obj as Window;
                window.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
