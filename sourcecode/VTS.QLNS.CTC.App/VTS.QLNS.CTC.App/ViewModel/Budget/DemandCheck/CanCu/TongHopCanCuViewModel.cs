using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck
{
    public class TongHopCanCuViewModel : DialogViewModelBase<ChungTuCanCuModel>
    {
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;
        private readonly ISessionService _sessionService;
        private readonly INsDtChungTuService _iDtChungTuService;
        private readonly INsDtChungTuChiTietService _iDtChungTuChiTietService;
        private readonly INsQtChungTuService _iQtChungTuService;
        private readonly INsQtChungTuChiTietService _iQtChungTuChiTietService;
        private readonly ICpChungTuService _iCpChungTuService;
        private readonly ICpChungTuChiTietService _iCpChungTuChiTietService;
        private readonly ISktChungTuService _iSktChungTuService;
        private readonly ISktChungTuChiTietService _iSktChungTuChiTietService;
        private readonly ISktChungTuChiTietCanCuChungTuService _iSktChungTuChiTietCanCuChungTuService;
        private readonly IDanhMucService _iDanhMucService;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        private IMapper _mapper;

        public override Type ContentType => typeof(View.Budget.DemandCheck.CanCu.TongHopCanCu);
        public override string Name => "CĂN CỨ";
        public override string Description => "Căn cứ";
        public NsSktChungTuModel NsSktChungTuModel { get; set; }

        private ObservableCollection<CauHinhCanCuModel> _cauHinhCanCu;
        public ObservableCollection<CauHinhCanCuModel> CauHinhCanCu
        {
            get => _cauHinhCanCu;
            set => SetProperty(ref _cauHinhCanCu, value);
        }

        private ObservableCollection<CauHinhCanCuModel> _cauHinhCanCuTemp;
        public ObservableCollection<CauHinhCanCuModel> CauHinhCanCuTemp
        {
            get => _cauHinhCanCuTemp;
            set => SetProperty(ref _cauHinhCanCuTemp, value);
        }

        private CauHinhCanCuModel _selectedCauHinhCanCuModel;
        public CauHinhCanCuModel SelectedCauHinhCanCuModel
        {
            get => _selectedCauHinhCanCuModel;
            set
            {
                SetProperty(ref _selectedCauHinhCanCuModel, value);
                if (_selectedCauHinhCanCuModel != null)
                {
                    TinhCanCuTheoMLNS();

                    SelectedNNganhModel = NNganhModelItems.FirstOrDefault(item => item.IIDMaDanhMuc.Equals(_selectedCauHinhCanCuModel.NganhSelected));
                    if (_selectedCauHinhCanCuModel.IIDMaChucNang.Equals(TypeCanCu.DEMAND) ||
                        _selectedCauHinhCanCuModel.IIDMaChucNang.Equals(TypeCanCu.CHECK_NUMBER))
                    {
                        _showColLNS = Visibility.Collapsed;
                        _showColNhomNganh = Visibility.Visible;
                    }
                    else
                    {
                        _showColLNS = Visibility.Visible;
                        _showColNhomNganh = Visibility.Collapsed;
                    }

                    if (_selectedCauHinhCanCuModel.IThietLap == 2)
                    {
                        EnableSelectedAll = true;
                    }
                    else
                    {
                        EnableSelectedAll = false;
                    }
                }
                else
                {
                    ChungTuCanCuModels = null;
                    _showColLNS = Visibility.Collapsed;
                    _showColNhomNganh = Visibility.Hidden;
                }

                OnPropertyChanged(nameof(ShowColLNS));
                OnPropertyChanged(nameof(ShowColNhomNganh));
                OnPropertyChanged(nameof(SelectAll));
                OnPropertyChanged(nameof(EnableSelectedAll));
            }
        }

        private ObservableCollection<ChungTuCanCuModel> _chungTucanCuModels;
        public ObservableCollection<ChungTuCanCuModel> ChungTuCanCuModels
        {
            get => _chungTucanCuModels;
            set => SetProperty(ref _chungTucanCuModels, value);
        }

        private ChungTuCanCuModel _selectedChungTuCanCuModel;
        public ChungTuCanCuModel SelectedChungTuCanCuModel
        {
            get => _selectedChungTuCanCuModel;
            set
            {
                SetProperty(ref _selectedChungTuCanCuModel, value);
                if (_selectedChungTuCanCuModel != null)
                {
                    //LoadCauHinhCanCu();
                }
            }
        }

        public bool? SelectAll
        {
            get
            {
                if (ChungTuCanCuModels != null)
                {
                    var selected = ChungTuCanCuModels.Select(item => item.IsChecked).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAllCanCu(value.Value, ChungTuCanCuModels);
                    OnPropertyChanged();
                }
            }
        }

        private void SelectAllCanCu(bool select, IEnumerable<ChungTuCanCuModel> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        public bool EnableSelectedAll { get; set; }

        public List<Guid> ListIdNhomNganhSelected { get; set; }
        private ObservableCollection<DanhMucNganhModel> _nNganhModelItems;
        public ObservableCollection<DanhMucNganhModel> NNganhModelItems
        {
            get => _nNganhModelItems;
            set => SetProperty(ref _nNganhModelItems, value);
        }

        private DanhMucNganhModel _selectedNNganhModel;
        public DanhMucNganhModel SelectedNNganhModel
        {
            get => _selectedNNganhModel;
            set
            {
                SetProperty(ref _selectedNNganhModel, value);
                if (_selectedNNganhModel != null)
                {
                    ListIdNhomNganhSelected = _iSktChungTuChiTietService.FindMucLucSKTTheoNganh(_selectedNNganhModel.IIDMaDanhMuc, 1, _sessionService.Current.YearOfWork).Select(item => item.IIdMlskt).ToList();
                }
                else
                {
                    ListIdNhomNganhSelected = null;
                }
                if (_selectedCauHinhCanCuModel != null)
                {
                    LoadChungTuCanCu();
                    _selectedCauHinhCanCuModel.NganhSelected = _selectedNNganhModel == null ? null : _selectedNNganhModel.IIDMaDanhMuc;
                }
            }
        }

        public List<Guid> ListIdMlnsSelected { get; set; }

        public List<string> LstLnsNsbd { get; set; }

        private ObservableCollection<NsMuclucNgansachModel> _dataLNS;
        public ObservableCollection<NsMuclucNgansachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public Visibility ShowColNSSD => (NsSktChungTuModel != null && NsSktChungTuModel.ILoaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key))) ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowColNSBD => (NsSktChungTuModel != null && NsSktChungTuModel.ILoaiChungTu.Equals(int.Parse(VoucherType.NSBD_Key))) ? Visibility.Visible : Visibility.Collapsed;

        public Visibility _showColNhomNganh;
        public Visibility ShowColNhomNganh
        {
            get => _showColNhomNganh;
            set => SetProperty(ref _showColNhomNganh, value);
        }

        public Visibility _showColLNS;
        public Visibility ShowColLNS
        {
            get => _showColLNS;
            set => SetProperty(ref _showColLNS, value);
        }

        public Visibility _isDuToanVisibility;
        public Visibility IsDuToanVisibility
        {
            get => _isDuToanVisibility;
            set => SetProperty(ref _isDuToanVisibility, value);
        }

        public Visibility _isQuyetToanVisibility;
        public Visibility IsQuyetToanVisibility
        {
            get => _isQuyetToanVisibility;
            set => SetProperty(ref _isQuyetToanVisibility, value);
        }

        public RelayCommand CanCuCommand { set; get; }

        public TongHopCanCuViewModel(ICauHinhCanCuService iCauHinhCanCuService,
            ISessionService sessionService,
            INsDtChungTuService iDtChungTuService,
            INsDtChungTuChiTietService iDtChungTuChiTietService,
            INsQtChungTuService iQtChungTuService,
            INsQtChungTuChiTietService iQtChungTuChiTietService,
            ICpChungTuService iCpChungTuService,
            ICpChungTuChiTietService iCpChungTuChiTietService,
            ISktChungTuService iSktChungTuService,
            ISktChungTuChiTietService iSktChungTuChiTietService,
            ISktChungTuChiTietCanCuChungTuService iSktChungTuChiTietCanCuChungTuService,
            IDanhMucService iDanhMucService,
            INsMucLucNganSachService iNsMucLucNganSachService,
            IMapper mapper)
        {
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _sessionService = sessionService;
            _iDtChungTuService = iDtChungTuService;
            _iDtChungTuChiTietService = iDtChungTuChiTietService;
            _iQtChungTuService = iQtChungTuService;
            _iQtChungTuChiTietService = iQtChungTuChiTietService;
            _iCpChungTuService = iCpChungTuService;
            _iCpChungTuChiTietService = iCpChungTuChiTietService;
            _iSktChungTuService = iSktChungTuService;
            _iSktChungTuChiTietService = iSktChungTuChiTietService;
            _iSktChungTuChiTietCanCuChungTuService = iSktChungTuChiTietCanCuChungTuService;
            _iDanhMucService = iDanhMucService;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            _mapper = mapper;
            CanCuCommand = new RelayCommand(obj => TinhCanCuTheoMLNS());
        }


        public override void Init()
        {
            _showColNhomNganh = Visibility.Hidden;
            _showColLNS = Visibility.Collapsed;
            _isDuToanVisibility = Visibility.Collapsed;
            _isQuyetToanVisibility = Visibility.Collapsed;
            _selectedCauHinhCanCuModel = null;
            LoadNhomNganh();
            LoadLNS();
            LoadCanCu();
        }

        public override void OnSave()
        {
            base.OnSave();
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(CauHinhCanCu);
        }

        private void LoadNhomNganh()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var type = VoucherType.VOCHER_TYPE;
            var iTrangThai = StatusType.ACTIVE;
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.INamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.SType.Equals(type));
            var listUnit = _iDanhMucService.FindByCondition(predicate).ToList();
            NNganhModelItems = new ObservableCollection<DanhMucNganhModel>();
            NNganhModelItems = _mapper.Map<ObservableCollection<DanhMucNganhModel>>(listUnit);
        }

        private void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            int loaiChungTu = NsSktChungTuModel.ILoaiChungTu.GetValueOrDefault();

            var listNsMucLucNganSach = _iNsMucLucNganSachService.FindByMLNS(yearOfWork, NSEntityStatus.ACTIVED, loaiChungTu).ToList();
            DataLNS = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listNsMucLucNganSach);
        }

        public void TinhCanCuTheoMLNS()
        {
            LstLnsNsbd = new List<string>();
            if (NsSktChungTuModel.ILoaiChungTu == int.Parse(VoucherType.NSBD_Key))
            {
                if (TypeCanCu.ESTIMATE.Equals(_selectedCauHinhCanCuModel.IIDMaChucNang))
                {
                    LstLnsNsbd.Add("1040100");
                    LstLnsNsbd.Add("1040200");
                    LstLnsNsbd.Add("1040300");
                }
                else if (TypeCanCu.SETTLEMENT.Equals(_selectedCauHinhCanCuModel.IIDMaChucNang)
                        || TypeCanCu.ALLOCATION.Equals(_selectedCauHinhCanCuModel.IIDMaChucNang))
                {
                    LstLnsNsbd.Add("1040200");
                    LstLnsNsbd.Add("1040300");
                }
            }

            if (_selectedCauHinhCanCuModel != null)
            {
                LoadChungTuCanCu();
            }
        }

        private void LoadCanCu()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<NsCauHinhCanCu>();
            predicate = predicate.And(item => item.SModule == TypeModuleCanCu.DEMAND);
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate);
            CauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);
            if (CauHinhCanCuTemp != null)
            {
                foreach (var item in CauHinhCanCu)
                {
                    foreach (var x in CauHinhCanCuTemp)
                    {
                        if (item.Id.Equals(x.Id))
                        {
                            item.LstIdChungTuCanCu = x.LstIdChungTuCanCu;
                            item.IdChungTuCanCuLuyKe = x.IdChungTuCanCuLuyKe;
                            item.NganhSelected = x.NganhSelected;
                        }
                    }
                    SelectedCauHinhCanCuModel = item;
                }
            }
            else
            {
                foreach (var item in CauHinhCanCu)
                {
                    var predicatecc = PredicateBuilder.True<NsSktChungTuChungTuCanCu>();
                    predicatecc = predicatecc.And(x => x.IIdCtSoKiemTra.Equals(NsSktChungTuModel.Id));
                    predicatecc = predicatecc.And(x => x.IIdCanCu.Equals(item.Id));
                    var listCTCC = _iSktChungTuChiTietCanCuChungTuService.FindByCondition(predicatecc).ToList();
                    if (listCTCC.Count > 0)
                    {
                        item.LstIdChungTuCanCu = item.LstIdChungTuCanCu == null ? new List<Guid>() : item.LstIdChungTuCanCu;
                        foreach (var x in listCTCC)
                        {
                            item.LstIdChungTuCanCu.Add(x.IIdCtCanCu);
                            if (item.IThietLap == 3)
                            {
                                item.IdChungTuCanCuLuyKe = x.IIdCtCanCu;
                            }
                        }

                        SelectedCauHinhCanCuModel = item;
                    }
                }
            }
            SelectedCauHinhCanCuModel = null;
            ChungTuCanCuModels = null;
        }

        private void LoadChungTuCanCu()
        {
            SetColumnVisibilityByCanCu();
            if (_selectedCauHinhCanCuModel.IIDMaChucNang.Equals(TypeCanCu.ESTIMATE))
            {
                LoadChungTuCanCuDuToan();
            }
            else if (_selectedCauHinhCanCuModel.IIDMaChucNang.Equals(TypeCanCu.SETTLEMENT))
            {
                LoadChungTuCanCuQuyetToan();
            }
            else if (_selectedCauHinhCanCuModel.IIDMaChucNang.Equals(TypeCanCu.ALLOCATION))
            {
                LoadChungTuCanCuCapPhat();
            }
            else if (_selectedCauHinhCanCuModel.IIDMaChucNang.Equals(TypeCanCu.DEMAND))
            {
                LoadChungTuCanCuNhuCau();
            }
            else
            {
                LoadChungTuCanCuSoKiemTra();
            }
        }
        
        private bool checkSLNS(string slns)
        {
            var listSLNS = new List<string> { "1010100", "1020900", "1020902", "1010400", "1020600", "105" };
            var lstSLNS = slns.Split(',');
            return lstSLNS.Any(item => !listSLNS.Contains(item));
        }

        private void LoadChungTuCanCuDuToan()
        {
            var idDonVi = NsSktChungTuModel.IIdMaDonVi;
            var loaiChungTu = NsSktChungTuModel.ILoaiChungTu;
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(item => item.SDsidMaDonVi.Contains(idDonVi));
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => item.ILoaiChungTu == loaiChungTu);
            predicate = predicate.And(item => item.ILoaiDuToan == (int)BudgetType.YEAR);
            predicate = predicate.And(item => true.Equals(item.BKhoa));

            switch (NsSktChungTuModel.ILoaiNguonNganSach)
            {
                case TypeLoaiNNS.BENH_VIEN:
                    predicate = predicate.And(item => new List<string> { "1010100", "1020900", "1020902" }.Any(y => item.SDslns.Contains(y)));
                    break;
                case TypeLoaiNNS.DOANH_NGHIEP:
                    predicate = predicate.And(item => new List<string> { "1010400", "1020600", "105" }.Any(y => item.SDslns.Contains(y)));
                    break;
                default:              
                    predicate = predicate.And(item => checkSLNS(item.SDslns));
                    break;
            }

            if (!string.IsNullOrEmpty(NsSktChungTuModel.SDssoChungTuTongHop) || NsSktChungTuModel.IIdMaDonVi == _sessionService.Current.IdDonVi)
            {
                predicate = predicate.And(item => item.ILoai == SoChungTuType.ReceiveEstimate);
            }
            else
            {
                predicate = predicate.And(item => item.ILoai == SoChungTuType.EstimateDivision);
            }

            if (NsSktChungTuModel.ILoaiChungTu == int.Parse(VoucherType.NSBD_Key))
            {
                predicate = predicate.And(item => item.SDslns != null && (item.SDslns.Contains("1040100") || item.SDslns.Contains("1040200") || item.SDslns.Contains("1040300")));
            }

            var listCTCanCu = _iDtChungTuService.FindByCondition(predicate).ToList();
            ChungTuCanCuModels = _mapper.Map<ObservableCollection<ChungTuCanCuModel>>(listCTCanCu);
            foreach (var item in ChungTuCanCuModels)
            {
                var listChiIiet = listCTCanCu.Where(x => x.Id.Equals(item.Id)).Select(x => x.ChungTuChiTiets).FirstOrDefault();
                if (listChiIiet != null)
                {
                    item.TuChi = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (LstLnsNsbd == null || LstLnsNsbd.Count == 0 || LstLnsNsbd.Contains(x.SLns))).Sum(x => x.FTuChi);
                    item.HangNhap = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (LstLnsNsbd == null || LstLnsNsbd.Count == 0 || LstLnsNsbd.Contains(x.SLns))).Sum(x => x.FHangNhap);
                    item.HangMua = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (LstLnsNsbd == null || LstLnsNsbd.Count == 0 || LstLnsNsbd.Contains(x.SLns))).Sum(x => x.FHangMua);
                    item.PhanCap = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (LstLnsNsbd == null || LstLnsNsbd.Count == 0 || LstLnsNsbd.Contains(x.SLns))).Sum(x => x.FPhanCap);
                    item.MuaHangHienVat = item.HangNhap + item.HangMua;
                    item.DacThu = item.PhanCap;
                }
                item.PropertyChanged += ItemOnPropertyChanged;
            }

            TinhChungTuChecked();

            if (ChungTuCanCuModels != null && ChungTuCanCuModels.Count > 0)
            {
                foreach (var model in ChungTuCanCuModels)
                {
                    model.PropertyChanged += Item_PropertyChanged;
                }
            }
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(ChungTuCanCuModel.IsChecked)))
            {
                OnPropertyChanged(nameof(SelectAll));
            }
        }

        private void LoadChungTuCanCuQuyetToan()
        {
            var idDonVi = NsSktChungTuModel.IIdMaDonVi;
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            var lstSoCtCon = GetListSoCtQuyetToan();
            var predicate = PredicateBuilder.True<NsQtChungTu>();
            predicate = predicate.And(item => item.IIdMaDonVi.Contains(idDonVi));
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => lstSoCtCon.Contains(item.SSoChungTu));
            if (NsSktChungTuModel.ILoaiChungTu == int.Parse(VoucherType.NSBD_Key))
            {
                predicate = predicate.And(item => item.SDslns != null && (item.SDslns.Contains("1040200") || item.SDslns.Contains("1040300")));
            }
            var listCTCanCu = _iQtChungTuService.FindByCondition(predicate).ToList();
            ChungTuCanCuModels = _mapper.Map<ObservableCollection<ChungTuCanCuModel>>(listCTCanCu);
            foreach (var item in ChungTuCanCuModels)
            {
                var listChiIiet = _iQtChungTuChiTietService.FindByCondition(x => x.IIdQtchungTu.Equals(item.Id));
                if (listChiIiet != null)
                {
                    item.TuChi = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (LstLnsNsbd == null || LstLnsNsbd.Count == 0 || LstLnsNsbd.Contains(x.SLns))).Sum(x => x.FTuChiPheDuyet);
                    item.HangNhap = 0;
                    item.HangMua = 0;
                    item.PhanCap = 0;
                    item.MuaHangHienVat = item.TuChi;
                }
                item.PropertyChanged += ItemOnPropertyChanged;
            }

            TinhChungTuChecked();

            if (ChungTuCanCuModels != null && ChungTuCanCuModels.Count > 0)
            {
                foreach (var model in ChungTuCanCuModels)
                {
                    model.PropertyChanged += Item_PropertyChanged;
                }
            }
        }

        private void LoadChungTuCanCuCapPhat()
        {
            var idDonVi = NsSktChungTuModel.IIdMaDonVi;
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            var lstSoCtCon = GetListSoCtCapPhat();
            var predicate = PredicateBuilder.True<NsCpChungTu>();
            predicate = predicate.And(item => item.SDsidMaDonVi.Contains(idDonVi));
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => lstSoCtCon.Contains(item.SSoChungTu));
            if (NsSktChungTuModel.ILoaiChungTu == int.Parse(VoucherType.NSBD_Key))
            {
                predicate = predicate.And(item => item.SDslns != null && (item.SDslns.Contains("1040200") || item.SDslns.Contains("1040300")));
            }

            var listCTCanCu = _iCpChungTuService.FindByCondition(predicate);
            ChungTuCanCuModels = _mapper.Map<ObservableCollection<ChungTuCanCuModel>>(listCTCanCu);


            foreach (var item in ChungTuCanCuModels)
            {
                var listChiIiet = _iCpChungTuChiTietService.FindByCondition(x => x.IIdCtcapPhat.Equals(item.Id));
                if (listChiIiet != null)
                {
                    item.TuChi = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (LstLnsNsbd == null || LstLnsNsbd.Count == 0 || LstLnsNsbd.Contains(x.SLns))).Sum(x => x.FTuChi) ?? 0;
                    item.HangNhap = 0;
                    item.HangMua = 0;
                    item.PhanCap = 0;
                    item.MuaHangHienVat = item.TuChi;
                }
                item.PropertyChanged += ItemOnPropertyChanged;
            }

            TinhChungTuChecked();

            if (ChungTuCanCuModels != null && ChungTuCanCuModels.Count > 0)
            {
                foreach (var model in ChungTuCanCuModels)
                {
                    model.PropertyChanged += Item_PropertyChanged;
                }
            }
        }

        private void LoadChungTuCanCuNhuCau()
        {
            var idDonVi = NsSktChungTuModel.IIdMaDonVi;
            var loaiChungTu = NsSktChungTuModel.ILoaiChungTu;
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            var lstSoCtCon = GetListSoCtSoNhuCau();
            var predicate = PredicateBuilder.True<NsSktChungTu>();
            predicate = predicate.And(item => item.IIdMaDonVi.Contains(idDonVi));
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => item.ILoaiChungTu == loaiChungTu);
            predicate = predicate.And(item => item.ILoaiNguonNganSach == NsSktChungTuModel.ILoaiNguonNganSach);
            predicate = predicate.And(item => item.ILoai == DemandCheckType.DEMAND);
            predicate = predicate.And(item => lstSoCtCon.Contains(item.SSoChungTu));
            var listCTCanCu = _iSktChungTuService.FindByCondition(predicate);
            ChungTuCanCuModels = _mapper.Map<ObservableCollection<ChungTuCanCuModel>>(listCTCanCu);
            foreach (var item in ChungTuCanCuModels)
            {
                var listChiIiet = _iSktChungTuChiTietService.FindByCondition(x => x.IIdCtsoKiemTra.Equals(item.Id)).ToList();
                if (listChiIiet.Count > 0)
                {
                    item.TuChi = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (ListIdNhomNganhSelected == null || ListIdNhomNganhSelected.Contains(x.IIdMlskt))).Sum(x => x.FTuChi);
                    item.MuaHangHienVat = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (ListIdNhomNganhSelected == null || ListIdNhomNganhSelected.Contains(x.IIdMlskt))).Sum(x => x.FMuaHangCapHienVat);
                    item.DacThu = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (ListIdNhomNganhSelected == null || ListIdNhomNganhSelected.Contains(x.IIdMlskt))).Sum(x => x.FPhanCap);
                }
                item.PropertyChanged += ItemOnPropertyChanged;
            }

            TinhChungTuChecked();

            if (ChungTuCanCuModels != null && ChungTuCanCuModels.Count > 0)
            {
                foreach (var model in ChungTuCanCuModels)
                {
                    model.PropertyChanged += Item_PropertyChanged;
                }
            }
        }

        private void LoadChungTuCanCuSoKiemTra()
        {
            var idDonVi = NsSktChungTuModel.IIdMaDonVi;
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            var loaiChungTu = NsSktChungTuModel.ILoaiChungTu;
            var predicate = PredicateBuilder.True<NsSktChungTu>();
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            if (NsSktChungTuModel.ILoaiNguonNganSach != TypeLoaiNNS.BENH_VIEN)
            {
                predicate = predicate.And(item => item.ILoai == DemandCheckType.CHECK);
            }
            predicate = predicate.And(item => item.ILoaiChungTu == loaiChungTu);
            predicate = predicate.And(item => item.ILoaiNguonNganSach == NsSktChungTuModel.ILoaiNguonNganSach);
            predicate = predicate.And(item => true.Equals(item.BKhoa));
            var listCTCanCu = _iSktChungTuService.FindByCondition(predicate);
            ChungTuCanCuModels = _mapper.Map<ObservableCollection<ChungTuCanCuModel>>(listCTCanCu);
            foreach (var item in ChungTuCanCuModels)
            {
                var listChiIiet = _iSktChungTuChiTietService.FindByCondition(x => x.IIdCtsoKiemTra.Equals(item.Id)).ToList();
                if (listChiIiet.Count > 0)
                {
                    item.TuChi = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (ListIdNhomNganhSelected == null || ListIdNhomNganhSelected.Contains(x.IIdMlskt))).Sum(x => x.FTuChi);
                    item.MuaHangHienVat = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (ListIdNhomNganhSelected == null || ListIdNhomNganhSelected.Contains(x.IIdMlskt))).Sum(x => x.FMuaHangCapHienVat);
                    item.DacThu = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (ListIdNhomNganhSelected == null || ListIdNhomNganhSelected.Contains(x.IIdMlskt))).Sum(x => x.FPhanCap);
                }
                item.PropertyChanged += ItemOnPropertyChanged;
            }

            TinhChungTuChecked();

            if (ChungTuCanCuModels != null && ChungTuCanCuModels.Count > 0)
            {
                foreach (var model in ChungTuCanCuModels)
                {
                    model.PropertyChanged += Item_PropertyChanged;
                }
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
            {
                var countCT = ChungTuCanCuModels.Where(x => x.IsChecked).Count();
                if (SelectedCauHinhCanCuModel.IThietLap == 2)
                {
                    var lstSoCt = string.Join(",", ChungTuCanCuModels.Where(x => x.IsChecked).Select(x => x.SoChungTu).ToList());
                    SelectedCauHinhCanCuModel.SoCTCanCu = lstSoCt;
                    SelectedCauHinhCanCuModel.TuChi =
                        ChungTuCanCuModels.Where(x => x.IsChecked).Sum(x => x.TuChi);
                    SelectedCauHinhCanCuModel.MuaHangHienVat =
                        ChungTuCanCuModels.Where(x => x.IsChecked).Sum(x => x.MuaHangHienVat);
                    SelectedCauHinhCanCuModel.DacThu =
                        ChungTuCanCuModels.Where(x => x.IsChecked).Sum(x => x.DacThu);
                    SelectedCauHinhCanCuModel.LstIdChungTuCanCu = new List<Guid>();
                    foreach (var ct in ChungTuCanCuModels)
                    {
                        if (ct.IsChecked)
                        {
                            SelectedCauHinhCanCuModel.LstIdChungTuCanCu.Add(ct.Id);
                        }
                    }
                }
                else
                {
                    if (countCT > 1)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.ChooseOneVoucher));
                        SelectedChungTuCanCuModel.IsChecked = false;
                        return;
                    }

                    if (SelectedCauHinhCanCuModel.IThietLap == 1)
                    {
                        var lstSoCt = string.Join(",", ChungTuCanCuModels.Where(x => x.IsChecked).Select(x => x.SoChungTu).ToList());
                        SelectedCauHinhCanCuModel.SoCTCanCu = lstSoCt;
                        SelectedCauHinhCanCuModel.TuChi =
                            ChungTuCanCuModels.Where(x => x.IsChecked).Sum(x => x.TuChi);
                        SelectedCauHinhCanCuModel.MuaHangHienVat =
                            ChungTuCanCuModels.Where(x => x.IsChecked).Sum(x => x.MuaHangHienVat);
                        SelectedCauHinhCanCuModel.DacThu =
                            ChungTuCanCuModels.Where(x => x.IsChecked).Sum(x => x.DacThu);
                        SelectedCauHinhCanCuModel.LstIdChungTuCanCu = new List<Guid>();
                        foreach (var ct in ChungTuCanCuModels)
                        {
                            if (ct.IsChecked)
                            {
                                SelectedCauHinhCanCuModel.LstIdChungTuCanCu.Add(ct.Id);
                            }
                        }
                    }
                    else
                    {
                        var ctSelected = ChungTuCanCuModels.Where(x => x.IsChecked).FirstOrDefault();
                        //if (ctSelected == null)
                        //{
                        //    SelectedCauHinhCanCuModel.SoCTCanCu = "";
                        //    SelectedCauHinhCanCuModel.TuChi = 0;
                        //}

                        SelectedCauHinhCanCuModel.IdChungTuCanCuLuyKe = ctSelected == null ? Guid.Empty : ctSelected.Id;
                        var lstSoCt = ctSelected == null ? "" : string.Join(",", ChungTuCanCuModels.Where(x => x.NgayChungTu <= ctSelected.NgayChungTu).Select(x => x.SoChungTu).ToList());
                        SelectedCauHinhCanCuModel.SoCTCanCu = lstSoCt;
                        SelectedCauHinhCanCuModel.TuChi = ctSelected == null ? 0 :
                            ChungTuCanCuModels.Where(x => x.NgayChungTu <= ctSelected.NgayChungTu).Sum(x => x.TuChi);
                        SelectedCauHinhCanCuModel.MuaHangHienVat = ctSelected == null ? 0 :
                            ChungTuCanCuModels.Where(x => x.NgayChungTu <= ctSelected.NgayChungTu).Sum(x => x.MuaHangHienVat);
                        SelectedCauHinhCanCuModel.DacThu = ctSelected == null ? 0 :
                            ChungTuCanCuModels.Where(x => x.NgayChungTu <= ctSelected.NgayChungTu).Sum(x => x.DacThu);
                        SelectedCauHinhCanCuModel.LstIdChungTuCanCu = new List<Guid>();
                        foreach (var ct in ChungTuCanCuModels)
                        {
                            if (ctSelected != null && ct.NgayChungTu <= ctSelected.NgayChungTu)
                            {
                                SelectedCauHinhCanCuModel.LstIdChungTuCanCu.Add(ct.Id);
                            }
                        }
                    }
                }
            }
        }

        private void TinhChungTuChecked()
        {
            if (SelectedCauHinhCanCuModel != null && SelectedCauHinhCanCuModel.LstIdChungTuCanCu != null && SelectedCauHinhCanCuModel.LstIdChungTuCanCu.Count > 0)
            {
                foreach (var item in ChungTuCanCuModels)
                {
                    if (SelectedCauHinhCanCuModel.IThietLap != 3)
                    {
                        if (SelectedCauHinhCanCuModel.LstIdChungTuCanCu.Contains(item.Id))
                        {
                            item.IsChecked = true;
                        }
                    }
                    else
                    {
                        if (SelectedCauHinhCanCuModel.IdChungTuCanCuLuyKe.Equals(item.Id))
                        {
                            item.IsChecked = true;
                        }
                    }

                }

                if (SelectedCauHinhCanCuModel.IThietLap != 3)
                {
                    var lstSoCt = string.Join(",", ChungTuCanCuModels.Where(x => x.IsChecked).Select(x => x.SoChungTu).ToList());
                    SelectedCauHinhCanCuModel.SoCTCanCu = lstSoCt;
                    SelectedCauHinhCanCuModel.TuChi =
                        ChungTuCanCuModels.Where(x => x.IsChecked).Sum(x => x.TuChi);
                    SelectedCauHinhCanCuModel.MuaHangHienVat =
                        ChungTuCanCuModels.Where(x => x.IsChecked).Sum(x => x.MuaHangHienVat);
                    SelectedCauHinhCanCuModel.DacThu =
                        ChungTuCanCuModels.Where(x => x.IsChecked).Sum(x => x.DacThu);
                }
                else
                {
                    var ctSelected = ChungTuCanCuModels.FirstOrDefault(x => x.IsChecked);
                    if (ctSelected == null)
                    {
                        SelectedCauHinhCanCuModel.SoCTCanCu = "";
                        SelectedCauHinhCanCuModel.TuChi = 0;
                        return;
                    }

                    SelectedCauHinhCanCuModel.IdChungTuCanCuLuyKe = ctSelected.Id;
                    var lstSoCt = string.Join(",", ChungTuCanCuModels.Where(x => x.NgayChungTu <= ctSelected.NgayChungTu).Select(x => x.SoChungTu).ToList());
                    SelectedCauHinhCanCuModel.SoCTCanCu = lstSoCt;
                    SelectedCauHinhCanCuModel.TuChi =
                        ChungTuCanCuModels.Where(x => x.NgayChungTu <= ctSelected.NgayChungTu).Sum(x => x.TuChi);
                    SelectedCauHinhCanCuModel.MuaHangHienVat =
                        ChungTuCanCuModels.Where(x => x.NgayChungTu <= ctSelected.NgayChungTu).Sum(x => x.MuaHangHienVat);
                    SelectedCauHinhCanCuModel.DacThu =
                        ChungTuCanCuModels.Where(x => x.NgayChungTu <= ctSelected.NgayChungTu).Sum(x => x.DacThu);
                    SelectedCauHinhCanCuModel.LstIdChungTuCanCu = new List<Guid>();
                    foreach (var ct in ChungTuCanCuModels)
                    {
                        if (ct.NgayChungTu <= ctSelected.NgayChungTu)
                        {
                            SelectedCauHinhCanCuModel.LstIdChungTuCanCu.Add(ct.Id);
                        }
                    }
                }
            }
        }

        private void SetColumnVisibilityByCanCu()
        {
            if (_selectedCauHinhCanCuModel.IIDMaChucNang.Equals(TypeCanCu.ESTIMATE))
            {
                IsDuToanVisibility = Visibility.Visible;
                IsQuyetToanVisibility = Visibility.Collapsed;
            }
            else if (_selectedCauHinhCanCuModel.IIDMaChucNang.Equals(TypeCanCu.SETTLEMENT))
            {
                IsDuToanVisibility = Visibility.Collapsed;
                IsQuyetToanVisibility = Visibility.Visible;
            }
            else if (_selectedCauHinhCanCuModel.IIDMaChucNang.Equals(TypeCanCu.ALLOCATION))
            {
                IsDuToanVisibility = Visibility.Collapsed;
                IsQuyetToanVisibility = Visibility.Collapsed;
            }
            else if (_selectedCauHinhCanCuModel.IIDMaChucNang.Equals(TypeCanCu.DEMAND))
            {
                IsDuToanVisibility = Visibility.Collapsed;
                IsQuyetToanVisibility = Visibility.Collapsed;
            }
            else
            {
                IsDuToanVisibility = Visibility.Collapsed;
                IsQuyetToanVisibility = Visibility.Collapsed;
            }
            OnPropertyChanged(nameof(IsDuToanVisibility));
            OnPropertyChanged(nameof(IsQuyetToanVisibility));
        }

        private List<string> GetListSoCtQuyetToan()
        {
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            var predicate = PredicateBuilder.True<NsQtChungTu>();
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => !string.IsNullOrEmpty(item.STongHop));
            var listCTCanCu = _iQtChungTuService.FindByCondition(predicate).ToList();
            if (listCTCanCu.Any())
            {
                return listCTCanCu.SelectMany(x => x.STongHop.Split(',', ';')).ToList();
            }
            return new List<string>();
        }

        private List<string> GetListSoCtCapPhat()
        {
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            var predicate = PredicateBuilder.True<NsCpChungTu>();
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => !string.IsNullOrEmpty(item.SDSSoChungTuTongHop));
            var listCTCanCu = _iCpChungTuService.FindByCondition(predicate).ToList();
            if (listCTCanCu.Any())
            {
                return listCTCanCu.SelectMany(x => x.SDSSoChungTuTongHop.Split(',', ';')).ToList();
            }
            return new List<string>();
        }

        private List<string> GetListSoCtSoNhuCau()
        {
            var loaiChungTu = NsSktChungTuModel.ILoaiChungTu;
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            var predicate = PredicateBuilder.True<NsSktChungTu>();
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => item.ILoaiChungTu == loaiChungTu);
            predicate = predicate.And(item => item.ILoai == DemandCheckType.DEMAND);
            predicate = predicate.And(item => !string.IsNullOrEmpty(item.SDssoChungTuTongHop));
            var listCTCanCu = _iSktChungTuService.FindByCondition(predicate).ToList();
            if (listCTCanCu.Any())
            {
                return listCTCanCu.SelectMany(x => x.SDssoChungTuTongHop.Split(',', ';')).ToList();
            }
            return new List<string>();
        }
    }
}
