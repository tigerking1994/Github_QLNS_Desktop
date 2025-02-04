using AutoMapper;
using ControlzEx.Standard;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.QuyetToanVDT
{
    public class QuyetToanVDTPrintReportViewModel : ViewModelBase
    {
        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly IVdtQtBcQuyetToanNienDoService _service;
        private readonly INsNguonNganSachService _nguonNganSachService;
        private readonly INsDonViService _nsDonViService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IVdtDmLoaiCongTrinhService _loaicongtrinhService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private Dictionary<string, DonVi> _dicDonVi;
        private const string DU_AN_MO_MOI = "Dự án mở mới";
        private const string DU_AN_CHUYEN_TIEP = "Dự án chuyển tiếp";
        private List<VdtDmLoaiCongTrinh> lstLoaiCongTrinh;
        #endregion

        #region Items
        private VdtQtBcquyetToanNienDoModel _selected;
        public VdtQtBcquyetToanNienDoModel Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiBaoCao;
        public ObservableCollection<ComboboxItem> ItemsLoaiBaoCao
        {
            get => _itemsLoaiBaoCao;
            set => SetProperty(ref _itemsLoaiBaoCao, value);
        }

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set
            {
                if (SetProperty(ref _selectedLoaiBaoCao, value))
                {
                    if (SelectedLoaiBaoCao != null)
                    {
                        switch (int.Parse(SelectedLoaiBaoCao.ValueItem))
                        {
                            case (int)PaymentTypeEnum.Type.THANH_TOAN:
                                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONNAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                                break;
                            case (int)PaymentTypeEnum.Type.TAM_UNG:
                                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONUNG) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                                break;
                        }
                    }
                }
                if (_dmChuKy != null)
                {
                    TxtHeader1 = _dmChuKy.TieuDe1MoTa ?? string.Empty;
                    TxtHeader2 = _dmChuKy.TieuDe2MoTa ?? string.Empty;
                    TxtHeader3 = _dmChuKy.TieuDe3MoTa ?? string.Empty;
                }
                else
                {
                    TxtHeader1 = string.Empty;
                    TxtHeader2 = string.Empty;
                    TxtHeader3 = string.Empty;
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                    if (_selectedDonVi != null && _dicDonVi.ContainsKey(_selectedDonVi.ValueItem))
                    {
                        if (_dicDonVi[_selectedDonVi.ValueItem].Loai == "0")
                        {
                            _cap1 = (NSConstants.BO_QUOC_PHONG).ToUpper();
                        }
                        else
                        {
                            var currentDivision = _nsDonViService.FindCurrentDonViSuDungByNamLamViec(_sessionService.Current.YearOfWork);
                            if (currentDivision != null)
                                _cap1 = currentDivision.TenDonVi.ToUpper();
                        }
                    }
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsNguonVon;
        public ObservableCollection<ComboboxItem> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private ComboboxItem _selectedNguonVon;
        public ComboboxItem SelectedNguonVon
        {
            get => _selectedNguonVon;
            set => SetProperty(ref _selectedNguonVon, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDonViTinh;
        public ObservableCollection<ComboboxItem> ItemsDonViTinh
        {
            get => _itemsDonViTinh;
            set => SetProperty(ref _itemsDonViTinh, value);
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }

        private int? _iNamKeHoach;
        public int? INamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);

        }

        private string _txtHeader1;
        public string TxtHeader1
        {
            get => _txtHeader1;
            set => SetProperty(ref _txtHeader1, value);
        }

        private string _txtHeader2;
        public string TxtHeader2
        {
            get => _txtHeader2;
            set => SetProperty(ref _txtHeader2, value);
        }

        private string _txtHeader3;
        public string TxtHeader3
        {
            get => _txtHeader3;
            set => SetProperty(ref _txtHeader3, value);
        }
        #endregion

        #region Relay Command
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        #endregion

        public DmChuKyDialogViewModel DmChuKyDialogViewModel { get; set; }

        public QuyetToanVDTPrintReportViewModel(
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            IVdtQtBcQuyetToanNienDoService service,
            INsNguonNganSachService nguonNganSachService,
            INsDonViService nsDonViService,
            IDmChuKyService dmChuKyService,
            IExportService exportService,
            IDanhMucService danhMucService,
            IVdtDmLoaiCongTrinhService loaicongtrinhService,
            ILog logger,
            IMapper mapper,
            ISessionService sessionService)
        {
            _service = service;
            _nguonNganSachService = nguonNganSachService;
            _nsDonViService = nsDonViService;
            _dmChuKyService = dmChuKyService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _loaicongtrinhService = loaicongtrinhService;
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;

            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintBrowserCommand = new RelayCommand(obj => OnPrintBrowser());
            PrintExcelCommand = new RelayCommand(obj => OnPrintExcel());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        #region RelayCommand
        public override void Init()
        {
            lstLoaiCongTrinh = _loaicongtrinhService.FindAll().ToList();
            LoadLoaiBaoCao();
            LoadDonVi();
            LoadDonViTinh();
            LoadNguonVon();
            LoadDanhMuc();
            SetSelectedValue();
        }

        private void SetSelectedValue()
        {
            if (Selected == null || Selected.Id == Guid.Empty) return;
            SelectedLoaiBaoCao = ItemsLoaiBaoCao.FirstOrDefault(n => n.ValueItem == (Selected.ILoaiThanhToan ?? 0).ToString());
            SelectedDonVi = ItemsDonVi.FirstOrDefault(n => n.ValueItem == Selected.IIDMaDonViQuanLy);
            SelectedNguonVon = ItemsNguonVon.FirstOrDefault(n => n.ValueItem == (Selected.IIDNguonVonID ?? 0).ToString());
            INamKeHoach = Selected.INamKeHoach;
            SelectedDonViTinh = ItemsDonViTinh.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedLoaiBaoCao));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedNguonVon));
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(SelectedDonViTinh));
        }

        private void OnConfigSign()
        {
            try
            {
                if (SelectedLoaiBaoCao == null)
                {
                    MessageBox.Show(string.Format(Resources.MsgErrorDataEmpty, "loại báo cáo"));
                    return;
                }
                DmChuKyModel chuKyModel = new DmChuKyModel();

                switch (int.Parse(SelectedLoaiBaoCao.ValueItem))
                {
                    case (int)PaymentTypeEnum.Type.THANH_TOAN:
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONNAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                        break;
                    case (int)PaymentTypeEnum.Type.TAM_UNG:
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONUNG) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                        break;
                }

                if (_dmChuKy == null)
                {
                    switch (int.Parse(SelectedLoaiBaoCao.ValueItem))
                    {
                        case (int)PaymentTypeEnum.Type.THANH_TOAN:
                            chuKyModel.IdType = TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONNAM;
                            break;
                        case (int)PaymentTypeEnum.Type.TAM_UNG:
                            chuKyModel.IdType = TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONUNG;
                            break;
                    }
                }
                else
                {
                    chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
                }

                DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
                DmChuKyDialogViewModel.SavedAction = obj =>
                {
                    DmChuKyModel chuKy = (DmChuKyModel)obj;
                    TxtHeader1 = chuKy.TieuDe1MoTa ?? string.Empty;
                    TxtHeader2 = chuKy.TieuDe2MoTa ?? string.Empty;
                    TxtHeader3 = chuKy.TieuDe3MoTa ?? string.Empty;
                };
                DmChuKyDialogViewModel.Init();
                DmChuKyDialogViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnPrintBrowser()
        {
            ExportFile(ExportType.PDF);
        }

        private void OnPrintExcel()
        {
            ExportFile(ExportType.EXCEL, false);
        }
        #endregion

        #region Helper
        private bool ValidateForm()
        {
            List<string> messErrors = new List<string>();
            if (SelectedLoaiBaoCao == null)
                messErrors.Add(string.Format(Resources.MsgErrorDataEmpty, "loại báo cáo"));
            if (SelectedDonVi == null)
                messErrors.Add(string.Format(Resources.MsgErrorDataEmpty, "đơn vị"));
            if (SelectedNguonVon == null)
                messErrors.Add(string.Format(Resources.MsgErrorDataEmpty, "nguồn vốn"));
            if (INamKeHoach == null)
                messErrors.Add(string.Format(Resources.MsgErrorDataEmpty, "năm kế hoạch"));
            if (SelectedDonViTinh == null)
                messErrors.Add(string.Format(Resources.MsgErrorDataEmpty, "đơn vị tính"));
            if (messErrors.Count != 0)
            {
                MessageBox.Show(string.Join("\n", messErrors));
                return false;
            }
            return true;
        }

        private void ExportFile(ExportType fileType, bool bIsBaoCao = true)
        {
            if (!ValidateForm()) return;
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    var lstQuyetToan = _service.GetDeNghiQuyetToanNienDoByCondition(int.Parse(SelectedLoaiBaoCao.ValueItem), SelectedDonVi.ValueItem, int.Parse(SelectedNguonVon.ValueItem), INamKeHoach ?? 0);
                    if(lstQuyetToan != null)
                    {
                        foreach(var item in lstQuyetToan)
                        {
                            results.Add(ExportQuyetToan(item.Id, fileType, bIsBaoCao));
                            if (bIsBaoCao)
                            {
                                if (int.Parse(SelectedLoaiBaoCao.ValueItem) == (int)PaymentTypeEnum.Type.THANH_TOAN && (item.IIdNguonVonId ?? 0) == 1)
                                    results.Add(ExportQuyetToanPhanTich(item.Id, fileType));
                            }
                            e.Result = results;
                        }
                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, fileType);
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

        private ExportResult ExportQuyetToanPhanTich(Guid? iIdQuyetToan, ExportType exportType)
        {
            switch (int.Parse(SelectedLoaiBaoCao.ValueItem))
            {
                case (int)PaymentTypeEnum.Type.THANH_TOAN:
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONNAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    break;
                case (int)PaymentTypeEnum.Type.TAM_UNG:
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONUNG) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    break;
            }
            var objQuyetToan = _service.Find(iIdQuyetToan ?? Guid.Empty);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("sTenDonVi", SelectedDonVi == null ? string.Empty : SelectedDonVi.DisplayItem.Split('-')[1]);
            data.Add("dNgayHienTai", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
            data.Add("iNam", INamKeHoach);
            data.Add("DiaDiem", _diaDiem);
            data.Add("Cap1", _cap1);
            data.Add("h2", string.Format("Đơn vị tính: {0}", SelectedDonViTinh.DisplayItem));
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            data.Add("Title1", string.Format(TxtHeader1, INamKeHoach));
            data.Add("Title2", string.Format(TxtHeader2, INamKeHoach));

            var itemExportPhanTich = LoadDataPhanTich(objQuyetToan);
            var itemParent = itemExportPhanTich.Where(x => x.LoaiParent == 0 && x.IIdLoaiCongTrinhParent == null);
            RptVdtQtBcQuyetToanNienDoPhanTichModel dataSummary = new RptVdtQtBcQuyetToanNienDoPhanTichModel();
            dataSummary.FDuToanCnsChuaGiaiNganTaiKbNamTruoc = itemParent.Sum(x => x.FDuToanCnsChuaGiaiNganTaiKbNamTruoc);
            dataSummary.FDuToanCnsChuaGiaiNganTaiDvNamTruoc = itemParent.Sum(x => x.FDuToanCnsChuaGiaiNganTaiDvNamTruoc);
            dataSummary.FDuToanCnsChuaGiaiNganTaiCucNamTruoc = itemParent.Sum(x => x.FDuToanCnsChuaGiaiNganTaiCucNamTruoc);
            //dataSummary.FTongChuaThuHoi = itemParent.Sum(x => x.FTongChuaThuHoi);
            //dataSummary.FTongDuToanDuocGiaoNamTruocChuyenSang = itemParent.Sum(x => x.FTongDuToanDuocGiaoNamTruocChuyenSang);
            dataSummary.FChiTieuNamNayKb = itemParent.Sum(x => x.FChiTieuNamNayKb);
            dataSummary.FChiTieuNamNayLc = itemParent.Sum(x => x.FChiTieuNamNayLc);
            //dataSummary.FTongDuToanDuocGiao = itemParent.Sum(x => x.FTongDuToanDuocGiao);
            //dataSummary.FTotalDuToanDuocGiao = itemParent.Sum(x => x.FTotalDuToanDuocGiao);
            dataSummary.FSoCapNamTrcCs = itemParent.Sum(x => x.FSoCapNamTrcCs);
            dataSummary.FSoCapNamNay = itemParent.Sum(x => x.FSoCapNamNay);
            //dataSummary.FTongSoDuocCap = itemParent.Sum(x => x.FTongSoDuocCap);
            dataSummary.FDnQuyetToanNamTrc = itemParent.Sum(x => x.FDnQuyetToanNamTrc);
            dataSummary.FDnQuyetToanNamNay = itemParent.Sum(x => x.FDnQuyetToanNamNay);
            //dataSummary.FTongDeNghiQuyetToan = itemParent.Sum(x => x.FTongDeNghiQuyetToan);
            //dataSummary.FTongChuyenNamSau = itemParent.Sum(x => x.FTongChuyenNamSau);
            //dataSummary.FTongTamUngChuaThuHoi = itemParent.Sum(x => x.FTongTamUngChuaThuHoi);
            dataSummary.FTuChuaThuHoiTaiCuc = itemParent.Sum(x => x.FTuChuaThuHoiTaiCuc);
            dataSummary.FTuChuaThuHoiTaiDonVi = itemParent.Sum(x => x.FTuChuaThuHoiTaiDonVi);
            //dataSummary.FTongDuChuaGiaiNgan = itemParent.Sum(x => x.FTongDuChuaGiaiNgan);
            dataSummary.FDuToanCnsChuaGiaiNganTaiCuc = itemParent.Sum(x => x.FDuToanCnsChuaGiaiNganTaiCuc);
            dataSummary.FDuToanCnsChuaGiaiNganTaiDv = itemParent.Sum(x => x.FDuToanCnsChuaGiaiNganTaiDv);
            dataSummary.FDuToanCnsChuaGiaiNganTaiKb = itemParent.Sum(x => x.FDuToanCnsChuaGiaiNganTaiKb);
            dataSummary.FDuToanThuHoi = itemParent.Sum(x => x.FDuToanThuHoi);


            FormatNumber formatNumber = new FormatNumber(1, exportType);
                data.Add("FormatNumber", formatNumber);
                //data.Add("Items", GetDataExportQuyetToanVonNam(objQuyetToan));
                string templateFileName = string.Empty;
                data.Add("ItemsPhanTich", itemExportPhanTich);
                data.Add("FDuToanCnsChuaGiaiNganTaiKbNamTruocSum", dataSummary.FDuToanCnsChuaGiaiNganTaiKbNamTruoc);
                data.Add("FDuToanCnsChuaGiaiNganTaiDvNamTruocSum", dataSummary.FDuToanCnsChuaGiaiNganTaiDvNamTruoc);
                data.Add("FDuToanCnsChuaGiaiNganTaiCucNamTruocSum", dataSummary.FDuToanCnsChuaGiaiNganTaiCucNamTruoc);
                data.Add("FTongChuaThuHoiSum", dataSummary.FTongChuaThuHoi);
                data.Add("FTongDuToanDuocGiaoNamTruocChuyenSangSum", dataSummary.FTongDuToanDuocGiaoNamTruocChuyenSang);
                data.Add("FChiTieuNamNayKbSum", dataSummary.FChiTieuNamNayKb);
                data.Add("FChiTieuNamNayLcSum", dataSummary.FChiTieuNamNayLc);
                data.Add("FTongDuToanDuocGiaoSum", dataSummary.FTongDuToanDuocGiao);
                data.Add("FTotalDuToanDuocGiaoSum", dataSummary.FTotalDuToanDuocGiao);
                data.Add("FSoCapNamTrcCsSum", dataSummary.FSoCapNamTrcCs);
                data.Add("FSoCapNamNaySum", dataSummary.FSoCapNamNay);
                data.Add("FTongSoDuocCapSum", dataSummary.FTongSoDuocCap);
                data.Add("FDnQuyetToanNamTrcSum", dataSummary.FDnQuyetToanNamTrc);
                data.Add("FDnQuyetToanNamNaySum", dataSummary.FDnQuyetToanNamNay);
                data.Add("FTongDeNghiQuyetToanSum", dataSummary.FTongDeNghiQuyetToan);
                data.Add("FTongChuyenNamSauSum", dataSummary.FTongChuyenNamSau);
                data.Add("FTongTamUngChuaThuHoiSum", dataSummary.FTongTamUngChuaThuHoi);
                data.Add("FTuChuaThuHoiTaiCucSum", dataSummary.FTuChuaThuHoiTaiCuc);
                data.Add("FTuChuaThuHoiTaiDonViSum", dataSummary.FTuChuaThuHoiTaiDonVi);
                data.Add("FTongDuChuaGiaiNganSum", dataSummary.FTongDuChuaGiaiNgan);
                data.Add("FDuToanCnsChuaGiaiNganTaiCucSum", dataSummary.FDuToanCnsChuaGiaiNganTaiCuc);
                data.Add("FDuToanCnsChuaGiaiNganTaiDvSum", dataSummary.FDuToanCnsChuaGiaiNganTaiDv);
                data.Add("FDuToanCnsChuaGiaiNganTaiKbSum", dataSummary.FDuToanCnsChuaGiaiNganTaiKb);
                data.Add("FDuToanThuHoiSum", dataSummary.FDuToanThuHoi);                
                templateFileName = Path.Combine(ExportPrefix.PATH_TL_QT, ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONNAM_PHANTICH);
                string fileNamePrefix = string.Format("BcPhanTich_{0}_{1}", objQuyetToan.SSoDeNghi, (INamKeHoach ?? 0));
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ExportVdtQtBcquyetToanNienDoChiTiet1Model, RptVdtQtBcQuyetToanNienDoPhanTichModel>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private ExportResult ExportQuyetToan(Guid? iIdQuyetToan, ExportType exportType, bool bIsBaoCao)
        {
            switch (int.Parse(SelectedLoaiBaoCao.ValueItem))
            {
                case (int)PaymentTypeEnum.Type.THANH_TOAN:
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONNAM) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    break;
                case (int)PaymentTypeEnum.Type.TAM_UNG:
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONUNG) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    break;
            }
            var objQuyetToan = _service.Find(iIdQuyetToan ?? Guid.Empty);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("sTenDonVi", SelectedDonVi == null ? string.Empty : SelectedDonVi.DisplayItem.Split('-')[1]);
            data.Add("dNgayHienTai", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
            data.Add("iNam", INamKeHoach);
            data.Add("DiaDiem", _diaDiem);
            data.Add("Cap1", _cap1);
            data.Add("DonViTinh", string.Format("Đơn vị tính: {0}", SelectedDonViTinh.DisplayItem));
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            data.Add("Title1", string.Format(TxtHeader1, INamKeHoach));
            data.Add("Title2", string.Format(TxtHeader2, INamKeHoach));

            var ItemExport = GetDataExportQuyetToanVonNam(objQuyetToan);
            var itemParent = ItemExport.Where(x => x.LoaiParent == 0 && x.IIdLoaiCongTrinhParent == null);
            ExportVdtQtBcquyetToanNienDoChiTiet1Model dataSummary = new ExportVdtQtBcquyetToanNienDoChiTiet1Model();
            dataSummary.FTongMucDauTu = itemParent.Sum(x => x.FTongMucDauTu);
            dataSummary.FLuyKeThanhToanNamTruoc = itemParent.Sum(x => x.FLuyKeThanhToanNamTruoc);
            dataSummary.FTamUngTheoCheDoChuaThuHoiNamTruoc = itemParent.Sum(x => x.FTamUngTheoCheDoChuaThuHoiNamTruoc);
            dataSummary.FGiaTriTamUngDieuChinhGiam = itemParent.Sum(x => x.FGiaTriTamUngDieuChinhGiam);
            dataSummary.FTamUngNamTruocThuHoiNamNay = itemParent.Sum(x => x.FTamUngNamTruocThuHoiNamNay);
            dataSummary.FKHVNamTruocChuyenNamNay = itemParent.Sum(x => x.FKHVNamTruocChuyenNamNay);
            dataSummary.FTongThanhToanVonKeoDaiNamNay = itemParent.Sum(x => x.FTongThanhToanVonKeoDaiNamNay);
            dataSummary.FTongThanhToanSuDungVonNamTruoc = itemParent.Sum(x => x.FTongThanhToanSuDungVonNamTruoc);
            dataSummary.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay = itemParent.Sum(x => x.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay);
            dataSummary.FGiaTriNamTruocChuyenNamSau = itemParent.Sum(x => x.FGiaTriNamTruocChuyenNamSau);
            dataSummary.FVonConLaiHuyBoKeoDaiNamNay = itemParent.Sum(x => x.FVonConLaiHuyBoKeoDaiNamNay);
            dataSummary.FKHVNamNay = itemParent.Sum(x => x.FKHVNamNay);
            dataSummary.FTongKeHoachThanhToanVonNamNay = itemParent.Sum(x => x.FTongKeHoachThanhToanVonNamNay);
            dataSummary.FTongThanhToanSuDungVonNamNay = itemParent.Sum(x => x.FTongThanhToanSuDungVonNamNay);
            dataSummary.FTamUngTheoCheDoChuaThuHoiNamNay = itemParent.Sum(x => x.FTamUngTheoCheDoChuaThuHoiNamNay);
            dataSummary.FGiaTriNamNayChuyenNamSau = itemParent.Sum(x => x.FGiaTriNamNayChuyenNamSau);
            dataSummary.FVonConLaiHuyBoNamNay = itemParent.Sum(x => x.FVonConLaiHuyBoNamNay);
            dataSummary.FTongVonThanhToanNamNay = itemParent.Sum(x => x.FTongVonThanhToanNamNay);
            dataSummary.FLuyKeTamUngChuaThuHoiChuyenSangNam = itemParent.Sum(x => x.FLuyKeTamUngChuaThuHoiChuyenSangNam);
            dataSummary.FLuyKeConDaThanhToanHetNamNay = itemParent.Sum(x => x.FLuyKeConDaThanhToanHetNamNay);
            if (int.Parse(SelectedLoaiBaoCao.ValueItem) == (int)PaymentTypeEnum.Type.THANH_TOAN)
            {
                FormatNumber formatNumber = new FormatNumber(1, exportType);
                data.Add("FormatNumber", formatNumber);
                data.Add("Items", ItemExport);
                data.Add("FTongMucDauTuSum", dataSummary.FTongMucDauTu);
                data.Add("FLuyKeThanhToanNamTruocSum", dataSummary.FLuyKeThanhToanNamTruoc);
                data.Add("FTamUngTheoCheDoChuaThuHoiNamTruocSum", dataSummary.FTamUngTheoCheDoChuaThuHoiNamTruoc);
                data.Add("FGiaTriTamUngDieuChinhGiamSum", dataSummary.FGiaTriTamUngDieuChinhGiam);
                data.Add("FTamUngNamTruocThuHoiNamNaySum", dataSummary.FTamUngNamTruocThuHoiNamNay);
                data.Add("FKHVNamTruocChuyenNamNaySum", dataSummary.FKHVNamTruocChuyenNamNay);
                data.Add("FTongThanhToanVonKeoDaiNamNaySum", dataSummary.FTongThanhToanVonKeoDaiNamNay);
                data.Add("FTongThanhToanSuDungVonNamTruocSum", dataSummary.FTongThanhToanSuDungVonNamTruoc);
                data.Add("FTamUngTheoCheDoChuaThuHoiKeoDaiNamNaySum", dataSummary.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay);
                data.Add("FGiaTriNamTruocChuyenNamSauSum", dataSummary.FGiaTriNamTruocChuyenNamSau);
                data.Add("FVonConLaiHuyBoKeoDaiNamNaySum", dataSummary.FVonConLaiHuyBoKeoDaiNamNay);
                data.Add("FKHVNamNaySum", dataSummary.FKHVNamNay);
                data.Add("FTongKeHoachThanhToanVonNamNaySum", dataSummary.FTongKeHoachThanhToanVonNamNay);
                data.Add("FTongThanhToanSuDungVonNamNaySum", dataSummary.FTongThanhToanSuDungVonNamNay);
                data.Add("FTamUngTheoCheDoChuaThuHoiNamNaySum", dataSummary.FTamUngTheoCheDoChuaThuHoiNamNay);
                data.Add("FGiaTriNamNayChuyenNamSauSum", dataSummary.FGiaTriNamNayChuyenNamSau);
                data.Add("FVonConLaiHuyBoNamNaySum", dataSummary.FVonConLaiHuyBoNamNay);
                data.Add("FTongVonThanhToanNamNaySum", dataSummary.FTongVonThanhToanNamNay);
                data.Add("FLuyKeTamUngChuaThuHoiChuyenSangNamSum", dataSummary.FLuyKeTamUngChuaThuHoiChuyenSangNam);
                data.Add("FLuyKeConDaThanhToanHetNamNaySum", dataSummary.FLuyKeConDaThanhToanHetNamNay);
                string templateFileName = string.Empty;
                if ((objQuyetToan.IIdNguonVonId ?? 0) == 1)
                {
                    //data.Add("ItemsPhanTich", LoadDataPhanTich(objQuyetToan));
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_QT, ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONNAM_NSQP);
                    string fileNamePrefix = string.Format("{0}_{1}", objQuyetToan.SSoDeNghi, (INamKeHoach ?? 0));
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ExportVdtQtBcquyetToanNienDoChiTiet1Model, RptVdtQtBcQuyetToanNienDoPhanTichModel>(templateFileName, data);
                    _exportService.FormatAllRowHeight(ItemExport, "STenDuAn", 12, 40, xlsFile);
                    return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }
                else
                {
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_QT, ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONNAM);
                    string fileNamePrefix = string.Format("{0}_{1}", objQuyetToan.SSoDeNghi, (INamKeHoach ?? 0));
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ExportVdtQtBcquyetToanNienDoChiTiet1Model>(templateFileName, data);
                    return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }
            }
            else
            {
                FormatNumber formatNumber = new FormatNumber(1, exportType);
                data.Add("FormatNumber", formatNumber);
                data.Add("Items", GetDataExportQuyetToanVonUng(iIdQuyetToan));

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QT, ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONUNG);
                string fileNamePrefix = string.Format("{0}_{1}", objQuyetToan.SSoDeNghi, (INamKeHoach ?? 0));
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ExportBcquyetToanNienDoVonUngChiTietModel>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }
        }

        private ObservableCollection<ExportVdtQtBcquyetToanNienDoChiTiet1Model> GetDataExportQuyetToanVonNam(VdtQtBcQuyetToanNienDo objQuyetToan)
        {
            List<VdtQtBcquyetToanNienDoChiTiet1Query> data = new List<VdtQtBcquyetToanNienDoChiTiet1Query>();
            var defaultData = _service.GetQuyetToanNienDoVonNamByParentId(objQuyetToan.Id);
            if (defaultData == null || defaultData.Count == 0)
            {
                defaultData = _service.GetDeNghiQuyetToanNienDoDetail(
                    SelectedDonVi.ValueItem, INamKeHoach ?? 0, int.Parse(SelectedNguonVon.ValueItem));
            }
            var results = SetupViewData(defaultData, (objQuyetToan.IIdNguonVonId ?? 0));
            int i = 0;
            double fTiLeDonVi = double.Parse(SelectedDonViTinh.ValueItem);
            foreach (var child in results)
            {
                child.FTongMucDauTu /= fTiLeDonVi;
                child.FLuyKeThanhToanNamTruoc /= fTiLeDonVi;
                child.FTamUngTheoCheDoChuaThuHoiNamTruoc /= fTiLeDonVi;
                child.FGiaTriTamUngDieuChinhGiam /= fTiLeDonVi;
                child.FTamUngNamTruocThuHoiNamNay /= fTiLeDonVi;
                child.FKHVNamTruocChuyenNamNay /= fTiLeDonVi;
                child.FTongThanhToanVonKeoDaiNamNay /= fTiLeDonVi;
                child.FTongThanhToanSuDungVonNamTruoc /= fTiLeDonVi;
                child.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay /= fTiLeDonVi;
                child.FGiaTriNamTruocChuyenNamSau /= fTiLeDonVi;
                child.FKHVNamNay /= fTiLeDonVi;
                child.FTongKeHoachThanhToanVonNamNay /= fTiLeDonVi;
                child.FTongThanhToanSuDungVonNamNay /= fTiLeDonVi;
                child.FTamUngTheoCheDoChuaThuHoiNamNay /= fTiLeDonVi;
                child.FGiaTriNamNayChuyenNamSau /= fTiLeDonVi;
                child.FTongVonThanhToanNamNay /= fTiLeDonVi;
                child.FLuyKeConDaThanhToanHetNamNay /= fTiLeDonVi;
                child.FVonConLaiHuyBoKeoDaiNamNay = child.FKHVNamTruocChuyenNamNay - child.FTongThanhToanVonKeoDaiNamNay - child.FGiaTriNamTruocChuyenNamSau;
                child.FVonConLaiHuyBoNamNay = child.FKHVNamNay - child.FTongKeHoachThanhToanVonNamNay - child.FGiaTriNamNayChuyenNamSau;
                child.FLuyKeTamUngChuaThuHoiChuyenSangNam =
                    child.FTamUngTheoCheDoChuaThuHoiNamTruoc - child.FGiaTriTamUngDieuChinhGiam - child.FTamUngNamTruocThuHoiNamNay
                    + child.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay + child.FTamUngTheoCheDoChuaThuHoiNamNay;
            }
            return _mapper.Map<ObservableCollection<ExportVdtQtBcquyetToanNienDoChiTiet1Model>>(results);
        }

        private ObservableCollection<RptVdtQtBcQuyetToanNienDoPhanTichModel> LoadDataPhanTich(VdtQtBcQuyetToanNienDo objQuyetToan)
        {
            List<VdtQtBcQuyetToanNienDoPhanTichQuery> data = new List<VdtQtBcQuyetToanNienDoPhanTichQuery>();
            List<VdtQtBcQuyetToanNienDoPhanTichQuery> defaultDatas = new List<VdtQtBcQuyetToanNienDoPhanTichQuery>();
            List<RptVdtQtBcQuyetToanNienDoPhanTichModel> lstData = new List<RptVdtQtBcQuyetToanNienDoPhanTichModel>();
            List<RptVdtQtBcQuyetToanNienDoPhanTichModel> results = new List<RptVdtQtBcQuyetToanNienDoPhanTichModel>();
            Dictionary<Guid, List<RptVdtQtBcQuyetToanNienDoPhanTichModel>> dicData = new Dictionary<Guid, List<RptVdtQtBcQuyetToanNienDoPhanTichModel>>();
            defaultDatas = _service.GetBaoCaoQuyetToanNienDoPhanTichById(objQuyetToan.Id).ToList();
            if (defaultDatas != null && defaultDatas.Count != 0)
            {
                lstData.AddRange(_mapper.Map<List<RptVdtQtBcQuyetToanNienDoPhanTichModel>>(defaultDatas));
            }
            else
            {
                data = _service.GetBaoCaoQuyetToanNienDoPhanTich(objQuyetToan.IIdMaDonViQuanLy, objQuyetToan.INamKeHoach.Value, objQuyetToan.IIdNguonVonId.Value).ToList();
                lstData.AddRange(_mapper.Map<List<RptVdtQtBcQuyetToanNienDoPhanTichModel>>(data));
            }
            if (lstData != null)
            {
                dicData = lstData.GroupBy(n => (n.IIdLoaiCongTrinh ?? Guid.Empty)).ToDictionary(n => n.Key, n => n.ToList());
            }
            foreach (var item in lstLoaiCongTrinh.Where(n => !n.IIdParent.HasValue).OrderBy(n => n.SMaLoaiCongTrinh))
            {
                bool bHaveData = false;
                results.AddRange(RecursiveLoaiCongTrinhPhanTich(item, lstLoaiCongTrinh, dicData, ref bHaveData));
            }
            foreach (var item in results.Where(x => x.LoaiParent == 2))
            {
                item.FDuToanCnsChuaGiaiNganTaiKbNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiKbNamTruoc);
                item.FDuToanCnsChuaGiaiNganTaiDvNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiDvNamTruoc);
                item.FDuToanCnsChuaGiaiNganTaiCucNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiCucNamTruoc);
                //item.FTongChuaThuHoi = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongChuaThuHoi);
                //item.FTongDuToanDuocGiaoNamTruocChuyenSang = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongDuToanDuocGiaoNamTruocChuyenSang);
                item.FChiTieuNamNayKb = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FChiTieuNamNayKb);
                item.FChiTieuNamNayLc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FChiTieuNamNayLc);
                //item.FTongDuToanDuocGiao = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongDuToanDuocGiao);
                //item.FTotalDuToanDuocGiao = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTotalDuToanDuocGiao);
                item.FSoCapNamTrcCs = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FSoCapNamTrcCs);
                item.FSoCapNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FSoCapNamNay);
                //item.FTongSoDuocCap = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongSoDuocCap);
                item.FDnQuyetToanNamTrc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDnQuyetToanNamTrc);
                item.FDnQuyetToanNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDnQuyetToanNamNay);
                //item.FTongDeNghiQuyetToan = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongDeNghiQuyetToan);
                //item.FTongChuyenNamSau = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongChuyenNamSau);
                //item.FTongTamUngChuaThuHoi = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongTamUngChuaThuHoi);
                item.FTuChuaThuHoiTaiCuc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTuChuaThuHoiTaiCuc);
                item.FTuChuaThuHoiTaiDonVi = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTuChuaThuHoiTaiDonVi);
                //item.FTongDuChuaGiaiNgan = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongDuChuaGiaiNgan);
                item.FDuToanCnsChuaGiaiNganTaiCuc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiCuc);
                item.FDuToanCnsChuaGiaiNganTaiDv = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiDv);
                item.FDuToanCnsChuaGiaiNganTaiKb = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiKb);
                item.FDuToanThuHoi = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanThuHoi);
            }
            foreach (var item in results.Where(x => x.LoaiParent == 1))
            {
                item.FDuToanCnsChuaGiaiNganTaiKbNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiKbNamTruoc);
                item.FDuToanCnsChuaGiaiNganTaiDvNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiDvNamTruoc);
                item.FDuToanCnsChuaGiaiNganTaiCucNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiCucNamTruoc);
                //item.FTongChuaThuHoi = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongChuaThuHoi);
                //item.FTongDuToanDuocGiaoNamTruocChuyenSang = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongDuToanDuocGiaoNamTruocChuyenSang);
                item.FChiTieuNamNayKb = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FChiTieuNamNayKb);
                item.FChiTieuNamNayLc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FChiTieuNamNayLc);
                //item.FTongDuToanDuocGiao = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongDuToanDuocGiao);
                //item.FTotalDuToanDuocGiao = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTotalDuToanDuocGiao);
                item.FSoCapNamTrcCs = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FSoCapNamTrcCs);
                item.FSoCapNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FSoCapNamNay);
                //item.FTongSoDuocCap = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongSoDuocCap);
                item.FDnQuyetToanNamTrc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDnQuyetToanNamTrc);
                item.FDnQuyetToanNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDnQuyetToanNamNay);
                //item.FTongDeNghiQuyetToan = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongDeNghiQuyetToan);
                //item.FTongChuyenNamSau = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongChuyenNamSau);
                //item.FTongTamUngChuaThuHoi = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongTamUngChuaThuHoi);
                item.FTuChuaThuHoiTaiCuc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTuChuaThuHoiTaiCuc);
                item.FTuChuaThuHoiTaiDonVi = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTuChuaThuHoiTaiDonVi);
                //item.FTongDuChuaGiaiNgan = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongDuChuaGiaiNgan);
                item.FDuToanCnsChuaGiaiNganTaiCuc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiCuc);
                item.FDuToanCnsChuaGiaiNganTaiDv = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiDv);
                item.FDuToanCnsChuaGiaiNganTaiKb = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiKb);
                item.FDuToanThuHoi = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanThuHoi);                
            }
            foreach (var item in results.Where(x => x.LoaiParent == 0 && x.IIdLoaiCongTrinhParent == null))
            {
                item.FDuToanCnsChuaGiaiNganTaiKbNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiKbNamTruoc);
                item.FDuToanCnsChuaGiaiNganTaiDvNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiDvNamTruoc);
                item.FDuToanCnsChuaGiaiNganTaiCucNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiCucNamTruoc);
                //item.FTongChuaThuHoi = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongChuaThuHoi);
                //item.FTongDuToanDuocGiaoNamTruocChuyenSang = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongDuToanDuocGiaoNamTruocChuyenSang);
                item.FChiTieuNamNayKb = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FChiTieuNamNayKb);
                item.FChiTieuNamNayLc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FChiTieuNamNayLc);
                //item.FTongDuToanDuocGiao = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongDuToanDuocGiao);
                //item.FTotalDuToanDuocGiao = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTotalDuToanDuocGiao);
                item.FSoCapNamTrcCs = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FSoCapNamTrcCs);
                item.FSoCapNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FSoCapNamNay);
                //item.FTongSoDuocCap = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongSoDuocCap);
                item.FDnQuyetToanNamTrc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDnQuyetToanNamTrc);
                item.FDnQuyetToanNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDnQuyetToanNamNay);
                //item.FTongDeNghiQuyetToan = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongDeNghiQuyetToan);
                //item.FTongChuyenNamSau = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongChuyenNamSau);
                //item.FTongTamUngChuaThuHoi = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongTamUngChuaThuHoi);
                item.FTuChuaThuHoiTaiCuc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTuChuaThuHoiTaiCuc);
                item.FTuChuaThuHoiTaiDonVi = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTuChuaThuHoiTaiDonVi);
                //item.FTongDuChuaGiaiNgan = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongDuChuaGiaiNgan);
                item.FDuToanCnsChuaGiaiNganTaiCuc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiCuc);
                item.FDuToanCnsChuaGiaiNganTaiDv = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiDv);
                item.FDuToanCnsChuaGiaiNganTaiKb = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanCnsChuaGiaiNganTaiKb);
                item.FDuToanThuHoi = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FDuToanThuHoi);
            }
            return _mapper.Map<ObservableCollection<RptVdtQtBcQuyetToanNienDoPhanTichModel>>(results);
        }

        private List<ExportBcquyetToanNienDoVonUngChiTietModel> GetDataExportQuyetToanVonUng(Guid? iIdQuyetToan)
        {
            List<BcquyetToanNienDoVonUngChiTietQuery> data = new List<BcquyetToanNienDoVonUngChiTietQuery>();
            var defaultData = _service.GetQuyetToanNienDoVonUngByParentId(iIdQuyetToan ?? Guid.Empty);
            if (defaultData == null || defaultData.Count() == 0)
            {
                defaultData = _service.GetDeNghiQuyetToanNienDoVonUngDetail(
                    SelectedDonVi.ValueItem, INamKeHoach ?? 0, int.Parse(SelectedNguonVon.ValueItem));
            }
            var results = SetupViewDataVonUng(defaultData);
            int i = 0;
            double fTiLeDonVi = double.Parse(SelectedDonViTinh.ValueItem);
            foreach (var child in results)
            {
                child.FUngTruocChuaThuHoiNamTruoc /= fTiLeDonVi;
                child.FLuyKeThanhToanNamTruoc /= fTiLeDonVi;
                child.FKeHoachVonDuocKeoDai /= fTiLeDonVi;
                child.FVonKeoDaiDaThanhToanNamNay /= fTiLeDonVi;
                child.FThuHoiVonNamNay /= fTiLeDonVi;
                child.FGiaTriThuHoiTheoGiaiNganThucTe /= fTiLeDonVi;
                child.FKHVUNamNay /= fTiLeDonVi;
                child.FVonDaThanhToanNamNay /= fTiLeDonVi;
                child.FKHVUChuaThuHoiChuyenNamSau /= fTiLeDonVi;
                child.FTongSoVonDaThanhToanThuHoi /= fTiLeDonVi;
            }
            return _mapper.Map<List<ExportBcquyetToanNienDoVonUngChiTietModel>>(results);
        }

        private void LoadLoaiBaoCao()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem() { DisplayItem = PaymentTypeEnum.TypeName.THANH_TOAN, ValueItem = ((int)PaymentTypeEnum.Type.THANH_TOAN).ToString() });
            lstData.Add(new ComboboxItem() { DisplayItem = PaymentTypeEnum.TypeName.TAM_UNG, ValueItem = ((int)PaymentTypeEnum.Type.TAM_UNG).ToString() });
            ItemsLoaiBaoCao = new ObservableCollection<ComboboxItem>(lstData);
            OnPropertyChanged(nameof(ItemsLoaiBaoCao));
        }

        private void LoadDonVi()
        {
            var lstData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => _lstDonViExclude.Contains(n.Loai));

            _dicDonVi = new Dictionary<string, DonVi>();
            List<ComboboxItem> lstItem = new List<ComboboxItem>();
            foreach (var item in lstData)
            {
                if (_dicDonVi.ContainsKey(item.IIDMaDonVi)) continue;
                _dicDonVi.Add(item.IIDMaDonVi, item);
                lstItem.Add(new ComboboxItem() { ValueItem = item.IIDMaDonVi, DisplayItem = string.Format("{0}-{1}", item.IIDMaDonVi, item.TenDonVi), HiddenValue = item.Id.ToString() });
            }
            ItemsDonVi = new ObservableCollection<ComboboxItem>(lstItem);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadNguonVon()
        {
            List<ComboboxItem> lstData = _nguonNganSachService.FindAll()
                .Select(n => new ComboboxItem() { DisplayItem = n.STen, ValueItem = n.IIdMaNguonNganSach.ToString() }).ToList();
            ItemsNguonVon = new ObservableCollection<ComboboxItem>(lstData);
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private void LoadDonViTinh()
        {
            List<ComboboxItem> lstDonViTinh = new List<ComboboxItem>()
            {
                new ComboboxItem(){DisplayItem = DonViTinh.DONG, ValueItem =  DonViTinh.DONG_VALUE},
                new ComboboxItem(){DisplayItem = DonViTinh.NGHIN_DONG, ValueItem = DonViTinh.NGHIN_DONG_VALUE},
                new ComboboxItem(){DisplayItem = DonViTinh.TRIEU_DONG, ValueItem = DonViTinh.TRIEU_VALUE},
                new ComboboxItem(){DisplayItem = DonViTinh.TY_DONG, ValueItem = DonViTinh.TY_VALUE}
            };

            ItemsDonViTinh = new ObservableCollection<ComboboxItem>(lstDonViTinh);
            SelectedDonViTinh = ItemsDonViTinh[0];

            OnPropertyChanged(nameof(ItemsDonViTinh));
        }

        private void LoadDanhMuc()
        {
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private List<BcquyetToanNienDoVonUngChiTietModel> SetupViewDataVonUng(List<BcquyetToanNienDoVonUngChiTietQuery> lstData)
        {
            List<BcquyetToanNienDoVonUngChiTietModel> results = new List<BcquyetToanNienDoVonUngChiTietModel>();
            if (lstData == null) return results;
            List<BcquyetToanNienDoVonUngChiTietModel> dataConvert = _mapper.Map<List<BcquyetToanNienDoVonUngChiTietModel>>(lstData);
            Dictionary<Guid, List<BcquyetToanNienDoVonUngChiTietModel>> dicData = dataConvert
                .GroupBy(n => new { n.BIsChuyenTiep, n.IIDDuAnID, n.SMaDuAn, n.SDiaDiem, n.STenDuAn, n.IIdLoaiCongTrinh })
                .Select(n => new BcquyetToanNienDoVonUngChiTietModel()
                {
                    BIsChuyenTiep = n.Key.BIsChuyenTiep,
                    IIDDuAnID = n.Key.IIDDuAnID,
                    SMaDuAn = n.Key.SMaDuAn,
                    SDiaDiem = n.Key.SDiaDiem,
                    STenDuAn = n.Key.STenDuAn,
                    IIdLoaiCongTrinh = n.Key.IIdLoaiCongTrinh,
                    FUngTruocChuaThuHoiNamTruoc = n.Sum(n => n.FUngTruocChuaThuHoiNamTruoc),
                    FLuyKeThanhToanNamTruoc = n.Sum(n => n.FLuyKeThanhToanNamTruoc),
                    FKeHoachVonDuocKeoDai = n.Sum(n => n.FKeHoachVonDuocKeoDai),
                    FVonKeoDaiDaThanhToanNamNay = n.Sum(n => n.FVonKeoDaiDaThanhToanNamNay),
                    FThuHoiVonNamNay = n.Sum(n => n.FThuHoiVonNamNay),
                    FGiaTriThuHoiTheoGiaiNganThucTe = n.Sum(n => n.FGiaTriThuHoiTheoGiaiNganThucTe),
                    FKHVUNamNay = n.Sum(n => n.FKHVUNamNay),
                    FVonDaThanhToanNamNay = n.Sum(n => n.FVonDaThanhToanNamNay),
                    FKHVUChuaThuHoiChuyenNamSau = n.Sum(n => n.FKHVUChuaThuHoiChuyenNamSau),
                    FTongSoVonDaThanhToanThuHoi = n.Sum(n => n.FTongSoVonDaThanhToanThuHoi)
                }).GroupBy(n => (n.IIdLoaiCongTrinh ?? Guid.Empty)).ToDictionary(n => n.Key, n => n.ToList());
            if (lstLoaiCongTrinh == null) return results;
            foreach (var item in lstLoaiCongTrinh.Where(n => !n.IIdParent.HasValue).OrderBy(n => n.SMaLoaiCongTrinh))
            {
                bool bHaveData = false;
                results.AddRange(RecursiveLoaiCongTrinhVonUng(item, lstLoaiCongTrinh, dicData, ref bHaveData));
            }
            return results;
        }        

        private List<VdtQtBcquyetToanNienDoChiTiet1Model> SetupViewData(List<VdtQtBcquyetToanNienDoChiTiet1Query> lstData, int iIdNguonVonId)
        {
            List<VdtQtBcquyetToanNienDoChiTiet1Model> results = new List<VdtQtBcquyetToanNienDoChiTiet1Model>();
            if (lstData == null) return results;
            List<VdtQtBcquyetToanNienDoChiTiet1Model> dataConvert = _mapper.Map<List<VdtQtBcquyetToanNienDoChiTiet1Model>>(lstData);
            Dictionary<Guid, List<VdtQtBcquyetToanNienDoChiTiet1Model>> dicData = dataConvert
                .GroupBy(n => new { n.IIDDuAnID, n.SMaDuAn, n.SDiaDiem, n.STenDuAn, n.FTongMucDauTu, n.BIsChuyenTiep, n.IIdLoaiCongTrinh, n.STenLoaiCongTrinh, n.SMaLoaiCongTrinh })
                .Select(n => new VdtQtBcquyetToanNienDoChiTiet1Model()
                {                   
                    IIDDuAnID = n.Key.IIDDuAnID,
                    SMaDuAn = n.Key.SMaDuAn,
                    SDiaDiem = n.Key.SDiaDiem,
                    STenDuAn = n.Key.STenDuAn,
                    FTongMucDauTu = n.Key.FTongMucDauTu,
                    BIsChuyenTiep = n.Key.BIsChuyenTiep,
                    IIdLoaiCongTrinh = n.Key.IIdLoaiCongTrinh,
                    STenLoaiCongTrinh = n.Key.STenLoaiCongTrinh,
                    SMaLoaiCongTrinh = n.Key.SMaLoaiCongTrinh,
                    FLuyKeThanhToanNamTruoc = n.Sum(k => k.FLuyKeThanhToanNamTruoc),
                    FTamUngTheoCheDoChuaThuHoiNamTruoc = n.Sum(k => k.FTamUngTheoCheDoChuaThuHoiNamTruoc),
                    FGiaTriTamUngDieuChinhGiam = n.Sum(k => k.FGiaTriTamUngDieuChinhGiam),
                    FTamUngNamTruocThuHoiNamNay = n.Sum(k => k.FTamUngNamTruocThuHoiNamNay),
                    FKHVNamTruocChuyenNamNay = n.Sum(k => k.FKHVNamTruocChuyenNamNay),
                    FTongThanhToanVonKeoDaiNamNay = n.Sum(k => k.FTongThanhToanVonKeoDaiNamNay),
                    FTongThanhToanSuDungVonNamTruoc = n.Sum(k => k.FTongThanhToanSuDungVonNamTruoc),
                    FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay = n.Sum(k => k.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay),
                    FGiaTriNamTruocChuyenNamSau = n.Sum(k => k.FGiaTriNamTruocChuyenNamSau),
                    FVonConLaiHuyBoKeoDaiNamNay = n.Sum(k => k.FVonConLaiHuyBoKeoDaiNamNay),
                    FKHVNamNay = n.Sum(k => k.FKHVNamNay),
                    FTongKeHoachThanhToanVonNamNay = n.Sum(k => k.FTongKeHoachThanhToanVonNamNay),
                    FTongThanhToanSuDungVonNamNay = n.Sum(k => k.FTongThanhToanSuDungVonNamNay),
                    FTamUngTheoCheDoChuaThuHoiNamNay = n.Sum(k => k.FTamUngTheoCheDoChuaThuHoiNamNay),
                    FGiaTriNamNayChuyenNamSau = n.Sum(k => k.FGiaTriNamNayChuyenNamSau),
                    FVonConLaiHuyBoNamNay = n.Sum(k => k.FVonConLaiHuyBoNamNay),
                    FTongVonThanhToanNamNay = n.Sum(k => k.FTongVonThanhToanNamNay),
                    FLuyKeTamUngChuaThuHoiChuyenSangNam = n.Sum(k => k.FLuyKeTamUngChuaThuHoiChuyenSangNam),
                    FLuyKeConDaThanhToanHetNamNay = n.Sum(k => k.FLuyKeConDaThanhToanHetNamNay)
                }).GroupBy(n => (n.IIdLoaiCongTrinh ?? Guid.Empty)).ToDictionary(n => n.Key, n => n.ToList());
            if (lstLoaiCongTrinh == null) return results;
            foreach(var item in lstLoaiCongTrinh.Where(n => !n.IIdParent.HasValue).OrderBy(n => n.SMaLoaiCongTrinh))
            {
                bool bHaveData = false;
                results.AddRange(RecursiveLoaiCongTrinh(item, lstLoaiCongTrinh, dicData, ref bHaveData));
            }          
            foreach(var item in results.Where(x => x.LoaiParent == 2))
            {
                item.FTongMucDauTu = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongMucDauTu);
                item.FLuyKeThanhToanNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FLuyKeThanhToanNamTruoc);
                item.FTamUngTheoCheDoChuaThuHoiNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTamUngTheoCheDoChuaThuHoiNamTruoc);
                item.FGiaTriTamUngDieuChinhGiam = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FGiaTriTamUngDieuChinhGiam);
                item.FTamUngNamTruocThuHoiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTamUngNamTruocThuHoiNamNay);
                item.FKHVNamTruocChuyenNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FKHVNamTruocChuyenNamNay);
                item.FTongThanhToanVonKeoDaiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongThanhToanVonKeoDaiNamNay);
                item.FTongThanhToanSuDungVonNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongThanhToanSuDungVonNamTruoc);
                item.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay);
                item.FGiaTriNamTruocChuyenNamSau = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FGiaTriNamTruocChuyenNamSau);
                item.FVonConLaiHuyBoKeoDaiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FVonConLaiHuyBoKeoDaiNamNay);
                item.FKHVNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FKHVNamNay);
                item.FTongKeHoachThanhToanVonNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongKeHoachThanhToanVonNamNay);
                item.FTongThanhToanSuDungVonNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongThanhToanSuDungVonNamNay);
                item.FTamUngTheoCheDoChuaThuHoiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTamUngTheoCheDoChuaThuHoiNamNay);
                item.FGiaTriNamNayChuyenNamSau = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FGiaTriNamNayChuyenNamSau);
                item.FVonConLaiHuyBoNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FVonConLaiHuyBoNamNay);
                item.FTongVonThanhToanNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongVonThanhToanNamNay);
                item.FLuyKeTamUngChuaThuHoiChuyenSangNam = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FLuyKeTamUngChuaThuHoiChuyenSangNam);
                item.FLuyKeConDaThanhToanHetNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FLuyKeConDaThanhToanHetNamNay);               
            }
            foreach (var item in results.Where(x => x.LoaiParent == 1))
                {
                item.FTongMucDauTu = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongMucDauTu);
                item.FLuyKeThanhToanNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FLuyKeThanhToanNamTruoc);
                item.FTamUngTheoCheDoChuaThuHoiNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTamUngTheoCheDoChuaThuHoiNamTruoc);
                item.FGiaTriTamUngDieuChinhGiam = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FGiaTriTamUngDieuChinhGiam);
                item.FTamUngNamTruocThuHoiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTamUngNamTruocThuHoiNamNay);
                item.FKHVNamTruocChuyenNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FKHVNamTruocChuyenNamNay);
                item.FTongThanhToanVonKeoDaiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongThanhToanVonKeoDaiNamNay);
                item.FTongThanhToanSuDungVonNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongThanhToanSuDungVonNamTruoc);
                item.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay);
                item.FGiaTriNamTruocChuyenNamSau = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FGiaTriNamTruocChuyenNamSau);
                item.FVonConLaiHuyBoKeoDaiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FVonConLaiHuyBoKeoDaiNamNay);
                item.FKHVNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FKHVNamNay);
                item.FTongKeHoachThanhToanVonNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongKeHoachThanhToanVonNamNay);
                item.FTongThanhToanSuDungVonNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongThanhToanSuDungVonNamNay);
                item.FTamUngTheoCheDoChuaThuHoiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTamUngTheoCheDoChuaThuHoiNamNay);
                item.FGiaTriNamNayChuyenNamSau = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FGiaTriNamNayChuyenNamSau);
                item.FVonConLaiHuyBoNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FVonConLaiHuyBoNamNay);
                item.FTongVonThanhToanNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongVonThanhToanNamNay);
                item.FLuyKeTamUngChuaThuHoiChuyenSangNam = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FLuyKeTamUngChuaThuHoiChuyenSangNam);
                item.FLuyKeConDaThanhToanHetNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FLuyKeConDaThanhToanHetNamNay);
            }
            foreach (var item in results.Where(x => x.LoaiParent == 0 && x.IIdLoaiCongTrinhParent == null))
            {
                item.FTongMucDauTu = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongMucDauTu);
                item.FLuyKeThanhToanNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FLuyKeThanhToanNamTruoc);
                item.FTamUngTheoCheDoChuaThuHoiNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTamUngTheoCheDoChuaThuHoiNamTruoc);
                item.FGiaTriTamUngDieuChinhGiam = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FGiaTriTamUngDieuChinhGiam);
                item.FTamUngNamTruocThuHoiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTamUngNamTruocThuHoiNamNay);
                item.FKHVNamTruocChuyenNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FKHVNamTruocChuyenNamNay);
                item.FTongThanhToanVonKeoDaiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongThanhToanVonKeoDaiNamNay);
                item.FTongThanhToanSuDungVonNamTruoc = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongThanhToanSuDungVonNamTruoc);
                item.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay);
                item.FGiaTriNamTruocChuyenNamSau = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FGiaTriNamTruocChuyenNamSau);
                item.FVonConLaiHuyBoKeoDaiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FVonConLaiHuyBoKeoDaiNamNay);
                item.FKHVNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FKHVNamNay);
                item.FTongKeHoachThanhToanVonNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongKeHoachThanhToanVonNamNay);
                item.FTongThanhToanSuDungVonNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongThanhToanSuDungVonNamNay);
                item.FTamUngTheoCheDoChuaThuHoiNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTamUngTheoCheDoChuaThuHoiNamNay);
                item.FGiaTriNamNayChuyenNamSau = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FGiaTriNamNayChuyenNamSau);
                item.FVonConLaiHuyBoNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FVonConLaiHuyBoNamNay);
                item.FTongVonThanhToanNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FTongVonThanhToanNamNay);
                item.FLuyKeTamUngChuaThuHoiChuyenSangNam = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FLuyKeTamUngChuaThuHoiChuyenSangNam);
                item.FLuyKeConDaThanhToanHetNamNay = results.Where(x => x.IIdLoaiCongTrinhParent == item.IIdLoaiCongTrinh).Sum(x => x.FLuyKeConDaThanhToanHetNamNay);
            }
            //for(int i = 0; i < results.Where(x => x.LoaiParent == 0 && x.IIdLoaiCongTrinhParent == null).ToList().Count; i++)
            //{
            //    results.Where(x => x.LoaiParent == 0 && x.IIdLoaiCongTrinhParent == null).ToList()[i].iStt = "A." + (i + 1).ToString();            
            //}            
            return results;
        }

        private List<VdtQtBcquyetToanNienDoChiTiet1Model> RecursiveLoaiCongTrinh(VdtDmLoaiCongTrinh current, List<VdtDmLoaiCongTrinh> lstLoaiCongTrinh, 
            Dictionary<Guid, List<VdtQtBcquyetToanNienDoChiTiet1Model>> dicData, ref bool bHaveData)
        {
            List<VdtQtBcquyetToanNienDoChiTiet1Model> results = new List<VdtQtBcquyetToanNienDoChiTiet1Model>();
            bool bIsAddParent = false;
            var currentData = new VdtQtBcquyetToanNienDoChiTiet1Model()
            {
                iStt = current.SMaLoaiCongTrinh,
                STenDuAn = current.STenLoaiCongTrinh,
                IIdLoaiCongTrinh = current.IIdLoaiCongTrinh,
                IsHangCha = true
                
            };
            if (dicData.ContainsKey(current.IIdLoaiCongTrinh))
            {
                results.Add(currentData);
                var lstChuyenTiep = dicData[current.IIdLoaiCongTrinh].Where(n => n.BIsChuyenTiep);
                var lstMoMoi = dicData[current.IIdLoaiCongTrinh].Where(n => !n.BIsChuyenTiep);
                if(lstMoMoi != null && lstMoMoi.Count() != 0)
                {
                    var duAnMoMoi = new VdtQtBcquyetToanNienDoChiTiet1Model()
                    {
                        IIdLoaiCongTrinh = Guid.NewGuid(),
                        LoaiParent = 2,
                        STenDuAn = DU_AN_MO_MOI,
                        iStt = "a",
                        IsHangCha = false,                        
                        IIdLoaiCongTrinhParent = currentData.IIdLoaiCongTrinh
                    };
                    results.Add(duAnMoMoi);
                    foreach(var item in lstMoMoi)
                    {
                        item.IIdLoaiCongTrinhParent = duAnMoMoi.IIdLoaiCongTrinh;
                    }
                    results.AddRange(lstMoMoi);
                }
                if (lstChuyenTiep != null && lstChuyenTiep.Count() != 0)
                {
                    var duAnChuyenTiep = new VdtQtBcquyetToanNienDoChiTiet1Model()
                    {
                        IIdLoaiCongTrinh = Guid.NewGuid(),
                        LoaiParent = 2,
                        STenDuAn = DU_AN_CHUYEN_TIEP,
                        iStt = "b",
                        IsHangCha = false,                        
                        IIdLoaiCongTrinhParent = currentData.IIdLoaiCongTrinh
                    };
                    results.Add(duAnChuyenTiep);
                    foreach (var item in lstChuyenTiep)
                    {
                        item.IIdLoaiCongTrinhParent = duAnChuyenTiep.IIdLoaiCongTrinh;
                    }
                    results.AddRange(lstChuyenTiep);
                }
                bHaveData = true;
                bIsAddParent = true;
            }
            foreach (var item in lstLoaiCongTrinh.Where(n=>n.IIdParent.HasValue && n.IIdParent == current.IIdLoaiCongTrinh).OrderBy(n=>n.SMaLoaiCongTrinh))
            {
                bool bChildData = false;
                var lstChild = RecursiveLoaiCongTrinh(item, lstLoaiCongTrinh, dicData, ref bChildData);
                if (bChildData)
                {
                    if (!bIsAddParent)
                    {                        
                        results.Add(currentData);
                        bIsAddParent = true;
                    }
                    foreach(var itemChild in lstChild.Where(x => x.IIdLoaiCongTrinhParent == null))
                    {
                        itemChild.IIdLoaiCongTrinhParent = currentData.IIdLoaiCongTrinh;
                        itemChild.LoaiParent = 1;
                    }
                    results.AddRange(lstChild);
                    bHaveData = true;
                }
            }
            return results;
        }

        private List<RptVdtQtBcQuyetToanNienDoPhanTichModel> RecursiveLoaiCongTrinhPhanTich(VdtDmLoaiCongTrinh current, List<VdtDmLoaiCongTrinh> lstLoaiCongTrinh,
            Dictionary<Guid, List<RptVdtQtBcQuyetToanNienDoPhanTichModel>> dicData, ref bool bHaveData)
        {
            List<RptVdtQtBcQuyetToanNienDoPhanTichModel> results = new List<RptVdtQtBcQuyetToanNienDoPhanTichModel>();
            bool bIsAddParent = false;
            var currentData = new RptVdtQtBcQuyetToanNienDoPhanTichModel()
            {
                SSoThuTu = current.SMaLoaiCongTrinh,
                STenDuAn = current.STenLoaiCongTrinh,
                IsHangCha = true,
                IIdLoaiCongTrinh = current.IIdLoaiCongTrinh,

            };
            if (dicData.ContainsKey(current.IIdLoaiCongTrinh))
            {
                results.Add(currentData);
                var lstChuyenTiep = dicData[current.IIdLoaiCongTrinh].Where(n => n.BIsChuyenTiep);
                var lstMoMoi = dicData[current.IIdLoaiCongTrinh].Where(n => !n.BIsChuyenTiep);
                if (lstMoMoi != null && lstMoMoi.Count() != 0)
                {
                    //results.Add(new RptVdtQtBcQuyetToanNienDoPhanTichModel()
                    //{
                    //    STenDuAn = DU_AN_MO_MOI,
                    //    SSoThuTu = "I",
                    //    IsHangCha = true
                    //});
                    //results.AddRange(lstMoMoi);
                    var duAnMoMoi = new RptVdtQtBcQuyetToanNienDoPhanTichModel()
                    {
                        IIdLoaiCongTrinh = Guid.NewGuid(),
                        LoaiParent = 2,
                        STenDuAn = DU_AN_MO_MOI,
                        SSoThuTu = "a",
                        IsHangCha = false,
                        IIdLoaiCongTrinhParent = currentData.IIdLoaiCongTrinh
                    };
                    results.Add(duAnMoMoi);
                    foreach (var item in lstMoMoi)
                    {
                        item.IIdLoaiCongTrinhParent = duAnMoMoi.IIdLoaiCongTrinh;
                    }
                    results.AddRange(lstMoMoi);
                }
                if (lstChuyenTiep != null && lstChuyenTiep.Count() != 0)
                {
                    //results.Add(new RptVdtQtBcQuyetToanNienDoPhanTichModel()
                    //{
                    //    STenDuAn = DU_AN_CHUYEN_TIEP,
                    //    SSoThuTu = "II",
                    //    IsHangCha = true
                    //});
                    //results.AddRange(lstChuyenTiep);
                    var duAnChuyenTiep = new RptVdtQtBcQuyetToanNienDoPhanTichModel()
                    {
                        IIdLoaiCongTrinh = Guid.NewGuid(),
                        LoaiParent = 2,
                        STenDuAn = DU_AN_CHUYEN_TIEP,
                        SSoThuTu = "b",
                        IsHangCha = false,
                        IIdLoaiCongTrinhParent = currentData.IIdLoaiCongTrinh
                    };
                    results.Add(duAnChuyenTiep);
                    foreach (var item in lstChuyenTiep)
                    {
                        item.IIdLoaiCongTrinhParent = duAnChuyenTiep.IIdLoaiCongTrinh;
                    }
                    results.AddRange(lstChuyenTiep);
                }
                bHaveData = true;
                bIsAddParent = true;
            }
            foreach (var item in lstLoaiCongTrinh.Where(n => n.IIdParent.HasValue && n.IIdParent == current.IIdLoaiCongTrinh).OrderBy(n => n.SMaLoaiCongTrinh))
            {
                bool bChildData = false;
                var lstChild = RecursiveLoaiCongTrinhPhanTich(item, lstLoaiCongTrinh, dicData, ref bChildData);
                if (bChildData)
                {
                    if (!bIsAddParent)
                    {
                        results.Add(currentData);
                        bIsAddParent = true;
                    }
                    foreach (var itemChild in lstChild.Where(x => x.IIdLoaiCongTrinhParent == null))
                    {
                        itemChild.IIdLoaiCongTrinhParent = currentData.IIdLoaiCongTrinh;
                        itemChild.LoaiParent = 1;
                    }
                    results.AddRange(lstChild);
                    bHaveData = true;
                }                
            }
            return results;
        }

        private List<BcquyetToanNienDoVonUngChiTietModel> RecursiveLoaiCongTrinhVonUng(VdtDmLoaiCongTrinh current, List<VdtDmLoaiCongTrinh> lstLoaiCongTrinh,
            Dictionary<Guid, List<BcquyetToanNienDoVonUngChiTietModel>> dicData, ref bool bHaveData)
        {
            List<BcquyetToanNienDoVonUngChiTietModel> results = new List<BcquyetToanNienDoVonUngChiTietModel>();
            bool bIsAddParent = false;
            var currentData = new BcquyetToanNienDoVonUngChiTietModel()
            {
                iStt = current.SMaLoaiCongTrinh,
                STenDuAn = current.STenLoaiCongTrinh,
                IsHangCha = true

            };
            if (dicData.ContainsKey(current.IIdLoaiCongTrinh))
            {
                results.Add(currentData);
                var lstChuyenTiep = dicData[current.IIdLoaiCongTrinh].Where(n => n.BIsChuyenTiep);
                var lstMoMoi = dicData[current.IIdLoaiCongTrinh].Where(n => !n.BIsChuyenTiep);
                if (lstMoMoi != null && lstMoMoi.Count() != 0)
                {
                    results.Add(new BcquyetToanNienDoVonUngChiTietModel()
                    {
                        STenDuAn = DU_AN_MO_MOI,
                        iStt = "I",
                        IsHangCha = true
                    });
                    results.AddRange(lstMoMoi);
                }
                if (lstChuyenTiep != null && lstChuyenTiep.Count() != 0)
                {
                    results.Add(new BcquyetToanNienDoVonUngChiTietModel()
                    {
                        STenDuAn = DU_AN_CHUYEN_TIEP,
                        iStt = "II",
                        IsHangCha = true
                    });
                    results.AddRange(lstChuyenTiep);
                }
                bHaveData = true;
                bIsAddParent = true;
            }
            foreach (var item in lstLoaiCongTrinh.Where(n => n.IIdParent.HasValue && n.IIdParent == current.IIdLoaiCongTrinh).OrderBy(n => n.SMaLoaiCongTrinh))
            {
                bool bChildData = false;
                var lstChild = RecursiveLoaiCongTrinhVonUng(item, lstLoaiCongTrinh, dicData, ref bChildData);
                if (bChildData)
                {
                    if (!bIsAddParent)
                    {
                        results.Add(currentData);
                        bIsAddParent = true;
                    }
                    results.AddRange(lstChild);
                    bHaveData = true;
                }
            }
            return results;
        }
        #endregion
    }
}
