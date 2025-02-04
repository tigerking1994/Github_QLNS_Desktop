using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCachTinhLuongChuanNq104Repository : Repository<TlDmCachTinhLuongChuanNq104>, ITlDmCachTinhLuongChuanNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCachTinhLuongChuanNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCachTinhLuongChuanNq104 FindByMaCot(string maCot)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCachTinhLuongChuansNq104.Where(x => x.MaCot == maCot).FirstOrDefault();
            }
        }
    }
}
