using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Service;
using AutoMapper;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.Forex.ExchangeRate;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDanhMucNhaThau;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class CategoryForexViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly ISessionService _sessionService;
        private readonly INhDmLoaiTienTeService _nhDmLoaiTienTeService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly INhDmChiPhiService _nhDmChiPhiService;
        private readonly INhDmNhaThauService _nhDmNhaThauService;
        private readonly INhDmLoaiHopDongService _nhDmLoaiHopDongService;
        private readonly INhDmHinhThucChonNhaThauService _nhDmHinhThucChonNhaThauService;
        private readonly INhDmPhuongThucChonNhaThauService _nhDmPhuongThucChonNhaThauService;
        private readonly INhDmPhanCapPheDuyetService _nhDmPhanCapPheDuyetService;
        private readonly INhDmLoaiCongTrinhService _nhDmLoaiCongTrinhService;
        private readonly INhDmLoaiTaiSanService _nhDmLoaiTaiSanService;
        private readonly INhDmNoiDungChiService _nhDmNoiDungChiService;
        private readonly INhDmTaiKhoanService _nhDmTaiKhoanService;
        private readonly INhDmButToanInputService _nhDmButToanInputService;
        private readonly INhDmCongThucOutputService _nhDmCongThucOutputService;

        

        public override Type ContentType => typeof(CategoryForex);
        public override string Name => "DANH MỤC NGOẠI HỐI";
        public override string Description => "Danh mục ngoại hối";

        public override string FuncCode => NSFunctionCode.CATEGORY_FOREX;

        ExchangeRateIndexViewModel ExchangeRateIndexViewModel { get; }

        ForexDmNhaThauIndexViewModel ForexDmNhaThauIndexViewModel { get; }

        public CategoryForexViewModel(IMapper mapper,
            IServiceProvider provider,
            ISessionService sessionService,
            INsPhongBanService nsPhongBanService,
            INhDmLoaiTienTeService nhDmLoaiTienTeService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            ExchangeRateIndexViewModel exchangeRateIndexViewModel,
            ForexDmNhaThauIndexViewModel forexDmNhaThauIndexViewModel,
            INhDmChiPhiService nhDmChiPhiService,
            INhDmNhaThauService nhDmNhaThauService,
            INhDmLoaiHopDongService nhDmLoaiHopDongService,
            INhDmHinhThucChonNhaThauService nhDmHinhThucChonNhaThauService,
            INhDmPhuongThucChonNhaThauService nhDmPhuongThucChonNhaThauService,
            INhDmPhanCapPheDuyetService nhDmPhanCapPheDuyetService,
            INhDmLoaiCongTrinhService nhDmLoaiCongTrinhService,
            INhDmNoiDungChiService nhDmNoiDungChiService,
            INhDmLoaiTaiSanService nhDmLoaiTaiSanService,
            INhDmTaiKhoanService nhDmTaiKhoanService,
            INhDmButToanInputService nhDmButToanInputService,
            INhDmCongThucOutputService nhDmCongThucOutputService)
        {
            _mapper = mapper;
            _provider = provider;
            _sessionService = sessionService;
            _nhDmLoaiTienTeService = nhDmLoaiTienTeService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _nhDmChiPhiService = nhDmChiPhiService;
            _nhDmNhaThauService = nhDmNhaThauService;
            _nhDmLoaiHopDongService = nhDmLoaiHopDongService;
            _nhDmHinhThucChonNhaThauService = nhDmHinhThucChonNhaThauService;
            _nhDmPhuongThucChonNhaThauService = nhDmPhuongThucChonNhaThauService;
            _nhDmPhanCapPheDuyetService = nhDmPhanCapPheDuyetService;
            _nhDmLoaiCongTrinhService = nhDmLoaiCongTrinhService;
            _nhDmNoiDungChiService = nhDmNoiDungChiService;
            _nhDmButToanInputService = nhDmButToanInputService;
            _nhDmCongThucOutputService = nhDmCongThucOutputService;
            _nhDmTaiKhoanService = nhDmTaiKhoanService;


            ExchangeRateIndexViewModel = exchangeRateIndexViewModel;
            ForexDmNhaThauIndexViewModel = forexDmNhaThauIndexViewModel;
            _nhDmLoaiTaiSanService = nhDmLoaiTaiSanService;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(0);
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                new GenericControlCustomViewModel<DmLoaiTienTeModel, Core.Domain.NhDmLoaiTienTe, NhDmLoaiTienTeService>((NhDmLoaiTienTeService)_nhDmLoaiTienTeService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục loại tiền tệ",
                    Title = "Danh mục loại tiền tệ",
                    Description = "Danh sách danh mục loại tiền tệ",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_FOREX_CURRENCY
                },
                ExchangeRateIndexViewModel,
                new GenericControlCustomViewModel<NhDmNhiemVuChiModel, Core.Domain.NhDmNhiemVuChi, NhDmNhiemVuChiService>((NhDmNhiemVuChiService)_nhDmNhiemVuChiService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục chương trình",
                    Title = "Danh mục chương trình",
                    Description = "Danh sách danh mục chương trình",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_FOREX_MISSION
                },
                new GenericControlCustomViewModel<NhDmChiPhiModel, Core.Domain.NhDmChiPhi, NhDmChiPhiService>((NhDmChiPhiService)_nhDmChiPhiService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục chi phí",
                    Title = "Danh mục chi phí",
                    Description = "Danh sách danh mục chi phí",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false
                   // FuncCode = NSFunctionCode.CATEGORY_FOREX_COST
                },
                new GenericControlCustomViewModel<NhDmNhaThauModel, Core.Domain.NhDmNhaThau, NhDmNhaThauService>((NhDmNhaThauService)_nhDmNhaThauService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục nhà thầu",
                    Title = "Danh mục nhà thầu",
                    Description = "Danh sách danh mục nhà thầu",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false,
                    TemplateFileName = ExportFileName.EPT_NH_DANHMUC_NHATHAU
                    // FuncCode = NSFunctionCode.CATEGORY_FOREX_COST
                },
                new GenericControlCustomViewModel<NhDmLoaiHopDongModel, Core.Domain.NhDmLoaiHopDong, NhDmLoaiHopDongService>((NhDmLoaiHopDongService)_nhDmLoaiHopDongService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục loại hợp đồng",
                    Title = "Danh mục loại hợp đồng",
                    Description = "Danh sách danh mục loại hợp đồng",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false
                   // FuncCode = NSFunctionCode.CATEGORY_FOREX_COST
                },
                new GenericControlCustomViewModel<NhDmHinhThucChonNhaThauModel, Core.Domain.NhDmHinhThucChonNhaThau, NhDmHinhThucChonNhaThauService>((NhDmHinhThucChonNhaThauService)_nhDmHinhThucChonNhaThauService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục hình thức chọn nhà thầu",
                    Title = "Danh mục hình thức chọn nhà thầu",
                    Description = "Danh sách danh mục hình thức chọn nhà thầu",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false
                   // FuncCode = NSFunctionCode.CATEGORY_FOREX_COST
                },
                new GenericControlCustomViewModel<NhDmPhuongThucChonNhaThauModel, Core.Domain.NhDmPhuongThucChonNhaThau, NhDmPhuongThucChonNhaThauSerive>((NhDmPhuongThucChonNhaThauSerive)_nhDmPhuongThucChonNhaThauService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục phương thức chọn nhà thầu",
                    Title = "Danh mục phương thức chọn nhà thầu",
                    Description = "Danh sách danh mục phương thức chọn nhà thầu",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false
                   // FuncCode = NSFunctionCode.CATEGORY_FOREX_COST
                },
                new GenericControlCustomViewModel<NhDmPhanCapPheDuyetModel, Core.Domain.NhDmPhanCapPheDuyet, NhDmPhanCapPheDuyetService>((NhDmPhanCapPheDuyetService)_nhDmPhanCapPheDuyetService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục phân cấp phê duyệt",
                    Title = "Danh mục phân cấp phê duyệt",
                    Description = "Danh sách danh mục phân cấp phê duyệt",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false
                   // FuncCode = NSFunctionCode.CATEGORY_FOREX_COST
                },
                new GenericControlCustomViewModel<NhDmLoaiCongTrinhModel, Core.Domain.NhDmLoaiCongTrinh, NhDmLoaiCongTrinhService>((NhDmLoaiCongTrinhService)_nhDmLoaiCongTrinhService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục loại công trình",
                    Title = "Danh mục loại công trình",
                    Description = "Danh sách danh mục loại công trình",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false
                   // FuncCode = NSFunctionCode.CATEGORY_FOREX_COST
                },
                new GenericControlCustomViewModel<NhDmLoaiTaiSanModel, Core.Domain.NhDmLoaiTaiSan, NhDmLoaiTaiSanService>((NhDmLoaiTaiSanService)_nhDmLoaiTaiSanService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục loại tài sản",
                    Title = "Danh mục loại tài sản",
                    Description = "Danh sách danh mục loại tài sản",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false
                   // FuncCode = NSFunctionCode.CATEGORY_FOREX_COST
                }, new GenericControlCustomViewModel<NhDmNoiDungChiModel, Core.Domain.NhDmNoiDungChi, NhDmNoiDungChiService>((NhDmNoiDungChiService)_nhDmNoiDungChiService, _mapper, _sessionService, _provider)
                {
                     Name = "Danh mục nội dung chi",
                    Title = "Danh mục nội dung chi",
                    Description = "Danh sách nội dung chi",
                     IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false
                   // FuncCode = NSFunctionCode.CATEGORY_FOREX_COST
                }, new GenericControlCustomViewModel<NhDmTaiKhoanModel, Core.Domain.NhDmTaiKhoan, NhDmTaiKhoanService>((NhDmTaiKhoanService)_nhDmTaiKhoanService, _mapper, _sessionService, _provider)
                {
                     Name = "Danh mục tài khoản",
                    Title = "Danh mục tài khoản",
                    Description = "Danh sách tài khoản",
                     IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false
                   // FuncCode = NSFunctionCode.CATEGORY_FOREX_COST
                }, new GenericControlCustomViewModel<NhDmButToanInputModel, Core.Domain.NhDmButToanInput, NhDmButToanInputService>((NhDmButToanInputService)_nhDmButToanInputService, _mapper, _sessionService, _provider)
                {
                     Name = "Danh mục bút toán",
                    Title = "Danh mục bút toán",
                    Description = "Danh sách bút toán",
                     IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false
                   // FuncCode = NSFunctionCode.CATEGORY_FOREX_COST
                }, new GenericControlCustomViewModel<NhDmCongThucOutputModel, Core.Domain.NhDmCongThucOutput, NhDmCongThucOutputService>((NhDmCongThucOutputService)_nhDmCongThucOutputService, _mapper, _sessionService, _provider)
                {
                     Name = "Danh mục công thức",
                    Title = "Danh mục công thức",
                    Description = "Danh sách công thức",
                     IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false
                   // FuncCode = NSFunctionCode.CATEGORY_FOREX_COST
                }
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);

        }
    }
}
