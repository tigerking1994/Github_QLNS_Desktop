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
using Newtonsoft.Json;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using Newtonsoft.Json.Linq;
using System.Windows.Markup;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewSalaryYearPlan
{
    public class NewSalaryYearPlanDialogViewModel : DialogViewModelBase<TlDsBangLuongKeHoachNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlDsBangLuongKeHoachNq104Service _tlDsBangLuongKeHoachService;
        private readonly ITlDmCachTinhLuongChuanNq104Service _tlDmCachTinhLuongChuanService;
        private readonly ITlCanBoPhuCapKeHoachNq104Service _tlCanBoPhuCapKeHoachService;
        private readonly ITlBangLuongKeHoachNq104Service _tlBangLuongKeHoachService;
        private readonly ITlQtChungTuChiTietKeHoachNq104Service _tlQtChungTuChiTietKeHoachService;
        private readonly ITlQtChungTuChiTietNq104Service _tlQtChungTuChiTietService;
        private readonly ITlQtChungTuNq104Service _tlQtChungTuService;
        private readonly ITlDieuChinhQsKeHoachNq104Service _tlDieuChinhQsKeHoachService;
        private readonly ITlBangLuongKeHoachBridgeNq104Service _tlBangLuongBridgeService;
        private SessionInfo _sessionInfo;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_QUAN_LY_LUONG_KE_HOACH_BANG_LUONG_NAM_KH_DIALOG;

        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagementPlan.NewSalaryYearPlan.NewSalaryYearPlanDialog);
        public override PackIconKind IconKind => PackIconKind.Calculator;
        public override string Title => "Thêm mới bảng lương năm kế hoạch";
        public override string Description => "Tạo mới bảng lương năm kế hoạch";

        private List<TlCachTinhLuongNq104Model> dsCongThucLuong;
        public List<TlCachTinhLuongNq104Model> DsCongThucLuong
        {
            get => dsCongThucLuong;
            set => dsCongThucLuong = value;
        }

        private ObservableCollection<AllowenceNq104Model> _dataAllowence;
        public ObservableCollection<AllowenceNq104Model> DataAllowence
        {
            get => _dataAllowence;
            set => SetProperty(ref _dataAllowence, value);
        }

        private AllowenceNq104Model _selectedAllowence;
        public AllowenceNq104Model SelectedAllowence
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
                SetProperty(ref _selectedDonViItems, value);
                LoadTenDsCnLuong();
            }
        }

        private string _tenDs;
        public string TenDs
        {
            get => _tenDs;
            set => SetProperty(ref _tenDs, value);
        }

        public NewSalaryYearPlanDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlDsBangLuongKeHoachNq104Service tlDsBangLuongKeHoachService,
            ITlDmCachTinhLuongChuanNq104Service tlDmCachTinhLuongChuanService,
            ITlCanBoPhuCapKeHoachNq104Service tlCanBoPhuCapKeHoachService,
            ITlBangLuongKeHoachNq104Service tlBangLuongKeHoachService,
            ITlQtChungTuChiTietKeHoachNq104Service tlQtChungTuChiTietKeHoachService,
            ITlQtChungTuChiTietNq104Service tlQtChungTuChiTietService,
            ITlQtChungTuNq104Service tlQtChungTuService,
            ITlDieuChinhQsKeHoachNq104Service tlDieuChinhQsKeHoachService,
            ITlBangLuongKeHoachBridgeNq104Service tlBangLuongBridgeService)
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
            _tlBangLuongBridgeService = tlBangLuongBridgeService;
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
            DonViItems = new ObservableCollection<TlDmDonViNq104Model>();
            DonViItems = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
            if (DonViItems != null && DonViItems.Count > 0)
            {
                SelectedDonViItems = DonViItems.FirstOrDefault(x => x.MaDonVi == Model.MaDonVi);
            }
        }

        private void LoadCongThucLuong()
        {
            var data = _tlDmCachTinhLuongChuanService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork).ToList();
            DsCongThucLuong = _mapper.Map<List<TlCachTinhLuongNq104Model>>(data);
        }

        private void LoadTenDsCnLuong()
        {
            if (SelectedDonViItems == null)
            {
                TenDs = string.Format("Danh sách lương kế hoạch năm {0} ", Model.Nam);
            }
            else
            {
                TenDs = string.Format("Danh sách lương kế hoạch năm {0} - {1} ", Model.Nam, SelectedDonViItems.TenDonVi);
            }
            OnPropertyChanged(TenDs);
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
                    Model.MaDonVi = SelectedDonViItems.MaDonVi;
                    Model.Nam = int.Parse(YearSelected.ValueItem);
                    Model.TenBangLuong = TenDs;
                    Model.MaCachTl = CachTinhLuong.CACH0;
                    Model.UserCreator = _sessionService.Current.Principal;
                    Model.DateCreated = DateTime.Now;
                    Model.Id = Guid.NewGuid();

                    _tlDsBangLuongKeHoachService.Add(_mapper.Map<TlDsBangLuongKeHoachNq104>(Model));

                    for (int i = 1; i <= 12; i++)
                    {
                        var lstCanBoData = _tlBangLuongKeHoachService.FindCbLuong(i, Model.Nam, Model.MaDonVi).Where(x => x.KhongLuong == false);
                        var lstCanBo = _mapper.Map<ObservableCollection<TlDmCanBoKeHoachNq104Model>>(lstCanBoData);
                        ObservableCollection<TlBangLuongKeHoachNq104Model> tlBangLuongThangModels = new ObservableCollection<TlBangLuongKeHoachNq104Model>();

                        foreach (var item in lstCanBo)
                        {
                            TlBangLuongThangNq104Model tlBangLuongThangModel = new TlBangLuongThangNq104Model();
                            var listCanBoPhuCap = _tlCanBoPhuCapKeHoachService.FindByMaCanBo(item.MaCanBo).FirstOrDefault();

                            var allowencesSaved = new List<AllowencePhuCapNq104Criteria>();
                            try
                            {
                                if (listCanBoPhuCap != null)
                                {
                                    var plainText = CompressExtension.DecompressFromBase64(listCanBoPhuCap.Data);
                                    allowencesSaved = JsonConvert.DeserializeObject<AllowenceCanBoNq104Criteria>(plainText).X.ToList();
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.Error(ex);
                            }

                            var allowencesUpdated = allowencesSaved.Select(x =>
                            {
                                x.A = x.A;
                                x.B = (x.A == PhuCap.NTN && x.B < 5) ? x.B = 0 : x.B;
                                x.C = x.C;
                                return x;
                            }).ToList();

                            if (allowencesUpdated.Any())
                            {
                                foreach (var congThucLuong in DsCongThucLuong)
                                {
                                    congThucLuong.Value = 0;
                                    congThucLuong.IsCalculated = false;
                                }
                                foreach (var phuCap in allowencesUpdated)
                                {
                                    var bangLuong = CreateBangLuongKeHoachModel(Model.Id, item, phuCap.A, phuCap.B);
                                    tlBangLuongThangModels.Add(bangLuong);
                                }
                                Dictionary<string, decimal> results = new Dictionary<string, decimal>();
                                foreach (var cachTinhLuong in DsCongThucLuong)
                                {
                                    results.Add(cachTinhLuong.MaCot, TinhLuong(allowencesUpdated, cachTinhLuong));
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
                                var pcTrichLuong = tlBangLuongThangModels.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.TRICHLUONG_TT) && x.MaCanBo == item.MaCanBo);
                                if (pcTrichLuong != null && pcTrichLuong.GiaTri != null)
                                {
                                    pcTrichLuong.GiaTri = Math.Round((decimal)pcTrichLuong.GiaTri / 1000) * 1000;
                                }
                            }
                        }

                        var dataSave = tlBangLuongThangModels.GroupBy(x => x.MaCanBo).Select(y =>
                        {
                            var phuCapJson = new JObject();
                            foreach (var item in y)
                            {
                                phuCapJson[item.MaPhuCap] = item.GiaTri;
                            }
                            return new
                            {
                                First = y.FirstOrDefault(),
                                Data = CompressExtension.CompressToBase64(phuCapJson.ToString()),
                            };
                        });

                        dataSave.ForAll(x => x.First.Data = x.Data);

                        var tlBangLuongKeHoachs = _mapper.Map<ObservableCollection<TlBangLuongKeHoachNq104>>(dataSave.Select(x => x.First));
                        _tlBangLuongKeHoachService.BulkInsert(tlBangLuongKeHoachs);
                    }
                    _tlBangLuongBridgeService.DataPreprocess(int.Parse(YearSelected.ValueItem), null, CachTinhLuong.CACH0);
                    TinhQuyetToanChungTuChiTiet();
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

        private void TinhQuyetToanChungTuChiTiet()
        {
            try
            {
                var _lstTlDieuChinhQsKeHoaches = _tlDieuChinhQsKeHoachService.FindData(Model.Nam, Model.MaDonVi).ToList();
                var lstSaveQtChungTuChiTiet = new List<TlQtChungTuChiTietKeHoachNq104Model>();
                for (int i = 1; i <= 12; i++)
                {
                    var chungTuChiTietQueries = _tlQtChungTuChiTietKeHoachService.GetDataByMonth(SelectedDonViItems.MaDonVi, i, int.Parse(YearSelected.ValueItem), int.Parse(YearSelected.ValueItem) - 1).ToList();
                    var lstChungTuChiTiet = _mapper.Map<ObservableCollection<TlQtChungTuChiTietKeHoachNq104Model>>(chungTuChiTietQueries).ToList();
                    lstChungTuChiTiet.Select(x =>
                    {
                        x.IdDonVi = SelectedDonViItems.MaDonVi;
                        x.TenDonVi = SelectedDonViItems.TenDonVi;
                        x.DateCreated = DateTime.Now;
                        x.UserCreator = _sessionService.Current.Principal;
                        return x;
                    }).ToList();
                    lstSaveQtChungTuChiTiet.AddRange(lstChungTuChiTiet);
                }

                var lstMap = lstSaveQtChungTuChiTiet.Where(x => ((x.TongCong != null || x.TongNamTruoc != null) && (x.TongCong != 0 || x.TongNamTruoc != 0) && x.BHangCha != true) || x.Thang == null).ToList();
                var lstSave = _mapper.Map<ObservableCollection<TlQtChungTuChiTietKeHoachNq104>>(lstMap);
                _tlQtChungTuChiTietKeHoachService.BulkInsert(lstSave);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private decimal TinhLuong(List<AllowencePhuCapNq104Criteria> tlBangLuongThangModel, TlCachTinhLuongNq104Model congThucLuong)
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
                            var property = tlBangLuongThangModel.Where(x => x.A == congThuc).FirstOrDefault();
                            if (property != null)
                            {
                                var pcCongChuan = tlBangLuongThangModel.FirstOrDefault(x => PhuCap.CONGCHUAN_SN.Equals(x.A));
                                if (property.C != null
                                    && pcCongChuan != null && pcCongChuan.B != null
                                    && pcCongChuan.B != 0
                                    && pcCongChuan.B > 0)
                                {
                                    property.B = (decimal)(property.B * property.C / pcCongChuan.B);
                                }
                                data.Add(congThuc, property.B ?? 0);
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

        private TlCachTinhLuongNq104Model CheckExitCongThuc(string congThuc)
        {
            return DsCongThucLuong.Where(x => x.MaCot == congThuc).FirstOrDefault();
        }

        private decimal ThueTN(decimal luongThuThue)
        {
            var data = _tlBangLuongKeHoachService.FindThue().ToList().OrderBy(x => x.ThuNhapTu);
            decimal tienThue = 0;
            decimal t = luongThuThue.Clone();
            var DmThuThue = _mapper.Map<List<TlDmThueThuNhapCaNhanNq104Model>>(data);
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

        private TlBangLuongKeHoachNq104Model CreateBangLuongKeHoachModel(Guid id, TlDmCanBoKeHoachNq104Model TlQtChungTuChiTietKeHoachModel, string maPhuCap, decimal? giaTri)
        {
            TlBangLuongKeHoachNq104Model model = new TlBangLuongKeHoachNq104Model();
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

            if (SelectedDonViItems == null)
            {
                messages.Add(string.Format(Resources.UnitNull));
            }
            if (TenDs.Equals(string.Empty))
            {
                messages.Add(string.Format(Resources.SalaryTableNameNull));
            }
            if (SelectedDonViItems != null)
            {
                TlDsBangLuongKeHoachNq104 tlDsCapNhapBangLuong = _tlDsBangLuongKeHoachService.FindByCondition(CachTinhLuong.CACH0, SelectedDonViItems.MaDonVi, (int)Model.Nam);
                if (tlDsCapNhapBangLuong != null)
                {
                    messages.Add(string.Format("Bảng lương năm {0} đơn vị {1} đã tồn tại!", YearSelected.ValueItem, SelectedDonViItems.TenDonVi));
                }
            }
            return string.Join(Environment.NewLine, messages);
        }
    }
}
