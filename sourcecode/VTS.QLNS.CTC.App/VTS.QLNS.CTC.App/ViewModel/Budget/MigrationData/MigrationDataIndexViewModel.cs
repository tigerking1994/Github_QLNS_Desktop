using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.MigrationData;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.MigrationData
{
    public class MigrationDataIndexViewModel : ViewModelBase
    {
        private readonly IMigrationDataService _migrationDataService;
        private readonly ISktSoLieuChungTuRepository _sktSoLieuChungTuRepository;
        private readonly ISktMucLucRepository _sktMucLucRepository;
        private readonly ISktChungTuRepository _sktChungTuRepository;
        private readonly IMapper _mapper;
        private readonly IDanhMucService _dmService;
        private readonly ICloneDataYearOfWork _cloneDataYearOfWorkService;
        private readonly ISessionService _sessionService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private readonly ILog _logger;

        //public override string FuncCode => NSFunctionCode.SYSTEM_USER_MANAGEMENT_USER_INDEX;
        public override string Name => "Tích hợp dữ liệu";
        public override string Description => "Tích hợp dữ liệu";
        public override Type ContentType => typeof(MigrationDataIndex);
        public override PackIconKind IconKind => PackIconKind.Database;

        private ObservableCollection<NsBkChungTu> nsBkChungTus { get; set; }
        private ObservableCollection<NsBkChungTuChiTiet> nsBkChungTuChiTiets { get; set; }
        private ObservableCollection<NsCpChungTu> nsCpChungTus { get; set; }
        private ObservableCollection<NsCpChungTuChiTiet> nsCpChungTuChiTiets { get; set; }
        private ObservableCollection<DanhMuc> danhMucs { get; set; }
        private ObservableCollection<DmChuKy> dmChuKys { get; set; }
        private ObservableCollection<NsDtChungTu> nsDtChungTus { get; set; }
        private ObservableCollection<NsDtChungTuChiTiet> nsDtChungTuChiTiets { get; set; }
        private ObservableCollection<DonVi> donVis { get; set; }
        private ObservableCollection<NsMucLucNganSach> nsMucLucNganSaches { get; set; }
        private ObservableCollection<NsQsChungTu> nsQsChungTus { get; set; }
        private ObservableCollection<NsQsChungTuChiTiet> nsQsChungTuChiTiets { get; set; }
        private ObservableCollection<NsQsMucLuc> nsQsMucLucs { get; set; }
        private ObservableCollection<NsQtChungTu> nsQtChungTus { get; set; }
        private ObservableCollection<NsQtChungTuChiTiet> nsQtChungTuChiTiets { get; set; }
        private ObservableCollection<NsQtChungTuChiTietGiaiThich> nsQtChungTuChiTietGiaiThiches { get; set; }
        private ObservableCollection<NsQtChungTuChiTietGiaiThichLuongTru> nsQtChungTuChiTietGiaiThichLuongTrus { get; set; }
        private ObservableCollection<NsSktChungTu> nsSktChungTus { get; set; }
        private ObservableCollection<NsSktChungTuChiTiet> nsSktChungTuChiTiets { get; set; }
        private ObservableCollection<NsSktMucLuc> nsSktMucLucs { get; set; }
        private ObservableCollection<NsMlsktMlns> nsMlsktMlns { get; set; }
        private ObservableCollection<NsDtdauNamChungTuChiTiet> nsDtdauNamChungTuChiTiets { get; set; }
        private List<NsMucLucNganSach> addedListMLns { get; set; }
        private List<NsDtdauNamChungTu> savedLstNsDtdauNamChungTu { get; set; }
        private List<NsDtdauNamChungTuChiTiet> savedLstNsDtdauNamChungTuChiTiet { get; set; }

        public ObservableCollection<ComboboxItem> TableList { get; set; }
        public ObservableCollection<object> DisplayData { get; set; }
        public ObservableCollection<ComboboxItem> NamLamViec { get; set; }
        public ObservableCollection<ComboboxItem> NamNganSach { get; set; }
        public ObservableCollection<ComboboxItem> NguonNganSach { get; set; }
        public List<DmChuKy> CurrentDanhMucBaoCao { get; set; }
        private List<DanhMuc> DanhMucChuKyTen { get; set; }
        private List<DanhMuc> DanhmucChuKyChucDanh { get; set; }

        private bool _isShowingDuToan;
        public bool IsShowingDuToan
        {
            get => _isShowingDuToan;
            set => SetProperty(ref _isShowingDuToan, value);
        }

        private int? _selectedNamLamViec;
        public int? SelectedNamLamViec
        {
            get => _selectedNamLamViec;
            set
            {
                SetProperty(ref _selectedNamLamViec, value);
                if (_nsDtChungTusLoaiNhanView != null)
                {
                    _nsDtChungTusLoaiNhanView.Refresh();
                }
                if (_nsDtChungTusLoaiPhanBoView != null)
                {
                    _nsDtChungTusLoaiPhanBoView.Refresh();
                }
            }
        }

        private int? _selectedNamNganSach;
        public int? SelectedNamNganSach
        {
            get => _selectedNamNganSach;
            set
            {
                SetProperty(ref _selectedNamNganSach, value);
                if (_nsDtChungTusLoaiNhanView != null)
                {
                    _nsDtChungTusLoaiNhanView.Refresh();
                }
                if (_nsDtChungTusLoaiPhanBoView != null)
                {
                    _nsDtChungTusLoaiPhanBoView.Refresh();
                }
            }
        }

        private int? _selectedNguonNganSach;
        public int? SelectedNguonNganSach
        {
            get => _selectedNguonNganSach;
            set
            {
                SetProperty(ref _selectedNguonNganSach, value);
                if (_nsDtChungTusLoaiNhanView != null)
                {
                    _nsDtChungTusLoaiNhanView.Refresh();
                }
                if (_nsDtChungTusLoaiPhanBoView != null)
                {
                    _nsDtChungTusLoaiPhanBoView.Refresh();
                }
            }
        }

        private DtChungTuModel _selectedDtChungTuModel;
        public DtChungTuModel SelectedDtChungTuModel
        {
            get => _selectedDtChungTuModel;
            set => SetProperty(ref _selectedDtChungTuModel, value);
        }

        public ObservableCollection<DtChungTuModel> NsDtChungTusLoaiNhan { get; set; }
        public ObservableCollection<DtChungTuModel> NsDtChungTusLoaiPhanBo { get; set; }

        private ICollectionView _nsDtChungTusLoaiNhanView;
        private ICollectionView _nsDtChungTusLoaiPhanBoView;

        public string FilePath { get; set; }

        private string _selectedTableName;
        public string SelectedTableName
        {
            get => _selectedTableName;
            set
            {
                bool isUpdate = SetProperty(ref _selectedTableName, value);
                if (isUpdate)
                {
                    LoadSelectedTable();
                }
            }
        }

        public RelayCommand OpenDialogFileCommand { get; set; }
        public RelayCommand MoveUpCommand { get; set; }
        public RelayCommand MoveDownCommand { get; set; }
        private List<NsDtNhanPhanBoMap> NsDtNhanPhanBoMap { get; set; }

        public MigrationDataIndexViewModel(
            IMigrationDataService migrationDataService,
            ISktSoLieuChungTuRepository sktSoLieuChungTuRepository,
            IMapper mapper,
            IDanhMucService danhMucService,
            ILog logger,
            ICloneDataYearOfWork cloneDataYearOfWork,
            ISktMucLucRepository sktMucLucRepository,
            ISktChungTuRepository sktChungTuRepository,
            ISessionService sessionService,
            IDanhMucService danhmucService,
            IDmChuKyService dmChuKyService)
        {
            _migrationDataService = migrationDataService;
            _sktSoLieuChungTuRepository = sktSoLieuChungTuRepository;
            _dmService = danhMucService;
            _cloneDataYearOfWorkService = cloneDataYearOfWork;
            _sktMucLucRepository = sktMucLucRepository;
            _sktChungTuRepository = sktChungTuRepository;
            _sessionService = sessionService;
            _dmChuKyService = dmChuKyService;
            _danhMucService = danhMucService;
            _mapper = mapper;
            OpenDialogFileCommand = new RelayCommand(obj => OnUploadFile(), obj => !IsLoading);
            /* NsDtChungTusLoaiNhan = new ObservableCollection<DtChungTuModel>();
             NsDtChungTusLoaiPhanBo = new ObservableCollection<DtChungTuModel>();*/
            MoveUpCommand = new RelayCommand(obj => MoveUp(), o => SelectedDtChungTuModel != null && NsDtChungTusLoaiPhanBo.Any(t => t.IsChecked));
            MoveDownCommand = new RelayCommand(obj => MoveDown(), o => NsDtChungTusLoaiNhan.Any(t => t.IsChecked));
            _logger = logger;
        }

        public override void Init()
        {
            LoadTableList();
            nsBkChungTus = new ObservableCollection<NsBkChungTu>();
            nsBkChungTuChiTiets = new ObservableCollection<NsBkChungTuChiTiet>();
            nsCpChungTus = new ObservableCollection<NsCpChungTu>();
            nsCpChungTuChiTiets = new ObservableCollection<NsCpChungTuChiTiet>();
            danhMucs = new ObservableCollection<DanhMuc>();
            dmChuKys = new ObservableCollection<DmChuKy>();
            nsDtChungTus = new ObservableCollection<NsDtChungTu>();
            nsDtChungTuChiTiets = new ObservableCollection<NsDtChungTuChiTiet>();
            donVis = new ObservableCollection<DonVi>();
            nsMucLucNganSaches = new ObservableCollection<NsMucLucNganSach>();
            nsQsChungTus = new ObservableCollection<NsQsChungTu>();
            nsQsChungTuChiTiets = new ObservableCollection<NsQsChungTuChiTiet>();
            nsQsMucLucs = new ObservableCollection<NsQsMucLuc>();
            nsQtChungTus = new ObservableCollection<NsQtChungTu>();
            nsQtChungTuChiTiets = new ObservableCollection<NsQtChungTuChiTiet>();
            nsQtChungTuChiTietGiaiThiches = new ObservableCollection<NsQtChungTuChiTietGiaiThich>();
            nsQtChungTuChiTietGiaiThichLuongTrus = new ObservableCollection<NsQtChungTuChiTietGiaiThichLuongTru>();
            nsSktChungTus = new ObservableCollection<NsSktChungTu>();
            nsSktChungTuChiTiets = new ObservableCollection<NsSktChungTuChiTiet>();
            nsSktMucLucs = new ObservableCollection<NsSktMucLuc>();
            nsMlsktMlns = new ObservableCollection<NsMlsktMlns>();
            addedListMLns = new List<NsMucLucNganSach>();
            CurrentDanhMucBaoCao = new List<DmChuKy>();
            DanhMucChuKyTen = new List<DanhMuc>();
            DanhmucChuKyChucDanh = new List<DanhMuc>();

            NsDtChungTusLoaiNhan = new ObservableCollection<DtChungTuModel>();
            _nsDtChungTusLoaiNhanView = CollectionViewSource.GetDefaultView(NsDtChungTusLoaiNhan);
            _nsDtChungTusLoaiNhanView.Filter = DtChungTuFilter;
            OnPropertyChanged(nameof(NsDtChungTusLoaiNhan));

            NsDtChungTusLoaiPhanBo = new ObservableCollection<DtChungTuModel>();
            _nsDtChungTusLoaiPhanBoView = CollectionViewSource.GetDefaultView(NsDtChungTusLoaiPhanBo);
            _nsDtChungTusLoaiPhanBoView.Filter = DtChungTuFilter;
            OnPropertyChanged(nameof(NsDtChungTusLoaiPhanBo));

            SelectedTableName = TableList.First().DisplayItem;
            OnPropertyChanged("DisplayData");
        }

        private void LoadTableList()
        {
            TableList = new ObservableCollection<ComboboxItem>();
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.BK_CHUNGTU, ValueItem = IMPORT_TABLE_NAME.BK_CHUNGTU });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.BK_CHUNGTUCHITIET, ValueItem = IMPORT_TABLE_NAME.BK_CHUNGTUCHITIET });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.CP_CHUNGTU, ValueItem = IMPORT_TABLE_NAME.CP_CHUNGTU });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.CP_CHUNGTUCHITIET, ValueItem = IMPORT_TABLE_NAME.CP_CHUNGTUCHITIET });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.DANHMUC, ValueItem = IMPORT_TABLE_NAME.DANHMUC });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.DM_CHUKY, ValueItem = IMPORT_TABLE_NAME.DM_CHUKY });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.DT_CHUNGTU, ValueItem = IMPORT_TABLE_NAME.DT_CHUNGTU });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.DT_CHUNGTUCHITIET, ValueItem = IMPORT_TABLE_NAME.DT_CHUNGTUCHITIET });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.NS_DONVI, ValueItem = IMPORT_TABLE_NAME.NS_DONVI });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.NS_MUCLUCNGANSACH, ValueItem = IMPORT_TABLE_NAME.NS_MUCLUCNGANSACH });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.QS_CHUNGTU, ValueItem = IMPORT_TABLE_NAME.QS_CHUNGTU });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.QS_CHUNGTUCHITIET, ValueItem = IMPORT_TABLE_NAME.QS_CHUNGTUCHITIET });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.QS_MUCLUC, ValueItem = IMPORT_TABLE_NAME.QS_MUCLUC });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.QT_CHUNGTU, ValueItem = IMPORT_TABLE_NAME.QT_CHUNGTU });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.QT_CHUNGTUCHITIET, ValueItem = IMPORT_TABLE_NAME.QT_CHUNGTUCHITIET });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.QT_CHUNGTUCHITIET_GIAITHICH, ValueItem = IMPORT_TABLE_NAME.QT_CHUNGTUCHITIET_GIAITHICH });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.QT_CHUNGTUCHITIET_GIAITHICH_LUONGTRU, ValueItem = IMPORT_TABLE_NAME.QT_CHUNGTUCHITIET_GIAITHICH_LUONGTRU });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.SKT_CHUNGTU, ValueItem = IMPORT_TABLE_NAME.SKT_CHUNGTU });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.SKT_CHUNGTU_CHITIET, ValueItem = IMPORT_TABLE_NAME.SKT_CHUNGTU_CHITIET });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.SKT_MUCLUC, ValueItem = IMPORT_TABLE_NAME.SKT_MUCLUC });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.SKT_MUCLUC_MAP, ValueItem = IMPORT_TABLE_NAME.SKT_MUCLUC_MAP });
            TableList.Add(new ComboboxItem { DisplayItem = IMPORT_TABLE_NAME.SKT_SO_LIEU_CHITIET, ValueItem = IMPORT_TABLE_NAME.SKT_SO_LIEU_CHITIET });
        }

        private void LoadFullData()
        {
            string filePath = this.FilePath;
            var danhMucNamLamViecExcept = _danhMucService.FindByCode("MIGRATE_EXCEPT", _sessionService.Current.YearOfWork);
            int iNamLamViecExcept = 0;
            if (danhMucNamLamViecExcept != null && danhMucNamLamViecExcept.SGiaTri != null)
            {
                iNamLamViecExcept = int.Parse(danhMucNamLamViecExcept.SGiaTri);
            }
            nsBkChungTus = new ObservableCollection<NsBkChungTu>(_migrationDataService.GetListNsBkChungTu(filePath, iNamLamViecExcept));
            nsBkChungTuChiTiets = new ObservableCollection<NsBkChungTuChiTiet>(_migrationDataService.GetListNsBkChungTuChiTiet(filePath, iNamLamViecExcept));
            nsCpChungTus = new ObservableCollection<NsCpChungTu>(_migrationDataService.GetListNsCpChungTu(filePath, iNamLamViecExcept));
            nsCpChungTuChiTiets = new ObservableCollection<NsCpChungTuChiTiet>(_migrationDataService.GetListNsCpChungTuChiTiet(filePath, iNamLamViecExcept));
            danhMucs = new ObservableCollection<DanhMuc>(_migrationDataService.GetListDanhMuc(filePath, iNamLamViecExcept));
            dmChuKys = new ObservableCollection<DmChuKy>(_migrationDataService.GetListDanhMucChuKy(filePath, iNamLamViecExcept));
            nsDtChungTus = new ObservableCollection<NsDtChungTu>(_migrationDataService.GetListDtChungTu(filePath, iNamLamViecExcept));
            nsDtChungTuChiTiets = new ObservableCollection<NsDtChungTuChiTiet>(_migrationDataService.GetListDtChungTuChiTiet(filePath, iNamLamViecExcept));
            donVis = new ObservableCollection<DonVi>(_migrationDataService.GetListDonVi(filePath, iNamLamViecExcept));
            nsMucLucNganSaches = new ObservableCollection<NsMucLucNganSach>(_migrationDataService.GetListMlns(filePath, iNamLamViecExcept));
            nsQsChungTus = new ObservableCollection<NsQsChungTu>(_migrationDataService.GetListNsQsChungTu(filePath, iNamLamViecExcept));
            nsQsChungTuChiTiets = new ObservableCollection<NsQsChungTuChiTiet>(_migrationDataService.GetListNsQsChungTuChiTiet(filePath, iNamLamViecExcept));
            nsQsMucLucs = new ObservableCollection<NsQsMucLuc>(_migrationDataService.GetListNsQsMucluc(filePath, iNamLamViecExcept));
            nsQtChungTus = new ObservableCollection<NsQtChungTu>(_migrationDataService.GetListNsQtChungTu(filePath, iNamLamViecExcept));
            nsQtChungTuChiTiets = new ObservableCollection<NsQtChungTuChiTiet>(_migrationDataService.GetListNsQtChungTuChiTiet(filePath, iNamLamViecExcept));
            nsQtChungTuChiTietGiaiThiches = new ObservableCollection<NsQtChungTuChiTietGiaiThich>(_migrationDataService.GetListNsQtChungTuChiTietGiaiThich(filePath, iNamLamViecExcept));
            nsQtChungTuChiTietGiaiThichLuongTrus = new ObservableCollection<NsQtChungTuChiTietGiaiThichLuongTru>(_migrationDataService.GetListNsQtChungTuChiTietGiaiThichLuongTru(filePath, iNamLamViecExcept));
            nsSktChungTus = new ObservableCollection<NsSktChungTu>(_migrationDataService.GetListSktChungTu(filePath, iNamLamViecExcept));
            nsSktChungTuChiTiets = new ObservableCollection<NsSktChungTuChiTiet>(_migrationDataService.GetListSktChungTuChiTiets(filePath, iNamLamViecExcept));
            nsSktMucLucs = new ObservableCollection<NsSktMucLuc>(_migrationDataService.GetListSktMucLuc(filePath, iNamLamViecExcept));
            nsMlsktMlns = new ObservableCollection<NsMlsktMlns>(_migrationDataService.GetListMapMLNSSKT(filePath, iNamLamViecExcept));
            nsDtdauNamChungTuChiTiets = new ObservableCollection<NsDtdauNamChungTuChiTiet>(_migrationDataService.GetListDtDauNamCTCT(filePath, iNamLamViecExcept));
            DanhMucChuKyTen = _migrationDataService.GetListDanhmucChuKyTen(filePath, iNamLamViecExcept);
            DanhmucChuKyChucDanh = _migrationDataService.GetListDanhmucChuKyChucDanh(filePath, iNamLamViecExcept);
            CurrentDanhMucBaoCao = _dmChuKyService.FindByCondition(t => true).ToList();

            LoadFilterChungTu();

            ObservableCollection<DtChungTuModel> dtChungTuModels = _mapper.Map<ObservableCollection<DtChungTuModel>>(nsDtChungTus);

            NsDtChungTusLoaiNhan.Clear();
            NsDtChungTusLoaiNhan = new ObservableCollection<DtChungTuModel>();
            NsDtChungTusLoaiNhan = _mapper.Map<ObservableCollection<DtChungTuModel>>(dtChungTuModels.Where(t => t.ILoai == 0).OrderBy(t => t.DNgayChungTu).ThenBy(t => t.ISoChungTuIndex));
            _nsDtChungTusLoaiNhanView = CollectionViewSource.GetDefaultView(NsDtChungTusLoaiNhan);
            _nsDtChungTusLoaiNhanView.Filter = DtChungTuFilter;
            foreach (var item in NsDtChungTusLoaiNhan)
            {
                item.IsAllowSelected = false;
            }
            OnPropertyChanged(nameof(NsDtChungTusLoaiNhan));

            NsDtChungTusLoaiPhanBo = new ObservableCollection<DtChungTuModel>();
            NsDtChungTusLoaiPhanBo = _mapper.Map<ObservableCollection<DtChungTuModel>>(dtChungTuModels.Where(t => t.ILoai == 1).OrderBy(t => t.DNgayChungTu).ThenBy(t => t.ISoChungTuIndex));
            _nsDtChungTusLoaiPhanBoView = CollectionViewSource.GetDefaultView(NsDtChungTusLoaiPhanBo);
            _nsDtChungTusLoaiPhanBoView.Filter = DtChungTuFilter;
            foreach (var item in NsDtChungTusLoaiPhanBo)
            {
                item.IsAllowSelected = true;
            }
            OnPropertyChanged(nameof(NsDtChungTusLoaiPhanBo));
        }

        private bool DtChungTuFilter(object obj)
        {
            DtChungTuModel dtChungTuModel = obj as DtChungTuModel;
            var result = true;
            if (SelectedNamLamViec.HasValue)
            {
                result = result && dtChungTuModel.INamLamViec == SelectedNamLamViec.Value;
            }
            if (SelectedNguonNganSach.HasValue)
            {
                result = result && dtChungTuModel.IIdMaNguonNganSach == SelectedNguonNganSach.Value;
            }
            if (SelectedNamNganSach.HasValue)
            {
                result = result && dtChungTuModel.INamNganSach == SelectedNamNganSach.Value;
            }
            return result;
        }

        private void LoadSelectedTable()
        {
            IsShowingDuToan = false;
            switch (SelectedTableName)
            {
                case IMPORT_TABLE_NAME.BK_CHUNGTU:
                    DisplayData = new ObservableCollection<object>(nsBkChungTus.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.BK_CHUNGTUCHITIET:
                    DisplayData = new ObservableCollection<object>(nsBkChungTuChiTiets.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.CP_CHUNGTU:
                    DisplayData = new ObservableCollection<object>(nsCpChungTus.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.CP_CHUNGTUCHITIET:
                    DisplayData = new ObservableCollection<object>(nsCpChungTuChiTiets.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.DANHMUC:
                    DisplayData = new ObservableCollection<object>(danhMucs.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.DM_CHUKY:
                    DisplayData = new ObservableCollection<object>(dmChuKys.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.DT_CHUNGTU:
                    IsShowingDuToan = true;
                    //DisplayData = new ObservableCollection<object>(nsDtChungTus.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.DT_CHUNGTUCHITIET:
                    DisplayData = new ObservableCollection<object>(nsDtChungTuChiTiets.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.NS_DONVI:
                    DisplayData = new ObservableCollection<object>(donVis.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.NS_MUCLUCNGANSACH:
                    DisplayData = new ObservableCollection<object>(nsMucLucNganSaches.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.QS_CHUNGTU:
                    DisplayData = new ObservableCollection<object>(nsQsChungTus.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.QS_CHUNGTUCHITIET:
                    DisplayData = new ObservableCollection<object>(nsQsChungTuChiTiets.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.QS_MUCLUC:
                    DisplayData = new ObservableCollection<object>(nsQsMucLucs.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.QT_CHUNGTU:
                    DisplayData = new ObservableCollection<object>(nsQtChungTus.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.QT_CHUNGTUCHITIET:
                    DisplayData = new ObservableCollection<object>(nsQtChungTuChiTiets.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.QT_CHUNGTUCHITIET_GIAITHICH:
                    DisplayData = new ObservableCollection<object>(nsQtChungTuChiTietGiaiThiches.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.QT_CHUNGTUCHITIET_GIAITHICH_LUONGTRU:
                    DisplayData = new ObservableCollection<object>(nsQtChungTuChiTietGiaiThichLuongTrus.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.SKT_CHUNGTU:
                    DisplayData = new ObservableCollection<object>(nsSktChungTus.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.SKT_CHUNGTU_CHITIET:
                    DisplayData = new ObservableCollection<object>(nsSktChungTuChiTiets.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.SKT_MUCLUC:
                    DisplayData = new ObservableCollection<object>(nsSktMucLucs.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.SKT_MUCLUC_MAP:
                    DisplayData = new ObservableCollection<object>(nsMlsktMlns.Cast<object>());
                    break;
                case IMPORT_TABLE_NAME.SKT_SO_LIEU_CHITIET:
                    DisplayData = new ObservableCollection<object>(nsDtdauNamChungTuChiTiets.Cast<object>());
                    break;
            }
            OnPropertyChanged(nameof(DisplayData));
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".mdf";
            openFileDialog.Filter = "DB Files|*.mdf;*.bak";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FilePath = openFileDialog.FileName;
            bool isBackUpFile = FilePath.ToLower().EndsWith(".bak");
            DatabaseInfo dbInfo = new DatabaseInfo();
            if (isBackUpFile)
            {
                dbInfo = _migrationDataService.RestoreDatabase(FilePath);
                FilePath = dbInfo.MdfFilePath;
            }
            try
            {
                LoadFullData();
                LoadSelectedTable();
                MessageBoxHelper.Info("Tải dữ liệu thành công");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                MessageBoxHelper.Warning("Có lỗi xảy ra");
            }
            finally
            {
                if (isBackUpFile)
                {
                    _migrationDataService.DropDatabase(dbInfo.Name);
                    File.Delete(dbInfo.MdfFilePath);
                    File.Delete(dbInfo.LogFilePath);
                }
                else
                {
                    _migrationDataService.DropDatabase(FilePath.ToUpper());
                }
            }
        }

        public override void OnSave()
        {
            string msgConfirm = "Bạn chắc chắn muốn lưu dữ liệu ?";
            MessageBoxResult dialogResult = System.Windows.MessageBox.Show(msgConfirm, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                if (!ValidateSave())
                    return;
                var isAddedMissingMlns = MessageBoxHelper.Confirm("Bạn có muốn bổ sung những mlns bị thiếu trong danh mục? ");
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    HandleDataBeforeSave(isAddedMissingMlns.Equals(MessageBoxResult.Yes));
                    _migrationDataService.SaveData(nsBkChungTus, nsBkChungTuChiTiets, nsCpChungTus, nsCpChungTuChiTiets, danhMucs, dmChuKys, nsDtChungTus,
                        nsDtChungTuChiTiets, donVis, addedListMLns, nsQsChungTus, nsQsChungTuChiTiets, nsQsMucLucs, nsQtChungTus, nsQtChungTuChiTiets,
                        nsQtChungTuChiTietGiaiThiches, nsQtChungTuChiTietGiaiThichLuongTrus, nsSktChungTus, nsSktChungTuChiTiets, nsSktMucLucs, nsMlsktMlns,
                        savedLstNsDtdauNamChungTu, savedLstNsDtdauNamChungTuChiTiet, NsDtNhanPhanBoMap, CurrentDanhMucBaoCao, DanhmucChuKyChucDanh, DanhMucChuKyTen);
                    MessageBoxHelper.Warning("Tích hợp dữ liệu thành công");

                }, (s, e) =>
                {
                    IsLoading = false;
                    if (e.Error != null)
                    {
                        MessageBoxHelper.Warning("Có lỗi xảy ra");
                        _logger.Error(e.Error.Message);
                    }
                });
            }
        }

        public override Func<object, bool> canExecute()
        {
            return x => !IsLoading;
        }

        private void LoadFilterChungTu()
        {
            NamNganSach = new ObservableCollection<ComboboxItem>();
            IEnumerable<DanhMuc> danhMucNamNS = danhMucs.Where(t => "NS_NamNganSach".Equals(t.SType));
            foreach (DanhMuc namNS in danhMucNamNS)
            {
                NamNganSach.Add(new ComboboxItem { DisplayItem = namNS.STen, ValueItem = namNS.IIDMaDanhMuc });
            }
            if (NamNganSach.Count() > 0)
            {
                SelectedNamNganSach = int.Parse(NamNganSach.First().ValueItem);
            }
            OnPropertyChanged("SelectedNamNganSach");
            OnPropertyChanged("NamNganSach");

            NguonNganSach = new ObservableCollection<ComboboxItem>();
            IEnumerable<DanhMuc> danhMucNguonNS = danhMucs.Where(t => "NS_NguonNganSach".Equals(t.SType));
            foreach (DanhMuc nguonNS in danhMucNguonNS)
            {
                NguonNganSach.Add(new ComboboxItem { DisplayItem = nguonNS.STen, ValueItem = nguonNS.IIDMaDanhMuc });
            }
            if (NguonNganSach.Count() > 0)
            {
                SelectedNguonNganSach = int.Parse(NguonNganSach.First().ValueItem);
            }
            OnPropertyChanged("NguonNganSach");
            OnPropertyChanged("SelectedNguonNganSach");

            NamLamViec = new ObservableCollection<ComboboxItem>();
            IEnumerable<DanhMuc> danhMucNamLamViec = danhMucs.Where(t => "NS_NamLamViec".Equals(t.SType));
            foreach (DanhMuc nam in danhMucNamLamViec)
            {
                NamLamViec.Add(new ComboboxItem { DisplayItem = nam.STen, ValueItem = nam.IIDMaDanhMuc });
            }
            if (NamLamViec.Count() > 0)
            {
                SelectedNamLamViec = int.Parse(NamLamViec.First().ValueItem);
            }
            OnPropertyChanged("NamLamViec");
            OnPropertyChanged("SelectedNamLamViec");
        }

        private void MoveUp()
        {
            if (SelectedDtChungTuModel != null)
            {
                var selectedIndex = NsDtChungTusLoaiNhan.IndexOf(SelectedDtChungTuModel);
                var selectedNsDtChungTusLoaiPhanBo = NsDtChungTusLoaiPhanBo.Where(t => t.IsChecked).ToList();
                foreach (var selectedItem in selectedNsDtChungTusLoaiPhanBo)
                {
                    selectedItem.IsChecked = false;
                    selectedItem.IIDChungTuNhan = SelectedDtChungTuModel.Id;
                    NsDtChungTusLoaiNhan.Insert(selectedIndex + 1, selectedItem);
                }
                foreach (var selectedItem in selectedNsDtChungTusLoaiPhanBo)
                {
                    NsDtChungTusLoaiPhanBo.Remove(selectedItem);
                }
                OnPropertyChanged(nameof(NsDtChungTusLoaiPhanBo));
                OnPropertyChanged(nameof(NsDtChungTusLoaiNhan));
            }
        }

        private void MoveDown()
        {
            var selectedNsDtChungTusLoaiNhan = NsDtChungTusLoaiNhan.Where(t => t.IsChecked).ToList();
            foreach (var selectedItem in selectedNsDtChungTusLoaiNhan)
            {
                NsDtChungTusLoaiNhan.Remove(selectedItem);
            }
            foreach (var selectedItem in selectedNsDtChungTusLoaiNhan)
            {
                selectedItem.IsChecked = false;
                selectedItem.IIDChungTuNhan = null;
                NsDtChungTusLoaiPhanBo.Add(selectedItem);
            }
            NsDtChungTusLoaiPhanBo = new ObservableCollection<DtChungTuModel>(NsDtChungTusLoaiPhanBo.OrderBy(t => t.DNgayChungTu).ThenBy(t => t.ISoChungTuIndex));
            _nsDtChungTusLoaiPhanBoView = CollectionViewSource.GetDefaultView(NsDtChungTusLoaiPhanBo);
            _nsDtChungTusLoaiPhanBoView.Filter = DtChungTuFilter;
            //_nsDtChungTusLoaiPhanBoView.Refresh();
            OnPropertyChanged(nameof(NsDtChungTusLoaiPhanBo));
            OnPropertyChanged(nameof(NsDtChungTusLoaiNhan));
        }

        private bool ValidateSave()
        {
            if (nsDtChungTus.Any(t => t.ILoai == 1) && NsDtChungTusLoaiPhanBo.Count() > 0)
            {
                var DtChungTuGroupBY = NsDtChungTusLoaiPhanBo.Select(t => new { t.INamLamViec, t.IIdMaNguonNganSach, t.INamNganSach }).Distinct();
                var msg = "";
                foreach (var i in DtChungTuGroupBY)
                {
                    string nguonNganSach = NguonNganSach.First(t => t.ValueItem.Equals(i.IIdMaNguonNganSach.ToString())).DisplayItem;
                    string namNganSach = NamNganSach.First(t => t.ValueItem.Equals(i.INamNganSach.ToString())).DisplayItem;
                    msg += string.Format("Chứng từ thuộc năm làm việc {0}, năm ngân sách {1},  nguồn ngân sách {2} chưa được phân bổ", i.INamLamViec, namNganSach, nguonNganSach) + Environment.NewLine;
                }
                MessageBoxResult rs = MessageBoxHelper.Confirm(msg + "Bạn có muốn tiếp tục?");
                return rs.Equals(MessageBoxResult.Yes);
            }
            return true;
        }

        private void HandleDataBeforeSave(bool isAddedMissingMlns)
        {
            // handle data danh muc
            var existDanhMuc = _dmService.FindByCondition(t => true).Select(t => new { t.IIDMaDanhMuc, t.SType });

            // nếu năm làm việc đã tồn tại ở hệ thống cũ, xóa mlskt ở ht mới có nvm = nvm hệ thống cũ
            var copiedNamLamViec = danhMucs.Where(t => t.SType.Equals("NS_NamLamViec")).Select(t => t.IIDMaDanhMuc);
            var dsNamLamViec = danhMucs.Where(t => t.SType.Equals("NS_NamLamViec")).ToList();
            _sktMucLucRepository.DeleteSktByNamLamViec(copiedNamLamViec.Select(t => int.Parse(t)));
            // handle skt muc luc
            foreach (var item in nsSktMucLucs)
            {
                item.SLoaiNhap = "1,2";
                IEnumerable<NsSktMucLuc> possibleParentList = nsSktMucLucs.Where(i => i.INamLamViec == item.INamLamViec && !item.SKyHieu.Equals(i.SKyHieu) && item.SKyHieu.StartsWith(i.SKyHieu));
                if (possibleParentList.Count() > 0)
                {
                    item.IIDMLSKTCha = possibleParentList.Last().IIDMLSKT;
                }
            }
            // chỉ copy danh mục chưa tồn tại (thei iid và type) trong hệ thống
            danhMucs = new ObservableCollection<DanhMuc>(danhMucs.Where(t =>
            {
                var dm = new { t.IIDMaDanhMuc, t.SType };
                return !existDanhMuc.Contains(dm);
            }));
            // handle data mlns
            // get nam lam viec dau tien cua he thong hien tai
            var namLamViec = existDanhMuc.FirstOrDefault(t => t.SType.Equals("NS_NamLamViec"));
            // tao data cua nam lam viec chua ton tai trong he thong
            AuthenticationInfo authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionService.Current);
            foreach (var nam in dsNamLamViec)
            {
                //_cloneDataYearOfWorkService.CloneData(int.Parse(namLamViec.IIDMaDanhMuc), int.Parse(nam.IIDMaDanhMuc), authenticationInfo, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1);
                //if (!nam.IIDMaDanhMuc.IsEmpty() && int.Parse(nam.IIDMaDanhMuc) < 2022)
                if (!nam.IIDMaDanhMuc.IsEmpty())
                {
                    _cloneDataYearOfWorkService.CloneData(int.Parse(namLamViec.IIDMaDanhMuc), int.Parse(nam.IIDMaDanhMuc), authenticationInfo, 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1);
                }
            }

            // handle data mlns
            List<NsMucLucNganSach> fullListMlns = nsMucLucNganSaches.Select(t => ObjectCopier.Clone(t)).ToList();
            List<NsMucLucNganSach> existMlns = _migrationDataService.GetListExistMlns();
            // thêm data mlns bị thiếu
            if (isAddedMissingMlns)
            {
                foreach (var nam in dsNamLamViec)
                {
                    var dsMlns = _migrationDataService.GetListMissingMlnsByNamLamViec(FilePath, int.Parse(nam.IIDMaDanhMuc));
                    fullListMlns.AddRange(dsMlns.Where(t => !string.IsNullOrEmpty(t.XauNoiMa)));
                }
            }
            fullListMlns = existMlns.Union(fullListMlns, new NsMucLucNganSachEqualityComparer()).ToList();
            addedListMLns = new List<NsMucLucNganSach>();
            // split mlns by namlamviec for faster
            var subListMlns = fullListMlns.Where(t => copiedNamLamViec.Contains(t.NamLamViec.Value.ToString())).GroupBy(t => t.NamLamViec, t => t, (namlamviec, lstMlns) => new { namlamviec = namlamviec, lstMlns = lstMlns }).ToList();
            foreach (var item in subListMlns)
            {
                //var insertedMLNS = item.lstMlns.Except(existMlns.Where(t => t.NamLamViec == item.namlamviec), new NsMucLucNganSachEqualityComparer()).OrderBy(m => m.XauNoiMa);
                foreach (var mlns in item.lstMlns.OrderBy(m => m.XauNoiMa))
                {
                    IEnumerable<NsMucLucNganSach> possibleParentList = item.lstMlns.Where(i => !i.XauNoiMa.Equals(mlns.XauNoiMa) && mlns.XauNoiMa.StartsWith(i.XauNoiMa)).OrderBy(t => t.XauNoiMa.Length);
                    mlns.MlnsId = Guid.NewGuid();
                    if (!string.IsNullOrEmpty(mlns.Lns) && string.IsNullOrEmpty(mlns.L))
                    {
                        mlns.BTuChi = true;
                        mlns.BHienVat = true;
                    }
                    // trên mức ng
                    if (string.IsNullOrEmpty(mlns.Ng))
                    {
                        mlns.BHangChaDuToan = true;
                        mlns.BHangChaQuyetToan = true;
                    }
                    // mức ng
                    else if (string.IsNullOrEmpty(mlns.Tng))
                    {
                        mlns.BHangChaDuToan = false;
                        mlns.BHangChaQuyetToan = false;
                        mlns.SDuToanChiTietToi = "NG";
                        mlns.SQuyetToanChiTietToi = "NG";
                    }

                    /*existDataMlnsOfNamLamViec.Add(mlns);
                    existDataXnm = existDataXnm + mlns.XauNoiMa + ";";*/

                    if (possibleParentList.Count() > 0)
                    {
                        mlns.MlnsIdParent = possibleParentList.Last().MlnsId;
                    }
                    addedListMLns.Add(mlns);
                }
            }
            List<NsMucLucNganSach> existDataMlns = subListMlns.SelectMany(t => t.lstMlns).ToList();
            //addedListMLns = existDataMlns;
            // handle data donvi
            foreach (var item in donVis)
            {
                item.Loai = "0".Equals(item.Loai) ? "0" : "1";
            }
            DonVi dvRoot = donVis.FirstOrDefault(t => "0".Equals(t.Loai));
            // handle data sktchungtu
            // get danh sach so nhu cau, so kiem tra
            List<Triplet> sktChungTuIndexList = new List<Triplet>();
            foreach (var nam in dsNamLamViec)
            {
                int iNamLamViec = int.Parse(nam.IIDMaDanhMuc);
                int INamNganSach = 2;
                int IIdMaNguonNganSach = 1;
                int indexSnc = _sktChungTuRepository.GetSoChungTuIndexByCondition("0", iNamLamViec, INamNganSach, IIdMaNguonNganSach);
                int indexSkt = _sktChungTuRepository.GetSoChungTuIndexByCondition("3", iNamLamViec, INamNganSach, IIdMaNguonNganSach);
                sktChungTuIndexList.Add(new Triplet(iNamLamViec, indexSnc, indexSkt));
            }
            foreach (var item in nsSktChungTus)
            {
                item.INamNganSach = 2;
                item.IIdMaNguonNganSach = 1;
                item.ILoaiChungTu = 1;

                if (item.ILoai == 3 && dvRoot != null)
                {
                    item.IIdMaDonVi = dvRoot.IIDMaDonVi;
                }

                if (item.ILoai == 0)
                {
                    item.ILoaiChungTu = 1;
                    if (dvRoot != null && string.IsNullOrEmpty(item.IIdMaDonVi))
                    {
                        item.IIdMaDonVi = dvRoot.IIDMaDonVi;
                    }
                }
                else if (item.ILoai == 104)
                {
                    item.ILoai = 0;
                    item.ILoaiChungTu = 2;
                }
                else if (item.ILoai == 3)
                {
                    item.ILoaiChungTu = 1;
                }
                else if (item.ILoai == 105)
                {
                    item.ILoai = 3;
                    item.ILoaiChungTu = 2;
                }

                if (item.ILoai == 0)
                {
                    var sncChungTuIndex = sktChungTuIndexList.FirstOrDefault(t => t.First.Equals(item.INamLamViec));
                    if (sncChungTuIndex != null)
                    {
                        int sncIndex = int.Parse(sncChungTuIndex.Second.ToString());
                        item.SSoChungTu = "SNC-" + sncIndex.ToString("D3");
                        sncChungTuIndex.Second = ++sncIndex;
                    }
                }

                if (item.ILoai == 3)
                {
                    var sktChungTuIndex = sktChungTuIndexList.FirstOrDefault(t => t.First.Equals(item.INamLamViec));
                    if (sktChungTuIndex != null)
                    {
                        int sktIndex = int.Parse(sktChungTuIndex.Third.ToString());
                        item.SSoChungTu = "SKT-" + sktIndex.ToString("D3");
                        sktChungTuIndex.Third = ++sktIndex;
                    }
                }

                item.FTongTuChi = nsSktChungTuChiTiets.Where(t => item.Id.Equals(t.IIdCtsoKiemTra)).Sum(t => t.FTuChi);
            }
            // handle data skt chung tu chi tiet
            nsSktChungTuChiTiets = new ObservableCollection<NsSktChungTuChiTiet>(nsSktChungTuChiTiets.Where(t => nsSktChungTus.Select(s => s.Id).Contains(t.IIdCtsoKiemTra)));
            foreach (var item in nsSktChungTuChiTiets)
            {
                NsSktMucLuc nsSktMucLuc = nsSktMucLucs.FirstOrDefault(t => t.SKyHieu.Equals(item.XauNoiMa) && t.INamLamViec == item.INamLamViec);
                if (nsSktMucLuc != null)
                {
                    item.IIdMlskt = nsSktMucLuc.IIDMLSKT;
                }
                item.INamNganSach = 2;
                item.IIdMaNguonNganSach = 1;
                if (dvRoot != null && string.IsNullOrEmpty(item.IIdMaDonVi))
                {
                    item.IIdMaDonVi = dvRoot.IIDMaDonVi;
                }
                if (item.ILoai == 0)
                {
                    item.ILoaiChungTu = 1;
                }
                else if (item.ILoai == 104)
                {
                    item.ILoai = 0;
                    item.ILoaiChungTu = 2;
                }
                else if (item.ILoai == 3)
                {
                    item.ILoaiChungTu = 1;
                }
                else if (item.ILoai == 105)
                {
                    item.ILoai = 3;
                    item.ILoaiChungTu = 2;
                }
                else if (item.ILoai == 4)
                {
                    item.ILoaiChungTu = 1;
                }
            }

            // handle data ns_qs_chungtuchitiet
            foreach (var item in nsQtChungTuChiTiets)
            {
                NsMucLucNganSach mlns = existDataMlns.FirstOrDefault(t => t.XauNoiMa.Equals(item.SXauNoiMa) && t.NamLamViec == item.INamLamViec);

                if (mlns != null)
                {
                    item.IIdMlns = mlns.MlnsId;
                    item.IIdMlnsCha = mlns.MlnsIdParent;
                }
            }
            // group các bản ghi quyết toán chứng từ chi tiết có loại tng thành 1 bản ghi chứng từ chi tiết có loại ng duy nhất
            List<NsQtChungTuChiTiet> NsQtChungTuChiTietsNGTongHop = new List<NsQtChungTuChiTiet>();
            var NsQtChungTuChiTietsTng = nsQtChungTuChiTiets.Where(t => !string.IsNullOrEmpty(t.STng)).GroupBy(t => new { t.IIdMlnsCha, t.IIdQtchungTu },
                                                (key, elements) => new { key, lstQtCtChiTiet = elements });
            foreach (var item in NsQtChungTuChiTietsTng)
            {
                NsQtChungTuChiTiet NsQtChungTuChiTietTNG = item.lstQtCtChiTiet.First();
                NsQtChungTuChiTiet NsQtChungTuChiTietNG = ObjectCopier.Clone(NsQtChungTuChiTietTNG);
                NsQtChungTuChiTietNG.Id = Guid.NewGuid();
                NsQtChungTuChiTietNG.IIdQtchungTu = item.key.IIdQtchungTu;
                NsQtChungTuChiTietNG.IIdMlns = item.key.IIdMlnsCha.HasValue ? item.key.IIdMlnsCha.Value : Guid.Empty;
                NsMucLucNganSach nsMucLucNganSach = existDataMlns.FirstOrDefault(t => t.MlnsId.Equals(NsQtChungTuChiTietNG.IIdMlns) && t.NamLamViec == NsQtChungTuChiTietTNG.INamLamViec);
                NsQtChungTuChiTietNG.IIdMlnsCha = nsMucLucNganSach?.MlnsIdParent;
                NsQtChungTuChiTietNG.SXauNoiMa = nsMucLucNganSach?.XauNoiMa;
                NsQtChungTuChiTietNG.STng = "";
                NsQtChungTuChiTietNG.FTuChiDeNghi = item.lstQtCtChiTiet.Sum(t => t.FTuChiDeNghi);
                NsQtChungTuChiTietNG.FTuChiPheDuyet = item.lstQtCtChiTiet.Sum(t => t.FTuChiPheDuyet);
                NsQtChungTuChiTietNG.FSoNguoi = item.lstQtCtChiTiet.Sum(t => t.FSoNguoi);
                NsQtChungTuChiTietNG.FSoNgay = item.lstQtCtChiTiet.Sum(t => t.FSoNgay);
                NsQtChungTuChiTietNG.FSoLuot = item.lstQtCtChiTiet.Sum(t => t.FSoLuot);
                NsQtChungTuChiTietsNGTongHop.Add(NsQtChungTuChiTietNG);

                NsQtChungTuChiTiet existNsQtChungTuChiTietNG = nsQtChungTuChiTiets.FirstOrDefault(t => t.IIdMlns.Equals(NsQtChungTuChiTietNG.IIdMlns) && t.IIdQtchungTu.Equals(NsQtChungTuChiTietNG.IIdQtchungTu));

                if (existNsQtChungTuChiTietNG != null)
                {
                    NsQtChungTuChiTietNG.FTuChiDeNghi += existNsQtChungTuChiTietNG.FTuChiDeNghi;
                    NsQtChungTuChiTietNG.FTuChiPheDuyet += existNsQtChungTuChiTietNG.FTuChiPheDuyet;
                    NsQtChungTuChiTietNG.FSoNguoi += existNsQtChungTuChiTietNG.FSoNguoi;
                    NsQtChungTuChiTietNG.FSoNgay += existNsQtChungTuChiTietNG.FSoNgay;
                    NsQtChungTuChiTietNG.FSoLuot += existNsQtChungTuChiTietNG.FSoLuot;
                }
            }
            // remove chung tu chitiet loại ng có id chứng từ và mlns thuộc list dt chứng từ chi tiết tổng hợp loại ng phía trên
            nsQtChungTuChiTiets = new ObservableCollection<NsQtChungTuChiTiet>(nsQtChungTuChiTiets.Where(t => string.IsNullOrEmpty(t.STng))
                .Where(t => !NsQtChungTuChiTietsNGTongHop.Select(s => new { s.IIdQtchungTu, s.IIdMlns }).Contains(new { t.IIdQtchungTu, t.IIdMlns })));

            foreach (var item in NsQtChungTuChiTietsNGTongHop)
            {
                nsQtChungTuChiTiets.Add(item);
            }

            // handle for ns qt chungtu
            foreach (var item in nsQtChungTus)
            {
                item.BDaTongHop = false;
                item.FTongTuChiDeNghi = nsQtChungTuChiTiets.Where(t => item.Id.Equals(t.IIdQtchungTu)).Sum(t => t.FTuChiDeNghi);
                item.FTongTuChiPheDuyet = nsQtChungTuChiTiets.Where(t => item.Id.Equals(t.IIdQtchungTu)).Sum(t => t.FTuChiPheDuyet);
            }

            foreach (var item in nsBkChungTuChiTiets)
            {
                NsMucLucNganSach mlns = existDataMlns.FirstOrDefault(t => t.XauNoiMa.Equals(item.SXauNoiMa) && t.NamLamViec == item.INamLamViec);
                if (mlns != null)
                {
                    item.IIdMlns = mlns.MlnsId;
                    item.IIdMlnsCha = mlns.MlnsIdParent;
                }
            }
            foreach (var item in nsCpChungTuChiTiets)
            {
                NsMucLucNganSach mlns = existDataMlns.FirstOrDefault(t => t.XauNoiMa.Equals(item.SXauNoiMa) && t.NamLamViec == item.INamLamViec);
                if (mlns != null)
                {
                    item.IIdMlns = mlns.MlnsId;
                }
            }
            // handle data NS_DTDauNam_ChungTu
            savedLstNsDtdauNamChungTu = new List<NsDtdauNamChungTu>();
            savedLstNsDtdauNamChungTuChiTiet = new List<NsDtdauNamChungTuChiTiet>();
            var NSDTDauNamChungTu = nsDtdauNamChungTuChiTiets.GroupBy(t => new { t.INamLamViec, t.IIdMaDonVi }, t => t,
                (k, v) => new { namLamViec = k.INamLamViec, maDonVi = k.IIdMaDonVi, lstCTChiTiet = v });
            var lstNamLamViec = nsDtdauNamChungTuChiTiets.Select(t => t.INamLamViec).Distinct();
            Dictionary<int, int> namLamViecSoChungTu = new Dictionary<int, int>();
            foreach (var nam in lstNamLamViec)
            {
                int ISoChungTuIndex = _sktSoLieuChungTuRepository.GetSoChungTuIndexByCondition(nam, 1, 2, 1);
                namLamViecSoChungTu.Add(nam, ISoChungTuIndex);
            }
            foreach (var item in NSDTDauNamChungTu)
            {
                var nsDtdauNamChungTuChiTiet = item.lstCTChiTiet.First();
                NsDtdauNamChungTu nsDtdauNamChungTu = new NsDtdauNamChungTu();
                nsDtdauNamChungTu.Id = Guid.NewGuid();
                nsDtdauNamChungTu.ISoChungTuIndex = namLamViecSoChungTu[nsDtdauNamChungTuChiTiet.INamLamViec];
                namLamViecSoChungTu[nsDtdauNamChungTuChiTiet.INamLamViec] = nsDtdauNamChungTu.ISoChungTuIndex + 1;
                nsDtdauNamChungTu.SSoChungTu = "DTDN-" + nsDtdauNamChungTu.ISoChungTuIndex.ToString("D3");
                nsDtdauNamChungTu.IIdMaDonVi = item.maDonVi;
                nsDtdauNamChungTu.INamLamViec = item.namLamViec;
                nsDtdauNamChungTu.SNguoiSua = nsDtdauNamChungTuChiTiet.SNguoiSua;
                nsDtdauNamChungTu.SNguoiTao = nsDtdauNamChungTuChiTiet.SNguoiTao;
                nsDtdauNamChungTu.DNgaySua = nsDtdauNamChungTuChiTiet.DNgaySua;
                nsDtdauNamChungTu.DNgayTao = nsDtdauNamChungTuChiTiet.DNgayTao;
                nsDtdauNamChungTu.IIdMaNguonNganSach = 1;
                nsDtdauNamChungTu.INamNganSach = 2;
                nsDtdauNamChungTu.ILoaiChungTu = 1;
                nsDtdauNamChungTu.SDslns = string.Join(",", item.lstCTChiTiet.Select(t => t.SLns).Distinct());
                nsDtdauNamChungTu.FTongTuChi = item.lstCTChiTiet.Sum(t => t.FTuChi);
                foreach (var chitiet in item.lstCTChiTiet)
                {
                    chitiet.IIdMaNguonNganSach = 1;
                    chitiet.INamNganSach = 2;
                    chitiet.ILoaiChungTu = "1";
                    chitiet.IIdCtdtdauNam = nsDtdauNamChungTu.Id;
                }
                savedLstNsDtdauNamChungTu.Add(nsDtdauNamChungTu);
                savedLstNsDtdauNamChungTuChiTiet.AddRange(item.lstCTChiTiet);
            }

            // handle data du toan chung tu,  du toan chung tu chitiet
            NsDtNhanPhanBoMap = new List<NsDtNhanPhanBoMap>();

            foreach (var dtChungTu in NsDtChungTusLoaiNhan)
            {
                // map cac chung tu loai phan bo
                if (dtChungTu.ILoai == 1)
                {
                    NsDtNhanPhanBoMap map = new NsDtNhanPhanBoMap();
                    map.Id = Guid.NewGuid();
                    map.IIdCtduToanNhan = dtChungTu.IIDChungTuNhan.HasValue ? dtChungTu.IIDChungTuNhan.Value : Guid.Empty;
                    map.IIdCtduToanPhanBo = dtChungTu.Id;
                    map.DNgayTao = dtChungTu.DNgayTao;
                    map.SNguoiTao = dtChungTu.SNguoiTao;
                    map.DNgaySua = dtChungTu.DNgaySua;
                    dtChungTu.IIdDotNhan = dtChungTu.IIDChungTuNhan.HasValue ? dtChungTu.IIDChungTuNhan.Value.ToString() : null;
                    NsDtNhanPhanBoMap.Add(map);
                }
            }

            foreach (var chitiet in nsDtChungTuChiTiets)
            {
                NsMucLucNganSach mlns = existDataMlns.FirstOrDefault(t => t.XauNoiMa.Equals(chitiet.SXauNoiMa) && t.NamLamViec == chitiet.INamLamViec);
                if (mlns != null)
                {
                    chitiet.IIdMlns = mlns.MlnsId;
                    chitiet.IIdMlnsCha = mlns.MlnsIdParent;
                }
                // tim chung tu dot nhan
                DtChungTuModel dtChungTuModel = NsDtChungTusLoaiNhan.FirstOrDefault(t => t.Id.Equals(chitiet.IIdDtchungTu));
                if (dtChungTuModel != null)
                    chitiet.IIdCtduToanNhan = dtChungTuModel.IIDChungTuNhan.Value;
                if (string.IsNullOrEmpty(chitiet.IIdMaDonVi) && dvRoot != null)
                {
                    chitiet.IIdMaDonVi = dvRoot.IIDMaDonVi;
                }
            }

            // group các bản ghi dự toán chứng từ chi tiết có loại tng thành 1 bản ghi chứng từ chi tiết có loại ng duy nhất
            List<NsDtChungTuChiTiet> nsDtChungTuChiTietsNGTongHop = new List<NsDtChungTuChiTiet>();
            var nsDtChungTuChiTietsTng = nsDtChungTuChiTiets.Where(t => !string.IsNullOrEmpty(t.STng)).GroupBy(t => new { t.IIdMlnsCha, t.IIdDtchungTu, t.IIdMaDonVi },
                                                (key, elements) => new { key, lstDtCtChiTiet = elements });
            foreach (var item in nsDtChungTuChiTietsTng)
            {
                NsDtChungTuChiTiet nsDtChungTuChiTietTNG = item.lstDtCtChiTiet.First();
                NsDtChungTuChiTiet nsDtChungTuChiTietNG = ObjectCopier.Clone(nsDtChungTuChiTietTNG);
                nsDtChungTuChiTietNG.Id = Guid.NewGuid();
                nsDtChungTuChiTietNG.IIdDtchungTu = item.key.IIdDtchungTu;
                nsDtChungTuChiTietNG.IIdMlns = item.key.IIdMlnsCha;
                NsMucLucNganSach nsMucLucNganSach = existDataMlns.FirstOrDefault(t => t.MlnsId.Equals(nsDtChungTuChiTietNG.IIdMlns) && t.NamLamViec == nsDtChungTuChiTietTNG.INamLamViec);
                nsDtChungTuChiTietNG.IIdMlnsCha = nsMucLucNganSach?.MlnsIdParent;
                nsDtChungTuChiTietNG.SXauNoiMa = nsMucLucNganSach?.XauNoiMa;
                nsDtChungTuChiTietNG.STng = "";
                nsDtChungTuChiTietNG.FTuChi = item.lstDtCtChiTiet.Sum(t => t.FTuChi);
                nsDtChungTuChiTietsNGTongHop.Add(nsDtChungTuChiTietNG);

                NsDtChungTuChiTiet existNsDtChungTuChiTietNG = nsDtChungTuChiTiets.FirstOrDefault(t => t.IIdMlns.Equals(nsDtChungTuChiTietNG.IIdMlns) && t.IIdDtchungTu.Equals(nsDtChungTuChiTietNG.IIdDtchungTu));
                if (existNsDtChungTuChiTietNG != null)
                {
                    nsDtChungTuChiTietNG.FTuChi += existNsDtChungTuChiTietNG.FTuChi;
                }
            }
            // remove chung tu chitiet loại ng có id chứng từ và mlns thuộc list dt chứng từ chi tiết tổng hợp loại ng phía trên
            nsDtChungTuChiTiets = new ObservableCollection<NsDtChungTuChiTiet>(nsDtChungTuChiTiets.Where(t => string.IsNullOrEmpty(t.STng))
                .Where(t => !nsDtChungTuChiTietsNGTongHop.Select(s => new { s.IIdDtchungTu, s.IIdMlns }).Contains(new { t.IIdDtchungTu, t.IIdMlns })));
            foreach (var item in nsDtChungTuChiTietsNGTongHop)
            {
                nsDtChungTuChiTiets.Add(item);
            }

            nsDtChungTus = _mapper.Map<ObservableCollection<NsDtChungTu>>(NsDtChungTusLoaiNhan);

            foreach (var item in nsDtChungTus)
            {
                item.ILoaiChungTu = 1;
                item.ILoaiDuToan = item.ILoaiDuToan switch
                {
                    0 => 1,
                    1 => 3,
                    2 => 3,
                    3 => 5,
                    _ => 1,
                };
                item.FTongTuChi = nsDtChungTuChiTiets.Where(t => item.Id.Equals(t.IIdDtchungTu)).Sum(t => t.FTuChi);
            }
            nsDtChungTuChiTiets = new ObservableCollection<NsDtChungTuChiTiet>(nsDtChungTuChiTiets.Where(t => t.IIdDtchungTu.HasValue && nsDtChungTus.Select(s => s.Id).Contains(t.IIdDtchungTu.Value)));
            foreach (var baocao in CurrentDanhMucBaoCao)
            {
                var oldBC = dmChuKys.FirstOrDefault(t => t.IdType != null && t.IdType.Equals(baocao.IdOldType));
                if (oldBC == null)
                    continue;
                baocao.ChucDanh1 = oldBC.ChucDanh1;
                baocao.ChucDanh1MoTa = DanhmucChuKyChucDanh.FirstOrDefault(t => t.IIDMaDanhMuc.Equals(baocao.ChucDanh1))?.SGiaTri;
                baocao.ChucDanh2 = oldBC.ChucDanh2;
                baocao.ChucDanh2MoTa = DanhmucChuKyChucDanh.FirstOrDefault(t => t.IIDMaDanhMuc.Equals(baocao.ChucDanh2))?.SGiaTri;
                baocao.ChucDanh3 = oldBC.ChucDanh3;
                baocao.ChucDanh3MoTa = DanhmucChuKyChucDanh.FirstOrDefault(t => t.IIDMaDanhMuc.Equals(baocao.ChucDanh3))?.SGiaTri;
                baocao.ThuaLenh1 = oldBC.ThuaLenh1;
                baocao.ThuaLenh1MoTa = DanhmucChuKyChucDanh.FirstOrDefault(t => t.IIDMaDanhMuc.Equals(baocao.ThuaLenh1))?.SGiaTri;
                baocao.ThuaLenh2 = oldBC.ChucDanh2;
                baocao.ThuaLenh2MoTa = DanhmucChuKyChucDanh.FirstOrDefault(t => t.IIDMaDanhMuc.Equals(baocao.ThuaLenh2))?.SGiaTri;
                baocao.ThuaLenh3 = oldBC.ChucDanh3;
                baocao.ThuaLenh3MoTa = DanhmucChuKyChucDanh.FirstOrDefault(t => t.IIDMaDanhMuc.Equals(baocao.ThuaLenh3))?.SGiaTri;
                baocao.Ten1 = oldBC.Ten1;
                baocao.Ten1MoTa = DanhMucChuKyTen.FirstOrDefault(t => t.IIDMaDanhMuc.Equals(baocao.Ten1))?.SGiaTri;
                baocao.Ten2 = oldBC.Ten2;
                baocao.Ten2MoTa = DanhMucChuKyTen.FirstOrDefault(t => t.IIDMaDanhMuc.Equals(baocao.Ten2))?.SGiaTri;
                baocao.Ten3 = oldBC.Ten3;
                baocao.Ten3MoTa = DanhMucChuKyTen.FirstOrDefault(t => t.IIDMaDanhMuc.Equals(baocao.Ten3))?.SGiaTri;
            }
        }
    }
}
