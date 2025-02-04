using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH
{
    public class QuyetToanChiQuyBHXHSummaryViewModel : ViewModelBase
    {
        private readonly INsDonViService _donViService;
        private readonly IQtcqBHXHService _qtcqBHXHService;
        private readonly IQtcqBHXHChiTietService _qtcqBHXHChiTietService;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public bool IsEditProcess = false;
        public override string Name => "TỔNG HỢP QUYẾT TOÁN CHI QUÝ BHXH";
        public override string Title => "TỔNG HỢP";
        public override string Description => "Tổng hợp quyết toán chi quý BHXH";
        public override Type ContentType => typeof(QuyetToanChiQuyBHXHSummary);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public bool IsEnableView { get; set; }
        private bool _isCapPhatToanDonVi;


        private BhQtcqBHXHModel _bhQtcqBHXH;
        public BhQtcqBHXHModel BhQtcqBHXH
        {
            get => _bhQtcqBHXH;
            set => SetProperty(ref _bhQtcqBHXH, value);
        }

        private ObservableCollection<BhQtcqBHXHModel> _dataBhqtcqBHXH;
        public ObservableCollection<BhQtcqBHXHModel> DataBhqtcqBHXH
        {
            get => _dataBhqtcqBHXH;
            set => SetProperty(ref _dataBhqtcqBHXH, value);
        }



        public RelayCommand SaveCommand { get; }

        public QuyetToanChiQuyBHXHSummaryViewModel(
            INsDonViService donViService,
            ISessionService sessionService,
            IQtcqBHXHService qtcqBHXHService,
            IQtcqBHXHChiTietService qtcqBHXHChTietService,
            ILog logger,
            IDanhMucService danhMucService,
            IMapper mapper)
        {
            _donViService = donViService;
            _qtcqBHXHService = qtcqBHXHService;
            _qtcqBHXHChiTietService = qtcqBHXHChTietService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _mapper = mapper;
            _logger = logger;
            SaveCommand = new RelayCommand(o => OnSave());
        }

        public override void Init()
        {
            try
            {
                IsEnableView = true;
                if (BhQtcqBHXH == null) BhQtcqBHXH = new Model.BhQtcqBHXHModel();
                if (BhQtcqBHXH.Id == Guid.Empty)
                {
                    BhQtcqBHXH = new Model.BhQtcqBHXHModel();
                    int soChungTuIndex = _qtcqBHXHService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                    BhQtcqBHXH.SSoChungTu = "QTC-" + soChungTuIndex.ToString("D3");
                    BhQtcqBHXH.DNgayChungTu = DateTime.Now;
                    BhQtcqBHXH.DNgayQuyetDinh = DateTime.Now;
                    BhQtcqBHXH.SMoTa = "Chi tiết";
                }

                OnPropertyChanged(nameof(IsEnableView));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateDetailSummary(BhQtcqBHXH chungTu)
        {
            _qtcqBHXHChiTietService.CreateVoudcherSummary(string.Join(",", DataBhqtcqBHXH.Select(n => n.Id.ToString()).ToList())
                , _sessionService.Current.Principal, _sessionService.Current.YearOfWork, chungTu.Id.ToString(), chungTu.IIdMaDonVi);
        }

        private List<string> CheckSummary()
        {
            var predicate = PredicateBuilder.True<BhQtcqBHXH>();
            predicate = predicate.And(x => x.INamChungTu == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.SDSSoChungTuTongHop));

            List<BhQtcqBHXH> chungTu = _qtcqBHXHService.FindByCondition(predicate).ToList();
            List<string> listSoChungTuSummary = new List<string>();
            foreach (var item in chungTu)
            {
                listSoChungTuSummary.AddRange(item.SDSSoChungTuTongHop.Split(",").ToList());
            }
            List<string> listResult = listSoChungTuSummary.Where(x => DataBhqtcqBHXH.Select(n => n.SSoChungTu).ToList().Contains(x)).ToList();
            return listResult;
        }

        public override void OnSave()
        {
            try
            {
                if (BhQtcqBHXH.Id != Guid.Empty)
                {

                    BhQtcqBHXH entity = _qtcqBHXHService.FindById(BhQtcqBHXH.Id);
                    entity.DNgayChungTu = BhQtcqBHXH.DNgayChungTu;
                    entity.DNgayQuyetDinh = BhQtcqBHXH.DNgayQuyetDinh;
                    entity.SMoTa = BhQtcqBHXH.SMoTa;
                    entity.SSoQuyetDinh = BhQtcqBHXH.SSoQuyetDinh;
                    _qtcqBHXHService.Update(entity);

                    DialogHost.Close(SystemConstants.ROOT_DIALOG);
                    SavedAction?.Invoke(_mapper.Map<BhQtcqBHXHModel>(entity));
                }
                else
                {
                    DonVi donVi0 = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
                    string message = GetMessageValidate();
                    if (!string.IsNullOrEmpty(message))
                    {
                        System.Windows.Forms.MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (donVi0 != null)
                    {
                        if (DataBhqtcqBHXH == null || DataBhqtcqBHXH.Count() == 0)
                        {
                            return;
                        }

                        List<string> listSummaryHistory = CheckSummary();
                        if (listSummaryHistory != null && listSummaryHistory.Count > 0)
                        {
                            MessageBoxHelper.Warning(string.Format(Resources.MsgSumaryWarning, string.Join(",", listSummaryHistory)));
                            return;
                        }

                        int soChungTuIndex = _qtcqBHXHService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                        BhQtcqBHXH entity = new BhQtcqBHXH();
                        entity.SSoChungTu = BhQtcqBHXH.SSoChungTu;
                        entity.SSoQuyetDinh = BhQtcqBHXH.SSoQuyetDinh;
                        entity.SMoTa = BhQtcqBHXH.SMoTa;
                        entity.DNgayChungTu = BhQtcqBHXH.DNgayChungTu;
                        entity.BDaTongHop = false;
                        entity.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                        entity.DNgayQuyetDinh = BhQtcqBHXH.DNgayQuyetDinh;
                        entity.IIdDonVi = donVi0.Id;
                        entity.IIdMaDonVi = donVi0.IIDMaDonVi;
                        entity.INamChungTu = _sessionService.Current.YearOfWork;
                        entity.SDSSoChungTuTongHop = string.Join(",", DataBhqtcqBHXH.Select(n => n.SSoChungTu).ToList());
                        entity.SDSLNS = string.Join(",", DataBhqtcqBHXH.Select(n => n.SDSLNS).Distinct().ToList());
                        entity.IQuyChungTu = DataBhqtcqBHXH.Select(x => x.IQuyChungTu).First();
                        entity.DNgayTao = DateTime.Now;
                        entity.DNgaySua = null;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        _qtcqBHXHService.Add(entity);

                        CreateDetailSummary(entity);
                        _qtcqBHXHService.UpdateTotalCPChungTu(entity.Id.ToString(), _sessionService.Current.Principal);

                        DialogHost.CloseDialogCommand.Execute(null, null);
                        MessageBoxHelper.Info(Resources.MsgSumaryDone);
                        SavedAction?.Invoke(_mapper.Map<BhQtcqBHXHModel>(entity));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (BhQtcqBHXH.DNgayChungTu == null)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            //check đã tồn tại số quyết định
            var predicate_sqd = PredicateBuilder.True<BhQtcqBHXH>();
            predicate_sqd = predicate_sqd.And(x => x.INamChungTu == BhQtcqBHXH.INamChungTu);

            var chungtu_sqd = _qtcqBHXHService.FindByCondition(predicate_sqd).FirstOrDefault();
            if (chungtu_sqd != null)
            {
                messages.Add(Resources.MsgTrungSoQD);
            }

            //Check đã tồn chứng từ tổng hợp của đơn vị 0
            DonVi donVi0 = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            var predicate = PredicateBuilder.True<BhQtcqBHXH>();
            predicate = predicate.And(x => x.IIdDonVi == donVi0.Id);
            predicate = predicate.And(x => x.IIdMaDonVi == donVi0.IIDMaDonVi);
            predicate = predicate.And(x => x.INamChungTu == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IQuyChungTu == DataBhqtcqBHXH.Select(x => x.IQuyChungTu).First());

            var chungtu = _qtcqBHXHService.FindByCondition(predicate).FirstOrDefault();
            if (chungtu != null)
            {
                messages.Add(string.Format(Resources.AlertExistSettlementMonthVoucher, donVi0?.TenDonVi, _sessionService.Current.YearOfWork, ""));
            }

            return string.Join(Environment.NewLine, messages);
        }
    }
}
