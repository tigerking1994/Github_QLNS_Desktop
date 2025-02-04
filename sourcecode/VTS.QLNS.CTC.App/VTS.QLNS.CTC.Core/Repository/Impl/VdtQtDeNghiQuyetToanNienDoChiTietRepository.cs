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
    public class VdtQtDeNghiQuyetToanNienDoChiTietRepository : Repository<VdtQtDeNghiQuyetToanNienDoChiTiet>, IVdtQtDeNghiQuyetToanNienDoChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQtDeNghiQuyetToanNienDoChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<VdtQtDenghiQuyetToanNienDoChiTietQuery> GetAllDuAnByQuyetToanNienDo(string iIdDonvi, int iNguonVon, Guid iIdLoaiNguonVon, DateTime dNgayLap, int iNamKeHoach)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtQtDenghiQuyetToanNienDoChiTietQuery>("EXECUTE dbo.sp_vdt_denghiquyettoanscreen_detail @nguonVonId, @loaiNguonVonID, @donViQuanLyId, @ngayLap, @year",
                    new SqlParameter("@nguonVonId", iNguonVon),
                    new SqlParameter("@loaiNguonVonID", iIdLoaiNguonVon),
                    new SqlParameter("@donViQuanLyId", iIdDonvi),
                    new SqlParameter("@ngayLap", dNgayLap),
                    new SqlParameter("@year", iNamKeHoach)).ToList();
            }

        }

        public List<VdtQtDenghiQuyetToanNienDoChiTietQuery> GetQuyetToanNienDoChiTietByParentid(Guid iIdQuyetToanNienDoId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtQtDenghiQuyetToanNienDoChiTietQuery>("EXECUTE dbo.sp_vdt_denghiquyettoanniendoscreen_detail_by_parentid @iIdQuyetToanNienDo",
                    new SqlParameter("@iIdQuyetToanNienDo", iIdQuyetToanNienDoId)).ToList();
            }
        }

        public bool DeleteByQuyetToanNienDoId(Guid iIdQuyetToanNienDo)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                List<VdtQtDeNghiQuyetToanNienDoChiTiet> lstDataDelete = ctx.VdtQtDeNghiQuyetToanNienDoChiTiets.Where(n => n.IIdDeNghiQuyetToanNienDoId == iIdQuyetToanNienDo).ToList();
                if (lstDataDelete == null || lstDataDelete.Count == 0) return true;
                ctx.VdtQtDeNghiQuyetToanNienDoChiTiets.RemoveRange(lstDataDelete);
                return ctx.SaveChanges() != 0;
            }
        }
    }
}
