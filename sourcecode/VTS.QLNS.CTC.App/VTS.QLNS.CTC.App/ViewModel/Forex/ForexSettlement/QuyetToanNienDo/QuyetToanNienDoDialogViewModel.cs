using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.QuyetToanNienDo;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo
{
    public class QuyetToanNienDoDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhQtQuyetToanNienDoModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhQtQuyetToanNienDoService _service;
        private SessionInfo _sessionInfo;
        public QuyetToanNienDoDetailViewModel QuyetToanNienDoDetailViewModel { get; set; }

        //public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_CHU_TRUONG_DAU_TU_DIALOG;
        public override string Title => "Đề nghị quyết toán niên độ";
        public override string Name => "Đề nghị quyết toán niên độ";
        public override Type ContentType => typeof(QuyetToanNienDoDialog);
        public bool IsDetail { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private ObservableCollection<NguonNganSachModel> _itemsNguonVon;
        public ObservableCollection<NguonNganSachModel> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private NguonNganSachModel _selectedNguonVon;
        public NguonNganSachModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set => SetProperty(ref _selectedNguonVon, value);
        }

        private ObservableCollection<NhDmLoaiThanhToanModel> _itemsLoaiThanhToan;
        public ObservableCollection<NhDmLoaiThanhToanModel> ItemsLoaiThanhToan
        {
            get => _itemsLoaiThanhToan;
            set => SetProperty(ref _itemsLoaiThanhToan, value);
        }

        private ObservableCollection<NhDmLoaiThanhToanModel> _itemsLoaiQuyetToan;
        public ObservableCollection<NhDmLoaiThanhToanModel> ItemsLoaiQuyetToan
        {
            get => _itemsLoaiQuyetToan;
            set => SetProperty(ref _itemsLoaiQuyetToan, value);
        }

        private ObservableCollection<NhDmCoQuanThanhToanModel> _itemsCoQuanThanhToan;
        public ObservableCollection<NhDmCoQuanThanhToanModel> ItemsCoQuanThanhToan
        {
            get => _itemsCoQuanThanhToan;
            set => SetProperty(ref _itemsCoQuanThanhToan, value);
        }

        public QuyetToanNienDoDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            INhQtQuyetToanNienDoService service,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            QuyetToanNienDoDetailViewModel quyetToanNienDoDetailViewModel
            )
            : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _service = service;
            QuyetToanNienDoDetailViewModel = quyetToanNienDoDetailViewModel;
        }

        protected override void OnModelPropertyChanged()
        {
            OnPropertyChanged(nameof(IsEditable));
        }

        public override void Init()
        {
            LoadDefault();
            LoadDonVi();
            LoadNguonVon();
            LoadLoaiThanhToan();
            LoadLoaiQuyetToan();
            LoadCoQuanThanhToan();
            LoadTiGia();
            LoadData();
            LoadAttach();
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindInternalByNamLamViec(_sessionInfo.YearOfWork);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadNguonVon()
        {
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonVon = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private void LoadLoaiThanhToan()
        {
            _itemsLoaiThanhToan = new ObservableCollection<NhDmLoaiThanhToanModel>();
            _itemsLoaiThanhToan.Add(new NhDmLoaiThanhToanModel() { Id = (int)NhLoaiDeNghi.Type.CAP_KINH_PHI, STen = (NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.CAP_KINH_PHI)) });
            _itemsLoaiThanhToan.Add(new NhDmLoaiThanhToanModel() { Id = (int)NhLoaiDeNghi.Type.TAM_UNG_KINH_PHI, STen = (NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.TAM_UNG_KINH_PHI)) });
            _itemsLoaiThanhToan.Add(new NhDmLoaiThanhToanModel() { Id = (int)NhLoaiDeNghi.Type.THANH_TOAN, STen = (NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.THANH_TOAN)) });
            _itemsLoaiThanhToan.Add(new NhDmLoaiThanhToanModel() { Id = (int)NhLoaiDeNghi.Type.TAM_UNG_THEO_CHE_DO, STen = (NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.TAM_UNG_THEO_CHE_DO)) });
            OnPropertyChanged(nameof(ItemsLoaiThanhToan));
        }
        private void LoadLoaiQuyetToan()
        {
            _itemsLoaiQuyetToan = new ObservableCollection<NhDmLoaiThanhToanModel>();
            _itemsLoaiQuyetToan.Add(new NhDmLoaiThanhToanModel() { Id = 1, STen = "Quyết toán theo dự án" });
            _itemsLoaiQuyetToan.Add(new NhDmLoaiThanhToanModel() { Id = 2, STen = "Quyết toán theo hợp đồng" });
            OnPropertyChanged(nameof(ItemsLoaiQuyetToan));
        }

        private void LoadCoQuanThanhToan()
        {
            _itemsCoQuanThanhToan = new ObservableCollection<NhDmCoQuanThanhToanModel>();
            _itemsCoQuanThanhToan.Add(new NhDmCoQuanThanhToanModel() { Id = 1, STen = "Kho bạc" });
            _itemsCoQuanThanhToan.Add(new NhDmCoQuanThanhToanModel() { Id = 2, STen = "Cơ quan tài chính Bộ quốc phòng" });
            OnPropertyChanged(nameof(ItemsCoQuanThanhToan));
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                IconKind = PackIconKind.PlaylistPlus;
                Description = "Thêm mới đề nghị quyết toán niên độ";
                Model.DNgayDeNghi = DateTime.Now;
            }
            else
            {
                NhQtQuyetToanNienDo entity = _service.FindById(Model.Id);
                Model = _mapper.Map<NhQtQuyetToanNienDoModel>(entity);
                if (IsDetail)
                {
                    IconKind = PackIconKind.Details;
                    Description = "Chi tiết đề nghị quyết toán niên độ";
                }
                else
                {
                    IconKind = PackIconKind.NoteEditOutline;
                    Description = "Cập nhật đề nghị quyết toán niên độ";
                }

                _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonVi));

                // Load tỉ giá và ngoại tệ khác
                SelectedTiGia = ItemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
                LoadTiGiaChiTiet();
                SelectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            }

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
        }

        public override void OnSave()
        {
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaId = SelectedTiGia.Id;
            }
            if (SelectedTiGiaChiTiet != null)
            {
                Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            }
            if (!Validate()) return;
            if (!ValidateViewModelHelper.Validate(Model)) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Mapper object
                //if (SelectedDonVi != null)
                //{
                //    Model.IIdDonViId = SelectedDonVi.Id;
                //    Model.IIdMaDonVi = SelectedDonVi.IIDMaDonVi;
                //}
                if (SelectedTiGia != null)
                {
                    Model.IIdTiGiaId = SelectedTiGia.Id;
                }
                if (SelectedTiGiaChiTiet != null)
                {
                    Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
                }

                // Main process
                NhQtQuyetToanNienDo entity;
                if (Model.Id.IsNullOrEmpty())
                {
                    // Thêm mới
                    entity = _mapper.Map<NhQtQuyetToanNienDo>(Model);
                    entity.BIsActive = true;
                    entity.BIsGoc = true;
                    entity.BIsKhoa = false;
                    entity.BIsXoa = false;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionInfo.Principal;

                    // Get id đơn vị
                    entity.IIdDonViId = ItemsDonVi.FirstOrDefault(x => x.IIDMaDonVi == Model.IIdMaDonVi && x.NamLamViec == _sessionService.Current.YearOfWork).Id;

                    _service.Add(entity);
                }
                else
                {
                    // Cập nhật
                    entity = _service.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionInfo.Principal;

                    // Get id đơn vị
                    entity.IIdDonViId = ItemsDonVi.FirstOrDefault(x => x.IIDMaDonVi == Model.IIdMaDonVi && x.NamLamViec == _sessionService.Current.YearOfWork).Id;

                    _service.Update(entity);
                }

                e.Result = entity;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    // Reload data
                    Model = _mapper.Map<NhQtQuyetToanNienDoModel>(e.Result);
                    if (SelectedDonVi != null)
                    {
                        Model.STenDonVi = SelectedDonVi.TenDonVi;
                    }
                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);

                    SavedAction?.Invoke(Model);
                    DialogHost.CloseDialogCommand.Execute(null, null);

                    // Sau khi ấn Lưu dữ liệu ở popup Thêm mới thì hiển thị màn thêm mới chi tiết như trong thiết kế GP
                    QuyetToanNienDoDetailViewModel.Model = Model;
                    QuyetToanNienDoDetailViewModel.Init();
                    QuyetToanNienDoDetailViewModel.ShowDialog();
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        public override void OnClosing()
        {
            // Clear items
            if (!ItemsNguonVon.IsEmpty()) ItemsNguonVon.Clear();
            if (!ItemsTiGiaChiTiet.IsEmpty()) ItemsTiGiaChiTiet.Clear();
            if (!ItemsTiGia.IsEmpty()) ItemsTiGia.Clear();
            if (!ItemsDonVi.IsEmpty()) ItemsDonVi.Clear();
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }

        private bool Validate()
        {
            List<string> lstError = new List<string>();
            //if (SelectedDonVi == null)
            //{
            //    lstError.Add(Resources.MsgCheckDonVi);
            //}
            Model.IIdDonViId = ItemsDonVi.FirstOrDefault(x => x.IIDMaDonVi == Model.IIdMaDonVi && x.NamLamViec == _sessionService.Current.YearOfWork).Id;
            if (Model.Id != null && _service.CheckDuplicateQTND(Model.IIdDonViId, Model.INamKeHoach, Model.Id))
            {
                lstError.Add(string.Format(Resources.MsgDuplicateQTND, Model.INamKeHoach));
            }
            if (SelectedTiGia == null)
            {
                lstError.Add(Resources.MsgCheckTiGiaNgoaiHoi);
            }
            if (SelectedTiGiaChiTiet == null)
            {
                lstError.Add(Resources.MsgCheckMaNgoaiTeNgoaiHoi);
            }
            if (Model.Id != null && _service.CheckDuplicateSoQD(Model.SSoDeNghi, Model.Id))
            {
                lstError.Add(Resources.MsgTrungSoQD);
            }
            if (lstError.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", lstError));
                return false;
            }            
            return true;
        }
    }
}
