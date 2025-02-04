using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaDuToanHangMucRepository : Repository<NhDaDuToanHangMuc>, INhDaDuToanHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaDuToanHangMucRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddOrUpdate(IEnumerable<NhDaDuToanHangMuc> entities)
        {
            List<NhDaDuToanHangMuc> lstAdded = entities.Where(x => x.IsAdded && !x.IsDeleted).ToList();
            if (lstAdded.Any())
            {
                this.AddRange(lstAdded);
            }

            List<NhDaDuToanHangMuc> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
            if (lstModified.Any())
            {
                this.UpdateRange(lstModified);
            }

            List<NhDaDuToanHangMuc> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
            if (lstDeleted.Any())
            {
                this.RemoveRange(lstDeleted);
            }
        }

        public IEnumerable<NhDaDetailHangMucQuery> GetHangMucByDuToanId(Guid iIdDuToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_nh_dutoan_hangmuc @iIdDuToanId";
                var parameters = new[] {
                    new SqlParameter("@iIdDuToanId", iIdDuToanId)
                };
                return ctx.FromSqlRaw<NhDaDetailHangMucQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
