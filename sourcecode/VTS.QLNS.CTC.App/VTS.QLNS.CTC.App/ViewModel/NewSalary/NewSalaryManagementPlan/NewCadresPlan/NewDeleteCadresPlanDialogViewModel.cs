using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewCadresPlan
{
    public class NewDeleteCadresPlanDialogViewModel : DialogViewModelBase<TlDmCanBoKeHoachModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlDmCanBoKeHoachService _tlDmCanBoKeHoachService;
        private readonly ITlCanBoPhuCapKeHoachService _tlCanBoPhuCapKeHoachService;

        public override string Title => "Xóa tất cả cán bộ năm kế hoạch";
        public override string Description => "Xóa tất cả cán bộ năm kế hoạch";
        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagementPlan.NewCadresPlan.NewDeleteCadresPlanDialog);

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

        private ObservableCollection<TlDmDonViNq104Model> _donViItems;
        public ObservableCollection<TlDmDonViNq104Model> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViNq104Model _selectedDonViItems;
        public TlDmDonViNq104Model SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                SetProperty(ref _selectedDonViItems, value);
            }
        }

        public RelayCommand DeleteAllCommand { get; }

        public NewDeleteCadresPlanDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlDmCanBoKeHoachService tlDmCanBoKeHoachService,
            ITlCanBoPhuCapKeHoachService tlCanBoPhuCapKeHoachService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;

            _tlDmDonViService = tlDmDonViService;
            _tlDmCanBoKeHoachService = tlDmCanBoKeHoachService;
            _tlCanBoPhuCapKeHoachService = tlCanBoPhuCapKeHoachService;

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
            _donViItems = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
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
                    var predicate = PredicateBuilder.True<TlDmCanBoKeHoach>();
                    predicate = predicate.And(x => x.Nam == int.Parse(YearSelected.ValueItem));
                    predicate = predicate.And(x => x.Parent == SelectedDonViItems.MaDonVi);

                    var lstCanBoKeHoach = _tlDmCanBoKeHoachService.FindByCondition(predicate);

                    _tlCanBoPhuCapKeHoachService.DeleteByYear(int.Parse(YearSelected.ValueItem));
                    _tlDmCanBoKeHoachService.DeleteByYear(int.Parse(YearSelected.ValueItem));
                    //foreach (var item in lstCanBoKeHoach)
                    //{
                    //    _tlCanBoPhuCapKeHoachService.DeleteByMaCanBo(item.MaCanBo);
                    //    _tlDmCanBoKeHoachService.Delete(item.Id);
                    //}
                    DialogHost.Close("RootDialog");
                    SavedAction?.Invoke(lstCanBoKeHoach);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
