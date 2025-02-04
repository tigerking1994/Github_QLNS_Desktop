using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhTtThongTriCapPhatRepository : Repository<NhTtThongTriCapPhat>, INhTtThongTriCapPhatRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhTtThongTriCapPhatRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhTtThongTriCapPhatQuery> FindAllThongTri()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_nh_thongtri_capphat_index";
                return ctx.FromSqlRaw<NhTtThongTriCapPhatQuery>(sql).ToList();
            }
        }

        public DataTable ReportThongTriCapPhat(Guid idThongTri)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "sp_nh_report_thongtri_capphat";
                var parameters = new[]
                {
                    new SqlParameter("@idThongTri", idThongTri)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }
    }
}