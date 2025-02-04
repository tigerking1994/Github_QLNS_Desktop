using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtQtBcQuyetToanNienDoPhanTichRepository :Repository<VdtQtBcQuyetToanNienDoPhanTich>, IVdtQtBcQuyetToanNienDoPhanTichRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQtBcQuyetToanNienDoPhanTichRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<VdtQtBcQuyetToanNienDoPhanTich> GetBcQuyetToanNienDoPhanTich(Guid iIdParentId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtBcQuyetToanNienDoPhanTichs.Where(n => n.IIdBcQuyetToanNienDo == iIdParentId).ToList();
            }
        }

        public void DeleteByParent(Guid iId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstData = ctx.VdtQtBcQuyetToanNienDoPhanTichs.Where(n => n.IIdBcQuyetToanNienDo == iId);
                if (lstData == null) return;
                ctx.VdtQtBcQuyetToanNienDoPhanTichs.RemoveRange(lstData);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<VdtQtBcQuyetToanNienDoPhanTichQuery> GetBaoCaoQuyetToanNienDoPhanTich(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_qt_baocaoquyettoanniendo_phantich @iIdMaDonVi, @iNamKeHoach, @iIdNguonVon";
                //string sql = "EXECUTE dbo.sp_vdt_qt_baocaoquyettoanniendo_phantich_clone @iIdMaDonVi, @iNamKeHoach, @iIdNguonVon";
                var parameters = new[]
                {
                    new SqlParameter("@iIdMaDonVi", iIdMaDonVi),
                    new SqlParameter("@iNamKeHoach", iNamKeHoach),
                    new SqlParameter("@iIdNguonVon", iIdNguonVon)
                };
                return ctx.FromSqlRaw<VdtQtBcQuyetToanNienDoPhanTichQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtQtBcQuyetToanNienDoPhanTichQuery> GetBaoCaoQuyetToanNienDoPhanTichById(Guid iIdBcQuyetToanNienDo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_qt_baocaoquyettoanniendo_phantich_by_id @iIDQuyetToanId";
                var parameters = new[]
                {
                    new SqlParameter("@iIDQuyetToanId", iIdBcQuyetToanNienDo)
                };
                return ctx.FromSqlRaw<VdtQtBcQuyetToanNienDoPhanTichQuery>(sql, parameters).ToList();
            }
        }
    }
}
