using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QtcnBHXHRepository : Repository<BhQtcnBHXH>, IQtcnBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QtcnBHXHRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQtcnBHXH> FindByCondition(Expression<Func<BhQtcnBHXH, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcnBHXHs.Where(predicate).ToList();
            }
        }

        public IEnumerable<BhQtcnBHXHQuery> GetDanhSachQuyetToanNamBHXH(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);

                return ctx.FromSqlRaw<BhQtcnBHXHQuery>("EXECUTE sp_bh_quyet_toan_danhsachquyettoannam_index @INamLamViec",iNamLamViecParam).ToList();
            }
        }

        public IEnumerable<BhQtcnBHXH> FindByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcnBHXHs.Where(x => x.INamLamViec == namLamViec).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhQtcnBHXHs.Where(x=> x.INamLamViec == iNamLamViec).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    var sSoChungTuMax = result.OrderByDescending(x => x.SSoChungTu).FirstOrDefault().SSoChungTu;
                    var indexString = sSoChungTuMax.Substring(4, 3);
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
                BhQtcnBHXH entity = ctx.BhQtcnBHXHs.Find(id);
                entity.BIsKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcnBHXHs.Any(t => t.INamLamViec == namLamViec && t.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop);
            }
        }

        public void UpdateTotalCPChungTu(string voucherId, string userModify)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtcnBHXH_update_total @VoucherId, @UserModify";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId),
                    new SqlParameter("@UserModify", userModify)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter searchLoaiCtParam = new SqlParameter("@LoaiChungTu", chungTu);
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_qtc_rpt_get_donvi_nBHXH_lns @NamLamViec, @LoaiChungTu", iNamLamViecParam, searchLoaiCtParam).ToList();
            }
        }

        public IEnumerable<BhQtcnBHXH> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcnBHXHs.Where(x => voucherNos.Contains(x.SSoChungTu) && x.INamLamViec == yearOfWork).ToList();
            }
        }
    }
}
