using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.View.SystemAdmin.SysLog;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.SysLog
{
    public class SysLogIndexViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly ISysAuditLogService _sysAuditLogService;
        private ICollectionView _dataCollectionView;

        public override string FuncCode => NSFunctionCode.SYSTEM_SYSLOG_INDEX;
        public override string Name => "Nhật ký dữ liệu";
        public override string Description => "Nhật ký dữ liệu";
        public override string Title => "Nhật ký dữ liệu";
        public override Type ContentType => typeof(SysLogIndex);
        public override PackIconKind IconKind => PackIconKind.Database;

        public Model.HTNhatKyCapNhatDuLieuModel SearchModel { get; set; }
        public ObservableCollection<Model.HTNhatKyCapNhatDuLieuModel> SysLogs { get; set; }

        private Model.HTNhatKyCapNhatDuLieuModel _selectedItem;
        public Model.HTNhatKyCapNhatDuLieuModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnPropertyChanged(nameof(IsEdit));
            }
        }

        public bool IsEdit => SelectedItem != null;

        public RelayCommand SearchCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand SelectionDoubleClickCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public SysLogIndexViewModel(ISysAuditLogService sysAuditLogService, IMapper mapper)
        {
            _sysAuditLogService = sysAuditLogService;
            _mapper = mapper;
            SearchCommand = new RelayCommand(obj => OnSearch());
            RefreshCommand = new RelayCommand(obj => OnRefresh());
            SelectionDoubleClickCommand = new RelayCommand(obj => OnViewLogDetail());
            DeleteCommand = new RelayCommand(obj => OnDelete());
        }

        public override void Init()
        {
            base.Init();
            SearchModel = new Model.HTNhatKyCapNhatDuLieuModel();
            DateTime now = DateTime.Now;
            SearchModel.StartTime = now.AddDays(-7);
            SearchModel.EndTime = now;
            ObservableCollection<Core.Domain.HtNhatKyCapNhatDuLieu> sysLogs = new ObservableCollection<Core.Domain.HtNhatKyCapNhatDuLieu>(_sysAuditLogService.FindAll(t => true).OrderBy(t => t.StartTime));
            SysLogs = _mapper.Map<ObservableCollection<Model.HTNhatKyCapNhatDuLieuModel>>(sysLogs);
            _dataCollectionView = CollectionViewSource.GetDefaultView(SysLogs);
            _dataCollectionView.Filter = ItemsViewFilter;
            OnPropertyChanged(nameof(SysLogs));
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            HTNhatKyCapNhatDuLieuModel item = (Model.HTNhatKyCapNhatDuLieuModel)obj;
            if (!string.IsNullOrEmpty(SearchModel.Account))
                result = result && (item.Account.ToLower().Contains(SearchModel.Account.ToLower()) || item.UserName.ToLower().Contains(SearchModel.Account.ToLower()));
            if (SearchModel.StartTime != null && SearchModel.StartTime.HasValue)
                result = result && item.StartTime.Value.Date >= SearchModel.StartTime.Value;
            if (SearchModel.EndTime != null && SearchModel.EndTime.HasValue)
                result = result && item.EndTime.Value.Date <= SearchModel.EndTime.Value;
            return result;
        }

        private void OnRefresh()
        {
            SearchModel = new Model.HTNhatKyCapNhatDuLieuModel();
            DateTime now = DateTime.Now;
            SearchModel.StartTime = now.AddDays(-7);
            SearchModel.EndTime = now;
            _dataCollectionView.Refresh();
        }

        private void OnSearch()
        {
            _dataCollectionView.Refresh();
        }

        private void OnViewLogDetail()
        {
            if (SelectedItem == null)
            {
                return;
            }
            GenericControlCustomDetailViewModel genericControlCustomDetailViewModel = new GenericControlCustomDetailViewModel(SelectedItem) { Title = Title, Description = SelectedItem.DetailInfoModalTitle };
            GenericControlCustomViewDetail genericControlCustomViewDetail = new GenericControlCustomViewDetail()
            {
                DataContext = genericControlCustomDetailViewModel
            };
            System.Threading.Tasks.Task<object> dialog = DialogHost.Show(genericControlCustomViewDetail, "RootDialog");
        }

        private void OnDelete()
        {
            if (SelectedItem == null)
            {
                return;
            }
            NSMessageBoxViewModel messeageBox = new NSMessageBoxViewModel("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận", NSMessageBoxButtons.YesNo, ActionHanlder);
            DialogHost.Show(messeageBox.Content, "RootDialog");
        }

        private void ActionHanlder(NSDialogResult result)
        {
            if (result == NSDialogResult.Yes)
            {
                _sysAuditLogService.Delete(SelectedItem.Id);
                ObservableCollection<Core.Domain.HtNhatKyCapNhatDuLieu> sysLogs = new ObservableCollection<Core.Domain.HtNhatKyCapNhatDuLieu>(_sysAuditLogService.FindAll(t => true).OrderBy(t => t.StartTime));
                SysLogs = _mapper.Map<ObservableCollection<Model.HTNhatKyCapNhatDuLieuModel>>(sysLogs);
                _dataCollectionView = CollectionViewSource.GetDefaultView(SysLogs);
                _dataCollectionView.Filter = ItemsViewFilter;
                OnPropertyChanged(nameof(SysLogs));
            }
        }
    }
}
