using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation
{
    public class AllocationSummaryViewModel : ViewModelBase
    {
        private readonly INsDonViService _donViService;
        private readonly ICpChungTuService _chungTuService;
        private readonly ICpChungTuChiTietService _chungTuChiTietService;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public bool IsEditProcess = false;
        public override string Name => "TỔNG HỢP CẤP PHÁT";
        public override string Title => "TỔNG HỢP CHỨNG TỪ";
        public override string Description => "Tổng hợp chứng từ cấp phát";
        public override Type ContentType => typeof(View.Budget.Allocation.AllocationSummary);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public bool IsEnableView { get; set; }
        public string IsEnableValue => IsEnableView ? "True" : "False";
        private bool _isCapPhatToanDonVi;


        private AllocationModel _allocation;
        public AllocationModel Allocation
        {
            get => _allocation;
            set => SetProperty(ref _allocation, value);
        }

        private ObservableCollection<AllocationModel> _dataAllocation;
        public ObservableCollection<AllocationModel> DataAllocation
        {
            get => _dataAllocation;
            set => SetProperty(ref _dataAllocation, value);
        }

        private AllocationModel _selectedAllocation;
        public AllocationModel SelectedAllocation
        {
            get => _selectedAllocation;
            set
            {
                SetProperty(ref _selectedAllocation, value);
            }
        }

        public RelayCommand SaveCommand { get; }

        public AllocationSummaryViewModel(
            INsDonViService donViService,
            ICpChungTuService chungTuService,
            ISessionService sessionService,
            ICpChungTuChiTietService chungTuChiTietService,
            ILog logger,
            IDanhMucService danhMucService,
            IMapper mapper)
        {
            _donViService = donViService;
            _chungTuChiTietService = chungTuChiTietService;
            _chungTuService = chungTuService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _mapper = mapper;
            _logger = logger;
            SaveCommand = new RelayCommand(o => OnSave());
        }

        public override void Init()
        {
            try
            {
                IsEnableView = true;
                if (Allocation == null) Allocation = new Model.AllocationModel();
                if (Allocation.Id == Guid.Empty)
                {
                    Allocation = new Model.AllocationModel();
                    int soChungTuIndex = _chungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork, _sessionService.Current.Budget, _sessionService.Current.YearOfBudget);
                    Allocation.SoChungTu = "CP-" + soChungTuIndex.ToString("D3");
                    Allocation.NgayChungTu = DateTime.Now;
                    Allocation.MoTa = "Chi tiết chứng từ";
                }

                OnPropertyChanged(nameof(IsEnableView));
                OnPropertyChanged(nameof(IsEnableValue));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetLevelSummary(List<AllocationModel> listChungTuChild)
        {
            if (listChungTuChild == null || listChungTuChild.Count == 0)
                return string.Empty;
            if (listChungTuChild.Any(n => n.ChiTietToi == NSChiTietToi.DB_VALUE_NGANH))
                return NSChiTietToi.DB_VALUE_NGANH;
            if (listChungTuChild.Any(n => n.ChiTietToi == NSChiTietToi.DB_VALUE_TIEU_MUC))
                return NSChiTietToi.DB_VALUE_TIEU_MUC;
            if (listChungTuChild.Any(n => n.ChiTietToi == NSChiTietToi.DB_VALUE_MUC))
                return NSChiTietToi.DB_VALUE_MUC;
            return NSChiTietToi.DB_VALUE_NGANH;
        }

        private void CreateDetailSummary(DonVi donVi0, NsCpChungTu chungTu)
        {
            _chungTuChiTietService.CreateVoudcherSummary(string.Join(",", DataAllocation.Select(n => n.Id.ToString()).ToList())
                        , donVi0.IIDMaDonVi, _sessionService.Current.Principal, _sessionService.Current.YearOfWork,
                        _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, chungTu.Id.ToString());
        }

        private List<string> CheckSummary()
        {
            var predicate = PredicateBuilder.True<NsCpChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.SDSSoChungTuTongHop));

            List<NsCpChungTu> chungTu = _chungTuService.FindByCondition(predicate).ToList();
            List<string> listSoChungTuSummary = new List<string>();
            foreach (var item in chungTu)
            {
                listSoChungTuSummary.AddRange(item.SDSSoChungTuTongHop.Split(",").ToList());
            }
            List<string> listResult = listSoChungTuSummary.Where(x => DataAllocation.Select(n => n.SoChungTu).ToList().Contains(x)).ToList();
            return listResult;
        }

        public override void OnSave()
        {
            try
            {
                if (Allocation.Id != Guid.Empty)
                {
                    NsCpChungTu entity = _chungTuService.FindById(Allocation.Id);
                    entity.SMoTa = Allocation.MoTa;
                    _chungTuService.Update(entity);

                    DialogHost.Close(SystemConstants.ROOT_DIALOG);
                    SavedAction?.Invoke(_mapper.Map<AllocationModel>(entity));
                }
                else
                {
                    DonVi donVi0 = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
                    if (donVi0 != null)
                    {
                        if (DataAllocation == null || DataAllocation.Count() == 0)
                        {
                            return;
                        }

                        List<string> listSummaryHistory = CheckSummary();
                        if (listSummaryHistory != null && listSummaryHistory.Count > 0)
                        {
                            MessageBoxHelper.Warning(string.Format(Resources.MsgSumaryWarning, string.Join(",", listSummaryHistory)));
                            return;
                        }

                        List<string> dsLns = new List<string>();
                        foreach (AllocationModel item in DataAllocation.Where(n => n.IsLocked && n.Selected))
                        {
                            if (!string.IsNullOrEmpty(item.Lns))
                            {
                                dsLns.AddRange(item.Lns.Split(","));
                            }
                        }

                        int soChungTuIndex = _chungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork, _sessionService.Current.Budget, _sessionService.Current.YearOfBudget);
                        NsCpChungTu entity = new NsCpChungTu();
                        entity.SSoChungTu = Allocation.SoChungTu;
                        entity.SMoTa = Allocation.MoTa;
                        entity.DNgayChungTu = Allocation.NgayChungTu;
                        entity.ISoChungTuIndex = soChungTuIndex;
                        entity.SDsidMaDonVi = donVi0.IIDMaDonVi;
                        entity.SDslns = string.Join(",", dsLns.Distinct().ToList());
                        string chiTietToi = GetLevelSummary(DataAllocation.ToList());
                        entity.NChiTietToi = chiTietToi;
                        entity.ITypeMoTa = DynamicMLNS.FormatLevelAllocation(chiTietToi);
                        entity.INamLamViec = _sessionService.Current.YearOfWork;
                        entity.IIdMaNguonNganSach = _sessionService.Current.Budget;
                        entity.INamNganSach = _sessionService.Current.YearOfBudget;
                        entity.SDSSoChungTuTongHop = string.Join(",", DataAllocation.Select(n => n.SoChungTu).ToList());
                        entity.ILoai = int.TryParse(DataAllocation.FirstOrDefault().ILoai, out var val) ? val : -1;
                        entity.IIdMaDmcapPhat = DataAllocation.First().LoaiCap;
                        entity.DNgayTao = DateTime.Now;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        _chungTuService.Add(entity);

                        CreateDetailSummary(donVi0, entity);

                        _chungTuService.UpdateTotalCPChungTu(entity.Id.ToString(), _sessionService.Current.Principal);

                        DialogHost.CloseDialogCommand.Execute(null, null);
                        MessageBoxHelper.Info(Resources.MsgSumaryDone);
                        SavedAction?.Invoke(_mapper.Map<AllocationModel>(entity));
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
