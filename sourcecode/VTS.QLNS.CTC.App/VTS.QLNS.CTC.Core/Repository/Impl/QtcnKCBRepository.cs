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
using static VTS.QLNS.CTC.Utility.Enum.BaoHiemDuToanTypeEnum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QtcnKCBRepository : Repository<BhQtcnKCB>, IQtcnKCBRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QtcnKCBRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQtcnKCB> FindByCondition(Expression<Func<BhQtcnKCB, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcnKCBs.Where(predicate).ToList();
            }
        }

        public IEnumerable<BhQtcnKCBQuery> GetDanhSachQuyetToanNamKCB(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                return ctx.FromSqlRaw<BhQtcnKCBQuery>("EXECUTE sp_bh_quyet_toan_danhsachquyettoannamKCB_index @INamLamViec", iNamLamViecParam).ToList();
            }
        }

        public IEnumerable<BhQtcnKCB> FindByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcnKCBs.Where(x => x.INamLamViec == namLamViec).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhQtcnKCBs.Where(x=> x.INamLamViec == iNamLamViec).ToList();
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
                BhQtcnKCB entity = ctx.BhQtcnKCBs.Find(id);
                entity.BIsKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcnKCBs.Any(t => t.INamLamViec == namLamViec && t.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop);
            }
        }

        public void UpdateTotalCPChungTu(string voucherId, string userModify)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtcnKCB_update_total @VoucherId, @UserModify";
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
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_qtc_rpt_get_donvi_nKCB_lns @NamLamViec, @LoaiChungTu", iNamLamViecParam, searchLoaiCtParam).ToList();
            }
        }
    }
}
