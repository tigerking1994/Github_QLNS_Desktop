using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class BhQttmBHYTChiTietCriteria
    {
        public Guid VoucherID { get; set; }
        public string SSoChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string IIDMaDonVi { get; set; }
        public string SMoTa { get; set; }
        public bool BIsKhoa { get; set; }
        public int? IQuyNam { get; set; }
        public DateTime DNgayTao { get; set; }
        public DateTime DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
        public string SDsMlns { get; set; }
        public int HienThi { get; set; }
        public string UserName { get; set; }
        public int IsViewDetailSummary { get; set; }
        public string ChuyenNganh { get; set; }
        public string IdDonViFilter { get; set; }
        public List<Guid> lstQttBhxhId { get; set; }
        public string ListIdChungTuTongHop { get; set; }
        public string IdChungTu { get; set; }
        public bool IsPrintReport { get; set; }
        public int DonViTinh { get; set; }
        public int IQuyNamLoai { get; set; }
        public string SLns { get; set; }
        public string VoucherIds { get; set; }
        public string VoucherId { get; set;}
        public bool IsDonViCha { get; set; }
    }
}
