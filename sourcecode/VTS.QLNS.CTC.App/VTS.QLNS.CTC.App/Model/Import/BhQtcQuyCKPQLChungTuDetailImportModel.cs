using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 9, 0)]
    public class BhQtcQuyCKPQLChungTuDetailImportModel : BindableBase
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
        [ColumnAttribute("Xâu nối mã", 0,Utility.Enum.ValidateType.IsXauNoiMaBH)]
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

        private string _sTienDuToanDuocGiao;
        [ColumnAttribute("Dự toán được giao", 2)]
        public string STienDuToanDuocGiao
        {
            get => _sTienDuToanDuocGiao;
            set => SetProperty(ref _sTienDuToanDuocGiao, value);
        }
        private string _sTienSoThucChi;
        [ColumnAttribute("Số thực chi", 3)]
        public string STienSoThucChi
        {
            get => _sTienSoThucChi;
            set => SetProperty(ref _sTienSoThucChi, value);
        }

        private string _sTienQuyetToanDaDuocDuyet;
        [ColumnAttribute("Số quyết toán đã được duyệt", 4)]
        public string STienQuyetToanDaDuocDuyet
        {
            get => _sTienQuyetToanDaDuocDuyet;
            set => SetProperty(ref _sTienQuyetToanDaDuocDuyet, value);
        }

        private string _sTienDeNghiQuyetToanQuyNay;
        [ColumnAttribute("Số đề nghị quyết toán quý này", 5)]
        public string STienDeNghiQuyetToanQuyNay
        {
            get => _sTienDeNghiQuyetToanQuyNay;
            set => SetProperty(ref _sTienDeNghiQuyetToanQuyNay, value);
        }

        private string _sTienXacNhanQuyetToanQuyNay;
        [ColumnAttribute("Xác nhận quyết toán quý này", 6)]
        public string STienXacNhanQuyetToanQuyNay
        {
            get => _sTienXacNhanQuyetToanQuyNay;
            set => SetProperty(ref _sTienXacNhanQuyetToanQuyNay, value);
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi Chu", 7)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
        private string _sDuToanChiTietToi;
        [ColumnAttribute("Dư toán chi tiết tới", 8)]
        public string SDuToanChiTietToi
        {
            get => _sDuToanChiTietToi;
            set => SetProperty(ref _sDuToanChiTietToi, value);
        }


        public bool IsHasData => !string.IsNullOrWhiteSpace(STienXacNhanQuyetToanQuyNay) || !string.IsNullOrWhiteSpace(STienSoThucChi) || !string.IsNullOrWhiteSpace(STienDuToanDuocGiao) || !string.IsNullOrWhiteSpace(STienDeNghiQuyetToanQuyNay);

        public double? FTienXacNhanQuyetToanQuyNay { get; set; }
        public double? FTienLuyKeCuoiQuyNay { get; set; }
        public double? FTienSoThucChi { get; set; }
        public double? FTienDuToanDuocGiao { get; set; }
        public double? FTienLuyKeDenCuoiKy { get; set; }
        public double? FTienDeNghiQuyetToanQuyNay { get; set; }

    }
}
