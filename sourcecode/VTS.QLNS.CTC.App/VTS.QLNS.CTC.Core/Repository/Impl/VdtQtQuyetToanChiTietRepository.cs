using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtQtQuyetToanChiTietRepository : Repository<VdtQtQuyetToanChiTiet>, IVdtQtQuyetToanChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public VdtQtQuyetToanChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<VdtQtQuyetToanChiTiet> FindByQuyetToanId(Guid quyetToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtQuyetToanChiTiets.Where(n => n.IIdQuyetToanId == quyetToanId).ToList();
            }
        }

        public void UpdateTotal(string voucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter("@VoucherId", voucherId);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_update_total_quyet_toan @VoucherId", voucherIdParam);
            }
        }
    }
}
