using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDmChiPhiRepository : Repository<VdtDmChiPhi>, IVdtDmChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDmChiPhiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtDmChiPhi> FindAll(AuthenticationInfo authenticationInfo)
        {
            return FindAll();
        }

        public int UpdateVdtDmChiPhi(IEnumerable<VdtDmChiPhi> entities)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    if (entity.IsDeleted)
                    {
                        if (!entity.IIdChiPhi.Equals(Guid.Empty))
                        {
                            var tracked = ctx.Set<VdtDmChiPhi>().Find(entity.IIdChiPhi);
                            ctx.Remove<VdtDmChiPhi>(tracked);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        if (!entity.IIdChiPhi.Equals(Guid.Empty))
                        {
                            ctx.Update(entity);
                        }
                        else
                        {
                            ctx.Set<VdtDmChiPhi>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }
    }
}
