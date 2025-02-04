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
    public class VdtKhvKeHoachVonUngChiTietRepository : Repository<VdtKhvKeHoachVonUngChiTiet>, IVdtKhvKeHoachVonUngChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvKeHoachVonUngChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtKhvKeHoachVonUngChiTietQuery> GetDuAnInKeHoachVonUngDetail(Guid? iIdKhvuDxId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_vdt_get_duan_kehoachvonung_detail @iIdKeHoachVonUngDeXuat";
                var parameters = new[]{
                    new SqlParameter("@iIdKeHoachVonUngDeXuat", iIdKhvuDxId)
                };
                return ctx.FromSqlRaw<VdtKhvKeHoachVonUngChiTietQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoachVonUngChiTietQuery> GetKeHoachVonUngChiTietByParentId(Guid iIdKeHoachVonUng)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_get_kehoachvonungchitiet_detail @iIdKeHoachVonUng";
                var parameters = new[]
                {
                    new SqlParameter("@iIdKeHoachVonUng", iIdKeHoachVonUng)
                };
                return ctx.FromSqlRaw<VdtKhvKeHoachVonUngChiTietQuery>(sql, parameters).ToList();
            }
        }

        public void DeleteKeHoachVonUngChiTietByParentId(Guid iIdKeHoachVonUng)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstKeHoachUngChiTiet = ctx.VdtKhvKeHoachVonUngChiTiets.Where(n => n.IIdKeHoachUngId == iIdKeHoachVonUng).ToList();
                if (lstKeHoachUngChiTiet == null || lstKeHoachUngChiTiet.Count == 0) return;
                ctx.VdtKhvKeHoachVonUngChiTiets.RemoveRange(lstKeHoachUngChiTiet);
                ctx.SaveChanges();
            }
        }

        public double GetkeHoachUng(Guid iIdDuAnId, DateTime dNgayBaoCao)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoachVonUngs.Where(n => n.DNgayQuyetDinh.HasValue && n.DNgayQuyetDinh.Value <= dNgayBaoCao)
                    .Join(ctx.VdtKhvKeHoachVonUngChiTiets.Where(n => n.IIdDuAnId == iIdDuAnId), tbl => tbl.Id, dt => dt.IIdKeHoachUngId, (tbl, dt) => new { fTongUng = (dt.FCapPhatBangLenhChi ?? 0) + (dt.FCapPhatTaiKhoBac ?? 0) })
                    .Sum(n=>n.fTongUng);
                        
            }
        }
    }
}
