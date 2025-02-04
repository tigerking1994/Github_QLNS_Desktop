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
using VTS.QLNS.CTC.App.View.Budget.Estimate.Hospital;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Hospital
{
    public class HospitalDialogViewModel : DialogViewModelBase<DtChungTuModel>
    {
        private readonly INsDtChungTuService _chungTuService;
        private readonly INsDtChungTuChiTietService _chungTuChiTietService;
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsMucLucNganSachService _mlnsService;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        private ICollectionView _listBudgetIndex;
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";

        public Guid Id;
        public bool IsEnableBudgetType => Id == Guid.Empty;
        public override Type ContentType => typeof(HospitalDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI CHỨNG TỪ" : "CẬP NHẬT CHỨNG TỪ";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới chứng từ dự toán bệnh viện tự chủ" : "Cập nhật chứng từ dự toán bệnh viện tự chủ";

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
                    OnPropertyChanged(nameof(SelectedAgencyCount));
                }
            }
        }

        public string SelectedAgencyCount
        {
            get
            {
                int totalCount = Agencies != null ? Agencies.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                AgencyModel agency = Agencies.Where(x => x.Selected).FirstOrDefault();
                if (agency != null)
                    Model.SDsidMaDonVi = agency.Id;
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
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
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private bool _isSelectAllBudgetIndex;
        public bool IsSelectAllBudgetIndex
        {
            get => BudgetIndexes.Where(x => x.IsFilter).All(x => x.IsSelected);
            set
            {
                SetProperty(ref _isSelectAllBudgetIndex, value);
                foreach (NsMuclucNgansachModel item in BudgetIndexes.Where(x => x.IsFilter))
                {
                    item.IsSelected = _isSelectAllBudgetIndex;
                }
            }
        }
        #endregion

        private List<ComboboxItem> _cbxBudgetType;
        public List<ComboboxItem> CbxBudgetType
        {
            get => _cbxBudgetType;
            set => SetProperty(ref _cbxBudgetType, value);
        }

        private ComboboxItem _cbxBudgetTypeSelected;
        public ComboboxItem CbxBudgetTypeSelected
        {
            get => _cbxBudgetTypeSelected;
            set => SetProperty(ref _cbxBudgetTypeSelected, value);
        }

        public HospitalDialogViewModel(
            INsDtChungTuService chungTuService,
            INsDtChungTuChiTietService chungTuChiTietService,
            INsDonViService donViService,
            IMapper mapper,
            ISessionService sessionService,
            INsMucLucNganSachService mlnsService)
        {
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _donViService = donViService;
            _mapper = mapper;
            _sessionService = sessionService;
            _mlnsService = mlnsService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _searchAgencyText = string.Empty;
            _searchBudgetIndexText = string.Empty;
            LoadAgencies();
            LoadBudgetIndexes();
            LoadData();
            LoadBudgetType();
        }

        private void LoadBudgetType()
        {
            CbxBudgetType = new List<ComboboxItem>();
            CbxBudgetTypeSelected = new ComboboxItem();
            if (!IsExitsDuToanDauNam() || (Id != Guid.Empty && Model.ILoaiDuToan == (int)BudgetType.YEAR))
            {
                CbxBudgetType.Add(new ComboboxItem { DisplayItem = VoucherType.BudgetTypeName[BudgetType.YEAR], ValueItem = ((int)BudgetType.YEAR).ToString() });
            }
            CbxBudgetType.Add(new ComboboxItem { DisplayItem = VoucherType.BudgetTypeName[BudgetType.LAST_YEAR], ValueItem = ((int)BudgetType.LAST_YEAR).ToString() });
            CbxBudgetType.Add(new ComboboxItem { DisplayItem = VoucherType.BudgetTypeName[BudgetType.ADDITIONAL], ValueItem = ((int)BudgetType.ADDITIONAL).ToString() });
            CbxBudgetType.Add(new ComboboxItem { DisplayItem = VoucherType.BudgetTypeName[BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR], ValueItem = ((int)BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR).ToString() });

            if (Id == Guid.Empty)
                CbxBudgetTypeSelected = _cbxBudgetType.First();
            else CbxBudgetTypeSelected = _cbxBudgetType.Where(x => x.ValueItem == Model.ILoaiDuToan.Value.ToString()).First();
        }

        private void LoadAgencies()
        {
            List<DonVi> listDonVi = new List<DonVi>();
            listDonVi = _donViService.FindByUserCreateVoucher(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.NOI_BO).ToList();
            listDonVi = listDonVi.Where(x => x.Khoi == KhoiDonVi.BENH_VIEN_TU_CHU).ToList();
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
                    }
                };
            }
        }

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadBudgetIndexes()
        {
            int loaiChungTu = int.Parse(VoucherType.NSSD_Key);
            var listMlns = _mlnsService.FindByMLNS(_sessionInfo.YearOfWork, NSEntityStatus.ACTIVED, loaiChungTu).ToList();
            BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMlns);

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
                                item.IsSelected = model.IsSelected;
                        }
                        OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                        OnPropertyChanged(nameof(IsSelectAllBudgetIndex));
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

        private Expression<Func<NsDtChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.HospitalEstimate);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSSD_Key));
            return predicate;
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            //trường hợp tạo mới
            if (Id == Guid.Empty)
            {
                var predicate = CreatePredicate();
                int soChungTuIndex = _chungTuService.FindNextSoChungTuIndex(predicate);
                Model = new DtChungTuModel
                {
                    SSoChungTu = "BV-" + soChungTuIndex.ToString().PadLeft(3, '0'),
                    ISoChungTuIndex = soChungTuIndex,
                    DNgayChungTu = DateTime.Now,
                    SDsidMaDonVi = string.Empty
                };
            }
            else
            {
                NsDtChungTu chungTu = _chungTuService.FindById(Id);
                Model = _mapper.Map<DtChungTuModel>(chungTu);
                if (!string.IsNullOrEmpty(Model.SDsidMaDonVi))
                {
                    _agencies.ToList().ForEach(x => x.IsHitTestVisible = false);
                    AgencyModel agency = _agencies.Where(x => x.Id == Model.SDsidMaDonVi).FirstOrDefault();
                    if (agency != null)
                        agency.Selected = true;
                }
                BudgetCatalogSelectedToStringConvert.SetCheckboxSelected(BudgetIndexes, Model.SDslns);
            }
        }

        private bool IsExitsDuToanDauNam()
        {
            var predicate = CreatePredicate();
            predicate = predicate.And(x => x.ILoaiDuToan == (int)BudgetType.YEAR);

            IEnumerable<NsDtChungTu> result = _chungTuService.FindByCondition(predicate).ToList();
            if (result.Any())
                return true;
            return false;
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

            if (Model == null) Model = new DtChungTuModel();
            Model.SDslns = BudgetCatalogSelectedToStringConvert.GetValueSelected(BudgetIndexes, true);
            Model.INamLamViec = _sessionService.Current.YearOfWork;
            Model.IIdMaNguonNganSach = _sessionService.Current.Budget;
            Model.INamNganSach = _sessionService.Current.YearOfBudget;
            Model.SDsidMaDonVi = Agencies.Where(x => x.Selected).First().Id;
            Model.ILoai = SoChungTuType.HospitalEstimate;
            Model.ILoaiChungTu = int.Parse(VoucherType.NSSD_Key);
            Model.ILoaiDuToan = int.Parse(_cbxBudgetTypeSelected.ValueItem);
            Model.SSoQuyetDinh = Model.SSoQuyetDinh.Trim();
            Model.SMoTa = !string.IsNullOrEmpty(Model.SMoTa) ? Model.SMoTa.Trim() : string.Empty;

            NsDtChungTu entity;
            if (Model.Id == Guid.Empty)
            {
                // Add
                entity = new NsDtChungTu();
                _mapper.Map(Model, entity);

                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                _chungTuService.Add(entity);
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
            SavedAction?.Invoke(_mapper.Map<DtChungTuModel>(entity));
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (!Model.DNgayChungTu.HasValue)
                messages.Add(Resources.AlertNgayChungTuEmpty);

            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
                messages.Add(Resources.AlertSoQuyetDinhEmpty);

            if (!Model.DNgayQuyetDinh.HasValue)
                messages.Add(Resources.AlertNgayQuyetDinhEmpty);

            if (Agencies.All(x => !x.Selected))
                messages.Add(Resources.AlertAgencyEmpty);

            if (BudgetIndexes.All(x => !x.IsSelected))
                messages.Add(Resources.AlertLNSEmpty);

            if (_cbxBudgetTypeSelected == null)
                messages.Add(Resources.AlertLoaiDuToanEmpty);

            if (!messages.Any())
            {
                messages.AddRange(ValidateSoQuyetDinh());
            }

            return string.Join(Environment.NewLine, messages);
        }

        private List<string> ValidateSoQuyetDinh()
        {
            List<string> messages = new List<string>();
            var predicate = CreatePredicate();
            predicate = predicate.And(x => x.SSoQuyetDinh == Model.SSoQuyetDinh);
            if (!Guid.Empty.Equals(Model.Id))
                predicate = predicate.And(x => x.Id != Model.Id);
            var listChungTu = _chungTuService.FindByCondition(predicate).ToList();
            if (listChungTu.Count > 0)
                messages.Add($"Hệ thống đã tồn tại số quyết định {Model.SSoQuyetDinh} ngày {listChungTu.First().DNgayQuyetDinh.Value.ToString("dd/MM/yyyy")}");
            return messages;
        }
    }
}
