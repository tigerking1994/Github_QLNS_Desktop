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

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.ApproveSettlementDone
{
    public class ApproveSettlementDoneDialogViewModel : DialogViewModelBase<ApproveSettlementDoneDialogModel>
    {
        private readonly INsDonViService _nsDonViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IVdtDaDuAnService _vdtDaDuAnService;
        private readonly IVdtQtQuyetToanService _vdtQtQuyetToanService;
        private readonly ILog _logger;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private readonly IVdtQtDeNghiQuyetToanNguonVonService _vdtQtDeNghiQuyetToanNguonVonService;
        private readonly IVdtDeNghiQuyetToanService _vdtDeNghiQuyetToanService;
        private readonly IPheDuyetQuyetToanService _vdtPheDuyetQuyetToanService;
        private readonly IVdtQtQuyetToanChiTietService _qtQuyetToanChiTietService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        public override string Name => "Quyết toán dự án hoàn thành";
        public override string Title => "Quyết toán dự án hoàn thành";
        public override string Description => string.Format("{0} thông tin phê duyệt quyết toán", BIsAdd ? "Thêm mới" : "Cập nhật");
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.ApproveSettlementDone.ApproveSettlementDoneDialog);
        public bool BIsAdd => Model.Id == Guid.Empty;
        public override PackIconKind IconKind => PackIconKind.Projector;
        public List<VdtDaDuAnQuery> ListDuAn;
        public VdtQtQuyetToan Entity;

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
                if (SetProperty(ref _selectedDonViQuanLy, value) && _selectedDonViQuanLy != null)
                {
                    LoadComboboxDuAn(_selectedDonViQuanLy.ValueItem);
                }
            }
        }

        private ObservableCollection<ComboboxItem> _listDeNghiQuyetToan;
        public ObservableCollection<ComboboxItem> ListDeNghiQuyetToan
        {
            get => _listDeNghiQuyetToan;
            set => SetProperty(ref _listDeNghiQuyetToan, value);
        }

        private ComboboxItem _selectedDeNghi;
        public ComboboxItem SelectedDeNghi
        {
            get => _selectedDeNghi;
            set
            {
                if (SetProperty(ref _selectedDeNghi, value) && _selectedDeNghi != null)
                {
                    LoadInfo(_selectedDuAn.HiddenValue);
                    if (_selectedDuAn != null && !string.IsNullOrEmpty(_selectedDuAn.HiddenValue))
                    {
                        LoadDataNguonVon(Guid.Parse(_selectedDuAn.HiddenValue));
                    }
                }
            }
        }

        private string _diaDiem;
        public string DiaDiem
        {
            get => _diaDiem;
            set => SetProperty(ref _diaDiem, value);
        }

        private string _thoiGianThucHien;
        public string ThoiGianThucHien
        {
            get => _thoiGianThucHien;
            set => SetProperty(ref _thoiGianThucHien, value);
        }

        private double _tongMucDauTu;
        public double TongMucDauTu
        {
            get => _tongMucDauTu;
            set => SetProperty(ref _tongMucDauTu, value);
        }

        private double _keHoachUng;
        public double KeHoachUng
        {
            get => _keHoachUng;
            set => SetProperty(ref _keHoachUng, value);
        }

        private double _vonUngDaCap;
        public double VonUngDaCap
        {
            get => _vonUngDaCap;
            set => SetProperty(ref _vonUngDaCap, value);
        }

        private double _vonUngDaThuHoi;
        public double VonUngDaThuHoi
        {
            get => _vonUngDaThuHoi;
            set => SetProperty(ref _vonUngDaThuHoi, value);
        }

        private double _giaTriConPhaiThuHoi;
        public double GiaTriConPhaiThuHoi
        {
            get => _giaTriConPhaiThuHoi;
            set => SetProperty(ref _giaTriConPhaiThuHoi, value);
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
                    LoadDeNghi(_selectedDuAn.HiddenValue);
                }
            }
        }

        private DateTime? _ngayQuyetDinh;
        public DateTime? NgayQuyetDinh
        {
            get => _ngayQuyetDinh;
            set
            {
                if (SetProperty(ref _ngayQuyetDinh, value) && _selectedDonViQuanLy != null)
                {
                    LoadComboboxDuAn(_selectedDonViQuanLy.ValueItem);
                }
            }
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.VdtQtQuyetToanPheDuyetNguonVonModel> _dataNguonVon;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.VdtQtQuyetToanPheDuyetNguonVonModel> DataNguonVon
        {
            get => _dataNguonVon;
            set => SetProperty(ref _dataNguonVon, value);
        }

        public ApproveSettlementDoneDialogViewModel(INsDonViService nsDonViService,
                                              ISessionService sessionService,
                                              IVdtDaDuAnService vdtDaDuAnService,
                                              IVdtQtQuyetToanService vdtQtQuyetToanService,
                                              IVdtDaDuToanService vdtDaDuToanService,
                                              IVdtQtDeNghiQuyetToanNguonVonService vdtQtDeNghiQuyetToanNguonVonService,
                                              IVdtDeNghiQuyetToanService vdtDeNghiQuyetToanService,
                                              IPheDuyetQuyetToanService vdtPheDuyetQuyetToanService,
                                              IVdtQtQuyetToanChiTietService qtQuyetToanChiTietService,
                                              INsNguoiDungDonViService nsNguoiDungDonViService,
                                              ILog logger,
                                              IMapper mapper)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _vdtDaDuAnService = vdtDaDuAnService;
            _vdtQtQuyetToanService = vdtQtQuyetToanService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _vdtDeNghiQuyetToanService = vdtDeNghiQuyetToanService;
            _vdtPheDuyetQuyetToanService = vdtPheDuyetQuyetToanService;
            _qtQuyetToanChiTietService = qtQuyetToanChiTietService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _vdtQtDeNghiQuyetToanNguonVonService = vdtQtDeNghiQuyetToanNguonVonService;
            _mapper = mapper;
            _logger = logger;
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

        private void LoadComboboxDonVi()
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
                SelectedDonViQuanLy = DataDonViQuanLy.FirstOrDefault();
            }
        }

        private void LoadComboboxDuAn(string idDonVi)
        {
            if (string.IsNullOrEmpty(idDonVi) || NgayQuyetDinh == null)
            {
                DataDuAn = new ObservableCollection<ComboboxItem>();
                return;
            }
            ListDuAn = _vdtDaDuAnService.FindByIdDonViAndNgayQuyetDinh(idDonVi, NgayQuyetDinh.Value).ToList();
            DataDuAn = new ObservableCollection<ComboboxItem>();
            foreach (VdtDaDuAnQuery item in ListDuAn)
            {
                DataDuAn.Add(new ComboboxItem { ValueItem = item.SMaDuAn, DisplayItem = string.Format("{0} - {1}", item.SMaDuAn, item.STenDuAn), HiddenValue = item.IID_DuAnID.ToString() });
            }
            if (DataDuAn != null && DataDuAn.Count > 0)
            {
                SelectedDuAn = DataDuAn.FirstOrDefault();
                if (ListDuAn.FirstOrDefault().IID_ChuDauTuID.HasValue)
                {
                    DonVi donvi = _nsDonViService.Find(ListDuAn.FirstOrDefault().IID_ChuDauTuID.Value);
                }
                if (Entity != null)
                    SelectedDuAn = DataDuAn.FirstOrDefault(n => n.HiddenValue == Entity.IIdDuAnId.ToString());
                OnPropertyChanged(nameof(SelectedDuAn));
            }
            else
            {
                LoadInfo(string.Empty);
            }
        }

        private void LoadDeNghi(string idDuan)
        {
            if (string.IsNullOrEmpty(idDuan) || NgayQuyetDinh == null)
            {
                ListDeNghiQuyetToan = new ObservableCollection<ComboboxItem>();
                return;
            }
            ListDeNghiQuyetToan = new ObservableCollection<ComboboxItem>();
            List<VdtQtDeNghiQuyetToan> lstdenghi = _vdtDeNghiQuyetToanService.FindLstDeNghiQTByDuAnId(Guid.Parse(idDuan)).ToList();
            if(lstdenghi != null && lstdenghi.Count() > 0)
            {
                int i = 0;
                foreach (var item in lstdenghi)
                {
                    ListDeNghiQuyetToan.Insert(i++, new ComboboxItem { DisplayItem = item.SSoBaoCao, ValueItem = item.Id.ToString() });
                }
            }
            if (Entity != null && Entity.IIdDenghiQuyetToanId != null)
                SelectedDeNghi = ListDeNghiQuyetToan.Where(n => n.ValueItem == Entity.IIdDenghiQuyetToanId.ToString()).FirstOrDefault();
        }

        private void LoadDataNguonVon(Guid duanId)
        {
            string duToanId = _vdtDaDuToanService.GetDuToanIdByDuAnId(duanId);
            if (string.IsNullOrEmpty(duToanId))
            {
                DataNguonVon = new ObservableCollection<VdtQtQuyetToanPheDuyetNguonVonModel>();
                return;
            }
            VdtQtDeNghiQuyetToan deNghiQuyetToan = _vdtDeNghiQuyetToanService.FindByDuAnId(duanId);
            string deNghiQuyetToanId = string.Empty;
            if (deNghiQuyetToan != null)
            {
                deNghiQuyetToanId = deNghiQuyetToan.Id.ToString();
            }


            List<NguonVonQuyetToanQuery> listDuToanNguonVonQuery = _vdtQtQuyetToanService.GetNguonVonByDuToanIdDeNghiQuyetToanId(duToanId, deNghiQuyetToanId).ToList();
            DataNguonVon = _mapper.Map<ObservableCollection<VdtQtQuyetToanPheDuyetNguonVonModel>>(listDuToanNguonVonQuery);

            if (Model != null && Model.Id != Guid.Empty)
            {
                List<VdtQtQuyetToanNguonvon> listNguonVon = _vdtPheDuyetQuyetToanService.FindByQuyetToanId(Model.Id);
                if (listNguonVon != null)
                {
                    foreach (var item in DataNguonVon)
                    {
                        if (item.IIdNguonVonId.HasValue)
                            item.FTienToTrinh = (listNguonVon.Where(n => n.IIdNguonVonId == item.IIdNguonVonId.Value).FirstOrDefault() != null
                            && listNguonVon.Where(n => n.IIdNguonVonId == item.IIdNguonVonId.Value).FirstOrDefault().FTienPheDuyet.HasValue) ?
                               listNguonVon.Where(n => n.IIdNguonVonId == item.IIdNguonVonId.Value).FirstOrDefault().FTienPheDuyet.Value : 0;
                    }
                }
            }
        }

        private string LoadDonVi(string idDuAn)
        {
            if (string.IsNullOrEmpty(idDuAn))
                return string.Empty;
            VdtDaDuAn duAn = _vdtDaDuAnService.Find(Guid.Parse(idDuAn));
            if (duAn != null)
            {
                LoadComboboxDuAn(duAn.IIdMaDonViQuanLy);
                return duAn.IIdMaDonViQuanLy;
            }
            else
            {
                return string.Empty;
            }
        }

        private void LoadInfo(string idDuAn)
        {
            VdtDaDuAnQuery itemDuAn = ListDuAn.Where(n => n.IID_DuAnID.ToString() == idDuAn).FirstOrDefault();
            if (itemDuAn == null)
            {
                DiaDiem = string.Empty;
                ThoiGianThucHien = string.Empty;
                TongMucDauTu = 0;
                KeHoachUng = 0;
                VonUngDaCap = 0;
                VonUngDaThuHoi = 0;
                GiaTriConPhaiThuHoi = 0;
            }
            else
            {
                DiaDiem = itemDuAn.SDiaDiem;
                ThoiGianThucHien = string.Format("{0} - {1}", itemDuAn.SKhoiCong, itemDuAn.SKetThuc);
                TongMucDauTu = itemDuAn.TongMucDauTu.HasValue ? itemDuAn.TongMucDauTu.Value : 0;
                KeHoachUng = itemDuAn.KeHoachUng.HasValue ? itemDuAn.KeHoachUng.Value : 0;
                VonUngDaCap = itemDuAn.VonUngDaCap.HasValue ? itemDuAn.VonUngDaCap.Value : 0;
                VonUngDaThuHoi = itemDuAn.VonUngThuHoi.HasValue ? itemDuAn.VonUngThuHoi.Value : 0;
                GiaTriConPhaiThuHoi = VonUngDaCap - VonUngDaThuHoi;
            }
            OnPropertyChanged(nameof(DiaDiem));
            OnPropertyChanged(nameof(ThoiGianThucHien));
            OnPropertyChanged(nameof(TongMucDauTu));
            OnPropertyChanged(nameof(KeHoachUng));
            OnPropertyChanged(nameof(VonUngDaCap));
            OnPropertyChanged(nameof(VonUngDaThuHoi));
            OnPropertyChanged(nameof(GiaTriConPhaiThuHoi));
        }

        private void ResetCondition()
        {
            DiaDiem = string.Empty;
            ThoiGianThucHien = string.Empty;
            TongMucDauTu = 0;
            KeHoachUng = 0;
            VonUngDaCap = 0;
            VonUngDaThuHoi = 0;
            GiaTriConPhaiThuHoi = 0;
            DataNguonVon = new ObservableCollection<VdtQtQuyetToanPheDuyetNguonVonModel>();

            OnPropertyChanged(nameof(DataNguonVon));
            OnPropertyChanged(nameof(DiaDiem));
            OnPropertyChanged(nameof(ThoiGianThucHien));
            OnPropertyChanged(nameof(TongMucDauTu));
            OnPropertyChanged(nameof(KeHoachUng));
            OnPropertyChanged(nameof(VonUngDaCap));
            OnPropertyChanged(nameof(VonUngDaThuHoi));
            OnPropertyChanged(nameof(GiaTriConPhaiThuHoi));
        }

        public override void Init()
        {
            try
            {
                ResetCondition();
                LoadComboboxDonVi();
                if (Model == null || Model.Id == Guid.Empty)
                {
                    Model = new ApproveSettlementDoneDialogModel();
                    NgayQuyetDinh = DateTime.Now;
                }
                else
                {
                    Entity = _vdtQtQuyetToanService.Find(Model.Id);
                    if (Entity == null)
                    {
                        Entity = new VdtQtQuyetToan();
                        return;
                    }
                    if (Entity != null)
                    {
                        NgayQuyetDinh = Entity.DNgayQuyetDinh;
                        Model.Id = Entity.Id;
                        Model.SoQuyetDinh = Entity.SSoQuyetDinh;
                        Model.CoQuanPheDuyet = Entity.SCoQuanPheDuyet;
                        Model.NguoiKy = Entity.SNguoiKy;
                        Model.ChiPhiThietHai = Entity.FChiPhiThietHai;
                        Model.ChiPhiKhongTaoTaiSan = Entity.FChiPhiKhongTaoNenTaiSan;
                        Model.DaiHanThuocQuanLy = Entity.FTaiSanDaiHanThuocCDTQuanLy;
                        Model.DaiHanDonViKhacQuanLy = Entity.FTaiSanDaiHanDonViKhacQuanLy;
                        Model.NganHanDonViKhacQuanLy = Entity.FTaiSanNganHanDonViKhacQuanLy;
                        Model.NganHanThuocQuanLy = Entity.FTaiSanNganHanThuocCDTQuanLy;
                    }
                    string idDonVi = LoadDonVi(Entity.IIdDuAnId.ToString());
                    if (DataDonViQuanLy != null && DataDonViQuanLy.Count > 0 && !string.IsNullOrEmpty(idDonVi))
                    {
                        SelectedDonViQuanLy = DataDonViQuanLy.Where(n => n.ValueItem == idDonVi).FirstOrDefault();
                    }
                    if (DataDuAn != null && DataDuAn.Count > 0 && !string.IsNullOrEmpty(Entity.IIdDuAnId.ToString()))
                    {
                        SelectedDuAn = DataDuAn.Where(n => n.HiddenValue == Entity.IIdDuAnId.ToString()).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave()
        {
            try
            {
                StringBuilder messageBuilder = new StringBuilder();
                if (Model == null)
                    Model = new ApproveSettlementDoneDialogModel();
                if (SelectedDonViQuanLy == null || string.IsNullOrEmpty(SelectedDonViQuanLy.ValueItem))
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị");
                }
                if (string.IsNullOrEmpty(Model.SoQuyetDinh))
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Số quyết định");
                }
                if (NgayQuyetDinh == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Ngày phê duyệt");
                }
                if (SelectedDuAn == null || string.IsNullOrEmpty(SelectedDuAn.HiddenValue))
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Nội dung");
                }
                if (!string.IsNullOrEmpty(Model.SoQuyetDinh) && _vdtQtQuyetToanService.IsExistSoQuyetDinh(Model.SoQuyetDinh, Model.Id))
                {
                    messageBuilder.AppendFormat(Resources.MsgTrungSoQuyetDinhs);
                }

                if (messageBuilder.Length != 0)
                {
                    System.Windows.Forms.MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Entity == null || Entity.Id == Guid.Empty)
                {
                    Entity = new VdtQtQuyetToan();
                    _mapper.Map(Model, Entity);
                    if (SelectedDonViQuanLy != null)
                    {
                        Entity.IIDDonViID = Guid.Parse(SelectedDonViQuanLy.HiddenValue);
                        Entity.IIDMaDonVi = SelectedDonViQuanLy.ValueItem;
                    }
                    if (SelectedDeNghi != null)
                    {
                        Entity.IIdDenghiQuyetToanId = Guid.Parse(SelectedDeNghi.ValueItem);
                    }
                    Entity.IIdDuAnId = Guid.Parse(SelectedDuAn.HiddenValue);
                    Entity.DNgayQuyetDinh = NgayQuyetDinh;
                    Entity.SUserCreate = _sessionService.Current.Principal;
                    Entity.DDateCreate = DateTime.Now;
                    Entity.BKhoa = false;
                    _vdtQtQuyetToanService.Add(Entity);

                    VdtDaDuAn duAn = _vdtDaDuAnService.FindById(Entity.IIdDuAnId);
                    duAn.BIsKetThuc = true;
                    _vdtDaDuAnService.Update(duAn);
                }
                else
                {
                    _mapper.Map(Model, Entity);
                    if (SelectedDonViQuanLy != null)
                    {
                        Entity.IIDDonViID = Guid.Parse(SelectedDonViQuanLy.HiddenValue);
                        Entity.IIDMaDonVi = SelectedDonViQuanLy.ValueItem;
                    }
                    if (SelectedDeNghi != null)
                    {
                        Entity.IIdDenghiQuyetToanId = Guid.Parse(SelectedDeNghi.ValueItem);
                    }
                    Entity.IIdDuAnId = Guid.Parse(SelectedDuAn.HiddenValue);
                    Entity.DNgayQuyetDinh = NgayQuyetDinh;
                    Entity.SUserUpdate = _sessionService.Current.Principal;
                    Entity.DDateUpdate = DateTime.Now;
                    _vdtQtQuyetToanService.Update(Entity);

                    VdtDaDuAn duAn = _vdtDaDuAnService.FindById(Entity.IIdDuAnId);
                    duAn.BIsKetThuc = true;
                    _vdtDaDuAnService.Update(duAn);
                }

                //Save nguon von
                _vdtPheDuyetQuyetToanService.DeleteQuyetToanNguonVonByQuyetToanId(Entity.Id);
                List<VdtQtQuyetToanNguonvon> listNguonVon = new List<VdtQtQuyetToanNguonvon>();
                foreach (VdtQtQuyetToanPheDuyetNguonVonModel item in DataNguonVon.Where(n => n.FTienToTrinh != 0))
                {
                    listNguonVon.Add(new VdtQtQuyetToanNguonvon
                    {
                        IIdQuyetToanId = Entity.Id,
                        FTienPheDuyet = item.FTienToTrinh,
                        IIdNguonVonId = item.IIdNguonVonId.HasValue ? item.IIdNguonVonId.Value : 0
                    });
                }
                if (listNguonVon.Count > 0)
                {
                    _vdtPheDuyetQuyetToanService.AddRangeQuyetToanNguonVon(listNguonVon);
                }
                _qtQuyetToanChiTietService.UpdateTotal(Entity.Id.ToString());
                DialogHost.CloseDialogCommand.Execute(null, null);
                PheDuyetQuyetToanModel modeldialog = _mapper.Map<PheDuyetQuyetToanModel>(Entity);
                modeldialog.IdDeNghiQuyetToan = Entity.IIdDenghiQuyetToanId;
                modeldialog.IsAdded = BIsAdd;
                SavedAction?.Invoke(modeldialog);
                
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
