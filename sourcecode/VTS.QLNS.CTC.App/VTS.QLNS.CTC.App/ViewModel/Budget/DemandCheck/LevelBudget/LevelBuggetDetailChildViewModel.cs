using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.LevelBudget
{
    public class LevelBuggetDetailChildViewModel : DetailViewModelBase<LevelBuggetModel, LevelBuggetDetailChildModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILbChungTuChiTietPhanCapService _soLieuChiTietPhanCapService;
        private readonly ILog _logger;
        private readonly INsDonViService _nSDonViService;
        private readonly IDanhMucService _danhMucService;

        public string XauNoiMa;
        public string ListXauNoiMa;
        public string IdChiTiet;
        public double TotalGlobal;

        public bool IsSaveData => Items != null && Items.Any(item => item.IsModified || item.IsDeleted);
        public bool IsDeleteAll => Items != null && Items.Any(item => !item.IsModified && item.HasData);
        public bool IsEnableButtonDelete => SelectedItem != null && !SelectedItem.IsHangCha;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;

        private string _id_DonVi;
        public string Id_DonVi
        {
            get => _id_DonVi;
            set => SetProperty(ref _id_DonVi, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private double _totalPhanCap;
        public double TotalPhanCap
        {
            get => _totalPhanCap;
            set => SetProperty(ref _totalPhanCap, value);
        }

        private double _totalHienVat;
        public double TotalHienVat
        {
            get => _totalHienVat;
            set => SetProperty(ref _totalHienVat, value);
        }

        public RelayCommand SaveDataCommand { get; }
        public RelayCommand CloseCommand { get; }

        public LevelBuggetDetailChildViewModel(
          IMapper mapper,
          INsDonViService nSDonViService,
          ISessionService sessionService,
          ILbChungTuChiTietPhanCapService soLieuChiTietPhanCapService,
          IDanhMucService danhMucService,
          ILog logger) : base(danhMucService, sessionService)
        {
            _mapper = mapper;
            _logger = logger;
            _nSDonViService = nSDonViService;
            _danhMucService = danhMucService;
            _sessionService = sessionService;
            _soLieuChiTietPhanCapService = soLieuChiTietPhanCapService;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            CloseCommand = new RelayCommand(OnClose);
        }

        public override void Init()
        {
            base.Init();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                List<LbChiTietPhanCapQuery> data = new List<LbChiTietPhanCapQuery>();
                data = _soLieuChiTietPhanCapService.GetSoLieuChiTietPhanCap(_sessionService.Current.YearOfWork, XauNoiMa, ListXauNoiMa, IdChiTiet).ToList();
                Items = _mapper.Map<ObservableCollection<Model.LevelBuggetDetailChildModel>>(data);
                int index = 0;

                NsNganhChungTuChiTietPhanCap rootData = _soLieuChiTietPhanCapService.FindByCondition(Model.IdDonVi, IdChiTiet, _sessionService.Current.YearOfWork);

                DonVi donViLoai0 = _nSDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
                DanhMuc itemDanhMuc = _danhMucService.FindByType("DM_CauHinh", _sessionService.Current.YearOfWork).Where(n => n.SGiaTri == "2" && n.IIDMaDanhMuc == "CAP_DON_VI").FirstOrDefault();

                if (Items != null && Items.Count > 0)
                {
                    LevelBuggetDetailChildModel firstChild = Items.Where(n => !n.IsHangCha).FirstOrDefault();
                    if (firstChild == null)
                        return;
                    index = Items.IndexOf(firstChild);
                    if (itemDanhMuc != null && donViLoai0 != null)
                    {
                        NsNganhChungTuChiTietPhanCap itemDonViLoai0 = _soLieuChiTietPhanCapService.FindByCondition(donViLoai0.IIDMaDonVi, IdChiTiet, _sessionService.Current.YearOfWork);
                        Items.Insert(index, new LevelBuggetDetailChildModel
                        {
                            MucLucID = firstChild.MucLucID,
                            LNS = firstChild.LNS,
                            L = firstChild.L,
                            K = firstChild.K,
                            M = firstChild.M,
                            TM = firstChild.TM,
                            TTM = firstChild.TTM,
                            NG = firstChild.NG,
                            TNG1 = firstChild.TNG1,
                            TNG2 = firstChild.TNG2,
                            TNG3 = firstChild.TNG3,
                            MoTa = firstChild.MoTa,
                            IdDonVi = donViLoai0.IIDMaDonVi,
                            TenDonVi = donViLoai0.TenDonVi,
                            IsRoot = true,
                            Id = itemDonViLoai0 == null ? Guid.Empty : itemDonViLoai0.Id,
                            PhanCap = itemDonViLoai0 != null && itemDonViLoai0.FPhanCap.HasValue ? itemDonViLoai0.FPhanCap.Value : 0,
                            XauNoiMa = firstChild.XauNoiMa,
                            IdDonViMLNS = firstChild.IdDonViMLNS,
                            IsHangCha = false
                        });
                    }

                    Items.Insert(index, new LevelBuggetDetailChildModel
                    {
                        MucLucID = firstChild.MucLucID,
                        LNS = firstChild.LNS,
                        L = firstChild.L,
                        K = firstChild.K,
                        M = firstChild.M,
                        TM = firstChild.TM,
                        TTM = firstChild.TTM,
                        NG = firstChild.NG,
                        TNG1 = firstChild.TNG1,
                        TNG2 = firstChild.TNG2,
                        TNG3 = firstChild.TNG3,
                        MoTa = "-- Số chưa phân bổ --",
                        IsTotal = true,
                        PhanCap = TotalGlobal,
                        XauNoiMa = firstChild.XauNoiMa,
                        IdDonViMLNS = firstChild.IdDonViMLNS,
                        IsHangCha = true
                    });

                    Items.Insert(index, new LevelBuggetDetailChildModel
                    {
                        MucLucID = firstChild.MucLucID,
                        LNS = firstChild.LNS,
                        L = firstChild.L,
                        K = firstChild.K,
                        M = firstChild.M,
                        TM = firstChild.TM,
                        TTM = firstChild.TTM,
                        NG = firstChild.NG,
                        TNG1 = firstChild.TNG1,
                        TNG2 = firstChild.TNG2,
                        TNG3 = firstChild.TNG3,
                        MoTa = firstChild.MoTa,
                        PhanCap = TotalGlobal,
                        XauNoiMa = firstChild.XauNoiMa,
                        IdDonViMLNS = firstChild.IdDonViMLNS,
                        IsHangCha = true
                    });
                }
                foreach (LevelBuggetDetailChildModel model in Items)
                {
                    if (!model.IsHangCha)
                    {
                        model.PropertyChanged += DetailModel_PropertyChanged;
                    }
                }
                CalculateData();
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(LevelBuggetDetailChildModel.PhanCap) || args.PropertyName == nameof(LevelBuggetDetailChildModel.HienVat)
                || args.PropertyName == nameof(LevelBuggetDetailChildModel.GhiChu))
            {
                LevelBuggetDetailChildModel item = Items.Where(x => !x.IsHangCha && x.IdDonVi == ((LevelBuggetDetailChildModel)sender).IdDonVi).First();
                item.IsModified = true;
                if (args.PropertyName == nameof(LevelBuggetDetailChildModel.PhanCap) || args.PropertyName == nameof(LevelBuggetDetailChildModel.HienVat))
                {
                    CalculateData();
                }
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEnableButtonDelete));
        }

        private void CalculateData()
        {
            if (Items == null && Items.Count == 0)
                return;
            Items.Where(n => n.IsHangCha).Select(n => { n.PhanCap = TotalGlobal; n.HienVat = 0; return n; }).ToList();
            LevelBuggetDetailChildModel totalItem = Items.Where(n => n.IsTotal).FirstOrDefault();
            if (totalItem == null)
                return;
            totalItem.PhanCap -= Items.Where(n => !n.IsHangCha && !n.IsDeleted).Select(n => n.PhanCap).Sum();
            TotalPhanCap = Items.Where(n => !n.IsHangCha && !n.IsDeleted).Select(n => n.PhanCap).Sum();

            TotalHienVat = Items.Where(n => !n.IsHangCha && !n.IsDeleted).Select(n => n.HienVat).Sum();

            Items.Where(n => n.IsHangCha && !n.IsTotal).Select(n => { n.HienVat = TotalHienVat; return n; }).ToList();
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha && !Model.IsLocked)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void OnSaveData()
        {
            try
            {
                List<LevelBuggetDetailChildModel> listAdd = Items.Where(x => !x.IsHangCha && x.IsModified && (x.Id == Guid.Empty || x.Id == null) && !x.IsDeleted).ToList();
                List<LevelBuggetDetailChildModel> listUpdate = Items.Where(x => !x.IsHangCha && x.IsModified && x.Id != Guid.Empty && x.Id != null && !x.IsDeleted).ToList();
                List<LevelBuggetDetailChildModel> listDelete = Items.Where(x => !x.IsHangCha && x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
                if (listAdd.Count > 0)
                {
                    List<NsNganhChungTuChiTietPhanCap> listChiTiet = new List<NsNganhChungTuChiTietPhanCap>();
                    listChiTiet = _mapper.Map<List<NsNganhChungTuChiTietPhanCap>>(listAdd);
                    listChiTiet = listChiTiet.Select(x =>
                    {
                        x.INamLamViec = _sessionService.Current.YearOfWork;
                        x.DNgayTao = DateTime.Now;
                        x.SNguoiTao = _sessionService.Current.Principal;
                        x.IIdCtnganhChiTiet = Guid.Parse(IdChiTiet);
                        return x;
                    }).ToList();
                    _soLieuChiTietPhanCapService.AddRange(listChiTiet);
                }

                if (listUpdate.Count > 0)
                {
                    foreach (var item in listUpdate)
                    {
                        item.IsModified = false;
                        NsNganhChungTuChiTietPhanCap entity = _soLieuChiTietPhanCapService.Find(item.Id);
                        if (entity != null)
                        {
                            entity.FHienVat = item.HienVat;
                            entity.FPhanCap = item.PhanCap;
                            entity.SGhiChu = item.GhiChu;
                            entity.DNgaySua = DateTime.Now;
                            entity.SNguoiSua = _sessionService.Current.Principal;
                            _soLieuChiTietPhanCapService.Update(entity);
                        }
                    }
                }

                if (listDelete.Count > 0)
                {
                    foreach (var item in listDelete)
                    {
                        NsNganhChungTuChiTietPhanCap entity = _soLieuChiTietPhanCapService.Find(item.Id);
                        if (entity != null)
                            _soLieuChiTietPhanCapService.Delete(entity);
                    }
                }
                SavedAction?.Invoke(null);
                MessageBoxHelper.Info(Resources.MsgSaveDone);
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                _soLieuChiTietPhanCapService.DeleteByNganhChiTiet(Guid.Parse(IdChiTiet));
                SavedAction?.Invoke(null);
                LoadData();
                MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }
    }
}
