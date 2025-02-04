using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class KhtBHXHRepository : Repository<BhKhtBHXH>, IKhtBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public KhtBHXHRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregate(KhtBHXHChiTietCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", creation.ListIdChungTuTongHop);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", creation.IdChungTu);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", creation.NamLamViec);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_kht_bhxh_chungtu_chitiet_tao_tonghop @ListIdChungTuTongHop, @IdChungTu, @NamLamViec",
                    listIdChungTuTongHop, idChungTu, namLamViec);
            }
        }
        public IEnumerable<BhKhtBHXHQuery> FindByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<BhKhtBHXHQuery>("EXECUTE dbo.sp_bhxh_thongtinkehoachthu_index @YearOfWork", yearOfWorkParam).ToList();
            }
        }
        public IEnumerable<BhKhtBHXHQuery> FindChungTuChiTietTongHopByCondition(int namLamViec, int loaiTongHop, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter loaiTongHopParam = new SqlParameter("@LoaiTongHop", loaiTongHop);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                return ctx.FromSqlRaw<BhKhtBHXHQuery>("EXECUTE dbo.sp_kht_bhxh_chung_tu_chi_tiet_tong_hop @YearOfWork, @LoaiTongHop, @UserName", yearOfWorkParam, loaiTongHopParam, userNameParam).ToList();
            }
        }

        public IEnumerable<BhKhtBHXHQuery> FindChungTuChiTietByCondition(int namLamViec, bool bDaTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter daTongHopParam = new SqlParameter("@DaTongHop", bDaTongHop);
                return ctx.FromSqlRaw<BhKhtBHXHQuery>("EXECUTE dbo.sp_kht_bhxh_chung_tu_chi_tiet @YearOfWork, @DaTongHop", yearOfWorkParam, daTongHopParam).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhKhtBHXHs.Where(x => x.INamLamViec == yearOfWork).ToList();
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

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var rootDonVi = ctx.NsDonVis.FirstOrDefault(t => t.NamLamViec == namLamViec && LoaiDonVi.ROOT.Equals(t.Loai))?.IIDMaDonVi;
                return ctx.BhKhtBHXHs.Any(t => t.IID_MaDonVi.Equals(rootDonVi) && t.INamLamViec == namLamViec && t.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop);
            }
        }

        public void LockOrUnLock(string id, bool isLock)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var chungTu = ctx.BhKhtBHXHs.First(x => x.Id.ToString() == id);
                chungTu.BIsKhoa = isLock;
                ctx.SaveChanges();
            }
        }
        public IEnumerable<BhKhtBHXH> FindByCondition(int namLamViec, string maDonVi, int loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            { 
                return ctx.BhKhtBHXHs.Where(x => x.INamLamViec == namLamViec && x.IID_MaDonVi == maDonVi && x.ILoaiTongHop == loaiChungTu).ToList();
            }
        }

        public IEnumerable<BhKhtBHXH> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtBHXHs.Where(x => x.INamLamViec == namLamViec && x.SSoChungTu == sct).ToList();
            }
        }

        public List<string> FindCurrentUnits(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtBHXHs.Where(x => x.INamLamViec == namLamViec).Select(x => x.IID_MaDonVi).Distinct().ToList();
            }
        }

        public IEnumerable<BhKhtBHXH> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtBHXHs.Where(x => voucherNoes.Contains(x.SSoChungTu) && x.INamLamViec == yearOfWork).ToList();
            }
        }

        public BhKhtBHXH FindAggregateVoucher(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtBHXHs.FirstOrDefault(x => x.INamLamViec == yearOfWork && x.ILoaiTongHop == 2 && x.BIsKhoa == true);
            }
        }

        public IEnumerable<BhKhtBHXH> FindByVoucherType(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtBHXHs.Where(x => x.INamLamViec == namLamViec && x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTu).ToList();
            }
        }

        public IEnumerable<BhKhtBHXH> FindByVoucherAggregateType(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtBHXHs.Where(x => x.INamLamViec == namLamViec && x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop 
                    && !string.IsNullOrEmpty(x.STongHop)).ToList();
            }
        }
    }
}
