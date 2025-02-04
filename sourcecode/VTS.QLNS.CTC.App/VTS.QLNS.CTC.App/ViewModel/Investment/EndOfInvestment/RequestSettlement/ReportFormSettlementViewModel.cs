using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.AnnualSettlement;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.RequestSettlement;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Category;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.RequestSettlement
{
    public class ReportFormSettlementViewModel : DialogViewModelBase<DeNghiQuyetToanModel>
    {
        private INsDonViService _nsDonViService;
        private readonly IMapper _mapper;
        private ISessionService _sessionService;
        private IVdtDaDuAnService _vdtDaDuAnService;
        private IExportService _exportService;
        private readonly ILog _logger;
        private IVdtQtQuyetToanService _vdtQtQuyetToanService;
        private IVdtDeNghiQuyetToanService _iVdtDeNghiQuyetToanService;
        private readonly IDanhMucService _danhMucService;
        private IVdtDaDuToanService _duToanService;
        private IApproveProjectService _quyetDinhDauTuService;
        private readonly IVdtQtDeNghiQuyetToanNguonVonService _vdtQtDeNghiQuyetToanNguonVonService;
        private IVdtQtDeNghiQuyetToanChiTietService _qtDeNghiQuyetToanChiTietService;
        private readonly IVdtDaDuAnService _duAnService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private IDmChuKyService _dmChuKyService;
        private DmChuKy _dmChuKy;
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string Name => "Báo cáo tổng hợp quyết toán";
        public override string Title => "Báo cáo tổng hợp quyết toán";
        public override string Description => "Báo cáo tổng hợp quyết toán";
        public override PackIconKind IconKind => PackIconKind.ClipboardFileOutline;
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.RequestSettlement.ReportFormSettlement);

        private List<DeNghiQuyetToanModel> _listDeNghi;
        public List<DeNghiQuyetToanModel> ListDeNghi
        {
            get => _listDeNghi;
            set => SetProperty(ref _listDeNghi, value);
        }

        private string _tieuDe;
        public string TieuDe
        {
            get => _tieuDe;
            set => SetProperty(ref _tieuDe, value);
        }

        private string _diaDiem;
        public string DiaDiem
        {
            get => _diaDiem;
            set => SetProperty(ref _diaDiem, value);
        }

        private string _kinhGui;
        public string KinhGui
        {
            get => _kinhGui;
            set => SetProperty(ref _kinhGui, value);
        }

        private DateTime? _ngayBaoCao;
        public DateTime? NgayBaoCao
        {
            get => _ngayBaoCao;
            set => SetProperty(ref _ngayBaoCao, value);
        }

        private string _tinhHinhThucHienDuAn;
        public string TinhHinhThucHienDuAn
        {
            get => _tinhHinhThucHienDuAn;
            set => SetProperty(ref _tinhHinhThucHienDuAn, value);
        }

        private string _nhanXetDanhGia;
        public string NhanXetDanhGia
        {
            get => _nhanXetDanhGia;
            set => SetProperty(ref _nhanXetDanhGia, value);
        }

        private string _kienNghi;
        public string KienNghi
        {
            get => _kienNghi;
            set => SetProperty(ref _kienNghi, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiBaoCao;
        public ObservableCollection<ComboboxItem> DataLoaiBaoCao
        {
            get => _dataLoaiBaoCao;
            set => SetProperty(ref _dataLoaiBaoCao, value);
        }

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set
            {
                SetProperty(ref _selectedLoaiBaoCao, value);
                if (_selectedLoaiBaoCao != null)
                {
                    //LoadTieuDe();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _DataDonViTinh;
        public ObservableCollection<ComboboxItem> DataDonViTinh
        {
            get => _DataDonViTinh;
            set => SetProperty(ref _DataDonViTinh, value);
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }

        private ObservableCollection<ComboboxItem> _dataDeNghi;
        public ObservableCollection<ComboboxItem> DataDeNghi
        {
            get => _dataDeNghi;
            set => SetProperty(ref _dataDeNghi, value);
        }

        private ComboboxItem _selectedDeNghi;
        public ComboboxItem SelectedDeNghi
        {
            get => _selectedDeNghi;
            set => SetProperty(ref _selectedDeNghi, value);
        }

        public RelayCommand ExportCommand { get; set; }
        public RelayCommand PrintPDFCommand { get; set; }
        public RelayCommand ConfigSignCommand { get; }

        public ReportFormSettlementViewModel(INsDonViService nsDonViService,
                                            ISessionService sessionService,
                                            IVdtDaDuAnService vdtDaDuAnService,
                                            IExportService exportService,
                                            IVdtQtQuyetToanService vdtQtQuyetToanService,
                                            ILog logger,
                                            IDmChuKyService dmChuKyService,
                                            DmChuKyDialogViewModel dmChuKyDialogViewModel,
                                            IDanhMucService danhMucService,
                                            IVdtDaDuToanService duToanService,
                                            IApproveProjectService quyetDinhDauTuService,
                                            IVdtDeNghiQuyetToanService iVdtDeNghiQuyetToanService,
                                            IVdtQtDeNghiQuyetToanNguonVonService vdtQtDeNghiQuyetToanNguonVonService,
                                            IVdtQtDeNghiQuyetToanChiTietService qtDeNghiQuyetToanChiTietService,
                                            IVdtDaDuAnService duAnService,
                                            IDmChuDauTuService dmChuDauTuService,
                                            IMapper mapper)
        {
            _sessionService = sessionService;
            _vdtDaDuAnService = vdtDaDuAnService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _vdtQtQuyetToanService = vdtQtQuyetToanService;
            _danhMucService = danhMucService;
            _iVdtDeNghiQuyetToanService = iVdtDeNghiQuyetToanService;
            _duToanService = duToanService;
            _quyetDinhDauTuService = quyetDinhDauTuService;
            _vdtQtDeNghiQuyetToanNguonVonService = vdtQtDeNghiQuyetToanNguonVonService;
            _qtDeNghiQuyetToanChiTietService = qtDeNghiQuyetToanChiTietService;
            _dmChuKyService = dmChuKyService;
            _dmChuDauTuService = dmChuDauTuService;
            _duAnService = duAnService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _logger = logger;
            _mapper = mapper;

            ExportCommand = new RelayCommand(o => OnExport(ExportType.EXCEL));
            PrintPDFCommand = new RelayCommand(o => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public double GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return double.Parse(SelectedDonViTinh.ValueItem);
        }

        private void OnExport(ExportType exportType)
        {
            if (DataDeNghi == null || SelectedDeNghi == null)
            {
                return;
            }
            DeNghiQuyetToanModel deNghiItem = ListDeNghi.Where(n => n.Id.ToString() == SelectedDeNghi.ValueItem).FirstOrDefault();
            if (deNghiItem == null)
            {
                return;
            }
            if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_TO_TRINH)
            {
                ExportToTrinh(deNghiItem, exportType);
            }
            else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC_TONGQUAT)
            {
                ExportPhuLucTongQuat(deNghiItem, exportType);
            }
            else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC_CHITIET)
            {
                ExportPhuLucChiTiet(deNghiItem, exportType);
            }
        }

        public List<DeNghiQuyetToanChiTietModel> LoadHangMuc(DeNghiQuyetToanChiTietModel chiphi, VdtQtDeNghiQuyetToan deNghiQuyetToanModel)
        {
            List<DuToanDetailQuery> listData = new List<DuToanDetailQuery>();
            //Lúc ban đầu sẽ lấy hạng mục ở phê duyệt dự án => check tồn tại Dutoan trong bang DuToanHangMuc
            List<VdtDaDuToan> duToan = _duToanService.FindListByDuAnId(deNghiQuyetToanModel.IIdDuAnId.Value);
            if (duToan == null || duToan.Count == 0)
            {
                return new List<DeNghiQuyetToanChiTietModel>();
            }
            bool checkExitsDuToanHangMuc = _duToanService.CheckExistInDuToanHangMuc(string.Join(",", duToan.Select(n => n.Id).ToList()), chiphi.ChiPhiId);
            if (checkExitsDuToanHangMuc)
            {
                listData = _duToanService.FindListDetail(string.Join(",", duToan.Select(n => n.Id).ToList()), chiphi.ChiPhiId).ToList();
            }
            else
            {
                VdtDaQddauTu qdDauTu = _quyetDinhDauTuService.FindByDuAnId(deNghiQuyetToanModel.IIdDuAnId.Value);
                if (qdDauTu != null)
                {
                    listData = _duToanService.ListHangMucInitial(qdDauTu.Id, chiphi.ChiPhiId).ToList();
                }
            }
            listData.Select(n => { n.Id = Guid.NewGuid(); n.MaOrDer = chiphi.MaOrderDb + "_" + n.MaOrDer; return n; }).ToList();
            List<DeNghiQuyetToanChiTietModel> listResult = _mapper.Map<List<Model.DeNghiQuyetToanChiTietModel>>(listData);
            listResult.Select(n => { n.ChiPhiIdParentOfHangMuc = chiphi.ChiPhiId; return n; }).ToList();
            return listResult;
        }

        /// <summary>
        /// thêm hạng mục con level 1 vào danh sách hiển thị trên báo cáo, như khi ấn vào hiển thị cấp con trong trang chi tiết
        /// </summary>
        /// <param name="list"></param>
        private List<DeNghiQuyetToanChiTietModel> AddHangMucCuaChiPhiVaoDanhSachHienThiTrenReport(List<DeNghiQuyetToanChiTietModel> list)
        {
            List<DeNghiQuyetToanChiTietModel> listModified = new List<DeNghiQuyetToanChiTietModel>();
            list.ForEach(n =>
            {
                n.IsHangCha = true;
                listModified.Add(n);
                //Click chi phi
                if(n.ListHangMuc != null && n.ListHangMuc.Count() > 0)
                {
                    DeNghiQuyetToanChiTietModel item = n.ListHangMuc.OrderBy(n => n.MaOrDer).FirstOrDefault();
                    //
                    List<DeNghiQuyetToanChiTietModel> listHangMucInsert = n.ListHangMuc.Where(n => n.MaOrDer.Length == item.MaOrDer.Length).ToList();
                    if(listHangMucInsert != null && listHangMucInsert.Count() > 0)
                    {
                        foreach (DeNghiQuyetToanChiTietModel itemInsert in listHangMucInsert)
                        {                    
                            if (!list.Contains(itemInsert))
                            {                        
                                n.FGiaTriAB = 0;
                                n.FGiaTriKiemToan = 0;
                                n.FGiaTriDeNghiQuyetToan = 0;
                                //if (itemInsert.IsHangCha)
                                //{
                                itemInsert.IdChiPhiDuAnParent = n.ChiPhiId;
                                itemInsert.GoiThauId = Guid.Empty;
                                //}
                                itemInsert.IsHangCha = false;
                                listModified.Add(itemInsert);
                            }
                        }
                    }
                }
            });
            return listModified;
        }
        private void ExportPhuLucTongQuat(DeNghiQuyetToanModel deNghiItem, ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDTG_TONG_HOP_QUYETTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    VdtQtDeNghiQuyetToan deNghiQuyetToanModel = _iVdtDeNghiQuyetToanService.Find(deNghiItem.Id);
                    List<VdtDaDuToanChiPhiDataQuery> listDuToanChiPhi = _qtDeNghiQuyetToanChiTietService.FindListDuToanChiPhiByDuAnNew(deNghiQuyetToanModel.iID_QuyetDinh.Value);
                    string sTenDuAn = _vdtDaDuAnService.FindById(deNghiQuyetToanModel.IIdDuAnId.Value).STenDuAn;
                    listDuToanChiPhi.OrderBy(n => n.IThuTu);
                    List<DeNghiQuyetToanChiTietModel> listDeNghiQuyetToan = _mapper.Map<List<Model.DeNghiQuyetToanChiTietModel>>(listDuToanChiPhi);
                    int thutu = 1;
                    foreach (var item in listDeNghiQuyetToan)
                    {
                        item.MaOrderDb = thutu++.ToString();
                    }
                    listDeNghiQuyetToan.Where(n => n.PhanCap == 1).Select(n => { n.IsShow = true; return n; }).ToList();
                    listDeNghiQuyetToan.Select(n => { n.IsChiPhi = true; return n; }).ToList();
                    List<VdtQtDeNghiQuyetToanChiTiet> listDbData = _qtDeNghiQuyetToanChiTietService.FindByDeNghiQuyetToanId(deNghiItem.Id);
                    List<DeNghiQuyetToanChiTietModel> listHangMucShow = new List<DeNghiQuyetToanChiTietModel>();
                    foreach (DeNghiQuyetToanChiTietModel item in listDeNghiQuyetToan)
                    {
                        item.ListHangMuc = LoadHangMuc(item, deNghiQuyetToanModel);
                        if (listDbData != null && listDbData.Count > 0)
                        {
                            VdtQtDeNghiQuyetToanChiTiet entityCP = listDbData.Where(n => n.IIdChiPhiId == item.ChiPhiId).FirstOrDefault();
                            if (entityCP != null)
                            {
                                item.IsShow = true;
                                item.FGiaTriKiemToan = entityCP.FGiaTriKiemToan.HasValue ? entityCP.FGiaTriKiemToan.Value : 0;
                                item.FGiaTriDeNghiQuyetToan = entityCP.FGiaTriDeNghiQuyetToan.HasValue ? entityCP.FGiaTriDeNghiQuyetToan.Value : 0;
                            }
                            foreach (DeNghiQuyetToanChiTietModel dataItem in item.ListHangMuc)
                            {
                                VdtQtDeNghiQuyetToanChiTiet entity = listDbData.Where(n => n.IIdHangMucId == dataItem.HangMucId).FirstOrDefault();
                                if (entity != null && entity.SMaOrder.Length < 4)
                                {
                                    dataItem.IsShow = true;
                                    dataItem.IdChiPhiDuAnParent = item.ChiPhiId;
                                    dataItem.MaOrderDb = entity.SMaOrder;
                                    dataItem.FGiaTriKiemToan = entity.FGiaTriKiemToan.HasValue ? entity.FGiaTriKiemToan.Value : 0;
                                    dataItem.FGiaTriDeNghiQuyetToan = entity.FGiaTriDeNghiQuyetToan.HasValue ? entity.FGiaTriDeNghiQuyetToan.Value : 0;
                                    listHangMucShow.Add(dataItem);
                                }
                                
                            }
                        }
                    }

                    listDeNghiQuyetToan.AddRange(listHangMucShow);
                    listDeNghiQuyetToan.Where(n => n.FGiaTriKiemToan != 0 || n.FGiaTriDeNghiQuyetToan != 0).Select(n => { n.IsShow = true; return n; }).ToList();
                    listDeNghiQuyetToan = listDeNghiQuyetToan.Where(n => n.IsShow).OrderBy(n => n.MaOrderDb).ToList();
                    double donViTinh = GetDonViTinh();

                    //listDeNghiQuyetToan = AddHangMucCuaChiPhiVaoDanhSachHienThiTrenReport(listDeNghiQuyetToan);
                    //CreateMaOrderItem(ref listDeNghiQuyetToan);
                    listDeNghiQuyetToan.Select(n =>
                    {
                        //if (n.IsChiPhi)
                        //{
                        //    n.Stt = (listDeNghiQuyetToan.IndexOf(n) + 1).ToString();
                        //}
                        //else
                        //{
                        //    DeNghiQuyetToanChiTietModel cp = listDeNghiQuyetToan.Find(x => x.ChiPhiId == n.ChiPhiIdParentOfHangMuc);
                        //    int cpIndex = listDeNghiQuyetToan.FindIndex(x => x.ChiPhiId == cp.ChiPhiId);
                        //    int hmIndex = cp.ListHangMuc.FindIndex(h => h.HangMucId == n.HangMucId)+1;
                        //    n.Stt = (cpIndex+1).ToString() + "_" + hmIndex.ToString();
                        //}
                        n.Stt = n.MaOrderDb;
                        n.GiaTriPheDuyetQDDT = n.GiaTriPheDuyetQDDT.HasValue ? n.GiaTriPheDuyetQDDT.Value / donViTinh : 0;
                        n.GiaTriPheDuyet = n.GiaTriPheDuyet.HasValue ? n.GiaTriPheDuyet.Value / donViTinh : 0;
                        n.FGiaTriKiemToan = n.FGiaTriKiemToan / donViTinh;
                        n.FGiaTriDeNghiQuyetToan = n.FGiaTriDeNghiQuyetToan / donViTinh;
                        n.FGiaTriAB = n.FGiaTriAB / donViTinh;
                        return n;
                    }).ToList();
                    //CheckHangCha(ref listDeNghiQuyetToan);
                    string CapTren = NSConstants.BO_QUOC_PHONG;
                    VdtQtDeNghiQuyetToan vdtDeNghiQT = _iVdtDeNghiQuyetToanService.Find(deNghiItem.Id);
                    if (vdtDeNghiQT.IIdDuAnId.HasValue)
                    {
                        VdtDaDuAn duan = _duAnService.FindById(vdtDeNghiQT.IIdDuAnId.Value);
                        if (duan != null && duan.IIdChuDauTuId.HasValue)
                        {
                            DmChuDauTu dmChuDauTu = _dmChuDauTuService.FindById(duan.IIdChuDauTuId.Value);
                            if (dmChuDauTu != null)
                            {
                                int namLamViec = dmChuDauTu.INamLamViec.HasValue ? dmChuDauTu.INamLamViec.Value :
                                    DateTime.Now.Year;
                                DonVi donvi = _nsDonViService.FindByIdDonVi(dmChuDauTu.IIDMaDonVi, namLamViec);
                                if (!"0".Equals(donvi?.Loai))
                                {
                                    DonVi donViCapTren = _nsDonViService.FindByLoai("0", namLamViec);
                                    CapTren = donViCapTren?.TenDonVi;
                                }
                            }
                        }
                    }

                    FormatNumber formatNumber = new FormatNumber((int)donViTinh, exportType);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("CapTren", CapTren);
                    data.Add("ChuDauTu", deNghiItem.TenChuDauTu);
                    data.Add("TenDuAn", sTenDuAn);
                    data.Add("DonViTinh", SelectedDonViTinh.DisplayItem);
                    data.Add("FormatNumber", formatNumber);
                    if (deNghiItem.IdLoaiQuyetToan == LOAI_QUYETTOAN_DUAN_HOANTHANH.TypeName.THEO_GOITHAU)
                        listDeNghiQuyetToan = new List<DeNghiQuyetToanChiTietModel>();
                    foreach (var item in listDeNghiQuyetToan)
                    {
                        item.Stt = item.Stt.Replace('_', '.');
                        if (item.HangMucId != Guid.Empty) item.IsHangCha = false;
                        if (item.Stt == "1") item.Stt = "I";
                        if (item.Stt == "2") item.Stt = "II";
                        if (item.Stt == "3") item.Stt = "III";
                        if (item.Stt == "4") item.Stt = "IV";
                        if (item.Stt == "5") item.Stt = "V";
                        if (item.Stt == "6") item.Stt = "VI";
                        if (item.Stt == "7") item.Stt = "VII";
                    }
                    data.Add("Items", listDeNghiQuyetToan);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KTDT, ExportFileName.RPT_VDT_TONGHOPQUYETTOANDUANHOANTHANH_PHULUC);
                    string fileNamePrefix = "rptQuyetToanHoanThanhPhuLuc";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<DeNghiQuyetToanChiTietModel>(templateFileName, data);
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
        private void ExportPhuLucChiTiet(DeNghiQuyetToanModel deNghiItem, ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDTG_TONG_HOP_QUYETTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    VdtQtDeNghiQuyetToan deNghiQuyetToanModel = _iVdtDeNghiQuyetToanService.Find(deNghiItem.Id);
                    List<VdtDaDuToanChiPhiDataQuery> listDuToanChiPhi = _qtDeNghiQuyetToanChiTietService.FindListDuToanChiPhiByDuAnNew(deNghiQuyetToanModel.iID_QuyetDinh.Value);
                    string sTenDuAn = _vdtDaDuAnService.FindById(deNghiQuyetToanModel.IIdDuAnId.Value).STenDuAn;
                    listDuToanChiPhi.OrderBy(n => n.IThuTu);
                    List<DeNghiQuyetToanChiTietModel> listDeNghiQuyetToan = _mapper.Map<List<Model.DeNghiQuyetToanChiTietModel>>(listDuToanChiPhi);
                    int thutu = 1;
                    foreach (var item in listDeNghiQuyetToan)
                    {
                        item.MaOrderDb = thutu++.ToString();
                    }
                    listDeNghiQuyetToan.Where(n => n.PhanCap == 1).Select(n => { n.IsShow = true; return n; }).ToList();
                    listDeNghiQuyetToan.Select(n => { n.IsChiPhi = true; return n; }).ToList();
                    List<VdtQtDeNghiQuyetToanChiTiet> listDbData = _qtDeNghiQuyetToanChiTietService.FindByDeNghiQuyetToanId(deNghiItem.Id);
                    List<DeNghiQuyetToanChiTietModel> listHangMucShow = new List<DeNghiQuyetToanChiTietModel>();
                    foreach (DeNghiQuyetToanChiTietModel item in listDeNghiQuyetToan)
                    {
                        item.ListHangMuc = LoadHangMuc(item, deNghiQuyetToanModel);
                        if (listDbData != null && listDbData.Count > 0)
                        {
                            VdtQtDeNghiQuyetToanChiTiet entityCP = listDbData.Where(n => n.IIdChiPhiId == item.ChiPhiId).FirstOrDefault();
                            if (entityCP != null)
                            {
                                item.IsShow = true;
                                item.FGiaTriKiemToan = entityCP.FGiaTriKiemToan.HasValue ? entityCP.FGiaTriKiemToan.Value : 0;
                                item.FGiaTriDeNghiQuyetToan = entityCP.FGiaTriDeNghiQuyetToan.HasValue ? entityCP.FGiaTriDeNghiQuyetToan.Value : 0;
                            }
                            foreach (DeNghiQuyetToanChiTietModel dataItem in item.ListHangMuc)
                            {
                                VdtQtDeNghiQuyetToanChiTiet entity = listDbData.Where(n => n.IIdHangMucId == dataItem.HangMucId).FirstOrDefault();
                                if (entity != null)
                                {
                                    dataItem.IsShow = true;
                                    dataItem.IdChiPhiDuAnParent = item.ChiPhiId;
                                    dataItem.MaOrderDb = entity.SMaOrder;                                    
                                    dataItem.FGiaTriKiemToan = entity.FGiaTriKiemToan.HasValue ? entity.FGiaTriKiemToan.Value : 0;
                                    dataItem.FGiaTriDeNghiQuyetToan = entity.FGiaTriDeNghiQuyetToan.HasValue ? entity.FGiaTriDeNghiQuyetToan.Value : 0;
                                }
                                listHangMucShow.Add(dataItem);
                            }
                        }
                    }

                    listDeNghiQuyetToan.AddRange(listHangMucShow);
                    listDeNghiQuyetToan.Where(n => n.FGiaTriKiemToan != 0 || n.FGiaTriDeNghiQuyetToan != 0).Select(n => { n.IsShow = true; return n; }).ToList();
                    listDeNghiQuyetToan = listDeNghiQuyetToan.Where(n => n.IsShow).OrderBy(n => n.MaOrderDb).ToList();
                    double donViTinh = GetDonViTinh();

                    //listDeNghiQuyetToan = AddHangMucCuaChiPhiVaoDanhSachHienThiTrenReport(listDeNghiQuyetToan);
                    //CreateMaOrderItem(ref listDeNghiQuyetToan);
                    listDeNghiQuyetToan.Select(n =>
                    {
                        //if (n.IsChiPhi)
                        //{
                        //    n.Stt = (listDeNghiQuyetToan.IndexOf(n) + 1).ToString();
                        //}
                        //else
                        //{
                        //    DeNghiQuyetToanChiTietModel cp = listDeNghiQuyetToan.Find(x => x.ChiPhiId == n.ChiPhiIdParentOfHangMuc);
                        //    int cpIndex = listDeNghiQuyetToan.FindIndex(x => x.ChiPhiId == cp.ChiPhiId);
                        //    int hmIndex = cp.ListHangMuc.FindIndex(h => h.HangMucId == n.HangMucId)+1;
                        //    n.Stt = (cpIndex+1).ToString() + "_" + hmIndex.ToString();
                        //}
                        n.Stt = n.MaOrderDb;
                        n.GiaTriPheDuyetQDDT = n.GiaTriPheDuyetQDDT.HasValue ? n.GiaTriPheDuyetQDDT.Value / donViTinh : 0;
                        n.GiaTriPheDuyet = n.GiaTriPheDuyet.HasValue ? n.GiaTriPheDuyet.Value / donViTinh : 0;
                        n.FGiaTriKiemToan = n.FGiaTriKiemToan / donViTinh;
                        n.FGiaTriDeNghiQuyetToan = n.FGiaTriDeNghiQuyetToan / donViTinh;
                        n.FGiaTriAB = n.FGiaTriAB / donViTinh;
                        return n;
                    }).ToList();
                    //CheckHangCha(ref listDeNghiQuyetToan);
                    string CapTren = NSConstants.BO_QUOC_PHONG;
                    VdtQtDeNghiQuyetToan vdtDeNghiQT = _iVdtDeNghiQuyetToanService.Find(deNghiItem.Id);
                    if (vdtDeNghiQT.IIdDuAnId.HasValue)
                    {
                        VdtDaDuAn duan = _duAnService.FindById(vdtDeNghiQT.IIdDuAnId.Value);
                        if (duan != null && duan.IIdChuDauTuId.HasValue)
                        {
                            DmChuDauTu dmChuDauTu = _dmChuDauTuService.FindById(duan.IIdChuDauTuId.Value);
                            if (dmChuDauTu != null)
                            {
                                int namLamViec = dmChuDauTu.INamLamViec.HasValue ? dmChuDauTu.INamLamViec.Value :
                                    DateTime.Now.Year;
                                DonVi donvi = _nsDonViService.FindByIdDonVi(dmChuDauTu.IIDMaDonVi, namLamViec);
                                if (!"0".Equals(donvi?.Loai))
                                {
                                    DonVi donViCapTren = _nsDonViService.FindByLoai("0", namLamViec);
                                    CapTren = donViCapTren?.TenDonVi;
                                }
                            }
                        }
                    }

                    FormatNumber formatNumber = new FormatNumber((int)donViTinh, exportType);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("CapTren", CapTren);
                    data.Add("ChuDauTu", deNghiItem.TenChuDauTu);
                    data.Add("TenDuAn", sTenDuAn);
                    data.Add("DonViTinh", SelectedDonViTinh.DisplayItem);
                    data.Add("FormatNumber", formatNumber);
                    if (deNghiItem.IdLoaiQuyetToan == LOAI_QUYETTOAN_DUAN_HOANTHANH.TypeName.THEO_GOITHAU)
                        listDeNghiQuyetToan = new List<DeNghiQuyetToanChiTietModel>();
                    foreach (var item in listDeNghiQuyetToan)
                    {
                        item.Stt = item.Stt.Replace('_', '.');
                        if (item.HangMucId != Guid.Empty) item.IsHangCha = false;
                        if (item.Stt == "1") item.Stt = "I";
                        if (item.Stt == "2") item.Stt = "II";
                        if (item.Stt == "3") item.Stt = "III";
                        if (item.Stt == "4") item.Stt = "IV";
                        if (item.Stt == "5") item.Stt = "V";
                        if (item.Stt == "6") item.Stt = "VI";
                        if (item.Stt == "7") item.Stt = "VII";
                    }
                    data.Add("Items", listDeNghiQuyetToan);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KTDT, ExportFileName.RPT_VDT_TONGHOPQUYETTOANDUANHOANTHANH_PHULUC);
                    string fileNamePrefix = "rptQuyetToanHoanThanhPhuLuc";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<DeNghiQuyetToanChiTietModel>(templateFileName, data);
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

        public void CreateMaOrderItem(ref List<DeNghiQuyetToanChiTietModel> listDeNghiQuyetToan)
        {
            if (listDeNghiQuyetToan == null || listDeNghiQuyetToan.Count == 0)
                return;
            DeNghiQuyetToanChiTietModel root = listDeNghiQuyetToan.Where(n => n.IsChiPhi && n.IdChiPhiDuAnParent == Guid.Empty && n.PhanCap == 1).FirstOrDefault();

            if (root != null)
            {
                root.MaOrderDb = "1";
                CreateMaOrderItemChild(root, ref listDeNghiQuyetToan);
            }
        }

        public void CheckHangCha(ref List<DeNghiQuyetToanChiTietModel> listDeNghiQuyetToan)
        {
            foreach (DeNghiQuyetToanChiTietModel item in listDeNghiQuyetToan)
            {
                if (item.IsChiPhi)
                {
                    DeNghiQuyetToanChiTietModel child = listDeNghiQuyetToan.Where(n => n.IdChiPhiDuAnParent == item.ChiPhiId).FirstOrDefault();
                    if (child != null)
                    {
                        item.IsHangCha = true;
                    }
                    else
                    {
                        item.IsHangCha = false;
                    }
                    if (item.ListHangMuc != null && item.ListHangMuc.Count > 0)
                    {
                        foreach (DeNghiQuyetToanChiTietModel hangMucChild in item.ListHangMuc)
                        {
                            DeNghiQuyetToanChiTietModel checkItem = listDeNghiQuyetToan.Where(n => n.HangMucId == hangMucChild.HangMucId).FirstOrDefault();
                            if (checkItem != null)
                            {
                                item.IsHangCha = true;
                            }
                        }
                    }
                }
                else
                {
                    DeNghiQuyetToanChiTietModel child = listDeNghiQuyetToan.Where(n => n.IdHangMucParent == item.HangMucId).FirstOrDefault();
                    if (child != null)
                    {
                        item.IsHangCha = true;
                    }
                    else
                    {
                        item.IsHangCha = false;
                    }
                }
            }
        }

        public void CreateMaOrderItemChild(DeNghiQuyetToanChiTietModel parent, ref List<DeNghiQuyetToanChiTietModel> listDeNghiQuyetToan)
        {
            List<DeNghiQuyetToanChiTietModel> listChild = listDeNghiQuyetToan.Where(n => n.IdChiPhiDuAnParent == parent.ChiPhiId).ToList();
            if (listChild == null || listChild.Count == 0)
            {
                return;
            }
            for (int i = 0; i < listChild.Count; i++)
            {
                listChild[i].MaOrderDb = parent.MaOrderDb + "_" + (i + 1).ToString();
                CreateMaOrderItemChild(listChild[i], ref listDeNghiQuyetToan);
            }
        }

        private void ExportToTrinh(DeNghiQuyetToanModel deNghiItem, ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDTG_TONG_HOP_QUYETTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    VdtQtDeNghiQuyetToan entity = _iVdtDeNghiQuyetToanService.Find(deNghiItem.Id);
                    List<ReportTongHopQuyetToanDuAnHoanThanhModel> result = new List<ReportTongHopQuyetToanDuAnHoanThanhModel>();
                    List<ReportTongHopQuyetToanDuAnHoanThanhModel> chiPhi = new List<ReportTongHopQuyetToanDuAnHoanThanhModel>();
                    VdtDaQddauTu quyetDinhDauTu = new VdtDaQddauTu();
                    double donViTinh = GetDonViTinh();
                    double giaTriDuToan = 0;
                    string CapTren = NSConstants.BO_QUOC_PHONG;
                    if (entity.IIdDuAnId.HasValue)
                    {
                        quyetDinhDauTu = _quyetDinhDauTuService.FindByDuAnId(entity.IIdDuAnId.Value);
                        giaTriDuToan = _duToanService.GetGiaTriDuToanIdByDuAnId(entity.IIdDuAnId.Value);
                        result = GetDataNguonVonByDuAnId(entity.IIdDuAnId.Value);
                        result.Select(n => { n.Stt = (result.IndexOf(n) + 1).ToString(); return n; }).ToList();

                        chiPhi = GetDataChiPhi(entity.IIdDuAnId.Value, deNghiItem);
                        chiPhi.Select(n => { n.Stt = (chiPhi.IndexOf(n) + 1).ToString(); return n; }).ToList();

                        VdtDaDuAn duan = _duAnService.FindById(entity.IIdDuAnId.Value);
                        if (duan != null && duan.IIdChuDauTuId.HasValue)
                        {
                            DmChuDauTu dmChuDauTu = _dmChuDauTuService.FindById(duan.IIdChuDauTuId.Value);
                            if (dmChuDauTu != null)
                            {
                                int namLamViec = dmChuDauTu.INamLamViec.HasValue ? dmChuDauTu.INamLamViec.Value :
                                    DateTime.Now.Year;
                                DonVi donvi = _nsDonViService.FindByIdDonVi(dmChuDauTu.IIDMaDonVi, namLamViec);
                                if (!"0".Equals(donvi?.Loai))
                                {
                                    DonVi donViCapTren = _nsDonViService.FindByLoai("0", namLamViec);
                                    CapTren = donViCapTren?.TenDonVi;
                                }
                            }
                        }
                    }
                    result.Insert(0, new ReportTongHopQuyetToanDuAnHoanThanhModel
                    {
                        NoiDung = "Tổng số",
                        DieuChinhCuoi = result.Sum(n => n.DieuChinhCuoi),
                        KeHoach = result.Sum(n => n.KeHoach),
                        DaThanhToan = result.Sum(n => n.DaThanhToan),
                        IsHangCha = true
                    });

                    chiPhi.Insert(0, new ReportTongHopQuyetToanDuAnHoanThanhModel
                    {
                        NoiDung = "Tổng số",
                        DieuChinhCuoi = chiPhi.Sum(n => n.DieuChinhCuoi),
                        KeHoach = chiPhi.Sum(n => n.KeHoach),
                        DaThanhToan = chiPhi.Sum(n => n.DaThanhToan),
                        IsHangCha = true
                    });
                    result.Select(n => { n.DieuChinhCuoi = n.DieuChinhCuoi / donViTinh; n.KeHoach = n.KeHoach / donViTinh; n.DaThanhToan = n.DaThanhToan / donViTinh; return n; }).ToList();
                    chiPhi.Select(n => { n.DieuChinhCuoi = n.DieuChinhCuoi / donViTinh; n.KeHoach = n.KeHoach / donViTinh; n.DaThanhToan = n.DaThanhToan / donViTinh; return n; }).ToList();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("CapTren", CapTren);
                    data.Add("ChuDauTu", deNghiItem.TenChuDauTu);
                    FormatNumber formatNumber = new FormatNumber((int)donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TieuDe", TieuDe);
                    data.Add("TenDuAn", deNghiItem.TenDuAn);
                    data.Add("DuyetDieuChinhLanCuoi", (quyetDinhDauTu != null && quyetDinhDauTu.Id != Guid.Empty && quyetDinhDauTu.FTongMucDauTuPheDuyet.HasValue) ? quyetDinhDauTu.FTongMucDauTuPheDuyet / donViTinh : 0);
                    data.Add("DuToanDuocDuyetCuoi", giaTriDuToan / donViTinh);
                    data.Add("DonViTinh", SelectedDonViTinh.DisplayItem);
                    data.Add("Items", result);
                    if (deNghiItem.IdLoaiQuyetToan == LOAI_QUYETTOAN_DUAN_HOANTHANH.TypeName.THEO_GOITHAU)
                        chiPhi = new List<ReportTongHopQuyetToanDuAnHoanThanhModel>();
                    data.Add("MLNS", chiPhi);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                    data.Add("NguyenNhanBatKhaKhang", entity.FChiPhiThietHai.HasValue ? entity.FChiPhiThietHai.Value / Convert.ToDouble(SelectedDonViTinh.ValueItem) : 0);
                    data.Add("ChiPhiKhongTaoTaiSan", entity.FChiPhiKhongTaoNenTaiSan.HasValue ? entity.FChiPhiKhongTaoNenTaiSan.Value / Convert.ToDouble(SelectedDonViTinh.ValueItem) : 0);
                    data.Add("TaiSanDaiHan", (entity.FTaiSanDaiHanDonViKhacQuanLy.HasValue ? entity.FTaiSanDaiHanDonViKhacQuanLy.Value / Convert.ToDouble(SelectedDonViTinh.ValueItem) : 0) +
                        (entity.FTaiSanDaiHanThuocCDTQuanLy.HasValue ? entity.FTaiSanDaiHanThuocCDTQuanLy.Value / Convert.ToDouble(SelectedDonViTinh.ValueItem) : 0)
                        );
                    data.Add("TaiSanNganHan", (entity.FTaiSanNganHanDonViKhacQuanLy.HasValue ? entity.FTaiSanNganHanDonViKhacQuanLy.Value / Convert.ToDouble(SelectedDonViTinh.ValueItem) : 0) +
                        (entity.FTaiSanNganHanThuocCDTQuanLy.HasValue ? entity.FTaiSanNganHanThuocCDTQuanLy.Value / Convert.ToDouble(SelectedDonViTinh.ValueItem) : 0)
                        );
                    data.Add("TinhHinhThucHienDuAn", TinhHinhThucHienDuAn);
                    data.Add("NhanXetDanhGia", NhanXetDanhGia);
                    data.Add("KienNghi", KienNghi);
                    data.Add("Kinhgui", KinhGui);
                    data.Add("Diadiem", DiaDiem);
                    data.Add("Ngay", NgayBaoCao.HasValue ? NgayBaoCao.Value.Day.ToString() : "...");
                    data.Add("Thang", NgayBaoCao.HasValue ? NgayBaoCao.Value.Month.ToString() : "...");
                    data.Add("Nam", NgayBaoCao.HasValue ? NgayBaoCao.Value.Year.ToString() : "...");

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KTDT, ExportFileName.RPT_VDT_TONGHOPQUYETTOANDUANHOANTHANH);
                    string fileNamePrefix = "rptQuyetToanHoanThanh";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportTongHopQuyetToanDuAnHoanThanhModel, ReportTongHopQuyetToanDuAnHoanThanhModel>(templateFileName, data);
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

        private List<ReportTongHopQuyetToanDuAnHoanThanhModel> GetDataNguonVonByDuAnId(Guid duanId)
        {
            // string duToanId = _duToanService.GetDuToanIdByDuAnId(duanId);
            IEnumerable<ApproveProjectQuery> listQDDT = _quyetDinhDauTuService.FindListQDDauTuByDuAnId(duanId);
            string qddtId = string.Join(",", listQDDT.Select(q => q.Id));
            if (string.IsNullOrEmpty(qddtId))
            {
                return new List<ReportTongHopQuyetToanDuAnHoanThanhModel>();
            }
            List<NguonVonQuyetToanKeHoachQuery> listDuToanNguonVonQuery = _vdtQtDeNghiQuyetToanNguonVonService.GetNguonVonByQDDTId(qddtId).ToList();
            List<ReportTongHopQuyetToanDuAnHoanThanhModel> listResult = new List<ReportTongHopQuyetToanDuAnHoanThanhModel>();
            foreach (NguonVonQuyetToanKeHoachQuery item in listDuToanNguonVonQuery)
            {
                listResult.Add(new ReportTongHopQuyetToanDuAnHoanThanhModel
                {
                    DieuChinhCuoi = item.GiaTriPheDuyet,
                    NoiDung = item.TenNguonVon,
                    KeHoach = item.fCapPhatBangLenhChi + item.fCapPhatTaiKhoBac,
                    DaThanhToan = item.DaThanhToan
                });
            }
            return listResult;
        }

        private List<ReportTongHopQuyetToanDuAnHoanThanhModel> GetDataChiPhi(Guid duanId, DeNghiQuyetToanModel deNghi)
        {
            string duToanId = _duToanService.GetDuToanIdByDuAnId(duanId);
            if (string.IsNullOrEmpty(duToanId))
            {
                return new List<ReportTongHopQuyetToanDuAnHoanThanhModel>();
            }
            VdtQtDeNghiQuyetToan deNghiQuyetToanModel = _iVdtDeNghiQuyetToanService.Find(deNghi.Id);
            List<VdtDaDuToanChiPhiDataQuery> listDuToanChiPhi = _qtDeNghiQuyetToanChiTietService.FindListDuToanChiPhiByDuAnNew(deNghiQuyetToanModel.iID_QuyetDinh.Value);
            List<DeNghiQuyetToanChiTietModel> listDeNghiQuyetToan = _mapper.Map<List<Model.DeNghiQuyetToanChiTietModel>>(listDuToanChiPhi);
            List<VdtQtDeNghiQuyetToanChiTiet> ListDbData = _qtDeNghiQuyetToanChiTietService.FindByDeNghiQuyetToanId(deNghi.Id);
            if (ListDbData != null && ListDbData.Count > 0)
            {
                foreach (DeNghiQuyetToanChiTietModel data in listDeNghiQuyetToan)
                {
                    VdtQtDeNghiQuyetToanChiTiet entity = ListDbData.Where(n => n.IIdChiPhiId == data.ChiPhiId).FirstOrDefault();
                    if (entity != null)
                    {
                        data.FGiaTriKiemToan = entity.FGiaTriKiemToan.HasValue ? entity.FGiaTriKiemToan.Value : 0;
                        data.FGiaTriDeNghiQuyetToan = entity.FGiaTriDeNghiQuyetToan.HasValue ? entity.FGiaTriDeNghiQuyetToan.Value : 0;
                    }
                }
            }

            List<ReportTongHopQuyetToanDuAnHoanThanhModel> listResult = new List<ReportTongHopQuyetToanDuAnHoanThanhModel>();
            foreach (DeNghiQuyetToanChiTietModel item in listDeNghiQuyetToan)
            {
                listResult.Add(new ReportTongHopQuyetToanDuAnHoanThanhModel
                {
                    DieuChinhCuoi = item.GiaTriPheDuyet.HasValue ? item.GiaTriPheDuyet.Value : 0,
                    KeHoach = item.FGiaTriDeNghiQuyetToan,
                    DaThanhToan = (item.GiaTriPheDuyet.HasValue ? item.GiaTriPheDuyet.Value : 0) - item.FGiaTriDeNghiQuyetToan,
                    NoiDung = item.TenChiPhi
                });
            }
            return listResult;
        }

        private void LoadDeNghi()
        {
            DataDeNghi = new ObservableCollection<ComboboxItem>();
            foreach (DeNghiQuyetToanModel item in ListDeNghi)
            {
                DataDeNghi.Add(new ComboboxItem { DisplayItem = item.SoBaoCao, ValueItem = item.Id.ToString() });
            }
            if (DataDeNghi != null && DataDeNghi.Count > 0)
            {
                SelectedDeNghi = DataDeNghi.FirstOrDefault();
            }
            OnPropertyChanged(nameof(DataDeNghi));
            OnPropertyChanged(nameof(SelectedDeNghi));
        }

        private void LoadDonViTinh()
        {
            DataDonViTinh = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionService.Current.YearOfWork).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (var dvt in listDonViTinh)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();
        }

        private void LoadLoaiBaoCao()
        {
            DataLoaiBaoCao = new ObservableCollection<ComboboxItem>();
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_TO_TRINH, DisplayItem = LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_TO_TRINH });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC_TONGQUAT, DisplayItem = LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC_TONGQUAT });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC_CHITIET, DisplayItem = LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC_CHITIET });
            SelectedLoaiBaoCao = DataLoaiBaoCao.FirstOrDefault();
        }

        private void LoadTieuDe()
        {
            if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_TO_TRINH)
            {
                TieuDe = "Tổng hợp quyết toán dự án hoàn thành";
            }
            else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC_CHITIET || SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC_TONGQUAT)
            {
                TieuDe = "Chi phí đầu tư đề nghị quyết toán";
            }
            OnPropertyChanged(nameof(TieuDe));
        }

        private void LoadData()
        {
            try
            {
                LoadLoaiBaoCao();
                LoadDeNghi();
                LoadDonViTinh();
                //LoadTieuDe();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void Init()
        {
            try
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDTG_TONG_HOP_QUYETTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                TieuDe = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : "Tổng hợp quyết toán dự án hoàn thành";
                LoadData();
                OnPropertyChanged(nameof(TieuDe));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDTG_TONG_HOP_QUYETTOAN) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_VDTG_TONG_HOP_QUYETTOAN;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TieuDe = chuKy.TieuDe1MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
