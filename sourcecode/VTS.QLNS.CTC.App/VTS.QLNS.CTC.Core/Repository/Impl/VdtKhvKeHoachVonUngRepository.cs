using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using Dapper;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKhvKeHoachVonUngRepository : Repository<VdtKhvKeHoachVonUng>, IVdtKhvKeHoachVonUngRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvKeHoachVonUngRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtKhvKeHoachVonUngQuery> GetKeHoachVonUngIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvKeHoachVonUngQuery>("EXECUTE dbo.sp_vdt_kehoachvonung_index").ToList();
            }
        }

        public IEnumerable<ExportVonUngDonViQuery> GetKeHoachVonUngDonViExport(List<YearPlanManagerExportCriteria> lstPhanboVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_export_kehoachvonung_duocduyet @Ids";
                DataTable dt = DBExtension.ConvertDataToTableDefined("t_tbl_uniqueidentifier", lstPhanboVon);
                var parameters = new[]
                {
                    new SqlParameter("@Ids", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                return ctx.FromSqlRaw<ExportVonUngDonViQuery>(executeQuery, parameters).ToList();
            }
        }

        public bool CheckTrungSoQuyetDinh(string sSoQuyetDinh, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoachVonUngs.Any(n => n.SSoQuyetDinh == sSoQuyetDinh && n.Id != id);
            }
        }
    }
}
