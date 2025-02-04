using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Criteria;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.PrintReport
{
    public class ProjectCapitalAdjustmentReportViewModel : GridViewModelBase<ProAdjustementReport>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly IVdtKhvPhanBoVonChiTietService _vdtKhvPhanBoVonChiTietService;
        private readonly ILog _logger;
        private readonly IExportService _exportService;
        private ProjectAdjustementSearch _conditionSearch;

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_PROJECT_CAPITAL_ADJUSTMENT_REPORT;
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string Name => "Báo cáo điều chỉnh kế hoạch vốn dự án";
        public override string Description => "Báo cáo điều chỉnh kế hoạch vốn dự án";
        public override Type ContentType => typeof(ProjectCapitalAdjustmentReport);
        public override PackIconKind IconKind => PackIconKind.ClipboardFileOutline;

        private ComboboxItem _cbxNguonVonSelected;
        public ComboboxItem CbxNguonVonSelected
        {
            get => _cbxNguonVonSelected;
            set => SetProperty(ref _cbxNguonVonSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxNguonVon;
        public ObservableCollection<ComboboxItem> CbxNguonVon
        {
            get => _cbxNguonVon;
            set => SetProperty(ref _cbxNguonVon, value);
        }

        private ComboboxItem _cbxLoaiNguonVonSelected;
        public ComboboxItem CbxLoaiNguonVonSelected
        {
            get => _cbxLoaiNguonVonSelected;
            set
            {
                SetProperty(ref _cbxLoaiNguonVonSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiNguonVon;
        public ObservableCollection<ComboboxItem> CbxLoaiNguonVon
        {
            get => _cbxLoaiNguonVon;
            set => SetProperty(ref _cbxLoaiNguonVon, value);
        }

        private int _yearPlan;
        public int YearPlan
        {
            get => _yearPlan;
            set => SetProperty(ref _yearPlan, value);
        }

        public RelayCommand SearchCommand { get; set; }
        public RelayCommand PrintCommand { get; }

        public ProjectCapitalAdjustmentReportViewModel(IMapper maper,
            ISessionService sessionService,
            INsNguonNganSachService nsNguonNganSachService,
            INsMucLucNganSachService nsMucLucNganSachService,
            IVdtKhvPhanBoVonChiTietService vdtKhvPhanBoVonChiTietService,
            ILog logger,
            IExportService exportService)
        {
            _mapper = maper;
            _sessionService = sessionService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _vdtKhvPhanBoVonChiTietService = vdtKhvPhanBoVonChiTietService;
            _exportService = exportService;
            _logger = logger;

            SearchCommand = new RelayCommand(obj => LoadData());
            PrintCommand = new RelayCommand(obj => OnPrint(ExportType.EXCEL));
        }

        public override void Init()
        {
            try
            {
                _yearPlan = DateTime.Now.Year;
                LoadNguonVon();
                LoadLoaiNguonVon();
                LoadData();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ResetConditionSearch()
        {
            _conditionSearch = new ProjectAdjustementSearch()
            {
                NamKeHoach = _yearPlan,
                NguonVonId = (CbxNguonVonSelected != null && CbxNguonVonSelected.ValueItem != null) ? Int32.Parse(CbxNguonVonSelected.ValueItem) : 0,
                LoaiNguonVon = Guid.Parse(CbxLoaiNguonVonSelected.ValueItem),
                UserName = _sessionService.Current.Principal
            };
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                ResetConditionSearch();

                List<VdtKhvPhanBoVonChiTietProAdjustementReportQuery> lstDuAn = _vdtKhvPhanBoVonChiTietService.FindByProjectAdjustementReport(_conditionSearch).ToList();
                List<VdtKhvPhanBoVonChiTietProAdjustementReportQuery> listParent = _vdtKhvPhanBoVonChiTietService.FindByProjectAdjustementReportParent(_conditionSearch).ToList();

                if (listParent != null && listParent.Count > 0 && lstDuAn != null && lstDuAn.Count > 0)
                {
                    foreach (var item in lstDuAn)
                    {
                        int index = listParent.IndexOf(listParent.Where(n => n.MaThuTu == item.MaThuTu).FirstOrDefault());
                        if (index > 0)
                        {
                            listParent.Insert(index + 1, item);
                        }
                    }
                }

                if (listParent != null && listParent.Count > 0)
                {
                    int int_muc = 0;
                    foreach (var item in listParent.OrderBy(x => x.IdDonVi).ToList())
                    {
                        if (item.MaThuTu == null && item.CT == null && item.CPD == null)
                        {
                            int_muc += 1;
                            item.STenMuc = string.Format("Mục {0}", int_muc);
                        }

                        if (!item.IsHangCha)
                        {
                            item.STenMuc = string.Format("{0}-{1}-{2}-{3}", item.M, item.TM, item.TTM, item.NG);
                        }
                    }
                }

                Items = _mapper.Map<ObservableCollection<ProAdjustementReport>>(listParent);

                CaculateData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CaculateData()
        {
            try
            {
                var listChildren = Items.Where(x => !(bool)x.IsHangCha).ToList();

                if (listChildren != null && listChildren.Count > 0)
                {
                    foreach (var item in listChildren)
                    {
                        FindParent(item);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void FindParent(ProAdjustementReport item)
        {
            try
            {
                var parentItemCapPheDuyet = Items.Where(x => (bool)x.IsHangCha && x.MaThuTu != null
                                        && x.IdDonVi == item.IdDonVi && x.CT == item.CT && x.CPD == item.CPD).FirstOrDefault();

                if (parentItemCapPheDuyet == null) return;

                var parentItemLoaiCongTrinh = Items.Where(x => (bool)x.IsHangCha && x.MaThuTu != null
                                                && x.IdDonVi == item.IdDonVi && x.CT == item.CT && x.CPD == null).FirstOrDefault();

                var parentItemDonViQuanLy = Items.Where(x => (bool)x.IsHangCha && x.MaThuTu == null
                                            && x.CT == null && x.CPD == null && x.IdDonVi == item.IdDonVi).FirstOrDefault();

                CalculateParent(item, parentItemCapPheDuyet);

                if (parentItemLoaiCongTrinh != null)
                {
                    CalculateParent(item, parentItemLoaiCongTrinh);
                }

                if (parentItemDonViQuanLy != null)
                {
                    CalculateParent(item, parentItemDonViQuanLy);
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateParent(ProAdjustementReport child, ProAdjustementReport parent)
        {
            parent.ChiTieuDauNam += child.ChiTieuDauNam;
            parent.Tang += child.Tang;
            parent.Giam += child.Giam;
        }

        private void LoadNguonVon()
        {
            try
            {
                List<Core.Domain.NsNguonNganSach> lstNguonNganSach = _nsNguonNganSachService.FindNguonNganSach().ToList();

                CbxNguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(lstNguonNganSach);

                if (CbxNguonVon != null && CbxNguonVon.Count > 0)
                {
                    CbxNguonVonSelected = CbxNguonVon.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadLoaiNguonVon()
        {
            try
            {
                List<NsMucLucNganSach> lstLoaiNganSach = _nsMucLucNganSachService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();

                CbxLoaiNguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(lstLoaiNganSach);

                if (CbxLoaiNguonVon != null && CbxLoaiNguonVon.Count > 0)
                {
                    CbxLoaiNguonVonSelected = CbxLoaiNguonVon.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnPrint(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Items", Items);
                    data.Add("Header1", string.Format("KẾ HOẠCH ĐIỀU CHỈNH CHI TIẾT DANH MỤC VÀ CHỈ TIÊU NGÂN SÁCH QUỐC PHÒNG NĂM", _yearPlan));
                    data.Add("Header2", "(Kèm theo Công văn số 5237/CTC-XDCB ngày   tháng   năm   của Cục Tài Chính)");

                    string templateFileName = ProjectCapitialAdjustmentType.KE_HOACH_DIEU_CHINH_VON_CHI_TIET_TEMPLATE;
                    string fileNamePrefix = "rptKeHoachDieuChinhVonChiTiet";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ProAdjustementReport>(templateFileName, data);
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
    }
}
