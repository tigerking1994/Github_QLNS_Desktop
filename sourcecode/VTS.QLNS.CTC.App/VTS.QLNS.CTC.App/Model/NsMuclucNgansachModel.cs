using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NsMuclucNgansachModel : ModelBase
    {
        private Guid _mlnsId;
        public Guid MlnsId
        {
            get => _mlnsId;
            set => SetProperty(ref _mlnsId, value);
        }

        private Guid? _mlnsIdParent;
        public Guid? MlnsIdParent
        {
            get => _mlnsIdParent;
            set => SetProperty(ref _mlnsIdParent, value);
        }

        private string _lns;
        [DisplayName("LNS")]
        [DisplayDetailInfo("LNS")]
        public string Lns
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        [DisplayName("L")]
        [DisplayDetailInfo("L")]
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        [DisplayName("K")]
        [DisplayDetailInfo("K")]
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        [DisplayName("M")]
        [DisplayDetailInfo("M")]
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        [DisplayName("TM")]
        [DisplayDetailInfo("TM")]
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        [DisplayName("TTM")]
        [DisplayDetailInfo("TTM")]
        public string TTM
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        [DisplayName("NG")]
        [DisplayDetailInfo("NG")]
        public string NG
        {
            get => _ng;
            set
            {
                SetProperty(ref _ng, value);
                OnPropertyChanged(nameof(IsEnableCheckData));
            }
        }

        private string _tng;
        [DisplayName("TNG")]
        [DisplayDetailInfo("TNG")]
        public string TNG
        {
            get => _tng;
            set
            {
                SetProperty(ref _tng, value);
                OnPropertyChanged(nameof(IsEnableCheckData));
            }
        }

        private string _tng1;
        [DisplayName("TNG1")]
        [DisplayDetailInfo("TNG1")]
        public string TNG1
        {
            get => _tng1;
            set => SetProperty(ref _tng1, value);
        }

        private string _tng2;
        [DisplayName("TNG2")]
        [DisplayDetailInfo("TNG2")]
        public string TNG2
        {
            get => _tng2;
            set => SetProperty(ref _tng2, value);
        }

        private string _tng3;
        [DisplayName("TNG3")]
        [DisplayDetailInfo("TNG3")]
        public string TNG3
        {
            get => _tng3;
            set => SetProperty(ref _tng3, value);
        }

        private string _mota;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string MoTa
        {
            get => _mota;
            set => SetProperty(ref _mota, value);
        }
        private double _fTienAn;
        [DisplayName("Mức tiền ăn")]
        [DisplayDetailInfo("Mức tiền ăn")]
        public double FTienAn
        {
            get => _fTienAn;
            set => SetProperty(ref _fTienAn, value);
        }

        private string _xauNoima;
        [DisplayDetailInfo("Xâu nối mã")]
        public string XauNoiMa
        {
            get => _xauNoima;
            set => SetProperty(ref _xauNoima, value);
        }

        public string LNSDisplay => String.Format("{0} - {1}", Lns, MoTa);

        public string XNMDisplay => String.Format("{0} - {1}", XauNoiMa, MoTa);

        private int? _iTrangThai;
        [DisplayName("Trạng thái")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAllTrangThaiMLNS")]
        public int? ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }

        [DisplayDetailInfo("Năm làm việc")]
        public int? NamLamViec { get; set; }
        public string Chuong { get; set; }

        private bool _bHangCha;
        public bool BHangCha
        {
            get => _bHangCha;
            set
            {
                SetProperty(ref _bHangCha, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        private string _idPhongBan;
        public string IdPhongBan
        {
            get => _idPhongBan;
            set => SetProperty(ref _idPhongBan, value);
        }

        private string _tenPhongBan;
        public string TenPhongBan
        {
            get => _tenPhongBan;
            set => SetProperty(ref _tenPhongBan, value);
        }

        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public bool? ILock { get; set; }
        public string IdDonViTao { get; set; }

        public string TrangThaiDisplay
        {
            get => ITrangThai switch
            {
                0 => "không sử dụng",
                1 => "Đang sử dụng",
                2 => "ngành nghiệp vụ",
                _ => ""
            };
        }

        public override string DetailInfoModalTitle => "Chi tiết MLNS " + XauNoiMa;

        public override bool IsEditable => !IsDeleted;

        public bool IsHangCha
        {
            get => BHangCha;
        }

        private string _chiTietToi;
        [DisplayDetailInfo("Chi tiết đến")]
        public string ChiTietToi
        {
            get => _chiTietToi;
            set => SetProperty(ref _chiTietToi, value);
        }

        public NsMuclucNgansachModel() { }

        public NsMuclucNgansachModel(string lns, string moTa)
        {
            _lns = lns;
            MoTa = moTa;
        }

        private bool _isUnableEditBQuanlyChiTietToi;
        public bool IsUnableEditBQuanlyChiTietToi
        {
            get => _isUnableEditBQuanlyChiTietToi;
            set => SetProperty(ref _isUnableEditBQuanlyChiTietToi, value);
        }

        private ObservableCollection<ComboboxItem> _mlnsReportSetting;
        public ObservableCollection<ComboboxItem> MLNSReportSetting
        {
            get => _mlnsReportSetting;
            set => SetProperty(ref _mlnsReportSetting, value);
        }

        private string _selectedMLNSReportSetting;
        public string SelectedMLNSReportSetting
        {
            get => _selectedMLNSReportSetting;
            set => SetProperty(ref _selectedMLNSReportSetting, value);
        }

        private bool _bNgay;
        public bool BNgay
        {
            get => _bNgay;
            set => SetProperty(ref _bNgay, value);
        }

        private bool _bSoNguoi;
        public bool BSoNguoi
        {
            get => _bSoNguoi;
            set => SetProperty(ref _bSoNguoi, value);
        }

        private bool _bTonKho;
        public bool BTonKho
        {
            get => _bTonKho;
            set => SetProperty(ref _bTonKho, value);
        }

        private bool _bTuChi;
        public bool BTuChi
        {
            get => _bTuChi;
            set => SetProperty(ref _bTuChi, value);
        }

        private bool _bHangNhap;
        public bool BHangNhap
        {
            get => _bHangNhap;
            set => SetProperty(ref _bHangNhap, value);
        }

        private bool _bHangMua;
        public bool BHangMua
        {
            get => _bHangMua;
            set => SetProperty(ref _bHangMua, value);
        }

        private bool _bHienVat;
        public bool BHienVat
        {
            get => _bHienVat;
            set => SetProperty(ref _bHienVat, value);
        }

        private bool _bDuPhong;
        public bool BDuPhong
        {
            get => _bDuPhong;
            set => SetProperty(ref _bDuPhong, value);
        }

        private bool _bPhanCap;
        public bool BPhanCap
        {
            get => _bPhanCap;
            set => SetProperty(ref _bPhanCap, value);
        }

        private string _sNhapTheoTruong;
        public string SNhapTheoTruong
        {
            get => _sNhapTheoTruong;
            set => SetProperty(ref _sNhapTheoTruong, value);
        }

        public string IdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public int? INamNganSach { get; set; }


        public bool IsHitTestVisible { get; set; } = true;
        private string _mucLucSkt;
        public string MucLucSkt
        {
            get => _mucLucSkt;
            set => SetProperty(ref _mucLucSkt, value);
        }

        private string _sktKyHieu;
        public string SktKyHieu
        {
            get => _sktKyHieu;
            set => SetProperty(ref _sktKyHieu, value);
        }

        [JsonIgnore]
        public NsMlsktMlns SktMucLucMap { get; set; }

        public string XNM => StringUtils.Join(StringUtils.DIVISION, Lns, L, K, M, TM, TTM, NG, TNG, TNG1, TNG2, TNG3);

        private ObservableCollection<ComboboxItem> _nGItems;
        public ObservableCollection<ComboboxItem> NGItems
        {
            get => _nGItems;
            set => SetProperty(ref _nGItems, value);
        }

        private string _sDuToanChiTietToi;
        [DisplayName("Dự toán chi tiết tới")]
        [ColumnTypeAttribute(ColumnType.Combobox)]
        public string SDuToanChiTietToi
        {
            get => _sDuToanChiTietToi;
            set => SetProperty(ref _sDuToanChiTietToi, value);
        }

        private string _sQuyetToanChiTietToi;
        [DisplayName("Quyết toán chi tiết tới")]
        [ColumnType(ColumnType.Combobox)]
        public string SQuyetToanChiTietToi
        {
            get => _sQuyetToanChiTietToi;
            set => SetProperty(ref _sQuyetToanChiTietToi, value);
        }

        private string _sCPChiTietToi;
        [DisplayName("Cấp phát chi tiết tới")]
        [ColumnTypeAttribute(ColumnType.Combobox)]
        public string SCPChiTietToi
        {
            get => _sCPChiTietToi;
            set => SetProperty(ref _sCPChiTietToi, value);
        }

        private bool _isEnableDuToanNGCombobox;
        public bool IsEnableDuToanNGCombobox
        {
            get => _isEnableDuToanNGCombobox;
            set => SetProperty(ref _isEnableDuToanNGCombobox, value);
        }

        private bool _isEnableQuyetToanNGCombobox;
        public bool IsEnableQuyetToanNGCombobox
        {
            get => _isEnableQuyetToanNGCombobox;
            set => SetProperty(ref _isEnableQuyetToanNGCombobox, value);
        }


        private string _sMaCB;
        [DisplayName("Đối tượng")]
        [ColumnTypeAttribute(ColumnType.Combobox)]
        public string SMaCB
        {
            get => _sMaCB;
            set => SetProperty(ref _sMaCB, value);
        }


        public bool IsEnableMaCB => Lns == "1010000" && (BHangChaQuyetToan.HasValue && !BHangChaQuyetToan.Value);

        public bool IsEnableCheckData => !string.IsNullOrEmpty(NG) && string.IsNullOrEmpty(TNG);
        public bool? BHangChaDuToan { get; set; }
        private bool? _bHangChaQuyetToan;
        public bool? BHangChaQuyetToan
        {
            get => _bHangChaQuyetToan;
            set
            {
                SetProperty(ref _bHangChaQuyetToan, value);
                //OnPropertyChanged(nameof(IsEnableMaCB));
            }
        }
        public bool IsEditableCPChiTietToi { get; set; }
        public bool IsUsedDuToanChiTietToi { get; set; }
        public bool IsUsedQuyetToanChiTietToi { get; set; }

        private string _mlnsParentName;
        public string MlnsParentName
        {
            get => _mlnsParentName;
            set => SetProperty(ref _mlnsParentName, value);
        }

        public bool IsValidTM { get; set; }
        public bool IsValidTTM { get; set; }
        public bool IsValidNG { get; set; }
        public bool IsValidTNG { get; set; }
        public bool IsValidTNG1 { get; set; }
        public bool IsValidTNG2 { get; set; }
        public bool IsValidTNG3 { get; set; }
        public bool IsEditableStatus { get; set; }

        private bool _isParent;
        public bool IsParent
        {
            get => _isParent;
            set => SetProperty(ref _isParent, value);
        }

        //Thêmmmmmm

        private double _tuChi;
        public double TuChi
        {
            get => _tuChi;
            set
            {
                SetProperty(ref _tuChi, value);
                OnPropertyChanged(nameof(TuChi));
            }
        }

        //private bool _isEditTuChi;
        //public bool IsEditTuChi
        //{
        //    get => _isEditTuChi;
        //    set
        //    {
        //        SetProperty(ref _isEditTuChi, value);
        //        OnPropertyChanged(nameof(IsEditTuChi));
        //    }
        //}

    }

    public class NsMuclucNganSachChildModel : ModelBase
    {
        private Guid _mlnsId;
        public Guid MlnsId
        {
            get => _mlnsId;
            set => SetProperty(ref _mlnsId, value);
        }

        private Guid? _mlnsIdParent;
        public Guid? MlnsIdParent
        {
            get => _mlnsIdParent;
            set => SetProperty(ref _mlnsIdParent, value);
        }

        private string _lns;
        [DisplayName("LNS")]
        [DisplayDetailInfo("LNS")]
        public string Lns
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        [DisplayName("L")]
        [DisplayDetailInfo("L")]
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        [DisplayName("K")]
        [DisplayDetailInfo("K")]
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        [DisplayName("M")]
        [DisplayDetailInfo("M")]
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        [DisplayName("TM")]
        [DisplayDetailInfo("TM")]
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        [DisplayName("TTM")]
        [DisplayDetailInfo("TTM")]
        public string TTM
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        [DisplayName("NG")]
        [DisplayDetailInfo("NG")]
        public string NG
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private string _tng;
        [DisplayName("TNG")]
        [DisplayDetailInfo("TNG")]
        public string TNG
        {
            get => _tng;
            set => SetProperty(ref _tng, value);
        }

        private string _mota;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string MoTa
        {
            get => _mota;
            set => SetProperty(ref _mota, value);
        }

        private string _xauNoima;
        [DisplayDetailInfo("Xâu nối mã")]
        public string XauNoiMa
        {
            get => _xauNoima;
            set => SetProperty(ref _xauNoima, value);
        }

        public string LNSDisplay => String.Format("{0} - {1}", Lns, MoTa);

        public string XNMDisplay => String.Format("{0} - {1}", XauNoiMa, MoTa);


        [DisplayDetailInfo("Năm làm việc")]
        public int? NamLamViec { get; set; }

        public string XNM => StringUtils.Join(StringUtils.DIVISION, Lns, L, K, M, TM, TTM, NG, TNG);

        public bool? BHangChaDuToan { get; set; }

        private bool _bHangCha;
        public bool BHangCha
        {
            get => _bHangCha;
            set
            {
                SetProperty(ref _bHangCha, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }
        public override bool IsHangCha => BHangCha;

    }
}
