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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT
{
    public class QuyetToanCapKinhPhiKcbDialogViewModel : DialogViewModelBase<BhQtCapKinhPhiKcbModel>
    {
        private readonly IQtcCapKinhPhiKcbService _chungTuService;
        private readonly IQtcCapKinhPhiKcbChiTietService _chungTuChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDmCoSoYTeService _bhDmCoSoYTeService;
        private readonly ISessionService _sessionService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private ICollectionView _dataLNSView;
        private ICollectionView _dataCSYTView;

        public override Type ContentType => typeof(QuyetToanCapKinhPhiKcbDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI" : "CẬP NHẬT";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới báo cáo quyết toán KP KCB BHYT" : "Cập nhật báo cáo quyết toán KP KCB BHYT";
        public bool isSummary { get; set; }
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);

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
            set
            {
                SetProperty(ref _dataCSYT, value);
                OnPropertyChanged();
            }
        }

        public string SelectedCountCSYT
        {
            get
            {
                int totalCount = DataCSYT != null ? DataCSYT.Count() : 0;
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
                }
            }
        }

        private ObservableCollection<BhDmMucLucNganSachModel> _dataLNS;
        public ObservableCollection<BhDmMucLucNganSachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        private ObservableCollection<BhDmMucLucNganSachModel> _dataLnsByExpenseType;
        public ObservableCollection<BhDmMucLucNganSachModel> DataLnsByExpenseType
        {
            get => _dataLnsByExpenseType;
            set => SetProperty(ref _dataLnsByExpenseType, value);
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

        public DateTime? DNgayChungTu { get; set; }

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

        private ComboboxItem _cbxExpenseTypeSelected;
        public ComboboxItem CbxExpenseTypeSelected
        {
            get => _cbxExpenseTypeSelected;
            set
            {
                SetProperty(ref _cbxExpenseTypeSelected, value);
                if (CbxExpenseTypeSelected != null)
                {
                    if (CbxExpenseTypeSelected.ValueItem == ((int)ExpenseTypeEnum.KCB_QN).ToString())
                    {
                        SetCheckboxSelected(_dataLNS, BhxhMLNS.KCB_BHYT_QN);
                    }
                    else
                    {
                        SetCheckboxSelected(_dataLNS, BhxhMLNS.KCB_BHYT_TNQN_NLD);
                    }
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxExpenseType;
        public ObservableCollection<ComboboxItem> CbxExpenseType
        {
            get => _cbxExpenseType;
            set => SetProperty(ref _cbxExpenseType, value);
        }

        public QuyetToanCapKinhPhiKcbDialogViewModel(
            IQtcCapKinhPhiKcbService iQtcCapKinhPhiKcbService,
            IQtcCapKinhPhiKcbChiTietService iQtcCapKinhPhiKcbChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            IMapper mapper,
            ISessionService sessionService,
            ISysAuditLogService log,
            ILog logger)
        {
            _sessionService = sessionService;
            _chungTuService = iQtcCapKinhPhiKcbService;
            _chungTuChiTietService = iQtcCapKinhPhiKcbChiTietService;
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
            LoadExpenseTypes();
            LoadData();
        }

        private void LoadQuarters()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q1], ValueItem = ((int)QuarterEnum.Q1).ToString()},
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q2], ValueItem = ((int)QuarterEnum.Q2).ToString()},
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q3], ValueItem = ((int)QuarterEnum.Q3).ToString()},
                new ComboboxItem {DisplayItem = Quarters.QuarterName[QuarterEnum.Q4], ValueItem = ((int)QuarterEnum.Q4).ToString()},
                new ComboboxItem {DisplayItem = "Năm " + yearOfWork.ToString(), ValueItem = yearOfWork.ToString()}
            };

            CbxQuarter = new ObservableCollection<ComboboxItem>(cbxVoucher);
            if (Model != null && Model.Id != Guid.Empty && Model.IQuy.HasValue)
            {
                CbxQuarterSelected = CbxQuarter.Single(item => item.ValueItem.Equals(Model.IQuy.ToString()));
            }
            else CbxQuarterSelected = CbxQuarter.FirstOrDefault();
        }

        private void LoadExpenseTypes()
        {
            var cbxExpenseType = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = ExpenseTypes.ExpenseTypeName[ExpenseTypeEnum.KCB_QN], ValueItem = ((int)ExpenseTypeEnum.KCB_QN).ToString()},
                new ComboboxItem {DisplayItem = ExpenseTypes.ExpenseTypeName[ExpenseTypeEnum.KCB_TNQN_NLD], ValueItem = ((int)ExpenseTypeEnum.KCB_TNQN_NLD).ToString()}
            };

            CbxExpenseType = new ObservableCollection<ComboboxItem>(cbxExpenseType);
            if (Model != null && Model.Id != Guid.Empty && Model.ILoaiKinhPhi.HasValue)
            {
                CbxExpenseTypeSelected = CbxExpenseType.Single(item => item.ValueItem.Equals(Model.ILoaiKinhPhi.ToString()));
            }
            else CbxExpenseTypeSelected = CbxExpenseType.FirstOrDefault();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);

            if (Model != null && Model.Id != Guid.Empty)
            {
                SetCheckboxSelected(_dataLNS, Model.SDslns);
                SetCheckboxSelectedCSYT(_dataCSYT, Model.SCoSoYTe);
                DNgayChungTu = Model.DNgayChungTu;
            }
            else
            {
                var soChungTuIndex = _chungTuService.GetVoucherIndex(_sessionInfo.YearOfWork);
                Model = new BhQtCapKinhPhiKcbModel()
                {
                    DNgayChungTu = DateTime.Now,
                    DNgayTao = DateTime.Now,
                    SSoChungTu = "QTC-" + soChungTuIndex.ToString("D3"),
                    SNguoiTao = _sessionInfo.Principal
                };
                DNgayChungTu = DateTime.Now;
            }
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
            Model.SMoTa = Model.SMoTa == null ? "" : Model.SMoTa.Trim();
            Model.SDslns = CapKinhPhi.GetLns(int.Parse(CbxExpenseTypeSelected?.ValueItem ?? "0"));
            Model.SCoSoYTe = GetCSYTSelected(DataCSYT);
            Model.IQuy = int.Parse(CbxQuarterSelected.ValueItem);
            Model.SQuyNamMoTa = CbxQuarterSelected == null ? string.Empty : CbxQuarterSelected.DisplayItem;
            Model.ILoaiKinhPhi = int.Parse(CbxExpenseTypeSelected.ValueItem);
            BhQtCapKinhPhiKcb chungTu;
            if (Model.Id == Guid.Empty)
            {
                chungTu = new BhQtCapKinhPhiKcb();
                _mapper.Map(Model, chungTu);
                chungTu.DNgayChungTu = DateTime.Now;
                chungTu.INamLamViec = _sessionService.Current.YearOfWork;
                _chungTuService.Add(chungTu);
                _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            }
            else
            {
                chungTu = _chungTuService.FindById(Model.Id);
                Model.DNgaySua = DateTime.Now;
                Model.SNguoiSua = _sessionInfo.Principal;
                Model.SNguoiTao = _sessionInfo.Principal;
                _mapper.Map(Model, chungTu);
                _chungTuService.Update(chungTu);
                _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(_mapper.Map<BhQtCapKinhPhiKcbModel>(chungTu));

        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (!DNgayChungTu.HasValue)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
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

            if (CbxExpenseTypeSelected == null)
            {
                messages.Add(Resources.AlertExpenseTypeEmpty);
            }
            return string.Join(Environment.NewLine, messages);
        }

        public static string GetValueSelected(ObservableCollection<BhDmMucLucNganSachModel> data)
        {
            data.ForAll(x =>
            {
                x.IsSelected = true;
            });

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
