using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using System.Linq.Expressions;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Plan;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Plan
{
    public class PlanBudgetBeginYearSummaryViewModel : DialogViewModelBase<TnDtdnChungTuModel>
    {
        private readonly ITnDtdnChungTuService _tnDtdnChungTuService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly ITnDtdnChungTuChiTietService _tnDtdnChungTuChiTietService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private ICollectionView _nsDonViModelsView;
        private SessionInfo _sessionInfo;

        public override string Name => "Tổng hợp dự toán đầu năm";
        public override Type ContentType => typeof(PlanBudgetBeginYearSummary);
        public override PackIconKind IconKind => PackIconKind.Sigma;
        public override string Title => "TỔNG HỢP CHỨNG TỪ LẬP DỰ TOÁN NGÂN SÁCH NĂM";
        public override string Description => "TỔNG HỢP CHỨNG TỪ LẬP DỰ TOÁN NGÂN SÁCH NĂM";

        public List<string> ListIdDonViHasCt { get; set; }
        private string SDSDonViTongHop { get; set; }
        public List<TnDtdnChungTuModel> ListIdsSktChungTuSummary { get; set; }
        public string LoaiChungTu { get; set; }

        private ObservableCollection<TnDtdnChungTuModel> _dataPlan;
        public ObservableCollection<TnDtdnChungTuModel> DataPlan
        {
            get => _dataPlan;
            set => SetProperty(ref _dataPlan, value);
        }

        private TnDtdnChungTuModel _selectedPlan;
        public TnDtdnChungTuModel SelectedPlan
        {
            get => _selectedPlan;
            set
            {
                SetProperty(ref _selectedPlan, value);
            }
        }

        private string _thucHienThu;
        public string ThucHienThu
        {
            get => _thucHienThu;
            set
            {
                SetProperty(ref _thucHienThu, value);
            }
        }
        private string _duToanNam;
        public string DuToanNam
        {
            get => _duToanNam;
            set
            {
                SetProperty(ref _duToanNam, value);
            }
        }
        private string _uocThucHien;
        public string UocThucHien
        {
            get => _uocThucHien;
            set
            {
                SetProperty(ref _uocThucHien, value);
            }
        }
        private string _duToanThu;
        public string DuToanThu
        {
            get => _duToanThu;
            set
            {
                SetProperty(ref _duToanThu, value);
            }
        }

        public PlanBudgetBeginYearSummaryViewModel(INsDonViService nsDonViService,
            ITnDtdnChungTuService tnDtdnChungTuService,
            ITnDtdnChungTuChiTietService tnDtdnChungTuChiTietService,
            IMapper mapper,
            ISktSoLieuService sktSoLieuService,
            ISessionService sessionService,
            ISysAuditLogService log)
        {
            _sessionService = sessionService;
            _tnDtdnChungTuService = tnDtdnChungTuService;
            _sktSoLieuService = sktSoLieuService;
            _tnDtdnChungTuChiTietService = tnDtdnChungTuChiTietService;
            _nsDonViService = nsDonViService;
            _log = log;
            _mapper = mapper;
        }

        private string GetListDonViTongHop()
        {
            List<string> listChungTu = DataPlan.Where(n => n.Selected && n.BKhoa).Select(n => n.IIdMaDonVi).ToList();
            if (listChungTu != null && listChungTu.Count > 0)
            {
                return string.Join(",", listChungTu);
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetListChungTuTongHop(ref string lnsResult)
        {
            List<string> listChungTu = DataPlan.Where(n => n.Selected && n.BKhoa).Select(n => n.SSoChungTu).ToList();
            if (listChungTu != null && listChungTu.Count > 0)
            {
                List<string> listLNS = new List<string>();
                List<string> listMaDonVi = new List<string>();
                foreach (var item in DataPlan.Where(n => n.Selected && n.BKhoa))
                {
                    if (!string.IsNullOrEmpty(item.IIdMaDonVi)) listMaDonVi.Add(item.IIdMaDonVi);
                    if (!string.IsNullOrEmpty(item.SDSLNS))
                        listLNS.AddRange(item.SDSLNS.Split(","));
                }
                lnsResult = string.Join(",", listLNS.Distinct().ToList());
                SDSDonViTongHop = string.Join(",", listMaDonVi.Distinct());
                return string.Join(",", listChungTu);
            }
            else
            {
                return string.Empty;
            }
        }

        public Expression<Func<TnDtdnChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<TnDtdnChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);

            return predicate;
        }

        public override void OnSave()
        {
            if (DataPlan != null && DataPlan.Where(n => n.Selected && n.BKhoa).Count() > 0)
            {
                string lns = string.Empty;
                string listChungTuTongHop = GetListChungTuTongHop(ref lns);
                if (!string.IsNullOrEmpty(listChungTuTongHop))
                {
                    DonVi root = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
                    TnDtdnChungTu entity = new TnDtdnChungTu();
                    _mapper.Map(Model, entity);
                    entity.SMoTa = Model.SMoTa;
                    entity.DNgayChungTu = Model.DNgayChungTu;
                    entity.IIdMaDonVi = root.IIDMaDonVi;
                    entity.INamLamViec = _sessionService.Current.YearOfWork;
                    entity.INamNganSach = _sessionService.Current.YearOfBudget;
                    entity.IIdMaNguonNganSach = _sessionService.Current.Budget;
                    entity.DNgayTao = DateTime.Now;
                    entity.SDSLNS = lns;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.SDSSoChungTuTongHop = listChungTuTongHop;
                    entity.SDSDonViTongHop = SDSDonViTongHop;
                    _tnDtdnChungTuService.Add(entity);
                    //chi tiet
                    _tnDtdnChungTuService.CreateDataReportTotalSummary(entity.Id.ToString(), _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                       _sessionService.Current.Budget, root.IIDMaDonVi, string.Join(",", DataPlan.Where(n => n.Selected && n.BKhoa).Select(n => n.Id.ToString()).ToList()),
                       _sessionService.Current.Principal);

                    DialogHost.CloseDialogCommand.Execute(null, null);
                    //DialogHost.Close("RootDialog");
                    //MessageBoxHelper.Info(Resources.MsgSumaryDone);
                    //SavedAction?.Invoke(null);
                    SavedAction?.Invoke(_mapper.Map<TnDtdnChungTuModel>(entity));
                }
            }
        }


        public override void LoadData(params object[] args)
        {
            base.LoadData(args);

            if (Model.Id == Guid.Empty)
            {
                Model = new TnDtdnChungTuModel()
                {
                    DNgayChungTu = DateTime.Now
                };
                GetSoChungTu();
            }
            if (DataPlan != null && DataPlan.Count > 0)
            {
                SelectedPlan = DataPlan.FirstOrDefault();
            }
        }

        private void GetSoChungTu()
        {
            if (Model.Id != Guid.Empty)
            {
                return;
            }
            int soChungTuIndex = _tnDtdnChungTuService.FindNextSoChungTuIndex(CreatePredicate());
            if (Model != null)
            {
                Model.SSoChungTu = "LT-" + soChungTuIndex.ToString("D3");
                Model.ISoChungTuIndex = soChungTuIndex;
                OnPropertyChanged(nameof(Model.SSoChungTu));
            }
        }

        private void LoadHeader()
        {
            var year = _sessionService.Current.YearOfWork;
            _thucHienThu = $"Thực hiện thu năm {year - 2}";
            _duToanNam = $"Dự toán năm {year - 1}";
            _uocThucHien = $"Ước thực hiện năm {year - 1}";
            _duToanThu = $"Dự toán thu năm kế hoạch {year}";
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadHeader();
            LoadData();
        }
    }
}
