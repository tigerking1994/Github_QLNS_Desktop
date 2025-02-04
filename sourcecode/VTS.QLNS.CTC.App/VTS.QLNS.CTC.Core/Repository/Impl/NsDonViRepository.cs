using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using static VTS.QLNS.CTC.Utility.Enum.BaoHiemDuToanTypeEnum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsDonViRepository : Repository<DonVi>, INsDonViRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsDonViRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int countNsDonViByNamLamViec(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.Where(n => n.NamLamViec == namLamViec).Count();
            }
        }

        public IEnumerable<DonVi> FindByNamLamViec(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.Where(n => n.NamLamViec == namLamViec && n.ITrangThai == NSEntityStatus.ACTIVED).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindAllDataDonVi()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.Where(n => n.ITrangThai == NSEntityStatus.ACTIVED).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public DonVi FindByIdDonVi(string idDonVi, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.FirstOrDefault(n => n.IIDMaDonVi == idDonVi && n.NamLamViec == namLamViec && n.ITrangThai == 1);
            }
        }

        public IEnumerable<DonVi> FindByCondition(int loai, int trangThai, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.Where(x =>
                x.Loai == loai.ToString() && x.ITrangThai == trangThai && x.NamLamViec == namLamViec).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindByListIdDonVi(string listIdDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var listSearch = listIdDonVi.Split(',').ToArray();
                var listDonVi = (from item in ctx.NsDonVis
                                 where listSearch.Contains(item.IIDMaDonVi)
                                 select new DonVi() { IIDMaDonVi = item.IIDMaDonVi, TenDonVi = item.TenDonVi }).Distinct().OrderBy(x => x.IIDMaDonVi).ToList();
                return listDonVi;
            }
        }

        public IEnumerable<DonVi> FindByListIdDonVi(IEnumerable<string> listIdDonVi, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var listDonVi = (from item in ctx.NsDonVis
                                 where listIdDonVi.Contains(item.IIDMaDonVi)
                                 && item.NamLamViec == namLamViec
                                 select new DonVi() { IIDMaDonVi = item.IIDMaDonVi, TenDonVi = item.TenDonVi }).Distinct().OrderBy(x => x.IIDMaDonVi).ToList();
                return listDonVi;
            }
        }

        public IEnumerable<DonVi> FindByListIdDonVi(string idsDonVi, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                if (String.IsNullOrEmpty(idsDonVi))
                {
                    return new List<DonVi>();
                }
                List<string> listId = idsDonVi.Split(',').ToList();
                return ctx.NsDonVis.Where(n => listId.Contains(n.IIDMaDonVi) && n.NamLamViec == namLamViec).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonViPlanBeginYearQuery> FindPlanBeginYearByConditon(int namLamViec, int namNganSach, int nguonNganSach, string loai, int loaiNNS, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_skt_plan_begin_year_2 @YearOfWork, @YearOfBudget, @BudgetSource, @Loai, @iLoaiNNS, @UserName";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@YearOfBudget", namNganSach),
                    new SqlParameter("@BudgetSource", nguonNganSach),
                    new SqlParameter("@Loai", loai),
                    new SqlParameter("@iLoaiNNS", loaiNNS),
                    new SqlParameter("@UserName", userName)
                };
                return ctx.FromSqlRaw<DonViPlanBeginYearQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NSDonViExportQuery> GetDonViExportByNamLamViec(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_ns_get_donvi_export @iNamLamViec";
                var parameters = new[]
                {
                    new SqlParameter("@iNamLamViec", iNamLamViec)
                };
                return ctx.FromSqlRaw<NSDonViExportQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<DonViNgayChungTuQuery> FindByNamLamViecHasCapPhatChiTiet(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_cp_rpt_get_donvi_lns @NamLamViec";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec)
                };
                return ctx.FromSqlRaw<DonViNgayChungTuQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<DonViNgayChungTuQuery> FindByNgayChungTu(int namLamViec, DateTime ngayChungTu, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dutoan_rpt_get_target_donvi @NamLamViec, @NgayChungTu, @IsLuyKe";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NgayChungTu", ngayChungTu),
                    new SqlParameter("@IsLuyKe", isLuyKe ? 1 : 0)
                };
                return ctx.FromSqlRaw<DonViNgayChungTuQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<DonVi> FindDonViHasDataSktSoLieuChiTiet(int namLamViec, int namNganSach, int nguonNganSach, string loaiChungTu, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_skt_get_donvi_has_solieu_chitiet @YearOfWork, @YearOfBudget, @BudgetOfSource, @LoaiChungTu, @iLoaiNNS";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@YearOfBudget", namNganSach),
                    new SqlParameter("@BudgetOfSource", nguonNganSach),
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@iLoaiNNS", loaiNNS)
                };
                return ctx.Set<DonVi>().FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindAllChildByIdDonVi(string idDonVi, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<DonVi> rs = new List<DonVi>();
                var donVi = ctx.NsDonVis.Include(x => x.Children)
                    .First(x => x.IIDMaDonVi.Equals(idDonVi) && x.NamLamViec == namLamViec && x.ITrangThai == NSEntityStatus.ACTIVED);
                if (donVi != null)
                {
                    return donVi.Children.OrderBy(x => x.IIDMaDonVi);
                }
                return rs.OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public DonVi FindById(Guid idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.FirstOrDefault(n => n.Id.Equals(idDonVi));
            }
        }

        public DonVi FindByLoai(string loai, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.FirstOrDefault(n => loai == n.Loai && namLamViec == n.NamLamViec && NSEntityStatus.ACTIVED == n.ITrangThai);
            }
        }

        public bool IsDonViCha(string maDonVi, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.Any(n => n.Loai == "0" && n.IIDMaDonVi == maDonVi && namLamViec == n.NamLamViec && NSEntityStatus.ACTIVED == n.ITrangThai);
            }
        }

        public IEnumerable<string> FindChildNsDonVi(string idDonVi, int yearOfWork, int status)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var listIdDonVi = ctx.NsDonVis
                .Where(item => item.NamLamViec.Equals(yearOfWork) && item.IIDMaDonVi.Equals(idDonVi))
                .Select(item => item.Id.ToString()).ToHashSet();

                return ctx.NsDonVis.Where(item =>
                        item.NamLamViec.Equals(yearOfWork) && item.ITrangThai.Equals(status) &&
                        listIdDonVi.Contains(item.IdParent.ToString()))
                    .Select(item => item.IIDMaDonVi).Distinct().OrderBy(x => x).ToList();
            }
        }

        public IEnumerable<DonVi> FindBySettlementMonth(int yearOfWork, int yearOfBudget, int budgetSource, string quarterMonth, int quarterMonthType, string loaiQuyetToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_donvi_thang @YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @QuarterMonthType, @LoaiQuyetToan";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@YearOfBudget", yearOfBudget),
                    new SqlParameter("@BudgetSource", budgetSource),
                    new SqlParameter("@QuarterMonth", quarterMonth),
                    new SqlParameter("@QuarterMonthType", quarterMonthType),
                    new SqlParameter("@LoaiQuyetToan", loaiQuyetToan)
            };
                return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindByEstimateSettlement(int yearOfWork, int yearOfBudget, int budgetSource, DateTime voucherDate, int quarterMonth, int quarterMonthType)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dt_chitieu_donvi_all @YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @VoucherId, @BranchId, @AgencyId, @Type, @QuarterMonth, @QuarterMonthType";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@YearOfBudget", yearOfBudget),
                    new SqlParameter("@BudgetSource", budgetSource),
                    new SqlParameter("@VoucherDate", voucherDate),
                    new SqlParameter("@VoucherId", DBNull.Value),
                    new SqlParameter("@BranchId", DBNull.Value),
                    new SqlParameter("@AgencyId", DBNull.Value),
                    new SqlParameter("@Type", DBNull.Value),
                    new SqlParameter("@QuarterMonth", quarterMonth),
                    new SqlParameter("@QuarterMonthType", quarterMonthType)
                };
                return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public DonVi FirstOrDefault(Func<DonVi, bool> condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.FirstOrDefault(condition);
            }
        }

        public IEnumerable<DonVi> FindByLoai(int namLamViec, string loai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.Where(n => n.NamLamViec == namLamViec && n.Loai == loai).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindForReceiveSettlementReport(int yearOfWork, string yearOfBudget, int budgetSource, string lns)
        {
            using var ctx = _contextFactory.CreateDbContext();
            string sql = "EXECUTE dbo.sp_qt_nhan_quyettoankinhphi_donvi @YearOfWork, @YearOfBudget, @BudgetSource, @LNS";
            var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@YearOfBudget", yearOfBudget),
                    new SqlParameter("@BudgetSource", budgetSource),
                    new SqlParameter("@LNS", lns)
                };
            return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
        }

        public IEnumerable<DonVi> FindBySettlement(int yearOfWork, int budgetSource, int dataType, string lns)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_donvi @YearOfWork, @BudgetSource, @DataType, @LNS";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@BudgetSource", budgetSource),
                    new SqlParameter("@DataType", dataType),
                     new SqlParameter("@LNS", lns)
                };
                return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindBySettlement(int yearOfWork, int budgetSource, int dataType, string lns, string type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_donvi_nam @YearOfWork, @BudgetSource, @DataType, @LNS, @Loai";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@BudgetSource", budgetSource),
                    new SqlParameter("@DataType", dataType),
                     new SqlParameter("@LNS", lns),
                     new SqlParameter("@Loai",type)
                };
                return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public List<DonVi> GetDanhSachDonViByNguoiDung(string sMaNguoiDung, int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_ns_get_danhsachdonvi_by_nguoidung @sMaNguoiDung, @iNamLamViec_DonVi";
                var parameters = new[]
                {
                    new SqlParameter("@sMaNguoiDung", sMaNguoiDung),
                    new SqlParameter("@iNamLamViec_DonVi", iNamLamViec)
                };
                return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindBySummaryVoucherList(int yearOfWork, int quarterMonth, string lns)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bk_donvi_tonghop @YearOfWork, @QuarterMonth, @LNS";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@QuarterMonth", quarterMonth),
                    new SqlParameter("@LNS", lns)
                };
                return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonViQuery> FindAllDonViNotDuplicate()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<DonViQuery>("EXECUTE dbo.sp_get_all_donvi").OrderBy(x => x.IdDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindByUser(string userName, int yearOfWork, string type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_ns_dv_get_by_user_and_type @UserName, @YearOfWork, @Type";
                var parameters = new[]
                {
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@Type", type)
                };
                return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindByUserCreateVoucher(string userName, int yearOfWork, string type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_ns_dv_get_by_user_and_type_create_voucher @UserName, @YearOfWork, @Type";
                var parameters = new[]
                {
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@Type", type)
                };
                return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindForEstimateDivisionReport(int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string idChungTu, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dutoan_rpt_get_target_donvi @NamLamViec, @NamNganSach, @NguonNganSach, @LoaiChungTu, @IdChungTu, @IsLuyke";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@IdChungTu", idChungTu),
                    new SqlParameter("@IsLuyke", isLuyKe ? 1 : 0)
                };
                return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindForRevenueExpenditureDivisionReport(int namLamViec, int namNganSach, int nguonNganSach, string idChungTu, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_thunop_rpt_get_target_donvi @NamLamViec, @NamNganSach, @NguonNganSach, @IdChungTu, @IsLuyke";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@IdChungTu", idChungTu),
                    new SqlParameter("@IsLuyke", isLuyKe ? 1 : 0)
                };
                return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindForSocialInsuranceEstimateDivisionReport(int namLamViec, string idChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_dutoan_rpt_get_target_donvi @NamLamViec, @IdChungTu";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@IdChungTu", idChungTu),
                };
                return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindHospitalTargetAgencyReportDonVi(int namLamViec, string idChungTu, int loaiChungTu)
        {
            List<string> ids = idChungTu.Split(",").ToList();
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var sql = from ct in ctx.NsDtChungTus
                          join chitiet in ctx.NsDtChungTuChiTiets on ct.Id equals chitiet.IIdDtchungTu
                          join donvi in ctx.NsDonVis.Where(dv => dv.NamLamViec == namLamViec) on chitiet.IIdMaDonVi equals donvi.IIDMaDonVi
                          where ct.INamLamViec == namLamViec && chitiet.INamLamViec == namLamViec && ct.ILoai == loaiChungTu
                          select new { donvi, ct };
                return sql.Where(item => ids.Contains(item.ct.Id.ToString())).Select(item => item.donvi).Distinct().OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindForAdjustmentEstimateReport(int yearOfWork, int yearOfBudget, int budgetSouce, int dot)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                if (!ctx.NsDonVis.Any(x => x.Loai == LoaiDonVi.NOI_BO && x.NamLamViec == yearOfWork))
                {
                    var sql = from ct in ctx.NsDcChungTus
                              join donvi in ctx.NsDonVis.Where(dv => dv.NamLamViec == yearOfWork && dv.Loai == LoaiDonVi.ROOT) on ct.IIdMaDonVi equals donvi.IIDMaDonVi
                              where ct.INamLamViec == yearOfWork && ct.INamLamViec == yearOfWork
                                && ct.INamNganSach == yearOfBudget && ct.IIdMaNguonNganSach == budgetSouce
                                && (ct.ILoaiDuKien == dot || dot == -1)
                              select new { donvi, ct };
                    return sql.Select(item => item.donvi).Distinct().OrderBy(x => x.IIDMaDonVi).ToList();
                }
                else
                {
                    var sql = from ct in ctx.NsDcChungTus
                              join donvi in ctx.NsDonVis.Where(dv => dv.NamLamViec == yearOfWork && dv.Loai != LoaiDonVi.ROOT) on ct.IIdMaDonVi equals donvi.IIDMaDonVi
                              where ct.INamLamViec == yearOfWork && ct.INamLamViec == yearOfWork
                                && ct.INamNganSach == yearOfBudget && ct.IIdMaNguonNganSach == budgetSouce
                                && (ct.ILoaiDuKien == dot || dot == -1)
                              select new { donvi, ct };
                    return sql.Select(item => item.donvi).Distinct().OrderBy(x => x.IIDMaDonVi).ToList();
                }
            }
        }

        public IEnumerable<DonVi> FindForSocialInsuranceEstimateReport(int yearOfWork, Guid iIDLoaiCap)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var sql = from ct in ctx.BhDtcDcdToanChis
                          join donvi in ctx.NsDonVis.Where(dv => dv.NamLamViec == yearOfWork && dv.Loai != LoaiDonVi.ROOT) on ct.IID_MaDonVi equals donvi.IIDMaDonVi
                          where ct.INamLamViec == yearOfWork && ct.IID_LoaiCap == iIDLoaiCap
                          select new { donvi, ct };
                return sql.Select(item => item.donvi).Distinct().OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public void SaveDonViSuDung(DonVi donVi, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                DonVi currentDVSD = ctx.NsDonVis.FirstOrDefault(dv => dv.NamLamViec == namLamViec && dv.Loai.Equals(LoaiDonVi.ROOT));
                if (currentDVSD != null)
                {
                    currentDVSD.Loai = LoaiDonVi.NOI_BO;
                }
                DonVi tracked = ctx.NsDonVis.FirstOrDefault(dv => dv.NamLamViec == namLamViec && dv.IIDMaDonVi.Equals(donVi.IIDMaDonVi));
                // override nếu mã đơn vị đã tồn tại
                if (tracked != null)
                {
                    tracked.TenDonVi = donVi.TenDonVi;
                    tracked.KyHieu = donVi.KyHieu;
                    tracked.BCoNSNganh = donVi.BCoNSNganh;
                    tracked.MoTa = donVi.MoTa;
                    tracked.Loai = donVi.Loai;
                }
                else
                {
                    ctx.Add(donVi);
                }
                ctx.SaveChanges();
            }
        }

        public DonVi LoadCurrentDonViSuDung(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.FirstOrDefault(dv => dv.NamLamViec == namLamViec && dv.Loai.Equals("0"));
            }
        }

        public DonVi FindByMaDonViAndNamLamViec(string maDonVi, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.FirstOrDefault(dv => dv.NamLamViec == namLamViec && dv.IIDMaDonVi.Equals(maDonVi));
            }
        }

        public IEnumerable<DonViPlanBeginYearQuery> FindPlanBeginYearAgencyByConditon(int namLamViec, int namNganSach, int nguonNganSach, string loai, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_skt_plan_begin_year_agency @YearOfWork, @YearOfBudget, @BudgetSource, @Loai, @UserName";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@YearOfBudget", namNganSach),
                    new SqlParameter("@BudgetSource", nguonNganSach),
                    new SqlParameter("@Loai", loai),
                    new SqlParameter("@UserName", userName)
                };
                return ctx.FromSqlRaw<DonViPlanBeginYearQuery>(sql, parameters).ToList();
            }
        }

        public int CountDonVi()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.Count();
            }
        }

        public IEnumerable<DonVi> FindByQtTongHopQuy(int yearOfWork, string yearOfBudget, int budgetSource, string quarterMonth, string khoi, string loaiQuyetToan, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_rpt_tonghop_quy_donvi @YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @Khoi, @LoaiQuyetToan, @UserName";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@YearOfBudget", yearOfBudget),
                    new SqlParameter("@BudgetSource", budgetSource),
                    new SqlParameter("@QuarterMonth", quarterMonth),
                    new SqlParameter("@Khoi", string.IsNullOrEmpty(khoi) ? string.Empty : khoi),
                    new SqlParameter("@LoaiQuyetToan",  string.IsNullOrEmpty(loaiQuyetToan) ? string.Empty : loaiQuyetToan),
                    new SqlParameter("@UserName", userName)
                };
                return ctx.NsDonVis.FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindByCapPhatId(int yearOfWork, string listCapPhatId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_get_donvi @YearOfWork, @CapPhatIds";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@CapPhatIds", listCapPhatId)
                };
                return ctx.NsDonVis.FromSql(executeSql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindByCapPhatIdForBH(int yearOfWork, string listCapPhatId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_get_donvi_bh @YearOfWork, @CapPhatIds";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@CapPhatIds", listCapPhatId)
                };
                return ctx.NsDonVis.FromSql(executeSql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindByDonViOfAllocationPlanForBH(int yearOfWork, string listMaDonVi, int iQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_get_donvi_report_kehoach_bh @YearOfWork, @IdMaDonVi, @iQuy";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@IdMaDonVi", listMaDonVi),
                    new SqlParameter("@iQuy", iQuy)
                };
                return ctx.NsDonVis.FromSql(executeSql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindByDonViOfAllocationTongHopForBH(int yearOfWork, int quy, Guid idLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_get_donvi_tonghop_bh @YearOfWork, @Quy, @IDLoaiChi";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@Quy", quy),
                    new SqlParameter("@IDLoaiChi", idLoaiChi)
                };
                return ctx.NsDonVis.FromSql(executeSql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindByCapPhatId2(int yearOfWork, string listCapPhatId, int loaiNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_get_donvi_2 @YearOfWork, @CapPhatIds, @ILoaiNganSach";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@CapPhatIds", listCapPhatId),
                    new SqlParameter("@ILoaiNganSach", loaiNganSach)
                };
                return ctx.NsDonVis.FromSql(executeSql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindByQuanSo(int yearOfWork, string months)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_qs_get_donvi @YearOfWork, @Months";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@Months", months),
                };
                return ctx.NsDonVis.FromSql(executeSql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindBySummaryAgencySettlement(int yearOfWork, int yearOfBudget, int budgetSouce, string lns, string quarterMonth, DateTime voucherDate, bool hasDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_qt_tonghop_donvi_donvi @YearOfWork, @YearOfBudget, @BudgetSource, @LNS, @QuarterMonth, @VoucherDate, @HasDuToan";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@YearOfBudget", yearOfBudget),
                    new SqlParameter("@BudgetSource", budgetSouce),
                    new SqlParameter("@LNS", lns),
                    new SqlParameter("@QuarterMonth", quarterMonth),
                    new SqlParameter("@VoucherDate", voucherDate),
                    new SqlParameter("@HasDuToan", hasDuToan),
                };
                if (lns is null)
                {
                    parameters[3].Value = DBNull.Value;
                }
                return ctx.NsDonVis.FromSql(executeSql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public int CountILoaiByNamLamViec(int year, string type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.Count(dv => dv.NamLamViec == year && dv.Loai.Equals(type));
            }
        }

        public IEnumerable<DonVi> FindByYearAndNhiemVuChi(int namLamViec, bool HasNhiemVuChi = true)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT DISTINCT b.iID_DonVi AS Id, b.iID_MaDonVi AS IIDMaDonVi, b.sTenDonVi AS TenDonVi FROM DonVi b");
                if (HasNhiemVuChi)
                {
                    sql.AppendLine("INNER JOIN NH_KHTongThe_NhiemVuChi c ON b.iID_DonVi = c.iID_DonViThuHuongID AND b.iID_MaDonVi = c.iID_MaDonViThuHuong ");
                }
                sql.AppendLine("WHERE b.iNamLamViec = @namLamViec AND b.iTrangThai = 1 ");
                sql.AppendLine("ORDER BY iID_MaDonVi ");
                var parameters = new[]
                {
                    new SqlParameter("@namLamViec", namLamViec)
                };
                return ctx.FromSqlRaw<DonVi>(sql.ToString(), parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindInTongHopSKTBenhVienTuChu(int yearOfWork, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_skt_donvi_benhvientuchu_report @iNamLamViec, @iLoaiNNS";
                var parameters = new[]
                {
                    new SqlParameter("@iNamLamViec", yearOfWork),
                    new SqlParameter("@iLoaiNNS", loaiNNS)
                };
                return ctx.NsDonVis.FromSql(executeSql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }
        public IEnumerable<DonViQuery> FindAllHopDongByDonViId(Guid idHopDong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = @"SELECT HD.sTenHopDong FROM NH_DA_HopDong HD
                                    JOIN NH_QT_TaiSan TS 
                                    ON HD.ID = TS.iID_HopDongID
                                    WHERE HD.iID_DonViQuanLyID = @idDonVi";
                var parameters = new object[]
                {
                    new SqlParameter("@idDonVi", idHopDong)
                };
                return ctx.FromSqlRaw<DonViQuery>(executeSql, parameters).OrderBy(x => x.IdDonVi).ToList();
            }
        }
        public IEnumerable<DonViQuery> FindAllDuAnByDonViId(Guid idDuAn)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = @"SELECT DA.sTenDuAn FROM NH_DA_DuAn DA
                                    JOIN NH_QT_TaiSan TS 
                                    ON DA.ID = TS.iID_DuAnID
                                    WHERE DA.iID_DonViQuanLyID = @idDuAn";
                var parameters = new object[]
                {
                    new SqlParameter("@idDuAn", idDuAn)
                };
                return ctx.FromSqlRaw<DonViQuery>(executeSql, parameters).OrderBy(x => x.IdDonVi).ToList();
            }
        }
        public IEnumerable<DonVi> FindByYearAndIDNhiemVuChi(int namLamViec, Guid? IDNhiemVuChi, string sLoaiSoCu = null)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT DISTINCT b.iID_DonVi AS Id, b.iID_MaDonVi AS IIDMaDonVi, b.sTenDonVi AS TenDonVi FROM DonVi b");
                sql.AppendLine("INNER JOIN NH_KHTongThe_NhiemVuChi c ON b.iID_DonVi = c.iID_DonViThuHuongID AND b.iID_MaDonVi = c.iID_MaDonViThuHuong ");
                sql.AppendLine("INNER JOIN NH_KHTongThe d ON c.iID_KHTongTheID = d.ID");
                if (!string.IsNullOrEmpty(sLoaiSoCu))
                {
                    if (sLoaiSoCu.Equals(SO_CU_TRUC_TIEP.THONG_TIN_DU_AN))
                    {
                        sql.AppendLine("INNER JOIN NH_DA_DuAn da ON b.iID_MaDonVi = da.iID_MaDonViQuanLy");
                    }
                    else if (sLoaiSoCu.Equals(SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU))
                    {
                        sql.AppendLine("INNER JOIN NH_DA_ChuTruongDauTu ctdt ON b.iID_MaDonVi = ctdt.iID_MaDonViQuanLy");
                    }
                    else if (sLoaiSoCu.Equals(SO_CU_TRUC_TIEP.QUYET_DINH_DAU_TU))
                    {
                        sql.AppendLine("INNER JOIN NH_DA_QDDauTu qddt ON b.iID_MaDonVi = qddt.iID_MaDonViQuanLy");
                    }
                }

                sql.AppendLine("WHERE b.iNamLamViec = @namLamViec AND b.iTrangThai = 1 AND d.ID = @iNhiemVuChi");
                sql.AppendLine("ORDER BY iID_MaDonVi ");
                var parameters = new[]
                {
                    new SqlParameter("@namLamViec", namLamViec),
                    new SqlParameter("@iNhiemVuChi", IDNhiemVuChi)
                };
                return ctx.FromSqlRaw<DonVi>(sql.ToString(), parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindByListDonViCap2KhacCha(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_skt_get_don_vi_cap_2_khac_don_vi_cha @NamLamViec";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                };
                return ctx.FromSqlRaw<DonVi>(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }

        public IEnumerable<DonVi> FindDonViCoDataSktSoLieuChiTietAllLoai(int namLamViec, int namNganSach, int nguonNganSach, int loaiNNS, int loaiChungtu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_skt_get_donvi_solieu_chitiet_all_loai @YearOfWork, @YearOfBudget, @BudgetOfSource, @iLoaiNNS, @ILoaiChungTu";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@YearOfBudget", namNganSach),
                    new SqlParameter("@BudgetOfSource", nguonNganSach),
                    new SqlParameter("@iLoaiNNS", loaiNNS),
                    new SqlParameter("@ILoaiChungTu", loaiChungtu)
                };
                return ctx.Set<DonVi>().FromSql(sql, parameters).OrderBy(x => x.IIDMaDonVi).ToList();
            }
        }


        public IEnumerable<DonVi> FindAllDataDonViCurrent(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"select * from DonVi where inamlamviec ={namLamViec} and iCapDonVi = (select iCapDonVi from DonVi where iLoai = '0' and inamlamviec = {namLamViec})";
                return ctx.Set<DonVi>().FromSql(sql).ToList();
            }
        }


        public IEnumerable<string> FindAllDonViByBaoCaoThamDinhBH(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = $@"SELECT DISTINCT a.* FROM 
                                    (select iID_MaDonVi from BH_ThamDinhQuyetToan_ChungTuChiTiet where iMa IN (225,274,240) and iNamLamViec = {namLamViec}
                                    UNION ALL
                                    select iID_MaDonVi from BH_DTC_PhanBoDuToanChi_ChiTiet where (sXauNoiMa LIKE '9010001%' OR sXauNoiMa LIKE '9010002%' OR sXauNoiMa LIKE  '9010003%')  and         iNamLamViec =    {namLamViec}
                                    UNION ALL
                                    select iID_MaDonVi from BH_DTC_PhanBoDuToanChi_ChiTiet where (sXauNoiMa LIKE '9010004%' OR sXauNoiMa LIKE '9010006%' OR sXauNoiMa LIKE  '9010009%')  and         iNamLamViec =    {namLamViec}
                                    UNION ALL
                                    select iID_MaDonVi from BH_ThamDinhQuyetToan_ChungTuChiTiet where iMa IN (202,253,210,216,224,232,239) and iNamLamViec = {namLamViec - 1}
                                    UNION ALL
                                    select iID_MaDonVi from BH_CP_ChungTu_ChiTiet where sXauNoiMa IN ('901','9010003') and iNamLamViec = {namLamViec}
                                    UNION ALL
                                    SELECT iID_MaDonVi FROM BH_CP_ChungTu_ChiTiet WHERE sXauNoiMa IN ('9050001-010-011-0001','9050001-010-011-0002','9010004','9010006','9010009')  and         iNamLamViec =    {namLamViec}
                                    UNION ALL
                                    select iID_MaDonVi FROM BH_QTC_Nam_CheDoBHXH_ChiTiet where iNamLamViec = {namLamViec}
                                    UNION ALL
                                    select iID_MaDonVi FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet where iNamLamViec = {namLamViec}
                                    UNION ALL
                                    select iID_MaDonVi FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet where iNamLamViec = {namLamViec}
                                    ) a
                                    order by a.iID_MaDonVi";

                return ctx.FromSqlRaw<string>(executeSql, new SqlParameter()).ToList();
            }
        }
    }
}
