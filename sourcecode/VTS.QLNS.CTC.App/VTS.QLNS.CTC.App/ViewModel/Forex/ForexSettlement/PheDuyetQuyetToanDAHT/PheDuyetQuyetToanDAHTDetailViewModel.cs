using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.PheDuyetQuyetToanDAHT;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.QuyetToanNienDo;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.PheDuyetQuyetToanDAHT
{
    public class PheDuyetQuyetToanDAHTDetailViewModel : DetailViewModelBase<NhQtPheDuyetQuyetToanDAHTModel, NhQtPheDuyetQuyetToanDAHTChiTietModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhQtPheDuyetQuyetToanDAHTService _service;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly IExportService _exportService;
        private readonly INhThTongHopService _nhThTongHopService;

        private SessionInfo _sessionInfo;
        private List<NsMucLucNganSach> _rootDataMlns;

        public override string Name => "Phê duyệt quyết toán dự án hoàn thành";
        public override string Title => "Chi tiết phê duyệt quyết toán dự án hoàn thành";
        public override string Description => "Chi tiết phê duyệt quyết toán dự án hoàn thành";
        public override Type ContentType => typeof(PheDuyetQuyetToanDAHTDetail);
        public bool IsDetail { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();
        public int CountGiaiDoanKeHoach { get; set; }
        public int CountGiaiDoanKinhPhi { get; set; }

        public int CountGiaiDoanQuyetToan { get; set; }


        public string GiaiDoan1 { get; set; }
        public string GiaiDoan2 { get; set; }
        public string GiaiDoan3 { get; set; }
        public string GiaiDoan4 { get; set; }
        public string GiaiDoan5 { get; set; }
        public string GiaiDoan6 { get; set; }
        public string GiaiDoan7 { get; set; }
        public string GiaiDoan8 { get; set; }
        public string GiaiDoan9 { get; set; }
        public string GiaiDoan10 { get; set; }

        public bool VGiaiDoan1 => GiaiDoan1 != null;
        public bool VGiaiDoan2 => GiaiDoan2 != null;
        public bool VGiaiDoan3 => GiaiDoan3 != null;
        public bool VGiaiDoan4 => GiaiDoan4 != null;
        public bool VGiaiDoan5 => GiaiDoan5 != null;
        public bool VGiaiDoan6 => GiaiDoan6 != null;
        public bool VGiaiDoan7 => GiaiDoan7 != null;
        public bool VGiaiDoan8 => GiaiDoan8 != null;
        public bool VGiaiDoan9 => GiaiDoan9 != null;
        public bool VGiaiDoan10 => GiaiDoan10 != null;

        public string Header { get; set; }
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Header3 { get; set; }
        public string Header4 { get; set; }
        public string Header5 { get; set; }
        public string Header6 { get; set; }
        public string Header7 { get; set; }
        public string Header8 { get; set; }
        public string Header9 { get; set; }
        public string Header10 { get; set; }

        public string HeaderKPUSD { get; set; }
        public string HeaderKPUSD1 { get; set; }
        public string HeaderKPUSD2 { get; set; }
        public string HeaderKPUSD3 { get; set; }
        public string HeaderKPUSD4 { get; set; }
        public string HeaderKPUSD5 { get; set; }
        public string HeaderKPUSD6 { get; set; }
        public string HeaderKPUSD7 { get; set; }
        public string HeaderKPUSD8 { get; set; }
        public string HeaderKPUSD9 { get; set; }
        public string HeaderKPUSD10 { get; set; }

        public string HeaderKPVND { get; set; }
        public string HeaderKPVND1 { get; set; }
        public string HeaderKPVND2 { get; set; }
        public string HeaderKPVND3 { get; set; }
        public string HeaderKPVND4 { get; set; }
        public string HeaderKPVND5 { get; set; }
        public string HeaderKPVND6 { get; set; }
        public string HeaderKPVND7 { get; set; }
        public string HeaderKPVND8 { get; set; }
        public string HeaderKPVND9 { get; set; }
        public string HeaderKPVND10 { get; set; }

        public string HeaderQTUSD { get; set; }
        public string HeaderQTUSD1 { get; set; }
        public string HeaderQTUSD2 { get; set; }
        public string HeaderQTUSD3 { get; set; }
        public string HeaderQTUSD4 { get; set; }
        public string HeaderQTUSD5 { get; set; }
        public string HeaderQTUSD6 { get; set; }
        public string HeaderQTUSD7 { get; set; }
        public string HeaderQTUSD8 { get; set; }
        public string HeaderQTUSD9 { get; set; }
        public string HeaderQTUSD10 { get; set; }

        public string HeaderQTVND { get; set; }
        public string HeaderQTVND1 { get; set; }
        public string HeaderQTVND2 { get; set; }
        public string HeaderQTVND3 { get; set; }
        public string HeaderQTVND4 { get; set; }
        public string HeaderQTVND5 { get; set; }
        public string HeaderQTVND6 { get; set; }
        public string HeaderQTVND7 { get; set; }
        public string HeaderQTVND8 { get; set; }
        public string HeaderQTVND9 { get; set; }
        public string HeaderQTVND10 { get; set; }

        public string HeaderSS1 { get; set; }
        public string HeaderSS2 { get; set; }
        public string HeaderTN1 { get; set; }
        public string HeaderTN2 { get; set; }

        public RelayCommand PrintCommand { get; }

        public PheDuyetQuyetToanDAHTDetailViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INhQtPheDuyetQuyetToanDAHTService service,
            INsMucLucNganSachService nsMucLucNganSachService,
            INhDaHopDongService nhDaHopDongService,
            IExportService exportService,
            INhThTongHopService nhThTongHopService)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _service = service;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _nhDaHopDongService = nhDaHopDongService;
            _exportService = exportService;
            _nhThTongHopService = nhThTongHopService;

            //PrintCommand = new RelayCommand(obj => OnPrint((ReportNhQuyetToanNienDoEnum)obj));
        }

        public override void Init()
        {
            //LoadMucLucNganSach();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            Items = new ObservableCollection<NhQtPheDuyetQuyetToanDAHTChiTietModel>();
            //var data = new List<NhQtPheDuyetQuyetToanDAHTChiTietQuery>() {
            //new NhQtPheDuyetQuyetToanDAHTChiTietQuery()
            //{
            //    FHopDongUsd=41231,
            //    FKeHoachTTCPUsd=13123,
            //    FSoSanhKinhPhiUsd=412312,
            //    FKinhPhiDuocCapTongUsd=5234532,
            //    INamBaoCaoDen=2010,
            //    INamBaoCaoTu=2000,
            //    FKinhPhiDuocCapTongVnd=53243,
            //    FQuyetToanDuocDuyetTongUsd=53452,
            //    FQuyetToanDuocDuyetTongVnd=53452,
            //    FSoSanhKinhPhiVnd=53452,
            //    FHopDongVnd=52453,
            //    FThuaTraNSNNUsd=425326,
            //    FThuaTraNSNNVnd=5145325,
            //},
            //new NhQtPheDuyetQuyetToanDAHTChiTietQuery()
            //{
            //    FHopDongUsd=52345,
            //    FKeHoachTTCPUsd=13126123,
            //    FSoSanhKinhPhiUsd=4125312,
            //    FKinhPhiDuocCapTongUsd=5236234532,
            //    INamBaoCaoDen=2022,
            //    INamBaoCaoTu=2010,
            //    FKinhPhiDuocCapTongVnd=535236243,
            //    FQuyetToanDuocDuyetTongUsd=512343452,
            //    FQuyetToanDuocDuyetTongVnd=512343452,
            //    FSoSanhKinhPhiVnd=5361452,
            //    FHopDongVnd=52423453,
            //    FThuaTraNSNNUsd=42525326,
            //    FThuaTraNSNNVnd=514245325,
            //}
            //};
            var data = new List<NhQtPheDuyetQuyetToanDAHTChiTietQuery>();
            var dataDetail = _service.GetDataDetail(null, null, null, Model.Id).ToList();
            var isAdd = false;
            if (dataDetail.Any())
            {
                data = dataDetail;
            }
            else
            {
                data = _service.GetDataCreate(Model.INamBaoCaoTu ?? 0, Model.INamBaoCaoDen ?? 0, Model.IIdDonViId,null,null).ToList();
                isAdd = true;

            }
            var returnData = getListDetailChiTiet(data, isAdd);

            var listGiaiDoan = data.Select(x => new
            {
                x.INamBaoCaoTu,
                x.INamBaoCaoDen
            }).Where(x => x.INamBaoCaoTu != null && x.INamBaoCaoDen != null).OrderBy(x => x.INamBaoCaoTu).Distinct().ToList();



            for (var i = 0; i < listGiaiDoan.Count(); i++)
            {
                GiaiDoan1 = i == 0 ? listGiaiDoan[0].INamBaoCaoTu.ToString() + " - " + listGiaiDoan[0].INamBaoCaoDen.ToString() : GiaiDoan1 != null ? GiaiDoan1 : null;
                GiaiDoan2 = i == 1 ? listGiaiDoan[1].INamBaoCaoTu.ToString() + " - " + listGiaiDoan[1].INamBaoCaoDen.ToString() : GiaiDoan2 != null ? GiaiDoan2 : null;
                GiaiDoan3 = i == 2 ? listGiaiDoan[2].INamBaoCaoTu.ToString() + " - " + listGiaiDoan[2].INamBaoCaoDen.ToString() : GiaiDoan3 != null ? GiaiDoan3 : null;
                GiaiDoan4 = i == 3 ? listGiaiDoan[3].INamBaoCaoTu.ToString() + " - " + listGiaiDoan[3].INamBaoCaoDen.ToString() : GiaiDoan4 != null ? GiaiDoan4 : null;
                GiaiDoan5 = i == 4 ? listGiaiDoan[4].INamBaoCaoTu.ToString() + " - " + listGiaiDoan[4].INamBaoCaoDen.ToString() : GiaiDoan5 != null ? GiaiDoan5 : null;
                GiaiDoan6 = i == 5 ? listGiaiDoan[5].INamBaoCaoTu.ToString() + " - " + listGiaiDoan[5].INamBaoCaoDen.ToString() : GiaiDoan6 != null ? GiaiDoan6 : null;
                GiaiDoan7 = i == 6 ? listGiaiDoan[6].INamBaoCaoTu.ToString() + " - " + listGiaiDoan[6].INamBaoCaoDen.ToString() : GiaiDoan7 != null ? GiaiDoan7 : null;
                GiaiDoan8 = i == 7 ? listGiaiDoan[7].INamBaoCaoTu.ToString() + " - " + listGiaiDoan[7].INamBaoCaoDen.ToString() : GiaiDoan8 != null ? GiaiDoan8 : null;
                GiaiDoan9 = i == 8 ? listGiaiDoan[8].INamBaoCaoTu.ToString() + " - " + listGiaiDoan[8].INamBaoCaoDen.ToString() : GiaiDoan9 != null ? GiaiDoan9 : null;
                GiaiDoan10 = i == 9 ? listGiaiDoan[9].INamBaoCaoTu.ToString() + " - " + listGiaiDoan[9].INamBaoCaoDen.ToString() : GiaiDoan10 != null ? GiaiDoan10 : null;
                Header1 = i == 0 ? (4 + i).ToString() : Header1 != null ? Header1 : null;
                Header2 = i == 1 ? (4 + i).ToString() : Header2 != null ? Header2 : null;
                Header3 = i == 2 ? (4 + i).ToString() : Header3 != null ? Header3 : null;
                Header4 = i == 3 ? (4 + i).ToString() : Header4 != null ? Header4 : null;
                Header5 = i == 4 ? (4 + i).ToString() : Header5 != null ? Header5 : null;
                Header6 = i == 5 ? (4 + i).ToString() : Header6 != null ? Header6 : null;
                Header7 = i == 6 ? (4 + i).ToString() : Header7 != null ? Header7 : null;
                Header8 = i == 7 ? (4 + i).ToString() : Header8 != null ? Header8 : null;
                Header9 = i == 8 ? (4 + i).ToString() : Header9 != null ? Header9 : null;
                Header10 = i == 9 ? (4 + i).ToString() : Header10 != null ? Header10 : null;

                HeaderKPUSD1 = i == 0 ? (6 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPUSD1 != null ? HeaderKPUSD1 : null;
                HeaderKPUSD2 = i == 1 ? (6 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPUSD2 != null ? HeaderKPUSD2 : null;
                HeaderKPUSD3 = i == 2 ? (6 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPUSD3 != null ? HeaderKPUSD3 : null;
                HeaderKPUSD4 = i == 3 ? (6 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPUSD4 != null ? HeaderKPUSD4 : null;
                HeaderKPUSD5 = i == 4 ? (6 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPUSD5 != null ? HeaderKPUSD5 : null;
                HeaderKPUSD6 = i == 5 ? (6 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPUSD6 != null ? HeaderKPUSD6 : null;
                HeaderKPUSD7 = i == 6 ? (6 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPUSD7 != null ? HeaderKPUSD7 : null;
                HeaderKPUSD8 = i == 7 ? (6 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPUSD8 != null ? HeaderKPUSD8 : null;
                HeaderKPUSD9 = i == 8 ? (6 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPUSD9 != null ? HeaderKPUSD9 : null;
                HeaderKPUSD10 = i == 9 ? (6 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPUSD10 != null ? HeaderKPUSD10 : null;

                HeaderKPVND1 = i == 0 ? (7 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPVND1 != null ? HeaderKPVND1 : null;
                HeaderKPVND2 = i == 1 ? (7 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPVND2 != null ? HeaderKPVND2 : null;
                HeaderKPVND3 = i == 2 ? (7 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPVND3 != null ? HeaderKPVND3 : null;
                HeaderKPVND4 = i == 3 ? (7 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPVND4 != null ? HeaderKPVND4 : null;
                HeaderKPVND5 = i == 4 ? (7 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPVND5 != null ? HeaderKPVND5 : null;
                HeaderKPVND6 = i == 5 ? (7 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPVND6 != null ? HeaderKPVND6 : null;
                HeaderKPVND7 = i == 6 ? (7 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPVND7 != null ? HeaderKPVND7 : null;
                HeaderKPVND8 = i == 7 ? (7 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPVND8 != null ? HeaderKPVND8 : null;
                HeaderKPVND9 = i == 8 ? (7 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPVND9 != null ? HeaderKPVND9 : null;
                HeaderKPVND10 = i == 9 ? (7 + listGiaiDoan.Count() + i * 2).ToString() : HeaderKPVND10 != null ? HeaderKPVND10 : null;

                HeaderQTUSD1 = i == 0 ? (8 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTUSD1 != null ? HeaderQTUSD1 : null;
                HeaderQTUSD2 = i == 1 ? (8 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTUSD2 != null ? HeaderQTUSD2 : null;
                HeaderQTUSD3 = i == 2 ? (8 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTUSD3 != null ? HeaderQTUSD3 : null;
                HeaderQTUSD4 = i == 3 ? (8 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTUSD4 != null ? HeaderQTUSD4 : null;
                HeaderQTUSD5 = i == 4 ? (8 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTUSD5 != null ? HeaderQTUSD5 : null;
                HeaderQTUSD6 = i == 5 ? (8 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTUSD6 != null ? HeaderQTUSD6 : null;
                HeaderQTUSD7 = i == 6 ? (8 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTUSD7 != null ? HeaderQTUSD7 : null;
                HeaderQTUSD8 = i == 7 ? (8 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTUSD8 != null ? HeaderQTUSD8 : null;
                HeaderQTUSD9 = i == 8 ? (8 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTUSD9 != null ? HeaderQTUSD9 : null;
                HeaderQTUSD10 = i == 9 ? (8 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTUSD10 != null ? HeaderQTUSD10 : null;

                HeaderQTVND1 = i == 0 ? (9 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTVND1 != null ? HeaderQTVND1 : null;
                HeaderQTVND2 = i == 1 ? (9 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTVND2 != null ? HeaderQTVND2 : null;
                HeaderQTVND3 = i == 2 ? (9 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTVND3 != null ? HeaderQTVND3 : null;
                HeaderQTVND4 = i == 3 ? (9 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTVND4 != null ? HeaderQTVND4 : null;
                HeaderQTVND5 = i == 4 ? (9 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTVND5 != null ? HeaderQTVND5 : null;
                HeaderQTVND6 = i == 5 ? (9 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTVND6 != null ? HeaderQTVND6 : null;
                HeaderQTVND7 = i == 6 ? (9 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTVND7 != null ? HeaderQTVND7 : null;
                HeaderQTVND8 = i == 7 ? (9 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTVND8 != null ? HeaderQTVND8 : null;
                HeaderQTVND9 = i == 8 ? (9 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTVND9 != null ? HeaderQTVND9 : null;
                HeaderQTVND10 = i == 9 ? (9 + listGiaiDoan.Count() * 3 + i * 2).ToString() : HeaderQTVND10 != null ? HeaderQTVND10 : null;
            }
            string headTong = "", headerTongKPUSD = "", headerTongKPVND = "", headerTongQTUSD = "", headerTongQTVND = "";
            int intTong = 0, intTongKPUSD = 0, intTongKPVND = 0, intTongQTUSD = 0, intTongQTVND = 0;
            for (var i = 0; i < listGiaiDoan.Count(); i++)
            {
                intTong = 4 + i;
                headTong += intTong + " + ";
                intTongKPUSD = 6 + listGiaiDoan.Count() + i * 2;
                headerTongKPUSD += intTongKPUSD + " + ";
                intTongKPVND = 7 + listGiaiDoan.Count() + i * 2;
                headerTongKPVND += intTongKPVND + " + ";
                intTongQTUSD = 8 + listGiaiDoan.Count() * 3 + i * 2;
                headerTongQTUSD += intTongQTUSD + " + ";
                intTongQTVND = 9 + listGiaiDoan.Count() * 3 + i * 2;
                headerTongQTVND += intTongQTVND + " + ";
            }
            if (listGiaiDoan.Count() > 0)
            {
                Header = 3 + " = " + headTong.Substring(0, headTong.Length - 3);
                HeaderKPUSD = (4 + listGiaiDoan.Count()).ToString() + " = " + headerTongKPUSD.Substring(0, headerTongKPUSD.Length - 3);
                HeaderKPVND = (5 + listGiaiDoan.Count()).ToString() + " = " + headerTongKPVND.Substring(0, headerTongKPVND.Length - 3);
                HeaderQTUSD = (6 + listGiaiDoan.Count() * 3).ToString() + " = " + headerTongQTUSD.Substring(0, headerTongQTUSD.Length - 3);
                HeaderQTVND = (7 + listGiaiDoan.Count() * 3).ToString() + " = " + headerTongQTVND.Substring(0, headerTongQTVND.Length - 3);
            }
            else
            {
                Header = "3";
                HeaderKPUSD = "4";
                HeaderKPVND = "5";
                HeaderQTUSD = "6"; 
                HeaderQTVND = "7";
            }
            HeaderSS1 = (8 + listGiaiDoan.Count() * 5).ToString() + " = " + (4 + listGiaiDoan.Count()).ToString() + " - " + (6 + listGiaiDoan.Count() * 3).ToString();
            HeaderSS2 = (9 + listGiaiDoan.Count() * 5).ToString() + " = " + (5 + listGiaiDoan.Count()).ToString() + " - " + (7 + listGiaiDoan.Count() * 3).ToString();
            HeaderTN1 = (10 + listGiaiDoan.Count() * 5).ToString();
            HeaderTN2 = (11 + listGiaiDoan.Count() * 5).ToString();

            CountGiaiDoanKeHoach = listGiaiDoan.Count() + 1;
            CountGiaiDoanKinhPhi = (listGiaiDoan.Count() + 1) * 2;
            CountGiaiDoanQuyetToan = (listGiaiDoan.Count() + 1) * 2;

            Items = _mapper.Map<ObservableCollection<NhQtPheDuyetQuyetToanDAHTChiTietModel>>(returnData);
            OnPropertyChanged(nameof(CountGiaiDoanKeHoach));
            OnPropertyChanged(nameof(CountGiaiDoanKinhPhi));
            OnPropertyChanged(nameof(CountGiaiDoanQuyetToan));


            OnPropertyChanged(nameof(GiaiDoan1));
            OnPropertyChanged(nameof(GiaiDoan2));
            OnPropertyChanged(nameof(GiaiDoan3));
            OnPropertyChanged(nameof(GiaiDoan4));
            OnPropertyChanged(nameof(GiaiDoan5));
            OnPropertyChanged(nameof(GiaiDoan6));
            OnPropertyChanged(nameof(GiaiDoan7));
            OnPropertyChanged(nameof(GiaiDoan8));
            OnPropertyChanged(nameof(GiaiDoan9));
            OnPropertyChanged(nameof(GiaiDoan10));

            OnPropertyChanged(nameof(Header1));
            OnPropertyChanged(nameof(Header2));
            OnPropertyChanged(nameof(Header3));
            OnPropertyChanged(nameof(Header4));
            OnPropertyChanged(nameof(Header5));
            OnPropertyChanged(nameof(Header6));
            OnPropertyChanged(nameof(Header7));
            OnPropertyChanged(nameof(Header8));
            OnPropertyChanged(nameof(Header9));
            OnPropertyChanged(nameof(Header10));

            OnPropertyChanged(nameof(HeaderKPUSD1));
            OnPropertyChanged(nameof(HeaderKPUSD2));
            OnPropertyChanged(nameof(HeaderKPUSD3));
            OnPropertyChanged(nameof(HeaderKPUSD4));
            OnPropertyChanged(nameof(HeaderKPUSD5));
            OnPropertyChanged(nameof(HeaderKPUSD6));
            OnPropertyChanged(nameof(HeaderKPUSD7));
            OnPropertyChanged(nameof(HeaderKPUSD8));
            OnPropertyChanged(nameof(HeaderKPUSD9));
            OnPropertyChanged(nameof(HeaderKPUSD10));

            OnPropertyChanged(nameof(HeaderKPVND1));
            OnPropertyChanged(nameof(HeaderKPVND2));
            OnPropertyChanged(nameof(HeaderKPVND3));
            OnPropertyChanged(nameof(HeaderKPVND4));
            OnPropertyChanged(nameof(HeaderKPVND5));
            OnPropertyChanged(nameof(HeaderKPVND6));
            OnPropertyChanged(nameof(HeaderKPVND7));
            OnPropertyChanged(nameof(HeaderKPVND8));
            OnPropertyChanged(nameof(HeaderKPVND9));
            OnPropertyChanged(nameof(HeaderKPVND10));

            OnPropertyChanged(nameof(HeaderQTUSD1));
            OnPropertyChanged(nameof(HeaderQTUSD2));
            OnPropertyChanged(nameof(HeaderQTUSD3));
            OnPropertyChanged(nameof(HeaderQTUSD4));
            OnPropertyChanged(nameof(HeaderQTUSD5));
            OnPropertyChanged(nameof(HeaderQTUSD6));
            OnPropertyChanged(nameof(HeaderQTUSD7));
            OnPropertyChanged(nameof(HeaderQTUSD8));
            OnPropertyChanged(nameof(HeaderQTUSD9));
            OnPropertyChanged(nameof(HeaderQTUSD10));

            OnPropertyChanged(nameof(HeaderQTVND1));
            OnPropertyChanged(nameof(HeaderQTVND2));
            OnPropertyChanged(nameof(HeaderQTVND3));
            OnPropertyChanged(nameof(HeaderQTVND4));
            OnPropertyChanged(nameof(HeaderQTVND5));
            OnPropertyChanged(nameof(HeaderQTVND6));
            OnPropertyChanged(nameof(HeaderQTVND7));
            OnPropertyChanged(nameof(HeaderQTVND8));
            OnPropertyChanged(nameof(HeaderQTVND9));
            OnPropertyChanged(nameof(HeaderQTVND10));

            OnPropertyChanged(nameof(HeaderSS1)); OnPropertyChanged(nameof(HeaderSS2));
            OnPropertyChanged(nameof(HeaderTN1)); OnPropertyChanged(nameof(HeaderTN2));
        }

        private List<NhQtPheDuyetQuyetToanDAHTChiTietModel> getListDetailChiTiet(List<NhQtPheDuyetQuyetToanDAHTChiTietQuery> listData, bool isAdd)
        {
            var listResult = new List<NhQtPheDuyetQuyetToanDAHTChiTietModel>();

            var giaiDoans = listData.Select(x => new
            {
                x.INamBaoCaoTu,
                x.INamBaoCaoDen
            }).Where(x => x.INamBaoCaoTu != null && x.INamBaoCaoDen != null).OrderBy(x => x.INamBaoCaoTu).Distinct().ToList();

            var getAllChuongTrinh = listData.Where(x => x.IIDDonViId == Model.IIdDonViId).Select(x => new { x.STenNhiemVuChi, x.IIDKHTTNhiemVuChiId }).Distinct().ToList();

            var iCountChuongTrinh = 0;
            foreach (var chuongTrinh in getAllChuongTrinh)
            {
                iCountChuongTrinh++;
                var newObj = new NhQtPheDuyetQuyetToanDAHTChiTietModel()
                {
                    STenNoiDungChi = convertLetter(iCountChuongTrinh) + ", " + chuongTrinh.STenNhiemVuChi,
                    FSumTTCP = listData.Where(x => x.IIDKHTTNhiemVuChiId == chuongTrinh.IIDKHTTNhiemVuChiId).Sum(x => x.FKeHoachTTCPUsd),
                    FSumKPDCUsd = listData.Where(x => x.IIDKHTTNhiemVuChiId == chuongTrinh.IIDKHTTNhiemVuChiId).Sum(x => x.FKinhPhiDuocCapTongUsd),
                    FSumKPDCVnd = listData.Where(x => x.IIDKHTTNhiemVuChiId == chuongTrinh.IIDKHTTNhiemVuChiId).Sum(x => x.FKinhPhiDuocCapTongVnd),
                    FSumQTDDUsd = listData.Where(x => x.IIDKHTTNhiemVuChiId == chuongTrinh.IIDKHTTNhiemVuChiId).Sum(x => x.FQuyetToanDuocDuyetTongUsd),
                    FSumQTDDVnd = listData.Where(x => x.IIDKHTTNhiemVuChiId == chuongTrinh.IIDKHTTNhiemVuChiId).Sum(x => x.FQuyetToanDuocDuyetTongVnd),
                    IsData = false
                };
                listResult.Add(newObj);
                var getListDuAn = listData.Where(x => x.IIDKHTTNhiemVuChiId == chuongTrinh.IIDKHTTNhiemVuChiId && x.IIDDuAnId != null).ToList();
                var getListHopDong = listData.Where(x => x.IIDKHTTNhiemVuChiId == chuongTrinh.IIDKHTTNhiemVuChiId && x.IIDDuAnId == null && x.IIDHopDongId != null).ToList();
                var getListNone = listData.Where(x => x.IIDKHTTNhiemVuChiId == chuongTrinh.IIDKHTTNhiemVuChiId && x.IIDDuAnId == null && x.IIDHopDongId == null).ToList();

                var iCountDuAn = 0;

                if (getListDuAn.Any())
                {
                    var getNameDuAn = getListDuAn.Select(x => new { x.STenDuAn, x.IIDDuAnId, x.FHopDongUsdDuAn, x.FHopDongVndDuAn })
                    .Distinct()
                    .ToList();
                    foreach (var hopDongDuAn in getNameDuAn)
                    {
                        iCountDuAn++;
                        var newObjHopDongDuAn = new NhQtPheDuyetQuyetToanDAHTChiTietModel()
                        {
                            STenNoiDungChi = convertLaMa(Decimal.Parse(iCountDuAn.ToString())) + ", " + hopDongDuAn.STenDuAn,
                            FHopDongUsd = hopDongDuAn.FHopDongUsdDuAn,
                            FHopDongVnd = hopDongDuAn.FHopDongVndDuAn,
                            FSumTTCP = getListDuAn.Sum(x => x.FKeHoachTTCPUsd),
                            FSumKPDCUsd = getListDuAn.Sum(x => x.FKinhPhiDuocCapTongUsd),
                            FSumKPDCVnd = getListDuAn.Sum(x => x.FKinhPhiDuocCapTongVnd),
                            FSumQTDDUsd = getListDuAn.Sum(x => x.FQuyetToanDuocDuyetTongUsd),
                            FSumQTDDVnd = getListDuAn.Sum(x => x.FQuyetToanDuocDuyetTongVnd),
                            IsData = false
                        };
                        listResult.Add(newObjHopDongDuAn);
                        listResult.AddRange(returnLoaiChi(iCountDuAn.ToString(), true, getListDuAn.Where(x => x.IIDDuAnId == hopDongDuAn.IIDDuAnId).ToList(), listData, isAdd));
                    }
                }
                if (getListHopDong.Any())
                {
                    iCountDuAn++;
                    var newObjHopDong = new NhQtPheDuyetQuyetToanDAHTChiTietModel()
                    {
                        STenNoiDungChi = convertLaMa(Decimal.Parse(iCountDuAn.ToString())) + ", Chi hợp đồng",
                        FSumTTCP = getListHopDong.Sum(x => x.FKeHoachTTCPUsd),
                        FSumKPDCUsd = getListHopDong.Sum(x => x.FKinhPhiDuocCapTongUsd),
                        FSumKPDCVnd = getListHopDong.Sum(x => x.FKinhPhiDuocCapTongVnd),
                        FSumQTDDUsd = getListHopDong.Sum(x => x.FQuyetToanDuocDuyetTongUsd),
                        FSumQTDDVnd = getListHopDong.Sum(x => x.FQuyetToanDuocDuyetTongVnd),
                        IsData = false
                    };
                    listResult.Add(newObjHopDong);
                    listResult.AddRange(returnLoaiChi(iCountChuongTrinh.ToString(), true, getListHopDong, listData, isAdd));
                    //
                }
                if (getListNone.Any())
                {
                    iCountDuAn++;
                    var newObjKhac = new NhQtPheDuyetQuyetToanDAHTChiTietModel()
                    {
                        STenNoiDungChi = convertLaMa(Decimal.Parse(iCountDuAn.ToString())) + ", Chi khác",
                        FSumTTCP = getListNone.Sum(x => x.FKeHoachTTCPUsd),
                        FSumKPDCUsd = getListNone.Sum(x => x.FKinhPhiDuocCapTongUsd),
                        FSumKPDCVnd = getListNone.Sum(x => x.FKinhPhiDuocCapTongVnd),
                        FSumQTDDUsd = getListNone.Sum(x => x.FQuyetToanDuocDuyetTongUsd),
                        FSumQTDDVnd = getListNone.Sum(x => x.FQuyetToanDuocDuyetTongVnd),
                        IsData = false
                    };
                    listResult.Add(newObjKhac);
                    listResult.AddRange(returnLoaiChi("1", false, getListNone, listData, isAdd));
                }
            }
            return listResult;
        }

        public List<NhQtPheDuyetQuyetToanDAHTChiTietModel> returnLoaiChi(string stt, bool idDuAn, List<NhQtPheDuyetQuyetToanDAHTChiTietQuery> list, List<NhQtPheDuyetQuyetToanDAHTChiTietQuery> listData, bool isAdd)
        {

            List<NhQtPheDuyetQuyetToanDAHTChiTietModel> returnData = new List<NhQtPheDuyetQuyetToanDAHTChiTietModel>();
            var listLoaiChiPhi = list.Select(x => new { x.ILoaiNoiDungChi }).Distinct().OrderBy(x => x.ILoaiNoiDungChi)
                  .ToList();
            var countLoaiChiPhi = 0;
            foreach (var loaiChiPhi in listLoaiChiPhi)
            {
                countLoaiChiPhi++;
                var newObjLoaiChiPhi = new NhQtPheDuyetQuyetToanDAHTChiTietModel()
                {
                    STenNoiDungChi = countLoaiChiPhi.ToString() + (loaiChiPhi.ILoaiNoiDungChi == 1 ? ", Chi ngoại tệ" : ", Chi trong nước"),
                    IsData = false
                };
                returnData.Add(newObjLoaiChiPhi);

                if (idDuAn)
                {
                    var listNameHopDong = list.Where(x => x.ILoaiNoiDungChi == loaiChiPhi.ILoaiNoiDungChi).Select(x => new { x.STenHopDong, x.IIDHopDongId, x.FHopDongUsdHopDong, x.FHopDongVndHopDong }).Distinct()
                    .ToList();
                    var countHopDong = 0;
                    foreach (var nameHopDong in listNameHopDong)
                    {
                        countHopDong++;
                        var listHopDong = list.Where(x => x.ILoaiNoiDungChi == loaiChiPhi.ILoaiNoiDungChi && x.IIDHopDongId == nameHopDong.IIDHopDongId)
                            .ToList();
                        var newObjHopDongDuAn = new NhQtPheDuyetQuyetToanDAHTChiTietModel()
                        {
                            STenNoiDungChi = countLoaiChiPhi.ToString() + "." + countHopDong.ToString() + ", " + (nameHopDong.STenHopDong == null ? "Không hình thành hợp đồng" : nameHopDong.STenHopDong),
                            FHopDongVnd = nameHopDong.FHopDongVndHopDong,
                            FHopDongUsd = nameHopDong.FHopDongUsdHopDong,
                            FSumTTCP = listHopDong.Sum(x => x.FKeHoachTTCPUsd),
                            FSumKPDCUsd = listHopDong.Sum(x => x.FKinhPhiDuocCapTongUsd),
                            FSumKPDCVnd = listHopDong.Sum(x => x.FKinhPhiDuocCapTongVnd),
                            FSumQTDDUsd = listHopDong.Sum(x => x.FQuyetToanDuocDuyetTongUsd),
                            FSumQTDDVnd = listHopDong.Sum(x => x.FQuyetToanDuocDuyetTongVnd),
                            IsData = false
                        };
                        returnData.Add(newObjHopDongDuAn);
                        var dataMap = _mapper.Map<ObservableCollection<NhQtPheDuyetQuyetToanDAHTChiTietModel>>(listHopDong).ToList();
                        dataMap.ForEach(x =>
                        {
                            x.IsData = true;
                            x.IsAdded = isAdd;
                            x.IsModified = !isAdd;
                            x.IIDPheDuyetQuyetToanDAHTId = Model.Id;
                            var a = listHopDong.GroupBy(z => z.IIDThanhToanChiTietId).Select(y => new
                            {
                                FSumKPDCUsd = y.Sum(k => k.FKinhPhiDuocCapTongUsd),
                                FSumKPDCVnd = y.Sum(k => k.FKinhPhiDuocCapTongVnd),
                                FSumQTDDUsd = y.Sum(k => k.FQuyetToanDuocDuyetTongUsd),
                                FSumQTDDVnd = y.Sum(k => k.FQuyetToanDuocDuyetTongVnd),
                                IIDThanhToanChiTietId = y.Key
                            }).Where(l => l.IIDThanhToanChiTietId == x.IIDThanhToanChiTietId).FirstOrDefault();
                            x.FSumKPDCUsd = a.FSumKPDCUsd;
                            x.FSumKPDCVnd = a.FSumKPDCVnd;
                            x.FSumQTDDUsd = a.FSumQTDDUsd;
                            x.FSumQTDDVnd = a.FSumQTDDVnd;
                            x.FSoSanhKinhPhiUsd = x.FSumKPDCUsd - x.FSumQTDDUsd;
                            x.FSoSanhKinhPhiVnd = x.FSumKPDCVnd - x.FSumQTDDVnd;
                        });
                        var listSetData = setData(dataMap, listData);
                        returnData.AddRange(listSetData);
                    }
                }
                else
                {
                    var listHopDong = list.Where(x => x.ILoaiNoiDungChi == loaiChiPhi.ILoaiNoiDungChi).ToList();
                    var listSetData = setData(_mapper.Map<ObservableCollection<NhQtPheDuyetQuyetToanDAHTChiTietModel>>(listHopDong).ToList(), listData);
                    listSetData.ForEach(x =>
                    {
                        x.IsData = true;
                        x.IsAdded = isAdd;
                        x.IsModified = !isAdd;
                        x.IIDPheDuyetQuyetToanDAHTId = Model.Id;
                    });
                    returnData.AddRange(listSetData);
                }

            }
            return returnData;
        }

        private List<NhQtPheDuyetQuyetToanDAHTChiTietModel> setData(List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataReturn, List<NhQtPheDuyetQuyetToanDAHTChiTietQuery> listData)
        {
            var giaiDoans = listData.Select(x => new
            {
                x.INamBaoCaoTu,
                x.INamBaoCaoDen
            }).Where(x => x.INamBaoCaoTu != null && x.INamBaoCaoDen != null).OrderBy(x => x.INamBaoCaoTu).Distinct().ToList();
            List<NhTtThucHienNganSachGiaiDoanQuery> listGiaiDoan = listData
                    .GroupBy(x => (x.INamBaoCaoTu, x.INamBaoCaoDen)).Select(x => x.First())
                    .Select(x => new NhTtThucHienNganSachGiaiDoanQuery
                    {
                        sGiaiDoan = "Giai đoạn " + x.INamBaoCaoTu + " - " + x.INamBaoCaoDen,
                        iGiaiDoanTu = x.INamBaoCaoTu,
                        iGiaiDoanDen = x.INamBaoCaoDen
                    }).Take(10).ToList();
            var dataTongHops = LoadDataTongHop(listGiaiDoan);
            var lstMaNguon = NHConstants.MA_TH_BCKL_QT.Split(",").Select(x => x.Trim()).ToList();

            for (var i = 0; i < listGiaiDoan.Count(); i++)
            {
                foreach (var item in listDataReturn)
                {
                    if (item.INamBaoCaoTu == listGiaiDoan[i].iGiaiDoanTu && item.INamBaoCaoDen == listGiaiDoan[i].iGiaiDoanDen)
                    {
                        item.FKeHoachTTCPUsd1 = i == 0 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd1 != null ? item.FKeHoachTTCPUsd1 : null;
                        item.FKeHoachTTCPUsd2 = i == 1 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd2 != null ? item.FKeHoachTTCPUsd2 : null;
                        item.FKeHoachTTCPUsd3 = i == 2 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd3 != null ? item.FKeHoachTTCPUsd3 : null;
                        item.FKeHoachTTCPUsd4 = i == 3 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd4 != null ? item.FKeHoachTTCPUsd4 : null;
                        item.FKeHoachTTCPUsd5 = i == 4 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd5 != null ? item.FKeHoachTTCPUsd5 : null;
                        item.FKeHoachTTCPUsd6 = i == 5 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd6 != null ? item.FKeHoachTTCPUsd6 : null;
                        item.FKeHoachTTCPUsd7 = i == 6 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd7 != null ? item.FKeHoachTTCPUsd7 : null;
                        item.FKeHoachTTCPUsd8 = i == 7 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd8 != null ? item.FKeHoachTTCPUsd8 : null;
                        item.FKeHoachTTCPUsd9 = i == 8 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd9 != null ? item.FKeHoachTTCPUsd9 : null;
                        item.FKeHoachTTCPUsd10 = i == 9 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd10 != null ? item.FKeHoachTTCPUsd10 : null;

                        //item.FKinhPhiDuocCapTongVnd1 = i == 0 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd1 != null ? item.FKinhPhiDuocCapTongVnd1 : null;
                        //item.FKinhPhiDuocCapTongVnd2 = i == 1 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd2 != null ? item.FKinhPhiDuocCapTongVnd2 : null;
                        //item.FKinhPhiDuocCapTongVnd3 = i == 2 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd3 != null ? item.FKinhPhiDuocCapTongVnd3 : null;
                        //item.FKinhPhiDuocCapTongVnd4 = i == 3 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd4 != null ? item.FKinhPhiDuocCapTongVnd4 : null;
                        //item.FKinhPhiDuocCapTongVnd5 = i == 4 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd5 != null ? item.FKinhPhiDuocCapTongVnd5 : null;
                        //item.FKinhPhiDuocCapTongVnd6 = i == 5 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd6 != null ? item.FKinhPhiDuocCapTongVnd6 : null;
                        //item.FKinhPhiDuocCapTongVnd7 = i == 6 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd7 != null ? item.FKinhPhiDuocCapTongVnd7 : null;
                        //item.FKinhPhiDuocCapTongVnd8 = i == 7 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd8 != null ? item.FKinhPhiDuocCapTongVnd8 : null;
                        //item.FKinhPhiDuocCapTongVnd9 = i == 8 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd9 != null ? item.FKinhPhiDuocCapTongVnd9 : null;
                        //item.FKinhPhiDuocCapTongVnd10 = i == 9 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd10 != null ? item.FKinhPhiDuocCapTongVnd10 : null;

                        //dataTHKinhphi = nguon 306(aj) - nguon306(ai-1)
                        var dataThKinhPhiCapVnd = (dataTongHops.Where(x => x.SMaNguon == NhTongHopConstants.MA_306 && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriVnd) - dataTongHops.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306 && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriVnd)) - (dataTongHops.Where(x => x.SMaNguon == NhTongHopConstants.MA_306 && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriVnd) - dataTongHops.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306 && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriVnd));
                        item.FKinhPhiDuocCapTongVnd1 = i == 0 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd1 != null ? item.FKinhPhiDuocCapTongVnd1 : null;
                        item.FKinhPhiDuocCapTongVnd2 = i == 1 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd2 != null ? item.FKinhPhiDuocCapTongVnd2 : null;
                        item.FKinhPhiDuocCapTongVnd3 = i == 2 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd3 != null ? item.FKinhPhiDuocCapTongVnd3 : null;
                        item.FKinhPhiDuocCapTongVnd4 = i == 3 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd4 != null ? item.FKinhPhiDuocCapTongVnd4 : null;
                        item.FKinhPhiDuocCapTongVnd5 = i == 4 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd5 != null ? item.FKinhPhiDuocCapTongVnd5 : null;
                        item.FKinhPhiDuocCapTongVnd6 = i == 5 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd6 != null ? item.FKinhPhiDuocCapTongVnd6 : null;
                        item.FKinhPhiDuocCapTongVnd7 = i == 6 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd7 != null ? item.FKinhPhiDuocCapTongVnd7 : null;
                        item.FKinhPhiDuocCapTongVnd8 = i == 7 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd8 != null ? item.FKinhPhiDuocCapTongVnd8 : null;
                        item.FKinhPhiDuocCapTongVnd9 = i == 8 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd9 != null ? item.FKinhPhiDuocCapTongVnd9 : null;
                        item.FKinhPhiDuocCapTongVnd10 = i == 9 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd10 != null ? item.FKinhPhiDuocCapTongVnd10 : null;

                        //item.FKinhPhiDuocCapTongUsd1 = i == 0 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd1 != null ? item.FKinhPhiDuocCapTongUsd1 : null;
                        //item.FKinhPhiDuocCapTongUsd2 = i == 1 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd2 != null ? item.FKinhPhiDuocCapTongUsd2 : null;
                        //item.FKinhPhiDuocCapTongUsd3 = i == 2 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd3 != null ? item.FKinhPhiDuocCapTongUsd3 : null;
                        //item.FKinhPhiDuocCapTongUsd4 = i == 3 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd4 != null ? item.FKinhPhiDuocCapTongUsd4 : null;
                        //item.FKinhPhiDuocCapTongUsd5 = i == 4 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd5 != null ? item.FKinhPhiDuocCapTongUsd5 : null;
                        //item.FKinhPhiDuocCapTongUsd6 = i == 5 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd6 != null ? item.FKinhPhiDuocCapTongUsd6 : null;
                        //item.FKinhPhiDuocCapTongUsd7 = i == 6 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd7 != null ? item.FKinhPhiDuocCapTongUsd7 : null;
                        //item.FKinhPhiDuocCapTongUsd8 = i == 7 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd8 != null ? item.FKinhPhiDuocCapTongUsd8 : null;
                        //item.FKinhPhiDuocCapTongUsd9 = i == 8 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd9 != null ? item.FKinhPhiDuocCapTongUsd9 : null;
                        //item.FKinhPhiDuocCapTongUsd10 = i == 9 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd10 != null ? item.FKinhPhiDuocCapTongUsd10 : null;


                        //dataTHKinhphi = nguon 306(aj) - nguon306(ai-1)
                        var dataThKinhPhiCapUsd = (dataTongHops.Where(x => x.SMaNguon == NhTongHopConstants.MA_306 && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriUsd) - dataTongHops.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306 && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriUsd)) - (dataTongHops.Where(x => x.SMaNguon == NhTongHopConstants.MA_306 && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriUsd) - dataTongHops.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306 && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriUsd));
                        item.FKinhPhiDuocCapTongUsd1 = i == 0 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd1 != null ? item.FKinhPhiDuocCapTongUsd1 : null;
                        item.FKinhPhiDuocCapTongUsd2 = i == 1 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd2 != null ? item.FKinhPhiDuocCapTongUsd2 : null;
                        item.FKinhPhiDuocCapTongUsd3 = i == 2 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd3 != null ? item.FKinhPhiDuocCapTongUsd3 : null;
                        item.FKinhPhiDuocCapTongUsd4 = i == 3 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd4 != null ? item.FKinhPhiDuocCapTongUsd4 : null;
                        item.FKinhPhiDuocCapTongUsd5 = i == 4 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd5 != null ? item.FKinhPhiDuocCapTongUsd5 : null;
                        item.FKinhPhiDuocCapTongUsd6 = i == 5 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd6 != null ? item.FKinhPhiDuocCapTongUsd6 : null;
                        item.FKinhPhiDuocCapTongUsd7 = i == 6 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd7 != null ? item.FKinhPhiDuocCapTongUsd7 : null;
                        item.FKinhPhiDuocCapTongUsd8 = i == 7 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd8 != null ? item.FKinhPhiDuocCapTongUsd8 : null;
                        item.FKinhPhiDuocCapTongUsd9 = i == 8 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd9 != null ? item.FKinhPhiDuocCapTongUsd9 : null;
                        item.FKinhPhiDuocCapTongUsd10 = i == 9 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd10 != null ? item.FKinhPhiDuocCapTongUsd10 : null;

                        //item.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd1 != null ? item.FQuyetToanDuocDuyetTongUsd1 : null;
                        //item.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd2 != null ? item.FQuyetToanDuocDuyetTongUsd2 : null;
                        //item.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd3 != null ? item.FQuyetToanDuocDuyetTongUsd3 : null;
                        //item.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd4 != null ? item.FQuyetToanDuocDuyetTongUsd4 : null;
                        //item.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd5 != null ? item.FQuyetToanDuocDuyetTongUsd5 : null;
                        //item.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd6 != null ? item.FQuyetToanDuocDuyetTongUsd6 : null;
                        //item.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd7 != null ? item.FQuyetToanDuocDuyetTongUsd7 : null;
                        //item.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd8 != null ? item.FQuyetToanDuocDuyetTongUsd8 : null;
                        //item.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd9 != null ? item.FQuyetToanDuocDuyetTongUsd9 : null;
                        //item.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd10 != null ? item.FQuyetToanDuocDuyetTongUsd10 : null;
                        //dataTHKinhPhiDuocDuyet = nguon 301,304(aj) - nguon301,304(ai-1)
                        var dataThKinhPhiDuocDuyetUsd = (dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguon) && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriUsd) - dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguonCha) && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriUsd)) - (dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguon) && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriUsd) - dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguonCha) && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriUsd));
                        item.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd1 != null ? item.FQuyetToanDuocDuyetTongUsd1 : null;
                        item.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd2 != null ? item.FQuyetToanDuocDuyetTongUsd2 : null;
                        item.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd3 != null ? item.FQuyetToanDuocDuyetTongUsd3 : null;
                        item.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd4 != null ? item.FQuyetToanDuocDuyetTongUsd4 : null;
                        item.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd5 != null ? item.FQuyetToanDuocDuyetTongUsd5 : null;
                        item.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd6 != null ? item.FQuyetToanDuocDuyetTongUsd6 : null;
                        item.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd7 != null ? item.FQuyetToanDuocDuyetTongUsd7 : null;
                        item.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd8 != null ? item.FQuyetToanDuocDuyetTongUsd8 : null;
                        item.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd9 != null ? item.FQuyetToanDuocDuyetTongUsd9 : null;
                        item.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd10 != null ? item.FQuyetToanDuocDuyetTongUsd10 : null;

                        //item.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd1 != null ? item.FQuyetToanDuocDuyetTongVnd1 : null;
                        //item.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd2 != null ? item.FQuyetToanDuocDuyetTongVnd2 : null;
                        //item.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd3 != null ? item.FQuyetToanDuocDuyetTongVnd3 : null;
                        //item.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd4 != null ? item.FQuyetToanDuocDuyetTongVnd4 : null;
                        //item.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd5 != null ? item.FQuyetToanDuocDuyetTongVnd5 : null;
                        //item.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd6 != null ? item.FQuyetToanDuocDuyetTongVnd6 : null;
                        //item.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd7 != null ? item.FQuyetToanDuocDuyetTongVnd7 : null;
                        //item.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd8 != null ? item.FQuyetToanDuocDuyetTongVnd8 : null;
                        //item.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd9 != null ? item.FQuyetToanDuocDuyetTongVnd9 : null;
                        //item.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd10 != null ? item.FQuyetToanDuocDuyetTongVnd10 : null;
                        //dataTHKinhPhiDuocDuyet = nguon 301,304(aj) - nguon301,304(ai-1)
                        var dataThKinhPhiDuocDuyetVnd = (dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguon) && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriVnd) - dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguonCha) && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriVnd)) - (dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguon) && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriVnd) - dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguonCha) && x.INamKeHoach == listGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriVnd));
                        item.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd1 != null ? item.FQuyetToanDuocDuyetTongVnd1 : null;
                        item.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd2 != null ? item.FQuyetToanDuocDuyetTongVnd2 : null;
                        item.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd3 != null ? item.FQuyetToanDuocDuyetTongVnd3 : null;
                        item.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd4 != null ? item.FQuyetToanDuocDuyetTongVnd4 : null;
                        item.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd5 != null ? item.FQuyetToanDuocDuyetTongVnd5 : null;
                        item.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd6 != null ? item.FQuyetToanDuocDuyetTongVnd6 : null;
                        item.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd7 != null ? item.FQuyetToanDuocDuyetTongVnd7 : null;
                        item.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd8 != null ? item.FQuyetToanDuocDuyetTongVnd8 : null;
                        item.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd9 != null ? item.FQuyetToanDuocDuyetTongVnd9 : null;
                        item.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd10 != null ? item.FQuyetToanDuocDuyetTongVnd10 : null;
                    }
                }
            }

            return listDataReturn;
        }
        private string convertLetter(int input)
        {
            StringBuilder res = new StringBuilder((input - 1).ToString());
            for (int j = 0; j < res.Length; j++)
                res[j] += (char)(17); // '0' is 48, 'A' is 65
            return res.ToString();
        }
        private string convertLaMa(decimal num)
        {
            string strRet = string.Empty;
            decimal _Number = num;
            Boolean _Flag = true;
            string[] ArrLama = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] ArrNumber = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            int i = 0;
            while (_Flag)
            {
                while (_Number >= ArrNumber[i])
                {
                    _Number -= ArrNumber[i];
                    strRet += ArrLama[i];
                    if (_Number < 1)
                        _Flag = false;
                }
                i++;
            }
            return strRet;
        }

        protected override void OnAdd()
        {
            int currentRow = -1;
            if (!Items.IsEmpty())
            {
                if (SelectedItem != null)
                {
                    currentRow = Items.IndexOf(SelectedItem);
                }
            }

            NhQtPheDuyetQuyetToanDAHTChiTietModel targetItem = new NhQtPheDuyetQuyetToanDAHTChiTietModel();
            targetItem.Id = Guid.NewGuid();
            targetItem.IIDPheDuyetQuyetToanDAHTId = Model.Id;
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            //SetDataMucLucNganSach(targetItem, string.Empty);
            //targetItem.PropertyChanged += DetailModel_PropertyChanged;

            Items.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
        }

        public override void OnSave()
        {
            if (!Validate()) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                var entities = _mapper.Map<List<NhQtPheDuyetQuyetToanDAHTChiTiet>>(Items.Where(x => x.IsData == true));
                _service.AddOrUpdate(entities);
                //_service.AddOrUpdate(entities);
            }, (s, e) =>
            {
                IsLoading = false;

                if (e.Error == null)
                {
                    // Reload data
                    LoadData();

                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }

        private void OnPrint(ReportNhQuyetToanNienDoEnum reportType)
        {

        }

        public override void OnClosing()
        {
            // Clear items
            if (!Items.IsEmpty()) Items.Clear();
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }

        //private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    NhQtQuyetToanNienDoChiTietModel item = (NhQtQuyetToanNienDoChiTietModel)sender;
        //    switch (e.PropertyName)
        //    {
        //        case nameof(NhQtQuyetToanNienDoChiTietModel.LNS):
        //        case nameof(NhQtQuyetToanNienDoChiTietModel.L):
        //        case nameof(NhQtQuyetToanNienDoChiTietModel.K):
        //        case nameof(NhQtQuyetToanNienDoChiTietModel.M):
        //        case nameof(NhQtQuyetToanNienDoChiTietModel.TM):
        //        case nameof(NhQtQuyetToanNienDoChiTietModel.TTM):
        //        case nameof(NhQtQuyetToanNienDoChiTietModel.NG):
        //            SetDataMucLucNganSach(item, e.PropertyName, true);
        //            break;
        //        case nameof(NhQtQuyetToanNienDoChiTietModel.IIdHopDongId):
        //            // Fill dữ liệu
        //            var data = _service.FetchData(Model.Id, item.IIdHopDongId.Value);
        //            if (data != null)
        //            {
        //                item.FHopDongUsd = data.FHopDongUsd;
        //                item.FHopDongVnd = data.FHopDongVnd;
        //                item.FQtKinhPhiDuyetCacNamTruocUsd = data.FQtKinhPhiDuyetCacNamTruocUsd;
        //                item.FQtKinhPhiDuyetCacNamTruocVnd = data.FQtKinhPhiDuyetCacNamTruocVnd;
        //                item.FQtKinhPhiDuocCapNamTruocChuyenSangUsd = data.FQtKinhPhiDuocCapNamTruocChuyenSangUsd;
        //                item.FQtKinhPhiDuocCapNamTruocChuyenSangVnd = data.FQtKinhPhiDuocCapNamTruocChuyenSangVnd;
        //                item.FQtKinhPhiDuocCapNamNayUsd = data.FQtKinhPhiDuocCapNamNayUsd;
        //                item.FQtKinhPhiDuocCapNamNayVnd = data.FQtKinhPhiDuocCapNamNayVnd;
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //    item.IsModified = true;
        //}

        //private void SetDataMucLucNganSach(NhQtQuyetToanNienDoChiTietModel item, string type, bool isUpdateMlnsId = false)
        //{
        //    Expression<Func<NsMucLucNganSach, bool>> predicate;
        //    if (string.IsNullOrEmpty(type))
        //    {
        //        predicate = PredicateBuilder.True<NsMucLucNganSach>();
        //        predicate = predicate.And(x => !string.IsNullOrEmpty(x.L));
        //        var lstLNS = _rootDataMlns.Where(predicate.Compile()).GroupBy(n => n.Lns).Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key }).ToList();
        //        item.ItemsLNS = new ObservableCollection<ComboboxItem>(lstLNS);
        //        item.ItemsL = new ObservableCollection<ComboboxItem>();
        //        item.ItemsK = new ObservableCollection<ComboboxItem>();
        //        item.ItemsM = new ObservableCollection<ComboboxItem>();
        //        item.ItemsTM = new ObservableCollection<ComboboxItem>();
        //        item.ItemsTTM = new ObservableCollection<ComboboxItem>();
        //    }

        //    if (type != null && type.Equals(nameof(NhQtQuyetToanNienDoChiTietModel.LNS)))
        //    {
        //        predicate = PredicateBuilder.True<NsMucLucNganSach>();
        //        predicate = predicate.And(x => x.Lns.Equals(item.LNS));
        //        predicate = predicate.And(x => !string.IsNullOrEmpty(x.L));
        //        var lstL = _rootDataMlns.Where(predicate.Compile()).GroupBy(n => n.L).Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key });
        //        item.ItemsL = new ObservableCollection<ComboboxItem>(lstL);
        //        item.ItemsK = new ObservableCollection<ComboboxItem>();
        //        item.ItemsM = new ObservableCollection<ComboboxItem>();
        //        item.ItemsTM = new ObservableCollection<ComboboxItem>();
        //        item.ItemsTTM = new ObservableCollection<ComboboxItem>();
        //    }

        //    if (type != null && type.Equals(nameof(NhQtQuyetToanNienDoChiTietModel.L)))
        //    {
        //        predicate = PredicateBuilder.True<NsMucLucNganSach>();
        //        predicate = predicate.And(x => x.Lns.Equals(item.LNS));
        //        predicate = predicate.And(x => x.L.Equals(item.L));
        //        predicate = predicate.And(x => !string.IsNullOrEmpty(x.K));
        //        var lstK = _rootDataMlns.Where(predicate.Compile()).GroupBy(n => n.K).Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key });
        //        item.ItemsK = new ObservableCollection<ComboboxItem>(lstK);
        //        item.ItemsM = new ObservableCollection<ComboboxItem>();
        //        item.ItemsTM = new ObservableCollection<ComboboxItem>();
        //        item.ItemsTTM = new ObservableCollection<ComboboxItem>();
        //    }

        //    if (type != null && type.Equals(nameof(NhQtQuyetToanNienDoChiTietModel.K)))
        //    {
        //        predicate = PredicateBuilder.True<NsMucLucNganSach>();
        //        predicate = predicate.And(x => x.Lns.Equals(item.LNS));
        //        predicate = predicate.And(x => x.L.Equals(item.L));
        //        predicate = predicate.And(x => x.K.Equals(item.K));
        //        predicate = predicate.And(x => !string.IsNullOrEmpty(x.M));
        //        var lstM = _rootDataMlns.Where(predicate.Compile()).GroupBy(n => n.M).Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key });
        //        item.ItemsM = new ObservableCollection<ComboboxItem>(lstM);
        //        item.ItemsTM = new ObservableCollection<ComboboxItem>();
        //        item.ItemsTTM = new ObservableCollection<ComboboxItem>();
        //    }

        //    if (type != null && type.Equals(nameof(NhQtQuyetToanNienDoChiTietModel.M)))
        //    {
        //        predicate = PredicateBuilder.True<NsMucLucNganSach>();
        //        predicate = predicate.And(x => x.Lns.Equals(item.LNS));
        //        predicate = predicate.And(x => x.L.Equals(item.L));
        //        predicate = predicate.And(x => x.K.Equals(item.K));
        //        predicate = predicate.And(x => x.M.Equals(item.M));
        //        predicate = predicate.And(x => !string.IsNullOrEmpty(x.Tm));
        //        var lstTM = _rootDataMlns.Where(predicate.Compile()).GroupBy(n => n.Tm).Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key });
        //        item.ItemsTM = new ObservableCollection<ComboboxItem>(lstTM);
        //        item.ItemsTTM = new ObservableCollection<ComboboxItem>();
        //    }

        //    if (type != null && type.Equals(nameof(NhQtQuyetToanNienDoChiTietModel.TM)))
        //    {
        //        predicate = PredicateBuilder.True<NsMucLucNganSach>();
        //        predicate = predicate.And(x => x.Lns.Equals(item.LNS));
        //        predicate = predicate.And(x => x.L.Equals(item.L));
        //        predicate = predicate.And(x => x.K.Equals(item.K));
        //        predicate = predicate.And(x => x.M.Equals(item.M));
        //        predicate = predicate.And(x => x.Tm.Equals(item.TM));
        //        predicate = predicate.And(x => !string.IsNullOrEmpty(x.Ttm));
        //        var lstTTM = _rootDataMlns.Where(predicate.Compile()).GroupBy(n => n.Ttm).Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key });
        //        item.ItemsTTM = new ObservableCollection<ComboboxItem>(lstTTM);
        //    }

        //    // Cập nhật mục lục ngân sách id
        //    if (isUpdateMlnsId)
        //    {
        //        predicate = PredicateBuilder.True<NsMucLucNganSach>();
        //        predicate = predicate.And(x => !string.IsNullOrEmpty(item.LNS) && x.Lns.Equals(item.LNS));
        //        predicate = predicate.And(x => !string.IsNullOrEmpty(item.L) && x.L.Equals(item.L));
        //        predicate = predicate.And(x => !string.IsNullOrEmpty(item.K) && x.K.Equals(item.K));
        //        predicate = predicate.And(x => !string.IsNullOrEmpty(item.M) && x.M.Equals(item.M));
        //        predicate = predicate.And(x => string.IsNullOrEmpty(item.TM) || x.Tm.Equals(item.TM));
        //        predicate = predicate.And(x => string.IsNullOrEmpty(item.TTM) || x.Ttm.Equals(item.TTM));
        //        NsMucLucNganSach currentMlns = _rootDataMlns.FirstOrDefault(predicate.Compile());
        //        if (currentMlns != null)
        //        {
        //            item.IIdMlnsId = currentMlns.MlnsId;
        //            item.IIdMucLucNganSachId = currentMlns.Id;
        //        }
        //        else
        //        {
        //            item.IIdMlnsId = null;
        //            item.IIdMucLucNganSachId = null;
        //        }
        //    }
        //}

        private bool Validate()
        {
            return true;
        }

        //Ham tinh data tong hop
        private List<NHTHTongHop> LoadDataTongHop(List<NhTtThucHienNganSachGiaiDoanQuery> lstGiaiDoan)
        {
            if (lstGiaiDoan.Any())
            {
                var listGiaiDoan = new List<int?>();
                listGiaiDoan.AddRange(lstGiaiDoan.Where(w => w.iGiaiDoanTu != null).Select(x => x.iGiaiDoanTu - 1).ToList());
                listGiaiDoan.AddRange(lstGiaiDoan.Where(w => w.iGiaiDoanDen != null).Select(x => x.iGiaiDoanDen).ToList());

                List<NHTHTongHop> data = new List<NHTHTongHop>();
                var lstSMaNguon = new List<string> { NHConstants.MA_TH_BCTH_NS_GIAIDOAN };
                var predicate = PredicateBuilder.True<NHTHTongHop>();
                predicate = predicate.And(x => (lstSMaNguon.Contains(x.SMaNguon) || lstSMaNguon.Contains(x.SMaNguonCha)) && listGiaiDoan.Contains(x.INamKeHoach));

                return _nhThTongHopService.FindByCondition(predicate).ToList();

            }
            return null;

        }
    }
}
