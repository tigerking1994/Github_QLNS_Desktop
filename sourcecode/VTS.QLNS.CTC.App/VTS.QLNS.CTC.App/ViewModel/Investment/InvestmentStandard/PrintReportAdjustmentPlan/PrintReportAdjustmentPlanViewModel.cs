using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using log4net;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.PrintReportAdjustmentPlan
{
    public class PrintReportAdjustmentPlanViewModel : ViewModelBase
    {
        #region Private
        private static string[] lstDonViExclude = new string[] { "0", "1" };
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvPhanBoVonChiTietService _phanBoVonService;
        private readonly IMucLucNganSachService _mlNganSachService;
        private readonly INsNguonNganSachService _nguonNganSachService;
        private readonly IExportService _exportService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_PRINT_REPORT_ADJUSTMENT_PLAN;
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string Name => "Báo cáo điều chỉnh kế hoạch";
        public override string Description => "Báo cáo điều chỉnh kế hoạch";
        public override PackIconKind IconKind => PackIconKind.ClipboardFileOutline;
        public override Type ContentType => typeof(View.Investment.InvestmentStandard.PrintReportAdjustmentPlan.PrintReportAdjustmentPlan);

        #region Items
        private double? fSumChiTieuDauNam { get; set; }
        private double? fSumGiam { get; set; }
        private double? fSumTang { get; set; }
        private double? fSumKeHoachDieuChinh { get; set; }
        private ObservableCollection<RptDieuChinhKeHoachModel> _data;
        public ObservableCollection<RptDieuChinhKeHoachModel> Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        private string _iNamThucHien;

        public string INamThucHien
        {
            get => _iNamThucHien;
            set => SetProperty(ref _iNamThucHien, value);
        }

        private ComboboxItem _cbxLoaiNganSachSelected;
        public ComboboxItem CbxLoaiNganSachSelected
        {
            get => _cbxLoaiNganSachSelected;
            set
            {
                SetProperty(ref _cbxLoaiNganSachSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiNganSach;
        public ObservableCollection<ComboboxItem> CbxLoaiNganSach
        {
            get => _cbxLoaiNganSach;
            set => SetProperty(ref _cbxLoaiNganSach, value);
        }


        private ComboboxItem _cbxNguonVonSelected;
        public ComboboxItem CbxNguonVonSelected
        {
            get => _cbxNguonVonSelected;
            set
            {
                SetProperty(ref _cbxNguonVonSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _cbxNguonVon;
        public ObservableCollection<ComboboxItem> CbxNguonVon
        {
            get => _cbxNguonVon;
            set => SetProperty(ref _cbxNguonVon, value);
        }
        #endregion

        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportCommand { get; }

        public PrintReportAdjustmentPlanViewModel(ISessionService sessionService,
            IVdtKhvPhanBoVonChiTietService phanBoVonService,
            IMucLucNganSachService mlNganSachService,
            INsNguonNganSachService nguonNganSachService,
            IExportService exportService,
            ILog logger,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _phanBoVonService = phanBoVonService;
            _mlNganSachService = mlNganSachService;
            _nguonNganSachService = nguonNganSachService;
            _logger = logger;
            _mapper = mapper;
            _exportService = exportService;
            SearchCommand = new RelayCommand(obj => OnSearch());
            ExportCommand = new RelayCommand(o => OnExport(ExportType.EXCEL));
        }

        #region Relay Command
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            LoadComboBoxLoaiNganSach();
            LoadComboBoxNguonNganSach();
        }

        public void OnSearch()
        {
            int iNamThucHien = 0;
            StringBuilder messageBuilder = new StringBuilder();
            if (CbxNguonVonSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Nguồn vốn");
                messageBuilder.AppendLine();
            }
            if (CbxLoaiNganSachSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Loại nguồn vốn");
                messageBuilder.AppendLine();
            }
            if (string.IsNullOrEmpty(INamThucHien))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Năm thực hiện");
                messageBuilder.AppendLine();
            }
            else if (!int.TryParse(INamThucHien, out iNamThucHien))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorFormat, "Năm thực hiện");
                messageBuilder.AppendLine();
            }
            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join(Environment.NewLine, messageBuilder.ToString()));
                LoadData();
                return;
            }

            var data = _phanBoVonService.GetRptDieuChinhKeHoach(int.Parse(CbxNguonVonSelected.ValueItem),
                iNamThucHien, CbxLoaiNganSachSelected.ValueItem, _sessionService.Current.Principal);
            List<RptDieuChinhKeHoachModel> lstDataConvert = _mapper.Map<List<RptDieuChinhKeHoachModel>>(data);
            ConvertData(lstDataConvert);
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("iNamThucHien", INamThucHien);
                    data.Add("fSumChiTieuDauNam", fSumChiTieuDauNam);
                    data.Add("fSumGiam", fSumGiam);
                    data.Add("fSumTang", fSumTang);
                    data.Add("fSumKeHoachDieuChinh", fSumKeHoachDieuChinh);
                    data.Add("Items", Data);

                    string templateFileName = "rpt_KeHoachDieuChinhVon.xlsx";
                    string fileNamePrefix = "KeHoachDieuChinhVon";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<RptDieuChinhKeHoachModel>(templateFileName, data);
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
        #endregion

        #region Helper
        private void ConvertData(List<RptDieuChinhKeHoachModel> lstDataConvert)
        {
            Dictionary<string, Dictionary<Guid, Dictionary<Guid, int>>> dicParent = new Dictionary<string, Dictionary<Guid, Dictionary<Guid, int>>>();
            Dictionary<string, int> dicParentIndex = new Dictionary<string, int>();
            List<RptDieuChinhKeHoachModel> data = new List<RptDieuChinhKeHoachModel>();
            int iIndex = 0;
            int iType = 1;
            fSumChiTieuDauNam = lstDataConvert.Sum(n => n.fChiTieuDauNam ?? 0);
            fSumGiam = lstDataConvert.Sum(n => n.fGiam ?? 0);
            fSumTang = lstDataConvert.Sum(n => n.fTang ?? 0);
            fSumKeHoachDieuChinh = lstDataConvert.Sum(n => n.fKeHoachDieuChinh);
            foreach (var item in lstDataConvert)
            {
                iIndex++;
                if (!dicParent.ContainsKey(item.iID_MaDonViQuanLy))
                {
                    dicParent.Add(item.iID_MaDonViQuanLy, new Dictionary<Guid, Dictionary<Guid, int>>());
                    dicParentIndex.Add(item.iID_MaDonViQuanLy, iIndex);
                    data.Add(new RptDieuChinhKeHoachModel() { iStt = iIndex, sTenDuAn = item.sTenDonVi.ToUpper(), IsHangCha = true, level = 1, sXauNoiChuoi = ToRoman(iType) });
                    iType++;
                    iIndex++;
                }
                if (!dicParent[item.iID_MaDonViQuanLy].ContainsKey(item.iID_LoaiCongTrinhID))
                {
                    dicParent[item.iID_MaDonViQuanLy].Add(item.iID_LoaiCongTrinhID, new Dictionary<Guid, int>());
                    dicParentIndex.Add(item.iID_MaDonViQuanLy + "\t" + item.iID_LoaiCongTrinhID.ToString().ToUpper(), iIndex);
                    data.Add(new RptDieuChinhKeHoachModel()
                    {
                        iStt = iIndex,
                        sTenDuAn = "  " + item.sTenLoaiCongTrinh.ToUpper(),
                        IsHangCha = true,
                        level = 2,
                        sXauNoiChuoi = string.Empty,
                        iID_MaDonViQuanLy = item.iID_MaDonViQuanLy
                    });
                    iIndex++;
                }
                if (!dicParent[item.iID_MaDonViQuanLy][item.iID_LoaiCongTrinhID].ContainsKey(item.iID_CapPheDuyetID))
                {
                    dicParent[item.iID_MaDonViQuanLy][item.iID_LoaiCongTrinhID].Add(item.iID_CapPheDuyetID, iIndex);
                    dicParentIndex.Add(item.iID_MaDonViQuanLy
                        + "\t" + item.iID_LoaiCongTrinhID.ToString().ToUpper()
                         + "\t" + item.iID_CapPheDuyetID.ToString().ToUpper(), iIndex);
                    data.Add(new RptDieuChinhKeHoachModel()
                    {
                        iStt = iIndex,
                        sTenDuAn = "    " + item.sTenPhanCap,
                        IsHangCha = true,
                        level = 3,
                        sXauNoiChuoi = string.Empty,
                        iID_MaDonViQuanLy = item.iID_MaDonViQuanLy,
                        iID_LoaiCongTrinhID = item.iID_LoaiCongTrinhID
                    });
                    iIndex++;
                }
                item.sTenDuAn = "      " + item.sTenDuAn;
                item.iStt = iIndex;
                data.Add(item);
            }

            int iRowIndex = 0;
            foreach (var sMaDonVi in dicParent.Keys)
            {
                foreach (var idCongTrinh in dicParent[sMaDonVi].Keys)
                {
                    string sIdCongTrinh = idCongTrinh.ToString().ToUpper();
                    foreach (var idPhanCap in dicParent[sMaDonVi][idCongTrinh].Keys)
                    {
                        string sIdPhanCap = idPhanCap.ToString().ToUpper();
                        ClsSumRow item1 = (from dt in data
                                           where dt.iID_MaDonViQuanLy == sMaDonVi
                                           && dt.iID_LoaiCongTrinhID == idCongTrinh
                                           && dt.iID_CapPheDuyetID == idPhanCap
                                           && !dt.IsHangCha
                                           group dt by 1 into g
                                           select new ClsSumRow()
                                           {
                                               fSumRowDauNam = g.Sum(n => n.fChiTieuDauNam ?? 0),
                                               fSumRowGiam = g.Sum(n => n.fGiam ?? 0),
                                               fSumRowTang = g.Sum(n => n.fTang ?? 0)
                                           }).FirstOrDefault();
                        iRowIndex = dicParentIndex[sMaDonVi + "\t" + sIdCongTrinh + "\t" + sIdPhanCap];
                        data[iRowIndex - 1].fTang = item1.fSumRowTang;
                        data[iRowIndex - 1].fGiam = item1.fSumRowGiam;
                        data[iRowIndex - 1].fChiTieuDauNam = item1.fSumRowDauNam;
                    }
                    ClsSumRow item2 = (from dt in data
                                       where dt.iID_MaDonViQuanLy == sMaDonVi
                                       && dt.iID_LoaiCongTrinhID == idCongTrinh
                                       && dt.IsHangCha && dt.level == 3
                                       group dt by 1 into g
                                       select new ClsSumRow()
                                       {
                                           fSumRowDauNam = g.Sum(n => n.fChiTieuDauNam ?? 0),
                                           fSumRowGiam = g.Sum(n => n.fGiam ?? 0),
                                           fSumRowTang = g.Sum(n => n.fTang ?? 0)
                                       }).FirstOrDefault();
                    iRowIndex = dicParentIndex[sMaDonVi + "\t" + sIdCongTrinh];
                    data[iRowIndex - 1].fTang = item2.fSumRowTang;
                    data[iRowIndex - 1].fGiam = item2.fSumRowGiam;
                    data[iRowIndex - 1].fChiTieuDauNam = item2.fSumRowDauNam;
                }
                ClsSumRow item3 = (from dt in data
                                   where dt.iID_MaDonViQuanLy == sMaDonVi
                                   && dt.IsHangCha && dt.level == 2
                                   group dt by 1 into g
                                   select new ClsSumRow()
                                   {
                                       fSumRowDauNam = g.Sum(n => n.fChiTieuDauNam ?? 0),
                                       fSumRowGiam = g.Sum(n => n.fGiam ?? 0),
                                       fSumRowTang = g.Sum(n => n.fTang ?? 0)
                                   }).FirstOrDefault();
                iRowIndex = dicParentIndex[sMaDonVi];
                data[iRowIndex - 1].fTang = item3.fSumRowTang;
                data[iRowIndex - 1].fGiam = item3.fSumRowGiam;
                data[iRowIndex - 1].fChiTieuDauNam = item3.fSumRowDauNam;
            }

            Data = _mapper.Map<ObservableCollection<RptDieuChinhKeHoachModel>>(data);
        }

        public void LoadComboBoxLoaiNganSach()
        {
            _cbxLoaiNganSach = new ObservableCollection<ComboboxItem>(new List<ComboboxItem>());
            var data = _mlNganSachService.GetLoaiNganSachByNamLamViec(_sessionService.Current.YearOfWork)
                .Select(n => new ComboboxItem() { ValueItem = n.Lns, DisplayItem = n.MoTa }).Distinct().ToList();
            _cbxLoaiNganSach = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(CbxLoaiNganSach));
        }

        private void LoadComboBoxNguonNganSach()
        {
            _cbxNguonVon = new ObservableCollection<ComboboxItem>(new List<ComboboxItem>());
            var data = _nguonNganSachService.FindNguonNganSach()
                .Select(n => new ComboboxItem() { ValueItem = (n.IIdMaNguonNganSach ?? 0).ToString(), DisplayItem = n.STen });
            _cbxNguonVon = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(CbxNguonVon));
        }

        private static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }

        class ClsSumRow
        {
            public double fSumRowDauNam { get; set; }
            public double fSumRowGiam { get; set; }
            public double fSumRowTang { get; set; }
        }
        #endregion
    }
}
