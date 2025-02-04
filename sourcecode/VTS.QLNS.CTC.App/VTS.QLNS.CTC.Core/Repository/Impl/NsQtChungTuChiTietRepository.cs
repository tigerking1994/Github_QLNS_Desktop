using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsQtChungTuChiTietRepository : Repository<NsQtChungTuChiTiet>, INsQtChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsQtChungTuChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_chungtu_chitiet_tao_tonghop @VoucherIds, @VoucherId, @YearOfBudget, @BudgetSource, @YearOfWork, @Type, @QuarterMonthType, @QuarterMonth, @AgencyId, @UserName";
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
                    new SqlParameter("@UserName", creation.UserName),
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void DeleteByListVoucherId(List<Guid> listVoucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherIds", string.Join(",", listVoucherId));
                ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_QT_ChungTuChiTiet WHERE iID_QTChungTu IN (SELECT * FROM f_split(@VoucherIds))", voucherIdParam);
            }
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherId", voucherId.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_QT_ChungTuChiTiet WHERE iID_QTChungTu = @VoucherId", voucherIdParam);
            }
        }

        public IEnumerable<FindApprovalSettlementYearQuery> FindApprovalSettlementYear(int yearOfWork, string yearOfBuget, string listLns, string listUnit, int dataType, int budgetSource, int unit, string printType)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_ns_rpt_quyettoannam_quyetoan_8063 @YearOfWork, @YearOrBudget, @ListLNS, @ListUnitId, @DataType, @BudgetSource, @dvt, @PrintType";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@YearOrBudget", yearOfBuget),
                    new SqlParameter("@ListLNS", listLns),
                    new SqlParameter("@ListUnitId", listUnit),
                    new SqlParameter("@DataType", dataType),
                    new SqlParameter("@BudgetSource", budgetSource),
                    new SqlParameter("@dvt",unit),
                    new SqlParameter("@PrintType",printType)
                };

                return ctx.FromSqlRaw<FindApprovalSettlementYearQuery>(sql, parameters);
            }
        }

        public NsQtChungTuChiTiet FindByChungTuIdAndMlnsId(Guid voucherId, string mlnsId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQtChungTuChiTiets.Where(x => x.IIdQtchungTu == voucherId && x.IIdMlns.ToString() == mlnsId).FirstOrDefault();
            }
        }

        /// <summary>
        /// Lấy ra danh sách chứng từ chi tiết theo chứng từ id
        /// </summary>
        /// <param name="searchCondition">Điều kiện tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<QtChungTuChiTietQuery> FindByCondition(SettlementVoucherDetailSearch searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_chungtu_chitiet @VoucherId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @Type, @AgencyId, @VoucherDate, @UserName, @QuarterMonth";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", searchCondition.VoucherId),
                    new SqlParameter("@LNS", searchCondition.LNS),
                    new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                    new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                    new SqlParameter("@BudgetSource", searchCondition.BudgetSource),
                    new SqlParameter("@Type", searchCondition.Type),
                    new SqlParameter("@AgencyId", searchCondition.AgencyId),
                    new SqlParameter("@VoucherDate", searchCondition.VoucherDate),
                    new SqlParameter("@UserName", searchCondition.UserName),
                    new SqlParameter("@QuarterMonth", searchCondition.QuarterMonth)
                };

                var rs = ctx.FromSqlRaw<QtChungTuChiTietQuery>(sql, parameters);
                return rs;
            }
        }

        public IEnumerable<NsQtChungTuChiTiet> FindBySummaryReport(SettlementVoucherDetailSearch searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                IEnumerable<NsQtChungTuChiTiet> result;
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
                SqlParameter quarterMonthTypeParam = new SqlParameter("@QuarterMonthType", searchCondition.QuarterMonthType);
                SqlParameter quarterMonthParam = new SqlParameter("@QuarterMonth", searchCondition.QuarterMonth);
                result = ctx.NsQtChungTuChiTiets.FromSql("EXECUTE dbo.sp_qt_chungtu_chitiet_tonghop @YearOfWork, @QuarterMonthType, @QuarterMonth",
                    yearOfWorkParam, quarterMonthTypeParam, quarterMonthParam).ToList();
                return result;
            }
        }

        public IEnumerable<ReportQtDuToanQuyetToanThangQuery> FindForEstimateSettlementMonthReport(ReportSettlementCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_dutoan_donvi_thang @YearOfWork, @YearOfBudget, @BudgetSource, @EstimateAgencyId, @AgencyId, @LNS, @QuarterMonth, @QuarterMonthBefore, @VoucherDate, @Dvt";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", condition.YearOfWork),
                    new SqlParameter("@YearOfBudget", condition.YearOfBudget),
                    new SqlParameter("@BudgetSource", condition.BudgetSource),
                    new SqlParameter("@EstimateAgencyId", condition.EstimateAgencyId),
                    new SqlParameter("@AgencyId", condition.AgencyId),
                    new SqlParameter("@LNS", condition.LNS),
                    new SqlParameter("@QuarterMonth", condition.QuarterMonth),
                    new SqlParameter("@QuarterMonthBefore", condition.QuarterMonthBefore),
                    new SqlParameter("@VoucherDate", condition.VoucherDate),
                    new SqlParameter("@Dvt", condition.Dvt)
                };
                return ctx.FromSqlRaw<ReportQtDuToanQuyetToanThangQuery>(sql, parameters);
            }
        }

        public IEnumerable<ReportQtDuToanQuyetToanThangQuery> FindForEstimateSettlementMonthReportClone(ReportSettlementCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_dutoan_donvi_thang_clone @YearOfWork, @YearOfBudget, @BudgetSource, @EstimateAgencyId, @AgencyId, @LNS, @QuarterMonth, @QuarterMonthBefore, @VoucherDate, @Dvt";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", condition.YearOfWork),
                    new SqlParameter("@YearOfBudget", condition.YearOfBudget),
                    new SqlParameter("@BudgetSource", condition.BudgetSource),
                    new SqlParameter("@EstimateAgencyId", condition.EstimateAgencyId),
                    new SqlParameter("@AgencyId", condition.AgencyId),
                    new SqlParameter("@LNS", condition.LNS),
                    new SqlParameter("@QuarterMonth", condition.QuarterMonth),
                    new SqlParameter("@QuarterMonthBefore", condition.QuarterMonthBefore),
                    new SqlParameter("@VoucherDate", condition.VoucherDate),
                    new SqlParameter("@Dvt", condition.Dvt)
                };
                return ctx.FromSqlRaw<ReportQtDuToanQuyetToanThangQuery>(sql, parameters);
            }
        }

        public IEnumerable<ReportQtDuToanQuyetToanQuyQuery> FindForEstimateSettlementQuarterReport(ReportSettlementCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_dutoan_donvi_quy @YearOfWork, @YearOfBudget, @BudgetSource, @EstimateAgencyId, @AgencyId, @LNS, @QuarterMonth, @QuarterMonthBefore, @VoucherDate, @Dvt";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", condition.YearOfWork),
                    new SqlParameter("@YearOfBudget", condition.YearOfBudget),
                    new SqlParameter("@BudgetSource", condition.BudgetSource),
                    new SqlParameter("@EstimateAgencyId", condition.EstimateAgencyId),
                    new SqlParameter("@AgencyId", condition.AgencyId),
                    new SqlParameter("@LNS", condition.LNS),
                    new SqlParameter("@QuarterMonth", condition.QuarterMonth),
                    new SqlParameter("@QuarterMonthBefore", condition.QuarterMonthBefore),
                    new SqlParameter("@VoucherDate", condition.VoucherDate),
                    new SqlParameter("@Dvt", condition.Dvt)
                };
                return ctx.FromSqlRaw<ReportQtDuToanQuyetToanQuyQuery>(sql, parameters);
            }
        }

        public IEnumerable<ReportQtDuToanQuyetToanQuyQuery> FindForEstimateSettlementQuarterReportClone(ReportSettlementCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_dutoan_donvi_quy_clone @YearOfWork, @YearOfBudget, @BudgetSource, @EstimateAgencyId, @AgencyId, @LNS, @QuarterMonth, @QuarterMonthBefore, @VoucherDate, @Dvt";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", condition.YearOfWork),
                    new SqlParameter("@YearOfBudget", condition.YearOfBudget),
                    new SqlParameter("@BudgetSource", condition.BudgetSource),
                    new SqlParameter("@EstimateAgencyId", condition.EstimateAgencyId),
                    new SqlParameter("@AgencyId", condition.AgencyId),
                    new SqlParameter("@LNS", condition.LNS),
                    new SqlParameter("@QuarterMonth", condition.QuarterMonth),
                    new SqlParameter("@QuarterMonthBefore", condition.QuarterMonthBefore),
                    new SqlParameter("@VoucherDate", condition.VoucherDate),
                    new SqlParameter("@Dvt", condition.Dvt)
                };
                return ctx.FromSqlRaw<ReportQtDuToanQuyetToanQuyQuery>(sql, parameters);
            }
        }

        public IEnumerable<ReportQtDuToanQuyetToanQuery> FindForEstimateSettlementReport(ReportSettlementCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_dutoan_donvi @YearOfWork, @YearOfBudget, @BudgetSource, @EstimateAgencyId, @AgencyId, @LNS, @QuarterMonth, @QuarterMonthBefore, @VoucherDate, @Dvt";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", condition.YearOfWork),
                    new SqlParameter("@YearOfBudget", condition.YearOfBudget),
                    new SqlParameter("@BudgetSource", condition.BudgetSource),
                    new SqlParameter("@EstimateAgencyId", condition.EstimateAgencyId),
                    new SqlParameter("@AgencyId", condition.AgencyId),
                    new SqlParameter("@LNS", condition.LNS),
                    new SqlParameter("@QuarterMonth", condition.QuarterMonth),
                    new SqlParameter("@QuarterMonthBefore", condition.QuarterMonthBefore),
                    new SqlParameter("@VoucherDate", condition.VoucherDate),
                    new SqlParameter("@Dvt", condition.Dvt)
                };
                return ctx.FromSqlRaw<ReportQtDuToanQuyetToanQuery>(sql, parameters);
            }
        }

        public IEnumerable<ReportQtDuToanQuyetToanQuery> FindForEstimateSettlementReportClone(ReportSettlementCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_dutoan_donvi_clone @YearOfWork, @YearOfBudget, @BudgetSource, @EstimateAgencyId, @AgencyId, @LNS, @QuarterMonth, @QuarterMonthBefore, @VoucherDate, @Dvt";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", condition.YearOfWork),
                    new SqlParameter("@YearOfBudget", condition.YearOfBudget),
                    new SqlParameter("@BudgetSource", condition.BudgetSource),
                    new SqlParameter("@EstimateAgencyId", condition.EstimateAgencyId),
                    new SqlParameter("@AgencyId", condition.AgencyId),
                    new SqlParameter("@LNS", condition.LNS),
                    new SqlParameter("@QuarterMonth", condition.QuarterMonth),
                    new SqlParameter("@QuarterMonthBefore", condition.QuarterMonthBefore),
                    new SqlParameter("@VoucherDate", condition.VoucherDate),
                    new SqlParameter("@Dvt", condition.Dvt)
                };
                return ctx.FromSqlRaw<ReportQtDuToanQuyetToanQuery>(sql, parameters);
            }
        }

        public IEnumerable<ReportQtDuToanQuyetToanTongThangQuery> FindForEstimateSettlementSummaryMonthReport(ReportSettlementCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_dutoan_tonghop_thang @YearOfWork, @YearOfBudget, @BudgetSource, @EstimateAgencyId, @AgencyId, @LNS, @QuarterMonth, @QuarterMonthBefore, @VoucherDate, @Dvt";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", condition.YearOfWork),
                    new SqlParameter("@YearOfBudget", condition.YearOfBudget),
                    new SqlParameter("@BudgetSource", condition.BudgetSource),
                    new SqlParameter("@EstimateAgencyId", condition.EstimateAgencyId),
                    new SqlParameter("@AgencyId", condition.AgencyId),
                    new SqlParameter("@LNS", condition.LNS),
                    new SqlParameter("@QuarterMonth", condition.QuarterMonth),
                    new SqlParameter("@QuarterMonthBefore", condition.QuarterMonthBefore),
                    new SqlParameter("@VoucherDate", condition.VoucherDate),
                    new SqlParameter("@Dvt", condition.Dvt)
                };
                return ctx.FromSqlRaw<ReportQtDuToanQuyetToanTongThangQuery>(sql, parameters);
            }
        }

        public IEnumerable<ReportQtChungTuChiTietQuery> FindForReportQuarterlySummary(ReportSettlementCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_chungtu_tonghop @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @EstimateAgencyId, @AgencyId, @VoucherDate, @QuarterMonth, @QuarterMonthBefore, @Dvt";
                var parameters = new[]
                {
                    new SqlParameter("@LNS", condition.LNS),
                    new SqlParameter("@YearOfWork", condition.YearOfWork),
                    new SqlParameter("@YearOfBudget", condition.YearOfBudget),
                    new SqlParameter("@BudgetSource", condition.BudgetSource),
                    new SqlParameter("@EstimateAgencyId", condition.EstimateAgencyId),
                    new SqlParameter("@AgencyId", condition.AgencyId),
                    new SqlParameter("@VoucherDate", condition.VoucherDate),
                    new SqlParameter("@QuarterMonth", condition.QuarterMonth),
                    new SqlParameter("@QuarterMonthBefore", condition.QuarterMonthBefore),
                    new SqlParameter("@Dvt", condition.Dvt)
                };
                return ctx.FromSqlRaw<ReportQtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }
        public IEnumerable<ReportQtChungTuChiTietQuery> FindForReportQuarterlySummary2Year(ReportSettlementCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_chungtu_tonghop_2_year @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @EstimateAgencyId, @AgencyId, @VoucherDate, @QuarterMonth, @QuarterMonthBefore, @Dvt";
                var parameters = new[]
                {
                    new SqlParameter("@LNS", condition.LNS),
                    new SqlParameter("@YearOfWork", condition.YearOfWork),
                    new SqlParameter("@YearOfBudget", condition.YearOfBudgets),
                    new SqlParameter("@BudgetSource", condition.BudgetSource),
                    new SqlParameter("@EstimateAgencyId", condition.EstimateAgencyId),
                    new SqlParameter("@AgencyId", condition.AgencyId),
                    new SqlParameter("@VoucherDate", condition.VoucherDate),
                    new SqlParameter("@QuarterMonth", condition.QuarterMonth),
                    new SqlParameter("@QuarterMonthBefore", condition.QuarterMonthBefore),
                    new SqlParameter("@Dvt", condition.Dvt)
                };
                return ctx.FromSqlRaw<ReportQtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportQtThongTriDonViQuery> FindForSettlementAgencyReport(int yearOfWork, int yearOfBudget, int budgetSource, string agencyId, string quarterMonth, string LNS, int dvt, bool IsInTongHop, int IKhoi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                IEnumerable<ReportQtThongTriDonViQuery> result;
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", yearOfBudget);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", budgetSource);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", agencyId);
                SqlParameter quarterMonthParam = new SqlParameter("@QuarterMonth", quarterMonth);
                SqlParameter lnsParam = new SqlParameter("@LNS", LNS);
                SqlParameter dvtParam = new SqlParameter("@Dvt", dvt);
                SqlParameter isInTongHopParam = new SqlParameter("@IsInTongHop", IsInTongHop);
                SqlParameter iKhoiParam = new SqlParameter("@IKhoi", IKhoi);
                result = ctx.FromSqlRaw<ReportQtThongTriDonViQuery>("EXECUTE dbo.sp_qt_rpt_thongtri_donvi @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @QuarterMonth, @LNS, @Dvt, @IsInTongHop, @IKhoi",
                    yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, agencyIdParam, quarterMonthParam, lnsParam, dvtParam, isInTongHopParam, iKhoiParam).ToList();
                return result;
            }
        }

        public IEnumerable<ReportQtThongTriLNSQuery> FindForSettlementLNSReport(SettlementVoucherDetailSearch searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                IEnumerable<ReportQtThongTriLNSQuery> result;
                SqlParameter voucherIdParam = new SqlParameter("@VoucherId", searchCondition.VoucherId);
                SqlParameter lnsParam = new SqlParameter("@LNS", searchCondition.LNS);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", searchCondition.AgencyId);
                SqlParameter dvtParam = new SqlParameter("@Dvt", searchCondition.Dvt);
                result = ctx.FromSqlRaw<ReportQtThongTriLNSQuery>("EXECUTE dbo.sp_qt_rpt_thongtri_lns @VoucherId, @LNS, @YearOfWork, @AgencyId, @Dvt",
                    voucherIdParam, lnsParam, yearOfWorkParam, agencyIdParam, dvtParam).ToList();
                return result;
            }
        }

        public IEnumerable<ReportQtThongTriLNSQuery> FindForSettlementLNSSoChungTu(SettlementVoucherDetailSearch searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                IEnumerable<ReportQtThongTriLNSQuery> result;
                SqlParameter voucherNameParam = new SqlParameter("@VoucherName", searchCondition.VoucherName);
                SqlParameter voucherTypeParam = new SqlParameter("@VoucherType", searchCondition.Type);
                SqlParameter quarterMonthParam = new SqlParameter("@QuarterMonth", searchCondition.QuarterMonth);
                SqlParameter quarterMonthTypeParam = new SqlParameter("@QuarterMonthType", searchCondition.QuarterMonthType);
                SqlParameter lnsParam = new SqlParameter("@LNS", searchCondition.LNS);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
                SqlParameter dvtParam = new SqlParameter("@Dvt", searchCondition.Dvt);
                result = ctx.FromSqlRaw<ReportQtThongTriLNSQuery>("EXECUTE dbo.sp_qt_rpt_thongtri_lns_sochungtu @VoucherName, @VoucherType, @LNS, @YearOfWork, @QuarterMonth, @QuarterMonthType, @Dvt",
                    voucherNameParam, voucherTypeParam, lnsParam, yearOfWorkParam, quarterMonthParam, quarterMonthTypeParam, dvtParam).ToList();
                return result;
            }
        }

        public IEnumerable<ReportQtChungTuChiTietQuery> FindForSettlementVoucherReport(SettlementVoucherDetailSearch searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_chung_tu @VoucherId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @QuarterMonthType, @QuarterMonth, @QuarterMonthBefore,  @VoucherDate, @Dvt";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", searchCondition.VoucherId),
                    new SqlParameter("@LNS", searchCondition.LNS),
                    new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                    new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                    new SqlParameter("@BudgetSource", searchCondition.BudgetSource),
                    new SqlParameter("@AgencyId", searchCondition.AgencyId),
                    new SqlParameter("@QuarterMonthType", searchCondition.QuarterMonthType),
                    new SqlParameter("@QuarterMonth", searchCondition.QuarterMonth),
                    new SqlParameter("@QuarterMonthBefore", searchCondition.QuarterMonthBefore),
                    new SqlParameter("@VoucherDate", searchCondition.VoucherDate),
                    new SqlParameter("@Dvt", searchCondition.Dvt)
                };
                return ctx.FromSqlRaw<ReportQtChungTuChiTietQuery>(sql, parameters);
            }
        }

        public IEnumerable<ReportQuyetToanTongHopDonViQuery> FindForSummaryAgencyReport(ReportSettlementCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_tonghop_donvi @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @EstimateAgencyId, @QuarterMonth, @QuarterMonthBefore, @LNS, @VoucherDate, @Dvt, @IsAccumulated";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                    new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                    new SqlParameter("@BudgetSource", searchCondition.BudgetSource),
                    new SqlParameter("@AgencyId", searchCondition.AgencyId),
                    new SqlParameter("@EstimateAgencyId", searchCondition.EstimateAgencyId),
                    new SqlParameter("@QuarterMonth", searchCondition.QuarterMonth),
                    new SqlParameter("@QuarterMonthBefore", searchCondition.QuarterMonthBefore),
                    new SqlParameter("@LNS", searchCondition.LNS),
                    new SqlParameter("@VoucherDate", searchCondition.VoucherDate),
                    new SqlParameter("@Dvt", searchCondition.Dvt),
                    new SqlParameter("@IsAccumulated", searchCondition.IsAccumulated),
                };
                return ctx.FromSqlRaw<ReportQuyetToanTongHopDonViQuery>(sql, parameters);
            }
        }

        public IEnumerable<ReportQtTongHopNamQuery> FindForSummaryYearSettlementReport(ReportSettlementCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                IEnumerable<ReportQtTongHopNamQuery> result;
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", searchCondition.AgencyId);
                SqlParameter lnsParam = new SqlParameter("@LNS", searchCondition.LNS);
                SqlParameter dataTypeParam = new SqlParameter("@DataType", searchCondition.DataType);
                SqlParameter dvtParam = new SqlParameter("@Dvt", searchCondition.Dvt);
                result = ctx.FromSqlRaw<ReportQtTongHopNamQuery>("EXECUTE dbo.sp_qt_rpt_quyetoannam_donvi  @YearOfWork, @AgencyId, @LNS, @DataType, @Dvt",
                    yearOfWorkParam, agencyIdParam, lnsParam, dataTypeParam, dvtParam).ToList();
                return result;
            }
        }

        public IEnumerable<ReportQtTongHopNamQuery> FindForSummaryYearSettlementReportUpdate(ReportSettlementCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                IEnumerable<ReportQtTongHopNamQuery> result;
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", searchCondition.AgencyId);
                SqlParameter lnsParam = new SqlParameter("@LNS", searchCondition.LNS);
                SqlParameter dataTypeParam = new SqlParameter("@DataType", searchCondition.DataType);
                SqlParameter dvtParam = new SqlParameter("@Dvt", searchCondition.Dvt);
                SqlParameter yearOfBugetParam = new SqlParameter("@YearOfBuget", searchCondition.YearOfBudget);
                result = ctx.FromSqlRaw<ReportQtTongHopNamQuery>("EXECUTE dbo.sp_qt_rpt_quyetoannam_donvi_index1  @YearOfWork, @AgencyId, @LNS, @DataType, @Dvt, @YearOfBuget",
                    yearOfWorkParam, agencyIdParam, lnsParam, dataTypeParam, dvtParam, yearOfBugetParam).ToList();
                return result;
            }
        }

        public List<ReportQtNhanQuyetToanKinhPhiQuery> FindForReceiveSettlementReport(ReportSettlementCriteria searchCondition)
        {
            using var ctx = _contextFactory.CreateDbContext();
            SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
            SqlParameter agencyIdParam = new SqlParameter("@AgencyId", searchCondition.AgencyId);
            SqlParameter maStringParam = new SqlParameter("@MaString", searchCondition.MaString);
            SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", searchCondition.YearOfBudgets);
            SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", searchCondition.BudgetSource);
            SqlParameter maBQuanLyParam = new SqlParameter("@MaBQuanLy", searchCondition.MaBQuanLy);
            SqlParameter dvtParam = new SqlParameter("@Dvt", searchCondition.Dvt);
            
            return ctx.FromSqlRaw<ReportQtNhanQuyetToanKinhPhiQuery>("EXECUTE dbo.sp_qt_rpt_nhan_quyettoankinhphi @YearOfWork, @AgencyId, @MaString, @YearOfBudget, @BudgetSource, @MaBQuanLy, @Dvt",
                yearOfWorkParam, agencyIdParam, maStringParam, yearOfBudgetParam, budgetSourceParam, maBQuanLyParam, dvtParam).ToList();
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQtChungTuChiTiets.Where(x => x.IIdQtchungTu.HasValue && chungTuIds.Contains(x.IIdQtchungTu.Value) && (x.FTuChiDeNghi != 0 || x.FTuChiPheDuyet != 0 || x.FSoNgay != 0 ||
                                                    x.FSoNguoi != 0 || x.FSoLuot != 0 || !string.IsNullOrEmpty(x.SGhiChu))).Select(x => x.SLns).Distinct().ToList();
            }
        }

        public void UpdateMonth(Guid voucherId, int month, int monthType, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_chungtu_chitiet_update_month @VoucherId, @Thang, @LoaiThang, @UserName";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@LoaiThang", monthType),
                    new SqlParameter("@UserName", userName),
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }


        public IEnumerable<PrintYearSettlementQuery> FindSettlementYear(int yearOfWork, string yearOfBuget, string listLns, string listUnit, int dataType, int budgetSource, int unit, string printType)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_ns_rpt_quyettoannam_quyetoan_8568 @YearOfWork, @YearOfBudget, @ListLNS, @ListUnitId, @DataType, @BudgetSource, @dvt, @PrintType";

                var yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                var yearOfBudgetParam = new SqlParameter("@YearOfBudget", yearOfBuget);
                var listLnsParam = new SqlParameter("@ListLNS", listLns);
                var listUnitParam = new SqlParameter("@ListUnitId", listUnit);
                var dataTypeParam = new SqlParameter("@DataType", dataType);
                var budgetParam = new SqlParameter("@BudgetSource", budgetSource);
                var unitParam = new SqlParameter("@dvt", unit);
                var reportType = new SqlParameter("@PrintType", printType);

                return ctx.FromSqlRaw<PrintYearSettlementQuery>(sql, yearOfWorkParam, yearOfBudgetParam, listLnsParam, listUnitParam, dataTypeParam, budgetParam, unitParam, reportType).ToList();
            }
        }

        public List<NsQtChungTuChiTiet> FindByParentId(List<Guid> iIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQtChungTuChiTiets.Where(n => n.IIdQtchungTu.HasValue && iIds.Contains(n.IIdQtchungTu.Value)).ToList();
            }
        }

        public IEnumerable<NsQuyetToanChiTietCongKhaiQuery> FindQTChungTuChiTietCongKhai(EstimationVoucherDetailCriteria criteria)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter danhSachId = new SqlParameter("@ListIdPublic", criteria.LNS);
                SqlParameter namLamViec = new SqlParameter("@YearOfWork", criteria.YearOfWork);
                SqlParameter namNganSach = new SqlParameter("@YearOfBudget", criteria.YearOfBudget);
                SqlParameter nguonNganSach = new SqlParameter("@BudgetSource", criteria.BudgetSource);
                SqlParameter thoiGian = new SqlParameter("@Time", criteria.IThangQuy);

                return ctx.FromSqlRaw<NsQuyetToanChiTietCongKhaiQuery>("EXECUTE sp_dt_quyettoan_mucluccongkhai @ListIdPublic, @YearOfWork, @YearOfBudget, @BudgetSource, @Time", danhSachId, namLamViec, namNganSach, nguonNganSach, thoiGian);

            }
        }

    }
}
