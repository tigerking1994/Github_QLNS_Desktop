using AutoMapper;
using AutoMapper.Internal;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.RegularSettlement
{
    public class RegularGetEstimatesDialogViewModel : DialogViewModelBase<DtChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDtChungTuService _estimationService;
        private SessionInfo _sessionInfo;
        public Action<object> ChooseAction;
        private readonly INsDonViService _donViService;

        public override Type ContentType => typeof(View.Salary.Settlement.RegularSettlement.RegularGetEstimatesDialog);

        private ObservableCollection<DtChungTuModel> _itemsDtChungTu;
        public ObservableCollection<DtChungTuModel> ItemsDtChungTu
        {
            get => _itemsDtChungTu;
            set => SetProperty(ref _itemsDtChungTu, value);
        }

        private DtChungTuModel _selectedDtChungTu;
        public DtChungTuModel SelectedDtChungTu
        {
            get => _selectedDtChungTu;
            set => SetProperty(ref _selectedDtChungTu, value);
        }

        public bool IsEnabled => SelectedDtChungTu != null;

        public Dictionary<string, string> DictIdChungTu = new Dictionary<string, string>();
        public TlQtChungTuModel tlQtChungTuModel;

        public RelayCommand ChooseCommand { get; }

        public RegularGetEstimatesDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService donViService,
            INsDtChungTuService nsDtChungTuService)
        {
            _logger = logger;
            _mapper = mapper;
            _donViService = donViService;
            _sessionService = sessionService;

            _estimationService = nsDtChungTuService;

            ChooseCommand = new RelayCommand(o => OnChoose());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
        }

        private void LoadData()
        {
            EstimationVoucherCriteria condition = new EstimationVoucherCriteria
            {
                EstimationType = SoChungTuType.EstimateDivision,
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                BudgetSource = _sessionInfo.Budget,
                Status = (int)Status.ACTIVE,
                UserName = _sessionInfo.Principal,
                VoucherType = 1
            };
            var listChungTu = _estimationService.FindByConditionInLuongView(condition).ToList();
            ItemsDtChungTu = new ObservableCollection<DtChungTuModel>();
            if (_donViService.IsDonViCha(tlQtChungTuModel.MaDonVi, tlQtChungTuModel.Nam))
            {
                ItemsDtChungTu = _mapper.Map<ObservableCollection<DtChungTuModel>>(listChungTu.Where(x => 
                    x.BKhoa && 
                    x.ILoai == 0 && 
                    (x.SDonViNhanDuLieu == null || !x.SDonViNhanDuLieu.Split(',').Contains(tlQtChungTuModel.MaDonVi)) && 
                    x.SDsidMaDonVi != null && x.SDsidMaDonVi.Split(',').Contains(tlQtChungTuModel.MaDonVi)
                ));
            } else
            {
                ItemsDtChungTu = _mapper.Map<ObservableCollection<DtChungTuModel>>(listChungTu.Where(x => 
                    x.BKhoa && 
                    x.ILoai == 1 && 
                    (x.SDonViNhanDuLieu == null || !x.SDonViNhanDuLieu.Split(',').Contains(tlQtChungTuModel.MaDonVi)) &&
                    x.SDsidMaDonVi != null && x.SDsidMaDonVi.Split(',').Contains(tlQtChungTuModel.MaDonVi))
                );
            }
            ItemsDtChungTu.Where(n=>n.IIdDotNhan != null).ForAll(x => x.ListSoChungTuDotNhan = string.Join(",", x.IIdDotNhan.Split(",").Select(e => DictIdChungTu.GetValueOrDefault(e, string.Empty))));

            if (ItemsDtChungTu != null && ItemsDtChungTu.Count > 0)
            {
                SelectedDtChungTu = ItemsDtChungTu.FirstOrDefault();
            }
        }

        private void OnChoose()
        {
            MessageBoxHelper.Info("Lấy dữ liệu thành công");
            DialogHost.Close("RegularSettlementDetail");
            ChooseAction?.Invoke(SelectedDtChungTu);
        }
    }
}
