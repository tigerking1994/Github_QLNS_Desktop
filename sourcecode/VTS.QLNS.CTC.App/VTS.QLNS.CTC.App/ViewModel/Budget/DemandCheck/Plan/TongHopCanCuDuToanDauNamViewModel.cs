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
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.CanCu;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck
{
    public class TongHopCanCuDuToanDauNamViewModel : DialogViewModelBase<ChungTuCanCuModel>
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
        private readonly ISktSoLieuChiTietCanCuService _sktSoLieuChiTietCanCuService;
        private readonly IDanhMucService _iDanhMucService;
        private INsDonViService _nsDonViService;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        private IMapper _mapper;
        private ICollectionView _cauHinhCanCuView;
        private ICollectionView _dataLNSView;

        public override Type ContentType => typeof(TongHopCanCu);
        public override string Name => "CĂN CỨ";
        public override string Description => "Căn cứ";
        public PlanBeginYearModel SktChungTuModel;

        private string _loaiChungTu;
        public string LoaiChungTu
        {
            get => _loaiChungTu;
            set => SetProperty(ref _loaiChungTu, value);
        }

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

                    EnableSelectedAll = _selectedCauHinhCanCuModel.IThietLap == ThietLapCanCu.NHIEU_CHUNG_TU;
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

        public string ComboboxDisplayMemberPathDanhMucNganh => nameof(DanhMucNganhModel.STen);
        public string ComboboxDisplayMemberPathLNS => nameof(NsMuclucNgansachModel.LNSDisplay);

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

        private ObservableCollection<NsMuclucNgansachModel> _dataLNS;
        public ObservableCollection<NsMuclucNgansachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public Visibility ShowColNSSD => (SktChungTuModel != null && LoaiChungTu == VoucherType.NSSD_Key) ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowColNSBD => (SktChungTuModel != null && LoaiChungTu == VoucherType.NSBD_Key) ? Visibility.Visible : Visibility.Collapsed;

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

        public RelayCommand CanCuCommand { set; get; }

        public TongHopCanCuDuToanDauNamViewModel(ICauHinhCanCuService iCauHinhCanCuService,
            ISessionService sessionService,
            INsDtChungTuService iDtChungTuService,
            INsDtChungTuChiTietService iDtChungTuChiTietService,
            INsQtChungTuService iQtChungTuService,
            INsQtChungTuChiTietService iQtChungTuChiTietService,
            ICpChungTuService iCpChungTuService,
            ICpChungTuChiTietService iCpChungTuChiTietService,
            ISktChungTuService iSktChungTuService,
            ISktChungTuChiTietService iSktChungTuChiTietService,
            ISktSoLieuChiTietCanCuService sktSoLieuChiTietCanCuService,
            IDanhMucService iDanhMucService,
            INsMucLucNganSachService iNsMucLucNganSachService,
            INsDonViService nsDonViService,
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
            _sktSoLieuChiTietCanCuService = sktSoLieuChiTietCanCuService;
            _iDanhMucService = iDanhMucService;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            _nsDonViService = nsDonViService;
            _mapper = mapper;
            CanCuCommand = new RelayCommand(obj => TinhCanCuTheoMLNS());
        }

        public override void Init()
        {
            _showColNhomNganh = Visibility.Hidden;
            _showColLNS = Visibility.Collapsed;
            _selectedCauHinhCanCuModel = null;
            LoadNhomNganh();
            LoadLNS();
            LoadCanCu();
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(ChungTuCanCuModel.IsChecked)))
            {
                OnPropertyChanged(nameof(SelectAll));
            }
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
            int loaiChungTu = int.Parse(LoaiChungTu);

            var listNsMucLucNganSach = _iNsMucLucNganSachService.FindByMLNS(yearOfWork, NSEntityStatus.ACTIVED, loaiChungTu).ToList();
            DataLNS = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listNsMucLucNganSach);
        }

        public void TinhCanCuTheoMLNS()
        {
            List<string> lnsNsbd = new List<string>();
            if (LoaiChungTu == VoucherType.NSBD_Key)
            {
                if (TypeCanCu.ESTIMATE.Equals(_selectedCauHinhCanCuModel.IIDMaChucNang))
                {
                    lnsNsbd.Add("1040100");
                    lnsNsbd.Add("1040200");
                    lnsNsbd.Add("1040300");
                }
                else if (TypeCanCu.SETTLEMENT.Equals(_selectedCauHinhCanCuModel.IIDMaChucNang)
                        || TypeCanCu.ALLOCATION.Equals(_selectedCauHinhCanCuModel.IIDMaChucNang))
                {
                    lnsNsbd.Add("1040200");
                    lnsNsbd.Add("1040300");
                }
            }

            var lstMlnsSelect = _dataLNS.Where(x => lnsNsbd.Contains(x.Lns)).Select(item => item.MlnsId).ToList();
            var lstIdMlns = String.Join(",",
                _dataLNS.Where(x => lstMlnsSelect.Count > 0 && !lstMlnsSelect.Contains(x.MlnsIdParent.GetValueOrDefault()))
                    .Select(item => item.MlnsId).ToList());
            if (!StringUtils.IsNullOrEmpty(lstIdMlns))
            {
                ListIdMlnsSelected = _iNsMucLucNganSachService.FindChildMlnsByParentLNS(lstIdMlns,
                    _sessionService.Current.YearOfWork).Select(item => item.MlnsId).ToList();
            }
            else
            {
                ListIdMlnsSelected = null;
            }

            if (_selectedCauHinhCanCuModel != null)
            {
                LoadChungTuCanCu();
                _selectedCauHinhCanCuModel.IdLns = lstIdMlns;
            }
        }

        private void LoadCanCu()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<NsCauHinhCanCu>();
            predicate = predicate.And(item => item.SModule == TypeModuleCanCu.PLAN_BEGIN_YEAR);
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate).OrderBy(n => n.INamCanCu);
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
                    var predicatecc = PredicateBuilder.True<NsDtdauNamChungTuChungTuCanCu>();
                    predicatecc = predicatecc.And(x => x.IIdMaDonVi.Equals(SktChungTuModel.Id_DonVi));
                    //predicatecc = predicatecc.And(x => x.iLoai.Equals(item.CanCu));// iID_MaChucNang
                    predicatecc = predicatecc.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
                    predicatecc = predicatecc.And(x => x.IIdCanCu.Equals(item.Id));
                    predicatecc = predicatecc.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

                    var listCTCC = _sktSoLieuChiTietCanCuService.FindByCondition(predicatecc).ToList();
                    if (listCTCC != null && listCTCC.Count > 0)
                    {
                        item.LstIdChungTuCanCu = item.LstIdChungTuCanCu == null ? new List<Guid>() : item.LstIdChungTuCanCu;
                        foreach (var x in listCTCC)
                        {
                            if (x.IIdCtcanCu.HasValue)
                                item.LstIdChungTuCanCu.Add(x.IIdCtcanCu.Value);

                            if (item.IThietLap == 3 && x.IIdCtcanCu.HasValue)
                            {
                                item.IdChungTuCanCuLuyKe = x.IIdCtcanCu.Value;
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

        private void LoadChungTuCanCuDuToan()
        {
            var idDonVi = SktChungTuModel.Id_DonVi;
            int loaiChungTu = int.Parse(LoaiChungTu);
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(item => item.SDsidMaDonVi.Split(',').Contains(idDonVi));
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => item.ILoaiChungTu == loaiChungTu);
            if (!string.IsNullOrEmpty(SktChungTuModel.DSSoChungTuTongHop) || SktChungTuModel.Id_DonVi == _sessionService.Current.IdDonVi)
            {
                predicate = predicate.And(item => item.ILoai == SoChungTuType.ReceiveEstimate);
            }
            else
            {
                predicate = predicate.And(item => item.ILoai == SoChungTuType.EstimateDivision);
            }
            var listCTCanCu = _iDtChungTuService.FindByCondition(predicate);
            ChungTuCanCuModels = _mapper.Map<ObservableCollection<ChungTuCanCuModel>>(listCTCanCu);
            foreach (var item in ChungTuCanCuModels)
            {
                var listChiIiet = listCTCanCu.Where(x => x.Id.Equals(item.Id)).Select(x => x.ChungTuChiTiets).FirstOrDefault();
                if (listChiIiet != null)
                {
                    if (loaiChungTu == int.Parse(VoucherType.NSBD_Key))
                    {
                        listChiIiet.Where(x => x.FHangNhap == 0 && x.FHangMua == 0 && x.FTuChi != 0).ForAll(x =>
                        {
                            if (x.SLns.Contains("1040100") || x.SLns.Contains("1040200"))
                            {
                                x.FHangNhap = x.FTuChi;
                            }
                            else if (x.SLns.Contains("1040300"))
                            {
                                x.FHangMua = x.FTuChi;

                            }
                        });
                    }

                    item.TuChi = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (ListIdMlnsSelected == null || ListIdMlnsSelected.Contains(x.IIdMlns.GetValueOrDefault()))).Sum(x => x.FTuChi);
                    item.HangNhap = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (ListIdMlnsSelected == null || ListIdMlnsSelected.Contains(x.IIdMlns.GetValueOrDefault()))).Sum(x => x.FHangNhap);
                    item.HangMua = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (ListIdMlnsSelected == null || ListIdMlnsSelected.Contains(x.IIdMlns.GetValueOrDefault()))).Sum(x => x.FHangMua);
                    item.PhanCap = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (ListIdMlnsSelected == null || ListIdMlnsSelected.Contains(x.IIdMlns.GetValueOrDefault()))).Sum(x => x.FPhanCap);
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

        private void LoadChungTuCanCuQuyetToan()
        {
            var idDonVi = SktChungTuModel.Id_DonVi;
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            var predicate = PredicateBuilder.True<NsQtChungTu>();
            predicate = predicate.And(item => item.IIdMaDonVi == idDonVi);
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            // predicate = predicate.And(item => item.BDaTongHop.HasValue && item.BDaTongHop.Value);
            var listCTCanCu = _iQtChungTuService.FindByCondition(predicate);
            ChungTuCanCuModels = _mapper.Map<ObservableCollection<ChungTuCanCuModel>>(listCTCanCu);

            var listChiIiet = _iQtChungTuChiTietService.FindByCondition(x => listCTCanCu.Select(d => d.Id).Contains(x.IIdQtchungTu.Value)).ToList();
            ChungTuCanCuModels.AsParallel().ForAll(item =>
            {
                var tempList = listChiIiet.Where(x => item.Id == x.IIdQtchungTu).ToList();
                if (tempList.Any())
                {
                    item.TuChi = tempList.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (ListIdMlnsSelected == null || ListIdMlnsSelected.Count == 0 || ListIdMlnsSelected.Contains(x.IIdMlns))).Sum(x => x.FTuChiPheDuyet);
                    item.HangNhap = tempList.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (x.SLns == "1040100" || x.SLns == "1040200") && (ListIdMlnsSelected == null || ListIdMlnsSelected.Count == 0 || ListIdMlnsSelected.Contains(x.IIdMlns))).Sum(x => x.FTuChiPheDuyet);
                    item.HangMua = tempList.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (x.SLns == "1040300") && (ListIdMlnsSelected == null || ListIdMlnsSelected.Count == 0 || ListIdMlnsSelected.Contains(x.IIdMlns))).Sum(x => x.FTuChiPheDuyet); ;
                    item.PhanCap = 0;
                    item.MuaHangHienVat = item.TuChi;
                }
                item.PropertyChanged += ItemOnPropertyChanged;
            });

            TinhChungTuChecked();

            if (ChungTuCanCuModels != null && ChungTuCanCuModels.Count > 0)
            {
                foreach (var model in ChungTuCanCuModels)
                {
                    model.PropertyChanged += Item_PropertyChanged;
                }
            }
        }

        private List<Guid> ListIdChungTuTongHop()
        {
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            var predicate = PredicateBuilder.True<NsCpChungTu>();
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => !string.IsNullOrEmpty(item.SDSSoChungTuTongHop));
            var listChungTuTongHop = _iCpChungTuService.FindByCondition(predicate);
            if (listChungTuTongHop != null && listChungTuTongHop.Count() > 0)
            {
                List<string> listSoChungTu = new List<string>();
                foreach (var item in listChungTuTongHop)
                {
                    listSoChungTu.AddRange(item.SDSSoChungTuTongHop.Split(",").ToList());
                }
                var predicateChungTu = PredicateBuilder.True<NsCpChungTu>();
                predicateChungTu = predicateChungTu.And(item => item.INamLamViec == namChungTu);
                predicateChungTu = predicateChungTu.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
                predicateChungTu = predicateChungTu.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
                predicateChungTu = predicateChungTu.And(item => listSoChungTu.Count() > 0 && listSoChungTu.Contains(item.SSoChungTu));
                var listChungTuCon = _iCpChungTuService.FindByCondition(predicateChungTu);
                return (listChungTuCon != null && listChungTuCon.Count() > 0) ? listChungTuCon.Select(n => n.Id).ToList() : new List<Guid>();
            }
            else
            {
                return new List<Guid>();
            }
        }

        private void LoadChungTuCanCuCapPhat()
        {
            var idDonVi = SktChungTuModel.Id_DonVi;
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            List<Guid> listId = ListIdChungTuTongHop();
            DonVi donvi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            var predicate = PredicateBuilder.True<NsCpChungTu>();
            predicate = predicate.And(item => item.SDsidMaDonVi.Contains(idDonVi));
            predicate = predicate.And(item => !item.SDsidMaDonVi.Contains(donvi0.IIDMaDonVi));
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => listId.Count() > 0 && listId.Contains(item.Id));
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            var listCTCanCu = _iCpChungTuService.FindByCondition(predicate);
            ChungTuCanCuModels = _mapper.Map<ObservableCollection<ChungTuCanCuModel>>(listCTCanCu);
            foreach (var item in ChungTuCanCuModels)
            {
                var listChiIiet = _iCpChungTuChiTietService.FindByCondition(x => x.IIdCtcapPhat.Equals(item.Id));
                if (listChiIiet != null)
                {
                    item.TuChi = listChiIiet.Where(x => x.IIdMaDonVi.Equals(idDonVi) && (ListIdMlnsSelected == null || ListIdMlnsSelected.Contains(x.IIdMlns))).Sum(x => x.FTuChi) ?? 0;
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
            var idDonVi = SktChungTuModel.Id_DonVi;
            int loaiChungTu = int.Parse(LoaiChungTu);
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            var predicate = PredicateBuilder.True<NsSktChungTu>();
            predicate = predicate.And(item => item.IIdMaDonVi.Contains(idDonVi));
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => item.ILoaiChungTu == loaiChungTu);
            predicate = predicate.And(item => item.ILoai == DemandCheckType.DEMAND);
            var listCTCanCu = _iSktChungTuService.FindByCondition(predicate);
            ChungTuCanCuModels = _mapper.Map<ObservableCollection<ChungTuCanCuModel>>(listCTCanCu);
            foreach (var item in ChungTuCanCuModels)
            {
                var listChiIiet = _iSktChungTuChiTietService.FindByCondition(x => x.IIdCtsoKiemTra.Equals(item.Id));
                if (listChiIiet != null)
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
            var idDonVi = SktChungTuModel.Id_DonVi;
            var namChungTu = SelectedCauHinhCanCuModel.INamCanCu;
            int loaiChungTu = int.Parse(LoaiChungTu);
            var predicate = PredicateBuilder.True<NsSktChungTu>();
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => item.ILoai == DemandCheckType.CHECK);
            predicate = predicate.And(item => item.ILoaiChungTu == loaiChungTu);
            var listCTCanCu = _iSktChungTuService.FindByCondition(predicate);
            ChungTuCanCuModels = _mapper.Map<ObservableCollection<ChungTuCanCuModel>>(listCTCanCu);
            foreach (var item in ChungTuCanCuModels)
            {
                var listChiIiet = _iSktChungTuChiTietService.FindByCondition(x => x.IIdCtsoKiemTra.Equals(item.Id));
                if (listChiIiet != null)
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
                            if (ctSelected != null && ((ct.NgayChungTu <= ctSelected.NgayChungTu) ||
                                ((ct.NgayChungTu == null || ctSelected.NgayChungTu == null) && ct.NgayQuyetDinh <= ctSelected.NgayQuyetDinh)))
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
                    var ctSelected = ChungTuCanCuModels.Where(x => x.IsChecked).FirstOrDefault();
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
    }
}
