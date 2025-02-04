using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query.Shared
{
    public class LookupQuery<Tkey, TValue>
    {
        public LookupQuery()
        {

        }

        public LookupQuery(Tkey id, TValue displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        public virtual Tkey Id { get; set; }
        public virtual TValue DisplayName { get; set; }
    }
}
