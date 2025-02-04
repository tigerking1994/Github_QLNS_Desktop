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
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexPlan.PlanDetail;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanDetail;


namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanDetail
{
    public class DmTiGiaDialogViewModel : DialogViewModelBase<NhDmTiGiaModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmLoaiTienTeService _nhDmLoaiTienTeService;
        private readonly INhHopDongService _nhHopDongService;

        public override string Title => "Tỉ Giá";
        public override string Description => "Danh mục tỉ giá";
        public override Type ContentType => typeof(DmTiGiaDialog);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public bool IsSelectTwoTiGia { get; set; }
        private ObservableCollection<NhDmTiGiaModel> _itemsTiGia;
        public ObservableCollection<NhDmTiGiaModel> ItemsTiGia
        {
            get => _itemsTiGia;
            set => SetProperty(ref _itemsTiGia, value);
        }

        private NhDmTiGiaModel _selectedItems;
        public NhDmTiGiaModel SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiTienTe;
        public ObservableCollection<ComboboxItem> CbxLoaiTienTe
        {
            get => _cbxLoaiTienTe;
            set => SetProperty(ref _cbxLoaiTienTe, value);
        }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public Guid? IIdTiGiaUsdVndId { get; set; }
        public Guid? IIdTiGiaEurUsdId { get; set; }
        public RelayCommand AddTiGiaCommand { get; }
        public RelayCommand DeleteTiGiaCommand { get; }
        public RelayCommand SelectedTiGiaCommand { get; }
        public RelayCommand VerifyCommand { get; }

        public DmTiGiaDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmLoaiTienTeService nhDmLoaiTienTeService,
            INhHopDongService nhHopDongService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmLoaiTienTeService = nhDmLoaiTienTeService;
            _nhHopDongService = nhHopDongService;

            AddTiGiaCommand = new RelayCommand(obj => OnAddTiGia());
            DeleteTiGiaCommand = new RelayCommand(obj => OnDeleteTiGia());
            VerifyCommand = new RelayCommand(obj => OnVerify(obj));
        }

        public override void Init()
        {
            LoadData();
            LoadDmTienTe();
        }

        public override void LoadData(params object[] args)
        {
            List<NhDmTiGia> listTiGia = _nhDmTiGiaService.FindAll().ToList();
            ItemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(listTiGia);
            SetCheckedTiGia();
            if (ItemsTiGia == null)
            {
                ItemsTiGia = new ObservableCollection<NhDmTiGiaModel>();
            }
        }

        private void SetCheckedTiGia()
        {
            if (IIdTiGiaUsdNgoaiTeKhacId != null)
            {
                ItemsTiGia.Where(n => n.Id == IIdTiGiaUsdNgoaiTeKhacId).ToList().ForEach(n => n.Selected = true);
            }
            if (IIdTiGiaUsdVndId != null)
            {
                ItemsTiGia.Where(n => n.Id == IIdTiGiaUsdVndId).ToList().ForEach(n => n.Selected = true);
            }
            if (IIdTiGiaEurUsdId != null)
            {
                ItemsTiGia.Where(n => n.Id == IIdTiGiaEurUsdId).ToList().ForEach(n => n.Selected = true);
            }
            OnPropertyChanged(nameof(ItemsTiGia));
        }

        private void LoadDmTienTe()
        {
            List<NhDmLoaiTienTe> lstTienTe = _nhDmLoaiTienTeService.FindAll().ToList();
            if (lstTienTe == null) return;
            var drpItem = lstTienTe.Select(n => new ComboboxItem() { ValueItem = n.SMaTienTe, HiddenValue = n.Id.ToString(), DisplayItem = (n.SMaTienTe) });
            _cbxLoaiTienTe = _mapper.Map<ObservableCollection<ComboboxItem>>(drpItem);
            OnPropertyChanged(nameof(CbxLoaiTienTe));
        }

        private void OnAddTiGia()
        {
            NhDmTiGiaModel newItem = new NhDmTiGiaModel();
            ItemsTiGia.Insert(0, newItem);
            OnPropertyChanged(nameof(ItemsTiGia));
        }
        private void OnDeleteTiGia()
        {
            if (SelectedItems != null)
            {
                SelectedItems.IsDeleted = !SelectedItems.IsDeleted;
            }
            OnPropertyChanged(nameof(ItemsTiGia));
        }

        public override void OnClose(object obj)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public override void OnSave(object obj)
        {
            var lstInsert = ItemsTiGia.Where(x => !x.IsDeleted && x.Id == Guid.Empty && x.SMaTiGia != null
                                                && x.SMaTienTe1 != null && x.SMaTienTe2 != null && x.FTiGiaHoiDoai != 0).ToList();
            var lstUpdate = ItemsTiGia.Where(x => !x.IsDeleted && x.Id != Guid.Empty).ToList();
            var lstDelete = ItemsTiGia.Where(x => x.IsDeleted && x.Id != Guid.Empty).ToList();

            if (lstInsert != null && lstInsert.Count > 0)
            {
                if (lstInsert.Any(n => !n.SMaTienTe1.Contains("USD")))
                {
                    System.Windows.Forms.MessageBox.Show("Mã tiền tệ thứ 1 phải là USD !",Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                AddTiGiaSave(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count > 0)
            {
                UpdateTiGiaSave(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count > 0)
            {
                DeleteTiGiaSave(lstDelete);
            }
            LoadData();
        }

        private void UpdateTiGiaSave(List<NhDmTiGiaModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                NhDmTiGia tiGia = _nhDmTiGiaService.FindById(item.Id);
                if (tiGia != null)
                {
                    tiGia.SMaTiGia = item.SMaTiGia;
                    tiGia.STenTiGia = item.STenTiGia;
                    _nhDmTiGiaService.Update(tiGia);
                }
            }
        }

        private void DeleteTiGiaSave(List<NhDmTiGiaModel> listDelete)
        {
            foreach (var itemId in listDelete.Select(x => x.Id))
            {
                if (!_nhHopDongService.FindAll().Any(x => x.IIdTiGiaUsdVndId == itemId || x.IIdTiGiaUsdNgoaiTeKhacID == itemId))
                {
                    _nhDmTiGiaService.Delete(itemId);
                }
            }
        }

        private void AddTiGiaSave(List<NhDmTiGiaModel> listAdd)
        {
            if (listAdd != null && listAdd.Count > 0)
            {
                foreach (var item in listAdd)
                {
                    NhDmTiGia tiGia = new NhDmTiGia();
                    tiGia.Id = Guid.NewGuid();
                    _nhDmTiGiaService.Add(tiGia);
                }
            }
        }

        public void OnVerify(object obj)
        {
            try
            {
                List<NhDmTiGiaModel> SelectedsTiGia = ItemsTiGia.Where(n => n.Selected).ToList();
                if (!ValidateData(SelectedsTiGia)) return;

                var listTiGiaResponse = new List<NhDmTiGiaModel>();
                // xếp cặp tỉ giá USD-VND thứ 1, USD-EUR thứ 2, USD-Ngoại tệ khác thứ 3 (Để tính toán ở màn gọi đến màn này !)
                var lstUsdVnd = SelectedsTiGia.Where(x => x.SMaTiGia.Contains("USD-VND") || x.SMaTiGia.Contains("VND-USD")).ToList();
                var lstUsdEuro = SelectedsTiGia.Where(x => x.SMaTiGia.Contains("EUR-USD") || x.SMaTiGia.Contains("USD-EUR")).ToList();
                var lstUsdNgoaiTeKhac = SelectedsTiGia.Where(x => x.SMaTiGia.Contains("USD") && !x.SMaTiGia.Contains("EUR") && !x.SMaTiGia.Contains("VND")).ToList();
                listTiGiaResponse.AddRange(lstUsdVnd);
                listTiGiaResponse.AddRange(lstUsdEuro);
                listTiGiaResponse.AddRange(lstUsdNgoaiTeKhac);
                SavedAction?.Invoke(_mapper.Map<List<NhDmTiGiaModel>>(listTiGiaResponse));
                ((Window)obj).Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ValidateData(List<NhDmTiGiaModel> SelectedsTiGia)
        {
            List<string> lstError = new List<string>();
            if (SelectedsTiGia == null || SelectedsTiGia.Count == 0)
            {
                lstError.Add(string.Format(Resources.MsgCheckTiGiaNgoaiHoi));
            }
            if (SelectedsTiGia.Count == 1 && !SelectedsTiGia.Any(x => x.SMaTiGia.Contains("USD-VND") || x.SMaTiGia.Contains("VND-USD")))
            {
                lstError.Add(string.Format(Resources.MsgCheckLuaChonTiGia));
            }
            if (SelectedsTiGia.Count == 2)
            {
                var lstUsdVnd = SelectedsTiGia.Where(x => x.SMaTiGia.Contains("USD-VND") || x.SMaTiGia.Contains("VND-USD")).ToList();
                var lstUsdEuro = SelectedsTiGia.Where(x => x.SMaTiGia.Contains("EUR-USD") || x.SMaTiGia.Contains("USD-EUR")).ToList();
                var lstUsdNgoaiTeKhac = SelectedsTiGia.Where(x => x.SMaTiGia.Contains("USD") && !x.SMaTiGia.Contains("EUR") && !x.SMaTiGia.Contains("VND")).ToList();
                if (lstUsdVnd.Count != 1 || lstUsdEuro.Count != 1 || lstUsdNgoaiTeKhac.Count != 0)
                {
                    lstError.Add(string.Format(Resources.MsgCheckLuaChonTiGia));
                }
            }
            if (SelectedsTiGia.Count == 3)
            {
                if (IsSelectTwoTiGia)
                {
                    lstError.Add(string.Format(Resources.MsgCheck2CapTiGia));
                }
                else
                {
                    var lstUsdVnd = SelectedsTiGia.Where(x => x.SMaTiGia.Contains("USD-VND") || x.SMaTiGia.Contains("VND-USD")).ToList();
                    var lstUsdEuro = SelectedsTiGia.Where(x => x.SMaTiGia.Contains("EUR-USD") || x.SMaTiGia.Contains("USD-EUR")).ToList();
                    var lstUsdNgoaiTeKhac = SelectedsTiGia.Where(x => x.SMaTiGia.Contains("USD") && !x.SMaTiGia.Contains("EUR") && !x.SMaTiGia.Contains("VND")).ToList();
                    if (lstUsdVnd.Count != 1 || lstUsdEuro.Count != 1 || lstUsdNgoaiTeKhac.Count != 1)
                    {
                        lstError.Add(string.Format(Resources.MsgCheckLuaChonTiGia));
                    }
                }
            }
            if (SelectedsTiGia.Count > 3)
            {
                lstError.Add(string.Format(Resources.MsgCheck3CapTiGia));
            }
            if (lstError.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstError),
                    Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

    }
}
