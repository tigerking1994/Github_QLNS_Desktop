using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsPhongBanRepository : Repository<DmBQuanLy>, INsPhongBanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsPhongBanRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int countPhongBanByNamLamViec(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsPhongBans.Where(t => namLamViec == t.INamLamViec).Count();
            }
        }
    }
}
