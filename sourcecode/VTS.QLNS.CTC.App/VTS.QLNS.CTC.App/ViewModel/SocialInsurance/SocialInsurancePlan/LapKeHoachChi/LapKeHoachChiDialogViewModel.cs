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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Demand;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChi
{
    public class LapKeHoachChiDialogViewModel : DialogViewModelBase<BhKhcCheDoBhXhModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IBhKhcCheDoBhXhChiTietService _bhKhcCheDoBhXhChiTietService;
        private readonly IBhKhcCheDoBhXhService _bhBhKhcCheDoBhxhService;
        private ICollectionView _nsDonViModelsView;
        private readonly IBhKhcCheDoBhXhService _service;
        #endregion

        #region Property
        private SessionInfo _sessionInfo;
        public bool IsDetail { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? INamChungTu { get; set; }
        public string SMoTa { get; set; }
        public string SSoQuyetDinh { get; set; }
        public bool IsSummary { get; set; }
        public override Type ContentType => typeof(LapKeHoachChiDialog);
        public string HeaderSoDaThucHienNam => "Số đã thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderUocThucHienNam => "Ước thực hiện năm " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderKehoachThucHienNam => "KH thực hiện năm " + (_sessionService.Current.YearOfWork);
        public LapKeHoachChiDetailViewModel LapKeHoachChiDetailViewModel { get; set; }
        private bool _isAgregate;
        public bool IsAgregate
        {
            get => _isAgregate;
            set => SetProperty(ref _isAgregate, value);
        }
        public bool IsEditable => Model.IID_BH_KHC_CheDoBHXH.IsNullOrEmpty();

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

        public bool IsEdit => BhKhcCheDoBhXhModel.IID_BH_KHC_CheDoBHXH.IsNullOrEmpty() && !IsSummary;

        public List<BhKhcCheDoBhXhModel> ListIdsBhKhcCheDoBhXhModel { get; set; }

        private BhKhcCheDoBhXhModel _bhKhcCheDoBhXhModel;
        public BhKhcCheDoBhXhModel BhKhcCheDoBhXhModel
        {
            get => _bhKhcCheDoBhXhModel;
            set
            {
                SetProperty(ref _bhKhcCheDoBhXhModel, value);
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
        public LapKeHoachChiDialogViewModel(IMapper mapper,
            ILog logger,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IBhKhcCheDoBhXhService ibhKhKeHoachChiService,
            IBhKhcCheDoBhXhChiTietService bhKhcCheDoBhXhChiTietService,
            IBhKhcCheDoBhXhService bhKhcCheDoBhXhService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            LapKeHoachChiDetailViewModel lapKeHoachChiDetailViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _service = ibhKhKeHoachChiService;
            _bhKhcCheDoBhXhChiTietService = bhKhcCheDoBhXhChiTietService;
            _bhBhKhcCheDoBhxhService = bhKhcCheDoBhXhService;

            LapKeHoachChiDetailViewModel = lapKeHoachChiDetailViewModel;
        }
        #endregion

        #region Init
        public override void Init()
        {
            try
            {
                LoadDefault();
                LoadBhDonVis();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region
        private void LoadBhDonVis()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = StatusType.ACTIVE;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var lstChungTu = _bhBhKhcCheDoBhxhService.FindIndex().Where(x => x.INamLamViec == yearOfWork);
            ListIdDonViHasCt = lstChungTu.Select(item => item.IID_MaDonVi).ToList();
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.NOI_BO);

            bool isDvCap4 = _nsDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            //if (isDvCap4)
            //{
            //    predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.ROOT);
            //    if (Model.IID_BH_KHC_CheDoBHXH == Guid.Empty)
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
                if (Model.IID_BH_KHC_CheDoBHXH == Guid.Empty)
                {
                    predicate = predicate.And(x => ListIdDonViHasCt.All(y => y != x.IIDMaDonVi));
                }
                else
                {
                    var idDonVisExclude = ListIdDonViHasCt.Where(item => item != Model.IID_MaDonVi).ToList();
                    predicate = predicate.And(x => idDonVisExclude.All(y => y != x.IIDMaDonVi));
                }
            //}

            var listUnit = _nsDonViService.FindByCondition(predicate).ToList();
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
        #endregion

        #region Load data
        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                int iNamlamViec = _sessionService.Current.YearOfWork;
                SearchNsDonVi = string.Empty;
                IconKind = Model.IID_BH_KHC_CheDoBHXH.IsNullOrEmpty() ? PackIconKind.PlaylistPlus : PackIconKind.NoteEditOutline;
                if (IsAgregate)
                {
                    if (Model.IID_BH_KHC_CheDoBHXH.IsNullOrEmpty())
                    {
                        var soChungTuIndex = _service.GetSoChungTuIndexByCondition(iNamlamViec);
                        SSoChungTu = "KHC-" + soChungTuIndex.ToString("D3");
                        INamChungTu = _sessionService.Current.YearOfWork;
                        DNgayChungTu = DateTime.Now;
                        SMoTa = string.Empty;
                    }
                    else
                    {
                        BhKhcCheDoBhXh entity = _service.FindById(Model.IID_BH_KHC_CheDoBHXH);
                        Model = _mapper.Map<BhKhcCheDoBhXhModel>(entity);


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
                    if (Model.IID_BH_KHC_CheDoBHXH.IsNullOrEmpty())
                    {
                        var soChungTuIndex = _service.GetSoChungTuIndexByCondition(iNamlamViec);
                        SSoChungTu = "KHC-" + soChungTuIndex.ToString("D3");
                        DNgayQuyetDinh = DateTime.Now;
                        DNgayChungTu = DateTime.Now;
                        INamChungTu = _sessionInfo.YearOfWork;
                        SMoTa = string.Empty;
                    }
                    else
                    {
                        BhKhcCheDoBhXh entity = _service.FindById(Model.IID_BH_KHC_CheDoBHXH);
                        Model = _mapper.Map<BhKhcCheDoBhXhModel>(entity);
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

        #region On save
        public override void OnSave()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    // Main process
                    BhKhcCheDoBhXh entity;
                    if (IsAgregate)
                    {
                        var currentDV = GetNsDonViOfCurrentUser();
                        var namLamViec = _sessionService.Current.YearOfWork;

                        if (Model.Id.IsNullOrEmpty())
                        {
                            var listSoChungTuTongHopString = string.Join(",", ListIdsBhKhcCheDoBhXhModel.Select(x => x.SSoChungTu).ToList());
                            var predicateSummary = PredicateBuilder.True<BhKhcCheDoBhXh>();
                            predicateSummary = predicateSummary.And(x => x.INamLamViec == namLamViec && x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop && x.IID_MaDonVi == currentDV.IIDMaDonVi);
                            var lstKhcChungTuSummary = _bhBhKhcCheDoBhxhService.FindByCondition(predicateSummary);
                            if (lstKhcChungTuSummary.Any())
                            {
                                var firstKhcChungTuSummary = lstKhcChungTuSummary.FirstOrDefault();
                                if (!firstKhcChungTuSummary.BIsKhoa)
                                {
                                    MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                                    if (messageBoxResult == MessageBoxResult.No)
                                    {
                                        return;
                                    }

                                    var idKhcChungTuSummary = firstKhcChungTuSummary.Id;
                                    var khcChungTuSummary = _bhBhKhcCheDoBhxhService.FindById(idKhcChungTuSummary);
                                    _bhBhKhcCheDoBhxhService.Delete(idKhcChungTuSummary);
                                    var predicateSummaryDetail = PredicateBuilder.True<BhKhcCheDoBhXhChiTiet>();
                                    predicateSummaryDetail = predicateSummaryDetail.And(x => x.IID_KHC_CheDoBHXH == idKhcChungTuSummary);
                                    var khcChungTuChiTiet = _bhKhcCheDoBhXhChiTietService.FindByCondition(predicateSummaryDetail);
                                    _bhKhcCheDoBhXhChiTietService.RemoveRange(khcChungTuChiTiet);
                                }
                                else
                                {
                                    MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                                    return;
                                }
                            }

                            string idDonVi = currentDV.IIDMaDonVi;
                            entity = _mapper.Map<BhKhcCheDoBhXh>(Model);
                            entity.SSoChungTu = SSoChungTu;
                            entity.DNgayChungTu = DNgayChungTu;
                            entity.INamLamViec = INamChungTu;
                            entity.IIdDonViId = currentDV.Id;
                            entity.IID_MaDonVi = currentDV.IIDMaDonVi;
                            entity.IID_TongHopID = Guid.NewGuid();
                            entity.BDaTongHop = false;
                            entity.BIsKhoa = false;
                            entity.ILoaiTongHop = KhcBhxhLoaiChungTu.BhxhChungTuTongHop;
                            entity.STongHop = listSoChungTuTongHopString;
                            entity.DNgayTao = DateTime.Now;
                            entity.SNguoiTao = _sessionService.Current.Principal;
                            entity.SMoTa = SMoTa?.Trim() ?? string.Empty;
                            _service.Add(entity);
                            CreateKhcCdBhXhVoucherDetail(_mapper.Map<BhKhcCheDoBhXhModel>(entity));
                            var listCtChiTiet =
                            _bhKhcCheDoBhXhChiTietService.FindByCondition(
                            item => item.IID_KHC_CheDoBHXH.Equals(entity.Id)).ToList();

                            if (listCtChiTiet.Count > 0)
                            {
                                entity.ITongSoDaThucHienNamTruoc = listCtChiTiet.Sum(item => item.ISoDaThucHienNamTruoc);
                                entity.FTongTienDaThucHienNamTruoc = listCtChiTiet.Sum(item => item.FTienDaThucHienNamTruoc);

                                entity.ITongSoUocThucHienNamTruoc = listCtChiTiet.Sum(item => item.ISoUocThucHienNamTruoc);
                                entity.FTongTienUocThucHienNamTruoc = listCtChiTiet.Sum(item => item.FTienUocThucHienNamTruoc);

                                entity.ITongSoKeHoachThucHienNamNay = listCtChiTiet.Sum(item => item.ISoKeHoachThucHienNamNay);
                                entity.FTongTienKeHoachThucHienNamNay = listCtChiTiet.Sum(item => item.FTienKeHoachThucHienNamNay);

                                entity.ITongSoSQ = listCtChiTiet.Sum(item => item.ISoSQ);
                                entity.FTongTienSQ = listCtChiTiet.Sum(item => item.FTienSQ);

                                entity.ITongSoQNCN = listCtChiTiet.Sum(item => item.ISoQNCN);
                                entity.FTongTienQNCN = listCtChiTiet.Sum(item => item.FTienQNCN);

                                entity.ITongSoCNVQP = listCtChiTiet.Sum(item => item.ISoCNVQP);
                                entity.FTongTienCNVQP = listCtChiTiet.Sum(item => item.FTienCNVQP);

                                entity.ITongSoLDHD = listCtChiTiet.Sum(item => item.ISoLDHD);
                                entity.FTongTienLDHD = listCtChiTiet.Sum(item => item.FTienLDHD);

                                entity.ITongSoHSQBS = listCtChiTiet.Sum(item => item.ISoHSQBS);
                                entity.FTongTienHSQBS = listCtChiTiet.Sum(item => item.FTienHSQBS);

                            }

                            _bhBhKhcCheDoBhxhService.Update(entity);
                        }
                        else
                        {
                            entity = new BhKhcCheDoBhXh();
                            entity = _service.FindById(Model.Id);
                            _mapper.Map(Model, entity);
                            entity.SSoChungTu = SSoChungTu;
                            entity.DNgayChungTu = DNgayChungTu;
                            entity.INamLamViec = INamChungTu;
                            entity.IIdDonViId = currentDV.Id;
                            entity.IID_MaDonVi = currentDV.IIDMaDonVi;
                            entity.SMoTa = SMoTa?.Trim() ?? string.Empty;
                            _service.Update(entity);
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
                            entity = _mapper.Map<BhKhcCheDoBhXh>(Model);

                            entity = new BhKhcCheDoBhXh();
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
                            entity.BDaTongHop = false;
                            entity.SSoQuyetDinh = SSoQuyetDinh;
                            entity.SMoTa = SMoTa?.Trim() ?? string.Empty;
                            entity.BIsKhoa = false;
                            entity.SNguoiTao = _sessionInfo.Principal;
                            entity.DNgayTao = DateTime.Now;
                            _service.Add(entity);
                        }
                        else
                        {

                            // Cập nhật
                            entity = new BhKhcCheDoBhXh();
                            entity = _service.FindById(Model.Id);
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
                            entity.SNguoiSua = _sessionInfo.Principal;

                            _service.Update(entity);
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
                        Model = _mapper.Map<BhKhcCheDoBhXhModel>(e.Result);
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

        private void CreateKhcCdBhXhVoucherDetail(BhKhcCheDoBhXhModel bhKhcCheDoBhXhModel)
        {
            KhcCheDoBhXhChiTietCriteria creation = new KhcCheDoBhXhChiTietCriteria()
            {
                ListIdChungTuTongHop = string.Join(",", ListIdsBhKhcCheDoBhXhModel.Select(x => x.IID_BH_KHC_CheDoBHXH.ToString()).ToList()),
                IdChungTu = bhKhcCheDoBhXhModel.Id.ToString(),
                IdDonVi = bhKhcCheDoBhXhModel.IID_MaDonVi,
                TenDonVi = bhKhcCheDoBhXhModel.STenDonVi,
                LoaiChungTu = bhKhcCheDoBhXhModel.ILoaiTongHop.GetValueOrDefault(2),
                NamLamViec = bhKhcCheDoBhXhModel.INamLamViec.GetValueOrDefault(_sessionService.Current.YearOfWork),
                NguoiTao = bhKhcCheDoBhXhModel.SNguoiTao,

            };
            _bhKhcCheDoBhXhChiTietService.AddAggregate(creation);
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var currentIdDonVi = _sessionInfo.IdDonVi;
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
