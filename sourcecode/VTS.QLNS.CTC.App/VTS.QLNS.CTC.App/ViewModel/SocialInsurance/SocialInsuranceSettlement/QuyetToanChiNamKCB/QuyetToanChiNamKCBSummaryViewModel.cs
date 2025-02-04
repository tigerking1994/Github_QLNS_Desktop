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
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB
{
    public class QuyetToanChiNamKCBSummaryViewModel : ViewModelBase
    {
        private readonly INsDonViService _donViService;
        private readonly IQtcnKCBService _qtcnKCBService;
        private readonly IQtcnKCBChiTietService _qtcnKCBChiTietService;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        public bool IsEditProcess = false;
        public override string Name => "TỔNG HỢP QUYẾT TOÁN CHI NĂM KCB quân y đơn vị";
        public override string Title => "TỔNG HỢP";
        public override string Description => "Tổng hợp quyết toán chi năm KCB quân y đơn vị";
        public override Type ContentType => typeof(QuyetToanChiNamKCBSummary);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public bool IsEnableView { get; set; }
        private readonly bool _isCapPhatToanDonVi;


        private BhQtcnKCBModel _bhQtcnKCB;
        public BhQtcnKCBModel BhQtcnKCB
        {
            get => _bhQtcnKCB;
            set => SetProperty(ref _bhQtcnKCB, value);
        }

        private ObservableCollection<BhQtcnKCBModel> _dataBhqtcnKCB;
        public ObservableCollection<BhQtcnKCBModel> DataBhqtcnKCB
        {
            get => _dataBhqtcnKCB;
            set => SetProperty(ref _dataBhqtcnKCB, value);
        }

        public RelayCommand SaveCommand { get; }

        public QuyetToanChiNamKCBSummaryViewModel(
            INsDonViService donViService,
            ISessionService sessionService,
            IQtcnKCBService qtcnKCBService,
            IQtcnKCBChiTietService qtcnKCBChTietService,
        ILog logger,
            IDanhMucService danhMucService,
            IMapper mapper)
        {
            _donViService = donViService;
            _qtcnKCBService = qtcnKCBService;
            _qtcnKCBChiTietService = qtcnKCBChTietService;
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
                _sessionInfo = _sessionService.Current;
                IsEnableView = true;
                if (BhQtcnKCB == null) BhQtcnKCB = new BhQtcnKCBModel();
                if (BhQtcnKCB.Id == Guid.Empty)
                {
                    BhQtcnKCB = new BhQtcnKCBModel();
                    int soChungTuIndex = _qtcnKCBService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                    BhQtcnKCB.SSoChungTu = "QT-" + soChungTuIndex.ToString("D3");
                    BhQtcnKCB.DNgayChungTu = DateTime.Now;
                    BhQtcnKCB.DNgayQuyetDinh = DateTime.Now;
                    BhQtcnKCB.SMoTa = "Chi tiết chứng từ";
                }

                OnPropertyChanged(nameof(IsEnableView));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateDetailSummary(BhQtcnKCB chungTu)
        {
            _qtcnKCBChiTietService.CreateVoudcherSummary(string.Join(",", DataBhqtcnKCB.Select(n => n.Id.ToString()).ToList()), chungTu.IIdMaDonVi
                , _sessionService.Current.Principal, _sessionService.Current.YearOfWork, chungTu.Id.ToString());
        }

        private List<string> CheckSummary()
        {
            System.Linq.Expressions.Expression<Func<BhQtcnKCB, bool>> predicate = PredicateBuilder.True<BhQtcnKCB>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.SDSSoChungTuTongHop));

            List<BhQtcnKCB> chungTu = _qtcnKCBService.FindByCondition(predicate).ToList();
            List<string> listSoChungTuSummary = new List<string>();
            foreach (BhQtcnKCB item in chungTu)
            {
                listSoChungTuSummary.AddRange(item.SDSSoChungTuTongHop.Split(",").ToList());
            }
            List<string> listResult = listSoChungTuSummary.Where(x => DataBhqtcnKCB.Select(n => n.SSoChungTu).ToList().Contains(x)).ToList();
            return listResult;
        }

        public override void OnSave()
        {
            try
            {
                if (BhQtcnKCB.Id != Guid.Empty)
                {

                    BhQtcnKCB entity = _qtcnKCBService.FindById(BhQtcnKCB.Id);
                    entity.SMoTa = BhQtcnKCB.SMoTa;
                    entity.SSoQuyetDinh = BhQtcnKCB.SSoQuyetDinh;
                    _qtcnKCBService.Update(entity);

                    DialogHost.Close(SystemConstants.ROOT_DIALOG);
                    SavedAction?.Invoke(_mapper.Map<BhQtcnKCBModel>(entity));
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
                        if (DataBhqtcnKCB == null || DataBhqtcnKCB.Count() == 0)
                        {
                            return;
                        }

                        List<string> listSummaryHistory = CheckSummary();
                        if (listSummaryHistory != null && listSummaryHistory.Count > 0)
                        {
                            MessageBoxHelper.Warning(string.Format(Resources.MsgSumaryWarning, string.Join(",", listSummaryHistory)));
                            return;
                        }

                        int soChungTuIndex = _qtcnKCBService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                        BhQtcnKCB entity = new BhQtcnKCB();
                        entity.SSoChungTu = BhQtcnKCB.SSoChungTu;
                        entity.SSoQuyetDinh = BhQtcnKCB.SSoQuyetDinh;
                        entity.SMoTa = BhQtcnKCB.SMoTa;
                        entity.DNgayChungTu = BhQtcnKCB.DNgayChungTu;
                        entity.BDaTongHop = false;
                        entity.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                        entity.DNgayQuyetDinh = BhQtcnKCB.DNgayQuyetDinh;
                        entity.IIdDonVi = donVi0.Id;
                        entity.IIdMaDonVi = donVi0.IIDMaDonVi;
                        entity.INamLamViec = _sessionService.Current.YearOfWork;
                        entity.SDSSoChungTuTongHop = string.Join(",", DataBhqtcnKCB.Select(n => n.SSoChungTu).ToList());
                        entity.SDSLNS = string.Join(",", DataBhqtcnKCB.Select(n => n.SDSLNS).Distinct().ToList());
                        entity.DNgayTao = DateTime.Now;
                        entity.DNgaySua = null;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        _qtcnKCBService.Add(entity);

                        CreateDetailSummary(entity);
                        List<Core.Domain.Query.BhQtcnKCBChiTietQuery> lstDetailSummary = _qtcnKCBChiTietService.GetChiTietQuyetToanChiNamKCB(entity.Id, entity.SDSLNS, entity.INamLamViec.Value, true, entity.IIdMaDonVi, !IsDonViRoot(entity.IIdMaDonVi)).ToList();
                        if (lstDetailSummary.Count > 0)
                        {
                            entity.FTongTienDuToanGiaoNamNay = lstDetailSummary.Sum(x => x.FTienDuToanGiaoNamNay);
                            entity.FTongTienDuToanNamTruocChuyenSang = lstDetailSummary.Sum(x => x.FTienDuToanNamTruocChuyenSang);
                            entity.FTongTienTongDuToanDuocGiao = entity.FTongTienDuToanGiaoNamNay + entity.FTongTienDuToanNamTruocChuyenSang;
                            entity.FTongTienThucChi = lstDetailSummary.Sum(x => x.FTienThucChi);
                            entity.FTongTienThieu = entity.FTongTienThucChi > entity.FTongTienTongDuToanDuocGiao ? entity.FTongTienThucChi - entity.FTongTienTongDuToanDuocGiao : 0;
                            entity.FTongTienThua = entity.FTongTienTongDuToanDuocGiao > entity.FTongTienThucChi ? entity.FTongTienTongDuToanDuocGiao - entity.FTongTienThucChi : 0;
                            _qtcnKCBService.Update(entity);
                        }
                        //_qtcnKCBService.UpdateTotalCPChungTu(entity.Id.ToString(), _sessionService.Current.Principal);

                        DialogHost.CloseDialogCommand.Execute(null, null);
                        MessageBoxHelper.Info(Resources.MsgSumaryDone);
                        SavedAction?.Invoke(_mapper.Map<BhQtcnKCBModel>(entity));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (BhQtcnKCB.DNgayChungTu == null)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            //Check đã tồn chứng từ tổng hợp của đơn vị 0
            DonVi donVi0 = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            System.Linq.Expressions.Expression<Func<BhQtcnKCB, bool>> predicate = PredicateBuilder.True<BhQtcnKCB>();
            predicate = predicate.And(x => x.IIdDonVi == donVi0.Id);
            predicate = predicate.And(x => x.IIdMaDonVi == donVi0.IIDMaDonVi);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            BhQtcnKCB chungtu = _qtcnKCBService.FindByCondition(predicate).FirstOrDefault();
            if (chungtu != null)
            {
                messages.Add(string.Format(Resources.AlertExistSettlementMonthVoucher, donVi0?.TenDonVi, _sessionService.Current.YearOfWork, ""));
            }

            return string.Join(Environment.NewLine, messages);
        }
    }
}
