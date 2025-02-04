using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class CpChungTuRepository : Repository<NsCpChungTu>, ICpChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public CpChungTuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<CpChungTuQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, string userName, bool isCapPhatToanDonVi, int iLoai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", nguonNganSach);
                SqlParameter iLoaiParam = new SqlParameter("@ILoai", iLoai);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                string proc = string.Empty;
                if (isCapPhatToanDonVi)
                    return ctx.FromSqlRaw<CpChungTuQuery>("EXECUTE dbo.sp_cp_chungtu_tatca_donvi @YearOfWork, @YearOfBudget, @BudgetSource, @ILoai",
                        yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, iLoaiParam).ToList();
                else
                    return ctx.FromSqlRaw<CpChungTuQuery>("EXECUTE dbo.sp_cp_chungtu_theo_donvi @YearOfWork, @YearOfBudget, @BudgetSource, @UserName, @ILoai",
                        yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, userNameParam, iLoaiParam).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach, int namNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NsCpChungTu> result = ctx.NsCpChungTus.Where(n => n.INamLamViec == namLamViec && n.IIdMaNguonNganSach == nguonNganSach && n.INamNganSach == namNganSach).OrderByDescending(n => n.ISoChungTuIndex).ToList();
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
                NsCpChungTu entity = ctx.NsCpChungTus.Find(id);
                entity.BKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public void UpdateTotalCPChungTu(string voucherId, string userModify)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_cp_update_total_cp_chung_tu @VoucherId, @UserModify";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId),
                    new SqlParameter("@UserModify", userModify)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<ReportCapPhatThongTriQuery> GetDataReportCapPhatThongTri(int namLamViec, Guid capPhatId, string idDonvi, string phanCap, string lns, string userName, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_lns_rpt_thongtri_lns @NamLamViec, @CapPhatId, @IdDonVi, @PhanCap, @LNS, @UserName, @Dvt";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@CapPhatId", capPhatId),
                    new SqlParameter("@IdDonvi", idDonvi),
                    new SqlParameter("@PhanCap", phanCap),
                    new SqlParameter("@LNS", lns),
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@Dvt", dvt)
                };
                return ctx.FromSqlRaw<ReportCapPhatThongTriQuery>(executeSql, parameters).ToList();
            }  
        }

        public IEnumerable<ReportCapPhatThongTriDonViQuery> GetDataReportCapPhatThongTriDonVi(int namLamViec, Guid capPhatId, string idDonvi, int dvt, int loaiNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_lns_rpt_thongtri_donvi @NamLamViec, @CapPhatId, @DonViId, @Dvt, @LoaiNganSach";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@CapPhatId", capPhatId),
                    new SqlParameter("@DonViId", idDonvi),
                    new SqlParameter("@Dvt", dvt),
                    new SqlParameter("@LoaiNganSach", loaiNganSach),
                };
                return ctx.FromSqlRaw<ReportCapPhatThongTriDonViQuery>(executeSql, parameters).ToList();
            }
        }

        public void UpdateAggregateStatus(string voucherIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "UPDATE NS_CP_ChungTu SET bDaTongHop = 0 WHERE iID_CTCapPhat IN (SELECT * FROM f_split(@VoucherIds))";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherIds", voucherIds)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<NsCpChungTu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork, int yearOfBudget, int budgetSource)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsCpChungTus.Where(x => voucherNoes.Contains(x.SSoChungTu) && x.INamLamViec == yearOfWork
                                                   && x.INamNganSach == yearOfBudget && x.IIdMaNguonNganSach == budgetSource).ToList();
            }
        }

        public IEnumerable<T> GetDataReportLoaiCap<T>(int yearOfWork, int yearOfBudget, int budget, string donViIds, string capPhatIds, int dvt, string loaiBaoCao) where T : class
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string procName = string.Empty;
                switch (loaiBaoCao)
                {
                    case LoaiBaoCao.TONG_HOP_DON_VI:
                        procName = "dbo.sp_cp_rpt_loaicap_tong_donvi";
                        break;
                    case LoaiBaoCao.CHI_TIET_TUNG_DON_VI:
                    case LoaiBaoCao.TONG_HOP_DON_VI_LNS1:
                    case LoaiBaoCao.TONG_HOP_DON_VI_LNS3:
                    case LoaiBaoCao.TONG_HOP_DON_VI_LNS:
                        procName = "dbo.sp_cp_rpt_loaicap_lns";
                        break;
                }
                string executeSql = string.Format("EXECUTE {0} @NamLamViec, @NamNganSach, @NguonNganSach, @IdDonvi, @CapPhatIds, @dvt", procName);
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@NamNganSach", yearOfBudget),
                    new SqlParameter("@NguonNganSach", budget),
                    new SqlParameter("@IdDonvi", donViIds),
                    new SqlParameter("@CapPhatIds", capPhatIds),
                    new SqlParameter("@Dvt", dvt)
                };
                return ctx.FromSqlRaw<T>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<T> GetDataReportSoSanh<T>(int yearOfWork, int yearOfBudget, int budget, string donViIds, string capPhatIds, DateTime ngayChungTu, string phanCap, string lns, string userName, int dvt, string loaiBaoCao) where T : class
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_rpt_sosanh_lns @NamLamViec, @NamNganSach, @NguonNganSach, @IdDonvi, @CapPhatIds, @NgayChungTu, @PhanCap, @LNS, @UserName, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@NamNganSach", yearOfBudget),
                    new SqlParameter("@NguonNganSach", budget),
                    new SqlParameter("@IdDonvi", donViIds),
                    new SqlParameter("@CapPhatIds", capPhatIds),
                    new SqlParameter("@NgayChungTu", ngayChungTu),
                    new SqlParameter("@PhanCap", phanCap),
                    new SqlParameter("@LNS", lns),
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@Dvt", dvt)
                };
                return ctx.FromSqlRaw<T>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<ReportCapPhatDonViQuery> GetDataReportCapPhatDonVi(int NamLamViec, int NamNganSach, int NguonNganSach, string IdDonvi, string ctID,  DateTime NgayChungTu, string UserName, int Dvt) 
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_rpt_donvi @NamLamViec, @NamNganSach, @NguonNganSach, @IdDonvi, @ctID, @NgayChungTu, @UserName, @Dvt";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", NamLamViec),
                    new SqlParameter("@NamNganSach", NamNganSach),
                    new SqlParameter("@NguonNganSach", NguonNganSach),
                    new SqlParameter("@IdDonvi", IdDonvi),
                    new SqlParameter("@ctID", ctID),
                    new SqlParameter("@NgayChungTu", NgayChungTu),
                    new SqlParameter("@UserName", UserName),
                    new SqlParameter("@Dvt", Dvt),
                };
                return ctx.FromSqlRaw<ReportCapPhatDonViQuery>(executeSql, parameters).ToList();
            }
        }
    }

  
}
