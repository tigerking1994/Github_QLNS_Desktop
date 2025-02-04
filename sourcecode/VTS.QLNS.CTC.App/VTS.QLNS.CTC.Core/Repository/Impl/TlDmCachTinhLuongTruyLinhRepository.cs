using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCachTinhLuongTruyLinhRepository : Repository<TlDmCachTinhLuongTruyLinh>, ITlDmCachTinhLuongTruyLinhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCachTinhLuongTruyLinhRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCachTinhLuongTruyLinh FindByMaCot(string maCot)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCachTinhLuongTruyLinhs.Where(x => x.MaCot == maCot).FirstOrDefault();
            }
        }
    }
}
