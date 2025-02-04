using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.RegularSettlement
{
    public class RegularDataIntertationViewModel : DetailViewModelBase<TlQtChungTuModel, TlQtChungTuChiTietModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private readonly ITlQtChungTuChiTietService _tlQtChungTuChiTietService;
        private readonly ITlQtChungTuService _tlQtChungTuService;
        private readonly ITlDmCanBoService _tlDmCanBoService;
        private readonly ITlQtChungTuChiTietGiaiThichService _tlQtChungTuChiTietGiaiThichService;

        public override string Name => "Quyết toán - Chứng từ chi tiết - Giải thích số liệu";
        public override Type ContentType => typeof(View.Salary.Settlement.RegularSettlement.RegularDataIntertation);

        public List<TlQtChungTuChiTietModel> itemsChungTuChiTiet;
        public List<TlQtChungTuModel> ItemsChungTu;
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public int Nam { get; set; }
        public int Thang { get; set; }
        public bool BIsSummary { get; set; }
        private static readonly List<string> _lstCachTinhLuong = new List<string>() { CachTinhLuong.CACH1, CachTinhLuong.CACH2 };
        public string ThangBC
            => Thang == 0 ? ThoiGian.CANAM :
            (Thang <= 12 ? Thang.ToString() :
            (Thang == (int)DateTimeExtension.TimeConst.Types.QUY_1 ? ThoiGian.QUY1 :
            (Thang == (int)DateTimeExtension.TimeConst.Types.QUY_2 ? ThoiGian.QUY2 :
            (Thang == (int)DateTimeExtension.TimeConst.Types.QUY_3 ? ThoiGian.QUY3 :
            ThoiGian.QUY4))));

        private TlQtChungTuChiTietGiaiThichModel _tlRegularDataIntertation;
        public TlQtChungTuChiTietGiaiThichModel TlRegularDataIntertation
        {
            get => _tlRegularDataIntertation;
            set => SetProperty(ref _tlRegularDataIntertation, value);
        }

        private int _selectedTab;
        public int SelectedTab
        {
            get => _selectedTab;
            set => SetProperty(ref _selectedTab, value);
        }


        public RegularDataIntertationViewModel(IMapper mapper,
            ISessionService sessionService,
            ITlQtChungTuChiTietService tlQtChungTuChiTietService,
            ITlQtChungTuService tlQtChungTuService,
            ITlDmCanBoService tlDmCanBoService,
            ILog logger,
            ITlQtChungTuChiTietGiaiThichService tlQtChungTuChiTietGiaiThichService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _tlQtChungTuChiTietService = tlQtChungTuChiTietService;
            _tlQtChungTuService = tlQtChungTuService;
            _tlDmCanBoService = tlDmCanBoService;
            _logger = logger;
            _tlQtChungTuChiTietGiaiThichService = tlQtChungTuChiTietGiaiThichService;
        }

        public override void Init()
        {
            base.Init();
            _selectedTab = 0;
            LoadDataChungTuChiTiet();
            LoadData();
        }

        private void LoadDataChungTuChiTiet()
        {
            string idChungTu = "";
            //idChungTu = Model.Id.ToString();
            if (BIsSummary) idChungTu = string.Join(",", ItemsChungTu.Select(x => x.STongHop));
            else idChungTu = string.Join(",", ItemsChungTu.Select(x => x.Id.ToString()));
            int nam = ItemsChungTu.Select(n => n.Nam).FirstOrDefault();
            var data = _tlQtChungTuChiTietService.GetDataGiaiThichBangSo(idChungTu, Nam, MaDonVi, BIsSummary, string.Format("{0},{1}", CachTinhLuong.CACH0, CachTinhLuong.CACH5)).ToList();
            itemsChungTuChiTiet = _mapper.Map<ObservableCollection<TlQtChungTuChiTietModel>>(data).ToList();
            CalculateData(itemsChungTuChiTiet);
        }

        private void LoadData()
        {
            try
            {
                var data = _tlQtChungTuChiTietGiaiThichService.FindByCondition(ThangBC, Nam, MaDonVi);
                if (data == null)
                {
                    TlRegularDataIntertation = new TlQtChungTuChiTietGiaiThichModel();
                    CalculateSettlementSalary();
                    CalculateSettlementSalaryBHXH();
                    CountRaQuanXuatNgu();
                    TlRegularDataIntertation.PropertyChanged += DetailModel_PropertyChanged;
                }
                else
                {
                    TlRegularDataIntertation = _mapper.Map<TlQtChungTuChiTietGiaiThichModel>(data);
                    CalculateSettlementSalary();
                    CountRaQuanXuatNgu();
                    TlRegularDataIntertation.PropertyChanged += DetailModel_PropertyChanged;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool IsCnvc(TlQtChungTuChiTietModel model)
        {
            if (model.MaCbCha.IsEmpty() || IsLdhd(model)) return false;
            return model.MaCbCha.Equals(MA_CAP_BAC.CNVC) || model.MaCbCha.Equals(MA_CAP_BAC.CNQP) || model.MaCbCha.Equals(MA_CAP_BAC.CCQP) || model.MaCbCha.Equals(MA_CAP_BAC.LDHD);
        }

        private bool IsLdhd(TlQtChungTuChiTietModel model)
        {
            if (model.MaCb.IsEmpty()) return false;
            var listMaCapBacLdhd = new List<string>() { "423", "425", "43" };
            return listMaCapBacLdhd.Contains(model.MaCb);
        }


        /// <summary>
        /// tính tiền lương xin quyết toán tháng này
        /// </summary>
        private void CalculateSettlementSalary()
        {
            //_tlRegularDataIntertation.FLuongSiQuan = GetDataFromSumWage(new List<string> { "1010000-010-011-6000-6001-10-00" });
            //_tlRegularDataIntertation.FPcSiQuan = GetDataFromSumWage(new List<string> { "1010000-010-011-6100-6101-10-00", "1010000-010-011-6100-6107-10-00",
            //    "1010000-010-011-6100-6124-10-00", "1010000-010-011-6100-6102-10-00", "1010000-010-011-6100-6103-10-00"});

            //_tlRegularDataIntertation.FLuongQNCN = GetDataFromSumWage(new List<string> { "1010000-010-011-6000-6001-20-00", "1010000-010-011-6000-6001-20-00-01" });
            //_tlRegularDataIntertation.FPcQNCN = GetDataFromSumWage(new List<string> { "1010000-010-011-6100-6101-20-00", "1010000-010-011-6100-6107-20-00",
            //    "1010000-010-011-6100-6124-20-00", "1010000-010-011-6100-6102-20-00", "1010000-010-011-6100-6103-20-00"});

            //_tlRegularDataIntertation.FLuongCNVC = GetDataFromSumWage(new List<string> { "1010000-010-011-6000-6001-30-00", "1010000-010-011-6000-6001-40-00", "1010000-010-011-6000-6001-70-00" });
            //_tlRegularDataIntertation.FPcCNVC = GetDataFromSumWage(new List<string> { "1010000-010-011-6100-6101-30-00", "1010000-010-011-6100-6107-30-00",
            //    "1010000-010-011-6100-6124-30-00", "1010000-010-011-6100-6102-30-00", "1010000-010-011-6100-6103-30-00",
            //    "1010000-010-011-6100-6101-40-00", "1010000-010-011-6100-6107-40-00", "1010000-010-011-6100-6124-40-00", "1010000-010-011-6100-6102-40-00", "1010000-010-011-6100-6103-40-00",
            //    "1010000-010-011-6100-6101-70-00", "1010000-010-011-6100-6107-70-00", "1010000-010-011-6100-6124-70-00", "1010000-010-011-6100-6102-70-00", "1010000-010-011-6100-6103-70-00"});

            //_tlRegularDataIntertation.FLuongHDLD = GetDataFromSumWage(new List<string> { "1010000-010-011-6000-6001-90-00" });
            //_tlRegularDataIntertation.FPcHDLD = GetDataFromSumWage(new List<string> { "1010000-010-011-6100-6101-90-00", "1010000-010-011-6100-6107-90-00",
            //    "1010000-010-011-6100-6124-90-00", "1010000-010-011-6100-6102-90-00", "1010000-010-011-6100-6103-90-00"});

            _tlRegularDataIntertation.FLuongSiQuan = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.SQ)).Sum(n => (double)(n.DieuChinh ?? 0));
            _tlRegularDataIntertation.FPcSiQuan = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.SQ)).Sum(n => (double)(n.DieuChinh ?? 0));

            _tlRegularDataIntertation.FLuongQNCN = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.QNCN)).Sum(n => (double)(n.DieuChinh ?? 0));
            _tlRegularDataIntertation.FPcQNCN = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.QNCN)).Sum(n => (double)(n.DieuChinh ?? 0));

            _tlRegularDataIntertation.FLuongCNVC = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && IsCnvc(n)).Sum(n => (double)(n.DieuChinh ?? 0));
            _tlRegularDataIntertation.FPcCNVC = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && IsCnvc(n)).Sum(n => (double)(n.DieuChinh ?? 0));

            _tlRegularDataIntertation.FLuongHDLD = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && IsLdhd(n)).Sum(n => (double)(n.DieuChinh ?? 0));
            _tlRegularDataIntertation.FPcHDLD = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && IsLdhd(n)).Sum(n => (double)(n.DieuChinh ?? 0));
        }

        private void CalculateSettlementSalaryBHXH()
        {
            var dataQuyetToan = _tlQtChungTuChiTietService.GetQuyetToanChiTietBHXH(MaDonVi, int.Parse(ThangBC), Nam);
            if (dataQuyetToan != null)
            {
                var dataQuyetToanModel = _mapper.Map<ObservableCollection<TlQtChungTuChiTietModel>>(dataQuyetToan).ToList();

                _tlRegularDataIntertation.FLuongBhxhSiQuanTru = dataQuyetToanModel.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.SQ) && _lstCachTinhLuong.Contains(n.MaCachTl)).Sum(n => (double)(n.DieuChinh ?? 0));
                _tlRegularDataIntertation.FPhuCapBhxhSiQuanTru = dataQuyetToanModel.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.SQ) && _lstCachTinhLuong.Contains(n.MaCachTl)).Sum(n => (double)(n.DieuChinh ?? 0));

                _tlRegularDataIntertation.FLuongBhxhQncnTru = dataQuyetToanModel.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.QNCN) && _lstCachTinhLuong.Contains(n.MaCachTl)).Sum(n => (double)(n.DieuChinh ?? 0));
                _tlRegularDataIntertation.FPhuCapBhxhQncnTru = dataQuyetToanModel.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.QNCN) && _lstCachTinhLuong.Contains(n.MaCachTl)).Sum(n => (double)(n.DieuChinh ?? 0));

                _tlRegularDataIntertation.FLuongBhxhCnvqpTru = dataQuyetToanModel.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && IsCnvc(n) && _lstCachTinhLuong.Contains(n.MaCachTl)).Sum(n => (double)(n.DieuChinh ?? 0));
                _tlRegularDataIntertation.FPhuCapBhxhCnvqpTru = dataQuyetToanModel.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && IsCnvc(n) && _lstCachTinhLuong.Contains(n.MaCachTl)).Sum(n => (double)(n.DieuChinh ?? 0));

                _tlRegularDataIntertation.FLuongBhxhHdTru = dataQuyetToanModel.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && IsLdhd(n) && _lstCachTinhLuong.Contains(n.MaCachTl)).Sum(n => (double)(n.DieuChinh ?? 0));
                _tlRegularDataIntertation.FPhuCapBhxhHdTru = dataQuyetToanModel.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && IsLdhd(n) && _lstCachTinhLuong.Contains(n.MaCachTl)).Sum(n => (double)(n.DieuChinh ?? 0));
            }
        }

        private void CalculateData(List<TlQtChungTuChiTietModel> lstQtChungTuChiTiet)
        {
            lstQtChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TongCong = 0;
                    x.DieuChinh = 0;
                    return x;
                }).ToList();
            var temp = lstQtChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false) && x.TongCong != null && x.TongCong != 0);
            foreach (var item in temp)
            {
                CalculateParent(item.MlnsIdParent, item, lstQtChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, TlQtChungTuChiTietModel item, List<TlQtChungTuChiTietModel> lstQtChungTuChiTiet)
        {
            var model = lstQtChungTuChiTiet.FirstOrDefault(x => x.MlnsId == idParent);
            if (model == null) return;
            model.TongCong += item.TongCong;
            model.DieuChinh += item.DieuChinh;
            CalculateParent(model.MlnsIdParent, item, lstQtChungTuChiTiet);
        }

        /// <summary>
        /// tính lương theo ngạch bậc
        /// </summary>
        private double GetDataFromSumWage(List<string> lstMaNgach)
        {
            double luong = 0;
            foreach (var item in lstMaNgach)
            {
                var gt = itemsChungTuChiTiet.FirstOrDefault(x => x.XauNoiMa.Equals(item));
                if (gt != null && gt.DieuChinh != null)
                {
                    luong += (double)(gt.DieuChinh ?? 0);
                }
                else
                {
                    luong += 0;
                }
            }
            return luong;
        }

        private void CountRaQuanXuatNgu()
        {
            var predicate = PredicateBuilder.True<TlDmCanBo>();
            predicate = predicate.And(x => x.Nam == Nam);
            if (Thang > 0 && Thang <= 12)
            {
                predicate = predicate.And(x => x.Thang == Thang);
            }
            else if (Thang > 1)
            {
                switch (Thang)
                {
                    case (int)DateTimeExtension.TimeConst.Types.QUY_1:
                        predicate = predicate.And(x => x.Thang == 1 || x.Thang == 2 || x.Thang == 3);
                        break;
                    case (int)DateTimeExtension.TimeConst.Types.QUY_2:
                        predicate = predicate.And(x => x.Thang == 4 || x.Thang == 5 || x.Thang == 6);
                        break;
                    case (int)DateTimeExtension.TimeConst.Types.QUY_3:
                        predicate = predicate.And(x => x.Thang == 7 || x.Thang == 8 || x.Thang == 9);
                        break;
                    case (int)DateTimeExtension.TimeConst.Types.QUY_4:
                        predicate = predicate.And(x => x.Thang == 10 || x.Thang == 11 || x.Thang == 12);
                        break;
                }
            }
            predicate = predicate.And(x => MaDonVi.Equals(x.Parent));
            predicate = predicate.And(x => x.ITrangThai == 3);
            var listCanBo = _tlDmCanBoService.FindByCondition(predicate).ToList();
            List<string> maTangGiam = new List<string>() { "310", "320", "330" };
            foreach (var item in maTangGiam)
            {
                var listData = listCanBo.Where(x => item.Equals(x.MaTangGiam));
                // xuất ngũ
                _tlRegularDataIntertation.XuatNguSQ = listData.Count(x => x.MaCb.StartsWith("1"));
                _tlRegularDataIntertation.XuatNguQNCN = listData.Count(x => x.MaCb.StartsWith("2"));
                _tlRegularDataIntertation.XuatNguCNVC = listData.Count(x => x.MaCb.StartsWith("4"));
                _tlRegularDataIntertation.XuatNguHSQ = listData.Count(x => x.MaCb.StartsWith("0"));

                // về hưu
                _tlRegularDataIntertation.HuuSQ = listData.Count(x => x.MaCb.StartsWith("1"));
                _tlRegularDataIntertation.HuuQNCN = listData.Count(x => x.MaCb.StartsWith("2"));
                _tlRegularDataIntertation.HuuCNVC = listData.Count(x => x.MaCb.StartsWith("4"));
                _tlRegularDataIntertation.HuuHSQ = listData.Count(x => x.MaCb.StartsWith("0"));

                // thôi việc
                _tlRegularDataIntertation.ThoiViecHSQ = listData.Count(x => x.MaCb.StartsWith("1"));
                _tlRegularDataIntertation.ThoiViecQNCN = listData.Count(x => x.MaCb.StartsWith("2"));
                _tlRegularDataIntertation.ThoiViecCNVC = listData.Count(x => x.MaCb.StartsWith("4"));
                _tlRegularDataIntertation.ThoiViecHSQ = listData.Count(x => x.MaCb.StartsWith("0"));
            }
            OnPropertyChanged(nameof(TlRegularDataIntertation));
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlQtChungTuChiTietGiaiThichModel item = (TlQtChungTuChiTietGiaiThichModel)sender;
            item.IsModified = true;
            OnPropertyChanged(nameof(TlQtChungTuChiTietGiaiThichModel));
        }

        public override void OnSave(object obj)
        {
            if (TlRegularDataIntertation.Id == null || TlRegularDataIntertation.Id == Guid.Empty)
            {
                TlRegularDataIntertation.Id = new Guid();
                TlRegularDataIntertation.IIdQtChungTu = Model.Id;
                TlRegularDataIntertation.IThang = Thang;
                TlRegularDataIntertation.INam = Nam;
                TlRegularDataIntertation.IMaDonVi = MaDonVi;
                TlRegularDataIntertation.DNgayTao = DateTime.Now;
                TlRegularDataIntertation.SNguoiTao = _sessionService.Current.Principal;
                TlRegularDataIntertation.PropertyChanged += DetailModel_PropertyChanged;
                var entity = _mapper.Map<TlQtChungTuChiTietGiaiThich>(TlRegularDataIntertation);
                _tlQtChungTuChiTietGiaiThichService.Add(entity);
            }
            else
            {
                TlRegularDataIntertation.DNgaySua = DateTime.Now;
                TlRegularDataIntertation.SNgaySua = _sessionService.Current.Principal;
                TlRegularDataIntertation.PropertyChanged += DetailModel_PropertyChanged;
                var entity = _mapper.Map<TlQtChungTuChiTietGiaiThich>(TlRegularDataIntertation);
                _tlQtChungTuChiTietGiaiThichService.Update(entity);
            }
            Window window = obj as Window;
            window.Close();
        }

        public void OnSaveRegularDataIntertationViewModel()
        {
            if (TlRegularDataIntertation.Id == null || TlRegularDataIntertation.Id == Guid.Empty)
            {
                TlRegularDataIntertation.Id = new Guid();
                TlRegularDataIntertation.IIdQtChungTu = Model.Id;
                TlRegularDataIntertation.IThang = Thang;
                TlRegularDataIntertation.INam = Nam;
                TlRegularDataIntertation.IMaDonVi = MaDonVi;
                TlRegularDataIntertation.DNgayTao = DateTime.Now;
                TlRegularDataIntertation.SNguoiTao = _sessionService.Current.Principal;
                TlRegularDataIntertation.PropertyChanged += DetailModel_PropertyChanged;
                var entity = _mapper.Map<TlQtChungTuChiTietGiaiThich>(TlRegularDataIntertation);
                _tlQtChungTuChiTietGiaiThichService.Add(entity);
            }
            else
            {
                TlRegularDataIntertation.DNgaySua = DateTime.Now;
                TlRegularDataIntertation.SNgaySua = _sessionService.Current.Principal;
                TlRegularDataIntertation.PropertyChanged += DetailModel_PropertyChanged;
                var entity = _mapper.Map<TlQtChungTuChiTietGiaiThich>(TlRegularDataIntertation);
                _tlQtChungTuChiTietGiaiThichService.Update(entity);
            }
        }

        public override void OnClose(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}
