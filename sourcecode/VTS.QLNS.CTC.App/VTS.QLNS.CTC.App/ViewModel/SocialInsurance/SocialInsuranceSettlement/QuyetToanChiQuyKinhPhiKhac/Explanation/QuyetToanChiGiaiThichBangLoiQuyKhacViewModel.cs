using AutoMapper;
using log4net;
using System;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.Explanation;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using System.Windows;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.Explanation
{
    public class QuyetToanChiGiaiThichBangLoiQuyKhacViewModel : ViewModelBase
    {
        private readonly IQtcQBHXHChiTietGiaiThichService _qtcQBHXHChiTietGiaiThichService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;

        private bool _isCreate;
        public override string Name => "Quyết toán - Chứng từ chi tiết - Giải thích bằng lời";
        public override Type ContentType => typeof(VerbalExplanation);
        public BhQtcQuyKPKModel BhQtcQuyKPKModel;
        public Guid ExplainId;
        public string AgencyId;
        public int QuarterYear;
        public int QuarterYearType;
        public string QuarterYearDescription;
        private BhQtcQBHXHChiTietGiaiThichModel _settlementVoucherDetailExplain;
        public BhQtcQBHXHChiTietGiaiThichModel SettlementVoucherDetailExplain
        {
            get => _settlementVoucherDetailExplain;
            set => SetProperty(ref _settlementVoucherDetailExplain, value);
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
        public QuyetToanChiGiaiThichBangLoiQuyKhacViewModel(IQtcQBHXHChiTietGiaiThichService qtcQBHXHChiTietGiaiThichService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
             IMapper mapper,
             ISessionService sessionService,
             ILog log)
        {
            _qtcQBHXHChiTietGiaiThichService = qtcQBHXHChiTietGiaiThichService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = log;

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
            try
            {
                var danhMucLoaiChi = _bhDanhMucLoaiChiService.FindById(BhQtcQuyKPKModel.IID_LoaiChi);
                BhQtcQBHXHChiTietGiaiThichCriteria condition = new BhQtcQBHXHChiTietGiaiThichCriteria();
                condition.VoucherId = BhQtcQuyKPKModel.Id;
                condition.AgencyId = BhQtcQuyKPKModel.IID_MaDonVi;
                condition.SLNS = BhQtcQuyKPKModel.SDSLNS;
                condition.ExplainType = danhMucLoaiChi.SMaLoaiChi;
                condition.YearOfWork = BhQtcQuyKPKModel.INamChungTu;
                var chungTuChiTietGiaiThich = _qtcQBHXHChiTietGiaiThichService.FindByCondition(condition);
                if (chungTuChiTietGiaiThich != null)
                {
                    _settlementVoucherDetailExplain = _mapper.Map<BhQtcQBHXHChiTietGiaiThichModel>(chungTuChiTietGiaiThich);

                    _isCreate = false;
                    OnPropertyChanged(nameof(SettlementVoucherDetailExplain));
                }
                else
                {
                    _settlementVoucherDetailExplain = new BhQtcQBHXHChiTietGiaiThichModel();
                    _isCreate = true;
                    OnPropertyChanged(nameof(SettlementVoucherDetailExplain));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        private void OnSaveData()
        {
            try
            {
                BhQtcQBHXHChiTietGiaiThich chungTuGiaiThich = new BhQtcQBHXHChiTietGiaiThich();
                //trường hợp tạo mới
                if (_isCreate)
                {
                    var danhMucLoaiChi = _bhDanhMucLoaiChiService.FindById(BhQtcQuyKPKModel.IID_LoaiChi);

                    chungTuGiaiThich.IID_QTC_QChungTu = BhQtcQuyKPKModel.Id;
                    chungTuGiaiThich.IQuy = BhQtcQuyKPKModel.IQuyChungTu;
                    chungTuGiaiThich.INamLamViec = BhQtcQuyKPKModel.INamChungTu;
                    chungTuGiaiThich.IID_MaDonVi = BhQtcQuyKPKModel.IID_MaDonVi;
                    chungTuGiaiThich.SMoTa_KienNghi = SettlementVoucherDetailExplain.SMoTa_KienNghi;
                    chungTuGiaiThich.SMoTa_TinhHinh = SettlementVoucherDetailExplain.SMoTa_TinhHinh;
                    chungTuGiaiThich.DNgayTao = DateTime.Now;
                    chungTuGiaiThich.SMaLoaiChi = danhMucLoaiChi.SMaLoaiChi;
                    chungTuGiaiThich.SLNS = danhMucLoaiChi.SLNS;
                    chungTuGiaiThich.SNguoiTao = _sessionInfo.Principal;
                    _qtcQBHXHChiTietGiaiThichService.Add(chungTuGiaiThich);
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    LoadData();
                }
                else
                {
                    SettlementVoucherDetailExplain.SNguoiSua = _sessionInfo.Principal;
                    SettlementVoucherDetailExplain.DNgaySua = DateTime.Now;

                    chungTuGiaiThich = _qtcQBHXHChiTietGiaiThichService.FindById(SettlementVoucherDetailExplain.Id);
                    _mapper.Map(SettlementVoucherDetailExplain, chungTuGiaiThich);
                    _qtcQBHXHChiTietGiaiThichService.Update(chungTuGiaiThich);
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

        }
    }
}
