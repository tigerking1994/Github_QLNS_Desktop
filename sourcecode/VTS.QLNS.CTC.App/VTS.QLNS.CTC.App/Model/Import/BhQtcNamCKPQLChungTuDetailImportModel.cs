using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 9, 0)]
    public class BhQtcNamCKPQLChungTuDetailImportModel : BindableBase
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

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }

        private string _sXauNoiMa;
        [ColumnAttribute("Xâu nối mã", 0, Utility.Enum.ValidateType.IsXauNoiMaBH)]
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private string _sNoiDung;
        [ColumnAttribute("Nội dung", 1)]
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }
        private string _sTienNamTruocChuyenSang;
        [ColumnAttribute("Năm trước chuyển sang", 2)]
        public string STienNamTruocChuyenSang
        {
            get => _sTienNamTruocChuyenSang;
            set => SetProperty(ref _sTienNamTruocChuyenSang, value);
        }
        private string _sTienDuToanDuocGiao;
        [ColumnAttribute("Dự toán được giao", 3)]
        public string STienDuToanDuocGiao
        {
            get => _sTienDuToanDuocGiao;
            set => SetProperty(ref _sTienDuToanDuocGiao, value);
        }
        private string _sTienTongCong;
        [ColumnAttribute("Tổng cộng", 4)]
        public string STienTongCong
        {
            get => _sTienTongCong;
            set => SetProperty(ref _sTienTongCong, value);
        }
        private string _sTienSoThucChiCaNam;
        [ColumnAttribute("Số thực chi cả năm", 5)]
        public string STienSoThucChiCaNam
        {
            get => _sTienSoThucChiCaNam;
            set => SetProperty(ref _sTienSoThucChiCaNam, value);
        }
        private string _sTienThua;
        [ColumnAttribute("Thừa ", 6)]
        public string STienThua
        {
            get => _sTienThua;
            set => SetProperty(ref _sTienThua, value);
        }
        private string _sTienThieu;
        [ColumnAttribute("Thiếu ", 7)]
        public string STienThieu
        {
            get => _sTienThieu;
            set => SetProperty(ref _sTienThieu, value);
        }
        private string _sTiLe;
        [ColumnAttribute("Tỉ lệ % thực hiện/ dự toán ", 8)]
        public string STiLe
        {
            get => _sTiLe;
            set => SetProperty(ref _sTiLe, value);
        }

        private string _sDuToanChiTietToi;
        [ColumnAttribute("Dư toán chi tiết tới", 9)]
        public string SDuToanChiTietToi
        {
            get => _sDuToanChiTietToi;
            set => SetProperty(ref _sDuToanChiTietToi, value);
        }


        public bool IsHasData => !string.IsNullOrWhiteSpace(STienTongCong) || !string.IsNullOrWhiteSpace(STienNamTruocChuyenSang)
                                || !string.IsNullOrWhiteSpace(STienDuToanDuocGiao) || !string.IsNullOrWhiteSpace(STienSoThucChiCaNam)
                                || !string.IsNullOrWhiteSpace(STienThua) || !string.IsNullOrWhiteSpace(STienThieu) || !string.IsNullOrWhiteSpace(STiLe);
    }
}
