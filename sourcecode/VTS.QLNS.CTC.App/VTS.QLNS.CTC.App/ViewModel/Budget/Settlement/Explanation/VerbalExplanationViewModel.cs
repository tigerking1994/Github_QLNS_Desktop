using AutoMapper;
using System;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Explanation
{
    public class VerbalExplanationViewModel : ViewModelBase
    {
        private INsQtChungTuChiTietGiaiThichService _chungTuChiTietGiaiThichService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private bool _isCreate;

        public override string Name => "Quyết toán - Chứng từ chi tiết - Giải thích bằng lời";
        public override Type ContentType => typeof(View.Budget.Settlement.Explanation.VerbalExplanation);

        public SettlementVoucherModel SettlementVoucher;
        public string ExplainId;
        public string AgencyId;
        public int QuarterMonth;
        public int QuarterMonthType;

        private SettlementVoucherDetailExplainModel _settlementVoucherDetailExplain;
        public SettlementVoucherDetailExplainModel SettlementVoucherDetailExplain
        {
            get => _settlementVoucherDetailExplain;
            set => SetProperty(ref _settlementVoucherDetailExplain, value);
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }

        public VerbalExplanationViewModel(INsQtChungTuChiTietGiaiThichService chungTuChiTietGiaiThichService,
            IMapper mapper,
            ISessionService sessionService)
        {
            _chungTuChiTietGiaiThichService = chungTuChiTietGiaiThichService;
            _mapper = mapper;
            _sessionService = sessionService;

            SaveCommand = new RelayCommand(obj => OnSaveData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
        }

        private void LoadData()
        {
            SettlementVoucherDetailExplainCriteria condition = new SettlementVoucherDetailExplainCriteria
            {
                VoucherId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id,
                ExplainId = SettlementVoucher == null ? ExplainId : SettlementVoucher.Id.ToString(),
                AgencyId = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIdMaDonVi,
                YearOfWork = _sessionInfo.YearOfWork
            };
            NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindByCondition(condition);
            if (chungTuChiTietGiaiThich != null)
            {
                _settlementVoucherDetailExplain = _mapper.Map<SettlementVoucherDetailExplainModel>(chungTuChiTietGiaiThich);
                _isCreate = false;
            }
            else
            {
                _settlementVoucherDetailExplain = new SettlementVoucherDetailExplainModel();
                _isCreate = true;
            }
        }

        private void OnSaveData()
        {
            NsQtChungTuChiTietGiaiThich chungTuChiTietGiaiThich = new NsQtChungTuChiTietGiaiThich();
            
            //trường hợp tạo mới
            if (_isCreate)
            {
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
            else
            {
                SettlementVoucherDetailExplain.SNguoiSua = _sessionInfo.Principal;
                SettlementVoucherDetailExplain.DNgaySua = DateTime.Now;
                chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindById(SettlementVoucherDetailExplain.Id);
                _mapper.Map(SettlementVoucherDetailExplain, chungTuChiTietGiaiThich);
                _chungTuChiTietGiaiThichService.Update(chungTuChiTietGiaiThich);
            }

            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}
