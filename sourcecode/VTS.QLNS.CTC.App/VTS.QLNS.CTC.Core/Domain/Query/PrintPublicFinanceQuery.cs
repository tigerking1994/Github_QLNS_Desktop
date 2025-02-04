using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class PrintPublicFinanceQuery
    {
        public string IdDonVi { get; set; }
        
        public string TenDonVi { get; set; }
        
        public string XauNoiMa { get; set; }
        
        public string MoTa { get; set; }

        public double GiaTri { get; set; }
    }

    public class PrintPublicFinanceItem
    {
        public string TenDonVi { get; set; }
        
        public double Tong { get; set; }

        public IEnumerable<PublicFinanceItem> Items { get; set; }
    }

    public class PublicFinanceItem
    {
        public string TenDanhMuc { get; set; }
        
        public double Tong { get; set; }
    }

}
