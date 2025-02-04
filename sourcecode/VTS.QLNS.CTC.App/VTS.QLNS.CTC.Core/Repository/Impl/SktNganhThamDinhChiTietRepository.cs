using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SktNganhThamDinhChiTietRepository : Repository<NsSktNganhThamDinhChiTiet>, ISktNganhThamDinhChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SktNganhThamDinhChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<ThDChungTuChiTietQuery> FindByCondition(int namLamViec, string idChungTu, int namNganSach, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    SqlParameter chungTuIdParam = new SqlParameter("@VoucherId", idChungTu);
                    SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                    SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                    SqlParameter budgetOfSourceParam = new SqlParameter("@BudgetOfSource", nguonNganSach);
                    return ctx.FromSqlRaw<ThDChungTuChiTietQuery>("EXECUTE dbo.sp_thd_chungtu_chitiet @VoucherId, @YearOfWork, @YearOfBudget, @BudgetOfSource",
                        chungTuIdParam, yearOfWorkParam, yearOfBudgetParam, budgetOfSourceParam).ToList();
                }
                catch (Exception ex)
                {
                    return new List<ThDChungTuChiTietQuery>();
                }
            }
        }

        public IEnumerable<ThDChungTuChiTietQuery> FindByConditionNSBD(int namLamViec, string idChungTu, int namNganSach, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    SqlParameter chungTuIdParam = new SqlParameter("@VoucherId", idChungTu);
                    SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                    SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                    SqlParameter budgetOfSourceParam = new SqlParameter("@BudgetOfSource", nguonNganSach);
                    return ctx.FromSqlRaw<ThDChungTuChiTietQuery>("EXECUTE dbo.sp_thd_chungtu_chitiet_nsbd @VoucherId, @YearOfWork, @YearOfBudget, @BudgetOfSource",
                        chungTuIdParam, yearOfWorkParam, yearOfBudgetParam, budgetOfSourceParam).ToList();
                }
                catch (Exception ex)
                {
                    return new List<ThDChungTuChiTietQuery>();
                }
            }
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherId", voucherId.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE NS_SKT_NganhThamDinhChiTiet WHERE iID_CTNganhThamDinh = @VoucherId", voucherIdParam);
            }
        }

        public IEnumerable<ThDChungTuChiTietQuery> FindByConditionReport(int namLamViec, string idChungTu, string nganh, int namNganSach, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    SqlParameter chuyenNganhParam = new SqlParameter("@ChuyenNganh", nganh);
                    SqlParameter chungTuIdParam = new SqlParameter("@VoucherId", idChungTu);
                    SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                    SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                    SqlParameter budgetOfSourceParam = new SqlParameter("@BudgetOfSource", nguonNganSach);
                    return ctx.FromSqlRaw<ThDChungTuChiTietQuery>("EXECUTE dbo.sp_thd_chungtu_chitiet_report @VoucherId, @ChuyenNganh, @YearOfWork, @YearOfBudget, @BudgetOfSource",
                        chungTuIdParam, chuyenNganhParam, yearOfWorkParam, yearOfBudgetParam, budgetOfSourceParam).ToList();
                }
                catch (Exception ex)
                {
                    return new List<ThDChungTuChiTietQuery>();
                }
            }
        }

        public IEnumerable<ThDChungTuReportNSBDQuery> GetDataReportNSBD(int namLamViec, string idChungTu, int namNganSach, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    SqlParameter chungTuIdParam = new SqlParameter("@VoucherId", idChungTu);
                    SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                    SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                    SqlParameter budgetOfSourceParam = new SqlParameter("@BudgetOfSource", nguonNganSach);
                    return ctx.FromSqlRaw<ThDChungTuReportNSBDQuery>("EXECUTE dbo.sp_thd_chungtu_chitiet_nsbd_report @VoucherId, @YearOfWork, @YearOfBudget, @BudgetOfSource",
                        chungTuIdParam, yearOfWorkParam, yearOfBudgetParam, budgetOfSourceParam).ToList();
                }
                catch (Exception ex)
                {
                    return new List<ThDChungTuReportNSBDQuery>();
                }
            }
        }
    }
}
