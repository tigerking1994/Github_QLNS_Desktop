using AutoMapper;
using FlexCel.Core;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check.PrintReport
{
    public class PrintReportSumaryCheckNumberViewModel : ViewModelBase
    {
        #region Private
        private readonly ISessionService _sessionService;
        private ICollectionView _donViCollectionView;
        private ICollectionView _nNganhCollectionView;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private DmChuKy _dmChuKy;
        private string _typeChuky => TypeChuKy.RPT_NS_SKT_TONGHOP_BENHVIENTUCHU;
        private string _diaDiem;
        #endregion

        public override string Title => "Tổng hợp số kiểm tra - Bệnh viện tự chủ";
        public override string Description => "Tổng hợp số kiểm tra - Bệnh viện tự chủ";

        #region RelayCommand
        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand ExportSignatureActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        #endregion

        #region Component
        string _txtTitleFirst;
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

        public bool InMotToChecked { get; set; }

        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }

        private ComboboxItem _paperPrintTypeSelected;

        public ComboboxItem PaperPrintTypeSelected
        {
            get => _paperPrintTypeSelected;
            set => SetProperty(ref _paperPrintTypeSelected, value);
        }


        private ObservableCollection<ComboboxItem> _itemsKieuGiayIn = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ItemsKieuGiayIn
        {
            get => _itemsKieuGiayIn;
            set => SetProperty(ref _itemsKieuGiayIn, value);
        }

        private ComboboxItem _selectedKieuGiayIn;

        public ComboboxItem SelectedKieuGiayIn
        {
            get => _selectedKieuGiayIn;
            set => SetProperty(ref _selectedKieuGiayIn, value);
        }

        private ObservableCollection<ComboboxItem> _catUnitTypes = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> CatUnitTypes
        {
            get => _catUnitTypes;
            set => SetProperty(ref _catUnitTypes, value);
        }

        private ComboboxItem _catUnitTypeSelected;

        public ComboboxItem CatUnitTypeSelected
        {
            get => _catUnitTypeSelected;
            set => SetProperty(ref _catUnitTypeSelected, value);
        }

        private ObservableCollection<ComboboxItem> _voucherTypes = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> VoucherTypes
        {
            get => _voucherTypes;
            set => SetProperty(ref _voucherTypes, value);
        }

        private ComboboxItem _voucherTypeSelected;

        public ComboboxItem VoucherTypeSelected
        {
            get => _voucherTypeSelected;
            set => SetProperty(ref _voucherTypeSelected, value);
        }

        public IEnumerable<DonVi> ListUnit { get; set; }

        private ObservableCollection<CheckBoxItem> _listDonVi = new ObservableCollection<CheckBoxItem>();
        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }

        private bool _selectAllDonVi;
        public bool SelectAllDonVi
        {
            get => ListDonVi.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDonVi, value);
                foreach (var item in ListDonVi) item.IsChecked = _selectAllDonVi;
            }
        }

        public string LabelSelectedCountDonVi
        {
            get => $"ĐƠN VỊ ({ListDonVi.Count(item => item.IsChecked)}/{ListDonVi.Count})";
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _donViCollectionView.Refresh();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _budgetSourceTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BudgetSourceTypes
        {
            get => _budgetSourceTypes;
            set => SetProperty(ref _budgetSourceTypes, value);
        }

        private ComboboxItem _budgetSourceTypeSelected;

        public ComboboxItem BudgetSourceTypeSelected
        {
            get => _budgetSourceTypeSelected;
            set
            {
                SetProperty(ref _budgetSourceTypeSelected, value);
                LoadDonVi();
                OnPropertyChanged(nameof(LabelSelectedCountDonVi));
            }
        }

        #endregion

        public override Type ContentType => typeof(PrintReportSumaryCheckNumber);

        public PrintReportSumaryCheckNumberViewModel(INsDonViService nsDonViService,
            IExportService exportService,
            ISktChungTuChiTietService sktChungTuChiTietService,
            ISktChungTuService sktChungTuService,
            IDmChuKyService dmChuKyService,
            IDanhMucService danhMucService,
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _mapper = mapper;
            _logger = logger;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _sktChungTuService = sktChungTuService;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportPdfActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportSignatureActionCommand = new RelayCommand(ExportSignature);
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            LoadBudgetSourceTypes();
            LoadPaperPrintTypes();
            LoadKieuGiayIn();
            LoadTitleFirst();
            LoadVoucherTypes();
            LoadCatUnitTypes();
        }

        private void LoadBudgetSourceTypes()
        {
            BudgetSourceTypes = new ObservableCollection<ComboboxItem> {
                new ComboboxItem() { DisplayItem = "Tất cả", ValueItem = TypeLoaiNNS.TAT_CA.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách dự toán", ValueItem = TypeLoaiNNS.DU_TOAN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách bệnh viện tự chủ", ValueItem = TypeLoaiNNS.BENH_VIEN.ToString() },
            };

            BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(0);
        }

        #region Event
        public void OnExport(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                int iPage = 0;
                bool bHaveDataNext = true;
                if (InMotToChecked && PaperPrintTypeSelected.ValueItem == "1")
                {
                    exportType = ExportType.PDF_ONE_PAPER;
                }

                while (bHaveDataNext)
                {
                    ExcelFile xlsFile;
                    string templateFileName = string.Empty;
                    var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;
                    else TxtTitleThird = string.Empty;
                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    bHaveDataNext = false;

                    var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                    _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", _sessionInfo.TenDonVi);

                    AddChuKy(data, _typeChuky);

                    if (PaperPrintTypeSelected.ValueItem == "1")
                    {
                        if (!GetDataExportBCTongHopSktBvTuChu(data, donViTinh, ref bHaveDataNext, ref iPage, loaiNNS))
                        {
                            MessageBoxHelper.Error(Resources.MsgErrorDataNotFound);
                            return;
                        }
                    }
                    else
                    {
                        if (!GetDataExportBCTongHopSktBvTuChuDoc(data, donViTinh, loaiNNS))
                        {
                            MessageBoxHelper.Error(Resources.MsgErrorDataNotFound);
                            return;
                        }
                    }
                    string fileNamePrefix = String.Empty;
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_SOKIEMTRA_BENHVIENTUCHU), iPage, ref fileNamePrefix);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTra>(templateFileName, data);
                    if (InMotToChecked && PaperPrintTypeSelected.ValueItem == "1")
                    {
                        results.Add(new ExportResult(string.Format("Tổng hợp số kiểm tra - Bệnh viện tự chủ - Tờ {0}", iPage), fileNameWithoutExtension, null, xlsFile));
                    }
                    else
                    {
                        results.Add(new ExportResult("Tổng hợp số kiểm tra - Bệnh viện tự chủ", fileNameWithoutExtension, null, xlsFile));
                    }
                }
                e.Result = results;
            },
            (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, exportType);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TxtTitleFirst = chuKy.TieuDe1MoTa;
                TxtTitleSecond = chuKy.TieuDe2MoTa;
                TxtTitleThird = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();

        }

        public void ExportSignature(object param)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        #endregion

        #region Helper
        private bool GetDataExportBCTongHopSktBvTuChu(Dictionary<string, object> data, int donViTinh, ref bool bHaveDataNext, ref int iPage, int loaiNNS)
        {
            var lstDonVi = ListDonVi.Where(item => item.IsChecked);
            if (lstDonVi.Count() > (iPage * 5 + 5))
            {
                bHaveDataNext = true;
            }
            iPage++;
            lstDonVi = ListDonVi.Where(item => item.IsChecked).Skip((iPage - 1) * 5).Take(5);
            if (lstDonVi == null || !lstDonVi.Any()) return false;
            string tenDonVi1, tenDonVi2, tenDonVi3, tenDonVi4, tenDonVi5;
            tenDonVi1 = tenDonVi2 = tenDonVi3 = tenDonVi4 = tenDonVi5 = string.Empty;
            List<ReportTongHopPhanBoSoKiemTra> lstData = new List<ReportTongHopPhanBoSoKiemTra>();
            List<ReportTongHopPhanBoSoKiemTra> resultTotal = new List<ReportTongHopPhanBoSoKiemTra>();
            var dataTotal = _sktChungTuChiTietService.FindReportTongHopSoKiemTraBenhVienTuChu(string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(n => n.ValueItem)), _sessionService.Current.YearOfWork, loaiNNS);
            if (dataTotal != null)
                resultTotal = dataTotal.ToList();

            var result = _sktChungTuChiTietService.FindReportTongHopSoKiemTraBenhVienTuChu(string.Join(",", lstDonVi.Select(n => n.ValueItem)), _sessionService.Current.YearOfWork, loaiNNS);
            if (result == null || !result.Any()) return false;
            lstData = _mapper.Map<List<ReportTongHopPhanBoSoKiemTra>>(result);

            if (dataTotal != null && dataTotal.Any())
                lstData = _mapper.Map<List<ReportTongHopPhanBoSoKiemTra>>(dataTotal);
            int index = 0;

            foreach (var item in lstDonVi)
            {
                index++;
                var dataDonVi = _sktChungTuChiTietService.FindReportTongHopSoKiemTraBenhVienTuChu(item.ValueItem, _sessionService.Current.YearOfWork, loaiNNS);
                switch (index)
                {
                    case 1:
                        tenDonVi1 = item.DisplayItem;
                        break;
                    case 2:
                        tenDonVi2 = item.DisplayItem;
                        break;
                    case 3:
                        tenDonVi3 = item.DisplayItem;
                        break;
                    case 4:
                        tenDonVi4 = item.DisplayItem;
                        break;
                    case 5:
                        tenDonVi5 = item.DisplayItem;
                        break;
                }
                if (dataDonVi == null) continue;
                foreach (var obj in dataDonVi)
                {
                    switch (index)
                    {
                        case 1:
                            lstData.Where(n => n.IIdMlskt == obj.IIdMlskt).Select(n =>
                            {
                                n.TuChiDV1 = obj.TongTuChi / donViTinh;
                                n.TongTuChiPB += obj.TongTuChi / donViTinh;
                                return n;
                            }).ToList();
                            break;
                        case 2:
                            lstData.Where(n => n.IIdMlskt == obj.IIdMlskt).Select(n =>
                            {
                                n.TuChiDV2 = obj.TongTuChi / donViTinh;
                                n.TongTuChiPB += obj.TongTuChi / donViTinh;
                                return n;
                            }).ToList();
                            break;
                        case 3:
                            lstData.Where(n => n.IIdMlskt == obj.IIdMlskt).Select(n =>
                            {
                                n.TuChiDV3 = obj.TongTuChi / donViTinh;
                                n.TongTuChiPB += obj.TongTuChi / donViTinh;
                                return n;
                            }).ToList();
                            break;
                        case 4:
                            lstData.Where(n => n.IIdMlskt == obj.IIdMlskt).Select(n =>
                            {
                                n.TuChiDV4 = obj.TongTuChi / donViTinh;
                                n.TongTuChiPB += obj.TongTuChi / donViTinh;
                                return n;
                            }).ToList();
                            break;
                        case 5:
                            lstData.Where(n => n.IIdMlskt == obj.IIdMlskt).Select(n =>
                            {
                                n.TuChiDV5 = obj.TongTuChi / donViTinh;
                                n.TongTuChiPB += obj.TongTuChi / donViTinh;
                                return n;
                            }).ToList();
                            break;
                    }
                }

            }

            CalculateData(lstData);
            lstData = lstData.Where(item => item.TongTuChi != 0 || item.TuChiDV1 != 0 || item.TuChiDV2 != 0
                || item.TuChiDV3 != 0 || item.TuChiDV4 != 0 || item.TuChiDV5 != 0).ToList();

            List<ReportTongHopPhanBoSoKiemTra> listDataSummary = new List<ReportTongHopPhanBoSoKiemTra>();
            if (iPage == 1 && resultTotal != null)
            {
                CalculateData(resultTotal);
                resultTotal = resultTotal.Where(item => item.TongTuChi != 0 || item.TuChiDV1 != 0 || item.TuChiDV2 != 0
                || item.TuChiDV3 != 0 || item.TuChiDV4 != 0 || item.TuChiDV5 != 0).ToList();

                listDataSummary = resultTotal.Where(x => x.IIdMlsktCha == Guid.Empty || x.IIdMlsktCha == null).ToList();
            }

            data.Add("TenDV1", tenDonVi1);
            data.Add("TenDV2", tenDonVi2);
            data.Add("TenDV3", tenDonVi3);
            data.Add("TenDV4", tenDonVi4);
            data.Add("TenDV5", tenDonVi5);
            data.Add("SumTongTuChiPB", listDataSummary.Sum(x => x.TongTuChiPB));
            data.Add("SumTuChiDV1", listDataSummary.Sum(x => x.TuChiDV1));
            data.Add("SumTuChiDV2", listDataSummary.Sum(x => x.TuChiDV2));
            data.Add("SumTuChiDV3", listDataSummary.Sum(x => x.TuChiDV3));
            data.Add("SumTuChiDV4", listDataSummary.Sum(x => x.TuChiDV4));
            data.Add("SumTuChiDV5", listDataSummary.Sum(x => x.TuChiDV5));
            data.Add("ListData", lstData);
            return true;
        }

        private bool GetDataExportBCTongHopSktBvTuChuDoc(Dictionary<string, object> data, int donViTinh, int loaiNNS)
        {
            var lstDonVi = ListDonVi.Where(item => item.IsChecked);
            if (lstDonVi == null || !lstDonVi.Any()) return false;
            List<ReportTongHopPhanBoSoKiemTra> lstData = new List<ReportTongHopPhanBoSoKiemTra>();
            var result = _sktChungTuChiTietService.FindReportTongHopSoKiemTraBenhVienTuChu(string.Join(",", lstDonVi.Select(n => n.ValueItem)), _sessionService.Current.YearOfWork, loaiNNS);
            if (result == null || !result.Any()) return false;
            lstData = _mapper.Map<List<ReportTongHopPhanBoSoKiemTra>>(result);
            lstData = lstData.Select(n => { n.TongTuChi = n.TongTuChi / donViTinh; return n; }).ToList();
            Dictionary<Guid, Dictionary<string, ReportTongHopPhanBoSoKiemTra>> dicMlsktDonVi = new Dictionary<Guid, Dictionary<string, ReportTongHopPhanBoSoKiemTra>>();
            foreach (var item in lstDonVi)
            {
                var dataDonVi = _sktChungTuChiTietService.FindReportTongHopSoKiemTraBenhVienTuChu(item.ValueItem, _sessionService.Current.YearOfWork, loaiNNS);
                if (dataDonVi == null || !dataDonVi.Any()) continue;
                foreach (var child in dataDonVi)
                {
                    if (child.TongTuChi == 0) continue;
                    child.TongTuChi = child.TongTuChi / donViTinh;
                    if (!dicMlsktDonVi.ContainsKey(child.IIdMlskt))
                        dicMlsktDonVi.Add(child.IIdMlskt, new Dictionary<string, ReportTongHopPhanBoSoKiemTra>());
                    dicMlsktDonVi[child.IIdMlskt].Add(item.DisplayItem, child);
                }
            }
            CalculateData(lstData);

            lstData = lstData.Where(item => item.TongTuChi != 0 || item.TuChiDV1 != 0 || item.TuChiDV2 != 0
                || item.TuChiDV3 != 0 || item.TuChiDV4 != 0 || item.TuChiDV5 != 0).ToList();
            List<ReportTongHopPhanBoSoKiemTra> lstResult = new List<ReportTongHopPhanBoSoKiemTra>();
            foreach (var item in lstData)
            {
                lstResult.Add(item);
                if (!dicMlsktDonVi.ContainsKey(item.IIdMlskt)) continue;
                foreach (var child in dicMlsktDonVi[item.IIdMlskt])
                {
                    lstResult.Add(new ReportTongHopPhanBoSoKiemTra()
                    {
                        sMoTa = child.Key,
                        TongTuChi = child.Value.TongTuChi,
                        TongMuaHangHienVatDacThu = child.Value.TongMuaHangHienVatDacThu,
                        TongMuaHangHienVatDacThuPB = child.Value.TongMuaHangHienVatDacThuPB,
                        TongMuaHangHienVatDacThuConLai = child.Value.TongMuaHangHienVatDacThuConLai
                    });
                }
            }
            data.Add("ListData", lstResult);

            #region Sumary
            List<ReportTongHopPhanBoSoKiemTra> listDataSummary = lstData.Where(x => x.IIdMlsktCha == Guid.Empty || x.IIdMlsktCha == null).ToList();
            data.Add("SumTongTuChi", listDataSummary.Sum(x => x.TongTuChi));
            #endregion

            return true;
        }

        public void LoadTitleFirst()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TxtTitleFirst = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
            TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
            TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
        }

        public virtual void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }

        public void LoadCatUnitTypes()
        {
            _catUnitTypes = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH));
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DanhMuc> data = _danhMucService.FindByCondition(predicate).OrderBy(x => x.SGiaTri).ToList();
            _catUnitTypes = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            if (data.Count == 0)
            {
                _catUnitTypes.Insert(0, new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            _catUnitTypeSelected = _catUnitTypes.FirstOrDefault();
        }

        private void LoadVoucherTypes()
        {
            var voucherTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key},
            };

            VoucherTypes = new ObservableCollection<ComboboxItem>(voucherTypes);
            VoucherTypeSelected = VoucherTypes.ElementAt(0);
        }

        public void LoadDonVi()
        {
            ListDonVi = new ObservableCollection<CheckBoxItem>();
            var yearOfWork = _sessionService.Current.YearOfWork;
            int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

            var data = _nsDonViService.FindInTongHopSKTBenhVienTuChu(yearOfWork, loaiNNS);
            if (data != null)
            {
                ListDonVi = new ObservableCollection<CheckBoxItem>(data.Select(n => new CheckBoxItem()
                {
                    ValueItem = n.IIDMaDonVi,
                    DisplayItem = string.Join("-", n.IIDMaDonVi, n.TenDonVi),
                    NameItem = n.TenDonVi
                }).OrderBy(item => item.ValueItem));
            }
            // Filter
            _donViCollectionView = CollectionViewSource.GetDefaultView(ListDonVi);
            _donViCollectionView.Filter = obj => string.IsNullOrWhiteSpace(_searchDonVi)
                                                 || (obj is CheckBoxItem item &&
                                                     item.DisplayItem.Contains(_searchDonVi, StringComparison.OrdinalIgnoreCase));

            foreach (var org in ListDonVi)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                    OnPropertyChanged(nameof(SelectAllDonVi));
                };
            }
        }

        private void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh1MoTa);
            data.Add("ChucDanh1", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh1MoTa);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy == null ? string.Empty : dmChyKy.Ten1MoTa);
            data.Add("ThuaLenh2", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh2MoTa);
            data.Add("ChucDanh2", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh2MoTa);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy == null ? string.Empty : dmChyKy.Ten2MoTa);
            data.Add("ThuaLenh3", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh3MoTa);
            data.Add("ChucDanh3", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh3MoTa);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy == null ? string.Empty : dmChyKy.Ten3MoTa);
        }

        public string GetTemplate(string input, int iSoTo, ref string fileNamePrefix)
        {
            string sFileName = input + "_To" + iSoTo;
            if (PaperPrintTypeSelected.ValueItem == "2")
            {
                input = input + "_Doc";
                sFileName = input + "_Doc_To" + iSoTo;
            }
            else if (iSoTo != 1)
            {
                input = input + "_To2";
                sFileName = input + "_To2_To" + iSoTo;
            }

            fileNamePrefix = Path.GetFileNameWithoutExtension(sFileName);
            return Path.Combine(ExportPrefix.PATH_TL_SKT, input + FileExtensionFormats.Xlsx);
        }

        private void CalculateData(List<ReportTongHopPhanBoSoKiemTra> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.bHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TongTuChi = 0;
                    x.TongTuChiConLai = 0;
                    x.TongTuChiPB = 0;
                    x.TuChiDV1 = 0;
                    x.TuChiDV2 = 0;
                    x.TuChiDV3 = 0;
                    x.TuChiDV4 = 0;
                    x.TuChiDV5 = 0;
                    x.TuChiDV6 = 0;
                    x.TongMuaHangHienVatDacThu = 0;
                    x.TongMuaHangHienVatDacThuConLai = 0;
                    x.TongMuaHangHienVatDacThuPB = 0;
                    x.MuaHangHienVatDV1 = 0;
                    x.MuaHangHienVatDV2 = 0;
                    x.MuaHangHienVatDV3 = 0;
                    x.DacThuDV1 = 0;
                    x.DacThuDV2 = 0;
                    x.DacThuDV3 = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.bHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IIdMlsktCha, item, lstSktChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, ReportTongHopPhanBoSoKiemTra item, List<ReportTongHopPhanBoSoKiemTra> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.TongTuChi += item.TongTuChi;
            model.TongTuChiConLai += item.TongTuChiConLai;
            model.TongTuChiPB += item.TongTuChiPB;
            model.TuChiDV1 += item.TuChiDV1;
            model.TuChiDV2 += item.TuChiDV2;
            model.TuChiDV3 += item.TuChiDV3;
            model.TuChiDV4 += item.TuChiDV4;
            model.TuChiDV5 += item.TuChiDV5;
            model.TuChiDV6 += item.TuChiDV6;
            model.TongMuaHangHienVatDacThu += item.TongMuaHangHienVatDacThu;
            model.TongMuaHangHienVatDacThuConLai += item.TongMuaHangHienVatDacThuConLai;
            model.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatDacThuPB;
            model.MuaHangHienVatDV1 += item.MuaHangHienVatDV1;
            model.MuaHangHienVatDV2 += item.MuaHangHienVatDV2;
            model.MuaHangHienVatDV3 += item.MuaHangHienVatDV3;
            model.DacThuDV1 += item.DacThuDV1;
            model.DacThuDV2 += item.DacThuDV2;
            model.DacThuDV3 += item.DacThuDV3;
            CalculateParent(model.IIdMlsktCha, item, lstSktChungTuChiTiet);
        }

        public void LoadPaperPrintTypes()
        {
            var paperPrintTypes = new List<ComboboxItem>();
            paperPrintTypes = new List<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Tổng hợp SKT - Bệnh viện tự chủ", ValueItem = "1" },
                new ComboboxItem { DisplayItem = "Tổng hợp SKT - Bệnh viện tự chủ dọc", ValueItem = "2" }
            };
            PaperPrintTypes = new ObservableCollection<ComboboxItem>(paperPrintTypes);
            PaperPrintTypeSelected = PaperPrintTypes.FirstOrDefault();
            OnPropertyChanged(nameof(PaperPrintTypes));
            OnPropertyChanged(nameof(PaperPrintTypeSelected));
        }
        #endregion
    }
}
