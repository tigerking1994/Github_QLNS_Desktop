using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsQsMucLucRepository : Repository<NsQsMucLuc>, INsQsMucLucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsQsMucLucRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int countMLQSByNamLamViec(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQsMucLucs.Where(x => x.INamLamViec == yearOfWork).Count();
            }
        }

        public IEnumerable<NsQsMucLuc> FindByCondition(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQsMucLucs.Where(x => x.INamLamViec == yearOfWork).ToList();
            }
        }

        public NsQsMucLuc FindByMaTangGiam(string maTangGiam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQsMucLucs.FirstOrDefault(x => x.SKyHieu == maTangGiam);
            }
        }

        public NsQsMucLuc FindMaMLNS(Guid MLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQsMucLucs.Where(x => x.IIdMlns == MLNS).FirstOrDefault();
            }
        }
    }
}
