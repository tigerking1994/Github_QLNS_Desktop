using AutoMapper;
using AutoMapper.Internal;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.IO;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Model.Report;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportTargetMajorsViewModel : ReportViewModelBase<DtChungTuModel, ReportChiTieuDuToanQuery, ReportChiTieuDuToanQuery>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _specializedView;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDtNhanPhanBoMapService _dtChungTuMapService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private SessionInfo _sessionInfo;
        private DuToanTongChiTieu _tongChiTieu;
        private List<ReportChiTieuDuToanQuery> _listDataReportChiTieuNganh;

        public override string Name
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeName[(int)DivisionEstimatePrintType.TARGET_MAJORS];
        }

        public override string Title
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeTitle[(int)DivisionEstimatePrintType.TARGET_MAJORS];
        }

        public override string Description
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeDescription[(int)DivisionEstimatePrintType.TARGET_MAJORS];
        }

        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportTargetMajors);

        private bool _isExportData;
        public bool IsExportData
        {
            get => ListSpecialized.Any() && ListSpecialized.Any(x => x.IsChecked);
            set => SetProperty(ref _isExportData, value);
        }

        // start handle chon dot
        private ObservableCollection<ComboboxManyItem> _dataDot;
        public ObservableCollection<ComboboxManyItem> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ComboboxManyItem _dataDotSelected;
        public ComboboxManyItem DataDotSelected
        {
            get => _dataDotSelected;
            set
            {
                SetProperty(ref _dataDotSelected, value);
                if (_dataDotSelected != null)
                {
                    LoadSpecialized();
                }
            }
        }

        private bool _checkPrintAccumulation;
        public bool CheckPrintAccumulation
        {
            get => _checkPrintAccumulation;
            set
            {
                SetProperty(ref _checkPrintAccumulation, value);
                LoadSpecialized();
            }
        }

        private bool _checkPrintSpecialized;
        public bool CheckPrintSpecialized
        {
            get => _checkPrintSpecialized;
            set
            {
                SetProperty(ref _checkPrintSpecialized, value);
                LoadSpecialized();
            }
        }

        private ObservableCollection<ComboboxItem> _cbxTitleFirst = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> CbxTitleFirst
        {
            get => _cbxTitleFirst;
            set => SetProperty(ref _cbxTitleFirst, value);
        }

        private ComboboxItem _cbxTitleFirstSelected;

        public ComboboxItem CbxTitleFirstSelected
        {
            get => _cbxTitleFirstSelected;
            set => SetProperty(ref _cbxTitleFirstSelected, value);
        }

        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }

        private bool _checkPrintTNG;
        public bool CheckPrintTNG
        {
            get => _checkPrintTNG;
            set => SetProperty(ref _checkPrintTNG, value);
        }

        private bool _checkPrintAgency;
        public bool CheckPrintAgency
        {
            get => _checkPrintAgency;
            set => SetProperty(ref _checkPrintAgency, value);
        }

        // start handle Specialized
        public List<DanhMuc> ListDanhMuc = new List<DanhMuc>();
        private ObservableCollection<CheckBoxItem> _listSpecialized = new ObservableCollection<CheckBoxItem>();
        public ObservableCollection<CheckBoxItem> ListSpecialized
        {
            get => _listSpecialized;
            set => SetProperty(ref _listSpecialized, value);
        }

        private string _labelSelectedCountSpecialized;
        public string LabelSelectedCountSpecialized
        {
            get
            {
                if (_checkPrintSpecialized)
                    _labelSelectedCountSpecialized = $"CHỌN CHUYÊN NGÀNH ({ListSpecialized.Count(item => item.IsChecked)}/{ListSpecialized.Count})";
                else
                    _labelSelectedCountSpecialized = $"CHỌN NGÀNH ({ListSpecialized.Count(item => item.IsChecked)}/{ListSpecialized.Count})";

                return _labelSelectedCountSpecialized;
            }
            set => SetProperty(ref _labelSelectedCountSpecialized, value);
        }

        private bool _selectAllSpecialized;

        public bool SelectAllSpecialized
        {
            get => ListSpecialized.Any() && ListSpecialized.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllSpecialized, value);
                foreach (var item in ListSpecialized) item.IsChecked = _selectAllSpecialized;
            }
        }

        private string _searchSpecialized;
        public string SearchSpecialized
        {
            get => _searchSpecialized;
            set
            {
                if (SetProperty(ref _searchSpecialized, value))
                {
                    _specializedView.Refresh();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set
            {
                SetProperty(ref _cbxVoucherTypeSelected, value);
                if (_cbxVoucherTypeSelected != null)
                    LoadDotPhanBo();
            }
        }

        public RelayCommand ConfigSignCommand { get; }

        public PrintReportTargetMajorsViewModel(
            IMapper mapper,
            INsDtChungTuService dtChungTuService,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsDtNhanPhanBoMapService dtChungTuMapService,
            IDanhMucService danhMucService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            ISktSoLieuService sktSoLieuService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel) : base(exportService, danhMucService, sessionService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _dtChungTuService = dtChungTuService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _dtChungTuMapService = dtChungTuMapService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _sktSoLieuService = sktSoLieuService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            if (Model == null)
            {
                Model = new DtChungTuModel();
            }
            _sessionInfo = _sessionService.Current;
            LoadVoucherType();
            if (!Models.Any())
            {
                return;
            }
            LoadCbxTitleFirst();
            LoadPaperPrintTypes();
            LoadCatUnitTypes();
            LoadSpecialized();
        }

        private void LoadVoucherType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key}
            };

            CbxVoucherType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            CbxVoucherTypeSelected = CbxVoucherType.ElementAt(0);

            OnPropertyChanged(nameof(CbxVoucherType));
            OnPropertyChanged(nameof(CbxVoucherTypeSelected));
        }

        private void LoadDotPhanBo()
        {
            var predicate = CreatePredicateChungTuIndex();
            var listDtChungTu = _dtChungTuService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
            Models = _mapper.Map<ObservableCollection<DtChungTuModel>>(listDtChungTu);

            DataDot = _mapper.Map<ObservableCollection<ComboboxManyItem>>(Models);
            DataDotSelected = DataDot.FirstOrDefault();

            OnPropertyChanged(nameof(DataDot));
            OnPropertyChanged(nameof(DataDotSelected));
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicateChungTuIndex()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.IIdDotNhan));
            predicate = predicate.And(x => x.ILoaiChungTu.HasValue && x.ILoaiChungTu.Value == int.Parse(CbxVoucherTypeSelected.ValueItem));
            return predicate;
        }

        private void LoadCbxTitleFirst()
        {
            var cbxTitleFirst = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Dự toán chi ngân sách năm " + _sessionInfo.YearOfWork, ValueItem = "1"},
            };

            CbxTitleFirst = new ObservableCollection<ComboboxItem>(cbxTitleFirst);
            CbxTitleFirstSelected = cbxTitleFirst.ElementAt(0);
        }

        private void LoadSpecialized()
        {
            ListDanhMuc = new List<DanhMuc>();
            var yearOfWork = _sessionInfo.YearOfWork;
            ListSpecialized = new ObservableCollection<CheckBoxItem>();

            if (_dataDotSelected != null && Models.Any(x => x.Id.Equals(_dataDotSelected.Id)))
            {
                var dtChungTuSelected = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
                var idChungTu = dtChungTuSelected.Id.ToString();
                if (_checkPrintAccumulation)
                {
                    idChungTu = string.Join(StringUtils.COMMA, Models.Where(x => x.DNgayChungTu.HasValue && x.DNgayChungTu <= DateUtils.EndOfDay(dtChungTuSelected.DNgayChungTu.Value))
                            .Select(x => x.Id.ToString()));
                }

                var type = EstimationReport.DU_TOAN_THEO_NGANH;
                if (_checkPrintSpecialized)
                {
                    type = EstimationReport.DU_TOAN_THEO_CHUYEN_NGANH;
                }
                ListDanhMuc = _danhMucService.FindDanhMucTheoNganh(idChungTu, yearOfWork, type).ToList();
            }

            ListSpecialized = _mapper.Map<ObservableCollection<CheckBoxItem>>(ListDanhMuc);
            OnPropertyChanged(nameof(LabelSelectedCountSpecialized));
            OnPropertyChanged(nameof(SelectAllSpecialized));
            OnPropertyChanged(nameof(IsExportData));
            // Filter
            _specializedView = CollectionViewSource.GetDefaultView(ListSpecialized);
            _specializedView.Filter = obj => string.IsNullOrWhiteSpace(_searchSpecialized)
                                             || (obj is CheckBoxItem item && item.DisplayItem.Contains(_searchSpecialized.Trim(), StringComparison.OrdinalIgnoreCase));

            foreach (var model in ListSpecialized)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountSpecialized));
                    OnPropertyChanged(nameof(SelectAllSpecialized));
                    OnPropertyChanged(nameof(IsExportData));
                };
            }
        }

        public override string GetFileTemplate(string strPageNumber = "")
        {
            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            var loaiChungTuStr = "NSSD";
            if (VoucherType.NSBD_Key.Equals(dataDotSelectedModel.ILoaiChungTu.ToString()))
            {
                loaiChungTuStr = "NSBD";
            }

            var inTieuNganh = string.Empty;
            if (CheckPrintTNG)
                inTieuNganh = "_TNG";
            var paper = "Normal";
            if (PaperPrintTypeSelected.ValueItem.Equals("2"))
            {
                paper = "A4Ngang";
            }

            return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH}_{loaiChungTuStr}{inTieuNganh}_{paper}{strPageNumber}{StringUtils.EXCEL_EXTENSION}");
        }

        public override IEnumerable<ReportChiTieuDuToanQuery> GetData()
        {
            int donViTinh = GetDonViTinh();
            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            var listDataReportChiTieuNganh = _dtChungTuChiTietService.GetDataReportChiTieuNganh(_sessionInfo.YearOfWork,
                                                                            _sessionInfo.Budget,
                                                                            _sessionInfo.YearOfBudget,
                                                                            string.Join(",", ListSpecialized.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList()),
                                                                            dataDotSelectedModel.Id.ToString(),
                                                                            Convert.ToInt32(CbxVoucherTypeSelected.ValueItem),
                                                                            donViTinh,
                                                                            CheckPrintAccumulation,
                                                                            CheckPrintAgency).ToList();
          
            return listDataReportChiTieuNganh;
        }

        public override List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExport(IEnumerable<ReportChiTieuDuToanQuery> dataExport, string extension)
        {
            int donViTinh = GetDonViTinh();
            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            var dictDanhMucById = ListDanhMuc.ToDictionary(x => x.Id);
            var listSpecializedChecked = ListSpecialized.Where(x => x.IsChecked).ToList();
            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();

            foreach (var item in listSpecializedChecked)
            {
                List<ReportChiTieuDuToanQuery> listHeader = new List<ReportChiTieuDuToanQuery>();
                _listDataReportChiTieuNganh = new List<ReportChiTieuDuToanQuery>();
                var dataExportOfSpecialized = dataExport.Where(x => item.ValueItem.Contains(x.NG)).ToList();
                CalculateTotal(dataExportOfSpecialized.ToList());
                if (dataExportOfSpecialized != null && dataExportOfSpecialized.ToList().Count > 0)
                {
                    List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionInfo.YearOfWork,
                                                                string.Join(",", dataExportOfSpecialized.Select(n => n.XauNoiMa).ToList())).ToList();
                    if (listParent != null && listParent.Count > 0)
                    {
                        foreach (NsMucLucNganSach mlnsItem in listParent)
                        {
                            listHeader.Add(new ReportChiTieuDuToanQuery
                            {
                                LNS = mlnsItem.Lns,
                                L = mlnsItem.L,
                                K = mlnsItem.K,
                                M = mlnsItem.M,
                                TM = mlnsItem.Tm,
                                TTM = mlnsItem.Ttm,
                                NG = mlnsItem.Ng,
                                MoTa = mlnsItem.MoTa,
                                XauNoiMa = mlnsItem.XauNoiMa,
                                HienVat = 0,
                                TuChi = 0,
                                IsHangCha = true,
                                MlnsId = mlnsItem.MlnsId,
                                MlnsIdParent = mlnsItem.MlnsIdParent
                            });
                        }
                        foreach (ReportChiTieuDuToanQuery exportItem in dataExportOfSpecialized)
                        {
                            int index = listHeader.IndexOf(listHeader.Where(n => n.TM == exportItem.TM && string.IsNullOrEmpty(n.TTM) && !string.IsNullOrEmpty(n.TM)).FirstOrDefault());
                            if (index >= 0)
                            {
                                exportItem.MlnsIdParent = listHeader[index].MlnsId;
                                if (CheckPrintAgency)
                                {
                                    if (!string.IsNullOrEmpty(exportItem.MaDonVi))
                                    {
                                        exportItem.TTM = string.Empty;
                                        if (CheckPrintTNG)
                                            exportItem.NG = string.Empty;
                                        if (CheckPrintTNG)
                                            exportItem.TNG = exportItem.MaDonVi;
                                        else
                                            exportItem.NG = exportItem.MaDonVi;
                                        exportItem.MoTa = exportItem.TenDonVi;
                                    }
                                }
                                listHeader.Insert(index + 1, exportItem);
                            }
                        }
                    }
                    _listDataReportChiTieuNganh = new List<ReportChiTieuDuToanQuery>(listHeader);
                    _listDataReportChiTieuNganh.Where(n => !n.IsHangCha).Select(n => { n.LNS = string.Empty; n.M = string.Empty; n.TM = string.Empty; return n; }).ToList();
                    _listDataReportChiTieuNganh.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.M)).Select(n => { n.LNS = string.Empty; return n; }).ToList();
                    _listDataReportChiTieuNganh.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.TM)).Select(n => { n.M = string.Empty; return n; }).ToList();
                }
                CalculateDataLNS();

                var danhMucItem = dictDanhMucById.GetValueOrDefault(item.Id, new DanhMuc());
                var data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(donViTinh, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                data.Add("FormatNumber", formatNumber);
                data.Add("TitleFirst", CbxTitleFirstSelected.DisplayItem);
                data.Add("TitleSecond", TxtTitleSecond);
                data.Add("TenNganh", danhMucItem.STen);
                data.Add("TongSo", _tongChiTieu.TongSo);
                data.Add("TotalTuChi", _tongChiTieu.TongTuChi);
                data.Add("TotalHienVat", _tongChiTieu.TongHienVat);
                data.Add("TotalDuPhong", _tongChiTieu.TongDuPhong);
                data.Add("TotalHangNhap", _tongChiTieu.TongHangNhap);
                data.Add("TotalHangMua", _tongChiTieu.TongHangMua);
                data.Add("TotalPhanCap", _tongChiTieu.TongPhanCap);
                data.Add("CatUnitType", string.Format("Đơn vị tính: {0}", CatUnitTypeSelected.DisplayItem));

                data.Add("TotalTuChiText", StringUtils.NumberToText(_tongChiTieu.TongTuChi * donViTinh));
                data.Add("TotalHienVatText", StringUtils.NumberToText(_tongChiTieu.TongHienVat * donViTinh));
                data.Add("TotalDuPhongText", StringUtils.NumberToText(_tongChiTieu.TongDuPhong * donViTinh));
                data.Add("TotalHangNhapText", StringUtils.NumberToText(_tongChiTieu.TongHangNhap * donViTinh));
                data.Add("TotalHangMuaText", StringUtils.NumberToText(_tongChiTieu.TongHangMua * donViTinh));
                data.Add("TotalPhanCapText", StringUtils.NumberToText(_tongChiTieu.TongPhanCap * donViTinh));
                data.Add("Items", _listDataReportChiTieuNganh);

                var templateFileName = GetFileTemplate();
                var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_NGANH}_{StringUtils.RemoveAccents(danhMucItem.STen)}_{DateUtils.GetFormatDateReport()}{extension}";
                listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
            }

            return listResult;
        }

        public override void HandleAfterExport()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CalculateDataLNS()
        {
            _listDataReportChiTieuNganh.Where(x => x.IsHangCha).Select(x => { x.TuChi = 0; x.HienVat = 0; x.DuPhong = 0; x.HangNhap = 0; x.HangMua = 0; x.PhanCap = 0; return x; }).ToList();
            foreach (var item in _listDataReportChiTieuNganh.Where(x => !x.IsHangCha && string.IsNullOrEmpty(x.MaDonVi) && ((x.TuChi != 0 || x.HienVat != 0 || x.DuPhong != 0 || x.HangNhap != 0 || x.HangMua != 0 || x.PhanCap != 0))))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(ReportChiTieuDuToanQuery currentItem, ReportChiTieuDuToanQuery selfItem)
        {
            var parentItem = _listDataReportChiTieuNganh.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.DuPhong += selfItem.DuPhong;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            CalculateParent(parentItem, selfItem);
        }

        private void CalculateTotal(List<ReportChiTieuDuToanQuery> listData)
        {
            _tongChiTieu = new DuToanTongChiTieu();
            foreach (ReportChiTieuDuToanQuery item in listData.Where(x => !x.IsHangCha))
            {
                _tongChiTieu.TongTuChi += item.TuChi.HasValue ? item.TuChi.Value : 0;
                _tongChiTieu.TongHienVat += item.HienVat.HasValue ? item.HienVat.Value : 0;
                _tongChiTieu.TongDuPhong += item.DuPhong.HasValue ? item.DuPhong.Value : 0;
                _tongChiTieu.TongHangNhap += item.HangNhap.HasValue ? item.HangNhap.Value : 0;
                _tongChiTieu.TongHangMua += item.HangMua.HasValue ? item.HangMua.Value : 0;
                _tongChiTieu.TongPhanCap += item.PhanCap.HasValue ? item.PhanCap.Value : 0;
                _tongChiTieu.TongSo += item.TongSo;
            };
        }

        public int GetDonViTinh()
        {
            if (CatUnitTypeSelected == null || string.IsNullOrEmpty(CatUnitTypeSelected.ValueItem))
                return 1;
            return int.Parse(CatUnitTypeSelected.ValueItem);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_CHITIET_NGANH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_CHITIET_NGANH;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
