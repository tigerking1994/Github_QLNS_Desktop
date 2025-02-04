using System;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportCapPhatDonViQuery
    {
        public Guid iID_MLNS { get; set; }
        public Guid? iID_MLNS_Cha { get; set; }
        public string sLNS { get; set; }

        public string sL { get; set; }
        public string sK { get; set; }
        public string sM { get; set; }
        public string sTM { get; set; }
        public string sTenDonVi { get; set; }
        public string sXauNoiMa { get; set; }

        public string sTTM { get; set; }
        public string sNG { get; set; }
        public string iID_MaDonVi { get; set; }
        public string sMoTa { get; set; }
        public bool bHangCha { get; set; }
        public double fCapPhat { get; set; }
        public List<CapPhatDonViDynamicModel> LstGiaTri { get; set; }
    }

    public class CapPhatDonViDynamicModel
    {
        public double TongSo { get; set; }
        public string iID_MaDonVi { get; set; }
        public string sTenDonVi { get; set; }
        public string sXauNoiMa { get; set; }
    }
    public class HeaderReportCapPhatDonViQuery
    {
        public int Stt { get; set; }
        public string sMoTa { get; set; }

        public double TongSo { get; set; }
    }
}
