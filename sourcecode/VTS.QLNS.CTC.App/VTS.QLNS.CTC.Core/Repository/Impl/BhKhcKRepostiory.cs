using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhKhcKRepostiory : Repository<BhKhcK>, IBhKhcKRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhKhcKRepostiory(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<BhKhcK> FindAggregateVoucher(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhcKs.Where(x => x.INamLamViec == yearOfWork && x.ILoaiTongHop == 2 && x.BIsKhoa == true).ToList();
            }
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, Guid IdLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter iIDLoaiChiParam = new SqlParameter("@IdLoaiChi", IdLoaiChi);

                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_khc_rpt_get_donvi_K @NamLamViec, @IdLoaiChi", iNamLamViecParam, iIDLoaiChiParam).ToList();
            }
        }

        public IEnumerable<BhKhcKQuery> FindIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "bh_kehoach_chikhac_index";
                return ctx.FromSqlRaw<BhKhcKQuery>(executeSql, new { }).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhKhcKs.Where(x=>x.INamLamViec== yearOfWork).OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
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
