using Microsoft.EntityFrameworkCore;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlCanBoPhuCapBridgeNq104Repository : Repository<TlCanBoPhuCapBridgeNq104>, ITlCanBoPhuCapBridgeNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlCanBoPhuCapBridgeNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [TL_CanBo_PhuCap_Bridge_NQ104]");
            }
        }
    }
}
