using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class LbChungTuCanCuDuToanDataQuery
    {
        public Guid MLNSId { get; set; }
        public string XauNoiMa { get; set; }
        public double TuChiTaiNganh { get; set; }
        public double TuChiTaiDonVi { get; set; }
        public double HangNhap { get; set; }
        public double HangMua { get; set; }
        public double PhanCap { get; set; }
        public double DuPhong { get; set; }
    }
}
