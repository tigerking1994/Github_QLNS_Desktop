using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.KHLuaChonNhaThau;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.KHLuaChonNhaThau
{
    public class KHLuaChonNhaThauDetailViewModel : DetailViewModelBase<VdtDaGoiThauModel, VdtKhlcNhaThauGoiThauDetailModel>
    {
        #region Private
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly IVdtQddtKhlcnhaThauService _service;
        private readonly IVdtDaDuToanService _duToanService;
        private readonly IVdtDaGoiThauService _vdtDaGoiThauService;
        private IMapper _mapper;
        Dictionary<Guid, List<ChiPhiHangMucModel>> _dicHangMuc;
        #endregion

        public override string Name => "THÔNG TIN GÓI THẦU CHI TIẾT";
        public override string Title => "Quản lý thông tin gói thầu chi tiết";
        public override Type ContentType => typeof(KHLuaChonNhaThauDetail);
        public bool BIsChuTruongDauTu => (SLoaiChungTu == LoaiKHLCNTType.CHU_TRUONG_DAU_TU) && !IsDetail;

        #region Header
        public string SHeaderGiaTriGoiThauNguonVon => BIsDieuChinh ? "Giá trị sau điều chỉnh" : "Giá trị nguồn vốn";
        public string SHeaderGiaTriGoiThauChiPhi => BIsDieuChinh ? "Giá trị sau điều chỉnh" : "Giá trị chi phí";
        public string SHeaderGiaTriGoiThauHangMuc => BIsDieuChinh ? "Giá trị sau điều chỉnh" : "Giá trị hạng mục";
        public string SHeaderNguonVon => string.Format("Giá trị phê duyệt {0}",
            SLoaiChungTu == LoaiKHLCNTType.DU_TOAN ? VDT_INITIAL_NAME.THIET_KE_THI_CONG_TDT :
            SLoaiChungTu == LoaiKHLCNTType.QDDT ? VDT_INITIAL_NAME.PHE_DUYET_DU_AN :
            VDT_INITIAL_NAME.CHU_TRUONG_DAU_TU);
        public string SHeaderChiPhi => string.Format("Giá trị phê duyệt {0}",
            SLoaiChungTu == LoaiKHLCNTType.DU_TOAN ? VDT_INITIAL_NAME.THIET_KE_THI_CONG_TDT :
            SLoaiChungTu == LoaiKHLCNTType.QDDT ? VDT_INITIAL_NAME.PHE_DUYET_DU_AN :
            VDT_INITIAL_NAME.CHU_TRUONG_DAU_TU);
        public string SHeaderHangMuc => string.Format("Giá trị phê duyệt {0}",
            SLoaiChungTu == LoaiKHLCNTType.DU_TOAN ? VDT_INITIAL_NAME.THIET_KE_THI_CONG_TDT :
            SLoaiChungTu == LoaiKHLCNTType.QDDT ? VDT_INITIAL_NAME.PHE_DUYET_DU_AN :
            VDT_INITIAL_NAME.CHU_TRUONG_DAU_TU);
        #endregion

        #region Items
        private bool _bIsDieuChinh;
        public bool BIsDieuChinh
        {
            get => _bIsDieuChinh;
            set => SetProperty(ref _bIsDieuChinh, value);
        }

        private string _sLoaiChungTu;
        public string SLoaiChungTu
        {
            get => _sLoaiChungTu;
            set => SetProperty(ref _sLoaiChungTu, value);
        }

        private ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel> _itemsNguonVon;
        public ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private VdtKhlcNhaThauGoiThauDetailModel _selectedNguonVon;
        public VdtKhlcNhaThauGoiThauDetailModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set => SetProperty(ref _selectedNguonVon, value);
        }

        private ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel> _itemsChiPhi;
        public ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel> ItemsChiPhi
        {
            get => _itemsChiPhi;
            set => SetProperty(ref _itemsChiPhi, value);
        }

        private ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel> _lstTempChiPhi;
        public ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel> LstTmpChiPhi
        {
            get => _lstTempChiPhi;
            set => SetProperty(ref _lstTempChiPhi, value);
        }

        private VdtKhlcNhaThauGoiThauDetailModel _selectedChiPhi;
        public VdtKhlcNhaThauGoiThauDetailModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
        }

        private ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel> _itemsHangMuc;
        public ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel> ItemsHangMuc
        {
            get => _itemsHangMuc;
            set => SetProperty(ref _itemsHangMuc, value);
        }

        private List<VdtKhlcNhaThauGoiThauDetailModel> _lstHangMuc;
        public List<VdtKhlcNhaThauGoiThauDetailModel> LstHangMuc
        {
            get => _lstHangMuc;
            set => SetProperty(ref _lstHangMuc, value);
        }

        // modify list này khi đang mở detail, lưu sẽ asign lại LstHangMuc
        private List<VdtKhlcNhaThauGoiThauDetailModel> _lstTempHangMuc;
        public List<VdtKhlcNhaThauGoiThauDetailModel> LstTmpHangMuc
        {
            get => _lstTempHangMuc;
            set => SetProperty(ref _lstTempHangMuc, value);
        }

        private bool _isDetail;
        public bool IsDetail
        {
            get => _isDetail;
            set => SetProperty(ref _isDetail, value);
        }

        private double _fSumNguonVon;
        public double FSumNguonVon
        {
            get => _fSumNguonVon;
            set => SetProperty(ref _fSumNguonVon, value);
        }

        private double _fSumChiPhi;
        public double FSumChiPhi
        {
            get => _fSumChiPhi;
            set => SetProperty(ref _fSumChiPhi, value);
        }

        private double _fSumHangMuc;
        public double FSumHangMuc
        {
            get => _fSumHangMuc;
            set => SetProperty(ref _fSumHangMuc, value);
        }

        private double _fSubNguonVonChiPhi;
        public double FSubNguonVonChiPhi
        {
            get => _fSubNguonVonChiPhi;
            set => SetProperty(ref _fSubNguonVonChiPhi, value);
        }

        private bool _selectAllChiPhi;
        public bool SelectAllChiPhi
        {
            get => (ItemsChiPhi == null || !ItemsChiPhi.Any()) ? false : ItemsChiPhi.All(item => item.IsChecked == true);
            set
            {
                SetProperty(ref _selectAllChiPhi, value);
                if (ItemsChiPhi != null)
                {
                    ItemsChiPhi.Select(c => { c.IsChecked = _selectAllChiPhi; return c; }).ToList();
                }
            }
        }

        private bool _selectAllNguonVon;
        public bool SelectAllNguonVon
        {
            get => (ItemsNguonVon == null || !ItemsNguonVon.Any()) ? false : ItemsNguonVon.All(item => item.IsChecked == true);
            set
            {
                SetProperty(ref _selectAllNguonVon, value);
                if (ItemsNguonVon != null)
                {
                    ItemsNguonVon.Select(c => { c.IsChecked = _selectAllNguonVon; return c; }).ToList();
                }
            }
        }

        private bool _selectAllHangMuc;
        public bool SelectAllHangMuc
        {
            get => (ItemsHangMuc == null || !ItemsHangMuc.Any()) ? false : ItemsHangMuc.All(item => item.IsChecked == true);
            set
            {
                SetProperty(ref _selectAllHangMuc, value);
                if (ItemsHangMuc != null)
                {
                    ItemsHangMuc.Select(c => { c.IsChecked = _selectAllHangMuc; return c; }).ToList();
                }
            }
        }
        #endregion

        public RelayCommand ShowHangMucDetailCommand { get; set; }
        public RelayCommand SaveDataCommand { get; set; }
        public RelayCommand AddChildChiPhiCommand { get; set; }
        public RelayCommand DeleteChildChiPhiCommand { get; set; }

        public KHLuaChonNhaThauDetailViewModel(
            IVdtQddtKhlcnhaThauService service,
            INsNguonNganSachService nguonVonService,
            IVdtDaDuToanService duToanService,
            IVdtDaGoiThauService vdtDaGoiThauService,
            IMapper mapper)
        {
            _duToanService = duToanService;
            _nguonVonService = nguonVonService;
            _service = service;
            _mapper = mapper;
            _vdtDaGoiThauService = vdtDaGoiThauService;

            SaveDataCommand = new RelayCommand(obj => OnSaveData(obj));
            ShowHangMucDetailCommand = new RelayCommand(obj => OnShowHangMucDetail());
            AddChildChiPhiCommand = new RelayCommand(obj => OnAddChiPhiChild());
            DeleteChildChiPhiCommand = new RelayCommand(obj => OnDeleteChiPhiChild());
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);

            ItemsHangMuc = new ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel>();
            OnPropertyChanged(nameof(ItemsHangMuc));

            foreach (var item in ItemsNguonVon)
            {
                item.FGiaTriConLaiShow = item.FGiaTriConLai - item.FGiaTriGoiThau;
                if (item.FGiaTriConLaiShow < 0)
                    item.FGiaTriConLaiShow = 0;
                item.PropertyChanged += NguonVon_PropertyChanged;
            }


            foreach (var item in ItemsChiPhi)
            {
                if (!BIsChuTruongDauTu)
                {
                    item.FGiaTriConLaiShow = item.FGiaTriConLai - item.FGiaTriGoiThau;
                    if (item.FGiaTriConLaiShow < 0)
                        item.FGiaTriConLaiShow = 0;
                }
                item.PropertyChanged += ChiPhi_PropertyChanged;
            }

            // clone LstHangMuc
            LstTmpHangMuc = new List<VdtKhlcNhaThauGoiThauDetailModel>(LstHangMuc);

            OnPropertyChanged(nameof(ItemsNguonVon));
            OnPropertyChanged(nameof(ItemsChiPhi));
            OnPropertyChanged(nameof(ItemsHangMuc));
            CaculatorNguonVon();
            CaculatorChiPhi();
            OnPropertyChanged(nameof(BIsChuTruongDauTu));
            OnPropertyChanged(nameof(BIsDieuChinh));
        }

        #region Event
        private void OnShowHangMucDetail()
        {
            if (SelectedChiPhi == null) return;
            ItemsHangMuc = new ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel>(OrderHangMuc(LstTmpHangMuc.Where(n => n.IIdChiPhiId == SelectedChiPhi.IIdChiPhiId).ToList()));
            var listHangMucChoosen = LstTmpHangMuc.Where(x => x.IsChecked).Select(x => x.IIdHangMucId);
            var listChiPhiChoosen = LstTmpChiPhi.Where(x => x.IsChecked).Select(x => x.IIdChiPhiId);
            foreach (var item in ItemsHangMuc)
            {
                item.FGiaTriConLaiShow = item.FGiaTriConLai - item.FGiaTriGoiThau;
                if (item.FGiaTriConLaiShow < 0)
                    item.FGiaTriConLaiShow = 0;
                if (listChiPhiChoosen.Contains(item.IIdChiPhiId))
                {
                    item.IsChecked = listHangMucChoosen.Contains(item.IIdHangMucId);                    
                }
                else
                {
                    item.IsChecked = SelectedChiPhi.IsChecked;
                }
                item.PropertyChanged += HangMuc_PropertyChanged;
                if (item.IsChecked)
                {
                    CheckedHangMuc(item);
                }
            }
            OnPropertyChanged(nameof(ItemsHangMuc));
        }

        private void OnSaveData(object obj)
        {
            if (FSubNguonVonChiPhi != 0)
            {
                var result = MessageBoxHelper.Confirm(Resources.MsgConfirmErrorChiPhiNotEqualNguonVon);
                if (result == MessageBoxResult.No) return;
            }

            // reassign LstHangMuc            
            LstHangMuc = new List<VdtKhlcNhaThauGoiThauDetailModel>(LstTmpHangMuc);

            SavedAction?.Invoke(null);
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }

        private void OnAddChiPhiChild()
        {
            if (SelectedChiPhi == null) return;
            var index = ItemsChiPhi.IndexOf(SelectedChiPhi);
            VdtKhlcNhaThauGoiThauDetailModel newItem = new VdtKhlcNhaThauGoiThauDetailModel();
            newItem.IsAdd = true;
            newItem.IIdGoiThauId = Model.Id;
            newItem.IIdParentId = SelectedChiPhi.IIdChiPhiId;
            newItem.IIdChiPhiGocId = SelectedChiPhi.IIdChiPhiGocId;
            newItem.IIdChiPhiId = Guid.NewGuid();
            newItem.PropertyChanged += ChiPhi_PropertyChanged;
            ItemsChiPhi.Insert(index + 1, newItem);
            SelectedChiPhi.IsHangCha = true;
            OnPropertyChanged(nameof(ItemsChiPhi));
        }

        private void OnDeleteChiPhiChild()
        {
            if (SelectedChiPhi == null || !SelectedChiPhi.IIdParentId.HasValue) return;
            SelectedChiPhi.IsDeleted = !SelectedChiPhi.IsDeleted;
        }
        #endregion

        #region Helper

        private void NguonVon_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var item = (VdtKhlcNhaThauGoiThauDetailModel)sender;

            if (args.PropertyName == nameof(VdtKhlcNhaThauGoiThauDetailModel.IsChecked)
                || args.PropertyName == nameof(VdtKhlcNhaThauGoiThauDetailModel.FGiaTriGoiThau))
            {
                item.FGiaTriConLaiShow = item.FGiaTriConLai - item.FGiaTriGoiThau;
                if (!item.IsChecked) item.FGiaTriGoiThau = 0;
                if (item.FGiaTriConLaiShow < 0)
                    item.FGiaTriConLaiShow = 0;
                CaculatorNguonVon();
            }
        }

        private void ChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var item = (VdtKhlcNhaThauGoiThauDetailModel)sender;

            if (args.PropertyName == nameof(VdtKhlcNhaThauGoiThauDetailModel.IsChecked)
                || args.PropertyName == nameof(VdtKhlcNhaThauGoiThauDetailModel.FGiaTriGoiThau)
                || args.PropertyName == nameof(VdtKhlcNhaThauGoiThauDetailModel.IsDeleted))
            {
                if (item.IsDeleted)
                {
                    item.IsChecked = false;
                    foreach (var child in ItemsChiPhi.Where(n => n.IIdParentId == item.IIdChiPhiId))
                        child.IsDeleted = true;
                }

                if (item.IsChecked)
                {
                    if(args.PropertyName != nameof(VdtKhlcNhaThauGoiThauDetailModel.FGiaTriGoiThau))
                    {
                        if (LstTmpHangMuc.Any(n => n.IIdChiPhiId == item.IIdChiPhiId))
                        {
                            foreach (var child in LstTmpHangMuc)
                            {
                                if (child.IIdChiPhiId == item.IIdChiPhiId)
                                {
                                    if (!child.IsChecked) 
                                    { 
                                        child.IsChecked = true; 
                                        child.IIdGoiThauId = Model.Id; 
                                    }
                                }
                            }
                        }
                    }
                    CheckedChiPhi(item);
                }
                else
                {
                    item.FGiaTriGoiThau = 0;
                    UnCheckedChiPhi(item);
                }
                if (!BIsChuTruongDauTu)
                {
                    item.FGiaTriConLaiShow = item.FGiaTriConLai - item.FGiaTriGoiThau;
                    if (item.FGiaTriConLaiShow < 0)
                        item.FGiaTriConLaiShow = 0;
                }
                CaculatorChiPhi();
            }            
        }

        private void HangMuc_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var item = (VdtKhlcNhaThauGoiThauDetailModel)sender;

            if (args.PropertyName == nameof(VdtKhlcNhaThauGoiThauDetailModel.IsChecked))
            {
                if (item.IsChecked)
                {
                    item.FGiaTriGoiThau = item.FGiaTriDuocDuyet;
                    CheckedHangMuc(item);
                }
                else
                {
                    item.FGiaTriGoiThau = 0;
                    UnCheckedHangMuc(item);
                }
                if (!BIsChuTruongDauTu)
                {
                    item.FGiaTriConLaiShow = item.FGiaTriConLai - item.FGiaTriGoiThau;
                    if (item.FGiaTriConLaiShow < 0)
                        item.FGiaTriConLaiShow = 0;
                }
                CaculatorHangMuc(item.IIdChiPhiId.Value);
            }
            //OnPropertyChanged(nameof(ItemsHangMuc));
        }

        // k cho phép lựa chọn hạng mục cha nếu có một số hạng mục con đã được check ở gói thầu khác
        private bool CheckParentHangMucWithoutAllChildren(Guid? hmParentId)
        {
            List<VdtKhlcNhaThauGoiThauDetailModel> children = LstHangMuc.Where(h => h.IIdParentId == hmParentId).ToList();
            VdtKhlcNhaThauGoiThauDetailModel parent = LstHangMuc.Find(h => h.IIdHangMucId == hmParentId);
            if (children.Count == 0) return true;
            if (parent.FGiaTriDuocDuyet == children.Sum(h => h.FGiaTriDuocDuyet) && children.Count > 0)
            {
                return true;
            }
            else return false;
        }

        private void CaculatorNguonVon()
        {
            FSumNguonVon = ItemsNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriGoiThau);
            OnPropertyChanged(nameof(FSumNguonVon));
            CaculatorDetail();
        }

        private void CaculatorChiPhi()
        {
            FSumChiPhi = ItemsChiPhi.Where(n => n.IsChecked && !n.IIdParentId.HasValue).Sum(n => n.FGiaTriGoiThau);
            if (ItemsNguonVon != null && ItemsNguonVon.Count == 1 && ItemsNguonVon.Any(n => n.IsChecked))
                ItemsNguonVon.FirstOrDefault().FGiaTriGoiThau = FSumChiPhi;
            OnPropertyChanged(nameof(FSumChiPhi));
            CaculatorDetail();
        }

        private void CaculatorHangMuc(Guid iIdChiPhiId)
        {
            //FSumHangMuc = ItemsHangMuc.Where(n => n.IsChecked && !n.IIdParentId.HasValue).Sum(n => n.FGiaTriGoiThau);
            //FSumHangMuc = ItemsHangMuc.Where(n => n.IsChecked).Sum(n => n.FGiaTriGoiThau);
            //var SumHangMucCha = ItemsHangMuc.Where(n => n.IsChecked && !n.IIdParentId.HasValue 
            //                 && ItemsHangMuc.Where(m => m.IIdParentId == n.IIdHangMucId).Any(n => n.IsChecked))
            //                        .Sum(n => n.FGiaTriGoiThau);
            //FSumHangMuc -= SumHangMucCha;

            FSumHangMuc = ItemsHangMuc.Where(n => n.IsChecked && !ItemsHangMuc.Any(m => m.IIdParentId == n.IIdHangMucId)).Sum(n => n.FGiaTriGoiThau);
            OnPropertyChanged(nameof(FSumHangMuc));
            SetChiPhiByHangMuc(iIdChiPhiId);
        }

        private void SetChiPhiByHangMuc(Guid iIdChiPhiId)
        {
            var objChiPhi = ItemsChiPhi.FirstOrDefault(n => n.IIdChiPhiId == iIdChiPhiId);
            if (objChiPhi == null) return;
            objChiPhi.FGiaTriGoiThau = FSumHangMuc;
            if (ItemsHangMuc.Any(n => n.IIdChiPhiId == iIdChiPhiId && n.IsChecked))
                objChiPhi.BHaveHangMuc = true;
            else
                objChiPhi.BHaveHangMuc = false;
            OnPropertyChanged(nameof(ItemsChiPhi));
        }

        private void CaculatorDetail()
        {
            FSubNguonVonChiPhi = FSumNguonVon - FSumChiPhi;
            OnPropertyChanged(nameof(FSubNguonVonChiPhi));
        }

        private void CheckedHangMuc(VdtKhlcNhaThauGoiThauDetailModel item)
        {
            if (CheckParentHangMucWithoutAllChildren(item.IIdHangMucId))
            {
                var objHangMuc = LstTmpHangMuc.FirstOrDefault(n => n.IIdHangMucId == item.IIdHangMucId);
                objHangMuc.IsChecked = true;
                objHangMuc.IIdGoiThauId = Model.Id;
                if (objHangMuc.IIdParentId.HasValue)
                    objHangMuc.IsCheckedchild = true;

                objHangMuc.FGiaTriGoiThau = item.FGiaTriGoiThau;

                // check all children of current checked hang muc, NẾU CHƯA ĐƯỢC CHECK
                if (ItemsHangMuc.Any(n => n.IIdParentId == item.IIdHangMucId) && item.IsCheckedchild == false)
                {
                    foreach (var child in ItemsHangMuc.Where(n => n.IIdParentId == item.IIdHangMucId && !n.IsChecked))
                    {
                        child.IsChecked = true;
                        child.IIdGoiThauId = Model.Id;
                    }

                    var childs = ItemsHangMuc.Where(n => n.IIdParentId == item.IIdHangMucId && n.IsChecked);
                    objHangMuc.FGiaTriGoiThau = childs.Sum(n => n.FGiaTriGoiThau);
                    //return;
                }
                if (!objHangMuc.IIdParentId.HasValue)
                    objHangMuc.IsCheckedchild = false;
                // khong co hang muc cha
                if (!item.IIdParentId.HasValue) return;
                // co hang muc cha nhung khong nam trong danh muc
                if (!ItemsHangMuc.Any(n => n.IIdHangMucId == item.IIdParentId)) return;

                // co danh muc cha
                var objParent = ItemsHangMuc.FirstOrDefault(n => n.IIdHangMucId == item.IIdParentId);
                objParent.FGiaTriGoiThau = ItemsHangMuc.Where(n => n.IsChecked && n.IIdParentId == item.IIdParentId).Sum(n => n.FGiaTriGoiThau);
                objParent.FGiaTriConLaiShow = objParent.FGiaTriDuocDuyet - item.FGiaTriGoiThau;
                if (objHangMuc.IsCheckedchild)
                {
                    objParent.IsCheckedchild = true;
                    //objParent.IsChecked = true;
                }
                // if all children checked => parent checked
                var itemChecked = ItemsHangMuc.Where(n => n.IsChecked && n.IIdParentId == item.IIdParentId && n.IIdGoiThauId == Model.Id);
                var itemAll = ItemsHangMuc.Where(n => n.IIdParentId == item.IIdParentId);
                if (itemChecked.Count() == itemAll.Count() && CheckParentHangMucWithoutAllChildren(objParent.IIdHangMucId))
                {
                    objParent.IsChecked = true;
                    objParent.IIdGoiThauId = Model.Id;
                }

                // update temp list (LstTmpHangMuc)
                LstTmpHangMuc.ForEach(t =>
                {
                    var corespondingHM = ItemsHangMuc.FirstOrDefault(h => h.IIdHangMucId == t.IIdHangMucId);
                    if (corespondingHM != null)
                    {
                        t.IsChecked = corespondingHM.IsChecked;
                        t.IIdGoiThauId = corespondingHM.IIdGoiThauId;
                    }
                });
            }
            else {
                item.IsChecked = false;
                MessageBoxHelper.Error(Resources.MsgErrorCannotCheckParent);
            }
        }

        private void UnCheckedHangMuc(VdtKhlcNhaThauGoiThauDetailModel item)
        {
            var objHangMuc = LstTmpHangMuc.FirstOrDefault(n => n.IIdHangMucId == item.IIdHangMucId);
            objHangMuc.FGiaTriGoiThau = 0;
            
            objHangMuc.IsChecked = false;
            objHangMuc.IIdGoiThauId = null;
            //objHangMuc.FGiaTriConLaiShow = objHangMuc.FGiaTriDuocDuyet;
            item.FGiaTriGoiThau = 0;
            // co hang muc cha
            if (item.IIdParentId.HasValue)
            {
                var objParent = ItemsHangMuc.FirstOrDefault(n => n.IIdHangMucId == item.IIdParentId);
                if(objParent != null)
                {
                    objParent.FGiaTriGoiThau = ItemsHangMuc.Where(n => n.IIdParentId == item.IIdParentId && n.IsChecked).Sum(n => n.FGiaTriGoiThau);
                    if (!ItemsHangMuc.Any(n => n.IIdParentId == item.IIdParentId && n.IsChecked))
                    {
                        objParent.IsChecked = false;
                        objParent.IIdGoiThauId = null;
                    }
                }
            }
            if (!ItemsHangMuc.Any(n => n.IIdParentId == item.IIdHangMucId)) return;
            // Nếu là cha -> uncheck all children
            foreach (var child in ItemsHangMuc.Where(n => n.IIdParentId == item.IIdHangMucId))
            {
                child.IsChecked = false;
                child.IIdGoiThauId = null;
            }


            //if (!item.IIdParentId.HasValue) return;
            if (!ItemsHangMuc.Any(n => n.IIdParentId == item.IIdParentId && !n.IsChecked))
            {
                var objParent = ItemsHangMuc.FirstOrDefault(n => n.IIdHangMucId == item.IIdParentId);
                if (objParent != null)
                {
                    objParent.IsChecked = false;
                    objParent.IIdGoiThauId = null;
                }
            }
                
            // update temp list (LstTmpHangMuc)
            LstTmpHangMuc.ForEach(t =>
            {
                var corespondingHM = ItemsHangMuc.FirstOrDefault(h => h.IIdHangMucId == t.IIdHangMucId);
                if (corespondingHM != null)
                {
                    t.IsChecked = corespondingHM.IsChecked;
                    t.IIdGoiThauId = corespondingHM.IIdGoiThauId;
                }
            });
        }

        private void CheckedChiPhi(VdtKhlcNhaThauGoiThauDetailModel item)
        {            
            if (item.FGiaTriGoiThau == 0 && item.FGiaTriDuocDuyet > 0)
                item.FGiaTriGoiThau = item.FGiaTriConLai;
            if (!item.IIdParentId.HasValue) return;
            if (!ItemsChiPhi.Any(n => n.IIdChiPhiId == item.IIdParentId)) return;
            var objParent = ItemsChiPhi.FirstOrDefault(n => n.IIdChiPhiId == item.IIdParentId);
            objParent.FGiaTriGoiThau = ItemsChiPhi.Where(n => n.IsChecked && n.IIdParentId == item.IIdParentId).Sum(n => n.FGiaTriGoiThau);
            objParent.IsChecked = true;            
        }

        private void UnCheckedChiPhi(VdtKhlcNhaThauGoiThauDetailModel item)
        {
            item.FGiaTriGoiThau = 0;
            if (LstTmpHangMuc.Any(n => n.IIdChiPhiId == item.IIdChiPhiId))
            {
                foreach (var child in LstTmpHangMuc)
                {
                    if(child.IIdChiPhiId == item.IIdChiPhiId)
                    {
                        child.IsChecked = false;
                        child.IIdGoiThauId = null;
                        child.FGiaTriGoiThau = 0;
                    }
                }
            }

            if (item.IIdParentId.HasValue)
            {
                var objParent = ItemsChiPhi.FirstOrDefault(n => n.IIdChiPhiId == item.IIdParentId);
                objParent.FGiaTriGoiThau = ItemsChiPhi.Where(n => n.IIdParentId == item.IIdParentId && n.IsChecked).Sum(n => n.FGiaTriGoiThau);
                if (!ItemsChiPhi.Any(n => n.IIdParentId == item.IIdParentId && n.IsChecked))
                    objParent.IsChecked = false;
            }
            if (!ItemsChiPhi.Any(n => n.IIdParentId == item.IIdChiPhiId)) return;
            foreach (var child in ItemsChiPhi.Where(n => n.IIdParentId == item.IIdChiPhiId))
                child.IsChecked = false;
            if (!item.IIdParentId.HasValue) return;
            if (!ItemsChiPhi.Any(n => n.IIdParentId == item.IIdParentId && !n.IsChecked))
            {
                var objParent = ItemsChiPhi.FirstOrDefault(n => n.IIdChiPhiId == item.IIdParentId);
                objParent.IsChecked = false;
            }
        }

        private List<VdtKhlcNhaThauGoiThauDetailModel> OrderHangMuc(List<VdtKhlcNhaThauGoiThauDetailModel> lstData)
        {
            if (lstData == null) return new List<VdtKhlcNhaThauGoiThauDetailModel>();
            List<VdtKhlcNhaThauGoiThauDetailModel> results = new List<VdtKhlcNhaThauGoiThauDetailModel>();
            foreach (var item in lstData.Where(n => !n.IIdParentId.HasValue))
            {
                results.AddRange(RecursiveHangMuc(item, lstData));
            }
            return results;
        }

        private List<VdtKhlcNhaThauGoiThauDetailModel> RecursiveHangMuc(VdtKhlcNhaThauGoiThauDetailModel item, List<VdtKhlcNhaThauGoiThauDetailModel> lstData)
        {
            List<VdtKhlcNhaThauGoiThauDetailModel> results = new List<VdtKhlcNhaThauGoiThauDetailModel>();
            var lstChild = lstData.Where(n => n.IIdParentId == item.IIdHangMucId);
            item.IsHangCha = lstChild.Count() != 0;
            results.Add(item);
            if (!item.IsHangCha) return results;

            foreach (var child in lstChild)
            {
                results.AddRange(RecursiveHangMuc(child, lstData));
            }
            return results;
        }


        //#region Tree
        //private void UnCheckedChiPhi(VdtKhlcNhaThauGoiThauDetailModel item)
        //{
        //    item.FGiaTriGoiThau = 0;
        //    if (LstHangMuc.Any(n => n.IIdChiPhiId == item.IIdChiPhiId))
        //    {
        //        foreach (var child in LstHangMuc)
        //        {
        //            child.IsChecked = false;
        //            child.FGiaTriGoiThau = 0;
        //        }
        //    }

        //    if (item.IIdParentId.HasValue)
        //    {
        //        var objParent = ItemsChiPhi.FirstOrDefault(n => n.IIdChiPhiId == item.IIdParentId);
        //        objParent.FGiaTriGoiThau = ItemsChiPhi.Where(n => n.IIdParentId == item.IIdParentId && (n.IsChecked ?? true)).Sum(n => n.FGiaTriGoiThau);
        //        if (!ItemsChiPhi.Any(n => n.IIdParentId == item.IIdParentId && (n.IsChecked ?? true)))
        //            objParent.IsChecked = false;
        //    }
        //    if (!ItemsChiPhi.Any(n => n.IIdParentId == item.IIdChiPhiId)) return;
        //    foreach (var child in ItemsChiPhi.Where(n => n.IIdParentId == item.IIdChiPhiId))
        //        child.IsChecked = false;
        //    if (!item.IIdParentId.HasValue) return;
        //    if (!ItemsChiPhi.Any(n => n.IIdParentId == item.IIdParentId && !n.IsChecked))
        //    {
        //        var objParent = ItemsChiPhi.FirstOrDefault(n => n.IIdChiPhiId == item.IIdParentId);
        //        objParent.IsChecked = false;
        //    }
        //}

        //private void CheckedChiPhi(VdtKhlcNhaThauGoiThauDetailModel item)
        //{
        //    if (!item.IIdParentId.HasValue) return;
        //    if (!ItemsChiPhi.Any(n => n.IIdChiPhiId == item.IIdParentId)) return;
        //    var objParent = ItemsChiPhi.FirstOrDefault(n => n.IIdChiPhiId == item.IIdParentId);
        //    objParent.FGiaTriGoiThau = ItemsChiPhi.Where(n => (n.IsChecked ?? true) && n.IIdParentId == item.IIdParentId).Sum(n => n.FGiaTriGoiThau);
        //    objParent.IsChecked = true;
        //}

        //private void SetChiPhiByHangMuc(Guid iIdChiPhiId)
        //{
        //    var objChiPhi = ItemsChiPhi.FirstOrDefault(n => n.IIdChiPhiId == iIdChiPhiId);
        //    if (objChiPhi == null) return;
        //    objChiPhi.FGiaTriGoiThau = FSumHangMuc;
        //    if (ItemsHangMuc.Any(n => n.IIdChiPhiId == iIdChiPhiId && (n.IsChecked ?? true)))
        //        objChiPhi.BHaveHangMuc = true;
        //    else
        //        objChiPhi.BHaveHangMuc = false;
        //    OnPropertyChanged(nameof(ItemsChiPhi));
        //}

        //private void CaculatorHangMuc(Guid iIdChiPhiId)
        //{
        //    FSumHangMuc = ItemsHangMuc.Where(n => (n.IsChecked ?? true) && !n.IIdParentId.HasValue).Sum(n => n.FGiaTriGoiThau);
        //    OnPropertyChanged(nameof(FSumHangMuc));
        //    SetChiPhiByHangMuc(iIdChiPhiId);
        //}

        //private void CaculatorChiPhi()
        //{
        //    FSumChiPhi = ItemsChiPhi.Where(n => (n.IsChecked ?? true) && !n.IIdParentId.HasValue).Sum(n => n.FGiaTriGoiThau);
        //    if (ItemsNguonVon != null && ItemsNguonVon.Count == 1 && ItemsNguonVon.Any(n => (n.IsChecked ?? false)))
        //        ItemsNguonVon.FirstOrDefault().FGiaTriGoiThau = FSumChiPhi;
        //    OnPropertyChanged(nameof(FSumChiPhi));
        //    CaculatorDetail();
        //}

        //private void CaculatorNguonVon()
        //{
        //    FSumNguonVon = ItemsNguonVon.Where(n => (n.IsChecked ?? false)).Sum(n => n.FGiaTriGoiThau);
        //    OnPropertyChanged(nameof(FSumNguonVon));
        //    CaculatorDetail();
        //}

        //private void CheckedHangMuc(VdtKhlcNhaThauGoiThauDetailModel item)
        //{
        //    if (!item.IIdParentId.HasValue) return;
        //    if (!ItemsHangMuc.Any(n => n.IIdHangMucId == item.IIdParentId)) return;
        //    var objParent = ItemsHangMuc.FirstOrDefault(n => n.IIdHangMucId == item.IIdParentId);
        //    objParent.FGiaTriGoiThau = ItemsHangMuc.Where(n => (n.IsChecked ?? true) && n.IIdParentId == item.IIdParentId).Sum(n => n.FGiaTriGoiThau);
        //    objParent.IsChecked = true;
        //}

        //private void CheckUpChiPhi(Guid? parentId)
        //{
        //    if (!parentId.HasValue) return;
        //    var objParent = ItemsChiPhi.FirstOrDefault(n => n.IIdChiPhiId == parentId);

        //    if (!ItemsChiPhi.Any(n => n.IIdParentId == parentId && !(n.IsChecked ?? false)))
        //        objParent.IsChecked = true;
        //    else if (!ItemsChiPhi.Any(n => n.IIdParentId == parentId && (n.IsChecked ?? true)))
        //        objParent.IsChecked = false;
        //    else
        //        objParent.IsChecked = null;

        //    objParent.FGiaTriGoiThau = ItemsChiPhi.Where(n => (n.IsChecked ?? true) && !n.IsDeleted).Sum(n => n.FGiaTriGoiThau);

        //    CheckUpChiPhi(ItemsChiPhi.FirstOrDefault().IIdParentId);
        //}

        //private void CheckDownChiPhi(VdtKhlcNhaThauGoiThauDetailModel item, bool bLast = false)
        //{
        //    if (!(item.IsChecked ?? true) || item.IsDeleted)
        //        ItemsChiPhi.FirstOrDefault(n => n.IIdChiPhiId == item.IIdChiPhiId).FGiaTriGoiThau = 0;

        //    foreach (var child in ItemsChiPhi.Where(n => n.IIdParentId == item.IIdChiPhiId))
        //    {
        //        if (item.IsChecked.HasValue)
        //            child.IsChecked = item.IsChecked;
        //        child.IsDeleted = item.IsDeleted;
        //        CheckDownChiPhi(child, bLast);
        //    }
        //    bLast = true;
        //    if (bLast && !ItemsChiPhi.Any(n => n.IIdParentId == item.IIdChiPhiId))
        //        CheckUpChiPhi(item.IIdParentId);
        //}

        //private void CheckUpHangMuc(Guid? parentId)
        //{
        //    if (!parentId.HasValue) return;
        //    var objParent = ItemsHangMuc.FirstOrDefault(n => n.IIdHangMucId == parentId);

        //    if (!ItemsHangMuc.Any(n => n.IIdParentId == parentId && !(n.IsChecked ?? false)))
        //        objParent.IsChecked = true;
        //    else if (!ItemsHangMuc.Any(n => n.IIdParentId == parentId && (n.IsChecked ?? true)))
        //        objParent.IsChecked = false;
        //    else
        //        objParent.IsChecked = null;

        //    objParent.FGiaTriGoiThau = ItemsHangMuc.Where(n => (n.IsChecked ?? true) && !n.IsDeleted).Sum(n => n.FGiaTriGoiThau);

        //    CheckUpHangMuc(ItemsHangMuc.FirstOrDefault().IIdParentId);
        //}

        //private void CheckDownHangMuc(VdtKhlcNhaThauGoiThauDetailModel item)
        //{
        //    if (!(item.IsChecked ?? true) || item.IsDeleted)
        //        ItemsHangMuc.FirstOrDefault(n => n.IIdHangMucId == item.IIdHangMucId).FGiaTriGoiThau = 0;

        //    foreach (var child in ItemsHangMuc.Where(n => n.IIdParentId == item.IIdHangMucId))
        //    {
        //        child.IsChecked = item.IsChecked;
        //        child.IsDeleted = item.IsDeleted;
        //        CheckDownHangMuc(child);
        //    }
        //}
        //#endregion
        #endregion
    }
}
