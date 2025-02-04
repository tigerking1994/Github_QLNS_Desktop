using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class AuthorityTypeRepository : Repository<HtLoaiQuyen>, IAuthorityTypeRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public AuthorityTypeRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<HtLoaiQuyen> LoadEagerAuthorityTypes(Expression<Func<HtLoaiQuyen, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.HtLoaiQuyens.Include(s => s.HtQuyens).Where(predicate).ToList();
            }
        }
    }
}
