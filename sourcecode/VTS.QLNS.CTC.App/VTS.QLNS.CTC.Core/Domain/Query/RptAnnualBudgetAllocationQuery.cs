using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class RptAnnualBudgetAllocationQuery
    {
        public string iID_MaDonViQuanLy { get; set; }
        public string sTenDonVi { get; set; }
        public double? fCapPhatNamTruoc { get; set; }
        public double? fChiTieuNamTruoc { get; set; }
        public double? fQuyetToan { get; set; }
        public double? fThongBaoDauNam { get; set; }
        public double? fTongBoXung { get; set; }
        public double? fCapPhatNamNay { get; set; }
        public double? fTamUng { get; set; }
    }
}
