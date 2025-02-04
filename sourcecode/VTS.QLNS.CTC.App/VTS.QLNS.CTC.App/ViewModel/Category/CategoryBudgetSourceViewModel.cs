using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class CategoryBudgetSourceViewModel : ViewModelBase
    {
        private IMapper _mapper;
        private INsNguonNganSachService _nsNguonNganSachService;
        private readonly IServiceProvider _provider;
        private ISessionService _sessionService;

        public override Type ContentType => typeof(CategoryBudgetSource);
        public override string Name => "DANH MỤC NGUỒN NGÂN SÁCH";
        public override string Description => "Danh mục nguồn ngân sách";
        public override string FuncCode => NSFunctionCode.CATEGORY_NNS;

        public CategoryBudgetSourceViewModel(IMapper mapper, 
            INsNguonNganSachService nsNguonNganSachService,
            IServiceProvider provider,
            ISessionService sessionService)
        {
            _mapper = mapper;
            _nsNguonNganSachService = nsNguonNganSachService;
            _sessionService = sessionService;
            _provider = provider;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
                new GenericControlCustomViewModel<NguonNganSachModel, Core.Domain.NsNguonNganSach, NsNguonNganSachService>((NsNguonNganSachService)_nsNguonNganSachService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục nguồn ngân sách",
                    Title = "Danh mục nguồn ngân sách",
                    Description = "Danh sách danh mục nguồn ngân sách",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false,
                },
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
