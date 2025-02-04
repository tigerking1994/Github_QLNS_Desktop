using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhDmCoSoYTeRepository : Repository<BhDmCoSoYTe>, IBhDmCoSoYTeRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhDmCoSoYTeRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhDmCoSoYTe> FindByCondition(Expression<Func<BhDmCoSoYTe, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDmCoSoYTes.Where(predicate)
                                       .OrderBy(x => x.IIDMaCoSoYTe)
                                       .ToList();
            }
        }

        public List<BhDmCoSoYTe> GetListCoSoYTe(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDmCoSoYTes.AsNoTracking().AsEnumerable().Where(x => x.INamLamViec == namLamViec).OrderBy(x => x.IIDMaCoSoYTe).ToList();
            }
        }

        public BhDmCoSoYTe GetCSYTByMa(string maCSYT, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDmCoSoYTes.Where(x => x.INamLamViec == namLamViec && x.IIDMaCoSoYTe == maCSYT).FirstOrDefault();
            }

        }

        public bool ExistCSYT(string maCSYT, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDmCoSoYTes.Any(t => t.IIDMaCoSoYTe == maCSYT && t.INamLamViec == namLamViec);
            }
        }

        public int AddOrUpdateRange(IEnumerable<BhDmCoSoYTe> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in listEntities)
                {
                    var tracked = ctx.BhDmCoSoYTes.AsNoTracking().FirstOrDefault(i => i.Id.Equals(entity.Id));
                    if (entity.IsDeleted)
                    {
                        if (tracked != null)
                        {
                            ctx.Remove(tracked);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        if (tracked != null)
                        {
                            tracked.IIDMaCoSoYTe = entity.IIDMaCoSoYTe;
                            tracked.STenCoSoYTe = entity.STenCoSoYTe;
                            tracked.INamLamViec = authenticationInfo.YearOfWork;
                            tracked.ITrangThai = entity.ITrangThai;
                            tracked.SNguoiSua = authenticationInfo.Principal;
                            tracked.DNgaySua = time;
                            ctx.Update(tracked);
                        }
                        else
                        {
                            entity.Id = Guid.NewGuid();
                            entity.INamLamViec = authenticationInfo.YearOfWork;
                            entity.SNguoiTao = authenticationInfo.Principal;
                            entity.DNgayTao = time;
                            ctx.Set<BhDmCoSoYTe>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }

        public IEnumerable<BhDmCoSoYTe> FindAll(AuthenticationInfo authenticationInfo)
        {
            var result = FindAll().Where(x => x.INamLamViec == authenticationInfo.YearOfWork).OrderBy(s => s.IIDMaCoSoYTe);
            return result.ToList();
        }
    }
}
