using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsQtChungTuRepository : Repository<NsQtChungTu>, INsQtChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsQtChungTuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteRange(List<NsQtChungTu> chungTus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherIds", string.Join(",", chungTus.Select(x => x.Id)));
                ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_QT_ChungTu WHERE iID_QTChungTu IN (SELECT * FROM f_split(@VoucherIds))", voucherIdParam);
            }
        }

        public List<string> FindAgencyIdByMonth(ReportSettlementCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<string> months = condition.QuarterMonth.Split(",").ToList();
                return ctx.NsQtChungTus.Where(x => months.Contains(x.IThangQuy.ToString()) && x.INamLamViec == condition.YearOfWork
                        && x.INamNganSach == condition.YearOfBudget && x.IIdMaNguonNganSach == condition.BudgetSource
                        && x.SLoai == condition.SettlementType).Select(x => x.IIdMaDonVi).Distinct().ToList();
            }
        }

        /// <summary>
        /// tìm chứng từ tổng hợp theo số chứng từ con
        /// </summary>
        /// <param name="voucherNoes"></param>
        /// <returns></returns>
        public NsQtChungTu FindAggregateVoucher(string voucherNoes)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQtChungTus.Where(x => x.STongHop == voucherNoes).FirstOrDefault();
            }
        }

        public IEnumerable<NsQtChungTu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork, int yearOfBudget, int budgetSource, string voucherType)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQtChungTus.Where(x => voucherNoes.Contains(x.SSoChungTu) && x.INamLamViec == yearOfWork
                                                   && x.INamNganSach == yearOfBudget && x.IIdMaNguonNganSach == budgetSource && x.SLoai == voucherType).ToList();
            }
        }

        public IEnumerable<NsQtChungTuQuery> FindByCondition(SettlementVoucherCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter typeParam = new SqlParameter("@Type", condition.SettlementType);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", condition.YearOfBudget);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", condition.YearOfWork);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", condition.BudgetSource);
                SqlParameter statusParam = new SqlParameter("@Status", condition.Status);
                SqlParameter quarterMonthParam = new SqlParameter("@QuarterMonthParam", condition.QuarterMonth == null ? 0 : condition.QuarterMonth.Value);
                SqlParameter quarterMonthTypeParam = new SqlParameter("@QuarterMonthTypeParam", condition.QuarterMonthType == null ? 0 : condition.QuarterMonthType.Value);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyIdParam", condition.AgencyId == null ? "" : condition.AgencyId);
                SqlParameter userNameParam = new SqlParameter("@UserName", condition.UserName);
                return ctx.FromSqlRaw<NsQtChungTuQuery>("EXECUTE dbo.sp_qt_chungtu_danhsach @Type, @YearOfBudget, @YearOfWork, @BudgetSource, @QuarterMonthParam, @QuarterMonthTypeParam, @AgencyIdParam, @UserName",
                typeParam, yearOfBudgetParam, yearOfWorkParam, budgetSourceParam, quarterMonthParam, quarterMonthTypeParam, agencyIdParam, userNameParam).ToList();
            }
        }

        /// <summary>
        /// Lấy ra danh sách chứng từ theo loại
        /// </summary>
        /// <param name="type">Loại chứng từ</param>
        /// <returns></returns>
        public IEnumerable<NsQtChungTu> FindByType(string type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQtChungTus.Where(x => x.SLoai == type);
            }
        }

        public int FindLastIndex(SettlementVoucherCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int lastIndex = 0;
                NsQtChungTu chungTu = ctx.NsQtChungTus.Where(x => x.INamLamViec == condition.YearOfWork && x.INamNganSach == condition.YearOfBudget
                && x.IIdMaNguonNganSach == condition.BudgetSource && x.SLoai == condition.SettlementType).OrderBy(x => x.ISoChungTuIndex).LastOrDefault();
                if (chungTu == null)
                    return lastIndex;
                else return chungTu.ISoChungTuIndex.Value;
            }
        }

        public int FindLastAdjustIndex(SettlementVoucherCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int lastIndex = 0;
                NsQtChungTu chungTu = ctx.NsQtChungTus.Where(x => x.INamLamViec == condition.YearOfWork && x.INamNganSach == condition.YearOfBudget
                && x.IIdMaNguonNganSach == condition.BudgetSource && x.SLoai == condition.SettlementType && x.ILoaiChungTu == condition.AdjustType).OrderBy(x => x.ILanDieuChinh).LastOrDefault();
                if (chungTu == null)
                    return lastIndex;
                else return chungTu.ILanDieuChinh.Value;
            }
        }

        public void LockOrUnlockMultiple(List<NsQtChungTu> chungTus, bool isLock)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherIds", string.Join(",", chungTus.Select(x => x.Id)));
                var lockParam = new SqlParameter("@LockParam", isLock ? 1 : 0);
                ctx.Database.ExecuteSqlCommand($"UPDATE NS_QT_ChungTu SET bKhoa = @LockParam WHERE iID_QTChungTu IN (SELECT * FROM f_split(@VoucherIds))", lockParam, voucherIdParam);
            }
        }

        public void UpdateAggregateStatus(string voucherIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherIds", voucherIds);
                //ctx.Database.ExecuteSqlCommand($"UPDATE NS_QT_ChungTu SET bDaTongHop = 0, fTongTuChi_PheDuyet = 0 WHERE iID_QTChungTu IN (SELECT * FROM f_split(@VoucherIds)) ", voucherIdParam);
                ctx.Database.ExecuteSqlCommand($"UPDATE NS_QT_ChungTu SET bDaTongHop = 0 WHERE iID_QTChungTu IN (SELECT * FROM f_split(@VoucherIds)) ", voucherIdParam);
            }
        }
    }
}
