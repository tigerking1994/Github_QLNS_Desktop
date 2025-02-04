using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmTiGiaChiTietRepository : Repository<NhDmTiGiaChiTiet>, INhDmTiGiaChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmTiGiaChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDmTiGiaChiTiet> FindByTiGia(Guid idTiGia)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDmTiGiaChiTiets.Where(x => x.IIdTiGiaId == idTiGia).ToList();
            }
        }

        public IEnumerable<NhDmTiGiaChiTiet> FindAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDmTiGiaChiTiets.ToList();
            }
        }

        
    }
}
