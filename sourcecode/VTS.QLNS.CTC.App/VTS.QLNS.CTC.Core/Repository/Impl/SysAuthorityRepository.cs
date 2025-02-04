using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SysAuthorityRepository : Repository<HtQuyen>, ISysAuthorityRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SysAuthorityRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<HtQuyen> FindAllWithFunction()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.HtQuyens.Include(s => s.SysFunctionAuthorities).ThenInclude(s => s.HTChucNang).ToList();
            }
        }

        public HtQuyen FindOneWithFunction(string authName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.HtQuyens.Include(s => s.SysFunctionAuthorities).ThenInclude(s => s.HTChucNang).FirstOrDefault(t => t.STenQuyen.Equals(authName));
            }
        }

        public IEnumerable<HtLoaiQuyen> FindAllAuthorTypes()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.HtLoaiQuyens.ToList();
            }
        }

        public void UpdateAuthorities(IEnumerable<HtQuyen> entities)
        {
            Validate(entities);
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    var tracked = ctx.HtQuyens.Include(s => s.SysFunctionAuthorities).Include(s => s.SysGroupAuthorities).FirstOrDefault(t => t.IIdQuyen.Equals(entity.IIdQuyen));
                    if (tracked != null)
                    {
                        ctx.Remove(tracked);
                    }
                    ctx.SaveChanges();
                    if (entity.IsModified)
                    {
                        foreach (var author in entity.SysFunctionAuthorities)
                        {
                            author.IIDMaQuyen = entity.IIDMaQuyen;
                        }
                        if (tracked != null && tracked.SysGroupAuthorities != null)
                        {
                            foreach (var htNhomQuyen in tracked.SysGroupAuthorities)
                            {
                                entity.SysGroupAuthorities.Add(new HtNhomQuyen { IIDMaQuyen = entity.IIDMaQuyen, IIDNhom = htNhomQuyen.IIDNhom });
                            }
                        }
                        ctx.Add(entity);
                    }
                }
                ctx.SaveChanges();
            }
        }

        private void Validate(IEnumerable<HtQuyen> listEntities)
        {
            List<string> maQuyens = listEntities.Select(t => t.IIDMaQuyen).Distinct().ToList();
            if (listEntities.Count() != maQuyens.Count())
            {
                throw new ArgumentException("Mã quyền không được trùng nhau");
            }
            /*using (var ctx = _contextFactory.CreateDbContext())
            {
                bool existedMaQuyen = ctx.HtQuyens.Any(x => maQuyens.Contains(x.IIDMaQuyen));
                if (existedMaQuyen)
                {
                    throw new ArgumentException("Mã quyền đã tồn tại");
                }
            }*/
            foreach (var q in listEntities)
            {
                bool isInValidMaQuyen = !q.IIDMaQuyen.All(c => Char.IsLetterOrDigit(c) || c == '_');
                if (isInValidMaQuyen)
                {
                    throw new ArgumentException("Mã quyền không hợp lệ");
                }
            }
        }
    }
}
