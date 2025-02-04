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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QtcqBHXHRepository : Repository<BhQtcqBHXH>, IQtcqBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QtcqBHXHRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQtcqBHXH> FindByCondition(Expression<Func<BhQtcqBHXH, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqBHXHs.Where(predicate).ToList();
            }
        }

        public IEnumerable<DonVi> FindByDonViForNamLamViec(int namLamViec, int iQuy, int iLoaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter searchQuyParam = new SqlParameter("@Quy", iQuy);
                SqlParameter searchLoaiCtParam = new SqlParameter("@LoaiChungTu", iLoaiChungTu);
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_cp_rpt_get_donvi_qBHXH_lns @NamLamViec, @Quy, @LoaiChungTu", iNamLamViecParam, searchQuyParam, searchLoaiCtParam).ToList();
            }
        }

        public IEnumerable<BhQtcqBHXH> FindByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqBHXHs.Where(x => x.INamChungTu == namLamViec).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhQtcqBHXHs.Where(x=> x.INamChungTu == iNamLamViec).ToList();
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
                BhQtcqBHXH entity = ctx.BhQtcqBHXHs.Find(id);
                entity.BIsKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqBHXHs.Any(t => t.INamChungTu == namLamViec && t.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop);
            }
        }


        public IEnumerable<BhQtcqBHXHQuery> GetDanhSachQuyetToanQuyBHXH(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);


                return ctx.FromSqlRaw<BhQtcqBHXHQuery>("EXECUTE sp_bh_quyet_toan_danhsachquyettoanquy_index @INamLamViec", iNamLamViecParam).ToList();
            }
        }


        public void UpdateTotalCPChungTu(string voucherId, string userModify)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtcqBHXH_update_total @VoucherId, @UserModify";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId),
                    new SqlParameter("@UserModify", userModify)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public int DeleteDupItem(Guid voucherID)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_bhxh_qtcqn_delete_duplicate_item @VoucherID";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherID", voucherID)
                };
                return ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
