using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.RequestSettlement
{
    public class RequestSettlementDialogViewModel : DialogViewModelBase<RequestSettlementDialogModel>
    {
        private readonly IDmChuDauTuService _nsChuDauTuService;
        private readonly INsDonViService _nsDonViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IVdtDaDuAnService _vdtDaDuAnService;
        private readonly IVdtDeNghiQuyetToanService _iVdtDeNghiQuyetToanService;
        private readonly IVdtQddtKhlcnhaThauService _vdtQddtKhlcnhaThauService;
        private readonly IVdtQtDeNghiQuyetToanNguonVonService _vdtQtDeNghiQuyetToanNguonVonService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly IDmChuDauTuService _iDmChuDauTuService;
        private readonly ILog _logger;

        public override string Name => "Quản lý đề nghị quyết toán dự án hoàn thành";
        public override string Description => string.Format("{0} thông tin đề nghị quyết toán", BIsAdd ? "Thêm mới" : "Cập nhật");
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.RequestSettlement.RequestSettlementDialog);
        public override PackIconKind IconKind => PackIconKind.Projector;
        public VdtQtDeNghiQuyetToan Entity;
        public List<VdtDaDuAn> ListDuAn;
        public bool BIsAdd => Model.Id == Guid.Empty;

        private ObservableCollection<ComboboxItem> _dataDonViQuanLy;
        public ObservableCollection<ComboboxItem> DataDonViQuanLy
        {
            get => _dataDonViQuanLy;
            set => SetProperty(ref _dataDonViQuanLy, value);
        }

        private ComboboxItem _selectedDonViQuanLy;
        public ComboboxItem SelectedDonViQuanLy
        {
            get => _selectedDonViQuanLy;
            set
            {
                if (SetProperty(ref _selectedDonViQuanLy, value))
                {
                    LoadComboboxDuAn();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemLoaiQuyetToan;
        public ObservableCollection<ComboboxItem> ItemLoaiQuyetToan
        {
            get => _itemLoaiQuyetToan;
            set => SetProperty(ref _itemLoaiQuyetToan, value);
        }

        private ComboboxItem _selectedLoaiQuyetToan;
        public ComboboxItem SelectedLoaiQuyetToan
        {
            get => _selectedLoaiQuyetToan;
            set
            {
                if (SetProperty(ref _selectedLoaiQuyetToan, value))
                {
                    LoadQuyetDinh();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemQuyetDinh;
        public ObservableCollection<ComboboxItem> ItemQuyetDinh
        {
            get => _itemQuyetDinh;
            set => SetProperty(ref _itemQuyetDinh, value);
        }

        private ComboboxItem _selectedQuyetDinh;
        public ComboboxItem SelectedQuyetDinh
        {
            get => _selectedQuyetDinh;
            set
            {
                if (SetProperty(ref _selectedQuyetDinh, value))
                {
                    LoadDuAnInfo();
                    LoadDataNguonVonByQDDauTuId();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataDuAn;
        public ObservableCollection<ComboboxItem> DataDuAn
        {
            get => _dataDuAn;
            set => SetProperty(ref _dataDuAn, value);
        }

        private ComboboxItem _selectedDuAn;
        public ComboboxItem SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                if (SetProperty(ref _selectedDuAn, value) && _selectedDuAn != null)
                {
                    LoadQuyetDinh();
                }
            }
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.VdtQtQuyetToanNguonVonModel> _dataNguonVon;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.VdtQtQuyetToanNguonVonModel> DataNguonVon
        {
            get => _dataNguonVon;
            set => SetProperty(ref _dataNguonVon, value);
        }

        public RequestSettlementDialogViewModel(IDmChuDauTuService nsChuDauTuService,
            INsDonViService nsDonViService,
            ISessionService sessionService,
            IVdtDaDuAnService vdtDaDuAnService,
            IVdtDeNghiQuyetToanService iVdtDeNghiQuyetToanService,
            IVdtDaDuToanService vdtDaDuToanService,
            IVdtQddtKhlcnhaThauService vdtQddtKhlcnhaThauService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IVdtQtDeNghiQuyetToanNguonVonService vdtQtDeNghiQuyetToanNguonVonService,
            IDmChuDauTuService iDmChuDauTuService,
            ILog logger,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _iVdtDeNghiQuyetToanService = iVdtDeNghiQuyetToanService;
            _nsChuDauTuService = nsChuDauTuService;
            _nsDonViService = nsDonViService;
            _vdtDaDuAnService = vdtDaDuAnService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _vdtQddtKhlcnhaThauService = vdtQddtKhlcnhaThauService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _vdtQtDeNghiQuyetToanNguonVonService = vdtQtDeNghiQuyetToanNguonVonService;
            _iDmChuDauTuService = iDmChuDauTuService;
            _logger = logger;
            _mapper = mapper;
        }

        public override void Init()
        {
            try
            {
                ResetCondition();
                Entity = _iVdtDeNghiQuyetToanService.Find(Model.Id);
                LoadCombobxDonVi();
                LoadLoaiQuyetToan();
                LoadQuyetDinh();
                if (Model.Id == Guid.Empty)
                {
                    Model.ThoiGianKhoiCong = DateTime.Now;
                    Model.ThoiGianHoanThanh = DateTime.Now;
                    Model.NgayDuyet = DateTime.Now;
                    Model.NgayNhan = DateTime.Now;
                }
                else
                {
                    if (Entity != null && Entity.Id != Guid.Empty)
                    {
                        Model.SoBaoCao = Entity.SSoBaoCao;
                        Model.NgayDuyet = Entity.DThoiGianLapBaoCao;
                        Model.NguoiDuyet = Entity.SNguoiLap;
                        Model.NgayNhan = Entity.DThoiGianNhanBaoCao;
                        Model.NguoiNhan = Entity.SNguoiNhan;
                        Model.ThoiGianKhoiCong = Entity.DThoiGianKhoiCong;
                        Model.ThoiGianHoanThanh = Entity.DThoiGianHoanThanh;
                        Model.GiaTriQuyetToan = Entity.FGiaTriDeNghiQuyetToan;

                        Model.ChiPhiThietHai = Entity.FChiPhiThietHai;
                        Model.ChiPhiKhongTaoTaiSan = Entity.FChiPhiKhongTaoNenTaiSan;
                        Model.DaiHanThuocQuanLy = Entity.FTaiSanDaiHanThuocCDTQuanLy;
                        Model.DaiHanDonViKhacQuanLy = Entity.FTaiSanDaiHanDonViKhacQuanLy;
                        Model.NganHanThuocQuanLy = Entity.FTaiSanNganHanThuocCDTQuanLy;
                        Model.NganHanDonViKhacQuanLy = Entity.FTaiSanNganHanDonViKhacQuanLy;
                        Model.GhiChu = Entity.SMoTa;
                    }
                    else
                    {
                        Entity = new VdtQtDeNghiQuyetToan();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ResetCondition()
        {
            DataNguonVon = new ObservableCollection<VdtQtQuyetToanNguonVonModel>();
            OnPropertyChanged(nameof(DataNguonVon));
        }

        private List<NguoiDungDonVi> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionService.Current.Principal));
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            List<NguoiDungDonVi> nsDungDonVis = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return nsDungDonVis;
        }

        private void LoadCombobxDonVi()
        {
            List<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
            List<NguoiDungDonVi> listNguoiDungDonVi = GetListNguoiDungDonVi();
            if (listNguoiDungDonVi != null && listNguoiDungDonVi.Count > 0)
            {
                listDonVi = listDonVi.Where(n => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(n.IIDMaDonVi)).ToList();
            }
            else
            {
                listDonVi = new List<DonVi>();
            }
            DataDonViQuanLy = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);
            DataDonViQuanLy.Insert(0, new ComboboxItem { DisplayItem = "Chọn đơn vị", ValueItem = string.Empty });
            if (DataDonViQuanLy != null && DataDonViQuanLy.Count > 0)
            {

                if (Entity != null && !string.IsNullOrEmpty(Entity.IIDMaDonVi))
                {
                    SelectedDonViQuanLy = DataDonViQuanLy.FirstOrDefault(n => n.ValueItem == Entity.IIDMaDonVi);
                }
                else
                {
                    SelectedDonViQuanLy = DataDonViQuanLy.FirstOrDefault();
                }
            }
        }

        private void LoadLoaiQuyetToan()
        {
            _itemLoaiQuyetToan = new ObservableCollection<ComboboxItem>();
            _itemLoaiQuyetToan.Insert(0, new ComboboxItem { DisplayItem = "--Chọn--", ValueItem = string.Empty });
            _itemLoaiQuyetToan.Insert(1, new ComboboxItem { DisplayItem = "Theo hạng mục", ValueItem = LOAI_QUYETTOAN_DUAN_HOANTHANH.TypeName.THEO_HANGMUC});
            _itemLoaiQuyetToan.Insert(2, new ComboboxItem { DisplayItem = "Theo gói thầu", ValueItem = LOAI_QUYETTOAN_DUAN_HOANTHANH.TypeName.THEO_GOITHAU});
            if (_itemLoaiQuyetToan != null && _itemLoaiQuyetToan.Count > 0)
            {

                if (Entity != null && !string.IsNullOrEmpty(Entity.IIDMaDonVi))
                {
                    SelectedLoaiQuyetToan = _itemLoaiQuyetToan.FirstOrDefault(n => n.ValueItem == Entity.iID_LoaiQuyetToan);
                }
                else
                {
                    SelectedLoaiQuyetToan = _itemLoaiQuyetToan.FirstOrDefault();
                }
            }
        }

        private void LoadQuyetDinh()
        {
            ItemQuyetDinh = new ObservableCollection<ComboboxItem>();
            if (SelectedLoaiQuyetToan != null && SelectedLoaiQuyetToan.ValueItem == LOAI_QUYETTOAN_DUAN_HOANTHANH.TypeName.THEO_HANGMUC)
            {
                List<VdtDaDuToanQuery> dutoans = _vdtDaDuToanService.GetDuToanByDuAnId(Guid.Parse(SelectedDuAn._hiddenValue)).ToList();
                if (dutoans != null && dutoans.Count > 0)
                {
                    int i = 0;
                    foreach (var item in dutoans)
                    {
                        ItemQuyetDinh.Insert(i++, new ComboboxItem { DisplayItem = item.SSoQuyetDinh, ValueItem = item.Id.ToString() });
                    }
                }
            }
            if (SelectedLoaiQuyetToan != null && SelectedDuAn != null && SelectedLoaiQuyetToan.ValueItem == LOAI_QUYETTOAN_DUAN_HOANTHANH.TypeName.THEO_GOITHAU)
            {
                List<KHLCNhaThauQuery> khlcnhathaus = _vdtQddtKhlcnhaThauService.GetKHLCNhaThauByIdDuAn(Guid.Parse(SelectedDuAn._hiddenValue)).ToList();
                if (khlcnhathaus != null && khlcnhathaus.Count > 0)
                {
                    int i = 0;
                    foreach (var item in khlcnhathaus)
                    {
                        ItemQuyetDinh.Insert(i++, new ComboboxItem { DisplayItem = item.SSoQuyetDinh, ValueItem = item.Id.ToString() });
                    }
                }
            }
            if (Entity != null && Entity.iID_QuyetDinh != null)
                SelectedQuyetDinh = ItemQuyetDinh.Where(n => n.ValueItem == Entity.iID_QuyetDinh.ToString()).FirstOrDefault();
        }

        private void LoadDataNguonVonByQDDauTuId()
        {
            if (SelectedDuAn == null) return;
            string duToanId = _vdtDaDuToanService.GetDuToanIdByDuAnId(Guid.Parse(SelectedDuAn.HiddenValue));
            if (string.IsNullOrEmpty(duToanId))
            {
                DataNguonVon = new ObservableCollection<VdtQtQuyetToanNguonVonModel>();
                return;
            }
            List<NguonVonQuyetToanKeHoachQuery> listDuToanNguonVonQuery = _vdtQtDeNghiQuyetToanNguonVonService.GetNguonVonByDuToanId(duToanId).ToList();
            DataNguonVon = _mapper.Map<ObservableCollection<VdtQtQuyetToanNguonVonModel>>(listDuToanNguonVonQuery);
            //CalculateConLaiNguonVon();
            if (Model != null && Model.Id != Guid.Empty)
            {
                List<VdtQtDeNghiQuyetToanNguonvon> listNguonVon = _vdtQtDeNghiQuyetToanNguonVonService.FindByDeNghiQuyetToanId(Model.Id);
                if (listNguonVon != null)
                {
                    foreach (var item in DataNguonVon)
                    {
                        if (item.IIdNguonVonId.HasValue)
                            item.FTienToTrinh = Entity.FGiaTriDeNghiQuyetToan ?? 0;
                               // (listNguonVon.Where(n => n.IIdNguonVonId.HasValue && n.IIdNguonVonId.Value == item.IIdNguonVonId.Value).FirstOrDefault() != null
                               // && listNguonVon.Where(n => n.IIdNguonVonId.HasValue && n.IIdNguonVonId.Value == item.IIdNguonVonId.Value).FirstOrDefault().FTienToTrinh.HasValue) ?
                               //listNguonVon.Where(n => n.IIdNguonVonId.HasValue && n.IIdNguonVonId.Value == item.IIdNguonVonId.Value).FirstOrDefault().FTienToTrinh.Value : 0;
                    }
                }
            }
        }

        private void LoadComboboxDuAn()
        {
            DataDuAn = new ObservableCollection<ComboboxItem>();
            if (SelectedDonViQuanLy == null) return;

            ListDuAn = _vdtDaDuAnService.GetDuAnInQuyetToanDuAnHoanThanh(SelectedDonViQuanLy.ValueItem, Model.Id).ToList();
            DataDuAn = new ObservableCollection<ComboboxItem>();
            foreach (VdtDaDuAn item in ListDuAn)
            {
                DataDuAn.Add(new ComboboxItem { ValueItem = item.SMaDuAn, DisplayItem = string.Format("{0} - {1}", item.SMaDuAn, item.STenDuAn), HiddenValue = item.Id.ToString() });
            }
            if (DataDuAn != null && DataDuAn.Count > 0)
            {
                if (Entity != null && Entity.IIdDuAnId.HasValue)
                    SelectedDuAn = DataDuAn.FirstOrDefault(n => n.HiddenValue == Entity.IIdDuAnId.Value.ToString());
                else
                    SelectedDuAn = DataDuAn.FirstOrDefault();
            }
        }

        private void LoadDuAnInfo()
        {
            if (ListDuAn != null && ListDuAn.Count > 0 && SelectedDuAn != null)
            {
                VdtDaDuAn itemDuAn = ListDuAn.Where(n => n.Id.ToString() == SelectedDuAn.HiddenValue).FirstOrDefault();
                Model.TenDuAn = string.Format("{0} - {1}", itemDuAn.SMaDuAn, itemDuAn.STenDuAn);
                if (itemDuAn.IIdChuDauTuId.HasValue)
                {
                    DmChuDauTu dmChuDauTu = _iDmChuDauTuService.FindById(itemDuAn.IIdChuDauTuId.Value);
                    Model.ChuDauTu = dmChuDauTu != null ? dmChuDauTu.STenDonVi : string.Empty;
                }
                else
                {
                    Model.ChuDauTu = string.Empty;
                }
            }
            else
            {
                Model.TenDuAn = string.Empty;
                Model.ChuDauTu = string.Empty;
            }
            OnPropertyChanged(nameof(Model.TenDuAn));
            OnPropertyChanged(nameof(Model.ChuDauTu));
        }

        public override void OnSave()
        {
            try
            {
                StringBuilder messageBuilder = new StringBuilder();
                if (Model == null)
                    Model = new RequestSettlementDialogModel();
                if (SelectedDonViQuanLy == null || string.IsNullOrEmpty(SelectedDonViQuanLy.ValueItem))
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị");
                }
                if (string.IsNullOrEmpty(Model.SoBaoCao))
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Số báo cáo");
                }
                //if (Model.NgayDuyet == null)
                //{
                //    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Ngày duyệt");
                //}
                //if (string.IsNullOrEmpty(Model.NguoiDuyet))
                //{
                //    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Người duyệt");
                //}
                if (Model.NgayNhan == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Ngày nhận");
                }
                //if (string.IsNullOrEmpty(Model.NguoiNhan))
                //{
                //    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Người nhận");
                //}
                if (SelectedDuAn == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Dự án");
                }
                if (SelectedLoaiQuyetToan == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Loại quyết toán");
                }
                if (SelectedQuyetDinh == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Quyết định");
                }
                if (Model.ThoiGianKhoiCong == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Thời gian khởi công");
                }
                if (Model.ThoiGianHoanThanh == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Thời gian hoàn thành");
                }
                //if (Model.GiaTriQuyetToan == null)
                //{
                //    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Giá trị quyết toán");
                //}
                if (messageBuilder.Length != 0)
                {
                    System.Windows.Forms.MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Entity == null || Entity.Id == Guid.Empty)
                {
                    Entity = new VdtQtDeNghiQuyetToan();
                    _mapper.Map(Model, Entity);
                    if (SelectedDonViQuanLy != null)
                    {
                        Entity.IIDDonViID = Guid.Parse(SelectedDonViQuanLy.HiddenValue);
                        Entity.IIDMaDonVi = SelectedDonViQuanLy.ValueItem;
                    }
                    if (SelectedLoaiQuyetToan != null)
                    {
                        Entity.iID_LoaiQuyetToan = SelectedLoaiQuyetToan.ValueItem;
                    }
                    if (SelectedLoaiQuyetToan != null)
                    {
                        Entity.iID_QuyetDinh = Guid.Parse(SelectedQuyetDinh.ValueItem);
                    }
                    Entity.IIdDuAnId = Guid.Parse(SelectedDuAn.HiddenValue);
                    Entity.SUserCreate = _sessionService.Current.Principal;
                    Entity.DDateCreate = DateTime.Now;
                    Entity.BKhoa = false;
                    _iVdtDeNghiQuyetToanService.Add(Entity);
                }
                else
                {
                    _mapper.Map(Model, Entity);
                    Entity.IIdDuAnId = Guid.Parse(SelectedDuAn.HiddenValue);
                    Entity.SUserUpdate = _sessionService.Current.Principal;
                    Entity.DDateUpdate = DateTime.Now;
                    if (SelectedDonViQuanLy != null)
                    {
                        Entity.IIDDonViID = Guid.Parse(SelectedDonViQuanLy.HiddenValue);
                        Entity.IIDMaDonVi = SelectedDonViQuanLy.ValueItem;
                    }
                    if (SelectedLoaiQuyetToan != null)
                    {
                        Entity.iID_LoaiQuyetToan = SelectedLoaiQuyetToan.ValueItem;
                    }
                    if (SelectedLoaiQuyetToan != null)
                    {
                        Entity.iID_QuyetDinh = Guid.Parse(SelectedQuyetDinh.ValueItem);
                    }
                    _iVdtDeNghiQuyetToanService.Update(Entity);
                }

                //Save nguon von
                _vdtQtDeNghiQuyetToanNguonVonService.DeleteByDeNghiQuyetToanId(Entity.Id);
                List<VdtQtDeNghiQuyetToanNguonvon> listNguonVon = new List<VdtQtDeNghiQuyetToanNguonvon>();
                foreach (VdtQtQuyetToanNguonVonModel item in DataNguonVon.Where(n => n.FTienToTrinh != 0))
                {
                    listNguonVon.Add(new VdtQtDeNghiQuyetToanNguonvon
                    {
                        IIdDeNghiQuyetToanId = Entity.Id,
                        FTienToTrinh = item.FTienToTrinh,
                        IIdNguonVonId = item.IIdNguonVonId.HasValue ? item.IIdNguonVonId.Value : 0
                    });
                }
                if (listNguonVon.Count > 0)
                {
                    _vdtQtDeNghiQuyetToanNguonVonService.AddRange(listNguonVon);
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                //System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                SavedAction?.Invoke(_mapper.Map<VTS.QLNS.CTC.App.Model.DeNghiQuyetToanModel>(Entity));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
