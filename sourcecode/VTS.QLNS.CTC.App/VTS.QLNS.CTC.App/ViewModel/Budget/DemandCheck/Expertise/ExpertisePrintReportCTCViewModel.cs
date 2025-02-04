using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Expertise
{
    public class ExpertisePrintReportCTCViewModel : ViewModelBase
    {
        private ICpChungTuService _chungTuService;
        private INsDonViService _donViService;
        private IMapper _mapper;
        private ICollectionView _listNganhView;
        private INsDonViService _nSDonViService;
        private ISessionService _sessionService;
        private IExportService _exportService;
        private ISktSoLieuService _sktSoLieuService;
        private IDanhMucService _danhMucService;
        private ISktNganhThamDinhService _thamDinhService;
        private ISktNganhThamDinhChiTietService _thamDinhChiTietService;
        private IDmChuKyService _dmChuKyService;
        private ISktMucLucService _iSktMucLucService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private readonly ILog _logger;
        private DmChuKy _dmChuKy;
        private string _diaDiem;

        public string IdChungTuCTC = string.Empty;
        public override Type ContentType => typeof(View.Budget.DemandCheck.Expertise.ExpertisePrintReportCTC);
        public ObservableCollection<CheckBoxItem> ListNNganh { get; set; }
        public int LoaiNganSach { get; set; }

        private ObservableCollection<ComboboxItem> _DataDonViTinh;
        public ObservableCollection<ComboboxItem> DataDonViTinh
        {
            get => _DataDonViTinh;
            set => SetProperty(ref _DataDonViTinh, value);
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }

        private string _tieuDe1;
        public string TieuDe1
        {
            get => _tieuDe1;
            set => SetProperty(ref _tieuDe1, value);
        }

        private string _tieuDe2;
        public string TieuDe2
        {
            get => _tieuDe2;
            set => SetProperty(ref _tieuDe2, value);
        }
        private string _tieuDe3;
        public string TieuDe3
        {
            get => _tieuDe3;
            set => SetProperty(ref _tieuDe3, value);
        }

        public string SelectedCountNganh
        {
            get
            {
                if (IsChuyenNganh)
                {
                    return ListNNganh != null
                        ? $"CHUYÊN NGÀNH ({ListNNganh.Count(item => item.IsChecked)}/{ListNNganh.Count})"
                        : "CHUYÊN NGÀNH (0/0)";
                }
                return ListNNganh != null
                    ? $"NGÀNH ({ListNNganh.Count(item => item.IsChecked)}/{ListNNganh.Count})"
                    : "NGÀNH (0/0)";
            }
        }

        public bool? SelectAllNganh
        {
            get
            {
                if (ListNNganh != null)
                {
                    var selected = ListNNganh.Select(item => item.IsChecked).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ListNNganh);
                    OnPropertyChanged();
                }
            }
        }

        private static void SelectAll(bool select, ObservableCollection<CheckBoxItem> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        public bool IsEnableButtonPrint
        {
            get
            {
                if (ListNNganh != null && ListNNganh.Where(n => n.IsChecked).Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _listNganhView.Refresh();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataLoaiNganSach;
        public ObservableCollection<ComboboxItem> DataLoaiNganSach
        {
            get => _dataLoaiNganSach;
            set => SetProperty(ref _dataLoaiNganSach, value);
        }

        private ComboboxItem _selectedLoaiNganSach;
        public ComboboxItem SelectedLoaiNganSach
        {
            get => _selectedLoaiNganSach;
            set
            {
                if (SetProperty(ref _selectedLoaiNganSach, value) && _selectedLoaiNganSach != null)
                {
                    LoadNganh();
                }
            }
        }

        private bool _isChuyenNganh;
        public bool IsChuyenNganh
        {
            get => _isChuyenNganh;
            set
            {
                if (SetProperty(ref _isChuyenNganh, value))
                {
                    LoadNganh();
                }
                OnPropertyChanged(nameof(SelectedCountNganh));
                OnPropertyChanged(nameof(SelectAllNganh));
            }
        }

        private ObservableCollection<CheckBoxItem> _listChuyenNganh = new ObservableCollection<CheckBoxItem>();
        public ObservableCollection<CheckBoxItem> ListChuyenNganh
        {
            get => _listChuyenNganh;
            set => SetProperty(ref _listChuyenNganh, value);
        }

        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public ExpertisePrintReportCTCViewModel(
         ICpChungTuService chungTuService,
         INsDonViService donViService,
         IMapper mapper,
         INsDonViService nSDonViService,
         ISessionService sessionService,
         ILog logger,
         ISktSoLieuService sktSoLieuService,
         IDanhMucService danhMucService,
         ISktNganhThamDinhChiTietService thamDinhChiTietService,
         ISktNganhThamDinhService thamDinhService,
         IExportService exportService,
         IDmChuKyService dmChuKyService,
         ISktMucLucService iSktMucLucService,
         DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _chungTuService = chungTuService;
            _donViService = donViService;
            _mapper = mapper;
            _nSDonViService = nSDonViService;
            _sessionService = sessionService;
            _exportService = exportService;
            _sktSoLieuService = sktSoLieuService;
            _danhMucService = danhMucService;
            _thamDinhChiTietService = thamDinhChiTietService;
            _thamDinhService = thamDinhService;
            _dmChuKyService = dmChuKyService;
            _iSktMucLucService = iSktMucLucService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintPDFCommand = new RelayCommand(o => PrintPDF());
            PrintExcelCommand = new RelayCommand(o => PrintExcel());
            PrintBrowserCommand = new RelayCommand(o => OnPrintBrowser());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public List<string> GetNganhByDetail()
        {
            if (string.IsNullOrEmpty(IdChungTuCTC))
            {
                return new List<string>();
            }
            var predicate = PredicateBuilder.True<NsSktNganhThamDinhChiTiet>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdCtnganhThamDinh == Guid.Parse(IdChungTuCTC));
            List<NsSktNganhThamDinhChiTiet> listDetail = _thamDinhChiTietService.FindByCondition(predicate).ToList();
            if (!listDetail.IsEmpty())
            {
                var param = listDetail.Select(n => n.IIdMucLuc).ToList();
                var predicateMucLuc = PredicateBuilder.True<NsSktMucLuc>();
                predicateMucLuc = predicateMucLuc.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                predicateMucLuc = predicateMucLuc.And(x => param.Contains(x.IIDMLSKT));
                List<NsSktMucLuc> listMucLuc = _iSktMucLucService.FindByCondition(predicateMucLuc).ToList();
                return !listMucLuc.IsEmpty() ? listMucLuc.Select(n => n.SNg).ToList() : new List<string>();
            }
            else
            {
                return new List<string>();
            }
        }

        public void LoadNganh()
        {
            if (SelectedLoaiNganSach == null)
            {
                return;
            }
            var predicateCt = PredicateBuilder.True<NsSktNganhThamDinh>();
            predicateCt = predicateCt.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicateCt = predicateCt.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicateCt = predicateCt.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicateCt = predicateCt.And(x => x.ILoaiChungTu == int.Parse(SelectedLoaiNganSach.ValueItem));
            predicateCt = predicateCt.And(x => x.ILoai == LoaiNganhThamDinh.CTCTCDN);
            NsSktNganhThamDinh ctc = _thamDinhService.FindByCondition(predicateCt).ToList().FirstOrDefault();
            if (ctc == null)
            {
                return;
            }
            IdChungTuCTC = ctc.Id.ToString();
            List<string> listNganh = GetNganhByDetail();
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(item => item.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(item => item.ITrangThai == StatusType.ACTIVE);

            // lấy list chuyên ngành
            var predicateChuyenNganh = predicate.And(item => VoucherType.DM_Nganh.Equals(item.SType));
            predicateChuyenNganh = predicateChuyenNganh.And(item => listNganh.Contains(item.IIDMaDanhMuc));
            var lstDmChuyenNganh = _danhMucService.FindByCondition(predicateChuyenNganh).ToList();
            var result = lstDmChuyenNganh.Where(item => item.ITrangThai == 1).Select(item => new CheckBoxItem
            {
                ValueItem = item.IIDMaDanhMuc,
                DisplayItem = string.Join("-", item.IIDMaDanhMuc, item.STen),
                NameItem = item.STen
            }).OrderBy(item => item.ValueItem);
            ListChuyenNganh = new ObservableCollection<CheckBoxItem>(result);
            if (IsChuyenNganh)
            {
                ListNNganh = ListChuyenNganh;
            }
            else
            {
                var predicateNhomNganh = predicate.And(item => VoucherType.VOCHER_TYPE.Equals(item.SType));
                var lstDmNganh = _danhMucService.FindByCondition(predicateNhomNganh).ToList();
                lstDmNganh = lstDmNganh.Where(item => listNganh.Any(x => item.SGiaTri.Contains(x))).ToList();
                var resultN = lstDmNganh.Where(item => item.ITrangThai == 1).Select(item => new CheckBoxItem
                {
                    ValueItem = item.SGiaTri,
                    DisplayItem = string.Join("-", item.IIDMaDanhMuc, item.STen),
                    NameItem = item.STen
                }).OrderBy(item => item.ValueItem);
                ListNNganh = new ObservableCollection<CheckBoxItem>(resultN);
            }

            // Filter
            _listNganhView = CollectionViewSource.GetDefaultView(ListNNganh);
            _listNganhView.Filter = obj => string.IsNullOrWhiteSpace(_searchDonVi)
                                                  || (obj is CheckBoxItem item &&
                                                      item.DisplayItem.Contains(_searchDonVi, StringComparison.OrdinalIgnoreCase));
            foreach (var org in ListNNganh)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(SelectedCountNganh));
                    OnPropertyChanged(nameof(SelectAllNganh));
                    OnPropertyChanged(nameof(IsEnableButtonPrint));
                };
            }
            OnPropertyChanged(nameof(ListNNganh));
            OnPropertyChanged(nameof(SelectAllNganh));
            OnPropertyChanged(nameof(SelectedCountNganh));
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _nSDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private List<NsSktMucLuc> GetSKTMucLucIdByNganh(string chuyenNganh)
        {
            if (string.IsNullOrEmpty(chuyenNganh))
            {
                return new List<NsSktMucLuc>();
            }
            var predicateMucLuc = PredicateBuilder.True<NsSktMucLuc>();
            predicateMucLuc = predicateMucLuc.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicateMucLuc = predicateMucLuc.And(x => chuyenNganh.Contains(x.SNg));
            List<NsSktMucLuc> listMucLuc = _iSktMucLucService.FindByCondition(predicateMucLuc).ToList();
            return listMucLuc;
        }

        private List<NsSktMucLuc> GetSKTMucLuc()
        {
            var predicateMucLuc = PredicateBuilder.True<NsSktMucLuc>();
            predicateMucLuc = predicateMucLuc.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            List<NsSktMucLuc> listMucLuc = _iSktMucLucService.FindByCondition(predicateMucLuc).ToList();
            return listMucLuc;
        }

        private List<ExpertisePrintCTCReportModel> FormatDataExportDonVi(List<ExpertiseModelDetailModel> input)
        {
            List<ExpertisePrintCTCReportModel> result = new List<ExpertisePrintCTCReportModel>();
            if (input == null || input.Count() == 0)
            {
                return result;
            }
            List<string> idDonVi = new List<string>();
            if (SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
            {
                idDonVi = input.Where(n => !string.IsNullOrEmpty(n.IdDonVi)).Select(n => n.IdDonVi).Distinct().ToList();
            }
            else
            {
                idDonVi.Add(string.Empty);
            }

            List<string> listKyHieu = input.Select(n => n.KyHieu).Distinct().ToList();
            foreach (string kyHieu in listKyHieu)
            {
                ExpertisePrintCTCReportModel item = new ExpertisePrintCTCReportModel();
                ExpertiseModelDetailModel source = input.FirstOrDefault(n => n.KyHieu == kyHieu);
                if (source != null)
                {
                    item.KyHieu = source.KyHieu;
                    item.STT = source.STT;
                    item.IdParent = source.IdParent;
                    item.IdMucLuc = source.IdMucLuc;
                    item.STTBC = source.STTBC;
                    item.MoTa = source.MoTa;
                    item.Nganh = source.Nganh;
                    if (idDonVi.Count() >= 1)
                    {
                        item.TuChi1 = input.Where(n => n.KyHieu == kyHieu && n.IdDonVi == idDonVi[0]).Sum(n => n.TuChi);
                        item.SuDungTonKho1 = input.Where(n => n.KyHieu == kyHieu && n.IdDonVi == idDonVi[0]).Sum(n => n.SuDungTonKho);
                        item.ChiDacThuNganhPhanCap1 = input.Where(n => n.KyHieu == kyHieu && n.IdDonVi == idDonVi[0]).Sum(n => n.ChiDacThuNganhPhanCap);
                    }
                    if (idDonVi.Count() >= 2)
                    {
                        item.TuChi2 = input.Where(n => n.KyHieu == kyHieu && n.IdDonVi == idDonVi[1]).Sum(n => n.TuChi);
                    }
                    if (idDonVi.Count() >= 3)
                    {
                        item.TuChi3 = input.Where(n => n.KyHieu == kyHieu && n.IdDonVi == idDonVi[2]).Sum(n => n.TuChi);
                    }
                    result.Add(item);
                }
            }
            result = result.Where(n => (n.TuChi1 != 0 || n.TuChi2 != 0 || n.TuChi3 != 0 || n.SuDungTonKho1 != 0 || n.ChiDacThuNganhPhanCap1 != 0)).ToList();
            List<string> listKyHieuParent = StringUtils.GetListKyHieuParent(result.Select(n => n.KyHieu).ToList());

            List<NsSktMucLuc> listMucLuc = GetSKTMucLuc();
            listMucLuc = listMucLuc.Where(n => (listKyHieuParent.Contains(n.SKyHieu) && n.BHangCha)).ToList();

            foreach (var item in listMucLuc)
            {
                result.Add(new ExpertisePrintCTCReportModel
                {
                    IsHangCha = true,
                    KyHieu = item.SKyHieu,
                    STT = item.SSTT,
                    IdMucLuc = item.IIDMLSKT,
                    IdParent = item.IIDMLSKTCha.HasValue ? item.IIDMLSKTCha.Value : Guid.Empty,
                    MoTa = item.SMoTa,
                    STTBC = item.SSttBC
                });
            }


            CalculateDataDonVi(ref result);
            return result.OrderBy(n => n.STTBC).ToList();
        }

        private void CalculateDataDonVi(ref List<ExpertisePrintCTCReportModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x => { x.TuChi1 = 0; x.TuChi2 = 0; x.TuChi3 = 0; x.ChiDacThuNganhPhanCap1 = 0; x.SuDungTonKho1 = 0; return x; }).ToList();
            foreach (var item in listData.Where(x => !x.IsHangCha && !x.IsDeleted && (x.TuChi1 != 0 || x.TuChi2 != 0 || x.TuChi3 != 0
            || x.SuDungTonKho1 != 0 || x.ChiDacThuNganhPhanCap1 != 0)))
            {
                CalculateParentDonVi(ref listData, item, item);
            }
        }

        private void CalculateParentDonVi(ref List<ExpertisePrintCTCReportModel> listData, ExpertisePrintCTCReportModel currentItem, ExpertisePrintCTCReportModel selfItem)
        {
            var parentItem = listData.Where(x => x.IdMucLuc == currentItem.IdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi1 += selfItem.TuChi1;
            parentItem.TuChi2 += selfItem.TuChi2;
            parentItem.TuChi3 += selfItem.TuChi3;
            parentItem.SuDungTonKho1 += selfItem.SuDungTonKho1;
            parentItem.ChiDacThuNganhPhanCap1 += selfItem.ChiDacThuNganhPhanCap1;
            CalculateParentDonVi(ref listData, parentItem, selfItem);
        }

        public List<NsSktNganhThamDinh> GetListChungTu()
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTCTCDN);
            predicate = predicate.And(x => x.ILoaiChungTu == LoaiNganSach);
            List<NsSktNganhThamDinh> chungTuDeNghi = _thamDinhService.FindByCondition(predicate).ToList();
            return (chungTuDeNghi != null && chungTuDeNghi.Count() > 0) ? chungTuDeNghi : new List<NsSktNganhThamDinh>();
        }

        public List<ThDChungTuChiTietQuery> GetValueTuChiPrev()
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoai == LoaiNganhThamDinh.CTCTCDN);
            predicate = predicate.And(x => x.ILoaiChungTu == LoaiNganSach);
            NsSktNganhThamDinh chungTuDeNghi = _thamDinhService.FindByCondition(predicate).FirstOrDefault();
            if (chungTuDeNghi == null)
            {
                return new List<ThDChungTuChiTietQuery>();
            }
            else
            {
                List<ThDChungTuChiTietQuery> data = _thamDinhChiTietService.FindByCondition(_sessionService.Current.YearOfWork, chungTuDeNghi.Id.ToString(),
                    _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();
                return data.Where(n => n.TuChi != 0).ToList();
            }
        }

        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }

        private List<NsSktMucLuc> GetLstTittle()
        {
            List<NsSktMucLuc> result = new List<NsSktMucLuc>();
            result.Add(new NsSktMucLuc { SMoTa = string.Empty });
            return result;
        }

        private List<NsSktMucLuc> GetLstTongTittle()
        {
            List<NsSktMucLuc> result = new List<NsSktMucLuc>();
            result.Add(new NsSktMucLuc { SMoTa = string.Empty });
            return result;
        }

        public List<ExportResult> GetResultExportReportData(ExportType exportType = ExportType.PDF, ObservableCollection<CheckBoxItem> nganhItem = null)
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_NGANHTHAMDINH_CTC) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            List<ExportResult> exportResults = new List<ExportResult>();
            string templateFileName = string.Empty;
            string fileNamePrefix;
            string fileNameWithoutExtension;
            int donViTinh = GetDonViTinh();
            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
            var yearOfWork = _sessionService.Current.YearOfWork;
            var tenNganh = "";
            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            List<NsSktNganhThamDinh> listChungTu = GetListChungTu();
            var lstIdCodeSelected = "";
            if (nganhItem != null)
            {
                lstIdCodeSelected = string.Join(",", nganhItem.Select(item => item.ValueItem).ToList());
            }
            List<ThDChungTuChiTietQuery> listDataDonVi = _thamDinhChiTietService.FindByConditionReport(_sessionService.Current.YearOfWork,
                string.Join(",", listChungTu.Select(n => n.Id).ToList()), lstIdCodeSelected,
                _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).Where(n => n.TuChi != 0 || n.HuyDongCTC != 0 || n.HuyDongNganh != 0).ToList();

            List<ThDChungTuChiTietQuery> dataDeNghi = GetValueTuChiPrev();
            dataDeNghi = dataDeNghi.Where(x => lstIdCodeSelected.Contains(x.Nganh)).ToList();
            if (dataDeNghi != null && dataDeNghi.Count > 0)
            {
                foreach (ThDChungTuChiTietQuery item in listDataDonVi)
                {
                    ThDChungTuChiTietQuery valueItem = dataDeNghi.Where(n => n.IdMucLuc == item.IdMucLuc && n.IdDonVi == item.IdDonVi).FirstOrDefault();
                    if (valueItem != null)
                    {
                        item.TuChiPrev = valueItem.TuChi;
                    }
                }
            }
            var listIdDonVi = string.Join(",", listDataDonVi.Select(x => x.IdDonVi).Distinct().ToList());
            var listDonVi = _nSDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList().OrderBy(n => n.IIDMaDonVi);
            var listIdMucLuc = listDataDonVi.Select(x => x.IdMucLuc).Distinct().ToList();
            List<NsSktMucLuc> sktMucLucs = _iSktMucLucService
                .FindByCondition(x => listIdMucLuc.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).OrderBy(x => x.SNg).ToList();
            List<NsSktMucLuc> lstMlResult = new List<NsSktMucLuc>();
            List<string> lstNg = new List<string>();
            foreach (var mucluc in sktMucLucs)
            {
                if (lstNg.Count <= 0 || !lstNg.Contains(mucluc.SNg))
                {
                    lstNg.Add(mucluc.SNg);
                }

                //if (lstMlResult.Count() == 0 || lstMlResult.Where(n => n.SNg == mucluc.SNg).Count() == 0)
                //{
                //    NsSktMucLuc mlTong = new NsSktMucLuc();
                //    mlTong.SNg = mucluc.SNg;
                //    mlTong.SMoTa = "Tổng";
                //    lstMlResult.Add(mlTong);
                //}
                lstMlResult.Add(mucluc);
            }
            string path = "";
            int pageSize = 4;

            var groupSktMucLuc = lstMlResult.GroupBy(n => n.SNg).Select(x => new { Key = x.Key, Total = x.Count() }).ToList();
            var count = 0;
            foreach (var item in groupSktMucLuc)
            {
                lstMlResult.Insert(count, new NsSktMucLuc { SNg = item.Key, SMoTa = "+" });
                count += item.Total + 1;
            }
            var listMucLucSplits = SplitList(lstMlResult, pageSize).ToList();
            for (int i = 0; i < listMucLucSplits.Count; i++)
            {
                List<HeaderReportNganhThamDinhModel> headers = new List<HeaderReportNganhThamDinhModel>();
                var lstNganhHeader = listMucLucSplits[i].Select(x => x.SNg).Distinct().ToList();
                int columnStart = 6;
                int columnChildStart = 6;
                foreach (var nganhHeader in lstNganhHeader)
                {
                    var nganh = ListChuyenNganh.FirstOrDefault(x => x.ValueItem.Contains(nganhHeader));
                    List<string> lstMlHeader = listMucLucSplits[i].Where(x => x.SNg.Equals(nganhHeader)).Select(x => x.SMoTa).ToList();
                    var mergeRange = "";
                    var columnStartName = GetExcelColumnName(columnStart);
                    var columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart - 1);
                    mergeRange = columnStartName + "7" + ":" + columnEndName + "7";
                    // columnStart += lstMlHeader.Count();
                    HeaderReportNganhThamDinhModel hd = new HeaderReportNganhThamDinhModel();
                    hd.TenNganh = nganh != null ? nganh.NameItem : "";
                    hd.LstMucLuc = new List<NsSktMucLuc>();
                    hd.LstNganhHeader = new List<NsSktMucLuc>();
                    hd.LstTitle = new List<NsSktMucLuc>();
                    hd.MergeRange = mergeRange;
                    int j = 0;
                    foreach (var mlHeader in lstMlHeader)
                    {
                        List<NsSktMucLuc> listTileChild = new List<NsSktMucLuc>();

                        if (mlHeader == "Tổng")
                        {
                            var columnChildStartName = GetExcelColumnName(columnChildStart);
                            //var columnChildEndName = GetExcelColumnName(lstMlHeader.Count() + columnChildStart - 1);
                            string mergeChildRange = columnChildStartName + "8" + ":" + columnChildStartName + "8";
                            hd.MergeRangeChild = mergeChildRange;
                            listTileChild = GetLstTongTittle();
                            columnChildStart += 2;
                        }
                        else
                        {
                            var columnChildStartName = GetExcelColumnName(columnChildStart);
                            var columnChildEndName = GetExcelColumnName(lstMlHeader.Count() + columnChildStart - 1);
                            string mergeChildRange = columnChildStartName + "8" + ":" + columnChildEndName + "8";
                            hd.MergeRangeChild2 = mergeChildRange;
                            listTileChild = GetLstTittle();
                            columnChildStart += 3;
                        }
                        int k = 0;
                        foreach (NsSktMucLuc itemTileChild in listTileChild)
                        {
                            NsSktMucLuc mlhd = new NsSktMucLuc();
                            if (j == 0)
                            {
                                mlhd = new NsSktMucLuc();
                                mlhd.SMoTa = nganh != null ? nganh.NameItem : "";
                                mlhd.SSttBC = "1";
                                hd.LstNganhHeader.Add(mlhd);
                            }
                            else
                            {
                                mlhd = new NsSktMucLuc();
                                mlhd.SMoTa = "";
                                mlhd.SSttBC = "2";
                                hd.LstNganhHeader.Add(mlhd);
                            }
                            mlhd = new NsSktMucLuc();
                            mlhd.SMoTa = mlHeader;
                            //if (k == 0)
                            //{
                            //    mlhd.SSttBC = "1";
                            //}
                            //else
                            //{
                            mlhd.SSttBC = "2";
                            //}
                            k++;
                            hd.LstMucLuc.Add(mlhd);
                        }

                        if (mlHeader == "Tổng")
                        {
                            hd.LstTitle.AddRange(GetLstTongTittle());
                        }
                        else
                        {
                            hd.LstTitle.AddRange(GetLstTittle());
                        }
                        j++;
                    }
                    headers.Add(hd);
                }

                int stt = 1;
                List<ReportNganhThamDinhDataKiemTra> results = new List<ReportNganhThamDinhDataKiemTra>();
                foreach (var dv in listDonVi)
                {
                    ReportNganhThamDinhDataKiemTra kq = new ReportNganhThamDinhDataKiemTra();
                    kq.Stt = stt++;
                    kq.MaDonVi = dv.IIDMaDonVi;
                    kq.TenDonVi = dv.TenDonVi;
                    kq.TongCong = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChi) / donViTinh;
                    kq.LstGiaTri = new List<NsSktNganhThamDinhChiTiet>();
                    foreach (var ml in listMucLucSplits[i])
                    {
                        NsSktNganhThamDinhChiTiet tongCTC = new NsSktNganhThamDinhChiTiet();
                        NsSktNganhThamDinhChiTiet giaTri = new NsSktNganhThamDinhChiTiet();
                        if (ml.IIDMLSKT != Guid.Empty)
                        {

                            giaTri = new NsSktNganhThamDinhChiTiet();
                            var gtDonVi = listDataDonVi.Where(x => x.Nganh.Equals(ml.SNg) && x.IdDonVi == dv.IIDMaDonVi && x.IdMucLuc == ml.IIDMLSKT).Sum(x => x.TuChi);
                            giaTri.FTuChi = gtDonVi / donViTinh;
                            kq.LstGiaTri.Add(giaTri);
                        }
                        else
                        {
                            tongCTC = new NsSktNganhThamDinhChiTiet();
                            var gtDonVi = listDataDonVi.Where(x => x.Nganh.Equals(ml.SNg) && x.IdDonVi == dv.IIDMaDonVi).Sum(x => x.TuChi);
                            tongCTC.FTuChi = gtDonVi / donViTinh;
                            kq.LstGiaTri.Add(tongCTC);
                        }
                    }
                    results.Add(kq);
                }
                List<ReportNganhThamDinhDataKiemTra> resultsTotal = new List<ReportNganhThamDinhDataKiemTra>();
                ReportNganhThamDinhDataKiemTra total = new ReportNganhThamDinhDataKiemTra();
                total.LstGiaTriTotal = new List<NsSktNganhThamDinhChiTiet>();
                int countResult = 1;
                foreach (var itemResult in results)
                {
                    for (int k = 0; k < itemResult.LstGiaTri.Count(); k++)
                    {
                        if (countResult == 1)
                        {
                            total.LstGiaTriTotal.Add(new NsSktNganhThamDinhChiTiet { FTuChi = itemResult.LstGiaTri[k].FTuChi });
                        }
                        else
                        {
                            if (total.LstGiaTriTotal.Count() <= itemResult.LstGiaTri.Count())
                            {
                                total.LstGiaTriTotal[k].FTuChi += itemResult.LstGiaTri[k].FTuChi;
                            }
                        }
                    }
                    countResult++;
                }
                resultsTotal.Add(total);
                var sumTotal = listDataDonVi.Sum(x => x.TuChi) / donViTinh;
                var numColumnMerge = listMucLucSplits[i].Count() < 3 ? 4 : listMucLucSplits[i].Count();
                var nameColunmMerge = GetExcelColumnName(numColumnMerge + 4);

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("FormatNumber", formatNumber);
                data.Add("ListData", results);
                data.Add("ListDataTotal", resultsTotal);
                data.Add("Header1", SelectedDonViTinh.DisplayItem);
                data.Add("TieuDe1", TieuDe1);
                data.Add("TieuDe2", TieuDe2);
                data.Add("TieuDe3", TieuDe3);
                data.Add("Cap1", _danhMucService.FindDonViQuanLy(yearOfWork).ToUpper());
                data.Add("Cap2", GetHeader2Report());
                data.Add("DonViTinh", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                data.Add("h2", string.Empty);
                data.Add("TenNganh", tenNganh);
                data.Add("Headers", headers);
                data.Add("SumTongCong", sumTotal);
                if (i == 0)
                {
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    data.Add("ThoiGian", string.Format("{0}, ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                }
                if (i == 0)
                {
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CTC_DATAKIEMTRA_TRANG1);
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                }
                else
                {
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CTC_DATAKIEMTRA_TRANG);
                    fileNamePrefix = Path.Combine(string.Format("{0}_{1}", Path.GetFileNameWithoutExtension(templateFileName), i + 1));
                }
                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportNganhThamDinhDataKiemTra, HeaderReportNganhThamDinhModel>(templateFileName, data);
                exportResults.Add(new ExportResult(string.Format("Kết quả thẩm định số kiểm tra thông báo - Tờ {0}", i + 1), fileNameWithoutExtension, null, xlsFile));
            }
            return exportResults;
        }

        private void PrintReportDuToanChiTietNganh(bool isPrintPDF = true, bool isShowOnBrowser = false)
        {
            if (ListNNganh == null || ListNNganh.Where(n => n.IsChecked).ToList().Count == 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckNganh);
                return;
            }

            if (string.IsNullOrEmpty(IdChungTuCTC))
            {
                MessageBoxHelper.Warning(Resources.MsgCheckCtcVoucher);
                return;
            }

            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    int donViTinh = GetDonViTinh();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
                    NsSktNganhThamDinh item = _thamDinhService.Find(Guid.Parse(IdChungTuCTC));
                    List<ExpertiseModelDetailModel> listDetail = GetDataDetail(item);
                    if (listDetail != null && listDetail.Count > 0)
                    {
                        if (SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
                        {
                            results.AddRange(GetResultExportReportData(isPrintPDF ? ExportType.PDF : ExportType.EXCEL, ListNNganh));
                        }
                        else
                        {
                            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_NGANHTHAMDINH_CTC) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CHUNGTUCTC_NSBD_DONVI);
                            int countPage = 1;
                            foreach (CheckBoxItem nganhItem in ListNNganh.Where(n => n.IsChecked).ToList())
                            {
                                List<ExpertiseModelDetailModel> listDetailPrint = listDetail.Where(n => !string.IsNullOrEmpty(n.Nganh) && nganhItem.ValueItem.Contains(n.Nganh)).ToList();
                                List<NsSktMucLuc> listMLSKT = GetSKTMucLucIdByNganh(nganhItem.ValueItem);
                                List<string> listKyHieu = StringUtils.GetListKyHieuParent(listMLSKT.Select(n => n.SKyHieu).ToList());
                                listDetailPrint = listDetailPrint.Where(n => listKyHieu.Contains(n.KyHieu)).ToList();
                                List<ExpertisePrintCTCReportModel> dataPrint = FormatDataExportDonVi(listDetailPrint).OrderBy(x => x.KyHieu).ToList();
                                dataPrint.Select(n =>
                                {
                                    n.TuChi1 = n.TuChi1 / donViTinh; n.TuChi2 = n.TuChi2 / donViTinh; n.TuChi3 = n.TuChi3 / donViTinh;
                                    n.SuDungTonKho1 = n.SuDungTonKho1 / donViTinh; n.ChiDacThuNganhPhanCap1 = n.ChiDacThuNganhPhanCap1 / donViTinh;
                                    return n;
                                }).ToList();
                                var prefixTenNganh = "Ngành";
                                if (IsChuyenNganh)
                                {
                                    prefixTenNganh = "Chuyên ngành";
                                }
                                if (dataPrint.Count() > 0)
                                {
                                    Dictionary<string, object> data = new Dictionary<string, object>();
                                    //double tongTuChi = listDonViData.Where(n => !n.IsHangCha).Sum(n => n.TuChi);
                                    string tenDonVi1 = "Mua hàng cấp hiện vật";
                                    string tenDonVi2 = "Sử dụng tồn kho";
                                    string tenDonVi3 = "Chi đặc thù ngành phân cấp";
                                    double tongDonVi1 = 0;
                                    double tongDonVi2 = 0;
                                    double tongDonVi3 = 0;

                                    tongDonVi1 = dataPrint.Where(n => !n.IsHangCha).Sum(n => n.TuChi1);
                                    tongDonVi2 = dataPrint.Where(n => !n.IsHangCha).Sum(n => n.SuDungTonKho1);
                                    tongDonVi3 = dataPrint.Where(n => !n.IsHangCha).Sum(n => n.ChiDacThuNganhPhanCap1);
                                    data.Add("DonVi1", tenDonVi1);
                                    data.Add("DonVi2", tenDonVi2);
                                    data.Add("DonVi3", tenDonVi3);
                                    data.Add("TongDonVi1", tongDonVi1);
                                    data.Add("TongDonVi2", tongDonVi2);
                                    data.Add("TongDonVi3", tongDonVi3);
                                    data.Add("ListData", dataPrint);
                                    data.Add("TieuDe1", TieuDe1);
                                    data.Add("TieuDe2", TieuDe2);
                                    data.Add("TieuDe3", TieuDe3);
                                    data.Add("FormatNumber", formatNumber);
                                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                                    data.Add("Cap2", GetHeader2Report());
                                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                                    data.Add("ThoiGian", string.Format("{0}, ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                                    fileNamePrefix = string.Format("{0}_{1}_{2}_{3}", item.SSoChungTu, countPage.ToString(), nganhItem.NameItem, DateTime.Now.ToString("yyyyMMdd"));
                                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                    var xlsFile = _exportService.Export<ExpertisePrintCTCReportModel>(templateFileName, data);

                                    int widthMoTa = 12000;
                                    //xlsFile.SetColWidth(3, widthMoTa);
                                    results.Add(new ExportResult(prefixTenNganh + " " + nganhItem.DisplayItem, fileNameWithoutExtension, null, xlsFile));
                                    countPage++;
                                }
                            }
                        }

                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void PrintReportDuToanChiTietChuyenNganh(bool isPrintPDF = true, bool isShowOnBrowser = false)
        {
            if (ListNNganh == null || ListNNganh.Where(n => n.IsChecked).ToList().Count == 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckNganh);
                return;
            }

            if (string.IsNullOrEmpty(IdChungTuCTC))
            {
                MessageBoxHelper.Warning(Resources.MsgCheckCtcVoucher);
                return;
            }

            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    int donViTinh = GetDonViTinh();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
                    NsSktNganhThamDinh item = _thamDinhService.Find(Guid.Parse(IdChungTuCTC));
                    List<ExpertiseModelDetailModel> listDetail = GetDataDetail(item);
                    foreach (var nganhItem in ListNNganh.Where(n => n.IsChecked))
                    {
                        if (listDetail != null && listDetail.Count > 0)
                        {
                            if (SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
                            {
                                results.AddRange(GetResultExportReportData(isPrintPDF ? ExportType.PDF : ExportType.EXCEL, new ObservableCollection<CheckBoxItem>() { nganhItem }));
                            }
                            else
                            {
                                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_NGANHTHAMDINH_CTC) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CHUNGTUCTC_NSBD_DONVI);
                                int countPage = 1;
                                List<ExpertiseModelDetailModel> listDetailPrint = listDetail.Where(n => !string.IsNullOrEmpty(n.Nganh) && nganhItem.ValueItem.Contains(n.Nganh)).ToList();
                                List<NsSktMucLuc> listMLSKT = GetSKTMucLucIdByNganh(nganhItem.ValueItem);
                                List<string> listKyHieu = StringUtils.GetListKyHieuParent(listMLSKT.Select(n => n.SKyHieu).ToList());
                                listDetailPrint = listDetailPrint.Where(n => listKyHieu.Contains(n.KyHieu)).ToList();
                                List<ExpertisePrintCTCReportModel> dataPrint = FormatDataExportDonVi(listDetailPrint).OrderBy(x => x.KyHieu).ToList();
                                dataPrint.Select(n =>
                                {
                                    n.TuChi1 = n.TuChi1 / donViTinh; n.TuChi2 = n.TuChi2 / donViTinh; n.TuChi3 = n.TuChi3 / donViTinh;
                                    n.SuDungTonKho1 = n.SuDungTonKho1 / donViTinh; n.ChiDacThuNganhPhanCap1 = n.ChiDacThuNganhPhanCap1 / donViTinh;
                                    return n;
                                }).ToList();
                                var prefixTenNganh = "Ngành";
                                if (IsChuyenNganh)
                                {
                                    prefixTenNganh = "Chuyên ngành";
                                }
                                if (dataPrint.Count() > 0)
                                {
                                    Dictionary<string, object> data = new Dictionary<string, object>();
                                    //double tongTuChi = listDonViData.Where(n => !n.IsHangCha).Sum(n => n.TuChi);
                                    string tenDonVi1 = "Mua hàng cấp hiện vật";
                                    string tenDonVi2 = "Sử dụng tồn kho";
                                    string tenDonVi3 = "Chi đặc thù ngành phân cấp";
                                    double tongDonVi1 = 0;
                                    double tongDonVi2 = 0;
                                    double tongDonVi3 = 0;

                                    tongDonVi1 = dataPrint.Where(n => !n.IsHangCha).Sum(n => n.TuChi1);
                                    tongDonVi2 = dataPrint.Where(n => !n.IsHangCha).Sum(n => n.SuDungTonKho1);
                                    tongDonVi3 = dataPrint.Where(n => !n.IsHangCha).Sum(n => n.ChiDacThuNganhPhanCap1);
                                    data.Add("DonVi1", tenDonVi1);
                                    data.Add("DonVi2", tenDonVi2);
                                    data.Add("DonVi3", tenDonVi3);
                                    data.Add("TongDonVi1", tongDonVi1);
                                    data.Add("TongDonVi2", tongDonVi2);
                                    data.Add("TongDonVi3", tongDonVi3);
                                    data.Add("ListData", dataPrint);
                                    data.Add("TieuDe1", TieuDe1);
                                    data.Add("TieuDe2", TieuDe2);
                                    data.Add("TieuDe3", TieuDe3);
                                    data.Add("FormatNumber", formatNumber);
                                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                                    data.Add("Cap2", GetHeader2Report());
                                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                                    data.Add("ThoiGian", string.Format("{0}, ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                                    fileNamePrefix = string.Format("{0}_{1}_{2}_{3}", item.SSoChungTu, countPage.ToString(), nganhItem.NameItem, DateTime.Now.ToString("yyyyMMdd"));
                                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                    var xlsFile = _exportService.Export<ExpertisePrintCTCReportModel>(templateFileName, data);

                                    int widthMoTa = 12000;
                                    //xlsFile.SetColWidth(3, widthMoTa);
                                    results.Add(new ExportResult(prefixTenNganh + " " + nganhItem.DisplayItem, fileNameWithoutExtension, null, xlsFile));
                                    countPage++;
                                }
                            }

                        }
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private List<ExpertiseModelDetailModel> GetDataDetail(NsSktNganhThamDinh chungTu)
        {
            List<ExpertiseModelDetailModel> resultDetail = new List<ExpertiseModelDetailModel>();
            List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => !string.IsNullOrEmpty(n.SGiaTri)).ToList();
            if (chungTu == null || chungTu.Id == Guid.Empty || listDanhMuc == null || listDanhMuc.Count == 0)
                return resultDetail;
            List<ThDChungTuChiTietQuery> data = new List<ThDChungTuChiTietQuery>();
            if (SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
            {
                data = _thamDinhChiTietService.FindByCondition(_sessionService.Current.YearOfWork, chungTu.Id.ToString(),
                _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();
            }
            else if (SelectedLoaiNganSach.ValueItem == VoucherType.NSBD_Key)
            {
                data = _thamDinhChiTietService.FindByConditionNSBD(_sessionService.Current.YearOfWork, chungTu.Id.ToString(),
                _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();
            }
            List<string> listKyHieu = new List<string>();
            foreach (ThDChungTuChiTietQuery item in data.Where(n => !n.IsHangCha))
            {
                listKyHieu.AddRange(StringUtils.SplitKyHieuParent(item.KyHieu));
            }
            data = data.Where(n => listKyHieu.Contains(n.KyHieu)).ToList();
            resultDetail = _mapper.Map<List<Model.ExpertiseModelDetailModel>>(data);
            return resultDetail;
        }

        private void CalculateData(ref List<ExpertiseModelDetailModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x => { x.TuChi = 0; x.TuChiCTC = 0; x.TuChiNganh = 0; x.HuyDongCTC = 0; x.HuyDongNganh = 0; x.Tang = 0; x.Giam = 0; return x; }).ToList();
            foreach (var item in listData.Where(x => !x.IsHangCha && !x.IsDeleted && (x.TuChi != 0 || x.TuChiCTC != 0 || x.TuChiNganh != 0 || x.HuyDongCTC != 0 || x.HuyDongNganh != 0)))
            {
                CalculateParent(ref listData, item, item);
            }
        }

        private void CalculateParent(ref List<ExpertiseModelDetailModel> listData, ExpertiseModelDetailModel currentItem, ExpertiseModelDetailModel selfItem)
        {
            var parentItem = listData.Where(x => x.IdMucLuc == currentItem.IdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.TuChiCTC += selfItem.TuChiCTC;
            parentItem.TuChiNganh += selfItem.TuChiNganh;
            parentItem.HuyDongCTC += selfItem.HuyDongCTC;
            parentItem.HuyDongNganh += selfItem.HuyDongNganh;
            parentItem.Tang += selfItem.Tang;
            parentItem.Giam += selfItem.Giam;
            CalculateParent(ref listData, parentItem, selfItem);
        }

        private void LoadLoaiNganSach()
        {
            DataLoaiNganSach = new ObservableCollection<ComboboxItem>();
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSSD_Key, DisplayItem = VoucherType.NSSD_Value });
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSBD_Key, DisplayItem = VoucherType.NSBD_Value });
            SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault();
        }

        public override void Init()
        {
            try
            {
                LoadLoaiNganSach();
                LoadDonViTinh();
                //LoadNganh();
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_NGANHTHAMDINH_CTC) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                TieuDe1 = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
                TieuDe2 = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
                TieuDe3 = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
                var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public bool CheckDonViThamDinhCondition()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO));
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            List<DonVi> listDonVi = _nSDonViService.FindByCondition(predicate).ToList();
            if (listDonVi != null && listDonVi.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LoadDonViTinh()
        {
            DataDonViTinh = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (var dvt in listDonViTinh)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();

            OnPropertyChanged(nameof(SelectedCountNganh));
        }

        public int GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return int.Parse(SelectedDonViTinh.ValueItem);
        }

        private void PrintExcel()
        {
            try
            {
                if (IsChuyenNganh)
                {
                    PrintReportDuToanChiTietChuyenNganh(false, false);
                }
                else
                {
                    PrintReportDuToanChiTietNganh(false, false);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void PrintPDF()
        {
            try
            {
                if (IsChuyenNganh)
                {
                    PrintReportDuToanChiTietChuyenNganh();
                }
                else
                {
                    PrintReportDuToanChiTietNganh();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnPrintBrowser()
        {
            try
            {
                if (IsChuyenNganh)
                {
                    PrintReportDuToanChiTietChuyenNganh(true, true);
                }
                else
                {
                    PrintReportDuToanChiTietChuyenNganh(true, true);

                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_NGANHTHAMDINH_CTC) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_NGANHTHAMDINH_CTC;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TieuDe1 = chuKy.TieuDe1MoTa;
                TieuDe2 = chuKy.TieuDe2MoTa;
                TieuDe3 = chuKy.TieuDe3MoTa;

            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}

