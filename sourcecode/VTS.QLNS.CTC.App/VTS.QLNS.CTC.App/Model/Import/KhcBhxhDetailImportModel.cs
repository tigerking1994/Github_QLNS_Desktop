using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 11, 0)]
    public class KhcBhxhDetailImportModel : BaseImportModel
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

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }

        private Guid _idMLNS;
        public Guid IdMLNS
        {
            get => _idMLNS;
            set => SetProperty(ref _idMLNS, value);
        }

        private string _sXauNoiMa;
        [ColumnAttribute("Xâu nối mã", 0, ValidateType.IsXauNoiMaBH)]

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

        //private string _sSoDaThucHienNamTruoc;
        //[ColumnAttribute("Số người (ngày)", 2)]
        //public string SSoDaThucHienNamTruoc
        //{
        //    get => _sSoDaThucHienNamTruoc;
        //    set => SetProperty(ref _sSoDaThucHienNamTruoc, value);
        //}
        //private string _sTienDaThucHienNamTruoc;
        //[ColumnAttribute("Số tiền", 3)]
        //public string STienDaThucHienNamTruoc
        //{
        //    get => _sTienDaThucHienNamTruoc;
        //    set => SetProperty(ref _sTienDaThucHienNamTruoc, value);
        //}

        private string _sSoUocThucHienNamTruoc;
        [ColumnAttribute("Số người (ngày)", 2)]
        public string SSoUocThucHienNamTruoc
        {
            get => _sSoUocThucHienNamTruoc;
            set => SetProperty(ref _sSoUocThucHienNamTruoc, value);
        }

        private string _sTienUocThucHienNamTruoc;
        [ColumnAttribute("Số tiền", 3)]
        public string STienUocThucHienNamTruoc
        {
            get => _sTienUocThucHienNamTruoc;
            set => SetProperty(ref _sTienUocThucHienNamTruoc, value);
        }

        private string _sSoKeHoachThucHienNamNay;
        [ColumnAttribute("Số người (ngày)", 4)]
        public string SSoKeHoachThucHienNamNay
        {
            get => _sSoKeHoachThucHienNamNay;
            set => SetProperty(ref _sSoKeHoachThucHienNamNay, value);
        }

        private string _sTienKeHoachThucHienNamNay;
        [ColumnAttribute("Số tiền", 5)]
        public string STienKeHoachThucHienNamNay
        {
            get => _sTienKeHoachThucHienNamNay;
            set => SetProperty(ref _sTienKeHoachThucHienNamNay, value);
        }

        private string _sSoSQ;
        [ColumnAttribute("Số người (ngày)", 6)]
        public string SSoSQ
        {
            get => _sSoSQ;
            set => SetProperty(ref _sSoSQ, value);
        }

        private string _sTienSQ;
        [ColumnAttribute("Số tiền", 7)]
        public string STienSQ
        {
            get => _sTienSQ;
            set => SetProperty(ref _sTienSQ, value);
        }

        private string _sSoQNCN;
        [ColumnAttribute("Số người (ngày)", 8)]
        public string SSoQNCN
        {
            get => _sSoQNCN;
            set => SetProperty(ref _sSoQNCN, value);
        }

        private string _sTienQNCN;
        [ColumnAttribute("Số tiền", 9)]
        public string STienQNCN
        {
            get => _sTienQNCN;
            set => SetProperty(ref _sTienQNCN, value);
        }

        private string _sSoCNVQP;
        [ColumnAttribute("Số người (ngày)", 10)]
        public string SSoCNVQP
        {
            get => _sSoCNVQP;
            set => SetProperty(ref _sSoCNVQP, value);
        }

        private string _sTienCNVQP;
        [ColumnAttribute("Số tiền", 11)]
        public string STienCNVQP
        {
            get => _sTienCNVQP;
            set => SetProperty(ref _sTienCNVQP, value);
        }


        private string _sSoLDHD;
        [ColumnAttribute("Số người (ngày)", 12)]
        public string SSoLDHD
        {
            get => _sSoLDHD;
            set => SetProperty(ref _sSoLDHD, value);
        }

        private string _sTienLDHD;
        [ColumnAttribute("Số tiền", 13)]
        public string STienLDHD
        {
            get => _sTienLDHD;
            set => SetProperty(ref _sTienLDHD, value);
        }

        private string _sSoHSQBS;
        [ColumnAttribute("Số người (ngày)", 14)]
        public string SSoHSQBS
        {
            get => _sSoHSQBS;
            set => SetProperty(ref _sSoHSQBS, value);
        }

        private string _sTienHSQBS;
        [ColumnAttribute("Số tiền", 15)]
        public string STienHSQBS
        {
            get => _sTienHSQBS;
            set => SetProperty(ref _sTienHSQBS, value);
        }

        private string _sGhiChu;
        [ColumnAttribute("Số tiền", 16)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        public bool IsDataNotNull => !string.IsNullOrWhiteSpace(SSoUocThucHienNamTruoc) || !string.IsNullOrWhiteSpace(STienUocThucHienNamTruoc)
            || !string.IsNullOrWhiteSpace(SSoKeHoachThucHienNamNay) || !string.IsNullOrWhiteSpace(STienKeHoachThucHienNamNay)
            || !string.IsNullOrWhiteSpace(SSoSQ) || !string.IsNullOrWhiteSpace(STienSQ)
            || !string.IsNullOrWhiteSpace(SSoQNCN) || !string.IsNullOrWhiteSpace(STienQNCN)
            || !string.IsNullOrWhiteSpace(SSoCNVQP) || !string.IsNullOrWhiteSpace(STienCNVQP)
            || !string.IsNullOrWhiteSpace(SSoLDHD) || !string.IsNullOrWhiteSpace(STienLDHD)
            || !string.IsNullOrWhiteSpace(SSoHSQBS) || !string.IsNullOrWhiteSpace(STienHSQBS);

        public int? ISoUocThucHienNamTruoc => !string.IsNullOrWhiteSpace(SSoUocThucHienNamTruoc.Replace(".", string.Empty)) ? Convert.ToInt32(SSoUocThucHienNamTruoc.Replace(".", string.Empty)) : 0;
        public double? FTienUocThucHienNamTruoc => !string.IsNullOrWhiteSpace(STienUocThucHienNamTruoc) ? Convert.ToDouble(STienUocThucHienNamTruoc) : 0;

        public int? ISoKeHoachThucHienNamNay => !string.IsNullOrWhiteSpace(SSoKeHoachThucHienNamNay.Replace(".", string.Empty)) ? Convert.ToInt32(SSoKeHoachThucHienNamNay.Replace(".", string.Empty)) : 0;
        public double? FTienKeHoachThucHienNamNay => !string.IsNullOrWhiteSpace(STienKeHoachThucHienNamNay) ? Convert.ToDouble(STienKeHoachThucHienNamNay) : 0;

        public int? ISoSQ => !string.IsNullOrWhiteSpace(SSoSQ.Replace(".", string.Empty)) ? Convert.ToInt32(SSoSQ.Replace(".", string.Empty)) : 0;
        public double? FTienSQ => !string.IsNullOrWhiteSpace(STienSQ) ? Convert.ToDouble(STienSQ) : 0;

        public int? ISoQNCN => !string.IsNullOrWhiteSpace(SSoQNCN.Replace(".", string.Empty)) ? Convert.ToInt32(SSoQNCN.Replace(".", string.Empty)) : 0;
        public double? FTienQNCN => !string.IsNullOrWhiteSpace(STienQNCN) ? Convert.ToDouble(STienQNCN) : 0;

        public int? ISoCNVQP => !string.IsNullOrWhiteSpace(SSoCNVQP.Replace(".", string.Empty)) ? Convert.ToInt32(SSoCNVQP.Replace(".", string.Empty)) : 0;
        public double? FTienCNVQP => !string.IsNullOrWhiteSpace(STienCNVQP) ? Convert.ToDouble(STienCNVQP) : 0;

        public int? ISoLDHD => !string.IsNullOrWhiteSpace(SSoLDHD.Replace(".", string.Empty)) ? Convert.ToInt32(SSoLDHD.Replace(".", string.Empty)) : 0;
        public double? FTienLDHD => !string.IsNullOrWhiteSpace(STienLDHD) ? Convert.ToDouble(STienLDHD) : 0;

        public int? ISoHSQBS => !string.IsNullOrWhiteSpace(SSoHSQBS.Replace(".", string.Empty)) ? Convert.ToInt32(SSoHSQBS.Replace(".", string.Empty)) : 0;
        public double? FTienHSQBS => !string.IsNullOrWhiteSpace(STienHSQBS) ? Convert.ToDouble(STienHSQBS) : 0;
    }
}

