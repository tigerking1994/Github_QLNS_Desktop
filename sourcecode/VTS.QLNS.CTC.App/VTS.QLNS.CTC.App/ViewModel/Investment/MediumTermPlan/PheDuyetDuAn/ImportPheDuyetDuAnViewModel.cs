using AutoMapper;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PheDuyetDuAn;
using VTS.QLNS.CTC.App.ViewModel.ImportViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.MediumTermPlan.PheDuyetDuAn
{
    public class ImportPheDuyetDuAnViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private IVdtDmChiPhiService _vdtDmChiPhiService;
        private IConfiguration _configuration;
        private string _importFolder;
        private string _fileName;
        private ImpHistory _impHistory;
        private List<ImportErrorItem> _listErrChungTuChiTiet;
        private List<ImportErrorItem> _listErrChungTu;
        private int _lastRowToRead = 0;

        public Action<BhDtctgBHXHModel> SavedAction;
        public override string Name => "Dự toán";
        public override Type ContentType => typeof(ImportPheDuyetDuAn);
        public override string Description => "Nhận phân bổ dự toán chi trên giao";
        public override PackIconKind IconKind => PackIconKind.Dollar;


        private ObservableCollection<ApproveProjectDetailModel> _dataQDDTHangMucByChiPhiImport;
        public ObservableCollection<ApproveProjectDetailModel> DataQDDTHangMucByChiPhiImport
        {
            get => _dataQDDTHangMucByChiPhiImport;
            set => SetProperty(ref _dataQDDTHangMucByChiPhiImport, value);
        }

        private ObservableCollection<VdtDaQddtChiPhiModel> _dataQDDauTuChiPhiImport;
        public ObservableCollection<VdtDaQddtChiPhiModel> DataQDDauTuChiPhiImport
        {
            get => _dataQDDauTuChiPhiImport;
            set => SetProperty(ref _dataQDDauTuChiPhiImport, value);
        }

        private PddaHangMucImportModel _selectedDivision;
        public PddaHangMucImportModel SelectedDivision
        {
            get => _selectedDivision;
            set => SetProperty(ref _selectedDivision, value);
        }

        private ObservableCollection<PddaHangMucImportModel> _divisionDetails = new ObservableCollection<PddaHangMucImportModel>();
        public ObservableCollection<PddaHangMucImportModel> DivisionDetails
        {
            get => _divisionDetails;
            set => SetProperty(ref _divisionDetails, value);
        }

        private PddaHangMucImportModel _selectedDivisionDetail;
        public PddaHangMucImportModel SelectedDivisionDetail
        {
            get => _selectedDivisionDetail;
            set => SetProperty(ref _selectedDivisionDetail, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

       
        public bool _isSaveData;
        public bool IsSaveData
        {
            get => DivisionDetails.Count > 0 && DivisionDetails.All(x => x.ImportStatus) && !IsValidateExists;
            set => SetProperty(ref _isSaveData, value);
        }

        public bool _isSelectedFile;
        public bool IsSelectedFile
        {
            get => !string.IsNullOrEmpty(_filePath);
            set => SetProperty(ref _isSelectedFile, value);
        }

        public bool _isValidateExists;
        public bool IsValidateExists
        {
            get => _isValidateExists;
            set
            {
                SetProperty(ref _isValidateExists, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
        }

        public bool IsEnableSaveMLNS = false;
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand AddMLNSCommand { get; }
        public RelayCommand SaveMLNSCommand { get; }
        public RelayCommand CloseCommand { get; }

        public ImportPheDuyetDuAnViewModel(ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            IConfiguration configuration,
            IVdtDmChiPhiService vdtDmChiPhiService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _configuration = configuration;
            _vdtDmChiPhiService = vdtDmChiPhiService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData(obj));
            ResetDataCommand = new RelayCommand(obj => OnResetData());

            ShowErrorCommand = new RelayCommand(ShowError);
            CloseCommand = new RelayCommand(OnCloseWindow);
        }

        public override void Init()
        {
            base.Init();

            _importFolder = _configuration.GetSection("ImportFolder").Value;
            Directory.CreateDirectory(_importFolder);
            OnResetData();
        }

        private void OnResetData()
        {
            _filePath = string.Empty;

            _divisionDetails = new ObservableCollection<PddaHangMucImportModel>();
            _impHistory = new ImpHistory();
            _listErrChungTuChiTiet = new List<ImportErrorItem>();
            _listErrChungTu = new List<ImportErrorItem>();
            LstFile = new ObservableCollection<FileFtpModel>();

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsSelectedFile));
            OnPropertyChanged(nameof(DivisionDetails));
            OnPropertyChanged(nameof(IsSaveData));

            OnPropertyChanged(nameof(LstFile));
        }

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
        }

        private void OnProcessFile()
        {
            _listErrChungTuChiTiet = new List<ImportErrorItem>();
            _listErrChungTu = new List<ImportErrorItem>();

            var messages = new List<string>();

            if (string.IsNullOrEmpty(FilePath))
            {
                messages.Add(Resources.ErrorFileEmpty);
            }

            var message = string.Join(Environment.NewLine, messages);
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.MessageBox.Show(message, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                //xử lý chứng từ chi tiết
                var sheetDetailAttribute = (SheetAttribute)Attribute.GetCustomAttribute(typeof(PddaHangMucImportModel), typeof(SheetAttribute));
                var importDivisionDetailResult = _importService.ProcessData<PddaHangMucImportModel>(FilePath);

                var dictErr = importDivisionDetailResult.ImportErrors.ToLookup(x => x.Row)
                    .ToDictionary(x => x.Key, x => x.ToList());

                importDivisionDetailResult.Data.Where(x => x.Loai == "CP" || (x.Loai == "HM" && !x.STT.Contains("_"))).Select(x => { x.IsHangCha = true; return x; }).ToList();
                _divisionDetails = new ObservableCollection<PddaHangMucImportModel>(importDivisionDetailResult.Data);
                OnPropertyChanged(nameof(DivisionDetails));

                if (!_divisionDetails.Any())
                {
                    message = string.Format(Resources.MsgSheetErrorDataEmpty, sheetDetailAttribute.SheetName);
                    System.Windows.MessageBox.Show(message, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    OnPropertyChanged(nameof(IsSaveData));
                    return;
                }
                FindHangMucChaCon();


                bool IsError = false;
                //Check lỗi giá trị phê duyệt ko được trống và kiểu dữ liệu không đúng định dạng
                foreach (var item in _divisionDetails)
                {

                    //Nếu Loại = CP và có hạng mục con
                    List<PddaHangMucImportModel> lstHMChiPhi = new List<PddaHangMucImportModel>();
                    if (item.Loai == "CP")
                    {
                        lstHMChiPhi = _divisionDetails.Where(x => x.Loai == "HM" && x.IIdChiPhi == item.IIdChiPhi && x.IdParent == null).ToList();
                        if(lstHMChiPhi!= null && lstHMChiPhi.Count() > 0)
                        {
                            Double fPheDuyetCP = NumberUtils.ConvertTextToDouble(item.FGiaTriPD);
                            Double fPheDuyetTongHM = lstHMChiPhi.Select(x => NumberUtils.ConvertTextToDouble(x.FGiaTriPD)).Sum();
                            if(fPheDuyetCP != fPheDuyetTongHM && fPheDuyetCP > 0 && fPheDuyetTongHM > 0)
                            {
                                item.ImportStatus = false;
                                IsError = true;
                                item.SError = "Giá trị chi phí không bằng tổng hạng mục";
                            }    
                        }    
                        

                    }
                    if (item.Loai == "HM")
                    {
                        if (NumberUtils.ConvertTextToDouble(item.FGiaTriPD) == 0)
                        {
                            item.ImportStatus = false;
                            IsError = true;
                            item.SError = "Giá trị phê duyệt phải lớn hơn 0";
                        }
                        else
                        {
                            Double number;
                            if (!Double.TryParse(item.FGiaTriPD, out number))
                            {
                                item.ImportStatus = false;
                                IsError = true;
                                item.SError = "Kiểu dữ liệu không đúng định dạng";
                            }
                            else
                            {
                                //Check số tiền con không bằng số tiên cha

                                if (item.Loai == "HM")
                                {
                                    Double fTienPheDuyet = NumberUtils.ConvertTextToDouble(item.FGiaTriPD);
                                    var lstHangMucCon = _divisionDetails.Where(x => x.Loai == "HM" && x.IdParent == item.Id && x.IIdChiPhi == item.IIdChiPhi).ToList();
                                    if (lstHangMucCon.Count() > 0)
                                    {
                                        Double fTongTienPheDuyet = lstHangMucCon.Select(x => NumberUtils.ConvertTextToDouble(x.FGiaTriPD)).Sum();
                                        if (fTienPheDuyet != fTongTienPheDuyet)
                                        {
                                            item.ImportStatus = false;
                                            IsError = true;
                                            item.SError = "Tổng các hạng mục con không bằng hạng mục cha";
                                        }
                                    }
                                }
                            }    
                        }
                    }

                }

                //Check lỗi chi phí không tồn tại
                if(!IsError)
                {
                    foreach (var item in _divisionDetails)
                    {
                        if (item.Loai == "CP")
                        {
                            //check chi phí ko tồn tại
                            if (!DataQDDauTuChiPhiImport.Any(x => x.SMaChiPhi == item.SMaChiPhi || x.TenChiPhi == item.STenCPHM))
                            {
                                item.ImportStatus = false;
                                IsError = true;
                                item.SError = "Chi phí không tồn tại";
                            }

                            //Check trùng mã chi phí
                            var isExitMCP = _divisionDetails.Where(x => x.SMaChiPhi == item.SMaChiPhi).ToList();
                            if(isExitMCP != null && isExitMCP.Count() > 1)
                            {
                                item.ImportStatus = false;
                                IsError = true;
                                item.SError = "Mã chi phí bị trùng";
                            }    
                        }
                    }

                }

               
                
                foreach (var item in _divisionDetails)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != nameof(PddaHangMucImportModel.ImportStatus))
                        {
                            var entityDetail = (PddaHangMucImportModel)sender;
                            var rowIndex = _divisionDetails.IndexOf(entityDetail);
                            var listError = _importService.ValidateItem(entityDetail, rowIndex);
                            if (listError.Count > 0)
                            {
                                var messageOfRow = listError.Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToList();
                                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, messageOfRow), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                                _listErrChungTuChiTiet.AddRange(listError);
                                entityDetail.ImportStatus = false;
                                if (listError.Any(x => x.IsErrorMLNS))
                                {
                                    entityDetail.IsErrorMLNS = true;
                                }
                            }
                            else
                            {
                                entityDetail.ImportStatus = true;
                                entityDetail.IsErrorMLNS = false;
                                _listErrChungTuChiTiet = _listErrChungTuChiTiet.Where(x => x.Row != rowIndex).ToList();
                            }
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                }

                OnPropertyChanged(nameof(DivisionDetails));
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception e)
            {
                if (e is Utility.Exceptions.WrongReportException)
                {
                    System.Windows.MessageBox.Show(Resources.WrongReportFormat, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    System.Windows.MessageBox.Show(Resources.ErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private List<ImportErrorItem> GetError(string sheet, int row, int columnIndex, string value, string valueCompare, string message = "Dữ liệu không đúng.")
        {
            var errors = new List<ImportErrorItem>();
            if (string.IsNullOrWhiteSpace(valueCompare) || !value.Equals(valueCompare.Trim()))
            {
                errors.Add(new ImportErrorItem
                {
                    ColumnName = _importService.GetColumnAttribute<DivisionImportModel>(columnIndex).ColumnName,
                    Row = row,
                    Error = message,
                    SheetName = sheet
                });
            }
            return errors;
        }

        public  void OnSaveData(object obj)
        {
            //// show message
            System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            //// mở màn hình chứng từ chi tiết
            //var entityModel = _mapper.Map<BhDtctgBHXHModel>(chungTu);
            SavedAction?.Invoke(null);
            ((Window)obj).Close();
        }

        public void FindHangMucChaCon()
        {
            foreach (var item in _divisionDetails.Where(x => x.Loai == "CP").ToList())
            {
                var chiphi = DataQDDauTuChiPhiImport.Where(x => x.SMaChiPhi == item.SMaChiPhi || x.TenChiPhi == item.STenCPHM).ToList();
                if (chiphi.Count() > 0)
                {
                    item.IIdChiPhi = chiphi.First().IdChiPhiDuAn;
                }

            }
            //Tìm hạng mục theo từng loại chi phí
            Guid? IIdChiPhi = null;

            foreach (var item in _divisionDetails)
            {
                ApproveProjectDetailModel model = new ApproveProjectDetailModel();
                if (item.Loai == "CP")
                {
                    IIdChiPhi = item.IIdChiPhi;
                }
                else
                {
                    item.IIdChiPhi = IIdChiPhi;
                    FindChildHangMuc(item);
                }
                model.TenHangMuc = item.STenCPHM;
                model.IdChiPhi = item.IIdChiPhi;
                model.IdDuAnChiPhi = item.IIdChiPhi;
                model.GiaTriPheDuyet = NumberUtils.ConvertTextToDouble(item.FGiaTriPD);
                model.IdDuAnHangMuc = item.Id;
                model.HangMucParentId = item.IdParent;
                model.IsAdded = true;
                model.TenLoaiCT = item.Loai;
                model.IsModified = (model.GiaTriPheDuyet > 0) ? true : false;
                model.MaOrDer = item.STT;
                model.IsHangCha = item.IsHangCha;
                DataQDDTHangMucByChiPhiImport.Add(model);
            }
        }

        public void FindChildHangMuc(PddaHangMucImportModel item)
        {
            if (item.STT.Contains("_"))
            {
                var lastIndex = item.STT.LastIndexOf("_");
                var sSttparent = item.STT.Substring(0, lastIndex);
                item.IdParent = _divisionDetails.Any(x => x.STT.Equals(sSttparent) && x.Loai == "HM" && x.IIdChiPhi == item.IIdChiPhi) ? _divisionDetails.FirstOrDefault(x => x.STT.Equals(sSttparent) &&  x.Loai == "HM" && x.IIdChiPhi == item.IIdChiPhi).Id : null;
            }
            else
            {
                item.IdParent =  null;
                item.IsHangCha = true;

            }

        }


        private void ShowError(object param)
        {
            var importTabIndex = (ImportTabIndex)((int)param);
            var errors = new HashSet<string>();
            int rowIndex;
            switch (importTabIndex)
            {
                case ImportTabIndex.Data:
                    rowIndex = _divisionDetails.IndexOf(SelectedDivisionDetail);
                    errors = _listErrChungTuChiTiet.Where(x => x.Row == rowIndex).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
                    break;
                case ImportTabIndex.MLNS:
                    errors = new HashSet<string>();
                    break;
            }
            System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void OnCloseWindow(object obj)
        {
            var window = obj as Window;
            window.Close();
        }
    }
}
