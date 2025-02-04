using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlPhuCapDieuChinhRepository : Repository<TlPhuCapDieuChinh>, ITlPhuCapDieuChinhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlPhuCapDieuChinhRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<TlPhuCapDieuChinhQuery> FindAllPhuCapDieuChinh()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<TlPhuCapDieuChinhQuery>("EXECUTE dbo.sp_tl_dieuchinh_phucap").ToList();
            }
        }
    }
}
