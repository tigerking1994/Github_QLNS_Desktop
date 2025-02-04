using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Estimate.DivisionEstimate.ExportReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate.ExportReport
{
    public class ExportJsonDivisionEstimateViewModel : DialogViewModelBase<DtChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private IDanhMucService _danhMucService;
        private INsDonViService _donViService;
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private IExportService _exportService;
        private readonly INsDtNhanPhanBoMapService _dtChungTuMapService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDtChungTuService _estimationService;

        private List<NsMucLucNganSach> _mucLucNganSachs;
        public ObservableCollection<DtChungTuModel> Items;
        public override Type ContentType => typeof(ExportJsonDivisionEstimate);

        private Dictionary<string, object> _donViItems;

        public Dictionary<string, object> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private bool _isProcessReport;
        public bool IsProcessReport
        {
            get => _isProcessReport;
            set => SetProperty(ref _isProcessReport, value);
        }

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        private Dictionary<string, object> _selectedDonViItems;
        public Dictionary<string, object> SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                SetProperty(ref _selectedDonViItems, value);
            }
        }

        public List<NsDtChungTu> ListChungTu { get; set; }

        public RelayCommand ExportCommand { get; }

        public ExportJsonDivisionEstimateViewModel(ILog logger,
                                               IMapper mapper,
                                               ISessionService sessionService,
                                               IDanhMucService danhMucService,
                                               INsDonViService donViService,
                                               INsQtChungTuChiTietService chungTuChiTietService,
                                               INsMucLucNganSachService mucLucNganSachService,
                                               IExportService exportService,
                                               INsDtNhanPhanBoMapService dtChungTuMapService,
                                               INsDtChungTuChiTietService dtChungTuChiTietService,
                                               INsDtChungTuService estimationService)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _donViService = donViService;
            _chungTuChiTietService = chungTuChiTietService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;

            _dtChungTuMapService = dtChungTuMapService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _estimationService = estimationService;

            ExportCommand = new RelayCommand(obj => OnExportJsonData());
        }

        private void OnExportJsonData()
        {
            ListChungTu.ForEach(x =>
            {
                x.ListDetailChiTiet = x.ListDetailChiTiet.Where(x => SelectedDonViItems.ContainsValue(x.IIdMaDonVi)).ToList();
            });
            _exportService.OpenJson(ListChungTu);
        }


        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            GetDataDonVi();
        }

        private void GetDataDonVi()
        {
            var idDonVis = ListChungTu.Select(x => x.SDsidMaDonVi.Split(",")).SelectMany(x => x).Distinct();
            var listDonVi = _donViService.FindByListIdDonVi(idDonVis, _sessionService.Current.YearOfWork).ToList();
            DonViItems = listDonVi.ToDictionary(x => $"{x.IIDMaDonVi} - {x.TenDonVi}", x => x.IIDMaDonVi as object);
        }
    }
}
