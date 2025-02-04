using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlCBNHSaoChepNamKeHoachModel : ModelBase
    {
        public int Month { get; set; }
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public TlDmDonViModel DonVi { get; set; }
        public TlDmDonViNq104Model DonVinq104 { get; set; }
    }
}
