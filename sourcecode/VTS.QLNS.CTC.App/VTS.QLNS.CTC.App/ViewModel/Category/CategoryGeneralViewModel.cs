using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.UserManagement.User;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class CategoryGeneralViewModel : ViewModelBase
    {
        private IMapper _mapper;
        private INsDonViService _nsDonViService;
        private INsPhongBanService _nsPhongBanService;
        private UserDialogViewModel _userDialogViewModel;
        private readonly IServiceProvider _provider;
        private ISessionService _sessionService;
        private DanhMucCauHinhHeThongService _danhMucCauHinhHeThongService;
        private IDmChuKyService _dmChuKyService;
        private DmNhomChuKyService _dmNhomChuKyService;
        private ICauHinhCanCuService _cauHinhCanCuService;
        private IDmCapBacService _dmCapBacService;
        private IDmDeTaiService _dmDeTaiService;

        public override Type ContentType => typeof(CategoryGeneral);
        public override string Name => "DANH MỤC DÙNG CHUNG";
        public override string Description => "Danh mục dùng chung";
        public override string FuncCode => NSFunctionCode.CATEGORY_GENERAL;
        public CategoryGeneralViewModel(IMapper mapper,
            INsPhongBanService nsPhongBanService,
            UserDialogViewModel userDialogViewModel,
            IServiceProvider provider,
            INsDonViService nsDonViService,
            ISessionService sessionService,
            DanhMucCauHinhHeThongService danhMucCauHinhHeThongService,
            IDmChuKyService dmChuKyService,
            DmNhomChuKyService dmNhomChuKyService,
            ICauHinhCanCuService cauHinhCanCuService,
            IDmCapBacService dmCapBacService,
            IDmDeTaiService dmDeTaiService)
        {
            _mapper = mapper;
            _userDialogViewModel = userDialogViewModel;
            _provider = provider;
            _nsDonViService = nsDonViService;
            _nsPhongBanService = nsPhongBanService;
            _sessionService = sessionService;
            _danhMucCauHinhHeThongService = danhMucCauHinhHeThongService;
            _dmChuKyService = dmChuKyService;
            _dmNhomChuKyService = dmNhomChuKyService;
            _cauHinhCanCuService = cauHinhCanCuService;
            _dmCapBacService = dmCapBacService;
            _dmDeTaiService = dmDeTaiService;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
                new GenericControlCustomViewModel<DonViModel, Core.Domain.DonVi, NsDonViService>((NsDonViService)_nsDonViService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục đơn vị",
                    Title = "Danh mục đơn vị",
                    Description = "Danh sách danh mục đơn vị",
                    IsDialog = false,
                    TemplateFileName = "rpt_dm_donvi.xlsx",
                    ImportModelType = typeof(DanhMucDonViImportModel),
                    DataTemplateFileName = "rpt_dm_donvi_template_data.xlsx",
                    FuncCode = NSFunctionCode.CATEGORY_GENERAL_DONVI
                },
                new GenericControlCustomViewModel<NSPhongBanModel, Core.Domain.DmBQuanLy, NsPhongBanService>((NsPhongBanService)_nsPhongBanService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục B quản lý",
                    Title = "Danh mục B quản lý",
                    Description = "Danh sách danh mục B quản lý",
                    IsDialog = false,
                    TemplateFileName = "rpt_dm_bquanly_template_data.xlsx",
                    FuncCode = NSFunctionCode.CATEGORY_GENERAL_PHONGBAN
                },
                new GenericControlCustomViewModel<DanhMucCauHinhHeThongModel, Core.Domain.DanhMuc, DanhMucCauHinhHeThongService>((DanhMucCauHinhHeThongService)_danhMucCauHinhHeThongService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục cấu hình hệ thống",
                    Title = "Danh mục cấu hình hệ thống",
                    Description = "Danh mục cấu hình hệ thống",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Settings,
                    IsDialog = false,
                    TemplateFileName = "rpt_dm_cauhinh_template_data.xlsx",
                    FuncCode = NSFunctionCode.CATEGORY_GENERAL_CFG_SYSTEM
                },
                new GenericControlCustomViewModel<DmChuKyModel, Core.Domain.DmChuKy, DmChuKyService>((DmChuKyService)_dmChuKyService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục cấu hình báo cáo",
                    Title = "Danh mục cấu hình báo cáo",
                    Description = "Danh mục cấu hình báo cáo",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Sign,
                    IsDialog = false,
                    TemplateFileName = "rpt_dm_cauhinh_baocao_template_data.xlsx",
                    IsDanhMucChuky = true,
                    FuncCode = NSFunctionCode.CATEGORY_GENERAL_SIGN
                },
                new GenericControlCustomViewModel<DmCapBacNhomChuKyModel, Core.Domain.DanhMuc, DmCapBacService>((DmCapBacService)_dmCapBacService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục nhóm chữ ký",
                    Title = "Danh mục nhóm chữ ký",
                    Description = "Danh mục nhóm chữ ký",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Group,
                    IsDialog = false,
                    IsDanhMucChuky = true,
                    FuncCode = NSFunctionCode.CATEGORY_GENERAL_SIGN_GROUP
                },
                new GenericControlCustomViewModel<DmCapBacModel, Core.Domain.DanhMuc, DmCapBacService>((DmCapBacService)_dmCapBacService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục cấu hình chữ ký",
                    Title = "Danh mục cấu hình chữ ký",
                    Description = "Danh mục cấu hình chữ ký",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Group,
                    IsDialog = false,
                    IsDanhMucChuky = true,
                    TemplateFileName = "rpt_dm_cauhinh_chuky_template_data.xlsx",
                    FuncCode = NSFunctionCode.CATEGORY_GENERAL_SIGN_GROUP
                },
                new GenericControlCustomViewModel<CauHinhCanCuModel, Core.Domain.NsCauHinhCanCu, CauHinhCanCuService>((CauHinhCanCuService)_cauHinhCanCuService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục cấu hình căn cứ",
                    Title = "Danh mục cấu hình căn cứ",
                    Description = "Danh mục cấu hình căn cứ",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.SquareRoot,
                    IsDialog = false,
                    TemplateFileName = "rpt_dm_cauhinh_cancu_template_data.xlsx",
                    FuncCode = NSFunctionCode.CATEGORY_GENERAL_CFG_CANCU
                },
                new GenericControlCustomViewModel<DmDeTaiModel, Core.Domain.DmDeTai, DmDeTaiService>((DmDeTaiService)_dmDeTaiService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục chuyên đề",
                    Title = "Danh mục chuyên đề",
                    Description = "Danh mục chuyên đề",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.SquareRoot,
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_GENERAL_CFG_CHUYENDE,
                    TemplateFileName = "rpt_dm_cauhinh_chuyende_template_data.xlsx",
                },
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
