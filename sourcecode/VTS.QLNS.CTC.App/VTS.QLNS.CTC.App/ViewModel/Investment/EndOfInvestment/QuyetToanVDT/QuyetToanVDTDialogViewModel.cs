using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.QuyetToanVDT
{
    public class QuyetToanVDTDialogViewModel : DialogViewModelBase<VdtQtBcquyetToanNienDoModel>
    {
        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly IMucLucNganSachService _mlNganSachService;
        private readonly IVdtQtBcQuyetToanNienDoService _service;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        #endregion

        public override string Name => "Quản lý đề nghị quyết toán niên độ";
        public override string Description => string.Format("{0} thông tin đề nghị quyết toán niên độ", (IsInsert ? "Tạo mới" : "Cập nhật"));

        private bool _isInsert;
        public bool IsInsert
        {
            get => _isInsert;
            set => SetProperty(ref _isInsert, value);
        }
        public Visibility HiddenTongHop => !BIsTongHop ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowTongHop => BIsTongHop ? Visibility.Visible : Visibility.Collapsed;
        public string sNguonVon { get; set; }

        #region Componer
        private bool _bIsTongHop;
        public bool BIsTongHop
        {
            get => _bIsTongHop;
            set => SetProperty(ref _bIsTongHop, value);
        }

        private ObservableCollection<VdtQtBcquyetToanNienDoModel> _itemsTongHopQuyetToan;
        public ObservableCollection<VdtQtBcquyetToanNienDoModel> ItemsTongHopQuyetToan
        {
            get => _itemsTongHopQuyetToan;
            set => SetProperty(ref _itemsTongHopQuyetToan, value);
        }

        private string _sSoDeNghi;
        public string SSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
        }

        private DateTime? _dNgayDeNghi;
        public DateTime? DNgayDeNghi
        {
            get => _dNgayDeNghi;
            set => SetProperty(ref _dNgayDeNghi, value);
        }

        private string _iNamKeHoach;
        public string INamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set => SetProperty(ref _cbxLoaiDonViSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }

        private ComboboxItem _cbxNguonVonSelected;
        public ComboboxItem CbxNguonVonSelected
        {
            get => _cbxNguonVonSelected;
            set => SetProperty(ref _cbxNguonVonSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxNguonVon;
        public ObservableCollection<ComboboxItem> CbxNguonVon
        {
            get => _cbxNguonVon;
            set => SetProperty(ref _cbxNguonVon, value);
        }

        private ComboboxItem _cbxLoaiThanhToanSelected;
        public ComboboxItem CbxLoaiThanhToanSelected
        {
            get => _cbxLoaiThanhToanSelected;
            set => SetProperty(ref _cbxLoaiThanhToanSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiThanhToan;
        public ObservableCollection<ComboboxItem> CbxLoaiThanhToan
        {
            get => _cbxLoaiThanhToan;
            set => SetProperty(ref _cbxLoaiThanhToan, value);
        }
        #endregion

        public QuyetToanVDTDialogViewModel(INsDonViService nsDonViService,
            IMucLucNganSachService mlNganSachService,
            ISessionService sessionService,
            INsNguonNganSachService nguonVonService,
            IVdtQtBcQuyetToanNienDoService service,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mlNganSachService = mlNganSachService;
            _nguonVonService = nguonVonService;
            _service = service;
            _mapper = mapper;
        }

        #region RelayCommand Event
        public override void Init()
        {
            LoadLoaiThanhToan();
            LoadComboBoxNguonVon();
            LoadComboBoxLoaiDonVi();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            SetDefaultData();
        }

        public override void OnSave()
        {
            int iNamKeHoach = 0;
            List<string> messageBuilder = new List<string>();
            if (Model == null) Model = new VdtQtBcquyetToanNienDoModel();
            if (CbxLoaiDonViSelected == null && !BIsTongHop)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Đơn vị quản lý"));
            }
            if (string.IsNullOrEmpty(SSoDeNghi))
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Số văn bản"));
            }
            if (!DNgayDeNghi.HasValue)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Ngày phê duyệt"));
            }
            if (string.IsNullOrEmpty(INamKeHoach))
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Năm kế hoạch"));
            }
            if (CbxNguonVonSelected == null)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Nguồn vốn"));
            }
            else if (!int.TryParse(INamKeHoach, out iNamKeHoach))
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorFormat, "Năm kế hoạch"));
            }
            if (CbxLoaiThanhToanSelected == null)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorRequire, "Loại thanh toán"));
            }
            if (messageBuilder.Count != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder));
                return;
            }

            var dataInsert = ConvertData();
            if (dataInsert.Id == Guid.Empty)
            {
                if (_service.CheckExistDeNghiQuyetToanNienDo(dataInsert.IIdMaDonViQuanLy, dataInsert.INamKeHoach.Value,
                    dataInsert.IIdNguonVonId.Value))
                {
                    messageBuilder.Add(string.Format(Resources.MsgErrorExitQuyetToanNienDo,
                        CbxLoaiDonViSelected.DisplayItem, dataInsert.INamKeHoach.Value.ToString(),
                        CbxNguonVonSelected.DisplayItem));
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }
                _service.Insert(dataInsert, _sessionService.Current.Principal);
                if (BIsTongHop)
                {
                    SaveDataTongHopDetail(dataInsert.Id);
                }
            }
            else
            {
                _service.Update(dataInsert,
                    _sessionService.Current.Principal);
                dataInsert.IsModified = true;
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
            Model = _mapper.Map<VdtQtBcquyetToanNienDoModel>(dataInsert);
            Model.STenDonVi = CbxLoaiDonViSelected.DisplayItem;
            Model.STenNguonVon = CbxNguonVonSelected.DisplayItem;
            SavedAction?.Invoke(Model);
        }
        #endregion

        #region Helper
        private VdtQtBcQuyetToanNienDo ConvertData()
        {
            var data = new VdtQtBcQuyetToanNienDo();
            data.DNgayDeNghi = DNgayDeNghi;
            if (!BIsTongHop)
            {
                data.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
                data.IIdMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
            }
            else
            {
                var lstDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
                if(lstDonVi != null && lstDonVi.Any(n=>n.Loai == "0"))
                {
                    data.IIdDonViQuanLyId = lstDonVi.FirstOrDefault(n => n.Loai == "0").Id;
                    data.IIdMaDonViQuanLy = lstDonVi.FirstOrDefault(n => n.Loai == "0").IIDMaDonVi;
                }
                data.STongHop = string.Join(";", ItemsTongHopQuyetToan.Select(n => n.Id.ToString()));
            }
            data.IIdNguonVonId = int.Parse(CbxNguonVonSelected.ValueItem);
            data.ILoaiThanhToan = int.Parse(CbxLoaiThanhToanSelected.ValueItem);
            data.INamKeHoach = int.Parse(INamKeHoach);
            data.SSoDeNghi = SSoDeNghi;
            data.SMoTa = SMoTa;
            data.Id = Model.Id;
            return data;
        }

        private void SetDefaultData()
        {
            if (!IsInsert || BIsTongHop)
            {
                if (!IsInsert)
                {
                    SSoDeNghi = Model.SSoDeNghi;
                    DNgayDeNghi = Model.DNgayDeNghi;
                }
                else
                {
                    SSoDeNghi = string.Empty;
                    DNgayDeNghi = DateTime.Now;
                }
                CbxLoaiDonViSelected = CbxLoaiDonVi.FirstOrDefault(n => n.ValueItem == Model.IIDMaDonViQuanLy);
                CbxLoaiThanhToanSelected = CbxLoaiThanhToan.FirstOrDefault(n => n.ValueItem == (Model.ILoaiThanhToan ?? 0).ToString());
                CbxNguonVonSelected = CbxNguonVon.FirstOrDefault(n => n.ValueItem == (Model.IIDNguonVonID ?? 0).ToString());
                INamKeHoach = (Model.INamKeHoach ?? 0).ToString();
                SMoTa = Model.SMoTa;
            }
            else
            {
                CbxLoaiDonViSelected = null;
                CbxLoaiThanhToanSelected = null;
                CbxNguonVonSelected = null;
                INamKeHoach = null;
                SSoDeNghi = null;
                DNgayDeNghi = DateTime.Now;
                SMoTa = string.Empty;
            }
            OnPropertyChanged(nameof(BIsTongHop));
            OnPropertyChanged(nameof(ItemsTongHopQuyetToan));
            OnPropertyChanged(nameof(HiddenTongHop));
            OnPropertyChanged(nameof(ShowTongHop));
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(SSoDeNghi));
            OnPropertyChanged(nameof(DNgayDeNghi));
            OnPropertyChanged(nameof(CbxLoaiDonViSelected));
            OnPropertyChanged(nameof(CbxLoaiThanhToanSelected));
            OnPropertyChanged(nameof(CbxNguonVonSelected));
        }

        private void LoadComboBoxLoaiDonVi()
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => _lstDonViExclude.Contains(n.Loai))
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi), HiddenValue = n.Id.ToString() });
            _cbxLoaiDonVi = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
            OnPropertyChanged(nameof(CbxLoaiDonVi));
        }

        private void LoadComboBoxNguonVon()
        {
            var cbxNguonVon = _nguonVonService.FindAll().OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
            _cbxNguonVon = new ObservableCollection<ComboboxItem>(cbxNguonVon);
            OnPropertyChanged(nameof(CbxNguonVon));
        }

        public void LoadLoaiThanhToan()
        {
            List<ComboboxItem> lstItem = new List<ComboboxItem>();
            lstItem.Add(new ComboboxItem()
            {
                ValueItem = ((int)PaymentTypeEnum.Type.THANH_TOAN).ToString(),
                DisplayItem = "Báo cáo quyết toán nguồn vốn đầu tư năm"
            });
            lstItem.Add(new ComboboxItem()
            {
                ValueItem = ((int)PaymentTypeEnum.Type.TAM_UNG).ToString(),
                DisplayItem = "Báo cáo kế hoạch và thanh toán vốn đầu tư - ứng trước kế hoạch vốn năm sau"
            });
            CbxLoaiThanhToan = new ObservableCollection<ComboboxItem>(lstItem);
            OnPropertyChanged(nameof(CbxLoaiThanhToan));
        }

        private void SaveDataTongHopDetail(Guid iIdChungTu)
        {
            List<VdtQtBcQuyetToanNienDoChiTiet01> lstTongHop = new List<VdtQtBcQuyetToanNienDoChiTiet01>();
            List<VdtQtBcQuyetToanNienDoPhanTich> lstPhanTich = new List<VdtQtBcQuyetToanNienDoPhanTich>();
            if (ItemsTongHopQuyetToan == null) return;
            foreach (var item in ItemsTongHopQuyetToan)
            {
                var dataTongHops = _service.GetDenghiQuyetToanNienDoChiTiet01ByParent(item.Id);
                if (dataTongHops != null)
                    lstTongHop.AddRange(dataTongHops);

                var dataPhanTichs = _service.GetBcQuyetToanNienDoPhanTich(item.Id);
                if (dataPhanTichs != null)
                    lstPhanTich.AddRange(dataPhanTichs);
            }

            if(lstTongHop != null)
            {
                var dataInsert = lstTongHop.GroupBy(n => new { n.IIDDuAnID, n.ICoQuanThanhToan })
                    .Select(n => new VdtQtBcQuyetToanNienDoChiTiet01() {
                        Id = Guid.NewGuid(),
                        IIdBcquyetToanNienDo = iIdChungTu,
                        IIDDuAnID = n.Key.IIDDuAnID,
                        ICoQuanThanhToan = n.Key.ICoQuanThanhToan,
                        FGiaTriNamTruocChuyenNamSau = n.Sum(k=>k.FGiaTriNamTruocChuyenNamSau),
                        FGiaTriNamNayChuyenNamSau = n.Sum(k => k.FGiaTriNamNayChuyenNamSau),
                        FGiaTriTamUngDieuChinhGiam = n.Sum(k => k.FGiaTriTamUngDieuChinhGiam),
                        SUserCreate = _sessionService.Current.Principal,
                        DDateCreate = DateTime.Now,
                        FGiaTriUngChuyenNamSau = n.Sum(k => k.FGiaTriUngChuyenNamSau),
                        FGiaTriThuHoiTheoGiaiNganThucTe = n.Sum(k => k.FGiaTriThuHoiTheoGiaiNganThucTe),
                        FLKThanhToanDenTrcNamQuyetToan = n.Sum(k => k.FLKThanhToanDenTrcNamQuyetToan),
                        FTamUngChuaThuHoiTrcNamQuyetToan = n.Sum(k => k.FTamUngChuaThuHoiTrcNamQuyetToan),
                        FThuHoiUngNamTrc = n.Sum(k => k.FThuHoiUngNamTrc),
                        FChiTieuNamTrcChuyenSang = n.Sum(k => k.FChiTieuNamTrcChuyenSang),
                        FThanhToanKLHTCTNamTrcChuyenSang = n.Sum(k => k.FThanhToanKLHTCTNamTrcChuyenSang),
                        FTamUngChuaThuHoiCTNamTrcChuyenSang = n.Sum(k => k.FTamUngChuaThuHoiCTNamTrcChuyenSang),
                        FChiTieuNamNay = n.Sum(k => k.FChiTieuNamNay),
                        FThanhToanKLHTCTNamNay = n.Sum(k => k.FThanhToanKLHTCTNamNay),
                        FTamUngChuaThuHoiCTNamNay = n.Sum(k => k.FTamUngChuaThuHoiCTNamNay),
                        FKHUngTrcChuaThuHoiTrcNamQuyetToan = n.Sum(k => k.FKHUngTrcChuaThuHoiTrcNamQuyetToan),
                        FLKThanhToanDenTrcNamQuyetToanKHUng = n.Sum(k => k.FLKThanhToanDenTrcNamQuyetToanKHUng),
                        FThanhToanKHUngNamTrcChuyenSang = n.Sum(k => k.FThanhToanKHUngNamTrcChuyenSang),
                        FThuHoiUngTruoc = n.Sum(k => k.FThuHoiUngTruoc),
                        FKHUngNamNay = n.Sum(k => k.FKHUngNamNay),
                        FThanhToanKHUngNamNay = n.Sum(k => k.FThanhToanKHUngNamNay),
                    }).ToList();
                _service.InsertVdtQtBcquyetToanNienDoChiTiet01(iIdChungTu, dataInsert);
            }

            if(lstPhanTich != null)
            {
                var dataInsert = lstPhanTich.GroupBy(n => n.IIdDuAnId)
                    .Select(n => new VdtQtBcQuyetToanNienDoPhanTich()
                    {
                        Id = Guid.NewGuid(),
                        IIdDuAnId = n.Key,
                        IIdBcQuyetToanNienDo = iIdChungTu,
                        FDuToanCnsChuaGiaiNganTaiKb = n.Sum(k=>k.FDuToanCnsChuaGiaiNganTaiKb),
                        FDuToanCnsChuaGiaiNganTaiDv = n.Sum(k => k.FDuToanCnsChuaGiaiNganTaiDv),
                        FDuToanCnsChuaGiaiNganTaiCuc = n.Sum(k => k.FDuToanCnsChuaGiaiNganTaiCuc),
                        FTuChuaThuHoiTaiCuc = n.Sum(k => k.FTuChuaThuHoiTaiCuc),
                        FTuChuaThuHoiTaiDonVi = n.Sum(k => k.FTuChuaThuHoiTaiDonVi),
                        FChiTieuNamNayKb = n.Sum(k => k.FChiTieuNamNayKb),
                        FChiTieuNamNayLc = n.Sum(k => k.FChiTieuNamNayLc),
                        FSoCapNamTrcCs = n.Sum(k => k.FSoCapNamTrcCs),
                        FSoCapNamNay = n.Sum(k => k.FSoCapNamNay),
                        FDnQuyetToanNamTrc = n.Sum(k => k.FDnQuyetToanNamTrc),
                        FDnQuyetToanNamNay = n.Sum(k => k.FDnQuyetToanNamNay),
                        FDuToanThuHoi = n.Sum(k => k.FDuToanThuHoi)
                    }).ToList();
                _service.AddRangePhanTich(iIdChungTu, dataInsert);
            }
        }
        #endregion
    }
}
