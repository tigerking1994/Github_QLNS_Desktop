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
    public class NhKtKhoiTaoCapPhatChiTietRepository : Repository<NhKtKhoiTaoCapPhatChiTiet>, INhKtKhoiTaoCapPhatChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhKtKhoiTaoCapPhatChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhKtKhoiTaoCapPhatChiTiet> FetchData(Guid khoiTaoCapPhatId, Guid hopDongId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_quyettoan_niendo_detail_fetch_data @KhoiTaoCapPhatId, @HopDongId";
                var parameters = new[]
                {
                    new SqlParameter("@KhoiTaoCapPhatId", khoiTaoCapPhatId),
                    new SqlParameter("@HopDongId", hopDongId)
                };
                return ctx.FromSqlRaw<NhKtKhoiTaoCapPhatChiTiet>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhKtKhoiTaoCapPhatChiTietQuery> FindById(Guid khoiTaoCapPhatId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_kt_khoitaocapphat_chitiet @KhoiTaoCapPhatId";
                var parameters = new[]
                {
                    new SqlParameter("@KhoiTaoCapPhatId", khoiTaoCapPhatId)
                };
                return ctx.FromSqlRaw<NhKtKhoiTaoCapPhatChiTietQuery>(executeSql, parameters).ToList();
            }
        }
        
        public IEnumerable<NhKtKhoiTaoCapPhatChiTietQuery> FindDetailById(Guid khoiTaoCapPhatId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_kt_khoitaocapphat_chitiet_ById @KhoiTaoCapPhatId";
                var parameters = new[]
                {
                    new SqlParameter("@KhoiTaoCapPhatId", khoiTaoCapPhatId)
                };
                return ctx.FromSqlRaw<NhKtKhoiTaoCapPhatChiTietQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
