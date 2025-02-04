using AutoMapper;
using FlexCel.XlsAdapter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexDeNghiThanhToan
{
    public class ForexDeNghiThanhToanImportViewModel : ViewModelBase
    {
        public override Type ContentType => typeof(VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.CapPhatThanhToan.CapPhatThanhToanImport);

        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private IImportExcelService _importService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly INsDonViService _nsDonViService;
        private readonly IDmChuDauTuService _chuDauTuService;
        private readonly ISessionService _sessionService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly INhKhTongTheService _iNhKhTongTheService;
        private readonly INhDmNhiemVuChiService _iDmNhiemVuChiService;
        private readonly IDmChuDauTuService _iDmChuDauTuService;
        private readonly INhDaHopDongService _iNhDaHopDongService;
        private readonly INsNguonNganSachService _iNguonNganSachService;
        private readonly INhDmNhaThauService _iNhDmNhaThauService;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        private readonly INhTtThanhToanService _iNhTtThanhToanService;
        private readonly INhTtThanhToanChiTietService _iNhTtThanhToanChiTietService;
        private readonly INhDaDuAnService _iNhDaDuAnService;

        private readonly IMapper _mapper;
        private List<ImportErrorItem> _lstErrChungTuChiTiet;
        private List<string> lstSoDeNghi = new List<string>();
        private List<VdtDaTtHopDong> _lstHopDong;
        private List<VdtDmChiPhi> _lstCP;
        private string _fileName;
        private Dictionary<string, Guid> _dicDuAn = new Dictionary<string, Guid>();
        #endregion

        #region Import item
        private ObservableCollection<ForexDeNghiThanhToanImportModel> _itemsThanhToan;
        public ObservableCollection<ForexDeNghiThanhToanImportModel> ItemsThanhToan
        {
            get => _itemsThanhToan;
            set => SetProperty(ref _itemsThanhToan, value);
        }

        private ForexDeNghiThanhToanImportModel _selectedThanhToan;
        public ForexDeNghiThanhToanImportModel SelectedThanhToan
        {
            get => _selectedThanhToan;
            set => SetProperty(ref _selectedThanhToan, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public bool _isSelectedFile;
        public bool IsSelectedFile
        {
            get => !string.IsNullOrEmpty(_filePath);
            set => SetProperty(ref _isSelectedFile, value);
        }

        public bool _isSaveData;
        public bool IsSaveData
        {
            get => ItemsThanhToan != null && ItemsThanhToan.Count > 0 && !ItemsThanhToan.Any(x => !x.ImportStatus);
            set => SetProperty(ref _isSaveData, value);
        }
        #endregion

        #region Items
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
                LoadNhiemVuChi();
            }
        }

        private string _sSoDeNghi;
        public string SSoDeNghi
        {
            set => SetProperty(ref _sSoDeNghi, value);
        }

        public DateTime _dNgayDeNghi;
        public DateTime DNgayDeNghi
        {
            set => SetProperty(ref _dNgayDeNghi, value);
        }

        private ObservableCollection<ComboboxItem> _itemsKeHoachTongThe;
        public ObservableCollection<ComboboxItem> ItemsKeHoachTongThe
        {
            get => _itemsKeHoachTongThe;
            set => SetProperty(ref _itemsKeHoachTongThe, value);
        }

        private ComboboxItem _selectedKeHoachTongThe;
        public ComboboxItem SelectedKeHoachTongThe
        {
            get => _selectedKeHoachTongThe;
            set
            {
                SetProperty(ref _selectedKeHoachTongThe, value);
                LoadNhiemVuChi();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsNhiemVuChi;
        public ObservableCollection<ComboboxItem> ItemsNhiemVuChi
        {
            get => _itemsNhiemVuChi;
            set => SetProperty(ref _itemsNhiemVuChi, value);
        }

        private ComboboxItem _selectedNhiemVuChi;
        public ComboboxItem SelectedNhiemVuChi
        {
            get => _selectedNhiemVuChi;
            set
            {
                SetProperty(ref _selectedNhiemVuChi, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsChuDauTu;
        public ObservableCollection<ComboboxItem> ItemsChuDauTu
        {
            get => _itemsChuDauTu;
            set => SetProperty(ref _itemsChuDauTu, value);
        }

        private ComboboxItem _selectedChuDauTu;
        public ComboboxItem SelectedChuDauTu
        {
            get => _selectedChuDauTu;
            set
            {
                SetProperty(ref _selectedChuDauTu, value);
            }
        }
        private ObservableCollection<ComboboxItem> _itemsDaHopDong;
        public ObservableCollection<ComboboxItem> ItemsDaHopDong
        {
            get => _itemsDaHopDong;
            set => SetProperty(ref _itemsDaHopDong, value);
        }

        private ComboboxItem _selectedDaHopDong;
        public ComboboxItem SelectedDaHopDong
        {
            get => _selectedDaHopDong;
            set
            {
                SetProperty(ref _selectedDaHopDong, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDaDuAn;
        public ObservableCollection<ComboboxItem> ItemsDaDuAn
        {
            get => _itemsDaDuAn;
            set => SetProperty(ref _itemsDaDuAn, value);
        }

        private ComboboxItem _selectedDaDuAn;
        public ComboboxItem SelectedDaDuAn
        {
            get => _selectedDaDuAn;
            set
            {
                SetProperty(ref _selectedDaDuAn, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiDeNghi;
        public ObservableCollection<ComboboxItem> ItemsLoaiDeNghi
        {
            get => _itemsLoaiDeNghi;
            set => SetProperty(ref _itemsLoaiDeNghi, value);
        }

        private ComboboxItem _selectedLoaiDeNghi;
        public ComboboxItem SelectedLoaiDeNghi
        {
            get => _selectedLoaiDeNghi;
            set
            {
                SetProperty(ref _selectedLoaiDeNghi, value);
            }
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
            set
            {
                SetProperty(ref _selectedNamNganSach, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsCoQuanThanhToan;
        public ObservableCollection<ComboboxItem> ItemsCoQuanThanhToan
        {
            get => _itemsCoQuanThanhToan;
            set => SetProperty(ref _itemsCoQuanThanhToan, value);
        }

        private ObservableCollection<ComboboxItem> _itemsThanhToanTheo;
        public ObservableCollection<ComboboxItem> ItemsThanhToanTheo
        {
            get => _itemsThanhToanTheo;
            set => SetProperty(ref _itemsThanhToanTheo, value);
        }

        private ComboboxItem _selectedCoQuanThanhToan;
        public ComboboxItem SelectedCoQuanThanhToan
        {
            get => _selectedCoQuanThanhToan;
            set
            {
                SetProperty(ref _selectedCoQuanThanhToan, value);
            }
        }

        public ComboboxItem _selectedThanhToanTheo;
        public ComboboxItem SelectedThanhToanTheo
        {
            get => _selectedThanhToanTheo;
            set
            {
                SetProperty(ref _selectedThanhToanTheo, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsNguonVon;
        public ObservableCollection<ComboboxItem> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private ComboboxItem _selectedNguonVon;
        public ComboboxItem SelectedNguonVon
        {
            get => _selectedNguonVon;
            set
            {
                SetProperty(ref _selectedNguonVon, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiNoiDungChi;
        public ObservableCollection<ComboboxItem> ItemsLoaiNoiDungChi
        {
            get => _itemsLoaiNoiDungChi;
            set => SetProperty(ref _itemsLoaiNoiDungChi, value);
        }

        private ComboboxItem _selectedLoaiNoiDungChi;
        public ComboboxItem SelectedLoaiNoiDungChi
        {
            get => _selectedLoaiNoiDungChi;
            set
            {
                SetProperty(ref _selectedLoaiNoiDungChi, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsQuyKeHoach;
        public ObservableCollection<ComboboxItem> ItemsQuyKeHoach
        {
            get => _itemsQuyKeHoach;
            set => SetProperty(ref _itemsQuyKeHoach, value);
        }

        private ComboboxItem _selectedQuyKeHoach;
        public ComboboxItem SelectedQuyKeHoach
        {
            get => _selectedQuyKeHoach;
            set
            {
                SetProperty(ref _selectedQuyKeHoach, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDmNhaThau;
        public ObservableCollection<ComboboxItem> ItemsDmNhaThau
        {
            get => _itemsDmNhaThau;
            set => SetProperty(ref _itemsDmNhaThau, value);
        }

        private ComboboxItem _selectedDmNhaThau;
        public ComboboxItem SelectedDmNhaThau
        {
            get => _selectedDmNhaThau;
            set
            {
                SetProperty(ref _selectedDmNhaThau, value);
                
            }
        }

        #endregion

        #region RelayCommand
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        #endregion

        public ForexDeNghiThanhToanImportViewModel(
            IImportExcelService importService,
            ITongHopNguonNSDauTuService tonghopService,
            INsNguonNganSachService nguonVonService,
            INsDonViService nsDonViService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IDmChuDauTuService chuDauTuService,
            ISessionService sessionService,
            INhKhTongTheService iNhKhTongTheService,
            INhDmNhiemVuChiService iDmNhiemVuChiService,
            IDmChuDauTuService iDmChuDauTuService,
            INhDaHopDongService iNhDaHopDongService,
            INsNguonNganSachService iNguonNganSachService,
            INhDmNhaThauService iNhDmNhaThauService,
            INsMucLucNganSachService iNsMucLucNganSachService,
            INhTtThanhToanService iNhTtThanhToanService,
            INhTtThanhToanChiTietService iNhTtThanhToanChiTietService,
            IMapper mapper)
        {
            _importService = importService;
            _tonghopService = tonghopService;
            _nguonVonService = nguonVonService;
            _nsDonViService = nsDonViService;
            _chuDauTuService = chuDauTuService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _chuDauTuService = chuDauTuService;
            _iNhKhTongTheService = iNhKhTongTheService;
            _iDmNhiemVuChiService = iDmNhiemVuChiService;
            _iDmChuDauTuService = iDmChuDauTuService;
            _iNhDaHopDongService = iNhDaHopDongService;
            _iNguonNganSachService = iNguonNganSachService;
            _iNhDmNhaThauService = iNhDmNhaThauService;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            _iNhTtThanhToanService = iNhTtThanhToanService;
            _iNhTtThanhToanChiTietService = iNhTtThanhToanChiTietService;


            _sessionService = sessionService;
            _mapper = mapper;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(ShowError);
        }

        #region RelayCommnad
        public override void Init()
        {
            
            FilePath = string.Empty;
            SelectedDonVi = null;
            SelectedChuDauTu = null;
            SelectedCoQuanThanhToan = null;
            ItemsThanhToan = new ObservableCollection<ForexDeNghiThanhToanImportModel>();
            LoadDonVi();
            LoadKeHoachTongThe();
            LoadCoQuanThanhToan();
            LoadThanhToanTheo();
            LoadChuDauTu();
            LoadLoaiDeNghi();
            LoadLoaiNoiDungChi();
        }
        private void LoadDonVi()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            var lstDonVi = _nsDonViService.FindByCondition(predicate).ToList();

            var cbDonVi = lstDonVi.Select(x =>
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = x.IIDMaDonVi + "-" + x.TenDonVi;
                cb.ValueItem = x.Id.ToString();
                cb.Id = x.Id;
                return cb;
            }).ToList();
            ItemsDonVi = new ObservableCollection<ComboboxItem>(cbDonVi);
        }

        private void LoadKeHoachTongThe()
        {
            var predicate = PredicateBuilder.True<NhKhTongThe>();
            predicate = predicate.And(x => x.BIsActive);
            var lstKeHoachTongThe = _iNhKhTongTheService.FindAll(predicate).ToList();
            if (lstKeHoachTongThe.Any())
            {
                var result = lstKeHoachTongThe.Select(x =>
                {
                    ComboboxItem cb = new ComboboxItem();
                    if (x.INamKeHoach.HasValue)
                    {
                        cb.DisplayItem = "KHTT " + x.INamKeHoach.Value + "- Số KH: " + x.SSoKeHoachBqp;
                        cb.ValueItem = x.Id.ToString();
                    }
                    else
                    {
                        cb.DisplayItem = "KHTT " + x.IGiaiDoanTu_BQP.GetValueOrDefault() + "-" + x.IGiaiDoanDen_BQP.GetValueOrDefault() + "- Số KH: " + x.SSoKeHoachBqp;
                        cb.ValueItem = x.Id.ToString();
                    }
                    return cb;
                }).ToList();
                _itemsKeHoachTongThe = new ObservableCollection<ComboboxItem>(result);
            }
            else
            {
                _itemsKeHoachTongThe = new ObservableCollection<ComboboxItem>();
            }
        }

        private void LoadChuDauTu()
        {
            var predicate = PredicateBuilder.True<DmChuDauTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            var lstChuDauTu = _iDmChuDauTuService.FindByCondition(predicate).ToList();

            var cbxChuDauTu = lstChuDauTu.Select(n => new ComboboxItem()
            {
                ValueItem = n.Id.ToString(),
                DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.STenDonVi),
                HiddenValue = n.Id.ToString()
            });
            _itemsChuDauTu = new ObservableCollection<ComboboxItem>(cbxChuDauTu);
        }

        private void LoadNhiemVuChi()
        {
            if (_selectedKeHoachTongThe != null && _selectedDonVi != null)
            {
                //var lstIdKhTongTheNvChiCoHopDong = GetListIdKhTongTheNhiemVuChiCoHopDong();
                var lstKHNhiemVuChi = GetListIdKhTongTheNhiemVuChi();
                var lstNhiemVuChi = _iDmNhiemVuChiService.FindAll().Where(x => lstKHNhiemVuChi.Contains(x.Id)).ToList();

                var cbxNhiemVuChi = lstNhiemVuChi.Select(n => new ComboboxItem()
                {
                    ValueItem = n.Id.ToString(),
                    DisplayItem = string.Format("{0}", n.STenNhiemVuChi),
                    HiddenValue = n.Id.ToString()
                });

                ItemsNhiemVuChi = new ObservableCollection<ComboboxItem>(cbxNhiemVuChi);
            }
        }

        private List<Guid> GetListIdKhTongTheNhiemVuChi()
        {
            var predicate = PredicateBuilder.True<NhKhTongTheNhiemVuChi>();
            predicate = predicate.And(x => x.IIdKhTongTheId.ToString().Equals(_selectedKeHoachTongThe.ValueItem));
            predicate = predicate.And(x => x.IIdDonViThuHuongId.Equals(_selectedDonVi.Id));
            var lstKHNhiemVuChi = _iNhKhTongTheService.FindKHTongTheNVCByConditon(predicate);
            if (lstKHNhiemVuChi.Any())
            {
                return lstKHNhiemVuChi.Select(x => x.IIdNhiemVuChiId).ToList();
            }
            return new List<Guid>();
        }

        private void LoadLoaiDeNghi()
        {
            var results = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.CAP_KINH_PHI), ((int)NhLoaiDeNghi.Type.CAP_KINH_PHI).ToString()),
                new ComboboxItem(NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.TAM_UNG_KINH_PHI), ((int)NhLoaiDeNghi.Type.TAM_UNG_KINH_PHI).ToString()),
                new ComboboxItem(NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.THANH_TOAN), ((int)NhLoaiDeNghi.Type.THANH_TOAN).ToString()),
                new ComboboxItem(NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.TAM_UNG_THEO_CHE_DO), ((int)NhLoaiDeNghi.Type.TAM_UNG_THEO_CHE_DO).ToString())

            };
            _itemsLoaiDeNghi = results;
        }

        private void LoadLoaiNoiDungChi()
        {
            var results = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(LoaiNoiDungChi.Get((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE), ((int)LoaiNoiDungChi.Type.CHI_BANG_NGOAI_TE).ToString()),
                new ComboboxItem(LoaiNoiDungChi.Get((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE), ((int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE).ToString())
            };
            _itemsLoaiNoiDungChi = results;
            _selectedLoaiNoiDungChi = _itemsLoaiNoiDungChi[1];
        }

        private void LoadCoQuanThanhToan()
        {
            var results = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(NhCoQuanThanhToan.Get((int)NhCoQuanThanhToan.Type.CTC_CAP), ((int)NhCoQuanThanhToan.Type.CTC_CAP).ToString()),
                new ComboboxItem(NhCoQuanThanhToan.Get((int)NhCoQuanThanhToan.Type.DON_VI_CAP), ((int)NhCoQuanThanhToan.Type.DON_VI_CAP).ToString())
            };
            _itemsCoQuanThanhToan = results;
            _selectedCoQuanThanhToan = _itemsCoQuanThanhToan[1];
        }

        private void LoadThanhToanTheo()
        {
            var results = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(NHThanhToanTheo.Get((int)NHThanhToanTheo.Type.CHI_THEO_HOP_DONG), ((int)NHThanhToanTheo.Type.CHI_THEO_HOP_DONG).ToString()),
                new ComboboxItem(NHThanhToanTheo.Get((int)NHThanhToanTheo.Type.CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG), ((int)NHThanhToanTheo.Type.CHI_THEO_DU_AN_KHONG_HINH_THANH_HOP_DONG).ToString()),
                new ComboboxItem(NHThanhToanTheo.Get((int)NHThanhToanTheo.Type.CHI_KHONG_THEO_DU_AN_HOP_DONG), ((int)NHThanhToanTheo.Type.CHI_KHONG_THEO_DU_AN_HOP_DONG).ToString())
            };

            _itemsThanhToanTheo = results;
            _selectedThanhToanTheo = _itemsThanhToanTheo[0];
        }

        private void OnUploadFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Chọn file excel",
                RestoreDirectory = true,
                DefaultExt = StringUtils.EXCEL_EXTENSION
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FilePath = openFileDialog.FileName;
            _fileName = openFileDialog.SafeFileName;
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
           
        }

        private void OnProcessFile()
        {
            _lstErrChungTuChiTiet = new List<ImportErrorItem>();
            List<string> lstError = new List<string>();

            if (string.IsNullOrEmpty(FilePath))
            {
                lstError.Add(Resources.ErrorFileEmpty);
            }

            ;
            if (!ValidateForm()) return;

            var data = GetDataImportByFileType();
           
            
            ItemsThanhToan = new ObservableCollection<ForexDeNghiThanhToanImportModel>(data);
            if (lstError.Any())
            {
                string sMessError = string.Join(Environment.NewLine, lstError);
                System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            ValidateChungTuChiTiet();
            OnPropertyChanged(nameof(ItemsThanhToan));
        }

        private void OnResetData()
        {
            FilePath = string.Empty;
            IsSelectedFile = false;
            ItemsThanhToan = null;
            SelectedThanhToan = null;
            SelectedDonVi = null;
            SelectedChuDauTu = null;
            SelectedCoQuanThanhToan = null;

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(ItemsThanhToan));
            OnPropertyChanged(nameof(SelectedThanhToan));
            OnPropertyChanged(nameof(SelectedChuDauTu));
            OnPropertyChanged(nameof(SelectedCoQuanThanhToan));
        }

        private void ShowError(object param)
        {
            var importTabIndex = (ImportTabIndex)((int)param);
            int rowIndex;
            rowIndex = ItemsThanhToan.IndexOf(SelectedThanhToan);
            var errors = _lstErrChungTuChiTiet.Where(x => x.Row == rowIndex).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
            System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public override void OnSave()
        {
            List<string> lstError = new List<string>();
            if (!ValidateForm()) return;
            if(IsSaveData)
            {
                //Lưu đề nghị thanh toán
                NhTtThanhToan thanhtoan = new NhTtThanhToan();
                thanhtoan.IIdDonVi = Guid.Parse(SelectedDonVi.ValueItem);
                thanhtoan.IIdChuDauTuId = Guid.Parse(SelectedChuDauTu.ValueItem);
                thanhtoan.IIdKhtongTheId = Guid.Parse(SelectedKeHoachTongThe.ValueItem);
                thanhtoan.IIdNhiemVuChiId = Guid.Parse(SelectedNhiemVuChi.ValueItem);
                thanhtoan.IThanhToanTheo = int.Parse(SelectedThanhToanTheo.ValueItem);
                thanhtoan.ICoQuanThanhToan = int.Parse(SelectedCoQuanThanhToan.ValueItem);
                thanhtoan.SSoDeNghi = _sSoDeNghi;
                thanhtoan.DNgayDeNghi = _dNgayDeNghi;
                thanhtoan.DNgayTao = DateTime.Now;
                thanhtoan.BIsKhoa = false;
                thanhtoan.BIsXoa = false;
                thanhtoan.ITrangThai = 1;
                thanhtoan.INamKeHoach = _sessionService.Current.YearOfWork;
                thanhtoan.SNguoiTao = _sessionService.Current.Principal;

                //Đề nghị thanh toán chi tiết
                List<NhTtThanhToanChiTiet> lstChiTiet = new List<NhTtThanhToanChiTiet>();
                foreach (var item in ItemsThanhToan.Where(x => x.ImportStatus))
                {
                    NhTtThanhToanChiTiet thanhtoanchitiet = new NhTtThanhToanChiTiet();
                    thanhtoanchitiet.IIdMlnsId = item.IIDMLNS;
                    thanhtoanchitiet.FDeNghiCapKyNayUsd = Double.Parse(item.FGiaTriDeNghiUSD);
                    thanhtoanchitiet.FDeNghiCapKyNayVnd = Double.Parse(item.FGiaTriDeNghiVND);
                    thanhtoanchitiet.FPheDuyetCapKyNayUsd = Double.Parse(item.FGiaTriDuocDuyetUSD);
                    thanhtoanchitiet.FPheDuyetCapKyNayVnd = Double.Parse(item.FGiaTriDuocDuyetVND);
                    thanhtoanchitiet.IsAdded = true;
                    thanhtoanchitiet.IIdMucLucNganSachId = item.IIDMLNS;
                    lstChiTiet.Add(thanhtoanchitiet);
                }

                thanhtoan.FTongPheDuyetUSD = lstChiTiet.Select(x => x.FPheDuyetCapKyNayUsd).Sum();
                thanhtoan.FTongPheDuyetVND = lstChiTiet.Select(x => x.FPheDuyetCapKyNayVnd).Sum();
                thanhtoan.FTongDeNghiUSD = lstChiTiet.Select(x => x.FDeNghiCapKyNayUsd).Sum();
                thanhtoan.FTongDeNghiVND = lstChiTiet.Select(x => x.FDeNghiCapKyNayVnd).Sum();

                thanhtoan.NhTtThanhToanChiTiets = new ObservableCollection<NhTtThanhToanChiTiet>(lstChiTiet);

                _iNhTtThanhToanService.Add(thanhtoan);


                //_service.InsertRange(lstData, _sessionService.Current.Principal);
                //_vdtKhvPhanBoVonService.AddRange(lstDataKHV);
                System.Windows.MessageBox.Show(Resources.MsgSaveDone);
                SavedAction.Invoke(null);
            }

        }
        #endregion

        #region Helper
        private bool ValidateForm()
        {
            List<string> lstError = new List<string>();
            if (SelectedDonVi == null)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "đơn vị"));
            if (SelectedKeHoachTongThe == null)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "kế hoạch tổng thể BQP"));
            //if (SelectedNhiemVuChi == null)
            //   lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "nhiệm vụ chi"));
            if (SelectedCoQuanThanhToan == null)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "cơ quan thanh toán"));
            if (SelectedChuDauTu == null)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "chủ đầu tư"));
            if (_dNgayDeNghi == null)
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "ngày đề nghị"));
            if (_sSoDeNghi == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "số đề nghị"));
            }
            else
            {
                var predicate = PredicateBuilder.True<NhTtThanhToan>();
                predicate = predicate.And(x => x.SSoDeNghi == _sSoDeNghi);

                var thanhtoan = _iNhTtThanhToanService.FindByCondition(predicate).FirstOrDefault();
                if (thanhtoan != null)
                {
                    lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "số đề nghị đã tồn tại"));
                }
            }
            if (lstError.Count != 0)
            {
                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, lstError), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            
            return true;
        }

        private void ValidateChungTuChiTiet()
        {
            var mulucngansach = _iNsMucLucNganSachService.FindMLNSByNamLamViec(_sessionService.Current.YearOfWork).ToList();
            var itemsTable = ItemsThanhToan.Where(x => !string.IsNullOrEmpty(x.SNoiDungChi) && !string.IsNullOrEmpty(x.FGiaTriDuocDuyetUSD) &&
                !string.IsNullOrEmpty(x.FGiaTriDuocDuyetVND) && !string.IsNullOrEmpty(x.FLuyKeUSD) && !string.IsNullOrEmpty(x.FLuyKeVND) &&
                !string.IsNullOrEmpty(x.FGiaTriDeNghiUSD) && !string.IsNullOrEmpty(x.FGiaTriDeNghiVND));
            ItemsThanhToan = new ObservableCollection<ForexDeNghiThanhToanImportModel>(itemsTable);
            foreach (var item in ItemsThanhToan)
            {
                var muclucngansach = mulucngansach.FirstOrDefault(x => x.XauNoiMa.Equals(item.SXauNoiMa));
                if (muclucngansach == null)
                {
                    item.IsErrorMLNS = true;
                    item.ImportStatus = false;
                }
                else
                {
                    item.IIDMLNS = muclucngansach.Id;
                }
            }

            OnPropertyChanged(nameof(ItemsThanhToan));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private List<ForexDeNghiThanhToanImportModel> GetDataImportByFileType()
        {
            XlsFile xls = new XlsFile(false);
            xls.Open(FilePath);
            xls.ActiveSheet = 1;

            var lstResults = _importService.ProcessData<ForexDeNghiThanhToanImportModel>(FilePath);
            if (lstResults.ImportErrors.Any())
            {
                _lstErrChungTuChiTiet.AddRange(lstResults.ImportErrors);
            }
            return _mapper.Map<List<ForexDeNghiThanhToanImportModel>>(lstResults.Data);
        }
      

      
        #endregion
    }
}
