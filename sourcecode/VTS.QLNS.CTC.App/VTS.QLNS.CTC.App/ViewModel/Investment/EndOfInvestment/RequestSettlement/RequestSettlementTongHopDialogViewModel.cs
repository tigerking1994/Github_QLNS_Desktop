using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.RequestSettlement;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.RequestSettlement
{
    public class RequestSettlementTongHopDialogViewModel : DialogViewModelBase<DeNghiQuyetToanModel>
    {
        private static string _sServiceName = "Chứng từ kế hoạch vốn năm";
        private readonly INsDonViService _nsDonViService;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IVdtDeNghiQuyetToanService _vdtDeNghiQuyetToanService;

        public Action<object> ClosedAction;
        public override string Name => "Quản lý kế hoạch vốn năm";

        public override Type ContentType => typeof(RequestSettlementTongHopDialog);
        public bool IsDieuChinh { get; set; }
        public string sNguonVon { get; set; }
        public bool IsInsert => Model.Id == Guid.Empty;

        #region Componer

        public ObservableCollection<ComboboxItem> DataCoQuanThanhToan { get; set; }

        private ObservableCollection<DeNghiQuyetToanModel> _voucherAgregates;
        public ObservableCollection<DeNghiQuyetToanModel> VoucherAgregates
        {
            get => _voucherAgregates;
            set => SetProperty(ref _voucherAgregates, value);
        }
        public bool IsDisabled { get; internal set; }
        #endregion

        public RequestSettlementTongHopDialogViewModel(INsDonViService nsDonViService,
            ISessionService sessionService,
            IVdtDeNghiQuyetToanService vdtDeNghiQuyetToanService,
            ILog logger,
            IMapper mapper)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mapper = mapper;
            _vdtDeNghiQuyetToanService = vdtDeNghiQuyetToanService;
        }

        public override void Init()
        {
            try
            {
                LoadCoQuanThanhToan();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        #region Relay Command
        public override void OnSave(object obj)
        {
            try
            {
                VdtQtDeNghiQuyetToan vdtTtDeNghiQT = _mapper.Map<VdtQtDeNghiQuyetToan>(Model);
                vdtTtDeNghiQT.SUserCreate = _sessionService.Current.Principal;
                List<Guid> VoucherAgregatesIds = VoucherAgregates.Select(t => t.Id).ToList();
                _vdtDeNghiQuyetToanService.TongHopDeNghiQuyetToan(vdtTtDeNghiQT, VoucherAgregatesIds);
                Helper.MessageBoxHelper.Info("Lưu dữ liệu thành công");
                var view = obj as RequestSettlementTongHopDialog;
                DialogHost.Close(view);
                SavedAction?.Invoke(null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        public override void OnClose(object obj)
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                ClosedAction?.Invoke(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadCoQuanThanhToan()
        {
            
        }
    }
}
