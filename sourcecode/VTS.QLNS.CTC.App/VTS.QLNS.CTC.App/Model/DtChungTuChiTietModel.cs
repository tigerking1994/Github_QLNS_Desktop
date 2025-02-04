using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class DtChungTuChiTietModel : DetailModelBase
    {
        private Guid? _iIdMlns;
        public Guid? IIdMlns
        {
            get => _iIdMlns;
            set => SetProperty(ref _iIdMlns, value);
        }

        private Guid? _iIdMlnsCha;
        public Guid? IIdMlnsCha
        {
            get => _iIdMlnsCha;
            set => SetProperty(ref _iIdMlnsCha, value);
        }

        private string _sXauNoiMa;
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private Guid? _iIdDtchungTu;
        public Guid? IIdDtchungTu
        {
            get => _iIdDtchungTu;
            set => SetProperty(ref _iIdDtchungTu, value);
        }

        private string _sLns;
        [ColumnAttribute("LNS", 2, MLNSFiled.LNS)]
        public string SLns
        {
            get => _sLns;
            set => SetProperty(ref _sLns, value);
        }

        private string _sL;
        [ColumnAttribute("L", 3, MLNSFiled.L)]
        public string SL
        {
            get => _sL;
            set => SetProperty(ref _sL, value);
        }

        private string _sK;
        public string SK
        {
            get => _sK;
            set => SetProperty(ref _sK, value);
        }

        private string _sM;
        [ColumnAttribute("M", 4, MLNSFiled.M)]
        public string SM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }

        private string _sTm;
        [ColumnAttribute("TM", 5, MLNSFiled.TM)]
        public string STm
        {
            get => _sTm;
            set => SetProperty(ref _sTm, value);
        }

        private string _sTtm;
        [ColumnAttribute("TTM", 6, MLNSFiled.TTM)]
        public string STtm
        {
            get => _sTtm;
            set => SetProperty(ref _sTtm, value);
        }

        private string _sNg;
        [ColumnAttribute("NG", 7, MLNSFiled.NG)]
        public string SNg
        {
            get => _sNg;
            set => SetProperty(ref _sNg, value);
        }

        private string _sTng;
        [ColumnAttribute("TNG", 8, MLNSFiled.TNG)]
        public string STng
        {
            get => _sTng;
            set => SetProperty(ref _sTng, value);
        }

        private string _sTng1;
        [ColumnAttribute("TNG1", 9, MLNSFiled.TNG1)]
        public string STng1
        {
            get => _sTng1;
            set => SetProperty(ref _sTng1, value);
        }

        private string _sTng2;
        [ColumnAttribute("TNG2", 10, MLNSFiled.TNG2)]
        public string STng2
        {
            get => _sTng2;
            set => SetProperty(ref _sTng2, value);
        }

        private string _sTng3;
        [ColumnAttribute("TNG3", 11, MLNSFiled.TNG3)]
        public string STng3
        {
            get => _sTng3;
            set => SetProperty(ref _sTng3, value);
        }

        private string _sMota;
        public string SMoTa
        {
            get => _sMota;
            set => SetProperty(ref _sMota, value);
        }

        private double _fTonKho;
        [ColumnAttribute(EstimateColumn.FTONKHO, 21)]
        public double FTonKho
        {
            get => _fTonKho;
            set => SetProperty(ref _fTonKho, value);
        }

        private double _fTuChi;
        [ColumnAttribute(EstimateColumn.FTUCHI, 13)]
        public double FTuChi
        {
            get => _fTuChi;
            set   
            {
                SetProperty(ref _fTuChi, value);
                OnPropertyChanged(nameof(FCapBangTien));
            }
        }

        [ColumnAttribute(EstimateColumn.FRUTKBNN, 14)]
        private double _fRutKBNN;
        public double FRutKBNN
        {
            get => _fRutKBNN;
            set
            {
                SetProperty(ref _fRutKBNN, value);
                OnPropertyChanged(nameof(FCapBangTien));
            }
        }

        [ColumnAttribute(EstimateColumn.FCAPBANGTIEN, 15)]
        public double FCapBangTien => _fTuChi - _fRutKBNN;

        private double _fHienVat;
        [ColumnAttribute(EstimateColumn.FHIENVAT, 16)]
        public double FHienVat
        {
            get => _fHienVat;
            set => SetProperty(ref _fHienVat, value);
        }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private int? _iNamLamViec;
        public int? INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }

        private DateTime? _dNgayTao;
        public DateTime? DNgayTao
        {
            get => _dNgayTao;
            set => SetProperty(ref _dNgayTao, value);
        }

        private string _sNguoiTao;
        public string SNguoiTao
        {
            get => _sNguoiTao;
            set => SetProperty(ref _sNguoiTao, value);
        }

        private DateTime? _dNgaySua;
        public DateTime? DNgaySua
        {
            get => _dNgaySua;
            set => SetProperty(ref _dNgaySua, value);
        }

        private string _sNguoiSua;
        public string SNguoiSua
        {
            get => _sNguoiSua;
            set => SetProperty(ref _sNguoiSua, value);
        }

        private int? _iNamNganSach;
        public int? INamNganSach
        {
            get => _iNamNganSach;
            set => SetProperty(ref _iNamNganSach, value);
        }

        private int? _iIdMaNguonNganSach;
        public int? IIdMaNguonNganSach
        {
            get => _iIdMaNguonNganSach;
            set => SetProperty(ref _iIdMaNguonNganSach, value);
        }

        private string _iIdMaDonVi;
        public string IIdMaDonVi
        {
            get => _iIdMaDonVi;
            set => SetProperty(ref _iIdMaDonVi, value);
        }

        public string NoiDung => string.Format("{0} - {1}", IIdMaDonVi, STenDonVi);

        private string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private bool _isConLai;
        public bool IsConLai
        {
            get => _isConLai;
            set
            {
                if (SetProperty(ref _isConLai, value))
                {
                    OnPropertyChanged(nameof(IsEditable));
                }
            }
        }

        private bool _isPhanBo;
        public bool IsPhanBo
        {
            get => _isPhanBo;
            set
            {
                if (SetProperty(ref _isPhanBo, value))
                {
                    OnPropertyChanged(nameof(IsEditable));
                }
            }
        }

        private bool _isEnabledCbxDonVi;
        public bool IsEnabledCbxDonVi
        {
            get => _isEnabledCbxDonVi;
            set => SetProperty(ref _isEnabledCbxDonVi, value);
        }

        private bool _isEnabledCbxDotNhan;
        public bool IsEnabledCbxDotNhan
        {
            get => _isEnabledCbxDotNhan;
            set => SetProperty(ref _isEnabledCbxDotNhan, value);
        }

        public override bool IsEditable => !IsHangCha && !IsDeleted && !IsConLai && !IsPhanBo;

        private int _iPhanCap;
        public int IPhanCap
        {
            get => _iPhanCap;
            set => SetProperty(ref _iPhanCap, value);
        }

        private double _tuChiDaCap;
        public double TuChiDaCap
        {
            get => _tuChiDaCap;
            set => SetProperty(ref _tuChiDaCap, value);
        }

        private double _hienVatDaCap;
        public double HienVatDaCap
        {
            get => _hienVatDaCap;
            set => SetProperty(ref _hienVatDaCap, value);
        }

        public double TuChiConLai => FTuChi - TuChiDaCap;

        public double HienVatConLai => FHienVat - HienVatDaCap;

        private double _fHangNhap;
        [ColumnAttribute(EstimateColumn.FHANGNHAP, 17)]
        public double FHangNhap
        {
            get => _fHangNhap;
            set => SetProperty(ref _fHangNhap, value);
        }

        private double _fHangMua;
        [ColumnAttribute(EstimateColumn.FHANGMUA, 18)]
        public double FHangMua
        {
            get => _fHangMua;
            set => SetProperty(ref _fHangMua, value);
        }

        private double _fPhanCap;
        [ColumnAttribute(EstimateColumn.FPHANCAP, 19)]
        public double FPhanCap
        {
            get => _fPhanCap;
            set => SetProperty(ref _fPhanCap, value);
        }

        private double _fDuPhong;
        [ColumnAttribute(EstimateColumn.FDUPHONG, 20)]
        public double FDuPhong
        {
            get => _fDuPhong;
            set => SetProperty(ref _fDuPhong, value);
        }

        private Guid? _iIdCtduToanNhan;
        public Guid? IIdCtduToanNhan
        {
            get => _iIdCtduToanNhan;
            set => SetProperty(ref _iIdCtduToanNhan, value);
        }

        public string SoChungTuDotNhan { get; set; }

        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private string? _idDotPhanBoTruoc;
        public string? IdDotPhanBoTruoc
        {
            get => _idDotPhanBoTruoc;
            set => SetProperty(ref _idDotPhanBoTruoc, value);
        }
        public string SDotPhanBoTruoc { get; set; }

        private double _fTuChiTruocDieuChinh;
        public double FTuChiTruocDieuChinh
        {
            get => _fTuChiTruocDieuChinh;
            set => SetProperty(ref _fTuChiTruocDieuChinh, value);
        }

        private double _fTuChiSauDieuChinh;
        public double FTuChiSauDieuChinh
        {
            get => _fTuChiSauDieuChinh;
            set => SetProperty(ref _fTuChiSauDieuChinh, value);
        }

        private double _fHienVatTruocDieuChinh;
        public double FHienVatTruocDieuChinh
        {
            get => _fHienVatTruocDieuChinh;
            set => SetProperty(ref _fHienVatTruocDieuChinh, value);
        }

        private double _fHienVatSauDieuChinh;
        public double FHienVatSauDieuChinh
        {
            get => _fHienVatSauDieuChinh;
            set => SetProperty(ref _fHienVatSauDieuChinh, value);
        }

        private double _fHangNhapTruocDieuChinh;
        public double FHangNhapTruocDieuChinh
        {
            get => _fHangNhapTruocDieuChinh;
            set => SetProperty(ref _fHangNhapTruocDieuChinh, value);
        }

        private double _fHangNhapSauDieuChinh;
        public double FHangNhapSauDieuChinh
        {
            get => _fHangNhapSauDieuChinh;
            set => SetProperty(ref _fHangNhapSauDieuChinh, value);
        }

        private double _fHangMuaTruocDieuChinh;
        public double FHangMuaTruocDieuChinh
        {
            get => _fHangMuaTruocDieuChinh;
            set => SetProperty(ref _fHangMuaTruocDieuChinh, value);
        }

        private double _fHangMuaSauDieuChinh;
        public double FHangMuaSauDieuChinh
        {
            get => _fHangMuaSauDieuChinh;
            set => SetProperty(ref _fHangMuaSauDieuChinh, value);
        }

        private double _fPhanCapTruocDieuChinh;
        public double FPhanCapTruocDieuChinh
        {
            get => _fPhanCapTruocDieuChinh;
            set => SetProperty(ref _fPhanCapTruocDieuChinh, value);
        }
        private double _fPhanCapSauDieuChinh;
        public double FPhanCapSauDieuChinh
        {
            get => _fPhanCapSauDieuChinh;
            set => SetProperty(ref _fPhanCapSauDieuChinh, value);
        }

        private ObservableCollection<ComboboxItem> _cbxNhanPhanBos;
        public ObservableCollection<ComboboxItem> CbxNhanPhanBos
        {
            get => _cbxNhanPhanBos;
            set => SetProperty(ref _cbxNhanPhanBos, value);
        }

        private ObservableCollection<ComboboxItem> _cbxDonVi;
        public ObservableCollection<ComboboxItem> CbxDonVi
        {
            get => _cbxDonVi;
            set => SetProperty(ref _cbxDonVi, value);
        }

        private string _sChiTietToi;
        public string SChiTietToi
        {
            get => _sChiTietToi;
            set => SetProperty(ref _sChiTietToi, value);
        }

        private bool _isMapData;
        public bool IsMapData
        {
            get => _isMapData;
            set => SetProperty(ref _isMapData, value);
        }

        private int _iDuLieuNhan;
        public int IDuLieuNhan
        {
            get => _iDuLieuNhan;
            set => SetProperty(ref _iDuLieuNhan, value);
        }

        private bool _isEditTuChi;
        public bool IsEditTuChi
        {
            get => _isEditTuChi;
            set => SetProperty(ref _isEditTuChi, value);
        }

        private bool _isEditHienVat;
        public bool IsEditHienVat
        {
            get => _isEditHienVat;
            set => SetProperty(ref _isEditHienVat, value);
        }

        private bool _isEditHangNhap;
        public bool IsEditHangNhap
        {
            get => _isEditHangNhap;
            set => SetProperty(ref _isEditHangNhap, value);
        }

        private bool _isEditHangMua;
        public bool IsEditHangMua
        {
            get => _isEditHangMua;
            set => SetProperty(ref _isEditHangMua, value);
        }

        private bool _isEditDuPhong;
        public bool IsEditDuPhong
        {
            get => _isEditDuPhong;
            set => SetProperty(ref _isEditDuPhong, value);
        }

        private bool _isEditPhanCap;
        public bool IsEditPhanCap
        {
            get => _isEditPhanCap;
            set => SetProperty(ref _isEditPhanCap, value);
        }

        public double TongTuChiHienVat => TuChiConLai + HienVatConLai;

        public bool HasData => !IsHangCha && (IRowType == 3 || (IRowType <= 1 && FTuChi != 0 || FHienVat != 0 || FHangNhap != 0 || FHangMua != 0 || FPhanCap != 0 || FDuPhong != 0 ||
            FTuChiTruocDieuChinh != 0 || FTuChiSauDieuChinh != 0 || FHienVatTruocDieuChinh != 0 || FHienVatSauDieuChinh != 0 ||
            FHangNhapTruocDieuChinh != 0 || FHangNhapSauDieuChinh != 0 || FHangMuaTruocDieuChinh != 0 || FHangMuaSauDieuChinh != 0 ||
            FPhanCapTruocDieuChinh != 0 || FPhanCapSauDieuChinh != 0 || !string.IsNullOrEmpty(SGhiChu)));

        private int _iRowType;
        public int IRowType
        {
            get => _iRowType;
            set => SetProperty(ref _iRowType, value);
        }

        private bool _bEmpty;
        public bool BEmpty
        {
            get => _bEmpty;
            set => SetProperty(ref _bEmpty, value);
        }

        private bool _isShowGhiChu;
        public bool IsShowGhiChu
        {
            get => _isShowGhiChu;
            set => SetProperty(ref _isShowGhiChu, value);
        }

        public Guid? IID_DMCongKhai { get; set; }
        public Guid? IID_DMCongKhai_Cha { get; set; }
        public string SMa { get; set; }
        public double FVal => FVal1 + FVal2 + FVal3 + FVal4 + FVal5 + FVal6;
        public double FVal1 { get; set; }
        public double FVal2 { get; set; }
        public double FVal3 { get; set; }
        public double FVal4 { get; set; }
        public double FVal5 { get; set; }
        public double FVal6 { get; set; }
        public double FVal7 => FVal1 + FVal2 + FVal3;
        public double FVal8 => FVal4 + FVal5 + FVal6;
        public List<ReportDivisionCurrentBatchQuery> ListData { get; set; } = new List<ReportDivisionCurrentBatchQuery>();
        public double FTongTuChiHienVatExcel => ListData.Sum(s => s.FVal1);

        public bool BTuChi { get; set; }
        public bool BHangNhap { get; set; }
        public bool BhangMua { get; set; }

        public bool IsAgency { get; set; }
        public bool IsExpression => !IsAgency;
    }
}
