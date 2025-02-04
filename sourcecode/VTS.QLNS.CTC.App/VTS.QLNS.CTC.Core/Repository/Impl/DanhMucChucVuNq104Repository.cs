using System.Linq;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DanhMucChucVuNq104Repository : Repository<TlDmChucVuNq104>, ITLDanhMucChucVuNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DanhMucChucVuNq104Repository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmChucVuNq104 FindByMa(string ma)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmChucVuNq104s.Where(x => x.Ma == ma).FirstOrDefault();
            }
        }
    }
}
