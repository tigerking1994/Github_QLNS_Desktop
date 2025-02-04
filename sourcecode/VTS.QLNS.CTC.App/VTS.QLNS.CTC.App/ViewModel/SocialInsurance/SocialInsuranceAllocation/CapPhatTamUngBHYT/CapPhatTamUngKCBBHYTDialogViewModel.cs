using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Core.Repository;
using System.Windows;
using System.Data;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation
{
    public class CapPhatTamUngKCBBHYTDialogViewModel : ViewModelBase
    {

        private readonly INsDonViService _donViService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly IBhDmCoSoYTeService _bhDmCoSoYTeService;

        private readonly ICptuBHYTService _cptuBHYTService;
        private readonly ICptuBHYTChiTietService _cptuBHYTChiTietService;

        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private ICollectionView _listCsYTeView;
        private ICollectionView _listLNSView;
        private SessionInfo _sessionInfo;
        private bool _isCapPhatToanDonVi;

        public bool IsEditProcess = false;
        public bool IsSummary { get; set; }
        public override string Name => "THÊM MỚI CẤP PHÁT";
        public override string Title => IsEditProcess ? "CẬP NHẬT" : "THÊM MỚI";
        public override string Description => IsEditProcess ? "Cập nhật cấp tạm ứng KP KCB BHYT" : "Thêm mới cấp tạm ứng KP KCB BHYT";
        public string IconMode => IsEditProcess ? "Edit" : "PlaylistPlus";
        public override Type ContentType => typeof(CapPhatTamUngKCBBHYTDialog);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public bool IsEnableView { get; set; }
        public string IsEnableValue => IsEnableView ? "True" : "False";
        public List<BhCptuBHYTModel> ListIdsChungTuSummary { get; set; }
        public bool bDeleteDetail { get; set; }

        private BhCptuBHYTModel _cpctBHYTModel;
        public BhCptuBHYTModel CpctBHYTModel
        {
            get => _cpctBHYTModel;
            set => SetProperty(ref _cpctBHYTModel, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = ListLNS != null ? ListLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = ListLNS != null ? ListLNS.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
            }
        }

        public string SelectedCountCsYTe
        {
            get
            {
                int totalCount = ListCsYTe != null ? ListCsYTe.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = ListCsYTe != null ? ListCsYTe.Count(item => item.IsChecked) : 0;
                return string.Format("CƠ SỞ Y TẾ ({0}/{1})", totalSelected, totalCount);
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _listLNSView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
        }

        private string _searchCsYTe;
        public string SearchCsYTe
        {
            get => _searchCsYTe;
            set
            {
                if (SetProperty(ref _searchCsYTe, value))
                {
                    _listCsYTeView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountCsYTe));
                }
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => ListLNS.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                foreach (CheckBoxItem item in ListLNS.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllLNS;
                }
            }
        }

        private bool _selectAllCsYTe;
        public bool SelectAllCsYTe
        {
            get => ListCsYTe.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllCsYTe, value);
                foreach (CheckBoxItem item in ListCsYTe.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllCsYTe;
                }
            }
        }

        public bool IsEnabled => Guid.Empty.Equals(CpctBHYTModel.Id);

        public bool IsEnableDonVi => true;

        private ObservableCollection<CheckBoxItem> _listCsYTe;
        public ObservableCollection<CheckBoxItem> ListCsYTe
        {
            get => _listCsYTe;
            set => SetProperty(ref _listCsYTe, value);
        }

        private ObservableCollection<CheckBoxTreeItem> _listLNS;
        public ObservableCollection<CheckBoxTreeItem> ListLNS
        {
            get => _listLNS;
            set => SetProperty(ref _listLNS, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiQuy;
        public ObservableCollection<ComboboxItem> DataLoaiQuy
        {
            get => _dataLoaiQuy;
            set => SetProperty(ref _dataLoaiQuy, value);
        }

        private ComboboxItem _selectedLoaiQuy;
        public ComboboxItem SelectedLoaiQuy
        {
            get => _selectedLoaiQuy;
            set => SetProperty(ref _selectedLoaiQuy, value);
        }

        private ObservableCollection<ComboboxItem> _listChiTietToi;
        public ObservableCollection<ComboboxItem> ListChiTietToi
        {
            get => _listChiTietToi;
            set => SetProperty(ref _listChiTietToi, value);
        }

        private ComboboxItem _selectedChiTietToi;
        public ComboboxItem SelectedChiTietToi
        {
            get => _selectedChiTietToi;
            set
            {
                if (SetProperty(ref _selectedChiTietToi, value) && _selectedChiTietToi != null)
                {
                    LoadLNS();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiKinhPhi;
        public ObservableCollection<ComboboxItem> ItemsLoaiKinhPhi
        {
            get => _itemsLoaiKinhPhi;
            set => SetProperty(ref _itemsLoaiKinhPhi, value);
        }

        private ComboboxItem _selectedLoaiKinhPhi;
        public ComboboxItem SelectedLoaiKinhPhi
        {
            get => _selectedLoaiKinhPhi;
            set
            {
                if (SetProperty(ref _selectedLoaiKinhPhi, value) && _selectedLoaiKinhPhi != null)
                {
                    CpctBHYTModel.SDSLNS = CapKinhPhi.GetLns(int.TryParse(value.ValueItem, out int loaiKinhPhi) ? loaiKinhPhi : (int)ConstantNumber.ZERO);
                    CpctBHYTModel.ILoaiKinhPhi = int.TryParse(value.ValueItem, out int iLoaiKinhPhi) ? iLoaiKinhPhi : (int)ConstantNumber.ZERO;
                }
            }
        }

        public RelayCommand SaveCommand { get; }

        public CapPhatTamUngKCBBHYTDialogViewModel(
            INsDonViService donViService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ISessionService sessionService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            ICptuBHYTService cptuBHYTService,
            IDanhMucService danhMucService,
            ICptuBHYTChiTietService cptuBHYTChiTietService,
            ILog logger,
            IMapper mapper)
        {
            _donViService = donViService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _sessionService = sessionService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;
            _cptuBHYTService = cptuBHYTService;
            _cptuBHYTChiTietService = cptuBHYTChiTietService;
            _danhMucService = danhMucService;
            _mapper = mapper;
            _logger = logger;
            SaveCommand = new RelayCommand(o => OnSave());
        }

        public override void Init()
        {
            try
            {
                _sessionInfo = _sessionService.Current;
                bDeleteDetail = false;
                LoadSettingCapPhat();
                LoadLoaiQuy();
                LoadLNS();
                LoadCsYTe();
                IsEnableView = true;
                LoadLoaiKinhPhi();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            if (CpctBHYTModel != null && CpctBHYTModel.Id != Guid.Empty)
            {
                CheckboxSelectedToStringConvert.SetCheckboxSelected(ListLNS, CpctBHYTModel.SDSLNS);
                CheckboxSelectedToStringConvert.SetCheckboxSelected(ListCsYTe, CpctBHYTModel.SDSID_CoSoYTe);
                SelectedLoaiKinhPhi = ItemsLoaiKinhPhi.FirstOrDefault(x => x.ValueItem.Equals(CpctBHYTModel.ILoaiKinhPhi.ToString()));
                OnPropertyChanged(nameof(IsEnableView));
                OnPropertyChanged(nameof(IsEnableValue));
            }
            else
            {
                if (IsSummary)
                {
                    var lstLNS = string.Join(",", ListIdsChungTuSummary.Select(x => x.SDSLNS).ToList());
                    var lstCSYT = string.Join(",", ListIdsChungTuSummary.Select(x => x.SDSID_CoSoYTe).ToList());
                    SelectedLoaiKinhPhi = ItemsLoaiKinhPhi.FirstOrDefault(x => x.ValueItem.Equals(CpctBHYTModel.ILoaiKinhPhi.ToString()));
                    CheckboxSelectedToStringConvert.SetCheckboxSelected(_listLNS, lstLNS);
                    CheckboxSelectedToStringConvert.SetCheckboxSelected(_listCsYTe, lstCSYT);
                }
                else
                {
                    SelectedLoaiKinhPhi = ItemsLoaiKinhPhi.FirstOrDefault();
                }
                var soChungTuIndex = _cptuBHYTService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                CpctBHYTModel = new BhCptuBHYTModel()
                {
                    DNgayChungTu = DateTime.Now,
                    DNgayTao = DateTime.Now,
                    DNgayQuyetDinh = DateTime.Now,
                    SSoChungTu = "CP-" + soChungTuIndex.ToString("D3"),
                    SNguoiTao = _sessionInfo.Principal,
                    INamLamViec = _sessionInfo.YearOfWork,
                    IIDMaDonVi = _sessionInfo.IdDonVi
                };
            }
        }

        private void LoadSettingCapPhat()
        {
            DanhMuc dmCapPhatToanDonVi = _danhMucService.FindByCode(MaDanhMuc.CAP_PHAT_TOAN_DON_VI);
            if (dmCapPhatToanDonVi != null)
                bool.TryParse(dmCapPhatToanDonVi.SGiaTri, out _isCapPhatToanDonVi);
            else _isCapPhatToanDonVi = false;
        }

        private void LoadLoaiKinhPhi()
        {
            ObservableCollection<ComboboxItem> lstKinhPhi = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem
                {
                    ValueItem = ((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN).ToString(),
                    DisplayItem = CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN,
                    HiddenValue = LNSValue.LNS_9040001
                },
                new ComboboxItem
                {
                    ValueItem = ((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD).ToString(),
                    DisplayItem = CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD,
                    HiddenValue = LNSValue.LNS_9040002
                },

            };
            ItemsLoaiKinhPhi = lstKinhPhi;
        }

        private void LoadLoaiQuy()
        {
            DataLoaiQuy = new ObservableCollection<ComboboxItem>();
            DataLoaiQuy.Add(new ComboboxItem { ValueItem = "1", DisplayItem = "Quý I" });
            DataLoaiQuy.Add(new ComboboxItem { ValueItem = "2", DisplayItem = "Quý II" });
            DataLoaiQuy.Add(new ComboboxItem { ValueItem = "3", DisplayItem = "Quý III" });
            DataLoaiQuy.Add(new ComboboxItem { ValueItem = "4", DisplayItem = "Quý IV" });
            if (CpctBHYTModel.Id == Guid.Empty)
                SelectedLoaiQuy = DataLoaiQuy.FirstOrDefault();
            else
                SelectedLoaiQuy = DataLoaiQuy.Where(n => n.ValueItem == CpctBHYTModel.IQuy.ToString()).FirstOrDefault();
        }

        public void LoadCsYTe()
        {
            ListCsYTe = new ObservableCollection<CheckBoxItem>();

            var predicate = PredicateBuilder.True<BhDmCoSoYTe>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);

            IEnumerable<BhDmCoSoYTe> listDmYte = _bhDmCoSoYTeService.FindByCondition(predicate);
            ListCsYTe = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDmYte);
            // Filter
            _listCsYTeView = CollectionViewSource.GetDefaultView(ListCsYTe);
            _listCsYTeView.Filter = ListDonViFilter;
            foreach (var model in ListCsYTe)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        OnPropertyChanged(nameof(SelectedCountCsYTe));
                        OnPropertyChanged(nameof(SelectAllCsYTe));
                    }
                };
            }
        }

        private bool ListDonViFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchCsYTe))
                result = item.ValueItem.ToLower().Contains(_searchCsYTe!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public void LoadLNS()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            string idDonVi = _sessionInfo.IdDonVi;

            var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(n => n.SXauNoiMa.StartsWith("904000"));
            var listLNSPrev = _bhDmMucLucNganSachService.FindByCondition(predicate).ToList().OrderBy(n => n.SXauNoiMa);

            ListLNS = new ObservableCollection<CheckBoxTreeItem>();
            ListLNS = _mapper.Map<ObservableCollection<CheckBoxTreeItem>>(listLNSPrev);
            // Filter
            _listLNSView = CollectionViewSource.GetDefaultView(ListLNS);
            _listLNSView.Filter = ListLNSFilter;
            foreach (CheckBoxTreeItem model in ListLNS)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        FindChildCheckbox(model);
                        OnPropertyChanged(nameof(SelectedCountLNS));
                        OnPropertyChanged(nameof(SelectAllLNS));
                    }
                };
            }
        }


        public void FindChildCheckbox(CheckBoxTreeItem parent)
        {
            ListLNS.Where(n => n.ParentId == parent.Id).Select(n => { n.IsChecked = parent.IsChecked; return n; }).ToList();
            if (ListLNS.Where(n => n.ParentId == parent.Id && n.IsChecked == !parent.IsChecked).ToList().Count == 0)
            {
                return;
            }
            else
            {
                foreach (CheckBoxTreeItem item in ListLNS.Where(n => n.ParentId == parent.Id))
                {
                    FindChildCheckbox(item);
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.ValueItem.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public override void OnSave()
        {
            try
            {
                //Validate trước khi save
                string message = GetMessageValidate();
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBoxHelper.Error(message);
                    return;
                }

                if (IsSummary)
                {
                    var namLamViec = _sessionInfo.YearOfWork;

                    CpctBHYTModel.SDSLNS = CapKinhPhi.GetLns(ListIdsChungTuSummary.FirstOrDefault().ILoaiKinhPhi);
                    CpctBHYTModel.SDSID_CoSoYTe = GetCSYTSelected(ListCsYTe);
                    CpctBHYTModel.IQuy = int.Parse(SelectedLoaiQuy.ValueItem);
                    if (CpctBHYTModel.Id == Guid.Empty)
                    {
                        var listSoChungTuTongHopString = string.Join(",", ListIdsChungTuSummary.Select(x => x.SSoChungTu).ToList());
                        var listSummaryVoucher = _cptuBHYTService.FindAggregateVoucher(namLamViec);
                        if (listSummaryVoucher.Any())
                        {
                            var firsChungTuSummary = listSummaryVoucher.FirstOrDefault();
                            if (!firsChungTuSummary.BIsKhoa)
                            {
                                MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                                if (messageBoxResult == MessageBoxResult.No)
                                {
                                    return;
                                }
                                var idChungTuSummary = firsChungTuSummary.Id;
                                var khtChungTuSummary = _cptuBHYTService.FindById(idChungTuSummary);
                                UpdateChungTuDaTongHop(khtChungTuSummary);
                                _cptuBHYTService.Delete(khtChungTuSummary);
                                var predicateSummaryDetail = PredicateBuilder.True<BhCptuBHYTChiTiet>();
                                predicateSummaryDetail = predicateSummaryDetail.And(x => x.IID_BH_CP_CapTamUng_KCB_BHYT == idChungTuSummary);
                                var chungTuChiTiets = _cptuBHYTChiTietService.FindByCondition(predicateSummaryDetail);
                                _cptuBHYTChiTietService.RemoveRange(chungTuChiTiets);
                            }
                            else
                            {
                                MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                                return;
                            }
                        }

                        BhCptuBHYT chungTuTongHop = new BhCptuBHYT();
                        _mapper.Map(CpctBHYTModel, chungTuTongHop);
                        chungTuTongHop.SDSSoChungTuTongHop = listSoChungTuTongHopString;
                        chungTuTongHop.BIsTongHop = true;
                        //chungTuTongHop.SDSLNS = GetValueSelected(ListLNS);
                        chungTuTongHop.SDSID_CoSoYTe = GetCSYTSelected(ListCsYTe);
                        chungTuTongHop.IQuy = int.Parse(SelectedLoaiQuy.ValueItem);
                        _cptuBHYTService.Add(chungTuTongHop);
                        CreateDemandVoucherDetail(_mapper.Map<BhCptuBHYTModel>(chungTuTongHop));
                        var listCtChiTiet = _cptuBHYTChiTietService.FindByCondition(item => item.IID_BH_CP_CapTamUng_KCB_BHYT.Equals(chungTuTongHop.Id)).ToList();
                        if (listCtChiTiet.Count > 0)
                        {
                            chungTuTongHop.FQTQuyTruoc = listCtChiTiet.Sum(item => item.FQTQuyTruoc);
                            chungTuTongHop.FTamUngQuyNay = listCtChiTiet.Sum(item => item.FTamUngQuyNay);

                            _cptuBHYTService.Update(chungTuTongHop);
                        }
                        DialogHost.Close("RootDialog");
                        SavedAction?.Invoke(_mapper.Map<BhCptuBHYTModel>(chungTuTongHop));
                    }
                    else
                    {
                        BhCptuBHYT chungTu;
                        chungTu = _cptuBHYTService.FindById(CpctBHYTModel.Id);
                        CpctBHYTModel.DNgaySua = DateTime.Now;
                        CpctBHYTModel.SNguoiSua = _sessionInfo.Principal;
                        CpctBHYTModel.SNguoiTao = _sessionInfo.Principal;
                        _mapper.Map(CpctBHYTModel, chungTu);
                        chungTu.DNgayTao = DateTime.Now;
                        _cptuBHYTService.Update(chungTu);
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        SavedAction?.Invoke(_mapper.Map<BhCptuBHYTModel>(chungTu));
                    }
                }
                else
                {
                    CpctBHYTModel.SMoTa = CpctBHYTModel.SMoTa == null ? "" : CpctBHYTModel.SMoTa.Trim();
                    CpctBHYTModel.SDSID_CoSoYTe = GetCSYTSelected(ListCsYTe);
                    CpctBHYTModel.IQuy = int.Parse(SelectedLoaiQuy.ValueItem);
                    BhCptuBHYT chungTu;
                    if (CpctBHYTModel.Id == Guid.Empty)
                    {
                        CpctBHYTModel.SDSLNS = CapKinhPhi.GetLns(int.Parse(SelectedLoaiKinhPhi?.ValueItem ?? "0"));
                        CpctBHYTModel.ILoaiKinhPhi = int.Parse(SelectedLoaiKinhPhi?.ValueItem ?? "0");
                        chungTu = new BhCptuBHYT();
                        _mapper.Map(CpctBHYTModel, chungTu);
                        chungTu.DNgayChungTu = DateTime.Now;
                        chungTu.SNguoiTao = _sessionInfo.Principal;
                        _cptuBHYTService.Add(chungTu);
                    }
                    else
                    {
                        chungTu = _cptuBHYTService.FindById(CpctBHYTModel.Id);
                        CpctBHYTModel.DNgaySua = DateTime.Now;
                        CpctBHYTModel.SNguoiSua = _sessionInfo.Principal;
                        CpctBHYTModel.SNguoiTao = _sessionInfo.Principal;
                        _mapper.Map(CpctBHYTModel, chungTu);
                        _cptuBHYTService.Update(chungTu);
                    }
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    SavedAction?.Invoke(_mapper.Map<BhCptuBHYTModel>(chungTu));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (string.IsNullOrEmpty(CpctBHYTModel.SSoQuyetDinh))
            {
                messages.Add(Resources.AlertSoKeHoachEmpty);
            }
            if (ListCsYTe.All(x => !x.IsChecked))
            {
                messages.Add("Hãy chọn cơ sở y tế");
            }
            //if (ListLNS.All(x => !x.IsChecked))
            //{
            //    messages.Add("Hãy chọn LNS");
            //}
            return string.Join(Environment.NewLine, messages);
        }

        public static string GetValueSelected(ObservableCollection<CheckBoxTreeItem> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsChecked == true).Select(n => n.ValueItem).Distinct().ToList());
            }
            return string.Empty;
        }

        public static string GetCSYTSelected(ObservableCollection<CheckBoxItem> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsChecked == true).Select(n => n.ValueItem).Distinct().ToList());
            }
            return string.Empty;
        }

        private void UpdateChungTuDaTongHop(BhCptuBHYT chungtu)
        {
            var lstSoCtChild = chungtu.SDSSoChungTuTongHop.Split(",");
            foreach (var soct in lstSoCtChild)
            {
                var ctChild = _cptuBHYTService.FindChungTuDaTongHopBySCT(soct, _sessionInfo.YearOfWork).FirstOrDefault();
                if (ctChild != null)
                {
                    ctChild.BIsTongHop = false;
                    _cptuBHYTService.Update(ctChild);
                }
            }
        }

        private void CreateDemandVoucherDetail(BhCptuBHYTModel chungTuModel)
        {
            BhCpTUChungTuChiTietCriteria creation = new BhCpTUChungTuChiTietCriteria()
            {
                ListIdChungTuTongHop = string.Join(",", ListIdsChungTuSummary.Select(x => x.Id.ToString()).ToList()),
                IdChungTu = chungTuModel.Id.ToString(),
                NamLamViec = chungTuModel.INamLamViec,
            };
            _cptuBHYTService.AddAggregate(creation);
        }
    }
}
