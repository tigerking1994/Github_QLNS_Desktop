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
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.AnnualSettlement
{
    public class AnnualSettlementDialogViewModel : DialogViewModelBase<VdtQtBcquyetToanNienDoModel>
    {
        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly IMucLucNganSachService _mlNganSachService;
        private readonly IVdtQtBcQuyetToanNienDoService _service;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        #endregion

        public override string Name => "Đề nghị quyết toán niên độ";
        public override string Description => "Cập nhật thông tin đề nghị quyết toán niên độ ";
        public bool IsInsert => Model.Id == Guid.Empty;
        public string sNguonVon { get; set; }

        #region Componer
        private string _sSoDeNghi;
        public string SSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        private string _sNguoiDeNghi;
        public string SNguoiDeNghi
        {
            get => _sNguoiDeNghi;
            set => SetProperty(ref _sNguoiDeNghi, value);
        }

        private DateTime? _dNgayDeNghi;
        public DateTime? DNgayDeNghi
        {
            get => _dNgayDeNghi;
            set => SetProperty(ref _dNgayDeNghi, value);
        }

        private string _iNamKeHoach;
        public string INamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
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

        private ComboboxItem _cbxLoaiBaoCaoSelected;
        public ComboboxItem CbxLoaiBaoCaoSelected
        {
            get => _cbxLoaiBaoCaoSelected;
            set => SetProperty(ref _cbxLoaiBaoCaoSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiBaoCao;
        public ObservableCollection<ComboboxItem> CbxLoaiBaoCao
        {
            get => _cbxLoaiBaoCao;
            set => SetProperty(ref _cbxLoaiBaoCao, value);
        }

        private ComboboxItem _cbxNguonVonSelected;
        public ComboboxItem CbxNguonVonSelected
        {
            get => _cbxNguonVonSelected;
            set => SetProperty(ref _cbxNguonVonSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxNguonVon;
        public ObservableCollection<ComboboxItem> CbxNguonVon
        {
            get => _cbxNguonVon;
            set => SetProperty(ref _cbxNguonVon, value);
        }
        #endregion

        public AnnualSettlementDialogViewModel(INsDonViService nsDonViService,
            IMucLucNganSachService mlNganSachService,
            ISessionService sessionService,
            INsNguonNganSachService nguonVonService,
            IVdtQtBcQuyetToanNienDoService service,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mlNganSachService = mlNganSachService;
            _nguonVonService = nguonVonService;
            _service = service;
            _mapper = mapper;
        }

        #region RelayCommand Event
        public override void Init()
        {
            LoadLoaiBaoCao();
            LoadComboBoxNguonVon();
            LoadComboBoxLoaiDonVi();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            SetDefaultData();
        }

        public override void OnSave()
        {
            int iNamKeHoach = 0;
            List<string> messageBuilder = new List<string>();
            if (Model == null) Model = new VdtQtBcquyetToanNienDoModel();
            if (CbxLoaiDonViSelected == null)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Đơn vị quản lý"));
            }
            if (string.IsNullOrEmpty(SSoDeNghi))
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Số phê duyệt"));
            }
            if (!DNgayDeNghi.HasValue)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Ngày phê duyệt"));
            }
            if (string.IsNullOrEmpty(INamKeHoach))
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Năm kế hoạch"));
            }
            else if(!int.TryParse(INamKeHoach, out iNamKeHoach))
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorFormat, "Năm kế hoạch"));
            }
            if (CbxLoaiBaoCaoSelected == null)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Loại báo cáo"));
            }
            if (messageBuilder.Count != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder));
                return;
            }

            var dataInsert = ConvertData();
            if (dataInsert.Id == Guid.Empty)
            {
                if (_service.CheckExistDeNghiQuyetToanNienDo(dataInsert.IIdMaDonViQuanLy, dataInsert.INamKeHoach.Value,
                    dataInsert.IIdNguonVonId.Value))
                {
                    messageBuilder.Add(string.Format(Resources.MsgErrorExitQuyetToanNienDo,
                        CbxLoaiDonViSelected.DisplayItem, dataInsert.INamKeHoach.Value.ToString(),
                        CbxNguonVonSelected.DisplayItem));
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }
                _service.Insert(dataInsert, _sessionService.Current.Principal);
            }
            else
            {
                _service.Update(dataInsert, _sessionService.Current.Principal);
                dataInsert.IsModified = true;
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(dataInsert);
        }
        #endregion

        #region Helper
        private VdtQtBcQuyetToanNienDo ConvertData()
        {
            var data = new VdtQtBcQuyetToanNienDo();
            data.DNgayDeNghi = DNgayDeNghi;
            data.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            data.IIdMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
            data.IIdNguonVonId = int.Parse(CbxNguonVonSelected.ValueItem);
            data.ILoaiThanhToan = int.Parse(CbxLoaiBaoCaoSelected.ValueItem);
            data.INamKeHoach = int.Parse(INamKeHoach);
            data.SSoDeNghi = SSoDeNghi;
            return data;
        }

        private void SetDefaultData()
        {
            if(Model.Id != Guid.Empty)
            {
                CbxLoaiDonViSelected = CbxLoaiDonVi.FirstOrDefault(n => n.ValueItem == Model.IIDMaDonViQuanLy);
                CbxLoaiBaoCaoSelected = CbxLoaiBaoCao.FirstOrDefault(n => n.ValueItem == (Model.ILoaiThanhToan ?? 0).ToString());
                CbxNguonVonSelected = CbxNguonVon.FirstOrDefault(n => n.ValueItem == (Model.IIDNguonVonID ?? 0).ToString());
                INamKeHoach = (Model.INamKeHoach ?? 0).ToString();
                SSoDeNghi = Model.SSoDeNghi;
                DNgayDeNghi = Model.DNgayDeNghi;
            }
            else
            {
                CbxLoaiDonViSelected = null;
                CbxLoaiBaoCaoSelected = null;
                CbxNguonVonSelected = null;
                INamKeHoach = null;
                SSoDeNghi = null;
                DNgayDeNghi = null;
            }
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(SSoDeNghi));
            OnPropertyChanged(nameof(DNgayDeNghi));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(CbxLoaiBaoCaoSelected));
            OnPropertyChanged(nameof(CbxNguonVonSelected));
        }

        private void LoadComboBoxLoaiDonVi()
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => _lstDonViExclude.Contains(n.Loai))
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = n.TenDonVi, HiddenValue = n.Id.ToString() });
            _cbxLoaiDonVi = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
            OnPropertyChanged(nameof(CbxLoaiDonVi));
        }

        private void LoadComboBoxNguonVon()
        {
            var cbxNguonVon = _nguonVonService.FindAll().OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
            _cbxNguonVon = new ObservableCollection<ComboboxItem>(cbxNguonVon);
            OnPropertyChanged(nameof(CbxNguonVon));
        }

        public void LoadLoaiBaoCao()
        {
            List<ComboboxItem> lstItem = new List<ComboboxItem>();
            lstItem.Add(new ComboboxItem()
            {
                ValueItem = ((int)LoaiQuyetToanEnum.Type.QUYET_TOAN_KHO_BAC).ToString(),
                DisplayItem = LoaiQuyetToanEnum.TypeName.QUYET_TOAN_KHO_BAC
            });
            CbxLoaiBaoCao = new ObservableCollection<ComboboxItem>(lstItem);
            OnPropertyChanged(nameof(CbxLoaiBaoCao));
        }
        #endregion
    }
}
