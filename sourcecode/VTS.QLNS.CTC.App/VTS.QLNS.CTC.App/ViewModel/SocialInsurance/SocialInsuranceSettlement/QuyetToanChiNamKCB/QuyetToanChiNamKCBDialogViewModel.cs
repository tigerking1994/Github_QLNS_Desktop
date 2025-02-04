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

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB
{
    public class QuyetToanChiNamKCBDialogViewModel : DialogViewModelBase<BhQtcnKCBModel>
    {

        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDmCoSoYTeService _bhDmCoSoYTeService;
        private readonly IQtcnKCBService _qtcnKCBService;
        private readonly IQtcnKCBChiTietService _qtcnKCBChiTietService;
        private readonly IQtcqKCBService _qtcqKCBService;
        private readonly ISessionService _sessionService;
        private readonly ISysAuditLogService _log;
        private readonly INsDonViService _nSDonViService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private ICollectionView _dataLNSView;
        private readonly ICollectionView _dataCSYTView;
        private ICollectionView _nsDonViModelsView;

        public override Type ContentType => typeof(QuyetToanChiNamBHXHDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI" : "CẬP NHẬT";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới chứng từ quyết toán chi năm KCB Quân y đơn vị" : "Cập nhật chứng từ quyết toán chi năm KCB Quân y đơn vị";
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
                int totalCount = DonViModelItems != null ? DonViModelItems.Count() : 0;
                int totalSelected = DonViModelItems != null ? DonViModelItems.Count(item => item.Selected) : 0;
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
            get => DataLNS != null && DataLNS.Any() && DataLNS.All(item => item.IsSelected);
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

        public QuyetToanChiNamKCBDialogViewModel(
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            IQtcnKCBService qtcnKCBService,
            IQtcnKCBChiTietService qtcnKCBChiTietService,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nSDonViService,
            ISysAuditLogService log,
            IQtcqKCBService qtcqKCBService,
            ILog logger)
        {
            _sessionService = sessionService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;
            _qtcnKCBService = qtcnKCBService;
            _qtcnKCBChiTietService = qtcnKCBChiTietService;
            _qtcqKCBService = qtcqKCBService;
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
            listMLNS = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(yearOfWork, LNSValue.LNS_9010004_9010005).ToList();
            DataLNS = _mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(listMLNS);

            _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
            _dataLNSView.Filter = ListLNSFilter;

            if (_dataLNS != null && _dataLNS.Count > 0)
            {
                foreach (BhDmMucLucNganSachModel model in _dataLNS)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsSelected))
                        {
                            foreach (BhDmMucLucNganSachModel item in _dataLNS)
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
            BhDmMucLucNganSachModel item = (BhDmMucLucNganSachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.LNSDisplay.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadUnits()
        {
            int iTrangThai = KhcStatusType.ACTIVE;
            int yearOfWork = _sessionInfo.YearOfWork;
            IEnumerable<Core.Domain.Query.BhQtcnKCBQuery> lstChungTu = _qtcnKCBService.GetDanhSachQuyetToanNamKCB(yearOfWork);
            ListIdDonViHasCt = lstChungTu.Select(item => item.IIdMaDonVi).ToList();
            System.Linq.Expressions.Expression<Func<DonVi, bool>> predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            predicate = predicate.And(x => x.ITrangThai == iTrangThai);
            System.Linq.Expressions.Expression<Func<DonVi, bool>> predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
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
                    List<string> idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IIdMaDonVi).ToList();
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
                    List<string> idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IIdMaDonVi).ToList();
                    predicate = predicate.And(x => !idDonVisExclude.Any(y => y == x.IIDMaDonVi));
                }
            }

            List<DonVi> listUnit = _nSDonViService.FindByCondition(predicate).Where(x => x.NamLamViec == yearOfWork).ToList();
            IEnumerable<string> listDonViByUser = _nSDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT)).Select(x => x.IIDMaDonVi);
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
            foreach (DonViModel model in DonViModelItems)
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

        private bool DonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchNsDonVi))
            {
                return true;
            }
            DonViModel item = (DonViModel)obj;
            bool condition = item.TenDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower()) ||
                            item.IIDMaDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower());
            return condition;
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

        public List<string> CheckQTQuy(Guid id)
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            System.Linq.Expressions.Expression<Func<BhQtcqKCB, bool>> predicate = PredicateBuilder.True<BhQtcqKCB>();
            predicate = predicate.And(x => x.INamChungTu == yearOfWork && x.IIdDonVi == id);
            predicate = predicate.And(x => x.BIsKhoa);
            List<BhQtcqKCB> listQuyChungTuByDonvi = _qtcqKCBService.FindByCondition(predicate).ToList();

            return listQuyChungTuByDonvi.Select(x => x.IIdMaDonVi).ToList();
        }

        public override void OnSave()
        {
            try
            {
                base.OnSave();
                DateTime dtNow = DateTime.Now;

                DonViModel donViSelected = DonViModelItems.FirstOrDefault(n => n.Selected);

                string message = GetMessageValidate();

                BhQtcnKCB entity;
                List<DonVi> lstDonVi = _nSDonViService.FindAll().Where(x => x.NamLamViec == _sessionInfo.YearOfWork).ToList();
                if (Model.Id == Guid.Empty)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        System.Windows.Forms.MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // Add
                    entity = new BhQtcnKCB();
                    _mapper.Map(Model, entity);
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.DNgaySua = null;

                    entity.IIdDonVi = donViSelected.Id;
                    entity.IIdMaDonVi = donViSelected.IIDMaDonVi;
                    entity.SDSLNS = LNSValue.LNS_9010004_9010005;
                    entity.BDaTongHop = false;
                    entity.BThucChiTheo4Quy = Model.BThucChiTheo4Quy;
                    if (donViSelected != null && donViSelected.Loai == LoaiDonVi.ROOT)
                    {
                        entity.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                    }
                    else
                    {
                        entity.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTu;
                    }

                    entity.SNguoiTao = _sessionService.Current.Principal;
                    _qtcnKCBService.Add(entity);

                    //Tạo chứng từ chi tiết khi chọn lấy dữ liệu số thực chi theo chứng từ quyết toán BHXH 04 quý
                    if (entity.BThucChiTheo4Quy)
                    {
                        _qtcnKCBChiTietService.CreateChungTuChiTietTheoQuy(entity.Id, entity.IIdMaDonVi, entity.INamLamViec.Value, entity.SNguoiTao, entity.BDaTongHop);
                        //update chứng từ
                        BhQtcnKCB chungtu = _qtcnKCBService.FindById(entity.Id);
                        //var listDV = CheckQTQuy(donViSelected.Id);
                        System.Linq.Expressions.Expression<Func<BhQtcnKCBChiTiet, bool>> predicate = PredicateBuilder.True<BhQtcnKCBChiTiet>();
                        predicate = predicate.And(x => x.IIdQTCNamKCBQuanYDonVi == entity.Id);

                        List<BhQtcnKCBChiTiet> chungtu_chitiet = _qtcnKCBChiTietService.FindByCondition(predicate).ToList();
                        List<Core.Domain.Query.BhQtcnKCBChiTietQuery> listDataQuery = _qtcnKCBChiTietService.GetChiTietQuyetToanChiNamKCB(entity.Id, entity.SDSLNS, _sessionInfo.YearOfWork, entity.BThucChiTheo4Quy, entity.IIdMaDonVi, !IsDonViRoot(entity.IIdMaDonVi)).ToList();

                        if (chungtu_chitiet.Any())
                        {
                            chungtu.FTongTienDuToanNamTruocChuyenSang = listDataQuery?.Select(x => x.FDuToanNamTruocChuyenSang).FirstOrDefault().GetValueOrDefault();
                            chungtu.FTongTienDuToanGiaoNamNay = listDataQuery?.Select(x => x.FTienDuToanGiaoNamNay).FirstOrDefault().GetValueOrDefault();
                            chungtu.FTongTienTongDuToanDuocGiao = chungtu.FTongTienDuToanGiaoNamNay.GetValueOrDefault() + chungtu.FTongTienDuToanNamTruocChuyenSang.GetValueOrDefault();
                            chungtu.FTongTienThucChi = chungtu_chitiet?.Select(x => x.FTienThucChi).Sum();
                            chungtu.FTongTienThua = chungtu.FTongTienTongDuToanDuocGiao.GetValueOrDefault() > chungtu.FTongTienThucChi.GetValueOrDefault() ? chungtu.FTongTienTongDuToanDuocGiao.GetValueOrDefault() - chungtu.FTongTienThucChi.GetValueOrDefault() : 0;
                            chungtu.FTongTienThieu = chungtu.FTongTienThucChi.GetValueOrDefault() > chungtu.FTongTienTongDuToanDuocGiao.GetValueOrDefault() ? chungtu.FTongTienThucChi.GetValueOrDefault() - chungtu.FTongTienTongDuToanDuocGiao.GetValueOrDefault() : 0;
                        }

                        _qtcnKCBService.Update(chungtu);
                    }

                }
                else
                {
                    entity = _qtcnKCBService.FindById(Model.Id);
                    entity.DNgayChungTu = Model.DNgayChungTu;
                    entity.SSoQuyetDinh = Model.SSoQuyetDinh;
                    entity.SDSLNS = LNSValue.LNS_9010004_9010005;
                    entity.DNgayQuyetDinh = Model.DNgayQuyetDinh;
                    entity.BThucChiTheo4Quy = Model.BThucChiTheo4Quy;
                    entity.SMoTa = Model.SMoTa;
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    _qtcnKCBService.Update(entity);

                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<BhQtcnKCBModel>(entity));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;


        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (Model.DNgayChungTu == null)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }


            DonViModel donViSelected = DonViModelItems.FirstOrDefault(n => n.Selected);
            if (donViSelected == null)
            {
                messages.Add(Resources.MsgCheckDonVi);
            }
            else
            {
                //Check đã tồn tại đơn vị
                System.Linq.Expressions.Expression<Func<BhQtcnKCB, bool>> predicate = PredicateBuilder.True<BhQtcnKCB>();
                predicate = predicate.And(x => x.IIdDonVi == donViSelected.Id);
                predicate = predicate.And(x => x.IIdMaDonVi == donViSelected.IIDMaDonVi);
                predicate = predicate.And(x => x.INamLamViec == Model.INamLamViec);

                BhQtcnKCB chungtu = _qtcnKCBService.FindByCondition(predicate).FirstOrDefault();
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
            int soChungTuIndex = _qtcnKCBService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
            Model.SSoChungTu = "QTC-" + soChungTuIndex.ToString("D3");
            //OnPropertyChanged(nameof(Division));
        }

    }
}
