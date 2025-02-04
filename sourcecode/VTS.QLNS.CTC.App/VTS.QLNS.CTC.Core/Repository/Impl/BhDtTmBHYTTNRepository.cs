using Microsoft.EntityFrameworkCore;
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
    public class BhDtTmBHYTTNRepository : Repository<BhDtTmBHYTTN>, IBhDtTmBHYTTNRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhDtTmBHYTTNRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhDtTmBHYTTNQuery> FindByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<BhDtTmBHYTTNQuery>("EXECUTE dbo.sp_du_toan_thu_bhyt_index @YearOfWork", yearOfWorkParam).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhDtTmBHYTTNs.Where(x => x.INamLamViec == yearOfWork).ToList();
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

        public IEnumerable<BhDtTmBHYTTNQuery> GetDanhSachDotNhanPhanBo(int yearOfWork, DateTime date, int iLoaiDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter dateParam = new SqlParameter("@Date", date);
                SqlParameter iLoaiDuToanParam = new SqlParameter("@LoaiDuToan", iLoaiDuToan);
                return ctx.FromSqlRaw<BhDtTmBHYTTNQuery>("EXECUTE dbo.sp_bh_dutoanthumua_dotnhan_phanbo_find_all @YearOfWork,@Date,@LoaiDuToan", yearOfWorkParam, dateParam, iLoaiDuToanParam).ToList();
            }
        }
    }
}
