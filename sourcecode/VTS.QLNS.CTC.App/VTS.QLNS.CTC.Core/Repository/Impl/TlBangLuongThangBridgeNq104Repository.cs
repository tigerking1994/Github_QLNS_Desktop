using Microsoft.EntityFrameworkCore;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlBangLuongThangBridgeNq104Repository : Repository<TlBangLuongThangBridgeNq104>, ITlBangLuongThangBridgeNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlBangLuongThangBridgeNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [TL_BangLuong_Thang_Bridge_NQ104]");
            }
        }
    }
}
