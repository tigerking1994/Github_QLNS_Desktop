using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.CBNHKeHoach
{
    public class CopyCanBoThangDialogViewModel : DialogViewModelBase<TlCBNHSaoChepNamKeHoachModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDmCapBacService _tlDmCapBacService;

        private readonly ITlDsCBNHKeHoachService _tlDsCBNHKeHoachService;

        private readonly ITlDmCapBacKeHoachService _tlDmCapBacKeHoachService;

        public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH_DANH_SACH_DOI_TUONG_HUONG_LUONG_KE_HOACH_COPY;

        public override string Title => "Sao chép nhanh đối tượng";
        public override string Description => "Sao chép nhanh đối tượng theo tháng";
        public override Type ContentType => typeof(View.Salary.SalaryManagementPlan.CBNHKeHoach.CopyCanBoThangDialog);

        private List<TlDmCapBacKeHoach> lstCapBacKeHoach;
        private List<TlDsCBNHKeHoach> lstCanBoNghiHuuKeHoachNam;

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _fromMonthSelected;
        public ComboboxItem FromMonthSelected
        {
            get => _fromMonthSelected;
            set
            {
                if (SetProperty(ref _fromMonthSelected, value))
                {
                    LoadData();
                }
            }
        }

        private ComboboxItem _toMonthSelected;
        public ComboboxItem ToMonthSelected
        {
            get => _toMonthSelected;
            set => SetProperty(ref _toMonthSelected, value);
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _fromYearSelected;
        public ComboboxItem FromYearSelected
        {
            get => _fromYearSelected;
            set
            {
                SetProperty(ref _fromYearSelected, value);
                LoadData();
            }
        }

        private ComboboxItem _toYearSelected;
        public ComboboxItem ToYearSelected
        {
            get => _toYearSelected;
            set => SetProperty(ref _toYearSelected, value);
        }

        private ObservableCollection<TlDmDonViModel> _donViItems;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViModel _selectedDonViItems;
        public TlDmDonViModel SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                SetProperty(ref _selectedDonViItems, value);
            }
        }

        private bool _selectedAllDonvi;
        public bool SelectedAllDonvi
        {
            get
            {
                if (DonViItems != null && DonViItems.Count > 0)
                {
                    return DonViItems.All(item => item.Selected);
                }
                return false;
            }
            set
            {
                SetProperty(ref _selectedAllDonvi, value);
                foreach (var item in DonViItems) item.Selected = _selectedAllDonvi;
            }
        }

        private ObservableCollection<CadresModel> _cadresItems;
        public ObservableCollection<CadresModel> CadresItems
        {
            get => _cadresItems;
            set => SetProperty(ref _cadresItems, value);
        }

        private CadresModel _selectedCarderItems;
        public CadresModel SelectedCarderItems
        {
            get => _selectedCarderItems;
            set => SetProperty(ref _selectedCarderItems, value);
        }

        private bool _selectedAllCadres;
        public bool SelectedAllCadres
        {
            get
            {
                if (CadresItems != null && CadresItems.Count > 0)
                {
                    return CadresItems.All(item => item.IsSelected);
                }
                return false;
            }
            set
            {
                SetProperty(ref _selectedAllCadres, value);
                foreach (var item in CadresItems) item.IsSelected = _selectedAllCadres;
            }
        }

        public string LabelSelectedCountCadres
        {
            get
            {
                if (CadresItems != null && CadresItems.Count > 0)
                {
                    var totalCount = CadresItems.Count;
                    var totalSelected = CadresItems.Count(x => x.IsSelected);
                    return $"ĐỐI TƯỢNG ({totalSelected}/{totalCount})";
                }
                return $"ĐỐI TƯỢNG (0/0)";
            }
        }

        public string LabelSelectedCountDonvi
        {
            get
            {
                if (DonViItems != null && DonViItems.Count > 0)
                {
                    var totalCount = DonViItems.Count;
                    var totalSelected = DonViItems.Count(x => x.Selected);
                    return $"ĐƠN VỊ ({totalSelected}/{totalCount})";
                }
                return $"ĐƠN VỊ (0/0)";
            }
        }

        public CopyCanBoThangDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmCanBoService cadresService,
            ITlDmDonViService tlDmDonViService,
            ITlDsCBNHKeHoachService tlDsCBNHKeHoachService,


            ITlDmCapBacKeHoachService tlDmCapBacKeHoachService,
            ITlDmCapBacService tlDmCapBacService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;

            _cadresService = cadresService;
            _tlDmDonViService = tlDmDonViService;
            _tlDsCBNHKeHoachService = tlDsCBNHKeHoachService;


            _tlDmCapBacKeHoachService = tlDmCapBacKeHoachService;
            _tlDmCapBacService = tlDmCapBacService;
        }

        public override void Init()
        {
            base.Init();
            lstCapBacKeHoach = _tlDmCapBacKeHoachService.FindAll().ToList();
            LoadData();
            LoadMonths();
            LoadYears();
            LoadDonVi();
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            OnPropertyChanged(nameof(Months));
            FromMonthSelected = Months.FirstOrDefault(x => x.ValueItem.Equals(Model.Month.ToString()));
        }

        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            for (int i = _sessionService.Current.YearOfWork - 2; i <= _sessionService.Current.YearOfWork + 2; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            OnPropertyChanged(nameof(Years));
            FromYearSelected = Years.FirstOrDefault(x => x.ValueItem.Equals(Model.FromYear.ToString()));
            ToYearSelected = Years.FirstOrDefault(x => x.ValueItem.Equals(Model.ToYear.ToString()));
        }

        private void LoadDonVi()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            _donViItems = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);

            OnPropertyChanged(nameof(DonViItems));

            foreach (var org in _donViItems)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountDonvi));
                    OnPropertyChanged(nameof(SelectedAllDonvi));
                    LoadData();
                };
            }
        }

        private void LoadData()
        {
            try
            {
                var lstDonviSelected = DonViItems.Where(x => x.Selected).Select(y => y.MaDonVi).ToList();
                var predicate = PredicateBuilder.True<TlDmCanBo>();
                predicate = predicate.And(x => x.Thang == int.Parse(FromMonthSelected.ValueItem));
                predicate = predicate.And(x => lstDonviSelected.Contains(x.Parent));
                predicate = predicate.And(x => x.Nam == int.Parse(FromYearSelected.ValueItem));
                predicate = predicate.And(x => x.IsDelete == true);
                var data = _cadresService.FindByCondition(predicate).OrderBy(x => x.TenCanBo.Split(" ").Last());
                CadresItems = _mapper.Map<ObservableCollection<CadresModel>>(data);
                foreach (var org in CadresItems)
                {
                    org.PropertyChanged += (sender, args) =>
                    {
                        OnPropertyChanged(nameof(LabelSelectedCountCadres));
                        OnPropertyChanged(nameof(SelectedAllCadres));
                    };
                }
                OnPropertyChanged(nameof(LabelSelectedCountCadres));
                OnPropertyChanged(nameof(SelectedAllCadres));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave()
        {
            base.OnSave();
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lstCanBoNghiHuuKeHoachNam = _tlDsCBNHKeHoachService.FindByYear(int.Parse(ToYearSelected.ValueItem)).ToList();


                if (lstCanBoNghiHuuKeHoachNam != null && lstCanBoNghiHuuKeHoachNam.Count() > 0)
                {
                    DialogResult dialog = MessageBox.Show(string.Format("Năm {0} đã có dữ liệu, đồng chí có muốn tiếp tục?", ToYearSelected.ValueItem), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        CopyCanBo();
                    }
                }
                else
                {
                    CopyCanBo();
                }
            }
        }

        private void CopyCanBo()
        {
            try
            {
                _ = BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    var listDonviSelected = DonViItems.Where(x => x.Selected).ToList();
                    var listCapBacCanBo = _tlDmCapBacService.FindAll();
                    var listCBNHKeHoach = _tlDsCBNHKeHoachService.FindByYear(int.Parse(ToYearSelected.ValueItem)).ToList();

                    RemoveCadresExisted();
                    foreach (var donvi in listDonviSelected)
                    {
                        var lstCopy = CadresItems.Where(x => x.IsSelected && x.Parent == donvi.MaDonVi);
                        int year = int.Parse(ToYearSelected.ValueItem);

                        var lstSave = new ObservableCollection<TlDsCBNHKeHoachModel>();

                        var lstCopyMaCanBo = lstCopy.Select(n => n.MaCanBo);
                        foreach (var item in lstCopy)
                        {
                            for (int i = 1; i <= 12; i++)
                            {
                                int iMonth = i;
                                string sMonth = i < 10 ? ("0" + i.ToString()) : i.ToString();
                                DateTime dateTimeKeHoach = new DateTime(year, iMonth, DateTime.DaysInMonth(year, iMonth));
                                var cBNHKeHoachModel = _mapper.Map<TlDsCBNHKeHoachModel>(item);

                                cBNHKeHoachModel.MaCanBo = string.Format("{0}{1}{2}", year, sMonth, item.MaHieuCanBo);

                                var capBacCanBo = listCapBacCanBo.FirstOrDefault(x => x.MaCb == cBNHKeHoachModel.MaCb);

                                //Tính năm thâm niên
                                if (cBNHKeHoachModel.NgayNn != null)
                                {
                                    if (cBNHKeHoachModel.BKhongTinhNTN != null && cBNHKeHoachModel.BKhongTinhNTN.Value)
                                    {
                                        cBNHKeHoachModel.NamTn = (int)(cBNHKeHoachModel.ThangTnn ?? 0) / 12;
                                    }
                                    else
                                    {
                                        cBNHKeHoachModel.NamTn = TinhNamThamNien(cBNHKeHoachModel.NgayNn, cBNHKeHoachModel.NgayXn, cBNHKeHoachModel.NgayTn, (int)(cBNHKeHoachModel.ThangTnn ?? 0), dateTimeKeHoach);
                                    }
                                }

                                if (capBacCanBo != null)
                                {
                                    if (capBacCanBo.Parent == "4")
                                    {
                                        var ngayNn = (DateTime)cBNHKeHoachModel.NgayNn;
                                        var ngayXn = ngayNn.AddMonths(24);

                                        if (ngayXn.Year < dateTimeKeHoach.Year)
                                        {
                                            continue;
                                        }
                                        else if (ngayXn.Year == dateTimeKeHoach.Year)
                                        {
                                            if (ngayXn.Month == dateTimeKeHoach.Month)
                                            {
                                                cBNHKeHoachModel.IsDelete = false;
                                                string loai = cBNHKeHoachModel.Loai == null ? string.Empty : cBNHKeHoachModel.Loai;
                                                loai = string.Format("{0}{1};", loai, LoaiCanBoKehoach.RAQUAN_XUATNGU);
                                                cBNHKeHoachModel.Loai = loai;
                                                var cbKeHoach2 = lstCapBacKeHoach.FirstOrDefault(x => x.MaCb.Equals(cBNHKeHoachModel.MaCb));
                                                var capBacRaQuan = listCapBacCanBo.FirstOrDefault(x => x.MaCb.Equals(cBNHKeHoachModel.MaCb));
                                                cBNHKeHoachModel.NgayXn = ngayXn;
                                            }
                                            if (ngayXn.Month < dateTimeKeHoach.Month)
                                            {
                                                continue;
                                            }
                                        }
                                    }

                                    if (cBNHKeHoachModel.NgaySinh != null)
                                    {
                                        //Nghỉ hưu - SQ/QNCN
                                        if (capBacCanBo.Parent == "1" || capBacCanBo.Parent == "2" || capBacCanBo.Parent == "3")
                                        {
                                            var cbKeHoach1 = lstCapBacKeHoach.FirstOrDefault(x => x.MaCb.Equals(cBNHKeHoachModel.MaCb));
                                            if (cbKeHoach1 != null)
                                            {
                                                DateTime ngaySinh = (DateTime)cBNHKeHoachModel.NgaySinh;
                                                if (cBNHKeHoachModel.IsNam == true)
                                                {
                                                    var ngayXn = ngaySinh.AddYears((int)(cbKeHoach1.TuoiHuuNam + 1)).AddMonths(1);
                                                    string loai = string.Empty;
                                                    if (ngayXn.Year < dateTimeKeHoach.Year)
                                                    {
                                                        continue;
                                                    }
                                                    else if (ngayXn.Year == dateTimeKeHoach.Year)
                                                    {
                                                        if (ngayXn.Month == dateTimeKeHoach.Month)
                                                        {
                                                            cBNHKeHoachModel.IsDelete = false;
                                                            loai = string.Format("{0}{1}", loai, LoaiCanBoKehoach.NGHIHUU);
                                                            cBNHKeHoachModel.Loai = loai;
                                                            cBNHKeHoachModel.NgayXn = ngayXn;
                                                        }
                                                        if (ngayXn.Month < dateTimeKeHoach.Month)
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                }
                                                if (cBNHKeHoachModel.IsNam == false)
                                                {
                                                    var ngayXn = ngaySinh.AddYears((int)(cbKeHoach1.TuoiHuuNu + 1)).AddMonths(1);
                                                    string loai = string.Empty;
                                                    if (ngayXn.Year < dateTimeKeHoach.Year)
                                                    {
                                                        continue;
                                                    }
                                                    else if (ngayXn.Year == dateTimeKeHoach.Year)
                                                    {
                                                        if (ngayXn.Month == dateTimeKeHoach.Month)
                                                        {
                                                            cBNHKeHoachModel.IsDelete = false;
                                                            loai = string.Format("{0}{1}", loai, LoaiCanBoKehoach.NGHIHUU);
                                                            cBNHKeHoachModel.Loai = loai;
                                                            cBNHKeHoachModel.NgayXn = ngayXn;

                                                        }
                                                        if (ngayXn.Month < dateTimeKeHoach.Month)
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        cBNHKeHoachModel.Thang = i;
                                        cBNHKeHoachModel.Nam = year;
                                        cBNHKeHoachModel.Id = Guid.NewGuid();

                                        var canBoNghiHuuKH = listCBNHKeHoach.Find(x => cBNHKeHoachModel.MaCanBo == x.MaCanBo);
                                        if (canBoNghiHuuKH != null)
                                        {
                                            cBNHKeHoachModel.Id = canBoNghiHuuKH.Id;
                                            _tlDsCBNHKeHoachService.Update(_mapper.Map<TlDsCBNHKeHoach>(cBNHKeHoachModel));
                                        }
                                        else
                                        {
                                            _tlDsCBNHKeHoachService.Add(_mapper.Map<TlDsCBNHKeHoach>(cBNHKeHoachModel));
                                        }
                                        lstSave.Add(cBNHKeHoachModel);
                                    }
                                }
                            }
                        }
                    }
                }, (s, e) =>
                {
                    IsLoading = false;
                    if (e.Error == null)
                    {
                        MessageBox.Show(string.Format("Lấy căn cứ đối tượng sang năm {0} thành công!", ToYearSelected.ValueItem), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogHost.Close("RootDialog");
                        SavedAction?.Invoke(Model);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void RemoveCadresExisted()
        {
            int year = int.Parse(ToYearSelected.ValueItem);
            List<CadresModel> listDoiTuong = CadresItems.Where(x => x.IsSelected).ToList();
            List<string> lstMaCB = new List<string>();

            foreach (var dt in listDoiTuong)
            {
                for (int i = 1; i <= 12; i++)
                {
                    string sMonth = i < 10 ? ("0" + i.ToString()) : i.ToString();
                    var maCanBo = string.Format("{0}{1}{2}", year, sMonth, dt.MaHieuCanBo);
                    lstMaCB.Add(maCanBo);
                }
            }
        }
        private int TinhNamThamNien(DateTime? ngayNn, DateTime? ngayXn, DateTime? ngayTn, int thangTnn, DateTime dateTimeKeHoach)
        {
            return DateUtils.TinhNamThamNien(ngayNn, ngayXn, ngayTn, thangTnn, dateTimeKeHoach.Month, dateTimeKeHoach.Year);
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
            if (DonViItems.Where(x => x.Selected).ToList().Count == 0)
            {
                messages.Add(string.Format(Resources.UnitNull));
                goto End;
            }
            if (ToYearSelected == null)
            {
                messages.Add("Hãy chọn năm để sao chép đến.");
                goto End;
            }
            if (int.Parse(ToYearSelected.ValueItem) < int.Parse(FromYearSelected.ValueItem))
            {
                messages.Add("Năm sao chép đến phải lớn hơn năm được chọn.");
                goto End;
            }

        End:
            return string.Join(Environment.NewLine, messages);
        }

        public void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
