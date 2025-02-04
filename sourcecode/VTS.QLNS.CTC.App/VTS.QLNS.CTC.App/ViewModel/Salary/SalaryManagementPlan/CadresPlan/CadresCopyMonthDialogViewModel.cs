using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.CadresPlan
{
    public class CadresCopyMonthDialogViewModel : DialogViewModelBase<TlSaoChepNamKeHoachModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDmCanBoKeHoachService _tlDmCanBoKeHoachService;
        private readonly ITlDmCapBacKeHoachService _tlDmCapBacKeHoachService;
        private readonly ITlPhuCapDieuChinhService _tlPhuCapDieuChinhService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly ITlCanBoPhuCapKeHoachService _tlCanBoPhuCapKeHoachService;
        private readonly ITlDmCapBacService _tlDmCapBacService;
        private readonly ITlDieuChinhQsKeHoachService _tlDieuChinhQsKeHoachService;
        private readonly ITlQsKeHoachChiTietService _tlQsKeHoachChiTietService;
        private readonly ITlBangLuongKeHoachService _tlBangLuongKeHoachService;
        private readonly ITlDsBangLuongKeHoachService _tlDsBangLuongKeHoachService;
        private readonly ITlQtChungTuChiTietKeHoachService _tlQtChungTuChiTietKeHoachService;
        private readonly ITlDmHslKeHoachService _tlDmHslKeHoachService;
        private readonly ITlDmNangLuongService _tlDmNangLuongService;
        private readonly ITlDmPhuCapService _tlDmPhuCapService;

        public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH_DANH_SACH_DOI_TUONG_HUONG_LUONG_KE_HOACH_COPY;

        public override string Title => "Sao chép nhanh đối tượng";
        public override string Description => "Sao chép nhanh đối tượng theo tháng";
        public override Type ContentType => typeof(View.Salary.SalaryManagementPlan.CadresPlan.CadresCopyMonthDialog);

        private List<TlDmCapBacKeHoach> lstCapBacKeHoach;
        private List<TlPhuCapDieuChinhQuery> lstPhuCapDieuChinh;
        private List<TlDmCanBoKeHoach> lstCanBoKeHoachNam;
        private List<TlDmHslKeHoach> lstHslKeHoach;
        private List<TlDmNangLuong> lstNangLuong;

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

        public CadresCopyMonthDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmCanBoService cadresService,
            ITlDmDonViService tlDmDonViService,
            ITlDmCanBoKeHoachService tlDmCanBoKeHoachService,
            ITlDmCapBacKeHoachService tlDmCapBacKeHoachService,
            ITlPhuCapDieuChinhService tlPhuCapDieuChinhService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            ITlCanBoPhuCapKeHoachService tlCanBoPhuCapKeHoachService,
            ITlDmCapBacService tlDmCapBacService,
            ITlDieuChinhQsKeHoachService tlDieuChinhQsKeHoachService,
            ITlQsKeHoachChiTietService tlQsKeHoachChiTietService,
            ITlDsBangLuongKeHoachService tlDsBangLuongKeHoachService,
            ITlQtChungTuChiTietKeHoachService tlQtChungTuChiTietKeHoachService,
            ITlBangLuongKeHoachService tlBangLuongKeHoachService,
            ITlDmHslKeHoachService tlDmHslKeHoachService,
            ITlDmNangLuongService tlDmNangLuongService,
            ITlDmPhuCapService tlDmPhuCapService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;

            _cadresService = cadresService;
            _tlDmDonViService = tlDmDonViService;
            _tlDmCanBoKeHoachService = tlDmCanBoKeHoachService;
            _tlDmCapBacKeHoachService = tlDmCapBacKeHoachService;
            _tlPhuCapDieuChinhService = tlPhuCapDieuChinhService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _tlCanBoPhuCapKeHoachService = tlCanBoPhuCapKeHoachService;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDieuChinhQsKeHoachService = tlDieuChinhQsKeHoachService;
            _tlQsKeHoachChiTietService = tlQsKeHoachChiTietService;
            _tlDsBangLuongKeHoachService = tlDsBangLuongKeHoachService;
            _tlQtChungTuChiTietKeHoachService = tlQtChungTuChiTietKeHoachService;
            _tlBangLuongKeHoachService = tlBangLuongKeHoachService;
            _tlDmHslKeHoachService = tlDmHslKeHoachService;
            _tlDmNangLuongService = tlDmNangLuongService;
            _tlDmPhuCapService = tlDmPhuCapService;
        }

        public override void Init()
        {
            base.Init();
            lstCapBacKeHoach = _tlDmCapBacKeHoachService.FindAll().ToList();
            lstPhuCapDieuChinh = _tlPhuCapDieuChinhService.FindAllPhuCapDieuChinh().ToList();
            lstHslKeHoach = _tlDmHslKeHoachService.FindAll().OrderBy(x => x.MaCb).ToList();
            lstNangLuong = _tlDmNangLuongService.FindAll().ToList();
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
            //if (Model.DonVi != null)
            //{
            //    SelectedDonViItems = _donViItems.FirstOrDefault(x => Model.DonVi.MaDonVi.Equals(x.MaDonVi))
            //}

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
                lstCanBoKeHoachNam = _tlDmCanBoKeHoachService.FindByYear(int.Parse(ToYearSelected.ValueItem)).ToList();
                if (lstCanBoKeHoachNam != null && lstCanBoKeHoachNam.Count() > 0)
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
                    var listPhuCap = _tlDmPhuCapService.FindAll();
                    var listCapBacCanBo = _tlDmCapBacService.FindAll();
                    var listCanBoKeHoach = _tlDmCanBoKeHoachService.FindByYear(int.Parse(ToYearSelected.ValueItem)).ToList();
                    _tlQsKeHoachChiTietService.DeleteByNam(int.Parse(ToYearSelected.ValueItem));
                    _tlDieuChinhQsKeHoachService.DeleteByNam(int.Parse(ToYearSelected.ValueItem));
                    RemoveCadresExisted();
                    //_tlCanBoPhuCapKeHoachService.DeleteByYear(int.Parse(ToYearSelected.ValueItem));
                    foreach (var donvi in listDonviSelected)
                    {
                        //_tlDmCanBoKeHoachService.DeleteByYear(int.Parse(ToYearSelected.ValueItem));
                        //foreach (var item in lstCanBoKeHoachNam)
                        //{
                        //    _tlCanBoPhuCapKeHoachService.DeleteByMaCanBo(item.MaCanBo);
                        //    _tlDmCanBoKeHoachService.Delete(item.Id);
                        //}
                        _tlQtChungTuChiTietKeHoachService.DeleteByNamAndMaDonVi(donvi.MaDonVi, int.Parse(ToYearSelected.ValueItem));
                        var bangLuong = _tlDsBangLuongKeHoachService.FindByCondition(CachTinhLuong.CACH0, donvi.MaDonVi, int.Parse(ToYearSelected.ValueItem));
                        if (bangLuong != null)
                        {
                            _tlBangLuongKeHoachService.DeleteByParentId(bangLuong.Id);
                            _tlDsBangLuongKeHoachService.Delete(bangLuong.Id);
                        }

                        var lstCopy = CadresItems.Where(x => x.IsSelected && x.Parent == donvi.MaDonVi);
                        int year = int.Parse(ToYearSelected.ValueItem);

                        var lstSave = new ObservableCollection<TlDmCanBoKeHoachModel>();
                        var lstQsKeHoachChiTiet = new List<TlQsKeHoachChiTiet>();
                        var lstTlDieuChinhKeHoach = new List<TlDieuChinhQsKeHoach>();


                        for (int i = 1; i <= 12; i++)
                        {
                            TlQsKeHoachChiTiet tlQsKeHoachChiTiet = new TlQsKeHoachChiTiet();
                            tlQsKeHoachChiTiet.Thang = i;
                            tlQsKeHoachChiTiet.Nam = year;
                            tlQsKeHoachChiTiet.MaDonVi = donvi.MaDonVi;
                            tlQsKeHoachChiTiet.TenDonVi = donvi.TenDonVi;
                            tlQsKeHoachChiTiet.SNguoiTao = _sessionService.Current.Principal;
                            tlQsKeHoachChiTiet.DNgayTao = DateTime.Now;
                            TlDieuChinhQsKeHoach tlDieuChinhQsKeHoach = new TlDieuChinhQsKeHoach();
                            tlDieuChinhQsKeHoach.Thang = i;
                            tlDieuChinhQsKeHoach.Nam = year;
                            tlDieuChinhQsKeHoach.MaDonVi = donvi.MaDonVi;
                            tlDieuChinhQsKeHoach.TenDonVi = donvi.TenDonVi;
                            tlDieuChinhQsKeHoach.SNguoiTao = _sessionService.Current.Principal;
                            tlDieuChinhQsKeHoach.DNgayTao = DateTime.Now;
                            lstQsKeHoachChiTiet.Add(tlQsKeHoachChiTiet);
                            lstTlDieuChinhKeHoach.Add(tlDieuChinhQsKeHoach);
                        }

                        var lstCopyMaCanBo = lstCopy.Select(n => n.MaCanBo);
                        var listAllCanBoPhuCap = _tlCanBoPhuCapService.FindAll(x => lstCopyMaCanBo.Contains(x.MaCbo));
                        foreach (var item in lstCopy)
                        {
                            var lstCanBoPhuCap = listAllCanBoPhuCap.Where(x => x.MaCbo.Equals(item.MaCanBo));
                            for (int i = 1; i <= 12; i++)
                            {
                                int iMonth = i;
                                string sMonth = i < 10 ? ("0" + i.ToString()) : i.ToString();
                                DateTime dateTimeKeHoach = new DateTime(year, iMonth, DateTime.DaysInMonth(year, iMonth));
                                var canBoKeHoachModel = _mapper.Map<TlDmCanBoKeHoachModel>(item);
                                canBoKeHoachModel.MaCanBo = string.Format("{0}{1}{2}", year, sMonth, item.MaHieuCanBo);

                                //_tlCanBoPhuCapKeHoachService.DeleteByMaCanBo(canBoKeHoachModel.MaCanBo);
                                List<TlCanBoPhuCapKeHoachModel> lstCanBoPhuCapKeHoach = new List<TlCanBoPhuCapKeHoachModel>();

                                foreach (var item2 in lstCanBoPhuCap)
                                {
                                    TlCanBoPhuCapKeHoachModel tlCanBoPhuCapKeHoachModel = new TlCanBoPhuCapKeHoachModel();
                                    tlCanBoPhuCapKeHoachModel.MaCanBo = canBoKeHoachModel.MaCanBo;
                                    tlCanBoPhuCapKeHoachModel.MaPhuCap = item2.MaPhuCap;
                                    tlCanBoPhuCapKeHoachModel.GiaTri = item2.GiaTri;
                                    tlCanBoPhuCapKeHoachModel.HuongPcSn = (int?)item2.HuongPcSn;
                                    tlCanBoPhuCapKeHoachModel.DateEnd = item2.DateEnd;
                                    tlCanBoPhuCapKeHoachModel.DateStart = item2.DateStart;

                                    //var phuCap = _tlDmPhuCapService.FindByMaPhuCap(item2.MaPhuCap);
                                    var phuCap = listPhuCap.FirstOrDefault(x => x.MaPhuCap == item2.MaPhuCap);

                                    var monthDiff = 12 * (dateTimeKeHoach.Year - int.Parse(FromYearSelected.ValueItem)) + dateTimeKeHoach.Month - i;
                                    var thangHuongMoi = item2.ISoThangHuong == null ? monthDiff : item2.ISoThangHuong + monthDiff;
                                    if (item2 != null && item2.ISoThangHuong != null && phuCap != null && phuCap.IthangToiDa != null)
                                    {
                                        if (thangHuongMoi <= phuCap.IthangToiDa)
                                        {
                                            tlCanBoPhuCapKeHoachModel.ISoThangHuong = thangHuongMoi;
                                        }
                                        else if (thangHuongMoi > phuCap.IthangToiDa)
                                        {
                                            tlCanBoPhuCapKeHoachModel.GiaTri = 0;
                                        }
                                    }
                                    else if (item2 != null && item2.ISoThangHuong != null && phuCap.IthangToiDa == null)
                                    {
                                        tlCanBoPhuCapKeHoachModel.ISoThangHuong = thangHuongMoi;
                                    }

                                    lstCanBoPhuCapKeHoach.Add(tlCanBoPhuCapKeHoachModel);
                                }

                                var phuCapLht = lstCanBoPhuCapKeHoach.FirstOrDefault(x => x.MaPhuCap == PhuCap.LHT_HS);

                                var lhtHeSoTruoc = phuCapLht.GiaTri;

                                var capBacCanBo = listCapBacCanBo.FirstOrDefault(x => x.MaCb == canBoKeHoachModel.MaCb);
                                if (capBacCanBo == null)
                                {
                                    goto KhongTinhCapBacKeHoach;
                                }

                                if (canBoKeHoachModel.NgayNn != null)
                                {
                                    if (canBoKeHoachModel.BKhongTinhNTN != null && canBoKeHoachModel.BKhongTinhNTN.Value)
                                    {
                                        canBoKeHoachModel.NamTn = (int)(canBoKeHoachModel.ThangTnn ?? 0) / 12;
                                    }
                                    else
                                    {
                                        canBoKeHoachModel.NamTn = TinhNamThamNien(canBoKeHoachModel.NgayNn, canBoKeHoachModel.NgayXn, canBoKeHoachModel.NgayTn, (int)(canBoKeHoachModel.ThangTnn ?? 0), dateTimeKeHoach);
                                    }
                                }

                                if (canBoKeHoachModel.NgayNhanCb != null && canBoKeHoachModel.HsLuongKeHoach != null && canBoKeHoachModel.CbKeHoach != null)
                                {
                                    var ngayNhanCb = (DateTime)canBoKeHoachModel.NgayNhanCb;

                                    if (canBoKeHoachModel.ThoiHanTangCb == null)
                                    {
                                        goto TinhNghiHuu;
                                    }

                                    if (ngayNhanCb.AddMonths((int)canBoKeHoachModel.ThoiHanTangCb) <= dateTimeKeHoach)
                                    {
                                        if (canBoKeHoachModel.MaCb.StartsWith("1"))
                                        {
                                            var hslKeHoach = lstHslKeHoach.FirstOrDefault(x => x.Id.ToString().Equals(canBoKeHoachModel.CbKeHoach));
                                            var hslTran = lstHslKeHoach.FirstOrDefault(x => x.Id.Equals(canBoKeHoachModel.IdLuongTran));

                                            if (hslTran == null || (lstHslKeHoach.IndexOf(hslKeHoach) <= lstHslKeHoach.IndexOf(hslTran)))
                                            {
                                                canBoKeHoachModel.MaCb = hslKeHoach.MaCb;
                                                //canBoKeHoachModel.HeSoLuong = hslKeHoach.LhtHsKh;
                                                if (phuCapLht != null)
                                                {
                                                    phuCapLht.GiaTri = hslKeHoach.LhtHsKh;
                                                }
                                                canBoKeHoachModel.NgayNhanCb = ngayNhanCb.AddMonths((int)canBoKeHoachModel.ThoiHanTangCb);
                                            }
                                            else if (lstHslKeHoach.IndexOf(hslKeHoach) > lstHslKeHoach.IndexOf(hslTran))
                                            {
                                                var nangLuong = lstNangLuong.FirstOrDefault(x => x.MaCbHt.Equals(hslTran.MaCb));
                                                if (nangLuong != null)
                                                {
                                                    canBoKeHoachModel.MaCb = nangLuong.MaCbKh;
                                                    //canBoKeHoachModel.HeSoLuong = nangLuong.LhtHsKh;
                                                    if (phuCapLht != null)
                                                    {
                                                        phuCapLht.GiaTri = nangLuong.LhtHsKh;
                                                    }
                                                    canBoKeHoachModel.NgayNhanCb = ngayNhanCb.AddMonths((int)nangLuong.ThoiHanTang);
                                                }
                                            }
                                        }
                                        else if (canBoKeHoachModel.MaCb.StartsWith("0"))
                                        {
                                            var cbHienTai = lstCapBacKeHoach.FirstOrDefault(x => x.MaCb.Equals(canBoKeHoachModel.MaCb));
                                            var cbKeHoach = FindCapBacKeHoach(cbHienTai.Id.ToString(), (DateTime)canBoKeHoachModel.NgayNhanCb, dateTimeKeHoach);
                                            if (cbKeHoach != null)
                                            {
                                                var hslKeHoach = lstHslKeHoach.FirstOrDefault(x => x.Id == cbKeHoach.IdHslKeHoach);
                                                if (hslKeHoach != null)
                                                {
                                                    canBoKeHoachModel.MaCb = cbKeHoach.MaCb;
                                                    //canBoKeHoachModel.HeSoLuong = cbKeHoach.LhtHs;
                                                    if (phuCapLht != null)
                                                    {
                                                        phuCapLht.GiaTri = cbKeHoach.LhtHs;
                                                    }
                                                    canBoKeHoachModel.NgayNhanCb = cbKeHoach.NgayNhanQh;
                                                }
                                            }
                                        }
                                        else if (canBoKeHoachModel.MaCb.StartsWith("2") || canBoKeHoachModel.MaCb.StartsWith("3"))
                                        {
                                            var hslKeHoach = lstHslKeHoach.FirstOrDefault(x => x.Id.ToString().Equals(canBoKeHoachModel.CbKeHoach));
                                            var hslTran = lstHslKeHoach.FirstOrDefault(x => x.Id.Equals(canBoKeHoachModel.IdLuongTran));
                                            if (lstHslKeHoach.IndexOf(hslKeHoach) <= lstHslKeHoach.IndexOf(hslTran))
                                            {
                                                canBoKeHoachModel.MaCb = hslKeHoach.MaCb;
                                                //canBoKeHoachModel.HeSoLuong = hslKeHoach.LhtHsKh;
                                                if (phuCapLht != null)
                                                {
                                                    phuCapLht.GiaTri = hslKeHoach.LhtHsKh;
                                                }
                                                canBoKeHoachModel.NgayNhanCb = ngayNhanCb.AddMonths((int)canBoKeHoachModel.ThoiHanTangCb);
                                            }
                                            else if (lstHslKeHoach.IndexOf(hslKeHoach) > lstHslKeHoach.IndexOf(hslTran))
                                            {
                                                var cbKeHoach = lstCapBacKeHoach.FirstOrDefault(x => x.IdHslKeHoach == hslKeHoach.Id && x.IdHslTran == hslTran.Id && x.Nhom.Equals(canBoKeHoachModel.Nhom));
                                                canBoKeHoachModel.MaCb = hslTran.MaCb;
                                                //canBoKeHoachModel.HeSoLuong = hslTran.LhtHsKh;
                                                if (phuCapLht != null)
                                                {
                                                    phuCapLht.GiaTri = hslTran.LhtHsKh;
                                                }
                                                canBoKeHoachModel.NgayNhanCb = ngayNhanCb.AddMonths((int)canBoKeHoachModel.ThoiHanTangCb);
                                                var pctnvk = lstCanBoPhuCapKeHoach.FirstOrDefault(x => PhuCap.PCTNVK_HS.Equals(x.MaPhuCap));
                                                if (pctnvk != null)
                                                {
                                                    var hsVk = cbKeHoach == null ? 0 : cbKeHoach.HsVk;
                                                    pctnvk.GiaTri += hsVk;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (canBoKeHoachModel.NgayNhanCb != null && canBoKeHoachModel.CbKeHoach == null)
                                {
                                    var ngayNhanCb = (DateTime)canBoKeHoachModel.NgayNhanCb;
                                    var nangLuong = lstNangLuong.FirstOrDefault(x => x.MaCbHt.Equals(canBoKeHoachModel.MaCb));
                                    if (ngayNhanCb.AddMonths((int)canBoKeHoachModel.ThoiHanTangCb) <= dateTimeKeHoach)
                                    {
                                        if (nangLuong != null)
                                        {
                                            canBoKeHoachModel.MaCb = nangLuong.MaCbKh;
                                            //canBoKeHoachModel.HeSoLuong = nangLuong.LhtHsKh;
                                            if (phuCapLht != null)
                                            {
                                                phuCapLht.GiaTri = nangLuong.LhtHsKh;
                                            }
                                            canBoKeHoachModel.NgayNhanCb = ngayNhanCb.AddMonths((int)nangLuong.ThoiHanTang);
                                        }
                                    }
                                }

                            TinhNghiHuu:
                                if (capBacCanBo.Parent == "4")
                                {
                                    var ngayNn = (DateTime)canBoKeHoachModel.NgayNn;
                                    var ngayXn = ngayNn.AddMonths(24);

                                    if (ngayXn.Year < dateTimeKeHoach.Year)
                                    {
                                        continue;
                                    }
                                    else if (ngayXn.Year == dateTimeKeHoach.Year)
                                    {
                                        if (ngayXn.Month == dateTimeKeHoach.Month)
                                        {
                                            canBoKeHoachModel.IsDelete = false;
                                            string loai = canBoKeHoachModel.Loai == null ? string.Empty : canBoKeHoachModel.Loai;
                                            loai = string.Format("{0}{1};", loai, LoaiCanBoKehoach.RAQUAN_XUATNGU);
                                            canBoKeHoachModel.Loai = loai;
                                            var tlQsKeHoachChiTiet = lstQsKeHoachChiTiet.FirstOrDefault(x => x.Thang == i);
                                            var cbKeHoach2 = lstCapBacKeHoach.FirstOrDefault(x => x.MaCb.Equals(canBoKeHoachModel.MaCb));
                                            var capBacRaQuan = listCapBacCanBo.FirstOrDefault(x => x.MaCb.Equals(canBoKeHoachModel.MaCb));
                                            canBoKeHoachModel.NgayXn = ngayXn;
                                            switch (canBoKeHoachModel.MaCb)
                                            {
                                                case "01":
                                                    if (tlQsKeHoachChiTiet.FSoBinhNhi == null)
                                                    {
                                                        tlQsKeHoachChiTiet.FSoBinhNhi = 1;
                                                        tlQsKeHoachChiTiet.FPcrqBinhNhi = capBacRaQuan.PhuCapRaQuan;
                                                    }
                                                    else
                                                    {
                                                        tlQsKeHoachChiTiet.FSoBinhNhi++;
                                                        tlQsKeHoachChiTiet.FPcrqBinhNhi = tlQsKeHoachChiTiet.FPcrqBinhNhi + capBacRaQuan.PhuCapRaQuan;
                                                    }
                                                    break;
                                                case "02":
                                                    if (tlQsKeHoachChiTiet.FSoBinhNhat == null)
                                                    {
                                                        tlQsKeHoachChiTiet.FSoBinhNhat = 1;
                                                        tlQsKeHoachChiTiet.FPcrqBinhNhat = capBacRaQuan.PhuCapRaQuan;
                                                    }
                                                    else
                                                    {
                                                        tlQsKeHoachChiTiet.FSoBinhNhat++;
                                                        tlQsKeHoachChiTiet.FPcrqBinhNhat = tlQsKeHoachChiTiet.FPcrqBinhNhat + capBacRaQuan.PhuCapRaQuan;
                                                    }
                                                    break;
                                                case "03":
                                                    if (tlQsKeHoachChiTiet.FSoHaSi == null)
                                                    {
                                                        tlQsKeHoachChiTiet.FSoHaSi = 1;
                                                        tlQsKeHoachChiTiet.FPcrqHaSi = capBacRaQuan.PhuCapRaQuan;
                                                    }
                                                    else
                                                    {
                                                        tlQsKeHoachChiTiet.FSoHaSi++;
                                                        tlQsKeHoachChiTiet.FPcrqHaSi = tlQsKeHoachChiTiet.FPcrqHaSi + capBacRaQuan.PhuCapRaQuan;
                                                    }
                                                    break;
                                                case "04":
                                                    if (tlQsKeHoachChiTiet.FSoTrungSi == null)
                                                    {
                                                        tlQsKeHoachChiTiet.FSoTrungSi = 1;
                                                        tlQsKeHoachChiTiet.FPcrqTrungSi = capBacRaQuan.PhuCapRaQuan;
                                                    }
                                                    else
                                                    {
                                                        tlQsKeHoachChiTiet.FSoTrungSi++;
                                                        tlQsKeHoachChiTiet.FPcrqTrungSi = tlQsKeHoachChiTiet.FPcrqTrungSi + capBacRaQuan.PhuCapRaQuan;
                                                    }
                                                    break;
                                                case "05":
                                                    if (tlQsKeHoachChiTiet.FSoThuongSi == null)
                                                    {
                                                        tlQsKeHoachChiTiet.FSoThuongSi = 1;
                                                        tlQsKeHoachChiTiet.FPcrqThuongSi = capBacRaQuan.PhuCapRaQuan;
                                                    }
                                                    else
                                                    {
                                                        tlQsKeHoachChiTiet.FSoThuongSi++;
                                                        tlQsKeHoachChiTiet.FPcrqThuongSi = tlQsKeHoachChiTiet.FPcrqThuongSi + capBacRaQuan.PhuCapRaQuan;
                                                    }
                                                    break;
                                            }
                                        }
                                        if (ngayXn.Month < dateTimeKeHoach.Month)
                                        {
                                            continue;
                                        }
                                    }
                                }

                                if (canBoKeHoachModel.NgaySinh == null)
                                {
                                    goto KhongTinhCapBacKeHoach;
                                }

                                if (capBacCanBo.Parent == "1" || capBacCanBo.Parent == "2" || capBacCanBo.Parent == "3")
                                {
                                    var cbKeHoach1 = lstCapBacKeHoach.FirstOrDefault(x => x.MaCb.Equals(canBoKeHoachModel.MaCb));
                                    if (cbKeHoach1 != null)
                                    {
                                        DateTime ngaySinh = (DateTime)canBoKeHoachModel.NgaySinh;
                                        var tlQsKeHoachChiTiet = lstQsKeHoachChiTiet.FirstOrDefault(x => x.Thang == i);
                                        if (canBoKeHoachModel.IsNam == true)
                                        {
                                            var ngayXn = ngaySinh.AddYears((int)(cbKeHoach1.TuoiHuuNam + 1)).AddMonths(1);
                                            string loai = canBoKeHoachModel.Loai == null ? string.Empty : canBoKeHoachModel.Loai;
                                            canBoKeHoachModel.Loai = loai;
                                            if (ngayXn.Year < dateTimeKeHoach.Year)
                                            {
                                                continue;
                                            }
                                            else if (ngayXn.Year == dateTimeKeHoach.Year)
                                            {
                                                canBoKeHoachModel.NgayXn = ngayXn;
                                                if (ngayXn.Month == dateTimeKeHoach.Month)
                                                {
                                                    canBoKeHoachModel.IsDelete = false;
                                                    loai = string.Format("{0}{1};", loai, LoaiCanBoKehoach.NGHIHUU);
                                                    if (canBoKeHoachModel.MaCb.StartsWith("111"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoThieuUy == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThieuUy = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThieuUy++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("112"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoTrungUy == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoTrungUy = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoTrungUy++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("113"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoThuongUy == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThuongUy = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThuongUy++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("114"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoDaiUy == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoDaiUy = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoDaiUy++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("121"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoThieuTa == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThieuTa = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThieuTa++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("122"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoTrungTa == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoTrungTa = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoTrungTa++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("123"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoThuongTa == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThuongTa = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThuongTa++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("124"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoDaiTa == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoDaiTa = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoDaiTa++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("13"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoTuong == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoTuong = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoTuong++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("2"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoQncn == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoQncn = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoQncn++;
                                                        }
                                                    }
                                                }
                                                if (ngayXn.Month < dateTimeKeHoach.Month)
                                                {
                                                    continue;
                                                }
                                            }
                                        }
                                        if (canBoKeHoachModel.IsNam == false)
                                        {
                                            var ngayXn = ngaySinh.AddYears((int)(cbKeHoach1.TuoiHuuNu + 1));
                                            canBoKeHoachModel.NgayXn = ngayXn;
                                            if (ngayXn.Year < dateTimeKeHoach.Year)
                                            {
                                                continue;
                                            }
                                            else if (ngayXn.Year == dateTimeKeHoach.Year)
                                            {
                                                if (ngayXn.Month == dateTimeKeHoach.Month)
                                                {
                                                    canBoKeHoachModel.IsDelete = false;
                                                    if (canBoKeHoachModel.MaCb.StartsWith("111"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoThieuUy == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThieuUy = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThieuUy++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("112"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoTrungUy == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoTrungUy = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoTrungUy++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("113"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoThuongUy == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThuongUy = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThuongUy++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("114"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoDaiUy == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoDaiUy = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoDaiUy++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("121"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoThieuTa == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThieuTa = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThieuTa++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("122"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoTrungTa == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoTrungTa = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoTrungTa++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("123"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoThuongTa == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThuongTa = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoThuongTa++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("124"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoDaiTa == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoDaiTa = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoDaiTa++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("13"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoTuong == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoTuong = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoTuong++;
                                                        }
                                                    }
                                                    else if (canBoKeHoachModel.MaCb.StartsWith("2"))
                                                    {
                                                        if (tlQsKeHoachChiTiet.FSoQncn == null)
                                                        {
                                                            tlQsKeHoachChiTiet.FSoQncn = 1;
                                                        }
                                                        else
                                                        {
                                                            tlQsKeHoachChiTiet.FSoQncn++;
                                                        }
                                                    }
                                                }
                                                if (ngayXn.Month < dateTimeKeHoach.Month)
                                                {
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }

                            KhongTinhCapBacKeHoach:
                                canBoKeHoachModel.Thang = i;
                                canBoKeHoachModel.Nam = year;
                                canBoKeHoachModel.Id = Guid.NewGuid();
                                var capBac = listCapBacCanBo.FirstOrDefault(x => x.MaCb == canBoKeHoachModel.MaCb);

                                var phuCapBhxhdvHs = lstCanBoPhuCapKeHoach.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHXHDV_HS);
                                if (phuCapBhxhdvHs != null && capBac != null)
                                {
                                    phuCapBhxhdvHs.GiaTri = capBac.BhxhCq;
                                }

                                var phuCapBhxhcnHs = lstCanBoPhuCapKeHoach.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHXHCN_HS);
                                if (phuCapBhxhcnHs != null && capBac != null)
                                {
                                    phuCapBhxhcnHs.GiaTri = capBac.HsBhxh;
                                }

                                var phuCapBhytdvHs = lstCanBoPhuCapKeHoach.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHYTDV_HS);
                                if (phuCapBhytdvHs != null && capBac != null)
                                {
                                    phuCapBhytdvHs.GiaTri = capBac.BhytCq;
                                }

                                var phuCapBhytcnHs = lstCanBoPhuCapKeHoach.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHYTCN_HS);
                                if (phuCapBhytcnHs != null && capBac != null)
                                {
                                    phuCapBhytcnHs.GiaTri = capBac.HsBhyt;
                                }

                                var phuCapBhtndvHs = lstCanBoPhuCapKeHoach.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHTNDV_HS);
                                if (phuCapBhtndvHs != null && capBac != null)
                                {
                                    phuCapBhtndvHs.GiaTri = capBac.BhtnCq;
                                }

                                var phuCapBhtncnHs = lstCanBoPhuCapKeHoach.FirstOrDefault(x => x.MaPhuCap == PhuCap.BHTNCN_HS);
                                if (phuCapBhtncnHs != null && capBac != null)
                                {
                                    phuCapBhtncnHs.GiaTri = capBac.HsBhtn;
                                }

                                var phuCapNtn = lstCanBoPhuCapKeHoach.FirstOrDefault(x => x.MaPhuCap == PhuCap.NTN);
                                if (phuCapNtn != null)
                                {
                                    phuCapNtn.GiaTri = canBoKeHoachModel.NamTn;
                                }

                                var cbKeHoachNext = lstCapBacKeHoach.FirstOrDefault(x => x.MaCb.Equals(canBoKeHoachModel.MaCb));
                                if (cbKeHoachNext != null)
                                {
                                    var hslKh = _tlDmHslKeHoachService.FindById(cbKeHoachNext.IdHslKeHoach == null ? Guid.Empty : (Guid)cbKeHoachNext.IdHslKeHoach);
                                    if (hslKh != null)
                                    {
                                        canBoKeHoachModel.ThoiHanTangCb = cbKeHoachNext.ThoiHanTang;
                                        canBoKeHoachModel.HsLuongKeHoach = hslKh.LhtHsKh;
                                        canBoKeHoachModel.HsLuongTran = hslKh.LhtHsKh;
                                        canBoKeHoachModel.CbKeHoach = cbKeHoachNext.Id.ToString();
                                    }
                                }

                                //var phuCapSnpt = lstCanBoPhuCapKeHoach.FirstOrDefault(x => x.MaPhuCap == PhuCap.GTPT_SN);
                                //if (phuCapSnpt != null)
                                //{
                                //    canBoKeHoachModel.SoNguoiPhuThuoc = phuCapSnpt.GiaTri == null ? null : phuCapSnpt.GiaTri;
                                //}

                                foreach (var item3 in lstPhuCapDieuChinh)
                                {
                                    var pcCanBoKeHoach = lstCanBoPhuCapKeHoach.FirstOrDefault(x => x.MaPhuCap.Equals(item3.MaPhuCap));
                                    if (pcCanBoKeHoach != null && dateTimeKeHoach > item3.ApDungTu)
                                    {
                                        pcCanBoKeHoach.GiaTri = item3.GiaTriMoi;
                                    }
                                }

                                if (canBoKeHoachModel.NamTn != item.NamTn)
                                {
                                    string loai = canBoKeHoachModel.Loai == null ? string.Empty : canBoKeHoachModel.Loai;
                                    loai = string.Format("{0}{1};", loai, LoaiCanBoKehoach.TANG_THAMNIEN);
                                    canBoKeHoachModel.Loai = loai;
                                    canBoKeHoachModel.IsModified = true;
                                }

                                if (i == 1 && phuCapLht.GiaTri != lhtHeSoTruoc)
                                {
                                    canBoKeHoachModel.IsModified = true;
                                    string loai = canBoKeHoachModel.Loai;
                                    loai = string.Format("{0}{1};", loai, LoaiCanBoKehoach.THAYDOIQH_NANGLUONG);
                                    canBoKeHoachModel.Loai = loai;
                                }
                                else if (i != 1 && phuCapLht.GiaTri != lhtHeSoTruoc)
                                {
                                    var canBoThangTruoc = _tlDmCanBoKeHoachService.FindByMaCanBo(string.Format("{0}{1}{2}", ToYearSelected.ValueItem, (i - 1).ToString("D2"), canBoKeHoachModel.MaHieuCanBo));
                                    var lhtHsThangTruoc = _tlCanBoPhuCapKeHoachService.FindByMaCanBo(canBoThangTruoc.MaCanBo).FirstOrDefault(x => PhuCap.LHT_HS.Equals(x.MaPhuCap));
                                    if (phuCapLht.GiaTri != lhtHsThangTruoc.GiaTri)
                                    {
                                        canBoKeHoachModel.IsModified = true;
                                        string loai = canBoKeHoachModel.Loai;
                                        loai = string.Format("{0}{1};", loai, LoaiCanBoKehoach.THAYDOIQH_NANGLUONG);
                                        canBoKeHoachModel.Loai = loai;
                                    }
                                }

                                canBoKeHoachModel.Loai = canBoKeHoachModel.Loai == null ? string.Empty : canBoKeHoachModel.Loai;

                                _tlCanBoPhuCapKeHoachService.BulkInsert(_mapper.Map<ObservableCollection<TlCanBoPhuCapKeHoach>>(lstCanBoPhuCapKeHoach));

                                var canboKH = listCanBoKeHoach.Find(x => canBoKeHoachModel.MaCanBo == x.MaCanBo);
                                if (canboKH != null)
                                {
                                    canBoKeHoachModel.Id = canboKH.Id;
                                    _tlDmCanBoKeHoachService.Update(_mapper.Map<TlDmCanBoKeHoach>(canBoKeHoachModel));
                                }
                                else
                                {
                                    _tlDmCanBoKeHoachService.Add(_mapper.Map<TlDmCanBoKeHoach>(canBoKeHoachModel));
                                }
                                lstSave.Add(canBoKeHoachModel);
                            }
                        }

                        foreach (var item in lstTlDieuChinhKeHoach)
                        {
                            var tlQsDieuChinhKeHoachChiTiet = lstQsKeHoachChiTiet.FirstOrDefault(x => x.Thang == item.Thang);

                            int soBinhNhi = tlQsDieuChinhKeHoachChiTiet.FSoBinhNhi == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoBinhNhi;
                            int soBinhNhat = tlQsDieuChinhKeHoachChiTiet.FSoBinhNhat == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoBinhNhat;
                            int soHaSi = tlQsDieuChinhKeHoachChiTiet.FSoHaSi == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoHaSi;
                            int soTrungSi = tlQsDieuChinhKeHoachChiTiet.FSoTrungSi == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoTrungSi;
                            int soThuongSi = tlQsDieuChinhKeHoachChiTiet.FSoThuongSi == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoThuongSi;
                            int soThieuUy = tlQsDieuChinhKeHoachChiTiet.FSoThieuUy == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoThieuUy;
                            int soTrungUy = tlQsDieuChinhKeHoachChiTiet.FSoTrungUy == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoTrungUy;
                            int soThuongUy = tlQsDieuChinhKeHoachChiTiet.FSoThuongUy == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoThuongUy;
                            int soDaiUy = tlQsDieuChinhKeHoachChiTiet.FSoDaiUy == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoDaiUy;
                            int soThieuTa = tlQsDieuChinhKeHoachChiTiet.FSoThieuTa == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoThieuTa;
                            int soTrungTa = tlQsDieuChinhKeHoachChiTiet.FSoTrungTa == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoTrungTa;
                            int soThuongTa = tlQsDieuChinhKeHoachChiTiet.FSoThuongTa == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoThuongTa;
                            int soDaiTa = tlQsDieuChinhKeHoachChiTiet.FSoDaiTa == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoDaiTa;
                            int soTuong = tlQsDieuChinhKeHoachChiTiet.FSoTuong == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoTuong;
                            int soQncn = tlQsDieuChinhKeHoachChiTiet.FSoQncn == null ? 0 : (int)tlQsDieuChinhKeHoachChiTiet.FSoQncn;

                            item.GiamXuatNgu = soBinhNhi + soBinhNhat + soHaSi + soTrungSi + soThuongSi;
                            item.GiamHuuTri = soThieuUy + soTrungUy + soThuongUy + soDaiUy
                                + soThieuTa + soTrungTa + soDaiTa + soThuongTa
                                + soTuong + soQncn;

                            double pcrqBinhNhi = tlQsDieuChinhKeHoachChiTiet.FPcrqBinhNhi == null ? 0 : (double)tlQsDieuChinhKeHoachChiTiet.FPcrqBinhNhi;
                            double pcrqBinhNhat = tlQsDieuChinhKeHoachChiTiet.FPcrqBinhNhat == null ? 0 : (double)tlQsDieuChinhKeHoachChiTiet.FPcrqBinhNhat;
                            double pcrqHaSi = tlQsDieuChinhKeHoachChiTiet.FPcrqHaSi == null ? 0 : (double)tlQsDieuChinhKeHoachChiTiet.FPcrqHaSi;
                            double pcrqTrungSi = tlQsDieuChinhKeHoachChiTiet.FPcrqTrungSi == null ? 0 : (double)tlQsDieuChinhKeHoachChiTiet.FPcrqTrungSi;
                            double pcrqThuongSi = tlQsDieuChinhKeHoachChiTiet.FPcrqThuongSi == null ? 0 : (double)tlQsDieuChinhKeHoachChiTiet.FPcrqThuongSi;

                            item.PhuCapXuatNgu = pcrqBinhNhi + pcrqBinhNhat + pcrqHaSi + pcrqTrungSi + pcrqThuongSi;
                        }

                        _tlDieuChinhQsKeHoachService.AddRange(lstTlDieuChinhKeHoach);
                        _tlQsKeHoachChiTietService.AddRange(lstQsKeHoachChiTiet);
                    }
                }, (s, e) =>
                {
                    IsLoading = false;
                    if (e.Error == null)
                    {
                        MessageBox.Show(string.Format("Sao chép thành công đối tượng sang năm {0}", ToYearSelected.ValueItem), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            _tlCanBoPhuCapKeHoachService.DeleteManyMaCanBo(string.Join(", ", lstMaCB));

        }
        private int TinhNamThamNien(DateTime? ngayNn, DateTime? ngayXn, DateTime? ngayTn, int thangTnn, DateTime dateTimeKeHoach)
        {
            return DateUtils.TinhNamThamNien(ngayNn, ngayXn, ngayTn, thangTnn, dateTimeKeHoach.Month, dateTimeKeHoach.Year);
        }

        private TlDmCapBacKeHoachModel FindCapBacKeHoach(string idCapBacKeHoach, DateTime ngayNhanQh, DateTime dateTimeKeHoach)
        {
            int monthDiff = ((dateTimeKeHoach.Year - ngayNhanQh.Year) * 12) + dateTimeKeHoach.Month - ngayNhanQh.Month;
            var cbKeHoach = lstCapBacKeHoach.FirstOrDefault(x => x.Id.ToString() == idCapBacKeHoach);
            if (cbKeHoach != null)
            {
                var cbKeHoachModel = _mapper.Map<TlDmCapBacKeHoachModel>(cbKeHoach);
                if (ngayNhanQh.AddMonths((int)cbKeHoach.ThoiHanTang) > dateTimeKeHoach)
                {
                    cbKeHoachModel.NgayNhanQh = ngayNhanQh;
                    return cbKeHoachModel;
                }
                else
                {
                    var hslKh = lstHslKeHoach.FirstOrDefault(x => x.Id == cbKeHoachModel.IdHslKeHoach);
                    if (hslKh != null)
                    {
                        var cbKeHoachNext = lstCapBacKeHoach.FirstOrDefault(x => x.MaCb == hslKh.MaCb);
                        return FindCapBacKeHoach(cbKeHoachNext.Id.ToString(), ngayNhanQh.AddMonths((int)cbKeHoach.ThoiHanTang), dateTimeKeHoach);
                    }
                    return cbKeHoachModel;
                }
            }
            else
            {
                return null;
            }
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
