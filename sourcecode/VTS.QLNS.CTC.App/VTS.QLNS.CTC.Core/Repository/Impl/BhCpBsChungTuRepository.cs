using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.Data.SqlClient;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using Microsoft.EntityFrameworkCore;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhCpBsChungTuRepository : Repository<BhCpBsChungTu>, IBhCpBsChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhCpBsChungTuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregate(BhCpBsChungTuChiTietCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", creation.ListIdChungTuTongHop);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", creation.IdChungTu);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", creation.NamLamViec);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_bh_cap_phat_bo_sung_tao_ct_tonghop @ListIdChungTuTongHop, @IdChungTu, @NamLamViec",
                    listIdChungTuTongHop, idChungTu, namLamViec);
            }
        }

        public IEnumerable<BhCpBsChungTu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCpBsChungTus.Where(x => voucherNoes.Contains(x.SSoChungTu) && x.INamLamViec == yearOfWork).ToList();
            }
        }

        public IEnumerable<BhCpBsChungTu> FindByCondition(int namLamViec, int loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCpBsChungTus.Where(x => x.INamLamViec == namLamViec && x.ILoaiTongHop == loaiChungTu).ToList();
            }
        }

        public IEnumerable<BhCpBsChungTu> FindByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCpBsChungTus.Where(x => x.INamLamViec == namLamViec).ToList();
            }
        }

        public IEnumerable<BhCpBsChungTu> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCpBsChungTus.Where(x => x.INamLamViec == namLamViec && x.SSoChungTu == sct).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhCpBsChungTus.Where(x => x.INamLamViec == yearOfWork).OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
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

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCpBsChungTus.Any(t => t.INamLamViec == namLamViec && t.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop);
            }
        }
    }
}
