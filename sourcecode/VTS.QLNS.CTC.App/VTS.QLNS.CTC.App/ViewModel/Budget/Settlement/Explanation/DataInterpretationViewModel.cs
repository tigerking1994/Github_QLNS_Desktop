using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Explanation
{
    public class DataInterpretationViewModel : ViewModelBase
    {
        private IMapper _mapper;
        private INsQtChungTuChiTietService _chungTuChiTietService;
        private INsMucLucNganSachService _iNsMucLucNganSachService;
        private INsQtChungTuChiTietGiaiThichService _chungTuChiTietGiaiThichService;
        private INsQtChungTuChiTietGiaiThichLuongTruService _chungTuChiTietGiaiThichLuongTruService;
        private INsQtChungTuService _chungTuService;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;

        public override string Name => "Quyết toán - Chứng từ chi tiết - Giải thích số liệu";
        public override Type ContentType => typeof(View.Budget.Settlement.Explanation.DataInterpretation);

        public SettlementVoucherModel SettlementVoucher;
        public List<SettlementVoucherDetailModel> SettlementVoucherDetails;
        public string ExplainId;
        public string AgencyId;
        public int QuarterMonth;
        public int QuarterMonthType;

        public bool IsEnableSelfPayTab { get; set; }

        private SettlementVoucherDetailExplainModel _settlementVoucherDetailExplain;
        public SettlementVoucherDetailExplainModel SettlementVoucherDetailExplain
        {
            get => _settlementVoucherDetailExplain;
            set => SetProperty(ref _settlementVoucherDetailExplain, value);
        }

        private ObservableCollection<SettlementVoucherDetailExplainSubtractModel> _explainSubtracts;
        public ObservableCollection<SettlementVoucherDetailExplainSubtractModel> ExplainSubtracts
        {
            get => _explainSubtracts;
            set => SetProperty(ref _explainSubtracts, value);
        }

        private SettlementVoucherDetailExplainSubtractModel _selectedExplainSubtract;
        public SettlementVoucherDetailExplainSubtractModel SelectedExplainSubtract
        {
            get => _selectedExplainSubtract;
            set => SetProperty(ref _selectedExplainSubtract, value);
        }

        private int _selectedTab;
        public int SelectedTab
        {
            get => _selectedTab;
            set
            {
                SetProperty(ref _selectedTab, value);
            }
        }

        private ObservableCollection<MilitaryObjectModel> _militaryObjects;
        public ObservableCollection<MilitaryObjectModel> MilitaryObjects
        {
            get => _militaryObjects;
            set => SetProperty(ref _militaryObjects, value);
        }

        private MilitaryObjectModel _selectedMilitaryObject;
        public MilitaryObjectModel SelectedMilitaryObject
        {
            get => _selectedMilitaryObject;
            set => SetProperty(ref _selectedMilitaryObject, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (_explainSubtracts != null)
                    return _explainSubtracts.Any(item => item.IsModified || item.IsDeleted);
                return false;
            }
        }

        public bool IsSaveAndUpdateData
        {
            get
            {
                if (_explainSubtracts != null)
                    return _explainSubtracts.Any(item => item.IsModified || item.IsDeleted);
                return false;
            }
        }


        public bool IsDeleteAll => _explainSubtracts != null && _explainSubtracts.Any(item => !item.IsModified && item.HasData);

        private SumExplainSubtractModel _sumExplainSubtract;
        public SumExplainSubtractModel SumExplainSubtract
        {
            get => _sumExplainSubtract;
            set => SetProperty(ref _sumExplainSubtract, value);
        }

        public RelayCommand SaveAndUpdateCommand { get; }
        public RelayCommand AddRowCommand { get; }
        public RelayCommand DeleteRowCommand { get; }
        public RelayCommand ReloadDataCommand { get; }
        public RelayCommand DeleteAllCommand { get; }

        public DataInterpretationViewModel(IMapper mapper,
            INsQtChungTuChiTietService chungTuChiTietService,
            INsQtChungTuChiTietGiaiThichService chungTuChiTietGiaiThichService,
            INsQtChungTuChiTietGiaiThichLuongTruService chungTuChiTietGiaiThichLuongTruService,
            INsMucLucNganSachService iNsMucLucNganSachService,
            INsQtChungTuService chungTuService,
            ISessionService sessionService)
        {
            _mapper = mapper;
            _chungTuChiTietService = chungTuChiTietService;
            _chungTuChiTietGiaiThichService = chungTuChiTietGiaiThichService;
            _chungTuChiTietGiaiThichLuongTruService = chungTuChiTietGiaiThichLuongTruService;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            _chungTuService = chungTuService;
            _sessionService = sessionService;

            AddRowCommand = new RelayCommand(obj => OnAddRow());
            DeleteRowCommand = new RelayCommand(obj => OnDeleteRow());
            ReloadDataCommand = new RelayCommand(obj => OnReloadData());
            DeleteAllCommand = new RelayCommand(obj => OnDeleteAll());
            SaveAndUpdateCommand = new RelayCommand(obj => OnSaveAndUpdate());
        }

        public override void Init()
        {
            base.Init();
            _selectedTab = 0;
            _sessionInfo = _sessionService.Current;
            LoadExplainSubtractData();
            LoadExplainData();
            LoadMilitaryObjects();
        }

        //load dữ liệu giải thích số
        private void LoadExplainData()
        {
            NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = new NsQtChungTuChiTietGiaiThich();

            if (!string.IsNullOrEmpty(SettlementVoucher.STongHop))
            {
                var listChildVoucher = _chungTuService.FindByCondition(n => n.INamLamViec == SettlementVoucher.INamLamViec
                                                                     && n.INamNganSach == SettlementVoucher.INamNganSach
                                                                     && n.IIdMaNguonNganSach == SettlementVoucher.IIdMaNguonNganSach
                                                                     && SettlementVoucher.STongHop.Contains(n.SSoChungTu)).ToList();

                List<NsQtChungTuChiTietGiaiThich> listChungTuChiTietGiaiThichChild = new List<NsQtChungTuChiTietGiaiThich>();
                foreach (var item in listChildVoucher)
                {
                    SettlementVoucherDetailExplainCriteria conditionChild = new SettlementVoucherDetailExplainCriteria
                    {
                        VoucherId = item == null ? Guid.Empty : item.Id,
                        ExplainId = item == null ? ExplainId : item.Id.ToString(),
                        AgencyId = item == null ? AgencyId : item.IIdMaDonVi,
                        YearOfWork = _sessionInfo.YearOfWork
                    };
                    var chungTuChiTietGiaiThichChild = _chungTuChiTietGiaiThichService.FindByCondition(conditionChild);
                    if (chungTuChiTietGiaiThichChild != null)
                    {
                        listChungTuChiTietGiaiThichChild.Add(chungTuChiTietGiaiThichChild);
                    }
                }

                chungTuChiTietGiaiThich = new NsQtChungTuChiTietGiaiThich()
                {
                    FLuongSiQuan = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongSiQuan),
                    FLuongSiQuanTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongSiQuanTru),
                    FLuongSiQuanQt = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongSiQuanQt),
                    FLuongQncn = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongQncn),
                    FLuongQncnTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongQncnTru),
                    FLuongQncnQt = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongQncnQt),
                    FLuongCnvqp = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongCnvqp),
                    FLuongCnvqpTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongCnvqpTru),
                    FLuongCnvqpQt = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongCnvqpQt),
                    FLuongHd = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongHd),
                    FLuongHdTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongHdTru),
                    FLuongHdQt = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongHdQt),
                    FPhuCapSiQuan = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapSiQuan),
                    FPhuCapSiQuanTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapSiQuanTru),
                    FPhuCapSiQuanQt = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapSiQuanQt),
                    FPhuCapQncn = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapQncn),
                    FPhuCapQncnTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapQncnTru),
                    FPhuCapQncnQt = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapQncnQt),
                    FPhuCapCnvqp = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapCnvqp),
                    FPhuCapCnvqpTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapCnvqpTru),
                    FPhuCapCnvqpQt = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapCnvqpQt),
                    FPhuCapHd = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapHd),
                    FPhuCapHdTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapHdTru),
                    FPhuCapHdQt = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapHdQt),
                    FNgayAn = listChungTuChiTietGiaiThichChild.Sum(n => n.FNgayAn),
                    FNgayAnCong = listChungTuChiTietGiaiThichChild.Sum(n => n.FNgayAnCong),
                    FNgayAnTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FNgayAnTru),
                    FNgayAnQt = listChungTuChiTietGiaiThichChild.Sum(n => n.FNgayAnQt),
                    FRaQuanSiQuanNguoiXuatNgu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanSiQuanNguoiXuatNgu),
                    FRaQuanSiQuanTienXuatNgu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanSiQuanTienXuatNgu),
                    FRaQuanSiQuanNguoiHuu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanSiQuanNguoiHuu),
                    FRaQuanSiQuanTienHuu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanSiQuanTienHuu),
                    FRaQuanSiQuanNguoiThoiViec = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanSiQuanNguoiThoiViec),
                    FRaQuanSiQuanTienThoiViec = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanSiQuanTienThoiViec),
                    FRaQuanQncnNguoiXuatNgu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanQncnNguoiXuatNgu),
                    FRaQuanQncnTienXuatNgu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanQncnTienXuatNgu),
                    FRaQuanQncnNguoiHuu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanQncnNguoiHuu),
                    FRaQuanQncnTienHuu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanQncnTienHuu),
                    FRaQuanQncnNguoiThoiViec = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanQncnNguoiThoiViec),
                    FRaQuanQncnTienThoiViec = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanQncnTienThoiViec),
                    FRaQuanCnvqpNguoiXuatNgu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanCnvqpNguoiXuatNgu),
                    FRaQuanCnvqpTienXuatNgu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanCnvqpTienXuatNgu),
                    FRaQuanCnvqpNguoiHuu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanCnvqpNguoiHuu),
                    FRaQuanCnvqpTienHuu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanCnvqpTienHuu),
                    FRaQuanCnvqpNguoiThoiViec = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanCnvqpNguoiThoiViec),
                    FRaQuanCnvqpTienThoiViec = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanCnvqpTienThoiViec),
                    FRaQuanHsqcsNguoiXuatNgu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanHsqcsNguoiXuatNgu),
                    FRaQuanHsqcsTienXuatNgu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanHsqcsTienXuatNgu),
                    FRaQuanHsqcsNguoiHuu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanHsqcsNguoiHuu),
                    FRaQuanHsqcsTienHuu = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanHsqcsTienHuu),
                    FRaQuanHsqcsNguoiThoiViec = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanHsqcsNguoiThoiViec),
                    FRaQuanHsqcsTienThoiViec = listChungTuChiTietGiaiThichChild.Sum(n => n.FRaQuanHsqcsTienThoiViec),
                    FLuongBhxhSiQuanTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongBhxhSiQuanTru),
                    FLuongBhxhCnvqpTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongBhxhCnvqpTru),
                    FLuongBhxhHdTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongBhxhHdTru),
                    FLuongBhxhQncnTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FLuongBhxhQncnTru),
                    FPhuCapBhxhSiQuanTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapBhxhSiQuanTru),
                    FPhuCapBhxhQncnTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapBhxhQncnTru),
                    FPhuCapBhxhCnvqpTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapBhxhCnvqpTru),
                    FPhuCapBhxhHdTru = listChungTuChiTietGiaiThichChild.Sum(n => n.FPhuCapBhxhHdTru),
                    FKinhPhiLuongPcKhac = listChungTuChiTietGiaiThichChild.Sum(n => n.FKinhPhiLuongPcKhac),
                    FKinhPhiPhuCapHsqbs = listChungTuChiTietGiaiThichChild.Sum(n => n.FKinhPhiPhuCapHsqbs),
                    FKinhPhiAn = listChungTuChiTietGiaiThichChild.Sum(n => n.FKinhPhiAn),
                };

            }
            else
            {
                var condition = new SettlementVoucherDetailExplainCriteria
                {
                    VoucherId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id,
                    ExplainId = SettlementVoucher == null ? ExplainId : SettlementVoucher.Id.ToString(),
                    AgencyId = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIdMaDonVi,
                    YearOfWork = _sessionInfo.YearOfWork
                };
                chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindByCondition(condition);
            }


            if (chungTuChiTietGiaiThich != null)
            {
                _settlementVoucherDetailExplain = _mapper.Map<SettlementVoucherDetailExplainModel>(chungTuChiTietGiaiThich);
            }
            else
            {
                _settlementVoucherDetailExplain = new SettlementVoucherDetailExplainModel();
            }

            if (SettlementVoucherDetails == null)
            {
                SettlementVoucherDetailSearch detailCondition = new SettlementVoucherDetailSearch
                {
                    YearOfWork = _sessionInfo.YearOfWork,
                    QuarterMonth = QuarterMonth.ToString(),
                    QuarterMonthType = QuarterMonthType
                };
                List<NsQtChungTuChiTiet> chungTuChiTiet = _chungTuChiTietService.FindBySummaryReport(detailCondition);
                SettlementVoucherDetails = _mapper.Map<List<SettlementVoucherDetailModel>>(chungTuChiTiet);
            }

            //tính tiền lương của những ngày không hưởng lương (lương trừ)
            //trường hợp không được gọi từ màn hình chi tiết chứng từ thì không tính lương trừ
            if (SettlementVoucher != null)
                CalculateSubtractSalary();

            //tính tiền lương xin quyết toán tháng này
            CalculateSettlementSalary();
        }

        private void CalculateSubtractSalaryUpdate()
        {
            _settlementVoucherDetailExplain.FLuongBhxhSiQuanTru = SumLuongFromExplainSubtract(new List<string> { "31", "71" });
            _settlementVoucherDetailExplain.FPhuCapBhxhSiQuanTru = SumPhuCapFromExplainSubstract(new List<string> { "31", "71" });

            _settlementVoucherDetailExplain.FLuongBhxhQncnTru = SumLuongFromExplainSubtract(new List<string> { "32", "72" });
            _settlementVoucherDetailExplain.FPhuCapBhxhQncnTru = SumPhuCapFromExplainSubstract(new List<string> { "32", "72" });

            _settlementVoucherDetailExplain.FLuongBhxhCnvqpTru = SumLuongFromExplainSubtract(new List<string> { "33", "73" });
            _settlementVoucherDetailExplain.FPhuCapBhxhCnvqpTru = SumPhuCapFromExplainSubstract(new List<string> { "33", "73" });

            _settlementVoucherDetailExplain.FLuongBhxhHdTru = SumLuongFromExplainSubtract(new List<string> { "34", "74" });
            _settlementVoucherDetailExplain.FPhuCapBhxhHdTru = SumPhuCapFromExplainSubstract(new List<string> { "34", "74" });
        }


        /// <summary>
        /// tính tiền lương của những ngày không hưởng lương (lương trừ)
        /// </summary>
        private void CalculateSubtractSalary()
        {
            _settlementVoucherDetailExplain.FLuongBhxhSiQuanTru = _settlementVoucherDetailExplain.FLuongBhxhSiQuanTru > 0
                                            ? _settlementVoucherDetailExplain.FLuongBhxhSiQuanTru
                                            : SumLuongFromExplainSubtract(new List<string> { "31", "71" });

            _settlementVoucherDetailExplain.FPhuCapBhxhSiQuanTru = _settlementVoucherDetailExplain.FPhuCapBhxhSiQuanTru > 0
                                            ? _settlementVoucherDetailExplain.FPhuCapBhxhSiQuanTru
                                            : SumPhuCapFromExplainSubstract(new List<string> { "31", "71" });

            _settlementVoucherDetailExplain.FLuongBhxhQncnTru = _settlementVoucherDetailExplain.FLuongBhxhQncnTru > 0
                                            ? _settlementVoucherDetailExplain.FLuongBhxhQncnTru
                                            : SumLuongFromExplainSubtract(new List<string> { "32", "72" });

            _settlementVoucherDetailExplain.FPhuCapBhxhQncnTru = _settlementVoucherDetailExplain.FPhuCapBhxhQncnTru > 0
                                            ? _settlementVoucherDetailExplain.FPhuCapBhxhQncnTru
                                            : SumPhuCapFromExplainSubstract(new List<string> { "32", "72" });

            _settlementVoucherDetailExplain.FLuongBhxhCnvqpTru = _settlementVoucherDetailExplain.FLuongBhxhCnvqpTru > 0
                                            ? _settlementVoucherDetailExplain.FLuongBhxhCnvqpTru
                                            : SumLuongFromExplainSubtract(new List<string> { "33", "73" });

            _settlementVoucherDetailExplain.FPhuCapBhxhCnvqpTru = _settlementVoucherDetailExplain.FPhuCapBhxhCnvqpTru > 0
                                            ? _settlementVoucherDetailExplain.FPhuCapBhxhCnvqpTru
                                            : SumPhuCapFromExplainSubstract(new List<string> { "33", "73" });

            _settlementVoucherDetailExplain.FLuongBhxhHdTru = _settlementVoucherDetailExplain.FLuongBhxhHdTru > 0
                                            ? _settlementVoucherDetailExplain.FLuongBhxhHdTru
                                            : SumLuongFromExplainSubtract(new List<string> { "34", "74" });

            _settlementVoucherDetailExplain.FPhuCapBhxhHdTru = _settlementVoucherDetailExplain.FPhuCapBhxhHdTru > 0
                                            ? _settlementVoucherDetailExplain.FPhuCapBhxhHdTru
                                            : SumPhuCapFromExplainSubstract(new List<string> { "34", "74" });
        }

        /// <summary>
        /// tính tiền lương xin quyết toán tháng này
        /// </summary>
        private void CalculateSettlementSalary()
        {
            _settlementVoucherDetailExplain.FLuongSiQuanQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000", "1");
            _settlementVoucherDetailExplain.FPhuCapSiQuanQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100", "1");

            _settlementVoucherDetailExplain.FLuongQncnQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000", "2");
            _settlementVoucherDetailExplain.FPhuCapQncnQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100", "2");

            _settlementVoucherDetailExplain.FLuongCnvqpQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000", "3.1,3.2,3.3");
            _settlementVoucherDetailExplain.FPhuCapCnvqpQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100", "3.1,3.2,3.3");

            _settlementVoucherDetailExplain.FLuongHdQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6000", "4");
            _settlementVoucherDetailExplain.FPhuCapHdQt = GetDataFromSettlementVoucherDetails("1010000-010-011-6100", "4");
        }

        /// <summary>
        /// load dữ liệu quyết toán giải thích lương trừ
        /// </summary>
        private void LoadExplainSubtractData()
        {
            if (SettlementVoucher == null) return;
            var predicate = PredicateBuilder.True<NsQtChungTuChiTietGiaiThichLuongTru>();
            if (!string.IsNullOrEmpty(SettlementVoucher.STongHop))
            {
                IsEnableSelfPayTab = false;
                var listChildVoucher = _chungTuService.FindByCondition(n => n.INamLamViec == SettlementVoucher.INamLamViec
                                                                     && n.INamNganSach == SettlementVoucher.INamNganSach
                                                                     && n.IIdMaNguonNganSach == SettlementVoucher.IIdMaNguonNganSach
                                                                     && SettlementVoucher.STongHop.Contains(n.SSoChungTu)).ToList();
                var listIdChildVoucher = listChildVoucher.Select(n => n.Id);
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && listIdChildVoucher.Contains(x.IIdQtchungTu));
            }
            else
            {
                IsEnableSelfPayTab = true;
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork && x.IIdQtchungTu == SettlementVoucher.Id);
            }

            List<NsQtChungTuChiTietGiaiThichLuongTru> luongTrus = _chungTuChiTietGiaiThichLuongTruService.FindByCondition(predicate);
            _explainSubtracts = _mapper.Map<ObservableCollection<SettlementVoucherDetailExplainSubtractModel>>(luongTrus);

            if (_explainSubtracts.Count == 0)
                _explainSubtracts.Add(new SettlementVoucherDetailExplainSubtractModel(SettlementVoucher.IIdMaDonVi));

            SelectedExplainSubtract = _explainSubtracts.First();
            foreach (var model in _explainSubtracts)
            {
                model.PropertyChanged += ExplainSubtractModel_PropertyChanged;
            }
            OnSumExplainSubtract();
            OnPropertyChanged(nameof(ExplainSubtracts));
        }

        private void ExplainSubtractModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(SettlementVoucherDetailExplainSubtractModel.IsModified)
                       && args.PropertyName != nameof(SettlementVoucherDetailExplainSubtractModel.IsDeleted))
            {
                SettlementVoucherDetailExplainSubtractModel item = (SettlementVoucherDetailExplainSubtractModel)sender;
                item.IsModified = true;
                OnSumExplainSubtract();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsSaveAndUpdateData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        /// <summary>
        /// Load data cho combobox đối tượng
        /// </summary>
        private void LoadMilitaryObjects()
        {
            List<MilitaryObjectModel> listMilitaryObject = new List<MilitaryObjectModel>
            {
                new MilitaryObjectModel("31", "31 - Thai sản, dài ngày - Sĩ quan"),
                new MilitaryObjectModel("32", "32 - Thai sản, dài ngày - QNCN"),
                new MilitaryObjectModel("33", "33 - Thai sản, dài ngày - CNVCQP"),
                new MilitaryObjectModel("34", "34 - Thai sản, dài ngày - Hợp đồng"),
                new MilitaryObjectModel("35", "35 - Thai sản, dài ngày - Chiến sĩ"),
                new MilitaryObjectModel("71", "71 - Ốm ngắn - Sĩ quan"),
                new MilitaryObjectModel("72", "72 - Ốm ngắn - QNCN"),
                new MilitaryObjectModel("73", "73 - Ốm ngắn - CNVCQP"),
                new MilitaryObjectModel("74", "74 - Ốm ngắn - Hợp đồng"),
                new MilitaryObjectModel("75", "75 - Ốm ngắn - Chiến sĩ")
            };
            _militaryObjects = new ObservableCollection<MilitaryObjectModel>(listMilitaryObject);
            OnPropertyChanged(nameof(MilitaryObjects));
        }

        /// <summary>
        /// tính lương từ chứng từ chi tiết
        /// </summary>
        /// <param name="concatenateCode"></param>Z
        /// <returns></returns>
        private double GetDataFromSettlementVoucherDetails(string concatenateCode, string code)
        {
            var listCode = code.Split(StringUtils.COMMA).ToList();
            var mlns = _iNsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork)
                                                .Where(n => listCode.Contains(n.SMaCB) && n.XauNoiMa.StartsWith(concatenateCode));

            var detail = from ss in SettlementVoucherDetails
                         join ml in mlns on ss.IIdMlns equals ml.MlnsId.ToString()
                         select ss;

            var detailVoucher = _mapper.Map<List<SettlementVoucherDetailModel>>(detail);

            return detailVoucher.Where(x => x.BHangCha == false).Sum(x => x.FTuChiPheDuyet);
        }

        /// <summary>
        /// tính phụ cấp từ chứng từ chi tiết
        /// </summary>
        /// <param name="concatenateCodes"></param>
        /// <returns></returns>
        private double SumDataFromSettelemtVoucherDetails(List<string> concatenateCodes)
        {
            double sum = 0;
            foreach (var item in SettlementVoucherDetails.Where(x => concatenateCodes.Contains(x.SXauNoiMa)).ToList())
            {
                sum += item.FTuChiPheDuyet;
            }
            return sum;
        }

        /// <summary>
        /// tính lương từ list giải thích lương trừ
        /// </summary>
        /// <param name="idDoiTuongs"></param>
        /// <returns></returns>
        private double SumLuongFromExplainSubtract(List<string> idDoiTuongs)
        {
            return _explainSubtracts.Where(x => idDoiTuongs.Contains(x.IIdDoiTuong)).Select(x => x.FLuongCapBac).Sum();
        }

        /// <summary>
        /// tính phụ cấp từ list giải thích lương trừ
        /// </summary>
        /// <param name="idDoiTuongs"></param>
        /// <returns></returns>
        private double SumPhuCapFromExplainSubstract(List<string> idDoiTuongs)
        {
            double phuCap = 0;
            List<SettlementVoucherDetailExplainSubtractModel> listPhuCap = _explainSubtracts.Where(x => idDoiTuongs.Contains(x.IIdDoiTuong)).ToList();
            foreach (var item in listPhuCap)
            {
                phuCap += item.FLuongThamNien + item.FLuongPhuCapCongVu + item.FLuongPhuCapKhacBh + item.FLuongPhuCapKhac;
            }
            return phuCap;
        }

        public void OnSaveAndUpdate()
        {
            List<SettlementVoucherDetailExplainSubtractModel> explainSubtractAdd = _explainSubtracts.Where(x => x.Id == Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
            List<SettlementVoucherDetailExplainSubtractModel> explainSubtractUpdate = _explainSubtracts.Where(x => x.Id != Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
            List<SettlementVoucherDetailExplainSubtractModel> explainSubtractDelete = _explainSubtracts.Where(x => x.IsDeleted).ToList();

            if (explainSubtractAdd.Any(x => string.IsNullOrEmpty(x.IIdDoiTuong) || explainSubtractUpdate.Any(x => string.IsNullOrEmpty(x.IIdDoiTuong))))
            {
                MessageBox.Show(Resources.AlertObjectEmpty, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //thêm mới bản ghi giải thích lương trừ
            if (explainSubtractAdd.Count > 0)
            {
                explainSubtractAdd = explainSubtractAdd.Select(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IIdQtchungTu = SettlementVoucher.Id;
                    x.IIdGiaiThich = SettlementVoucher.Id.ToString();
                    x.IThangQuy = Convert.ToInt32(SettlementVoucher.IThangQuy);
                    x.IThangQuyLoai = SettlementVoucher.IThangQuyLoai;
                    x.IIdMaDonVi = SettlementVoucher.IIdMaDonVi;
                    x.INamLamViec = _sessionInfo.YearOfWork;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.DNgayTao = DateTime.Now;
                    return x;
                }).ToList();
                List<NsQtChungTuChiTietGiaiThichLuongTru> listGiaiThichLuongTrus = new List<NsQtChungTuChiTietGiaiThichLuongTru>();
                listGiaiThichLuongTrus = _mapper.Map<List<NsQtChungTuChiTietGiaiThichLuongTru>>(explainSubtractAdd);
                _chungTuChiTietGiaiThichLuongTruService.AddRange(listGiaiThichLuongTrus);
                _explainSubtracts.Where(x => x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            //cập nhật bản ghi giải thích lương trừ
            if (explainSubtractUpdate.Count > 0)
            {
                foreach (var item in explainSubtractUpdate)
                {
                    item.SNguoiSua = _sessionInfo.Principal;
                    item.DNgaySua = DateTime.Now;
                    NsQtChungTuChiTietGiaiThichLuongTru giaiThichLuongTru = _chungTuChiTietGiaiThichLuongTruService.FindById(item.Id);
                    _mapper.Map(item, giaiThichLuongTru);
                    _chungTuChiTietGiaiThichLuongTruService.Update(giaiThichLuongTru);
                    item.IsModified = false;
                }
            }

            //xóa bản ghi giải thích lương trừ
            if (explainSubtractDelete.Count > 0)
            {
                foreach (var item in explainSubtractDelete)
                {
                    _chungTuChiTietGiaiThichLuongTruService.Delete(item.Id);
                    _explainSubtracts.Remove(item);
                }
            }

            OnSumExplainSubtract();
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsSaveAndUpdateData));
            OnPropertyChanged(nameof(IsDeleteAll));

            //tính tiền lương của những ngày không hưởng lương (lương trừ)
            CalculateSubtractSalaryUpdate();

            NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = new NsQtChungTuChiTietGiaiThich();

            if (SettlementVoucherDetailExplain.Id != Guid.Empty)
            {
                SettlementVoucherDetailExplain.SNguoiSua = _sessionInfo.Principal;
                SettlementVoucherDetailExplain.DNgaySua = DateTime.Now;
                chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindById(SettlementVoucherDetailExplain.Id);
                _mapper.Map(SettlementVoucherDetailExplain, chungTuChiTietGiaiThich);
                _chungTuChiTietGiaiThichService.Update(chungTuChiTietGiaiThich);
            }
            else
            {
                var condition = new SettlementVoucherDetailExplainCriteria
                {
                    VoucherId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id,
                    ExplainId = SettlementVoucher == null ? ExplainId : SettlementVoucher.Id.ToString(),
                    AgencyId = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIdMaDonVi,
                    YearOfWork = _sessionInfo.YearOfWork
                };
                var chungTuChiTietGiaiThichExist = _chungTuChiTietGiaiThichService.FindByCondition(condition);

                if (chungTuChiTietGiaiThichExist != null)
                {
                    _chungTuChiTietGiaiThichService.Delete(chungTuChiTietGiaiThichExist.Id);
                }

                SettlementVoucherDetailExplain.Id = Guid.NewGuid();
                SettlementVoucherDetailExplain.IIdQtchungTu = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id;
                SettlementVoucherDetailExplain.IIdMaDonVi = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIdMaDonVi;
                SettlementVoucherDetailExplain.IIdGiaiThich = SettlementVoucher == null ? ExplainId : SettlementVoucher.Id.ToString();
                SettlementVoucherDetailExplain.INamLamViec = _sessionInfo.YearOfWork;
                SettlementVoucherDetailExplain.IThangQuy = SettlementVoucher == null ? QuarterMonth : Convert.ToInt32(SettlementVoucher.IThangQuy);
                SettlementVoucherDetailExplain.IThangQuyLoai = SettlementVoucher == null ? QuarterMonthType : SettlementVoucher.IThangQuyLoai;
                SettlementVoucherDetailExplain.SNguoiTao = _sessionInfo.Principal;
                SettlementVoucherDetailExplain.DNgayTao = DateTime.Now;
                _mapper.Map(SettlementVoucherDetailExplain, chungTuChiTietGiaiThich);
                _chungTuChiTietGiaiThichService.Add(chungTuChiTietGiaiThich);
            }

            MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            // SelectedTab = 0;

        }

        public void OnSaveAndUpdateWithoutMessage()
        {
            List<SettlementVoucherDetailExplainSubtractModel> explainSubtractAdd = _explainSubtracts.Where(x => x.Id == Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
            List<SettlementVoucherDetailExplainSubtractModel> explainSubtractUpdate = _explainSubtracts.Where(x => x.Id != Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
            List<SettlementVoucherDetailExplainSubtractModel> explainSubtractDelete = _explainSubtracts.Where(x => x.IsDeleted).ToList();

            if (explainSubtractAdd.Any(x => string.IsNullOrEmpty(x.IIdDoiTuong) || explainSubtractUpdate.Any(x => string.IsNullOrEmpty(x.IIdDoiTuong))))
            {
                MessageBox.Show(Resources.AlertObjectEmpty, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //thêm mới bản ghi giải thích lương trừ
            if (explainSubtractAdd.Count > 0)
            {
                explainSubtractAdd = explainSubtractAdd.Select(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IIdQtchungTu = SettlementVoucher.Id;
                    x.IIdGiaiThich = SettlementVoucher.Id.ToString();
                    x.IThangQuy = Convert.ToInt32(SettlementVoucher.IThangQuy);
                    x.IThangQuyLoai = SettlementVoucher.IThangQuyLoai;
                    x.IIdMaDonVi = SettlementVoucher.IIdMaDonVi;
                    x.INamLamViec = _sessionInfo.YearOfWork;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.DNgayTao = DateTime.Now;
                    return x;
                }).ToList();
                List<NsQtChungTuChiTietGiaiThichLuongTru> listGiaiThichLuongTrus = new List<NsQtChungTuChiTietGiaiThichLuongTru>();
                listGiaiThichLuongTrus = _mapper.Map<List<NsQtChungTuChiTietGiaiThichLuongTru>>(explainSubtractAdd);
                _chungTuChiTietGiaiThichLuongTruService.AddRange(listGiaiThichLuongTrus);
                _explainSubtracts.Where(x => x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            //cập nhật bản ghi giải thích lương trừ
            if (explainSubtractUpdate.Count > 0)
            {
                foreach (var item in explainSubtractUpdate)
                {
                    item.SNguoiSua = _sessionInfo.Principal;
                    item.DNgaySua = DateTime.Now;
                    NsQtChungTuChiTietGiaiThichLuongTru giaiThichLuongTru = _chungTuChiTietGiaiThichLuongTruService.FindById(item.Id);
                    _mapper.Map(item, giaiThichLuongTru);
                    _chungTuChiTietGiaiThichLuongTruService.Update(giaiThichLuongTru);
                    item.IsModified = false;
                }
            }

            //xóa bản ghi giải thích lương trừ
            if (explainSubtractDelete.Count > 0)
            {
                foreach (var item in explainSubtractDelete)
                {
                    _chungTuChiTietGiaiThichLuongTruService.Delete(item.Id);
                    _explainSubtracts.Remove(item);
                }
            }

            OnSumExplainSubtract();
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsSaveAndUpdateData));
            OnPropertyChanged(nameof(IsDeleteAll));

            //tính tiền lương của những ngày không hưởng lương (lương trừ)
            CalculateSubtractSalaryUpdate();

            NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = new NsQtChungTuChiTietGiaiThich();

            if (SettlementVoucherDetailExplain.Id != Guid.Empty)
            {
                SettlementVoucherDetailExplain.SNguoiSua = _sessionInfo.Principal;
                SettlementVoucherDetailExplain.DNgaySua = DateTime.Now;
                chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindById(SettlementVoucherDetailExplain.Id);
                _mapper.Map(SettlementVoucherDetailExplain, chungTuChiTietGiaiThich);
                _chungTuChiTietGiaiThichService.Update(chungTuChiTietGiaiThich);
            }
            else
            {
                var condition = new SettlementVoucherDetailExplainCriteria
                {
                    VoucherId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id,
                    ExplainId = SettlementVoucher == null ? ExplainId : SettlementVoucher.Id.ToString(),
                    AgencyId = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIdMaDonVi,
                    YearOfWork = _sessionInfo.YearOfWork
                };
                var chungTuChiTietGiaiThichExist = _chungTuChiTietGiaiThichService.FindByCondition(condition);

                if (chungTuChiTietGiaiThichExist != null)
                {
                    _chungTuChiTietGiaiThichService.Delete(chungTuChiTietGiaiThichExist.Id);
                }

                SettlementVoucherDetailExplain.Id = Guid.NewGuid();
                SettlementVoucherDetailExplain.IIdQtchungTu = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id;
                SettlementVoucherDetailExplain.IIdMaDonVi = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIdMaDonVi;
                SettlementVoucherDetailExplain.IIdGiaiThich = SettlementVoucher == null ? ExplainId : SettlementVoucher.Id.ToString();
                SettlementVoucherDetailExplain.INamLamViec = _sessionInfo.YearOfWork;
                SettlementVoucherDetailExplain.IThangQuy = SettlementVoucher == null ? QuarterMonth : Convert.ToInt32(SettlementVoucher.IThangQuy);
                SettlementVoucherDetailExplain.IThangQuyLoai = SettlementVoucher == null ? QuarterMonthType : SettlementVoucher.IThangQuyLoai;
                SettlementVoucherDetailExplain.SNguoiTao = _sessionInfo.Principal;
                SettlementVoucherDetailExplain.DNgayTao = DateTime.Now;
                _mapper.Map(SettlementVoucherDetailExplain, chungTuChiTietGiaiThich);
                _chungTuChiTietGiaiThichService.Add(chungTuChiTietGiaiThich);
            }

        }

        public override void OnSave()
        {
            //lữu dữ liệu giải thích số
            if (_selectedTab == 0)
            {
                NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = new NsQtChungTuChiTietGiaiThich();

                if (SettlementVoucherDetailExplain.Id != Guid.Empty)
                {
                    SettlementVoucherDetailExplain.SNguoiSua = _sessionInfo.Principal;
                    SettlementVoucherDetailExplain.DNgaySua = DateTime.Now;
                    chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindById(SettlementVoucherDetailExplain.Id);
                    _mapper.Map(SettlementVoucherDetailExplain, chungTuChiTietGiaiThich);
                    _chungTuChiTietGiaiThichService.Update(chungTuChiTietGiaiThich);
                }
                else
                {
                    var condition = new SettlementVoucherDetailExplainCriteria
                    {
                        VoucherId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id,
                        ExplainId = SettlementVoucher == null ? ExplainId : SettlementVoucher.Id.ToString(),
                        AgencyId = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIdMaDonVi,
                        YearOfWork = _sessionInfo.YearOfWork
                    };
                    var chungTuChiTietGiaiThichExist = _chungTuChiTietGiaiThichService.FindByCondition(condition);

                    if (chungTuChiTietGiaiThichExist != null)
                    {
                        _chungTuChiTietGiaiThichService.Delete(chungTuChiTietGiaiThichExist.Id);
                    }

                    SettlementVoucherDetailExplain.Id = Guid.NewGuid();
                    SettlementVoucherDetailExplain.IIdQtchungTu = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id;
                    SettlementVoucherDetailExplain.IIdMaDonVi = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIdMaDonVi;
                    SettlementVoucherDetailExplain.IIdGiaiThich = SettlementVoucher == null ? ExplainId : SettlementVoucher.Id.ToString();
                    SettlementVoucherDetailExplain.INamLamViec = _sessionInfo.YearOfWork;
                    SettlementVoucherDetailExplain.IThangQuy = SettlementVoucher == null ? QuarterMonth : Convert.ToInt32(SettlementVoucher.IThangQuy);
                    SettlementVoucherDetailExplain.IThangQuyLoai = SettlementVoucher == null ? QuarterMonthType : SettlementVoucher.IThangQuyLoai;
                    SettlementVoucherDetailExplain.SNguoiTao = _sessionInfo.Principal;
                    SettlementVoucherDetailExplain.DNgayTao = DateTime.Now;
                    _mapper.Map(SettlementVoucherDetailExplain, chungTuChiTietGiaiThich);
                    _chungTuChiTietGiaiThichService.Add(chungTuChiTietGiaiThich);
                }
            }
            //lưu dữ liệu giải thích lương trừ
            if (_selectedTab == 1)
            {
                List<SettlementVoucherDetailExplainSubtractModel> explainSubtractAdd = _explainSubtracts.Where(x => x.Id == Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
                List<SettlementVoucherDetailExplainSubtractModel> explainSubtractUpdate = _explainSubtracts.Where(x => x.Id != Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
                List<SettlementVoucherDetailExplainSubtractModel> explainSubtractDelete = _explainSubtracts.Where(x => x.IsDeleted).ToList();

                if (explainSubtractAdd.Any(x => string.IsNullOrEmpty(x.IIdDoiTuong) || explainSubtractUpdate.Any(x => string.IsNullOrEmpty(x.IIdDoiTuong))))
                {
                    MessageBox.Show(Resources.AlertObjectEmpty, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                //thêm mới bản ghi giải thích lương trừ
                if (explainSubtractAdd.Count > 0)
                {
                    explainSubtractAdd = explainSubtractAdd.Select(x =>
                    {
                        x.Id = Guid.NewGuid();
                        x.IIdQtchungTu = SettlementVoucher.Id;
                        x.IIdGiaiThich = SettlementVoucher.Id.ToString();
                        x.IThangQuy = Convert.ToInt32(SettlementVoucher.IThangQuy);
                        x.IThangQuyLoai = SettlementVoucher.IThangQuyLoai;
                        x.IIdMaDonVi = SettlementVoucher.IIdMaDonVi;
                        x.INamLamViec = _sessionInfo.YearOfWork;
                        x.SNguoiTao = _sessionInfo.Principal;
                        x.DNgayTao = DateTime.Now;
                        return x;
                    }).ToList();
                    List<NsQtChungTuChiTietGiaiThichLuongTru> listGiaiThichLuongTrus = new List<NsQtChungTuChiTietGiaiThichLuongTru>();
                    listGiaiThichLuongTrus = _mapper.Map<List<NsQtChungTuChiTietGiaiThichLuongTru>>(explainSubtractAdd);
                    _chungTuChiTietGiaiThichLuongTruService.AddRange(listGiaiThichLuongTrus);
                    _explainSubtracts.Where(x => x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
                }

                //cập nhật bản ghi giải thích lương trừ
                if (explainSubtractUpdate.Count > 0)
                {
                    foreach (var item in explainSubtractUpdate)
                    {
                        item.SNguoiSua = _sessionInfo.Principal;
                        item.DNgaySua = DateTime.Now;
                        NsQtChungTuChiTietGiaiThichLuongTru giaiThichLuongTru = _chungTuChiTietGiaiThichLuongTruService.FindById(item.Id);
                        _mapper.Map(item, giaiThichLuongTru);
                        _chungTuChiTietGiaiThichLuongTruService.Update(giaiThichLuongTru);
                        item.IsModified = false;
                    }
                }

                //xóa bản ghi giải thích lương trừ
                if (explainSubtractDelete.Count > 0)
                {
                    foreach (var item in explainSubtractDelete)
                    {
                        _chungTuChiTietGiaiThichLuongTruService.Delete(item.Id);
                        _explainSubtracts.Remove(item);
                    }
                }

                OnSumExplainSubtract();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));

                //tính tiền lương của những ngày không hưởng lương (lương trừ)
                CalculateSubtractSalary();
            }
            MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnAddRow()
        {
            if (SettlementVoucher == null || !IsEnableSelfPayTab) return;
            if (ExplainSubtracts != null && ExplainSubtracts.Count > 0)
            {
                int currentRow = _explainSubtracts.IndexOf(SelectedExplainSubtract);
                SettlementVoucherDetailExplainSubtractModel item = new SettlementVoucherDetailExplainSubtractModel(SettlementVoucher.IIdMaDonVi);
                item.PropertyChanged += ExplainSubtractModel_PropertyChanged;
                _explainSubtracts.Insert(currentRow + 1, item);
                OnPropertyChanged(nameof(ExplainSubtracts));
            }
        }

        private void OnDeleteRow()
        {
            if (SettlementVoucher == null || !IsEnableSelfPayTab) return;
            if (SelectedExplainSubtract != null)
            {
                SettlementVoucherDetailExplainSubtractModel item = _explainSubtracts.Where(x => x.CustomId == SelectedExplainSubtract.CustomId).FirstOrDefault();
                if (item != null)
                {
                    item.IsDeleted = !item.IsDeleted;
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                }
            }
        }

        private void OnReloadData()
        {
            if (IsSaveData)
            {
                string message = Resources.MsgConfirmEdit;
                var messageBox = new NSMessageBoxViewModel(message, "Thông báo", NSMessageBoxButtons.YesNoCancel, OnConfirmReloadHandler);
                DialogHost.Show(messageBox.Content, "DataInterpretationDialog");
            }
        }

        private void OnConfirmReloadHandler(NSDialogResult result)
        {
            if (result == NSDialogResult.Cancel) return;
            if (result == NSDialogResult.Yes)
                OnSave();
            LoadExplainSubtractData();
        }

        private void OnSumExplainSubtract()
        {
            _sumExplainSubtract = new SumExplainSubtractModel();
            foreach (var item in _explainSubtracts)
            {
                _sumExplainSubtract.TongLuongThang += item.FLuongThang;
                _sumExplainSubtract.TongLuongCapBac += item.FLuongCapBac;
                _sumExplainSubtract.TongLuongThamNien += item.FLuongThamNien;
                _sumExplainSubtract.TongLuongPhuCapCongVu += item.FLuongPhuCapCongVu;
                _sumExplainSubtract.TongLuongPhuCapKhacBh += item.FLuongPhuCapKhacBh;
                _sumExplainSubtract.TongLuongPhuCapKhac += item.FLuongPhuCapKhac;
                _sumExplainSubtract.TongBaoHiem += item.FTongBaoHiem;
            }
            OnPropertyChanged(nameof(SumExplainSubtract));
        }

        public override void OnClose(object obj)
        {
            base.OnClose(obj);
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }

        protected void OnDeleteAll()
        {
            var result = MessageBox.Show(Resources.DeleteAllChungTuChiTiet, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                _chungTuChiTietGiaiThichLuongTruService.DeleteByVoucherId(SettlementVoucher.Id);
                LoadExplainSubtractData();
                MessageBox.Show(Resources.MsgDeleteSuccess, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }
    }
}
