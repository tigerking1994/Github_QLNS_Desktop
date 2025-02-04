using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class QtcQuyKCriteria
    {
        public Guid? ID { get; set; }
        public int? NamLamViec { get; set; }
        public Guid? IDDonVi { get; set; }
        public string IDMaDonVi { get; set; }
        public string LstSoChungTu { get; set; }
        public int LoaiChungTu { get; set; }
        public string SNguoiTao { get; set; }
        public int ITrangThai { get; set; }
        public string SLNS { get; set; }
        public Guid IDLoaiChi { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public int? IQuyChungTu { get; set; }
        public int DonViTinh { get; set; }
    }
}
