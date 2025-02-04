using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCachTinhLuongTruyLinhNq104Repository : Repository<TlDmCachTinhLuongTruyLinhNq104>, ITlDmCachTinhLuongTruyLinhNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCachTinhLuongTruyLinhNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCachTinhLuongTruyLinhNq104 FindByMaCot(string maCot)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCachTinhLuongTruyLinhsNq104.Where(x => x.MaCot == maCot).FirstOrDefault();
            }
        }
    }
}
