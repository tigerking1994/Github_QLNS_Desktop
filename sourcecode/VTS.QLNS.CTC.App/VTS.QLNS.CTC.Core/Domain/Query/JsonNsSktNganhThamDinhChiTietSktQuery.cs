using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class JsonNsSktNganhThamDinhChiTietSktQuery
    {
        public Guid Id { get; set; }
        public Guid IIdCtsoKiemTra { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public Guid IIdMucLuc { get; set; }
        public string SM { get; set; }
        public string SMoTa { get; set; }
        public string SGhiChu { get; set; }
        public int INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public double? FTuChi { get; set; }
        public double? FSuDungTonKho { get; set; }
        public double? FChiDacThuNganhPhanCap { get; set; }
        public string SKyHieu { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }
    }
}
