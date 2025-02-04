using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNKeHoachLuaChonNhaThau.MSTNImportGoiThau
{
    public class MSTNImportGoiThauViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ILog _logger;
        private readonly IImportExcelService _importService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly INhDmHinhThucChonNhaThauService _nhDmHinhThucChonNhaThauService;
        private readonly INhDmLoaiHopDongService _nhDmLoaiHopDongService;
        private readonly INhDmPhuongThucChonNhaThauService _nhDmPhuongThucChonNhaThauService;

        public override Type ContentType => typeof(View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNKeHoachLuaChonNhaThau.MSTNImportGoiThau.MSTNImportGoiThau);

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorGoiThauCommand { get; }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private ObservableCollection<NhGoiThauImportModel> _nhGoiThauImportModels;
        public ObservableCollection<NhGoiThauImportModel> NhGoiThauImportModels
        {
            get => _nhGoiThauImportModels;
            set => SetProperty(ref _nhGoiThauImportModels, value);
        }

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private NhGoiThauImportModel _selectedGoiThau;
        public NhGoiThauImportModel SelectedGoiThau
        {
            get => _selectedGoiThau;
            set => SetProperty(ref _selectedGoiThau, value);
        }

        private ObservableCollection<NhDaGoiThauModel> _itemsGoiThauModel;
        public ObservableCollection<NhDaGoiThauModel> ItemsGoiThauModel
        {
            get => _itemsGoiThauModel;
            set => SetProperty(ref _itemsGoiThauModel, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (NhGoiThauImportModels.Count > 0)
                    return !NhGoiThauImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        public MSTNImportGoiThauViewModel(
            ISessionService sessionService,
            ILog logger,
            IImportExcelService importService,
            IMapper mapper,
            INhDmHinhThucChonNhaThauService nhDmHinhThucChonNhaThauService,
            INhDmLoaiHopDongService nhDmLoaiHopDongService,
            INhDmPhuongThucChonNhaThauService nhDmPhuongThucChonNhaThauService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _nhDmHinhThucChonNhaThauService = nhDmHinhThucChonNhaThauService;
            _nhDmLoaiHopDongService = nhDmLoaiHopDongService;
            _nhDmPhuongThucChonNhaThauService = nhDmPhuongThucChonNhaThauService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData(obj));
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ShowErrorGoiThauCommand = new RelayCommand(obj => ShowErrorGoiThau());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            OnResetData();
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

        private void OnProcessFile()
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(FileName))
            {
                message = Resources.ErrorFileEmpty;
                goto ShowError;
            }
        ShowError:
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                //lấy thông tin thuế tncn
                ImportResult<NhGoiThauImportModel> dataImport = _importService.ProcessData<NhGoiThauImportModel>(FileName);
                NhGoiThauImportModels = new ObservableCollection<NhGoiThauImportModel>(dataImport.Data);

                if (dataImport.ImportErrors.Count > 0)
                    errors.AddRange(dataImport.ImportErrors);

                if (NhGoiThauImportModels == null || NhGoiThauImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (NhGoiThauImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);

                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            NhGoiThauImportModels = new ObservableCollection<NhGoiThauImportModel>();
            _importErrors = new List<ImportErrorItem>();
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void ShowErrorGoiThau()
        {
            int rowIndex = NhGoiThauImportModels.IndexOf(SelectedGoiThau);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private List<NhDaGoiThauModel> ConverData(List<NhGoiThauImportModel> importModels)
        {
            List<NhDaGoiThauModel> result = new List<NhDaGoiThauModel>();
            foreach (var import in importModels)
            {
                NhDaGoiThauModel item = new NhDaGoiThauModel();
                item.STenGoiThau = import.STenGoiThau;
                var hinhThucChonNhaThau = _nhDmHinhThucChonNhaThauService.FindByMaHinhThuc(import.SHinhThucChonNhaThau);
                var phuongThucChonNhaThau = _nhDmPhuongThucChonNhaThauService.FindAll().FirstOrDefault(x => x.SMaPhuongThucChonNhaThau == import.SPhuongThucChonNhaThau);
                var LoaiHopDong = _nhDmLoaiHopDongService.FindAll().FirstOrDefault(x => x.SMaLoaiHopDong == import.SLoaiHopDong);
                item.IIdHinhThucChonNhaThauId = hinhThucChonNhaThau != null ? hinhThucChonNhaThau.Id : Guid.Empty;
                item.IIdPhuongThucDauThauId = phuongThucChonNhaThau != null ? phuongThucChonNhaThau.Id : Guid.Empty;
                item.IIdLoaiHopDongId = LoaiHopDong != null ? LoaiHopDong.IIdLoaiHopDongId : Guid.Empty;
                item.STenHinhThucChonNhaThau = hinhThucChonNhaThau.STenHinhThucChonNhaThau;
                item.STenPhuongThucChonNhaThau = phuongThucChonNhaThau.STenPhuongThucChonNhaThau;
                item.STenLoaiHopDong = LoaiHopDong.STenLoaiHopDong;
                item.IThoiGianThucHien = (int)double.Parse(import.IThoiGianThucHien.Replace(',', '.'));
                result.Add(item);
            }
            return result;
        }

        private void OnSaveData(object obj)
        {
            try
            {
                var lstModel = ConverData(NhGoiThauImportModels.ToList());
                ItemsGoiThauModel = new ObservableCollection<NhDaGoiThauModel>(lstModel);
                foreach (var item in ItemsGoiThauModel)
                {
                    item.Id = Guid.NewGuid();
                    item.IsAdded = true;
                }
                MessageBoxHelper.Info("Import dữ liệu thành công");
                SavedAction?.Invoke(ItemsGoiThauModel);
                OnCloseWindow(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}
