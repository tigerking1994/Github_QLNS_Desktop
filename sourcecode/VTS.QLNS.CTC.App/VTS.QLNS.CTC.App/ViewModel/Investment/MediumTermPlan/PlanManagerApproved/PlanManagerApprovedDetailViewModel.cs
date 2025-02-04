using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PlanManagerApproved;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using log4net;
using System.Windows.Data;
using VTS.QLNS.CTC.Utility;
using System.Windows;
using VTS.QLNS.CTC.App.Service;
using System.Text;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanManagerApproved
{
    public class PlanManagerApprovedDetailViewModel : DetailViewModelBase<VdtKhvKeHoach5NamModel, VdtKhvKeHoach5NamChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IVdtKhvKeHoach5NamService _vdtKhvKeHoach5NamService;
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvKeHoach5NamChiTietService _vdtKhvKeHoach5NamChiTietService;
        private readonly INsDonViService _nsDonViService;
        private ICollectionView _vdtKhvKhthView;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateSettlementVoucherEvent;

        public override string FuncCode => NSFunctionCode.INVESTMENT_MEDIUM_TERM_PLAN_PLAN_MANAGER_APPROVED_DETAIL;
        public override string Title => "Kế hoạch trung hạn được duyệt";
        public override string Name => (Model != null && Model.IsViewDetail) ? "XEM KẾ HOẠCH TRUNG HẠN CHI TIẾT" : "KẾ HOẠCH TRUNG HẠN CHI TIẾT";
        public override string Description => string.Format("Số quyết định: {0} - Ngày quyết định: {1} - Giai đoạn: {2} - Đơn vị: {3}",
                                                Model.SSoQuyetDinh, Model.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy"),
                                                string.Format("{0}-{1}", Model.IGiaiDoanTu, Model.IGiaiDoanDen),
                                                _nsDonViService.FindByIdDonVi(Model.IIdMaDonVi, _sessionService.Current.YearOfWork).TenDonVi);
        public override Type ContentType => typeof(PlanManagerApprovedDetail);
        public bool IsDieuChinh => Model != null && Model.IIdParentId != null;
        public bool IsDuAnChuyenTiep => Model != null && (int)LoaiDuAnEnum.Type.CHUYEN_TIEP == Model.ILoai;
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public bool IsActive => Model != null && !Model.IsViewDetail;
        public string SHeaderVonDaBoTriHetNam => string.Format("Vốn bố trí hết năm {0}", Model.IGiaiDoanTu);
        public string SHeaderVonDaBoTriNam => string.Format("Vốn đã bố trí năm {0}", Model.IGiaiDoanTu);
        public string SHeaderHanMucDauTu => (Model.ILoai == (int)LoaiDuAnEnum.Type.CHUYEN_TIEP) ? "Tổng mức đầu tư được duyệt" : "Hạn mức đầu tư";


        protected override void OnItemsChanged()
        {
            OnPropertyChanged(nameof(IsSaveData));
        }

        private VdtKhvKeHoach5NamChiTietModel _summaryItem = new VdtKhvKeHoach5NamChiTietModel();
        public VdtKhvKeHoach5NamChiTietModel SummaryItem
        {
            get => _summaryItem;
            set => SetProperty(ref _summaryItem, value);
        }

        private ObservableCollection<ComboboxItem> _modifiedType;
        public ObservableCollection<ComboboxItem> ModifiedType
        {
            get => _modifiedType;
            set => SetProperty(ref _modifiedType, value);
        }

        private ObservableCollection<ComboboxItem> _drpLoaiCongTrinhs;
        public ObservableCollection<ComboboxItem> DrpLoaiCongTrinhs
        {
            get => _drpLoaiCongTrinhs;
            set => SetProperty(ref _drpLoaiCongTrinhs, value);
        }

        private ComboboxItem _drpLoaiCongTrinhSelected;
        public ComboboxItem DrpLoaiCongTrinhSelected
        {
            get => _drpLoaiCongTrinhSelected;
            set
            {
                SetProperty(ref _drpLoaiCongTrinhSelected, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _drpTenDuAns;
        public ObservableCollection<ComboboxItem> DrpTenDuAns
        {
            get => _drpTenDuAns;
            set => SetProperty(ref _drpTenDuAns, value);
        }

        private ComboboxItem _drpTenDuAnSelected;
        public ComboboxItem DrpTenDuAnSelected
        {
            get => _drpTenDuAnSelected;
            set
            {
                SetProperty(ref _drpTenDuAnSelected, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _drpDonViQuanLys;
        public ObservableCollection<ComboboxItem> DrpDonViQuanLys
        {
            get => _drpDonViQuanLys;
            set => SetProperty(ref _drpDonViQuanLys, value);
        }

        private ComboboxItem _drpDonViQuanLySelected;
        public ComboboxItem DrpDonViQuanLySelected
        {
            get => _drpDonViQuanLySelected;
            set
            {
                SetProperty(ref _drpDonViQuanLySelected, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _drpNguonVons;
        public ObservableCollection<ComboboxItem> DrpNguonVons
        {
            get => _drpNguonVons;
            set => SetProperty(ref _drpNguonVons, value);
        }

        private ComboboxItem _drpNguonVonSelected;
        public ComboboxItem DrpNguonVonSelected
        {
            get => _drpNguonVonSelected;
            set
            {
                SetProperty(ref _drpNguonVonSelected, value);
                OnSearch();
            }
        }

        private string _sDiaDiemThucHien;
        public string SDiaDiemThucHien
        {
            get => _sDiaDiemThucHien;
            set => SetProperty(ref _sDiaDiemThucHien, value);
        }

        private string _sThoiGianThucHien;
        public string SThoiGianThucHien
        {
            get => _sThoiGianThucHien;
            set => SetProperty(ref _sThoiGianThucHien, value);
        }

        public RelayCommand ChooseProjectCommand { get; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }

        public ProjectInPlanManagerApprovedDiaLogViewModel ProjectInPlanManagerApprovedDiaLogViewModel { get; set; }

        public PlanManagerApprovedDetailViewModel(IMapper mapper,
            ILog logger,
            IVdtKhvKeHoach5NamService vdtKhvKeHoach5NamService,
            IVdtKhvKeHoach5NamChiTietService vdtKhvKeHoach5NamChiTietService,
            INsDonViService nsDonViService,
            ISessionService sessionService,
            ProjectInPlanManagerApprovedDiaLogViewModel projectInPlanManagerApprovedDiaLogViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _sessionService = sessionService;
            _vdtKhvKeHoach5NamChiTietService = vdtKhvKeHoach5NamChiTietService;
            _vdtKhvKeHoach5NamService = vdtKhvKeHoach5NamService;

            ProjectInPlanManagerApprovedDiaLogViewModel = projectInPlanManagerApprovedDiaLogViewModel;
            ProjectInPlanManagerApprovedDiaLogViewModel.ParentPage = this;

            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj =>
            {
                OnResetFilter();
                _vdtKhvKhthView.Refresh();
            });
            ChooseProjectCommand = new RelayCommand(obj => OnAdd());
        }

        public override void Init()
        {
            try
            {
                LoadHeader();
                LoadData();
                OnResetFilter();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadHeader()
        {
            if (Model != null)
            {
                Model.HeaderVonDaGiaoOrigin = string.Format("Vốn đã giao năm {0}", (Model.IGiaiDoanTu - 1).ToString());
                Model.HeaderVonBoTriTuNamDenNamOrigin = string.Format("Vốn bố trí năm {0} - {1}", Model.IGiaiDoanTu, Model.IGiaiDoanDen);
                Model.HeaderVonBoTriSauNamOrigin = string.Format("Vốn bố trí sau năm {0}", (Model.IGiaiDoanTu + 4).ToString());

                Model.HeaderVonDaGiao = string.Format("Vốn đã giao năm {0}{1}", (Model.IGiaiDoanTu - 1).ToString(), Model.IIdParentId != null ? " (Sau điều chỉnh)" : "");
                Model.HeaderVonBoTriTuNamDenNam = string.Format("Vốn bố trí năm {0} - {1}{2}", Model.IGiaiDoanTu, Model.IGiaiDoanDen, Model.IIdParentId != null ? " (Sau điều chỉnh)" : "");
                Model.HeaderVonBoTriSauNam = string.Format("Vốn bố trí sau năm {0}{1}", Model.IGiaiDoanDen, Model.IIdParentId != null ? " (Sau điều chỉnh)" : "");
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                Items = new ObservableCollection<VdtKhvKeHoach5NamChiTietModel>();
                // Mặc định
                List<VdtKhvKeHoach5NamChiTietQuery> lstItems = _vdtKhvKeHoach5NamChiTietService.FindByKeHoach5NamChiTiet(Model.Id.ToString()).ToList();
                Items = _mapper.Map<ObservableCollection<VdtKhvKeHoach5NamChiTietModel>>(lstItems);

                CalculatTotal();

                CreateDropdownLoaiCongTrinh();
                CreateDropDownDuAn();
                CreateDropDownDonViQuanLy();
                CreateDropDownNguonVon();

                Items.Select(item => { item.BActive = Model.BActive && !Model.IsViewDetail && !Model.BKhoa; item.PropertyChanged += DetailModel_PropertyChanged; return item; }).ToList();                
                Items.OrderBy(n => n.STT);
                _vdtKhvKhthView = CollectionViewSource.GetDefaultView(Items);
                _vdtKhvKhthView.Filter = KeHoachTrungHanFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSearch()
        {
            _vdtKhvKhthView.Refresh();
            CalculatTotal();
        }

        private bool KeHoachTrungHanFilter(object obj)
        {
            try
            {
                if (!(obj is VdtKhvKeHoach5NamChiTietModel temp)) return true;
                var bCondition = true;

                if (_drpLoaiCongTrinhSelected != null)
                {
                    bCondition = bCondition && temp.IIdLoaiCongTrinhId.HasValue && temp.IIdLoaiCongTrinhId == Guid.Parse(_drpLoaiCongTrinhSelected.ValueItem);
                }

                if (_drpTenDuAnSelected != null)
                {
                    bCondition = bCondition && temp.IIdDuAnId != null && temp.IIdDuAnId == Guid.Parse(_drpTenDuAnSelected.ValueItem);
                }

                if (_drpDonViQuanLySelected != null)
                {
                    bCondition = bCondition && !string.IsNullOrEmpty(temp.IIdMaDonVi) && temp.IIdMaDonVi == _drpDonViQuanLySelected.ValueItem;
                }

                if (_drpNguonVonSelected != null)
                {
                    bCondition = bCondition && temp.IIdNguonVonId == Int32.Parse(_drpNguonVonSelected.ValueItem);
                }

                if (!string.IsNullOrEmpty(SDiaDiemThucHien))
                {
                    //bCondition = bCondition && !string.IsNullOrEmpty(temp.SDiaDiem) && temp.SDiaDiem.ToLower().StartsWith(SDiaDiemThucHien.ToLower());
                    bCondition = bCondition && temp.SDiaDiem.ToLower().Contains(SDiaDiemThucHien.ToLower());
                }

                if (!string.IsNullOrEmpty(SThoiGianThucHien))
                {
                    bCondition = bCondition && !string.IsNullOrEmpty(temp.ThoiGianThucHien) && temp.ThoiGianThucHien.ToLower().StartsWith(SThoiGianThucHien.ToLower());
                }

                temp.IsFilter = bCondition;
                return bCondition;
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return true;
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                VdtKhvKeHoach5NamChiTietModel item = (VdtKhvKeHoach5NamChiTietModel)sender;
                switch (args.PropertyName)
                {
                    case nameof(VdtKhvKeHoach5NamChiTietModel.FVonBoTriTuNamDenNam):
                    case nameof(VdtKhvKeHoach5NamChiTietModel.FVonDaGiao):
                    case nameof(VdtKhvKeHoach5NamChiTietModel.SGhiChu):
                    case nameof(VdtKhvKeHoach5NamChiTietModel.FHanMucDauTu):
                        item.IsModified = true;

                        item.FGiaTriSau5Nam = (item.FHanMucDauTu ?? 0) - (item.FVonBoTriTuNamDenNam ?? 0);
                        if (item.FGiaTriSau5Nam < 0)
                        {
                            MessageBoxHelper.Error(Resources.VoucherBeforeYearLimit);
                        }
                        if (args.PropertyName != nameof(VdtKhvKeHoach5NamChiTietModel.SGhiChu))
                        {
                            CalculatTotal();
                        }
                        OnPropertyChanged(nameof(IsSaveData));
                        OnPropertyChanged(nameof(Items));
                        break;  
                    //case nameof(VdtKhvKeHoach5NamChiTietModel.FHanMucDauTu):
                    case nameof(VdtKhvKeHoach5NamChiTietModel.FGiaTriSau5Nam):
                    case nameof(VdtKhvKeHoach5NamChiTietModel.FGiaTriSau5NamOrigin):
                        if ((item.FHanMucDauTu ?? 0) < (item.FGiaTriSau5Nam ?? 0 + item.FGiaTriSau5NamOrigin ?? 0))
                            MessageBoxHelper.Error(Resources.MsgErrorTongVonTrungHanKhongQuaHanMucDauTu);
                        break; 
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnResetFilter()
        {
            _drpLoaiCongTrinhSelected = null;
            _drpTenDuAnSelected = null;
            _drpDonViQuanLySelected = null;
            _drpNguonVonSelected = null;

            SDiaDiemThucHien = string.Empty;
            SThoiGianThucHien = string.Empty;

            OnPropertyChanged(nameof(DrpLoaiCongTrinhSelected));
            OnPropertyChanged(nameof(DrpTenDuAnSelected));
            OnPropertyChanged(nameof(DrpDonViQuanLySelected));
            OnPropertyChanged(nameof(DrpNguonVonSelected));
        }

        protected override void OnAdd()
        {
            try
            {
                if (!IsDuAnChuyenTiep)
                {
                    List<DuAnKeHoachTrungHanModel> duAnExisted = _mapper.Map<List<DuAnKeHoachTrungHanModel>>(Items.ToList());
                    ProjectInPlanManagerApprovedDiaLogViewModel.DuAnExisted = duAnExisted;
                }
                ProjectInPlanManagerApprovedDiaLogViewModel.Model = Model;
                ProjectInPlanManagerApprovedDiaLogViewModel.Init();
                ProjectInPlanManagerApprovedDiaLogViewModel.ChooseDuAnAction = obj => OnChooseDuAn(obj);
                ProjectInPlanManagerApprovedDiaLogViewModel.ChooseDuAnDeXuatAction = obj => OnDuAnDeXuatChoise(obj);
                ProjectInPlanManagerApprovedDiaLogViewModel.ShowDialog();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        public string AddSTT(List<VdtKhvKeHoach5NamChiTietModel> items)
        {
            List<int> Thutus = items.Select(n => Convert.ToInt32(n.STT)).ToList();
            return (Thutus.Max() + 1).ToString();
        }

        private void OnChooseDuAn(object obj)
        {
            try
            {
                if (obj is List<DuAnKeHoachTrungHanModel> itemsChoise)
                {
                    foreach (var item in itemsChoise.OrderBy(x => x.SMaDuAn))
                    {
                        // Map đề xuất chi tiết to chi tiết được duyệt
                        var itemMap = _mapper.Map<VdtKhvKeHoach5NamChiTietModel>(item);
                        itemMap.Id = Guid.NewGuid();
                        itemMap.BActive = true;
                        itemMap.IIdKeHoach5NamId = null;
                        itemMap.IGiaiDoanTu = Model.IGiaiDoanTu;
                        itemMap.IGiaiDoanDen = Model.IGiaiDoanDen;
                        itemMap.IsModified = true;
                        if(Items!= null && Items.Count>0)
                            itemMap.STT = AddSTT(Items.ToList());
                        else
                            itemMap.STT = "1";
                        Items.Add(itemMap);
                    }

                    CalculatTotal();

                    Items.Select(item => { item.PropertyChanged += DetailModel_PropertyChanged; return item; }).ToList();

                    CreateDropdownLoaiCongTrinh();
                    CreateDropDownDuAn();
                    CreateDropDownDonViQuanLy();
                    CreateDropDownNguonVon();

                    OnPropertyChanged(nameof(Items));
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnDuAnDeXuatChoise(object obj)
        {
            try
            {
                if (obj is List<VdtKhvKeHoach5NamDeXuatChiTietModel> itemsChoise)
                {
                    foreach (var item in itemsChoise)
                    {
                        // Map đề xuất chi tiết to chi tiết được duyệt
                        var itemMap = _mapper.Map<VdtKhvKeHoach5NamChiTietModel>(item);
                        itemMap.Id = Guid.NewGuid();
                        itemMap.BActive = true;
                        itemMap.IIdKeHoach5NamId = null;
                        itemMap.IsModified = true;
                        if (Items != null && Items.Count > 0)
                            itemMap.STT = AddSTT(Items.ToList());
                        else
                            itemMap.STT = "1";
                        Items.Add(itemMap);
                    }
                    Items.Select(item => { item.PropertyChanged += DetailModel_PropertyChanged; return item; }).ToList();
                    CalculatTotal();
                    
                    CreateDropdownLoaiCongTrinh();
                    CreateDropDownDuAn();
                    CreateDropDownDonViQuanLy();
                    CreateDropDownNguonVon();

                    OnPropertyChanged(nameof(Items));
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnDelete()
        {
            try
            {
                if (!IsActive) return;
                if (Items != null && Items.Count > 0 && SelectedItem != null)
                {
                    SelectedItem.IsDeleted = !SelectedItem.IsDeleted;

                    CalculatTotal();
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnRefresh()
        {
            if (Items.Any(x => x.IsModified || x.IsDeleted || x.IsAdded))
            {
                var messageBox = new NSMessageBoxViewModel(Resources.MsgConfirmEdit, "Xác nhận", NSMessageBoxButtons.YesNo, RefreshConfirmEventHandler);
                DialogHost.Show(messageBox.Content, "PlanManagerApprovedDetail");
            }
            else
            {
                LoadData();
            }
        }

        private void RefreshConfirmEventHandler(NSDialogResult result)
        {
            if (result == NSDialogResult.No) LoadData();
        }

        public override void OnSave()
        {
            try
            {
                foreach(var item in Items.Where(n => !n.IsDeleted))
                {
                    if ((item.FHanMucDauTu ?? 0) < (item.FGiaTriSau5Nam ?? 0 + item.FGiaTriSau5NamOrigin ?? 0))
                    {
                        MessageBoxHelper.Error(Resources.MsgErrorTongVonTrungHanKhongQuaHanMucDauTu);
                        return;
                    }
                }

                if (!IsActive) return;
                List<VdtKhvKeHoach5NamChiTietModel> listChungTuChiTietAdd = Items.Where(x => x.IsModified && x.IIdKeHoach5NamId == null).ToList();
                List<VdtKhvKeHoach5NamChiTietModel> listChungTuChiTietUpdate = Items.Where(x => x.IsModified && x.IIdKeHoach5NamId != null).ToList();
              

                StringBuilder messageBuilder = new StringBuilder();

                if (Model != null && Model.ILoai.Equals((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI) && Items.Any(x => !x.FHanMucDauTu.Equals(x.FTongVonBoTri) && x.IIdNguonVonId.Equals((int)MediumTermType.Nsqp)))
                {
                    messageBuilder.AppendFormat(Resources.VoucherInvestementLimit);
                }

                if (Items.Any(x => x.FGiaTriSau5Nam < 0))
                {
                    messageBuilder.AppendFormat(Resources.VoucherBeforeYearLimit);
                }

                if (messageBuilder.Length != 0)
                {
                    MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                    return;
                }

                // Thêm mới chi tiết
                if (listChungTuChiTietAdd.Count > 0)
                {
                    listChungTuChiTietAdd = listChungTuChiTietAdd.Select(x =>
                    {
                        x.Id = Guid.NewGuid();
                        x.IIdKeHoach5NamId = Model.Id;
                        x.STen = x.STenDuAn;
                        x.BActive = true;
                        return x;
                    }).ToList();

                    List<VdtKhvKeHoach5NamChiTiet> listChungTuChiTiets = _mapper.Map<List<VdtKhvKeHoach5NamChiTiet>>(listChungTuChiTietAdd);
                    _vdtKhvKeHoach5NamChiTietService.AddRange(listChungTuChiTiets);
                }

                // Cập nhật chi tiết
                if (listChungTuChiTietUpdate.Count > 0)
                {
                    listChungTuChiTietUpdate = listChungTuChiTietUpdate.Select(x =>
                    {
                        x.STen = x.STenDuAn;
                        x.BActive = true;
                        return x;
                    }).ToList();
                    foreach (var item in listChungTuChiTietUpdate)
                    {
                        VdtKhvKeHoach5NamChiTiet chungTuChiTiet = _vdtKhvKeHoach5NamChiTietService.FindById(item.Id);
                        if (chungTuChiTiet != null)
                        {
                            _mapper.Map(item, chungTuChiTiet);
                            _vdtKhvKeHoach5NamChiTietService.Update(chungTuChiTiet);
                        }
                    }
                }
                List<VdtKhvKeHoach5NamChiTietModel> listChungTuChiTietDelete = Items.Where(x => x.IsDeleted && x.IIdKeHoach5NamId != null).ToList();
                // Xóa chi tiết
                if (listChungTuChiTietDelete.Count > 0)
                {
                    if (listChungTuChiTietDelete.Count > 0)
                    {
                        foreach (var item in listChungTuChiTietDelete)
                        {
                            _vdtKhvKeHoach5NamChiTietService.Delete(item.Id);
                        }
                    }
                }

                // Cập nhật thông tin chứng từ
                VdtKhvKeHoach5Nam chungTu = _vdtKhvKeHoach5NamService.FindById(Model.Id);
                if (chungTu != null)
                {
                    chungTu.FGiaTriDuocDuyet = Items.Sum(x => x.FVonBoTriTuNamDenNam);
                    chungTu.IIDKhthDeXuat = _vdtKhvKeHoach5NamService.FindIdKHTHByID(chungTu.Id);
                    _vdtKhvKeHoach5NamService.Update(chungTu);
                }

                // Reset trạng thái form
                var listItems = Items.Where(x => !x.IsDeleted).Select(x => { x.IsModified = false; return x; }).ToList();
                Items = new ObservableCollection<VdtKhvKeHoach5NamChiTietModel>(listItems);
                ProjectInPlanManagerApprovedDiaLogViewModel.DuAnExisted.Clear();

                LoadData();

                // Message notify
                string message = Resources.MsgSaveDone;
                var messageBox = new NSMessageBoxViewModel(message);
                DialogHost.Show(messageBox.Content, "PlanManagerApprovedDetail");

                // Refresh dữ liệu ở màn index
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

        private void CalculatTotal()
        {
            try
            {
                _summaryItem.FHanMucDauTu = Items.Where(x => x.IsFilter).Sum(x => x.FHanMucDauTu);
                _summaryItem.FVonDaGiao = Items.Where(x => x.IsFilter).Sum(x => x.FVonDaGiao);
                _summaryItem.FVonBoTriTuNamDenNam = Items.Where(x => x.IsFilter).Sum(x => x.FVonBoTriTuNamDenNam);
                _summaryItem.FGiaTriSau5Nam = Items.Where(x => x.IsFilter).Sum(x => x.FGiaTriSau5Nam);
                _summaryItem.FVonDaGiaoOrigin = Items.Where(x => x.IsFilter).Sum(x => x.FVonDaGiaoOrigin);
                _summaryItem.FVonBoTriTuNamDenNamOrigin = Items.Where(x => x.IsFilter).Sum(x => x.FVonBoTriTuNamDenNamOrigin);
                _summaryItem.FGiaTriSau5NamOrigin = Items.Where(x => x.IsFilter).Sum(x => x.FGiaTriSau5NamOrigin);
                _summaryItem.FVonBoTriNamTruoc = Items.Where(x => x.IsFilter).Sum(x => x.FVonBoTriNamTruoc);
                _summaryItem.FVonDaBoTriNamNay = Items.Where(x => x.IsFilter).Sum(x => x.FVonDaBoTriNamNay);

                //Model.FGiaTriKeHoach = Items.Where(x => x.IsFilter && x.IIdNguonVonId.Equals((int)MediumTermType.Nsqp)).Sum(x => x.FHanMucDauTu);             //dong code gay loi tinh sai gia tri ke hoach

                OnPropertyChanged(nameof(SummaryItem));
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateDropdownLoaiCongTrinh()
        {
            var lstData = Items.Where(n => n.IIdLoaiCongTrinhId.HasValue).Select(n => new SelectedItemModel() { Value = n.IIdLoaiCongTrinhId.ToString(), DisplayName = n.STenLoaiCongTrinh }).Distinct().ToList();
            lstData = lstData.GroupBy(n => new { n.DisplayName, n.Value }).Select(g => g.First()).ToList();
            DrpLoaiCongTrinhs = new ObservableCollection<ComboboxItem>(_mapper.Map<List<ComboboxItem>>(lstData));
        }

        private void CreateDropDownDuAn()
        {
            var lstData = Items.Where(n => n.IIdDuAnId != null).Select(n => new SelectedItemModel() { Value = n.IIdDuAnId.ToString(), DisplayName = string.Format("{0} - {1}", n.SMaDuAn, n.STenDuAn)}).Distinct().ToList();
            lstData = lstData.GroupBy(n => new { n.DisplayName, n.Value }).Select(g => g.First()).ToList();
            DrpTenDuAns = new ObservableCollection<ComboboxItem>(_mapper.Map<List<ComboboxItem>>(lstData));
        }

        private void CreateDropDownDonViQuanLy()
        {
            var lstData = Items.Where(n => !string.IsNullOrEmpty(n.IIdMaDonVi)).Select(n => new SelectedItemModel() { Value = n.IIdMaDonVi.ToString(), DisplayName = string.Format("{0} - {1}", n.IIdMaDonVi, n.STenDonVi)}).Distinct().ToList();
            lstData = lstData.GroupBy(n => new { n.DisplayName, n.Value }).Select(g => g.First()).ToList();
            DrpDonViQuanLys = new ObservableCollection<ComboboxItem>(_mapper.Map<List<ComboboxItem>>(lstData));
        }

        private void CreateDropDownNguonVon()
        {
            var lstData = Items.Where(n => n.IIdNguonVonId != 0).Select(n => new SelectedItemModel() { Value = n.IIdNguonVonId.ToString(), DisplayName = n.STenNguonVon}).Distinct().ToList();
            lstData = lstData.GroupBy(n => new { n.DisplayName, n.Value }).Select(g => g.First()).ToList();
            DrpNguonVons = new ObservableCollection<ComboboxItem>(_mapper.Map<List<ComboboxItem>>(lstData));
        }

        public override void OnClose(object obj)
        {
            try
            {
                ProjectInPlanManagerApprovedDiaLogViewModel.DuAnExisted.Clear();

                var window = obj as Window;
                window.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
