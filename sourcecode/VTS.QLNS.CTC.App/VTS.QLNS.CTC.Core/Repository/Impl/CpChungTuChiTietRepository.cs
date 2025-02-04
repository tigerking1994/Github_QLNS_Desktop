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

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class CpChungTuChiTietRepository : Repository<NsCpChungTuChiTiet>, ICpChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public CpChungTuChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<CpChungTuChiTietQuery> FindChungTuChiTietByCondition(AllocationDetailCriteria searchCondition, bool bQueryAll)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                if (searchCondition.Type == ((int)LoaiCapPhat.CAP_TREN).ToString())
                {
                    sql = "EXECUTE dbo.sp_cp_chungtu_chitiet_nhan @VoucherId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @VoucherDate, @UserName, @PhanCap, @IsCapPhatToanDonVi";
                }
                else if (bQueryAll)
                {
                    sql = "EXECUTE dbo.sp_cp_chungtu_chitiet_all @VoucherId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @VoucherDate, @UserName, @PhanCap, @IsCapPhatToanDonVi";
                }
                else
                {
                    sql = "EXECUTE dbo.sp_cp_chungtu_chitiet @VoucherId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @VoucherDate, @UserName, @PhanCap, @IsCapPhatToanDonVi";

                }
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", searchCondition.VoucherId),
                    new SqlParameter("@LNS", searchCondition.LNS),
                    new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                    new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                    new SqlParameter("@BudgetSource", searchCondition.BudgetSource),
                    new SqlParameter("@AgencyId", searchCondition.AgencyId),
                    new SqlParameter("@VoucherDate", searchCondition.VoucherDate),
                    new SqlParameter("@UserName", searchCondition.UserName),
                    new SqlParameter("@PhanCap", searchCondition.PhanCap),
                    new SqlParameter("@IsCapPhatToanDonVi", searchCondition.IsCapPhatToanDonVi)
                };
                return ctx.FromSqlRaw<CpChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<CpChungTuChiTietQuery> FindChungTuChiTietByConditionSummary(AllocationDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    string sql = "EXECUTE dbo.sp_cp_chungtu_chitiet_summary @VoucherId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @VoucherDate, @UserName";
                    var parameters = new[]
                    {
                        new SqlParameter("@VoucherId", searchCondition.VoucherId),
                        new SqlParameter("@LNS", searchCondition.LNS),
                        new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                        new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                        new SqlParameter("@BudgetSource", searchCondition.BudgetSource),
                        new SqlParameter("@AgencyId", searchCondition.AgencyId),
                        new SqlParameter("@VoucherDate", searchCondition.VoucherDate),
                        new SqlParameter("@UserName", searchCondition.UserName)
                    };
                    return ctx.FromSqlRaw<CpChungTuChiTietQuery>(sql, parameters).ToList();
                }
                catch (Exception ex)
                {
                    return new List<CpChungTuChiTietQuery>();
                }
            }
        }

        public bool CheckExitsByChungTuId(Guid chungtuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int count = ctx.NsCpChungTuChiTiets.Where(n => n.IIdCtcapPhat == chungtuId).ToList().Count;
                return count > 0;
            }
        }

        public IEnumerable<CpChungTuChiTietQuery> FindChungTuChiTietByConditionForExport(AllocationDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    string sql = "EXECUTE dbo.sp_cp_chungtu_chitiet_export @VoucherId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @VoucherDate";
                    var parameters = new[]
                    {
                        new SqlParameter("@VoucherId", searchCondition.VoucherId),
                        new SqlParameter("@LNS", searchCondition.LNS),
                        new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                        new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                        new SqlParameter("@BudgetSource", searchCondition.BudgetSource),
                        new SqlParameter("@AgencyId", searchCondition.AgencyId),
                        new SqlParameter("@VoucherDate", searchCondition.VoucherDate)
                    };
                    return ctx.FromSqlRaw<CpChungTuChiTietQuery>(sql, parameters).ToList();
                }
                catch (Exception ex)
                {
                    return new List<CpChungTuChiTietQuery>();
                }
            }
        }

        public IEnumerable<CpChungTuChiTietDuToanQuery> FindChungTuChiTietDuToanByCondition(AllocationDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    string sql = "EXECUTE dbo.sp_cp_chungtu_chitiet_dutoan @VoucherId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @VoucherDate, @AgencyIdChild";
                    var parameters = new[]
                    {
                        new SqlParameter("@VoucherId", searchCondition.VoucherId),
                        new SqlParameter("@LNS", searchCondition.LNS),
                        new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                        new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                        new SqlParameter("@BudgetSource", searchCondition.BudgetSource),
                        new SqlParameter("@AgencyId", searchCondition.AgencyId),
                        new SqlParameter("@VoucherDate", searchCondition.VoucherDate),
                         new SqlParameter("@AgencyIdChild", searchCondition.DonViTongHop)
                    };
                    return ctx.FromSqlRaw<CpChungTuChiTietDuToanQuery>(sql, parameters).ToList();
                }
                catch (Exception ex)
                {
                    return new List<CpChungTuChiTietDuToanQuery>();
                }
            }
        }

        public IEnumerable<CpChungTuChiTietDaCapQuery> FindChungTuChiTietDaCapByCondition(AllocationDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    string sql = "EXECUTE dbo.sp_cp_chungtu_chitiet_dacap @VoucherId, @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @VoucherDate";
                    var parameters = new[]
                    {
                        new SqlParameter("@VoucherId", searchCondition.VoucherId),
                        new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                        new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                        new SqlParameter("@BudgetSource", searchCondition.BudgetSource),
                        new SqlParameter("@AgencyId", searchCondition.AgencyId),
                        new SqlParameter("@VoucherDate", searchCondition.VoucherDate)
                    };
                    return ctx.FromSqlRaw<CpChungTuChiTietDaCapQuery>(sql, parameters).ToList();
                }
                catch (Exception ex)
                {
                    return new List<CpChungTuChiTietDaCapQuery>();
                }
            }
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"DELETE NS_CP_ChungTuChiTiet WHERE iID_CTCapPhat = @VoucherId";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId.ToString())
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void CreateVoudcherSummary(string idChungTu, string idDonVi, string nguoiTao, int namLamViec, int namNganSach, int nguonNganSach, string idChungTuSummary)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_cp_create_data_summary @IdChungTu, @AgencyId, @NguoiTao, @YearOfWork, @YearOfBudget, @BudgetSource, @IdChungTuSummary";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu", idChungTu),
                    new SqlParameter("@AgencyId", idDonVi),
                    new SqlParameter("@NguoiTao", nguoiTao),
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@YearOfBudget", namNganSach),
                    new SqlParameter("@BudgetSource", nguonNganSach),
                    new SqlParameter("@IdChungTuSummary", idChungTuSummary)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
