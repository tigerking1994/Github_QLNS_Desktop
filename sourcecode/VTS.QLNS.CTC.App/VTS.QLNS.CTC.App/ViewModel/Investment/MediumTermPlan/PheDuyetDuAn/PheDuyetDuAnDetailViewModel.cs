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
    public class PheDuyetDuAnDetailViewModel : DetailViewModelBase<ApproveProjectModel, ApproveProjectDetailModel>
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IProjectManagerService _projectService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private readonly IVdtDuAnHangMucService _vdtDuAnHangMucService;
        private int currentRow = -1;
        private int _indexHangMucMax = 0;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        #endregion

        public override string Name => "PHÊ DUYỆT DỰ ÁN CHI TIẾT";
        public override string Title => "Quản lý phê duyệt dự án";
        public override Type ContentType => typeof(PheDuyetDuAnDetail);

        #region Items
        private ObservableCollection<ApproveProjectDetailModel> _lstDefaultItem = new ObservableCollection<ApproveProjectDetailModel>();

        public ObservableCollection<ApproveProjectDetailModel> ItemsTemp = new ObservableCollection<ApproveProjectDetailModel>();

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

        public bool IsEditable => (Model.BActive == true || Model.BActive == null) && IsNotViewDetail;

        private bool _isNotViewDetail;
        public bool IsNotViewDetail
        {
            get => _isNotViewDetail;
            set => SetProperty(ref _isNotViewDetail, value);
        }

        public bool BIsReadOnly => !IsNotViewDetail;
        #endregion

        #region RelayCommand
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand AddChildCommand { get; }

        #endregion

        public PheDuyetDuAnDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IApproveProjectService approveProjectService,
            IProjectManagerService projectService,
            IVdtDaDuToanService vdtDaDuToanService,
            IVdtDuAnHangMucService vdtDuAnHangMucService
            )
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _approveProjectService = approveProjectService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _projectService = projectService;
            _vdtDuAnHangMucService = vdtDuAnHangMucService;

            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            AddChildCommand = new RelayCommand(obj => OnAddChild());
        }

        #region Events
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            _lstDefaultItem = Items.Clone();
            _lstDefaultItem = ItemsTemp.Clone();
            LoadLoaiCongTrinh();
            LoadData();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);

            UpdateListHangMucCanEdit();
            CalculateData();
            CalculateConLaiChiPhi();
            _indexHangMucMax = _vdtDuAnHangMucService.FindNextSoChungTuIndex() - 1;
            foreach (ApproveProjectDetailModel model in Items)
            {
                model.PropertyChanged += DetailModel_PropertyChanged;
            }
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
            // !!! HẠNG MỤC THÊM MỚI ĐƯỢC BASE TRÊN HẠNG MỤC ĐANG ĐƯỢC CHỌN (SELECTEDITEM)
            if (Items != null && Items.Count > 0 && SelectedItem != null)
            {
                if (SelectedItem.IsDeleted)
                {
                    return;
                }
                ApproveProjectDetailModel sourceItem = SelectedItem;
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.IsEditHangMuc = true;
                targetItem.Id = Guid.Empty;
                targetItem.IdDuAnHangMuc = Guid.NewGuid();
                targetItem.IdQDHangMuc = null;
                targetItem.MaHangMuc = GetMaHangMuc();
                targetItem.TenHangMuc = string.Empty;
                targetItem.GiaTriPheDuyet = 0;
                targetItem.MaOrDer = GetSTTHangMuc();
                targetItem.BIsEdit = true;
            }
            else
            {
                targetItem.IsHangCha = true;
                targetItem.IsEditHangMuc = true;
                targetItem.Id = Guid.Empty;
                targetItem.IdDuAnHangMuc = Guid.NewGuid();
                targetItem.IdQDHangMuc = null;
                targetItem.MaHangMuc = GetMaHangMuc();
                targetItem.TenHangMuc = string.Empty;
                targetItem.GiaTriPheDuyet = 0;
                targetItem.MaOrDer = GetSTTHangMuc();
                targetItem.BIsEdit = true;
                targetItem.IIdQddauTuId = Model.Id;
            }
            targetItem.PropertyChanged += DetailModel_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);
            UpdateListHangMucCanEdit();
            OnPropertyChanged(nameof(Items));
        }

        private void OnAddChild()
        {
            if (Items == null || Items.Count == 0 || SelectedItem == null)
            {
                MessageBox.Show(Resources.MsgErrNotChooseParent, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SelectedItem.IsDeleted)
            {
                MessageBox.Show(Resources.MsgHMChaDelete, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maSTT = GetSTTHangMuc(true);
            ApproveProjectDetailModel sourceItem = SelectedItem;
            ApproveProjectDetailModel targetItem = ObjectCopier.Clone(sourceItem);
            targetItem.Id = Guid.Empty;
            targetItem.IdDuAnHangMuc = Guid.NewGuid();
            targetItem.IdQDHangMuc = null;
            //targetItem.IdChiPhi = null;
            targetItem.MaOrDer = maSTT;
            targetItem.GiaTriPheDuyet = 0;
            targetItem.IsHangCha = false;
            targetItem.HangMucParentId = sourceItem.IdDuAnHangMuc;
            targetItem.MaHangMuc = GetMaHangMuc();
            targetItem.TenHangMuc = string.Empty;
            targetItem.IsEditHangMuc = true;
            targetItem.PropertyChanged += DetailModel_PropertyChanged;
            sourceItem.IsHangCha = true;
            targetItem.BIsEdit = true;

            Items.Insert(currentRow + 1, targetItem);
            UpdateListHangMucCanEdit();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        protected override void OnRefresh()
        {
            Items = ObjectCopier.Clone(_lstDefaultItem);
            LoadData();
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
                    currentRow = Items.IndexOf(Items.Last());
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
                }
                else
                {
                    hangMucLast = Items.Where(x => x.HangMucParentId == SelectedItem.HangMucParentId).Last();
                    string sTTHangMucLast = hangMucLast.MaOrDer;
                    inDexSTTHangMucLast = Int32.Parse(sTTHangMucLast.Substring(sTTHangMucLast.Length - 1));
                    sttHangMuc = sTTHangMucLast.Substring(0, (sTTHangMucLast.Length - 1)) + (inDexSTTHangMucLast + 1).ToString();
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
                //currentRow = Items.IndexOf(hangMucLast);
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
                    if (string.IsNullOrEmpty(sTTHangMucLast))
                    {
                        sttHangMuc = SelectedItem.MaOrDer + "_1";
                    }
                    List<string> arrayMaOrDer = sTTHangMucLast.Split("_").ToList();
                    if (arrayMaOrDer.Count > 0)
                    {
                        string maOrderOld = arrayMaOrDer.Last();
                        inDexSTTHangMucLast = Int32.Parse(maOrderOld) + 1;
                        arrayMaOrDer.RemoveAt(arrayMaOrDer.Count - 1);
                        arrayMaOrDer.Add(inDexSTTHangMucLast.ToString());
                        sttHangMuc = string.Join("_", arrayMaOrDer);
                    }

                    //tìm vị trí của dòng con cuối cùng của hạng mục ngang hàng cuối cùng
                    var listChildOfHangMucLast = FindListChild(hangMucChildLast.IdDuAnHangMuc.Value);
                    if (listChildOfHangMucLast == null || listChildOfHangMucLast.Count == 0)
                    {
                        currentRow = Items.IndexOf(hangMucChildLast);
                    }
                    else
                    {
                        currentRow = Items.IndexOf(listChildOfHangMucLast.Last());
                    }
                }
            }
            return sttHangMuc;
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

        public override void OnSave(object obj)
        {
            foreach (var item in Items)
            {
                if (item.GiaTriPheDuyet.HasValue && item.GiaTriPheDuyet != 0)
                    item.IdDuAnChiPhi = DataChiPhiModel.IdChiPhiDuAn;
                if (item.IsDeleted == true)
                    ItemsTemp.Remove(item);
            }
            SavedAction?.Invoke(null);
            MessageBox.Show(Resources.MsgSaveDone);
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }

        private void OnCloseWindow()
        {
            OnRefresh();
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
        #endregion

        #region Helper
        private bool Validate()
        {
            if (!Items.Any(n => !n.IsDeleted && !string.IsNullOrEmpty(n.TenHangMuc) && (n.GiaTriPheDuyet ?? 0) != 0))
            {
                MessageBox.Show(Resources.MsgCheckHangMucDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var selected = (ApproveProjectDetailModel)sender;
            if ((args.PropertyName == nameof(ApproveProjectDetailModel.GiaTriPheDuyet)))
            {
                CalculateData();
                CalculateConLaiChiPhi();
            }

            selected.IsModified = true;
            OnPropertyChanged(nameof(Items));

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

        private void DeleteChildChiPhi(DuAnDetailModel item, Dictionary<Guid, List<DuAnDetailModel>> dicData)
        {
            item.IsDeleted = true;
            if (!dicData.ContainsKey(item.iIdHangMuc.Value)) return;
            foreach (var child in dicData[item.iIdHangMuc.Value])
            {
                DeleteChildChiPhi(child, dicData);
            }
        }

        private string GetMaHangMuc()
        {
            string maHM = string.Empty;
            if (Model.IIdDuAnId.Value != null)
            {
                VdtDaDuAn duAn = _projectService.FindById(Model.IIdDuAnId.Value);
                if (duAn != null)
                {
                    if (duAn.SMaDuAn != null && duAn.SMaDuAn.Length > 4)
                    {
                        maHM = duAn.SMaDuAn.Substring(duAn.SMaDuAn.Length - 4);
                    }
                }

                int indexHangMuc = _indexHangMucMax + 1;
                _indexHangMucMax = indexHangMuc;
                maHM = maHM + indexHangMuc.ToString("D3");
            }

            return maHM;
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
            if (listHangMuc != null && listHangMuc.Count > 0)
            {
                tongHangMuc = listHangMuc.Sum(x => x.GiaTriPheDuyet.Value);
            }
            ConLai = tongHangMuc;
        }
        #endregion
    }
}
