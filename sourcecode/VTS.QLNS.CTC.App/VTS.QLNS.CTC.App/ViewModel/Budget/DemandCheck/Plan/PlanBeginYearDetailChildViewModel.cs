using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan
{
    public class PlanBeginYearDetailChildViewModel : DetailViewModelBase<PlanBeginYearModel, SktSoLieuPhanCapModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private ISoLieuChiTietPhanCapService _soLieuChiTietPhanCapService;
        private readonly ILog _logger;
        private INsDonViService _nSDonViService;
        private IDanhMucService _danhMucService;

        public string XauNoiMa;
        public string ListXauNoiMa;
        public string IdChiTiet;
        public double TotalGlobal;

        public string sXauNoiMaGoc;

        public bool IsSaveData => Items != null && Items.Any(item => item.IsModified || item.IsDeleted) && !IsReadOnlyTable;
        public bool IsEnableButtonDelete => SelectedItem != null && !SelectedItem.IsHangCha && !IsReadOnlyTable;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public bool IsDeleteAll => Items.Any(item => !item.IsModified && item.HasData);

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

        private double _totalTuChi;
        public double TotalTuChi
        {
            get => _totalTuChi;
            set => SetProperty(ref _totalTuChi, value);
        }

        private bool _isReadOnlyTable;
        public bool IsReadOnlyTable
        {
            get => _isReadOnlyTable;
            set => SetProperty(ref _isReadOnlyTable, value);
        }

        public RelayCommand SaveDataCommand { get; }
        public RelayCommand CloseCommand { get; }

        public PlanBeginYearDetailChildViewModel(
          IMapper mapper,
          ISessionService sessionService,
          INsDonViService nSDonViService,
          IDanhMucService danhMucService,
          ISoLieuChiTietPhanCapService soLieuChiTietPhanCapService,
          ILog logger
            ) : base(danhMucService, sessionService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nSDonViService = nSDonViService;
            _danhMucService = danhMucService;
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
                List<SoLieuChiTietPhanCapQuery> data = new List<SoLieuChiTietPhanCapQuery>();
                if (IdChiTiet == "")
                {
                    IdChiTiet = null;
                }
                if (IsReadOnlyTable)
                {
                    data = _soLieuChiTietPhanCapService.GetSoLieuChiTietPhanCapDonVi0(_sessionService.Current.YearOfWork, XauNoiMa, ListXauNoiMa, IdChiTiet).ToList();
                }
                else
                {
                    data = _soLieuChiTietPhanCapService.GetSoLieuChiTietPhanCapDTDN(_sessionService.Current.YearOfWork, XauNoiMa, ListXauNoiMa, IdChiTiet, Model.Id, sXauNoiMaGoc).ToList();
                }

                Items = _mapper.Map<ObservableCollection<Model.SktSoLieuPhanCapModel>>(data);
                int index = 0;
                DonVi donViLoai0 = _nSDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
                DanhMuc itemDanhMuc = _danhMucService.FindByType("DM_CauHinh", _sessionService.Current.YearOfWork).Where(n => n.SGiaTri == "2" && n.IIDMaDanhMuc == "CAP_DON_VI").FirstOrDefault();
                if (Items != null && Items.Count > 0)
                {
                    SktSoLieuPhanCapModel firstChild = Items.Where(n => !n.IsHangCha).FirstOrDefault();
                    if (firstChild == null)
                        return;
                    index = Items.IndexOf(firstChild);

                    if (itemDanhMuc != null && donViLoai0 != null)
                    {
                        if (IsReadOnlyTable)
                        {
                            List<NsDtdauNamPhanCap> listItemDonViLoai0 = _soLieuChiTietPhanCapService.FindDonViTongHop(donViLoai0.IIDMaDonVi,
                                            firstChild.MucLucID.ToString(), _sessionService.Current.YearOfWork).ToList();
                            Items.Insert(index, new SktSoLieuPhanCapModel
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
                                IsTotal = true,
                                TuChi = listItemDonViLoai0 != null ? listItemDonViLoai0.Where(n => n.FTuChi.HasValue).Select(n => n.FTuChi.Value).ToList().Sum() : 0,
                                Id = Guid.Empty,
                                XauNoiMa = firstChild.XauNoiMa,
                                IdDonViMLNS = donViLoai0.IIDMaDonVi,
                                IdDonVi = donViLoai0.IIDMaDonVi,
                                TenDonVi = donViLoai0.TenDonVi,
                                IsHangCha = false
                            });
                        }
                        else
                        {
                            NsDtdauNamPhanCap itemDonViLoai0 = _soLieuChiTietPhanCapService.FindByCondition(donViLoai0.IIDMaDonVi, IdChiTiet, _sessionService.Current.YearOfWork);
                            Items.Insert(index, new SktSoLieuPhanCapModel
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
                                IsTotal = true,
                                TuChi = itemDonViLoai0 != null && itemDonViLoai0.FTuChi.HasValue ? itemDonViLoai0.FTuChi.Value : 0,
                                Id = itemDonViLoai0 != null ? itemDonViLoai0.Id : Guid.Empty,
                                XauNoiMa = firstChild.XauNoiMa,
                                IdDonViMLNS = donViLoai0.IIDMaDonVi,
                                IdDonVi = donViLoai0.IIDMaDonVi,
                                TenDonVi = donViLoai0.TenDonVi,
                                IsHangCha = false
                            });
                        }
                    }

                    Items.Insert(index, new SktSoLieuPhanCapModel
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
                        TuChi = TotalGlobal,
                        XauNoiMa = firstChild.XauNoiMa,
                        IdDonViMLNS = firstChild.IdDonViMLNS,
                        IsHangCha = true
                    });
                    Items.Insert(index, new SktSoLieuPhanCapModel
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
                        TuChi = TotalGlobal,
                        IsRoot = true,
                        XauNoiMa = firstChild.XauNoiMa,
                        IdDonViMLNS = firstChild.IdDonViMLNS,
                        IsHangCha = true
                    });
                }
                foreach (SktSoLieuPhanCapModel model in Items)
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
            if (args.PropertyName == nameof(SktSoLieuPhanCapModel.TuChi) || args.PropertyName == nameof(SktSoLieuPhanCapModel.GhiChu))
            {
                SktSoLieuPhanCapModel item = Items.Where(x => !x.IsHangCha && x.IdDonVi == ((SktSoLieuPhanCapModel)sender).IdDonVi).First();
                item.IsModified = true;
                if (args.PropertyName == nameof(SktSoLieuPhanCapModel.TuChi))
                {
                    CalculateData();
                }
                OnPropertyChanged(nameof(IsDeleteAll));
                OnPropertyChanged(nameof(IsSaveData));
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
            Items.Where(n => n.IsHangCha).Select(n => { n.TuChi = TotalGlobal; return n; }).ToList();
            SktSoLieuPhanCapModel totalItem = Items.Where(n => n.IsTotal).FirstOrDefault();
            if (totalItem == null)
                return;
            totalItem.TuChi -= Items.Where(n => !n.IsHangCha && !n.IsDeleted).Select(n => n.TuChi).Sum();
            TotalTuChi = Items.Where(n => !n.IsHangCha && !n.IsDeleted).Select(n => n.TuChi).Sum();
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha && !IsReadOnlyTable)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            if (Model.IsLocked || string.IsNullOrEmpty(IdChiTiet))
            {
                return;
            }
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                _soLieuChiTietPhanCapService.DeleteByVoucherId(Guid.Parse(IdChiTiet));
                LoadData();
                MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void OnSaveData()
        {
            try
            {
                List<SktSoLieuPhanCapModel> listAdd = Items.Where(x => !x.IsHangCha && x.IsModified && (x.Id == Guid.Empty || x.Id == null) && !x.IsDeleted).ToList();
                List<SktSoLieuPhanCapModel> listUpdate = Items.Where(x => !x.IsHangCha && x.IsModified && x.Id != Guid.Empty && x.Id != null && !x.IsDeleted).ToList();
                List<SktSoLieuPhanCapModel> listDelete = Items.Where(x => !x.IsHangCha && x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
                if (listAdd.Count > 0)
                {
                    List<NsDtdauNamPhanCap> listChiTiet = new List<NsDtdauNamPhanCap>();
                    listChiTiet = _mapper.Map<List<NsDtdauNamPhanCap>>(listAdd);
                    listChiTiet = listChiTiet.Select(x =>
                    {
                        x.INamLamViec = _sessionService.Current.YearOfWork;
                        x.DNgayTao = DateTime.Now;
                        x.SNguoiTao = _sessionService.Current.Principal;
                        x.IIdCtdtDauNam = Model.Id;
                        x.IIdCtdtdauNamChiTiet = IdChiTiet == null ? Guid.Empty : Guid.Parse(IdChiTiet);
                        x.sXauNoiMaGoc = sXauNoiMaGoc;
                        return x;
                    }).ToList();
                    _soLieuChiTietPhanCapService.AddRange(listChiTiet);
                }

                if (listUpdate.Count > 0)
                {
                    foreach (var item in listUpdate)
                    {
                        item.IsModified = false;
                        NsDtdauNamPhanCap entity = _soLieuChiTietPhanCapService.Find(item.Id);
                        if (entity != null)
                        {
                            entity.FTuChi = item.TuChi;
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
                        NsDtdauNamPhanCap entity = _soLieuChiTietPhanCapService.Find(item.Id);
                        if (entity != null)
                            _soLieuChiTietPhanCapService.Delete(entity);
                    }
                }
                var message = Resources.MsgSaveDone;
                //var messageBox = new NSMessageBoxViewModel(message, Resources.NotifiTitle, NSMessageBoxButtons.OK, null);
                OnPropertyChanged(nameof(IsDeleteAll));
                //DialogHost.Show(messageBox.Content, "PlanBeginYearChildDialog");
                MessageBoxHelper.Info(message);
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
    }
}
