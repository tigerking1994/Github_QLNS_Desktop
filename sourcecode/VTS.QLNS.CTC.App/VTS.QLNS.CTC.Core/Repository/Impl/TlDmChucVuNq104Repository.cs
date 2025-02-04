using System.Linq;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmChucVuNq104Repository : Repository<TlDmChucVuNq104>, ITlDmChucVuNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmChucVuNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmChucVuNq104 FindByMaChucVu(string maChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmChucVuNq104s.Where(x => x.Ma == maChucVu).FirstOrDefault();
            }
        }

        public TlDmChucVuNq104 FindByHeSoChucVu(decimal? heSoCv)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmChucVuNq104s.FirstOrDefault();
            }
        }
    }
}
