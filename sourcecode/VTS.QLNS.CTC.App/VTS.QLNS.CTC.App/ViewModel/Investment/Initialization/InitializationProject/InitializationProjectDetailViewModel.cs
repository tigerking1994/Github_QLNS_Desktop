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
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.LevelBudget;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.Initialization.InitializationProject
{
    public class InitializationProjectDetailViewModel : DetailViewModelBase<InitializationProjectModel, InitializationProjectDetailModel>
    {
        private IVdtKtKhoiTaoService _chungTuService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private IVdtKtKhoiTaoChiTietService _chungTuChiTietService;
        private INsDonViService _nsDonViService;
        private ICollectionView _dataDetailFilter;
        private ICollectionView _budgetCatalogFilter;
        private INsMucLucNganSachService _mucLucNganSachService;
        private readonly ILog _logger;
        private static Dictionary<string, string> _dicMucLucNganSach = new Dictionary<string, string>();
        private static string sL;
        private static string sK;

        public List<NsMucLucNganSach> DataMucLucNganSach;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public Action<int> SavedAction;

        public RelayCommand SaveDataCommand { get; }
        public RelayCommand CloseWindowCommand { get; }

        public InitializationProjectDetailViewModel(IVdtKtKhoiTaoService cpChungTuService,
           IVdtKtKhoiTaoChiTietService chungTuChiTietService,
           IMapper mapper,
           ISessionService sessionService,
           INsDonViService nsDonViService,
           ILog logger,
           INsMucLucNganSachService mucLucNganSachService)
        {
            _mapper = mapper;
            _chungTuService = cpChungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mucLucNganSachService = mucLucNganSachService;
            _logger = logger;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new System.Windows.Thickness(10);
                GetMucLucNganSachByParent();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        protected override void OnAdd()
        {
            if (SelectedItem != null)
            {
                int currentRow = Items.IndexOf(SelectedItem);

                if (currentRow > -1)
                {
                    InitializationProjectDetailModel targetItem = new InitializationProjectDetailModel
                    {
                        LNS = SelectedItem.LNS,
                        L = SelectedItem.L,
                        K = SelectedItem.K,

                        M = SelectedItem.M,
                        TM = SelectedItem.TM,
                        TTM = SelectedItem.TTM,
                        NG = SelectedItem.NG,
                        IsModified = true,
                        MaNguonNganSach = SelectedItem.MaNguonNganSach,
                        TenNganSach = SelectedItem.TenNganSach,
                        MoTaNganSach = SelectedItem.MoTaNganSach,
                        IdNguonVonID = SelectedItem.IdNguonVonID,
                        IdLoaiNguonVonID = SelectedItem.IdLoaiNguonVonID
                    };
                    targetItem.PropertyChanged += DetailModel_PropertyChanged;
                    Items.Insert(currentRow + 1, targetItem);
                    OnPropertyChanged(nameof(Items));
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }
        }

        protected override void OnRefresh()
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnSaveData()
        {
            DateTime dStartDate = DateTime.Now;
            StringBuilder messageBuilder = new StringBuilder();
            List<string> lstXauNoiChuoi;
            try
            {
                foreach (var item in Items.Where(n => n.IsModified && !n.IsDeleted))
                {
                    lstXauNoiChuoi = new List<string>();
                    lstXauNoiChuoi.Add(item.LNS ?? string.Empty);
                    lstXauNoiChuoi.Add(item.L ?? string.Empty);
                    lstXauNoiChuoi.Add(item.K ?? string.Empty);
                    lstXauNoiChuoi.Add(item.M ?? string.Empty);
                    lstXauNoiChuoi.Add(item.TM ?? string.Empty);
                    lstXauNoiChuoi.Add(item.TTM ?? string.Empty);
                    lstXauNoiChuoi.Add(item.NG ?? string.Empty);

                    if (string.IsNullOrEmpty(string.Join("", lstXauNoiChuoi)))
                    {
                        messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Mục lục ngân sách");
                        break;
                    }
                    if (lstXauNoiChuoi.IndexOf(string.Empty) != -1 &&
                        lstXauNoiChuoi.IndexOf(string.Empty) < lstXauNoiChuoi.LastIndexOf(lstXauNoiChuoi.LastOrDefault(n => !string.IsNullOrEmpty(n))))
                    {
                        messageBuilder.AppendFormat(Resources.MsgErrorFormat, "Mục lục ngân sách");
                        break;
                    }
                }
                if (messageBuilder.Length != 0)
                {
                    System.Windows.MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                    return;
                }

                List<InitializationProjectDetailModel> dataDetailsAdd = Items.Where(x => x.IsModified && (x.IdDb == Guid.Empty || x.IdDb == null) && !x.IsDeleted).ToList();
                List<InitializationProjectDetailModel> dataDetailsUpdate = Items.Where(x => x.IsModified && x.IdDb != Guid.Empty && x.IdDb != null && !x.IsDeleted).ToList();
                List<InitializationProjectDetailModel> dataDetailsDelete = Items.Where(x => x.IsDeleted && x.IdDb != Guid.Empty && x.IdDb != null).ToList();

                // Thêm mới chứng từ chi tiết
                if (dataDetailsAdd.Count > 0)
                {
                    dataDetailsAdd = dataDetailsAdd.Select(x => { x.IdKhoiTaoID = Model.Id; return x; }).ToList();
                    List<VdtKtKhoiTaoChiTiet> listChungTuChiTiets = new List<VdtKtKhoiTaoChiTiet>();
                    listChungTuChiTiets = _mapper.Map<List<VdtKtKhoiTaoChiTiet>>(dataDetailsAdd);
                    listChungTuChiTiets.Select(n => { n.Id = Guid.Empty; return n; }).ToList();
                    _chungTuChiTietService.AddRange(listChungTuChiTiets);
                }

                // Cập nhật chứng từ chi tiết
                if (dataDetailsUpdate.Count > 0)
                {
                    foreach (var item in dataDetailsUpdate)
                    {
                        item.IsModified = false;
                        item.Id = item.IdDb.Value;
                        VdtKtKhoiTaoChiTiet chungTuChiTiet = _chungTuChiTietService.Find(item.IdDb.Value);
                        if (chungTuChiTiet != null)
                        {
                            _mapper.Map(item, chungTuChiTiet);
                            _chungTuChiTietService.Update(chungTuChiTiet);
                        }
                    }
                }

                // Delete
                if (dataDetailsDelete.Count > 0)
                {
                    foreach (var item in dataDetailsDelete)
                    {
                        _chungTuChiTietService.Delete(item.IdDb.Value);
                    }
                }
                System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                SavedAction?.Invoke(1);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetMucLucNganSachByParent()
        {
            DataMucLucNganSach = _mucLucNganSachService.FindAll(Model.NamKhoiTao).ToList();
            if (DataMucLucNganSach != null && DataMucLucNganSach.Any())
            {
                if (DataMucLucNganSach.Any(n => !string.IsNullOrEmpty(n.L)))
                {
                    sL = DataMucLucNganSach.FirstOrDefault(n => !string.IsNullOrEmpty(n.L)).L;
                }
                if (DataMucLucNganSach.Any(n => n.L == sL && !string.IsNullOrEmpty(n.K)))
                {
                    sK = DataMucLucNganSach.FirstOrDefault(n => n.L == sL && !string.IsNullOrEmpty(n.K)).K;
                }
                List<string> lstItem = DataMucLucNganSach.Select(n => (n.Lns + "-" + n.L + "-" + n.K + "-" + n.M + "-" + n.Tm + "-" + n.Ttm + "-" + n.Ng)).ToList();
                _dicMucLucNganSach = lstItem.Distinct().ToDictionary(n => n, n => n);
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                base.LoadData(args);
                if (Model == null || Model.Id == Guid.Empty || !Model.DuAnId.HasValue)
                {
                    return;
                }
                List<KhoiTaoChiTietQuery> list = _chungTuChiTietService.FindDataKhoiTaoChiTiet(Model.Id.ToString(), Model.DuAnId.Value.ToString()).ToList();
                Items = _mapper.Map<ObservableCollection<InitializationProjectDetailModel>>(list);
                if (Items != null && Items.Count > 0 && DataMucLucNganSach != null && DataMucLucNganSach.Count > 0)
                {
                    foreach (InitializationProjectDetailModel model in Items)
                    {
                        NsMucLucNganSach mucluc = DataMucLucNganSach.Where(n => n.Id == model.IdLoaiNguonVonID).FirstOrDefault();
                        if (mucluc != null)
                        {
                            model.LNS = mucluc.Lns;
                            model.L = mucluc.L;
                            model.K = mucluc.K;
                            model.M = mucluc.M;
                            model.TM = mucluc.Tm;
                            model.TTM = mucluc.Ttm;
                            model.NG = mucluc.Ng;
                        }
                    }
                }
                foreach (InitializationProjectDetailModel model in Items)
                {
                    model.PropertyChanged += DetailModel_PropertyChanged;
                }
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(InitializationProjectDetailModel.KHVonHetNamTruoc) ||
                args.PropertyName == nameof(InitializationProjectDetailModel.LuyKeThanhToanKLHT) ||
                args.PropertyName == nameof(InitializationProjectDetailModel.LuyKeThanhToanTamUng) ||
                args.PropertyName == nameof(InitializationProjectDetailModel.ThanhToanQuaKB) ||
                args.PropertyName == nameof(InitializationProjectDetailModel.TamUngQuaKB) ||
                args.PropertyName == nameof(InitializationProjectDetailModel.TiGiaDonVi) ||
                args.PropertyName == nameof(InitializationProjectDetailModel.TiGia) ||
                args.PropertyName == nameof(InitializationProjectDetailModel.SoChuyenChiTieuDaCap) ||
                args.PropertyName == nameof(InitializationProjectDetailModel.SoChuyenChiTieuChuaCap)
                )
            {
                InitializationProjectDetailModel item = Items.Where(x => x.Id == ((InitializationProjectDetailModel)sender).Id).First();
                if (item != null)
                    item.IsModified = true;
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        public void GetInfoDuAn()
        {
            if (SelectedItem == null || SelectedItem.IsDeleted)
                return;
            string sXauNoiChuoi = SelectedItem.LNS + "-" + SelectedItem.L + "-" + SelectedItem.K + "-" + SelectedItem.M + "-" + SelectedItem.TM + "-" + SelectedItem.TTM + "-" + SelectedItem.NG;
            if (!_dicMucLucNganSach.ContainsKey(sXauNoiChuoi))
            {
                System.Windows.MessageBox.Show(Resources.MsgErrorMucLucNganSachNotExist);
                SelectedItem.IsDeleted = true;
                return;
            }
            NsMucLucNganSach itemMucLuc = DataMucLucNganSach.Where(n => n.XauNoiMa == sXauNoiChuoi).FirstOrDefault();
            if (itemMucLuc != null)
            {
                SelectedItem.IdLoaiNguonVonID = itemMucLuc.Id;
            }
        }

        private void OnCloseWindow()
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
