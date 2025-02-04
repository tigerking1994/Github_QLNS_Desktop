using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Demand;
using VTS.QLNS.CTC.App.View.Budget.Settlement.GetDataLuong;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.GetDataLuong
{
    public class GetDataQtQuanSoLuongViewModel : DialogViewModelBase<TlQsChungTuModel>
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlQsChungTuService _tlQsChungTuService;
        private readonly ITlQsChungTuChiTietService _tlQsChungTuChiTietService;
        private readonly INsQsChungTuService _iQsChungTuService;
        private readonly INsQsChungTuChiTietService _iQsChungTuChiTietService;
        private readonly ISysAuditLogService _log;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private ICollectionView _chungTuView;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(GetDataQtQuanSoLuong);
        public ObservableCollection<TlQsChungTuModel> ListTlQtChungTuSummary { get; set; }
        public string IIdDonVi { get; set; }

        private ArmyVoucherModel _nsQsChungTuModel;
        public ArmyVoucherModel NsQsChungTuModel
        {
            get => _nsQsChungTuModel;
            set
            {
                SetProperty(ref _nsQsChungTuModel, value);
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

        public GetDataQtQuanSoLuongViewModel(INsDonViService nsDonViService,
            ITlQsChungTuService tlQsChungTuService,
            ITlQsChungTuChiTietService tlQsChungTuChiTietService,
            INsQsChungTuService iQsChungTuService,
            INsQsChungTuChiTietService iQsChungTuChiTietService,
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            ISysAuditLogService log)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _tlQsChungTuService = tlQsChungTuService;
            _tlQsChungTuChiTietService = tlQsChungTuChiTietService;
            _iQsChungTuService = iQsChungTuService;
            _iQsChungTuChiTietService = iQsChungTuChiTietService;
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
                    DialogResult dialogLock = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgTransferChungTuQtQS), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogLock == DialogResult.Yes)
                    { 
                        var predicate = PredicateBuilder.True<TlQsChungTuChiTiet>();
                        predicate = predicate.And(x => ctSelected.Id.ToString().Equals(x.IdChungTu));
                        var ChungTuChiTiet = _tlQsChungTuChiTietService.FindAll(predicate);
                        //foreach (var it in ChungTuChiTiet)
                        //{
                        //    it.IdChungTu = NsQtChungTuModel.Id;
                        //    it.IdDonVi = NsQtChungTuModel.IIdMaDonVi;
                        //    it.IThangQuy = NsQtChungTuModel.IThangQuy;
                        //    it.NamLamViec = NsQtChungTuModel.INamLamViec;
                        //}
                        var ChungTuChiTietModel = _mapper.Map<ObservableCollection<TlQsChungTuChiTietModel>>(ChungTuChiTiet);
                        var chungTuChiTietNsAdd = _mapper.Map<ObservableCollection<NsQsChungTuChiTiet>>(ChungTuChiTietModel).ToList();
                        //System.Windows.Forms.MessageBox.Show(Resources.MsgChuyenDuLieuThanhCong, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogHost.Close("ArmyDetailDialog");
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
            var data = _tlQsChungTuService.FindAll().OrderByDescending(x => x.Thang);
            data = data.Where(x => !string.IsNullOrEmpty(x.STongHop) && x.IsLock.GetValueOrDefault() && x.MaDonVi.Equals(IIdDonVi) && x.Thang == NsQsChungTuModel.IThangQuy && x.Nam == NsQsChungTuModel.INamLamViec).OrderByDescending(x => x.Thang);
            ListTlQtChungTuSummary = _mapper.Map<ObservableCollection<TlQsChungTuModel>>(data);
            foreach (var model in ListTlQtChungTuSummary)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TlQtChungTuModel.Selected))
                    {
                    }
                };
            }
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
