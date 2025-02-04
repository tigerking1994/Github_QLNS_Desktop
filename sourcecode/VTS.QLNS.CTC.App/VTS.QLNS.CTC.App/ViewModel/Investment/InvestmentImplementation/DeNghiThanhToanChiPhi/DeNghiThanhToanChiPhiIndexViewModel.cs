using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.DeNghiThanhToanChiPhi;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.DeNghiThanhToanChiPhi
{
    public class DeNghiThanhToanChiPhiIndexViewModel : GridViewModelBase<VdtTtDeNghiThanhToanChiPhiIndexModel>
    {
        #region Private
        private static string[] _lstDonViInclude = new string[] { "0", "1" };
        private readonly IVdtTtDeNghiThanhToanChiPhiService _service;
        private readonly INsDonViService _donviService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private ICollectionView _view;
        #endregion

        #region Public
        //public override string GroupName => MenuItemContants.GROUP_ALLOCATION;
        //public override string Name => "Đề nghị thanh toán theo chi phí";
        public override string Name => "Thanh toán chi phí quản lý dự án";
        public override string Description => "Danh sách thông tin quản lý Thanh toán chi phí quản lý dự án";
        public override Type ContentType => typeof(View.Investment.InvestmentImplementation.DeNghiThanhToanChiPhi.DeNghiThanhToanChiPhiIndex);
        #endregion

        #region Items
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

        private string _sNamKeHoach;
        public string SNamKeHoach
        {
            get => _sNamKeHoach;
            set => SetProperty(ref _sNamKeHoach, value);
        }

        private string _sTenDuAn;
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }
        #endregion

        public DeNghiThanhToanChiPhiDialogViewModel DeNghiThanhToanChiPhiDialogViewModel;
        public DeNghiThanhToanChiPhiDetailViewModel DeNghiThanhToanChiPhiDetailViewModel;
        private readonly IVdtTtDeNghiThanhToanService _deNghiThanhToanService;


        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        #endregion

        public DeNghiThanhToanChiPhiIndexViewModel(
            IVdtTtDeNghiThanhToanService deNghiThanhToanService,
            IVdtTtDeNghiThanhToanChiPhiService service,
            INsDonViService donviService,
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            DeNghiThanhToanChiPhiDialogViewModel deNghiThanhToanChiPhiDialogViewModel,
            DeNghiThanhToanChiPhiDetailViewModel deNghiThanhToanChiPhiDetailViewModel)
        {
            _service = service;
            _donviService = donviService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _deNghiThanhToanService = deNghiThanhToanService;

            DeNghiThanhToanChiPhiDialogViewModel = deNghiThanhToanChiPhiDialogViewModel;
            DeNghiThanhToanChiPhiDialogViewModel.ParentPage = this;
            DeNghiThanhToanChiPhiDetailViewModel = deNghiThanhToanChiPhiDetailViewModel;
            DeNghiThanhToanChiPhiDetailViewModel.ParentPage = this;

            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            SearchCommand = new RelayCommand(obj => OnSearch());
        }

        #region Event
        public override void Init()
        {
            LoadDonVi();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            var lstData = _service.GetDeNghiThanhToanChiPhiIndex();
            Items = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanChiPhiIndexModel>>(lstData);
            int iIndex = 1;
            foreach (var item in Items)
            {
                item.SChungTuDeNghiThanhToan = _deNghiThanhToanService.Find(item.IIdDeNghiThanhToanId).SSoDeNghi;
                item.IRowIndex = iIndex;
                iIndex++;
            }
            _view = CollectionViewSource.GetDefaultView(Items);
            _view.Filter = IndexFilter;
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnAdd()
        {
            DeNghiThanhToanChiPhiDialogViewModel.Model = new VdtTtDeNghiThanhToanChiPhiIndexModel();
            DeNghiThanhToanChiPhiDialogViewModel.Init();
            DeNghiThanhToanChiPhiDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDetail((VdtTtDeNghiThanhToanChiPhiIndexModel)obj);
            };
            var view = new DeNghiThanhToanChiPhiDialog
            {
                DataContext = DeNghiThanhToanChiPhiDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnUpdate()
        {
            if (SelectedItem == null) return;
            DeNghiThanhToanChiPhiDialogViewModel.Model = SelectedItem;
            DeNghiThanhToanChiPhiDialogViewModel.Init();
            DeNghiThanhToanChiPhiDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDetail((VdtTtDeNghiThanhToanChiPhiIndexModel)obj);
            };
            var view = new DeNghiThanhToanChiPhiDialog
            {
                DataContext = DeNghiThanhToanChiPhiDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnDelete()
        {
            base.OnDelete();

            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.MsgConfirmDeleteDeNghiThanhToan);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenDetail((VdtTtDeNghiThanhToanChiPhiIndexModel)obj, true);
        }
        #endregion

        #region Helper
        public void OnSearch()
        {
            _view.Refresh();
        }

        private void OnResetFilter()
        {
            SelectedDonVi = null;
            STenDuAn = string.Empty;
            SNamKeHoach = string.Empty;
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(STenDuAn));
            OnPropertyChanged(nameof(SNamKeHoach));
            OnSearch();
        }

        private void OnOpenDetail(VdtTtDeNghiThanhToanChiPhiIndexModel SelectedItem, bool bIsDetail = false)
        {
            DeNghiThanhToanChiPhiDetailViewModel.BIsViewDetail = bIsDetail;
            DeNghiThanhToanChiPhiDetailViewModel.Model = SelectedItem;
            VdtTtDeNghiThanhToan vdtTtDeNghiThanhToanModel = _deNghiThanhToanService.Find(SelectedItem.IIdDeNghiThanhToanId);
            DeNghiThanhToanChiPhiDetailViewModel.FGiaTriDeNghiThanhToan = vdtTtDeNghiThanhToanModel.FGiaTriThanhToanTN + vdtTtDeNghiThanhToanModel.FGiaTriThanhToanNN;
            DeNghiThanhToanChiPhiDetailViewModel.FGiaTriDeNghiThuHoi = vdtTtDeNghiThanhToanModel.FGiaTriThuHoiTN + vdtTtDeNghiThanhToanModel.FGiaTriThuHoiNN;
            DeNghiThanhToanChiPhiDetailViewModel.Init();
            var view = new DeNghiThanhToanChiPhiDetail { DataContext = DeNghiThanhToanChiPhiDetailViewModel };
            view.ShowDialog();
            LoadData();
        }

        private bool IndexFilter(object obj)
        {
            int iNamKeHoach = 0;
            if (!(obj is VdtTtDeNghiThanhToanChiPhiIndexModel temp)) return true;
            var bCondition = true;
            if (SelectedDonVi != null)
            {
                bCondition &= (temp.IIdMaDonViQuanLy == SelectedDonVi.ValueItem);
            }
            if (!string.IsNullOrEmpty(STenDuAn))
            {
                bCondition = bCondition && !string.IsNullOrEmpty(temp.STenDuAn) && temp.STenDuAn.ToLower().Contains(STenDuAn.ToLower());
            }
            if (!string.IsNullOrEmpty(SNamKeHoach) && int.TryParse(SNamKeHoach, out iNamKeHoach))
            {
                bCondition &= (temp.INamKeHoach == iNamKeHoach);
            }
            return bCondition;
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _service.Delete(SelectedItem.Id);
            LoadData();
        }

        private void LoadDonVi()
        {
            var datas = _donviService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => _lstDonViInclude.Contains(n.Loai))
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi) });
            ItemsDonVi = new ObservableCollection<ComboboxItem>(datas);
            SelectedDonVi = null;
            OnPropertyChanged(nameof(ItemsDonVi));
            OnPropertyChanged(nameof(SelectedDonVi));
        }
        #endregion
    }
}
