using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan
{
    public class PlanBeginYearSummaryViewModel : DialogViewModelBase<PlanBeginYearModel>
    {
        private readonly ISktSoLieuChungTuService _sktChungTuService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private ICollectionView _nsDonViModelsView;
        private SessionInfo _sessionInfo;

        public override string Name => "Tổng hợp dự toán đầu năm";
        public override Type ContentType => typeof(View.Budget.DemandCheck.Plan.PlanBeginYearSummary);
        public override PackIconKind IconKind => PackIconKind.Sigma;
        public override string Title => "TỔNG HỢP CHỨNG TỪ LẬP DỰ TOÁN NGÂN SÁCH NĂM";
        public override string Description => "TỔNG HỢP CHỨNG TỪ LẬP DỰ TOÁN NGÂN SÁCH NĂM";

        public List<string> ListIdDonViHasCt { get; set; }
        public List<NsSktChungTuModel> ListIdsSktChungTuSummary { get; set; }
        public string LoaiChungTu { get; set; }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.PlanBeginYearModel> _dataPlan;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.PlanBeginYearModel> DataPlan
        {
            get => _dataPlan;
            set => SetProperty(ref _dataPlan, value);
        }

        private PlanBeginYearModel _selectedPlan;
        public PlanBeginYearModel SelectedPlan
        {
            get => _selectedPlan;
            set
            {
                SetProperty(ref _selectedPlan, value);
            }
        }

        public PlanBeginYearSummaryViewModel(INsDonViService nsDonViService,
            ISktSoLieuChungTuService sktChungTuService,
            ISktChungTuChiTietService sktChungTuChiTietService,
            IMapper mapper,
            ISktSoLieuService sktSoLieuService,
            ISessionService sessionService,
            ISysAuditLogService log)
        {
            _sessionService = sessionService;
            _sktChungTuService = sktChungTuService;
            _sktSoLieuService = sktSoLieuService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _nsDonViService = nsDonViService;
            _log = log;
            _mapper = mapper;
        }

        private string GetListDonViTongHop()
        {
            List<string> listChungTu = DataPlan.Where(n => n.Selected && n.IsLocked).Select(n => n.Id_DonVi).ToList();
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
            List<string> listChungTu = DataPlan.Where(n => n.Selected && n.IsLocked).Select(n => n.SSoChungTu).ToList();
            if (listChungTu != null && listChungTu.Count > 0)
            {
                List<string> listLNS = new List<string>();
                foreach (var item in DataPlan.Where(n => n.Selected && n.IsLocked))
                {
                    if (!string.IsNullOrEmpty(item.DsLNS))
                        listLNS.AddRange(item.DsLNS.Split(","));
                }
                lnsResult = string.Join(",", listLNS.Distinct().ToList());
                return string.Join(",", listChungTu);
            }
            else
            {
                return string.Empty;
            }
        }

        public override void OnSave()
        {
            if (DataPlan != null && DataPlan.Where(n => n.Selected && n.IsLocked).Count() > 0)
            {
                string lns = string.Empty;
                string listChungTuTongHop = GetListChungTuTongHop(ref lns);
                if (!string.IsNullOrEmpty(listChungTuTongHop))
                {
                    DonVi root = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
                    //var predicate = PredicateBuilder.True<NsDtdauNamChungTu>();
                    //predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                    //predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                    //predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                    //predicate = predicate.And(x => x.IIdMaDonVi == donvi0.IIDMaDonVi);
                    //predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));

                    //NsDtdauNamChungTu chungTu = _sktChungTuService.FindByCondition(predicate).FirstOrDefault();
                    //if (chungTu != null)
                    //{
                    //    _sktChungTuService.Delete(chungTu.Id);
                    //}

                    NsDtdauNamChungTu entity = new NsDtdauNamChungTu();
                    int soChungTuIndex = _sktChungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork,
                                 _sessionService.Current.Budget, _sessionService.Current.YearOfBudget, int.Parse(LoaiChungTu));
                    var loaiNNS = DataPlan.Where(n => n.Selected && n.IsLocked).FirstOrDefault().ILoaiNguonNganSach ?? 0;
                    entity.ISoChungTuIndex = soChungTuIndex;
                    entity.ILoaiNguonNganSach = loaiNNS;
                    entity.SSoChungTu = Model.SSoChungTu;
                    entity.SMoTa = Model.SMoTa;
                    entity.DNgayChungTu = Model.DNgayChungTu;
                    entity.ILoaiChungTu = int.Parse(LoaiChungTu);
                    entity.IIdMaDonVi = root.IIDMaDonVi;
                    entity.INamLamViec = _sessionService.Current.YearOfWork;
                    entity.INamNganSach = _sessionService.Current.YearOfBudget;
                    entity.IIdMaNguonNganSach = _sessionService.Current.Budget;
                    entity.DNgayTao = DateTime.Now;
                    entity.SDslns = lns;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.SDSSoChungTuTongHop = listChungTuTongHop;
                    _sktChungTuService.Add(entity);
                    //chi tiet
                    _sktSoLieuService.CreateDataReportTotalSummary(entity.Id.ToString(), _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                       _sessionService.Current.Budget, 0, 0, root.IIDMaDonVi, LoaiChungTu, string.Join(",", DataPlan.Where(n => n.Selected && n.IsLocked).Select(n => n.Id.ToString()).ToList()),
                       _sessionService.Current.Principal);

                    _sktChungTuService.UpdateTotalChungTu(entity.IIdMaDonVi, LoaiDonVi.ROOT.ToString(), int.Parse(LoaiChungTu),
                    _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, loaiNNS, entity.Id.ToString());

                    DialogHost.CloseDialogCommand.Execute(null, null);
                    //DialogHost.Close("RootDialog");
                    //MessageBoxHelper.Info(Resources.MsgSumaryDone);
                    //SavedAction?.Invoke(null);
                    SavedAction?.Invoke(_mapper.Map<NsDtdauNamChungTu>(entity));
                }
            }
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);

            if (Model.Id == Guid.Empty)
            {
                Model = new PlanBeginYearModel()
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
            if (string.IsNullOrEmpty(LoaiChungTu) || Model.Id != Guid.Empty)
            {
                return;
            }
            int soChungTuIndex = _sktChungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork,
                    _sessionService.Current.Budget, _sessionService.Current.YearOfBudget, int.Parse(LoaiChungTu));
            if (Model != null)
            {
                Model.SSoChungTu = "DTDN-" + soChungTuIndex.ToString("D3");
                OnPropertyChanged(nameof(Model.SSoChungTu));
            }
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;

            LoadData();
        }
    }
}
