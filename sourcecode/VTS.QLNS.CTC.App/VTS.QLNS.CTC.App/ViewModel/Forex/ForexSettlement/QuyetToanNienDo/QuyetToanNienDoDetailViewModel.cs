using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.QuyetToanNienDo;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo.PrintDialog;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo
{
    public class QuyetToanNienDoDetailViewModel : DetailViewModelBase<NhQtQuyetToanNienDoModel, NhQtQuyetToanNienDoChiTietModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhQtQuyetToanNienDoChiTietService _service;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly IExportService _exportService;
        protected readonly INhDmTiGiaService _nhDmTiGiaService;
        protected readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        protected readonly INhThTongHopService _nhThTongHopService;

        public QuyetToanNienDoPrintDialogViewModel QuyetToanNienDoPrintDialogViewModel { get; set; }

        private SessionInfo _sessionInfo;
        private List<NsMucLucNganSach> _rootDataMlns;

        public override string Name => "Đề nghị quyết toán niên độ";
        public override string Title => "Chi tiết đề nghị quyết toán niên độ";
        public override string Description => "Chi tiết đề nghị quyết toán niên độ";
        public override Type ContentType => typeof(QuyetToanNienDoDetail);
        public bool IsDetail { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();

        private ObservableCollection<NhDaHopDongModel> _itemsHopDong;
        public ObservableCollection<NhDaHopDongModel> ItemsHopDong
        {
            get => _itemsHopDong;
            set => SetProperty(ref _itemsHopDong, value);
        }

        private bool _isAddOrUpdate;
        public bool IsAddOrUpdate
        {
            get => _isAddOrUpdate;
            set => SetProperty(ref _isAddOrUpdate, value);
        }

        public RelayCommand PrintCommand { get; }

        public QuyetToanNienDoDetailViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INhQtQuyetToanNienDoChiTietService service,
            INsMucLucNganSachService nsMucLucNganSachService,
            INhDaHopDongService nhDaHopDongService,
            IExportService exportService,
            INhDmTiGiaService nhDmTiGiaService,
            INhThTongHopService nhThTongHopService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            QuyetToanNienDoPrintDialogViewModel quyetToanNienDoPrintDialogViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _service = service;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _nhDaHopDongService = nhDaHopDongService;
            _exportService = exportService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            _nhThTongHopService = nhThTongHopService;

            QuyetToanNienDoPrintDialogViewModel = quyetToanNienDoPrintDialogViewModel;
            PrintCommand = new RelayCommand(obj => OnPrint((ReportNhQuyetToanNienDoEnum)obj));
        }

        public override void Init()
        {
            LoadMucLucNganSach();
            LoadHopDong();
            LoadData();
        }

        private void LoadMucLucNganSach()
        {
            if (Model.INamKeHoach.HasValue)
            {
                var predicate = PredicateBuilder.True<NsMucLucNganSach>();
                predicate = predicate.And(x => x.NamLamViec == Model.INamKeHoach.Value);
                predicate = predicate.And(x => string.IsNullOrEmpty(x.Tng));
                predicate = predicate.And(x => string.IsNullOrEmpty(x.Tng1));
                predicate = predicate.And(x => string.IsNullOrEmpty(x.Tng2));
                predicate = predicate.And(x => string.IsNullOrEmpty(x.Tng3));
                _rootDataMlns = _nsMucLucNganSachService.FindByCondition(predicate).ToList();
            }
        }

        private void LoadHopDong()
        {
            var predicate = PredicateBuilder.True<NhDaHopDong>();
            predicate = predicate.And(x => x.IIdKhTongTheNhiemVuChiId != null);
            var data = _nhDaHopDongService.FindByCondition(predicate);
            ItemsHopDong = _mapper.Map<ObservableCollection<NhDaHopDongModel>>(data);
        }

        public override void LoadData(params object[] args)
        {
            Items = new ObservableCollection<NhQtQuyetToanNienDoChiTietModel>();
            IsAddOrUpdate = _service.checkListAddOrUpdateQTNDChiTiet(Model.Id, Model.IIdDonViId, Model.INamKeHoach, null, null);
            var data = _service.getListQTNDDetailChiTiet(Model.Id, Model.IIdDonViId, Model.INamKeHoach, null, null);
            Items = _mapper.Map<ObservableCollection<NhQtQuyetToanNienDoChiTietModel>>(data);
            foreach (var item in Items.OrderByDescending(x => x.SLevel))
            {
                if (item.SLevel == "0" || item.SLevel == "1" || item.SLevel == "2")
                {
                    item.IsHangCha = true;
                    item.FQtKinhPhiDuocCapTongSoUsd = item.FQtKinhPhiDuocCapNamTruocChuyenSangUsd.GetValueOrDefault(0) + item.FQtKinhPhiDuocCapNamNayUsd.GetValueOrDefault(0);
                    item.FQtKinhPhiDuocCapTongSoVnd = item.FQtKinhPhiDuocCapNamTruocChuyenSangVnd.GetValueOrDefault(0) + item.FQtKinhPhiDuocCapNamNayVnd.GetValueOrDefault(0);
                    item.FThuaThieuKinhPhiTrongNamUsd = item.FQtKinhPhiDuocCapTongSoUsd.GetValueOrDefault(0) - item.FDeNghiQtNamNayUsd.GetValueOrDefault(0) - item.FDeNghiChuyenNamSauUsd.GetValueOrDefault(0);
                    item.FThuaThieuKinhPhiTrongNamVnd = item.FQtKinhPhiDuocCapTongSoVnd.GetValueOrDefault(0) - item.FDeNghiQtNamNayVnd.GetValueOrDefault(0) - item.FDeNghiChuyenNamSauVnd.GetValueOrDefault(0);
                    item.FKeHoachChuaGiaiNganUsd = item.FKeHoachBqpUsd.GetValueOrDefault(0) - item.FQtKinhPhiDuocCapTongSoUsd.GetValueOrDefault(0);
                    item.FKeHoachChuaGiaiNganVnd = item.FKeHoachBqpVnd.GetValueOrDefault(0) - item.FQtKinhPhiDuocCapTongSoVnd.GetValueOrDefault(0);

                } 
                else if (item.SLevel == "3")
                {
                    item.FQtKinhPhiDuocCapTongSoUsd = item.FQtKinhPhiDuocCapNamTruocChuyenSangUsd.GetValueOrDefault(0) + item.FQtKinhPhiDuocCapNamNayUsd.GetValueOrDefault(0);
                    item.FQtKinhPhiDuocCapTongSoVnd = item.FQtKinhPhiDuocCapNamTruocChuyenSangVnd.GetValueOrDefault(0) + item.FQtKinhPhiDuocCapNamNayVnd.GetValueOrDefault(0);
                    item.FThuaThieuKinhPhiTrongNamUsd = item.FQtKinhPhiDuocCapTongSoUsd.GetValueOrDefault(0) - item.FDeNghiQtNamNayUsd.GetValueOrDefault(0) - item.FDeNghiChuyenNamSauUsd.GetValueOrDefault(0);
                    item.FThuaThieuKinhPhiTrongNamVnd = item.FQtKinhPhiDuocCapTongSoVnd.GetValueOrDefault(0) - item.FDeNghiQtNamNayVnd.GetValueOrDefault(0) - item.FDeNghiChuyenNamSauVnd.GetValueOrDefault(0);
                    item.FKeHoachChuaGiaiNganUsd = item.FKeHoachBqpUsd.GetValueOrDefault(0) - item.FQtKinhPhiDuocCapTongSoUsd.GetValueOrDefault(0);
                    item.FKeHoachChuaGiaiNganVnd = item.FKeHoachBqpVnd.GetValueOrDefault(0) - item.FQtKinhPhiDuocCapTongSoVnd.GetValueOrDefault(0);
                }
                //var item_Level = new List<NhQtQuyetToanNienDoChiTietModel>();
                //if (item.SLevel == "0")
                //{
                //    item_Level = Items.Where(x => x.SLevel == "4" && x.IIdKhttNhiemVuChiId == item.IIdKhttNhiemVuChiId).ToList();
                //    item.IsHangCha = true;
                //}
                //else if (item.SLevel == "1")
                //{
                //    item_Level = Items.Where(x => x.SLevel == "4" && x.IID_DuAnID == item.IID_DuAnID && x.IIdKhttNhiemVuChiId == item.IIdKhttNhiemVuChiId).ToList();
                //    if (item.STenNoiDungChi == "Chi hợp đồng")
                //    {
                //        item_Level = Items.Where(x => x.SLevel == "4" && x.IIdKhttNhiemVuChiId == item.IIdKhttNhiemVuChiId && x.IID_DuAnID == null && x.IIdHopDongId != null).ToList();
                //    }
                //    if (item.STenNoiDungChi == "Chi khác")
                //    {
                //        item_Level = Items.Where(x => x.SLevel == "2" && x.IIdKhttNhiemVuChiId == item.IIdKhttNhiemVuChiId && x.IID_DuAnID == null && x.IIdHopDongId == null).ToList();
                //    }
                //    item.IsHangCha = true;
                //}
                //else if (item.SLevel == "2")
                //{
                //    item_Level = Items.Where(x => x.SLevel == "4" && x.IID_DuAnID == item.IID_DuAnID &&x.ILoaiNoiDungChi == item.ILoaiNoiDungChi && x.IIdKhttNhiemVuChiId == item.IIdKhttNhiemVuChiId && x.IsChiKhac == item.IsChiKhac).ToList();                    
                //    item.IsHangCha = true;
                //}
                //else if (item.SLevel == "3")
                //{
                //    item_Level = Items.Where(x => x.SLevel == "4" && x.IID_DuAnID == item.IID_DuAnID && x.ILoaiNoiDungChi == item.ILoaiNoiDungChi 
                //                                                       && x.IIdHopDongId == item.IIdHopDongId && x.IIdKhttNhiemVuChiId == item.IIdKhttNhiemVuChiId 
                //                                                       && x.IsChiKhac == item.IsChiKhac).ToList();

                //}
                //else if (item.SLevel == "4")
                //{
                //    double? fQtKinhPhiDuyetCacNamTruocVnd = 0, fQtKinhPhiDuyetCacNamTruocUsd = 0,
                //            fQtKinhPhiDuocCapNamTruocChuyenSangVnd = 0, fQtKinhPhiDuocCapNamTruocChuyenSangUsd = 0,
                //            fQtKinhPhiDuocCapNamNayVnd = 0, fQtKinhPhiDuocCapNamNayUsd = 0,
                //            fDeNghiQtNamNayVnd = 0, fDeNghiQtNamNayUsd = 0,
                //            fDeNghiChuyenNamSauVnd = 0, fDeNghiChuyenNamSauUsd = 0;
                //    fQtKinhPhiDuyetCacNamTruocVnd += item.FQtKinhPhiDuyetCacNamTruocVnd == null ? 0 : item.FQtKinhPhiDuyetCacNamTruocVnd;
                //    fQtKinhPhiDuyetCacNamTruocUsd += item.FQtKinhPhiDuyetCacNamTruocUsd == null ? 0 : item.FQtKinhPhiDuyetCacNamTruocUsd;
                //    fQtKinhPhiDuocCapNamTruocChuyenSangVnd += item.FQtKinhPhiDuocCapNamTruocChuyenSangVnd == null ? 0 : item.FQtKinhPhiDuocCapNamTruocChuyenSangVnd;
                //    fQtKinhPhiDuocCapNamTruocChuyenSangUsd += item.FQtKinhPhiDuocCapNamTruocChuyenSangUsd == null ? 0 : item.FQtKinhPhiDuocCapNamTruocChuyenSangUsd;
                //    fQtKinhPhiDuocCapNamNayVnd += item.FQtKinhPhiDuocCapNamNayVnd == null ? 0 : item.FQtKinhPhiDuocCapNamNayVnd;
                //    fQtKinhPhiDuocCapNamNayUsd += item.FQtKinhPhiDuocCapNamNayUsd == null ? 0 : item.FQtKinhPhiDuocCapNamNayUsd;
                //    item.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapNamTruocChuyenSangVnd + fQtKinhPhiDuocCapNamNayVnd - fDeNghiQtNamNayVnd - fDeNghiChuyenNamSauVnd) == 0 ? null : (fQtKinhPhiDuocCapNamTruocChuyenSangVnd + fQtKinhPhiDuocCapNamNayVnd - fDeNghiQtNamNayVnd - fDeNghiChuyenNamSauVnd);
                //    item.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapNamTruocChuyenSangUsd + fQtKinhPhiDuocCapNamNayUsd - fDeNghiQtNamNayUsd - fDeNghiChuyenNamSauUsd) == 0 ? null : (fQtKinhPhiDuocCapNamTruocChuyenSangUsd + fQtKinhPhiDuocCapNamNayUsd - fDeNghiQtNamNayUsd - fDeNghiChuyenNamSauUsd);

                //}
                //if (item.SLevel != "4" && item_Level != null)
                //{
                //    double? fQtKinhPhiDuyetCacNamTruocVnd = 0, fQtKinhPhiDuyetCacNamTruocUsd = 0,
                //            fQtKinhPhiDuocCapNamTruocChuyenSangVnd = 0, fQtKinhPhiDuocCapNamTruocChuyenSangUsd = 0,
                //            fQtKinhPhiDuocCapNamNayVnd = 0, fQtKinhPhiDuocCapNamNayUsd = 0,
                //            fDeNghiQtNamNayVnd = 0, fDeNghiQtNamNayUsd = 0,
                //            fDeNghiChuyenNamSauVnd = 0, fDeNghiChuyenNamSauUsd = 0,
                //            fThuaNopNsnnVnd = 0, fThuaNopNsnnUsd = 0,
                //            fLuyKeKinhPhiDuocCapVnd = 0, fLuyKeKinhPhiDuocCapUsd = 0;
                //    foreach (var i in item_Level)
                //    {
                //        fQtKinhPhiDuyetCacNamTruocVnd += i.FQtKinhPhiDuyetCacNamTruocVnd == null ? 0 : i.FQtKinhPhiDuyetCacNamTruocVnd;
                //        fQtKinhPhiDuyetCacNamTruocUsd += i.FQtKinhPhiDuyetCacNamTruocUsd == null ? 0 : i.FQtKinhPhiDuyetCacNamTruocUsd;
                //        fQtKinhPhiDuocCapNamTruocChuyenSangVnd += i.FQtKinhPhiDuocCapNamTruocChuyenSangVnd == null ? 0 : i.FQtKinhPhiDuocCapNamTruocChuyenSangVnd;
                //        fQtKinhPhiDuocCapNamTruocChuyenSangUsd += i.FQtKinhPhiDuocCapNamTruocChuyenSangUsd == null ? 0 : i.FQtKinhPhiDuocCapNamTruocChuyenSangUsd;
                //        fQtKinhPhiDuocCapNamNayVnd += i.FQtKinhPhiDuocCapNamNayVnd == null ? 0 : i.FQtKinhPhiDuocCapNamNayVnd;
                //        fQtKinhPhiDuocCapNamNayUsd += i.FQtKinhPhiDuocCapNamNayUsd == null ? 0 : i.FQtKinhPhiDuocCapNamNayUsd;
                //        fDeNghiQtNamNayVnd += i.FDeNghiQtNamNayVnd == null ? 0 : i.FDeNghiQtNamNayVnd;
                //        fDeNghiQtNamNayUsd += i.FDeNghiQtNamNayUsd == null ? 0 : i.FDeNghiQtNamNayUsd;
                //        fDeNghiChuyenNamSauVnd += i.FDeNghiChuyenNamSauVnd == null ? 0 : i.FDeNghiChuyenNamSauVnd;
                //        fDeNghiChuyenNamSauUsd += i.FDeNghiChuyenNamSauUsd == null ? 0 : i.FDeNghiChuyenNamSauUsd;
                //        fThuaNopNsnnVnd += i.FThuaNopNsnnVnd == null ? 0 : i.FThuaNopNsnnVnd;
                //        fThuaNopNsnnUsd += i.FThuaNopNsnnUsd == null ? 0 : i.FThuaNopNsnnUsd;
                //        fLuyKeKinhPhiDuocCapVnd += i.FLuyKeKinhPhiDuocCapVnd == null ? 0 : i.FLuyKeKinhPhiDuocCapVnd;
                //        fLuyKeKinhPhiDuocCapUsd += i.FLuyKeKinhPhiDuocCapUsd == null ? 0 : i.FLuyKeKinhPhiDuocCapUsd;
                //    }
                //    item.FQtKinhPhiDuyetCacNamTruocVnd = fQtKinhPhiDuyetCacNamTruocVnd == 0 ? null : fQtKinhPhiDuyetCacNamTruocVnd;
                //    item.FQtKinhPhiDuyetCacNamTruocUsd = fQtKinhPhiDuyetCacNamTruocUsd == 0 ? null : fQtKinhPhiDuyetCacNamTruocUsd;
                //    item.FQtKinhPhiDuocCapNamTruocChuyenSangVnd = fQtKinhPhiDuocCapNamTruocChuyenSangVnd == 0 ? null : fQtKinhPhiDuocCapNamTruocChuyenSangVnd;
                //    item.FQtKinhPhiDuocCapNamTruocChuyenSangUsd = fQtKinhPhiDuocCapNamTruocChuyenSangUsd == 0 ? null : fQtKinhPhiDuocCapNamTruocChuyenSangUsd;
                //    item.FQtKinhPhiDuocCapNamNayVnd = fQtKinhPhiDuocCapNamNayVnd == 0 ? null : fQtKinhPhiDuocCapNamNayVnd;
                //    item.FQtKinhPhiDuocCapNamNayUsd = fQtKinhPhiDuocCapNamNayUsd == 0 ? null : fQtKinhPhiDuocCapNamNayUsd;
                //    item.FQtKinhPhiDuocCapTongSoVnd = (fQtKinhPhiDuocCapNamTruocChuyenSangVnd + fQtKinhPhiDuocCapNamNayVnd) == 0 ? null : (fQtKinhPhiDuocCapNamTruocChuyenSangVnd + fQtKinhPhiDuocCapNamNayVnd);
                //    item.FQtKinhPhiDuocCapTongSoUsd = (fQtKinhPhiDuocCapNamTruocChuyenSangUsd + fQtKinhPhiDuocCapNamNayUsd) == 0 ? null : (fQtKinhPhiDuocCapNamTruocChuyenSangUsd + fQtKinhPhiDuocCapNamNayUsd);
                //    item.FDeNghiQtNamNayVnd = fDeNghiQtNamNayVnd == 0 ? null : fDeNghiQtNamNayVnd;
                //    item.FDeNghiQtNamNayUsd = fDeNghiQtNamNayUsd == 0 ? null : fDeNghiQtNamNayUsd;
                //    item.FDeNghiChuyenNamSauVnd = fDeNghiChuyenNamSauVnd == 0 ? null : fDeNghiChuyenNamSauVnd;
                //    item.FDeNghiChuyenNamSauUsd = fDeNghiChuyenNamSauUsd == 0 ? null : fDeNghiChuyenNamSauUsd;
                //    item.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapNamTruocChuyenSangVnd + fQtKinhPhiDuocCapNamNayVnd - fDeNghiQtNamNayVnd - fDeNghiChuyenNamSauVnd) == 0 ? null : (fQtKinhPhiDuocCapNamTruocChuyenSangVnd + fQtKinhPhiDuocCapNamNayVnd - fDeNghiQtNamNayVnd - fDeNghiChuyenNamSauVnd);
                //    item.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapNamTruocChuyenSangUsd + fQtKinhPhiDuocCapNamNayUsd - fDeNghiQtNamNayUsd - fDeNghiChuyenNamSauUsd) == 0 ? null : (fQtKinhPhiDuocCapNamTruocChuyenSangUsd + fQtKinhPhiDuocCapNamNayUsd - fDeNghiQtNamNayUsd - fDeNghiChuyenNamSauUsd);
                //    item.FThuaNopNsnnVnd = fThuaNopNsnnVnd == 0 ? null : fThuaNopNsnnVnd;
                //    item.FThuaNopNsnnUsd = fThuaNopNsnnUsd == 0 ? null : fThuaNopNsnnUsd;
                //    item.FLuyKeKinhPhiDuocCapVnd = fLuyKeKinhPhiDuocCapVnd == 0 ? null : fLuyKeKinhPhiDuocCapVnd;
                //    item.FLuyKeKinhPhiDuocCapUsd = fLuyKeKinhPhiDuocCapUsd == 0 ? null : fLuyKeKinhPhiDuocCapUsd;
                //}
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnAdd()
        {
            int currentRow = -1;
            if (!Items.IsEmpty())
            {
                if (SelectedItem != null)
                {
                    currentRow = Items.IndexOf(SelectedItem);
                }
            }

            NhQtQuyetToanNienDoChiTietModel targetItem = new NhQtQuyetToanNienDoChiTietModel();
            targetItem.Id = Guid.NewGuid();
            targetItem.IIdQuyetToanNienDoId = Model.Id;
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            SetDataMucLucNganSach(targetItem, string.Empty);
            targetItem.PropertyChanged += DetailModel_PropertyChanged;

            Items.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
        }

        public override void OnSave()
        {
            if (!Validate()) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                var Items_Save = Items.Where(x => x.IsData == true).ToList();
                foreach (var item in Items_Save)
                {
                    if (item.Id == new Guid()) item.IsAdded = true; else item.IsModified = true;
                    item.IIdQuyetToanNienDoId = Model.Id;
                    var itemparent = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == item.IIdHopDongId && x.IID_DuAnID == item.IID_DuAnID && x.IIdKhttNhiemVuChiId == item.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == item.ILoaiNoiDungChi).FirstOrDefault();
                    item.FQtKinhPhiDuyetCacNamTruocUsd = itemparent.FQtKinhPhiDuyetCacNamTruocUsd;
                    item.FQtKinhPhiDuyetCacNamTruocVnd = itemparent.FQtKinhPhiDuyetCacNamTruocVnd;
                    item.FLuyKeKinhPhiDuocCapUsd = itemparent.FLuyKeKinhPhiDuocCapUsd;
                    item.FLuyKeKinhPhiDuocCapVnd = itemparent.FLuyKeKinhPhiDuocCapVnd;
                    var itemparentNVC = Items.Where(x => x.SLevel == "0" && x.IIdKhttNhiemVuChiId == item.IIdKhttNhiemVuChiId).FirstOrDefault();
                    item.FKeHoachBqpUsd = itemparentNVC.FKeHoachBqpUsd;
                    item.FKeHoachTtcpUsd = itemparentNVC.FKeHoachTtcpUsd;
                    if (item.SLevel == "3")
                    {
                        item.FKeHoachTtcpUsd = Items.Where(x => x.IIdKhttNhiemVuChiId == item.IIdKhttNhiemVuChiId && x.IIdQuyetToanNienDoId == null).FirstOrDefault().FKeHoachTtcpUsd;
                        item.FKeHoachBqpUsd = Items.Where(x => x.IIdKhttNhiemVuChiId == item.IIdKhttNhiemVuChiId && x.IIdQuyetToanNienDoId == null).FirstOrDefault().FKeHoachBqpUsd;
                    }
                    if (item.BIsSaveTongHop == true)
                    {
                        item.FThuaThieuKinhPhiTrongNamUsd = (item.FQtKinhPhiDuocCapTongSoUsd ?? 0) - (item.FDeNghiQtNamNayUsd ?? 0) - (item.FDeNghiChuyenNamSauUsd ?? 0);
                        item.FThuaThieuKinhPhiTrongNamVnd = (item.FQtKinhPhiDuocCapTongSoVnd ?? 0) - (item.FDeNghiQtNamNayVnd ?? 0) - (item.FDeNghiChuyenNamSauVnd ?? 0);
                    }
                    item.IIdMlnsId = item.IIdMucLucNganSachId;
                }
                var entities = _mapper.Map<List<NhQtQuyetToanNienDoChiTiet>>(Items_Save);


                _service.AddOrUpdate(entities);
                //_nhThTongHopService.InsertNHTongHop_Tang("QUYET_TOAN", IsAddOrUpdate ? 2 : 1, Model.Id);
                //_nhThTongHopService.InsertNHTongHop_Giam("QUYET_TOAN", IsAddOrUpdate ? 2 : 1, Model.Id);
                //_nhThTongHopService.InsertNHTongHop_Giam("QTND", IsAddOrUpdate ? 2 : 1, Model.Id);
            }, (s, e) =>
            {
                IsLoading = false;

                if (e.Error == null)
                {
                    // Reload data

                    LoadData();

                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }

        private void OnPrint(ReportNhQuyetToanNienDoEnum reportType)
        {
            QuyetToanNienDoPrintDialogViewModel.Model = Model;
            QuyetToanNienDoPrintDialogViewModel.Init();
            QuyetToanNienDoPrintDialogViewModel.ShowDialog();

        }

        public override void OnClosing()
        {
            // Clear items
            if (!Items.IsEmpty()) Items.Clear();
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhQtQuyetToanNienDoChiTietModel objSender = (NhQtQuyetToanNienDoChiTietModel)sender;
            objSender.PropertyChanged -= DetailModel_PropertyChanged;
            var dmTyGia = _nhDmTiGiaService.FindById(objSender.IIdTiGiaId.GetValueOrDefault());
            var listTiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(dmTyGia.Id);
            string rootCurrency = dmTyGia.SMaTienTeGoc;
            string sourceCurrency = "";
            double value = 0;
            switch (e.PropertyName)
            {
                case nameof(NhQtQuyetToanNienDoChiTietModel.FDeNghiQtNamNayVnd):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                    value = objSender.FDeNghiQtNamNayVnd.GetValueOrDefault();
                    double? fDeNghiQtNamNayUsdNew = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    objSender.FThuaThieuKinhPhiTrongNamUsd = Convert.ToDouble(objSender.FQtKinhPhiDuocCapTongSoUsd) - fDeNghiQtNamNayUsdNew - Convert.ToDouble(objSender.FDeNghiChuyenNamSauUsd);
                    objSender.FThuaThieuKinhPhiTrongNamVnd = Convert.ToDouble(objSender.FQtKinhPhiDuocCapTongSoVnd) - value - Convert.ToDouble(objSender.FDeNghiChuyenNamSauVnd);
                    fDeNghiQtNamNayUsdNew = fDeNghiQtNamNayUsdNew == 0 ? null : fDeNghiQtNamNayUsdNew;
                    if (objSender.FDeNghiQtNamNayUsd == fDeNghiQtNamNayUsdNew || (objSender.SLevel != "3" && objSender.SLevel != "4")) break;
                    objSender.FDeNghiQtNamNayUsd = fDeNghiQtNamNayUsdNew;
                    if (objSender.SLevel == "4")
                    {
                        var Items_FDeNghiQtNamNayVnd = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        double? fDeNghiQtNamNayVnd3 = Items_FDeNghiQtNamNayVnd.Sum(x => x.FDeNghiQtNamNayVnd), 
                                fDeNghiQtNamNayUsd3 = Items_FDeNghiQtNamNayVnd.Sum(x => x.FDeNghiQtNamNayUsd);
                        var item_parent3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).FirstOrDefault();
                        if (item_parent3 != null)
                        {
                            item_parent3.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent3.FDeNghiQtNamNayUsd = fDeNghiQtNamNayUsd3;
                            item_parent3.FDeNghiQtNamNayVnd = fDeNghiQtNamNayVnd3;
                            item_parent3.FThuaThieuKinhPhiTrongNamVnd = Items_FDeNghiQtNamNayVnd.Sum(x => x.FThuaThieuKinhPhiTrongNamVnd);
                            item_parent3.FThuaThieuKinhPhiTrongNamUsd = Items_FDeNghiQtNamNayVnd.Sum(x => x.FThuaThieuKinhPhiTrongNamUsd);
                            item_parent3.PropertyChanged += DetailModel_PropertyChanged;
                            var Items_FDeNghiQtNamNayVnd3 = Items.Where(x => x.SLevel == "3" && (objSender.IID_DuAnID != null || x.IIdHopDongId == objSender.IIdHopDongId) && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                            double? fDeNghiQtNamNayVnd2 = Items_FDeNghiQtNamNayVnd3.Sum(x => x.FDeNghiQtNamNayVnd), 
                                    fDeNghiQtNamNayUsd2 = Items_FDeNghiQtNamNayVnd3.Sum(x => x.FDeNghiQtNamNayUsd);
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            if (item_parent2 != null)
                            {
                                double? fQtKinhPhiDuocCapTongSoVnd = 0, fQtKinhPhiDuocCapTongSoUsd = 0, fDeNghiChuyenNamSauVnd = 0, fDeNghiChuyenNamSauUsd = 0;
                                item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                                item_parent2.FDeNghiQtNamNayUsd = fDeNghiQtNamNayUsd2;
                                item_parent2.FDeNghiQtNamNayVnd = fDeNghiQtNamNayVnd2;
                                fQtKinhPhiDuocCapTongSoVnd = item_parent2.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoVnd;
                                fQtKinhPhiDuocCapTongSoUsd = item_parent2.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoUsd;
                                fDeNghiChuyenNamSauVnd = item_parent2.FDeNghiChuyenNamSauVnd == null ? 0 : item_parent2.FDeNghiChuyenNamSauVnd;
                                fDeNghiChuyenNamSauUsd = item_parent2.FDeNghiChuyenNamSauUsd == null ? 0 : item_parent2.FDeNghiChuyenNamSauUsd;
                                item_parent2.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd - Convert.ToDouble(fDeNghiQtNamNayVnd2) - fDeNghiChuyenNamSauVnd) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd - Convert.ToDouble(fDeNghiQtNamNayVnd2) - fDeNghiChuyenNamSauVnd);
                                item_parent2.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd - Convert.ToDouble(fDeNghiQtNamNayUsd2) - fDeNghiChuyenNamSauUsd) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd - Convert.ToDouble(fDeNghiQtNamNayUsd2) - fDeNghiChuyenNamSauUsd);
                                item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                            }
                        }
                        else
                        {
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            double? fQtKinhPhiDuocCapTongSoVnd = 0, fQtKinhPhiDuocCapTongSoUsd = 0, fDeNghiChuyenNamSauVnd = 0, fDeNghiChuyenNamSauUsd = 0;
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FDeNghiQtNamNayUsd = fDeNghiQtNamNayUsd3;
                            item_parent2.FDeNghiQtNamNayVnd = fDeNghiQtNamNayVnd3;
                            fQtKinhPhiDuocCapTongSoVnd = item_parent2.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoVnd;
                            fQtKinhPhiDuocCapTongSoUsd = item_parent2.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoUsd;
                            fDeNghiChuyenNamSauVnd = item_parent2.FDeNghiChuyenNamSauVnd == null ? 0 : item_parent2.FDeNghiChuyenNamSauVnd;
                            fDeNghiChuyenNamSauUsd = item_parent2.FDeNghiChuyenNamSauUsd == null ? 0 : item_parent2.FDeNghiChuyenNamSauUsd;
                            item_parent2.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd - Convert.ToDouble(fDeNghiQtNamNayVnd3) - fDeNghiChuyenNamSauVnd) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd - Convert.ToDouble(fDeNghiQtNamNayVnd3) - fDeNghiChuyenNamSauVnd);
                            item_parent2.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd - Convert.ToDouble(fDeNghiQtNamNayUsd3) - fDeNghiChuyenNamSauUsd) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd - Convert.ToDouble(fDeNghiQtNamNayUsd3) - fDeNghiChuyenNamSauUsd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }

                    }
                    //Tính tiền của Dự án, Chi hợp đồng, Chi khác
                    var Items_HD_FDeNghiQtNamNayVnd = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).ToList();
                    double? fDeNghiQtNamNayVndDA = Items_HD_FDeNghiQtNamNayVnd.Sum(x => x.FDeNghiQtNamNayVnd), 
                            fDeNghiQtNamNayUsdDA = Items_HD_FDeNghiQtNamNayVnd.Sum(x => x.FDeNghiQtNamNayUsd), 
                            fQtKinhPhiDuocCapTongSoVnd_DA0 = 0, fQtKinhPhiDuocCapTongSoUsd_DA0 = 0, fDeNghiChuyenNamSauVnd_DA0 = 0, fDeNghiChuyenNamSauUsd_DA0 = 0;
                    var item_DuAn_FDeNghiQtNamNayVnd = Items.Where(x => x.SLevel == "1" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                    fQtKinhPhiDuocCapTongSoVnd_DA0 = item_DuAn_FDeNghiQtNamNayVnd.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_DuAn_FDeNghiQtNamNayVnd.FQtKinhPhiDuocCapTongSoVnd;
                    fQtKinhPhiDuocCapTongSoUsd_DA0 = item_DuAn_FDeNghiQtNamNayVnd.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_DuAn_FDeNghiQtNamNayVnd.FQtKinhPhiDuocCapTongSoUsd;
                    fDeNghiChuyenNamSauVnd_DA0 = item_DuAn_FDeNghiQtNamNayVnd.FDeNghiChuyenNamSauVnd == null ? 0 : item_DuAn_FDeNghiQtNamNayVnd.FDeNghiChuyenNamSauVnd;
                    fDeNghiChuyenNamSauUsd_DA0 = item_DuAn_FDeNghiQtNamNayVnd.FDeNghiChuyenNamSauUsd == null ? 0 : item_DuAn_FDeNghiQtNamNayVnd.FDeNghiChuyenNamSauUsd;
                    item_DuAn_FDeNghiQtNamNayVnd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_DuAn_FDeNghiQtNamNayVnd.FDeNghiQtNamNayVnd = fDeNghiQtNamNayVndDA;
                    item_DuAn_FDeNghiQtNamNayVnd.FDeNghiQtNamNayUsd = fDeNghiQtNamNayUsdDA;
                    item_DuAn_FDeNghiQtNamNayVnd.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd_DA0 - Convert.ToDouble(fDeNghiQtNamNayVndDA) - fDeNghiChuyenNamSauVnd_DA0) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd_DA0 - Convert.ToDouble(fDeNghiQtNamNayVndDA) - fDeNghiChuyenNamSauVnd_DA0);
                    item_DuAn_FDeNghiQtNamNayVnd.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd_DA0 - Convert.ToDouble(fDeNghiQtNamNayUsdDA) - fDeNghiChuyenNamSauUsd_DA0) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd_DA0 - Convert.ToDouble(fDeNghiQtNamNayUsdDA) - fDeNghiChuyenNamSauUsd_DA0);
                    item_DuAn_FDeNghiQtNamNayVnd.PropertyChanged += DetailModel_PropertyChanged;
                    //Tính tiền của NVC
                    var Items_DA_FDeNghiQtNamNayVnd = Items.Where(x => x.SLevel == "1" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).ToList();
                    double? fDeNghiQtNamNayVndNVC = Items_HD_FDeNghiQtNamNayVnd.Sum(x => x.FDeNghiQtNamNayVnd),
                            fDeNghiQtNamNayUsdNVC = Items_HD_FDeNghiQtNamNayVnd.Sum(x => x.FDeNghiQtNamNayUsd), 
                            fQtKinhPhiDuocCapTongSoVnd_NVC0 = 0, fQtKinhPhiDuocCapTongSoUsd_NVC0 = 0, fDeNghiChuyenNamSauVnd_NVC0 = 0, fDeNghiChuyenNamSauUsd_NVC0 = 0;
                    var item_NVC_FDeNghiQtNamNayVnd = Items.Where(x => x.SLevel == "0" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                    fQtKinhPhiDuocCapTongSoVnd_NVC0 = item_NVC_FDeNghiQtNamNayVnd.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_NVC_FDeNghiQtNamNayVnd.FQtKinhPhiDuocCapTongSoVnd;
                    fQtKinhPhiDuocCapTongSoUsd_NVC0 = item_NVC_FDeNghiQtNamNayVnd.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_NVC_FDeNghiQtNamNayVnd.FQtKinhPhiDuocCapTongSoUsd;
                    fDeNghiChuyenNamSauVnd_NVC0 = item_NVC_FDeNghiQtNamNayVnd.FDeNghiChuyenNamSauVnd == null ? 0 : item_NVC_FDeNghiQtNamNayVnd.FDeNghiChuyenNamSauVnd;
                    fDeNghiChuyenNamSauUsd_NVC0 = item_NVC_FDeNghiQtNamNayVnd.FDeNghiChuyenNamSauUsd == null ? 0 : item_NVC_FDeNghiQtNamNayVnd.FDeNghiChuyenNamSauUsd;
                    item_NVC_FDeNghiQtNamNayVnd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_NVC_FDeNghiQtNamNayVnd.FDeNghiQtNamNayVnd = fDeNghiQtNamNayVndNVC;
                    item_NVC_FDeNghiQtNamNayVnd.FDeNghiQtNamNayUsd = fDeNghiQtNamNayUsdNVC;
                    item_NVC_FDeNghiQtNamNayVnd.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd_NVC0 - Convert.ToDouble(fDeNghiQtNamNayVndNVC) - fDeNghiChuyenNamSauVnd_NVC0) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd_NVC0 - Convert.ToDouble(fDeNghiQtNamNayVndNVC) - fDeNghiChuyenNamSauVnd_NVC0);
                    item_NVC_FDeNghiQtNamNayVnd.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd_NVC0 - Convert.ToDouble(fDeNghiQtNamNayUsdNVC) - fDeNghiChuyenNamSauUsd_NVC0) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd_NVC0 - Convert.ToDouble(fDeNghiQtNamNayUsdNVC)    - fDeNghiChuyenNamSauUsd_NVC0);
                    item_NVC_FDeNghiQtNamNayVnd.PropertyChanged += DetailModel_PropertyChanged;
                    break;
                case nameof(NhQtQuyetToanNienDoChiTietModel.FDeNghiQtNamNayUsd):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    value = objSender.FDeNghiQtNamNayUsd.GetValueOrDefault();
                    double? fDeNghiQtNamNayVndNew = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    objSender.FThuaThieuKinhPhiTrongNamUsd = Convert.ToDouble(objSender.FQtKinhPhiDuocCapTongSoUsd) - value - Convert.ToDouble(objSender.FDeNghiChuyenNamSauVnd);
                    objSender.FThuaThieuKinhPhiTrongNamVnd = Convert.ToDouble(objSender.FQtKinhPhiDuocCapTongSoVnd) - fDeNghiQtNamNayVndNew - Convert.ToDouble(objSender.FDeNghiChuyenNamSauVnd);
                    fDeNghiQtNamNayVndNew = fDeNghiQtNamNayVndNew == 0 ? null : fDeNghiQtNamNayVndNew;
                    if (objSender.FDeNghiQtNamNayVnd == fDeNghiQtNamNayVndNew || (objSender.SLevel != "3" && objSender.SLevel != "4")) break;
                    objSender.FDeNghiQtNamNayVnd = fDeNghiQtNamNayVndNew;                   
                    if (objSender.SLevel == "4")
                    {
                        var Items_FDeNghiQtNamNayUsd = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        double? fDeNghiQtNamNayVnd_USD3 = Items_FDeNghiQtNamNayUsd.Sum(x => x.FDeNghiQtNamNayVnd), 
                                fDeNghiQtNamNayUsd_USD3 = Items_FDeNghiQtNamNayUsd.Sum(x => x.FDeNghiQtNamNayUsd);
                        var item_parent3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                        if (item_parent3 != null)
                        {
                            item_parent3.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent3.FDeNghiQtNamNayUsd = fDeNghiQtNamNayUsd_USD3;
                            item_parent3.FDeNghiQtNamNayVnd = fDeNghiQtNamNayVnd_USD3;
                            item_parent3.FThuaThieuKinhPhiTrongNamVnd = Items_FDeNghiQtNamNayUsd.Sum(x => x.FThuaThieuKinhPhiTrongNamVnd);
                            item_parent3.FThuaThieuKinhPhiTrongNamUsd = Items_FDeNghiQtNamNayUsd.Sum(x => x.FThuaThieuKinhPhiTrongNamUsd);
                            item_parent3.PropertyChanged += DetailModel_PropertyChanged;
                            var Items_FDeNghiQtNamNayUsd3 = Items.Where(x => x.SLevel == "3" && (objSender.IID_DuAnID != null || x.IIdHopDongId == objSender.IIdHopDongId) && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                            double? fDeNghiQtNamNayVnd2 = Items_FDeNghiQtNamNayUsd3.Sum(x => x.FDeNghiQtNamNayVnd), 
                                    fDeNghiQtNamNayUsd2 = Items_FDeNghiQtNamNayUsd3.Sum(x => x.FDeNghiQtNamNayUsd);
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            if (item_parent2 != null)
                            {
                                double? fQtKinhPhiDuocCapTongSoVnd = 0, fQtKinhPhiDuocCapTongSoUsd = 0, fDeNghiChuyenNamSauVnd = 0, fDeNghiChuyenNamSauUsd = 0;
                                item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                                item_parent2.FDeNghiQtNamNayUsd = fDeNghiQtNamNayUsd2;
                                item_parent2.FDeNghiQtNamNayVnd = fDeNghiQtNamNayVnd2;
                                fQtKinhPhiDuocCapTongSoVnd = item_parent2.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoVnd;
                                fQtKinhPhiDuocCapTongSoUsd = item_parent2.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoUsd;
                                fDeNghiChuyenNamSauVnd = item_parent2.FDeNghiChuyenNamSauVnd == null ? 0 : item_parent2.FDeNghiChuyenNamSauVnd;
                                fDeNghiChuyenNamSauUsd = item_parent2.FDeNghiChuyenNamSauUsd == null ? 0 : item_parent2.FDeNghiChuyenNamSauUsd;
                                item_parent2.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd - Convert.ToDouble(fDeNghiQtNamNayVnd2) - fDeNghiChuyenNamSauVnd) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd - Convert.ToDouble(fDeNghiQtNamNayVnd2) - fDeNghiChuyenNamSauVnd);
                                item_parent2.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd - Convert.ToDouble(fDeNghiQtNamNayUsd2) - fDeNghiChuyenNamSauUsd) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd - Convert.ToDouble(fDeNghiQtNamNayUsd2) - fDeNghiChuyenNamSauUsd);
                                item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                            }
                        }
                        else
                        {
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            double? fQtKinhPhiDuocCapTongSoVnd = 0, fQtKinhPhiDuocCapTongSoUsd = 0, fDeNghiChuyenNamSauVnd = 0, fDeNghiChuyenNamSauUsd = 0;
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FDeNghiQtNamNayUsd = fDeNghiQtNamNayUsd_USD3;
                            item_parent2.FDeNghiQtNamNayVnd = fDeNghiQtNamNayVnd_USD3;
                            fQtKinhPhiDuocCapTongSoVnd = item_parent2.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoVnd;
                            fQtKinhPhiDuocCapTongSoUsd = item_parent2.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoUsd;
                            fDeNghiChuyenNamSauVnd = item_parent2.FDeNghiChuyenNamSauVnd == null ? 0 : item_parent2.FDeNghiChuyenNamSauVnd;
                            fDeNghiChuyenNamSauUsd = item_parent2.FDeNghiChuyenNamSauUsd == null ? 0 : item_parent2.FDeNghiChuyenNamSauUsd;
                            item_parent2.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd - Convert.ToDouble(fDeNghiQtNamNayVnd_USD3) - fDeNghiChuyenNamSauVnd) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd - Convert.ToDouble(fDeNghiQtNamNayVnd_USD3) - fDeNghiChuyenNamSauVnd);
                            item_parent2.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd - Convert.ToDouble(fDeNghiQtNamNayUsd_USD3) - fDeNghiChuyenNamSauUsd) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd - Convert.ToDouble(fDeNghiQtNamNayUsd_USD3) - fDeNghiChuyenNamSauUsd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }

                    }
                    //Tính tiền của Dự án, Chi hợp đồng, Chi khác
                    var Items_HD_FDeNghiQtNamNayUsd = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).ToList();
                    double? fDeNghiQtNamNayVndDA_USD = Items_HD_FDeNghiQtNamNayUsd.Sum(x => x.FDeNghiQtNamNayVnd), 
                            fDeNghiQtNamNayUsdDA_USD = Items_HD_FDeNghiQtNamNayUsd.Sum(x => x.FDeNghiQtNamNayUsd), 
                            fQtKinhPhiDuocCapTongSoVnd_DA1 = 0, fQtKinhPhiDuocCapTongSoUsd_DA1 = 0, fDeNghiChuyenNamSauVnd_DA = 0, fDeNghiChuyenNamSauUsd_DA = 0;
                    var item_DuAn_FDeNghiQtNamNayUsd = Items.Where(x => x.SLevel == "1" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                    fQtKinhPhiDuocCapTongSoVnd_DA1 = item_DuAn_FDeNghiQtNamNayUsd.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_DuAn_FDeNghiQtNamNayUsd.FQtKinhPhiDuocCapTongSoVnd;
                    fQtKinhPhiDuocCapTongSoUsd_DA1 = item_DuAn_FDeNghiQtNamNayUsd.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_DuAn_FDeNghiQtNamNayUsd.FQtKinhPhiDuocCapTongSoUsd;
                    fDeNghiChuyenNamSauVnd_DA = item_DuAn_FDeNghiQtNamNayUsd.FDeNghiChuyenNamSauVnd == null ? 0 : item_DuAn_FDeNghiQtNamNayUsd.FDeNghiChuyenNamSauVnd;
                    fDeNghiChuyenNamSauUsd_DA = item_DuAn_FDeNghiQtNamNayUsd.FDeNghiChuyenNamSauUsd == null ? 0 : item_DuAn_FDeNghiQtNamNayUsd.FDeNghiChuyenNamSauUsd;
                    item_DuAn_FDeNghiQtNamNayUsd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_DuAn_FDeNghiQtNamNayUsd.FDeNghiQtNamNayVnd = fDeNghiQtNamNayVndDA_USD;
                    item_DuAn_FDeNghiQtNamNayUsd.FDeNghiQtNamNayUsd = fDeNghiQtNamNayUsdDA_USD;
                    item_DuAn_FDeNghiQtNamNayUsd.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd_DA1 - Convert.ToDouble(fDeNghiQtNamNayVndDA_USD) - fDeNghiChuyenNamSauVnd_DA) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd_DA1 - Convert.ToDouble(fDeNghiQtNamNayVndDA_USD) - fDeNghiChuyenNamSauVnd_DA);
                    item_DuAn_FDeNghiQtNamNayUsd.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd_DA1 - Convert.ToDouble(fDeNghiQtNamNayUsdDA_USD) - fDeNghiChuyenNamSauUsd_DA) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd_DA1 - Convert.ToDouble(fDeNghiQtNamNayUsdDA_USD) - fDeNghiChuyenNamSauUsd_DA);
                    item_DuAn_FDeNghiQtNamNayUsd.PropertyChanged += DetailModel_PropertyChanged;
                    //Tính tiền của NVC
                    var Items_DA_FDeNghiQtNamNayUsd = Items.Where(x => x.SLevel == "1" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).ToList();
                    double? fDeNghiQtNamNayVndNVC_USD = Items_DA_FDeNghiQtNamNayUsd.Sum(x => x.FDeNghiQtNamNayVnd), 
                            fDeNghiQtNamNayUsdNVC_USD = Items_DA_FDeNghiQtNamNayUsd.Sum(x => x.FDeNghiQtNamNayUsd), 
                            fQtKinhPhiDuocCapTongSoVnd_NVC1 = 0, fQtKinhPhiDuocCapTongSoUsd_NVC1 = 0, fDeNghiChuyenNamSauVnd_NVC = 0, fDeNghiChuyenNamSauUsd_NVC = 0;
                    var item_NVC_FDeNghiQtNamNayUsd = Items.Where(x => x.SLevel == "0" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                    fQtKinhPhiDuocCapTongSoVnd_NVC1 = item_NVC_FDeNghiQtNamNayUsd.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_NVC_FDeNghiQtNamNayUsd.FQtKinhPhiDuocCapTongSoVnd;
                    fQtKinhPhiDuocCapTongSoUsd_NVC1 = item_NVC_FDeNghiQtNamNayUsd.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_NVC_FDeNghiQtNamNayUsd.FQtKinhPhiDuocCapTongSoUsd;
                    fDeNghiChuyenNamSauVnd_NVC = item_NVC_FDeNghiQtNamNayUsd.FDeNghiChuyenNamSauVnd == null ? 0 : item_NVC_FDeNghiQtNamNayUsd.FDeNghiChuyenNamSauVnd;
                    fDeNghiChuyenNamSauUsd_NVC = item_NVC_FDeNghiQtNamNayUsd.FDeNghiChuyenNamSauUsd == null ? 0 : item_NVC_FDeNghiQtNamNayUsd.FDeNghiChuyenNamSauUsd;
                    item_NVC_FDeNghiQtNamNayUsd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_NVC_FDeNghiQtNamNayUsd.FDeNghiQtNamNayVnd = fDeNghiQtNamNayVndNVC_USD;
                    item_NVC_FDeNghiQtNamNayUsd.FDeNghiQtNamNayUsd = fDeNghiQtNamNayUsdNVC_USD;
                    item_NVC_FDeNghiQtNamNayUsd.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd_NVC1 - Convert.ToDouble(fDeNghiQtNamNayVndNVC_USD) - fDeNghiChuyenNamSauVnd_NVC) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd_NVC1 - Convert.ToDouble(fDeNghiQtNamNayVndNVC_USD) - fDeNghiChuyenNamSauVnd_NVC);
                    item_NVC_FDeNghiQtNamNayUsd.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd_NVC1 - Convert.ToDouble(fDeNghiQtNamNayUsdNVC_USD) - fDeNghiChuyenNamSauUsd_NVC) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd_NVC1 - Convert.ToDouble(fDeNghiQtNamNayUsdNVC_USD) - fDeNghiChuyenNamSauUsd_NVC);
                    item_NVC_FDeNghiQtNamNayUsd.PropertyChanged += DetailModel_PropertyChanged;
                    break;
                case nameof(NhQtQuyetToanNienDoChiTietModel.FDeNghiChuyenNamSauVnd):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                    value = objSender.FDeNghiChuyenNamSauVnd.GetValueOrDefault();
                    double? fDeNghiChuyenNamSauUsdNew = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    objSender.FThuaThieuKinhPhiTrongNamUsd = Convert.ToDouble(objSender.FQtKinhPhiDuocCapTongSoUsd) - fDeNghiChuyenNamSauUsdNew - Convert.ToDouble(objSender.FDeNghiQtNamNayUsd);
                    objSender.FThuaThieuKinhPhiTrongNamVnd = Convert.ToDouble(objSender.FQtKinhPhiDuocCapTongSoVnd) - value - Convert.ToDouble(objSender.FDeNghiQtNamNayVnd);
                    fDeNghiChuyenNamSauUsdNew = fDeNghiChuyenNamSauUsdNew == 0 ? null : fDeNghiChuyenNamSauUsdNew;
                    if (objSender.FDeNghiChuyenNamSauUsd == fDeNghiChuyenNamSauUsdNew || (objSender.SLevel != "3" && objSender.SLevel != "4")) break;
                    objSender.FDeNghiChuyenNamSauUsd = fDeNghiChuyenNamSauUsdNew;
                    if (objSender.SLevel == "4")
                    {
                        var Items_FDeNghiChuyenNamSauVnd = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        double? fDeNghiChuyenNamSauVnd3 = Items_FDeNghiChuyenNamSauVnd.Sum(x => x.FDeNghiChuyenNamSauVnd), 
                                fDeNghiChuyenNamSauUsd3 = Items_FDeNghiChuyenNamSauVnd.Sum(x => x.FDeNghiChuyenNamSauUsd);
                        var item_parent3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).FirstOrDefault();
                        if (item_parent3 != null)
                        {
                            item_parent3.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent3.FDeNghiChuyenNamSauUsd = fDeNghiChuyenNamSauUsd3;
                            item_parent3.FDeNghiChuyenNamSauVnd = fDeNghiChuyenNamSauVnd3;
                            item_parent3.FThuaThieuKinhPhiTrongNamVnd = Items_FDeNghiChuyenNamSauVnd.Sum(x => x.FThuaThieuKinhPhiTrongNamVnd);
                            item_parent3.FThuaThieuKinhPhiTrongNamUsd = Items_FDeNghiChuyenNamSauVnd.Sum(x => x.FThuaThieuKinhPhiTrongNamUsd);
                            item_parent3.PropertyChanged += DetailModel_PropertyChanged;
                            var Items_FDeNghiChuyenNamSauVnd3 = Items.Where(x => x.SLevel == "3" && (objSender.IID_DuAnID != null || x.IIdHopDongId == objSender.IIdHopDongId) && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                            double? fDeNghiChuyenNamSauVnd2 = Items_FDeNghiChuyenNamSauVnd3.Sum(x => x.FDeNghiChuyenNamSauVnd), 
                                    fDeNghiChuyenNamSauUsd2 = Items_FDeNghiChuyenNamSauVnd3.Sum(x => x.FDeNghiChuyenNamSauUsd);
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            if (item_parent2 != null)
                            {
                                double? fQtKinhPhiDuocCapTongSoVnd = 0, fQtKinhPhiDuocCapTongSoUsd = 0, fDeNghiQtNamNayVnd = 0, fDeNghiQtNamNayUsd = 0;
                                item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                                item_parent2.FDeNghiChuyenNamSauUsd = fDeNghiChuyenNamSauUsd2;
                                item_parent2.FDeNghiChuyenNamSauVnd = fDeNghiChuyenNamSauVnd2;
                                fQtKinhPhiDuocCapTongSoVnd = item_parent2.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoVnd;
                                fQtKinhPhiDuocCapTongSoUsd = item_parent2.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoUsd;
                                fDeNghiQtNamNayVnd = item_parent2.FDeNghiQtNamNayVnd == null ? 0 : item_parent2.FDeNghiQtNamNayVnd;
                                fDeNghiQtNamNayUsd = item_parent2.FDeNghiQtNamNayUsd == null ? 0 : item_parent2.FDeNghiQtNamNayUsd;
                                item_parent2.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd - fDeNghiQtNamNayVnd - Convert.ToDouble(fDeNghiChuyenNamSauVnd2)) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd - fDeNghiQtNamNayVnd - Convert.ToDouble(fDeNghiChuyenNamSauVnd2));
                                item_parent2.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd - fDeNghiQtNamNayUsd - Convert.ToDouble(fDeNghiChuyenNamSauUsd2)) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd - fDeNghiQtNamNayUsd - Convert.ToDouble(fDeNghiChuyenNamSauUsd2));
                                item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                            }
                        }
                        else
                        {
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            double? fQtKinhPhiDuocCapTongSoVnd = 0, fQtKinhPhiDuocCapTongSoUsd = 0, fDeNghiQtNamNayVnd = 0, fDeNghiQtNamNayUsd = 0;
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FDeNghiChuyenNamSauUsd = fDeNghiChuyenNamSauUsd3;
                            item_parent2.FDeNghiChuyenNamSauVnd = fDeNghiChuyenNamSauVnd3;
                            fQtKinhPhiDuocCapTongSoVnd = item_parent2.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoVnd;
                            fQtKinhPhiDuocCapTongSoUsd = item_parent2.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoUsd;
                            fDeNghiQtNamNayVnd = item_parent2.FDeNghiQtNamNayVnd == null ? 0 : item_parent2.FDeNghiQtNamNayVnd;
                            fDeNghiQtNamNayUsd = item_parent2.FDeNghiQtNamNayUsd == null ? 0 : item_parent2.FDeNghiQtNamNayUsd;
                            item_parent2.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd - fDeNghiQtNamNayVnd - Convert.ToDouble(fDeNghiChuyenNamSauVnd3)) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd - fDeNghiQtNamNayVnd - Convert.ToDouble(fDeNghiChuyenNamSauVnd3));
                            item_parent2.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd - fDeNghiQtNamNayUsd - Convert.ToDouble(fDeNghiChuyenNamSauUsd3)) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd - fDeNghiQtNamNayUsd - Convert.ToDouble(fDeNghiChuyenNamSauUsd3));
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }

                    }
                    //Tính tiền của Dự án, Chi hợp đồng, Chi khác
                    var Items_HD_FDeNghiChuyenNamSauVnd = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).ToList();
                    double? fDeNghiChuyenNamSauVndDA = Items_HD_FDeNghiChuyenNamSauVnd.Sum(x => x.FDeNghiChuyenNamSauVnd), 
                            fDeNghiChuyenNamSauUsdDA = Items_HD_FDeNghiChuyenNamSauVnd.Sum(x => x.FDeNghiChuyenNamSauUsd), 
                            fQtKinhPhiDuocCapTongSoVnd_DA = 0, fQtKinhPhiDuocCapTongSoUsd_DA = 0, fDeNghiQtNamNayVnd_DA = 0, fDeNghiQtNamNayUsd_DA = 0;
                    var item_DuAn_FDeNghiChuyenNamSauVnd = Items.Where(x => x.SLevel == "1" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                    fQtKinhPhiDuocCapTongSoVnd_DA = item_DuAn_FDeNghiChuyenNamSauVnd.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_DuAn_FDeNghiChuyenNamSauVnd.FQtKinhPhiDuocCapTongSoVnd;
                    fQtKinhPhiDuocCapTongSoUsd_DA = item_DuAn_FDeNghiChuyenNamSauVnd.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_DuAn_FDeNghiChuyenNamSauVnd.FQtKinhPhiDuocCapTongSoUsd;
                    fDeNghiQtNamNayVnd_DA = item_DuAn_FDeNghiChuyenNamSauVnd.FDeNghiQtNamNayVnd == null ? 0 : item_DuAn_FDeNghiChuyenNamSauVnd.FDeNghiQtNamNayVnd;
                    fDeNghiQtNamNayUsd_DA = item_DuAn_FDeNghiChuyenNamSauVnd.FDeNghiQtNamNayUsd == null ? 0 : item_DuAn_FDeNghiChuyenNamSauVnd.FDeNghiQtNamNayUsd;
                    item_DuAn_FDeNghiChuyenNamSauVnd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_DuAn_FDeNghiChuyenNamSauVnd.FDeNghiChuyenNamSauVnd = fDeNghiChuyenNamSauVndDA;
                    item_DuAn_FDeNghiChuyenNamSauVnd.FDeNghiChuyenNamSauUsd = fDeNghiChuyenNamSauUsdDA;
                    item_DuAn_FDeNghiChuyenNamSauVnd.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd_DA - fDeNghiQtNamNayVnd_DA - Convert.ToDouble(fDeNghiChuyenNamSauVndDA)) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd_DA - fDeNghiQtNamNayVnd_DA - Convert.ToDouble(fDeNghiChuyenNamSauVndDA));
                    item_DuAn_FDeNghiChuyenNamSauVnd.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd_DA - fDeNghiQtNamNayUsd_DA - Convert.ToDouble(fDeNghiChuyenNamSauUsdDA)) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd_DA - fDeNghiQtNamNayUsd_DA - Convert.ToDouble(fDeNghiChuyenNamSauUsdDA));
                    item_DuAn_FDeNghiChuyenNamSauVnd.PropertyChanged += DetailModel_PropertyChanged;
                    //Tính tiền của NVC
                    var Items_DA_FDeNghiChuyenNamSauVnd = Items.Where(x => x.SLevel == "1" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).ToList();
                    double? fDeNghiChuyenNamSauVndNVC = Items_HD_FDeNghiChuyenNamSauVnd.Sum(x => x.FDeNghiChuyenNamSauVnd), 
                            fDeNghiChuyenNamSauUsdNVC = Items_HD_FDeNghiChuyenNamSauVnd.Sum(x => x.FDeNghiChuyenNamSauUsd), 
                            fQtKinhPhiDuocCapTongSoVnd_NVC = 0, fQtKinhPhiDuocCapTongSoUsd_NVC = 0, fDeNghiQtNamNayVnd_NVC = 0, fDeNghiQtNamNayUsd_NVC = 0;
                    var item_NVC_FDeNghiChuyenNamSauVnd = Items.Where(x => x.SLevel == "0" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                    fQtKinhPhiDuocCapTongSoVnd_NVC = item_NVC_FDeNghiChuyenNamSauVnd.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_NVC_FDeNghiChuyenNamSauVnd.FQtKinhPhiDuocCapTongSoVnd;
                    fQtKinhPhiDuocCapTongSoUsd_NVC = item_NVC_FDeNghiChuyenNamSauVnd.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_NVC_FDeNghiChuyenNamSauVnd.FQtKinhPhiDuocCapTongSoUsd;
                    fDeNghiQtNamNayVnd_NVC = item_NVC_FDeNghiChuyenNamSauVnd.FDeNghiQtNamNayVnd == null ? 0 : item_NVC_FDeNghiChuyenNamSauVnd.FDeNghiQtNamNayVnd;
                    fDeNghiQtNamNayUsd_NVC = item_NVC_FDeNghiChuyenNamSauVnd.FDeNghiQtNamNayUsd == null ? 0 : item_NVC_FDeNghiChuyenNamSauVnd.FDeNghiQtNamNayUsd;
                    item_NVC_FDeNghiChuyenNamSauVnd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_NVC_FDeNghiChuyenNamSauVnd.FDeNghiChuyenNamSauVnd = fDeNghiChuyenNamSauVndNVC;
                    item_NVC_FDeNghiChuyenNamSauVnd.FDeNghiChuyenNamSauUsd = fDeNghiChuyenNamSauUsdNVC;
                    item_NVC_FDeNghiChuyenNamSauVnd.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd_NVC - fDeNghiQtNamNayVnd_NVC - Convert.ToDouble(fDeNghiChuyenNamSauVndNVC)) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd_NVC - fDeNghiQtNamNayVnd_NVC - Convert.ToDouble(fDeNghiChuyenNamSauVndNVC));
                    item_NVC_FDeNghiChuyenNamSauVnd.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd_NVC - fDeNghiQtNamNayUsd_NVC - Convert.ToDouble(fDeNghiChuyenNamSauUsdNVC)) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd_NVC - fDeNghiQtNamNayUsd_NVC - Convert.ToDouble(fDeNghiChuyenNamSauUsdNVC));
                    item_NVC_FDeNghiChuyenNamSauVnd.PropertyChanged += DetailModel_PropertyChanged;
                    break;
                case nameof(NhQtQuyetToanNienDoChiTietModel.FDeNghiChuyenNamSauUsd):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    value = objSender.FDeNghiChuyenNamSauUsd.GetValueOrDefault();
                    double? fDeNghiChuyenNamSauVndNew = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    objSender.FThuaThieuKinhPhiTrongNamUsd = Convert.ToDouble(objSender.FQtKinhPhiDuocCapTongSoUsd) - value - Convert.ToDouble(objSender.FDeNghiQtNamNayUsd);
                    objSender.FThuaThieuKinhPhiTrongNamVnd = Convert.ToDouble(objSender.FQtKinhPhiDuocCapTongSoVnd) - fDeNghiChuyenNamSauVndNew - Convert.ToDouble(objSender.FDeNghiQtNamNayVnd);
                    fDeNghiChuyenNamSauVndNew = fDeNghiChuyenNamSauVndNew == 0 ? null : fDeNghiChuyenNamSauVndNew;
                    if (objSender.FDeNghiChuyenNamSauVnd == fDeNghiChuyenNamSauVndNew || (objSender.SLevel != "3" && objSender.SLevel != "4")) break;
                    objSender.FDeNghiChuyenNamSauVnd = fDeNghiChuyenNamSauVndNew;
                    if (objSender.SLevel == "4")
                    {
                        var Items_FDeNghiChuyenNamSauUsd = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        double? fDeNghiChuyenNamSauVnd_USD3 = Items_FDeNghiChuyenNamSauUsd.Sum(x => x.FDeNghiChuyenNamSauVnd), 
                                fDeNghiChuyenNamSauUsd_USD3 = Items_FDeNghiChuyenNamSauUsd.Sum(x => x.FDeNghiChuyenNamSauUsd);
                        var item_parent3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                        if (item_parent3 != null)
                        {
                            item_parent3.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent3.FDeNghiChuyenNamSauUsd = fDeNghiChuyenNamSauUsd_USD3;
                            item_parent3.FDeNghiChuyenNamSauVnd = fDeNghiChuyenNamSauVnd_USD3;
                            item_parent3.FThuaThieuKinhPhiTrongNamVnd = Items_FDeNghiChuyenNamSauUsd.Sum(x => x.FThuaThieuKinhPhiTrongNamVnd);
                            item_parent3.FThuaThieuKinhPhiTrongNamUsd = Items_FDeNghiChuyenNamSauUsd.Sum(x => x.FThuaThieuKinhPhiTrongNamUsd);
                            item_parent3.PropertyChanged += DetailModel_PropertyChanged;
                            var Items_FDeNghiChuyenNamSauUsd3 = Items.Where(x => x.SLevel == "3" && (objSender.IID_DuAnID != null || x.IIdHopDongId == objSender.IIdHopDongId) && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                            double? fDeNghiChuyenNamSauVnd2 = Items_FDeNghiChuyenNamSauUsd3.Sum(x => x.FDeNghiChuyenNamSauVnd), 
                                    fDeNghiChuyenNamSauUsd2 = Items_FDeNghiChuyenNamSauUsd3.Sum(x => x.FDeNghiChuyenNamSauUsd);
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            if (item_parent2 != null)
                            {
                                double? fQtKinhPhiDuocCapTongSoVnd = 0, fQtKinhPhiDuocCapTongSoUsd = 0, fDeNghiQtNamNayVnd = 0, fDeNghiQtNamNayUsd = 0;
                                item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                                item_parent2.FDeNghiChuyenNamSauUsd = fDeNghiChuyenNamSauUsd2;
                                item_parent2.FDeNghiChuyenNamSauVnd = fDeNghiChuyenNamSauVnd2;
                                fQtKinhPhiDuocCapTongSoVnd = item_parent2.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoVnd;
                                fQtKinhPhiDuocCapTongSoUsd = item_parent2.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoUsd;
                                fDeNghiQtNamNayVnd = item_parent2.FDeNghiQtNamNayVnd == null ? 0 : item_parent2.FDeNghiQtNamNayVnd;
                                fDeNghiQtNamNayUsd = item_parent2.FDeNghiQtNamNayUsd == null ? 0 : item_parent2.FDeNghiQtNamNayUsd;
                                item_parent2.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd - fDeNghiQtNamNayVnd - Convert.ToDouble(fDeNghiChuyenNamSauVnd2)) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd - fDeNghiQtNamNayVnd - Convert.ToDouble(fDeNghiChuyenNamSauVnd2));
                                item_parent2.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd - fDeNghiQtNamNayUsd - Convert.ToDouble(fDeNghiChuyenNamSauUsd2)) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd - fDeNghiQtNamNayUsd - Convert.ToDouble(fDeNghiChuyenNamSauUsd2));
                                item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                            }
                        }
                        else
                        {
                            var item_parent2 = Items.Where(x => x.SLevel == "2"  && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            double? fQtKinhPhiDuocCapTongSoVnd = 0, fQtKinhPhiDuocCapTongSoUsd = 0, fDeNghiQtNamNayVnd = 0, fDeNghiQtNamNayUsd = 0;
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FDeNghiChuyenNamSauUsd = fDeNghiChuyenNamSauUsd_USD3;
                            item_parent2.FDeNghiChuyenNamSauVnd = fDeNghiChuyenNamSauVnd_USD3;
                            fQtKinhPhiDuocCapTongSoVnd = item_parent2.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoVnd;
                            fQtKinhPhiDuocCapTongSoUsd = item_parent2.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_parent2.FQtKinhPhiDuocCapTongSoUsd;
                            fDeNghiQtNamNayVnd = item_parent2.FDeNghiQtNamNayVnd == null ? 0 : item_parent2.FDeNghiQtNamNayVnd;
                            fDeNghiQtNamNayUsd = item_parent2.FDeNghiQtNamNayUsd == null ? 0 : item_parent2.FDeNghiQtNamNayUsd;
                            item_parent2.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd - fDeNghiQtNamNayVnd - Convert.ToDouble(fDeNghiChuyenNamSauVnd_USD3)) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd - fDeNghiQtNamNayVnd - Convert.ToDouble(fDeNghiChuyenNamSauVnd_USD3));
                            item_parent2.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd - fDeNghiQtNamNayUsd - Convert.ToDouble(fDeNghiChuyenNamSauUsd_USD3)) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd - fDeNghiQtNamNayUsd - Convert.ToDouble(fDeNghiChuyenNamSauUsd_USD3));
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }

                    }
                    //Tính tiền của Dự án, Chi hợp đồng, Chi khác
                    var Items_HD_FDeNghiChuyenNamSauUsd = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).ToList();
                    double? fDeNghiChuyenNamSauVndDA_USD = Items_HD_FDeNghiChuyenNamSauUsd.Sum(x => x.FDeNghiChuyenNamSauVnd), 
                            fDeNghiChuyenNamSauUsdDA_USD = Items_HD_FDeNghiChuyenNamSauUsd.Sum(x => x.FDeNghiChuyenNamSauUsd), fQtKinhPhiDuocCapTongSoVnd_DA_USD = 0, fQtKinhPhiDuocCapTongSoUsd_DA_USD = 0, fDeNghiQtNamNayVnd_DA_USD = 0, fDeNghiQtNamNayUsd_DA_USD = 0;
                    var item_DuAn_FDeNghiChuyenNamSauUsd = Items.Where(x => x.SLevel == "1" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                    fQtKinhPhiDuocCapTongSoVnd_DA_USD = item_DuAn_FDeNghiChuyenNamSauUsd.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_DuAn_FDeNghiChuyenNamSauUsd.FQtKinhPhiDuocCapTongSoVnd;
                    fQtKinhPhiDuocCapTongSoUsd_DA_USD = item_DuAn_FDeNghiChuyenNamSauUsd.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_DuAn_FDeNghiChuyenNamSauUsd.FQtKinhPhiDuocCapTongSoUsd;
                    fDeNghiQtNamNayVnd_DA_USD = item_DuAn_FDeNghiChuyenNamSauUsd.FDeNghiQtNamNayVnd == null ? 0 : item_DuAn_FDeNghiChuyenNamSauUsd.FDeNghiQtNamNayVnd;
                    fDeNghiQtNamNayUsd_DA_USD = item_DuAn_FDeNghiChuyenNamSauUsd.FDeNghiQtNamNayUsd == null ? 0 : item_DuAn_FDeNghiChuyenNamSauUsd.FDeNghiQtNamNayUsd;
                    item_DuAn_FDeNghiChuyenNamSauUsd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_DuAn_FDeNghiChuyenNamSauUsd.FDeNghiChuyenNamSauVnd = fDeNghiChuyenNamSauVndDA_USD;
                    item_DuAn_FDeNghiChuyenNamSauUsd.FDeNghiChuyenNamSauUsd = fDeNghiChuyenNamSauUsdDA_USD;
                    item_DuAn_FDeNghiChuyenNamSauUsd.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd_DA_USD - fDeNghiQtNamNayVnd_DA_USD - Convert.ToDouble(fDeNghiChuyenNamSauVndDA_USD)) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd_DA_USD - fDeNghiQtNamNayVnd_DA_USD - Convert.ToDouble(fDeNghiChuyenNamSauVndDA_USD));
                    item_DuAn_FDeNghiChuyenNamSauUsd.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd_DA_USD - fDeNghiQtNamNayUsd_DA_USD - Convert.ToDouble(fDeNghiChuyenNamSauUsdDA_USD)) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd_DA_USD - fDeNghiQtNamNayUsd_DA_USD - Convert.ToDouble(fDeNghiChuyenNamSauUsdDA_USD));
                    item_DuAn_FDeNghiChuyenNamSauUsd.PropertyChanged += DetailModel_PropertyChanged;
                    //Tính tiền của NVC
                    var Items_DA_FDeNghiChuyenNamSauUsd = Items.Where(x => x.SLevel == "1" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).ToList();
                    double? fDeNghiChuyenNamSauVndNVC_USD = Items_DA_FDeNghiChuyenNamSauUsd.Sum(x => x.FDeNghiChuyenNamSauVnd),
                            fDeNghiChuyenNamSauUsdNVC_USD = Items_DA_FDeNghiChuyenNamSauUsd.Sum(x => x.FDeNghiChuyenNamSauUsd), 
                            fQtKinhPhiDuocCapTongSoVnd_NVC_USD = 0, fQtKinhPhiDuocCapTongSoUsd_NVC_USD = 0, fDeNghiQtNamNayVnd_NVC_USD = 0, fDeNghiQtNamNayUsd_NVC_USD = 0;
                    var item_NVC_FDeNghiChuyenNamSauUsd = Items.Where(x => x.SLevel == "0" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                    fQtKinhPhiDuocCapTongSoVnd_NVC_USD = item_NVC_FDeNghiChuyenNamSauUsd.FQtKinhPhiDuocCapTongSoVnd == null ? 0 : item_NVC_FDeNghiChuyenNamSauUsd.FQtKinhPhiDuocCapTongSoVnd;
                    fQtKinhPhiDuocCapTongSoUsd_NVC_USD = item_NVC_FDeNghiChuyenNamSauUsd.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : item_NVC_FDeNghiChuyenNamSauUsd.FQtKinhPhiDuocCapTongSoUsd;
                    fDeNghiQtNamNayVnd_NVC_USD = item_NVC_FDeNghiChuyenNamSauUsd.FDeNghiQtNamNayVnd == null ? 0 : item_NVC_FDeNghiChuyenNamSauUsd.FDeNghiQtNamNayVnd;
                    fDeNghiQtNamNayUsd_NVC_USD = item_NVC_FDeNghiChuyenNamSauUsd.FDeNghiQtNamNayUsd == null ? 0 : item_NVC_FDeNghiChuyenNamSauUsd.FDeNghiQtNamNayUsd;
                    item_NVC_FDeNghiChuyenNamSauUsd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_NVC_FDeNghiChuyenNamSauUsd.FDeNghiChuyenNamSauVnd = fDeNghiChuyenNamSauVndNVC_USD;
                    item_NVC_FDeNghiChuyenNamSauUsd.FDeNghiChuyenNamSauUsd = fDeNghiChuyenNamSauUsdNVC_USD;
                    item_NVC_FDeNghiChuyenNamSauUsd.FThuaThieuKinhPhiTrongNamVnd = (fQtKinhPhiDuocCapTongSoVnd_NVC_USD - fDeNghiQtNamNayVnd_NVC_USD - Convert.ToDouble(fDeNghiChuyenNamSauVndNVC_USD)) == 0 ? null : (fQtKinhPhiDuocCapTongSoVnd_NVC_USD - fDeNghiQtNamNayVnd_NVC_USD - Convert.ToDouble(fDeNghiChuyenNamSauVndNVC_USD));
                    item_NVC_FDeNghiChuyenNamSauUsd.FThuaThieuKinhPhiTrongNamUsd = (fQtKinhPhiDuocCapTongSoUsd_NVC_USD - fDeNghiQtNamNayUsd_NVC_USD - Convert.ToDouble(fDeNghiChuyenNamSauUsdNVC_USD)) == 0 ? null : (fQtKinhPhiDuocCapTongSoUsd_NVC_USD - fDeNghiQtNamNayUsd_NVC_USD - Convert.ToDouble(fDeNghiChuyenNamSauUsdNVC_USD));
                    item_NVC_FDeNghiChuyenNamSauUsd.PropertyChanged += DetailModel_PropertyChanged;
                    break;
                case nameof(NhQtQuyetToanNienDoChiTietModel.FThuaNopNsnnVnd):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                    value = objSender.FThuaNopNsnnVnd.GetValueOrDefault();
                    double? fThuaNopNsnnUsdNew = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    fThuaNopNsnnUsdNew = fThuaNopNsnnUsdNew == 0 ? null : fThuaNopNsnnUsdNew;
                    if (objSender.FThuaNopNsnnUsd == fThuaNopNsnnUsdNew || (objSender.SLevel != "3" && objSender.SLevel != "4")) break;
                    objSender.FThuaNopNsnnUsd = fThuaNopNsnnUsdNew;
                    if (objSender.SLevel == "3")
                    {
                        //if (objSender.IsData == false) break;
                        var Items_Child = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        foreach (var i in Items_Child)
                        {
                            i.PropertyChanged -= DetailModel_PropertyChanged;
                            i.FThuaNopNsnnVnd = i.FThuaNopNsnnUsd = null;
                            i.PropertyChanged += DetailModel_PropertyChanged;
                        }
                        var Items_FThuaNopNsnnVnd3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                        if (item_parent2 != null)
                        {
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FThuaNopNsnnUsd = Items_FThuaNopNsnnVnd3.Sum(x => x.FThuaNopNsnnUsd);
                            item_parent2.FThuaNopNsnnVnd = Items_FThuaNopNsnnVnd3.Sum(x => x.FThuaNopNsnnVnd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }
                    }
                    else if (objSender.SLevel == "4")
                    {
                        var Items_FThuaNopNsnnVnd = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        var item_parent3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).FirstOrDefault();
                        if(item_parent3 != null)
                        {
                            item_parent3.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent3.FThuaNopNsnnUsd = Items_FThuaNopNsnnVnd.Sum(x => x.FThuaNopNsnnUsd);
                            item_parent3.FThuaNopNsnnVnd = Items_FThuaNopNsnnVnd.Sum(x => x.FThuaNopNsnnVnd);
                            item_parent3.PropertyChanged += DetailModel_PropertyChanged;
                            var Items_FThuaNopNsnnVnd3 = Items.Where(x => x.SLevel == "3" && (objSender.IID_DuAnID != null || x.IIdHopDongId == objSender.IIdHopDongId) && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            if (item_parent2 != null)
                            {
                                item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                                item_parent2.FThuaNopNsnnUsd = Items_FThuaNopNsnnVnd3.Sum(x => x.FThuaNopNsnnUsd);
                                item_parent2.FThuaNopNsnnVnd = Items_FThuaNopNsnnVnd3.Sum(x => x.FThuaNopNsnnVnd);
                                item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                            }
                        }
                        else
                        {
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FThuaNopNsnnUsd = Items_FThuaNopNsnnVnd.Sum(x => x.FThuaNopNsnnUsd);
                            item_parent2.FThuaNopNsnnVnd = Items_FThuaNopNsnnVnd.Sum(x => x.FThuaNopNsnnVnd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }

                    }
                    //Tính tiền của Dự án, Chi hợp đồng, Chi khác
                    var Items_HD_FThuaNopNsnnVnd = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).ToList();                   
                    var item_DuAn_FThuaNopNsnnVnd = Items.Where(x => x.SLevel == "1" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                    item_DuAn_FThuaNopNsnnVnd.FThuaNopNsnnVnd = Items_HD_FThuaNopNsnnVnd.Sum(x => x.FThuaNopNsnnVnd);
                    item_DuAn_FThuaNopNsnnVnd.FThuaNopNsnnUsd = Items_HD_FThuaNopNsnnVnd.Sum(x => x.FThuaNopNsnnUsd);
                    //Tính tiền của NVC
                    var Items_DA_FThuaNopNsnnVnd = Items.Where(x => x.SLevel == "1" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).ToList();
                    var item_NVC_FThuaNopNsnnVnd = Items.Where(x => x.SLevel == "0" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                    item_NVC_FThuaNopNsnnVnd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_NVC_FThuaNopNsnnVnd.FThuaNopNsnnVnd = Items_DA_FThuaNopNsnnVnd.Sum(x => x.FThuaNopNsnnVnd);
                    item_NVC_FThuaNopNsnnVnd.FThuaNopNsnnUsd = Items_DA_FThuaNopNsnnVnd.Sum(x => x.FThuaNopNsnnUsd);
                    item_NVC_FThuaNopNsnnVnd.PropertyChanged += DetailModel_PropertyChanged;
                    break;
                case nameof(NhQtQuyetToanNienDoChiTietModel.FThuaNopNsnnUsd):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    value = objSender.FThuaNopNsnnUsd.GetValueOrDefault();
                    double? fThuaNopNsnnVndNew = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    fThuaNopNsnnVndNew = fThuaNopNsnnVndNew == 0 ? null : fThuaNopNsnnVndNew;
                    if (objSender.FThuaNopNsnnVnd == fThuaNopNsnnVndNew || (objSender.SLevel != "3" && objSender.SLevel != "4")) break;
                    objSender.FThuaNopNsnnVnd = fThuaNopNsnnVndNew;
                    if (objSender.SLevel == "3")
                    {
                        //if (objSender.IsData == false) break;
                        var Items_Child = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        foreach (var i in Items_Child)
                        {
                            i.PropertyChanged -= DetailModel_PropertyChanged;
                            i.FThuaNopNsnnVnd = i.FThuaNopNsnnUsd = null;
                            i.PropertyChanged += DetailModel_PropertyChanged;
                        }
                        var Items_FThuaNopNsnnUsd3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                        if (item_parent2 != null)
                        {
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FThuaNopNsnnUsd = Items_FThuaNopNsnnUsd3.Sum(x => x.FThuaNopNsnnUsd);
                            item_parent2.FThuaNopNsnnVnd = Items_FThuaNopNsnnUsd3.Sum(x => x.FThuaNopNsnnVnd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }
                    }
                    else if (objSender.SLevel == "4")
                    {
                        var Items_FThuaNopNsnnUsd = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        var item_parent3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                        if (item_parent3 != null)
                        {
                            item_parent3.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent3.FThuaNopNsnnUsd = Items_FThuaNopNsnnUsd.Sum(x => x.FThuaNopNsnnUsd);
                            item_parent3.FThuaNopNsnnVnd = Items_FThuaNopNsnnUsd.Sum(x => x.FThuaNopNsnnVnd);
                            item_parent3.PropertyChanged += DetailModel_PropertyChanged;
                            var Items_FThuaNopNsnnUsd3 = Items.Where(x => x.SLevel == "3" && (objSender.IID_DuAnID != null || x.IIdHopDongId == objSender.IIdHopDongId) && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            if (item_parent2 != null)
                            {
                                item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                                item_parent2.FThuaNopNsnnUsd = Items_FThuaNopNsnnUsd3.Sum(x => x.FThuaNopNsnnUsd);
                                item_parent2.FThuaNopNsnnVnd = Items_FThuaNopNsnnUsd3.Sum(x => x.FThuaNopNsnnVnd);
                                item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                            }
                        }
                        else
                        {
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FThuaNopNsnnUsd = Items_FThuaNopNsnnUsd.Sum(x => x.FThuaNopNsnnUsd);
                            item_parent2.FThuaNopNsnnVnd = Items_FThuaNopNsnnUsd.Sum(x => x.FThuaNopNsnnVnd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }

                    }
                    //Tính tiền của Dự án, Chi hợp đồng, Chi khác
                    var Items_HD_FThuaNopNsnnUsd = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).ToList();
                    var item_DuAn_FThuaNopNsnnUsd = Items.Where(x => x.SLevel == "1" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                    item_DuAn_FThuaNopNsnnUsd.FThuaNopNsnnVnd = Items_HD_FThuaNopNsnnUsd.Sum(x => x.FThuaNopNsnnVnd);
                    item_DuAn_FThuaNopNsnnUsd.FThuaNopNsnnUsd = Items_HD_FThuaNopNsnnUsd.Sum(x => x.FThuaNopNsnnUsd);
                    //Tính tiền của NVC
                    var Items_DA_FThuaNopNsnnUsd = Items.Where(x => x.SLevel == "1" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).ToList();
                    var item_NVC_FThuaNopNsnnUsd = Items.Where(x => x.SLevel == "0" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                    item_NVC_FThuaNopNsnnUsd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_NVC_FThuaNopNsnnUsd.FThuaNopNsnnVnd = Items_DA_FThuaNopNsnnUsd.Sum(x => x.FThuaNopNsnnVnd);
                    item_NVC_FThuaNopNsnnUsd.FThuaNopNsnnUsd = Items_DA_FThuaNopNsnnUsd.Sum(x => x.FThuaNopNsnnUsd);
                    item_NVC_FThuaNopNsnnUsd.PropertyChanged += DetailModel_PropertyChanged;
                    break;
                case nameof(NhQtQuyetToanNienDoChiTietModel.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    value = objSender.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd.GetValueOrDefault();
                    double? fLuyKeKinhPhiDGNTNNVndnew = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    fLuyKeKinhPhiDGNTNNVndnew ??= 0;
                    if (objSender.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd == fLuyKeKinhPhiDGNTNNVndnew || (objSender.SLevel != "3" && objSender.SLevel != "4")) break;
                    objSender.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = fLuyKeKinhPhiDGNTNNVndnew;
                    if (objSender.SLevel == "3")
                    {
                        //if (objSender.IsData == false) break;
                        var Items_Child = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        foreach (var i in Items_Child)
                        {
                            i.PropertyChanged -= DetailModel_PropertyChanged;
                            i.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = i.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = null;
                            i.PropertyChanged += DetailModel_PropertyChanged;
                        }
                        var Items_FLuyKeKinhPhiDgntnnUsd3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                        if (item_parent2 != null)
                        {
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = Items_FLuyKeKinhPhiDgntnnUsd3.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd);
                            item_parent2.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = Items_FLuyKeKinhPhiDgntnnUsd3.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }
                    }
                    else if (objSender.SLevel == "4")
                    {
                        var Items_FLuyKeKinhPhiDgntnnUsd = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        var item_parent3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                        if (item_parent3 != null)
                        {
                            item_parent3.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent3.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = Items_FLuyKeKinhPhiDgntnnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd);
                            item_parent3.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = Items_FLuyKeKinhPhiDgntnnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd);
                            item_parent3.PropertyChanged += DetailModel_PropertyChanged;
                            var Items_FLuyKeKinhPhiDgntnnUsd3 = Items.Where(x => x.SLevel == "3" && (objSender.IID_DuAnID != null || x.IIdHopDongId == objSender.IIdHopDongId) && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            if (item_parent2 != null)
                            {
                                item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                                item_parent2.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = Items_FLuyKeKinhPhiDgntnnUsd3.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd);
                                item_parent2.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = Items_FLuyKeKinhPhiDgntnnUsd3.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd);
                                item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                            }
                        }
                        else
                        {
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = Items_FLuyKeKinhPhiDgntnnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd);
                            item_parent2.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = Items_FLuyKeKinhPhiDgntnnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }

                    }
                    //Tính tiền của Dự án, Chi hợp đồng, Chi khác
                    var Items_HD_FLuyKeKinhPhiDgntnnUsd = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).ToList();
                    var item_FLuyKeKinhPhiDgntnnUsd = Items.Where(x => x.SLevel == "1" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                    item_FLuyKeKinhPhiDgntnnUsd.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = Items_HD_FLuyKeKinhPhiDgntnnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd);
                    item_FLuyKeKinhPhiDgntnnUsd.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = Items_HD_FLuyKeKinhPhiDgntnnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd);
                    //Tính tiền của NVC
                    var Items_DA_FLuyKeKinhPhiDgntnnUsd = Items.Where(x => x.SLevel == "1" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).ToList();
                    var item_NVC_FLuyKeSoDuTukddhnnUsd = Items.Where(x => x.SLevel == "0" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                    item_NVC_FLuyKeSoDuTukddhnnUsd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_NVC_FLuyKeSoDuTukddhnnUsd.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = Items_DA_FLuyKeKinhPhiDgntnnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd);
                    item_NVC_FLuyKeSoDuTukddhnnUsd.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = Items_DA_FLuyKeKinhPhiDgntnnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd);
                    item_NVC_FLuyKeSoDuTukddhnnUsd.PropertyChanged += DetailModel_PropertyChanged;
                    break;
                case nameof(NhQtQuyetToanNienDoChiTietModel.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    value = objSender.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd.GetValueOrDefault();
                    double? fLuyKeKinhPhiDgnkddhnnVndnew = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    fLuyKeKinhPhiDgnkddhnnVndnew ??= 0;
                    if (objSender.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd == fLuyKeKinhPhiDgnkddhnnVndnew || (objSender.SLevel != "3" && objSender.SLevel != "4")) break;
                    objSender.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = fLuyKeKinhPhiDgnkddhnnVndnew;
                    if (objSender.SLevel == "3")
                    {
                        //if (objSender.IsData == false) break;
                        var Items_Child = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        foreach (var i in Items_Child)
                        {
                            i.PropertyChanged -= DetailModel_PropertyChanged;
                            i.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = i.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = null;
                            i.PropertyChanged += DetailModel_PropertyChanged;
                        }
                        var Items_FLuyKeKinhPhiDgnkddhnUsd3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                        if (item_parent2 != null)
                        {
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = Items_FLuyKeKinhPhiDgnkddhnUsd3.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd);
                            item_parent2.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = Items_FLuyKeKinhPhiDgnkddhnUsd3.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }
                    }
                    else if (objSender.SLevel == "4")
                    {
                        var Items_FLuyKeKinhPhiDgnkddhnUsd = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        var item_parent3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                        if (item_parent3 != null)
                        {
                            item_parent3.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent3.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = Items_FLuyKeKinhPhiDgnkddhnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd);
                            item_parent3.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = Items_FLuyKeKinhPhiDgnkddhnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd);
                            item_parent3.PropertyChanged += DetailModel_PropertyChanged;
                            var Items_FLuyKeKinhPhiDgntnnUsd3 = Items.Where(x => x.SLevel == "3" && (objSender.IID_DuAnID != null || x.IIdHopDongId == objSender.IIdHopDongId) && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            if (item_parent2 != null)
                            {
                                item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                                item_parent2.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = Items_FLuyKeKinhPhiDgntnnUsd3.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd);
                                item_parent2.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = Items_FLuyKeKinhPhiDgntnnUsd3.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd);
                                item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                            }
                        }
                        else
                        {
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = Items_FLuyKeKinhPhiDgnkddhnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd);
                            item_parent2.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = Items_FLuyKeKinhPhiDgnkddhnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }

                    }
                    //Tính tiền của Dự án, Chi hợp đồng, Chi khác
                    var Items_HD_FLuyKeKinhPhiDgnkddhnUsd = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).ToList();
                    var item_FLuyKeKinhPhiDgnkddhnUsd = Items.Where(x => x.SLevel == "1" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                    item_FLuyKeKinhPhiDgnkddhnUsd.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = Items_HD_FLuyKeKinhPhiDgnkddhnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd);
                    item_FLuyKeKinhPhiDgnkddhnUsd.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = Items_HD_FLuyKeKinhPhiDgnkddhnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd);
                    //Tính tiền của NVC
                    var Items_DA_FLuyKeKinhPhiDgnkddhnUsd = Items.Where(x => x.SLevel == "1" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).ToList();
                    var item_NVC_FLuyKeKinhPhiDgnkddhnUsd = Items.Where(x => x.SLevel == "0" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                    item_NVC_FLuyKeKinhPhiDgnkddhnUsd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_NVC_FLuyKeKinhPhiDgnkddhnUsd.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = Items_DA_FLuyKeKinhPhiDgnkddhnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd);
                    item_NVC_FLuyKeKinhPhiDgnkddhnUsd.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = Items_DA_FLuyKeKinhPhiDgnkddhnUsd.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd);
                    item_NVC_FLuyKeKinhPhiDgnkddhnUsd.PropertyChanged += DetailModel_PropertyChanged;
                    break;
                case nameof(NhQtQuyetToanNienDoChiTietModel.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    value = objSender.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd.GetValueOrDefault();
                    double? fLuyKeSoDuTukddhnnVndnew = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    fLuyKeSoDuTukddhnnVndnew ??= 0;
                    if (objSender.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd == fLuyKeSoDuTukddhnnVndnew || (objSender.SLevel != "3" && objSender.SLevel != "4")) break;
                    objSender.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = fLuyKeSoDuTukddhnnVndnew;
                    if (objSender.SLevel == "3")
                    {
                        //if (objSender.IsData == false) break;
                        var Items_Child = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        foreach (var i in Items_Child)
                        {
                            i.PropertyChanged -= DetailModel_PropertyChanged;
                            i.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = i.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = null;
                            i.PropertyChanged += DetailModel_PropertyChanged;
                        }
                        var Items_FLuyKeSoDuTukddhnnUsd3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                        if (item_parent2 != null)
                        {
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = Items_FLuyKeSoDuTukddhnnUsd3.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd);
                            item_parent2.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = Items_FLuyKeSoDuTukddhnnUsd3.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }
                    }
                    else if (objSender.SLevel == "4")
                    {
                        var Items_FLuyKeSoDuTukddhnnUsd = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        var item_parent3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                        if (item_parent3 != null)
                        {
                            item_parent3.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent3.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = Items_FLuyKeSoDuTukddhnnUsd.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd);
                            item_parent3.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = Items_FLuyKeSoDuTukddhnnUsd.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd);
                            item_parent3.PropertyChanged += DetailModel_PropertyChanged;
                            var Items_FLuyKeKinhPhiDgntnnUsd3 = Items.Where(x => x.SLevel == "3" && (objSender.IID_DuAnID != null || x.IIdHopDongId == objSender.IIdHopDongId) && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            if (item_parent2 != null)
                            {
                                item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                                item_parent2.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = Items_FLuyKeKinhPhiDgntnnUsd3.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd);
                                item_parent2.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = Items_FLuyKeKinhPhiDgntnnUsd3.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd);
                                item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                            }
                        }
                        else
                        {
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = Items_FLuyKeSoDuTukddhnnUsd.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd);
                            item_parent2.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = Items_FLuyKeSoDuTukddhnnUsd.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }

                    }
                    //Tính tiền của Dự án, Chi hợp đồng, Chi khác
                    var Items_HD_FLuyKeSoDuTukddhnnUsd = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).ToList();
                    var item_FLuyKeSoDuTukddhnnUsd = Items.Where(x => x.SLevel == "1" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                    item_FLuyKeSoDuTukddhnnUsd.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = Items_HD_FLuyKeSoDuTukddhnnUsd.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd);
                    item_FLuyKeSoDuTukddhnnUsd.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = Items_HD_FLuyKeSoDuTukddhnnUsd.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd);
                    //Tính tiền của NVC
                    var Items_DA_FLuyKeSoDuTukddhnnUsd = Items.Where(x => x.SLevel == "1" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).ToList();
                    var item_NVC_FLuyKeKinhPhiDgntnnUsd = Items.Where(x => x.SLevel == "0" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                    item_NVC_FLuyKeKinhPhiDgntnnUsd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_NVC_FLuyKeKinhPhiDgntnnUsd.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = Items_DA_FLuyKeSoDuTukddhnnUsd.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd);
                    item_NVC_FLuyKeKinhPhiDgntnnUsd.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = Items_DA_FLuyKeSoDuTukddhnnUsd.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd);
                    item_NVC_FLuyKeKinhPhiDgntnnUsd.PropertyChanged += DetailModel_PropertyChanged;
                    break;
                case nameof(NhQtQuyetToanNienDoChiTietModel.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd):
                    sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    value = objSender.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd.GetValueOrDefault();
                    double? fLuyKeSoDuTucdkhdhnnVndnew = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    fLuyKeSoDuTucdkhdhnnVndnew ??= 0;
                    if (objSender.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd == fLuyKeSoDuTucdkhdhnnVndnew || (objSender.SLevel != "3" && objSender.SLevel != "4")) break;
                    objSender.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = fLuyKeSoDuTucdkhdhnnVndnew;
                    if (objSender.SLevel == "3")
                    {
                        //if (objSender.IsData == false) break;
                        var Items_Child = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        foreach (var i in Items_Child)
                        {
                            i.PropertyChanged -= DetailModel_PropertyChanged;
                            i.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = i.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = null;
                            i.PropertyChanged += DetailModel_PropertyChanged;
                        }
                        var Items_FLuyKeSoDuTucdkhdhnnUsd3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                        if (item_parent2 != null)
                        {
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = Items_FLuyKeSoDuTucdkhdhnnUsd3.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd);
                            item_parent2.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = Items_FLuyKeSoDuTucdkhdhnnUsd3.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }
                    }
                    else if (objSender.SLevel == "4")
                    {
                        var Items_FLuyKeSoDuTucdkhdhnnUsd = Items.Where(x => x.SLevel == "4" && x.IIdHopDongId == objSender.IIdHopDongId && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                        var item_parent3 = Items.Where(x => x.SLevel == "3" && x.IIdHopDongId == objSender.IIdHopDongId && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                        if (item_parent3 != null)
                        {
                            item_parent3.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent3.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = Items_FLuyKeSoDuTucdkhdhnnUsd.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd);
                            item_parent3.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = Items_FLuyKeSoDuTucdkhdhnnUsd.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd);
                            item_parent3.PropertyChanged += DetailModel_PropertyChanged;
                            var Items_FLuyKeKinhPhiDgntnnUsd3 = Items.Where(x => x.SLevel == "3" && (objSender.IID_DuAnID != null || x.IIdHopDongId == objSender.IIdHopDongId) && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi).ToList();
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            if (item_parent2 != null)
                            {
                                item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                                item_parent2.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = Items_FLuyKeKinhPhiDgntnnUsd3.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd);
                                item_parent2.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = Items_FLuyKeKinhPhiDgntnnUsd3.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd);
                                item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                            }
                        }
                        else
                        {
                            var item_parent2 = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.ILoaiNoiDungChi == objSender.ILoaiNoiDungChi && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                            item_parent2.PropertyChanged -= DetailModel_PropertyChanged;
                            item_parent2.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = Items_FLuyKeSoDuTucdkhdhnnUsd.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd);
                            item_parent2.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = Items_FLuyKeSoDuTucdkhdhnnUsd.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd);
                            item_parent2.PropertyChanged += DetailModel_PropertyChanged;
                        }

                    }
                    //Tính tiền của Dự án, Chi hợp đồng, Chi khác
                    var Items_HD_FLuyKeSoDuTucdkhdhnnUsd = Items.Where(x => x.SLevel == "2" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).ToList();
                    var item_FLuyKeSoDuTucdkhdhnnUsd = Items.Where(x => x.SLevel == "1" && x.IID_DuAnID == objSender.IID_DuAnID && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId && x.IsChiKhac == objSender.IsChiKhac).FirstOrDefault();
                    item_FLuyKeSoDuTucdkhdhnnUsd.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = Items_HD_FLuyKeSoDuTucdkhdhnnUsd.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd);
                    item_FLuyKeSoDuTucdkhdhnnUsd.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = Items_HD_FLuyKeSoDuTucdkhdhnnUsd.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd);
                    //Tính tiền của NVC
                    var Items_DA_FLuyKeSoDuTucdkhdhnnUsd = Items.Where(x => x.SLevel == "1" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).ToList();
                    var item_NVC_FLuyKeSoDuTucdkhdhnnUsd = Items.Where(x => x.SLevel == "0" && x.IIdKhttNhiemVuChiId == objSender.IIdKhttNhiemVuChiId).FirstOrDefault();
                    item_NVC_FLuyKeSoDuTucdkhdhnnUsd.PropertyChanged -= DetailModel_PropertyChanged;
                    item_NVC_FLuyKeSoDuTucdkhdhnnUsd.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = Items_DA_FLuyKeSoDuTucdkhdhnnUsd.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd);
                    item_NVC_FLuyKeSoDuTucdkhdhnnUsd.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = Items_DA_FLuyKeSoDuTucdkhdhnnUsd.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd);
                    item_NVC_FLuyKeSoDuTucdkhdhnnUsd.PropertyChanged += DetailModel_PropertyChanged;
                    break;

                default:
                    break;
            }
            objSender.PropertyChanged += DetailModel_PropertyChanged;
            objSender.IsModified = true;
            objSender.IsData = true;
        }

        private void SetDataMucLucNganSach(NhQtQuyetToanNienDoChiTietModel item, string type, bool isUpdateMlnsId = false)
        {
            Expression<Func<NsMucLucNganSach, bool>> predicate;
            if (string.IsNullOrEmpty(type))
            {
                predicate = PredicateBuilder.True<NsMucLucNganSach>();
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.L));
                var lstLNS = _rootDataMlns.Where(predicate.Compile()).GroupBy(n => n.Lns).Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key }).ToList();
                item.ItemsLNS = new ObservableCollection<ComboboxItem>(lstLNS);
                item.ItemsL = new ObservableCollection<ComboboxItem>();
                item.ItemsK = new ObservableCollection<ComboboxItem>();
                item.ItemsM = new ObservableCollection<ComboboxItem>();
                item.ItemsTM = new ObservableCollection<ComboboxItem>();
                item.ItemsTTM = new ObservableCollection<ComboboxItem>();
            }

            if (type != null && type.Equals(nameof(NhQtQuyetToanNienDoChiTietModel.LNS)))
            {
                predicate = PredicateBuilder.True<NsMucLucNganSach>();
                predicate = predicate.And(x => x.Lns.Equals(item.LNS));
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.L));
                var lstL = _rootDataMlns.Where(predicate.Compile()).GroupBy(n => n.L).Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key });
                item.ItemsL = new ObservableCollection<ComboboxItem>(lstL);
                item.ItemsK = new ObservableCollection<ComboboxItem>();
                item.ItemsM = new ObservableCollection<ComboboxItem>();
                item.ItemsTM = new ObservableCollection<ComboboxItem>();
                item.ItemsTTM = new ObservableCollection<ComboboxItem>();
            }

            if (type != null && type.Equals(nameof(NhQtQuyetToanNienDoChiTietModel.L)))
            {
                predicate = PredicateBuilder.True<NsMucLucNganSach>();
                predicate = predicate.And(x => x.Lns.Equals(item.LNS));
                predicate = predicate.And(x => x.L.Equals(item.L));
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.K));
                var lstK = _rootDataMlns.Where(predicate.Compile()).GroupBy(n => n.K).Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key });
                item.ItemsK = new ObservableCollection<ComboboxItem>(lstK);
                item.ItemsM = new ObservableCollection<ComboboxItem>();
                item.ItemsTM = new ObservableCollection<ComboboxItem>();
                item.ItemsTTM = new ObservableCollection<ComboboxItem>();
            }

            if (type != null && type.Equals(nameof(NhQtQuyetToanNienDoChiTietModel.K)))
            {
                predicate = PredicateBuilder.True<NsMucLucNganSach>();
                predicate = predicate.And(x => x.Lns.Equals(item.LNS));
                predicate = predicate.And(x => x.L.Equals(item.L));
                predicate = predicate.And(x => x.K.Equals(item.K));
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.M));
                var lstM = _rootDataMlns.Where(predicate.Compile()).GroupBy(n => n.M).Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key });
                item.ItemsM = new ObservableCollection<ComboboxItem>(lstM);
                item.ItemsTM = new ObservableCollection<ComboboxItem>();
                item.ItemsTTM = new ObservableCollection<ComboboxItem>();
            }

            if (type != null && type.Equals(nameof(NhQtQuyetToanNienDoChiTietModel.M)))
            {
                predicate = PredicateBuilder.True<NsMucLucNganSach>();
                predicate = predicate.And(x => x.Lns.Equals(item.LNS));
                predicate = predicate.And(x => x.L.Equals(item.L));
                predicate = predicate.And(x => x.K.Equals(item.K));
                predicate = predicate.And(x => x.M.Equals(item.M));
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.Tm));
                var lstTM = _rootDataMlns.Where(predicate.Compile()).GroupBy(n => n.Tm).Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key });
                item.ItemsTM = new ObservableCollection<ComboboxItem>(lstTM);
                item.ItemsTTM = new ObservableCollection<ComboboxItem>();
            }

            if (type != null && type.Equals(nameof(NhQtQuyetToanNienDoChiTietModel.TM)))
            {
                predicate = PredicateBuilder.True<NsMucLucNganSach>();
                predicate = predicate.And(x => x.Lns.Equals(item.LNS));
                predicate = predicate.And(x => x.L.Equals(item.L));
                predicate = predicate.And(x => x.K.Equals(item.K));
                predicate = predicate.And(x => x.M.Equals(item.M));
                predicate = predicate.And(x => x.Tm.Equals(item.TM));
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.Ttm));
                var lstTTM = _rootDataMlns.Where(predicate.Compile()).GroupBy(n => n.Ttm).Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key });
                item.ItemsTTM = new ObservableCollection<ComboboxItem>(lstTTM);
            }

            // Cập nhật mục lục ngân sách id
            if (isUpdateMlnsId)
            {
                predicate = PredicateBuilder.True<NsMucLucNganSach>();
                predicate = predicate.And(x => !string.IsNullOrEmpty(item.LNS) && x.Lns.Equals(item.LNS));
                predicate = predicate.And(x => !string.IsNullOrEmpty(item.L) && x.L.Equals(item.L));
                predicate = predicate.And(x => !string.IsNullOrEmpty(item.K) && x.K.Equals(item.K));
                predicate = predicate.And(x => !string.IsNullOrEmpty(item.M) && x.M.Equals(item.M));
                predicate = predicate.And(x => string.IsNullOrEmpty(item.TM) || x.Tm.Equals(item.TM));
                predicate = predicate.And(x => string.IsNullOrEmpty(item.TTM) || x.Ttm.Equals(item.TTM));
                NsMucLucNganSach currentMlns = _rootDataMlns.FirstOrDefault(predicate.Compile());
                if (currentMlns != null)
                {
                    item.IIdMlnsId = currentMlns.MlnsId;
                    item.IIdMucLucNganSachId = currentMlns.Id;
                }
                else
                {
                    item.IIdMlnsId = null;
                    item.IIdMucLucNganSachId = null;
                }
            }
        }

        private bool Validate()
        {
            return true;
        }
    }
}
