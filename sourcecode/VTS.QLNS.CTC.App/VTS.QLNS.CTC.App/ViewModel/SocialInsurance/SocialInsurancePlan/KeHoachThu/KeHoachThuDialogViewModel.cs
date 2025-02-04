using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThu;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu
{ 
    public class KeHoachThuDialogViewModel : DialogViewModelBase<BhKhtBHXHModel>
    {
        private readonly IKhtBHXHService _khtBHXHService;
        private readonly IKhtBHXHChiTietService _khtBHXHChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private ICollectionView _bhDonViModelsView;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;

        public override Type ContentType => typeof(KeHoachThuDialog);
        public List<string> ListIdDonViHasCt { get; set; }
        public List<BhKhtBHXHModel> ListIdsKhtBHXHSummary { get; set; }
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

        private BhKhtBHXHModel _bhKhtBHXHModel;
        public BhKhtBHXHModel BhKhtBHXHModel
        {
            get => _bhKhtBHXHModel;
            set
            {
                SetProperty(ref _bhKhtBHXHModel, value);
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
        public bool IsEdit => BhKhtBHXHModel.Id == Guid.Empty && !isSummary;

        public KeHoachThuDialogViewModel(INsDonViService nsDonViService,
            IKhtBHXHService khtBHXHService,
            IKhtBHXHChiTietService khtBHXHChiTietService,
            IMapper mapper,
            ISessionService sessionService,
            ISysAuditLogService log,
            ILog logger)
        {
            _sessionService = sessionService;
            _khtBHXHService = khtBHXHService;
            _khtBHXHChiTietService = khtBHXHChiTietService;
            _nsDonViService = nsDonViService;
            _log = log;
            _mapper = mapper;
            _logger = logger;
        }

        public override void OnSave()
        {
            base.OnSave();

            DateTime dtNow = DateTime.Now;
            if (BhKhtBHXHModel != null && BhKhtBHXHModel.DNgayChungTu == null)
            {
                MessageBoxHelper.Warning(Resources.MsgNgayChungTuNotEmpty);
                return;
            }
            if (isSummary)
            {
                var currentDV = GetNsDonViOfCurrentUser();
                var namLamViec = _sessionInfo.YearOfWork;
                if (BhKhtBHXHModel.Id == Guid.Empty)
                {
                    var listSoChungTuTongHopString = string.Join(",", ListIdsKhtBHXHSummary.Select(x => x.SSoChungTu).ToList());
                    var listKhtChungTuSummary = _khtBHXHService.FindByCondition(namLamViec, currentDV.IIDMaDonVi, BhxhLoaiChungTu.BhxhChungTuTongHop);
                    if (listKhtChungTuSummary.Any())
                    {
                        var firstKhtChungTuSummary = listKhtChungTuSummary.FirstOrDefault();
                        if (!firstKhtChungTuSummary.BIsKhoa)
                        {
                            MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                            if (messageBoxResult == MessageBoxResult.No)
                            {
                                return;
                            }
                            var idKhtChungTuSummary = firstKhtChungTuSummary.Id;
                            var khtChungTuSummary = _khtBHXHService.FindById(idKhtChungTuSummary);
                            UpdateChungTuDaTongHop(khtChungTuSummary);
                            _khtBHXHService.Delete(khtChungTuSummary);
                            var predicateSummaryDetail = PredicateBuilder.True<BhKhtBHXHChiTiet>();
                            predicateSummaryDetail = predicateSummaryDetail.And(x => x.KhtBHXHId == idKhtChungTuSummary);
                            var khtChungTuChiTiets = _khtBHXHChiTietService.FindByCondition(predicateSummaryDetail);
                            _khtBHXHChiTietService.RemoveRange(khtChungTuChiTiets);
                        }
                        else
                        {
                            MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                            return;
                        }
                    }
                    var rootUnit = _nsDonViService.FindByNamLamViec(namLamViec).FirstOrDefault(x => x.Loai == LoaiDonVi.ROOT);
                    BhKhtBHXH khtBHXHChungTuTongHop = new BhKhtBHXH();
                    BhKhtBHXHModel.IID_MaDonVi = rootUnit.IIDMaDonVi;
                    BhKhtBHXHModel.STenDonVi = rootUnit.TenDonVi;
                    _mapper.Map(BhKhtBHXHModel, khtBHXHChungTuTongHop);
                    khtBHXHChungTuTongHop.STongHop = listSoChungTuTongHopString;
                    khtBHXHChungTuTongHop.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                    khtBHXHChungTuTongHop.BDaTongHop = true;
                    _khtBHXHService.Add(khtBHXHChungTuTongHop);
                    CreateDemandVoucherDetail(_mapper.Map<BhKhtBHXHModel>(khtBHXHChungTuTongHop));
                    var listCtChiTiet =
                        _khtBHXHChiTietService.FindByCondition(
                            item => item.KhtBHXHId.Equals(khtBHXHChungTuTongHop.Id)).ToList();
                    if (listCtChiTiet.Count > 0)
                    {
                        foreach(var item in listCtChiTiet)
                        {
                            item.INamLamViec = namLamViec;
                            item.IIdMaDonVi = rootUnit.IIDMaDonVi;
                            item.STenDonVi = rootUnit.TenDonVi;
                            _khtBHXHChiTietService.Update(item);
                        }
                        khtBHXHChungTuTongHop.IQSBQNam = listCtChiTiet.Sum(item => item.IQSBQNam);
                        khtBHXHChungTuTongHop.FLuongChinh = listCtChiTiet.Sum(item => item.FLuongChinh);
                        khtBHXHChungTuTongHop.FPhuCapChucVu = listCtChiTiet.Sum(item => item.FPhuCapChucVu);
                        khtBHXHChungTuTongHop.FPCTNNghe = listCtChiTiet.Sum(item => item.FPCTNNghe);
                        khtBHXHChungTuTongHop.FPCTNVuotKhung = listCtChiTiet.Sum(item => item.FPCTNVuotKhung);
                        khtBHXHChungTuTongHop.FNghiOm = listCtChiTiet.Sum(item => item.FNghiOm);
                        khtBHXHChungTuTongHop.FHSBL = listCtChiTiet.Sum(item => item.FHSBL);
                        khtBHXHChungTuTongHop.FTongQTLN = listCtChiTiet.Sum(item => item.FTongQuyTienLuongNam);

                        khtBHXHChungTuTongHop.FThuBHXHNLDDong = listCtChiTiet.Sum(item => item.FThuBHXHNguoiLaoDong);
                        khtBHXHChungTuTongHop.FThuBHXHNSDDong = listCtChiTiet.Sum(item => item.FThuBHXHNguoiSuDungLaoDong);
                        khtBHXHChungTuTongHop.FThuBHXH = listCtChiTiet.Sum(item => item.FTongThuBHXH);
                        khtBHXHChungTuTongHop.FThuBHYTNLDDong = listCtChiTiet.Sum(item => item.FThuBHYTNguoiLaoDong);
                        khtBHXHChungTuTongHop.FThuBHYTNSDDong = listCtChiTiet.Sum(item => item.FThuBHYTNguoiSuDungLaoDong);
                        khtBHXHChungTuTongHop.FTongBHYT = listCtChiTiet.Sum(item => item.FTongThuBHYT);
                        khtBHXHChungTuTongHop.FThuBHTNNLDDong = listCtChiTiet.Sum(item => item.FThuBHTNNguoiLaoDong);
                        khtBHXHChungTuTongHop.FThuBHTNNSDDong = listCtChiTiet.Sum(item => item.FThuBHTNNguoiSuDungLaoDong);
                        khtBHXHChungTuTongHop.FThuBHTN = listCtChiTiet.Sum(item => item.FTongThuBHTN);
                        khtBHXHChungTuTongHop.FTong = listCtChiTiet.Sum(item => item.FTongCong);
                        _khtBHXHService.Update(khtBHXHChungTuTongHop);
                    }
                    
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    DialogHost.Close("RootDialog");
                    SavedAction?.Invoke(_mapper.Map<BhKhtBHXHModel>(khtBHXHChungTuTongHop));
                }
                else
                {
                    BhKhtBHXH bhKhtChungTu;
                    bhKhtChungTu = _khtBHXHService.FindById(BhKhtBHXHModel.Id);
                    BhKhtBHXHModel.DNgaySua = DateTime.Now;
                    BhKhtBHXHModel.SNguoiSua = _sessionInfo.Principal;
                    BhKhtBHXHModel.SNguoiTao = _sessionInfo.Principal;
                    BhKhtBHXHModel.INamLamViec = _sessionInfo.YearOfWork;
                    _mapper.Map(BhKhtBHXHModel, bhKhtChungTu);
                    bhKhtChungTu.DNgayTao = DateTime.Now;
                    bhKhtChungTu.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                    _khtBHXHService.Update(bhKhtChungTu);
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    SavedAction?.Invoke(_mapper.Map<BhKhtBHXHModel>(bhKhtChungTu));
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
                BhKhtBHXHModel.IID_MaDonVi = donViSelected?.IIDMaDonVi;
                BhKhtBHXHModel.STenDonVi = donViSelected?.TenDonVi;
                BhKhtBHXHModel.SMoTa = BhKhtBHXHModel.SMoTa == null ? "" : BhKhtBHXHModel.SMoTa.Trim();
                BhKhtBHXH bhKhtBHXH;
                if (BhKhtBHXHModel.Id == Guid.Empty)
                {
                    bhKhtBHXH = new BhKhtBHXH();
                    _mapper.Map(BhKhtBHXHModel, bhKhtBHXH);
                    bhKhtBHXH.DNgayChungTu = DateTime.Now;
                    bhKhtBHXH.ILoaiTongHop = donViSelected.Loai == LoaiDonVi.ROOT ? BhxhLoaiChungTu.BhxhChungTuTongHop : BhxhLoaiChungTu.BhxhChungTu;

                    _khtBHXHService.Add(bhKhtBHXH);
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                }
                else
                {
                    bhKhtBHXH = _khtBHXHService.FindById(BhKhtBHXHModel.Id);
                    BhKhtBHXHModel.DNgaySua = DateTime.Now;
                    BhKhtBHXHModel.SNguoiSua = _sessionInfo.Principal;
                    BhKhtBHXHModel.SNguoiTao = _sessionInfo.Principal;
                    BhKhtBHXHModel.INamLamViec = _sessionInfo.YearOfWork;
                    BhKhtBHXHModel.INamNganSach = _sessionInfo.YearOfBudget;
                    BhKhtBHXHModel.IIdMaNguonNganSach = _sessionInfo.Budget;
                    _mapper.Map(BhKhtBHXHModel, bhKhtBHXH);
                    _khtBHXHService.Update(bhKhtBHXH);
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<BhKhtBHXHModel>(bhKhtBHXH));
            }
        }

        private void UpdateChungTuDaTongHop(BhKhtBHXH chungtu)
        {
            if (!string.IsNullOrEmpty(chungtu.STongHop))
            {
                var lstSoCtChild = chungtu.STongHop.Split(",");
                foreach (var soct in lstSoCtChild)
                {
                    var ctChild = _khtBHXHService.FindChungTuDaTongHopBySCT(soct, _sessionInfo.YearOfWork).FirstOrDefault();
                    if (ctChild != null)
                    {
                        ctChild.BDaTongHop = false;
                        _khtBHXHService.Update(ctChild);
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
                //    if (BhKhtBHXHModel.Id == Guid.Empty)
                //    {
                //        predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
                //    }
                //    else
                //    {
                //        var idDonVisExclude = ListIdDonViHasCt.Where(item => item != BhKhtBHXHModel.IID_MaDonVi).ToList();
                //        predicate = predicate.And(x => idDonVisExclude.All(y => y != x.IIDMaDonVi));
                //    }
                //}
                var lstCurrentUnitBh = _khtBHXHService.FindCurrentUnits(yearOfWork);
                var listUnit = _nsDonViService.FindByCondition(predicate).Where(x => x.NamLamViec == yearOfWork).ToList();
                var listDonViByUser = _nsDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT))
                    .Where(y => !lstCurrentUnitBh.Contains(y.IIDMaDonVi)).Select(x => x.IIDMaDonVi);

                if (BhKhtBHXHModel.Id == Guid.Empty)
                {
                    BhDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi)));
                }
                else
                {
                    BhDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
                }

                if (!string.IsNullOrEmpty(BhKhtBHXHModel.IID_MaDonVi))
                {
                    BhDonViModelItems.Where(x => x.IIDMaDonVi == BhKhtBHXHModel.IID_MaDonVi).Select(x =>
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
            if (BhKhtBHXHModel.Id == Guid.Empty)
            {
                var soChungTuIndex = _khtBHXHService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                BhKhtBHXHModel = new BhKhtBHXHModel()
                {
                    DNgayChungTu = DateTime.Now,
                    DNgayTao = DateTime.Now,
                    DNgayQuyetDinh = DateTime.Now,
                    SSoChungTu = "KHT-" + soChungTuIndex.ToString("D3"),
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

        private void CreateDemandVoucherDetail(BhKhtBHXHModel bhKhtChungTuModel)
        {
            KhtBHXHChiTietCriteria creation = new KhtBHXHChiTietCriteria()
            {
                ListIdChungTuTongHop = string.Join(",", ListIdsKhtBHXHSummary.Select(x => x.Id.ToString()).ToList()),
                IdChungTu = bhKhtChungTuModel.Id.ToString(),
                NamLamViec = bhKhtChungTuModel.INamLamViec,
            };
            _khtBHXHService.AddAggregate(creation);
        }
    }
}