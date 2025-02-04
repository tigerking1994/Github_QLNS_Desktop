using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.View.Forex.ForexPlan.PlanDetail;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanDetail
{
    public class PlanDetailDialogViewModel : DialogAttachmentViewModelBase<NhKhChiTietModel>
    {
        #region Private
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhHopDongService _nhHopDongService;
        private readonly INhKhChiTietHopDongService _nhKhChiTietHopDongService;
        private readonly INhKhChiTietService _nhKhChiTietService;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        #endregion

        public override string Name => "KẾ HOẠCH CHI TIẾT";
        public override string Title => "KẾ HOẠCH CHI TIẾT";
        public override Type ContentType => typeof(View.Forex.ForexPlan.PlanDetail.PlanDetailDialog);
        public bool IsInsert => Model.Id == Guid.Empty;
        public bool IsReadOnly { get; set; }
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.NH_KH_CHITIET;

        #region Items
        private ObservableCollection<VTS.QLNS.CTC.App.Model.NhKhChiTietHopDongModel> _itemsChiTietHopDong;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.NhKhChiTietHopDongModel> ItemsChiTietHopDong
        {
            get => _itemsChiTietHopDong;
            set => SetProperty(ref _itemsChiTietHopDong, value);
        }

        private NhKhChiTietHopDongModel _selectedChiTietHopDong;
        public NhKhChiTietHopDongModel SelectedChiTietHopDong
        {
            get => _selectedChiTietHopDong;
            set => SetProperty(ref _selectedChiTietHopDong, value);
        }

        private ObservableCollection<ComboboxItem> _cbxKhTongThe_NhiemVuChi;
        public ObservableCollection<ComboboxItem> CbxKhTongThe_NhiemVuChi
        {
            get => _cbxKhTongThe_NhiemVuChi;
            set => SetProperty(ref _cbxKhTongThe_NhiemVuChi, value);
        }

        private ObservableCollection<ComboboxItem> _dataKhTongTheGiaiDoan;
        public ObservableCollection<ComboboxItem> DataKhTongTheGiaiDoan
        {
            get => _dataKhTongTheGiaiDoan;
            set => SetProperty(ref _dataKhTongTheGiaiDoan, value);
        }

        private ComboboxItem _selectedKhTongTheGiaiDoan;
        public ComboboxItem SelectedKhTongTheGiaiDoan
        {
            get => _selectedKhTongTheGiaiDoan;
            set
            {
                SetProperty(ref _selectedKhTongTheGiaiDoan, value);
                if (value != null)
                {
                    Guid idKhTongTheGiaiDoan = new Guid(_selectedKhTongTheGiaiDoan.ValueItem);
                    LoadKhTongTheNamByParentId(idKhTongTheGiaiDoan);
                    LoadDonViByKhTongThe(idKhTongTheGiaiDoan);
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataKhTongTheNam;
        public ObservableCollection<ComboboxItem> DataKhTongTheNam
        {
            get => _dataKhTongTheNam;
            set => SetProperty(ref _dataKhTongTheNam, value);
        }

        private ComboboxItem _selectedKhTongTheNam;
        public ComboboxItem SelectedKhTongTheNam
        {
            get => _selectedKhTongTheNam;
            set
            {
                SetProperty(ref _selectedKhTongTheNam, value);
                if (value != null)
                {
                    Guid idKhTongTheNam = new Guid(_selectedKhTongTheNam.ValueItem);
                    LoadDonViByKhTongThe(idKhTongTheNam);
                }
            }
        }

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
                if (_selectedDonVi != null && _selectedKhTongTheGiaiDoan != null)
                {
                    if (_selectedKhTongTheNam != null)
                    {
                        LoadKhTongThe_NhiemVuChi(new Guid(_selectedKhTongTheNam.ValueItem), new Guid(_selectedDonVi.ValueItem));
                    }
                    else
                    {
                        LoadKhTongThe_NhiemVuChi(new Guid(_selectedKhTongTheGiaiDoan.ValueItem), new Guid(_selectedDonVi.ValueItem));
                    }
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataHopDong;
        public ObservableCollection<ComboboxItem> DataHopDong
        {
            get => _dataHopDong;
            set => SetProperty(ref _dataHopDong, value);
        }

        #endregion

        #region RelayCommand
        public RelayCommand AddNguonVonDetailCommand { get; }
        public RelayCommand DeleteDetailCommand { get; }
        public RelayCommand AddTiGiaCommand { get; }

        #endregion

        #region View
        public DmTiGiaDialogViewModel DmTiGiaDialogViewModel { get; set; }

        #endregion

        public PlanDetailDialogViewModel(ISessionService sessionService,
            IMapper mapper,
            INsDonViService nsDonViService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhKhTongTheService nhKhTongTheService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INhHopDongService nhHopDongService,
            INhKhChiTietHopDongService nhKhChiTietHopDongService,
            INhKhChiTietService nhKhChiTietService,
            DmTiGiaDialogViewModel dmTigiaDialogViewModel)
            : base(mapper, storageServiceFactory, attachService)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nhHopDongService = nhHopDongService;
            _nhKhChiTietHopDongService = nhKhChiTietHopDongService;
            _nhKhChiTietService = nhKhChiTietService;
            DmTiGiaDialogViewModel = dmTigiaDialogViewModel;

            AddNguonVonDetailCommand = new RelayCommand(obj => OnAddChiTietHopDongDetail());
            DeleteDetailCommand = new RelayCommand(obj => OnDeleteChiTietHopDongDetail());
        }

        #region Event
        public override void Init()
        {
            InitData();
            LoadAttach();
            LoadKhTongTheGiaiDoan();
            LoadSoHopDong();
            LoadData();
        }

        private void InitData()
        {
            _dataDonVi = null;
            _dataKhTongTheGiaiDoan = null;
            _dataKhTongTheNam = null;
            OnPropertyChanged(nameof(DataDonVi));
            OnPropertyChanged(nameof(DataKhTongTheGiaiDoan));
            OnPropertyChanged(nameof(DataKhTongTheNam));
        }

        public override void LoadData(params object[] args)
        {
            if (!Model.Id.IsNullOrEmpty())
            {
                Description = "Cập nhật kế hoạch chi tiết";
                LoadKhTongTheByKHCT();
                LoadChiTietHopDongByKHCT();
                SetEnableComboboxItemKhttNhiemVuChi();
            }
            else
            {
                Description = "Thêm mới kế hoạch chi tiết";
                ItemsChiTietHopDong = new ObservableCollection<NhKhChiTietHopDongModel>();
            }

            OnPropertyChanged(nameof(Description));
            GetTongGiaTriTienTe();
        }

        private void LoadKhTongTheByKHCT()
        {
            if (Model.IIdKHTongTheId == null) return;
            NhKhTongThe khTongThe = _nhKhTongTheService.Find(Model.IIdKHTongTheId.Value);
            if (khTongThe == null) return;
            if (khTongThe.IIdParentId == null) SelectedKhTongTheGiaiDoan = DataKhTongTheGiaiDoan.FirstOrDefault(n => n.ValueItem.Equals(khTongThe.Id.ToString()));
            else
            {
                SelectedKhTongTheGiaiDoan = DataKhTongTheGiaiDoan.FirstOrDefault(n => n.ValueItem.Equals(khTongThe.IIdParentId.ToString()));
                LoadKhTongTheNamByParentId(khTongThe.IIdParentId.Value);
                SelectedKhTongTheNam = DataKhTongTheNam.FirstOrDefault(n => n.ValueItem.Equals(Model.IIdKHTongTheId.ToString()));
            }
            LoadDonViByKhTongThe(khTongThe.Id);
            SelectedDonVi = DataDonVi.FirstOrDefault(n => n.ValueItem.Equals(Model.IIdDonViThuHuongId.ToString()));
        }

        private void LoadChiTietHopDongByKHCT()
        {
            List<NhKhChiTietHopDong> listChiTietHopDong = _nhKhChiTietHopDongService.FindChiTietHopDongByKHCT(Model.Id).ToList();
            ItemsChiTietHopDong = _mapper.Map<ObservableCollection<NhKhChiTietHopDongModel>>(listChiTietHopDong);
            int firstIndex = 1;
            foreach (var model in ItemsChiTietHopDong)
            {
                model.IRowIndex = firstIndex++;
                model.PropertyChanged += ChiTietHopDong_PropertyChanged;
            }
        }

        public override void OnSave()
        {
            if (!ValidateData()) return;
            NhKhChiTiet entity = _mapper.Map<NhKhChiTiet>(Model);
            if (Model.Id == Guid.Empty)
            {
                // insert
                entity.Id = Guid.NewGuid();
                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                entity.ILanDieuChinh = 0;
                entity.BIsActive = true;
                entity.BIsKhoa = false;
                entity.BIsGoc = true;
                Model.Id = entity.Id;
                _nhKhChiTietService.Add(entity);
                SaveChiTietHopDong();
            }
            else if (!IsDieuChinh)
            {
                // update
                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionService.Current.Principal;
                _nhKhChiTietService.Update(entity);
                SaveChiTietHopDong();
                SaveTienTeKHCT(entity);
            }
            else
            {
                // adjust
                entity = new NhKhChiTiet();
                _mapper.Map(Model, entity);

                entity.Id = Guid.NewGuid();
                entity.IIdParentAdjustId = Model.Id;
                entity.IIdGocId = entity.IIdGocId != null ? entity.IIdGocId : Model.Id;
                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                entity.BIsActive = true;
                entity.BIsKhoa = false;
                entity.BIsGoc = false;
                entity.ILanDieuChinh += 1;
                _nhKhChiTietService.Adjust(entity);
                SaveChiTietHopDongAdjust(entity.Id);
            }

            SaveAttachment(entity.Id);
            LoadData();
            DialogHost.CloseDialogCommand.Execute(null, null);
            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone);
            SavedAction?.Invoke(_mapper.Map<NhKhChiTietModel>(entity));
        }

        private void SaveChiTietHopDongAdjust(Guid idKHCTDieuChinh)
        {
            var listAdd = ItemsChiTietHopDong.Where(x => !x.IsDeleted && x.IIdNhHopDongId != null).ToList();
            if (listAdd != null && listAdd.Count > 0)
            {
                foreach (var item in listAdd)
                {
                    item.Id = Guid.NewGuid();
                    item.IIdKhChiTietId = idKHCTDieuChinh;
                    if (item.IIdNhHopDongId == null)
                    {
                        NhHopDong hopDong = new NhHopDong();
                        hopDong.Id = Guid.NewGuid();
                        hopDong.STenHopDong = string.IsNullOrEmpty(item.TenHopDong) ? null : item.TenHopDong;
                        hopDong.SSoHopDong = string.IsNullOrEmpty(item.SoHopDongText) ? null : item.SoHopDongText;
                        hopDong.IIdTiGiaUsdVndId = item.IIdTiGiaUsdVndId;
                        hopDong.IIdTiGiaUsdNgoaiTeKhacID = item.IIdTiGiaUsdNgoaiTeKhacId;
                        hopDong.FGiaTriNgoaiTeKhac = item.FGiaTriNgoaiTeKhac;
                        hopDong.FGiaTriUSD = item.FGiaTriUSD;
                        hopDong.FGiaTriVND = item.FGiaTriVND;
                        hopDong.BHieuLuc = false;
                        _nhHopDongService.Add(hopDong);
                        item.IIdNhHopDongId = hopDong.Id;
                    }
                    if (item.IIdNhHopDongId != null)
                    {
                        NhHopDong hopDongUpdate = _nhHopDongService.FindById(item.IIdNhHopDongId.Value);
                        if (hopDongUpdate != null)
                        {
                            hopDongUpdate.IIdTiGiaUsdVndId = item.IIdTiGiaUsdVndId;
                            hopDongUpdate.IIdTiGiaUsdNgoaiTeKhacID = item.IIdTiGiaUsdNgoaiTeKhacId;
                            hopDongUpdate.FGiaTriNgoaiTeKhac = item.FGiaTriNgoaiTeKhac;
                            hopDongUpdate.FGiaTriUSD = item.FGiaTriUSD;
                            hopDongUpdate.FGiaTriVND = item.FGiaTriVND;
                            _nhHopDongService.Update(hopDongUpdate);
                        }
                    }
                }
                List<NhKhChiTietHopDong>  listChiTietHopDong = _mapper.Map<List<NhKhChiTietHopDong>>(listAdd);
                listChiTietHopDong.ForEach(n => n.Id = Guid.NewGuid());
                _nhKhChiTietHopDongService.AddRange(listChiTietHopDong);
            }

        }

        private void SaveTienTeKHCT(NhKhChiTiet entity)
        {
            GetTongGiaTriTienTe();
            entity.FGiaTriNgoaiTeKhac = Model.FGiaTriNgoaiTeKhac;
            entity.FGiaTriUsd = Model.FGiaTriUsd;
            entity.FGiaTriVnd = Model.FGiaTriVnd;
            _nhKhChiTietService.Update(entity);
        }

        #region HopDong

        private void SaveChiTietHopDong()
        {
            var lstInsert = ItemsChiTietHopDong.Where(x => !x.IsDeleted && x.Id.IsNullOrEmpty()).ToList();
            var lstUpdate = ItemsChiTietHopDong.Where(x => !x.IsDeleted && !x.Id.IsNullOrEmpty()).ToList();
            var lstDelete = ItemsChiTietHopDong.Where(x => x.IsDeleted && !x.Id.IsNullOrEmpty()).ToList();

            if (lstInsert != null && lstInsert.Count > 0)
            {
                AddChiTietHopDongSave(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count > 0)
            {
                UpdateChiTietHopDongSave(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count > 0)
            {
                DeleteChiTietHopDongSave(lstDelete);
            }
        }

        private void UpdateChiTietHopDongSave(List<NhKhChiTietHopDongModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                NhKhChiTietHopDong chiTietHopDong = _nhKhChiTietHopDongService.FindById(item.Id);
                if (chiTietHopDong != null)
                {
                    chiTietHopDong.IIdKhTongTheNhiemVuChiId = item.IIdKhTongTheNhiemVuChiId;
                    chiTietHopDong.IIdNhHopDongId = item.IIdNhHopDongId;
                    chiTietHopDong.IIdTiGiaUsdNgoaiTeKhacID = item.IIdTiGiaUsdNgoaiTeKhacId;
                    chiTietHopDong.IIdTiGiaUsdVndId = item.IIdTiGiaUsdVndId;
                    chiTietHopDong.FGiaTriNgoaiTeKhac = item.FGiaTriNgoaiTeKhac;
                    chiTietHopDong.FGiaTriUSD = item.FGiaTriUSD;
                    chiTietHopDong.FGiaTriVND = item.FGiaTriVND;
                    _nhKhChiTietHopDongService.Update(chiTietHopDong);

                    if (chiTietHopDong.IIdNhHopDongId == null) return;
                    NhHopDong nhHopDong = _nhHopDongService.FindById(chiTietHopDong.IIdNhHopDongId.Value);
                    nhHopDong.IIdTiGiaUsdVndId = item.IIdTiGiaUsdVndId;
                    nhHopDong.IIdTiGiaUsdNgoaiTeKhacID = item.IIdTiGiaUsdNgoaiTeKhacId;
                    _nhHopDongService.Update(nhHopDong);
                }
            }
        }

        private void DeleteChiTietHopDongSave(List<NhKhChiTietHopDongModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _nhKhChiTietHopDongService.Delete(item.Id);
            }
        }

        private void AddChiTietHopDongSave(List<NhKhChiTietHopDongModel> listAdd)
        {
            foreach (var item in listAdd)
            {
                item.IIdKhChiTietId = Model.Id;
                if (item.IIdNhHopDongId == null)
                {
                    NhHopDong hopDong = new NhHopDong();
                    hopDong.Id = Guid.NewGuid();
                    hopDong.STenHopDong = string.IsNullOrEmpty(item.TenHopDong) ? null : item.TenHopDong;
                    hopDong.SSoHopDong = string.IsNullOrEmpty(item.SoHopDongText) ? null : item.SoHopDongText;
                    hopDong.IIdTiGiaUsdVndId = item.IIdTiGiaUsdVndId;
                    hopDong.IIdTiGiaUsdNgoaiTeKhacID = item.IIdTiGiaUsdNgoaiTeKhacId;
                    hopDong.FGiaTriNgoaiTeKhac = item.FGiaTriNgoaiTeKhac;
                    hopDong.FGiaTriUSD = item.FGiaTriUSD;
                    hopDong.FGiaTriVND = item.FGiaTriVND;
                    hopDong.BHieuLuc = false;
                    _nhHopDongService.Add(hopDong);
                    item.IIdNhHopDongId = hopDong.Id;
                }
                if (item.IIdNhHopDongId != null)
                {
                    NhHopDong hopDongUpdate = _nhHopDongService.FindById(item.IIdNhHopDongId.Value);
                    if (hopDongUpdate != null)
                    {
                        hopDongUpdate.IIdTiGiaUsdVndId = item.IIdTiGiaUsdVndId;
                        hopDongUpdate.IIdTiGiaUsdNgoaiTeKhacID = item.IIdTiGiaUsdNgoaiTeKhacId;
                        hopDongUpdate.FGiaTriNgoaiTeKhac = item.FGiaTriNgoaiTeKhac;
                        hopDongUpdate.FGiaTriUSD = item.FGiaTriUSD;
                        hopDongUpdate.FGiaTriVND = item.FGiaTriVND;
                        _nhHopDongService.Update(hopDongUpdate);
                    }
                }
            }
            List<NhKhChiTietHopDong> listChiTietHopDong = _mapper.Map<List<NhKhChiTietHopDong>>(listAdd);
            listChiTietHopDong.ForEach(n => n.Id = Guid.NewGuid());
            _nhKhChiTietHopDongService.AddRange(listChiTietHopDong);
        }

        private void OnAddChiTietHopDongDetail()
        {
            if (SelectedDonVi == null) return;
            NhKhChiTietHopDongModel newItem = new NhKhChiTietHopDongModel();
            newItem.PropertyChanged += ChiTietHopDong_PropertyChanged;
            newItem.IRowIndex = ItemsChiTietHopDong.Count + 1;
            ItemsChiTietHopDong.Insert(ItemsChiTietHopDong.Count, newItem);
            OnPropertyChanged(nameof(ItemsChiTietHopDong));
            GetTongGiaTriTienTe();
        }

        private void OnDeleteChiTietHopDongDetail()
        {
            if (SelectedChiTietHopDong != null)
            {
                SelectedChiTietHopDong.IsDeleted = !SelectedChiTietHopDong.IsDeleted;
            }
            OnPropertyChanged(nameof(ItemsChiTietHopDong));
        }

        #endregion
        #endregion

        #region Helper
        private bool ValidateData()
        {
            List<string> lstError = new List<string>();
            if (_selectedDonVi == null)
            {
                lstError.Add(Resources.MsgCheckDonVi);
            }
            if (string.IsNullOrEmpty(Model.SSoKeHoach))
            {
                lstError.Add(Resources.MsgCheckSoKeHoach);
            }
            if (!Model.DNgayKeHoach.HasValue)
            {
                lstError.Add(Resources.MsgCheckNgayKeHoach);
            }
            if (!ItemsChiTietHopDong.Any(n => !n.IsDeleted))
            {
                lstError.Add(string.Format(Resources.MsgCheckThemHopDong));
            }
            if (ItemsChiTietHopDong.Where(n => !n.IsDeleted).Any(n => n.IIdKhTongTheNhiemVuChiId.IsNullOrEmpty()))
            {
                lstError.Add(string.Format(Resources.MsgCheckThieuNhiemVuChi));
            }
            if (lstError.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstError),
                    Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        #region Master

        private void LoadKhTongTheGiaiDoan()
        {
            List<NhKhTongThe> lstKhTongThe = _nhKhTongTheService.FindAll().ToList();
            if (lstKhTongThe == null) return;
            var drpItem = lstKhTongThe.Where(n => n.IIdParentId == null).Select(n => new ComboboxItem() { ValueItem = n.Id.ToString(), HiddenValue = n.Id.ToString(), DisplayItem = (n.SSoKeHoachBqp) });
            _dataKhTongTheGiaiDoan = _mapper.Map<ObservableCollection<ComboboxItem>>(drpItem);
            OnPropertyChanged(nameof(DataKhTongTheGiaiDoan));
        }

        private void LoadSoHopDong()
        {
            List<NhHopDongQuery> lstHopDongQuery = _nhHopDongService.FindAllWithTiGia().ToList();
            if (lstHopDongQuery == null) return;
            var drpItem = lstHopDongQuery.Select(n => new ComboboxItem()
            {
                ValueItem = n.Id.ToString(),
                HiddenValue = SetValueTiGiaCombobox(n),
                DisplayItem = n.SSoHopDong,
                DisplayItemOption2 = n.STenHopDong,
                Type = n.FTiGia1 == null ? null : n.FTiGia2 == null ? n.FTiGia1 + "" : n.FTiGia1 + "-" + n.FTiGia2,
                IsEnabled = true,
            });
            _dataHopDong = _mapper.Map<ObservableCollection<ComboboxItem>>(drpItem);
            OnPropertyChanged(nameof(DataHopDong));
        }

        private string SetValueTiGiaCombobox(NhHopDongQuery hopdong)
        {
            string displayText;
            if (hopdong.IIdTiGiaUsdVndId == null) displayText = "";
            else
            {
                if (hopdong.IIdTiGiaUsdNgoaiTeKhacID == null) displayText = "1 USD = " + hopdong.FTiGia1 + " VND. ";
                else
                {
                    displayText = "1 " + hopdong.SMaTienTe1NgoaiTeKhacUsd + " = " + hopdong.FTiGia2 + " " + hopdong.SMaTienTe2NgoaiTeKhacUsd + ". 1 USD = " + hopdong.FTiGia1 + " VND. ";
                }
            }
            return displayText;
        }
        private void LoadKhTongTheNamByParentId(Guid idParent)
        {
            List<NhKhTongThe> lstKhTongThe = _nhKhTongTheService.FindAll();
            if (lstKhTongThe == null) return;
            var drpItem = lstKhTongThe.Where(n => n.IIdParentId == idParent).Select(n => new ComboboxItem() { ValueItem = n.Id.ToString(), HiddenValue = n.Id.ToString(), DisplayItem = (n.SSoKeHoachBqp) });
            _dataKhTongTheNam = _mapper.Map<ObservableCollection<ComboboxItem>>(drpItem);
            OnPropertyChanged(nameof(DataKhTongTheNam));
        }

        private void LoadDonViByKhTongThe(Guid idKeHoachTongThe)
        {
            var lstNhKhTongTheNhiemVuChi = _nhKhTongTheNhiemVuChiService.FindAllByKhTongTheId(idKeHoachTongThe).ToList();
            if (lstNhKhTongTheNhiemVuChi == null) return;

            List<DonVi> lstDonVi = new List<DonVi>();
            lstNhKhTongTheNhiemVuChi.ForEach(e =>
            {
                if (!lstDonVi.Any(x => x.Id == e.IIdDonViThuHuongId))
                {
                    var donvi = _nsDonViService.FindById(e.IIdDonViThuHuongId);
                    if (donvi != null) lstDonVi.Add(donvi);
                }
            });

            if (lstDonVi == null) return;
            var drpItem = lstDonVi.OrderBy(x => x.IIDMaDonVi).Select(n => new ComboboxItem() { ValueItem = n.Id.ToString(), HiddenValue = n.Id.ToString(), DisplayItem = (n.IIDMaDonVi + " - " + n.TenDonVi) });
            _dataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(drpItem);
            OnPropertyChanged(nameof(DataDonVi));
        }

        private void LoadKhTongThe_NhiemVuChi(Guid idKeHoachTongThe, Guid idDonVi)
        {
            List<NhKhTongTheNhiemVuChiQuery> lstKhTongThe_NhiemVuChi = _nhKhTongTheNhiemVuChiService.FindByIdKhTongTheAndIdDonVi(idKeHoachTongThe, idDonVi).ToList();
            var drpData = lstKhTongThe_NhiemVuChi.Select(n => new ComboboxItem() { ValueItem = n.Id.ToString(), DisplayItem = (n.STenNhiemVuChi), IsEnabled = true, });
            _cbxKhTongThe_NhiemVuChi = _mapper.Map<ObservableCollection<ComboboxItem>>(drpData);
            OnPropertyChanged(nameof(CbxKhTongThe_NhiemVuChi));
        }

        private void GetTongGiaTriTienTe()
        {
            Model.FGiaTriNgoaiTeKhac = ItemsChiTietHopDong.Where(x => !x.IsDeleted).Sum(n => (n.FGiaTriNgoaiTeKhac));
            Model.FGiaTriUsd = ItemsChiTietHopDong.Where(x => !x.IsDeleted).Sum(n => n.FGiaTriUSD);
            Model.FGiaTriVnd = ItemsChiTietHopDong.Where(x => !x.IsDeleted).Sum(n => n.FGiaTriVND);
        }

        private void ChiTietHopDong_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            NhKhChiTietHopDongModel objectSender = (NhKhChiTietHopDongModel)sender;
            if (args.PropertyName == nameof(objectSender.SelectedSoHopDong))
            {
                if (Model.Id.IsNullOrEmpty())
                {
                    objectSender.FGiaTriNgoaiTeKhac = 0;
                    objectSender.FGiaTriUSD = 0;
                    objectSender.FGiaTriVND = 0;
                }
                if (objectSender.SelectedSoHopDong != null)
                {
                    objectSender.TiGia = objectSender.SelectedSoHopDong.HiddenValue;
                    objectSender.lstTiGia = objectSender.SelectedSoHopDong.Type != null ? objectSender.SelectedSoHopDong.Type.Split("-") : new string[0];
                    objectSender.TenHopDong = objectSender.SelectedSoHopDong.DisplayItemOption2;
                    objectSender.SoHopDongText = objectSender.SelectedSoHopDong.DisplayItem;
                    // binding idTiGIa to save data when selectedHopDong. 
                    if (objectSender.SelectedSoHopDong.ValueItem == null) return;
                    NhHopDong hopDong = _nhHopDongService.FindById(new Guid(objectSender.SelectedSoHopDong.ValueItem));
                    if (hopDong != null)
                    {
                        objectSender.IIdTiGiaUsdVndId = hopDong.IIdTiGiaUsdVndId;
                        objectSender.IIdTiGiaUsdNgoaiTeKhacId = hopDong.IIdTiGiaUsdNgoaiTeKhacID;
                    }
                }
                else
                {
                    objectSender.lstTiGia = new string[0];
                }
            }
            if (args.PropertyName == nameof(objectSender.FGiaTriNgoaiTeKhac))
            {
                if (objectSender.lstTiGia != null)
                {
                    if (objectSender.lstTiGia.Length == 2)
                    {
                        objectSender.FGiaTriUSD = objectSender.FGiaTriNgoaiTeKhac * Convert.ToDouble(objectSender.lstTiGia[1]);
                        objectSender.FGiaTriVND = objectSender.FGiaTriUSD * Convert.ToDouble(objectSender.lstTiGia[0]);
                    }
                    else objectSender.FGiaTriNgoaiTeKhac = 0;
                }
            }
            if (args.PropertyName == nameof(objectSender.FGiaTriUSD))
            {
                if (objectSender.lstTiGia != null)
                {
                    if (objectSender.lstTiGia.Length == 1)
                    {
                        objectSender.FGiaTriNgoaiTeKhac = 0;
                        objectSender.FGiaTriVND = objectSender.FGiaTriUSD * Convert.ToDouble(objectSender.lstTiGia[0]);
                    }
                    else if (objectSender.lstTiGia.Length == 2)
                    {
                        objectSender.FGiaTriNgoaiTeKhac = objectSender.FGiaTriUSD / Convert.ToDouble(objectSender.lstTiGia[1]);
                        objectSender.FGiaTriVND = objectSender.FGiaTriUSD * Convert.ToDouble(objectSender.lstTiGia[0]);
                    }
                    else objectSender.FGiaTriUSD = 0;
                }
            }
            if (args.PropertyName == nameof(objectSender.FGiaTriVND))
            {
                if (objectSender.lstTiGia != null)
                {
                    if (objectSender.lstTiGia.Length == 1)
                    {
                        objectSender.FGiaTriNgoaiTeKhac = 0;
                        objectSender.FGiaTriUSD = objectSender.FGiaTriVND / Convert.ToDouble(objectSender.lstTiGia[0]);
                    }
                    else if (objectSender.lstTiGia.Length == 2)
                    {
                        objectSender.FGiaTriUSD = objectSender.FGiaTriVND / Convert.ToDouble(objectSender.lstTiGia[0]);
                        objectSender.FGiaTriNgoaiTeKhac = objectSender.FGiaTriUSD / Convert.ToDouble(objectSender.lstTiGia[1]);
                    }
                    else objectSender.FGiaTriVND = 0;
                }
            }
            if (args.PropertyName == nameof(objectSender.SoHopDongText))
            {
                if (objectSender.SelectedSoHopDong == null && objectSender.SoHopDongText != null)
                {
                    objectSender.SelectedSoHopDong = _dataHopDong.FirstOrDefault(n => n.DisplayItem == objectSender.SoHopDongText);
                }
            }
            if (args.PropertyName == nameof(objectSender.IIdKhTongTheNhiemVuChiId))
            {
                SetEnableComboboxItemKhttNhiemVuChi();
            }
            GetTongGiaTriTienTe();
        }

        public override void OnClose(object obj)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void SetEnableComboboxItemKhttNhiemVuChi()
        {
            if (CbxKhTongThe_NhiemVuChi == null || ItemsChiTietHopDong == null) return;
            List<Guid> lstIdKhTongTheNhiemVuChiSelected = ItemsChiTietHopDong
                     .Where(n => (n.IIdKhTongTheNhiemVuChiId != null && !n.IIdKhTongTheNhiemVuChiId.IsNullOrEmpty()))
                     .Select(n => n.IIdKhTongTheNhiemVuChiId)
                     .Distinct().ToList();
            _cbxKhTongThe_NhiemVuChi.Where(n => lstIdKhTongTheNhiemVuChiSelected.Contains(Guid.Parse(n.ValueItem))).All(n => { n.IsEnabled = false; return true; });
            _cbxKhTongThe_NhiemVuChi.Where(n => !lstIdKhTongTheNhiemVuChiSelected.Contains(Guid.Parse(n.ValueItem))).All(n => { n.IsEnabled = true; return true; });
            OnPropertyChanged(nameof(CbxKhTongThe_NhiemVuChi));
        }

        #endregion
        #endregion
    }
}
