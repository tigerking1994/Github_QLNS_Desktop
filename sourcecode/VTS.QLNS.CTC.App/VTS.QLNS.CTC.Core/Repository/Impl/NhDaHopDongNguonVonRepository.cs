using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaHopDongNguonVonRepository : Repository<NhDaHopDongNguonVon>, INhDaHopDongNguonVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaHopDongNguonVonRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaHopDongNguonVon> FindByIdHopDong(Guid idHopDong)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaHopDongNguonVons.Where(x => x.IIdHopDongId == idHopDong).ToList();
            }
        }
        public IEnumerable<NhDaHopDongNguonVon> FindByHopDongIdQuyetDinhId(Guid? idHopDong, Guid? idQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaHopDongNguonVons.Where(x => x.IIdHopDongId == idHopDong && x.IIdCacQuyetDinhNguonVonId == idQuyetDinh).ToList();
            }
        }
        public IEnumerable<NhDaHopDongNguonVon> FindByHopDongIdGoiThauNguonVonId(Guid? idHopDong, Guid? idGoiThauNguonVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaHopDongNguonVons.Where(x => x.IIdHopDongId == idHopDong && x.IIdGoiThauNguonVonId == idGoiThauNguonVon).ToList();
            }
        }
    }
}
