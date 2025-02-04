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
    public class ExpertisePrintReportDataBySKTViewModel : ViewModelBase
    {
        private INsDonViService _donViService;
        private IMapper _mapper;
        private ICollectionView _listNganhView;
        private INsDonViService _nSDonViService;
        private ISessionService _sessionService;
        private IExportService _exportService;
        private ISktSoLieuService _sktSoLieuService;
        private IDanhMucService _danhMucService;
        private ISktMucLucService _iSktMucLucService;
        private ISktNganhThamDinhService _thamDinhService;
        private ISktNganhThamDinhChiTietService _thamDinhChiTietService;
        private IDmChuKyService _dmChuKyService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private readonly ILog _logger;
        private DmChuKy _dmChuKy;
        private string _diaDiem;

        public override string Name => "Kết quả thẩm định số kiểm tra thông báo trực tiếp cho các đơn vị cấp 2, 3 trực thuộc bộ";
        public override string Title => "Kết quả thẩm định số kiểm tra thông báo trực tiếp cho các đơn vị cấp 2, 3 trực thuộc bộ";
        public override string Description => "Kết quả thẩm định số kiểm tra thông báo trực tiếp cho các đơn vị cấp 2, 3 trực thuộc bộ";

        public override Type ContentType => typeof(View.Budget.DemandCheck.Expertise.ExpertisePrintReportCTC);
        public ObservableCollection<CheckBoxItem> ListNNganh { get; set; }
        public int Loai { get; set; }
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
                int totalCount = ListNNganh != null ? ListNNganh.Count : 0;
                int totalSelected = ListNNganh != null ? ListNNganh.Count(item => item.IsChecked) : 0;
                return string.Format("NGÀNH ({0}/{1})", totalSelected, totalCount);
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

        public ExpertisePrintReportDataBySKTViewModel(
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
            _logger = logger;
            _iSktMucLucService = iSktMucLucService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintPDFCommand = new RelayCommand(o => PrintPDF());
            PrintExcelCommand = new RelayCommand(o => PrintExcel());
            PrintBrowserCommand = new RelayCommand(o => OnPrintBrowser());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public void LoadNganh()
        {
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

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        public List<ThDChungTuChiTietQuery> GetValueTuChiPrev()
        {
            if (SelectedLoaiNganSach == null)
            {
                return new List<ThDChungTuChiTietQuery>();
            }
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTCTCDN);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(SelectedLoaiNganSach.ValueItem));
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

        public string GetIdChungTu(string idDonVi)
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD);
            predicate = predicate.And(x => x.ILoaiChungTu == LoaiNganSach);
            predicate = predicate.And(x => x.IIdMaDonVi == idDonVi);
            NsSktNganhThamDinh chungTuDeNghi = _thamDinhService.FindByCondition(predicate).FirstOrDefault();
            return chungTuDeNghi != null ? chungTuDeNghi.Id.ToString() : string.Empty;
        }

        public List<NsSktNganhThamDinh> GetListChungTu()
        {
            if (SelectedLoaiNganSach == null)
            {
                return new List<NsSktNganhThamDinh>();
            }
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(SelectedLoaiNganSach.ValueItem));
            List<NsSktNganhThamDinh> chungTuDeNghi = _thamDinhService.FindByCondition(predicate).ToList();
            return (chungTuDeNghi != null && chungTuDeNghi.Count() > 0) ? chungTuDeNghi : new List<NsSktNganhThamDinh>();
        }

        public List<string> GetNganhByDetail()
        {
            List<NsSktNganhThamDinh> listChungTu = GetListChungTu();
            if (listChungTu.Count() == 0)
            {
                return new List<string>();
            }

            var lstChungTuId = listChungTu.Select(n => n.Id).ToList();
            var predicate = PredicateBuilder.True<NsSktNganhThamDinhChiTiet>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => lstChungTuId.Contains(x.IIdCtnganhThamDinh));
            List<NsSktNganhThamDinhChiTiet> listDetail = _thamDinhChiTietService.FindByCondition(predicate).ToList();
            var lstMucLucId = listDetail.Select(n => n.IIdMucLuc).ToList();
            if (listDetail != null && listDetail.Count() > 0)
            {
                var predicateMucLuc = PredicateBuilder.True<NsSktMucLuc>();
                predicateMucLuc = predicateMucLuc.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                predicateMucLuc = predicateMucLuc.And(x => lstMucLucId.Contains(x.IIDMLSKT));
                List<NsSktMucLuc> listMucLuc = _iSktMucLucService.FindByCondition(predicateMucLuc).ToList();
                return (listMucLuc != null && listMucLuc.Count() > 0) ? listMucLuc.Select(n => n.SNg).ToList() : new List<string>();
            }
            else
            {
                return new List<string>();
            }
        }

        private List<Guid> GetSKTMucLucIdByNganh(List<string> nganhNganh)
        {
            if (nganhNganh == null || nganhNganh.Count() == 0)
            {
                return new List<Guid>();
            }
            List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_NGANH_NGANH", _sessionService.Current.YearOfWork).Where(n => nganhNganh.Contains(n.IIDMaDanhMuc)).ToList();
            List<string> listNganh = new List<string>();
            foreach (var item in listDanhMuc)
            {
                if (!string.IsNullOrEmpty(item.SGiaTri))
                {
                    listNganh.AddRange(item.SGiaTri.Split(","));
                }
            }
            var predicateMucLuc = PredicateBuilder.True<NsSktMucLuc>();
            predicateMucLuc = predicateMucLuc.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicateMucLuc = predicateMucLuc.And(x => listNganh.Contains(x.SNg));
            List<NsSktMucLuc> listMucLuc = _iSktMucLucService.FindByCondition(predicateMucLuc).ToList();
            return listMucLuc.Select(n => n.IIDMLSKT).ToList();
        }

        private List<NsSktMucLuc> GetLstTittle()
        {
            List<NsSktMucLuc> result = new List<NsSktMucLuc>();
            result.Add(new NsSktMucLuc { SMoTa = "Số CTC đề nghị" });
            result.Add(new NsSktMucLuc { SMoTa = "Số ngành thẩm định" });
            result.Add(new NsSktMucLuc { SMoTa = "Tăng (+)/Giảm (-)" });
            return result;
        }

        private List<NsSktMucLuc> GetLstTongTittle()
        {
            List<NsSktMucLuc> result = new List<NsSktMucLuc>();
            result.Add(new NsSktMucLuc { SMoTa = "Số CTC đề nghị" });
            result.Add(new NsSktMucLuc { SMoTa = "Số ngành thẩm định" });
            return result;
        }

        private void PrintReportDuToanChiTietDonVi(bool isPrintPDF = true, bool isShowOnBrowser = false)
        {
            if (ListNNganh == null || ListNNganh.Count == 0)
            {
                return;
            }

            try
            {
                if (ListNNganh == null || ListNNganh.Where(n => n.IsChecked).Count() == 0)
                {
                    return;
                }
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_NGANHTHAMDINH_SKT) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_DANHSACH_CHITRA_LUONGCN);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    int donViTinh = GetDonViTinh();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
                    var yearOfWork = _sessionService.Current.YearOfWork;

                    var tenNganh = "";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var lstIdCodeSelected = "";
                    if (ListNNganh != null)
                    {
                        lstIdCodeSelected = string.Join(",", ListNNganh.Where(item => item.IsChecked).Select(item => item.ValueItem).ToList());
                    }
                    List<NsSktNganhThamDinh> listChungTu = GetListChungTu();

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
                    var listDonVi = _nSDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList();
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
                            NsSktMucLuc mlTong = new NsSktMucLuc();
                            mlTong.SNg = mucluc.SNg;
                            mlTong.SMoTa = "Tổng";
                            lstMlResult.Add(mlTong);
                        }
                        lstMlResult.Add(mucluc);
                    }

                    string path = "";
                    var listMucLucSplits = SplitList(lstMlResult, 2).ToList();
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
                            var columnEndName = GetExcelColumnName(lstMlHeader.Count() + 2 + columnStart);
                            if (lstMlHeader.Count() == 2)
                            {
                                if (lstMlHeader.Contains("Tổng"))
                                {
                                    columnEndName = GetExcelColumnName(lstMlHeader.Count() + 2 + columnStart);
                                }
                                else
                                {
                                    columnEndName = GetExcelColumnName(lstMlHeader.Count() + 3 + columnStart);
                                }
                            }
                            else
                            {
                                if (lstMlHeader.Contains("Tổng"))
                                {
                                    columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart);
                                }
                                else
                                {
                                    columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart + 1);
                                }
                            }
                            mergeRange = columnStartName + "7" + ":" + columnEndName + "7";
                            //columnStart += lstMlHeader.Count();
                            HeaderReportNganhThamDinhModel hd = new HeaderReportNganhThamDinhModel();
                            hd.TenNganh = nganh != null ? nganh.NameItem : "";
                            hd.LstMucLuc = new List<NsSktMucLuc>();
                            hd.LstNganhHeader = new List<NsSktMucLuc>();
                            hd.LstTitle = new List<NsSktMucLuc>();
                            hd.MergeRange = mergeRange;
                            int j = 0;
                            int checkTong = 0;
                            int checkKhacTong = 0;
                            foreach (var mlHeader in lstMlHeader)
                            {
                                List<NsSktMucLuc> listTileChild = new List<NsSktMucLuc>();

                                if (mlHeader == "Tổng")
                                {
                                    var columnChildStartName = GetExcelColumnName(columnChildStart);
                                    var columnChildEndName = GetExcelColumnName(lstMlHeader.Count() + columnChildStart - 1);
                                    if (lstMlHeader.Count() == 1 && lstMlHeader.Contains("Tổng"))
                                    {
                                        columnChildEndName = GetExcelColumnName(lstMlHeader.Count() + columnChildStart);
                                    }
                                    string mergeChildRange = columnChildStartName + "8" + ":" + columnChildEndName + "8";
                                    hd.MergeRangeChild = mergeChildRange;
                                    listTileChild = GetLstTongTittle();
                                    //columnChildStart += 2;
                                    checkTong = 1;
                                }
                                else
                                {
                                    string columnChildStartName = "";
                                    string columnChildEndName = "";

                                    columnChildStartName = GetExcelColumnName(columnChildStart);
                                    columnChildEndName = GetExcelColumnName(columnChildStart + 3 - 1);

                                    if (checkTong == 1)
                                    {
                                        columnChildStartName = GetExcelColumnName(columnChildStart);
                                        columnChildEndName = GetExcelColumnName(columnChildStart + 3 - 1);
                                    }

                                    if (checkKhacTong == 1)
                                    {
                                        columnChildStartName = GetExcelColumnName(columnChildStart);
                                        columnChildEndName = GetExcelColumnName(columnChildStart + 3 - 1);
                                    }

                                    string mergeChildRange = columnChildStartName + "8" + ":" + columnChildEndName + "8";
                                    hd.MergeRangeChild2 = mergeChildRange;
                                    listTileChild = GetLstTittle();
                                    //columnChildStart += 3;
                                    checkKhacTong = 1;
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
                                    if (k == 0)
                                    {
                                        if (mlHeader.Equals("Tổng"))
                                        {
                                            mlhd.SSttBC = "1";
                                        }
                                        else
                                        {
                                            mlhd.SSttBC = "2";
                                        }
                                    }
                                    else
                                    {
                                        mlhd.SSttBC = "3";
                                    }
                                    k++;
                                    j++;
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
                            kq.TongCong = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChi);
                            kq.TongCongCTC = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChiPrev);
                            kq.TongCongTD = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChi);
                            kq.LstGiaTri = new List<NsSktNganhThamDinhChiTiet>();
                            foreach (var ml in listMucLucSplits[i])
                            {
                                NsSktNganhThamDinhChiTiet tongCTC = new NsSktNganhThamDinhChiTiet();
                                NsSktNganhThamDinhChiTiet tongNTD = new NsSktNganhThamDinhChiTiet();
                                NsSktNganhThamDinhChiTiet giaTri = new NsSktNganhThamDinhChiTiet();
                                NsSktNganhThamDinhChiTiet giaTriDeNghi = new NsSktNganhThamDinhChiTiet();
                                NsSktNganhThamDinhChiTiet tangGiam = new NsSktNganhThamDinhChiTiet();
                                if (ml.IIDMLSKT != Guid.Empty)
                                {
                                    giaTriDeNghi = new NsSktNganhThamDinhChiTiet();
                                    var gtDonViDeNghi = listDataDonVi.Where(x => x.Nganh.Equals(ml.SNg) && x.IdDonVi == dv.IIDMaDonVi && x.IdMucLuc == ml.IIDMLSKT).Sum(x => x.TuChiPrev);
                                    giaTriDeNghi.FTuChi = gtDonViDeNghi / donViTinh;
                                    kq.LstGiaTri.Add(giaTriDeNghi);

                                    giaTri = new NsSktNganhThamDinhChiTiet();
                                    var gtDonVi = listDataDonVi.Where(x => x.Nganh.Equals(ml.SNg) && x.IdDonVi == dv.IIDMaDonVi && x.IdMucLuc == ml.IIDMLSKT).Sum(x => x.TuChi);
                                    giaTri.FTuChi = gtDonVi / donViTinh;
                                    kq.LstGiaTri.Add(giaTri);

                                    tangGiam.FTuChi = (gtDonVi - gtDonViDeNghi) / donViTinh;
                                    kq.LstGiaTri.Add(tangGiam);
                                }
                                else
                                {
                                    tongCTC = new NsSktNganhThamDinhChiTiet();
                                    var gtDonVi = listDataDonVi.Where(x => x.Nganh.Equals(ml.SNg) && x.IdDonVi == dv.IIDMaDonVi).Sum(x => x.TuChiPrev);
                                    tongCTC.FTuChi = gtDonVi / donViTinh;

                                    tongNTD = new NsSktNganhThamDinhChiTiet();
                                    var gtDonViDeNghi = listDataDonVi.Where(x => x.Nganh.Equals(ml.SNg) && x.IdDonVi == dv.IIDMaDonVi).Sum(x => x.TuChi);
                                    tongNTD.FTuChi = gtDonViDeNghi / donViTinh;

                                    kq.LstGiaTri.Add(tongCTC);
                                    kq.LstGiaTri.Add(tongNTD);
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
                        var sumTotalCTC = listDataDonVi.Sum(x => x.TuChiPrev);
                        var sumTotalTD = listDataDonVi.Sum(x => x.TuChi);
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
                        data.Add("SumTongCongCTC", sumTotalCTC);
                        data.Add("SumTongCongTD", sumTotalTD);
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
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINHDATAKIEMTRA_TRANG1);
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINHDATAKIEMTRA_TRANG);
                            fileNamePrefix = Path.Combine(string.Format("{0}_{1}", Path.GetFileNameWithoutExtension(templateFileName), i));
                            if (listMucLucSplits[i].Any(x => !string.IsNullOrEmpty(x.SMoTa) && x.SMoTa.Equals("Tổng")))
                            {
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINHDATAKIEMTRA_TRANG2);
                            }
                        }
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ReportNganhThamDinhDataKiemTra, HeaderReportNganhThamDinhModel>(templateFileName, data);
                        exportResults.Add(new ExportResult(string.Format("Kết quả thẩm định số kiểm tra thông báo - Tờ {0}", (i + 1)), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = exportResults;
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

        private void PrintReportDuToanChiTietDonViChuyenNganh(bool isPrintPDF = true, bool isShowOnBrowser = false)
        {
            if (ListNNganh == null || ListNNganh.Count == 0)
            {
                return;
            }

            try
            {
                if (ListNNganh == null || ListNNganh.Where(n => n.IsChecked).Count() == 0)
                {
                    return;
                }
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_NGANHTHAMDINH_SKT) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_DANHSACH_CHITRA_LUONGCN);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    int donViTinh = GetDonViTinh();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
                    var yearOfWork = _sessionService.Current.YearOfWork;

                    var tenNganh = "";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var lstIdCodeSelected = "";
                    if (ListNNganh != null)
                    {
                        foreach (var perListNNganh in ListNNganh.Where(item => item.IsChecked))
                        {
                            lstIdCodeSelected = string.Join(",", perListNNganh.ValueItem);
                            List<NsSktNganhThamDinh> listChungTu = GetListChungTu();

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
                            var listDonVi = _nSDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList();
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
                                    NsSktMucLuc mlTong = new NsSktMucLuc();
                                    mlTong.SNg = mucluc.SNg;
                                    mlTong.SMoTa = "Tổng";
                                    lstMlResult.Add(mlTong);
                                }
                                lstMlResult.Add(mucluc);
                            }

                            string path = "";
                            var listMucLucSplits = SplitList(lstMlResult, 2).ToList();
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
                                    var columnEndName = GetExcelColumnName(lstMlHeader.Count() + 2 + columnStart);
                                    if (lstMlHeader.Count() == 2)
                                    {
                                        if (lstMlHeader.Contains("Tổng"))
                                        {
                                            columnEndName = GetExcelColumnName(lstMlHeader.Count() + 2 + columnStart);
                                        }
                                        else
                                        {
                                            columnEndName = GetExcelColumnName(lstMlHeader.Count() + 3 + columnStart);
                                        }
                                    }
                                    else
                                    {
                                        if (lstMlHeader.Contains("Tổng"))
                                        {
                                            columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart);
                                        }
                                        else
                                        {
                                            columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart + 1);
                                        }
                                    }
                                    mergeRange = columnStartName + "7" + ":" + columnEndName + "7";
                                    //columnStart += lstMlHeader.Count();
                                    HeaderReportNganhThamDinhModel hd = new HeaderReportNganhThamDinhModel();
                                    hd.TenNganh = nganh != null ? nganh.NameItem : "";
                                    hd.LstMucLuc = new List<NsSktMucLuc>();
                                    hd.LstNganhHeader = new List<NsSktMucLuc>();
                                    hd.LstTitle = new List<NsSktMucLuc>();
                                    hd.MergeRange = mergeRange;
                                    int j = 0;
                                    int checkTong = 0;
                                    int checkKhacTong = 0;
                                    foreach (var mlHeader in lstMlHeader)
                                    {
                                        List<NsSktMucLuc> listTileChild = new List<NsSktMucLuc>();

                                        if (mlHeader == "Tổng")
                                        {
                                            var columnChildStartName = GetExcelColumnName(columnChildStart);
                                            var columnChildEndName = GetExcelColumnName(lstMlHeader.Count() + columnChildStart - 1);
                                            if (lstMlHeader.Count() == 1 && lstMlHeader.Contains("Tổng"))
                                            {
                                                columnChildEndName = GetExcelColumnName(lstMlHeader.Count() + columnChildStart);
                                            }
                                            string mergeChildRange = columnChildStartName + "8" + ":" + columnChildEndName + "8";
                                            hd.MergeRangeChild = mergeChildRange;
                                            listTileChild = GetLstTongTittle();
                                            //columnChildStart += 2;
                                            checkTong = 1;
                                        }
                                        else
                                        {
                                            string columnChildStartName = "";
                                            string columnChildEndName = "";

                                            columnChildStartName = GetExcelColumnName(columnChildStart);
                                            columnChildEndName = GetExcelColumnName(columnChildStart + 3 - 1);

                                            if (checkTong == 1)
                                            {
                                                columnChildStartName = GetExcelColumnName(columnChildStart);
                                                columnChildEndName = GetExcelColumnName(columnChildStart + 3 - 1);
                                            }

                                            if (checkKhacTong == 1)
                                            {
                                                columnChildStartName = GetExcelColumnName(columnChildStart);
                                                columnChildEndName = GetExcelColumnName(columnChildStart + 3 - 1);
                                            }

                                            string mergeChildRange = columnChildStartName + "8" + ":" + columnChildEndName + "8";
                                            hd.MergeRangeChild2 = mergeChildRange;
                                            listTileChild = GetLstTittle();
                                            //columnChildStart += 3;
                                            checkKhacTong = 1;
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
                                            if (k == 0)
                                            {
                                                if (mlHeader.Equals("Tổng"))
                                                {
                                                    mlhd.SSttBC = "1";
                                                }
                                                else
                                                {
                                                    mlhd.SSttBC = "2";
                                                }
                                            }
                                            else
                                            {
                                                mlhd.SSttBC = "3";
                                            }
                                            k++;
                                            j++;
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
                                    }
                                    headers.Add(hd);
                                }

                                int stt = 1;
                                List<ReportNganhThamDinhDataKiemTra> results = new List<ReportNganhThamDinhDataKiemTra>();
                                foreach (var dv in listDonVi.OrderBy(x => x.IIDMaDonVi))
                                {
                                    ReportNganhThamDinhDataKiemTra kq = new ReportNganhThamDinhDataKiemTra();
                                    kq.Stt = stt++;
                                    kq.MaDonVi = dv.IIDMaDonVi;
                                    kq.TenDonVi = dv.TenDonVi;
                                    kq.TongCong = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChi);
                                    kq.TongCongCTC = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChiPrev);
                                    kq.TongCongTD = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChi);
                                    kq.LstGiaTri = new List<NsSktNganhThamDinhChiTiet>();
                                    foreach (var ml in listMucLucSplits[i])
                                    {
                                        NsSktNganhThamDinhChiTiet tongCTC = new NsSktNganhThamDinhChiTiet();
                                        NsSktNganhThamDinhChiTiet tongNTD = new NsSktNganhThamDinhChiTiet();
                                        NsSktNganhThamDinhChiTiet giaTri = new NsSktNganhThamDinhChiTiet();
                                        NsSktNganhThamDinhChiTiet giaTriDeNghi = new NsSktNganhThamDinhChiTiet();
                                        NsSktNganhThamDinhChiTiet tangGiam = new NsSktNganhThamDinhChiTiet();
                                        if (ml.IIDMLSKT != Guid.Empty)
                                        {
                                            giaTriDeNghi = new NsSktNganhThamDinhChiTiet();
                                            var gtDonViDeNghi = listDataDonVi.Where(x => x.Nganh.Equals(ml.SNg) && x.IdDonVi == dv.IIDMaDonVi && x.IdMucLuc == ml.IIDMLSKT).Sum(x => x.TuChiPrev);
                                            giaTriDeNghi.FTuChi = gtDonViDeNghi / donViTinh;
                                            kq.LstGiaTri.Add(giaTriDeNghi);

                                            giaTri = new NsSktNganhThamDinhChiTiet();
                                            var gtDonVi = listDataDonVi.Where(x => x.Nganh.Equals(ml.SNg) && x.IdDonVi == dv.IIDMaDonVi && x.IdMucLuc == ml.IIDMLSKT).Sum(x => x.TuChi);
                                            giaTri.FTuChi = gtDonVi / donViTinh;
                                            kq.LstGiaTri.Add(giaTri);

                                            tangGiam.FTuChi = (gtDonVi - gtDonViDeNghi) / donViTinh;
                                            kq.LstGiaTri.Add(tangGiam);
                                        }
                                        else
                                        {
                                            tongCTC = new NsSktNganhThamDinhChiTiet();
                                            var gtDonVi = listDataDonVi.Where(x => x.Nganh.Equals(ml.SNg) && x.IdDonVi == dv.IIDMaDonVi).Sum(x => x.TuChiPrev);
                                            tongCTC.FTuChi = gtDonVi / donViTinh;

                                            tongNTD = new NsSktNganhThamDinhChiTiet();
                                            var gtDonViDeNghi = listDataDonVi.Where(x => x.Nganh.Equals(ml.SNg) && x.IdDonVi == dv.IIDMaDonVi).Sum(x => x.TuChi);
                                            tongNTD.FTuChi = gtDonViDeNghi / donViTinh;

                                            kq.LstGiaTri.Add(tongCTC);
                                            kq.LstGiaTri.Add(tongNTD);
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
                                var sumTotalCTC = listDataDonVi.Sum(x => x.TuChiPrev);
                                var sumTotalTD = listDataDonVi.Sum(x => x.TuChi);
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
                                data.Add("SumTongCongCTC", sumTotalCTC);
                                data.Add("SumTongCongTD", sumTotalTD);
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
                                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINHDATAKIEMTRA_TRANG1);
                                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                                }
                                else
                                {
                                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINHDATAKIEMTRA_TRANG);
                                    fileNamePrefix = Path.Combine(string.Format("{0}_{1}", Path.GetFileNameWithoutExtension(templateFileName), i));
                                    if (listMucLucSplits[i].Any(x => !string.IsNullOrEmpty(x.SMoTa) && x.SMoTa.Equals("Tổng")))
                                    {
                                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINHDATAKIEMTRA_TRANG2);
                                    }
                                }
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ReportNganhThamDinhDataKiemTra, HeaderReportNganhThamDinhModel>(templateFileName, data);
                                exportResults.Add(new ExportResult(string.Format("Kết quả thẩm định số kiểm tra thông báo - Tờ {0}", (i + 1)), fileNameWithoutExtension, null, xlsFile));
                            }
                        }
                    }
                    e.Result = exportResults;
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

        public override void Init()
        {
            try
            {
                LoadDonViTinh();
                LoadLoaiNganSach();
                LoadNganh();
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_NGANHTHAMDINH_SKT) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
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

        private void LoadLoaiNganSach()
        {
            DataLoaiNganSach = new ObservableCollection<ComboboxItem>();
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSSD_Key, DisplayItem = VoucherType.NSSD_Value });
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSBD_Key, DisplayItem = VoucherType.NSBD_Value });
            SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault();
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

        private void PrintReport(bool isPrintPDF = true, bool isShowOnBrowser = false)
        {
            if (SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
            {
                if (IsChuyenNganh)
                {
                    PrintReportDuToanChiTietDonViChuyenNganh(isPrintPDF, isShowOnBrowser);
                }
                else
                {
                    PrintReportDuToanChiTietDonViChuyenNganh(isPrintPDF, isShowOnBrowser);
                }
            }
            else
            {
                PrintReportNSBD(isPrintPDF, isShowOnBrowser);
            }
        }

        private List<string> GetNganhByNganhNganh(List<string> nganhNganh)
        {
            if (nganhNganh == null || nganhNganh.Count() == 0)
            {
                return new List<string>();
            }
            List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_NGANH_NGANH", _sessionService.Current.YearOfWork).Where(n => nganhNganh.Contains(n.IIDMaDanhMuc)).ToList();
            List<string> listNganh = new List<string>();
            foreach (var item in listDanhMuc)
            {
                if (!string.IsNullOrEmpty(item.SGiaTri))
                {
                    listNganh.AddRange(item.SGiaTri.Split(","));
                }
            }
            return listNganh;
        }

        private List<NsSktMucLuc> GetDataSKTMucLucIdByNganh(string chuyenNganh)
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

        private void CalculateData(ref List<ThDChungTuReportNSBDQuery> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x => { x.TuChi = 0; x.SuDungTonKho = 0; x.ChiDacThuNganhPhanCap = 0; x.TuChiCTC = 0; x.SuDungTonKhoCTC = 0; x.ChiDacThuNganhPhanCapCTC = 0; return x; }).ToList();
            foreach (var item in listData.Where(x => !x.IsHangCha && (x.TuChi != 0 || x.SuDungTonKho != 0 || x.ChiDacThuNganhPhanCap != 0
            || x.TuChiCTC != 0 || x.SuDungTonKhoCTC != 0 || x.ChiDacThuNganhPhanCapCTC != 0)))
            {
                CalculateParent(ref listData, item, item);
            }
        }

        private void CalculateParent(ref List<ThDChungTuReportNSBDQuery> listData, ThDChungTuReportNSBDQuery currentItem, ThDChungTuReportNSBDQuery selfItem)
        {
            var parentItem = listData.Where(x => x.IdMucLuc == currentItem.IdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.SuDungTonKho += selfItem.SuDungTonKho;
            parentItem.ChiDacThuNganhPhanCap += selfItem.ChiDacThuNganhPhanCap;
            parentItem.TuChiCTC += selfItem.TuChiCTC;
            parentItem.SuDungTonKhoCTC += selfItem.SuDungTonKhoCTC;
            parentItem.ChiDacThuNganhPhanCapCTC += selfItem.ChiDacThuNganhPhanCapCTC;
            CalculateParent(ref listData, parentItem, selfItem);
        }

        private void PrintReportNSBD(bool isPrintPDF = true, bool isShowOnBrowser = false)
        {
            if (ListNNganh == null || ListNNganh.Where(n => n.IsChecked).ToList().Count == 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckNganh);
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
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_NGANHTHAMDINH_SKT) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                    List<NsSktNganhThamDinh> listChungTu = GetListChungTu();
                    List<ThDChungTuReportNSBDQuery> listDetail = _thamDinhChiTietService.GetDataReportNSBD(_sessionService.Current.YearOfWork,
                    string.Join(",", listChungTu.Select(n => n.Id).ToList()),
                    _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();


                    if (listDetail.Count > 0)
                    {
                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CHUNGTUNTD_NSBD);

                        foreach (CheckBoxItem nganhItem in ListNNganh.Where(n => n.IsChecked).ToList())
                        {
                            List<ThDChungTuReportNSBDQuery> listDetailPrint = listDetail.Where(n => string.IsNullOrEmpty(n.Nganh) || nganhItem.ValueItem.Contains(n.Nganh)).ToList();
                            List<NsSktMucLuc> listMLSKT = GetDataSKTMucLucIdByNganh(nganhItem.ValueItem);
                            List<string> listKyHieu = StringUtils.GetListKyHieuParent(listMLSKT.Select(n => n.SKyHieu).ToList());
                            listDetailPrint = listDetailPrint.Where(n => listKyHieu.Contains(n.KyHieu)).ToList();
                            CalculateData(ref listDetailPrint);
                            listDetailPrint = listDetailPrint.Where(n => n.TuChi != 0 || n.ChiDacThuNganhPhanCap != 0 || n.SuDungTonKho != 0
                                                        || n.TuChiCTC != 0 || n.ChiDacThuNganhPhanCapCTC != 0 || n.SuDungTonKhoCTC != 0).ToList();
                            listDetailPrint.Select(n =>
                            {
                                n.TuChi = n.TuChi / donViTinh; n.SuDungTonKho = n.SuDungTonKho / donViTinh; n.ChiDacThuNganhPhanCap = n.ChiDacThuNganhPhanCap / donViTinh;
                                n.TuChiCTC = n.TuChiCTC / donViTinh; n.SuDungTonKhoCTC = n.SuDungTonKhoCTC / donViTinh; n.ChiDacThuNganhPhanCapCTC = n.ChiDacThuNganhPhanCapCTC / donViTinh;
                                return n;
                            }).ToList();
                            listDetailPrint = listDetailPrint.OrderBy(x => x.KyHieu).ToList();

                            if (listDetailPrint.Count() > 0)
                            {
                                Dictionary<string, object> data = new Dictionary<string, object>();

                                double tongTuChiCTC = 0;
                                double tongTuChi = 0;
                                double tongSuDungTonKhoCTC = 0;
                                double tongSuDungTonKho = 0;
                                double tongChiDacThuCTC = 0;
                                double tongChiDacThu = 0;

                                tongTuChiCTC = listDetailPrint.Where(n => !n.IsHangCha).Sum(n => n.TuChiCTC);
                                tongTuChi = listDetailPrint.Where(n => !n.IsHangCha).Sum(n => n.TuChi);
                                tongSuDungTonKhoCTC = listDetailPrint.Where(n => !n.IsHangCha).Sum(n => n.SuDungTonKhoCTC);
                                tongSuDungTonKho = listDetailPrint.Where(n => !n.IsHangCha).Sum(n => n.SuDungTonKho);
                                tongChiDacThuCTC = listDetailPrint.Where(n => !n.IsHangCha).Sum(n => n.ChiDacThuNganhPhanCapCTC);
                                tongChiDacThu = listDetailPrint.Where(n => !n.IsHangCha).Sum(n => n.ChiDacThuNganhPhanCap);

                                data.Add("TongTuChiCTC", tongTuChiCTC);
                                data.Add("TongTuChi", tongTuChi);
                                data.Add("TongSuDungTonKhoCTC", tongSuDungTonKhoCTC);
                                data.Add("TongSuDungTonKho", tongSuDungTonKho);
                                data.Add("TongChiDacThuCTC", tongChiDacThuCTC);
                                data.Add("TongChiDacThu", tongChiDacThu);
                                data.Add("ListData", listDetailPrint);
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
                                fileNamePrefix = string.Format("report_nsbd_{0}_", nganhItem.NameItem);
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ThDChungTuReportNSBDQuery>(templateFileName, data);
                                results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

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

        private void PrintExcel()
        {
            try
            {
                PrintReport(false, false);
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
                PrintReport();
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
                PrintReport(true, true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_NGANHTHAMDINH_SKT) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_NGANHTHAMDINH_SKT;
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


