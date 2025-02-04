using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhKhChiTietHopDongRepository : Repository<NhKhChiTietHopDong>, INhKhChiTietHopDongRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhKhChiTietHopDongRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhKhChiTietHopDong> FindChiTietHopDongByKHCT(Guid idKHChiTiet)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhKhChiTietHopDongs.Where(n => n.IIdKhChiTietId == idKHChiTiet).ToList();
            }
        }

    }
}
