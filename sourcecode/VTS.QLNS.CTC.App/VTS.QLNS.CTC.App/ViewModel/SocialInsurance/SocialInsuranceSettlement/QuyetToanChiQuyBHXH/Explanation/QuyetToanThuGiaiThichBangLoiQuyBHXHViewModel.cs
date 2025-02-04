using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.Explanation;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Command;
using System.Windows;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;
using log4net;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.Explanation
{
    public class QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel : ViewModelBase
    {
        private readonly IQtcqBHXHChiTietService _qtcqBHXHChiTietService;
        private readonly IQtcQBHXHChiTietGiaiThichService _qtcQBHXHChiTietGiaiThichService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;

        private bool _isCreate;
        public override string Name => "Quyết toán - Chứng từ chi tiết - Giải thích bằng lời";
        public override Type ContentType => typeof(VerbalExplanation);
        public BhQtcqBHXHModel BhQtcqBHXHModel;
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

        public QuyetToanChiGiaiThichBangLoiQuyBHXHViewModel(IQtcqBHXHChiTietService qtcqBHXHChiTietService,
             IQtcQBHXHChiTietGiaiThichService qtcQBHXHChiTietGiaiThichService,
             IMapper mapper,
             ISessionService sessionService,
             ILog log

            )
        {
            _qtcqBHXHChiTietService = qtcqBHXHChiTietService;
            _qtcQBHXHChiTietGiaiThichService = qtcQBHXHChiTietGiaiThichService;
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
                BhQtcQBHXHChiTietGiaiThichCriteria condition = new BhQtcQBHXHChiTietGiaiThichCriteria();
                condition.VoucherId = BhQtcqBHXHModel.Id;
                condition.AgencyId = BhQtcqBHXHModel.IIdMaDonVi;
                condition.SLNS = BhQtcqBHXHModel.SDSLNS;
                condition.ExplainType = MaLoaiChiBHXH.SMABHXH;
                condition.YearOfWork = BhQtcqBHXHModel.INamChungTu;
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
                    chungTuGiaiThich.IID_QTC_QChungTu = BhQtcqBHXHModel.Id;
                    chungTuGiaiThich.IQuy = BhQtcqBHXHModel.IQuyChungTu;
                    chungTuGiaiThich.INamLamViec = BhQtcqBHXHModel.INamChungTu;
                    chungTuGiaiThich.IID_MaDonVi = BhQtcqBHXHModel.IIdMaDonVi;
                    chungTuGiaiThich.SMoTa_KienNghi = SettlementVoucherDetailExplain.SMoTa_KienNghi;
                    chungTuGiaiThich.SMoTa_TinhHinh = SettlementVoucherDetailExplain.SMoTa_TinhHinh;
                    chungTuGiaiThich.DNgayTao = DateTime.Now;
                    chungTuGiaiThich.SMaLoaiChi = MaLoaiChiBHXH.SMABHXH;
                    chungTuGiaiThich.SLNS = BhQtcqBHXHModel.SDSLNS;
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
