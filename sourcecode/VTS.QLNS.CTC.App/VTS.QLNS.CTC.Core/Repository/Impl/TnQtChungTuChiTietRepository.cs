using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TnQtChungTuChiTietRepository : Repository<TnQtChungTuChiTiet>, ITnQtChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TnQtChungTuChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdsParam = new SqlParameter("@VoucherIds", creation.VoucherIds);
                SqlParameter voucherIdParam = new SqlParameter("@VoucherId", creation.VoucherId);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", creation.YearOfBudget);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", creation.BudgetSource);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", creation.YearOfWork);
                SqlParameter typeParam = new SqlParameter("@Type", creation.Type);
                SqlParameter quarterMonthTypeParam = new SqlParameter("@QuarterMonthType", creation.QuarterMonthType);
                SqlParameter quarterMonthParam = new SqlParameter("@QuarterMonth", creation.QuarterMonth);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", creation.AgencyId);
                SqlParameter agencyNameParam = new SqlParameter("@AgencyName", creation.AgencyName);
                SqlParameter userNameParam = new SqlParameter("@UserName", creation.UserName);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_tn_qt_chungtu_chitiet_tao_tonghop @VoucherIds, @VoucherId, @YearOfBudget, @BudgetSource, @YearOfWork, @Type, @QuarterMonthType, @QuarterMonth, @AgencyId, @AgencyName, @UserName",
                    voucherIdsParam, voucherIdParam, yearOfBudgetParam, budgetSourceParam, yearOfWorkParam, typeParam, quarterMonthTypeParam, quarterMonthParam, agencyIdParam, agencyNameParam, userNameParam);
            }
        }

        public IEnumerable<TnQtChungTuChiTietQuery> FindByRealRevenueExpenditureCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = searchCondition.VoucherId.ToString();
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS;
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<TnQtChungTuChiTietQuery>("EXECUTE sp_tn_qt_chungtu_chitiet1 @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource",
                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam).ToList();
            }
        }

        public IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureReportCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter typeParam = new SqlParameter();
                typeParam.ParameterName = "@Type";
                typeParam.DbType = DbType.Int32;
                typeParam.Value = searchCondition.ILoai;
                typeParam.Direction = ParameterDirection.Input;

                SqlParameter IdDonViParam = new SqlParameter();
                IdDonViParam.ParameterName = "@IdDonVi";
                IdDonViParam.DbType = DbType.String;
                IdDonViParam.Value = searchCondition.IdDonVi != null ? searchCondition.IdDonVi : string.Empty;
                IdDonViParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS;
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter monthQuaterParam = new SqlParameter();
                monthQuaterParam.ParameterName = "@IMonthQuater";
                monthQuaterParam.DbType = DbType.Int32;
                monthQuaterParam.Value = searchCondition.IThangQuy;
                monthQuaterParam.Direction = ParameterDirection.Input;

                SqlParameter monthQuaterTypeParam = new SqlParameter();
                monthQuaterTypeParam.ParameterName = "@IMonthQuaterType";
                monthQuaterTypeParam.DbType = DbType.Int32;
                monthQuaterTypeParam.Value = searchCondition.IThangQuyLoai;
                monthQuaterTypeParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<TnQtChungTuChiTietReportQuery>("EXECUTE sp_tn_qt_rpt_tong_hop_thu_nop @IdDonVi, @LNS, @YearOfWork, @YearOfBudget," +
                            "@BudgetSource, @Type, @IMonthQuater, @IMonthQuaterType",
                            IdDonViParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, typeParam, monthQuaterParam, monthQuaterTypeParam).ToList();
            }
        }

        public IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureResultCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter IdDonViParam = new SqlParameter();
                IdDonViParam.ParameterName = "@IdDonVi";
                IdDonViParam.DbType = DbType.String;
                IdDonViParam.Value = searchCondition.IdDonVi != null ? searchCondition.IdDonVi : string.Empty;
                IdDonViParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS;
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter monthQuaterParam = new SqlParameter();
                monthQuaterParam.ParameterName = "@IMonthQuater";
                monthQuaterParam.DbType = DbType.Int32;
                monthQuaterParam.Value = searchCondition.IThangQuy;
                monthQuaterParam.Direction = ParameterDirection.Input;

                SqlParameter monthQuaterTypeParam = new SqlParameter();
                monthQuaterTypeParam.ParameterName = "@IMonthQuaterType";
                monthQuaterTypeParam.DbType = DbType.Int32;
                monthQuaterTypeParam.Value = searchCondition.IThangQuyLoai;
                monthQuaterTypeParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<TnQtChungTuChiTietReportQuery>("EXECUTE sp_tn_qt_rpt_thong_tri_thu_nop @IdDonVi, @LNS, @YearOfWork, @YearOfBudget," +
                            "@BudgetSource, @IMonthQuater, @IMonthQuaterType",
                            IdDonViParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, monthQuaterParam, monthQuaterTypeParam).ToList();
            }
        }
    }
}
