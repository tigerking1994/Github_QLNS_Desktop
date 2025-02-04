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
    public class VdtTtDeNghiThanhToanChiTietRepository : Repository<VdtTtDeNghiThanhToanChiTiet>, IVdtTtDeNghiThanhToanChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtTtDeNghiThanhToanChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtTtDeNghiThanhToanChiTietQuery> GetDuAnByIdThanhToan(Guid iIdThanhToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtTtDeNghiThanhToanChiTietQuery>("EXECUTE dbo.sp_vdt_get_duan_by_thanhtoanscreen_by_thanhtoanid @iIdThanhToanId",
                new SqlParameter("@iIdThanhToanId", iIdThanhToanId)).ToList();
            }
        }

        public HopDongInfoQuery GetHopDongInfo(Guid iIdHopDong, DateTime dNgayPheDuyet, int iIdNguonVonId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var data = ctx.FromSqlRaw<HopDongInfoQuery>("EXECUTE dbo.sp_vdt_get_hopdonginfo @iIdHopDong, @dNgayPheDuyet, @iIdNguonVonId",
                    new SqlParameter("@iIdHopDong", iIdHopDong),
                    new SqlParameter("@dNgayPheDuyet", dNgayPheDuyet),
                    new SqlParameter("@iIdNguonVonId", iIdNguonVonId)).ToList();
                if (data == null || data.Count() == 0) return new HopDongInfoQuery();
                return data.FirstOrDefault();
            }
        }

        public bool Insert(List<VdtTtDeNghiThanhToanChiTiet> data)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtTtDeNghiThanhToanChiTiets.AddRange(data);
                int result = ctx.SaveChanges();
                return result != 0;
            }
        }

        public bool DeleteByThanhToanId(Guid iIdThanhToan)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                List<VdtTtDeNghiThanhToanChiTiet> lstDataDelete = ctx.VdtTtDeNghiThanhToanChiTiets.Where(n => n.IIdDeNghiThanhToanId == iIdThanhToan).ToList();
                if (lstDataDelete == null || lstDataDelete.Count == 0) return true;
                ctx.VdtTtDeNghiThanhToanChiTiets.RemoveRange(lstDataDelete);
                return ctx.SaveChanges() != 0;
            }
        }
    }
}
