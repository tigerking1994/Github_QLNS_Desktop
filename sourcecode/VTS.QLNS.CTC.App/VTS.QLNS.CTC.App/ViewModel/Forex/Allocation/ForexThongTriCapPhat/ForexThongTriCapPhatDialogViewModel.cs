using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexPheDuyetThanhToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexThongTriCapPhat
{
    public class ForexThongTriCapPhatDialogViewModel : DialogAttachmentViewModelBase<NhTtThongTriCapPhatModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILogger<ForexThongTriCapPhatDialogViewModel> _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhDmLoaiTienTeService _nhDmLoaiTienTeService;
        private readonly INhTtThongTriCapPhatChiTietService _nhTtThongTriCapPhatChiTietService;
        private readonly INhTtThanhToanService _nhTtThanhToanService;
        private readonly INhThTongHopService _nhThTongHopService;
        private readonly INhTtThongTriCapPhatService _nhTtThongTriCapPhatService;

        public override Type ContentType => typeof(View.Forex.ForexAllocation.ForexThongTriCapPhat.ForexThongTriCapPhatDialog);

        public RelayCommand OnChiTietPheDuyetCommand { get; }

        public ForexPheDuyetThanhToanDialogViewModel ForexPheDuyetThanhToanDialogViewModel { get; }

        public bool IsDetail { get; set; }
        private ObservableCollection<YearModel> _itemsNamThucHien;
        public ObservableCollection<YearModel> ItemsNamThucHien
        {
            get => _itemsNamThucHien;
            set => SetProperty(ref _itemsNamThucHien, value);
        }

        private YearModel _selectedNamThucHien;
        public YearModel SelectedNamThucHien
        {
            get => _selectedNamThucHien;
            set
            {
                SetProperty(ref _selectedNamThucHien, value);
                LoadDsPheDuyetThanhToan();
            }
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
                SetProperty(ref _selectedDonVi, value);
                LoadDsPheDuyetThanhToan();
            }
        }

        private ObservableCollection<NguonNganSachModel> _itemsNguonNganSach;
        public ObservableCollection<NguonNganSachModel> ItemsNguonNganSach
        {
            get => _itemsNguonNganSach;
            set => SetProperty(ref _itemsNguonNganSach, value);
        }

        private NguonNganSachModel _selectedNganSach;
        public NguonNganSachModel SelectedNganSach
        {
            get => _selectedNganSach;
            set 
            {                
                SetProperty(ref _selectedNganSach, value);
                LoadDsPheDuyetThanhToan();
            }

        }

        private ObservableCollection<DmLoaiTienTeModel> _itemsLoaiTienTe;
        public ObservableCollection<DmLoaiTienTeModel> ItemsLoaiTienTe
        {
            get => _itemsLoaiTienTe;
            set => SetProperty(ref _itemsLoaiTienTe, value);
        }

        private DmLoaiTienTeModel _selectedLoaiTienTe;
        public DmLoaiTienTeModel SelectedLoaiTienTe
        {
            get => _selectedLoaiTienTe;
            set => SetProperty(ref _selectedLoaiTienTe, value);
        }

        private ObservableCollection<NhTtThongTriCapPhatChiTietModel> _itemsDsPheDuyetThanhToan;
        public ObservableCollection<NhTtThongTriCapPhatChiTietModel> ItemsDsPheDuyetThanhToan
        {
            get => _itemsDsPheDuyetThanhToan;
            set => SetProperty(ref _itemsDsPheDuyetThanhToan, value);
        }

        private NhTtThongTriCapPhatChiTietModel _selectedDsPheDuyetThanhToan;
        public NhTtThongTriCapPhatChiTietModel SelectedDsPheDuyetThanhToan
        {
            get => _selectedDsPheDuyetThanhToan;
            set => SetProperty(ref _selectedDsPheDuyetThanhToan, value);
        }

        private double? _fUsd;
        public double? FUsd
        {
            get => _fUsd;
            set => SetProperty(ref _fUsd, value);
        }

        private double? _fVnd;
        public double? FVnd
        {
            get => _fVnd;
            set => SetProperty(ref _fVnd, value);
        }

        private double? _fEur;
        public double? FEur
        {
            get => _fEur;
            set => SetProperty(ref _fEur, value);
        }

        private double? _fNgoaiTe;
        public double? FNgoaiTe
        {
            get => _fNgoaiTe;
            set => SetProperty(ref _fNgoaiTe, value);
        }

        public bool? IsAllSelected
        {
            get
            {
                if (ItemsDsPheDuyetThanhToan != null)
                {
                    var selected = ItemsDsPheDuyetThanhToan.Select(x => x.IsSelected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ItemsDsPheDuyetThanhToan);
                    OnPropertyChanged();
                }
            }
        }

        public ForexThongTriCapPhatDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILogger<ForexThongTriCapPhatDialogViewModel> logger,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            INhThTongHopService nhThTongHopService,
            INhDmLoaiTienTeService nhDmLoaiTienTeService,
            INhTtThongTriCapPhatChiTietService nhTtThongTriCapPhatChiTietService,
            ForexPheDuyetThanhToanDialogViewModel forexPheDuyetThanhToanDialogViewModel,
            INhTtThanhToanService nhTtThanhToanService,
            INhTtThongTriCapPhatService nhTtThongTriCapPhatService) : base(mapper, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDmLoaiTienTeService = nhDmLoaiTienTeService;
            _nhTtThongTriCapPhatChiTietService = nhTtThongTriCapPhatChiTietService;
            _nhThTongHopService = nhThTongHopService;
            _nhTtThanhToanService = nhTtThanhToanService;
            _nhTtThongTriCapPhatService = nhTtThongTriCapPhatService;

            ForexPheDuyetThanhToanDialogViewModel = forexPheDuyetThanhToanDialogViewModel;
            OnChiTietPheDuyetCommand = new RelayCommand(obj => OnChiTietPheDuyet());
        }

        public override void Init()
        {
            base.Init();
            LoadNamThucHien();
            LoadDonVi();
            LoadNganSach();
            LoadTienTe();
            LoadDsPheDuyetThanhToan();
            LoadData();
            TinhTong();
        }
        private void LoadNamThucHien()
        {
            _itemsNamThucHien = new ObservableCollection<YearModel>();
            _selectedNamThucHien = new YearModel();
            List<YearModel> years = new List<YearModel>();
            int yearPlus = _sessionService.Current.YearOfWork;
            int yearMinus = _sessionService.Current.YearOfWork;
            for (int i = 0; i < 20; i++)
            {
                YearModel item = new YearModel
                {
                    Year = yearMinus
                };
                years.Add(item);
                --yearMinus;
                ++yearPlus;
                YearModel itemPlus = new YearModel
                {
                    Year = yearPlus
                };
                years.Add(itemPlus);

            }
            _itemsNamThucHien = new ObservableCollection<YearModel>(years.OrderByDescending(x => x.Year));
            if (!Model.Id.IsNullOrEmpty())
            {
                _selectedNamThucHien = _itemsNamThucHien.Where(n => n.Year == Model.INamThucHien).FirstOrDefault();
            }
            OnPropertyChanged(nameof(SelectedNamThucHien));
            OnPropertyChanged(nameof(ItemsNamThucHien));
        }
        private void LoadDonVi()
        {
            int year = _sessionService.Current.YearOfWork;
            var data = _nsDonViService.FindByCondition(x => x.NamLamViec == year);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            if (!Model.Id.IsNullOrEmpty() && _itemsDonVi != null)
            {
                _selectedDonVi = _itemsDonVi.Where(n => n.IIDMaDonVi == Model.IIdMaDonViId).FirstOrDefault();
            }
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadNganSach()
        {           
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonNganSach = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            if (!Model.Id.IsNullOrEmpty() && _itemsNguonNganSach != null)
            {
                _selectedNganSach = _itemsNguonNganSach.Where(n => n.IIdMaNguonNganSach == Model.IIdNguonVonId).FirstOrDefault();
            }
            OnPropertyChanged(nameof(SelectedNganSach));
            OnPropertyChanged(nameof(ItemsNguonNganSach));
        }

        private void LoadTienTe()
        {
            var data = _nhDmLoaiTienTeService.FindAll().OrderBy(x => x.SMaTienTe);
            _itemsLoaiTienTe = _mapper.Map<ObservableCollection<DmLoaiTienTeModel>>(data);
            if (!Model.Id.IsNullOrEmpty() && _itemsLoaiTienTe != null)
            {
                _selectedLoaiTienTe = _itemsLoaiTienTe.Where(n => n.SMaTienTe == Model.STenTienTe).FirstOrDefault();
            }
            OnPropertyChanged(nameof(SelectedLoaiTienTe));
            OnPropertyChanged(nameof(ItemsLoaiTienTe));
        }

        private void LoadDsPheDuyetThanhToan()
        {
            var data = _nhTtThongTriCapPhatChiTietService.FindAllChiTiet();
            var dataMap = _mapper.Map<ObservableCollection<NhTtThongTriCapPhatChiTietModel>>(data).ToList();
            //Thien Update (25/10/2022) bug 31419 / hienpt57 
            var checkMapData = dataMap.Where(x => x.ITrangThai == 0 || x.IIdThongTriCapPhatId == Model.Id).ToList();
            //
            if (Model.Id.IsNullOrEmpty())
            {
                checkMapData.Select(x =>
                {
                    x.STenTrangThai = x.ITrangThai != 0 ? "Đã được cấp thông tri" : "Chưa được cấp thông tri";
                    x.IsEnabled = x.ITrangThai != 0 ? true : false;
                    return x;
                }).ToList();
            }
            else
            {
                checkMapData.Select(x =>
                {
                    x.STenTrangThai = x.ITrangThai != 0 ? "Đã được cấp thông tri" : "Chưa được cấp thông tri";
                    if (x.IIdThongTriCapPhatId == Model.Id)
                    {
                        x.IsSelected = true;
                        x.IsChecked = true;
                        x.IsEnabled = false;
                    }
                    else
                    {
                        x.IsEnabled = x.ITrangThai != 0 ? true : false;
                    }
                    return x;
                }).ToList();
            }
            _itemsDsPheDuyetThanhToan = new ObservableCollection<NhTtThongTriCapPhatChiTietModel>(checkMapData);
            foreach (var item in _itemsDsPheDuyetThanhToan)
            {
                item.PropertyChanged += Detail_PropertyChange;
            }

            if (SelectedDonVi != null)
            {
                ObservableCollection<NhTtThongTriCapPhatChiTietModel> filterByDonvi = new ObservableCollection<NhTtThongTriCapPhatChiTietModel>();
                foreach (var item in _itemsDsPheDuyetThanhToan)
                {
                    if (item.iID_DonVi.ToString() == SelectedDonVi.Id.ToString() || item.IsChecked == true)
                        filterByDonvi.Add(item);
                };
                _itemsDsPheDuyetThanhToan = filterByDonvi;
            }

            if (SelectedNamThucHien != null && SelectedNamThucHien.Year != 0)
            {
                ObservableCollection<NhTtThongTriCapPhatChiTietModel> filterByNam = new ObservableCollection<NhTtThongTriCapPhatChiTietModel>();
                foreach (var item in _itemsDsPheDuyetThanhToan)
                {
                    if (item.iNamKeHoach == SelectedNamThucHien.Year || item.IsChecked == true)
                        filterByNam.Add(item);
                };
                _itemsDsPheDuyetThanhToan = filterByNam;
            }

            if (SelectedNganSach != null)
            {
                ObservableCollection<NhTtThongTriCapPhatChiTietModel> filterByNguonvon = new ObservableCollection<NhTtThongTriCapPhatChiTietModel>();
                foreach (var item in _itemsDsPheDuyetThanhToan)
                {
                    if (item.iID_NguonVonID == SelectedNganSach.IIdMaNguonNganSach || item.IsChecked == true)
                        filterByNguonvon.Add(item);
                };
                _itemsDsPheDuyetThanhToan = filterByNguonvon;
            }
            OnPropertyChanged(nameof(ItemsDsPheDuyetThanhToan));
        }

        private void Detail_PropertyChange(object sender, PropertyChangedEventArgs e)
        {
            NhTtThongTriCapPhatChiTietModel item = (NhTtThongTriCapPhatChiTietModel)sender;
            if (e.PropertyName == nameof(NhTtThongTriCapPhatChiTietModel.IsSelected))
            {
                if (item.IsEnabled)
                {
                    item.IsSelected = false;
                }
                else
                {
                    if (item.ITrangThai == 0)
                    {
                        item.IsAdded = !item.IsAdded;
                    }
                    if (item.ITrangThai != 0 && item.IIdThongTriCapPhatId == Model.Id)
                    {
                        item.IsDeleted = !item.IsDeleted;
                    }
                    TinhTong();
                }
            }
        }

        private void TinhTong()
        {
            var lstDsPheDuyetSelected = ItemsDsPheDuyetThanhToan.Where(x => x.IsSelected).ToList();
            FUsd = lstDsPheDuyetSelected.Sum(x => x.FPheDuyetUsd);
            FVnd = lstDsPheDuyetSelected.Sum(x => x.FPheDuyetVnd);
            FEur = lstDsPheDuyetSelected.Sum(x => x.FPheDuyetEur);
            FNgoaiTe = lstDsPheDuyetSelected.Sum(x => x.FPheDuyetNgoaiTeKhac);
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                Title = "THÊM MỚI THÔNG TRI";
                Description = "Thêm mới thông tri";
            }
            else
            {
                if (IsDetail)
                {
                    Title = "THÔNG TRI CHI TIẾT";
                    Description = "Thông tri chi tiết";
                }
                else
                {
                    Title = "CẬP NHẬT THÔNG TRI";
                    Description = "cập nhật thông tri";
                }
                SelectedNamThucHien = ItemsNamThucHien.FirstOrDefault(x => x.Year == Model.INamThucHien);
                SelectedDonVi = ItemsDonVi.FirstOrDefault(x => x.Id == Model.IIdDonViId);
                SelectedNganSach = ItemsNguonNganSach.FirstOrDefault(x => x.IIdMaNguonNganSach == Model.IIdNguonVonId);
                SelectedLoaiTienTe = ItemsLoaiTienTe.FirstOrDefault(x => x.Id == Model.IIdDonViTienTeId);
            }
        }

        public override void OnSave()
        {
            if (!ValidateData()) return;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                
                ConvertData();
                NhTtThongTriCapPhat entity;
                if (Model.Id.IsNullOrEmpty())
                {
                    foreach (var item in ItemsDsPheDuyetThanhToan)
                    {
                        if (item.IsSelected && item.ITrangThai == 0)
                        {
                            item.ITrangThai = 1;
                            Model.NhTtThongTriCapPhatChiTiets.Add(item);
                            _nhThTongHopService.InsertNHTongHop_New(NHConstants.TTCP, (int)TypeExecute.Insert, Guid.Parse(item.IIdPheDuyetThanhToanId.ToString()), null);
                        }
                    }
                    entity = _mapper.Map<NhTtThongTriCapPhat>(Model);
                    entity.Id = Guid.NewGuid();
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.INamThucHien = SelectedNamThucHien.Year;
                    entity.BIsKhoa = false;
                    entity.BIsActive = true;
                    _nhTtThongTriCapPhatService.Add(entity);
                }
                else
                {
                    foreach (var item in ItemsDsPheDuyetThanhToan)
                    {
                        if (item.IIdThongTriCapPhatId == Model.Id && item.IsDeleted)
                        {
                            item.ITrangThai = 0;
                            //_nhThTongHopService.InsertNHTongHop_Tang("TTCP", 3, Guid.Parse(item.IIdPheDuyetThanhToanId.ToString()), null);
                            _nhThTongHopService.InsertNHTongHop_New(NHConstants.TTCP, (int)TypeExecute.Delete, Guid.Parse(item.IIdPheDuyetThanhToanId.ToString()), null);

                            item.IsChecked = false;
                        }
                        if (item.IsSelected && item.ITrangThai == 0)
                        {
                            item.ITrangThai = 1;
                            Model.NhTtThongTriCapPhatChiTiets.Add(item);
                            //_nhThTongHopService.InsertNHTongHop_Tang("TTCP", 1, Guid.Parse(item.IIdPheDuyetThanhToanId.ToString()), null);
                            _nhThTongHopService.InsertNHTongHop_New(NHConstants.TTCP, (int)TypeExecute.Insert, Guid.Parse(item.IIdPheDuyetThanhToanId.ToString()), null);

                        }
                    }
                    Model.NhTtThongTriCapPhatChiTiets = ItemsDsPheDuyetThanhToan;
                    entity = _nhTtThongTriCapPhatService.FindById(Model.Id);
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    entity.DNgaySua = DateTime.Now;
                    Model.INamThucHien = SelectedNamThucHien.Year;
                    _mapper.Map(Model, entity);
                    _nhTtThongTriCapPhatService.Update(entity);
                }
                SaveAttachment(entity.Id);
                e.Result = entity;
            }, (s, e) =>
            {
                IsLoading = false;
                // Reload data
                Model = _mapper.Map<NhTtThongTriCapPhatModel>(e.Result);
                SavedAction?.Invoke(Model);
                System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone);
                LoadData();
                LoadDsPheDuyetThanhToan();
            });
        }

        private void ConvertData()
        {
            if (SelectedDonVi != null)
            {
                Model.IIdDonViId = SelectedDonVi.Id;
                Model.IIdMaDonViId = SelectedDonVi.IIDMaDonVi;
            }
            if (SelectedNganSach != null)
            {
                Model.IIdNguonVonId = SelectedNganSach.IIdMaNguonNganSach;
            }
            if (SelectedLoaiTienTe != null)
            {
                Model.IIdDonViTienTeId = SelectedLoaiTienTe.Id;
            }
            Model.FTongGiaTriUsd = FUsd;
            Model.FTongGiaTriVnd = FVnd;
            Model.FTongGiaTriEUR = FEur;
            Model.FTongGiaTriNgoaiTeKhac = FNgoaiTe;
        }

        private bool ValidateData()
        {
            if (SelectedDonVi == null)
            {
                MessageBoxHelper.Warning("Chọn đơn vị");
                return false;
            }
            if (SelectedNganSach == null)
            {
                MessageBoxHelper.Warning("Chọn nguồn vốn");
                return false;
            }
            if (SelectedLoaiTienTe == null)
            {
                MessageBoxHelper.Warning("Chọn loại tiền tệ");
                return false;
            }
            if (string.IsNullOrEmpty(Model.SMaThongTri))
            {
                MessageBoxHelper.Warning("Nhập mã thông tri");
                return false;
            }
            if (Model.DNgayLapThongTri.Value == null)
            {
                MessageBoxHelper.Warning("Nhập ngày lập thông tri");
                return false;
            }
            if (SelectedNamThucHien == null)
            {
                MessageBoxHelper.Warning("Nhập năm thực hiện");
                return false;
            }
            return true;
        }

        private static void SelectAll(bool select, ObservableCollection<NhTtThongTriCapPhatChiTietModel> models)
        {
            foreach (var model in models)
            {
                if (model.IIdThongTriCapPhatId != null)
                {
                    model.IsSelected = select;
                }
                else
                {
                    model.IsSelected = false;
                }
            }
        }

        private void OnChiTietPheDuyet()
        {
            var thanhToan = _nhTtThanhToanService.FindById((Guid)SelectedDsPheDuyetThanhToan.IIdPheDuyetThanhToanId);
            ForexPheDuyetThanhToanDialogViewModel.Model = _mapper.Map<NhTtThanhToanModel>(thanhToan);
            ForexPheDuyetThanhToanDialogViewModel.IsDetail = true;
            ForexPheDuyetThanhToanDialogViewModel.IsEdit = false;
            ForexPheDuyetThanhToanDialogViewModel.Init();
            ForexPheDuyetThanhToanDialogViewModel.ShowDialog();
        }

        public override void Dispose()
        {
            // clearn item
            if (!_itemsDsPheDuyetThanhToan.IsEmpty()) _itemsDsPheDuyetThanhToan.Clear();
            if (_fUsd != null) _fUsd = 0;
            if (_fVnd != null) _fVnd = 0;
            if (_fEur != null) _fEur = 0;
            if (_fNgoaiTe != null) _fNgoaiTe = 0;
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
