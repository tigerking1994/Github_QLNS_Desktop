using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmCapBacKeHoachNq104 : EntityBase
    {
        public string MaCb { get; set; }
        public string TenCb { get; set; }
        public bool? Splits { get; set; }
        public string Parent { get; set; }
        public bool? Readonly { get; set; }
        public string MoTa { get; set; }
        public int? ThoiHanTang { get; set; }
        public string MaCbKeHoach { get; set; }
        public string TenCbKeHoach { get; set; }
        public int? TuoiHuuNam { get; set; }
        public int? TuoiHuuNu { get; set; }
        public double? PcrqTt { get; set; }
        public int? NamLamViec { get; set; }
        public string Loai { get; set; }
        public string Nhom { get; set; }
        public decimal? HsVk { get; set; }
        public string MaBacLuong { get; set; }
        public string MaBacLuongKeHoach { get; set; }
        public string MaBacLuongTran { get; set; }
        public string LoaiNhom { get; set; }
    }
}
