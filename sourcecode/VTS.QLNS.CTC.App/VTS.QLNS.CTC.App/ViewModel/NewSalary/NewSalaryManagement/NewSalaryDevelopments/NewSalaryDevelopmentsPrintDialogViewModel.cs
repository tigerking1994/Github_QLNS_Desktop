using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewSalaryDevelopments
{
    public class NewSalaryDevelopmentsPrintDialogViewModel : DialogViewModelBase<TlRptDienBienLuongNq104Query>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;

        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagement.NewSalaryDevelopments.NewSalaryDevelopmentsPrintDialog);
        public override PackIconKind IconKind => PackIconKind.Printer;
        public override string Title => "In báo cáo diễn biến lương cán bộ " + Model.TenCanBo;
        public override string Description => "In báo cáo diễn biến lương cán bộ " + Model.TenCanBo;
        private SessionInfo _sessionInfo;

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _fromMonthSelected;
        public ComboboxItem FromMonthSelected
        {
            get => _fromMonthSelected;
            set => SetProperty(ref _fromMonthSelected, value);
        }

        private ComboboxItem _toMonthSelected;
        public ComboboxItem ToMonthSelected
        {
            get => _toMonthSelected;
            set => SetProperty(ref _toMonthSelected, value);
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _fromYearSelected;
        public ComboboxItem FromYearSelected
        {
            get => _fromYearSelected;
            set => SetProperty(ref _fromYearSelected, value);
        }

        private ComboboxItem _toYearSelected;
        public ComboboxItem ToYearSelected
        {
            get => _toYearSelected;
            set => SetProperty(ref _toYearSelected, value);
        }

        private List<ComboboxItem> _itemsFileExport;
        public List<ComboboxItem> ItemsFileExport
        {
            get => _itemsFileExport;
            set => SetProperty(ref _itemsFileExport, value);
        }

        private ComboboxItem _selectedFileExport;
        public ComboboxItem SelectedFileExport
        {
            get => _selectedFileExport;
            set => SetProperty(ref _selectedFileExport, value);
        }

        public RelayCommand PrintCommand { get; }

        public NewSalaryDevelopmentsPrintDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            IExportService exportService,
            IDanhMucService danhMucService,
            INsDonViService donViService,
            ITlDmDonViNq104Service tlDmDonViService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _donViService = donViService;
            _logger = logger;

            _tlBangLuongThangService = tlBangLuongThangService;
            _exportService = exportService;
            _tlDmDonViService = tlDmDonViService;

            PrintCommand = new RelayCommand(o => OnPrint());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadMonths();
            LoadYear();
            LoadLoaiFileOutPut();
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            OnPropertyChanged(nameof(Months));
        }

        private void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = _sessionService.Current.YearOfWork - 5; i <= _sessionService.Current.YearOfWork + 5; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            OnPropertyChanged(nameof(Years));
        }

        private void LoadLoaiFileOutPut()
        {
            ItemsFileExport = new List<ComboboxItem>();
            _itemsFileExport.Add(new ComboboxItem("Excel", "Excel"));
            _itemsFileExport.Add(new ComboboxItem("PDF", "PDF"));
            SelectedFileExport = ItemsFileExport.Where(x => x.ValueItem == "PDF").FirstOrDefault();
        }

        private void OnPrint()
        {
            ExportType exportType = SelectedFileExport != null && "PDF".Equals(SelectedFileExport.ValueItem) ? ExportType.PDF : ExportType.EXCEL;
            var tuNgay = new DateTime(int.Parse(FromYearSelected.ValueItem), int.Parse(FromMonthSelected.ValueItem), 1);
            var denNgay = new DateTime(int.Parse(ToYearSelected.ValueItem), int.Parse(ToMonthSelected.ValueItem), 1);
            DataTable item = _tlBangLuongThangService.ReportDienBienLuong(Model.MaHieuCanBo, tuNgay, denNgay);
            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(1, exportType);
            data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
            data.Add("Cap2", GetHeader2Report());
            int donViTinh = 1;
            data.Add("FormatNumber", formatNumber);
            data.Add("Unit", donViTinh);

            string tu = string.Format("{0}/{1}", tuNgay.Month.ToString("D2"), tuNgay.Year);
            string den = string.Format("{0}/{1}", denNgay.Month.ToString("D2"), denNgay.Year);
            data.Add("TieuDe2", string.Format("Thời gian: {0} - {1}", tu, den));
            data.Add("TenDonVi", Model.TenDonVi.ToUpper());

            var donVi = _tlDmDonViService.FindByMaDonVi(Model.MaDonVi);
            if (donVi != null && !string.IsNullOrEmpty(donVi.ParentId))
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

            data.Add("CanBo", Model.TenCanBo);
            data.Add("Items", item);
            data.Add("ReportName", "");

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_DIENBIEN_LUONG_CANBO_NEW);
            var xlsFile = _exportService.Export<TlDmCanBoKeHoachModel>(templateFileName, data);
            string fileNamePrefix = string.Format("rpt_DienBien_Luong_CanBo_{0}", Model.TenCanBo);
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);

            ExportResult rs = new ExportResult(string.Format("{0} - {1}", Model.MaDonVi, Model.TenDonVi), fileNameWithoutExtension, null, xlsFile);
            _exportService.Open(rs, exportType);
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        protected override void OnModelPropertyChanged()
        {
            base.OnModelPropertyChanged();
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
        }
    }
}
