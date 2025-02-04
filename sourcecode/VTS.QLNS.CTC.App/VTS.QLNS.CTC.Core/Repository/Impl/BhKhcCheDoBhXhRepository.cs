using Microsoft.EntityFrameworkCore;
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
    public class BhKhcCheDoBhXhRepository : Repository<BhKhcCheDoBhXh>, IBhKhcCheDoBhXhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhKhcCheDoBhXhRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public BhKhcCheDoBhXh FindAggregateVoucher(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhcCheDoBhXhs.FirstOrDefault(x => x.INamLamViec == yearOfWork && x.ILoaiTongHop == 2 && x.BIsKhoa == true);
            }
        }

        public List<DonVi> FindByDonViForNamLamViec(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);

                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_khc_rpt_get_donvi_BHXH @NamLamViec", iNamLamViecParam).ToList();
            }
        }

        public IEnumerable<BhKhcCheDoBhXhQuery> FindIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE bh_xh_kehoach_chi_index";
                return ctx.FromSqlRaw<BhKhcCheDoBhXhQuery>(executeSql, new { }).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhKhcCheDoBhXhs.Where(x => x.INamLamViec == iNamLamViec).OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
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

        public bool IsExistChungTuTongHop(int loai, int namLamViec)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                var rootDonVi = ctx.NsDonVis.FirstOrDefault(t => t.NamLamViec == namLamViec && LoaiDonVi.ROOT.Equals(t.Loai))?.IIDMaDonVi;
                return ctx.BhKhcCheDoBhXhs.Any(t => t.IIdDonViId.Equals(rootDonVi) && t.ILoaiTongHop == loai && t.INamLamViec == namLamViec);
            }
        }
    }
}
