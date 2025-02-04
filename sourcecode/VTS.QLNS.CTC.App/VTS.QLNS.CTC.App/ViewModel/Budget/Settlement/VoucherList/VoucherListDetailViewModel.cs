using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.VoucherList
{
    public class VoucherListDetailViewModel : DetailViewModelBase<VoucherListModel, VoucherListDetailModel>
    {
        private IMapper _mapper;
        private INsBkChungTuChiTietService _chungTuChiTietService;
        private INsBkChungTuService _chungTuService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private INsDonViService _donViService;
        private INsMucLucNganSachService _mucLucNganSachService;
        private PrintVoucherListViewModel PrintVoucherListViewModel;
        private PrintSummaryVoucherListViewModel PrintSummaryVoucherListViewModel;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateVoucherListEvent;
        public override Type ContentType => typeof(View.Budget.Settlement.VoucherList.VoucherListDetail);

        public int NamLamViec { get; set; }

        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private AgencyModel _selectedAgency;
        public AgencyModel SelectedAgency
        {
            get => _selectedAgency;
            set => SetProperty(ref _selectedAgency, value);
        }

        private List<ComboboxItem> _loaiChi;

        public List<ComboboxItem> LoaiChi
        {
            get => _loaiChi;
            set => SetProperty(ref _loaiChi, value);
        }

        private ObservableCollection<VoucherListDetailModel> _voucherListDetails;
        public ObservableCollection<VoucherListDetailModel> VoucherListDetails
        {
            get => _voucherListDetails;
            set => SetProperty(ref _voucherListDetails, value);
        }

        public bool IsLocked => Model.BKhoa;

        public bool IsSaveData => _voucherListDetails.Any(item => item.IsModified || item.IsDeleted);
        public bool IsDeleteAll => _voucherListDetails.Any(item => !item.IsModified && item.HasData);

        private double _tuChi;
        public double TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        private double _hienVat;
        public double HienVat
        {
            get => _hienVat;
            set => SetProperty(ref _hienVat, value);
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        public RelayCommand SaveDataCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }

        public VoucherListDetailViewModel(IMapper mapper,
            INsBkChungTuChiTietService chungTuChiTietService,
            ISessionService sessionService,
            INsDonViService donViService,
            INsBkChungTuService chungTuService,
            INsMucLucNganSachService mucLucNganSachService,
            PrintVoucherListViewModel printVoucherListViewModel,
            PrintSummaryVoucherListViewModel printSummaryVoucherListViewModel)
        {
            _mapper = mapper;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;
            _donViService = donViService;
            _chungTuChiTietService = chungTuChiTietService;
            _chungTuService = chungTuService;
            _mucLucNganSachService = mucLucNganSachService;
            PrintVoucherListViewModel = printVoucherListViewModel;
            PrintSummaryVoucherListViewModel = printSummaryVoucherListViewModel;

            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            ResetAll();
            LoadLoaiChi();
            LoadAgencies();
            LoadData();
        }

        private void ResetAll()
        {
            _voucherListDetails = new ObservableCollection<VoucherListDetailModel>();
            OnPropertyChanged(nameof(VoucherListDetails));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
        }

        private void LoadLoaiChi()
        {
            LoaiChi = new List<ComboboxItem>()
            {
                //new ComboboxItem { DisplayItem = "Tất cả", ValueItem = "0" },
                new ComboboxItem { DisplayItem = "Chi TSCĐ", ValueItem = "1" },
                new ComboboxItem { DisplayItem = "Chi trực tiếp", ValueItem = "2" },
                new ComboboxItem { DisplayItem = "Nhập kho", ValueItem = "3" }
            };
        }

        private void LoadAgencies()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            var isDonViCap4 = !_donViService.FindAll().Where(x => x.NamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == StatusType.ACTIVE && x.Loai == LoaiDonVi.NOI_BO).Any();
            List<DonVi> listDonVi;

            if (isDonViCap4)
            {
                predicate = predicate.And(x => x.Loai == LoaiDonVi.ROOT && x.NamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == StatusType.ACTIVE);
                listDonVi = _donViService.FindByCondition(predicate).ToList();
            }
            else
            {
                predicate = predicate.And(x => x.Loai != LoaiDonVi.ROOT && x.NamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == StatusType.ACTIVE);
                listDonVi = _donViService.FindByCondition(predicate).ToList();
            }

            _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            List<NsBkChungTuChiTietQuery> _listChungTuChiTiet = _chungTuChiTietService.FindByVoucherListId(Model.Id, _sessionInfo.YearOfWork).OrderBy(n => n.DNgayChungTu).ThenBy(n => n.SSoChungTu).ToList();
            _voucherListDetails = _mapper.Map<ObservableCollection<VoucherListDetailModel>>(_listChungTuChiTiet);

            if (_voucherListDetails.Count == 0)
            {
                var item = new VoucherListDetailModel((DateTime)Model.DNgayChungTu);
                item.ILoaiChi = 2;
                _voucherListDetails.Add(item);
            }
            //_voucherListDetails.Add(new VoucherListDetailModel(DateTime.Now));
            int stt = 1;
            foreach (var item in _voucherListDetails)
            {
                item.Stt = stt++;
                item.PropertyChanged += VoucherListDetailModel_PropertyChanged;
            }
            SummaryVoucherListDetail();
            OnPropertyChanged(nameof(VoucherListDetails));
        }

        private void SummaryVoucherListDetail()
        {
            TuChi = 0;
            HienVat = 0;
            foreach (var item in _voucherListDetails)
            {
                TuChi += item.FTongTuChi;
                HienVat += item.FTongHienVat;
            }
        }

        protected override void OnAdd()
        {
            base.OnAdd();
            if (VoucherListDetails.Count > 0)
            {
                if (SelectedItem != null)
                {
                    int currentRow = VoucherListDetails.IndexOf(SelectedItem);
                    VoucherListDetailModel newItem = new VoucherListDetailModel((DateTime)SelectedItem.DNgayChungTu);
                    newItem.Stt = SelectedItem.Stt + 1;
                    newItem.ILoaiChi = 2;
                    newItem.PropertyChanged += VoucherListDetailModel_PropertyChanged;
                    VoucherListDetails.Insert(currentRow + 1, newItem);
                    VoucherListDetails.Where((x, index) => index > currentRow + 1).Select(x => x.Stt++).ToList();
                }
                else
                {
                    int currentRow = VoucherListDetails.Count - 1;
                    VoucherListDetailModel newItem = new VoucherListDetailModel((DateTime)Model.DNgayChungTu);
                    newItem.Stt = VoucherListDetails.Count + 1;
                    newItem.ILoaiChi = 2;
                    newItem.PropertyChanged += VoucherListDetailModel_PropertyChanged;
                    VoucherListDetails.Insert(currentRow + 1, newItem);
                }
            }
        }

        private void VoucherListDetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(VoucherListDetailModel.IsModified)
                       && args.PropertyName != nameof(VoucherListDetailModel.IsDeleted))
            {
                VoucherListDetailModel item = (VoucherListDetailModel)sender;
                item.IsModified = true;
                SummaryVoucherListDetail();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            if (SelectedItem != null)
            {
                VoucherListDetailModel item = VoucherListDetails.Where(x => x.CustomId == SelectedItem.CustomId).First();
                item.IsDeleted = !item.IsDeleted;
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            if (IsSaveData)
            {
                var result = MessageBoxHelper.ConfirmCancel(Resources.ConfirmReloadData);
                if (result == MessageBoxResult.Cancel)
                    return;
                else if (result == MessageBoxResult.Yes)
                    OnSaveData();
            }
            LoadData();
        }

        private void OnSaveData()
        {
            List<VoucherListDetailModel> voucherListDetailAdd = _voucherListDetails.Where(x => x.Id == Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
            List<VoucherListDetailModel> voucherListDetailUpdate = _voucherListDetails.Where(x => x.Id != Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
            List<VoucherListDetailModel> voucherListDetailDelete = _voucherListDetails.Where(x => x.IsDeleted).ToList();

            if (voucherListDetailAdd.Count > 0)
            {
                NsMucLucNganSach mlns = null;
                List<NsMucLucNganSach> listMlns = _mucLucNganSachService.FindByXauNoiMaAndNamLamViec(new List<string> { Model.SXauNoiMa }, _sessionInfo.YearOfWork).ToList();
                if (listMlns.Count > 0)
                    mlns = listMlns.First();
                voucherListDetailAdd = voucherListDetailAdd.Select(x =>
                {
                    x.IIdBkchungTu = Model.Id;
                    x.IIdMlns = Guid.Empty;
                    x.SXauNoiMa = Model.SXauNoiMa;
                    x.INamLamViec = _sessionInfo.YearOfWork;
                    x.INamNganSach = _sessionInfo.YearOfBudget;
                    x.IIdMaNguonNganSach = _sessionInfo.Budget;
                    x.IThangQuyLoai = Model.IThangQuyLoai;
                    x.IThangQuy = Model.IThangQuy;
                    x.STenDonVi = string.IsNullOrEmpty(x.IIdMaDonVi) ? string.Empty : Agencies.Where(agency => agency.Id == x.IIdMaDonVi).FirstOrDefault().AgencyName;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.DNgayTao = DateTime.Now;
                    if (mlns != null)
                    {
                        x.SLns = mlns.Lns;
                        x.SL = mlns.L;
                        x.SK = mlns.K;
                        x.SM = mlns.M;
                        x.STm = mlns.Tm;
                        x.STtm = mlns.Ttm;
                        x.SNg = mlns.Ng;
                        x.STng = mlns.Tng;
                        x.SXauNoiMa = mlns.XauNoiMa;
                        x.IIdMlns = mlns.MlnsId;
                        x.IIdMlnsCha = mlns.MlnsIdParent;
                    }
                    return x;
                }).ToList();

                List<NsBkChungTuChiTiet> listChungTuChiTiet = new List<NsBkChungTuChiTiet>();
                listChungTuChiTiet = _mapper.Map<List<NsBkChungTuChiTiet>>(voucherListDetailAdd);
                _chungTuChiTietService.AddRange(listChungTuChiTiet);
                _voucherListDetails.Where(x => x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            if (voucherListDetailUpdate.Count > 0)
            {
                foreach (var item in voucherListDetailUpdate)
                {
                    item.SNguoiSua = _sessionInfo.Principal;
                    item.DNgaySua = DateTime.Now;
                    item.STenDonVi = string.IsNullOrEmpty(item.IIdMaDonVi) ? string.Empty : Agencies.Where(agency => agency.Id == item.IIdMaDonVi).FirstOrDefault().AgencyName;
                    NsBkChungTuChiTiet giaiThichLuongTru = _chungTuChiTietService.FindById(item.Id);
                    _mapper.Map(item, giaiThichLuongTru);
                    _chungTuChiTietService.Update(giaiThichLuongTru);
                    item.IsModified = false;
                }
            }

            if (voucherListDetailDelete.Count > 0)
            {
                foreach (var item in voucherListDetailDelete)
                {
                    _chungTuChiTietService.Delete(item.Id);
                    _voucherListDetails.Remove(item);
                }
            }

            //cập nhật thông tin chứng từ
            UpdateVoucherList();

            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            LoadData();

            MessageBoxHelper.Info(Resources.MsgSaveDone);

            //refresh dữ liệu ở màn index
            DataChangedEventHandler handler = UpdateVoucherListEvent;
            if (handler != null)
            {
                handler(Model, new EventArgs());
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                _chungTuChiTietService.DeleteByVoucherId(Model.Id);
                TuChi = 0;
                HienVat = 0;
                UpdateVoucherList();
                LoadData();
                MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void UpdateVoucherList()
        {
            NsBkChungTu chungTu = _chungTuService.FindById(Model.Id);
            chungTu.FTongTuChi = TuChi;
            chungTu.FTongHienVat = HienVat;
            Model.FTongTuChi = TuChi;
            Model.FTongHienVat = HienVat;
            _chungTuService.Update(chungTu);
        }

        /// <summary>
        /// Mở màn hình in
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog(object param)
        {
            int dialogType = (int)param;
            switch (dialogType)
            {
                case (int)VoucherListPrintType.PRINT_VOUCHER_LIST:
                    PrintVoucherListViewModel.VoucherList = Model;
                    PrintVoucherListViewModel.Init();
                    var view1 = new PrintVoucherList { DataContext = PrintVoucherListViewModel };
                    //show the dialog
                    DialogHost.Show(view1, SettlementScreen.VOUCHER_LIST_DETAIL_DIALOG, null, null);
                    break;
                case (int)VoucherListPrintType.PRINT_SUMMARY_VOUCHER_LIST:
                    PrintSummaryVoucherListViewModel.Init();
                    var view2 = new PrintSummaryVoucherList { DataContext = PrintSummaryVoucherListViewModel };
                    //show the dialog
                    DialogHost.Show(view2, SettlementScreen.VOUCHER_LIST_DETAIL_DIALOG, null, null);
                    break;
            }
        }

        /// <summary>
        /// mở dialog confirm khóa chứng từ
        /// </summary>
        protected override void OnLockUnLock()
        {
            string message = Model.BKhoa ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = Model.BKhoa ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
            {
                _chungTuService.LockOrUnlock(Model.Id, !Model.BKhoa);
                Model.BKhoa = !Model.BKhoa;
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                MessageBoxHelper.Info(msgDone);
            }
        }
    }
}
