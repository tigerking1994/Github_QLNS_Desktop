using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlPhuCapMlnsNq104Repository : Repository<TlPhuCapMlnNq104>, ITlPhuCapMlnsNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlPhuCapMlnsNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int CountByYear(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlPhuCapMlnsNq104.Count(t => year == t.Nam);
            }
        }
    }
}
