using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKhac;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV
{
    public class LapKeHoachChiKCBQYDVDialogViewModel : DialogViewModelBase<BhKhcKcbModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IBhKhcKcbService _bhKcbService;
        private readonly IBhKhcKcbChiTietService _bhKhcKcbChiTietService;
        private ICollectionView _nsDonViModelsView;
        private SessionInfo _sessionInfo;
        #endregion

        #region Property
        public bool IsDetail { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? INamChungTu { get; set; }
        public string SMoTa { get; set; }
        public string SSoQuyetDinh { get; set; }
        public bool IsSummary { get; set; }
        public string HeaderUocThucHienNam => "Tổng uớc thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderKehoachThucHienNam => "Tổng kế hoạch thực hiện năm " + (_sessionService.Current.YearOfWork);
        //public override string Name => Model.IID_BH_KHC_KCB.IsNullOrEmpty() ? "THÊM MỚI CHỨNG TỪ" : "CẬP NHẬT CHỨNG TỪ"
        //public override string Description => Model.IID_BH_KHC_KCB.IsNullOrEmpty() ? "Tạo mới chứng từ kế hoạch chi khám chữ bệnh quân y đơn vị" : "Cập nhật chứng từ kế hoạch chi khám chữ bệnh quân y đơn vị"
        public override Type ContentType => typeof(LapKeHoachChiKCBQYDVDialog);
        private bool _isAgregate;
        public bool IsAgregate
        {
            get => _isAgregate;
            set => SetProperty(ref _isAgregate, value);
        }
        public bool IsEditable => Model.Id.IsNullOrEmpty();

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

        public List<string> ListIdDonViHasCt { get; set; }

        private bool NsDonViFilter(object obj)
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

        public bool IsEdit => BhKhcKCBModel.IID_BH_KHC_KCB == Guid.Empty && !IsSummary;

        public List<BhKhcKcbModel> ListIdsBhKhcKcbModel { get; set; }

        private BhKhcKcbModel _bhKhcKCBModel;
        public BhKhcKcbModel BhKhcKCBModel
        {
            get => _bhKhcKCBModel;
            set
            {
                SetProperty(ref _bhKhcKCBModel, value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEdit));
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
        public string SelectedCountNsDonVi
        {
            get
            {
                var totalCount = NsDonViModelItems != null ? NsDonViModelItems.Count() : 0;
                var totalSelected = NsDonViModelItems != null ? NsDonViModelItems.Count(item => item.Selected) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }
        #endregion

        #region Constructor
        public LapKeHoachChiKCBQYDVDialogViewModel(
               IMapper mapper,
               ILog logger,
               ISessionService sessionService,
               INsDonViService nsDonViService,
               IBhKhcKcbService bhKhcKcbService,
               IBhKhcKcbChiTietService bhKhcKcbChiTietService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _sessionService = sessionService;
            _bhKcbService = bhKhcKcbService;
            _bhKhcKcbChiTietService = bhKhcKcbChiTietService;
        }
        #endregion

        #region Init

        public override void Init()
        {
            try
            {
                _sessionInfo = _sessionService.Current;
                LoadBhDonVis();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Load data
        private void LoadBhDonVis()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = KhcStatusType.ACTIVE;
            var lstChungTu = _bhKcbService.FindIndex().Where(x => x.INamLamViec == yearOfWork);
            ListIdDonViHasCt = lstChungTu.Select(item => item.IID_MaDonVi).ToList();
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.NOI_BO);

            //bool isDvCap4 = _nsDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            //if (isDvCap4)
            //{
            //    predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.ROOT);
            //    if (Model.Id == Guid.Empty)
            //    {
            //        predicate = predicate.And(x => ListIdDonViHasCt.All(y => y == x.IIDMaDonVi));
            //    }
            //    else
            //    {
            //        var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IID_MaDonVi).ToList();
            //        predicate = predicate.And(x => idDonVisExclude.All(y => y != x.IIDMaDonVi));
            //    }
            //}
            //else
            //{
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT));
            if (Model.IID_BH_KHC_KCB == Guid.Empty)
            {
                predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
            }
            else
            {
                var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IID_MaDonVi).ToList();
                predicate = predicate.And(x => idDonVisExclude.All(y => y != x.IIDMaDonVi));
            }
            //}

            var listUnit = _nsDonViService.FindByCondition(predicate).Where(x => x.NamLamViec == yearOfWork).ToList();
            var listDonViByUser = _nsDonViService.FindByUserCreateVoucher(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Format("{0},{1}", LoaiDonVi.NOI_BO, LoaiDonVi.ROOT)).Select(x => x.IIDMaDonVi);
            NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit.Where(x => listDonViByUser.Contains(x.IIDMaDonVi)));
            if (!string.IsNullOrEmpty(Model.IID_MaDonVi))
            {
                NsDonViModelItems.Where(x => x.IIDMaDonVi == Model.IID_MaDonVi).Select(x =>
                {
                    x.Selected = true;
                    return x;
                }).ToList();
            }
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

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            try
            {
                SearchNsDonVi = string.Empty;
                IconKind = Model.IID_BH_KHC_KCB.IsNullOrEmpty() ? PackIconKind.PlaylistPlus : PackIconKind.NoteEditOutline;
                if (IsAgregate)
                {
                    if (Model.IID_BH_KHC_KCB.IsNullOrEmpty())
                    {
                        var soChungTuIndex = _bhKcbService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);

                        SSoChungTu = "KHC-" + soChungTuIndex.ToString("D3");
                        INamChungTu = _sessionService.Current.YearOfWork;
                        DNgayChungTu = DateTime.Now;
                        SMoTa = string.Empty;
                        Model.FTyLeThu = BHXHConstants.KHC_KCB_TYLETHU_DEFAULT;
                    }
                    else
                    {
                        BhKhcKcb entity = _bhKcbService.FindById(Model.IID_BH_KHC_KCB);
                        Model = _mapper.Map<BhKhcKcbModel>(entity);

                        DonViModel donViSelected = NsDonViModelItems.Where(n => n.IIDMaDonVi == Model.IID_MaDonVi).FirstOrDefault();
                        if (donViSelected != null)
                        {
                            donViSelected.Selected = true;
                        }
                        SSoChungTu = Model.SSoChungTu;
                        DNgayChungTu = Model.DNgayChungTu;
                        DNgayQuyetDinh = Model.DNgayQuyetDinh;
                        INamChungTu = Model.INamLamViec;
                        SSoQuyetDinh = Model.SSoQuyetDinh;
                        SMoTa = Model.SMoTa;
                        OnPropertyChanged(nameof(NsDonViModelItems));
                    }
                }
                else
                {
                    if (Model.IID_BH_KHC_KCB.IsNullOrEmpty())
                    {
                        var soChungTuIndex = _bhKcbService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                        SSoChungTu = "KHC-" + soChungTuIndex.ToString("D3");
                        DNgayQuyetDinh = DateTime.Now;
                        DNgayChungTu = DateTime.Now;
                        INamChungTu = _sessionInfo.YearOfWork;
                        SMoTa = string.Empty;
                        Model.FTyLeThu = BHXHConstants.KHC_KCB_TYLETHU_DEFAULT;
                    }
                    else
                    {
                        BhKhcKcb entity = _bhKcbService.FindById(Model.IID_BH_KHC_KCB);
                        Model = _mapper.Map<BhKhcKcbModel>(entity);
                        DonViModel donViSelected = NsDonViModelItems.Where(n => n.IIDMaDonVi == Model.IID_MaDonVi).FirstOrDefault();
                        if (donViSelected != null)
                        {
                            donViSelected.Selected = true;
                        }
                        SSoChungTu = Model.SSoChungTu;
                        DNgayChungTu = Model.DNgayChungTu;
                        DNgayQuyetDinh = Model.DNgayQuyetDinh;
                        INamChungTu = Model.INamLamViec;
                        SSoQuyetDinh = Model.SSoQuyetDinh;
                        SMoTa = Model.SMoTa;
                        OnPropertyChanged(nameof(NsDonViModelItems));
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region On Save

        public override void OnSave()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    // Main process
                    BhKhcKcb entity;
                    if (IsAgregate)
                    {
                        var currentDV = GetDonViOfCurrentUser();
                        var namLamViec = _sessionService.Current.YearOfWork;
                        var loaiChungTu = ListIdsBhKhcKcbModel.Count > 0 ? ListIdsBhKhcKcbModel[0].ILoaiTongHop : -1;
                        if (Model.Id.IsNullOrEmpty())
                        {
                            var listSoChungTuTongHopString = string.Join(",", ListIdsBhKhcKcbModel.Select(x => x.SSoChungTu).ToList());
                            var predicateSummary = PredicateBuilder.True<BhKhcKcb>();
                            predicateSummary = predicateSummary.And(x => x.INamLamViec == namLamViec && x.ILoaiTongHop == loaiChungTu && x.IID_MaDonVi == currentDV.IIDMaDonVi);
                            var lstChungTu = _bhKcbService.FindByCondition(predicateSummary);
                            if (lstChungTu.Any())
                            {
                                var firstChungTuSummary = lstChungTu.FirstOrDefault();
                                if (!firstChungTuSummary.BIsKhoa)
                                {
                                    MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                                    if (messageBoxResult == MessageBoxResult.No)
                                    {
                                        return;
                                    }

                                    var idChungTuSummary = firstChungTuSummary.Id;
                                    var ChungTuSummary = _bhKcbService.FindById(idChungTuSummary);
                                    _bhKcbService.Delete(idChungTuSummary);
                                    var predicateSummaryDetail = PredicateBuilder.True<BhKhcKcbChiTiet>();
                                    predicateSummaryDetail = predicateSummaryDetail.And(x => x.IID_KHC_KCB == idChungTuSummary);
                                    var chungTuChiTiet = _bhKhcKcbChiTietService.FindByCondition(predicateSummaryDetail);
                                    _bhKhcKcbChiTietService.RemoveRange(chungTuChiTiet);
                                }
                                else
                                {
                                    MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                                    return;
                                }
                            }

                            string idDonVi = currentDV.IIDMaDonVi;
                            entity = _mapper.Map<BhKhcKcb>(Model);
                            entity.Id = Guid.NewGuid();
                            entity.SSoChungTu = SSoChungTu;
                            entity.DNgayChungTu = DNgayChungTu;
                            entity.INamLamViec = INamChungTu;
                            entity.IIdDonViId = currentDV.Id;
                            entity.IID_MaDonVi = currentDV.IIDMaDonVi;
                            entity.IID_TongHopID = Guid.NewGuid();
                            entity.BIsKhoa = false;
                            entity.BDaTongHop = false;
                            entity.ILoaiTongHop = KhcBhxhLoaiChungTu.BhxhChungTuTongHop;
                            entity.STongHop = listSoChungTuTongHopString;
                            entity.DNgayTao = DateTime.Now;
                            entity.SNguoiTao = _sessionService.Current.Principal;
                            entity.SMoTa = SMoTa?.Trim() ?? string.Empty;
                            _bhKcbService.Add(entity);
                            CreateKhcKinhphiQuanlyVoucherDetail(_mapper.Map<BhKhcKcbModel>(entity));

                            var listCtChiTiet =
                           _bhKhcKcbChiTietService.FindByCondition(item => item.IID_KHC_KCB.Equals(entity.Id)).ToList();
                            if (listCtChiTiet.Count > 0)
                            {
                                entity.FTongTienDaThucHienNamTruoc = listCtChiTiet.Sum(item => item.FTienDaThucHienNamTruoc);
                                entity.FTongTienUocThucHienNamTruoc = listCtChiTiet.Sum(item => item.FTienUocThucHienNamTruoc);
                                entity.FTongTienKeHoachThucHienNamNay = listCtChiTiet.Sum(item => item.FTienKeHoachThucHienNamNay);
                                _bhKcbService.Update(entity);
                            }

                        }
                        else
                        {
                            entity = new BhKhcKcb();
                            entity = _bhKcbService.FindById(Model.Id);
                            _mapper.Map(Model, entity);
                            entity.SSoChungTu = SSoChungTu;
                            entity.DNgayChungTu = DNgayChungTu;
                            entity.INamLamViec = INamChungTu;
                            entity.IIdDonViId = currentDV.Id;
                            entity.IID_MaDonVi = currentDV.IIDMaDonVi;
                            entity.SMoTa = SMoTa?.Trim() ?? string.Empty;
                            _bhKcbService.Update(entity);
                        }
                    }
                    else
                    {
                        var donViSelected = NsDonViModelItems.FirstOrDefault(n => n.Selected);
                        if (donViSelected == null)
                        {
                            MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                            return;
                        }


                        if (!Validate()) return;

                        if (Model.Id.IsNullOrEmpty())
                        {
                            // Thêm mới
                            entity = _mapper.Map<BhKhcKcb>(Model);
                            entity = new BhKhcKcb();
                            _mapper.Map(Model, entity);
                            entity.Id = Guid.NewGuid();
                            entity.IID_MaDonVi = donViSelected?.IIDMaDonVi;
                            entity.IIdDonViId = donViSelected?.Id;

                            if (donViSelected.Loai == LoaiDonVi.ROOT)
                            {
                                entity.ILoaiTongHop = KhcBhxhLoaiChungTu.BhxhChungTuTongHop;
                                entity.IID_TongHopID = Guid.NewGuid();
                            }
                            else
                            {
                                entity.ILoaiTongHop = KhcBhxhLoaiChungTu.BhxhChungTu;
                            }

                            entity.SSoChungTu = SSoChungTu;
                            entity.DNgayChungTu = DNgayChungTu;
                            entity.DNgayQuyetDinh = DNgayQuyetDinh;
                            entity.INamLamViec = INamChungTu;
                            entity.SSoQuyetDinh = SSoQuyetDinh;
                            entity.SMoTa = SMoTa?.Trim() ?? string.Empty;
                            entity.BIsKhoa = false;
                            entity.BDaTongHop = false;
                            entity.SNguoiTao = _sessionService.Current.Principal;
                            entity.DNgayTao = DateTime.Now;
                            _bhKcbService.Add(entity);
                        }
                        else
                        {
                            // Cập nhật
                            entity = new BhKhcKcb();
                            entity = _bhKcbService.FindById(Model.Id);
                            _mapper.Map(Model, entity);
                            entity.IID_MaDonVi = donViSelected?.IIDMaDonVi;
                            entity.IIdDonViId = donViSelected?.Id;
                            entity.SSoChungTu = SSoChungTu;
                            entity.DNgayChungTu = DNgayChungTu;
                            entity.DNgayQuyetDinh = DNgayQuyetDinh;
                            entity.INamLamViec = INamChungTu;
                            entity.SSoQuyetDinh = SSoQuyetDinh;
                            entity.DNgaySua = DateTime.Now;
                            entity.SMoTa = SMoTa?.Trim() ?? string.Empty;
                            entity.SNguoiSua = _sessionService.Current.Principal;

                            _bhKcbService.Update(entity);
                        }
                    }

                    e.Result = entity;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        // Reload data
                        SSoQuyetDinh = string.Empty;
                        SMoTa = string.Empty;
                        Model = _mapper.Map<BhKhcKcbModel>(e.Result);
                        SavedAction?.Invoke(Model);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateKhcKinhphiQuanlyVoucherDetail(BhKhcKcbModel bhKhcKcbModel)
        {
            KhcKcbChiTietCriteria criteria = new KhcKcbChiTietCriteria()
            {
                ListIdChungTuTongHop = string.Join(",", ListIdsBhKhcKcbModel.Select(x => x.IID_BH_KHC_KCB.ToString()).ToList()),
                IdChungTu = bhKhcKcbModel.Id.ToString(),
                IdDonVi = bhKhcKcbModel.IID_MaDonVi,
                TenDonVi = bhKhcKcbModel.STenDonVi,
                LoaiChungTu = bhKhcKcbModel.ILoaiTongHop.GetValueOrDefault(2),
                NamLamViec = bhKhcKcbModel.INamLamViec.GetValueOrDefault(_sessionService.Current.YearOfWork),
                NguoiTao = bhKhcKcbModel.SNguoiTao,
            };
            _bhKhcKcbChiTietService.AddAggregate(criteria);
        }

        private DonVi GetDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var currentIdDonVi = _sessionService.Current.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private bool Validate()
        {
            StringBuilder messageBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(INamChungTu.ToString()))
            {
                messageBuilder.AppendFormat(Resources.AlertNamChungTuEmpty);
            }

            if (string.IsNullOrEmpty(SSoChungTu))
            {
                messageBuilder.AppendFormat(Resources.AlertSoChungTuEmpty);
            }
            if (!DNgayChungTu.HasValue)
            {
                messageBuilder.AppendFormat(Resources.AlertNgayChungTuEmpty);
            }

            if (!IsSummary)
            {
                if (!DNgayQuyetDinh.HasValue)
                {
                    messageBuilder.AppendFormat(Resources.AlertNgayQuyetDinhEmpty);
                }
            }


            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                return false;
            }

            return true;
        }
        #endregion

        #region close
        public override void OnClose(object obj)
        {
            try
            {
                base.OnClose(obj);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
