using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtQtBcquyetToanNienDoChiTiet1Query
    {
        public bool BIsChuyenTiep { get; set; }
        public Guid IIDDuAnID { get; set; }
        public string SMaDuAn { get; set; }
        public string SDiaDiem { get; set; }
        public string STenDuAn { get; set; }
        public double FTongMucDauTu { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public string SMaLoaiCongTrinh { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        // column 8*
        public double FGiaTriTamUngDieuChinhGiam { get; set; }
        // column 14*
        public double FGiaTriNamTruocChuyenNamSau { get; set; }
        // column 20*
        public double FGiaTriNamNayChuyenNamSau { get; set; }

        // column 6
        public double FLuyKeThanhToanNamTruoc { get; set; }
        // column 7
        public double FTamUngTheoCheDoChuaThuHoiNamTruoc { get; set; }
        // column 9
        public double FTamUngNamTruocThuHoiNamNay { get; set; }
        // column 10
        public double FKHVNamTruocChuyenNamNay { get; set; }
        // column 11
        public double FTongThanhToanVonKeoDaiNamNay
        {
            get
            {
                return FTongThanhToanSuDungVonNamTruoc + FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay;
            }
        }
        // column 12
        public double FTongThanhToanSuDungVonNamTruoc { get; set; }
        // column 13
        public double FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay
        {
            get
            {
                return FTamUngNamNayDungVonNamTruoc - FThuHoiTamUngNamNayDungVonNamTruoc;
            }
        }
        // column 16
        public double FKHVNamNay { get; set; }
        // column 17
        public double FTongKeHoachThanhToanVonNamNay
        {
            get
            {
                return FTongThanhToanSuDungVonNamNay + FTamUngTheoCheDoChuaThuHoiNamNay;
            }
        }
        // column 18
        public double FTongThanhToanSuDungVonNamNay { get; set; }
        // column 19
        public double FTamUngTheoCheDoChuaThuHoiNamNay
        {
            get
            {
                return FTongTamUngNamNay - FTongThuHoiTamUngNamNay;
            }
        }
        // column 22
        public double FTongVonThanhToanNamNay
        {
            get
            {
                return FTamUngNamTruocThuHoiNamNay + FTongThanhToanSuDungVonNamTruoc + FTongThanhToanSuDungVonNamNay;
            }
        }
        // column 24
        public double FLuyKeConDaThanhToanHetNamNay
        {
            get
            {
                return FLuyKeThanhToanNamTruoc + FTongThanhToanVonKeoDaiNamNay + FTongKeHoachThanhToanVonNamNay;
            }
        }
        public double FThuHoiTamUngNamNayDungVonNamTruoc { get; set; }
        public double FTongTamUngNamNay { get; set; }
        public double FTamUngNamNayDungVonNamTruoc { get; set; }
        public double FTongThuHoiTamUngNamNay { get; set; }
        public int? ICoQuanThanhToan { get; set; }
    }
}
