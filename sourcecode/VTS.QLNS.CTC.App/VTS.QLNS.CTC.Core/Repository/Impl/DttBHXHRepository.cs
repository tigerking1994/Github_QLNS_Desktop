using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DttBHXHRepository : Repository<BhDttBHXH>, IDttBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DttBHXHRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhDttBHXHQuery> FindByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<BhDttBHXHQuery>("EXECUTE dbo.sp_du_toan_thu_bhxh_index @YearOfWork", yearOfWorkParam).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhDttBHXHs.Where(x => x.INamLamViec == yearOfWork).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    var indexString = result.OrderByDescending(x => x.SSoChungTu).FirstOrDefault().SSoChungTu.Substring(4, 3);
                    var index = int.Parse(indexString) + 1;
                    return index;
                }
                catch (Exception)
                {
                    return result.Count + 1;
                }
            }
        }

        public void LockOrUnLock(string id, bool isLock)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var chungTu = ctx.BhDttBHXHs.First(x => x.Id.ToString() == id);
                chungTu.BIsKhoa = isLock;
                ctx.SaveChanges();
            }
        }
    }
}
