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
    public class BhQtcQuyKinhPhiQuanLyRepostiory : Repository<BhQtcQuyKinhPhiQuanLy>, IBhQtcQuyKinhPhiQuanLyRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhQtcQuyKinhPhiQuanLyRepostiory(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQtcQuyKinhPhiQuanLyQuery> FindIndex(int iNamChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_qtc_quykinhphi_quanly_chungtu_index_bh @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", iNamChungTu)
                };
                return ctx.FromSqlRaw<BhQtcQuyKinhPhiQuanLyQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<DonVi> FindByDonViForNamLamViec(int namLamViec, int iQuy, int iLoaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter searchQuyParam = new SqlParameter("@Quy", iQuy);
                SqlParameter searchLoaiCtParam = new SqlParameter("@LoaiChungTu", iLoaiChungTu);
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_qtc_rpt_get_donvi_qKPQL_lns @NamLamViec, @Quy, @LoaiChungTu", iNamLamViecParam, searchQuyParam, searchLoaiCtParam).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhQtcQuyKinhPhiQuanLys.Where(x => x.INamChungTu == namLamViec).OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
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
