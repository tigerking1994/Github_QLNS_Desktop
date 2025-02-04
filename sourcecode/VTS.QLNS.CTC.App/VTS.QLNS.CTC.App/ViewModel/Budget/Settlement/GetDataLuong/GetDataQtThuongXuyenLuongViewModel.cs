using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Settlement.GetDataLuong;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.GetDataLuong
{
    public class GetDataQtThuongXuyenLuongViewModel : DialogViewModelBase<TlQtChungTuModel>
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlQtChungTuService _tlQtChungTuService;
        private readonly ITlQtChungTuChiTietService _tlQtChungTuChiTietService;
        private readonly INsQtChungTuService _iQtChungTuService;
        private readonly INsQtChungTuChiTietService _iQtChungTuChiTietService;
        private readonly ISysAuditLogService _log;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private ICollectionView _chungTuView;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(GetDataQtThuongXuyenLuong);
        public ObservableCollection<TlQtChungTuModel> ListTlQtChungTuSummary { get; set; }

        private SettlementVoucherModel _nsQtChungTuModel;
        public SettlementVoucherModel NsQtChungTuModel
        {
            get => _nsQtChungTuModel;
            set
            {
                SetProperty(ref _nsQtChungTuModel, value);
                OnPropertyChanged();
            }
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

        public GetDataQtThuongXuyenLuongViewModel(INsDonViService nsDonViService,
            ITlQtChungTuService tlQtChungTuService,
            ITlQtChungTuChiTietService tlQtChungTuChiTietService,
            INsQtChungTuService iQtChungTuService,
            INsQtChungTuChiTietService iQtChungTuChiTietService,
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            ISysAuditLogService log)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _tlQtChungTuService = tlQtChungTuService;
            _tlQtChungTuChiTietService = tlQtChungTuChiTietService;
            _iQtChungTuService = iQtChungTuService;
            _iQtChungTuChiTietService = iQtChungTuChiTietService;
            _log = log;
            _logger = logger;
            _mapper = mapper;
        }

        public override void OnSave()
        {
            var ctSelected = ListTlQtChungTuSummary.FirstOrDefault(x => x.Selected);
            if (ctSelected != null)
            {
                try
                {
                    DialogResult dialogLock = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgTransferChungTuQt), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogLock == DialogResult.Yes)
                    {
                        var ChungTuChiTiet = _tlQtChungTuChiTietService.GetDataChungTuChiTiet(ctSelected.Id.ToString(), ctSelected.Nam, string.Empty).ToList();
                        /*
                        var predicate = PredicateBuilder.True<TlQtChungTuChiTiet>();
                        predicate = predicate.And(x => ctSelected.Id.Equals(x.IdChungTu));
                        var ChungTuChiTiet = _tlQtChungTuChiTietService.FindAll(predicate);
                        */
                        //foreach (var it in ChungTuChiTiet)
                        //{
                        //    it.IdChungTu = NsQtChungTuModel.Id;
                        //    it.IdDonVi = NsQtChungTuModel.IIdMaDonVi;
                        //    it.IThangQuy = NsQtChungTuModel.IThangQuy;
                        //    it.NamLamViec = NsQtChungTuModel.INamLamViec;
                        //}
                        var chungTuChiTietNsAdd = _mapper.Map<List<SettlementVoucherDetailModel>>(ChungTuChiTiet);
                        System.Windows.Forms.MessageBox.Show(Resources.MsgChuyenDuLieuThanhCong, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogHost.Close("RegularBudgetDetailDialog");
                        SavedAction?.Invoke(chungTuChiTietNsAdd);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                }
            }
            base.OnSave();
        }

        public override void LoadData(params object[] args)
        {
            var data = _tlQtChungTuService.FindAll().OrderByDescending(x => x.Thang);
            data = data.Where(x => !string.IsNullOrEmpty(x.STongHop) && x.BKhoa && x.MaDonVi.Equals(NsQtChungTuModel.IIdMaDonVi) && x.Thang == NsQtChungTuModel.IThangQuy && x.Nam == NsQtChungTuModel.INamLamViec).OrderByDescending(x => x.Thang);
            ListTlQtChungTuSummary = _mapper.Map<ObservableCollection<TlQtChungTuModel>>(data);
            _chungTuView = CollectionViewSource.GetDefaultView(ListTlQtChungTuSummary);
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _months = FnCommonUtils.LoadMonths();
            LoadData();
        }

    }
}
