using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportMLNSQuery
    {
        [Column("iID_MLNS")]
        public Guid MlnsId { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? MlnsIdParent { get; set; }
        [Column("sLNS")]
        public string Lns { get; set; }
        [Column("sL")]
        public string L { get; set; }
        [Column("sK")]
        public string K { get; set; }
        [Column("sM")]
        public string M { get; set; }
        [Column("sTM")]
        public string TM { get; set; }
        [Column("sTTM")]
        public string TTM { get; set; }
        [Column("sNG")]
        public string NG { get; set; }
        [Column("sTNG")]
        public string TNG { get; set; }
        public int NamLamViec { get; set; }

        [Column("sMoTa")]
        public string MoTa { get; set; }
        [Column("sXauNoiMa")]
        public string XauNoiMa { get; set; }
        [Column("BHangCha")]
        public bool BHangCha { get; set; }
        [Column("iTrangThai")]
        public int? ITrangThai { get; set; }
        [NotMapped]
        public string TrangThaiDisplay
        {
            get => ITrangThai switch
            {
                0 => "không sử dụng",
                1 => "Đang sử dụng",
                2 => "ngành nghiệp vụ",
                _ => ""
            };
        }

        //Thêmmmmm
        public string TuChi { get; set; }
    }
}
