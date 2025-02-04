using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Plan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.RevenueExpenditureType;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Plan
{
    public class PlanBudgetBeginYearDialogViewModel : DialogViewModelBase<TnDtdnChungTuModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITnDtChungTuService _tnDtChungTuService;
        private readonly ITnDtdnChungTuService _tnDtdnChungTuService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITnDtChungTuChiTietService _tnDtChungTuChiTietService;
        private readonly ITnDtdnChungTuChiTietService _tnDtdnChungTuChiTietService;
        private readonly ILog _logger;
        private ICollectionView _dataLNSView;
        private ICollectionView _dataUnitView;

        public override Type ContentType => typeof(PlanBudgetBeginYearDialog);
        public override string Name => Model.Id == Guid.Empty ? "THÊM CHỨNG TỪ" : "CẬP NHẬT CHỨNG TỪ";
        public override string Description => Model.Id == Guid.Empty ? "Tạo mới chứng từ dự toán" : "Cập nhật chứng từ dự toán";
        public List<string> ListIdDonViHasCt { get; set; }

        private ObservableCollection<NsMuclucNgansachModel> _dataLNS;
        public ObservableCollection<NsMuclucNgansachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalItems = DataLNS != null ? DataLNS.Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalItems);
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => (DataLNS == null || !DataLNS.Any()) ? false : DataLNS.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    DataLNS.Select(c => { c.IsSelected = _selectAllLNS; return c; }).ToList();
                }
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                SetProperty(ref _searchLNS, value);
                _dataLNSView.Refresh();
            }
        }

        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private string _searchAgencyText;
        public string SearchAgencyText
        {
            get => _searchAgencyText;
            set
            {
                if (SetProperty(ref _searchAgencyText, value))
                {
                    _dataUnitView.Refresh();
                }
            }
        }

        public string SelectedAgencyCount
        {
            get
            {
                int totalCount = Agencies != null ? Agencies.Count : 0;
                int totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                return string.Format("CHỌN ĐƠN VỊ", totalSelected, totalCount);
            }
        }

        private bool _isAggregate;
        public bool IsAggregate
        {
            get => _isAggregate;
            set => SetProperty(ref _isAggregate, value);
        }

        public bool IsEnableAggregate => !IsAggregate;
        public bool IsEnableAgency => !IsAggregate && Model.Id.IsNullOrEmpty();

        public Visibility VisibilityDataLns
        {
            get => _sessionService.Current.Authorities.Contains(Role.TRO_LY_TONG_HOP) ? Visibility.Collapsed : Visibility.Visible;
        }

        public DonVi AggregateAgency;
        public List<TnDtdnChungTuModel> AggregateSettlementVouchers;
        private List<string> ListLNSHasDataUnchecked { get; set; }


        public PlanBudgetBeginYearDialogViewModel(IMapper mapper,
            ISessionService sessionService,
            ITnDtChungTuService tnDtChungTuService,
            INsMucLucNganSachService nsMucLucNganSachService,
            INsDonViService nsDonViService,
            ITnDtChungTuChiTietService tnDtChungTuChiTietService,
            ITnDtdnChungTuChiTietService tnDtdnChungTuChiTietService,
            ITnDtdnChungTuService tnDtdnChungTuService,
            ILog logger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _tnDtChungTuService = tnDtChungTuService;
            _tnDtdnChungTuService = tnDtdnChungTuService;
            _tnDtdnChungTuChiTietService = tnDtdnChungTuChiTietService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _nsDonViService = nsDonViService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;
            _logger = logger;
        }

        public override void Init()
        {
            LoadDataLNS();
            LoadDataUnit();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                base.LoadData(args);

                if (Model != null && Model.Id != Guid.Empty)
                {
                    BudgetCatalogSelectedToStringConvert.SetCheckboxSelected(_dataLNS, Model.SDSLNS);
                    TnDtChungTu chungTu = _tnDtChungTuService.FindById(Model.Id);
                    if (!string.IsNullOrEmpty(Model.IIdMaDonVi))
                    {
                        AgencyModel agency = _agencies.Where(x => x.Id == Model.IIdMaDonVi).FirstOrDefault();
                        if (agency != null)
                            agency.Selected = true;
                    }
                }
                else
                {
                    _selectAllLNS = false;                
                    var predicate = this.CreatePredicate();
                    int soChungTuIndex = _tnDtdnChungTuService.FindNextSoChungTuIndex(predicate);
                    Model = new TnDtdnChungTuModel()
                    {
                        SSoChungTu = "LT-" + soChungTuIndex.ToString("D3"),
                        ISoChungTuIndex = soChungTuIndex,
                        DNgayChungTu = DateTime.Now,
                        SMoTa = ""
                    };

                    // tổng hợp chứng từ
                    if (IsAggregate && AggregateAgency != null)
                    {
                        Model.IIdMaDonVi = AggregateAgency.IIDMaDonVi;
                        Model.STenDonVi = AggregateAgency.IIDMaDonVi + "-" + AggregateAgency.TenDonVi;

                        AgencyModel agency = _agencies.FirstOrDefault();
                        if (agency != null)
                            agency.Selected = true;
                    }

                    OnPropertyChanged(nameof(SelectAllLNS));
                    OnPropertyChanged(nameof(SelectedCountLNS));
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            } 
        }

        private Expression<Func<TnDtdnChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<TnDtdnChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            return predicate;
        }

        private void LoadDataLNS()
        {
            try
            {
                int yearOfWork = _sessionService.Current.YearOfWork;
                string idDonVi = _sessionService.Current.IdDonVi;
                List<NsMucLucNganSach> listNsMucLucNganSach = new List<NsMucLucNganSach>();

                if (_sessionService.Current.Budget.Equals(NSQP))
                {
                    listNsMucLucNganSach = _nsMucLucNganSachService.FindByMLNS(yearOfWork, MLNS_QP).ToList();
                }
                else if (_sessionService.Current.Budget.Equals(NSNN))
                {
                    listNsMucLucNganSach = _nsMucLucNganSachService.FindByMLNS(yearOfWork, MLNS_NN).ToList();
                }

                DataLNS = new ObservableCollection<NsMuclucNgansachModel>();
                DataLNS = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listNsMucLucNganSach);
                if (!Model.Id.IsNullOrEmpty())
                {
                    List<string> listLnsHasData = _tnDtdnChungTuService.GetLnsHasData(new List<Guid> { Model.Id }).ToList();
                    DataLNS.Where(x => listLnsHasData.Contains(x.Lns)).ToList().ForEach(x => x.IsHitTestVisible = false);
                }
                _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
                _dataLNSView.Filter = ListLNSFilter;

                if (_dataLNS != null && _dataLNS.Count > 0)
                {
                    foreach (var model in _dataLNS)
                    {
                        model.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                            {
                                foreach (var item in _dataLNS)
                                {
                                    if (item.MlnsIdParent == model.MlnsId)
                                    {
                                        item.IsSelected = model.IsSelected;
                                    }
                                }
                                OnPropertyChanged(nameof(SelectAllLNS));
                                OnPropertyChanged(nameof(SelectedCountLNS));
                            }
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListLNSFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.Lns.ToLower().Contains(_searchLNS!.ToLower());
        }

        private void LoadDataUnit()
        {
            //try
            //{
            //    List<DonVi> listUnit = new List<DonVi>();

            //    if (IsAggregate && AggregateAgency != null)
            //        listUnit.Add(AggregateAgency);
            //    else
            //    {
            //        int namLamViec = _sessionService.Current.YearOfWork;
            //        listUnit = _nsDonViService.FindByCondition(RevenueAndExpenditureType.UnitType, NSEntityStatus.ACTIVED, namLamViec).ToList();
            //    }

            //    var lstUnitCreated = _tnDtChungTuService.FindByType(RevenueAndExpenditureType.PlanEstimation).Select(x => x.IdDonVi).ToArray();

            //    if (Model.Id == Guid.Empty)
            //    {
            //        listUnit = listUnit.Where(x => !lstUnitCreated.Contains(x.IIDMaDonVi)).ToList();
            //    }
            //    else
            //    {
            //        lstUnitCreated = lstUnitCreated.Where(x => !x.Contains(Model.IIdMaDonVi)).ToArray();
            //        listUnit = listUnit.Where(x => !lstUnitCreated.Contains(x.IIDMaDonVi)).ToList();
            //    }

            //    Agencies = new ObservableCollection<AgencyModel>();
            //    Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listUnit);

            //    _dataUnitView = CollectionViewSource.GetDefaultView(Agencies);
            //    _dataUnitView.Filter = ListUnitFilter;

            //    if (Agencies != null && Agencies.Count > 0)
            //    {
            //        foreach (var model in Agencies)
            //        {
            //            model.PropertyChanged += (sender, args) =>
            //            {
            //                if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
            //                {
            //                    OnPropertyChanged(nameof(SelectedAgencyCount));
            //                }
            //            };
            //        }
            //    }
            //}
            //catch(Exception ex)
            //{
            //    _logger.Error(ex.Message, ex);
            //} 
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = StatusType.ACTIVE;
            int yearOfBudget = _sessionService.Current.YearOfBudget;
            int budgetSource = _sessionService.Current.Budget;
            var predicateCt = CreatePredicateCT();
            List<TnDtdnChungTu> listChungTu = _tnDtdnChungTuService.FindByCondition(predicateCt).ToList();
            listChungTu = listChungTu.Where(x => x.IIdMaNguonNganSach == budgetSource).ToList();
            ListIdDonViHasCt = listChungTu.Select(item => item.IIdMaDonVi).ToList();
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.NOI_BO);

            bool isDvCap4 = _nsDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            if (isDvCap4)
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.ROOT);
                if (Model.Id == Guid.Empty)
                {
                    predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
                }
                else
                {
                    var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IIdMaDonVi).ToList();
                    predicate = predicate.And(x => idDonVisExclude.All(y => y != x.IIDMaDonVi));
                }
            }
            else
            {

                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT));
                if (Model.Id == Guid.Empty)
                {
                    predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
                }
                else
                {
                    var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IIdMaDonVi).ToList();
                    predicate = predicate.And(x => idDonVisExclude.All(y => y != x.IIDMaDonVi));
                }


            }
            var listUnit = _nsDonViService.FindByCondition(predicate).ToList();
            var listDonViByUser = _nsDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT)).Select(x => x.IIDMaDonVi);
            Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi)));
            if (!string.IsNullOrEmpty(Model.IIdMaDonVi))
            {
                Agencies.Where(x => x.IIDMaDonVi == Model.IIdMaDonVi).Select(x =>
                {
                    x.Selected = true;
                    return x;
                }).ToList();
            }
            _dataUnitView = CollectionViewSource.GetDefaultView(Agencies);
            _dataUnitView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                ListSortDirection.Ascending));
            _dataUnitView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.IIDMaDonVi),
                ListSortDirection.Ascending));
            _dataUnitView.Filter = ListUnitFilter;
            foreach (var model in Agencies)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(AgencyModel.Selected))
                    {
                        OnPropertyChanged(nameof(SelectedAgencyCount));
                    }
                };
            }
        }

        public Expression<Func<TnDtdnChungTu, bool>> CreatePredicateCT()
        {
            var predicate = PredicateBuilder.True<TnDtdnChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);

            return predicate;
        }

        private bool ListUnitFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchAgencyText))
            {
                return true;
            }
            return obj is AgencyModel item && item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
        }

        public override void OnSave()
        {
            try
            {
                TnDtdnChungTu entity;
                if (IsAggregate && Model.Id != Guid.Empty)
                {
                    entity = _tnDtdnChungTuService.FindById(Model.Id);
                    _mapper.Map(Model, entity);

                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    _tnDtdnChungTuService.Update(entity);
                }
                else
                {
                    Model ??= new TnDtdnChungTuModel();
                    Model.SDSLNS = BudgetCatalogSelectedToStringConvert.GetValueSelected(DataLNS);
                    Model.INamLamViec = _sessionService.Current.YearOfWork;
                    Model.INamNganSach = _sessionService.Current.YearOfBudget;
                    Model.IIdMaNguonNganSach = _sessionService.Current.Budget;
                    Model.IIdMaDonVi = (from item in Agencies where item.Selected select item.Id != null ? item.Id : string.Empty).ToList().FirstOrDefault();
                    Model.SDSSoChungTuTongHop = null;
                    string message = GetMessageValidate();
                    if (!string.IsNullOrEmpty(message))
                    {
                        System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    bool bDeleteDetail = false;
                    string messageCheckBox = GetMessageValidateCheckBox();
                    if (!string.IsNullOrEmpty(messageCheckBox))
                    {
                        MessageBoxResult messageValidate = MessageBoxHelper.Confirm(messageCheckBox);
                        if (messageValidate.Equals(MessageBoxResult.Yes))
                        {
                            bDeleteDetail = true;
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (bDeleteDetail && !Model.Id.IsNullOrEmpty())
                    {
                        var itemDetails = _tnDtdnChungTuChiTietService.FindByChungTuId(Model.Id);
                        var itemRemoves = itemDetails.Where(x => ListLNSHasDataUnchecked.Contains(x.Lns)).ToList();
                        if (!itemRemoves.IsEmpty()) _tnDtdnChungTuChiTietService.RemoveRange(itemRemoves);
                        var itemRemain = itemDetails.Except(itemRemoves);
                        Model.FTongThucThuNamTruoc = itemRemain.Sum(x => x.FThucThuNamTruoc);
                        Model.FTongDuToanNamKeHoach = itemRemain.Sum(x => x.FDuToanNamKeHoach);
                        Model.FTongUocThucHienNamNay = itemRemain.Sum(x => x.FUocThucHienNamNay);
                        Model.FTongDuToanNamNay = itemRemain.Sum(x => x.FDuToanNamNay);

                    }
                    if (Model.IIdMaDonVi == _sessionService.Current.IdDonVi && !IsAggregate && Model.Id == Guid.Empty) IsAggregate = true;
                    if (Model.Id == Guid.Empty)
                    {
                        // ở trạng thái tông hợp
                        if (IsAggregate)
                        {
                            AggregateSettlementVouchers ??= new List<TnDtdnChungTuModel>();
                            Model.FTongDuToanNamKeHoach = AggregateSettlementVouchers.Sum(x => x.FTongDuToanNamKeHoach);
                            Model.FTongDuToanNamNay = AggregateSettlementVouchers.Sum(x => x.FTongDuToanNamNay);
                            Model.FTongThucThuNamTruoc = AggregateSettlementVouchers.Sum(x => x.FTongThucThuNamTruoc);
                            Model.FTongUocThucHienNamNay = AggregateSettlementVouchers.Sum(x => x.FTongUocThucHienNamNay);
                            Model.SDSSoChungTuTongHop = string.Join(",", AggregateSettlementVouchers.OrderBy(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList());
                            //Model.SDSLNS = string.Join(",", AggregateSettlementVouchers.Select(x => x.SDSLNS).ToList());
                        }

                        // Add
                        entity = new TnDtdnChungTu();
                        _mapper.Map(Model, entity);

                        entity.DNgayTao = DateTime.Now;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        _tnDtdnChungTuService.Add(entity);

                        // tọa chứng từ chi tiết khi tổng hợp
                        //if (IsAggregate)
                        //    CreateSettlementVoucherDetail(_mapper.Map<TnDtChungTuModel>(entity));
                    }
                    else
                    {
                        //nếu chứng từ ở trạng thái mới hoặc từ chối kiểm duyệt thì cập nhật thành trạng thái mới
                        //if (!IsAggregate && (Model.IKiemDuyet == (int)Censorship.NEW || Model.IKiemDuyet == (int)Censorship.DENY))
                        //    Model.IKiemDuyet = (int)Censorship.NEW;

                        // Update
                        entity = _tnDtdnChungTuService.FindById(Model.Id);
                        _mapper.Map(Model, entity);

                        entity.DNgaySua = DateTime.Now;
                        entity.SNguoiSua = _sessionService.Current.Principal;
                        _tnDtdnChungTuService.Update(entity);
                    }
                }
                

                DialogHost.CloseDialogCommand.Execute(null, null);

                // Show detail page when saved
                SavedAction?.Invoke(_mapper.Map<TnDtdnChungTuModel>(entity));
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetMessageValidateCheckBox()
        {
            List<string> messages = new List<string>();

            ListLNSHasDataUnchecked = DataLNS.Where(n => !n.IsHitTestVisible && !n.IsSelected).Select(n => n.Lns).ToList();
            string lnsText = string.Join(StringUtils.COMMA_SPLIT, ListLNSHasDataUnchecked);

            if (!string.IsNullOrEmpty(lnsText))
            {
                messages.Add(string.Format(Resources.PlanBudgetBeginYearHasDataLNS, lnsText));
            }

            return string.Join(Environment.NewLine, messages);
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();

            if (!Model.DNgayChungTu.HasValue)
            {
                messages.Add("Hãy nhập ngày chứng từ.");
            }

            if (DataLNS.All(x => !x.IsSelected))
            {
                messages.Add("Hãy chọn LNS");
            }

            if (Model.IIdMaDonVi == null)
            {
                messages.Add(Resources.AlertAgencyEmpty);
            }

            return string.Join(Environment.NewLine, messages);
        }

        /// <summary>
        /// Tạo chứng từ chi tiết
        /// </summary>
        /// <param name="settlementVoucher"></param>
        private void CreateSettlementVoucherDetail(TnDtChungTuModel settlementVoucher)
        {
            SettlementVoucherDetailCriteria creation = new SettlementVoucherDetailCriteria()
            {
                VoucherIds = string.Join(",", AggregateSettlementVouchers.Select(x => x.Id.ToString()).ToList()),
                VoucherId = settlementVoucher.Id.ToString(),
                YearOfBudget = settlementVoucher.NamNganSach != null ? settlementVoucher.NamNganSach.Value : _sessionService.Current.YearOfBudget,
                BudgetSource = settlementVoucher.NguonNganSach != null ? settlementVoucher.NguonNganSach.Value : _sessionService.Current.Budget,
                YearOfWork = settlementVoucher.NamLamViec != null ? settlementVoucher.NamLamViec.Value : _sessionService.Current.YearOfWork,
                Type = SettlementType.REGULAR_BUDGET,
                AgencyId = settlementVoucher.IdDonVi,
                AgencyName = settlementVoucher.TenDonVi,
                UserName = _sessionService.Current.Principal
            };

            _tnDtChungTuChiTietService.AddAggregateVoucherDetail(creation);
        }
    }
}
