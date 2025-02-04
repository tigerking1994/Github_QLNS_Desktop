using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportKhtmDuToanBHYTQuery
    {
        public Guid? IIdMlnsCha { get; set; }
        public Guid IIdMlns { get; set; }
        public string IdDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string sXauNoiMa { get; set; }
        public int? STT { get; set; }
        public bool? BHangCha { get; set; }
        public string sM { get; set; }
        public double TNQNDuToan { get; set; }
        public double TNCNVQPDuToan { get; set; }
        public double TongDuToan { get; set; }
        public double TNQNHachToan { get; set; }
        public double TNCNVQPHachToan { get; set; }
        public double TongHachToan { get; set; }
        public double TongCongThanNhan { get; set; }

        public double HSSV { get; set; }
        public double LuuHS { get; set; }
        public double TongHSSV { get; set; }
        public double HVQS { get; set; }
        public double SQDuBi { get; set; }
        public double TongCongHSSV { get; set; }
        public bool HasData => TNQNDuToan != 0 || TNCNVQPDuToan != 0 || TNQNHachToan != 0 || TNCNVQPHachToan != 0
            || HSSV != 0 || LuuHS != 0 || TongHSSV != 0 || HVQS != 0 || SQDuBi != 0;
    }
}
