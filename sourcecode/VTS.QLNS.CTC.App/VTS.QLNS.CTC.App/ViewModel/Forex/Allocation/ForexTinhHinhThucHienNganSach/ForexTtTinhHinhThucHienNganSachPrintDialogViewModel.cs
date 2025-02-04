using AutoMapper;
using FlexCel.Core;
using FlexCel.Report;
using FlexCel.XlsAdapter;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexTinhHinhThucHienNganSach
{
    public class ForexTtTinhHinhThucHienNganSachPrintDialogViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly string _typeChuky = TypeChuKy.RPT_NGOAIHOI_THUCHIEN_NGANSACH;
        private readonly INhTtThucHienNganSachService _nhTtThucHienNganSachService;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;

        public override Type ContentType => typeof(View.Forex.ForexAllocation.ForexTinhHinhThucHienNganSach.ForexTtTinhHinhThucHienNganSachPrintDialog);
        public override string Title => "Tinh hình thực hiện ngân sách";
        public override string Description => "Tình hình thực hiện ngân sách";
        private readonly string TITLE_FIRST_DEFAULT_VALUE = "BÁO CÁO THỰC HIỆN KINH PHÍ NGUỒN QUỸ DỰ TRỮ NGOẠI HỐI";
        private readonly string TITLE_SECOND_DEFAULT_VALUE = "";
        private readonly string TITLE_THIRD_DEFAULT_VALUE = "";
        private int _columnCountBC1 = 10;
        private DmChuKy _dmChuKy;
        public NhTtThucHienNganSachModel NhTtThucHienNganSachModel { get; set; }
        public int? tabindex { get; set; }
        public int? iNamPrint { get; set; }
        public int? iTuNamPrint { get; set; }
        public int? iDenNamPrint { get; set; }
        public int? iQuyPrint { get; set; }
        public Guid? iDonVi { get; set; }
        public bool InMotToChecked { get; set; }
        public DmChuKyDialogViewModel _dmChuKyDialogViewModel { get; }

        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ExportCommand { get; }

        private string _txtTitleFirst;
        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set => SetProperty(ref _txtTitleFirst, value);
        }

        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }
        private string _txtTitleThird;
        public string TxtTitleThird
        {
            get => _txtTitleThird;
            set => SetProperty(ref _txtTitleThird, value);
        }

        private readonly string _templatePath;

        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public ForexTtTinhHinhThucHienNganSachPrintDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            IDmChuKyService dmChuKyService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            INhTtThucHienNganSachService nhTtThucHienNganSachService,
            INsDonViService nsDonViService,
            IExportService exportService,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            INsNguonNganSachService nsNguonNganSachService
           )
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _dmChuKyService = dmChuKyService;
            _nhTtThucHienNganSachService = nhTtThucHienNganSachService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _configuration = configuration;
            _templatePath = _configuration.GetSection(ConfigHelper.TEMPLATE_XLXSPATH).Value;

            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            PrintReportCommand = new RelayCommand(obj => OnPrintReport(ExportType.PDF));
            ExportCommand = new RelayCommand(obj => OnPrintReport(ExportType.EXCEL));
            _dmChuKyDialogViewModel = dmChuKyDialogViewModel;
        }

        public override void Init()
        {
            LoadTitleFirst();
            base.Init();
        }
        private void LoadTitleFirst()
        {
            TxtTitleFirst = TITLE_FIRST_DEFAULT_VALUE;
            TxtTitleSecond = TITLE_SECOND_DEFAULT_VALUE;
            TxtTitleThird = TITLE_THIRD_DEFAULT_VALUE;
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
                TxtTitleFirst = chuKy != null ? (string.IsNullOrEmpty(chuKy.TieuDe1MoTa) ? TITLE_FIRST_DEFAULT_VALUE : chuKy.TieuDe1MoTa) : string.Empty;
                TxtTitleSecond = chuKy != null ? (string.IsNullOrEmpty(chuKy.TieuDe2MoTa) ? TITLE_SECOND_DEFAULT_VALUE : chuKy.TieuDe2MoTa) : string.Empty;
                TxtTitleThird = chuKy != null ? (string.IsNullOrEmpty(chuKy.TieuDe3MoTa) ? TITLE_THIRD_DEFAULT_VALUE : chuKy.TieuDe3MoTa) : string.Empty;
            };
            _dmChuKyDialogViewModel.Init();
            _dmChuKyDialogViewModel.ShowDialog();
        }

        private void OnPrintReport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    List<NhTtThucHienNganSachModel> list = _mapper.Map<ObservableCollection<NhTtThucHienNganSachModel>>(_nhTtThucHienNganSachService.ReportThucHienNganSach(tabindex, iQuyPrint, iNamPrint, iTuNamPrint, iDenNamPrint, iDonVi)).ToList();
                    List<NhTtThucHienNganSachModel> getlistGiaiDoan = list.Where(x => x.iGiaiDoanTu != null && x.iGiaiDoanDen != null).ToList();
                    List<NhTtThucHienNganSachGiaiDoanModel> lstGiaiDoan = getlistGiaiDoan
                        .GroupBy(x => (x.iGiaiDoanTu, x.iGiaiDoanDen)).Select(x => x.First())
                        .Select(x => new NhTtThucHienNganSachGiaiDoanModel
                        {
                            sGiaiDoan = "Giai đoạn " + x.iGiaiDoanTu + " - " + x.iGiaiDoanDen,
                            iGiaiDoanTu = x.iGiaiDoanTu,
                            iGiaiDoanDen = x.iGiaiDoanDen
                        }).ToList();
                    IsLoading = true;
                    if (tabindex == 1)
                    {
                        List<ExportResult> results = new List<ExportResult>();


                        if (InMotToChecked)
                        {
                            exportType = ExportType.PDF_ONE_PAPER;
                        }
                        var iTo = 0;
                        if (((10 + lstGiaiDoan.Count * 5) % _columnCountBC1) != 0)
                        {
                            iTo = lstGiaiDoan != null ? (((10 + lstGiaiDoan.Count * 5) / _columnCountBC1) + 1) : 1;

                        }
                        else
                        {
                            iTo = lstGiaiDoan != null ? (((10 + lstGiaiDoan.Count * 5) / _columnCountBC1)) : 1;
                        }
                        for (int i = 1; i <= iTo; i++)
                        {
                            string fileNamePrefixTo = string.Format("rptNgoaiHoi_ThucHienNganSachTo");
                            string fileNameWithoutExtensionTo = StringUtils.CreateExportFileName(fileNamePrefixTo);
                            var xlsFileto = TaoFileExel(list, lstGiaiDoan, i);
                            results.Add(new ExportResult(fileNameWithoutExtensionTo, fileNameWithoutExtensionTo, null, xlsFileto));
                        }
                        e.Result = results;
                    }
                    else
                    {
                        string fileNamePrefix = tabindex == 1 ? string.Format("rptNgoaiHoi_ThucHienNganSach_GiaiDoan") : string.Format("rptNgoaiHoi_ThucHienNganSach");
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = TaoFileExel(list, lstGiaiDoan);
                        e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                    }

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        if (tabindex == 1)
                        {
                            var result = (List<ExportResult>)e.Result;
                            _exportService.Open(result, exportType);
                        }
                        else
                        {
                            var result = (ExportResult)e.Result;
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

        public ExcelFile TaoFileExel(List<NhTtThucHienNganSachModel> list = null, List<NhTtThucHienNganSachGiaiDoanModel> lstGiaiDoan = null, int To = 1)
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NGOAIHOI_THUCHIEN_NGANSACH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

            List<NhTtThucHienNganSachModel> listData = getList(list, lstGiaiDoan);
            XlsFile Result = new XlsFile(true);
            if (tabindex == 1)
            {
                Result.Open(Path.Combine(_templatePath, ExportPrefix.PATH_TL_NGOAIHOI, ExportFileName.RPT_NGOAIHOI_THUCHIEN_NGANSACH_GIAIDOAN_TO));
            }
            else
            {
                Result.Open(Path.Combine(_templatePath, ExportPrefix.PATH_TL_NGOAIHOI, ExportFileName.RPT_NGOAIHOI_THUCHIEN_NGANSACH));

            }

            Header objHeader = new Header();
            objHeader.lstHeaderLv1 = new List<HeaderInfo>();
            objHeader.lstHeaderLv2 = new List<HeaderInfo>();
            objHeader.lstHeaderLv3 = new List<HeaderInfo>();
            var countColumn = 10 + lstGiaiDoan.Count() + (lstGiaiDoan.Count() * 2) + (lstGiaiDoan.Count() * 2);
            int ResultTo = (countColumn % _columnCountBC1) == 0 ? countColumn / _columnCountBC1 : (countColumn / _columnCountBC1) + 1;

            var listColumn = new List<NhTtThucHienNganSachGiaiDoanModel>();
            if (lstGiaiDoan != null)
            {
                for (var i = 1; i <= countColumn; i++)
                {
                    var startColumn1 = 3 + lstGiaiDoan.Count() + 1;
                    var startColumn2 = startColumn1 + (lstGiaiDoan.Count() * 2) + 2;
                    var startColumn3 = startColumn2 + (lstGiaiDoan.Count() * 2) + 2;

                    // header vl1 ;

                    if (i < 3)
                    {
                        objHeader.lstHeaderLv1.Add(new HeaderInfo
                        {
                            sTen = "Giá trị hợp đồng mua sắm hoặc dự toán được duyệt",
                        });
                        objHeader.lstHeaderLv3.Add(new HeaderInfo
                        {
                            sTen = i == 1 ? "USD" : "VND",
                        });
                    }
                    else if (i < startColumn1)
                    {
                        objHeader.lstHeaderLv1.Add(new HeaderInfo
                        {
                            sTen = "Kế hoạch sử dụng Quỹ dự trữ ngoại hối (NSĐB) được Thủ tướng Chính phủ phê duyệt (QĐ số.....) (*)",
                        });
                        objHeader.lstHeaderLv3.Add(new HeaderInfo
                        {
                            sTen = "USD",
                        });
                    }
                    else if (i < startColumn2)
                    {
                        objHeader.lstHeaderLv1.Add(new HeaderInfo
                        {
                            sTen = "Kinh phí được cấp",
                        });
                        objHeader.lstHeaderLv3.Add(new HeaderInfo
                        {
                            sTen = (startColumn2 - i) % 2 == 1 ? "VND" : "USD",
                        });
                    }
                    else if (i < startColumn3)
                    {
                        objHeader.lstHeaderLv1.Add(new HeaderInfo
                        {
                            sTen = "Kinh phí đã giải ngân (thanh toán, tạm ứng)",
                        });
                        objHeader.lstHeaderLv3.Add(new HeaderInfo
                        {
                            sTen = (startColumn3 - i) % 2 == 1 ? "VND" : "USD",
                        });
                    }
                    else if (i < countColumn)
                    {
                        objHeader.lstHeaderLv1.Add(new HeaderInfo
                        {
                            sTen = "Kinh phí chưa quyết toán",
                        });
                        objHeader.lstHeaderLv3.Add(new HeaderInfo
                        {
                            sTen = (countColumn - i) % 2 == 1 ? "VND" : "USD",
                        });
                    }
                    else if (i == countColumn)
                    {
                        objHeader.lstHeaderLv1.Add(new HeaderInfo
                        {
                            sTen = "Kế hoạch chưa giải ngân",
                        });
                        objHeader.lstHeaderLv3.Add(new HeaderInfo
                        {
                            sTen = "USD",
                        });
                    }
                    // listColumn 1 2 3 .......
                    if (i == 3)
                    {
                        NhTtThucHienNganSachGiaiDoanModel SoBC = new NhTtThucHienNganSachGiaiDoanModel();
                        var nowColumn = i.ToString() + " =";
                        for (var j = 1; j <= lstGiaiDoan.Count(); j++)
                        {
                            nowColumn += (i + j).ToString() + " +";
                        }
                        SoBC.sGiaiDoan = nowColumn.Remove(nowColumn.Length - 1);
                        listColumn.Add(SoBC);
                    }
                    else if (i == startColumn1 || i == startColumn1 + 1 || i == startColumn2 || i == startColumn2 + 1)
                    {
                        NhTtThucHienNganSachGiaiDoanModel SoBC = new NhTtThucHienNganSachGiaiDoanModel();
                        var nowColumn = i.ToString() + " =";
                        for (var j = 1; j <= lstGiaiDoan.Count(); j++)
                        {
                            nowColumn += (i + (j * 2)).ToString() + " +";
                        }
                        SoBC.sGiaiDoan = nowColumn.Remove(nowColumn.Length - 1);
                        listColumn.Add(SoBC);
                    }
                    else if (i == startColumn3)
                    {
                        NhTtThucHienNganSachGiaiDoanModel SoBC = new NhTtThucHienNganSachGiaiDoanModel();
                        var nowColumn = i.ToString() + " =" + (startColumn1).ToString() + " -" + (startColumn2).ToString();
                        SoBC.sGiaiDoan = nowColumn;
                        listColumn.Add(SoBC);
                    }
                    else if (i == startColumn3 + 1)
                    {
                        NhTtThucHienNganSachGiaiDoanModel SoBC = new NhTtThucHienNganSachGiaiDoanModel();
                        var nowColumn = i.ToString() + " =" + (startColumn1 + 1).ToString() + " -" + (startColumn2 + 1).ToString();
                        SoBC.sGiaiDoan = nowColumn;
                        listColumn.Add(SoBC);
                    }
                    else if (i == countColumn)
                    {
                        NhTtThucHienNganSachGiaiDoanModel SoBC = new NhTtThucHienNganSachGiaiDoanModel();
                        var nowColumn = i.ToString() + " =" + 3 + " -" + (startColumn1).ToString();
                        SoBC.sGiaiDoan = nowColumn;
                        listColumn.Add(SoBC);
                    }
                    else
                    {
                        NhTtThucHienNganSachGiaiDoanModel SoBC = new NhTtThucHienNganSachGiaiDoanModel();
                        SoBC.sGiaiDoan = i.ToString();
                        listColumn.Add(SoBC);
                    }
                }
            }
            #region headerlv2
            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Giá trị hợp đồng mua sắm hoặc dự toán được duyệt",
            });
            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Giá trị hợp đồng mua sắm hoặc dự toán được duyệt",
            });

            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Tổng số",
            });

            for (var i = 0; i < lstGiaiDoan.Count; i++)
            {
                objHeader.lstHeaderLv2.Add(new HeaderInfo
                {
                    sTen = lstGiaiDoan[i].sGiaiDoan,
                });
            }

            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Tổng số",
            });
            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Tổng số",
            });

            for (var i = 0; i < lstGiaiDoan.Count; i++)
            {
                objHeader.lstHeaderLv2.Add(new HeaderInfo
                {
                    sTen = lstGiaiDoan[i].sGiaiDoan,
                });
                objHeader.lstHeaderLv2.Add(new HeaderInfo
                {
                    sTen = lstGiaiDoan[i].sGiaiDoan,
                });
            }

            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Tổng số",
            });
            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Tổng số",
            });

            for (var i = 0; i < lstGiaiDoan.Count; i++)
            {
                objHeader.lstHeaderLv2.Add(new HeaderInfo
                {
                    sTen = lstGiaiDoan[i].sGiaiDoan,
                });
                objHeader.lstHeaderLv2.Add(new HeaderInfo
                {
                    sTen = lstGiaiDoan[i].sGiaiDoan,
                });
            }


            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Kinh phí chưa quyết toán",
            });
            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Kinh phí chưa quyết toán",
            });
            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Kế hoạch chưa giải ngân",
            });
            #endregion

            var iQuy = iQuyPrint == -2 ? "" : iQuyPrint.ToString();
            var iNam = iNamPrint == -2 ? "" : iNamPrint.ToString();
            var iTuNam = iTuNamPrint == -2 ? "" : iTuNamPrint.ToString();
            var iDenNam = iDenNamPrint == -2 ? "" : iDenNamPrint.ToString();
            FlexCelReport fr = new FlexCelReport();

            int columnStart = _columnCountBC1 * (To - 1);

            for (int i = 1; i <= _columnCountBC1; i++)
            {
                if (columnStart + i <= objHeader.lstHeaderLv1.Count)
                {
                    fr.SetValue(string.Format("HeaderA{0}", i), objHeader.lstHeaderLv1[columnStart + i - 1].sTen);
                    fr.SetValue(string.Format("HeaderB{0}", i), objHeader.lstHeaderLv2[columnStart + i - 1].sTen);
                    fr.SetValue(string.Format("HeaderC{0}", i), objHeader.lstHeaderLv3[columnStart + i - 1].sTen);
                }
                else
                {
                    fr.SetValue(string.Format("HeaderA{0}", i), "");
                    fr.SetValue(string.Format("HeaderB{0}", i), "");
                    fr.SetValue(string.Format("HeaderC{0}", i), "");
                }
            }

            fr.SetValue(
                "iQuy", iQuy
            );
            fr.SetValue(
                "iNam", iNam
            );
            fr.SetValue(
                "iTuNam", iTuNam
            );
            fr.SetValue(
                "iDenNam", iDenNam
            );
            fr.SetValue(
                "dvt", "1"
            );
            fr.SetValue(
                "To", To
            );
            fr.SetValue(
                "ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty
            );
            fr.SetValue(
                "ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty
            );
            fr.SetValue(
                "ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty
            );
            fr.SetValue(
                "TenBaoCao1", TxtTitleFirst
            );
            fr.SetValue(
                "TenBaoCao2", TxtTitleSecond
            );
            fr.SetValue(
                "TenBaoCao3", TxtTitleThird
            );
            fr.SetValue(
                "DonViCapTren", _sessionService.Current.TenDonViTrucThuocReportHeader
            );
            fr.SetValue(
                "DonVi", _sessionService.Current.TenDonViReportHeader
            );

            foreach (var item in listData)
            {
                item.lstData = new List<NhTtThucHienNganSachGiaiDoanModel>();
                item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(item.HopDongUSD, 2, false) });
                item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(item.HopDongVND, 0, false) });
                item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(item.NCVTTCP, 2, false) });

                foreach (var giaidoan in item.lstGiaiDoanTTCP)
                {
                    item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(giaidoan.valueUSD, 2, false) });
                }
                item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(item.fLuyKeKinhPhiDuocCap_USD, 2, false) });
                item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(item.fLuyKeKinhPhiDuocCap_VND, 0, false) });

                foreach (var giaidoan in item.lstGiaiDoanKinhPhiDuocCap)
                {
                    item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(giaidoan.valueUSD, 2, false) });
                    item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(giaidoan.valueVND, 0, false) });
                }

                item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(item.fDeNghiQTNamNay_USD, 2, false) });
                item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(item.fDeNghiQTNamNay_VND, 0, false) });

                foreach (var giaidoan in item.lstGiaiDoanKinhPhiDaGiaiNgan)
                {
                    item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(giaidoan.valueUSD, 2, false) });
                    item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(giaidoan.valueVND, 0, false) });
                }

                item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(item.KinhPhiChuaQuyetToanUSD, 2, false) });
                item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(item.KinhPhiChuaQuyetToanVND, 0, false) });
                item.lstData.Add(new NhTtThucHienNganSachGiaiDoanModel() { sGiaiDoan = StringUtils.DinhDangSo(item.KeHoachGiaiNgan, 2, false) });

                item.lstData = item.lstData.Skip(columnStart).Take(_columnCountBC1).ToList();

            }
            listColumn = listColumn.Skip(columnStart).Take(_columnCountBC1).ToList();
            fr.AddTable<NhTtThucHienNganSachModel>("dt", listData);
            fr.AddTable<NhTtThucHienNganSachGiaiDoanModel>("listColumn", listColumn);
            fr.Run(Result);
            //count merge cột
            //tạo border format
            if (tabindex == 1)
            {
                if (To == 1)
                {
                    Result.MergeCells(6, 4, 7, 5);

                }
                if (To == ResultTo)
                {
                    Result.MergeCells(6, countColumn - _columnCountBC1 * (To - 1) + 3, 7, countColumn - _columnCountBC1 * (To - 1) + 3);
                    if (((countColumn - 4) / _columnCountBC1) + 1 == To)
                    {
                        Result.MergeCells(6, countColumn - _columnCountBC1 * (To - 1) + 1, 7, countColumn - _columnCountBC1 * (To - 1) + 2);
                    }
                    else if (((countColumn - 3) / _columnCountBC1) + 1 == To)
                    {
                        Result.MergeCells(6, countColumn - _columnCountBC1 * (To - 1) + 1, 7, countColumn - _columnCountBC1 * (To - 1) + 1);
                        Result.MergeCells(6, countColumn - _columnCountBC1 * (To - 1) + 2, 7, countColumn - _columnCountBC1 * (To - 1) + 2);
                    }
                }

                Result = MergeH(Result, 6, 4, 12);
                Result = MergeH(Result, 7, 4, 12);

                Result = MergeC(Result, 6, 5, 6, 12);

                //tạo border format
                var b = Result.GetDefaultFormat;

                var ApplyFormat = new TFlxApplyFormat();
                ApplyFormat.SetAllMembers(false);
                //ApplyFormat.Borders.SetAllMembers(true);
                TCellAddress Cell = null;
                //tìm dòng cuối cùng của bảng
                Cell = Result.Find("Cộng", null, Cell, false, true, true, false);
                //set border cho bảng
                Result.SetCellFormat(6, 1, 9 + listData.Count(), 7 + lstGiaiDoan.Count() * 3, b, ApplyFormat, false);
            }

            return Result;
        }

        public List<NhTtThucHienNganSachModel> getList(List<NhTtThucHienNganSachModel> list, List<NhTtThucHienNganSachGiaiDoanModel> lstGiaiDoan)
        {
            var tabTable = tabindex;

            List<NhTtThucHienNganSachModel> listData = new List<NhTtThucHienNganSachModel>().ToList();
            int SttLoai = 0;
            int SttHopDong = 0;
            int SttDuAn = 0;
            int SttChuongTrinh = 0;
            Guid? idDuAn = null;
            Guid? idHopDong = null;
            Guid? idChuongTrinh = null;
            int? idLoai = null;
            int sttTong = 0;
            List<NhTtThucHienNganSachModel> listTong = list;
            NhTtThucHienNganSachModel DataTong = new NhTtThucHienNganSachModel();
            DataTong.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
            DataTong.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
            DataTong.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();

            DataTong.HopDongUSD = listTong.GroupBy(x => new { x.IDHopDong, x.iLoaiNoiDungChi }).Select(x => x.First()).Sum(x => x.HopDongUSD);
            DataTong.HopDongVND = listTong.GroupBy(x => new { x.IDHopDong, x.iLoaiNoiDungChi }).Select(x => x.First()).Sum(x => x.HopDongVND);

            DataTong.NCVTTCP = listTong.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP);
            DataTong.NhiemVuChi = listTong.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NhiemVuChi);
            DataTong.KinhPhiUSD = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiUSD);
            DataTong.KinhPhiVND = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiVND);
            DataTong.KinhPhiToYUSD = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYUSD);
            DataTong.KinhPhiToYVND = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYVND);
            DataTong.KinhPhiDaChiUSD = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiUSD);
            DataTong.KinhPhiDaChiVND = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiVND);
            DataTong.KinhPhiDaChiToYUSD = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYUSD);
            DataTong.KinhPhiDaChiToYVND = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYVND);
            DataTong.TongKinhPhiUSD = DataTong.KinhPhiUSD + DataTong.KinhPhiToYUSD;
            DataTong.TongKinhPhiVND = DataTong.KinhPhiVND + DataTong.KinhPhiToYVND;

            DataTong.TongKinhPhiDaChiUSD = DataTong.KinhPhiDaChiUSD + DataTong.KinhPhiDaChiToYUSD;
            DataTong.TongKinhPhiDaChiVND = DataTong.KinhPhiDaChiVND + DataTong.KinhPhiDaChiToYVND;

            DataTong.KinhPhiDuocCapChuaChiUSD = DataTong.TongKinhPhiUSD - DataTong.TongKinhPhiDaChiUSD;
            DataTong.KinhPhiDuocCapChuaChiVND = DataTong.TongKinhPhiVND - DataTong.TongKinhPhiDaChiVND;
            DataTong.QuyGiaiNganTheoQuy = DataTong.NhiemVuChi - DataTong.TongKinhPhiUSD;

            if (lstGiaiDoan != null)
            {
                foreach (var giaiDoan in lstGiaiDoan)
                {
                    List<NhTtThucHienNganSachModel> listDataChaGiaiDoan = listTong.Where(x => x.iGiaiDoanTu == giaiDoan.iGiaiDoanTu && x.iGiaiDoanDen == giaiDoan.iGiaiDoanDen).ToList();
                    DataTong.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) });
                    DataTong.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD), valueVND = listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) });
                    DataTong.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD), valueVND = listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) });
                }
            }
            if (list != null)
            {
                foreach (var item in list)
                {
                    sttTong++;
                    item.TongKinhPhiUSD = item.KinhPhiUSD + item.KinhPhiToYUSD;
                    item.TongKinhPhiVND = item.KinhPhiVND + item.KinhPhiToYVND;

                    item.TongKinhPhiDaChiUSD = item.KinhPhiDaChiUSD + item.KinhPhiDaChiToYUSD;
                    item.TongKinhPhiDaChiVND = item.KinhPhiDaChiVND + item.KinhPhiDaChiToYVND;

                    item.KinhPhiDuocCapChuaChiUSD = item.TongKinhPhiUSD - item.TongKinhPhiDaChiUSD;
                    item.KinhPhiDuocCapChuaChiVND = item.TongKinhPhiVND - item.TongKinhPhiDaChiVND;
                    item.QuyGiaiNganTheoQuy = item.NhiemVuChi - item.TongKinhPhiUSD;

                    item.KinhPhiChuaQuyetToanUSD = item.fLuyKeKinhPhiDuocCap_USD - item.fDeNghiQTNamNay_USD;
                    item.KinhPhiChuaQuyetToanVND = item.fLuyKeKinhPhiDuocCap_VND - item.fDeNghiQTNamNay_VND;
                    item.KeHoachGiaiNgan = item.NCVTTCP - item.fLuyKeKinhPhiDuocCap_USD;

                    if (lstGiaiDoan != null)
                    {
                        item.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        item.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        item.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        foreach (var giaiDoan in lstGiaiDoan)
                        {

                            if (item.iGiaiDoanTu == giaiDoan.iGiaiDoanTu && item.iGiaiDoanDen == giaiDoan.iGiaiDoanDen)
                            {

                                item.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = item.NCVTTCP });
                                item.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = item.fLuyKeKinhPhiDuocCap_USD, valueVND = item.fLuyKeKinhPhiDuocCap_VND });
                                item.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = item.fDeNghiQTNamNay_USD, valueVND = item.fDeNghiQTNamNay_VND });
                            }
                            else
                            {
                                item.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0 });
                                item.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0, valueVND = 0 });
                                item.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0, valueVND = 0 });
                            }
                        }
                    }
                    if (item.IDNhiemVuChi != idChuongTrinh/* && item.IDNhiemVuChi != Guid.Empty*/)
                    {
                        SttChuongTrinh++;
                        SttDuAn = 0;
                        SttLoai = 0;
                        SttDuAn = 0;
                        idDuAn = null;
                        idLoai = null;
                        idHopDong = null;
                        NhTtThucHienNganSachModel DataCha = new NhTtThucHienNganSachModel();

                        List<NhTtThucHienNganSachModel> listDataCha = list.Where(x => x.IDNhiemVuChi == item.IDNhiemVuChi).ToList();
                        //DataCha.HopDongUSD = listDataCha.Sum(x => x.HopDongUSD);
                        //DataCha.HopDongVND = listDataCha.Sum(x => x.HopDongVND);

                        DataCha.KinhPhiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiUSD);
                        DataCha.KinhPhiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiVND);
                        DataCha.KinhPhiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYUSD);
                        DataCha.KinhPhiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYVND);
                        DataCha.KinhPhiDaChiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiUSD);
                        DataCha.KinhPhiDaChiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiVND);
                        DataCha.KinhPhiDaChiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYUSD);
                        DataCha.KinhPhiDaChiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYVND);

                        DataCha.fLuyKeKinhPhiDuocCap_USD = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_USD);
                        DataCha.fLuyKeKinhPhiDuocCap_VND = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_VND);
                        DataCha.fDeNghiQTNamNay_USD = listDataCha.Sum(x => x.fDeNghiQTNamNay_USD);
                        DataCha.fDeNghiQTNamNay_VND = listDataCha.Sum(x => x.fDeNghiQTNamNay_VND);

                        //DataCha.NCVTTCP = listDataCha.Sum(x => x.NCVTTCP);
                        //DataCha.NhiemVuChi = listDataCha.Sum(x => x.NhiemVuChi);
                        DataCha.NCVTTCP = item.NCVTTCP;
                        DataCha.NhiemVuChi = item.NhiemVuChi;

                        DataCha.TongKinhPhiUSD = DataCha.KinhPhiUSD + DataCha.KinhPhiToYUSD;
                        DataCha.TongKinhPhiVND = DataCha.KinhPhiVND + DataCha.KinhPhiToYVND;

                        DataCha.TongKinhPhiDaChiUSD = DataCha.KinhPhiDaChiUSD + DataCha.KinhPhiDaChiToYUSD;
                        DataCha.TongKinhPhiDaChiVND = DataCha.KinhPhiDaChiVND + DataCha.KinhPhiDaChiToYVND;

                        DataCha.KinhPhiDuocCapChuaChiUSD = DataCha.TongKinhPhiUSD - DataCha.TongKinhPhiDaChiUSD;
                        DataCha.KinhPhiDuocCapChuaChiVND = DataCha.TongKinhPhiVND - DataCha.TongKinhPhiDaChiVND;
                        DataCha.QuyGiaiNganTheoQuy = DataCha.NhiemVuChi - DataCha.TongKinhPhiUSD;

                        DataCha.KinhPhiChuaQuyetToanUSD = DataCha.fLuyKeKinhPhiDuocCap_USD - DataCha.fDeNghiQTNamNay_USD;
                        DataCha.KinhPhiChuaQuyetToanVND = DataCha.fLuyKeKinhPhiDuocCap_VND - DataCha.fDeNghiQTNamNay_VND;
                        DataCha.KeHoachGiaiNgan = DataCha.NCVTTCP - DataCha.fLuyKeKinhPhiDuocCap_USD;

                        if (item.IDNhiemVuChi != Guid.Empty)
                        {
                            DataCha.sTenNoiDungChi = item.sTenNhiemVuChi;
                        }
                        else
                        {
                            DataCha.sTenNoiDungChi = "Nội dung chi khác";
                        }
                        DataCha.depth = convertLetter(SttChuongTrinh) + ".";
                        DataCha.isTitle = "font-bold-red";
                        idChuongTrinh = item.IDNhiemVuChi;
                        DataCha.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.iGiaiDoanDen = item.iGiaiDoanDen;
                        DataCha.iGiaiDoanTu = item.iGiaiDoanTu;
                        if (lstGiaiDoan != null)
                        {
                            foreach (var giaiDoan in lstGiaiDoan)
                            {
                                List<NhTtThucHienNganSachModel> listDataChaGiaiDoan = listDataCha.Where(x => x.iGiaiDoanTu == giaiDoan.iGiaiDoanTu && x.iGiaiDoanDen == giaiDoan.iGiaiDoanDen).ToList();
                                if (listDataChaGiaiDoan.Count != 0)
                                {
                                    DataCha.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = item.NCVTTCP });
                                    DataCha.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD), valueVND = listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) });
                                    DataCha.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD), valueVND = listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) });
                                }
                                else
                                {
                                    DataCha.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0 });
                                    DataCha.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0, valueVND = 0 });
                                    DataCha.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0, valueVND = 0 });
                                }

                            }
                        }

                        listData.Add(DataCha);
                    }
                    if (item.IDDuAn != idDuAn /*&& item.IDDuAn != Guid.Empty*/)
                    {
                        SttDuAn++;
                        SttLoai = 0;
                        SttHopDong = 0;
                        idLoai = null;
                        idHopDong = null;
                        NhTtThucHienNganSachModel DataCha = new NhTtThucHienNganSachModel();
                        List<NhTtThucHienNganSachModel> listDataCha = list.Where(x => x.IDDuAn == item.IDDuAn && x.IDNhiemVuChi == item.IDNhiemVuChi).ToList();

                        DataCha.HopDongUSD = listDataCha.GroupBy(x => new { x.IDHopDong, x.iLoaiNoiDungChi }).Select(x => x.First()).Sum(x => x.HopDongUSD);
                        DataCha.HopDongVND = listDataCha.GroupBy(x => new { x.IDHopDong, x.iLoaiNoiDungChi }).Select(x => x.First()).Sum(x => x.HopDongVND);

                        DataCha.KinhPhiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiUSD);
                        DataCha.KinhPhiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiVND);
                        DataCha.KinhPhiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYUSD);
                        DataCha.KinhPhiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYVND);
                        DataCha.KinhPhiDaChiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiUSD);
                        DataCha.KinhPhiDaChiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiVND);
                        DataCha.KinhPhiDaChiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYUSD);
                        DataCha.KinhPhiDaChiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYVND);

                        DataCha.fLuyKeKinhPhiDuocCap_USD = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_USD);
                        DataCha.fLuyKeKinhPhiDuocCap_VND = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_VND);
                        DataCha.fDeNghiQTNamNay_USD = listDataCha.Sum(x => x.fDeNghiQTNamNay_USD);
                        DataCha.fDeNghiQTNamNay_VND = listDataCha.Sum(x => x.fDeNghiQTNamNay_VND);

                        //DataCha.NCVTTCP = listDataCha.Sum(x => x.NCVTTCP);
                        //DataCha.NhiemVuChi = listDataCha.Sum(x => x.NhiemVuChi);

                        DataCha.TongKinhPhiUSD = DataCha.KinhPhiUSD + DataCha.KinhPhiToYUSD;
                        DataCha.TongKinhPhiVND = DataCha.KinhPhiVND + DataCha.KinhPhiToYVND;

                        DataCha.TongKinhPhiDaChiUSD = DataCha.KinhPhiDaChiUSD + DataCha.KinhPhiDaChiToYUSD;
                        DataCha.TongKinhPhiDaChiVND = DataCha.KinhPhiDaChiVND + DataCha.KinhPhiDaChiToYVND;

                        DataCha.KinhPhiDuocCapChuaChiUSD = DataCha.TongKinhPhiUSD - DataCha.TongKinhPhiDaChiUSD;
                        DataCha.KinhPhiDuocCapChuaChiVND = DataCha.TongKinhPhiVND - DataCha.TongKinhPhiDaChiVND;
                        //DataCha.QuyGiaiNganTheoQuy = DataCha.NhiemVuChi - DataCha.TongKinhPhiUSD;

                        DataCha.KinhPhiChuaQuyetToanUSD = DataCha.fLuyKeKinhPhiDuocCap_USD - DataCha.fDeNghiQTNamNay_USD;
                        DataCha.KinhPhiChuaQuyetToanVND = DataCha.fLuyKeKinhPhiDuocCap_VND - DataCha.fDeNghiQTNamNay_VND;
                        //DataCha.KeHoachGiaiNgan = DataCha.NCVTTCP - DataCha.fLuyKeKinhPhiDuocCap_USD;

                        if (item.IDDuAn != Guid.Empty)
                        {
                            DataCha.sTenNoiDungChi = item.sTenDuAn;
                        }
                        else if (item.IDHopDong != Guid.Empty)
                        {
                            DataCha.sTenNoiDungChi = "Chi hợp đồng";
                        }
                        else
                        {
                            DataCha.sTenNoiDungChi = "Chi khác";
                        }
                        DataCha.sTenCDT = item.sTenCDT;
                        DataCha.isTitle = "font-bold";
                        DataCha.isDuAn = true;
                        DataCha.depth = _nhTtThucHienNganSachService.GetSTTLAMA(SttDuAn) + ".";
                        idDuAn = item.IDDuAn;
                        DataCha.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();

                        DataCha.iGiaiDoanDen = item.iGiaiDoanDen;
                        DataCha.iGiaiDoanTu = item.iGiaiDoanTu;
                        if (lstGiaiDoan != null)
                        {
                            foreach (var giaiDoan in lstGiaiDoan)
                            {
                                List<NhTtThucHienNganSachModel> listDataChaGiaiDoan = listDataCha.Where(x => x.iGiaiDoanTu == giaiDoan.iGiaiDoanTu && x.iGiaiDoanDen == giaiDoan.iGiaiDoanDen).ToList();
                                if (listDataChaGiaiDoan.Count != 0)
                                {
                                    DataCha.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0 });
                                    DataCha.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD), valueVND = listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) });
                                    DataCha.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD), valueVND = listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) });
                                }
                                else
                                {
                                    DataCha.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0 });
                                    DataCha.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0, valueVND = 0 });
                                    DataCha.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0, valueVND = 0 });
                                }

                            }
                        }

                        listData.Add(DataCha);
                    }
                    if (item.iLoaiNoiDungChi != idLoai && item.iLoaiNoiDungChi != 0)
                    {
                        SttLoai++;
                        SttHopDong = 0;
                        idHopDong = null;
                        NhTtThucHienNganSachModel DataCha = new NhTtThucHienNganSachModel();
                        List<NhTtThucHienNganSachModel> listDataCha = list.Where(x => x.iLoaiNoiDungChi == item.iLoaiNoiDungChi && x.IDDuAn == item.IDDuAn && x.IDNhiemVuChi == item.IDNhiemVuChi).ToList();

                        //DataCha.HopDongUSD = listDataCha.Sum(x => x.HopDongUSD);
                        //DataCha.HopDongVND = listDataCha.Sum(x => x.HopDongVND);

                        DataCha.KinhPhiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiUSD);
                        DataCha.KinhPhiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiVND);
                        DataCha.KinhPhiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYUSD);
                        DataCha.KinhPhiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYVND);
                        DataCha.KinhPhiDaChiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiUSD);
                        DataCha.KinhPhiDaChiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiVND);
                        DataCha.KinhPhiDaChiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYUSD);
                        DataCha.KinhPhiDaChiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYVND);

                        DataCha.fLuyKeKinhPhiDuocCap_USD = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_USD);
                        DataCha.fLuyKeKinhPhiDuocCap_VND = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_VND);
                        DataCha.fDeNghiQTNamNay_USD = listDataCha.Sum(x => x.fDeNghiQTNamNay_USD);
                        DataCha.fDeNghiQTNamNay_VND = listDataCha.Sum(x => x.fDeNghiQTNamNay_VND);

                        //DataCha.NCVTTCP = listDataCha.Sum(x => x.NCVTTCP);
                        //DataCha.NhiemVuChi = listDataCha.Sum(x => x.NhiemVuChi);

                        DataCha.TongKinhPhiUSD = DataCha.KinhPhiUSD + DataCha.KinhPhiToYUSD;
                        DataCha.TongKinhPhiVND = DataCha.KinhPhiVND + DataCha.KinhPhiToYVND;

                        DataCha.TongKinhPhiDaChiUSD = DataCha.KinhPhiDaChiUSD + DataCha.KinhPhiDaChiToYUSD;
                        DataCha.TongKinhPhiDaChiVND = DataCha.KinhPhiDaChiVND + DataCha.KinhPhiDaChiToYVND;

                        DataCha.KinhPhiDuocCapChuaChiUSD = DataCha.TongKinhPhiUSD - DataCha.TongKinhPhiDaChiUSD;
                        DataCha.KinhPhiDuocCapChuaChiVND = DataCha.TongKinhPhiVND - DataCha.TongKinhPhiDaChiVND;
                        //DataCha.QuyGiaiNganTheoQuy = DataCha.NhiemVuChi - DataCha.TongKinhPhiUSD;

                        DataCha.KinhPhiChuaQuyetToanUSD = DataCha.fLuyKeKinhPhiDuocCap_USD - DataCha.fDeNghiQTNamNay_USD;
                        DataCha.KinhPhiChuaQuyetToanVND = DataCha.fLuyKeKinhPhiDuocCap_VND - DataCha.fDeNghiQTNamNay_VND;
                        //DataCha.KeHoachGiaiNgan = DataCha.NCVTTCP - DataCha.fLuyKeKinhPhiDuocCap_USD;

                        if (item.iLoaiNoiDungChi == 1)
                        {
                            DataCha.sTenNoiDungChi = "Chi ngoại tệ";
                        }
                        else if (item.iLoaiNoiDungChi == 2)
                        {
                            DataCha.sTenNoiDungChi = "Chi trong nước";
                        }
                        else
                        {
                            DataCha.sTenNoiDungChi = "Chi khác";
                        }
                        DataCha.depth = SttLoai.ToString() + ".";
                        DataCha.isTitle = "font-bold";
                        idLoai = item.iLoaiNoiDungChi;
                        DataCha.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();

                        DataCha.iGiaiDoanDen = item.iGiaiDoanDen;
                        DataCha.iGiaiDoanTu = item.iGiaiDoanTu;
                        if (lstGiaiDoan != null)
                        {
                            foreach (var giaiDoan in lstGiaiDoan)
                            {
                                List<NhTtThucHienNganSachModel> listDataChaGiaiDoan = listDataCha.Where(x => x.iGiaiDoanTu == giaiDoan.iGiaiDoanTu && x.iGiaiDoanDen == giaiDoan.iGiaiDoanDen).ToList();
                                if (listDataChaGiaiDoan.Count != 0)
                                {
                                    DataCha.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0 });
                                    DataCha.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD), valueVND = listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) });
                                    DataCha.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD), valueVND = listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) });
                                }
                                else
                                {
                                    DataCha.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0 });
                                    DataCha.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0, valueVND = 0 });
                                    DataCha.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0, valueVND = 0 });
                                }

                            }
                        }

                        listData.Add(DataCha);
                    }
                    if (item.IDHopDong != idHopDong && item.IDHopDong != Guid.Empty)
                    {
                        SttHopDong++;
                        NhTtThucHienNganSachModel DataCha = new NhTtThucHienNganSachModel();
                        List<NhTtThucHienNganSachModel> listDataCha = list.Where(x => x.IDHopDong == item.IDHopDong && x.iLoaiNoiDungChi == item.iLoaiNoiDungChi && x.IDDuAn == item.IDDuAn && x.IDNhiemVuChi == item.IDNhiemVuChi).ToList();

                        DataCha.HopDongUSD = item.HopDongUSD;
                        DataCha.HopDongVND = item.HopDongVND;

                        DataCha.KinhPhiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiUSD);
                        DataCha.KinhPhiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiVND);
                        DataCha.KinhPhiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYUSD);
                        DataCha.KinhPhiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYVND);
                        DataCha.KinhPhiDaChiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiUSD);
                        DataCha.KinhPhiDaChiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiVND);
                        DataCha.KinhPhiDaChiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYUSD);
                        DataCha.KinhPhiDaChiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYVND);

                        DataCha.fLuyKeKinhPhiDuocCap_USD = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_USD);
                        DataCha.fLuyKeKinhPhiDuocCap_VND = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_VND);
                        DataCha.fDeNghiQTNamNay_USD = listDataCha.Sum(x => x.fDeNghiQTNamNay_USD);
                        DataCha.fDeNghiQTNamNay_VND = listDataCha.Sum(x => x.fDeNghiQTNamNay_VND);

                        //DataCha.NCVTTCP = listDataCha.Sum(x => x.NCVTTCP);
                        //DataCha.NhiemVuChi = listDataCha.Sum(x => x.NhiemVuChi);

                        DataCha.TongKinhPhiUSD = DataCha.KinhPhiUSD + DataCha.KinhPhiToYUSD;
                        DataCha.TongKinhPhiVND = DataCha.KinhPhiVND + DataCha.KinhPhiToYVND;

                        DataCha.TongKinhPhiDaChiUSD = DataCha.KinhPhiDaChiUSD + DataCha.KinhPhiDaChiToYUSD;
                        DataCha.TongKinhPhiDaChiVND = DataCha.KinhPhiDaChiVND + DataCha.KinhPhiDaChiToYVND;

                        DataCha.KinhPhiDuocCapChuaChiUSD = DataCha.TongKinhPhiUSD - DataCha.TongKinhPhiDaChiUSD;
                        DataCha.KinhPhiDuocCapChuaChiVND = DataCha.TongKinhPhiVND - DataCha.TongKinhPhiDaChiVND;
                        //DataCha.QuyGiaiNganTheoQuy = DataCha.NhiemVuChi - DataCha.TongKinhPhiUSD;

                        DataCha.KinhPhiChuaQuyetToanUSD = DataCha.fLuyKeKinhPhiDuocCap_USD - DataCha.fDeNghiQTNamNay_USD;
                        DataCha.KinhPhiChuaQuyetToanVND = DataCha.fLuyKeKinhPhiDuocCap_VND - DataCha.fDeNghiQTNamNay_VND;
                        //DataCha.KeHoachGiaiNgan = DataCha.NCVTTCP - DataCha.fLuyKeKinhPhiDuocCap_USD;

                        DataCha.sTenNoiDungChi = item.sTenHopDong;
                        DataCha.isHopDong = true;
                        DataCha.depth = SttLoai.ToString() + "." + SttHopDong.ToString() + ".";
                        DataCha.isTitle = "font-bold";
                        idHopDong = item.IDHopDong;
                        DataCha.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();

                        DataCha.iGiaiDoanDen = item.iGiaiDoanDen;
                        DataCha.iGiaiDoanTu = item.iGiaiDoanTu;
                        if (lstGiaiDoan != null)
                        {
                            foreach (var giaiDoan in lstGiaiDoan)
                            {
                                List<NhTtThucHienNganSachModel> listDataChaGiaiDoan = listDataCha.Where(x => x.iGiaiDoanTu == giaiDoan.iGiaiDoanTu && x.iGiaiDoanDen == giaiDoan.iGiaiDoanDen).ToList();
                                if (listDataChaGiaiDoan.Count != 0)
                                {
                                    DataCha.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0 });
                                    DataCha.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD), valueVND = listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) });
                                    DataCha.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD), valueVND = listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) });
                                }
                                else
                                {
                                    DataCha.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0 });
                                    DataCha.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0, valueVND = 0 });
                                    DataCha.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0, valueVND = 0 });
                                }

                            }
                        }

                        listData.Add(DataCha);
                    }




                    //DataTong.KinhPhiUSD += item.KinhPhiUSD;
                    //DataTong.KinhPhiVND += item.KinhPhiVND;
                    //DataTong.KinhPhiToYUSD += item.KinhPhiToYUSD;
                    //DataTong.KinhPhiToYVND += item.KinhPhiToYVND;
                    //DataTong.KinhPhiDaChiUSD += item.KinhPhiDaChiUSD;
                    //DataTong.KinhPhiDaChiVND += item.KinhPhiDaChiVND;
                    //DataTong.KinhPhiDaChiToYUSD += item.KinhPhiDaChiToYUSD;
                    //DataTong.KinhPhiDaChiToYVND += item.KinhPhiDaChiToYVND;
                    //DataTong.KinhPhiDuocCapChuaChiUSD += item.KinhPhiDuocCapChuaChiUSD;
                    //DataTong.KinhPhiDuocCapChuaChiVND += item.KinhPhiDuocCapChuaChiVND;


                    //DataTong.TongKinhPhiUSD += item.TongKinhPhiUSD;
                    //DataTong.TongKinhPhiVND += item.TongKinhPhiVND;

                    //DataTong.TongKinhPhiDaChiUSD += item.TongKinhPhiDaChiUSD;
                    //DataTong.TongKinhPhiDaChiVND += item.TongKinhPhiDaChiVND;
                    //DataTong.QuyGiaiNganTheoQuy += item.QuyGiaiNganTheoQuy;

                    DataTong.fLuyKeKinhPhiDuocCap_USD += item.fLuyKeKinhPhiDuocCap_USD;
                    DataTong.fLuyKeKinhPhiDuocCap_VND += item.fLuyKeKinhPhiDuocCap_VND;
                    DataTong.fDeNghiQTNamNay_USD += item.fDeNghiQTNamNay_USD;
                    DataTong.fDeNghiQTNamNay_VND += item.fDeNghiQTNamNay_VND;

                    DataTong.KinhPhiChuaQuyetToanUSD += item.KinhPhiChuaQuyetToanUSD;
                    DataTong.KinhPhiChuaQuyetToanVND += item.KinhPhiChuaQuyetToanVND;

                    //DataTong.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
                    //DataTong.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
                    //DataTong.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();
                    //DataTong.iGiaiDoanDen = item.iGiaiDoanDen;
                    //DataTong.iGiaiDoanTu = item.iGiaiDoanTu;

                    if (tabTable == 1)
                    {
                        item.sTenCDT = "";
                        item.HopDongUSD = 0;
                        item.HopDongVND = 0;
                        item.NCVTTCP = 0;
                        item.NhiemVuChi = 0;
                        item.KinhPhiDuocCapChuaChiUSD = 0;
                        item.KinhPhiDuocCapChuaChiVND = 0;
                        item.QuyGiaiNganTheoQuy = 0;
                        item.KeHoachGiaiNgan = 0;
                        listData.Add(item);
                    }

                    if (sttTong == list.Count())
                    {
                        DataTong.KeHoachGiaiNgan = DataTong.NCVTTCP - DataTong.fLuyKeKinhPhiDuocCap_USD;
                        DataTong.sTenNoiDungChi = "Tổng Cộng: ";
                        DataTong.isDuAn = true;
                        DataTong.isTitle = "font-bold";
                        DataTong.isSum = true;
                        listData.Add(DataTong);
                    }
                }
            }

            return listData;
        }
        private string convertLetter(int input)
        {
            StringBuilder res = new StringBuilder((input - 1).ToString());
            for (int j = 0; j < res.Length; j++)
                res[j] += (char)(17); // '0' is 48, 'A' is 65
            return res.ToString();
        }

        public XlsFile MergeH(XlsFile xls, int row, int col, int length)
        {
            var x = col;
            var y = row;

            var x_to = x;
            var x_from = x;

            var dic = new Dictionary<int, int>();

            var x_to_temp = x_from;

            for (int i = 0; i < length; i++)
            {
                var cell1 = xls.GetCellValue(y, x + i);
                var cell2 = xls.GetCellValue(y, x + i + 1);

                if (cell1 == cell2 && cell2 != null && cell1 != null)
                {
                    x_to = x_to + 1;
                    //x_to_temp += 1;
                }
                else
                {
                    if (x_to > x_from)
                    {
                        // merge cells
                        //xls.MergeCells(y, x_from, y, x_to);

                        dic.Add(x_from, x_to);
                    }

                    x_from = x + i + 1;
                    x_to = x_from;
                }

            }


            dic.ToList()
                .ForEach(p =>
                {
                    xls.MergeCells(y, p.Key, y, p.Value);

                    var value = xls.GetCellValue(y, p.Key);
                });

            return xls;

        }

        public static XlsFile MergeC(XlsFile xls, int rowStart, int rowEnd, int colStart, int length)
        {
            List<MergeInfo> listMerge = new List<MergeInfo>();
            var x = colStart;
            var y = rowStart;

            var y_to = rowStart;
            var y_from = rowStart;


            var x_to_temp = y_from;

            for (int i = 0; i < length; i++)
            {
                for (int j = rowStart; j <= rowEnd; j++)
                {
                    if (j + 1 <= rowEnd)
                    {
                        var cell1 = xls.GetCellValue(j, x + i);
                        var cell2 = xls.GetCellValue(j + 1, x + i);

                        if (cell1 == cell2 && cell2 != null && cell1 != null)
                        {
                            y_to = y_to + 1;
                        }
                        else
                        {
                            if (y_to > y_from)
                            {
                                listMerge.Add(new MergeInfo
                                {
                                    xFrom = x + i,
                                    yFrom = y_from,
                                    xTo = x + i,
                                    yTo = y_to
                                });
                                //dic.Add(y_from, y_to);
                            }

                            y_from = j + 1;
                            y_to = y_from;
                        }
                    }
                    else
                    {
                        if (y_to > y_from)
                        {
                            listMerge.Add(new MergeInfo
                            {
                                xFrom = x + i,
                                yFrom = y_from,
                                xTo = x + i,
                                yTo = y_to
                            });
                            //dic.Add(y_from, y_to);
                        }

                        y_from = rowStart;
                        y_to = y_from;
                    }

                }
            }

            listMerge
                 .ForEach(p =>
                 {
                     xls.MergeCells(p.yFrom, p.xFrom, p.yTo, p.xTo);
                     //var value = xls.GetCellValue(y, p.Key);
                 });

            return xls;
        }
        public class MergeInfo
        {
            public int xFrom { get; set; }
            public int yFrom { get; set; }
            public int xTo { get; set; }
            public int yTo { get; set; }
        }
    }
}
