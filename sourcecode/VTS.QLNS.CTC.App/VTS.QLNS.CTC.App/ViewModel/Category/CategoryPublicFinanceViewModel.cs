using System;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.Core.Service;
using AutoMapper;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model;
using System.Linq;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class CategoryPublicFinanceViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly ISessionService _sessionService;
        private readonly IDmCongKhaiTaiChinhService _dmCongKhaiTaiChinhService;

        public override Type ContentType => typeof(CategorySocialInsurance);
        public override string Name => "DANH MỤC CÔNG KHAI TÀI CHÍNH";
        public override string Description => "Danh mục công khai tài chính";

        public CategoryPublicFinanceViewModel
        (
            IMapper mapper,
            IServiceProvider provider,
            ISessionService sessionService,
            IDmCongKhaiTaiChinhService dmCongKhaiTaiChinhService,
            IBhDmKinhPhiService bhDmKinhPhiService
        )
        {
            _mapper = mapper;
            _provider = provider;
            _sessionService = sessionService;
            _dmCongKhaiTaiChinhService = dmCongKhaiTaiChinhService;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(0);
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                new GenericControlCustomViewModel<DmCongKhaiTaiChinhModel, Core.Domain.NsDanhMucCongKhai, DmCongKhaiTaiChinhService>((DmCongKhaiTaiChinhService)_dmCongKhaiTaiChinhService, _mapper, _sessionService, _provider)
                {
                    Name = "Công khai tài chính",
                    Title = "Danh mục công khai tài chính",
                    Description = "Danh sách các mục tài chính công khai",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Bank,
                    IsDialog = false,
                }
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
