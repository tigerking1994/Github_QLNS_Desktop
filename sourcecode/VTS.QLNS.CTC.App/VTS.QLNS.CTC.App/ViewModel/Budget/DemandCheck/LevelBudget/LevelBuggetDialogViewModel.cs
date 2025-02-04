using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Domain;
using AutoMapper;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using System.ComponentModel;
using System.Windows.Data;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Properties;
using System.Windows.Forms;
using log4net;
using VTS.QLNS.CTC.Utility.Enum;
using System.Linq.Expressions;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.LevelBudget
{
    public class LevelBuggetDialogViewModel : DialogViewModelBase<VTS.QLNS.CTC.App.Model.LevelBuggetModel>
    {
        private ICpDanhMucService _cpDanhMucService;
        private INsDonViService _nSDonViService;
        private INsMucLucNganSachService _nSMucLucNganSachService;
        private ILbChungTuService _chungTuService;
        private ISessionService _sessionService;
        private IDanhMucService _danhMucService;
        private INsNguoiDungDonViService _nsNguoiDungDonViService;
        private INsNguoiDungLnsService _ngNguoiDungLNSService;
        private IMapper _mapper;
        private ICollectionView _listLNSView;
        private ICollectionView _listDonViView;
        private readonly ILog _logger;

        public bool IsEditProcess = false;

        public override string Name => "THÊM MỚI PHÂN CẤP";
        public override string Title => IsEditProcess ? "SỬA CHỨNG TỪ" : "THÊM CHỨNG TỪ";
        public override string Description => IsEditProcess ? "Sửa chứng từ phân cấp" : "Thêm mới chứng từ phân cấp";
        public string IconMode => IsEditProcess ? "Edit" : "PlaylistPlus";
        public override Type ContentType => typeof(View.Budget.DemandCheck.LevelBudget.LevelBuggetDialog);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public List<DonVi> ListDataDonVi;
        public string SelectedCountLNS
        {
            get
            {
                int totalCount = ListLNS != null ? ListLNS.Count : 0;
                int totalSelected = ListLNS != null ? ListLNS.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
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
                    _listLNSView.Refresh();
                }
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => _selectAllLNS;
            set
            {
                SetProperty(ref _selectAllLNS, value);
                foreach (CheckBoxItem item in ListLNS)
                {
                    item.IsChecked = _selectAllLNS;
                }
            }
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _listDonViView.Refresh();
                }
            }
        }

        private ObservableCollection<CheckBoxItem> _listDonVi;
        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }

        private ObservableCollection<CheckBoxTreeItem> _listLNS;
        public ObservableCollection<CheckBoxTreeItem> ListLNS
        {
            get => _listLNS;
            set => SetProperty(ref _listLNS, value);
        }

        private ObservableCollection<ComboboxItem> _dataNguonNganSach;
        public ObservableCollection<ComboboxItem> DataNguonNganSach
        {
            get => _dataNguonNganSach;
            set => SetProperty(ref _dataNguonNganSach, value);
        }

        private ComboboxItem _selectedNguonNganSach;
        public ComboboxItem SelectedNguonNganSach
        {
            get => _selectedNguonNganSach;
            set
            {
                if (SetProperty(ref _selectedNguonNganSach, value) && _selectedNguonNganSach != null)
                {
                    LoadLNS();
                    if (ListDonVi != null && ListDonVi.Count() > 0)
                    {
                        ListDonVi.Where(n => n.IsChecked).Select(n => { n.IsChecked = false; return n; }).ToList();
                    }
                }
            }
        }

        public LevelBuggetDialogViewModel(ICpDanhMucService cpDanhMucService,
            INsDonViService nSDonViService,
            INsMucLucNganSachService nSMucLucNganSachService,
            ILbChungTuService chungTuService,
            ISessionService sessionService,
            IDanhMucService danhMucService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            INsNguoiDungLnsService ngNguoiDungLNSService,
            ILog logger,
            IMapper mapper)
        {
            _cpDanhMucService = cpDanhMucService;
            _nSDonViService = nSDonViService;
            _nSMucLucNganSachService = nSMucLucNganSachService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _chungTuService = chungTuService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _ngNguoiDungLNSService = ngNguoiDungLNSService;
            _mapper = mapper;
            _logger = logger;
        }

        public override void Init()
        {
            try
            {
                LoadNganSach();
                LoadLNS();
                LoadDonVi();
                if (Model == null) Model = new Model.LevelBuggetModel();
                if (Model.Id == Guid.Empty)
                {
                    // Add
                    Model = new Model.LevelBuggetModel();
                    int soChungTuIndex = _chungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork, _sessionService.Current.Budget);
                    Model.SoChungTu = "PB-" + soChungTuIndex.ToString("D3");
                    Model.NgayChungTu = DateTime.Now;
                    Model.NgayQuyetDinh = DateTime.Now;
                    SelectedNguonNganSach = DataNguonNganSach.FirstOrDefault();
                }
                else
                {
                    // Update
                    if (Model.LoaiChungTu.HasValue)
                        SelectedNguonNganSach = DataNguonNganSach.Where(n => int.Parse(n.ValueItem) == Model.LoaiChungTu.Value).FirstOrDefault();
                    else
                        SelectedNguonNganSach = DataNguonNganSach.FirstOrDefault();
                    CheckBoxItem itemDonVi = ListDonVi.Where(n => n.ValueItem == Model.IdDonVi).FirstOrDefault();
                    if (itemDonVi != null)
                    {
                        itemDonVi.IsChecked = true;
                    }
                    CheckboxSelectedToStringConvert.SetCheckboxSelected(ListLNS, Model.Lns);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadNganSach()
        {
            DataNguonNganSach = new ObservableCollection<ComboboxItem>();
            DataNguonNganSach.Add(new ComboboxItem { ValueItem = NguonNganSach.NSQP.ToString(), DisplayItem = NguonNganSach.TEN_NSQQ });
            DataNguonNganSach.Add(new ComboboxItem { ValueItem = NguonNganSach.NSK.ToString(), DisplayItem = NguonNganSach.TEN_NSK });
        }

        public void LoadDonVi()
        {
            ListDonVi = new ObservableCollection<CheckBoxItem>();
            var predicate = CreatePredicate();
            ListDataDonVi = _nSDonViService.FindByCondition(predicate).ToList();
            ListDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(ListDataDonVi);
            foreach (CheckBoxItem model in ListDonVi)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        LoadLNS();
                    }
                };
            }
            // Filter
            _listDonViView = CollectionViewSource.GetDefaultView(ListDonVi);
            _listDonViView.Filter = ListDonViFilter;
        }

        private Expression<Func<DonVi, bool>> CreatePredicate()
        {
            var session = _sessionService.Current;
            var predicate = PredicateBuilder.True<DonVi>();
            List<string> nguoiDungDonVi = GetListNguoiDungDonVi();

            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT) && x.BCoNSNganh);
            predicate = predicate.And(x => nguoiDungDonVi != null && nguoiDungDonVi.Contains(x.IIDMaDonVi));
            return predicate;
        }

        private List<string> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.IIDMaNguoiDung == _sessionService.Current.Principal);
            List<NguoiDungDonVi> listNguoiDung = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return (listNguoiDung != null && listNguoiDung.Count > 0) ? listNguoiDung.Select(n => n.IIdMaDonVi).ToList() : new List<string>();
        }

        private List<string> GetListNguoiDungLNS()
        {
            var predicate = PredicateBuilder.True<NsNguoiDungLns>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.SMaNguoiDung == _sessionService.Current.Principal);
            List<NsNguoiDungLns> listNguoiDung = _ngNguoiDungLNSService.FindAll(predicate).ToList();
            return (listNguoiDung != null && listNguoiDung.Count > 0) ? listNguoiDung.Select(n => n.SLns).ToList() : new List<string>();
        }

        private bool ListDonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchDonVi))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.ValueItem.ToLower().Contains(_searchDonVi.Trim()!.ToLower());
        }

        private List<NsMucLucNganSach> GetListMLNSMaDonVi(string maDonVi)
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.IdMaDonVi) && x.IdMaDonVi == maDonVi);
            List<NsMucLucNganSach> listLNS = _nSMucLucNganSachService.FindByCondition(predicate).OrderBy(n => n.XauNoiMa).ToList();
            return listLNS;
        }

        public void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            string idDonVi = _sessionService.Current.IdDonVi;
            var predicate = _nSMucLucNganSachService.createPredicateAllNull();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);

            CheckBoxItem itemDonVi = (ListDonVi != null && ListDonVi.Count > 0) ? ListDonVi.Where(n => n.IsChecked).FirstOrDefault() : null;
            if (itemDonVi != null && ListDataDonVi != null)
            {
                DonVi donvi = ListDataDonVi.Where(n => n.IIDMaDonVi == itemDonVi.ValueItem).FirstOrDefault();
                if (donvi != null && donvi.Loai == LoaiDonVi.NOI_BO)
                {
                    List<string> listNguoiDungLNS = GetListNguoiDungLNS();
                    listNguoiDungLNS = StringUtils.GetListXauNoiMaParent(listNguoiDungLNS);
                    predicate = predicate.And(x => listNguoiDungLNS != null && listNguoiDungLNS.Contains(x.Lns));
                }
            }
            if (itemDonVi == null && SelectedNguonNganSach != null && SelectedNguonNganSach.ValueItem == NguonNganSach.NSK.ToString())
            {
                predicate = predicate.And(n => false);
            }
            if (SelectedNguonNganSach != null && SelectedNguonNganSach.ValueItem == NguonNganSach.NSQP.ToString())
            {
                predicate = predicate.And(n => n.XauNoiMa.StartsWith("104"));
            }
            else if (itemDonVi != null)
            {
                DonVi donvi = ListDataDonVi.Where(n => n.IIDMaDonVi == itemDonVi.ValueItem).FirstOrDefault();
                if (donvi != null)
                {
                    List<NsMucLucNganSach> listMLNSMaDonVi = GetListMLNSMaDonVi(donvi.IIDMaDonVi);
                    if (listMLNSMaDonVi != null && listMLNSMaDonVi.Count > 0)
                    {
                        List<string> listLNSDonVi = StringUtils.GetListXauNoiMaParent(listMLNSMaDonVi.Select(n => n.Lns).ToList());
                        predicate = predicate.And(n => listLNSDonVi.Contains(n.Lns));
                    }
                    else
                    {
                        predicate = predicate.And(n => false);
                    }
                }
                predicate = predicate.And(n => ((n.XauNoiMa.StartsWith("2") || n.XauNoiMa.StartsWith("4") || n.XauNoiMa.StartsWith("109"))));
            }

            IEnumerable<NsMucLucNganSach> listLNS = _nSMucLucNganSachService.FindByCondition(predicate).OrderBy(n => n.XauNoiMa);
            if (listLNS == null || listLNS.Count() == 0)
            {
                ListLNS = new ObservableCollection<CheckBoxTreeItem>();
                OnPropertyChanged(nameof(SelectedCountLNS));
                OnPropertyChanged(nameof(SelectAllLNS));
                return;
            }
            ListLNS = new ObservableCollection<CheckBoxTreeItem>();
            ListLNS = _mapper.Map<ObservableCollection<CheckBoxTreeItem>>(listLNS);
            // Filter
            _listLNSView = CollectionViewSource.GetDefaultView(ListLNS);
            _listLNSView.Filter = ListLNSFilter;
            foreach (CheckBoxTreeItem model in ListLNS)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        FindChildCheckbox(model);
                        OnPropertyChanged(nameof(SelectedCountLNS));
                        OnPropertyChanged(nameof(SelectAllLNS));
                    }
                };
            }
            OnPropertyChanged(nameof(SelectedCountLNS));
        }

        public void FindChildCheckbox(CheckBoxTreeItem parent)
        {
            ListLNS.Where(n => n.ParentId == parent.Id).Select(n => { n.IsChecked = parent.IsChecked; return n; }).ToList();
            if (ListLNS.Where(n => n.ParentId == parent.Id && n.IsChecked == !parent.IsChecked).ToList().Count == 0)
            {
                return;
            }
            else
            {
                foreach (CheckBoxTreeItem item in ListLNS.Where(n => n.ParentId == parent.Id))
                {
                    FindChildCheckbox(item);
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.ValueItem.ToLower().Contains(_searchLNS.Trim()!.ToLower());
        }

        public override void OnSave()
        {
            try
            {
                if (ListDonVi == null || ListDonVi.Count == 0 || ListDonVi.Where(n => n.IsChecked).Count() == 0)
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                if (ListLNS == null || ListLNS.Where(n => n.IsChecked).Count() == 0)
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckLNS);
                    return;
                }
                //if (string.IsNullOrEmpty(Model.SoCongVan) || string.IsNullOrEmpty(Model.SoCongVan.Trim()))
                //{
                //    MessageBox.Show(string.Format(Resources.MsgInputRequire, "Số công văn"), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                CheckBoxItem itemDonVi = ListDonVi.Where(n => n.IsChecked).FirstOrDefault();
                Model.LoaiChungTu = int.Parse(SelectedNguonNganSach.ValueItem);
                Model.Lns = CheckboxSelectedToStringConvert.GetValueSelected(ListLNS);
                Model.NamLamViec = _sessionService.Current.YearOfWork;
                Model.NguonNganSach = _sessionService.Current.Budget;
                Model.NamNganSach = _sessionService.Current.YearOfBudget;
                Model.IdDonVi = itemDonVi.ValueItem;
                Model.TenDonVi = itemDonVi.NameItem;
                NsNganhChungTu entity;
                if (Model.Id != Guid.Empty)
                {
                    // Update
                    entity = _chungTuService.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    _chungTuService.Update(entity);
                }
                else
                {
                    // Add
                    int soChungTuIndex = _chungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork, _sessionService.Current.Budget);
                    Model.IdDonViTao = _sessionService.Current.IdDonVi;
                    entity = new NsNganhChungTu();
                    entity = _mapper.Map<NsNganhChungTu>(Model);
                    entity.ISoChungTuIndex = soChungTuIndex;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    _chungTuService.Add(entity);
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                VTS.QLNS.CTC.App.Model.LevelBuggetModel obj = _mapper.Map<VTS.QLNS.CTC.App.Model.LevelBuggetModel>(entity);
                SavedAction?.Invoke(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
