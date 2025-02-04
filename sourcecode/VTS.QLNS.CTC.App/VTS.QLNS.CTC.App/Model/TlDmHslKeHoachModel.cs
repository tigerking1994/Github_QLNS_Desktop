using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmHslKeHoachModel : ModelBase
    {
        public string Ngach { get; set; }
        public decimal? LhtHsKh { get; set; }
        public string MaCb { get; set; }
        public string MoTa { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserModifier { get; set; }
        public DateTime? DateModified { get; set; }
        public string Display => string.Format("{0} - {1} - {2}", LhtHsKh, MoTa, MaCb);
    }
}
