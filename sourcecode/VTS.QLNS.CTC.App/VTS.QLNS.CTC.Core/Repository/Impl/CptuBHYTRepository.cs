using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.BaoHiemDuToanTypeEnum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class CptuBHYTRepository : Repository<BhCptuBHYT>, ICptuBHYTRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public CptuBHYTRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhCptuBHYT> FindByCondition(Expression<Func<BhCptuBHYT, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCptuBHYTs.Where(predicate).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhCptuBHYTs.Where(x => x.INamLamViec == iNamLamViec).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    var sSoChungTuMax = result.OrderByDescending(x => x.SSoChungTu).FirstOrDefault().SSoChungTu;
                    var indexString = sSoChungTuMax.Substring(4, sSoChungTuMax.Length - 4);
                    var index = int.Parse(indexString) + 1;
                    return index;
                }
                catch (Exception)
                {
                    return result.Count + 1;
                }
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                BhCptuBHYT entity = ctx.BhCptuBHYTs.Find(id);
                entity.BIsKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public void UpdateTotalCPChungTu(string voucherId, string userModify)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_cptubhyt_update_total @VoucherId, @UserModify";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId),
                    new SqlParameter("@UserModify", userModify)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void UpdateAggregateStatus(string voucherIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "UPDATE BH_CP_CapTamUng_KCB_BHYT SET bIsTongHop = 0 WHERE iID_BH_CP_CapTamUng_KCB_BHYT IN (SELECT * FROM f_split(@VoucherIds))";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherIds", voucherIds)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<BhCptuBHYT> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCptuBHYTs.Where(x => x.INamLamViec == namLamViec && x.SSoChungTu == sct).ToList();
            }
        }

        public IEnumerable<BhCptuBHYT> FindByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCptuBHYTs.Where(x => x.INamLamViec == namLamViec).ToList();
            }
        }

        public IEnumerable<BhCptuBHYT> FindAggregateVoucher(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCptuBHYTs.Where(x => x.INamLamViec == namLamViec && !string.IsNullOrEmpty(x.SDSSoChungTuTongHop)).ToList();
            }
        }

        public void AddAggregate(BhCpTUChungTuChiTietCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", creation.ListIdChungTuTongHop);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", creation.IdChungTu);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", creation.NamLamViec);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_bh_cap_phat_tam_ung_tao_ct_tonghop @ListIdChungTuTongHop, @IdChungTu, @NamLamViec",
                    listIdChungTuTongHop, idChungTu, namLamViec);
            }
        }

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCptuBHYTs.Any(t => t.INamLamViec == namLamViec && !string.IsNullOrEmpty(t.SDSSoChungTuTongHop));
            }
        }
    }
}
