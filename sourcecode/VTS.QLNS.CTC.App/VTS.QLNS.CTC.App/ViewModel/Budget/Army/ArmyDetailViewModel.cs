using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Settlement.GetDataLuong;
using VTS.QLNS.CTC.App.View.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.GetDataLuong;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Army
{
    public class ArmyDetailViewModel : ViewModelBase
    {
        private readonly ILog _logger;
        private IMapper _mapper;
        private INsQsChungTuChiTietService _chungTuChiTietService;
        private ITlQsChungTuService _iTlQsChungTuService;
        private INsQsChungTuService _chungTuService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private INsDonViService _donViService;
        private IDanhMucService _danhMucService;
        private INsQsMucLucService _mucLucService;
        private readonly IExportService _exportService;
        private GetDataQtQuanSoLuongViewModel GetDataQtQuanSoLuongViewModel;
        private List<string> _propertiesChange;
        private const string M1 = "1";
        private const string M2 = "2";
        private const string M3 = "3";
        private const string M4 = "4";
        private const string M5 = "5";
        private const string M6 = "6";
        private const string M7 = "7";
        private PrintArmyViewModel PrintArmyViewModel;
        private ObservableCollection<ArmyVoucherDetailModel> _armyVoucherDetailsBefore;
        private List<QsChungTuChiTietQuery> _chungTuChiTiets;
        private List<int> _listMonth;

        public override Type ContentType => typeof(View.Budget.Settlement.Army.ArmyDetail);

        public int NamLamViec { get; set; }

        private ObservableCollection<ArmyVoucherDetailModel> _armyVoucherDetails;
        public ObservableCollection<ArmyVoucherDetailModel> ArmyVoucherDetails
        {
            get => _armyVoucherDetails;
            set => SetProperty(ref _armyVoucherDetails, value);
        }

        private ArmyVoucherModel _armyVoucher;
        public ArmyVoucherModel ArmyVoucher
        {
            get => _armyVoucher;
            set => SetProperty(ref _armyVoucher, value);
        }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }
        private bool OnlyHasRoot { get; set; }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _selectedMonth;
        public ComboboxItem SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                SetProperty(ref _selectedMonth, value);
                SearchData();
            }
        }

        private ComboboxItem _selectedAgency;
        public ComboboxItem SelectedAgency
        {
            get => _selectedAgency;
            set
            {
                //OnSaveDatum();
                SetProperty(ref _selectedAgency, value);
                OnPropertyChanged(nameof(IsEdit));
                OnPropertyChanged(nameof(IsGetDataLuong));
                SearchData();
            }
        }

        /// <summary>
        /// lưu trạng thái để enable/disable button khóa
        /// </summary>
        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set
            {
                SetProperty(ref _isLock, value);
                OnPropertyChanged(nameof(IsEdit));
            }
        }

        private ArmyVoucherDetailModel _selectedArmyVoucherDetail;
        public ArmyVoucherDetailModel SelectedArmyVoucherDetail
        {
            get => _selectedArmyVoucherDetail;
            set => SetProperty(ref _selectedArmyVoucherDetail, value);
        }

        public bool IsEdit => (OnlyHasRoot || (string.IsNullOrEmpty(_selectedAgency.ValueItem) ? false : true)) && !IsLock;

        public bool IsSaveData => _armyVoucherDetails.Any(item => item.IsModified || item.IsDeleted);
        public bool IsDeleteAll => _armyVoucherDetails.Any(item => !item.IsModified && item.HasData);

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
        }
        public bool IsGetDataLuong => !ArmyVoucher.BKhoa && !(string.IsNullOrEmpty(_selectedAgency.ValueItem) && _agencies.Count > 1);
        public bool IsSetDataLuong { get; set; }
        public Guid IdChungTuLuong { get; set; }

        public RelayCommand LockCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }
        public RelayCommand DeleteAllCommand { get; }
        public RelayCommand GetDataCommand { get; }
        public RelayCommand RefreshCommand { get; }

        public ArmyDetailViewModel(
            ILog logger,
            IMapper mapper,
            INsQsChungTuChiTietService chungTuChiTietService,
            INsQsChungTuService chungTuService,
            ISessionService sessionService,
            INsDonViService donViService,
            IExportService exportService,
            INsQsMucLucService mucLucService,
            IDanhMucService danhMucService,
            PrintArmyViewModel printArmyViewModel,
            ITlQsChungTuService iTlQsChungTuService,
            GetDataQtQuanSoLuongViewModel getDataQtQuanSoLuongViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _chungTuChiTietService = chungTuChiTietService;
            _chungTuService = chungTuService;
            _sessionService = sessionService;
            _donViService = donViService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _mucLucService = mucLucService;
            _iTlQsChungTuService = iTlQsChungTuService;
            PrintArmyViewModel = printArmyViewModel;
            GetDataQtQuanSoLuongViewModel = getDataQtQuanSoLuongViewModel;

            LockCommand = new RelayCommand(obj => OnOpenLockConfirmDialog());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            DeleteCommand = new RelayCommand(obj => OnDelete());
            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            ExportCommand = new RelayCommand(obj => IsOpenExportPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData(obj));
            DeleteAllCommand = new RelayCommand(obj => OnDeleteAll());
            GetDataCommand = new RelayCommand(obj => OnGetDataLuong());
            RefreshCommand = new RelayCommand(obj => OnRefresh());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            IsLock = ArmyVoucher.BKhoa;
            LoadPropertyChange();
            LoadAgencies();
            LoadMonths();
            LoadArmyDetail();
        }

        private ArmyVoucherDetailCriteria LoadCondition(ArmyVoucherModel armyVoucher, string agencyId, int yearOfWork)
        {
            ArmyVoucherDetailMethod method = ArmyVoucherDetailMethod.GET_PART;
            if (string.IsNullOrEmpty(agencyId))
            {
                if (string.IsNullOrEmpty(_selectedAgency.ValueItem))
                {
                    if (OnlyHasRoot)
                    {
                        List<DonVi> listDonVi = _donViService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == StatusType.ACTIVE).ToList();
                        var agencyRoot = listDonVi.FirstOrDefault(x => x.Loai == LoaiDonVi.ROOT);
                        agencyId = agencyRoot.IIDMaDonVi.ToString();
                    }
                    else
                    {
                        agencyId = string.Join(",", _agencies.Where(x => !string.IsNullOrEmpty(x.ValueItem)).Select(x => x.ValueItem).ToArray());
                        method = ArmyVoucherDetailMethod.GET_ALL;
                    }
                }
                else
                    agencyId = _selectedAgency.ValueItem;
            }
            return new ArmyVoucherDetailCriteria
            {
                YearOfWork = yearOfWork,
                VoucherId = armyVoucher.Id.ToString(),
                AgencyId = agencyId,
                Method = method
            };
        }

        private void LoadArmyDetail()
        {
            ArmyVoucherDetailCriteria condition = LoadCondition(ArmyVoucher, string.Empty, _sessionInfo.YearOfWork);
            _chungTuChiTiets = _chungTuChiTietService.FindByCondition(condition);

            // loại bỏ điều kiện tháng 12 năm trước không có dữ liệu mới cho phép nhập mục 100
            // CTC yêu cầu cho nhập hết, sai dữ liệu phải chịu
            // ducbq1, thân ái!
            /*
            if (int.Parse(_selectedMonth.ValueItem) == 0)
            {
                NsQsChungTu chungTu = _chungTuService.FindByMonth(12, _sessionInfo.YearOfWork - 1);
                if (chungTu == null)
                {
                    _chungTuChiTiets.Where(n => n.SKyHieu == "100").Select(m => m.BHangCha = false).ToList();
                }
            }
            */

            _chungTuChiTiets.Where(n => n.SKyHieu == "100").Select(m => m.BHangCha = false).ToList();

            _armyVoucherDetails = _mapper.Map<ObservableCollection<ArmyVoucherDetailModel>>(_chungTuChiTiets);

            foreach (var model in _armyVoucherDetails)
            {
                if (!model.BHangCha)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (_propertiesChange.Contains(args.PropertyName))
                        {
                            ArmyVoucherDetailModel item = (ArmyVoucherDetailModel)sender;
                            item.IsModified = true;
                            if (!IsSetDataLuong)
                            {
                                CalculateData(ref _armyVoucherDetails);
                            }
                            OnPropertyChanged(nameof(IsSaveData));
                            OnPropertyChanged(nameof(IsDeleteAll));
                        }
                    };
                }
            }
            if (_armyVoucherDetails.Count > 0)
                CalculateData(ref _armyVoucherDetails);
            OnPropertyChanged(nameof(ArmyVoucherDetails));
        }

        private void SearchData()
        {
            int month = Convert.ToInt32(_selectedMonth.ValueItem);
            NsQsChungTu chungTu = _chungTuService.FindByMonth(month, _sessionInfo.YearOfWork);
            ArmyVoucher = _mapper.Map<ArmyVoucherModel>(chungTu);
            LoadArmyDetail();
        }

        /// <summary>
        /// Tạo data cho combobox đơn vị
        /// </summary>
        private void LoadAgencies()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            List<DonVi> listDonVi = _donViService.FindByCondition(predicate).ToList();
            OnlyHasRoot = !listDonVi.Any(x => x.Loai != LoaiDonVi.ROOT);

            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi.Where(x => x.Loai == LoaiDonVi.NOI_BO));
            _agencies.Insert(0, new ComboboxItem("--Tất cả đơn vị--", null));
            _selectedAgency = _agencies.FirstOrDefault();

        }

        /// <summary>
        /// Tạo data cho combobox tháng
        /// </summary>
        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            _listMonth = _chungTuService.FindMonthOfArmy(_sessionInfo.YearOfWork);
            foreach (var month in _listMonth)
            {
                _months.Add(new ComboboxItem("Tháng " + month, month.ToString()));
            }
            _selectedMonth = _months.Where(x => x.ValueItem == ArmyVoucher.IThangQuy.ToString()).FirstOrDefault();
        }

        /// <summary>
        /// mở dialog confirm khóa chứng từ
        /// </summary>
        private void OnOpenLockConfirmDialog()
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
            {
                IsLock = !IsLock;
                _chungTuService.LockOrUnlock(ArmyVoucher.Id, !ArmyVoucher.BKhoa);
                MessageBoxHelper.Info(msgDone);
            }
        }

        private void CalculateData(ref ObservableCollection<ArmyVoucherDetailModel> armyVoucherDetail)
        {
            //reset parent value                       
            armyVoucherDetail.Where(x => x.BHangCha)
                .Select(x =>
                {
                    if (x.SM != M1)
                        ResetVoucherDetailData(x);
                    return x;
                }).ToList();
            ArmyVoucherDetailModel voucherDetailM1 = armyVoucherDetail.Where(x => x.SM == M1).FirstOrDefault();
            List<ArmyVoucherDetailModel> voucherDetailsM2 = armyVoucherDetail.Where(x => x.SM == M2 && !x.IsDeleted).ToList();
            List<ArmyVoucherDetailModel> voucherDetailsM3 = armyVoucherDetail.Where(x => x.SM == M3 && !x.IsDeleted).ToList();
            ArmyVoucherDetailModel voucherDetailM4 = armyVoucherDetail.Where(x => x.SM == M4).FirstOrDefault();
            ArmyVoucherDetailModel voucherDetailM5 = armyVoucherDetail.Where(x => x.SM == M5).FirstOrDefault();
            ArmyVoucherDetailModel voucherDetailM6 = armyVoucherDetail.Where(x => x.SM == M6).FirstOrDefault();
            ArmyVoucherDetailModel voucherDetailM7 = armyVoucherDetail.Where(x => x.SM == M7).FirstOrDefault();

            ArmyVoucherDetailModel voucherDetailM2 = voucherDetailsM2.Where(x => x.BHangCha).FirstOrDefault();
            foreach (var item in voucherDetailsM2)
            {
                SumItem(item, ref voucherDetailM2);
            }

            ArmyVoucherDetailModel voucherDetailM3 = voucherDetailsM3.Where(x => x.BHangCha).FirstOrDefault();
            foreach (var item in voucherDetailsM3)
            {
                SumItem(item, ref voucherDetailM3);
            }

            SumItem(voucherDetailM1, ref voucherDetailM4);
            SumItem(voucherDetailM2, ref voucherDetailM4);
            AbstractItem(voucherDetailM3, ref voucherDetailM4);
            SumItem(voucherDetailM4, ref voucherDetailM7);
            SumItem(voucherDetailM5, ref voucherDetailM7);
            AbstractItem(voucherDetailM6, ref voucherDetailM7);
        }

        private void SumItem(ArmyVoucherDetailModel child, ref ArmyVoucherDetailModel parent)
        {
            if (parent == null || child == null)
            {
                return;
            }
            parent.FSoThieuUy += child.FSoThieuUy;
            parent.FSoTrungUy += child.FSoTrungUy;
            parent.FSoThuongUy += child.FSoThuongUy;
            parent.FSoDaiUy += child.FSoDaiUy;
            parent.FSoThieuTa += child.FSoThieuTa;
            parent.FSoThuongTa += child.FSoThuongTa;
            parent.FSoTrungTa += child.FSoTrungTa;
            parent.FSoDaiTa += child.FSoDaiTa;
            parent.FSoTuong += child.FSoTuong;
            parent.FSoBinhNhi += child.FSoBinhNhi;
            parent.FSoBinhNhat += child.FSoBinhNhat;
            parent.FSoHaSi += child.FSoHaSi;
            parent.FSoTrungSi += child.FSoTrungSi;
            parent.FSoThuongSi += child.FSoThuongSi;
            parent.FSoThuongTaQNCN += child.FSoThuongTaQNCN;
            parent.FSoTrungTaQNCN += child.FSoTrungTaQNCN;
            parent.FSoThieuTaQNCN += child.FSoThieuTaQNCN;
            parent.FSoDaiUyQNCN += child.FSoDaiUyQNCN;
            parent.FSoThuongUyQNCN += child.FSoThuongUyQNCN;
            parent.FSoTrungUyQNCN += child.FSoTrungUyQNCN;
            parent.FSoThieuUyQNCN += child.FSoThieuUyQNCN;
            parent.FSoVcqp += child.FSoVcqp;
            parent.FSoCnvqp += child.FSoCnvqp;
            parent.FSoLdhd += child.FSoLdhd;
            parent.FSoCcqp += child.FSoCcqp;
        }

        private void AbstractItem(ArmyVoucherDetailModel child, ref ArmyVoucherDetailModel parent)
        {
            if (parent == null || child == null)
            {
                return;
            }
            parent.FSoThieuUy -= child.FSoThieuUy;
            parent.FSoTrungUy -= child.FSoTrungUy;
            parent.FSoThuongUy -= child.FSoThuongUy;
            parent.FSoDaiUy -= child.FSoDaiUy;
            parent.FSoThieuTa -= child.FSoThieuTa;
            parent.FSoTrungTa -= child.FSoTrungTa;
            parent.FSoThuongTa -= child.FSoThuongTa;
            parent.FSoDaiTa -= child.FSoDaiTa;
            parent.FSoTuong -= child.FSoTuong;
            parent.FSoBinhNhi -= child.FSoBinhNhi;
            parent.FSoBinhNhat -= child.FSoBinhNhat;
            parent.FSoHaSi -= child.FSoHaSi;
            parent.FSoTrungSi -= child.FSoTrungSi;
            parent.FSoThuongSi -= child.FSoThuongSi;
            parent.FSoThuongTaQNCN -= child.FSoThuongTaQNCN;
            parent.FSoTrungTaQNCN -= child.FSoTrungTaQNCN;
            parent.FSoThieuTaQNCN -= child.FSoThieuTaQNCN;
            parent.FSoDaiUyQNCN -= child.FSoDaiUyQNCN;
            parent.FSoThuongUyQNCN -= child.FSoThuongUyQNCN;
            parent.FSoTrungUyQNCN -= child.FSoTrungUyQNCN;
            parent.FSoThieuUyQNCN -= child.FSoThieuUyQNCN;
            parent.FSoVcqp -= child.FSoVcqp;
            parent.FSoCnvqp -= child.FSoCnvqp;
            parent.FSoLdhd -= child.FSoLdhd;
            parent.FSoCcqp -= child.FSoCcqp;
        }
        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        private void OnSaveData(bool isMessage = false)
        {
            bool isChangeData = false;
            CalculateData(ref _armyVoucherDetails);
            List<ArmyVoucherDetailModel> voucherDetailsAdd = _armyVoucherDetails.Where(x => (x.IsModified && !x.IsDeleted && x.Id == Guid.Empty) || (x.BHangCha && x.Id == Guid.Empty)).ToList();
            List<ArmyVoucherDetailModel> voucherDetailUpdate = _armyVoucherDetails.Where(x => (x.IsModified && !x.IsDeleted && x.Id != Guid.Empty) || (x.BHangCha && x.Id != Guid.Empty)).ToList();
            List<ArmyVoucherDetailModel> voucherDetailDelete = _armyVoucherDetails.Where(x => x.IsDeleted).ToList();

            int yearOfWork = _sessionInfo.YearOfWork;
            List<DonVi> listDonVi = _donViService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == StatusType.ACTIVE).ToList();
            var agencyRoot = listDonVi.FirstOrDefault(x => x.Loai == LoaiDonVi.ROOT);

            //thêm mới chứng từ chi tiết
            if (voucherDetailsAdd.Count > 0)
            {
                isChangeData = true;
                voucherDetailsAdd = voucherDetailsAdd.Select(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IIdQschungTu = ArmyVoucher.Id;
                    x.IThangQuy = Convert.ToInt32(_selectedMonth.ValueItem);
                    x.IIdMaDonVi = _selectedAgency.ValueItem ?? agencyRoot.IIDMaDonVi;
                    x.ITrangThai = (int)Status.ACTIVE;
                    x.INamLamViec = _sessionInfo.YearOfWork;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.DNgayTao = DateTime.Now;
                    return x;
                }).ToList();
                List<NsQsChungTuChiTiet> chungTuChiTiets = new List<NsQsChungTuChiTiet>();
                chungTuChiTiets = _mapper.Map(voucherDetailsAdd, chungTuChiTiets);
                _chungTuChiTietService.AddRange(chungTuChiTiets);
                _armyVoucherDetails.Where(x => x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            //cập nhật chứng từ chi tiết
            if (voucherDetailUpdate.Count > 0)
            {
                isChangeData = true;
                foreach (var item in voucherDetailUpdate)
                {
                    item.IIdQschungTu = ArmyVoucher.Id;
                    item.IThangQuy = Convert.ToInt32(_selectedMonth.ValueItem);
                    item.IIdMaDonVi = _selectedAgency.ValueItem ?? agencyRoot.IIDMaDonVi;
                    item.ITrangThai = (int)Status.ACTIVE;
                    item.INamLamViec = _sessionInfo.YearOfWork;
                    item.SNguoiSua = _sessionInfo.Principal;
                    item.DNgaySua = DateTime.Now;
                    NsQsChungTuChiTiet chungTuChiTiet = _chungTuChiTietService.FindById(item.Id);
                    _mapper.Map(item, chungTuChiTiet);
                    _chungTuChiTietService.Update(chungTuChiTiet);

                    //reset flag
                    item.IsModified = false;
                }
            }

            //xóa chứng từ chi tiết
            if (voucherDetailDelete.Count > 0)
            {
                isChangeData = true;
                foreach (var item in voucherDetailDelete)
                {
                    _chungTuChiTietService.Delete(item.Id);
                    item.Id = Guid.NewGuid();
                    ResetVoucherDetailData(item);

                    //reset flag
                    item.IsModified = false;
                    item.IsDeleted = false;
                }
            }

            int month = int.Parse(SelectedMonth.ValueItem);

            var monthLast = month;
            while (_listMonth.Contains(monthLast))
            {
                monthLast++;
            }

            var dataM7Old = _chungTuChiTiets.FirstOrDefault(x => x.SM == M7);
            var dataM7New = _armyVoucherDetails.FirstOrDefault(x => x.SM == M7);
            for (int i = month + 1; i < monthLast; i++)
            {
                var predicate = PredicateBuilder.True<NsQsChungTuChiTiet>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && x.IIdMaDonVi == (_selectedAgency.ValueItem ?? agencyRoot.IIDMaDonVi) && x.IThangQuy == i);
                predicate = predicate.And(x => x.SKyHieu == "100" || x.SKyHieu == "700");
                var listChiTiet = _chungTuChiTietService.FindByCondition(predicate).ToList();
                foreach (var item in listChiTiet)
                {
                    item.FSoThieuUy += (dataM7New.FSoThieuUy - dataM7Old.FSoThieuUy.GetValueOrDefault());
                    item.FSoTrungUy += (dataM7New.FSoTrungUy - dataM7Old.FSoTrungUy.GetValueOrDefault());
                    item.FSoThuongUy += (dataM7New.FSoThuongUy - dataM7Old.FSoThuongUy.GetValueOrDefault());
                    item.FSoDaiUy += (dataM7New.FSoDaiUy - dataM7Old.FSoDaiUy.GetValueOrDefault());
                    item.FSoThieuTa += (dataM7New.FSoThieuTa - dataM7Old.FSoThieuTa.GetValueOrDefault());
                    item.FSoTrungTa += (dataM7New.FSoTrungTa - dataM7Old.FSoTrungTa.GetValueOrDefault());
                    item.FSoThuongTa += (dataM7New.FSoThuongTa - dataM7Old.FSoThuongTa.GetValueOrDefault());
                    item.FSoDaiTa += (dataM7New.FSoDaiTa - dataM7Old.FSoDaiTa.GetValueOrDefault());
                    item.FSoTuong += (dataM7New.FSoTuong - dataM7Old.FSoTuong.GetValueOrDefault());
                    item.FSoBinhNhi += (dataM7New.FSoBinhNhi - dataM7Old.FSoBinhNhi.GetValueOrDefault());
                    item.FSoBinhNhat += (dataM7New.FSoBinhNhat - dataM7Old.FSoBinhNhat.GetValueOrDefault());
                    item.FSoHaSi += (dataM7New.FSoHaSi - dataM7Old.FSoHaSi.GetValueOrDefault());
                    item.FSoTrungSi += (dataM7New.FSoTrungSi - dataM7Old.FSoTrungSi.GetValueOrDefault());
                    item.FSoThuongSi += (dataM7New.FSoThuongSi - dataM7Old.FSoThuongSi.GetValueOrDefault());
                    item.FSoThuongTaQNCN += (dataM7New.FSoThuongTaQNCN - dataM7Old.FSoThuongTaQNCN.GetValueOrDefault());
                    item.FSoTrungTaQNCN += (dataM7New.FSoTrungTaQNCN - dataM7Old.FSoTrungTaQNCN.GetValueOrDefault());
                    item.FSoThieuTaQNCN += (dataM7New.FSoThieuTaQNCN - dataM7Old.FSoThieuTaQNCN.GetValueOrDefault());
                    item.FSoDaiUyQNCN += (dataM7New.FSoDaiUyQNCN - dataM7Old.FSoDaiUyQNCN.GetValueOrDefault());
                    item.FSoThuongUyQNCN += (dataM7New.FSoThuongUyQNCN - dataM7Old.FSoThuongUyQNCN.GetValueOrDefault());
                    item.FSoTrungUyQNCN += (dataM7New.FSoTrungUyQNCN - dataM7Old.FSoTrungUyQNCN.GetValueOrDefault());
                    item.FSoThieuUyQNCN += (dataM7New.FSoThieuUyQNCN - dataM7Old.FSoThieuUyQNCN.GetValueOrDefault());
                    item.FSoVcqp += (dataM7New.FSoVcqp - dataM7Old.FSoVcqp.GetValueOrDefault());
                    item.FSoCnvqp += (dataM7New.FSoCnvqp - dataM7Old.FSoCnvqp.GetValueOrDefault());
                    item.FSoLdhd += (dataM7New.FSoLdhd - dataM7Old.FSoLdhd.GetValueOrDefault());
                    item.FSoCcqp += (dataM7New.FSoCcqp - dataM7Old.FSoCcqp);
                    _chungTuChiTietService.Update(item);
                }
            }

            //set chứng từ bên lương là đã lấy dữ liệu
            if (IdChungTuLuong != Guid.Empty)
            {
                var ctLuong = _iTlQsChungTuService.Find(IdChungTuLuong);
                if (ctLuong != null)
                {
                    ctLuong.BNganSachNhanDuLieu = true;
                    _iTlQsChungTuService.Update(ctLuong);
                }
            }

            if (isChangeData)
            {
                LoadArmyDetail();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                if (!isMessage)
                {
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                }
            }
        }

        private void OnDelete()
        {
            if (IsEdit)
            {
                if (SelectedArmyVoucherDetail != null)
                {
                    ArmyVoucherDetailModel item = _armyVoucherDetails.Where(x => x.SKyHieu == SelectedArmyVoucherDetail.SKyHieu).First();
                    if (!item.BHangCha)
                    {
                        item.IsDeleted = !item.IsDeleted;
                        CalculateData(ref _armyVoucherDetails);
                        OnPropertyChanged(nameof(IsSaveData));
                        OnPropertyChanged(nameof(IsDeleteAll));
                    }
                }
            }
        }

        private ArmyVoucherDetailModel ResetVoucherDetailData(ArmyVoucherDetailModel voucherDetail)
        {
            voucherDetail.FSoThieuUy = 0;
            voucherDetail.FSoTrungUy = 0;
            voucherDetail.FSoThuongUy = 0;
            voucherDetail.FSoDaiUy = 0;
            voucherDetail.FSoThieuTa = 0;
            voucherDetail.FSoTrungTa = 0;
            voucherDetail.FSoThuongTa = 0;
            voucherDetail.FSoDaiTa = 0;
            voucherDetail.FSoTuong = 0;
            voucherDetail.FSoBinhNhi = 0;
            voucherDetail.FSoBinhNhat = 0;
            voucherDetail.FSoHaSi = 0;
            voucherDetail.FSoTrungSi = 0;
            voucherDetail.FSoThuongSi = 0;
            voucherDetail.FSoThuongTaQNCN = 0;
            voucherDetail.FSoTrungTaQNCN = 0;
            voucherDetail.FSoThieuTaQNCN = 0;
            voucherDetail.FSoDaiUyQNCN = 0;
            voucherDetail.FSoThuongUyQNCN = 0;
            voucherDetail.FSoTrungUyQNCN = 0;
            voucherDetail.FSoThieuUyQNCN = 0;
            voucherDetail.FSoVcqp = 0;
            voucherDetail.FSoCnvqp = 0;
            voucherDetail.FSoLdhd = 0;
            voucherDetail.FSoCcqp = 0;
            return voucherDetail;
        }

        private void LoadPropertyChange()
        {
            _propertiesChange = new List<string>
            {
                nameof(ArmyVoucherDetailModel.FSoThieuUy), nameof(ArmyVoucherDetailModel.FSoTrungUy), nameof(ArmyVoucherDetailModel.FSoThuongUy),
                nameof(ArmyVoucherDetailModel.FSoDaiUy), nameof(ArmyVoucherDetailModel.FSoThieuTa), nameof(ArmyVoucherDetailModel.FSoTrungTa),
                nameof(ArmyVoucherDetailModel.FSoThuongTa), nameof(ArmyVoucherDetailModel.FSoDaiTa), nameof(ArmyVoucherDetailModel.FSoTuong),
                nameof(ArmyVoucherDetailModel.FSoBinhNhi), nameof(ArmyVoucherDetailModel.FSoBinhNhat), nameof(ArmyVoucherDetailModel.FSoHaSi),
                nameof(ArmyVoucherDetailModel.FSoTrungSi), nameof(ArmyVoucherDetailModel.FSoThuongSi),
                nameof(ArmyVoucherDetailModel.FSoThuongTaQNCN),
                nameof(ArmyVoucherDetailModel.FSoTrungTaQNCN),
                nameof(ArmyVoucherDetailModel.FSoThieuTaQNCN),
                nameof(ArmyVoucherDetailModel.FSoDaiUyQNCN),
                nameof(ArmyVoucherDetailModel.FSoThuongUyQNCN),
                nameof(ArmyVoucherDetailModel.FSoTrungUyQNCN),
                nameof(ArmyVoucherDetailModel.FSoThieuUyQNCN),
                nameof(ArmyVoucherDetailModel.FSoVcqp), nameof(ArmyVoucherDetailModel.FSoCnvqp), nameof(ArmyVoucherDetailModel.FSoLdhd),
                nameof(ArmyVoucherDetailModel.FSoCcqp), nameof(ArmyVoucherDetailModel.SGhiChu)
            };
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
                case (int)ArmyPrintType.PRINT_ARMY:
                    PrintArmyViewModel.Init();
                    var view1 = new PrintArmy { DataContext = PrintArmyViewModel };
                    //show the dialog
                    DialogHost.Show(view1, "ArmyDetailDialog", null, null);
                    break;
            }
        }

        private void OnExportAggregateData(object param)
        {
            int dialogType = (int)param;
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    var currentDv = GetNsDonViOfCurrentUser();
                    string tenDv = currentDv != null ? currentDv.TenDonVi : "";

                    var agencyId = "";
                    var month = _selectedMonth.ValueItem;
                    switch (dialogType)
                    {
                        case (int)ArmyExportCheckType.EXPORT_AGGREGATE_TYPE:
                            agencyId = string.Join(",", _agencies.Where(x => !string.IsNullOrEmpty(x.ValueItem)).Select(x => x.ValueItem).ToArray());
                            break;
                        case (int)ArmyExportCheckType.EXPORT_UNITS_TYPE:
                            var listQuanSo = _chungTuChiTietService.FindForAgencyHasvalueReport(_sessionInfo.YearOfWork, null, month);
                            agencyId = string.Join(",", _agencies.Where(x => !string.IsNullOrEmpty(x.ValueItem) && listQuanSo.Contains(x.ValueItem)).Select(x => x.ValueItem).ToArray());
                            break;
                        case (int)ArmyExportCheckType.EXPORT_UNIT_TYPE:
                            agencyId = _selectedAgency.ValueItem;
                            currentDv = GetNsDonViByMaDonVi(agencyId);
                            tenDv = currentDv != null ? currentDv.TenDonVi : "";
                            break;
                    }
                    List<ExportResult> results = new List<ExportResult>();

                    if (dialogType == (int)ArmyExportCheckType.EXPORT_UNITS_TYPE)
                    {
                        var listAgencies = agencyId.Split(',');
                        foreach(var agency in listAgencies) { 
                            List<ReportQuanSoDonViQuery> listQuanSo = _chungTuChiTietService.FindForAgencyReport(_sessionInfo.YearOfWork, agency, month);
                            List<NsQsMucLuc> qsMucLuc = _mucLucService.FindByCondition(_sessionInfo.YearOfWork).OrderBy(x => x.SKyHieu).ToList();
                            RptQuanSoDonVi rptQuanSo = new RptQuanSoDonVi();
                            currentDv = GetNsDonViByMaDonVi(agency);
                            tenDv = currentDv != null ? currentDv.TenDonVi : "";
                            rptQuanSo.Truoc = listQuanSo.Where(x => x.XauNoiMa == "100").ToList();
                            rptQuanSo.Tang = listQuanSo.Where(x => x.XauNoiMa.StartsWith("2") && !x.XauNoiMa.Equals("2")).ToList();
                            rptQuanSo.Giam = listQuanSo.Where(x => x.XauNoiMa.StartsWith("3") && !x.XauNoiMa.Equals("3")).ToList();
                            rptQuanSo.BS1 = listQuanSo.Where(x => x.XauNoiMa == "500").ToList();
                            rptQuanSo.BS2 = listQuanSo.Where(x => x.XauNoiMa == "600").ToList();

                            rptQuanSo.Cap2 = _sessionInfo.TenDonVi;
                            rptQuanSo.Cap3 = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_THONGTRI_BANHANH).FirstOrDefault()?.SGiaTri;
                            rptQuanSo.TieuDe1 = "Tổng hợp quân số quyết toán";
                            rptQuanSo.TieuDe2 = string.Format("{0} năm {1}", _selectedMonth.ValueItem.Equals("0") ? "Đầu " : _selectedMonth.DisplayItem, _sessionInfo.YearOfWork);
                            rptQuanSo.TenDonVi = tenDv;
                            rptQuanSo.Ngay = StringUtils.CreateDateTimeString();
                            rptQuanSo.ThangQuy = "Tháng";

                            Dictionary<string, object> data = new Dictionary<string, object>();
                            foreach (var prop in rptQuanSo.GetType().GetProperties())
                            {
                                data.Add(prop.Name, prop.GetValue(rptQuanSo));
                            }
                            data.Add("MLQS", qsMucLuc);

                            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUANSO_DONVI_3);
                            string fileNamePrefix = string.Format("{0}_{1}", $"SL_QTQNS_T{month}", tenDv);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportQuanSoDonViQuery, NsQsMucLuc>(templateFileName, data);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    else
                    {
                        List<ReportQuanSoDonViQuery> listQuanSo = _chungTuChiTietService.FindForAgencyReport(_sessionInfo.YearOfWork, agencyId, month);
                        List<NsQsMucLuc> qsMucLuc = _mucLucService.FindByCondition(_sessionInfo.YearOfWork).OrderBy(x => x.SKyHieu).ToList();
                        RptQuanSoDonVi rptQuanSo = new RptQuanSoDonVi();
                        rptQuanSo.Truoc = listQuanSo.Where(x => x.XauNoiMa == "100").ToList();
                        rptQuanSo.Tang = listQuanSo.Where(x => x.XauNoiMa.StartsWith("2") && !x.XauNoiMa.Equals("2")).ToList();
                        rptQuanSo.Giam = listQuanSo.Where(x => x.XauNoiMa.StartsWith("3") && !x.XauNoiMa.Equals("3")).ToList();
                        rptQuanSo.BS1 = listQuanSo.Where(x => x.XauNoiMa == "500").ToList();
                        rptQuanSo.BS2 = listQuanSo.Where(x => x.XauNoiMa == "600").ToList();

                        rptQuanSo.Cap2 = _sessionInfo.TenDonVi;
                        rptQuanSo.Cap3 = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_THONGTRI_BANHANH).FirstOrDefault()?.SGiaTri;
                        rptQuanSo.TieuDe1 = "Tổng hợp quân số quyết toán";
                        rptQuanSo.TieuDe2 = string.Format("{0} năm {1}", _selectedMonth.ValueItem.Equals("0") ? "Đầu " : _selectedMonth.DisplayItem, _sessionInfo.YearOfWork);
                        rptQuanSo.TenDonVi = tenDv;
                        rptQuanSo.Ngay = StringUtils.CreateDateTimeString();
                        rptQuanSo.ThangQuy = "Tháng";

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        foreach (var prop in rptQuanSo.GetType().GetProperties())
                        {
                            data.Add(prop.Name, prop.GetValue(rptQuanSo));
                        }
                        data.Add("MLQS", qsMucLuc);

                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUANSO_DONVI_3);
                        string fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_NS_QUANSO_DONVI_3.Split(".").First(), tenDv);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ReportQuanSoDonViQuery, NsQsMucLuc>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            _exportService.OpenEncrypt(result, ExportType.EXCEL);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var currentIdDonVi = _sessionService.Current.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private DonVi GetNsDonViByMaDonVi(string iIDMaDonVi)
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == iIDMaDonVi);
            var nsDonVi = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonVi;
        }

        private void OnDeleteAll()
        {
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                _chungTuChiTietService.DeleteInputData(ArmyVoucher.Id, Convert.ToInt32(_selectedMonth.ValueItem), _selectedAgency.ValueItem);
                LoadArmyDetail();
                OnSaveData(true);
                MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void OnGetDataLuong()
        {
            GetDataQtQuanSoLuongViewModel.Name = "Lấy dữ liệu quyết toán quân số lương";
            GetDataQtQuanSoLuongViewModel.Description = "Lấy dữ liệu quyết toán quân số lương";
            GetDataQtQuanSoLuongViewModel.NsQsChungTuModel = ArmyVoucher;
            GetDataQtQuanSoLuongViewModel.IIdDonVi = (_agencies.Count > 1) ? _selectedAgency.ValueItem : _sessionInfo.IdDonVi;

            GetDataQtQuanSoLuongViewModel.Init();
            GetDataQtQuanSoLuongViewModel.SavedAction = obj =>
            {
                //this.LoadData();
                var lstCtLuong = (List<NsQsChungTuChiTiet>)obj;
                if (lstCtLuong != null && lstCtLuong.Count > 0)
                {
                    AddDataFromLuong(lstCtLuong);
                    IdChungTuLuong = lstCtLuong.FirstOrDefault().IIdQschungTu;
                }

            };
            var addView = new GetDataQtQuanSoLuong() { DataContext = GetDataQtQuanSoLuongViewModel };
            DialogHost.Show(addView, "ArmyDetailDialog");
        }

        public void AddDataFromLuong(List<NsQsChungTuChiTiet> lstDataLuong)
        {
            IsSetDataLuong = true;
            foreach (var it in _armyVoucherDetails)
            {
                var itLuong = lstDataLuong.FirstOrDefault(x => x.SKyHieu.Equals(it.SKyHieu) && !x.BHangCha);
                if (itLuong != null)
                {
                    it.FSoThieuUy = itLuong.FSoThieuUy;
                    it.FSoTrungUy = itLuong.FSoTrungUy;
                    it.FSoThuongUy = itLuong.FSoThuongUy;
                    it.FSoDaiUy = itLuong.FSoDaiUy;
                    it.FSoThieuTa = itLuong.FSoThieuTa;
                    it.FSoTrungTa = itLuong.FSoTrungTa;
                    it.FSoThuongTa = itLuong.FSoThuongTa;
                    it.FSoDaiTa = itLuong.FSoDaiTa;
                    it.FSoTuong = itLuong.FSoTuong;
                    it.FSoBinhNhi = itLuong.FSoBinhNhi;
                    it.FSoBinhNhat = itLuong.FSoBinhNhat;
                    it.FSoHaSi = itLuong.FSoHaSi;
                    it.FSoTrungSi = itLuong.FSoTrungSi;
                    it.FSoThuongSi = itLuong.FSoThuongSi;
                    it.FSoTsq = itLuong.FSoTsq;
                    it.FSoThuongTaQNCN = itLuong.FSoThuongTaQNCN;
                    it.FSoTrungTaQNCN = itLuong.FSoTrungTaQNCN;
                    it.FSoThieuTaQNCN = itLuong.FSoThieuTaQNCN;
                    it.FSoDaiUyQNCN = itLuong.FSoDaiUyQNCN;
                    it.FSoThuongUyQNCN = itLuong.FSoThuongUyQNCN;
                    it.FSoTrungUyQNCN = itLuong.FSoTrungUyQNCN;
                    it.FSoThieuUyQNCN = itLuong.FSoThieuUyQNCN;
                    it.FSoCnvqp = itLuong.FSoCnvqp;
                    it.FSoVcqp = itLuong.FSoVcqp.GetValueOrDefault();
                    it.FSoLdhd = itLuong.FSoLdhd;
                    it.FSoCcqp = itLuong.FSoCcqp ?? 0;
                }
            }
            IsSetDataLuong = false;
            CalculateData(ref _armyVoucherDetails);
            OnPropertyChanged(nameof(ArmyVoucherDetails));
        }

        public void OnRefresh()
        {
            if (IsSaveData)
            {
                var result = MessageBoxHelper.ConfirmCancel(Resources.ConfirmReloadData);
                if (result == MessageBoxResult.Cancel)
                    return;
                else if (result == MessageBoxResult.Yes)
                    OnSaveData();
            }
            LoadArmyDetail();
        }
    }
}
