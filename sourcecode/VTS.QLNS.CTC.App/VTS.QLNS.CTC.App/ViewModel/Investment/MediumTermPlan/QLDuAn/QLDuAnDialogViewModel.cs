using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
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

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.QLDuAn
{
    public class QLDuAnDialogViewModel : DialogAttachmentViewModelBase<ProjectManagerModel>
    {
        #region Private
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IVdtDaChuTruongDauTuService _vdtDaChuTruongDauTuService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly IVdtDaDuAnService _duAnService;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private static int indexSMaDuAn;
        private int currentRow = -1;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        #endregion

        public override string Name => "QUẢN LÝ THÔNG TIN DỰ ÁN";
        public override string Title => "QUẢN LÝ THÔNG TIN DỰ ÁN";
        public override Type ContentType => typeof(View.Investment.MediumTermPlan.QLDuAn.QLDuAnDialog);
        public bool IsInsert => Model.Id == Guid.Empty ? true : false;
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_THONGTIN_DUAN;

        #region Items
        private double _fHanMucDauTu;
        public double FHanMucDauTu
        {
            get => _fHanMucDauTu;
            set => SetProperty(ref _fHanMucDauTu, value);
        }

        private string _sLoaiChiPhi;
        public string SLoaiChiPhi
        {
            get => _sLoaiChiPhi;
            set => SetProperty(ref _sLoaiChiPhi, value);
        }

        private ObservableCollection<VdtDaHangMucModel> _hangMucItems;
        public ObservableCollection<VdtDaHangMucModel> HangMucItems
        {
            get => _hangMucItems;
            set => SetProperty(ref _hangMucItems, value);
        }

        private VdtDaHangMucModel _selectedHangMuc;
        public VdtDaHangMucModel SelectedHangMuc
        {
            get => _selectedHangMuc;
            set => SetProperty(ref _selectedHangMuc, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaNguonVonModel> _duAnNguonVonItems;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaNguonVonModel> DuAnNguonVonItems
        {
            get => _duAnNguonVonItems;
            set => SetProperty(ref _duAnNguonVonItems, value);
        }

        private VdtDaNguonVonModel _dataNguonVonSelected;
        public VdtDaNguonVonModel DataNguonVonSelected
        {
            get => _dataNguonVonSelected;
            set => SetProperty(ref _dataNguonVonSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxNguonVon;
        public ObservableCollection<ComboboxItem> CbxNguonVon
        {
            get => _cbxNguonVon;
            set => SetProperty(ref _cbxNguonVon, value);
        }

        private ObservableCollection<ComboboxItem> _dataDonVi;
        public ObservableCollection<ComboboxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private ObservableCollection<ComboboxItem> _dataChuDauTu;
        public ObservableCollection<ComboboxItem> DataChuDauTu
        {
            get => _dataChuDauTu;
            set => SetProperty(ref _dataChuDauTu, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private ComboboxItem _selectedChuDauTu;
        public ComboboxItem SelectedChuDauTu
        {
            get => _selectedChuDauTu;
            set
            {
                SetProperty(ref _selectedChuDauTu, value);
            }
        }

        private ObservableCollection<ComboboxItem> _dataPhanCapPheDuyet;
        public ObservableCollection<ComboboxItem> DataPhanCapPheDuyet
        {
            get => _dataPhanCapPheDuyet;
            set => SetProperty(ref _dataPhanCapPheDuyet, value);
        }

        private ComboboxItem _selectedPhanCapPheDuyet;
        public ComboboxItem SelectedPhanCapPheDuyet
        {
            get => _selectedPhanCapPheDuyet;
            set
            {
                SetProperty(ref _selectedPhanCapPheDuyet, value);
            }
        }

        private ObservableCollection<ComboboxItem> _dataNhomDuAn;
        public ObservableCollection<ComboboxItem> DataNhomDuAn
        {
            get => _dataNhomDuAn;
            set => SetProperty(ref _dataNhomDuAn, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> DataLoaiCongTrinh
        {
            get => _dataLoaiCongTrinh;
            set => SetProperty(ref _dataLoaiCongTrinh, value);
        }

        private ComboboxItem _selectedNhomDuAn;
        public ComboboxItem SelectedNhomDuAn
        {
            get => _selectedNhomDuAn;
            set
            {
                SetProperty(ref _selectedNhomDuAn, value);
            }
        }

        private ObservableCollection<ComboboxItem> _dataHinhThucQL;
        public ObservableCollection<ComboboxItem> DataHinhThucQL
        {
            get => _dataHinhThucQL;
            set => SetProperty(ref _dataHinhThucQL, value);
        }

        private ComboboxItem _selectedHinhThucQL;
        public ComboboxItem SelectedHinhThucQL
        {
            get => _selectedHinhThucQL;
            set
            {
                SetProperty(ref _selectedHinhThucQL, value);
            }
        }

        private double _fTongHangMuc;
        public double FTongHangMuc
        {
            get => _fTongHangMuc;
            set => SetProperty(ref _fTongHangMuc, value);
        }
        #endregion

        #region RelayCommand
        public RelayCommand AddNguonVonDetailCommand { get; }
        public RelayCommand DeleteDetailCommand { get; }
        public RelayCommand AddHangMucDetailCommand { get; }
        public RelayCommand AddChildHangMucCommand { get; }
        public RelayCommand DeleteDetailHangMucCommand { get; }
        #endregion

        public QLDuAnDialogViewModel(ISessionService sessionService,
            IMapper mapper,
            INsDonViService nsDonViService,
            IProjectManagerService projectManagerService,
            IApproveProjectService approveProjectService,
            IVdtDaChuTruongDauTuService vdtDaChuTruongDauTuService,
            INsNguonNganSachService nsNguonVonService,
            IDmChuDauTuService chuDauTuService,
            IVdtDaDuAnService duAnService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHienDuAnService)
            : base(mapper, storageServiceFactory, attachService)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _projectManagerService = projectManagerService;
            _approveProjectService = approveProjectService;
            _vdtDaChuTruongDauTuService = vdtDaChuTruongDauTuService;
            _nsNguonVonService = nsNguonVonService;
            _dmChuDauTuService = chuDauTuService;
            _duAnService = duAnService;
            _vdtDmDonViThucHienDuAnService = vdtDmDonViThucHienDuAnService;

            AddNguonVonDetailCommand = new RelayCommand(obj => OnAddNguonVonDetail());
            DeleteDetailCommand = new RelayCommand(obj => OnDeleteDetail());
            AddHangMucDetailCommand = new RelayCommand(obj => OnAddHangMucDetail());
            AddChildHangMucCommand = new RelayCommand(obj => OnAddChildHangMuc());
            DeleteDetailHangMucCommand = new RelayCommand(obj => OnDeleteHangMuc());
        }

        #region Event
        public override void Init()
        {
            LoadAttach();
            LoadNguonVon();
            LoadLoaiCongTrinh();
            LoadDonVi();
            LoadChuDauTu();
            LoadPhanCapPheDuyet();
            LoadNhomDuAn();
            LoadHinhThucQL();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            FHanMucDauTu = 0;
            if (!Model.Id.IsNullOrEmpty())
            {
                Description = "Cập nhật thông tin dự án";
                OnSetSelectedDataModel();
                LoadDataNguonVonByDuAn();
                LoadDataHangMucByDuAn();
            }
            else
            {
                Description = "Thêm mới thông tin dự án";
                DuAnNguonVonItems = new ObservableCollection<VdtDaNguonVonModel>();
                HangMucItems = new ObservableCollection<VdtDaHangMucModel>();
            }
            if (Model != null && DuAnNguonVonItems != null)
            {
                FHanMucDauTu = DuAnNguonVonItems.Where(n => !n.IsDeleted).Sum(n => (n.FThanhTien ?? 0));
            }
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(SLoaiChiPhi));
            GetTongHangMuc();
            OnPropertyChanged(nameof(FHanMucDauTu));
        }

        private void LoadDataNguonVonByDuAn()
        {
            List<VDTDaNguonVonQuery> listNguonVon = _projectManagerService.FindListNguonVonByDuan(Model.Id).ToList();
            DuAnNguonVonItems = _mapper.Map<ObservableCollection<VdtDaNguonVonModel>>(listNguonVon);

            foreach (var model in DuAnNguonVonItems)
            {
                model.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            }
        }

        private void LoadDataHangMucByDuAn()
        {
            int count = 1;
            List<VdtDaHangMucQuery> listHangMuc = new List<VdtDaHangMucQuery>();
            //listHangMuc = _vdtDaChuTruongDauTuService.FindListDAHangMucDetail(Model.Id).ToList();
            listHangMuc = _projectManagerService.FindListHangMucByDuan(Model.Id).ToList();
            foreach (var model in listHangMuc)
            {
                model.Stt = count;
                count++;
            }
            HangMucItems = _mapper.Map<ObservableCollection<VdtDaHangMucModel>>(listHangMuc);

            if (HangMucItems.Count > 0)
            {
                foreach (VdtDaHangMucModel model in HangMucItems)
                {
                    model.BIsAdd = false;
                    model.PropertyChanged += HangMuc_PropertyChanged;
                }
            }
        }

        public override void OnSave(object obj)
        {
            if (!ValidateData()) return;
            try
            {
                VdtDaDuAn entity = ConvertDataModel();
                if (Model.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                    entity.DDateCreate = DateTime.Now;
                    entity.SUserCreate = _sessionService.Current.Principal;
                    entity.SMaDuAn = UpdateMaDuAn();
                    entity.FHanMucDauTu = DuAnNguonVonItems.Where(n => !n.IsDeleted).Sum(n => n.FThanhTien ?? 0);

                    _projectManagerService.InsertAutoCode(entity);
                    Model.Id = entity.Id;
                    Model.SMaDuAn = entity.SMaDuAn;
                }
                else
                {
                    entity.DDateUpdate = DateTime.Now;
                    entity.SUserUpdate = _sessionService.Current.Principal;
                    entity.FHanMucDauTu = DuAnNguonVonItems.Where(n => !n.IsDeleted).Sum(n => n.FThanhTien ?? 0);
                    _projectManagerService.UpdateDataDuAn(entity);
                }
                SaveDataDetail();
                Model.IIdMaDonViThucHienDuAn = entity.IIdMaDonViQuanLy;
                Model.IIdCapPheDuyetId = entity.IIdCapPheDuyetId;
                Model.IIdChuDauTuId = entity.IIdChuDauTuId;
                Model.IIdNhomDuAnId = entity.IIdNhomDuAnId;
                // Save attach file
                SaveAttachment(entity.Id);
                LoadData();

                System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone);
                SavedAction?.Invoke(_mapper.Map<ProjectManagerModel>(entity));
                ((Window)obj).Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgSaveError, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        #region NguonVon
        private void DetailNguonVonModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var selected = (VdtDaNguonVonModel)sender;
            if (args.PropertyName == nameof(VdtDaNguonVonModel.IIdNguonVonId))
            {
                if (selected.IIdNguonVonId.HasValue && selected.IIdNguonVonId != 0)
                {
                    if (CheckDuplicateNguonVon(selected.IIdNguonVonId ?? 0))
                    {
                        MessageBoxHelper.Error(Resources.MsgCheckTrungNguonVonDauTu);
                        selected.IIdNguonVonId = 0;
                        return;
                    }
                }
            }
            FHanMucDauTu = DuAnNguonVonItems.Where(n => !n.IsDeleted).Sum(n => (n.FThanhTien ?? 0));
            OnPropertyChanged(nameof(FHanMucDauTu));
        }

        private bool CheckDuplicateNguonVon(int idNguonVon)
        {
            List<VdtDaNguonVonModel> listNguonVon = DuAnNguonVonItems.Where(x => x.IIdNguonVonId == idNguonVon && !x.IsDeleted).ToList();
            if (listNguonVon != null && listNguonVon.Count > 1)
            {
                return true;
            }
            return false;
        }

        private void SaveNguonVon()
        {
            var lstInsert = DuAnNguonVonItems.Where(x => !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null) && x.IIdNguonVonId != null).ToList();
            var lstUpdate = DuAnNguonVonItems.Where(x => !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            var lstDelete = DuAnNguonVonItems.Where(x => x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();

            if (lstInsert != null && lstInsert.Count > 0)
            {
                AddNguonVonSave(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count > 0)
            {
                UpdateNguonVonSave(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count > 0)
            {
                DeleteDuAnNguonVonSave(lstDelete);
            }
        }

        private void DeleteDuAnNguonVonSave(List<VdtDaNguonVonModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _projectManagerService.DeleteDuAnNguonVon(item.Id);
            }
        }

        private void AddNguonVonSave(List<VdtDaNguonVonModel> listAdd)
        {
            if (listAdd != null && listAdd.Count > 0)
            {
                foreach (var item in listAdd)
                {
                    item.IIdDuAn = Model.Id;
                }
                List<VdtDaNguonVon> listNguonVon = new List<VdtDaNguonVon>();
                listNguonVon = _mapper.Map<List<VdtDaNguonVon>>(listAdd);
                listNguonVon.Select(n => { n.Id = Guid.NewGuid(); return n; }).ToList();
                _projectManagerService.AddRangeDuAnNguonVon(listNguonVon);
            }
        }

        private void UpdateNguonVonSave(List<VdtDaNguonVonModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                VdtDaNguonVon duAnNV = _projectManagerService.FindDuAnNguonVonById(item.Id);
                if (duAnNV != null)
                {
                    duAnNV.FThanhTien = item.FThanhTien;
                    duAnNV.IIdNguonVonId = item.IIdNguonVonId;
                    _projectManagerService.UpdateDuAnNguonVon(duAnNV);
                }
            }
        }

        private void OnAddNguonVonDetail()
        {
            VdtDaNguonVonModel newItem = new VdtDaNguonVonModel();
            //newItem.Id = Guid.NewGuid();
            newItem.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            DuAnNguonVonItems.Insert(DuAnNguonVonItems.Count, newItem);
            OnPropertyChanged(nameof(DuAnNguonVonItems));
        }

        private void OnDeleteDetail()
        {
            if (DataNguonVonSelected != null)
            {
                DataNguonVonSelected.IsDeleted = !DataNguonVonSelected.IsDeleted;
            }
            FHanMucDauTu = DuAnNguonVonItems.Where(n => !n.IsDeleted).Sum(n => n.FThanhTien ?? 0);
            OnPropertyChanged(nameof(DuAnNguonVonItems));
            OnPropertyChanged(nameof(FHanMucDauTu));
        }
        #endregion



        #region HangMuc
        private void OnAddHangMucDetail()
        {
            int stt = 1;
            VdtDaHangMucModel targetItem = new VdtDaHangMucModel()
            {
                IdDuAnHangMuc = Guid.NewGuid(),
                //SMaHangMuc = GetMaHangMuc(),
                MaOrDer = GetSTTHangMuc(false),

                IsHangCha = true,
                BIsAdd = true
                //indexMaHangMuc = _indexHangMucMax
            };

            stt = GetSoThuTu(targetItem);

            if (HangMucItems != null && HangMucItems.Count > 0)
            {
                if (SelectedHangMuc != null)
                {

                    VdtDaHangMucModel sourceItem = SelectedHangMuc;
                    targetItem = ObjectCopier.Clone(sourceItem);
                    if (!sourceItem.IsHangCha)
                    {
                        targetItem.IsHangCha = false;
                        targetItem.IIdParentId = sourceItem.IIdParentId;
                    }
                    else
                    {
                        targetItem.IIdParentId = null;
                    }
                    targetItem.Id = Guid.Empty;
                    targetItem.HanMucDT = 0;
                    targetItem.IdDuAnHangMuc = Guid.NewGuid();
                    targetItem.STenHangMuc = string.Empty;
                    targetItem.BIsAdd = true;
                    targetItem.Stt = GetSoThuTu(targetItem);
                    //targetItem.SMaHangMuc = GetMaHangMuc();
                    //targetItem.indexMaHangMuc = _indexHangMucMax;
                    targetItem.MaOrDer = GetSTTHangMuc(false);
                }
            }
            targetItem.PropertyChanged += HangMuc_PropertyChanged;
            HangMucItems.Insert(currentRow + 1, targetItem);
            //_indexHangMucMax ++ ;
            OnPropertyChanged(nameof(HangMucItems));
        }

        private int GetSoThuTu(VdtDaHangMucModel targetItem)
        {
            int stt = 1;
            if (HangMucItems.Count >= 1)
            {
                var Count = HangMucItems.Where(x => x.IIdParentId == null).Select(x => x.Stt).ToList().Count();

                var hangMucItemLastStt = HangMucItems.Where(x => x.IIdParentId == null).Select(x => x.Stt).ToList().Last();

                if (Count == hangMucItemLastStt)
                {
                    stt = hangMucItemLastStt + 1;
                    targetItem.Stt = hangMucItemLastStt + 1;
                }
                else
                {
                    stt = Count + 1;
                    targetItem.Stt = Count + 1;
                }

            }
            else
            {
                targetItem.Stt = 1;
            }

            return stt;
        }


        private void OnAddChildHangMuc()
        {
            if (HangMucItems == null || HangMucItems.Count == 0 || SelectedHangMuc == null)
            {
                return;
            }
            string maSTT = GetSTTHangMuc(true);

            VdtDaHangMucModel sourceItem = SelectedHangMuc;
            VdtDaHangMucModel targetItem = ObjectCopier.Clone(sourceItem);
            targetItem.Id = Guid.Empty;
            targetItem.IdDuAnHangMuc = Guid.NewGuid();
            targetItem.IIdParentId = sourceItem.IdDuAnHangMuc;
            targetItem.HanMucDT = 0;
            sourceItem.IsHangCha = true;
            targetItem.IsHangCha = false;
            //targetItem.SMaHangMuc = GetMaHangMuc();
            targetItem.STenHangMuc = string.Empty;
            targetItem.BIsAdd = true;
            //targetItem.indexMaHangMuc = _indexHangMucMax;
            targetItem.MaOrDer = maSTT;

            targetItem.Stt = GetSoThuTu(targetItem);

            targetItem.PropertyChanged += HangMuc_PropertyChanged;
            HangMucItems.Insert(currentRow + 1, targetItem);

            OnPropertyChanged(nameof(HangMucItems));
        }

        protected void OnDeleteHangMuc()
        {
            if (HangMucItems != null && HangMucItems.Count > 0 && SelectedHangMuc != null)
            {
                var listDelete = FindListHangMucChild(SelectedHangMuc.Id);
                listDelete.Add(SelectedHangMuc);
                var listHangMucDeleted = HangMucItems.Where(x => x.IsDeleted == true).ToList();
                if (listHangMucDeleted.Count >= HangMucItems.Count - 1)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgSaveErrorCategory, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    foreach (var item in listDelete)
                    {
                        item.IsDeleted = !SelectedHangMuc.IsDeleted;
                    }
                    CalculateDataChiPhiHangMuc();
                }
            }
        }

        public List<VdtDaHangMucModel> FindListHangMucChild(Guid parentId)
        {
            List<VdtDaHangMucModel> inner = new List<VdtDaHangMucModel>();
            foreach (var t in HangMucItems.Where(item => item.IIdParentId == parentId))
            {
                inner.Add(t);
                inner = inner.Union(FindListHangMucChild(t.Id)).ToList();
            }

            return inner;
        }

        #endregion
        #endregion

        #region Helper
        private bool ValidateData()
        {
            List<string> lstError = new List<string>();
            int iKhoiCong = 0, iKetThuc = 0;
            if (SelectedDonVi == null)
            {
                lstError.Add(Resources.MsgCheckDonVi);
            }
            if (string.IsNullOrEmpty(Model.STenDuAn))
            {
                lstError.Add(Resources.MsgCheckTenDuAn);
            }
            if (!string.IsNullOrEmpty(Model.SKhoiCong) && !int.TryParse(Model.SKhoiCong, out iKhoiCong))
            {
                lstError.Add(string.Format(Resources.MsgErrorFormat, "Thời gian thực hiện từ"));
            }
            if (!string.IsNullOrEmpty(Model.SKetThuc) && !int.TryParse(Model.SKetThuc, out iKetThuc))
            {
                lstError.Add(string.Format(Resources.MsgErrorFormat, "Thời gian thực hiện đến"));
            }
            if (iKhoiCong != 0 && iKetThuc != 0 && iKhoiCong > iKetThuc)
            {
                lstError.Add(string.Format(Resources.MsgErrorValueBigger, "Thời gian thực hiện từ", "Thời gian thực hiện đến"));
            }

            var CheckValidateLoaiCongTrinh = HangMucItems.Where(n => n.LoaiCongTrinhId.HasValue);
            if (!CheckValidateLoaiCongTrinh.Any() || HangMucItems.Any(x => x.LoaiCongTrinhId == null))
            {
                lstError.Add(Resources.MsgSavePropertyChange);
            }

            var CheckValidateNguonVon = DuAnNguonVonItems.Where(n => n.IIdNguonVonId.HasValue);
            if (!CheckValidateNguonVon.Any())
            {
                lstError.Add(Resources.MesCheckValidateNguonVon);
            }

            var CheckValidateGiaTriPheDuyet = DuAnNguonVonItems.Where(n => n.FThanhTien.HasValue);
            if (!CheckValidateGiaTriPheDuyet.Any())
            {
                lstError.Add(Resources.MesCheckValidateGiaTriPheDuyet);
            }

            if (lstError.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstError),
                    Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private VdtDaDuAn ConvertDataModel()
        {
            VdtDaDuAn data = _mapper.Map<VdtDaDuAn>(Model);
            DonVi donviSuDung = _nsDonViService.FindByIdDonVi(_sessionService.Current.IdDonVi, _sessionService.Current.YearOfWork);
            if (donviSuDung != null)
            {
                data.IIdMaDonViQuanLy = donviSuDung.IIDMaDonVi;
                data.IIdDonViQuanLyId = donviSuDung.Id;
            }

            if (SelectedDonVi != null)
            {
                data.IIdMaDonViThucHienDuAn = SelectedDonVi.ValueItem;
                data.IIdDonViThucHienDuAnId = Guid.Parse(SelectedDonVi.HiddenValue);
            }

            if (SelectedChuDauTu != null)
            {
                data.IIdChuDauTuId = SelectedChuDauTu.Id;
                data.IIdMaChuDauTuId = SelectedChuDauTu.HiddenValue;
            }
            if (SelectedPhanCapPheDuyet != null)
                data.IIdCapPheDuyetId = Guid.Parse(SelectedPhanCapPheDuyet.ValueItem);
            if (SelectedHinhThucQL != null)
                data.IIdHinhThucQuanLyId = Guid.Parse(SelectedHinhThucQL.ValueItem);
            if (SelectedNhomDuAn != null)
                data.IIdNhomDuAnId = Guid.Parse(SelectedNhomDuAn.ValueItem);
            if (Model.Id == Guid.Empty)
            {
                if (!string.IsNullOrEmpty(data.IIdMaChuDauTuId))
                {
                    data.SMaDuAn = data.IIdMaDonViQuanLy + "-" + data.IIdMaChuDauTuId;
                }
                else
                {
                    data.SMaDuAn = data.IIdMaDonViQuanLy + "-XXX";
                }
            }
            return data;
        }

        private string UpdateMaDuAn()
        {
            string maDuAn = string.Empty;
            string sMaChuDauTu = "XXX";

            if (SelectedChuDauTu != null)
            {
                var items = _dmChuDauTuService.FindById(SelectedChuDauTu.Id);
                if (items != null)
                {
                    sMaChuDauTu = items.IIDMaDonVi;
                }
            }

            int indexGenerate = _duAnService.FindNextSoChungTuIndex();

            if (indexSMaDuAn >= indexGenerate)
            {
                indexGenerate = indexSMaDuAn + 1;
            }

            indexSMaDuAn = indexGenerate;

            string sGenerate = indexGenerate.ToString("D4");

            string sMaDuAn = string.Format("{0}-{1}-{2}", SelectedDonVi.ValueItem, sMaChuDauTu, sGenerate);

            maDuAn = sMaDuAn;
            return maDuAn;
        }

        private void SaveDataDetail()
        {
            SaveNguonVon();
            SaveHangMuc();
            LoadData();
        }

        #region Master
        private void OnSetSelectedDataModel()
        {
            if (!string.IsNullOrEmpty(Model.IIdMaDonViQuanLy))
            {
                _selectedDonVi = DataDonVi.FirstOrDefault(n => n.ValueItem == Model.IIdMaDonViQuanLy);
                OnPropertyChanged(nameof(SelectedDonVi));
            }
            if (Model.IIdChuDauTuId != null && Model.IIdChuDauTuId != Guid.Empty)
            {
                _selectedChuDauTu = DataChuDauTu.FirstOrDefault(n => n.Id == Model.IIdChuDauTuId);
                OnPropertyChanged(nameof(SelectedChuDauTu));
            }
            if (Model.IIdCapPheDuyetId.HasValue)
            {
                _selectedPhanCapPheDuyet = DataPhanCapPheDuyet
                    .FirstOrDefault(n => n.ValueItem.ToLower() == Model.IIdCapPheDuyetId.Value.ToString().ToLower());
                OnPropertyChanged(nameof(SelectedPhanCapPheDuyet));
            }
            if (Model.IIdNhomDuAnId.HasValue)
            {
                _selectedNhomDuAn = DataNhomDuAn
                    .FirstOrDefault(n => n.ValueItem.ToLower() == Model.IIdNhomDuAnId.Value.ToString().ToLower());
                OnPropertyChanged(nameof(SelectedNhomDuAn));
                if (Model.IIdHinhThucQuanLyId.HasValue)
                {
                    _selectedHinhThucQL = DataHinhThucQL
                        .FirstOrDefault(n => n.ValueItem.ToLower() == Model.IIdHinhThucQuanLyId.Value.ToString().ToLower());
                    OnPropertyChanged(nameof(SelectedHinhThucQL));
                }
            }
        }

        private void LoadDonVi()
        {
            List<DonVi> lstDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
            if (lstDonVi.Count > 0)
            {
                lstDonVi = lstDonVi.OrderBy(x => x.IIDMaDonVi).ToList();
            }
            if (lstDonVi == null) return;
            var drpItem = lstDonVi.Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, HiddenValue = n.Id.ToString(), DisplayItem = (n.IIDMaDonVi + " - " + n.TenDonVi) });
            _dataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(drpItem);

            var strUnitManager = _sessionService.Current.IdsDonViQuanLy;

            if (!string.IsNullOrEmpty(strUnitManager))
            {
                List<string> listUnitManager = strUnitManager.Split(",").ToList();
                _dataDonVi = new ObservableCollection<ComboboxItem>(_dataDonVi.Where(x => listUnitManager.Any(y => y.Contains(x.ValueItem))).ToList());
            }
            else
            {
                _dataDonVi = new ObservableCollection<ComboboxItem>();
            }

            OnPropertyChanged(nameof(DataDonVi));
        }

        private void LoadChuDauTu()
        {
            //IEnumerable<DmChuDauTu> listChuDauTu = _dmChuDauTuService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            IEnumerable<DmChuDauTu> listChuDauTu = _dmChuDauTuService.FindByAllDataDonVi();
            _dataChuDauTu = _mapper.Map<ObservableCollection<ComboboxItem>>(listChuDauTu);
            OnPropertyChanged(nameof(DataChuDauTu));
        }

        private void LoadPhanCapPheDuyet()
        {
            IEnumerable<VdtDmPhanCapDuAn> listPhanCap = _projectManagerService.GetAllPhanCapDuAn();
            _dataPhanCapPheDuyet = _mapper.Map<ObservableCollection<ComboboxItem>>(listPhanCap);
            OnPropertyChanged(nameof(DataPhanCapPheDuyet));
        }

        private void LoadNguonVon()
        {
            var drpData = _nsNguonVonService.FindNguonNganSach().OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
            _cbxNguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(drpData);
            OnPropertyChanged(nameof(CbxNguonVon));
        }

        private void LoadNhomDuAn()
        {
            IEnumerable<VdtDmNhomDuAn> listNhomDuAn = _approveProjectService.GetAllNhomDuAn();
            _dataNhomDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listNhomDuAn);
            OnPropertyChanged(nameof(DataNhomDuAn));
        }

        private void LoadHinhThucQL()
        {
            IEnumerable<VdtDmHinhThucQuanLy> listHinhThucQL = _approveProjectService.GetAllHinhThucQuanLy();
            _dataHinhThucQL = _mapper.Map<ObservableCollection<ComboboxItem>>(listHinhThucQL);
            OnPropertyChanged(nameof(DataHinhThucQL));
        }

        private void LoadLoaiCongTrinh()
        {
            var data = _projectManagerService.GetAllDMLoaiCongTrinh();
            if (data == null) return;
            _dataLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(data.Select(n => new ComboboxItem()
            {
                ValueItem = n.IIdLoaiCongTrinh.ToString(),
                DisplayItem = n.STenLoaiCongTrinh
            }));
            OnPropertyChanged(nameof(DataLoaiCongTrinh));
        }
        #endregion

        #region HangMuc
        private void SaveHangMuc()
        {
            List<VdtDaHangMucModel> listAdd = HangMucItems.Where(x => !x.IsDeleted && x.BIsAdd).ToList();
            List<VdtDaHangMucModel> listEdit = HangMucItems.Where(x => !x.IsDeleted && !x.BIsAdd).ToList();
            List<VdtDaHangMucModel> listDetailDelete = HangMucItems.Where(x => x.IsDeleted && !x.BIsAdd).ToList();

            if (listAdd != null && listAdd.Count > 0)
            {
                AddHangMucSave(listAdd);
            }
            if (listEdit != null && listEdit.Count > 0)
            {
                UpdateHangMucSave(listEdit);
            }
            if (listDetailDelete != null && listDetailDelete.Count > 0)
            {
                DeleteHangMuc(listDetailDelete);
            }
        }

        private void AddHangMucSave(List<VdtDaHangMucModel> listAdd)
        {
            if (listAdd != null && listAdd.Count > 0)
            {
                foreach (var item in listAdd)
                {
                    item.IIdDuAnId = Model.Id;
                }
                List<VdtDaDuAnHangMuc> listHangMuc = new List<VdtDaDuAnHangMuc>();
                listHangMuc = _mapper.Map<List<VdtDaDuAnHangMuc>>(listAdd);
                _approveProjectService.AddRangeDuAnHangMuc(listHangMuc);
            }
        }

        private void UpdateHangMucSave(List<VdtDaHangMucModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                VdtDaDuAnHangMuc duAnHangMuc = _approveProjectService.FindDuAnHangMuc(item.IdDuAnHangMuc);
                if (duAnHangMuc != null)
                {
                    _mapper.Map(item, duAnHangMuc);
                    _approveProjectService.UpdateHangMuc(duAnHangMuc);
                }
            }
        }

        private void HangMuc_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtDaHangMucModel objectSender = (VdtDaHangMucModel)sender;
            if (args.PropertyName == nameof(objectSender.SMaHangMuc) || args.PropertyName == nameof(objectSender.STenHangMuc))
            {
                SelectedHangMuc = objectSender;
                if (args.PropertyName == nameof(objectSender.SMaHangMuc) && !string.IsNullOrEmpty(objectSender.SMaHangMuc))
                {
                    if (string.IsNullOrEmpty(objectSender.MaOrDer))
                    {
                        objectSender.MaOrDer = objectSender.SMaHangMuc;
                    }
                }
            }
            if (args.PropertyName == nameof(objectSender.HanMucDT))
            {
                CalculateDataChiPhiHangMuc();
            }

            objectSender.IsModified = true;
            GetTongHangMuc();
            OnPropertyChanged(nameof(HangMucItems));

        }

        private void DeleteHangMuc(List<VdtDaHangMucModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _approveProjectService.DeleteDuAnHangMucByDuAnId(item.IdDuAnHangMuc);
            }
        }

        private void GetTongHangMuc()
        {
            _fTongHangMuc = HangMucItems.Where(n => n.IIdParentId == null && !n.IsDeleted).Sum(n => (n.HanMucDT));
            OnPropertyChanged(nameof(FTongHangMuc));
        }

        private string GetSTTHangMuc(bool isAddChild = false)
        {
            string sttHangMuc = string.Empty;
            int inDexSTTHangMucLast = 1;
            if (SelectedHangMuc == null && isAddChild == false)
            {
                if (HangMucItems.Count < 1)
                {
                    sttHangMuc = "1";

                    currentRow = -1;
                }
                else
                {
                    var hangMucItemLast = HangMucItems.Where(x => x.IIdParentId == null).Last();
                    if (!string.IsNullOrEmpty(hangMucItemLast.MaOrDer))
                    {
                        inDexSTTHangMucLast = Int32.Parse(hangMucItemLast.MaOrDer);
                    }
                    else
                    {
                        inDexSTTHangMucLast = 0;
                    }

                    sttHangMuc = (inDexSTTHangMucLast + 1).ToString();
                    currentRow = HangMucItems.IndexOf(hangMucItemLast);
                }
            }
            if (SelectedHangMuc != null && isAddChild == false)
            {
                var hangMucLast = HangMucItems.First();// lấy giá trị mặc định là giá trị đầu tiên

                //tìm giá trị ngang hàng cuối cùng trong list => giá trị thêm mới được copy từ giá trị ngang hàng cuối cùng
                if (SelectedHangMuc.IIdParentId == null)
                {
                    hangMucLast = HangMucItems.Where(x => x.IIdParentId == null).Last();
                    inDexSTTHangMucLast = Int32.Parse(hangMucLast.MaOrDer);
                    sttHangMuc = (inDexSTTHangMucLast + 1).ToString();
                    currentRow = HangMucItems.Count - 1;
                }
                else
                {
                    hangMucLast = HangMucItems.Where(x => x.IIdParentId == SelectedHangMuc.IIdParentId).Last();
                    string sTTHangMucLast = hangMucLast.MaOrDer;
                    inDexSTTHangMucLast = Int32.Parse(sTTHangMucLast.Substring(sTTHangMucLast.Length - 1));
                    sttHangMuc = sTTHangMucLast.Substring(0, (sTTHangMucLast.Length - 1)) + (inDexSTTHangMucLast + 1).ToString();
                    currentRow = HangMucItems.IndexOf(hangMucLast);
                }


            }
            if (SelectedHangMuc != null && isAddChild == true)
            {
                var listChild = HangMucItems.Where(x => x.IIdParentId == SelectedHangMuc.IdDuAnHangMuc).ToList();
                if (listChild == null || listChild.Count == 0)
                {
                    sttHangMuc = SelectedHangMuc.MaOrDer + "_1";
                    currentRow = HangMucItems.IndexOf(SelectedHangMuc);
                }
                else
                {
                    var hangMucChildLast = HangMucItems.Where(x => x.IIdParentId == SelectedHangMuc.IdDuAnHangMuc).Last();
                    string sTTHangMucLast = hangMucChildLast.MaOrDer;
                    inDexSTTHangMucLast = Int32.Parse(sTTHangMucLast.Substring(sTTHangMucLast.Length - 1));
                    sttHangMuc = sTTHangMucLast.Substring(0, (sTTHangMucLast.Length - 1)) + (inDexSTTHangMucLast + 1).ToString();
                    currentRow = HangMucItems.IndexOf(hangMucChildLast);
                }
            }
            return sttHangMuc;
        }

        private void CalculateDataChiPhiHangMuc()
        {
            List<VdtDaHangMucModel> listCalculate = HangMucItems.Where(x => x.IIdParentId.HasValue && !x.IsDeleted).ToList();
            List<VdtDaHangMucModel> listParent = HangMucItems.Where(x => listCalculate.Any(y => y.IIdParentId == x.IdDuAnHangMuc) && !x.IsDeleted).ToList();
            if (listParent != null && listParent.Count > 0)
            {
                foreach (var item in listParent)
                {
                    CalculateParent(item);
                }
            }
        }

        private void CalculateParent(VdtDaHangMucModel parentItem)
        {
            List<VdtDaHangMucModel> listChild = HangMucItems.Where(x => x.IIdParentId == parentItem.IdDuAnHangMuc && !x.IsDeleted).ToList();
            if (listChild != null && listChild.Count > 0)
            {
                var tongParent = listChild.Sum(x => x.HanMucDT);
                parentItem.HanMucDT = tongParent;
                OnPropertyChanged(nameof(HangMucItems));
            }
        }
        #endregion
        #endregion

    }
}
