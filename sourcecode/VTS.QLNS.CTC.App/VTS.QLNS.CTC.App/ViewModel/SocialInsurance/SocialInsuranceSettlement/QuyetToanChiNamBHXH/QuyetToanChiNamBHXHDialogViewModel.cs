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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH
{
    public class QuyetToanChiNamBHXHDialogViewModel : DialogViewModelBase<BhQtcnBHXHModel>
    {

        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDmCoSoYTeService _bhDmCoSoYTeService;
        private readonly IQtcnBHXHService _qtcnBHXHService;
        private readonly IQtcqBHXHService _qtcqBHXHService;
        private readonly IQtcnBHXHChiTietService _qtcnBHXHChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly ISessionService _sessionService;
        private readonly ISysAuditLogService _log;
        private readonly INsDonViService _nSDonViService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private ICollectionView _dataLNSView;
        private ICollectionView _dataCSYTView;
        private ICollectionView _nsDonViModelsView;

        public override Type ContentType => typeof(QuyetToanChiNamBHXHDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI" : "CẬP NHẬT ";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới quyết toán chi năm BHXH" : "Cập nhật quyết toán chi năm BHXH";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        public bool isSummary { get; set; }
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);
        public List<BhQtcnBHXHModel> ListIdsChungTuSummary { get; set; }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }
        public bool IsEdit => Model.Id == Guid.Empty && !isSummary;

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

        public QuyetToanChiNamBHXHDialogViewModel(
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            IQtcnBHXHService qtcnBHXHService,
            IQtcqBHXHService qtcqBHXHService,
            IQtcnBHXHChiTietService qtcnBHXHChiTietService,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nSDonViService,
            ISysAuditLogService log,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            ILog logger)
        {
            _sessionService = sessionService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;
            _qtcnBHXHService = qtcnBHXHService;
            _qtcqBHXHService = qtcqBHXHService;
            _qtcnBHXHChiTietService = qtcnBHXHChiTietService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
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
            var lstChungTu = _qtcnBHXHService.GetDanhSachQuyetToanNamBHXH(yearOfWork);
            ListIdDonViHasCt = lstChungTu.Select(item => item.IIdMaDonVi).ToList();

            // remove 999 hard code
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.NOI_BO);
            bool isDvCap4 = _nSDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            if (isDvCap4)
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.ROOT);
                if (Model.Id == Guid.Empty)
                {
                    predicate = predicate.And(x => !ListIdDonViHasCt.Any(y => y == x.IIDMaDonVi));
                }
                else
                {
                    var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IIdMaDonVi).ToList();
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
                    var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IIdMaDonVi).ToList();
                    predicate = predicate.And(x => !idDonVisExclude.Any(y => y == x.IIDMaDonVi));
                }
            }

            var listUnit = _nSDonViService.FindByCondition(predicate).Where(x => x.NamLamViec == yearOfWork).ToList();
            var listDonViByUser = _nSDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT)).Select(x => x.IIDMaDonVi);

            DonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi)));
            if (!string.IsNullOrEmpty(Model.IIdMaDonVi))
            {
                DonViModelItems.Where(x => x.IIDMaDonVi == Model.IIdMaDonVi).Select(x =>
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
                Model.DNgayQuyetDinh = DateTime.Now;
                Model.INamLamViec = _sessionInfo.YearOfWork;
                LoadChungTuIndex();
            }
            else
            {
                DonViModel donViModel = DonViModelItems.Where(x => x.IIDMaDonVi == Model.IIdMaDonVi).FirstOrDefault();
                if (donViModel != null)
                {
                    donViModel.Selected = true;
                }

                SetCheckboxSelected(_dataLNS, Model.SDSLNS);
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
        public List<string> CheckQTQuy(Guid id)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<BhQtcqBHXH>();
            predicate = predicate.And(x => x.INamChungTu == yearOfWork && x.IIdDonVi == id);
            predicate = predicate.And(x => x.BIsKhoa);
            var listQuyChungTuByDonvi = _qtcqBHXHService.FindByCondition(predicate).ToList();
            return listQuyChungTuByDonvi.Select(x => x.IIdMaDonVi).ToList();
        }
        public override void OnSave()
        {
            try
            {
                base.OnSave();
                //DateTime dtNow = DateTime.Now
                string message = GetMessageValidate();

                BhQtcnBHXH entity;
                //var lstDonVi = _nSDonViService.FindAll().Where(x => x.NamLamViec == _sessionInfo.YearOfWork).ToList()
                var donViSelected = DonViModelItems.FirstOrDefault(n => n.Selected);

                if (Model.Id == Guid.Empty)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        System.Windows.Forms.MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    // Add
                    entity = new BhQtcnBHXH();
                    _mapper.Map(Model, entity);
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.DNgaySua = null;
                    entity.INamLamViec = _sessionInfo.YearOfWork;
                    entity.IIdDonVi = donViSelected.Id;
                    entity.IIdMaDonVi = donViSelected.IIDMaDonVi;
                    entity.SDSLNS = LNSValue.LNS_9010001_9010002;
                    entity.BThucChiTheo4Quy = Model.BThucChiTheo4Quy;
                    bool isTongHop = false;
                    if (donViSelected != null && donViSelected.Loai == LoaiDonVi.ROOT)
                    {
                        entity.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                    }
                    else
                    {
                        entity.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTu;

                    }

                    entity.SNguoiTao = _sessionService.Current.Principal;
                    _qtcnBHXHService.Add(entity);

                    //Tạo chứng từ chi tiết khi chọn lấy dữ liệu số thực chi theo chứng từ quyết toán BHXH 04 quý
                    if (entity.BThucChiTheo4Quy)
                    {
                        _qtcnBHXHChiTietService.CreateChungTuChiTietTheoQuy(entity.Id, entity.IIdMaDonVi, entity.INamLamViec.Value, entity.SNguoiTao, isTongHop);
                        var lstLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionInfo.YearOfWork);
                        var loaiChi = lstLoaiChi.Where(x => x.SLNS == LNSValue.LNS_901_9010001_9010002).FirstOrDefault();
                        //update chứng từ
                        var chungtu = _qtcnBHXHService.FindById(entity.Id);
                        var listDV = CheckQTQuy(donViSelected.Id);

                        var predicate = PredicateBuilder.True<BhQtcnBHXHChiTiet>();
                        predicate = predicate.And(x => x.IIdQTCNamCheDoBHXH == chungtu.Id);
                        predicate = predicate.And(x => listDV.Contains(x.IIdMaDonVi));
                        var chungtu_chitiet = _qtcnBHXHChiTietService.FindByCondition(predicate).ToList();

                        var listDataQuery = _qtcnBHXHChiTietService.GetChiTietQuyetToanChiNamBHXH(entity.Id, _sessionInfo.YearOfWork, entity.BThucChiTheo4Quy, entity.ILoaiTongHop, entity.IIdMaDonVi).ToList();

                        var lstDuToan = _qtcnBHXHChiTietService.GetTienPhanBoChiTietDuToanChi(_sessionInfo.YearOfWork, loaiChi.SMaLoaiChi, entity.IIdMaDonVi, entity.SDSLNS, entity.DNgayChungTu);

                        chungtu.FTongTienDuToanDuyet = lstDuToan?.Select(x => x.FTienDuToanDuyet).Sum();
                        chungtu.ITongSoSQDeNghi = listDataQuery?.Select(x => x.ISoSQThucChi).Sum();
                        chungtu.FTongTienSQDeNghi = listDataQuery?.Select(x => x.FTienSQThucChi).Sum();
                        chungtu.ITongSoQNCNDeNghi = listDataQuery?.Select(x => x.ISoQNCNThucChi).Sum();
                        chungtu.FTongTienQNCNDeNghi = listDataQuery?.Select(x => x.FTienQNCNThucChi).Sum();
                        chungtu.ITongSoCNVCQPDeNghi = listDataQuery?.Select(x => x.ISoCNVCQPThucChi).Sum();
                        chungtu.FTongTienCNVCQPDeNghi = listDataQuery?.Select(x => x.FTienQNCNThucChi).Sum();
                        chungtu.ITongSoHSQBSDeNghi = listDataQuery?.Select(x => x.ISoHSQBSThucChi).Sum();
                        chungtu.FTongTienHSQBSDeNghi = listDataQuery?.Select(x => x.FTienHSQBSThucChi).Sum();
                        chungtu.ITongSoLDHDDeNghi = chungtu_chitiet?.Select(x => x.ISoLDHDThucChi).Sum();
                        chungtu.FTongTienLDHDDeNghi = listDataQuery?.Select(x => x.FTienLDHDThucChi).Sum();
                        chungtu.ITongSoDeNghi = listDataQuery?.Select(x => x.ITongSoThucChi).Sum();
                        chungtu.FTongTienDeNghi = listDataQuery?.Select(x => x.FTongTienThucChi).Sum();

                        _qtcnBHXHService.Update(chungtu);
                        _qtcnBHXHChiTietService.UpdateRange(chungtu_chitiet);
                    }
                }
                else
                {
                    entity = _qtcnBHXHService.FindById(Model.Id);
                    entity.DNgayChungTu = Model.DNgayChungTu;
                    entity.SSoQuyetDinh = Model.SSoQuyetDinh;
                    entity.DNgayQuyetDinh = Model.DNgayQuyetDinh;
                    entity.BThucChiTheo4Quy = Model.BThucChiTheo4Quy;
                    entity.SDSLNS = LNSValue.LNS_9010001_9010002;
                    entity.SMoTa = Model.SMoTa;
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionInfo.Principal;
                    _qtcnBHXHService.Update(entity);
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<BhQtcnBHXHModel>(entity));
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
                var predicate = PredicateBuilder.True<BhQtcnBHXH>();
                predicate = predicate.And(x => x.IIdDonVi == donViSelected.Id);
                predicate = predicate.And(x => x.IIdMaDonVi == donViSelected.IIDMaDonVi);
                predicate = predicate.And(x => x.INamLamViec == Model.INamLamViec);

                var chungtu = _qtcnBHXHService.FindByCondition(predicate).FirstOrDefault();
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
            int soChungTuIndex = _qtcnBHXHService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
            Model.SSoChungTu = "QTC-" + soChungTuIndex.ToString("D3");
            //OnPropertyChanged(nameof(Division));
        }
    }
}
