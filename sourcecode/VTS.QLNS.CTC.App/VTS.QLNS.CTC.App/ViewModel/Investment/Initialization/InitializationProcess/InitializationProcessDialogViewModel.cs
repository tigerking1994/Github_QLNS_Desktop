using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.Initialization.InitializationProcess
{
    public class InitializationProcessDialogViewModel : DialogViewModelBase<InitializationProcessModel>
    {
        private static string[] _lstDonViInclude = new string[] { "0", "1" };
        private readonly INsDonViService _nsDonViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IVdtDaDuAnService _vdtDaDuAnService;
        private readonly ILog _logger;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IVdtKtKhoiTaoDuLieuService _vdtKtKhoiTaoService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;

        public Action<VTS.QLNS.CTC.App.Model.InitializationProcessModel> SavedAction;
        public override string Name => "Khởi tạo dự án";
        public override string Title => "Khởi tạo dự án";
        public override string Description => "Nhập thông tin khởi tạo dự án";
        public override Type ContentType => typeof(View.Investment.Initialization.InitializationProject.InitializationProjectDialog);
        public override PackIconKind IconKind => PackIconKind.Projector;
        public List<VdtDaDuAnQuery> ListDuAn;

        private ObservableCollection<ComboboxItem> _dataDonVi;
        public ObservableCollection<ComboboxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        public InitializationProcessDialogViewModel(INsDonViService nsDonViService,
                                              ISessionService sessionService,
                                              IVdtDaDuAnService vdtDaDuAnService,
                                              IProjectManagerService projectManagerService,
                                              IVdtKtKhoiTaoDuLieuService vdtKtKhoiTaoService,
                                              ILog logger,
                                              IApproveProjectService approveProjectService,
                                              IVdtDaDuToanService vdtDaDuToanService,
                                              IMapper mapper)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _vdtDaDuAnService = vdtDaDuAnService;
            _projectManagerService = projectManagerService;
            _vdtKtKhoiTaoService = vdtKtKhoiTaoService;
            _approveProjectService = approveProjectService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _mapper = mapper;
            _logger = logger;
        }

        private void ResetCondition()
        {
            if (Model == null)
                Model = new InitializationProcessModel();
            Model.NamKhoiTao = DateTime.Now.Year;
            Model.NgayKhoiTao = DateTime.Now;
            if (DataDonVi != null && DataDonVi.Count > 0)
            {
                SelectedDonVi = DataDonVi.FirstOrDefault();
            }
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedDonVi));
        }

        public override void Init()
        {
            try
            {
                ResetCondition();
                LoadDonVi();
                if (Model == null || Model.Id == Guid.Empty)
                {
                    Model = new InitializationProcessModel();
                    Model.NamKhoiTao = DateTime.Now.Year;
                    Model.NgayKhoiTao = DateTime.Now;
                    InitCombobox();
                }
                else
                {
                    VdtKtKhoiTaoDuLieu entityKhoiTao = _vdtKtKhoiTaoService.Find(Model.Id);
                    if (entityKhoiTao != null)
                    {
                        Model = _mapper.Map<InitializationProcessModel>(entityKhoiTao);

                        if (DataDonVi != null && DataDonVi.Count > 0 && entityKhoiTao.IIdDonViId.HasValue)
                        {
                            SelectedDonVi = DataDonVi.Where(n => n.HiddenValue == entityKhoiTao.IIdDonViId.ToString()).FirstOrDefault();
                        }
                    }
                    else
                    {
                        InitCombobox();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void InitCombobox()
        {
            if (DataDonVi != null && DataDonVi.Count > 0)
                SelectedDonVi = DataDonVi.FirstOrDefault();
        }

        private bool ValiDateDataInitialization()
        {
            if (Model.NamKhoiTao <= 0)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckNamKhoiTao, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Model.NgayKhoiTao == null)
            {
                System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgInputRequire, "Ngày khởi tạo"), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (SelectedDonVi == null || string.IsNullOrEmpty(SelectedDonVi.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckDonVi, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public override void OnSave()
        {
            try
            {


                VdtKtKhoiTaoDuLieu entityKhoiTao;

                if (Model == null || Model.Id == Guid.Empty)
                {
                    //add 
                    if (!ValiDateDataInitialization())
                    {
                        return;
                    }
                    DonVi donVi = _nsDonViService.FindByIdDonVi(SelectedDonVi.ValueItem, _sessionService.Current.YearOfWork);
                    entityKhoiTao = new VdtKtKhoiTaoDuLieu();
                    entityKhoiTao = _mapper.Map<VdtKtKhoiTaoDuLieu>(Model);
                    entityKhoiTao.DDateCreate = DateTime.Now;
                    entityKhoiTao.IIdDonViId = donVi.Id;
                    entityKhoiTao.IIdMaDonVi = donVi.IIDMaDonVi;
                    entityKhoiTao.SUserCreate = _sessionService.Current.Principal;
                    _vdtKtKhoiTaoService.Add(entityKhoiTao);
                }
                else
                {
                    //update khoi tao
                    if (!ValiDateDataInitialization())
                    {
                        return;
                    }
                    DonVi donVi = _nsDonViService.FindByIdDonVi(SelectedDonVi.ValueItem, _sessionService.Current.YearOfWork);
                    entityKhoiTao = _vdtKtKhoiTaoService.Find(Model.Id);
                    if (entityKhoiTao != null)
                    {
                        _mapper.Map(Model, entityKhoiTao);
                        entityKhoiTao.IIdDonViId = donVi.Id;
                        entityKhoiTao.IIdMaDonVi = donVi.IIDMaDonVi;
                        entityKhoiTao.DDateUpdate = DateTime.Now;
                        entityKhoiTao.SUserUpdate = _sessionService.Current.Principal;
                        _vdtKtKhoiTaoService.Update(entityKhoiTao);
                    }
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<VTS.QLNS.CTC.App.Model.InitializationProcessModel>(entityKhoiTao));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDonVi()
        {
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDonVi != null)
                _dataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi.Where(n => _lstDonViInclude.Contains(n.Loai)));
            else
                _dataDonVi = new ObservableCollection<ComboboxItem>();
            _dataDonVi.Insert(0, new ComboboxItem { ValueItem = string.Empty, DisplayItem = "-- Chọn đơn vị --" });
        }
    }
}
