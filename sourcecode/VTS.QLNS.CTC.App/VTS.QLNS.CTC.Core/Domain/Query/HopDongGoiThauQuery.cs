using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class HopDongGoiThauQuery
    {
        [Column("IdHopDongGoiThauNhaThau")]
        public Guid IdHopDongGoiThauNhaThau { get; set; }
        [Column("old_IdHopDonggoiThauNhaThau")]
        public Guid? OldIdHopDongGoiThauNhaThau { get; set; }
        public Guid Id { get; set; }
        [Column("iID_GoiThauID")]
        public Guid GoiThauId { get; set; }
        [Column("iID_DuAnID")]
        public Guid? IIdDuAnId { get; set; }
        //[Column("iID_NhaThauID")]
        //public Guid? IID_NhaThauID { get; set; }
        [Column("sMaGoiThau")]
        public string SMaGoiThau { get; set; }
        [Column("sTenGoiThau")]
        public string STenGoiThau { get; set; }
        [Column("fTienTrungThau")]
        public double? FTienTrungThau { get; set; }
        [Column("fGiaTriConLai")]
        public double? FGiaTriConLai { get; set; }
        [Column("fGiaTriDaSD")]
        public double? FGiaTriDaSD { get; set; }
        [Column("NhaThauId")]
        public Guid? NhaThauId { get; set; }
        [Column("FGiaTriGoiThau")]
        public double FGiaTriGoiThau { get; set; }
        [Column("fGiaTriTrungThau")]
        public double FGiaTriTrungThau { get; set; }
        [Column("fGiaTriTrungThauTruocDC")]
        public double? FGiaTriTrungThauTruocDC { get; set; }
        [Column("fGiaTriHopDong")]
        public double FGiaTriHopDong { get; set; }
        [Column("fGiaTriHopDongTruocDC")]
        public double? FGiaTriHopDongTruocDieuChinh { get; set; }
        public string SThoiGianThucHien { get; set; }
        [Column("sThoiGianThucHien")]
        // public Guid? Old
        [NotMapped]
        public ObservableCollection<HopDongChiPhiQuery> ListChiPhi { get; set; }
    }
}
