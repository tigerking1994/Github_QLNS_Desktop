using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 8, 0)]
    public class CapPhatTamUngBHYTImportExport : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        private string _stt;
        [ColumnAttribute("STT", 0, isRequired: true)]
        public string Stt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _lns;
        [ColumnAttribute("LNS", 1)]
        public string LNS
        {
            get => _lns;
            set
            {
                SetProperty(ref _lns, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _lk;
        public string LK
        {
            get => _lk;
            set
            {
                SetProperty(ref _lk, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _l;
        public string L
        {
            get => StringUtils.RemoveSpecialCharacters(LK).Split(StringUtils.DIVISION)[0];
            set
            {
                SetProperty(ref _l, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _k;
        public string K
        {
            get => StringUtils.RemoveSpecialCharacters(LK).Split(StringUtils.DIVISION).Length > 1 ? StringUtils.RemoveSpecialCharacters(LK).Split(StringUtils.DIVISION)[1] : string.Empty;
            set
            {
                SetProperty(ref _k, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _m;
        [ColumnAttribute("M", 3)]
        public string M
        {
            get => _m;
            set
            {
                SetProperty(ref _m, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _tm;
        public string TM
        {
            get => _tm;
            set
            {
                SetProperty(ref _tm, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _ttm;
        public string TTM
        {
            get => _ttm;
            set
            {
                SetProperty(ref _ttm, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _ng;
        public string NG
        {
            get => _ng;
            set
            {
                SetProperty(ref _ng, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _tng;
        public string TNG
        {
            get => _tng;
            set
            {
                SetProperty(ref _tng, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _tng1;
        [ColumnAttribute("TNG1", 9)]
        public string TNG1
        {
            get => _tng1;
            set
            {
                SetProperty(ref _tng1, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _tng2;
        [ColumnAttribute("TNG2", 10)]
        public string TNG2
        {
            get => _tng2;
            set
            {
                SetProperty(ref _tng2, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _tng3;
        [ColumnAttribute("TNG3", 11)]
        public string TNG3
        {
            get => _tng3;
            set
            {
                SetProperty(ref _tng3, value);
                OnPropertyChanged(nameof(XauNoiMa));
            }
        }

        private string _moTa;
        [ColumnAttribute("Nội dung", 2)]
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private string _sCoSoYTe;
        [ColumnAttribute("Tên cơ sở y tế", 5)]
        public string SCoSoYTe
        {
            get => _sCoSoYTe;
            set => SetProperty(ref _sCoSoYTe, value);
        }

        public string _iID_MaCoSoYTe;
        [ColumnAttribute("Mã cơ sở y tế", 4)]
        public string IID_MaCoSoYTe
        {
            get => _iID_MaCoSoYTe;
            set => SetProperty(ref _iID_MaCoSoYTe, value);
        }

        private Guid _iID_CoSoYTe;
        public Guid IID_CoSoYTe
        {
            get => _iID_CoSoYTe;
            set => SetProperty(ref _iID_CoSoYTe, value);
        }

        private string _fQTQuyTruoc;
        [ColumnAttribute("Quyết toán quý trước", 6, ValidateType.IsNumber)]
        public string FQTQuyTruoc
        {
            get => _fQTQuyTruoc;
            set => SetProperty(ref _fQTQuyTruoc, value);
        }

        private string _fCapThuaQuyTruocChuyenSang;
        [ColumnAttribute("Số đã cấp còn thừa quý trước chuyển sang", 7, ValidateType.IsNumber)]
        public string FCapThuaQuyTruocChuyenSang
        {
            get => _fCapThuaQuyTruocChuyenSang;
            set => SetProperty(ref _fCapThuaQuyTruocChuyenSang, value);
        }

        private string _fTamUngDenQuyNay;
        [ColumnAttribute("Dự kiến số cấp tạm ứng quý này", 8, ValidateType.IsNumber)]
        public string FTamUngDenQuyNay
        {
            get => _fTamUngDenQuyNay;
            set => SetProperty(ref _fTamUngDenQuyNay, value);
        }

        private string _fPhaiCapTamUngQuyNay;
        [ColumnAttribute("Số phải cấp tạm ứng quý này", 9, ValidateType.IsNumber)]
        public string FPhaiCapTamUngQuyNay
        {
            get => _fPhaiCapTamUngQuyNay;
            set => SetProperty(ref _fPhaiCapTamUngQuyNay, value);
        }

        private string _fLuyKeDenCuoiQuyNay;
        [ColumnAttribute("Lũy kế đến cuối quý này", 10, ValidateType.IsNumber)]
        public string FLuyKeDenCuoiQuyNay
        {
            get => _fLuyKeDenCuoiQuyNay;
            set => SetProperty(ref _fLuyKeDenCuoiQuyNay, value);
        }

        private string _xauNoiMa;
        [ColumnAttribute("Xâu nối mã", 1, ValidateType.IsXauNoiMaBH)]
        public string XauNoiMa
        {
            get => _xauNoiMa;
            set => SetProperty(ref _xauNoiMa, value);
        }

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }
    }
}
