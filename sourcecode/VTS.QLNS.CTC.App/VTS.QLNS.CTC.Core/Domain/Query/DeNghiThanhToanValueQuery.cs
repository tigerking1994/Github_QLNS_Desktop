using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class DeNghiThanhToanValueQuery
    {
        public double ThanhToanTN { get; set; }
        public double ThanhToanNN { get; set; }
        public double ThuHoiUngTN { get; set; }
        public double ThuHoiUngNN { get; set; }
        public double TamUngTN { get; set; }
        public double TamUngNN { get; set; }
        public double ThuHoiUngUngTruocTN { get; set; }
        public double ThuHoiUngUngTruocNN { get; set; }
        public double TamUngUngTruocTN { get; set; }
        public double TamUngUngTruocNN { get; set; }

        public double fLuyKeThanhToanTN { get; set; }
        public double fLuyKeTUChuaThuHoiTN { get; set; }
        public double fLuyKeTUChuaThuHoiKhacTN { get; set; }
        public double fLuyKeThanhToanNN { get; set; }
        public double fLuyKeTUChuaThuHoiNN { get; set; }
        public double fLuyKeTUChuaThuHoiKhacNN { get; set; }
        public double fLyKeTN { get; set; }
        public double fLuyKeNN { get; set; }
        public double ThuHoiTN { get; set; }
        public double ThuHoiNN { get; set; }
    }
}
