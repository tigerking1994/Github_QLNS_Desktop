using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDmNhaThauRepository : Repository<VdtDmNhaThau>, IVdtDmNhaThauRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDmNhaThauRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtDmNhaThau> FindAll(AuthenticationInfo authenticationInfo)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmNhaThaus.ToList();
            }
        }

        public int UpdateVdtDmNhaThauRepository(IEnumerable<VdtDmNhaThau> entities)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    if (entity.IsDeleted)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
                            var tracked = ctx.Set<VdtDmNhaThau>().Find(entity.Id);
                            ctx.Remove<VdtDmNhaThau>(tracked);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
                            ctx.Update(entity);
                        }
                        else
                        {
                            ctx.Set<VdtDmNhaThau>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }
    }
}
