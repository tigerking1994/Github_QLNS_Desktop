using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ", 20, 0)]
    public class KeHoachChiQuyImportModel : BindableBase        
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _istt;
        [ColumnAttribute("STT", 0)]
        public string iStt
        {
            get => _istt;
            set => SetProperty(ref _istt, value);
        }

        private string _sMaDuAn;
        [ColumnAttribute("Mã dự án", 1, isRequired: true)]
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("Tên dự án", 2, isRequired: true)]
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sLoaiThanhToan;
        [ColumnAttribute("Loại thanh toán", 14, isRequired: true)]
        public string SLoaiThanhToan
        {
            get => _sLoaiThanhToan;
            set => SetProperty(ref _sLoaiThanhToan, value);
        }

        private string _fKeHoachVonNam;
        [ColumnAttribute("KHV năm", 3, ValidateType.IsNumber)]
        public string fKeHoachVonNam
        {
            get => _fKeHoachVonNam;
            set => SetProperty(ref _fKeHoachVonNam, value);
        }

        private string _fTongQuyTruoc;
        [ColumnAttribute("Tổng số", 4, ValidateType.IsNumber)]
        public string fTongQuyTruoc
        {
            get => _fTongQuyTruoc;
            set => SetProperty(ref _fTongQuyTruoc, value);
        }

        private string _fThanhToanKLHTQuyTruoc;
        [ColumnAttribute("Thanh toán KLHT", 5, ValidateType.IsNumber)]
        public string fThanhToanKLHTQuyTruoc
        {
            get => _fThanhToanKLHTQuyTruoc;
            set => SetProperty(ref _fThanhToanKLHTQuyTruoc, value);
        }

        private string _fThanhToanTamUngQuyTruoc;
        [ColumnAttribute("Tạm ứng theo chế độ", 6, ValidateType.IsNumber)]
        public string fThanhToanTamUngQuyTruoc
        {
            get => _fThanhToanTamUngQuyTruoc;
            set => SetProperty(ref _fThanhToanTamUngQuyTruoc, value);
        }

        private string _fTongQuyNay;
        [ColumnAttribute("Tổng số", 7, ValidateType.IsNumber)]
        public string fTongQuyNay
        {
            get => _fTongQuyNay;
            set => SetProperty(ref _fTongQuyNay, value);
        }

        private string _fThanhToanKLHTQuyNay;
        [ColumnAttribute("Thanh toán KLHT", 8, ValidateType.IsNumber)]
        public string fThanhToanKLHTQuyNay
        {
            get => _fThanhToanKLHTQuyNay;
            set => SetProperty(ref _fThanhToanKLHTQuyNay, value);
        }

        private string _fThuHoiUng;
        [ColumnAttribute("Thu hồi tạm ứng", 9, ValidateType.IsNumber)]
        public string fThuHoiUng
        {
            get => _fThuHoiUng;
            set => SetProperty(ref _fThuHoiUng, value);
        }

        private string _fThanhToanTamUngQuyNay;
        [ColumnAttribute("Tạm ứng theo chế độ", 10, ValidateType.IsNumber)]
        public string fThanhToanTamUngQuyNay
        {
            get => _fThanhToanTamUngQuyNay;
            set => SetProperty(ref _fThanhToanTamUngQuyNay, value);
        }

        private string _fSoConGiaiNganNam;
        [ColumnAttribute("Số còn lại giải ngân năm", 11, ValidateType.IsNumber)]
        public string fSoConGiaiNganNam
        {
            get => _fSoConGiaiNganNam;
            set => SetProperty(ref _fSoConGiaiNganNam, value);
        }

        private string _fGiaTriDeNghi;
        [ColumnAttribute("Nhu cầu chi quý", 12, ValidateType.IsNumber)]
        public string FGiaTriDeNghi
        {
            get => _fGiaTriDeNghi;
            set => SetProperty(ref _fGiaTriDeNghi, value);
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 13)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
    }
}
