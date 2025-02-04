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
    public class VdtQtDeNghiQuyetToanNguonVonRepository : Repository<VdtQtDeNghiQuyetToanNguonvon>, IVdtQtDeNghiQuyetToanNguonVonRepository
    {
        private ApplicationDbContextFactory _contextFactory;
        public VdtQtDeNghiQuyetToanNguonVonRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<VdtQtDeNghiQuyetToanNguonvon> FindByDeNghiQuyetToanId(Guid deNghiQuyetToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtDeNghiQuyetToanNguonvons.Where(n => n.IIdDeNghiQuyetToanId == deNghiQuyetToanId).ToList();
            }
        }

        public List<NguonVonQuyetToanKeHoachQuery> GetNguonVonByDuToanId(string duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToandParam = new SqlParameter("@duToanId", duToanId);
                return ctx.FromSqlRaw<NguonVonQuyetToanKeHoachQuery>("EXECUTE dbo.sp_vdt_get_all_dutoan_nguonvon_by_list_dutoan_id @duToanId", duToandParam).ToList();
            }
        }

        public List<NguonVonQuyetToanKeHoachQuery> GetNguonVonByQDDTId(string duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToandParam = new SqlParameter("@qddtId", duToanId);
                return ctx.FromSqlRaw<NguonVonQuyetToanKeHoachQuery>("EXECUTE dbo.sp_vdt_get_all_qddt_nguonvon_by_list_dutoan_id @qddtId", duToandParam).ToList();
            }
        }
    }
}
