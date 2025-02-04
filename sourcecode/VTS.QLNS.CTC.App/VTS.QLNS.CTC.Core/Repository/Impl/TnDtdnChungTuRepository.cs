using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TnDtdnChungTuRepository : Repository<TnDtdnChungTu>, ITnDtdnChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TnDtdnChungTuRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;

        }

        public void CreateDataReportTotalSummary(string id, int namLamViec, int namNganSach, int nguonNganSach, string idDonVi, string listChungTuTongHop, string nguoiTao)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuId = new SqlParameter("@chungTuId", id);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", nguonNganSach);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", idDonVi);
                SqlParameter listChungTuTongHopParam = new SqlParameter("@ChungTuTongHop", listChungTuTongHop);
                SqlParameter nguoiTaoParam = new SqlParameter("@NguoiTao", nguoiTao);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_tn_dtdn_chungtuchitiet_create_data_summary @chungTuId, @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @ChungTuTongHop, @NguoiTao",
                    chungTuId, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, agencyIdParam, listChungTuTongHopParam, nguoiTaoParam);
            }
        }

        public int DeleteItem(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                TnDtdnChungTu entity = ctx.TnDtdnChungTus.Find(id);
                List<TnDtdnChungTuChiTiet> itemDetail = ctx.TnDtdnChungTuChiTiets.Where(x => x.IdChungTu.HasValue && x.IdChungTu.Equals(id)).ToList();
                if (!string.IsNullOrEmpty(entity.SDSSoChungTuTongHop))
                {
                    var items = ctx.TnDtdnChungTus.Where(x => entity.SDSSoChungTuTongHop.Split(',').Contains(x.SSoChungTu)).ToList();
                    if (!items.IsEmpty())
                    {
                        items.ForEach(x =>
                        {
                            x.BDaTongHop = false;
                        });
                        ctx.UpdateRange(items);
                    }
                }
                ctx.RemoveRange(itemDetail);
                ctx.Remove(entity);
                return ctx.SaveChanges();
            }
        }

        public TnDtdnChungTu FindAggregateVoucher(string voucherNoes)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnDtdnChungTus.Where(x => x.SDSSoChungTuTongHop == voucherNoes).FirstOrDefault();
            }
        }

        public int FindNextSoChungTuIndex(Expression<Func<TnDtdnChungTu, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var currentIndex = ctx.TnDtdnChungTus
                 .Where(predicate)
                 .Max(x => x.ISoChungTuIndex);
                return currentIndex != null ? (int)currentIndex + 1 : 1;
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                TnDtdnChungTu entity = ctx.TnDtdnChungTus.Find(id);
                entity.BKhoa = lockStatus;
                return Update(entity);
            }
        }

        public List<string> GetAgencyCodeByVoucherDetail(Expression<Func<TnDtdnChungTu, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var data = from chungtu in ctx.TnDtdnChungTus.Where(predicate)
                           join ct in ctx.TnDtdnChungTuChiTiets on chungtu.Id equals ct.IdChungTu
                           select chungtu.IIdMaDonVi;
                return data.ToList();
            }
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnDtdnChungTuChiTiets.Where(x => x.IdChungTu.HasValue && chungTuIds.Contains(x.IdChungTu.Value) && (x.FDuToanNamKeHoach != 0 || x.FDuToanNamNay != 0 || x.FThucThuNamTruoc != 0 || x.FUocThucHienNamNay != 0)).Select(x => x.Lns).Distinct().ToList();
            }
        }

        public IEnumerable<BaoCaoNhanVaQuyetToanKinhPhi> GetBaoCaoNhanVaQuyetToanKinhPhis(string sMaDonVi, int iNamLamViec, int iDonViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@MaDonVi ", sMaDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@DonviTinh", iDonViTinh);

                return ctx.FromSqlRaw<BaoCaoNhanVaQuyetToanKinhPhi>("EXECUTE sp_rptBH_QT_TinhHinhNhanVaQuyetToan @MaDonVi, @NamLamViec,@DonViTinh",
                    sIdDonViParam, iNamLamViecParam, donViTinhParam).ToList();
            }
        }
    }
}
