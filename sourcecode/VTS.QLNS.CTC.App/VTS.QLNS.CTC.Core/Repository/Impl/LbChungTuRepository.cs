using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class LbChungTuRepository : Repository<NsNganhChungTu>, ILbChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public LbChungTuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                NsNganhChungTu entity = ctx.NsNganhChungTus.Find(id);
                entity.BKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public void UpdateTotalLbChungTu(string voucherId, string userModify)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_lb_update_total_lb_chung_tu @VoucherId, @UserModify";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId),
                    new SqlParameter("@UserModify", userModify)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<LbChungTuQuery> FindByCondition(int namLamViec, int nguonNganSach, string donviUserId, int namNganSach, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_lb_chungtu @YearOfWork, @BudgetSource, @AgencyUserId, @YearOfBudget, @UserName";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@BudgetSource", nguonNganSach),
                    new SqlParameter("@AgencyUserId", donviUserId),
                    new SqlParameter("@YearOfBudget", namNganSach),
                    new SqlParameter("@UserName", userName)
                };
                return ctx.FromSqlRaw<LbChungTuQuery>(sql, parameters).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NsNganhChungTu> result = ctx.NsNganhChungTus.Where(n => n.INamLamViec == namLamViec && n.IIdMaNguonNganSach == nguonNganSach).OrderByDescending(n => n.ISoChungTuIndex).ToList();
                if (result.Count > 0 && result.FirstOrDefault().ISoChungTuIndex.HasValue && result.FirstOrDefault().ISoChungTuIndex > 0)
                {
                    return (result.FirstOrDefault().ISoChungTuIndex.Value + 1);
                }
                return 1;
            }
        }

        public IEnumerable<LbChungTuCanCuQuery> FindByCondition(int namLamViec, Guid idChungTu, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dt_nhan_phanbo_dutoan_cancu_phanbo_nganh @NamLamViec, @IdChungTu, @IdDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@IdChungTu", idChungTu),
                    new SqlParameter("@IdDonVi", idDonVi)
                };
                return ctx.FromSqlRaw<LbChungTuCanCuQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<LbChungTuCanCuDuToanDataQuery> GetCanCuDuToanData(int namLamViec, string idChungTu, int loaiChungTu, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dt_nhan_phanbo_dutoan_cancu_phanbo_nganh_laydulieu @NamLamViec, @IdChungTu, @LoaiChungTu, @IdDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@IdChungTu", idChungTu),
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@IdDonVi", idDonVi)
                };
                return ctx.FromSqlRaw<LbChungTuCanCuDuToanDataQuery>(sql, parameters).ToList();
            }
        }
    }
}
