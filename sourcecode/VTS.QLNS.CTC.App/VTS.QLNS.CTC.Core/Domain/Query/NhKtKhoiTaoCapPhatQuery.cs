using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhKtKhoiTaoCapPhatQuery
    {
        public Guid Id { get; set; }
        public DateTime? DNgayKhoiTao { get; set; }
        public int? INamKhoiTao { get; set; }
        public Guid? IIdDonViID { get; set; }
        public string IIdMaDonVi { get; set; }
        public Guid? IIdTiGiaID { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsKhoa { get; set; }
        public Guid? IIdTongHopID { get; set; }
        public string STongHop { get; set; }
        public bool BIsXoa { get; set; }

        // Another properties
        public string STenDonVi { get; set; }
    }
}
