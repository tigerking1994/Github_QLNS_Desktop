using AutoMapper;
using AutoMapper.Internal;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Hospital;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Hospital
{
    public class HospitalIndexViewModel : GridViewModelBase<DtChungTuModel>
    {
        private readonly ILogger<HospitalIndexViewModel> _logger;
        private readonly IMapper _mapper;
        private readonly INsDtChungTuService _chungTuService;
        private readonly INsDtChungTuChiTietService _chungTuChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private SessionInfo _sessionInfo;

        public override string FuncCode => NSFunctionCode.BUDGET_ESTIMATE_HOSPITAL;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Dự toán bệnh viện tự chủ";
        public override string Description => "Danh sách chứng từ dự toán bệnh viện tự chủ";
        public override Type ContentType => typeof(HospitalIndex);
        public override PackIconKind IconKind => PackIconKind.HospitalBuilding;
        public bool IsEdit => SelectedItem != null && !SelectedItem.BKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;
        public bool IsEnableLock => SelectedItem != null;
        public HospitalDialogViewModel HospitalDialogViewModel { get; set; }
        public HospitalDetailViewModel HospitalDetailViewModel { get; set; }
        public HospitalPrintReportTargetAgencyViewModel HospitalPrintReportTargetAgencyViewModel { get; set; }
        public RelayCommand PrintActionCommand { get; }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    if (Items != null)
                        Items.ForAll(c => c.Selected = value.Value);
                }
            }
        }

        public HospitalIndexViewModel(
            ILogger<HospitalIndexViewModel> logger,
            IMapper mapper,
            INsDtChungTuService chungTuService,
            INsDtChungTuChiTietService chungTuChiTietService,
            INsDonViService donViService,
            HospitalDialogViewModel hospitalDialogViewModel,
            HospitalDetailViewModel hospitalDetailViewModel,
            HospitalPrintReportTargetAgencyViewModel hospitalPrintReportTargetAgencyViewModel,
            ISessionService sessionService)
        {
            _logger = logger;
            _mapper = mapper;
            _chungTuService = chungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _donViService = donViService;
            _sessionService = sessionService;

            HospitalDialogViewModel = hospitalDialogViewModel;
            HospitalDetailViewModel = hospitalDetailViewModel;
            HospitalPrintReportTargetAgencyViewModel = hospitalPrintReportTargetAgencyViewModel;
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
        }

        private void OpenPrintDialog(object param)
        {
            var divisionEstimatePrintType = (DivisionEstimatePrintType)((int)param);
            object content = null;
            switch (divisionEstimatePrintType)
            {
                case DivisionEstimatePrintType.TARGET_AGENCY:
                    HospitalPrintReportTargetAgencyViewModel.Model = SelectedItem;
                    HospitalPrintReportTargetAgencyViewModel.Init();
                    content = new PrintReportTargetAgency
                    {
                        DataContext = HospitalPrintReportTargetAgencyViewModel
                    };
                    break;
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, DivisionEstimateScreen.ROOT_DIALOG, null, null);
            }
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            IsAllItemsSelected = false;
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                EstimationVoucherCriteria condition = new EstimationVoucherCriteria
                {
                    EstimationType = SoChungTuType.HospitalEstimate,
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    BudgetSource = _sessionInfo.Budget,
                    Status = (int)Status.ACTIVE,
                    UserName = _sessionInfo.Principal,
                    VoucherType = int.Parse(VoucherType.NSSD_Key)
                };
                e.Result = _chungTuService.FindHospitalByCondition(condition).ToList();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    // Process when run completed
                    Items = _mapper.Map<ObservableCollection<DtChungTuModel>>(e.Result);

                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }

                    foreach (var item in Items)
                    {
                        item.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(DtChungTuModel.Selected))
                                OnPropertyChanged(nameof(IsAllItemsSelected));
                        };
                    }
                }
                else
                {
                    _logger.LogError(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnableLock));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            //check quyền được chỉnh sửa
            if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedItem.SNguoiTao));
                return;
            }

            messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu.HasValue ? SelectedItem.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty);
            MessageBoxResult result = MessageBoxHelper.Confirm(messageBuilder.ToString());
            if (result == MessageBoxResult.Yes)
            {
                _chungTuChiTietService.DeleteByIdChungTu(SelectedItem.Id);
                _chungTuService.Delete(SelectedItem.Id);
                MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
                LoadData();
            }    
        }

        protected override void OnLockUnLock()
        {
            if (IsLock)
            {
                //chỉ có đơn vị cha mới được mở khóa chứng từ
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                    return;
                }
            }
            else
            {
                //chỉ có người tạo chứng từ mới được khóa chứng từ
                if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, SelectedItem.SNguoiTao));
                    return;
                }
            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult result = MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var rs = _chungTuService.LockOrUnLock(SelectedItem.Id, !SelectedItem.BKhoa);
                if (rs == DBContextSaveChangeState.SUCCESS)
                {
                    SelectedItem.BKhoa = !SelectedItem.BKhoa;
                    OnPropertyChanged(nameof(IsLock));
                    OnPropertyChanged(nameof(IsEdit));
                    MessageBoxHelper.Info(msgDone);
                }
            }
        }

        protected override void OnAdd()
        {
            //check quyền được tạo mới
            List<DonVi> userAgency = _donViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
            if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            {
                MessageBoxHelper.Warning(Resources.MsgRoleAdd);
                return;
            }
            HospitalDialogViewModel.Id = Guid.Empty;
            HospitalDialogViewModel.Init();
            HospitalDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenHospitalDetail((DtChungTuModel)obj);
            };
            HospitalDialogViewModel.ShowDialogHost();
        }

        protected override void OnUpdate()
        {
            //check quyền được chỉnh sửa
            if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedItem.SNguoiTao));
                return;
            }
            HospitalDialogViewModel.Id = SelectedItem.Id;
            HospitalDialogViewModel.Init();
            HospitalDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            HospitalDialogViewModel.ShowDialogHost();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenHospitalDetail((DtChungTuModel)obj);
        }

        private void OnOpenHospitalDetail(DtChungTuModel SelectedItem)
        {
            HospitalDetailViewModel.Model = SelectedItem;
            HospitalDetailViewModel.UpdateVoucherEvent += RefreshAfterSaveData;
            HospitalDetailViewModel.Init();
            var view = new HospitalDetail { DataContext = HospitalDetailViewModel };
            view.ShowDialog();
            HospitalDetailViewModel.UpdateVoucherEvent -= RefreshAfterSaveData;
        }

        private void RefreshAfterSaveData(object sender, EventArgs e)
        {
            DtChungTuModel model = (DtChungTuModel)sender;
            DtChungTuModel item = Items.Where(x => x.Id == model.Id).FirstOrDefault();
            item.FTongTuChi = model.FTongTuChi;
            item.FTongHienVat = model.FTongHienVat;
            item.FTongHangMua = model.FTongHangMua;
            item.FTongHangNhap = model.FTongHangNhap;
            item.FTongDuPhong = model.FTongDuPhong;
            item.FTongPhanCap = model.FTongPhanCap;
            item.FTongTonKho = model.FTongTonKho;
            item.FTongDuToan = item.FTongTuChi + item.FTongHienVat + item.FTongHangNhap + item.FTongHangMua + item.FTongDuPhong + item.FTongPhanCap + item.FTongTonKho;
            OnPropertyChanged(nameof(item.FTongDuToan));
        }
    }
}
