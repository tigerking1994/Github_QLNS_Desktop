using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlPhuCapDieuChinhQuery
    {
        public Guid Id { get; set; }
        public Guid? IdPhuCap { get; set; }
        public string MaPhuCap { get; set; }
        public string TenPhuCap { get; set; }
        public decimal? GiaTriCu { get; set; }
        public decimal? GiaTriMoi { get; set; }
        public DateTime? ApDungTu { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
    }
}
