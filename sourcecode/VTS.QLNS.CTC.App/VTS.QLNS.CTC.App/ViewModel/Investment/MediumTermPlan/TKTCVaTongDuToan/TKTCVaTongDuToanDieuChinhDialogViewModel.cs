using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.TKTCVaTongDuToan
{
    public class TKTCVaTongDuToanDieuChinhDialogViewModel : DialogAttachmentViewModelBase<VdtDuToanModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_TKTC_VA_TONG_DU_TOAN_DIALOG_MODIFY;
        public override string Name => "ĐIỀU CHỈNH THÔNG TIN TKTC VÀ TỔNG DỰ TOÁN";
        public override string Title => "Thiết kế thi công và tổng dự toán";
        public override string Description { get; set; }
        public override Type ContentType => typeof(View.Investment.MediumTermPlan.TKTCVaTongDuToan.TKTCVaTongDuToanDieuChinhDialog);
        //public bool IsEditable => (Model.BActive == true || IsAdd) && IsNotViewDetail;
        
        private double? _tongGiaTriPheDuyetDuAn;
        public double? TongGiaTriPheDuyetDuAn
        {
            get => _tongGiaTriPheDuyetDuAn;
            set => SetProperty(ref _tongGiaTriPheDuyetDuAn, value);
        }

        private VdtDuToanModel _duToan;
        public VdtDuToanModel DuToan
        {
            get => _duToan;
            set => SetProperty(ref _duToan, value);
        }
      
        private List<ComboboxItem> _dataNguonVon;
        public List<ComboboxItem> DataNguonVon
        {
            get => _dataNguonVon;
            set => SetProperty(ref _dataNguonVon, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaDuToanChiPhiModel> _dataDuToanChiPhi;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaDuToanChiPhiModel> DataDuToanChiPhi
        {
            get => _dataDuToanChiPhi;
            set
            {
                TaoMaOrders(value.ToList());
                SetProperty(ref _dataDuToanChiPhi, value);
                OnPropertyChanged(nameof(DataDuToanChiPhi));
            }
        }

        private List<ComboboxItem> _dataLoaiQD;
        public List<ComboboxItem> DataLoaiQD
        {
            get => _dataLoaiQD;
            set => SetProperty(ref _dataLoaiQD, value);
        }

        private ComboboxItem _selectedLoaiQD;
        public ComboboxItem SelectedLoaiQD
        {
            get => _selectedLoaiQD;
            set
            {
                SetProperty(ref _selectedLoaiQD, value);
            }
        }

        private string txtLoaiQD { get; set; }

        private VdtDaDuToanChiPhiModel _selectedChiPhi;
        public VdtDaDuToanChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaDuToanNguonVonModel> _dataDuToanNguonVon;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaDuToanNguonVonModel> DataDuToanNguonVon
        {
            get => _dataDuToanNguonVon;
            set => SetProperty(ref _dataDuToanNguonVon, value);
        }

        private VdtDaDuToanNguonVonModel _selectedNguonVon;
        public VdtDaDuToanNguonVonModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set => SetProperty(ref _selectedNguonVon, value);
        }

        private List<VTS.QLNS.CTC.App.Model.DuToanDetailModel> _dataDuToanHangMuc;
        public List<VTS.QLNS.CTC.App.Model.DuToanDetailModel> DataDuToanHangMuc
        {
            get => _dataDuToanHangMuc;
            set => SetProperty(ref _dataDuToanHangMuc, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.DuToanDetailModel> _dataDuToanHangMucByChiPhi;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.DuToanDetailModel> DataDuToanHangMucByChiPhi
        {
            get => _dataDuToanHangMucByChiPhi;
            set => SetProperty(ref _dataDuToanHangMucByChiPhi, value);
        }

        private double? _fTongNguonVon;
        public double? FTongNguonVon
        {
            get => _fTongNguonVon;
            set => SetProperty(ref _fTongNguonVon, value);
        }

        private double? _conLai;
        public double? ConLai
        {
            get => _conLai;
            set => SetProperty(ref _conLai, value);
        }

        private bool _isAddDieuChinh;
        public bool IsAddDieuChinh
        {
            get => _isAddDieuChinh;
            set => SetProperty(ref _isAddDieuChinh, value);
        }

        public double? TongDuToanPheDuyet => Model.FTongDuToanPheDuyet;

        private bool _isNotViewDetail;
        public bool IsNotViewDetail
        {
            get => _isNotViewDetail;
            set => SetProperty(ref _isNotViewDetail, value);
        }

        private List<DuToanDetailModel> _hangMucPhanChiaSaved;
        public List<DuToanDetailModel> HangMucPhanChiaSaved
        {
            get => _hangMucPhanChiaSaved;
            set => SetProperty(ref _hangMucPhanChiaSaved, value);
        }

        public Dictionary<Guid?, Guid> refDictionary { get; set; }

        public RelayCommand AddChiPhiDetailCommand { get; }
        public RelayCommand AddChiPhiChildDetailCommand { get; }
        public RelayCommand AddNguonVonDetailCommand { get; }
        public RelayCommand ShowHangMucDetailCommand { get; }
        public RelayCommand DeleteChiPhiCommand { get; }
        public RelayCommand DeleteNguonVonCommand { get; }

        TKTCVaTongDuToanDieuChinhDetailViewModel TongDuToanDieuChinhDetailViewModel { get; }

        public TKTCVaTongDuToanDieuChinhDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IApproveProjectService approveProjectService,
            IProjectManagerService projectManagerService,
            IVdtDaDuToanService vdtDaDuToanService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            TKTCVaTongDuToanDieuChinhDetailViewModel tKTCVaTongDuToanDetailViewModel)
            : base(mapper, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _approveProjectService = approveProjectService;
            _projectManagerService = projectManagerService;
            _vdtDaDuToanService = vdtDaDuToanService;

            TongDuToanDieuChinhDetailViewModel = tKTCVaTongDuToanDetailViewModel;

            AddChiPhiDetailCommand = new RelayCommand(obj => OnAddChiPhi());
            AddChiPhiChildDetailCommand = new RelayCommand(obj => OnAddChiPhiChild());
            AddNguonVonDetailCommand = new RelayCommand(obj => OnAddNguonVon());
            ShowHangMucDetailCommand = new RelayCommand(obj => OnShowDetailDuToan());
            DeleteNguonVonCommand = new RelayCommand(obj => OnDeleteNguonVon());
            DeleteChiPhiCommand = new RelayCommand(obj => OnDeleteChiPhi());
        }

        public override void Init()
        {
            ConLai = 0;
            HangMucPhanChiaSaved = new List<DuToanDetailModel>();
            LoadNguonVon();
            LoadData();
        }

        private void LoadNguonVon()
        {
            IEnumerable<Core.Domain.NsNguonNganSach> listNguonVon = _approveProjectService.GetAllNguonNS();
            DataNguonVon = _mapper.Map<List<ComboboxItem>>(listNguonVon);
        }

        private void LoadDataLoaiQD()
        {
            List<ComboboxItem> listLoaiQD = new List<ComboboxItem>();
            listLoaiQD.Add(new ComboboxItem { ValueItem = "true", DisplayItem = "Là tổng dự toán" });
            listLoaiQD.Add(new ComboboxItem { ValueItem = "false", DisplayItem = "Theo giai đoạn" });
            DataLoaiQD = listLoaiQD;
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            LoadDataLoaiQD();
            DataDuToanHangMuc = new List<DuToanDetailModel>();
            if (IsAddDieuChinh)
            {
                Description = "Điều chỉnh thiết kế thi công và tổng dự toán";
                DuToan = ObjectCopier.Clone(Model);
                DuToan.Id = Guid.Empty;
                DuToan.SSoQuyetDinh = null;
                DuToan.DNgayQuyetDinh = DateTime.Now;
                DuToan.LoaiDuToan = DataLoaiQD.FirstOrDefault(x => x.ValueItem == DuToan.BLaTongDuToan.ToString().ToLower()).DisplayItem;
                LoadDataNguonVonDieuChinhByDuToanIdAdd();
                LoadDataChiPhiDieuChinhAdd();
                LoadDataDuToanHangMucAdd();
            }
            else
            {
                Description = "Cập nhật thiết kế thi công và tổng dự toán điều chỉnh";
                //DuToan = ObjectCopier.Clone(Model);
                LoadDataNguonVonDieuChinhUpdate();
                LoadDataChiPhiDieuChinhUpdate();
                LoadDataDutoanHangMucUpdate();
            }
            if (DuToan.IIdDuAnId != null && DuToan.IIdDuAnId.Value != null)
            {
                VdtDaDuAn duAn = _projectManagerService.FindById(DuToan.IIdDuAnId.Value);
                if (duAn != null)
                {
                    var objQdDauTu = _approveProjectService.FindListQDDauTuByDuAnId(duAn.Id).FirstOrDefault(n => (n.BActive ?? false));
                    if (objQdDauTu != null)
                        TongGiaTriPheDuyetDuAn = objQdDauTu.FTongMucDauTuPheDuyet;
                    else
                        TongGiaTriPheDuyetDuAn = 0;
                }
            }
            OnPropertyChanged(nameof(TongGiaTriPheDuyetDuAn));
            CalculateConLaiNguonVon();
            if (!IsNotViewDetail)
            {
                Description = "Xem chi tiết thông tin thiết kế thi công và tổng dự toán";
            }
        }

        private void LoadDataNguonVonDieuChinhByDuToanIdAdd()
        {
            List<VdtDaDuToanNguonVonQuery> listDuToanNguonVonQuery = _vdtDaDuToanService.FindListDuToanNguonVonDieuChinhAdd(Model.Id).ToList();
            DataDuToanNguonVon = _mapper.Map<ObservableCollection<VdtDaDuToanNguonVonModel>>(listDuToanNguonVonQuery);
            foreach (var item in DataDuToanNguonVon)
            {
                item.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            }
        }

        private void LoadDataNguonVonDieuChinhUpdate()
        {
            List<VdtDaDuToanNguonVonQuery> listDuToanNguonVonQuery = _vdtDaDuToanService.FindListDuToanNguonVonDieuChinhUpdate(DuToan.Id).ToList();
            DataDuToanNguonVon = _mapper.Map<ObservableCollection<VdtDaDuToanNguonVonModel>>(listDuToanNguonVonQuery);
            foreach (var item in DataDuToanNguonVon)
            {
                item.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            }
        }

        private void LoadDataChiPhiDieuChinhAdd()
        {
            List<VdtDaDuToanChiPhiQuery> listQuery = _vdtDaDuToanService.FindListDuToanChiPhiDieuChinhAdd(Model.Id).ToList();

            // Renew id điều chỉnh
            refDictionary = listQuery.ToDictionary(x => x.IdChiPhiDuAn, x => Guid.NewGuid());
            listQuery = listQuery.Select(x =>
            {
                x.IdChiPhiDuAn = refDictionary[x.IdChiPhiDuAn];
                if (x.IdChiPhiDuAnParent != null)
                {
                    x.IdChiPhiDuAnParent = refDictionary[x.IdChiPhiDuAnParent];
                }
                return x;
            }).ToList();

            DataDuToanChiPhi = _mapper.Map<ObservableCollection<VdtDaDuToanChiPhiModel>>(listQuery);
            UpdateListChiPhiCanEdit();
            foreach (var item in DataDuToanChiPhi)
            {
                item.PropertyChanged += DetailChiPhiModel_PropertyChanged;
            }
            //CalculateDataChiPhi();
        }

        private void LoadDataChiPhiDieuChinhUpdate()
        {
            List<VdtDaDuToanChiPhiQuery> listQuery = _vdtDaDuToanService.FindListDuToanChiPhiDieuChinhUpdate(DuToan.Id).ToList();
            DataDuToanChiPhi = _mapper.Map<ObservableCollection<VdtDaDuToanChiPhiModel>>(listQuery);
            UpdateListChiPhiCanEdit();
            foreach (var item in DataDuToanChiPhi)
            {
                item.PropertyChanged += DetailChiPhiModel_PropertyChanged;
            }
            //CalculateDataChiPhi();
        }

        private void UpdateListChiPhiCanEdit()
        {
            foreach (var item in DataDuToanChiPhi.Where(x => x.IsHangCha && !x.IsDeleted))
            {
                var listChild = FindListChildChiPhi(item.IdChiPhiDuAn.Value);
                if (listChild == null || listChild.Count == 0)
                {
                    item.IsEditHangMuc = true;
                }
                else
                {
                    item.IsEditHangMuc = false;
                }
            }
            OnPropertyChanged(nameof(DataDuToanChiPhi));
        }

        public void LoadDataDuToanHangMucAdd()
        {
            List<DuToanDetailQuery> listData = new List<DuToanDetailQuery>();

            listData = _vdtDaDuToanService.ListHangMucByDuToan(Model.Id).ToList();

            // Renew id chi phí trong hạng mục
            listData = listData.Select(x =>
            {
                x.IdDuAnChiPhi = refDictionary[x.IdDuAnChiPhi];

                return x;
            }).ToList();

            // renew id hạng mục
            var refDictionaryDM = listData.ToDictionary(x => x.IdDuAnHangMuc, x => Guid.NewGuid());

            listData = listData.Select(x =>
            {
                x.Id = null;
                x.IdDuToanHangMuc = null;
                x.IdDuAnHangMuc = refDictionaryDM[x.IdDuAnHangMuc];
                if (x.HangMucParentId != null)
                {
                    x.HangMucParentId = refDictionaryDM[x.HangMucParentId];
                }

                return x;
            }).ToList();

            DataDuToanHangMuc = _mapper.Map<List<Model.DuToanDetailModel>>(listData);
            UpdateListChiPhiCanEditGiaTriChiPhi();
        }

        private void UpdateListChiPhiCanEditGiaTriChiPhi()
        {
            foreach (var item in DataDuToanChiPhi.Where(x => x.IsEditHangMuc && !x.IsDeleted))
            {
                if (CheckChiPhiCanDelete(item.IdChiPhiDuAn.Value))
                {
                    item.IsEditGiaTriChiPhi = true;
                }
                else
                {
                    item.IsEditGiaTriChiPhi = false;
                }
            }
        }

        public bool CheckChiPhiCanDelete(Guid duanChiPhiId)
        {
            var listHangMucByChiPhi = DataDuToanHangMuc.Where(x => x.IdDuAnChiPhi == duanChiPhiId && !x.IsDeleted).ToList();
            if (listHangMucByChiPhi.Count > 0)
            {
                return false;
            }
            return true;
        }

        public void LoadDataDutoanHangMucUpdate()
        {
            List<DuToanDetailQuery> listData = new List<DuToanDetailQuery>();

            listData = _vdtDaDuToanService.ListHangMucByDuToan(DuToan.Id).ToList();
            DataDuToanHangMuc = _mapper.Map<List<Model.DuToanDetailModel>>(listData);
        }

        public List<VdtDaDuToanChiPhiModel> FindListChildChiPhi(Guid parentId)
        {
            List<VdtDaDuToanChiPhiModel> inner = new List<VdtDaDuToanChiPhiModel>();
            foreach (var t in DataDuToanChiPhi.Where(item => item.IdChiPhiDuAnParent == parentId && !item.IsDeleted))
            {
                inner.Add(t);
                inner = inner.Union(FindListChildChiPhi(t.Id)).ToList();
            }

            return inner;
        }

        private void DetailChiPhiModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtDaDuToanChiPhiModel objectSender = (VdtDaDuToanChiPhiModel)sender;

            if (args.PropertyName == nameof(VdtDaDuToanChiPhiModel.GiaTriPheDuyet) || args.PropertyName == nameof(VdtDaDuToanChiPhiModel.IsDeleted))
            {
                if (args.PropertyName == nameof(VdtDaDuToanChiPhiModel.GiaTriPheDuyet))
                {
                    objectSender.FGiaTriDieuChinh = objectSender.GiaTriPheDuyet - objectSender.GiaTriTruocDieuChinh;
                }
                CalculateDataChiPhi();
                CalculateConLaiNguonVon();
                OnPropertyChanged(nameof(DataDuToanNguonVon));
            }

            objectSender.IsModified = true;
        }


        private void CalculateDataChiPhi()
        {
            if (DataDuToanChiPhi == null) return;
            foreach (var item in DataDuToanChiPhi.Where(n => !n.IdChiPhiDuAnParent.HasValue))
            {
                CalculateParent(item);
            }
            OnPropertyChanged(nameof(DataDuToanChiPhi));
        }

        private void CalculateParent(VdtDaDuToanChiPhiModel parentItem)
        {
            int childRow = 0;
            foreach (var item in DataDuToanChiPhi.Where(n => n.IdChiPhiDuAnParent.HasValue && n.IdChiPhiDuAnParent == parentItem.IdChiPhiDuAn))
            {
                CalculateParent(item);
                childRow++;
            }
            if (childRow != 0)
                parentItem.GiaTriPheDuyet = DataDuToanChiPhi.Where(n => n.IdChiPhiDuAnParent.HasValue && n.IdChiPhiDuAnParent == parentItem.IdChiPhiDuAn && !n.IsDeleted).Sum(n => n.GiaTriPheDuyet);
        }

        private void DetailNguonVonModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtDaDuToanNguonVonModel objectSender = (VdtDaDuToanNguonVonModel)sender;

            if (args.PropertyName == nameof(VdtDaDuToanNguonVonModel.IdNguonVon))
            {
                if (objectSender.IdNguonVon != null && objectSender.IdNguonVon != 0)
                {
                    if (CheckDuplicateNguonVon(objectSender.IdNguonVon.Value))
                    {
                        System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SelectedNguonVon.IdNguonVon = null;
                        return;
                    }
                }
            }
            if (args.PropertyName == nameof(VdtDaDuToanNguonVonModel.GiaTriPheDuyet) || args.PropertyName == nameof(VdtDaDuToanNguonVonModel.IsDeleted))
            {
                if (args.PropertyName == nameof(VdtDaDuToanNguonVonModel.GiaTriPheDuyet))
                {
                    objectSender.FGiaTriDieuChinh = objectSender.GiaTriPheDuyet - objectSender.GiaTriTruocDieuChinh;
                }
                if (DataDuToanNguonVon != null && DataDuToanNguonVon.Count > 0)
                {
                    CalculateConLaiNguonVon();
                    OnPropertyChanged(nameof(DuToan));
                }
            }

            objectSender.IsModified = true;
            OnPropertyChanged(nameof(DataDuToanChiPhi));
        }

        private bool CheckDuplicateNguonVon(int idNguonVon)
        {
            List<VdtDaDuToanNguonVonModel> listNguonVon = DataDuToanNguonVon.Where(x => x.IdNguonVon == idNguonVon && !x.IsDeleted).ToList();
            if (listNguonVon != null && listNguonVon.Count > 1)
            {
                return true;
            }
            return false;
        }

        private void CalculateConLaiNguonVon()
        {
            double tongNguonVon = 0;
            double tongChiPhi = 0;
            ConLai = 0;
            if (DataDuToanNguonVon != null && DataDuToanNguonVon.Count > 0)
            {
                tongNguonVon = DataDuToanNguonVon.Where(y => !y.IsDeleted).Sum(x => x.GiaTriPheDuyet);
            }

            if (DataDuToanChiPhi != null && DataDuToanChiPhi.Count > 0)
            {
                var listChiPhiCha = DataDuToanChiPhi.Where(x => x.IsLoaiChiPhi).ToList();
                tongChiPhi = listChiPhiCha.Sum(x => x.GiaTriPheDuyet);
            }
            ConLai = tongNguonVon - tongChiPhi;
            FTongNguonVon = tongNguonVon;
            OnPropertyChanged(nameof(FTongNguonVon));
        }

        protected void OnDeleteChiPhi()
        {
            if (SelectedChiPhi == null || SelectedChiPhi.IsLoaiChiPhi)
            {
                if (SelectedChiPhi.IdChiPhiDuAnParent == null)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgVocherParent, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
            if (DataDuToanChiPhi.Count > 0 && SelectedChiPhi != null)
            {
                var listChiPhi = FindListChildChiPhi(SelectedChiPhi.Id);
                listChiPhi.Add(SelectedChiPhi);
                foreach (var item in listChiPhi)
                {
                    item.IsDeleted = !SelectedChiPhi.IsDeleted;
                }
                UpdateListChiPhiCanEdit();
                // SelectedChiPhi.IsDeleted = !SelectedChiPhi.IsDeleted;
                OnPropertyChanged(nameof(DataDuToanChiPhi));
            }
        }

        protected void OnAddNguonVon()
        {
            VdtDaDuToanNguonVonModel targetItem = new VdtDaDuToanNguonVonModel()
            {
                Id = Guid.NewGuid(),
                IdDuToanNguonVon = Guid.Empty,
                IdNguonVon = null,
                GiaTriPheDuyet = 0,
                GiaTriTruocDieuChinh = 0
            };
            int currentRow = -1;
            if (DataDuToanNguonVon != null && DataDuToanNguonVon.Count > 0)
            {
                currentRow = DataDuToanNguonVon.Count - 1;
                VdtDaDuToanNguonVonModel sourceItem = DataDuToanNguonVon.ElementAt(currentRow);
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.Id = Guid.NewGuid();
                targetItem.IdDuToanNguonVon = Guid.Empty;
                targetItem.GiaTriPheDuyet = 0;
                targetItem.FTienPheDuyetQDDT = 0;
                targetItem.GiaTriTruocDieuChinh = 0;
                targetItem.IdNguonVon = null;
                targetItem.IsDeleted = false;
            }
            targetItem.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            DataDuToanNguonVon.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(DataDuToanNguonVon));
        }

        protected void OnDeleteNguonVon()
        {
            if (DataDuToanNguonVon != null && DataDuToanNguonVon.Count > 0 && SelectedNguonVon != null)
            {
                SelectedNguonVon.IsDeleted = !SelectedNguonVon.IsDeleted;
                OnPropertyChanged(nameof(DataDuToanNguonVon));
            }
        }

        protected void OnAddChiPhi()
        {
            if (SelectedChiPhi == null || SelectedChiPhi.IsLoaiChiPhi || SelectedChiPhi.IsDeleted)
            {
                return;
            }
            VdtDaDuToanChiPhiModel targetItem = new VdtDaDuToanChiPhiModel()
            {
                Id = Guid.Empty,
                IdDuToanChiPhi = Guid.Empty,
                IdChiPhi = Guid.Empty
            };
            int currentRow = -1;
            if (DataDuToanChiPhi != null && DataDuToanChiPhi.Count > 0)
            {
                currentRow = 0;
                VdtDaDuToanChiPhiModel sourceItem = new VdtDaDuToanChiPhiModel();
                if (SelectedChiPhi != null)
                {
                    var listChiPhiChild = FindListChildChiPhi(SelectedChiPhi.IdChiPhiDuAn.Value);
                    if (listChiPhiChild == null || listChiPhiChild.Count == 0)
                    {
                        currentRow = DataDuToanChiPhi.IndexOf(SelectedChiPhi);
                    }
                    else
                    {
                        var indexOfLastChild = DataDuToanChiPhi.Where(x => x.IdChiPhiDuAn == listChiPhiChild.Last().IdChiPhiDuAn).FirstOrDefault();
                        currentRow = DataDuToanChiPhi.IndexOf(indexOfLastChild);
                    }

                    sourceItem = DataDuToanChiPhi.Where(x => x.IdChiPhiDuAn == SelectedChiPhi.IdChiPhiDuAn).FirstOrDefault();
                }

                //sourceItem = DataDuToanChiPhi.ElementAt(currentRow);
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.GiaTriPheDuyet = 0;
                targetItem.GiaTriTruocDieuChinh = 0;
                //targetItem.Id = Guid.Empty;
                targetItem.IdDuToanChiPhi = Guid.Empty;
                //targetItem.IsHangCha = false;
                targetItem.TenChiPhi = string.Empty;
                targetItem.IdChiPhi = sourceItem.IdChiPhi;
                targetItem.IdChiPhiDuAn = Guid.NewGuid();
                targetItem.IsLoaiChiPhi = false;
                targetItem.IsDuAnChiPhiOld = false;
                targetItem.IsEditHangMuc = true;
            }
            targetItem.PropertyChanged += DetailChiPhiModel_PropertyChanged;
            DataDuToanChiPhi.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(DataDuToanChiPhi));
        }

        protected void OnAddChiPhiChild()
        {
            VdtDaDuToanChiPhiModel targetItem = new VdtDaDuToanChiPhiModel()
            {
                Id = Guid.Empty,
                IdDuToanChiPhi = Guid.Empty,
                IdChiPhi = Guid.Empty
            };
            int currentRow = -1;
            if (DataDuToanChiPhi != null && DataDuToanChiPhi.Count > 0)
            {
                currentRow = 0;
                if (SelectedChiPhi != null)
                {
                    if (!CheckChiPhiCanDelete(SelectedChiPhi.IdChiPhiDuAn.Value))
                    {
                        System.Windows.Forms.MessageBox.Show(Resources.MsgErrAddChiPhiChild, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (SelectedChiPhi.IsDeleted)
                    {
                        return;
                    }
                    currentRow = DataDuToanChiPhi.IndexOf(SelectedChiPhi);
                }
                VdtDaDuToanChiPhiModel sourceItem = DataDuToanChiPhi.ElementAt(currentRow);
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.GiaTriPheDuyet = 0;
                targetItem.IsHangCha = false;
                targetItem.TenChiPhi = string.Empty;
                targetItem.Id = Guid.Empty;
                targetItem.IdDuToanChiPhi = Guid.Empty;
                targetItem.IdChiPhi = sourceItem.IdChiPhi;
                targetItem.IdChiPhiDuAn = Guid.NewGuid();
                targetItem.IdChiPhiDuAnParent = sourceItem.IdChiPhiDuAn;
                targetItem.IsLoaiChiPhi = false;
                targetItem.IsEditHangMuc = true;
                targetItem.IsEditGiaTriChiPhi = true;
                sourceItem.IsHangCha = true;
                sourceItem.GiaTriPheDuyet = 0;
                targetItem.GiaTriTruocDieuChinh = 0;
                sourceItem.IsModified = true;
                sourceItem.IsEditHangMuc = false;
                targetItem.IsDuAnChiPhiOld = false;
            }

            targetItem.PropertyChanged += DetailChiPhiModel_PropertyChanged;
            DataDuToanChiPhi.Insert(currentRow + 1, targetItem);
            TaoMaOrders(DataDuToanChiPhi.ToList());
            OnPropertyChanged(nameof(DataDuToanChiPhi));
        }

        public override void OnSave()
        {

            if (!ValiDateData() || !ValidateDataDetail())
            {
                return;
            }
            DuToan.FTongDuToanPheDuyet = DataDuToanNguonVon.Where(y => !y.IsDeleted).Sum(x => x.GiaTriPheDuyet);
            VdtDaDuToan entity = new VdtDaDuToan();
            if (DuToan.Id != Guid.Empty && DuToan.Id != null)
            {
                // Update
                entity = _vdtDaDuToanService.FindById(DuToan.Id);
                _mapper.Map(DuToan, entity);
                entity.BActive = true;
                entity.BIsGoc = false;
                entity.DDateUpdate = DateTime.Now;
                entity.SUserUpdate = _sessionService.Current.Principal;
                _vdtDaDuToanService.Update(entity);
            }
            else
            {
                // Add VdtDaDuToan
                entity = _mapper.Map<VdtDaDuToan>(DuToan);
                entity.BActive = true;
                entity.BIsGoc = false;
                entity.DDateCreate = DateTime.Now;
                entity.SUserCreate = _sessionService.Current.Principal;
                Model.DDateCreate = entity.DDateCreate;
                Model.SUserCreate = entity.SUserCreate;
                entity.IIdParentId = Model.Id;
                entity.IIdDuToanGocId = Model.IIdDuToanGocId;
                _vdtDaDuToanService.Add(entity);
                DuToan.Id = entity.Id;
                DuToan.IIdParentId = entity.IIdParentId;


                // update QDDauTu cha BActive = 0
                VdtDaDuToan entityParent = _vdtDaDuToanService.FindById(Model.Id);
                if (entityParent != null)
                {
                    entityParent.BActive = false;
                    entityParent.DDateUpdate = DateTime.Now;
                    entityParent.SUserUpdate = _sessionService.Current.Principal;
                    _vdtDaDuToanService.Update(entityParent);
                }

            }
            // Lưu chi tiết nguồn vốn và chi phí
            SaveDetail(entity.Id);

            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(_mapper.Map<VTS.QLNS.CTC.App.Model.VdtDuToanModel>(entity));
            IsAddDieuChinh = false;
            DuToan.Id = entity.Id;
            LoadData();
        }

        private bool ValiDateData()
        {
            if (string.IsNullOrEmpty(DuToan.SSoQuyetDinh))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckSoQD, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                if (_vdtDaDuToanService.CheckDuplicateSoQD(DuToan.SSoQuyetDinh, DuToan.Id))
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgTrungSoQD, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if ((DuToan.DNgayQuyetDinh == null))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckNgayPheDuyet, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (DuToan.DNgayQuyetDinh.HasValue)
            {
                int compareDate = DateTime.Compare(DuToan.DNgayQuyetDinh.Value, Model.DNgayQuyetDinh.Value);
                if (compareDate < 0)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckNgayPheDuyet, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            var tongGiaTriPheDuyetDaNhap = DataDuToanNguonVon.Where(y => !y.IsDeleted).Sum(x => x.GiaTriPheDuyet);
            if (tongGiaTriPheDuyetDaNhap > 0)
            {                
                var tongNguonVon = _vdtDaDuToanService.TinhTongPheDuyetDuAn(Model.IIdDuAnId, Model.Id) + tongGiaTriPheDuyetDaNhap;                
                if (tongNguonVon > TongGiaTriPheDuyetDuAn)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.ErrorNguonVonNotEqualPheDuyet, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        public bool ValidateDataDetail()
        {
            //validate tong chi phi = tong nguon von

            if (DataDuToanChiPhi.Count > 0 && DataDuToanNguonVon.Count > 0)
            {
                var listChiPhi = DataDuToanChiPhi.Where(x => !x.IsDeleted).ToList();
                var listNguonVon = DataDuToanNguonVon.Where(x => !x.IsDeleted && x.GiaTriPheDuyet > 0 && x.IdNguonVon != null && x.IdNguonVon != 0).ToList();

                if (listChiPhi != null && listNguonVon != null && listChiPhi.Count > 0 && listNguonVon.Count > 0)
                {
                    var tongChiPhi = listChiPhi.Where(x => x.IdChiPhiDuAnParent == null).Sum(x => x.GiaTriPheDuyet);
                    var tongNguonVon = listNguonVon.Sum(x => x.GiaTriPheDuyet);
                    if (tongChiPhi != tongNguonVon)
                    {
                        if (MessageBoxHelper.Confirm(Resources.MsgConfirmErrorChiPhiNotEqualNguonVon) == MessageBoxResult.Yes)
                        {
                            return true;
                        }
                        return false;
                    }
                    if (tongNguonVon > TongDuToanPheDuyet)
                    {
                        System.Windows.Forms.MessageBox.Show(Resources.ErrorNguonVonNotEqualPheDuyet, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
        }

        private void SaveDetail(Guid idDuToan)
        {
            List<VdtDaDuToanNguonVonModel> listNguonVonAdd = DataDuToanNguonVon.Where(x => x.IdDuToanNguonVon == null || x.IdDuToanNguonVon == Guid.Empty && !x.IsDeleted && x.GiaTriPheDuyet > 0).ToList();
            List<VdtDaDuToanChiPhiModel> listChiPhiAdd = DataDuToanChiPhi.Where(x => (x.IdDuToanChiPhi == null || x.IdDuToanChiPhi == Guid.Empty) && !x.IsDeleted).ToList();
            List<VdtDaDuToanNguonVonModel> listNguonVonEdit = DataDuToanNguonVon.Where(x => x.IsModified && x.IdDuToanNguonVon != null && x.IdDuToanNguonVon != Guid.Empty && !x.IsDeleted).ToList();
            List<VdtDaDuToanChiPhiModel> listChiPhiEdit = DataDuToanChiPhi.Where(x => x.IsModified && x.IdDuToanChiPhi != null && x.IdDuToanChiPhi != Guid.Empty && !x.IsDeleted).ToList();
            List<VdtDaDuToanNguonVonModel> listNguonVonDelete = DataDuToanNguonVon.Where(x => x.IsDeleted && x.IdDuToanNguonVon != null && x.IdDuToanNguonVon != Guid.Empty).ToList();
            List<VdtDaDuToanChiPhiModel> listChiPhiDelete = DataDuToanChiPhi.Where(x => x.IsDeleted && x.IdDuToanChiPhi != null && x.IdDuToanChiPhi != Guid.Empty).ToList();
            // Thêm mới, sửa vào các bảng chi tiết
            if (listNguonVonAdd.Count > 0)
            {
                AddNguonVonDetail(listNguonVonAdd, idDuToan);
            }
            if (listChiPhiAdd != null && listChiPhiAdd.Count > 0)
            {
                AddChiPhiDetail(listChiPhiAdd, idDuToan);
            }
            if (listChiPhiEdit != null && listChiPhiEdit.Count > 0)
            {
                UpdateChiPhiDetail(listChiPhiEdit);
            }
            if (listNguonVonEdit != null && listNguonVonEdit.Count > 0)
            {
                UpdateNguonVonDetail(listNguonVonEdit);
            }
            if (listNguonVonDelete.Count > 0)
            {
                DeleteNguonVon(listNguonVonDelete);
            }
            if (listChiPhiDelete.Count > 0)
            {
                DeleteChiPhi(listChiPhiDelete);
            }

            SaveHangMuc();
        }

        private void SaveHangMuc()
        {
            List<DuToanDetailModel> listDMHangMucAdd = DataDuToanHangMuc.Where(x => x.Id.IsNullOrEmpty() && !x.IsDeleted && !string.IsNullOrEmpty(x.TenHangMuc)).ToList();
            List<DuToanDetailModel> listDuToanHangMucAdd = DataDuToanHangMuc.Where(x => x.Id.IsNullOrEmpty() && !x.IsDeleted && !string.IsNullOrEmpty(x.TenHangMuc)).ToList();
            List<DuToanDetailModel> listDanhMucHangMucEdit = DataDuToanHangMuc.Where(x => !x.IsDeleted && !x.Id.IsNullOrEmpty()).ToList();
            List<DuToanDetailModel> listDuToanHangMucEdit = DataDuToanHangMuc.Where(x => !x.IsDeleted && !x.Id.IsNullOrEmpty()).ToList();
            List<DuToanDetailModel> listDetailDelete = DataDuToanHangMuc.Where(x => !x.IdDuToanHangMuc.IsNullOrEmpty() && x.IsDeleted).ToList();

            //Thêm mới,sửa vào các bảng chi tiết
            if (listDMHangMucAdd.Count > 0)
            {
                AddDanhMucHangMucDetail(listDMHangMucAdd);
            }
            if (listDuToanHangMucAdd.Count > 0)
            {
                AddDuToanHangMucDetail(listDuToanHangMucAdd);
            }
            if (listDanhMucHangMucEdit.Count > 0)
            {
                UpdateDanhMucHangMucDetail(listDanhMucHangMucEdit);
            }
            if (listDuToanHangMucEdit.Count > 0)
            {
                UpdateDuToanHangMucDetail(listDuToanHangMucEdit);
            }

            if (listDetailDelete.Count > 0)
            {
                DeleteHangMuc(listDetailDelete);
            }
        }

        private void AddDanhMucHangMucDetail(List<DuToanDetailModel> listAdd)
        {
            // add hạng mục
            List<VdtDaDuToanDmHangMuc> listDMHangMuc = new List<VdtDaDuToanDmHangMuc>();
            if (listAdd != null && listAdd.Count > 0)
            {
                listDMHangMuc = _mapper.Map<List<VdtDaDuToanDmHangMuc>>(listAdd);
                foreach (var item in listDMHangMuc)
                {
                    VdtDaDuToanDmHangMuc dmHangMuc = _vdtDaDuToanService.FindDuToanDMHangMuc(item.Id);
                    if (dmHangMuc == null)
                    {
                        _vdtDaDuToanService.AddDuToanDanhMucHangMuc(item);
                    }
                }
            }
        }

        private void AddDuToanHangMucDetail(List<DuToanDetailModel> listAdd)
        {
            #region add vào bảng Vdt_da_DuToan_HangMuc
            //List<DuToanDetailModel> listDuToanHangMuc = listAdd.Where(x => !x.IsHangCha).ToList();
            foreach (var item in listAdd)
            {
                item.IIdDuToanId = DuToan.Id;
            }
            List<VdtDaDuToanHangMuc> listDuToanHangMucAdd = new List<VdtDaDuToanHangMuc>();
            listDuToanHangMucAdd = _mapper.Map<List<VdtDaDuToanHangMuc>>(listAdd);
            _vdtDaDuToanService.AddRangeDuToanHangMuc(listDuToanHangMucAdd);

            #endregion
        }

        private void UpdateDanhMucHangMucDetail(List<DuToanDetailModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                VdtDaDuToanDmHangMuc danhMucHangMuc = _vdtDaDuToanService.FindDuToanDMHangMuc(item.IdDuAnHangMuc.Value);
                if (danhMucHangMuc != null)
                {
                    _mapper.Map(item, danhMucHangMuc);
                    _vdtDaDuToanService.UpdateDuToanDanhMucHangMuc(danhMucHangMuc);
                }
            }
        }

        private void UpdateDuToanHangMucDetail(List<DuToanDetailModel> listEdit)
        {
            //List<DuToanDetailModel> listDTHangMuc = listEdit.Where(x => x.IsCPNV.Value).ToList();
            foreach (var item in listEdit)
            {
                VdtDaDuToanHangMuc duToanHangMuc = _vdtDaDuToanService.FindDuToanHangMuc(item.IdDuToanHangMuc.Value);
                if (duToanHangMuc != null)
                {
                    _mapper.Map(item, duToanHangMuc);
                    _vdtDaDuToanService.UpdateDuToanHangMuc(duToanHangMuc);
                }
            }
        }

        private void DeleteHangMuc(List<DuToanDetailModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                if (item.IdDuToanHangMuc.HasValue)
                {
                    _vdtDaDuToanService.DeleteDuToanHangMucDetail(item.IdDuToanHangMuc.Value);
                }
            }
        }

        private void AddChiPhiDetail(List<VdtDaDuToanChiPhiModel> listAdd, Guid idDuToan)
        {
            foreach (var item in listAdd)
            {
                item.IdDuToan = idDuToan;
            }
            // add vào bảng Vdt_Dm_DuAn_ChiPhi
            List<VdtDaDuToanChiPhiModel> listDuanChiPhi = listAdd.Where(x => x.Id.IsNullOrEmpty()).ToList();
            if (listDuanChiPhi != null && listDuanChiPhi.Count > 0)
            {
                List<VdtDmDuAnChiPhi> listDuAnChiPhiAdd = new List<VdtDmDuAnChiPhi>();
                listDuAnChiPhiAdd = _mapper.Map<List<VdtDmDuAnChiPhi>>(listDuanChiPhi);
                _approveProjectService.AddRangeDMDuAnChiPhi(listDuAnChiPhiAdd);
            }

            #region add vào bảng VDt_DA_DuToan_Chiphi
            //List<VdtDaDuToanChiPhiModel> listDuToanChiPhiAdd = listAdd.Where(x => x.IsEditHangMuc).ToList();
            if (listAdd != null && listAdd.Count > 0)
            {
                List<VdtDaDuToanChiPhi> listDuToanChiPhi = new List<VdtDaDuToanChiPhi>();
                listDuToanChiPhi = _mapper.Map<List<VdtDaDuToanChiPhi>>(listAdd);

                _vdtDaDuToanService.AddRangeDuToanChiPhi(listDuToanChiPhi);
            }


            #endregion
        }

        private void AddNguonVonDetail(List<VdtDaDuToanNguonVonModel> listAdd, Guid idDuToan)
        {
            foreach (var item in listAdd)
            {
                item.IdDuToan = idDuToan;
            }
            #region add Nguon von
            List<VdtDaDuToanNguonvon> listDuToanNguonVon = new List<VdtDaDuToanNguonvon>();
            listDuToanNguonVon = _mapper.Map<List<VdtDaDuToanNguonvon>>(listAdd);
            _vdtDaDuToanService.AddRangeDuToanNguonVon(listDuToanNguonVon);

            #endregion
        }

        private void UpdateChiPhiDetail(List<VdtDaDuToanChiPhiModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                // sửa VDT_DM_Duan_Chiphi
                VdtDmDuAnChiPhi duAnChiPhi = _approveProjectService.FindDMDuAnChiPhi(item.IdChiPhiDuAn);
                if (duAnChiPhi != null)
                {
                    duAnChiPhi.STenChiPhi = item.TenChiPhi;
                    duAnChiPhi.IIdChiPhiParent = item.IdChiPhiDuAnParent;
                    _approveProjectService.UpdateVdtDmDuAnChiPhi(duAnChiPhi);
                }


                VdtDaDuToanChiPhi duToanChiPhi = _vdtDaDuToanService.FindDuToanChiPhi(item.IdDuToanChiPhi);
                if (duToanChiPhi != null)
                {
                    duToanChiPhi.FTienPheDuyet = item.GiaTriPheDuyet;
                    _vdtDaDuToanService.UpdateDuToanChiPhi(duToanChiPhi);
                }
            }
        }

        private void UpdateNguonVonDetail(List<VdtDaDuToanNguonVonModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                VdtDaDuToanNguonvon duToanNV = _vdtDaDuToanService.FindDuToanNguonVon(item.IdDuToanNguonVon);
                if (duToanNV != null)
                {
                    _mapper.Map(item, duToanNV);
                    _vdtDaDuToanService.UpdateDuToanNguonVon(duToanNV);
                }
            }
        }

        private void DeleteNguonVon(List<VdtDaDuToanNguonVonModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _vdtDaDuToanService.DeleteDuToanNguonVon(item.IdDuToanNguonVon);
            }
        }

        private void DeleteChiPhi(List<VdtDaDuToanChiPhiModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _vdtDaDuToanService.DeleteDuToanChiPhi(item.IdDuToanChiPhi);
            }
        }

        public void OnShowDetailDuToan()
        {
            if (SelectedChiPhi != null)
            {
                if (HangMucPhanChiaSaved != null)
                    TongDuToanDieuChinhDetailViewModel.HangMucPhanChiaSaved = HangMucPhanChiaSaved.Where(n => n.IdDuAnChiPhi == SelectedChiPhi.IdChiPhiDuAn).ToList();
                TongDuToanDieuChinhDetailViewModel.DataChiPhiModel = SelectedChiPhi;
                TongDuToanDieuChinhDetailViewModel.Model = DuToan;
                TongDuToanDieuChinhDetailViewModel.DutoanParentId = Model.Id;
                TongDuToanDieuChinhDetailViewModel.IsNotViewDetail = IsNotViewDetail;
                TongDuToanDieuChinhDetailViewModel.Items = GetListHangMucDetailByChiPhi(SelectedChiPhi.IdChiPhiDuAn.Value);
                TongDuToanDieuChinhDetailViewModel.Init();
                TongDuToanDieuChinhDetailViewModel.SavedAction = obj => this.LoadDataQDDauTuHangMucSave();
                TongDuToanDieuChinhDetailViewModel.ShowDialog();
            }
        }

        public void LoadDataQDDauTuHangMucSave()
        {
            DataDuToanHangMucByChiPhi = TongDuToanDieuChinhDetailViewModel.Items;
            var lstHangMucPhanChia = TongDuToanDieuChinhDetailViewModel.HangMucPhanChiaSaved;
            if (HangMucPhanChiaSaved == null)
            {
                HangMucPhanChiaSaved = new List<DuToanDetailModel>();
            }
            HangMucPhanChiaSaved = HangMucPhanChiaSaved.Where(n => n.IdDuAnChiPhi == SelectedChiPhi.IdChiPhiDuAn).ToList();
            HangMucPhanChiaSaved.AddRange(lstHangMucPhanChia);

            if (DataDuToanHangMucByChiPhi.Count > 0)
            {
                SelectedChiPhi.IsEditGiaTriChiPhi = false;
                var giaTriChiPhi = DataDuToanHangMucByChiPhi.Where(x => x.HangMucParentId == null && !x.IsDeleted).Sum(y => y.GiaTriPheDuyet);

                if (giaTriChiPhi != null)
                {
                    SelectedChiPhi.GiaTriPheDuyet = giaTriChiPhi.Value;
                }
                DataDuToanHangMuc = DataDuToanHangMuc.Where(x => x.IdDuAnChiPhi != SelectedChiPhi.IdChiPhiDuAn).ToList();
                DataDuToanHangMuc.AddRange(DataDuToanHangMucByChiPhi.ToList());
            }
        }

        public ObservableCollection<DuToanDetailModel> GetListHangMucDetailByChiPhi(Guid chiPhiId)
        {
            var result = new List<DuToanDetailModel>();
            // tìm trong ListHangMucSave có data chưa, nếu chưa có data thì lấy theo list DataQDDauTuHangMuc load từ chủ trương đầu tư
            List<DuToanDetailModel> listHangMucByChiPhi = DataDuToanHangMuc.Where(x => x.IdDuAnChiPhi == chiPhiId).ToList();
            if (listHangMucByChiPhi.Count < 1 && (Model.Id == null || Model.Id == Guid.Empty))
            {
                result = DataDuToanHangMuc.Where(x => x.IdDuAnChiPhi == chiPhiId).ToList();
            }

            if (listHangMucByChiPhi.Count > 0)
            {
                result = DataDuToanHangMuc.Where(x => x.IdDuAnChiPhi == chiPhiId).ToList();
            }

            return new ObservableCollection<DuToanDetailModel>(result);
        }

        private void TaoMaOrders(List<VdtDaDuToanChiPhiModel> data)
        {
            int currentIdThuTu = 0;
            int index = 0;
            foreach (var item in data)
            {
                if (item.IThuTu == currentIdThuTu && !item.IsHangCha)
                {
                    index++;
                    item.SMaOrder = $"{item.IThuTu}_{index}";
                }
                else
                {
                    item.SMaOrder = $"{item.IThuTu}";
                    index = 0;
                }

                currentIdThuTu = item.IThuTu;
            }
        }

    }
}
