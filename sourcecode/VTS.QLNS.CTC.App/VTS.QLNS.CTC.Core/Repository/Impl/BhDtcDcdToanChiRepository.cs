using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhDtcDcdToanChiRepository : Repository<BhDtcDcdToanChi>, IBhDtcDcdToanChiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhDtcDcdToanChiRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, Guid iDLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter iDLoaiChiViecParam = new SqlParameter("@IDLoaiChi", iDLoaiChi);
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_dtc_rpt_get_donvi @NamLamViec, @IDLoaiChi", iNamLamViecParam, iDLoaiChiViecParam).ToList();
            }
        }

        public IEnumerable<BhDtcDcdToanChiQuery> FindIndex(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                string executeSql = "sp_dt_dieuchinh_dutoan_index @NamLamViec";
                return ctx.FromSqlRaw<BhDtcDcdToanChiQuery>(executeSql, iNamLamViecParam).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhDtcDcdToanChis.Where(x => x.INamLamViec == namLamViec).OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    var indexString = result.FirstOrDefault().Substring(3, 3);
                    var index = int.Parse(indexString) + 1;
                    return index;
                }
                catch (Exception)
                {
                    return result.Count + 1;
                }
            }
        }

        public IEnumerable<BhDtcDcdToanChi> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtcDcdToanChis.Where(x => voucherNos.Contains(x.SSoChungTu) && x.INamLamViec == yearOfWork).ToList();
            }
        }

        public List<BhDtcDcdToanChi> FindAggregateAdjustVoucher(int yearOfWork, Guid? iIDLoaiDanhMucChi, string sMaLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstMaLoaiChi = sMaLoaiChi.Split(',');
                return ctx.BhDtcDcdToanChis.Where(x => x.INamLamViec == yearOfWork && x.ILoaiTongHop == 2 && x.BIsKhoa == true && lstMaLoaiChi.Contains(x.SMaLoaiChi)).ToList();
            }
        }
    }
}
