using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Army
{
    public class ArmyDialogViewModel : ViewModelBase
    {
        private IMapper _mapper;
        private INsQsChungTuService _chungTuService;
        private INsQsChungTuChiTietService _chungTuChiTietService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private int _armyVoucherMonth;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler CloseWindow;
        public override string Name => "Quyết toán quân số - Thêm chứng từ";
        public override string Title => Id == Guid.Empty ? "Thêm chứng từ" : "Sửa chứng từ";
        public override string Description => Id == Guid.Empty ? "Quyết toán quân số - Thêm chứng từ" : "Quyết toán quân số - Sửa chứng từ";
        public override Type ContentType => typeof(View.Budget.Settlement.Army.ArmyDialog);

        private ArmyVoucherModel _armyVoucher;
        public ArmyVoucherModel ArmyVoucher
        {
            get => _armyVoucher;
            set => SetProperty(ref _armyVoucher, value);
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
            set
            {
                SetProperty(ref _monthSelected, value);
            }
        }

        public bool IsEdit => Id == Guid.Empty ? false : true;

        public Guid Id;
        public int VoucherNoIndex;
        public RelayCommand SaveCommand { get; }
        public ArmyDialogViewModel(IMapper mapper,
            INsQsChungTuService chungTuService,
            INsQsChungTuChiTietService chungTuChiTietService,
            ISessionService sessionService)
        {
            _mapper = mapper;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;

            SaveCommand = new RelayCommand(obj => SaveVoucher((ArmyVoucherModel)obj));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            SetArmyVoucherData();
            LoadMonths();
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            _months.Add(new ComboboxItem("Đầu năm", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i, i.ToString());
                _months.Add(month);
            }
            if (ArmyVoucher.IThangQuy == 0 && ArmyVoucher.Id.Equals(Guid.Empty))
            {
                _monthSelected = _months.Where(x => x.ValueItem == DateTime.Now.Month.ToString()).First();
                ArmyVoucher.IThangQuy = Convert.ToInt32(_monthSelected.ValueItem);
            }
            else
            {
                MonthSelected = _months.Where(x => x.ValueItem == ArmyVoucher.IThangQuy.ToString()).First();
                ArmyVoucher.IThangQuy = Convert.ToInt32(_monthSelected.ValueItem);
            }
        }

        private void SetArmyVoucherData()
        {
            //trường hợp tạo mới
            if (Id == Guid.Empty)
            {
                string voucherNo = _chungTuService.GenerateVoucherNo(VoucherNoIndex);
                ArmyVoucher = new ArmyVoucherModel();
                ArmyVoucher.SSoChungTu = voucherNo;
                ArmyVoucher.ISoChungTuIndex = VoucherNoIndex;
                ArmyVoucher.DNgayChungTu = DateTime.Now;
                ArmyVoucher.DNgayQuyetDinh = DateTime.Now;
                _armyVoucherMonth = 0;
            }
            else
            {
                NsQsChungTu chungTu = _chungTuService.FindById(Id);
                ArmyVoucher = _mapper.Map<ArmyVoucherModel>(chungTu);
                _armyVoucherMonth = ArmyVoucher.IThangQuy;
            }
        }

        private void SaveVoucher(ArmyVoucherModel settlementVoucher)
        {
            int monthSelected = MonthSelected == null ? 0 : Convert.ToInt32(MonthSelected.ValueItem);
            if (_armyVoucherMonth == 0 || (_armyVoucherMonth != 0 && _armyVoucherMonth != monthSelected))
            {
                string message = CheckMonthOfArmyVoucher(monthSelected);
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBoxHelper.Warning(message);
                    return;
                }
            }
            ArmyVoucher.IThangQuy = monthSelected;

            NsQsChungTu chungTu = new NsQsChungTu();
            //trường hợp tạo mới
            if (Id == Guid.Empty)
            {
                ArmyVoucher.Id = Guid.NewGuid();
                ArmyVoucher.INamLamViec = _sessionInfo.YearOfWork;
                ArmyVoucher.ITrangThai = (int)Status.ACTIVE;
                ArmyVoucher.SNguoiTao = _sessionInfo.Principal;
                ArmyVoucher.DNgayTao = DateTime.Now;
                _mapper.Map(settlementVoucher, chungTu);
                _chungTuService.Add(chungTu);

                // tạo mới chứng từ chi tiết
                _chungTuChiTietService.CreateDetail(ArmyVoucher.Id, _sessionInfo.YearOfWork, monthSelected, _sessionInfo.Principal);
                if (monthSelected > 0)
                {
                    _chungTuChiTietService.UpdateDetail(_sessionInfo.YearOfWork, monthSelected, string.Empty);
                }
                else
                {
                    _chungTuChiTietService.UpdateDetailYearBegin(_sessionInfo.YearOfWork, string.Empty);
                }

                var _chungTuChiTiets = _chungTuChiTietService.FindByCondition(x => x.IIdQschungTu == ArmyVoucher.Id).ToList();
                var _listMonth = _chungTuService.FindMonthOfArmy(_sessionInfo.YearOfWork);

                var monthLast = monthSelected;

                while (_listMonth.Contains(monthLast))
                {
                    monthLast++;
                }


                if (_listMonth.Contains(monthSelected + 1))
                {
                    var _chungTuChiTietsNext = _chungTuChiTietService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork && x.IThangQuy > monthSelected).ToList();
                    var dataM7New = _chungTuChiTiets.Where(x => x.SKyHieu == "700");
                    var dataM7Old = _chungTuChiTietsNext.Where(x => x.IThangQuy == (monthSelected + 1) && x.SKyHieu == "100").ToList();

                    for (int i = monthSelected + 1; i < monthLast; i++)
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
                            item.FSoCnvqp += (dataNew?.FSoCnvqp ?? 0 - dataOld?.FSoCnvqp ?? 0);
                            item.FSoCcqp += (dataNew?.FSoCcqp ?? 0 - dataOld?.FSoCcqp ?? 0);
                            item.FSoLdhd += (dataNew?.FSoLdhd ?? 0 - dataOld?.FSoLdhd ?? 0);
                            _chungTuChiTietService.Update(item);
                        }
                    }
                }
            }
            else
            {
                ArmyVoucher.SNguoiSua = _sessionInfo.Principal;
                ArmyVoucher.DNgaySua = DateTime.Now;
                chungTu = _chungTuService.FindById(settlementVoucher.Id);
                _mapper.Map(settlementVoucher, chungTu);
                _chungTuService.Update(chungTu);
            }
            DialogHost.CloseDialogCommand.Execute(ArmyVoucher, null);
            SavedAction?.Invoke(_mapper.Map<ArmyVoucherModel>(chungTu));
        }

        private string CheckMonthOfArmyVoucher(int monthSelected)
        {
            string message = string.Empty;
            List<int> monthOfArmy = _chungTuService.FindMonthOfArmy(_sessionInfo.YearOfWork);
            string monthStr = string.Empty;
            if (monthSelected == 0)
                monthStr = "đầu năm";
            else monthStr = "tháng " + monthSelected;
            if (monthOfArmy != null)
            {
                if (monthOfArmy.Contains(monthSelected))
                    message = "Đã tồn tại chứng từ cho " + monthStr + ", đ/c vui lòng kiểm tra lại hoặc chọn tháng khác!";
                /*
                else
                {
                    if (monthOfArmy.Count > 0)
                    {
                        int lastOfMonth = monthOfArmy.Last();
                        if (monthSelected - lastOfMonth - 1 > 0)
                            message = "Chưa tạo chứng từ quân số của tháng " + (lastOfMonth + 1) + ", đ/c vui lòng tạo cho tháng " + (lastOfMonth + 1);
                    }
                    else
                    {
                        if (monthSelected > 0)
                            message = "Chưa tạo chứng từ quân số đầu năm, đ/c vui lòng tạo cho đầu năm";
                    }
                }
                */
            }
            return message;
        }
    }
}
