using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class DmChuKyDialogViewModel : ViewModelBase
    {
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private readonly ITlBaoCaoService _iTlBaoCaoService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _provider;
        private ISessionService _sessionService;
        private string _appSettingConfigPath;
        private List<BaoHiemXaHoiSetting> _listSetting;


        public override string Name => "Chỉnh sửa thông tin chữ ký báo cáo";
        public override string Description => "Chỉnh sửa thông tin chữ ký";
        public override Type ContentType => typeof(DmChuKyDialogView);

        public ObservableCollection<DanhMucCauHinhHeThongModel> ChucDanhs { get; set; }
        public ObservableCollection<DanhMucCauHinhHeThongModel> DonViBanHanhs { get; set; }
        public ObservableCollection<DanhMucCauHinhHeThongModel> NhomChuKys { get; set; }
        public ObservableCollection<DanhMucCauHinhHeThongModel> ChuKyTen { get; set; }
        public ObservableCollection<TlBaoCaoModel> NhomChuKyLuong { get; set; }
        public List<ComboboxItem> RowHeights { get; set; }
        public ObservableCollection<ComboboxItem> TypeChuKys { get; set; }

        private ObservableCollection<ComboboxItem> _fontTypes;
        public ObservableCollection<ComboboxItem> FontTypes
        {
            get => _fontTypes;
            set => SetProperty(ref _fontTypes, value);
        }

        private ComboboxItem _selectedFontType;
        public ComboboxItem SelectedFontType
        {
            get => _selectedFontType;
            set
            {
                if (SetProperty(ref _selectedFontType, value) && value != null)
                {
                    GlobalVariables.AddItemsByTag("FontType", value.ValueItem);
                }
            }
        }

        private ObservableCollection<ComboboxItem> _fontSizes;
        public ObservableCollection<ComboboxItem> FontSizes
        {
            get => _fontSizes;
            set => SetProperty(ref _fontSizes, value);
        }

        private ComboboxItem _selectedFontSize;
        public ComboboxItem SelectedFontSize
        {
            get => _selectedFontSize;
            set { 
                if (SetProperty(ref _selectedFontSize, value) && value != null)
                {
                    GlobalVariables.AddItemsByTag("FontSize", value.ValueItem);
                }
            }
        }

        public DmChuKyModel DmChuKyModel { get; set; }
        public bool IsEnableChuKy { get; set; } = true;
        public bool IsEnableThongTri { get; set; }
        public bool IsShowNoiDungChi { get; set; } = false;

        private string _noiDungChi;
        public string NoiDungChi
        {
            get => _noiDungChi;
            set => SetProperty(ref _noiDungChi, value);
        }

        public TlBaoCaoModel BaoCaoLuongModel { get; set; }

        private string _nhomChuKy;
        public string NhomChuky
        {
            get => _nhomChuKy;
            set => SetProperty(ref _nhomChuKy, value);
        }

        private TlBaoCaoModel _nhomChuKyLuongSelected;
        public TlBaoCaoModel NhomChuKyLuongSelected
        {
            get => _nhomChuKyLuongSelected;
            set => SetProperty(ref _nhomChuKyLuongSelected, value);
        }

        private int _rowHeight;
        public int RowHeight
        {
            get => _rowHeight;
            set
            {
                SetProperty(ref _rowHeight, value);
                if (_rowHeight != 0)
                {
                    if (Application.Current.Properties.Contains(NSConstants.ROWHEIGHT))
                        Application.Current.Properties[NSConstants.ROWHEIGHT] = _rowHeight;
                    else Application.Current.Properties.Add(NSConstants.ROWHEIGHT, _rowHeight);
                }
            }
        }

        private string _typeChuKy;
        public string TypeChuKy
        {
            get => _typeChuKy;
            set => SetProperty(ref _typeChuKy, value);
        }

        public bool IsLuong { get; set; }
        public bool HasAddedSign4 { get; set; }
        public bool HasAddedSign5 { get; set; }
        public bool HasAddedSign6 { get; set; }

        public DmChuKyDialogViewModel(IMapper mapper, IServiceProvider serviceProvider, ISessionService sessionService)
        {
            _mapper = mapper;
            _provider = serviceProvider;
            _dmChuKyService = (IDmChuKyService)_provider.GetService(typeof(IDmChuKyService));
            _danhMucService = (IDanhMucService)_provider.GetService(typeof(IDanhMucService));
            _configuration = (IConfiguration)_provider.GetService(typeof(IConfiguration));
            _iTlBaoCaoService = (ITlBaoCaoService)_provider.GetService(typeof(ITlBaoCaoService));
            _sessionService = sessionService;

            _appSettingConfigPath = _configuration.GetSection(ConfigHelper.CONFIG_REPORT_BHXH_SETTING_PATH).Value;

            SaveCommand = new RelayCommand(obj => OnSave(obj));
        }

        public override void OnSave(object obj)
        {
            if (string.IsNullOrEmpty(DmChuKyModel.IdType))
            {
                MessageBox.Show(string.Format(Resources.MsgInputRequire, "Mã chữ ký"), Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(DmChuKyModel.IdCode))
            {
                DmChuKyModel.IdCode = DmChuKyModel.IdType;
            }
            if (!string.IsNullOrEmpty(DmChuKyModel.TenDVBanHanh1) && DmChuKyModel.TenDVBanHanh1.Length > 100)
            {
                MessageBox.Show(string.Format(Resources.MsgMaxLengthInvalid, 1), Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(DmChuKyModel.TenDVBanHanh2) && DmChuKyModel.TenDVBanHanh2.Length > 100)
            {
                MessageBox.Show(string.Format(Resources.MsgMaxLengthInvalid, 2), Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (IsEnableThongTri)
            {
                DmChuKyModel.ThuaUyQuyen1MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ThuaUyQuyen1))?.SGiaTri;
                DmChuKyModel.ThuaUyQuyen2MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ThuaUyQuyen2))?.SGiaTri;
                DmChuKyModel.ThuaUyQuyen3MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ThuaUyQuyen3))?.SGiaTri;
            }

            DmChuKyModel.ChucDanh1MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ChucDanh1))?.SGiaTri;
            DmChuKyModel.ThuaLenh1MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ThuaLenh1))?.SGiaTri;
            DmChuKyModel.Ten1MoTa = ChuKyTen.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.Ten1))?.SGiaTri;
            DmChuKyModel.ChucDanh2MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ChucDanh2))?.SGiaTri;
            DmChuKyModel.ThuaLenh2MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ThuaLenh2))?.SGiaTri;
            DmChuKyModel.Ten2MoTa = ChuKyTen.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.Ten2))?.SGiaTri;
            DmChuKyModel.ChucDanh3MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ChucDanh3))?.SGiaTri;
            DmChuKyModel.ThuaLenh3MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ThuaLenh3))?.SGiaTri;
            DmChuKyModel.Ten3MoTa = ChuKyTen.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.Ten3))?.SGiaTri;
            DmChuKyModel.ChucDanh4MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ChucDanh4))?.SGiaTri;
            DmChuKyModel.ThuaLenh4MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ThuaLenh4))?.SGiaTri;
            DmChuKyModel.Ten4MoTa = ChuKyTen.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.Ten4))?.SGiaTri;
            DmChuKyModel.ChucDanh5MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ChucDanh5))?.SGiaTri;
            DmChuKyModel.ThuaLenh5MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ThuaLenh5))?.SGiaTri;
            DmChuKyModel.Ten5MoTa = ChuKyTen.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.Ten5))?.SGiaTri;
            DmChuKyModel.ChucDanh6MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ChucDanh6))?.SGiaTri;
            DmChuKyModel.ThuaLenh6MoTa = ChucDanhs.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.ThuaLenh6))?.SGiaTri;
            DmChuKyModel.Ten6MoTa = ChuKyTen.FirstOrDefault(c => c.IIDMaDanhMuc != null && c.IIDMaDanhMuc.Equals(DmChuKyModel.Ten6))?.SGiaTri;
            DmChuKyModel.SLoai = TypeChuKy;
            DmChuKy dmChuKy = _mapper.Map<DmChuKy>(DmChuKyModel);
            dmChuKy.BDanhSach = true;
            if (Guid.Empty.Equals(DmChuKyModel.Id))
            {
                _dmChuKyService.Add(dmChuKy);
            }
            else
            {
                _dmChuKyService.Save(dmChuKy);
            }

            if (IsLuong)
            {
                BaoCaoLuongModel.TenBaoCao = DmChuKyModel.Ten;
                BaoCaoLuongModel.MaParent = NhomChuKyLuongSelected != null ? NhomChuKyLuongSelected.MaBaoCao : BaoCaoLuongModel.MaParent;
                TlBaoCao tlBaoCao = new TlBaoCao();
                _mapper.Map(BaoCaoLuongModel, tlBaoCao);
                _iTlBaoCaoService.Update(tlBaoCao);
            }

            var data = new BaoHiemXaHoiSetting()
            {
                NoiDung = NoiDungChi,
                Loai = "Quyết toán chi quý",
                MaBaoCao = DmChuKyModel.IdCode
            };

            _listSetting ??= new List<BaoHiemXaHoiSetting>();
            var setting = _listSetting.FirstOrDefault(x => x.MaBaoCao == DmChuKyModel.IdType && x.Loai == "Quyết toán chi quý");
            if (setting is null)
            {
                _listSetting.Add(data);
            } else
            {
                setting.NoiDung = data.NoiDung;
            }

            Helper.ConfigHelper.UpdateSetting<List<BaoHiemXaHoiSetting>>(_appSettingConfigPath, _listSetting);

            MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            if (SavedAction != null)
            {
                SavedAction.Invoke(DmChuKyModel);
                Window window = obj as Window;
                if (window != null) window.Close();
            }
        }

        public override void Init()
        {

            IsEnableThongTri = IsEnableChuKy && (DmChuKyModel.IdType == Utility.TypeChuKy.RPT_NS_QUYETTOAN_THONGTRI_LNS || DmChuKyModel.IdType == Utility.TypeChuKy.RPT_NS_QUYETTOAN_THONGTRI_DONVI);

            int year = _sessionService.Current.YearOfWork;
            base.Init();
            DanhMucCauHinhHeThongModel defaultEmptyVal = new DanhMucCauHinhHeThongModel();

            IEnumerable<DanhMuc> chucdanhs = new ObservableCollection<DanhMuc>(_dmChuKyService.FindChuKyChucDanh());
            ChucDanhs = _mapper.Map<ObservableCollection<DanhMucCauHinhHeThongModel>>(chucdanhs);
            ChucDanhs.Insert(0, defaultEmptyVal);

            IEnumerable<DanhMuc> nhomChuKy = new ObservableCollection<DanhMuc>(_dmChuKyService.FindNhomChuKy(year));
            NhomChuKys = _mapper.Map<ObservableCollection<DanhMucCauHinhHeThongModel>>(nhomChuKy);
            NhomChuKys.Insert(0, defaultEmptyVal);

            IEnumerable<DanhMuc> chuKyTen = new ObservableCollection<DanhMuc>(_dmChuKyService.FindChuKyTen());
            ChuKyTen = _mapper.Map<ObservableCollection<DanhMucCauHinhHeThongModel>>(chuKyTen);
            ChuKyTen.Insert(0, defaultEmptyVal);

            List<DanhMuc> donViBanHanhs = new ObservableCollection<DanhMuc>(_danhMucService.FindByCondition(
                x => (x.INamLamViec == _sessionService.Current.YearOfWork) && (x.IIDMaDanhMuc == "DV_QUANLY" || x.IIDMaDanhMuc == "DV_THONGTRI_BANHANH"))).ToList();

            List<int> heights = new List<int> { 85, 90, 95, 100, 110, 115, 120, 125, 130, 135, 140, 150, 160, 180, 200 };
            RowHeights = new List<ComboboxItem>();
            foreach (var height in heights)
            {
                RowHeights.Add(new ComboboxItem(height.ToString(), height.ToString()));
            }

            TypeChuKys = new ObservableCollection<ComboboxItem>();
            TypeChuKys.Add(new ComboboxItem(string.Empty, null));
            TypeChuKys.Add(new ComboboxItem(NSConstants.SO_NHU_CAU, NSConstants.SO_NHU_CAU));
            TypeChuKys.Add(new ComboboxItem(NSConstants.NGANH_THAM_DINH, NSConstants.NGANH_THAM_DINH));
            TypeChuKys.Add(new ComboboxItem(NSConstants.SO_KIEM_TRA_NHAN, NSConstants.SO_KIEM_TRA_NHAN));
            TypeChuKys.Add(new ComboboxItem(NSConstants.SO_KIEM_TRA_PHAN_BO, NSConstants.SO_KIEM_TRA_PHAN_BO));
            TypeChuKys.Add(new ComboboxItem(NSConstants.DU_TOAN_DAU_NAM, NSConstants.DU_TOAN_DAU_NAM));
            TypeChuKys.Add(new ComboboxItem(NSConstants.DU_TOAN_NHAN_PHAN_BO, NSConstants.DU_TOAN_NHAN_PHAN_BO));
            TypeChuKys.Add(new ComboboxItem(NSConstants.DU_TOAN_PHAN_BO, NSConstants.DU_TOAN_PHAN_BO));
            TypeChuKys.Add(new ComboboxItem(NSConstants.DU_TOAN_DIEU_CHINH, NSConstants.DU_TOAN_DIEU_CHINH));
            TypeChuKys.Add(new ComboboxItem(NSConstants.CAP_PHAT, NSConstants.CAP_PHAT));
            TypeChuKys.Add(new ComboboxItem(NSConstants.QUYET_TOAN, NSConstants.QUYET_TOAN));
            TypeChuKys.Add(new ComboboxItem(NSConstants.BANG_KE, NSConstants.BANG_KE));
            TypeChuKys.Add(new ComboboxItem(NSConstants.QUAN_SO, NSConstants.QUAN_SO));
            TypeChuKys.Add(new ComboboxItem(NSConstants.NHU_CAU_CHI_QUY, NSConstants.NHU_CAU_CHI_QUY));
            TypeChuKys.Add(new ComboboxItem(BHXHConstants.CAP_KINH_PHI, BHXHConstants.CAP_KINH_PHI));
            TypeChuKys.Add(new ComboboxItem(BHXHConstants.CAP_TAM_UNG_KCB_BHYT, BHXHConstants.CAP_TAM_UNG_KCB_BHYT));
            TypeChuKys.Add(new ComboboxItem(BHXHConstants.CAP_BOSUNG_KCB_BHYT, BHXHConstants.CAP_BOSUNG_KCB_BHYT));
            if (Application.Current.Properties.Contains(NSConstants.ROWHEIGHT))
                RowHeight = (int)Application.Current.Properties[NSConstants.ROWHEIGHT];
            else RowHeight = NSConstants.ReportRowHeight;
            if (!Guid.Empty.Equals(DmChuKyModel.Id))
            {
                DmChuKy dmChuKy = _dmChuKyService.FindById(DmChuKyModel.Id);
                if (string.IsNullOrEmpty(dmChuKy.TieuDe1MoTa)) dmChuKy.TieuDe1MoTa = DmChuKyModel.TieuDe1MoTa;
                if (string.IsNullOrEmpty(dmChuKy.TieuDe2MoTa)) dmChuKy.TieuDe2MoTa = DmChuKyModel.TieuDe2MoTa;
                if (string.IsNullOrEmpty(dmChuKy.TieuDe3MoTa)) dmChuKy.TieuDe3MoTa = DmChuKyModel.TieuDe3MoTa;
                DmChuKyModel = _mapper.Map<DmChuKyModel>(dmChuKy);
                TypeChuKy = DmChuKyModel.SLoai;
            }

            if (IsLuong)
            {
                IEnumerable<TlBaoCao> nhomChuKyLuong = _iTlBaoCaoService.FindByCondition(x => true.Equals(x.IsParent));
                NhomChuKyLuong = _mapper.Map<ObservableCollection<TlBaoCaoModel>>(nhomChuKyLuong);
                if (BaoCaoLuongModel != null)
                {
                    NhomChuKyLuongSelected =
                        NhomChuKyLuong.FirstOrDefault(x => x.MaBaoCao.Equals(BaoCaoLuongModel.MaParent));
                    DmChuKyModel.Ten = BaoCaoLuongModel.TenBaoCao;
                }
            }

            var mappings = new Dictionary<string, string>
            {
                { "DV_QUANLY", "1" },
                { "DV_THONGTRI_BANHANH", "3" }
            };

            donViBanHanhs.ForEach(x =>
            {
                if (mappings.ContainsKey(x.IIDMaDanhMuc))
                {
                    x.IIDMaDanhMuc = mappings[x.IIDMaDanhMuc];
                }
            });

            var danhMucList = new List<DanhMuc>
            {
                new DanhMuc { SGiaTri = _sessionService.Current.TenDonVi, IIDMaDanhMuc = "2" },
                new DanhMuc { SGiaTri = "Tùy chỉnh", IIDMaDanhMuc = "5" }
            };

            if (DmChuKyModel.IdType != "rptNS_QuyetToan_ThongTri_DonVi" && DmChuKyModel.IdType != "rptNS_CapPhat_DonVi")
            {
                danhMucList.Insert(1, new DanhMuc { SGiaTri = "Đơn vị được chọn", IIDMaDanhMuc = "4" });
            }

            donViBanHanhs.AddRange(danhMucList);

            DonViBanHanhs = _mapper.Map<ObservableCollection<DanhMucCauHinhHeThongModel>>(donViBanHanhs.OrderBy(n => n.IIDMaDanhMuc));
            DonViBanHanhs.Insert(0, defaultEmptyVal);

            FontTypes = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem("Times New Roman", "Times New Roman"),
                new ComboboxItem("Calibri", "Calibri"),
                new ComboboxItem("Arial", "Arial"),
                new ComboboxItem("Consolas", "Consolas"),
            };

            if (GlobalVariables.GetItemsByTag("FontType") == "0")
            {
                SelectedFontType = FontTypes.ElementAt(0);
            } else
            {
                SelectedFontType = FontTypes.FirstOrDefault(x => x.ValueItem == GlobalVariables.GetItemsByTag("FontType"));
            }

            FontSizes = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem("100", "100"),
                new ComboboxItem("110", "110"),
                new ComboboxItem("120", "120"),
                new ComboboxItem("130", "130"),
                new ComboboxItem("140", "140"),
                new ComboboxItem("150", "150"),
                new ComboboxItem("160", "160"),
                new ComboboxItem("170", "170"),
                new ComboboxItem("180", "180"),
                new ComboboxItem("190", "190"),
                new ComboboxItem("200", "200"),
            };

            if (GlobalVariables.GetItemsByTag("FontSize") == "0")
            {
                SelectedFontSize = FontSizes.ElementAt(0);
            }
            else
            {
                SelectedFontSize = FontSizes.FirstOrDefault(x => x.ValueItem == GlobalVariables.GetItemsByTag("FontSize"));
            }

            _listSetting = Helper.ConfigHelper.ReadSetting<List<BaoHiemXaHoiSetting>>(_appSettingConfigPath);
            NoiDungChi = _listSetting?.FirstOrDefault(x => x.MaBaoCao == DmChuKyModel.IdType && x.Loai == "Quyết toán chi quý")?.NoiDung ?? string.Empty;
        }

        public override void OnClose(object obj)
        {
            base.OnClose(obj);
            Window window = obj as Window;
            window.Close();
        }
    }
}
