using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SktMucLucMapRepository : Repository<NsMlsktMlns>, ISktMucLucMapRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SktMucLucMapRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteBySktMucLucKyHieu(string kyHieu, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                IEnumerable<NsMlsktMlns> sktMucLucMaps = ctx.NsMlsktMlns.Where(t => t.SSktKyHieu.Equals(kyHieu) && t.INamLamViec == namLamViec).ToList();
                ctx.NsMlsktMlns.RemoveRange(sktMucLucMaps);
                ctx.SaveChanges();
            }
        }
    }
}
