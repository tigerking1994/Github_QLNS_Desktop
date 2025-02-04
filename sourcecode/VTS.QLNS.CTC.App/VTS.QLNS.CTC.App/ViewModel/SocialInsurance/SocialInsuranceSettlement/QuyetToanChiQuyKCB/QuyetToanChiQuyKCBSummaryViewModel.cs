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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB
{
    public class QuyetToanChiQuyKCBSummaryViewModel : ViewModelBase
    {
        private readonly INsDonViService _donViService;
        private readonly IQtcqKCBService _qtcqKCBService;
        private readonly IQtcqKCBChiTietService _qtcqKCBChiTietService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public bool IsEditProcess = false;
        public override string Name => "TỔNG HỢP QUYẾT TOÁN CHI QUÝ KCB TẠI QUÂN Y ĐƠN VỊ";
        public override string Title => "TỔNG HỢP";
        public override string Description => "Tổng hợp quyết toán chi KCB tại quân y đơn vị";
        public override Type ContentType => typeof(QuyetToanChiQuyKCBSummary);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public bool IsEnableView { get; set; }
        private bool _isCapPhatToanDonVi;


        private BhQtcqKCBModel _bhQtcqKCB;
        public BhQtcqKCBModel BhQtcqKCB
        {
            get => _bhQtcqKCB;
            set => SetProperty(ref _bhQtcqKCB, value);
        }

        private ObservableCollection<BhQtcqKCBModel> _dataBhqtcqKCB;
        public ObservableCollection<BhQtcqKCBModel> DataBhqtcqKCB
        {
            get => _dataBhqtcqKCB;
            set => SetProperty(ref _dataBhqtcqKCB, value);
        }



        public RelayCommand SaveCommand { get; }

        public QuyetToanChiQuyKCBSummaryViewModel(
            INsDonViService donViService,
            ISessionService sessionService,
            IQtcqKCBService qtcqKCBService,
            IQtcqKCBChiTietService qtcqKCBChTietService,
            ILog logger,
            IDanhMucService danhMucService,
            IMapper mapper,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService)
        {
            _donViService = donViService;
            _qtcqKCBService = qtcqKCBService;
            _qtcqKCBChiTietService = qtcqKCBChTietService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _mapper = mapper;
            _logger = logger;
            SaveCommand = new RelayCommand(o => OnSave());
        }

        public override void Init()
        {
            try
            {
                IsEnableView = true;
                if (BhQtcqKCB == null) BhQtcqKCB = new Model.BhQtcqKCBModel();
                if (BhQtcqKCB.Id == Guid.Empty)
                {
                    BhQtcqKCB = new Model.BhQtcqKCBModel();
                    int soChungTuIndex = _qtcqKCBService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                    BhQtcqKCB.SSoChungTu = "QTC-" + soChungTuIndex.ToString("D3");
                    BhQtcqKCB.DNgayChungTu = DateTime.Now;
                    BhQtcqKCB.DNgayQuyetDinh = DateTime.Now;
                    BhQtcqKCB.SMoTa = "Chi tiết";
                }

                OnPropertyChanged(nameof(IsEnableView));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateDetailSummary(BhQtcqKCB chungTu)
        {
            _qtcqKCBChiTietService.CreateVoudcherSummary(string.Join(",", DataBhqtcqKCB.Select(n => n.Id.ToString()).ToList())
                , _sessionService.Current.Principal, _sessionService.Current.YearOfWork, chungTu.Id.ToString(), chungTu.IIdMaDonVi);
        }

        private List<string> CheckSummary()
        {
            var predicate = PredicateBuilder.True<BhQtcqKCB>();
            predicate = predicate.And(x => x.INamChungTu == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.SDSSoChungTuTongHop));

            List<BhQtcqKCB> chungTu = _qtcqKCBService.FindByCondition(predicate).ToList();
            List<string> listSoChungTuSummary = new List<string>();
            foreach (var item in chungTu)
            {
                listSoChungTuSummary.AddRange(item.SDSSoChungTuTongHop.Split(",").ToList());
            }
            List<string> listResult = listSoChungTuSummary.Where(x => DataBhqtcqKCB.Select(n => n.SSoChungTu).ToList().Contains(x)).ToList();
            return listResult;
        }

        public override void OnSave()
        {
            try
            {
                if (BhQtcqKCB.Id != Guid.Empty)
                {

                    BhQtcqKCB entity = _qtcqKCBService.FindById(BhQtcqKCB.Id);
                    entity.DNgayChungTu = BhQtcqKCB.DNgayChungTu;
                    entity.DNgayQuyetDinh = BhQtcqKCB.DNgayQuyetDinh;
                    entity.SMoTa = BhQtcqKCB.SMoTa;
                    entity.SSoQuyetDinh = BhQtcqKCB.SSoQuyetDinh;
                    _qtcqKCBService.Update(entity);

                    DialogHost.Close(SystemConstants.ROOT_DIALOG);
                    SavedAction?.Invoke(_mapper.Map<BhQtcqKCBModel>(entity));
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
                        if (DataBhqtcqKCB == null || DataBhqtcqKCB.Count() == 0)
                        {
                            return;
                        }

                        List<string> listSummaryHistory = CheckSummary();
                        if (listSummaryHistory != null && listSummaryHistory.Count > 0)
                        {
                            MessageBoxHelper.Warning(string.Format(Resources.MsgSumaryWarning, string.Join(",", listSummaryHistory)));
                            return;
                        }

                        int soChungTuIndex = _qtcqKCBService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                        BhQtcqKCB entity = new BhQtcqKCB();
                        entity.SSoChungTu = BhQtcqKCB.SSoChungTu;
                        entity.SSoQuyetDinh = BhQtcqKCB.SSoQuyetDinh;
                        entity.SMoTa = BhQtcqKCB.SMoTa;
                        entity.DNgayChungTu = BhQtcqKCB.DNgayChungTu;
                        entity.BDaTongHop = false;
                        entity.ILoaiTongHop = BhxhLoaiChungTu.BhxhChungTuTongHop;
                        entity.DNgayQuyetDinh = BhQtcqKCB.DNgayQuyetDinh;
                        entity.IIdDonVi = donVi0.Id;
                        entity.IIdMaDonVi = donVi0.IIDMaDonVi;
                        entity.INamChungTu = _sessionService.Current.YearOfWork;
                        entity.SDSSoChungTuTongHop = string.Join(",", DataBhqtcqKCB.Select(n => n.SSoChungTu).ToList());
                        entity.IQuyChungTu = DataBhqtcqKCB.Select(x => x.IQuyChungTu).First();
                        entity.SDSLNS = string.Join(",", DataBhqtcqKCB.Select(n => n.SDSLNS).ToList());
                        entity.DNgayTao = DateTime.Now;
                        entity.DNgaySua = null;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        _qtcqKCBService.Add(entity);
                        CreateDetailSummary(entity);

                        var danhMucLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(entity.INamChungTu).ToList();
                        var loaiChi = danhMucLoaiChi.Where(x => x.SLNS.Equals(LNSValue.LNS_9010004_9010005)).FirstOrDefault();
                        var lstDetailSummary = _qtcqKCBChiTietService.GetChiTietQuyetToanChiQuyKCB(entity.Id, loaiChi.Id, entity.SDSLNS, loaiChi.SMaLoaiChi, entity.IIdMaDonVi, entity.DNgayChungTu,
                                                                                                   entity.IQuyChungTu, entity.INamChungTu, entity.ILoaiTongHop).ToList();
                        if (lstDetailSummary.Count > 0)
                        {
                            entity.FTongTienDuToanGiaoNamNay = lstDetailSummary.Sum(x => x.FTienDuToanGiaoNamNay);
                            entity.FTongTienDuToanNamTruocChuyenSang = lstDetailSummary.Sum(x => x.FTienDuToanNamTruocChuyenSang);
                            entity.FTongTienTongDuToanDuocGiao = entity.FTongTienDuToanGiaoNamNay + entity.FTongTienDuToanNamTruocChuyenSang;
                            entity.FTongTienThucChi = lstDetailSummary.Sum(x => x.FTienThucChi);
                            entity.FTongTienQuyetToanDaDuyet = lstDetailSummary.Sum(x => x.FTienQuyetToanDaDuyet);
                            entity.FTongTienDeNghiQuyetToanQuyNay = lstDetailSummary.Sum(x => x.FTienDeNghiQuyetToanQuyNay);
                            entity.FTongTienXacNhanQuyetToanQuyNay = lstDetailSummary.Sum(x => x.FTienXacNhanQuyetToanQuyNay);
                            _qtcqKCBService.Update(entity);
                        }

                        //_qtcqKCBService.UpdateTotalCPChungTu(entity.Id.ToString(), _sessionService.Current.Principal);

                        DialogHost.CloseDialogCommand.Execute(null, null);
                        MessageBoxHelper.Info(Resources.MsgSumaryDone);
                        SavedAction?.Invoke(_mapper.Map<BhQtcqKCBModel>(entity));

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

            if (BhQtcqKCB.DNgayChungTu == null)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            //check đã tồn tại số quyết định
            var predicate_sqd = PredicateBuilder.True<BhQtcqKCB>();
            predicate_sqd = predicate_sqd.And(x => x.SSoQuyetDinh == BhQtcqKCB.SSoQuyetDinh);
            predicate_sqd = predicate_sqd.And(x => x.INamChungTu == BhQtcqKCB.INamChungTu);
            predicate_sqd = predicate_sqd.And(x => x.IQuyChungTu == BhQtcqKCB.IQuyChungTu);

            var chungtu_sqd = _qtcqKCBService.FindByCondition(predicate_sqd).FirstOrDefault();
            if (chungtu_sqd != null)
            {
                messages.Add(Resources.MsgTrungSoQD);
            }

            //Check đã tồn chứng từ tổng hợp của đơn vị 0
            DonVi donVi0 = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            var predicate = PredicateBuilder.True<BhQtcqKCB>();
            predicate = predicate.And(x => x.IIdDonVi == donVi0.Id);
            predicate = predicate.And(x => x.IIdMaDonVi == donVi0.IIDMaDonVi);
            predicate = predicate.And(x => x.INamChungTu == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IQuyChungTu == DataBhqtcqKCB.Select(x => x.IQuyChungTu).First());

            var chungtu = _qtcqKCBService.FindByCondition(predicate).FirstOrDefault();
            if (chungtu != null)
            {
                messages.Add(string.Format(Resources.AlertExistSettlementMonthVoucher, donVi0?.TenDonVi, _sessionService.Current.YearOfWork, ""));
            }

            return string.Join(Environment.NewLine, messages);
        }
    }
}
