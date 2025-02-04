using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class GroupRepository : Repository<HtNhom>, IGroupRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public GroupRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteGroup(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var group = ctx.HtNhoms.Include(g => g.SysGroupAuthorities).Include(g => g.SysGroupUsers).FirstOrDefault(t => t.Id.Equals(id));
                ctx.Remove(group);
                ctx.SaveChanges();
            }
        }

        public HtNhom FindByPredicate(Expression<Func<HtNhom, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.HtNhoms.Include(g => g.SysGroupAuthorities).ThenInclude(g => g.HTQuyen).Include(g => g.SysGroupUsers).SingleOrDefault(predicate);
            }
        }

        public IEnumerable<HtNhom> LoadEagerGroups(Expression<Func<HtNhom, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.HtNhoms.Include(g => g.SysGroupAuthorities).ThenInclude(ga => ga.HTQuyen)
                    .Include(g => g.SysGroupUsers)
                .Where(predicate).ToList();
            }
        }

        public void UpdateNhom(HtNhom entity)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var tracked = ctx.HtNhoms.Include(g => g.SysGroupAuthorities).FirstOrDefault(n => n.Id.Equals(entity.Id));
                tracked.SysGroupAuthorities.Clear();
                ctx.SaveChanges();
                tracked.SysGroupAuthorities = entity.SysGroupAuthorities;
                ctx.Entry(tracked).CurrentValues.SetValues(entity);
                ctx.SaveChanges();
            }
        }

        public void LockUnLockGroup(Guid id, bool status)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var tracked = ctx.HtNhoms.FirstOrDefault(n => n.Id.Equals(id));
                tracked.BKichHoat = status;
                ctx.SaveChanges();
            }
        }
    }
}
