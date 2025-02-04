using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtQtBcquyetToanNienDoChiTiet1Model : BindableBase
    {
        public bool BIsShow { get; set; }
        public bool BIsChuyenTiep { get; set; }
        public string iStt { get; set; }
        public Guid IIDDuAnID { get; set; }
        public string SMaDuAn { get; set; }
        public string SDiaDiem { get; set; }
        public string STenDuAn { get; set; }
        public double FTongMucDauTu { get; set; }
        public bool IsHangCha { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public string SMaLoaiCongTrinh { get; set; }

        // column 6
        public double FLuyKeThanhToanNamTruoc { get; set; }
        // column 7
        public double FTamUngTheoCheDoChuaThuHoiNamTruoc { get; set; }
        // column 8
        private double _fGiaTriTamUngDieuChinhGiam;
        public double FGiaTriTamUngDieuChinhGiam
        {
            get => _fGiaTriTamUngDieuChinhGiam;
            set => SetProperty(ref _fGiaTriTamUngDieuChinhGiam, value);
        }
        // column 9
        public double FTamUngNamTruocThuHoiNamNay { get; set; }
        // column 10
        public double FKHVNamTruocChuyenNamNay { get; set; }
        // column 11
        public double FTongThanhToanVonKeoDaiNamNay { get; set; }
        // column 12
        public double FTongThanhToanSuDungVonNamTruoc { get; set; }
        // column 13
        public double FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay { get; set; }
        // column 14
        private double _fGiaTriNamTruocChuyenNamSau;
        public double FGiaTriNamTruocChuyenNamSau
        {
            get => _fGiaTriNamTruocChuyenNamSau;
            set => SetProperty(ref _fGiaTriNamTruocChuyenNamSau, value);
        }
        // column 15
        private double _fVonConLaiHuyBoKeoDaiNamNay;
        public double FVonConLaiHuyBoKeoDaiNamNay
        {
            get => _fVonConLaiHuyBoKeoDaiNamNay;
            set => SetProperty(ref _fVonConLaiHuyBoKeoDaiNamNay, value);
        }
        // column 16
        public double FKHVNamNay { get; set; }
        // column 17
        public double FTongKeHoachThanhToanVonNamNay { get; set; }
        // column 18
        public double FTongThanhToanSuDungVonNamNay { get; set; }
        // column 19
        public double FTamUngTheoCheDoChuaThuHoiNamNay { get; set; }
        // column 20
        private double _fGiaTriNamNayChuyenNamSau;
        public double FGiaTriNamNayChuyenNamSau
        {
            get => _fGiaTriNamNayChuyenNamSau;
            set => SetProperty(ref _fGiaTriNamNayChuyenNamSau, value);
        }
        //column 21
        private double _fVonConLaiHuyBoNamNay;
        public double FVonConLaiHuyBoNamNay
        {
            get => _fVonConLaiHuyBoNamNay;
            set => SetProperty(ref _fVonConLaiHuyBoNamNay, value);
        }
        // column 22
        public double FTongVonThanhToanNamNay { get; set; }
        // column 23
        private double _fLuyKeTamUngChuaThuHoiChuyenSangNam;
        public double FLuyKeTamUngChuaThuHoiChuyenSangNam
        {
            get => _fLuyKeTamUngChuaThuHoiChuyenSangNam;
            set => SetProperty(ref _fLuyKeTamUngChuaThuHoiChuyenSangNam, value);
        }
        // column 24
        public double FLuyKeConDaThanhToanHetNamNay { get; set; }

        public int ICoQuanThanhToan { get; set; }
        public int LoaiParent { get; set; }
        public Guid? IIdLoaiCongTrinhParent { get; set; }
    }
}
