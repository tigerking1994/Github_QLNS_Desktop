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
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class CategoryBudgetViewModel : ViewModelBase
    {
        private IMapper _mapper;
        private DanhMucNguonNganSachService _danhMucNguonNganSachService;
        private IMucLucNganSachService _mucLucNganSachService;
        private INsQsMucLucService _qSMucLucService;
        private ISktMucLucService _sktMuclucService;
        private ICpDanhMucService _cpDanhMucService;
        private DanhMucNganhService _danhMucNganhService;
        private DanhMucNhomNganhService _danhMucNhomNganhService;
        private ICauHinhMLNSService _cauHinhMLNSService;
        private ISessionService _sessionService;
        private IServiceProvider _serviceProvider;
        private readonly IDmCongKhaiTaiChinhService _dmCongKhaiTaiChinhService;
        private readonly IDmMucLucQuyetToanService _dmMucLucQuyetToanService;

        private MucLucNganSachViewModel MucLucNganSachViewModel { get; set; }
        private MucLucSKTViewModel MucLucSKTViewModel { get; set; }

        public override Type ContentType => typeof(CategoryBudget);
        public override string Name => "DANH MỤC NGÂN SÁCH";
        public override string Description => "Danh mục ngân sách";
        public override string FuncCode => NSFunctionCode.BUDGET;


        public CategoryBudgetViewModel(IMapper mapper,
            DanhMucNguonNganSachService danhMucNguonNganSachService,
            IMucLucNganSachService mucLucNganSachService,
            INsQsMucLucService qSMucLucService,
            ISktMucLucService sktMucLucService,
            ICpDanhMucService cpDanhMucService,
            DanhMucNganhService danhMucNganhService,
            DanhMucNhomNganhService danhMucNhomNganhService,
            IDmMucLucQuyetToanService dmMucLucQuyetToanService,
            ICauHinhMLNSService cauHinhMLNSService,
            ISessionService sessionService,
            IDmCongKhaiTaiChinhService dmCongKhaiTaiChinhService,
            IServiceProvider provider,
            MucLucNganSachViewModel mucLucNganSachViewModel,
            MucLucSKTViewModel mucLucSKTViewModel)
        {
            _mapper = mapper;
            _danhMucNguonNganSachService = danhMucNguonNganSachService;
            _mucLucNganSachService = mucLucNganSachService;
            _qSMucLucService = qSMucLucService;
            _sktMuclucService = sktMucLucService;
            _cpDanhMucService = cpDanhMucService;
            _danhMucNganhService = danhMucNganhService;
            _danhMucNhomNganhService = danhMucNhomNganhService;
            _cauHinhMLNSService = cauHinhMLNSService;
            _sessionService = sessionService;
            _serviceProvider = provider;
            MucLucNganSachViewModel = mucLucNganSachViewModel;
            MucLucSKTViewModel = mucLucSKTViewModel;
            _dmCongKhaiTaiChinhService = dmCongKhaiTaiChinhService;
            _dmMucLucQuyetToanService = dmMucLucQuyetToanService;

        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(0);
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                new GenericControlCustomViewModel<DanhMucNguonNganSachModel, Core.Domain.DanhMuc, DanhMucNguonNganSachService>((DanhMucNguonNganSachService)_danhMucNguonNganSachService, _mapper, _sessionService, _serviceProvider)
                {
                    Name = "Danh mục nguồn ngân sách",
                    Title = "Danh mục nguồn ngân sách",
                    Description = "Danh mục cấu hình nguồn ngân sách",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.LibraryPlus,
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_GENERAL_CFG_SYSTEM
                },
                MucLucNganSachViewModel,
                new GenericControlCustomViewModel<QsMucLucModel, Core.Domain.NsQsMucLuc, NsQsMucLucService>((NsQsMucLucService)_qSMucLucService, _mapper, _sessionService, _serviceProvider)
                {
                    FuncCode = NSFunctionCode.CATEGORY_BUDGET_MLQS,
                    Name = "Danh mục mục lục quân số",
                    Title = "Danh mục mục lục quân số",
                    Description = "Chỉnh sửa thông tin mục lục quân số",
                    TemplateFileName = "rpt_dm_mlqs.xlsx",
                    ImportModelType = typeof(DanhMucMLQSImportModel),
                    DataTemplateFileName = "rpt_dm_mlqs_template_data.xlsx"
                },
                MucLucSKTViewModel,
                new GenericControlCustomViewModel<CpDanhMucModel, Core.Domain.CpDanhMuc, CpDanhMucService>((CpDanhMucService)_cpDanhMucService, _mapper, _sessionService, _serviceProvider)
                {
                    FuncCode = NSFunctionCode.CATEGORY_BUDGET_ALLOCATION,
                    Name = "Danh mục cấp phát",
                    Title = "Danh mục cấp phát",
                    Description = "Danh sách danh mục cấp phát",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false,
                },
                new GenericControlCustomViewModel<DanhMucNganhModel, Core.Domain.DanhMuc, DanhMucNganhService>(_danhMucNganhService, _mapper, _sessionService, _serviceProvider)
                {
                    FuncCode = NSFunctionCode.CATEGORY_BUDGET_CNG,
                    Name = "Danh mục chuyên ngành",
                    Title = "Danh mục chuyên ngành",
                    Description = "Chỉnh sửa thông tin chuyên ngành"
                },
                new GenericControlCustomViewModel<DanhMucNhomNganhModel, Core.Domain.DanhMuc, DanhMucNhomNganhService>(_danhMucNhomNganhService, _mapper, _sessionService, _serviceProvider)
                {
                    FuncCode = NSFunctionCode.CATEGORY_BUDGET_NGANH,
                    Name = "Danh mục ngành",
                    Title = "Danh mục ngành",
                    Description = "Chỉnh sửa thông tin ngành"
                },
                new GenericControlCustomViewModel<CauHinhMLNSModel, Core.Domain.NsMucLucNganSach, CauHinhMLNSService>((CauHinhMLNSService)_cauHinhMLNSService, _mapper, _sessionService, _serviceProvider)
                {
                    FuncCode = NSFunctionCode.CATEGORY_BUDGET_CFG_MLNS,
                    Name = "Danh mục cấu hình MLNS",
                    Title = "Danh mục cấu hình MLNS",
                    Description = "Danh mục cấu hình MLNS",
                    IsDialog = false
                },
                 new GenericControlCustomViewModel<DmCongKhaiTaiChinhModel, Core.Domain.NsDanhMucCongKhai, DmCongKhaiTaiChinhService>((DmCongKhaiTaiChinhService)_dmCongKhaiTaiChinhService, _mapper, _sessionService, _serviceProvider)
                {
                    Name = "Công khai tài chính",
                    Title = "Danh mục công khai tài chính",
                    Description = "Danh sách các mục tài chính công khai",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Bank,
                    IsDialog = false,
                },
                   new GenericControlCustomViewModel<DmMucLucQuyetToanModel, Core.Domain.NsMucLucQuyetToanNam, DmMucLucQuyetToanService>((DmMucLucQuyetToanService)_dmMucLucQuyetToanService, _mapper, _sessionService, _serviceProvider)
                {
                    Name = "Mục lục quyết toán năm",
                    Title = "Danh mục mục lục quyết toán năm",
                    Description = "Danh sách các mục lục quyết toán",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Bank,
                    IsDialog = false,
                }
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
