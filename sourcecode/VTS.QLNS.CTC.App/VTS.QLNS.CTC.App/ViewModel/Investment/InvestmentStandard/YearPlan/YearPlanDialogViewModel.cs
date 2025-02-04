using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.YearPlan
{
    public class YearPlanDialogViewModel : DialogViewModelBase<PhanBoVonModel>
    {
        private static string[] lstDonViExclude = new string[] { "0" };
        private static string _sServiceName = "Chứng từ kế hoạch vốn năm được duyệt";
        private readonly INsDonViService _nsDonViService;
        private readonly IMucLucNganSachService _mlNganSachService;
        private readonly IVdtKhvPhanBoVonService _phanBoVonService;
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvPhanBoVonChiTietService _phanBoVonChiTietService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISysAuditLogService _log;

        public Action<object> ClosedAction;
        public override string Name => "Quản lý dự toán được giao";

        private DateTime _dStartDate;
        private string _Description;
        public override string Description
        {
            get => _Description;
            set
            {
                SetProperty(ref _Description, value);
            }
        }

        private ComboboxItem _selectedLoaiDuToan;
        public ComboboxItem SelectedLoaiDuToan
        {
            get => _selectedLoaiDuToan;
            set => SetProperty(ref _selectedLoaiDuToan, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiDuToan;
        public ObservableCollection<ComboboxItem> ItemsLoaiDuToan
        {
            get => _itemsLoaiDuToan;
            set => SetProperty(ref _itemsLoaiDuToan, value);
        }


        private ObservableCollection<ComboboxItem> _drpNguonVon;
        public ObservableCollection<ComboboxItem> DrpNguonVon
        {
            get => _drpNguonVon;
            set => SetProperty(ref _drpNguonVon, value);
        }

        private ComboboxItem _drpNguonVonSelected;
        public ComboboxItem DrpNguonVonSelected
        {
            get => _drpNguonVonSelected;
            set => SetProperty(ref _drpNguonVonSelected, value);
        }

        public bool IsInsert => Model.Id == Guid.Empty;
        #region Componer
        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set
            {
                SetProperty(ref _cbxLoaiDonViSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }

        private string _iNamKeHoach;
        public string INamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }
        #endregion

        public YearPlanDialogViewModel(INsDonViService nsDonViService,
            IMucLucNganSachService mlNganSachService,
            ISessionService sessionService,
            IVdtKhvPhanBoVonService phanBoVonService,
            ILog logger,
            IMapper mapper,
            ISysAuditLogService log,
            IVdtKhvPhanBoVonChiTietService vdtKhvPhanBoVonChiTietService,
            INsNguonNganSachService nsNguonNganSachService)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mlNganSachService = mlNganSachService;
            _phanBoVonService = phanBoVonService;
            _nsNguonVonService = nsNguonNganSachService;
            _phanBoVonChiTietService = vdtKhvPhanBoVonChiTietService;
            _logger = logger;
            _mapper = mapper;
            _log = log;
        }

        public override void Init()
        {
            try
            {
                LoadLoaiDuToan();
                GetNguonVon();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                if (Model != null && Model.Id != Guid.Empty)
                {
                    Description = "Cập nhật dự toán được giao";
                    if (Model.IsAdjust) Description = "Điều chỉnh dự toán được giao";
                    _iNamKeHoach = Model.iNamKeHoach.HasValue ? Model.iNamKeHoach.Value.ToString() : string.Empty;
                    LoadComboBoxLoaiDonVi(Model.iID_MaDonViQuanLy);
                    GetNguonVon(Model.iId_NguonVonId);
                    OnPropertyChanged(nameof(Description));
                }
                else
                {
                    Model.dNgayQuyetDinh = DateTime.Now;
                    _iNamKeHoach = _sessionService.Current.YearOfWork.ToString();
                    Description = "Thêm mới dự toán được giao";
                    GetNguonVon();
                    Model.iId_NguonVonId = _sessionService.Current.Budget;
                    LoadComboBoxLoaiDonVi();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #region Relay Command
        public override void OnSave()
        {
            _dStartDate = DateTime.Now;
            string sError = string.Empty;
            StringBuilder messageBuilder = new StringBuilder();
            if (Model == null) Model = new PhanBoVonModel();
            if (CbxLoaiDonViSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị quản lý");
            }
            if (string.IsNullOrEmpty(Model.sSoQuyetDinh))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Số kế hoạch");
            }
            if (!Model.dNgayQuyetDinh.HasValue)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Ngày lập");
            }
            if (string.IsNullOrEmpty(INamKeHoach))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Năm kế hoạch");
            }
            if (DrpNguonVonSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Nguồn vốn");
            }
            if (SelectedLoaiDuToan == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Loại dự toán");
            }

            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(messageBuilder.ToString());
                LoadData();
                return;
            }

            VdtKhvPhanBoVon entityModified = new VdtKhvPhanBoVon();
            var dataInsert = _mapper.Map<VdtKhvPhanBoVon>(Model);
            dataInsert.INamKeHoach = int.Parse(INamKeHoach);
            dataInsert.ILoai = (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet;
            dataInsert.IIdNguonVonId = Int32.Parse(DrpNguonVonSelected.ValueItem);
            dataInsert.ILoaiDuToan = int.Parse(SelectedLoaiDuToan.ValueItem);
            if (dataInsert.Id == Guid.Empty)
            {
                var lstUnitManager = _sessionService.Current.IdsDonViQuanLy;
                List<string> lstDv = new List<string>();
                if (lstUnitManager.Contains(","))
                {
                    lstDv = lstUnitManager.Split(",").ToList();
                }
                else
                {
                    lstDv.Add(lstUnitManager);
                }

                if (!lstDv.Contains(CbxLoaiDonViSelected.ValueItem))
                {
                    MessageBox.Show(string.Format(Resources.VoucherUserManagerKHVNWarning, _sessionService.Current.Principal, CbxLoaiDonViSelected.DisplayItem), Resources.Alert);
                    return;
                }

                dataInsert.Id = Guid.NewGuid();
                dataInsert.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
                dataInsert.IIDMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
                dataInsert.IIdPhanBoGocId = dataInsert.Id;
                dataInsert.FGiaTrPhanBo = 0;
                if (_phanBoVonService.ExistPhanBoVonBySoQuyetDinhAndDonVi(dataInsert, (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet))
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorDuplicateSoQuyetDinh, CbxLoaiDonViSelected.DisplayItem, Model.sSoQuyetDinh);
                    messageBuilder.AppendLine();
                }
                if (messageBuilder.Length != 0)
                {
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }
                _phanBoVonService.Insert(dataInsert, _sessionService.Current.Principal, ref sError);
            }
            else
            {
                if (Model.IsAdjust)
                {
                    dataInsert.IIdParentId = dataInsert.Id;
                    dataInsert.Id = Guid.NewGuid();
                }

                if (_phanBoVonService.ExistPhanBoVonBySoQuyetDinhAndDonVi(dataInsert, (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet))
                {
                    MessageBox.Show(string.Format(Resources.MsgErrorDuplicateSoQuyetDinh, CbxLoaiDonViSelected.DisplayItem, Model.sSoQuyetDinh));
                    return;
                }

                MidiumTermPlanCriteria dataCreation = new MidiumTermPlanCriteria();

                if (Model.IsAdjust)
                {
                    dataInsert.CloneObj(entityModified);
                    entityModified.IIdPhanBoGocId = dataInsert.IIdPhanBoGocId;
                    entityModified.DDateCreate = DateTime.Now;
                    entityModified.SUserCreate = _sessionService.Current.Principal;
                    _phanBoVonService.Adjust(entityModified);
                }
                else
                {
                    _phanBoVonService.UpdatePhanBoVon(dataInsert, _sessionService.Current.Principal, ref sError, ref dataCreation);
                }
            }
            _log.WriteLog(Resources.ApplicationName, _sServiceName, Model.ActionState, _dStartDate, TransactionStatus.Success, _sessionService.Current.Principal);
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (Model.IsAdjust)
            {
                SavedAction?.Invoke(entityModified);
            }
            else
            {
                SavedAction?.Invoke(dataInsert);
            }
        }
        #endregion

        #region Helper
        public override void OnClose(object obj)
        {
            try
            {
                base.OnClose(obj);
                DialogHost.CloseDialogCommand.Execute(null, null);
                ClosedAction?.Invoke(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void LoadComboBoxLoaiDonVi(string iIdDonVi = null)
        {
            try
            {
                var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => lstDonViExclude.Contains(n.Loai) && (string.IsNullOrEmpty(iIdDonVi) || n.IIDMaDonVi == iIdDonVi))
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = string.Format("{0} - {1}", n.IIDMaDonVi, n.TenDonVi), HiddenValue = n.Id.ToString() });
                _cbxLoaiDonVi = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
                if (!string.IsNullOrEmpty(iIdDonVi))
                {
                    CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault(n => n.ValueItem.ToUpper() == iIdDonVi.ToUpper());
                }
                else
                {
                    CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault();
                }
                OnPropertyChanged(nameof(CbxLoaiDonVi));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetNguonVon(int? iIdNguonVon = null)
        {
            try
            {
                var cbxNguonVonData = _nsNguonVonService.FindNguonNganSach().OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
                _drpNguonVon = new ObservableCollection<ComboboxItem>(cbxNguonVonData);
                if (iIdNguonVon.HasValue)
                {
                    DrpNguonVonSelected = _drpNguonVon.FirstOrDefault(n => n.ValueItem == iIdNguonVon.Value.ToString());
                }
                else
                {
                    DrpNguonVonSelected = _drpNguonVon.FirstOrDefault();
                }
                OnPropertyChanged(nameof(DrpNguonVon));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadLoaiDuToan()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem() { DisplayItem = LoaiDuToan.Name.DAU_NAM, ValueItem = ((int)LoaiDuToan.Type.DAU_NAM).ToString() });
            lstData.Add(new ComboboxItem() { DisplayItem = LoaiDuToan.Name.BO_XUNG, ValueItem = ((int)LoaiDuToan.Type.BO_XUNG).ToString() });
            lstData.Add(new ComboboxItem() { DisplayItem = LoaiDuToan.Name.NAM_TRUOC_CHUYEN_SANG, ValueItem = ((int)LoaiDuToan.Type.NAM_TRUOC_CHUYEN_SANG).ToString() });
            ItemsLoaiDuToan = new ObservableCollection<ComboboxItem>(lstData);
            SelectedLoaiDuToan = ItemsLoaiDuToan.FirstOrDefault(n => n.ValueItem == Model.ILoaiDuToan.ToString());
            OnPropertyChanged(nameof(ItemsLoaiDuToan));
            OnPropertyChanged(nameof(SelectedLoaiDuToan));
        }
        #endregion
    }
}
