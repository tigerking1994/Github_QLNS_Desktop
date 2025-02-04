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
using VTS.QLNS.CTC.App.View.Forex.ForexKhoiTaoCapPhat.ForexDanhSachKhoiTao;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.NhuCauChiQuy;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexKhoiTaoCapPhat.ForexDanhSachKhoiTao
{
    public class ForexDanhSachKhoiTaoDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhKtKhoiTaoCapPhatModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhKtKhoiTaoCapPhatService _service;
        private SessionInfo _sessionInfo;
        public ForexDanhSachKhoiTaoDetailViewModel ForexDanhSachKhoiTaoDetailViewModel { get; set; }

        //public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_CHU_TRUONG_DAU_TU_DIALOG;
        public override string Title => "Khởi tạo cấp phát";
        public override string Name => "Khởi tạo cấp phát";
        public override Type ContentType => typeof(ForexDanhSachKhoiTaoDialog);
        public bool IsDetail { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();

        private ObservableCollection<NhKtNamKhoiTao> _itemsNamKhoiTao;
        public ObservableCollection<NhKtNamKhoiTao> ItemsNamKhoiTao
        {
            get => _itemsNamKhoiTao;
            set => SetProperty(ref _itemsNamKhoiTao, value);
        }

        private NhKtNamKhoiTao _selectedNamKhoiTao;
        public NhKtNamKhoiTao SelectedNamKhoiTao
        {
            get => _selectedNamKhoiTao;
            set => SetProperty(ref _selectedNamKhoiTao, value);
        }

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

        public ForexDanhSachKhoiTaoDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            INhKtKhoiTaoCapPhatService service,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            ForexDanhSachKhoiTaoDetailViewModel forexDanhSachKhoiTaoDetailViewModel)
            
            : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _service = service;
            ForexDanhSachKhoiTaoDetailViewModel = forexDanhSachKhoiTaoDetailViewModel;
        }

        protected override void OnModelPropertyChanged()
        {
            OnPropertyChanged(nameof(IsEditable));
        }

        public override void Init()
        {
            LoadNamKhoiTao();
            LoadDefault();
            LoadDonVi();
            LoadTiGia();
            LoadData();
            LoadAttach();
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
        }

        private void LoadNamKhoiTao()
        {
            ItemsNamKhoiTao = new ObservableCollection<NhKtNamKhoiTao>();
            int year = DateTime.Now.Year;
            for (int i = 0; i <= 10; i++)
            {
                ItemsNamKhoiTao.Add(new NhKtNamKhoiTao() { INam = year, SNam = year.ToString() });
                year--;
            }
            OnPropertyChanged(nameof(ItemsNamKhoiTao));
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindInternalByNamLamViec(_sessionInfo.YearOfWork);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                IconKind = PackIconKind.PlaylistPlus;
                Description = "Thêm mới khởi tạo cấp phát";
                Model.DNgayKhoiTao = DateTime.Now;
            }
            else
            {
                NhKtKhoiTaoCapPhat entity = _service.FindById(Model.Id);
                Model = _mapper.Map<NhKtKhoiTaoCapPhatModel>(entity);
                if (IsDetail)
                {
                    IconKind = PackIconKind.Details;
                    Description = "Chi tiết khởi tạo cấp phát";
                }
                else
                {
                    IconKind = PackIconKind.NoteEditOutline;
                    Description = "Cập nhật khởi tạo cấp phát";
                }

                SelectedNamKhoiTao = ItemsNamKhoiTao.FirstOrDefault(x => x.INam == Model.INamKhoiTao);
                SelectedDonVi = ItemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonVi));
                SelectedTiGia = ItemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaID);
            }

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
        }

        public override void OnSave(object obj)
        {
            // Mapper object
            if (SelectedDonVi != null)
            {
                Model.IIdDonViID = SelectedDonVi.Id;
                Model.IIdMaDonVi = SelectedDonVi.IIDMaDonVi;
            }
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaID = SelectedTiGia.Id;
            }
            if (SelectedNamKhoiTao != null)
            {
                Model.INamKhoiTao = SelectedNamKhoiTao.INam;
            }

            //Validate
            if (!ValidateViewModelHelper.Validate(Model)) return;
            //if (!Validate()) return;
            if (!CheckUnique()) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                NhKtKhoiTaoCapPhat entity;
                if (Model.Id.IsNullOrEmpty())
                {
                    // Thêm mới
                    entity = _mapper.Map<NhKtKhoiTaoCapPhat>(Model);
                    entity.BIsKhoa = false;
                    entity.BIsXoa = false;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionInfo.Principal;
                    entity.IsModified = false;
                }
                else
                {
                    // Cập nhật
                    entity = _service.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionInfo.Principal;
                    entity.IsModified = true;
                }

                e.Result = entity;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    //Load tên đơn vị sang màn chi tiết
                    Model = _mapper.Map<NhKtKhoiTaoCapPhatModel>(e.Result);
                    var dataDonVi = _nsDonViService.FindInternalByNamLamViec(_sessionInfo.YearOfWork);
                    _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(dataDonVi);
                    foreach (var item in _itemsDonVi)
                    {
                        if (item.Id == Model.IIdDonViID)
                        {
                            Model.STenDonVi = item.TenDonVi;
                            break;
                        }
                    }

                    SavedAction?.Invoke(Model);
                    DialogHost.CloseDialogCommand.Execute(obj, null);

                    // Sau khi ấn Lưu dữ liệu ở popup Thêm mới thì hiển thị màn thêm mới chi tiết như trong thiết kế GP
                    ForexDanhSachKhoiTaoDetailViewModel.Model = Model;
                    ForexDanhSachKhoiTaoDetailViewModel.Init();
                    ForexDanhSachKhoiTaoDetailViewModel.SavedAction = obj => this.OnClose(obj);
                    ForexDanhSachKhoiTaoDetailViewModel.ShowDialog();
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
            DialogHost.CloseDialogCommand.Execute(obj, null);
            SavedAction?.Invoke(Model);
        }

        private bool CheckUnique()
        {
            //Check trùng đơn vị + năm
            var tempList = _service.FindAll();
            bool myCheck = true;
            foreach (var item in tempList)
            {
                if (Model.Id.IsNullOrEmpty())
                {
                    if (item.IIdDonViID == SelectedDonVi.Id && item.INamKhoiTao == SelectedNamKhoiTao.INam)
                    {
                        List<string> lstError = new List<string>();
                        lstError.Add(Resources.MsgCheckUniqueYearAndUnit);
                        myCheck = false;
                        MessageBoxHelper.Warning(string.Join("\n", lstError));
                        break;
                    }
                }
                else
                {
                    if (item.IIdDonViID == SelectedDonVi.Id && item.INamKhoiTao == SelectedNamKhoiTao.INam && item.Id != Model.Id)
                    {
                        List<string> lstError = new List<string>();
                        lstError.Add(Resources.MsgCheckUniqueYearAndUnit);
                        myCheck = false;
                        MessageBoxHelper.Warning(string.Join("\n", lstError));
                        break;
                    }
                }
            }
            return myCheck;
        }
    }
}
