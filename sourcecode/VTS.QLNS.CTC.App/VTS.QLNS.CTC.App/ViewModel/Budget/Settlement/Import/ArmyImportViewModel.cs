using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Import
{
    public class ArmyImportViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IImportExcelService _importService;
        private readonly INsQsChungTuService _chungTuService;
        private readonly INsQsChungTuChiTietService _chungTuChiTietService;
        private readonly INsQsMucLucService _mucLucNganSachService;
        private readonly IMapper _mapper;
        private List<NsQsMucLuc> _mucLucNganSachs;
        private SessionInfo _sessionInfo;

        private const string M1 = "1";
        private const string M2 = "2";
        private const string M3 = "3";
        private const string M4 = "4";
        private const string M5 = "5";
        private const string M6 = "6";
        private const string M7 = "7";
        public override string Name => "Quyết toán quân số";
        public override Type ContentType => typeof(View.Budget.Settlement.Import.ArmyImport);
        public override string Description => "Chứng từ quyết toán quân số";
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public int VoucherNoIndex;

        private ObservableCollection<ImportErrorItem> _importErrors;
        public ObservableCollection<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private ObservableCollection<ArmyDetailImportModel> _armyVoucherDetails;
        public ObservableCollection<ArmyDetailImportModel> ArmyVoucherDetails
        {
            get => _armyVoucherDetails;
            set => SetProperty(ref _armyVoucherDetails, value);
        }

        private bool OnlyHasRoot { get; set; }


        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set => SetProperty(ref _monthSelected, value);
        }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private ComboboxItem _agencySelected;
        public ComboboxItem AgencySelected
        {
            get => _agencySelected;
            set => SetProperty(ref _agencySelected, value);
        }

        private DateTime _voucherDate;
        public DateTime VoucherDate
        {
            get => _voucherDate;
            set => SetProperty(ref _voucherDate, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (ArmyVoucherDetails.Count > 0)
                    return !ArmyVoucherDetails.Any(x => !x.ImportStatus);
                return false;
            }
        }

        private bool IsUpdateYearBegin { get; set; }
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }

        public ArmyImportViewModel(ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            IImportExcelService importService,
            INsQsChungTuService chungTuService,
            INsQsChungTuChiTietService chungTuChiTietService,
            INsQsMucLucService mucLucNganSachService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _importService = importService;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _mucLucNganSachService = mucLucNganSachService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _voucherDate = DateTime.Now;
            LoadMonths();
            LoadAgencies();
            OnResetData();
        }

        private void LoadMonths()
        {
            ComboboxItem dauNam = new ComboboxItem("Đầu năm", "0");
            _months = new List<ComboboxItem>();
            _months.Add(dauNam);
            _months.AddRange(FnCommonUtils.LoadMonths());
        }

        private void OnResetData()
        {
            var namLamViec = _sessionInfo.YearOfWork;
            _fileName = string.Empty;
            _monthSelected = null;
            _agencySelected = null;
            _mucLucNganSachs = _mucLucNganSachService.FindByCondition(namLamViec).OrderBy(x => x.SKyHieu).ToList();

            _armyVoucherDetails = new ObservableCollection<ArmyDetailImportModel>();
            _importErrors = new ObservableCollection<ImportErrorItem>();

            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(MonthSelected));
            OnPropertyChanged(nameof(AgencySelected));
            OnPropertyChanged(nameof(ArmyVoucherDetails));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
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
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file excel");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xlsx";
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FileName = openFileDialog.FileName;
        }

        private void OnProcessFile()
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(FileName))
            {
                message = Resources.ErrorFileEmpty;
                goto ShowError;
            }
            if (_monthSelected == null)
            {
                message = Resources.ErrorMonthEmpty;
                goto ShowError;
            }
            if (_agencySelected == null)
            {
                message = Resources.ErrorAgencyEmpty;
                goto ShowError;
            }
        ShowError:
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Warning(message);
                return;
            }

            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();

                //xử lý chứng từ chi tiết
                ImportResult<ArmyDetailImportModel> _voucherDetailResult = _importService.ProcessData<ArmyDetailImportModel>(FileName);
                _armyVoucherDetails = new ObservableCollection<ArmyDetailImportModel>(_voucherDetailResult.Data);
                OnPropertyChanged(nameof(ArmyVoucherDetails));
                if (_voucherDetailResult.ImportErrors.Count > 0)
                    errors.AddRange(_voucherDetailResult.ImportErrors);

                if (errors.Count > 0)
                {
                    _importErrors = new ObservableCollection<ImportErrorItem>(errors);
                    OnPropertyChanged(nameof(ImportErrors));
                    MessageBoxHelper.Warning(Resources.AlertDataError);
                }
                OnPropertyChanged(nameof(IsSaveData));
            }
            catch
            {
                MessageBoxHelper.Warning(Resources.ErrorImport);
            }
        }
        // hàm này lấy bên detail
        private void CalculateData(ref List<ArmyVoucherDetailModel> armyVoucherDetail)
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
            List<ArmyVoucherDetailModel> voucherDetailsM2 = armyVoucherDetail.Where(x => x.SM == M2).ToList();
            List<ArmyVoucherDetailModel> voucherDetailsM3 = armyVoucherDetail.Where(x => x.SM == M3).ToList();
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
        }

        // hàm này lấy bên detail
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
            return voucherDetail;
        }

        private Core.Domain.Criteria.ArmyVoucherDetailCriteria LoadCondition(ArmyVoucherModel armyVoucher, string agencyId, int yearOfWork)
        {
            Utility.Enum.ArmyVoucherDetailMethod method = Utility.Enum.ArmyVoucherDetailMethod.GET_PART;
            if (string.IsNullOrEmpty(agencyId))
            {
                if (string.IsNullOrEmpty(_agencySelected.ValueItem))
                {
                    if (OnlyHasRoot)
                    {
                        List<DonVi> listDonVi = _donViService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork && x.ITrangThai == Utility.Enum.StatusType.ACTIVE).ToList();
                        var agencyRoot = listDonVi.FirstOrDefault(x => x.Loai == LoaiDonVi.ROOT);
                        agencyId = agencyRoot.IIDMaDonVi.ToString();
                    }
                    else
                    {
                        agencyId = string.Join(",", _agencies.Where(x => !string.IsNullOrEmpty(x.ValueItem)).Select(x => x.ValueItem).ToArray());
                        method = Utility.Enum.ArmyVoucherDetailMethod.GET_ALL;
                    }
                }
                else
                    agencyId = _agencySelected.ValueItem;
            }
            return new Core.Domain.Criteria.ArmyVoucherDetailCriteria
            {
                YearOfWork = yearOfWork,
                VoucherId = armyVoucher.Id.ToString(),
                AgencyId = agencyId,
                Method = method
            };
        }

        private void ImportDataAndCalculate(List<NsQsChungTuChiTiet> chungTuChiTiets, NsQsChungTu chungTu, string thang, string donVi, List<NsQsChungTuChiTiet> chungTuChiTietDauNam = null)
        {
            var _listMonth = _chungTuService.FindMonthOfArmy(_sessionInfo.YearOfWork);

            var monthLast = chungTu.IThangQuy;

            while (_listMonth.Contains(monthLast))
            {
                monthLast++;
            }
            if (chungTu.IThangQuy == 0)
            {
                if ((!IsUpdateYearBegin && chungTuChiTietDauNam != null))
                {
                    chungTuChiTietDauNam = chungTuChiTietDauNam.IsEmpty() ? new List<NsQsChungTuChiTiet>() : chungTuChiTietDauNam;
                    var namLamViec = _sessionInfo.YearOfWork;
                    var chungTuUpdates = chungTuChiTiets.IsEmpty() ? new List<NsQsChungTuChiTiet>() : chungTuChiTiets.Where(w => chungTuChiTietDauNam.Select(s => s.SKyHieu).Contains(w.SKyHieu)).ToList();
                    var chungtuAdds = chungTuChiTiets.Except(chungTuUpdates).ToList();
                    foreach (var itemUpdate in chungTuUpdates)
                    {

                        itemUpdate.INamLamViec = namLamViec;
                        itemUpdate.DNgaySua = DateTime.Now;
                        NsQsChungTuChiTiet chungTuChiTiet = chungTuChiTietDauNam.FirstOrDefault(s => s.SKyHieu == itemUpdate.SKyHieu);
                        //_mapper.Map(itemUpdate, chungTuChiTiet);

                        foreach (var props in itemUpdate.GetType().GetProperties())
                        {
                            if (props.Name.StartsWith("F") || props.Name == "DNgaySua")
                            {
                                props.SetValue(chungTuChiTiet, props.GetValue(itemUpdate));

                            }
                        }
                        _chungTuChiTietService.Update(chungTuChiTiet);
                    }

                    // chung tu them moi neu 
                    foreach (var itemAdd in chungtuAdds)
                    {
                        var mucLucNs = _mucLucNganSachs.Where(x => x.SKyHieu == itemAdd.SKyHieu).FirstOrDefault();
                        itemAdd.Id = Guid.NewGuid();
                        itemAdd.IIdQschungTu = chungTu.Id;
                        itemAdd.IIdMlns = mucLucNs == null ? Guid.Empty : mucLucNs.IIdMlns;
                        itemAdd.IIdMlnsCha = mucLucNs == null ? Guid.Empty : mucLucNs.IIdMlnsCha;
                        itemAdd.BHangCha = mucLucNs == null ? false : mucLucNs.BHangCha;
                        itemAdd.IThangQuy = Convert.ToInt32(thang);
                        itemAdd.IIdMaDonVi = _agencySelected.ValueItem;
                        itemAdd.INamLamViec = _sessionInfo.YearOfWork;
                        itemAdd.SNguoiTao = _sessionInfo.Principal;
                        itemAdd.DNgayTao = DateTime.Now;
                    }

                    _chungTuChiTietService.AddRange(chungtuAdds);
                }

            }
            else
            {
                
                foreach (var item in chungTuChiTiets)
                {
                    var mucLucNs = _mucLucNganSachs.Where(x => x.SKyHieu == item.SKyHieu).FirstOrDefault();
                    item.Id = Guid.NewGuid();
                    item.IIdQschungTu = chungTu.Id;
                    item.IIdMlns = mucLucNs == null ? Guid.Empty : mucLucNs.IIdMlns;
                    item.IIdMlnsCha = mucLucNs == null ? Guid.Empty : mucLucNs.IIdMlnsCha;
                    item.BHangCha = mucLucNs == null ? false : mucLucNs.BHangCha;
                    item.IThangQuy = Convert.ToInt32(thang);
                    item.IIdMaDonVi = _agencySelected.ValueItem;
                    item.INamLamViec = _sessionInfo.YearOfWork;
                    item.SNguoiTao = _sessionInfo.Principal;
                    item.DNgayTao = DateTime.Now;
                }

                _chungTuChiTietService.AddRange(chungTuChiTiets);
            }

            var listChiTietNotCal = _chungTuChiTietService.FindByCondition(LoadCondition(_mapper.Map<ArmyVoucherModel>(chungTu), string.Empty, _sessionInfo.YearOfWork));

            var chungTuChiTietsArmyVoucher = _mapper.Map<List<ArmyVoucherDetailModel>>(listChiTietNotCal);
            //chungTuChiTietsArmyVoucher.Select(x => x.IsModified = true).ToList();
            CalculateData(ref chungTuChiTietsArmyVoucher);
            chungTuChiTietsArmyVoucher = chungTuChiTietsArmyVoucher.Where(x => x.Id != Guid.Empty).ToList();

            foreach (var item in chungTuChiTietsArmyVoucher)
            {
                item.IIdQschungTu = chungTu.Id;
                item.IThangQuy = int.Parse(thang);
                item.IIdMaDonVi = donVi;
                item.ITrangThai = (int)VTS.QLNS.CTC.Utility.Enum.Status.ACTIVE;
                item.INamLamViec = _sessionInfo.YearOfWork;
                item.SNguoiSua = _sessionInfo.Principal;
                item.DNgaySua = DateTime.Now;
                NsQsChungTuChiTiet chungTuChiTiet = _chungTuChiTietService.FindById(item.Id);
                _mapper.Map(item, chungTuChiTiet);
                _chungTuChiTietService.Update(chungTuChiTiet);
            }


            var dataM7Old_ = listChiTietNotCal.FirstOrDefault(x => x.SM == M7);
            var dataM7New_ = chungTuChiTietsArmyVoucher.FirstOrDefault(x => x.SM == M7);

            for (int i = chungTu.IThangQuy + 1; i < monthLast; i++)
            {
                var predicate_ = PredicateBuilder.True<NsQsChungTuChiTiet>();
                predicate_ = predicate_.And(x => x.INamLamViec == _sessionInfo.YearOfWork && x.IIdMaDonVi == donVi && x.IThangQuy == i);
                predicate_ = predicate_.And(x => x.SKyHieu == "100" || x.SKyHieu == "700");
                var listChiTiet = _chungTuChiTietService.FindByCondition(predicate_).ToList();
                foreach (var item in listChiTiet)
                {
                    item.FSoThieuUy += (dataM7New_.FSoThieuUy - dataM7Old_.FSoThieuUy.GetValueOrDefault());
                    item.FSoTrungUy += (dataM7New_.FSoTrungUy - dataM7Old_.FSoTrungUy.GetValueOrDefault());
                    item.FSoThuongUy += (dataM7New_.FSoThuongUy - dataM7Old_.FSoThuongUy.GetValueOrDefault());
                    item.FSoDaiUy += (dataM7New_.FSoDaiUy - dataM7Old_.FSoDaiUy.GetValueOrDefault());
                    item.FSoThieuTa += (dataM7New_.FSoThieuTa - dataM7Old_.FSoThieuTa.GetValueOrDefault());
                    item.FSoTrungTa += (dataM7New_.FSoTrungTa - dataM7Old_.FSoTrungTa.GetValueOrDefault());
                    item.FSoThuongTa += (dataM7New_.FSoThuongTa - dataM7Old_.FSoThuongTa.GetValueOrDefault());
                    item.FSoDaiTa += (dataM7New_.FSoDaiTa - dataM7Old_.FSoDaiTa.GetValueOrDefault());
                    item.FSoTuong += (dataM7New_.FSoTuong - dataM7Old_.FSoTuong.GetValueOrDefault());
                    item.FSoBinhNhi += (dataM7New_.FSoBinhNhi - dataM7Old_.FSoBinhNhi.GetValueOrDefault());
                    item.FSoBinhNhat += (dataM7New_.FSoBinhNhat - dataM7Old_.FSoBinhNhat.GetValueOrDefault());
                    item.FSoHaSi += (dataM7New_.FSoHaSi - dataM7Old_.FSoHaSi.GetValueOrDefault());
                    item.FSoTrungSi += (dataM7New_.FSoTrungSi - dataM7Old_.FSoTrungSi.GetValueOrDefault());
                    item.FSoThuongSi += (dataM7New_.FSoThuongSi - dataM7Old_.FSoThuongSi.GetValueOrDefault());
                    item.FSoThuongTaQNCN += (dataM7New_.FSoThuongTaQNCN - dataM7Old_.FSoThuongTaQNCN.GetValueOrDefault());
                    item.FSoTrungTaQNCN += (dataM7New_.FSoTrungTaQNCN - dataM7Old_.FSoTrungTaQNCN.GetValueOrDefault());
                    item.FSoThieuTaQNCN += (dataM7New_.FSoThieuTaQNCN - dataM7Old_.FSoThieuTaQNCN.GetValueOrDefault());
                    item.FSoDaiUyQNCN += (dataM7New_.FSoDaiUyQNCN - dataM7Old_.FSoDaiUyQNCN.GetValueOrDefault());
                    item.FSoThuongUyQNCN += (dataM7New_.FSoThuongUyQNCN - dataM7Old_.FSoThuongUyQNCN.GetValueOrDefault());
                    item.FSoTrungUyQNCN += (dataM7New_.FSoTrungUyQNCN - dataM7Old_.FSoTrungUyQNCN.GetValueOrDefault());
                    item.FSoThieuUyQNCN += (dataM7New_.FSoThieuUyQNCN - dataM7Old_.FSoThieuUyQNCN.GetValueOrDefault());
                    item.FSoVcqp += (dataM7New_.FSoVcqp - dataM7Old_.FSoVcqp.GetValueOrDefault());
                    item.FSoCnvqp += (dataM7New_.FSoCnvqp - dataM7Old_.FSoCnvqp.GetValueOrDefault());
                    item.FSoLdhd += (dataM7New_.FSoLdhd - dataM7Old_.FSoLdhd.GetValueOrDefault());
                    _chungTuChiTietService.Update(item);
                }
            }
        }

        private void OnSaveData()
        {
            List<NsQsChungTuChiTiet> chungTuChiTiets = _mapper.Map<List<NsQsChungTuChiTiet>>(_armyVoucherDetails.Where(x => x.ImportStatus));
            var chungTuCtM100Excel = chungTuChiTiets.FirstOrDefault(x => x.SKyHieu == "100");
            chungTuChiTiets = chungTuChiTiets.Where(x => x.SKyHieu != "100" && x.SKyHieu != "700" && !x.BHangCha).ToList();
            var thang = _monthSelected.ValueItem;
            var donVi = _agencySelected.ValueItem;
            var namLamViec = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<NsQsChungTu>();
            predicate = predicate.And(x => x.INamLamViec == namLamViec);
            predicate = predicate.And(x => x.ILoai == 0);
            predicate = predicate.And(x => x.IThangQuy == int.Parse(thang));
            var existsChungTu = _chungTuService.FindByCondition(predicate).FirstOrDefault();

            if (existsChungTu != null)
            {
                var predicateCt = PredicateBuilder.True<NsQsChungTuChiTiet>();
                predicateCt = predicateCt.And(x => x.IIdQschungTu == existsChungTu.Id);
                predicateCt = predicateCt.And(x => x.IIdMaDonVi == donVi);
                predicateCt = predicateCt.And(x => x.IThangQuy == int.Parse(thang));
                predicateCt = predicateCt.And(x => x.INamLamViec == namLamViec);
                var existsChungTuCT = _chungTuChiTietService.FindByCondition(predicateCt).ToList();
                existsChungTuCT = existsChungTuCT.Where(x => x.SKyHieu != "100" && x.SKyHieu != "700").ToList();
                if (existsChungTuCT.Count <= 0 || existsChungTuCT.All(x => !x.BHasData))
                {
                    ImportDataAndCalculate(chungTuChiTiets, existsChungTu, thang, donVi, existsChungTuCT);

                    ArmyVoucherModel armyVoucher = _mapper.Map<ArmyVoucherModel>(existsChungTu);
                    DialogHost.CloseDialogCommand.Execute(armyVoucher, null);
                    SavedAction?.Invoke(armyVoucher);
                }
                else
                {
                    MessageBoxHelper.Warning(string.Format(Resources.QsVoucherIsExists, _monthSelected.DisplayItem, _agencySelected.DisplayItem));
                    return;
                }
            }
            else
            {
                NsQsChungTu chungTu = new NsQsChungTu();
                chungTu.ISoChungTuIndex = VoucherNoIndex;
                chungTu.SSoChungTu = _chungTuService.GenerateVoucherNo(VoucherNoIndex);
                chungTu.DNgayChungTu = DateTime.Now;
                chungTu.SMoTa = "Chi tiết chứng từ";
                chungTu.ILoai = 0;
                chungTu.IThangQuy = int.Parse(thang);
                chungTu.INamLamViec = namLamViec;
                chungTu.DNgayTao = DateTime.Now;
                chungTu.SNguoiTao = _sessionInfo.Principal;
                _chungTuService.Add(chungTu);

                if (chungTu.IThangQuy > 0)
                {
                    _chungTuChiTietService.CreateDetail(chungTu.Id, _sessionInfo.YearOfWork, chungTu.IThangQuy, _sessionInfo.Principal);
                    _chungTuChiTietService.UpdateDetail(_sessionInfo.YearOfWork, chungTu.IThangQuy, string.Empty);

                }
                else
                {
                    MessageBoxResult dialogResult = MessageBoxHelper.Confirm(Resources.MessageConfirm);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        IsUpdateYearBegin = true;
                        var chungTuChiTietExcels = _mapper.Map<List<NsQsChungTuChiTiet>>(_armyVoucherDetails.Where(x => x.ImportStatus));
                        if (!chungTuChiTietExcels.IsEmpty())
                        {
                            foreach (var item in chungTuChiTietExcels)
                            {
                                var mucLucNs = _mucLucNganSachs.FirstOrDefault(x => x.SKyHieu == item.SKyHieu);
                                item.Id = Guid.NewGuid();
                                item.IIdQschungTu = chungTu.Id;
                                item.IIdMlns = mucLucNs == null ? Guid.Empty : mucLucNs.IIdMlns;
                                item.IIdMlnsCha = mucLucNs == null ? Guid.Empty : mucLucNs.IIdMlnsCha;
                                item.BHangCha = mucLucNs != null && mucLucNs.BHangCha;
                                item.IThangQuy = Convert.ToInt32(thang);
                                item.IIdMaDonVi = _agencySelected.ValueItem;
                                item.INamLamViec = _sessionInfo.YearOfWork;
                                item.SNguoiTao = _sessionInfo.Principal;
                                item.DNgayTao = DateTime.Now;
                            }

                            //var chungTuExcels = chungTuChiTietExcels.Where(x => !x.IIdMlns.IsNullOrEmpty()).ToList();
                            _chungTuChiTietService.AddRange(chungTuChiTietExcels);
                        }

                    }
                    else
                    {
                        IsUpdateYearBegin = false;
                        _chungTuChiTietService.CreateDetail(chungTu.Id, _sessionInfo.YearOfWork, chungTu.IThangQuy, _sessionInfo.Principal);
                        var dataUpdateOut = _chungTuChiTietService.UpdateDetailYearBegin(_sessionInfo.YearOfWork, string.Empty);
                    }

                    var _chungTuChiTiets = _chungTuChiTietService.FindByCondition(x => x.IIdQschungTu == chungTu.Id).ToList();
                    var _listMonth = _chungTuService.FindMonthOfArmy(_sessionInfo.YearOfWork);

                    var monthLast = chungTu.IThangQuy;

                    while (_listMonth.Contains(monthLast))
                    {
                        monthLast++;
                    }

                    if (_listMonth.Contains(chungTu.IThangQuy + 1))
                    {
                        var _chungTuChiTietsNext = _chungTuChiTietService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork && x.IThangQuy > chungTu.IThangQuy).ToList();
                        var dataM7New = _chungTuChiTiets.Where(x => x.SKyHieu == "700");
                        var dataM7Old = _chungTuChiTietsNext.Where(x => x.IThangQuy == (chungTu.IThangQuy + 1) && x.SKyHieu == "100").ToList();

                        for (int i = chungTu.IThangQuy + 1; i < monthLast; i++)
                        {
                            var listChiTiet = _chungTuChiTietService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork && x.IThangQuy == i).ToList();
                            listChiTiet = listChiTiet.Where(x => x.SKyHieu == "700" || x.SKyHieu == "100").ToList();
                            foreach (var item in listChiTiet)
                            {
                                var dataNew = dataM7New.FirstOrDefault(x => x.IIdMaDonVi == item.IIdMaDonVi);
                                var dataOld = dataM7Old.FirstOrDefault(x => x.IIdMaDonVi == item.IIdMaDonVi);
                                item.FSoThieuUy += (dataNew?.FSoThieuUy ?? 0 - dataOld?.FSoThieuUy ?? 0);
                                item.FSoTrungUy += (dataNew?.FSoTrungUy ?? 0 - dataOld?.FSoTrungUy ?? 0);
                                item.FSoThuongUy += (dataNew?.FSoThuongUy ?? 0 - dataOld?.FSoThuongUy ?? 0);
                                item.FSoDaiUy += (dataNew?.FSoDaiUy ?? 0 - dataOld?.FSoDaiUy ?? 0);
                                item.FSoThieuTa += (dataNew?.FSoThieuTa ?? 0 - dataOld?.FSoThieuTa ?? 0);
                                item.FSoTrungTa += (dataNew?.FSoTrungTa ?? 0 - dataOld?.FSoTrungTa ?? 0);
                                item.FSoThuongTa += (dataNew?.FSoThuongTa ?? 0 - dataOld?.FSoThuongTa ?? 0);
                                item.FSoDaiTa += (dataNew?.FSoDaiTa ?? 0 - dataOld?.FSoDaiTa ?? 0);
                                item.FSoTuong += (dataNew?.FSoTuong ?? 0 - dataOld?.FSoTuong ?? 0);
                                item.FSoBinhNhi += (dataNew?.FSoBinhNhi ?? 0 - dataOld?.FSoBinhNhi ?? 0);
                                item.FSoBinhNhat += (dataNew?.FSoBinhNhat ?? 0 - dataOld?.FSoBinhNhat ?? 0);
                                item.FSoHaSi += (dataNew?.FSoHaSi ?? 0 - dataOld?.FSoHaSi ?? 0);
                                item.FSoTrungSi += (dataNew?.FSoTrungSi ?? 0 - dataOld?.FSoTrungSi ?? 0);
                                item.FSoThuongSi += (dataNew?.FSoThuongSi ?? 0 - dataOld?.FSoThuongSi ?? 0);
                                item.FSoThuongTaQNCN += (dataNew?.FSoThuongTaQNCN ?? 0 - dataOld?.FSoThuongTaQNCN ?? 0);
                                item.FSoTrungTaQNCN += (dataNew?.FSoTrungTaQNCN ?? 0 - dataOld?.FSoTrungTaQNCN ?? 0);
                                item.FSoThieuTaQNCN += (dataNew?.FSoThieuTaQNCN ?? 0 - dataOld?.FSoThieuTaQNCN ?? 0);
                                item.FSoDaiUyQNCN += (dataNew?.FSoDaiUyQNCN ?? 0 - dataOld?.FSoDaiUyQNCN ?? 0);
                                item.FSoThuongUyQNCN += (dataNew?.FSoThuongUyQNCN ?? 0 - dataOld?.FSoThuongUyQNCN ?? 0);
                                item.FSoTrungUyQNCN += (dataNew?.FSoTrungUyQNCN ?? 0 - dataOld?.FSoTrungUyQNCN ?? 0);
                                item.FSoThieuUyQNCN += (dataNew?.FSoThieuUyQNCN ?? 0 - dataOld?.FSoThieuUyQNCN ?? 0);
                                item.FSoVcqp += (dataNew?.FSoVcqp ?? 0 - dataOld?.FSoVcqp ?? 0);
                                item.FSoCnvqp += (dataNew?.FSoCnvqp ?? 0 - dataOld?.FSoCnvqp ?? 0);
                                item.FSoLdhd += (dataNew?.FSoLdhd ?? 0 - dataOld?.FSoLdhd ?? 0);
                                _chungTuChiTietService.Update(item);
                            }
                        }
                    }

                    //ImportDataAndCalculate(chungTuChiTiets, chungTu, thang, donVi);

                    //ArmyVoucherModel armyVoucher = _mapper.Map<ArmyVoucherModel>(chungTu);
                    //DialogHost.CloseDialogCommand.Execute(armyVoucher, null);
                    //SavedAction?.Invoke(armyVoucher);
                }

                ImportDataAndCalculate(chungTuChiTiets, chungTu, thang, donVi);
                ArmyVoucherModel armyVoucher = _mapper.Map<ArmyVoucherModel>(chungTu);
                DialogHost.CloseDialogCommand.Execute(armyVoucher, null);
                SavedAction?.Invoke(armyVoucher);
            }
        }
    }
}
