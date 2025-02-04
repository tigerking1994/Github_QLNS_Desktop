using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Salary.TransferData.AllowenceMapper;
using VTS.QLNS.CTC.App.ViewModel.Salary.TransferData.ColumnMapper;
using VTS.QLNS.CTC.App.ViewModel.Salary.TransferData.TransferDataKtdt;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.TransferData
{
    public class TransferDataViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly ISessionService _sessionService;

        public override string FuncCode => NSFunctionCode.SALARY_CHUYEN_DOI_DU_LIEU;
        public override string Name => "CHUYỂN ĐỔI DỮ LIỆU";
        public override string Description => "Chuyển đổi dữ liệu";
        public override Type ContentType => typeof(View.Salary.Cadres.Cadres);
        public override PackIconKind IconKind => PackIconKind.FolderSwapOutline;

        public TransferDataIndexViewModel TransferDataIndexViewModel { get; }
        public AllowenceMapperIndexViewModel AllowenceMapperIndexViewModel { get; }
        public ColumnMapperIndexViewModel ColumnMapperIndexViewModel { get; }
        public TransferDataKtdtIndexViewModel TransferDataKtdtIndexViewModel { get; } 

        public TransferDataViewModel(
            TransferDataIndexViewModel transferDataIndexViewModel,
            AllowenceMapperIndexViewModel allowenceMapperIndexViewModel,
            ColumnMapperIndexViewModel columnMapperIndexViewModel,
            TransferDataKtdtIndexViewModel transferDataKtdtIndexViewModel,
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
