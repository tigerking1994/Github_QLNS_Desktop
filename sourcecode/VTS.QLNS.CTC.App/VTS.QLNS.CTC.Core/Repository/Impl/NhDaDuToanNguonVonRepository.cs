using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaDuToanNguonVonRepository : Repository<NhDaDuToanNguonVon>, INhDaDuToanNguonVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaDuToanNguonVonRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaDetailNguonVonQuery> FindByDuToanId(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_nh_dutoan_nguonvon @iIdDuToanId";
                var parameters = new[]
                {
                    new SqlParameter("@iIdDuToanId", duToanId)
                };
                return ctx.FromSqlRaw<NhDaDetailNguonVonQuery>(executeSql, parameters).ToList();
            }
        }

        public void AddOrUpdate(Guid duToanId, IEnumerable<NhDaDuToanNguonVon> entities)
        {
            List<NhDaDuToanNguonVon> lstAdded = entities.Where(x => x.IsAdded && !x.IsDeleted).ToList();
            if (lstAdded.Any())
            {
                foreach (var item in lstAdded)
                {
                    item.IIdDuToanId = duToanId;
                }
                this.AddRange(lstAdded);
            }

            List<NhDaDuToanNguonVon> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
            if (lstModified.Any())
            {
                foreach (var item in lstModified)
                {
                    item.IIdDuToanId = duToanId;
                }
                this.UpdateRange(lstModified);
            }

            List<NhDaDuToanNguonVon> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
            if (lstDeleted.Any())
            {
                this.RemoveRange(lstDeleted);
            }
        }

        public void DeleteByDuAnId(Guid duToanId)
        {
            var lstDeleted = this.FindAll(x => x.IIdDuToanId == duToanId);
            if (!lstDeleted.IsEmpty())
            {
                this.RemoveRange(lstDeleted);
            }
        }
    }
}
