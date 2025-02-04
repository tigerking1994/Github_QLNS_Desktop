using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DmChuKyRepository : Repository<DmChuKy>, IDmChuKyRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DmChuKyRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void SaveChuKy(DmChuKy dmChuKy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var tracked = ctx.DmChuKies.Find(dmChuKy.Id);
                ctx.Entry(tracked).CurrentValues.SetValues(dmChuKy);
                ctx.SaveChanges();
            }
        }
    }
}
