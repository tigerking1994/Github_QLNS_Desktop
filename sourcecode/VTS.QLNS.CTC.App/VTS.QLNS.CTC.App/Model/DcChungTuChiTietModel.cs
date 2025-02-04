using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class DcChungTuChiTietModel : DetailModelBase
    {
        private Guid _iIdMlns;
        public Guid IIdMlns
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

        private Guid? _iIdDcchungTu;
        public Guid? IIdDcchungTu
        {
            get => _iIdDcchungTu;
            set => SetProperty(ref _iIdDcchungTu, value);
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

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
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

        private int? _iNamLamViec;
        public int? INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }

        private string _iIdMaDonVi;
        public string IIdMaDonVi
        {
            get => _iIdMaDonVi;
            set => SetProperty(ref _iIdMaDonVi, value);
        }

        private string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private double _fDuToanNganSachNam;
        public double FDuToanNganSachNam
        {
            get => _fDuToanNganSachNam;
            set
            {
                SetProperty(ref _fDuToanNganSachNam, value);
                OnPropertyChanged(nameof(FDuToanConLai));
                OnPropertyChanged(nameof(FTang));
                OnPropertyChanged(nameof(FGiam));
            }
        }

        private double _fDuToanChuyenNamSau;
        public double FDuToanChuyenNamSau
        {
            get => _fDuToanChuyenNamSau;
            set
            {
                SetProperty(ref _fDuToanChuyenNamSau, value);
                OnPropertyChanged(nameof(FDuToanConLai));
                OnPropertyChanged(nameof(FTang));
                OnPropertyChanged(nameof(FGiam));
            }
        }

        public double FDuToanConLai => _fDuToanNganSachNam - _fDuToanChuyenNamSau;

        private double _fDuKienQtDauNam;
        public double FDuKienQtDauNam
        {
            get => _fDuKienQtDauNam;
            set
            {
                SetProperty(ref _fDuKienQtDauNam, value);
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FTang));
                OnPropertyChanged(nameof(FGiam));
            }
        }

        private double _fDuKienQtCuoiNam;
        public double FDuKienQtCuoiNam
        {
            get => _fDuKienQtCuoiNam;
            set
            {
                SetProperty(ref _fDuKienQtCuoiNam, value);
                OnPropertyChanged(nameof(FTongCong));
                OnPropertyChanged(nameof(FTang));
                OnPropertyChanged(nameof(FGiam));
            }
        }

        public double FTongCong => _fDuKienQtDauNam + _fDuKienQtCuoiNam;

        private double _fTang;
        public double FTang
        {
            get
            {
                if (_fTang > 0) return _fTang;
                else if ((FTongCong > FDuToanConLai) && !CongTangGiam) return FTongCong - FDuToanConLai;
                else return 0;
            }
            set
            {
                SetProperty(ref _fTang, value);
            }
        }

        private double _fGiam;

        public double FGiam
        {
            get
            {
                if (_fGiam > 0) return _fGiam;
                else if ((FDuToanConLai > FTongCong) && !CongTangGiam) return FDuToanConLai - FTongCong;
                else return 0;
            }
            set
            {
                SetProperty(ref _fGiam, value);
            }
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
        public bool CongTangGiam { get; set; } = false;
        public int ILoaiDuKien { get; set; }
        public bool HasData => !IsHangCha && (FDuKienQtDauNam != 0 || FDuKienQtDauNam != 0 || FDuToanChuyenNamSau != 0 || !string.IsNullOrEmpty(SGhiChu));
    }
}
