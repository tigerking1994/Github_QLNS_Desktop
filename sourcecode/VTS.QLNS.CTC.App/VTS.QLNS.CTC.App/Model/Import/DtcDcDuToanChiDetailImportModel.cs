using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 7, 0)]
    public class DtcDcDuToanChiDetailImportModel : BindableBase
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
        [ColumnAttribute("Xâu nối mã", 0,Utility.Enum.ValidateType.IsXauNoiMaBH)]
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
        private string _sTienDuToanDuocGiao;
        [ColumnAttribute("Dự toán được giao", 2)]
        public string STienDuToanDuocGiao
        {
            get => _sTienDuToanDuocGiao;
            set => SetProperty(ref _sTienDuToanDuocGiao, value);
        }

        private string _sTienThucHien06ThangDauNam;
        [ColumnAttribute("Thưc hiện 6 tháng đầu năm ", 3)]
        public string STienThucHien06ThangDauNam
        {
            get => _sTienThucHien06ThangDauNam;
            set => SetProperty(ref _sTienThucHien06ThangDauNam, value);
        }

        private string _sTienUocThucHien06ThangCuoiNam;
        [ColumnAttribute("Ước thực hiện 6 tháng cuối năm", 4)]
        public string STienUocThucHien06ThangCuoiNam
        {
            get => _sTienUocThucHien06ThangCuoiNam;
            set => SetProperty(ref _sTienUocThucHien06ThangCuoiNam, value);
        }

        private string _sTienUocThucHienCaNam;
        [ColumnAttribute("Cộng", 5)]
        public string STienUocThucHienCaNam
        {
            get => _sTienUocThucHienCaNam;
            set => SetProperty(ref _sTienUocThucHienCaNam, value);
        }


        private string _sTienSoSanhTang;
        [ColumnAttribute("Tăng", 6)]
        public string STienSoSanhTang
        {
            get => _sTienSoSanhTang;
            set => SetProperty(ref _sTienSoSanhTang, value);
        }

        private string _sTienSoSanhGiam;
        [ColumnAttribute("Giảm", 7)]
        public string STienSoSanhGiam
        {
            get => _sTienSoSanhGiam;
            set => SetProperty(ref _sTienSoSanhGiam, value);
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 8)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private string _sDuToanChiTietToi;
        [ColumnAttribute("Dự toán chi tiết chi tới", 9)]
        public string SDuToanChiTietToi
        {
            get => _sDuToanChiTietToi;
            set => SetProperty(ref _sDuToanChiTietToi, value);
        }
        public bool IsDataNotNull => !string.IsNullOrWhiteSpace(STienDuToanDuocGiao) || !string.IsNullOrWhiteSpace(STienThucHien06ThangDauNam) || !string.IsNullOrWhiteSpace(STienUocThucHien06ThangCuoiNam)
                || !string.IsNullOrWhiteSpace(STienUocThucHienCaNam) || !string.IsNullOrWhiteSpace(STienSoSanhTang) || !string.IsNullOrWhiteSpace(STienSoSanhGiam);

        public double? FTienDuToanDuocGiao => !string.IsNullOrWhiteSpace(STienDuToanDuocGiao) ? Convert.ToDouble(STienDuToanDuocGiao) : 0;
        public double? FTienThucHien06ThangDauNam => !string.IsNullOrWhiteSpace(STienThucHien06ThangDauNam) ? Convert.ToDouble(STienThucHien06ThangDauNam) : 0;
        public double? FTienUocThucHien06ThangCuoiNam => !string.IsNullOrWhiteSpace(STienUocThucHien06ThangCuoiNam) ? Convert.ToDouble(STienUocThucHien06ThangCuoiNam) : 0;
        public double? FTienUocThucHienCaNam => !string.IsNullOrWhiteSpace(STienUocThucHienCaNam) ? Convert.ToDouble(STienUocThucHienCaNam) : 0;
        public double? FTienSoSanhTang => !string.IsNullOrWhiteSpace(STienSoSanhTang) ? Convert.ToDouble(STienSoSanhTang) : 0;
        public double? FTienSoSanhGiam => !string.IsNullOrWhiteSpace(STienSoSanhGiam) ? Convert.ToDouble(STienSoSanhGiam) : 0;
    }
}
