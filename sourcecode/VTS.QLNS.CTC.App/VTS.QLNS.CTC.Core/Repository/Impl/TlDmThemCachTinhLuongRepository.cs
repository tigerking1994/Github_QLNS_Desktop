using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmThemCachTinhLuongRepository : Repository<TlDmThemCachTinhLuong>, ITlDmThemCachTinhLuongRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmThemCachTinhLuongRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmThemCachTinhLuong FindByMaCachTinhLuong(string maCachTinhLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmThemCachTinhLuongs.Where(x => x.MaThemCachTl.Equals(maCachTinhLuong)).FirstOrDefault();
            }
        }
    }
}
