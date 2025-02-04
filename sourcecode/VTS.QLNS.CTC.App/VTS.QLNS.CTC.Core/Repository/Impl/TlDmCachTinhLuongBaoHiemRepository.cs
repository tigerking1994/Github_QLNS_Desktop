using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCachTinhLuongBaoHiemRepository : Repository<TlDmCachTinhLuongBaoHiem>, ITlDmCachTinhLuongBaoHiemRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCachTinhLuongBaoHiemRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCachTinhLuongBaoHiem FindByMaCot(string maCot)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCachTinhLuongBaoHiems.Where(x => x.MaCot == maCot).FirstOrDefault();
            }
        }
    }
}
