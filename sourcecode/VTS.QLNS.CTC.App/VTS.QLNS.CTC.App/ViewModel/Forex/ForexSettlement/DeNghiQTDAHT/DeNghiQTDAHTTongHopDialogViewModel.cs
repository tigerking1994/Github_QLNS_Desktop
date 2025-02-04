using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.DeNghiQTDAHT;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.DeNghiQTDAHT
{
    public class DeNghiQTDAHTTongHopDialogViewModel : DialogViewModelBase<NhQtQuyetToanDahtModel>
    {
        private static string _sServiceName = "Chứng từ kế hoạch vốn năm";
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly INhQtQuyetToanDahtService _nhQtQuyetToanDahtService;

        public Action<object> ClosedAction;
        public override string Name => "Quản lý đề nghị quyết toán dự án hoàn thành";
        public override string Description => "Tổng hợp đề nghị quyết toán dự án hoàn thành";

        public override Type ContentType => typeof(DeNghiQTDAHTTongHopDialog);
        public bool IsDieuChinh { get; set; }
        public string sNguonVon { get; set; }
        public bool IsInsert => Model.Id == Guid.Empty;

        #region Componer

        public ObservableCollection<ComboboxItem> DataCoQuanThanhToan { get; set; }

        private ObservableCollection<NhQtQuyetToanDahtModel> _voucherAgregates;
        public ObservableCollection<NhQtQuyetToanDahtModel> VoucherAgregates
        {
            get => _voucherAgregates;
            set => SetProperty(ref _voucherAgregates, value);
        }
        #endregion

        public DeNghiQTDAHTTongHopDialogViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            INhQtQuyetToanDahtService nhQtQuyetToanDahtService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _mapper = mapper;
            _nhQtQuyetToanDahtService = nhQtQuyetToanDahtService;
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
                if (!Validate())
                {
                    return;
                }

                NhQtQuyetToanDaht nhTtDeNghiThanhToan = _mapper.Map<NhQtQuyetToanDaht>(Model);
                nhTtDeNghiThanhToan.SNguoiTao = _sessionService.Current.Principal;
                List<Guid> VoucherAgregatesIds = VoucherAgregates.Select(t => t.Id).ToList();
                _nhQtQuyetToanDahtService.TongHopQTDAHT(nhTtDeNghiThanhToan, VoucherAgregatesIds);
                Helper.MessageBoxHelper.Info("Lưu dữ liệu thành công");
                var view = obj as NhQtQuyetToanDaht;
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
            DataCoQuanThanhToan = new ObservableCollection<ComboboxItem>();
            DataCoQuanThanhToan.Add(new ComboboxItem { DisplayItem = CoQuanThanhToanEnum.TypeName.KHO_BAC, ValueItem = ((int)CoQuanThanhToanEnum.Type.KHO_BAC).ToString() });
            DataCoQuanThanhToan.Add(new ComboboxItem { DisplayItem = CoQuanThanhToanEnum.TypeName.CQTC, ValueItem = ((int)CoQuanThanhToanEnum.Type.CQTC).ToString() });
            OnPropertyChanged(nameof(DataCoQuanThanhToan));
        }

        private bool Validate()
        {
            List<string> lstError = new List<string>();
            if (string.IsNullOrEmpty(Model.SSoDeNghi?.Trim()))
            {
                lstError.Add(Resources.MsgCheckSoDeNghi);
            }
            if (!Model.DNgayDeNghi.HasValue)
            {
                lstError.Add(Resources.MsgCheckNgayDeNghi);
            }
            if (lstError.Count != 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }
            return true;
        }
    }
}

