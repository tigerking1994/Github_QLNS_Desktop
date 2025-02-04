using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.View.Forex.ForexAllocation.ForexDeNghiThanhToan;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexDeNghiThanhToan
{
    public class ForexDeNghiTTTongHopDialogViewModel : DialogViewModelBase<NhTtThanhToanModel>
    {
        private static string _sServiceName = "Chứng từ kế hoạch vốn năm";
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly INhTtThanhToanService _nhTtThanhToanService;

        public Action<object> ClosedAction;
        public override string Name => "Tổng hợp đề nghị thanh toán";

        public override Type ContentType => typeof(ForexDeNghiThanhToanTongHopDialog);
        public bool IsDieuChinh { get; set; }
        public string sNguonVon { get; set; }
        public bool IsInsert => Model.Id == Guid.Empty;

        #region Componer

        public ObservableCollection<ComboboxItem> DataCoQuanThanhToan { get; set; }

        private ObservableCollection<NhTtThanhToanModel> _voucherAgregates;
        public ObservableCollection<NhTtThanhToanModel> VoucherAgregates
        {
            get => _voucherAgregates;
            set => SetProperty(ref _voucherAgregates, value);
        }
        #endregion

        public ForexDeNghiTTTongHopDialogViewModel(
            ISessionService sessionService,
            INhTtThanhToanService nhTtThanhToanService,
            ILog logger,
            IMapper mapper)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nhTtThanhToanService = nhTtThanhToanService;
            _mapper = mapper;
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
                NhTtThanhToan vdtTtDeNghiThanhToan = _mapper.Map<NhTtThanhToan>(Model);
                vdtTtDeNghiThanhToan.SNguoiTao = _sessionService.Current.Principal;
                List<Guid> VoucherAgregatesIds = VoucherAgregates.Select(t => t.Id).ToList();
                _nhTtThanhToanService.TongHopDeNghiThanhToan(vdtTtDeNghiThanhToan, VoucherAgregatesIds);
                Helper.MessageBoxHelper.Info("Lưu dữ liệu thành công");
                var view = obj as ForexDeNghiThanhToanTongHopDialog;
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
    }
}

