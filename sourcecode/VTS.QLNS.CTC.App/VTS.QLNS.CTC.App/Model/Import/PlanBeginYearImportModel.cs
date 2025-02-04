using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 4, 0)]
    public class PlanBeginYearImportModel : BindableBase
    {
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
            set
            {
                SetProperty(ref _bHangcha, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        public bool IsHangCha
        {
            get => BHangCha;
        }

        public List<string> ListConcatenateCodeChild { get; set; }
        public string ConcatenateCodeParent { get; set; }

        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _lns;
        [ColumnAttribute("LNS", 0)]
        public string LNS
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        [ColumnAttribute("L", 1)]
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        [ColumnAttribute("K", 2)]
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        [ColumnAttribute("M", 3)]
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        [ColumnAttribute("TM", 4)]
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        [ColumnAttribute("TTM", 5)]
        public string TTM
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        [ColumnAttribute("NG", 6)]
        public string NG
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private string _tng;
        [ColumnAttribute("TNG", 7)]
        public string TNG
        {
            get => _tng;
            set => SetProperty(ref _tng, value);
        }

        private string _tng1;
        [ColumnAttribute("TNG1", 8)]
        public string TNG1
        {
            get => _tng1;
            set => SetProperty(ref _tng1, value);
        }

        private string _tng2;
        [ColumnAttribute("TNG2", 9)]
        public string TNG2
        {
            get => _tng2;
            set => SetProperty(ref _tng2, value);
        }

        private string _tng3;
        [ColumnAttribute("TNG3", 10)]
        public string TNG3
        {
            get => _tng3;
            set => SetProperty(ref _tng3, value);
        }

        private string _count;
        [ColumnAttribute("Count", 11)]
        public string Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        private string _mlnsId;
        [ColumnAttribute("MlnsId", 12)]
        public string MlnsId
        {
            get => _mlnsId;
            set => SetProperty(ref _mlnsId, value);
        }
        private string _mlnsIdParent;
        [ColumnAttribute("MlnsIdParent", 13)]
        public string MlnsIdParent
        {
            get => _mlnsIdParent;
            set => SetProperty(ref _mlnsIdParent, value);
        }


        private string _xauNoiMa;
        //[ColumnAttribute("Xâu nối mã", 11)]
        public string XauNoiMa
        {
            get => string.Join("-", new string[] { _lns, _l, _k, _m, _tm, _ttm, _ng, _tng, _tng1, _tng2, _tng3 }.Where(s => !string.IsNullOrEmpty(s)));
            set => SetProperty(ref _xauNoiMa, value);
        }

        private string _moTa;
        [ColumnAttribute("Mô tả", 14)]
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        //private string _duToan;
        //[ColumnAttribute("Dự toán", 12, ValidateType.IsNumber)]
        //public string DuToan
        //{
        //    get => _duToan;
        //    set => SetProperty(ref _duToan, value);
        //}

        //private string _quyetToan;
        //[ColumnAttribute("Quyết toán", 13, ValidateType.IsNumber)]
        //public string QuyetToan
        //{
        //    get => _quyetToan;
        //    set => SetProperty(ref _quyetToan, value);
        //}

        private string _fTuChi1;
        [ColumnAttribute("Tự chi 1", 15, ValidateType.IsNumber)]
        public string FTuChi1
        {
            get => _fTuChi1;
            set => SetProperty(ref _fTuChi1, value);
        }

        private string _fTuChi2;
        [ColumnAttribute("Tự chi 1", 16, ValidateType.IsNumber)]
        public string FTuChi2
        {
            get => _fTuChi2;
            set => SetProperty(ref _fTuChi2, value);
        }

        private string _fTuChi3;
        [ColumnAttribute("Tự chi 1", 17, ValidateType.IsNumber)]
        public string FTuChi3
        {
            get => _fTuChi3;
            set => SetProperty(ref _fTuChi3, value);
        }

        private string _fTuChi4;
        [ColumnAttribute("Tự chi 1", 18, ValidateType.IsNumber)]
        public string FTuChi4
        {
            get => _fTuChi4;
            set => SetProperty(ref _fTuChi4, value);
        }

        private string _fTuChi5;
        [ColumnAttribute("Tự chi 5", 19, ValidateType.IsNumber)]
        public string FTuChi5
        {
            get => _fTuChi5;
            set => SetProperty(ref _fTuChi5, value);
        }

        private string _uocThucHien;
        [ColumnAttribute("Ước T.Hiện", 20, ValidateType.IsNumber)]
        public string UocThucHien
        {
            get => _uocThucHien;
            set => SetProperty(ref _uocThucHien, value);
        }

        private string _chiTiet;
        [ColumnAttribute("Chi Tiết", 21, ValidateType.IsNumber)]
        public string ChiTiet
        {
            get => _chiTiet;
            set => SetProperty(ref _chiTiet, value);
        }

        private string _hangNhap;
        [ColumnAttribute("Hàng nhập", 22, ValidateType.IsNumber)]
        public string HangNhap
        {
            get => _hangNhap;
            set => SetProperty(ref _hangNhap, value);
        }

        private string _hangMua;
        [ColumnAttribute("Hàng mua", 23, ValidateType.IsNumber)]
        public string HangMua
        {
            get => _hangMua;
            set => SetProperty(ref _hangMua, value);
        }

        private string _phanCap;
        [ColumnAttribute("Phân cấp", 24, ValidateType.IsNumber)]
        public string PhanCap
        {
            get => _phanCap;
            set => SetProperty(ref _phanCap, value);
        }

        private string _chuaPhanCap;
        [ColumnAttribute("Hàng nhập", 25, ValidateType.IsNumber)]
        public string ChuaPhanCap
        {
            get => _chuaPhanCap;
            set => SetProperty(ref _chuaPhanCap, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        [ColumnAttribute(ValidateType.IsXauNoiMa)]
        public string ConcatenateCode
        {
            get => string.Join("-", new string[] { _lns, _l, _k, _m, _tm, _ttm, _ng, _tng, _tng1, _tng2, _tng3 }.Where(s => !string.IsNullOrEmpty(s)));
        }
    }

    public class DuToanDauNamDonViChungTu
    {
        public string IdDonVi { get; set; }
        public string LoaiChungTu { get; set; }
    }
}
