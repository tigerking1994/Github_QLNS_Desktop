using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlPhuCapDieuChinhNq104Repository : Repository<TlPhuCapDieuChinhNq104>, ITlPhuCapDieuChinhNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlPhuCapDieuChinhNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<TlPhuCapDieuChinhNq104Query> FindAllPhuCapDieuChinh()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<TlPhuCapDieuChinhNq104Query>("EXECUTE dbo.sp_tl_dieuchinh_phucap_nq104").ToList();
            }
        }
    }
}
