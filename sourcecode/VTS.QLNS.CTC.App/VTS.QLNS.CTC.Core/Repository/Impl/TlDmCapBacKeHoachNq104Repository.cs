using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCapBacKeHoachNq104Repository : Repository<TlDmCapBacKeHoachNq104>, ITlDmCapBacKeHoachNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCapBacKeHoachNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCapBacKeHoachNq104 FindByMaCb(string maCb)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacKeHoachNq104s.FirstOrDefault(x => x.MaCb.Equals(maCb));
            }
        }

        public TlDmCapBacKeHoachNq104 FindByMaCbAndHsl(string maCb, decimal? hsl)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacKeHoachNq104s.FirstOrDefault(x => x.MaCb.Equals(maCb));
            }
        }

        public TlDmCapBacKeHoachNq104 FindByMaCbAndHslAndNhom(string maCb, decimal? hsl, string nhom)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacKeHoachNq104s.FirstOrDefault(x => x.MaCb.Equals(maCb) && x.Nhom.Equals(nhom));
            }
        }

        public int CountByYear(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacKeHoachNq104s.Count(t => year == t.NamLamViec);
            }
        }
    }
}
