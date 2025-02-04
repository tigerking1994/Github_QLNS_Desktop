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
using VTS.QLNS.CTC.Core.Service.Impl;
using Newtonsoft.Json;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewCadresPlan
{
    public class NewCadresCopyMonthDialogViewModel : DialogViewModelBase<TlSaoChepNamKeHoachModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmCanBoNq104Service _cadresService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlDmCanBoKeHoachNq104Service _tlDmCanBoKeHoachService;
        private readonly ITlDmCapBacKeHoachNq104Service _tlDmCapBacKeHoachService;
        private readonly ITlPhuCapDieuChinhNq104Service _tlPhuCapDieuChinhService;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private readonly ITlCanBoPhuCapKeHoachNq104Service _tlCanBoPhuCapKeHoachService;
        private readonly ITlDmCapBacNq104Service _tlDmCapBacService;
        private readonly ITlDieuChinhQsKeHoachNq104Service _tlDieuChinhQsKeHoachService;
        private readonly ITlQsKeHoachChiTietNq104Service _tlQsKeHoachChiTietService;
        private readonly ITlBangLuongKeHoachNq104Service _tlBangLuongKeHoachService;
        private readonly ITlDsBangLuongKeHoachNq104Service _tlDsBangLuongKeHoachService;
        private readonly ITlQtChungTuChiTietKeHoachNq104Service _tlQtChungTuChiTietKeHoachService;
        private readonly ITlDmPhuCapNq104Service _tlDmPhuCapService;
        private readonly ITlDmCapBacLuongNq104Service _tlDmCapBacLuongService;
        private readonly ITlCanBoPhuCapKeHoachBridgeNq104Service _tlCanBoPhuCapBridgeNq104Service;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_QUAN_LY_LUONG_KE_HOACH_DANH_SACH_DOI_TUONG_HUONG_LUONG_KE_HOACH_COPY;

        public override string Title => "Sao chép nhanh đối tượng";
        public override string Description => "Sao chép nhanh đối tượng theo tháng";
        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagementPlan.NewCadresPlan.NewCadresCopyMonthDialog);

        private List<TlDmCapBacKeHoachNq104> lstCapBacKeHoach;
        private List<TlPhuCapDieuChinhNq104Query> lstPhuCapDieuChinh;
        private List<TlDmCanBoKeHoachNq104> lstCanBoKeHoachNam;

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

        private ObservableCollection<TlDmDonViNq104Model> _donViItems;
        public ObservableCollection<TlDmDonViNq104Model> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViNq104Model _selectedDonViItems;
        public TlDmDonViNq104Model SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                if (SetProperty(ref _selectedDonViItems, value))
                {
                    LoadData();
                }
            }
        }

        private ObservableCollection<CadresNq104Model> _cadresItems;
        public ObservableCollection<CadresNq104Model> CadresItems
        {
            get => _cadresItems;
            set => SetProperty(ref _cadresItems, value);
        }

        private CadresNq104Model _selectedCarderItems;
        public CadresNq104Model SelectedCarderItems
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

        public NewCadresCopyMonthDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmCanBoNq104Service cadresService,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlDmCanBoKeHoachNq104Service tlDmCanBoKeHoachService,
            ITlDmCapBacKeHoachNq104Service tlDmCapBacKeHoachService,
            ITlPhuCapDieuChinhNq104Service tlPhuCapDieuChinhService,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapService,
            ITlCanBoPhuCapKeHoachNq104Service tlCanBoPhuCapKeHoachService,
            ITlDmCapBacNq104Service tlDmCapBacService,
            ITlDieuChinhQsKeHoachNq104Service tlDieuChinhQsKeHoachService,
            ITlQsKeHoachChiTietNq104Service tlQsKeHoachChiTietService,
            ITlDsBangLuongKeHoachNq104Service tlDsBangLuongKeHoachService,
            ITlQtChungTuChiTietKeHoachNq104Service tlQtChungTuChiTietKeHoachService,
            ITlBangLuongKeHoachNq104Service tlBangLuongKeHoachService,
            ITlDmPhuCapNq104Service tlDmPhuCapService,
            ITlDmCapBacLuongNq104Service tlDmCapBacLuongService,
            ITlCanBoPhuCapKeHoachBridgeNq104Service tlCanBoPhuCapBridgeNq104Service)
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
            _tlDmPhuCapService = tlDmPhuCapService;
            _tlDmCapBacLuongService = tlDmCapBacLuongService;
            _tlCanBoPhuCapBridgeNq104Service = tlCanBoPhuCapBridgeNq104Service;
        }

        public override void Init()
        {
            base.Init();
            lstCapBacKeHoach = _tlDmCapBacKeHoachService.FindAll().ToList();
            lstPhuCapDieuChinh = _tlPhuCapDieuChinhService.FindAllPhuCapDieuChinh().ToList();
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
            _donViItems = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
            if (Model.DonVi != null)
            {
                SelectedDonViItems = _donViItems.FirstOrDefault(x => Model.DonVi.MaDonVi.Equals(x.MaDonVi));
            }

            OnPropertyChanged(nameof(DonViItems));
        }

        private void LoadData()
        {
            try
            {
                var predicate = PredicateBuilder.True<TlDmCanBoNq104>();
                predicate = predicate.And(x => x.Thang == int.Parse(FromMonthSelected.ValueItem));
                predicate = predicate.And(x => x.Parent == SelectedDonViItems.MaDonVi);
                predicate = predicate.And(x => x.Nam == int.Parse(FromYearSelected.ValueItem));
                predicate = predicate.And(x => x.IsDelete == true);
                var data = _cadresService.FindByCondition(predicate).OrderBy(x => x.TenCanBo.Split(" ").Last());
                CadresItems = _mapper.Map<ObservableCollection<CadresNq104Model>>(data);
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
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _tlCanBoPhuCapKeHoachService.DeleteByYear(int.Parse(ToYearSelected.ValueItem));
                    _tlDmCanBoKeHoachService.DeleteByYear(int.Parse(ToYearSelected.ValueItem));
                    _tlQtChungTuChiTietKeHoachService.DeleteByNamAndMaDonVi(SelectedDonViItems.MaDonVi, int.Parse(ToYearSelected.ValueItem));
                    var bangLuong = _tlDsBangLuongKeHoachService.FindByCondition(CachTinhLuong.CACH0, SelectedDonViItems.MaDonVi, int.Parse(ToYearSelected.ValueItem));
                    if (bangLuong != null)
                    {
                        _tlBangLuongKeHoachService.DeleteByParentId(bangLuong.Id);
                        _tlDsBangLuongKeHoachService.Delete(bangLuong.Id);
                    }

                    var lstCopy = CadresItems.Where(x => x.IsSelected);
                    int year = int.Parse(ToYearSelected.ValueItem);

                    var lstSave = new ObservableCollection<TlDmCanBoKeHoachNq104Model>();
                    var lstQsKeHoachChiTiet = new List<TlQsKeHoachChiTietNq104>();
                    var lstTlDieuChinhKeHoach = new List<TlDieuChinhQsKeHoachNq104>();

                    for (int i = 1; i <= 12; i++)
                    {
                        TlQsKeHoachChiTietNq104 tlQsKeHoachChiTiet = new TlQsKeHoachChiTietNq104();
                        tlQsKeHoachChiTiet.Thang = i;
                        tlQsKeHoachChiTiet.Nam = int.Parse(ToYearSelected.ValueItem);
                        tlQsKeHoachChiTiet.MaDonVi = SelectedDonViItems.MaDonVi;
                        tlQsKeHoachChiTiet.TenDonVi = SelectedDonViItems.TenDonVi;
                        tlQsKeHoachChiTiet.SNguoiTao = _sessionService.Current.Principal;
                        tlQsKeHoachChiTiet.DNgayTao = DateTime.Now;
                        TlDieuChinhQsKeHoachNq104 tlDieuChinhQsKeHoach = new TlDieuChinhQsKeHoachNq104();
                        tlDieuChinhQsKeHoach.Thang = i;
                        tlDieuChinhQsKeHoach.Nam = int.Parse(ToYearSelected.ValueItem);
                        tlDieuChinhQsKeHoach.MaDonVi = SelectedDonViItems.MaDonVi;
                        tlDieuChinhQsKeHoach.TenDonVi = SelectedDonViItems.TenDonVi;
                        tlDieuChinhQsKeHoach.SNguoiTao = _sessionService.Current.Principal;
                        tlDieuChinhQsKeHoach.DNgayTao = DateTime.Now;
                        lstQsKeHoachChiTiet.Add(tlQsKeHoachChiTiet);
                        lstTlDieuChinhKeHoach.Add(tlDieuChinhQsKeHoach);
                    }

                    var listPhuCap = _tlDmPhuCapService.FindAll();
                    var listCapBacCanBo = _tlDmCapBacService.FindAll();
                    var lstCopyMaCanBo = lstCopy.Select(n => n.MaCanBo);
                    var listAllCanBoPhuCap = _tlCanBoPhuCapService.FindAll(x => lstCopyMaCanBo.Contains(x.MaCbo));
                    foreach (var item in lstCopy)
                    {
                        var canBoPhuCap = listAllCanBoPhuCap.FirstOrDefault(x => x.MaCbo.Equals(item.MaCanBo));
                        var lstcanBoPhuCap = new List<AllowencePhuCapNq104Criteria>();
                        try
                        {
                            if (canBoPhuCap != null)
                            {
                                var plainText = CompressExtension.DecompressFromBase64(canBoPhuCap.Data);
                                lstcanBoPhuCap = JsonConvert.DeserializeObject<AllowenceCanBoNq104Criteria>(plainText).X.ToList();
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex);
                        }

                        for (int i = 1; i <= 12; i++)
                        {
                            int iMonth = i;
                            string sMonth = i < 10 ? ("0" + i.ToString()) : i.ToString();
                            DateTime dateTimeKeHoach = new DateTime(year, iMonth, DateTime.DaysInMonth(year, iMonth));
                            var canBoKeHoachModel = _mapper.Map<TlDmCanBoKeHoachNq104Model>(item);
                            canBoKeHoachModel.MaCanBo = string.Format("{0}{1}{2}", year, sMonth, item.MaHieuCanBo);
                            canBoKeHoachModel.Thang = i;
                            canBoKeHoachModel.Nam = year;
                            canBoKeHoachModel.Id = Guid.NewGuid();

                            var cbKeHoachNext = lstCapBacKeHoach.FirstOrDefault(x => x.MaCb.Equals(canBoKeHoachModel.MaCb));
                            if (cbKeHoachNext != null)
                            {
                                canBoKeHoachModel.ThoiHanTangCb = cbKeHoachNext.ThoiHanTang;
                                canBoKeHoachModel.CbKeHoach = cbKeHoachNext.Id.ToString();
                            }
                            var capBacKeHoach = lstCapBacKeHoach.FirstOrDefault(x => x.MaCbKeHoach == canBoKeHoachModel.MaCb);
                            var namTangQH = DateTime.Now.Year - (canBoKeHoachModel.NgayNhanCb?.Year ?? DateTime.Now.Year);
                            var thangTangQH = DateTime.Now.Month - (canBoKeHoachModel.NgayNhanCb?.Month ?? DateTime.Now.Month);
                            var thoiHanTangQH = namTangQH * 12 + thangTangQH;

                            var age = DateTime.Now.Year - canBoKeHoachModel.NgaySinh?.Year ?? DateTime.Now.Year;
                            bool isNghiHuu;
                            if (canBoKeHoachModel.IsNam == true)
                                isNghiHuu = age >= (capBacKeHoach?.TuoiHuuNam.GetValueOrDefault() ?? 0) ? true : false;
                            else
                                isNghiHuu = age >= (capBacKeHoach?.TuoiHuuNu.GetValueOrDefault() ?? 0) ? true : false;

                            canBoKeHoachModel.IsDelete = isNghiHuu ? false : true;

                            //HSQ-CS xuất ngũ
                            int thoiHanXuatNgu = 0;
                            if (canBoKeHoachModel.LoaiDoiTuong.Equals(LoaiDoiTuong.HSQCS))
                            {
                                var namXuatNgu = year - (canBoKeHoachModel.NgayNhanCb?.Year ?? DateTime.Now.Year);
                                var thangXuatNgu = i - (canBoKeHoachModel.NgayNhanCb?.Month ?? DateTime.Now.Month);
                                thoiHanXuatNgu = namXuatNgu * 12 + thangXuatNgu;
                                if (thoiHanXuatNgu > 24)
                                    canBoKeHoachModel.IsDelete = false;
                            }
                            //Tăng quân hàm
                            if (capBacKeHoach != null && thoiHanTangQH >= canBoKeHoachModel.ThoiHanTangCb.GetValueOrDefault())
                            {

                                TlDmCapBacLuongNq104 dataBacLuong = new TlDmCapBacLuongNq104();
                                //Sỹ quan, HCY
                                if (canBoKeHoachModel.LoaiDoiTuong.Equals(LoaiDoiTuong.SQ) || canBoKeHoachModel.LoaiDoiTuong.Equals(LoaiDoiTuong.HCY))
                                {
                                    var xauNoiMa = "1" + "-" + canBoKeHoachModel.MaBacLuong;
                                    dataBacLuong = _tlDmCapBacLuongService.FindAllByXauNoiMa(xauNoiMa, _sessionService.Current.YearOfWork).FirstOrDefault();
                                }
                                //QNCN, CMKTCY
                                else if (canBoKeHoachModel.LoaiDoiTuong.Equals(LoaiDoiTuong.QNCN) || canBoKeHoachModel.LoaiDoiTuong.Equals(LoaiDoiTuong.CMKTCY))
                                {
                                    var xauNoiMa = "2" + "-" + canBoKeHoachModel.Loai + "-" + canBoKeHoachModel.NhomChuyenMon + "-" + canBoKeHoachModel.MaBacLuong;
                                    dataBacLuong = _tlDmCapBacLuongService.FindAllByXauNoiMa(xauNoiMa, _sessionService.Current.YearOfWork).FirstOrDefault();
                                }
                                //CNQP, VCQP
                                else if (canBoKeHoachModel.LoaiDoiTuong.Equals(LoaiDoiTuong.CNQP) || canBoKeHoachModel.LoaiDoiTuong.Equals(MA_CAP_BAC.VCQP))
                                {
                                    var xauNoiMa = "3" + "-" + canBoKeHoachModel.Loai + "-" + canBoKeHoachModel.NhomChuyenMon + "-" + canBoKeHoachModel.MaBacLuong;
                                    dataBacLuong = _tlDmCapBacLuongService.FindAllByXauNoiMa(xauNoiMa, _sessionService.Current.YearOfWork).FirstOrDefault();
                                }
                                //CCQP
                                else if (canBoKeHoachModel.LoaiDoiTuong.Equals(LoaiDoiTuong.CCQP))
                                {
                                    var xauNoiMa = "3" + "-" + canBoKeHoachModel.MaBacLuong;
                                    dataBacLuong = _tlDmCapBacLuongService.FindAllByXauNoiMa(xauNoiMa, _sessionService.Current.YearOfWork).FirstOrDefault();
                                }
                                //HSQ-CS, binh nhì
                                else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.BINH_NHI))
                                {
                                    var xauNoiMa = "7" + "-" + MA_CAP_BAC.BINH_NHI;
                                    dataBacLuong = _tlDmCapBacLuongService.FindAllByXauNoiMa(xauNoiMa, _sessionService.Current.YearOfWork).FirstOrDefault();
                                }
                                //HSQ-CS, binh nhất
                                else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.BINH_NHAT))
                                {
                                    var xauNoiMa = "7" + "-" + MA_CAP_BAC.BINH_NHAT;
                                    dataBacLuong = _tlDmCapBacLuongService.FindAllByXauNoiMa(xauNoiMa, _sessionService.Current.YearOfWork).FirstOrDefault();
                                }
                                //HSQ-CS, hạ sĩ
                                else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.HA_SI))
                                {
                                    var xauNoiMa = "7" + "-" + MA_CAP_BAC.HA_SI;
                                    dataBacLuong = _tlDmCapBacLuongService.FindAllByXauNoiMa(xauNoiMa, _sessionService.Current.YearOfWork).FirstOrDefault();
                                }
                                //HSQ-CS, trung sĩ
                                else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.TRUNG_SI))
                                {
                                    var xauNoiMa = "7" + "-" + MA_CAP_BAC.TRUNG_SI;
                                    dataBacLuong = _tlDmCapBacLuongService.FindAllByXauNoiMa(xauNoiMa, _sessionService.Current.YearOfWork).FirstOrDefault();
                                }
                                //HSQ-CS, thượng sĩ
                                else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.THUONG_SI))
                                {
                                    var xauNoiMa = "7" + "-" + MA_CAP_BAC.THUONG_SI;
                                    dataBacLuong = _tlDmCapBacLuongService.FindAllByXauNoiMa(xauNoiMa, _sessionService.Current.YearOfWork).FirstOrDefault();
                                }

                                canBoKeHoachModel.MaCb = capBacKeHoach.MaCbKeHoach;
                                canBoKeHoachModel.TienLuongCb = dataBacLuong.TienLuong.GetValueOrDefault();
                                canBoKeHoachModel.TienNangLuongCb = dataBacLuong.TienNangLuong.GetValueOrDefault();

                                lstcanBoPhuCap.FirstOrDefault(x => x.A.Equals(PhuCap.LCB_TT)).B = dataBacLuong.TienLuong.GetValueOrDefault();
                                lstcanBoPhuCap.FirstOrDefault(x => x.A.Equals(PhuCap.NLCB_TT)).B = dataBacLuong.TienNangLuong.GetValueOrDefault();
                            }

                            List<TlCanBoPhuCapKeHoachNq104Model> lstCanBoPhuCapKeHoach = new List<TlCanBoPhuCapKeHoachNq104Model>();
                            TlCanBoPhuCapKeHoachNq104Model tlCanBoPhuCapKeHoachModel = new TlCanBoPhuCapKeHoachNq104Model();
                            tlCanBoPhuCapKeHoachModel.MaCanBo = canBoKeHoachModel.MaCanBo;
                            tlCanBoPhuCapKeHoachModel.MaPhuCap = "";

                            //Điều chỉnh phụ cấp
                            foreach (var pcDieuChinh in lstPhuCapDieuChinh)
                            {
                                var pcCanBoKeHoach = lstcanBoPhuCap.FirstOrDefault(x => x.A.Equals(pcDieuChinh.MaPhuCap));
                                if (pcCanBoKeHoach != null && dateTimeKeHoach > pcDieuChinh.ApDungTu)
                                {
                                    pcCanBoKeHoach.B = pcDieuChinh.GiaTriMoi;
                                }
                            }

                            //Convert to Json
                            string strJson = JsonConvert.SerializeObject(new AllowenceCanBoNq104Criteria()
                            {
                                X = lstcanBoPhuCap
                            });

                            tlCanBoPhuCapKeHoachModel.Data = CompressExtension.CompressToBase64(strJson);
                            lstCanBoPhuCapKeHoach.Add(tlCanBoPhuCapKeHoachModel);

                            var capBacCanBo = listCapBacCanBo.FirstOrDefault(x => x.MaCb == canBoKeHoachModel.MaCb);
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
                            #region
                            //Quyết toán quân số Kế hoạch chi tiết
                            var cbKeHoach1 = lstCapBacKeHoach.FirstOrDefault(x => x.MaCb.Equals(canBoKeHoachModel.MaCb));
                            if (cbKeHoach1 != null)
                            {
                                DateTime ngaySinh = (DateTime)canBoKeHoachModel.NgaySinh;
                                var tlQsKeHoachChiTiet = lstQsKeHoachChiTiet.FirstOrDefault(x => x.Thang == i);
                                DateTime ngayXn;
                                if (!canBoKeHoachModel.LoaiDoiTuong.Equals(LoaiDoiTuong.HSQCS))
                                {
                                    if (canBoKeHoachModel.IsNam == true)
                                    {
                                        ngayXn = ngaySinh.AddYears((int)(cbKeHoach1.TuoiHuuNam + 1)).AddMonths(1);
                                    }
                                    else
                                    {
                                        ngayXn = ngaySinh.AddYears((int)(cbKeHoach1.TuoiHuuNu + 1));
                                    }
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
                                            if (canBoKeHoachModel.MaCb.StartsWith("13") || canBoKeHoachModel.MaCb.StartsWith("53"))
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
                                            else if (canBoKeHoachModel.MaCb.StartsWith("124") || canBoKeHoachModel.MaCb.StartsWith("524"))
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
                                            else if (canBoKeHoachModel.MaCb.StartsWith("123") || canBoKeHoachModel.MaCb.StartsWith("523"))
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
                                            else if (canBoKeHoachModel.MaCb.StartsWith("122") || canBoKeHoachModel.MaCb.StartsWith("522"))
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
                                            else if (canBoKeHoachModel.MaCb.StartsWith("121") || canBoKeHoachModel.MaCb.StartsWith("521"))
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
                                            else if (canBoKeHoachModel.MaCb.StartsWith("114") || canBoKeHoachModel.MaCb.StartsWith("514"))
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
                                            else if (canBoKeHoachModel.MaCb.StartsWith("113") || canBoKeHoachModel.MaCb.StartsWith("513"))
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
                                            else if (canBoKeHoachModel.MaCb.StartsWith("112") || canBoKeHoachModel.MaCb.StartsWith("512"))
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
                                            else if (canBoKeHoachModel.MaCb.StartsWith("111") || canBoKeHoachModel.MaCb.StartsWith("511"))
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
                                            else if (canBoKeHoachModel.LoaiDoiTuong.Equals(LoaiDoiTuong.QNCN))
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
                                        
                                    }
                                    if (ngayXn.Month < dateTimeKeHoach.Month)
                                    {
                                        continue;
                                    }
                                }
                                //Hạ sĩ quan chiến sỹ
                                else
                                {
                                    if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.BINH_NHI))
                                    {
                                        if (tlQsKeHoachChiTiet.FSoBinhNhi == null)
                                        {
                                            tlQsKeHoachChiTiet.FSoBinhNhi = 1;
                                        }
                                        else
                                        {
                                            tlQsKeHoachChiTiet.FSoBinhNhi++;
                                        }
                                    }
                                    else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.BINH_NHAT))
                                    {
                                        if (tlQsKeHoachChiTiet.FSoBinhNhat == null)
                                        {
                                            tlQsKeHoachChiTiet.FSoBinhNhat = 1;
                                        }
                                        else
                                        {
                                            tlQsKeHoachChiTiet.FSoBinhNhat++;
                                        }
                                    }
                                    else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.HA_SI))
                                    {
                                        if (tlQsKeHoachChiTiet.FSoHaSi == null)
                                        {
                                            tlQsKeHoachChiTiet.FSoHaSi = 1;
                                        }
                                        else
                                        {
                                            tlQsKeHoachChiTiet.FSoHaSi++;
                                        }
                                    }
                                    else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.TRUNG_SI))
                                    {
                                        if (tlQsKeHoachChiTiet.FSoTrungSi == null)
                                        {
                                            tlQsKeHoachChiTiet.FSoTrungSi = 1;
                                        }
                                        else
                                        {
                                            tlQsKeHoachChiTiet.FSoTrungSi++;
                                        }
                                    }
                                    else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.THUONG_SI))
                                    {
                                        if (tlQsKeHoachChiTiet.FSoThuongSi == null)
                                        {
                                            tlQsKeHoachChiTiet.FSoThuongSi = 1;
                                        }
                                        else
                                        {
                                            tlQsKeHoachChiTiet.FSoThuongSi++;
                                        }
                                    }
                                    //Hạ sĩ quan chiến sỹ ra quân
                                    else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.BINH_NHI) && thoiHanXuatNgu > 24)
                                    {
                                        if (tlQsKeHoachChiTiet.FPcrqBinhNhi == null)
                                        {
                                            tlQsKeHoachChiTiet.FPcrqBinhNhi = 1;
                                        }
                                        else
                                        {
                                            tlQsKeHoachChiTiet.FPcrqBinhNhi++;
                                        }
                                    }
                                    else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.BINH_NHAT) && thoiHanXuatNgu > 24)
                                    {
                                        if (tlQsKeHoachChiTiet.FPcrqBinhNhat == null)
                                        {
                                            tlQsKeHoachChiTiet.FPcrqBinhNhat = 1;
                                        }
                                        else
                                        {
                                            tlQsKeHoachChiTiet.FPcrqBinhNhat++;
                                        }
                                    }
                                    else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.HA_SI) && thoiHanXuatNgu > 24)
                                    {
                                        if (tlQsKeHoachChiTiet.FPcrqHaSi == null)
                                        {
                                            tlQsKeHoachChiTiet.FPcrqHaSi = 1;
                                        }
                                        else
                                        {
                                            tlQsKeHoachChiTiet.FPcrqHaSi++;
                                        }
                                    }
                                    else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.TRUNG_SI) && thoiHanXuatNgu > 24)
                                    {
                                        if (tlQsKeHoachChiTiet.FPcrqTrungSi == null)
                                        {
                                            tlQsKeHoachChiTiet.FPcrqTrungSi = 1;
                                        }
                                        else
                                        {
                                            tlQsKeHoachChiTiet.FPcrqTrungSi++;
                                        }
                                    }
                                    else if (canBoKeHoachModel.MaCb.Equals(MA_CAP_BAC.THUONG_SI) && thoiHanXuatNgu > 24)
                                    {
                                        if (tlQsKeHoachChiTiet.FPcrqThuongSi == null)
                                        {
                                            tlQsKeHoachChiTiet.FPcrqThuongSi = 1;
                                        }
                                        else
                                        {
                                            tlQsKeHoachChiTiet.FPcrqThuongSi++;
                                        }
                                    }
                                }
                            }

                            #endregion

                            if (canBoKeHoachModel.NamTn != item.NamTn)
                            {
                                string loai = canBoKeHoachModel.Loai == null ? string.Empty : canBoKeHoachModel.Loai;
                                loai = string.Format("{0}{1};", loai, LoaiCanBoKehoach.TANG_THAMNIEN);
                                canBoKeHoachModel.Loai = loai;
                                canBoKeHoachModel.IsModified = true;
                            }

                            canBoKeHoachModel.Loai = canBoKeHoachModel.Loai == null ? string.Empty : canBoKeHoachModel.Loai;

                            _tlCanBoPhuCapKeHoachService.BulkInsert(_mapper.Map<ObservableCollection<TlCanBoPhuCapKeHoachNq104>>(lstCanBoPhuCapKeHoach));
                            _tlDmCanBoKeHoachService.Add(_mapper.Map<TlDmCanBoKeHoachNq104>(canBoKeHoachModel));
                            lstSave.Add(canBoKeHoachModel);
                        }
                        //_tlCanBoPhuCapBridgeNq104Service.DataPreprocess(12, year);
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

                    _tlQsKeHoachChiTietService.DeleteByNam(int.Parse(ToYearSelected.ValueItem));
                    _tlDieuChinhQsKeHoachService.DeleteByNam(int.Parse(ToYearSelected.ValueItem));

                    _tlDieuChinhQsKeHoachService.AddRange(lstTlDieuChinhKeHoach);
                    _tlQsKeHoachChiTietService.AddRange(lstQsKeHoachChiTiet);
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

        private int TinhNamThamNien(DateTime? ngayNn, DateTime? ngayXn, DateTime? ngayTn, int thangTnn, DateTime dateTimeKeHoach)
        {
            return DateUtils.TinhNamThamNien(ngayNn, ngayXn, ngayTn, thangTnn, dateTimeKeHoach.Month, dateTimeKeHoach.Year);
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
            if (SelectedDonViItems == null)
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
