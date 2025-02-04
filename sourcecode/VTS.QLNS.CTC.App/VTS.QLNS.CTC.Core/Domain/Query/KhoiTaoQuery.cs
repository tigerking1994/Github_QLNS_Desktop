using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class KhoiTaoQuery
    {
        public Guid Id { get; set; }
        public int NamKhoiTao { get; set; }
        public Guid? DuAnId { get; set; }
        public string TenDuAn { get; set; }
        public string DonViId { get; set; }
        public string TenDonVi { get; set; }
        public double? TongMucDauTuPheDuyet { get; set; }
        public double? KHVonBoTriHetNamTruoc { get; set; }
        public double? LuyKeThanhToanKLHT { get; set; }
        public double? LuyKeThanhToanTamUng { get; set; }
        public double? VonThanhToanKLHT { get; set; }
        public double? CheDoChuaThuHoi { get; set; }
    }
}
