using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class GoiThauQuery
    {
        public Guid? IdGoiThauNhaThau { get; set; }
        public Guid Id { get; set; }
        public Guid IIDGoiThauID { get; set; }
        public Guid IIDDuAnID { get; set; }
        public string SMaGoiThau { get; set; }
        public string STenGoiThau { get; set; }
        public Guid? IIDGoiThauGocID { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public double FTienTrungThau { get; set; }
        public double FGiaTriGoiThau { get; set; }
        public double FGiaTriTrungThau { get; set; }
        public bool IsChecked { get; set; }
        public double? FGiaTriTrungThauTruocDC { get; set; }
    }
}
