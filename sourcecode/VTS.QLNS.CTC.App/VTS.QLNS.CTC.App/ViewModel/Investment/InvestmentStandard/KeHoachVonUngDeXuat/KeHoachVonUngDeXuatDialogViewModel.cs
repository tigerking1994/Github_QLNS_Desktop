using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
using System.Windows.Markup;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachVonUngDeXuat
{
    public class KeHoachVonUngDeXuatDialogViewModel : DialogViewModelBase<VdtKhvKeHoachVonUngDxModel>
    {
        #region Private
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtKhvKeHoachVonUngDxService _keHoachVonUngService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly INsMucLucNganSachService _mlNganSachService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _duAnView;
        private string _sMaDonViCap0;
        #endregion

        public override string Name => "Quản lý kế hoạch vốn ứng đề xuất";
        public override string Description => string.Format("{0} thông tin vốn ứng đề xuất", IsInsert ? "Thêm mới" : "Cập nhật");
        public Visibility HiddenTongHop => !BIsTongHop ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowTongHop => BIsTongHop ? Visibility.Visible : Visibility.Collapsed;
        public bool BIsEnableItem => IsInsert && (!BIsTongHop);

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

        private bool _bIsTongHop;
        public bool BIsTongHop
        {
            get => _bIsTongHop;
            set => SetProperty(ref _bIsTongHop, value);
        }

        private string _sSoDeNghi;
        public string SSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        private int? _iNamKeHoach;
        public int? INamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }

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
                if(SetProperty(ref _cbxNguonVonSelected, value))
                {
                    LoadDuAn(); ;
                }    

            }
        }

        private bool _isDieuChinh;
        public bool IsDieuChinh
        {
            get => _isDieuChinh;
            set => SetProperty(ref _isDieuChinh, value);
        }
        #endregion

        public KeHoachVonUngDeXuatDialogViewModel(INsDonViService nsDonViService,
            ISessionService sessionService,
            IVdtKhvKeHoachVonUngDxService keHoachVonUngService,
            INsNguonNganSachService nguonVonService,
            INsMucLucNganSachService mlNganSachService,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _keHoachVonUngService = keHoachVonUngService;
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
            if (Model == null) Model = new VdtKhvKeHoachVonUngDxModel();
            if (!Validate()) return;
            var dataInsert = _mapper.Map<VdtKhvKeHoachVonUngDx>(Model);
            var entity = new VdtKhvKeHoachVonUngDx();

            if (!BIsTongHop)
            {
                dataInsert.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
                dataInsert.IIdMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
            }
            else
            {
                if (dataInsert.Id == Guid.Empty)
                    dataInsert.BKhoa = true;
                dataInsert.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
                dataInsert.IIdMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
                dataInsert.STongHop = string.Join(";", ItemsTongHop.Select(n => n.Id.ToString()));
            }

            dataInsert.DNgayDeNghi = DNgayDeNghi;
            dataInsert.SSoDeNghi = SSoDeNghi;
            dataInsert.INamKeHoach = INamKeHoach ?? 0;
            dataInsert.IIdNguonVonId = int.Parse(CbxNguonVonSelected.ValueItem);
            if (dataInsert.Id == Guid.Empty)
            {
                dataInsert.BActive = true;
                dataInsert.BIsGoc = true;
                _keHoachVonUngService.Insert(dataInsert, _sessionService.Current.Principal);
                if (BIsTongHop)
                {
                    _keHoachVonUngService.InsertKhVonUngDeXuatTongHop(dataInsert.Id, ItemsTongHop.Select(n => n.Id).ToList());
                }
            }
            else
            {
                if (IsDieuChinh == IsInsert)
                {
                    _keHoachVonUngService.Update(dataInsert, _sessionService.Current.Principal);
                    dataInsert.IsModified = true;
                } else
                {
                    try
                    {
                        var entityDuplicate = _mapper.Map<VdtKhvKeHoachVonUngDx>(Model);
                        entity = new VdtKhvKeHoachVonUngDx();
                        entityDuplicate.CloneObj(entity);

                        entity.DNgayDeNghi = DNgayDeNghi;
                        entity.SSoDeNghi = SSoDeNghi;

                        entity.Id = Guid.NewGuid();
                        entity.DDateCreate = DateTime.Now;
                        entity.IIdParentId = entityDuplicate.Id;
                        entity.SUserCreate = _sessionService.Current.Principal;
                        entity.IIdMaDonViQuanLy = _cbxLoaiDonViSelected.ValueItem;
                        entity.IIdDonViQuanLyId = Guid.Parse(_cbxLoaiDonViSelected.HiddenValue);
                        entity.BActive = true;
                        entity.BIsGoc = false;

                        var listQuery = new List<VdtKhvKeHoachVonUngDxChiTietQuery>();

                        listQuery = _keHoachVonUngService.GetKeHoachVonUngChiTietByParentId(Model.Id).ToList();

                        var lstDetail = _mapper.Map<List<VdtKhvKeHoachVonUngDxChiTietModel>>(listQuery);

                        List<VdtKhvKeHoachVonUngDxChiTiet> lstData = new List<VdtKhvKeHoachVonUngDxChiTiet>();
                        foreach (var item in lstDetail)
                        {
                            VdtKhvKeHoachVonUngDxChiTiet itemData = ConvertData(item);
                            if (itemData == null) continue;
                            lstData.Add(itemData);
                        }

                        _keHoachVonUngService.Adjust(entity, lstData);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            Model = _mapper.Map<VdtKhvKeHoachVonUngDxModel>(!(IsDieuChinh == IsInsert) ? entity : dataInsert);
            Model.LstDuAnId = LstDuAn.Where(n => n.IsChecked).Select(n => n.iID_DuAnID).ToList();
            DialogHost.CloseDialogCommand.Execute(null, null);
            if (!BIsTongHop)
                Model.STenDonViQuanLy = CbxLoaiDonViSelected.DisplayItem;
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
            List<string> messageBuilder = new List<string>();
            if (CbxLoaiDonViSelected == null && !BIsTongHop)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Đơn vị quản lý"));
            }
            if (string.IsNullOrEmpty(SSoDeNghi))
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Số kế hoạch"));
            }
            if (!DNgayDeNghi.HasValue)
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


            if (!string.IsNullOrEmpty(SSoDeNghi))
            {
                if (_keHoachVonUngService.CheckTrungSoDeNghi(SSoDeNghi.Trim(), (IsDieuChinh != IsInsert ? Guid.Empty : Model.Id)))
                {
                    messageBuilder.Add("Trùng số kế hoạch");
                }
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
            if (CbxLoaiDonViSelected == null || !DNgayDeNghi.HasValue) return;
            List<VdtKhvKeHoachVonUngDxChiTietQuery> lstDataUpdate = new List<VdtKhvKeHoachVonUngDxChiTietQuery>();
            if (Model.Id != Guid.Empty)
            {
                lstDataUpdate = _keHoachVonUngService.GetKeHoachVonUngChiTietByParentId(Model.Id).ToList();
            }
            var data = _keHoachVonUngService.GetDuAnInKeHoachVonUngDetail(CbxLoaiDonViSelected.ValueItem,
                DNgayDeNghi.Value, null);

            if (_cbxNguonVonSelected!=null)
            {
                string strNguonVon= _cbxNguonVonSelected.ValueItem.ToString();
                LstDuAn = _mapper.Map<ObservableCollection<DuAnDenghiThanhToanModel>>(data.Where(x=>x.iID_NguonVonID== strNguonVon));
            }
            else
            {
                LstDuAn = _mapper.Map<ObservableCollection<DuAnDenghiThanhToanModel>>(data);
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
            return obj is DuAnDenghiThanhToanModel item &&
                (item.sTenDuAn.ToLower().Contains(_searchDuAn, StringComparison.OrdinalIgnoreCase)
                || item.sMaDuAn.ToLower().Contains(_searchDuAn, StringComparison.OrdinalIgnoreCase));
        }

        private void SetValueDefault()
        {
            if (!IsInsert || BIsTongHop)
            {
                if (!IsInsert)
                {
                    SSoDeNghi = Model.SSoDeNghi;
                    DNgayDeNghi = Model.DNgayDeNghi;
                    CbxLoaiDonViSelected = CbxLoaiDonVi.FirstOrDefault(n => n.ValueItem.ToUpper() == Model.IIDMaDonViQuanLy.ToUpper());
                }
                else
                {
                    SSoDeNghi = string.Empty;
                    DNgayDeNghi = DateTime.Now;
                    if (!string.IsNullOrEmpty(_sMaDonViCap0))
                        CbxLoaiDonViSelected = CbxLoaiDonVi.FirstOrDefault(n => n.ValueItem == _sMaDonViCap0);
                    else
                        CbxLoaiDonViSelected = null;
                }
                INamKeHoach = Model.INamKeHoach;
                if (Model.IIDNguonVonID.HasValue)
                    CbxNguonVonSelected = CbxNguonVon.FirstOrDefault(n => n.ValueItem == Model.IIDNguonVonID.Value.ToString());
            }
            else
            {
                SSoDeNghi = null;
                DNgayDeNghi = DateTime.Now;
                INamKeHoach = DateTime.Now.Year;
                CbxLoaiDonViSelected = null;
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

        private VdtKhvKeHoachVonUngDxChiTiet ConvertData(VdtKhvKeHoachVonUngDxChiTietModel data)
        {
            VdtKhvKeHoachVonUngDxChiTiet dataInsert = new VdtKhvKeHoachVonUngDxChiTiet();
            if (dataInsert == null) return null;
            dataInsert.Id = data.Id;
            dataInsert.FGiaTriDeNghi = data.FGiaTriDeNghi;
            dataInsert.FTiGia = data.FTiGia;
            dataInsert.FTiGiaDonVi = data.FTiGiaDonVi;
            dataInsert.IIdDonViTienTeId = data.IIDDonViTienTeID;
            dataInsert.IIdDuAnId = data.IIDDuAnID;
            dataInsert.ID_DuAn_HangMuc = data.ID_DuAn_HangMuc;
            dataInsert.IIdKeHoachUngId = Model.Id;
            dataInsert.IIdTienTeId = data.IIDTienTeID;
            dataInsert.SGhiChu = data.SGhiChu;
            dataInsert.STrangThaiDuAnDangKy = data.STrangThaiDuAnDangKy;
            return dataInsert;
        }
        #endregion
    }
}
