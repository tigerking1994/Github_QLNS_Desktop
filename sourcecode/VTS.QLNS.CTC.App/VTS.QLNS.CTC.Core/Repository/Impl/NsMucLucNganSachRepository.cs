using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsMucLucNganSachRepository : Repository<NsMucLucNganSach>, INsMucLucNganSachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsMucLucNganSachRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NsMucLucNganSach> FindByNamLamViec(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucNganSaches.Where(n => n.NamLamViec == namLamViec
                                                          && string.IsNullOrEmpty(n.L)
                                                          && string.IsNullOrEmpty(n.K)
                                                          && string.IsNullOrEmpty(n.M)
                                                          && string.IsNullOrEmpty(n.Tm)
                                                          && string.IsNullOrEmpty(n.Ttm)
                                                          && string.IsNullOrEmpty(n.Ng)).OrderBy(n => n.Lns).ToList();
            }
        }
        public IEnumerable<NsMucLucNganSach> FindMLNSByNamLamViec(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucNganSaches.Where(n => n.NamLamViec == namLamViec && n.Lns.Substring(0, 1) == "3").OrderBy(n => n.Lns).ToList();
            }
        }

        public List<NsMucLucNganSach> FindByDefenseBudget(BudgetIndexForBudgetCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NsMucLucNganSach> result = new List<NsMucLucNganSach>();
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
                SqlParameter LNSParam = new SqlParameter("@LNS", searchCondition.LNS);
                SqlParameter generateAgencyIdParam = new SqlParameter("@GenerateAgencyId", searchCondition.GenerateAgencyId);
                result = ctx.NsMucLucNganSaches.FromSql("EXEC dbo.sp_mlns_quocphong @YearOfWork, @LNS, @GenerateAgencyId", yearOfWorkParam, LNSParam, generateAgencyIdParam).ToList();
                return result;
            }
        }

        public List<NsMucLucNganSach> FindByStateBudget(BudgetIndexForBudgetCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NsMucLucNganSach> result = new List<NsMucLucNganSach>();
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
                SqlParameter LNSParam = new SqlParameter("@LNS", searchCondition.LNS);
                SqlParameter generateAgencyIdParam = new SqlParameter("@GenerateAgencyId", searchCondition.GenerateAgencyId);
                SqlParameter userNameParam = new SqlParameter("@UserName", searchCondition.UserName);
                result = ctx.NsMucLucNganSaches.FromSql("EXEC dbo.sp_mlns_nhanuoc @YearOfWork, @LNS, @GenerateAgencyId, @UserName", yearOfWorkParam, LNSParam, generateAgencyIdParam, userNameParam).ToList();
                return result;
            }
        }

        public IEnumerable<NsMucLucNganSach> FindByListLnsDonVi(string lns, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                if (String.IsNullOrEmpty(lns))
                {
                    return new List<NsMucLucNganSach>();
                }
                List<string> listId = lns.Split(',').ToList();
                return ctx.NsMucLucNganSaches.Where(n => listId.Contains(n.Lns)
                                                         && n.NamLamViec == namLamViec
                                                         && string.IsNullOrEmpty(n.L)
                                                         && string.IsNullOrEmpty(n.K)
                                                         && string.IsNullOrEmpty(n.M)
                                                         && string.IsNullOrEmpty(n.Tm)
                                                         && string.IsNullOrEmpty(n.Ttm)
                                                         && string.IsNullOrEmpty(n.Ng)).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindByListLnsDonVi(List<string> lns, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucNganSaches.Where(n => lns.Contains(n.Lns)
                                                         && n.NamLamViec == namLamViec
                                                         && string.IsNullOrEmpty(n.L)
                                                         && string.IsNullOrEmpty(n.K)
                                                         && string.IsNullOrEmpty(n.M)
                                                         && string.IsNullOrEmpty(n.Tm)
                                                         && string.IsNullOrEmpty(n.Ttm)
                                                         && string.IsNullOrEmpty(n.Ng)).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindByLnsCondition(string chungTuId, int namLamViec, DateTime ngayChungTu, int type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuIdParam = new SqlParameter();
                chungTuIdParam.ParameterName = "@ChungTuId";
                chungTuIdParam.DbType = DbType.String;
                chungTuIdParam.Value = chungTuId;
                chungTuIdParam.Direction = ParameterDirection.Input;

                SqlParameter voucherDate = new SqlParameter();
                voucherDate.ParameterName = "@VoucherDate";
                voucherDate.DbType = DbType.DateTime;
                voucherDate.Value = ngayChungTu;
                voucherDate.Direction = ParameterDirection.Input;

                SqlParameter typeParam = new SqlParameter();
                typeParam.ParameterName = "@Type";
                typeParam.DbType = DbType.Int32;
                typeParam.Value = type;
                typeParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter("@NamLamViec", namLamViec);

                return ctx.NsMucLucNganSaches.FromSql("EXEC dbo.sp_dt_rpt_get_lns1 @NamLamViec,@ChungTuId,@VoucherDate,@Type", yearOfWorkParam,
                    chungTuIdParam, voucherDate, typeParam).ToList();
            }
        }

        public List<NsMucLucNganSach> FindByParentCd(int iNamKeHoach, Guid iIdLoaiNganSach, string sL, string sK, string sM, string sTM, string sTTM)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucNganSaches.FromSql("EXEC sp_ns_find_muclucngansach_by_parentcd @iNamKeHoach, @iIdLoaiNganSach, @sL, @sK, @sM, @sTM, @sTTM",
                                                            new SqlParameter("@iNamKeHoach", iNamKeHoach),
                                                            new SqlParameter("@iIdLoaiNganSach", iIdLoaiNganSach),
                                                            new SqlParameter("@sL", sL),
                                                            new SqlParameter("@sK", sK),
                                                            new SqlParameter("@sM", sM),
                                                            new SqlParameter("@sTM", sTM),
                                                            new SqlParameter("@sTTM", sTTM)).ToList();
            }
        }

        public IEnumerable<LNSQuery> FindBySettlementMonth(int yearOfWork, int budgetSource, string agencyId, string quarterMonth, int quarterMonthType)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", budgetSource);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyIdParam", agencyId);
                SqlParameter quaterMonthParam = new SqlParameter("@QuarterMonth", quarterMonth);
                SqlParameter quaterMonthTypeParam = new SqlParameter("@QuarterMonthType", quarterMonthType);
                return ctx.FromSqlRaw<LNSQuery>("EXECUTE dbo.sp_qt_rpt_lns_thang @YearOfWork, @BudgetSource, @AgencyIdParam, @QuarterMonth, @QuarterMonthType",
                    yearOfWorkParam, budgetSourceParam, agencyIdParam, quaterMonthParam, quaterMonthTypeParam).ToList();
            }
        }

        public IEnumerable<LNSQuery> FindBySettlementMonth(int yearOfWork, int budgetSource, string agencyId, string quarterMonth, int quarterMonthType, string loaiQuyetToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", budgetSource);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyIdParam", agencyId);
                SqlParameter quaterMonthParam = new SqlParameter("@QuarterMonth", quarterMonth);
                SqlParameter quaterMonthTypeParam = new SqlParameter("@QuarterMonthType", quarterMonthType);
                SqlParameter loaiQuyetToanParam = new SqlParameter("@LoaiQuyetToan", loaiQuyetToan);
                return ctx.FromSqlRaw<LNSQuery>("EXECUTE dbo.sp_qt_rpt_lns_thang_2 @YearOfWork, @BudgetSource, @AgencyIdParam, @QuarterMonth, @QuarterMonthType, @LoaiQuyetToan",
                    yearOfWorkParam, budgetSourceParam, agencyIdParam, quaterMonthParam, quaterMonthTypeParam, loaiQuyetToanParam).ToList();
            }
        }

        public IEnumerable<LNSQuery> FindBySettlementEstimateMonth(int yearOfWork, int yearOfBudget, int budgetSource, string agencyId, string quarterMonth, int quarterMonthType)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", yearOfBudget);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", budgetSource);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyIdParam", agencyId);
                SqlParameter quaterMonthParam = new SqlParameter("@QuarterMonth", quarterMonth);
                SqlParameter quaterMonthTypeParam = new SqlParameter("@QuarterMonthType", quarterMonthType);
                return ctx.FromSqlRaw<LNSQuery>("EXECUTE dbo.sp_qt_dt_rpt_lns_thang @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyIdParam, @QuarterMonth, @QuarterMonthType",
                    yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, agencyIdParam, quaterMonthParam, quaterMonthTypeParam).ToList();
            }
        }

        public IEnumerable<ReportMLNSQuery> FindChildMlns(Guid mlnsId, IEnumerable<string> mlnsIdInclude, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var mlnsIdParam = new SqlParameter("@MLNS_ID", mlnsId);
                var mlnsIdIncludeParam = new SqlParameter("@MLNS_ID_INCLUDE", string.Join(",", mlnsIdInclude));
                var yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);

                return ctx.FromSqlRaw<ReportMLNSQuery>("EXECUTE rpt_mlns_child @MLNS_ID, @MLNS_ID_INCLUDE, @YearOfWork", mlnsIdParam, mlnsIdIncludeParam, yearOfWorkParam).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindByVoucherlist(int yearOfWork, int yearOfBudget, int budgetSource, VoucherListLNS displayType)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = string.Empty;
                if (displayType == VoucherListLNS.ESTIMATE_SETTLEMENT)
                    sql = "EXECUTE dbo.sp_bk_mlns_dutoan_quyettoan @YearOfWork, @YearOfBudget, @BudgetSource";
                else sql = "EXECUTE dbo.sp_bk_mlns_bangke @YearOfWork, @YearOfBudget, @BudgetSource";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@YearOfBudget", yearOfBudget),
                    new SqlParameter("@BudgetSource", budgetSource)
                };
                return ctx.Set<NsMucLucNganSach>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<LNSQuery> FindBySummaryYearSettlement(int yearOfWork, int budgetSource, int dataType, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", budgetSource);
                SqlParameter dataTypeParam = new SqlParameter("@DataType", dataType);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                return ctx.FromSqlRaw<LNSQuery>("EXECUTE dbo.sp_qt_tonghop_nam_lns @YearOfWork, @BudgetSource, @DataType, @UserName",
                    yearOfWorkParam, budgetSourceParam, dataTypeParam, userNameParam).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindByLoaiNganSach(string lstNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucNganSaches.Where(n => lstNamLamViec.Contains(n.NamLamViec.ToString())
                                                          && n.ITrangThai == NSEntityStatus.ACTIVED
                                                          && string.IsNullOrEmpty(n.L)
                                                          && string.IsNullOrEmpty(n.K)
                                                          && string.IsNullOrEmpty(n.M)
                                                          && string.IsNullOrEmpty(n.Tm)
                                                          && string.IsNullOrEmpty(n.Ttm)
                                                          && string.IsNullOrEmpty(n.Ng)).OrderBy(n => n.NamLamViec).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindBySummaryVoucherList(int yearOfWork, int quarterMonth)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bk_mlns_tonghop @YearOfWork, @QuarterMonth";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@QuarterMonth", quarterMonth)
                };
                return ctx.Set<NsMucLucNganSach>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportMLNSQuery> FindChildMlnsByParent(string mlnsId, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var mlnsIdParam = new SqlParameter("@MLNS_ID", mlnsId);
                var yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<ReportMLNSQuery>("EXECUTE rpt_mlns_by_parent @MLNS_ID, @YearOfWork", mlnsIdParam, yearOfWorkParam).ToList();
            }
        }

        public IEnumerable<ReportMLNSQuery> FindChildMlnsByParentLNS(string mlnsId, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var mlnsIdParam = new SqlParameter("@MLNS_ID", mlnsId);
                var yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<ReportMLNSQuery>("EXECUTE rpt_mlns_by_parent_lns @MLNS_ID, @YearOfWork", mlnsIdParam, yearOfWorkParam).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindByUser(string userName, int yearOfWork, string LNS, string LNSExcept)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter lnsParam = new SqlParameter("@LNS", LNS);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter lnsExceptParam = new SqlParameter("@LNSExcept", LNSExcept);
                return ctx.NsMucLucNganSaches.FromSql("EXECUTE dbo.sp_ns_mlns_get_by_user @LNS, @UserName, @YearOfWork, @LNSExcept",
                    lnsParam, userNameParam, yearOfWorkParam, lnsExceptParam).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindForPhuCap(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<string> lstLNS = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" };
                return ctx.NsMucLucNganSaches.Where(x => lstLNS.Contains(x.Lns) && x.NamLamViec == namLamViec).ToList();
                //return ctx.NsMucLucNganSaches.FromSql("EXECUTE dbo.sp_tl_phucap_mlns").ToList();   
            }
        }

        public IEnumerable<NsMucLucNganSach> FindByLnsAndNam(string lns, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucNganSaches.Where(x => x.Lns.Equals(lns) && x.NamLamViec == nam).OrderBy(x => x.XauNoiMa).ToList();
            }
        }

        public IEnumerable<LNSQuery> FindBySoQuyetDinhDuToan(int yearOfWork, int yearOfBudget, int budgetSource, string soQuyetDinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", yearOfBudget);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", budgetSource);
                SqlParameter soQuyetDinhParam = new SqlParameter("@SoQuyetDinh", soQuyetDinh);
                return ctx.FromSqlRaw<LNSQuery>("EXECUTE dbo.sp_dt_rpt_lns_theo_so_quyet_dinh @YearOfWork, @YearOfBudget, @BudgetSource, @SoQuyetDinh",
                    yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, soQuyetDinhParam).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindForDieuChinh(int namLamViec, int namNganSach, int nguonNganSach, string donVi, int loaiChungTu, DateTime ngayChungTu, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@namNganSach", namNganSach);
                SqlParameter nguonNganSachParam = new SqlParameter("@nguonNganSach", nguonNganSach);
                SqlParameter donViParam = new SqlParameter("@donVi", donVi);
                SqlParameter loaiChungTuParam = new SqlParameter("@loaiChungTu", loaiChungTu);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", ngayChungTu);
                SqlParameter userNameParam = new SqlParameter("@userName", userName);
                return ctx.NsMucLucNganSaches.FromSql("EXECUTE dbo.sp_dc_get_lns @namLamViec, @namNganSach, @nguonNganSach, @donVi, @loaiChungTu, @ngayChungTu, @userName",
                    namLamViecParam, namNganSachParam, nguonNganSachParam, donViParam, loaiChungTuParam, ngayChungTuParam, userNameParam).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindBySummaryAgencySettlement(int yearOfWork, int yearOfBudget, int budgetSource, string quarterMonth, DateTime voucherDate, bool hasDuToan, string userName, string agencyIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_tonghop_donvi_lns @YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @VoucherDate, @HasDuToan, @UserName, @AgencyIds";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@YearOfBudget", yearOfBudget),
                    new SqlParameter("@BudgetSource", budgetSource),
                    new SqlParameter("@QuarterMonth", quarterMonth),
                    new SqlParameter("@VoucherDate", voucherDate),
                    new SqlParameter("@HasDuToan", hasDuToan),
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@AgencyIds", agencyIds)
                };
                return ctx.FromSqlRaw<NsMucLucNganSach>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindByQtTongHopQuy(int yearOfWork, string yearOfBudget, int budgetSource, string agencyId, string quarterMonth, string loaiQuyetToan, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_tonghop_quy_lns @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @QuarterMonth, @LoaiQuyetToan, @UserName";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@YearOfBudget", yearOfBudget),
                    new SqlParameter("@BudgetSource", budgetSource),
                    new SqlParameter("@AgencyId", agencyId),
                    new SqlParameter("@QuarterMonth", quarterMonth),
                    new SqlParameter("@LoaiQuyetToan", loaiQuyetToan),
                    new SqlParameter("@UserName", userName)
                };
                return ctx.FromSqlRaw<NsMucLucNganSach>(sql, parameters).ToList();
            }
        }

        public IEnumerable<LNSQuery> FindBySummaryYearSettlement(string yearOfBudget, int yearOfWork, int budgetSource, int dataType, string type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", budgetSource);
                SqlParameter dataTypeParam = new SqlParameter("@DataType", dataType);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearBudget", yearOfBudget);
                SqlParameter typeParam = new SqlParameter("@Loai", type);
                return ctx.FromSqlRaw<LNSQuery>("EXECUTE sp_qt_baocao_tonghop_nam_lns @YearOfWork, @BudgetSource, @DataType, @YearBudget, @Loai",
                    yearOfWorkParam, budgetSourceParam, dataTypeParam, yearOfBudgetParam, typeParam).ToList();
            }
        }

        public DataTable FindLNSByYear(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"SELECT sLNS, CONCAT(sLNS, ' - ', sMoTa) AS TenLNS FROM NS_MucLucNganSach WHERE iNamLamViec=@iNamLamViec AND iTrangThai=1 AND sL='' ORDER BY sLNS";
                var parameters = new[]
                {
                    new SqlParameter("@iNamLamViec", yearOfWork)
                };
                return ctx.FromSqlCommand(sql, CommandType.Text, parameters);
            }
        }
        public IEnumerable<MucLucNganSachCheckDataQuery> FindMlnsEstimateSettlementByYearOfBudget(int yearOfBuget)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                IEnumerable<MucLucNganSachCheckDataQuery> result;
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfBuget);
                result = ctx.FromSqlRaw<MucLucNganSachCheckDataQuery>("EXECUTE dbo.sp_check_exits_Lns_by_yearOfBudget  @YearOfWork", yearOfWorkParam).ToList();
                return result;
            }
        }
    }
}
