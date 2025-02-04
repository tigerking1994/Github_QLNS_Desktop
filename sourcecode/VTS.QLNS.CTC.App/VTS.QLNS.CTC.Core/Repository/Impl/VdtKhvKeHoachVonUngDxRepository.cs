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
    public class VdtKhvKeHoachVonUngDxRepository : Repository<VdtKhvKeHoachVonUngDx>, IVdtKhvKeHoachVonUngDxRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvKeHoachVonUngDxRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtKhvKeHoachVonUngDxQuery> GetKeHoachVonUngDxIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvKeHoachVonUngDxQuery>("EXECUTE dbo.sp_vdt_kehoachvonungdx_index").ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoachVonUngDx> GetKHVUDeXuatInKHVUDuocDuyet(string iIdMaDonVi, int iNamKeHoach, DateTime dNgayLap)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvKeHoachVonUngDx>("EXECUTE dbo.sp_vdt_khvu_get_kehoachvonungdexuat_in_duocduyet @iIdMaDonVi, @iNamKeHoach, @dNgayLap",
                    new SqlParameter("iIdMaDonVi", iIdMaDonVi),
                    new SqlParameter("iNamKeHoach", iNamKeHoach),
                    new SqlParameter("dNgayLap", dNgayLap)).ToList();
            }
        }

        public bool CheckTrungSoDeNghi(string sSoDeNghi, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoachVonUngDxes.Any(n => n.SSoDeNghi == sSoDeNghi && n.Id != id);
            }
        }
    }
}
