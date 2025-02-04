using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsDcChungTuRepository : Repository<NsDcChungTu>, INsDcChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsDcChungTuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NsDcChungTuQuery> FindByCondition(EstimationVoucherCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dc_chungtu_danhsach @YearOfBudget, @YearOfWork, @BudgetSource, @UserName, @VoucherTypes";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfBudget", condition.YearOfBudget),
                    new SqlParameter("@YearOfWork", condition.YearOfWork),
                    new SqlParameter("@BudgetSource", condition.BudgetSource),
                    new SqlParameter("@UserName", condition.UserName),
                    new SqlParameter("@VoucherTypes", condition.VoucherTypes)
                };
                return ctx.FromSqlRaw<NsDcChungTuQuery>(sql, parameters).ToList();
            }
        }

        public int FindNextSoChungTuIndex(Expression<Func<NsDcChungTu, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var currentIndex = ctx.NsDcChungTus.Where(predicate).Max(x => x.ISoChungTuIndex);
                return currentIndex != null ? (int)currentIndex + 1 : 1;
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                NsDcChungTu entity = ctx.NsDcChungTus.Find(id);
                entity.BKhoa = lockStatus;
                return Update(entity);
            }
        }

        public void UpdateAggregateStatus(string voucherIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"UPDATE NS_DC_ChungTu SET bDaTongHop = 0 WHERE iID_DCChungTu IN (SELECT * FROM f_split(@VoucherIds)) ";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherIds", voucherIds)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<NsDcChungTuQuery> FindByCondition(int namLamViec, int loaiChungTu, Guid idNhan, int namNganSach, int loaiNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dc_danhsach_chungtu_tonghop @YearOfWork, @VoucherType, @IdNhan, @YearOfBudget, @BudgetSourceType";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@VoucherType", loaiChungTu),
                    new SqlParameter("@IdNhan", idNhan),
                    new SqlParameter("@YearOfBudget", namNganSach),
                    new SqlParameter("@BudgetSourceType", loaiNganSach)
                };
                return ctx.FromSqlRaw<NsDcChungTuQuery>(sql, parameters).ToList();
            }
        }

        public List<string> GetDonViDieuChinh(string iDs, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_ns_phanbo_dutoan_get_donvi_dieuchinh @IDs, @NamLamViec";
                var parameters = new[]
                {
                    new SqlParameter("@IDs", iDs),
                    new SqlParameter("@NamLamViec", namLamViec)
                };
                return ctx.FromSqlRaw<string>(sql, parameters).ToList();
            }
        }
    }
}
