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
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.PheDuyetQuyetToanDAHT;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.PheDuyetQuyetToanDAHT
{
    public class PheDuyetQuyetToanDAHTDialogViewModel : DialogAttachmentViewModelBase<NhQtPheDuyetQuyetToanDAHTModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhQtPheDuyetQuyetToanDAHTService _service;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private SessionInfo _sessionInfo;
        public PheDuyetQuyetToanDAHTDetailViewModel PheDuyetQuyetToanDAHTDetailViewModel { get; set; }

        //public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_CHU_TRUONG_DAU_TU_DIALOG;
        public override string Title => "Phê duyệt quyết toán dự án hoàn thành";
        public override string Name => "Phê duyệt quyết toán dự án hoàn thành";
        public override Type ContentType => typeof(PheDuyetQuyetToanDAHTDialog);
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
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                }
            }
        }
        private ObservableCollection<NhDmTiGiaModel> _itemsTiGia;
        public ObservableCollection<NhDmTiGiaModel> ItemsTiGia
        {
            get => _itemsTiGia;
            set => SetProperty(ref _itemsTiGia, value);
        }

        private NhDmTiGiaModel _selectedTiGia;
        public NhDmTiGiaModel SelectedTiGia
        {
            get => _selectedTiGia;
            set
            {
                if (SetProperty(ref _selectedTiGia, value))
                {
                }
            }
        }

        public PheDuyetQuyetToanDAHTDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            INhQtPheDuyetQuyetToanDAHTService service,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
             INhDmTiGiaService nhDmTiGiaService,
            PheDuyetQuyetToanDAHTDetailViewModel pheDuyetQuyetToanDAHTDetailViewModel
            )
            : base(mapper, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _service = service;
            PheDuyetQuyetToanDAHTDetailViewModel = pheDuyetQuyetToanDAHTDetailViewModel;
        }

        protected override void OnModelPropertyChanged()
        {
            OnPropertyChanged(nameof(IsEditable));
        }

        public override void Init()
        {
            LoadDefault();
            LoadDonVi();
            LoadDmTyGia();
            LoadData();
            LoadAttach();
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindAll().Where(x => x.NamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.IIDMaDonVi);
            ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            if (!Model.Id.Equals(Guid.Empty))
            {
                SelectedDonVi = ItemsDonVi.FirstOrDefault(t => t.IIDMaDonVi.Equals(Model.IIdMaDonVi));
            }
            else if (ItemsDonVi.Count > 0)
            {
                SelectedDonVi = ItemsDonVi.FirstOrDefault();
            }
        }

        private void LoadDmTyGia()
        {
            var tiGia = new ObservableCollection<NhDmTiGia>(_nhDmTiGiaService.FindAll().OrderByDescending(t => t.DNgayTao));
            ItemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(tiGia);
            if (!Model.Id.Equals(Guid.Empty))
            {
                SelectedTiGia = ItemsTiGia.FirstOrDefault(t => t.Id.Equals(Model.IIdTiGiaId));
            }
            else if (ItemsTiGia.Count > 0)
            {
                SelectedTiGia = ItemsTiGia.FirstOrDefault();
            }
        }
        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                IconKind = PackIconKind.PlaylistPlus;
                Description = "Thêm mới phê duyệt quyết toán dự án hoàn thành";
                Model.DNgayPheDuyet = DateTime.Now;
                _selectedDonVi = null;
            }
            else
            {
                NhQtPheDuyetQuyetToanDAHT entity = _service.FindById(Model.Id);
                Model = _mapper.Map<NhQtPheDuyetQuyetToanDAHTModel>(entity);
                if (IsDetail)
                {
                    IconKind = PackIconKind.Details;
                    Description = "Chi tiết phê duyệt quyết toán dự án hoàn thành";
                }
                else
                {
                    IconKind = PackIconKind.NoteEditOutline;
                    Description = "Cập nhật phê duyệt quyết toán dự án hoàn thành";
                }

                _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);

                // Load tỉ giá và ngoại tệ khác
                _selectedTiGia = _itemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
                LoadDonVi();
                LoadDmTyGia();
            }

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedTiGia));
        }
        private void ConverData()
        {
            if (SelectedDonVi != null)
            {
                Model.IIdMaDonVi = SelectedDonVi.IIDMaDonVi;
                Model.IIdDonViId = SelectedDonVi.Id;
            }
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaId = SelectedTiGia.Id;
            }
        }
        public override void OnSave()
        {
            ConverData();
            if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!Validate()) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Mapper object
                if (SelectedDonVi != null)
                {
                    Model.IIdDonViId = SelectedDonVi.Id;
                    Model.IIdMaDonVi = SelectedDonVi.IIDMaDonVi;
                }
                if (SelectedTiGia != null)
                {
                    Model.IIdTiGiaId = SelectedTiGia.Id;
                }

                // Main process
                NhQtPheDuyetQuyetToanDAHT entity;
                if (Model.Id.IsNullOrEmpty())
                {
                    // Thêm mới
                    entity = _mapper.Map<NhQtPheDuyetQuyetToanDAHT>(Model);
                    entity.BIsXoa = false;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionInfo.Principal;
                    _service.Add(entity);
                }
                else
                {
                    // Cập nhật
                    entity = _service.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionInfo.Principal;
                    _service.Update(entity);
                }

                e.Result = entity;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    // Reload data
                    Model = _mapper.Map<NhQtPheDuyetQuyetToanDAHTModel>(e.Result);

                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);

                    SavedAction?.Invoke(Model);
                    DialogHost.CloseDialogCommand.Execute(null, null);

                    //Sau khi ấn Lưu dữ liệu ở popup Thêm mới thì hiển thị màn thêm mới chi tiết như trong thiết kế GP
                    PheDuyetQuyetToanDAHTDetailViewModel.Model = Model;
                    PheDuyetQuyetToanDAHTDetailViewModel.Init();
                    PheDuyetQuyetToanDAHTDetailViewModel.ShowDialog();
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
           
            if (Model.INamBaoCaoTu >= Model.INamBaoCaoDen)
            {
                lstError.Add(string.Format(Resources.MsgCheckNamBaoCaoNhoHon));
            }
            if (SelectedDonVi == null)
            {
                lstError.Add(Resources.MsgCheckDonVi);
            }
            if (SelectedTiGia == null)
            {
                lstError.Add(Resources.MsgCheckTiGiaNgoaiHoi);
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
