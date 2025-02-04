using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCanBoKeHoachRepository : Repository<TlDmCanBoKeHoach>, ITlDmCanBoKeHoachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCanBoKeHoachRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<TlDmCanBoKeHoach> FindByYear(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoKeHoaches.Where(x => x.Nam == year).ToList();
            }
        }

        public IEnumerable<TlDmCanBoKeHoach> FindLoadIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoKeHoaches.Include(x => x.TlDmCapBac).Include(x => x.TlDmChucVu).Where(x => x.IsDelete == true).ToList();
            }
        }

        public IEnumerable<TlDmCanBoKeHoach> FindAllCanBo()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoKeHoaches.Include(x => x.TlDmCapBac).Include(x => x.TlDmChucVu).ToList();
            }
        }

        public TlDmCanBoKeHoach FindByMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoKeHoaches.FirstOrDefault(x => maCanBo.Equals(x.MaCanBo));
            }
        }

        public int DeleteByYear(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand("DELETE FROM TL_DM_CanBo_KeHoach WHERE Nam = {0}", year);
            }
        }
    }
}
