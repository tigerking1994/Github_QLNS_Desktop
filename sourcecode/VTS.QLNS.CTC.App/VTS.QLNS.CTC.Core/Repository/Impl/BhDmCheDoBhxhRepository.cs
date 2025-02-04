using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhDmCheDoBhxhRepository : Repository<BhDmCheDoBhxh>, IBhDmCheDoBhxhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhDmCheDoBhxhRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public BhDmCheDoBhxh FindByParentId(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDmCheDoBhxh.Where(x => x.IIdCheDoCha.HasValue && x.IIdCheDoCha == id).FirstOrDefault();
            }
        }

        public int AddOrUpdateRange(IEnumerable<BhDmCheDoBhxh> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in listEntities.OrderBy(s => s.IIdMaCheDo))
                {
                    var tracked = ctx.BhDmCheDoBhxh.AsNoTracking().FirstOrDefault(i => i.Id.Equals(entity.Id));
                    if (entity.IsDeleted)
                    {
                        if (tracked != null)
                        {
                            // remove map mlns_mlskt
                            ctx.Remove(tracked);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        //Gán xâu nối mã
                        var xauNoiMaParent = ctx.BhDmCheDoBhxh.FirstOrDefault(s => s.Id.Equals(entity.IIdCheDoCha));
                        if (xauNoiMaParent == null)
                        {
                            entity.SXauNoiMa = entity.IIdMaCheDo;
                        }
                        else
                        {
                            entity.SXauNoiMa = $"{xauNoiMaParent.SXauNoiMa}-{entity.IIdMaCheDo}";
                        }

                        if (tracked != null)
                        {
                            entity.SNguoiSua = authenticationInfo.Principal;
                            entity.DNgaySua = time;
                            ctx.Update(entity);
                        }
                        else
                        {
                            entity.SNguoiTao = authenticationInfo.Principal;
                            entity.DNgayTao = time;
                            ctx.Set<BhDmCheDoBhxh>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }
    }
}
