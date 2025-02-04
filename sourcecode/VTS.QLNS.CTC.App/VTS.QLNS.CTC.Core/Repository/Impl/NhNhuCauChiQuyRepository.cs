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
    public class NhNhuCauChiQuyRepository : Repository<NhNhuCauChiQuy>, INhNhuCauChiQuyRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhNhuCauChiQuyRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhNhuCauChiQuyQuery> GetAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_nhucauchiquy_index";
                return ctx.FromSqlRaw<NhNhuCauChiQuyQuery>(sql).ToList();
            }
        }

        public IEnumerable<NhNhuCauChiQuyBaoCaoQuery> ReportNhuCauChiQuy(Guid idChiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_baocao_nhucau_chitiet @idNhuCauChiQuy";
                var parameters = new[]
                {
                    new SqlParameter("@idNhuCauChiQuy", idChiQuy)
                };
                return ctx.FromSqlRaw<NhNhuCauChiQuyBaoCaoQuery>(sql, parameters).ToList();
            }
        }
        public string GetSTTLAMA(int STT)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "SELECT dbo.ToRoman(@STT) as STT;";
                var parameters = new[]
                {
                    new SqlParameter("@STT", STT)
                };
                return ctx.FromSqlRaw<string>(sql, parameters).FirstOrDefault().ToString();
            }
        }

        public string UpdateChilrenGeneral(Guid? Id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "Update NH_NhuCauChiQuy set NH_NhuCauChiQuy.iID_ParentID = null where NH_NhuCauChiQuy.iID_ParentID = @Id";
                var parameters = new[]
                {
                    new SqlParameter("@Id", Id)
                };
                return ctx.FromSqlRaw<string>(sql, parameters).FirstOrDefault().ToString();
            }
        }

        
    }
}
