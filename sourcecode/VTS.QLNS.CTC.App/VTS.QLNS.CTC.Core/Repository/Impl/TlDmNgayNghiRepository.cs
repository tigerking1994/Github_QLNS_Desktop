using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmNgayNghiRepository : Repository<TlDmNgayNghi>, ITlDmNgayNghiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public TlDmNgayNghiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<TlDmNgayNghi> FindByYear(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lst = ctx.TlDmNgayNghis.Where(x => x.INamLamViec == year).ToList();
                return lst;
            }
        }
    }
}
