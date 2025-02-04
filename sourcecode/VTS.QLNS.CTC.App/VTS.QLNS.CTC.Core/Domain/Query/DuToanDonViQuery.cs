using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class DuToanDonViQuery
    {
        public Guid? iID_MLNS { get; set; }
        public Guid? iID_MLNS_Cha { get; set; }
        public string sLns { get; set; }
        public string sL { get; set; }
        public string sK { get; set; }
        public string sM { get; set; }
        public string sTm { get; set; }
        public string sTtm { get; set; }
        public string sNg { get; set; }
        public string sTng { get; set; }
        public string sXauNoiMa { get; set; }
        public string sMota { get; set; }
        public double TuChi { get; set; }
        public double HienVat { get; set; }
        public double RutKBNN { get; set; }
        public bool IsHangCha {get;set;}
    }
}
