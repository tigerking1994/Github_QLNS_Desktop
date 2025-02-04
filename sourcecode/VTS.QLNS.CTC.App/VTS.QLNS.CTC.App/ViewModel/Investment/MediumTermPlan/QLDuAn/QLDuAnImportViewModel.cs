using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Command;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using log4net;
using FlexCel.XlsAdapter;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Service;
using System.IO;
using System.Web.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.QLDuAn
{
    public class QLDuAnImportViewModel : ViewModelBase
    {
        #region Private
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private IProjectManagerService _projectManagerService;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtDaDuAnService _duAnService;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private readonly IVdtDmLoaiCongTrinhService _loaicongtrinhService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly ILog _logger;
        private static int indexSMaDuAn;
        private List<ImportErrorItem> _lstErrQlDuAn = new List<ImportErrorItem>();
        private Dictionary<string, VdtDaDuAn> _dicDuAn;
        private Dictionary<string, Dictionary<string, VdtDaNguonVon>> _dicNguonVon;
        private Dictionary<string, Dictionary<string, VdtDaDuAnHangMuc>> _dicHangMuc;
        private Dictionary<string, VdtDmLoaiCongTrinh> _dicLoaiCongTrinh;
        private Dictionary<string, NsNguonNganSach> _dicNsNguonVon;
        #endregion

        #region Public
        public override string Name => "IMPORT THÔNG TIN CHUNG DỰ ÁN";
        public override string Description => "Chọn file Excel, thực hiện kiểm tra và import dữ liệu thông tin chung dự án";
        #endregion

        #region RelayCommand
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand DownloadTemplateCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand ResetDataCommand { get; }
        #endregion

        #region Variables    
        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }


        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }


        private ObservableCollection<VdtDaThongTinDuAnImportModel> _items;
        public ObservableCollection<VdtDaThongTinDuAnImportModel> Items
        {
            get => _items;
            set 
            {
                SetProperty(ref _items, value);
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private VdtDaThongTinDuAnImportModel _selectedItem;
        public VdtDaThongTinDuAnImportModel SelectedItem
        {
            get => _selectedItem;
            set {
                SetProperty(ref _selectedItem, value);
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get
            {
                _isSaveData = ((Items != null && Items.Count > 0 ) && SelectedDonVi != null) ? true : false;
                return _isSaveData;
            }
            set
            {
                SetProperty(ref _isSaveData, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        #endregion

        public QLDuAnImportViewModel(
            ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            INsDonViService nsDonViService,
            IProjectManagerService projectManagerService,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHienDuAnService,
            IVdtDaDuAnService duAnService,
            IVdtDmLoaiCongTrinhService loaicongtrinhService,
            INsNguonNganSachService nsNguonVonService,
            ILog logger)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _projectManagerService = projectManagerService;
            _nsDonViService = nsDonViService;
            _vdtDmDonViThucHienDuAnService = vdtDmDonViThucHienDuAnService;
            _duAnService = duAnService;
            _loaicongtrinhService = loaicongtrinhService;
            _nsNguonVonService = nsNguonVonService;
            _logger = logger;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            DownloadTemplateCommand = new RelayCommand(obj => OnDownloadTemplate());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
        }

        #region Methods
        public override void Init()
        {
            try
            {
                LoadDonVi();
                OnLoadLoaiCongTrinh();
                OnLoadNguonVon();
                OnResetData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _selectedDonVi = null;
            _items = new ObservableCollection<VdtDaThongTinDuAnImportModel>();

            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnDownloadTemplate()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Chọn file excel",
                RestoreDirectory = true,
                DefaultExt = StringUtils.EXCEL_EXTENSION,
                Filter = "Execl files (*.xlsx)|*.xlsx"
        };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileTemplatePath = Path.Combine(IOExtensions.ApplicationPath, "AppData", "Template","Xlxs", "VonDauTu", "QLDuAn", "tmp_Vdt_Da_ThongTinDuAn.xlsx");
                try
                {
                    File.Copy(fileTemplatePath, saveFileDialog.FileName);
                    System.Windows.MessageBox.Show("Đã tải về thành công!");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
               
            }

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

                FilePath = openFileDialog.FileName;
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    HandleData();
                }, (s, e) =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.MsgErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
                return;
            }
        }

        private void HandleData()
        {
            Dictionary<string, Guid> _dicMaDuAn = new Dictionary<string, Guid>();
            var lstDuAn = _projectManagerService.FindAll(n => 1 == 1);
            if(lstDuAn != null)
            {
                foreach(var item in lstDuAn)
                {
                    if (!_dicMaDuAn.ContainsKey(item.SMaDuAn))
                    {
                        _dicMaDuAn.Add(item.SMaDuAn, item.Id);
                    }
                }
            }
            _dicDuAn = new Dictionary<string, VdtDaDuAn>();
            _dicNguonVon = new Dictionary<string, Dictionary<string, VdtDaNguonVon>>();
            _dicHangMuc = new Dictionary<string, Dictionary<string, VdtDaDuAnHangMuc>>();
            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;
                
                var dataImport = _importService.ProcessData<VdtDaThongTinDuAnImportModel>(FilePath);
                var QlDuAnImportModels = new ObservableCollection<VdtDaThongTinDuAnImportModel>(dataImport.Data);

                _lstErrQlDuAn = new List<ImportErrorItem>();
                List<string> lstError = new List<string>();

                if(SelectedDonVi == null)
                {
                    _lstErrQlDuAn.Add(new ImportErrorItem()
                    {
                        ColumnName = "Đơn vị",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "đơn vị")
                    });
                }

                if (dataImport.ImportErrors.Count > 0)
                {
                    _lstErrQlDuAn.AddRange(dataImport.ImportErrors);
                }

                if (QlDuAnImportModels == null || QlDuAnImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (QlDuAnImportModels.Any(x => !x.ImportStatus))
                {
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (string.IsNullOrEmpty(FilePath))
                {
                    lstError.Add(Resources.ErrorFileEmpty);
                }

                int i = 0;
                foreach (var item in QlDuAnImportModels)
                {
                    ++i;
                    var listError = ValidateItem(item, i, _dicMaDuAn);

                    if (listError.Count > 0)
                    {
                        _lstErrQlDuAn.AddRange(listError);
                        item.ImportStatus = false;

                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                }

                Items = QlDuAnImportModels;

                if (lstError.Any())
                {
                    string sMessError = string.Join(Environment.NewLine, lstError);
                    System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                FilePath = "";
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave(object obj)
        {
            try
            {
                List<string> lstError = new List<string>();
                HandleSaveData(Items.Where(x=>x.ImportStatus).ToList());
                System.Windows.MessageBox.Show(Resources.FileImportStatus);
                SavedAction?.Invoke(null);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                _logger.Error(ex.Message, ex);
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

        private void HandleSaveData(List<VdtDaThongTinDuAnImportModel> items)
        {

            if (items.Count <= 0) throw new Exception("Không có bản ghi nào có thể lưu!");

            DonVi donviSuDung = _nsDonViService.FindByIdDonVi(_sessionService.Current.IdDonVi, _sessionService.Current.YearOfWork);
            _dicDuAn = new Dictionary<string, VdtDaDuAn>();
            Dictionary<string, VdtDaDuAnHangMuc> dicDuAnHangMuc = new Dictionary<string, VdtDaDuAnHangMuc>();
            Dictionary<string, VdtDaNguonVon>  dicDuAnNguonVon = new Dictionary<string, VdtDaNguonVon>();

            foreach (var item in items)
            {

                if (!_dicDuAn.ContainsKey(item.SMaDuAn))
                {
                    _dicDuAn.Add(item.SMaDuAn, new VdtDaDuAn()
                    {
                        Id = Guid.NewGuid(),
                        STenDuAn = item.STenDuAn,
                        SMaDuAn = item.SMaDuAn,
                        SKhoiCong = item.SKhoiCong,
                        SKetThuc = item.SKetThuc,
                        DDateCreate = DateTime.Now,
                        IIdDonViThucHienDuAnId = Guid.Parse(SelectedDonVi.HiddenValue),
                        IIdMaDonViThucHienDuAn = SelectedDonVi.ValueItem,
                        SUserCreate = _sessionService.Current.Principal
                    });
                }

                if (!string.IsNullOrEmpty(item.STenHangMuc))
                {
                    string sKeyHangMuc = string.Format("{0}\n{1}\n{2}", item.SMaDuAn, item.STenHangMuc, item.SMaLoaiCongTrinh);
                    if (!dicDuAnHangMuc.ContainsKey(sKeyHangMuc))
                    {
                        dicDuAnHangMuc.Add(sKeyHangMuc, new VdtDaDuAnHangMuc()
                        {
                            Id = Guid.NewGuid(),
                            STenHangMuc = item.STenHangMuc,
                            IdLoaiCongTrinh = _dicLoaiCongTrinh[item.SMaLoaiCongTrinh].IIdLoaiCongTrinh,
                            IIdDuAnId = _dicDuAn[item.SMaDuAn].Id
                        });
                    }
                }

                if (!string.IsNullOrEmpty(item.IIdNguonVonId))
                {
                    string sKeyNguonVon = string.Format("{0}\n{1}", item.SMaDuAn, item.IIdNguonVonId);
                    if (!dicDuAnNguonVon.ContainsKey(sKeyNguonVon))
                    {
                        dicDuAnNguonVon.Add(sKeyNguonVon, new VdtDaNguonVon()
                        {
                            Id = Guid.NewGuid(),
                            IIdNguonVonId = int.Parse(item.IIdNguonVonId),
                            IIdDuAn = _dicDuAn[item.SMaDuAn].Id
                        });
                    }
                }
            }
            _projectManagerService.AddRange(_dicDuAn.Values);
            if(dicDuAnHangMuc.Count != 0)
            {
                _projectManagerService.InsertDuAnHangMuc(dicDuAnHangMuc.Values);
            }
            if(dicDuAnNguonVon.Count != 0)
            {
                _projectManagerService.AddRangeDuAnNguonVon(dicDuAnNguonVon.Values);
            }
        }

        private List<ImportErrorItem> ValidateItem(VdtDaThongTinDuAnImportModel item, int rowIndex, Dictionary<string, Guid> _dicMaDuAn)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                if (string.IsNullOrEmpty(item.SMaDuAn))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Tên dự án",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "tên dự án"),
                        Row = rowIndex
                    });
                }
                else 
                {
                    if (!_dicDuAn.ContainsKey(item.SMaDuAn))
                    {
                        _dicDuAn.Add(item.SMaDuAn, new VdtDaDuAn());
                        _dicHangMuc.Add(item.SMaDuAn, new Dictionary<string, VdtDaDuAnHangMuc>());
                        _dicNguonVon.Add(item.SMaDuAn, new Dictionary<string, VdtDaNguonVon>());
                    }

                    if (_dicMaDuAn.ContainsKey(item.SMaDuAn))
                    {
                        errors.Add(new ImportErrorItem()
                        {
                            ColumnName = "Mã dự án",
                            Error = string.Format(Resources.MsgDuAnExisted, item.SMaDuAn),
                            Row = rowIndex
                        });
                    }

                    if (string.IsNullOrEmpty(item.STenDuAn))
                    {
                        errors.Add(new ImportErrorItem()
                        {
                            ColumnName = "Tên dự án",
                            Error = string.Format(Resources.MsgErrorDataEmpty, "tên dự án"),
                            Row = rowIndex
                        });
                    }

                    if (string.IsNullOrEmpty(item.STenHangMuc) != string.IsNullOrEmpty(item.SMaLoaiCongTrinh))
                    {
                        if (string.IsNullOrEmpty(item.STenHangMuc))
                        {
                            errors.Add(new ImportErrorItem()
                            {
                                ColumnName = "Tên hạng mục",
                                Error = string.Format(Resources.MsgErrorDataEmpty, "tên hạng mục"),
                                Row = rowIndex
                            });
                        }
                        if (string.IsNullOrEmpty(item.SMaLoaiCongTrinh))
                        {
                            errors.Add(new ImportErrorItem()
                            {
                                ColumnName = "Mã loại công trình",
                                Error = string.Format(Resources.MsgErrorDataEmpty, "Mã loại công trình"),
                                Row = rowIndex
                            });
                        }
                    }
                    else if(!string.IsNullOrEmpty(item.STenHangMuc))
                    {
                        string sKeyHangMuc = string.Format("{0}\n{1}", item.STenHangMuc, item.SMaLoaiCongTrinh);
                        if (!_dicHangMuc[item.SMaDuAn].ContainsKey(sKeyHangMuc))
                        {
                            _dicHangMuc[item.SMaDuAn].Add(sKeyHangMuc, new VdtDaDuAnHangMuc());
                        }
                        if (!_dicLoaiCongTrinh.ContainsKey(item.SMaLoaiCongTrinh))
                        {
                            errors.Add(new ImportErrorItem()
                            {
                                ColumnName = "Mã loại công trình",
                                Error = string.Format(Resources.MsgErrorItemNotFound, "Mã loại công trình"),
                                Row = rowIndex
                            });
                        }
                    }

                    if (!string.IsNullOrEmpty(item.IIdNguonVonId))
                    {
                        if (!_dicNguonVon[item.SMaDuAn].ContainsKey(item.IIdNguonVonId))
                        {
                            _dicNguonVon[item.SMaDuAn].Add(item.IIdNguonVonId, new VdtDaNguonVon());
                        }
                        if (!_dicNsNguonVon.ContainsKey(item.IIdNguonVonId))
                        {
                            errors.Add(new ImportErrorItem()
                            {
                                ColumnName = "Mã nguồn vốn",
                                Error = string.Format(Resources.MsgErrorItemNotFound, "Mã nguồn vốn"),
                                Row = rowIndex
                            });
                        }
                    }
                }

                return errors;

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

                return new List<ImportErrorItem>();
            }
        }

        private void ShowError()
        {
            try
            {
                var errors = new HashSet<string>();
                int rowIndex = Items.IndexOf(SelectedItem);
                errors = _lstErrQlDuAn.Where(x => x.Row == (rowIndex + 1)).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string UpdateMaDuAn(string iIdMaDonViQuanLy)
        {
            string maDuAn = string.Empty;
            string sMaChuDauTu = "XXX";

            int indexGenerate = _duAnService.FindNextSoChungTuIndex();

            if (indexSMaDuAn >= indexGenerate)
            {
                indexGenerate = indexSMaDuAn + 1;
            }

            indexSMaDuAn = indexGenerate;

            string sGenerate = indexGenerate.ToString("D4");

            string sMaDuAn = string.Format("{0}-{1}-{2}", iIdMaDonViQuanLy, sMaChuDauTu, sGenerate);

            maDuAn = sMaDuAn;
            return maDuAn;
        }

        #endregion

        #region Helper
        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<ComboboxItem>();
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if(listDonVi != null)
            {
                ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi.Select(n=> new ComboboxItem()
                {
                    DisplayItem = n.TenDonVi,
                    ValueItem = n.IIDMaDonVi,
                    HiddenValue = n.Id.ToString()
                }));
            }
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void OnLoadLoaiCongTrinh()
        {
            _dicLoaiCongTrinh = new Dictionary<string, VdtDmLoaiCongTrinh>();
            var data = _loaicongtrinhService.FindAll();
            if (data == null) return;
            foreach(var item in data)
            {
                if (!_dicLoaiCongTrinh.ContainsKey(item.SMaLoaiCongTrinh))
                {
                    _dicLoaiCongTrinh.Add(item.SMaLoaiCongTrinh, item);
                }
            }
        }

        private void OnLoadNguonVon()
        {
            _dicNsNguonVon = new Dictionary<string, NsNguonNganSach>();
            var data = _nsNguonVonService.FindAll();
            if(data == null) return;
            foreach(var item in data)
            {
                if (!_dicNsNguonVon.ContainsKey((item.IIdMaNguonNganSach ?? 0).ToString()))
                {
                    _dicNsNguonVon.Add((item.IIdMaNguonNganSach ?? 0).ToString(), item);
                }
            }
        }
        #endregion
    }
}
