using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportDuToanDauNamSoSanhQuery
    {
        public Guid Id {get; set;}
        public Guid IDMucLuc { get; set;}
        public string STT {get; set;}
        public string MoTa {get; set;}
        public Guid IdParent {get; set;}
        public string STTBC {get; set;}
        public string M {get; set;}
        public bool IsHangCha { get; set; }
        public string KyHieu {get; set;}
        public double QuyetToan {get; set;}
        public double DuToan {get; set;}
        public double TuChi {get; set;}
        public double TuChi2 { get; set; }
        [NotMapped]
        public double Tang
        {
            get
            {
                if ((TuChi2 - TuChi) < 0)
                    return 0;
                return TuChi2 - TuChi;
            }
            set { }
        }

        [NotMapped]
        public double Giam
        {
            get
            {
                if ((TuChi - TuChi2) < 0)
                    return 0;
                return TuChi - TuChi2;
            }
            set { }
        }
    }
}
