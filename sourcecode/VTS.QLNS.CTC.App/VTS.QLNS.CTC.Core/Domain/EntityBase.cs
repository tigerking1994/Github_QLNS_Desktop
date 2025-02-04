using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class EntityBase
    {
        [Key]
        [Column("Id")]
        public virtual Guid Id { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
        [NotMapped]
        public bool IsModified { get; set; }
        [NotMapped]
        public bool IsAdded { get; set; }
    }
}
