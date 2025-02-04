using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThanhToanKhoBac
{
    public class ThanhToanKhoBacDialogViewModel : DialogViewModelBase<ThanhToanQuaKhoBacModel>
    {
        #region Private
        private readonly INsDonViService _nsDonViService;
        private readonly IMucLucNganSachService _mlNganSachService;
        private readonly IVdtTtThanhToanQuaKhoBacService _thanhToanService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _duAnView;
        #endregion

        public override string Name => "Thanh toán qua kho bạc";
        public bool IsInsert => Model.Id == Guid.Empty;
        public override string Description => string.Format("{0} thông tin thanh toán qua kho bạc", IsInsert ? "Thêm mới" : "Cập nhật");

        #region GridView du an
        private ObservableCollection<DuAnDenghiThanhToanModel> _lstDuAn;
        public ObservableCollection<DuAnDenghiThanhToanModel> LstDuAn
        {
            get => _lstDuAn;
            set
            {
                if (SetProperty(ref _lstDuAn, value))
                {
                    OnPropertyChanged(nameof(sCountDuAn));
                }
            }
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
        #endregion

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

        private string _iNamKeHoach;
        public string INamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                if (SetProperty(ref _iNamKeHoach, value))
                {
                    LoadLoaiNganSach();
                    LoadDuAn();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
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

        private ObservableCollection<ComboboxItem> _cbxDonViThanhToan;
        public ObservableCollection<ComboboxItem> CbxDonViThanhToan
        {
            get => _cbxDonViThanhToan;
            set => SetProperty(ref _cbxDonViThanhToan, value);
        }

        private ComboboxItem _cbxDonViThanhToanSelected;
        public ComboboxItem CbxDonViThanhToanSelected
        {
            get => _cbxDonViThanhToanSelected;
            set => SetProperty(ref _cbxDonViThanhToanSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiNganSach;
        public ObservableCollection<ComboboxItem> CbxLoaiNganSach
        {
            get => _cbxLoaiNganSach;
            set => SetProperty(ref _cbxLoaiNganSach, value);
        }

        private ComboboxItem _cbxLoaiNganSachSelected;
        public ComboboxItem CbxLoaiNganSachSelected
        {
            get => _cbxLoaiNganSachSelected;
            set
            {
                if (SetProperty(ref _cbxLoaiNganSachSelected, value))
                {
                    LoadDuAn();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxNguonVon;
        public ObservableCollection<ComboboxItem> CbxNguonVon
        {
            get => _cbxNguonVon;
            set => SetProperty(ref _cbxNguonVon, value);
        }

        private ComboboxItem _cbxNguonVonSelected;
        public ComboboxItem CbxNguonVonSelected
        {
            get => _cbxNguonVonSelected;
            set => SetProperty(ref _cbxNguonVonSelected, value);
        }
        #endregion

        public ThanhToanKhoBacDialogViewModel(
            INsDonViService nsDonViService,
            IMucLucNganSachService mlNganSachService,
            IVdtTtThanhToanQuaKhoBacService thanhToanService,
            ISessionService sessionService,
            INsNguonNganSachService nguonVonService,
            IMapper mapper)
        {
            _nsDonViService = nsDonViService;
            _mlNganSachService = mlNganSachService;
            _thanhToanService = thanhToanService;
            _sessionService = sessionService;
            _nguonVonService = nguonVonService;
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
                DNgayDeNghi = Model.dNgayThanhToan;
                _iNamKeHoach = Model.iNamKeHoach.ToString();
                LoadComboBoxLoaiDonVi(Model.iId_MaDonViQuanLyID, Model.iId_MaDonViNhanThanhToanID);
                LoadLoaiNganSach(Model.iID_LoaiNguonVonID);
                GetNguonNganSach(Model.iID_NguonVonID);
            }
            else
            {
                LoadComboBoxLoaiDonVi();
                LoadLoaiNganSach();
                GetNguonNganSach();
            }
            LoadDuAn();
        }

        public override void OnSave()
        {
            List<string> lstMessage = new List<string>();
            if (Model == null) Model = new ThanhToanQuaKhoBacModel();
            if (CbxLoaiDonViSelected == null)
            {
                lstMessage.Add(string.Format(Resources.MsgErrorRequire, "Đơn vị quản lý"));
            }
            else
            {
                Model.iId_MaDonViQuanLyID = CbxLoaiDonViSelected.ValueItem;
                Model.iID_DonViQuanLyID = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            }
            if (string.IsNullOrEmpty(Model.sSoThanhToan))
            {
                lstMessage.Add(string.Format(Resources.MsgErrorRequire, "Số chứng từ"));
            }
            if (!DNgayDeNghi.HasValue)
            {
                lstMessage.Add(string.Format(Resources.MsgErrorRequire, "Ngày lập"));
            }
            if (string.IsNullOrEmpty(INamKeHoach))
            {
                lstMessage.Add(string.Format(Resources.MsgErrorRequire, "Năm kế hoạch"));
            }
            if (CbxNguonVonSelected == null)
            {
                lstMessage.Add(string.Format(Resources.MsgErrorRequire, "Nguồn vốn"));
            }
            if (CbxLoaiNganSachSelected == null)
            {
                lstMessage.Add(string.Format(Resources.MsgErrorRequire, "Loại nguồn vốn"));
            }
            if (LstDuAn == null || !LstDuAn.Any(n => n.IsChecked))
            {
                lstMessage.Add(string.Format(Resources.MsgErrorRequire, "Dự án"));
            }
            if (lstMessage.Count != 0)
            {
                MessageBox.Show(String.Join("\n", lstMessage));
                LoadData();
                return;
            }

            var dataInsert = _mapper.Map<VdtTtThanhToanQuaKhoBac>(Model);
            dataInsert.IIdMaDonViQuanLyID = CbxLoaiDonViSelected.ValueItem;
            dataInsert.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            dataInsert.IIdNguonVonId = int.Parse(CbxNguonVonSelected.ValueItem);
            dataInsert.IIdLoaiNguonVonId = Guid.Parse(CbxLoaiNganSachSelected.ValueItem);
            if (CbxDonViThanhToanSelected != null)
            {
                dataInsert.IIdDonViNhanThanhToanId = Guid.Parse(CbxDonViThanhToanSelected.HiddenValue);
                dataInsert.IIdMaDonViNhanThanhToanID = CbxDonViThanhToanSelected.ValueItem;
            }
            dataInsert.INamKeHoach = int.Parse(INamKeHoach);
            dataInsert.DNgayThanhToan = DNgayDeNghi;
            if (dataInsert.Id == Guid.Empty)
            {
                dataInsert.Id = Guid.NewGuid();
                _thanhToanService.Insert(dataInsert, _sessionService.Current.Principal);
            }
            else
            {
                _thanhToanService.Update(dataInsert, _sessionService.Current.Principal);
                dataInsert.IsModified = true;
            }
            dataInsert.lstDuAnId = LstDuAn.Where(n => n.IsChecked).Select(n => n.iID_DuAnID).ToList();
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(dataInsert);
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            DuAnDenghiThanhToanModel item = (DuAnDenghiThanhToanModel)sender;
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
            if (string.IsNullOrEmpty(INamKeHoach) || !DNgayDeNghi.HasValue || CbxLoaiNganSachSelected == null || CbxLoaiDonViSelected == null) return;
            var data = _thanhToanService.GetDuAnByThanhToanKhoBac(int.Parse(INamKeHoach), DNgayDeNghi.Value, CbxLoaiNganSachSelected.HiddenValue, CbxLoaiDonViSelected.ValueItem);
            LstDuAn = _mapper.Map<ObservableCollection<DuAnDenghiThanhToanModel>>(data);
            foreach (var item in LstDuAn)
            {
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
            _duAnView = CollectionViewSource.GetDefaultView(LstDuAn);
            _duAnView.Filter = DuAnFilter;
            OnPropertyChanged(nameof(LstDuAn));
        }

        private void LoadComboBoxLoaiDonVi(string iIdDonVi = null, string iIdDonViThanhToan = null)
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = n.TenDonVi, HiddenValue = n.Id.ToString() });
            _cbxLoaiDonVi = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
            _cbxDonViThanhToan = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
            if (!string.IsNullOrEmpty(iIdDonVi))
            {
                CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault(n => n.ValueItem.ToUpper() == iIdDonVi.ToUpper());
            }
            else
            {
                CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault();
            }
            if (!string.IsNullOrEmpty(iIdDonViThanhToan))
            {
                CbxDonViThanhToanSelected = _cbxDonViThanhToan.FirstOrDefault(n => n.ValueItem.ToUpper() == iIdDonVi.ToUpper());
            }
            else
            {
                CbxDonViThanhToanSelected = _cbxDonViThanhToan.FirstOrDefault();
            }
            OnPropertyChanged(nameof(CbxLoaiDonVi));
            OnPropertyChanged(nameof(CbxDonViThanhToan));
        }

        private void GetNguonNganSach(int? iIDNguonVon = null)
        {
            var data = _nguonVonService.FindNguonNganSach().Where(n => n.IIdMaNguonNganSach != 1)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.Value.ToString(), DisplayItem = n.STen });
            _cbxNguonVon = new ObservableCollection<ComboboxItem>(data);
            if (iIDNguonVon.HasValue)
            {
                CbxNguonVonSelected = _cbxNguonVon.FirstOrDefault(n => n.ValueItem == iIDNguonVon.Value.ToString());
            }
            else
            {
                CbxNguonVonSelected = _cbxNguonVon.FirstOrDefault();
            }
            OnPropertyChanged(nameof(CbxNguonVon));
        }

        public void LoadLoaiNganSach(Guid? iIdLoaiNganSach = null)
        {
            if (string.IsNullOrEmpty(INamKeHoach))
            {
                _cbxLoaiNganSach = new ObservableCollection<ComboboxItem>(new List<ComboboxItem>());
                return;
            }
            var data = _mlNganSachService.GetLoaiNganSachByNamLamViec(int.Parse(INamKeHoach)).Select(n => new ComboboxItem() { ValueItem = n.Id.ToString(), HiddenValue = n.Lns, DisplayItem = n.MoTa });
            _cbxLoaiNganSach = new ObservableCollection<ComboboxItem>(data);
            if (iIdLoaiNganSach.HasValue)
            {
                CbxLoaiNganSachSelected = _cbxLoaiNganSach.FirstOrDefault(n => n.ValueItem.ToUpper() == iIdLoaiNganSach.Value.ToString().ToUpper());
            }
            else
            {
                CbxLoaiNganSachSelected = _cbxLoaiNganSach.FirstOrDefault();
            }
            OnPropertyChanged(nameof(CbxLoaiNganSach));
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
