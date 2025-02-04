using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExportVonNamDonViModel
    {
        public int iID_NguonVonID { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public List<ExportVonNamDonViQuery> Datas { get; set; }
    }
}
