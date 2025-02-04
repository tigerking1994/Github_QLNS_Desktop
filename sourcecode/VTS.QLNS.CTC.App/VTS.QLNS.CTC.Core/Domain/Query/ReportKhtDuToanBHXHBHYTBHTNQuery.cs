using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportKhtDuToanBHXHBHYTBHTNQuery
    {
        [Column("STT")]
        public string STT { get; set; }
        [Column("STenDonVi")]
        public string STenDonVi { get; set; }
        [Column("FTienBHXHNLD")]
        public double? FTienBHXHNLD { get; set; }
        [Column("FTienBHXHNSDLDD")]
        public double? FTienBHXHNSDLDD { get; set; }
        [Column("FTienCongBHXH")]
        public double? FTienCongBHXH => FTienBHXHNLD.GetValueOrDefault(0) + FTienBHXHNSDLDD.GetValueOrDefault(0);
        [Column("FTienBHTNNLD")]
        public double? FTienBHTNNLD { get; set; }
        [Column("FTienBHTNNSDLDD")]
        public double? FTienBHTNNSDLDD { get; set; }
        [Column("FTienCongBHTN")]
        public double? FTienCongBHTN => FTienBHTNNLD.GetValueOrDefault(0) + FTienBHTNNSDLDD.GetValueOrDefault(0);
        [Column("FTienBHYTNLDNLD")]
        public double? FTienBHYTNLDNLD { get; set; }
        [Column("FTienBHYTNSDLDD")]
        public double? FTienBHYTNSDLDD { get; set; }
        [Column("FTienCongBHYTNLD")]
        public double? FTienCongBHYTNLD => FTienBHYTNLDNLD.GetValueOrDefault(0) + FTienBHYTNSDLDD.GetValueOrDefault(0);
        [Column("FTienBHYTQNNLD")]
        public double? FTienBHYTQNNLD { get; set; }
        [Column("FTienBHYTQNNSDNLD")]
        public double? FTienBHYTQNNSDNLD { get; set; }
        [Column("FTienCongBHYTQN")]
        public double? FTienCongBHYTQN => FTienBHYTQNNLD.GetValueOrDefault(0) + FTienBHYTQNNSDNLD.GetValueOrDefault(0);
        [Column("FTienBHYTTNQN")]
        public double? FTienBHYTTNQN { get; set; }
        [Column("FTienBHYTTNCNCNVQP")]
        public double? FTienBHYTTNCNCNVQP { get; set; }
        [Column("FTienCongBHYTTN")]
        public double? FTienCongBHYTTN => FTienBHYTTNQN.GetValueOrDefault(0) + FTienBHYTTNCNCNVQP.GetValueOrDefault(0);
        [Column("FTienCongALL")]
        public double? FTienCongALL => FTienCongBHXH.GetValueOrDefault(0) + FTienCongBHTN.GetValueOrDefault(0) + FTienCongBHYTNLD.GetValueOrDefault(0) + FTienCongBHYTQN.GetValueOrDefault(0) + FTienCongBHYTTN.GetValueOrDefault(0);
        [Column("Type")]
        public int Type { get; set; }
        [Column("bHangCha")]
        public bool? BHangCha { get; set; }
        public int IKhoi { get;set; }
        public bool HasData => FTienBHXHNLD.GetValueOrDefault() != 0 || FTienBHXHNSDLDD.GetValueOrDefault() != 0 || FTienBHTNNLD.GetValueOrDefault() != 0 || FTienBHTNNSDLDD.GetValueOrDefault() != 0
            || FTienBHYTNLDNLD.GetValueOrDefault() != 0 || FTienBHYTNSDLDD.GetValueOrDefault() != 0 || FTienBHYTQNNLD.GetValueOrDefault() != 0 || FTienBHYTQNNSDNLD.GetValueOrDefault() != 0
            || FTienBHYTTNQN.GetValueOrDefault() != 0 || FTienBHYTTNCNCNVQP.GetValueOrDefault() != 0;
    }
}
