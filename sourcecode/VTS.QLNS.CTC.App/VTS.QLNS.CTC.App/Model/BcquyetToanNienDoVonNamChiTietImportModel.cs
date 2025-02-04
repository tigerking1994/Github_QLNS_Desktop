using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    [SheetAttribute(0, "Chứng từ", 9, 0)]
    public class BcquyetToanNienDoVonNamChiTietImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _sMaLoaiCongTrinh;
        [ColumnAttribute("MaLoaiCongTrinh", 4, ValidateType.IsNumber)]
        public string SMaLoaiCongTrinh
        {
            get => _sMaLoaiCongTrinh;
            set => SetProperty(ref _sMaLoaiCongTrinh, value);
        }

        private string _iStt;
        [ColumnAttribute("STT", 0, ValidateType.IsNumber)]
        public string iStt
        {
            get => _iStt;
            set => SetProperty(ref _iStt, value);
        }

        private Guid _iID_DuAnID;
        public Guid iID_DuAnID
        {
            get => _iID_DuAnID;
            set => SetProperty(ref _iID_DuAnID, value);
        }

        private string _sMaDuAn;
        [ColumnAttribute("Mã dự án đầu tư", 3)]
        public string sMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sDiaDiem;
        [ColumnAttribute("Địa điểm mở tài khoản", 2)]
        public string sDiaDiem
        {
            get => _sDiaDiem;
            set => SetProperty(ref _sDiaDiem, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("Nội dung", 1)]
        public string sTenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _fTongMucDauTu;
        [ColumnAttribute("Tổng mức đầu tư ", 5)]
        public string fTongMucDauTu
        {
            get => _fTongMucDauTu;
            set => SetProperty(ref _fTongMucDauTu, value);
        }

        // column 6
        private string _fLuyKeThanhToanNamTruoc;
        [ColumnAttribute("Tổng số", 6)]
        public string fLuyKeThanhToanNamTruoc
        {
            get => _fLuyKeThanhToanNamTruoc;
            set => SetProperty(ref _fLuyKeThanhToanNamTruoc, value);
        }

        // column 7
        private string _fTamUngTheoCheDoChuaThuHoiNamTruoc;
        [ColumnAttribute("Trong đó vốn tạm ứng theo chế độ chưa thu hồi", 7)]
        public string fTamUngTheoCheDoChuaThuHoiNamTruoc
        {
            get => _fTamUngTheoCheDoChuaThuHoiNamTruoc;
            set => SetProperty(ref _fTamUngTheoCheDoChuaThuHoiNamTruoc, value);
        }

        // column 8 *
        private string _fGiaTriTamUngDieuChinhGiam;
        [ColumnAttribute("Số vốn tạm ứng theo chế độ chưa thu hồi của các năm trước nộp điều chỉnh giảm trong năm", 8)]
        public string fGiaTriTamUngDieuChinhGiam
        {
            get => _fGiaTriTamUngDieuChinhGiam;
            set => SetProperty(ref _fGiaTriTamUngDieuChinhGiam, value);
        }

        // column 9
        private string _fTamUngNamTruocThuHoiNamNay;
        [ColumnAttribute("Thanh toán KLHT của phần vốn tạm ứng theo chế độ từ KC đến hết niên độ năm trước năm", 9)]
        public string fTamUngNamTruocThuHoiNamNay
        {
            get => _fTamUngNamTruocThuHoiNamNay;
            set => SetProperty(ref _fTamUngNamTruocThuHoiNamNay, value);
        }

        // column 10
        private string _fKHVNamTruocChuyenNamNay;
        [ColumnAttribute("Kế hoạch vốn được kéo dài", 10)]
        public string fKHVNamTruocChuyenNamNay
        {
            get => _fKHVNamTruocChuyenNamNay;
            set => SetProperty(ref _fKHVNamTruocChuyenNamNay, value);
        }

        // column 11
        private string _fTongThanhToanVonKeoDaiNamNay;
        [ColumnAttribute("Tổng số", 11)]
        public string fTongThanhToanVonKeoDaiNamNay
        {
            get => _fTongThanhToanVonKeoDaiNamNay;
            set => SetProperty(ref _fTongThanhToanVonKeoDaiNamNay, value);
        }

        // column 12
        private string _fTongThanhToanSuDungVonNamTruoc;
        [ColumnAttribute("Thanh toán KLHT", 12)]
        public string fTongThanhToanSuDungVonNamTruoc
        {
            get => _fTongThanhToanSuDungVonNamTruoc;
            set => SetProperty(ref _fTongThanhToanSuDungVonNamTruoc, value);
        }

        // column 13
        private string _fTamUngTheoCheDoChuaThuHoiKeoDaiNamNay;
        [ColumnAttribute("Vốn tạm ứng theo chế độ chưa thu hồi", 13)]
        public string fTamUngTheoCheDoChuaThuHoiKeoDaiNamNay
        {
            get => _fTamUngTheoCheDoChuaThuHoiKeoDaiNamNay;
            set => SetProperty(ref _fTamUngTheoCheDoChuaThuHoiKeoDaiNamNay, value);
        }

        // column 14 *
        private string _fGiaTriNamTruocChuyenNamSau;
        [ColumnAttribute("Kế hoạch vốn được phép kéo dài sang năm sau (nếu có)", 14)]
        public string fGiaTriNamTruocChuyenNamSau
        {
            get => _fGiaTriNamTruocChuyenNamSau;
            set => SetProperty(ref _fGiaTriNamTruocChuyenNamSau, value);
        }

        // column 15
        private string _fVonConLaiHuyBoKeoDaiNamNay;
        [ColumnAttribute("Số vốn còn lại chưa thanh toán hủy bỏ (nếu có)", 15)]
        public string fVonConLaiHuyBoKeoDaiNamNay
        {
            get => _fVonConLaiHuyBoKeoDaiNamNay;
            set => SetProperty(ref _fVonConLaiHuyBoKeoDaiNamNay, value);
        }

        // column 16
        private string _fKHVNamNay;
        [ColumnAttribute("Kế hoạch vốn đầu tư năm", 16)]
        public string fKHVNamNay
        {
            get => _fKHVNamNay;
            set => SetProperty(ref _fKHVNamNay, value);
        }

        // column 17
        private string _fTongKeHoachThanhToanVonNamNay;
        [ColumnAttribute("Tổng số", 17)]
        public string fTongKeHoachThanhToanVonNamNay
        {
            get => _fTongKeHoachThanhToanVonNamNay;
            set => SetProperty(ref _fTongKeHoachThanhToanVonNamNay, value);
        }

        // column 18
        private string _fTongThanhToanSuDungVonNamNay;
        [ColumnAttribute("Số vốn thanh toán KLHT", 18)]
        public string fTongThanhToanSuDungVonNamNay
        {
            get => _fTongThanhToanSuDungVonNamNay;
            set => SetProperty(ref _fTongThanhToanSuDungVonNamNay, value);
        }

        // column 19
        private string _fTamUngTheoCheDoChuaThuHoiNamNay;
        [ColumnAttribute("Số vốn tạm ứng theo chế độ chưa thu hồi", 19)]
        public string fTamUngTheoCheDoChuaThuHoiNamNay
        {
            get => _fTamUngTheoCheDoChuaThuHoiNamNay;
            set => SetProperty(ref _fTamUngTheoCheDoChuaThuHoiNamNay, value);
        }

        // column 20*
        private string _fGiaTriNamNayChuyenNamSau;
        [ColumnAttribute("KH vốn được phép kéo dài sang năm sau (nếu có)", 20)]
        public string fGiaTriNamNayChuyenNamSau
        {
            get => _fGiaTriNamNayChuyenNamSau;
            set => SetProperty(ref _fGiaTriNamNayChuyenNamSau, value);
        }

        //column 21
        private string _fVonConLaiHuyBoNamNay;
        [ColumnAttribute("Số vốn còn lại chưa thanh toán hủy bỏ (nếu có)", 21)]
        public string fVonConLaiHuyBoNamNay
        {
            get => _fVonConLaiHuyBoNamNay;
            set => SetProperty(ref _fVonConLaiHuyBoNamNay, value);
        }
        // column 22
        private string _fTongVonThanhToanNamNay;
        [ColumnAttribute("Tổng cộng vốn đã thanh toán KLHT quyết toán trong năm", 22)]
        public string fTongVonThanhToanNamNay
        {
            get => _fTongVonThanhToanNamNay;
            set => SetProperty(ref _fTongVonThanhToanNamNay, value);
        }

        // column 23
        private string _fLuyKeTamUngChuaThuHoiChuyenSangNam;
        [ColumnAttribute("Tổng cộng vốn đã thanh toán KLHT quyết toán trong năm", 23)]
        public string FLuyKeTamUngChuaThuHoiChuyenSangNam
        {
            get => _fLuyKeTamUngChuaThuHoiChuyenSangNam;
            set => SetProperty(ref _fLuyKeTamUngChuaThuHoiChuyenSangNam, value);
        }

        private string _fLuyKeConDaThanhToanHetNamNay;
        [ColumnAttribute("Tổng cộng vốn đã thanh toán KLHT quyết toán trong năm", 24)]
        public string FLuyKeConDaThanhToanHetNamNay
        {
            get => _fLuyKeConDaThanhToanHetNamNay;
            set => SetProperty(ref _fLuyKeConDaThanhToanHetNamNay, value);
        }
    }
}
