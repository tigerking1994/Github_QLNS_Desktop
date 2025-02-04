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
    public class VdtTtDeNghiThanhToanUngChiTietRepository : Repository<VdtTtDeNghiThanhToanUngChiTiet>, IVdtTtDeNghiThanhToanUngChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtTtDeNghiThanhToanUngChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtTtDeNghiThanhToanUngChiTietQuery> GetDuAnByDeNghiThanhToanUng(string iIdDonVi, DateTime dNgayLap)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtTtDeNghiThanhToanUngChiTietQuery>("EXECUTE dbo.sp_vdt_get_duan_by_pheduyetungscreen @iIdDonViQuanLy, @dngayPheDuyet",
                    new SqlParameter("@iIdDonViQuanLy", iIdDonVi),
                    new SqlParameter("@dngayPheDuyet", dNgayLap)).ToList();
            }
        }

        public IEnumerable<VdtTtDeNghiThanhToanUngChiTietQuery> GetDuAnByIdThanhToan(Guid iIdParent, string iIdDonViQuanLyId, DateTime dNgayDeNghi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtTtDeNghiThanhToanUngChiTietQuery>("EXECUTE dbo.sp_vdt_get_duan_by_pheduyetungscreen_by_thanhtoanid @iIdThanhToanId, @iIdDonViQuanLy, @dngayPheDuyet",
                    new SqlParameter("@iIdThanhToanId", iIdParent),
                    new SqlParameter("@iIdDonViQuanLy", iIdDonViQuanLyId),
                    new SqlParameter("@dngayPheDuyet", dNgayDeNghi)).ToList();
            }
        }

        public bool Insert(List<VdtTtDeNghiThanhToanUngChiTiet> data)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtTtDeNghiThanhToanUngChiTiets.AddRange(data);
                return ctx.SaveChanges() != 0;
            }
        }

        public bool DeleteByThanhToanId(Guid iIdThanhToan)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                List<VdtTtDeNghiThanhToanUngChiTiet> lstDataDelete = ctx.VdtTtDeNghiThanhToanUngChiTiets.Where(n => n.IIdDeNghiThanhToanId == iIdThanhToan).ToList();
                if (lstDataDelete == null || lstDataDelete.Count == 0) return true;
                ctx.VdtTtDeNghiThanhToanUngChiTiets.RemoveRange(lstDataDelete);
                return ctx.SaveChanges() != 0;
            }
        }

        public VdtTtDeNghiThanhToanUngChiTietQuery GetLuyKeThanhToan(Guid iIdDuAn, Guid? iIdHopDong, string sMaDonViQuanLy, DateTime dNgayPheDuyet)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtTtDeNghiThanhToanUngChiTietQuery>("EXECUTE dbo.sp_vdt_get_luyke_denghithanhtoanung @iIdDonViQuanLy, @dngayPheDuyet, @iIdDuAn, @iIDHopDongID",
                    new SqlParameter("@iIdDonViQuanLy", sMaDonViQuanLy),
                    new SqlParameter("@dngayPheDuyet", dNgayPheDuyet),
                    new SqlParameter("@iIdDuAn", iIdDuAn),
                    new SqlParameter("@iIDHopDongID", iIdHopDong)).ToList().FirstOrDefault();
            }
        }
    }
}
