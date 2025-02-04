using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExportVdtQtBcquyetToanNienDoChiTiet1Model : BindableBase
    {
        public bool IsHangCha { get; set; }
        public string iStt { get; set; }
        public Guid IIDDuAnID { get; set; }
        public string SMaDuAn { get; set; }
        public string SDiaDiem { get; set; }
        public string STenDuAn { get; set; }
        public string SMaLoaiCongTrinh { get; set; }
        public double FTongMucDauTu { get; set; }

        // column 6
        public double FLuyKeThanhToanNamTruoc { get; set; }
        // column 7
        public double FTamUngTheoCheDoChuaThuHoiNamTruoc { get; set; }
        // column 8
        public double FGiaTriTamUngDieuChinhGiam { get; set; }
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
        public double FGiaTriNamTruocChuyenNamSau { get; set; }
        // column 15
        public double FVonConLaiHuyBoKeoDaiNamNay { get; set; }
        // column 16
        public double FKHVNamNay { get; set; }
        // column 17
        public double FTongKeHoachThanhToanVonNamNay { get; set; }
        // column 18
        public double FTongThanhToanSuDungVonNamNay { get; set; }
        // column 19
        public double FTamUngTheoCheDoChuaThuHoiNamNay { get; set; }
        // column 20
        public double FGiaTriNamNayChuyenNamSau { get; set; }
        //column 21
        public double FVonConLaiHuyBoNamNay { get; set; }
        // column 22
        public double FTongVonThanhToanNamNay { get; set; }
        // column 23
        public double FLuyKeTamUngChuaThuHoiChuyenSangNam { get; set; }
        // column 24
        public double FLuyKeConDaThanhToanHetNamNay { get; set; }
        public int LoaiParent { get; set; }
        public Guid? IIdLoaiCongTrinhParent { get; set; }
    }
}
