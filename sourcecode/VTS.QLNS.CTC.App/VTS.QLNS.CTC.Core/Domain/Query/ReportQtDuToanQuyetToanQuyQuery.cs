using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQtDuToanQuyetToanQuyQuery
    {
        [Column("iID_MLNS")]
        public Guid MLNS_Id { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? MLNS_Id_Parent { get; set; }
        [Column("sLNS")]
        public string LNS { get; set; }
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
        [Column("sTNG1")]
        public string TNG1 { get; set; }
        [Column("sTNG2")]
        public string TNG2 { get; set; }
        [Column("sTNG3")]
        public string TNG3 { get; set; }
        [Column("sXauNoiMa")]
        public string XauNoiMa { get; set; }
        [Column("sMoTa")]
        public string MoTa { get; set; }
        public bool IsHangCha { get; set; }
        public bool BHangChaDuToan { get; set; }
        public bool BHangChaQuyetToan { get; set; }
        public double ChiTieu { get; set; }
        public double TuChi { get; set; }
        public double Quy1 { get; set; }
        public double Quy2 { get; set; }
        public double Quy3 { get; set; }
        public double Quy4 { get; set; }
        public string TenDonVi { get; set; }

        [NotMapped]
        public double SoConLai
        {
            get => ChiTieu - TuChi;
        }

        [NotMapped]
        public double TiLe
        {
            get => ChiTieu == 0 ? 0 : TuChi * 100 / ChiTieu;
        }
        [NotMapped]
        public bool HasData => ChiTieu != 0 || TuChi != 0
                            || Quy1 != 0 || Quy2 != 0 || Quy3 != 0 || Quy4 != 0;
        [NotMapped]
        public double ChiTieuOrigin => ChiTieu;
    }
}
