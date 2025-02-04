using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCachTinhLuongTruyThuRepository : Repository<TlDmCachTinhLuongTruyThu>, ITlDmCachTinhLuongTruyThuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCachTinhLuongTruyThuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCachTinhLuongTruyThu FindByMaCot(string maCot)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCachTinhLuongTruyThus.Where(x => x.MaCot == maCot).FirstOrDefault();
            }
        }
    }
}
