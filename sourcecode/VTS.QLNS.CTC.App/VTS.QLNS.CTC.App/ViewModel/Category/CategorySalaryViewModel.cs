using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.CalculateSalary;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.CategoryHoliday;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class CategorySalaryViewModel : ViewModelBase
    {
        private IMapper _mapper;
        private readonly IServiceProvider _provider;
        private ISessionService _sessionService;
        private TLDanhMucChucVuService _tlDanhMucChucVuService;
        private TlDmTietTieuMucNganhService _tlDmTietTieuMucNganhService;
        private TlDmThueThuNhapCaNhanService _tlDmThueThuNhapCaNhanService;
        private TlDmTangGiamService _tlDmTangGiamService;
        private TlDmPhuCapService _tlDmPhuCapService;
        private TlDmCapBacService _tlDmCapBacService;
        private TlDmDonViService _tlDmDonViService;
        private TlDmPhuCapHeThongService _tlDmPhuCapHeThongService;
        private ITlDmKinhPhiService _tlDmKinhPhiService;
        private INsQsMucLucService _qSMucLucService;
        private TlDmCheDoBHXHService _tlDmCheDoBHXHService;

        public override Type ContentType => typeof(CategoryGeneral);
        public override string Name => "DANH MỤC LƯƠNG";
        public override string Description => "Danh mục dùng chung";
        public override string FuncCode => NSFunctionCode.CATEGORY_SALARY;

        public CalculateSalaryIndexViewModel CalculateSalaryIndexViewModel { get; }
        public CategoryHolidayIndexViewModel CategoryHolidayIndexViewModel { get; }

        public CategorySalaryViewModel(IMapper mapper,
            IServiceProvider provider,
            ISessionService sessionService,
            TLDanhMucChucVuService tLDanhMucChucVuService,
            TlDmTietTieuMucNganhService tlDmTietTieuMucNganhService,
            TlDmThueThuNhapCaNhanService tlDmThueThuNhapCaNhanService,
            TlDmTangGiamService tlDmTangGiamService,
            TlDmPhuCapService tlDmPhuCapService,
            TlDmCapBacService tlDmCapBacService,
            TlDmDonViService tlDmDonviService,
            TlDmPhuCapHeThongService tlDmPhuCapHeThongService,
            ITlDmKinhPhiService tlDmKinhPhiService,
            INsQsMucLucService qsMucLucService,
            CalculateSalaryIndexViewModel calculateSalaryIndexViewModel,
            TlDmCheDoBHXHService tlDmCheDoBHXHService,
            CategoryHolidayIndexViewModel categoryHolidayIndexViewModel)
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
            _tlDmPhuCapHeThongService = tlDmPhuCapHeThongService;
            _tlDmKinhPhiService = tlDmKinhPhiService;
            _qSMucLucService = qsMucLucService;
            _tlDmCheDoBHXHService = tlDmCheDoBHXHService;

            CalculateSalaryIndexViewModel = calculateSalaryIndexViewModel;
            CategoryHolidayIndexViewModel = categoryHolidayIndexViewModel;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
                new GenericControlCustomViewModel<TlDmPhuCapHeThongModel, Core.Domain.TlDmPhuCap, TlDmPhuCapHeThongService>(_tlDmPhuCapHeThongService, _mapper, _sessionService, _provider)
                {
                    Name = "Cấu hình tham số hệ thống",
                    Title = "Cấu hình tham số hệ thống",
                    Description = "Danh sách cấu hình tham số hệ thống",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_SALARY_SYSTEM_CFG
                },
                new GenericControlCustomViewModel<TlDanhMucChucVuModel, Core.Domain.TlDmChucVu, TLDanhMucChucVuService>(_tlDanhMucChucVuService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục chức vụ",
                    Title = "Danh mục chức vu",
                    Description = "Danh sách chức vụ",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_SALARY_CHUC_VU
                },
                new GenericControlCustomViewModel<TLDmKinhPhiModel, Core.Domain.NsMucLucNganSach, TlDmKinhPhiService>((TlDmKinhPhiService)_tlDmKinhPhiService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục kinh phí thường xuyên",
                    Title = "Danh mục kinh phí thường xuyên",
                    Description = "Danh mục kinh phí thường xuyên",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_SALARY_KINH_PHI
                },
                new GenericControlCustomViewModel<TlDmThueThuNhapCaNhanModel, Core.Domain.TlDmThueThuNhapCaNhan, TlDmThueThuNhapCaNhanService>(_tlDmThueThuNhapCaNhanService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục các mục thuế thu nhập cá nhân",
                    Title = "Danh mục các mục thuế thu nhập cá nhân",
                    Description = "Danh sách các mục thuế thu nhập cá nhân",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_SALARY_THUE_TN
                },
                new GenericControlCustomViewModel<QsMucLucModel, Core.Domain.NsQsMucLuc, NsQsMucLucService>((NsQsMucLucService)_qSMucLucService, _mapper, _sessionService, _provider)
                {
                    FuncCode = NSFunctionCode.CATEGORY_SALARY_TANG_GIAM,
                    Name = "Danh mục tăng giảm",
                    Title = "Danh mục tăng giảm",
                    Description = "Danh sách các mục tăng giảm",
                    IsDialog = false,
                },
                new GenericControlCustomViewModel<TlDmPhuCapModel, Core.Domain.TlDmPhuCap, TlDmPhuCapService>(_tlDmPhuCapService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục phụ cấp thu nhập",
                    Title = "Danh mục phụ cấp thu nhập",
                    Description = "Danh sách các mục phụ cấp thu nhập",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_SALARY_PHU_CAP
                },
                new GenericControlCustomViewModel<TlDmCapBacModel, Core.Domain.TlDmCapBac, TlDmCapBacService>(_tlDmCapBacService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục cấp bậc",
                    Title = "Danh mục cấp bậc",
                    Description = "Danh sách các mục cập bậc",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_SALARY_CAP_BAC
                },
                new GenericControlCustomViewModel<TlDmDonViModel, Core.Domain.TlDmDonVi, TlDmDonViService>(_tlDmDonViService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục phân hộ",
                    Title = "Danh mục phân hộ",
                    Description = "Danh sách các mục phân hộ",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_SALARY_DON_VI
                },
                CalculateSalaryIndexViewModel,
                new GenericControlCustomViewModel<TlDmCheDoBHXHModel, Core.Domain.TlDmCheDoBHXH, TlDmCheDoBHXHService>(_tlDmCheDoBHXHService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục chế độ BHXH",
                    Title = "Danh mục chế độ BHXH",
                    Description = "Danh sách các mục chế độ BHXH",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_SALARY_CHE_DO_BHXH,
                    TemplateFileName = "rpt_dm_che_do_bhxh.xlsx",
                    ImportModelType = typeof(DanhMucCheDoBHXHImportModel),
                    DataTemplateFileName = "rpt_dm_che_do_bhxh_template_data.xlsx",
                    IsConfigMLNS = true,
                },
                CategoryHolidayIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
