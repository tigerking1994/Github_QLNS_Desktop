using System.Linq;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Thông tin chứng từ", 8, 0)]

    public class TnDtdnChungTuChiTietImportModel : BindableBase
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

        private string _mlnsId;
        //[ColumnAttribute("MLNSId", 0)]
        public string MlnsId
        {
            get => _mlnsId;
            set => SetProperty(ref _mlnsId, value);
        }

        private string _mlnsParentId;
        //[ColumnAttribute("MLNSIdParent", 1)]
        public string MlnsIdParent
        {
            get => _mlnsParentId;
            set => SetProperty(ref _mlnsParentId, value);
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
        public string Tm
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        [ColumnAttribute("TTM", 5)]
        public string Ttm
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        [ColumnAttribute("NG", 6)]
        public string Ng
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private string _tng;
        [ColumnAttribute("TNG", 7)]
        public string Tng
        {
            get => _tng;
            set => SetProperty(ref _tng, value);
        }

        private string _tng1;
        [ColumnAttribute("TNG1", 8)]
        public string Tng1
        {
            get => _tng1;
            set => SetProperty(ref _tng1, value);
        }
        private string _tng2;
        [ColumnAttribute("TNG2", 9)]
        public string Tng2
        {
            get => _tng2;
            set => SetProperty(ref _tng2, value);
        }
        private string _tng3;
        [ColumnAttribute("TNG3", 10)]
        public string Tng3
        {
            get => _tng3;
            set => SetProperty(ref _tng3, value);
        }

        private string _moTa;
        [ColumnAttribute("Mô tả", 11)]
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        public string _fThucThuNamTruoc;
        [ColumnAttribute("FThucThuNamTruoc", 12)]

        public string FThucThuNamTruoc
        {
            get => _fThucThuNamTruoc;
            set
            {
                SetProperty(ref _fThucThuNamTruoc, value);
            }
        }
        public string _fDuToanNamNay;
        [ColumnAttribute("FDuToanNamNay", 13)]

        public string FDuToanNamNay
        {
            get => _fDuToanNamNay;
            set
            {
                SetProperty(ref _fDuToanNamNay, value);
            }
        }
        public string _fUocThucHienNamNay;
        [ColumnAttribute("FUocThucHienNamNay", 14)]

        public string FUocThucHienNamNay
        {
            get => _fUocThucHienNamNay;
            set
            {
                SetProperty(ref _fUocThucHienNamNay, value);
            }
        }
        public string _fDuToanNamKeHoach;
        [ColumnAttribute("FDuToanNamKeHoach", 15)]

        public string FDuToanNamKeHoach
        {
            get => _fDuToanNamKeHoach;
            set
            {
                SetProperty(ref _fDuToanNamKeHoach, value);
            }
        }
        private string _sHangCha;
        [ColumnAttribute("BHangCha", 16)]

        public string SHangCha
        {
            get => _sHangCha;
            set => SetProperty(ref _sHangCha, value);
        }

        public bool BHangCha => !string.IsNullOrEmpty(SHangCha) && SHangCha.Equals("TRUE");
        public bool IsHangCha => !string.IsNullOrEmpty(SHangCha) && SHangCha.Equals("TRUE");

        [ColumnAttribute(ValidateType.IsXauNoiMa)]
        public string ConcatenateCode
        {
            get => string.Join("-", new string[] { _lns, _l, _k, _m, _tm, _ttm, _ng, _tng }.Where(s => !string.IsNullOrEmpty(s)));
        }

        public string XauNoiMa => ConcatenateCode;

        private string _note;
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }
    }
}
