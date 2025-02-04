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
    public class NhQtQuyetToanNienDoChiTietRepository : Repository<NhQtQuyetToanNienDoChiTiet>, INhQtQuyetToanNienDoChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhQtQuyetToanNienDoChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhQtQuyetToanNienDoChiTiet> FetchData(Guid quyetToanNienDoId, Guid hopDongId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_quyettoan_niendo_detail_fetch_data @QuyetToanNienDoId, @HopDongId";
                var parameters = new[]
                {
                    new SqlParameter("@QuyetToanNienDoId", quyetToanNienDoId),
                    new SqlParameter("@HopDongId", hopDongId)
                };
                return ctx.FromSqlRaw<NhQtQuyetToanNienDoChiTiet>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhQTQuyetToanNienDoChiTietQuery> GetDetailQuyetToanNienDoDetail(Guid quyetToanNienDoId, int? donViTinhUSD, int? donViTinhVND)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.proc_get_all_nh_baocaoquyettoanniendo_detail @iIDQuyetToan, @devideDonViUSD, @devideDonViVND";
                var parameters = new[]
                {
                    new SqlParameter("@iIDQuyetToan", quyetToanNienDoId),
                    new SqlParameter("@devideDonViUSD", donViTinhUSD),
                    new SqlParameter("@devideDonViVND", donViTinhVND)
                };
                return ctx.FromSqlRaw<NhQTQuyetToanNienDoChiTietQuery>(executeSql, parameters).ToList();
            }
        }
        public IEnumerable<NhQTQuyetToanNienDoChiTietQuery> GetDetailQuyetToanNienDoCreate(Guid? donViId, int? nam, int? donViTinhUSD, int? donViTinhVND)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.proc_get_all_nh_baocaoquyettoanniendo_paging @iIDDonVi, @iNamKeHoach, @devideDonViUSD, @devideDonViVND";
                var parameters = new[]
                {
                    new SqlParameter("@iIDDonVi", donViId),
                    new SqlParameter("@iNamKeHoach", nam),
                    new SqlParameter("@devideDonViUSD", donViTinhUSD),
                    new SqlParameter("@devideDonViVND", donViTinhVND)
                };
                return ctx.FromSqlRaw<NhQTQuyetToanNienDoChiTietQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
