using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class ImpHistory : EntityBase
    {
        public Guid Id { get; set; }
        public string ServiceCode { get; set; }
        public string TableName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserModifier { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
