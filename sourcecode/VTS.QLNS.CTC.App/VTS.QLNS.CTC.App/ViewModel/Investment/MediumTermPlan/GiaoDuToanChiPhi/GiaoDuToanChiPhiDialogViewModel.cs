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
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GiaoDuToanChiPhi
{
    public class GiaoDuToanChiPhiDialogViewModel : DialogViewModelBase<VdtKhvPhanBoVonChiPhiModel>
    {
        #region Private
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtKhvKeHoachVonUngDxService _keHoachVonUngService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly INsMucLucNganSachService _mlNganSachService;
        private readonly IVdtKhvPhanBoVonChiPhiService _keHoachVonChiPhiService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _duAnView;
        private string _sMaDonViCap0;
        #endregion

        public override string Name => "Quản lý chứng từ giao dự toán chi phí";
        public override string Description => string.Format("{0} thông tin chứng từ giao dự toán chi phí", IsInsert ? "Thêm mới" : "Cập nhật");

        #region Componer
        private ObservableCollection<VdtKhvKeHoachVonUngDxModel> _itemsTongHop;
        public ObservableCollection<VdtKhvKeHoachVonUngDxModel> ItemsTongHop
        {
            get => _itemsTongHop;
            set => SetProperty(ref _itemsTongHop, value);
        }

        private bool _isInsert;
        public bool IsInsert
        {
            get => _isInsert;
            set => SetProperty(ref _isInsert, value);
        }

        public bool IsDieuChinh { get; set; }

        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private int? _iNamKeHoach;
        public int? INamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set
            {
                if (SetProperty(ref _dNgayQuyetDinh, value))
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

        private ObservableCollection<DuAnDenghiThanhToanModel> _lstDuAn;
        public ObservableCollection<DuAnDenghiThanhToanModel> LstDuAn
        {
            get => _lstDuAn;
            set => SetProperty(ref _lstDuAn, value);
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
            set
            {
                SetProperty(ref _cbxNguonVonSelected, value);
            }
        }
        #endregion

        public GiaoDuToanChiPhiDialogViewModel(INsDonViService nsDonViService,
            ISessionService sessionService,
            IVdtKhvKeHoachVonUngDxService keHoachVonUngService,
            INsNguonNganSachService nguonVonService,
            INsMucLucNganSachService mlNganSachService,
            IVdtKhvPhanBoVonChiPhiService keHoachVonChiPhiService,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _keHoachVonUngService = keHoachVonUngService;
            _nguonVonService = nguonVonService;
            _mlNganSachService = mlNganSachService;
            _keHoachVonChiPhiService = keHoachVonChiPhiService;
            _mapper = mapper;
        }

        #region RelayCommand Event
        public override void Init()
        {
            LoadComboBoxNguonVon();
            LoadComboBoxLoaiDonVi();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            if (IsDieuChinh)
            {
                Name = "Điều chỉnh";
                Description = "Điều chỉnh thông tin chứng từ giao dự toán chi phí";
            }
            SetValueDefault();
            LoadDuAn();
        }

        public override void OnSave()
        {
            if (Model == null) Model = new VdtKhvPhanBoVonChiPhiModel();
            if (!Validate()) return;
            var dataInsert = _mapper.Map<VdtKhvPhanBoVonChiPhiModel>(Model);
            
            dataInsert.IIdDonViId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            dataInsert.IIdMaDonVi = CbxLoaiDonViSelected.ValueItem;

            dataInsert.DNgayQuyetDinh = DNgayQuyetDinh;
            dataInsert.SSoQuyetDinh = SSoQuyetDinh;
            dataInsert.INamKeHoach = INamKeHoach ?? 0;
            dataInsert.IIdLoaiNguonVonId = int.Parse(CbxNguonVonSelected.ValueItem);
            dataInsert.IIdDuAnId = LstDuAn.FirstOrDefault(n => n.IsChecked).iID_DuAnID;
            dataInsert.BActive = true;

            if (dataInsert.Id == Guid.Empty && !IsDieuChinh)
            {
                dataInsert.Id = Guid.NewGuid();
                dataInsert.BIsGoc = true;
                dataInsert.sUserCreate = _sessionService.Current.Principal;
                dataInsert.dDateCreate = DateTime.Now;  
                _keHoachVonChiPhiService.Add(_mapper.Map<VdtKhvPhanBoVonChiPhi>(dataInsert));
            } else if (IsDieuChinh)
            {
                dataInsert.IIdParentId = Model.Id;
                dataInsert.Id = Guid.NewGuid();
                dataInsert.BActive = true;
                dataInsert.BIsGoc = false;
                dataInsert.dDateCreate = DateTime.Now;
                dataInsert.sUserCreate = _sessionService.Current.Principal;
                _keHoachVonChiPhiService.Adjust(_mapper.Map<VdtKhvPhanBoVonChiPhi>(dataInsert));
            } else
            {
                dataInsert.sUserUpdate = _sessionService.Current.Principal;
                dataInsert.dDateUpdate = DateTime.Now;
                _keHoachVonChiPhiService.Update(_mapper.Map<VdtKhvPhanBoVonChiPhi>(dataInsert));
                dataInsert.IsModified = true;
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
            Model = _mapper.Map<VdtKhvPhanBoVonChiPhiModel>(dataInsert);
            SavedAction?.Invoke(Model);
        }
        #endregion

        #region Helper
        private bool Validate()
        {
            List<string> messageBuilder = new List<string>();
            if (CbxLoaiDonViSelected == null)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Đơn vị quản lý"));
            }
            if (string.IsNullOrEmpty(SSoQuyetDinh))
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Số kế hoạch"));
            }
            if (!DNgayQuyetDinh.HasValue)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Ngày lập"));
            }
            if (!INamKeHoach.HasValue)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Năm kế hoạch"));
            }
            if (CbxNguonVonSelected == null)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Nguồn vốn"));
            }

            if (!string.IsNullOrEmpty(SSoQuyetDinh) && _keHoachVonChiPhiService.IsExistSoQuyetDinh(SSoQuyetDinh, Model.Id))
            {
                messageBuilder.Add(Resources.MsgTrungSoQuyetDinhs);
            }

            if (messageBuilder.Count != 0)
            {
                MessageBox.Show(string.Join("\n", messageBuilder));
                return false; ;
            }
            return true;
        }

        private void LoadDuAn()
        {
            if (CbxLoaiDonViSelected == null || !DNgayQuyetDinh.HasValue) return;
            List<VdtKhvKeHoachVonUngDxChiTietQuery> lstDataUpdate = new List<VdtKhvKeHoachVonUngDxChiTietQuery>();
            if (Model.Id != Guid.Empty)
            {
                lstDataUpdate = _keHoachVonUngService.GetKeHoachVonUngChiTietByParentId(Model.Id).ToList();
            }
            var data = _keHoachVonUngService.GetDuAnInKeHoachVonUngDetail(CbxLoaiDonViSelected.ValueItem,
                DNgayQuyetDinh.Value, null);
            LstDuAn = _mapper.Map<ObservableCollection<DuAnDenghiThanhToanModel>>(data);
            if (!Model.IIdDuAnId.IsNullOrEmpty()) LstDuAn.FirstOrDefault(n => n.iID_DuAnID == Model.IIdDuAnId).IsChecked = true;
            foreach (var item in LstDuAn)
            {
                if (lstDataUpdate.Any(n => n.iID_DuAnID == item.iID_DuAnID))
                {
                    item.IsChecked = true;
                }
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
            _duAnView = CollectionViewSource.GetDefaultView(LstDuAn);
            _duAnView.Filter = DuAnFilter;
        }

        private bool DuAnFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchDuAn))
            {
                return true;
            }
            return obj is DuAnDenghiThanhToanModel item &&
                (item.sTenDuAn.ToLower().Contains(_searchDuAn, StringComparison.OrdinalIgnoreCase)
                || item.sMaDuAn.ToLower().Contains(_searchDuAn, StringComparison.OrdinalIgnoreCase));
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            DuAnDenghiThanhToanModel item = (DuAnDenghiThanhToanModel)sender;
            switch (args.PropertyName)
            {
                case nameof(DuAnDenghiThanhToanModel.IsChecked):
                    if (LstDuAn.Where(n => n.IsChecked).Count() >= 2)
                    {
                        MessageBox.Show("Chỉ được chọn 1 dự án");
                        item.IsChecked = false;
                    }
                    break;
            }
        }

        private void SetValueDefault()
        {
            if (!IsInsert && !IsDieuChinh)
            {
                if (!IsInsert)
                {
                    SSoQuyetDinh = Model.SSoQuyetDinh;
                    DNgayQuyetDinh = Model.DNgayQuyetDinh;
                    CbxLoaiDonViSelected = CbxLoaiDonVi.FirstOrDefault(n => n.ValueItem.ToUpper() == Model.IIdMaDonVi.ToUpper());
                }
                else
                {
                    SSoQuyetDinh = string.Empty;
                    DNgayQuyetDinh = DateTime.Now;
                    if (!string.IsNullOrEmpty(_sMaDonViCap0))
                        CbxLoaiDonViSelected = CbxLoaiDonVi.FirstOrDefault(n => n.ValueItem == _sMaDonViCap0);
                    else
                        CbxLoaiDonViSelected = null;
                }
                INamKeHoach = Model.INamKeHoach;
                if (Model.IIdLoaiNguonVonId.HasValue)
                    CbxNguonVonSelected = CbxNguonVon.FirstOrDefault(n => n.ValueItem == Model.IIdLoaiNguonVonId.Value.ToString());
            }
            else
            {
                SSoQuyetDinh = null;
                DNgayQuyetDinh = DateTime.Now;
                INamKeHoach = DateTime.Now.Year;
                Model.IIdDuAnId = null;
                CbxLoaiDonViSelected = null;
                CbxNguonVonSelected = null;
            }
            LstDuAn = new ObservableCollection<DuAnDenghiThanhToanModel>();
            OnPropertyChanged(nameof(LstDuAn));
            OnPropertyChanged(nameof(DNgayQuyetDinh));
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(CbxNguonVonSelected));
        }

        private void LoadComboBoxLoaiDonVi()
        {
            var lstDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            _sMaDonViCap0 = lstDonVi.FirstOrDefault(n => n.Loai == "0").IIDMaDonVi;
            var cbxLoaiDonViData = lstDonVi
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi), HiddenValue = n.Id.ToString() });
            _cbxLoaiDonVi = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
            OnPropertyChanged(nameof(CbxLoaiDonVi));
        }

        private void LoadComboBoxNguonVon()
        {
            var data = _nguonVonService.FindNguonNganSach().OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
            CbxNguonVon = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(CbxNguonVon));
        }
        #endregion
    }
}
