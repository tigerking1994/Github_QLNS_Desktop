using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThongTriCapPhat
{
    public class ThongTriCapPhatDialogViewModel : DialogViewModelBase<VdtThongTriModel>
    {
        #region Private
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IVdtTtDeNghiThanhToanService _thanhtoanService;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtThongTriService _thongTriService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        #endregion

        public override string Name => "Quản lý thông tri cấp phát";
        public bool IsInsert => !Model.Id.HasValue || Model.Id == Guid.Empty;
        private bool _isOpenedFromThongTriCapPhat; // if true, field 'Năm ngân sách' is visible

        public bool isOpenedFromThongTriCapPhat
        {
            get => _isOpenedFromThongTriCapPhat;
            set {
                SetProperty(ref _isOpenedFromThongTriCapPhat, value);
                isOpenedFromDeNghiThanhToan = !isOpenedFromThongTriCapPhat;
                OnPropertyChanged(nameof(isOpenedFromDeNghiThanhToan));
            }
        }

        private bool _isOpenedFromDeNghiThanhToan; // if true, Bảng is visible

        public bool isOpenedFromDeNghiThanhToan
        {
            get => _isOpenedFromDeNghiThanhToan;
            set => SetProperty(ref _isOpenedFromDeNghiThanhToan, value);
        }

        private ObservableCollection<VdtTtDeNghiThanhToanModel> _listCapPhatThanhToan;
        public ObservableCollection<VdtTtDeNghiThanhToanModel> listCapPhatThanhToan
        {
            get => _listCapPhatThanhToan;
            set => SetProperty(ref _listCapPhatThanhToan, value);
        }
        public bool BIsThanhToan => SelectedLoaiThongTri != null
            && (SelectedLoaiThongTri.ValueItem == ((int)LoaiThongTriEnum.Type.CAP_THANH_TOAN).ToString() || SelectedLoaiThongTri.ValueItem == ((int)LoaiThongTriEnum.Type.CAP_TAM_UNG).ToString());
        public override string Description => string.Format("{0} thông tin thông tri cấp phát", IsInsert ? "Thêm mới" : "Cập nhật");

        #region Componer
        private bool _bFromThanhToan;
        public bool BFromThanhToan
        {
            get => _bFromThanhToan;
            set => SetProperty(ref _bFromThanhToan, value);
        }

        private string _sNamThongTri;
        public string SNamThongTri
        {
            get => _sNamThongTri;
            set => SetProperty(ref _sNamThongTri, value);
        }

        private DateTime? _dNgayThongTri;
        public DateTime? DNgayThongTri
        {
            get => _dNgayThongTri;
            set => SetProperty(ref _dNgayThongTri, value);
        }

        private string? _sMoTa;
        public string? SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set => SetProperty(ref _cbxLoaiDonViSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }

        private ComboboxItem _cbxNguonNganSachSelected;
        public ComboboxItem CbxNguonNganSachSelected
        {
            get => _cbxNguonNganSachSelected;
            set => SetProperty(ref _cbxNguonNganSachSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxNguonNganSach;
        public ObservableCollection<ComboboxItem> CbxNguonNganSach
        {
            get => _cbxNguonNganSach;
            set => SetProperty(ref _cbxNguonNganSach, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiThongTri;
        public ObservableCollection<ComboboxItem> ItemsLoaiThongTri
        {
            get => _itemsLoaiThongTri;
            set => SetProperty(ref _itemsLoaiThongTri, value);
        }



        private ObservableCollection<ComboboxItem> _itemsNamNganSach;
        public ObservableCollection<ComboboxItem> ItemsNamNganSach
        {
            get => _itemsNamNganSach;
            set => SetProperty(ref _itemsNamNganSach, value);
        }

        private ComboboxItem _selectedNamNganSach;
        public ComboboxItem SelectedNamNganSach
        {
            get => _selectedNamNganSach;
            set => SetProperty(ref _selectedNamNganSach, value);
        }

        private ComboboxItem _selectedLoaiThongTri;
        public ComboboxItem SelectedLoaiThongTri
        {
            get => _selectedLoaiThongTri;
            set
            {
                SetProperty(ref _selectedLoaiThongTri, value);
                OnPropertyChanged(nameof(BIsThanhToan));
            }
        }

        private List<Guid> _itemsCanCuId;
        public List<Guid> ItemsCanCuId
        {
            get => _itemsCanCuId;
            set => SetProperty(ref _itemsCanCuId, value);
        }

        public IEnumerable<VdtTtDeNghiThanhToanModel> ItemsChungTuThanhToan { get; set; }

        #endregion

        public ThongTriCapPhatDialogViewModel(
            INsNguonNganSachService nsNguonNganSachService,
            IVdtTtDeNghiThanhToanService thanhtoanService,
            INsDonViService nsDonViService,
            IVdtThongTriService thongTriService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _nsNguonNganSachService = nsNguonNganSachService;
            _thanhtoanService = thanhtoanService;
            _nsDonViService = nsDonViService;
            _thongTriService = thongTriService;
            _sessionService = sessionService;
            _mapper = mapper;
            listCapPhatThanhToan = new ObservableCollection<VdtTtDeNghiThanhToanModel>();
        }

        #region RelayCommand Event
        public override void Init()
        {
            resetCondittion();
            LoadData();
        }

        private void resetCondittion()
        {
            SMoTa = "";
        }

        public override void LoadData(params object[] args)
        {
            LoadLoaiThongTri();
            LoadNamNganSach();
            LoadComboBoxLoaiDonVi();
            LoadNguonVon();
            if (Model.Id != Guid.Empty && Model.Id != null)
            {
                listCapPhatThanhToan.Clear();
                getListDeNghiThanhToanThongTri();
            }
            SNamThongTri = Model.iNamThongTri <= 0 ? string.Empty : Model.iNamThongTri.ToString();
            DNgayThongTri = !Model.dNgayThongTri.HasValue ? DateTime.Now : Model.dNgayThongTri.Value;
        }

        private void getListDeNghiThanhToanThongTri()
        {
            _thanhtoanService.getListDeNghiThanhToanByThongtriId(Model.Id, _sessionService.Current.YearOfWork, _sessionService.Current.Principal).ForEach(deNghi =>
            {
                VdtTtDeNghiThanhToanModel model = _mapper.Map<VdtTtDeNghiThanhToanModel>(deNghi);
                listCapPhatThanhToan.Add(model);
            });
        }

        public override void OnSave()
        {
            int iNamKehoach = 0;
            StringBuilder messageBuilder = new StringBuilder();
            if (Model == null) Model = new VdtThongTriModel();
            if (CbxLoaiDonViSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị quản lý");
            }
            if (string.IsNullOrEmpty(SNamThongTri))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Năm làm việc");
            }
            else if (!int.TryParse(SNamThongTri, out iNamKehoach))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorFormat, "Năm làm việc");
            }
            if (CbxNguonNganSachSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorFormat, "Nguồn vốn");
            }
            if (SelectedLoaiThongTri == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorFormat, "Loại cấp phát");
            }
            if (string.IsNullOrEmpty(Model.sMaThongTri))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Mã thông tri");
            }
            if (SelectedNamNganSach == null && isOpenedFromThongTriCapPhat)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Năm ngân sách");
            }
            if (!DNgayThongTri.HasValue)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Ngày lập");
            }
            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return;
            }
            var dataInsert = _mapper.Map<VdtThongTri>(Model);
            dataInsert.iIDMaDonViID = CbxLoaiDonViSelected.ValueItem;
            dataInsert.IIdDonViId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            dataInsert.ILoaiThongTri = int.Parse(SelectedLoaiThongTri.ValueItem);
            if(isOpenedFromThongTriCapPhat)
                dataInsert.INamNganSach = int.Parse(SelectedNamNganSach.ValueItem);
            //dataInsert.BThanhToan = ThongChiThanhToanEnum.Get((int)ThongChiThanhToanEnum.Type.CAP_THANH_TOAN);
            dataInsert.IIdLoaiThongTriId = _thongTriService.GetAllDmLoaiThongTri().FirstOrDefault(n => n.IKieuLoaiThongTri == (int)LoaiThongTri.THONG_TRI_THANH_TOAN).Id;
            dataInsert.SMaNguonVon = CbxNguonNganSachSelected.ValueItem;
            dataInsert.DNgayThongTri = DNgayThongTri;
            dataInsert.INamThongTri = iNamKehoach;
            dataInsert.SMoTa = SMoTa;
            if (dataInsert.Id == Guid.Empty)
            {
                _thongTriService.Insert(dataInsert, _sessionService.Current.Principal);               
            }
            else
            {
                _thongTriService.Update(dataInsert, _sessionService.Current.Principal);
                dataInsert.IsModified = true;
            }
            if (BIsThanhToan)
            {
                _thanhtoanService.UpdateThongTriThanhToan(dataInsert.Id, ItemsCanCuId);
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
            Model = _mapper.Map<VdtThongTriModel>(dataInsert);
            Model.ItemsChungTuThanhToan = ItemsChungTuThanhToan;
            Model.sTenDonVi = CbxLoaiDonViSelected.DisplayItem.Split("-")[1];
            SavedAction?.Invoke(Model);
        }
        #endregion

        #region Helper
        private void LoadComboBoxLoaiDonVi()
        {
            var cbxLoaiDonViData = _nsDonViService.GetDanhSachDonViByNguoiDung(
                _sessionService.Current.Principal, _sessionService.Current.YearOfWork)
                .Where(n => string.IsNullOrEmpty(Model.iID_MaDonViID) || n.IIDMaDonVi == Model.iID_MaDonViID)
                .OrderBy(o => o.IIDMaDonVi)
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi), HiddenValue = n.Id.ToString() });
            _cbxLoaiDonVi = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
            if (!string.IsNullOrEmpty(Model.iID_MaDonViID))
            {
                CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault(n => n.ValueItem.ToUpper() == Model.iID_MaDonViID.ToUpper());
            }
            else
            {
                CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault();
            }
            OnPropertyChanged(nameof(CbxLoaiDonVi));
        }

        public void LoadNguonVon()
        {
            List<ComboboxItem> cbxNguonVonData = new List<ComboboxItem>();
            cbxNguonVonData = _nsNguonNganSachService.FindNguonNganSach()
                .Where(n => n.SMoTa != NSNguonNganSachEnum.TypeCode.NGAN_SACH_QP)
                .Select(n =>
                new ComboboxItem()
                {
                    DisplayItem = n.STen,
                    ValueItem = n.SMoTa,
                    HiddenValue = n.IIdMaNguonNganSach.ToString()
                }).ToList();

            _cbxNguonNganSach = new ObservableCollection<ComboboxItem>(cbxNguonVonData);
            if (!string.IsNullOrEmpty(Model.sMaNguonVon))
            {
                CbxNguonNganSachSelected = _cbxNguonNganSach.FirstOrDefault(n => n.ValueItem.ToUpper() == Model.sMaNguonVon.ToUpper());
            }
            else
            {
                CbxNguonNganSachSelected = _cbxNguonNganSach.FirstOrDefault();
            }
            OnPropertyChanged(nameof(CbxNguonNganSach));
        }

        private void LoadLoaiThongTri()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            if (BFromThanhToan || (Model.Id != null && Model.Id != Guid.Empty))
            {
                if (Model.ILoaiThongTri == (int)LoaiThongTriEnum.Type.CAP_THANH_TOAN)
                    lstData.Add(new ComboboxItem() { DisplayItem = LoaiThongTriEnum.Name.CAP_THANH_TOAN, ValueItem = ((int)LoaiThongTriEnum.Type.CAP_THANH_TOAN).ToString() });
                else
                    lstData.Add(new ComboboxItem() { DisplayItem = LoaiThongTriEnum.Name.CAP_TAM_UNG, ValueItem = ((int)LoaiThongTriEnum.Type.CAP_TAM_UNG).ToString() });
            }
            if (!BFromThanhToan)
            {
                lstData.Add(new ComboboxItem() { DisplayItem = LoaiThongTriEnum.Name.CAP_KINH_PHI, ValueItem = ((int)LoaiThongTriEnum.Type.CAP_KINH_PHI).ToString() });
                lstData.Add(new ComboboxItem() { DisplayItem = LoaiThongTriEnum.Name.CAP_HOP_THUC, ValueItem = ((int)LoaiThongTriEnum.Type.CAP_HOP_THUC).ToString() });
            }
            ItemsLoaiThongTri = new ObservableCollection<ComboboxItem>(lstData);
            if (Model != null)
            {
                SelectedLoaiThongTri = ItemsLoaiThongTri.FirstOrDefault(n => n.ValueItem == Model.ILoaiThongTri.ToString());
            }
        }

        private void LoadNamNganSach()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem() { DisplayItem = NamNganSachThongTri.Name.NAM_TRUOC_CHUYEN_SANG, ValueItem = ((int)NamNganSachThongTri.Type.NAM_TRUOC_CHUYEN_SANG).ToString() });
            lstData.Add(new ComboboxItem() { DisplayItem = NamNganSachThongTri.Name.NAM_NAY, ValueItem = ((int)NamNganSachThongTri.Type.NAM_NAY).ToString() });
            ItemsNamNganSach = new ObservableCollection<ComboboxItem>(lstData);
            if (Model != null)
            {
                SelectedNamNganSach = ItemsNamNganSach.FirstOrDefault(n => n.ValueItem == Model.INamNganSach.ToString());
            }
        }
        #endregion
    }
}
