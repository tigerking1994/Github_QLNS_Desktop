using AutoMapper;
using FlexCel.Core;
using FlexCel.Report;
using FlexCel.XlsAdapter;
using log4net;
using Microsoft.Extensions.Configuration;
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
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.PheDuyetQuyetToanDAHT
{
    public class BaoCaoKetLuanQuyetToanPrintDialogViewModel : DialogViewModelBase<NhQtPheDuyetQuyetToanDAHTModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly string _typeChuky = TypeChuKy.RPT_BAOCAO_KETLUAN_QUYETTOAN;
        private readonly INhQtPheDuyetQuyetToanDAHTService _nhQtPheDuyetQuyetToanDAHTService;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;

        public override Type ContentType => typeof(View.Forex.ForexSettlement.PheDuyetQuyetToanDAHT.PrintDialog.PheDuyetQuyetToanDAHTPrintDialog);
        public override string Title => "BÁO CÁO KẾT LUẬN QUYẾT TOÁN";
        public override string Description => "Báo cáo kết luận quyết toán";

        private int _columnCountBC1 = 10;

        public NhQtPheDuyetQuyetToanDAHT NhQtPheDuyetQuyetToanDAHT { get; set; }
        public DmChuKyDialogViewModel _dmChuKyDialogViewModel { get; }
        private DmChuKy _dmChuKy;
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ExportCommand { get; }

        public DateTime? DNgayPheDuyetTu { get; set; }
        public DateTime? DNgayPheDuyetDen { get; set; }
        public Guid? IIdDonVi { get; set; }



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

        public bool InMotToChecked { get; set; }


        private ComboboxItem _selectedDonViTinhVND;
        public ComboboxItem SelectedDonViTinhVND
        {
            get => _selectedDonViTinhVND;
            set => SetProperty(ref _selectedDonViTinhVND, value);
        }

        private readonly string _templatePath;
        private readonly IConfiguration _configuration;
        public BaoCaoKetLuanQuyetToanPrintDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            IDmChuKyService dmChuKyService,
            IConfiguration configuration,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            INhQtPheDuyetQuyetToanDAHTService nhQtPheDuyetQuyetToanDAHTService,
            INsDonViService nsDonViService,

            IExportService exportService,
            INsNguonNganSachService nsNguonNganSachService)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _dmChuKyService = dmChuKyService;
            _configuration = configuration;
            _nhQtPheDuyetQuyetToanDAHTService = nhQtPheDuyetQuyetToanDAHTService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _templatePath = _configuration.GetSection(ConfigHelper.TEMPLATE_XLXSPATH).Value;
            _nsNguonNganSachService = nsNguonNganSachService;

            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            PrintReportCommand = new RelayCommand(obj => OnPrintReport(ExportType.PDF));
            ExportCommand = new RelayCommand(obj => OnPrintReport(ExportType.EXCEL));
            _dmChuKyDialogViewModel = dmChuKyDialogViewModel;
        }

        public override void Init()
        {
            base.Init();
            loadDonViTinh();
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


        private void loadDonViTinh()
        {
            ItemsDonViTinhUSD.Clear();

            ItemsDonViTinhUSD.Add(new ComboboxItem() { DisplayItem = "USD", ValueItem = "1" });
            ItemsDonViTinhUSD.Add(new ComboboxItem() { DisplayItem = "Nghìn USD", ValueItem = "1000" });
            ItemsDonViTinhUSD.Add(new ComboboxItem() { DisplayItem = "Tỷ USD", ValueItem = "10000000" });

            ItemsDonViTinhVND.Clear();
            ItemsDonViTinhVND.Add(new ComboboxItem() { DisplayItem = "VND", ValueItem = "1" });
            ItemsDonViTinhVND.Add(new ComboboxItem() { DisplayItem = "Nghìn VND", ValueItem = "1000" });
            ItemsDonViTinhVND.Add(new ComboboxItem() { DisplayItem = "Tỷ VND", ValueItem = "10000000" });

        }
        private void OnPrintReport(ExportType exportType)
        {
            if (!Validate()) return;
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    var getData = _nhQtPheDuyetQuyetToanDAHTService.GetDataBaoCaoKetLuanDetail(IIdDonVi, Int32.Parse(SelectedDonViTinhUSD.ValueItem), Int32.Parse(SelectedDonViTinhVND.ValueItem), DNgayPheDuyetTu, DNgayPheDuyetDen).ToList();
                    List<NhQtPheDuyetQuyetToanDAHTChiTietQuery> getlistGiaiDoan = getData.Where(x => x.INamBaoCaoTu != null && x.INamBaoCaoDen != null).ToList();

                    List<NhTtThucHienNganSachGiaiDoanQuery> lstGiaiDoan = getlistGiaiDoan
                            .GroupBy(x => (x.INamBaoCaoTu, x.INamBaoCaoDen)).Select(x => x.First())
                            .Select(x => new NhTtThucHienNganSachGiaiDoanQuery
                            {
                                sGiaiDoan = "Giai đoạn " + x.INamBaoCaoTu + " - " + x.INamBaoCaoDen,
                                iGiaiDoanTu = x.INamBaoCaoTu,
                                iGiaiDoanDen = x.INamBaoCaoDen
                            }).Take(10).ToList();
                    var list = getList(_mapper.Map<ObservableCollection<NhQtPheDuyetQuyetToanDAHTChiTietModel>>(getData).ToList(), lstGiaiDoan);
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    var iTo = 0;
                    if (((9 + lstGiaiDoan.Count * 5) % _columnCountBC1) != 0)
                    {
                        iTo = lstGiaiDoan != null ? (((9 + lstGiaiDoan.Count * 5) / _columnCountBC1) + 1) : 1;

                    }
                    else
                    {
                        iTo = lstGiaiDoan != null ? (((9 + lstGiaiDoan.Count * 5) / _columnCountBC1)) : 1;
                    }
                    for (int i = 1; i <= iTo; i++)
                    {
                        string fileNamePrefixTo = string.Format("rptNgoaiHoi_BaoCaoKetLuanQuyetToan");
                        string fileNameWithoutExtensionTo = StringUtils.CreateExportFileName(fileNamePrefixTo);
                        var xlsFileto = TaoFileExel(list, lstGiaiDoan, i);
                        results.Add(new ExportResult(fileNameWithoutExtensionTo, fileNameWithoutExtensionTo, null, xlsFileto));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            if (InMotToChecked == true)
                            {
                                exportType = ExportType.PDF_ONE_PAPER;
                            }
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
        private bool Validate()
        {
            List<string> lstError = new List<string>();

            if (SelectedDonViTinhUSD == null)
            {
                lstError.Add(Resources.MsgCheckDonVi);
            }
            if (SelectedDonViTinhVND == null)
            {
                lstError.Add(Resources.MsgCheckDonVi);
            }
            if (lstError.Count != 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        public ExcelFile TaoFileExel(List<NhQtPheDuyetQuyetToanDAHTChiTietModel> data = null, List<NhTtThucHienNganSachGiaiDoanQuery> listGiaiDoan = null, int To = 1)
        {
            XlsFile Result = new XlsFile(true);
            Result.Open(Path.Combine(_templatePath, ExportPrefix.PATH_NH_QT, ExportFileName.RPT_BAOCAO_KETLUAN_QUYETTOAN));
            FlexCelReport fr = new FlexCelReport();

            //
            Header objHeader = new Header();
            objHeader.lstHeaderLv1 = new List<HeaderInfo>();
            objHeader.lstHeaderLv2 = new List<HeaderInfo>();
            objHeader.lstHeaderLv3 = new List<HeaderInfo>();

            var countColumn = 9 + listGiaiDoan.Count() + (listGiaiDoan.Count() * 2) + (listGiaiDoan.Count() * 2);

            int ResultTo = (countColumn % _columnCountBC1) == 0 ? countColumn / _columnCountBC1 : (countColumn / _columnCountBC1) + 1;

            var listColumn = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
            #region -------------------------------- Header vl1---------------------------
            if (listGiaiDoan != null)
            {
                for (var i = 1; i <= countColumn; i++)
                {
                    var startColumn1 = listGiaiDoan.Count() + 1;
                    var startColumn2 = startColumn1 + (listGiaiDoan.Count() * 2) + 2;
                    var startColumn3 = startColumn2 + (listGiaiDoan.Count() * 2) + 2;

                    // header vl1 ;

                    if (i <= startColumn1)
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
                    else if (i <= startColumn2)
                    {
                        objHeader.lstHeaderLv1.Add(new HeaderInfo
                        {
                            sTen = "Kinh phí được cấp",
                        });
                        objHeader.lstHeaderLv3.Add(new HeaderInfo
                        {
                            sTen = (startColumn2 - i) % 2 == 0 ? "VND" : "USD",
                        });
                    }
                    else if (i <= startColumn3)
                    {
                        objHeader.lstHeaderLv1.Add(new HeaderInfo
                        {
                            sTen = "Kinh phí đã giải ngân (thanh toán, tạm ứng)",
                        });
                        objHeader.lstHeaderLv3.Add(new HeaderInfo
                        {
                            sTen = (startColumn3 - i) % 2 == 0 ? "VND" : "USD",
                        });
                    }
                    else if (i < countColumn - 1)
                    {
                        objHeader.lstHeaderLv1.Add(new HeaderInfo
                        {
                            sTen = "So sánh số cấp kinh phí được cấp với số quyết toán",
                        });
                        objHeader.lstHeaderLv3.Add(new HeaderInfo
                        {
                            sTen = (countColumn - i) % 2 == 0 ? "VND" : "USD",
                        });
                    }
                    else if (i <= countColumn)
                    {
                        objHeader.lstHeaderLv1.Add(new HeaderInfo
                        {
                            sTen = "Kinh phí thừa nộp trả NSNN",
                        });
                        objHeader.lstHeaderLv3.Add(new HeaderInfo
                        {
                            sTen = (countColumn - i) % 2 == 0 ? "VND" : "USD",
                        });
                    }
                    // listColumn 1 2 3 .......
                    if (i == 1)
                    {
                        NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan SoBC = new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan();
                        var nowColumn = i.ToString() + " =";
                        for (var j = 1; j <= listGiaiDoan.Count(); j++)
                        {
                            nowColumn += (i + j).ToString() + " +";
                        }
                        SoBC.sGiaiDoan = nowColumn.Remove(nowColumn.Length - 1);
                        listColumn.Add(SoBC);
                    }
                    else if (i == startColumn1 || i == startColumn1 + 1 || i == startColumn2 || i == startColumn2 + 1)
                    {
                        NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan SoBC = new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan();
                        var nowColumn = i.ToString() + " =";
                        for (var j = 1; j <= listGiaiDoan.Count(); j++)
                        {
                            nowColumn += (i + (j * 2)).ToString() + " +";
                        }
                        SoBC.sGiaiDoan = nowColumn.Remove(nowColumn.Length - 1);
                        listColumn.Add(SoBC);
                    }
                    else if (i == startColumn3 + 1)
                    {
                        NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan SoBC = new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan();
                        var nowColumn = i.ToString() + " =" + (startColumn1 + 1).ToString() + " -" + (startColumn2 + 1).ToString();
                        SoBC.sGiaiDoan = nowColumn;
                        listColumn.Add(SoBC);
                    }
                    else if (i == startColumn3 + 2)
                    {
                        NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan SoBC = new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan();
                        var nowColumn = i.ToString() + " =" + (startColumn1 + 2).ToString() + " -" + (startColumn2 + 2).ToString();
                        SoBC.sGiaiDoan = nowColumn;
                        listColumn.Add(SoBC);
                    }
                    else
                    {
                        NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan SoBC = new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan();
                        SoBC.sGiaiDoan = i.ToString();
                        listColumn.Add(SoBC);
                    }
                }
            }
            #endregion

            #region ------------------------------headerlv2------------------------------
            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Tổng số",
            });

            for (var i = 0; i < listGiaiDoan.Count; i++)
            {
                objHeader.lstHeaderLv2.Add(new HeaderInfo
                {
                    sTen = listGiaiDoan[i].sGiaiDoan,
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

            for (var i = 0; i < listGiaiDoan.Count; i++)
            {
                objHeader.lstHeaderLv2.Add(new HeaderInfo
                {
                    sTen = listGiaiDoan[i].sGiaiDoan,
                });
                objHeader.lstHeaderLv2.Add(new HeaderInfo
                {
                    sTen = listGiaiDoan[i].sGiaiDoan,
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

            for (var i = 0; i < listGiaiDoan.Count; i++)
            {
                objHeader.lstHeaderLv2.Add(new HeaderInfo
                {
                    sTen = listGiaiDoan[i].sGiaiDoan,
                });
                objHeader.lstHeaderLv2.Add(new HeaderInfo
                {
                    sTen = listGiaiDoan[i].sGiaiDoan,
                });
            }


            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "So sánh số cấp kinh phí được cấp với số quyết toán",
            });
            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "So sánh số cấp kinh phí được cấp với số quyết toán",
            });
            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Kinh phí thừa nộp trả NSNN",
            });
            objHeader.lstHeaderLv2.Add(new HeaderInfo
            {
                sTen = "Kinh phí thừa nộp trả NSNN",
            });
            #endregion

            #region ----------------------- setvalue------------------------------

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

            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_BAOCAO_KETLUAN_QUYETTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            fr.SetValue("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            fr.SetValue("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            fr.SetValue("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            fr.SetValue("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            fr.SetValue("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            fr.SetValue("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            fr.SetValue("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            fr.SetValue("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            fr.SetValue("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            fr.SetValue("DonViCapTren", _sessionService.Current.TenDonViTrucThuocReportHeader.ToUpper());
            fr.SetValue("DonVi", _sessionService.Current.TenDonViReportHeader.ToUpper());

            fr.SetValue(
                "TenBaoCao1", _dmChuKy != null ? _dmChuKy.TieuDe1MoTa?.ToUpper() : string.Empty
            );
            fr.SetValue(
               "donViUSD", SelectedDonViTinhUSD.DisplayItem
           );
            fr.SetValue(
               "donViVND", SelectedDonViTinhVND.DisplayItem
           );
            fr.SetValue(
                "TenBaoCao2",
                _dmChuKy != null
                ? _dmChuKy.TieuDe2MoTa + "" != ""
                ? _dmChuKy.TieuDe2MoTa.ToUpper()
                : "Thời gian báo cáo :" + (DNgayPheDuyetTu != null ? DNgayPheDuyetTu.Value.ToString("dd/MM/YYYY") : string.Empty) + " - " + (DNgayPheDuyetDen != null ? DNgayPheDuyetDen.Value.ToString("dd/MM/YYYY") : string.Empty)
                : string.Empty
            );
            fr.SetValue(
                "TenBaoCao3",
                _dmChuKy != null
                ? _dmChuKy.TieuDe2MoTa + "" != ""
                ? _dmChuKy.TieuDe3MoTa + "" != ""
                ? _dmChuKy.TieuDe3MoTa.ToUpper()
                : "Thời gian báo cáo :" + (DNgayPheDuyetTu != null ? DNgayPheDuyetTu.Value.ToString("dd/MM/YYYY") : string.Empty) + " - " + (DNgayPheDuyetDen != null ? DNgayPheDuyetDen.Value.ToString("dd/MM/YYYY") : string.Empty)
                : string.Empty
                : string.Empty
            );
            fr.SetValue(
                "To", To
            );
            #endregion

            foreach (var item in data)
            {
                item.lstData = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(item.FKeHoachTTCPUsd, 2, false) });

                foreach (var giaidoan in item.listDataTTCP)
                {
                    item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(giaidoan.valueUSD, 2, false) });
                }
                item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(item.FKinhPhiDuocCapTongUsd, 2, false) });
                item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(item.FKinhPhiDuocCapTongVnd, 0, false) });

                foreach (var giaidoan in item.listDataKPDC)
                {
                    item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(giaidoan.valueUSD, 2, false) });
                    item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(giaidoan.valueVND, 0, false) });
                }

                item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(item.FQuyetToanDuocDuyetTongUsd, 2, false) });
                item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(item.FQuyetToanDuocDuyetTongVnd, 0, false) });

                foreach (var giaidoan in item.listDataQTDD)
                {
                    item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(giaidoan.valueUSD, 2, false) });
                    item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(giaidoan.valueVND, 0, false) });
                }

                item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(item.FSoSanhKinhPhiUsd, 2, false) });
                item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(item.FSoSanhKinhPhiVnd, 0, false) });
                item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(item.FThuaTraNSNNUsd, 2, false) });
                item.lstData.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { sGiaiDoan = StringUtils.DinhDangSo(item.FThuaTraNSNNVnd, 0, false) });


                item.lstData = item.lstData.Skip(columnStart).Take(_columnCountBC1).ToList();
            }
            listColumn = listColumn.Skip(columnStart).Take(_columnCountBC1).ToList();
            fr.AddTable<NhQtPheDuyetQuyetToanDAHTChiTietModel>("dt", data);
            fr.AddTable<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>("listColumn", listColumn);
            fr.Run(Result);


            //count merge cột
            //tạo border format
            var SoHangTrangCuoi = countColumn - ((ResultTo - 1) * _columnCountBC1);
            if (SoHangTrangCuoi == 3)
            {
                if ((To - 1) == ResultTo)
                {
                    Result.MergeCells(6, 12, 7, 12);
                }

                if (To == ResultTo)
                {
                    Result.MergeCells(6, 3, 7, 3);
                    Result.MergeCells(6, 4, 7, 5);
                }

            }
            else if (SoHangTrangCuoi == 2)
            {
                if ((To - 1) == ResultTo)
                {
                    Result.MergeCells(6, 11, 7, 12);
                }
                if (To == ResultTo)
                {
                    Result.MergeCells(6, 3, 7, 4);
                }
            }
            else if (SoHangTrangCuoi == 1)
            {
                if ((To - 1) == ResultTo)
                {
                    Result.MergeCells(6, 10, 7, 11);
                    Result.MergeCells(6, 12, 7, 12);
                }
                if (To == ResultTo)
                {
                    Result.MergeCells(6, 3, 7, 3);
                }
            }
            else if (SoHangTrangCuoi == 0)
            {
                if (To == ResultTo)
                {
                    Result.MergeCells(6, _columnCountBC1 - 3, 7, _columnCountBC1 - 2);
                    Result.MergeCells(6, _columnCountBC1 - 1, 7, _columnCountBC1);
                }

            }
            else
            {
                if (To == ResultTo)
                {
                    Result.MergeCells(6, SoHangTrangCuoi - 3, 7, SoHangTrangCuoi - 2);
                    Result.MergeCells(6, SoHangTrangCuoi - 1, 7, SoHangTrangCuoi);
                }
            }

            Result = MergeH(Result, 6, 3, 12);
            Result = MergeH(Result, 7, 3, 12);

            Result = MergeC(Result, 6, 3, 6, 12);

            //tạo border format
            var b = Result.GetDefaultFormat;

            var ApplyFormat = new TFlxApplyFormat();
            ApplyFormat.SetAllMembers(false);
            //ApplyFormat.Borders.SetAllMembers(true);
            TCellAddress Cell = null;
            //tìm dòng cuối cùng của bảng
            Cell = Result.Find("Cộng", null, Cell, false, true, true, false);
            //set border cho bảng
            Result.SetCellFormat(6, 1, 9 + data.Count(), 7 + listGiaiDoan.Count() * 3, b, ApplyFormat, false);
            return Result;
        }

        private List<NhQtPheDuyetQuyetToanDAHTChiTietModel> getList(List<NhQtPheDuyetQuyetToanDAHTChiTietModel> list, List<NhTtThucHienNganSachGiaiDoanQuery> lstGiaiDoan)
        {
            List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listData = new List<NhQtPheDuyetQuyetToanDAHTChiTietModel>();
            List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listTong = list;
            int SttLoai = 0;
            int SttHopDong = 0;
            int SttDuAn = 0;
            int SttChuongTrinh = 0;
            Guid? idDuAn = null;
            Guid? idHopDong = null;
            Guid? idChuongTrinh = null;
            int? idLoai = null;
            int sttTong = 0;
            NhQtPheDuyetQuyetToanDAHTChiTietModel DataTong = new NhQtPheDuyetQuyetToanDAHTChiTietModel();
            DataTong.listDataTTCP = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
            DataTong.listDataKPDC = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
            DataTong.listDataQTDD = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();

            if (lstGiaiDoan != null)
            {
                foreach (var giaiDoan in lstGiaiDoan)
                {
                    List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataChaGiaiDoan = listTong.Where(x => x.INamBaoCaoTu == giaiDoan.iGiaiDoanTu && x.INamBaoCaoDen == giaiDoan.iGiaiDoanDen).ToList();
                    DataTong.listDataTTCP.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = listDataChaGiaiDoan.GroupBy(x => x.IIDKHTTNhiemVuChiId).Select(x => x.First()).Sum(x => x.FKeHoachTTCPUsd) });
                    DataTong.listDataKPDC.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd), valueVND = listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd) });
                    DataTong.listDataQTDD.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd), valueVND = listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd) });
                }
            }
            DataTong.FKeHoachTTCPUsd = list.GroupBy(x => x.IIDKHTTNhiemVuChiId).Select(x => x.First()).Sum(x => x.FKeHoachTTCPUsd);

            DataTong.FKinhPhiDuocCapTongUsd = list.GroupBy(x => x.IIDThanhToanChiTietId).Select(x => x.First()).Sum(x => x.FKinhPhiDuocCapTongUsd);
            DataTong.FKinhPhiDuocCapTongVnd = list.GroupBy(x => x.IIDThanhToanChiTietId).Select(x => x.First()).Sum(x => x.FKinhPhiDuocCapTongVnd);

            DataTong.FQuyetToanDuocDuyetTongUsd = list.GroupBy(x => x.IIDThanhToanChiTietId).Select(x => x.First()).Sum(x => x.FQuyetToanDuocDuyetTongUsd);
            DataTong.FQuyetToanDuocDuyetTongVnd = list.GroupBy(x => x.IIDThanhToanChiTietId).Select(x => x.First()).Sum(x => x.FQuyetToanDuocDuyetTongVnd);

            DataTong.FSoSanhKinhPhiUsd = list.GroupBy(x => x.IIDThanhToanChiTietId).Select(x => x.First()).Sum(x => x.FSoSanhKinhPhiUsd);
            DataTong.FSoSanhKinhPhiVnd = list.GroupBy(x => x.IIDThanhToanChiTietId).Select(x => x.First()).Sum(x => x.FSoSanhKinhPhiVnd);

            DataTong.FThuaTraNSNNUsd = list.GroupBy(x => x.IIDThanhToanChiTietId).Select(x => x.First()).Sum(x => x.FThuaTraNSNNUsd);
            DataTong.FThuaTraNSNNVnd = list.GroupBy(x => x.IIDThanhToanChiTietId).Select(x => x.First()).Sum(x => x.FThuaTraNSNNVnd);
            DataTong.IsHangCha = true;
            DataTong.STenNoiDungChi = "Tổng cộng: ";
            if (list != null || list.Count == 0)
            {
                foreach (var item in list)
                {
                    sttTong++;
                    if (lstGiaiDoan != null)
                    {
                        item.listDataTTCP = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        item.listDataKPDC = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        item.listDataQTDD = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        item.INamBaoCaoDen = item.INamBaoCaoDen;
                        item.INamBaoCaoTu = item.INamBaoCaoTu;

                        if (lstGiaiDoan != null)
                        {
                            foreach (var giaiDoan in lstGiaiDoan)
                            {
                                if (item.INamBaoCaoTu == giaiDoan.iGiaiDoanTu && item.INamBaoCaoDen == giaiDoan.iGiaiDoanDen)
                                {
                                    item.listDataTTCP.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = item.FKeHoachTTCPUsd });
                                    item.listDataKPDC.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = item.FKinhPhiDuocCapTongUsd, valueVND = item.FKinhPhiDuocCapTongVnd });
                                    item.listDataQTDD.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = item.FQuyetToanDuocDuyetTongUsd, valueVND = item.FQuyetToanDuocDuyetTongVnd });
                                }
                                else
                                {
                                    item.listDataTTCP.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0 });
                                    item.listDataKPDC.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0, valueVND = 0 });
                                    item.listDataQTDD.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0, valueVND = 0 });
                                }

                            }
                        }
                    }
                    if (item.IIDKHTTNhiemVuChiId != idChuongTrinh/* && item.IIDKHTTNhiemVuChiId != Guid.Empty*/)
                    {
                        SttChuongTrinh++;
                        SttDuAn = 0;
                        SttLoai = 0;
                        SttDuAn = 0;
                        idDuAn = null;
                        idLoai = null;
                        idHopDong = null;
                        NhQtPheDuyetQuyetToanDAHTChiTietModel DataCha = new NhQtPheDuyetQuyetToanDAHTChiTietModel();

                        List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataCha = list.Where(x => x.IIDKHTTNhiemVuChiId == item.IIDKHTTNhiemVuChiId).ToList();
                        DataCha.FHopDongUsd = null;
                        DataCha.FHopDongVnd = null;

                        DataCha.FKeHoachTTCPUsd = listDataCha.Sum(x => x.FKeHoachTTCPUsd);

                        DataCha.FKinhPhiDuocCapTongUsd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongUsd);
                        DataCha.FKinhPhiDuocCapTongVnd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongVnd);

                        DataCha.FQuyetToanDuocDuyetTongUsd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongUsd);
                        DataCha.FQuyetToanDuocDuyetTongVnd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongVnd);

                        DataCha.FSoSanhKinhPhiUsd = listDataCha.Sum(x => x.FSoSanhKinhPhiUsd);
                        DataCha.FSoSanhKinhPhiVnd = listDataCha.Sum(x => x.FSoSanhKinhPhiVnd);

                        DataCha.FThuaTraNSNNUsd = listDataCha.Sum(x => x.FThuaTraNSNNUsd);
                        DataCha.FThuaTraNSNNVnd = listDataCha.Sum(x => x.FThuaTraNSNNVnd);

                        if (item.IIDKHTTNhiemVuChiId != Guid.Empty)
                        {
                            DataCha.STenNoiDungChi = ConvertLetter(SttChuongTrinh) + ", " + item.STenNhiemVuChi;
                        }
                        else
                        {
                            DataCha.STenNoiDungChi = ConvertLetter(SttChuongTrinh) + ", " + "Nội dung chi khác";
                        }
                        idChuongTrinh = item.IIDKHTTNhiemVuChiId;
                        DataCha.listDataTTCP = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataKPDC = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataQTDD = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.IsHangCha = true;

                        if (lstGiaiDoan != null)
                        {
                            foreach (var giaiDoan in lstGiaiDoan)
                            {
                                List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataChaGiaiDoan = listDataCha.Where(x => x.INamBaoCaoTu == giaiDoan.iGiaiDoanTu && x.INamBaoCaoDen == giaiDoan.iGiaiDoanDen).ToList();
                                if (listDataChaGiaiDoan.Count != 0)
                                {
                                    DataCha.listDataTTCP.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = item.FKeHoachTTCPUsd });
                                    DataCha.listDataKPDC.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd), valueVND = listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd) });
                                    DataCha.listDataQTDD.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd), valueVND = listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd) });
                                }
                                else
                                {
                                    DataCha.listDataTTCP.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0 });
                                    DataCha.listDataKPDC.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0, valueVND = 0 });
                                    DataCha.listDataQTDD.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0, valueVND = 0 });
                                }

                            }
                        }

                        listData.Add(DataCha);
                    }
                    if (item.IIDDuAnId != idDuAn /*&& item.IIDDuAnId != Guid.Empty*/)
                    {
                        SttDuAn++;
                        SttLoai = 0;
                        SttHopDong = 0;
                        idLoai = null;
                        idHopDong = null;
                        NhQtPheDuyetQuyetToanDAHTChiTietModel DataCha = new NhQtPheDuyetQuyetToanDAHTChiTietModel();
                        List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataCha = list.Where(x => x.IIDDuAnId == item.IIDDuAnId && x.IIDKHTTNhiemVuChiId == item.IIDKHTTNhiemVuChiId).ToList();

                        DataCha.FHopDongUsd = listDataCha.Sum(x => x.FHopDongUsdDuAn);
                        DataCha.FHopDongVnd = listDataCha.Sum(x => x.FHopDongVndDuAn);

                        DataCha.FKeHoachTTCPUsd = null;

                        DataCha.FKinhPhiDuocCapTongUsd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongUsd);
                        DataCha.FKinhPhiDuocCapTongVnd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongVnd);

                        DataCha.FQuyetToanDuocDuyetTongUsd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongUsd);
                        DataCha.FQuyetToanDuocDuyetTongVnd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongVnd);

                        DataCha.FSoSanhKinhPhiUsd = listDataCha.Sum(x => x.FSoSanhKinhPhiUsd);
                        DataCha.FSoSanhKinhPhiVnd = listDataCha.Sum(x => x.FSoSanhKinhPhiVnd);

                        DataCha.FThuaTraNSNNUsd = listDataCha.Sum(x => x.FThuaTraNSNNUsd);
                        DataCha.FThuaTraNSNNVnd = listDataCha.Sum(x => x.FThuaTraNSNNVnd);

                        if (item.IIDDuAnId != Guid.Empty)
                        {
                            DataCha.STenNoiDungChi = ConvertLaMa(SttDuAn) + ", " + item.STenDuAn;
                        }
                        else if (item.IIDHopDongId != Guid.Empty)
                        {
                            DataCha.STenNoiDungChi = ConvertLaMa(SttDuAn) + ", " + "Chi hợp đồng";
                        }
                        else
                        {
                            DataCha.STenNoiDungChi = ConvertLaMa(SttDuAn) + ", " + "Chi khác";
                        }
                        idDuAn = item.IIDDuAnId;
                        DataCha.listDataTTCP = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataKPDC = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataQTDD = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.INamBaoCaoDen = item.INamBaoCaoDen;
                        DataCha.INamBaoCaoTu = item.INamBaoCaoTu;
                        DataCha.IsHangCha = true;

                        if (lstGiaiDoan != null)
                        {
                            foreach (var giaiDoan in lstGiaiDoan)
                            {
                                List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataChaGiaiDoan = listDataCha.Where(x => x.INamBaoCaoTu == giaiDoan.iGiaiDoanTu && x.INamBaoCaoDen == giaiDoan.iGiaiDoanDen).ToList();
                                if (listDataChaGiaiDoan.Count != 0)
                                {
                                    DataCha.listDataTTCP.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0 });
                                    DataCha.listDataKPDC.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd), valueVND = listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd) });
                                    DataCha.listDataQTDD.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd), valueVND = listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd) });
                                }
                                else
                                {
                                    DataCha.listDataTTCP.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0 });
                                    DataCha.listDataKPDC.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0, valueVND = 0 });
                                    DataCha.listDataQTDD.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0, valueVND = 0 });
                                }

                            }
                        }

                        listData.Add(DataCha);
                    }
                    if (item.ILoaiNoiDungChi != idLoai && item.ILoaiNoiDungChi != 0)
                    {
                        SttLoai++;
                        SttHopDong = 0;
                        idHopDong = null;
                        NhQtPheDuyetQuyetToanDAHTChiTietModel DataCha = new NhQtPheDuyetQuyetToanDAHTChiTietModel();
                        List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataCha = list.Where(x => x.ILoaiNoiDungChi == item.ILoaiNoiDungChi && x.IIDDuAnId == item.IIDDuAnId && x.IIDKHTTNhiemVuChiId == item.IIDKHTTNhiemVuChiId).ToList();

                        DataCha.FHopDongUsd = listDataCha.Sum(x => x.FHopDongUsdHopDong);
                        DataCha.FHopDongVnd = listDataCha.Sum(x => x.FHopDongVndHopDong);

                        DataCha.FKeHoachTTCPUsd = null;

                        DataCha.FKinhPhiDuocCapTongUsd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongUsd);
                        DataCha.FKinhPhiDuocCapTongVnd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongVnd);

                        DataCha.FQuyetToanDuocDuyetTongUsd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongUsd);
                        DataCha.FQuyetToanDuocDuyetTongVnd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongVnd);

                        DataCha.FSoSanhKinhPhiUsd = listDataCha.Sum(x => x.FSoSanhKinhPhiUsd);
                        DataCha.FSoSanhKinhPhiVnd = listDataCha.Sum(x => x.FSoSanhKinhPhiVnd);

                        DataCha.FThuaTraNSNNUsd = listDataCha.Sum(x => x.FThuaTraNSNNUsd);
                        DataCha.FThuaTraNSNNVnd = listDataCha.Sum(x => x.FThuaTraNSNNVnd);

                        if (item.ILoaiNoiDungChi == 1)
                        {
                            DataCha.STenNoiDungChi = SttLoai.ToString() + ", Chi ngoại tệ";
                        }
                        else if (item.ILoaiNoiDungChi == 2)
                        {
                            DataCha.STenNoiDungChi = SttLoai.ToString() + ", Chi trong nước";
                        }
                        else
                        {
                            DataCha.STenNoiDungChi = SttLoai.ToString() + ", Chi khác";
                        }
                        idLoai = item.ILoaiNoiDungChi;
                        DataCha.listDataTTCP = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataKPDC = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataQTDD = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.INamBaoCaoDen = item.INamBaoCaoDen;
                        DataCha.INamBaoCaoTu = item.INamBaoCaoTu;
                        DataCha.IsHangCha = true;

                        if (lstGiaiDoan != null)
                        {
                            foreach (var giaiDoan in lstGiaiDoan)
                            {
                                List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataChaGiaiDoan = listDataCha.Where(x => x.INamBaoCaoTu == giaiDoan.iGiaiDoanTu && x.INamBaoCaoDen == giaiDoan.iGiaiDoanDen).ToList();
                                if (listDataChaGiaiDoan.Count != 0)
                                {
                                    DataCha.listDataTTCP.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0 });
                                    DataCha.listDataKPDC.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd), valueVND = listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd) });
                                    DataCha.listDataQTDD.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd), valueVND = listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd) });
                                }
                                else
                                {
                                    DataCha.listDataTTCP.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0 });
                                    DataCha.listDataKPDC.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0, valueVND = 0 });
                                    DataCha.listDataQTDD.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0, valueVND = 0 });
                                }

                            }
                        }

                        listData.Add(DataCha);
                    }
                    if (item.IIDHopDongId != idHopDong && item.IIDHopDongId != Guid.Empty)
                    {
                        SttHopDong++;

                        NhQtPheDuyetQuyetToanDAHTChiTietModel DataCha = new NhQtPheDuyetQuyetToanDAHTChiTietModel();
                        List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataCha = list.Where(x => x.IIDHopDongId == item.IIDHopDongId && x.ILoaiNoiDungChi == item.ILoaiNoiDungChi && x.IIDDuAnId == item.IIDDuAnId && x.IIDKHTTNhiemVuChiId == item.IIDKHTTNhiemVuChiId).ToList();

                        DataCha.FHopDongUsd = listDataCha.Sum(x => x.FHopDongUsdHopDong);
                        DataCha.FHopDongVnd = listDataCha.Sum(x => x.FHopDongVndHopDong);

                        DataCha.FKeHoachTTCPUsd = null;

                        DataCha.FKinhPhiDuocCapTongUsd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongUsd);
                        DataCha.FKinhPhiDuocCapTongVnd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongVnd);

                        DataCha.FQuyetToanDuocDuyetTongUsd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongUsd);
                        DataCha.FQuyetToanDuocDuyetTongVnd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongVnd);

                        DataCha.FSoSanhKinhPhiUsd = listDataCha.Sum(x => x.FSoSanhKinhPhiUsd);
                        DataCha.FSoSanhKinhPhiVnd = listDataCha.Sum(x => x.FSoSanhKinhPhiVnd);

                        DataCha.FThuaTraNSNNUsd = listDataCha.Sum(x => x.FThuaTraNSNNUsd);
                        DataCha.FThuaTraNSNNVnd = listDataCha.Sum(x => x.FThuaTraNSNNVnd);

                        DataCha.STenNoiDungChi = SttLoai.ToString() + "." + SttHopDong.ToString() + ", " + item.STenHopDong;
                        idHopDong = item.IIDHopDongId;
                        DataCha.listDataTTCP = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataKPDC = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataQTDD = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.INamBaoCaoDen = item.INamBaoCaoDen;
                        DataCha.INamBaoCaoTu = item.INamBaoCaoTu;


                        if (lstGiaiDoan != null)
                        {
                            foreach (var giaiDoan in lstGiaiDoan)
                            {
                                List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataChaGiaiDoan = listDataCha.Where(x => x.INamBaoCaoTu == giaiDoan.iGiaiDoanTu && x.INamBaoCaoDen == giaiDoan.iGiaiDoanDen).ToList();
                                if (listDataChaGiaiDoan.Count != 0)
                                {
                                    DataCha.listDataTTCP.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0 });
                                    DataCha.listDataKPDC.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd), valueVND = listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd) });
                                    DataCha.listDataQTDD.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd), valueVND = listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd) });
                                }
                                else
                                {
                                    DataCha.listDataTTCP.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0 });
                                    DataCha.listDataKPDC.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0, valueVND = 0 });
                                    DataCha.listDataQTDD.Add(new NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan() { valueUSD = 0, valueVND = 0 });
                                }

                            }
                        }

                        listData.Add(DataCha);
                    }

                    listData.Add(item);
                }

                if (sttTong == list.Count())
                {
                    listData.Add(DataTong);
                }
            }

            return listData;
        }

        private string ConvertLetter(int input)
        {
            StringBuilder res = new StringBuilder((input - 1).ToString());
            for (int j = 0; j < res.Length; j++)
                res[j] += (char)(17); // '0' is 48, 'A' is 65
            return res.ToString();
        }
        private string ConvertLaMa(decimal num)
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
                    x_to++;
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
