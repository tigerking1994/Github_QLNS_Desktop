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
    public class BhDcDuToanThuRepository : Repository<BhDcDuToanThu>, IBhDcDuToanThuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhDcDuToanThuRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhDcDuToanThu> FindByCondition(int namLamViec, string maDonVi, int loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDcDuToanThus.Where(x => x.INamLamViec == namLamViec && x.IIDMaDonVi == maDonVi && x.ILoaiTongHop == loaiChungTu).ToList();
            }
        }

        public IEnumerable<BhDcDuToanThu> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDcDuToanThus.Where(x => x.INamLamViec == namLamViec && x.SSoChungTu == sct).ToList();
            }
        }

        public List<string> FindCurrentUnits(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDcDuToanThus.Where(x => x.INamLamViec == namLamViec).Select(x => x.IIDMaDonVi).Distinct().ToList();
            }
        }

        public IEnumerable<BhDcDuToanThuQuery> FindByYearOfWord(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<BhDcDuToanThuQuery>("EXECUTE dbo.sp_bh_dieu_chinh_du_toan_thu_index @YearOfWork", yearOfWorkParam).ToList();
            }
        }

        public IEnumerable<BhDcDuToanThuQuery> FindIndex(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<BhDcDuToanThuQuery>("EXECUTE dbo.sp_bh_dieu_chinh_du_toan_thu_index @YearOfWork", yearOfWorkParam).ToList();
            }
        }

        public IEnumerable<BhDcDuToanThu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDcDuToanThus.Where(x => voucherNoes.Contains(x.SSoChungTu) && x.INamLamViec == yearOfWork).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhDcDuToanThus.OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
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
    }
}
