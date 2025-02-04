using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Expertise
{
    public class ExpertiseDialogViewModel : DialogViewModelBase<VTS.QLNS.CTC.App.Model.ExpertiseModel>
    {
        private readonly INsDonViService _nSDonViService;
        private readonly ISktNganhThamDinhService _chungTuService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly IDanhMucService _danhMucService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private ICollectionView _listDonViView;

        public override string Name => "THÊM MỚI THẨM ĐỊNH NGÀNH";
        public override string Title => "THÊM CHỨNG TỪ";
        public override string Description => "Thêm mới chứng từ thẩm định ngành";
        public override Type ContentType => typeof(View.Budget.DemandCheck.Expertise.ExpertiseDialog);
        public override PackIconKind IconKind => PackIconKind.FileDocument;

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _listDonViView.Refresh();
                }
            }
        }

        // Fix lỗi tự động cạp nhập thông tin mô tả và ngày chứng từ xuống màn danh sách khi chưa nhấn lưu(chỉ nhấn button đóng)
        public string MoTa { get; set; }
        public DateTime? NgayChungTu { get; set; }


        private ObservableCollection<CheckBoxItem> _listDonVi;
        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }

        private ObservableCollection<ComboboxItem> _dataPhanLoai;
        public ObservableCollection<ComboboxItem> DataPhanLoai
        {
            get => _dataPhanLoai;
            set => SetProperty(ref _dataPhanLoai, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiThamDinh;
        public ObservableCollection<ComboboxItem> DataLoaiThamDinh
        {
            get => _dataLoaiThamDinh;
            set => SetProperty(ref _dataLoaiThamDinh, value);
        }

        private ComboboxItem _selectedPhanLoai;
        public ComboboxItem SelectedPhanLoai
        {
            get => _selectedPhanLoai;
            set
            {
                if (SetProperty(ref _selectedPhanLoai, value) && _selectedPhanLoai != null)
                {
                    LoadDonVi();
                }
            }
        }

        private ComboboxItem _selectedLoaiThamDinh;
        public ComboboxItem SelectedLoaiThamDinh
        {
            get => _selectedLoaiThamDinh;
            set => SetProperty(ref _selectedLoaiThamDinh, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiNganSach;
        public ObservableCollection<ComboboxItem> DataLoaiNganSach
        {
            get => _dataLoaiNganSach;
            set => SetProperty(ref _dataLoaiNganSach, value);
        }

        private ComboboxItem _selectedLoaiNganSach;
        public ComboboxItem SelectedLoaiNganSach
        {
            get => _selectedLoaiNganSach;
            set
            {
                if (SetProperty(ref _selectedLoaiNganSach, value) && _selectedLoaiNganSach != null)
                {
                    LoadDonVi();
                }
            }
        }

        public ExpertiseDialogViewModel(
          INsDonViService nSDonViService,
          ISktNganhThamDinhService chungTuService,
          ISessionService sessionService,
          IDanhMucService danhMucService,
          INsNguoiDungDonViService nsNguoiDungDonViService,
          ILog logger,
          IMapper mapper)
        {
            _nSDonViService = nSDonViService;
            _chungTuService = chungTuService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _mapper = mapper;
            _logger = logger;
        }

        private bool CheckCreateCTC(Guid id)
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            if (id != Guid.Empty)
            {
                predicate = predicate.And(x => x.Id != id);
            }
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoai == LoaiNganhThamDinh.CTCTCDN);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(SelectedLoaiNganSach.ValueItem));
            NsSktNganhThamDinh chungTuDeNghi = _chungTuService.FindByCondition(predicate).FirstOrDefault();
            if (chungTuDeNghi != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckCreateNTD(string idDonVi, Guid id)
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            if (id != Guid.Empty)
            {
                predicate = predicate.And(x => x.Id != id);
            }
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD);
            predicate = predicate.And(x => x.IIdMaDonVi == idDonVi);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(SelectedLoaiNganSach.ValueItem));
            NsSktNganhThamDinh chungTuDeNghi = _chungTuService.FindByCondition(predicate).FirstOrDefault();
            if (chungTuDeNghi != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override void OnSave()
        {
            try
            {
                if (SelectedLoaiNganSach == null)
                {
                    return;
                }
                if (!NgayChungTu.HasValue)
                {
                    MessageBoxHelper.Warning(Resources.AlertNgayChungTuEmpty);
                    return;
                }
                if (ListDonVi == null || ListDonVi.Count == 0 || ListDonVi.Where(n => n.IsChecked).Count() == 0)
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                CheckBoxItem itemDonVi = ListDonVi.Where(n => n.IsChecked).FirstOrDefault();

                if (SelectedPhanLoai.ValueItem == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTCTCDN.ToString())
                {
                    if (!CheckCreateCTC(Model.Id))
                    {
                        MessageBoxHelper.Warning(Resources.MsgExistVoucher);
                        return;
                    }
                }
                else if (SelectedPhanLoai.ValueItem == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD.ToString())
                {
                    if (!CheckCreateNTD(itemDonVi.ValueItem, Model.Id))
                    {
                        MessageBoxHelper.Warning(Resources.MsgExistVoucherAgency);
                        return;
                    }
                }

                Model.NamLamViec = _sessionService.Current.YearOfWork;
                Model.NamNganSach = _sessionService.Current.YearOfBudget;
                Model.NguonNganSach = _sessionService.Current.Budget;
                Model.MoTa = MoTa;
                Model.NgayChungTu = NgayChungTu;
                if (itemDonVi != null)
                {
                    Model.IdDonVi = itemDonVi.ValueItem;
                    Model.TenDonVi = itemDonVi.NameItem;
                }

                NsSktNganhThamDinh entity = new NsSktNganhThamDinh(); ;
                if (Model.Id != Guid.Empty)
                {
                    // Update
                    entity = _chungTuService.Find(Model.Id);
                    _mapper.Map(Model, entity);
                    if (SelectedPhanLoai != null && !string.IsNullOrEmpty(SelectedPhanLoai.ValueItem))
                    {
                        entity.ILoai = int.Parse(SelectedPhanLoai.ValueItem);
                    }
                    if (SelectedLoaiNganSach != null && !string.IsNullOrEmpty(SelectedLoaiNganSach.ValueItem))
                    {
                        entity.ILoaiChungTu = int.Parse(SelectedLoaiNganSach.ValueItem);
                    }

                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    _chungTuService.Update(entity);
                }
                else
                {
                    // Add
                    int soChungTuIndex = _chungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                    entity = new NsSktNganhThamDinh();
                    entity = _mapper.Map<NsSktNganhThamDinh>(Model);
                    entity.ISoChungTuIndex = soChungTuIndex;
                    if (SelectedPhanLoai != null && !string.IsNullOrEmpty(SelectedPhanLoai.ValueItem))
                    {
                        entity.ILoai = int.Parse(SelectedPhanLoai.ValueItem);
                    }
                    if (SelectedLoaiNganSach != null && !string.IsNullOrEmpty(SelectedLoaiNganSach.ValueItem))
                    {
                        entity.ILoaiChungTu = int.Parse(SelectedLoaiNganSach.ValueItem);
                    }
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    _chungTuService.Add(entity);
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<VTS.QLNS.CTC.App.Model.ExpertiseModel>(entity));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadPhanLoai()
        {
            DataPhanLoai = new ObservableCollection<ComboboxItem>();
            DataPhanLoai.Add(new ComboboxItem { ValueItem = LoaiNganhThamDinh.CTCTCDN.ToString(), DisplayItem = LoaiNganhThamDinh.TEN_CTCTCDN });
            DataPhanLoai.Add(new ComboboxItem { ValueItem = LoaiNganhThamDinh.CTNTD.ToString(), DisplayItem = LoaiNganhThamDinh.TEN_CTNTD });
            SelectedPhanLoai = DataPhanLoai.FirstOrDefault();
        }

        private void LoaiLoaiThamDinh()
        {
            DataLoaiThamDinh = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem { ValueItem = LoaiNganhThamDinh.INOI_BO.ToString(), DisplayItem = LoaiNganhThamDinh.TOAN_QUAN },
                new ComboboxItem { ValueItem = LoaiNganhThamDinh.ITOAN_QUAN.ToString(), DisplayItem = LoaiNganhThamDinh.NOI_BO }
            };
            SelectedLoaiThamDinh = DataLoaiThamDinh.FirstOrDefault();
        }

        private void LoadLoaiNganSach()
        {
            DataLoaiNganSach = new ObservableCollection<ComboboxItem>();
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSSD_Key, DisplayItem = VoucherType.NSSD_Value });
            DataLoaiNganSach.Add(new ComboboxItem { ValueItem = VoucherType.NSBD_Key, DisplayItem = VoucherType.NSBD_Value });
            SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault();
        }

        public override void Init()
        {
            try
            {
                LoadPhanLoai();
                LoaiLoaiThamDinh();
                LoadLoaiNganSach();
                LoadDonVi();
                if (Model == null) Model = new Model.ExpertiseModel();
                if (Model.Id == Guid.Empty)
                {
                    // Add
                    Model = new Model.ExpertiseModel();
                    int soChungTuIndex = _chungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                    Model.SoChungTu = "THD-" + soChungTuIndex.ToString("D3");
                    //Model.NgayChungTu = DateTime.Now;
                    Model.NgayQuyetDinh = DateTime.Now;
                    MoTa = "";
                    NgayChungTu = DateTime.Now;
                }
                else
                {
                    // Update
                    if (Model.ILoai.HasValue)
                    {
                        SelectedPhanLoai = DataPhanLoai.Where(n => n.ValueItem == Model.ILoai.Value.ToString()).FirstOrDefault();
                    }
                    if (Model.ILoaiChungTu.HasValue)
                    {
                        SelectedLoaiNganSach = DataLoaiNganSach.FirstOrDefault(n => n.ValueItem == Model.ILoaiChungTu.Value.ToString());
                    }
                    CheckBoxItem itemDonVi = ListDonVi.Where(n => n.ValueItem == Model.IdDonVi).FirstOrDefault();
                    if (itemDonVi != null)
                    {
                        itemDonVi.IsChecked = true;
                    }
                    MoTa = Model.MoTa;
                    NgayChungTu = Model.NgayChungTu;
                }
                //OnPropertyChanged(nameof(MoTa));
                OnPropertyChanged(nameof(SelectedLoaiNganSach));
                OnPropertyChanged(nameof(SelectedPhanLoai));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private Expression<Func<DonVi, bool>> CreatePredicate()
        {
            var session = _sessionService.Current;
            var predicate = PredicateBuilder.True<DonVi>();
            if (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == LoaiNganhThamDinh.CTCTCDN.ToString())
            {
                if (CheckDonViCondition())
                {
                    predicate = predicate.And(x => (x.Loai == LoaiDonVi.ROOT));
                }
                else
                {
                    predicate = predicate.And(x => false);
                }
            }
            else
            {
                if (CheckDonViThamDinhCondition())
                {
                    predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO) && x.BCoNSNganh);
                }
                else
                {
                    predicate = predicate.And(x => (x.Loai == LoaiDonVi.ROOT) && x.BCoNSNganh);
                }
            }
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            return predicate;
        }

        public bool CheckDonViThamDinhCondition()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO));
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            List<DonVi> listDonVi = _nSDonViService.FindByCondition(predicate).ToList();
            if (listDonVi != null && listDonVi.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckDonViCondition()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT));
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.BCoNSNganh);
            List<DonVi> listDonVi = _nSDonViService.FindByCondition(predicate).ToList();
            if (listDonVi != null && listDonVi.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<DonVi> GetDonViHasCheckDonViCondition()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT));
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.BCoNSNganh);
            List<DonVi> listDonVi = _nSDonViService.FindByCondition(predicate).ToList();
            return listDonVi;
        }

        private List<NguoiDungDonVi> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionService.Current.Principal));
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            List<NguoiDungDonVi> nsDungDonVis = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return nsDungDonVis;
        }

        public void LoadDonVi()
        {
            ListDonVi = new ObservableCollection<CheckBoxItem>();
            var predicate = CreatePredicate();
            IEnumerable<DonVi> listDonVi = _nSDonViService.FindByCondition(predicate).ToList();
            List<NguoiDungDonVi> listNguoiDungDonVi = GetListNguoiDungDonVi();
            if (listNguoiDungDonVi != null && listNguoiDungDonVi.Count > 0)
            {
                listDonVi = listDonVi.Where(n => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(n.IIDMaDonVi)).ToList();
            }
            else
            {
                listDonVi = new List<DonVi>();
            }
            if (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == LoaiNganhThamDinh.CTCTCDN.ToString())
            {
                List<DonVi> donVi = GetDonViHasCheckDonViCondition();
                List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => !string.IsNullOrEmpty(n.SGiaTri)).ToList();

                donVi = donVi.Where(n => listDanhMuc.Where(t => !string.IsNullOrEmpty(t.SGiaTri)).Select(x => x.SGiaTri).ToList().Contains(n.IIDMaDonVi)).ToList();
                if (donVi.Count == 0)
                {
                    listDonVi = new List<DonVi>();
                }
            }
            else
            {
                List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => !string.IsNullOrEmpty(n.SGiaTri)).ToList();
                var listGiaTri = listDanhMuc.Where(x => !string.IsNullOrEmpty(x.SGiaTri)).Select(x => x.SGiaTri);
                listDonVi = listDonVi.Where(n => listGiaTri.Any(x => x.Split(",").Contains(n.IIDMaDonVi)));
            }

            if (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == LoaiNganhThamDinh.CTNTD.ToString())
            {
                var lstIdChungTuExclude = GetListIdDonViDaTaoCt();
                listDonVi = listDonVi.Where(x => !lstIdChungTuExclude.Contains(x.IIDMaDonVi));
            }

            ListDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
            // Filter
            _listDonViView = CollectionViewSource.GetDefaultView(ListDonVi);
            _listDonViView.Filter = ListDonViFilter;
        }

        public List<string> GetListIdDonViDaTaoCt()
        {
            int loai = LoaiNganhThamDinh.CTCTCDN;
            int loaiNganSach = int.Parse(VoucherType.NSSD_Key);
            if (SelectedPhanLoai != null && SelectedPhanLoai.ValueItem == LoaiNganhThamDinh.CTNTD.ToString())
            {
                loai = LoaiNganhThamDinh.CTNTD;
            }

            if (SelectedLoaiNganSach != null && SelectedLoaiNganSach.ValueItem == VoucherType.NSBD_Key.ToString())
            {
                loaiNganSach = int.Parse(VoucherType.NSBD_Key);
            }

            IEnumerable<ThDChungTuQuery> data = _chungTuService.FindByNamLamViec(_sessionService.Current.YearOfWork,
                _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, _sessionService.Current.Principal,
                loai, loaiNganSach);
            if (data != null && data.Any())
            {
                return data.Select(x => x.IdDonVi).ToList();
            }

            return new List<string>();
        }

        private bool CheckExistDonVi(string idDonVi, Guid id)
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdMaDonVi == idDonVi);
            if (id != Guid.Empty)
            {
                predicate = predicate.And(x => x.Id != id);
            }
            List<NsSktNganhThamDinh> list = _chungTuService.FindByCondition(predicate).ToList();
            if (list != null && list.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ListDonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchDonVi))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.ValueItem.ToLower().Contains(_searchDonVi!.ToLower());
        }
    }
}
