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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.VonNamDonVi;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.CapPhatThanhToan;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.CapPhatThanhToan
{
    public class CapPhatThanhToanTongHopDialogViewModel : DialogViewModelBase<VdtTtDeNghiThanhToanModel>
    {
        private static string _sServiceName = "Chứng từ kế hoạch vốn năm";
        private readonly INsDonViService _nsDonViService;
        private readonly IMucLucNganSachService _mlNganSachService;
        private readonly IVdtKhvPhanBoVonDonViService _phanBoVonService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IVdtTtDeNghiThanhToanService _vdtTtDeNghiThanhToanService;

        public Action<object> ClosedAction;
        public override string Name => "Tổng hợp đề nghị thanh toán";

        public override Type ContentType => typeof(CapPhatThanhToanTongHopDialog);
        public bool IsDieuChinh { get; set; }
        public string sNguonVon { get; set; }
        public bool IsInsert => Model.Id == Guid.Empty;
        public bool IsShowDoubleClick { get; set; }

        #region Componer

        public ObservableCollection<ComboboxItem> DataCoQuanThanhToan { get; set; }

        private ObservableCollection<VdtTtDeNghiThanhToanModel> _voucherAgregates;
        public ObservableCollection<VdtTtDeNghiThanhToanModel> VoucherAgregates
        {
            get => _voucherAgregates;
            set => SetProperty(ref _voucherAgregates, value);
        }
        public bool IsDisabled { get; set; }
        #endregion

        public CapPhatThanhToanTongHopDialogViewModel(INsDonViService nsDonViService,
            IMucLucNganSachService mlNganSachService,
            ISessionService sessionService,
            IVdtKhvPhanBoVonDonViService phanBoVonService,
            INsNguonNganSachService nsNguonVonService,
            IVdtTtDeNghiThanhToanService vdtTtDeNghiThanhToanService,
            ILog logger,
            IMapper mapper)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mlNganSachService = mlNganSachService;
            _phanBoVonService = phanBoVonService;
            _nsNguonVonService = nsNguonVonService;
            _vdtTtDeNghiThanhToanService = vdtTtDeNghiThanhToanService;
            _mapper = mapper;
        }

        public override void Init()
        {
            try
            {
                IsDisabled = false;
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
                VdtTtDeNghiThanhToan vdtTtDeNghiThanhToan = _mapper.Map<VdtTtDeNghiThanhToan>(Model);
                vdtTtDeNghiThanhToan.SUserCreate = _sessionService.Current.Principal;
                List<Guid> VoucherAgregatesIds = VoucherAgregates.Select(t => t.Id).ToList();
                _vdtTtDeNghiThanhToanService.TongHopDeNghiThanhToan(vdtTtDeNghiThanhToan, VoucherAgregatesIds);
                Helper.MessageBoxHelper.Info("Lưu dữ liệu thành công");
                var view = obj as CapPhatThanhToanTongHopDialog;
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
            DataCoQuanThanhToan.Add(new ComboboxItem { DisplayItem = CoQuanThanhToanEnum.TypeName.TONKHOAN_DONVI, ValueItem = ((int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI).ToString() });
            OnPropertyChanged(nameof(DataCoQuanThanhToan));
        }
    }
}
