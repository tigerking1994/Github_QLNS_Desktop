using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division
{
    public class GetAdjustedEstimateViewModel : DialogViewModelBase<DcChungTuModel>
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISysAuditLogService _log;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly INsDcChungTuService _chungTuDCService;
        private readonly INsDcChungTuChiTietService _dieuChinhChiTietService;
        private readonly INsDtChungTuService _dutoanService;

        public List<DcChungTuModel> SelectedDcChungTu { get; set; }
        public int? ILoaiChungTu { get; set; }
        public override Type ContentType => typeof(GetAdjustedEstimate);
        public ObservableCollection<DcChungTuModel> ListAdjEtmVoucherSummary { get; set; }
        public DtChungTuModel EstimateVoucher { get; set; }
        public bool IsGetDataEnable => ListAdjEtmVoucherSummary.Any(x => x.Selected);
        private bool _selectedAllItem;
        public bool SelectedAllItem
        {
            get => ListAdjEtmVoucherSummary.All(x => x.Selected);
            set
            {
                SetProperty(ref _selectedAllItem, value);
                foreach (var item in ListAdjEtmVoucherSummary) item.Selected = _selectedAllItem;
                OnPropertyChanged(nameof(IsGetDataEnable));
            }
        }

        public GetAdjustedEstimateViewModel(
            ISessionService iSessionService,
            INsDonViService iNsDonViService,
            ISysAuditLogService iSysAuditLogService,
            ILog logger,
            IMapper mapper,
            SessionInfo sessionInfo,
            INsDcChungTuService chungTuDCService,
            INsDcChungTuChiTietService dieuChinhChiTietService,
            INsDtChungTuService DutoanService)
        {
            _sessionService = iSessionService;
            _nsDonViService = iNsDonViService;
            _logger = logger;
            _log = iSysAuditLogService;
            _mapper = mapper;
            _sessionInfo = sessionInfo;
            _chungTuDCService = chungTuDCService;
            _dieuChinhChiTietService = dieuChinhChiTietService;
            _dutoanService = DutoanService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
        }
        public override void LoadData(params object[] args)
        {
            List<string> lstIDAdj = new List<string>();
            var estimateVoucherId = EstimateVoucher.Id;
            var loaiNS = EstimateVoucher.IIdMaNguonNganSach;
            var listChungTu = _chungTuDCService.FindByCondition(_sessionInfo.YearOfWork, ILoaiChungTu.GetValueOrDefault(), estimateVoucherId, _sessionService.Current.YearOfBudget, loaiNS.GetValueOrDefault()).ToList();
            ListAdjEtmVoucherSummary = _mapper.Map<ObservableCollection<DcChungTuModel>>(listChungTu);
            var lstUpdatedEtmVoucher = _dutoanService.FindByCondition(x => x.INamLamViec == _sessionService.Current.YearOfWork
                                                                    && !string.IsNullOrEmpty(x.IIDChungTuDieuChinh)).ToList();
            var lstIdDuToanNhans = lstUpdatedEtmVoucher.Select(x => x.IIDChungTuDieuChinh.Split(',')).SelectMany(x => x).ToList();

            var updatedEstimateVoucher = _dutoanService.FindById(estimateVoucherId);
            if (!string.IsNullOrEmpty(updatedEstimateVoucher.IIDChungTuDieuChinh))
                lstIDAdj = updatedEstimateVoucher.IIDChungTuDieuChinh.Split(',').ToList();

            foreach (var item in ListAdjEtmVoucherSummary)
            {
                if (lstIdDuToanNhans.Any() && lstIdDuToanNhans.Contains(item.Id.ToString()))
                {
                    item.STrangThaiDieuChinh = "Đã lấy dữ liệu";
                }
                else
                {
                    item.STrangThaiDieuChinh = "Chưa lấy dữ liệu";
                }

                if (lstIDAdj.Contains(item.Id.ToString()))
                    item.Selected = true;

                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(DcChungTuModel.Selected))
                    {
                        OnPropertyChanged(nameof(SelectedAllItem));
                        OnPropertyChanged(nameof(IsGetDataEnable));
                    }
                };
            }
        }

        public override void OnSave()
        {
            var ctSelected = ListAdjEtmVoucherSummary.Where(x => x.Selected).ToList();
            if (ctSelected != null)
            {
                try
                {
                    DialogResult dialogLock = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgTransferAdjEstimte), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogLock == DialogResult.Yes)
                    {
                        List<NsDcChungTuChiTietQuery> chungTuChiTiet = new List<NsDcChungTuChiTietQuery>();
                        foreach (var ct in ctSelected)
                        {
                            var ctct = _dieuChinhChiTietService.FindByVoucherID(ct.Id).ToList();
                            chungTuChiTiet.AddRange(ctct);
                        }

                        var chungTuChiTietNsAdd = _mapper.Map<ObservableCollection<DcChungTuChiTietModel>>(chungTuChiTiet).ToList();
                        SelectedDcChungTu = ctSelected;
                        DialogHost.Close("DivisionDetailDialog");
                        SavedAction?.Invoke(chungTuChiTietNsAdd);
                        System.Windows.Forms.MessageBox.Show(Resources.MsgTransferDataDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                }
            }
            base.OnSave();
        }
    }
}
