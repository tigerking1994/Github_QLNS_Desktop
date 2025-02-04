using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT
{
    public class QuyetToanCapKinhPhiKcbDetailViewModel : DetailViewModelBase<BhQtCapKinhPhiKcbModel, BhQtCapKinhPhiKcbChiTietModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly IQtcCapKinhPhiKcbChiTietService _chungTuChiTietService;
        private readonly IQtcCapKinhPhiKcbService _chungTuService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private ICollectionView ItemsView;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;

        private ICollectionView _chungTuChiTietModelsView { get; set; }
        private ICollectionView _searchPopupView { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (!string.IsNullOrEmpty(_searchText))
                {
                    SearchDataParent();
                }
            }
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set
            {
                SetProperty(ref _isLock, value);
                OnPropertyChanged(nameof(IsEnabledDelete));
            }
        }

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted)
                                || Items.Any(x => !x.IsHangCha);


        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ObservableCollection<ComboboxItem> _viewSummary = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ViewSummary
        {
            get => _viewSummary;
            set => SetProperty(ref _viewSummary, value);
        }

        public string SoQuyetToanDisplay { get; set; }
        public bool IsShowColumnKPKCBBHYT { get; set; }
        public bool IsAnotherUserCreate { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public int NamLamViec { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public DateTime DtNow => DateTime.Now;
        public RelayCommand RefreshCommand { get; }

        public PrintQuyetToanCapKinhPhiKCBBHYTViewModel PrintQuyetToanCapKinhPhiKCBBHYTViewModel { get; }

        public QuyetToanCapKinhPhiKcbDetailViewModel(
            IQtcCapKinhPhiKcbChiTietService iQtcCapKinhPhiKcbChiTietService,
            IQtcCapKinhPhiKcbService iQtcCapKinhPhiKcbService,
            ISessionService sessionService,
            IMapper mapper,
            ISysAuditLogService log,
            INsDonViService nsDonViService,
            ILog logger,
            PrintQuyetToanCapKinhPhiKCBBHYTViewModel printQuyetToanCapKinhPhiKCBBHYTViewModel)
        {
            _chungTuChiTietService = iQtcCapKinhPhiKcbChiTietService;
            _chungTuService = iQtcCapKinhPhiKcbService;
            _sessionService = sessionService;
            _log = log;
            _logger = logger;
            _mapper = mapper;
            _nsDonViService = nsDonViService;
            PrintQuyetToanCapKinhPhiKCBBHYTViewModel = printQuyetToanCapKinhPhiKCBBHYTViewModel;

            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            SearchCommand = new RelayCommand(obj => SearchDataParent());
            RefreshCommand = new RelayCommand(obj => Init());
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            if (Model != null)
            {
                IsLock = Model.BKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            SearchText = string.Empty;
            LoadData();
        }

        /// <summary>
        /// Mở màn hình in
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog(object param)
        {
            try
            {
                PrintQuyetToanCapKinhPhiKCBBHYTViewModel.Init();
                var view1 = new PrintQuyetToanCapKinhPhiKCBBHYT
                {
                    DataContext = PrintQuyetToanCapKinhPhiKCBBHYTViewModel
                };
                DialogHost.Show(view1, SettlementScreen.DETAIL_DIALOG, null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void SearchDataParent()
        {
            if (_chungTuChiTietModelsView != null)
            {
                _chungTuChiTietModelsView.Refresh();
            }
        }

        private void OnClearSearch(object obj)
        {
            _chungTuChiTietModelsView.Refresh();
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null || IsLock)
                return;
            if (Model.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, Model.SNguoiTao));
                return;
            }
            SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
        }

        public override void OnSave()
        {
            if (!IsSaveData)
            {
                return;
            }
            Func<BhQtCapKinhPhiKcbChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
            Func<BhQtCapKinhPhiKcbChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            Func<BhQtCapKinhPhiKcbChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();

            //Thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<BhQtCapKinhPhiKcbChiTiet>();
                _mapper.Map(detailsAdd, addItems);
                _chungTuChiTietService.AddRange(addItems);

                Items.Where(isAdd).Select(x =>
                {
                    x.IsModified = false;
                    x.IsAdd = false;
                    return x;
                }).ToList();
            }

            //cập nhật chứng từ chi tiết
            if (detailsUpdate.Count > 0)
            {
                foreach (var updateItem in detailsUpdate)
                {
                    var khtBHXHChiTiet = _chungTuChiTietService.FindById(updateItem.Id);
                    _mapper.Map(updateItem, khtBHXHChiTiet);
                    _chungTuChiTietService.Update(khtBHXHChiTiet);
                    updateItem.IsModified = false;
                }
            }
            //cập nhật tổng cộng chứng từ
            var chungTu = _chungTuService.FindById(Model.Id);
            chungTu.FKeHoachCap = Model.FKeHoachCap;
            chungTu.FDaQuyetToan = Model.FDaQuyetToan;
            chungTu.FConLai = Model.FConLai;
            chungTu.FQuyetToanQuyNay = Model.FQuyetToanQuyNay;
            _chungTuService.Update(chungTu);

            _log.WriteLog(Resources.ApplicationName, "Quyết toán KP KCB BHYT - Chứng từ chi tiết", (int)TypeExecute.Adjust, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            OnRefresh();
            var message = Resources.MsgSaveDone;
            MessageBoxHelper.Info(message);
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        public bool IsVoucherSummary { get; set; }

        public override void LoadData(params object[] args)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            List<BhQtCapKinhPhiKcb> listChungTu = new List<BhQtCapKinhPhiKcb>();
            BhQtCapKinhPhiKcbChiTietCriteria searchCondition = new BhQtCapKinhPhiKcbChiTietCriteria();
            searchCondition.NamLamViec = yearOfWork;
            searchCondition.IIDCTCapKinhPhiKCB = Model.Id;
            searchCondition.LstLns = Model.SDslns.Split(",").Distinct().ToList();
            searchCondition.SMaCSYT = Model.SCoSoYTe;
            searchCondition.IQuy = Model.IQuy;
            searchCondition.ILoaiKinhPhi = Model.ILoaiKinhPhi;

            var temp = _chungTuChiTietService.FindVoucherDetailByCondition(searchCondition).ToList();
            var existBhChiTiet = _chungTuChiTietService.ExistVoucherDetail(Model.Id);
            if (!temp.IsEmpty())
            {
                temp = temp.Select(x =>
                {
                    x.IsAuToFillTuChi = !existBhChiTiet;
                    return x;
                }).ToList();
            }

            if (!IsShowColumnKPKCBBHYT)
            {
                temp.ForEach(x =>
                {
                    if (!x.FQuyetToanQuyNay.HasValue || x.FQuyetToanQuyNay.Value == 0)
                    {
                        x.FQuyetToanQuyNay = x.FQuyetToan4Quy;
                    }
                });
            }

            if (Model.IQuy == yearOfWork)
            {
                var lstQuy = "1,2,3,4";
                listChungTu = _chungTuService.FindByYear(yearOfWork).Where(x => lstQuy.Contains(x.IQuy.ToString())).ToList();
            }

            Items = _mapper.Map<ObservableCollection<BhQtCapKinhPhiKcbChiTietModel>>(temp.Where(x => !x.IsHangCha));
            _chungTuChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            _chungTuChiTietModelsView.Filter = BHQTCKPKCBModelsFilter;
            foreach (var qtItem in Items)
            {
                if (Model.IQuy == yearOfWork)
                {
                    var listChungTuMap = listChungTu.Where(x => x.SCoSoYTe == qtItem.sMaCoSoYTe && x.SDslns == qtItem.SLns).ToList();
                    qtItem.FQuyetToan4Quy = listChungTuMap.Sum(x => x.FQuyetToanQuyNay);
                }

                qtItem.IsFilter = true;
                if (!qtItem.IsHangCha)
                {
                    qtItem.PropertyChanged += (sender, args) =>
                    {
                        BhQtCapKinhPhiKcbChiTietModel item = (BhQtCapKinhPhiKcbChiTietModel)sender;
                        item.IsModified = true;
                        CalculateData();
                        qtItem.IsModified = true;
                        OnPropertyChanged(nameof(IsSaveData));
                    };
                }
                CalculateData();
            }
        }

        private bool BHQTCKPKCBModelsFilter(object obj)
        {
            if (!(obj is BhQtCapKinhPhiKcbChiTietModel temp))
                return true;
            var keyword = SearchText?.Trim().ToLower() ?? string.Empty;
            var condition1 = false;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(temp.SLns))
                    condition1 = condition1 || temp.SLns.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.STenMLNS))
                    condition1 = condition1 || temp.STenMLNS.ToLower().Contains(keyword);
            }
            else
            {
                condition1 = true;
            }

            var result = condition1;
            return result;
        }

        private void CalculateParent(Guid idParent, BhQtCapKinhPhiKcbChiTietModel item, Dictionary<Guid, BhQtCapKinhPhiKcbChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FKeHoachCap += item.FKeHoachCap;
            model.FDaQuyetToan += item.FDaQuyetToan;
            model.FQuyetToanQuyNay += item.FQuyetToanQuyNay;

            CalculateParent(model.IIdMlnsCha, item, dictByMlns);
        }
        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FKeHoachCap = 0;
                    x.FDaQuyetToan = 0;
                    x.FQuyetToanQuyNay = 0;
                });

            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IIdMlns).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IIdMlnsCha, item, dictByMlns);
            }

            UpdateTotal();
        }
        private void UpdateTotal()
        {
            Model.FKeHoachCap = 0;
            Model.FDaQuyetToan = 0;
            Model.FQuyetToanQuyNay = 0;
            Model.FConLai = 0;

            var roots = Items.Where(t => !t.IsHangCha).ToList();
            foreach (var item in roots)
            {
                Model.FKeHoachCap += item.FKeHoachCap;
                Model.FDaQuyetToan += item.FDaQuyetToan;
                Model.FQuyetToanQuyNay += item.FQuyetToanQuyNay;
                Model.FConLai += item.FConLai;
            }
        }
        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnabledDelete));
        }
    }
}
