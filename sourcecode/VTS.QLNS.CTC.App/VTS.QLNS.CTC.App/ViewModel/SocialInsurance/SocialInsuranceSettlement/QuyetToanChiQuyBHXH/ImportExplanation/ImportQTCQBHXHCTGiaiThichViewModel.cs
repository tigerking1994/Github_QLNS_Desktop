using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.ImportExplanation;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Command;
using System.Windows.Forms;
using System.Windows;
using VTS.QLNS.CTC.App.Properties;
using FlexCel.XlsAdapter;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.ImportExplanation
{
    public class ImportQTCQBHXHCTGiaiThichViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly IQtcqBHXHService _chungTuService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IQtcqBHXHChiTietService _chungTuChiTietService;
        private readonly IBhQtcqCtctGtTroCapService _iBhQtcqCtctGtTroCapService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private List<ImportErrorItem> _lstErrQtcqBHXH = new List<ImportErrorItem>();
        private List<ImportErrorItem> _lstErrExplainBHXH = new List<ImportErrorItem>();
        public override string Name => "Import giải thích chi quý các chế độ BHXH";
        public override string Description => "Import giải thích chi quý các chế độ BHXH";
        public override Type ContentType => typeof(ImportQTCQBHXHCTGiaiThich);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        private Guid _iDBhQtcqBHXH;
        public Guid IdBhQtcqBHXH
        {
            get => _iDBhQtcqBHXH;
            set => SetProperty(ref _iDBhQtcqBHXH, value);
        }
        public string IIdMaDonVi { get; set; }
        public int IQuy { get; set; }

        public string SSoChungTu { get; set; }
        private QtcqTcGiaiThichDetailImportModel _selectedExplainItem;
        public QtcqTcGiaiThichDetailImportModel SelectedExplainItem
        {
            get => _selectedExplainItem;
            set => SetProperty(ref _selectedExplainItem, value);
        }
        private ObservableCollection<QtcqTcGiaiThichDetailImportModel> _explainItems;
        public ObservableCollection<QtcqTcGiaiThichDetailImportModel> ExplainItems
        {
            get => _explainItems;
            set
            {
                SetProperty(ref _explainItems, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }
        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get
            {
                _isSaveData = ExplainItems.Any() && !_lstErrExplainBHXH.Any();
                return _isSaveData;
            }
            set
            {
                SetProperty(ref _isSaveData, value);
            }
        }
        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }
        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand DownloadTemplateCommand { get; }
        public RelayCommand HandleDataCommand { get; }
        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }
        public ImportQTCQBHXHCTGiaiThichViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            IQtcqBHXHService qttBHXHService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IQtcqBHXHChiTietService qttBHXHChiTietService,
            IBhQtcqCtctGtTroCapService iBhQtcqCtctGtTroCapService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _chungTuService = qttBHXHService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _chungTuChiTietService = qttBHXHChiTietService;
            _iBhQtcqCtctGtTroCapService = iBhQtcqCtctGtTroCapService;
            _logger = logger;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            SaveCommand = new RelayCommand(obj => OnSaveData(obj));
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(ShowError);
            HandleDataCommand = new RelayCommand(obj => HandleData());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            OnResetData();
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _lstErrExplainBHXH.Clear();
            _tabIndex = ImportTabIndex.Data;
            _explainItems = new ObservableCollection<Model.Import.QtcqTcGiaiThichDetailImportModel>();
            OnPropertyChanged(nameof(TabIndex));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(ExplainItems));
            OnPropertyChanged(nameof(IsSaveData));
        }
        private void HandleData()
        {
            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;
                var dataExplainImport = _importService.ProcessData<QtcqTcGiaiThichDetailImportModel>(FilePath);
                var explainImportModels = new ObservableCollection<QtcqTcGiaiThichDetailImportModel>(dataExplainImport.Data);
                List<string> lstError = new List<string>();

                if (dataExplainImport.ImportErrors.Count > 0)
                {
                    _lstErrExplainBHXH.AddRange(dataExplainImport.ImportErrors);
                }

                if (explainImportModels == null || explainImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.CanBoCheDoImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(FilePath))
                {
                    lstError.Add(Resources.ErrorFileEmpty);
                }
                if (lstError.Any() || explainImportModels.Any(x => !x.ImportStatus))
                {
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ExplainItems = explainImportModels;

                foreach (var item in ExplainItems)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(QtcqTcGiaiThichDetailImportModel.SXauNoiMa))
                        {
                            var entityDetail = (QtcqTcGiaiThichDetailImportModel)sender;
                            var rowIndex = ExplainItems.IndexOf(entityDetail);
                            var listError = _importService.ValidateItem(entityDetail, rowIndex);
                            if (listError.Count > 0)
                            {
                                var messageOfRow = listError.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToList();
                                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, messageOfRow), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                                _lstErrExplainBHXH.AddRange(listError);
                                entityDetail.ImportStatus = false;
                                if (listError.Any(x => x.IsErrorMLNS))
                                {
                                    entityDetail.IsError = true;
                                }
                            }
                            else
                            {
                                entityDetail.ImportStatus = true;
                                entityDetail.IsError = false;
                                _lstErrExplainBHXH = _lstErrExplainBHXH.Where(x => x.Row != rowIndex).ToList();
                            }
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                FilePath = "";
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
            }
        }

        private List<ImportErrorItem> ValidateExplainItem(QtcqExplainImportModel item, int rowIndex)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                var lstXauNoiMaMlns = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(_sessionInfo.YearOfWork, LNSValue.LNS_9010001_9010002)
                    .Select(x => x.SXauNoiMa).ToList();
                if (!lstXauNoiMaMlns.Contains(item.SXauNoiMa))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Xâu nối mã",
                        Error = string.Format(Resources.MsgXauNoiMaKhongTonTai),
                        Row = rowIndex - 1
                    });
                }

                return errors;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new List<ImportErrorItem>();
            }
        }

        private void ShowError(object obj)
        {
            int rowExpIndex = ExplainItems.IndexOf(SelectedExplainItem);
            List<string> expErrors = _lstErrExplainBHXH.Where(x => x.Row == rowExpIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string expMessage = string.Join(Environment.NewLine, expErrors);
            System.Windows.MessageBox.Show(expMessage, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnSaveData(object obj)
        {
            var explainImport = _explainItems.Where(x => x.ImportStatus);
            var chungTu = _chungTuService.FindById(IdBhQtcqBHXH);
            var lstUpdateChungTuDetail = _chungTuChiTietService.FindByCondition(x => x.IdQTCQuyCheDoBHXH == IdBhQtcqBHXH);
            var lstXauNoiMaMLNS = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(_sessionInfo.YearOfWork, LNSValue.LNS_9010001_9010002).ToList();
            List<BhQtcqBHXHChiTiet> lstAddChungTuChiTiet = new List<BhQtcqBHXHChiTiet>();
            List<BhQtcqCtctGtTroCap> lstTroCap = new List<BhQtcqCtctGtTroCap>();
            foreach (var item in explainImport)
            {
                BhQtcqCtctGtTroCap bhQtcqCtctGtTroCap = new BhQtcqCtctGtTroCap();
                bhQtcqCtctGtTroCap.IID_QTC_Quy_ChungTu = IdBhQtcqBHXH;
                bhQtcqCtctGtTroCap.IQuy = IQuy;
                bhQtcqCtctGtTroCap.ID_MaDonVi = IIdMaDonVi;
                bhQtcqCtctGtTroCap.DNgayTao = DateTime.Now;
                bhQtcqCtctGtTroCap.INamLamViec = _sessionInfo.YearOfWork;
                bhQtcqCtctGtTroCap.SNguoiTao = _sessionInfo.Principal;
                bhQtcqCtctGtTroCap.SMaHieuCanBo = IIdMaDonVi + "_" + SSoChungTu + "_" + item.SMaHieuCanBo;
                bhQtcqCtctGtTroCap.STenCanBo = item.STenCanBo;
                bhQtcqCtctGtTroCap.SMaCapBac = item.SMaCapBac;
                bhQtcqCtctGtTroCap.ID_MaPhanHo = null;
                bhQtcqCtctGtTroCap.ISoNgayHuong = string.IsNullOrEmpty(item.ISoNgayHuong) ? 0 : int.Parse(item.ISoNgayHuong);
                bhQtcqCtctGtTroCap.SSoQuyetDinh = item.SSoQuyetDinh;
                bhQtcqCtctGtTroCap.DNgayQuyetDinh = ConvertToDateTime(item.DNgayQuyetDinh);
                bhQtcqCtctGtTroCap.FSoTien = ConvertStringToNumber<Double>(item.FSoTien);
                bhQtcqCtctGtTroCap.SXauNoiMa = item.SXauNoiMa;
                bhQtcqCtctGtTroCap.STenPhanHo = item.STenPhanHo;
                bhQtcqCtctGtTroCap.STenCapBac = item.STenCapBac;
                bhQtcqCtctGtTroCap.SSoSoBHXH = item.SMaCheDo;
                bhQtcqCtctGtTroCap.DTuNgay = ConvertToDateTime(item.DTuNgay);
                bhQtcqCtctGtTroCap.DDenNgay = ConvertToDateTime(item.DDenNgay);
                bhQtcqCtctGtTroCap.FTienLuongThangDongBHXH = ConvertStringToNumber<Double>(item.FTienLuongThangDongBHXH);
                bhQtcqCtctGtTroCap.ISoNgayTruyLinh = ConvertStringToNumber<int>(item.ISoNgayTruyLinh.Replace(".", string.Empty));
                bhQtcqCtctGtTroCap.FTienTruyLinh = ConvertStringToNumber<Double>(item.FTienTruyLinh);
                lstTroCap.Add(bhQtcqCtctGtTroCap);
            }

            foreach (var SXauNoiMa in lstTroCap.Select(x => x.SXauNoiMa).Distinct().ToList())
            {
                if (!lstUpdateChungTuDetail.Any(y => y.SXauNoiMa == SXauNoiMa))
                {
                    BhDmMucLucNganSach mucLuc = lstXauNoiMaMLNS.FirstOrDefault(x => x.INamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == StatusType.ACTIVE
                   && SXauNoiMa == x.SXauNoiMa);
                    if (mucLuc == null)
                    {
                        continue;
                    }
                    BhQtcqBHXHChiTiet chungTuChiTiet = new BhQtcqBHXHChiTiet();
                    chungTuChiTiet.Id = Guid.NewGuid();
                    chungTuChiTiet.IdQTCQuyCheDoBHXH = IdBhQtcqBHXH;
                    chungTuChiTiet.DNgayTao = DateTime.Now;
                    chungTuChiTiet.INamLamViec = _sessionInfo.YearOfWork;
                    chungTuChiTiet.IIDMaDonVi = IIdMaDonVi;
                    chungTuChiTiet.SXauNoiMa = mucLuc.SXauNoiMa;
                    chungTuChiTiet.SLNS = mucLuc.SLNS;
                    chungTuChiTiet.IIdMucLucNganSach = mucLuc.IIDMLNS;
                    chungTuChiTiet.SLoaiTroCap = mucLuc.SMoTa;

                    chungTuChiTiet.ISoSQDeNghi = lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("1"))
                                                ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("1")).Select(x => x.ISoNgayHuong).Sum() : 0;
                    chungTuChiTiet.FTienSQDeNghi = lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("1"))
                                                ? lstTroCap?.Where(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("1")).Select(x => x.FSoTien).Sum() : 0;

                    chungTuChiTiet.ISoQNCNDeNghi = lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("2"))
                                                   ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("2")).Select(x => x.ISoNgayHuong).Sum() : 0;
                    chungTuChiTiet.FTienQNCNDeNghi = lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("2"))
                                                   ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("2")).Select(x => x.FSoTien).Sum() : 0;

                    chungTuChiTiet.ISoCNVCQPDeNghi = lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("3.") || y.SMaCapBac.Equals("413") || y.SMaCapBac.Equals("415")))
                                                    ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("3.") || y.SMaCapBac.Equals("413") || y.SMaCapBac.Equals("415"))).Select(x => x.ISoNgayHuong).Sum() : 0;
                    chungTuChiTiet.FTienCNVCQPDeNghi = lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("3.") || y.SMaCapBac.Equals("413") || y.SMaCapBac.Equals("415")))
                                                    ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("3.") || y.SMaCapBac.Equals("413") || y.SMaCapBac.Equals("415"))).Select(x => x.FSoTien).Sum() : 0;

                    chungTuChiTiet.ISoHSQBSDeNghi = lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("0"))
                                                    ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("0")).Select(x => x.ISoNgayHuong).Sum() : 0;
                    chungTuChiTiet.FTienHSQBSDeNghi = lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("0"))
                                                    ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("0")).Select(x => x.FSoTien).Sum() : 0;

                    chungTuChiTiet.ISoLDHDDeNghi = lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("423") || y.SMaCapBac.StartsWith("425") || y.SMaCapBac.StartsWith("43")))
                                                    ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("423") || y.SMaCapBac.StartsWith("425") || y.SMaCapBac.StartsWith("43"))).Select(x => x.ISoNgayHuong).Sum() : 0;
                    chungTuChiTiet.FTienLDHDDeNghi = lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("423") || y.SMaCapBac.StartsWith("425") || y.SMaCapBac.StartsWith("43")))
                                                    ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("423") || y.SMaCapBac.StartsWith("425") || y.SMaCapBac.StartsWith("43"))).Select(x => x.FSoTien).Sum() : 0;
                    chungTuChiTiet.ITongSoDeNghi = chungTuChiTiet.ISoSQDeNghi + chungTuChiTiet.ISoQNCNDeNghi + chungTuChiTiet.ISoCNVCQPDeNghi + chungTuChiTiet.ISoHSQBSDeNghi + chungTuChiTiet.ISoLDHDDeNghi;

                    chungTuChiTiet.FTongTienDeNghi = chungTuChiTiet.FTienSQDeNghi + chungTuChiTiet.FTienQNCNDeNghi + chungTuChiTiet.FTienCNVCQPDeNghi + chungTuChiTiet.FTienHSQBSDeNghi + chungTuChiTiet.FTienLDHDDeNghi;
                    lstAddChungTuChiTiet.Add(chungTuChiTiet);
                }
                else
                {
                    foreach (var item in lstUpdateChungTuDetail)
                    {
                        item.ISoSQDeNghi = item.ISoSQDeNghi + (lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("1"))
                                                               ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("1")).Select(x => x.ISoNgayHuong).Sum() : 0);
                        item.FTienSQDeNghi = item.FTienSQDeNghi + (lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("1"))
                                                                 ? lstTroCap?.Where(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("1")).Select(x => x.FSoTien).Sum() : 0);

                        item.ISoQNCNDeNghi = item.ISoQNCNDeNghi + (lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("2"))
                                                   ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("2")).Select(x => x.ISoNgayHuong).Sum() : 0);
                        item.FTienQNCNDeNghi = item.FTienQNCNDeNghi + (lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("2"))
                                                   ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("2")).Select(x => x.FSoTien).Sum() : 0);

                        item.ISoCNVCQPDeNghi = item.ISoCNVCQPDeNghi + (lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("3.") || y.SMaCapBac.Equals("413") || y.SMaCapBac.Equals("415")))
                                                    ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("3.") || y.SMaCapBac.Equals("413") || y.SMaCapBac.Equals("415"))).Select(x => x.ISoNgayHuong).Sum() : 0);
                        item.FTienCNVCQPDeNghi = item.FTienCNVCQPDeNghi + (lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("3.") || y.SMaCapBac.Equals("413") || y.SMaCapBac.Equals("415")))
                                                    ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("3.") || y.SMaCapBac.Equals("413") || y.SMaCapBac.Equals("415"))).Select(x => x.FSoTien).Sum() : 0);

                        item.ISoHSQBSDeNghi = item.ISoHSQBSDeNghi + (lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("0"))
                                                    ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("0")).Select(x => x.ISoNgayHuong).Sum() : 0);
                        item.FTienHSQBSDeNghi = item.FTienHSQBSDeNghi + (lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("0"))
                                                    ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && y.SMaCapBac.StartsWith("0")).Select(x => x.FSoTien).Sum() : 0);

                        item.ISoLDHDDeNghi = item.ISoLDHDDeNghi + (lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("423") || y.SMaCapBac.StartsWith("425") || y.SMaCapBac.StartsWith("43")))
                                                    ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("423") || y.SMaCapBac.StartsWith("425") || y.SMaCapBac.StartsWith("43"))).Select(x => x.ISoNgayHuong).Sum() : 0);
                        item.FTienLDHDDeNghi = item.FTienLDHDDeNghi + (lstTroCap.Any(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("423") || y.SMaCapBac.StartsWith("425") || y.SMaCapBac.StartsWith("43")))
                                                    ? lstTroCap.Where(y => y.SXauNoiMa == SXauNoiMa && (y.SMaCapBac.StartsWith("423") || y.SMaCapBac.StartsWith("425") || y.SMaCapBac.StartsWith("43"))).Select(x => x.FSoTien).Sum() : 0);

                        item.FTongTienDeNghi = item.FTienSQDeNghi + item.FTienQNCNDeNghi + item.FTienCNVCQPDeNghi + item.FTienHSQBSDeNghi + item.FTienLDHDDeNghi;
                        item.ITongSoDeNghi = item.ISoSQDeNghi + item.ISoQNCNDeNghi + item.ISoCNVCQPDeNghi + item.ISoHSQBSDeNghi + item.ISoLDHDDeNghi;
                    }
                }
            }

            //_chungTuChiTietService.AddRange(lstAddChungTuChiTiet);
            //_chungTuChiTietService.UpdateRange(lstUpdateChungTuDetail);
            //_iBhQtcqCtctGtTroCapService.AddRange(lstTroCap);
            //BhQtcqBHXHModel qTCQBHXH = _mapper.Map<BhQtcqBHXHModel>(chungTu);
            DialogHost.CloseDialogCommand.Execute(lstTroCap, null);
            SavedAction?.Invoke(lstTroCap);
            OnClose(obj);
            //var message = Resources.MsgSaveDone;
            //MessageBoxHelper.Info(message);
        }

        private T ConvertStringToNumber<T>(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return default(T);
            }
            else
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }
        }

        private DateTime? ConvertToDateTime(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            if (DateTime.TryParse(input, out DateTime result))
            {
                return result;
            }

            return null;
        }

        private void OnUploadFile()
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Title = "Chọn file excel",
                    RestoreDirectory = true,
                    DefaultExt = StringUtils.EXCEL_EXTENSION
                };
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                _lstErrQtcqBHXH.Clear();
                FilePath = openFileDialog.FileName;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.MsgErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
                return;
            }
        }

        public override void OnClose(object obj)
        {
            try
            {
                var window = obj as Window;
                window.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
