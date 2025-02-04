using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SysFunctionRepository : Repository<HtChucNang>, ISysFunctionRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SysFunctionRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<HtChucNang> FindAllWithAuthorties()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.HtChucNangs.Where(s => s.ITrangThai).Include(s => s.SysFunctionAuthorities).ThenInclude(s => s.HTQuyen).ToList();
            }
        }

        public HtChucNang FindOneWithAuthorities(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.HtChucNangs.Where(s => s.Id.Equals(id)).Include(s => s.SysFunctionAuthorities).ThenInclude(s => s.HTQuyen).FirstOrDefault();
            }
        }
    }
}
