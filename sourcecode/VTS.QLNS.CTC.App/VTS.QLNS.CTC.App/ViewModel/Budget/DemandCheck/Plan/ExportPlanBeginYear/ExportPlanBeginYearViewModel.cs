using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan.ExportPlanBeginYear
{
    public class ExportPlanBeginYearViewModel : ViewModelBase
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private IDanhMucService _danhMucService;
        private IExportService _exportService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly ISoLieuChiTietPhanCapService _soLieuChiTietPhanCapService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly ISktSoLieuChiTietCanCuDataService _sktSoLieuChiTietCanCuDataService;
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;

        public List<PlanBeginYearModel> ListSelectedPlanVoucher;
        //public PlanBeginYearModel SelectedPlanSummary;
        public List<SktSoLieuChiTietMLNSModel> DataPlanBeginYearExport;
        public ComboboxItem SelectedLoaiNganSach;
        public string LoaiChungTu { get; set; }

        public string _cap1 { get; set; }
        public string HeaderThucHien { get; set; }
        public string HeaderChiTiet { get; set; }

        public override Type ContentType => typeof(View.Budget.DemandCheck.Plan.ExportPlanBeginYear.ExportPlanBeginYear);

        private ObservableCollection<ComboboxItem> _bTieuChiItems = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BTieuChiItems
        {
            get => _bTieuChiItems;
            set => SetProperty(ref _bTieuChiItems, value);
        }

        private ComboboxItem _bTieuChiSelected;
        public ComboboxItem BTieuChiSelected
        {
            get => _bTieuChiSelected;
            set
            {
                SetProperty(ref _bTieuChiSelected, value);
            }
        }

        public ImportTabIndex TabIndex;
        private List<DonVi> _listDonVi;
        public RelayCommand ExportCommand { get; }

        public void LoadTieuChis()
        {

            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                string chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                BTieuChiItems = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi, false));
                _bTieuChiSelected = BTieuChiItems != null ? BTieuChiItems[0] : null;
            }

        }

        public ExportPlanBeginYearViewModel(ILog logger,
                IMapper mapper,
                ISessionService sessionService,
                IDanhMucService danhMucService,
                INsDonViService donViService,
                INsQtChungTuChiTietService chungTuChiTietService,
                INsMucLucNganSachService mucLucNganSachService,
                IExportService exportService,
                ISoLieuChiTietPhanCapService soLieuChiTietPhanCapService,
                ISktSoLieuService sktSoLieuService,
                ISktSoLieuChiTietCanCuDataService sktSoLieuChiTietCanCuDataService,
                ICauHinhCanCuService iCauHinhCanCuService)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;

            ExportCommand = new RelayCommand(obj => OnExportAggregateData());
            _soLieuChiTietPhanCapService = soLieuChiTietPhanCapService;
            _sktSoLieuService = sktSoLieuService;
            _sktSoLieuChiTietCanCuDataService = sktSoLieuChiTietCanCuDataService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
        }

        public override void Init()
        {
            base.Init();
            LoadTieuChis();
            _sessionInfo = _sessionService.Current;
            //_listDonVi = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT).ToList();
            //_mucLucNganSaches = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();

        }

        private void OnExportAggregateData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();

                    int namLamViec = _sessionService.Current.YearOfWork;
                    List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(namLamViec).ToList();
                    List<string> lstHeader = new List<string>() { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                    List<bool> lstCheckData = new List<bool>() { false, false, false, false, false };
                    List<int> hideColumns = new List<int>();
                    foreach (var item in ListSelectedPlanVoucher)
                    {
                        GetDataPlanBeginYearExport(ref lstHeader, ref lstCheckData, item);

                        DataPlanBeginYearExport = new List<SktSoLieuChiTietMLNSModel>(DataPlanBeginYearExport.Where(n => n.ChiTiet != 0 || n.UocThucHien != 0 || n.QuyetToan != 0 || n.DuToan != 0
                            || n.HangNhap != 0 || n.HangMua != 0 || n.PhanCap != 0 || n.ChuaPhanCap != 0
                            || n.FTuChi1 != 0 || n.FTuChi2 != 0 || n.FTuChi3 != 0 || n.FTuChi4 != 0 || n.FTuChi5 != 0
                            || n.FMHHV1 != 0 || n.FMHHV2 != 0 || n.FMHHV3 != 0 || n.FMHHV4 != 0 || n.FMHHV5 != 0
                            || n.FPhanCap1 != 0 || n.FPhanCap2 != 0 || n.FPhanCap3 != 0 || n.FPhanCap4 != 0 || n.FPhanCap5 != 0
                            ).ToList());

                        List<int> columnHidden = new List<int>();
                        switch (BTieuChiSelected.ValueItem)
                        {
                            case nameof(MLNSFiled.NG):
                                DataPlanBeginYearExport = DataPlanBeginYearExport.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                                DataPlanBeginYearExport.Where(x => !string.IsNullOrEmpty(x.NG)).ForAll(x => { x.BHangChaDuToan = false; });
                                columnHidden.Add(8);
                                columnHidden.Add(9);
                                columnHidden.Add(10);
                                columnHidden.Add(11);
                                break;
                            case nameof(MLNSFiled.TNG):
                                DataPlanBeginYearExport = DataPlanBeginYearExport.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                                DataPlanBeginYearExport.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                                columnHidden.Add(9);
                                columnHidden.Add(10);
                                columnHidden.Add(11);
                                break;
                            case nameof(MLNSFiled.TNG1):
                                DataPlanBeginYearExport = DataPlanBeginYearExport.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                                DataPlanBeginYearExport.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                                columnHidden.Add(10);
                                columnHidden.Add(11);
                                break;
                            case nameof(MLNSFiled.TNG2):
                                DataPlanBeginYearExport = DataPlanBeginYearExport.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                                DataPlanBeginYearExport.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                                columnHidden.Add(11);
                                break;
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Header1", _cap1 != null ? _cap1.ToUpper() : "");
                        data.Add("Header2", _sessionService.Current.TenDonVi.ToUpper());

                        data.Add("Title1", lstHeader[0]);
                        data.Add("Title2", lstHeader[1]);
                        data.Add("Title3", lstHeader[2]);
                        data.Add("Title4", lstHeader[3]);
                        data.Add("Title5", lstHeader[4]);
                        data.Add("Count", 10000);
                        data.Add("HeaderThucHien", HeaderThucHien);
                        data.Add("HeaderChiTiet", HeaderChiTiet);
                        data.Add("Items", DataPlanBeginYearExport);
                        data.Add("MLNS", mucLucNganSaches);
                        data.Add("NamLamViec", _sessionService.Current.YearOfWork);

                        data.Add("ThoiGian", string.Format("TP.Hà Nội, ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));

                        string tenNganSach;
                        string templateFileName;
                        if (SelectedLoaiNganSach != null && SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
                        {
                            double tongTien = (DataPlanBeginYearExport != null && DataPlanBeginYearExport.Count > 0) ? DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.ChiTiet).Sum() : 0;
                            data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));

                            tenNganSach = "NSSD";
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.EPT_NS_DUTOANDAUNAM_CHUNGTU_TONGHOP_NSSD);
                        }
                        else
                        {
                            double tongTien = 0;
                            if (DataPlanBeginYearExport != null && DataPlanBeginYearExport.Count > 0)
                            {
                                tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.HangNhap).Sum();
                                tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.HangMua).Sum();
                                tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.PhanCap).Sum();
                                tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.ChuaPhanCap).Sum();
                            }
                            data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));

                            var dataPhanCap = _soLieuChiTietPhanCapService.GetSoLieuChiTietPhanCapDonVi0_1(_sessionService.Current.YearOfWork, item.Id).ToList();
                            ObservableCollection<SktSoLieuPhanCapModel> dataPhanCapExport = _mapper.Map<ObservableCollection<SktSoLieuPhanCapModel>>(dataPhanCap);
                            double tongTienPhanCap = (dataPhanCap != null && dataPhanCap.Count > 0) ? dataPhanCap.Where(n => !n.bHangCha).Select(n => n.TuChi).Sum() : 0;
                            data.Add("TongTienBangChuPhanCap", StringUtils.NumberToText(tongTienPhanCap, true));
                            //if (dataPhanCapExport == null || !dataPhanCapExport.Any())
                            //{
                            //    MessageBoxHelper.Warning(string.Format("Không có dữ liệu phân cấp"));
                            //    return;
                            //}

                            data.Add("ItemPhanCaps", dataPhanCapExport);
                            tenNganSach = "NSBD";
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.EPT_NS_DUTOANDAUNAM_CHUNGTU_TONGHOP_NSBD);
                        }
                        string fileNamePrefix = string.Format("rptDuToan_DauNam_ChungTu_{0}_{1}_{2}", item.SSoChungTu, item.TenDonVi, tenNganSach);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);

                        if (SelectedLoaiNganSach != null && SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
                        {

                            int startIndex = 16;
                            for (int i = 0; i < 5; i++)
                            {
                                if (!lstCheckData[i])
                                {
                                    columnHidden.Add(startIndex);
                                }
                                startIndex += 1;
                            }
                            //columnHidden.Add(18);
                            //columnHidden.Add(19);
                            //columnHidden.Add(20);
                            var xlsFile = _exportService.Export<SktSoLieuChiTietMLNSModel, NsMucLucNganSach>(templateFileName, data, columnHidden);
                            var nameRange = xlsFile.GetNamedRange(1);
                            nameRange.Comment = "Workbook";
                            xlsFile.SetNamedRange(nameRange);
                            xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                            xlsFile.SetCellValue(50, 50, "CheckSum");
                            xlsFile.SetRowHidden(50, true);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                        else
                        {
                            int startIndex = 16;
                            for (int i = 0; i < 5; i++)
                            {
                                if (!lstCheckData[i])
                                {
                                    columnHidden.AddRange(new List<int> { startIndex, startIndex + 1 });
                                }
                                startIndex += 2;
                            }
                            //columnHidden.Add(18);
                            //columnHidden.Add(19);
                            //columnHidden.Add(20);
                            var xlsFile = _exportService.Export<SktSoLieuChiTietMLNSModel, NsMucLucNganSach, SktSoLieuPhanCapModel>(templateFileName, data, columnHidden);
                            var nameRange = xlsFile.GetNamedRange(1);
                            nameRange.Comment = "Workbook";
                            xlsFile.SetNamedRange(nameRange);
                            xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                            xlsFile.SetCellValue(50, 50, "CheckSum");
                            xlsFile.SetRowHidden(50, true);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = results;

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        // var result = (ExportResult)e.Result;
                        var result = (List<ExportResult>)e.Result;

                        if (result != null)
                        {
                            _exportService.Open(result, ExportType.EXCEL);
                        }
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

        public void GetDataPlanBeginYearExport(ref List<string> lstHeader, ref List<bool> lstCheckData, PlanBeginYearModel selectedVoucher)
        {
            List<SktSoLieuChiTietMlnsQuery> dataPlanExport = _sktSoLieuService.FindByConditionDonVi0(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
         _sessionService.Current.Budget, 0, 0, selectedVoucher.Id_DonVi, LoaiChungTu, selectedVoucher.Id.ToString(), selectedVoucher.DsLNS).ToList();
            //DataPlanBeginYearExport = _mapper.Map<ObservableCollection<Model.SktSoLieuChiTietMLNSModel>>(dataPlanExport);
            var listHasMap = dataPlanExport.Where(n => !string.IsNullOrEmpty(n.SKT_KyHieu)).ToList();
            List<string> listXauNoiMaMap = new List<string>();
            listXauNoiMaMap.AddRange(StringUtils.GetListXauNoiMaParent(listHasMap.Select(n => n.XauNoiMa).ToList()));
            dataPlanExport = dataPlanExport.Where(n => listXauNoiMaMap.Contains(n.XauNoiMa)).ToList();

            ObservableCollection<SktSoLieuChiTietMLNSModel> data = _mapper.Map<ObservableCollection<SktSoLieuChiTietMLNSModel>>(dataPlanExport);
            LoadDataCanCuExport(ref data, ref lstHeader, ref lstCheckData, selectedVoucher);
            DataPlanBeginYearExport = new List<SktSoLieuChiTietMLNSModel>(data.ToList());
        }

        private void LoadDataCanCuExport(ref ObservableCollection<SktSoLieuChiTietMLNSModel> data, ref List<string> lstHeader, ref List<bool> lstCheckData, PlanBeginYearModel selectedVoucher)
        {
            var loaiChungTu = LoaiChungTu;
            int yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<NsCauHinhCanCu>();
            predicate = predicate.And(item => item.SModule == TypeModuleCanCu.PLAN_BEGIN_YEAR);
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate).OrderBy(n => n.INamCanCu);
            if (listCanCu == null) return;
            int DuToanNamTruocIndex = -1;
            int count = 0;
            foreach (var item in listCanCu)
            {
                if (item.IIDMaChucNang.Equals(TypeCanCu.ESTIMATE) && item.INamCanCu == yearOfWork - 1)
                {
                    DuToanNamTruocIndex = count;
                }
                var predicateCc = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
                predicateCc = predicateCc.And(x => x.IIdMaDonVi.Equals(selectedVoucher.Id_DonVi));
                predicateCc = predicateCc.And(x => x.IIdCanCu.HasValue && x.IIdCanCu.Equals(item.Id));
                predicateCc = predicateCc.And(x => x.IID_CTDTDauNam == selectedVoucher.Id);
                predicateCc = predicateCc.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                //predicateCc = predicateCc.And(x => x.LoaiChungTu == loaiChungTu);
                var lstCanCu = _sktSoLieuChiTietCanCuDataService.FindByCondition(predicateCc);

                foreach (var cc in lstCanCu)
                {
                    var mucLuc = data.FirstOrDefault(x => x.XauNoiMa == cc.SXauNoiMa);
                    if (mucLuc != null)
                    {

                        switch (item.IIDMaChucNang)
                        {
                            case TypeCanCu.SETTLEMENT:
                                lstCheckData[0] = true;
                                lstHeader[0] = item.STenCot;
                                mucLuc.FTuChi1 = cc.FTuChi;
                                mucLuc.FPhanCap1 = cc.FPhanCap;
                                mucLuc.FMHHV1 = cc.FHangNhap + cc.FHangMua;
                                mucLuc.HangMua1 = cc.FHangMua;
                                mucLuc.HangNhap1 = cc.FHangNhap;
                                break;
                            case TypeCanCu.ESTIMATE:
                                lstCheckData[1] = true;
                                lstHeader[1] = item.STenCot;
                                mucLuc.FTuChi2 = cc.FTuChi;
                                mucLuc.FPhanCap2 = cc.FPhanCap;
                                mucLuc.FMHHV2 = cc.FHangNhap + cc.FHangMua;
                                mucLuc.HangMua2 = cc.FHangMua;
                                mucLuc.HangNhap2 = cc.FHangNhap;
                                break;
                            case TypeCanCu.ALLOCATION:
                                lstCheckData[2] = true;
                                lstHeader[2] = item.STenCot;
                                mucLuc.FTuChi3 = cc.FTuChi;
                                mucLuc.FPhanCap3 = cc.FPhanCap;
                                mucLuc.FMHHV3 = cc.FHangNhap + cc.FHangMua;
                                mucLuc.HangMua3 = cc.FHangMua;
                                mucLuc.HangNhap3 = cc.FHangNhap;
                                break;
                            case TypeCanCu.DEMAND:
                                lstCheckData[3] = true;
                                lstHeader[3] = item.STenCot;
                                mucLuc.FTuChi4 = cc.FTuChi;
                                mucLuc.FPhanCap4 = cc.FPhanCap;
                                mucLuc.FMHHV4 = cc.FHangNhap + cc.FHangMua;
                                mucLuc.HangMua4 = cc.FHangMua;
                                mucLuc.HangNhap4 = cc.FHangNhap;
                                break;
                            case TypeCanCu.CHECK_NUMBER:
                                lstCheckData[4] = true;
                                lstHeader[4] = item.STenCot;
                                mucLuc.FTuChi5 = cc.FTuChi;
                                mucLuc.FPhanCap5 = cc.FPhanCap;
                                mucLuc.FMHHV5 = cc.FHangNhap + cc.FHangMua;
                                mucLuc.HangMua5 = cc.FHangMua;
                                mucLuc.HangNhap5 = cc.FHangNhap;
                                break;
                        }
                    }
                }
                count++;
            }
            CalculateData(ref data);
        }

        private void CalculateData(ref ObservableCollection<SktSoLieuChiTietMLNSModel> data)
        {
            data.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.ChiTiet = 0; x.UocThucHien = 0; x.HangMua = 0; x.HangNhap = 0; x.PhanCap = 0; x.ChuaPhanCap = 0;
                    x.FTuChi1 = 0; x.FTuChi2 = 0; x.FTuChi3 = 0; x.FTuChi4 = 0; x.FTuChi5 = 0; x.FMHHV1 = 0; x.FPhanCap1 = 0;
                    x.FMHHV2 = 0; x.FPhanCap2 = 0; x.FMHHV3 = 0; x.FPhanCap3 = 0; x.FMHHV4 = 0; x.FPhanCap4 = 0; x.FMHHV5 = 0; x.FPhanCap5 = 0;
                    x.HangNhap1 = 0; x.HangMua1 = 0; x.HangNhap2 = 0; x.HangMua2 = 0; x.HangNhap3 = 0; x.HangMua3 = 0; x.HangNhap4 = 0; x.HangMua4 = 0; x.HangNhap5 = 0; x.HangMua5 = 0;

                    return x;
                }).ToList();
            foreach (var item in data.Where(x => !x.IsHangCha &&
            (x.ChiTiet != 0 || x.UocThucHien != 0 || x.HangMua != 0 || x.HangNhap != 0 || x.PhanCap != 0 || x.ChuaPhanCap != 0
             || x.FTuChi1 != 0 || x.FTuChi2 != 0 || x.FTuChi3 != 0 || x.FTuChi4 != 0 || x.FTuChi5 != 0 || x.FMHHV1 != 0 || x.FPhanCap1 != 0 ||
            x.FMHHV2 != 0 || x.FPhanCap2 != 0 || x.FMHHV3 != 0 || x.FPhanCap3 != 0 || x.FMHHV4 != 0 || x.FPhanCap4 != 0 || x.FMHHV5 != 0 ||
            x.FPhanCap5 != 0 || x.HangNhap1 != 0 || x.HangMua1 != 0 || x.HangNhap2 != 0 || x.HangMua2 != 0 || x.HangNhap3 != 0 || x.HangMua3 != 0
            || x.HangNhap4 != 0 || x.HangMua4 != 0 || x.HangNhap5 != 0 || x.HangMua5 != 0)))
            {
                CalculateParent(ref data, item, item);
            }
        }

        private void CalculateParent(ref ObservableCollection<SktSoLieuChiTietMLNSModel> data, SktSoLieuChiTietMLNSModel currentItem, SktSoLieuChiTietMLNSModel selfItem)
        {
            var parentItem = data.FirstOrDefault(x => x.MlnsId == currentItem.MlnsIdParent);
            if (parentItem == null) return;
            parentItem.ChiTiet += selfItem.ChiTiet;
            parentItem.UocThucHien += selfItem.UocThucHien;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            parentItem.ChuaPhanCap += selfItem.ChuaPhanCap;
            parentItem.FTuChi1 += selfItem.FTuChi1;
            parentItem.FTuChi2 += selfItem.FTuChi2;
            parentItem.FTuChi3 += selfItem.FTuChi3;
            parentItem.FTuChi4 += selfItem.FTuChi4;
            parentItem.FTuChi5 += selfItem.FTuChi5;

            parentItem.FMHHV1 += selfItem.FMHHV1;
            parentItem.FPhanCap1 += selfItem.FPhanCap1;
            parentItem.HangNhap1 += selfItem.HangNhap1;
            parentItem.HangMua1 += selfItem.HangMua1;

            parentItem.FMHHV2 += selfItem.FMHHV2;
            parentItem.FPhanCap2 += selfItem.FPhanCap2;
            parentItem.HangNhap2 += selfItem.HangNhap2;
            parentItem.HangMua2 += selfItem.HangMua2;
            parentItem.FMHHV3 += selfItem.FMHHV3;
            parentItem.HangNhap3 += selfItem.HangNhap3;
            parentItem.HangMua3 += selfItem.HangMua3;
            parentItem.FPhanCap3 += selfItem.FPhanCap3;
            parentItem.FMHHV4 += selfItem.FMHHV4;
            parentItem.FPhanCap4 += selfItem.FPhanCap4;
            parentItem.HangNhap4 += selfItem.HangNhap4;
            parentItem.HangMua4 += selfItem.HangMua4;
            parentItem.FMHHV5 += selfItem.FMHHV5;
            parentItem.FPhanCap5 += selfItem.FPhanCap5;
            parentItem.HangNhap5 += selfItem.HangNhap5;
            parentItem.HangMua5 += selfItem.HangMua5;
            CalculateParent(ref data, parentItem, selfItem);
        }

    }
}
