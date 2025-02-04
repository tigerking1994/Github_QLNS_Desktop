using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.Data;
using VTS.QLNS.CTC.Core.Extensions;
using Dapper;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsSktNganhThamDinhChiTietSktRepository : Repository<NsSktNganhThamDinhChiTietSkt>, INsSktNganhThamDinhChiTietSktRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsSktNganhThamDinhChiTietSktRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteByIdChungTuSkt(Guid voucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherId", voucherId.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_SKT_NganhThamDinhChiTiet_SKT WHERE iID_CTSoKiemTra = @VoucherId", voucherIdParam);
            }
        }

        public void DeleteByYearOfWork(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@NamLamViec", namLamViec);
                ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_SKT_NganhThamDinhChiTiet_SKT WHERE iNamLamViec = @NamLamViec", voucherIdParam);
            }
        }

        public List<JsonNsSktNganhThamDinhChiTietSktQuery> GetNsSktNganhThamDinhChiTietByChungTuId(List<Guid> iIds)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_ns_sktnganhthamdinh_by_chungtuid @iIdsChungTu";
                DataTable dt = DBExtension.ConvertDataToGuidTable(iIds);
                var parameters = new[]
                {
                    new SqlParameter("@iIdsChungTu", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                return ctx.FromSqlRaw<JsonNsSktNganhThamDinhChiTietSktQuery>(executeQuery, parameters).ToList();
            }
        }
    }
}
