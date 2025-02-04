using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TnQtChungTuChiTietHD4554Repository : Repository<TnQtChungTuChiTietHD4554>, ITnQtChungTuChiTietHD4554Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public TnQtChungTuChiTietHD4554Repository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_chungtu_chitiet_HD4554_tao_tonghop @VoucherIds, @VoucherId, @YearOfBudget, @BudgetSource, @YearOfWork, @Type, @QuarterMonthType, @QuarterMonth, @AgencyId, @UserName";
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

        public List<TnQtChungTuChiTietHD4554Query> FindAllNSDCChungTuByCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.SetCommandTimeout(300);
                string sql = string.Format("EXECUTE dbo.sp_rpt_qt_chungtu_chitiet_hd4554_all @chungTuIds, @namLamViec, @namNganSach, @nguonNganSach, @lns, @donVi, @dvt ,@thangQuyLoai,@thangQuy,@userName");
                var parameters = new[]
                {
                    new SqlParameter("@chungTuIds", searchCondition.VoucherIds.ToString()),
                    new SqlParameter("@namLamViec", searchCondition.YearOfWork),
                    new SqlParameter("@namNganSach", searchCondition.YearOfBudget),
                    new SqlParameter("@nguonNganSach", searchCondition.BudgetSource),
                    new SqlParameter("@lns", searchCondition.LNS),
                    new SqlParameter("@donVi", searchCondition.IdDonVi),
                    new SqlParameter("@dvt", searchCondition.DonViTinh),
                    new SqlParameter("@thangQuyLoai", searchCondition.IThangQuyLoai),
                    new SqlParameter("@thangQuy", searchCondition.LstThangQuy),
                    new SqlParameter("@userName", searchCondition.UserName)
                };
                return ctx.FromSqlRaw<TnQtChungTuChiTietHD4554Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TnQtChungTuChiTietHD4554> FindByIdChiTiet(Guid idChungTu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnQtChungTuChiTietHD4554s.Where(x => x.IIdTnQtChungTu.Equals(idChungTu)).ToList();
            }
        }

        public IEnumerable<TnQtChungTuChiTietHD4554Query> FindByRealRevenueExpenditureCondition(EstimationVoucherDetailCriteria searchCondition)
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

                return ctx.FromSqlRaw<TnQtChungTuChiTietHD4554Query>("EXECUTE sp_tn_qt_chungtu_chitietHD4554 @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource",
                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam).ToList();
            }
        }

        public IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureReportCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureResultCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TnQtChungTuChiTietHD4554Query> GetDataChungTuDetail(EstimationVoucherDetailCriteria searchCondition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnQtChungTuChiTietHD4554s.Where(x => x.IIdTnQtChungTu.HasValue && chungTuIds.Contains(x.IIdTnQtChungTu.Value) && (x.FSoTien != 0 || !string.IsNullOrEmpty(x.SGhiChu))).Select(x => x.SLNS).Distinct().ToList();
            }
        }

        public void UpdateMonth(Guid voucherId, int month, int monthType, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_chungtu_chitietHD4554_update_month @VoucherId, @Thang, @LoaiThang, @UserName";
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
    }
}
