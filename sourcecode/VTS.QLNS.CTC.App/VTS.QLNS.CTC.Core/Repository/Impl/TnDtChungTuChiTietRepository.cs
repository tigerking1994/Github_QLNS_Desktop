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
    public class TnDtChungTuChiTietRepository : Repository<TnDtChungTuChiTiet>, ITnDtChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TnDtChungTuChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tn_dt_chungtu_chitiet_tao_tonghop @VoucherIds, @VoucherId, @YearOfBudget, @BudgetSource, @YearOfWork, @Type, @QuarterMonthType, @QuarterMonth, @AgencyId, @AgencyName, @UserName";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherIds", creation.VoucherIds),
                    new SqlParameter("@VoucherId", creation.VoucherId),
                    new SqlParameter("@YearOfBudget", creation.YearOfBudget),
                    new SqlParameter("@BudgetSource", creation.BudgetSource),
                    new SqlParameter("@YearOfWork", creation.YearOfWork),
                    new SqlParameter("@Type", creation.Type),
                    new SqlParameter("@QuarterMonthType", creation.QuarterMonthType),
                    new SqlParameter("@QuarterMonth", creation.QuarterMonth),
                    new SqlParameter("@AgencyId", creation.AgencyId),
                    new SqlParameter("@AgencyName", creation.AgencyName),
                    new SqlParameter("@UserName", creation.UserName)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<TnDtChungTuChiTietQuery> FindByApprovedAndPlanEstimationCondition(EstimationVoucherDetailCriteria searchCondition, int type)
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
                lnsParam.Value = searchCondition.LNS.ToString();
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

                SqlParameter typeParam = new SqlParameter();
                typeParam.ParameterName = "@Type";
                typeParam.DbType = DbType.Int32;
                typeParam.Value = type;
                typeParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<TnDtChungTuChiTietQuery>("EXECUTE sp_tn_dt_chungtu_chitiet @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @Type",
                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, typeParam).ToList();
            }
        }

        public IEnumerable<TnDtChungTuChiTietReportQuery> FindByPlanBudgetReportCondition(EstimationVoucherDetailCriteria searchCondition, int type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
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

                SqlParameter typeParam = new SqlParameter();
                typeParam.ParameterName = "@Type";
                typeParam.DbType = DbType.Int32;
                typeParam.Value = searchCondition.ILoai;
                typeParam.Direction = ParameterDirection.Input;

                SqlParameter idDonViParam = new SqlParameter();
                idDonViParam.ParameterName = "@IdDonVi";
                idDonViParam.DbType = DbType.String;
                idDonViParam.Value = searchCondition.IdDonVi;
                idDonViParam.Direction = ParameterDirection.Input;

                SqlParameter reportType = new SqlParameter();
                reportType.ParameterName = "@reportType";
                reportType.DbType = DbType.Int32;
                reportType.Value = type;
                reportType.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<TnDtChungTuChiTietReportQuery>("EXECUTE sp_tn_dt_rpt_du_toan_ngan_sach @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @Type, @IdDonVi, @reportType",
                    lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, typeParam, idDonViParam, reportType).ToList();
            }
        }

        public IEnumerable<TnDtChungTuChiTietQuery> FindByRevenueExpendDivisionCondition(EstimationVoucherDetailCriteria searchCondition)
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

                return ctx.FromSqlRaw<TnDtChungTuChiTietQuery>("EXECUTE sp_tn_dt_phanbo_dutoan_chitiet @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource",
                                voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam).ToList();
            }
        }

        public IEnumerable<TnDtChungTuChiTiet> FindByChungtuId(Guid chungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var items = ctx.TnDtChungTuChiTiets.Where(x => x.IdChungTu.HasValue && x.IdChungTu.Value.Equals(chungTuId)).ToList();
                return items;
            }
        }

        public IEnumerable<TnDtChungTuChiTiet> FindByChungtuNhanId(Guid chungTuNhanId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var items = ctx.TnDtChungTuChiTiets.Where(x => x.IdDotNhan.HasValue && x.IdDotNhan.Value.Equals(chungTuNhanId)).ToList();
                return items;
            }
        }
        public IEnumerable<TnDtChungTuChiTietQuery> FindByRevenueExpendDivisionReport(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_pbdt_giao_du_toan_don_vi @IIDMaDonVis, @VoucherIds,@DonViTinh, @YearOfWork, @YearOfBudget,@BudgetSource";
                var parameters = new[]
                {
                    new SqlParameter("@IIDMaDonVis", searchCondition.IIDMaDonVis),
                    new SqlParameter("@VoucherIds", searchCondition.VoucherIds),
                    new SqlParameter("@DonViTinh", searchCondition.DonViTinh),
                    new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                    new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                    new SqlParameter("@BudgetSource", searchCondition.BudgetSource)
                };


                return ctx.FromSqlRaw<TnDtChungTuChiTietQuery>(sql, parameters).ToList();
            }

        }
    }
}
