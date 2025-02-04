using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PheDuyetDuAn;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PheDuyetDuAn
{
    public class PheDuyetDuAnDieuChinhDetailViewModel : DetailViewModelBase<ApproveProjectModel, ApproveProjectDetailModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IProjectManagerService _projectService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private ObservableCollection<ApproveProjectDetailModel> _lstDefaultItem = new ObservableCollection<ApproveProjectDetailModel>();

        public override string Name => "PHÊ DUYỆT DỰ ÁN ĐIỀU CHỈNH CHI TIẾT";
        public override string Title => "Quản lý phê duyệt dự án";
        public override Type ContentType => typeof(PheDuyetDuAnDieuChinhDetail);

        private ObservableCollection<ComboboxItem> _dataLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> DataLoaiCongTrinh
        {
            get => _dataLoaiCongTrinh;
            set => SetProperty(ref _dataLoaiCongTrinh, value);
        }

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);

        private VdtDaQddtChiPhiModel _dataChiPhiModel;
        public VdtDaQddtChiPhiModel DataChiPhiModel
        {
            get => _dataChiPhiModel;
            set => SetProperty(ref _dataChiPhiModel, value);
        }

        private double? _conLai;
        public double? ConLai
        {
            get => _conLai;
            set => SetProperty(ref _conLai, value);
        }

        private double? _tongGiaTriTruocDieuChinh;
        public double? TongGiaTriTruocDieuChinh
        {
            get => _tongGiaTriTruocDieuChinh;
            set => SetProperty(ref _tongGiaTriTruocDieuChinh, value);
        }

        private double? _tongGiaTriSauDieuChinh;
        public double? TongGiaTriSauDieuChinh
        {
            get => _tongGiaTriSauDieuChinh;
            set => SetProperty(ref _tongGiaTriSauDieuChinh, value);
        }

        private int currentRow = -1;

        public Guid QDDauTuParentId { get; set; }

        private bool _isNotViewDetail;
        public bool IsNotViewDetail
        {
            get => _isNotViewDetail;
            set => SetProperty(ref _isNotViewDetail, value);
        }

        public bool BIsReadOnly => !IsNotViewDetail;

        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand AddChildCommand { get; }
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;

        public PheDuyetDuAnDieuChinhDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IApproveProjectService approveProjectService,
            IProjectManagerService projectService,
            IVdtDaDuToanService vdtDaDuToanService
            )
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _approveProjectService = approveProjectService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _projectService = projectService;

            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            AddChildCommand = new RelayCommand(obj => OnAddHangMucChild());
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            _lstDefaultItem = Items.Clone();
            LoadLoaiCongTrinh();
            LoadData();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        protected override void OnRefresh()
        {
            Items = _lstDefaultItem.Clone();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            List<ApproveProjectDetailQuery> listData = new List<ApproveProjectDetailQuery>();

            UpdateListHangMucCanEdit();
            CalculateData();
            CalculateConLaiChiPhi();

            foreach (ApproveProjectDetailModel model in Items)
            {
                model.PropertyChanged += DetailModel_PropertyChanged;
            }
        }

        private void LoadLoaiCongTrinh()
        {
            var data = _projectService.GetAllDMLoaiCongTrinh();
            if (data == null) return;
            _dataLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(data.Select(n => new ComboboxItem()
            {
                ValueItem = n.IIdLoaiCongTrinh.ToString(),
                DisplayItem = n.STenLoaiCongTrinh
            }));
            OnPropertyChanged(nameof(DataLoaiCongTrinh));
        }

        private void UpdateListHangMucCanEdit()
        {
            foreach (var item in Items)
            {
                item.IsEditHangMuc = true;
            }
            //update hàng cha có con thì ko đc edit
            foreach (var item in Items.Where(x => x.IsHangCha && !x.IsDeleted))
            {
                List<ApproveProjectDetailModel> listChild = new List<ApproveProjectDetailModel>();
                listChild = FindListChildHangMuc(item.IdDuAnHangMuc.Value);
                if (listChild == null || listChild.Count == 0)
                {
                    item.IsEditHangMuc = true;
                }
                else
                {
                    item.IsEditHangMuc = false;
                }
            }
            OnPropertyChanged(nameof(Items));
        }

        public List<ApproveProjectDetailModel> FindListChildHangMuc(Guid parentId)
        {
            List<ApproveProjectDetailModel> inner = new List<ApproveProjectDetailModel>();
            foreach (var t in Items.Where(item => item.HangMucParentId == parentId && !item.IsDeleted))
            {
                inner.Add(t);
                inner = inner.Union(FindListChildHangMuc(t.IdDuAnHangMuc.Value)).ToList();
            }

            return inner;
        }

        private void CalculateData()
        {
            if (Items == null) return;
            foreach (var item in Items.Where(n => !n.HangMucParentId.HasValue))
            {
                CalculateParent(item);
            }
        }

        private void CalculateParent(ApproveProjectDetailModel parentItem)
        {
            int childRow = 0;
            foreach (var item in Items.Where(n => n.HangMucParentId.HasValue && n.HangMucParentId == parentItem.IdDuAnHangMuc))
            {
                CalculateParent(item);
                childRow++;
            }
            if (childRow != 0)
                parentItem.GiaTriPheDuyet = Items.Where(n => n.HangMucParentId.HasValue && n.HangMucParentId == parentItem.IdDuAnHangMuc && !n.IsDeleted).Sum(n => n.GiaTriPheDuyet);
        }

        private void CalculateConLaiChiPhi()
        {
            double tongHangMuc = 0;

            List<ApproveProjectDetailModel> listHangMuc = Items.Where(x => x.HangMucParentId == null && x.GiaTriPheDuyet != null && !x.IsDeleted).ToList();
            ConLai = 0;
            TongGiaTriSauDieuChinh = 0;
            TongGiaTriTruocDieuChinh = 0;
            if (listHangMuc != null && listHangMuc.Count > 0)
            {
                tongHangMuc = listHangMuc.Sum(x => x.GiaTriPheDuyet.Value);
                TongGiaTriTruocDieuChinh = listHangMuc.Sum(x => x.GiaTriTruocDieuChinh.Value);
                TongGiaTriSauDieuChinh = listHangMuc.Sum(x => x.GiaTriPheDuyet.Value);
            }
            ConLai = tongHangMuc;
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var selected = (ApproveProjectDetailModel)sender;
            if ((args.PropertyName == nameof(ApproveProjectDetailModel.GiaTriPheDuyet)))
            {
                selected.GiaTriDieuChinh = selected.GiaTriPheDuyet - selected.GiaTriTruocDieuChinh;
                CalculateData();
                CalculateConLaiChiPhi();
            }

            selected.IsModified = true;
            OnPropertyChanged(nameof(Items));

        }

        protected override void OnAdd(object obj)
        {
            ApproveProjectDetailModel targetItem = new ApproveProjectDetailModel()
            {
                IdDuAnHangMuc = Guid.NewGuid(),
                MaHangMuc = GetMaHangMuc(),
                MaOrDer = GetSTTHangMuc(false),
                BIsEdit = true,
                GiaTriPheDuyet = 0
            };
            //int currentRow = -1;
            if (Items != null && Items.Count > 0 && SelectedItem != null)
            {
                ApproveProjectDetailModel sourceItem = SelectedItem;
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.IsEditHangMuc = true;
                targetItem.BIsEdit = true;
                targetItem.Id = Guid.Empty;
                targetItem.IdDuAnHangMuc = Guid.NewGuid();
                targetItem.IdQDHangMuc = null;
                targetItem.MaHangMuc = GetMaHangMuc();
                targetItem.TenHangMuc = string.Empty;
                targetItem.GiaTriPheDuyet = 0;
                targetItem.GiaTriTruocDieuChinh = 0;
                targetItem.MaOrDer = GetSTTHangMuc();
            }
            else
            {
                targetItem.IsHangCha = true;
            }
            targetItem.PropertyChanged += DetailModel_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);
            UpdateListHangMucCanEdit();
            OnPropertyChanged(nameof(Items));
        }

        private string GetSTTHangMuc(bool isAddChild = false)
        {
            string sttHangMuc = string.Empty;
            int inDexSTTHangMucLast = 1;
            if (SelectedItem == null && isAddChild == false)
            {
                if (Items.Count < 1)
                {
                    sttHangMuc = "1";
                    currentRow = -1;
                }
                else
                {
                    var hangMucItemLast = Items.Where(x => x.HangMucParentId.IsNullOrEmpty()).Last();
                    inDexSTTHangMucLast = Int32.Parse(hangMucItemLast.MaOrDer);
                    sttHangMuc = (inDexSTTHangMucLast + 1).ToString();
                    currentRow = Items.IndexOf(hangMucItemLast);
                }
            }
            if (SelectedItem != null && isAddChild == false)
            {
                var hangMucLast = Items.First();// lấy giá trị mặc định là giá trị đầu tiên

                //tìm giá trị ngang hàng cuối cùng trong list => giá trị thêm mới được copy từ giá trị ngang hàng cuối cùng
                if (SelectedItem.HangMucParentId == null)
                {
                    hangMucLast = Items.Where(x => x.HangMucParentId == null).Last();
                    inDexSTTHangMucLast = Int32.Parse(hangMucLast.MaOrDer);
                    sttHangMuc = (inDexSTTHangMucLast + 1).ToString();
                    //currentRow = Items.Count - 1;
                }
                else
                {
                    hangMucLast = Items.Where(x => x.HangMucParentId == SelectedItem.HangMucParentId).Last();
                    string sTTHangMucLast = hangMucLast.MaOrDer;
                    inDexSTTHangMucLast = Int32.Parse(sTTHangMucLast.Substring(sTTHangMucLast.Length - 1));
                    sttHangMuc = sTTHangMucLast.Substring(0, (sTTHangMucLast.Length - 1)) + (inDexSTTHangMucLast + 1).ToString();
                    //currentRow = Items.IndexOf(hangMucLast);
                }
                // tìm dòng con cuối cùng của hạng mục ngang hàng => dòng được thêm sẽ là dòng bên dưới dòng con cuối cùng của hạng mục ngang hàng đó.
                var listHangMucChild = FindListChild(hangMucLast.IdDuAnHangMuc.Value);
                if (listHangMucChild == null || listHangMucChild.Count == 0)
                {
                    currentRow = Items.IndexOf(hangMucLast);
                }
                else
                {
                    currentRow = Items.IndexOf(listHangMucChild.Last());
                }

            }
            if (SelectedItem != null && isAddChild == true)
            {
                var listChild = Items.Where(x => x.HangMucParentId == SelectedItem.IdDuAnHangMuc).ToList();
                if (listChild == null || listChild.Count == 0)
                {
                    sttHangMuc = SelectedItem.MaOrDer + "_1";
                    currentRow = Items.IndexOf(SelectedItem);
                }
                else
                {
                    var hangMucChildLast = Items.Where(x => x.HangMucParentId == SelectedItem.IdDuAnHangMuc).Last();
                    string sTTHangMucLast = hangMucChildLast.MaOrDer;
                    inDexSTTHangMucLast = Int32.Parse(sTTHangMucLast.Substring(sTTHangMucLast.Length - 1));
                    sttHangMuc = sTTHangMucLast.Substring(0, (sTTHangMucLast.Length - 1)) + (inDexSTTHangMucLast + 1).ToString();
                    currentRow = Items.IndexOf(hangMucChildLast);
                }
            }
            return sttHangMuc;
        }

        private string GetMaHangMuc()
        {
            string maHangMuc = string.Empty;
            VdtDaDuAn duAn = _projectService.FindById(Model.IIdDuAnId.Value);

            if (duAn != null && duAn.SMaDuAn.Length > 4)
            {
                if (duAn.SMaDuAn != null && duAn.SMaDuAn.Length > 4)
                {
                    maHangMuc = duAn.SMaDuAn.Substring(duAn.SMaDuAn.Length - 4);
                }
            }

            return maHangMuc;
        }

        private void OnAddHangMucChild()
        {
            if (Items == null || Items.Count == 0 || SelectedItem == null)
            {
                return;
            }
            string maSTT = GetSTTHangMuc(true);

            ApproveProjectDetailModel sourceItem = SelectedItem;
            ApproveProjectDetailModel targetItem = ObjectCopier.Clone(sourceItem);

            targetItem.Id = Guid.Empty;
            targetItem.IdDuAnHangMuc = Guid.NewGuid();
            targetItem.IdQDHangMuc = null;
            targetItem.IdChiPhi = null;
            targetItem.MaOrDer = maSTT;
            targetItem.GiaTriPheDuyet = 0;
            targetItem.GiaTriTruocDieuChinh = 0;
            targetItem.IsHangCha = false;
            targetItem.HangMucParentId = sourceItem.IdDuAnHangMuc;
            targetItem.MaHangMuc = GetMaHangMuc();
            targetItem.TenHangMuc = string.Empty;
            targetItem.IsEditHangMuc = true;
            targetItem.PropertyChanged += DetailModel_PropertyChanged;
            sourceItem.IsHangCha = true;

            Items.Insert(currentRow + 1, targetItem);
            UpdateListHangMucCanEdit();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        public override void OnSave(object obj)
        {
            //Items = new ObservableCollection<ApproveProjectDetailModel>(Items.Where(x => x.GiaTriPheDuyet > 0 && !x.IsDeleted));
            foreach (var item in Items)
            {
                item.IdDuAnChiPhi = DataChiPhiModel.IdChiPhiDuAn;
            }
            SavedAction?.Invoke(null);
            MessageBox.Show(Resources.MsgSaveDone);
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }

        private bool Validate()
        {
            if (!Items.Any(n => !n.IsDeleted && !string.IsNullOrEmpty(n.TenHangMuc) && (n.GiaTriPheDuyet ?? 0) != 0))
            {
                MessageBox.Show(Resources.MsgCheckHangMucDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (ConLai != null && ConLai < 0)
            {
                MessageBox.Show(Resources.ErrorChiPhiNotEqualHangMuc, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void AddDanhMucHangMucDetail(List<ApproveProjectDetailModel> listAdd)
        {
            // add hạng mục vào bảng VdtDmDuAnHangMuc
            //List<ApproveProjectDetailModel> listHangMucAdd = listAdd.Where(x => !x.IsHangMucOld.Value).ToList();
            List<VdtDaQddauTuDmHangMuc> listDMHangMuc = new List<VdtDaQddauTuDmHangMuc>();

            listDMHangMuc = _mapper.Map<List<VdtDaQddauTuDmHangMuc>>(listAdd);
            _approveProjectService.AddRangeQdDauTuDMHangMuc(listDMHangMuc);
        }

        private void AddQDHangMucDetail(List<ApproveProjectDetailModel> listAdd)
        {
            // add vào bảng QDDauTuTHangMuc
            foreach (var item in listAdd)
            {
                //item.IdChiPhi = DataChiPhiModel.IdChiPhi;
                item.IdDuAnChiPhi = DataChiPhiModel.IdChiPhiDuAn;
                item.IIdQddauTuId = Model.Id;
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

        private void OnCloseWindow()
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null)
            {
                var listDelete = FindListChild(SelectedItem.IdDuAnHangMuc.Value);
                listDelete.Add(SelectedItem);
                foreach (var item in listDelete)
                {
                    item.IsDeleted = !SelectedItem.IsDeleted;
                }
                CalculateData();
                CalculateConLaiChiPhi();
            }
        }

        public List<ApproveProjectDetailModel> FindListChild(Guid parentId)
        {
            List<ApproveProjectDetailModel> inner = new List<ApproveProjectDetailModel>();
            foreach (var t in Items.Where(item => item.HangMucParentId == parentId))
            {
                inner.Add(t);
                inner = inner.Union(FindListChild(t.IdDuAnHangMuc.Value)).ToList();
            }
            return inner;
        }

    }
}
