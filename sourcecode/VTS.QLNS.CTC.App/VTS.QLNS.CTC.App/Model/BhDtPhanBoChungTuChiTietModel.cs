using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDtPhanBoChungTuChiTietModel : DetailModelBase
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

        public Guid Id { get; set; }

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

        private double? _fBHXHNLD;
        [ColumnAttribute("FBHXHNLD", 13)]
        public double? FBHXHNLD
        {
            get => _fBHXHNLD;
            set
            {
                SetProperty(ref _fBHXHNLD, value);
                OnPropertyChanged(nameof(FThuBHXH));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHXHNSD;
        [ColumnAttribute("FBHXHNSD", 14)]
        public double? FBHXHNSD
        {
            get => _fBHXHNSD;
            set
            {
                SetProperty(ref _fBHXHNSD, value);
                OnPropertyChanged(nameof(FThuBHXH));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        public double? FThuBHXH
        {
            get => (_fBHXHNLD ?? 0) + (_fBHXHNSD ?? 0);
            set
            {
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHYTNLD;
        [ColumnAttribute("FBHYTNLD", 16)]
        public double? FBHYTNLD
        {
            get => _fBHYTNLD;
            set
            {
                SetProperty(ref _fBHYTNLD, value);
                OnPropertyChanged(nameof(FThuBHYT));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHYTNSD;
        [ColumnAttribute("FBHYTNSD", 17)]
        public double? FBHYTNSD
        {
            get => _fBHYTNSD;
            set
            {
                SetProperty(ref _fBHYTNSD, value);
                OnPropertyChanged(nameof(FThuBHYT));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        public double? FThuBHYT
        {
            get => (_fBHYTNLD ?? 0) + (_fBHYTNSD ?? 0);
            set
            {
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHTNNLD;
        [ColumnAttribute("FBHTNNLD", 19)]
        public double? FBHTNNLD
        {
            get => _fBHTNNLD;
            set
            {
                SetProperty(ref _fBHTNNLD, value);
                OnPropertyChanged(nameof(FThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHTNNSD;
        [ColumnAttribute("FBHTNNSD", 20)]
        public double? FBHTNNSD
        {
            get => _fBHTNNSD;
            set
            {
                SetProperty(ref _fBHTNNSD, value);
                OnPropertyChanged(nameof(FThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        public double? FThuBHTN
        {
            get => (_fBHTNNLD ?? 0) + (_fBHTNNSD ?? 0);
            set
            {
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        public double? FTongCong
        {
            get => (FThuBHXH ?? 0) + (FThuBHYT ?? 0) + (FThuBHTN ?? 0);
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

        private Guid _iIdCtduToanNhan;
        public Guid IIDCTDuToanNhan
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

        private string _idDotPhanBoTruoc;
        public string IdDotPhanBoTruoc
        {
            get => _idDotPhanBoTruoc;
            set => SetProperty(ref _idDotPhanBoTruoc, value);
        }
        public string SDotPhanBoTruoc { get; set; }

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
        public bool HasDataChild => FBHXHNLD.GetValueOrDefault() != 0 || FBHXHNSD.GetValueOrDefault() != 0 || FBHYTNSD.GetValueOrDefault() != 0 || FBHYTNLD.GetValueOrDefault() != 0 || FBHTNNLD.GetValueOrDefault() != 0 || FBHTNNSD.GetValueOrDefault() != 0;
        public bool HasData => !IsHangCha && (FBHXHNLD.GetValueOrDefault() != 0 || FBHXHNSD.GetValueOrDefault() != 0 || FBHYTNSD.GetValueOrDefault() != 0 || FBHYTNLD.GetValueOrDefault() != 0 || FBHTNNLD.GetValueOrDefault() != 0 || FBHTNNSD.GetValueOrDefault() != 0 || !string.IsNullOrEmpty(SGhiChu));
        public bool HasNotData => FBHXHNLD.GetValueOrDefault() == 0 || FBHXHNSD.GetValueOrDefault() == 0 || FBHYTNSD.GetValueOrDefault() == 0 || FBHYTNLD.GetValueOrDefault() == 0 || FBHTNNLD.GetValueOrDefault() == 0 || FBHTNNSD.GetValueOrDefault() == 0 || !string.IsNullOrEmpty(SGhiChu);

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

        private bool _isEditBhxhNld;
        public bool IsEditBhxhNld
        {
            get => _isEditBhxhNld;
            set => SetProperty(ref _isEditBhxhNld, value);
        }
        private bool _isEditBhxhNsd;
        public bool IsEditBhxhNsd
        {
            get => _isEditBhxhNsd;
            set => SetProperty(ref _isEditBhxhNsd, value);
        }
        private bool _isEditBhytNld;
        public bool IsEditBhytNld
        {
            get => _isEditBhytNld;
            set => SetProperty(ref _isEditBhytNld, value);
        }
        private bool _isEditBhytNsd;
        public bool IsEditBhytNsd
        {
            get => _isEditBhytNsd;
            set => SetProperty(ref _isEditBhytNsd, value);
        }
        private bool _isEditBhtnNld;
        public bool IsEditBhtnNld
        {
            get => _isEditBhtnNld;
            set => SetProperty(ref _isEditBhtnNld, value);
        }
        private bool _isEditBhtnNsd;
        public bool IsEditBhtnNsd
        {
            get => _isEditBhtnNsd;
            set => SetProperty(ref _isEditBhtnNsd, value);
        }
        public bool BHangCha { get; set; }
        public bool IsHangCha => BHangCha;

        private double? _fBHXHNLDTruocDieuChinh;
        public double? FBHXHNLDTruocDieuChinh
        {
            get => _fBHXHNLDTruocDieuChinh;
            set
            {
                SetProperty(ref _fBHXHNLDTruocDieuChinh, value);
                OnPropertyChanged(nameof(FThuBHXHTruocDieuChinh));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHXHNSDTruocDieuChinh;
        public double? FBHXHNSDTruocDieuChinh
        {
            get => _fBHXHNSDTruocDieuChinh;
            set
            {
                SetProperty(ref _fBHXHNSDTruocDieuChinh, value);
                OnPropertyChanged(nameof(FThuBHXHTruocDieuChinh));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        public double? FThuBHXHTruocDieuChinh
        {
            get => (_fBHXHNLDTruocDieuChinh ?? 0) + (_fBHXHNSDTruocDieuChinh ?? 0);
            set
            {
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHXHNLDSauDieuChinh;
        public double? FBHXHNLDSauDieuChinh
        {
            get => _fBHXHNLDSauDieuChinh;
            set
            {
                SetProperty(ref _fBHXHNLDSauDieuChinh, value);
                OnPropertyChanged(nameof(FThuBHXHSauDieuChinh));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHXHNSDSauDieuChinh;
        public double? FBHXHNSDSauDieuChinh
        {
            get => _fBHXHNSDSauDieuChinh;
            set
            {
                SetProperty(ref _fBHXHNSDSauDieuChinh, value);
                OnPropertyChanged(nameof(FThuBHXHSauDieuChinh));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        public double? FThuBHXHSauDieuChinh
        {
            get => (_fBHXHNLDSauDieuChinh ?? 0) + (_fBHXHNSDSauDieuChinh?? 0);
            set
            {
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHYTNLDSauDieuChinh;
        public double? FBHYTNLDSauDieuChinh
        {
            get => _fBHYTNLDSauDieuChinh;
            set
            {
                SetProperty(ref _fBHYTNLDSauDieuChinh, value);
                OnPropertyChanged(nameof(FThuBHYTSauDieuChinh));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHYTNSDSauDieuChinh;
        public double? FBHYTNSDSauDieuChinh
        {
            get => _fBHYTNSDSauDieuChinh;
            set
            {
                SetProperty(ref _fBHYTNSDSauDieuChinh, value);
                OnPropertyChanged(nameof(FThuBHYTSauDieuChinh));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        public double? FThuBHYTSauDieuChinh
        {
            get => (_fBHYTNLDSauDieuChinh ?? 0) + (_fBHYTNSDSauDieuChinh ?? 0);
            set
            {
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHYTNLDTruocDieuChinh;
        public double? FBHYTNLDTruocDieuChinh
        {
            get => _fBHYTNLDTruocDieuChinh;
            set
            {
                SetProperty(ref _fBHYTNLDTruocDieuChinh, value);
                OnPropertyChanged(nameof(FThuBHYTTruocDieuChinh));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHYTNSDTruocDieuChinh;
        public double? FBHYTNSDTruocDieuChinh
        {
            get => _fBHYTNSDTruocDieuChinh;
            set
            {
                SetProperty(ref _fBHYTNSDTruocDieuChinh, value);
                OnPropertyChanged(nameof(FThuBHYTTruocDieuChinh));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        public double? FThuBHYTTruocDieuChinh
        {
            get => (_fBHYTNLDTruocDieuChinh ?? 0) + (_fBHYTNSDTruocDieuChinh ?? 0);
            set
            {
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHTNNLDTruocDieuChinh;
        public double? FBHTNNLDTruocDieuChinh
        {
            get => _fBHTNNLDTruocDieuChinh;
            set
            {
                SetProperty(ref _fBHTNNLDTruocDieuChinh, value);
                OnPropertyChanged(nameof(FThuBHTNTruocDieuChinh));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHTNNSDTruocDieuChinh;
        public double? FBHTNNSDTruocDieuChinh
        {
            get => _fBHTNNSDTruocDieuChinh;
            set
            {
                SetProperty(ref _fBHTNNSDTruocDieuChinh, value);
                OnPropertyChanged(nameof(FThuBHTNTruocDieuChinh));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        public double? FThuBHTNTruocDieuChinh
        {
            get => (_fBHTNNLDTruocDieuChinh ?? 0) + (_fBHTNNSDTruocDieuChinh ?? 0);
            set
            {
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHTNNLDSauDieuChinh;
        public double? FBHTNNLDSauDieuChinh
        {
            get => _fBHTNNLDSauDieuChinh;
            set
            {
                SetProperty(ref _fBHTNNLDSauDieuChinh, value);
                OnPropertyChanged(nameof(FThuBHTNSauDieuChinh));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        private double? _fBHTNNSDSauDieuChinh;
        public double? FBHTNNSDSauDieuChinh
        {
            get => _fBHTNNSDSauDieuChinh;
            set
            {
                SetProperty(ref _fBHTNNSDSauDieuChinh, value);
                OnPropertyChanged(nameof(FThuBHTNSauDieuChinh));
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        public double? FThuBHTNSauDieuChinh
        {
            get => (_fBHTNNLDSauDieuChinh ?? 0) + (_fBHTNNSDSauDieuChinh ?? 0);
            set
            {
                OnPropertyChanged(nameof(FTongCong));
            }
        }

        public bool HasAllocationData => FBHXHNLDTruocDieuChinh.GetValueOrDefault() != 0 || FBHXHNSDTruocDieuChinh.GetValueOrDefault() != 0 ||
            FBHYTNLDTruocDieuChinh.GetValueOrDefault() != 0 || FBHYTNSDTruocDieuChinh.GetValueOrDefault() != 0 ||
            FBHTNNLDTruocDieuChinh.GetValueOrDefault() != 0 || FBHTNNSDTruocDieuChinh.GetValueOrDefault() != 0 ||
            FBHXHNLD.GetValueOrDefault() != 0 || FBHXHNSD.GetValueOrDefault() != 0 ||
            FBHYTNLD.GetValueOrDefault() != 0 || FBHYTNSD.GetValueOrDefault() != 0 ||
            FBHTNNLD.GetValueOrDefault() != 0 || FBHTNNSD.GetValueOrDefault() != 0 ||
            FBHXHNLDSauDieuChinh.GetValueOrDefault() != 0 || FBHXHNSDSauDieuChinh.GetValueOrDefault() != 0 ||
            FBHYTNLDSauDieuChinh.GetValueOrDefault() != 0 || FBHYTNSDSauDieuChinh.GetValueOrDefault() != 0 ||
            FBHTNNLDSauDieuChinh.GetValueOrDefault() != 0 || FBHTNNSDSauDieuChinh.GetValueOrDefault() != 0 ||
            IRowType == (int)BaoHiemDuToanTypeEnum.Type.SO_CHUA_PHAN_BO;

        public bool IsEmptyPlanData => FBHXHNLD.GetValueOrDefault() == 0 && FBHXHNSD.GetValueOrDefault() == 0
            && FBHYTNLD.GetValueOrDefault() == 0 && FBHYTNSD.GetValueOrDefault() == 0
            && FBHTNNLD.GetValueOrDefault() == 0 && FBHTNNSD.GetValueOrDefault() == 0;

        
        private bool _isRemainRow;
        public bool IsRemainRow
        {
            get => _isRemainRow;
            set
            {
                SetProperty(ref _isRemainRow, value);
            }
        }
    }
}
