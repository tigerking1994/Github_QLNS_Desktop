using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    [SheetAttribute(0, "Chứng từ", 9, 0)]
    public class BcquyetToanNienDoVonUngChiTietImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _sMaLoaiCongTrinh;
        [ColumnAttribute("Mã loại công trình", 2)]
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
        [ColumnAttribute("Mã dự án đầu tư", 4)]
        public string sMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sDiaDiem;
        [ColumnAttribute("Địa điểm mở tài khoản", 3)]
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

        // col 1
        private string _fUngTruocChuaThuHoiNamTruoc;
        [ColumnAttribute("Kế hoạch vốn ứng trước chưa thu hồi", 5)]
        public string fUngTruocChuaThuHoiNamTruoc
        {
            get => _fUngTruocChuaThuHoiNamTruoc;
            set => SetProperty(ref _fUngTruocChuaThuHoiNamTruoc, value);
        }

        // col 2
        private string _fLuyKeThanhToanNamTruoc;
        [ColumnAttribute("Lũy kế vốn đã thanh toán", 6)]
        public string fLuyKeThanhToanNamTruoc
        {
            get => _fLuyKeThanhToanNamTruoc;
            set => SetProperty(ref _fLuyKeThanhToanNamTruoc, value);
        }

        // col 3
        private string _fKeHoachVonDuocKeoDai;
        [ColumnAttribute("Kế hoạch", 7)]
        public string fKeHoachVonDuocKeoDai
        {
            get => _fKeHoachVonDuocKeoDai;
            set => SetProperty(ref _fKeHoachVonDuocKeoDai, value);
        }

        // col 4
        private string _fVonKeoDaiDaThanhToanNamNay;
        [ColumnAttribute("Số vốn đã thanh toán trong năm", 8)]
        public string fVonKeoDaiDaThanhToanNamNay
        {
            get => _fVonKeoDaiDaThanhToanNamNay;
            set => SetProperty(ref _fVonKeoDaiDaThanhToanNamNay, value);
        }

        // col 5
        private string _fThuHoiVonNamNay;
        [ColumnAttribute("Kế hoạch vốn bố trí thu hồi trong năm", 9)]
        public string fThuHoiVonNamNay
        {
            get => _fThuHoiVonNamNay;
            set => SetProperty(ref _fThuHoiVonNamNay, value);
        }

        // col 6
        private string _fGiaTriThuHoiTheoGiaiNganThucTe;
        [ColumnAttribute("Số thu hồi theo kết quả giải ngân thực tế", 10)]
        public string fGiaTriThuHoiTheoGiaiNganThucTe
        {
            get => _fGiaTriThuHoiTheoGiaiNganThucTe;
            set => SetProperty(ref _fGiaTriThuHoiTheoGiaiNganThucTe, value);
        }

        // col 7
        private string _fKHVUNamNay;
        [ColumnAttribute("Kế hoạch vốn ứng trước trong năm", 11)]
        public string fKHVUNamNay
        {
            get => _fKHVUNamNay;
            set => SetProperty(ref _fKHVUNamNay, value);
        }

        // col 8
        private string _fVonDaThanhToanNamNay;
        [ColumnAttribute("Số vốn đã thanh toán trong năm", 12)]
        public string fVonDaThanhToanNamNay
        {
            get => _fVonDaThanhToanNamNay;
            set => SetProperty(ref _fVonDaThanhToanNamNay, value);
        }

        // col 9
        private string _fKHVUChuaThuHoiChuyenNamSau;
        [ColumnAttribute("Kế hoạch vốn ứng trước chưa thu hồi", 13)]
        public string fKHVUChuaThuHoiChuyenNamSau
        {
            get => _fKHVUChuaThuHoiChuyenNamSau;
            set => SetProperty(ref _fKHVUChuaThuHoiChuyenNamSau, value);
        }

        // col 10
        private string _fTongSoVonDaThanhToanThuHoi;
        [ColumnAttribute("Tổng số vốn đã thanh toán... thu hồi", 14)]
        public string fTongSoVonDaThanhToanThuHoi
        {
            get => _fTongSoVonDaThanhToanThuHoi;
            set => SetProperty(ref _fTongSoVonDaThanhToanThuHoi, value);
        }
    }
}
