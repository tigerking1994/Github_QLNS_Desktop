using log4net;
using System;
using AutoMapper;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.Utility;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Model;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTPlanImport;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTPlanImport
{
    public class MSNTNHPhuongAnNhapKhauDetailViewModel : DialogCurrencyAttachmentViewModelBase<NhDaGoiThauModel>
    {
        private readonly INhDaDuAnNguonVonService _nhDaDuAnNguonVonService;
        private readonly INhDaChuTruongDauTuService _nhDaChuTruongDauTuService;
        private readonly INhDaChuTruongDauTuNguonVonService _nhDaChuTruongDauTuNguonVonService;

        private readonly INhDaQdDauTuService _qdDauTuService;
        private readonly INhDaQdDauTuNguonVonService _nhDaQdDauTuNguonVonService;
        private readonly INhDaQdDauTuHangMucService _nhDaQdDauTuHangMucService;
        private readonly INhDaQdDauTuChiPhiService _nhDaQdDauTuChiPhiService;

        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private readonly INhDaGoiThauNguonVonService _nhDaGoiThauNguonVonService;
        private readonly INhDaGoiThauChiPhiService _nhDaGoiThauChiPhiService;
        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucSerrvice;
        private readonly INhDaDuAnHangMucService _nhDaDuAnHangMucService;

        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private IMapper _mapper;

        public override string Name => "THÔNG TIN GÓI THẦU CHI TIẾT";
        public override string Title => "Quản lý thông tin gói thầu chi tiết";
        public override Type ContentType => typeof(MSNTNHPhuongAnNhapKhauDetail);
        public bool IsDetail { get; set; }
        public bool IsAdded { get; set; }
        public bool IsEnableSoCuChuongTrinh { get; set; }
        public string SLoaiSoCu { get; set; }
        public string SoPANK { get; set; }

        private bool _isSelectedNguonVon;
        public bool IsSelectedNguonVon {
            get => _isSelectedNguonVon;
            set => SetProperty(ref _isSelectedNguonVon, value);
        }

        private ObservableCollection<NguonNganSachModel> _itemsNguonVon;
        public ObservableCollection<NguonNganSachModel> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private ObservableCollection<NhDaGoiThauNguonVonModel> _itemsGoiThauNguonVon;
        public ObservableCollection<NhDaGoiThauNguonVonModel> ItemsGoiThauNguonVon
        {
            get => _itemsGoiThauNguonVon;
            set => SetProperty(ref _itemsGoiThauNguonVon, value);
        }

        private NhDaGoiThauNguonVonModel _selectedGoiThauNguonVon;
        public NhDaGoiThauNguonVonModel SelectedGoiThauNguonVon
        {
            get => _selectedGoiThauNguonVon;
            set => SetProperty(ref _selectedGoiThauNguonVon, value);
        }

        private NhDaGoiThauNguonVonModel _tongTienGoiThauNguonVon;
        public NhDaGoiThauNguonVonModel TongTienGoiThauNguonVon
        {
            get => _tongTienGoiThauNguonVon;
            set => SetProperty(ref _tongTienGoiThauNguonVon, value);
        }

        private ObservableCollection<NhDaGoiThauChiPhiModel> _itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>();
        public ObservableCollection<NhDaGoiThauChiPhiModel> ItemsChiPhi
        {
            get => _itemsChiPhi;
            set => SetProperty(ref _itemsChiPhi, value);
        }

        private ObservableCollection<NhDaGoiThauChiPhiModel> _itemsChiPhiTemp = new ObservableCollection<NhDaGoiThauChiPhiModel>();
        public ObservableCollection<NhDaGoiThauChiPhiModel> ItemsChiPhiTemp
        {
            get => _itemsChiPhiTemp;
            set => SetProperty(ref _itemsChiPhiTemp, value);
        }

        private NhDaGoiThauChiPhiModel _selectedChiPhi;
        public NhDaGoiThauChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
        }

        private NhDaGoiThauChiPhiModel _tongTienChiPhi;
        public NhDaGoiThauChiPhiModel TongTienChiPhi
        {
            get => _tongTienChiPhi;
            set => SetProperty(ref _tongTienChiPhi, value);
        }

        private bool _selectAllNguonVon;
        public bool SelectAllNguonVon
        {
            get => ItemsGoiThauNguonVon.IsEmpty() ? false : ItemsGoiThauNguonVon.All(item => item.IsChecked == true);
            set
            {
                SetProperty(ref _selectAllNguonVon, value);
                if (ItemsGoiThauNguonVon != null)
                {
                    ItemsGoiThauNguonVon.Select(c => { c.IsChecked = _selectAllNguonVon; return c; }).ToList();
                }
            }
        }

        // Nguồn vốn
        public RelayCommand AddDuAnGoiThauNguonVonCommand { get; }
        public RelayCommand DeleteDuAnGoiThauNguonVonCommand { get; }

        // Chi phí
        public RelayCommand AddChiPhiCommand { get; set; }
        public RelayCommand DeleteChiPhiCommand { get; set; }
        public RelayCommand ShowHangMucDetailCommand { get; set; }
        public RelayCommand SaveDataCommand { get; set; }   

        public MSNTNHPhuongAnNhapKhauItemDialogViewModel MSNTNHPhuongAnNhapKhauItemDialogViewModel { get; }

        private Dictionary<string, NhDaDuAnHangMuc> _ttdaDuanHangMuc = null;

        public MSNTNHPhuongAnNhapKhauDetailViewModel
        (
            INhDaGoiThauService nhDaGoiThauService,
            INhDaGoiThauChiPhiService nhDaGoiThauChiPhiService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucSerrvice,
            INhDaGoiThauNguonVonService nhDaGoiThauNguonVonService,
            INhDaDuAnNguonVonService nhDaDuAnNguonVonService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhDaDuAnHangMucService nhDaDuAnHangMucService,
            IMapper mapper,
            INhDaQdDauTuService qdDauTuService,
            INhDaQdDauTuNguonVonService nhDaQdDauTuNguonVonService,
            INhDaQdDauTuHangMucService nhDaQdDauTuHangMucService,
            INhDaQdDauTuChiPhiService nhDaQdDauTuChiPhiService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDaChuTruongDauTuService nhDaChuTruongDauTuService,
            INhDaChuTruongDauTuNguonVonService nhDaChuTruongDauTuNguonVonService,
            MSNTNHPhuongAnNhapKhauItemDialogViewModel msntnhPhuongAnNhapKhauItemDialogViewModel
        ) : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _nhDaGoiThauService = nhDaGoiThauService;
            _nhDaGoiThauChiPhiService = nhDaGoiThauChiPhiService;
            _nhDaGoiThauHangMucSerrvice = nhDaGoiThauHangMucSerrvice;
            _nhDaGoiThauNguonVonService = nhDaGoiThauNguonVonService;
            _mapper = mapper;
            _nhDaDuAnNguonVonService = nhDaDuAnNguonVonService;
            _qdDauTuService = qdDauTuService;
            _nhDaQdDauTuNguonVonService = nhDaQdDauTuNguonVonService;
            _nhDaQdDauTuHangMucService = nhDaQdDauTuHangMucService;
            _nhDaQdDauTuChiPhiService = nhDaQdDauTuChiPhiService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDaChuTruongDauTuService = nhDaChuTruongDauTuService;
            _nhDaChuTruongDauTuNguonVonService = nhDaChuTruongDauTuNguonVonService;
            _nhDaDuAnHangMucService = nhDaDuAnHangMucService;

            MSNTNHPhuongAnNhapKhauItemDialogViewModel = msntnhPhuongAnNhapKhauItemDialogViewModel;
            MSNTNHPhuongAnNhapKhauItemDialogViewModel.ParentPage = this;

            AddDuAnGoiThauNguonVonCommand = new RelayCommand(obj => OnAddDuAnGoiThauNguonVon());
            DeleteDuAnGoiThauNguonVonCommand = new RelayCommand(obj => OnDeleteDuAnGoiThauNguonVon(), s => SelectedGoiThauNguonVon != null);
            AddChiPhiCommand = new RelayCommand(obj => OnAddGoiThauChiPhi(obj), obj => (bool)obj || SelectedChiPhi != null);
            DeleteChiPhiCommand = new RelayCommand(obj => OnDeleteGoiThauChiPhi(), obj => SelectedChiPhi != null);
            SaveDataCommand = new RelayCommand(obj => OnSaveData(obj));
            ShowHangMucDetailCommand = new RelayCommand(obj => OnShowHangMucDetail());
        }

        public override void Init()
        {
            LoadDefault();
            LoadNguonVon();
            LoadDuAnGoiThauNguonVon();
            LoadGoiThauChiPhi(SelectedGoiThauNguonVon);
            Console.WriteLine(IsEnableSoCuChuongTrinh);
        }

        private void LoadDefault()
        {
            _tongTienGoiThauNguonVon = new NhDaGoiThauNguonVonModel();
            _tongTienChiPhi = new NhDaGoiThauChiPhiModel();
            _itemsNguonVon = new ObservableCollection<NguonNganSachModel>();
            _itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>();
            _ttdaDuanHangMuc = new Dictionary<string, NhDaDuAnHangMuc>();
        }

        private void LoadNguonVon()
        {
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonVon = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private void LoadDuAnGoiThauNguonVon()
        {
            _itemsGoiThauNguonVon = new ObservableCollection<NhDaGoiThauNguonVonModel>();
            if(Model.GoiThauNguonVons.Count() > 0)
            {
                _itemsGoiThauNguonVon = Model.GoiThauNguonVons;
                foreach (var item in _itemsGoiThauNguonVon)
                {
                    item.PropertyChanged += GoiThauNguonVon_PropertyChanged;
                    item.IsSaved = true;
                    SelectedGoiThauNguonVon = item;
                    LoadGoiThauChiPhi(item);
                }
            }
            CaculatorTotalNguonVon();
            SetEnableItemsNguonVon();
            OnPropertyChanged(nameof(ItemsGoiThauNguonVon));
        }

        private void LoadGoiThauChiPhi(NhDaGoiThauNguonVonModel nguonVon)
        {
            //cap nhat, dieu chinh, temp them moi
            if (Model.GoiThauNguonVons.Count() > 0 && _itemsChiPhiTemp.Count == 0)
            {
                foreach (var item in Model.GoiThauNguonVons)
                {                        
                    item.GoiThauChiPhis.ForAll(s =>
                    {
                        if(nguonVon.STenNguonVon != null)
                        s.STenNguonVon = ItemsNguonVon.Where(n=> n.IIdMaNguonNganSach.GetValueOrDefault().Equals(nguonVon.IIdNguonVonId.GetValueOrDefault())).FirstOrDefault().STen;
                        s.IsSaved = item.IsSaved;
                        if(s.IIdGoiThauNguonVonId == nguonVon.Id)
                            //_itemsChiPhi.Add(s);
                            _itemsChiPhiTemp.Add(s);
                    });
                }
            }
            //temp chi phi
            else if (_itemsChiPhiTemp.Count > 0 && _itemsChiPhiTemp.Clone().Select(n => n.IIdGoiThauNguonVonId).Contains(nguonVon.Id))
            {
                foreach (var item in _itemsChiPhiTemp.Clone())
                {
                    if (item.IIdGoiThauNguonVonId == nguonVon.Id)
                    {
                        item.STenNguonVon = ItemsNguonVon.Where(n => n.IIdMaNguonNganSach.GetValueOrDefault().Equals(nguonVon.IIdNguonVonId.GetValueOrDefault())).FirstOrDefault().STen;
                        if(!_itemsChiPhi.Clone().Select(n => n.Id).Contains(item.Id))
                            _itemsChiPhi.Add(item);
                    }                      
                }
            }
            _itemsChiPhi.ForAll(s =>
            {
                s.PropertyChanged += GoiThauChiPhi_PropertyChanged;
            });

            OrderItemsChiPhi();
            _itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>(_itemsChiPhi.OrderBy(s => s.SMaOrder));
            UpdateTreeItemsChiPhi();
            CaculatorTotalChiPhi();
            OnPropertyChanged(nameof(ItemsChiPhi));
        }

        private void OnAddDuAnGoiThauNguonVon()
        {
            if (_itemsGoiThauNguonVon == null) _itemsGoiThauNguonVon = new ObservableCollection<NhDaGoiThauNguonVonModel>();

            int currentRow = -1;
            if (!_itemsGoiThauNguonVon.IsEmpty())
            {
                currentRow = 0;
                if (SelectedGoiThauNguonVon != null)
                {
                    currentRow = _itemsGoiThauNguonVon.IndexOf(SelectedGoiThauNguonVon);
                }
            }

            NhDaGoiThauNguonVonModel targetItem = new NhDaGoiThauNguonVonModel();
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.IsSaved = false;
            targetItem.PropertyChanged += GoiThauNguonVon_PropertyChanged;
            _itemsGoiThauNguonVon.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(ItemsGoiThauNguonVon));
        }

        private void OnDeleteDuAnGoiThauNguonVon()
        {
            if (SelectedGoiThauNguonVon != null)
            {
                SelectedGoiThauNguonVon.IsDeleted = !SelectedGoiThauNguonVon.IsDeleted;
                OnPropertyChanged(nameof(ItemsGoiThauNguonVon));
                CaculatorTotalNguonVon();
            }
        }

        private void OnAddGoiThauChiPhi(object obj)
        {
            if (_itemsChiPhi == null) _itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>();

            NhDaGoiThauChiPhiModel sourceItem = _selectedChiPhi;
            NhDaGoiThauChiPhiModel targetItem = new NhDaGoiThauChiPhiModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!_itemsChiPhi.IsEmpty())
            {
                if (sourceItem != null)
                {
                    currentRow = _itemsChiPhi.IndexOf(sourceItem) + CountTreeChildItems(sourceItem);
                }
                else
                {
                    // Thêm vào cuối danh sách
                    currentRow = _itemsChiPhi.Count() - 1;
                }
            }

            if (sourceItem != null)
            {
                targetItem.IIdParentId = isParent ? sourceItem.IIdParentId : sourceItem.Id;
                // targetItem.IIdGoiThauNguonVonId = sourceItem.IIdGoiThauNguonVonId;
               // targetItem.STenChiPhi = sourceItem.STenChiPhi;
            } 
            targetItem.IIdGoiThauNguonVonId = SelectedGoiThauNguonVon.Id;
            targetItem.STenNguonVon = ItemsNguonVon.Where(n => n.IIdMaNguonNganSach.GetValueOrDefault().Equals(SelectedGoiThauNguonVon.IIdNguonVonId.GetValueOrDefault())).FirstOrDefault().STen; 
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.IsSaved = false;
            targetItem.Id = Guid.NewGuid();
            targetItem.PropertyChanged += GoiThauChiPhi_PropertyChanged;
            _itemsChiPhi.Insert(currentRow + 1, targetItem);

            var rawGoiThauChiPhis = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == SelectedGoiThauNguonVon.Id)?.GoiThauChiPhis;
            if(rawGoiThauChiPhis != null)
            {
                rawGoiThauChiPhis.Add(targetItem);
            }
            OrderItemsChiPhi(targetItem.IIdParentId);
            UpdateTreeItemsChiPhi();
            OnPropertyChanged(nameof(ItemsChiPhi));
        }

        private void OnDeleteGoiThauChiPhi()
        {
            if (SelectedChiPhi != null)
            {
                DeleteTreeItemsChiPhi(SelectedChiPhi, !SelectedChiPhi.IsDeleted);
                CaculatorTotalChiPhi();
            }
        }

        private void GoiThauChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = (NhDaGoiThauChiPhiModel)sender;

            if (e.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriUsd)
                || e.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriVnd)
                || e.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriEur)
                || e.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriNgoaiTeKhac))
            {
                CalculateChiPhi();
                CaculateNguonVon();
                CaculatorTotalChiPhi();
            }
        }

        private void UpdateTreeItemsChiPhi()
        {
            if (!ItemsChiPhi.IsEmpty())
            {
                ItemsChiPhi.ForAll(s => s.CanEditValue = !ItemsChiPhi.Any(y => y.IIdParentId == s.Id));
                ItemsChiPhi.ForAll(x =>
                {
                    // Là hàng cha nếu thỏa mãn một trong các điều kiện sau
                    // 1. Có parent id là null hoặc ko nhận phần tử nào là cha
                    // 2. Có phần tử con. CanEditValue = false
                    // 3. Có phần tử cùng cấp là hàng cha
                    if (x.IIdParentId.IsNullOrEmpty() || !ItemsChiPhi.Any(y => y.Id == x.IIdParentId)) x.IsHangCha = true;
                    if (!x.CanEditValue) x.IsHangCha = true;
                    else if (ItemsChiPhi.Any(y => y.IIdParentId == x.IIdParentId && !y.CanEditValue)) x.IsHangCha = true;
                });
            }
        }

        private void DeleteTreeItemsChiPhi(NhDaGoiThauChiPhiModel currentItem, bool status)
        {
            if (currentItem != null)
            {
                var items = ItemsChiPhi;
                currentItem.IsDeleted = status;
                var childs = items.Where(x => x.IIdParentId == currentItem.Id);
                if (!childs.IsEmpty())
                {
                    foreach (var item in childs)
                    {
                        DeleteTreeItemsChiPhi(item, status);
                    }
                }
            }
        }

        private void OrderItemsChiPhi(Guid? parentId = null)
        {
            var childs = ItemsChiPhi.Where(x => x.IIdParentId == parentId);
            if (!childs.IsEmpty())
            {
                var parent = ItemsChiPhi.FirstOrDefault(x => x.Id == parentId);
                int index = 1;
                foreach (var child in childs)
                {
                    if (parent != null)
                    {
                        child.SMaOrder = string.Format("{0}-{1}", parent.SMaOrder, index.ToString("D2"));
                    }
                    else
                    {
                        child.SMaOrder = index.ToString("D2");
                    }
                    child.SMaChiPhi = StringUtils.ConvertMaOrder(child.SMaOrder);
                    OrderItemsChiPhi(child.Id);
                    index++;
                }
            }
        }

        private int CountTreeChildItems(NhDaGoiThauChiPhiModel currentItem)
        {
            int count = 0;
            var childs = ItemsChiPhi.Where(x => x.IIdParentId == currentItem.Id);
            if (!childs.IsEmpty())
            {
                count += childs.Count();
                foreach (var item in childs)
                {
                    count += CountTreeChildItems(item);
                }
            }
            return count;
        }

        public override void Dispose()
        {

        }

        public override void OnClosing()
        {            
            ObservableCollection<NhDaGoiThauChiPhiModel> deleteChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>();
            ObservableCollection<NhDaGoiThauNguonVonModel> deleteNguonVon = new ObservableCollection<NhDaGoiThauNguonVonModel>();
            if (!_itemsChiPhiTemp.IsEmpty())
            {
                _itemsChiPhiTemp.Clear();
                OnPropertyChanged(nameof(ItemsChiPhiTemp));
            }

            if (!_itemsChiPhi.IsEmpty())
            {
                _itemsChiPhi.ForAll(s =>
                {
                    if (s.IsSaved == false)
                        deleteChiPhi.Add(s);
                });
                if (deleteChiPhi.Count > 0)
                    deleteChiPhi.ForAll(s =>
                    _itemsChiPhi.Remove(s));
            }
            
            if (!_itemsGoiThauNguonVon.IsEmpty())
            {
                _itemsGoiThauNguonVon.ForAll(s =>
                {
                    s.GoiThauChiPhis.Clear();
                });
                _itemsGoiThauNguonVon.ForAll(s =>
                {
                    foreach (var item in _itemsChiPhi)
                    {
                        if(s.Id == item.IIdGoiThauNguonVonId)
                            s.GoiThauChiPhis.Add(item);
                    }
                });
                _itemsGoiThauNguonVon.ForAll(s =>
                {
                    if (s.IsSaved == false)
                        deleteNguonVon.Add(s);
                });
                if (deleteNguonVon.Count > 0)
                    deleteNguonVon.ForAll(s =>
                    _itemsGoiThauNguonVon.Remove(s));
                OnPropertyChanged(nameof(ItemsGoiThauNguonVon));
            }
        }

        #region Event
        private void OnShowHangMucDetail()
        {
            MSNTNHPhuongAnNhapKhauItemDialogViewModel.Model = SelectedChiPhi;
            MSNTNHPhuongAnNhapKhauItemDialogViewModel.IsAdded = IsAdded;
            MSNTNHPhuongAnNhapKhauItemDialogViewModel.IsDetail = BIsReadOnly;
            MSNTNHPhuongAnNhapKhauItemDialogViewModel.IsEnableSoCuChuongTrinh = IsEnableSoCuChuongTrinh;
            MSNTNHPhuongAnNhapKhauItemDialogViewModel.CurrencyExchangeAction = (obj) => GoiThauHangMucCurrencyExchange(obj);
            MSNTNHPhuongAnNhapKhauItemDialogViewModel.Init();
            MSNTNHPhuongAnNhapKhauItemDialogViewModel.SavedAction = obj =>
            {
                var data = (obj as IEnumerable<NhDaGoiThauHangMucModel>).Where(s => !s.IsDeleted);

                // Tính tổng tiền hạng mục
                if (!data.IsEmpty())
                {
                    SelectedChiPhi.GoiThauHangMucs = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(data);

                    // gán giá trị cho dữ liệu tạm
                    var rawGoiThauNguonVons = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == SelectedGoiThauNguonVon.Id);
                    var rawGoiThauChiPhi = rawGoiThauNguonVons?.GoiThauChiPhis?.FirstOrDefault(m => m.Id == SelectedChiPhi.Id);
                    var screenGoiThauChiPhi = ItemsChiPhi?.FirstOrDefault(m => m.Id == SelectedChiPhi.Id);

                    if (rawGoiThauChiPhi != null)
                    {
                        rawGoiThauChiPhi.GoiThauHangMucs = SelectedChiPhi.GoiThauHangMucs;
                        screenGoiThauChiPhi.GoiThauHangMucs = SelectedChiPhi.GoiThauHangMucs;
                    }

                    var goiThauChiPhis = data.Where(s => s.IIdParentId == null && !s.IsDeleted);
                    SelectedChiPhi.FGiaTriUsd = goiThauChiPhis.Sum(s => s.FGiaTriUsd);
                    SelectedChiPhi.FGiaTriEur = goiThauChiPhis.Sum(s => s.FGiaTriEur);
                    SelectedChiPhi.FGiaTriVnd = goiThauChiPhis.Sum(s => s.FGiaTriVnd);
                    SelectedChiPhi.FGiaTriNgoaiTeKhac = goiThauChiPhis.Sum(s => s.FGiaTriNgoaiTeKhac);
                    SelectedChiPhi.IsModified = true;
                    SelectedGoiThauNguonVon.IsModified = true;
                    CalculateChiPhi();
                }
            };
            SuggestionsHangMuc();
        }

        /// <summary>
        /// Gợi ý nhập hạng mục, được lấy theo sở cứ trục tiếp tương ứng
        /// </summary>
        private void SuggestionsHangMuc()
        {
            MSNTNHPhuongAnNhapKhauItemDialogViewModel.ShowDialogHost("NHPhuongAnNhapKhauItemDialog");
            #region Hoàng tâm BA - Hiển thị kế thừa đầy đủ thông tin từ TTDA
            switch (SLoaiSoCu)
            {
                case SO_CU_TRUC_TIEP.THONG_TIN_DU_AN:
                case SO_CU_TRUC_TIEP.QUYET_DINH_DAU_TU:
                case SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU:
                case SO_CU_TRUC_TIEP.CHUONG_TRINH:
                    break;
                default:
                    break;
            }
            #endregion
        }

        private void GoiThauHangMucCurrencyExchange(object obj)
        {
            OnCellEditEnding(obj);
        }

        private void OnSaveData(object obj)
        {
            if (SelectedChiPhi?.IsModified == true)
            SelectedGoiThauNguonVon.IsModified = true;
            Model.GoiThauNguonVons = ItemsGoiThauNguonVon;
            foreach (var item in Model.GoiThauNguonVons)
            {
                if (!ValidateViewModelHelper.Validate(item)) return;
                item.GoiThauChiPhis = _mapper.Map<ObservableCollection<NhDaGoiThauChiPhiModel>>(ItemsChiPhi.Where(n => !n.IsDeleted && n.IIdGoiThauNguonVonId == item.Id));
            }
            SavedAction?.Invoke(Model);
            //MessageBoxHelper.Info(Resources.MsgSaveDone);
            Window window = obj as Window;
            window.Close();
            //this.OnClose();
        }

        public override void OnSave()
        {

        }

        #endregion

        private void GoiThauNguonVon_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SelectedGoiThauNguonVon = (NhDaGoiThauNguonVonModel)sender;

            if (args.PropertyName == nameof(NhDaGoiThauNguonVonModel.FGiaTriUsd) ||
                args.PropertyName == nameof(NhDaGoiThauNguonVonModel.FGiaTriVnd) ||
                args.PropertyName == nameof(NhDaGoiThauNguonVonModel.FGiaTriEur) ||
                args.PropertyName == nameof(NhDaGoiThauNguonVonModel.FGiaTriNgoaiTeKhac))
            {
                CaculateNguonVon();
            }

            if (args.PropertyName == nameof(NhDaGoiThauNguonVonModel.IsChecked))
            {
                if (SelectedGoiThauNguonVon.IsChecked) // khi tích chọn nguồn vốn
                {
                    IsSelectedNguonVon = true;
                    var dataMap = _itemsChiPhi.Where(s => s.IIdGoiThauNguonVonId == SelectedGoiThauNguonVon.Id).ToList();
                    var unsaveItemChiPhis = _itemsChiPhi?.Except(dataMap);

                    // nếu dữ liệu tạm chưa có và dữ liệu không thuộc SelectedGoiThauNguonVon hiện tại thì lưu lại
                    if(unsaveItemChiPhis?.Any() == true)
                    {
                        foreach (var item in unsaveItemChiPhis)
                        {
                            item.IsChecked = true;
                            var unsaveGoiThauNguonVon = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == item.IIdGoiThauNguonVonId);
                            if (unsaveGoiThauNguonVon?.GoiThauChiPhis != null
                                && !unsaveGoiThauNguonVon.GoiThauChiPhis.Any(m => m.Id == item.Id))
                                unsaveGoiThauNguonVon.GoiThauChiPhis.Add(item);
                        }
                    }

                    var rawGoiThauChiPhis = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == SelectedGoiThauNguonVon.Id)?.GoiThauChiPhis;
                    if (IsAdded && rawGoiThauChiPhis?.Any() == true)
                    {
                        foreach (var item in rawGoiThauChiPhis)
                        {
                            item.IsChecked = true;
                            if (ItemsChiPhi?.Any(m => m.Id == item.Id) == true) continue;
                            ItemsChiPhi.Add(item);
                        }
                    }
                    else
                    {
                        SelectedGoiThauNguonVon.GoiThauChiPhis = _mapper.Map<ObservableCollection<NhDaGoiThauChiPhiModel>>(dataMap);
                    }

                    LoadGoiThauChiPhi(SelectedGoiThauNguonVon);
                }
                else // khi bỏ chọn nguồn vốn
                {
                    var rawGoiThauNguonVon = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == SelectedGoiThauNguonVon.Id) ?? SelectedGoiThauNguonVon;
                    if (IsAdded && Model?.GoiThauNguonVons?.Any(m => m.Id == SelectedGoiThauNguonVon.Id) != true)
                    {
                        Model.GoiThauNguonVons.Add(SelectedGoiThauNguonVon);
                    }

                    // không xoá trong bảng tạm đi khi bỏ chọn
                    var selectedItemChiPhis = _itemsChiPhi?.Where(s => s.IIdGoiThauNguonVonId == SelectedGoiThauNguonVon.Id)?.ToList();
                    if(selectedItemChiPhis?.Any() == true)
                    {
                        foreach(var item in selectedItemChiPhis)
                        {
                            item.IsChecked = false;
                        }    
                        if (IsAdded && _itemsChiPhi?.Any() == true
                            && rawGoiThauNguonVon != null)
                        {
                            selectedItemChiPhis.ForEach(m => 
                            {
                                if(!rawGoiThauNguonVon.GoiThauChiPhis.Any(n => n.Id == m.Id))
                                {
                                    rawGoiThauNguonVon.GoiThauChiPhis.Add(m);
                                }
                            });
                        }
                    }

                    // Xoá những chi phí có nguồn vốn có IsChecked = false
                    IsSelectedNguonVon = false;
                    List<string> LstTenChiPhiItemTemp = _itemsChiPhiTemp.Select(n => n.STenChiPhi).ToList();
                    foreach (var item in _itemsChiPhi.Clone())
                    {
                        if(!LstTenChiPhiItemTemp.Contains(item.STenChiPhi))
                            _itemsChiPhiTemp.Add(item);
                    }
                    
                    OnPropertyChanged(nameof(ItemsChiPhiTemp));
                    List<NhDaGoiThauChiPhiModel> lstRemove = selectedItemChiPhis.Where(m => !m.IsChecked && m.IIdGoiThauNguonVonId == SelectedGoiThauNguonVon.Id).ToList();
                    lstRemove.ForEach(s => _itemsChiPhi.Remove(s));
                }

                CaculateNguonVon();
            }

            if (args.PropertyName.Equals(nameof(NhDaGoiThauNguonVonModel.IIdNguonVonId)))
            {
                SetEnableItemsNguonVon();
                SelectedGoiThauNguonVon.IsModified = true;
            }
        }

        public void GoiThauChiPhi_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedChiPhi = (NhDaGoiThauChiPhiModel)e.Row.Item;
            if (e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauChiPhiModel.FGiaTriUsd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauChiPhiModel.FGiaTriVnd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauChiPhiModel.FGiaTriEur)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauChiPhiModel.FGiaTriNgoaiTeKhac)))
            {
                e.Cancel = !SelectedChiPhi.CanEditValue;
            }
        }

        private void ChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var item = (NhDaGoiThauChiPhiModel)sender;

            if (args.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriUsd)
                || args.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriVnd)
                || args.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriEur)
                || args.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriNgoaiTeKhac))
            {
                CalculateChiPhi();
                CaculatorTotalChiPhi();
            }
        }

        private void CaculatorTotalNguonVon()
        {
            if (ItemsGoiThauNguonVon.IsEmpty()) return;
            _tongTienGoiThauNguonVon.FGiaTriUsd = ItemsGoiThauNguonVon.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriUsd);
            _tongTienGoiThauNguonVon.FGiaTriEur = ItemsGoiThauNguonVon.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriEur);
            _tongTienGoiThauNguonVon.FGiaTriVnd = ItemsGoiThauNguonVon.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriVnd);
            _tongTienGoiThauNguonVon.FGiaTriNgoaiTeKhac = ItemsGoiThauNguonVon.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriNgoaiTeKhac);
            OnPropertyChanged(nameof(TongTienGoiThauNguonVon));
        }

        private void CaculatorTotalChiPhi()
        {
            //if (ItemsChiPhi.IsEmpty()) return;
            //var sumChiPhis = ItemsChiPhi.Where(n => !n.IsDeleted && n.IIdParentId == null);
            //if (sumChiPhis.Any())
            if(_itemsChiPhi.IsEmpty())
            {
                _tongTienChiPhi = new NhDaGoiThauChiPhiModel();
            }    
            else
            {
                var sumChiPhis = ItemsChiPhi.Where(n => !n.IsDeleted && n.IIdParentId == null);
                _tongTienChiPhi.FGiaTriUsd = sumChiPhis.Sum(n => n.FGiaTriUsd);
                _tongTienChiPhi.FGiaTriEur = sumChiPhis.Sum(n => n.FGiaTriEur);
                _tongTienChiPhi.FGiaTriVnd = sumChiPhis.Sum(n => n.FGiaTriVnd);
                _tongTienChiPhi.FGiaTriNgoaiTeKhac = sumChiPhis.Sum(n => n.FGiaTriNgoaiTeKhac);
                //_tongTienChiPhi.FGiaTriUsd = ItemsChiPhi.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriUsd);
                //_tongTienChiPhi.FGiaTriEur = ItemsChiPhi.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriEur);
                //_tongTienChiPhi.FGiaTriVnd = ItemsChiPhi.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriVnd);
                //_tongTienChiPhi.FGiaTriNgoaiTeKhac = ItemsChiPhi.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriNgoaiTeKhac);
                OnPropertyChanged(nameof(TongTienChiPhi));
            }
        }

        private void SetEnableItemsNguonVon()
        {
            if (!_itemsNguonVon.IsEmpty())
            {
                _itemsNguonVon.ForAll(x =>
                {
                    x.IsEnabled = _itemsGoiThauNguonVon.IsEmpty() || !_itemsGoiThauNguonVon.Any(y => y.IIdNguonVonId != null && y.IIdNguonVonId.Equals(x.IIdMaNguonNganSach));
                });
            }
        }

        private void CalculateChiPhi()
        {
            var parents = ItemsChiPhi.Where(x => x.IIdParentId.IsNullOrEmpty() || !ItemsChiPhi.Any(y => y.Id == x.IIdParentId));
            foreach (var item in parents)
            {
                item.IsModified = true;
                CalculateChiPhi(item);
            }
        }

        private void CalculateChiPhi(NhDaGoiThauChiPhiModel parentItem)
        {
            var childs = ItemsChiPhi.Where(x => x.IIdParentId == parentItem.Id && !x.IsDeleted);
            if (!childs.IsEmpty())
            {
                foreach (var item in childs)
                {
                    CalculateChiPhi(item);
                }
                parentItem.IsModified = true;
                parentItem.FGiaTriUsd = childs.Sum(x => x.FGiaTriUsd);
                parentItem.FGiaTriEur = childs.Sum(x => x.FGiaTriEur);
                parentItem.FGiaTriVnd = childs.Sum(x => x.FGiaTriVnd);
                parentItem.FGiaTriNgoaiTeKhac = childs.Sum(x => x.FGiaTriNgoaiTeKhac);
            }
        }

        private void CaculateNguonVon()
        {
            if (!_itemsGoiThauNguonVon.IsEmpty())
            {
                foreach (var item in _itemsGoiThauNguonVon.Where(s => s.IsChecked))
                {
                    var dataSums = _itemsChiPhi.Where(s => s.IIdGoiThauNguonVonId == item.Id && !s.IIdParentId.HasValue);
                    if (dataSums.Any())
                    {
                        item.FGiaTriUsd = dataSums.Sum(s => s.FGiaTriUsd);
                        item.FGiaTriVnd = dataSums.Sum(s => s.FGiaTriVnd);
                        item.FGiaTriEur = dataSums.Sum(s => s.FGiaTriEur);
                        item.FGiaTriNgoaiTeKhac = dataSums.Sum(s => s.FGiaTriNgoaiTeKhac);
                        item.IsModified = true;
                    }
                }

                // Tính Giá trị hợp đồng = Tổng giá trị hợp đồng nguồn vốn
                OnPropertyChanged(nameof(ItemsGoiThauNguonVon));
                CaculatorTotalNguonVon();
            }
        }
    }
}
