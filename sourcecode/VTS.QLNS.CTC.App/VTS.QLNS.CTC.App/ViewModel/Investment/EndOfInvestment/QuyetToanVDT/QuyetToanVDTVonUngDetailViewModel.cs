using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.QuyetToanVDT
{

    public class QuyetToanVDTVonUngDetailViewModel : DetailViewModelBase<VdtQtBcquyetToanNienDoModel, BcquyetToanNienDoVonUngChiTietModel>
    {
        #region Private
        private readonly IVdtQtBcQuyetToanNienDoService _service;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        List<BcquyetToanNienDoVonUngChiTietQuery> _lstDataRefer;
        #endregion

        public bool IsSaveData => !BIsDetail && !BIsTongHop;

        private bool _bIsDetail;
        public bool BIsDetail
        {
            get => _bIsDetail;
            set => SetProperty(ref _bIsDetail, value);
        }

        private bool _bIsTongHop;
        public bool BIsTongHop
        {
            get => _bIsTongHop;
            set => SetProperty(ref _bIsTongHop, value);
        }

        public override string Title => "Quản lý đề nghị quyết toán niên độ";
        public override string Name => "Quản lý đề nghị quyết toán niên độ chi tiết";

        public RelayCommand SaveDataCommand { get; }

        public QuyetToanVDTVonUngDetailViewModel(IVdtQtBcQuyetToanNienDoService service,
            ITongHopNguonNSDauTuService tonghopService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _tonghopService = tonghopService;
            _service = service;
            _sessionService = sessionService;
            _mapper = mapper;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
        }

        #region RelayCommand Event
        public override void Init()
        {
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            _lstDataRefer = new List<BcquyetToanNienDoVonUngChiTietQuery>();
            List<BcquyetToanNienDoVonUngChiTietQuery> data = new List<BcquyetToanNienDoVonUngChiTietQuery>();
            List<BcquyetToanNienDoVonUngChiTietQuery> defaultData = new List<BcquyetToanNienDoVonUngChiTietQuery>();
            if (BIsTongHop)
            {
                foreach (Guid iId in Model.STongHop.Split(";").Select(n => Guid.Parse(n)))
                {
                    var child = _service.GetQuyetToanNienDoVonUngByParentId(iId);
                    if (child != null)
                        defaultData.AddRange(child);
                }
            }
            else
            {
                defaultData = _service.GetQuyetToanNienDoVonUngByParentId(Model.Id);
            }
            if (BIsTongHop || (defaultData != null && defaultData.Count != 0))
            {
                Items = _mapper.Map<ObservableCollection<BcquyetToanNienDoVonUngChiTietModel>>(SetupViewData(defaultData));
            }
            else
            {
                data = _service.GetDeNghiQuyetToanNienDoVonUngDetail(Model.IIDMaDonViQuanLy, Model.INamKeHoach ?? 0, Model.IIDNguonVonID ?? 0);
                Items = _mapper.Map<ObservableCollection<BcquyetToanNienDoVonUngChiTietModel>>(SetupViewData(data));
                _lstDataRefer = data.Clone();
            }
            foreach (var item in Items)
            {
                item.FTongSoVonDaThanhToanThuHoi = item.FTongSoVonDaThanhToanThuHoi - item.FGiaTriThuHoiTheoGiaiNganThucTe;
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSaveData()
        {
            //if (Items == null || !Items.Any(n => n.FGiaTriThuHoiTheoGiaiNganThucTe != 0))
            //{
            //    MessageBox.Show(Resources.MsgErrorDataEmpty, "thông tin quyết toán");
            //    return;
            //}

            List<VdtQtBcQuyetToanNienDoChiTiet01> lstData = new List<VdtQtBcQuyetToanNienDoChiTiet01>();
            foreach (var item in Items.Where(n => !n.IsHangCha))
            {
                VdtQtBcQuyetToanNienDoChiTiet01 data = new VdtQtBcQuyetToanNienDoChiTiet01();
                data.DDateCreate = DateTime.Now;
                data.SUserCreate = _sessionService.Current.Principal;
                data.FGiaTriUngChuyenNamSau = item.FKHVUChuaThuHoiChuyenNamSau;
                data.FGiaTriThuHoiTheoGiaiNganThucTe = item.FGiaTriThuHoiTheoGiaiNganThucTe;
                data.FKHUngTrcChuaThuHoiTrcNamQuyetToan = item.FUngTruocChuaThuHoiNamTruoc;
                data.FLKThanhToanDenTrcNamQuyetToanKHUng = item.FLuyKeThanhToanNamTruoc;
                data.FThanhToanKHUngNamTrcChuyenSang = item.FVonKeoDaiDaThanhToanNamNay;
                data.FThuHoiUngTruoc = item.FThuHoiVonNamNay;
                data.FKHUngNamNay = item.FKHVUNamNay;
                data.ICoQuanThanhToan = item.ICoQuanThanhToan;
                data.FThanhToanKHUngNamNay = item.FVonDaThanhToanNamNay;
                data.IIdLoaiCongTrinh = item.IIdLoaiCongTrinh;
                data.Id = Guid.NewGuid();
                data.IIdBcquyetToanNienDo = Model.Id;
                data.IIDDuAnID = item.IIDDuAnID;
                lstData.Add(data);
            }
            _service.InsertVdtQtBcquyetToanNienDoChiTiet01(Model.Id, lstData);
            if (!BIsTongHop)
            {
                _tonghopService.InsertTongHopNguonDauTu_Tang(LOAI_CHUNG_TU.QUYET_TOAN, (int)TypeExecute.Update, Model.Id);
                SetupDataTongHop();
            }
            MessageBox.Show(Resources.MsgSaveDone);
            LoadData();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            BcquyetToanNienDoVonUngChiTietModel item = (BcquyetToanNienDoVonUngChiTietModel)sender;
            if (args.PropertyName != nameof(BcquyetToanNienDoVonUngChiTietModel.FGiaTriThuHoiTheoGiaiNganThucTe)) return;
            item.FTongSoVonDaThanhToanThuHoi = item.FTongSoVonDaThanhToanThuHoi - item.FGiaTriThuHoiTheoGiaiNganThucTe;
            OnPropertyChanged(nameof(Items));
        }

        private void SetupDataTongHop()
        {
            if (_lstDataRefer == null && _lstDataRefer.Count == 0) return;
            List<TongHopNguonNSDauTuQuery> lstData = new List<TongHopNguonNSDauTuQuery>();
            List<TongHopNguonNSDauTuQuery> lstLuyKeNamTruoc = _service.GetLuyKeQuyetToanNamTruoc(
                (int)PaymentTypeEnum.Type.TAM_UNG, Model.IIDMaDonViQuanLy, Model.INamKeHoach.Value, Model.IIDNguonVonID.Value);
            foreach (var item in _lstDataRefer)
            {
                double fLuyKeKhvuNamTruoc = 0;
                double fLuyKeTamUngChuaThuHoi = 0;
                if (lstLuyKeNamTruoc != null)
                {
                    if (lstLuyKeNamTruoc.Any(n => n.iID_DuAnID == item.IIDDuAnID && (n.sMaNguon == ((item.ICoQuanThanhToan ==
                      (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI))))
                    {
                        fLuyKeKhvuNamTruoc = lstLuyKeNamTruoc.FirstOrDefault(n => n.iID_DuAnID == item.IIDDuAnID && (n.sMaNguon ==
                        ((item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI))).fGiaTri ?? 0;
                    }
                    if (lstLuyKeNamTruoc.Any(n => n.iID_DuAnID == item.IIDDuAnID && (n.sMaNguon == ((item.ICoQuanThanhToan ==
                      (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI))))
                    {
                        fLuyKeTamUngChuaThuHoi = lstLuyKeNamTruoc.FirstOrDefault(n => n.iID_DuAnID == item.IIDDuAnID && (n.sMaDich ==
                        ((item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_LENHCHI))).fGiaTri ?? 0;
                    }
                }

                var FGiaTriThuHoiTheoGiaiNganThucTe = item.FGiaTriThuHoiTheoGiaiNganThucTe - Items.FirstOrDefault(n => n.IIDDuAnID == item.IIDDuAnID).FGiaTriThuHoiTheoGiaiNganThucTe;
                if (FGiaTriThuHoiTheoGiaiNganThucTe != 0 && (Model.IIDNguonVonID ?? 0) == 1)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIDDuAnID,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TTKLHT_CHUA_PHANBO_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TTKLHT_CHUA_PHANBO_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI,
                        fGiaTri = item.FGiaTriThuHoiTheoGiaiNganThucTe,
                    });
                }

                // col 10 - 
                double FTongSoVonDaThanhToanThuHoi = item.FLuyKeThanhToanNamTruoc
                + ((item.FThanhToanKLHTNamTruocChuyenSang + item.FThanhToanUngNamTruocChuyenSang) - (item.FThuHoiTamUngNamNayVonNamTruoc + item.FThuHoiTamUngNamTruocVonNamTruoc))
                + ((item.FThanhToanKLHTTamUngNamNay + item.FThanhToanUngNamNay) - (item.FThuHoiTamUngNamNay + item.FThuHoiTamUngNamTruoc));

                if (FTongSoVonDaThanhToanThuHoi != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIDDuAnID,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TT_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TT_KHVU_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI,
                        fGiaTri = FTongSoVonDaThanhToanThuHoi
                    });
                }

                // col 8 - luy ke tam ung chua thu hoi
                lstData.Add(new TongHopNguonNSDauTuQuery()
                {
                    iID_ChungTu = Model.Id,
                    iID_DuAnID = item.IIDDuAnID,
                    sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                    sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_LENHCHI,
                    sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI,
                    fGiaTri = item.FThanhToanUngNamNay - (item.FThuHoiTamUngNamNay + item.FThuHoiTamUngNamTruoc) + fLuyKeTamUngChuaThuHoi
                });

                // col 9 - 
                double FKHVUChuaThuHoiChuyenNamSau = (item.FUngTruocChuaThuHoiNamTruoc - item.FThuHoiVonNamNay + item.FKHVUNamNay);
                if (FKHVUChuaThuHoiChuyenNamSau != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIDDuAnID,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_UNGTRUOC_CHUATHUHOI_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_UNGTRUOC_CHUATHUHOI_LENHCHI,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = FKHVUChuaThuHoiChuyenNamSau
                    });
                }

                //col 9 - 10
                double fKeHoachUngChuyenNamSau = FKHVUChuaThuHoiChuyenNamSau - FTongSoVonDaThanhToanThuHoi;
                if (fKeHoachUngChuyenNamSau != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIDDuAnID,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_UNG_KHOBAC_CHUYENNAMTRUOC : LOAI_CHUNG_TU.QT_UNG_LENHCHI_CHUYENNAMTRUOC,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = fKeHoachUngChuyenNamSau
                    });
                }

                // col 7 - ung nam truoc
                double fLuyKeKHVU = item.FKHVUNamNay - item.fLuyKeUngNamTruoc;
                if (fLuyKeKHVU != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIDDuAnID,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = fLuyKeKHVU
                    });
                }
            }

            foreach (var item in Items)
            {
                // col 5
                if (item.FThuHoiVonNamNay != 0 && (Model.IIDNguonVonID ?? 0) == 1)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIDDuAnID,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.KHVU_KHOBAC : LOAI_CHUNG_TU.KHVU_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.KHVN_KHOBAC : LOAI_CHUNG_TU.KHVN_LENHCHI,
                        fGiaTri = item.FThuHoiVonNamNay,
                        iThuHoiTUCheDo = 1
                    });
                }
            }
            _tonghopService.InsertTongHopNguonDauTuQuyetToan(Model.Id, lstData);
        }

        private List<BcquyetToanNienDoVonUngChiTietModel> SetupViewData(List<BcquyetToanNienDoVonUngChiTietQuery> lstData)
        {
            List<BcquyetToanNienDoVonUngChiTietModel> results = new List<BcquyetToanNienDoVonUngChiTietModel>();
            if (lstData == null) return results;
            List<BcquyetToanNienDoVonUngChiTietModel> dataConvert = _mapper.Map<List<BcquyetToanNienDoVonUngChiTietModel>>(lstData);

            if (dataConvert.Any(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.CQTC))
            {
                results.Add(new BcquyetToanNienDoVonUngChiTietModel()
                {
                    STenDuAn = "* CẤP QUA BỘ QUỐC PHÒNG",
                    IsHangCha = true
                });
                results.AddRange(dataConvert.Where(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.CQTC));
            }
            if (dataConvert.Any(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC))
            {
                results.Add(new BcquyetToanNienDoVonUngChiTietModel()
                {
                    STenDuAn = "* CẤP QUA KHO BẠC",
                    IsHangCha = true
                });
                results.AddRange(dataConvert.Where(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC));
            }
            return results;
        }
        #endregion
    }
}
