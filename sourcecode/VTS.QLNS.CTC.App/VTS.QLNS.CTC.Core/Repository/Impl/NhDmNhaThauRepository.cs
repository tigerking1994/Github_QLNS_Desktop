using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmNhaThauRepository : Repository<NhDmNhaThau>, INhDmNhaThauRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmNhaThauRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddOrUpdateRange(IEnumerable<NhDmNhaThau> entities, int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    if (entity.IsDeleted)
                    {
                        //if (!entity.Id.Equals(Guid.Empty))
                        //{
                        //    // remove map mlns_mlskt
                        //    IEnumerable<NsMlsktMlns> nsMlsktMlns = ctx.NsMlsktMlns.Where(i => i.INamLamViec == iNamLamViec && i.SNsXauNoiMa.Equals(entity.XauNoiMa));
                        //    ctx.RemoveRange(nsMlsktMlns);
                        //    ctx.Remove(entity);
                        //}
                    }
                    else if (entity.IsModified)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
                            ctx.Update(entity);
                        }
                        else
                        {
                            ctx.Set<NhDmNhaThau>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }
    }
}
