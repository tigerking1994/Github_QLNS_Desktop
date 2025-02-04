using AutoMapper;
using FlexCel.Core;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewSalaryYearPlan
{
    public class NewSalaryYearPlanVolatilityViewModel : DialogViewModelBase<TlQtChungTuChiTietKeHoachNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private ITlDmCanBoNq104Service _tlDmCanBoService;
        private ITlDmCanBoKeHoachNq104Service _tlDmCanBoKeHoachService;
        private ITlBangLuongKeHoachNq104Service _tlBangLuongKeHoachService;
        private ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private ITlQtChungTuChiTietKeHoachNq104Service _tlQtChungTuChiTietKeHoachService;
        private ITlPhuCapMlnsNq104Service _tlPhuCapMlnsService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private IExportService _exportService;
        private List<TlDmCanBoNq104> _lstDmCanBos;
        private List<TlBangLuongThangNq104> _lstBangLuongThang;
        private List<TlBangLuongKeHoachNq104> _lstBangLuongKeHoachs;
        private List<TlDmCanBoKeHoachNq104> _lstCanBoKeHoaches;
        private List<TlQtChungTuChiTietKeHoachNq104> _lstChungTuChiTietKeHoach;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;
        private List<string> _lstNgach;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_QUAN_LY_LUONG_KE_HOACH_BANG_LUONG_NAM_KH_DETAIL;

        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagementPlan.NewSalaryYearPlan.NewSalaryYearPlanVolatility);
        public override PackIconKind IconKind => PackIconKind.AccountDetails;

        public override string Name => "Biến động " + Model.MoTa + " " + Model.TenDonVi + " năm " + Model.NamLamViec;
        public override string Title => "Biến động " + Model.MoTa;
        public override string Description => "Biến động " + Model.MoTa + " " + Model.TenDonVi + " năm " + Model.NamLamViec;

        private ObservableCollection<TlBienDongLuongThangModel> _itemsBienDong;
        public ObservableCollection<TlBienDongLuongThangModel> ItemsBienDong
        {
            get => _itemsBienDong;
            set => _itemsBienDong = value;
        }

        private TlBienDongLuongThangModel _selectedItemBienDong;
        public TlBienDongLuongThangModel SelectedItemBienDong
        {
            get => _selectedItemBienDong;
            set => SetProperty(ref _selectedItemBienDong, value);
        }

        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPdfCommand { get; }

        public NewSalaryYearPlanVolatilityViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmCanBoNq104Service tlDmCanBoService,
            ITlDmCanBoKeHoachNq104Service tlDmCanBoKeHoachService,
            ITlBangLuongKeHoachNq104Service tlBangLuongKeHoachService,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            ITlQtChungTuChiTietKeHoachNq104Service tlQtChungTuChiTietKeHoachService,
            ITlPhuCapMlnsNq104Service tlPhuCapMlnsService,
            IExportService exportService,
            IDanhMucService danhMucService,
            INsDonViService donViService,
            ITlDmDonViNq104Service tlDmDonViService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _tlBangLuongKeHoachService = tlBangLuongKeHoachService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlDmCanBoKeHoachService = tlDmCanBoKeHoachService;
            _tlDmCanBoService = tlDmCanBoService;
            _tlQtChungTuChiTietKeHoachService = tlQtChungTuChiTietKeHoachService;
            _exportService = exportService;
            _tlPhuCapMlnsService = tlPhuCapMlnsService;
            _tlDmDonViService = tlDmDonViService;
            _danhMucService = danhMucService;
            _donViService = donViService;

            ExportExcelCommand = new RelayCommand(obj => ExportChiTiet(ExportType.EXCEL));
            ExportPdfCommand = new RelayCommand(obj => ExportChiTiet(ExportType.PDF));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _lstDmCanBos = _tlDmCanBoService.FindAll().ToList();
            _lstBangLuongThang = _tlBangLuongThangService.FindAll().ToList();
            _lstBangLuongKeHoachs = _tlBangLuongKeHoachService.FindAll().ToList();
            _lstCanBoKeHoaches = _tlDmCanBoKeHoachService.FindAll().ToList();
            _lstChungTuChiTietKeHoach = _tlQtChungTuChiTietKeHoachService.FindAll().ToList();
            LoadData();
        }

        private void LoadData()
        {
            ItemsBienDong = new ObservableCollection<TlBienDongLuongThangModel>();
            for (int i = 1; i <= 12; i++)
            {
                TlBienDongLuongThangModel tlBienDongLuongThangModel = new TlBienDongLuongThangModel();
                tlBienDongLuongThangModel.Thang = i;
                if (_lstChungTuChiTietKeHoach != null && _lstChungTuChiTietKeHoach.Count > 0)
                {
                    var qtChungTuChiTiet = _lstChungTuChiTietKeHoach.FirstOrDefault(x => x.Thang == i && x.NamLamViec == Model.NamLamViec && x.XauNoiMa == Model.XauNoiMa);
                    if (qtChungTuChiTiet != null)
                    {
                        tlBienDongLuongThangModel.NamTruoc = qtChungTuChiTiet.TongNamTruoc ?? 0;
                        tlBienDongLuongThangModel.NamKeHoach = qtChungTuChiTiet.TongCong ?? 0;
                        tlBienDongLuongThangModel.ChenhLech = tlBienDongLuongThangModel.NamKeHoach - tlBienDongLuongThangModel.NamTruoc;
                        ItemsBienDong.Add(tlBienDongLuongThangModel);
                    }
                }
            }
            var lstPhuCapMlns = _tlPhuCapMlnsService.FindByCondition(x => Model.XauNoiMa.Equals(x.XauNoiMa) && (Model.NamLamViec - 1 == x.Nam));
            _lstNgach = lstPhuCapMlns.Select(x => x.MaCb).Distinct().ToList();
        }

        private void ExportChiTiet(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    string title = string.Format("{0} - Năm {1}", Model.TenDonVi, Model.NamLamViec);
                    Dictionary<string, object> data = new Dictionary<string, object>();

                    FormatNumber formatNumber = new FormatNumber(1, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("NgayThangNam", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.Day.ToString("D2"), DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("TieuDe1", Model.MoTa);
                    data.Add("TieuDe", string.Format("Năm {0}", Model.NamLamViec));
                    data.Add("TenDonVi", string.Format("{0}", Model.TenDonVi));

                    var donVi = _tlDmDonViService.FindByMaDonVi(Model.IdDonVi);

                    if (!string.IsNullOrEmpty(donVi.ParentId))
                    {
                        var donViCha = _tlDmDonViService.FindAll().FirstOrDefault(x => x.MaDonVi.Equals(donVi.ParentId));
                        if (donViCha != null)
                        {
                            data.Add("DonViCha", donViCha.TenDonVi.ToUpper());
                        }
                        else
                        {
                            data.Add("DonViCha", string.Empty);
                        }
                    }
                    else
                    {
                        data.Add("DonViCha", string.Empty);
                    }

                    data.Add("Items", ItemsBienDong);

                    var items2 = GetDataChiTiet(1);
                    var items3 = GetDataChiTiet(2);
                    var items4 = GetDataChiTiet(3);
                    var items5 = GetDataChiTiet(4);
                    var items6 = GetDataChiTiet(5);
                    var items7 = GetDataChiTiet(6);
                    var items8 = GetDataChiTiet(7);
                    var items9 = GetDataChiTiet(8);
                    var items10 = GetDataChiTiet(9);
                    var items11 = GetDataChiTiet(10);
                    var items12 = GetDataChiTiet(11);
                    var items13 = GetDataChiTiet(12);

                    data.Add("Items2", items2);
                    data.Add("Items3", items3);
                    data.Add("Items4", items4);
                    data.Add("Items5", items5);
                    data.Add("Items6", items6);
                    data.Add("Items7", items7);
                    data.Add("Items8", items8);
                    data.Add("Items9", items9);
                    data.Add("Items10", items10);
                    data.Add("Items11", items11);
                    data.Add("Items12", items12);
                    data.Add("Items13", items13);
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_BIEN_DONG_LUONG_NEW);
                    string fileNamePrefix = string.Format("rpt_Bien_Dong_Luong_Ke_Hoach_Chi_Tiet_Nam_{0}_{1}", Model.NamLamViec, Model.TenDonVi);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<TlBienDongLuongThangModel, TlBienDongLuongThangChiTietModel>(templateFileName, data);
                    e.Result = new ExportResult(title, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
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

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        private List<TlBienDongLuongThangChiTietModel> GetDataChiTiet(int thang)
        {
            var lstData = new List<TlBienDongLuongThangChiTietModel>();
            int stt = 0;
            var lstCanBo = new List<TlDmCanBoKeHoachNq104>();

            foreach (var item in _lstNgach)
            {
                if ("1".Equals(item) || "2".Equals(item))
                {
                    var lstAdd = _lstCanBoKeHoaches.Where(x => x.Thang == thang && x.Parent == Model.IdDonVi && x.Nam == Model.NamLamViec && x.MaCb.StartsWith(item)).ToList();
                    lstCanBo.AddRange(lstAdd);
                }
                else if ("3".Equals(item))
                {
                    var lstAdd = _lstCanBoKeHoaches.Where(x => x.Thang == thang && x.Parent == Model.IdDonVi && x.Nam == Model.NamLamViec && x.MaCb.StartsWith("4")).ToList();
                    lstCanBo.AddRange(lstAdd);
                }
                else if ("4".Equals(item))
                {
                    var lstAdd = _lstCanBoKeHoaches.Where(x => x.Thang == thang && x.Parent == Model.IdDonVi && x.Nam == Model.NamLamViec && x.MaCb.StartsWith("0")).ToList();
                    lstCanBo.AddRange(lstAdd);
                }

                if (lstCanBo == null || lstCanBo.Count() == 0)
                {
                    return lstData;
                }
            }

            foreach (var item in lstCanBo)
            {
                TlBienDongLuongThangChiTietModel tlBienDongLuongThangChiTietModel = new TlBienDongLuongThangChiTietModel();
                tlBienDongLuongThangChiTietModel.HoTen = item.TenCanBo;
                var luongNamTruoc = _lstBangLuongThang.FirstOrDefault(x => x.MaHieuCanBo == item.MaHieuCanBo && x.Thang == thang
                && x.Nam == (item.Nam - 2) && x.MaPhuCap == Model.MaPhuCap);
                if (luongNamTruoc != null)
                {
                    tlBienDongLuongThangChiTietModel.NamTruoc = luongNamTruoc.GiaTri;
                }
                else
                {
                    tlBienDongLuongThangChiTietModel.NamTruoc = 0;
                }
                var luongNamNay = _lstBangLuongKeHoachs.FirstOrDefault(x => x.MaHieuCanBo == item.MaHieuCanBo && x.Thang == thang
                && x.Nam == item.Nam && x.MaPhuCap == Model.MaPhuCap);
                if (luongNamNay != null)
                {
                    tlBienDongLuongThangChiTietModel.NamHienTai = luongNamNay.GiaTri;
                }
                else
                {
                    tlBienDongLuongThangChiTietModel.NamHienTai = 0;
                }
                tlBienDongLuongThangChiTietModel.ChenhLech = tlBienDongLuongThangChiTietModel.NamHienTai - tlBienDongLuongThangChiTietModel.NamTruoc;
                lstData.Add(tlBienDongLuongThangChiTietModel);
            }

            lstData = lstData.OrderBy(x => x.HoTen).ToList();
            foreach (var item in lstData)
            {
                item.Stt = ++stt;
            }

            return lstData.OrderBy(x => x.Stt).ToList();
        }
    }
}
