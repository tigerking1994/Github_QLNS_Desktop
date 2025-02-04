using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.Data.SqlClient;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QttBHXHRepository : Repository<BhQttBHXH>, IQttBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QttBHXHRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQttBHXH> FindAggregateVoucher(string sct, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHs.Where(x => x.INamLamViec == namLamViec && x.SSoChungTu == sct).ToList();
            }
        }

        public IEnumerable<BhQttBHXHQuery> FindByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<BhQttBHXHQuery>("EXECUTE dbo.sp_bh_quyet_toan_thu_bhxh_index @YearOfWork", yearOfWorkParam).ToList();
            }
        }

        public int GetVoucherIndex(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhQttBHXHs.Where(x => x.INamLamViec == year).ToList();
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

        public bool IsExistAggregateVoucher(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var rootDonVi = ctx.NsDonVis.FirstOrDefault(t => t.NamLamViec == namLamViec && LoaiDonVi.ROOT.Equals(t.Loai))?.IIDMaDonVi;
                return ctx.BhQttBHXHs.Any(t => t.IIDMaDonVi.Equals(rootDonVi) && t.INamLamViec == namLamViec && t.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop);
            }
        }

        public IEnumerable<BhQttQuarterQuery> GetQuarterYearByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                return ctx.FromSqlRaw<BhQttQuarterQuery>("EXECUTE dbo.sp_bh_qtt_get_quy_nam @NamLamViec", namLamViecParam).ToList();
            }
        }

        public List<int> GetVoucherYears(int year)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHs.Where(x => x.IQuyNamLoai == 2 && x.IQuyNam >= year - 10 && x.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTu)
                    .Select(x => x.IQuyNam).Distinct().ToList();
            }
        }

        public IEnumerable<BhQttBHXH> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHs.Where(x => x.INamLamViec == namLamViec && x.SSoChungTu == sct).ToList();
            }
        }

        public IEnumerable<BhQttBHXH> FindByCondition(string sIdDonVi, int namLamViec, int selectedQuarter, int selectedQuarterType, bool isDonViCha)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var selectedQuarterList = new List<int>();
                var loaiQuarterList = new List<int>();

                if (selectedQuarterType == 0)
                {
                    selectedQuarterList = new List<int> { selectedQuarter };
                    loaiQuarterList = new List<int> { selectedQuarterType };
                }
                else
                {
                    if (selectedQuarterType == 1 && selectedQuarter == 3)
                    {
                        selectedQuarterList = new List<int> { 1, 2, 3 };
                    }
                    else if (selectedQuarterType == 1 && selectedQuarter == 6)
                    {
                        selectedQuarterList = new List<int> { 4, 5, 6 };
                    }
                    else if (selectedQuarterType == 1 && selectedQuarter == 9)
                    {
                        selectedQuarterList = new List<int> { 7, 8, 9 };
                    }
                    else if (selectedQuarterType == 1 && selectedQuarter == 12)
                    {
                        selectedQuarterList = new List<int> { 10, 11, 12 };
                    }
                    loaiQuarterList = new List<int> { 0, 1 };
                }
                var listBhCTCT = ctx.BhQttBHXHChiTiets.Select(x => x.QttBHXHId).ToList();
                var listBhCT = ctx.BhQttBHXHs.Where(x => x.INamLamViec == namLamViec && selectedQuarterList.Contains(x.IQuyNam) && loaiQuarterList.Contains(x.IQuyNamLoai) && (!isDonViCha || (isDonViCha && x.ILoaiTongHop == 2)));

                var result = listBhCT.Where(t => listBhCTCT.Contains(t.Id)).ToList();
                return result;
            }
        }

        public IEnumerable<BhQttBHXH> FindByCondition(int namLamViec, int quyNam, int quyNamLoai, int loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHs.Where(x => x.INamLamViec == namLamViec && x.IQuyNam == quyNam && x.IQuyNamLoai == quyNamLoai && x.ILoaiTongHop == loaiChungTu).ToList();
            }
        }

        public IEnumerable<BhQttBHXHQuery> FindChungTuDonVi(int namLamViec, int loaiTongHop, bool bDaTongHop, int quyNam, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter loaiTongHopParam = new SqlParameter("@LoaiTongHop", loaiTongHop);
                SqlParameter daTongHopParam = new SqlParameter("@DaTongHop", bDaTongHop);
                SqlParameter quyNamParam = new SqlParameter("@QuyNam", quyNam);
                SqlParameter loaiQuyParam = new SqlParameter("@LoaiQuyNam", loaiQuy);

                return ctx.FromSqlRaw<BhQttBHXHQuery>("EXECUTE dbo.sp_bh_qtt_get_chung_tu_don_vi @YearOfWork, @LoaiTongHop, @DaTongHop, @QuyNam, @LoaiQuyNam", 
                    yearOfWorkParam, loaiTongHopParam, daTongHopParam, quyNamParam, loaiQuyParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHQuery> FindChungTuDonViThangQuy(int namLamViec, int loaiTongHop, bool bDaTongHop, int quyNam, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter loaiTongHopParam = new SqlParameter("@LoaiTongHop", loaiTongHop);
                SqlParameter daTongHopParam = new SqlParameter("@DaTongHop", bDaTongHop);
                SqlParameter quyNamParam = new SqlParameter("@QuyNam", quyNam);
                SqlParameter loaiQuyParam = new SqlParameter("@LoaiQuyNam", loaiQuy);

                return ctx.FromSqlRaw<BhQttBHXHQuery>("EXECUTE dbo.sp_bh_qtt_get_chung_tu_don_vi_thang_quy @YearOfWork, @LoaiTongHop, @DaTongHop, @QuyNam, @LoaiQuyNam",
                    yearOfWorkParam, loaiTongHopParam, daTongHopParam, quyNamParam, loaiQuyParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHQuery> FindAllChungTuDonVi(int namLamViec, int quyNam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter quyNamParam = new SqlParameter("@QuyNam", quyNam);
                return ctx.FromSqlRaw<BhQttBHXHQuery>("EXECUTE dbo.sp_bh_qtt_get_all_chung_tu_don_vi @YearOfWork, @QuyNam",
                    yearOfWorkParam, quyNamParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHQuery> FindChungTuDonViTongHop(int namLamViec, int loaiTongHop, string userName, int quyNam, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter loaiTongHopParam = new SqlParameter("@LoaiTongHop", loaiTongHop);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter quyNamParam = new SqlParameter("@QuyNam", quyNam);
                SqlParameter loaiQuyParam = new SqlParameter("@LoaiQuyNam", loaiQuy);

                return ctx.FromSqlRaw<BhQttBHXHQuery>("EXECUTE dbo.sp_bh_qtt_get_chung_tu_don_vi_tong_hop @YearOfWork, @LoaiTongHop, @UserName, @QuyNam, @LoaiQuyNam", 
                    yearOfWorkParam, loaiTongHopParam, userNameParam, quyNamParam, loaiQuyParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHQuery> FindChungTuDonViTongHopThangQuy(int namLamViec, int loaiTongHop, string userName, int quyNam, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter loaiTongHopParam = new SqlParameter("@LoaiTongHop", loaiTongHop);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter quyNamParam = new SqlParameter("@QuyNam", quyNam);
                SqlParameter loaiQuyParam = new SqlParameter("@LoaiQuyNam", loaiQuy);

                return ctx.FromSqlRaw<BhQttBHXHQuery>("EXECUTE dbo.sp_bh_qtt_get_chung_tu_don_vi_tong_hop_thang @YearOfWork, @LoaiTongHop, @UserName, @QuyNam, @LoaiQuyNam",
                    yearOfWorkParam, loaiTongHopParam, userNameParam, quyNamParam, loaiQuyParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHQuery> GetAggregateParentUnit(int iNamLamViec, string sIdDonVi, int selectedQuarter)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter iQuy = new SqlParameter("@IQuy ", selectedQuarter);
                return ctx.FromSqlRaw<BhQttBHXHQuery>("EXECUTE dbo.sp_bh_qtt_get_don_vi_tong_hop @NamLamViec, @IdDonVi, @IQuy",
                    iNamLamViecParam, sIdDonViParam, iQuy).ToList();
            }
        }

        public List<string> FindCurrentUnits(int namLamViec, int selectedQuarter, int loaiQuy, bool isInBudget)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var selectedQuarterList = new List<int>();
                var loaiQuarterList = new List<int>();

                if (loaiQuy == 0)
                {
                    selectedQuarterList = new List<int> { selectedQuarter };
                    loaiQuarterList = new List<int> { loaiQuy };
                }
                else if (loaiQuy == 1)
                {
                    if (selectedQuarter == 3)
                    {
                        selectedQuarterList = new List<int> { 1, 2, 3 };
                    }
                    else if (selectedQuarter == 6)
                    {
                        selectedQuarterList = new List<int> { 4, 5, 6 };
                    }
                    else if (selectedQuarter == 9)
                    {
                        selectedQuarterList = new List<int> { 7, 8, 9 };
                    }
                    else if ( selectedQuarter == 12)
                    {
                        selectedQuarterList = new List<int> { 10, 11, 12 };
                    }
                    loaiQuarterList = new List<int> { 1 };
                }
                else
                {
                    selectedQuarterList = new List<int> { selectedQuarter };
                    loaiQuarterList = new List<int> { 2 };
                }
                var listBhCTCT = ctx.BhQttBHXHChiTiets;
                var listBhCT = ctx.BhQttBHXHs.Where(x => x.INamLamViec == namLamViec && selectedQuarterList.Contains(x.IQuyNam) && loaiQuarterList.Contains(x.IQuyNamLoai) && (!isInBudget || (isInBudget && x.ILoaiTongHop == 1)));

                var sql = from bhctct in listBhCTCT
                          join bhct in listBhCT
                          on bhctct.QttBHXHId equals bhct.Id into tbl
                          from m in tbl.DefaultIfEmpty()
                          where m.INamLamViec == namLamViec && selectedQuarterList.Contains(m.IQuyNam) && loaiQuarterList.Contains(m.IQuyNamLoai)
                          select new { IIDMaDonVi = bhctct.IIDMaDonVi };
                var result = sql.ToList().Select(t => t.IIDMaDonVi).ToList();
                result.AddRange(listBhCT.Select(t => t.IIDMaDonVi).ToList());
                return result.Distinct().ToList();
            }
        }

        public bool HasMonthlyVouchers(int iNamLamViec, int iQuy, int iLoai, bool isLuyKe, string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var arrMaDonVi = sMaDonVi.Split(",");
                if (iQuy == 3 && iLoai == 1)
                {
                    string[] months = { "1", "2", "3" };
                    return ctx.BhQttBHXHs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi));
                }
                else if (iQuy == 6 && iLoai == 1 && !isLuyKe)
                {
                    string[] months = { "4", "5", "6" };
                    return ctx.BhQttBHXHs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi));
                }
                else if (iQuy == 9 && iLoai == 1 && !isLuyKe)
                {
                    string[] months = { "7", "8", "9" };
                    return ctx.BhQttBHXHs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi));
                }
                else if (iQuy == 12 && iLoai == 1 && !isLuyKe)
                {
                    string[] months = { "10", "11", "12" };
                    return ctx.BhQttBHXHs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi));
                }

                else if (iQuy == 6 && iLoai == 1 && isLuyKe)
                {
                    string[] months = { "1", "2", "3", "4", "5", "6" };
                    return ctx.BhQttBHXHs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi));
                }

                else if (iQuy == 9 && iLoai == 1 && isLuyKe)
                {
                    string[] months = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    return ctx.BhQttBHXHs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi));
                }
                else if (iQuy == 12 && iLoai == 1 && isLuyKe)
                {
                    string[] months = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                    return ctx.BhQttBHXHs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi));
                }
                else return false;
            }
        }

        public int GetNumOfMonthlyVoucher(int year, string sMaDonVi, bool isLuyKe, int iquy, int iLoaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var arrMaDonVi = sMaDonVi.Split(",");
                int[] q1 = { 1, 2, 3 };
                int[] q2 = { 4, 5, 6 };
                int[] q3 = { 7, 8, 9 };
                int[] q4 = { 10, 11, 12 };
                int numOfMonth;
                if (isLuyKe)
                {
                    var vouchers = ctx.BhQttBHXHs.Where(x => x.INamLamViec == year && arrMaDonVi.Contains(x.IIDMaDonVi) && x.IQuyNamLoai == 0 && x.IQuyNam <= iquy).ToList();
                    numOfMonth = vouchers.Count();
                }
                else
                {
                    if (iLoaiQuy == 1 && iquy == 3)
                    {
                        var vouchers = ctx.BhQttBHXHs.Where(x => q1.Contains(x.IQuyNam) && x.INamLamViec == year && arrMaDonVi.Contains(x.IIDMaDonVi) && x.IQuyNamLoai == 0).ToList();
                        numOfMonth = vouchers.Count();
                    }
                    else if (iLoaiQuy == 1 && iquy == 6)
                    {
                        var vouchers = ctx.BhQttBHXHs.Where(x => q2.Contains(x.IQuyNam) && x.INamLamViec == year && arrMaDonVi.Contains(x.IIDMaDonVi) && x.IQuyNamLoai == 0).ToList();
                        numOfMonth = vouchers.Count();
                    }
                    else if (iLoaiQuy == 1 && iquy == 9)
                    {
                        var vouchers = ctx.BhQttBHXHs.Where(x => q3.Contains(x.IQuyNam) && x.INamLamViec == year && arrMaDonVi.Contains(x.IIDMaDonVi) && x.IQuyNamLoai == 0).ToList();
                        numOfMonth = vouchers.Count();
                    }
                    else
                    {
                        var vouchers = ctx.BhQttBHXHs.Where(x => q4.Contains(x.IQuyNam) && x.INamLamViec == year && arrMaDonVi.Contains(x.IIDMaDonVi) && x.IQuyNamLoai == 0).ToList();
                        numOfMonth = vouchers.Count();
                    }
                }
                return numOfMonth;
            }
        }
    }
}
