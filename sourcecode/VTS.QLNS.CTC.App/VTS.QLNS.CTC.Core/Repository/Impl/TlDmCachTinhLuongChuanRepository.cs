using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCachTinhLuongChuanRepository : Repository<TlDmCachTinhLuongChuan>, ITlDmCachTinhLuongChuanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCachTinhLuongChuanRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCachTinhLuongChuan FindByMaCot(string maCot)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCachTinhLuongChuans.Where(x => x.MaCot == maCot).FirstOrDefault();
            }
        }
    }
}
