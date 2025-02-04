using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class ReportKhcTongHopBHXHCriteria
    {
        public double STT { get; set; }
        public string SMota { get; set; }
        public double TroCapOmDau { get; set; }
        public double TroCapThaiSan { get; set; }
        public double TroCapTLLĐNN { get; set; }
        public double TroCapHuuTri { get; set; }
        public double TroCapPhucVien { get; set; }
        public double TroCapXuatNgu { get; set; }
        public double TroCapThoiViec { get; set; }
        public double TroCapTuTuat { get; set; }
        public double Cong { get; set; }
        public bool IsHangCha { get; set; }
    }
}
