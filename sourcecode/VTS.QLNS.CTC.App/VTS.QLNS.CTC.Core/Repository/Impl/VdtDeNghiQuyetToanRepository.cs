using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDeNghiQuyetToanRepository : Repository<VdtQtDeNghiQuyetToan>, IVdtDeNghiQuyetToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDeNghiQuyetToanRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public VdtQtDeNghiQuyetToan FindByDuAnId(Guid duAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtDeNghiQuyetToans.Where(n => n.IIdDuAnId.HasValue && n.IIdDuAnId == duAnId).FirstOrDefault();
            }
        }
        public IEnumerable<VdtQtDeNghiQuyetToan> FindLstDeNghiQTByDuAnId(Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter("@IdDuAn", duAnId);
                return ctx.FromSqlRaw<VdtQtDeNghiQuyetToan>("EXECUTE dbo.sp_vdt_get_denghiqt_by_idduan @IdDuAn", voucherIdParam);
            }
        }

        public void UpdateTotal(string voucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter("@VoucherId", voucherId);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_update_total_de_nghi_quyet_toan @VoucherId", voucherIdParam);
            }
        }

        public List<ReportQuyetToanHoanThanhQuery> GetDataReportQuyetToanHoanThanh(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                    return ctx.FromSqlRaw<ReportQuyetToanHoanThanhQuery>("EXECUTE dbo.sp_vdt_get_data_report_quyettoan_hoanthanh @NamLamViec", namLamViecParam).ToList();
                }
                catch (Exception ex)
                {
                    return new List<ReportQuyetToanHoanThanhQuery>();
                }
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                VdtQtDeNghiQuyetToan entity = ctx.VdtQtDeNghiQuyetToans.Find(id);
                entity.BKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public void TongHopDeNghiQuyetToan(VdtQtDeNghiQuyetToan vdtTtDeNghiQT, List<Guid> voucherAgregatesIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var exist = ctx.VdtQtDeNghiQuyetToans.Any(t => t.Id.Equals(vdtTtDeNghiQT.Id));
                if (exist)
                    ctx.Update(vdtTtDeNghiQT);
                else
                    ctx.Add(vdtTtDeNghiQT);
                var children = ctx.VdtQtDeNghiQuyetToans.Where(t => voucherAgregatesIds.Contains(t.Id)).ToList();
                children.ForEach(t => t.ParentId = vdtTtDeNghiQT.Id);
                ctx.SaveChanges();
            }
        }

        public List<VdtQtDeNghiQuyetToan> FindDeNghiTongHop()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtDeNghiQuyetToans.Where(t => t.BTongHop.HasValue && t.BTongHop.Value).ToList();
            }
        }
    }
}
