using System;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhQtQuyetToanDahtNguonVon
    {
        public Guid Id { get; set; }
        public Guid? IIdDeNghiQuyetToanDahtId { get; set; }
        public Guid? IIdNguonVonId { get; set; }
    }
}
