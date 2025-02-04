using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class HopDongChiPhiQuery
    {
        public Guid IdHopDongGoiThauNhaThau { get; set; }
        [Column("sTenChiPhi")]
        public string TenChiPhi { get; set; }
        [Column("iID_GoiThau_ChiPhiID")]
        public Guid? IdGoiThauChiPhi { get; set; }
        [Column("iID_ChiPhiID")]
        public Guid? IdChiPhi { get; set; }
        [Column("fTienGoiThau")]
        public double? GiaTriPheDuyet { get; set; }
        [Column("fTienCoTheSD")]
        public double? FTienCoTheSD { get; set; }
        [Column("ithutu")]
        public string MaOrDer { get; set; }
        [Column("bHangCha")]
        public bool? IsHangCha { get; set; }
        [Column("iID_GoiThauID")]
        public Guid? IdGoiThau { get; set; }
        [Column("fGiaTriConLai")]
        public double? FGiaTriConLai { get; set; }
        [Column("iID_ChiPhi_Parent")]
        public Guid? IdChiPhiParent { get; set; }
        [Column("fGiaTriChiPhi")]
        public double? FGiaTriChiPhi {get;set;}
        [Column("giatritruocdc")]
        public double? FGiaTriTruocDC { get; set; }
        [NotMapped]
        public ObservableCollection<HopDongChiPhiHangMucQuery> ListHangMuc { get; set; }
    }
}
