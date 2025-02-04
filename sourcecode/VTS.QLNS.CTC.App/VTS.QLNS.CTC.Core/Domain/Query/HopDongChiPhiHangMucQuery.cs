using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class HopDongChiPhiHangMucQuery
    {
        public Guid IdHopDongGoiThauNhaThau { get; set; }
        [Column("iID_GoiThau_HangMucID")]
        public Guid IdGoiThauHangMuc { get; set; }
        [Column("iID_GoiThauID")]
        public Guid? GoiThauId { get; set; }
        [Column("iID_HangMucID")]
        public Guid? HangMucId { get; set; }
        [Column("iID_ChiPhiID")]
        public Guid? ChiPhiId { get; set; }
        [Column("iID_ParentID")]
        public Guid? IID_ParentID { get; set; }
        [Column("fTienGoiThau")]
        public double? FTienGoiThau { get; set; }
        [Column("fGiatriSuDung")]
        public double? FGiatriSuDung { get; set; }
        [Column("fGiaTriConLai")]
        public double? FGiaTriConLai { get; set; }
        [Column("fTienCoTheSD")]
        public double? FTienCoTheSD { get; set; }
        [Column("maOrder")]
        public string MaOrDer { get; set; }
        [Column("sTenHangMuc")]
        public string STenHangMuc { get; set; }
        [Column("giatritruocdc")]
        public double? FGiaTriTruocDC { get; set; }
    }
}
