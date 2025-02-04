using System;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.Core.Service;
using AutoMapper;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class CategorySocialInsuranceViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly ISessionService _sessionService;
        private readonly IBhDmCheDoBhxhService _bhDmCheDoBhxhService;
        private readonly IBhDmCauHinhThamSoService _bhDmCauHinhThamSoService;
        private readonly IBhDmMucLucBHXHMapService _bhDmMucLucBHXHMapService;
        private readonly IBhDmKinhPhiService _bhDmKinhPhiService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IBhDmMucDongBHXHService _bhDmMucDongBHXHService;
        private readonly IBhDmCoSoYTeService _bhDmCoSoYTeService;
        private readonly IBhDmThamDinhQuyetToanService _bhDmThamDinhQuyetToanService;

        private MucLucNganSachBHXHViewModel MucLucNganSachBHXHViewModel { get; set; }

        public override Type ContentType => typeof(CategorySocialInsurance);
        public override string Name => "DANH MỤC BHXH";
        public override string Description => "Danh mục BHXH";

        //public override string FuncCode => NSFunctionCode.CATEGORY_FOREX;

        public CategorySocialInsuranceViewModel
        (
            IMapper mapper,
            IServiceProvider provider,
            ISessionService sessionService,
            IBhDmCheDoBhxhService bhDmCheDoBhxhService,
            IBhDmCauHinhThamSoService bhDmCauHinhThamSoService,
            IBhDmMucLucBHXHMapService bhDmMucLucBHXHMapService,
            IBhDmKinhPhiService bhDmKinhPhiService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            MucLucNganSachBHXHViewModel mucLucNganSachBHXHViewModel,
            IBhDmMucDongBHXHService bhDmMucDongBHXHService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            IBhDmThamDinhQuyetToanService bhDmThamDinhQuyetToanService
        )
        {
            _mapper = mapper;
            _provider = provider;
            _sessionService = sessionService;
            _bhDmCheDoBhxhService = bhDmCheDoBhxhService;
            _bhDmCauHinhThamSoService = bhDmCauHinhThamSoService;
            _bhDmMucLucBHXHMapService = bhDmMucLucBHXHMapService;
            _bhDmKinhPhiService = bhDmKinhPhiService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            MucLucNganSachBHXHViewModel = mucLucNganSachBHXHViewModel;
            _bhDmMucDongBHXHService = bhDmMucDongBHXHService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;
            _bhDmThamDinhQuyetToanService = bhDmThamDinhQuyetToanService;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(0);
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                MucLucNganSachBHXHViewModel,
                new GenericControlCustomViewModel<BhDmCauHinhThamSoModel, Core.Domain.BhDmCauHinhThamSo, BhDmCauHinhThamSoService>((BhDmCauHinhThamSoService)_bhDmCauHinhThamSoService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục cấu hình tham số BHXH",
                    Title = "Danh mục cấu hình tham số BHXH",
                    Description = "Danh sách cấu hình tham số BHXH",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Library,
                    IsDialog = false,
                },
                new GenericControlCustomViewModel<BhDmMucLucBHXHMapModel, Core.Domain.BhDmMucLucNganSach, BhDmMucLucBHXHMapService>((BhDmMucLucBHXHMapService)_bhDmMucLucBHXHMapService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục cấu hình MLNS BHXH",
                    Title = "Danh mục cấu hình MLNS BHXH",
                    Description = "Danh sách cấu hình MLNS BHXH",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Library,
                    IsDialog = false,
                },
                //new GenericControlCustomViewModel<BhDmCheDoBhxhModel, Core.Domain.BhDmCheDoBhxh, BhDmCheDoBhxhService>((BhDmCheDoBhxhService)_bhDmCheDoBhxhService, _mapper, _sessionService, _provider)
                //{
                //    Name = "Danh mục các chế độ BHXH",
                //    Title = "Danh mục các chế độ BHXH",
                //    Description = "Danh sách các chế độ BHXH",
                //    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Bank,
                //    IsDialog = false,
                //    //FuncCode = NSFunctionCode.CATEGORY_FOREX_CURRENCY
                //},
                //new GenericControlCustomViewModel<BhDmKinhPhiModel, Core.Domain.BhDmKinhPhi, BhDmKinhPhiService>((BhDmKinhPhiService)_bhDmKinhPhiService, _mapper, _sessionService, _provider)
                //{
                //    Name = "Danh mục các loại kinh phí",
                //    Title = "Danh mục các loại kinh phí",
                //    Description = "Danh mục các loại kinh phí",
                //    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Cash100,
                //    IsDialog = false,
                //},
                new GenericControlCustomViewModel<BhDanhMucLoaiChiModel, Core.Domain.BhDanhMucLoaiChi, BhDanhMucLoaiChiService>((BhDanhMucLoaiChiService)_bhDanhMucLoaiChiService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục các loại chi",
                    Title = "Danh mục các loại chi",
                    Description = "Danh mục các loại chi",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Cash100,
                    IsDialog = false,
                },
                //new GenericControlCustomViewModel<BhDmMucDongBHXHModel, Core.Domain.BhDmMucDongBHXH, BhDmMucDongBHXHService>((BhDmMucDongBHXHService)_bhDmMucDongBHXHService, _mapper, _sessionService, _provider)
                //{
                //    Name = "Danh mục đóng BHXH",
                //    Title = "Danh mục đóng BHXH",
                //    Description = "Danh mục đóng BHXH",
                //    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Cash100,
                //    IsDialog = false,
                //},
                new GenericControlCustomViewModel<BhDmCoSoYTeModel, Core.Domain.BhDmCoSoYTe, BhDmCoSoYTeService>((BhDmCoSoYTeService)_bhDmCoSoYTeService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục cơ sở y tế ",
                    Title = "Danh mục cơ sở y tế",
                    Description = "Danh mục cơ sở y tế",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Cash100,
                    IsDialog = false,
                },
                new GenericControlCustomViewModel<BhDmThamDinhQuyetToanModel, Core.Domain.BhDmThamDinhQuyetToan, BhDmThamDinhQuyetToanService>((BhDmThamDinhQuyetToanService)_bhDmThamDinhQuyetToanService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục thẩm định quyết toán ",
                    Title = "Danh mục thẩm định quyết toán",
                    Description = "Danh mục thẩm định quyết toán",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Cash100,
                    IsDialog = false,
                },
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
