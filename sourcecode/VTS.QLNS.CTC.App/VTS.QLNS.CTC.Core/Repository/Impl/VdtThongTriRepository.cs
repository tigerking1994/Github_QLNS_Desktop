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
    public class VdtThongTriRepository : Repository<VdtThongTri>, IVdtThongTriRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtThongTriRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtThongTriQuery> GetVdtThongTriIndex(Guid iIdLoaiThongTri, int openFromPheDuyetThanhToan)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_thongtri_index @iIdLoaiThongTri, @openFromPheDuyetThanhToan";
                var parameters = new[]
                {
                    new SqlParameter("@iIdLoaiThongTri", iIdLoaiThongTri),
                    new SqlParameter("@openFromPheDuyetThanhToan", openFromPheDuyetThanhToan)
                };
                return ctx.FromSqlRaw<VdtThongTriQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtDmLoaiThongTri> GetAllDmLoaiThongTri()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmLoaiThongTris.ToList();
            }
        }

        public IEnumerable<VdtDmKieuThongTri> GetAllKieuThongTri()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmKieuThongTris.ToList();
            }
        }
    }
}
