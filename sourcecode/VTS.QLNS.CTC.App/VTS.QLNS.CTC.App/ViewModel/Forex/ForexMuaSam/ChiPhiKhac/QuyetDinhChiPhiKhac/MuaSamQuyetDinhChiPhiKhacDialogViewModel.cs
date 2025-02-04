using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTThietKeKyThuatTongDuToan;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.ChiPhiKhac.QuyetDinhChiPhiKhac;
using System.ComponentModel;
using System.Windows;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChiPhiKhac.QuyetDinhChiPhiKhac
{
    public class MuaSamQuyetDinhChiPhiKhacDialogViewModel : DialogViewModelBase<NhDaQuyetDinhKhacModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IDmChuDauTuService _chuDauTuService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhDmLoaiCongTrinhService _nhDmLoaiCongTrinhService;
        private readonly INhDmChiPhiService _nhDmChiPhiService;
        private readonly INhDaQdDauTuNguonVonService _nhDaQdDauTuNguonVonService;
        private readonly INhDaQdDauTuChiPhiService _nhDaQdDauTuChiPhiService;
        private readonly INhDaQdDauTuHangMucService _nhDaQdDauTuHangMucService;
        private readonly INhDaQdDauTuService _nhDaQdDauTuService;
        private readonly INhDaDuToanNguonVonService _nhDaDuToanNguonVonService;
        private readonly INhDaDuToanChiPhiService _nhDaDuToanChiPhiService;
        private readonly INhDaDuToanHangMucService _nhDaDuToanHangMucService;
        private readonly INhDaDuToanService _service;
        private SessionInfo _sessionInfo;
        private List<NhDaQdDauTuHangMucModel> _itemsQdDauTuHangMuc;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhDaQuyetDinhKhacService _nhDaQuyetDinhKhacService;
        private readonly INhDaQuyetDinhKhacChiPhiService _nhDaQuyetDinhKhacChiPhiService;

        //public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_TKTC_VA_TONG_DU_TOAN_DIALOG;
        public override string Name => "Quyết định chi phí khác";
        public override string Title { get; set; }
        public override string Description { get; set; }
        public override System.Type ContentType => typeof(MuaSamQuyetDinhChiPhiKhacDialog);
        public bool IsDetail { get; set; }
        public int ILoai { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();
        public bool IsQuyetDinh => !IsDetail;
        public bool IsShowDuAn { get; set; }
        public bool IsDieuChinh { get; set; }
        public bool IsEnableQuyetDinhDauTuPheDuyet => !IsShowDuAn;
        public bool IsAddSpend { get; set; }
        public bool IsAddSpendRowChild { get; set; }
        private double? _tongGiaTriPheDuyetDuAn;
        public int IThuocMenu { get; set; }

        public double? TongGiaTriPheDuyetDuAn
        {
            get => _tongGiaTriPheDuyetDuAn;
            set => SetProperty(ref _tongGiaTriPheDuyetDuAn, value);
        }

        private double? _fGiaTriVnd;
        public double? FGiaTriVnd
        {
            get => _fGiaTriVnd;
            set => SetProperty(ref _fGiaTriVnd, value);
        }

        private double? _fGiaTriUsd;
        public double? FGiaTriUsd
        {
            get => _fGiaTriUsd;
            set => SetProperty(ref _fGiaTriUsd, value);
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
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                    LoadKeHoachTongThe();
                    LoadNhiemVuChi();
                }
            }
        }

        private string _titlePheDuyet;
        public string TitlePheDuyet
        {
            get => _titlePheDuyet;
            set => SetProperty(ref _titlePheDuyet, value);
        }

        private ObservableCollection<NhKhTongTheModel> _itemsNhKhTongThe;
        public ObservableCollection<NhKhTongTheModel> ItemsNhKhTongThe
        {
            get => _itemsNhKhTongThe;
            set => SetProperty(ref _itemsNhKhTongThe, value);
        }
        private NhKhTongTheModel _selectedNhKhTongThe;
        public NhKhTongTheModel SelectedNhKhTongThe
        {
            get => _selectedNhKhTongThe;
            set
            {
                SetProperty(ref _selectedNhKhTongThe, value);
                if (value != null)
                {
                    LoadNhiemVuChi();
                }
            }
        }

        private Boolean _isReadOnlyChiPhi;
        public Boolean isReadOnlyChiPhi
        {
            get => _isReadOnlyChiPhi;
            set => SetProperty(ref _isReadOnlyChiPhi, value);
        }

        private ObservableCollection<NhKhTongTheNhiemVuChiModel> _itemsKhTongTheNhiemVuChi;
        public ObservableCollection<NhKhTongTheNhiemVuChiModel> ItemsKhTongTheNhiemVuChi
        {
            get => _itemsKhTongTheNhiemVuChi;
            set => SetProperty(ref _itemsKhTongTheNhiemVuChi, value);
        }

        private NhKhTongTheNhiemVuChiModel _selectedKhTongTheNhiemVuChi;
        public NhKhTongTheNhiemVuChiModel SelectedKhTongTheNhiemVuChi
        {
            get => _selectedKhTongTheNhiemVuChi;
            set => SetProperty(ref _selectedKhTongTheNhiemVuChi, value);
        }

        private ObservableCollection<NhDmChiPhiModel> _itemsDMChiPhi;
        public ObservableCollection<NhDmChiPhiModel> ItemsDMChiPhi
        {
            get => _itemsDMChiPhi;
            set => SetProperty(ref _itemsDMChiPhi, value);
        }
        private ObservableCollection<NhDaQuyetDinhKhacChiPhiModel> _itemsQuyetDinhKhacChiPhi = new ObservableCollection<NhDaQuyetDinhKhacChiPhiModel>();
        public ObservableCollection<NhDaQuyetDinhKhacChiPhiModel> ItemsQuyetDinhKhacChiPhi
        {
            get => _itemsQuyetDinhKhacChiPhi;
            set => SetProperty(ref _itemsQuyetDinhKhacChiPhi, value);
        }

        private NhDaQuyetDinhKhacChiPhiModel _selectedQuyetDinhKhacChiPhi;
        public NhDaQuyetDinhKhacChiPhiModel SelectedQuyetDinhKhacChiPhi
        {
            get => _selectedQuyetDinhKhacChiPhi;
            set => SetProperty(ref _selectedQuyetDinhKhacChiPhi, value);
        }

        private double? _fSumGiaTriVnd;
        public double? FSumGiaTriVnd
        {
            get => _fSumGiaTriVnd;
            set => SetProperty(ref _fSumGiaTriVnd, value);
        }

        private double? _fSumGiaTriUsd;
        public double? FSumGiaTriUsd
        {
            get => _fSumGiaTriUsd;
            set => SetProperty(ref _fSumGiaTriUsd, value);
        }

        public MSNTThietKeKyThuatTongDuToanItemDialogViewModel DuToanHangMucDialogViewModel { get; }

        public RelayCommand AddQuyetDinhKhacChiPhiCommand { get; }
        public RelayCommand DeleteQuyetDinhKhacChiPhiCommand { get; }

        public MuaSamQuyetDinhChiPhiKhacDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IDmChuDauTuService chuDauTuService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDmLoaiCongTrinhService nhDmLoaiCongTrinhService,
            INhDmChiPhiService nhDmChiPhiService,
            INhDaDuAnService nhDaDuAnService,
            INhDaQdDauTuHangMucService nhDaQdDauTuHangMucService,
            INhDaQdDauTuNguonVonService nhDaQdDauTuNguonVonService,
            INhDaQdDauTuChiPhiService nhDaQdDauTuChiPhiService,
            INhDaQdDauTuService nhDaQdDauTuService,
            INhDaDuToanNguonVonService nhDaDuToanNguonVonService,
            INhDaDuToanChiPhiService nhDaDuToanChiPhiService,
            INhDaDuToanHangMucService nhDaDuToanHangMucService,
            INhDaDuToanService service,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            MSNTThietKeKyThuatTongDuToanItemDialogViewModel duToanHangMucDialogViewModel,
            INhKhTongTheService nhKhTongTheService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INhDaQuyetDinhKhacService nhDaQuyetDinhKhacService,
            INhDaQuyetDinhKhacChiPhiService nhDaQuyetDinhKhacChiPhiService) 
        {
            _isReadOnlyChiPhi = false;
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _chuDauTuService = chuDauTuService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDmLoaiCongTrinhService = nhDmLoaiCongTrinhService;
            _nhDmChiPhiService = nhDmChiPhiService;
            _nhDaDuAnService = nhDaDuAnService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            _nhDaQdDauTuNguonVonService = nhDaQdDauTuNguonVonService;
            _nhDaQdDauTuChiPhiService = nhDaQdDauTuChiPhiService;
            _nhDaQdDauTuHangMucService = nhDaQdDauTuHangMucService;
            _nhDaQdDauTuService = nhDaQdDauTuService;
            _nhDaDuToanNguonVonService = nhDaDuToanNguonVonService;
            _nhDaDuToanChiPhiService = nhDaDuToanChiPhiService;
            _nhDaDuToanHangMucService = nhDaDuToanHangMucService;
            _service = service;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nhDaQuyetDinhKhacService = nhDaQuyetDinhKhacService;
            _nhDaQuyetDinhKhacChiPhiService = nhDaQuyetDinhKhacChiPhiService;
            DuToanHangMucDialogViewModel = duToanHangMucDialogViewModel;

            AddQuyetDinhKhacChiPhiCommand = new RelayCommand(obj => OnAddQuyetDinhKhacChiPhi(obj));
            DeleteQuyetDinhKhacChiPhiCommand = new RelayCommand(obj => OnDeleteQuyetDinhKhacChiPhi());
        }

        public override void Init()
        {
            //LoadDefault();
            LoadDanhMucChiPhi();
            LoadDonVi();
            LoadKeHoachTongThe();
            LoadData();
            LoadNhiemVuChi();

            //SwitchTitleAndShowDuAn();
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                IconKind = PackIconKind.PlaylistPlus;
                Title = "Thêm mới quyết định khác chi phí";
                Description = "Thêm mới quyết định khác chi phí";
                Model.DNgayQuyetDinh = DateTime.Now;
            }
            else
            {
                NhDaQuyetDinhKhac entity = _nhDaQuyetDinhKhacService.FindById(Model.Id);
                Model = _mapper.Map<NhDaQuyetDinhKhacModel>(entity);
                if (IsDetail)
                {
                    IconKind = PackIconKind.Details;
                    Title = "Chi tiết quyết định khác chi phí";
                    Description = "Chi tiết quyết định khác chi phí";
                }
                else if (IsDieuChinh)
                {
                    IconKind = PackIconKind.Adjust;
                    Title = "Chi tiết quyết định khác chi phí";
                    Description = "Điều chỉnh quyết định khác chi phí";
                    //Model.SSoQuyetDinh = string.Empty;
                    //Model.DNgayQuyetDinh = DateTime.Now;
                }
                else
                {
                    IconKind = PackIconKind.NoteEditOutline;
                    Title = "Chi tiết quyết định khác chi phí";
                    Description = "Cập nhật quyết định khác chi phí";
                }

                _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonViQuanLy));

                if (Model.IIdKHTTNhiemVuChiId != null)
                {
                    var data = _nhKhTongTheNhiemVuChiService.FindAll();
                    Guid IIdKhTongTheId = data.Any(x => x.Id == Model.IIdKHTTNhiemVuChiId) ? data.FirstOrDefault(x => x.Id == Model.IIdKHTTNhiemVuChiId).IIdKhTongTheId : Guid.Empty;
                    LoadKeHoachTongThe();
                    _selectedNhKhTongThe = _itemsNhKhTongThe.FirstOrDefault(x => x.Id == IIdKhTongTheId);
                    var listNhiemVuChi = _nhKhTongTheNhiemVuChiService.FindByIdKhTongTheAndMaDonVi(IIdKhTongTheId, _selectedDonVi.IIDMaDonVi);
                    _itemsKhTongTheNhiemVuChi = _mapper.Map<ObservableCollection<NhKhTongTheNhiemVuChiModel>>(listNhiemVuChi);
                    _selectedKhTongTheNhiemVuChi = _itemsKhTongTheNhiemVuChi.FirstOrDefault(x => x.Id == Model.IIdKHTTNhiemVuChiId);
                }
            }

            LoadDuToanChiPhi();
            CalculateChiPhi();
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedNhKhTongThe));
            OnPropertyChanged(nameof(SelectedKhTongTheNhiemVuChi));
        }


        private void LoadDonVi()
        {
            var data = _nsDonViService.FindInternalByNamLamViec(_sessionService.Current.YearOfWork);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            if (!Model.IIdDonViQuanLyId.IsNullOrEmpty())
            {
                SelectedDonVi = _itemsDonVi.FirstOrDefault(x => x.Id.Equals(Model.IIdDonViQuanLyId));
            }
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadDanhMucChiPhi()
        {
            IEnumerable<NhDmChiPhi> data = _nhDmChiPhiService.FindAll();
            _itemsDMChiPhi = _mapper.Map<ObservableCollection<NhDmChiPhiModel>>(data);

            OnPropertyChanged(nameof(ItemsDMChiPhi));
        }

        private void LoadKeHoachTongThe()
        {
            if (!_itemsKhTongTheNhiemVuChi.IsEmpty()) _itemsKhTongTheNhiemVuChi.Clear();
            List<NhKhTongThe> data = new List<NhKhTongThe>();
            if (SelectedDonVi != null)
            {
                data = _nhKhTongTheService.FindByDonViId(SelectedDonVi.Id).ToList();
            }

            _itemsNhKhTongThe = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data);
            _itemsNhKhTongThe.ForAll(s =>
            {
                if (s.ILoai == Loai_KHTT.GIAIDOAN)
                {
                    s.SSoKeHoachBqp = $"KHTT {s.IGiaiDoanTu_BQP} - {s.IGiaiDoanDen_BQP} - Số KH: {s.SSoKeHoachBqp}";
                }
                else
                {
                    s.SSoKeHoachBqp = $"KHTT {s.INamKeHoach} - Số KH: {s.SSoKeHoachBqp}";
                }
            });
            if (!Model.IIdKHTongTheID.IsNullOrEmpty())
            {
                SelectedNhKhTongThe = _itemsNhKhTongThe.FirstOrDefault(x => x.Id.Equals(Model.IIdKHTongTheID));
            }
            OnPropertyChanged(nameof(ItemsNhKhTongThe));
        }

        private void LoadNhiemVuChi()
        {
            if (SelectedDonVi != null && SelectedNhKhTongThe != null)
            {
                var data = _nhKhTongTheNhiemVuChiService.FindByIdKhTongTheAndMaDonViID(SelectedNhKhTongThe.Id, SelectedDonVi.Id);
                _itemsKhTongTheNhiemVuChi = _mapper.Map<ObservableCollection<NhKhTongTheNhiemVuChiModel>>(data);
                if (!Model.IIdKHTTNhiemVuChiId.IsNullOrEmpty())
                {
                    SelectedKhTongTheNhiemVuChi = _itemsKhTongTheNhiemVuChi.FirstOrDefault(x => x.Id.Equals(Model.IIdKHTTNhiemVuChiId));
                }
            }
            OnPropertyChanged(nameof(ItemsKhTongTheNhiemVuChi));
        }
        public override void OnSave(object obj)
        {

            if (SelectedDonVi != null)
            {
                Model.IIdDonViQuanLyId = SelectedDonVi.Id;
                Model.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
            }

            if (SelectedKhTongTheNhiemVuChi != null)
            {
                Model.IIdKHTTNhiemVuChiId = SelectedKhTongTheNhiemVuChi.Id;
            }
            Model.ListDataDetail = _itemsQuyetDinhKhacChiPhi.ToList();
            if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!ValiDateData()) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                NhDaQuyetDinhKhac entity;
                var lstDataAdd = _mapper.Map<List<NhDaQuyetDinhKhacChiPhi>>(Model.ListDataDetail.Where(x => x.IsAdded));
                var lstDataUpdate = _mapper.Map<List<NhDaQuyetDinhKhacChiPhi>>(Model.ListDataDetail.Where(x => x.IsEnableEdit && !x.IsDeleted));
                var lstDatDelete = _mapper.Map<List<NhDaQuyetDinhKhacChiPhi>>(Model.ListDataDetail.Where(x => x.IsDeleted));
                if (Model.Id.IsNullOrEmpty())
                {
                    // Add NhDaDuToan
                    entity = _mapper.Map<NhDaQuyetDinhKhac>(Model);
                    entity.BIsActive = true;
                    entity.Id = Guid.NewGuid();
                    entity.BIsGoc = true;
                    entity.BIsXoa = false;
                    entity.ILanDieuChinh = 0;
                    entity.BIsKhoa = false;
                    entity.FGiaTriUsd = FGiaTriUsd;
                    entity.FGiaTriVnd = FGiaTriVnd;
                    entity.IThuocMenu = IThuocMenu;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    _nhDaQuyetDinhKhacService.Add(entity);
                    if (lstDataAdd.Any())
                    {
                        lstDataAdd.ForAll(f =>
                        {
                            f.IIdQuyetDinhKhacId = entity.Id;
                        });

                        _nhDaQuyetDinhKhacChiPhiService.AddRange(lstDataAdd);

                    }
                }
                else if (IsDieuChinh)
                {
                    var listAddAdjust = new List<NhDaQuyetDinhKhacChiPhi>();
                    // Điều chỉnh
                    entity = _mapper.Map<NhDaQuyetDinhKhac>(Model);
                    entity.BIsActive = true;
                    entity.Id = Guid.NewGuid();
                    entity.BIsGoc = true;
                    entity.BIsXoa = false;
                    entity.ILanDieuChinh++;
                    entity.BIsKhoa = false;
                    entity.FGiaTriUsd = FGiaTriUsd;
                    entity.FGiaTriVnd = FGiaTriVnd;
                    entity.IThuocMenu = IThuocMenu;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    _nhDaQuyetDinhKhacService.Add(entity);
                    if (lstDataAdd.Any())
                    {
                        listAddAdjust.AddRange(lstDataAdd);
                    }
                    if (lstDataUpdate.Any())
                    {
                        listAddAdjust.AddRange(lstDataUpdate);
                    }
                    if (listAddAdjust.Any())
                    {
                        listAddAdjust.ForAll(f =>
                        {
                            f.IIdQuyetDinhKhacId = entity.Id;
                        });
                        _nhDaQuyetDinhKhacChiPhiService.AddRange(listAddAdjust);
                    }
                }
                else
                {
                    // Update
                    entity = _nhDaQuyetDinhKhacService.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.DNgaySua = DateTime.Now;
                    entity.FGiaTriUsd = FGiaTriUsd;
                    entity.FGiaTriVnd = FGiaTriVnd;
                    entity.SNguoiSua = _sessionService.Current.Principal;

                    _nhDaQuyetDinhKhacService.Update(entity);
                    if (lstDataAdd.Any())
                    {
                        lstDataAdd.Select(x => { x.IIdQuyetDinhKhacId = entity.Id; return x; }).ToList();
                        _nhDaQuyetDinhKhacChiPhiService.AddRange(lstDataAdd);
                    }
                    if (lstDataUpdate.Any())
                    {
                        _nhDaQuyetDinhKhacChiPhiService.UpdateRange(lstDataUpdate);
                    }
                    if (lstDatDelete.Any())
                    {
                        _nhDaQuyetDinhKhacChiPhiService.RemoveRange(lstDatDelete);
                    }
                }

                e.Result = entity;
            }, (s, e) =>
            {
                IsLoading = false;

                if (e.Error == null)
                {
                    // Reload data
                    Model = _mapper.Map<NhDaQuyetDinhKhacModel>(e.Result);
                    IsDieuChinh = false;
                    SavedAction?.Invoke(Model);
                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    OnClose(obj);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }

        private bool ValiDateData()
        {
            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                MessageBoxHelper.Error(Resources.MsgCheckSoQD);
                return false;
            }
            else if (_nhDaQuyetDinhKhacService.CheckDuplicateSoQD(Model.SSoQuyetDinh, Model.Id))
            {
                MessageBoxHelper.Error(Resources.MsgTrungSoQD);
                return false;
            }
            if (Model.DNgayQuyetDinh == null)
            {
                MessageBoxHelper.Error(Resources.MsgCheckNgayPheDuyet);
                return false;
            }

            if (SelectedDonVi == null)
            {
                MessageBoxHelper.Error(Resources.MsgCheckDonVi);
                return false;
            }         
            if (SelectedKhTongTheNhiemVuChi == null)
            {
                MessageBoxHelper.Error(Resources.MsgNhiemVuChi);
                return false;
            }         
            if (SelectedNhKhTongThe == null)
            {
                MessageBoxHelper.Error(Resources.MsgCheckKeHoachTongThe);
                return false;
            }

            return true;
        }

        protected void OnAddQuyetDinhKhacChiPhi(object obj)
        {
            if (_itemsQuyetDinhKhacChiPhi == null) _itemsQuyetDinhKhacChiPhi = new ObservableCollection<NhDaQuyetDinhKhacChiPhiModel>();
            IsAddSpendRowChild = true;
            NhDaQuyetDinhKhacChiPhiModel sourceItem = SelectedQuyetDinhKhacChiPhi;
            NhDaQuyetDinhKhacChiPhiModel targetItem = new NhDaQuyetDinhKhacChiPhiModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!_itemsQuyetDinhKhacChiPhi.IsEmpty())
            {
                currentRow = 0;
                if (sourceItem != null)
                {
                    currentRow = _itemsQuyetDinhKhacChiPhi.IndexOf(sourceItem);
                    if (isParent)
                    {
                        currentRow += CountTreeChildItems(sourceItem);
                    }
                }
            }

            if (sourceItem != null)
            {
                targetItem.IIdParentId = isParent ? sourceItem.IIdParentId : sourceItem.Id;
                if (isParent)
                {
                    targetItem.IsHasChildren = sourceItem.IsHasChildren;
                }
                else
                {
                    targetItem.IsHasChildren = true;
                    sourceItem.IsHasChildren = false;
                }
            }
            else
            {
                targetItem.IsHasChildren = true;
            }
            targetItem.Id = Guid.NewGuid();
            targetItem.ItemsLoaiNoiDungChi = ItemsDMChiPhi;
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            _itemsQuyetDinhKhacChiPhi.Insert(currentRow + 1, targetItem);
            if (_itemsQuyetDinhKhacChiPhi.Count > 0)
            {
                if (!IsAddSpendRowChild)
                {
                    IsAddSpendRowChild = true;
                    OnPropertyChanged(nameof(IsAddSpendRowChild));
                }
            }
            else
            {
                if (IsAddSpendRowChild)
                {
                    IsAddSpendRowChild = false;
                    OnPropertyChanged(nameof(IsAddSpendRowChild));
                }
            }
            OrderItems(targetItem.IIdParentId);
            targetItem.PropertyChanged += QuyetDinhKhacChiPhi_PropertyChanged;
            OnPropertyChanged(nameof(ItemsQuyetDinhKhacChiPhi));
        }

        private void OnDeleteQuyetDinhKhacChiPhi()
        {
            if (SelectedQuyetDinhKhacChiPhi != null)
            {
                DeleteTreeItems(SelectedQuyetDinhKhacChiPhi, !SelectedQuyetDinhKhacChiPhi.IsDeleted);
            }
        }

        private void DeleteTreeItems(NhDaQuyetDinhKhacChiPhiModel currentItem, bool status)
        {
            if (currentItem != null)
            {
                var items = _itemsQuyetDinhKhacChiPhi;
                currentItem.IsDeleted = status;
                var childs = items.Where(x => x.IIdParentId == currentItem.Id);
                if (!childs.IsEmpty())
                {
                    foreach (var item in childs)
                    {
                        DeleteTreeItems(item, status);
                    }
                }
            }
        }

        private int CountTreeChildItems(NhDaQuyetDinhKhacChiPhiModel currentItem)
        {
            var items = _itemsQuyetDinhKhacChiPhi;
            int count = 0;
            var childs = items.Where(x => x.IIdParentId == currentItem.Id);
            if (!childs.IsEmpty())
            {
                count += childs.Count();
                foreach (var item in childs)
                {
                    count += CountTreeChildItems(item);
                }
            }
            return count;
        }


        private void OrderItems(Guid? parentId = null)
        {
            var childs = _itemsQuyetDinhKhacChiPhi.Where(x => x.IIdParentId == parentId);
            if (!childs.IsEmpty())
            {
                var parent = _itemsQuyetDinhKhacChiPhi.FirstOrDefault(x => x.Id == parentId);
                int index = 1;
                foreach (var child in childs)
                {
                    if (parent != null)
                    {
                        child.SMaOrder = string.Format("{0}-{1}", parent.SMaOrder, index.ToString("D0"));
                    }
                    else
                    {
                        child.SMaOrder = index.ToString("D0");
                    }
                    child.SMaChiPhi = StringUtils.ConvertMaOrder(child.SMaOrder);
                    OrderItems(child.Id);
                    index++;
                }
            }
        }



        private void QuyetDinhKhacChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhDaQuyetDinhKhacChiPhiModel objectSender = (NhDaQuyetDinhKhacChiPhiModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaQuyetDinhKhacChiPhiModel.IsDeleted))
                || e.PropertyName.Equals(nameof(NhDaQuyetDinhKhacChiPhiModel.FGiaTriUsd))
                || e.PropertyName.Equals(nameof(NhDaQuyetDinhKhacChiPhiModel.FGiaTriVnd)))
            {

                var chiPhi = ItemsQuyetDinhKhacChiPhi.Where(n => n.Id == objectSender.IIdParentId).FirstOrDefault();
                if (chiPhi != null)
                {
                    if (e.PropertyName.Equals(nameof(NhDaQuyetDinhKhacChiPhiModel.FGiaTriUsd)))
                    {
                        chiPhi.FGiaTriUsd = ItemsQuyetDinhKhacChiPhi.Where(n => n.IIdParentId == chiPhi.Id).Sum(n => n.FGiaTriUsd);
                    }


                    else if (e.PropertyName.Equals(nameof(NhDaQuyetDinhKhacChiPhiModel.FGiaTriVnd)))
                    {
                        chiPhi.FGiaTriVnd = ItemsQuyetDinhKhacChiPhi.Where(n => n.IIdParentId == chiPhi.Id).Sum(n => n.FGiaTriVnd);


                    }
                    else
                    {
                        chiPhi.FGiaTriUsd = ItemsQuyetDinhKhacChiPhi.Where(n => n.IIdParentId == chiPhi.Id && !n.IsDeleted).Sum(n => n.FGiaTriUsd);
                        chiPhi.FGiaTriVnd = ItemsQuyetDinhKhacChiPhi.Where(n => n.IIdParentId == chiPhi.Id && !n.IsDeleted).Sum(n => n.FGiaTriVnd);

                    }
                }

                var LstchiPhiNoParent = ItemsQuyetDinhKhacChiPhi.Where(x => x.IIdParentId == null).ToList();
                if (LstchiPhiNoParent != null)
                {
                    foreach (var item in LstchiPhiNoParent)
                    {
                        var itemChiPhiDM = ItemsDMChiPhi.Where(x => x.IIdChiPhi == item.IIdDmChiPhiId).FirstOrDefault();
                        item.STenChiPhi = itemChiPhiDM != null ? itemChiPhiDM.STenChiPhi : string.Empty;
                    }
                }
            }
            //objectSender.PropertyChanged -= QuyetDinhKhacChiPhi_PropertyChanged;
            CalculateChiPhi();
            objectSender.IsModified = true;
        }

        private void CalculateChiPhi()
        {
            var parents = ItemsQuyetDinhKhacChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null);
            if (parents.Any())
            {
                foreach (var item in parents)
                {
                    item.FGiaTriUsd = ItemsQuyetDinhKhacChiPhi.Any(x => !x.IsDeleted && x.IIdParentId == item.Id) ? ItemsQuyetDinhKhacChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == item.Id).Sum(x => x.FGiaTriUsd) : item.FGiaTriUsd;
                    item.FGiaTriVnd = ItemsQuyetDinhKhacChiPhi.Any(x => !x.IsDeleted && x.IIdParentId == item.Id) ? ItemsQuyetDinhKhacChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == item.Id).Sum(x => x.FGiaTriVnd) : item.FGiaTriVnd;
                }
            }

            Model.FGiaTriUsd = ItemsQuyetDinhKhacChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriUsd);
            Model.FGiaTriVnd = ItemsQuyetDinhKhacChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriVnd);
            FGiaTriVnd = Model.FGiaTriVnd;
            FGiaTriUsd = Model.FGiaTriUsd;
            FSumGiaTriVnd = FGiaTriVnd;
            FSumGiaTriUsd = FGiaTriUsd;
            OnPropertyChanged(nameof(Model));
        }

        private void LoadDuToanChiPhi()
        {
            if (!Model.Id.IsNullOrEmpty())
            {
                // Cập nhật hoặc Điều chỉnh
                IEnumerable<NhDaQuyetDinhKhacChiPhi> data = _nhDaQuyetDinhKhacChiPhiService.FindByQuyetDinhKhacId(Model.Id).OrderBy(x => x.SMaOrder);
                _itemsQuyetDinhKhacChiPhi = _mapper.Map<ObservableCollection<NhDaQuyetDinhKhacChiPhiModel>>(data);
                if (_itemsQuyetDinhKhacChiPhi.Any())
                {
                    _itemsQuyetDinhKhacChiPhi.ForAll(x =>
                    {
                        x.ItemsLoaiNoiDungChi = ItemsDMChiPhi;
                        x.PropertyChanged += QuyetDinhKhacChiPhi_PropertyChanged;
                    });
                    IsAddSpendRowChild = true;
                    if (IsDetail)
                        IsAddSpendRowChild = false;
                }
            }
            else
            {
                _itemsQuyetDinhKhacChiPhi = new ObservableCollection<NhDaQuyetDinhKhacChiPhiModel>();
            }
            FGiaTriUsd = Model.FGiaTriUsd;
            FGiaTriVnd = Model.FGiaTriVnd;
            CalculateChiPhiDetail();
            OnPropertyChanged(nameof(_itemsQuyetDinhKhacChiPhi));
        }

        private void CalculateChiPhiDetail()
        {
            var parents = ItemsQuyetDinhKhacChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null);
            if (parents.Any())
            {
                foreach (var item in parents)
                {
                    item.FGiaTriUsd = ItemsQuyetDinhKhacChiPhi.Any(x => !x.IsDeleted && x.IIdParentId == item.Id) ? ItemsQuyetDinhKhacChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == item.Id).Sum(x => x.FGiaTriUsd) : item.FGiaTriUsd;
                    item.FGiaTriVnd = ItemsQuyetDinhKhacChiPhi.Any(x => !x.IsDeleted && x.IIdParentId == item.Id) ? ItemsQuyetDinhKhacChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == item.Id).Sum(x => x.FGiaTriVnd) : item.FGiaTriVnd;
                }
            }
            FSumGiaTriUsd = ItemsQuyetDinhKhacChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriUsd);
            FSumGiaTriVnd = ItemsQuyetDinhKhacChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriVnd);
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                Dispose();
                window.Close();
            }
        }
    }
}
