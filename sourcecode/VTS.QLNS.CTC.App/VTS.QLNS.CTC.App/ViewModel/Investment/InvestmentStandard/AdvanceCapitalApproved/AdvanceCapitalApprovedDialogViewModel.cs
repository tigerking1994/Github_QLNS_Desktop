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
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.AdvanceCapitalApproved
{
    public class AdvanceCapitalApprovedDialogViewModel : DialogViewModelBase<VdtKhvKeHoachVonUngModel>
    {
        #region Private
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtKhvKeHoachVonUngDxService _khvudxService;
        private readonly IVdtKhvKeHoachVonUngService _keHoachVonUngService;
        private readonly IVdtKhvKeHoachVonUngChiTietService _vonUngChiTietService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly INsMucLucNganSachService _mlNganSachService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _duAnView;

        #endregion
        public override string Name => "Quản lý kế hoạch vốn ứng được duyệt";
        public override string Description => string.Format("{0} thông tin vốn ứng được duyệt", IsInsert ? "Thêm mới" : "Cập nhật");
        public bool IsInsert => Model.Id == Guid.Empty;
        private bool _isInsertDieuChinh;
        public bool IsInsertDieuChinh{
            get => _isInsertDieuChinh;
            set
            {
                SetProperty(ref _isInsertDieuChinh, value);
            }
        }
        private DateTime _dStartDate;

        #region Componer
        private string _iNamKeHoach;
        public string INamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                if (SetProperty(ref _iNamKeHoach, value))
                {
                    LoadComboBoxKHVUDX();
                }
            }
        }

        private DateTime? _dNgayDeNghi;
        public DateTime? DNgayDeNghi
        {
            get => _dNgayDeNghi;
            set
            {
                if (SetProperty(ref _dNgayDeNghi, value))
                {
                    LoadComboBoxKHVUDX();
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
                    LoadComboBoxKHVUDX();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }

        private ComboboxItem _cbxKHVUDeXuatSelected;
        public ComboboxItem CbxKHVUDeXuatSelected
        {
            get => _cbxKHVUDeXuatSelected;
            set
            {
                if (SetProperty(ref _cbxKHVUDeXuatSelected, value))
                {
                    LoadDuAn();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxKHVUDeXuat;
        public ObservableCollection<ComboboxItem> CbxKHVUDeXuat
        {
            get => _cbxKHVUDeXuat;
            set => SetProperty(ref _cbxKHVUDeXuat, value);
        }

        private ObservableCollection<DuAnDenghiThanhToanModel> _lstDuAn;
        public ObservableCollection<DuAnDenghiThanhToanModel> LstDuAn
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

        public AdvanceCapitalApprovedDialogViewModel(INsDonViService nsDonViService,
            ISessionService sessionService,
            IVdtKhvKeHoachVonUngDxService khvudxService,
            IVdtKhvKeHoachVonUngService keHoachVonUngService,
            IVdtKhvKeHoachVonUngChiTietService vonUngChiTietService,
            INsNguonNganSachService nguonVonService,
            INsMucLucNganSachService mlNganSachService,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _khvudxService = khvudxService;
            _keHoachVonUngService = keHoachVonUngService;
            _vonUngChiTietService = vonUngChiTietService;
            _nguonVonService = nguonVonService;
            _mlNganSachService = mlNganSachService;
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
            SetValueDefault();
            LoadDuAn();
        }

        public override void OnSave()
        {
            if (!Validate()) return;
            _dStartDate = DateTime.Now;
            string sError = string.Empty;

            var dataInsert = _mapper.Map<VdtKhvKeHoachVonUng>(Model);
            var entity = new VdtKhvKeHoachVonUng();

            dataInsert.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            dataInsert.IIDMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
            dataInsert.DNgayQuyetDinh = DNgayDeNghi;
            dataInsert.INamKeHoach = int.Parse(INamKeHoach);
            dataInsert.IIDKeHoachUngDeXuatID = Guid.Parse(CbxKHVUDeXuatSelected.ValueItem);
            dataInsert.IIdNguonVonId = int.Parse(CbxNguonVonSelected.ValueItem);
            if (dataInsert.Id == Guid.Empty)
            {
                dataInsert.BActive = true;
                dataInsert.BIsGoc = true;
                _keHoachVonUngService.Insert(dataInsert, _sessionService.Current.Principal);
            }
            else
            {
                if (IsInsertDieuChinh)
                {
                    entity = new VdtKhvKeHoachVonUng();
                    dataInsert.CloneObj(entity);

                    entity.Id = Guid.NewGuid();
                    entity.DDateCreate = DateTime.Now;
                    entity.IIdParentId = dataInsert.Id;
                    entity.SUserCreate = _sessionService.Current.Principal;
                    entity.BActive = true;
                    entity.BIsGoc = false;

                    //List<VdtKhvKeHoachVonUngChiTietQuery> lstDataUpdate = _vonUngChiTietService.GetKeHoachVonUngChiTietByParentId(Model.Id).ToList();

                    var listQuery = new List<VdtKhvKeHoachVonUngChiTietQuery>();

                    listQuery = _vonUngChiTietService.GetKeHoachVonUngChiTietByParentId(Model.Id).ToList();

                    var lstDataNew = _mapper.Map<List<VdtKhvKeHoachVonUngChiTietModel>>(listQuery);

                    List<VdtKhvKeHoachVonUngChiTiet> lstData = new List<VdtKhvKeHoachVonUngChiTiet>();
                    foreach (var item in lstDataNew)
                    {
                        VdtKhvKeHoachVonUngChiTiet itemData = ConvertDataInsert(item);
                        if (itemData == null) continue;
                      
                        lstData.Add(itemData);

                    }

                    _keHoachVonUngService.Adjust(entity, lstData);
                }
                else
                {
                    _keHoachVonUngService.Update(dataInsert, _sessionService.Current.Principal);
                    dataInsert.IsModified = true;
                }
            }
            Model = _mapper.Map<VdtKhvKeHoachVonUngModel>((IsInsertDieuChinh!=IsInsert) ? entity : dataInsert);
            Model.lstDuAnId = LstDuAn.Where(n => n.IsChecked).Select(n => n.iID_DuAnID).ToList();

            DialogHost.CloseDialogCommand.Execute(null, null);
            
            Model.sTenDonViQuanLy = CbxLoaiDonViSelected.DisplayItem;
            SavedAction?.Invoke(Model);
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
        private bool Validate()
        {
            int iNamKeHoach = 0;

            List<string> messageBuilder = new List<string>();
            if (Model == null) Model = new VdtKhvKeHoachVonUngModel();
            if (CbxLoaiDonViSelected == null)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Đơn vị quản lý"));
            }
            if (string.IsNullOrEmpty(Model.sSoQuyetDinh))
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Số kế hoạch"));
            }
            if (!DNgayDeNghi.HasValue)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Ngày lập"));
            }
            if (string.IsNullOrEmpty(INamKeHoach))
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Năm kế hoạch"));
            }
            else if (!int.TryParse(INamKeHoach, out iNamKeHoach))
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorFormat, "Năm kế hoạch"));
            }
            if (CbxNguonVonSelected == null)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Nguồn vốn"));
            }
            if (CbxKHVUDeXuatSelected == null)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Kế hoạch vốn ứng đề xuất"));
            }

            if (_keHoachVonUngService.CheckTrungSoQuyetDinh(Model.sSoQuyetDinh.Trim(), (IsInsertDieuChinh != IsInsert ? Guid.Empty : Model.Id)))
            {
                messageBuilder.Add("Trùng số kế hoạch");
            }

            if (messageBuilder.Count != 0)
            {
                MessageBox.Show(string.Join("\n", messageBuilder));
                return false;
            }
            return true;
        }

        private void LoadDuAn()
        {
            if (CbxLoaiDonViSelected == null || !DNgayDeNghi.HasValue || CbxKHVUDeXuatSelected == null) return;
            var data = _vonUngChiTietService.GetDuAnInKeHoachVonUngDetail(Guid.Parse(CbxKHVUDeXuatSelected.ValueItem));
            LstDuAn = _mapper.Map<ObservableCollection<DuAnDenghiThanhToanModel>>(data);

            List<VdtKhvKeHoachVonUngChiTietQuery> lstDataUpdate = new List<VdtKhvKeHoachVonUngChiTietQuery>();
            if (Model.Id != Guid.Empty)
            {
                lstDataUpdate = _vonUngChiTietService.GetKeHoachVonUngChiTietByParentId(Model.Id).ToList();
            }

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
            return obj is DuAnDenghiThanhToanModel item && item.sTenDuAn.ToLower().Contains(_searchDuAn, StringComparison.OrdinalIgnoreCase);
        }

        private void SetValueDefault()
        {
            if (Model.Id != Guid.Empty)
            {
                DNgayDeNghi = Model.dNgayQuyetDinh;
                INamKeHoach = (Model.iNamKeHoach ?? 0).ToString();
                CbxLoaiDonViSelected = CbxLoaiDonVi.FirstOrDefault(n => n.ValueItem.ToUpper() == Model.iID_MaDonViQuanLy.ToUpper());
                if (Model.iId_NguonVonId.HasValue)
                    CbxNguonVonSelected = CbxNguonVon.FirstOrDefault(n => n.ValueItem == Model.iId_NguonVonId.Value.ToString());
            }
            else
            {
                DNgayDeNghi = DateTime.Now;
                INamKeHoach = DateTime.Now.Year.ToString();
                CbxNguonVonSelected = null;
            }
            LstDuAn = new ObservableCollection<DuAnDenghiThanhToanModel>();
            OnPropertyChanged(nameof(LstDuAn));
            OnPropertyChanged(nameof(DNgayDeNghi));
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(CbxNguonVonSelected));
        }

        private void LoadComboBoxLoaiDonVi()
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => n.Loai == "0")
                .Select(n => new ComboboxItem()
                {
                    ValueItem = n.IIDMaDonVi,
                    DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi),
                    HiddenValue = n.Id.ToString()
                });
            CbxLoaiDonVi = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
            CbxLoaiDonViSelected = CbxLoaiDonVi.FirstOrDefault();
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(CbxLoaiDonVi));
        }

        private void LoadComboBoxNguonVon()
        {
            var data = _nguonVonService.FindNguonNganSach().OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
            CbxNguonVon = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(CbxNguonVon));
        }

        private void LoadComboBoxKHVUDX()
        {
            int iNamKeHoach = 0;
            if (CbxLoaiDonViSelected == null || string.IsNullOrEmpty(INamKeHoach) || !int.TryParse(INamKeHoach, out iNamKeHoach) || !DNgayDeNghi.HasValue) return;
            var data = _khvudxService.GetKHVUDeXuatInKHVUDuocDuyet(CbxLoaiDonViSelected.ValueItem, iNamKeHoach, DNgayDeNghi.Value)
                .Where(n => !string.IsNullOrEmpty(n.STongHop))
                .Select(n => new ComboboxItem()
                {
                    ValueItem = n.Id.ToString(),
                    DisplayItem = n.SSoDeNghi
                });
            CbxKHVUDeXuat = new ObservableCollection<ComboboxItem>(data);
            if (Model.iID_KeHoachUngDeXuatID.HasValue)
            {
                CbxKHVUDeXuatSelected = CbxKHVUDeXuat.FirstOrDefault(n => n.ValueItem == Model.iID_KeHoachUngDeXuatID.Value.ToString());
            }
            OnPropertyChanged(nameof(CbxKHVUDeXuat));
        }

        private VdtKhvKeHoachVonUngChiTiet ConvertDataInsert(VdtKhvKeHoachVonUngChiTietModel data)
        {
            VdtKhvKeHoachVonUngChiTiet dataInsert = new VdtKhvKeHoachVonUngChiTiet();
            if (dataInsert == null) return null;
            dataInsert.Id = data.Id;
            dataInsert.FCapPhatBangLenhChi = data.fCapPhatBangLenhChi;
            dataInsert.FCapPhatTaiKhoBac = data.fCapPhatTaiKhoBac;
            dataInsert.FTonKhoanTaiDonVi = data.fTonKhoanTaiDonVi;
            dataInsert.FTiGia = data.fTiGia;
            dataInsert.FTiGiaDonVi = data.fTiGiaDonVi;
            dataInsert.IIdDonViTienTeId = data.iID_DonViTienTeID;
            dataInsert.IIdDuAnId = data.iID_DuAnID;
            dataInsert.IIdKeHoachUngId = Model.Id;
            dataInsert.IIdMucId = data.iID_MucID;
            dataInsert.IIdTienTeId = data.iID_TienTeID;
            dataInsert.IIdNganhId = data.iID_NganhID;
            dataInsert.IIdTietMucId = data.iID_TietMucID;
            dataInsert.IIdTieuMucId = data.iID_TieuMucID;
            dataInsert.SGhiChu = data.sGhiChu;
            dataInsert.STrangThaiDuAnDangKy = data.sTrangThaiDuAnDangKy;
            return dataInsert;
        }
        #endregion
    }
}
