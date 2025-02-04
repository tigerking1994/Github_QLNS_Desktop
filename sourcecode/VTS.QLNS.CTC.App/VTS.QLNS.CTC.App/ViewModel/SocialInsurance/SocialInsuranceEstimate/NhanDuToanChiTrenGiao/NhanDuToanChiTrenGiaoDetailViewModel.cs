using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao
{
    public class NhanDuToanChiTrenGiaoDetailViewModel : DetailViewModelBase<BhDtctgBHXHModel, BhDtctgBHXHChiTietModel>
    {
        private readonly ISessionService _sessionService;
        private readonly INdtctgBHXHChiTietService _ndtctgBHXHChiTietService;
        private readonly INdtctgBHXHChiTietXNMService _ndtctgBHXHChiTietXNMService;
        private readonly INdtctgBHXHService _ndtctgBHXHService;
        private readonly IBhKhcCheDoBhXhService _bhKhcCheDoBhXhService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly INsDonViService _iNsDonViService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public bool IsExit;
        public bool IsSaveData => Items.Any(x => x.IsModified);
        public bool IsUpdateData;
        public bool IsPropertyChange;
        public bool IsExist;
        private string _sNoiDungSearch;

        private Dictionary<Guid, List<BhDtctgBHXHChiTietModel>> _parentChildLookup;
        private Dictionary<Guid, BhDtctgBHXHChiTietModel> _dictionaryLookup;

        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set
            {
                if (SetProperty(ref _sNoiDungSearch, value))
                {
                    SearchTextFilter();
                    _ndtctgBHXHChiTietModelView.Refresh();
                    //_budgetCatalogItemsView.Refresh();
                }
            }
        }


        private ObservableCollection<BhDtctgBHXHChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhDtctgBHXHChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhDtctgBHXHChiTietModel _selectedPopupItem;
        public BhDtctgBHXHChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.SNoiDung;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }

        private ObservableCollection<BhDtctgBHXHChiTietModel> _dataSearch;
        public ObservableCollection<BhDtctgBHXHChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ICollectionView _ndtctgBHXHChiTietModelView { get; set; }
        public override Type ContentType => typeof(NhanDuToanChiTrenGiaoDetail);
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public bool IsAnotherUserCreate { get; set; }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        public bool IsGetDataAgregate;

        public bool IsProcess { get; set; }
        public bool IsGetDataAgregateAdjust { get; set; }

        private bool _isShowGetDataAgregateAdjust;
        public bool IsShowGetDataAgregateAdjust
        {
            get => _isShowGetDataAgregateAdjust;
            set => SetProperty(ref _isShowGetDataAgregateAdjust, value);
        }
        private bool _isShowGetDataAgregate;
        public bool IsShowGetDataAgregate
        {
            get => _isShowGetDataAgregate;
            set => SetProperty(ref _isShowGetDataAgregate, value);
        }
        List<BhDtctgBHXHChiTietQuery> lstDtBhxhQuanNhan;
        public RelayCommand RefreshCommand { get; }
        public RelayCommand OnOpenNhanDTCTKQPLCommand { get; }
        public RelayCommand GetDataAgregateCommand { get; }
        public RelayCommand GetDataAgregateAdjustCommand { get; }
        public bool IsInit { get; set; }

        public bool IsShowKeHoach => Model.ILoaiDotNhanPhanBo == 1;

        public PrintReportNhanDuToanChiTrenGiaoViewModel PrintReportNhanDuToanChiTrenGiaoViewModel { get; set; }
        public NhanDuToanChiTietChiKPQLViewModel NhanDuToanChiTietChiKPQLViewModel { get; set; }
        public NhanDuToanChiTrenGiaoDetailViewModel(
            ISessionService sessionService,
            INdtctgBHXHChiTietService ndtctgBHXHChiTietService,
            INdtctgBHXHChiTietXNMService ndtctgBHXHChiTietXNMService,
            INdtctgBHXHService ndtctgBHXHService,
            IMapper mapper,
            ILog logger,
            INsDonViService iNsDonViService,
            PrintReportNhanDuToanChiTrenGiaoViewModel printReportNhanDuToanChiTrenGiaoViewModel,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            ISysAuditLogService log,
            NhanDuToanChiTietChiKPQLViewModel nhanDuToanChiTietChiKPQLViewModel)
        {
            _iNsDonViService = iNsDonViService;
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _ndtctgBHXHChiTietService = ndtctgBHXHChiTietService;
            _ndtctgBHXHChiTietXNMService = ndtctgBHXHChiTietXNMService;
            _ndtctgBHXHService = ndtctgBHXHService;
            PrintReportNhanDuToanChiTrenGiaoViewModel = printReportNhanDuToanChiTrenGiaoViewModel;
            NhanDuToanChiTietChiKPQLViewModel = nhanDuToanChiTietChiKPQLViewModel;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;

            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            RefreshCommand = new RelayCommand(obj => OnRefresh());
            PrintCommand = new RelayCommand(obj => OnOpenReport(obj));

            GetDataAgregateCommand = new RelayCommand(obj => GetDataAgregate());
            GetDataAgregateAdjustCommand = new RelayCommand(obj => GetDataAgregateAdjust());
            OnOpenNhanDTCTKQPLCommand = new RelayCommand(obj => OnOpenNhanDTCTCTKQPL(obj));
        }

        private void OnOpenNhanDTCTCTKQPL(object obj)
        {

            NhanDuToanChiTietChiKPQLViewModel.BhDtctgBHXHModel = Model;
            NhanDuToanChiTietChiKPQLViewModel.IIDChungTuChiTiet = Items.Where(x => x.SXauNoiMa == LNSValue.LNS_9010003).FirstOrDefault().ID;
            NhanDuToanChiTietChiKPQLViewModel.Init();
            var view = new NhanDuToanChiTietChiKPQL() { DataContext = NhanDuToanChiTietChiKPQLViewModel };
            DialogHost.Show(view, "DialogDetail", null, null);
        }

        public override void Init()
        {
            base.Init();
            IsProcess = true;
            _sessionInfo = _sessionService.Current;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            IsGetDataAgregate = false;
            IsGetDataAgregateAdjust = false;
            IsPropertyChange = false;

            IsShowGetDataAgregateAdjust = false;
            if (Model.ILoaiDotNhanPhanBo == 2)
            {
                IsShowGetDataAgregateAdjust = true;
            }

            LoadData();

            IsShowGetDataAgregate = !IsShowGetDataAgregateAdjust;
            OnPropertyChanged(nameof(IsShowGetDataAgregate));
            OnPropertyChanged(nameof(IsShowGetDataAgregateAdjust));
            IsProcess = false;
        }

        private void GetDataAgregate()
        {
            //IsGetDataAgregate = true;
            var yearOfWork = _sessionInfo.YearOfWork;
            var lstSLNS = Model.SLNS.Split(",");
            string sLNS = GetValueSLNS();
            List<BhDtctgBHXHChiTietQuery> temp = new List<BhDtctgBHXHChiTietQuery>();
            temp = _ndtctgBHXHChiTietService.GetListDataAgregateChiTiet(Model.Id, sLNS, yearOfWork, Model.IID_MaDonVi, Model.IIdLoaiDanhMucChi).ToList();
            CalculateAgregatePlanData(temp);
            temp = temp.Where(x => x.BHangChaDuToan.HasValue && x.FTienTuChi > 0).ToList();
            if (temp.Any())
            {
                var itemFilter = Items.Where(x => !x.IsHangCha && temp.Select(s => s.SXauNoiMa).ToList().Contains(x.SXauNoiMa));
                Parallel.ForEach(itemFilter, item =>
                {
                    item.FTienTuChi = temp.Where(x => x.SXauNoiMa == item.SXauNoiMa).Select(x => x.FTienTuChi.GetValueOrDefault()).FirstOrDefault();
                });
            }
        }

        private void GetDataAgregateAdjust()
        {
            IsGetDataAgregateAdjust = true;
            this.LoadData();
        }

        private List<BhDtctgBHXHChiTietQuery> GetDataAgregate(Guid IdChungTu, string sLNS, int yearOfWork, string sMaDonVi)
        {
            List<BhDtctgBHXHChiTietQuery> temp = new List<BhDtctgBHXHChiTietQuery>();
            temp = _ndtctgBHXHChiTietService.GetListDataAgregateChiTiet(IdChungTu, sLNS, yearOfWork, sMaDonVi, Model.IIdLoaiDanhMucChi).ToList();
            return temp;
        }

        private List<BhDtctgBHXHChiTietQuery> GetDataAgregateAdjust(Guid idChungTu, int namLamViec, string sMaDonVi, DateTime? dNgayChungTu, string sLNS)
        {
            var temp = new List<BhDtctgBHXHChiTietQuery>();
            temp = _ndtctgBHXHChiTietService.GetListDataAgregateAdjustChiTiet(idChungTu, namLamViec, sMaDonVi, dNgayChungTu, sLNS).ToList();
            return temp;
        }

        private string GetValueSLNS()
        {
            string[] lstSLNS = Model.SLNS.Split(",");
            string sLNS = Model.SLNS;
            if (lstSLNS.Contains(LNSValue.LNS_9010001) || lstSLNS.Contains(LNSValue.LNS_9010002))
            {
                sLNS += "," + LNSValue.LNS_9_901;
            }

            if (lstSLNS.Contains(LNSValue.LNS_9010003) || lstSLNS.Contains(LNSValue.LNS_9010004)
                || lstSLNS.Contains(LNSValue.LNS_9010006) || lstSLNS.Contains(LNSValue.LNS_9010008)
                || lstSLNS.Contains(LNSValue.LNS_9010009) || lstSLNS.Contains(LNSValue.LNS_9010010))
            {
                sLNS += "," + LNSValue.LNS_9;
            }
            return sLNS;
        }

        public override void LoadData(params object[] args)
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            IsExit = false;
            string[] lstSLNS = Model.SLNS.Split(",");
            string sLNS = GetValueSLNS();

            List<BhDtctgBHXHChiTietQuery> temp = new List<BhDtctgBHXHChiTietQuery>();
            if (!IsGetDataAgregateAdjust)
            {
                temp = _ndtctgBHXHChiTietService.GetListNhanDuToanChiTrenGiaoChiTiet(Model.Id, sLNS, yearOfWork, Model.IID_MaDonVi, Model.ILoaiDotNhanPhanBo).ToList();
                Items = _mapper.Map<ObservableCollection<BhDtctgBHXHChiTietModel>>(temp);
            }
            else
            {
                //if (IsGetDataAgregate)
                //{
                //    temp = GetDataAgregate(Model.Id, sLNS, yearOfWork, Model.IID_MaDonVi);
                //    CalculateAgregatePlanData(temp);
                //    temp = temp.Where(x => x.BHangChaDuToan != null).ToList();
                //    temp.ForEach(x =>
                //            {
                //                x.BHangCha = x.BHangChaDuToan.Value;
                //                x.IsHangCha = x.BHangChaDuToan.Value;
                //            }
                //        );
                //    Items = _mapper.Map<ObservableCollection<BhDtctgBHXHChiTietModel>>(temp);
                //}

                if (IsGetDataAgregateAdjust)
                {
                    temp = GetDataAgregateAdjust(Model.Id, Model.INamLamViec, Model.IID_MaDonVi, Model.DNgayChungTu, sLNS);
                    Items = _mapper.Map<ObservableCollection<BhDtctgBHXHChiTietModel>>(temp);
                }
            }

            var lstchungTuChiTietExist = _ndtctgBHXHChiTietService.FindByCondition(Model.Id);
            var chungTuChiTietExist = lstchungTuChiTietExist.Where(x => x.SMaLoaiChi == MaLoaiChiBHXH.SMAKCBQYDV).FirstOrDefault();
            //var chungTuChiTietEdit = lstchungTuChiTietExist.Where(x => x.SMaLoaiChi == MaLoaiChiBHXH.SMAKCBQYDV && x.DNgaySua.HasValue).FirstOrDefault();
            if (lstSLNS.Contains(LNSValue.LNS_9010004_9010005) && Model.ILoaiDotNhanPhanBo == 2)
            {

                lstDtBhxhQuanNhan = _ndtctgBHXHChiTietService.FindGiaTriDieuChinhThuBHXHChangeRequest(Model.IID_MaDonVi, Model.INamLamViec);
                var listChungTuChiTietMap = _mapper.Map<List<BhDtctgBHXHChiTietModel>>(Items);
                listChungTuChiTietMap.Where(x => x.SMaLoaiChi == MaLoaiChiBHXH.SMAKCBQYDV)
                    .ForAll(x =>
                    {
                        x.FTienTuChi = lstDtBhxhQuanNhan.FirstOrDefault().FTienTuChi;
                    });

                Items = new ObservableCollection<BhDtctgBHXHChiTietModel>(listChungTuChiTietMap);
            }

            _ = Items.Where(x => x.SXauNoiMa == "9010004").Select(x =>
            {
                if (chungTuChiTietExist != null && !IsGetDataAgregate && !IsGetDataAgregateAdjust)
                {
                    x.FTienTuChi = chungTuChiTietExist.FTienTuChi;
                }

                if (x.FTienTuChi != null && x.FTienTuChi.GetValueOrDefault(0) != 0 && chungTuChiTietExist?.FTienTuChi != x.FTienTuChi)
                {
                    x.IsModified = true;
                }
                return x;
            }).ToList();

            BuildParentChildLookup();
            _ = Items.Where(x => (x.Level > 4 && 
                (x.SXauNoiMa.StartsWith(LNSValue.LNS_9010001) 
                || x.SXauNoiMa.StartsWith(LNSValue.LNS_9010002))
                ) || (x.Level > 2 && !x.SXauNoiMa.StartsWith(LNSValue.LNS_9010001) && !x.SXauNoiMa.StartsWith(LNSValue.LNS_9010002))).Select(x =>
            {
                x.IsExpand = false;
                return x;
            }).ToList();
            _ = Items.Where(x => x.FTienTuChi.GetValueOrDefault() != 0).Select(x =>
            {
                FindParent(x).ForAll(x => x.IsExpand = true);
                FindChildren(x).ForAll(x => x.IsExpand = false);
                if (x.IID_MLNS_Cha.HasValue && _parentChildLookup.TryGetValue(x.IID_MLNS_Cha.Value, out var children))
                {
                    children.ForAll(x => x.IsExpand = true);
                } else
                {
                    x.IsExpand = true;
                }
                return x;
            }).ToList();
            ProcessData();
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhDtctgBHXHChiTietModel>>(Items);
            _ndtctgBHXHChiTietModelView = CollectionViewSource.GetDefaultView(Items);
            _ndtctgBHXHChiTietModelView.Filter = ItemsViewFilter;
            foreach (var bhDtctgBHXHChiTietModel in Items)
            {
                bhDtctgBHXHChiTietModel.PropertyChanged += (sender, args) =>
                {
                    if (IsProcess) return;
                    var item = sender as BhDtctgBHXHChiTietModel;
                    OnPropertyChanged(nameof(IsSaveData));
                    if (args.PropertyName == nameof(BhDtctgBHXHChiTietModel.IsCollapse))
                    {
                        ExpandChild(item);
                    }
                    else if (args.PropertyName.StartsWith("F", StringComparison.OrdinalIgnoreCase))
                    {
                        IsProcess = true;
                        CalculateValue();
                        IsProcess = false;
                        ProcessData();
                        item.IsModified = !item.IsHangCha || (args.PropertyName == "FTienTuChiTrenGiao" && item.IsDisabled);
                    }
                    OnPropertyChanged(nameof(IsSaveData));
                };
            }
            CalculateData();
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        public override void OnSave()
        {
            if (!IsSaveData)
            { return; }

            if (IsGetDataAgregateAdjust)
            {
                var lstChungTuChiTietOld = _ndtctgBHXHChiTietService.FindByCondition(Model.Id).ToList();
                _ndtctgBHXHChiTietService.RemoveRange(lstChungTuChiTietOld);
                //IsGetDataAgregate = false;
                IsGetDataAgregateAdjust = false;
            }


            var listCanCu = Items.Where(x => x.IsDisabled && x.FTienTuChiTrenGiao.GetValueOrDefault() != 0);
            var listCanCuAdd = listCanCu.Select(x => new BhDtctgBHXHChiTietXNM
            {
                IID_DTC_DuToanChiTrenGiao = Model.Id,
                SXauNoiMa = x.SXauNoiMa,
                FTuChi = x.FTienTuChiTrenGiao
            });
            _ndtctgBHXHChiTietXNMService.RemoveRange(x => x.IID_DTC_DuToanChiTrenGiao == Model.Id);
            _ndtctgBHXHChiTietXNMService.AddRange(listCanCuAdd);
            var lstDetaillAdd = Items.Where(x => x.IsModified && !x.IsHangCha && x.Id == Guid.Empty && x.IsExpand).ToList();
            var lstDetaillUpdate = Items.Where(x => x.IsModified && !x.IsHangCha && x.Id != Guid.Empty && x.IsExpand && (x.FTienTuChi.GetValueOrDefault() != 0 || x.FTienTuChiTrenGiao.GetValueOrDefault() != 0)).ToList();
            var lstDetaillDelete = Items.Where(x =>
                (!x.IsHangCha && x.Id != Guid.Empty && x.FTienTuChi.GetValueOrDefault() == 0 && x.FTienTuChiTrenGiao.GetValueOrDefault() == 0)
                ||  (!x.IsHangCha && x.Id != Guid.Empty && !x.IsExpand) 
                || (x.IsHangCha && x.Id != Guid.Empty)).ToList();
            var addItemList = new List<BhDtctgBHXHChiTiet>();
            _mapper.Map(lstDetaillDelete, addItemList);
            _ndtctgBHXHChiTietService.RemoveRange(addItemList);
            if (lstDetaillAdd.Count() > 0)
            {
                _mapper.Map(lstDetaillAdd, addItemList);
                _ = addItemList.Select(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IID_DTC_DuToanChiTrenGiao = Model.Id;
                    x.DNgayTao = DateTime.Now;
                    x.SNguoiTao = _sessionInfo.Principal;
                    x.SNoiDung = x.SNoiDung.Trim();
                    return x;
                }).ToList();
                _ndtctgBHXHChiTietService.AddRange(addItemList);
                _ = Items.Where(x => !x.IsHangCha && x.IsModified).Select(x => { x.IsModified = false; x.IsAdded = false; return x; }).ToList();
            }
            if (lstDetaillUpdate.Count() > 0)
            {
                _mapper.Map(lstDetaillUpdate, addItemList);
                _ = addItemList.Select(x =>
                {
                    x.DNgaySua = DateTime.Now;
                    x.SNguoiSua = _sessionInfo.Principal;
                    x.SNoiDung = x.SNoiDung.Trim();
                    return x;
                }).ToList();
                foreach (var item in addItemList)
                {
                    _ndtctgBHXHChiTietService.Update(item);
                }
                _ = Items.Where(x => !x.IsHangCha && x.IsModified).Select(x => { x.IsModified = false; x.IsUpdate = false; return x; }).ToList();
            }

            //Update dự toán chi trên giao
            var chungTuParent = _ndtctgBHXHService.FindById(Model.Id);
            var lstChungTuChitiet = _ndtctgBHXHChiTietService.FindByCondition(Model.Id);
            if (chungTuParent != null && lstChungTuChitiet != null)
            {
                chungTuParent.FTongTien = lstChungTuChitiet.Sum(x => x.FTongTien);
                chungTuParent.FTongTienTuChi = lstChungTuChitiet.Sum(x => x.FTienTuChi);
                chungTuParent.FTongTienHienVat = lstChungTuChitiet.Sum(x => x.FTienHienVat);

                _ndtctgBHXHService.Update(chungTuParent);
            }

            MessageBoxHelper.Info(Resources.MsgSaveDone);
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());

            OnPropertyChanged(nameof(IsSaveData));
            this.LoadData();
        }

        protected override void OnRefresh()
        {
            IsInit = true;
            _sessionInfo = _sessionService.Current;
            LoadData();
            IsInit = false;
        }

        private void ExpandChild(BhDtctgBHXHChiTietModel element)
        {
            bool isCollapse = element.IsCollapse;
            if (!isCollapse)
            {
                var children = FindChildren(element);
                foreach (var child in children)
                {
                    child.IsExpand = false;
                }
            }
            else
            {
                if (_parentChildLookup.TryGetValue(element.IID_MLNS.Value, out var children))
                {
                    if (element.FTienTuChi.HasValue && element.FTienTuChi.Value != 0)
                    {
                        //if (FindParent(element).All(x => x.FTienTuChiTrenGiao.GetValueOrDefault() == 0)) {
                        //    element.FTienTuChiTrenGiao = element.FTienTuChi;
                        //}
                        if (!(element.IID_MLNS_Cha.HasValue
                            && _dictionaryLookup.TryGetValue(element.IID_MLNS_Cha.Value, out var parent)
                            && parent.FTienTuChiTrenGiao.GetValueOrDefault() != 0))
                        {
                            if (element.FTienTuChiTrenGiao.GetValueOrDefault() == 0)
                            {
                                element.FTienTuChiTrenGiao = element.FTienTuChi;
                            }
                        }
                    }
                    foreach (var child in children)
                    {
                        child.IsExpand = true;
                    }
                }
            }

            ProcessData();
        }

        public void BuildParentChildLookup()
        {
            _parentChildLookup = Items
                .Where(x => x.IID_MLNS_Cha.HasValue) // Ensure parent exists
                .GroupBy(x => x.IID_MLNS_Cha.Value) // Group by parent ID
                .ToDictionary(g => g.Key, g => g.ToList());
            _dictionaryLookup = Items.ToDictionary(x => x.IID_MLNS.Value, x => x);
        }

        public IEnumerable<BhDtctgBHXHChiTietModel> FindChildren(BhDtctgBHXHChiTietModel item)
        {
            if (_parentChildLookup.TryGetValue(item.IID_MLNS.Value, out var children))
            {
                return children.Concat(children.SelectMany(FindChildren));
            }

            return Enumerable.Empty<BhDtctgBHXHChiTietModel>();
        }

        public IEnumerable<BhDtctgBHXHChiTietModel> FindParent(BhDtctgBHXHChiTietModel item)
        {
            if (!item.IID_MLNS_Cha.HasValue || item.IID_MLNS_Cha.Value.Equals(Guid.Empty))
            {
                return Enumerable.Empty<BhDtctgBHXHChiTietModel>(); // Base case: no parent
            }

            if (_dictionaryLookup.TryGetValue(item.IID_MLNS_Cha.Value, out var parent))
            {
                // Recursive case: include the parent and continue traversing up
                return FindParent(parent).Concat(new[] { parent });
            }

            return Enumerable.Empty<BhDtctgBHXHChiTietModel>(); // If parent not found
        }


        private void ProcessData()
        {
            IsProcess = true;

            foreach (var item in Items)
            {
                var children = FindChildren(item).ToList(); // Cache the children list
                item.IsDisabled = children.IsEmpty()
                    || children.All(x => x.FTienTuChiTrenGiao.GetValueOrDefault() == 0);
            }
            foreach (var item in Items)
            {
                var children = FindChildren(item).ToList(); // Cache the children list

                item.IsChildSummary =
                    //(item.FTienTuChi.GetValueOrDefault() != 0 && item.FTienTuChiTrenGiao.GetValueOrDefault() != 0) ||
                    !children.Any() || // No children
                    children.Any(x => x.IsExpand &&
                                      (x.FTienTuChiTrenGiao.GetValueOrDefault() != 0 || x.FTienTuChi.GetValueOrDefault() != 0));

                bool hasExpandedChildren = children.Any(x => x.IsExpand);

                item.IsCollapse = hasExpandedChildren;
                item.IsHangCha = hasExpandedChildren;
                if (hasExpandedChildren) item.IsModified = false;
            }

            CalculateValue();
            IsProcess = false;
        }

        private void CalculateValue()
        {
            var leaf = Items.Where(x => x.FTienTuChiTrenGiao.GetValueOrDefault() != 0 && x.IsDisabled && x.IsExpand).ToList();

            foreach (var item in Items.Where(x => !x.IsDisabled))
            {
                item.FTienTuChiTrenGiao = 0;
            }

            foreach (var item in Items.Where(x => x.IsHangCha))
            {
                item.FTienTuChi = 0;
            }

            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsExpand).ToList();
            foreach (var item in temp)
            {
                CalculateParentValue2(item.IID_MLNS_Cha, item);
            }
            foreach (var item in leaf)
            {
                CalculateParentValue1(item.IID_MLNS_Cha, item);
            }
            UpdateTotal(temp);
        }

        private void CalculateData()
        {
            IsPropertyChange = false;
            IsProcess = true;
            Items.Where(x => x.IsHangCha && !x.IsRemainRow)
            .ForAll(x =>
            {
                x.FTienTuChi = 0;
            });

            var dictByMlns = Items.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (var item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }
            UpdateTotal(temp);
            IsPropertyChange = true;
            IsProcess = false;
        }

        private void CalculateAgregatePlanData(List<BhDtctgBHXHChiTietQuery> lstQuery)
        {

            lstQuery.Where(x => x.IsHangCha && !x.IsRemainRow && x.SXauNoiMa != "9010004")
                       .ForAll(x =>
                       {
                           x.FTienTuChi = 0;
                       });

            var dictByMlns = lstQuery.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = lstQuery.Where(x => !x.IsHangCha && x.SLNS != "9010004").ToList();
            foreach (var item in temp)
            {

                CalculateAgregatePlanParent(item.IID_MLNS_Cha, item, dictByMlns);
            }
        }

        private void UpdateTotal(List<BhDtctgBHXHChiTietModel> listChildren)
        {
            Model.FTongTienTuChi = Items.Where(t => !t.IsHangCha && t.IsExpand).Sum(x => x.FTienTuChi.GetValueOrDefault());
            Model.FTongTienKeHoach = Items.Where(t => t.IsDisabled && t.IsExpand).Sum(x => x.FTienKeHoach.GetValueOrDefault());
            Model.FTongTienBoSung = Items.Where(t => t.IsDisabled && t.IsExpand).Sum(x => x.FTienBoSung.GetValueOrDefault());
            Model.FTongTienTuChiTrenGiao = Items.Where(t => t.IsDisabled && t.IsExpand).Sum(x => x.FTienTuChiTrenGiao.GetValueOrDefault());
        }

        private void CalculateAgregatePlanParent(Guid idParent, BhDtctgBHXHChiTietQuery item, Dictionary<Guid, BhDtctgBHXHChiTietQuery> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienTuChi = model.FTienTuChi.GetValueOrDefault(0) + item.FTienTuChi.GetValueOrDefault(0);

            CalculateAgregatePlanParent(model.IID_MLNS_Cha, item, dictByMlns);
        }

        private void CalculateParentValue2(Guid? idParent, BhDtctgBHXHChiTietModel item)
        {
            if (!idParent.HasValue || !_dictionaryLookup.ContainsKey(idParent.Value))
            {
                return;
            }

            var model = _dictionaryLookup[idParent.Value];
            model.FTienTuChi = model.FTienTuChi.GetValueOrDefault(0) + item.FTienTuChi.GetValueOrDefault(0);

            CalculateParentValue2(model.IID_MLNS_Cha, item);
        }

        private void CalculateParentValue1(Guid? idParent, BhDtctgBHXHChiTietModel item)
        {
            if (!idParent.HasValue || !_dictionaryLookup.ContainsKey(idParent.Value))
            {
                return;
            }

            var model = _dictionaryLookup[idParent.Value];
            model.FTienTuChiTrenGiao = model.FTienTuChiTrenGiao.GetValueOrDefault(0) + item.FTienTuChiTrenGiao.GetValueOrDefault(0);

            CalculateParentValue1(model.IID_MLNS_Cha, item);
        }

        private void CalculateParent(Guid? idParent, BhDtctgBHXHChiTietModel item, Dictionary<Guid?, BhDtctgBHXHChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTienTuChi = model.FTienTuChi.GetValueOrDefault(0) + item.FTienTuChi.GetValueOrDefault(0);

            CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
        }

        private void OnOpenReport(object param)
        {

            var ndtcheckPrintType = (NdtcheckPrintType)((int)param);
            object content;
            switch (ndtcheckPrintType)
            {
                case NdtcheckPrintType.NDTCCTNS:
                    PrintReportNhanDuToanChiTrenGiaoViewModel.NdtcheckPrintType = ndtcheckPrintType;
                    PrintReportNhanDuToanChiTrenGiaoViewModel.Name = "In nhận dự toán chi trên giao";
                    PrintReportNhanDuToanChiTrenGiaoViewModel.Description = "In nhận dự toán chi trên giao";
                    PrintReportNhanDuToanChiTrenGiaoViewModel.Init();

                    content = new PrintReportNhanDuToanChiTrenGiao
                    {
                        DataContext = PrintReportNhanDuToanChiTrenGiaoViewModel
                    };

                    break;

                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, "DialogDetail", null, null);
            }
        }

        #region Search
        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhDtctgBHXHChiTietModel)obj;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.IID_MLNS.Equals(item.IID_MLNS));
            }

            item.IsFilter = result;
            return result;
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                var lstResult = new List<string>();
                var lstParents = new List<string>();
                var results = new List<BhDtctgBHXHChiTietModel>();

                var lstSXaNoiMaChildSearch = DataPopupSearchItems.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                var lstSXaNoiMaParentSearch = DataPopupSearchItems.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                if (!lstSXaNoiMaChildSearch.IsEmpty())
                {
                    lstParents.AddRange(StringUtils.GetListKyHieuParent(lstSXaNoiMaChildSearch));
                    if (lstParents.Any(x => x.Count() >= 3))
                    {
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
                    }
                    results = DataPopupSearchItems.Where(x => lstParents.Contains(x.SXauNoiMa)).ToList();
                }
                if (!lstSXaNoiMaParentSearch.IsEmpty())
                {
                    if (results.IsEmpty())
                        results = GetDataParent(lstSXaNoiMaParentSearch);
                    else
                        results.AddRange(GetDataParent(lstSXaNoiMaParentSearch.Where(x => !lstParents.Contains(x)).ToList()));
                }
                DataSearch = new ObservableCollection<BhDtctgBHXHChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhDtctgBHXHChiTietModel>();
            }
            _ndtctgBHXHChiTietModelView.Refresh();
        }

        private List<BhDtctgBHXHChiTietModel> GetDataParent(List<string> lstInput)
        {
            var result = new List<BhDtctgBHXHChiTietModel>();
            var lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            var lstData = DataPopupSearchItems.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhDtctgBHXHChiTietModel> lstInput, List<BhDtctgBHXHChiTietModel> results)
        {
            var itemChild = DataPopupSearchItems.Where(x => lstInput.Select(x => x.IID_MLNS).Distinct().Contains(x.IID_MLNS_Cha ?? Guid.Empty)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => DataPopupSearchItems.Select(y => y.IID_MLNS_Cha).Distinct().Contains(x.IID_MLNS)))
                {
                    GetListChild(new List<BhDtctgBHXHChiTietModel>() { item }, results);
                }
            }
        }

        #endregion
    }
}
