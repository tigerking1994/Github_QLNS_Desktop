using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{

    [SheetAttribute(0, "Chứng từ chi tiết", 9, 0)]
    public class KhcKcbDetailImportModel : BaseImportModel 
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
        private string _sTienDaThucHienNamTruoc;
        [ColumnAttribute("Số đã thực hiện năm", 2)]
        public string STienDaThucHienNamTruoc
        {
            get => _sTienDaThucHienNamTruoc;
            set => SetProperty(ref _sTienDaThucHienNamTruoc, value);
        }

        private string _sTienUocThucHienNamTruoc;
        [ColumnAttribute("Ước thực  hiện năm", 3)]
        public string STienUocThucHienNamTruoc
        {
            get => _sTienUocThucHienNamTruoc;
            set => SetProperty(ref _sTienUocThucHienNamTruoc, value);
        }

        private string _sTienKeHoachThucHienNamNay;
        [ColumnAttribute("Kế hoạch thực hiện năm", 4)]
        public string STienKeHoachThucHienNamNay
        {
            get => _sTienKeHoachThucHienNamNay;
            set => SetProperty(ref _sTienKeHoachThucHienNamNay, value);
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 5)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
        public bool IsDataNotNull => !string.IsNullOrWhiteSpace(STienDaThucHienNamTruoc) || !string.IsNullOrWhiteSpace(STienKeHoachThucHienNamNay) || !string.IsNullOrWhiteSpace(STienUocThucHienNamTruoc);

        public double? FTienDaThucHienNamTruoc => !string.IsNullOrWhiteSpace(STienDaThucHienNamTruoc) ? Convert.ToDouble(STienDaThucHienNamTruoc) : 0;
        public double? FTienKeHoachThucHienNamNay => !string.IsNullOrWhiteSpace(STienKeHoachThucHienNamNay) ? Convert.ToDouble(STienKeHoachThucHienNamNay) : 0;
        public double? FTienUocThucHienNamTruoc => !string.IsNullOrWhiteSpace(STienUocThucHienNamTruoc) ? Convert.ToDouble(STienUocThucHienNamTruoc) : 0;
    }
}
