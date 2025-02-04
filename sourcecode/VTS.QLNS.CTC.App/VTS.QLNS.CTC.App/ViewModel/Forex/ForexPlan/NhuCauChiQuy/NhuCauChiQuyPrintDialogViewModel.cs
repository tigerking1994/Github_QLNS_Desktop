using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.NhuCauChiQuy
{
    public class NhuCauChiQuyPrintDialogViewModel : DialogViewModelBase<NhNhuCauChiQuyModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IExportService _exportService;
        private readonly string _typeChuky = TypeChuKy.RPT_NGOAIHOI_NHUCAU_CHIQUY;
        private readonly INhNhuCauChiQuyService _nhNhuCauChiQuyService;
        private readonly INhNhuCauChiQuyChiTietService _nhNhuCauChiQuyChiTietService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly INsDonViService _nsDonViService;
        private DmChuKy _dmChuKy;
        private readonly INhKhTongTheService _nhKhTongTheService;
        protected readonly INhThTongHopService _nhThTongHopService;


        public override string Title => "NHU CẦU CHI QUÝ";
        public override string Description => "Nhu cầu chi quý";
        public override Type ContentType => typeof(View.Forex.ForexPlan.NhuCauChiQuy.ForexNhuCauChiQuyPrintDilaog);

        public DmChuKyDialogViewModel _dmChuKyDialogViewModel { get; }

        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ExportCommand { get; }
        public Guid? iTiGia { get; set; }
        private string _txtTitleFirst;
        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set => SetProperty(ref _txtTitleFirst, value);
        }
        private ObservableCollection<ComboboxItem> _itemsDonViTinhUSD = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ItemsDonViTinhUSD
        {
            get => _itemsDonViTinhUSD;
            set => SetProperty(ref _itemsDonViTinhUSD, value);
        }

        private ComboboxItem _selectedDonViTinhUSD;
        public ComboboxItem SelectedDonViTinhUSD
        {
            get => _selectedDonViTinhUSD;
            set => SetProperty(ref _selectedDonViTinhUSD, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDonViTinhVND = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ItemsDonViTinhVND
        {
            get => _itemsDonViTinhVND;
            set => SetProperty(ref _itemsDonViTinhVND, value);
        }

        private ComboboxItem _selectedDonViTinhVND;
        public ComboboxItem SelectedDonViTinhVND
        {
            get => _selectedDonViTinhVND;
            set => SetProperty(ref _selectedDonViTinhVND, value);
        }
        public NhuCauChiQuyPrintDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IExportService exportService,
            INhNhuCauChiQuyService nhNhuCauChiQuyService,
            INhNhuCauChiQuyChiTietService nhNhuCauChiQuyChiTietService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            INhKhTongTheService nhKhTongTheService,
            INsDonViService nsDonViService,
            INhThTongHopService nhThTongHopService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _mapper = mapper;
            _dmChuKyService = dmChuKyService;
            _exportService = exportService;
            _nhNhuCauChiQuyChiTietService = nhNhuCauChiQuyChiTietService;
            _nhNhuCauChiQuyService = nhNhuCauChiQuyService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _nsDonViService = nsDonViService;
            _nhKhTongTheService = nhKhTongTheService;
            _nhThTongHopService = nhThTongHopService;
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            PrintReportCommand = new RelayCommand(obj => OnPrintReport(ExportType.PDF));
            ExportCommand = new RelayCommand(obj => OnPrintReport(ExportType.EXCEL));
            _dmChuKyDialogViewModel = dmChuKyDialogViewModel;
            loadDonViTinh();
        }

        public override void Init()
        {
            base.Init();
        }
        private void loadDonViTinh()
        {
            TxtTitleFirst = "Nhu cầu sử dụng kinh phí nguồn quỹ dự trữ ngoại hối";
            ItemsDonViTinhUSD.Add(new ComboboxItem() { DisplayItem = "USD", ValueItem = "1" });
            ItemsDonViTinhUSD.Add(new ComboboxItem() { DisplayItem = "Nghìn USD", ValueItem = "1000" });
            ItemsDonViTinhUSD.Add(new ComboboxItem() { DisplayItem = "Tỷ USD", ValueItem = "10000000" });

            ItemsDonViTinhVND.Add(new ComboboxItem() { DisplayItem = "VND", ValueItem = "1" });
            ItemsDonViTinhVND.Add(new ComboboxItem() { DisplayItem = "Nghìn VND", ValueItem = "1000" });
            ItemsDonViTinhVND.Add(new ComboboxItem() { DisplayItem = "Tỷ VND", ValueItem = "10000000" });

        }
        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            DmChuKy _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            _dmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            _dmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                /* TxtTitleFirst = chuKy != null ? (string.IsNullOrEmpty(chuKy.TieuDe1MoTa) ? TITLE_FIRST_DEFAULT_VALUE : chuKy.TieuDe1MoTa) : string.Empty;
                 TxtTitleSecond = chuKy != null ? (string.IsNullOrEmpty(chuKy.TieuDe2MoTa) ? TITLE_SECOND_DEFAULT_VALUE : chuKy.TieuDe2MoTa) : string.Empty;*/
            };
            _dmChuKyDialogViewModel.Init();
            _dmChuKyDialogViewModel.ShowDialog();
        }

        private void OnPrintReport(ExportType exportType)
        {
            if (!ValidateData()) return;

            int iDvUsd = Convert.ToInt32(SelectedDonViTinhUSD.ValueItem);
            int iDvVnd = Convert.ToInt32(SelectedDonViTinhVND.ValueItem);

            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int Stt = 0;
                    int SttCha = 0;
                    int SttChuongTrinh = 0;
                    Guid? idcha = null;
                    Guid? idchuongtrinh = null;

                    var data = _nhNhuCauChiQuyService.ReportNhuCauChiQuy(Model.Id);
                    List<NhNhuCauChiQuyBaoCaoModel> listChitiet = _mapper.Map<List<NhNhuCauChiQuyBaoCaoModel>>(data);
                    List<NhNhuCauChiQuyBaoCaoModel> ListData = new List<NhNhuCauChiQuyBaoCaoModel>();
                    // Tinhs lai data tong hop
                    var dataTongHopHandler = LoadDataTongHop();
                    foreach (var item in listChitiet)
                    {
                        if (item.ID_NhiemVuChi != idchuongtrinh && item.ID_NhiemVuChi != null)
                        {
                            SttChuongTrinh++;
                            SttCha = 0;
                            var dataTongHopNVC = dataTongHopHandler.Where(x => x.ID_NhiemVuChi.Equals(item.ID_NhiemVuChi));
                            NhNhuCauChiQuyBaoCaoModel DataCha = new NhNhuCauChiQuyBaoCaoModel();
                            List<NhNhuCauChiQuyBaoCaoModel> ListDataCha = listChitiet.Where(x => x.ID_NhiemVuChi == item.ID_NhiemVuChi).ToList();
                            
                            DataCha.GiaTriHopDongUSD = ListDataCha.Sum(x => x.GiaTriHopDongUSD);
                            DataCha.GiaTriHopDongVND = ListDataCha.Sum(x => x.GiaTriHopDongVND);
                            DataCha.GiaTriTongTheUSD = ListDataCha.Sum(x => x.GiaTriTongTheUSD);
                            DataCha.GiaTriBQPUSD = ListDataCha.Sum(x => x.GiaTriBQPUSD);
                            //DataCha.KinhPhiUSD = ListDataCha.Sum(x => x.KinhPhiUSD);
                            //DataCha.KinhPhiVND = ListDataCha.Sum(x => x.KinhPhiVND);
                            //DataCha.KinhPhiDaChiUSD = ListDataCha.Sum(x => x.KinhPhiDaChiUSD);
                            //DataCha.KinhPhiDaChiVND = ListDataCha.Sum(x => x.KinhPhiDaChiVND);
                            //DataCha.KinhPhiToYUSD = ListDataCha.Sum(x => x.KinhPhiToYUSD);
                            //DataCha.KinhPhiToYVND = ListDataCha.Sum(x => x.KinhPhiToYVND);
                            //DataCha.KinhPhiDaChiToYUSD = ListDataCha.Sum(x => x.KinhPhiDaChiToYUSD);
                            //DataCha.KinhPhiDaChiToYVND = ListDataCha.Sum(x => x.KinhPhiDaChiToYVND);

                            //--Tinh lai data tong hop--
                            DataCha.KinhPhiUSD = dataTongHopNVC.Sum(x => x.KinhPhiUSD);
                            DataCha.KinhPhiVND = dataTongHopNVC.Sum(x => x.KinhPhiVND);
                            DataCha.KinhPhiDaChiUSD = dataTongHopNVC.Sum(x => x.KinhPhiDaChiUSD);
                            DataCha.KinhPhiDaChiVND = dataTongHopNVC.Sum(x => x.KinhPhiDaChiVND);
                            DataCha.KinhPhiToYUSD = dataTongHopNVC.Sum(x => x.KinhPhiToYUSD);
                            DataCha.KinhPhiToYVND = dataTongHopNVC.Sum(x => x.KinhPhiToYVND);
                            DataCha.KinhPhiDaChiToYVND = dataTongHopNVC.Sum(x => x.KinhPhiDaChiToYVND);
                            DataCha.KinhPhiDaChiToYUSD = dataTongHopNVC.Sum(x => x.KinhPhiDaChiToYUSD);


                            DataCha.fChiNgoaiTeUSDTyGia = ListDataCha.Sum(x => x.fChiNgoaiTeUSDTyGia);
                            DataCha.fChiTrongNuocVNDTyGia = ListDataCha.Sum(x => x.fChiTrongNuocVNDTyGia);

                            DataCha.Noidungchi = item.sTenNhiemVuChi;
                            DataCha.IsHangCha = true;
                            DataCha.fChiNgoaiTeUSD = null;
                            DataCha.fChiTrongNuocVND = null;
                            DataCha.depth = convertLetter(SttChuongTrinh);
                            idchuongtrinh = item.ID_NhiemVuChi;
                            ListData.Add(DataCha);
                        }
                        if (item.iID_DonViID != idcha && item.iID_DonViID != null)
                        {
                            SttCha++;
                            Stt = 0;
                            NhNhuCauChiQuyBaoCaoModel DataCha = new NhNhuCauChiQuyBaoCaoModel();

                            List<NhNhuCauChiQuyBaoCaoModel> ListDataCha = listChitiet.Where(x => x.ID_NhiemVuChi == item.ID_NhiemVuChi && x.iID_DonViID == item.iID_DonViID).ToList();
                            var dataThDonvi = dataTongHopHandler.Where(x => x.ID_NhiemVuChi.Equals(item.ID_NhiemVuChi) && x.iID_DonViID.Equals(item.iID_DonViID));

                            DataCha.GiaTriHopDongUSD = ListDataCha.Sum(x => x.GiaTriHopDongUSD);
                            DataCha.GiaTriHopDongVND = ListDataCha.Sum(x => x.GiaTriHopDongVND);
                            DataCha.GiaTriTongTheUSD = ListDataCha.Sum(x => x.GiaTriTongTheUSD);
                            DataCha.GiaTriBQPUSD = ListDataCha.Sum(x => x.GiaTriBQPUSD);
                            //DataCha.KinhPhiUSD = ListDataCha.Sum(x => x.KinhPhiUSD);
                            //DataCha.KinhPhiVND = ListDataCha.Sum(x => x.KinhPhiVND);
                            //DataCha.KinhPhiDaChiUSD = ListDataCha.Sum(x => x.KinhPhiDaChiUSD);
                            //DataCha.KinhPhiDaChiVND = ListDataCha.Sum(x => x.KinhPhiDaChiVND);
                            //DataCha.KinhPhiToYUSD = ListDataCha.Sum(x => x.KinhPhiToYUSD);
                            //DataCha.KinhPhiToYVND = ListDataCha.Sum(x => x.KinhPhiToYVND);
                            //DataCha.KinhPhiDaChiToYUSD = ListDataCha.Sum(x => x.KinhPhiDaChiToYUSD);
                            //DataCha.KinhPhiDaChiToYVND = ListDataCha.Sum(x => x.KinhPhiDaChiToYVND);
                            //--Tinh lai data tong hop--
                            DataCha.KinhPhiUSD = dataThDonvi.Sum(x => x.KinhPhiUSD);
                            DataCha.KinhPhiVND = dataThDonvi.Sum(x => x.KinhPhiVND);
                            DataCha.KinhPhiDaChiUSD = dataThDonvi.Sum(x => x.KinhPhiDaChiUSD);
                            DataCha.KinhPhiDaChiVND = dataThDonvi.Sum(x => x.KinhPhiDaChiVND);
                            DataCha.KinhPhiToYUSD = dataThDonvi.Sum(x => x.KinhPhiToYUSD);
                            DataCha.KinhPhiToYVND = dataThDonvi.Sum(x => x.KinhPhiToYVND);
                            DataCha.KinhPhiDaChiToYVND = dataThDonvi.Sum(x => x.KinhPhiDaChiToYVND);
                            DataCha.KinhPhiDaChiToYUSD = dataThDonvi.Sum(x => x.KinhPhiDaChiToYUSD);
                            DataCha.fChiNgoaiTeUSDTyGia = ListDataCha.Sum(x => x.fChiNgoaiTeUSDTyGia);
                            DataCha.fChiTrongNuocVNDTyGia = ListDataCha.Sum(x => x.fChiTrongNuocVNDTyGia);

                            DataCha.Noidungchi = item.sTenDonvi;
                            DataCha.IsHangCha = true;
                            DataCha.fChiNgoaiTeUSD = null;
                            DataCha.fChiTrongNuocVND = null;
                            DataCha.depth = _nhNhuCauChiQuyService.GetSTTLAMA(SttCha);
                            idcha = item.iID_DonViID;
                            ListData.Add(DataCha);
                        }
                        Stt++;
                        item.depth = SttCha + "." + Stt;

                        if (item.iID_HopDongID.IsNullOrEmpty())
                        {
                            var dataThHopDong = dataTongHopHandler.Where(x => x.ID_NhiemVuChi.Equals(item.ID_NhiemVuChi) && x.iID_DuAnID.Equals(item.iID_DuAnID) && x.iID_DonViID.Equals(x.iID_DonViID) && x.iID_HopDongID.IsNullOrEmpty());
                            item.KinhPhiUSD = dataThHopDong.Sum(x => x.KinhPhiUSD);
                            item.KinhPhiVND = dataThHopDong.Sum(x => x.KinhPhiVND);
                            item.KinhPhiDaChiUSD = dataThHopDong.Sum(x => x.KinhPhiDaChiUSD);
                            item.KinhPhiDaChiVND = dataThHopDong.Sum(x => x.KinhPhiDaChiVND);
                            item.KinhPhiToYUSD = dataThHopDong.Sum(x => x.KinhPhiToYUSD);
                            item.KinhPhiToYVND = dataThHopDong.Sum(x => x.KinhPhiToYVND);
                            item.KinhPhiDaChiToYVND = dataThHopDong.Sum(x => x.KinhPhiDaChiToYVND);
                            item.KinhPhiDaChiToYUSD = dataThHopDong.Sum(x => x.KinhPhiDaChiToYUSD);
                        }
                        else
                        {
                            item.Noidungchi = item.sTenHopDong;
                            var dataThHopDong = dataTongHopHandler.Where(x => x.ID_NhiemVuChi.Equals(item.ID_NhiemVuChi) && x.iID_DonViID.Equals(item.iID_DonViID) && x.iID_HopDongID.Equals(x.iID_HopDongID));
                            item.KinhPhiUSD = dataThHopDong.Sum(x => x.KinhPhiUSD);
                            item.KinhPhiVND = dataThHopDong.Sum(x => x.KinhPhiVND);
                            item.KinhPhiDaChiUSD = dataThHopDong.Sum(x => x.KinhPhiDaChiUSD);
                            item.KinhPhiDaChiVND = dataThHopDong.Sum(x => x.KinhPhiDaChiVND);
                            item.KinhPhiToYUSD = dataThHopDong.Sum(x => x.KinhPhiToYUSD);
                            item.KinhPhiToYVND = dataThHopDong.Sum(x => x.KinhPhiToYVND);
                            item.KinhPhiDaChiToYVND = dataThHopDong.Sum(x => x.KinhPhiDaChiToYVND);
                            item.KinhPhiDaChiToYUSD = dataThHopDong.Sum(x => x.KinhPhiDaChiToYUSD);

                        }
                        ListData.Add(item);
                    }

                    foreach (var item in ListData)
                    {
                        item.GiaTriHopDongUSD = item.GiaTriHopDongUSD / iDvUsd;
                        item.GiaTriHopDongVND = item.GiaTriHopDongVND / iDvVnd;
                        item.GiaTriTongTheUSD = item.GiaTriTongTheUSD / iDvUsd;
                        item.GiaTriBQPUSD = item.GiaTriBQPUSD / iDvUsd;
                        item.KinhPhiUSD = item.KinhPhiUSD / iDvUsd;
                        item.KinhPhiVND = item.KinhPhiVND / iDvVnd;
                        item.KinhPhiDaChiUSD = item.KinhPhiDaChiUSD / iDvUsd;
                        item.KinhPhiDaChiVND = item.KinhPhiDaChiVND / iDvVnd;
                        item.KinhPhiToYUSD = item.KinhPhiToYUSD / iDvUsd;
                        item.KinhPhiToYVND = item.KinhPhiToYVND / iDvVnd;
                        item.KinhPhiDaChiToYUSD = item.KinhPhiDaChiToYUSD / iDvUsd;
                        item.KinhPhiDaChiToYVND = item.KinhPhiDaChiToYVND / iDvVnd;
                        item.fChiNgoaiTeUSDTyGia = item.fChiNgoaiTeUSDTyGia / iDvUsd;
                        item.fChiTrongNuocVNDTyGia = item.fChiTrongNuocVNDTyGia / iDvVnd;
                    }

                    var DonVi = _nsDonViService.FindAll().FirstOrDefault(x => x.Id == Model.IIdDonViId);
                    var sDonvi = "";
                    if (DonVi != null)
                    {
                        sDonvi = DonVi.MaTenDonVi;
                    }

                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NGOAIHOI_NHUCAU_CHIQUY) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                    Dictionary<string, object> dataReport = new Dictionary<string, object>();
                    dataReport.Add("QuyNam", string.Format("Quý {0} Năm {1}", Model.IQuy, Model.INamKeHoach));
                    dataReport.Add("dt", ListData);
                    dataReport.Add("DonVi", sDonvi);
                    dataReport.Add("dvt", "1");
                    dataReport.Add("To", "1");
                    dataReport.Add("DonViCapTren", _sessionService.Current.TenDonViTrucThuocReportHeader);
                    dataReport.Add("DonViToiMat", _sessionService.Current.TenDonViReportHeader);
                    dataReport.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa.ToUpper() : string.Empty);
                    dataReport.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa.ToUpper() : string.Empty);
                    dataReport.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa.ToUpper() : string.Empty);
                    dataReport.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    dataReport.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    dataReport.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    dataReport.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    dataReport.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    dataReport.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                    string sTitle = TxtTitleFirst + "";
                    if (sTitle.Trim() == "") sTitle = "Nhu cầu sử dụng kinh phí nguồn quỹ dự trữ ngoại hối";
                    dataReport.Add("Title", sTitle.ToUpper());

                    var nhuCauChiQuy = _nhNhuCauChiQuyService.FindById(Model.Id);
                    var keHoachTongThe = new NhKhTongThe();
                    if (nhuCauChiQuy.IIdKHTongTheID + "" != "")
                    {
                        keHoachTongThe = _nhKhTongTheService.Find((Guid)nhuCauChiQuy.IIdKHTongTheID);
                    }
                    dataReport.Add("sSoTTCP", keHoachTongThe.SSoKeHoachTtcp);
                    dataReport.Add("sSoBQP", keHoachTongThe.SSoKeHoachBqp);
                    dataReport.Add("dvUsd", SelectedDonViTinhUSD.DisplayItem);
                    dataReport.Add("dvVnd", SelectedDonViTinhVND.DisplayItem);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_NGOAIHOI, ExportFileName.RPT_NH_NHUCAU_CHIQUY);
                    string fileNamePrefix = string.Format("rpt_Nhucauchiquy_BaoCao");
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<NhNhuCauChiQuyBaoCaoModel>(templateFileName, dataReport);
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
                _logger.Error(ex.Message);
            }
        }
        private string convertLetter(int input)
        {
            StringBuilder res = new StringBuilder((input - 1).ToString());
            for (int j = 0; j < res.Length; j++)
                res[j] += (char)(17); // '0' is 48, 'A' is 65
            return res.ToString();
        }

        private bool ValidateData()
        {
            if (SelectedDonViTinhUSD == null)
            {
                MessageBoxHelper.Error("Chọn đơn vị tính USD");
                return false;
            }
            if (SelectedDonViTinhVND == null)
            {
                MessageBoxHelper.Error("Chọn đơn vị tính VND");
                return false;
            }
            return true;
        }

        private List<NhNhuCauChiQuyBaoCaoModel> LoadDataTongHop()
        {
            List<NHTHTongHop> data = new List<NHTHTongHop>();
            var lstSMaNguon = new List<string> { NHConstants.MA_TH_BC_NCCQ };
            var lstSMaNguonQT = new List<string> { NHConstants.MA_TH_BCTH_NS_QUY };
            var predicate = PredicateBuilder.True<NHTHTongHop>();
            //predicate = predicate.And(x => x.INamKeHoach == Model.INamKeHoach);
            predicate = predicate.And(x => (lstSMaNguon.Contains(x.SMaNguon) || lstSMaNguon.Contains(x.SMaNguonCha)) && x.INamKeHoach == Model.INamKeHoach - 1);
            predicate = predicate.Or(x => (lstSMaNguonQT.Contains(x.SMaNguon) || lstSMaNguonQT.Contains(x.SMaNguonCha)) && x.INamKeHoach == Model.INamKeHoach && x.IQuyKeHoach == Model.IQuy);

            return CalculateDataTongHop(_nhThTongHopService.FindByCondition(predicate));
        }

        private List<NhNhuCauChiQuyBaoCaoModel> CalculateDataTongHop(IEnumerable<NHTHTongHop> lstData)
        {
            List<NhNhuCauChiQuyBaoCaoModel> results = new List<NhNhuCauChiQuyBaoCaoModel>();
            if (lstData.Any())
            {
                var listHopDong = lstData.Where(x => x.IIdHopDongId != null).GroupBy(g => g.IIdHopDongId).Select(x => x.First());
                var listDuAn = lstData.Where(x => x.IIdDuAnId != null &&
                                !listHopDong.Where(w => w.IIdDuAnId != null || w.IIdDuAnId != Guid.Empty).Select(s => s.IIdDuAnId).Contains(x.IIdDuAnId))
                                .GroupBy(g => g.IIdDuAnId).Select(s => s.First());

                foreach (var item in listHopDong)
                {
                    var dataHandlerNccq = new NhNhuCauChiQuyBaoCaoModel();
                    var dataHopDong = lstData.Where(x => x.IIdHopDongId.Equals(item.IIdHopDongId));
                    //KinhPhiVND8- nguon 306(n-1)
                    var dataCol8 = dataHopDong.Where(x => x.SMaNguon == NhTongHopConstants.MA_306 || x.SMaNguonCha == NhTongHopConstants.MA_306);
                    var dataUsdCol8 = dataCol8.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriUsd) - dataCol8.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriUsd);
                    var dataVndCol8 = dataCol8.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriVnd) - dataCol8.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriVnd);

                    //KinhPhiDaChiUSD10- nguon 308(n-1)
                    var dataCol10 = dataHopDong.Where(x => x.SMaNguon == NhTongHopConstants.MA_308 || x.SMaNguonCha == NhTongHopConstants.MA_308);
                    var dataUsdCol10 = dataCol10.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriUsd) - dataCol10.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriUsd);
                    var dataVndCol10 = dataCol10.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriVnd) - dataCol10.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriVnd);

                    //KinhPhiToYUSD12 <-> nguon(101,102,111,112,121,122) - (131,132)(quy, nam ke hoach)
                    var lstSmaNguonPlus = NHConstants.MA_TH_BC_NCCQ_KPDC.Split(StringUtils.COMMA).Select(x => x.Trim());
                    var dataPlus12 = dataHopDong.Where(x => (lstSmaNguonPlus.Contains(x.SMaNguon) || lstSmaNguonPlus.Contains(x.SMaNguonCha)));
                    var dataPlusUsd12 = dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataPlusVnd12 = dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    //data minus
                    var lstSmaNguonMinus = NHConstants.MA_TH_BC_NCCQ.Split(StringUtils.COMMA).Select(x => x.Trim());
                    var dataMinus = dataHopDong.Where(x => (lstSmaNguonMinus.Contains(x.SMaNguon) || lstSmaNguonMinus.Contains(x.SMaNguonCha)));
                    var dataMinusUsd = dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataMinusVnd = dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);

                    var dataUsdCol12 = dataPlusUsd12 - dataMinusUsd;
                    var dataVndCol12 = dataPlusVnd12 - dataMinusVnd;
                    //KinhPhiDaChiToYUSD14 <-> nguon(141,142,111,112,121,122) - (131,132)(quy, nam ke hoach)
                    lstSmaNguonPlus = NHConstants.MA_TH_BC_NCCQ_KPC.Split(StringUtils.COMMA).Select(x => x.Trim());
                    var dataPlus14 = dataHopDong.Where(x => (lstSmaNguonPlus.Contains(x.SMaNguon) || lstSmaNguonPlus.Contains(x.SMaNguonCha)));
                    var dataPlusUsd14 = dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataPlusVnd14 = dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    var dataUsdCol14 = dataPlusUsd14 - dataMinusUsd;
                    var dataVndCol14 = dataPlusVnd14 - dataMinusVnd;

                    dataHandlerNccq.KinhPhiUSD = dataUsdCol8;
                    dataHandlerNccq.KinhPhiVND = dataVndCol8;
                    dataHandlerNccq.KinhPhiDaChiUSD = dataUsdCol10;
                    dataHandlerNccq.KinhPhiDaChiVND = dataVndCol10;
                    dataHandlerNccq.KinhPhiToYUSD = dataUsdCol12;
                    dataHandlerNccq.KinhPhiToYVND = dataVndCol12;
                    dataHandlerNccq.KinhPhiDaChiToYUSD = dataUsdCol14;
                    dataHandlerNccq.KinhPhiDaChiToYVND = dataVndCol14;

                    dataHandlerNccq.Id = item.Id;
                    dataHandlerNccq.iID_HopDongID = item.IIdHopDongId;
                    dataHandlerNccq.iID_DuAnID = item.IIdDuAnId;
                    dataHandlerNccq.ID_NhiemVuChi = item.IIDKHTTNhiemVuChiID ?? Guid.Empty;
                    dataHandlerNccq.iID_KHCTBQP_NhiemVuChiID = item.IIDKHTTNhiemVuChiID;
                    dataHandlerNccq.iID_NhuCauChiQuyID = item.IIdChungTu;
                    dataHandlerNccq.iID_DonViID = item.IIdDonVi ?? Guid.Empty;
                    //dataHandlerQtnd.ILoaiNoiDungChi = item.lo;


                    //dataHandlerQtnd.
                    results.Add(dataHandlerNccq);
                }

                foreach (var item in listDuAn)
                {
                    var dataHandlerNccq = new NhNhuCauChiQuyBaoCaoModel();
                    var dataDuAn = lstData.Where(x => x.IIdDuAnId.Equals(item.IIdDuAnId));
                    //KinhPhiVND8- nguon 306(n-1)
                    var dataCol8 = dataDuAn.Where(x => x.SMaNguon == NhTongHopConstants.MA_306 || x.SMaNguonCha == NhTongHopConstants.MA_306);
                    var dataUsdCol8 = dataCol8.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriUsd) - dataCol8.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriUsd);
                    var dataVndCol8 = dataCol8.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriVnd) - dataCol8.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriVnd);

                    //KinhPhiDaChiUSD10- nguon 308(n-1)
                    var dataCol10 = dataDuAn.Where(x => x.SMaNguon == NhTongHopConstants.MA_308 || x.SMaNguonCha == NhTongHopConstants.MA_308);
                    var dataUsdCol10 = dataCol10.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriUsd) - dataCol10.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriUsd);
                    var dataVndCol10 = dataCol10.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriVnd) - dataCol10.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriVnd);

                    //KinhPhiToYUSD12 <-> nguon(101,102,111,112,121,122) - (131,132)(quy, nam ke hoach)
                    var lstSmaNguonPlus = NHConstants.MA_TH_BC_NCCQ_KPDC.Split(StringUtils.COMMA).Select(x => x.Trim());
                    var dataPlus12 = dataDuAn.Where(x => (lstSmaNguonPlus.Contains(x.SMaNguon) || lstSmaNguonPlus.Contains(x.SMaNguonCha)));
                    var dataPlusUsd12 = dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataPlusVnd12 = dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    //data minus
                    var lstSmaNguonMinus = NHConstants.MA_TH_BC_NCCQ_MINUS.Split(StringUtils.COMMA).Select(x => x.Trim());
                    var dataMinus = dataDuAn.Where(x => (lstSmaNguonMinus.Contains(x.SMaNguon) || lstSmaNguonMinus.Contains(x.SMaNguonCha)));
                    var dataMinusUsd = dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataMinusVnd = dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);

                    var dataUsdCol12 = dataPlusUsd12 - dataMinusUsd;
                    var dataVndCol12 = dataPlusVnd12 - dataMinusVnd;
                    //KinhPhiDaChiToYUSD14 <-> nguon(141,142,111,112,121,122) - (131,132)(quy, nam ke hoach)
                    lstSmaNguonPlus = NHConstants.MA_TH_BC_NCCQ_KPC.Split(StringUtils.COMMA).Select(x => x.Trim());
                    var dataPlus14 = dataDuAn.Where(x => (lstSmaNguonPlus.Contains(x.SMaNguon) || lstSmaNguonPlus.Contains(x.SMaNguonCha)));
                    var dataPlusUsd14 = dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataPlusVnd14 = dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    var dataUsdCol14 = dataPlusUsd14 - dataMinusUsd;
                    var dataVndCol14 = dataPlusVnd14 - dataMinusVnd;

                    dataHandlerNccq.KinhPhiUSD = dataUsdCol8;
                    dataHandlerNccq.KinhPhiVND = dataVndCol8;
                    dataHandlerNccq.KinhPhiDaChiUSD = dataUsdCol10;
                    dataHandlerNccq.KinhPhiDaChiVND = dataVndCol10;
                    dataHandlerNccq.KinhPhiToYUSD = dataUsdCol12;
                    dataHandlerNccq.KinhPhiToYVND = dataVndCol12;
                    dataHandlerNccq.KinhPhiDaChiToYUSD = dataUsdCol14;
                    dataHandlerNccq.KinhPhiDaChiToYVND = dataVndCol14;

                    dataHandlerNccq.Id = item.Id;
                    dataHandlerNccq.iID_HopDongID = item.IIdHopDongId;
                    dataHandlerNccq.iID_DuAnID = item.IIdDuAnId;
                    dataHandlerNccq.ID_NhiemVuChi = item.IIDKHTTNhiemVuChiID ?? Guid.Empty;
                    dataHandlerNccq.iID_KHCTBQP_NhiemVuChiID = item.IIDKHTTNhiemVuChiID;
                    dataHandlerNccq.iID_NhuCauChiQuyID = item.IIdChungTu;
                    dataHandlerNccq.iID_DonViID = item.IIdDonVi ?? Guid.Empty;

                    //dataHandlerQtnd.
                    results.Add(dataHandlerNccq);

                }
            }
            return results;
        }
    }
}
