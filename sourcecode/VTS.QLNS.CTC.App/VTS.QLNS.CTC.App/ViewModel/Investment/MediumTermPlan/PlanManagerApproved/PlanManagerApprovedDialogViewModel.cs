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
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PlanManagerApproved;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanManagerApproved
{
    public class PlanManagerApprovedDialogViewModel : DialogAttachmentViewModelBase<VdtKhvKeHoach5NamModel>
    {
        private static string[] lstDonViExclude = new string[] {"0"};

        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtKhvKeHoach5NamService _vdtKhvKeHoach5NamService;
        private readonly IVdtKhvKeHoach5NamDeXuatService _vdtKhvKeHoach5NamDeXuat;
        private MediumTermPlanIndexSearch _conditionSearch;

        public override string FuncCode => NSFunctionCode.INVESTMENT_MEDIUM_TERM_PLAN_PLAN_MANAGER_APPROVED_DIALOG;
        public override string Name
        {
            get
            {
                if(Model.Id == Guid.Empty)
                {
                    return "THÊM CHỨNG TỪ ĐƯỢC DUYỆT";
                }
                else
                {
                    if(IsDieuChinh)
                    {
                        return "ĐIỀU CHỈNH CHỨNG TỪ ĐƯỢC DUYỆT";
                    }
                    else
                    {
                        return "CẬP NHẬT CHỨNG TỪ ĐƯỢC DUYỆT";
                    }
                }
            }
        }
        public override string Description => "Kế hoạch trung hạn được duyệt";
        public override Type ContentType => typeof(PlanManagerApprovedDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_KHTH_DUOCDUYET;
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public bool IsEditable => (Model.Id == Guid.Empty || Model.Id == null) ? true : false;

        private string _sGiaiDoanTu;
        public string SGiaiDoanTu
        {
            get => _sGiaiDoanTu;
            set
            {
                SetProperty(ref _sGiaiDoanTu, value);
                SGiaiDoanDen = !string.IsNullOrEmpty(value) ? (Int32.Parse(_sGiaiDoanTu) + 4).ToString() : "0";
                Model.IGiaiDoanTu = !string.IsNullOrEmpty(value) ? Int32.Parse(SGiaiDoanTu) : 0;
                Model.IGiaiDoanDen = Int32.Parse(SGiaiDoanDen);
                OnPropertyChanged(nameof(SGiaiDoanDen));
            }
        }

        private string _sGiaiDoanDen;
        public string SGiaiDoanDen
        {
            get => _sGiaiDoanDen;
            set => SetProperty(ref _sGiaiDoanDen, value);
        }

        private ComboboxItem _selectedLoaiDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedLoaiDonVi;
            set => SetProperty(ref _selectedLoaiDonVi, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiDuAn;
        public ObservableCollection<ComboboxItem> ItemsLoaiDuAn
        {
            get => _itemsLoaiDuAn;
            set => SetProperty(ref _itemsLoaiDuAn, value);
        }

        private ComboboxItem _selectedLoaiDuAn;
        public ComboboxItem SelectedLoaiDuAn
        {
            get => _selectedLoaiDuAn;
            set => SetProperty(ref _selectedLoaiDuAn, value);
        }


        public PlanManagerApprovedDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtKhvKeHoach5NamService vdtKhvKeHoach5NamService,
            IVdtKhvKeHoach5NamDeXuatService vdtKhvKeHoach5NamDeXuat,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService)
            : base(mapper, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _vdtKhvKeHoach5NamService = vdtKhvKeHoach5NamService;
            _vdtKhvKeHoach5NamDeXuat = vdtKhvKeHoach5NamDeXuat;
        }

        public override void Init()
        {
            try
            {
                LoadAttach();
                LoadProjectType();
                LoadDonVi();
                LoadData();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadProjectType()
        {
            ItemsLoaiDuAn = new ObservableCollection<ComboboxItem>(new[]
            {
                new ComboboxItem(LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI), ((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI).ToString()),
                new ComboboxItem(LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.CHUYEN_TIEP), ((int)LoaiDuAnEnum.Type.CHUYEN_TIEP).ToString())
            });
        }

        private void LoadDonVi()
        {
            try
            {
                var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                     .Where(n => lstDonViExclude.Contains(n.Loai));

                ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxLoaiDonViData);
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
                if (Model.Id == Guid.Empty)
                {
                    Model.DNgayQuyetDinh = DateTime.Now;
                    //Model.IGiaiDoanTu = _sessionService.Current.YearOfWork;
                    Model.IGiaiDoanTu = _vdtKhvKeHoach5NamDeXuat.FindCurrentPeriod(_sessionService.Current.YearOfWork);                    
                    Model.IGiaiDoanDen = Model.IGiaiDoanTu + 4;
                    SGiaiDoanTu = Model.IGiaiDoanTu.ToString();
                    SGiaiDoanDen = Model.IGiaiDoanDen.ToString();
                    if (ItemsDonVi != null && ItemsDonVi.Count > 0)
                    {
                        SelectedDonVi = ItemsDonVi.FirstOrDefault();
                    }
                    if (ItemsLoaiDuAn != null && ItemsLoaiDuAn.Count > 0)
                    {
                        SelectedLoaiDuAn = ItemsLoaiDuAn.FirstOrDefault();
                    }
                }
                else
                {
                    SGiaiDoanTu = Model.IGiaiDoanTu.ToString();
                    SGiaiDoanDen = Model.IGiaiDoanDen.ToString();

                    SelectedDonVi = ItemsDonVi.Where(x => x.ValueItem.Equals(Model.IIdMaDonVi)).FirstOrDefault();
                    if (Model.ILoai != null)
                    {
                        SelectedLoaiDuAn = ItemsLoaiDuAn.FirstOrDefault(x => x.ValueItem.Equals(Model.ILoai.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave()
        {
            try
            {
                if (ValidationData()) return;

                VdtKhvKeHoach5Nam entity;
                if (Model.Id == Guid.Empty)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    if (messageBuilder.Length != 0)
                    {
                        MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                        return;
                    }

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

                    if (!lstDv.Contains(SelectedDonVi.ValueItem))
                    {
                        MessageBox.Show(string.Format(Resources.UserManagerUnitWarning, _sessionService.Current.Principal, SelectedDonVi.DisplayItem), Resources.Alert);
                        return;
                    }

                    // Thêm mới
                    entity = new VdtKhvKeHoach5Nam();
                    _mapper.Map(Model, entity);

                    entity.Id = Guid.NewGuid();
                    entity.IIdMaDonViQuanLy = _selectedLoaiDonVi.ValueItem;
                    entity.IIdDonViQuanLyId = Guid.Parse(_selectedLoaiDonVi.HiddenValue);
                    entity.ILoai = int.Parse(_selectedLoaiDuAn.ValueItem);
                    entity.NamLamViec = _sessionService.Current.YearOfWork;
                    entity.BIsGoc = true;
                    entity.BActive = true;
                    entity.BKhoa = false;
                    entity.DDateCreate = DateTime.Now;
                    entity.SUserCreate = _sessionService.Current.Principal;
                    _vdtKhvKeHoach5NamService.Add(entity);
                }
                else if (IsDieuChinh)
                {
                    // Điều chỉnh
                    entity = new VdtKhvKeHoach5Nam();
                    _mapper.Map(Model, entity);

                    entity.Id = Guid.NewGuid();
                    entity.IIdParentId = Model.Id;
                    entity.IIdMaDonViQuanLy = _selectedLoaiDonVi.ValueItem;
                    entity.IIdDonViQuanLyId = Guid.Parse(_selectedLoaiDonVi.HiddenValue);
                    entity.ILoai = int.Parse(_selectedLoaiDuAn.ValueItem);
                    entity.NamLamViec = _sessionService.Current.YearOfWork;
                    entity.BActive = true;
                    entity.BIsGoc = false;
                    entity.BKhoa = false;
                    entity.DDateCreate = DateTime.Now;
                    entity.SUserCreate = _sessionService.Current.Principal;
                    _vdtKhvKeHoach5NamService.Adjust(entity);
                }
                else
                {
                    // Cập nhật
                    entity = _vdtKhvKeHoach5NamService.FindById(Model.Id);
                    _mapper.Map(Model, entity);

                    entity.IIdMaDonViQuanLy = _selectedLoaiDonVi.ValueItem;
                    entity.IIdDonViQuanLyId = Guid.Parse(_selectedLoaiDonVi.HiddenValue);
                    entity.ILoai = int.Parse(_selectedLoaiDuAn.ValueItem);
                    entity.NamLamViec = _sessionService.Current.YearOfWork;
                    entity.BIsGoc = true;
                    entity.BActive = true;
                    entity.DDateUpdate = DateTime.Now;
                    entity.SUserUpdate = _sessionService.Current.Principal;
                    _vdtKhvKeHoach5NamService.Update(entity);
                }

                // Save attach file
                SaveAttachment(entity.Id);

                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<VdtKhvKeHoach5NamModel>(entity));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ResetValidationSearch()
        {
            _conditionSearch = new MediumTermPlanIndexSearch()
            {
                idMaDonViQuanLy = _selectedLoaiDonVi.ValueItem,
                iGiaiDoanTu = Model.IGiaiDoanTu,
                iGiaiDoanDen = Model.IGiaiDoanDen,
                iNamLamViec = _sessionService.Current.YearOfWork,
                iLoai = int.Parse(_selectedLoaiDuAn.ValueItem)
            };
        }

        private bool ValidationData()
        {
            StringBuilder messageBuilder = new StringBuilder();

            if (_selectedLoaiDonVi == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire + "\n", "Đơn vị quản lý");
            }
            if (_selectedLoaiDuAn == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire + "\n", "Loại dự án");
            }
            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire + "\n", "Số kế hoạch");
            }
            if (!Model.DNgayQuyetDinh.HasValue)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire + "\n", "Ngày lập");
            }
            if (Model.IGiaiDoanTu % 5 != 1)
            {
                messageBuilder.AppendFormat("Giai đoạn không hợp lệ ! \n");
            }

             if (!string.IsNullOrEmpty(Model.SSoQuyetDinh) && _vdtKhvKeHoach5NamService.IsExistSoQuyetDinh(Model.SSoQuyetDinh, IsDieuChinh ? Guid.Empty : Model.Id))
            {
                messageBuilder.AppendFormat(Resources.MsgTrungSoQuyetDinhs + "\n");
            }

            ResetValidationSearch();
            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                return true;
            }

            if (Model.Id.IsNullOrEmpty() && !IsDieuChinh && !CheckGiaiDoan())
            {
                MessageBox.Show(Resources.VoucherPeriodInValid, Resources.Alert);
                return true;
            }

            return false;
        }

        private bool CheckGiaiDoan()
        {
            var predicate = PredicateBuilder.True<VdtKhvKeHoach5Nam>();
            if (SelectedDonVi != null)
            {
                predicate = predicate.And(x => x.IIdMaDonViQuanLy.Equals(SelectedDonVi.ValueItem));
            }
            if (SelectedLoaiDuAn != null)
            {
                predicate = predicate.And(x => x.ILoai == int.Parse(SelectedLoaiDuAn.ValueItem));
            }
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            var rs = _vdtKhvKeHoach5NamService.FindByCondition(predicate).ToList();

            /*
             * uncheck gđ
            foreach (var item in rs)
            {
                if (Model.IGiaiDoanTu >= item.IGiaiDoanTu && Model.IGiaiDoanTu <= item.IGiaiDoanDen)
                {
                    return false;
                }
            }
            */
            return true;
        }

        public override void OnClose(object obj)
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
