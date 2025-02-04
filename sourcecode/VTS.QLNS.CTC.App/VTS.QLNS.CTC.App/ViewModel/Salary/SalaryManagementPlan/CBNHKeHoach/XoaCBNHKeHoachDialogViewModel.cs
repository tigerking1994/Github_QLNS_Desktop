using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.CBNHKeHoach
{
    public class XoaCBNHKeHoachDialogViewModel : DialogViewModelBase<TlDsCBNHKeHoachModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDsCBNHKeHoachService _tlDsCBNHKeHoachService;

        public override string Title => "Xóa tất cả cán bộ năm kế hoạch";
        public override string Description => "Xóa tất cả cán bộ năm kế hoạch";
        public override Type ContentType => typeof(View.Salary.SalaryManagementPlan.CBNHKeHoach.XoaCBNHKeHoachDialog);

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _YearSelected;
        public ComboboxItem YearSelected
        {
            get => _YearSelected;
            set => SetProperty(ref _YearSelected, value);
        }

        private ObservableCollection<TlDmDonViModel> _donViItems;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViModel _selectedDonViItems;
        public TlDmDonViModel SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                SetProperty(ref _selectedDonViItems, value);
            }
        }

        public RelayCommand DeleteAllCommand { get; }

        public XoaCBNHKeHoachDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlDsCBNHKeHoachService tlDsCBNHKeHoachService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;

            _tlDmDonViService = tlDmDonViService;
            _tlDsCBNHKeHoachService = tlDsCBNHKeHoachService;

            DeleteAllCommand = new RelayCommand(obj => OnDeleteAllCadres());
        }

        public override void Init()
        {
            base.Init();
            LoadYears();
            LoadDonVi();
        }

        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            for (int i = 1970; i <= 2050; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        private void LoadDonVi()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            _donViItems = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
            SelectedDonViItems = _donViItems.FirstOrDefault(x => x.MaDonVi == SelectedDonViItems.MaDonVi);

            OnPropertyChanged(nameof(DonViItems));
        }

        private void OnDeleteAllCadres()
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show(string.Format("Đồng chí chắc chắn muốn xóa tất cả cán bộ kế hoạch năm {0}?", YearSelected.ValueItem), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    var predicate = PredicateBuilder.True<TlDsCBNHKeHoach>();
                    predicate = predicate.And(x => x.Nam == int.Parse(YearSelected.ValueItem));
                    //predicate = predicate.And(x => x.Parent == SelectedDonViItems.MaDonVi);

                    var lstCBNHKeHoach = _tlDsCBNHKeHoachService.FindByCondition(predicate);

                    _tlDsCBNHKeHoachService.DeleteByYear(int.Parse(YearSelected.ValueItem));
                    DialogHost.Close("RootDialog");
                    SavedAction?.Invoke(lstCBNHKeHoach);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
