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
    public class VdtThongTriChiTietRepository : Repository<VdtThongTriChiTiet>, IVdtThongTriChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtThongTriChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriChiTiet(Guid iIdThongTri, string sMaDonVi, int iLoaiThongTri, int iNamKeHoach, DateTime dNgayThongTri,
            string sMaNguonVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"EXECUTE dbo.sp_vdt_thongtri_detail @iIdThongTriId, @sMaDonViQuanLy, @iLoaiThongTri, @iNamkeHoach, @dNgayThongTri, @sMaNguonVon";
                var parameters = new[]
                {
                    new SqlParameter("@iIdThongTriId",iIdThongTri),
                    new SqlParameter("@sMaDonViQuanLy", sMaDonVi),
                    new SqlParameter("@iLoaiThongTri", iLoaiThongTri),
                    new SqlParameter("@iNamkeHoach", iNamKeHoach),
                    new SqlParameter("@dNgayThongTri", dNgayThongTri),
                    new SqlParameter("@sMaNguonVon", sMaNguonVon)
                };
                return ctx.FromSqlRaw<VdtThongTriChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriQuyetToanChiTiet(string sMaDonVi, int iNamKeHoach, DateTime dNgayThongTri,
            string sMaNguonVon, DateTime? dNgayLapGanNhat, string sMaLoaiCongTrinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"EXECUTE dbo.sp_vdt_thongtri_quyettoan_detail @sMaDonViQuanLy, @iNamkeHoach, @dNgayThongTri, @sMaNguonVon, @dNgayLapGanNhat, @sMaLoaiCongTrinh";
                var parameters = new[]
                {
                    new SqlParameter("@sMaDonViQuanLy", sMaDonVi),
                    new SqlParameter("@iNamkeHoach", iNamKeHoach),
                    new SqlParameter("@dNgayThongTri", dNgayThongTri),
                    new SqlParameter("@sMaNguonVon", sMaNguonVon),
                    new SqlParameter("@dNgayLapGanNhat", dNgayLapGanNhat),
                    new SqlParameter("sMaLoaiCongTrinh", sMaLoaiCongTrinh)
                };
                return ctx.FromSqlRaw<VdtThongTriChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriChiTietByParentId(Guid iId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtThongTriChiTietQuery>(@"EXECUTE dbo.sp_vdt_thongtri_detail_by_id @iId",
                new SqlParameter("iId", iId)).ToList();
            }
        }

        public void DeleteThongTriChiTietByParentId(Guid iIdThongTriId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                List<VdtThongTriChiTiet> lstData = ctx.VdtThongTriChiTiets.Where(n => n.IIdThongTriId == iIdThongTriId).ToList();
                ctx.VdtThongTriChiTiets.RemoveRange(lstData);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<VdtCanCuThanhToanQuery> GetCanCuThanhToanByThongTri(Guid iIdThongTri, bool bIsThanhToan, string sMaDonVi, int iNamLamViec, int iNguonVon, DateTime dNgayLap)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var sql = @"EXECUTE dbo.sp_vdt_thongtri_get_chungtuthanhtoan @iIdThongTri, @bIsThanhToan, @sMaDonVi, @iNamLamViec, @iNguonVon, @dNgayLap";
                var parameters = new[]
                {
                    new SqlParameter("iIdThongTri", iIdThongTri),
                    new SqlParameter("bIsThanhToan", bIsThanhToan),
                    new SqlParameter("sMaDonVi", sMaDonVi),
                    new SqlParameter("iNamLamViec", iNamLamViec),
                    new SqlParameter("iNguonVon", iNguonVon),
                    new SqlParameter("dNgayLap", dNgayLap)
                };
                return ctx.FromSqlRaw<VdtCanCuThanhToanQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriChiTietByPheDuyet()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"EXECUTE dbo.sp_vdt_thongtri_by_pheduyet_detail";
                return ctx.FromSqlRaw<VdtThongTriChiTietQuery>(sql).ToList();
            }
        }

        public IEnumerable<VdtThongTriChiTietQuery> FindByIdThongTri(Guid idThongTriId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"select NULL as SSoThongTri, ct.fSoTien as FSoTien, 
                ct.iID_DuAnID as IIdDuAnId, ct.iID_NhaThauID as IIdNhaThauId, 
                ct.iID_MucID as IIdMucId, ct.iID_TieuMucID as IIdTieuMucId,
                ct.iID_TietMucID as IIdTietMucId, ct.iID_NganhID as IIdNganhId,
                ct.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId, NULL as IIdLoaiNguonVonId,
                ct.sDonViThuHuong as SDonViThuHuong 
                from VDT_ThongTri_ChiTiet ct left join VDT_ThongTri tt on ct.iID_ThongTriID = tt.Id 
                where tt.Id = @idThongTri";
                var parameters = new[]
                {
                    new SqlParameter("@idThongTri", idThongTriId)
                };
                return ctx.FromSqlRaw<VdtThongTriChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtThongTriQuyetToanQuery> GetVdtThongTriQuyetToanById(Guid iIdThongTri)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"EXECUTE dbo.sp_vdt_getthongtriquyettoanchitiet_by_id @iIdThongTriId";
                var parameters = new[]
                {
                    new SqlParameter("@iIdThongTriId", iIdThongTri)
                };
                return ctx.FromSqlRaw<VdtThongTriQuyetToanQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtThongTriQuyetToanQuery> GetVdtThongTriQuyetToan(Guid iIdQuyetToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"EXECUTE dbo.sp_vdt_getthongtriquyettoanchitiet @iIdQuyetToanId";
                var parameters = new[]
                {
                    new SqlParameter("@iIdQuyetToanId", iIdQuyetToanId)
                };
                return ctx.FromSqlRaw<VdtThongTriQuyetToanQuery>(sql, parameters).ToList();
            }
        }
    }
}
