using System;
using System.Linq;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhDmKinhPhiRepository : Repository<BhDmKinhPhi>, IBhDmKinhPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhDmKinhPhiRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhDmKinhPhi> FindAll(AuthenticationInfo authenticationInfo)
        {
            var result = FindAll().OrderBy(s => s.SapXep);
            return result.ToList();
        }

        public int AddOrUpdateRange(IEnumerable<BhDmKinhPhi> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in listEntities.OrderBy(s => s.SapXep))
                {
                    var tracked = ctx.BhDmKinhPhi.AsNoTracking().FirstOrDefault(i => i.Id.Equals(entity.Id));
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
                            tracked.MaKinhPhi = entity.MaKinhPhi;
                            tracked.TenKinhPhi = entity.TenKinhPhi;
                            tracked.MoTa = entity.MoTa;
                            tracked.SapXep = entity.SapXep;
                            tracked.NamLamViec = authenticationInfo.YearOfWork;
                            tracked.NguoiSua = authenticationInfo.Principal;
                            tracked.NgaySua = time;
                            ctx.Update(tracked);
                        }
                        else
                        {
                            entity.NamLamViec = authenticationInfo.YearOfWork;
                            entity.NguoiTao = authenticationInfo.Principal;
                            entity.NgayTao = time;
                            ctx.Set<BhDmKinhPhi>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }
    }
}
