using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCanBoKeHoachNq104Repository : Repository<TlDmCanBoKeHoachNq104>, ITlDmCanBoKeHoachNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCanBoKeHoachNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<TlDmCanBoKeHoachNq104> FindByYear(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoKeHoachNq104s.Where(x => x.Nam == year).ToList();
            }
        }

        public IEnumerable<TlDmCanBoKeHoachNq104> FindLoadIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoKeHoachNq104s.Where(x => x.IsDelete == true).ToList();
            }
        }

        public IEnumerable<TlDmCanBoKeHoachNq104> FindAllCanBo()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoKeHoachNq104s.ToList();
            }
        }

        public TlDmCanBoKeHoachNq104 FindByMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoKeHoachNq104s.FirstOrDefault(x => maCanBo.Equals(x.MaCanBo));
            }
        }

        public int DeleteByYear(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand("DELETE FROM TL_DM_CanBo_KeHoach_NQ104 WHERE Nam = {0}", year);
            }
        }
    }
}
