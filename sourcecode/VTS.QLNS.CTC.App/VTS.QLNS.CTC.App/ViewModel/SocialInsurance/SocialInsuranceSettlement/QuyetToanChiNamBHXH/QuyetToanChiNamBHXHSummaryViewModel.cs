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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH
{
    public class QuyetToanChiNamBHXHSummaryViewModel : ViewModelBase
    {
        private readonly INsDonViService _donViService;
        private readonly IQtcnBHXHService _qtcnBHXHService;
        private readonly IQtcnBHXHChiTietService _qtcnBHXHChiTietService;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public bool IsEditProcess = false;
        public override string Name => "TỔNG HỢP QUYẾT TOÁN CHI NĂM BHXH";
        public override string Title => "TỔNG HỢP";
        public override string Description => "Tổng hợp quyết toán chi năm BHXH";
        public override Type ContentType => typeof(QuyetToanChiNamBHXHSummary);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public bool IsEnableView { get; set; }
        private bool _isCapPhatToanDonVi;


        private BhQtcnBHXHModel _bhQtcnBHXH;
        public BhQtcnBHXHModel BhQtcnBHXH
        {
            get => _bhQtcnBHXH;
            set => SetProperty(ref _bhQtcnBHXH, value);
        }

        private ObservableCollection<BhQtcnBHXHModel> _dataBhqtcnBHXH;
        public ObservableCollection<BhQtcnBHXHModel> DataBhqtcnBHXH
        {
            get => _dataBhqtcnBHXH;
            set => SetProperty(ref _dataBhqtcnBHXH, value);
        }

        private BhCptuBHYTModel _selectedBhcptuBHYT;
        public BhCptuBHYTModel SelectedBhcptuBHYT
        {
            get => _selectedBhcptuBHYT;
            set
            {
                SetProperty(ref _selectedBhcptuBHYT, value);
            }
        }

        public RelayCommand SaveCommand { get; }

        public QuyetToanChiNamBHXHSummaryViewModel(
            INsDonViService donViService,
            ISessionService sessionService,
            IQtcnBHXHService qtcnBHXHService,
            IQtcnBHXHChiTietService qtcnBHXHChTietService,
            ILog logger,
            IDanhMucService danhMucService,
            IMapper mapper)
        {
            _donViService = donViService;
            _qtcnBHXHService = qtcnBHXHService;
            _qtcnBHXHChiTietService = qtcnBHXHChTietService;
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
                if (BhQtcnBHXH == null) BhQtcnBHXH = new Model.BhQtcnBHXHModel();
                if (BhQtcnBHXH.Id == Guid.Empty)
                {
                    BhQtcnBHXH = new Model.BhQtcnBHXHModel();
                    int soChungTuIndex = _qtcnBHXHService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                    BhQtcnBHXH.SSoChungTu = "QTC-" + soChungTuIndex.ToString("D3");
                    BhQtcnBHXH.DNgayChungTu = DateTime.Now;
                    BhQtcnBHXH.DNgayQuyetDinh = DateTime.Now;
                    BhQtcnBHXH.SMoTa = "Chi tiết chứng từ";
                }

                OnPropertyChanged(nameof(IsEnableView));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateDetailSummary(BhQtcnBHXH chungTu)
        {
            _qtcnBHXHChiTietService.CreateVoudcherSummary(
                string.Join(",", DataBhqtcnBHXH.Select(n => n.Id.ToString()).ToList()),
                _sessionService.Current.IdDonVi, _sessionService.Current.Principal,
                _sessionService.Current.YearOfWork, chungTu.Id.ToString());
        }

        private List<string> CheckSummary()
        {
            var predicate = PredicateBuilder.True<BhQtcnBHXH>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.SDSSoChungTuTongHop));

            List<BhQtcnBHXH> chungTu = _qtcnBHXHService.FindByCondition(predicate).ToList();
            List<string> listSoChungTuSummary = new List<string>();
            foreach (var item in chungTu)
            {
                listSoChungTuSummary.AddRange(item.SDSSoChungTuTongHop.Split(",").ToList());
            }
            List<string> listResult = listSoChungTuSummary.Where(x => DataBhqtcnBHXH.Select(n => n.SSoChungTu).ToList().Contains(x)).ToList();
            return listResult;
        }

        public override void OnSave()
        {
            try
            {
                if (BhQtcnBHXH.Id != Guid.Empty)
                {

                    BhQtcnBHXH entity = _qtcnBHXHService.FindById(BhQtcnBHXH.Id);
                    entity.SMoTa = BhQtcnBHXH.SMoTa;
                    entity.SSoQuyetDinh = BhQtcnBHXH.SSoQuyetDinh;
                    _qtcnBHXHService.Update(entity);

                    DialogHost.Close(SystemConstants.ROOT_DIALOG);
                    SavedAction?.Invoke(_mapper.Map<BhQtcnBHXHModel>(entity));
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
                        if (DataBhqtcnBHXH == null || DataBhqtcnBHXH.Count() == 0)
                        {
                            return;
                        }

                        List<string> listSummaryHistory = CheckSummary();
                        if (listSummaryHistory != null && listSummaryHistory.Count > 0)
                        {
                            MessageBoxHelper.Warning(string.Format(Resources.MsgSumaryWarning, string.Join(",", listSummaryHistory)));
                            return;
                        }

                        int soChungTuIndex = _qtcnBHXHService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                        BhQtcnBHXH entity = new BhQtcnBHXH();
                        entity.SSoChungTu = BhQtcnBHXH.SSoChungTu;
                        entity.SSoQuyetDinh = BhQtcnBHXH.SSoQuyetDinh;
                        entity.SMoTa = BhQtcnBHXH.SMoTa;
                        entity.DNgayChungTu = BhQtcnBHXH.DNgayChungTu;
                        entity.BDaTongHop = false;
                        entity.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                        entity.DNgayQuyetDinh = BhQtcnBHXH.DNgayQuyetDinh;
                        entity.IIdDonVi = donVi0.Id;
                        entity.IIdMaDonVi = donVi0.IIDMaDonVi;
                        entity.INamLamViec = _sessionService.Current.YearOfWork;
                        entity.SDSSoChungTuTongHop = string.Join(",", DataBhqtcnBHXH.Select(n => n.SSoChungTu).ToList());
                        entity.SDSLNS = LNSValue.LNS_9010001_9010002;
                        entity.DNgayTao = DateTime.Now;
                        entity.DNgaySua = null;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        _qtcnBHXHService.Add(entity);

                        CreateDetailSummary(entity);
                        var lstDetail = _qtcnBHXHChiTietService.GetChiTietQuyetToanChiNamBHXH(entity.Id, entity.INamLamViec.Value, entity.BThucChiTheo4Quy, entity.ILoaiTongHop, entity.IIdMaDonVi).ToList();
                        if (lstDetail.Count > 0)
                        {
                            entity.ITongSoSQDeNghi = lstDetail.Sum(x => x.ISoSQThucChi ?? 0);
                            entity.FTongTienSQDeNghi = lstDetail.Sum(x => x.FTienSQThucChi ?? 0);

                            entity.ITongSoQNCNDeNghi = lstDetail.Sum(x => x.ISoQNCNThucChi ?? 0);
                            entity.FTongTienQNCNDeNghi = lstDetail.Sum(x => x.FTienQNCNThucChi ?? 0);

                            entity.ITongSoCNVCQPDeNghi = lstDetail.Sum(x => x.ISoCNVCQPThucChi ?? 0);
                            entity.FTongTienCNVCQPDeNghi = lstDetail.Sum(x => x.FTienCNVCQPThucChi ?? 0);

                            entity.ITongSoLDHDDeNghi = lstDetail.Sum(x => x.ISoLDHDThucChi ?? 0);
                            entity.FTongTienLDHDDeNghi = lstDetail.Sum(x => x.FTienLDHDThucChi ?? 0);

                            entity.ITongSoHSQBSDeNghi = lstDetail.Sum(x => x.ISoHSQBSThucChi ?? 0);
                            entity.FTongTienHSQBSDeNghi = lstDetail.Sum(x => x.FTienHSQBSThucChi ?? 0);

                            entity.ITongSoDeNghi = lstDetail.Sum(x => x.ITongSoThucChi ?? 0);
                            entity.FTongTienDeNghi = lstDetail.Sum(x => x.FTongTienThucChi ?? 0);

                            entity.FTongTienDuToanDuyet = lstDetail.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienDuToanDuyet ?? 0);
                            _qtcnBHXHService.Update(entity);

                            //entity.Fto = Items.Where(x => string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienThua ?? 0);
                            //entity.FTongTienThieu = Items.Where(x => string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienThieu ?? 0);
                            //_qtcnBHXHService.UpdateTotalCPChungTu(entity.Id.ToString(), _sessionService.Current.Principal);
                        }
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        MessageBoxHelper.Info(Resources.MsgSumaryDone);
                        SavedAction?.Invoke(_mapper.Map<BhQtcnBHXHModel>(entity));
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

            if (BhQtcnBHXH.DNgayChungTu == null)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }
            //check đã tồn tại số quyết định
            var predicate_sqd = PredicateBuilder.True<BhQtcnBHXH>();
            predicate_sqd = predicate_sqd.And(x => x.SSoQuyetDinh == BhQtcnBHXH.SSoQuyetDinh);
            predicate_sqd = predicate_sqd.And(x => x.INamLamViec == BhQtcnBHXH.INamLamViec);

            var chungtu_sqd = _qtcnBHXHService.FindByCondition(predicate_sqd).FirstOrDefault();
            if (chungtu_sqd != null)
            {
                messages.Add(Resources.MsgTrungSoQD);
            }

            //Check đã tồn chứng từ tổng hợp của đơn vị 0
            DonVi donVi0 = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            var predicate = PredicateBuilder.True<BhQtcnBHXH>();
            predicate = predicate.And(x => x.IIdDonVi == donVi0.Id);
            predicate = predicate.And(x => x.IIdMaDonVi == donVi0.IIDMaDonVi);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            var chungtu = _qtcnBHXHService.FindByCondition(predicate).FirstOrDefault();
            if (chungtu != null)
            {
                messages.Add(string.Format(Resources.AlertExistSettlementMonthVoucher, donVi0?.TenDonVi, _sessionService.Current.YearOfWork, ""));
            }

            return string.Join(Environment.NewLine, messages);
        }
    }
}
