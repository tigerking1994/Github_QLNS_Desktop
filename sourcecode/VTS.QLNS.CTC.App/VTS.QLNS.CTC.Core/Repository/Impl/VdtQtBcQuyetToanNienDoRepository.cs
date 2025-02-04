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
    public class VdtQtBcQuyetToanNienDoRepository : Repository<VdtQtBcQuyetToanNienDo>, IVdtQtBcQuyetToanNienDoRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQtBcQuyetToanNienDoRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public VdtQtBcQuyetToanNienDo FindById(Guid iId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtBcquyetToanNienDos.Find(iId);
            }
        }

        public List<VdtQtBcQuyetToanNienDoQuery> GetDeNghiQuyetToanNienDoIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtQtBcQuyetToanNienDoQuery>("EXECUTE dbo.sp_qt_bcquyettoanniendo_index").ToList();
            }
        }

        public List<VdtQtBcQuyetToanNienDo> GetDeNghiQuyetToanNienDoByCondition(int iLoaiThanhToan, string sMaDonVi, int iNguonVon, int iNamKeHoach)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstData = ctx.VdtQtBcquyetToanNienDos.Where(n => n.ILoaiThanhToan == iLoaiThanhToan && n.IIdMaDonViQuanLy == sMaDonVi && n.IIdNguonVonId == iNguonVon && n.INamKeHoach == iNamKeHoach).ToList();
                if (lstData != null && lstData.Any()) return lstData.ToList();
                return new List<VdtQtBcQuyetToanNienDo>();
            }
        }

        public List<VdtQtBcquyetToanNienDoChiTiet1Query> GetDeNghiQuyetToanNienDoDetail(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtQtBcquyetToanNienDoChiTiet1Query>("EXECUTE dbo.sp_vdt_get_baocaodquyettoanniendo1 @iIdMaDonVi, @iNamKeHoach, @iIdNguonVon",
                    new SqlParameter("iIdMaDonVi", iIdMaDonVi),
                    new SqlParameter("iNamKeHoach", iNamKeHoach),
                    new SqlParameter("iIdNguonVon", iIdNguonVon)).ToList();
            }
        }

        public List<BcquyetToanNienDoVonUngChiTietQuery> GetDeNghiQuyetToanNienDoVonUngDetail(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<BcquyetToanNienDoVonUngChiTietQuery>("EXECUTE dbo.sp_vdt_get_baocaodquyettoanniendo_vonung @iIdMaDonVi, @iNamKeHoach, @iIdNguonVon",
                    new SqlParameter("iIdMaDonVi", iIdMaDonVi),
                    new SqlParameter("iNamKeHoach", iNamKeHoach),
                    new SqlParameter("iIdNguonVon", iIdNguonVon)).ToList();
            }
        }

        public bool CheckExistDeNghiQuyetToanNienDo(string iIdMaDonVi, int iNamKeHoach, int iNguonVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtDeNghiQuyetToanNienDos.Any(n => !n.DDateDelete.HasValue && n.IIDMaDonViDeNghi == iIdMaDonVi && n.INamKeHoach == iNamKeHoach && n.IIdNguonVonId == iNguonVon);
            }
        }

        public List<BcquyetToanNienDoVonUngChiTietQuery> GetQuyetToanNienDoVonUngByParentId(Guid iIdParentId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<BcquyetToanNienDoVonUngChiTietQuery>("EXECUTE dbo.sp_vdt_get_quyettoanniendovonung_by_parentid @iIdQuyetToanId",
                    new SqlParameter("iIdQuyetToanId", iIdParentId)).ToList();
            }
        }

        public List<VdtQtBcquyetToanNienDoChiTiet1Query> GetQuyetToanNienDoVonNamByParentId(Guid iIdParentId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtQtBcquyetToanNienDoChiTiet1Query>("EXECUTE dbo.sp_vdt_get_quyettoanniendovonnam_by_parentid @iIdQuyetToanId",
                    new SqlParameter("iIdQuyetToanId", iIdParentId)).ToList();
            }
        }

        public List<TongHopNguonNSDauTuQuery> GetLuyKeQuyetToanNamTruoc(int iLoaiQuyetToan, string iIdMaDonViQuanLy, int iNamKeHoach, int iIdNguonVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<TongHopNguonNSDauTuQuery>("EXECUTE dbo.sp_qttt_get_luykekehoachvon @iIdMaDonViQuanLy, @iNamKeHoach, @iIdNguonVon, @iLoaiQuyetToan",
                    new SqlParameter("iIdMaDonViQuanLy", iIdMaDonViQuanLy),
                    new SqlParameter("iNamKeHoach", iNamKeHoach),
                    new SqlParameter("iIdNguonVon", iIdNguonVon),
                    new SqlParameter("iLoaiQuyetToan", iLoaiQuyetToan)).ToList();
            }
        }

        public List<VdtQtBcQuyetToanNienDo> GetBcQuyetToanInThongTriScreen(Guid? iIdThongTri, string iIdMaDonVi, int iNamThongTri, int iIdNguonVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtQtBcQuyetToanNienDo>("EXECUTE dbo.sp_vdt_qt_get_quyettoanniendo_in_thongtriscreen @iIdThongTri, @iIdMaDonVi, @iNamThongTri, @iIdNguonVon",
                    new SqlParameter("iIdThongTri", iIdThongTri),
                    new SqlParameter("iIdMaDonVi", iIdMaDonVi),
                    new SqlParameter("iNamThongTri", iNamThongTri),
                    new SqlParameter("iIdNguonVon", iIdNguonVon)).ToList();
            }
        }
    }
}
