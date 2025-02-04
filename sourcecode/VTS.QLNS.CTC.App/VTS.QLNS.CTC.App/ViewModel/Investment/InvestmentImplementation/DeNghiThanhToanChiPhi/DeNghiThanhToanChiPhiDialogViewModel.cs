using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.DeNghiThanhToanChiPhi
{
    public class DeNghiThanhToanChiPhiDialogViewModel : DialogAttachmentViewModelBase<VdtTtDeNghiThanhToanChiPhiIndexModel>
    {
        #region Private
        private static string[] _lstDonViInclude = new string[] { "0", "1" };
        private readonly IVdtTtDeNghiThanhToanChiPhiService _service;
        private readonly IVdtKhvPhanBoVonChiPhiService _phanbovonchiphiService;
        private readonly IVdtTtDeNghiThanhToanService _capphatService;
        private readonly INsDonViService _donviService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private List<VdtTtDeNghiThanhToanQuery> _lstChungTu;
        #endregion

        public override string Name => "Đề nghị thanh toán theo chi phí";
        public override string Description => string.Format("{0} đề nghị thanh toán theo chi phí", Model.Id == Guid.Empty ? "Thêm mới" : "Cập nhật");

        #region Items
        private ObservableCollection<VdtKhvPhanBoVonChiPhiModel> _itemsDuToan;
        public ObservableCollection<VdtKhvPhanBoVonChiPhiModel> ItemsDuToan
        {
            get => _itemsDuToan;
            set => SetProperty(ref _itemsDuToan, value);
        }

        private VdtKhvPhanBoVonChiPhiModel _selectedDuToan;
        public VdtKhvPhanBoVonChiPhiModel SelectedDuToan
        {
            get => _selectedDuToan;
            set => SetProperty(ref _selectedDuToan, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                LoadDuAn();
                LoadPhanBoVonChiPhi();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDuAn;
        public ObservableCollection<ComboboxItem> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private ComboboxItem _selectedDuAn;
        public ComboboxItem SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                SetProperty(ref _selectedDuAn, value);
                LoadPhanBoVonChiPhi();
                LoadChungTu();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsChungTu;
        public ObservableCollection<ComboboxItem> ItemsChungTu
        {
            get => _itemsChungTu;
            set => SetProperty(ref _itemsChungTu, value);
        }

        private ComboboxItem _selectedChungTu;
        public ComboboxItem SelectedChungTu
        {
            get => _selectedChungTu;
            set => SetProperty(ref _selectedChungTu, value);
        }

        private int? _iNamKeHoach;
        public int? INamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                SetProperty(ref _iNamKeHoach, value);
                LoadDuAn();
                LoadPhanBoVonChiPhi();
            }
        }
        #endregion

        public DeNghiThanhToanChiPhiDialogViewModel(
            IVdtTtDeNghiThanhToanChiPhiService service,
            IVdtKhvPhanBoVonChiPhiService phanbovonchiphiService,
            IVdtTtDeNghiThanhToanService capphatService,
            INsDonViService donviService,
            ISessionService sessionService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            IMapper mapper,
            ILog logger) : base(mapper, storageServiceFactory, attachService)
        {
            _service = service;
            _phanbovonchiphiService = phanbovonchiphiService;
            _capphatService = capphatService;
            _donviService = donviService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
        }

        public override void Init()
        {
            LoadDonVi();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id != Guid.Empty)
            {
                INamKeHoach = Model.INamKeHoach;
                OnPropertyChanged(nameof(INamKeHoach));
            }
        }

        #region Event
        public override void OnSave()
        {
            if (!Validate()) return;
            VdtTtDeNghiThanhToanChiPhi obj = new VdtTtDeNghiThanhToanChiPhi();
            if (Model.Id == Guid.Empty)
            {
                obj = SetDataSave(obj);
                obj.Id = Guid.NewGuid();
                obj.DDateCreate = DateTime.Now;
                obj.SUserCreate = _sessionService.Current.Principal;
                _service.Insert(obj);
            }
            else
            {
                obj = _service.Find(Model.Id);
                obj = SetDataSave(obj);
                obj.DDateUpdate = DateTime.Now;
                obj.SUserUpdate = _sessionService.Current.Principal;
                _service.Update(obj);
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
            var tempVdtTtDeNghiThanhToanChiPhiIndexModel = _mapper.Map<VdtTtDeNghiThanhToanChiPhiIndexModel>(obj);
            tempVdtTtDeNghiThanhToanChiPhiIndexModel.STenDuAn = SelectedDuAn.DisplayItem;
            tempVdtTtDeNghiThanhToanChiPhiIndexModel.STenDonVi = SelectedDonVi.DisplayItem;
            SavedAction?.Invoke(tempVdtTtDeNghiThanhToanChiPhiIndexModel);
        }
        #endregion

        #region Helper
        private VdtTtDeNghiThanhToanChiPhi SetDataSave(VdtTtDeNghiThanhToanChiPhi obj)
        {
            obj.IIdMaDonViQuanLy = SelectedDonVi.ValueItem;
            obj.IIdDonViQuanLyId = Guid.Parse(SelectedDonVi.HiddenValue);
            obj.IIdDuAnId = Guid.Parse(SelectedDuAn.ValueItem);
            obj.IIdDeNghiThanhToanId = Guid.Parse(SelectedChungTu.ValueItem);
            obj.IIdPhanBoVonChiPhiId = ItemsDuToan.FirstOrDefault(n => n.IsChecked).Id;
            obj.INamKeHoach = INamKeHoach.Value;
            obj.SGhiChu = Model.SGhiChu;
            return obj;
        }

        private bool Validate()
        {
            List<string> lstError = new List<string>();
            if (SelectedDonVi == null)
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Đơn vị"));
            if (SelectedDuAn == null)
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Dự án"));
            if (!INamKeHoach.HasValue)
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Năm kế hoạch"));
            if (SelectedChungTu == null)
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Chứng từ"));
            if (ItemsDuToan == null || !ItemsDuToan.Any(n => n.IsChecked))
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Dự toán"));
            if (lstError.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        private void LoadDonVi()
        {
            var datas = _donviService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => _lstDonViInclude.Contains(n.Loai))
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, HiddenValue = n.Id.ToString(), DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi) });
            ItemsDonVi = new ObservableCollection<ComboboxItem>(datas);
            if (!string.IsNullOrEmpty(Model.IIdMaDonViQuanLy))
            {
                SelectedDonVi = ItemsDonVi.FirstOrDefault(n => n.ValueItem == Model.IIdMaDonViQuanLy);
            }
            OnPropertyChanged(nameof(ItemsDonVi));
            OnPropertyChanged(nameof(SelectedDonVi));
        }

        private void LoadDuAn()
        {
            ItemsDuAn = new ObservableCollection<ComboboxItem>();
            if (SelectedDonVi == null || !INamKeHoach.HasValue)
            {
                OnPropertyChanged(nameof(ItemsDuAn));
                return;
            }
            Dictionary<Guid, string> _dicDuAn = new Dictionary<Guid, string>();
            var lstCapPhat = _capphatService.GetDataDeNghiThanhToanIndex(INamKeHoach.Value, _sessionService.Current.Principal);
            if (lstCapPhat != null)
            {
                _lstChungTu = lstCapPhat.ToList();
                foreach (var item in lstCapPhat.Where(n => !n.BThanhToanTheoHopDong && n.iID_MaDonViQuanLy == SelectedDonVi.ValueItem && n.iNamKeHoach == INamKeHoach.Value))
                {
                    if (!_dicDuAn.ContainsKey(item.iID_DuAnId.Value))
                        _dicDuAn.Add(item.iID_DuAnId.Value, item.sTenDuAn);
                }
            }
            ItemsDuAn = new ObservableCollection<ComboboxItem>(_dicDuAn.Select(n => new ComboboxItem() { ValueItem = n.Key.ToString(), DisplayItem = n.Value }));
            if (Model.IIdDuAnId.HasValue)
            {
                SelectedDuAn = ItemsDuAn.FirstOrDefault(n => n.ValueItem == Model.IIdDuAnId.Value.ToString());
            }
            else
            {
                SelectedDuAn = null;
            }
            OnPropertyChanged(nameof(ItemsDuAn));
        }

        private void LoadChungTu()
        {
            ItemsChungTu = new ObservableCollection<ComboboxItem>();
            if (SelectedDuAn == null || _lstChungTu == null) return;
            Dictionary<Guid, string> dicChungTu = new Dictionary<Guid, string>();
            foreach (var item in _lstChungTu)
            {
                if (!dicChungTu.ContainsKey(item.Id))
                    dicChungTu.Add(item.Id, item.sSoDeNghi);
            }
            ItemsChungTu = new ObservableCollection<ComboboxItem>(dicChungTu.Select(n => new ComboboxItem() { ValueItem = n.Key.ToString(), DisplayItem = n.Value }));
            if (Model.IIdDeNghiThanhToanId.HasValue)
            {
                SelectedChungTu = ItemsChungTu.FirstOrDefault(n => n.ValueItem == Model.IIdDeNghiThanhToanId.Value.ToString());
            }
            else
            {
                SelectedChungTu = ItemsChungTu.FirstOrDefault();
            }
            OnPropertyChanged(nameof(ItemsChungTu));
            OnPropertyChanged(nameof(SelectedChungTu));
        }

        private void LoadPhanBoVonChiPhi()
        {
            ItemsDuToan = new ObservableCollection<VdtKhvPhanBoVonChiPhiModel>();
            if (SelectedDonVi == null || !INamKeHoach.HasValue || SelectedDuAn == null)
            {
                OnPropertyChanged(nameof(ItemsDuToan));
                return;
            }
            var data = _phanbovonchiphiService.GetVdtKhvPhanBoVonChiPhiInThanhToanChiPhiDialog(SelectedDonVi.ValueItem, Guid.Parse(SelectedDuAn.ValueItem), INamKeHoach.Value);
            ItemsDuToan = _mapper.Map<ObservableCollection<VdtKhvPhanBoVonChiPhiModel>>(data);
            foreach (var item in ItemsDuToan)
            {
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
            if (Model.IIdPhanBoVonChiPhiId.HasValue)
            {
                foreach (var item in ItemsDuToan)
                {
                    if (item.Id == Model.IIdPhanBoVonChiPhiId.Value)
                        item.IsChecked = true;
                }
            }
            OnPropertyChanged(nameof(ItemsDuToan));
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtKhvPhanBoVonChiPhiModel item = (VdtKhvPhanBoVonChiPhiModel)sender;
            switch (args.PropertyName)
            {
                case nameof(VdtKhvPhanBoVonChiPhiModel.IsChecked):
                    if (ItemsDuToan.Where(n => n.IsChecked).Count() >= 2)
                    {
                        MessageBox.Show("Chỉ được chọn 1 dự án");
                        item.IsChecked = false;
                    }
                    break;
            }
        }
        #endregion
    }
}
