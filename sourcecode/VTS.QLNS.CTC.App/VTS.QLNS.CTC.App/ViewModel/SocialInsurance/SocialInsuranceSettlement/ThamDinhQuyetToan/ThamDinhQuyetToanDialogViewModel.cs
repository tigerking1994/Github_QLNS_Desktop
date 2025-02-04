using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH
{
    public class ThamDinhQuyetToanDialogViewModel : DialogViewModelBase<BhThamDinhQuyetToanModel>
    {
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDmThamDinhQuyetToanService _bhDmThamDinhQuyetToanService;
        private readonly IBhThamDinhQuyetToanService _bhThamDinhQuyetToanService;
        private readonly IBhThamDinhQuyetToanChiTietService _bhThamDinhQuyetToanChiTietService;
        private readonly ISessionService _sessionService;
        private readonly ISysAuditLogService _log;
        private readonly INsDonViService _nSDonViService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private ICollectionView _dataLNSView;
        private ICollectionView _dataCSYTView;
        private ICollectionView _nsDonViModelsView;

        public override Type ContentType => typeof(ThamDinhQuyetToanDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI" : "CẬP NHẬT";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới báo cáo thẩm định quyết toán" : "Cập nhật báo cáo thẩm định quyết toán";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        public bool IsSummary { get; set; }
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);
        public List<BhQtcnBHXHModel> ListIdsChungTuSummary { get; set; }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }
        public bool IsAggregate { get; set; } = false;
        public bool IsEdit => Model.Id == Guid.Empty && !IsSummary;

        public List<string> ListIdDonViHasCt { get; set; }
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

        private ObservableCollection<DonViModel> _donViModelItems;
        public ObservableCollection<DonViModel> DonViModelItems
        {
            get => _donViModelItems;
            set
            {
                SetProperty(ref _donViModelItems, value);
                OnPropertyChanged();
            }
        }
        public string SelectedCountNsDonVi
        {
            get
            {
                var totalCount = DonViModelItems != null ? DonViModelItems.Count() : 0;
                var totalSelected = DonViModelItems != null ? DonViModelItems.Count(item => item.Selected) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }
        #region list LNS

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
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
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
        #endregion

        public ThamDinhQuyetToanDialogViewModel(
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmThamDinhQuyetToanService bhDmThamDinhQuyetToanService,
            IBhThamDinhQuyetToanService bhThamDinhQuyetToanService,
            IBhThamDinhQuyetToanChiTietService bhThamDinhQuyetToanChiTietService,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nSDonViService,
            ISysAuditLogService log,
            ILog logger)
        {
            _sessionService = sessionService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDmThamDinhQuyetToanService = bhDmThamDinhQuyetToanService;
            _bhThamDinhQuyetToanService = bhThamDinhQuyetToanService;
            _bhThamDinhQuyetToanChiTietService = bhThamDinhQuyetToanChiTietService;
            _nSDonViService = nSDonViService;
            _log = log;
            _mapper = mapper;
            _logger = logger;
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            IsSaveData = true;
            LoadUnits();
            LoadLNS();
            LoadData();
        }

        private void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            List<BhDmMucLucNganSach> listMLNS;
            listMLNS = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(yearOfWork, LNSValue.LNS_9010001_9010002).ToList();

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

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (BhDmMucLucNganSachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.LNSDisplay.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadUnits()
        {
            var iTrangThai = KhcStatusType.ACTIVE;
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == iTrangThai);
            var lstChungTu = _bhThamDinhQuyetToanService.FindAll(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            ListIdDonViHasCt = lstChungTu.Select(item => item.IID_MaDonVi).ToList();

            // remove 999 hard code
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.NOI_BO);
            bool isDvCap4 = _nSDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            if (IsAggregate)
            {

            }
            else if (isDvCap4)
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.ROOT);
                if (Model.Id == Guid.Empty)
                {
                    predicate = predicate.And(x => !ListIdDonViHasCt.Any(y => y == x.IIDMaDonVi));
                }
                else
                {
                    var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IID_MaDonVi).ToList();
                    predicate = predicate.And(x => !idDonVisExclude.Any(y => y == x.IIDMaDonVi));
                }
            }
            else
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT));
                if (Model.Id == Guid.Empty)
                {
                    predicate = predicate.And(x => !ListIdDonViHasCt.Any(y => y == x.IIDMaDonVi));
                }
                else
                {
                    var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IID_MaDonVi).ToList();
                    predicate = predicate.And(x => !idDonVisExclude.Any(y => y == x.IIDMaDonVi));
                }
            }

            var listUnit = _nSDonViService.FindByCondition(predicate).Where(x => x.NamLamViec == yearOfWork).ToList();
            var listDonViByUser = _nSDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT)).Select(x => x.IIDMaDonVi);

            DonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi)));
            if (!string.IsNullOrEmpty(Model.IID_MaDonVi))
            {
                DonViModelItems.Where(x => x.IIDMaDonVi == Model.IID_MaDonVi).Select(x =>
                {
                    x.Selected = true;
                    return x;
                }).ToList();
            }
            _nsDonViModelsView = CollectionViewSource.GetDefaultView(DonViModelItems);
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                ListSortDirection.Ascending));
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.IIDMaDonVi),
                ListSortDirection.Ascending));
            _nsDonViModelsView.Filter = DonViFilter;
            foreach (var model in DonViModelItems)
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

        public override void LoadData(params object[] args)
        {
            if (Model == null || Model.Id == Guid.Empty)
            {
                Model.DNgayChungTu = DateTime.Now;
                Model.INamLamViec = _sessionInfo.YearOfWork;
                LoadChungTuIndex();
            }
            else
            {
                DonViModel donViModel = DonViModelItems.Where(x => x.IIDMaDonVi == Model.IID_MaDonVi).FirstOrDefault();
                if (donViModel != null)
                {
                    donViModel.Selected = true;
                }

                //SetCheckboxSelected(_dataLNS, Model.SDSLNS);
            }
        }

        private bool DonViFilter(object obj)
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

        public override void OnSave()
        {
            try
            {
                base.OnSave();
                DateTime dtNow = DateTime.Now;
                string message = GetMessageValidate();

                BhThamDinhQuyetToan entity;
                var lstDonVi = _nSDonViService.FindAll().Where(x => x.NamLamViec == _sessionInfo.YearOfWork).ToList();
                if (Model.Id == Guid.Empty)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        System.Windows.Forms.MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var donViSelected = DonViModelItems.FirstOrDefault(n => n.Selected);
                    entity = new BhThamDinhQuyetToan();
                    _mapper.Map(Model, entity);
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.DNgaySua = null;
                    entity.INamLamViec = _sessionInfo.YearOfWork;
                    entity.IID_MaDonVi = donViSelected.IIDMaDonVi;

                    entity.SNguoiTao = _sessionService.Current.Principal;
                    _bhThamDinhQuyetToanService.Add(entity);
                }
                else
                {
                    entity = _bhThamDinhQuyetToanService.Find(Model.Id);
                    entity.DNgayChungTu = (DateTime)Model.DNgayChungTu;
                    entity.SSoChungTu = Model.SSoChungTu;
                    entity.SMoTa = Model.SMoTa;
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionInfo.Principal;
                    _bhThamDinhQuyetToanService.Update(entity);
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<BhThamDinhQuyetToanModel>(entity));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (Model.DNgayChungTu == null)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            var donViSelected = DonViModelItems.FirstOrDefault(n => n.Selected);
            if (donViSelected == null)
            {
                messages.Add(Resources.MsgCheckDonVi);
            }
            else
            {
                //Check đã tồn tại đơn vị
                var predicate = PredicateBuilder.True<BhThamDinhQuyetToan>();
                predicate = predicate.And(x => x.IID_MaDonVi == donViSelected.IIDMaDonVi);
                predicate = predicate.And(x => x.INamLamViec == Model.INamLamViec);

                var chungtu = _bhThamDinhQuyetToanService.FindAll(predicate).FirstOrDefault();
                if (chungtu != null)
                {
                    messages.Add(string.Format(Resources.AlertExistSettlementMonthVoucher, donViSelected.TenDonViDisplay, Model.INamLamViec, ""));
                }
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

        private void LoadChungTuIndex()
        {
            var listCT = _bhThamDinhQuyetToanService.FindAll(x => x.INamLamViec == _sessionService.Current.YearOfWork).OrderByDescending(x => x.SSoChungTu);
            if (!listCT.Any()) Model.SSoChungTu = "QT-001";
            else
            {
                try
                {
                    var soCT = listCT.Select(x => x.SSoChungTu).FirstOrDefault();
                    Model.SSoChungTu = "QT-" + (int.Parse(soCT.Substring(3, 3)) + 1).ToString("D3");
                }
                catch
                {
                    Model.SSoChungTu = "QT-" + (listCT.Count() + 1).ToString("D3");
                }
                //OnPropertyChanged(nameof(Division));
            }

        }
    }
}
