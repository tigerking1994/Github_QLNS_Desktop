using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaTtHopDongRepository : Repository<VdtDaTtHopDong>, IVdtDaTtHopDongRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaTtHopDongRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<HopDongQuery> FindAllHopDongByNamLamViec(int namLamViec)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@NamLamViec", namLamViec);
                return ctx.FromSqlRaw<HopDongQuery>("EXECUTE dbo.sp_vdt_get_hop_dong_info @NamLamViec", yearOfWorkParam).ToList();
            }
        }

        public List<VdtDaTtHopDong> FindByListDuAnId(List<Guid> iIdDuAnIds)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaTtHopDongs.Where(n => n.IIdDuAnId.HasValue && iIdDuAnIds.Contains(n.IIdDuAnId.Value) && (n.BActive ?? false)).ToList();
            }
        }

        public bool CheckExistHopDongByGoiThai(Guid iIdGoiThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaTtHopDongs.Any(n => n.IIdGoiThauId == iIdGoiThau && (n.BActive ?? false));
            }
        }

        public void DeactiveHopDong(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                VdtDaTtHopDong e = ctx.VdtDaTtHopDongs.FirstOrDefault(t => t.Id.Equals(id));
                if (e != null)
                {
                    e.BActive = false;
                }
                ctx.SaveChanges();
            }
        }
    }
}
