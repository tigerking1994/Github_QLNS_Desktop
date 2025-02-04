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
using System.Windows;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThu;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT
{
    public class KeHoachThuMuaBHYTDialogViewModel : DialogViewModelBase<BhKhtmBHYTModel>
    {
        private readonly IKhtmBHYTService _khtmBHYTService;
        private readonly IKhtmBHYTChiTietService _khtmBHYTChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private ICollectionView _bhDonViModelsView;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;

        public override Type ContentType => typeof(KeHoachThuMuaBHYTDialog);
        public List<string> ListIdDonViHasCt { get; set; }
        public List<BhKhtmBHYTModel> ListIdsKhtmBHYTSummary { get; set; }
        public bool isSummary { get; set; }

        private string _searchBhDonVi;
        public string SearchBhDonVi
        {
            get => _searchBhDonVi;
            set
            {
                if (SetProperty(ref _searchBhDonVi, value))
                {
                    _bhDonViModelsView.Refresh();
                }
            }
        }

        public string SelectedCountNsDonVi
        {
            get
            {
                var totalCount = BhDonViModelItems != null ? BhDonViModelItems.Count() : 0;
                var totalSelected = BhDonViModelItems != null ? BhDonViModelItems.Count(item => item.Selected) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }

        private BhKhtmBHYTModel _bhKhtmBHYTModel;
        public BhKhtmBHYTModel BhKhtmBHYTModel
        {
            get => _bhKhtmBHYTModel;
            set
            {
                SetProperty(ref _bhKhtmBHYTModel, value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEdit));
            }
        }

        private ObservableCollection<DonViModel> _bhDonViModelItems;
        public ObservableCollection<DonViModel> BhDonViModelItems
        {
            get => _bhDonViModelItems;
            set
            {
                SetProperty(ref _bhDonViModelItems, value);
                OnPropertyChanged();
            }
        }
        public bool IsEdit => BhKhtmBHYTModel.Id == Guid.Empty && !isSummary;

        public KeHoachThuMuaBHYTDialogViewModel(INsDonViService nsDonViService,
            IKhtmBHYTService khtmBHYTService,
            IKhtmBHYTChiTietService khtmBHYTChiTietService,
            IMapper mapper,
            ISessionService sessionService,
            ISysAuditLogService log,
            ILog logger)
        {
            _sessionService = sessionService;
            _khtmBHYTService = khtmBHYTService;
            _khtmBHYTChiTietService = khtmBHYTChiTietService;
            _nsDonViService = nsDonViService;
            _log = log;
            _mapper = mapper;
            _logger = logger;
        }

        public override void OnSave()
        {
            base.OnSave();

            DateTime dtNow = DateTime.Now;
            if (BhKhtmBHYTModel != null && BhKhtmBHYTModel.DNgayChungTu == null)
            {
                MessageBoxHelper.Warning(Resources.MsgNgayChungTuNotEmpty);
                return;
            }
            if (isSummary)
            {
                var currentDV = GetNsDonViOfCurrentUser();
                var namLamViec = _sessionInfo.YearOfWork;

                if (BhKhtmBHYTModel.Id == Guid.Empty)
                {
                    var listSoChungTuTongHopString = string.Join(",", ListIdsKhtmBHYTSummary.Select(x => x.SSoChungTu).ToList());

                    var listKhtChungTuSummary = _khtmBHYTService.FindByCondition(namLamViec, currentDV.IIDMaDonVi, BhxhLoaiChungTu.BhxhChungTuTongHop);
                    if (listKhtChungTuSummary.Any())
                    {
                        var firstKhtChungTuSummary = listKhtChungTuSummary.FirstOrDefault();
                        if (!firstKhtChungTuSummary.BKhoa)
                        {
                            MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                            if (messageBoxResult == MessageBoxResult.No)
                            {
                                return;
                            }
                            var idKhtChungTuSummary = firstKhtChungTuSummary.Id;
                            var khtmChungTuSummary = _khtmBHYTService.FindById(idKhtChungTuSummary);
                            UpdateChungTuDaTongHop(khtmChungTuSummary);
                            _khtmBHYTService.Delete(khtmChungTuSummary);
                            var predicateSummaryDetail = PredicateBuilder.True<BhKhtmBHYTChiTiet>();
                            predicateSummaryDetail = predicateSummaryDetail.And(x => x.IdKhtmBHYT == idKhtChungTuSummary);
                            var khtChungTuChiTiets = _khtmBHYTChiTietService.FindByCondition(predicateSummaryDetail);
                            _khtmBHYTChiTietService.RemoveRange(khtChungTuChiTiets);
                        }
                        else
                        {
                            MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                            return;
                        }

                    }
                    var rootUnit = _nsDonViService.FindByNamLamViec(namLamViec).FirstOrDefault(x => x.Loai == LoaiDonVi.ROOT);
                    BhKhtmBHYT khtmBHYTChungTuTongHop = new BhKhtmBHYT();
                    BhKhtmBHYTModel.IIDMaDonVi = rootUnit.IIDMaDonVi;
                    BhKhtmBHYTModel.IIDDonVi = rootUnit.Id;
                    BhKhtmBHYTModel.STenDonVi = rootUnit.TenDonVi;
                    _mapper.Map(BhKhtmBHYTModel, khtmBHYTChungTuTongHop);
                    khtmBHYTChungTuTongHop.STongHop = listSoChungTuTongHopString;
                    khtmBHYTChungTuTongHop.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                    _khtmBHYTService.Add(khtmBHYTChungTuTongHop);
                    CreateDemandVoucherDetail(_mapper.Map<BhKhtmBHYTModel>(khtmBHYTChungTuTongHop));
                    var listCtChiTiet =
                        _khtmBHYTChiTietService.FindByCondition(
                            item => item.IdKhtmBHYT.Equals(khtmBHYTChungTuTongHop.Id)).ToList();
                    if (listCtChiTiet.Count > 0)
                    {
                        foreach (var item in listCtChiTiet)
                        {
                            item.INamLamViec = namLamViec;
                            item.IIDMaDonVi = rootUnit.IIDMaDonVi;
                            item.STenDonVi = rootUnit.TenDonVi;
                            _khtmBHYTChiTietService.Update(item);
                        }
                        khtmBHYTChungTuTongHop.ITongSoNguoi = listCtChiTiet.Sum(item => item.ISoNguoi);
                        khtmBHYTChungTuTongHop.ITongSoThang = listCtChiTiet.Sum(item => item.ISoThang);
                        khtmBHYTChungTuTongHop.FTongDinhMuc = listCtChiTiet.Sum(item => item.FDinhMuc);
                        khtmBHYTChungTuTongHop.FTongThanhTien = listCtChiTiet.Sum(item => item.FThanhTien);
                        _khtmBHYTService.Update(khtmBHYTChungTuTongHop);
                    }
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    DialogHost.Close("RootDialog");
                    SavedAction?.Invoke(_mapper.Map<BhKhtmBHYTModel>(khtmBHYTChungTuTongHop));
                }
                else
                {
                    BhKhtmBHYT bhKhtmChungTu;
                    bhKhtmChungTu = _khtmBHYTService.FindById(BhKhtmBHYTModel.Id);
                    BhKhtmBHYTModel.DNgaySua = DateTime.Now;
                    BhKhtmBHYTModel.SNguoiSua = _sessionInfo.Principal;
                    BhKhtmBHYTModel.SNguoiTao = _sessionInfo.Principal;
                    BhKhtmBHYTModel.INamLamViec = _sessionInfo.YearOfWork;
                    _mapper.Map(BhKhtmBHYTModel, bhKhtmChungTu);
                    bhKhtmChungTu.DNgayTao = DateTime.Now;
                    bhKhtmChungTu.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                    _khtmBHYTService.Update(bhKhtmChungTu);
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    SavedAction?.Invoke(_mapper.Map<BhKhtmBHYTModel>(bhKhtmChungTu));
                }
            }
            else
            {
                var donViSelected = BhDonViModelItems.FirstOrDefault(n => n.Selected);
                if (donViSelected == null)
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                BhKhtmBHYTModel.IIDMaDonVi = donViSelected?.IIDMaDonVi;
                BhKhtmBHYTModel.STenDonVi = donViSelected?.TenDonVi;
                BhKhtmBHYTModel.IIDDonVi = donViSelected?.Id;
                BhKhtmBHYTModel.SMoTa = BhKhtmBHYTModel.SMoTa == null ? "" : BhKhtmBHYTModel.SMoTa.Trim();
                BhKhtmBHYT bhKhtmBHYT;
                if (BhKhtmBHYTModel.Id == Guid.Empty)
                {
                    bhKhtmBHYT = new BhKhtmBHYT();
                    _mapper.Map(BhKhtmBHYTModel, bhKhtmBHYT);
                    bhKhtmBHYT.DNgayChungTu = DateTime.Now;
                    bhKhtmBHYT.ILoaiTongHop = donViSelected.Loai == LoaiDonVi.ROOT ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;


                    _khtmBHYTService.Add(bhKhtmBHYT);
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                }
                else
                {
                    bhKhtmBHYT = _khtmBHYTService.FindById(BhKhtmBHYTModel.Id);
                    BhKhtmBHYTModel.DNgaySua = DateTime.Now;
                    BhKhtmBHYTModel.SNguoiSua = _sessionInfo.Principal;
                    BhKhtmBHYTModel.SNguoiTao = _sessionInfo.Principal;
                    BhKhtmBHYTModel.INamLamViec = _sessionInfo.YearOfWork;
                    _mapper.Map(BhKhtmBHYTModel, bhKhtmBHYT);
                    _khtmBHYTService.Update(bhKhtmBHYT);
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<BhKhtmBHYTModel>(bhKhtmBHYT));
            }
        }

        private void UpdateChungTuDaTongHop(BhKhtmBHYT chungtu)
        {
            if (!string.IsNullOrEmpty(chungtu.STongHop))
            {
                var lstSoCtChild = chungtu.STongHop.Split(",");
                foreach (var soct in lstSoCtChild)
                {
                    var ctChild = _khtmBHYTService.FindChungTuDaTongHopBySCT(soct, _sessionInfo.YearOfWork).FirstOrDefault();
                    if (ctChild != null)
                    {
                        ctChild.BDaTongHop = false;
                        _khtmBHYTService.Update(ctChild);
                    }
                }
            }
        }
        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var currentIdDonVi = _sessionInfo.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private void LoadDonVis()
        {
            try
            {
                var yearOfWork = _sessionInfo.YearOfWork;
                var predicate = PredicateBuilder.True<DonVi>();
                var iTrangThai = StatusType.ACTIVE;
                int yearOfBudget = _sessionInfo.YearOfBudget;
                int budgetSource = _sessionInfo.Budget;

                var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
                predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.NOI_BO);

                //bool isDvCap4 = _nsDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
                //if (isDvCap4)
                //{
                //    predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.ROOT);
                //    if (BhKhtmBHYTModel.Id == Guid.Empty)
                //    {
                //        predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
                //    }
                //    else
                //    {
                //        var idDonVisExclude = ListIdDonViHasCt.Where(item => item != BhKhtmBHYTModel.IIDMaDonVi).ToList();
                //        predicate = predicate.And(x => idDonVisExclude.All(y => y != x.IIDMaDonVi));
                //    }
                //}
                var lstCurrentUnitBh = _khtmBHYTService.FindCurrentUnits(yearOfWork);
                var listUnit = _nsDonViService.FindByCondition(predicate).Where(x => x.NamLamViec == yearOfWork).ToList();
                var listDonViByUser = _nsDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT))
                    .Where(y => !lstCurrentUnitBh.Contains(y.IIDMaDonVi)).Select(x => x.IIDMaDonVi);

                if (BhKhtmBHYTModel.Id == Guid.Empty)
                {
                    BhDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi)));
                }
                else
                {
                    BhDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
                }

                if (!string.IsNullOrEmpty(BhKhtmBHYTModel.IIDMaDonVi))
                {
                    BhDonViModelItems.Where(x => x.IIDMaDonVi == BhKhtmBHYTModel.IIDMaDonVi).Select(x =>
                    {
                        x.Selected = true;
                        return x;
                    }).ToList();
                }
                _bhDonViModelsView = CollectionViewSource.GetDefaultView(BhDonViModelItems);
                _bhDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                    ListSortDirection.Ascending));
                _bhDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.IIDMaDonVi),
                    ListSortDirection.Ascending));
                _bhDonViModelsView.Filter = NsDonViFilter;
                foreach (var model in BhDonViModelItems)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(DonViModel.Selected))
                        {
                            OnPropertyChanged(nameof(SelectedCountNsDonVi));
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool NsDonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchBhDonVi))
            {
                return true;
            }
            var item = (DonViModel)obj;
            var condition = item.TenDonVi.ToLower().Contains(SearchBhDonVi.Trim().ToLower()) ||
                            item.IIDMaDonVi.ToLower().Contains(SearchBhDonVi.Trim().ToLower());
            return condition;
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            SearchBhDonVi = string.Empty;

            // Trường hợp tạo mới
            if (BhKhtmBHYTModel.Id == Guid.Empty)
            {
                var soChungTuIndex = _khtmBHYTService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                BhKhtmBHYTModel = new BhKhtmBHYTModel()
                {
                    DNgayChungTu = DateTime.Now,
                    DNgayTao = DateTime.Now,
                    DNgayQuyetDinh = DateTime.Now,
                    SSoChungTu = "KHTM-" + soChungTuIndex.ToString("D3"),
                    SNguoiTao = _sessionInfo.Principal,
                    INamLamViec = _sessionInfo.YearOfWork
                };
            }
        }
        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadDonVis();
            LoadData();
        }

        private void CreateDemandVoucherDetail(BhKhtmBHYTModel bhKhtmChungTuModel)
        {
            KhtmBHYTChiTietCriteria creation = new KhtmBHYTChiTietCriteria()
            {
                ListIdChungTuTongHop = string.Join(",", ListIdsKhtmBHYTSummary.Select(x => x.Id.ToString()).ToList()),
                IdChungTu = bhKhtmChungTuModel.Id.ToString(),
                NamLamViec = bhKhtmChungTuModel.INamLamViec
            };
            _khtmBHYTService.AddAggregate(creation);
        }
    }
}
