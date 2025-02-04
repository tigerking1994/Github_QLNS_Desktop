
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
    public class NhDaQdDauTuChiPhiRepository : Repository<NhDaQdDauTuChiPhi>, INhDaQdDauTuChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaQdDauTuChiPhiRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaDetailChiPhiQuery> GetChiPhiByQdDauTuId(Guid iIdQdDauTuId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_nh_qddautu_chiphi @iIdQdDauTuId";
                var parameters = new[] {
                    new SqlParameter("@iIdQdDauTuId", iIdQdDauTuId)
                };
                return ctx.FromSqlRaw<NhDaDetailChiPhiQuery>(executeSql, parameters).ToList();
            }
        }
        
        public IEnumerable<NhDaQdDauTuChiPhi> FindByQdDauTuId(Guid qdDauTuId)
        {
            return this.FindAll(x => x.IIdQdDauTuId == qdDauTuId);
        }

        public void DeleteByQdDauTuId(Guid qdDauTuId)
        {
            var lstDeleted = this.FindAll(x => x.IIdQdDauTuId == qdDauTuId);
            if (!lstDeleted.IsEmpty())
            {
                this.RemoveRange(lstDeleted);
            }
        }

        public void DeleteByQdNguonVonId(Guid idNguonVon)
        {
            var lstDeleted = this.FindAll(x => x.IIdQDDauTuNguonVonId == idNguonVon);
            if (!lstDeleted.IsEmpty())
            {
                this.RemoveRange(lstDeleted);
            }
        }

        public IEnumerable<NhDaQdDauTuChiPhi> FindByQdDauTuNGuonVonId(Guid idNguonVon)
        {
            return this.FindAll(x => x.IIdQDDauTuNguonVonId == idNguonVon);
        }

        public IEnumerable<NhDaQdDauTuChiPhi> FindByQdDauTuByDuAnId(Guid iIdDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var data = from qddt in ctx.NhDaQdDauTu.Where(x => x.IIdDuAnId == iIdDuAn)
                           join dt in ctx.NhDaQdDauTuChiPhi on qddt.Id equals dt.IIdQdDauTuId
                           select dt;
                return data.IsEmpty() ? new List<NhDaQdDauTuChiPhi>() : data.ToList();
            }
        }

    }
}
