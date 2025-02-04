using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{

    [SheetAttribute(0, "Chứng từ chi tiết", 8, 0)]
    public class QuyetToanThuImportModel : BindableBase
    {
        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
        }
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }

        private bool _bHangcha;
        public bool BHangCha
        {
            get => _bHangcha;
            set => SetProperty(ref _bHangcha, value);
        }
        private string _sXauNoiMa;
        [ColumnAttribute("Xâu nối mã", 0)]
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }
        private string _sTenMLNS;
        [ColumnAttribute("Nội dung", 1)]
        public string STenMLNS
        {
            get => _sTenMLNS;
            set => SetProperty(ref _sTenMLNS, value);
        }
        private string _sQSBQNam;
        [ColumnAttribute("QSBQ", 2, ValidateType.IsNumber)]
        public string IQSBQNam
        {
            get => _sQSBQNam;
            set => SetProperty(ref _sQSBQNam, value);
        }
        
        private string _sLuongChinh;
        [ColumnAttribute("Lương chính", 3, ValidateType.IsNumber)]
        public string FLuongChinh
        {
            get => _sLuongChinh;
            set
            {
                SetProperty(ref _sLuongChinh, value);
            }
        }

        private string _fPhuCapChucVu;
        [ColumnAttribute("PC chức vụ", 4, ValidateType.IsNumber)]
        public string FPhuCapChucVu
        {
            get => _fPhuCapChucVu;
            set
            {
                SetProperty(ref _fPhuCapChucVu, value);
            }
        }

        private string _fPCTNNghe;
        [ColumnAttribute("PC TN nghề", 5, ValidateType.IsNumber)]
        public string FPCTNNghe
        {
            get => _fPCTNNghe;
            set
            {
                SetProperty(ref _fPCTNNghe, value);
            }
        }

        private string _fPCTNVuotKhung;
        [ColumnAttribute("PC TN VK", 6, ValidateType.IsNumber)]
        public string FPCTNVuotKhung
        {
            get => _fPCTNVuotKhung;
            set
            {
                SetProperty(ref _fPCTNVuotKhung, value);
            }
        }

        private string _fNghiOm;
        [ColumnAttribute("Nghỉ ốm ít hơn 14 ngày/tháng", 7, ValidateType.IsNumber)]
        public string FNghiOm
        {
            get => _fNghiOm;
            set
            {
                SetProperty(ref _fNghiOm, value);
            }
        }

        private string _fHSBL;
        [ColumnAttribute("HSBL", 8, ValidateType.IsNumber)]
        public string FHSBL
        {
            get => _fHSBL;
            set
            {
                SetProperty(ref _fHSBL, value);
            }
        }

        private string _fTongQuyTienLuongNam;
        [ColumnAttribute("Cộng", 9, ValidateType.IsNumber)]
        public double FTongQuyTienLuongNam
        {
            //get => _fTongQuyTienLuongNam;
            //set
            //{
            //    SetProperty(ref _fTongQuyTienLuongNam, value);
            //}
            get => Math.Round(NumberUtils.ConvertTextToDouble(FLuongChinh), MidpointRounding.AwayFromZero)
            + Math.Round(NumberUtils.ConvertTextToDouble(FPhuCapChucVu), MidpointRounding.AwayFromZero)
            + Math.Round(NumberUtils.ConvertTextToDouble(FPCTNNghe), MidpointRounding.AwayFromZero)
            + Math.Round(NumberUtils.ConvertTextToDouble(FPCTNVuotKhung), MidpointRounding.AwayFromZero)
            + Math.Round(NumberUtils.ConvertTextToDouble(FNghiOm), MidpointRounding.AwayFromZero)
            + Math.Round(NumberUtils.ConvertTextToDouble(FHSBL), MidpointRounding.AwayFromZero);
        }

        private string _fDuToan;
        //[ColumnAttribute("Dự toán", 10, ValidateType.IsNumber)]
        public string FDuToan
        {
            get => _fDuToan;
            set
            {
                SetProperty(ref _fDuToan, value);
            }
        }

        private string _fDaQuyetToan;
        //[ColumnAttribute("Đã quyết toán", 11, ValidateType.IsNumber)]
        public string FDaQuyetToan
        {
            get => _fDaQuyetToan;
            set
            {
                SetProperty(ref _fDaQuyetToan, value);
            }
        }

        private string _fConLai;
        //[ColumnAttribute("Còn lại", 12, ValidateType.IsNumber)]
        public string FConLai
        {
            get => _fConLai;
            set
            {
                SetProperty(ref _fConLai, value);
            }
        }

        private double _fThuBHXHNLD;
        //[ColumnAttribute("NLĐ đóng", 13, ValidateType.IsNumber)]
        public double FThuBHXHNLD
        {
            get
            {
                if (SXauNoiMa == BhxhMLNS.SI_QUAN_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_DU_TOAN || SXauNoiMa == BhxhMLNS.CC_CN_VCQP_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.LDHD_DU_TOAN || SXauNoiMa == BhxhMLNS.SI_QUAN_HACH_TOAN || SXauNoiMa == BhxhMLNS.QNCN_HACH_TOAN || SXauNoiMa == BhxhMLNS.CC_CN_VCQP_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.LDHD_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_PNPQ_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_PNPQ_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_TV_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_TV_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_TGTG_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_TGTG_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_PNPQ_HACH_TOAN || SXauNoiMa == BhxhMLNS.QNCN_PNPQ_HACH_TOAN)
                {
                    return _fThuBHXHNLD = FTongQuyTienLuongNam * FTyLeBHXHNLD.GetValueOrDefault();
                }
                else
                {
                    return _fThuBHXHNLD;
                }
            }
            set
            {
                SetProperty(ref _fThuBHXHNLD, value);
            }
        }

        private double _fThuBHXHNSD;
        //[ColumnAttribute("NSD đóng", 14, ValidateType.IsNumber)]
        public double FThuBHXHNSD
        {
            get
            {
                if (SXauNoiMa == BhxhMLNS.SI_QUAN_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_DU_TOAN || SXauNoiMa == BhxhMLNS.CC_CN_VCQP_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.LDHD_DU_TOAN || SXauNoiMa == BhxhMLNS.SI_QUAN_HACH_TOAN || SXauNoiMa == BhxhMLNS.QNCN_HACH_TOAN || SXauNoiMa == BhxhMLNS.CC_CN_VCQP_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.LDHD_HACH_TOAN || SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_PNPQ_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_PNPQ_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_TV_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_TV_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_TGTG_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_TGTG_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_PNPQ_HACH_TOAN || SXauNoiMa == BhxhMLNS.QNCN_PNPQ_HACH_TOAN)
                {
                    return _fThuBHXHNSD = FTongQuyTienLuongNam * FTyLeBHXHNSD.GetValueOrDefault();
                }
                else
                {
                    return _fThuBHXHNSD;
                }
            }
            set
            {
                SetProperty(ref _fThuBHXHNSD, value);
            }
        }
        private double _fTongSoPhaiThuBHXH;
        //[ColumnAttribute("Cộng", 15, ValidateType.IsNumber)]
        public double FTongSoPhaiThuBHXH
        {
            get
            {
                return FThuBHXHNLD + FThuBHXHNSD;
            }
            set
            {
                SetProperty(ref _fTongSoPhaiThuBHXH, value);
            }
        }

        private double _fThuBHYTNLD;
        //[ColumnAttribute("NLĐ đóng", 16, ValidateType.IsNumber)]
        public double FThuBHYTNLD
        {
            get
            {
                if (SXauNoiMa == BhxhMLNS.CC_CN_VCQP_DU_TOAN || SXauNoiMa == BhxhMLNS.LDHD_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.CC_CN_VCQP_HACH_TOAN || SXauNoiMa == BhxhMLNS.LDHD_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_PNPQ_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_PNPQ_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_TV_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_TV_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_TGTG_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_TGTG_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_PNPQ_HACH_TOAN || SXauNoiMa == BhxhMLNS.QNCN_PNPQ_HACH_TOAN)
                {
                    return FTongQuyTienLuongNam * FTyLeBHYTNLD.GetValueOrDefault();
                }
                else
                {
                    return _fThuBHYTNLD;
                }
            }
            set
            {
                SetProperty(ref _fThuBHYTNLD, value);
            }
        }

        private double _fThuBHYTNSD;
        //[ColumnAttribute("NSD đóng", 17, ValidateType.IsNumber)]
        public double FThuBHYTNSD
        {
            get
            {
                if (SXauNoiMa == BhxhMLNS.SI_QUAN_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_HACH_TOAN || SXauNoiMa == BhxhMLNS.QNCN_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.CC_CN_VCQP_DU_TOAN || SXauNoiMa == BhxhMLNS.LDHD_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.CC_CN_VCQP_HACH_TOAN || SXauNoiMa == BhxhMLNS.LDHD_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_PNPQ_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_PNPQ_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_TV_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_TV_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_TGTG_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_TGTG_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_PNPQ_HACH_TOAN || SXauNoiMa == BhxhMLNS.QNCN_PNPQ_HACH_TOAN)
                {
                    return FTongQuyTienLuongNam * FTyLeBHYTNSD.GetValueOrDefault();
                }
                else
                {
                    return _fThuBHYTNSD;
                }
            }
            set
            {
                SetProperty(ref _fThuBHYTNSD, value);
            }
        }

        private double _fTongSoPhaiThuBHYT;
        //[ColumnAttribute("Cộng", 18, ValidateType.IsNumber)]
        public double FTongSoPhaiThuBHYT
        {
            get
            {
                return FThuBHYTNLD + FThuBHYTNSD;
            }
            set
            {
                SetProperty(ref _fTongSoPhaiThuBHYT, value);
            }
        }

        private double _fThuBHTNNLD;
        //[ColumnAttribute("NLĐ đóng", 19, ValidateType.IsNumber)]
        public double FThuBHTNNLD
        {
            get
            {
                if (SXauNoiMa == BhxhMLNS.CC_CN_VCQP_DU_TOAN || SXauNoiMa == BhxhMLNS.LDHD_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.CC_CN_VCQP_HACH_TOAN || SXauNoiMa == BhxhMLNS.LDHD_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_PNPQ_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_PNPQ_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_TV_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_TV_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_TGTG_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_TGTG_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_PNPQ_HACH_TOAN || SXauNoiMa == BhxhMLNS.QNCN_PNPQ_HACH_TOAN)
                {
                    return FTongQuyTienLuongNam * FTyLeBHTNNLD.GetValueOrDefault();
                }

                else
                {
                    return _fThuBHTNNLD;
                }
            }
            set
            {
                SetProperty(ref _fThuBHTNNLD, value);
            }
        }
        private double _fThuBHTNNSD;
        //[ColumnAttribute("NSD đóng", 20, ValidateType.IsNumber)]
        public double FThuBHTNNSD
        {
            get
            {
                if (SXauNoiMa == BhxhMLNS.CC_CN_VCQP_DU_TOAN || SXauNoiMa == BhxhMLNS.LDHD_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.CC_CN_VCQP_HACH_TOAN || SXauNoiMa == BhxhMLNS.LDHD_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_PNPQ_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_PNPQ_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_TV_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_TV_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_TGTG_DU_TOAN || SXauNoiMa == BhxhMLNS.QNCN_TGTG_DU_TOAN
                    || SXauNoiMa == BhxhMLNS.SI_QUAN_PNPQ_HACH_TOAN || SXauNoiMa == BhxhMLNS.QNCN_PNPQ_HACH_TOAN)
                {
                    return FTongQuyTienLuongNam * FTyLeBHTNNSD.GetValueOrDefault();
                }
                else
                {
                    return _fThuBHTNNSD;
                }
            }
            set
            {
                SetProperty(ref _fThuBHTNNSD, value);
            }
        }

        private double _fTongSoPhaiThuBHTN;
        //[ColumnAttribute("Cộng", 21, ValidateType.IsNumber)]
        public double FTongSoPhaiThuBHTN
        {
            get
            {
                return FThuBHTNNLD + FThuBHTNNSD;
            }
            set
            {
                SetProperty(ref _fTongSoPhaiThuBHTN, value);
            }
        }

        private double _fTongCong;
        //[ColumnAttribute("Tổng số phải thu BHXH, BHYT, BHTN", 22, ValidateType.IsNumber)]
        public double FTongCong
        {
            get
            {
               return FTongSoPhaiThuBHXH + FTongSoPhaiThuBHYT + FTongSoPhaiThuBHTN;
            }
            set
            {
                SetProperty(ref _fTongCong, value);
            }
        }

        private string _sGhiChu;
        //[ColumnAttribute("Ghi chú", 23)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set
            {
                SetProperty(ref _sGhiChu, value);
            }
        }

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }

        public double? FTyLeBHXHNSD { get; set; }
        public double? FTyLeBHXHNLD { get; set; }
        public double? FTyLeBHYTNSD { get; set; }
        public double? FTyLeBHYTNLD { get; set; }
        public double? FTyLeBHTNNSD { get; set; }
        public double? FTyLeBHTNNLD { get; set; }

        public bool IsHasData => !string.IsNullOrEmpty(SXauNoiMa) || !string.IsNullOrEmpty(STenMLNS) ||
            IQSBQNam != "" || FLuongChinh != "" || FPhuCapChucVu != "" || FPCTNNghe != "" || FPCTNVuotKhung != "" || FNghiOm != "" || FHSBL != "" ||
            !NumberUtils.DoubleIsNullOrZero(FTongQuyTienLuongNam) || FDuToan != "" || FDaQuyetToan != "" || FConLai != "" || !NumberUtils.DoubleIsNullOrZero(FThuBHXHNLD) || !NumberUtils.DoubleIsNullOrZero(FThuBHXHNSD) || !NumberUtils.DoubleIsNullOrZero(FTongSoPhaiThuBHXH) ||
            !NumberUtils.DoubleIsNullOrZero(FThuBHYTNLD) || !NumberUtils.DoubleIsNullOrZero(FThuBHYTNSD) || !NumberUtils.DoubleIsNullOrZero(FTongSoPhaiThuBHYT) || !NumberUtils.DoubleIsNullOrZero(FThuBHTNNLD) || !NumberUtils.DoubleIsNullOrZero(FThuBHTNNSD) ||
            !NumberUtils.DoubleIsNullOrZero(FTongSoPhaiThuBHTN) || !NumberUtils.DoubleIsNullOrZero(FTongCong) || !string.IsNullOrEmpty(SGhiChu);
    }
}
