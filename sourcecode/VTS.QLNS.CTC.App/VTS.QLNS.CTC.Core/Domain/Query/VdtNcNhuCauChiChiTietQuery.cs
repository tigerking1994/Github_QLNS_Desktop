using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtNcNhuCauChiChiTietQuery
    {
        public Guid? iID_DuAnId { get; set; }
        public string sTenDuAn { get; set; }
        public string sLoaiThanhToan { get; set; }
        public Guid? iID_LoaiCongTrinhId { get; set; }
        public double fKeHoachVonNam { get; set; }
        public double fGiaTriDeNghi { get; set; }
        public double fThanhToanKLHTQuyTruoc { get; set; }
        public double fThanhToanTamUngQuyTruoc { get; set; }
        public double fTongQuyTruoc
        {
            get
            {
                return fThanhToanKLHTQuyTruoc + fThanhToanTamUngQuyTruoc;
            }
        }
        public double fThanhToanKLHTQuyNay { get; set; }
        public double fThanhToanTamUngQuyNay { get; set; }
        public double fThuHoiUng { get; set; }
        public double fTongQuyNay
        {
            get
            {
                return fThanhToanKLHTQuyNay + fThanhToanTamUngQuyNay - fThuHoiUng;
            }
        }

        public double fSoConGiaiNganNam
        {
            get
            {
                return fKeHoachVonNam - fTongQuyTruoc - fTongQuyNay;
            }
        }

        public string sGhiChu { get; set; }
        public string sMaDuAn { get; set; }
    }
}
