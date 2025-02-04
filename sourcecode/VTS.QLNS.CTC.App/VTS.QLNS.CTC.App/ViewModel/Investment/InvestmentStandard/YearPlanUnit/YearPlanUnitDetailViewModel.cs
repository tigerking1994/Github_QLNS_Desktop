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
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.VonNamDonVi;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Helper;
using System.Drawing.Text;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.VonNamDonVi
{
    public class YearPlanUnitDetailViewModel : DetailViewModelBase<PhanBoVonDonViModel, PhanBoVonDonViChiTietModel>
    {
        private readonly IVdtKhvPhanBoVonDonViService _phanBoVonService;
        private readonly ILog _logger;
        private static string _sServiceName = "Quản lý đề xuất nhu cầu lập kế hoạch vốn năm";
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISysAuditLogService _log;
        private ICollectionView _phanBoVonView;
        private IMapper _mapper;

        public override string Title => "Đề xuất nhu cầu lập kế hoạch vốn năm";
        public override string Name => (Model != null && Model.IsViewDetail) ? "XEM KẾ HOẠCH VỐN NĂM ĐỀ XUẤT CHI TIẾT" : "KẾ HOẠCH VỐN NĂM ĐỀ XUẤT CHI TIẾT";
        public override string Description => string.Format("Số quyết định: {0} - Ngày quyết định: {1} - Năm kế hoạch: {2} - Đơn vị: {3}",
                                                Model.sSoQuyetDinh, Model.dNgayQuyetDinh.Value.ToString("dd/MM/yyyy"),
                                                Model.iNamKeHoach,
                                                _nsDonViService.FindByIdDonVi(Model.iID_MaDonViQuanLy, Model.iNamKeHoach) != null ?
                                                _nsDonViService.FindByIdDonVi(Model.iID_MaDonViQuanLy, Model.iNamKeHoach).TenDonVi : string.Empty);
        public override Type ContentType => typeof(VonNamDonViDetail);
        public Visibility HidenAdjust => !Model.IsAdjust ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowAdjust => Model.IsAdjust ? Visibility.Visible : Visibility.Collapsed;
        public bool IsAdjust => Model.IsAdjust;
        //public bool IsEnableProject => Model != null && !Model.IsViewDetail && Model.BActive && !Model.BKhoa && string.IsNullOrEmpty(Model.STongHop);
        public bool IsEnableProject => Model != null && !Model.IsViewDetail && Model.BActive && !Model.BKhoa;
        private bool _isCoppied = false;
        private bool _isCoppiedSave = false;
        #region Items header
        private string _sLuyKeVonHeader;
        public string sLuyKeVonHeader
        {
            get => _sLuyKeVonHeader;
            set => SetProperty(ref _sLuyKeVonHeader, value);
        }

        private string _sKeHoachVonHeader;
        public string sKeHoachVonHeader
        {
            get => _sKeHoachVonHeader;
            set => SetProperty(ref _sKeHoachVonHeader, value);
        }

        private string _sUocThucHienHeader;
        public string sUocThucHienHeader
        {
            get => _sUocThucHienHeader;
            set => SetProperty(ref _sUocThucHienHeader, value);
        }

        private string _sNhuCauVonHeader;
        public string sNhuCauVonHeader
        {
            get => _sNhuCauVonHeader;
            set => SetProperty(ref _sNhuCauVonHeader, value);
        }

        private string _sLuyKeVonDaDuocBoTriHetNamHeader;
        public string SLuyKeVonDaDuocBoTriHetNamHeader
        {
            get => _sLuyKeVonDaDuocBoTriHetNamHeader;
            set => SetProperty(ref _sLuyKeVonDaDuocBoTriHetNamHeader, value);
        }
        #endregion

        public bool IsEdit => Model.iID_ParentId != null;
        public bool IsEditThuHoiVonUngTruoc => Model.iId_NguonVonId != 1 ? IsEdit : !IsEdit;
        public Visibility VisibilityModified => Model.iID_ParentId != null ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisibilityNew => Model.iID_ParentId != null ? Visibility.Collapsed : Visibility.Visible;

        #region Item tong

        private PhanBoVonDonViChiTietModel _summaryItems = new PhanBoVonDonViChiTietModel();
        public PhanBoVonDonViChiTietModel SummaryItems
        {
            get => _summaryItems;
            set => SetProperty(ref _summaryItems, value);
        }

        #endregion

        #region data combobox
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

        private ObservableCollection<ComboboxItem> _drpMaDuAn;
        public ObservableCollection<ComboboxItem> DrpMaDuAn
        {
            get => _drpMaDuAn;
            set => SetProperty(ref _drpMaDuAn, value);
        }

        private ComboboxItem _drpMaDuAnSelected;
        public ComboboxItem DrpMaDuAnSelected
        {
            get => _drpMaDuAnSelected;
            set
            {
                SetProperty(ref _drpMaDuAnSelected, value);
                OnSearch();
            }
        }

        public Visibility ModifiedVisibility
        {
            get => (Model != null && Model.iID_ParentId.HasValue) ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion

        public RelayCommand SaveDataCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }

        public RelayCommand CopyCommand { get; }

        public YearPlanUnitDetailViewModel(
            IMapper mapper,
            ILog logger,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtKhvPhanBoVonDonViService phanBoVonService,
            INsMucLucNganSachService mucLucNganSachService,
            ISysAuditLogService log)
        {
            _logger = logger;
            _phanBoVonService = phanBoVonService;
            _mucLucNganSachService = mucLucNganSachService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mapper = mapper;
            _log = log;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());

            CopyCommand = new RelayCommand(obj => ConfirmCopyData());
        }

        public override void Init()
        {
            try
            {
                LoadData();
                OnResetFilter();

                _sLuyKeVonHeader = string.Format("Lũy kế vốn thực hiện đến cuối năm {0}", (Model.iNamKeHoach - 2));
                _sKeHoachVonHeader = string.Format("Kế hoạch vốn năm {0}", (Model.iNamKeHoach - 1));
                _sUocThucHienHeader = string.Format("Ước thực hiện năm {0}", (Model.iNamKeHoach - 1));
                _sNhuCauVonHeader = string.Format("Nhu cầu vốn năm {0}", (Model.iNamKeHoach));
                _sLuyKeVonDaDuocBoTriHetNamHeader = string.Format("Lũy kế vốn đã được bố trí hết năm {0}", (Model.iNamKeHoach - 1));

                OnPropertyChanged(nameof(sLuyKeVonHeader));
                OnPropertyChanged(nameof(sKeHoachVonHeader));
                OnPropertyChanged(nameof(sUocThucHienHeader));
                OnPropertyChanged(nameof(sNhuCauVonHeader));
                OnPropertyChanged(nameof(SLuyKeVonDaDuocBoTriHetNamHeader));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        #region Relay Command
        public override void LoadData(params object[] args)
        {
            try
            {
                List<VdtKhvPhanBoVonDonViChiTietQuery> lstDuAn = new List<VdtKhvPhanBoVonDonViChiTietQuery>();
                List<VdtKhvPhanBoVonDonViChiTietQuery> lstDuAnEdit = new List<VdtKhvPhanBoVonDonViChiTietQuery>();
                lstDuAn = _phanBoVonService.GetAllDuAnInPhanBoVon(Model.iNamKeHoach, Model.dNgayQuyetDinh.Value, Model.iID_MaDonViQuanLy, Model.iId_NguonVonId, -1).ToList();
                //lstDuAn = lstDuAn.GroupBy(x => new { x.iID_DuAnID, x.iID_LoaiCongTrinhID, x.IID_DuAn_HangMucID }).Select(grp => grp.FirstOrDefault()).ToList();
                lstDuAn = lstDuAn.ToList();
                if (Model.LstDuAnId != null)
                {
                    lstDuAn = lstDuAn.Where(x => Model.LstDuAnId.Contains(x.iID_DuAnID)).ToList();
                }
                else
                {
                    lstDuAn = new List<VdtKhvPhanBoVonDonViChiTietQuery>();
                }

                lstDuAnEdit = _phanBoVonService.GetPhanBoVonChiTietByParentId(Model.Id).ToList();
                lstDuAnEdit = lstDuAnEdit.GroupBy(n => new { n.iID_DuAnID, n.IID_DuAn_HangMucID }).Select(group => group.First()).ToList();

                if (Model != null && Model.LstDuAnId != null && Model.IsChooseDuAn && string.IsNullOrEmpty(Model.STongHop))
                {
                    lstDuAnEdit = lstDuAnEdit.Where(x => Model.LstDuAnId.Contains(x.iID_DuAnID)).ToList();
                }

                if (lstDuAnEdit != null && lstDuAnEdit.Count != 0)
                {
                    lstDuAn = lstDuAn.Where(x => !lstDuAnEdit.Any(y => x.iID_DuAnID == y.iID_DuAnID && x.iID_LoaiCongTrinhID == y.iID_LoaiCongTrinhID)).ToList();
                    lstDuAn.AddRange(lstDuAnEdit);
                }

                Items = _mapper.Map<ObservableCollection<PhanBoVonDonViChiTietModel>>(lstDuAn.OrderBy(n => n.sTenDuAn));

                //Items.Select(x => { x.BActive = Model.BActive && !Model.IsViewDetail && !Model.BKhoa && string.IsNullOrEmpty(Model.STongHop); return x; }).ToList();
                Items.Select(x => { x.BActive = Model.BActive && !Model.IsViewDetail && !Model.BKhoa; return x; }).ToList();            // cho sửa bản ghi tổng hợp

                Items.Select(x => { x.PropertyChanged += DetailModel_PropertyChanged; return x; }).ToList();

                //foreach (var item in Items)
                //{
                //    if (item.iID_MaNguonNganSach != Model.iId_NguonVonId)
                //    {
                //        item.fTongMucDauTuDuocDuyet = 0;
                //        item.FKeHoachTrungHanDuocDuyet = 0;
                //    }
                //}

                CreateDropdownDuAn();
                CreateDropdownCapPheDuyet();
                CreateDropdownLoaiCongTrinh();
                CreateDropdownMaDuAn();

                _phanBoVonView = CollectionViewSource.GetDefaultView(Items);
                _phanBoVonView.Filter = VdtKhPhanBoVonFilter;
                SumTotalItem();
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
                if (args.PropertyName == nameof(PhanBoVonDonViChiTietModel.fVonKeoDaiCacNamTruoc)
                    || args.PropertyName == nameof(PhanBoVonDonViChiTietModel.fUocThucHien)
                    || args.PropertyName == nameof(PhanBoVonDonViChiTietModel.FUocThucHienSauDc)
                    || args.PropertyName == nameof(PhanBoVonDonViChiTietModel.fThuHoiVonUngTruoc)
                    || args.PropertyName == nameof(PhanBoVonDonViChiTietModel.FThuHoiVonUngTruocSauDc)
                    || args.PropertyName == nameof(PhanBoVonDonViChiTietModel.fThanhToan)
                    || args.PropertyName == nameof(PhanBoVonDonViChiTietModel.FThanhToanSauDc))
                {
                    PhanBoVonDonViChiTietModel item = (PhanBoVonDonViChiTietModel)sender;
                    item.IsModified = true;
                    SumTotalItem();
                }
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
                List<PhanBoVonDonViChiTietModel> lstDataNew = new List<PhanBoVonDonViChiTietModel>();

                if (Model.iID_ParentId.HasValue)
                {
                    lstDataNew = Items.Where(x => !x.IsDeleted && (x.FThuHoiVonUngTruocSauDc != 0 && x.FThuHoiVonUngTruocSauDc.HasValue) || (x.FThanhToanSauDc.HasValue && x.FThanhToanSauDc != 0)).ToList();
                }
                else
                {
                    lstDataNew = Items.Where(x => !x.IsDeleted && ((x.fThuHoiVonUngTruoc != 0 && x.fThuHoiVonUngTruoc.HasValue) || (x.fThanhToan != 0 && x.fThanhToan.HasValue))).ToList();
                }

                if (lstDataNew == null || lstDataNew.Count == 0)
                {
                    messageBuilder.Append(Resources.MsgErrorDataEmpty);
                    MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                    return;
                }
                if (messageBuilder.Length != 0)
                {
                    MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                    return;
                }

                if (Model.iID_ParentId.HasValue)
                {
                    lstDataNew = lstDataNew.Select(x =>
                    {
                        x.fUocThucHien = x.FUocThucHienSauDc ?? 0;
                        x.fThuHoiVonUngTruoc = x.FThuHoiVonUngTruocSauDc ?? 0;
                        //x.fThanhToan = x.FThanhToanSauDc ?? 0;
                        return x;
                    }).ToList();
                }

                lstDataNew = lstDataNew.Select(x => { x.BActive = true; return x; }).ToList();
                List<VdtKhvPhanBoVonDonViChiTiet> lstData = new List<VdtKhvPhanBoVonDonViChiTiet>();
                if (Model.IsAdjust)
                {
                    var listDataSave = lstDataNew.Clone().Select(x =>
                    {
                        x.fThanhToan = x.FThanhToanSauDc ?? 0;
                        return x;
                    });
                    lstData = _mapper.Map<List<VdtKhvPhanBoVonDonViChiTiet>>(listDataSave);
                }
                else
                {
                    lstData = _mapper.Map<List<VdtKhvPhanBoVonDonViChiTiet>>(lstDataNew);
                }

                bool isSucess = _phanBoVonService.CreatePhanBoVonChiTiet(Model.Id, lstData);

                _log.WriteLog(Resources.ApplicationName, _sServiceName, (int)TypeExecute.Update, dStartDate, TransactionStatus.Success, _sessionService.Current.Principal);
                messageBuilder.AppendFormat(Resources.MsgSaveDone);
                MessageBox.Show(messageBuilder.ToString());
                if (!Model.IsEdit && !Model.IsAdjust)
                {
                    Model.IsEdit = true;
                }
                LoadData();
                _isCoppiedSave = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ConfirmCopyData()
        {
            var result = MessageBoxHelper.Confirm(Resources.ConfirmCopyData);
            if (result == MessageBoxResult.Yes)
            {
                Items.ForAll(f =>
                {
                    f.fThanhToan = f.fTongMucDauTuDuocDuyet;
                });
                _phanBoVonView = CollectionViewSource.GetDefaultView(Items);
                _phanBoVonView.Filter = VdtKhPhanBoVonFilter;
                _isCoppied = true;
            }
        }

        public void OnSearch()
        {
            if (_phanBoVonView != null)
            {
                _phanBoVonView.Refresh();
            }
            SumTotalItem();
        }

        private void OnResetFilter()
        {
            DrpLoaiCongTrinhSelected = null;
            _drpLoaiDuAnSelected = null;
            DrpDuAnSelected = null;
            DrpMaDuAnSelected = null;
            OnPropertyChanged(nameof(DrpLoaiCongTrinhSelected));
            OnPropertyChanged(nameof(DrpLoaiDuAnSelected));
            OnPropertyChanged(nameof(DrpDuAnSelected));
            OnPropertyChanged(nameof(DrpMaDuAnSelected));
            OnSearch();
        }
        #endregion

        #region Helper
        private bool VdtKhPhanBoVonFilter(object obj)
        {
            try
            {
                if (!(obj is PhanBoVonDonViChiTietModel temp)) return true;
                var bCondition = true;
                if (DrpLoaiCongTrinhSelected != null)
                {
                    bCondition &= (temp.iID_LoaiCongTrinhID.HasValue && temp.iID_LoaiCongTrinhID == Guid.Parse(DrpLoaiCongTrinhSelected.ValueItem));
                }
                if (DrpLoaiDuAnSelected != null)
                {
                    bCondition &= (temp.ILoaiDuAn.HasValue && temp.ILoaiDuAn.Value == Int32.Parse(DrpLoaiDuAnSelected.ValueItem));
                }
                if (DrpDuAnSelected != null)
                {
                    bCondition &= (temp.iID_DuAnID == Guid.Parse(DrpDuAnSelected.ValueItem));
                }

                if (DrpMaDuAnSelected != null)
                {
                    bCondition &= (temp.sMaDuAn == DrpMaDuAnSelected.ValueItem);
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
                _summaryItems.fTongMucDauTuDuocDuyet = Items.Where(x => x.IsFilter).Sum(n => n.fTongMucDauTuDuocDuyet ?? 0);
                _summaryItems.fLuyKeVonNamTruoc = Items.Where(x => x.IsFilter).Sum(n => n.fLuyKeVonNamTruoc ?? 0);
                _summaryItems.fKeHoachVonDuocDuyetNamNay = Items.Where(x => x.IsFilter).Sum(n => n.fKeHoachVonDuocDuyetNamNay ?? 0);
                _summaryItems.fVonKeoDaiCacNamTruoc = Items.Where(x => x.IsFilter).Sum(n => n.fVonKeoDaiCacNamTruoc ?? 0);
                _summaryItems.fUocThucHien = Items.Where(x => x.IsFilter).Sum(n => n.fUocThucHien ?? 0);
                _summaryItems.FUocThucHienSauDc = Items.Where(x => x.IsFilter).Sum(n => n.FUocThucHienSauDc ?? 0);
                _summaryItems.FLuyKeVonDaBoTriHetNam = Items.Where(x => x.IsFilter).Sum(n => n.FLuyKeVonDaBoTriHetNam);
                _summaryItems.fThuHoiVonUngTruoc = Items.Where(x => x.IsFilter).Sum(n => n.fThuHoiVonUngTruoc ?? 0);
                _summaryItems.FThuHoiVonUngTruocSauDc = Items.Where(x => x.IsFilter).Sum(n => n.FThuHoiVonUngTruocSauDc ?? 0);
                _summaryItems.fThanhToan = Items.Where(x => x.IsFilter).Sum(n => n.fThanhToan ?? 0);
                _summaryItems.FThanhToanSauDc = Items.Where(x => x.IsFilter).Sum(x => x.FThanhToanSauDc);
                _summaryItems.FKeHoachTrungHanDuocDuyet = Items.Where(x => x.IsFilter).Sum(x => x.FKeHoachTrungHanDuocDuyet ?? 0);

                OnPropertyChanged(nameof(SummaryItems));
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
            var lstData = new List<ComboboxItem>()
            {
                new ComboboxItem(){DisplayItem = "Khởi công mới", ValueItem = ((int)(LoaiDuAnEnum.Type.KHOI_CONG_MOI)).ToString()},
                new ComboboxItem(){DisplayItem = "Chuyển tiếp", ValueItem = ((int)(LoaiDuAnEnum.Type.CHUYEN_TIEP)).ToString()}
            };

            DrpLoaiDuAn = new ObservableCollection<ComboboxItem>(lstData);
        }

        private void CreateDropdownMaDuAn()
        {
            try
            {
                var lstData = Items.Select(n => new SelectedItemModel() { Value = n.sMaDuAn, DisplayName = n.sMaDuAn }).Distinct().ToList();
                lstData = lstData.GroupBy(n => new { n.DisplayName, n.Value }).Select(g => g.First()).ToList();
                DrpMaDuAn = new ObservableCollection<ComboboxItem>(_mapper.Map<List<ComboboxItem>>(lstData));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        public override void OnClose(object obj)
        {
            try
            {
                //todo check dữ liệu được sao chép trong DB

                if(_isCoppied && !_isCoppiedSave)
                {
                    MessageBoxHelper.WarningCopyBeforeClose(Resources.WarningCopyBeforeClose);
                }
                
                
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
