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
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung;
using VTS.QLNS.CTC.App.Model.Control;
using System.Windows;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung
{
    public class CapPhatBoSungDialogViewModel : DialogViewModelBase<BhCpBsChungTuModel>
    {
        private readonly IBhCpBsChungTuService _bhCpBsChungTuService;
        private readonly IBhCpBsChungTuChiTietService _bhCpBsChungTuChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDmCoSoYTeService _bhDmCoSoYTeService;
        private readonly ISessionService _sessionService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private ICollectionView _dataLNSView;
        private ICollectionView _dataCSYTView;

        public override Type ContentType => typeof(CapPhatBoSungDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI CHỨNG TỪ" : "CẬP NHẬT CHỨNG TỪ";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới chứng từ cấp phát bổ sung" : "Cập nhật chứng từ cấp phát bổ sung";
        public bool isSummary { get; set; }
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);
        public List<BhCpBsChungTuModel> ListIdsChungTuSummary { get; set; }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }
        public bool IsEdit => Model.Id == Guid.Empty && !isSummary;

        private ObservableCollection<BhDmCoSoYTeModel> _dataCSYT;
        public ObservableCollection<BhDmCoSoYTeModel> DataCSYT
        {
            get => _dataCSYT;
            set => SetProperty(ref _dataCSYT, value);
        }

        public string SelectedCountCSYT
        {
            get
            {
                int totalCount = DataCSYT != null ? DataCSYT.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DataCSYT != null ? DataCSYT.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN CƠ SỞ Y TẾ ({0}/{1})", totalSelected, totalCount);
            }
        }

        private bool _selectAllCSYT;
        public bool SelectAllCSYT
        {
            get => (DataCSYT == null || !DataCSYT.Any()) ? false : DataCSYT.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllCSYT, value);
                if (DataCSYT != null)
                {
                    DataCSYT.Select(c => { c.IsSelected = _selectAllCSYT; return c; }).ToList();
                }
            }
        }

        private string _searchCSYT;
        public string SearchCSYT
        {
            get => _searchCSYT;
            set
            {
                if (SetProperty(ref _searchCSYT, value))
                {
                    _dataCSYTView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountCSYT));
                }
            }
        }

        private ObservableCollection<BhDmMucLucNganSachModel> _dataLNS;
        public ObservableCollection<BhDmMucLucNganSachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = DataLNS != null ? DataLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
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
                if (SetProperty(ref _searchLNS, value))
                {
                    _dataLNSView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountLNS));
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
                    Model.SDslns = CapKinhPhi.GetLns(int.TryParse(value.ValueItem, out int loaiKinhPhi) ? loaiKinhPhi : (int)ConstantNumber.ZERO);
                    Model.ILoaiKinhPhi = int.TryParse(value.ValueItem, out int iLoaiKinhPhi) ? iLoaiKinhPhi : (int)ConstantNumber.ZERO;
                }
            }
        }

        public DateTime? DNgayChungTu { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }

        private ComboboxItem _cbxQuarterSelected;
        public ComboboxItem CbxQuarterSelected
        {
            get => _cbxQuarterSelected;
            set
            {
                SetProperty(ref _cbxQuarterSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _cbxQuarter;
        public ObservableCollection<ComboboxItem> CbxQuarter
        {
            get => _cbxQuarter;
            set => SetProperty(ref _cbxQuarter, value);
        }

        public CapPhatBoSungDialogViewModel(
            IBhCpBsChungTuService bhCpBsChungTuService,
            IBhCpBsChungTuChiTietService bhCpBsChungTuChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            IMapper mapper,
            ISessionService sessionService,
            ISysAuditLogService log,
            ILog logger)
        {
            _sessionService = sessionService;
            _bhCpBsChungTuService = bhCpBsChungTuService;
            _bhCpBsChungTuChiTietService = bhCpBsChungTuChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;
            _log = log;
            _mapper = mapper;
            _logger = logger;
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            IsSaveData = true;
            _searchLNS = string.Empty;
            _searchCSYT = string.Empty;
            LoadLNS();
            LoadCSYT();
            LoadQuarters();
            LoadLoaiKinhPhi();
            LoadData();
            //SelectAllLNS = true;
        }

        private void LoadQuarters()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q1], ValueItem = ((int)QuarterEnum.Q1).ToString()},
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q2], ValueItem = ((int)QuarterEnum.Q2).ToString()},
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q3], ValueItem = ((int)QuarterEnum.Q3).ToString()},
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q4], ValueItem = ((int)QuarterEnum.Q4).ToString()}
            };

            CbxQuarter = new ObservableCollection<ComboboxItem>(cbxVoucher);
            if (Model != null && Model.Id != Guid.Empty && Model.IQuy.HasValue)
            {
                CbxQuarterSelected = CbxQuarter.Single(item => item.ValueItem.Equals(Model.IQuy.ToString()));
            }
            else CbxQuarterSelected = CbxQuarter.FirstOrDefault();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);

            if (Model != null && Model.Id != Guid.Empty)
            {
                SetCheckboxSelected(_dataLNS, Model.SDslns);
                SetCheckboxSelectedCSYT(_dataCSYT, Model.SCoSoYTe);
                SelectedLoaiKinhPhi = ItemsLoaiKinhPhi.FirstOrDefault(x => x.ValueItem.Equals(Model.ILoaiKinhPhi.ToString()));
                DNgayChungTu = Model.DNgayChungTu;
                DNgayQuyetDinh = Model.DNgayQuyetDinh;
            }
            else
            {
                if (isSummary)
                {
                    var lstLNS = string.Join(",", ListIdsChungTuSummary.Select(x => x.SDslns).ToList());
                    var lstCSYT = string.Join(",", ListIdsChungTuSummary.Select(x => x.SCoSoYTe).ToList());
                    SelectedLoaiKinhPhi = ItemsLoaiKinhPhi.FirstOrDefault(x => x.ValueItem.Equals(Model.ILoaiKinhPhi.ToString()));
                    SetCheckboxSelected(_dataLNS, lstLNS);
                    SetCheckboxSelectedCSYT(_dataCSYT, lstCSYT);
                }
                else
                {
                    SelectedLoaiKinhPhi = ItemsLoaiKinhPhi.FirstOrDefault();
                }
                var soChungTuIndex = _bhCpBsChungTuService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                Model = new BhCpBsChungTuModel()
                {
                    DNgayChungTu = DateTime.Now,
                    DNgayTao = DateTime.Now,
                    DNgayQuyetDinh = DateTime.Now,
                    SSoChungTu = "CP-" + soChungTuIndex.ToString("D3"),
                    SNguoiTao = _sessionInfo.Principal,
                    INamLamViec = _sessionInfo.YearOfWork,
                    IIDMaDonVi = _sessionInfo.IdDonVi
                };
                DNgayChungTu = DateTime.Now;
                DNgayQuyetDinh = DateTime.Now;
            }
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

        private void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;

            var listMLNS = _bhDmMucLucNganSachService.GetListBhytMucLucNs(yearOfWork, BhxhMLNS.KCB_BHYT_CPBS).ToList();
            DataLNS = _mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(listMLNS);

            _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
            _dataLNSView.Filter = ListLNSFilter;

            if (_dataLNS != null && _dataLNS.Count > 0)
            {
                foreach (var model in _dataLNS)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsSelected))
                        {
                            foreach (var item in _dataLNS)
                            {
                                if (item.IIDMLNSCha == model.IIDMLNS)
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

        private void LoadCSYT()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;

            var listCSYT = _bhDmCoSoYTeService.GetListCoSoYTe(yearOfWork).ToList();
            DataCSYT = _mapper.Map<ObservableCollection<BhDmCoSoYTeModel>>(listCSYT);

            _dataCSYTView = CollectionViewSource.GetDefaultView(DataCSYT);
            _dataCSYTView.Filter = ListCSYTFilter;

            if (_dataCSYT != null && _dataCSYT.Count > 0)
            {
                foreach (var model in _dataCSYT)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDmCoSoYTeModel.IsSelected))
                        {
                            OnPropertyChanged(nameof(SelectAllCSYT));
                            OnPropertyChanged(nameof(SelectedCountCSYT));
                        }
                    };
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (BhDmMucLucNganSachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.LNSDisplay.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private bool ListCSYTFilter(object obj)
        {
            bool result = true;
            var item = (BhDmCoSoYTeModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.Display.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public override void OnSave()
        {
            base.OnSave();

            DateTime dtNow = DateTime.Now;
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.Forms.MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (isSummary)
            {
                var namLamViec = _sessionInfo.YearOfWork;
                Model.SDslns = CapKinhPhi.GetLns(ListIdsChungTuSummary.FirstOrDefault()?.ILoaiKinhPhi ?? 0);
                Model.ILoaiKinhPhi = ListIdsChungTuSummary.FirstOrDefault()?.ILoaiKinhPhi;
                Model.SCoSoYTe = GetCSYTSelected(DataCSYT);
                Model.IQuy = int.Parse(CbxQuarterSelected.ValueItem);
                if (Model.Id == Guid.Empty)
                {
                    var listSoChungTuTongHopString = string.Join(",", ListIdsChungTuSummary.Select(x => x.SSoChungTu).ToList());
                    var listSummaryVoucher = _bhCpBsChungTuService.FindByCondition(namLamViec, BhxhLoaiChungTu.BhxhChungTuTongHop);
                    if (listSummaryVoucher.Any())
                    {
                        var firsChungTuSummary = listSummaryVoucher.FirstOrDefault();
                        if (!firsChungTuSummary.BKhoa)
                        {
                            MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                            if (messageBoxResult == MessageBoxResult.No)
                            {
                                return;
                            }
                            var idChungTuSummary = firsChungTuSummary.Id;
                            var khtChungTuSummary = _bhCpBsChungTuService.FindById(idChungTuSummary);
                            UpdateChungTuDaTongHop(khtChungTuSummary);
                            _bhCpBsChungTuService.Delete(khtChungTuSummary);
                            var predicateSummaryDetail = PredicateBuilder.True<BhCpBsChungTuChiTiet>();
                            predicateSummaryDetail = predicateSummaryDetail.And(x => x.IIDCTCapPhatBS == idChungTuSummary);
                            var chungTuChiTiets = _bhCpBsChungTuChiTietService.FindByCondition(predicateSummaryDetail);
                            _bhCpBsChungTuChiTietService.RemoveRange(chungTuChiTiets);
                        }
                        else
                        {
                            MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                            return;
                        }
                    }

                    BhCpBsChungTu chungTuTongHop = new BhCpBsChungTu();
                    _mapper.Map(Model, chungTuTongHop);
                    chungTuTongHop.SDSSoChungTuTongHop = listSoChungTuTongHopString;
                    chungTuTongHop.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                    chungTuTongHop.BDaTongHop = true;
                    //chungTuTongHop.SDslns = GetValueSelected(DataLNS);
                    chungTuTongHop.SCoSoYTe = GetCSYTSelected(DataCSYT);
                    chungTuTongHop.IQuy = int.Parse(CbxQuarterSelected.ValueItem);
                    _bhCpBsChungTuService.Add(chungTuTongHop);
                    CreateDemandVoucherDetail(_mapper.Map<BhCpBsChungTuModel>(chungTuTongHop));
                    var listCtChiTiet = _bhCpBsChungTuChiTietService.FindByCondition(item => item.IIDCTCapPhatBS.Equals(chungTuTongHop.Id)).ToList();
                    if (listCtChiTiet.Count > 0)
                    {
                        chungTuTongHop.FTongDaQuyetToan = listCtChiTiet.Sum(item => item.FDaQuyetToan);
                        chungTuTongHop.FTongDaCapUng = listCtChiTiet.Sum(item => item.FDaCapUng);
                        chungTuTongHop.FTongThuaThieu = listCtChiTiet.Sum(item => item.FThuaThieu);
                        chungTuTongHop.FTongSoCapBoSung = listCtChiTiet.Sum(item => item.FSoCapBoSung);

                        _bhCpBsChungTuService.Update(chungTuTongHop);
                    }
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    DialogHost.Close("RootDialog");
                    SavedAction?.Invoke(_mapper.Map<BhCpBsChungTuModel>(chungTuTongHop));
                }
                else
                {
                    BhCpBsChungTu chungTu;
                    chungTu = _bhCpBsChungTuService.FindById(Model.Id);
                    Model.DNgaySua = DateTime.Now;
                    Model.SNguoiSua = _sessionInfo.Principal;
                    Model.SNguoiTao = _sessionInfo.Principal;
                    _mapper.Map(Model, chungTu);
                    chungTu.DNgayTao = DateTime.Now;
                    chungTu.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                    _bhCpBsChungTuService.Update(chungTu);
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    SavedAction?.Invoke(_mapper.Map<BhCpBsChungTuModel>(chungTu));
                }
            }
            else
            {
                Model.SMoTa = Model.SMoTa == null ? "" : Model.SMoTa.Trim();
                //Model.SDslns = GetValueSelected(DataLNS);
                Model.SDslns = CapKinhPhi.GetLns(int.Parse(SelectedLoaiKinhPhi?.ValueItem ?? "0"));
                Model.ILoaiKinhPhi = int.Parse(SelectedLoaiKinhPhi?.ValueItem ?? "0");
                Model.SCoSoYTe = GetCSYTSelected(DataCSYT);
                Model.IQuy = int.Parse(CbxQuarterSelected.ValueItem);
                BhCpBsChungTu chungTu;
                if (Model.Id == Guid.Empty)
                {
                    chungTu = new BhCpBsChungTu();
                    _mapper.Map(Model, chungTu);
                    chungTu.DNgayChungTu = DateTime.Now;
                    chungTu.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTu;

                    _bhCpBsChungTuService.Add(chungTu);
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                }
                else
                {
                    chungTu = _bhCpBsChungTuService.FindById(Model.Id);
                    Model.DNgaySua = DateTime.Now;
                    Model.SNguoiSua = _sessionInfo.Principal;
                    Model.SNguoiTao = _sessionInfo.Principal;
                    _mapper.Map(Model, chungTu);
                    _bhCpBsChungTuService.Update(chungTu);
                    _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<BhCpBsChungTuModel>(chungTu));
            }
        }

        private void UpdateChungTuDaTongHop(BhCpBsChungTu chungtu)
        {
            var lstSoCtChild = chungtu.SDSSoChungTuTongHop.Split(",");
            foreach (var soct in lstSoCtChild)
            {
                var ctChild = _bhCpBsChungTuService.FindChungTuDaTongHopBySCT(soct, _sessionInfo.YearOfWork).FirstOrDefault();
                if (ctChild != null)
                {
                    ctChild.BDaTongHop = false;
                    _bhCpBsChungTuService.Update(ctChild);
                }
            }
        }
        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (!DNgayChungTu.HasValue)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                messages.Add(Resources.AlertSoKeHoachEmpty);
            }

            if (!DNgayQuyetDinh.HasValue)
            {
                messages.Add(Resources.AlertNgayQuyetDinhEmpty);
            }

            //if (DataLNS.All(x => !x.IsSelected))
            //{
            //    messages.Add(Resources.AlertLNSEmpty);
            //}

            if (DataCSYT.All(x => !x.IsSelected))
            {
                messages.Add(Resources.AlertCSYTEmpty);
            }

            if (CbxQuarterSelected == null)
            {
                messages.Add(Resources.AlertQuartyEmpty);
            }
            return string.Join(Environment.NewLine, messages);
        }

        public static string GetValueSelected(ObservableCollection<BhDmMucLucNganSachModel> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsSelected == true).Select(n => n.SLNS).Distinct().ToList());
            }
            return string.Empty;
        }

        public static void SetCheckboxSelected(ObservableCollection<BhDmMucLucNganSachModel> data, string value)
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            List<string> selectedValues = value.Split(",").Distinct().ToList();
            foreach (BhDmMucLucNganSachModel item in data)
            {
                item.IsSelected = selectedValues.Contains(item.SLNS);
            }
        }

        private void CreateDemandVoucherDetail(BhCpBsChungTuModel chungTuModel)
        {
            BhCpBsChungTuChiTietCriteria creation = new BhCpBsChungTuChiTietCriteria()
            {
                ListIdChungTuTongHop = string.Join(",", ListIdsChungTuSummary.Select(x => x.Id.ToString()).ToList()),
                IdChungTu = chungTuModel.Id.ToString(),
                NamLamViec = chungTuModel.INamLamViec,
            };
            _bhCpBsChungTuService.AddAggregate(creation);
        }

        public static void SetCheckboxSelectedCSYT(ObservableCollection<BhDmCoSoYTeModel> data, string value)
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            List<string> selectedValues = value.Split(",").Distinct().ToList();
            foreach (BhDmCoSoYTeModel item in data)
            {
                item.IsSelected = selectedValues.Contains(item.IIDMaCoSoYTe);
            }
        }

        public static string GetCSYTSelected(ObservableCollection<BhDmCoSoYTeModel> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsSelected == true).Select(n => n.IIDMaCoSoYTe).Distinct().ToList());
            }
            return string.Empty;
        }
    }
}
