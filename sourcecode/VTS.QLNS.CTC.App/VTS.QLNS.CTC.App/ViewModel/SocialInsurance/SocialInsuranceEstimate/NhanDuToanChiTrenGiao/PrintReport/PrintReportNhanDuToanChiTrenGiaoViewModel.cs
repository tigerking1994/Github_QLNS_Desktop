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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao.PrintReport
{
    public class PrintReportNhanDuToanChiTrenGiaoViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ICollectionView _donViCollectionView;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly ILog _logger;
        private readonly IDanhMucService _danhMucService;
        private SessionInfo _sessionInfo;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly INdtctgBHXHService _ndtctgBHXHService;
        private readonly INdtctgBHXHChiTietService _ndtctgBHXHChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;

        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _typeChuky;
        private string _diaDiem;
        private string _cap1;

        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        private List<BhKhcCheDoBhXhModel> _lstKhcCdBhxhChiTiet;
        public List<BhKhcCheDoBhXhModel> LstKhcCdBhxhChiTiet
        {
            get => _lstKhcCdBhxhChiTiet;
            set => SetProperty(ref _lstKhcCdBhxhChiTiet, value);
        }

        public override Type ContentType => typeof(PrintReportNhanDuToanChiTrenGiao);

        private bool isActive;

        public static NdtcheckPrintType NdtcheckPrintType { get; set; }
        private ComboboxItem _cbxExpenseTypeSelected;
        public ComboboxItem CbxExpenseTypeSelected
        {
            get => _cbxExpenseTypeSelected;
            set
            {
                SetProperty(ref _cbxExpenseTypeSelected, value);
                if (_cbxExpenseTypeSelected != null)
                {
                    LoadLNS();
                    GetTypeChuLyTheoLoaiChi();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxExpenseType;
        public ObservableCollection<ComboboxItem> CbxExpenseType
        {
            get => _cbxExpenseType;
            set => SetProperty(ref _cbxExpenseType, value);
        }
        private string _txtTitleFirst;

        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set
            {
                SetProperty(ref _txtTitleFirst, value);
            }
        }
        private string _txtTitleSecond;

        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }
        private string _txtTitleThird;

        public string TxtTitleThird
        {
            get => _txtTitleThird;
            set
            {

                SetProperty(ref _txtTitleThird, value);
            }
        }

        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }

        private bool _isEnabledUnit;
        public bool IsEnabledUnit
        {
            get => _isEnabledUnit;
            set => SetProperty(ref _isEnabledUnit, value);
        }

        private ComboboxItem _paperPrintTypeSelected;

        public ComboboxItem PaperPrintTypeSelected
        {
            get => _paperPrintTypeSelected;
            set
            {
                SetProperty(ref _paperPrintTypeSelected, value);
                LoadTitleFirst();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsKieuGiayIn = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ItemsKieuGiayIn
        {
            get => _itemsKieuGiayIn;
            set => SetProperty(ref _itemsKieuGiayIn, value);
        }

        private ComboboxItem _selectedKieuGiayIn;

        public ComboboxItem SelectedKieuGiayIn
        {
            get => _selectedKieuGiayIn;
            set => SetProperty(ref _selectedKieuGiayIn, value);
        }

        private ObservableCollection<ComboboxItem> _catUnitTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> CatUnitTypes
        {
            get => _catUnitTypes;
            set => SetProperty(ref _catUnitTypes, value);
        }

        private ComboboxItem _catUnitTypeSelected;

        public ComboboxItem CatUnitTypeSelected
        {
            get => _catUnitTypeSelected;
            set => SetProperty(ref _catUnitTypeSelected, value);
        }

        private ICollectionView _lnsView;

        private ObservableCollection<BhDmMucLucNganSachModel> _listLns;
        public ObservableCollection<BhDmMucLucNganSachModel> ListLns
        {
            get => _listLns;
            set => SetProperty(ref _listLns, value);
        }
        private bool _selectAllLns;
        public bool SelectAllLns
        {
            get => ListLns.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllLns, value);
                foreach (var item in ListLns) item.IsChecked = _selectAllLns;
            }
        }

        public string LabelSelectedCountLns
        {
            get => $"LNS ({ListLns.Count(item => item.IsChecked)}/{ListLns.Count})";
        }

        private string _searchLns;

        public string SearchLns
        {
            get => _searchLns;
            set
            {
                if (SetProperty(ref _searchLns, value))
                {
                    _lnsView.Refresh();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _typeDotPhanBo = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> TypeDotPhanBo
        {
            get => _typeDotPhanBo;
            set => SetProperty(ref _typeDotPhanBo, value);
        }

        private ComboboxItem _selectDotPhanBo;
        public ComboboxItem SelectDotPhanBo
        {
            get => _selectDotPhanBo;
            set
            {
                SetProperty(ref _selectDotPhanBo, value);
            }
        }

        public PrintReportNhanDuToanChiTrenGiaoViewModel(
            INsDonViService nsDonViService,
            IExportService exportService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IDmChuKyService dmChuKyService,
            ISessionService sessionService,
            IDanhMucService danhMucService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            INdtctgBHXHService ndtctgBHXHService,
            INdtctgBHXHChiTietService ndtctgBHXHChiTietService,
            IMapper mapper,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService
            )
        {
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _logger = logger;
            _mapper = mapper;
            _dmChuKyService = dmChuKyService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _ndtctgBHXHService = ndtctgBHXHService;
            _ndtctgBHXHChiTietService = ndtctgBHXHChiTietService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;

            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            //ExportExcelActionCommand = new RelayCommand(obj => OnExportExcel());
            ExportPdfActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            InitReportDefaultDate();
            Clear();
            LoadDotPhanBo();
            LoadExpenseType();
            LoadTypeChuKy();
            LoadTitleFirst();
            LoadCatUnitTypes();
            LoadLNS();
            LoadKieuGiayIn();
            LoadDiaDiem();

        }

        public void LoadDotPhanBo()
        {
            var typeReport = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Đầu năm", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Bổ sung", ValueItem = "2"},
            };

            TypeDotPhanBo = new ObservableCollection<ComboboxItem>(typeReport);
            SelectDotPhanBo = TypeDotPhanBo.ElementAt(0);
        }

        private void LoadDiaDiem()
        {
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).FirstOrDefault(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM);
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
        }

        private void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }

        private void LoadCatUnitTypes()
        {
            _catUnitTypes = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH));
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DanhMuc> data = _danhMucService.FindByCondition(predicate).OrderBy(x => x.SGiaTri).ToList();
            if (data.Count == 0)
            {
                _catUnitTypes.Insert(0, new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            else
                _catUnitTypes = _mapper.Map<ObservableCollection<ComboboxItem>>(data);

            _catUnitTypeSelected = _catUnitTypes.FirstOrDefault();
        }

        private void LoadTitleFirst()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TxtTitleFirst = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
            if (_dmChuKy == null)
            {
                TxtTitleFirst = "THÔNG BÁO CẤP CHỈ TIÊU bẢO HIỂM NĂM " + _sessionService.Current.YearOfWork;
            }

            TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
            TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
        }

        private void LoadExpenseType()
        {
            var listDanhMucChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
            var cbxExpense = listDanhMucChi?.Select(x => new ComboboxItem
            {
                DisplayItem = x.STenDanhMucLoaiChi,
                HiddenValue = x.SLNS.ToString(),
                ValueItem = x.SMaLoaiChi,
                Id = x.Id
            }).ToList();
            CbxExpenseType = new ObservableCollection<ComboboxItem>(cbxExpense);
            if (CbxExpenseType.Count() > 0)
            {
                CbxExpenseTypeSelected = CbxExpenseType.ElementAt(0);
            }
        }

        public void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            string idDonVi = _sessionService.Current.IdDonVi;

            List<BhDmMucLucNganSach> lstBhmuclucngansach = new List<BhDmMucLucNganSach>();
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);

            lstBhmuclucngansach = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).ToList();

            List<BhDmMucLucNganSachModel> lstBhmuclucngansachmodel = new List<BhDmMucLucNganSachModel>();

            lstBhmuclucngansachmodel = lstBhmuclucngansach.Select(d => new BhDmMucLucNganSachModel
            {
                Id = d.Id,
                SXauNoiMa = d.SXauNoiMa,
                SLNS = d.SLNS,
                SL = d.SL,
                SK = d.SK,
                SMoTa = d.SLNS + "-" + d.SMoTa,
                IIDMLNS = d.IIDMLNS,
                IIDMLNSCha = d.IIDMLNSCha,
                BHangCha = d.BHangCha,
                INamLamViec = d.INamLamViec
            }).ToList();

            if (CbxExpenseTypeSelected != null)
            {
                var dataSLNS = CbxExpenseTypeSelected.HiddenValue.Split(',');
                lstBhmuclucngansachmodel = lstBhmuclucngansachmodel.Where(x => dataSLNS.Contains(x.SLNS)).OrderBy(x => x.SXauNoiMa).ToList();
            }

            ListLns = new ObservableCollection<BhDmMucLucNganSachModel>(lstBhmuclucngansachmodel);

            // Filter
            _lnsView = CollectionViewSource.GetDefaultView(ListLns);
            _lnsView.Filter = ListLNSFilter;


            if (_listLns != null && _listLns.Count > 0)
            {

                foreach (var model in _listLns)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsChecked))
                        {
                            foreach (var item in _listLns)
                            {
                                if (item.IIDMLNSCha == model.IIDMLNS)
                                {
                                    item.IsChecked = model.IsChecked;
                                }
                            }
                            OnPropertyChanged(nameof(LabelSelectedCountLns));
                            OnPropertyChanged(nameof(SelectAllLns));
                        }
                    };
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (BhDmMucLucNganSachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchLns))
                result = item.SMoTa.ToLower().Contains(_searchLns!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void AddListTreeChilDanhMuc(BhDmMucLucNganSachModel danhmuc, List<BhDmMucLucNganSachModel> lstDanhMuc)
        {
            if (lstDanhMuc.Any(n => n.IIDMLNSCha == danhmuc.IIDMLNS))
            {
                foreach (var item in lstDanhMuc.Where(n => n.IIDMLNSCha == danhmuc.IIDMLNS))
                {
                    AddListTreeChilDanhMuc(item, lstDanhMuc);
                }
            }
        }

        private void Clear()
        {
            _donViCollectionView = null;
        }


        private void OnExport(ExportType exportType)
        {
            if (NdtcheckPrintType.NDTCCTNS.Equals(NdtcheckPrintType))
            {
                OnPrintReportThongBaoChiTieuNganSach(exportType);
            }
        }

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            if (dmChuKy == null) return string.Empty;
            var loaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return loaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => string.Empty,
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }

        private void OnPrintReportThongBaoChiTieuNganSach(ExportType exportType)
        {
            ListLns.ForAll(n =>
            {
                n.IsChecked = true;
            });
            var lstLNsCheck = ListLns.Where(x => x.IsChecked).ToList();
            if (lstLNsCheck.Count() <= 0)
            {
                MessageBoxHelper.Warning(Resources.MsgLNSEmpty);
                return;
            }
            try
            {

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    string idDonVi = _sessionService.Current.IdDonVi;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    string lstLNS = CbxExpenseTypeSelected.HiddenValue;
                    int dotNhan = int.Parse(SelectDotPhanBo.ValueItem);
                    string sMaLoaiChi = CbxExpenseTypeSelected.ValueItem;
                    List<BhDtctgBHXHChiTietQuery> lstData = new List<BhDtctgBHXHChiTietQuery>();
                    lstData = _ndtctgBHXHChiTietService.GetBaoCaoChiTieuNganSach(idDonVi, yearOfWork, lstLNS, dotNhan, sMaLoaiChi, donViTinh).ToList();

                    CalculateData(lstData);
                    lstData = lstData.Where(x => (x.FTienTuChi ?? 0) > 0).OrderBy(x => x.SXauNoiMa).ToList();

                    var fTongCongTongTien = lstData.Where(x => !x.BHangCha).Sum(x => x.FTongTien);
                    var fTongCongTienHienVat = lstData.Where(x => !x.BHangCha).Sum(x => x.FTienHienVat);
                    var fTongCongTienTuChi = lstData.Where(x => !x.BHangCha).Sum(x => x.FTienTuChi);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    if (CbxExpenseTypeSelected.HiddenValue != LNSValue.LNS_9010003)
                    {
                        lstData.ForEach(x =>
                        {
                            x.SLNS = string.Empty;
                            x.SM = string.Empty;
                            x.STM = string.Empty;
                            x.STTM = string.Empty;
                            x.SNG = string.Empty;
                        });
                    }

                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("YearWork", yearOfWork);
                    data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : _cap1);
                    data.Add("DonVi", _sessionInfo.TenDonVi.ToUpper());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("FTongCongTongTien", fTongCongTongTien);
                    data.Add("FTongCongTienHienVat", fTongCongTienHienVat);
                    data.Add("FTongCongTienTuChi", fTongCongTienTuChi);
                    data.Add("ListData", lstData);
                    data.Add("TongSoTien", fTongCongTienTuChi != null ? StringUtils.NumberToText((double)fTongCongTienTuChi * donViTinh, true) : string.Empty);
                    AddChuKy(data, _typeChuky);
                    data.Add("ThoiGian", _diaDiem + ", " + DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    string templateFileName;
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_BH_NDT_THONGBAOCAPCHITIEUNGANSACH_DOC));
                    string fileNamePrefix;
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                    var xlsFile = _exportService.Export<BhDtctgBHXHChiTietQuery>(templateFileName, data);
                    results.Add(new ExportResult("THÔNG BÁO CẤP CHỈ TIÊU BẢO HIỂM " + _sessionInfo.YearOfWork, filename, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
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

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh1MoTa);
            data.Add("ChucDanh1", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh1MoTa);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy == null ? string.Empty : dmChyKy.Ten1MoTa);
            data.Add("ThuaLenh2", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh2MoTa);
            data.Add("ChucDanh2", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh2MoTa);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy == null ? string.Empty : dmChyKy.Ten2MoTa);
            data.Add("ThuaLenh3", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh3MoTa);
            data.Add("ChucDanh3", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh3MoTa);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy == null ? string.Empty : dmChyKy.Ten3MoTa);
        }

        private void CalculateData(List<BhDtctgBHXHChiTietQuery> lstNdtChungTuChiTiet)
        {
            lstNdtChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FTienHienVat = 0;
                    x.FTienTuChi = 0;
                    x.FTongTien = 0;
                    return x;
                }).ToList();
            var temp = lstNdtChungTuChiTiet.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {
                CalculateParent(item.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid idParent, BhDtctgBHXHChiTietQuery item, List<BhDtctgBHXHChiTietQuery> lstNdtChungTuChiTiet)
        {
            var dictByMlns = lstNdtChungTuChiTiet.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }
            var model = dictByMlns[idParent];
            model.FTienTuChi = (model.FTienTuChi ?? 0) + (item.FTienTuChi ?? 0);
            model.FTienHienVat = (model.FTienHienVat ?? 0) + (item.FTienHienVat ?? 0);
            model.FTongTien = (model.FTongTien ?? 0) + (item.FTongTien ?? 0);
            CalculateParent(model.IID_MLNS_Cha, item, lstNdtChungTuChiTiet);
        }

        private string GetTemplate(string input)
        {
            if (SelectedKieuGiayIn.ValueItem == "1")
                input = input + "_Doc";
            return Path.Combine(ExportPrefix.PATH_BH_NDT, input + FileExtensionFormats.Xlsx);
        }

        public object ConvertNumberToString(object value)
        {
            if (value == null) return null;
            int input = (int)value;
            if (input == 0)
                return null;
            return value;
        }


        private void LoadTypeChuKy()
        {
            switch (NdtcheckPrintType)
            {
                case NdtcheckPrintType.NDTCCTNS:
                    GetTypeChuLyTheoLoaiChi();
                    break;
            }
        }

        private void GetTypeChuLyTheoLoaiChi()
        {
            string sLNS = CbxExpenseTypeSelected.HiddenValue;
            switch (sLNS)
            {
                case LNSValue.LNS_9010001_9010002:
                case LNSValue.LNS_901_9010001_9010002:
                    _typeChuky = TypeChuKy.RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_BHXH;
                    break;
                case LNSValue.LNS_9010003:
                    _typeChuky = TypeChuKy.RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_KPQL;
                    break;
                case LNSValue.LNS_9010004_9010005:
                    _typeChuky = TypeChuKy.RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_KCB_QY;
                    break;
                case LNSValue.LNS_9010006_9010007:
                    _typeChuky = TypeChuKy.RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_KCB_TS;
                    break;
                case LNSValue.LNS_9010008:
                    _typeChuky = TypeChuKy.RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_KCB_BHYT_QN;
                    break;
                case LNSValue.LNS_9010009:
                    _typeChuky = TypeChuKy.RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_KCB_MSTTB_YT;
                    break;
                case LNSValue.LNS_9010010:
                    _typeChuky = TypeChuKy.RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_HT_BHTN;
                    break;
                case LNSValue.LNS_9050001_9050002:
                    _typeChuky = TypeChuKy.RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_HT_BHTN;
                    break;
            }

        }
        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TxtTitleFirst = chuKy.TieuDe1MoTa;
                TxtTitleSecond = chuKy.TieuDe2MoTa;
                TxtTitleThird = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
