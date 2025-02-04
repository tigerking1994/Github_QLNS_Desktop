using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PlanSuggestions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;
using System.Text;
using VTS.QLNS.CTC.App.Helper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanSuggestions
{
    public class PlanSuggestionsDetailViewModel : DetailViewModelBase<VdtKhvKeHoach5NamDeXuatModel, VdtKhvKeHoach5NamDeXuatChiTietModel>
    {
        private static Dictionary<int, int> _indexMax = new Dictionary<int, int>();
        private static Dictionary<int, int> _indexDetailMax = new Dictionary<int, int>();

        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvKeHoach5NamDeXuatService _vdtKhvKeHoach5NamDeXuatService;
        private readonly IVdtKhvKeHoach5NamDeXuatChiTietService _vdtKhvKeHoach5NamChiTietDexuatService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtDmLoaiCongTrinhService _vdtDmLoaiCongTrinhService;
        private readonly ILog _logger;
        private readonly IDmLoaiCongTrinhService _dmLoaiCongTrinhService;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private ICollectionView _vdtKhvKhthView;
        private readonly IServiceProvider _provider;
        private List<VdtKhvKeHoach5NamDeXuatChiTietModel> _vdtKhvFilter = new List<VdtKhvKeHoach5NamDeXuatChiTietModel>();
        private int _index = 0;
        private const int MAX_LEVEL = 20;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateSettlementVoucherEvent;

        public override string Title => "Kế hoạch trung hạn đề xuất";
        public override string Name => (Model != null && Model.IsViewDetail) ? "XEM KẾ HOẠCH TRUNG HẠN ĐỀ XUẤT CHI TIẾT" : "KẾ HOẠCH TRUNG HẠN ĐỀ XUẤT CHI TIẾT";
        public override string Description => string.Format("Số quyết định: {0} - Ngày quyết định: {1} - Giai đoạn: {2} - Đơn vị: {3}",
                                                Model.SSoQuyetDinh, Model.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy"),
                                                string.Format("{0}-{1}", Model.IGiaiDoanTu, Model.IGiaiDoanDen),
                                                _nsDonViService.FindByIdDonVi(Model.IIdMaDonVi, _sessionService.Current.YearOfWork).TenDonVi);
        public override Type ContentType => typeof(PlanSuggestionsDetail);

        public string SHeaderLuyKeNSQPHetNam => string.Format("Lũy kế vốn NSQP đã bố trí hết năm {0}", Model.IGiaiDoanTu - 2);
        public string SHeaderVonNSQPNam => string.Format("Vốn NSQP đề nghị bố trí năm {0}", Model.IGiaiDoanTu - 1);
        public string SHeaderHanMucDauTu => (Model.ILoai == (int)LoaiDuAnEnum.Type.CHUYEN_TIEP) ? "Tổng mức đầu tư được duyệt" : "Hạn mức đầu tư";
        public bool IsDieuChinh => Model != null && Model.IIdParentId != null;
        public bool IsActive =>  Model != null && Model.BActive && !Model.IsViewDetail && !Model.BKhoa ;
        public bool IsActive1 => string.IsNullOrEmpty(Model.STongHop) && IsActive;
        public bool IsDuAnChuyenTiep => Model != null && (int)LoaiDuAnEnum.Type.CHUYEN_TIEP == Model.ILoai;

        private VdtKhvKeHoach5NamDeXuatChiTietModel _summaryItem = new VdtKhvKeHoach5NamDeXuatChiTietModel();
        public VdtKhvKeHoach5NamDeXuatChiTietModel SummaryItem
        {
            get => _summaryItem;
            set => SetProperty(ref _summaryItem, value);
        }

        private string _txtTenDuAn;
        public string TxtTenDuAn
        {
            get => _txtTenDuAn;
            set => SetProperty(ref _txtTenDuAn, value);
        }

        private string _txtDiaDiemThucHien;
        public string TxtDiaDiemThucHien
        {
            get => _txtDiaDiemThucHien;
            set => SetProperty(ref _txtDiaDiemThucHien, value);
        }

        private string _txtThoiGianTu;
        public string TxtThoiGianTu
        {
            get => _txtThoiGianTu;
            set => SetProperty(ref _txtThoiGianTu, value);
        }

        private string _txtThoiGianDen;
        public string TxtThoiGianDen
        {
            get => _txtThoiGianDen;
            set => SetProperty(ref _txtThoiGianDen, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> ItemsLoaiCongTrinh
        {
            get => _itemsLoaiCongTrinh;
            set => SetProperty(ref _itemsLoaiCongTrinh, value);
        }

        private ComboboxItem _selectedLoaiCongTrinh;
        public ComboboxItem SelectedLoaiCongTrinh
        {
            get => _selectedLoaiCongTrinh;
            set
            {
                SetProperty(ref _selectedLoaiCongTrinh, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                OnSearch();
            }
        }

        private ComboboxItem _selectedNguonVon;
        public ComboboxItem SelectedNguonVon
        {
            get => _selectedNguonVon;
            set
            {
                SetProperty(ref _selectedNguonVon, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsNguonVon;
        public ObservableCollection<ComboboxItem> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        public Visibility VisibilityChuyenTiep
        {
            get => (Model != null && (int)LoaiDuAnEnum.Type.CHUYEN_TIEP == Model.ILoai) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility VisibilityKhoiCongMoi
        {
            get => (Model != null && (int)LoaiDuAnEnum.Type.KHOI_CONG_MOI == Model.ILoai) ? Visibility.Visible : Visibility.Collapsed;
        }

        public RelayCommand OpenReferencePopupCommand { get; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }

        public PlanSuggestionsDetailViewModel(IMapper mapper,
            ISessionService sessionService,
            IVdtKhvKeHoach5NamDeXuatService vdtKhvKeHoach5NamDeXuatService,
            IVdtKhvKeHoach5NamDeXuatChiTietService vdtKhvKeHoach5NamChiTietDexuat,
            INsMucLucNganSachService nsMucLucNganSachService,
            INsNguonNganSachService nsNguonNganSachService,
            INsDonViService nsDonViService,
            IVdtDmLoaiCongTrinhService vdtDmLoaiCongTrinhService,
            ILog logger,
            IDmLoaiCongTrinhService dmLoaiCongTrinhService,
            IVdtDmDonViThucHienDuAnService dmDonViThucHienDuAnService,
            IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _dmLoaiCongTrinhService = dmLoaiCongTrinhService;
            _provider = serviceProvider;
            _vdtDmLoaiCongTrinhService = vdtDmLoaiCongTrinhService;
            _vdtKhvKeHoach5NamDeXuatService = vdtKhvKeHoach5NamDeXuatService;
            _vdtKhvKeHoach5NamChiTietDexuatService = vdtKhvKeHoach5NamChiTietDexuat;
            _mucLucNganSachService = nsMucLucNganSachService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nsDonViService = nsDonViService;
            _vdtDmLoaiCongTrinhService = vdtDmLoaiCongTrinhService;
            _vdtDmDonViThucHienDuAnService = dmDonViThucHienDuAnService;

            OpenReferencePopupCommand = new RelayCommand(obj => OnOpenReferencePopup(obj));
            AddCommand = new RelayCommand(obj => OnAdd(obj, isDuAn: false));
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
        }

        public override void Init()
        {
            try
            {
                LoadIndex();
                LoadLoaiCongTrinh();
                LoadDonViQuanLy();
                LoadNguonNganSach();
                LoadHeader();
                LoadData();
                OnResetFilter();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadIndex()
        {
            try
            {
                if (_indexMax == null || (_indexMax != null && _indexMax.Count == 0))
                {
                    for (int item = 2; item <= MAX_LEVEL; item++)
                    {
                        _indexMax.Add(item, 0);
                    }
                }

                if (_indexDetailMax == null || (_indexDetailMax != null && _indexDetailMax.Count == 0))
                {
                    for (int item = 2; item <= MAX_LEVEL; item++)
                    {
                        _indexDetailMax.Add(item, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadLoaiCongTrinh()
        {
            List<VdtDmLoaiCongTrinh> listLoaiCongTrinh = _vdtDmLoaiCongTrinhService.FindAll().ToList();
            _itemsLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(listLoaiCongTrinh);
        }

        private void LoadDonViQuanLy()
        {
            var cbxLoaiDonViData = _vdtDmDonViThucHienDuAnService.FindAll().ToList();
            _itemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxLoaiDonViData);
        }

        private void LoadNguonNganSach()
        {
            var lstData = _nsNguonNganSachService.FindNguonNganSach();
            _itemsNguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(lstData);
        }

        private void LoadHeader()
        {
            if (Model != null && Model.IGiaiDoanTu != 0)
            {
                Model.HeaderGroupVonBoTri = string.Format("NHU CẦU BỐ TRÍ VỐN NSQP {0} - {1}", Model.IGiaiDoanTu, Model.IGiaiDoanDen);

                Model.Header1 = string.Format("Năm {0}", Model.IGiaiDoanTu.ToString());
                Model.Header2 = string.Format("Năm {0}", (Model.IGiaiDoanTu + 1).ToString());
                Model.Header3 = string.Format("Năm {0}", (Model.IGiaiDoanTu + 2).ToString());
                Model.Header4 = string.Format("Năm {0}", (Model.IGiaiDoanTu + 3).ToString());
                Model.Header5 = string.Format("Năm {0}", (Model.IGiaiDoanTu + 4).ToString());
                Model.HeaderAfterYear = string.Format("Vốn bố trí sau năm {0}", (Model.IGiaiDoanTu + 4).ToString());

                string tilesModified = Model.IIdParentId != null ? " (Sau điều chỉnh)" : "";
                Model.HeaderModified1 = string.Format("{0}{1}", Model.Header1, tilesModified);
                Model.HeaderModified2 = string.Format("{0}{1}", Model.Header2, tilesModified);
                Model.HeaderModified3 = string.Format("{0}{1}", Model.Header3, tilesModified);
                Model.HeaderModified4 = string.Format("{0}{1}", Model.Header4, tilesModified);
                Model.HeaderModified5 = string.Format("{0}{1}", Model.Header5, tilesModified);
                Model.HeaderAfterYearModified = string.Format("{0}{1}", Model.HeaderAfterYear, tilesModified);
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    if (IsDuAnChuyenTiep)
                    {
                        if (Model != null && !string.IsNullOrEmpty(Model.STongHop))
                        {
                            List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstQuery = _vdtKhvKeHoach5NamChiTietDexuatService.FindConditionIndex(Model.Id.ToString()).ToList();
                            Items = _mapper.Map<ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel>>(lstQuery);
                            foreach (var item in Items)
                            {
                                foreach (var item2 in Items)
                                {
                                    if (item.Id == item2.IdParent)
                                    {
                                        item.IsHangCha = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstDuAn = new List<VdtKhvKeHoach5NamDeXuatChiTietQuery>();
                            List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstQuery = _vdtKhvKeHoach5NamChiTietDexuatService.FindChiTietDuAnChuyenTiep(Model.Id).ToList();
                            List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstDuAnExisted = _vdtKhvKeHoach5NamChiTietDexuatService.FindConditionIndex(Model.Id.ToString()).ToList();
                            if (lstDuAnExisted != null && lstDuAnExisted.Count() > 0 && Model != null && Model.LstIdDuAn != null && Model.LstIdDuAn.Count() > 0)
                            {
                                lstDuAnExisted = lstDuAnExisted.Where(x => Model.LstIdDuAn.Contains(x.IIdDuAnId.Value)).ToList();
                                //lstDuAn.AddRange(lstQuery);
                            }
                            lstDuAn.AddRange(lstDuAnExisted);

                            if (Model != null && Model.LstIdDuAn != null && Model.LstIdDuAn.Count() > 0 && Model.BIsGoc.GetValueOrDefault())
                            {
                                lstQuery = lstQuery.Where(x => Model.LstIdDuAn.Contains(x.IIdDuAnId.Value)).ToList();
                                lstDuAnExisted = lstQuery.Where(x => !lstDuAn.Any(y => x.IIdDuAnId == y.IIdDuAnId)).ToList();
                                lstDuAn.AddRange(lstQuery);
                            }
                            else
                            {
                                lstQuery = new List<VdtKhvKeHoach5NamDeXuatChiTietQuery>();
                            }
                            lstDuAn = lstDuAn.GroupBy(x => new { x.IIdDuAnId, x.IIdLoaiCongTrinhId, x.IIdNguonVonId }).Select(grp => grp.FirstOrDefault()).ToList();
                            lstDuAn.Select(x => { x.STT = (lstDuAn.IndexOf(x) + 1).ToString(); return x; }).ToList();
                            Items = _mapper.Map<ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel>>(lstDuAn);
                            foreach (var item in Items)
                            {
                                foreach (var item2 in Items)
                                {
                                    if (item.Id == item2.IdParent)
                                    {
                                        item.IsHangCha = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstQuery = _vdtKhvKeHoach5NamChiTietDexuatService.FindConditionIndex(Model.Id.ToString()).ToList();
                        Items = _mapper.Map<ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel>>(lstQuery);
                        foreach (var item in Items)
                        {
                            foreach (var item2 in Items)
                            {
                                if (item.Id == item2.IdParent)
                                {
                                    item.IsHangCha = true;
                                }
                            }
                        }
                    }

                    Items.Select(x => { x.BActive = Model.BActive && !Model.IsViewDetail && !Model.BKhoa; return x; }).ToList();

                    var max_level = Items.Select(x => x.Level).Max();
                    VdtKhvKeHoach5NamDeXuatChiTietModel itemMaxLevel = Items.Where(x => x.Level.Equals(max_level)).FirstOrDefault();
                    if (itemMaxLevel != null)
                    {
                        CalculateData(itemMaxLevel);
                    }
                    CalculatTotal();
                }, (s, e) =>
                {
                    IsLoading = false;
                    foreach (var item in Items)
                    {
                        item.PropertyChanged += DetailModel_PropertyChanged;
                    }
                    _vdtKhvKhthView = CollectionViewSource.GetDefaultView(Items);
                    _vdtKhvKhthView.Filter = KeHoachTrungHanFilter;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                if (args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.STen)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.STenNguonVon)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.FGiaTriNamThuNhat)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.FGiaTriNamThuHai)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.FGiaTriNamThuBa)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.FGiaTriNamThuTu)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.FGiaTriNamThuNam)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.FHanMucDauTu)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.IGiaiDoanTu)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.IGiaiDoanDen)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.STenNguonVon)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.STenLoaiCongTrinh)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.SDiaDiem)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.SGhiChu)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.STenDonVi)
                   || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.IIdNguonVonId))
                {
                    VdtKhvKeHoach5NamDeXuatChiTietModel item = (VdtKhvKeHoach5NamDeXuatChiTietModel)sender;
                    item.IsModified = true;

                    item.FGiaTriKeHoach = (item.FGiaTriNamThuNhat ?? 0)
                            + (item.FGiaTriNamThuHai ?? 0)
                            + (item.FGiaTriNamThuBa ?? 0)
                            + (item.FGiaTriNamThuTu ?? 0)
                            + (item.FGiaTriNamThuNam ?? 0);

                    if (!item.IsParent)
                    {
                        item.FTongSoNhuCauNSQP = (item.IIdNguonVonId != null && item.IIdNguonVonId.Value.Equals((int)MediumTermType.Nsqp)) ? (item.FGiaTriKeHoach + (item.FGiaTriBoTri ?? 0)) : 0;
                    }

                    List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstChildrent = GetChildren(Items.ToList(), item.Id);

                    if ((args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.FHanMucDauTu)
                        || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.FGiaTriNamThuNhat)
                        || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.FGiaTriNamThuHai)
                        || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.FGiaTriNamThuBa)
                        || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.FGiaTriNamThuTu)
                        || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.FGiaTriNamThuNam)
                        || args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.IIdNguonVonId)
                        ) && (!item.IsParent && !item.IsDeleted || (lstChildrent != null && lstChildrent.Count == 0 && item.IsParent && item.IdParent == null)))
                    {
                        CalculateData(item);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool KeHoachTrungHanFilter(object obj)
        {
            try
            {
                if (!(obj is VdtKhvKeHoach5NamDeXuatChiTietModel temp)) return true;
                var bCondition = true;

                if (_vdtKhvFilter != null && _vdtKhvFilter.Count > 0)
                {
                    bCondition &= _vdtKhvFilter.Where(x => x.Id.Equals(temp.Id)).Count() > 0;
                }
                else
                {
                    if (!string.IsNullOrEmpty(TxtTenDuAn))
                    {
                        bCondition &= !string.IsNullOrEmpty(temp.STen) && temp.STen.ToLower().Contains(TxtTenDuAn.ToLower());
                    }

                    if (SelectedLoaiCongTrinh != null)
                    {
                        bCondition &= temp.IIdLoaiCongTrinhId.Equals(SelectedLoaiCongTrinh.ValueItem);
                    }

                    if (SelectedNguonVon != null)
                    {
                        bCondition &= temp.IIdNguonVonId.Equals(SelectedNguonVon.ValueItem);
                    }
                }

                if (!string.IsNullOrEmpty(TxtDiaDiemThucHien))
                {
                    bCondition &= !string.IsNullOrEmpty(temp.SDiaDiem) && temp.SDiaDiem.ToLower().Contains(TxtDiaDiemThucHien.ToLower());
                }

                if (!string.IsNullOrEmpty(TxtThoiGianTu))
                {
                    bCondition &= temp.IGiaiDoanTu.ToString().ToLower().Equals(TxtThoiGianTu.ToLower());
                }

                if (!string.IsNullOrEmpty(TxtThoiGianDen))
                {
                    bCondition &= temp.IGiaiDoanDen.ToString().ToLower().Equals(TxtThoiGianDen.ToLower());
                }

                if (SelectedDonVi != null)
                {
                    bCondition &= !string.IsNullOrEmpty(temp.IIdMaDonVi) && temp.IIdMaDonVi.Equals(SelectedDonVi.ValueItem.ToString());
                }

                _index++;

                if (Items.Count.Equals(_index))
                {
                    _vdtKhvFilter = new List<VdtKhvKeHoach5NamDeXuatChiTietModel>();
                    _index = 0;
                }

                temp.IsFilter = bCondition;
                return bCondition;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return true;
            }
        }

        private List<VdtKhvKeHoach5NamDeXuatChiTietModel> HandleSearchParentChildrent(List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstCurrentSelected)
        {
            try
            {
                List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstFilter = new List<VdtKhvKeHoach5NamDeXuatChiTietModel>();
                lstFilter.AddRange(lstCurrentSelected);

                foreach (var item in lstCurrentSelected)
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstParent = GetParent(Items.ToList(), item).ToList();
                    lstFilter.AddRange(lstParent);
                }

                lstFilter = lstFilter.GroupBy(x => x.Id).Select(grp => grp.FirstOrDefault()).ToList();

                return lstFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return Items.ToList();
            }
        }

        private void OnSearch()
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtTenDuAn))
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstCurrentSelected = Items.Where(x => x.STen.ToLower().Contains(TxtTenDuAn.ToLower())).ToList();
                    _vdtKhvFilter.AddRange(HandleSearchParentChildrent(lstCurrentSelected));
                }

                if (SelectedLoaiCongTrinh != null)
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstCurrentSelected = Items.Where(x => x.IIdLoaiCongTrinhId.Equals(Guid.Parse(SelectedLoaiCongTrinh.ValueItem))).ToList();
                    _vdtKhvFilter.AddRange(HandleSearchParentChildrent(lstCurrentSelected));
                }

                if (SelectedNguonVon != null)
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstCurrentSelected = Items.Where(x => x.IIdNguonVonId.Equals(Int32.Parse(SelectedNguonVon.ValueItem))).ToList();
                    _vdtKhvFilter.AddRange(HandleSearchParentChildrent(lstCurrentSelected));
                }

                if (_vdtKhvKhthView != null) _vdtKhvKhthView.Refresh();
                CalculateData(Items.Where(x => x.Level.Equals(1)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnResetFilter()
        {
            try
            {
                TxtTenDuAn = string.Empty;
                TxtThoiGianTu = string.Empty;
                TxtThoiGianDen = string.Empty;
                TxtDiaDiemThucHien = string.Empty;
                SelectedLoaiCongTrinh = null;
                SelectedDonVi = null;
                SelectedNguonVon = null;

                this.LoadData();

                OnPropertyChanged(nameof(SelectedNguonVon));
                OnPropertyChanged(nameof(SelectedDonVi));
                OnPropertyChanged(nameof(SelectedLoaiCongTrinh));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenReferencePopup(object obj)
        {
            try
            {
                DataGrid dataGrid = obj as DataGrid;

                if (dataGrid.CurrentCell.Column != null && dataGrid.CurrentCell.Column.SortMemberPath.Equals("STenLoaiCongTrinh"))
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstChildren = new List<VdtKhvKeHoach5NamDeXuatChiTietModel>();

                    if (SelectedItem != null)
                    {
                        lstChildren = GetChildren(Items.Where(x => !x.IsDeleted).ToList(), SelectedItem.Id);
                    }

                    if (SelectedItem != null && (SelectedItem.IsStatus.Equals(MediumTermPlanType.GROUP)
                        || lstChildren.Count() > 0 || Model.BKhoa || !Model.BActive || !string.IsNullOrEmpty(Model.STongHop)))
                    {
                        return;
                    }

                    GenericControlCustomViewModel<VdtDmLoaiCongTrinhModel, Core.Domain.VdtDmLoaiCongTrinh, DmLoaiCongTrinhService> viewModelBase = new GenericControlCustomViewModel<VdtDmLoaiCongTrinhModel, Core.Domain.VdtDmLoaiCongTrinh, DmLoaiCongTrinhService>((DmLoaiCongTrinhService)_dmLoaiCongTrinhService, _mapper, _sessionService, _provider)
                    {
                        Name = "Danh mục loại công trình",
                        Title = "Danh mục loại công trình",
                        Description = "Danh mục loại công trình",
                        IconKind = MaterialDesignThemes.Wpf.PackIconKind.Building,
                        IsDialog = true
                    };
                    GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(viewModelBase);
                    GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                    {
                        DataContext = genericControlCustomWindowViewModel,
                        Title = "Danh mục loại công trình",
                    };

                    GenericControlCustomWindow.SavedAction = obj =>
                    {
                        try
                        {
                            VdtDmLoaiCongTrinhModel item2 = (VdtDmLoaiCongTrinhModel)obj;

                            if (item2 != null)
                            {
                                foreach (var item in Items)
                                {
                                    if ((item.Id == null) || item.Id == Guid.Empty)
                                    {
                                        if (item.IdDiscern.Equals(SelectedItem.IdDiscern))
                                        {
                                            item.STenLoaiCongTrinh = item2.STenLoaiCongTrinh;
                                            item.IIdLoaiCongTrinhId = item2.IIdLoaiCongTrinh;
                                        }
                                    }
                                    else if (item.Id != null && item.Id != Guid.Empty)
                                    {
                                        if (item.Id.Equals(SelectedItem.Id))
                                        {
                                            item.STenLoaiCongTrinh = item2.STenLoaiCongTrinh;
                                            item.IIdLoaiCongTrinhId = item2.IIdLoaiCongTrinh;
                                        }
                                    }
                                }
                            }

                            GenericControlCustomWindow.Close();

                            OnPropertyChanged(nameof(Items));

                            if (SelectedItem != null)
                            {
                                if (ValidateDetailProject())
                                {
                                    MessageBox.Show(Resources.MsgDaNVHangMucExisted);
                                    SelectedItem.IIdLoaiCongTrinhId = null;
                                    SelectedItem.STenLoaiCongTrinh = string.Empty;
                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex.Message, ex);
                        }
                    };
                    viewModelBase.GenericControlCustomWindow = GenericControlCustomWindow;
                    GenericControlCustomWindow.Show();
                }
                else if (dataGrid.CurrentCell.Column!= null && dataGrid.CurrentCell.Column.SortMemberPath.Equals("STenNguonVon"))
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstChildren = new List<VdtKhvKeHoach5NamDeXuatChiTietModel>();

                    if (SelectedItem != null)
                    {
                        lstChildren = GetChildren(Items.Where(x => !x.IsDeleted).ToList(), SelectedItem.Id);
                    }

                    if (SelectedItem != null && (SelectedItem.IsStatus.Equals(MediumTermPlanType.GROUP) || lstChildren.Count() > 0
                        || Model.BKhoa || !Model.BActive || !string.IsNullOrEmpty(Model.STongHop)))
                    {
                        return;
                    }

                    GenericControlCustomViewModel<NguonNganSachModel, Core.Domain.NsNguonNganSach, NsNguonNganSachService> viewModelBase = new GenericControlCustomViewModel<NguonNganSachModel, Core.Domain.NsNguonNganSach, NsNguonNganSachService>((NsNguonNganSachService)_nsNguonNganSachService, _mapper, _sessionService, _provider)
                    {
                        Name = "Danh mục nguồn ngân sách",
                        Title = "Danh mục nguồn ngân sách",
                        Description = "Danh sách danh mục nguồn ngân sách",
                        IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                        IsDialog = true,
                    };
                    GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(viewModelBase);
                    GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                    {
                        DataContext = genericControlCustomWindowViewModel,
                        Title = "Danh mục nguồn ngân sách",
                    };

                    GenericControlCustomWindow.SavedAction = obj =>
                    {
                        try
                        {
                            NguonNganSachModel item2 = (NguonNganSachModel)obj;

                            if (item2 != null)
                            {
                                foreach (var item in Items)
                                {
                                    if ((item.Id == null) || item.Id == Guid.Empty)
                                    {
                                        if (item.IdDiscern.Equals(SelectedItem.IdDiscern))
                                        {
                                            item.STenNguonVon = item2.STen;
                                            item.IIdNguonVonId = item2.IIdMaNguonNganSach;
                                        }
                                    }
                                    else if (item.Id != null && item.Id != Guid.Empty)
                                    {
                                        if (item.Id.Equals(SelectedItem.Id))
                                        {
                                            item.STenNguonVon = item2.STen;
                                            item.IIdNguonVonId = item2.IIdMaNguonNganSach;
                                        }
                                    }
                                }
                            }

                            GenericControlCustomWindow.Close();

                            OnPropertyChanged(nameof(Items));

                            if (SelectedItem != null)
                            {
                                if (ValidateDetailProject())
                                {
                                    MessageBox.Show(Resources.MsgDaNVHangMucExisted);
                                    SelectedItem.IIdNguonVonId = null;
                                    SelectedItem.STenNguonVon = string.Empty;
                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex.Message, ex);
                        }
                    };

                    viewModelBase.GenericControlCustomWindow = GenericControlCustomWindow;
                    GenericControlCustomWindow.Show();
                }
                else if (dataGrid.CurrentCell.Column != null && dataGrid.CurrentCell.Column.SortMemberPath.Equals("STenDonVi"))
                {
                    if (SelectedItem != null && (SelectedItem.IsStatus.Equals(MediumTermPlanType.GROUP) || Model.BKhoa || !Model.BActive || !string.IsNullOrEmpty(Model.STongHop)))
                    {
                        return;
                    }

                    GenericControlCustomViewModel<VdtDmDonViThucHienDuAnModel, Core.Domain.VdtDmDonViThucHienDuAn, VdtDmDonViThucHienDuAnService> viewModelBase = new GenericControlCustomViewModel<VdtDmDonViThucHienDuAnModel, Core.Domain.VdtDmDonViThucHienDuAn, VdtDmDonViThucHienDuAnService>((VdtDmDonViThucHienDuAnService)_vdtDmDonViThucHienDuAnService, _mapper, _sessionService, _provider)
                    {
                        Name = "Danh mục đơn vị thực hiện dự án",
                        Title = "Danh mục đơn vị thực hiện dự án",
                        Description = "Danh sách danh mục vốn đầu tư",
                        IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                        IsDialog = true,
                    };
                    GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(viewModelBase);
                    GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                    {
                        DataContext = genericControlCustomWindowViewModel,
                        Title = "Danh mục đơn vị thực hiện dự án",
                    };

                    GenericControlCustomWindow.SavedAction = obj =>
                    {
                        try
                        {
                            VdtDmDonViThucHienDuAnModel item2 = (VdtDmDonViThucHienDuAnModel)obj;

                            if (item2 != null)
                            {
                                foreach (var item in Items)
                                {
                                    if ((item.Id == null) || item.Id == Guid.Empty)
                                    {
                                        if (item.IdDiscern.Equals(SelectedItem.IdDiscern))
                                        {
                                            item.STenDonVi = item2.STenDonVi;
                                            item.IIdDonViId = item2.IIdDonVi;
                                            item.IIdMaDonVi = item2.IIdMaDonVi;
                                        }
                                    }
                                    else if (item.Id != null && item.Id != Guid.Empty)
                                    {
                                        if (item.Id.Equals(SelectedItem.Id))
                                        {
                                            item.STenDonVi = item2.STenDonVi;
                                            item.IIdDonViId = item2.IIdDonVi;
                                            item.IIdMaDonVi = item2.IIdMaDonVi;
                                        }
                                    }
                                }
                            }

                            GenericControlCustomWindow.Close();

                            OnPropertyChanged(nameof(Items));
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex.Message, ex);
                        }
                    };

                    viewModelBase.GenericControlCustomWindow = GenericControlCustomWindow;
                    GenericControlCustomWindow.Show();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnDelete()
        {
            try
            {
                if (!IsActive) return;

                if (SelectedItem != null && SelectedItem.IIdKeHoach5NamId.HasValue)
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstDataExisted = _vdtKhvKeHoach5NamChiTietDexuatService.FindListVoucherDetailsModified(SelectedItem.IIdKeHoach5NamId.Value).ToList();

                    if (lstDataExisted.Count() > 0)
                    {
                        MessageBox.Show(Resources.VoucherKhthDelete, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }

                bool isDeleted = false;

                if (IsDuAnChuyenTiep && SelectedItem != null)
                {
                    SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                    CalculatTotal();
                    OnPropertyChanged(nameof(Items));
                    return;
                }

                if (SelectedItem != null && SelectedItem.IsParent)
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstChildren = GetChildren(Items.ToList(), SelectedItem.Id);
                    if (lstChildren != null && lstChildren.Count > 0)
                    {
                        Items.Where(x => lstChildren.Select(y => y.Id).ToList().Contains(x.Id)).Select(x => { x.IsDeleted = !SelectedItem.IsDeleted; return x; }).ToList();
                    }
                }

                if (Items != null && Items.Count > 0 && SelectedItem != null)
                {
                    isDeleted = SelectedItem.IsDeleted;
                    SelectedItem.IsDeleted = !SelectedItem.IsDeleted;

                    VdtKhvKeHoach5NamDeXuatChiTietModel itemDelete = SelectedItem.Id != Guid.Empty ? Items.Where(x => x.Id == SelectedItem.Id).FirstOrDefault()
                                                                            : Items.Where(x => x.IdDiscern == SelectedItem.IdDiscern).FirstOrDefault();

                    itemDelete.IsDeleted = SelectedItem.IsDeleted;

                    List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstItemGroup = Items.Where(x => !x.IsDeleted && x.IdParent == itemDelete.IdParent).ToList();

                    VdtKhvKeHoach5NamDeXuatChiTietModel itemParent = Items.Where(x => x.Id == itemDelete.IdParent).FirstOrDefault();

                    if (SelectedItem.IsDeleted && itemParent != null)
                    {
                        if (lstItemGroup.Count() == 0 && !SelectedItem.IsParent)
                        {
                            itemParent.IsParent = false;
                        }
                    }
                    else if (itemParent != null)
                    {
                        itemParent.IsParent = !SelectedItem.IsDeleted;
                    }
                }

                if (isDeleted && ValidateDetailProject())
                {
                    if (SelectedItem != null)
                    {
                        MessageBox.Show(Resources.MsgDaNVHangMucExisted);
                        SelectedItem.IIdLoaiCongTrinhId = null;
                        SelectedItem.STenLoaiCongTrinh = string.Empty;
                        SelectedItem.IIdNguonVonId = 0;
                        SelectedItem.STenNguonVon = string.Empty;
                        return;
                    }
                }

                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private int GetMaxIndex(int Level, string SMaOrderParent, Guid? id, Dictionary<int, int> dicData)
        {
            try
            {
                List<string> lstViaLevel = _vdtKhvKeHoach5NamChiTietDexuatService.FindByLevel(Level, Model.Id, id).Select(x => x.SMaOrder).ToList();
                List<int> result = new List<int>();
                int indexMax = 1;

                if (lstViaLevel != null && lstViaLevel.Count > 0)
                {
                    foreach (var item in lstViaLevel)
                    {
                        if (item.StartsWith(SMaOrderParent))
                        {
                            int index_item = Int32.Parse(item.Split("_")[Level]);
                            result.Add(index_item);
                        }
                    }

                    if (result != null && result.Count > 0)
                    {
                        indexMax = (from m in result select m).Max();
                    }

                    for (int item = 2; item <= MAX_LEVEL; item++)
                    {
                        if (Level == item)
                        {
                            if (dicData[Level] >= indexMax)
                            {
                                indexMax = dicData[Level] + 1;
                                dicData[Level] = indexMax;
                            }
                            else
                            {
                                indexMax += 1;
                                dicData[Level] = indexMax;
                            }
                        }
                    }
                }
                else
                {
                    if (dicData[Level] >= indexMax)
                    {
                        indexMax = dicData[Level] + 1;
                        dicData[Level] = indexMax;
                    }
                    else
                    {
                        dicData[Level] = indexMax;
                    }
                }

                return indexMax;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return 1;
            }
        }

        protected void OnAdd(object param, bool isDuAn)
        {
            try
            {
                if (!IsActive) return;

                var recordType = (MediumTermModifyType)(int)param;
                if (recordType.Equals(MediumTermModifyType.NEW) && Model.BActive)
                {
                    int currentRow = Items.IndexOf(SelectedItem);
                    VdtKhvKeHoach5NamDeXuatChiTietModel newItem = new VdtKhvKeHoach5NamDeXuatChiTietModel();
                    newItem.STen = string.Empty;
                    newItem.IIdLoaiCongTrinhId = null;
                    newItem.IIdNguonVonId = null;
                    newItem.STenLoaiCongTrinh = string.Empty;
                    newItem.STenNguonVon = string.Empty;
                    newItem.SDiaDiem = string.Empty;
                    newItem.IIdDonViId = null;
                    newItem.IIdMaDonVi = string.Empty;
                    newItem.STenDonVi = string.Empty;
                    newItem.IGiaiDoanTu = null;
                    newItem.IGiaiDoanDen = null;
                    newItem.FHanMucDauTu = 0;
                    newItem.FGiaTriNamThuNhat = 0;
                    newItem.FGiaTriNamThuHai = 0;
                    newItem.FGiaTriNamThuBa = 0;
                    newItem.FGiaTriNamThuTu = 0;
                    newItem.FGiaTriNamThuNam = 0;
                    newItem.FGiaTriBoTriOrigin = 0;
                    newItem.SGhiChu = string.Empty;
                    newItem.IsNew = true;
                    newItem.IIdKeHoach5NamId = Model.Id;
                    newItem.IdReference = null;
                    newItem.Level = 1;
                    newItem.IdParent = null;
                    newItem.IsParent = false;
                    newItem.IdDiscern = Guid.NewGuid();
                    newItem.IsClone = false;
                    newItem.IdHangMuc = Guid.NewGuid();
                    newItem.IdParentHangMuc = null;

                    int indexParent = 1;

                    if (!isDuAn)
                    {
                        newItem.IsStatus = MediumTermPlanType.GROUP;
                        newItem.IsHangCha = true;
                        var itemGroup = Items.Where(x => x.IsStatus.Equals(MediumTermPlanType.GROUP) && !x.IsDeleted);

                        if (itemGroup != null && itemGroup.Count() > 0)
                        {
                            indexParent = itemGroup.Select(x => Int32.Parse(x.STT)).Max() + 1;
                        }

                        newItem.SMaOrder = string.Format("{0}_{1}", MediumTermPlanType.GROUP.ToString("D3"), indexParent.ToString("D3"));
                        newItem.STT = indexParent.ToString();
                    }
                    else
                    {
                        newItem.IsStatus = MediumTermPlanType.PROJECT;
                        newItem.IsHangCha = false;
                        var itemProject = Items.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT) && !x.IsDeleted);
                        var itemGroup = Items.Where(x => x.IsStatus.Equals(MediumTermPlanType.GROUP) && !x.IsDeleted);

                        itemProject = itemProject.Where(x => !x.IdParent.HasValue || (x.IdParent.HasValue && !itemGroup.Select(y => y.Id).ToList().Contains(x.IdParent.Value))).ToList();

                        if (itemProject != null && itemProject.Count() > 0)
                        {
                            indexParent = itemProject.Select(x => Int32.Parse(x.STT)).Max() + 1;
                        }

                        newItem.SMaOrder = string.Format("{0}_{1}", MediumTermPlanType.PROJECT.ToString("D3"), indexParent.ToString("D3"));
                        newItem.STT = indexParent.ToString();
                    }

                    newItem.PropertyChanged += DetailModel_PropertyChanged;
                    Items.Insert(currentRow + 1, newItem);
                    Items = new ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel>(Items.OrderBy(x => x.SMaOrder).ToList());
                }
                else if (recordType.Equals(MediumTermModifyType.CHILD) && Model.BActive)
                {
                    if (SelectedItem == null || !(SelectedItem.IsStatus.Equals(MediumTermPlanType.GROUP)))
                    {
                        OnAdd(MediumTermModifyType.NEW, isDuAn: true);
                        return;
                    }

                    if (SelectedItem != null && (SelectedItem.Id == null || SelectedItem.Id == Guid.Empty))
                    {
                        MessageBox.Show(Resources.MsgWarnParentNotExisted);
                        return;
                    }

                    if (SelectedItem.IsStatus.Equals(MediumTermPlanType.GROUP))
                    {
                        var messageResult = MessageBox.Show(String.Format(Resources.MsgWarnGroup, SelectedItem.STen), "Cảnh báo", MessageBoxButton.YesNo);

                        if (messageResult == MessageBoxResult.Yes)
                        {
                            int currentRow = Items.IndexOf(SelectedItem);
                            VdtKhvKeHoach5NamDeXuatChiTietModel newItem = new VdtKhvKeHoach5NamDeXuatChiTietModel();

                            if (SelectedItem != null)
                            {
                                newItem.STen = string.Empty;
                                newItem.IIdLoaiCongTrinhId = SelectedItem.IIdLoaiCongTrinhId;
                                newItem.STenLoaiCongTrinh = SelectedItem.STenLoaiCongTrinh;
                                newItem.IIdNguonVonId = SelectedItem.IIdNguonVonId;
                                newItem.STenNguonVon = SelectedItem.STenNguonVon;
                                newItem.IIdDonViId = SelectedItem.IIdDonViId;
                                newItem.STenDonVi = SelectedItem.STenDonVi;
                                newItem.IIdMaDonVi = SelectedItem.IIdMaDonVi;
                                newItem.IGiaiDoanTu = Model.IGiaiDoanTu;
                                newItem.IGiaiDoanDen = Model.IGiaiDoanDen;
                                newItem.SDiaDiem = string.Empty;
                                newItem.FHanMucDauTu = 0;
                                newItem.FGiaTriNamThuNhat = 0;
                                newItem.FGiaTriNamThuHai = 0;
                                newItem.FGiaTriNamThuBa = 0;
                                newItem.FGiaTriNamThuTu = 0;
                                newItem.FGiaTriNamThuNam = 0;
                                newItem.FGiaTriBoTriOrigin = 0;
                                newItem.IsNew = true;
                                newItem.SGhiChu = string.Empty;
                                newItem.IsHangCha = false;
                                newItem.IdParent = SelectedItem.Id;
                                newItem.IIdKeHoach5NamId = SelectedItem.IIdKeHoach5NamId;
                                newItem.IdReference = null;
                                newItem.IdDiscern = Guid.NewGuid();
                                newItem.IdCloneReference = Guid.NewGuid();
                                newItem.IsClone = false;
                                newItem.IdHangMuc = Guid.NewGuid();
                                newItem.IdParentHangMuc = null;
                                newItem.IsStatus = MediumTermPlanType.PROJECT;

                                int Level = SelectedItem.Level.Value;

                                var lstGroup = GetParent(Items.ToList(), SelectedItem).ToList();

                                if (SelectedItem.IsStatus.Equals(MediumTermPlanType.GROUP) || (lstGroup.Count == 0 && SelectedItem.IsStatus.Equals(MediumTermPlanType.PROJECT)))
                                {
                                    Level = SelectedItem.Level.Value + 1;
                                    string SMaOrderParent = SelectedItem.SMaOrder;
                                    int indexMax = GetMaxIndex(Level, SMaOrderParent, SelectedItem.Id, _indexMax);

                                    newItem.SMaOrder = string.Format("{0}_{1}", SMaOrderParent, indexMax.ToString("D3"));
                                    newItem.STT = string.Format("{0}.{1}", SelectedItem.STT, indexMax);
                                }
                                else
                                {
                                    string SubMaOrder = Items.Where(x => x.Id == SelectedItem.IdParent).Select(x => x.SMaOrder).FirstOrDefault();
                                    string SubStt = Items.Where(x => x.Id == SelectedItem.IdParent).Select(x => x.STT).FirstOrDefault();

                                    int indexMax = GetMaxIndex(Level, SubMaOrder, SelectedItem.Id, _indexMax);
                                    string SMaOrder = string.Format("{0}_{1}", SubMaOrder, indexMax.ToString("D3"));
                                    string Stt = string.Format("{0}.{1}", SubStt, indexMax);

                                    newItem.SMaOrder = SMaOrder;
                                    newItem.STT = Stt;
                                }

                                newItem.Level = Level;

                                var lstParent = GetParent(Items.ToList(), newItem).ToList();

                                foreach (var item in Items.ToList())
                                {
                                    if (lstParent.Where(x => x.Id == item.Id).Count() > 0)
                                    {
                                        item.IsParent = true;
                                    }
                                }

                                newItem.IsParent = false;

                                newItem.PropertyChanged += DetailModel_PropertyChanged;
                                Items.Insert(currentRow + 1, newItem);
                            }
                        }
                        else
                        {
                            OnAdd(MediumTermModifyType.NEW, isDuAn: true);
                        }
                    }
                    else
                    {
                        OnAdd(MediumTermModifyType.NEW, isDuAn: true);
                    }

                    Items = new ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel>(Items.OrderBy(x => x.SMaOrder).ToList());
                }
                else if (recordType.Equals(MediumTermModifyType.CLONE) && Model.BActive)
                {
                    if (SelectedItem == null)
                    {
                        MessageBox.Show(Resources.MsgErrorVocherDetails);
                        return;
                    }

                    if (SelectedItem.IsStatus.Equals(MediumTermPlanType.GROUP))
                    {
                        MessageBox.Show(Resources.MsgErrorGroupDetail);
                        return;
                    }

                    if (SelectedItem != null && (SelectedItem.Id == null || SelectedItem.Id == Guid.Empty))
                    {
                        MessageBox.Show(Resources.MsgWarnParentNotExisted);
                        return;
                    }

                    int currentRow = Items.IndexOf(SelectedItem);
                    VdtKhvKeHoach5NamDeXuatChiTietModel newItem = new VdtKhvKeHoach5NamDeXuatChiTietModel();
                    if (SelectedItem != null)
                    {
                        newItem.STen = SelectedItem.STen;
                        newItem.IIdLoaiCongTrinhId = null;
                        newItem.STenLoaiCongTrinh = string.Empty;
                        newItem.IIdNguonVonId = null;
                        newItem.STenNguonVon = string.Empty;
                        newItem.IIdDonViId = SelectedItem.IIdDonViId;
                        newItem.STenDonVi = SelectedItem.STenDonVi;
                        newItem.IIdMaDonVi = SelectedItem.IIdMaDonVi;
                        newItem.IGiaiDoanTu = SelectedItem.IGiaiDoanTu;
                        newItem.IGiaiDoanDen = SelectedItem.IGiaiDoanDen;
                        newItem.SDiaDiem = SelectedItem.SDiaDiem;
                        newItem.FHanMucDauTu = 0;
                        newItem.FGiaTriNamThuNhat = 0;
                        newItem.FGiaTriNamThuHai = 0;
                        newItem.FGiaTriNamThuBa = 0;
                        newItem.FGiaTriNamThuTu = 0;
                        newItem.FGiaTriNamThuNam = 0;
                        newItem.FGiaTriBoTriOrigin = 0;
                        newItem.SGhiChu = SelectedItem.SGhiChu;
                        newItem.IIdKeHoach5NamId = Model.Id;
                        newItem.IsHangCha = false;
                        newItem.Level = SelectedItem.Level;
                        newItem.IsNew = true;
                        newItem.IdDiscern = Guid.NewGuid();
                        newItem.STT = SelectedItem.STT;
                        newItem.IndexCode = SelectedItem.IndexCode;
                        newItem.IsClone = true;
                        newItem.IdCloneReference = SelectedItem.IdCloneReference;
                        newItem.IdParent = SelectedItem.Id;

                        SelectedItem.FTongSoNhuCauNSQP = 0;
                        SelectedItem.FGiaTriKeHoach = 0;
                        SelectedItem.FGiaTriNamThuNhat = 0;
                        SelectedItem.FGiaTriNamThuHai = 0;
                        SelectedItem.FGiaTriNamThuBa = 0;
                        SelectedItem.FGiaTriNamThuTu = 0;
                        SelectedItem.FGiaTriNamThuNam = 0;
                        SelectedItem.FGiaTriBoTri = 0;
                        SelectedItem.FHanMucDauTu = 0;

                        SelectedItem.FTongSoNhuCauNSQPOrigin = 0;
                        SelectedItem.FGiaTriKeHoachOrigin = 0;
                        SelectedItem.FGiaTriNamThuNhatOrigin = 0;
                        SelectedItem.FGiaTriNamThuHaiOrigin = 0;
                        SelectedItem.FGiaTriNamThuBaOrigin = 0;
                        SelectedItem.FGiaTriNamThuTuOrigin = 0;
                        SelectedItem.FGiaTriNamThuNamOrigin = 0;
                        SelectedItem.FGiaTriBoTriOrigin = 0;

                        if (!SelectedItem.IsStatus.Equals(MediumTermPlanType.DETAIL_PROJECT))
                        {
                            newItem.IdParentHangMuc = null;
                            newItem.IdReference = SelectedItem.Id;
                        }
                        else
                        {
                            newItem.IdParentHangMuc = SelectedItem.IdHangMuc;
                            newItem.IdReference = SelectedItem.IdReference;
                        }

                        newItem.IdHangMuc = Guid.NewGuid();
                        newItem.IsStatus = MediumTermPlanType.DETAIL_PROJECT;

                        int Level = SelectedItem.Level.Value + 1;
                        string SMaOrderParent = SelectedItem.SMaOrder;
                        int indexParent = SelectedItem.IndexCode.HasValue ? SelectedItem.IndexCode.Value : 0;

                        int indexMax = GetMaxIndex(Level, SMaOrderParent, SelectedItem.Id, _indexDetailMax);

                        newItem.Level = Level;
                        newItem.SMaOrder = string.Format("{0}_{1}", SMaOrderParent, indexMax.ToString("D3"));
                        newItem.STT = string.Format("{0}.{1}", SelectedItem.STT, indexMax);

                        var lstParent = GetParent(Items.ToList(), newItem);

                        foreach (var item in Items.ToList())
                        {
                            if (lstParent.Where(x => x.Id == item.Id).Count() > 0)
                            {
                                item.IsParent = true;
                            }
                        }

                        VdtKhvKeHoach5NamDeXuatChiTietModel itemProject = Items.Where(x => x.Id == SelectedItem.Id).FirstOrDefault();

                        if (itemProject != null && itemProject.IsStatus.Equals(MediumTermPlanType.PROJECT))
                        {
                            itemProject.IIdLoaiCongTrinhId = null;
                            itemProject.STenLoaiCongTrinh = string.Empty;
                            itemProject.IIdNguonVonId = null;
                            itemProject.STenNguonVon = string.Empty;
                        }

                        newItem.IsParent = false;
                        newItem.PropertyChanged += DetailModel_PropertyChanged;

                        Items.Insert(currentRow + 1, newItem);
                    }

                    Items = new ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel>(Items.OrderBy(x => x.SMaOrder).ToList());
                }
                foreach (var item in Items)
                {
                    foreach (var item2 in Items)
                    {
                        if (item.Id == item2.IdParent)
                        {
                            item.IsHangCha = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private static List<VdtKhvKeHoach5NamDeXuatChiTietModel> GetChildren(List<VdtKhvKeHoach5NamDeXuatChiTietModel> foos, Guid id)
        {
            return foos
                .Where(x => x.IdParent == id)
                .Union(foos.Where(x => x.IdParent == id)
                    .SelectMany(y => GetChildren(foos, y.Id))
                ).ToList();
        }

        private IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietModel> GetParent(List<VdtKhvKeHoach5NamDeXuatChiTietModel> foos, VdtKhvKeHoach5NamDeXuatChiTietModel childrent)
        {
            var parent = foos.FirstOrDefault(x => x.Id == childrent.IdParent);
            if (parent == null)
                return Enumerable.Empty<VdtKhvKeHoach5NamDeXuatChiTietModel>();

            return new[] { parent }.Concat(GetParent(foos, parent));
        }

        private void CalculateData(VdtKhvKeHoach5NamDeXuatChiTietModel itemSelected = null)
        {
            try
            {
                if (itemSelected != null)
                {
                    if (itemSelected.Level > 1)
                    {
                        List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstParent = GetParent(Items.ToList(), itemSelected).ToList();
                        List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstChildren = Items.Where(x => x.Level <= itemSelected.Level && !x.Level.Equals(1)).ToList();

                        lstChildren = lstChildren.Where(x => x.IdParent != null && lstParent.Select(y => y.Id).Contains(x.IdParent.Value)).ToList();
                        lstChildren.Where(x => !x.IIdNguonVonId.Equals((int)MediumTermType.Nsqp)).Select(x => { x.FTongSoNhuCauNSQP = 0; x.FTongSoNhuCauNSQPOrigin = 0; return x; }).ToList();

                        lstParent.Select(x =>
                        {
                            x.FHanMucDauTu = 0;
                            x.FTongSoNhuCauNSQP = 0;
                            x.FGiaTriNamThuNhat = 0;
                            x.FGiaTriNamThuHai = 0;
                            x.FGiaTriNamThuBa = 0;
                            x.FGiaTriNamThuTu = 0;
                            x.FGiaTriNamThuNam = 0;
                            x.FGiaTriBoTri = 0;

                            x.FGiaTriNamThuNhatOrigin = 0;
                            x.FGiaTriNamThuHaiOrigin = 0;
                            x.FGiaTriNamThuBaOrigin = 0;
                            x.FGiaTriNamThuTuOrigin = 0;
                            x.FGiaTriNamThuNamOrigin = 0;
                            x.FGiaTriBoTriOrigin = 0;
                            x.FTongSoNhuCauNSQPOrigin = 0;

                            return x;
                        }).ToList();

                        if (lstChildren != null && lstChildren.Count > 0)
                        {
                            foreach (var item in lstChildren)
                            {
                                CalculateParent(item, item);
                            }
                        }
                    }
                }

                CalculatTotal();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateParent(VdtKhvKeHoach5NamDeXuatChiTietModel currentItem, VdtKhvKeHoach5NamDeXuatChiTietModel seftItem)
        {
            try
            {
                var parrentItem = Items.Where(x => x.Id == currentItem.IdParent && x.IsFilter).FirstOrDefault();
                if (parrentItem == null) return;
                parrentItem.FTongSoNhuCauNSQP += seftItem.FTongSoNhuCauNSQP;
                parrentItem.FHanMucDauTu += seftItem.FHanMucDauTu;
                parrentItem.FGiaTriNamThuNhat += seftItem.FGiaTriNamThuNhat;
                parrentItem.FGiaTriNamThuHai += seftItem.FGiaTriNamThuHai;
                parrentItem.FGiaTriNamThuBa += seftItem.FGiaTriNamThuBa;
                parrentItem.FGiaTriNamThuTu += seftItem.FGiaTriNamThuTu;
                parrentItem.FGiaTriNamThuNam += seftItem.FGiaTriNamThuNam;
                parrentItem.FGiaTriBoTri += seftItem.FGiaTriBoTri;
                parrentItem.FGiaTriNamThuNhatOrigin += seftItem.FGiaTriNamThuNhatOrigin;
                parrentItem.FGiaTriNamThuHaiOrigin += seftItem.FGiaTriNamThuHaiOrigin;
                parrentItem.FGiaTriNamThuBaOrigin += seftItem.FGiaTriNamThuBaOrigin;
                parrentItem.FGiaTriNamThuTuOrigin += seftItem.FGiaTriNamThuTuOrigin;
                parrentItem.FGiaTriNamThuNamOrigin += seftItem.FGiaTriNamThuNamOrigin;
                parrentItem.FGiaTriBoTriOrigin += seftItem.FGiaTriBoTriOrigin;
                CalculateParent(parrentItem, seftItem);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculatTotal()
        {
            try
            {
                if (SummaryItem == null) SummaryItem = new VdtKhvKeHoach5NamDeXuatChiTietModel();

                SummaryItem.FHanMucDauTu = 0;
                SummaryItem.FTongSoNhuCauNSQP = 0;
                SummaryItem.FGiaTriBoTri = 0;
                SummaryItem.FGiaTriKeHoach = 0;
                SummaryItem.FGiaTriNamThuNhat = 0;
                SummaryItem.FGiaTriNamThuHai = 0;
                SummaryItem.FGiaTriNamThuBa = 0;
                SummaryItem.FGiaTriNamThuTu = 0;
                SummaryItem.FGiaTriNamThuNam = 0;
                SummaryItem.FVonNSQPLuyKe = 0;
                SummaryItem.FVonNSQP = 0;

                //SummaryItem.FGiaTriBoTrio = 0;
                SummaryItem.FGiaTriKeHoachOrigin = 0;
                SummaryItem.FGiaTriNamThuNhatOrigin = 0;
                SummaryItem.FGiaTriNamThuHaiOrigin = 0;
                SummaryItem.FGiaTriNamThuBaOrigin = 0;
                SummaryItem.FGiaTriNamThuTuOrigin = 0;
                SummaryItem.FGiaTriNamThuNamOrigin = 0;
                SummaryItem.FGiaTriBoTriOrigin = 0;

                List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstItemTotal = new List<VdtKhvKeHoach5NamDeXuatChiTietModel>();

                if (IsDuAnChuyenTiep)
                {
                    lstItemTotal = Items.Where(x => x.IsFilter && !x.IsDeleted).ToList();
                }
                else
                {
                    lstItemTotal = Items.Where(x => x.IsFilter && !x.IsDeleted && x.IsStatus.Equals(MediumTermPlanType.PROJECT)).ToList();
                }

                foreach (var item in lstItemTotal)
                {
                    SummaryItem.FHanMucDauTu += item.FHanMucDauTu;
                    SummaryItem.FTongSoNhuCauNSQP += item.FTongSoNhuCauNSQP;
                    SummaryItem.FGiaTriBoTri += (item.FGiaTriBoTri ?? 0);
                    SummaryItem.FGiaTriKeHoach += item.FGiaTriKeHoach;
                    SummaryItem.FGiaTriNamThuNhat += (item.FGiaTriNamThuNhat ?? 0);
                    SummaryItem.FGiaTriNamThuHai += (item.FGiaTriNamThuHai ?? 0);
                    SummaryItem.FGiaTriNamThuBa += (item.FGiaTriNamThuBa ?? 0);
                    SummaryItem.FGiaTriNamThuTu += (item.FGiaTriNamThuTu ?? 0);
                    SummaryItem.FGiaTriNamThuNam += (item.FGiaTriNamThuNam ?? 0);
                    SummaryItem.FVonNSQPLuyKe += (item.FVonNSQPLuyKe);
                    SummaryItem.FVonNSQP += (item.FVonNSQP);

                    SummaryItem.FGiaTriKeHoachOrigin += item.FGiaTriKeHoachOrigin;
                    SummaryItem.FGiaTriNamThuNhatOrigin += (item.FGiaTriNamThuNhatOrigin ?? 0);
                    SummaryItem.FGiaTriNamThuHaiOrigin += (item.FGiaTriNamThuHaiOrigin ?? 0);
                    SummaryItem.FGiaTriNamThuBaOrigin += (item.FGiaTriNamThuBaOrigin ?? 0);
                    SummaryItem.FGiaTriNamThuTuOrigin += (item.FGiaTriNamThuTuOrigin ?? 0);
                    SummaryItem.FGiaTriNamThuNamOrigin += (item.FGiaTriNamThuNamOrigin ?? 0);
                    SummaryItem.FGiaTriBoTriOrigin += (item.FGiaTriBoTriOrigin ?? 0);
                }

                OnPropertyChanged(nameof(SummaryItem));
                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnRefresh()
        {
            _index = 0;
            _indexMax = new Dictionary<int, int>();
            _indexDetailMax = new Dictionary<int, int>();
            this.LoadIndex();
            this.LoadData();
            OnPropertyChanged(nameof(Items));
        }

        public override void OnSave()
        {
            try
            {
                if (!IsActive) return;

                List<VdtKhvKeHoach5NamDeXuatChiTietModel> listUpdate = Items.Where(x => x.Id != Guid.Empty && x.IsModified).ToList();
                List<VdtKhvKeHoach5NamDeXuatChiTietModel> listAdd = Items.Where(x => (x.Id == null || x.Id == Guid.Empty) && x.IsModified && !x.IsDeleted).ToList();
                List<VdtKhvKeHoach5NamDeXuatChiTietModel> listDelete = Items.Where(x => x.IsDeleted && x.Id != Guid.NewGuid()).ToList();

                StringBuilder messageBuilder = new StringBuilder();
                if (Items.Any(x => x.STen.Length >= 255 && !x.IsDeleted))
                {
                    messageBuilder.AppendFormat(Resources.ValidSTen);
                }

                if (Items.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Any(x => !x.FHanMucDauTu.Equals(x.FTongSoNhuCauNSQP) && x.IIdNguonVonId.Equals((int)MediumTermType.Nsqp) && !x.IsDeleted))
                {
                    messageBuilder.AppendFormat(Resources.VoucherInvestementLimit);
                }

                if (Items.Any(x => x.FGiaTriBoTri < 0 && !x.IsDeleted))
                {
                    messageBuilder.AppendFormat(Resources.VoucherBeforeYearLimit);
                }

                if (messageBuilder.Length != 0)
                {
                    MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                    return;
                }

                if (Items.Any())

                    if (listAdd != null && listAdd.Count > 0)
                    {
                        if (!ValiDateData()) return;
                        listAdd = listAdd.Select(x =>
                        {
                            x.Id = Guid.NewGuid();
                            x.IIdKeHoach5NamId = Model.Id;
                            x.BActive = true;
                            return x;

                        }).ToList();

                        List<VdtKhvKeHoach5NamDeXuatChiTiet> listChungTuChiTiets = _mapper.Map<List<VdtKhvKeHoach5NamDeXuatChiTiet>>(listAdd);
                        _vdtKhvKeHoach5NamChiTietDexuatService.AddRange(listChungTuChiTiets);
                    }

                if (listUpdate != null && listUpdate.Count > 0)
                {
                    if (!ValiDateData()) return;
                    listUpdate = listUpdate.Select(x =>
                    {
                        x.BActive = true;
                        return x;
                    }).ToList();

                    foreach (var item in listUpdate)
                    {
                        VdtKhvKeHoach5NamDeXuatChiTiet chungTuChiTiet = _vdtKhvKeHoach5NamChiTietDexuatService.FindById(item.Id);
                        if (chungTuChiTiet != null)
                        {
                            _mapper.Map(item, chungTuChiTiet);
                            _vdtKhvKeHoach5NamChiTietDexuatService.Update(chungTuChiTiet);
                        }
                    }
                }

                if (listDelete != null && listDelete.Count > 0)
                {
                    foreach (var item in listDelete)
                    {
                        _vdtKhvKeHoach5NamChiTietDexuatService.Delete(item.Id);
                    }
                }

                //cập nhật thông tin chứng từ
                VdtKhvKeHoach5NamDeXuat chungTu = _vdtKhvKeHoach5NamDeXuatService.FindById(Model.Id);
                CalculatTotal();
                chungTu.FGiaTriKeHoach = SummaryItem.FGiaTriKeHoach;
                _vdtKhvKeHoach5NamDeXuatService.Update(chungTu);

                Items.Select(x => { x.IsModified = false; x.IsDeleted = false; return x; }).ToList();

                string message = Resources.MsgSaveDone;
                var messageBox = new NSMessageBoxViewModel(message);
                DialogHost.Show(messageBox.Content, "PlanManagerDetailDexuat");

                _indexMax = new Dictionary<int, int>();
                _indexDetailMax = new Dictionary<int, int>();
                _index = 0;

                // Refresh data
                LoadIndex();
                this.LoadData();

                // Callback
                DataChangedEventHandler handler = UpdateSettlementVoucherEvent;
                if (handler != null) 
                {
                    handler(Model, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnClose(object obj)
        {
            try
            {
                _indexMax = new Dictionary<int, int>();
                _indexDetailMax = new Dictionary<int, int>();
                _vdtKhvFilter = new List<VdtKhvKeHoach5NamDeXuatChiTietModel>();

                var window = obj as Window;
                window.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ValidateDetailProject()
        {
            try
            {
                List<VdtKhvKeHoach5NamDeXuatChiTietModel> listPartern = Items.Where(x => x.IdReference == SelectedItem.IdReference && !x.IsDeleted && !x.IsStatus.Equals(MediumTermPlanType.PROJECT)).ToList();

                if (SelectedItem.Id != Guid.Empty)
                {
                    listPartern = listPartern.Where(x => !(x.Id == SelectedItem.Id)).ToList();
                }
                else if (SelectedItem.IdDiscern.HasValue)
                {
                    listPartern = listPartern.Where(x => !(x.IdDiscern == SelectedItem.IdDiscern)).ToList();
                }

                foreach (var item in listPartern)
                {
                    if (SelectedItem.IIdLoaiCongTrinhId.HasValue && SelectedItem.IIdNguonVonId.HasValue)
                    {
                        if (item.IIdLoaiCongTrinhId == SelectedItem.IIdLoaiCongTrinhId && item.IIdNguonVonId == SelectedItem.IIdNguonVonId)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return false;
            }
        }

        private bool ValiDateData()
        {
            List<string> lstMess = new List<string>();
            if (Items.Any())
            {
                // validate nếu là thêm nhóm dự án => chỉ check sten dự án.
                if (Items.Any(s => s.Level == 1 && s.IsStatus == 1 && string.IsNullOrEmpty(s.STen)))
                    lstMess.Add(Resources.MesInvalidNameProject);
                if (Items.Any(s => s.Level == 1 && s.IsStatus == 2 && string.IsNullOrEmpty(s.STen)))
                    lstMess.Add(Resources.MesInvalidNameProject);
                if (!Items.Any(s => s.Level == 1 && s.IsStatus == 2 && (Items.Where(s => s.Level != 1 && s.IsStatus != 2).Select(s => s.IdParent).Contains(s.Id))))
                {
                    if (Items.Any(s => s.Level == 1 && s.IsStatus == 2 && s.IIdLoaiCongTrinhId is null))
                        lstMess.Add(Resources.MesLoaiCongTrinhChuaNhap);
                    if (Items.Any(s => s.Level == 1 && s.IsStatus == 2 && s.IIdNguonVonId is null))
                        lstMess.Add(Resources.MesNguonVonChuaNhap);
                }

                if (Items.Any(s => s.Level != 1 && s.IsStatus != 1 && s.IsStatus != 2 && s.IIdLoaiCongTrinhId is null))
                    lstMess.Add(Resources.MesLoaiCongTrinhChuaNhap);
                if (Items.Any(s => s.Level != 1 && s.IsStatus != 1 && s.IsStatus != 2 && s.IIdNguonVonId is null))
                    lstMess.Add(Resources.MesNguonVonChuaNhap);
            }

            if (lstMess.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstMess), Resources.NotifiTitle);
                return false;
            }
            return true;
        }
    }
}
