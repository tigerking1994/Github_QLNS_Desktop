using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ApprovalOfAdvanceCaptital
{
    public class ApprovalOfAdvanceCaptitalDialogViewModel : DialogViewModelBase<VdtTtDeNghiThanhToanUngModel>
    {
        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtTtDeNghiThanhToanUngService _deNghiThanhToanUngService;
        private readonly IVdtTtDeNghiThanhToanUngChiTietService _deNghiThanhToanUngChiTietService;
        private readonly IVdtDaTtHopDongService _ttHopDongService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _duAnView;
        #endregion

        public override string Name => "Quản lý cấp phát cấp ứng ngoài chỉ tiêu";
        public override string Description => "Cập nhật thông tin vốn ứng";
        public bool IsInsert => Model.Id == Guid.Empty;
        public string sNguonVon { get; set; }

        private ObservableCollection<DuAnByDenghiThanhToanUngModel> _lstDuAn;
        public ObservableCollection<DuAnByDenghiThanhToanUngModel> LstDuAn
        {
            get => _lstDuAn;
            set => SetProperty(ref _lstDuAn, value);
        }

        private bool _selectAllDuAn;
        public bool SelectAllDuAn
        {
            get => (LstDuAn == null || !LstDuAn.Any()) ? false : LstDuAn.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDuAn, value);
                if (LstDuAn != null)
                {
                    LstDuAn.Select(c => { c.IsChecked = _selectAllDuAn; return c; }).ToList();
                }
            }
        }

        private string _searchDuAn;
        public string SearchDuAn
        {
            get => _searchDuAn;
            set
            {
                SetProperty(ref _searchDuAn, value);
                _duAnView.Refresh();
            }
        }

        private string _sCountDuAn;
        public string sCountDuAn
        {
            get => LstDuAn != null ? string.Format("{0}/{1}", LstDuAn.Count(n => n.IsChecked), LstDuAn.Count) : "0/0";
            set => SetProperty(ref _sCountDuAn, value);
        }

        #region Componer
        private DateTime? _dNgayDeNghi;
        public DateTime? DNgayDeNghi
        {
            get => _dNgayDeNghi;
            set
            {
                if (SetProperty(ref _dNgayDeNghi, value))
                {
                    LoadDuAn();
                }
            }
        }

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set
            {
                if (SetProperty(ref _cbxLoaiDonViSelected, value))
                {
                    LoadDuAn(); ;
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }
        #endregion

        public RelayCommand SaveDataCommand { get; }

        public ApprovalOfAdvanceCaptitalDialogViewModel(
            IVdtTtDeNghiThanhToanUngService deNghiThanhToanUngService,
            IVdtTtDeNghiThanhToanUngChiTietService deNghiThanhToanUngChiTietService,
            INsDonViService nsDonViService,
            IVdtDaTtHopDongService ttHopDongService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _deNghiThanhToanUngService = deNghiThanhToanUngService;
            _deNghiThanhToanUngChiTietService = deNghiThanhToanUngChiTietService;
            _nsDonViService = nsDonViService;
            _ttHopDongService = ttHopDongService;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        #region RelayCommand Event
        public override void Init()
        {
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            if (Model != null && Model.Id != Guid.Empty)
            {
                DNgayDeNghi = Model.dNgayDeNghi;
                LoadComboBoxLoaiDonVi(Model.iID_MaDonViQuanLy);
                if (QlnsData.nguonNganSach.ContainsKey(Model.iId_NguonVonId.ToString()))
                {
                    sNguonVon = QlnsData.nguonNganSach[Model.iId_NguonVonId.ToString()];
                }
            }
            else
            {
                sNguonVon = _sessionService.Current.BudgetStr;
                Model.iId_NguonVonId = _sessionService.Current.Budget;
                LoadComboBoxLoaiDonVi();
            }
            LoadDuAn();
        }

        public override void OnSave()
        {
            StringBuilder messageBuilder = new StringBuilder();
            if (Model == null) Model = new VdtTtDeNghiThanhToanUngModel();
            if (CbxLoaiDonViSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị quản lý");
            }
            if (string.IsNullOrEmpty(Model.sSoDeNghi))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Số phê duyệt");
            }
            if (!DNgayDeNghi.HasValue)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Ngày phê duyệt");
            }
            if (LstDuAn == null || !LstDuAn.Any(n => n.IsChecked))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Dự án");
            }
            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                LoadData();
                return;
            }

            var dataInsert = _mapper.Map<VdtTtDeNghiThanhToanUng>(Model);
            dataInsert.IIDMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
            dataInsert.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            dataInsert.DNgayDeNghi = DNgayDeNghi;
            if (dataInsert.Id == Guid.Empty)
            {
                _deNghiThanhToanUngService.Insert(dataInsert, _sessionService.Current.Principal);
            }
            else
            {
                _deNghiThanhToanUngService.Update(dataInsert, _sessionService.Current.Principal);
                dataInsert.IsModified = true;
            }
            dataInsert.lstDuAnId = LstDuAn.Where(n => n.IsChecked).Select(n => n.iID_DuAnID.Value).ToList();
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(dataInsert);
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            DuAnByDenghiThanhToanUngModel item = (DuAnByDenghiThanhToanUngModel)sender;
            switch (args.PropertyName)
            {
                case nameof(DuAnDenghiThanhToanModel.IsChecked):
                    sCountDuAn = string.Format("{0}/{1}", LstDuAn.Count(n => n.IsChecked), LstDuAn.Count);
                    if (LstDuAn.Count(n => n.IsChecked) == LstDuAn.Count)
                    {
                        SelectAllDuAn = true;
                    }
                    else if (LstDuAn.Count(n => !n.IsChecked) == LstDuAn.Count)
                    {
                        SelectAllDuAn = false;
                    }
                    break;
            }
            OnPropertyChanged(nameof(sCountDuAn));
            OnPropertyChanged(nameof(SelectAllDuAn));
        }
        #endregion

        #region Helper
        private void LoadDuAn()
        {
            if (CbxLoaiDonViSelected == null || !DNgayDeNghi.HasValue) return;
            var data = _deNghiThanhToanUngChiTietService.GetDuAnByDeNghiThanhToanUng(CbxLoaiDonViSelected.ValueItem, DNgayDeNghi.Value);
            if (data != null && data.Count() != 0)
            {
                LstDuAn = _mapper.Map<ObservableCollection<DuAnByDenghiThanhToanUngModel>>(data);
                foreach (var item in LstDuAn)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }
                _duAnView = CollectionViewSource.GetDefaultView(LstDuAn);
                _duAnView.Filter = DuAnFilter;
            }
        }

        private void LoadComboBoxLoaiDonVi(string iIdDonVi = null)
        {
            var cbxLoaiDonViData = _nsDonViService.GetDanhSachDonViByNguoiDung(_sessionService.Current.Principal, _sessionService.Current.YearOfWork)
                .Where(n => (string.IsNullOrEmpty(iIdDonVi) || n.IIDMaDonVi == iIdDonVi))
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi.ToString(), DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi), HiddenValue = n.Id.ToString() });
            _cbxLoaiDonVi = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
            if (!string.IsNullOrEmpty(iIdDonVi))
            {
                CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault(n => n.ValueItem.ToUpper() == iIdDonVi.ToUpper());
            }
            else
            {
                CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault();
            }
            OnPropertyChanged(nameof(CbxLoaiDonVi));
        }

        private bool DuAnFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchDuAn))
            {
                return true;
            }
            return obj is DuAnDenghiThanhToanModel item && item.sTenDuAn.ToLower().Contains(_searchDuAn, StringComparison.OrdinalIgnoreCase);
        }
        #endregion
    }
}