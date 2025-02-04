using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptQuyetToanChungTuTongHop
    {
        public List<SettlementVoucherDetailModel> Items { get; set; }
        public List<NsMucLucNganSach> MLNS { get; set; }
        public string DonVi1 { get; set; }
        public string DonVi2 { get; set; }
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }
        public string ThoiGian { get; set; }
    }
}
