using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmTangGiamRepository : Repository<TlDmTangGiam>, ITlDmTangGiamRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmTangGiamRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmTangGiam FindByMaTangGiam(string maTangGiam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmTangGiams.Where(x => x.MaTangGiam == maTangGiam).FirstOrDefault();
            }
        }
    }
}
