using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportTinhLuongQuyetToanQuanSoQuery
    {
        public string Id_DonVi { get; set; }
        public string TenDonVi { get; set; }
        public string MoTa { get; set; }
        public double? RThieuUy { get; set; }
        public double? RTrungUy { get; set; }
        public double? RThuongUy { get; set; }
        public double? RDaiUy { get; set; }
        public double? RThieuTa { get; set; }
        public double? RTrungTa { get; set; }
        public double? RThuongTa { get; set; }
        public double? RDaiTa { get; set; }
        public double? RTuong { get; set; }
        public double? RTsq { get; set; }
        public double? RBinhNhi { get; set; }
        public double? RBinhNhat { get; set; }
        public double? RHaSi { get; set; }
        public double? RTrungSi { get; set; }
        public double? RThuongSi { get; set; }
        public double? RQncn { get; set; }
        public double? RVcqp { get; set; }
        public double? RCnvqp { get; set; }
        public double? RLdhd { get; set; }
    }
}
