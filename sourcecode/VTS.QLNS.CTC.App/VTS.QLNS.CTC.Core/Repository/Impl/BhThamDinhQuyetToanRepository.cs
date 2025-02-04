using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhThamDinhQuyetToanRepository : Repository<BhThamDinhQuyetToan>, IBhThamDinhQuyetToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhThamDinhQuyetToanRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhThamDinhQuyetToanQuery> FindAll(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", yearOfWork);
                return ctx.FromSqlRaw<BhThamDinhQuyetToanQuery>("EXECUTE sp_bh_thamdinhquyettoan @INamLamViec ", iNamLamViecParam).ToList();
            }
        }

        public IEnumerable<BhThamDinhQuyetToan> FindUnitVoucher(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhThamDinhQuyetToans.Where(x => x.INamLamViec == yearOfWork && string.IsNullOrEmpty(x.STongHop)).ToList();
            }
        }

        public IEnumerable<BhThamDinhQuyetToan> FindUnitAggregateVoucher(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhThamDinhQuyetToans.Where(x => x.INamLamViec == yearOfWork && !string.IsNullOrEmpty(x.STongHop)).ToList();
            }
        }

        public void UpdateTotalChungTu(string voucherId, string userModify)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_update_total_tham_dinh_quyet_toan @VoucherId, @UserModify";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId),
                    new SqlParameter("@UserModify", userModify)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public string GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach, int namNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhThamDinhQuyetToans.Where(n => n.INamLamViec == namLamViec).OrderByDescending(n => n.SSoChungTu).ToList();
                if (!result.Any())
                    return "QT-001";
                else
                {
                    try
                    {
                        var soCT = result.Select(x => x.SSoChungTu).FirstOrDefault();
                        return "QT-" + (int.Parse(soCT.Substring(3, 3)) + 1).ToString("D3");
                    }
                    catch
                    {
                        return "QT-" + (result.Count() + 1).ToString("D3");
                    }
                }
            }
        }
    }
}
