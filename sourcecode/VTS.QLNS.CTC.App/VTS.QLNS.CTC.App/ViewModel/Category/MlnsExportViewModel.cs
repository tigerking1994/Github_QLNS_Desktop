using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class MlnsExportViewModel : ViewModelBase
    {
        public override string Name => "DANH MỤC";
        public override Type ContentType => typeof(View.Category.MlnsExportView);
        public override PackIconKind IconKind => PackIconKind.ViewList;

        public IMapper Mapper { get; set; }
        public ISessionService sessionService { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        private MucLucNganSachService _nsMucLucNganSachService;
        private readonly ILog _logger;

        private int _namLamViec;

        public Guid SelectedMlnsId { get; set; }

        public ObservableCollection<NsMuclucNgansachModel> nsMuclucNgansachModels { get; set; }

        public RelayCommand ExportCommand { get; }

        public MlnsExportViewModel(MucLucNganSachService nsMucLucNganSachService)
        {
            _nsMucLucNganSachService = nsMucLucNganSachService;
            ExportCommand = new RelayCommand(obj => OnExport());
        }

        public override void Init()
        {
            base.Init();
            _namLamViec = sessionService.Current.YearOfWork;
            MarginRequirement = new System.Windows.Thickness(0);
            ObservableCollection<NsMucLucNganSach> nsMLNS = new ObservableCollection<NsMucLucNganSach>(_nsMucLucNganSachService.FindByIsHangCha(_namLamViec, true).ToList());
            nsMuclucNgansachModels = Mapper.Map<ObservableCollection<NsMucLucNganSach>, ObservableCollection<NsMuclucNgansachModel>>(nsMLNS);
            nsMuclucNgansachModels.Insert(0, new NsMuclucNgansachModel { MlnsId = Guid.Empty, Lns = "", MoTa = "-Tất cả--" });
            SelectedMlnsId = nsMuclucNgansachModels[0].MlnsId;
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
                var listDMMLNS = _nsMucLucNganSachService.FindReportMLNSQuery(_namLamViec, SelectedMlnsId);
                listDMMLNS.Select(x => x.NamLamViec = _namLamViec).ToList();
                data.Add("Items", listDMMLNS);
                var xlsFile = exportService.Export<ReportMLNSQuery>(ExportFileName.RPT_DM_MLNS, data);
                string fileNameWithoutExtension = "rpt_dm_mlns_" + DateTime.Now.ToString("ddMMyyhhmmss");
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
