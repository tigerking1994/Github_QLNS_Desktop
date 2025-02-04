using AutoMapper;
using FlexCel.XlsAdapter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PheDuyetThanhToan
{
    public class PheDuyetThanhToanImportViewModel : ViewModelBase
    {
        public override Type ContentType => typeof(VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.PheDuyetThanhToan.PheDuyetThanhToanImport);

        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtTtDeNghiThanhToanKhvService _keHoachVonDeNghiThanhToanService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly INsMucLucNganSachService _mlService;
        private readonly IVdtTtPheDuyetThanhToanService _service;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private IImportExcelService _importService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private List<ImportErrorItem> _lstErrChungTuChiTiet;
        private string _fileName;
        private Dictionary<string, Guid> _dicDuAn = new Dictionary<string, Guid>();
        #endregion

        #region Import item
        private ObservableCollection<VdtTtPheDuyetThanhToanImportModel> _items;
        public ObservableCollection<VdtTtPheDuyetThanhToanImportModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private VdtTtPheDuyetThanhToanImportModel _selected;
        public VdtTtPheDuyetThanhToanImportModel Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private bool _isSelectedFile;
        public bool IsSelectedFile
        {
            get => !string.IsNullOrEmpty(_filePath);
            set => SetProperty(ref _isSelectedFile, value);
        }

        private DateTime? _dNgayDeNghi;
        public DateTime? DNgayDeNghi
        {
            get => _dNgayDeNghi;
            set => SetProperty(ref _dNgayDeNghi, value);
        }
        #endregion

        #region RelayCommand
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        #endregion

        public PheDuyetThanhToanImportViewModel(INsDonViService nsDonViService,
            IVdtTtDeNghiThanhToanKhvService keHoachVonDeNghiThanhToanService,
            ITongHopNguonNSDauTuService tonghopService,
            INsMucLucNganSachService mlService,
            IVdtTtPheDuyetThanhToanService service,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IImportExcelService importService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _nsDonViService = nsDonViService;
            _keHoachVonDeNghiThanhToanService = keHoachVonDeNghiThanhToanService;
            _tonghopService = tonghopService;
            _mlService = mlService;
            _service = service;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _importService = importService;
            _sessionService = sessionService;
            _mapper = mapper;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorCommand = new RelayCommand(ShowError);
        }

        #region Relay Command
        private void OnUploadFile()
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

            FilePath = openFileDialog.FileName;
            _fileName = openFileDialog.SafeFileName;
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnProcessFile();
        }

        private void OnProcessFile()
        {
            _lstErrChungTuChiTiet = new List<ImportErrorItem>();
            List<string> lstError = new List<string>();

            if (string.IsNullOrEmpty(FilePath))
            {
                lstError.Add(Resources.ErrorFileEmpty);
            }

            ;
            //if (!ValidateForm()) return;

            var data = GetDataImportByFileType();
            Items = new ObservableCollection<VdtTtPheDuyetThanhToanImportModel>(data);
            if (lstError.Any())
            {
                string sMessError = string.Join(Environment.NewLine, lstError);
                System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //ValidateChungTuChiTiet();
            OnPropertyChanged(nameof(Items));
        }

        private void OnResetData()
        {
            FilePath = string.Empty;
            IsSelectedFile = false;
            Items = null;
            Selected = null;

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(Selected));
        }

        private void ShowError(object param)
        {
            var importTabIndex = (ImportTabIndex)((int)param);
            int rowIndex;
            rowIndex = Items.IndexOf(Selected);
            var errors = _lstErrChungTuChiTiet.Where(x => x.Row == rowIndex).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
            System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        #region Helper
        private List<VdtTtPheDuyetThanhToanImportModel> GetDataImportByFileType()
        {
            XlsFile xls = new XlsFile(false);
            xls.Open(FilePath);
            xls.ActiveSheet = 1;

            var lstResults = _importService.ProcessData<VdtTtPheDuyetThanhToanImportModel>(FilePath);
            if (lstResults.ImportErrors.Any())
            {
                _lstErrChungTuChiTiet.AddRange(lstResults.ImportErrors);
            }
            return _mapper.Map<List<VdtTtPheDuyetThanhToanImportModel>>(lstResults.Data);
        }
        #endregion
    }
}
