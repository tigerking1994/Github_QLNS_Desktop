using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Estimate.AdjustedEstimate;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate
{
    public class AdjustedEstimateDialogViewModel : DialogViewModelBase<DcChungTuModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly INsDcChungTuService _chungTuService;
        private readonly INsDcChungTuChiTietService _chungTuChiTietService;
        private readonly INsNguoiDungLnsService _nguoiDungLNSService;
        private IUserService _userService;
        private ICauHinhMLNSService _cauHinhMLNSService;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private SessionInfo _sessionInfo;
        private bool IsInitEdit;

        public override string Name => Id == Guid.Empty ? "Điều chỉnh dự toán - Thêm chứng từ" : "Điều chỉnh dự toán - Sửa chứng từ";
        public override Type ContentType => typeof(AdjustedEstimateDialog);
        public override string Title => Id == Guid.Empty ? "Thêm chứng từ" : "Sửa chứng từ";
        public override string Description => Id == Guid.Empty ? "Thêm mới chứng từ điều chỉnh" : "Cập nhật thông tin chứng từ điều chỉnh";

        #region list đơn vị  
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
                    _listAgency.Refresh();
                }
            }
        }

        public string SelectedAgencyCount
        {
            get
            {
                int totalCount = Agencies != null ? Agencies.Count : 0;
                int totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                AgencyModel agency = Agencies.Where(x => x.Selected).FirstOrDefault();
                if (agency != null)
                {
                    Model.IIdMaDonVi = agency.Id;
                    Model.STenDonVi = agency.AgencyName;
                }
                return string.Format("Chọn đơn vị ({0}/{1})", totalSelected, totalCount);
            }
        }
        #endregion

        #region list LNS
        private ObservableCollection<NsMuclucNgansachModel> _budgetIndexes;
        public ObservableCollection<NsMuclucNgansachModel> BudgetIndexes
        {
            get => _budgetIndexes;
            set => SetProperty(ref _budgetIndexes, value);
        }

        private string _searchBudgetIndexText;
        public string SearchBudgetIndexText
        {
            set
            {
                if (SetProperty(ref _searchBudgetIndexText, value))
                {
                    _listBudgetIndex.Refresh();
                    OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                }
            }
        }

        public string SelectedBudgetIndexCount
        {
            get
            {
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes.Count > 0 ? BudgetIndexes.All(x => x.IsSelected) : false;
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                foreach (NsMuclucNgansachModel item in BudgetIndexes)
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
        }
        #endregion

        private ObservableCollection<ComboboxItem> _voucherTypes;
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
                if (_voucherTypeSelected != null)
                {
                    //LoadBudgetIndexes(string.Empty);
                    OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                }
            }
        }

        private ComboboxItem _estimateSettlementTypeSelected;
        public ComboboxItem EstimateSettlementTypeSelected
        {
            get => _estimateSettlementTypeSelected;
            set => SetProperty(ref _estimateSettlementTypeSelected, value);
        }

        private ObservableCollection<ComboboxItem> _estimateSettlementTypes;
        public ObservableCollection<ComboboxItem> EstimateSettlementTypes
        {
            get => _estimateSettlementTypes;
            set => SetProperty(ref _estimateSettlementTypes, value);
        }

        public Guid Id;
        public DonVi AggregateAgency;
        public List<DcChungTuModel> AggregateAdjustedEstimates;
        public string AggregateLNS;
        public int AggregateLoaiChungTu;
        public int AggregateLoaiDuKienQt;

        private bool _isAggregate;
        public bool IsAggregate
        {
            get => _isAggregate;
            set => SetProperty(ref _isAggregate, value);
        }

        public AdjustedEstimateDialogViewModel(
            INsDonViService donViService,
            IMapper mapper,
            ISessionService sessionService,
            INsMucLucNganSachService mucLucNganSachService,
            INsDcChungTuService chungTuService,
            INsDcChungTuChiTietService chungTuChiTietService,
            ICauHinhMLNSService cauHinhMLNSService,
            IUserService userService,
            INsNguoiDungLnsService nguoiDungLNSService)
        {
            _donViService = donViService;
            _mapper = mapper;
            _sessionService = sessionService;
            _mucLucNganSachService = mucLucNganSachService;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _cauHinhMLNSService = cauHinhMLNSService;
            _userService = userService;
            _nguoiDungLNSService = nguoiDungLNSService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _searchBudgetIndexText = string.Empty;
            _budgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
            IsInitEdit = !Guid.Empty.Equals(Id);
            LoadBudgetIndexes(string.Empty);
            LoadAgencies();
            LoadData();
            LoadVoucherType();
            LoadEstimateSettlementType();
            IsInitEdit = false;
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            if (Id == Guid.Empty)
            {
                Model = new DcChungTuModel()
                {
                    DNgayChungTu = DateTime.Now
                };
                LoadChungTuIndex();
                //tổng hợp chứng từ
                if (IsAggregate)
                {
                    Model.IIdMaDonVi = AggregateAgency.IIDMaDonVi;
                    Model.STenDonVi = AggregateAgency.IIDMaDonVi + " - " + AggregateAgency.TenDonVi;
                    Model.SDslns = AggregateLNS;
                    Model.ILoaiChungTu = AggregateLoaiChungTu;
                    Model.ILoaiDuKien = AggregateLoaiDuKienQt;
                    //LoadBudgetIndexes(string.Empty);
                }
            }
            else
            {
                NsDcChungTu chungTu = _chungTuService.FindById(Id);
                Model = _mapper.Map<DcChungTuModel>(chungTu);
                //LoadBudgetIndexes(Model.IIdMaDonVi);
            }

            if (!string.IsNullOrEmpty(Model.SDslns))
            {
                BudgetCatalogSelectedToStringConvert.SetCheckboxSelected(BudgetIndexes, Model.SDslns);
                if (IsAggregate)
                    BudgetIndexes.ToList().ForEach(x => x.IsHitTestVisible = false);
            }
            if (!string.IsNullOrEmpty(Model.IIdMaDonVi))
            {
                _agencies.ToList().ForEach(x => x.IsHitTestVisible = false);
                AgencyModel agency = _agencies.Where(x => x.Id == Model.IIdMaDonVi).FirstOrDefault();
                if (agency != null)
                    agency.Selected = true;
            }
            Model.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(DcChungTuModel.DNgayChungTu))
                {
                    //LoadBudgetIndexes(string.Empty);
                }
            };
        }

        private void LoadChungTuIndex()
        {
            var predicate = CreatePredicate();
            int soChungTuIndex = _chungTuService.FindNextSoChungTuIndex(predicate);
            Model.SSoChungTu = "DC-" + soChungTuIndex.ToString("D3");
            Model.ISoChungTuIndex = soChungTuIndex;
            OnPropertyChanged(nameof(Division));
        }

        private Expression<Func<NsDcChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<NsDcChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            return predicate;
        }

        private void LoadVoucherType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
            };

            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(item => item.BCoNSNganh);
            predicate = predicate.And(item => item.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(item => item.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(item => !item.Loai.Equals(LoaiDonVi.TOAN_QUAN));
            var listDonVi = _donViService.FindByCondition(predicate);
            if (listDonVi != null && listDonVi.Count() > 0)
            {
                cbxVoucher.Add(new ComboboxItem { DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key });
            }

            VoucherTypes = new ObservableCollection<ComboboxItem>(cbxVoucher);
            if (Model != null && Model.Id != Guid.Empty)
            {
                _voucherTypeSelected = VoucherTypes.Where(item => item.ValueItem.Equals(Model.ILoaiChungTu.ToString()))
                    .Select(item => item).DefaultIfEmpty(VoucherTypes.ElementAt(0)).FirstOrDefault();
            }
            else
            {
                if (IsAggregate)
                    _voucherTypeSelected = VoucherTypes.Where(x => x.ValueItem == AggregateLoaiChungTu.ToString()).First();
                else _voucherTypeSelected = VoucherTypes.ElementAt(0);
            }
        }

        private void LoadEstimateSettlementType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.EstimateSettlementTypeName[EstimateSettlementType.SIX_MONTH], ValueItem = ((int)EstimateSettlementType.SIX_MONTH).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.EstimateSettlementTypeName[EstimateSettlementType.NINE_MONTH], ValueItem = ((int)EstimateSettlementType.NINE_MONTH).ToString()}
            };

            EstimateSettlementTypes = new ObservableCollection<ComboboxItem>(cbxVoucher);
            if (Model != null && Model.Id != Guid.Empty)
            {
                EstimateSettlementTypeSelected = EstimateSettlementTypes.Single(item => item.ValueItem.Equals(Model.ILoaiDuKien.ToString()));
            }
            else
            {
                if (IsAggregate)
                    EstimateSettlementTypeSelected = EstimateSettlementTypes.Where(x => x.ValueItem == AggregateLoaiDuKienQt.ToString()).First();
                else
                    EstimateSettlementTypeSelected = EstimateSettlementTypes.ElementAt(0);
            }
        }

        private void LoadAgencies()
        {
            List<DonVi> listDonVi = new List<DonVi>();
            if (IsAggregate)
                listDonVi.Add(AggregateAgency);
            else
            {
                //var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
                //predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == StatusType.ACTIVE && x.Loai == LoaiDonVi.NOI_BO);
                //bool isDvCap4 = _donViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
                //if (isDvCap4)
                //{
                //    listDonVi = _donViService.FindByUserCreateVoucher(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT).ToList();
                //}
                //else
                //{
                //    listDonVi = _donViService.FindByUserCreateVoucher(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.NOI_BO).ToList();
                //}
                listDonVi = _donViService.GetDanhSachDonViByNguoiDung(_sessionInfo.Principal, _sessionInfo.YearOfWork);
            }
            Agencies = new ObservableCollection<AgencyModel>();
            Agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
            _listAgency = CollectionViewSource.GetDefaultView(Agencies);
            _listAgency.Filter = ListAgencyFilter;
            foreach (var model in Agencies)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(AgencyModel.Selected))
                    {
                        OnPropertyChanged(nameof(SelectedAgencyCount));
                        if (model.Selected && !IsAggregate)
                        {
                            if (!IsInitEdit)
                                LoadBudgetIndexes(model.Id);
                        }
                    }
                };
            }
        }

        private bool ListAgencyFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchAgencyText))
            {
                return true;
            }
            return obj is AgencyModel item && item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
        }

        private List<NsNguoiDungLns> GetListLNSByUser()
        {
            var predicate = PredicateBuilder.True<NsNguoiDungLns>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.SMaNguoiDung == _sessionInfo.Principal);
            List<NsNguoiDungLns> listNguoiDungDonVi = _nguoiDungLNSService.FindAll(predicate).ToList();
            return listNguoiDungDonVi;
        }

        public void LoadBudgetIndexes(string agencyId)
        {
            int yearOfWork = _sessionInfo.YearOfWork;

            var predicate = _mucLucNganSachService.createPredicateAllNull();
            predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(n => !n.XauNoiMa.StartsWith("8"));
            IEnumerable<NsMucLucNganSach> listLNSPrev = _mucLucNganSachService.FindByCondition(predicate).OrderBy(n => n.XauNoiMa);
            if (listLNSPrev != null && listLNSPrev.Count() == 0)
            {
                BudgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
                return;
            }
            List<string> xauNoiMaParent = StringUtils.GetListXauNoiMaParent(listLNSPrev.Select(n => n.XauNoiMa).ToList());

            var predicateMLNS = _mucLucNganSachService.createPredicateAllNull();
            predicateMLNS = predicateMLNS.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
            predicateMLNS = predicateMLNS.And(n => !n.XauNoiMa.StartsWith("8"));
            predicateMLNS = predicateMLNS.And(n => xauNoiMaParent.Contains(n.XauNoiMa));
            IEnumerable<NsMucLucNganSach> listLNS = _mucLucNganSachService.FindByCondition(predicateMLNS).OrderBy(n => n.XauNoiMa);

            List<NsNguoiDungLns> listLNSNguoiDung = GetListLNSByUser();
            List<string> listParentLNS = StringUtils.GetListXauNoiMaParent(listLNSNguoiDung.Select(n => n.SLns).ToList());
            listLNS = listLNS.Where(n => listParentLNS.Contains(n.Lns));
            BudgetIndexes = new ObservableCollection<NsMuclucNgansachModel>();
            BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listLNS);
            // Filter
            _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
            _listBudgetIndex.Filter = ListBudgetIndexFilter;
            foreach (var model in BudgetIndexes)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                    {
                        foreach (NsMuclucNgansachModel item in BudgetIndexes)
                        {
                            if (item.MlnsIdParent == model.MlnsId)
                            {
                                item.IsSelected = model.IsSelected;
                                OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                                OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
                            }
                        }
                    }
                };
            }
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            bool result = true;
            var item = (NsMuclucNgansachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchBudgetIndexText))
                result = item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public override void OnSave()
        {
            base.OnSave();
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Warning(message);
                return;
            }

            if (Model == null) Model = new DcChungTuModel();
            Model.SDslns = BudgetCatalogSelectedToStringConvert.GetValueSelected(BudgetIndexes, true);
            Model.INamLamViec = _sessionService.Current.YearOfWork;
            Model.IIdMaNguonNganSach = _sessionService.Current.Budget;
            Model.INamNganSach = _sessionService.Current.YearOfBudget;
            Model.ILoaiChungTu = int.Parse(_voucherTypeSelected.ValueItem);
            Model.ILoaiDuKien = int.Parse(_estimateSettlementTypeSelected.ValueItem);
            Model.SMoTa = !string.IsNullOrEmpty(Model.SMoTa) ? Model.SMoTa.Trim() : null;

            NsDcChungTu entity;
            if (Model.Id == Guid.Empty)
            {
                // Add
                entity = new NsDcChungTu();
                //ở trạng thái tổng hợp
                if (IsAggregate)
                {
                    Model.FDieuChinh = AggregateAdjustedEstimates.Sum(x => x.FDieuChinh);
                    Model.STongHop = string.Join(",", AggregateAdjustedEstimates.Select(x => x.SSoChungTu).OrderBy(x => x).ToList());
                }
                _mapper.Map(Model, entity);

                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                _chungTuService.Add(entity);

                //tạo chứng từ chi tiết khi tổng hợp
                if (IsAggregate)
                    CreateAdjustedEstimateDetail(_mapper.Map<DcChungTuModel>(entity));
            }
            else
            {
                // Update
                entity = _chungTuService.FindById(Model.Id);
                _mapper.Map(Model, entity);

                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionService.Current.Principal;
                _chungTuService.Update(entity);
            }

            DialogHost.CloseDialogCommand.Execute(null, null);

            // Show detail page when saved
            SavedAction?.Invoke(_mapper.Map<DcChungTuModel>(entity));
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (!Model.DNgayChungTu.HasValue)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            if (BudgetIndexes.All(x => !x.IsSelected))
            {
                messages.Add(Resources.AlertLNSEmpty);
            }

            if (string.IsNullOrEmpty(Model.IIdMaDonVi))
            {
                messages.Add(Resources.AlertAgencyEmpty);
            }

            List<string> listLNSExist = CheckExistLNS();
            if (listLNSExist.Count > 0)
            {
                messages.Add(string.Format(Resources.AlertExistAdjustedEstimate, Model.STenDonVi, _estimateSettlementTypeSelected.DisplayItem,
                            _voucherTypeSelected.DisplayItem, string.Join(",", listLNSExist)));
            }
            return string.Join(Environment.NewLine, messages);
        }

        /// <summary>
        /// Tạo chứng từ chi tiết
        /// </summary>
        /// <param name="settlementVoucher"></param>
        private void CreateAdjustedEstimateDetail(DcChungTuModel chungTu)
        {
            EstimationVoucherDetailCriteria creation = new EstimationVoucherDetailCriteria()
            {
                VoucherIds = string.Join(",", AggregateAdjustedEstimates.Select(x => x.Id.ToString()).ToList()),
                VoucherId = chungTu.Id,
                YearOfBudget = chungTu.INamNganSach,
                BudgetSource = chungTu.IIdMaNguonNganSach,
                YearOfWork = chungTu.INamLamViec,
                IdDonVi = chungTu.IIdMaDonVi,
                UserName = _sessionInfo.Principal
            };
            _chungTuChiTietService.AddAggregateVoucherDetail(creation);
        }

        /// <summary>
        /// kiểm tra trùng loại ngân sách, đơn vị, loại dự kiến quyết toán và loại chứng từ
        /// </summary>
        private List<string> CheckExistLNS()
        {
            var predicate = PredicateBuilder.True<NsDcChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && x.INamNganSach == _sessionInfo.YearOfBudget && x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.IIdMaDonVi == Model.IIdMaDonVi);
            predicate = predicate.And(x => x.ILoaiChungTu == Convert.ToInt32(_voucherTypeSelected.ValueItem));
            predicate = predicate.And(x => x.ILoaiDuKien == Convert.ToInt32(_estimateSettlementTypeSelected.ValueItem));
            if (Id != Guid.Empty)
                predicate = predicate.And(x => x.Id != Id);
            List<NsDcChungTu> chungTus = _chungTuService.FindByCondition(predicate).ToList();
            List<string> listLNSExist = new List<string>();
            chungTus.ForEach(x =>
            {
                listLNSExist.AddRange(x.SDslns.Split(','));
            });
            List<string> listLNSSelected = BudgetIndexes.Where(x => x.IsSelected).Select(x => x.Lns).ToList();
            return listLNSSelected.Where(x => listLNSExist.Contains(x)).ToList();
        }
    }
}
