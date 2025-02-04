using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Domain;
using System.Globalization;
using System.Text.RegularExpressions;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.TransferData
{
    public class TransferDataDialogViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDmMapPcDetailService _tlDmMapPcDetailService;
        private readonly ITlMapColumnConfigService _tlMapColumnConfigService;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly ITlDmPhuCapService _tlDmPhuCapService;
        private readonly ITlDmCapBacService _tlDmCapBacService;
        private readonly ITlDmChucVuService _tlDmChucVuService;

        public ObservableCollection<TlDmDonViModel> lstDonVi { get; set; }
        public ObservableCollection<CadresModel> lstCadres { get; set; }
        public ObservableCollection<TlCanBoPhuCapModel> lstCanBoPhuCap { get; set; }
        public List<TlPhuCapFoxproNotMappingModel> tlPhuCapFoxproNotMappingModels { get; set; }
        public DataTable DtChuaMap { get; set; }

        public string warningMessage { get; set; }

        public override string FuncCode => NSFunctionCode.SALARY_CHUYEN_DOI_DU_LIEU_DOI_TUONG_DIALOG;

        public override Type ContentType => typeof(View.Salary.TransferData.TransferCadres.TransferDataDialog);
        public override PackIconKind IconKind => PackIconKind.FolderSwapOutline;
        public override string Title => "Chọn file để import";
        public override string Description => "Chọn file để import";

        private string _donViPath;
        public string DonViPath
        {
            get => _donViPath;
            set => SetProperty(ref _donViPath, value);
        }

        private string _doiTuongPath;
        public string DoiTuongPath
        {
            get => _doiTuongPath;
            set => SetProperty(ref _doiTuongPath, value);
        }

        private string _dmLuongPath;
        public string DmLuongPath
        {
            get => _dmLuongPath;
            set => SetProperty(ref _dmLuongPath, value);
        }

        private DataTable _dtDonVi;
        public DataTable DtDonVi
        {
            get => _dtDonVi;
            set => SetProperty(ref _dtDonVi, value);
        }

        private DataTable _dtDoiTuong;
        public DataTable DtDoiTuong
        {
            get => _dtDoiTuong;
            set => SetProperty(ref _dtDoiTuong, value);
        }

        private DataTable _dtDmLuong;
        public DataTable DtDmLuong
        {
            get => _dtDmLuong;
            set => SetProperty(ref _dtDmLuong, value);
        }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set => SetProperty(ref _monthSelected, value);
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _yearSelected;
        public ComboboxItem YearSelected
        {
            get => _yearSelected;
            set => SetProperty(ref _yearSelected, value);
        }

        private int _month;
        public int Month
        {
            get => _month;
            set => _month = value;
        }

        private int _year;
        public int Year
        {
            get => _year;
            set => _year = value;
        }

        private bool _isBienPhong;
        public bool IsBienPhong
        {
            get => _isBienPhong;
            set => SetProperty(ref _isBienPhong, value);
        }

        private bool _isMau2;
        public bool IsMau2
        {
            get => _isMau2;
            set => SetProperty(ref _isMau2, value);
        }

        public RelayCommand UploadFileDonViCommand { get; }
        public RelayCommand UploadFileDoiTuongCommand { get; }
        public RelayCommand UploadFileDmLuongCommand { get; }
        public RelayCommand ChooseCommand { get; }
        public RelayCommand RefreshCommand { get; }

        public Action<object> ChooseAction;

        public TransferDataDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlDmMapPcDetailService tlDmMapPcDetailService,
            ITlMapColumnConfigService tlMapColumnConfigService,
            ITlDmCanBoService cadresService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            ITlDmPhuCapService tlDmPhuCapService,
            ITlDmCapBacService tlDmCapBacService,
            ITlDmChucVuService tlDmChucVuService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            UploadFileDonViCommand = new RelayCommand(o => OnUploadDonVi());
            UploadFileDoiTuongCommand = new RelayCommand(o => OnUploadDoiTuong());
            UploadFileDmLuongCommand = new RelayCommand(o => OnUploadDmLuong());
            ChooseCommand = new RelayCommand(o => OnChoose());
            RefreshCommand = new RelayCommand(o => OnRefresh());

            _tlDmDonViService = tlDmDonViService;
            _tlDmMapPcDetailService = tlDmMapPcDetailService;
            _tlMapColumnConfigService = tlMapColumnConfigService;
            _cadresService = cadresService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDmChucVuService = tlDmChucVuService;
        }

        public override void Init()
        {
            base.Init();
            LoadMonths();
            LoadYear();
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(Months));
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == thang.ToString());
        }

        private void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        private void OnUploadDoiTuong()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Chọn file DBF đối tượng hưởng lương, phụ cấp",
                RestoreDirectory = true,
                DefaultExt = StringUtils.DBF_EXTENSION,
                Filter = "DBF(*.dbf)|*.dbf|All file(*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DoiTuongPath = openFileDialog.FileName;
            OnPropertyChanged(nameof(DoiTuongPath));
        }

        private void OnUploadDonVi()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Chọn file DONVI.DBF",
                RestoreDirectory = true,
                DefaultExt = StringUtils.DBF_EXTENSION,
                Filter = "DBF(*.dbf)|*.dbf|All file(*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DonViPath = openFileDialog.FileName;
            OnPropertyChanged(nameof(DonViPath));
        }

        private void OnUploadDmLuong()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Chọn file DMLG.DBF",
                RestoreDirectory = true,
                DefaultExt = StringUtils.DBF_EXTENSION,
                Filter = "DBF(*.dbf)|*.dbf|All file(*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DmLuongPath = openFileDialog.FileName;
            OnPropertyChanged(nameof(DmLuongPath));
        }

        private void OnRefresh()
        {
            DonViPath = string.Empty;
            DoiTuongPath = string.Empty;
        }

        private void OnChoose()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                Month = int.Parse(MonthSelected.ValueItem);
                Year = int.Parse(YearSelected.ValueItem);
                tlPhuCapFoxproNotMappingModels = new List<TlPhuCapFoxproNotMappingModel>();
                DtChuaMap = new DataTable();
                var message = GetMessageValidate();
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    IsLoading = false;
                    return;
                }
                DtDonVi = ParseDBF.ReadDBF(DonViPath);
                DtDoiTuong = ParseDBF.ReadDBF(DoiTuongPath);
                DtDmLuong = ParseDBF.ReadDBF(DmLuongPath);

                SaveDonVi();
                if (!IsBienPhong)
                {
                    SaveCanBo();
                }
                else
                {
                    SaveCanBoBienPhong();
                }

                foreach (var item in tlPhuCapFoxproNotMappingModels)
                {
                    var dataRows = DtDmLuong.AsEnumerable().FirstOrDefault(x => x.Field<string>("KH").Trim().Equals(item.MaPhuCap));
                    if (dataRows != null)
                    {
                        item.GiaTriFoxPro = dataRows.Field<decimal>("Tien");
                    }
                }
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    // Thành công
                    MessageBoxHelper.Info("Chọn file thành công");
                    ChooseAction?.Invoke(ChooseCommand);
                    DialogHost.Close("RootDialog");
                }
                else
                {
                    MessageBoxHelper.Info("Có lỗi xảy ra.");
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private void SaveDonVi()
        {
            List<TlDmDonVi> tlDmDonVis = new List<TlDmDonVi>();
            if (!IsBienPhong)
            {
                foreach (DataRow item in DtDonVi.Rows)
                {
                    TlDmDonVi tlDmDonVi = new TlDmDonVi();
                    tlDmDonVi.MaDonVi = item.Field<string>("DVI").Trim();
                    tlDmDonVi.TenDonVi = item.Field<string>("TEN").Trim();
                    tlDmDonVi.ITrangThai = true;
                    tlDmDonVis.Add(tlDmDonVi);
                }
            }
            else
            {
                foreach (DataRow item in DtDonVi.Rows)
                {
                    TlDmDonVi tlDmDonVi = new TlDmDonVi();
                    tlDmDonVi.MaDonVi = item.Field<string>("MASO").Trim();
                    tlDmDonVi.TenDonVi = item.Field<string>("TENDONVI").Trim();
                    tlDmDonVi.ITrangThai = true;
                    tlDmDonVis.Add(tlDmDonVi);
                }
            }

            lstDonVi = _mapper.Map<ObservableCollection<TlDmDonViModel>>(tlDmDonVis);
        }

        private void SaveCanBo()
        {
            try
            {
                var lstMapColumnConfig = _tlMapColumnConfigService.FindAll().Where(x => IsMau2 ? x.Mau == 2 : x.Mau == 1);
                var lstDmMapPcDetail = _tlDmMapPcDetailService.FindAll();
                var lstPhuCap = _tlDmPhuCapService.FindAll();
                var lst = lstPhuCap.Select(x => x.MaPhuCap).ToList();
                var lstCanBo = _cadresService.FindAllState();

                IList<string> message = new List<string>();

                var lstImportCanBo = new List<TlDmCanBo>();
                var lstImportPhuCapCanBo = new List<TlCanBoPhuCap>();

                int maHieuCanBoMax;
                if (lstCanBo.Count() > 0 && lstCanBo != null)
                {
                    maHieuCanBoMax = lstCanBo.Max(x => int.Parse(x.MaHieuCanBo));
                }
                else
                {
                    maHieuCanBoMax = 0;
                }

                var lstColumn = DtDoiTuong.Columns;
                DtDoiTuong.Columns.Add("TenDonVi", typeof(string));
                DtChuaMap = DtDoiTuong.Clone();
                var isFileMauMoi = DtDoiTuong.Columns.Contains("HO");

                foreach (DataRow item in DtDoiTuong.Rows)
                {
                    TlDmCanBo tlDmCanBo = new TlDmCanBo();
                    tlDmCanBo.Thang = Month;
                    tlDmCanBo.Nam = Year;
                    tlDmCanBo.IsDelete = true;
                    tlDmCanBo.BHTN = false;
                    tlDmCanBo.PCCV = true;
                    tlDmCanBo.KhongLuong = false;
                    tlDmCanBo.BNuocNgoai = false;
                    tlDmCanBo.Tm = false;
                    tlDmCanBo.IsLock = false;
                    tlDmCanBo.MaHieuCanBo = (++maHieuCanBoMax).ToString();
                    tlDmCanBo.MaCanBo = Year + Month.ToString("D2") + tlDmCanBo.MaHieuCanBo;

                    var tlCanBoPhuCapModels = new List<TlCanBoPhuCap>();

                    foreach (var item2 in lstPhuCap)
                    {
                        TlCanBoPhuCap tlCanBoPhuCap = new TlCanBoPhuCap();
                        tlCanBoPhuCap.MaCbo = tlDmCanBo.MaCanBo;
                        tlCanBoPhuCap.MaPhuCap = item2.MaPhuCap;
                        tlCanBoPhuCap.GiaTri = item2.GiaTri;
                        tlCanBoPhuCap.HuongPcSn = item2.HuongPCSN;
                        tlCanBoPhuCap.BSaoChep = item2.BSaoChep;
                        tlCanBoPhuCap.Flag = false;
                        tlCanBoPhuCapModels.Add(tlCanBoPhuCap);
                    }

                    if (isFileMauMoi)
                    {
                        tlDmCanBo.TenCanBo = item["HO"].ToString() + item["TEN"].ToString();
                        if (!item["TNN"].ToString().IsEmpty() && !item["NNN"].ToString().IsEmpty())
                            tlDmCanBo.NgayNn = new DateTime(int.Parse(item["NNN"].ToString()), int.Parse(item["TNN"].ToString()), 1);
                        if (!item["TTN"].ToString().IsEmpty() && !item["NTN"].ToString().IsEmpty())
                            tlDmCanBo.NgayTn = new DateTime(int.Parse(item["NTN"].ToString()), int.Parse(item["TTN"].ToString()), 1);
                        if (!item["TXN"].ToString().IsEmpty() && !item["NXN"].ToString().IsEmpty())
                            tlDmCanBo.NgayXn = new DateTime(int.Parse(item["NXN"].ToString()), int.Parse(item["TXN"].ToString()), 1);
                    }

                    var lstProperties = tlDmCanBo.GetType().GetProperties();
                    foreach (var item1 in lstMapColumnConfig)
                    {
                        var property = lstProperties.FirstOrDefault(x => x.Name.Equals(item1.NewColumn));
                        if (property != null)
                        {
                            if (item1.OldColumn.Equals(OldColumnName.NHAPNGU) || item1.OldColumn.Equals(OldColumnName.XUATNGU) || item1.OldColumn.Equals(OldColumnName.TAINGU))
                            {
                                DateTime? dateTime = ReCalculateDateTime(item1.OldColumn, item);
                                property.SetValue(tlDmCanBo, dateTime);
                            }
                            else if (item1.OldColumn.Equals(OldColumnName.CBAC))
                            {
                                if (!string.IsNullOrEmpty(item[OldColumnName.CBAC].ToString().Trim()))
                                {
                                    if (item[OldColumnName.MADT].ToString().Trim().Equals("1") || item[OldColumnName.MADT].ToString().Trim().Equals("2"))
                                    {
                                        property.SetValue(tlDmCanBo, string.Format("{0}{1}", item[OldColumnName.MADT].ToString().Trim(), item[OldColumnName.CBAC].ToString().Trim()));
                                    }
                                    else
                                    {
                                        property.SetValue(tlDmCanBo, item[OldColumnName.CBAC].ToString().Trim());
                                    }
                                }
                                else
                                {
                                    property.SetValue(tlDmCanBo, string.Empty);
                                }
                            }
                            else if (item1.OldColumn.Equals(OldColumnName.NU))
                            {
                                if (string.IsNullOrEmpty(item[OldColumnName.NU].ToString().Trim()))
                                {
                                    property.SetValue(tlDmCanBo, true);
                                }
                                else
                                {
                                    property.SetValue(tlDmCanBo, false);
                                }
                            }
                            else
                            {
                                if (!lstColumn.Contains(item1.OldColumn))
                                {
                                    continue;
                                }

                                if (property.PropertyType.FullName.Contains("System.Int32"))
                                {
                                    property.SetValue(tlDmCanBo, int.Parse(item[item1.OldColumn].ToString().Trim()));
                                }
                                else if (property.PropertyType.FullName.Contains("System.Boolean"))
                                {
                                    if (string.IsNullOrEmpty(item[item1.OldColumn].ToString().Trim()))
                                    {
                                        property.SetValue(tlDmCanBo, false);
                                    }
                                    else
                                    {
                                        property.SetValue(tlDmCanBo, true);
                                    }
                                }
                                else
                                {
                                    property.SetValue(tlDmCanBo, item[item1.OldColumn].ToString().Trim());
                                }
                            }
                        }

                        if (property == null && item1.IsMapPhuCap.HasValue && (bool)item1.IsMapPhuCap)
                        {
                            string maPc = item[item1.OldColumn].ToString().Trim();
                            if (!string.IsNullOrEmpty(maPc))
                            {
                                var mapPcDetail = lstDmMapPcDetail.FirstOrDefault(x => x.OldValue.Equals(maPc));
                                if (mapPcDetail != null)
                                {
                                    var canBoPhuCap = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(mapPcDetail.MaPhuCap));
                                    if (canBoPhuCap != null)
                                    {
                                        canBoPhuCap.GiaTri = mapPcDetail.Giatri;
                                    }
                                }
                                else
                                {
                                    DtChuaMap.Rows.Add(item.ItemArray);
                                    var lstContains = message.Where(x => x.Contains(maPc));
                                    if (lstContains == null || lstContains.Count() == 0)
                                    {
                                        message.Add(string.Format("Mã phụ cấp {0} chưa được cấu hình.", maPc));
                                        TlPhuCapFoxproNotMappingModel tlPhuCapFoxproNotMappingModel = new TlPhuCapFoxproNotMappingModel();
                                        tlPhuCapFoxproNotMappingModel.MaPhuCap = maPc;
                                        tlPhuCapFoxproNotMappingModels.Add(tlPhuCapFoxproNotMappingModel);
                                    }
                                }
                            }
                        }

                        if (property == null && (item1.UsePhuCapValue.HasValue && (bool)item1.UsePhuCapValue == false) && string.IsNullOrEmpty(item1.MapExpression))
                        {
                            var canBoPhuCap = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(item1.NewColumn));
                            if (canBoPhuCap != null)
                            {
                                canBoPhuCap.GiaTri = decimal.Parse(item[item1.OldColumn].ToString().Trim());
                            }
                        }

                        if (property == null && (item1.UsePhuCapValue.HasValue && (bool)item1.UsePhuCapValue == false) && !string.IsNullOrEmpty(item1.MapExpression))
                        {
                            var canBoPhuCap = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(item1.NewColumn));
                            if (canBoPhuCap != null)
                            {
                                var dic = new Dictionary<string, object>();
                                var lstCot = item1.MapExpression.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                                var lstCotRemove = new List<String>();
                                foreach (var item3 in lstCot)
                                {
                                    float a;
                                    if (float.TryParse(item3, out a))
                                    {
                                        if (!dic.ContainsKey(item3))
                                        {
                                            dic.Add(item3, a);
                                        }
                                    }
                                    else
                                    {
                                        if (!dic.ContainsKey(item3))
                                        {
                                            dic.Add(item3, float.Parse(item[item3].ToString().Trim() == "" ? "0" : item[item3].ToString().Trim()));
                                        }
                                    }
                                }

                                var value = decimal.Parse(item[item1.OldColumn].ToString().Trim() == "" ? "0" : item[item1.OldColumn].ToString().Trim());
                                canBoPhuCap.GiaTri = Decimal.Parse(EvalExtensions.Execute(item1.MapExpression, dic).ToString());
                            }
                        }
                    }

                    var donVi = lstDonVi.FirstOrDefault(x => x.MaDonVi == tlDmCanBo.Parent);
                    if (donVi != null)
                    {
                        tlDmCanBo.TenDonVi = donVi.TenDonVi;
                        item["TenDonVi"] = donVi.TenDonVi;
                    }

                    var capBac = _tlDmCapBacService.FindByMaCapBac(tlDmCanBo.MaCb);
                    var pcTILeHuong = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.TILE_HUONG.Equals(x.MaPhuCap));
                    if (capBac == null)
                    {
                        var pcLht = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.LHT_HS));
                        if (pcLht == null)
                            goto Save;
                        var giaTri = pcLht.GiaTri;
                        if (giaTri >= (decimal)6.8)
                        {
                            tlDmCanBo.MaCb = "223";
                        }
                        else if ((decimal)6.1 <= giaTri && giaTri < (decimal)6.8)
                        {
                            tlDmCanBo.MaCb = "222";
                        }
                        else if ((decimal)5.3 <= giaTri && giaTri < (decimal)6.1)
                        {
                            tlDmCanBo.MaCb = "221";
                        }
                        else if ((decimal)4.9 <= giaTri && giaTri < (decimal)5.3)
                        {
                            tlDmCanBo.MaCb = "214";
                        }
                        else if ((decimal)4.45 <= giaTri && giaTri < (decimal)4.9)
                        {
                            tlDmCanBo.MaCb = "213";
                        }
                        else if ((decimal)3.95 <= giaTri && giaTri < (decimal)4.45)
                        {
                            tlDmCanBo.MaCb = "212";
                        }
                        else if (giaTri < (decimal)3.95)
                        {
                            tlDmCanBo.MaCb = "211";
                        }
                    }
                    tlDmCanBo.NamTn = TinhNamThamNien(tlDmCanBo.NgayNn, tlDmCanBo.NgayXn, tlDmCanBo.NgayTn, (tlDmCanBo.ThangTnn ?? 0), tlDmCanBo.Nam, tlDmCanBo.Thang);
                    var phuCapNtn = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.NTN.Equals(x.MaPhuCap));
                    if (phuCapNtn != null)
                    {
                        phuCapNtn.GiaTri = tlDmCanBo.NamTn;
                    }

                    capBac = _tlDmCapBacService.FindByMaCapBac(tlDmCanBo.MaCb);
                    if (capBac != null)
                    {
                        if (pcTILeHuong != null)
                        {
                            pcTILeHuong.GiaTri = capBac.TiLeHuong;
                        }

                        var phuCapBhxhdvHs = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.BHXHDV_HS.Equals(x.MaPhuCap));
                        if (phuCapBhxhdvHs != null)
                        {
                            phuCapBhxhdvHs.GiaTri = capBac.BhxhCq;
                        }

                        var phuCapBhxhcnHs = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.BHXHCN_HS.Equals(x.MaPhuCap));
                        if (phuCapBhxhcnHs != null)
                        {
                            phuCapBhxhcnHs.GiaTri = capBac.HsBhxh;
                        }

                        var phuCapBhytdvHs = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.BHYTDV_HS.Equals(x.MaPhuCap));
                        if (phuCapBhytdvHs != null)
                        {
                            phuCapBhytdvHs.GiaTri = capBac.BhytCq;
                        }

                        var phuCapBhytcnHs = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.BHYTCN_HS.Equals(x.MaPhuCap));
                        if (phuCapBhytcnHs != null)
                        {
                            phuCapBhytcnHs.GiaTri = capBac.HsBhyt;
                        }

                        var phuCapBhtndvHs = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.BHTNDV_HS.Equals(x.MaPhuCap));
                        if (phuCapBhtndvHs != null)
                        {
                            phuCapBhtndvHs.GiaTri = capBac.BhtnCq;
                        }

                        var phuCapBhtncnHs = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.BHTNCN_HS.Equals(x.MaPhuCap));
                        if (phuCapBhtncnHs != null)
                        {
                            phuCapBhtncnHs.GiaTri = capBac.HsBhtn;
                        }

                        var pcLht = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.LHT_HS.Equals(x.MaPhuCap));
                        if (pcLht != null)
                        {
                            tlDmCanBo.HeSoLuong = pcLht.GiaTri;
                        }
                    }
                    else
                    {
                        if (pcTILeHuong != null)
                        {
                            pcTILeHuong.GiaTri = 1;
                        }
                    }

                    var pcBhtn = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.Nop_BHTN.Equals(x.MaPhuCap));
                    if (pcBhtn != null)
                    {
                        pcBhtn.GiaTri = tlDmCanBo.BHTN == true ? 1 : 0;
                    }

                    var pcPccov = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.Huong_PCCOV.Equals(x.MaPhuCap));
                    if (pcPccov != null)
                    {
                        pcPccov.GiaTri = tlDmCanBo.PCCV == true ? 1 : 0;
                    }

                    var pccv = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.PCCV_HS.Equals(x.MaPhuCap));
                    if (pccv != null)
                    {
                        var cv = _tlDmChucVuService.FindByHeSoChucVu(pccv.GiaTri);
                        if (cv != null)
                        {
                            tlDmCanBo.MaCv = cv.MaCv;
                        }
                    }

                    if (string.IsNullOrEmpty(tlDmCanBo.SoTaiKhoan))
                    {
                        tlDmCanBo.Tm = false;
                    }
                    else
                    {
                        tlDmCanBo.Tm = true;
                    }

                    var pcTm = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.TM.Equals(x.MaPhuCap));
                    if (pcTm != null)
                    {
                        pcTm.GiaTri = tlDmCanBo.Tm == false ? 1 : 0;
                    }

                    if (tlDmCanBo.MaCb.StartsWith("0"))
                    {
                        var pctnHs = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCTN_HS);
                        if (pctnHs != null)
                        {
                            pctnHs.GiaTri = 0;
                        }
                    }
                    else
                    {
                        var pcTemThuTt = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCTEMTHU_TT);
                        if (pcTemThuTt != null)
                        {
                            pcTemThuTt.GiaTri = 0;
                        }
                    }

                    //if (!tlDmCanBo.MaCb.StartsWith("0") || (tlDmCanBo.MaCb.StartsWith("0") && tlDmCanBo.NgayXn == null))
                    //{
                    //    var tcViecLam = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == PhuCap.TCVIECLAM_TT);
                    //    if (tcViecLam != null)
                    //    {
                    //        tcViecLam.GiaTri = 0;
                    //    }
                    //}

                    var phuCapViecLam = tlCanBoPhuCapModels.FirstOrDefault(pc => pc.MaCbo == tlDmCanBo.MaCanBo && pc.MaPhuCap == PhuCap.THANG_TCVIECLAM);

                    if (phuCapViecLam != null && tlDmCanBo.NgayXn == null)
                    {
                        phuCapViecLam.GiaTri = 0;
                    }

                    if (!tlDmCanBo.MaCb.StartsWith("0") || tlDmCanBo.IsNam == true)
                    {
                        var pcnu = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCNU_HS);
                        if (pcnu != null)
                        {
                            pcnu.GiaTri = 0;
                        }
                    }

                    var pcanqp = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCANQP_HS);
                    if (pcanqp != null && tlDmCanBo.MaCb.Equals("415"))
                    {
                        pcanqp.GiaTri = (decimal?)0.5;
                    }
                    else if (pcanqp != null && tlDmCanBo.MaCb.Equals("413"))
                    {
                        pcanqp.GiaTri = (decimal?)0.3;
                    }

                    var lstHsTruyLinh = new List<string>() { PhuCap.LHT_HS, PhuCap.PCCV_HS, PhuCap.PCTHUHUT_HS, PhuCap.PCCOV_HS, PhuCap.PCCU_HS };
                    foreach (var item2 in lstHsTruyLinh)
                    {
                        var pc = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == item2);
                        var pcTruyLinh = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == string.Format("{0}{1}", item2, "_CU"));
                        if (pc != null && pcTruyLinh != null)
                        {
                            pcTruyLinh.GiaTri = pc.GiaTri;
                        }
                    }

                    if (lstCanBo != null && lstCanBo.Count() > 0)
                    {
                        var lstCanBoKhacThang = lstCanBo.Where(x => x.SoSoLuong.Equals(tlDmCanBo.SoSoLuong)
                            && x.Parent.Equals(tlDmCanBo.Parent)
                            && x.TenCanBo.Equals(tlDmCanBo.TenCanBo));
                        if (lstCanBoKhacThang != null && lstCanBoKhacThang.Count() > 0)
                        {
                            var canBo = lstCanBoKhacThang.FirstOrDefault(x => x.SoSoLuong.Equals(tlDmCanBo.SoSoLuong) && x.Thang == tlDmCanBo.Thang && x.Nam == tlDmCanBo.Nam);
                            if (canBo != null)
                            {
                                --maHieuCanBoMax;
                                continue;
                            }
                            else
                            {
                                --maHieuCanBoMax;
                                tlDmCanBo.MaHieuCanBo = lstCanBoKhacThang.FirstOrDefault().MaHieuCanBo;
                                tlDmCanBo.MaCanBo = Year + Month.ToString("D2") + tlDmCanBo.MaHieuCanBo;
                                foreach (var item3 in tlCanBoPhuCapModels)
                                {
                                    item3.MaCbo = tlDmCanBo.MaCanBo;
                                }
                            }
                        }
                    }

                Save:
                    lstImportCanBo.Add(tlDmCanBo);
                    lstImportPhuCapCanBo.AddRange(tlCanBoPhuCapModels);
                }

                foreach (DataRow item in DtChuaMap.Rows)
                {
                    var donVi = lstDonVi.FirstOrDefault(x => x.MaDonVi == item.Field<string>("DVI").Trim());
                    if (donVi != null)
                    {
                        item["TenDonVi"] = donVi.TenDonVi;
                    }
                }

                warningMessage = string.Join(Environment.NewLine, message);
                lstCadres = _mapper.Map<ObservableCollection<CadresModel>>(lstImportCanBo);
                lstCanBoPhuCap = _mapper.Map<ObservableCollection<TlCanBoPhuCapModel>>(lstImportPhuCapCanBo);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void SaveCanBoBienPhong()
        {
            try
            {
                var lstDmMapPcDetail = _tlDmMapPcDetailService.FindAll();
                var lstPhuCap = _tlDmPhuCapService.FindAll();
                var lstCanBo = _cadresService.FindAllState();
                var lstCapBac = _tlDmCapBacService.FindAll();

                IList<string> message = new List<string>();

                var lstImportCanBo = new List<TlDmCanBo>();
                var lstImportPhuCapCanBo = new List<TlCanBoPhuCap>();

                int maHieuCanBoMax;
                if (lstCanBo.Count() > 0 && lstCanBo != null)
                {
                    maHieuCanBoMax = lstCanBo.Max(x => int.Parse(x.MaHieuCanBo));
                }
                else
                {
                    maHieuCanBoMax = 0;
                }

                var lstColumn = DtDoiTuong.Columns;
                DtDoiTuong.Columns.Add("TenDonVi", typeof(string));
                DtChuaMap = DtDoiTuong.Clone();
                var columnKy = DtDoiTuong.Columns.Cast<DataColumn>().Where(x => x.ColumnName.StartsWith("KY")).Select(x => x.ColumnName).ToList();

                CultureInfo c = new CultureInfo("vi-VN");
                c.NumberFormat.NumberDecimalSeparator = ",";
                c.NumberFormat.NumberGroupSeparator = ".";

                foreach (DataRow item in DtDoiTuong.Rows)
                {
                    TlDmCanBo tlDmCanBo = new TlDmCanBo();
                    tlDmCanBo.Thang = Month;
                    tlDmCanBo.Nam = Year;
                    tlDmCanBo.IsDelete = true;
                    tlDmCanBo.BHTN = false;
                    tlDmCanBo.PCCV = true;
                    tlDmCanBo.KhongLuong = false;
                    tlDmCanBo.BNuocNgoai = false;
                    tlDmCanBo.Tm = false;
                    tlDmCanBo.IsLock = false;
                    tlDmCanBo.MaHieuCanBo = (++maHieuCanBoMax).ToString();
                    tlDmCanBo.MaCanBo = Year + Month.ToString("D2") + tlDmCanBo.MaHieuCanBo;
                    tlDmCanBo.IsNam = true;

                    var tlCanBoPhuCapModels = new List<TlCanBoPhuCap>();

                    foreach (var item2 in lstPhuCap)
                    {
                        TlCanBoPhuCap tlCanBoPhuCap = new TlCanBoPhuCap();
                        tlCanBoPhuCap.MaCbo = tlDmCanBo.MaCanBo;
                        tlCanBoPhuCap.MaPhuCap = item2.MaPhuCap;
                        tlCanBoPhuCap.GiaTri = item2.GiaTri;
                        tlCanBoPhuCap.HuongPcSn = item2.HuongPCSN;
                        tlCanBoPhuCap.BSaoChep = item2.BSaoChep;
                        tlCanBoPhuCap.Flag = false;
                        tlCanBoPhuCapModels.Add(tlCanBoPhuCap);
                    }

                    tlDmCanBo.Parent = item.Field<string>("MADV").Trim();
                    tlDmCanBo.SoSoLuong = item.Field<string>("MATHUE").Trim();
                    tlDmCanBo.TenCanBo = item.Field<string>("HOVATEN").Trim();
                    tlDmCanBo.SoTaiKhoan = item.Field<string>("TAIKHOAN").Trim();
                    var ngayNn = item.Field<string>("NHAPNGU").Trim();
                    tlDmCanBo.NgayNn = new DateTime(int.Parse(ngayNn.Substring(3, 4)), int.Parse(ngayNn.Substring(0, 2)), 1);
                    tlDmCanBo.ThangTnn = 0;
                    tlDmCanBo.NamTn = TinhNamThamNien(tlDmCanBo.NgayNn, tlDmCanBo.NgayXn, tlDmCanBo.NgayTn, tlDmCanBo.ThangTnn, tlDmCanBo.Nam, tlDmCanBo.Thang);

                    var donVi = lstDonVi.FirstOrDefault(x => x.MaDonVi == tlDmCanBo.Parent);
                    if (donVi != null)
                    {
                        tlDmCanBo.TenDonVi = donVi.TenDonVi;
                    }

                    var loaiCb = item.Field<string>("LOAICAB");
                    var capBacStr = item.Field<string>("CAPBAC");
                    tlDmCanBo.HeSoLuong = decimal.Parse(item["HSLUONG"].ToString().Trim(), c);
                    Regex rxMaCapBac = new Regex("^q");

                    if ("sq".Equals(loaiCb) || "sq".Equals(capBacStr))
                    {
                        var capBacCb = lstCapBac.Where(x => "1".Equals(x.Parent) && x.LhtHs == tlDmCanBo.HeSoLuong).OrderBy(x => x.MaCb);
                        if (capBacCb != null && capBacCb.Count() > 0)
                        {
                            decimal value;
                            bool isParse = decimal.TryParse(item["HSBLUU"].ToString().Trim(), NumberStyles.Any, c, out value);
                            if ((!isParse) || (isParse && value == 0))
                            {
                                tlDmCanBo.MaCb = capBacCb.FirstOrDefault().MaCb;
                            }
                            else if (isParse && value > 0)
                            {
                                tlDmCanBo.MaCb = capBacCb.LastOrDefault().MaCb;
                            }
                        }
                    }
                    else if ("s2".Equals(loaiCb) || "s2".Equals(capBacStr))
                    {
                        var capBacCb = lstCapBac.Where(x => "1".Equals(x.Parent) && x.LhtHs == tlDmCanBo.HeSoLuong).OrderBy(x => x.MaCb);
                        if (capBacCb != null && capBacCb.Count() > 0)
                        {
                            tlDmCanBo.MaCb = capBacCb.FirstOrDefault().MaCb;
                        }
                    }
                    else if ("cn".Equals(loaiCb) || "cn".Equals(capBacStr) || "oc".Equals(loaiCb) || "oc".Equals(capBacStr) || "vc".Equals(loaiCb) || "vc".Equals(capBacStr))
                    {
                        if (!string.IsNullOrEmpty(item["HSANQP"].ToString().Trim()))
                        {
                            decimal value;
                            bool isParse = decimal.TryParse(item["HSANQP"].ToString().Trim(), NumberStyles.Any, c, out value);
                            if (isParse && value == (decimal)0.3)
                            {
                                tlDmCanBo.MaCb = "413";
                            }
                            else if (isParse && value == (decimal)0.5)
                            {
                                tlDmCanBo.MaCb = "415";
                            }
                        }
                    }
                    else if ("ct".Equals(loaiCb) || "ct".Equals(capBacStr))
                    {
                        tlDmCanBo.MaCb = "43";
                    }
                    else if ("cd".Equals(loaiCb) || "cd".Equals(capBacStr))
                    {
                        tlDmCanBo.MaCb = "423";
                    }
                    else if ("hc".Equals(loaiCb) || "hc".Equals(capBacStr) || "hn".Equals(loaiCb) || "hn".Equals(capBacStr) || "ht".Equals(loaiCb) || "ht".Equals(capBacStr) || "hu".Equals(loaiCb) || "hu".Equals(capBacStr))
                    {
                        var capBacCb = lstCapBac.FirstOrDefault(x => "4".Equals(x.Parent) && x.LhtHs == tlDmCanBo.HeSoLuong);
                        if (capBacCb != null)
                        {
                            tlDmCanBo.MaCb = capBacCb.MaCb;
                        }
                    }
                    else
                    {
                        switch (loaiCb)
                        {
                            case "q1":
                                tlDmCanBo.MaCb = "211";
                                break;
                            case "q2":
                                tlDmCanBo.MaCb = "212";
                                break;
                            case "q3":
                                tlDmCanBo.MaCb = "213";
                                break;
                            case "q4":
                                tlDmCanBo.MaCb = "214";
                                break;
                            case "q5":
                                tlDmCanBo.MaCb = "221";
                                break;
                            case "q6":
                                tlDmCanBo.MaCb = "222";
                                break;
                            default:
                                if (rxMaCapBac.IsMatch(loaiCb) || rxMaCapBac.IsMatch(capBacStr))
                                {
                                    var giaTri = tlDmCanBo.HeSoLuong;
                                    if (giaTri >= (decimal)6.8)
                                    {
                                        tlDmCanBo.MaCb = "223";
                                    }
                                    else if ((decimal)6.1 <= giaTri && giaTri < (decimal)6.8)
                                    {
                                        tlDmCanBo.MaCb = "222";
                                    }
                                    else if ((decimal)5.3 <= giaTri && giaTri < (decimal)6.1)
                                    {
                                        tlDmCanBo.MaCb = "221";
                                    }
                                    else if ((decimal)4.9 <= giaTri && giaTri < (decimal)5.3)
                                    {
                                        tlDmCanBo.MaCb = "214";
                                    }
                                    else if ((decimal)4.45 <= giaTri && giaTri < (decimal)4.9)
                                    {
                                        tlDmCanBo.MaCb = "213";
                                    }
                                    else if ((decimal)3.95 <= giaTri && giaTri < (decimal)4.45)
                                    {
                                        tlDmCanBo.MaCb = "212";
                                    }
                                    else if (giaTri < (decimal)3.95)
                                    {
                                        tlDmCanBo.MaCb = "211";
                                    }
                                }
                                break;
                        }
                    }

                    #region hardcode
                    var pcThd = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.PCTHD_HS));
                    if (pcThd != null && !string.IsNullOrEmpty(item["TRENHD"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["TRENHD"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            pcThd.GiaTri = value;
                        }
                    }

                    var pcLht = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.LHT_HS));
                    if (pcLht != null && !string.IsNullOrEmpty(item["HSLUONG"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["HSLUONG"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            pcLht.GiaTri = value;
                        }
                    }

                    var pctnvk = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.PCTNVK_HS));
                    if (pctnvk != null && !string.IsNullOrEmpty(item["VKHUNG"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["VKHUNG"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            pctnvk.GiaTri = value;
                        }
                    }

                    var hsbl = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.HSBL_HS));
                    if (hsbl != null && !string.IsNullOrEmpty(item["HSBLUU"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["HSBLUU"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            hsbl.GiaTri = value;
                        }
                    }

                    var pccvHs = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.PCCV_HS));
                    if (pccvHs != null && !string.IsNullOrEmpty(item["HSLDAO"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["HSLDAO"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            pccvHs.GiaTri = value;
                        }
                    }

                    var pckvHs = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.PCKV_HS));
                    if (pckvHs != null && !string.IsNullOrEmpty(item["HSKVUC"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["HSKVUC"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            pckvHs.GiaTri = value;
                        }
                    }

                    var pcanqpHs = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.PCANQP_HS));
                    if (pcanqpHs != null && !string.IsNullOrEmpty(item["HSANQP"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["HSANQP"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            pcanqpHs.GiaTri = value;
                        }
                    }

                    var pcdhHs = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.PC8_HS));
                    if (pcdhHs != null && !string.IsNullOrEmpty(item["HSDHAI"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["HSDHAI"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            pcdhHs.GiaTri = value;
                        }
                    }

                    var pckieHs = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.PCKIE_HS));
                    if (pckieHs != null && !string.IsNullOrEmpty(item["HSKIEM"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["HSKIEM"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            pckieHs.GiaTri = value;
                        }
                    }

                    var pcthuhutHs = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.PCTHUHUT_HS));
                    if (pcthuhutHs != null && !string.IsNullOrEmpty(item["HSTHUT"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["HSTHUT"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            pcthuhutHs.GiaTri = value;
                        }
                    }

                    var tienan = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.TA_BB_DG));
                    if (tienan != null && !string.IsNullOrEmpty(item["TIENAN"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["TIENAN"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            tienan.GiaTri = value;
                        }
                    }

                    var gtkhacTt = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.GTKHAC_TT));
                    if (gtkhacTt != null && !string.IsNullOrEmpty(item["TRUKHAC"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["TRUKHAC"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            gtkhacTt.GiaTri = value;
                        }
                    }

                    var giamThue = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.GIAMTHUE_TT));
                    if (giamThue != null && !string.IsNullOrEmpty(item["GIATHUE"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["GIATHUE"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            giamThue.GiaTri = value;
                        }
                    }

                    var gtptSn = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.GTPT_SN));
                    if (gtptSn != null && !string.IsNullOrEmpty(item["SOTHEO"].ToString().Trim()))
                    {
                        decimal value;
                        bool isParse = decimal.TryParse(item["SOTHEO"].ToString().Trim(), NumberStyles.Any, c, out value);
                        if (isParse)
                        {
                            gtptSn.GiaTri = value;
                        }
                    }

                    foreach (var column in columnKy)
                    {
                        var pcMap = lstDmMapPcDetail.FirstOrDefault(x => x.OldValue.Equals(item[column].ToString().Trim()));
                        if (pcMap != null)
                        {
                            var pc = tlCanBoPhuCapModels.FirstOrDefault(x => pcMap.MaPhuCap.Equals(x.MaPhuCap));
                            if (pc != null)
                            {
                                pc.GiaTri = pcMap.Giatri;
                            }
                        }
                        else if (pcMap == null && !string.IsNullOrEmpty(item[column].ToString().Trim()))
                        {
                            var lstContains = message.Where(x => x.Contains(item[column].ToString().Trim()));
                            if (lstContains == null || lstContains.Count() == 0)
                            {
                                message.Add(string.Format("Mã phụ cấp {0} chưa được cấu hình.", item[column].ToString().Trim()));
                                TlPhuCapFoxproNotMappingModel tlPhuCapFoxproNotMappingModel = new TlPhuCapFoxproNotMappingModel();
                                tlPhuCapFoxproNotMappingModel.MaPhuCap = item[column].ToString();
                                tlPhuCapFoxproNotMappingModels.Add(tlPhuCapFoxproNotMappingModel);
                            }
                        }
                    }

                    var phuCapNtn = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.NTN.Equals(x.MaPhuCap));
                    if (phuCapNtn != null)
                    {
                        phuCapNtn.GiaTri = tlDmCanBo.NamTn;
                    }

                    var capBac = _tlDmCapBacService.FindByMaCapBac(tlDmCanBo.MaCb);
                    var pcTILeHuong = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.TILE_HUONG.Equals(x.MaPhuCap));
                    if (capBac != null)
                    {
                        if (pcTILeHuong != null)
                        {
                            pcTILeHuong.GiaTri = capBac.TiLeHuong;
                        }

                        var phuCapBhxhdvHs = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.BHXHDV_HS.Equals(x.MaPhuCap));
                        if (phuCapBhxhdvHs != null)
                        {
                            phuCapBhxhdvHs.GiaTri = capBac.BhxhCq;
                        }

                        var phuCapBhxhcnHs = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.BHXHCN_HS.Equals(x.MaPhuCap));
                        if (phuCapBhxhcnHs != null)
                        {
                            phuCapBhxhcnHs.GiaTri = capBac.HsBhxh;
                        }

                        var phuCapBhytdvHs = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.BHYTDV_HS.Equals(x.MaPhuCap));
                        if (phuCapBhytdvHs != null)
                        {
                            phuCapBhytdvHs.GiaTri = capBac.BhytCq;
                        }

                        var phuCapBhytcnHs = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.BHYTCN_HS.Equals(x.MaPhuCap));
                        if (phuCapBhytcnHs != null)
                        {
                            phuCapBhytcnHs.GiaTri = capBac.HsBhyt;
                        }

                        var phuCapBhtndvHs = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.BHTNDV_HS.Equals(x.MaPhuCap));
                        if (phuCapBhtndvHs != null)
                        {
                            phuCapBhtndvHs.GiaTri = capBac.BhtnCq;
                        }

                        var phuCapBhtncnHs = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.BHTNCN_HS.Equals(x.MaPhuCap));
                        if (phuCapBhtncnHs != null)
                        {
                            phuCapBhtncnHs.GiaTri = capBac.HsBhtn;
                        }
                    }
                    else
                    {
                        if (pcTILeHuong != null)
                        {
                            pcTILeHuong.GiaTri = 1;
                        }
                    }

                    var pcBhtn = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.Nop_BHTN.Equals(x.MaPhuCap));
                    if (pcBhtn != null)
                    {
                        pcBhtn.GiaTri = tlDmCanBo.BHTN == true ? 1 : 0;
                    }

                    var pcPccov = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.Huong_PCCOV.Equals(x.MaPhuCap));
                    if (pcPccov != null)
                    {
                        pcPccov.GiaTri = tlDmCanBo.PCCV == true ? 1 : 0;
                    }

                    var pccv = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.PCCV_HS.Equals(x.MaPhuCap));
                    if (pccv != null)
                    {
                        var cv = _tlDmChucVuService.FindByHeSoChucVu(pccv.GiaTri);
                        if (cv != null)
                        {
                            tlDmCanBo.MaCv = cv.MaCv;
                        }
                    }

                    if (string.IsNullOrEmpty(tlDmCanBo.SoTaiKhoan))
                    {
                        tlDmCanBo.Tm = false;
                    }
                    else
                    {
                        tlDmCanBo.Tm = true;
                    }

                    var pcTm = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.TM.Equals(x.MaPhuCap));
                    if (pcTm != null)
                    {
                        pcTm.GiaTri = tlDmCanBo.Tm == false ? 1 : 0;
                    }

                    if (!string.IsNullOrEmpty(tlDmCanBo.MaCb))
                    {
                        if (tlDmCanBo.MaCb.StartsWith("0"))
                        {
                            var pctnHs = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCTN_HS);
                            if (pctnHs != null)
                            {
                                pctnHs.GiaTri = 0;
                            }
                        }
                        else
                        {
                            var pcTemThuTt = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCTEMTHU_TT);
                            if (pcTemThuTt != null)
                            {
                                pcTemThuTt.GiaTri = 0;
                            }
                        }

                        //if (!tlDmCanBo.MaCb.StartsWith("0") || (tlDmCanBo.MaCb.StartsWith("0") && tlDmCanBo.NgayXn == null))
                        //{
                        //    var tcViecLam = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == PhuCap.TCVIECLAM_TT);
                        //    if (tcViecLam != null)
                        //    {
                        //        tcViecLam.GiaTri = 0;
                        //    }
                        //}
                        var phuCapViecLam = tlCanBoPhuCapModels.FirstOrDefault(pc => pc.MaCbo == tlDmCanBo.MaCanBo && pc.MaPhuCap == PhuCap.THANG_TCVIECLAM);

                        if (phuCapViecLam != null && tlDmCanBo.NgayXn == null)
                        {
                            phuCapViecLam.GiaTri = 0;
                        }

                        if (!tlDmCanBo.MaCb.StartsWith("0") || tlDmCanBo.IsNam == true)
                        {
                            var pcnu = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == PhuCap.PCNU_HS);
                            if (pcnu != null)
                            {
                                pcnu.GiaTri = 0;
                            }
                        }
                    }

                    var lstHsTruyLinh = new List<string>() { PhuCap.LHT_HS, PhuCap.PCCV_HS, PhuCap.PCTHUHUT_HS, PhuCap.PCCOV_HS, PhuCap.PCCU_HS };
                    foreach (var item2 in lstHsTruyLinh)
                    {
                        var pc = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == item2);
                        var pcTruyLinh = tlCanBoPhuCapModels.FirstOrDefault(x => x.MaPhuCap == string.Format("{0}{1}", item2, "_CU"));
                        if (pc != null && pcTruyLinh != null)
                        {
                            pcTruyLinh.GiaTri = pc.GiaTri;
                        }
                    }
                    #endregion
                    if (lstCanBo != null && lstCanBo.Count() > 0)
                    {
                        var lstCanBoKhacThang = lstCanBo.Where(x => x.SoSoLuong.Equals(tlDmCanBo.SoSoLuong)
                            && x.Parent.Equals(tlDmCanBo.Parent)
                            && x.TenCanBo.Equals(tlDmCanBo.TenCanBo));
                        if (lstCanBoKhacThang != null && lstCanBoKhacThang.Count() > 0)
                        {
                            var canBo = lstCanBoKhacThang.FirstOrDefault(x => x.SoSoLuong.Equals(tlDmCanBo.SoSoLuong) && x.Thang == tlDmCanBo.Thang && x.Nam == tlDmCanBo.Nam);
                            if (canBo != null)
                            {
                                --maHieuCanBoMax;
                                continue;
                            }
                            else
                            {
                                --maHieuCanBoMax;
                                tlDmCanBo.MaHieuCanBo = lstCanBoKhacThang.FirstOrDefault().MaHieuCanBo;
                                tlDmCanBo.MaCanBo = Year + Month.ToString("D2") + tlDmCanBo.MaHieuCanBo;
                                foreach (var item3 in tlCanBoPhuCapModels)
                                {
                                    item3.MaCbo = tlDmCanBo.MaCanBo;
                                }
                            }
                        }
                    }

                    Save:
                    lstImportCanBo.Add(tlDmCanBo);
                    lstImportPhuCapCanBo.AddRange(tlCanBoPhuCapModels);
                }

                warningMessage = string.Join(Environment.NewLine, message);
                lstCadres = _mapper.Map<ObservableCollection<CadresModel>>(lstImportCanBo);
                lstCanBoPhuCap = _mapper.Map<ObservableCollection<TlCanBoPhuCapModel>>(lstImportPhuCapCanBo);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private int TinhNamThamNien(DateTime? ngayNn, DateTime? ngayXn, DateTime? ngayTn, int? thangTnn, int? nam, int? thang)
        {
            return DateUtils.TinhNamThamNien(ngayNn, ngayXn, ngayTn, thangTnn.GetValueOrDefault(0), thang.GetValueOrDefault(0), nam.GetValueOrDefault(0));
        }

        private DateTime? ReCalculateDateTime(string oldColumn, DataRow dataRow)
        {
            try
            {
                string s1 = dataRow[oldColumn].ToString().Trim();
                if (s1.Length > 2)
                {
                    string year = s1.Substring(s1.Length - 2, 2).Trim();
                    string month = s1.Substring(0, s1.Length - 2).Trim();
                    int iyear = int.Parse(year);
                    if (iyear < 40)
                        iyear = 2000 + iyear;
                    else if (iyear >= 40)
                        iyear = 1900 + iyear;
                    DateTime dateTime = new DateTime(iyear, int.Parse(month), 1);
                    return dateTime;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }

        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
            if (string.IsNullOrEmpty(DonViPath) || string.IsNullOrEmpty(DoiTuongPath))
            {
                messages.Add(string.Format("Đồng chí nhập thiếu file."));
            }
            return string.Join(Environment.NewLine, messages);
        }
    }
}
