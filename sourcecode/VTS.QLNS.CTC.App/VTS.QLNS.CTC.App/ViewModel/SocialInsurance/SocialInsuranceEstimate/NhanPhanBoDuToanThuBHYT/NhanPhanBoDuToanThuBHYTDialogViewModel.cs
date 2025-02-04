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
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.NhanPhanBoDuToanThuBHYT;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanPhanBoDuToanThuBHYT
{
    public class NhanPhanBoDuToanThuBHYTDialogViewModel : DialogViewModelBase<BhDtTmBHYTTNModel>
    {
        #region Interface
        private readonly IBhDtTmBHYTTNService _bhDtTmBHYTTNService;
        private readonly IBhDtTmBHYTTNChiTietService _chiTetService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private ICollectionView _dataLNSView;
        #endregion

        #region Property
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public override Type ContentType => typeof(NhanPhanBoDuToanThuBHYTDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI" : "CẬP NHẬT";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới đợt nhận phân bổ dự toán" : "Cập nhật đợt nhận phân bổ dự toán";
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);

        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }

        private ObservableCollection<BhDmMucLucNganSachModel> _dataLNS;
        public ObservableCollection<BhDmMucLucNganSachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = DataLNS != null ? DataLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => (DataLNS == null || !DataLNS.Any()) ? false : DataLNS.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    DataLNS.Select(c => { c.IsSelected = _selectAllLNS; return c; }).ToList();
                }
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _dataLNSView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
        }

        public DateTime? DNgayChungTu { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }

        private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set
            {
                SetProperty(ref _cbxVoucherTypeSelected, value);
                if (_cbxVoucherTypeSelected != null)
                {
                    if (Model != null && Guid.Empty.Equals(Model.Id))
                    {
                        //LoadChungTuIndex();
                    }
                    LoadLNS();
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
        }

        private ComboboxItem _cbxEstimateTypeSelected;
        public ComboboxItem CbxEstimateTypeSelected
        {
            get => _cbxEstimateTypeSelected;
            set
            {
                SetProperty(ref _cbxEstimateTypeSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _cbxEstimateType;
        public ObservableCollection<ComboboxItem> CbxEstimateType
        {
            get => _cbxEstimateType;
            set => SetProperty(ref _cbxEstimateType, value);
        }
        #endregion

        #region Constructor
        public NhanPhanBoDuToanThuBHYTDialogViewModel(
            IBhDtTmBHYTTNService bhDtTmBHYTTNService,
            IBhDtTmBHYTTNChiTietService bhDtTmBHYTTNChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IMapper mapper,
            ISessionService sessionService,
            ISysAuditLogService log,
            ILog logger)
        {
            _sessionService = sessionService;
            _bhDtTmBHYTTNService = bhDtTmBHYTTNService;
            _chiTetService = bhDtTmBHYTTNChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _log = log;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Init
        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            IsSaveData = true;
            _searchLNS = string.Empty;
            LoadEstimateType();
            LoadLNS();
            LoadData();
            SelectAllLNS = true;
        }
        #endregion

        #region Load data
        public override void LoadData(params object[] args)
        {
            base.LoadData(args);

            if (Model != null && Model.Id != Guid.Empty)
            {
                SetCheckboxSelected(_dataLNS, Model.SDSLNS);
                DNgayChungTu = Model.DNgayChungTu;
                DNgayQuyetDinh = Model.DNgayQuyetDinh;
            }
            else
            {
                var soChungTuIndex = _bhDtTmBHYTTNService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                Model = new BhDtTmBHYTTNModel()
                {
                    DNgayChungTu = DateTime.Now,
                    DNgayTao = DateTime.Now,
                    DNgayQuyetDinh = DateTime.Now,
                    SSoChungTu = "DTT-" + soChungTuIndex.ToString("D3"),
                    SNguoiTao = _sessionInfo.Principal,
                    INamLamViec = _sessionInfo.YearOfWork
                };
                DNgayChungTu = DateTime.Now;
                DNgayQuyetDinh = DateTime.Now;
            }
        }

        public static void SetCheckboxSelected(ObservableCollection<BhDmMucLucNganSachModel> data, string value)
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            List<string> selectedValues = value.Split(",").ToList();
            foreach (BhDmMucLucNganSachModel item in data)
            {
                item.IsSelected = selectedValues.Contains(item.SLNS);
            }
        }

        private void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            var listMLNS = _bhDmMucLucNganSachService.GetListBhytMucLucNs(yearOfWork, BhxhMLNS.THU_MUA_BHYT).ToList();
            DataLNS = _mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(listMLNS);

            _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
            _dataLNSView.Filter = ListLNSFilter;

            if (_dataLNS != null && _dataLNS.Count > 0)
            {
                foreach (var model in _dataLNS)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsSelected))
                        {
                            foreach (var item in _dataLNS)
                            {
                                if (item.IIDMLNSCha == model.IIDMLNS)
                                {
                                    item.IsSelected = model.IsSelected;
                                }
                            }
                            OnPropertyChanged(nameof(SelectAllLNS));
                            OnPropertyChanged(nameof(SelectedCountLNS));
                        }
                    };
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (BhDmMucLucNganSachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.LNSDisplay.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadEstimateType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = EstimateType.EstimateTypeName[EstimateTypeNum.YEAR], ValueItem = ((int)EstimateTypeNum.YEAR).ToString()},
                new ComboboxItem {DisplayItem = EstimateType.EstimateTypeName[EstimateTypeNum.ADDITIONAL], ValueItem = ((int)EstimateTypeNum.ADDITIONAL).ToString()}
            };

            CbxEstimateType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            if (Model != null && Model.Id != Guid.Empty && Model.ILoaiDuToan.HasValue)
            {
                _cbxEstimateTypeSelected = CbxEstimateType.Single(item => item.ValueItem.Equals(Model.ILoaiDuToan.ToString()));
            }
            else _cbxEstimateTypeSelected = CbxEstimateType.First();
        }
        #endregion

        #region On save
        public override void OnSave()
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.Forms.MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IsSaveData = false;

            if (Model == null) Model = new BhDtTmBHYTTNModel();
            Model.SDSLNS = GetValueSelected(DataLNS);
            Model.INamLamViec = _sessionService.Current.YearOfWork;
            Model.IIDMaDonVi = _sessionService.Current.IdDonVi;
            Model.ILoaiDuToan = int.Parse(_cbxEstimateTypeSelected.ValueItem);
            Model.DNgayQuyetDinh = DNgayQuyetDinh;
            Model.DNgayChungTu = DNgayChungTu;

            BhDtTmBHYTTN entity;
            if (Model.Id == Guid.Empty)
            {
                // Add
                var soChungTuIndex = _bhDtTmBHYTTNService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                entity = new BhDtTmBHYTTN();
                _mapper.Map(Model, entity);
                entity.Id = Guid.NewGuid();
                entity.INamLamViec= _sessionService.Current.YearOfWork;
                entity.SSoChungTu = "DTT-" + soChungTuIndex.ToString("D3");
                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                _bhDtTmBHYTTNService.Add(entity);
            }
            else
            {
                entity = _bhDtTmBHYTTNService.FindById(Model.Id);
                _mapper.Map(Model, entity);
                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionService.Current.Principal;
                _bhDtTmBHYTTNService.Update(entity);
            }

            DialogHost.Close(SystemConstants.ROOT_DIALOG);

            DialogHost.CloseDialogCommand.Execute(null, null);

            // Show detail page when saved
            SavedAction?.Invoke(_mapper.Map<BhDtTmBHYTTNModel>(entity));
        }

        private string GetValueSelected(ObservableCollection<BhDmMucLucNganSachModel> dataLNS)
        {
            if (dataLNS.Count > 0)
            {
                dataLNS.ForAll(x =>
                {
                    x.IsSelected = true;
                });

                return string.Join(",", dataLNS.Where(n => n.IsSelected == true).Select(n => n.SLNS).Distinct().ToList());
            }
            return string.Empty;
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (!DNgayChungTu.HasValue)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                messages.Add(Resources.AlertSoQuyetDinhEmpty);
            }

            if (!DNgayQuyetDinh.HasValue)
            {
                messages.Add(Resources.AlertNgayQuyetDinhEmpty);
            }

            if (_cbxEstimateTypeSelected == null)
            {
                messages.Add(Resources.AlertLoaiDuToanEmpty);
            }
            return string.Join(Environment.NewLine, messages);
        }
        #endregion

        #region On close
        public override void OnClose(object obj)
        {
            try
            {
                base.OnClose(obj);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
