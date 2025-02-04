using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.TKTCVaTongDuToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.TKTCVaTongDuToan
{
    public class TKTCVaTongDuToanDieuChinhDetailViewModel : DetailViewModelBase<VdtDuToanModel, DuToanDetailModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IVdtDuAnHangMucService _vdtDuAnHangMucService;

        public override string Name => "TKTC VÀ TỔNG DỰ TOÁN ĐIỀU CHỈNH CHI TIẾT";
        public override string Title => "Quản lý thiết kế thi công và tổng dự toán";
        public override Type ContentType => typeof(TKTCVaTongDuToanDieuChinhDetail);

        private VdtDaDuToanChiPhiModel _dataChiPhiModel;
        public VdtDaDuToanChiPhiModel DataChiPhiModel
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

        private ObservableCollection<ComboboxItem> _dataLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> DataLoaiCongTrinh
        {
            get => _dataLoaiCongTrinh;
            set => SetProperty(ref _dataLoaiCongTrinh, value);
        }

        private ObservableCollection<DuToanDetailModel> _dataHangMucPhanChiaByHangMuc;
        public ObservableCollection<DuToanDetailModel> DataHangMucPhanChiaByHangMuc
        {
            get => _dataHangMucPhanChiaByHangMuc;
            set => SetProperty(ref _dataHangMucPhanChiaByHangMuc, value);
        }

        private List<DuToanDetailModel> _hangMucPhanChiaSaved;
        public List<DuToanDetailModel> HangMucPhanChiaSaved
        {
            get => _hangMucPhanChiaSaved;
            set => SetProperty(ref _hangMucPhanChiaSaved, value);
        }

        //private ObservableCollection<DuToanDetailModel> _hangMucPheDuyetItems;
        //public ObservableCollection<DuToanDetailModel> HangMucPheDuyetItems
        //{
        //    get => _hangMucPheDuyetItems;
        //    set => SetProperty(ref _hangMucPheDuyetItems, value);
        //}

        private int currentRow = -1;
        public Guid DutoanParentId { get; set; }

        private bool _isNotViewDetail;
        public bool IsNotViewDetail
        {
            get => _isNotViewDetail;
            set => SetProperty(ref _isNotViewDetail, value);
        }

        private double? _tongHMPhanChia;
        public double? TongHMPhanChia
        {
            get => _tongHMPhanChia;
            set => SetProperty(ref _tongHMPhanChia, value);
        }

        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand AddHangMucCommand { get; }
        public RelayCommand AddChildCommand { get; }
        public RelayCommand DevideHangMucSelectedCommand { get; }

        public TKTCVaTongDuToanHangMucDevideViewModel TKTCVaTongDuToanHangMucDevideViewModel { get; }

        public TKTCVaTongDuToanDieuChinhDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IApproveProjectService approveProjectService,
            IVdtDaDuToanService vdtDaDuToanService,
            IProjectManagerService projectManagerService,
            IVdtDuAnHangMucService vdtDuAnHangMucService,
            TKTCVaTongDuToanHangMucDevideViewModel tongDuToanHangMucDevideViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _approveProjectService = approveProjectService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _projectManagerService = projectManagerService;
            _vdtDuAnHangMucService = vdtDuAnHangMucService;

            AddChildCommand = new RelayCommand(obj => OnAddHangMucChild());
            AddHangMucCommand = new RelayCommand(obj => OnAddHangMucDetail());
            DevideHangMucSelectedCommand = new RelayCommand(obj => OnShowPopUpChooseHangMuc());

            TKTCVaTongDuToanHangMucDevideViewModel = tongDuToanHangMucDevideViewModel;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            //LoadChiPhi();
            //LoadNguonVon();
            LoadData();
            LoadLoaiCongTrinh();
            CalculateData();
            OnPropertyChanged(nameof(Items));
            //OnPropertyChanged(nameof(IsSaveData));
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            List<DuToanDetailQuery> listData = new List<DuToanDetailQuery>();
            if (HangMucPhanChiaSaved == null)
                HangMucPhanChiaSaved = new List<DuToanDetailModel>();

            UpdateListHangMucCanEdit();

            foreach (DuToanDetailModel model in Items)
            {
                model.PropertyChanged += DetailModel_PropertyChanged;
            }
            CalculateData();
            CalculateConLaiChiPhi();
        }

        private void LoadLoaiCongTrinh()
        {
            IEnumerable<VdtDmLoaiCongTrinh> listLoaiCongTrinh = _projectManagerService.GetAllDMLoaiCongTrinh();
            DataLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(listLoaiCongTrinh);
        }

        protected void OnAddHangMucChild()
        {
            if (Items == null || Items.Count == 0 || SelectedItem == null || SelectedItem.IsDeleted)
            {
                return;
            }
            string maSTT = GetSTTHangMuc(SelectedItem, true);

            DuToanDetailModel sourceItem = SelectedItem;
            DuToanDetailModel targetItem = ObjectCopier.Clone(sourceItem);

            targetItem.Id = Guid.Empty;
            targetItem.IdDuAnHangMuc = Guid.NewGuid();
            targetItem.IdDuToanHangMuc = null;
            targetItem.IdChiPhi = null;
            targetItem.GiaTriPheDuyet = 0;
            targetItem.IsHangCha = false;
            sourceItem.IsHangCha = true;
            sourceItem.GiaTriPheDuyet = 0;
            targetItem.HangMucParentId = sourceItem.IdDuAnHangMuc;
            targetItem.MaHangMuc = GetMaHangMuc();
            targetItem.TenHangMuc = string.Empty;
            targetItem.MaOrDer = maSTT;
            targetItem.IsHangMucOld = false;
            targetItem.GiaTriTruocDieuChinh = 0;
            targetItem.FGiaTriDieuChinh = 0;
            targetItem.PropertyChanged += DetailModel_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);
            UpdateListHangMucCanEdit();
            OnPropertyChanged(nameof(Items));
        }

        protected void OnAddHangMucDetail()
        {
            if (SelectedItem == null)
            {
                if (Items == null || Items.Count == 0)
                    SelectedItem = new DuToanDetailModel();
                else
                    SelectedItem = Items.FirstOrDefault();
            }


            DuToanDetailModel targetItem = new DuToanDetailModel()
            {
                IdDuAnHangMuc = Guid.NewGuid(),
                MaHangMuc = GetMaHangMuc(),
                IdChiPhi = DataChiPhiModel.IdChiPhiDuAn,
                GiaTriPheDuyet = 0,
                FTienPheDuyetQDDT = 0,
                IsHangCha = false,
                GiaTriTruocDieuChinh = 0,
                HangMucParentId = SelectedItem.HangMucParentId,
                TenHangMuc = string.Empty,
                IsHangMucOld = false,
            };
            targetItem.MaOrDer = GetSTTHangMuc(SelectedItem);
            targetItem.PropertyChanged += DetailModel_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);
            UpdateListHangMucCanEdit();
            OnPropertyChanged(nameof(Items));
        }

        public string GetSTTHangMuc(DuToanDetailModel objSeletected, bool isAddChild = false)
        {
            string sttHangMuc = string.Empty;
            int inDexSTTHangMucLast = 1;
            if (objSeletected == null && isAddChild == false)
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
            if (objSeletected != null && isAddChild == false)
            {
                DuToanDetailModel hangMucLast = new DuToanDetailModel();// lấy giá trị mặc định là giá trị đầu tiên
                if (Items != null && Items.Count != 0)
                    hangMucLast = Items.First();

                //tìm giá trị ngang hàng cuối cùng trong list => giá trị thêm mới được copy từ giá trị ngang hàng cuối cùng
                if (objSeletected.HangMucParentId == null)
                {
                    if (Items.Any(x => x.HangMucParentId == null))
                        hangMucLast = Items.Where(x => x.HangMucParentId == null).Last();
                    if (!string.IsNullOrEmpty(hangMucLast.MaOrDer))
                        inDexSTTHangMucLast = Int32.Parse(hangMucLast.MaOrDer);
                    else
                        inDexSTTHangMucLast = 0;
                    sttHangMuc = (inDexSTTHangMucLast + 1).ToString();
                }
                else
                {
                    if (Items.Any(x => x.HangMucParentId == objSeletected.HangMucParentId))
                        hangMucLast = Items.Where(x => x.HangMucParentId == objSeletected.HangMucParentId).Last();
                    string sTTHangMucLast = string.IsNullOrEmpty(hangMucLast.MaOrDer) ? string.Empty : hangMucLast.MaOrDer;
                    if (!string.IsNullOrEmpty(sTTHangMucLast))
                        inDexSTTHangMucLast = Int32.Parse(sTTHangMucLast.Substring(sTTHangMucLast.Length - 1));
                    else
                        inDexSTTHangMucLast = 0;
                    sttHangMuc = sTTHangMucLast.Substring(0, (sTTHangMucLast.Length - 1)) + (inDexSTTHangMucLast + 1).ToString();
                }
                // tìm dòng con cuối cùng của hạng mục ngang hàng => dòng được thêm sẽ là dòng bên dưới dòng con cuối cùng của hạng mục ngang hàng đó.
                List<DuToanDetailModel> listHangMucChild = new List<DuToanDetailModel>();
                if (hangMucLast.IdDuAnHangMuc.HasValue)
                    listHangMucChild.AddRange(FindListChildDelete(hangMucLast.IdDuAnHangMuc.Value));

                if (listHangMucChild == null || listHangMucChild.Count == 0)
                {
                    currentRow = Items.IndexOf(hangMucLast);
                }
                else
                {
                    currentRow = Items.IndexOf(listHangMucChild.Last());
                }
            }
            if (objSeletected != null && isAddChild == true)
            {
                var listChild = Items.Where(x => x.HangMucParentId == objSeletected.IdDuAnHangMuc).ToList();
                if (listChild == null || listChild.Count == 0)
                {
                    sttHangMuc = objSeletected.MaOrDer + "_1";
                    currentRow = Items.IndexOf(objSeletected);
                }
                else
                {
                    var hangMucChildLast = Items.Where(x => x.HangMucParentId == objSeletected.IdDuAnHangMuc).Last();
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
                    var listChildOfHangMucLast = FindListChildDelete(hangMucChildLast.IdDuAnHangMuc.Value);
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

        private string GetMaHangMuc()
        {
            string maDuAn = string.Empty;
            VdtDaDuAn duAn = _projectManagerService.FindById(Model.IIdDuAnId.Value);

            if (duAn != null)
            {
                if (duAn.SMaDuAn != null && duAn.SMaDuAn.Length > 4)
                {
                    maDuAn = duAn.SMaDuAn.Substring(duAn.SMaDuAn.Length - 4);
                }
            }

            int indexHangMuc = _vdtDuAnHangMucService.FindNextSoChungTuIndex();
            maDuAn = maDuAn + indexHangMuc.ToString("D3");

            return maDuAn;
        }

        private void CalculateData()
        {
            if (Items == null) return;
            foreach (var item in Items.Where(n => !n.HangMucParentId.HasValue))
            {
                CalculateParent(item);
            }
            OnPropertyChanged(nameof(Items));
        }

        private void CalculateParent(DuToanDetailModel parentItem)
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

        private void UpdateListHangMucCanEdit()
        {
            foreach (var item in Items)
            {
                item.IsEditHangMuc = true;
            }
            //update hàng cha có con thì ko đc edit
            foreach (var item in Items.Where(x => x.IsHangCha && !x.IsDeleted))
            {
                List<DuToanDetailModel> listChild = new List<DuToanDetailModel>();
                listChild = FindListChildDelete(item.IdDuAnHangMuc.Value);
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

        public List<DuToanDetailModel> FindListChildDelete(Guid parentId)
        {
            List<DuToanDetailModel> inner = new List<DuToanDetailModel>();
            foreach (var t in Items.Where(item => item.HangMucParentId == parentId))
            {
                inner.Add(t);
                inner = inner.Union(FindListChildDelete(t.Id)).ToList();
            }

            return inner;
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            DuToanDetailModel objectSender = (DuToanDetailModel)sender;
            if (SelectedItem == null)
            {
                return;
            }
            if (args.PropertyName == nameof(ApproveProjectDetailModel.GiaTriPheDuyet))
            {
                objectSender.FGiaTriDieuChinh = objectSender.GiaTriPheDuyet - objectSender.GiaTriTruocDieuChinh;
                CalculateData();
                CalculateConLaiChiPhi();

            }

            objectSender.IsModified = true;
            OnPropertyChanged(nameof(Items));
            //OnPropertyChanged(nameof(IsSaveData));
        }

        private void CalculateConLaiChiPhi()
        {
            double tongHangMuc = 0;
            List<DuToanDetailModel> listHangMuc = Items.Where(x => x.HangMucParentId == null && !x.IsDeleted).ToList();
            ConLai = 0;
            if (listHangMuc != null && listHangMuc.Count > 0)
            {
                tongHangMuc = listHangMuc.Sum(x => x.GiaTriPheDuyet.Value);
            }
            ConLai = tongHangMuc;
        }

        public override void OnSave(object obj)
        {
            foreach (var item in Items)
            {
                item.IdDuAnChiPhi = DataChiPhiModel.IdChiPhiDuAn;
                item.IdChiPhi = DataChiPhiModel.IdChiPhi;
            }
            OnPropertyChanged(nameof(Items));

            SavedAction?.Invoke(null);
            MessageBox.Show(Resources.MsgSaveDone);
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null)
            {
                if (SelectedItem.IdDuAnHangMuc.HasValue)
                {
                    var listDelete = FindListChildDelete(SelectedItem.IdDuAnHangMuc.Value);
                    listDelete.Add(SelectedItem);
                    foreach (var item in listDelete)
                    {
                        item.IsDeleted = !SelectedItem.IsDeleted;
                    }
                    //SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                    CalculateData();
                    CalculateConLaiChiPhi();
                    //OnPropertyChanged(nameof(IsSaveData));
                }
            }
        }

        public void OnShowPopUpChooseHangMuc()
        {
            if (SelectedItem == null || SelectedItem.IdDuAnHangMuc == null)
            {
                MessageBoxHelper.Warning(Resources.MsgErrHangMucPhanChiaChuaChon);
                return;
            }
            if (Items.Any(n => !n.IsDeleted && n.HangMucParentId == SelectedItem.IdDuAnHangMuc))
            {
                MessageBoxHelper.Warning(Resources.MsgErrorNotSplitParentCategory);
                return;
            }
            if (SelectedItem.GiaTriPheDuyet <= 0)
            {
                MessageBoxHelper.Warning(Resources.MsgErrHangMucPhanChiaChuaNhap);
                return;
            }
            TKTCVaTongDuToanHangMucDevideViewModel.Model = new Model.VdtDuToanModel();

            TKTCVaTongDuToanHangMucDevideViewModel.DataDuToanHangMucParent = GetListHangMucParentShowDevide(SelectedItem.IdDuAnHangMuc.Value);
            TKTCVaTongDuToanHangMucDevideViewModel.HangMucPhanChiaName = SelectedItem.TenHangMuc;
            TKTCVaTongDuToanHangMucDevideViewModel.HangMucPhanChiaValue = TongHMPhanChia;
            TKTCVaTongDuToanHangMucDevideViewModel.HangMucPhanChiaId = SelectedItem.IdDuAnHangMuc.Value;
            TKTCVaTongDuToanHangMucDevideViewModel.Init();
            TKTCVaTongDuToanHangMucDevideViewModel.SavedAction = obj => this.LoadDataDuToanHangMucPhanChia();
            TKTCVaTongDuToanHangMucDevideViewModel.ShowDialog();
        }

        public void LoadDataDuToanHangMucPhanChia()
        {
            //DataHangMucPhanChiaByHangMuc = new ObservableCollection<DuToanDetailModel>(TKTCVaTongDuToanHangMucDevideViewModel.DataDuToanHangMucParent.Where(x =>x.IsChecked).ToList());
            DataHangMucPhanChiaByHangMuc = new ObservableCollection<DuToanDetailModel>(TKTCVaTongDuToanHangMucDevideViewModel.DataDuToanHangMucParent.ToList());

            HangMucPhanChiaSaved = HangMucPhanChiaSaved.Where(x => x.IIdHangMucPhanChia != SelectedItem.IdDuAnHangMuc).ToList();

            HangMucPhanChiaSaved.AddRange(DataHangMucPhanChiaByHangMuc.ToList());
            //tìm list hạng mục đc phân chia mới và add vào item hạng mục
            List<DuToanDetailModel> listHangMucChecked = DataHangMucPhanChiaByHangMuc.Where(x => x.IsChecked && x.FGiaTriPhanChia != null && x.FGiaTriPhanChia > 0).ToList();
            List<DuToanDetailModel> listAdd = new List<DuToanDetailModel>();

            if (listHangMucChecked.Count > 0)
            {
                foreach (var item in listHangMucChecked)
                {
                    var objHangMucParent = Items.Where(x => x.IIdHangMucPhanChia == item.IIdHangMucPhanChia && x.IdDuAnHangMuc == item.IdDuAnHangMuc && x.IsSaved).FirstOrDefault();
                    if (objHangMucParent == null)
                    {
                        OnAddHangMucChildByHangMucPhanChia(item);
                    }
                    else
                    {
                        //objHangMuc.GiaTriPheDuyet = item.FGiaTriPhanChia;
                        var objHangMucDuocPhanChia = Items.Where(x => x.HangMucParentId == objHangMucParent.IdDuAnHangMuc && x.IIdHangMucPhanChia == item.IIdHangMucPhanChia).FirstOrDefault();
                        if (objHangMucDuocPhanChia != null)
                        {
                            objHangMucDuocPhanChia.GiaTriPheDuyet = item.FGiaTriPhanChia;
                            OnPropertyChanged(nameof(Items));
                        }
                    }
                }
            }
            SelectedItem.IsEditHangMuc = false;
        }

        public List<DuToanDetailModel> GetListHangMucParentShowDevide(Guid hangMucId)
        {
            var result = new List<DuToanDetailModel>();
            //check xem hạng mục đó đã lưu dữ liệu phân chia chưa, nếu chưa có dữ liệu lưu thì lấy data là tất cả hạng mục
            List<DuToanDetailModel> listHangMucPhanChiaSaved = HangMucPhanChiaSaved.Where(x => x.IIdHangMucPhanChia == hangMucId).ToList();
            var hangMucChaOfHangMucSelected = FindHangMucChaCuaHangMucPhanChia(SelectedItem);
            if (listHangMucPhanChiaSaved.Count < 1 && hangMucChaOfHangMucSelected != null)
            {
                result = Items.Where(x => x.HangMucParentId == null && x.IdDuAnHangMuc != hangMucChaOfHangMucSelected.IdDuAnHangMuc).ToList();

                TongHMPhanChia = SelectedItem.GiaTriPheDuyet;
            }
            else
            {
                result = listHangMucPhanChiaSaved;
                TongHMPhanChia = listHangMucPhanChiaSaved.Where(x => x.IsChecked && x.IIdHangMucPhanChia == hangMucId && x.FGiaTriPhanChia != null)
                                    .Sum(y => y.FGiaTriPhanChia) + SelectedItem.GiaTriPheDuyet;
            }
            // nếu hạng mục phân chia đã đc lưu trước đó thì load ra các hạng mục đc lưu trước đó và số tiền
            var listData = ObjectCopier.Clone(result);

            return listData;
        }

        public DuToanDetailModel FindHangMucChaCuaHangMucPhanChia(DuToanDetailModel objItem)
        {
            var result = new DuToanDetailModel();
            if (objItem.HangMucParentId.IsNullOrEmpty())
            {
                result = objItem;
            }
            else
            {
                var objParent = Items.Where(x => x.IdDuAnHangMuc == objItem.HangMucParentId).FirstOrDefault();
                if (objParent != null && !objParent.HangMucParentId.IsNullOrEmpty())
                {
                    result = FindHangMucChaCuaHangMucPhanChia(objParent);
                }
                else
                {
                    result = objParent;
                }
            }
            return result;
        }

        protected void OnAddHangMucChildByHangMucPhanChia(DuToanDetailModel objParent)
        {
            DuToanDetailModel sourceItem = Items.Where(x => x.IdDuAnHangMuc == objParent.IdDuAnHangMuc).FirstOrDefault();
            if (sourceItem == null)
            {
                return;
            }
            string maSTT = GetSTTHangMuc(sourceItem, true);

            DuToanDetailModel targetItem = ObjectCopier.Clone(sourceItem);

            targetItem.Id = Guid.Empty;
            targetItem.IdDuAnHangMuc = Guid.NewGuid();
            targetItem.IIdHangMucPhanChia = objParent.IIdHangMucPhanChia;
            targetItem.IdDuToanHangMuc = null;
            targetItem.IdChiPhi = DataChiPhiModel.IdChiPhiDuAn;
            targetItem.FTienPheDuyetQDDT = 0;
            targetItem.IsHangCha = false;
            targetItem.GiaTriPheDuyet = objParent.FGiaTriPhanChia;
            targetItem.FTienChenhLech = objParent.FGiaTriPhanChia;
            targetItem.GiaTriTruocDieuChinh = 0;
            targetItem.HangMucParentId = sourceItem.IdDuAnHangMuc;
            targetItem.MaHangMuc = GetMaHangMuc();
            targetItem.TenHangMuc = SelectedItem.TenHangMuc;
            targetItem.MaOrDer = maSTT;
            sourceItem.IsSaved = true;
            sourceItem.IsHangCha = true;
            targetItem.IsEditHangMuc = false;
            sourceItem.IsEditHangMuc = false;
            sourceItem.IIdHangMucPhanChia = objParent.IIdHangMucPhanChia;
            //targetItem.IsSaved = true;
            SelectedItem.GiaTriPheDuyet -= objParent.FGiaTriPhanChia;
            targetItem.PropertyChanged += DetailModel_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);

            //UpdateListHangMucCanEdit();
            CalculateData();
            CalculateConLaiChiPhi();
            OnPropertyChanged(nameof(Items));
        }
    }
}
