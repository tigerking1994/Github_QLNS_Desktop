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
    public class BhQtcQuyKPKRepostiory : Repository<BhQtcQuyKPK>, IBhQtcQuyKPKRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhQtcQuyKPKRepostiory(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<DonVi> FindByDonViForNamLamViec(int namLamViec, int iQuy, int iLoaiChungTu, Guid loaiChiQT)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter searchQuyParam = new SqlParameter("@Quy", iQuy);
                SqlParameter searchLoaiCtParam = new SqlParameter("@LoaiChungTu", iLoaiChungTu);
                SqlParameter searchloaiChiQTParam = new SqlParameter("@LoaiChi", loaiChiQT);
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_qtc_rpt_get_donvi_qKPK_lns @NamLamViec, @Quy, @LoaiChungTu, @LoaiChi", iNamLamViecParam, searchQuyParam, searchLoaiCtParam, searchloaiChiQTParam).ToList();
            }
        }

        public IEnumerable<BhQtcQuyKPKQuery> FindIndex(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_qtc_quykinhphi_khac_chungtu_index_bh @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork)
                };
                return ctx.FromSqlRaw<BhQtcQuyKPKQuery>(executeSql, parameters).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhQtcQuyKPKs.Where(x => x.INamChungTu == namLamViec).OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
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

        public IEnumerable<BhQtcQuyKPK> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcQuyKPKs.Where(x => voucherNos.Contains(x.SSoChungTu) && x.INamChungTu == yearOfWork).ToList();
            }
        }
    }
}
