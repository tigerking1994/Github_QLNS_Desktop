using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.App.ViewModel.Forex.ExchangeRate;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDanhMucNhaThau.ImportNhaThau;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDanhMucNhaThau
{
    public class ForexDmNhaThauIndexViewModel : GridViewModelBase<NhDmNhaThauModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhDmNhaThauService _iNhDmNhaThauService;
        private readonly INhDmNhaThauNguoiNhanService _iNhDmNhaThauNguoiNhanService;
        private readonly INhDmNhaThauNganHangService _iNhDmNhaThauNganHangService;
        private readonly IExportService _exportService;

        private ICollectionView _itemsCollectionView;
        public override string Name => "Danh mục nhà thầu";
        public override string Title => "Danh mục nhà thàu";
        public override string Description => "Danh sách nhà thầu";
        public override Type ContentType => typeof(View.Forex.ForexDanhMucNhaThau.ForexDmNhaThauIndex);
        public override PackIconKind IconKind => PackIconKind.Category;
        //public override string FuncCode => NSFunctionCode.CATEGORY_FOREX_NHA_THAU;

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public ForexDmNhaThauDialogViewModel ForexDmNhaThauDialogViewModel { get; set; }
        public ImportNhaThauViewModel ImportNhaThauViewModel { get; set; }

        public RelayCommand ExportTemplateCommand { get; }
        public RelayCommand ImportDataCommand { get; }

        public ForexDmNhaThauIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INhDmNhaThauService iNhDmNhaThauService,
            INhDmNhaThauNguoiNhanService iNhDmNhaThauNguoiNhanService,
            INhDmNhaThauNganHangService iNhDmNhaThauNganHangService,
            IExportService exportService,
            ForexDmNhaThauDialogViewModel forexDmNhaThauDialogViewModel,
            ImportNhaThauViewModel importNhaThauViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _iNhDmNhaThauService = iNhDmNhaThauService;
            _iNhDmNhaThauNguoiNhanService = iNhDmNhaThauNguoiNhanService;
            _iNhDmNhaThauNganHangService = iNhDmNhaThauNganHangService;
            _exportService = exportService;

            ImportNhaThauViewModel = importNhaThauViewModel;
            ForexDmNhaThauDialogViewModel = forexDmNhaThauDialogViewModel;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate());
            ExportTemplateCommand = new RelayCommand(obj => OnExportTemplate());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
        }

        public override void Init()
        {
            LoadData();
        }

        private void LoadData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                Items = new ObservableCollection<NhDmNhaThauModel>();
                e.Result = _iNhDmNhaThauService.FindAll().ToList();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<NhDmNhaThauModel>>(e.Result);
                    // Process when run completed. e.Result
                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null) return;

            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            DialogResult dialogResult = MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                _iNhDmNhaThauService.Delete(SelectedItem.Id);
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            ForexDmNhaThauDialogViewModel.Model = new NhDmNhaThauModel();
            ForexDmNhaThauDialogViewModel.Init();
            ForexDmNhaThauDialogViewModel.IsDetail = false;
            ForexDmNhaThauDialogViewModel.SavedAction = obj => this.OnRefresh();
            ForexDmNhaThauDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                ForexDmNhaThauDialogViewModel.Model = SelectedItem;
                ForexDmNhaThauDialogViewModel.Init();
                ForexDmNhaThauDialogViewModel.IsDetail = false;
                ForexDmNhaThauDialogViewModel.SavedAction = obj => this.OnRefresh();
                ForexDmNhaThauDialogViewModel.ShowDialog();
            }
        }

        protected void OnViewDetail()
        {
            if (SelectedItem != null)
            {
                ForexDmNhaThauDialogViewModel.Model = SelectedItem;
                ForexDmNhaThauDialogViewModel.IsDetail = true;
                ForexDmNhaThauDialogViewModel.Init();
                ForexDmNhaThauDialogViewModel.SavedAction = obj => this.OnRefresh();
                ForexDmNhaThauDialogViewModel.ShowDialog();
            }
        }

        public void OnExportTemplate()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    templateFileName = Path.Combine(ExportPrefix.PATH_NH_DM, ExportFileName.EPT_NH_DANHMUC_NHATHAU);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(Path.GetFileNameWithoutExtension(templateFileName));
                    var xlsFile = _exportService.Export<NhDmNhaThauModel>(templateFileName, new Dictionary<string, object>());
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>) e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
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

        public void OnImportData()
        {
            ImportNhaThauViewModel.Init();
            ImportNhaThauViewModel.SavedAction = obj =>
            {
                this.LoadData();
            };
            ImportNhaThauViewModel.ShowDialog();
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void OnRemoveFilter()
        {
            if (_itemsCollectionView != null) _itemsCollectionView.Refresh();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (SelectedItem != null)
            {
                OnViewDetail();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
        }

        public override void Dispose()
        {
            base.Dispose();

            // Clear data
            _itemsCollectionView = null;
            Items.Clear();
        }
    }
}
