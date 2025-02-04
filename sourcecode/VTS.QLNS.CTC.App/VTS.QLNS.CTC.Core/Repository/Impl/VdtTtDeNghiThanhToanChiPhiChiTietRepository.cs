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
    public class VdtTtDeNghiThanhToanChiPhiChiTietRepository : Repository<VdtTtDeNghiThanhToanChiPhiChiTiet>, IVdtTtDeNghiThanhToanChiPhiChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtTtDeNghiThanhToanChiPhiChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteByParentId(Guid iId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstData = ctx.VdtTtDeNghiThanhToanChiPhiChiTiets.Where(n => n.IIdDeNghiThanhToanChiPhiId == iId);
                if (lstData == null) return;
                ctx.RemoveRange(lstData);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<VdtTtDeNghiThanhToanChiPhiChiTietQuery> GetDeNghiThanhToanChiPhiDetail(Guid iIdDuToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_vdt_thanhtoanchiphi_detail @iIdDuToanId";
                var parameters = new[]
                {
                    new SqlParameter("iIdDuToanId",iIdDuToanId)
                };
                return ctx.FromSqlRaw<VdtTtDeNghiThanhToanChiPhiChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<VdtTtDeNghiThanhToanChiPhiChiTietQuery> GetDeNghiThanhToanChiPhiDetailById(Guid iIdChungTu, Guid iIdDuToan)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_vdt_thanhtoanchiphi_detail_by_id @iId, @iIdDuToan";
                var parameters = new[]
                {
                    new SqlParameter("iId",iIdChungTu),
                    new SqlParameter("iIdDuToan", iIdDuToan)
                };
                return ctx.FromSqlRaw<VdtTtDeNghiThanhToanChiPhiChiTietQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
