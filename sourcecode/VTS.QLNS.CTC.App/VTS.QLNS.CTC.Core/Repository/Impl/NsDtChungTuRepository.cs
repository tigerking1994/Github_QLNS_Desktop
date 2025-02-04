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
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsDtChungTuRepository : Repository<NsDtChungTu>, INsDtChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsDtChungTuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public override IEnumerable<NsDtChungTu> FindAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTus.OrderByDescending(x => x.SSoChungTu).ToList();
            }
        }

        public override IEnumerable<NsDtChungTu> FindAll(Expression<Func<NsDtChungTu, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTus.Include(e => e.ChungTuChiTiets).Where(predicate).OrderByDescending(x => x.SSoChungTu).ToList();
            }
        }

        public IEnumerable<NsDtChungTu> FindByYearOfWorker(int yearOfWorker, string soChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTus.Where(n => n.INamLamViec == yearOfWorker &&
                                                  n.SSoChungTu.Contains(soChungTu)).ToList();
            }
        }

        public NsDtChungTu FindBySoChungTu(string soChungTu, int yearOfWork, int yearOfBudget, int budgetSource)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTus.Where(n => n.SSoChungTu == soChungTu && n.INamLamViec == yearOfWork && n.INamNganSach == yearOfBudget && n.IIdMaNguonNganSach == budgetSource).FirstOrDefault();
            }
        }

        public List<NsDtChungTu> FindBySoQuyetDinh(string soQuyetDinh, int yearOfWork, int yearOfBudget, int budgetSource)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTus.Where(n => n.SSoQuyetDinh == soQuyetDinh && n.INamLamViec == yearOfWork && n.INamNganSach == yearOfBudget && n.IIdMaNguonNganSach == budgetSource).ToList();
            }
        }

        public IEnumerable<NsDtChungTu> FindByYearOfWorker(int yearOfWorker)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTus.Where(n => n.INamLamViec == yearOfWorker).ToList();
            }
        }

        public IEnumerable<NsDtChungTu> FindCond(int yearOfWorker, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTus.Where(n => n.INamLamViec == yearOfWorker && n.SDsidMaDonVi == idDonVi).ToList();
            }
        }

        public int FindNextSoChungTuIndex(Expression<Func<NsDtChungTu, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var currentIndex = ctx.NsDtChungTus.Where(predicate).Max(x => x.ISoChungTuIndex);
                return currentIndex != null ? (int)currentIndex + 1 : 1;
            }

        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                NsDtChungTu entity = ctx.NsDtChungTus.Find(id);
                if (entity != null) entity.BKhoa = lockStatus;
                return Update(entity);
            }
        }

        public Dictionary<string, string> FindAllDict()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTus.ToDictionary(x => x.Id.ToString(), x => x.SSoChungTu);
            }
        }

        public IEnumerable<NsDtChungTu> FindByCondition(EstimationVoucherCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter typeParam = new SqlParameter("@Type", condition.EstimationType);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", condition.YearOfBudget);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", condition.YearOfWork);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", condition.BudgetSource);
                SqlParameter statusParam = new SqlParameter("@Status", condition.Status);
                SqlParameter userNameParam = new SqlParameter("@UserName", condition.UserName);
                SqlParameter voucherTypeParam = new SqlParameter("@VoucherType", condition.VoucherType);
                return ctx.NsDtChungTus.FromSql("EXECUTE dbo.sp_dt_chungtu_danhsach @Type, @YearOfBudget, @YearOfWork, @BudgetSource, @Status, @UserName, @VoucherType",
                    typeParam, yearOfBudgetParam, yearOfWorkParam, budgetSourceParam, statusParam, userNameParam, voucherTypeParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTu> FindByConditionInLuongView(EstimationVoucherCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", condition.YearOfBudget);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", condition.YearOfWork);
                return ctx.NsDtChungTus.FromSql("EXECUTE dbo.sp_luong_dt_chungtu_danhsach @YearOfBudget, @YearOfWork",
                    yearOfBudgetParam, yearOfWorkParam).ToList();
            }
        }

        public void LockOrUnlockMultiple(List<NsDtChungTu> chungTus, bool isLock)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherIds", string.Join(",", chungTus.Select(x => x.Id)));
                var lockParam = new SqlParameter("@LockParam", isLock ? 1 : 0);
                ctx.Database.ExecuteSqlCommand($"UPDATE NS_DT_ChungTu SET bKhoa = @LockParam WHERE iID_DTChungTu IN (SELECT * FROM f_split(@VoucherIds))", lockParam, voucherIdParam);
            }
        }

        public IEnumerable<string> FindSoQuyetDinh(int yearOfWork, int yearOfBudget, int budgetSource)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTus.Where(x => x.INamLamViec == yearOfWork && x.INamNganSach == yearOfBudget && x.IIdMaNguonNganSach == budgetSource && x.ILoai == SoChungTuType.ReceiveEstimate && !string.IsNullOrEmpty(x.SSoQuyetDinh))
                                       .Select(x => x.SSoQuyetDinh).ToList();
            }
        }

        public IEnumerable<NsDtChungTu> FindDotNhanByChungTuPhanBo(Guid idPhanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idPhanBoParam = new SqlParameter("@IdPhanBo", idPhanBo.ToString());
                return ctx.NsDtChungTus.FromSql("EXECUTE sp_dt_danhsach_dotnhan @IdPhanBo", idPhanBoParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuDotNhanQuery> FindChungTuDotNhan(EstimationVoucherCriteria condition, bool isCreate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", condition.YearOfWork);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", condition.YearOfBudget);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", condition.BudgetSource);
                if (isCreate)
                {
                    SqlParameter typeParam = new SqlParameter("@Type", condition.EstimationType);
                    SqlParameter voucherTypeParam = new SqlParameter("@VoucherType", condition.VoucherType);
                    SqlParameter dateParam = new SqlParameter("@Date", condition.Date);
                    return ctx.FromSqlRaw<NsDtChungTuDotNhanQuery>("EXECUTE dbo.sp_dt_dutoan_dotnhan_phanbo @YearOfWork, @YearOfBudget, @BudgetSource, @Type, @VoucherType, @Date",
                        yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, typeParam, voucherTypeParam, dateParam).ToList();
                }
                else
                {
                    SqlParameter idNhanPhanBosParam = new SqlParameter("@IdNhanPhanBos", condition.IdNhanPhanBos);
                    return ctx.FromSqlRaw<NsDtChungTuDotNhanQuery>("EXECUTE dbo.sp_dt_dutoan_dotnhan_cua_chungtu_phanbo @YearOfWork, @YearOfBudget, @BudgetSource, @IdNhanPhanBos",
                        yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, idNhanPhanBosParam).ToList();
                }
            }
        }

        public IEnumerable<NsDtChungTuQuery> FindHospitalByCondition(EstimationVoucherCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dt_chungtu_benhvien_tuchu @Type, @YearOfBudget, @YearOfWork, @BudgetSource, @Status, @UserName, @VoucherType";
                var parameters = new[]
                {
                    new SqlParameter("@Type", condition.EstimationType),
                    new SqlParameter("@YearOfBudget", condition.YearOfBudget),
                    new SqlParameter("@YearOfWork", condition.YearOfWork),
                    new SqlParameter("@BudgetSource", condition.BudgetSource),
                    new SqlParameter("@Status", condition.Status),
                    new SqlParameter("@UserName", condition.UserName),
                    new SqlParameter("@VoucherType", condition.VoucherType),
                };
                return ctx.FromSqlRaw<NsDtChungTuQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDtChungTuDotNhanQuery> FindAllChungTuDotNhan(EstimationVoucherCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", condition.YearOfWork);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", condition.YearOfBudget);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", condition.BudgetSource);
                SqlParameter typeParam = new SqlParameter("@Type", condition.EstimationType);
                SqlParameter voucherTypeParam = new SqlParameter("@VoucherType", condition.VoucherType);
                SqlParameter dateParam = new SqlParameter("@Date", condition.Date);
                SqlParameter indexParam = new SqlParameter("@Index", condition.SoChungTuIndex);
                return ctx.FromSqlRaw<NsDtChungTuDotNhanQuery>("EXECUTE dbo.sp_dt_dutoan_dotnhan_phanbo_find_all @YearOfWork, @YearOfBudget, @BudgetSource, @Type, @VoucherType, @Date, @Index",
                    yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, typeParam, voucherTypeParam, dateParam, indexParam).ToList();

            }
        }

        public bool CheckByIdAdjVoucher(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<string> lstID = new List<string>();
                var chungTuDT = ctx.NsDtChungTus.Where(n => !string.IsNullOrEmpty(n.IIDChungTuDieuChinh));
                foreach (var ct in chungTuDT)
                {
                    var sID = ct.IIDChungTuDieuChinh.Split(',').ToList();
                    lstID.AddRange(sID);
                }
                if (lstID.Any() && lstID.Contains(id.ToString()))
                    return true;
                else
                    return false;
            }
        }
    }
}