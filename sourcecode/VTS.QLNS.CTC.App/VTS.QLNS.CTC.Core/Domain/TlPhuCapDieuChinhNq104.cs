using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlPhuCapDieuChinhNq104 : EntityBase
    {
        public Guid? IdPhuCap { get; set; }
        public decimal? GiaTriMoi { get; set; }
        public DateTime? ApDungTu { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
    }
}
