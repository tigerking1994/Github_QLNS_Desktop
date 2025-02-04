using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 7, 0)]
    public class BhQtcQuyKPKDetailImportModel : BindableBase
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
        private string _sTienNamNayDuocGiao;
        [ColumnAttribute("Chỉ tiêu được giao năm nay", 3)]
        public string STienNamNayDuocGiao
        {
            get => _sTienNamNayDuocGiao;
            set => SetProperty(ref _sTienNamNayDuocGiao, value);
        }
        private string _sTienTongCong;
        [ColumnAttribute("Tổng cộng", 4)]
        public string STienTongCong
        {
            get => _sTienTongCong;
            set => SetProperty(ref _sTienTongCong, value);
        }
        private string _sTienThucChi;
        [ColumnAttribute("Số thực thi", 5)]
        public string STienThucChi
        {
            get => _sTienThucChi;
            set => SetProperty(ref _sTienThucChi, value);
        }
        private string _sTienDaDuyet;
        [ColumnAttribute("Số quyết toán đã được duyệt", 6)]
        public string STienDaDuyet
        {
            get => _sTienDaDuyet;
            set => SetProperty(ref _sTienDaDuyet, value);
        }
        private string _sTienDeNghiQuyetToanQuy;
        [ColumnAttribute("Số đề nghị quyết toán quý này", 7)]
        public string STienDeNghiQuyetToanQuy
        {
            get => _sTienDeNghiQuyetToanQuy;
            set => SetProperty(ref _sTienDeNghiQuyetToanQuy, value);
        }
        private string _sTienXacNhanQuy;
        [ColumnAttribute("Xác nhận quyết toán quý này", 8)]
        public string STienXacNhanQuy
        {
            get => _sTienXacNhanQuy;
            set => SetProperty(ref _sTienXacNhanQuy, value);
        }

        private string _sDuToanChiTietToi;
        [ColumnAttribute("Dự toán chi tiết tới", 9)]
        public string SDuToanChiTietToi
        {
            get => _sDuToanChiTietToi;
            set => SetProperty(ref _sDuToanChiTietToi, value);
        }

        public bool IsDataNotNull => !string.IsNullOrWhiteSpace(STienNamTruocChuyenSang) || !string.IsNullOrWhiteSpace(STienNamNayDuocGiao)
            || !string.IsNullOrWhiteSpace(STienTongCong) || !string.IsNullOrWhiteSpace(STienThucChi)
            || !string.IsNullOrWhiteSpace(STienDaDuyet) || !string.IsNullOrWhiteSpace(STienDeNghiQuyetToanQuy)
            || !string.IsNullOrWhiteSpace(STienDeNghiQuyetToanQuy);
    }
}
