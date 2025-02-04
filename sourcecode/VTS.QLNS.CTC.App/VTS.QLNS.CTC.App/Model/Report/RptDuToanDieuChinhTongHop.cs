using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptDuToanDieuChinhTongHop
    {
        public List<DcChungTuChiTietModel> Items { get; set; }
        public List<NsMucLucNganSach> MLNS { get; set; }
        public int Count { get; set; }
        public string DonVi1 { get; set; }
        public string DonVi2 { get; set; }
        public string TieuDe1 { get; set; }
        public string TieuDe2 { get; set; }
        public string ThoiGian { get; set; }
        public int QtDauNam { get; set; }
        public int QtCuoiNam { get; set; }
    }
}
