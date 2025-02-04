using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewTransferData.NewAllowenceMapper;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewTransferData.NewColumnMapper;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewTransferData.NewTransferDataKtdt;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewTransferData
{
    public class NewTransferDataViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly ISessionService _sessionService;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_CHUYEN_DOI_DU_LIEU;
        public override string Name => "CHUYỂN ĐỔI DỮ LIỆU";
        public override string Description => "Chuyển đổi dữ liệu";
        public override Type ContentType => typeof(View.NewSalary.NewCadres.NewCadres);
        public override PackIconKind IconKind => PackIconKind.FolderSwapOutline;

        public NewTransferDataIndexViewModel TransferDataIndexViewModel { get; }
        public NewAllowenceMapperIndexViewModel AllowenceMapperIndexViewModel { get; }
        public NewColumnMapperIndexViewModel ColumnMapperIndexViewModel { get; }
        public NewTransferDataKtdtIndexViewModel TransferDataKtdtIndexViewModel { get; } 

        public NewTransferDataViewModel(
            NewTransferDataIndexViewModel transferDataIndexViewModel,
            NewAllowenceMapperIndexViewModel allowenceMapperIndexViewModel,
            NewColumnMapperIndexViewModel columnMapperIndexViewModel,
            NewTransferDataKtdtIndexViewModel transferDataKtdtIndexViewModel,
            IMapper mapper,
            ISessionService sessionService,
            IServiceProvider provider)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _provider = provider;

            TransferDataIndexViewModel = transferDataIndexViewModel;
            TransferDataIndexViewModel.ParentPage = this;
            AllowenceMapperIndexViewModel = allowenceMapperIndexViewModel;
            AllowenceMapperIndexViewModel.ParentPage = this;
            ColumnMapperIndexViewModel = columnMapperIndexViewModel;
            ColumnMapperIndexViewModel.ParentPage = this;
            TransferDataKtdtIndexViewModel = transferDataKtdtIndexViewModel;
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
                TransferDataIndexViewModel,
                AllowenceMapperIndexViewModel,
                ColumnMapperIndexViewModel,
                TransferDataKtdtIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }

        public void RedirectPage(object sender, EventArgs e)
        {
            InitDocument();
        }
    }
}
