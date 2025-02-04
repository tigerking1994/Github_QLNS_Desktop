using System;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class Nc3YChungTuChiTietCriteria
    {
        public Guid SktChungTuId { get; set; }
        public int NamLamViec { get; set; }
        public int NamNganSach { get; set; }
        public int NguonNganSach { get; set; }
        public int ILoai { get; set; }
        public int ILoaiNguonNganSach { get; set; }
        public string IdDonVi { get; set; }
        public string CurrentIdDonVi { get; set; }
        public int ITrangThai { get; set; }
        public string IdDonViTao { get; set; }
        public int LoaiChungTu { get; set; }
        public int HienThi { get; set; }
        public Guid IdMucLucSkt { get; set; }
        public string IdDonViSearch { get; set; }
        public string UserName { get; set; }
        public int IsViewDetailSummary { get; set; }
        public string ChuyenNganh { get; set; }
        public string IdDonViFilter { get; set; }
        public List<Guid> lstSktChungTuId { get; set; }
    }
}
