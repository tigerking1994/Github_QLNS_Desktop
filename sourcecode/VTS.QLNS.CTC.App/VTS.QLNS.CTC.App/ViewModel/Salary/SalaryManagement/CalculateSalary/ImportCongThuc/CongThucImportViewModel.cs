using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility.Exceptions;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.CalculateSalary.ImportCongThuc
{
    public class CongThucImportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly TlDmPhuCapService _tlDmPhuCapService;
        private readonly ITlDmCachTinhLuongChuanService _tlDmCachTinhLuongChuanService;
        private readonly ITlDmCachTinhLuongTruyLinhService _tlDmCachTinhLuongTruyLinhService;
        private readonly ITlDmCachTinhLuongBaoHiemService _tlDmCachTinhLuongBaoHiemService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler OpenDetail;

        public override string Name => "Import Công thức tính lương";
        public override Type ContentType => typeof(View.Salary.SalaryManagement.CalculateSalary.ImportCongThuc.CongThucImport);
        public override string Description => "Import Công thức tính lương";
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private ObservableCollection<TlDmCongThucLuongImportModel> _dmCongThucImportModels;
        public ObservableCollection<TlDmCongThucLuongImportModel> DmCongThucImportModels
        {
            get => _dmCongThucImportModels;
            set => SetProperty(ref _dmCongThucImportModels, value);
        }
        private ObservableCollection<TlDmPhuCapImportModel> _dmCanBoPhuCapImportModels;
        public ObservableCollection<TlDmPhuCapImportModel> DmCanBoPhuCapImportModels
        {
            get => _dmCanBoPhuCapImportModels;
            set => SetProperty(ref _dmCanBoPhuCapImportModels, value);
        }

        private TlDmCongThucLuongImportModel _selectedItem;
        public TlDmCongThucLuongImportModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (DmCongThucImportModels.Count > 0)
                    return !DmCongThucImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorCommand { get; }

        public CongThucImportViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            TlDmPhuCapService tlDmPhuCapService,
            ITlDmCachTinhLuongChuanService tlDmCachTinhLuongChuanService,
            ITlDmCachTinhLuongTruyLinhService tlDmCachTinhLuongTruyLinhService,
            ITlDmCachTinhLuongBaoHiemService tlDmCachTinhLuongBaoHiemService,
            ILog logger)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _tlDmCachTinhLuongTruyLinhService = tlDmCachTinhLuongTruyLinhService;
            _tlDmCachTinhLuongBaoHiemService = tlDmCachTinhLuongBaoHiemService;
            _logger = logger;


            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ShowErrorCommand = new RelayCommand(obj => ShowError());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            OnResetData();
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            _dmCongThucImportModels = new ObservableCollection<TlDmCongThucLuongImportModel>();
            _dmCanBoPhuCapImportModels = new ObservableCollection<TlDmPhuCapImportModel>();
            _importErrors = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(DmCongThucImportModels));
            OnPropertyChanged(nameof(DmCanBoPhuCapImportModels));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file excel");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FileName = openFileDialog.FileName;
        }

        private void ShowError()
        {
            int rowIndex = _dmCongThucImportModels.IndexOf(SelectedItem);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            MessageBoxHelper.Info(message);
        }

        private void OnProcessFile()
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(FileName))
            {
                message = Resources.MsgChooseFileImport;
                goto ShowError;
            }

        ShowError:
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Warning(message);
                return;
            }

            try
            {
                _importErrors = new List<ImportErrorItem>();
                ImportResult<TlDmCongThucLuongImportModel> _dmCongThucResult = _importService.ProcessData<TlDmCongThucLuongImportModel>(FileName);
                DmCongThucImportModels = new ObservableCollection<TlDmCongThucLuongImportModel>(_dmCongThucResult.Data);

                //danh muc phu cap
                ImportResult<TlDmPhuCapImportModel> _dmCanBoPhuCapResult = _importService.ProcessData<TlDmPhuCapImportModel>(FileName);
                DmCanBoPhuCapImportModels = new ObservableCollection<TlDmPhuCapImportModel>(_dmCanBoPhuCapResult.Data);
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                if (ex is WrongReportException)
                {
                    System.Windows.MessageBox.Show(Resources.WrongReportFormat, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckFormatFileImport);
                }
            }
        }

        private void OnSaveData()
        {
            List<TlDmPhuCap> dmPhuCaps= new List<TlDmPhuCap>();
            List<TlDmPhuCapImportModel> listPhuCapImport = _dmCanBoPhuCapImportModels.Where(x => x.ImportStatus && !x.IsWarning).ToList();
            foreach (var pc in listPhuCapImport)
            {
                TlDmPhuCap tlDmPhuCap = new TlDmPhuCap();
                if (!string.IsNullOrEmpty(pc.MaPhuCap))
                {
                    tlDmPhuCap = _tlDmPhuCapService.FindByMaPhuCap(pc.MaPhuCap);
                    if (tlDmPhuCap == null)
                    {
                        tlDmPhuCap = new TlDmPhuCap();
                    }
                    tlDmPhuCap.MaPhuCap = pc.MaPhuCap;
                    tlDmPhuCap.TenPhuCap = pc.TenPhuCap;
                    tlDmPhuCap.GiaTri = string.IsNullOrEmpty(pc.GiaTri) ? 0 : decimal.Parse(pc.GiaTri);
                    tlDmPhuCap.IthangToiDa = string.IsNullOrEmpty(pc.IthangToiDa) ? 0 : int.Parse(pc.IthangToiDa);
                    tlDmPhuCap.IsFormula = convertStringToBool(pc.IsFormula);
                    tlDmPhuCap.Chon = convertStringToBool(pc.Chon); ;
                    tlDmPhuCap.IsReadonly = convertStringToBool(pc.IsReadonly);
                    tlDmPhuCap.Parent = pc.Parent;
                    tlDmPhuCap.ILoai = string.IsNullOrEmpty(pc.ILoai) ? 0 : int.Parse(pc.ILoai);
                    tlDmPhuCap.IDinhDang = string.IsNullOrEmpty(pc.IDinhDang) ? 0 : int.Parse(pc.IDinhDang);
                    tlDmPhuCap.BGiaTri = convertStringToBool(pc.BGiaTri);
                    tlDmPhuCap.BHuongPcSn = convertStringToBool(pc.BHuongPcSn);
                    tlDmPhuCap.BSaoChep = convertStringToBool(pc.BSaoChep);
                    tlDmPhuCap.IsModified = true;
                }
                dmPhuCaps.Add(tlDmPhuCap);
            }
            _tlDmPhuCapService.AddOrUpdateRange(dmPhuCaps, _mapper.Map<AuthenticationInfo>(_sessionInfo));

            //cong thuc luong
            List<TlDmCongThucLuongImportModel> listCongThucImport = _dmCongThucImportModels.Where(x => x.ImportStatus && !x.IsWarning).ToList();
            foreach (var congThuc in listCongThucImport)
            {
                saveCongThucLuong(congThuc);
            }

            MessageBoxHelper.Info(Resources.MsgImportSuccess);
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(null);
    }

        private void saveCongThucLuong(TlDmCongThucLuongImportModel itemImport)
        {
            try
            {
                string maCachTl = itemImport.MaCachTl;

                // Lương chuẩn
                if (CachTinhLuong.CACH0.Equals(maCachTl))
                {
                    TlDmCachTinhLuongChuan tlDmCachTinhLuongChuan;
                    if (!string.IsNullOrEmpty(itemImport.MaCot))
                    {
                        tlDmCachTinhLuongChuan = _tlDmCachTinhLuongChuanService.FindByMaCot(itemImport.MaCot);
                        if(tlDmCachTinhLuongChuan == null) 
                        {
                            tlDmCachTinhLuongChuan = new TlDmCachTinhLuongChuan();
                        }

                        tlDmCachTinhLuongChuan.MaCot = itemImport.MaCot;
                        tlDmCachTinhLuongChuan.TenCot = itemImport.TenCot;
                        tlDmCachTinhLuongChuan.CongThuc = itemImport.CongThuc;
                        tlDmCachTinhLuongChuan.NoiDung = itemImport.NoiDung;
                        tlDmCachTinhLuongChuan.MaCachTl =  itemImport.MaCachTl;

                        if (tlDmCachTinhLuongChuan.Id.IsNullOrEmpty())
                        {
                            _tlDmCachTinhLuongChuanService.Add(tlDmCachTinhLuongChuan);
                        }
                        else
                        {
                            _tlDmCachTinhLuongChuanService.Update(tlDmCachTinhLuongChuan);
                        }
                    }
                }

                // Truy lĩnh
                if (CachTinhLuong.CACH5.Equals(maCachTl))
                {
                    TlDmCachTinhLuongTruyLinh tlDmCachTinhLuongTruyLinh;
                    if (!string.IsNullOrEmpty(itemImport.MaCot))
                    {
                        tlDmCachTinhLuongTruyLinh = _tlDmCachTinhLuongTruyLinhService.FindByMaCot(itemImport.MaCot);
                        if (tlDmCachTinhLuongTruyLinh == null)
                        {
                            tlDmCachTinhLuongTruyLinh = new TlDmCachTinhLuongTruyLinh();
                        }

                        tlDmCachTinhLuongTruyLinh.MaCot = itemImport.MaCot;
                        tlDmCachTinhLuongTruyLinh.TenCot = itemImport.TenCot;
                        tlDmCachTinhLuongTruyLinh.CongThuc = itemImport.CongThuc;
                        tlDmCachTinhLuongTruyLinh.NoiDung = itemImport.NoiDung;
                        tlDmCachTinhLuongTruyLinh.MaCachTl = itemImport.MaCachTl;

                        if (tlDmCachTinhLuongTruyLinh.Id.IsNullOrEmpty())
                        {
                            _tlDmCachTinhLuongTruyLinhService.Add(tlDmCachTinhLuongTruyLinh);
                        }
                        else
                        {
                            _tlDmCachTinhLuongTruyLinhService.Update(tlDmCachTinhLuongTruyLinh);
                        }
                    }
                }

                // Bảo hiểm
                if (CachTinhLuong.CACH2.Equals(maCachTl))
                {
                    TlDmCachTinhLuongBaoHiem tlDmCachTinhLuongBaoHiem;
                    if (!string.IsNullOrEmpty(itemImport.MaCot))
                    {
                        tlDmCachTinhLuongBaoHiem = _tlDmCachTinhLuongBaoHiemService.FindByMaCot(itemImport.MaCot);
                        if (tlDmCachTinhLuongBaoHiem == null)
                        {
                            tlDmCachTinhLuongBaoHiem = new TlDmCachTinhLuongBaoHiem();
                        }

                        tlDmCachTinhLuongBaoHiem.MaCot = itemImport.MaCot;
                        tlDmCachTinhLuongBaoHiem.TenCot = itemImport.TenCot;
                        tlDmCachTinhLuongBaoHiem.CongThuc = itemImport.CongThuc;
                        tlDmCachTinhLuongBaoHiem.NoiDung = itemImport.NoiDung;
                        tlDmCachTinhLuongBaoHiem.MaCachTl = itemImport.MaCachTl;

                        if (tlDmCachTinhLuongBaoHiem.Id.IsNullOrEmpty())
                        {
                            _tlDmCachTinhLuongBaoHiemService.Add(tlDmCachTinhLuongBaoHiem);
                        }
                        else
                        {
                            _tlDmCachTinhLuongBaoHiemService.Update(tlDmCachTinhLuongBaoHiem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
       
        private bool convertStringToBool(string value)
        {
            if(string.IsNullOrEmpty(value)) return false; 
            return Convert.ToBoolean(value);
        }
        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}

