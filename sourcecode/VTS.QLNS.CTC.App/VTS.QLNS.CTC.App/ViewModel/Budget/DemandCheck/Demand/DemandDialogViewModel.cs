using AutoMapper;
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
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Demand;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand
{
    public class DemandDialogViewModel : DialogViewModelBase<NsSktChungTuModel>
    {
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private ICollectionView _nsDonViModelsView;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(DemandDialog);
        public List<string> ListIdDonViHasCt { get; set; }
        public List<NsSktChungTuModel> ListIdsSktChungTuSummary { get; set; }
        public bool isSummary { get; set; }

        private string _searchNsDonVi;
        public string SearchNsDonVi
        {
            get => _searchNsDonVi;
            set
            {
                if (SetProperty(ref _searchNsDonVi, value))
                {
                    _nsDonViModelsView.Refresh();
                }
            }
        }

        public string SelectedCountNsDonVi
        {
            get
            {
                var totalCount = NsDonViModelItems != null ? NsDonViModelItems.Count() : 0;
                var totalSelected = NsDonViModelItems != null ? NsDonViModelItems.Count(item => item.Selected) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }

        private NsSktChungTuModel _nsSktChungTuModel;
        public NsSktChungTuModel NsSktChungTuModel
        {
            get => _nsSktChungTuModel;
            set
            {
                SetProperty(ref _nsSktChungTuModel, value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEdit));
            }
        }

        private ObservableCollection<DonViModel> _nsDonViModelItems;
        public ObservableCollection<DonViModel> NsDonViModelItems
        {
            get => _nsDonViModelItems;
            set
            {
                SetProperty(ref _nsDonViModelItems, value);
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ComboboxItem> _voucherTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> VoucherTypes
        {
            get => _voucherTypes;
            set => SetProperty(ref _voucherTypes, value);
        }

        private ComboboxItem _voucherTypeSelected;

        public ComboboxItem VoucherTypeSelected
        {
            get => _voucherTypeSelected;
            set
            {
                SetProperty(ref _voucherTypeSelected, value);
                LoadNsDonVis();
                OnPropertyChanged(nameof(SelectedCountNsDonVi));
            }
        }

        private ObservableCollection<ComboboxItem> _budgetSourceTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BudgetSourceTypes
        {
            get => _budgetSourceTypes;
            set => SetProperty(ref _budgetSourceTypes, value);
        }

        private ComboboxItem _budgetSourceTypeSelected;

        public ComboboxItem BudgetSourceTypeSelected
        {
            get => _budgetSourceTypeSelected;
            set
            {
                SetProperty(ref _budgetSourceTypeSelected, value);
                LoadNsDonVis();
                OnPropertyChanged(nameof(SelectedCountNsDonVi));
            }
        }

        public bool IsEdit => NsSktChungTuModel.Id == Guid.Empty && !isSummary;
        public Visibility ShowColNSBD => VoucherTypeSelected != null && VoucherType.NSBD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowColNSSD => VoucherTypeSelected != null && VoucherType.NSSD_Key == VoucherTypeSelected.ValueItem ? Visibility.Visible : Visibility.Collapsed;

        public DemandDialogViewModel(INsDonViService nsDonViService,
            ISktChungTuService sktChungTuService,
            ISktChungTuChiTietService sktChungTuChiTietService,
            IMapper mapper,
            ISessionService sessionService,
            ISysAuditLogService log)
        {
            _sessionService = sessionService;
            _sktChungTuService = sktChungTuService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _nsDonViService = nsDonViService;
            _log = log;
            _mapper = mapper;
        }

        public override void OnSave()
        {
            base.OnSave();

            DateTime dtNow = DateTime.Now;
            if (NsSktChungTuModel != null && NsSktChungTuModel.DNgayChungTu == null)
            {
                MessageBoxHelper.Warning(Resources.MsgNgayChungTuNotEmpty);
                return;
            }
            if (isSummary)
            {
                var currentDV = GetNsDonViOfCurrentUser();
                var namLamViec = _sessionInfo.YearOfWork;
                var loaiChungTu = ListIdsSktChungTuSummary.Count > 0 ? ListIdsSktChungTuSummary[0].ILoaiChungTu : -1;
                var loaiNguonNganSach = ListIdsSktChungTuSummary.Count > 0 ? ListIdsSktChungTuSummary[0].ILoaiNguonNganSach : 0;
                if (NsSktChungTuModel.Id == Guid.Empty)
                {
                    var listSoChungTuTongHopString = string.Join(",", ListIdsSktChungTuSummary.Select(x => x.SSoChungTu).ToList());

                    var predicateSummary = PredicateBuilder.True<NsSktChungTu>();
                    predicateSummary = predicateSummary.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                    predicateSummary = predicateSummary.And(x => x.ILoaiNguonNganSach == loaiNguonNganSach && x.INamLamViec == namLamViec && x.ILoaiChungTu == loaiChungTu && x.ILoai == DemandCheckType.DEMAND && x.IIdMaDonVi == currentDV.IIDMaDonVi);
                    var listSktChungTuSummary = _sktChungTuService.FindByCondition(predicateSummary);
                    if (listSktChungTuSummary.Any())
                    {
                        var firstSktChungTuSummary = listSktChungTuSummary.FirstOrDefault();
                        if (!firstSktChungTuSummary.BKhoa)
                        {
                            MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                            if (messageBoxResult == MessageBoxResult.No)
                            {
                                return;
                            }
                            var idSktChungTuSummary = firstSktChungTuSummary.Id;
                            var sktChungTuSummary = _sktChungTuService.FindById(idSktChungTuSummary);
                            _sktChungTuService.Delete(sktChungTuSummary);
                            var predicateSummaryDetail = PredicateBuilder.True<NsSktChungTuChiTiet>();
                            predicateSummaryDetail = predicateSummaryDetail.And(x => x.IIdCtsoKiemTra == idSktChungTuSummary);
                            var sktChungTuChiTiets = _sktChungTuChiTietService.FindByCondition(predicateSummaryDetail);
                            _sktChungTuChiTietService.RemoveRange(sktChungTuChiTiets);
                        }
                        else
                        {
                            MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                            return;
                        }

                    }

                    NsSktChungTu nsSktChungTuTongHop = new NsSktChungTu();
                    NsSktChungTuModel.IIdMaDonVi = currentDV.IIDMaDonVi;
                    NsSktChungTuModel.STenDonVi = currentDV.TenDonVi;
                    NsSktChungTuModel.ILoaiChungTu = loaiChungTu;
                    NsSktChungTuModel.ILoaiNguonNganSach = loaiNguonNganSach;
                    _mapper.Map(NsSktChungTuModel, nsSktChungTuTongHop);
                    nsSktChungTuTongHop.ILoai = DemandCheckType.DEMAND;
                    nsSktChungTuTongHop.DNgayTao = DateTime.Now;
                    nsSktChungTuTongHop.SDssoChungTuTongHop = listSoChungTuTongHopString;
                    _sktChungTuService.Add(nsSktChungTuTongHop);
                    CreateDemandVoucherDetail(_mapper.Map<NsSktChungTuModel>(nsSktChungTuTongHop));
                    var listCtChiTiet =
                        _sktChungTuChiTietService.FindByCondition(
                            item => item.IIdCtsoKiemTra.Equals(nsSktChungTuTongHop.Id)).ToList();
                    if (listCtChiTiet.Count > 0)
                    {
                        nsSktChungTuTongHop.FTongTuChi = listCtChiTiet.Sum(item => item.FTuChi);
                        nsSktChungTuTongHop.FTongPhanCap = listCtChiTiet.Sum(item => item.FPhanCap);
                        nsSktChungTuTongHop.FTongMuaHangCapHienVat = listCtChiTiet.Sum(item => item.FMuaHangCapHienVat);
                        _sktChungTuService.Update(nsSktChungTuTongHop);
                    }
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    DialogHost.Close("RootDialog");
                    SavedAction?.Invoke(_mapper.Map<NsSktChungTuModel>(nsSktChungTuTongHop));
                }
                else
                {
                    NsSktChungTu nsSktChungTu;
                    nsSktChungTu = _sktChungTuService.FindById(NsSktChungTuModel.Id);
                    NsSktChungTuModel.DNgaySua = DateTime.Now;
                    NsSktChungTuModel.SNguoiSua = _sessionInfo.Principal;
                    NsSktChungTuModel.SNguoiTao = _sessionInfo.Principal;
                    NsSktChungTuModel.INamLamViec = _sessionInfo.YearOfWork;
                    NsSktChungTuModel.INamNganSach = _sessionInfo.YearOfBudget;
                    NsSktChungTuModel.IIdMaNguonNganSach = _sessionInfo.Budget;
                    _mapper.Map(NsSktChungTuModel, nsSktChungTu);
                    nsSktChungTu.ILoai = DemandCheckType.DEMAND;
                    nsSktChungTu.DNgayTao = DateTime.Now;
                    _sktChungTuService.Update(nsSktChungTu);
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    SavedAction?.Invoke(_mapper.Map<NsSktChungTuModel>(nsSktChungTu));
                }
            }
            else
            {
                var donViSelected = NsDonViModelItems.FirstOrDefault(n => n.Selected);
                if (donViSelected == null)
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                NsSktChungTuModel.IIdMaDonVi = donViSelected.IIDMaDonVi;
                NsSktChungTuModel.BDaTongHop = (donViSelected.Loai == LoaiDonVi.ROOT);
                NsSktChungTuModel.STenDonVi = donViSelected.TenDonVi;
                NsSktChungTuModel.ILoaiChungTu = Int32.Parse(VoucherTypeSelected.ValueItem);
                NsSktChungTuModel.ILoaiNguonNganSach = Int32.Parse(BudgetSourceTypeSelected.ValueItem);
                NsSktChungTuModel.SMoTa = NsSktChungTuModel.SMoTa == null ? "" : NsSktChungTuModel.SMoTa.Trim();
                NsSktChungTu nsSktChungTu;
                if (NsSktChungTuModel.Id == Guid.Empty)
                {
                    nsSktChungTu = new NsSktChungTu();
                    _mapper.Map(NsSktChungTuModel, nsSktChungTu);
                    nsSktChungTu.ILoai = DemandCheckType.DEMAND;
                    nsSktChungTu.DNgayTao = DateTime.Now;
                    _sktChungTuService.Add(nsSktChungTu);
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                }
                else
                {
                    nsSktChungTu = _sktChungTuService.FindById(NsSktChungTuModel.Id);
                    NsSktChungTuModel.DNgaySua = DateTime.Now;
                    NsSktChungTuModel.SNguoiSua = _sessionInfo.Principal;
                    NsSktChungTuModel.SNguoiTao = _sessionInfo.Principal;
                    NsSktChungTuModel.INamLamViec = _sessionInfo.YearOfWork;
                    NsSktChungTuModel.INamNganSach = _sessionInfo.YearOfBudget;
                    NsSktChungTuModel.IIdMaNguonNganSach = _sessionInfo.Budget;
                    _mapper.Map(NsSktChungTuModel, nsSktChungTu);
                    _sktChungTuService.Update(nsSktChungTu);
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<NsSktChungTuModel>(nsSktChungTu));
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

        private void LoadNsDonVis()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = StatusType.ACTIVE;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var iLoai = DemandCheckType.DEMAND;
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            var loaiNguonNganSach = Int32.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
            var listChungTu = _sktChungTuService
                .FindChungTuIndexByCondition(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, null, _sessionInfo.Principal, "sp_skt_nhap_so_nhu_cau").ToList();
            listChungTu = listChungTu.Where(x => loaiNguonNganSach == 0 || x.ILoaiNguonNganSach == loaiNguonNganSach).ToList();
            ListIdDonViHasCt = listChungTu.Select(item => item.IIdMaDonVi).ToList();
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.NOI_BO);

            bool isDvCap4 = _nsDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            if (isDvCap4)
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.ROOT);
                if (NsSktChungTuModel.Id == Guid.Empty)
                {
                    predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
                }
                else
                {
                    var idDonVisExclude = ListIdDonViHasCt.Where(item => item != NsSktChungTuModel.IIdMaDonVi).ToList();
                    predicate = predicate.And(x => idDonVisExclude.All(y => y != x.IIDMaDonVi));
                }
            }
            else
            {
                if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key)
                {
                    predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT));
                    if (NsSktChungTuModel.Id == Guid.Empty)
                    {
                        predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
                    }
                    else
                    {
                        var idDonVisExclude = ListIdDonViHasCt.Where(item => item != NsSktChungTuModel.IIdMaDonVi).ToList();
                        predicate = predicate.And(x => idDonVisExclude.All(y => y != x.IIDMaDonVi));
                    }

                }
                else if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSBD_Key)
                {
                    predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT) && true.Equals(x.BCoNSNganh));
                    if (NsSktChungTuModel.Id == Guid.Empty)
                    {
                        predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
                    }
                    else
                    {
                        var idDonVisExclude = ListIdDonViHasCt.Where(item => item != NsSktChungTuModel.IIdMaDonVi).ToList();
                        predicate = predicate.And(x => idDonVisExclude.All(y => y != x.IIDMaDonVi));
                    }
                }
            }
            var listUnit = _nsDonViService.FindByCondition(predicate).ToList();
            var listDonViByUser = _nsDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT)).Select(x => x.IIDMaDonVi);
            NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi)));
            if (!string.IsNullOrEmpty(NsSktChungTuModel.IIdMaDonVi))
            {
                NsDonViModelItems.Where(x => x.IIDMaDonVi == NsSktChungTuModel.IIdMaDonVi).Select(x =>
                {
                    x.Selected = true;
                    return x;
                }).ToList();
            }
            _nsDonViModelsView = CollectionViewSource.GetDefaultView(NsDonViModelItems);
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                ListSortDirection.Ascending));
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.IIDMaDonVi),
                ListSortDirection.Ascending));
            _nsDonViModelsView.Filter = NsDonViFilter;
            foreach (var model in NsDonViModelItems)
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

        private bool NsDonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchNsDonVi))
            {
                return true;
            }
            var item = (DonViModel)obj;
            var condition = item.TenDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower()) ||
                            item.IIDMaDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower());
            return condition;
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);

            // Reset defaule value
            SearchNsDonVi = string.Empty;

            // Trường hợp tạo mới
            if (NsSktChungTuModel.Id == Guid.Empty)
            {
                var soChungTuIndex = _sktChungTuService.GetSoChungTuIndexByCondition(DemandCheckType.DEMAND.ToString(),
                    _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
                NsSktChungTuModel = new NsSktChungTuModel()
                {
                    DNgayChungTu = DateTime.Now,
                    DNgayTao = DateTime.Now,
                    SSoChungTu = "SNC-" + soChungTuIndex.ToString("D3"),
                    SNguoiTao = _sessionInfo.Principal,
                    INamLamViec = _sessionInfo.YearOfWork,
                    INamNganSach = _sessionInfo.YearOfBudget,
                    IIdMaNguonNganSach = _sessionInfo.Budget
                };
            }
        }

        private void LoadBudgetSourceTypes()
        {
            BudgetSourceTypes = new ObservableCollection<ComboboxItem> {
                new ComboboxItem() { DisplayItem = "Ngân sách dự toán", ValueItem = TypeLoaiNNS.DU_TOAN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách bệnh viện tự chủ", ValueItem = TypeLoaiNNS.BENH_VIEN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách doanh nghiệp", ValueItem = TypeLoaiNNS.DOANH_NGHIEP.ToString() }
            };

            //BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(0);
            if (NsSktChungTuModel.Id != Guid.Empty)
            {
                if (NsSktChungTuModel.ILoaiNguonNganSach.HasValue)
                {
                    BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(NsSktChungTuModel.ILoaiNguonNganSach.Value - 1);
                }
                else
                {
                    BudgetSourceTypeSelected = null;
                }
            }
            else
            {
                if (isSummary)
                {
                    BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(ListIdsSktChungTuSummary.First().ILoaiNguonNganSach.Value - 1);
                }
                else
                {
                    BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(0);
                }
            }

        }

        private void LoadVoucherTypes()
        {
            var voucherTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key},
            };

            VoucherTypes = new ObservableCollection<ComboboxItem>(voucherTypes);
            if (NsSktChungTuModel.Id != Guid.Empty)
            {
                if (NsSktChungTuModel.ILoaiChungTu.HasValue)
                {
                    VoucherTypeSelected = VoucherTypes.ElementAt(NsSktChungTuModel.ILoaiChungTu.Value - 1);
                }
                else
                {
                    VoucherTypeSelected = null;
                }
            }
            else
            {
                if (isSummary)
                {
                    VoucherTypeSelected = VoucherTypes.ElementAt(ListIdsSktChungTuSummary.First().ILoaiChungTu.Value - 1);
                }
                else
                {
                    VoucherTypeSelected = VoucherTypes.ElementAt(0);
                }
            }

        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadNsDonVis();
            LoadData();
            LoadVoucherTypes();
            LoadBudgetSourceTypes();
        }


        private void CreateDemandVoucherDetail(NsSktChungTuModel nsSktChungTuModel)
        {
            DemandVoucherDetailCriteria creation = new DemandVoucherDetailCriteria()
            {
                ListIdChungTuTongHop = string.Join(",", ListIdsSktChungTuSummary.Select(x => x.Id.ToString()).ToList()),
                IdChungTu = nsSktChungTuModel.Id.ToString(),
                IdDonVi = nsSktChungTuModel.IIdMaDonVi,
                TenDonVi = nsSktChungTuModel.STenDonVi,
                LoaiChungTu = nsSktChungTuModel.ILoaiChungTu.GetValueOrDefault(-1),
                NamLamViec = nsSktChungTuModel.INamLamViec,
                NamNganSach = nsSktChungTuModel.INamNganSach,
                NguonNganSach = nsSktChungTuModel.IIdMaNguonNganSach,
                LoaiNguonNganSach = nsSktChungTuModel.ILoaiNguonNganSach ?? 1
            };
            _sktChungTuChiTietService.AddAggregate(creation);
        }

    }
}