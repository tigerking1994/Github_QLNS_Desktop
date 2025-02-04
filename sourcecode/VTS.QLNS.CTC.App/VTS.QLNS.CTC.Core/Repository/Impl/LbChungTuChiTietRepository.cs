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
    public class LbChungTuChiTietRepository : Repository<NsNganhChungTuChiTiet>, ILbChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public LbChungTuChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool CheckExitsByChungTuId(Guid chungtuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int count = ctx.NsNganhChungTuChiTiets.Where(n => n.IIdCtnganh == chungtuId).ToList() != null ? ctx.NsNganhChungTuChiTiets.Where(n => n.IIdCtnganh == chungtuId).ToList().Count : 0;
                return count > 0;
            }
        }

        public void DeleteByChungTuId(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"DELETE FROM NS_Nganh_ChungTuChiTiet WHERE iID_CTNganh = @Id";
                var parameters = new[]
                {
                    new SqlParameter("@Id", id.ToString())
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public List<NsNganhChungTuChiTiet> FindByChungTuId(Guid chungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsNganhChungTuChiTiets.Where(n => n.IIdCtnganh == chungTuId).ToList();
            }
        }

        public IEnumerable<LbChungTuChiTietQuery> FindChungTuChiTietByCondition(AllocationDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    string sql = "EXECUTE dbo.sp_lb_chungtu_chitiet @VoucherId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @VoucherDate, @UserName, @LoaiChungTu";
                    var parameters = new[]
                    {
                        new SqlParameter("@VoucherId", searchCondition.VoucherId),
                        new SqlParameter("@LNS", searchCondition.LNS),
                        new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                        new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                        new SqlParameter("@BudgetSource", searchCondition.BudgetSource),
                        new SqlParameter("@AgencyId", searchCondition.AgencyId),
                        new SqlParameter("@VoucherDate", searchCondition.VoucherDate),
                        new SqlParameter("@UserName", searchCondition.UserName),
                        new SqlParameter("@LoaiChungTu", searchCondition.Type)
                    };
                    return ctx.FromSqlRaw<LbChungTuChiTietQuery>(sql, parameters).ToList();
                }
                catch (Exception ex)
                {
                    return new List<LbChungTuChiTietQuery>();
                }
            }
        }
    }
}

