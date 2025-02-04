using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
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
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDanhMucNhaThau.ImportNhaThau
{
    public class ImportNhaThauViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ILog _logger;
        private readonly IImportExcelService _importService;
        private readonly INhDmNhaThauService _iNhDmNhaThauService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(View.Forex.ForexDanhMucNhaThau.ImportNhaThau.ImportNhaThau);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private ObservableCollection<NhaThauImportModel> _nhaThauImportModels;
        public ObservableCollection<NhaThauImportModel> NhaThauImportModels
        {
            get => _nhaThauImportModels;
            set => SetProperty(ref _nhaThauImportModels, value);
        }

        private NhaThauImportModel _seletedNhaThau;
        public NhaThauImportModel SeletedNhaThau
        {
            get => _seletedNhaThau;
            set => SetProperty(ref _seletedNhaThau, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoai;
        public ObservableCollection<ComboboxItem> ItemsLoai
        {
            get => _itemsLoai;
            set => SetProperty(ref _itemsLoai, value);
        }

        //public bool IsSaveData
        //{
        //    get
        //    {
        //        if (NhaThauImportModels.Count > 0)
        //            return !NhaThauImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
        //        return false;
        //    }
        //}

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorNhaThauCommand { get; }

        public ImportNhaThauViewModel(ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IImportExcelService importService,
            INhDmNhaThauService iNhDmNhaThauService)
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _importService = importService;
            _iNhDmNhaThauService = iNhDmNhaThauService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            ShowErrorNhaThauCommand = new RelayCommand(obj => ShowErrorNhaThau());
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
            NhaThauImportModels = new ObservableCollection<NhaThauImportModel>();
            _importErrors = new List<ImportErrorItem>();
            LoadLoai();
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(ImportErrors));
            //OnPropertyChanged(nameof(IsSaveData));
        }

        public void LoadLoai()
        {
            var results = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem("Nhà thầu", "1"),
                new ComboboxItem("Đơn vị ủy thác", "2")
            };
            _itemsLoai = results;
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

        private void ShowErrorNhaThau()
        {
            int rowIndex = NhaThauImportModels.IndexOf(SeletedNhaThau);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
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
                ImportResult<NhaThauImportModel> dataImport = _importService.ProcessData<NhaThauImportModel>(FileName, false);
                NhaThauImportModels = new ObservableCollection<NhaThauImportModel>(dataImport.Data);
                ImportErrors.Clear();
                if (dataImport.ImportErrors.Count > 0)
                {
                    ImportErrors.AddRange(dataImport.ImportErrors);
                }

                if (NhaThauImportModels == null || NhaThauImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (NhaThauImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                //OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnSaveData()
        {
            if (NhaThauImportModels == null || NhaThauImportModels.Count() <= 0) return;
            if (!ValidateViewModelHelper.Validate(NhaThauImportModels.AsEnumerable())) return;
            if (!CheckDuplicateMaNhaThau(NhaThauImportModels.ToList())) return;
            try
            {
                var lstModel = ConvertData(NhaThauImportModels.ToList());
                var entities = _mapper.Map<List<NhDmNhaThau>>(lstModel);
                _iNhDmNhaThauService.AddRange(entities);
                MessageBoxHelper.Info("Lưu dữ liệu thành công.");
                SavedAction?.Invoke(null);
                OnResetData();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                MessageBoxHelper.Error("Lỗi lưu dữ liệu.");
            }
        }

        private bool CheckDuplicateMaNhaThau(List<NhaThauImportModel> nhaThauImportModels)
        {
            var lstMaDuplicate = nhaThauImportModels.GroupBy(x => x.MaNhaThau.ToUpper()).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            if (lstMaDuplicate != null && lstMaDuplicate.Count() > 0)
            {
                MessageBoxHelper.Error("Mã nhà thầu " + lstMaDuplicate.FirstOrDefault() + " bị lặp, vui lòng thử lại!");
                return false;
            }

            var lstMaExists = _iNhDmNhaThauService.FindAll().Where(x => nhaThauImportModels.Select(y => y.MaNhaThau.ToUpper()).Contains(x.SMaNhaThau.ToUpper()));
            if (lstMaExists != null && lstMaExists.Count() > 0)
            {
                MessageBoxHelper.Error("Mã nhà thầu " + lstMaExists.FirstOrDefault().SMaNhaThau + " bị lặp, vui lòng thử lại!");
                return false;
            }

            return true;
        }

        private List<NhDmNhaThauModel> ConvertData(List<NhaThauImportModel> importModels)
        {
            List<NhDmNhaThauModel> results = new List<NhDmNhaThauModel>();
            NhDmNhaThauModel it;
            foreach (var import in importModels)
            {
                it = new NhDmNhaThauModel();
                if (!import.NgayCapCmnd.IsEmpty())
                {
                    if (DateTime.TryParse(import.NgayCapCmnd, CultureInfo.CreateSpecificCulture("vi-VN"), DateTimeStyles.None, out DateTime date))
                    {
                        it.DNgayCapCmnd = date;
                    }
                    else
                    {
                        continue;
                    }
                }
                it.ILoai = !string.IsNullOrEmpty(import.Loai) ? int.Parse(import.Loai) : 0;
                it.SMaNhaThau = import.MaNhaThau;
                it.STenNhaThau = import.TenNhaThau;
                it.SDiaChi = import.DiaChi;
                it.SDaiDien = import.DaiDien;
                it.SWebsite = import.Website;
                it.SMaSoThue = import.MaSoThue;
                it.SNganHang = import.NganHang;
                it.SMaNganHang = import.MaNganHang;
                it.SSoTaiKhoan = import.SoTaiKhoan;
                it.SChucVu = import.ChucVu;
                it.SDienThoai = import.SoDienThoai;
                it.SFax = import.SoFax;
                it.SEmail = import.Email;
                it.SNguoiLienHe = import.NguoiLienHe;
                it.SDienThoaiLienHe = import.SdtLienHe;
                it.SSoCmnd = import.SoCmnd;
                it.SNoiCapCmnd = import.NoiCapCmnd;

                results.Add(it);
            }
            return results;
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        public override void OnClose(object obj)
        {
            base.OnClose(obj);
            Window window = obj as Window;
            window.Close();
        }
    }
}
