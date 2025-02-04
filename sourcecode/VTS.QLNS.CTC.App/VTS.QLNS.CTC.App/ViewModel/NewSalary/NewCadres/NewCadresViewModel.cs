using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewCadres.NewIncomeTax;
using VTS.QLNS.CTC.App.ViewModel.Salary.NewCadres.NewLaunchAdjusted;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewCadres
{
    public class NewCadresViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly ISessionService _sessionService;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_CADRES;
        public override string Name => "ĐỐI TƯỢNG HƯỞNG LƯƠNG, PHỤ CẤP";
        public override string Description => "Quản lý đối tượng hưởng lương, phụ cấp";
        public override Type ContentType => typeof(View.NewSalary.NewCadres.NewCadres);
        public override PackIconKind IconKind => PackIconKind.AccountTie;
        public NewCadresIndexViewModel CadresIndexViewModel { get; }
        public NewIncomeTaxViewModel IncomeTaxViewModel { get; }
        public NewCadresLauncAdjustedIndexViewModel CadresLauncAdjustedIndexViewModel { get; }

        public NewCadresViewModel(
            NewCadresIndexViewModel cadresIndexViewModel,
            IMapper mapper,
            ISessionService sessionService,
            IServiceProvider provider,
            NewIncomeTaxViewModel incomeTaxViewModel,
            NewCadresLauncAdjustedIndexViewModel cadresLauncAdjustedIndexViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _provider = provider;

            CadresIndexViewModel = cadresIndexViewModel;
            CadresIndexViewModel.ParentPage = this;
            IncomeTaxViewModel = incomeTaxViewModel;
            IncomeTaxViewModel.ParentPage = this;
            CadresLauncAdjustedIndexViewModel = cadresLauncAdjustedIndexViewModel;
            CadresLauncAdjustedIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            InitDocument();
        }

        public void InitDocument()
        {
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                CadresIndexViewModel,
                IncomeTaxViewModel,
                CadresLauncAdjustedIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }

        public void RedirectPage(object sender, EventArgs e)
        {
            InitDocument();
        }
    }
}
