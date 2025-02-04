using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class MlnsBhxhExportViewModel : ViewModelBase
    {
        public override string Name => "DANH MỤC";
        public override Type ContentType => typeof(View.Category.MlnsExportView);
        public override PackIconKind IconKind => PackIconKind.ViewList;

        public IMapper Mapper { get; set; }
        public ISessionService sessionService { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        private BhDmMucLucNganSachService _bhMucLucNganSachService;
        private readonly ILog _logger;

        private int _namLamViec;

        public Guid SelectedMlnsId { get; set; }

        public ObservableCollection<BhDmMucLucNganSachModel> bhMuclucNgansachModels { get; set; }

        public RelayCommand ExportCommand { get; }

        public MlnsBhxhExportViewModel(BhDmMucLucNganSachService bhMucLucNganSachService)
        {
            _bhMucLucNganSachService = bhMucLucNganSachService;
            ExportCommand = new RelayCommand(obj => OnExport());
        }

        public override void Init()
        {
            base.Init();
            _namLamViec = sessionService.Current.YearOfWork;
            MarginRequirement = new System.Windows.Thickness(0);
            ObservableCollection<BhDmMucLucNganSach> nsMLNS = new ObservableCollection<BhDmMucLucNganSach>(_bhMucLucNganSachService.FindByIsHangCha(_namLamViec, true).ToList());
            bhMuclucNgansachModels = Mapper.Map<ObservableCollection<BhDmMucLucNganSach>, ObservableCollection<BhDmMucLucNganSachModel>>(nsMLNS);
            bhMuclucNgansachModels.Insert(0, new BhDmMucLucNganSachModel { IIDMLNS = Guid.Empty, SLNS = "", SMoTa = "Tất cả-" });
            SelectedMlnsId = bhMuclucNgansachModels[0].IIDMLNS;
        }

        public void OnExport()
        {
            IExportService exportService = (IExportService)ServiceProvider.GetService(typeof(IExportService));
            ILog logger = (ILog)ServiceProvider.GetService(typeof(ILog));
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                Dictionary<string, object> data = new Dictionary<string, object>();
                var listDMMLNS = _bhMucLucNganSachService.FindReportMLNSQuery(_namLamViec, SelectedMlnsId);
                listDMMLNS.Select(x => x.NamLamViec = _namLamViec).ToList();
                data.Add("Items", listDMMLNS);
                var xlsFile = exportService.Export<ReportMLNSQuery>(ExportFileName.RPT_DM_MLNS_BHXH, data);
                string fileNameWithoutExtension = "rpt_dm_mlns_bhxh_" + DateTime.Now.ToString("ddMMyyhhmmss");
                results.Add(new ExportResult(Description, fileNameWithoutExtension, null, xlsFile));
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    exportService.Open(result, ExportType.EXCEL);
                }
                else
                {
                    logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }
    }
}
