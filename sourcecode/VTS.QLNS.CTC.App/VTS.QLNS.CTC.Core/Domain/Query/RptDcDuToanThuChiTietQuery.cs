using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class RptDcDuToanThuChiTietQuery
    {
        [Column("STT")]
        public int STT { get; set; }
        [Column("MaSo")]
        public string MaSo { get; set; }
        [Column("NoiDung")]
        public string NoiDung { get; set; }
        [Column("DttDauNam")]
        public double? DttDauNam { get; set; }
        [Column("Dtt6ThangDauNam")]
        public double? Dtt6ThangDauNam { get; set; }
        [Column("Dtt6ThangCuoiNam")]
        public double? Dtt6ThangCuoiNam { get; set; }
        [Column("TongCong")]
        public double? TongCong { get; set; }
        [Column("Tang")]
        public double? Tang { get; set; }
        [Column("Giam")]
        public double? Giam { get; set; }
        [Column("BHangCha")]
        public bool BHangCha { get; set; }
        [NotMapped]
        public double? FTang
        {
            get
            {
                return TongCong.GetValueOrDefault() > DttDauNam.GetValueOrDefault() ? TongCong.GetValueOrDefault() - DttDauNam.GetValueOrDefault() : 0;
            }
        }
        [NotMapped]
        public double? FGiam
        {
            get
            {
                return TongCong.GetValueOrDefault() < DttDauNam.GetValueOrDefault() ? DttDauNam.GetValueOrDefault() - TongCong.GetValueOrDefault() : 0;
            }
        }
    }
}
