using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VTS.QLNS.CTC.Utility.Enum;
using System.Net;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDmMucLucNganSachModel : DetailModelBase
    {
        public Guid Id { get; set; }
        private string _sXauNoima;
        [DisplayDetailInfo("Xâu nối mã")]
        public string SXauNoiMa
        {
            get => _sXauNoima;
            set => SetProperty(ref _sXauNoima, value);
        }
        private string _sLNS;
        [DisplayName("LNS")]
        [DisplayDetailInfo("LNS")]
        [Validate("LNS", Utility.Enum.DATA_TYPE.String, true)]
        public string SLNS
        {
            get => _sLNS;
            set => SetProperty(ref _sLNS, value);
        }

        private string _sL;
        [DisplayName("L")]
        [DisplayDetailInfo("L")]
        public string SL
        {
            get => _sL;
            set => SetProperty(ref _sL, value);
        }

        private string _sK;
        [DisplayName("K")]
        [DisplayDetailInfo("K")]
        public string SK
        {
            get => _sK;
            set => SetProperty(ref _sK, value);
        }

        private string _sM;
        [DisplayName("M")]
        [DisplayDetailInfo("M")]
        public string SM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }

        private string _sTM;
        [DisplayName("TM")]
        [DisplayDetailInfo("TM")]
        public string STM
        {
            get => _sTM;
            set => SetProperty(ref _sTM, value);
        }

        private string _STTM;
        [DisplayName("TTM")]
        [DisplayDetailInfo("TTM")]
        public string STTM
        {
            get => _STTM;
            set => SetProperty(ref _STTM, value);
        }

        private string _sNG;
        [DisplayName("NG")]
        [DisplayDetailInfo("NG")]
        public string SNG
        {
            get => _sNG;
            set
            {
                SetProperty(ref _sNG, value);
                OnPropertyChanged(nameof(IsEnableCheckData));
            }
        }

        private string _sTNG;
        [DisplayName("TNG")]
        [DisplayDetailInfo("TNG")]
        public string STNG
        {
            get => _sTNG;
            set
            {
                SetProperty(ref _sTNG, value);
                OnPropertyChanged(nameof(IsEnableCheckData));
            }
        }
        private string _sMoTa;
        [DisplayName("Mô tả")]
        [Validate("Mô tả", Utility.Enum.DATA_TYPE.String, true)]
        public string SMoTa
        {
            get => _sMoTa;
            set
            {
                SetProperty(ref _sMoTa, value);
            }
        }
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
        public bool IsHangCha
        {
            get => BHangCha;
        }
        private int? _iTrangThai;
        [DisplayName("Trạng thái")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAllTrangThaiMLNS")]
        public int? ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }
        public string XNMDisplay => String.Format("{0} - {1}", SXauNoiMa, SMoTa);
        public bool BDuPhong { get; set; }
        public bool? BHangChaDuToan { get; set; }
        public bool? BHangChaDuToanDieuChinh { get; set; }
        public bool? BHangChaQuyetToan { get; set; }
        public bool BHangMua { get; set; }
        public bool BHangNhap { get; set; }
        public bool BHienVat { get; set; }
        public bool BNgay { get; set; }
        public bool BPhanCap { get; set; }
        public bool BSoNguoi { get; set; }
        public bool BTonKho { get; set; }
        public bool BTuChi { get; set; }
        public string SChiTietToi { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string ILoai { get; set; }
        public bool? ILock { get; set; }
        public string IIDMaDonVi { get; set; }
        public string IIDMaBQuanLy { get; set; }
        public string Log { get; set; }
        private Guid _iIDMLNS;
        public Guid IIDMLNS
        {
            get => _iIDMLNS;
            set => SetProperty(ref _iIDMLNS, value);
        }
        public int? INamLamViec { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNhapTheoTruong { get; set; }
        public string Tag { get; set; }
        private string _sTNG1;
        [DisplayName("TNG1")]
        [DisplayDetailInfo("TNG1")]
        public string STNG1
        {
            get => _sTNG1;
            set => SetProperty(ref _sTNG1, value);
        }

        private string _sTNG2;
        [DisplayName("TNG2")]
        [DisplayDetailInfo("TNG2")]
        public string STNG2
        {
            get => _sTNG2;
            set => SetProperty(ref _sTNG2, value);
        }

        private string _sTNG3;
        [DisplayName("TNG3")]
        [DisplayDetailInfo("TNG3")]
        public string STNG3
        {
            get => _sTNG3;
            set => SetProperty(ref _sTNG3, value);
        }
        public int? ILoaiNganSach { get; set; }
        
        private string _iDonViTinh;
        [DisplayName("Đơn vị tính")]
        [ColumnTypeAttribute(ColumnType.Combobox)]
        public string IDonViTinh
        {
            get => _iDonViTinh;
            set => SetProperty(ref _iDonViTinh, value);
        }

        private string _sMaCB;
        [DisplayName("Đối tượng")]
        [ColumnTypeAttribute(ColumnType.Combobox)]
        public string SMaCB
        {
            get => _sMaCB;
            set => SetProperty(ref _sMaCB, value);
        }

        public string MergeRangeChild { get; set; }
        public int Rank { get; set; }
        public string SKhoiDonVi { get; set; }
        public string LNSDisplay => String.Format("{0} - {1}", SLNS, SMoTa);
        private Guid? _iIDMLNSCha;
        public Guid? IIDMLNSCha
        {
            get => _iIDMLNSCha;
            set => SetProperty(ref _iIDMLNSCha, value);
        }
        private string _mlnsParentName;
        public string MlnsParentName
        {
            get => _mlnsParentName;
            set => SetProperty(ref _mlnsParentName, value);
        }
        public bool IsHitTestVisible { get; set; } = true;
        public string XNM => StringUtils.Join(StringUtils.DIVISION, SLNS, SL, SK, SM, STM, STTM, SNG, STNG);
        public bool IsEnableCheckData => !string.IsNullOrEmpty(SNG) && string.IsNullOrEmpty(STNG);
        public bool IsEditableStatus { get; set; }
        public string SMaPhuCap { get; set; }
        public string STenPhuCap { get; set; }
        public string SMaCheDo { get; set; }
        public string STenCheDo { get; set; }
        private string _sDuToanChiTietToi;
        [DisplayName("Dự toán chi tiết tới")]
        [ColumnTypeAttribute(ColumnType.Combobox)]
        public string SDuToanChiTietToi
        {
            get => _sDuToanChiTietToi;
            set => SetProperty(ref _sDuToanChiTietToi, value);
        }

        private string _sDuToanDieuChinhChiTietToi;
        [DisplayName("Dự toán điều chỉnh chi tiết tới")]
        [ColumnTypeAttribute(ColumnType.Combobox)]
        public string SDuToanDieuChinhChiTietToi
        {
            get => _sDuToanDieuChinhChiTietToi;
            set => SetProperty(ref _sDuToanDieuChinhChiTietToi, value);
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

        private bool _isEnableDuToanDieuChinhNGCombobox;
        public bool IsEnableDuToanDieuChinhNGCombobox
        {
            get => _isEnableDuToanDieuChinhNGCombobox;
            set => SetProperty(ref _isEnableDuToanDieuChinhNGCombobox, value);
        }

        private bool _isEnableQuyetToanNGCombobox;
        public bool IsEnableQuyetToanNGCombobox
        {
            get => _isEnableQuyetToanNGCombobox;
            set => SetProperty(ref _isEnableQuyetToanNGCombobox, value);
        }
        public bool IsEditableCPChiTietToi { get; set; }
        public bool IsUsedDuToanChiTietToi { get; set; }
        public bool IsUsedQuyetToanChiTietToi { get; set; }
        public bool IsUsedDuToanDieuChinhChiTietToi { get; set; }

        private double? _fTyLeBHXHNSD;
        [DisplayName("BHXH (Hệ số NSD lao động đóng)")]
        public double? FTyLeBHXHNSD
        {
            get => _fTyLeBHXHNSD;
            set
            {
                SetProperty(ref _fTyLeBHXHNSD, value);
            }
        }
        private double? _fTyLeBHXHNLD;
        [DisplayName("BHXH (Hệ số NLĐ đóng)")]
        public double? FTyLeBHXHNLD
        {
            get => _fTyLeBHXHNLD;
            set
            {
                SetProperty(ref _fTyLeBHXHNLD, value);
            }
        }
        private double? _fTyLeBHYTNSD;
        [DisplayName("BHYT (Hệ số NSD lao động đóng)")]
        public double? FTyLeBHYTNSD
        {
            get => _fTyLeBHYTNSD;
            set
            {
                SetProperty(ref _fTyLeBHYTNSD, value);
            }
        }
        private double? _fTyLeBHYTNLD;
        [DisplayName("BHYT (Hệ số NLĐ đóng)")]
        public double? FTyLeBHYTNLD
        {
            get => _fTyLeBHYTNLD;
            set
            {
                SetProperty(ref _fTyLeBHYTNLD, value);
            }
        }
        private double? _fTyLeBHTNNSD;
        [DisplayName("BHTN (Hệ số NSD lao động đóng)")]
        public double? FTyLeBHTNNSD
        {
            get => _fTyLeBHTNNSD;
            set
            {
                SetProperty(ref _fTyLeBHTNNSD, value);
            }
        }
        private double? _fTyLeBHTNNLD;
        [DisplayName("BHTN (Hệ số NLĐ đóng)")]
        public double? FTyLeBHTNNLD
        {
            get => _fTyLeBHTNNLD;
            set
            {
                SetProperty(ref _fTyLeBHTNNLD, value);
            }
        }
        private double? _fHeSoLayQuyLuong;
        [DisplayName("Hệ số lấy quỹ lương")]
        public double? FHeSoLayQuyLuong
        {
            get => _fHeSoLayQuyLuong;
            set
            {
                SetProperty(ref _fHeSoLayQuyLuong, value);
            }
        }
    }
}
