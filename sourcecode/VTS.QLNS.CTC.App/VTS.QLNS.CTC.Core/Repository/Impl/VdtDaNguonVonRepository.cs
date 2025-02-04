using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaNguonVonRepository : Repository<VdtDaNguonVon>, IVdtDaNguonVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaNguonVonRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtDaNguonVon> FindByIdDuAn(List<Guid?> lstIdDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaNguonVons.Where(x => lstIdDuAn.Contains(x.IIdDuAn)).ToList();
            }    
        }

        public IEnumerable<VdtDaNguonVon> FindByIdDuAn(Guid idDuAn)
        {
            using(ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaNguonVons.Where(x => x.IIdDuAn == idDuAn).ToList();
            }    
        }

        public IEnumerable<VdtDaNguonVon> FindByNguonVon(Guid idDuAn, int nguonVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaNguonVons.Where(x => x.IIdDuAn == idDuAn && x.IIdNguonVonId == nguonVon).ToList();
            }
        }
    }
}
