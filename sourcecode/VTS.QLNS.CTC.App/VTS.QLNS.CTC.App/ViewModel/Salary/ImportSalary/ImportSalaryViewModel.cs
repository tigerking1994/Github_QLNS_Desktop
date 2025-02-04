using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.ImportSalary
{
    public class ImportSalaryViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly IImportExcelService _importService;
        private readonly ITlDsCapNhapBangLuongService _tlDsCapNhapBangLuongService;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private readonly ITlDmDonViService _dmDonViService;
        private readonly ITlDmCanBoService _dmCanBoService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(View.Salary.ImportSalary.ImportSalary);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private ObservableCollection<SalaryMonthImportModel> _salaryMonthImportModels;
        public ObservableCollection<SalaryMonthImportModel> SalaryMonthImportModels
        {
            get => _salaryMonthImportModels;
            set => SetProperty(ref _salaryMonthImportModels, value);
        }

        private SalaryMonthImportModel _seletedBangLuong;
        public SalaryMonthImportModel SeletedBangLuong
        {
            get => _seletedBangLuong;
            set => SetProperty(ref _seletedBangLuong, value);
        }

        private ObservableCollection<SalaryMonthDetailImportModel> _salaryMonthDetailImportModels;
        public ObservableCollection<SalaryMonthDetailImportModel> SalaryMonthDetailImportModels
        {
            get => _salaryMonthDetailImportModels;
            set => SetProperty(ref _salaryMonthDetailImportModels, value);
        }

        private SalaryMonthDetailImportModel _seletedItem;
        public SalaryMonthDetailImportModel SelectedItem
        {
            get => _seletedItem;
            set => SetProperty(ref _seletedItem, value);
        }

        private ObservableCollection<SalaryMonthDetailImportModel> _salaryMonthDetailImportViewModels;
        public ObservableCollection<SalaryMonthDetailImportModel> SalaryMonthDetailImportViewModels
        {
            get => _salaryMonthDetailImportViewModels;
            set => SetProperty(ref _salaryMonthDetailImportViewModels, value);
        }

        private ObservableCollection<TlDmCanBoImportModel> _dmCanBoImportModels;
        public ObservableCollection<TlDmCanBoImportModel> DmCanBoImportModels
        {
            get => _dmCanBoImportModels;
            set => SetProperty(ref _dmCanBoImportModels, value);
        }

        private ObservableCollection<TlDmDonViImportModel> _dmDonViImportModel;
        public ObservableCollection<TlDmDonViImportModel> DmDonViImportModel
        {
            get => _dmDonViImportModel;
            set => SetProperty(ref _dmDonViImportModel, value);
        }

        private ObservableCollection<TlDmCanBoPhuCapImportModel> _dmCanBoPhuCapImportModel;
        public ObservableCollection<TlDmCanBoPhuCapImportModel> DmCanBoPhuCapImportModel
        {
            get => _dmCanBoPhuCapImportModel;
            set => SetProperty(ref _dmCanBoPhuCapImportModel, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (SalaryMonthDetailImportModels.Count > 0)
                    return !SalaryMonthDetailImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorBangLuongCommand { get; }
        public RelayCommand ShowErrorBangLuongDetailCommand { get; }

        public ImportSalaryViewModel(ISessionService sessionService,
            ITlDsCapNhapBangLuongService tlDsCapNhapBangLuongService,
            ITlBangLuongThangService tlBangLuongThangService,
            ITlDmCanBoService dmCanBoService,
            ITlDmDonViService dmDonViService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            IMapper mapper,
            IImportExcelService importService)
        {
            _sessionService = sessionService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _dmDonViService = dmDonViService;
            _dmCanBoService = dmCanBoService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _mapper = mapper;
            _importService = importService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ShowErrorBangLuongCommand = new RelayCommand(obj => ShowErrorBangLuong());
            ShowErrorBangLuongDetailCommand = new RelayCommand(obj => ShowErrorBangluongDetail());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            OnResetData();
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            SalaryMonthDetailImportModels = new ObservableCollection<SalaryMonthDetailImportModel>();
            SalaryMonthDetailImportViewModels = new ObservableCollection<SalaryMonthDetailImportModel>();
            SalaryMonthImportModels = new ObservableCollection<SalaryMonthImportModel>();
            _importErrors = new List<ImportErrorItem>();
            _tabIndex = ImportTabIndex.Data;
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file excel");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FileName = openFileDialog.FileName;
        }

        private void ShowErrorBangLuong()
        {
            int rowIndex = _salaryMonthImportModels.IndexOf(SeletedBangLuong);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowErrorBangluongDetail()
        {
            int rowIndex = _salaryMonthDetailImportModels.IndexOf(SelectedItem);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnProcessFile()
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(FileName))
            {
                message = Resources.ErrorFileEmpty;
                goto ShowError;
            }
        ShowError:
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                //lấy thông tin bảng lương
                ImportResult<SalaryMonthImportModel> salaryMonth = _importService.ProcessData<SalaryMonthImportModel>(FileName);
                SalaryMonthImportModels = new ObservableCollection<SalaryMonthImportModel>(salaryMonth.Data);

                //lấy thông tin bảng lương chi tiết
                ImportResult<SalaryMonthDetailImportModel> salaryMonthDetail = _importService.ProcessData<SalaryMonthDetailImportModel>(FileName);
                SalaryMonthDetailImportModels = new ObservableCollection<SalaryMonthDetailImportModel>(salaryMonthDetail.Data);

                // lấy thông tin dm đơn vị
                ImportResult<TlDmDonViImportModel> lstDmDonVi = _importService.ProcessData<TlDmDonViImportModel>(FileName);
                DmDonViImportModel = new ObservableCollection<TlDmDonViImportModel>(lstDmDonVi.Data);

                // lấy thông tin dm cán bộ
                ImportResult<TlDmCanBoImportModel> lstCanBo = _importService.ProcessData<TlDmCanBoImportModel>(FileName);
                DmCanBoImportModels = new ObservableCollection<TlDmCanBoImportModel>(lstCanBo.Data);

                //lấy thông in cán bộ phụ cấp
                ImportResult<TlDmCanBoPhuCapImportModel> lstDmCanBoPhuCap = _importService.ProcessData<TlDmCanBoPhuCapImportModel>(FileName);
                DmCanBoPhuCapImportModel = new ObservableCollection<TlDmCanBoPhuCapImportModel>(lstDmCanBoPhuCap.Data);

                //convert view bang luong
                var listCb = _salaryMonthDetailImportModels.Select(item => item.MaCanBo.Trim()).Distinct();
                var bangLuong = new ObservableCollection<SalaryMonthDetailImportModel>();
                foreach (var maCb in listCb)
                {
                    SalaryMonthDetailImportModel result = new SalaryMonthDetailImportModel();
                    var cb = _salaryMonthDetailImportModels.Where(x => x.MaCanBo == maCb).FirstOrDefault();
                    result.Thang = cb.Thang;
                    result.Nam = cb.Nam;
                    result.MaCanBo = cb.MaCanBo;
                    result.TenCanBo = cb.TenCanBo;
                    result.MaCachTinhLuong = cb.MaCachTinhLuong;
                    result.MaDonVi = cb.MaDonVi;
                    result.MaCapBac = cb.MaCapBac;
                    result.LstPhuCap = _salaryMonthDetailImportModels.Where(item => item.MaCanBo == maCb).OrderBy(x => x.MaPhuCap).ToList();
                    bangLuong.Add(result);
                }
                SalaryMonthDetailImportViewModels = bangLuong;

                if (salaryMonth.ImportErrors.Count > 0)
                    errors.AddRange(salaryMonth.ImportErrors);
                if (salaryMonthDetail.ImportErrors.Count > 0)
                    errors.AddRange(salaryMonthDetail.ImportErrors);

                if (_salaryMonthDetailImportModels == null || _salaryMonthDetailImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (_salaryMonthDetailImportModels.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);

                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnSaveData()
        {
            foreach (var bl in SalaryMonthImportModels)
            {
                var blExists = _tlDsCapNhapBangLuongService.FindByCondition(x =>
                    x.Thang.Equals(decimal.Parse(bl.Thang))
                    && x.Nam.Equals(decimal.Parse(bl.Nam))
                    && x.MaCbo.Equals(bl.MaDonVi)
                    && x.MaCachTl.Equals(bl.MaCachTinhLuong)).FirstOrDefault();
                if (blExists != null)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.SalaryTableExist, int.Parse(bl.Thang), int.Parse(bl.Nam), ""));
                    return;
                }
                
            }
            bool importAll = false;
            var messageBox = MessageBoxHelper.Confirm("Đồng chí có muốn nhận dữ liệu cán bộ không?");
            if (messageBox == MessageBoxResult.Yes)
                importAll = true;
            if (importAll)
            {
                // lưu dm đơn vị
                ObservableCollection<TlDmDonVi> tlDmDonVis = new ObservableCollection<TlDmDonVi>();
                ObservableCollection<TlDmDonVi> tlDmDonVisUpdate = new ObservableCollection<TlDmDonVi>();
                foreach (var dv in DmDonViImportModel)
                {

                    TlDmDonVi tlDmDonVi = _dmDonViService.FindByMaDonVi(dv.MaDonVi);
                    if (tlDmDonVi == null)
                    {
                        tlDmDonVi = new TlDmDonVi();
                        tlDmDonVi.MaDonVi = dv.MaDonVi;
                        tlDmDonVi.TenDonVi = dv.TenDonVi;
                        tlDmDonVi.ParentId = dv.ParentId;
                        tlDmDonVi.XauNoiMa = dv.XauNoiMa;
                        tlDmDonVis.Add(tlDmDonVi);
                    }
                    else
                    {
                        tlDmDonVi.MaDonVi = dv.MaDonVi;
                        tlDmDonVi.TenDonVi = dv.TenDonVi;
                        tlDmDonVi.ParentId = dv.ParentId;
                        tlDmDonVi.XauNoiMa = dv.XauNoiMa;
                        tlDmDonVisUpdate.Add(tlDmDonVi);
                    }
                }

                if (tlDmDonVis.Count > 0)
                {
                    _dmDonViService.AddRange(tlDmDonVis);
                }
                if (tlDmDonVisUpdate.Count > 0)
                {
                    _dmDonViService.UpdateRange(tlDmDonVisUpdate.ToList());
                }
                // lưu dm cán bộ
                ObservableCollection<TlDmCanBo> tlDmCanBos = new ObservableCollection<TlDmCanBo>();
                ObservableCollection<TlDmCanBo> tlDmCanBosUpdate = new ObservableCollection<TlDmCanBo>();
                foreach (var cb in DmCanBoImportModels)
                {
                    TlDmCanBo tlDmCanBo = _dmCanBoService.FindByMaCanBo(cb.MaCanBo);
                    if (tlDmCanBo == null)
                    {
                        tlDmCanBo = new TlDmCanBo();
                        ConvertToTlDmCanBo(tlDmCanBo, cb);
                        tlDmCanBos.Add(tlDmCanBo);
                    }
                    else
                    {
                        ConvertToTlDmCanBo(tlDmCanBo, cb);
                        tlDmCanBosUpdate.Add(tlDmCanBo);
                    }
                }
                if (tlDmCanBos.Count > 0)
                {
                    _dmCanBoService.BulkInsert(tlDmCanBos);
                }
                if (tlDmCanBosUpdate.Count > 0)
                {
                    _dmCanBoService.BulkUpdate(tlDmCanBosUpdate.ToList());
                }

                //lưu dm cán bộ phụ cấp
                ObservableCollection<TlCanBoPhuCap> tldBoPhuCaps = new ObservableCollection<TlCanBoPhuCap>();
                ObservableCollection<TlCanBoPhuCap> tldBoPhuCapsUpdate = new ObservableCollection<TlCanBoPhuCap>();
                var listMaCbImport = DmCanBoImportModels.Select(x => x.MaCanBo).Distinct().ToList();
                var lstPhuCapOld = _tlCanBoPhuCapService.FindAll(x => listMaCbImport.Contains(x.MaCbo));
                foreach (var cb in DmCanBoPhuCapImportModel)
                {
                    TlCanBoPhuCap tlCanBoPhuCap = lstPhuCapOld
                        .Where(x => x.MaCbo.Equals(cb.MaCbo) && x.MaPhuCap.Equals(cb.MaPhuCap)).FirstOrDefault();
                    if (tlCanBoPhuCap == null)
                    {
                        tlCanBoPhuCap = new TlCanBoPhuCap();
                        ConvertToTlDmCanBoPhuCap(tlCanBoPhuCap, cb);
                        tldBoPhuCaps.Add(tlCanBoPhuCap);
                    }
                    else
                    {
                        ConvertToTlDmCanBoPhuCap(tlCanBoPhuCap, cb);
                        tldBoPhuCapsUpdate.Add(tlCanBoPhuCap);
                    }
                }
                if (tldBoPhuCaps.Count > 0)
                {
                    _tlCanBoPhuCapService.BulkInsert(tldBoPhuCaps);
                }
                if (tldBoPhuCapsUpdate.Count > 0)
                {
                    _tlCanBoPhuCapService.BulkUpdate(tldBoPhuCapsUpdate.ToList());
                }
            }

            foreach (var bl in SalaryMonthImportModels)
            {
                TlDsCapNhapBangLuong tlDsCapNhapBangLuong = new TlDsCapNhapBangLuong();
                tlDsCapNhapBangLuong.TenDsCnbluong = bl.TenBangLuong;
                tlDsCapNhapBangLuong.TuNgay  = DateTime.ParseExact(bl.TuNgay, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                tlDsCapNhapBangLuong.DenNgay = DateTime.ParseExact(bl.DenNgay, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                tlDsCapNhapBangLuong.MaCbo = bl.MaDonVi;
                tlDsCapNhapBangLuong.Thang = int.Parse(bl.Thang);
                tlDsCapNhapBangLuong.Nam = int.Parse(bl.Nam);
                tlDsCapNhapBangLuong.MaCachTl = bl.MaCachTinhLuong;
                tlDsCapNhapBangLuong.Status = true;
                tlDsCapNhapBangLuong.NgayTaoBL = DateTime.Now;
                tlDsCapNhapBangLuong.IsTongHop = false;
                _tlDsCapNhapBangLuongService.Add(tlDsCapNhapBangLuong);
                var id = tlDsCapNhapBangLuong.Id;

                // lưu bảng lương tháng chi tiết
                ObservableCollection<TlBangLuongThangModel> tlBangLuongThangModels = new ObservableCollection<TlBangLuongThangModel>();

                foreach (var item in SalaryMonthDetailImportModels)
                {
                    TlBangLuongThangModel blDetail = new TlBangLuongThangModel();
                    blDetail.Thang = int.Parse(item.Thang);
                    blDetail.Nam = int.Parse(item.Nam);
                    blDetail.MaCbo = item.MaCanBo;
                    blDetail.TenCbo = item.TenCanBo;
                    blDetail.MaDonVi = item.MaDonVi;
                    blDetail.MaCachTl = item.MaCachTinhLuong;
                    blDetail.Parent = id;
                    blDetail.MaCb = item.MaCapBac;
                    blDetail.MaPhuCap = item.MaPhuCap;
                    blDetail.GiaTri = string.IsNullOrEmpty(item.GiaTri) ? 0 : decimal.Parse(item.GiaTri);
                    blDetail.MaHieuCanBo = item.MaHieuCanBo;
                    tlBangLuongThangModels.Add(blDetail);
                }
                IEnumerable<TlBangLuongThang> tlBangLuongThangs = _mapper.Map<ObservableCollection<TlBangLuongThang>>(tlBangLuongThangModels);
                _tlBangLuongThangService.BulkInsert(tlBangLuongThangs);

                SavedAction?.Invoke(_mapper.Map<TlDSCapNhapBangLuongModel>(tlDsCapNhapBangLuong));
            }
        }

        public TlDmCanBo ConvertToTlDmCanBo(TlDmCanBo tlDmCanBo, TlDmCanBoImportModel canBoImport)
        {
            tlDmCanBo.MaCanBo = canBoImport.MaCanBo;
            tlDmCanBo.TenCanBo = canBoImport.TenCanBo;
            tlDmCanBo.DiaChi = canBoImport.DiaChi;
            tlDmCanBo.MaCv = canBoImport.MaCv;
            tlDmCanBo.MaBl = canBoImport.MaBl;
            tlDmCanBo.MaCb = canBoImport.MaCb;
            tlDmCanBo.MaPban = canBoImport.MaPban;
            tlDmCanBo.Gtgc = string.IsNullOrEmpty(canBoImport.Gtgc) ? 0 : decimal.Parse(canBoImport.Gtgc);
            tlDmCanBo.DienThoai = canBoImport.DienThoai;
            tlDmCanBo.MaSoVat = canBoImport.MaSoVat;
            tlDmCanBo.TenDonVi = canBoImport.TenDonVi;
            tlDmCanBo.SoCmt = canBoImport.SoCmt;
            tlDmCanBo.NoiCapCmt = canBoImport.NoiCapCmt;
            tlDmCanBo.NgayCapCmt = string.IsNullOrEmpty(canBoImport.NgayCapCmt) ? (DateTime?)null : DateTime.ParseExact(canBoImport.NgayCapCmt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture); 
            tlDmCanBo.SoTaiKhoan = canBoImport.SoTaiKhoan;
            tlDmCanBo.TenKhoBac = canBoImport.TenKhoBac;
            tlDmCanBo.MaSoDvSdns = canBoImport.MaSoDvSdns;
            tlDmCanBo.MaDiaBanHc = canBoImport.MaDiaBanHc;
            tlDmCanBo.MaTkLq = canBoImport.MaTkLq;
            tlDmCanBo.Parent = canBoImport.Parent;
            tlDmCanBo.MaKhoBac = canBoImport.MaKhoBac;
            tlDmCanBo.Splits = string.IsNullOrEmpty(canBoImport.Splits) || canBoImport.Splits.Equals(BooleanString.FALSE)? false : true;
            tlDmCanBo.Readonly = string.IsNullOrEmpty(canBoImport.Readonly) || canBoImport.Readonly.Equals(BooleanString.FALSE) ? false : true;
            tlDmCanBo.KhongLuong = string.IsNullOrEmpty(canBoImport.KhongLuong) || canBoImport.KhongLuong.Equals(BooleanString.FALSE) ? false : true;
            tlDmCanBo.MaHieuCanBo = canBoImport.MaHieuCanBo;
            tlDmCanBo.Thang = string.IsNullOrEmpty(canBoImport.Thang) ? 0 : int.Parse(canBoImport.Thang);
            tlDmCanBo.Nam = string.IsNullOrEmpty(canBoImport.Nam) ? 0 : int.Parse(canBoImport.Nam);
            tlDmCanBo.NgayNn = string.IsNullOrEmpty(canBoImport.NgayNn) ? (DateTime?)null : DateTime.ParseExact(canBoImport.NgayNn, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            tlDmCanBo.NgayXn = string.IsNullOrEmpty(canBoImport.NgayXn) ? (DateTime?)null : DateTime.ParseExact(canBoImport.NgayXn, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            tlDmCanBo.NgayTn = string.IsNullOrEmpty(canBoImport.NgayTn) ? (DateTime?)null : DateTime.ParseExact(canBoImport.NgayTn, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            tlDmCanBo.NamTn = string.IsNullOrEmpty(canBoImport.NamTn) ? 0 : int.Parse(canBoImport.NamTn);
            tlDmCanBo.ThangTnn = string.IsNullOrEmpty(canBoImport.ThangTnn) ? 0 : int.Parse(canBoImport.ThangTnn);
            tlDmCanBo.NamVk = string.IsNullOrEmpty(canBoImport.NamVk) ? 0 : int.Parse(canBoImport.NamVk);
            tlDmCanBo.IsNam = string.IsNullOrEmpty(canBoImport.IsNam) || canBoImport.IsNam.Equals(BooleanString.FALSE) ? false : true;
            tlDmCanBo.MaTangGiam = canBoImport.MaTangGiam;
            tlDmCanBo.SoSoLuong = canBoImport.SoSoLuong;
            tlDmCanBo.NgayNhanCb = string.IsNullOrEmpty(canBoImport.NgayNhanCb) ? (DateTime?)null : DateTime.ParseExact(canBoImport.NgayNhanCb, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            tlDmCanBo.ThoiHanTangCb = string.IsNullOrEmpty(canBoImport.ThoiHanTangCb) ? 0 : int.Parse(canBoImport.ThoiHanTangCb);
            tlDmCanBo.CbKeHoach = canBoImport.CbKeHoach;
            tlDmCanBo.Cccd = canBoImport.Cccd;
            tlDmCanBo.NoiCongTac = canBoImport.NoiCongTac;
            tlDmCanBo.NgaySinh = string.IsNullOrEmpty(canBoImport.NgaySinh) ? (DateTime?)null : DateTime.ParseExact(canBoImport.NgaySinh, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            tlDmCanBo.Tm = string.IsNullOrEmpty(canBoImport.Tm) || canBoImport.Tm.Equals(BooleanString.FALSE) ? false : true;
            tlDmCanBo.IsLock = string.IsNullOrEmpty(canBoImport.IsLock) || canBoImport.IsLock.Equals(BooleanString.FALSE) ? false : true;
            tlDmCanBo.IsDelete = string.IsNullOrEmpty(canBoImport.IsDelete) || canBoImport.IsDelete.Equals(BooleanString.FALSE) ? false : true;
            tlDmCanBo.DateCreated = string.IsNullOrEmpty(canBoImport.DateCreated) ? (DateTime?)null : DateTime.ParseExact(canBoImport.DateCreated, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            tlDmCanBo.DateModified = string.IsNullOrEmpty(canBoImport.DateModified) ? (DateTime?)null : DateTime.ParseExact(canBoImport.DateModified, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            tlDmCanBo.UserCreator = canBoImport.UserCreator;
            tlDmCanBo.UserModifier = canBoImport.UserModifier;
            tlDmCanBo.BHTN = string.IsNullOrEmpty(canBoImport.BHTN) || canBoImport.BHTN.Equals(BooleanString.FALSE) ? false : true;
            tlDmCanBo.PCCV = string.IsNullOrEmpty(canBoImport.PCCV) || canBoImport.PCCV.Equals(BooleanString.FALSE) ? false : true;
            tlDmCanBo.HsLuongTran = string.IsNullOrEmpty(canBoImport.HsLuongTran) ? 0 : decimal.Parse(canBoImport.HsLuongTran);
            tlDmCanBo.HsLuongKeHoach = string.IsNullOrEmpty(canBoImport.HsLuongKeHoach) ? 0 : decimal.Parse(canBoImport.HsLuongKeHoach);
            tlDmCanBo.HeSoLuong = string.IsNullOrEmpty(canBoImport.HeSoLuong) ? 0 : decimal.Parse(canBoImport.HeSoLuong);
            tlDmCanBo.IdLuongTran = string.IsNullOrEmpty(canBoImport.IdLuongTran) ? (Guid?)null : Guid.Parse(canBoImport.IdLuongTran);
            tlDmCanBo.MaCbCu = canBoImport.MaCbCu;
            tlDmCanBo.Nhom = canBoImport.Nhom;
            return tlDmCanBo;
        }

        public TlCanBoPhuCap ConvertToTlDmCanBoPhuCap(TlCanBoPhuCap tlCanBoPhuCap, TlDmCanBoPhuCapImportModel canBoPhuCapImport)
        {
            tlCanBoPhuCap.MaCbo = canBoPhuCapImport.MaCbo;
            tlCanBoPhuCap.MaPhuCap = canBoPhuCapImport.MaPhuCap;
            tlCanBoPhuCap.GiaTri = string.IsNullOrEmpty(canBoPhuCapImport.GiaTri) ? 0 : decimal.Parse(canBoPhuCapImport.GiaTri);

            if (!string.IsNullOrEmpty(canBoPhuCapImport.HeSo))
                tlCanBoPhuCap.HeSo = decimal.Parse(canBoPhuCapImport.HeSo);
            else
                tlCanBoPhuCap.HeSo = null;

            if (!string.IsNullOrEmpty(canBoPhuCapImport.PhanTramCt))
                tlCanBoPhuCap.PhanTramCt = decimal.Parse(canBoPhuCapImport.PhanTramCt);
            else
                tlCanBoPhuCap.PhanTramCt = null;

            if (!string.IsNullOrEmpty(canBoPhuCapImport.HuongPcSn))
                tlCanBoPhuCap.HuongPcSn = decimal.Parse(canBoPhuCapImport.HuongPcSn);
            else
                tlCanBoPhuCap.HuongPcSn = null;

            tlCanBoPhuCap.MaKmcp = canBoPhuCapImport.MaKmcp;
            tlCanBoPhuCap.CongThuc = canBoPhuCapImport.CongThuc;
            tlCanBoPhuCap.Chon = string.IsNullOrEmpty(canBoPhuCapImport.Chon) || canBoPhuCapImport.Chon.Equals(BooleanString.FALSE) ? false : true;
            tlCanBoPhuCap.Flag = string.IsNullOrEmpty(canBoPhuCapImport.Flag) || canBoPhuCapImport.Flag.Equals(BooleanString.FALSE) ? false : true;
            return tlCanBoPhuCap;
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}

