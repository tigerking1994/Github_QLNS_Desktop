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
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.ApproveSettlementDone;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.RequestSettlement;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.RequestSettlement;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.ApproveSettlementDone
{
    public class ApproveSettlementDoneImportViewModel : GridViewModelBase<ChungTuQuyetToanImportModel>
    {
        private readonly INsDonViService _nsDonViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IVdtDaDuAnService _vdtDaDuAnService;
        private readonly IVdtDeNghiQuyetToanService _iVdtDeNghiQuyetToanService;
        private readonly IVdtQtDeNghiQuyetToanNguonVonService _vdtQtDeNghiQuyetToanNguonVonService;
        private readonly IVdtQtDeNghiQuyetToanChiTietService _qtDeNghiQuyetToanChiTietService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly ILog _logger;
        private readonly IVdtQtQuyetToanService _vdtQtQuyetToanService;
        private readonly IPheDuyetQuyetToanService _vdtPheDuyetQuyetToanService;
        private readonly IVdtQtQuyetToanChiTietService _qtQuyetToanChiTietService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private IImportExcelService _importService;

        public override string Name => "Import phê duyệt quyết toán dự án hoàn thành";
        public override string Title => "Import phê duyệt quyết toán dự án hoàn thành";
        public override string Description => "Import thông tin phê duyệt quyết toán";
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.ApproveSettlementDone.ApproveSettlementDoneImport);
        public override PackIconKind IconKind => PackIconKind.Projector;
        public List<VdtDaDuAn> ListDuAn;
        public VdtQtDeNghiQuyetToan Entity;
        public List<DeNghiQuyetToanChiTietModel> ListOrderDb = new List<DeNghiQuyetToanChiTietModel>();
        public ApproveSettlementDoneImportDialogViewModel ApproveSettlementDoneImportDialogViewModel { get; set; }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

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
            set => SetProperty(ref _selectedDonViQuanLy, value);
        }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ResetDataCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand SelectionDoubleClickCommand { get; }

        public ApproveSettlementDoneImportViewModel(INsDonViService nsDonViService,
                                              ISessionService sessionService,
                                              IVdtDaDuAnService vdtDaDuAnService,
                                              IVdtDeNghiQuyetToanService iVdtDeNghiQuyetToanService,
                                              IVdtDaDuToanService vdtDaDuToanService,
                                              IImportExcelService importService,
                                              IVdtQtDeNghiQuyetToanChiTietService qtDeNghiQuyetToanChiTietService,
                                              IVdtQtDeNghiQuyetToanNguonVonService vdtQtDeNghiQuyetToanNguonVonService,
                                              IApproveProjectService approveProjectService,
                                              IVdtQtQuyetToanService vdtQtQuyetToanService,
                                              IPheDuyetQuyetToanService vdtPheDuyetQuyetToanService,
                                              IVdtQtQuyetToanChiTietService qtQuyetToanChiTietService,
                                              INsNguoiDungDonViService nsNguoiDungDonViService,
                                              ILog logger,
                                              ApproveSettlementDoneImportDialogViewModel approveSettlementDoneImportDialogViewModel,
                                              IMapper mapper)
        {
            _sessionService = sessionService;
            _iVdtDeNghiQuyetToanService = iVdtDeNghiQuyetToanService;
            _nsDonViService = nsDonViService;
            _vdtDaDuAnService = vdtDaDuAnService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _importService = importService;
            _approveProjectService = approveProjectService;
            _vdtQtQuyetToanService = vdtQtQuyetToanService;
            _vdtQtDeNghiQuyetToanNguonVonService = vdtQtDeNghiQuyetToanNguonVonService;
            _qtDeNghiQuyetToanChiTietService = qtDeNghiQuyetToanChiTietService;
            _vdtPheDuyetQuyetToanService = vdtPheDuyetQuyetToanService;
            _qtQuyetToanChiTietService = qtQuyetToanChiTietService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _logger = logger;
            _mapper = mapper;

            ApproveSettlementDoneImportDialogViewModel = approveSettlementDoneImportDialogViewModel;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ResetDataCommand = new RelayCommand(obj => OnResetConditon());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            SelectionDoubleClickCommand = new RelayCommand(obj => OnShowPopup((ChungTuQuyetToanImportModel)obj));
        }

        public void OnProcessFile()
        {
            try
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    return;
                }

                _importService.SetLastRowToRead(0);
                ImportResult<ChungTuQuyetToanImportModel> _chungTuResult = _importService.ProcessData<ChungTuQuyetToanImportModel>(FilePath);
                Items = new ObservableCollection<ChungTuQuyetToanImportModel>(_chungTuResult.Data);

                _importService.SetLastRowToRead(0);
                ImportResult<PheDuyetQuyetToanNguonVonImportModel> _nguonVonResult = _importService.ProcessData<PheDuyetQuyetToanNguonVonImportModel>(FilePath);

                if (_nguonVonResult.Data != null && _nguonVonResult.Data.Count > 0)
                {
                    foreach (ChungTuQuyetToanImportModel item in Items)
                    {
                        item.ListNguonVon = _nguonVonResult.Data.Where(n => n.MaDuAn.Trim() == item.MaDuAn.Trim()).ToList();
                        if (_nguonVonResult.Data.Where(n => !n.ImportStatus && n.MaDuAn.Trim() == item.MaDuAn.Trim()).Count() > 0)
                        {
                            item.ImportStatus = false;
                        }
                    }
                }

                string cell = _importService.FindCellByValue(FilePath, "Tổng hợp chi phí đầu tư đề nghị quyết toán:", 2);
                int lastRow = 0;
                if (!string.IsNullOrEmpty(cell))
                {
                    lastRow = int.Parse(cell.Replace("B", string.Empty)) - 1;
                }
                if (lastRow != 0)
                {
                    _importService.SetLastRowToRead(lastRow);
                }

                ImportResult<DeNghiQuyetToanChiPhiKhacImportModel> _chiPhiKhac = _importService.ProcessData<DeNghiQuyetToanChiPhiKhacImportModel>(FilePath);
                if (_chiPhiKhac.Data != null && _chiPhiKhac.Data.Count > 0)
                {
                    foreach (ChungTuQuyetToanImportModel item in Items)
                    {
                        DeNghiQuyetToanChiPhiKhacImportModel chiPhiThietHai = _chiPhiKhac.Data.Where(n => n.MaDuAn.Trim() == item.MaDuAn.Trim()
                        && n.NoiDungChiPhi.Trim() == "Chi phí thiệt hại do các nguyên nhân bất khả kháng").FirstOrDefault();
                        if (chiPhiThietHai != null)
                        {
                            double value = 0;
                            bool check = double.TryParse(chiPhiThietHai.GiaTri, out value);
                            item.ChiPhiThietHai = value;
                        }

                        DeNghiQuyetToanChiPhiKhacImportModel chiPhiKhongTaoNenTaiSan = _chiPhiKhac.Data.Where(n => n.MaDuAn.Trim() == item.MaDuAn.Trim()
                        && n.NoiDungChiPhi.Trim() == "Chi phí không tạo nên tài sản").FirstOrDefault();
                        if (chiPhiKhongTaoNenTaiSan != null)
                        {
                            double value = 0;
                            bool check = double.TryParse(chiPhiKhongTaoNenTaiSan.GiaTri, out value);
                            item.ChiPhiKhongTaoNenTaiSan = value;
                        }

                        if (_chiPhiKhac.Data.Where(n => !n.ImportStatus && n.MaDuAn.Trim() == item.MaDuAn.Trim()).Count() > 0)
                        {
                            item.ImportStatus = false;
                        }
                    }
                }

                _importService.SetLastRowToRead(0);
                ImportResult<PheDuyetQuyetToanChiPhiImportModel> _chiPhiResullt = _importService.ProcessDataByRow<PheDuyetQuyetToanChiPhiImportModel>(FilePath, (lastRow + 4));
                if (_chiPhiResullt.Data != null && _chiPhiResullt.Data.Count > 0)
                {
                    foreach (ChungTuQuyetToanImportModel item in Items)
                    {
                        item.ListChiPhi = _chiPhiResullt.Data.Where(n => n.MaDuAn.Trim() == item.MaDuAn.Trim()).ToList();
                        if (_chiPhiResullt.Data.Where(n => !n.ImportStatus && n.MaDuAn.Trim() == item.MaDuAn.Trim()).Count() > 0)
                        {
                            item.ImportStatus = false;
                        }
                    }
                }

                _importService.SetLastRowToRead(0);
                ImportResult<DeNghiQuyetToanTaiSanImportModel> _taiSanResult = _importService.ProcessData<DeNghiQuyetToanTaiSanImportModel>(FilePath);
                if (_taiSanResult.Data != null && _taiSanResult.Data.Count > 0)
                {
                    foreach (ChungTuQuyetToanImportModel item in Items)
                    {
                        DeNghiQuyetToanTaiSanImportModel daiHanCDT = _taiSanResult.Data.Where(n => n.MaDuAn.Trim() == item.MaDuAn.Trim()
                        && n.Nhom.Trim() == "Tài sản dài hạn (cố định)").FirstOrDefault();
                        if (daiHanCDT != null)
                        {
                            item.DaiHanCDT = daiHanCDT.ThuocCDTQuanLyValue;
                            item.DaiHanDonViKhac = daiHanCDT.DonViKhacQuanLyValue;
                        }

                        DeNghiQuyetToanTaiSanImportModel nganHan = _taiSanResult.Data.Where(n => n.MaDuAn.Trim() == item.MaDuAn.Trim()
                        && n.Nhom.Trim() == "Tài sản ngắn hạn").FirstOrDefault();
                        if (nganHan != null)
                        {
                            item.NganHanCDT = nganHan.ThuocCDTQuanLyValue;
                            item.NganHanDonViKhac = nganHan.DonViKhacQuanLyValue;
                        }

                        if (_taiSanResult.Data.Where(n => !n.ImportStatus && n.MaDuAn.Trim() == item.MaDuAn.Trim()).Count() > 0)
                        {
                            item.ImportStatus = false;
                        }
                    }
                }
                OnPropertyChanged(nameof(Items));
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

                if (SelectedDonViQuanLy == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị quản lý");
                }

                if (messageBuilder.Length != 0)
                {
                    System.Windows.Forms.MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                OnProcessFile();
                if (Items == null || Items.Count() == 0)
                {
                    return;
                }
                foreach (var item in Items)
                {
                    VdtDaDuAn duAn = _vdtDaDuAnService.FindByMaDuAn(item.MaDuAn);
                    if (duAn.IIdMaDonViQuanLy != SelectedDonViQuanLy.ValueItem)
                    {
                        messageBuilder.AppendFormat(Resources.MsgDuAnThuocDonVi, item.TenDuAn);
                    }
                    else
                    {
                        item.IdDuAn = duAn.Id;
                    }
                }
                if (messageBuilder.Length != 0)
                {
                    System.Windows.Forms.MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<VdtQtQuyetToan> entityChungTu = new List<VdtQtQuyetToan>();
                List<VdtQtQuyetToanNguonvon> entityNguonVon = new List<VdtQtQuyetToanNguonvon>();
                List<VdtQtQuyetToanChiTiet> entityChiTiet = new List<VdtQtQuyetToanChiTiet>();

                foreach (ChungTuQuyetToanImportModel item in Items.Where(n => n.ImportStatus).ToList())
                {
                    entityChungTu.Add(new VdtQtQuyetToan
                    {
                        SSoQuyetDinh = item.SoBaoCao,
                        DNgayQuyetDinh = item.NgayQuyetDinhValue,
                        SUserCreate = _sessionService.Current.Principal,
                        DDateCreate = DateTime.Now,
                        FChiPhiThietHai = item.ChiPhiThietHai,
                        FChiPhiKhongTaoNenTaiSan = item.ChiPhiKhongTaoNenTaiSan,
                        FTaiSanDaiHanThuocCDTQuanLy = item.DaiHanCDT,
                        FTaiSanDaiHanDonViKhacQuanLy = item.DaiHanDonViKhac,
                        FTaiSanNganHanThuocCDTQuanLy = item.NganHanCDT,
                        FTaiSanNganHanDonViKhacQuanLy = item.NganHanDonViKhac,
                        IIdDuAnId = item.IdDuAn,
                        IIDMaDonVi = SelectedDonViQuanLy.ValueItem,
                        IIDDonViID = Guid.Parse(SelectedDonViQuanLy.HiddenValue)
                    });


                }

                _vdtQtQuyetToanService.AddRange(entityChungTu);

                foreach (ChungTuQuyetToanImportModel item in Items.Where(n => n.ImportStatus).ToList())
                {
                    foreach (PheDuyetQuyetToanNguonVonImportModel nguonVon in item.ListNguonVon)
                    {
                        Guid quyetToanId = entityChungTu.Where(n => n.SSoQuyetDinh == item.SoBaoCao).FirstOrDefault().Id;
                        entityNguonVon.Add(new VdtQtQuyetToanNguonvon
                        {
                            IIdQuyetToanId = quyetToanId,
                            FTienPheDuyet = nguonVon.GiaTriQuyetToanValue,
                            IIdNguonVonId = int.Parse(nguonVon.MaNguonVon)
                        });
                    }
                }
                _vdtPheDuyetQuyetToanService.AddRangeQuyetToanNguonVon(entityNguonVon);

                foreach (ChungTuQuyetToanImportModel item in Items.Where(n => n.ImportStatus).ToList())
                {
                    GetListOrderDb(item.IdDuAn);
                    foreach (PheDuyetQuyetToanChiPhiImportModel chiphi in item.ListChiPhi)
                    {
                        Guid quyetToanId = entityChungTu.Where(n => n.SSoQuyetDinh == item.SoBaoCao).FirstOrDefault().Id;
                        VdtQtQuyetToanChiTiet chiPhiEntity = new VdtQtQuyetToanChiTiet();
                        chiPhiEntity.IIdQuyetToanId = quyetToanId;
                        chiPhiEntity.FGiaTriThamTra = chiphi.GiaTriThamTraValue;
                        chiPhiEntity.FGiaTriQuyetToan = chiphi.GiaTriQuyetToanValue;
                        chiPhiEntity.DDateCreate = DateTime.Now;
                        chiPhiEntity.SUserCreate = _sessionService.Current.Principal;
                        if (chiphi.Ma.StartsWith("CP"))
                        {
                            VdtDmDuAnChiPhi dmChiPhi = _approveProjectService.FindByMaDuAnChiPhi(chiphi.Ma.Replace("CP-", string.Empty));
                            if (dmChiPhi != null)
                            {
                                chiPhiEntity.IIdChiPhiId = dmChiPhi.Id;
                                chiPhiEntity.IIdHangMucId = Guid.Empty;
                                if (chiPhiEntity.IIdChiPhiId.HasValue)
                                    chiPhiEntity.SMaOrder = GetMaOrderForChiPhiItem(chiPhiEntity.IIdChiPhiId.Value);
                            }
                        }
                        else if (chiphi.Ma.StartsWith("HM"))
                        {
                            VdtDaDuToanDmHangMuc hangMuc = _vdtDaDuToanService.FindDaDuToanHangMucByMa(chiphi.Ma.Replace("HM-", string.Empty));
                            if (hangMuc != null)
                            {
                                chiPhiEntity.IIdHangMucId = hangMuc.Id;
                                chiPhiEntity.IIdChiPhiId = Guid.Empty;
                            }
                        }
                        entityChiTiet.Add(chiPhiEntity);
                    }
                }
                _qtQuyetToanChiTietService.AddRange(entityChiTiet);

                foreach (var entity in entityChungTu)
                {
                    _qtQuyetToanChiTietService.UpdateTotal(entity.Id.ToString());
                }

                SavedAction?.Invoke(Entity);
                System.Windows.MessageBox.Show(Resources.MsgSaveDone, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetMaOrderForChiPhiItem(Guid chiPhiId)
        {
            DeNghiQuyetToanChiTietModel item = ListOrderDb.Where(n => n.ChiPhiId == chiPhiId).FirstOrDefault();
            return item != null ? item.MaOrderDb : string.Empty;
        }

        private void GetListOrderDb(Guid duAnId)
        {
            List<VdtDaDuToanChiPhiDataQuery> listDuToanChiPhi = _qtDeNghiQuyetToanChiTietService.FindListDuToanChiPhiByDuAn(duAnId);
            ListOrderDb = _mapper.Map<List<Model.DeNghiQuyetToanChiTietModel>>(listDuToanChiPhi);
            ListOrderDb.Select(n => { n.IsChiPhi = true; return n; }).ToList();
            CreateMaOrderItem();
        }

        public void CreateMaOrderItem()
        {
            if (ListOrderDb == null || ListOrderDb.Count == 0)
                return;
            List<DeNghiQuyetToanChiTietModel> roots = ListOrderDb.Where(n => n.IsChiPhi && n.IdChiPhiDuAnParent == Guid.Empty && n.PhanCap == 1).ToList();
            if (roots != null && roots.Count() > 0)
            {
                int count = 1;
                foreach (var item in roots)
                {
                    item.MaOrderDb = count.ToString();
                    CreateMaOrderItemChild(item);
                    count++;
                }
            }
        }

        public void CreateMaOrderItemChild(DeNghiQuyetToanChiTietModel parent)
        {
            List<DeNghiQuyetToanChiTietModel> listChild = ListOrderDb.Where(n => n.IdChiPhiDuAnParent == parent.ChiPhiId).ToList();
            if (listChild == null || listChild.Count == 0)
            {
                return;
            }
            for (int i = 0; i < listChild.Count; i++)
            {
                listChild[i].MaOrderDb = parent.MaOrderDb + "_" + (i + 1).ToString();
                CreateMaOrderItemChild(listChild[i]);
            }
        }

        protected void OnShowPopup(ChungTuQuyetToanImportModel obj)
        {
            try
            {
                if (obj == null)
                {
                    return;
                }
                ApproveSettlementDoneImportDialogViewModel.Model = obj;
                ApproveSettlementDoneImportDialogViewModel.Init();
                var view = new ApproveSettlementDoneImportDialog
                {
                    DataContext = ApproveSettlementDoneImportDialogViewModel
                };
                DialogHost.Show(view, "ApproveSettlementImportlWindow");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void Init()
        {
            try
            {
                OnResetConditon();
                LoadCombobxDonVi();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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
            if (DataDonViQuanLy != null && DataDonViQuanLy.Count > 0)
            {
                SelectedDonViQuanLy = DataDonViQuanLy.FirstOrDefault();
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        private void OnResetConditon()
        {
            Entity = new VdtQtDeNghiQuyetToan();
            FilePath = string.Empty;
            FileName = string.Empty;
            Items = new ObservableCollection<ChungTuQuyetToanImportModel>();
            if (DataDonViQuanLy != null && DataDonViQuanLy.Count > 0)
            {
                SelectedDonViQuanLy = DataDonViQuanLy.FirstOrDefault();
            }
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(SelectedDonViQuanLy));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(FileName));
        }

        private void OnUploadFile()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = string.Format("Chọn file excel");
                openFileDialog.RestoreDirectory = true;
                openFileDialog.DefaultExt = ".xlsx";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                FilePath = openFileDialog.FileName;
                FileName = openFileDialog.SafeFileName;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
