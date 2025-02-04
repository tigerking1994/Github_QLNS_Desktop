using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AutoMapper;
using log4net;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachVonNamDuocDuyet
{
    public class KHVNamDuocDuyetDetailViewModel : DetailViewModelBase<VdtKhvPhanBoVonDonViPheDuyetModel, VdtKhvPhanBoVonDonViChiTietPheDuyetModel>
    {
        private readonly IVdtKhvPhanBoVonDonViChiTietPheDuyetService _iVdtKhvPhanBoVonDonViChiTietPheDuyetService;
        private static string _sServiceName = "Chứng từ chi tiết kế hoạch vốn năm được duyệt ";
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IMucLucNganSachService _mlMucLucNganSach;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISysAuditLogService _log;
        private readonly IVdtKhvPhanBoVonDonViService _vdtKhvPhanBoVonDonViService;
        private readonly IVdtKhvPhanBoVonDonViPheDuyetService _iVdtKhvPhanBoVonDonViPheDuyetService;
        private readonly ILog _logger;
        private ICollectionView _phanBoVonView;
        private IMapper _mapper;
        private static Dictionary<string, Guid> _dicMucLucNganSach = new Dictionary<string, Guid>();

        public override string Title => "Kế hoạch vốn năm được duyệt";
        public override string Name => (Model != null && Model.IsViewDetail) ? "XEM KẾ HOẠCH VỐN NĂM ĐƯỢC DUYỆT CHI TIẾT" : "KẾ HOẠCH VỐN NĂM ĐƯỢC DUYỆT CHI TIẾT";
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
        private VdtKhvPhanBoVonDonViChiTietPheDuyetModel _summaryItems = new VdtKhvPhanBoVonDonViChiTietPheDuyetModel();
        public VdtKhvPhanBoVonDonViChiTietPheDuyetModel SummaryItems
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
        public string HeaderColumnPhanBo { get; set; } = "Kế hoạch phân bổ năm";
        public string HeaderColumnThanhToanDeNghi { get; set; } = "Giá trị thanh toán";
        public string HeaderColumnPhanBoDieuChinh { get; set; } = "Kế hoạch phân bổ năm";
        public bool IsNganSachNhaNuoc { get; set; }
        public bool IsNganSachNhaNuocDieuChinh { get; set; }

        public KHVNamDuocDuyetDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtKhvPhanBoVonDonViChiTietPheDuyetService iVdtKhvPhanBoVonDonViChiTietPheDuyetService,
            ITongHopNguonNSDauTuService tonghopService,
            INsMucLucNganSachService mucLucNganSachService,
            IMucLucNganSachService mlMucLucNganSach,
            IVdtKhvPhanBoVonDonViService vdtKhvPhanBoVonDonViService,
            IVdtKhvPhanBoVonDonViPheDuyetService iVdtKhvPhanBoVonDonViPheDuyetService,
            ILog logger,
            ISysAuditLogService log)
        {
            _iVdtKhvPhanBoVonDonViChiTietPheDuyetService = iVdtKhvPhanBoVonDonViChiTietPheDuyetService;
            _mucLucNganSachService = mucLucNganSachService;
            _vdtKhvPhanBoVonDonViService = vdtKhvPhanBoVonDonViService;
            _logger = logger;
            _tonghopService = tonghopService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mlMucLucNganSach = mlMucLucNganSach;
            _iVdtKhvPhanBoVonDonViPheDuyetService = iVdtKhvPhanBoVonDonViPheDuyetService;
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
                var predicate = PredicateBuilder.True<VdtKhvPhanBoVonDonVi>();
                predicate = predicate.And(x => x.INamKeHoach == Model.iNamKeHoach);
                predicate = predicate.And(x => x.IIdMaDonViQuanLy == Model.iID_MaDonViQuanLy);
                predicate = predicate.And(x => x.IIdNguonVonId == Model.iId_NguonVonId);
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.STongHop));

                var itemQuery = _vdtKhvPhanBoVonDonViService.FindByCondition(predicate).ToList();

                DrpVoucherSuggestionAgregates = _mapper.Map<ObservableCollection<ComboboxItem>>(itemQuery);


                // ?? không biết dòng code này để làm gì, đang gây ra lỗi không chọn lại được KHVN đề xuất khi sửa KHVN được duyệt.                    
                //if(phanBoVonDonViPheDuyet != null && phanBoVonDonViPheDuyet.IIdVonNamDeXuatId != null)
                //{
                //    DrpVoucherSuggestionAgregates = new ObservableCollection<ComboboxItem>( DrpVoucherSuggestionAgregates.Where(x => x.Id == phanBoVonDonViPheDuyet.IIdVonNamDeXuatId));  
                //}

                if (DrpVoucherSuggestionAgregates.Count() > 0 && DrpVoucherSuggestionSelected == null)
                {
                    var phanBoVonDonViPheDuyet = _iVdtKhvPhanBoVonDonViPheDuyetService.FindById(Model.Id);
                    if (phanBoVonDonViPheDuyet?.IIdVonNamDeXuatId != null)
                    {
                        DrpVoucherSuggestionSelected = DrpVoucherSuggestionAgregates.FirstOrDefault(x => x.Id == phanBoVonDonViPheDuyet.IIdVonNamDeXuatId);
                    }
                }

                


                var a = IsAdjust;
                var b = BIsDetail;
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

                    List<PhanBoVonDonViChiTietPheDuyetQuery> lstDuAn = new List<PhanBoVonDonViChiTietPheDuyetQuery>();
                    List<PhanBoVonDonViChiTietPheDuyetQuery> lstDuAnEdit = new List<PhanBoVonDonViChiTietPheDuyetQuery>();
                    //GetMucLucNganSachByParent();

                    lstDuAn = _iVdtKhvPhanBoVonDonViChiTietPheDuyetService.GetAllDuAnInPhanBoVon((DrpVoucherSuggestionSelected != null ? DrpVoucherSuggestionSelected.ValueItem : string.Empty), Model.iId_NguonVonId.Value).ToList();

                    if (!Model.iId_ParentId.HasValue)
                    {
                        lstDuAnEdit = _iVdtKhvPhanBoVonDonViChiTietPheDuyetService.GetPhanBoVonChiTietByParentIdClone(Model.Id);
                        if (Model.IsAdjust)
                        {
                            lstDuAnEdit = lstDuAnEdit.Select(n => { n.fChiTieuNganSach = n.fChiTieuGoc; return n; }).ToList();
                        }
                    }
                    else if (Model.iId_ParentId.HasValue)
                    {
                        var itemlstDuAnDieuChinh = _iVdtKhvPhanBoVonDonViChiTietPheDuyetService.GetPhanBoVonChiTietDieuChinhByParentId(Model.Id);
                        lstDuAnEdit = _mapper.Map<List<PhanBoVonDonViChiTietPheDuyetQuery>>(itemlstDuAnDieuChinh.Where(x => x.iID_DuAnID != Guid.Empty && x.iID_LoaiCongTrinhID != null));
                    }

                    // lstDuAnEdit = UpdateFThanhToanDeXuatForListDuAn(lstDuAnEdit, lstDuAn);

                    if (lstDuAnEdit != null && lstDuAnEdit.Count != 0)
                    {
                        lstDuAn = lstDuAn.Where(n => !lstDuAnEdit.Any(m => m.iID_DuAnID == n.iID_DuAnID && m.iID_LoaiCongTrinhID == n.iID_LoaiCongTrinhID && m.iID_DuAn_HangMucID == n.iID_DuAn_HangMucID)).ToList();
                        lstDuAn.AddRange(lstDuAnEdit);
                    }

                    if (Model.iId_ParentId.HasValue)
                    {
                        var lstDuAnEditGoc = _iVdtKhvPhanBoVonDonViChiTietPheDuyetService.GetPhanBoVonChiTietByParentId(Model.iId_ParentId.Value);
                        foreach (var dc in lstDuAn)
                        {
                            dc.FGiaTrPhanBo = lstDuAnEditGoc
                                .Where(x => x.iID_DuAnID.Equals(dc.iID_DuAnID) &&
                                            x.iID_LoaiCongTrinhID == dc.iID_LoaiCongTrinhID)
                                .Sum(x => x.FGiaTrPhanBo.GetValueOrDefault());
                            dc.FGiaTriThuHoi = lstDuAnEditGoc
                                .Where(x => x.iID_DuAnID.Equals(dc.iID_DuAnID) &&
                                            x.iID_LoaiCongTrinhID == dc.iID_LoaiCongTrinhID)
                                .Sum(x => x.FGiaTriThuHoi.GetValueOrDefault());
                        }
                    }

                    Items = _mapper.Map<ObservableCollection<VdtKhvPhanBoVonDonViChiTietPheDuyetModel>>(lstDuAn);

                    Items.Select(x => { x.BActive = Model.BActive && !Model.IsViewDetail && !Model.BKhoa; return x; }).ToList();

                    CreateDropdownDuAn();
                    CreateDropdownCapPheDuyet();
                    CreateDropdownLoaiCongTrinh();
                    CreateDropDownDonViThucHienDuAn();
                    SumTotalItem();
                    LoadHeaderPhanBo();
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
                    VdtKhvPhanBoVonDonViChiTietPheDuyetModel newItem = ObjectCopier.Clone(sourceItem);
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
                List<VdtKhvPhanBoVonDonViChiTietPheDuyetModel> lstDataNew = new List<VdtKhvPhanBoVonDonViChiTietPheDuyetModel>();

                if (Model.iId_ParentId.HasValue)
                {
                    Model.IsEdit = true;
                    lstDataNew = Items.Where(n => ((n.FGiaTriThuHoiDc.HasValue && n.FGiaTriThuHoiDc.Value != 0)
                                                    || (n.FGiaTrPhanBoDc.HasValue && n.FGiaTrPhanBoDc.Value != 0)) && !n.IsDeleted).ToList();
                }
                else
                {
                    lstDataNew = Items.Where(n => ((n.FGiaTriThuHoi.HasValue && n.FGiaTriThuHoi.Value != 0)
                                                    || (n.FGiaTrPhanBo.HasValue && n.FGiaTrPhanBo.Value != 0)) && !n.IsDeleted).ToList();
                }

                if (lstDataNew == null || lstDataNew.Count == 0)
                {
                    messageBuilder.Append(Resources.MsgErrorDataEmpty);
                    MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                    return;
                }
                List<string> lstXauNoiChuoi;
                List<PhanBoVonDonViChiTietPheDuyetInsertQuery> lstData = new List<PhanBoVonDonViChiTietPheDuyetInsertQuery>();
                foreach (var item in lstDataNew)
                {
                    lstData.Add(ConvertDataInsert(item));
                }
                if (messageBuilder.Length != 0)
                {
                    MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                    return;
                }

                bool isSucess = _iVdtKhvPhanBoVonDonViChiTietPheDuyetService.CreatePhanBoVonChiTiet(Model.Id, (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet, lstData, _sessionService.Current.Principal, Model.IsEdit);
                _log.WriteLog(Resources.ApplicationName, _sServiceName, (int)TypeExecute.Update, dStartDate, TransactionStatus.Success, _sessionService.Current.Principal);
                ProcessXuLyDuLieu();
                UpdateIdVonNamDeXuat();

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

        /// <summary>
        /// Update trường giá trị thanh toán đề xuất cho các phần tử trong lstDuAnEdit, vì list này lấy từ bảng đề xuất nên k có trường fThanhToanDeXuat
        /// </summary>
        /// <param name="lstDuAnEdit"></param>
        /// <param name="lstDuAn"></param>
        private List<PhanBoVonDonViChiTietPheDuyetQuery> UpdateFThanhToanDeXuatForListDuAn(List<PhanBoVonDonViChiTietPheDuyetQuery> lstDuAnEdit, List<PhanBoVonDonViChiTietPheDuyetQuery> lstDuAn)
        {
            lstDuAn.ForEach(da =>
            {
                PhanBoVonDonViChiTietPheDuyetQuery duan = lstDuAnEdit.FirstOrDefault(d => d.iID_DuAnID == da.iID_DuAnID && d.iID_LoaiCongTrinhID == da.iID_LoaiCongTrinhID);
                if(duan != null)
                {
                    lstDuAnEdit = lstDuAnEdit.Where(d => d.iID_DuAnID != da.iID_DuAnID || d.iID_LoaiCongTrinhID != da.iID_LoaiCongTrinhID).ToList();
                    duan.fThanhToanDeXuat = da.fThanhToanDeXuat;
                    lstDuAnEdit.Add(duan);
                }                
            });
            return lstDuAnEdit;
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
                if (args.PropertyName == nameof(VdtKhvPhanBoVonDonViChiTietPheDuyetModel.FGiaTriThuHoi)
                || args.PropertyName == nameof(VdtKhvPhanBoVonDonViChiTietPheDuyetModel.FGiaTrPhanBo)
                || args.PropertyName == nameof(VdtKhvPhanBoVonDonViChiTietPheDuyetModel.FGiaTriThuHoiDc)
                || args.PropertyName == nameof(VdtKhvPhanBoVonDonViChiTietPheDuyetModel.FGiaTrPhanBoDc))
                {
                    VdtKhvPhanBoVonDonViChiTietPheDuyetModel item = (VdtKhvPhanBoVonDonViChiTietPheDuyetModel)sender;
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
                if (!(obj is VdtKhvPhanBoVonDonViChiTietPheDuyetModel temp)) return true;
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
                _summaryItems.FGiaTriThuHoi = Items.Where(x => x.IsFilter).Sum(x => x.FGiaTriThuHoi.GetValueOrDefault());
                _summaryItems.FGiaTriThuHoiDc = Items.Where(x => x.IsFilter).Sum(x => x.FGiaTriThuHoiDc.GetValueOrDefault());
                _summaryItems.FGiaTrPhanBo = Items.Where(x => x.IsFilter).Sum(x => x.FGiaTrPhanBo.GetValueOrDefault());
                _summaryItems.FGiaTrPhanBoDc = Items.Where(x => x.IsFilter).Sum(x => x.FGiaTrPhanBoDc.GetValueOrDefault());
                OnPropertyChanged(nameof(SummaryItems));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadHeaderPhanBo()
        {
            if (Model != null)
            {
                //if (Model.iId_NguonVonId.HasValue && !Model.iId_NguonVonId.ToString().Equals("1"))
                //{
                //    HeaderColumnPhanBo = "Kế hoạch phân bổ năm";
                //    HeaderColumnPhanBoDieuChinh = "Kế hoạch phân bổ năm (Sau điều chỉnh)";
                //    //HeaderColumnPhanBo = "Trả nợ XDCB";
                //    //HeaderColumnPhanBoDieuChinh = "Trả nợ XDCB (Sau điều chỉnh)";
                //    IsNganSachNhaNuoc = false;
                //    if (Model.iId_ParentId.HasValue)
                //    {
                //        IsNganSachNhaNuocDieuChinh = true;
                //    }
                //    else
                //    {
                //        HeaderColumnPhanBo = "Kế hoạch phân bổ năm";
                //        HeaderColumnPhanBoDieuChinh = "Kế hoạch phân bổ năm (Sau điều chỉnh)";
                //        IsNganSachNhaNuocDieuChinh = false;
                //    }
                //}
                //else
                //{
                    HeaderColumnPhanBo = "Kế hoạch phân bổ năm";
                    HeaderColumnPhanBoDieuChinh = "Kế hoạch phân bổ năm (Sau điều chỉnh)";
                    IsNganSachNhaNuoc = false;
                    IsNganSachNhaNuocDieuChinh = false;
                //}
                OnPropertyChanged(nameof(HeaderColumnPhanBo));
            }
        }

        public void GetInfoDuAn()
        {
            //try
            //{
            //    string sXauNoiChuoi = SelectedItem.sLNS + "-" + SelectedItem.sL + "-" + SelectedItem.sK + "-" + SelectedItem.sM + "-" + SelectedItem.sTM + "-" + SelectedItem.sTTM + "-" + SelectedItem.sNG;
            //    if (!_dicMucLucNganSach.ContainsKey(sXauNoiChuoi))
            //    {
            //        MessageBox.Show(Resources.MsgErrorMucLucNganSachNotExist);
            //        SelectedItem.IsDeleted = true;
            //        return;
            //    }
            //    int iItemExist = Items.Where(n => n.iID_DuAnID == SelectedItem.iID_DuAnID && n.sL == SelectedItem.sL && n.sK == SelectedItem.sK && n.sM == SelectedItem.sM && n.sTM == SelectedItem.sTM && n.sTTM == SelectedItem.sTTM && n.sNG == SelectedItem.sNG && !n.IsDeleted).Count();
            //    if (iItemExist != 1)
            //    {
            //        MessageBox.Show(Resources.MsgErrorDataExist);
            //        SelectedItem.IsDeleted = true;
            //        return;
            //    }
            //    if (SelectedItem.fChiTieuNganSach != 0) return;

            //    Dictionary<string, Guid> data = _mlMucLucNganSach.GetLoaiNganSachByNamLamViec(Model.iNamKeHoach.Value).GroupBy(x => x.Lns).ToDictionary(x => x.Key, x => x.Select(x => x.Id).FirstOrDefault());

            //    if (data.Keys.Contains(SelectedItem.sLNS))
            //    {
            //        var duAnInfo = _iVdtKhvPhanBoVonDonViChiTietPheDuyetService.GetVonDaBoTriByDuAnIdAnMucLucNganSach((int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet, SelectedItem.iID_DuAnID, Model.iID_MaDonViQuanLy, Model.iNamKeHoach.Value,
            //        Model.dNgayQuyetDinh.Value, data[SelectedItem.sLNS], Model.iId_NguonVonId ?? 0, SelectedItem.sL, SelectedItem.sK, SelectedItem.sM, SelectedItem.sTM, SelectedItem.sTTM, SelectedItem.sNG);
            //        SelectedItem.fVonDaBoTri = duAnInfo.fVonDaBoTri;
            //        SelectedItem.fChiTieuDauNam = duAnInfo.fChiTieuDauNam;
            //        SelectedItem.fVonConLai = SelectedItem.fGiaTriDauTu - duAnInfo.fVonDaBoTri;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.Error(ex.Message, ex);
            //}
        }

        private PhanBoVonDonViChiTietPheDuyetInsertQuery ConvertDataInsert(VdtKhvPhanBoVonDonViChiTietPheDuyetModel item)
        {
            try
            {
                PhanBoVonDonViChiTietPheDuyetInsertQuery data = new PhanBoVonDonViChiTietPheDuyetInsertQuery();
                data.fGiaTrDeNghi = null;
                data.fGiaTriThuHoi = null;
                data.fTiGia = (double)(Model.fTiGiaDonVi ?? 0);
                data.fTiGiaDonVi = (double)(Model.fTiGiaDonVi ?? 0);
                data.iID_DonViTienTeID = Model.iId_DonViTienTeId;
                data.iID_DuAnID = item.iID_DuAnID;
                data.iID_LoaiNguonVonID = Model.iId_LoaiNguonVonId;
                data.iID_PhanBoVonID = Model.Id;
                if (Model.iId_ParentId.HasValue)
                {
                    data.fGiaTriThuHoi = item.FGiaTriThuHoiDc.GetValueOrDefault();
                    data.fGiaTrPhanBo = item.FGiaTrPhanBoDc.GetValueOrDefault();
                    data.IIdParent = Model.iId_ParentId.Value;//item.IIdParent;
                }
                else
                {
                    data.fGiaTriThuHoi = item.FGiaTriThuHoi.GetValueOrDefault();
                    data.fGiaTrPhanBo = item.FGiaTrPhanBo.GetValueOrDefault();
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
                data.sGhiChu = item.sGhiChu;
                data.IIdLoaiCongTrinh = item.iID_LoaiCongTrinhID;
                data.fThanhToanDeXuat = item.fThanhToanDeXuat;
                data.IID_DuAn_HangMucID = item.IID_DuAn_HangMucID;
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new PhanBoVonDonViChiTietPheDuyetInsertQuery();
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
                        string sKey = n.Lns + "-" + n.L + "-" + n.K + "-" + n.M + "-" + n.Tm + "-" + n.Ttm + "-" + n.Ng;
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

        private TongHopNguonNSDauTuQuery GetMucLucNganSachByPhanBoVon(VdtKhvPhanBoVonDonViChiTietPheDuyetModel item)
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

        private void UpdateIdVonNamDeXuat()
        {
            var phanBoVonDonViPheDuyet = _iVdtKhvPhanBoVonDonViPheDuyetService.FindById(Model.Id);
            try
            {
                if (phanBoVonDonViPheDuyet != null && DrpVoucherSuggestionSelected != null && DrpVoucherSuggestionSelected.Id != null)
                {
                    phanBoVonDonViPheDuyet.IIdVonNamDeXuatId = DrpVoucherSuggestionSelected.Id;
                    _iVdtKhvPhanBoVonDonViPheDuyetService.Update(phanBoVonDonViPheDuyet);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

        }
        #endregion
    }
}
