using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmChiPhiRepository : Repository<NhDmChiPhi>, INhDmChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmChiPhiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDmChiPhi> FindAll(AuthenticationInfo authenticationInfo)
        {
            return FindAll();
        }

        public int UpdateVdtDmChiPhi(IEnumerable<NhDmChiPhi> entities)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    if (entity.IsDeleted)
                    {
                        if (!entity.IIdChiPhi.Equals(Guid.Empty))
                        {
                            var tracked = ctx.Set<NhDmChiPhi>().Find(entity.IIdChiPhi);
                            ctx.Remove<NhDmChiPhi>(tracked);
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
                            ctx.Set<NhDmChiPhi>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }
    }
}
