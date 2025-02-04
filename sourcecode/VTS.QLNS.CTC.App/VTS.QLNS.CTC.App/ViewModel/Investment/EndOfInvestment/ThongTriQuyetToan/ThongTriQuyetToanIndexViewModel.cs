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
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.ThongTriQuyetToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.ThongTriQuyetToan
{
    public class ThongTriQuyetToanIndexViewModel : GridViewModelBase<VdtThongTriModel>
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT_END_OF_INVESTMENT_THONG_TRI_QUYET_TOAN_INDEX;
        public override string GroupName => MenuItemContants.GROUP_ANNUAL_SETTLEMENT;
        //public override string Name => "Quản lý thông tri quyết toán";
        public override string Name => "Thông tri quyết toán";
        public override string Description => "Danh sách thông tin thông tri quyết toán";
        public bool IsEdit => SelectedItem != null && SelectedItem.Id != Guid.Empty;
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.ThongTriQuyetToan.ThongTriQuyetToanIndex);

        #region Private
        private readonly ISessionService _sessionService;
        private readonly IVdtThongTriService _thongTriService;
        private readonly INsDonViService _nsDonViService;
        private ICollectionView _thongTriView;
        private IMapper _mapper;
        private readonly ILog _logger;
        private readonly IExportService _exportService;
        #endregion

        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand ExportCommand { get; }
        #endregion

        #region Componer
        private string _iNamThongTri;
        public string iNamThongTri
        {
            get => _iNamThongTri;
            set
            {
                SetProperty(ref _iNamThongTri, value);
                OnSearch();
            }
        }

        private DateTime? _dNgayThongTriFrom;
        public DateTime? DNgayThongTriFrom
        {
            get => _dNgayThongTriFrom;
            set
            {
                SetProperty(ref _dNgayThongTriFrom, value);
                OnSearch();
            }
        }

        private string _sNguoiLap;
        public string sNguoiLap
        {
            get => _sNguoiLap;
            set => SetProperty(ref _sNguoiLap, value);
        }

        private string _sTruongPhong;
        public string sTruongPhong
        {
            get => _sNguoiLap;
            set => SetProperty(ref _sNguoiLap, value);
        }

        private string _sThuTruong;
        public string sThuTruong
        {
            get => _sThuTruong;
            set => SetProperty(ref _sThuTruong, value);
        }

        private DateTime? _dNgayThongTriTo;
        public DateTime? DNgayThongTriTo
        {
            get => _dNgayThongTriTo;
            set
            {
                SetProperty(ref _dNgayThongTriTo, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _drpDonViQuanLy;
        public ObservableCollection<ComboboxItem> DrpDonViQuanLy
        {
            get => _drpDonViQuanLy;
            set => SetProperty(ref _drpDonViQuanLy, value);
        }

        private ComboboxItem _drpDonViQuanLySelected;
        public ComboboxItem DrpDonViQuanLySelected
        {
            get => _drpDonViQuanLySelected;
            set
            {
                SetProperty(ref _drpDonViQuanLySelected, value);
                OnSearch();
            }
        }
        #endregion

        public ThongTriQuyetToanDialogViewModel ThongTriQuyetToanDialogViewModel { get; set; }
        public ThongTriQuyetToanDetailViewModel ThongTriQuyetToanDetailViewModel { get; set; }

        public ThongTriQuyetToanIndexViewModel(
            ThongTriQuyetToanDialogViewModel thongTriQuyetToanDialogViewModel,
            ThongTriQuyetToanDetailViewModel thongTriQuyetToanDetailViewModel,
            IVdtThongTriService thongTriService,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IExportService exportService,
            IMapper mapper,
            ILog logger)
        {
            ThongTriQuyetToanDialogViewModel = thongTriQuyetToanDialogViewModel;
            ThongTriQuyetToanDialogViewModel.ParentPage = this;
            ThongTriQuyetToanDetailViewModel = thongTriQuyetToanDetailViewModel;
            ThongTriQuyetToanDetailViewModel.ParentPage = this;
            _thongTriService = thongTriService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _mapper = mapper;
            _logger = logger;
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => onResetFilter());
            ExportCommand = new RelayCommand(obj => OnExportExcel(ExportType.EXCEL));
        }

        #region RelayCommand Event
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            GetDonViQuanLy();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            Guid iIdLoaiThongTri = _thongTriService.GetAllDmLoaiThongTri().FirstOrDefault(n => n.IKieuLoaiThongTri == (int)LoaiThongTri.THONG_TRI_QUYET_TOAN).Id;
            List<VdtThongTriQuery> listChungTu = _thongTriService.GetVdtThongTriIndex(iIdLoaiThongTri, OPEN_FROM_PHEDUYETTHANHTOAN.THONGTRIQUYETTOAN).ToList();
            var lstItem = _mapper.Map<List<VdtThongTriModel>>(listChungTu);
            lstItem = lstItem.Select(n => { n.iRowIndex = lstItem.IndexOf(n) + 1; return n; }).ToList();
            Items = _mapper.Map<ObservableCollection<VdtThongTriModel>>(lstItem);
            _thongTriView = CollectionViewSource.GetDefaultView(Items);
            _thongTriView.Filter = VdtTtDeNghiThanhToanFilter;
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSearch()
        {
            _thongTriView.Refresh();
        }

        protected override void OnAdd()
        {
            ThongTriQuyetToanDialogViewModel.Model = new VdtThongTriModel();
            ThongTriQuyetToanDialogViewModel.Init();
            ThongTriQuyetToanDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenThongTriQuyetToanDetail(_mapper.Map<VdtThongTriModel>(obj));
            };
            var view = new ThongTriQuyetToanDialog
            {
                DataContext = ThongTriQuyetToanDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnUpdate()
        {
            ThongTriQuyetToanDialogViewModel.Model = SelectedItem;
            ThongTriQuyetToanDialogViewModel.Init();
            ThongTriQuyetToanDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenThongTriQuyetToanDetail(_mapper.Map<VdtThongTriModel>(obj));
            };
            var view = new ThongTriQuyetToanDialog
            {
                DataContext = ThongTriQuyetToanDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.MsgConfirmDeleteThongTriQuyetToan);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void onResetFilter()
        {
            iNamThongTri = null;
            DNgayThongTriFrom = null;
            DNgayThongTriTo = null;
            DrpDonViQuanLySelected = null;
            sThuTruong = string.Empty;
            sTruongPhong = string.Empty;
            sNguoiLap = string.Empty;
            OnPropertyChanged(nameof(iNamThongTri));
            OnPropertyChanged(nameof(DNgayThongTriFrom));
            OnPropertyChanged(nameof(DNgayThongTriTo));
            OnPropertyChanged(nameof(DrpDonViQuanLySelected));
            OnPropertyChanged(nameof(sThuTruong));
            OnPropertyChanged(nameof(sTruongPhong));
            OnPropertyChanged(nameof(sNguoiLap));
            OnSearch();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            var data = (VdtThongTriModel)obj;
            OnOpenThongTriQuyetToanDetail(data);
            LoadData();
        }

        private void OnExportExcel(ExportType exportType)
        {
            try
            {
                if (!Items.Any(n => n.BIsChecked))
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    MessageBox.Show(Resources.VoucherExportEmpty);
                    return;
                }

                IsLoading = true;
                foreach (var item in Items.Where(n => n.BIsChecked))
                {
                    List<VdtThongTriQuyetToanModel> lstData = _mapper.Map<List<VdtThongTriQuyetToanModel>>(_thongTriService.GetVdtThongTriQuyetToanById(item.Id.Value));
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    double fTongTien = lstData.Sum(n => n.FSoTien);
                    FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("FTongTien", fTongTien);
                    data.Add("STenDonVi", item.sTenDonVi);
                    data.Add("INamKeHoach", item.iNamThongTri);
                    data.Add("Items", lstData);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QT, ExportFileName.RPT_VDT_THONGTRI_QUYETTOAN);
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(TypeChuKy.RPT_VDT_THONGTRI_QUYETTOAN);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<VdtThongTriQuyetToanModel>(templateFileName, data);
                    var result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                    _exportService.Open(result, exportType);
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Helper
        private void GetDonViQuanLy()
        {
            var cbxLoaiDonViData = _nsDonViService.GetDanhSachDonViByNguoiDung(
                _sessionService.Current.Principal, _sessionService.Current.YearOfWork)
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = n.TenDonVi });
            _drpDonViQuanLy = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
        }

        private bool VdtTtDeNghiThanhToanFilter(object obj)
        {
            if (!(obj is VdtThongTriModel temp)) return true;
            var bCondition = true;
            int iNamKeHoachParse = 0;
            if (!string.IsNullOrEmpty(iNamThongTri) && int.TryParse(iNamThongTri, out iNamKeHoachParse))
            {
                bCondition &= (temp.iNamThongTri == iNamKeHoachParse);
            }
            if (DNgayThongTriFrom.HasValue)
            {
                bCondition &= (temp.dNgayThongTri.HasValue && temp.dNgayThongTri >= DNgayThongTriFrom);
            }
            if (DNgayThongTriTo.HasValue)
            {
                bCondition &= (temp.dNgayThongTri.HasValue && temp.dNgayThongTri <= DNgayThongTriTo);
            }
            if (DrpDonViQuanLySelected != null)
            {
                bCondition &= (temp.iID_MaDonViID == DrpDonViQuanLySelected.ValueItem);
            }
            if (!string.IsNullOrEmpty(sNguoiLap))
            {
                bCondition &= (temp.sNguoiLap.ToUpper().Contains(sNguoiLap.ToUpper()));
            }
            if (!string.IsNullOrEmpty(sThuTruong))
            {
                bCondition &= (temp.sThuTruongDonVi.ToUpper().Contains(sThuTruong.ToUpper()));
            }
            if (!string.IsNullOrEmpty(sTruongPhong))
            {
                bCondition &= (temp.sTruongPhong.ToUpper().Contains(sTruongPhong.ToUpper()));
            }
            return bCondition;
        }

        private void OnOpenThongTriQuyetToanDetail(VdtThongTriModel SelectedItem)
        {
            ThongTriQuyetToanDetailViewModel.Model = SelectedItem;
            ThongTriQuyetToanDetailViewModel.Init();
            var view = new ThongTriQuyetToanDetail { DataContext = ThongTriQuyetToanDetailViewModel };
            //view.Owner = System.Windows.Application.Current.MainWindow;
            view.ShowDialog();
            LoadData();
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _thongTriService.DeleteThongTriThanhToan(_mapper.Map<VdtThongTri>(SelectedItem));
            LoadData();
        }
        #endregion
    }
}
