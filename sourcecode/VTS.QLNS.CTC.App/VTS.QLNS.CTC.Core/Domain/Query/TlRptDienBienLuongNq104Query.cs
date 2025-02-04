using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlRptDienBienLuongNq104Query
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaCanBo { get; set; }
        public string TenCanBo { get; set; }
        public string TenDonVi { get; set; }
        public string MaDonVi { get; set; }
        public decimal LhtTt { get; set; }
        public decimal PctnvkTt { get; set; }
        public decimal HsblTt { get; set; }
        public decimal PctnTt { get; set; }
        public decimal PccvTt { get; set; }
        public decimal PccovTt { get; set; }
        public decimal PctraSum { get; set; }
        public decimal PcdacthuSum { get; set; }
        public decimal PckhacSum { get; set; }
        public decimal BhcnTt { get; set; }
        public decimal ThuetncnTt { get; set; }
        public decimal TaTt { get; set; }
        public decimal ThanhTien { get; set; }
        public string MaHieuCanBo { get; set; }
    }
}
