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
    public class NsBkChungTuChiTietRepository : Repository<NsBkChungTuChiTiet>, INsBkChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsBkChungTuChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteByListVoucherId(List<Guid> listVoucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"DELETE FROM NS_BK_ChungTuChiTiet WHERE iID_BKChungTu IN (SELECT * FROM f_split(@VoucherIds))";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherIds", string.Join(",", listVoucherId))
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"DELETE FROM NS_BK_ChungTuChiTiet WHERE iID_BKChungTu = @VoucherId";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId.ToString())
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<ReportBangKeTongHopQuery> FindBySummaryVoucherList(ReportVoucherListCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bk_tonghop @YearOfWork, @QuarterMonth, @LNS, @AgencyId, @DataType, @Dvt, @Loai";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", condition.YearOfWork),
                    new SqlParameter("@QuarterMonth", condition.QuarterMonth),
                    new SqlParameter("@LNS", condition.LNS),
                    new SqlParameter("@AgencyId", condition.AgencyId),
                    new SqlParameter("@DataType", condition.DataType),
                    new SqlParameter("@Dvt", condition.Dvt),
                    new SqlParameter("@Loai", condition.LoaiChi)
                };
                return ctx.FromSqlRaw<ReportBangKeTongHopQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsBkChungTuChiTietQuery> FindByVoucherListId(Guid voucherListId, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bk_chungtu_danhsach @VoucherListId, @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherListId", voucherListId),
                    new SqlParameter("@YearOfWork", yearOfWork)
                };
                return ctx.FromSqlRaw<NsBkChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }
    }
}
