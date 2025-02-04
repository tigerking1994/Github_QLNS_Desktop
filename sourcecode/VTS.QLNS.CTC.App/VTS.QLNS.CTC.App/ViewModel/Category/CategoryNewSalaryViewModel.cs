using AutoMapper;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewCalculateSalary;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewCategoryHoliday;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.CalculateSalary;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.CategoryHoliday;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class CategoryNewSalaryViewModel : ViewModelBase
    {
        private IMapper _mapper;
        private readonly IServiceProvider _provider;
        private ISessionService _sessionService;
        private TLDanhMucChucVuNq104Service _tlDanhMucChucVuService;
        private TlDmTietTieuMucNganhService _tlDmTietTieuMucNganhService;
        private TlDmThueThuNhapCaNhanNq104Service _tlDmThueThuNhapCaNhanService;
        private TlDmTangGiamService _tlDmTangGiamService;
        private TlDmPhuCapNq104Service _tlDmPhuCapService;
        private TlDmCapBacNq104Service _tlDmCapBacService;
        private TlDmDonViNq104Service _tlDmDonViService;
        private TlDmPhuCapHeThongNq104Service _tlDmPhuCapHeThongNq104Service;
        private ITlDmKinhPhiService _tlDmKinhPhiService;
        private INsQsMucLucService _qSMucLucService;
        private TlDmCheDoBHXHService _tlDmCheDoBHXHService;

        public override Type ContentType => typeof(CategoryGeneral);
        public override string Name => "DANH MỤC LƯƠNG";
        public override string Description => "Danh mục dùng chung";
        public override string FuncCode => NSFunctionCode.CATEGORY_SALARY;

        public CalculateSalaryIndexViewModel CalculateSalaryIndexViewModel { get; }
        public CategoryHolidayIndexViewModel CategoryHolidayIndexViewModel { get; }

        public NewCalculateSalaryIndexViewModel NewCalculateSalaryIndexViewModel { get; }
        public NewCategoryHolidayIndexViewModel NewCategoryHolidayIndexViewModel { get; }
        public MucLucCapBacLuongViewModel MucLucCapBacLuongViewModel { get; set; }


        public CategoryNewSalaryViewModel(IMapper mapper,
            IServiceProvider provider,
            ISessionService sessionService,
            TLDanhMucChucVuNq104Service tLDanhMucChucVuService,
            TlDmTietTieuMucNganhService tlDmTietTieuMucNganhService,
            TlDmThueThuNhapCaNhanNq104Service tlDmThueThuNhapCaNhanService,
            TlDmTangGiamService tlDmTangGiamService,
            TlDmPhuCapNq104Service tlDmPhuCapService,
            TlDmCapBacNq104Service tlDmCapBacService,
            TlDmDonViNq104Service tlDmDonviService,
            TlDmPhuCapHeThongService tlDmPhuCapHeThongService,
            TlDmPhuCapHeThongNq104Service tlDmPhuCapHeThongNq104Service,
            ITlDmKinhPhiService tlDmKinhPhiService,
            INsQsMucLucService qsMucLucService,
            CalculateSalaryIndexViewModel calculateSalaryIndexViewModel,
            NewCalculateSalaryIndexViewModel newCalculateSalaryIndexViewModel,
            TlDmCheDoBHXHService tlDmCheDoBHXHService,
            CategoryHolidayIndexViewModel categoryHolidayIndexViewModel,
            NewCategoryHolidayIndexViewModel newCategoryHolidayIndexViewModel,
            MucLucCapBacLuongViewModel mucLucCapBacLuongViewModel)
        {
            _mapper = mapper;
            _provider = provider;
            _sessionService = sessionService;
            _tlDanhMucChucVuService = tLDanhMucChucVuService;
            _tlDmTietTieuMucNganhService = tlDmTietTieuMucNganhService;
            _tlDmThueThuNhapCaNhanService = tlDmThueThuNhapCaNhanService;
            _tlDmTangGiamService = tlDmTangGiamService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDmDonViService = tlDmDonviService;
            _tlDmPhuCapHeThongNq104Service = tlDmPhuCapHeThongNq104Service;
            _tlDmKinhPhiService = tlDmKinhPhiService;
            _qSMucLucService = qsMucLucService;
            _tlDmCheDoBHXHService = tlDmCheDoBHXHService;

            MucLucCapBacLuongViewModel = mucLucCapBacLuongViewModel;
            CalculateSalaryIndexViewModel = calculateSalaryIndexViewModel;
            CategoryHolidayIndexViewModel = categoryHolidayIndexViewModel;
            NewCalculateSalaryIndexViewModel = newCalculateSalaryIndexViewModel;
            NewCategoryHolidayIndexViewModel = newCategoryHolidayIndexViewModel;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
                new GenericControlCustomViewModel<TlDmPhuCapHeThongNq104Model, Core.Domain.TlDmPhuCapNq104, TlDmPhuCapHeThongNq104Service>(_tlDmPhuCapHeThongNq104Service, _mapper, _sessionService, _provider)
                {
                    Name = "Cấu hình tham số hệ thống",
                    Title = "Cấu hình tham số hệ thống",
                    Description = "Danh sách cấu hình tham số hệ thống",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_NEW_SALARY_SYSTEM_CFG
                },
                new GenericControlCustomViewModel<TlDanhMucChucVuNq104Model, Core.Domain.TlDmChucVuNq104, TLDanhMucChucVuNq104Service>(_tlDanhMucChucVuService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục chức vụ",
                    Title = "Danh mục chức vụ",
                    Description = "Danh sách chức vụ",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_NEW_SALARY_CHUC_VU,
                    IsLoaiChucVu = false
                },
                new GenericControlCustomViewModel<TlDanhMucChucDanhNq104Model, Core.Domain.TlDmChucVuNq104, TLDanhMucChucVuNq104Service>(_tlDanhMucChucVuService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục chức danh",
                    Title = "Danh mục chức danh",
                    Description = "Danh sách chức danh",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_NEW_SALARY_CHUC_DANH,
                    IsLoaiChucVu = true
                },
                new GenericControlCustomViewModel<TLDmKinhPhiModel, Core.Domain.NsMucLucNganSach, TlDmKinhPhiService>((TlDmKinhPhiService)_tlDmKinhPhiService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục kinh phí thường xuyên",
                    Title = "Danh mục kinh phí thường xuyên",
                    Description = "Danh mục kinh phí thường xuyên",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_NEW_SALARY_KINH_PHI
                },
                new GenericControlCustomViewModel<TlDmThueThuNhapCaNhanNq104Model, Core.Domain.TlDmThueThuNhapCaNhanNq104, TlDmThueThuNhapCaNhanNq104Service>(_tlDmThueThuNhapCaNhanService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục các mục thuế thu nhập cá nhân",
                    Title = "Danh mục các mục thuế thu nhập cá nhân",
                    Description = "Danh sách các mục thuế thu nhập cá nhân",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_NEW_SALARY_THUE_TN
                },
                new GenericControlCustomViewModel<QsMucLucModel, Core.Domain.NsQsMucLuc, NsQsMucLucService>((NsQsMucLucService)_qSMucLucService, _mapper, _sessionService, _provider)
                {
                    FuncCode = NSFunctionCode.CATEGORY_NEW_SALARY_TANG_GIAM,
                    Name = "Danh mục tăng giảm",
                    Title = "Danh mục tăng giảm",
                    Description = "Danh sách các mục tăng giảm",
                    IsDialog = false,
                },
                new GenericControlCustomViewModel<TlDmPhuCapNq104Model, Core.Domain.TlDmPhuCapNq104, TlDmPhuCapNq104Service>(_tlDmPhuCapService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục phụ cấp thu nhập",
                    Title = "Danh mục phụ cấp thu nhập",
                    Description = "Danh sách các mục phụ cấp thu nhập",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_NEW_SALARY_PHU_CAP
                },
                MucLucCapBacLuongViewModel,
                //new GenericControlCustomViewModel<TlDmCapBacNq104Model, Core.Domain.TlDmCapBacNq104, TlDmCapBacNq104Service>(_tlDmCapBacService, _mapper, _sessionService, _provider)
                //{
                //    Name = "Danh mục cấp bậc",
                //    Title = "Danh mục cấp bậc",
                //    Description = "Danh sách các mục cập bậc",
                //    IsDialog = false,
                //    FuncCode = NSFunctionCode.CATEGORY_NEW_SALARY_CAP_BAC
                //},
                new GenericControlCustomViewModel<TlDmDonViNq104Model, Core.Domain.TlDmDonViNq104, TlDmDonViNq104Service>(_tlDmDonViService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục phân hộ",
                    Title = "Danh mục phân hộ",
                    Description = "Danh sách các mục phân hộ",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_NEW_SALARY_DON_VI
                },
                NewCalculateSalaryIndexViewModel,
                new GenericControlCustomViewModel<TlDmCheDoBHXHModel, Core.Domain.TlDmCheDoBHXH, TlDmCheDoBHXHService>(_tlDmCheDoBHXHService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục chế độ BHXH",
                    Title = "Danh mục chế độ BHXH",
                    Description = "Danh sách các mục chế độ BHXH",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_NEW_SALARY_CHE_DO_BHXH,
                    TemplateFileName = "rpt_dm_che_do_bhxh.xlsx",
                    ImportModelType = typeof(DanhMucCheDoBHXHImportModel),
                    DataTemplateFileName = "rpt_dm_che_do_bhxh_template_data.xlsx",
                    IsConfigMLNS = true,
                },
                NewCategoryHolidayIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
