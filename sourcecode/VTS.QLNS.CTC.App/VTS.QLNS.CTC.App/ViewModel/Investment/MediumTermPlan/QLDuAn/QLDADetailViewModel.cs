using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.QLDuAn;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.QLDuAn
{
    public class QLDADetailViewModel : DialogAttachmentViewModelBase<ProjectManagerModel>
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IProjectManagerService _projectService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private readonly IVdtDaChuTruongDauTuService _vdtDaChuTruongDauTuService;
        #endregion

        public override string Name => "THÔNG TIN DỰ ÁN CHI TIẾT";
        public override string Title => "Quản lý thông tin dự án";
        public override Type ContentType => typeof(QLDADetail);

        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_THONGTIN_DUAN;

        private ObservableCollection<VdtDaNguonVonModel> _nguonVonItems;
        public ObservableCollection<VdtDaNguonVonModel> NguonVonItems
        {
            get => _nguonVonItems;
            set => SetProperty(ref _nguonVonItems, value);
        }

        private ObservableCollection<VdtDaHangMucModel> _hangMucItems;
        public ObservableCollection<VdtDaHangMucModel> HangMucItems
        {
            get => _hangMucItems;
            set => SetProperty(ref _hangMucItems, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaChuTruongDTNguonVonModel> _chuTruongNguonVonItems;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaChuTruongDTNguonVonModel> ChuTruongNguonVonItems
        {
            get => _chuTruongNguonVonItems;
            set => SetProperty(ref _chuTruongNguonVonItems, value);
        }

        private ObservableCollection<VdtDaHangMucModel> _chuTruongHangMucItems;
        public ObservableCollection<VdtDaHangMucModel> ChuTruongHangMucItems
        {
            get => _chuTruongHangMucItems;
            set => SetProperty(ref _chuTruongHangMucItems, value);
        }

        private ObservableCollection<ApproveProjectModel> _qDDauTuItems;
        public ObservableCollection<ApproveProjectModel> QDDauTuItems
        {
            get => _qDDauTuItems;
            set => SetProperty(ref _qDDauTuItems, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaQddtNguonVonModel> _qDDauTuNguonVonItems;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaQddtNguonVonModel> QDDauTuNguonVonItems
        {
            get => _qDDauTuNguonVonItems;
            set => SetProperty(ref _qDDauTuNguonVonItems, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaQddtChiPhiModel> _qDDauTuChiPhiItems;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaQddtChiPhiModel> QDDauTuChiPhiItems
        {
            get => _qDDauTuChiPhiItems;
            set => SetProperty(ref _qDDauTuChiPhiItems, value);
        }

        private ObservableCollection<ApproveProjectDetailModel> _qDDauTuHangMucItems;
        public ObservableCollection<ApproveProjectDetailModel> QDDauTuHangMucItems
        {
            get => _qDDauTuHangMucItems;
            set => SetProperty(ref _qDDauTuHangMucItems, value);
        }

        private ObservableCollection<VdtDuToanModel> _duToanItems;
        public ObservableCollection<VdtDuToanModel> DuToanItems
        {
            get => _duToanItems;
            set => SetProperty(ref _duToanItems, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaDuToanNguonVonModel> _duToanNguonVonItems;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaDuToanNguonVonModel> DuToanNguonVonItems
        {
            get => _duToanNguonVonItems;
            set => SetProperty(ref _duToanNguonVonItems, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaDuToanChiPhiModel> _duToanChiPhiItems;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.VdtDaDuToanChiPhiModel> DuToanChiPhiItems
        {
            get => _duToanChiPhiItems;
            set => SetProperty(ref _duToanChiPhiItems, value);
        }

        private ObservableCollection<DuToanDetailModel> _duToanHangMucItems;
        public ObservableCollection<DuToanDetailModel> DuToanHangMucItems
        {
            get => _duToanHangMucItems;
            set => SetProperty(ref _duToanHangMucItems, value);
        }

        private ObservableCollection<ChuTruongDauTuModel> _chuTruongDauTuItems;
        public ObservableCollection<ChuTruongDauTuModel> ChuTruongDauTuItems
        {
            get => _chuTruongDauTuItems;
            set => SetProperty(ref _chuTruongDauTuItems, value);
        }

        public ChuTruongDauTuModel CTDauTu { get; set; }
        public ApproveProjectModel SelectedQDDauTu { get; set; }
        public VdtDuToanModel SelectedDuToan { get; set; }
        public VdtDaQddtChiPhiModel SelectedChiPhiQDDauTu { get; set; }
        public VdtDaDuToanChiPhiModel SelectedChiPhiDuToan { get; set; }

        public RelayCommand ShowHangMucQDDauTuCommand { get; }
        public RelayCommand ShowHangMucDuToanCommand { get; }
        public RelayCommand ShowNguonVonChiPhiQDCommand { get; }
        public RelayCommand ShowNguonVonChiPhiDTCommand { get; }

        public QLDADetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IApproveProjectService approveProjectService,
            IProjectManagerService projectService,
            IVdtDaDuToanService vdtDaDuToanService,
            IVdtDaChuTruongDauTuService vdtDaChuTruongDauTuService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService
            ) : base(mapper, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _approveProjectService = approveProjectService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _projectService = projectService;
            _vdtDaChuTruongDauTuService = vdtDaChuTruongDauTuService;

            ShowHangMucQDDauTuCommand = new RelayCommand(obj => OnShowHangMucQDDauTu());
            ShowHangMucDuToanCommand = new RelayCommand(obj => OnShowHangMucDuToan());
            ShowNguonVonChiPhiQDCommand = new RelayCommand(obj => OnSelectionQDDauTuDoubleClick());
            ShowNguonVonChiPhiDTCommand = new RelayCommand(obj => OnSelectionDoubleClick(obj));
        }

        public override void Init()
        {
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            BIsReadOnly = true;
            LoadAttach();
            LoadDataDuAn();
            LoadDataChuTruongDT();
            LoadDataQDDauTu();
            LoadDataDuToan();
        }

        private void LoadDataDuAn()
        {
            LoadNguonVonDuAn();
            LoadHangMucDuAn();
        }

        private void LoadDataChuTruongDT()
        {
            VdtDaChuTruongDauTu chutruong = _vdtDaChuTruongDauTuService.FindByDuAnId(Model.Id);
            if (chutruong != null)
            {
                ChuTruongDauTuQuery queryData = _vdtDaChuTruongDauTuService.FindChuTruongById(chutruong.Id);
                CTDauTu = _mapper.Map<ChuTruongDauTuModel>(queryData);
                ChuTruongDauTuItems = new ObservableCollection<ChuTruongDauTuModel>();
                ChuTruongDauTuItems.Add(CTDauTu);
                LoadNguonVonChuTruong();
                LoadHangMucChuTruong();
            } else
            {
                ChuTruongDauTuItems = new ObservableCollection<ChuTruongDauTuModel>();
                CTDauTu = null;
                ChuTruongNguonVonItems = null;
                ChuTruongHangMucItems = null;
                OnPropertyChanged(nameof(CTDauTu));
                OnPropertyChanged(nameof(ChuTruongNguonVonItems));
                OnPropertyChanged(nameof(ChuTruongHangMucItems));
            }
            
        }

        private void LoadDataQDDauTu()
        {
            IEnumerable<ApproveProjectQuery> listData = _approveProjectService.FindListQDDauTuByDuAnId(Model.Id).ToList();
            _qDDauTuItems = _mapper.Map<ObservableCollection<Model.ApproveProjectModel>>(listData);
            QDDauTuHangMucItems = new ObservableCollection<ApproveProjectDetailModel>();
            //LoadNguonVonQDDauTu();
            //LoadChiPhiQDDauTu();
            QDDauTuNguonVonItems = null;
            QDDauTuHangMucItems = null;
            QDDauTuChiPhiItems = null;
            OnPropertyChanged(nameof(QDDauTuItems));
        }

        private void LoadDataDuToan()
        {
            IEnumerable<VdtDaDuToanQuery> data = _vdtDaDuToanService.GetDuToanByDuAnIdAndActive(Model.Id, -1);
            _duToanItems = _mapper.Map<ObservableCollection<Model.VdtDuToanModel>>(data);
            DuToanNguonVonItems = new ObservableCollection<VdtDaDuToanNguonVonModel>();
            DuToanChiPhiItems = new ObservableCollection<VdtDaDuToanChiPhiModel>();
            DuToanHangMucItems = new ObservableCollection<DuToanDetailModel>();
            OnPropertyChanged(nameof(DuToanItems));
        }

        private void LoadNguonVonDuAn()
        {
            IEnumerable<VDTDaNguonVonQuery> listNguonVon = _projectService.FindListNguonVonByDuan(Model.Id).ToList();
            NguonVonItems = _mapper.Map<ObservableCollection<VdtDaNguonVonModel>>(listNguonVon);
        }

        private void LoadHangMucDuAn()
        {
            IEnumerable<VdtDaHangMucQuery> listHangMuc = _projectService.FindListHangMucByDuan(Model.Id).ToList();
            HangMucItems = _mapper.Map<ObservableCollection<VdtDaHangMucModel>>(listHangMuc);
        }

        private void LoadNguonVonChuTruong()
        {
            if (CTDauTu != null)
            {
                IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> listNguonVonCT = _vdtDaChuTruongDauTuService.FindListChuTruongNguonVonDetail(CTDauTu.Id).ToList();
                ChuTruongNguonVonItems = _mapper.Map<ObservableCollection<VdtDaChuTruongDTNguonVonModel>>(listNguonVonCT);
            }
            else
            {
                ChuTruongNguonVonItems = new ObservableCollection<VdtDaChuTruongDTNguonVonModel>();
            }

        }

        private void LoadHangMucChuTruong()
        {
            if (CTDauTu != null)
            {
                IEnumerable<VdtDaHangMucQuery> listHangMucCT = _vdtDaChuTruongDauTuService.FindListDAHangMucDetailAfterSaveChuTruong(CTDauTu.Id).ToList();
                ChuTruongHangMucItems = _mapper.Map<ObservableCollection<VdtDaHangMucModel>>(listHangMucCT);
            }
            else
            {
                ChuTruongHangMucItems = new ObservableCollection<VdtDaHangMucModel>();
            }
        }

        private void LoadNguonVonQDDauTu()
        {
            if (SelectedQDDauTu != null)
            {
                IEnumerable<VdtDaQDNguonVonQuery> listNguonVonQD = _approveProjectService.FindListQDDauTuNguonVon(SelectedQDDauTu.Id).ToList();
                QDDauTuNguonVonItems = _mapper.Map<ObservableCollection<VdtDaQddtNguonVonModel>>(listNguonVonQD);
            }
            else
            {
                QDDauTuNguonVonItems = new ObservableCollection<VdtDaQddtNguonVonModel>();
            }

        }

        private void LoadChiPhiQDDauTu()
        {

            IEnumerable<VdtDaQddtChiPhiQuery> listChiPhiQD = _approveProjectService.FindListQDDauTuChiPhi(SelectedQDDauTu.Id).ToList();
            QDDauTuChiPhiItems = _mapper.Map<ObservableCollection<VdtDaQddtChiPhiModel>>(listChiPhiQD);
            if (QDDauTuChiPhiItems.Count > 0)
            {
                UpdateListQDChiPhiCanEdit();
            }

        }

        private void UpdateListQDChiPhiCanEdit()
        {
            foreach (var item in QDDauTuChiPhiItems.Where(x => x.IsHangCha && !x.IsDeleted))
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
            OnPropertyChanged(nameof(QDDauTuChiPhiItems));
        }

        public List<VdtDaQddtChiPhiModel> FindListChildChiPhi(Guid parentId)
        {
            List<VdtDaQddtChiPhiModel> inner = new List<VdtDaQddtChiPhiModel>();
            foreach (var t in QDDauTuChiPhiItems.Where(item => item.IdChiPhiDuAnParent == parentId && !item.IsDeleted))
            {
                inner.Add(t);
                inner = inner.Union(FindListChildChiPhi(t.Id)).ToList();
            }

            return inner;
        }

        private void LoadHangMucQDDauTu()
        {
            IEnumerable<ApproveProjectDetailQuery> listHangMucQD = _approveProjectService.FindListDetail(SelectedQDDauTu.Id, SelectedQDDauTu.IIdDuAnId.Value, SelectedChiPhiQDDauTu.IdChiPhiDuAn.Value).ToList();
            QDDauTuHangMucItems = _mapper.Map<ObservableCollection<ApproveProjectDetailModel>>(listHangMucQD);
        }
        private void LoadHangMucQDDauTuDefault()
        {
            IEnumerable<ApproveProjectDetailQuery> listHangMucQD = _approveProjectService.FindListAllHangMucByQDDauTu(SelectedQDDauTu.Id).ToList();
            QDDauTuHangMucItems = _mapper.Map<ObservableCollection<ApproveProjectDetailModel>>(listHangMucQD);
        }

        protected void OnSelectionDoubleClick(object obj)
        {
            if (SelectedDuToan != null)
            {
                LoadNguonVonDuToan();
                LoadChiPhiDuToan();
                LoadHangMucDuToanDefault();
            }
        }

        protected void OnSelectionQDDauTuDoubleClick()
        {
            if (SelectedQDDauTu != null)
            {
                LoadNguonVonQDDauTu();
                LoadChiPhiQDDauTu();
                LoadHangMucQDDauTuDefault();
            }
        }

        private void LoadNguonVonDuToan()
        {
            IEnumerable<VdtDaDuToanNguonVonQuery> listNguonVonDT = _vdtDaDuToanService.FindListDuToanNguonVonByDuToanId(SelectedDuToan.Id).ToList();
            _duToanNguonVonItems = _mapper.Map<ObservableCollection<VdtDaDuToanNguonVonModel>>(listNguonVonDT);
            OnPropertyChanged(nameof(DuToanNguonVonItems));
        }

        private void LoadChiPhiDuToan()
        {
            IEnumerable<VdtDaDuToanChiPhiQuery> listChiPhiDT = _vdtDaDuToanService.FindListDuToanChiPhiByDuToanId(SelectedDuToan.Id).ToList();
            _duToanChiPhiItems = _mapper.Map<ObservableCollection<VdtDaDuToanChiPhiModel>>(listChiPhiDT);
            OnPropertyChanged(nameof(DuToanChiPhiItems));
            UpdateListDuToanChiPhiCanEdit();
        }

        private void UpdateListDuToanChiPhiCanEdit()
        {
            foreach (var item in DuToanChiPhiItems.Where(x => x.IsHangCha && !x.IsDeleted))
            {
                var listChild = FindListChildDuToanChiPhi(item.IdChiPhiDuAn.Value);
                if (listChild == null || listChild.Count == 0)
                {
                    item.IsEditHangMuc = true;
                }
                else
                {
                    item.IsEditHangMuc = false;
                }
            }
            OnPropertyChanged(nameof(DuToanChiPhiItems));
        }

        public List<VdtDaDuToanChiPhiModel> FindListChildDuToanChiPhi(Guid parentId)
        {
            List<VdtDaDuToanChiPhiModel> inner = new List<VdtDaDuToanChiPhiModel>();
            foreach (var t in DuToanChiPhiItems.Where(item => item.IdChiPhiDuAnParent == parentId && !item.IsDeleted))
            {
                inner.Add(t);
                inner = inner.Union(FindListChildDuToanChiPhi(t.Id)).ToList();
            }
            return inner;
        }


        private void LoadHangMucDuToan()
        {
            IEnumerable<DuToanDetailQuery> listHangMucDT = _vdtDaDuToanService.FindListDetail(SelectedDuToan.Id, SelectedChiPhiDuToan.IdChiPhiDuAn).ToList();
            _duToanHangMucItems = _mapper.Map<ObservableCollection<DuToanDetailModel>>(listHangMucDT);
            OnPropertyChanged(nameof(DuToanHangMucItems));
        }

        private void LoadHangMucDuToanDefault()
        {
            IEnumerable<DuToanDetailQuery> listHangMucDT = _vdtDaDuToanService.FindListHangMucAllDetail(SelectedDuToan.Id).ToList();
            _duToanHangMucItems = _mapper.Map<ObservableCollection<DuToanDetailModel>>(listHangMucDT);
            OnPropertyChanged(nameof(DuToanHangMucItems));
        }

        public void OnShowHangMucQDDauTu()
        {
            if (SelectedChiPhiQDDauTu != null)
            {
                LoadHangMucQDDauTu();
            }
        }

        public void OnShowHangMucDuToan()
        {
            if (SelectedChiPhiDuToan != null)
            {
                LoadHangMucDuToan();
            }
        }
    }
}
