using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhKhcKinhphiQuanlyRepostiory : Repository<BhKhcKinhphiQuanly>, IBhKhcKinhphiQuanlyRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhKhcKinhphiQuanlyRepostiory(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public BhKhcKinhphiQuanly FindAggregateVoucher(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhcKinhphiQuanlys.FirstOrDefault(x => x.INamLamViec == yearOfWork && x.ILoaiTongHop == 2 && x.BIsKhoa == true);
            }
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);

                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_khc_rpt_get_donvi_QLKP @NamLamViec", iNamLamViecParam).ToList();
            }
        }

        public IEnumerable<BhKhcKinhphiQuanlyQuery> FindIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "bh_kehoach_chikinh_phiquan_ly_index";
                return ctx.FromSqlRaw<BhKhcKinhphiQuanlyQuery>(executeSql, new { }).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int NamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhKhcKinhphiQuanlys.Where(x => x.INamLamViec == NamLamViec).OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
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
