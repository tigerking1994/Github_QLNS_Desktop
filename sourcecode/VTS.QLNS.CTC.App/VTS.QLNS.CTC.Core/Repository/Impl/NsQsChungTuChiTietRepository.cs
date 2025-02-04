using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsQsChungTuChiTietRepository : Repository<NsQsChungTuChiTiet>, INsQsChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsQsChungTuChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void CreateDetail(Guid voucherId, int yearOfWork, int month, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qs_tao_chitiet @IdChungTu, @YearOfWork, @Thang, @User";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu", voucherId),
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@User", userName),
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherId", voucherId.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_QS_ChungTuChiTiet WHERE iID_QSChungTu = @VoucherId", voucherIdParam);
            }
        }

        public void DeleteInputData(Guid armyVoucherId, int month, string agencyId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var armyVoucherIdParam = new SqlParameter("@ArmyVoucherId", armyVoucherId);
                var monthParam = new SqlParameter("@Month", month);
                var agencyIdParam = new SqlParameter("@AgencyId", agencyId);
                ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_QS_ChungTuChiTiet WHERE iID_QSChungTu = @ArmyVoucherId AND iThangQuy = @Month AND iID_MaDonVi = @AgencyId AND sKyHieu <> '100' AND sKyHieu <> '700';", armyVoucherIdParam, monthParam, agencyIdParam);
            }
        }

        public IEnumerable<QsChungTuChiTietQuery> FindByCondition(ArmyVoucherDetailCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", condition.YearOfWork);
                SqlParameter voucherIdParam = new SqlParameter("@VoucherId", condition.VoucherId);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", condition.AgencyId);
                string sql = "";
                if (condition.Method == ArmyVoucherDetailMethod.GET_PART)
                    sql = "EXECUTE dbo.sp_qs_chungtu_chitiet @YearOfWork, @VoucherId, @AgencyId";
                else
                    sql = "EXECUTE dbo.sp_qs_chungtu_chitiet_tonghop @YearOfWork, @VoucherId, @AgencyId";
                return ctx.FromSqlRaw<QsChungTuChiTietQuery>(sql, yearOfWorkParam, voucherIdParam, agencyIdParam).ToList();
            }
        }

        public IEnumerable<ReportQuanSoDonViQuery> FindForAgencyDetailReport(int yearOfWork, string agencyId, string period)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", agencyId);
                SqlParameter periodParam = new SqlParameter("@Period", period);
                return ctx.FromSqlRaw<ReportQuanSoDonViQuery>("EXECUTE dbo.sp_qs_rpt_chitiet_donvi @YearOfWork, @AgencyId, @Period", yearOfWorkParam, agencyIdParam, periodParam).ToList();
            }
        }

        public IEnumerable<ReportQuanSoDonViQuery> FindForAgencyReport(int yearOfWork, string agencyId, string period)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", agencyId);
                agencyIdParam.Value = string.IsNullOrEmpty(agencyId) ? DBNull.Value : (object)agencyId;
                SqlParameter periodParam = new SqlParameter("@Period", period);
                return ctx.FromSqlRaw<ReportQuanSoDonViQuery>("EXECUTE dbo.sp_qs_rpt_donvi @YearOfWork, @AgencyId, @Period", yearOfWorkParam, agencyIdParam, periodParam).ToList();
            }
        }
        public List<string> FindForAgencyHasvalueReport(int yearOfWork, string agencyId, string period)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var rs = ctx.NsQsChungTuChiTiets.Where(x => !x.BHangCha && x.INamLamViec == yearOfWork && (string.IsNullOrEmpty(agencyId) || agencyId.Contains(x.IIdMaDonVi)) && period.Contains(x.IThangQuy.ToString()));                
                return rs.Select(x=>x.IIdMaDonVi).Distinct().ToList();
            }
        }        

        public IEnumerable<ReportQuanSoTongHopQuery> FindForAverage(int yearOfWork, string agencyId, string period, ReportArmy reportType, int soThang)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", agencyId);
                SqlParameter periodParam = new SqlParameter("@Period", period);
                SqlParameter totalMonth = new SqlParameter("@TotalMonth", soThang);
                string sql = string.Empty;
                if (reportType == ReportArmy.AgencyDetail)
                    sql = "EXECUTE dbo.sp_qs_rpt_binhquan_thang @YearOfWork, @AgencyId, @Period";
                else sql = "EXECUTE dbo.sp_qs_rpt_binhquan_donvi @YearOfWork, @AgencyId, @Period, @TotalMonth";
                return ctx.FromSqlRaw<ReportQuanSoTongHopQuery>(sql, yearOfWorkParam, agencyIdParam, periodParam, totalMonth).ToList();
            }
        }

        public IEnumerable<ReportQuanSoLienThamQuery> FindForJurisprudence(int month, int yearOfWork, string agencyId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter monthParam = new SqlParameter("@Month", month);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter yearOfWorkBeforeParam = new SqlParameter("@YearOfWorkBefore", yearOfWork - 1);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", agencyId);
                return ctx.FromSqlRaw<ReportQuanSoLienThamQuery>("EXECUTE dbo.sp_qs_rpt_lientham @Month, @YearOfWork, @YearOfWorkBefore, @AgencyId", monthParam, yearOfWorkParam, yearOfWorkBeforeParam, agencyIdParam).ToList();
            }
        }

        public IEnumerable<ReportQuanSoRaQuanQuery> FindForLeave(int yearOfWork, string agencyId, string period)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", agencyId);
                SqlParameter periodParam = new SqlParameter("@Period", period);
                return ctx.FromSqlRaw<ReportQuanSoRaQuanQuery>("EXECUTE dbo.sp_qs_rpt_raquan @YearOfWork, @AgencyId, @Period", yearOfWorkParam, agencyIdParam, periodParam).ToList();
            }
        }

        public IEnumerable<ReportQuanSoRaQuanQuery> FindForLeaveBefore(int month, int yearOfWork, string agencyId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter monthParam = new SqlParameter("@Month", month);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter yearOfWorkBeforeParam = new SqlParameter("@YearOfWorkBefore", yearOfWork - 1);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", agencyId);
                return ctx.FromSqlRaw<ReportQuanSoRaQuanQuery>("EXECUTE dbo.sp_qs_rpt_raquan_thangtruoc @Month, @YearOfWork, @YearOfWorkBefore, @AgencyId", monthParam, yearOfWorkParam, yearOfWorkBeforeParam, agencyIdParam).ToList();
            }
        }

        public IEnumerable<ReportQuanSoThuongXuyenQuery> FindForRegular(int month1, int month2, int month3, int month4, int yearOfWork, string agencyId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter month1Param = new SqlParameter("@month1", month1);
                SqlParameter month2Param = new SqlParameter("@month2", month2);
                SqlParameter month3Param = new SqlParameter("@month3", month3);
                SqlParameter month4Param = new SqlParameter("@month4", month4);
                SqlParameter yearOfWorkParam = new SqlParameter("@yearOfWork", yearOfWork);
                SqlParameter agencyIdParam = new SqlParameter("@agencyId", agencyId);
                return ctx.FromSqlRaw<ReportQuanSoThuongXuyenQuery>("EXECUTE dbo.sp_qs_rpt_thuongxuyen @month1, @month2, @month3, @month4, @yearOfWork, @agencyId", month1Param, month2Param, month3Param, month4Param, yearOfWorkParam, agencyIdParam).ToList();
            }
        }

        public IEnumerable<ReportQuanSoTongHopQuery> FindForSummaryReport(int yearOfWork, string agencyId, string period)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", agencyId);
                SqlParameter periodParam = new SqlParameter("@Period", period);
                return ctx.FromSqlRaw<ReportQuanSoTongHopQuery>("EXECUTE dbo.sp_qs_rpt_tonghop @YearOfWork, @AgencyId, @Period", yearOfWorkParam, agencyIdParam, periodParam).ToList();
            }
        }

        public void UpdateDetail(int yearOfWork, int month, string idMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qs_capnhat_chitiet @YearOfWork, @Thang, @IdMaDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@Thang", month),
                    new SqlParameter("@IdMaDonVi", idMaDonVi),
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
        public IEnumerable<NsQsChungTuChiTiet> UpdateDetailYearBegin(int yearOfWork, string idMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qs_capnhat_chitiet_year_begin @YearOfWork, @IdMaDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@IdMaDonVi", idMaDonVi),
                };
                return ctx.FromSqlRaw<NsQsChungTuChiTiet>(sql, parameters).ToList();

            }
        }
    }
}
