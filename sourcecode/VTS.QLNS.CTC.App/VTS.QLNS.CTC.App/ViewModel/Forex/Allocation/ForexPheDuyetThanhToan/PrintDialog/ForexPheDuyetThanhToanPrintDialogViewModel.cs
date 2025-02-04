using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexAllocation.ForexPheDuyetThanhToan.PrintDialog
{
    public class ForexPheDuyetThanhToanPrintDialogViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INhTtThanhToanService _iNhTtThanhToanService;
        private readonly INhTtThanhToanChiTietService _iNhTtThanhToanChiTietService;
        private readonly IExportService _exportService;
        private readonly DmChuKyDialogViewModel _dmChuKyDialogViewModel;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private readonly IMucLucNganSachService _iMucLucNganSachService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly string _typeChuky = TypeChuKy.RPT_NH_CAPPHAT_PHEDUYETTHANHTOAN;
        private string _diaDiem;
        private readonly string TITLE_FIRST_DEFAULT_VALUE = "PHẦN GHI CỦA CƠ QUAN TÀI CHÍNH";
        private readonly string TITLE_SECOND_DEFAULT_VALUE = "";
        public override string Title => "Phần ghi của cơ quan tài chính (thanh toán, tạm ứng)";
        public override string Description => "Phần ghi của cơ quan tài chính (thanh toán, tạm ứng)";

        private string _txtTitleFirst;
        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set
            {
                SetProperty(ref _txtTitleFirst, value);
            }
        }

        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
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

        public NhTtThanhToanModel nhDeNghiThanhToanModels { get; set; }

        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand ExportCommand { get; }

        public ForexPheDuyetThanhToanPrintDialogViewModel(ISessionService sessionService,
            IVdtTtPheDuyetThanhToanChiTietService pheDuyetThanhToanChiTietService,
            IExportService exportService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            INhTtThanhToanService NhThanhToanService,
            INhTtThanhToanChiTietService NhThanhToanChiTietService,
            IDmChuKyService dmChuKyService,
            IDanhMucService danhMucService,
            IMucLucNganSachService mucLucNganSachService,
            IMapper mapper,
            ILog logger)
        {
            _sessionService = sessionService;
            _iNhTtThanhToanService = NhThanhToanService;
            _iNhTtThanhToanChiTietService = NhThanhToanChiTietService;
            _exportService = exportService;
            _dmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _dmChuKyService = dmChuKyService;
            _danhMucService = danhMucService;
            _iMucLucNganSachService = mucLucNganSachService;

            _mapper = mapper;
            _logger = logger;
            PrintReportCommand = new RelayCommand(obj => OnPrintReport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            ExportCommand = new RelayCommand(obj => OnPrintReport(ExportType.EXCEL));
        }

        public override void Init()
        {
            LoadCatUnitTypes();
            LoadTitleFirst();
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private List<VdtTtPheDuyetThanhToanChiTiet> GetListDataToPrintWithDonViTinh(List<VdtTtPheDuyetThanhToanChiTiet> listData)
        {
            List<VdtTtPheDuyetThanhToanChiTiet> listWithDVT = listData.Where(n => (!string.IsNullOrEmpty(n.M) || !string.IsNullOrEmpty(n.Tm) || !string.IsNullOrEmpty(n.Ttm) || !string.IsNullOrEmpty(n.Ng))).ToList();
            listWithDVT.ForEach(data =>
            {
                data.TongSoWithDonViTinh = data.TongSo / Int32.Parse(CatUnitTypeSelected.ValueItem);
                data.FGiaTriThanhToanTn = data.FGiaTriThanhToanTn.HasValue ? data.FGiaTriThanhToanTn / Int32.Parse(CatUnitTypeSelected.ValueItem) : data.FGiaTriThanhToanTn; // có giá trị thì chia k thì kệ
                data.FGiaTriThanhToanNn = data.FGiaTriThanhToanNn.HasValue ? data.FGiaTriThanhToanNn / Int32.Parse(CatUnitTypeSelected.ValueItem) : data.FGiaTriThanhToanNn; // có giá trị thì chia k thì kệ
            });
            return listWithDVT;
        }

        private void OnPrintReport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);


                    Dictionary<string, object> data = new Dictionary<string, object>();
                    List<BCPheDuyetThanhToanModel> lstChiTietBC = new List<BCPheDuyetThanhToanModel>();
                    List<BCPheDuyetThanhToanModel> lstM = new List<BCPheDuyetThanhToanModel>(); 

                    //Thông tin chi tiết
                    var lstThanhToanChiTiet = _iNhTtThanhToanChiTietService.FindByIdThanhToan(nhDeNghiThanhToanModels.Id);
                    var lstMucLucNganSach = _iMucLucNganSachService.GetAll();

                    var query = from tt_ct in lstThanhToanChiTiet
                                join ns in lstMucLucNganSach on tt_ct.IIdMucLucNganSachId equals ns.Id
                                select new { ns.M, ns.Tm, ns.Ttm, tt_ct.STenNoiDungChi, tt_ct.FPheDuyetCapKyNayUsd, tt_ct.FPheDuyetCapKyNayVnd };

                    if (query.Count() > 0)
                    {
                        foreach (var sm in query)
                        {
                            var checkM = lstChiTietBC.Where(x => x.sTen.Trim() == sm.M && x.IdParent == null).FirstOrDefault();
                            if (checkM == null)
                            {
                                //Add Mục
                                Guid? Id_M = Guid.NewGuid();
                                lstChiTietBC.Add(new BCPheDuyetThanhToanModel { sTen = sm.M, sNoiDung = "", fGiaTriUSD = null, fGiaTriVND = null, Id = Id_M, IdParent = null, Level = 1});
                                //Add Tiểu mục
                                var lstTM = query.Where(x => x.M == sm.M).ToList();
                                foreach(var stm in lstTM)
                                {
                                    Guid? Id_TM = Guid.NewGuid();
                                    var checkTm = lstChiTietBC.Where(x => x.sTen.Trim() == sm.Tm && x.IdParent == Id_M).FirstOrDefault();
                                    if(checkTm == null)
                                    {
                                        lstChiTietBC.Add(new BCPheDuyetThanhToanModel { sTen = " "+ sm.Tm, sNoiDung = "", fGiaTriUSD = null, fGiaTriVND = null, IdParent = Id_M, Id = Id_TM, Level = 2 });
                                        //Add Tiểu tiết Mục
                                        var lstTTM = query.Where(x => x.M == sm.M && x.Tm == stm.Tm).ToList();
                                        Guid? Id_TTM = Guid.NewGuid();
                                        foreach (var sttm in lstTTM)
                                        {
                                            lstChiTietBC.Add(new BCPheDuyetThanhToanModel { sTen = "  "+ sttm.Ttm, sNoiDung = sttm.STenNoiDungChi, fGiaTriUSD = sttm.FPheDuyetCapKyNayUsd, fGiaTriVND = sttm.FPheDuyetCapKyNayVnd, IdParent = Id_TM, Id = Id_TTM, Level = 3 });
                                        }
                                    }    
                                   
                                }    

                            }

                        }
                    }
                    //Tính tổng tiết mục
                    foreach(var tm in lstChiTietBC.Where(x=>x.Level == 2).ToList())
                    {
                        tm.fGiaTriUSD = lstChiTietBC.Where(x => x.IdParent == tm.Id).Sum(x => x.fGiaTriUSD);
                        tm.fGiaTriVND = lstChiTietBC.Where(x => x.IdParent == tm.Id).Sum(x => x.fGiaTriVND);
                    }
                    //Tính tổng mục
                    foreach (var tm in lstChiTietBC.Where(x => x.Level == 1).ToList())
                    {
                        tm.fGiaTriUSD = lstChiTietBC.Where(x => x.IdParent == tm.Id).Sum(x => x.fGiaTriUSD);
                        tm.fGiaTriVND = lstChiTietBC.Where(x => x.IdParent == tm.Id).Sum(x => x.fGiaTriVND);
                    }
                    //Thông tin chung
                    string sNgayDeNghi = nhDeNghiThanhToanModels.DNgayDeNghi.HasValue ? nhDeNghiThanhToanModels.DNgayDeNghi.Value.ToString("dd/MM/yyyy") : String.Empty;
                    int? iLoaiDeNghi = nhDeNghiThanhToanModels.ILoaiDeNghi;
                    string sCapKinhPhi = "Cấp kinh phí";
                    string sThanhToan = "Thanh toán";
                    string sTamUng = "Tạm ứng";
                   
                    string sDonViTinh;
                    if(donViTinh == 1)
                    {
                        sDonViTinh = "VND";
                    }
                    else
                    {
                        sDonViTinh = "USD";
                    }
                    string sNguonVon;
                    if(nhDeNghiThanhToanModels.ILoaiNoiDungChi == 1)
                    {
                        sNguonVon = "Vốn tín dụng";
                    }
                    else
                    {
                        sNguonVon = "Vốn trong nước";
                    }
                    double? fThuHoiTamUng_USD = null;
                    double? fThuHoiTamUng_VND = null;
                    double? fSoTraDonViHuongThu_USD = null;
                    double? fSoTraDonViHuongThu_VND = null;
                    double? fSoTuChoiThanhToan_USD = null;
                    double? fSoTuChoiThanhToan_VND = null;
                    string sLyDoTuChoi = nhDeNghiThanhToanModels.SLyDoTuChoi;
                    string sGhiChu = String.IsNullOrEmpty(nhDeNghiThanhToanModels.SGhiChu) ? "" : nhDeNghiThanhToanModels.SGhiChu.Trim();
                    double? fTongSoUSD = lstThanhToanChiTiet.Sum(x => x.FPheDuyetCapKyNayUsd);
                    double? fTongSoVND = lstThanhToanChiTiet.Sum(x => x.FPheDuyetCapKyNayVnd);
                    string sSoTraDonViHuongThu_VND = "";

                    if(nhDeNghiThanhToanModels.ILoaiNoiDungChi == 1)
                    {
                        fThuHoiTamUng_USD = nhDeNghiThanhToanModels.FThuHoiTamUngBangSo;
                        fSoTraDonViHuongThu_USD = nhDeNghiThanhToanModels.FTraDonViThuHuongBangSo;
                        fSoTuChoiThanhToan_USD = nhDeNghiThanhToanModels.FTuChoiThanhToanBangSo;
                    }
                    else
                    {
                        fThuHoiTamUng_VND = nhDeNghiThanhToanModels.FThuHoiTamUngBangSo;
                        fSoTraDonViHuongThu_VND = nhDeNghiThanhToanModels.FTraDonViThuHuongBangSo;
                        sSoTraDonViHuongThu_VND = nhDeNghiThanhToanModels.FTraDonViThuHuongBangChu;
                        fSoTuChoiThanhToan_VND = nhDeNghiThanhToanModels.FTuChoiThanhToanBangSo;
                    }    

                    //Add data
                    data.Add("sNgayDeNghi", sNgayDeNghi);
                    data.Add("sCapKinhPhi", sCapKinhPhi);
                    data.Add("sTamUng", sTamUng);
                    data.Add("sThanhToan", sThanhToan);
                    data.Add("sNguonVon", sNguonVon);
                    data.Add("sDonViTinh", sDonViTinh);
                    data.Add("fTongSoUSD", fTongSoUSD);
                    data.Add("fTongSoVND", fTongSoVND);
                    data.Add("fThuHoiTamUng_USD", fThuHoiTamUng_USD);
                    data.Add("fThuHoiTamUng_VND", fThuHoiTamUng_VND);
                    data.Add("fSoTraDonViHuongThu_USD", fSoTraDonViHuongThu_USD);
                    data.Add("fSoTraDonViHuongThu_VND", fSoTraDonViHuongThu_VND);
                    data.Add("fSoTuChoiThanhToan_USD", fSoTuChoiThanhToan_USD);
                    data.Add("fSoTuChoiThanhToan_VND", fSoTuChoiThanhToan_VND);
                    data.Add("sSoTraDonViHuongThu_VND", sSoTraDonViHuongThu_VND);
                    data.Add("sLyDoTuChoi", sLyDoTuChoi);
                    data.Add("sGhiChu", sGhiChu);
                    data.Add("iLoaiDeNghi", iLoaiDeNghi);
                    data.Add("Items", lstChiTietBC.ToList());
                    data.Add("TieuDe", TxtTitleFirst);

                    AddChuKy(data, _typeChuky);

                    templateFileName = Path.Combine(ExportPrefix.PATH_NH_CP_DNTT, ExportFileName.RPT_NH_CAPPHAT_PHEDUYETTHANHTOAN);

                    fileNamePrefix = "rptNgoaiHoiCapPhatPheDuyetThanhToan_{0}";
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<BCPheDuyetThanhToanModel>(templateFileName, data);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
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
            _dmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            _dmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TxtTitleFirst = chuKy != null ? (string.IsNullOrEmpty(chuKy.TieuDe1MoTa) ? TITLE_FIRST_DEFAULT_VALUE : chuKy.TieuDe1MoTa) : string.Empty;
                TxtTitleSecond = chuKy != null ? (string.IsNullOrEmpty(chuKy.TieuDe2MoTa) ? TITLE_SECOND_DEFAULT_VALUE : chuKy.TieuDe2MoTa) : string.Empty;
            };
            _dmChuKyDialogViewModel.Init();
            _dmChuKyDialogViewModel.ShowDialog();
        }

        private void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add ngày địa điểm
            data.Add("Ngay", DateUtils.GetCurrentDateReport());
            data.Add("DiaDiem", _diaDiem);
            // add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy != null ? dmChyKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", dmChyKy != null ? dmChyKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ChucDanh1", dmChyKy != null ? dmChyKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", dmChyKy != null ? dmChyKy.ChucDanh2MoTa : string.Empty);
            data.Add("Ten1", dmChyKy != null ? dmChyKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", dmChyKy != null ? dmChyKy.Ten2MoTa : string.Empty);
        }

        private void LoadCatUnitTypes()
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

        private void LoadTitleFirst()
        {
            DmChuKy _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TxtTitleFirst = TITLE_FIRST_DEFAULT_VALUE;
            TxtTitleSecond = TITLE_SECOND_DEFAULT_VALUE;
        }
    }
}
