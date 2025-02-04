using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check
{
    public class CheckDialogViewModel : DialogViewModelBase<NsSktChungTuModel>
    {
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private ICollectionView _nsDonViModelsView;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(CheckDialog);
        public List<string> ListIdsSktChungTu { get; set; }

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

        public bool IsSummary { get; set; }

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

        private DonViModel _nsDonViModelSelected;

        public DonViModel NsDonViModelSelected

        {
            get => _nsDonViModelSelected;
            set
            {
                SetProperty(ref _nsDonViModelSelected, value);
                OnPropertyChanged();
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
                OnPropertyChanged(nameof(SelectedCountNsDonVi));
                _nsDonViModelsView.Refresh();
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
                _nsDonViModelsView.Refresh();
            }
        }
        public DateTime DtNow => DateTime.Now;
        public CheckDialogViewModel(INsDonViService nsDonViService,
            ISktChungTuService sktChungTuService,
            ISysAuditLogService log,
            IMapper mapper,
            ISessionService sessionService)
        {
            _sessionService = sessionService;
            _sktChungTuService = sktChungTuService;
            _nsDonViService = nsDonViService;
            _log = log;
            _mapper = mapper;
        }

        public override void OnSave()
        {
            base.OnSave();
            var donViSelected = NsDonViModelItems.FirstOrDefault(n => n.Selected);
            if (donViSelected == null)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }
            if (NsSktChungTuModel != null && NsSktChungTuModel.DNgayChungTu == null)
            {
                MessageBoxHelper.Warning(Resources.MsgNgayChungTuNotEmpty);
                return;
            }
            NsSktChungTuModel.IIdMaDonVi = donViSelected?.IIDMaDonVi;
            NsSktChungTuModel.STenDonVi = donViSelected?.TenDonVi;
            NsSktChungTuModel.ILoaiChungTu = Int32.Parse(VoucherTypeSelected.ValueItem);
            NsSktChungTuModel.ILoaiNguonNganSach = Int32.Parse(BudgetSourceTypeSelected.ValueItem);
            NsSktChungTuModel.SMoTa = NsSktChungTuModel.SMoTa == null ? "" : NsSktChungTuModel.SMoTa.Trim();
            NsSktChungTu nsSktChungTu;
            if (NsSktChungTuModel.Id == Guid.Empty)
            {
                nsSktChungTu = _mapper.Map<NsSktChungTu>(NsSktChungTuModel);
                nsSktChungTu.DNgayTao = DateTime.Now;
                if (donViSelected.Loai == "0")
                {
                    nsSktChungTu.ILoai = DemandCheckType.CHECK;
                }
                else if (donViSelected.Loai == "1")
                {
                    nsSktChungTu.ILoai = DemandCheckType.CORPORATIZED_HOSPITAL;
                }
                _sktChungTuService.Add(nsSktChungTu);
                _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
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
                _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Update, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(_mapper.Map<VTS.QLNS.CTC.App.Model.NsSktChungTuModel>(nsSktChungTu));
        }

        private List<NsSktChungTu> GetListSktChungTu(string iLoai, int loaiChungTu)
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var yearOfBudget = _sessionService.Current.YearOfBudget;
            var budgetSource = _sessionService.Current.Budget;
            var loaiNguonNganSach = BudgetSourceTypeSelected != null ? Int32.Parse(BudgetSourceTypeSelected.ValueItem) : 0;
            // var iLoai = DemandCheckType.CHECK;
            // var loaiChungTu = int.Parse(VoucherType.NSBD_Key);
            List<NsSktChungTu> listChungTu;
            listChungTu = _sktChungTuService
                .FindChungTuIndexByConditionBVTC(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, _sessionService.Current.Principal, loaiNguonNganSach, "sp_skt_nhan_so_kiem_tra").ToList();
            return listChungTu;
        }

        private void LoadNsDonVis()
        {
            var yearOfWork = _sessionInfo.YearOfWork;

            var predicate = PredicateBuilder.True<DonVi>(); ;
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.Loai == LoaiDonVi.ROOT || (x.Loai == LoaiDonVi.NOI_BO && x.Khoi.Equals(KhoiDonVi.BENH_VIEN_TU_CHU)));
            var idsDonViQuanLy = _sessionService.Current.IdsDonViQuanLy;
            // predicate = predicate.And(x => idsDonViQuanLy.Contains(x.IIDMaDonVi));
            var listUnit = _nsDonViService.FindByCondition(predicate);
            if (!listUnit.Any(x => idsDonViQuanLy.Contains(x.IIDMaDonVi) && x.Loai == LoaiDonVi.ROOT))
            {
                listUnit = listUnit.Where(x => idsDonViQuanLy.Contains(x.IIDMaDonVi));
            }

            if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem.Equals(VoucherType.NSBD_Key))
            {
                var iLoai = DemandCheckType.CHECK.ToString();
                var iLoaiChungTu = int.Parse(VoucherType.NSBD_Key);
                var listChungTuNSBD = GetListSktChungTu(iLoai, iLoaiChungTu).Select(n => n.IIdMaDonVi);
                //listUnit = listUnit.Where(n => !listChungTuNSBD.Contains(n.IIDMaDonVi) && n.Khoi != "3");
                listUnit = listUnit.Where(n => n.Loai == "0" || (n.Loai != "0" && !listChungTuNSBD.Contains(n.IIDMaDonVi) && n.Khoi != "3"));
            }
            else if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem.Equals(VoucherType.NSSD_Key))
            {
                var iLoai = DemandCheckType.CHECK + "," + DemandCheckType.CORPORATIZED_HOSPITAL;
                var iLoaiChungTu = int.Parse(VoucherType.NSSD_Key);
                var listChungTuNSSD = GetListSktChungTu(iLoai, iLoaiChungTu).Select(n => n.IIdMaDonVi);
                listUnit = listUnit.Where(n => n.Loai == "0" || (n.Loai != "0" && !listChungTuNSSD.Contains(n.IIDMaDonVi)));
            }

            NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);

            if (!string.IsNullOrEmpty(NsSktChungTuModel.IIdMaDonVi))
            {
                DonVi itemDonVi = _nsDonViService.FindByMaDonViAndNamLamViec(NsSktChungTuModel.IIdMaDonVi, _sessionService.Current.YearOfWork);
                DonViModel itemDonViModel = _mapper.Map<DonViModel>(itemDonVi);
                if (itemDonViModel.Loai != "0")
                {
                    NsDonViModelItems.Add(itemDonViModel);
                }
                NsDonViModelItems.Where(x => x.IIDMaDonVi == NsSktChungTuModel.IIdMaDonVi).Select(x =>
                {
                    x.Selected = true;
                    return x;
                }).ToList();
            }
            else
            {
                if (!IsSummary)
                {
                    NsDonViModelItems.Where(x => x.Loai == "0").Select(x =>
                    {
                        x.Selected = true;
                        return x;
                    }).ToList();
                }
            }
            //else
            //{
            //    if (!IsSummary)
            //    {
            //        NsDonViModelItems.Where(x => x.Loai == "0").Select(x =>
            //        {
            //            x.Selected = true;
            //            return x;
            //        }).ToList();
            //    }
            //    else
            //    {
            //        NsDonViModelItems.ForAll(n => n.Selected = false);
            //    }
            //}
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
            var item = (DonViModel)obj;
            var condition = true;
            if (!string.IsNullOrWhiteSpace(SearchNsDonVi))
            {
                condition = condition && (item.TenDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower()) ||
                                     item.IIDMaDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower()));
            }

            if (BudgetSourceTypeSelected != null && BudgetSourceTypeSelected.ValueItem != TypeLoaiNNS.BENH_VIEN.ToString())
            {
                condition = condition && (item.Khoi == null || item.Khoi != KhoiDonVi.BENH_VIEN_TU_CHU);
            }

            return condition;
        }

        public override void LoadData(params object[] obj)
        {
            base.LoadData(obj);

            // Reset defaule value
            SearchNsDonVi = string.Empty;

            // Trường hợp tạo mới
            if (NsSktChungTuModel.Id == Guid.Empty)
            {
                var loai = DemandCheckType.CHECK + "," + DemandCheckType.CORPORATIZED_HOSPITAL;
                var soChungTuIndex = _sktChungTuService.GetSoChungTuIndexByCondition(loai,
                    _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
                NsSktChungTuModel = new NsSktChungTuModel()
                {
                    DNgayChungTu = DateTime.Now,
                    DNgayQuyetDinh = DateTime.Now,
                    DNgayTao = DateTime.Now,
                    SSoChungTu = "SKT-" + soChungTuIndex.ToString("D3"),
                    SNguoiTao = _sessionInfo.Principal,
                    INamLamViec = _sessionInfo.YearOfWork,
                    INamNganSach = _sessionInfo.YearOfBudget,
                    IIdMaNguonNganSach = _sessionInfo.Budget
                };
            }
        }

        private void LoadVoucherTypes()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var yearOfBudget = _sessionInfo.YearOfBudget;
            var budgetSource = _sessionInfo.Budget;
            var currentIdDonVi = _sessionInfo.IdDonVi;
            var predicate = PredicateBuilder.True<NsSktChungTu>();
            predicate = predicate.And(x =>
                x.ILoai == DemandCheckType.CHECK && x.INamLamViec == yearOfWork && x.INamNganSach == yearOfBudget && x.IIdMaNguonNganSach == budgetSource &&
                x.IIdMaDonVi == currentIdDonVi);
            var listChungTu = _sktChungTuService.FindByCondition(predicate);
            var SktChungTuModelItems = _mapper.Map<ObservableCollection<NsSktChungTuModel>>(listChungTu);
            var lstVoucherTypesIsExist = SktChungTuModelItems.Select(item => item.ILoaiChungTu).ToList();
            var voucherTypes = new List<ComboboxItem>();
            if (NsSktChungTuModel.Id == Guid.Empty)
            {
                //if (!lstVoucherTypesIsExist.Contains(Int32.Parse(VoucherType.NSSD_Key)))
                //{
                //    voucherTypes.Add(new ComboboxItem { DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key });
                //}
                voucherTypes.Add(new ComboboxItem { DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key });
                if (!lstVoucherTypesIsExist.Contains(Int32.Parse(VoucherType.NSBD_Key)))
                {
                    voucherTypes.Add(new ComboboxItem { DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key });
                }
            }
            else
            {
                if (NsSktChungTuModel.ILoaiChungTu.HasValue && NsSktChungTuModel.ILoaiChungTu.Value == Int32.Parse(VoucherType.NSSD_Key))
                {
                    voucherTypes.Add(new ComboboxItem { DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key });
                }
                if (NsSktChungTuModel.ILoaiChungTu.HasValue && NsSktChungTuModel.ILoaiChungTu.Value == Int32.Parse(VoucherType.NSBD_Key))
                {
                    voucherTypes.Add(new ComboboxItem { DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key });
                }
            }

            VoucherTypes = new ObservableCollection<ComboboxItem>(voucherTypes);
            if (VoucherTypes != null && VoucherTypes.Count > 0)
            {
                VoucherTypeSelected = VoucherTypes.ElementAt(0);
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
                BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(0);
            }

        }
    }
}