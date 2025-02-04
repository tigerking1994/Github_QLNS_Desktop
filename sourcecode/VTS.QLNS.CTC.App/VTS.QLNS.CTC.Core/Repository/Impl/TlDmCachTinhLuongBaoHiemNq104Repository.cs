using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCachTinhLuongBaoHiemNq104Repository : Repository<TlDmCachTinhLuongBaoHiemNq104>, ITlDmCachTinhLuongBaoHiemNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCachTinhLuongBaoHiemNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCachTinhLuongBaoHiemNq104 FindByMaCot(string maCot)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCachTinhLuongBaoHiemsNq104.Where(x => x.MaCot == maCot).FirstOrDefault();
            }
        }
    }
}
