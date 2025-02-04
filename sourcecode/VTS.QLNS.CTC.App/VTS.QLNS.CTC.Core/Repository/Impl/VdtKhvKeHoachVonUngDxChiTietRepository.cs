using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using System.Data;
using Dapper;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Windows.Forms;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKhvKeHoachVonUngDxChiTietRepository : Repository<VdtKhvKeHoachVonUngDxChiTiet>, IVdtKhvKeHoachVonUngDxChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvKeHoachVonUngDxChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtKhvKeHoachVonUngDxChiTietQuery> GetDuAnInKeHoachVonUngDetail(string iIdDonVi, DateTime dNgayLap, string sTongHop)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                if (iIdDonVi == null) iIdDonVi = string.Empty;
                if (sTongHop == null) sTongHop = string.Empty;
                return ctx.FromSqlRaw<VdtKhvKeHoachVonUngDxChiTietQuery>("EXECUTE dbo.sp_vdt_get_duan_kehoachvonungdx_detail @sTongHop,@iIdDonViQuanLy, @dNgayLap",
                    new SqlParameter("@sTongHop", sTongHop),
                    new SqlParameter("@iIdDonViQuanLy", iIdDonVi),
                    new SqlParameter("@dNgayLap", dNgayLap)).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoachVonUngDxChiTietQuery> GetKeHoachVonUngChiTietByParentId(Guid iIdKeHoachVonUng)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvKeHoachVonUngDxChiTietQuery>("EXECUTE dbo.sp_vdt_get_kehoachvonungdxchitiet_detail @iIdKeHoachVonUng",
                    new SqlParameter("@iIdKeHoachVonUng", iIdKeHoachVonUng)).ToList();
            }
        }

        public void DeleteKeHoachVonUngChiTietByParentId(Guid iIdKeHoachVonUng)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstKeHoachUngChiTiet = ctx.VdtKhvKeHoachVonUngDxChiTiets.Where(n => n.IIdKeHoachUngId == iIdKeHoachVonUng).ToList();
                if (lstKeHoachUngChiTiet == null || lstKeHoachUngChiTiet.Count == 0) return;
                ctx.VdtKhvKeHoachVonUngDxChiTiets.RemoveRange(lstKeHoachUngChiTiet);
                ctx.SaveChanges();
            }
        }

        public void InsertKhVonUngDeXuatTongHop(Guid iIdKeHoachTongHop, List<Guid> lstIdChild)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_vdt_khv_kehoachvonung_dexuat_tonghop_insert @iIDKeHoachTongHop, @iIDs";
                DataTable dt = DBExtension.ConvertDataToGuidTable(lstIdChild);
                SqlParameter dtDetailParam = new SqlParameter("iIDs", SqlDbType.Structured);
                dtDetailParam.TypeName = "t_tbl_uniqueidentifier";
                dtDetailParam.Value = dt;
                var parameters = new[]
                {
                    new SqlParameter("@iIDKeHoachTongHop", iIdKeHoachTongHop),
                    dtDetailParam
                };
                ctx.Database.ExecuteSqlCommand(executeQuery, parameters);
            }
        }

        public IEnumerable<ExportVonUngDonViQuery> GetKeHoachVonUngDonViExport(List<YearPlanManagerExportCriteria> lstPhanboVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_export_kehoachvonung_donvi @Ids";
                DataTable dt = DBExtension.ConvertDataToTableDefined("t_tbl_uniqueidentifier", lstPhanboVon);
                var parameters = new[]
                {
                    new SqlParameter("@Ids", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                return ctx.FromSqlRaw<ExportVonUngDonViQuery>(executeQuery, parameters).ToList();
            }
        }

        public bool CheckExistSoKeHoach(string sSoQuyetDinh, int iNamLamViec, Guid? iId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {                
                var sql = "SELECT * FROM VDT_KHV_KeHoachVonUng_DX WHERE sSoDeNghi = @sSoQuyetDinh";
                return ctx.FromSqlRaw<VdtKhvKeHoachVonUngDxChiTietQuery>(sql, new SqlParameter("@sSoQuyetDinh", sSoQuyetDinh)).Count() == 0 ? false : true;
            }
        }
    }
}
