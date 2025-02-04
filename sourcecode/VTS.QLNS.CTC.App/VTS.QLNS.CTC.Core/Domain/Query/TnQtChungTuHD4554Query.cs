using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TnQtChungTuHD4554Query
    {
        public Guid Id { get; set; }
        public int? IThangQuy { get; set; }
        public int? IThangQuyLoai { get; set; }
        public string SThangQuyMoTa { get; set; }
        public bool? BDaTongHop { get; set; }
        public string STongHop { get; set; }
        public double? FTongSoTien { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public string SSoChungTu { get; set; }
        public bool? BKhoa { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SMoTa { get; set; }
        public bool? BSent { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public string SDSLNS { get; set; }
        public int? INguonNganSach { get; set; }
        public int? INamNganSach { get; set; }
    }
}
