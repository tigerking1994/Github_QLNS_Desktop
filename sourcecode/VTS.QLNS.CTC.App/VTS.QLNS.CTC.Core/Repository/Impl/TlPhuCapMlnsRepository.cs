using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlPhuCapMlnsRepository : Repository<TlPhuCapMln>, ITlPhuCapMlnsRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlPhuCapMlnsRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int CountByYear(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlPhuCapMlns.Count(t => year == t.Nam);
            }
        }
    }
}
