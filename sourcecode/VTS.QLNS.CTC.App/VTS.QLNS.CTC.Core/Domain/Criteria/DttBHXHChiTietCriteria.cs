using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class DttBHXHChiTietCriteria
    {
        public Guid DtttBhxhId { get; set; }
        public int NamLamViec { get; set; }
        public int NamNganSach { get; set; }
        public int ILoai { get; set; }
        public string IdDonVi { get; set; }
        public string CurrentIdDonVi { get; set; }
        public int ITrangThai { get; set; }
        public string IdDonViTao { get; set; }
        public int LoaiChungTu { get; set; }
        public int HienThi { get; set; }
        public string IdDonViSearch { get; set; }
        public string UserName { get; set; }
        public int IsViewDetailSummary { get; set; }
        public string ChuyenNganh { get; set; }
        public string IdDonViFilter { get; set; }
        public string IdChungTu { get; set; }
        public List<string> LstLns { get; set; }
    }
}
