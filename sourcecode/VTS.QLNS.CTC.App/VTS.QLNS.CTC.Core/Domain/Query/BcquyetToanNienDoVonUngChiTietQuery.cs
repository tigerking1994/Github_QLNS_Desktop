using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BcquyetToanNienDoVonUngChiTietQuery
    {
        public Guid IIDDuAnID { get; set; }
        public string SMaDuAn { get; set; }
        public string SDiaDiem { get; set; }
        public string STenDuAn { get; set; }
        public int ICoQuanThanhToan { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public string SMaLoaiCongTrinh { get; set; }
        public bool BIsChuyenTiep { get; set; }

        // col 6*
        public double FGiaTriThuHoiTheoGiaiNganThucTe { get; set; }

        // col 1
        public double FUngTruocChuaThuHoiNamTruoc { get; set; }
        // col 2
        public double FLuyKeThanhToanNamTruoc { get; set; }
        // col 3
        //public double FKeHoachVonDuocKeoDai
        //{
        //    get
        //    {
        //        return FUngTruocChuaThuHoiNamTruoc - FLuyKeThanhToanNamTruoc;
        //    }
        //}
        // col 4
        //public double FVonKeoDaiDaThanhToanNamNay
        //{
        //    get
        //    {
        //        return (FThanhToanKLHTNamTruocChuyenSang + FThanhToanUngNamTruocChuyenSang)
        //            - (FThuHoiTamUngNamNayVonNamTruoc + FThuHoiTamUngNamTruocVonNamTruoc);
        //    }
        //}

        // col 5
        public double FThuHoiVonNamNay { get; set; }
        // col 7
        public double FKHVUNamNay { get; set; }
        //// col 8
        //public double FVonDaThanhToanNamNay
        //{
        //    get
        //    {
        //        return (FThanhToanKLHTTamUngNamNay + FThanhToanUngNamNay)
        //            - (FThuHoiTamUngNamNay + FThuHoiTamUngNamTruoc);
        //    }
        //}
        //// col 9
        //public double FKHVUChuaThuHoiChuyenNamSau
        //{
        //    get
        //    {
        //        return U - FThuHoiVonNamNay + FKHVUNamNay;
        //    }
        //}
        //// col 10
        //public double FTongSoVonDaThanhToanThuHoi
        //{
        //    get
        //    {
        //        return FLuyKeThanhToanNamTruoc + FVonKeoDaiDaThanhToanNamNay + FVonDaThanhToanNamNay;
        //    }
        //}

        // col 4 - ThanhToanKLHTNamTruocChuyenSang
        public double FThanhToanKLHTNamTruocChuyenSang { get; set; }
        // col 4 - ThanhToanUngNamTruocChuyenSang
        public double FThanhToanUngNamTruocChuyenSang { get; set; }
        // col 4 - Thu hoi tam ung nam nay dung von nam truoc
        public double FThuHoiTamUngNamNayVonNamTruoc { get; set; }
        // col 4 - Thu hoi tam ung nam truoc dung von nam truoc
        public double FThuHoiTamUngNamTruocVonNamTruoc { get; set; }

        // col 8 - ThanhToanKLHTNamNay
        public double FThanhToanKLHTTamUngNamNay { get; set; }
        // col 8 - ThanhToanUngNamNay
        public double FThanhToanUngNamNay { get; set; }
        // col 8 - Thu hoi tam ung nam nay dung von nam nay
        public double FThuHoiTamUngNamNay { get; set; }
        // col 8 - Thu hoi tam ung nam nay dung von nam truoc
        public double FThuHoiTamUngNamTruoc { get; set; }

        // --- column bo xung
        public double fLuyKeUngNamTruoc { get; set; }
    }
}
