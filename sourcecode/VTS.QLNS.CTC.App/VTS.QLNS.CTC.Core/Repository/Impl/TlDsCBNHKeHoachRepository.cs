using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDsCBNHKeHoachRepository : Repository<TlDsCBNHKeHoach>, ITlDsCBNHKeHoachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDsCBNHKeHoachRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<TlDsCBNHKeHoach> FindByYear(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCBNHKeHoachs.Where(x => x.Nam == year).ToList();
            }
        }

        public IEnumerable<TlDsCBNHKeHoach> FindLoadIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCBNHKeHoachs.Include(x => x.TlDmCapBac).Include(x => x.TlDmChucVu).Where(x => x.IsDelete == true).ToList();
            }
        }

        public IEnumerable<TlDsCBNHKeHoach> FindAllCanBo()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCBNHKeHoachs.Include(x => x.TlDmCapBac).Include(x => x.TlDmChucVu).ToList();
            }
        }

        public TlDsCBNHKeHoach FindByMaCanBo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCBNHKeHoachs.FirstOrDefault(x => maCanBo.Equals(x.MaCanBo));
            }
        }

        public int DeleteByYear(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand("DELETE FROM TL_DS_CBNH_KeHoach WHERE Nam = {0}", year);
            }
        }

        public IEnumerable<TlDsCBNHKeHoach> FindAllCanBoNghiHuu()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCBNHKeHoachs.Include(x => x.TlDmCapBac).Include(x => x.TlDmChucVu).Where(x => x.Loai == LoaiCanBoKehoach.NGHIHUU).ToList();
            }
        }
    }
}
