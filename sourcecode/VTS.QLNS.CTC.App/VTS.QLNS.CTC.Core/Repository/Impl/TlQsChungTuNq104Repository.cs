using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlQsChungTuNq104Repository : Repository<TlQsChungTuNq104>, ITlQsChungTuNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlQsChungTuNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.TlQsChungTuNq104s.Where(x => x.Nam == namLamViec).OrderByDescending(n => n.SoChungTu).ToList();
                if (result.Count <= 0) return 1;
                var indexString = result.FirstOrDefault().SoChungTu.Substring(3, 3);
                try
                {
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
