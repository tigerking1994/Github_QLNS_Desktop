using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Domain;
using MaterialDesignThemes.Wpf;

using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Service.Impl;
using System.IO;

using log4net;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand.ExportDemand
{
    public class ExportDemandViewModel : DialogViewModelBase<NsSktChungTuModel>
    {
        private readonly ILog _logger;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly INsPhongBanService _iNsPhongBanService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private readonly IExportService _exportService;
        private readonly ISktMucLucService _SktMucLucService;
        private readonly INsDonViService _donViService;
        private SessionInfo _sessionInfo;
        private ICauHinhMLNSService _cauHinhMLNSService;

        public override Type ContentType => typeof(View.Budget.DemandCheck.Demand.ExportDemand.ExportDemand);

        private ObservableCollection<ComboboxItem> _khoiItems = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> KhoiItems
        {
            get => _khoiItems;
            set => SetProperty(ref _khoiItems, value);
        }

        private ComboboxItem _khoiSelected;

        private bool _khoiEnable;

        public bool KhoiEnable => String.IsNullOrEmpty(Model.SDssoChungTuTongHop);

        public ComboboxItem KhoiSelected
        {
            get => _khoiSelected;
            set
            {
                SetProperty(ref _khoiSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _bQuanLyItems = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BQuanLyItems
        {
            get => _bQuanLyItems;
            set => SetProperty(ref _bQuanLyItems, value);
        }

        private ComboboxItem _bQuanLySelected;

        public ComboboxItem BQuanLySelected
        {
            get => _bQuanLySelected;
            set
            {
                SetProperty(ref _bQuanLySelected, value);
            }
        }

        public List<NsSktChungTuModel> ListChildrenModel { get; set; }

        public RelayCommand ExportCommand { get; }

        public ExportDemandViewModel(INsDonViService nsDonViService,
                ISktChungTuService sktChungTuService,
                ISktChungTuChiTietService sktChungTuChiTietService,
                IMapper mapper,
                IExportService exportService,
                ILog logger,
                ISktMucLucService sktMucLucService,
                INsPhongBanService iNsPhongBanService,
                INsDonViService donViService,
                ISessionService sessionService,
                ICauHinhMLNSService cauHinhMLNSService,
                ISysAuditLogService log)
        {
            _sessionService = sessionService;
            _SktMucLucService = sktMucLucService;
            _sktChungTuService = sktChungTuService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _log = log;
            _donViService = donViService;
            _iNsPhongBanService = iNsPhongBanService; 
            _mapper = mapper;
            _logger = logger;
            _cauHinhMLNSService = cauHinhMLNSService;

            ExportCommand = new RelayCommand(obj => OnExportData());


        }


        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadKhois();
            LoadBQuanLys();
        }

        public void LoadBQuanLys()
        {
            _bQuanLyItems = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DmBQuanLy>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DmBQuanLy> data = _iNsPhongBanService.FindByCondition(predicate).ToList();
            _bQuanLyItems = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            _bQuanLyItems.Insert(0, new ComboboxItem { DisplayItem = "Tất cả", ValueItem = "0" });
            _bQuanLySelected = _bQuanLyItems.FirstOrDefault();
        }

        public void LoadKhois()
        {
            var khoiItems = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Tất cả", ValueItem = TypeKhoi.TAT_CA.ToString()},
                new ComboboxItem {DisplayItem = "Doanh nghiệp", ValueItem = TypeKhoi.DOANH_NGHIEP.ToString()},
                new ComboboxItem {DisplayItem = "Dự toán", ValueItem = TypeKhoi.DU_TOAN.ToString()},
                new ComboboxItem {DisplayItem = "Bệnh viện tự chủ", ValueItem = TypeKhoi.BENH_VIEN.ToString()},
            };
            _khoiItems = new ObservableCollection<ComboboxItem>(khoiItems);
            _khoiSelected = _khoiItems.ElementAt(0);
        }

        private List<NsSktChungTuChiTietModel> GetDemandVoucherDetail(NsSktChungTuModel nsSktChungTuModel, string bQuanLy, string iKhoi)
        {
            var loaiChungTu = nsSktChungTuModel.ILoaiChungTu.GetValueOrDefault(-1);
            SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
            searchCondition.NguonNganSach = _sessionInfo.Budget;
            searchCondition.ITrangThai = StatusType.ACTIVE;
            searchCondition.SktChungTuId = nsSktChungTuModel.Id;
            searchCondition.ILoai = nsSktChungTuModel.ILoai;
            searchCondition.IdDonVi = nsSktChungTuModel.IIdMaDonVi;
            searchCondition.CurrentIdDonVi = _sessionInfo.IdDonVi;
            searchCondition.LoaiChungTu = loaiChungTu;
            searchCondition.UserName = _sessionInfo.Principal;
            var temp = _sktChungTuChiTietService.FindByConditionForChildUnit(searchCondition);
            var lstChungTuChiTietModels = _mapper.Map<List<NsSktChungTuChiTietModel>>(temp);
            //var lstChungTuChiTietModelsParent = lstChungTuChiTietModels.Where(n => n.IsHangCha);
            if (bQuanLy.Equals("0") && iKhoi.Equals("0"))
            {
                CalculateData(lstChungTuChiTietModels);
                lstChungTuChiTietModels = lstChungTuChiTietModels.Where(item => item.FHuyDongTonKho > 0 || item.FTuChi > 0 || item.FMuaHangCapHienVat > 0 || item.FPhanCap > 0 || item.FTonKhoDenNgay > 0).ToList();
                return lstChungTuChiTietModels;
            }

            var tempFilter = _sktChungTuChiTietService.FindSoKyHieus(_sessionService.Current.YearOfWork, bQuanLy);
            List<SoKyHieuMucLucNganSachModel> soKyHieus = _mapper.Map<List<SoKyHieuMucLucNganSachModel>>(tempFilter);
            var tatCaSoKyHieus = _SktMucLucService.FindByCondition(x => x.INamLamViec == _sessionService.Current.YearOfWork).Select(n => new SoKyHieuMucLucNganSachModel
            {
                sSKT_KyHieu = n.SKyHieu
            });

            var soKyHieuFilters = tatCaSoKyHieus.Where(n => soKyHieus.Any(m => m.sSKT_KyHieu.StartsWith(n.sSKT_KyHieu))).OrderBy(n => n.sSKT_KyHieu).ToList();

            var tempDonVi = _donViService.FindAll().Where(n => n.NamLamViec == _sessionService.Current.YearOfWork && (iKhoi.Equals("0") || !String.IsNullOrEmpty(n.Khoi) && n.Khoi.Equals(iKhoi)));
            List<DonVi> donVis = _mapper.Map<List<DonVi>>(tempDonVi);


            var nsSktChungTuChiTietQuery = _mapper.Map < List < NsSktChungTuChiTietModel >> (
                                            from lstChungTuChiTietModel in lstChungTuChiTietModels
                                            join soKyHieuFilter in soKyHieuFilters on lstChungTuChiTietModel.SKyHieu equals soKyHieuFilter.sSKT_KyHieu
                                            join donVi in donVis on lstChungTuChiTietModel.IIdMaDonVi equals donVi.IIDMaDonVi
                                            select lstChungTuChiTietModel);
            //nsSktChungTuChiTietQuery = nsSktChungTuChiTietQuery.Union(lstChungTuChiTietModelsParent).OrderBy(n => n.SKyHieu).ToList();
            CalculateData(nsSktChungTuChiTietQuery);
            nsSktChungTuChiTietQuery = nsSktChungTuChiTietQuery.Where(item => item.FHuyDongTonKho > 0 || item.FTuChi > 0 || item.FMuaHangCapHienVat > 0 || item.FPhanCap > 0 || item.FTonKhoDenNgay > 0).ToList();
            return nsSktChungTuChiTietQuery;
        }

        private void CalculateData(List<NsSktChungTuChiTietModel> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHuyDongTonKho = 0;
                    x.FMuaHangCapHienVat = 0;
                    x.FPhanCap = 0;
                    x.FTonKhoDenNgay = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.IsHangCha && !x.IsDeleted);
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstSktChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid idParent, NsSktChungTuChiTietModel item, List<NsSktChungTuChiTietModel> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.FTuChi += item.FTuChi;
            model.FHuyDongTonKho += item.FHuyDongTonKho;
            model.FMuaHangCapHienVat += item.FMuaHangCapHienVat;
            model.FPhanCap += item.FPhanCap;
            model.FTonKhoDenNgay += item.FTonKhoDenNgay;
            CalculateParent(model.IdParent, item, lstSktChungTuChiTiet);
        }
        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        private void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    var tempDonVi = _donViService.FindAll().Where(n => n.NamLamViec == _sessionService.Current.YearOfWork);
                    List<DonVi> donVis = _mapper.Map<List<DonVi>>(tempDonVi);

                    var yearOfWork = _sessionInfo.YearOfWork;
                    var currentDonVi = GetNsDonViOfCurrentUser();
                    List<NsSktChungTuChiTietModel> sktChungTuChiTietModels = new List<NsSktChungTuChiTietModel>();
                    if (!string.IsNullOrEmpty(Model.SDssoChungTuTongHop) && donVis.Any(n => string.IsNullOrEmpty(n.Khoi) && n.IIDMaDonVi == Model.IIdMaDonVi) && ListChildrenModel != null)
                    {   
                        foreach(var childModel in ListChildrenModel)
                        {
                            var sktChungTuChiTietChildModel = GetDemandVoucherDetail(childModel, BQuanLySelected.ValueItem, KhoiSelected.ValueItem);
                            sktChungTuChiTietModels = sktChungTuChiTietModels.Union(sktChungTuChiTietChildModel).ToList();
                        }
                    } else
                    {
                        sktChungTuChiTietModels = GetDemandVoucherDetail(Model, BQuanLySelected.ValueItem, KhoiSelected.ValueItem);
                    }

                    var sktChungTuChiTietModelsQuery =
                        from sktChungTuChiTietModel in sktChungTuChiTietModels
                        group sktChungTuChiTietModel by new
                        {
                            sktChungTuChiTietModel.SKyHieu,
                            sktChungTuChiTietModel.INamLamViec,
                            sktChungTuChiTietModel.INamNganSach,
                            sktChungTuChiTietModel.SMoTa,
                            sktChungTuChiTietModel.IIdMlskt,
                            sktChungTuChiTietModel.IdParent,
                            sktChungTuChiTietModel.IsHangCha
                        } into newGroup
                        orderby newGroup.Key.SKyHieu
                        select new NsSktChungTuChiTietModel
                        {
                            FHienVat = newGroup.Sum(x => x.FHienVat),
                            FHuyDongTonKho = newGroup.Sum(x => x.FHuyDongTonKho),
                            FMuaHangCapHienVat = newGroup.Sum(x => x.FMuaHangCapHienVat),
                            TongHuyDongTuChi = newGroup.Sum(x => x.TongHuyDongTuChi),
                            TongMuaHangHienVatDacThu = newGroup.Sum(x => x.TongMuaHangHienVatDacThu),
                            FTonKhoDenNgay = newGroup.Sum(x => x.FTonKhoDenNgay),
                            FPhanCap = newGroup.Sum(x => x.FPhanCap),
                            FTuChi = newGroup.Sum(x => x.FTuChi),
                            SKyHieu = newGroup.Key.SKyHieu,
                            INamLamViec = newGroup.Key.INamLamViec,
                            INamNganSach = newGroup.Key.INamNganSach,
                            SMoTa = newGroup.Key.SMoTa,
                            IIdMlskt = newGroup.Key.IIdMlskt,
                            IdParent = newGroup.Key.IdParent,
                            IsHangCha = newGroup.Key.IsHangCha
                        };

                    sktChungTuChiTietModels = sktChungTuChiTietModelsQuery.ToList();

                    var predicate = PredicateBuilder.True<NsSktMucLuc>();
                    predicate = predicate.And(x => x.INamLamViec == yearOfWork);
                    predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
                    List<NsSktMucLuc> sktMucLucs = _SktMucLucService.FindByCondition(predicate).ToList();
                    var sktMucLucsOrder = from sktMucLuc in sktMucLucs
                                            orderby sktMucLuc.SKyHieu
                                            select sktMucLuc;
                    foreach (var ct in sktChungTuChiTietModels)
                    {
                        var ml = sktMucLucsOrder.FirstOrDefault(x => x.IIDMLSKT.Equals(ct.IIdMlskt));
                        if (ml != null)
                        {
                            ct.Nganh = ml.SNg;
                            ct.NganhParent = ml.SNGCha;
                            ct.Stt = ml.SSTT;
                        }
                    }

                    //NSSD
                    double SumTotalHuyDong = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FHuyDongTonKho);
                    double SumTotalTuChi = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTuChi);
                    double SumTotalTongCongNSSD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongHuyDongTuChi);
                    //NSBD
                    double SumTotalMuaHangHienVat = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FMuaHangCapHienVat);
                    double SumTotalDacThu = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FPhanCap);
                    double SumTotalTongCongNSBD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongMuaHangHienVatDacThu);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("SoChungTu", Model.SSoChungTu);
                    data.Add("TenDonVi", Model.STenDonVi);
                    data.Add("IdDonVi", Model.IIdMaDonVi);
                    data.Add("Cap1", currentDonVi.TenDonVi);
                    data.Add("TieuDe1", "BÁO CÁO CHI TIẾT SỐ NHU CẦU NGÂN SÁCH NĂM " + _sessionInfo.YearOfWork);
                    data.Add("h2", "Lữ đoàn X");
                    data.Add("h1", "Lữ đoàn X");
                    data.Add("LoaiChungTu", Model.ILoaiChungTu == 1 ? VoucherType.NSSD_Value : VoucherType.NSBD_Value);
                    data.Add("MoTa", Model.SMoTa);
                    data.Add("NgayChungTu", Model.DNgayChungTu.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy"));
                    data.Add("NguoiTao", Model.SNguoiTao);
                    data.Add("NgayTao", Model.DNgayTao.GetValueOrDefault(new DateTime()).ToString("dd/MM/yyyy"));
                    data.Add("SumTotalHuyDong", SumTotalHuyDong);
                    data.Add("SumTotalTuChi", SumTotalTuChi);
                    data.Add("SumTotalMHHV", SumTotalMuaHangHienVat);
                    data.Add("SumTotalDT", SumTotalDacThu);
                    data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                    data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                    data.Add("SumTotalTonKho", sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTonKhoDenNgay));
                    data.Add("ListData", sktChungTuChiTietModels);
                    data.Add("Count", 10000);
                    data.Add("SKTML", sktMucLucsOrder);
                    data.Add("TonKhoDenNgay", "Tồn kho đến ngày 1/1/" + (_sessionInfo.YearOfWork - 1));
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSSD);
                    /*if (item.ILoaiChungTu == 1)
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSSD);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSBD);
                    }*/
                    fileNamePrefix = Model.SSoChungTu;
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<NsSktChungTuChiTietModel, NsSktMucLuc>(templateFileName, data);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
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
    }
}
