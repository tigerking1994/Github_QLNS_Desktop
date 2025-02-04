using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class KhtmBHYTRepository : Repository<BhKhtmBHYT>, IKhtmBHYTRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public KhtmBHYTRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregate(KhtmBHYTChiTietCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", creation.ListIdChungTuTongHop);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", creation.IdChungTu);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", creation.NamLamViec);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_khtm_bhyt_chungtu_chitiet_tao_tonghop @ListIdChungTuTongHop, @IdChungTu, @NamLamViec",
                    listIdChungTuTongHop, idChungTu, namLamViec);
            }
        }

        public IEnumerable<BhKhtmBHYTQuery> FindByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<BhKhtmBHYTQuery>("EXECUTE dbo.sp_ke_hoach_thu_mua_bhyt_index @YearOfWork", yearOfWorkParam).ToList();
            }
        }

        public IEnumerable<BhKhtmBHYT> FindByCondition(int namLamViec, string maDonVi, int loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtmBHYTs.Where(x => x.INamLamViec == namLamViec && x.IIDMaDonVi == maDonVi && x.ILoaiTongHop == loaiChungTu).ToList();
            }
        }

        public IEnumerable<BhKhtmBHYTQuery> FindChungTuChiTietTongHopByCondition(int namLamViec, int loaiTongHop, bool bDaTongHop, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter loaiTongHopParam = new SqlParameter("@LoaiTongHop", loaiTongHop);
                SqlParameter daTongHopParam = new SqlParameter("@DaTongHop", bDaTongHop);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                return ctx.FromSqlRaw<BhKhtmBHYTQuery>("EXECUTE dbo.sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop @YearOfWork, @LoaiTongHop, @DaTongHop, @UserName",
                    yearOfWorkParam, loaiTongHopParam, daTongHopParam, userNameParam).ToList();
            }
        }

        public IEnumerable<BhKhtmBHYTQuery> FindChungTuChiTietDonVi(int namLamViec, int loaiTongHop, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter loaiTongHopParam = new SqlParameter("@LoaiTongHop", loaiTongHop);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                return ctx.FromSqlRaw<BhKhtmBHYTQuery>("EXECUTE dbo.sp_khtm_bhyt_chung_tu_chi_tiet_donvi @YearOfWork, @LoaiTongHop, @UserName",
                    yearOfWorkParam, loaiTongHopParam, userNameParam).ToList();
            }
        }

        public IEnumerable<BhKhtmBHYT> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtmBHYTs.Where(x => x.INamLamViec == namLamViec && x.SSoChungTu == sct).ToList();
            }
        }

        public List<string> FindCurrentUnits(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtmBHYTs.Where(x => x.INamLamViec == namLamViec).Select(x => x.IIDMaDonVi).Distinct().ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhKhtmBHYTs.Where(x => x.INamLamViec == yearOfWork).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    var indexString = result.OrderByDescending(x => x.SSoChungTu).FirstOrDefault().SSoChungTu.Substring(5, 3);
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
                var rootDonVi = ctx.NsDonVis.FirstOrDefault(t => t.NamLamViec == namLamViec && LoaiDonVi.ROOT.Equals(t.Loai))?.IIDMaDonVi;
                return ctx.BhKhtmBHYTs.Any(t => t.IIDMaDonVi.Equals(rootDonVi) && t.INamLamViec == namLamViec && t.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop);
            }
        }

        public void LockOrUnLock(string id, bool isLock)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var chungTu = ctx.BhKhtmBHYTs.First(x => x.Id.ToString() == id);
                chungTu.BKhoa = isLock;
                ctx.SaveChanges();
            }
        }

        public IEnumerable<BhKhtmBHYT> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtmBHYTs.Where(x => voucherNoes.Contains(x.SSoChungTu) && x.INamLamViec == yearOfWork).ToList();
            }
        }

        public BhKhtmBHYT FindAggregateVoucher(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtmBHYTs.FirstOrDefault(x => x.INamLamViec == yearOfWork && x.ILoaiTongHop == 2 && x.BKhoa == true);
            }
        }
    }
}
