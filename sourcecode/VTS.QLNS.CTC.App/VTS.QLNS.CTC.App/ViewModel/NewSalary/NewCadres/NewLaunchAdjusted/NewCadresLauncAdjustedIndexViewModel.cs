using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewCadres;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.NewCadres.NewLaunchAdjusted
{
    public class NewCadresLauncAdjustedIndexViewModel : GridViewModelBase<TlCanBoRaQuanModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ITlCanBoPhuCapBridgeNq104Service _tlCanBoPhuCapBridgeNq104Service;
        private readonly ILog _logger;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlDmCanBoNq104Service _cadresService;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private readonly ITlDmPhuCapNq104Service _tlDmPhuCapService;
        private ICollectionView _dtCadresView;
        private readonly ISysAuditLogService _sysAuditLogService;


        //public override string FuncCode => NSFunctionCode.SALARY_CADRES_CAP_NHAT_LUONG_THUONG_THUE_TNCN_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Cập nhật thông tin ra quân của HSQ-CS";
        public override Type ContentType => typeof(View.NewSalary.NewCadres.NewLaunchAdjusted.NewLaunchAdjustedIndex);
        public override PackIconKind IconKind => PackIconKind.FormatListBulleted;
        public override string Title => "Cập nhật thông tin ra quân của HSQ-CS";
        public override string Description => string.Format("Danh sách đối tượng (Tổng số bản ghi: {0})", Items == null ? 0 : Items.Count());

        private List<ComboboxItem> _itemsMonths;
        public List<ComboboxItem> ItemsMonth
        {
            get => _itemsMonths;
            set => SetProperty(ref _itemsMonths, value);
        }

        private ComboboxItem _selectedMonth;
        public ComboboxItem SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                if (SetProperty(ref _selectedMonth, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
                }
            }
        }

        private ObservableCollection<TlDmDonViNq104Model> _itemsDonVi;
        public ObservableCollection<TlDmDonViNq104Model> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private TlDmDonViNq104Model _selectedDonVi;
        public TlDmDonViNq104Model SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                if (SetProperty(ref _selectedDonVi, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
                }
            }
        }

        private List<ComboboxItem> _itemsYear;
        public List<ComboboxItem> ItemsYear
        {
            get => _itemsYear;
            set => SetProperty(ref _itemsYear, value);
        }

        private ComboboxItem _selectedYear;
        public ComboboxItem SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (SetProperty(ref _selectedYear, value) && _dtCadresView != null)
                {
                    _dtCadresView.Refresh();
                }
            }
        }

        private string _searchCanBo;
        public string SearchCanBo
        {
            get => _searchCanBo;
            set => SetProperty(ref _searchCanBo, value);
        }

        public RelayCommand SearchCommand { get; }

        public RelayCommand SaveAllCommand { get; }

        public bool IsEnabled => SelectedItem != null;

        public NewCadresUpdateMultiAllowenceViewModel CadresUpdateMultiAllowenceViewModel { get; }

        public RelayCommand OpenUpdateMultiAllowenceCommand { get; }

        public NewCadresLauncAdjustedIndexViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmDonViNq104Service tlDmDonViService,
            INsDonViService nsDonViService,
            ITlDmCanBoNq104Service cadresService,
            ITlCanBoPhuCapBridgeNq104Service tlCanBoPhuCapBridgeNq104Service,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapService,
            ITlDmPhuCapNq104Service tlDmPhuCapService,
            NewCadresUpdateMultiAllowenceViewModel cadresUpdateMultiAllowenceViewModel,
            ISysAuditLogService sysAuditLogService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _tlDmDonViService = tlDmDonViService;
            _tlCanBoPhuCapBridgeNq104Service = tlCanBoPhuCapBridgeNq104Service;
            _nsDonViService = nsDonViService;
            _cadresService = cadresService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _sysAuditLogService = sysAuditLogService;
            CadresUpdateMultiAllowenceViewModel = cadresUpdateMultiAllowenceViewModel;
            SearchCommand = new RelayCommand(o => _dtCadresView.Refresh());
            SaveAllCommand = new RelayCommand(o => OnSaveAllCommand());
            OpenUpdateMultiAllowenceCommand = new RelayCommand(o => OnOpenUpdateMulti());
        }

        public override void Init()
        {
            base.Init();
            SearchCanBo = string.Empty;
            LoadDonVi();
            LoadMonths();
            LoadYear();
            LoadData();
        }

        private void LoadMonths()
        {
            _itemsMonths = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                var month = new ComboboxItem(i.ToString(), i.ToString());
                _itemsMonths.Add(month);
            }
            var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(ItemsMonth));
            SelectedMonth = _itemsMonths.FirstOrDefault(x => x.ValueItem == thang.ToString());
        }

        private void LoadDonVi()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            var lstDonVi = new List<TlDmDonViNq104Model>();

            TlDmDonViNq104Model tlDmDonViModel = new TlDmDonViNq104Model();
            tlDmDonViModel.TenDonVi = "-- Tất cả --";
            tlDmDonViModel.Id = Guid.Empty;

            lstDonVi.Add(tlDmDonViModel);
            lstDonVi.AddRange(_mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data).ToList());

            SelectedDonVi = tlDmDonViModel;

            _itemsDonVi = new ObservableCollection<TlDmDonViNq104Model>(lstDonVi);
        }

        private void LoadYear()
        {
            _itemsYear = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _itemsYear.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(ItemsYear));
            SelectedYear = _itemsYear.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        private void LoadData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _tlCanBoPhuCapBridgeNq104Service.DataPreprocess();
                    var _listDonVi = _nsDonViService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == 1).ToList();
                    if (_listDonVi.Any(n => _sessionService.Current.IdsDonViQuanLy.Contains(n.IIDMaDonVi) && n.Loai == "0") || _sessionService.Current.Principal.Equals("admin"))
                    {
                        var data = _cadresService.FindCanBoRaQuan(true);
                        e.Result = data;
                    }
                    else
                    {
                        var data = _cadresService.FindCanBoRaQuan(true).Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.MaDonVi));
                        e.Result = data;
                    }

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<TlCanBoRaQuanModel>>(e.Result);
                        if (Items.Count > 0)
                        {
                            foreach (var item in Items)
                            {
                                item.PropertyChanged += Detail_PropertyChanged;
                            }
                        }
                        _dtCadresView = CollectionViewSource.GetDefaultView(Items);
                        _dtCadresView.Filter = IncomeTaxFilter;

                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool IncomeTaxFilter(object obj)
        {
            bool result = true;
            var item = (TlCanBoRaQuanModel)obj;
            if (SelectedMonth != null)
            {
                result &= item.Thang == int.Parse(SelectedMonth.ValueItem);
            }
            if (SelectedYear != null)
            {
                result &= item.Nam == int.Parse(SelectedYear.ValueItem);
            }
            if (SelectedDonVi != null && !SelectedDonVi.Id.Equals(Guid.Empty))
            {
                result &= item.MaDonVi == SelectedDonVi.MaDonVi;
            }
            if (SearchCanBo != null)
            {
                result &= (item.TenCb.ToLower().Contains(SearchCanBo.ToLower()))
                    || item.MaCanBo.ToLower().Contains(SearchCanBo.ToLower());
            }
            return result;
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void OnOpenUpdateMulti()
        {
            try
            {
                //CadresModel cadres = new CadresModel();
                AllowenceNq104Model allowenceModel = new AllowenceNq104Model();
                allowenceModel.SelectedMonth = int.Parse(SelectedMonth.ValueItem);
                allowenceModel.SelectedYear = int.Parse(SelectedYear.ValueItem);
                CadresUpdateMultiAllowenceViewModel.Model = allowenceModel;
                CadresUpdateMultiAllowenceViewModel.IsHsq = true;
                CadresUpdateMultiAllowenceViewModel.MenuType = UpdateMultiMenuType.CHIKHAC;
                CadresUpdateMultiAllowenceViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                CadresUpdateMultiAllowenceViewModel.Init();
                CadresUpdateMultiAllowenceViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void Detail_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlCanBoRaQuanModel taxModel = (TlCanBoRaQuanModel)sender;
            taxModel.IsModified = true;
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(SelectedItem));
        }

        public override void OnSave()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<TlCanBoRaQuanModel> listEdit = Items.Where(x => x.IsModified).ToList();
                List<TlCanBoPhuCapNq104> lstCanBoPhuCaps = new List<TlCanBoPhuCapNq104>();
                List<TlCanBoPhuCapBridgeNq104> lstCanBoPhuCapBridges = new List<TlCanBoPhuCapBridgeNq104>();
                List<TlDmCanBoNq104> lstCanBo = new List<TlDmCanBoNq104>();

                if (listEdit != null && listEdit.Count > 0)
                {
                    var predicate104 = PredicateBuilder.True<TlCanBoPhuCapNq104>();
                    predicate104 = predicate104.And(x => listEdit.Select(s => s.MaCanBo).Contains(x.MaCbo));
                    var items104 = _tlCanBoPhuCapService.FindAll(predicate104);

                    var predicateBridge104 = PredicateBuilder.True<TlCanBoPhuCapBridgeNq104>();
                    predicateBridge104 = predicateBridge104.And(x => listEdit.Select(s => s.MaCanBo).Contains(x.MaCanBo));
                    var itemsBridge104 = _tlCanBoPhuCapBridgeNq104Service.FindAll(predicateBridge104);

                    var predicateCanBo = PredicateBuilder.True<TlDmCanBoNq104>();
                    predicateCanBo = predicateCanBo.And(x => listEdit.Select(s => s.MaCanBo).Contains(x.MaCanBo));
                    var itemsCanBo = _cadresService.FindAll(predicateCanBo);

                    foreach (var item in listEdit)
                    {
                        //Update Code
                        var dataCbPhuCapTmp = itemsBridge104.Where(x => item.MaCanBo.Equals(x.MaCanBo));
                        var dataCbPhuCap = items104.FirstOrDefault(x => item.MaCanBo.Equals(x.MaCbo));
                        var dataCanBo = itemsCanBo.FirstOrDefault(x => item.MaCanBo.Equals(x.MaCanBo));
                        if (dataCbPhuCap == null || dataCbPhuCapTmp.IsEmpty())
                        {
                            continue;
                        }

                        if (dataCanBo != null)
                            dataCanBo.NgayXn = item.NgayXn;

                        var itemTauXe = dataCbPhuCapTmp.FirstOrDefault(x => x.MaCanBo.Equals(item.MaCanBo) && x.MaPhuCap.Equals(PhuCap.PC_TTX));
                        if (itemTauXe != null)
                        {
                            itemTauXe.GiaTri = item.TienTauXe;
                            lstCanBoPhuCapBridges.Add(itemTauXe);
                        }

                        var itemAnDuong = dataCbPhuCapTmp.FirstOrDefault(x => x.MaCanBo.Equals(item.MaCanBo) && x.MaPhuCap.Equals(PhuCap.PC_TAD));
                        if (itemAnDuong != null)
                        {
                            itemAnDuong.GiaTri = item.TienAnDuong;
                            lstCanBoPhuCapBridges.Add(itemAnDuong);
                        }
                        var itemChiaTay = dataCbPhuCapTmp.FirstOrDefault(x => x.MaCanBo.Equals(item.MaCanBo) && x.MaPhuCap.Equals(PhuCap.PC_CTLH));
                        if (itemChiaTay != null)
                        {
                            itemChiaTay.GiaTri = item.TienChiaTay;
                            lstCanBoPhuCapBridges.Add(itemChiaTay);
                        }
                        var itemGiamTruKhac = dataCbPhuCapTmp.FirstOrDefault(x => x.MaCanBo.Equals(item.MaCanBo) && x.MaPhuCap.Equals(PhuCap.GTKHAC_TT));
                        if (itemGiamTruKhac != null)
                        {
                            itemGiamTruKhac.GiaTri = item.GiamTruKhac;
                            lstCanBoPhuCapBridges.Add(itemGiamTruKhac);
                        }

                        var phuCapRaQuan = dataCbPhuCapTmp.FirstOrDefault(pc => pc.MaCanBo.Equals(item.MaCanBo) && pc.MaPhuCap.Equals(PhuCap.THANG_TCXN));
                        if (phuCapRaQuan != null && dataCanBo.MaCb.StartsWith("0"))
                        {
                            phuCapRaQuan.GiaTri = TinhThangHuongTcxn(dataCanBo.NgayNn, item.NgayXn);
                            lstCanBoPhuCapBridges.Add(phuCapRaQuan);
                        }

                        var phuCapViecLam = dataCbPhuCapTmp.FirstOrDefault(pc => pc.MaCanBo.Equals(item.MaCanBo) && pc.MaPhuCap.Equals(PhuCap.THANG_TCVIECLAM));

                        if (phuCapViecLam != null)
                        {
                            if (dataCanBo.NgayXn == null)
                            {
                                phuCapViecLam.GiaTri = 0;
                            }
                            else
                            {
                                var phuCapThangViecLam = _tlDmPhuCapService.FindByMaPhuCap(PhuCap.THANG_TCVIECLAM);
                                phuCapViecLam.GiaTri = phuCapThangViecLam.GiaTri;
                            }

                            lstCanBoPhuCapBridges.Add(phuCapViecLam);
                        }
                        dataCbPhuCap.Data = CompressExtension.CompressToBase64(JsonConvert.SerializeObject(new AllowenceCanBoNq104Criteria()
                        {
                            X = _mapper.Map<IEnumerable<AllowencePhuCapNq104Criteria>>(dataCbPhuCapTmp)
                        }));
                        lstCanBo.Add(dataCanBo);
                        lstCanBoPhuCaps.Add(dataCbPhuCap);

                    }
                    _cadresService.UpdateMulti(lstCanBo, lstCanBoPhuCaps, lstCanBoPhuCapBridges);
                    _sysAuditLogService.WriteLog(Resources.ApplicationName, Name, (int)TypeExecute.Update, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);

                }
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    MessageBoxHelper.Info("Lưu dữ liệu thành công.");
                    LoadData();
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
               
        }

        public void OnSaveAllCommand()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<TlCanBoRaQuanModel> listEdit = Items.ToList();
                List<TlCanBoPhuCapNq104> lstCanBoPhuCaps = new List<TlCanBoPhuCapNq104>();
                List<TlDmCanBoNq104> lstCanBo = new List<TlDmCanBoNq104>();

                if (listEdit != null && listEdit.Count > 0)
                {
                    foreach (var item in listEdit)
                    {
                        var dataPhuCap = _tlCanBoPhuCapService.FindByMaCanBo(item.MaCanBo);
                        var canBo = _cadresService.FindByMaCanBo(item.MaCanBo);
                        canBo.NgayXn = item.NgayXn;

                        var thuong = dataPhuCap.FirstOrDefault(x => x.MaCbo == item.MaCanBo && x.MaPhuCap == PhuCap.PC_TTX);
                        thuong.GiaTri = item.TienTauXe;

                        var thueDaNop = dataPhuCap.FirstOrDefault(x => x.MaCbo == item.MaCanBo && x.MaPhuCap == PhuCap.PC_TAD);
                        thueDaNop.GiaTri = item.TienAnDuong;

                        var giamThue = dataPhuCap.FirstOrDefault(x => x.MaCbo == item.MaCanBo && x.MaPhuCap == PhuCap.PC_CTLH);
                        giamThue.GiaTri = item.TienChiaTay;

                        var thuNhapKhac = dataPhuCap.FirstOrDefault(x => x.MaCbo == item.MaCanBo && x.MaPhuCap == PhuCap.GTKHAC_TT);
                        thuNhapKhac.GiaTri = item.GiamTruKhac;

                        List<TlCanBoPhuCapNq104> canboPhuCap = new List<TlCanBoPhuCapNq104> { thuong, thueDaNop, giamThue, thuNhapKhac };
                        var phuCapRaQuan = dataPhuCap.FirstOrDefault(pc => pc.MaCbo == item.MaCanBo && pc.MaPhuCap == PhuCap.THANG_TCXN);

                        if (phuCapRaQuan != null && canBo.MaCb.StartsWith("0"))
                        {
                            phuCapRaQuan.GiaTri = TinhThangHuongTcxn(canBo.NgayNn, item.NgayXn);
                            canboPhuCap.Add(phuCapRaQuan);
                        }

                        var phuCapViecLam = dataPhuCap.FirstOrDefault(pc => pc.MaCbo == item.MaCanBo && pc.MaPhuCap == PhuCap.THANG_TCVIECLAM);

                        if (phuCapViecLam != null && canBo.NgayXn == null)
                        {
                            phuCapViecLam.GiaTri = 0;
                            canboPhuCap.Add(phuCapViecLam);
                        }

                        //var troCapViecLam = dataPhuCap.FirstOrDefault(pc => pc.MaCbo == item.MaCanBo && pc.MaPhuCap == PhuCap.TCVIECLAM_TT);

                        //if (troCapViecLam != null && canBo.NgayXn != null)
                        //{
                        //    var phuCap = _tlDmPhuCapService.FindByMaPhuCap(PhuCap.TCVIECLAM_TT);
                        //    troCapViecLam.GiaTri = phuCap.GiaTri;
                        //    canboPhuCap.Add(troCapViecLam);
                        //}

                        lstCanBoPhuCaps.AddRange(canboPhuCap);
                        lstCanBo.Add(canBo);
                        //_tlCanBoPhuCapService.BulkUpdate(canboPhuCap);
                    }
                    _cadresService.UpdateMulti(lstCanBo, lstCanBoPhuCaps);
                    _sysAuditLogService.WriteLog(Resources.ApplicationName, Name, (int)TypeExecute.Update, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);

                }
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    MessageBoxHelper.Info("Lưu dữ liệu thành công.");
                    LoadData();
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }
        private int TinhThangHuongTcxn(DateTime? ngayNn, DateTime? ngayXn)
        {
            if(ngayNn == null  || ngayXn == null || !ngayNn.HasValue || !ngayXn.HasValue)
            {
                return 0;
            }

            var ngayNhapNgu = ngayNn.Value;
            var ngayXuatNgu = ngayXn.Value;
            var monthDiff = (ngayXuatNgu.Year - ngayNhapNgu.Year) * 12 + ngayXuatNgu.Month - ngayNhapNgu.Month + 1;
            var phanNguyen = monthDiff / 12;
            var phanDu = monthDiff % 12;

            int thangDu = 0;
            if (1 <= phanDu && phanDu <= 6)
            {
                thangDu = 1;
            }
            else if (7 <= phanDu && phanDu <= 12)
            {
                thangDu = 2;
            }

            return (phanNguyen * 2 + thangDu);
        }
        
    }
}
