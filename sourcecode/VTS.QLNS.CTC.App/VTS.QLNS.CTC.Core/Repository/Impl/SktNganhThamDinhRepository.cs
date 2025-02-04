using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SktNganhThamDinhRepository : Repository<NsSktNganhThamDinh>, ISktNganhThamDinhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SktNganhThamDinhRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool CheckExitsByChungTuId(Guid chungtuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int count = ctx.NsSktNganhThamDinhChiTiets.Where(n => n.IIdCtnganhThamDinh == chungtuId).ToList() != null ? ctx.NsSktNganhThamDinhChiTiets.Where(n => n.IIdCtnganhThamDinh == chungtuId).ToList().Count : 0;
                return count > 0;
            }
        }

        public IEnumerable<ThDChungTuQuery> FindByNamLamViec(int namLamViec, int namNganSach, int nguonNganSach, string userName, int loai, int loaiNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                SqlParameter budgetOfSourceParam = new SqlParameter("@BudgetOfSource", nguonNganSach);
                SqlParameter loaiParam = new SqlParameter("@Loai", loai);
                SqlParameter loaiNganSachParam = new SqlParameter("@LoaiNganSach", loaiNganSach);
                return ctx.FromSqlRaw<ThDChungTuQuery>("EXECUTE dbo.sp_thd_chungtu @YearOfWork, @YearOfBudget, @BudgetOfSource, @UserName, @Loai, @LoaiNganSach",
                    yearOfWorkParam, yearOfBudgetParam, budgetOfSourceParam, userNameParam, loaiParam, loaiNganSachParam).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NsSktNganhThamDinh> result = ctx.NsSktNganhThamDinhs.Where(n => n.INamLamViec == namLamViec).OrderByDescending(n => n.ISoChungTuIndex).ToList();
                if (result.Count > 0 && result.FirstOrDefault().ISoChungTuIndex.HasValue && result.FirstOrDefault().ISoChungTuIndex > 0)
                {
                    return (result.FirstOrDefault().ISoChungTuIndex.Value + 1);
                }
                return 1;
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                NsSktNganhThamDinh entity = ctx.NsSktNganhThamDinhs.Find(id);
                entity.BKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public void CreateVoucherAggregate(string voucherId, string maDonVi, string tenDonVi, int namLamViec, string userCreate, int namNganSach, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter("@VoucherId", voucherId);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter tenDonViParam = new SqlParameter("@TenDonVi", tenDonVi);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter userCreateParam = new SqlParameter("@UserCreate", userCreate);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach); 
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_thd_tonghopchungtu @VoucherId, @MaDonVi, @TenDonVi, @NamLamViec, @UserCreate, @NamNganSach, @NguonNganSach",
                    voucherIdParam, maDonViParam, tenDonViParam, namLamViecParam, userCreateParam, namNganSachParam, nguonNganSachParam);
            }
        }
    }
}
