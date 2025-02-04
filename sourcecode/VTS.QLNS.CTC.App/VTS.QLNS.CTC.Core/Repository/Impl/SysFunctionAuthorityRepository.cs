using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SysFunctionAuthorityRepository : Repository<HtQuyenChucNang>, ISysFunctionAuthorityRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SysFunctionAuthorityRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void RemoveSysFunctionAuthorities(IEnumerable<HtQuyenChucNang> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                if (entities.Count() == 0)
                    return;
                ctx.RemoveRange(entities);
                ctx.SaveChanges();
            }
        }
    }
}
