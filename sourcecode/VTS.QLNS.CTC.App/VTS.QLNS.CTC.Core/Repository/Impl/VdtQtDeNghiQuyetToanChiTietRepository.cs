using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtQtDeNghiQuyetToanChiTietRepository : Repository<VdtQtDeNghiQuyetToanChiTiet>, IVdtQtDeNghiQuyetToanChiTietRepository
    {
        private ApplicationDbContextFactory _contextFactory;
        public VdtQtDeNghiQuyetToanChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<VdtQtDeNghiQuyetToanChiTiet> FindByDeNghiQuyetToanId(Guid deNghiQuyetToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtDeNghiQuyetToanChiTiets.Where(n => n.IIdDeNghiQuyetToanId == deNghiQuyetToanId).ToList();
            }
        }

        public List<VdtDaDuToanChiPhiDataQuery> FindListDuToanChiPhiByDuAn(Guid duAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", duAnId);
                return ctx.FromSqlRaw<VdtDaDuToanChiPhiDataQuery>("EXECUTE dbo.sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet @duAnId", duAnIdParam).ToList();
            }
        }

        public List<VdtDaDuToanChiPhiDataQuery> FindListDuToanChiPhiByDuAnNew(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@iIdDuToanId", duToanId);
                return ctx.FromSqlRaw<VdtDaDuToanChiPhiDataQuery>("EXECUTE dbo.sp_vdt_get_all_dutoan_chiphi_by_duan_quyettoanchitiet_1 @iIdDuToanId", duAnIdParam).ToList();
            }
        }
    }
}
