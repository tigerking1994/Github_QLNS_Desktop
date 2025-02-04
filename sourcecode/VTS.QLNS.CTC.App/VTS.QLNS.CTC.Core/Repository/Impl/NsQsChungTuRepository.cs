using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsQsChungTuRepository : Repository<NsQsChungTu>, INsQsChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsQsChungTuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public NsQsChungTu FindByMonth(int month, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                NsQsChungTu chungTu = ctx.NsQsChungTus.Where(x => x.IThangQuy == month && x.INamLamViec == yearOfWork).FirstOrDefault();
                return chungTu;
            }
            
        }

        public IEnumerable<int> FindMonthOfArmy(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQsChungTus.Where(x => x.INamLamViec == yearOfWork).Select(x => x.IThangQuy).Distinct().OrderBy(x => x).ToList();
            }
        }
    }
}
