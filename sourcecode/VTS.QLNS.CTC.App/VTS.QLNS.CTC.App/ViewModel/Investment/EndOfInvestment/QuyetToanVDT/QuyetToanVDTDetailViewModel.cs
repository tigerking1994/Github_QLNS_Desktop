using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
    public class QuyetToanVDTDetailViewModel : DetailViewModelBase<VdtQtBcquyetToanNienDoModel, VdtQtBcquyetToanNienDoChiTiet1Model>
    {
        #region Private
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly IVdtQtBcQuyetToanNienDoService _service;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        List<VdtQtBcquyetToanNienDoChiTiet1Query> _lstDataRefer;
        #endregion

        #region Items
        public bool BIsShowPhanTich => (Model.IIDNguonVonID ?? 0) == 1 ? true : false;
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

        private LOAI_BC_QUYET_TOAN _tabIndex;
        public LOAI_BC_QUYET_TOAN TabIndex
        {
            get => _tabIndex;
            set {
                SetProperty(ref _tabIndex, value);
            }
        }

        private LOAI_TAB_QUYETTOAN _tabControlIndex;
        public LOAI_TAB_QUYETTOAN TabControlIndex
        {
            get => _tabControlIndex;
            set
            {
                SetProperty(ref _tabControlIndex, value);
                IsTongHop = (_tabControlIndex == LOAI_TAB_QUYETTOAN.TONG_HOP_SO_LIEU);
                IsPhanTich = (_tabControlIndex == LOAI_TAB_QUYETTOAN.PHAN_TICH_SO_LIEU);
                OnPropertyChanged(nameof(IsTongHop));
                OnPropertyChanged(nameof(IsPhanTich));
                
            }
        }

        private bool _isTongHop;
        public bool IsTongHop
        {
            get => _isTongHop;
            set => SetProperty(ref _isTongHop, value);
        }

        private bool _isPhanTich;
        public bool IsPhanTich
        {
            get => _isPhanTich;
            set => SetProperty(ref _isPhanTich, value);
        }

        private ObservableCollection<VdtQtBcQuyetToanNienDoPhanTichModel> _itemsPhanTich;
        public ObservableCollection<VdtQtBcQuyetToanNienDoPhanTichModel> ItemsPhanTich
        {
            get => _itemsPhanTich;
            set => SetProperty(ref _itemsPhanTich, value);
        }

        private VdtQtBcQuyetToanNienDoPhanTichModel _selectedPhanTich;
        public VdtQtBcQuyetToanNienDoPhanTichModel SelectedPhanTich
        {
            get => _selectedPhanTich;
            set => SetProperty(ref _selectedPhanTich, value);
        }
        #endregion

        public override string Title => "Quản lý đề nghị quyết toán niên độ";
        public override string Name => "Quản lý đề nghị quyết toán niên độ chi tiết";

        #region Header
        public string sVonTamUngChuaThuHoi => string.Format("Vốn tạm ứng theo chế độ chưa thu hồi của các năm trước nộp điều chỉnh giảm trong năm quyết toán    {0}", Model.INamKeHoach);
        public string sKeHoachThanhToanVonNamNay => string.Format("Kế hoạch và thanh toán vốn đầu tư năm {0}", Model.INamKeHoach);
        public string sThanhToanVonTamUng => string.Format("Thanh toán KLHT trong năm quyết toán phần vốn TU theo chế độ chưa thu hồi từ khởi công đến hết năm ngân sách trước năm quyết toán {0}", Model.INamKeHoach);
        public string sKeHoachThanhToanVon => string.Format("Kế hoạch và thanh toán vốn đầu tư các năm trước được kéo dài thời gian thực hiện và thanh toán sang năm {0}", Model.INamKeHoach);
        public string sVonDaQuyetToanTrongNam => string.Format("Dự toán năm {0}", Model.INamKeHoach);
        public string sLuyKeVonDaThanhToan => string.Format("Luỹ kế số vốn đã giải ngân từ K/C đến hết năm quyết toán", Model.INamKeHoach);
        public string sKeHoachVonNamNay => string.Format("Kế hoạch vốn đầu tư năm {0}", Model.INamKeHoach);
        #endregion

        public RelayCommand SaveDataCommand { get; }

        public QuyetToanVDTDetailViewModel(IVdtQtBcQuyetToanNienDoService service,
            ISessionService sessionService,
            ITongHopNguonNSDauTuService tonghopService,
            IMapper mapper)
        {
            _service = service;
            _sessionService = sessionService;
            _tonghopService = tonghopService;
            _mapper = mapper;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
        }

        #region RelayCommand Event
        public override void Init()
        {
            LoadData();
            TabControlIndex = LOAI_TAB_QUYETTOAN.TONG_HOP_SO_LIEU;
            OnPropertyChanged(nameof(TabControlIndex));
        }

        public override void LoadData(params object[] args)
        {
            TabIndex = LOAI_BC_QUYET_TOAN.TONG_HOP_SO_LIEU;
            
            //OnPropertyChanged(nameof(_tabControlIndex));
            LoadChungTuTongHop();
            LoadChungTuPhanTich();   
        }

        private void LoadChungTuTongHop()
        {
            _lstDataRefer = new List<VdtQtBcquyetToanNienDoChiTiet1Query>();
            List<VdtQtBcquyetToanNienDoChiTiet1Query> data = new List<VdtQtBcquyetToanNienDoChiTiet1Query>();
            int iCoQuanTaiChinh = Model.ICoQuanThanhToan ?? 0;
            List<VdtQtBcquyetToanNienDoChiTiet1Query> defaultData = new List<VdtQtBcquyetToanNienDoChiTiet1Query>();
            if (BIsTongHop)
            {
                foreach (Guid iId in Model.STongHop.Split(";").Select(n => Guid.Parse(n)))
                {
                    var child = _service.GetQuyetToanNienDoVonNamByParentId(iId);
                    if (child != null)
                        defaultData.AddRange(child);
                }
            }
            else
            {
                defaultData = _service.GetQuyetToanNienDoVonNamByParentId(Model.Id);
            }
            if (BIsTongHop || (defaultData != null && defaultData.Count != 0))
            {
                Items = _mapper.Map<ObservableCollection<VdtQtBcquyetToanNienDoChiTiet1Model>>(SetupViewData(defaultData));
            }
            else
            {
                data = _service.GetDeNghiQuyetToanNienDoDetail(Model.IIDMaDonViQuanLy, Model.INamKeHoach ?? 0, Model.IIDNguonVonID ?? 0);
                Items = _mapper.Map<ObservableCollection<VdtQtBcquyetToanNienDoChiTiet1Model>>(SetupViewData(data));
                _lstDataRefer = data.Clone();
            }

            foreach (var item in Items)
            {
                item.FVonConLaiHuyBoKeoDaiNamNay = item.FKHVNamTruocChuyenNamNay - item.FTongThanhToanVonKeoDaiNamNay - item.FGiaTriNamTruocChuyenNamSau;
                item.FVonConLaiHuyBoNamNay = item.FKHVNamNay - item.FTongKeHoachThanhToanVonNamNay - item.FGiaTriNamNayChuyenNamSau;
                item.FLuyKeTamUngChuaThuHoiChuyenSangNam =
                    item.FTamUngTheoCheDoChuaThuHoiNamTruoc - item.FGiaTriTamUngDieuChinhGiam - item.FTamUngNamTruocThuHoiNamNay
                    + item.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay + item.FTamUngTheoCheDoChuaThuHoiNamNay;
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
        }

        private void LoadChungTuPhanTich()
        {
            List<VdtQtBcQuyetToanNienDoPhanTichQuery> data = new List<VdtQtBcQuyetToanNienDoPhanTichQuery>();
            List<VdtQtBcQuyetToanNienDoPhanTichQuery> defaultDatas = new List<VdtQtBcQuyetToanNienDoPhanTichQuery>();
            defaultDatas = _service.GetBaoCaoQuyetToanNienDoPhanTichById(Model.Id).ToList();
            if (defaultDatas != null && defaultDatas.Count != 0)
            {
                ItemsPhanTich = _mapper.Map<ObservableCollection<VdtQtBcQuyetToanNienDoPhanTichModel>>(defaultDatas);
            }
            else
            {
                data = _service.GetBaoCaoQuyetToanNienDoPhanTich(Model.IIDMaDonViQuanLy, Model.INamKeHoach.Value, Model.IIDNguonVonID.Value).ToList();
                ItemsPhanTich = _mapper.Map<ObservableCollection<VdtQtBcQuyetToanNienDoPhanTichModel>>(data);
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSaveData()
        {
            //if (Items == null || !Items.Any(n => n.FGiaTriTamUngDieuChinhGiam != 0 || n.FGiaTriNamTruocChuyenNamSau != 0 || n.FGiaTriNamNayChuyenNamSau != 0))
            //{
            //    MessageBox.Show(string.Format(Resources.MsgErrorDataEmpty, "thông tin quyết toán"));
            //    return;
            //}

            List<VdtQtBcQuyetToanNienDoChiTiet01> lstData = new List<VdtQtBcQuyetToanNienDoChiTiet01>();
            foreach (var item in Items.Where(n => n.IIDDuAnID != Guid.Empty))
            {
                VdtQtBcQuyetToanNienDoChiTiet01 data = new VdtQtBcQuyetToanNienDoChiTiet01();
                data.DDateCreate = DateTime.Now;
                data.SUserCreate = _sessionService.Current.Principal;
                data.FGiaTriNamNayChuyenNamSau = item.FGiaTriNamNayChuyenNamSau;
                data.FGiaTriNamTruocChuyenNamSau = item.FGiaTriNamTruocChuyenNamSau;
                data.FGiaTriTamUngDieuChinhGiam = item.FGiaTriTamUngDieuChinhGiam;
                data.FLKThanhToanDenTrcNamQuyetToan = item.FLuyKeThanhToanNamTruoc;
                data.FTamUngChuaThuHoiTrcNamQuyetToan = item.FTamUngTheoCheDoChuaThuHoiNamTruoc;
                data.FThuHoiUngNamTrc = item.FTamUngNamTruocThuHoiNamNay;
                data.FChiTieuNamTrcChuyenSang = item.FKHVNamTruocChuyenNamNay;
                data.FThanhToanKLHTCTNamTrcChuyenSang = item.FTongThanhToanSuDungVonNamTruoc;
                data.FTamUngChuaThuHoiCTNamTrcChuyenSang = item.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay;
                data.FChiTieuNamNay = item.FKHVNamNay;
                data.ICoQuanThanhToan = item.ICoQuanThanhToan;
                data.FThanhToanKLHTCTNamNay = item.FTongThanhToanSuDungVonNamNay;
                data.FTamUngChuaThuHoiCTNamNay = item.FTamUngTheoCheDoChuaThuHoiNamNay;
                data.Id = Guid.NewGuid();
                data.IIdBcquyetToanNienDo = Model.Id;
                data.IIDDuAnID = item.IIDDuAnID;
                data.IIdLoaiCongTrinh = item.IIdLoaiCongTrinh;
                lstData.Add(data);
            }
            List<VdtQtBcQuyetToanNienDoPhanTich> lstDataPhanTich = new List<VdtQtBcQuyetToanNienDoPhanTich>();
            foreach (var item in ItemsPhanTich)
            {
                VdtQtBcQuyetToanNienDoPhanTich data = new VdtQtBcQuyetToanNienDoPhanTich()
                {
                    IIdBcQuyetToanNienDo = Model.Id,
                    FChiTieuNamNayKb = item.FChiTieuNamNayKb,
                    FChiTieuNamNayLc = item.FChiTieuNamNayLc,
                    FDnQuyetToanNamNay = item.FDnQuyetToanNamNay,
                    FDnQuyetToanNamTrc = item.FDnQuyetToanNamTrc,
                    FDuToanCnsChuaGiaiNganTaiCuc = item.FDuToanCnsChuaGiaiNganTaiCuc,
                    FDuToanCnsChuaGiaiNganTaiDv = item.FDuToanCnsChuaGiaiNganTaiDv,
                    FDuToanCnsChuaGiaiNganTaiKb = item.FDuToanCnsChuaGiaiNganTaiKb,
                    FDuToanThuHoi = item.FDuToanThuHoi,
                    FSoCapNamNay = item.FSoCapNamNay,
                    FSoCapNamTrcCs = item.FSoCapNamTrcCs,
                    FTuChuaThuHoiTaiCuc = item.FTuChuaThuHoiTaiCuc,
                    FTuChuaThuHoiTaiDonVi = item.FTuChuaThuHoiTaiDonVi,
                    IIdDuAnId = item.IIdDuAnId,
                    IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                };
                lstDataPhanTich.Add(data);
            }
            _service.AddRangePhanTich(Model.Id, lstDataPhanTich);
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
            VdtQtBcquyetToanNienDoChiTiet1Model item = (VdtQtBcquyetToanNienDoChiTiet1Model)sender;
            switch (args.PropertyName)
            {
                case nameof(VdtQtBcquyetToanNienDoChiTiet1Model.FGiaTriNamTruocChuyenNamSau):
                    item.FVonConLaiHuyBoKeoDaiNamNay = item.FKHVNamTruocChuyenNamNay - item.FTongThanhToanVonKeoDaiNamNay - item.FGiaTriNamTruocChuyenNamSau;
                    break;
                case nameof(VdtQtBcquyetToanNienDoChiTiet1Model.FGiaTriNamNayChuyenNamSau):
                    item.FVonConLaiHuyBoNamNay = item.FKHVNamNay - item.FTongKeHoachThanhToanVonNamNay - item.FGiaTriNamNayChuyenNamSau;
                    break;
                case nameof(VdtQtBcquyetToanNienDoChiTiet1Model.FGiaTriTamUngDieuChinhGiam):
                    item.FLuyKeTamUngChuaThuHoiChuyenSangNam =
                        item.FTamUngTheoCheDoChuaThuHoiNamTruoc - item.FGiaTriTamUngDieuChinhGiam - item.FTamUngNamTruocThuHoiNamNay
                        + item.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay + item.FTamUngTheoCheDoChuaThuHoiNamNay;
                    break;
            }
            OnPropertyChanged(nameof(Items));
        }

        private void SetupDataTongHop()
        {
            if (_lstDataRefer == null || _lstDataRefer.Count == 0) return;
            List<TongHopNguonNSDauTuQuery> lstData = new List<TongHopNguonNSDauTuQuery>();
            List<TongHopNguonNSDauTuQuery> lstLuyKeNamTruoc = _service.GetLuyKeQuyetToanNamTruoc(
                (int)PaymentTypeEnum.Type.THANH_TOAN, Model.IIDMaDonViQuanLy, Model.INamKeHoach.Value, Model.IIDNguonVonID.Value);
            foreach (var item in _lstDataRefer)
            {
                double fLuyKeKhvnNamTruoc = 0;
                if (lstLuyKeNamTruoc != null && lstLuyKeNamTruoc.Any(n => n.iID_DuAnID == item.IIDDuAnID && (n.sMaNguon == ((item.ICoQuanThanhToan ==
                (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVN_LENHCHI))))
                {
                    fLuyKeKhvnNamTruoc = lstLuyKeNamTruoc.FirstOrDefault(n => n.iID_DuAnID == item.IIDDuAnID && (n.sMaNguon ==
                    ((item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVN_LENHCHI))).fGiaTri ?? 0;
                }

                // col 16
                if (item.FKHVNamNay != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIDDuAnID,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVN_LENHCHI,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = item.FKHVNamNay + fLuyKeKhvnNamTruoc,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                // col 22
                if (item.FTongVonThanhToanNamNay != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIDDuAnID,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVN_LENHCHI,
                        fGiaTri = Items.Where(i => i.IIDDuAnID == item.IIDDuAnID).FirstOrDefault().FLuyKeTamUngChuaThuHoiChuyenSangNam,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                // col 24
                if (item.FLuyKeConDaThanhToanHetNamNay != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIDDuAnID,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TT_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TT_KHVN_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVN_LENHCHI,
                        fGiaTri = item.FLuyKeConDaThanhToanHetNamNay,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh,
                    });
                }
            }
            _tonghopService.InsertTongHopNguonDauTuQuyetToan(Model.Id, lstData);
        }

        private List<VdtQtBcquyetToanNienDoChiTiet1Model> SetupViewData(List<VdtQtBcquyetToanNienDoChiTiet1Query> lstData)
        {
            List<VdtQtBcquyetToanNienDoChiTiet1Model> results = new List<VdtQtBcquyetToanNienDoChiTiet1Model>();
            if (lstData == null) return results;
            List<VdtQtBcquyetToanNienDoChiTiet1Model> dataConvert = _mapper.Map<List<VdtQtBcquyetToanNienDoChiTiet1Model>>(lstData);

            if (Model.IIDNguonVonID == 1)
            {
                return SetupViewThanhToanNsqpData(dataConvert);
            }

            if (dataConvert.Any(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.CQTC))
            {
                results.Add(new VdtQtBcquyetToanNienDoChiTiet1Model()
                {
                    STenDuAn = "* CẤP QUA BỘ QUỐC PHÒNG",
                    IsHangCha = true
                });
                results.AddRange(dataConvert.Where(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.CQTC));
            }
            if (dataConvert.Any(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC))
            {
                results.Add(new VdtQtBcquyetToanNienDoChiTiet1Model()
                {
                    STenDuAn = "* CẤP QUA KHO BẠC",
                    IsHangCha = true
                });
                results.AddRange(dataConvert.Where(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC));
            }
            return results;
        }

        private List<VdtQtBcquyetToanNienDoChiTiet1Model> SetupViewThanhToanNsqpData(List<VdtQtBcquyetToanNienDoChiTiet1Model> lstData)
        {
            return lstData.GroupBy(n => new { n.IIDDuAnID, n.SMaDuAn, n.SDiaDiem, n.STenDuAn, n.FTongMucDauTu, n.IIdLoaiCongTrinh, n.STenLoaiCongTrinh })
                .Select(n => new VdtQtBcquyetToanNienDoChiTiet1Model()
                {
                    IIDDuAnID = n.Key.IIDDuAnID,
                    SMaDuAn = n.Key.SMaDuAn,
                    SDiaDiem = n.Key.SDiaDiem,
                    STenDuAn = n.Key.STenDuAn,
                    FTongMucDauTu = n.Key.FTongMucDauTu,
                    IIdLoaiCongTrinh = n.Key.IIdLoaiCongTrinh,
                    STenLoaiCongTrinh = n.Key.STenLoaiCongTrinh,
                    FLuyKeThanhToanNamTruoc = n.Sum(k => k.FLuyKeThanhToanNamTruoc),
                    FTamUngTheoCheDoChuaThuHoiNamTruoc = n.Sum(k => k.FTamUngTheoCheDoChuaThuHoiNamTruoc),
                    FGiaTriTamUngDieuChinhGiam = n.Sum(k => k.FGiaTriTamUngDieuChinhGiam),
                    FTamUngNamTruocThuHoiNamNay = n.Sum(k => k.FTamUngNamTruocThuHoiNamNay),
                    FKHVNamTruocChuyenNamNay = n.Sum(k => k.FKHVNamTruocChuyenNamNay),
                    FTongThanhToanVonKeoDaiNamNay = n.Sum(k => k.FTongThanhToanVonKeoDaiNamNay),
                    FTongThanhToanSuDungVonNamTruoc = n.Sum(k => k.FTongThanhToanSuDungVonNamTruoc),
                    FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay = n.Sum(k => k.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay),
                    FGiaTriNamTruocChuyenNamSau = n.Sum(k => k.FGiaTriNamTruocChuyenNamSau),
                    FVonConLaiHuyBoKeoDaiNamNay = n.Sum(k => k.FVonConLaiHuyBoKeoDaiNamNay),
                    FKHVNamNay = n.Sum(k => k.FKHVNamNay),
                    FTongKeHoachThanhToanVonNamNay = n.Sum(k => k.FTongKeHoachThanhToanVonNamNay),
                    FTongThanhToanSuDungVonNamNay = n.Sum(k => k.FTongThanhToanSuDungVonNamNay),
                    FTamUngTheoCheDoChuaThuHoiNamNay = n.Sum(k => k.FTamUngTheoCheDoChuaThuHoiNamNay),
                    FGiaTriNamNayChuyenNamSau = n.Sum(k => k.FGiaTriNamNayChuyenNamSau),
                    FVonConLaiHuyBoNamNay = n.Sum(k => k.FVonConLaiHuyBoNamNay),
                    FTongVonThanhToanNamNay = n.Sum(k => k.FTongVonThanhToanNamNay),
                    FLuyKeTamUngChuaThuHoiChuyenSangNam = n.Sum(k => k.FLuyKeTamUngChuaThuHoiChuyenSangNam),
                    FLuyKeConDaThanhToanHetNamNay = n.Sum(k => k.FLuyKeConDaThanhToanHetNamNay),
                    ICoQuanThanhToan = 1//n.Sum(k => k.ICoQuanThanhToan)
                }).ToList();
        }
        #endregion
    }
}
