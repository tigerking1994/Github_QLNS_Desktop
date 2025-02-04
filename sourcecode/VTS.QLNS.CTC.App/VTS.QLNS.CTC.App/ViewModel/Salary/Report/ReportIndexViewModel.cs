using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Salary.Report;
using VTS.QLNS.CTC.App.ViewModel.Salary.Report.ListReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Report.ReportSalaryPursuit
{
    public class ReportIndexViewModel : GridViewModelBase<TlBaoCaoModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlBaoCaoService _tlBaoCaoService;
        private ICollectionView _dataCollectionView;

        public override string FuncCode => NSFunctionCode.SALARY_MANAGEMENT_REPORTS_INDEX;
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string Name => "Danh sách báo cáo";
        public override string Description => "Danh sách báo cáo";
        public override PackIconKind IconKind => PackIconKind.ClipboardFileOutline;
        public override Type ContentType => typeof(ReportIndex);

        public ReportDialogViewModel ListReportDialogViewModel { get; set; }

        public RelayCommand ShowPopupExportCommand { get; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand EditCommand { get; set; }

        private string _tenBaoCao;
        public string TenBaoCao
        {
            get => _tenBaoCao;
            set => SetProperty(ref _tenBaoCao, value);
        }

        public ReportIndexViewModel(
            ISessionService sessionService,
            ILog logger,
            IExportService exportService,
            IMapper mapper,
            ITlBaoCaoService tlBaoCaoService,
            ReportDialogViewModel listReportDialogViewModel)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _exportService = exportService;
            _tlBaoCaoService = tlBaoCaoService;

            ListReportDialogViewModel = listReportDialogViewModel;

            ShowPopupExportCommand = new RelayCommand(o => OnOpenExportDialog());
            SearchCommand = new RelayCommand(obj => _dataCollectionView.Refresh());
            EditCommand = new RelayCommand(obj => Edit());
        }

        private void Edit()
        {
            try
            {
                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(Resources.ConfirmChange);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    var listEdit = Items.Where(i => i.IsModified);
                    var dataSave = _mapper.Map<IEnumerable<TlBaoCao>>(listEdit).ToList();
                    foreach (var item in dataSave)
                    {
                        item.IsModified = false;
                    }
                    _tlBaoCaoService.UpdateBaoCao(dataSave);
                    OnRefresh();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(TlBaoCaoModel.TenBaoCao) || args.PropertyName == nameof(TlBaoCaoModel.Note))
            {
                TlBaoCaoModel item = (TlBaoCaoModel)sender;
                item.IsModified = true;
                OnPropertyChanged(nameof(Items));
            }
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(10);
            LoadData();
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                var data = _tlBaoCaoService.FindAll();
                Items = _mapper.Map<ObservableCollection<TlBaoCaoModel>>(data);
                var listParent = Items.Where(x => x.IsParent == true).OrderBy(x => int.Parse(x.MaBaoCao)).ToList();
                var listChild = Items.Where(x => x.IsParent == false).OrderByDescending(x => float.Parse(x.MaBaoCao)).ToList();
                var notChildOrParent = Items.Where(x => x.IsParent == false && x.MaParent == null).OrderBy(x => float.Parse(x.MaBaoCao)).ToList();
                if (listParent != null && listParent.Count > 0)
                {
                    foreach (var item in listParent)
                    {
                        item.IsHangCha = true;
                    }
                    if (listChild != null && listChild.Count > 0)
                    {
                        foreach (var item in listChild)
                        {
                            int indexParent = -1;
                            indexParent = listParent.IndexOf(listParent.Where(x => x.MaBaoCao == item.MaParent).FirstOrDefault());
                            if (indexParent > -1)
                            {
                                listParent.Insert(indexParent + 1, item);
                            }
                        }
                    }
                }
                listParent.AddRange(notChildOrParent);
                foreach (var item in listParent)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }
                Items = new ObservableCollection<TlBaoCaoModel>(listParent);
                _dataCollectionView = CollectionViewSource.GetDefaultView(Items);
                _dataCollectionView.Filter = DataCollectionView_Filter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenExportDialog()
        {
            try
            {
                if (SelectedItem != null && SelectedItem.IsParent == false)
                {
                    if (!SelectedItem.MaBaoCao.Equals("12.5") && !SelectedItem.MaBaoCao.Equals("12.6"))
                    {
                        ListReportDialogViewModel.Model = SelectedItem;
                        ListReportDialogViewModel.Init();
                        ListReportDialogViewModel.SavedAction = obj => this.OnRefresh();
                        ListReportDialogViewModel.ShowDialogHost();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool DataCollectionView_Filter(object obj)
        {
            if (string.IsNullOrEmpty(_tenBaoCao))
            {
                return true;
            }
            return obj is TlBaoCaoModel item && item.TenBaoCao.ToLower().Contains(_tenBaoCao.ToLower().Trim());
        }
    }
}
