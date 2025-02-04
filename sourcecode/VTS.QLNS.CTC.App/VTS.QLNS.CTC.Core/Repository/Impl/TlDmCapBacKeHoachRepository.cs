using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCapBacKeHoachRepository : Repository<TlDmCapBacKeHoach>, ITlDmCapBacKeHoachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCapBacKeHoachRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCapBacKeHoach FindByMaCb(string maCb)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacKeHoaches.FirstOrDefault(x => x.MaCb.Equals(maCb));
            }
        }

        public TlDmCapBacKeHoach FindByMaCbAndHsl(string maCb, decimal? hsl)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacKeHoaches.FirstOrDefault(x => x.MaCb.Equals(maCb) && x.LhtHs == hsl);
            }
        }

        public TlDmCapBacKeHoach FindByMaCbAndHslAndNhom(string maCb, decimal? hsl, string nhom)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacKeHoaches.FirstOrDefault(x => x.MaCb.Equals(maCb) && x.LhtHs == hsl && x.Nhom.Equals(nhom));
            }
        }

        public int CountByYear(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacKeHoaches.Count(t => year == t.NamLamViec);
            }
        }

    }
}
