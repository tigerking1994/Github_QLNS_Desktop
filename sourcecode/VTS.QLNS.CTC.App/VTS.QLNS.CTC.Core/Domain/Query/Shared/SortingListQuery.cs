using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query.Shared
{
    public class SortingListQuery<TKey>
    {
        public virtual TKey Key { get; set; }
        public virtual string SortKey { get; set; }
        public virtual string SortKeyConverter { get; set; }
    }
}
