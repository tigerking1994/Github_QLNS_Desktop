using System.Collections.Generic;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 5, 1)]
    public class CheckVoucherDetailImportModel : BindableBase
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

        public List<string> ListKyHieuChild { get; set; }
        public string KyHieuParent { get; set; }

        private string _kyHieu;
        [ColumnAttribute("Ký hiệu", 0, ValidateType.KyHieu)]
        public string KyHieu
        {
            get => _kyHieu;
            set => SetProperty(ref _kyHieu, value);
        }

        private string _stt;
        [ColumnAttribute("STT", 1)]
        public string STT
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _nganh;
        [ColumnAttribute("Ngành", 2)]
        public string Nganh
        {
            get => _nganh;
            set => SetProperty(ref _nganh, value);
        }

        private string _nganhCha;
        [ColumnAttribute("Ngành cha", 3)]
        public string NganhCha
        {
            get => _nganhCha;
            set => SetProperty(ref _nganhCha, value);
        }

        private string _description;
        [ColumnAttribute("Mô tả", 4)]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _tuChi;
        [ColumnAttribute("Tự chi", 6, ValidateType.IsNumber)]
        public string TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        private string _huyDong;
        [ColumnAttribute("Huy động tồn kho", 7, ValidateType.IsNumber)]
        public string HuyDong
        {
            get => _huyDong;
            set => SetProperty(ref _huyDong, value);
        }

        private string _muaHangHienVat;
        [ColumnAttribute("Mua hàng cấp hiện vật", 8, ValidateType.IsNumber)]
        public string MuaHangHienVat
        {
            get => _muaHangHienVat;
            set => SetProperty(ref _muaHangHienVat, value);
        }

        private string _dacThu;
        [ColumnAttribute("Đặc thù", 9, ValidateType.IsNumber)]
        public string DacThu
        {
            get => _dacThu;
            set => SetProperty(ref _dacThu, value);
        }
    }
}
