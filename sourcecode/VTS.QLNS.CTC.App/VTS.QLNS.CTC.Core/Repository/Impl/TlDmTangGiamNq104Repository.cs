using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmTangGiamNq104Repository : Repository<TlDmTangGiamNq104>, ITlDmTangGiamNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmTangGiamNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmTangGiamNq104 FindByMaTangGiam(string maTangGiam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmTangGiamNq104s.Where(x => x.MaTangGiam == maTangGiam).FirstOrDefault();
            }
        }
    }
}
