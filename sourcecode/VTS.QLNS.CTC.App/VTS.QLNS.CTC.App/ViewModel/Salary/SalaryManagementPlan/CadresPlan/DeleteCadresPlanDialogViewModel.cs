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
using VTS.QLNS.CTC.Utility.Enum;
using Microsoft.SqlServer.Management.XEvent;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.CadresPlan
{
    public class DeleteCadresPlanDialogViewModel : DialogViewModelBase<TlDmCanBoKeHoachModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDmCanBoKeHoachService _tlDmCanBoKeHoachService;
        private readonly ITlCanBoPhuCapKeHoachService _tlCanBoPhuCapKeHoachService;

        public override string Title => SalaryCadresPlanEnum.DELETE_ALL_CADRES_PLAN.Equals(DeleteTypeValue) ? "Xóa tất cả cán bộ nghỉ hưu năm kế hoạch" : "Xóa cán bộ nghỉ hưu năm kế hoạch theo đơn vị";
        public override string Description => SalaryCadresPlanEnum.DELETE_ALL_CADRES_PLAN.Equals(DeleteTypeValue) ? "Xóa tất cả cán bộ nghỉ hưu năm kế hoạch" : "Xóa cán bộ nghỉ hưu năm kế hoạch theo đơn vị";
        public override Type ContentType => typeof(View.Salary.SalaryManagementPlan.CadresPlan.DeleteCadresPlanDialog);
        public static SalaryCadresPlanEnum DeleteTypeValue { get; set; }
        public bool IsVisibilitySelectedUnit => SalaryCadresPlanEnum.DELETE_ALL_CADRES_PLAN.Equals(DeleteTypeValue);

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

        public DeleteCadresPlanDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
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
            _donViItems = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
            SelectedDonViItems = _donViItems.FirstOrDefault(x => x.MaDonVi == SelectedDonViItems.MaDonVi);

            OnPropertyChanged(nameof(DonViItems));
        }

        private void OnDeleteAllCadres()
        {
            try
            {
                DialogResult dialogResult = new DialogResult();
                var predicate = PredicateBuilder.True<TlDmCanBoKeHoach>();
                var maDonVi = SelectedDonViItems?.MaDonVi ?? "";
                predicate = predicate.And(x => x.Nam == int.Parse(YearSelected.ValueItem));
                predicate = predicate.And(x => x.Parent == maDonVi);
                var lstCanBoKeHoach = _tlDmCanBoKeHoachService.FindByCondition(predicate);

                if (SalaryCadresPlanEnum.DELETE_ALL_CADRES_PLAN.Equals(DeleteTypeValue))
                {
                    dialogResult = MessageBox.Show(string.Format("Đồng chí chắc chắn muốn xóa tất cả cán bộ kế hoạch năm {0}?", YearSelected.ValueItem), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
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
                else
                {
                    if (string.IsNullOrEmpty(maDonVi))
                    {
                        MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                        return;
                    }

                    dialogResult = MessageBox.Show(string.Format("Đồng chí chắc chắn muốn xóa cán bộ kế hoạch năm {0}, đơn vị {1}?", YearSelected.ValueItem, SelectedDonViItems.TenDonVi), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        _tlCanBoPhuCapKeHoachService.DeleteByUnitYear(int.Parse(YearSelected.ValueItem), maDonVi);
                        DialogHost.Close("RootDialog");
                        SavedAction?.Invoke(lstCanBoKeHoach);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
