using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.SalaryYearPlan
{
    public class SalaryYearPlanDialogViewModel : DialogViewModelBase<TlDsBangLuongKeHoachModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDsBangLuongKeHoachService _tlDsBangLuongKeHoachService;
        private readonly ITlDmCachTinhLuongChuanService _tlDmCachTinhLuongChuanService;
        private readonly ITlCanBoPhuCapKeHoachService _tlCanBoPhuCapKeHoachService;
        private readonly ITlBangLuongKeHoachService _tlBangLuongKeHoachService;
        private readonly ITlQtChungTuChiTietKeHoachService _tlQtChungTuChiTietKeHoachService;
        private readonly ITlQtChungTuChiTietService _tlQtChungTuChiTietService;
        private readonly ITlQtChungTuService _tlQtChungTuService;
        private readonly ITlDieuChinhQsKeHoachService _tlDieuChinhQsKeHoachService;
        private SessionInfo _sessionInfo;

        public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH_BANG_LUONG_NAM_KH_DIALOG;

        public override Type ContentType => typeof(View.Salary.SalaryManagementPlan.SalaryYearPlan.SalaryYearPlanDialog);
        public override PackIconKind IconKind => PackIconKind.Calculator;
        public override string Title => "Thêm mới bảng lương năm kế hoạch";
        public override string Description => "Tạo mới bảng lương năm kế hoạch";

        private List<TlCachTinhLuongModel> dsCongThucLuong;
        public List<TlCachTinhLuongModel> DsCongThucLuong
        {
            get => dsCongThucLuong;
            set => dsCongThucLuong = value;
        }

        private ObservableCollection<AllowenceModel> _dataAllowence;
        public ObservableCollection<AllowenceModel> DataAllowence
        {
            get => _dataAllowence;
            set => SetProperty(ref _dataAllowence, value);
        }

        private AllowenceModel _selectedAllowence;
        public AllowenceModel SelectedAllowence
        {
            get => _selectedAllowence;
            set => SetProperty(ref _selectedAllowence, value);
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _yearSelected;
        public ComboboxItem YearSelected
        {
            get => _yearSelected;
            set
            {
                SetProperty(ref _yearSelected, value);
                if (_yearSelected != null)
                {
                    Model.Nam = int.Parse(_yearSelected.ValueItem);
                    LoadTenDsCnLuong();
                }
            }
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
                //LoadTenDsCnLuong();
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

        private string _tenDs;
        public string TenDs
        {
            get => _tenDs;
            set => SetProperty(ref _tenDs, value);
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

        public SalaryYearPlanDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlDsBangLuongKeHoachService tlDsBangLuongKeHoachService,
            ITlDmCachTinhLuongChuanService tlDmCachTinhLuongChuanService,
            ITlCanBoPhuCapKeHoachService tlCanBoPhuCapKeHoachService,
            ITlBangLuongKeHoachService tlBangLuongKeHoachService,
            ITlQtChungTuChiTietKeHoachService tlQtChungTuChiTietKeHoachService,
            ITlQtChungTuChiTietService tlQtChungTuChiTietService,
            ITlQtChungTuService tlQtChungTuService,
            ITlDieuChinhQsKeHoachService tlDieuChinhQsKeHoachService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _tlDmDonViService = tlDmDonViService;
            _tlDsBangLuongKeHoachService = tlDsBangLuongKeHoachService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _tlCanBoPhuCapKeHoachService = tlCanBoPhuCapKeHoachService;
            _tlBangLuongKeHoachService = tlBangLuongKeHoachService;
            _tlQtChungTuChiTietService = tlQtChungTuChiTietService;
            _tlQtChungTuService = tlQtChungTuService;
            _tlQtChungTuChiTietKeHoachService = tlQtChungTuChiTietKeHoachService;
            _tlDieuChinhQsKeHoachService = tlDieuChinhQsKeHoachService;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new Thickness(10);

            LoadCongThucLuong();
            LoadDonViData();
            LoadYears();
            LoadTenDsCnLuong();
        }

        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            for (int i = 1970; i <= 2050; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == Model.Nam.ToString());
        }

        private void LoadDonViData()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            DonViItems = new ObservableCollection<TlDmDonViModel>();
            DonViItems = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
            if (DonViItems != null && DonViItems.Count > 0)
            {
                SelectedDonViItems = DonViItems.FirstOrDefault(x => x.MaDonVi == Model.MaDonVi);
            }
        }

        private void LoadCongThucLuong()
        {
            var data = _tlDmCachTinhLuongChuanService.FindAll().ToList();
            DsCongThucLuong = _mapper.Map<List<TlCachTinhLuongModel>>(data);
        }

        private void LoadTenDsCnLuong()
        {
            //if (SelectedDonViItems == null)
            //{
            TenDs = string.Format("Danh sách lương kế hoạch năm {0} ", Model.Nam);
            //}
            // else
            //{
            //    TenDs = string.Format("Danh sách lương kế hoạch năm {0} - {1} ", Model.Nam, SelectedDonViItems.TenDonVi);
            //}
            //OnPropertyChanged(TenDs);
        }

        public override void OnSave()
        {
            base.OnSave();
            string msg = GetMessageValidate();
            if (!string.IsNullOrEmpty(msg))
            {
                System.Windows.Forms.MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                IsLoading = true;
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    var listDonviSelected = DonViItems.Where(x => x.Selected).ToList();
                    foreach(var donvi  in listDonviSelected)
                    {
                        Model.MaDonVi = donvi.MaDonVi;
                        Model.Nam = int.Parse(YearSelected.ValueItem);
                        Model.TenBangLuong = string.Format("Danh sách lương kế hoạch năm {0} - {1} ", Model.Nam, donvi.TenDonVi);
                        Model.MaCachTl = CachTinhLuong.CACH0;
                        Model.UserCreator = _sessionService.Current.Principal;
                        Model.DateCreated = DateTime.Now;
                        Model.Id = Guid.NewGuid();

                        _tlDsBangLuongKeHoachService.Add(_mapper.Map<TlDsBangLuongKeHoach>(Model));

                        for (int i = 1; i <= 12; i++)
                        {
                            var lstCanBoData = _tlBangLuongKeHoachService.FindCbLuong(i, Model.Nam, Model.MaDonVi).Where(x => x.KhongLuong == false);
                            var lstCanBo = _mapper.Map<ObservableCollection<TlDmCanBoKeHoachModel>>(lstCanBoData);
                            ObservableCollection<TlBangLuongKeHoachModel> tlBangLuongThangModels = new ObservableCollection<TlBangLuongKeHoachModel>();

                            foreach (var item in lstCanBo)
                            {
                                TlBangLuongThangModel tlBangLuongThangModel = new TlBangLuongThangModel();
                                //var listPhuCap = _tlCanBoPhuCapKeHoachService.FindByMaCanBo(item.MaCanBo);
                                var listPhuCap = _tlCanBoPhuCapKeHoachService.FindCanBoPhuCapKeHoachByMaCanBo(item.MaCanBo);
                                var listPhuCapModel = _mapper.Map<ObservableCollection<TlCanBoPhuCapKeHoachModel>>(listPhuCap).Select(x =>
                                {
                                    x.GiaTri = (x.MaPhuCap == PhuCap.NTN && x.GiaTri < 5) ? x.GiaTri = 0 : x.GiaTri;
                                    return x;
                                }).ToList();
                                if (listPhuCapModel != null && listPhuCapModel.Count > 0)
                                {
                                    foreach (var congThucLuong in DsCongThucLuong)
                                    {
                                        congThucLuong.Value = 0;
                                        congThucLuong.IsCalculated = false;
                                    }
                                    foreach (var item1 in listPhuCap)
                                    {
                                        var bangLuong = CreateBangLuongKeHoachModel(Model.Id, item, item1.MaPhuCap, item1.GiaTri);
                                        tlBangLuongThangModels.Add(bangLuong);
                                    }
                                    Dictionary<string, decimal> results = new Dictionary<string, decimal>();
                                    foreach (var cachTinhLuong in DsCongThucLuong)
                                    {
                                        results.Add(cachTinhLuong.MaCot, TinhLuong(listPhuCapModel, cachTinhLuong));
                                    }

                                    var keys = results.Keys;
                                    foreach (var key in keys.ToList())
                                    {
                                        string value = results[key].ToString("N4");
                                        var bangLuong = tlBangLuongThangModels.Where(x => x.MaPhuCap == key && x.MaCanBo == item.MaCanBo).FirstOrDefault();
                                        if (bangLuong != null)
                                        {
                                            bangLuong.GiaTri = Decimal.Parse(value);
                                        }
                                    }
                                    //var pcLhtTT = tlBangLuongThangModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.LHT_TT) && x.MaCanBo == item.MaCanBo);
                                    //if (pcLhtTT != null && pcLhtTT.GiaTri != null && item.TlDmCapBac != null && item.TlDmCapBac.TiLeHuong != null)
                                    //{
                                    //    pcLhtTT.GiaTri = pcLhtTT.GiaTri * item.TlDmCapBac.TiLeHuong;
                                    //}
                                    var pcTrichLuong = tlBangLuongThangModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.TRICHLUONG_TT) && x.MaCanBo == item.MaCanBo);
                                    if (pcTrichLuong != null && pcTrichLuong.GiaTri != null)
                                    {
                                        pcTrichLuong.GiaTri = Math.Round((decimal)pcTrichLuong.GiaTri / 1000) * 1000;
                                    }
                                }
                            }

                            var tlBangLuongKeHoachs = _mapper.Map<ObservableCollection<TlBangLuongKeHoach>>(tlBangLuongThangModels.Where(x => x.GiaTri.GetValueOrDefault() != 0));
                            _tlBangLuongKeHoachService.BulkInsert(tlBangLuongKeHoachs);
                        }
                        TinhQuyetToanChungTuChiTiet(donvi);
                    }                   
                }, (s, e) =>
                {
                    IsLoading = false;
                    System.Windows.Forms.MessageBox.Show("Tạo bảng lương năm kế hoạch thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SavedAction?.Invoke(SaveCommand);
                    DialogHost.Close("RootDialog");
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void TinhQuyetToanChungTuChiTiet(TlDmDonViModel donvi)
        {
            try
            {
                var _lstTlDieuChinhQsKeHoaches = _tlDieuChinhQsKeHoachService.FindData(Model.Nam, Model.MaDonVi).ToList();
                var lstSaveQtChungTuChiTiet = new List<TlQtChungTuChiTietKeHoachModel>();
                for (int i = 1; i <= 12; i++)
                {
                    var chungTuChiTietQueries = _tlQtChungTuChiTietKeHoachService.GetDataByMonth(donvi.MaDonVi, i, int.Parse(YearSelected.ValueItem), int.Parse(YearSelected.ValueItem) - 1).ToList();
                    var lstChungTuChiTiet = _mapper.Map<ObservableCollection<TlQtChungTuChiTietKeHoachModel>>(chungTuChiTietQueries).ToList();
                    lstChungTuChiTiet.Select(x =>
                    {
                        x.Id = Guid.NewGuid();
                        x.IdDonVi = donvi.MaDonVi;
                        x.TenDonVi = donvi.TenDonVi;
                        x.DateCreated = DateTime.Now;
                        x.UserCreator = _sessionService.Current.Principal;
                        return x;
                    }).ToList();
                    lstSaveQtChungTuChiTiet.AddRange(lstChungTuChiTiet);
                }

                var lstMap = lstSaveQtChungTuChiTiet.Where(x => ((x.TongCong != null || x.TongNamTruoc != null) && (x.TongCong != 0 || x.TongNamTruoc != 0) && x.BHangCha != true) || x.Thang == null).ToList();
                var lstSave = _mapper.Map<ObservableCollection<TlQtChungTuChiTietKeHoach>>(lstMap);
                _tlQtChungTuChiTietKeHoachService.BulkInsert(lstSave);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private decimal TinhLuong(List<TlCanBoPhuCapKeHoachModel> tlBangLuongThangModel, TlCachTinhLuongModel congThucLuong)
        {
            if (congThucLuong.IsCalculated == true)
            {
                return congThucLuong.Value;
            }
            else
            {
                var data = new Dictionary<string, object>();
                List<string> phuCap = congThucLuong.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (congThucLuong.CongThuc != PhuCap.THUETNCN_TT_CONGTHUC)
                {
                    foreach (var congThuc in phuCap)
                    {
                        var congThucExit = CheckExitCongThuc(congThuc);
                        if (congThucExit != null)
                        {
                            if (!data.ContainsKey(congThuc))
                            {
                                data.Add(congThuc, TinhLuong(tlBangLuongThangModel, congThucExit));

                            }
                        }
                        else
                        {
                            var property = tlBangLuongThangModel.Where(x => x.MaPhuCap == congThuc).FirstOrDefault();
                            if (property != null)
                            {
                                var pcCongChuan = tlBangLuongThangModel.FirstOrDefault(x => PhuCap.CONGCHUAN_SN.Equals(x.MaPhuCap));
                                if (property.HuongPcSn != null
                                    && pcCongChuan != null && pcCongChuan.GiaTri != null
                                    && pcCongChuan.GiaTri != 0
                                    && pcCongChuan.GiaTri > 0)
                                {
                                    property.GiaTri = (decimal)(property.GiaTri * property.HuongPcSn / pcCongChuan.GiaTri);
                                }
                                data.Add(congThuc, property.GiaTri ?? 0);
                            }
                            else
                            {
                                data.Add(congThuc, 0);
                            }
                        }
                    }
                }
                else if (congThucLuong.CongThuc == PhuCap.THUETNCN_TT_CONGTHUC)
                {
                    var luongThueTNCN = CheckExitCongThuc(PhuCap.LUONGTHUE_TT);
                    if (luongThueTNCN.Value == 0)
                    {
                        data.Add(PhuCap.LUONGTHUE_TT, TinhLuong(tlBangLuongThangModel, luongThueTNCN));
                    }
                    else
                    {
                        var tien = Convert.ToString(ThueTN(luongThueTNCN.Value));
                        data.Add(PhuCap.THUETNCN_TT_CONGTHUC, tien);
                    }
                }
                congThucLuong.IsCalculated = true;
                if (data.Count() > 0)
                {
                    var result = EvalExtensions.Execute(congThucLuong.CongThuc, data);
                    congThucLuong.Value = Math.Round(decimal.Parse(result.ToString()));
                    return congThucLuong.Value;
                }
            }
            return 0;
        }

        private TlCachTinhLuongModel CheckExitCongThuc(string congThuc)
        {
            return DsCongThucLuong.Where(x => x.MaCot == congThuc).FirstOrDefault();
        }

        private decimal ThueTN(decimal luongThuThue)
        {
            var data = _tlBangLuongKeHoachService.FindThue().ToList().OrderBy(x => x.ThuNhapTu);
            decimal tienThue = 0;
            decimal t = luongThuThue.Clone();
            var DmThuThue = _mapper.Map<List<TlDmThueThuNhapCaNhanModel>>(data);
            if (luongThuThue <= 0)
            {
                return 0;
            }
            else
            {
                foreach (var item in DmThuThue)
                {
                    if (luongThuThue >= (decimal)item.ThuNhapDen && item.ThuNhapDen != 0)
                    {
                        tienThue += ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu) * ((decimal)item.ThueXuat / 100);
                        t = t - ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu);
                    }
                    else if (item.ThuNhapDen == 0)
                    {
                        tienThue += (luongThuThue - (decimal)item.ThuNhapTu) * ((decimal)item.ThueXuat / 100);
                    }
                    else if (luongThuThue < (decimal)item.ThuNhapDen)
                    {
                        decimal tien = t * ((decimal)item.ThueXuat / 100);
                        tienThue += tien;
                        return tienThue;
                    }
                }
                return tienThue;
            }
        }

        private TlBangLuongKeHoachModel CreateBangLuongKeHoachModel(Guid id, TlDmCanBoKeHoachModel TlQtChungTuChiTietKeHoachModel, string maPhuCap, decimal? giaTri)
        {
            TlBangLuongKeHoachModel model = new TlBangLuongKeHoachModel();
            model.Parent = id;
            model.MaCachTl = CachTinhLuong.CACH0;
            model.Thang = (int)TlQtChungTuChiTietKeHoachModel.Thang;
            model.Nam = (int)TlQtChungTuChiTietKeHoachModel.Nam;
            model.MaCanBo = TlQtChungTuChiTietKeHoachModel.MaCanBo;
            model.MaCb = TlQtChungTuChiTietKeHoachModel.MaCb;
            model.TenCanBo = TlQtChungTuChiTietKeHoachModel.TenCanBo;
            model.MaDonVi = TlQtChungTuChiTietKeHoachModel.Parent;
            model.MaPhuCap = maPhuCap;
            model.GiaTri = giaTri;
            model.MaHieuCanBo = TlQtChungTuChiTietKeHoachModel.MaHieuCanBo;
            return model;
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();

            var lstDonViSelected = DonViItems.Where(x => x.Selected).ToList();
            if (lstDonViSelected.Count == 0)
            {
                messages.Add(string.Format(Resources.UnitNull));
            }
            if (TenDs.Equals(string.Empty))
            {
                messages.Add(string.Format(Resources.SalaryTableNameNull));
            }
            if (lstDonViSelected.Count > 0)
            {
                foreach( var donvi in lstDonViSelected) {
                    TlDsBangLuongKeHoach tlDsCapNhapBangLuong = _tlDsBangLuongKeHoachService.FindByCondition(CachTinhLuong.CACH0, donvi.MaDonVi, (int)Model.Nam);
                    if (tlDsCapNhapBangLuong != null)
                    {
                      messages.Add(string.Format("Bảng lương năm {0} đơn vị {1} đã tồn tại!", YearSelected.ValueItem, donvi.TenDonVi));
                      return string.Join(Environment.NewLine, messages);
                    }
                }
    
            }
            return string.Join(Environment.NewLine, messages);
        }
    }
}
