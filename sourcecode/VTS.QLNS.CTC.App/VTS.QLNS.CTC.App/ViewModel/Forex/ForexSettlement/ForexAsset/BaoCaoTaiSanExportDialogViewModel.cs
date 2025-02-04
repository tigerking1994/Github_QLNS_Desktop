using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ForexAsset
{
    public class BaoCaoTaiSanExportDialogViewModel : DialogViewModelBase<NhQtTaiSanModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INhDaChenhLechTiGiaService _service;
        private readonly IExportService _exportService;
        private readonly string _typeChuky = TypeChuKy.RPT_NH_CHENHLECH_TIGIA_HOIDOAI;
        private readonly string TITLE_FIRST_DEFAULT_VALUE = "BÁO CÁO TÀI SẢN HÌNH THÀNH THEO HỢP ĐỒNG";
        public override string Title => "Báo cáo tài sản hình thành theo hợp đồng";
        public override string Description => "Báo cáo tài sản hình thành theo hợp đồng";
        public override Type ContentType => typeof(View.Forex.ForexSettlement.ForexAsset.BaoCaoTaiSanExportDialodg);      
        private string _tieuDe1;
        public string TieuDe1
        {
            get => _tieuDe1;
            set => SetProperty(ref _tieuDe1, value);
        }

        private string _tieuDe2;
        public string TieuDe2
        {
            get => _tieuDe2;
            set => SetProperty(ref _tieuDe2, value);
        }

        private string _tieuDe3;
        public string TieuDe3
        {
            get => _tieuDe3;
            set => SetProperty(ref _tieuDe3, value);
        }
        public List<NhQtTaiSanModel> ItemsTaiSan { get;set; }
        public List<NhQtTaiSanModel> ItemsThongKeTaiSan { get;set; }

        private DmChuKyDialogViewModel DmChuKyDialogViewModel { get; }

        public RelayCommand ExportExcelCommand { get; set; }
        public RelayCommand ExportPdfCommand { get; set; }
        public RelayCommand ConfigSignCommand { get; }

        public BaoCaoTaiSanExportDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            IDmChuKyService dmChuKyService,
            IExportService exportService,
            INhDaChenhLechTiGiaService service,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _dmChuKyService = dmChuKyService;
            _exportService = exportService;
            _service = service;

            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportExcelCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportPdfCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            LoadTitleFirst();
        }

        private void LoadTitleFirst()
        {
            TieuDe1 = TITLE_FIRST_DEFAULT_VALUE;
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(1, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("sTenDonViCapTren", _sessionService.Current.TenDonViTrucThuocReportHeader);
                    data.Add("sTenDonViCapDuoi", _sessionService.Current.TenDonViReportHeader);
                    data.Add("TieuDe1", TieuDe1);
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    AddChuKy(data, _typeChuky);
                    data.Add("dt", ItemsTaiSan);
                    data.Add("ItemsThongKe", ItemsThongKeTaiSan);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_NH_QT, ExportFileName.PRT_NH_BAOCAOTAISAN);
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<NhQtTaiSanModel>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

        }
        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            DmChuKy _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TieuDe1 = chuKy.TieuDe1MoTa.IsEmpty("") ? TITLE_FIRST_DEFAULT_VALUE : chuKy.TieuDe1MoTa;
                TieuDe2 = chuKy.TieuDe2MoTa;
                TieuDe3 = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            // add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy != null ? dmChyKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ChucDanh1", dmChyKy != null ? dmChyKy.ChucDanh1MoTa : string.Empty);
            data.Add("Ten1", dmChyKy != null ? dmChyKy.Ten1MoTa : string.Empty);
            data.Add("ThuaLenh2", dmChyKy != null ? dmChyKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ChucDanh2", dmChyKy != null ? dmChyKy.ChucDanh2MoTa : string.Empty);
            data.Add("Ten2", dmChyKy != null ? dmChyKy.Ten2MoTa : string.Empty);
            data.Add("ThuaLenh3", dmChyKy != null ? dmChyKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh3", dmChyKy != null ? dmChyKy.ChucDanh3MoTa : string.Empty);
            data.Add("Ten3", dmChyKy != null ? dmChyKy.Ten3MoTa : string.Empty);
        }

    }



}
