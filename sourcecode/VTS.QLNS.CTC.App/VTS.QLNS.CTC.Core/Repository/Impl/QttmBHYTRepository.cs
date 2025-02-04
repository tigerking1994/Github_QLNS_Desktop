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

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QttmBHYTRepository : Repository<BhQttmBHYT>, IQttmBHYTRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QttmBHYTRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQttmBHYT> FindAggregateVoucher(string sct, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttmBHYTs.Where(x => x.INamLamViec == namLamViec && x.SSoChungTu == sct).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTQuery> FindByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<BhQttmBHYTQuery>("EXECUTE dbo.sp_bh_quyet_toan_thu_mua_bhyt_index @YearOfWork", yearOfWorkParam).ToList();
            }
        }

        public IEnumerable<BhQttmBHYT> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttmBHYTs.Where(x => x.INamLamViec == namLamViec && x.SSoChungTu == sct).ToList();
            }
        }

        public IEnumerable<BhQttQuarterQuery> GetQuarterYearByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                return ctx.FromSqlRaw<BhQttQuarterQuery>("EXECUTE dbo.sp_bh_qttm_get_quy_nam @NamLamViec", namLamViecParam).ToList();
            }
        }

        public int GetVoucherIndex(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhQttmBHYTs.Where(x => x.INamLamViec == year).ToList();
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

        public List<int> GetVoucherYears(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttmBHYTs.Where(x => x.IQuyNamLoai == 2 && x.IQuyNam >= year - 10 && x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTu)
                    .Select(x => x.IQuyNam).Distinct().ToList();
            }
        }

        public bool IsExistAggregateVoucher(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var rootDonVi = ctx.NsDonVis.FirstOrDefault(t => t.NamLamViec == namLamViec && LoaiDonVi.ROOT.Equals(t.Loai))?.IIDMaDonVi;
                return ctx.BhQttmBHYTs.Any(t => t.IIDMaDonVi.Equals(rootDonVi) && t.INamLamViec == namLamViec && t.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop);
            }
        }

        public IEnumerable<BhQttmBHYT> FindByCondition(int namLamViec, int quyNam, int loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttmBHYTs.Where(x => x.INamLamViec == namLamViec && x.IQuyNam == quyNam && x.ILoaiTongHop == loaiChungTu).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTQuery> FindChungTuDonVi(int namLamViec, int loaiTongHop, bool bDaTongHop, int quyNam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter loaiTongHopParam = new SqlParameter("@LoaiTongHop", loaiTongHop);
                SqlParameter daTongHopParam = new SqlParameter("@DaTongHop", bDaTongHop);
                SqlParameter quyNamParam = new SqlParameter("@QuyNam", quyNam);
                return ctx.FromSqlRaw<BhQttmBHYTQuery>("EXECUTE dbo.sp_bh_qttm_get_chung_tu_don_vi @YearOfWork, @LoaiTongHop, @DaTongHop, @QuyNam",
                    yearOfWorkParam, loaiTongHopParam, daTongHopParam, quyNamParam).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTQuery> FindAllChungTuDonVi(int namLamViec, int quyNam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter quyNamParam = new SqlParameter("@QuyNam", quyNam);
                return ctx.FromSqlRaw<BhQttmBHYTQuery>("EXECUTE dbo.sp_bh_qttm_get_all_chung_tu_don_vi @YearOfWork, @QuyNam",
                    yearOfWorkParam, quyNamParam).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTQuery> FindChungTuDonViTongHop(int namLamViec, int loaiTongHop, string userName, int quyNam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter loaiTongHopParam = new SqlParameter("@LoaiTongHop", loaiTongHop);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter quyNamParam = new SqlParameter("@QuyNam", quyNam);
                return ctx.FromSqlRaw<BhQttmBHYTQuery>("EXECUTE dbo.sp_bh_qttm_get_chung_tu_don_vi_tong_hop @YearOfWork, @LoaiTongHop, @UserName, @QuyNam",
                    yearOfWorkParam, loaiTongHopParam, userNameParam, quyNamParam).ToList();
            }
        }

        public List<string> FindCurrentUnits(int namLamViec, int quynam, int loaiQuyNam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttmBHYTs.Where(x => x.INamLamViec == namLamViec && x.IQuyNam == quynam && x.IQuyNamLoai == loaiQuyNam)
                    .Select(x => x.IIDMaDonVi).Distinct().ToList();
            }
        }
    }
}
