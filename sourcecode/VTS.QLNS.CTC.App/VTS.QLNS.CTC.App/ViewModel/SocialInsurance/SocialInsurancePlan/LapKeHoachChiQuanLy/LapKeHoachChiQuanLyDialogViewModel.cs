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
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChi;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy
{
    public class LapKeHoachChiQuanLyDialogViewModel : DialogAttachmentViewModelBase<BhKhcKinhphiQuanlyModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IBhKhcKinhphiQuanlyService _bhKhcKinhphiQuanlyService;
        private readonly IBhKhcKinhphiQuanlyChiTietService _bhKhcKinhphiQuanlyChiTietService;
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
        //public override string Name => Model.IID_BH_KHC_KinhPhiQuanLy.IsNullOrEmpty() ? "THÊM MỚI KẾ HOẠCH CHI" : "CẬP NHẬT KẾ HOẠCH CHI"
        //public override string Description => Model.IID_BH_KHC_KinhPhiQuanLy.IsNullOrEmpty() ? "Tạo mới kế hoạch chi kinh phí quản lý" : "Cập nhật kế hoạch chi kinh phí quản lý"
        public override Type ContentType => typeof(LapKeHoachChiDialog);
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

        public bool IsEdit => BhKhcKinhphiQuanlyModel.IID_BH_KHC_KinhPhiQuanLy == Guid.Empty && !IsSummary;

        public List<BhKhcKinhphiQuanlyModel> ListIdsBhKhcCheDoBhXhModel { get; set; }

        private BhKhcKinhphiQuanlyModel _bhKhcKinhphiQuanlyModel;
        public BhKhcKinhphiQuanlyModel BhKhcKinhphiQuanlyModel
        {
            get => _bhKhcKinhphiQuanlyModel;
            set
            {
                SetProperty(ref _bhKhcKinhphiQuanlyModel, value);
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
        public LapKeHoachChiQuanLyDialogViewModel(IMapper mapper,
           ILog logger,
           ISessionService sessionService,
           INsDonViService nsDonViService,
           IBhKhcKinhphiQuanlyService bhKhcKinhphiQuanlyService,
           IBhKhcKinhphiQuanlyChiTietService bhKhcKinhphiQuanlyChiTietService,

           IStorageServiceFactory storageServiceFactory,
           IAttachmentService attachService,
           LapKeHoachChiDetailViewModel lapKeHoachChiDetailViewModel)
           : base(mapper, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _bhKhcKinhphiQuanlyService = bhKhcKinhphiQuanlyService;
            _bhKhcKinhphiQuanlyChiTietService = bhKhcKinhphiQuanlyChiTietService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            DownloadFileCommand = new RelayCommand(obj => OnDownloadFile(), obj => SelectedAttachment != null);
            DownloadAllFileCommand = new RelayCommand(obj => OnDownloadAllFile(), obj => ItemsAttachment != null && ItemsAttachment.Count > 0);
            DeleteFileCommand = new RelayCommand(obj => OnDeleteFile(), obj => SelectedAttachment != null);
            //LapKeHoachChiDetailViewModel = lapKeHoachChiDetailViewModel;
        }
        #endregion

        #region Init
        public override void Init()
        {
            try
            {
                LoadDefault();
                LoadAttach();
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
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = KhcStatusType.ACTIVE;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var lstChungTu = _bhKhcKinhphiQuanlyService.FindIndex().Where(x => x.INamLamViec == yearOfWork);
            ListIdDonViHasCt = lstChungTu.Select(item => item.IID_MaDonVi).ToList();
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.NOI_BO);

            //bool isDvCap4 = _nsDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            //if (isDvCap4)
            //{
            //    predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == LoaiDonVi.ROOT);
            //    if (Model.IID_BH_KHC_KinhPhiQuanLy == Guid.Empty)
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
            if (Model.IID_BH_KHC_KinhPhiQuanLy == Guid.Empty)
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

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            try
            {
                int iNamChungTu = _sessionService.Current.YearOfWork;
                SearchNsDonVi = string.Empty;
                IconKind = Model.IID_BH_KHC_KinhPhiQuanLy.IsNullOrEmpty() ? PackIconKind.PlaylistPlus : PackIconKind.NoteEditOutline;
                if (IsAgregate)
                {
                    if (Model.IID_BH_KHC_KinhPhiQuanLy.IsNullOrEmpty())
                    {

                        var soChungTuIndex = _bhKhcKinhphiQuanlyService.GetSoChungTuIndexByCondition(iNamChungTu);
                        SSoChungTu = "KHC-" + soChungTuIndex.ToString("D3");
                        INamChungTu = _sessionService.Current.YearOfWork;
                        DNgayChungTu = DateTime.Now;
                        SMoTa = string.Empty;
                    }
                    else
                    {
                        BhKhcKinhphiQuanly entity = _bhKhcKinhphiQuanlyService.FindById(Model.IID_BH_KHC_KinhPhiQuanLy);
                        Model = _mapper.Map<BhKhcKinhphiQuanlyModel>(entity);

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
                    if (Model.IID_BH_KHC_KinhPhiQuanLy.IsNullOrEmpty())
                    {
                        var soChungTuIndex = _bhKhcKinhphiQuanlyService.GetSoChungTuIndexByCondition(iNamChungTu);
                        SSoChungTu = "KHC-" + soChungTuIndex.ToString("D3");
                        DNgayQuyetDinh = DateTime.Now;
                        DNgayChungTu = DateTime.Now;
                        INamChungTu = _sessionService.Current.YearOfWork;
                        SMoTa = string.Empty;
                    }
                    else
                    {
                        BhKhcKinhphiQuanly entity = _bhKhcKinhphiQuanlyService.FindById(Model.IID_BH_KHC_KinhPhiQuanLy);
                        Model = _mapper.Map<BhKhcKinhphiQuanlyModel>(entity);

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
                    BhKhcKinhphiQuanly entity;
                    if (IsAgregate)
                    {
                        var currentDV = GetNsDonViOfCurrentUser();
                        var namLamViec = _sessionService.Current.YearOfWork;

                        if (Model.Id.IsNullOrEmpty())
                        {
                            var listSoChungTuTongHopString = string.Join(",", ListIdsBhKhcCheDoBhXhModel.Select(x => x.SSoChungTu).ToList());
                            var predicateSummary = PredicateBuilder.True<BhKhcKinhphiQuanly>();
                            predicateSummary = predicateSummary.And(x => x.INamLamViec == namLamViec && x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop && x.IID_MaDonVi == currentDV.IIDMaDonVi);
                            var lstChungTuSummary = _bhKhcKinhphiQuanlyService.FindByCondition(predicateSummary);
                            if (lstChungTuSummary.Any())
                            {
                                var firstChungTuSummary = lstChungTuSummary.FirstOrDefault();
                                if (!firstChungTuSummary.BIsKhoa)
                                {
                                    MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(string.Format(Resources.ConfirmThayTheChungTuTongHop, _sessionService.Current.YearOfWork));
                                    if (messageBoxResult == MessageBoxResult.No)
                                    {
                                        return;
                                    }

                                    var idChungTuSummary = firstChungTuSummary.Id;
                                    var khcChungTuSummary = _bhKhcKinhphiQuanlyService.FindById(idChungTuSummary);
                                    _bhKhcKinhphiQuanlyService.Delete(idChungTuSummary);
                                    var predicateSummaryDetail = PredicateBuilder.True<BhKhcKinhphiQuanlyChiTiet>();
                                    predicateSummaryDetail = predicateSummaryDetail.And(x => x.IID_KHC_KinhPhiQuanLy == idChungTuSummary);
                                    var khcChungTuChiTiet = _bhKhcKinhphiQuanlyChiTietService.FindByCondition(predicateSummaryDetail);
                                    _bhKhcKinhphiQuanlyChiTietService.RemoveRange(khcChungTuChiTiet);
                                }
                                else
                                {
                                    MessageBoxHelper.Warning(Resources.MsgCheckLockVoucherSummary);
                                    return;
                                }
                            }

                            string idDonVi = currentDV.IIDMaDonVi;
                            entity = _mapper.Map<BhKhcKinhphiQuanly>(Model);
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
                            _bhKhcKinhphiQuanlyService.Add(entity);
                            CreateKhcKinhphiQuanlyVoucherDetail(_mapper.Map<BhKhcKinhphiQuanlyModel>(entity));

                            var listCtChiTiet =
                            _bhKhcKinhphiQuanlyChiTietService.FindByCondition(
                            item => item.IID_KHC_KinhPhiQuanLy.Equals(entity.Id)).ToList();
                            if (listCtChiTiet.Count > 0)
                            {
                                entity.FTongTienDaThucHienNamTruoc = listCtChiTiet.Sum(item => item.FTienDaThucHienNamTruoc);
                                entity.FTongTienUocThucHienNamTruoc = listCtChiTiet.Sum(item => item.FTienUocThucHienNamTruoc);
                                entity.FTongTienKeHoachThucHienNamNay = listCtChiTiet.Sum(item => item.FTienKeHoachThucHienNamNay);
                                entity.FTongTienQuanLuc = listCtChiTiet.Sum(item => item.FTienQuanLuc);
                                entity.FTongTienQuanY = listCtChiTiet.Sum(item => item.FTienQuanY);
                                entity.FTongTienTaiChinh = listCtChiTiet.Sum(item => item.FTienTaiChinh);
                                entity.FTongTienCanBo = listCtChiTiet.Sum(item => item.FTienCanBo);
                            }
                            _bhKhcKinhphiQuanlyService.Update(entity);
                        }
                        else
                        {
                            entity = new BhKhcKinhphiQuanly();
                            entity = _bhKhcKinhphiQuanlyService.FindById(Model.Id);
                            _mapper.Map(Model, entity);
                            entity.SSoChungTu = SSoChungTu;
                            entity.DNgayChungTu = DNgayChungTu;
                            entity.INamLamViec = INamChungTu;
                            entity.IIdDonViId = currentDV.Id;
                            entity.IID_MaDonVi = currentDV.IIDMaDonVi;
                            entity.SMoTa = SMoTa?.Trim() ?? string.Empty;
                            _bhKhcKinhphiQuanlyService.Update(entity);
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
                            entity = _mapper.Map<BhKhcKinhphiQuanly>(Model);
                            entity = new BhKhcKinhphiQuanly();
                            _mapper.Map(Model, entity);
                            entity.Id = Guid.NewGuid();
                            entity.IID_MaDonVi = donViSelected?.IIDMaDonVi;
                            entity.IIdDonViId = donViSelected?.Id;
                            entity.BDaTongHop = false;
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
                            entity.SNguoiTao = _sessionInfo.Principal;
                            entity.DNgayTao = DateTime.Now;
                            _bhKhcKinhphiQuanlyService.Add(entity);
                        }
                        else
                        {

                            // Cập nhật
                            entity = new BhKhcKinhphiQuanly();
                            entity = _bhKhcKinhphiQuanlyService.FindById(Model.Id);
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

                            _bhKhcKinhphiQuanlyService.Update(entity);
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
                        Model = _mapper.Map<BhKhcKinhphiQuanlyModel>(e.Result);
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

        private void CreateKhcKinhphiQuanlyVoucherDetail(BhKhcKinhphiQuanlyModel bhKhcKinhphiQuanlyModel)
        {
            KhcQuanlyKinhphiChiTietCriteria criteria = new KhcQuanlyKinhphiChiTietCriteria()
            {
                ListIdChungTuTongHop = string.Join(",", ListIdsBhKhcCheDoBhXhModel.Select(x => x.IID_BH_KHC_KinhPhiQuanLy.ToString()).ToList()),
                IdChungTu = bhKhcKinhphiQuanlyModel.Id.ToString(),
                IdDonVi = bhKhcKinhphiQuanlyModel.IID_MaDonVi,
                TenDonVi = bhKhcKinhphiQuanlyModel.STenDonVi,
                LoaiChungTu = bhKhcKinhphiQuanlyModel.ILoaiTongHop.GetValueOrDefault(2),
                NamLamViec = bhKhcKinhphiQuanlyModel.INamLamViec.GetValueOrDefault(_sessionService.Current.YearOfWork),
                NguoiTao = bhKhcKinhphiQuanlyModel.SNguoiTao,
            };
            _bhKhcKinhphiQuanlyChiTietService.AddAggregate(criteria);
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
