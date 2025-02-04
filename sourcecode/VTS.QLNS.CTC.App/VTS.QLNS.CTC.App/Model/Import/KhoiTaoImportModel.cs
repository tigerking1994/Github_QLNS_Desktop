using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Sheet1", 7, 0)]
    public class KhoiTaoImportModel : BindableBase
    {
        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
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

        private string _maDuAn;
        [ColumnAttribute("Mã dự án", 0)]
        public string MaDuAn
        {
            get => _maDuAn;
            set => SetProperty(ref _maDuAn, value);
        }

        private string _tenDuAn;
        [ColumnAttribute("Tên dự án", 1)]
        public string TenDuAn
        {
            get => _tenDuAn;
            set => SetProperty(ref _tenDuAn, value);
        }

        private string _sMaLoaiCongTrinh;
        [ColumnAttribute("Mã loại công trình", 2)]
        public string SMaLoaiCongTrinh 
        { 
            get => _sMaLoaiCongTrinh; 
            set => SetProperty(ref _sMaLoaiCongTrinh, value); 
        }

        private string _fKHVN_VonBoTriHetNamTruoc;
        [ColumnAttribute("Vốn bố trí hết năm trước", 3, ValidateType.IsNumber)]
        public string FKHVN_VonBoTriHetNamTruoc
        {
            get => _fKHVN_VonBoTriHetNamTruoc;
            set => SetProperty(ref _fKHVN_VonBoTriHetNamTruoc, value);
        }

        private string _fKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc;
        [ColumnAttribute("Lũy kế vốn đã thanh toán từ khởi công đến hết năm trước", 4, ValidateType.IsNumber)]
        public string FKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc
        {
            get => _fKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc;
            set => SetProperty(ref _fKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc, value);
        }

        private string _fKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi;
        [ColumnAttribute("Trong đó vốn tạm ứng theo chế độ chưa thu hồi", 5, ValidateType.IsNumber)]
        public string FKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi
        {
            get => _fKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi;
            set => SetProperty(ref _fKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi, value);
        }

        private string _fKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc;
        [ColumnAttribute("Lũy kế vốn tạm ứng theo chế độ chưa thu hồi  nộp điều chỉnh giảm đến hết năm trước", 6, ValidateType.IsNumber)]
        public string FKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc
        {
            get => _fKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc;
            set => SetProperty(ref _fKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc, value);
        }

        private string _fKHVN_KeHoachVonKeoDaiSangNam;
        [ColumnAttribute("Kế hoạch vốn kéo dài sang năm", 7, ValidateType.IsNumber)]
        public string FKHVN_KeHoachVonKeoDaiSangNam
        {
            get => _fKHVN_KeHoachVonKeoDaiSangNam;
            set => SetProperty(ref _fKHVN_KeHoachVonKeoDaiSangNam, value);
        }

        private string _fKHUT_VonBoTriHetNamTruoc;
        [ColumnAttribute("Vốn bố trí  hết năm trước", 8, ValidateType.IsNumber)]
        public string FKHUT_VonBoTriHetNamTruoc
        {
            get => _fKHUT_VonBoTriHetNamTruoc;
            set => SetProperty(ref _fKHUT_VonBoTriHetNamTruoc, value);
        }

        private string _fKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc;
        [ColumnAttribute("Lũy kế vốn đã thanh toán từ khởi công đến hết năm trước", 9, ValidateType.IsNumber)]
        public string FKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc
        {
            get => _fKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc;
            set => SetProperty(ref _fKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc, value);
        }

        private string _fKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi;
        [ColumnAttribute("Trong đó vốn tạm ứng theo chế độ chưa thu hồi", 10, ValidateType.IsNumber)]
        public string FKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi
        {
            get => _fKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi;
            set => SetProperty(ref _fKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi, value);
        }

        private string _fKHUT_KeHoachUngTruocKeoDaiSangNam;
        [ColumnAttribute("Kế hoạch ứng trước kéo dài sang năm", 11, ValidateType.IsNumber)]
        public string FKHUT_KeHoachUngTruocKeoDaiSangNam
        {
            get => _fKHUT_KeHoachUngTruocKeoDaiSangNam;
            set => SetProperty(ref _fKHUT_KeHoachUngTruocKeoDaiSangNam, value);
        }

        private string _fKHUT_KeHoachUngTruocChuaThuHoi;
        [ColumnAttribute("Kế hoạch ứng trc chưa thu hồi", 12, ValidateType.IsNumber)]
        public string FKHUT_KeHoachUngTruocChuaThuHoi
        {
            get => _fKHUT_KeHoachUngTruocChuaThuHoi;
            set => SetProperty(ref _fKHUT_KeHoachUngTruocChuaThuHoi, value);
        }

        public double FKHVN_VonBoTriHetNamTruocValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(FKHVN_VonBoTriHetNamTruoc, out value);
                return value;
            }
        }

        public double FKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruocValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(FKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc, out value);
                return value;
            }
        }

        public double FKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoiValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(FKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi, out value);
                return value;
            }
        }

        public double FKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruocValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(FKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc, out value);
                return value;
            }
        }

        public double FKHVN_KeHoachVonKeoDaiSangNamValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(FKHVN_KeHoachVonKeoDaiSangNam, out value);
                return value;
            }
        }

        public double FKHUT_VonBoTriHetNamTruocValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(FKHUT_VonBoTriHetNamTruoc, out value);
                return value;
            }
        }

        public double FKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruocValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(FKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc, out value);
                return value;
            }
        }

        public double FKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoiValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(FKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi, out value);
                return value;
            }
        }

        public double FKHUT_KeHoachUngTruocKeoDaiSangNamValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(FKHUT_KeHoachUngTruocKeoDaiSangNam, out value);
                return value;
            }
        }

        public double FKHUT_KeHoachUngTruocChuaThuHoiValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(FKHUT_KeHoachUngTruocChuaThuHoi, out value);
                return value;
            }
        }

        public Guid DuAnId { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
    }
}
