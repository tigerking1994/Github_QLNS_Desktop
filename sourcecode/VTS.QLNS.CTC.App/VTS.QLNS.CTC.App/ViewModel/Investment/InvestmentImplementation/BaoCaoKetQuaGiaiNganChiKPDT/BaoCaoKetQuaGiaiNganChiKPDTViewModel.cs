using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using log4net;
using VTS.QLNS.CTC.App.Service.UserFunction;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.BaoCaoKetQuaGiaiNganChiKPDT
{
    public class BaoCaoKetQuaGiaiNganChiKPDTViewModel : ViewModelBase
    {
        #region Private
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvPhanBoVonService _phanBoVonService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _dmService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITongHopNguonNSDauTuService _iTongHopNguonNsDauTuService;
        private readonly INsNguonNganSachService _iNsNguonNganSachService;
        private readonly IVdtDaDuAnService _iVdtDaDuAnService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_BC_KQ_GIAI_NGAN_KPDT;
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string Name => "Báo cáo kết quả giải ngân chi kinh phí đầu tư năm";
        public override string Description => "Báo cáo kết quả giải ngân chi kinh phí đầu tư năm";
        public override PackIconKind IconKind => PackIconKind.ClipboardFileOutline;
        public override Type ContentType => typeof(View.Investment.InvestmentImplementation.BaoCaoKetQuaGiaiNganChiKPDT.BaoCaoKetQuaGiaiNganChiKPDT);



        private ObservableCollection<BaoCaoKetQuaGiaiNganChiKPDTModel> _data;
        public ObservableCollection<BaoCaoKetQuaGiaiNganChiKPDTModel> Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        private string _iNamKeHoach;

        public string INamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
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

        private ObservableCollection<CheckBoxItem> _dataDonVi;
        public ObservableCollection<CheckBoxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private CheckBoxItem _selectedDonVi;
        public CheckBoxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        public string DonViCurrent { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportCommand { get; }

        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Header3 { get; set; }

        public BaoCaoKetQuaGiaiNganChiKPDTViewModel(ISessionService sessionService,
            IVdtKhvPhanBoVonService phanBoVonService,
            IExportService exportService,
            IDanhMucService dmService,
            INsDonViService nsDonViService,
            INsNguonNganSachService iNsNguonNganSachService,
            IVdtDaDuAnService iVdtDaDuAnService,
            ITongHopNguonNSDauTuService iTongHopNguonNsDauTuService,
            IMapper mapper,
            ILog logger)
        {
            _sessionService = sessionService;
            _phanBoVonService = phanBoVonService;
            _dmService = dmService;
            _nsDonViService = nsDonViService;
            _iNsNguonNganSachService = iNsNguonNganSachService;
            _iVdtDaDuAnService = iVdtDaDuAnService;
            _iTongHopNguonNsDauTuService = iTongHopNguonNsDauTuService;
            _mapper = mapper;
            _logger = logger;
            _exportService = exportService;

            SearchCommand = new RelayCommand(obj => OnSearch());
            ExportCommand = new RelayCommand(o => OnExport());
        }

        #region Relay Command
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            Header1 = "Dự toán trong năm";
            Header2 = "Dự toán đơn vị đề nghị chuyển năm trước chuyển sang";
            Header3 = "Kết quả giải ngân trong năm";
            OnPropertyChanged(nameof(Header1));
            OnPropertyChanged(nameof(Header2));
            OnPropertyChanged(nameof(Header3));
            LoadComboBoxNguonNganSach();
            LoadDonVi();
        }

        public void OnSearch()
        {
            int iNamThucHien = 0;
            StringBuilder messageBuilder = new StringBuilder();
            if (CbxNguonVonSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Nguồn ngân sách");
                messageBuilder.AppendLine();
            }
            if (string.IsNullOrEmpty(INamKeHoach))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Năm kế hoạch");
                messageBuilder.AppendLine();
            }
            else if (!int.TryParse(INamKeHoach, out iNamThucHien))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorFormat, "Năm kế hoạch");
                messageBuilder.AppendLine();
            }
            if (SelectedDonVi == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorFormat, "Đơn vị");
                messageBuilder.AppendLine();
            }
            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join(Environment.NewLine, messageBuilder.ToString()));
                LoadData();
                return;
            }
            Header1 = "Dự toán năm " + INamKeHoach;
            Header2 = "Dự toán đơn vị đề nghị chuyển năm " + (int.Parse(INamKeHoach) - 1) + " sang năm " + INamKeHoach;
            Header3 = "Kết quả giải ngân năm " + INamKeHoach;
            OnPropertyChanged(nameof(Header1));
            OnPropertyChanged(nameof(Header2));
            OnPropertyChanged(nameof(Header3));

            var lstData = _iTongHopNguonNsDauTuService.GetBcKetQuaGiaiNganChiPhiKinhPhiDT(SelectedDonVi.ValueItem, int.Parse(INamKeHoach), int.Parse(CbxNguonVonSelected.ValueItem)).ToList();
            Data = new ObservableCollection<BaoCaoKetQuaGiaiNganChiKPDTModel>(_mapper.Map<List<BaoCaoKetQuaGiaiNganChiKPDTModel>>(lstData));
            int i = 0;
            foreach (var item in Data)
            {
                ++i;
                item.IStt = i;
            }
        }

        private double GetGiaTriMaNguon(List<VdtTongHopNguonNsdauTu> lsTongHopNguonNsdauTus, string ma, Guid? idDuAn,
            string maNguonCha, int? iThuHoiTUCheDo = null)
        {
            if (string.IsNullOrEmpty(maNguonCha))
            {
                return lsTongHopNguonNsdauTus
                    .Where(x => x.IIdDuAnId.Equals(idDuAn) && ma == x.SMaNguon && (!iThuHoiTUCheDo.HasValue || (iThuHoiTUCheDo.HasValue && x.IThuHoiTUCheDo == iThuHoiTUCheDo.Value)))
                    .Sum(x => x.FGiaTri ?? 0);
            }
            return lsTongHopNguonNsdauTus
                .Where(x => x.IIdDuAnId.Equals(idDuAn) && ma == x.SMaNguon && maNguonCha != x.SMaNguonCha && (!iThuHoiTUCheDo.HasValue || (iThuHoiTUCheDo.HasValue && x.IThuHoiTUCheDo == iThuHoiTUCheDo.Value)))
                .Sum(x => x.FGiaTri ?? 0);
        }

        private double GetGiaTriMaDich(List<VdtTongHopNguonNsdauTu> lsTongHopNguonNsdauTus, string ma, Guid? idDuAn,
            string maNguonCha, int? iThuHoiTUCheDo = null)
        {
            if (string.IsNullOrEmpty(maNguonCha))
            {
                return lsTongHopNguonNsdauTus
                    .Where(x => x.IIdDuAnId.Equals(idDuAn) && ma == x.SMaDich && (!iThuHoiTUCheDo.HasValue || (iThuHoiTUCheDo.HasValue && x.IThuHoiTUCheDo == iThuHoiTUCheDo.Value)))
                    .Sum(x => x.FGiaTri ?? 0);
            }
            return lsTongHopNguonNsdauTus
                .Where(x => x.IIdDuAnId.Equals(idDuAn) && ma == x.SMaDich && maNguonCha != x.SMaNguonCha && (!iThuHoiTUCheDo.HasValue || (iThuHoiTUCheDo.HasValue && x.IThuHoiTUCheDo == iThuHoiTUCheDo.Value)))
                .Sum(x => x.FGiaTri ?? 0);
        }

        private void OnExport()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(1, Utility.Enum.ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("NamKeHoach", INamKeHoach);
                    data.Add("NamTruoc", (int.Parse(INamKeHoach) - 1));
                    data.Add("DonVi", DonViCurrent);
                    data.Add("Items", Data);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_THDT, ExportFileName.RPT_VDT_KETQUA_GIAINGAN_CHIKHINHPHIDAUTU);
                    string fileNamePrefix = string.Format("{0}_{1}", Path.GetFileNameWithoutExtension(templateFileName), INamKeHoach);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<BaoCaoKetQuaGiaiNganChiKPDTModel>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, Utility.Enum.ExportType.EXCEL);
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

        private void LoadComboBoxNguonNganSach()
        {
            _cbxNguonVon = new ObservableCollection<ComboboxItem>(new List<ComboboxItem>());
            var data = _iNsNguonNganSachService.FindNguonNganSach()
                .Select(n => new ComboboxItem() { ValueItem = (n.IIdMaNguonNganSach ?? 0).ToString(), DisplayItem = n.STen });
            _cbxNguonVon = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(CbxNguonVon));
        }

        private void LoadDonVi()
        {
            DataDonVi = new ObservableCollection<CheckBoxItem>();
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            DataDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
        }

    }
}
