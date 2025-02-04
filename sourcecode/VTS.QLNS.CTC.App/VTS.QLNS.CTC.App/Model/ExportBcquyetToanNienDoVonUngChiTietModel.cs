using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExportBcquyetToanNienDoVonUngChiTietModel : BindableBase
    {
        public bool IsHangCha { get; set; }
        public string iStt { get; set; }
        public Guid IIDDuAnID { get; set; }
        public string SMaDuAn { get; set; }
        public string SDiaDiem { get; set; }
        public string STenDuAn { get; set; }
        public string SMaLoaiCongTrinh { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public bool BIsChuyenTiep { get; set; }
        // col 1
        public double FUngTruocChuaThuHoiNamTruoc { get; set; }
        // col 2
        public double FLuyKeThanhToanNamTruoc { get; set; }
        // col 3
        public double FKeHoachVonDuocKeoDai { get; set; }
        // col 4
        public double FVonKeoDaiDaThanhToanNamNay { get; set; }
        // col 5
        public double FThuHoiVonNamNay { get; set; }
        // col 6
        public double FGiaTriThuHoiTheoGiaiNganThucTe { get; set; }
        // col 7
        public double FKHVUNamNay { get; set; }
        // col 8
        public double FVonDaThanhToanNamNay { get; set; }
        // col 9
        public double FKHVUChuaThuHoiChuyenNamSau { get; set; }
        // col 10
        public double FTongSoVonDaThanhToanThuHoi { get; set; }
    }
}
