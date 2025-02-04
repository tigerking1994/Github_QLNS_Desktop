using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewFeeCollectionManagement
{
    public class NewFeeCollectionManagementBhxhDetailViewModel : DetailViewModelBase<TlQuanLyThuNopBhxhModel, TlQuanLyThuNopBhxhNq104ChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly ITlBangLuongThangNq104Service _tlBangThuongService;
        private readonly IExportService _exportService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlDmCachTinhLuongChuanNq104Service _tlDmCachTinhLuongChuanService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;
        private readonly ITlQuanLyThuNopBhxhChiTietService _tlQuanLyThuNopBhxhChiTietService;
        private readonly ITlQuanLyThuNopBhxhService _tlQuanLyThuNopBhxhService;
        private SessionInfo _sessionInfo;
        private DataTable _data;

        //public override string FuncCode => NSFunctionCode.SALARY_MANAGEMENT_SALARY_TABLE_MONTH_DETAIL;
        public override string Title => "CHỨNG TỪ QUẢN LÝ THU NỘP BHXH CHI TIẾT";
        public override string Description => String.Format("Danh sách chứng từ thu nộp tháng {0} - năm {1} - đơn vị {2}", Model.IThang, Model.INam, Model.STenDonVi);
        public override Type ContentType => typeof(View.NewSalary.NewSettlement.NewFeeCollectionManagement.NewFeeCollectionManagementBhxhDetail);

        private DataTable _itemsBangLuong;
        public DataTable ItemsBangLuong
        {
            get => _itemsBangLuong;
            set => SetProperty(ref _itemsBangLuong, value);
        }

        private DataRowView _selectedBangLuong;
        public DataRowView SelectedBangLuong
        {
            get => _selectedBangLuong;
            set
            {
                SetProperty(ref _selectedBangLuong, value);
            }
        }

        private string _searchCanBo;
        public string SearchCanBo
        {
            get => _searchCanBo;
            set => SetProperty(ref _searchCanBo, value);
        }

        public RelayCommand ExportCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand ExportPhaiTruCommand { get; set; }

        public NewFeeCollectionManagementBhxhDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            IExportService exportService,
            ITlDmDonViNq104Service tlDmDonViService,
            IDanhMucService danhMucService,
            INsDonViService donViService,
            ITlDmCachTinhLuongChuanNq104Service tlDmCachTinhLuongChuanService,
            ITlQuanLyThuNopBhxhChiTietService tlQuanLyThuNopBhxhChiTietService,
            ITlQuanLyThuNopBhxhService tlQuanLyThuNopBhxhService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _donViService = donViService;
            _logger = logger;
            _tlBangThuongService = tlBangLuongThangService;
            _sessionInfo = _sessionService.Current;
            _exportService = exportService;
            _tlDmDonViService = tlDmDonViService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _tlQuanLyThuNopBhxhChiTietService = tlQuanLyThuNopBhxhChiTietService;
            _tlQuanLyThuNopBhxhService = tlQuanLyThuNopBhxhService;

            ExportCommand = new RelayCommand(obj => OnExport(), obj => SelectedBangLuong != null);
            ExportPhaiTruCommand = new RelayCommand(obj => OnExportPhaiTru(), obj => SelectedBangLuong != null);
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            SearchCommand = new RelayCommand(obj => OnSearch());
        }

        public override void Init()
        {
            base.Init();
            SearchCanBo = string.Empty;
            _sessionInfo = _sessionService.Current;
            LoadData();
        }

        public void LoadData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                e.Result = _tlQuanLyThuNopBhxhChiTietService.GetDataQlThuNopBhxhDetails(Model.Id);
            }, (s, e) =>
            {
                IsLoading = false;

                if (e.Error == null)
                {
                    _data = (DataTable)e.Result;
                    ItemsBangLuong = _data;
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }

        private void OnSearch()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                DataTable rs;
                if (!string.IsNullOrEmpty(SearchCanBo))
                {
                    Func<DataRow, bool> predicate = x => x.Field<string>("MaCanBo").Contains(SearchCanBo, StringComparison.OrdinalIgnoreCase)
                        || x.Field<string>("TenCanBo").Contains(SearchCanBo, StringComparison.OrdinalIgnoreCase);

                    var data = _data.AsEnumerable().Where(predicate);
                    if (data.Any())
                    {
                        rs = data.CopyToDataTable();
                    }
                    else
                    {
                        rs = _data.Clone();
                    }
                }
                else
                {
                    rs = _data.Copy();
                }

                e.Result = rs;
            }, (s, e) =>
            {
                IsLoading = false;

                if (e.Error == null)
                {
                    ItemsBangLuong = (DataTable)e.Result;
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }

        private void OnResetFilter()
        {
            ItemsBangLuong = _data;
        }

        private void OnExport()
        {
            if (SelectedBangLuong != null)
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    DataRow row = SelectedBangLuong.Row;
                    if (row.Table.Columns.Contains(ExportColumnHeader.MA_CAN_BO))
                    {
                        string maCanBo = row[ExportColumnHeader.MA_CAN_BO].ToString();
                        string tenCanBo = row.Table.Columns.Contains("TenCanBo") ? row.Field<string>("TenCanBo") : string.Empty;

                        var ctlTrichLuong = _tlDmCachTinhLuongChuanService.FindByMaCot(PhuCap.PCKHAC_SUM);
                        var lstPhuCap = ctlTrichLuong.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();

                        var items = _tlBangThuongService.ExportGiaiThichPhaiTru(maCanBo, string.Join(",", lstPhuCap));

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("DonViTinh", "Đơn vị tính: Đồng");
                        data.Add("TenCanBo", tenCanBo);
                        data.Add("Items", items);
                        data.Add("TieuDe", "GIẢI THÍCH CHI TIẾT CÁC PHỤ CẤP KHÁC");

                        data.Add("TenDonVi", Model.STenDonVi);

                        var tenDonViCha = _sessionService?.Current?.TenDonViReportHeader;
                        data.Add("DonViCha", tenDonViCha);

                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAI_THICH_PHUCAPKHAC_NEW);
                        string fileNamePrefix = string.Format("rptLuong_GiaiThich_PhuCapKhac_{0}", maCanBo);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export(templateFileName, data);
                        e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                    }
                }, (s, e) =>
                {
                    IsLoading = false;
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, ExportType.PDF);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
        }

        private void OnExportPhaiTru()
        {
            if (SelectedBangLuong != null)
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    DataRow row = SelectedBangLuong.Row;
                    if (row.Table.Columns.Contains(ExportColumnHeader.MA_CAN_BO))
                    {
                        string maCanBo = row[ExportColumnHeader.MA_CAN_BO].ToString();
                        string tenCanBo = row.Table.Columns.Contains("TenCanBo") ? row.Field<string>("TenCanBo") : string.Empty;

                        var ctlTrichLuong = _tlDmCachTinhLuongChuanService.FindByMaCot(PhuCap.TRICHLUONG_TT);
                        var lstPhuCap = ctlTrichLuong.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                        lstPhuCap.Add(PhuCap.GTKHAC_TT);

                        var items = _tlBangThuongService.ExportGiaiThichPhaiTru(maCanBo, string.Join(",", lstPhuCap));

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("DonViTinh", "Đơn vị tính: Đồng");
                        data.Add("TenCanBo", tenCanBo);
                        data.Add("Items", items);
                        data.Add("TenDonVi", Model.STenDonVi);
                        data.Add("TieuDe", "GIẢI THÍCH CHI TIẾT CÁC KHOẢN PHẢI TRỪ KHÁC");

                        var tenDonViCha = _sessionService?.Current?.TenDonViReportHeader;
                        data.Add("DonViCha", tenDonViCha);
                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAI_THICH_PHUCAPKHAC_NEW);
                        string fileNamePrefix = string.Format("rptLuong_GiaiThich_PhuCapPhaiTru_{0}", maCanBo);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export(templateFileName, data);
                        e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                    }
                }, (s, e) =>
                {
                    IsLoading = false;
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, ExportType.PDF);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }
        
        public override void Dispose()
        {
            base.Dispose();

            if (ItemsBangLuong != null)
                ItemsBangLuong.Clear();
        }
    }
}
