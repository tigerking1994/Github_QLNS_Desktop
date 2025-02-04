using FlexCel.XlsAdapter;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachChiQuy
{
    public class KeHoachChiQuyServerFtpViewModel : DialogViewModelBase<VdtNcNhuCauChiModel>
    {
        #region Private
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtNcNhuCauChiService _service;
        private readonly ISessionService _sessionService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly FtpStorageService _ftpStorageService;
        private List<ImportErrorItem> _lstErrChungTuChiTiet;
        private IImportExcelService _importService;
        private readonly ILog _logger;


        #endregion


        public override string Name => "Quản lý kế hoạch chi Quý";
        public bool IsInsert => Model.Id == Guid.Empty;
        public override string Description => string.Format("{0} thông tin kế hoạch chi Quý", IsInsert ? "Thêm mới" : "Cập nhật");

        #region Componer
        private string _sSoDeNghi;
        public string SSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        private DateTime? _dNgayDeNghi;
        public DateTime? DNgayDeNghi
        {
            get => _dNgayDeNghi;
            set => SetProperty(ref _dNgayDeNghi, value);
        }

        private int? _iNamKeHoach;
        public int? INamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                if (SetProperty(ref _iNamKeHoach, value))
                {
                    GetKinhPhiCucTaiChinhCap();
                }
            }
        }
        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }
        private string _sNguoiLap;
        public string SNguoiLap
        {
            get => _sNguoiLap;
            set => SetProperty(ref _sNguoiLap, value);
        }

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set
            {
                if (SetProperty(ref _cbxLoaiDonViSelected, value))
                {
                    GetKinhPhiCucTaiChinhCap();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }

        private ComboboxItem _cbxQuySelected;
        public ComboboxItem CbxQuySelected
        {
            get => _cbxQuySelected;
            set
            {
                if (SetProperty(ref _cbxQuySelected, value))
                {
                    GetKinhPhiCucTaiChinhCap();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxQuy;
        public ObservableCollection<ComboboxItem> CbxQuy
        {
            get => _cbxQuy;
            set => SetProperty(ref _cbxQuy, value);
        }

        private ComboboxItem _cbxNguonVonSelected;
        public ComboboxItem CbxNguonVonSelected
        {
            get => _cbxNguonVonSelected;
            set
            {
                if (SetProperty(ref _cbxNguonVonSelected, value))
                {
                    GetKinhPhiCucTaiChinhCap();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxNguonVon;
        public ObservableCollection<ComboboxItem> CbxNguonVon
        {
            get => _cbxNguonVon;
            set => SetProperty(ref _cbxNguonVon, value);
        }
        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }
        private double? _fQuyTruocChuaGiaiNgan;
        public double? FQuyTruocChuaGiaiNgan
        {
            get => _fQuyTruocChuaGiaiNgan;
            set => SetProperty(ref _fQuyTruocChuaGiaiNgan, value);
        }

        private double? _fGiaiNganQuyNay;
        public double? FGiaiNganQuyNay
        {
            get => _fGiaiNganQuyNay;
            set => SetProperty(ref _fGiaiNganQuyNay, value);
        }

        private double? _fThucHienGiaiNgan;
        public double? FThucHienGiaiNgan
        {
            get => _fThucHienGiaiNgan;
            set => SetProperty(ref _fThucHienGiaiNgan, value);
        }

        private double? _fKinhPhiChuyenQuySau;
        public double? FKinhPhiChuyenQuySau
        {
            get => _fKinhPhiChuyenQuySau;
            set => SetProperty(ref _fKinhPhiChuyenQuySau, value);
        }

        private double? _fKinhPhiCapQuyToi;
        public double? FKinhPhiCapQuyToi
        {
            get => _fKinhPhiCapQuyToi;
            set => SetProperty(ref _fKinhPhiCapQuyToi, value);
        }
        #endregion
        #region RelayCommand
        public RelayCommand GetFileFtpCommand { get; }
        public RelayCommand DownloadFileFtpServer { get; }
        #endregion

        public KeHoachChiQuyServerFtpViewModel(
            INsDonViService nsDonViService,
            IVdtNcNhuCauChiService service,
            ISessionService sessionService,
            FtpStorageService ftpStorageService,
            INsNguonNganSachService nsNguonNganSachService)
        {
            _nsDonViService = nsDonViService;
            _service = service;
            _sessionService = sessionService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _ftpStorageService = ftpStorageService;

            GetFileFtpCommand = new RelayCommand(obj => OnGetFileFtpCommand());
            DownloadFileFtpServer = new RelayCommand(obj => OnDownloadFileFtpServer());

        }

        #region Process
        public override void Init()
        {
            LoadDonVi();
            LoadQuy(); 
            LoadData();
        }
        private void LoadDonVi()
        {
            var data = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Select(n => new ComboboxItem() { DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi), ValueItem = n.IIDMaDonVi, HiddenValue = n.Id.ToString() });
            CbxLoaiDonVi = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(CbxLoaiDonVi));
        }
        public override void LoadData(params object[] args)
        {
            SetDefaultData();
        }

        private void SetDefaultData()
        {
            if (Model.Id == Guid.Empty)
            {
                CbxLoaiDonViSelected = null;
                SSoDeNghi = null;
                DNgayDeNghi = DateTime.Now;
                CbxNguonVonSelected = null;
                CbxQuySelected = null;
                SNguoiLap = null;
                INamKeHoach = null;
                FQuyTruocChuaGiaiNgan = null;
                FThucHienGiaiNgan = null;
                FKinhPhiChuyenQuySau = null;
                FKinhPhiCapQuyToi = null;
            }
            else
            {
                CbxLoaiDonViSelected = CbxLoaiDonVi.FirstOrDefault(n => n.ValueItem == Model.iID_MaDonViQuanLy);
                SSoDeNghi = Model.sSoDeNghi;
                DNgayDeNghi = Model.dNgayDeNghi;
                CbxNguonVonSelected = CbxNguonVon.FirstOrDefault(n => n.ValueItem == Model.iID_NguonVonID.ToString());
                CbxQuySelected = CbxQuy.FirstOrDefault(n => n.ValueItem == Model.iQuy.ToString()); ;
                SNguoiLap = Model.sNguoiLap;
                INamKeHoach = Model.iNamKeHoach;
            }
            LstFile = new ObservableCollection<FileFtpModel>();
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(SSoDeNghi));
            OnPropertyChanged(nameof(DNgayDeNghi));
            OnPropertyChanged(nameof(CbxNguonVonSelected));
            OnPropertyChanged(nameof(CbxQuySelected));
            OnPropertyChanged(nameof(SNguoiLap));
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(LstFile));

        }
        private void LoadQuy()
        {
            List<ComboboxItem> data = new List<ComboboxItem>();
            data.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_1, ValueItem = ((int)LoaiQuyEnum.Type.QUY_1).ToString() });
            data.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_2, ValueItem = ((int)LoaiQuyEnum.Type.QUY_2).ToString() });
            data.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_3, ValueItem = ((int)LoaiQuyEnum.Type.QUY_3).ToString() });
            data.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_4, ValueItem = ((int)LoaiQuyEnum.Type.QUY_4).ToString() });
            CbxQuy = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(CbxQuy));
        }
        private void GetKinhPhiCucTaiChinhCap()
        {
            if (!INamKeHoach.HasValue || CbxLoaiDonViSelected == null || CbxNguonVonSelected == null || CbxQuySelected == null)
            {
                FQuyTruocChuaGiaiNgan = 0;
                FGiaiNganQuyNay = 0;
                FThucHienGiaiNgan = 0;
                FKinhPhiChuyenQuySau = 0;
            }
            else
            {
                var data = _service.GetKinhPhiCucTaiChinhCap(INamKeHoach.Value,
                    CbxLoaiDonViSelected.ValueItem,
                    int.Parse(CbxNguonVonSelected.ValueItem),
                    int.Parse(CbxQuySelected.ValueItem));
                if (data != null)
                {
                    FQuyTruocChuaGiaiNgan = data.fQuyTruocChuaGiaiNgan;
                    FGiaiNganQuyNay = data.fQuyNayDuocCap;
                    FThucHienGiaiNgan = data.fGiaiNganQuyNay;
                    FKinhPhiChuyenQuySau = data.fChuaGiaiNganChuyenQuySau;
                }
            }
            OnPropertyChanged(nameof(FQuyTruocChuaGiaiNgan));
            OnPropertyChanged(nameof(FGiaiNganQuyNay));
            OnPropertyChanged(nameof(FThucHienGiaiNgan));
            OnPropertyChanged(nameof(FKinhPhiChuyenQuySau));
        }
        private void OnGetFileFtpCommand()
        {
            if (CbxLoaiDonViSelected == null || INamKeHoach == null || INamKeHoach < 0)
            {

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng nhập năm kế hoạch");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            else if (CbxQuySelected == null)
            {

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn quý");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            else
            {
                var btmTenDonVi = StringUtils.UCS2Convert(CbxLoaiDonViSelected.ValueItem);
                string sTime = string.Format("{0}", INamKeHoach);
                string precious = CbxQuySelected.ValueItem;
                var strUrl = string.Format("{0}/{1}/{2}/{3}", btmTenDonVi,ConstantUrlPathPhanHe.UrlKhcqWinformSend,sTime, precious);
                var lstData = _ftpStorageService.GetFileServerFtp(strUrl);
                if (lstData == null || lstData.Count == 0)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Không tìm thấy dữ liệu");
                    System.Windows.MessageBox.Show(messageBuilder.ToString());
                    return;
                }
                LstFile = new ObservableCollection<FileFtpModel>(lstData);
            }
            
        }
        private void OnDownloadFileFtpServer()
        {
            string urlUrIDownLoad = "";
            string fileName = "";
            if (LstFile == null || LstFile.Count == 0 || !LstFile.Any(e => e.BIsCheck))
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng lấy dữ liệu");
                System.Windows.MessageBox.Show(messageBuilder.ToString());
                return;
            }
            foreach (var item in LstFile)
            {
                if (item.BIsCheck)
                {
                    urlUrIDownLoad = item.SUrl;
                    fileName = item.SNameFile;
                    _ftpStorageService.GiveFileFtpGiveLocal(urlUrIDownLoad,ref fileName);
                }
            }

        }
        #endregion
    }

}
