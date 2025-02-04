using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class KHLCNhaThauDetailQuery
    {
        public Guid? Id { get; set; }
        public Guid? iID_DuAnID { get; set; }
        public DateTime? dNgayLap { get; set; }
        public string sMaGoiThau { get; set; }
        public string sTenGoiThau { get; set; }
        public string sHinhThucChonNhaThau { get; set; }
        public string sPhuongThucDauThau { get; set; }
        public DateTime? dBatDauChonNhaThau { get; set; }
        public string sHinhThucHopDong { get; set; }
        public int? iThoiGianThucHien { get; set; }
        public double? fTienTrungThau { get; set; }
    }
}
