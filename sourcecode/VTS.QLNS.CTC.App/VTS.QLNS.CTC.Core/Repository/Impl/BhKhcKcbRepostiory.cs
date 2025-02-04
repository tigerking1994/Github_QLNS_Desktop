using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhKhcKcbRepostiory : Repository<BhKhcKcb>, IBhKhcKcbRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhKhcKcbRepostiory(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public BhKhcKcb FindAggregateVoucher(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhcKcbs.FirstOrDefault(x => x.INamLamViec == yearOfWork && x.ILoaiTongHop == 2 && x.BIsKhoa == true);
            }
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);

                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_khc_rpt_get_donvi_KCBQY @NamLamViec", iNamLamViecParam).ToList();
            }
        }

        public IEnumerable<BhKhcKcbQuery> FindIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "bh_kehoach_chikham_chubenh_index";
                return ctx.FromSqlRaw<BhKhcKcbQuery>(executeSql, new { }).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhKhcKcbs.Where(x => x.INamLamViec == yearOfWork).OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    var indexString = result.FirstOrDefault().Substring(4, 3);
                    var index = int.Parse(indexString) + 1;
                    return index;
                }
                catch (Exception)
                {
                    return result.Count + 1;
                }
            }
        }
    }
}
