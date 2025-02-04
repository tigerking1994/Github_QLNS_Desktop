using Microsoft.EntityFrameworkCore;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlBangLuongKeHoachBridgeNq104Repository : Repository<TlBangLuongKeHoachBridgeNq104>, ITlBangLuongKeHoachBridgeNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlBangLuongKeHoachBridgeNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [TL_BangLuong_KeHoach_Bridge_NQ104]");
            }
        }
    }
}
