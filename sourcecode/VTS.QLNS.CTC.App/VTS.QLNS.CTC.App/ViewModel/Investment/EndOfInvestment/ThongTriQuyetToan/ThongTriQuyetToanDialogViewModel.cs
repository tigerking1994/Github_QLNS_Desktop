using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.ThongTriQuyetToan
{
    public class ThongTriQuyetToanDialogViewModel : DialogViewModelBase<VdtThongTriModel>
    {
        #region Private
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IDmLoaiCongTrinhService _dmLoaiCongTrinhService;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtThongTriService _thongTriService;
        private readonly IVdtQtBcQuyetToanNienDoService _quyetToanService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        #endregion

        public override string Name => "Quản lý thông tri quyết toán";
        public bool IsInsert => !Model.Id.HasValue || Model.Id == Guid.Empty;
        public override string Description => string.Format("{0} thông tin thông tri quyết toán", IsInsert ? "Thêm mới" : "Cập nhật");

        #region Componer
        private string _sNamThongTri;
        public string SNamThongTri
        {
            get => _sNamThongTri;
            set
            {
                if (SetProperty(ref _sNamThongTri, value))
                {
                    LoadChungTuQuyetToan();
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
                    LoadChungTuQuyetToan();
                }
            }
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
            set
            {
                if (SetProperty(ref _cbxNguonNganSachSelected, value))
                {
                    LoadChungTuQuyetToan();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxNguonNganSach;
        public ObservableCollection<ComboboxItem> CbxNguonNganSach
        {
            get => _cbxNguonNganSach;
            set => SetProperty(ref _cbxNguonNganSach, value);
        }

        private ObservableCollection<VdtQtBcquyetToanNienDoModel> _itemsQuyetToan;
        public ObservableCollection<VdtQtBcquyetToanNienDoModel> ItemsQuyetToan
        {
            get => _itemsQuyetToan;
            set => SetProperty(ref _itemsQuyetToan, value);
        }
        #endregion

        public ThongTriQuyetToanDialogViewModel(
            INsNguonNganSachService nsNguonNganSachService,
            IDmLoaiCongTrinhService dmLoaiCongTrinhService,
            INsDonViService nsDonViService,
            IVdtThongTriService thongTriService,
            IVdtQtBcQuyetToanNienDoService quyetToanService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _dmLoaiCongTrinhService = dmLoaiCongTrinhService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nsDonViService = nsDonViService;
            _thongTriService = thongTriService;
            _quyetToanService = quyetToanService;
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
            LoadComboBoxLoaiDonVi();
            LoadNguonVon();
            SNamThongTri = Model.iNamThongTri <= 0 ? string.Empty : Model.iNamThongTri.ToString();
        }

        public override void OnSave()
        {
            int iNamThongTri = 0;
            StringBuilder messageBuilder = new StringBuilder();
            if (Model == null) Model = new VdtThongTriModel();
            if (CbxLoaiDonViSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị quản lý");
                messageBuilder.AppendLine();
            }
            if (CbxNguonNganSachSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Nguồn vốn");
                messageBuilder.AppendLine();
            }
            if (string.IsNullOrEmpty(SNamThongTri))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Năm thực hiện");
                messageBuilder.AppendLine();
            }
            else if (!int.TryParse(SNamThongTri, out iNamThongTri))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorFormat, "Năm thực hiện");
                messageBuilder.AppendLine();
            }
            if (string.IsNullOrEmpty(Model.sMaThongTri))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Mã thông tri");
                messageBuilder.AppendLine();
            }
            if (!Model.dNgayThongTri.HasValue)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Ngày tạo thông tri");
                messageBuilder.AppendLine();
            }
            if (string.IsNullOrEmpty(Model.sNguoiLap))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Người lập thông tri");
                messageBuilder.AppendLine();
            }
            if (ItemsQuyetToan == null || !ItemsQuyetToan.Any(n => n.IsChecked))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorNotChooseRecordAnnounce);
                messageBuilder.AppendLine();
            }
            else if (ItemsQuyetToan.Where(n => n.IsChecked).Count() > 1)
            {
                messageBuilder.AppendFormat(Resources.ChooseOneVoucher);
                messageBuilder.AppendLine();
            }
            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                LoadData();
                return;
            }
            var dataInsert = _mapper.Map<VdtThongTri>(Model);
            dataInsert.iIDMaDonViID = CbxLoaiDonViSelected.ValueItem;
            dataInsert.IIdDonViId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            dataInsert.IIdLoaiThongTriId = _thongTriService.GetAllDmLoaiThongTri().FirstOrDefault(n => n.IKieuLoaiThongTri == (int)LoaiThongTri.THONG_TRI_QUYET_TOAN).Id;
            dataInsert.SMaNguonVon = CbxNguonNganSachSelected.HiddenValue;
            dataInsert.IIdBcQuyetToanNienDo = ItemsQuyetToan.FirstOrDefault(n => n.IsChecked).Id;
            dataInsert.INamThongTri = iNamThongTri;
            if (dataInsert.Id == Guid.Empty)
            {
                _thongTriService.Insert(dataInsert, _sessionService.Current.Principal);
            }
            else
            {
                _thongTriService.Update(dataInsert, _sessionService.Current.Principal);
                dataInsert.IsModified = true;
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(dataInsert);
        }
        #endregion

        #region Helper
        private void LoadComboBoxLoaiDonVi()
        {
            var cbxLoaiDonViData = _nsDonViService.GetDanhSachDonViByNguoiDung(
                _sessionService.Current.Principal, _sessionService.Current.YearOfWork)
                .Where(n => string.IsNullOrEmpty(Model.iID_MaDonViID) || n.IIDMaDonVi == Model.iID_MaDonViID)
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = n.IIDMaDonVi + "-" +n.TenDonVi, HiddenValue = n.Id.ToString() });
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
            List<ComboboxItem> cbxNguonVonData = _nsNguonNganSachService.FindNguonNganSach()
                .Select(n =>
                new ComboboxItem()
                {
                    DisplayItem = n.STen,
                    ValueItem = n.IIdMaNguonNganSach.ToString(),
                    HiddenValue = n.SMoTa
                }).ToList();
            _cbxNguonNganSach = new ObservableCollection<ComboboxItem>(cbxNguonVonData);
            if (!string.IsNullOrEmpty(Model.sMaNguonVon))
            {
                CbxNguonNganSachSelected = _cbxNguonNganSach.FirstOrDefault(n => n.HiddenValue.ToUpper() == Model.sMaNguonVon.ToUpper());
            }
            else
            {
                CbxNguonNganSachSelected = _cbxNguonNganSach.FirstOrDefault();
            }
            OnPropertyChanged(nameof(CbxNguonNganSach));
        }

        private void LoadChungTuQuyetToan()
        {
            int iNamThongTri = 0;
            ItemsQuyetToan = new ObservableCollection<VdtQtBcquyetToanNienDoModel>();
            if (CbxLoaiDonViSelected == null || string.IsNullOrEmpty(SNamThongTri) || CbxNguonNganSach == null || !int.TryParse(SNamThongTri, out iNamThongTri)) return;
            var lstData = _quyetToanService.GetBcQuyetToanInThongTriScreen(Model.Id, CbxLoaiDonViSelected.ValueItem, iNamThongTri, int.Parse(CbxNguonNganSachSelected.ValueItem));
            ItemsQuyetToan = new ObservableCollection<VdtQtBcquyetToanNienDoModel>(_mapper.Map<List<VdtQtBcquyetToanNienDoModel>>(lstData));
            if (Model != null && Model.IIdBcQuyetToanNienDo.HasValue)
            {
                foreach (var item in ItemsQuyetToan)
                {
                    if (item.Id == Model.IIdBcQuyetToanNienDo.Value)
                    {
                        item.IsChecked = true;
                        break;
                    }
                }
            }
        }
        #endregion
    }
}