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

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachChiQuy
{
    public class KeHoachChiQuyDialogViewModel : DialogViewModelBase<VdtNcNhuCauChiModel>
    {
        #region Private
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtNcNhuCauChiService _service;
        private readonly ISessionService _sessionService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        #endregion

        public override string Name => "Quản lý kế hoạch chi Quý";
        public bool IsInsert => Model.Id == Guid.Empty;
        public override string Description => string.Format("{0} thông tin kế hoạch chi Quý", IsInsert ? "Thêm mới" : "Cập nhật");

        #region Componer
        private string _sSoDeNghi;
        public string SSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        private DateTime? _dNgayDeNghi;
        public DateTime? DNgayDeNghi
        {
            get => _dNgayDeNghi;
            set => SetProperty(ref _dNgayDeNghi, value);
        }

        private int? _iNamKeHoach;
        public int? INamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                if (SetProperty(ref _iNamKeHoach, value))
                {
                    GetKinhPhiCucTaiChinhCap();
                }
            }
        }

        private string _sNguoiLap;
        public string SNguoiLap
        {
            get => _sNguoiLap;
            set => SetProperty(ref _sNguoiLap, value);
        }

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set
            {
                if (SetProperty(ref _cbxLoaiDonViSelected, value))
                {
                    GetKinhPhiCucTaiChinhCap();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }

        private ComboboxItem _cbxQuySelected;
        public ComboboxItem CbxQuySelected
        {
            get => _cbxQuySelected;
            set
            {
                if (SetProperty(ref _cbxQuySelected, value))
                {
                    GetKinhPhiCucTaiChinhCap();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxQuy;
        public ObservableCollection<ComboboxItem> CbxQuy
        {
            get => _cbxQuy;
            set => SetProperty(ref _cbxQuy, value);
        }

        private ComboboxItem _cbxNguonVonSelected;
        public ComboboxItem CbxNguonVonSelected
        {
            get => _cbxNguonVonSelected;
            set
            {
                if (SetProperty(ref _cbxNguonVonSelected, value))
                {
                    GetKinhPhiCucTaiChinhCap();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxNguonVon;
        public ObservableCollection<ComboboxItem> CbxNguonVon
        {
            get => _cbxNguonVon;
            set => SetProperty(ref _cbxNguonVon, value);
        }

        private double? _fQuyTruocChuaGiaiNgan;
        public double? FQuyTruocChuaGiaiNgan
        {
            get => _fQuyTruocChuaGiaiNgan;
            set => SetProperty(ref _fQuyTruocChuaGiaiNgan, value);
        }

        private double? _fGiaiNganQuyNay;
        public double? FGiaiNganQuyNay
        {
            get => _fGiaiNganQuyNay;
            set => SetProperty(ref _fGiaiNganQuyNay, value);
        }

        private double? _fThucHienGiaiNgan;
        public double? FThucHienGiaiNgan
        {
            get => _fThucHienGiaiNgan;
            set => SetProperty(ref _fThucHienGiaiNgan, value);
        }

        private double? _fKinhPhiChuyenQuySau;
        public double? FKinhPhiChuyenQuySau
        {
            get => _fKinhPhiChuyenQuySau;
            set => SetProperty(ref _fKinhPhiChuyenQuySau, value);
        }

        private double? _fKinhPhiCapQuyToi;
        public double? FKinhPhiCapQuyToi
        {
            get => _fKinhPhiCapQuyToi;
            set => SetProperty(ref _fKinhPhiCapQuyToi, value);
        }
        #endregion

        public KeHoachChiQuyDialogViewModel(
            INsDonViService nsDonViService,
            IVdtNcNhuCauChiService service,
            ISessionService sessionService,
            INsNguonNganSachService nsNguonNganSachService)
        {
            _nsDonViService = nsDonViService;
            _service = service;
            _sessionService = sessionService;
            _nsNguonNganSachService = nsNguonNganSachService;
        }

        #region Process
        public override void Init()
        {
            LoadDonVi();
            LoadQuy();
            LoaiNguonVon();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            SetDefaultData();
        }

        public override void OnSave()
        {
            if (!ValidateData()) return;
            var data = ConvertData();
            if (Model.Id == Guid.Empty)
            {
                data.DDateCreate = DateTime.Now;
                data.SUserCreate = _sessionService.Current.Principal;
                if (_service.IsExistSoDeNghi(data))
                {
                    MessageBox.Show(string.Join("\n", Resources.MsgTrungSoQuyetDinhs));
                    return;
                }
                _service.InsertKeHoachChiQuy(data);
            }
            else
            {
                data.DDateUpdate = DateTime.Now;
                data.SUserUpdate = _sessionService.Current.Principal;
                _service.UpdateKeHoachChiQuy(data);
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(data);
        }
        #endregion

        #region Helper
        private VdtNcNhuCauChi ConvertData()
        {
            VdtNcNhuCauChi data = new VdtNcNhuCauChi();
            data.Id = Model.Id;
            data.DNgayDeNghi = DNgayDeNghi;
            data.SSoDeNghi = SSoDeNghi;
            data.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            data.IIdMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
            data.IIdNguonVonId = int.Parse(CbxNguonVonSelected.ValueItem);
            data.IQuy = int.Parse(CbxQuySelected.ValueItem);
            data.INamKeHoach = INamKeHoach;
            data.SNguoiLap = SNguoiLap;
            data.SNoiDung = Model.SNoiDung;
            return data;
        }

        private void GetKinhPhiCucTaiChinhCap()
        {
            if (!INamKeHoach.HasValue || CbxLoaiDonViSelected == null || CbxNguonVonSelected == null || CbxQuySelected == null)
            {
                FQuyTruocChuaGiaiNgan = 0;
                FGiaiNganQuyNay = 0;
                FThucHienGiaiNgan = 0;
                FKinhPhiChuyenQuySau = 0;
            }
            else
            {
                var data = _service.GetKinhPhiCucTaiChinhCap(INamKeHoach.Value,
                    CbxLoaiDonViSelected.ValueItem,
                    int.Parse(CbxNguonVonSelected.ValueItem),
                    int.Parse(CbxQuySelected.ValueItem));
                if (data != null)
                {
                    FQuyTruocChuaGiaiNgan = data.fQuyTruocChuaGiaiNgan;
                    FGiaiNganQuyNay = data.fQuyNayDuocCap;
                    FThucHienGiaiNgan = data.fGiaiNganQuyNay;
                    FKinhPhiChuyenQuySau = data.fChuaGiaiNganChuyenQuySau;
                }
            }
            OnPropertyChanged(nameof(FQuyTruocChuaGiaiNgan));
            OnPropertyChanged(nameof(FGiaiNganQuyNay));
            OnPropertyChanged(nameof(FThucHienGiaiNgan));
            OnPropertyChanged(nameof(FKinhPhiChuyenQuySau));
        }

        private bool ValidateData()
        {
            List<string> lstError = new List<string>();
            if (CbxLoaiDonViSelected == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Đơn vị quản lý"));
            }
            if (string.IsNullOrEmpty(SSoDeNghi))
            {
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Số đề nghị"));
            }
            if (!DNgayDeNghi.HasValue)
            {
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Ngày đề nghị"));
            }
            if (CbxNguonVonSelected == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Nguồn vốn"));
            }
            if (CbxQuySelected == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Quý"));
            }
            if (!INamKeHoach.HasValue)
            {
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Năm kế hoạch"));
            }
            if (lstError.Count != 0)
            {
                MessageBox.Show(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        private void SetDefaultData()
        {
            if (Model.Id == Guid.Empty)
            {
                CbxLoaiDonViSelected = null;
                SSoDeNghi = null;
                DNgayDeNghi = DateTime.Now;
                CbxNguonVonSelected = null;
                CbxQuySelected = null;
                SNguoiLap = null;
                INamKeHoach = null;
                FQuyTruocChuaGiaiNgan = null;
                FThucHienGiaiNgan = null;
                FKinhPhiChuyenQuySau = null;
                FKinhPhiCapQuyToi = null;
                FGiaiNganQuyNay = null;
            }
            else
            {
                CbxLoaiDonViSelected = CbxLoaiDonVi.FirstOrDefault(n => n.ValueItem == Model.iID_MaDonViQuanLy);
                SSoDeNghi = Model.sSoDeNghi;
                DNgayDeNghi = Model.dNgayDeNghi;
                CbxNguonVonSelected = CbxNguonVon.FirstOrDefault(n => n.ValueItem == Model.iID_NguonVonID.ToString());
                CbxQuySelected = CbxQuy.FirstOrDefault(n => n.ValueItem == Model.iQuy.ToString()); ;
                SNguoiLap = Model.sNguoiLap;
                INamKeHoach = Model.iNamKeHoach;
            }
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(SSoDeNghi));
            OnPropertyChanged(nameof(DNgayDeNghi));
            OnPropertyChanged(nameof(CbxNguonVonSelected));
            OnPropertyChanged(nameof(CbxQuySelected));
            OnPropertyChanged(nameof(SNguoiLap));
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
        }
        private void LoadDonVi()
        {
            var data = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Select(n => new ComboboxItem() { DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi), ValueItem = n.IIDMaDonVi, HiddenValue = n.Id.ToString() });
            CbxLoaiDonVi = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(CbxLoaiDonVi));
        }

        private void LoadQuy()
        {
            List<ComboboxItem> data = new List<ComboboxItem>();
            data.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_1, ValueItem = ((int)LoaiQuyEnum.Type.QUY_1).ToString() });
            data.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_2, ValueItem = ((int)LoaiQuyEnum.Type.QUY_2).ToString() });
            data.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_3, ValueItem = ((int)LoaiQuyEnum.Type.QUY_3).ToString() });
            data.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_4, ValueItem = ((int)LoaiQuyEnum.Type.QUY_4).ToString() });
            CbxQuy = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(CbxQuy));
        }

        private void LoaiNguonVon()
        {
            var data = _nsNguonNganSachService.FindNguonNganSach()
                .OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { DisplayItem = n.STen, ValueItem = n.IIdMaNguonNganSach.ToString() });
            CbxNguonVon = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(CbxNguonVon));
        }
        #endregion
    }
}
