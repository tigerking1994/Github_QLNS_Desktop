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
    public class NhTtThucHienNganSachRepository : Repository<NhTtThucHienNganSach>, INhTtThucHienNganSachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhTtThucHienNganSachRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhTtThucHienNganSachQuery> FindAllData(int? tabTable, int? iQuyList, int? iNam, int? iTuNam, int? iDenNam, Guid? iDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_nh_thuchien_ngansach_index @tabTable, @iQuyList, @iNam, @iTuNam, @iDenNam, @iDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@tabTable", tabTable),
                    new SqlParameter("@iQuyList", iQuyList),
                    new SqlParameter("@iNam", iNam),
                    new SqlParameter("@iTuNam", iTuNam),
                    new SqlParameter("@iDenNam", iDenNam),
                    new SqlParameter("@iDonVi", iDonVi),
                };
                return ctx.FromSqlRaw<NhTtThucHienNganSachQuery>(sql, parameters).ToList();
            }
        }
        public IEnumerable<NhTtThucHienNganSachGiaiDoanQuery> GetGiaiDoan()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "select distinct iGiaiDoanTu, iGiaiDoanDen , CONCAT(N'Giai đoạn ', iGiaiDoanTu , ' - ' , iGiaiDoanDen) as sGiaiDoan from NH_KHTongThe where CONCAT(iGiaiDoanDen , '') != '' order by iGiaiDoanTu;";
                return ctx.FromSqlRaw<NhTtThucHienNganSachGiaiDoanQuery>(sql).ToList();
            }
        }
        public string GetSTTLAMA(int STT)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "SELECT dbo.ToRoman(@STT) as STT;";
                var parameters = new[]
                {
                    new SqlParameter("@STT", STT),

                };
                return ctx.FromSqlRaw<string>(sql , parameters).FirstOrDefault().ToString();
            }
        }
        

        public IEnumerable<NhTtThucHienNganSachQuery> ReportThucHienNganSach(int? tabindex, int? iQuyPrint, int? iNamPrint, int? iTuNamPrint, int? iDenNamPrint, Guid? iDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_nh_thuchien_ngansach_index @tabTable, @iQuyList, @iNam, @iTuNam, @iDenNam, @iDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@tabTable", tabindex),
                    new SqlParameter("@iQuyList", iQuyPrint),
                    new SqlParameter("@iNam", iNamPrint),
                    new SqlParameter("@iTuNam", iTuNamPrint),
                    new SqlParameter("@iDenNam", iDenNamPrint),
                    new SqlParameter("@iDonVi", iDonVi),
                };
                return ctx.FromSqlRaw<NhTtThucHienNganSachQuery>(sql, parameters).ToList();

            }
        }
    }
}