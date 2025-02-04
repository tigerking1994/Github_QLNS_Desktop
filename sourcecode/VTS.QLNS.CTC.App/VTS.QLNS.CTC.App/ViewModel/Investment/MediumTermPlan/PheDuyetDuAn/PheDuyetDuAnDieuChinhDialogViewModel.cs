using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;
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

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PheDuyetDuAn
{
    public class PheDuyetDuAnDieuChinhDialogViewModel : DialogAttachmentViewModelBase<ApproveProjectModel>
    {
        #region Private
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private readonly IVdtDaChuTruongDauTuService _vdtDaChuTruongDauTuService;
        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_PHE_DUYET_DU_AN_DIALOG_MODIFY;
        //public override string Name => "Điều chỉnh";
        public override string Title => "Quản lý phê duyệt dự án";
        //public override string Description => "Điều chỉnh thông tin phê duyệt dự án";
        public override Type ContentType => typeof(View.Investment.MediumTermPlan.PheDuyetDuAn.PheDuyetDuAnDieuChinhDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_PHEDUYET_DUAN;
        private VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PheDuyetDuAn.PheDuyetDuAnDieuChinhDetail view;

        private ApproveProjectModel _qdDauTu;
        public ApproveProjectModel QDDauTu
        {
            get => _qdDauTu;
            set => SetProperty(ref _qdDauTu, value);
        }

        private string _sHeaderNguonVon;
        public string SHeaderNguonVon
        {
            get => _sHeaderNguonVon;
            set => SetProperty(ref _sHeaderNguonVon, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiQuyetDinh;
        public ObservableCollection<ComboboxItem> ItemsLoaiQuyetDinh
        {
            get => _itemsLoaiQuyetDinh;
            set => SetProperty(ref _itemsLoaiQuyetDinh, value);
        }

        private ComboboxItem _selectedLoaiQuyetDinh;
        public ComboboxItem SelectedLoaiQuyetDinh
        {
            get => _selectedLoaiQuyetDinh;
            set
            {
                if (SetProperty(ref _selectedLoaiQuyetDinh, value))
                {
                    if (value != null && value.ValueItem == ((int)LOAI_QD_PDDA.Type.BC_KINH_TE_KY_THUAT).ToString())
                    {
                        SHeaderNguonVon = "Giá trị phê duyệt BCKTKT";
                    }
                    else
                    {
                        SHeaderNguonVon = "Giá trị phê duyệt PDDA";
                    }
                }
                OnPropertyChanged(nameof(SHeaderNguonVon));
            }
        }
        private ObservableCollection<VdtDaQddtChiPhiModel> _dataQDDauTuChiPhi;
        public ObservableCollection<VdtDaQddtChiPhiModel> DataQDDauTuChiPhi
        {
            get => _dataQDDauTuChiPhi;
            set 
            {
                TaoMaOrders(value.ToList());
                SetProperty(ref _dataQDDauTuChiPhi, value);
                OnPropertyChanged(nameof(DataQDDauTuChiPhi));
            }
        }

        private List<VdtDaQddtChiPhiModel> _dataQDDauTuChiPhiPheDuyet;
        public List<VdtDaQddtChiPhiModel> DataQDDauTuChiPhiPheDuyet
        {
            get => _dataQDDauTuChiPhiPheDuyet;
            set => SetProperty(ref _dataQDDauTuChiPhiPheDuyet, value);
        }

        private ObservableCollection<VdtDaQddtNguonVonModel> _dataQDDauTuNguonVon;
        public ObservableCollection<VdtDaQddtNguonVonModel> DataQDDauTuNguonVon
        {
            get => _dataQDDauTuNguonVon;
            set => SetProperty(ref _dataQDDauTuNguonVon, value);
        }

        private void TaoMaOrders(List<VdtDaQddtChiPhiModel> data)
        {
            int currentIdThuTu = 0;
            int index = 0;
            foreach (var item in data)
            {
                if(item.IThuTu == currentIdThuTu && !item.IsHangCha)
                {
                    index++;
                    item.SMaOrder = $"{item.IThuTu}_{index}";
                } else
                {
                    item.SMaOrder = $"{item.IThuTu}";
                    index = 0;
                }

                currentIdThuTu = item.IThuTu;
            }
        }

        private List<ComboboxItem> _dataNguonVon;
        public List<ComboboxItem> DataNguonVon
        {
            get => _dataNguonVon;
            set => SetProperty(ref _dataNguonVon, value);
        }

        private List<ComboboxItem> _dataLoai;
        public List<ComboboxItem> DataLoai
        {
            get => _dataLoai;
            set => SetProperty(ref _dataLoai, value);
        }

        private VdtDaQddtChiPhiModel _selectedChiPhi;
        public VdtDaQddtChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set
            {
                SetProperty(ref _selectedChiPhi, value);
            }
        }

        private VdtDaQddtNguonVonModel _selectedNguonVon;
        public VdtDaQddtNguonVonModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set
            {
                SetProperty(ref _selectedNguonVon, value);
            }
        }

        private ObservableCollection<ComboboxItem> _hinhThucQLItems;
        public ObservableCollection<ComboboxItem> HinhThucQLItems
        {
            get => _hinhThucQLItems;
            set => SetProperty(ref _hinhThucQLItems, value);
        }

        private ComboboxItem _selectedHinhThucQL;
        public ComboboxItem SelectedHinhThucQL
        {
            get => _selectedHinhThucQL;
            set => SetProperty(ref _selectedHinhThucQL, value);
        }

        private List<ApproveProjectDetailModel> _dataQDDTHangMuc;
        public List<ApproveProjectDetailModel> DataQDDTHangMuc
        {
            get => _dataQDDTHangMuc;
            set => SetProperty(ref _dataQDDTHangMuc, value);
        }

        private ObservableCollection<ApproveProjectDetailModel> _dataQDDTHangMucByChiPhi;
        public ObservableCollection<ApproveProjectDetailModel> DataQDDTHangMucByChiPhi
        {
            get => _dataQDDTHangMucByChiPhi;
            set => SetProperty(ref _dataQDDTHangMucByChiPhi, value);
        }

        private double? _conLai;
        public double? ConLai
        {
            get => _conLai;
            set => SetProperty(ref _conLai, value);
        }

        public bool IsEdit => !IsAddDieuChinh;

        private bool _isAddDieuChinh;
        public bool IsAddDieuChinh
        {
            get => _isAddDieuChinh;
            set => SetProperty(ref _isAddDieuChinh, value);
        }

        private bool _isNotViewDetail;
        public bool IsNotViewDetail
        {
            get => _isNotViewDetail;
            set => SetProperty(ref _isNotViewDetail, value);
        }

        public bool IsEditable => (QDDauTu.BActive == true || IsAddDieuChinh) && IsNotViewDetail;
        public bool IsReadOnly => !((QDDauTu.BActive == true || IsAddDieuChinh) && IsNotViewDetail);

        public Dictionary<Guid?, Guid> refDictionary { get; set; }

        #region RelayCommand
        public RelayCommand AddChiPhiDetailCommand { get; }
        public RelayCommand AddChiPhiChildDetailCommand { get; }
        public RelayCommand AddNguonVonDetailCommand { get; }
        public RelayCommand DeleteNguonVonDetailCommand { get; }
        public RelayCommand DeleteChiPhiDetailCommand { get; }
        public RelayCommand ShowHangMucDetailCommand { get; }
        #endregion

        public PheDuyetDuAnDieuChinhDetailViewModel PheDuyetDuAnDieuChinhDetailViewModel { get; }

        public PheDuyetDuAnDieuChinhDialogViewModel(
            IApproveProjectService approveProjectService,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IProjectManagerService projectManagerService,
            IVdtDaDuToanService vdtDaDuToanService,
            IVdtDaChuTruongDauTuService vdtDaChuTruongDauTuService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            PheDuyetDuAnDieuChinhDetailViewModel pheDuyetDuAnDetailViewModel)
            : base(mapper, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _approveProjectService = approveProjectService;
            _projectManagerService = projectManagerService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _vdtDaChuTruongDauTuService = vdtDaChuTruongDauTuService;

            PheDuyetDuAnDieuChinhDetailViewModel = pheDuyetDuAnDetailViewModel;

            AddChiPhiDetailCommand = new RelayCommand(obj => OnAddChiPhi());
            AddChiPhiChildDetailCommand = new RelayCommand(obj => OnAddChiPhiChild());
            AddNguonVonDetailCommand = new RelayCommand(obj => OnAddNguonVon());
            DeleteNguonVonDetailCommand = new RelayCommand(obj => OnDeleteNguonVon());
            DeleteChiPhiDetailCommand = new RelayCommand(obj => OnDeleteChiPhi());
            ShowHangMucDetailCommand = new RelayCommand(obj => OnShowDetailApproveProject());
        }

        public override void Init()
        {
            LoadLoaiQuyetDinh();
            LoadAttach();
            LoadNguonVon();
            LoadHinhThucQL();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            BIsReadOnly = !IsNotViewDetail;
            if (IsAddDieuChinh)
            {
                Description = "Điều chỉnh thông tin phê duyệt dự án";
                Name = "Điều chỉnh";
                QDDauTu = ObjectCopier.Clone(base.Model);
                LoadDataNguonVonDieuChinhByQDDauTuIdAdd();
                LoadDataQDDauTuChiPhiAdd();
                LoadDataQDDauTuHangMucByQDDTId();
                QDDauTu.SSoQuyetDinh = null;
                QDDauTu.Id = Guid.Empty;
                QDDauTu.DNgayQuyetDinh = DateTime.Now;
                if (HinhThucQLItems != null && QDDauTu.IIdHinhThucQuanLyId.HasValue)
                {
                    SelectedHinhThucQL = HinhThucQLItems.Where(x => x.ValueItem == QDDauTu.IIdHinhThucQuanLyId.ToString()).FirstOrDefault();
                }
                OnPropertyChanged(nameof(QDDauTu));
            }
            else
            {

                if (IsNotViewDetail == false)
                {
                    Name = "CHI TIẾT";
                    Description = "Xem chi tiết thông tin phê duyệt dự án";
                }
                else
                {
                    Name = "Cập nhật";
                    Description = "Cập nhật thông tin điều chỉnh phê duyệt dự án";
                }
                LoadDataNguonVonDieuChinhByQDDauTuIdUpdate();
                LoadDataQDDauTuChiPhiUpdate();
                LoadDataQDDauTuHangMucByQDDTIdUpdate();
                if (HinhThucQLItems != null && QDDauTu.IIdHinhThucQuanLyId.HasValue)
                {
                    SelectedHinhThucQL = HinhThucQLItems.Where(x => x.ValueItem == QDDauTu.IIdHinhThucQuanLyId.ToString()).FirstOrDefault();
                }
            }
            DataQDDauTuChiPhiPheDuyet = DataQDDauTuChiPhi.Where(x => x.IdChiPhiDuAnParent == null).ToList();
            double fSumNguonVon = 0, fSumChiPhi = 0;
            if (DataQDDauTuNguonVon != null)
                fSumNguonVon = DataQDDauTuNguonVon.Where(n => !n.IsDeleted).Sum(n => n.GiaTriPheDuyet);
            if (DataQDDauTuChiPhi != null)
                fSumChiPhi = DataQDDauTuChiPhi.Where(n => !n.IsDeleted && !n.IdChiPhiDuAnParent.HasValue).Sum(n => n.GiaTriPheDuyet);
            ConLai = fSumNguonVon - fSumChiPhi;
            OnPropertyChanged(nameof(ConLai));
        }

        private void LoadHinhThucQL()
        {
            IEnumerable<VdtDmHinhThucQuanLy> listHinhThucQL = _approveProjectService.GetAllHinhThucQuanLy();
            HinhThucQLItems = _mapper.Map<ObservableCollection<ComboboxItem>>(listHinhThucQL);
            OnPropertyChanged(nameof(HinhThucQLItems));
        }

        private void LoadNguonVon()
        {
            IEnumerable<NsNguonNganSach> listNguonVon = _approveProjectService.GetAllNguonNS();
            DataNguonVon = _mapper.Map<List<ComboboxItem>>(listNguonVon);
        }

        private void LoadDataNguonVonDieuChinhByQDDauTuIdAdd()
        {
            List<VdtDaQDNguonVonQuery> listQdNguonVonQuery = _approveProjectService.FindListQDDauTuNguonVonDieuChinh(Model.Id).ToList();
            DataQDDauTuNguonVon = _mapper.Map<ObservableCollection<VdtDaQddtNguonVonModel>>(listQdNguonVonQuery);
            foreach (var item in DataQDDauTuNguonVon)
            {
                item.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            }
        }

        private void LoadDataNguonVonDieuChinhByQDDauTuIdUpdate()
        {
            List<VdtDaQDNguonVonQuery> listQdNguonVonQuery = _approveProjectService.FindListQDDauTuNguonVonDieuChinhUpdate(QDDauTu.Id).ToList();
            DataQDDauTuNguonVon = _mapper.Map<ObservableCollection<VdtDaQddtNguonVonModel>>(listQdNguonVonQuery);
            foreach (var item in DataQDDauTuNguonVon)
            {
                item.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            }
        }

        private void LoadDataQDDauTuChiPhiAdd()
        {
            List<VdtDaQddtChiPhiQuery> listQdChiPhiQuery = _approveProjectService.FindListQDDauTuChiPhiDieuChinhDefault(Model.Id).ToList();

            // Renew id điều chỉnh
            refDictionary = listQdChiPhiQuery.ToDictionary(x => x.IdChiPhiDuAn, x => Guid.NewGuid());
            listQdChiPhiQuery = listQdChiPhiQuery.Select(x =>
            {
                x.IdChiPhiDuAn = refDictionary[x.IdChiPhiDuAn];
                if (x.IdChiPhiDuAnParent != null)
                {
                    x.IdChiPhiDuAnParent = refDictionary[x.IdChiPhiDuAnParent];
                }
                return x;
            }).ToList();

            DataQDDauTuChiPhi = _mapper.Map<ObservableCollection<VdtDaQddtChiPhiModel>>(listQdChiPhiQuery);
            UpdateListChiPhiCanEdit();
            foreach (var item in DataQDDauTuChiPhi)
            {
                item.PropertyChanged += DetailChiPhiModel_PropertyChanged;
            }
        }

        private void LoadDataQDDauTuChiPhiUpdate()
        {
            List<VdtDaQddtChiPhiQuery> listQdChiPhiQuery = _approveProjectService.FindListQDDauTuChiPhiDieuChinhUpdate(QDDauTu.Id).ToList();
            DataQDDauTuChiPhi = _mapper.Map<ObservableCollection<VdtDaQddtChiPhiModel>>(listQdChiPhiQuery);
            UpdateListChiPhiCanEdit();
            foreach (var item in DataQDDauTuChiPhi)
            {
                item.PropertyChanged += DetailChiPhiModel_PropertyChanged;
            }
        }

        public void LoadDataQDDauTuHangMucByQDDTId()
        {
            List<ApproveProjectDetailQuery> listData = new List<ApproveProjectDetailQuery>();
            listData = _approveProjectService.FindListHangMucDieuChinhByQDDauTuAdd(Model.Id).ToList();

            // Renew id chi phí trong hạng mục
            listData = listData.Select(x =>
            {
                x.IdDuAnChiPhi = refDictionary[x.IdDuAnChiPhi];
                return x;
            }).ToList();

            // renew id hạng mục
            var refDictionaryDM = new Dictionary<Guid, Guid>();//listData.ToDictionary(x => x.IdDuAnHangMuc, x => Guid.NewGuid());
            if (listData.Any())
            {
                foreach (var item in listData)
                {
                    if (!refDictionaryDM.ContainsKey(item.IdDuAnHangMuc.Value))
                        refDictionaryDM.Add(item.IdDuAnHangMuc.Value, Guid.NewGuid());
                }
            }
            listData = listData.Select(x =>
            {
                x.IdDuAnHangMuc = refDictionaryDM[x.IdDuAnHangMuc.Value];
                if (x.HangMucParentId.HasValue)
                {
                    x.HangMucParentId = refDictionaryDM[x.HangMucParentId.Value];
                }

                return x;
            }).ToList();

            DataQDDTHangMuc = _mapper.Map<List<Model.ApproveProjectDetailModel>>(listData);
            UpdateListChiPhiCanEditGiaTriChiPhi();
        }

        public void LoadDataQDDauTuHangMucByQDDTIdUpdate()
        {
            var listData = _approveProjectService.FindListHangMucDieuChinhByQDDauTuUpdate(QDDauTu.Id).ToList();
            DataQDDTHangMuc = _mapper.Map<List<Model.ApproveProjectDetailModel>>(listData);
            UpdateListChiPhiCanEditGiaTriChiPhi();
        }

        private void UpdateListChiPhiCanEditGiaTriChiPhi()
        {
            foreach (var item in DataQDDauTuChiPhi.Where(x => x.IsEditHangMuc && !x.IsDeleted))
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

        private void UpdateListChiPhiCanEdit()
        {
            foreach (var item in DataQDDauTuChiPhi.Where(x => x.IsHangCha && !x.IsDeleted))
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

            TaoMaOrders(DataQDDauTuChiPhi.ToList());
            OnPropertyChanged(nameof(DataQDDauTuChiPhi));
        }

        public List<VdtDaQddtChiPhiModel> FindListChildChiPhi(Guid parentId)
        {
            List<VdtDaQddtChiPhiModel> inner = new List<VdtDaQddtChiPhiModel>();
            foreach (var t in DataQDDauTuChiPhi.Where(item => item.IdChiPhiDuAnParent == parentId && !item.IsDeleted))
            {
                inner.Add(t);
                inner = inner.Union(FindListChildChiPhi(t.Id)).ToList();
            }

            return inner;
        }

        private void DetailNguonVonModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtDaQddtNguonVonModel objectSender = (VdtDaQddtNguonVonModel)sender;

            if (args.PropertyName == nameof(VdtDaQddtNguonVonModel.GiaTriPheDuyet))
            {
                List<VdtDaQddtNguonVonModel> listNguonVon = DataQDDauTuNguonVon.Where(x => !x.IsDeleted && (x.IdNguonVon != 0) && x.GiaTriPheDuyet > 0).ToList();
                var tongGiaTriPheDuyet = DataQDDauTuNguonVon.Sum(x => x.GiaTriPheDuyet);
                //QDDauTu.FTongMucDauTuPheDuyet = tongGiaTriPheDuyet;

                CalculateConLaiNguonVon();
            }

            if (args.PropertyName == nameof(VdtDaQddtNguonVonModel.IdNguonVon))
            {
                if (objectSender.IdNguonVon != 0)
                {
                    if (CheckDuplicateNguonVon(objectSender.IdNguonVon))
                    {
                        System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SelectedNguonVon.IdNguonVon = 0;
                        return;
                    }
                }
            }

            if (args.PropertyName == nameof(VdtDaQddtNguonVonModel.GiaTriPheDuyet))
            {
                objectSender.GiaTriDieuChinh = objectSender.GiaTriPheDuyet - objectSender.GiaTriTruocDieuChinh;
            }

            objectSender.IsModified = true;
            OnPropertyChanged(nameof(DataQDDauTuNguonVon));
            OnPropertyChanged(nameof(QDDauTu));
        }

        private void OnAddChiPhi()
        {
            if (SelectedChiPhi == null || SelectedChiPhi.IdChiPhiDuAnParent == null || SelectedChiPhi.IsDeleted)
            {
                return;
            }
            VdtDaQddtChiPhiModel targetItem = new VdtDaQddtChiPhiModel();

            int currentRow = -1;
            if (DataQDDauTuChiPhi != null && DataQDDauTuChiPhi.Count > 0)
            {
                currentRow = 0;
                if (SelectedChiPhi != null)
                {
                    currentRow = DataQDDauTuChiPhi.IndexOf(SelectedChiPhi);
                }

                VdtDaQddtChiPhiModel sourceItem = DataQDDauTuChiPhi.ElementAt(currentRow);
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.GiaTriPheDuyet = 0;
                targetItem.GiaTriTruocDieuChinh = 0;
                targetItem.Id = Guid.Empty;
                targetItem.IdQDChiPhi = Guid.Empty;
                targetItem.IsHangCha = false;
                targetItem.TenChiPhi = string.Empty;
                targetItem.IdChiPhiDuAn = Guid.NewGuid();

            }
            targetItem.PropertyChanged += DetailChiPhiModel_PropertyChanged;
            DataQDDauTuChiPhi.Insert(currentRow + 1, targetItem);
            UpdateListChiPhiCanEdit();
            OnPropertyChanged(nameof(DataQDDauTuChiPhi));
        }

        private void OnAddChiPhiChild()
        {
            VdtDaQddtChiPhiModel targetItem = new VdtDaQddtChiPhiModel()
            {
                Id = Guid.Empty,
                IdQDChiPhi = Guid.Empty
            };
            int currentRow = -1;
            if (DataQDDauTuChiPhi != null && DataQDDauTuChiPhi.Count > 0)
            {
                currentRow = 0;
                if (SelectedChiPhi != null)
                {
                    if (SelectedChiPhi.IsDeleted)
                    {
                        return;
                    }
                    if (!CheckChiPhiCanDelete(SelectedChiPhi.IdChiPhiDuAn.Value))
                    {
                        System.Windows.Forms.MessageBox.Show(Resources.MsgErrAddChiPhiChild, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    currentRow = DataQDDauTuChiPhi.IndexOf(SelectedChiPhi);
                }
                VdtDaQddtChiPhiModel sourceItem = DataQDDauTuChiPhi.ElementAt(currentRow);
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.GiaTriPheDuyet = 0;
                targetItem.GiaTriTruocDieuChinh = 0;
                targetItem.IsHangCha = false;
                targetItem.TenChiPhi = string.Empty;
                targetItem.Id = Guid.Empty;
                targetItem.IdQDChiPhi = Guid.Empty;
                targetItem.IdChiPhiDuAn = Guid.NewGuid();
                targetItem.IdChiPhiDuAnParent = sourceItem.IdChiPhiDuAn;
                targetItem.IsEditHangMuc = true;
                targetItem.IsEditGiaTriChiPhi = true;
                sourceItem.IsHangCha = true;
                sourceItem.GiaTriPheDuyet = 0;
                sourceItem.IsModified = true;
                sourceItem.IsEditHangMuc = false;
            }

            targetItem.PropertyChanged += DetailChiPhiModel_PropertyChanged;
            DataQDDauTuChiPhi.Insert(currentRow + 1, targetItem);
            UpdateListChiPhiCanEdit();
            OnPropertyChanged(nameof(DataQDDauTuChiPhi));
        }

        public bool CheckChiPhiCanDelete(Guid duanChiPhiId)
        {
            var listHangMucByChiPhi = DataQDDTHangMuc.Where(x => x.IdDuAnChiPhi == duanChiPhiId && !x.IsDeleted).ToList();
            if (listHangMucByChiPhi.Count > 0)
            {
                return false;
            }
            return true;
        }

        protected void OnDeleteNguonVon()
        {
            if (DataQDDauTuNguonVon != null && DataQDDauTuNguonVon.Count > 0 && SelectedNguonVon != null)
            {
                SelectedNguonVon.IsDeleted = !SelectedNguonVon.IsDeleted;
                CalculateConLaiNguonVon();
                OnPropertyChanged(nameof(DataQDDauTuNguonVon));
            }
        }

        protected void OnDeleteChiPhi()
        {
            if (SelectedChiPhi == null || SelectedChiPhi.IdChiPhiDuAnParent == null)
            {
                if (SelectedChiPhi != null && SelectedChiPhi.IdChiPhiDuAnParent == null)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgVocherParent, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (!CheckChiPhiCanDelete(SelectedChiPhi.IdChiPhiDuAn.Value))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgDeleteChiPhiHasHangMuc, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DataQDDauTuChiPhi.Count > 0 && SelectedChiPhi != null)
            {
                if (CheckChiPhiCoHangMuc(SelectedChiPhi.IdChiPhiDuAn.Value))
                {
                    return;
                }
                var listChiPhi = FindListChildChiPhi(SelectedChiPhi.Id);
                listChiPhi.Add(SelectedChiPhi);
                foreach (var item in listChiPhi)
                {
                    item.IsDeleted = !SelectedChiPhi.IsDeleted;
                }
                CalculateDataChiPhi();
                CalculateConLaiNguonVon();
                UpdateListChiPhiCanEdit();
                // SelectedChiPhi.IsDeleted = !SelectedChiPhi.IsDeleted;
                OnPropertyChanged(nameof(DataQDDauTuChiPhi));
            }
        }

        private bool CheckChiPhiCoHangMuc(Guid chiphiId)
        {
            return _approveProjectService.CheckChiPhiCoHangMuc(chiphiId);

        }

        private void DetailChiPhiModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtDaQddtChiPhiModel objectSender = (VdtDaQddtChiPhiModel)sender;

            if (args.PropertyName == nameof(VdtDaQddtChiPhiModel.GiaTriPheDuyet))
            {
                objectSender.GiaTriDieuChinh = objectSender.GiaTriPheDuyet - objectSender.GiaTriTruocDieuChinh;
                CalculateDataChiPhi();
                CalculateConLaiNguonVon();
            }

            objectSender.IsModified = true;
        }

        private void CalculateConLaiNguonVon()
        {
            double tongNguonVon = 0;
            double tongChiPhi = 0;
            if (DataQDDauTuNguonVon != null && DataQDDauTuNguonVon.Count > 0)
            {
                tongNguonVon = DataQDDauTuNguonVon.Where(x => !x.IsDeleted).Sum(x => x.GiaTriPheDuyet);
                QDDauTu.FTongMucDauTuPheDuyet = tongNguonVon;
                OnPropertyChanged(nameof(QDDauTu));
            }

            if (DataQDDauTuChiPhi != null && DataQDDauTuChiPhi.Count > 0)
            {
                var listChiPhiCha = DataQDDauTuChiPhi.Where(x => x.IdChiPhiDuAnParent == null).ToList();
                tongChiPhi = listChiPhiCha.Sum(x => x.GiaTriPheDuyet);
            }
            ConLai = tongNguonVon - tongChiPhi;
        }

        private bool CheckDuplicateNguonVon(int idNguonVon)
        {
            List<VdtDaQddtNguonVonModel> listNguonVon = DataQDDauTuNguonVon.Where(x => x.IdNguonVon == idNguonVon && !x.IsDeleted).ToList();
            if (listNguonVon != null && listNguonVon.Count > 1)
            {
                return true;
            }
            return false;
        }

        private void OnAddNguonVon()
        {
            VdtDaQddtNguonVonModel targetItem = new VdtDaQddtNguonVonModel()
            {
                Id = Guid.NewGuid(),
                IdQDNguonVon = Guid.Empty,
                IdNguonVon = 0,
                GiaTriPheDuyet = 0
            };
            int currentRow = -1;
            if (DataQDDauTuNguonVon != null && DataQDDauTuNguonVon.Count > 0)
            {
                currentRow = 0;
                if (SelectedNguonVon != null)
                {
                    currentRow = DataQDDauTuNguonVon.IndexOf(SelectedNguonVon);
                }
                else
                {
                    currentRow = DataQDDauTuNguonVon.IndexOf(DataQDDauTuNguonVon.Last());
                }

                VdtDaQddtNguonVonModel sourceItem = DataQDDauTuNguonVon.ElementAt(currentRow);
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.Id = Guid.NewGuid();
                targetItem.IdQDNguonVon = Guid.Empty;
                targetItem.IdNguonVon = 0;
                targetItem.GiaTriPheDuyet = 0;
                targetItem.GiaTriDieuChinh = 0;
                targetItem.GiaTriTruocDieuChinh = 0;
                targetItem.GiaTriPheDuyetCTDT = 0;
                targetItem.IsDeleted = false;
            }
            targetItem.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            DataQDDauTuNguonVon.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(DataQDDauTuNguonVon));
            CalculateConLaiNguonVon();
        }

        private void CalculateDataChiPhi()
        {
            if (DataQDDauTuChiPhi == null) return;
            foreach (var item in DataQDDauTuChiPhi.Where(n => !n.IdChiPhiDuAnParent.HasValue))
            {
                CalculateParent(item);
            }
        }

        private void CalculateParent(VdtDaQddtChiPhiModel parentItem)
        {
            int childRow = 0;
            foreach (var item in DataQDDauTuChiPhi.Where(n => n.IdChiPhiDuAnParent.HasValue && n.IdChiPhiDuAnParent == parentItem.IdChiPhiDuAn))
            {
                CalculateParent(item);
                childRow++;
            }
            if (childRow != 0)
                parentItem.GiaTriPheDuyet = DataQDDauTuChiPhi.Where(n => n.IdChiPhiDuAnParent.HasValue && n.IdChiPhiDuAnParent == parentItem.IdChiPhiDuAn && !n.IsDeleted).Sum(n => n.GiaTriPheDuyet);
        }

        public override void OnSave()
        {
            if (!ValiDateData() || !ValidateDataDetail())
            {
                return;
            }

            if (DataQDDTHangMuc == null || DataQDDTHangMuc.Count == 0)
            {
                if (MessageBoxHelper.Confirm(Resources.MsgConfirmNotHaveHangMucSave) == MessageBoxResult.No) return;
            }

            VdtDaQddauTu entity;
            int result;
            if (SelectedHinhThucQL != null)
            {
                QDDauTu.IIdHinhThucQuanLyId = Guid.Parse(SelectedHinhThucQL.ValueItem);
            }
            if (QDDauTu.Id != null && QDDauTu.Id != Guid.Empty)
            {
                // Update
                entity = _approveProjectService.FindById(QDDauTu.Id);
                bool? bIsGoc = entity.BIsGoc.Clone();
                QDDauTu.FTongMucDauTuPheDuyet = entity.FTongMucDauTuPheDuyet;
                _mapper.Map(QDDauTu, entity);
                entity.BIsGoc = bIsGoc ?? false;
                entity.ILoaiQuyetDinh = int.Parse(SelectedLoaiQuyetDinh.ValueItem);
                entity.DDateUpdate = DateTime.Now;
                entity.SUserUpdate = _sessionService.Current.Principal;
                entity.SSoBuocThietKe = Model.SSoBuocThietKe;
                result = _approveProjectService.Update(entity);
                //IsAdd = false;
            }
            else
            {
                // Add VdtDaQddauTu
                entity = _mapper.Map<VdtDaQddauTu>(QDDauTu);
                entity.BActive = true;
                entity.BIsGoc = false;
                entity.DDateCreate = DateTime.Now;
                entity.ILoaiQuyetDinh = int.Parse(SelectedLoaiQuyetDinh.ValueItem);
                entity.SUserCreate = _sessionService.Current.Principal;
                Model.SUserCreate = entity.SUserCreate;
                entity.IIdParentId = Model.Id;
                entity.SSoBuocThietKe = Model.SSoBuocThietKe;
                result = _approveProjectService.Add(entity);
                QDDauTu.Id = entity.Id;
                QDDauTu.IIdParentId = entity.IIdParentId;
                // update QDDauTu cha BActive = 0
                VdtDaQddauTu entityParent = _approveProjectService.FindById(Model.Id);
                if (entityParent != null)
                {
                    entityParent.BActive = false;
                    entityParent.DDateUpdate = DateTime.Now;
                    entityParent.SUserUpdate = _sessionService.Current.Principal;
                    _approveProjectService.Update(entityParent);
                }
            }

            UpdateDuAn();

            SaveDataDetail();

            // Save attach file
            base.SaveAttachment(QDDauTu.Id);

            QDDauTu.Id = entity.Id;
            IsAddDieuChinh = false;

            MessageBoxHelper.Info(Resources.MsgSaveDone);
            SavedAction?.Invoke(_mapper.Map<ApproveProjectModel>(entity));
            LoadData();
        }

        private void UpdateDuAn()
        {
            // Update VDT_DA_DuAn
            VdtDaDuAn entityDuAn = _approveProjectService.FindDuAnById(Model.IIdDuAnId.Value);
            _mapper.Map(Model, entityDuAn);

            if (SelectedHinhThucQL != null)
            {
                entityDuAn.IIdHinhThucQuanLyId = Guid.Parse(SelectedHinhThucQL.ValueItem);
            }
            //entityDuAn.IIdChuDauTuId = Model.IIdChuDauTuId;
            entityDuAn.SKhoiCong = QDDauTu.SKhoiCong;
            entityDuAn.SKetThuc = QDDauTu.SKetThuc;
            entityDuAn.SDiaDiem = QDDauTu.SDiaDiem;
            entityDuAn.SUserUpdate = _sessionService.Current.Principal;
            entityDuAn.DDateUpdate = DateTime.Now;
            _approveProjectService.UpdateVdtDuAn(entityDuAn);
        }

        private bool ValiDateData()
        {
            List<string> lstError = new List<string>();
            if (string.IsNullOrEmpty(QDDauTu.SSoQuyetDinh))
            {
                lstError.Add(Resources.MsgCheckSoQD);
            }
            else
            {
                if (QDDauTu.Id != null && _approveProjectService.CheckDuplicateSoQD(QDDauTu.SSoQuyetDinh, QDDauTu.Id))
                {
                    lstError.Add(Resources.MsgTrungSoQD);
                }
            }
            if (!QDDauTu.DNgayQuyetDinh.HasValue)
            {
                lstError.Add(Resources.MsgCheckNgayPheDuyet);
            }
            if (QDDauTu.DNgayQuyetDinh.HasValue)
            {
                int compareDate = DateTime.Compare(QDDauTu.DNgayQuyetDinh.Value, QDDauTu.DNgayQuyetDinh.Value);
                if (compareDate < 0)
                {
                    lstError.Add(Resources.MsgErrorNgayQDDieuChinhNhoHon);
                }
            }

            if (lstError.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstError), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            return true;
        }

        public bool ValidateDataDetail()
        {
            double fTongChiPhi = 0;
            double fTongNguonVon = 0;

            var listChiPhi = DataQDDauTuChiPhi.Where(x => !x.IsDeleted && x.GiaTriPheDuyet > 0).ToList();
            var listNguonVon = DataQDDauTuNguonVon.Where(x => !x.IsDeleted && x.GiaTriPheDuyet > 0 && x.IdNguonVon != 0).ToList();

            if (listNguonVon == null || listNguonVon.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (listChiPhi != null)
                fTongChiPhi = listChiPhi.Where(x => x.IdChiPhiDuAnParent == null).Sum(x => x.GiaTriPheDuyet);
            if (listNguonVon != null)
                fTongNguonVon = listNguonVon.Sum(x => x.GiaTriPheDuyet);

            if (fTongChiPhi != fTongNguonVon)
            {
                if (MessageBoxHelper.Confirm(Resources.MsgConfirmErrorChiPhiNotEqualNguonVon) == MessageBoxResult.Yes)
                {
                    return true;
                }
                return false;
            }

            if (fTongNguonVon != QDDauTu.FTongMucDauTuPheDuyet)
            {
                System.Windows.Forms.MessageBox.Show(Resources.ErrorNguonVonNotChange, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void SaveDataDetail()
        {
            SaveNguonVon();
            SaveChiPhi();
            SaveHangMuc();
        }

        private void SaveChiPhi()
        {
            var listChiPhiAdd = DataQDDauTuChiPhi.Where(x => !x.IsDeleted && (x.IdQDChiPhi == null || x.IdQDChiPhi == Guid.Empty)).ToList();
            var listChiPhiEdit = DataQDDauTuChiPhi.Where(x => !x.IdQDChiPhi.IsNullOrEmpty() && !x.IsDeleted).ToList();
            List<VdtDaQddtChiPhiModel> listChiPhiDelete = DataQDDauTuChiPhi.Where(x => (x.IsDeleted && x.IdQDChiPhi != null && x.IdQDChiPhi != Guid.Empty)).ToList();

            if (listChiPhiAdd.Count > 0)
            {
                foreach (var item in listChiPhiAdd)
                {
                    item.IdQDDauTu = QDDauTu.Id;
                }
                // add vào bảng Vdt_Dm_DuAn_ChiPhi
                List<VdtDaQddtChiPhiModel> listDuanChiPhi = listChiPhiAdd.Where(x => x.Id.IsNullOrEmpty()).ToList();
                if (listDuanChiPhi.Count > 0)
                {
                    List<VdtDmDuAnChiPhi> listDuAnChiPhiAdd = new List<VdtDmDuAnChiPhi>();
                    listDuAnChiPhiAdd = _mapper.Map<List<VdtDmDuAnChiPhi>>(listDuanChiPhi);
                    _approveProjectService.AddRangeDMDuAnChiPhi(listDuAnChiPhiAdd);
                }

                // update vào bảng Vdt_Dm_DuAn_ChiPhi nếu có thay đổi tên
                List<VdtDaQddtChiPhiModel> listDuanChiPhiUpdate = listChiPhiAdd.Where(x => !x.IdChiPhiDuAn.IsNullOrEmpty()).ToList();
                if (listDuanChiPhiUpdate != null && listDuanChiPhiUpdate.Count > 0)
                {
                    foreach (var item in listDuanChiPhiUpdate)
                    {
                        VdtDmDuAnChiPhi duAnChiPhiUpdate = _approveProjectService.FindDMDuAnChiPhi(item.IdChiPhiDuAn);
                        if (duAnChiPhiUpdate != null)
                        {
                            duAnChiPhiUpdate.STenChiPhi = item.TenChiPhi;
                            _approveProjectService.UpdateVdtDmDuAnChiPhi(duAnChiPhiUpdate);
                        }
                    }
                }

                #region add vào bảng VDt_DA_QDDauTu_Chi phi
                //List<VdtDaQddtChiPhiModel> listQDChiPhiAdd = listAdd.Where(x => x.IsEditHangMuc).ToList();
                if (listChiPhiAdd != null && listChiPhiAdd.Count > 0)
                {
                    List<VdtDaQddauTuChiPhi> listQDChiPhi = new List<VdtDaQddauTuChiPhi>();
                    listQDChiPhi = _mapper.Map<List<VdtDaQddauTuChiPhi>>(listChiPhiAdd);

                    _approveProjectService.AddRangeChiPhi(listQDChiPhi);
                }
                #endregion
            }

            //update
            if (listChiPhiEdit.Count > 0)
            {
                foreach (var item in listChiPhiEdit)
                {
                    // sửa VDT_DM_Duan_Chiphi
                    VdtDmDuAnChiPhi duAnChiPhi = _approveProjectService.FindDMDuAnChiPhi(item.IdChiPhiDuAn);
                    if (duAnChiPhi != null)
                    {
                        duAnChiPhi.STenChiPhi = item.TenChiPhi;
                        duAnChiPhi.IIdChiPhiParent = item.IdChiPhiDuAnParent;
                        _approveProjectService.UpdateVdtDmDuAnChiPhi(duAnChiPhi);
                    }

                    VdtDaQddauTuChiPhi qDChiPhi = _approveProjectService.FindChiPhi(item.IdQDChiPhi);
                    if (qDChiPhi != null)
                    {
                        qDChiPhi.FTienPheDuyet = item.GiaTriPheDuyet;
                        _approveProjectService.UpdateChiPhi(qDChiPhi);
                    }
                }
            }

            //delete QDDauTuChiPhi
            if (listChiPhiDelete.Count > 0)
            {
                foreach (var item in listChiPhiDelete)
                {
                    if (item.IdQDChiPhi.HasValue)
                        _approveProjectService.DeleteChiPhi(item.IdQDChiPhi.Value);
                }
            }
        }

        private void SaveNguonVon()
        {
            List<VdtDaQddtNguonVonModel> listNguonVonAdd = DataQDDauTuNguonVon.Where(x => x.IdNguonVon != 0 && (x.IdQDNguonVon.IsNullOrEmpty()) && !x.IsDeleted && x.GiaTriPheDuyet > 0).ToList();
            List<VdtDaQddtNguonVonModel> listNguonVonEdit = DataQDDauTuNguonVon.Where(x => x.IsModified && !x.IdQDNguonVon.IsNullOrEmpty() && !x.IsDeleted).ToList();
            List<VdtDaQddtNguonVonModel> listNguonVonDelete = DataQDDauTuNguonVon.Where(x => x.IsDeleted && !x.IdQDNguonVon.IsNullOrEmpty()).ToList();

            if (listNguonVonAdd.Count > 0)
            {
                foreach (var item in listNguonVonAdd)
                {
                    item.IdQDDauTu = QDDauTu.Id;
                }

                List<VdtDaQddauTuNguonVon> listQDNguonVon = new List<VdtDaQddauTuNguonVon>();
                listQDNguonVon = _mapper.Map<List<VdtDaQddauTuNguonVon>>(listNguonVonAdd);
                _approveProjectService.AddRangeNguonVon(listQDNguonVon);
            }

            if (listNguonVonEdit.Count > 0)
            {
                foreach (var item in listNguonVonEdit)
                {
                    VdtDaQddauTuNguonVon quyetDinhNV = _approveProjectService.FindNguonVon(item.IdQDNguonVon);
                    if (quyetDinhNV != null)
                    {
                        _mapper.Map(item, quyetDinhNV);
                        _approveProjectService.UpdateNguonVon(quyetDinhNV);
                    }
                }
            }

            if (listNguonVonDelete.Count > 0)
            {
                foreach (var item in listNguonVonDelete)
                {
                    _approveProjectService.DeleteNguonVon(item.IdQDNguonVon);
                }
            }
        }

        private void SaveHangMuc()
        {
            List<ApproveProjectDetailModel> listDMHangMucAdd = DataQDDTHangMuc.Where(x => !x.IsDeleted && (x.Id.IsNullOrEmpty()) && x.GiaTriPheDuyet > 0).ToList();
            List<ApproveProjectDetailModel> listQDHangMucAdd = DataQDDTHangMuc.Where(x => !x.IsDeleted && x.IdQDHangMuc.IsNullOrEmpty() && x.GiaTriPheDuyet > 0).ToList();

            List<ApproveProjectDetailModel> listDMHangMucEdit = DataQDDTHangMuc.Where(x => x.IsModified && !x.IsDeleted && !x.Id.IsNullOrEmpty()).ToList();
            List<ApproveProjectDetailModel> listQDHangMucEdit = DataQDDTHangMuc.Where(x => x.IsModified && !x.IsDeleted && !x.IdQDHangMuc.IsNullOrEmpty()).ToList();
            //List<ApproveProjectDetailModel> listEdit = Items.Where(x => x.IsModified && !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            List<ApproveProjectDetailModel> listDetailDelete = DataQDDTHangMuc.Where(x => x.IsDeleted && !x.Id.IsNullOrEmpty()).ToList();

            //Thêm mới,sửa vào các bảng chi tiết
            if (listDMHangMucAdd != null && listDMHangMucAdd.Count > 0)
            {
                AddDanhMucHangMucDetail(listDMHangMucAdd);
            }

            if (listQDHangMucAdd != null && listQDHangMucAdd.Count > 0)
            {
                AddQDHangMucDetail(listQDHangMucAdd);
            }

            if (listDMHangMucEdit != null && listDMHangMucEdit.Count > 0)
            {
                UpdateDanhMucHangMucDetail(listDMHangMucEdit);
            }
            if (listQDHangMucEdit != null && listQDHangMucEdit.Count > 0)
            {
                UpdateQDHangMucDetail(listQDHangMucEdit);
            }

            //Sửa vào các bảng chi tiết

            if (listDetailDelete.Count > 0)
            {
                DeleteDetail(listDetailDelete);
            }
        }

        private void AddDanhMucHangMucDetail(List<ApproveProjectDetailModel> listAdd)
        {
            // add hạng mục vào bảng VdtDmDuAnHangMuc
            List<VdtDaQddauTuDmHangMuc> listDMHangMuc = new List<VdtDaQddauTuDmHangMuc>();

            listDMHangMuc = _mapper.Map<List<VdtDaQddauTuDmHangMuc>>(listAdd);
            _approveProjectService.AddRangeQdDauTuDMHangMuc(listDMHangMuc);
        }

        private void AddQDHangMucDetail(List<ApproveProjectDetailModel> listAdd)
        {
            // add vào bảng QDDauTuTHangMuc
            foreach (var item in listAdd)
            {
                item.IIdQddauTuId = QDDauTu.Id;
            }
            List<VdtDaQddauTuHangMuc> listQdDtHangMuc = new List<VdtDaQddauTuHangMuc>();
            listQdDtHangMuc = _mapper.Map<List<VdtDaQddauTuHangMuc>>(listAdd);

            _approveProjectService.AddRangeHangMuc(listQdDtHangMuc);

        }

        private void UpdateDanhMucHangMucDetail(List<ApproveProjectDetailModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                VdtDaQddauTuDmHangMuc danhMucHangMuc = _approveProjectService.FindDanhMucHangMuc(item.IdDuAnHangMuc);
                if (danhMucHangMuc != null)
                {
                    _mapper.Map(item, danhMucHangMuc);
                    _approveProjectService.UpdateVDTDanhMucHangMuc(danhMucHangMuc);
                }
            }
        }

        private void UpdateQDHangMucDetail(List<ApproveProjectDetailModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                VdtDaQddauTuHangMuc qdDauTuHangMuc = _approveProjectService.FindQDDTHangMuc(item.IdQDHangMuc);
                if (qdDauTuHangMuc != null)
                {
                    _mapper.Map(item, qdDauTuHangMuc);
                    _approveProjectService.UpdateQDDTHangMuc(qdDauTuHangMuc);
                }
            }
        }

        private void DeleteDetail(List<ApproveProjectDetailModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _approveProjectService.DeleteQDDTHangMuc(item.IdQDHangMuc.Value);
            }
        }

        public void OnShowDetailApproveProject()
        {
            if (SelectedChiPhi != null)
            {
                PheDuyetDuAnDieuChinhDetailViewModel.DataChiPhiModel = SelectedChiPhi;
                PheDuyetDuAnDieuChinhDetailViewModel.Model = QDDauTu;
                PheDuyetDuAnDieuChinhDetailViewModel.QDDauTuParentId = Model.Id;
                PheDuyetDuAnDieuChinhDetailViewModel.IsNotViewDetail = IsNotViewDetail;
                PheDuyetDuAnDieuChinhDetailViewModel.Items = GetListHangMucDetailByChiPhi(SelectedChiPhi.IdChiPhiDuAn.Value);
                PheDuyetDuAnDieuChinhDetailViewModel.Init();
                PheDuyetDuAnDieuChinhDetailViewModel.SavedAction = obj => this.LoadDataQDDauTuHangMucSave();
                view = new VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PheDuyetDuAn.PheDuyetDuAnDieuChinhDetail
                {
                    DataContext = PheDuyetDuAnDieuChinhDetailViewModel
                };
                view.ShowDialog();
            }
        }

        public ObservableCollection<ApproveProjectDetailModel> GetListHangMucDetailByChiPhi(Guid chiPhiId)
        {
            var result = new List<ApproveProjectDetailModel>();
            // tìm trong ListHangMucSave có data chưa, nếu chưa có data thì lấy theo list DataQDDauTuHangMuc load từ chủ trương đầu tư
            List<ApproveProjectDetailModel> listHangMucByChiPhi = DataQDDTHangMuc.Where(x => x.IdDuAnChiPhi == chiPhiId).ToList();

            if (listHangMucByChiPhi.Count > 0)
            {
                result = DataQDDTHangMuc.Where(x => x.IdDuAnChiPhi == chiPhiId).ToList();
            }

            return new ObservableCollection<ApproveProjectDetailModel>(result);
        }

        public void LoadDataQDDauTuHangMucSave()
        {
            DataQDDTHangMucByChiPhi = PheDuyetDuAnDieuChinhDetailViewModel.Items;
            if (DataQDDTHangMucByChiPhi.Count > 0)
            {
                SelectedChiPhi.IsEditGiaTriChiPhi = false;
                var giaTriChiPhi = DataQDDTHangMucByChiPhi.Where(x => x.HangMucParentId == null && !x.IsDeleted).Sum(y => y.GiaTriPheDuyet);

                if (giaTriChiPhi != null)
                {
                    SelectedChiPhi.GiaTriPheDuyet = giaTriChiPhi.Value;
                }
                DataQDDTHangMuc = DataQDDTHangMuc.Where(x => x.IdDuAnChiPhi != SelectedChiPhi.IdChiPhiDuAn).ToList();
                DataQDDTHangMuc.AddRange(DataQDDTHangMucByChiPhi.ToList());
                UpdateListChiPhiCanEditGiaTriChiPhi();
            }
        }

        public bool CheckChiPhiHaveHangMuc()
        {
            var item = SelectedChiPhi;
            if (item == null) return false;

            var lstData = GetListHangMucDetailByChiPhi(item.IdChiPhiDuAn.Value);
            if (lstData == null || !lstData.Any(n => n.GiaTriPheDuyet != 0))
            {
                return false;
            }
            return true;
        }

        private void LoadLoaiQuyetDinh()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = LOAI_QD_PDDA.TypeName.PHE_DUYET_DU_AN,
                ValueItem = ((int)LOAI_QD_PDDA.Type.PHE_DUYET_DU_AN).ToString()
            });
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = LOAI_QD_PDDA.TypeName.BC_KINH_TE_KY_THUAT,
                ValueItem = ((int)LOAI_QD_PDDA.Type.BC_KINH_TE_KY_THUAT).ToString()
            });
            ItemsLoaiQuyetDinh = new ObservableCollection<ComboboxItem>(lstData);
            if (Model.Id == Guid.Empty)
            {
                SelectedLoaiQuyetDinh = ItemsLoaiQuyetDinh.FirstOrDefault();
            }
            else
            {
                SelectedLoaiQuyetDinh = ItemsLoaiQuyetDinh.FirstOrDefault(n => n.ValueItem == Model.ILoaiQuyetDinh.ToString());
                if (SelectedLoaiQuyetDinh == null) SelectedLoaiQuyetDinh = ItemsLoaiQuyetDinh.FirstOrDefault();
            }
            OnPropertyChanged(nameof(SelectedLoaiQuyetDinh));
        }
    }
}
