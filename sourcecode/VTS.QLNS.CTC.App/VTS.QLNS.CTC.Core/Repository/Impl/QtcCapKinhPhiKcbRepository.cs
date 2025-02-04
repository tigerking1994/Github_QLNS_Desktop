using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QtcCapKinhPhiKcbRepository : Repository<BhQtCapKinhPhiKcb>, IQtcCapKinhPhiKcbRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QtcCapKinhPhiKcbRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQtCapKinhPhiKcb> FindByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtCapKinhPhiKcbs.Where(x => x.INamLamViec == namLamViec).OrderBy(n => n.SSoChungTu).ToList();
            }
        }

        public int GetVoucherIndex(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhQtCapKinhPhiKcbs.Where(x => x.INamLamViec == yearOfWork).OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    var indexString = result.FirstOrDefault().Substring(4, 3);
                    var index = int.Parse(indexString) + 1;
                    return index;
                }
                catch (Exception)
                {
                    return result.Count + 1;
                }
            }
        }
    }
}
