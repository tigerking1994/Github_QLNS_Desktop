using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewTransferData.NewTransferCadres;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewTransferData.NewTransferDataKtdt
{
    public class NewTransferDataKtdtIndexViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmCanBoNq104Service _cadresService;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private readonly ITlDmPhuCapNq104Service _tlDmPhuCapService;
        private readonly ITlDmCapBacService _tlDmCapBacService;
        private readonly ITlDmChucVuService _tlDmChucVuService;
        private ObservableCollection<TlDmDonViNq104Model> _lstDonVi;
        private ObservableCollection<CadresNq104Model> _lstCadres;
        private List<TlCanBoPhuCapNq104Model> _lstCanBoPhuCap;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_CHUYEN_DOI_DU_LIEU_KTDT_INDEX;
        public override string GroupName => MenuItemContants.GROUP_TRANSFER_FROM_KTDT;
        public override string Name => "Chuyển đổi dữ liệu đối tượng";
        public override Type ContentType => typeof(View.NewSalary.NewTransferData.NewTranferDataKtdt.NewTransferDataKtdtIndex);
        public override PackIconKind IconKind => PackIconKind.FolderSwapOutline;
        public override string Title => "Chuyển đổi dữ liệu đối tượng hưởng lương, phụ cấp từ phần mềm KTDT";
        public override string Description => "Chuyển đổi dữ liệu đối tượng hưởng lương, phụ cấp từ phần mềm KTDT";

        private DataTable _dtDonVi;
        public DataTable DtDonVi
        {
            get => _dtDonVi;
            set => SetProperty(ref _dtDonVi, value);
        }

        private DataTable _dtDoiTuong;
        public DataTable DtDoiTuong
        {
            get => _dtDoiTuong;
            set
            {
                SetProperty(ref _dtDoiTuong, value);
                if (_dtDoiTuong == null)
                {
                    IsEnabled = false;
                }
                else
                {
                    if (_dtDoiTuong.Rows.Count == 0)
                    {
                        IsEnabled = false;
                    }
                    else
                    {
                        IsEnabled = true;
                    }
                }
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private DataTable _dtPhuCapCanBo;
        public DataTable DtPhuCapCanBo
        {
            get => _dtPhuCapCanBo;
            set => SetProperty(ref _dtPhuCapCanBo, value);
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private int _month;
        public int Month
        {
            get => _month;
            set => _month = value;
        }

        private int _year;
        public int Year
        {
            get => _year;
            set => _year = value;
        }

        private string warningMessage;

        public NewTransferDataKtdtDialogViewModel TransferDataKtdtDialogViewModel { get; }
        public NewTransferDataDetailViewModel TransferDataDetailViewModel { get; }

        public RelayCommand OpenCommand { get; }

        public NewTransferDataKtdtIndexViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmCanBoNq104Service cadresService,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapService,
            ITlDmPhuCapNq104Service tlDmPhuCapService,
            ITlDmCapBacService tlDmCapBacService,
            ITlDmChucVuService tlDmChucVuService,
            NewTransferDataKtdtDialogViewModel transferDataKtdtDialogViewModel,
            NewTransferDataDetailViewModel transferDataDetailViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;

            _cadresService = cadresService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDmChucVuService = tlDmChucVuService;

            TransferDataKtdtDialogViewModel = transferDataKtdtDialogViewModel;
            TransferDataDetailViewModel = transferDataDetailViewModel;

            OpenCommand = new RelayCommand(o => OnOpen());
        }

        public override void Init()
        {
            base.Init();
        }

        private void OnOpen()
        {
            TransferDataKtdtDialogViewModel.Init();
            TransferDataKtdtDialogViewModel.ChooseAction = obj =>
            {
                this.DtDoiTuong = TransferDataKtdtDialogViewModel.DtDoiTuong;
                this.DtDonVi = TransferDataKtdtDialogViewModel.DtDonVi;
                this.DtPhuCapCanBo = TransferDataKtdtDialogViewModel.DtPhuCapCanBo;
                Month = int.Parse(TransferDataKtdtDialogViewModel.MonthImportSelected.ValueItem);
                Year = int.Parse(TransferDataKtdtDialogViewModel.YearImportSelected.ValueItem);
                OnPropertyChanged(nameof(DtDonVi));
                OnPropertyChanged(nameof(DtDoiTuong));
                OnPropertyChanged(nameof(DtPhuCapCanBo));
            };
            TransferDataKtdtDialogViewModel.ShowDialogHost();
        }

        public override void OnSave()
        {
            _lstDonVi = new ObservableCollection<TlDmDonViNq104Model>();
            _lstCadres = new ObservableCollection<CadresNq104Model>();
            _lstCanBoPhuCap = new List<TlCanBoPhuCapNq104Model>();
            SaveDonVi();
            SaveCanBo();
            foreach (var item in _lstCadres)
            {
                var capBac = _tlDmCapBacService.FindByMaCapBac(item.MaCb);
                if (capBac != null)
                {
                    item.CapBac = capBac.Note;
                }
                var chucVu = _tlDmChucVuService.FindByMaChucVu(item.MaCv);
                if (chucVu != null)
                {
                    item.ChucVu = chucVu.TenCv;
                }
            }

            if (_lstCadres == null || _lstCadres.Count == 0)
            {
                MessageBox.Show("Không đối tượng nào được import.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var canBo = _lstCadres.FirstOrDefault();
                TransferDataDetailViewModel.Model = canBo;
                TransferDataDetailViewModel.NgayNhapNgu = canBo.NgayNn;
                TransferDataDetailViewModel.NgayXuatNgu = canBo.NgayXn;
                TransferDataDetailViewModel.NgayTaiNgu = canBo.NgayTn;
                TransferDataDetailViewModel.NamThamNien = canBo.NamTn == null ? 0 : (int)canBo.NamTn;
                TransferDataDetailViewModel.ThangThamNienNghe = canBo.ThangTnn == null ? 0 : (int)canBo.ThangTnn;
                TransferDataDetailViewModel.ViewState = Utility.Enum.FormViewState.DETAIL;
                TransferDataDetailViewModel.LstCanBo = _lstCadres;
                TransferDataDetailViewModel.DonViItems = _lstDonVi;
                TransferDataDetailViewModel.LstCanBoPhuCap = new ObservableCollection<TlCanBoPhuCapNq104Model>(_lstCanBoPhuCap);
                TransferDataDetailViewModel.SearchTenPhuCap = string.Empty;
                TransferDataDetailViewModel.SearchMaPhuCap = string.Empty;
                TransferDataDetailViewModel.SelectedDonVi = _lstDonVi.FirstOrDefault(x => x.MaDonVi == canBo.Parent);
                TransferDataDetailViewModel.SelectedCanBo = canBo;
                TransferDataDetailViewModel.SavedAction = obj =>
                {
                    DtDoiTuong = null;
                    DtDonVi = null;
                    DtPhuCapCanBo = null;
                };
                TransferDataDetailViewModel.Init();
                var view = new View.NewSalary.NewTransferData.NewTransferCadres.NewDataTransferDetail
                {
                    DataContext = TransferDataDetailViewModel
                };
                view.ShowDialog();
            }
        }

        private void SaveCanBo()
        {
            var lstAllCanBo = _cadresService.FindAll();
            var lstPhuCap = _tlDmPhuCapService.FindAll();
            int maHieuCanBoMax = 0;
            if (lstAllCanBo.Count() > 0 && lstAllCanBo != null)
            {
                maHieuCanBoMax = lstAllCanBo.Max(x => int.Parse(x.MaHieuCanBo));
            }

            foreach (DataRow item in DtDoiTuong.Rows)
            {
                CadresNq104Model cadresModel = new CadresNq104Model();
                cadresModel.Thang = Month;
                cadresModel.Nam = Year;
                cadresModel.IsDelete = true;
                cadresModel.BHTN = false;
                cadresModel.PCCV = true;
                cadresModel.KhongLuong = false;
                cadresModel.Tm = false;
                cadresModel.IsLock = false;
                cadresModel.MaHieuCanBo = (++maHieuCanBoMax).ToString();
                cadresModel.MaCanBo = Year + Month.ToString("D2") + cadresModel.MaHieuCanBo;

                var tlCanBoPhuCapModels = new List<TlCanBoPhuCapNq104Model>();

                foreach (var item1 in lstPhuCap)
                {
                    TlCanBoPhuCapNq104Model tlCanBoPhuCapModel = new TlCanBoPhuCapNq104Model();
                    tlCanBoPhuCapModel.MaCbo = cadresModel.MaCanBo;
                    tlCanBoPhuCapModel.MaPhuCap = item1.MaPhuCap;
                    tlCanBoPhuCapModel.GiaTri = item1.GiaTri;
                    tlCanBoPhuCapModel.HuongPcSn = item1.HuongPCSN;
                    tlCanBoPhuCapModel.Flag = false;
                    tlCanBoPhuCapModel.BSaoChep = item1.BSaoChep;
                    tlCanBoPhuCapModels.Add(tlCanBoPhuCapModel);
                }

                cadresModel.TenCanBo = item["TEN_CBO"].ToString();
                cadresModel.MaCv = item["MA_CV"].ToString();
                cadresModel.MaCb = item["MA_NCC"].ToString();
                cadresModel.DienThoai = item["DIEN_THOAI"].ToString();
                cadresModel.MaSoVat = item["MASO_VAT"].ToString();
                cadresModel.SoTaiKhoan = item["SO_TAIKHOAN"].ToString();
                cadresModel.SoSoLuong = item["MA_CBO"].ToString();
                cadresModel.TenKhoBac = item["TEN_KHOBAC"].ToString();
                cadresModel.Parent = item["PARENT"].ToString();
                cadresModel.KhongLuong = item.Field<bool?>("KHONG_LUONG");
                cadresModel.IsNam = item.Field<bool?>("IS_NAM");
                cadresModel.NgayNn = item.Field<DateTime?>("NGAY_NN");
                cadresModel.NgayXn = item.Field<DateTime?>("NGAY_XN");
                cadresModel.NgayTn = item.Field<DateTime?>("NGAY_TN");
                cadresModel.ThangTnn = item.Field<int?>("THANG_TNN");
                cadresModel.MaTangGiam = item["MA_TAGIAM"].ToString().Trim();
                if (item.Field<decimal?>("TM") == 0)
                {
                    cadresModel.Tm = true;
                }
                else
                {
                    cadresModel.Tm = false;
                }

                foreach (var item2 in tlCanBoPhuCapModels)
                {
                    var row = DtPhuCapCanBo.AsEnumerable().FirstOrDefault(x => x["MA_PHUCAP"].ToString() == item2.MaPhuCap && x["MA_CBO"].ToString() == item["MA_CBO"].ToString());
                    if (row != null)
                    {
                        item2.GiaTri = row.Field<decimal?>("GIA_TRI");
                    }
                }

                cadresModel.NamTn = TinhNamThamNien(cadresModel.NgayNn, cadresModel.NgayXn, cadresModel.NgayTn, (cadresModel.ThangTnn ?? 0), cadresModel.Nam, cadresModel.Thang);

                var donVi = _lstDonVi.FirstOrDefault(x => x.MaDonVi == cadresModel.Parent);
                if (donVi != null)
                {
                    cadresModel.TenDonVi = donVi.TenDonVi;
                }

                var phuCapNtn = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.NTN.Equals(x.MaPhuCap));
                if (phuCapNtn != null)
                {
                    phuCapNtn.GiaTri = cadresModel.NamTn;
                }

                var pcTm = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.TM.Equals(x.MaPhuCap));
                if (pcTm != null)
                {
                    pcTm.GiaTri = cadresModel.Tm == true ? 1 : 0;
                }

                var pcBhtn = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.Nop_BHTN.Equals(x.MaPhuCap));
                if (pcTm != null)
                {
                    pcTm.GiaTri = cadresModel.BHTN == true ? 1 : 0;
                }

                var pcPccov = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.Huong_PCCOV.Equals(x.MaPhuCap));
                if (pcPccov != null)
                {
                    pcPccov.GiaTri = cadresModel.PCCV == true ? 1 : 0;
                }

                var pcLht = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.LHT_HS.Equals(x.MaPhuCap));
                if (pcLht != null)
                {
                    cadresModel.HeSoLuong = pcLht.GiaTri;
                }

                var pcSnpt = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.GTPT_SN.Equals(x.MaPhuCap));
                if (pcSnpt != null)
                {
                    //cadresModel.SoNguoiPhuThuoc = pcSnpt.GiaTri;
                }

                var pccv = tlCanBoPhuCapModels.FirstOrDefault(x => PhuCap.PCCV_HS.Equals(x.MaPhuCap));
                if (pccv != null)
                {
                    var cv = _tlDmChucVuService.FindByHeSoChucVu(pccv.GiaTri);
                    if (cv != null)
                    {
                        cadresModel.MaCv = cv.MaCv;
                    }
                }

                var canBo = lstAllCanBo.FirstOrDefault(x => x.SoSoLuong.Equals(cadresModel.SoSoLuong));
                if (canBo != null)
                {
                    maHieuCanBoMax--;
                }
                else
                {
                    _lstCadres.Add(cadresModel);
                    _lstCanBoPhuCap.AddRange(tlCanBoPhuCapModels);
                }
            }
        }

        private void SaveDonVi()
        {
            foreach (DataRow item in DtDonVi.Rows)
            {
                TlDmDonViNq104Model tlDmDonVi = new TlDmDonViNq104Model();
                tlDmDonVi.MaDonVi = item.Field<string>("MA_CBO").Trim();
                tlDmDonVi.TenDonVi = item.Field<string>("TEN_CBO").Trim();
                _lstDonVi.Add(tlDmDonVi);
            }
        }

        private int TinhNamThamNien(DateTime? ngayNn, DateTime? ngayXn, DateTime? ngayTn, int? thangTnn, int? nam, int? thang)
        {
            return DateUtils.TinhNamThamNien(ngayNn, ngayXn, ngayTn, (int)thangTnn, (int)thang, (int)nam);
        }
    }
}
