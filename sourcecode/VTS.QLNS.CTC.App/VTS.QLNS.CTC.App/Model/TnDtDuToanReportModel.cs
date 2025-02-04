using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class TnDtDuToanReportModel : DetailModelBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private Guid? _idChungTu;
        public Guid? IdChungTu
        {
            get => _idChungTu;
            set => SetProperty(ref _idChungTu, value);
        }
        private Guid? _mlnsId;
        public Guid? MlnsId
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
        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }
        private string _xauNoiMa;
        public string XauNoiMa
        {
            get => _xauNoiMa;
            set => SetProperty(ref _xauNoiMa, value);
        }

        private string _lns;
        public string Lns
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        public string Tm
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        public string Ttm
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }
        private string _ng;
        public string Ng
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private string _tng;
        public string Tng
        {
            get => _tng;
            set => SetProperty(ref _tng, value);
        }

        private string _tng1;
        public string Tng1
        {
            get => _tng1;
            set => SetProperty(ref _tng1, value);
        }

        private string _tng2;
        public string Tng2
        {
            get => _tng2;
            set => SetProperty(ref _tng2, value);
        }

        private string _tng3;
        public string Tng3
        {
            get => _tng3;
            set => SetProperty(ref _tng3, value);
        }
        public double _fThucThuNamTruoc;
        public double FThucThuNamTruoc
        {
            get => _fThucThuNamTruoc;
            set
            {
                SetProperty(ref _fThucThuNamTruoc, value);
            }
        }
        public double _fDuToan;
        public double FDuToan
        {
            get => _fDuToan;
            set
            {
                SetProperty(ref _fDuToan, value);
            }
        }
        public double _fBanThan;
        public double FBanThan
        {
            get => _fBanThan;
            set
            {
                SetProperty(ref _fBanThan, value);
            }
        }
        public double _fTuChi;
        public double FTuChi
        {
            get => _fTuChi;
            set
            {
                SetProperty(ref _fTuChi, value);
            }
        }
        private bool _bHangCha;
        public bool BHangCha
        {
            get => _bHangCha;
            set => SetProperty(ref _bHangCha, value);
        }
        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
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

        private int _iNamLamViec;
        public int INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }

        private int _iIdMaNguonNganSach;
        public int IIdMaNguonNganSach
        {
            get => _iIdMaNguonNganSach;
            set => SetProperty(ref _iIdMaNguonNganSach, value);
        }

        private int _iNamNganSach;
        public int INamNganSach
        {
            get => _iNamNganSach;
            set => SetProperty(ref _iNamNganSach, value);
        }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        public string ChiTietToi { get; set; }
        public DateTime? _dNgayTao;
        public DateTime? DNgayTao
        {
            get => _dNgaySua;
            set => SetProperty(ref _dNgaySua, value);
        }
        public string _sNguoiTao;
        public string SNguoiTao
        {
            get => _sNguoiTao;
            set => SetProperty(ref _sNguoiTao, value);
        }
        public DateTime? _dNgaySua;
        public DateTime? DNgaySua
        {
            get => _dNgaySua;
            set => SetProperty(ref _dNgaySua, value);
        }
        public string _sNguoiSua;
        public string SNguoiSua
        {
            get => _sNguoiSua;
            set => SetProperty(ref _sNguoiSua, value);
        }
        public override bool IsEditable => !BHangCha && !IsDeleted;
        public bool IsHasData => FDuToan != 0 || FBanThan != 0 || FThucThuNamTruoc != 0 || FTuChi != 0;

        public string Stt => string.IsNullOrEmpty(MoTa) ? string.Empty : MoTa.Contains(".") && MoTa.Split('.')[0].Length < 5 ? MoTa.Split('.')[0] : string.Empty;

        public List<TnDtdnChungTuChiTietDynamicModel> LstGiaTri { get; set; } = new List<TnDtdnChungTuChiTietDynamicModel>();

        public double FTongUocThucHien => !LstGiaTri.IsEmpty() && LstGiaTri.Any(x => x.FUocThucHienNamNay != 0) ? LstGiaTri.Sum(x => x.FUocThucHienNamNay) : 0;
        public double FTongDuToanNamKeHoach => !LstGiaTri.IsEmpty() && LstGiaTri.Any(x => x.FDuToanNamKeHoach != 0) ? LstGiaTri.Sum(x => x.FDuToanNamKeHoach) : 0;
        public bool IsDynamic => !LstGiaTri.IsEmpty() && LstGiaTri.Any(x => x.FVal != 0) || !NumberUtils.DoubleIsNullOrZero(FDuToan);

        public double FVal { get; set; }
        public string NameCol1 { get; set; }
        public string NameCol2 { get; set; }
        public bool IsThuNop { get; set; }
        public int? IPhanCap { get; set; }

        public bool IsTng => !string.IsNullOrEmpty(Tng) || !string.IsNullOrEmpty(Tng1) || !string.IsNullOrEmpty(Tng2) || !string.IsNullOrEmpty(Tng3);
    }

    public class TnDtDuToanReport2Model : DetailModelBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private Guid? _idChungTu;
        public Guid? IdChungTu
        {
            get => _idChungTu;
            set => SetProperty(ref _idChungTu, value);
        }
        private Guid? _mlnsId;
        public Guid? MlnsId
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
        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }
        private string _xauNoiMa;
        public string XauNoiMa
        {
            get => _xauNoiMa;
            set => SetProperty(ref _xauNoiMa, value);
        }

        private string _lns;
        public string Lns
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        public string Tm
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        public string Ttm
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }
        private string _ng;
        public string Ng
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private string _tng;
        public string Tng
        {
            get => _tng;
            set => SetProperty(ref _tng, value);
        }

        private string _tng1;
        public string Tng1
        {
            get => _tng1;
            set => SetProperty(ref _tng1, value);
        }

        private string _tng2;
        public string Tng2
        {
            get => _tng2;
            set => SetProperty(ref _tng2, value);
        }

        private string _tng3;
        public string Tng3
        {
            get => _tng3;
            set => SetProperty(ref _tng3, value);
        }
        public double _fThucThuNamTruoc;
        public double FThucThuNamTruoc
        {
            get => _fThucThuNamTruoc;
            set
            {
                SetProperty(ref _fThucThuNamTruoc, value);
            }
        }
        public double _fDuToan;
        public double FDuToan
        {
            get => _fDuToan;
            set
            {
                SetProperty(ref _fDuToan, value);
            }
        }
        public double _fBanThan;
        public double FBanThan
        {
            get => _fBanThan;
            set
            {
                SetProperty(ref _fBanThan, value);
            }
        }
        public double _fTuChi;
        public double FTuChi
        {
            get => _fTuChi;
            set
            {
                SetProperty(ref _fTuChi, value);
            }
        }
        private bool _bHangCha;
        public bool BHangCha
        {
            get => _bHangCha;
            set => SetProperty(ref _bHangCha, value);
        }
        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
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

        private int _iNamLamViec;
        public int INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }

        private int _iIdMaNguonNganSach;
        public int IIdMaNguonNganSach
        {
            get => _iIdMaNguonNganSach;
            set => SetProperty(ref _iIdMaNguonNganSach, value);
        }

        private int _iNamNganSach;
        public int INamNganSach
        {
            get => _iNamNganSach;
            set => SetProperty(ref _iNamNganSach, value);
        }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        public string ChiTietToi { get; set; }
        public DateTime? _dNgayTao;
        public DateTime? DNgayTao
        {
            get => _dNgaySua;
            set => SetProperty(ref _dNgaySua, value);
        }
        public string _sNguoiTao;
        public string SNguoiTao
        {
            get => _sNguoiTao;
            set => SetProperty(ref _sNguoiTao, value);
        }
        public DateTime? _dNgaySua;
        public DateTime? DNgaySua
        {
            get => _dNgaySua;
            set => SetProperty(ref _dNgaySua, value);
        }
        public string _sNguoiSua;
        public string SNguoiSua
        {
            get => _sNguoiSua;
            set => SetProperty(ref _sNguoiSua, value);
        }
        public override bool IsEditable => !BHangCha && !IsDeleted;
        public bool IsHasData => FDuToan != 0 || FBanThan != 0 || FThucThuNamTruoc != 0 || FTuChi != 0;

        public string Stt => string.IsNullOrEmpty(MoTa) ? string.Empty : MoTa.Contains(".") && MoTa.Split('.')[0].Length < 5 ? MoTa.Split('.')[0] : string.Empty;

        public List<TnDtdnChungTuChiTietDynamicModel> ListGiaTri { get; set; } = new List<TnDtdnChungTuChiTietDynamicModel>();

        public double FTongUocThucHien => !ListGiaTri.IsEmpty() && ListGiaTri.Any(x => x.FUocThucHienNamNay != 0) ? ListGiaTri.Sum(x => x.FUocThucHienNamNay) : 0;
        public double FTongDuToanNamKeHoach => !ListGiaTri.IsEmpty() && ListGiaTri.Any(x => x.FDuToanNamKeHoach != 0) ? ListGiaTri.Sum(x => x.FDuToanNamKeHoach) : 0;
        public bool IsDynamic => !ListGiaTri.IsEmpty() && ListGiaTri.Any(x => x.FVal != 0);

        public double FVal { get; set; }
        public string NameCol1 { get; set; }
        public string NameCol2 { get; set; }
        public bool IsThuNop { get; set; }
        public int? IPhanCap { get; set; }
        public bool IsTng => !string.IsNullOrEmpty(Tng) || !string.IsNullOrEmpty(Tng1) || !string.IsNullOrEmpty(Tng2) || !string.IsNullOrEmpty(Tng3);


    }

}
