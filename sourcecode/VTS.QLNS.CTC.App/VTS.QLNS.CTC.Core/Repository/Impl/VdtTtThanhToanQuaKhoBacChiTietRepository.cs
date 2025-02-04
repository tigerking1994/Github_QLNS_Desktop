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
    public class VdtTtThanhToanQuaKhoBacChiTietRepository : Repository<VdtTtThanhToanQuaKhoBacChiTiet>, IVdtTtThanhToanQuaKhoBacChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtTtThanhToanQuaKhoBacChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtTtThanhToanQuaKhoBacChiTiet> GetDetailDataByParentId(Guid iId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtTtThanhToanQuaKhoBacChiTiets.Where(n => n.IIdThanhToanId == iId);
            }
        }

        public void DeleteDetailData(IEnumerable<VdtTtThanhToanQuaKhoBacChiTiet> lstData)
        {
            RemoveRange(lstData);
        }

        public IEnumerable<DuAnDeNghiThanhToanQuery> GetDuAnByThanhToanKhoBac(int iNamKeHoach, DateTime dNgayQuyetDinh, string sLNS, string iIdMaDonViQuanLy)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_get_duan_by_thanhtoankhobac_dialog @iNamKeHoach, @dNgayQuyetDinh, @sLNS, @iIdMaDonViQuanLy";
                var parameters = new[]
                {
                    new SqlParameter("@iNamKeHoach", iNamKeHoach),
                    new SqlParameter("@dNgayQuyetDinh", dNgayQuyetDinh),
                    new SqlParameter("@sLNS", sLNS),
                    new SqlParameter("@iIdMaDonViQuanLy", iIdMaDonViQuanLy)
                };
                return ctx.FromSqlRaw<DuAnDeNghiThanhToanQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ThanhToanQuaKhoBacChiTietQuery> GetThanhToanKhoBacDetail(int iNamKeHoach, DateTime dNgayQuyetDinh, Guid iIdLoaiNguonVon, string iIdMaDonViQuanLy)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_get_duan_by_thanhtoankhobac_detail @iNamKeHoach, @dNgayQuyetDinh, @iIdLoaiNganSach, @iIdMaDonViQuanLy";
                var parameters = new[]
                {
                    new SqlParameter("@iNamKeHoach", iNamKeHoach),
                    new SqlParameter("@dNgayQuyetDinh", dNgayQuyetDinh),
                    new SqlParameter("@iIdLoaiNganSach", iIdLoaiNguonVon),
                    new SqlParameter("@iIdMaDonViQuanLy", iIdMaDonViQuanLy)
                };
                return ctx.FromSqlRaw<ThanhToanQuaKhoBacChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ThanhToanQuaKhoBacChiTietQuery> GetThanhToanKhoBacDetailByParentId(Guid iIdParentId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_get_duan_by_thanhtoankhobac_detail_by_parentid @iIdParentid";
                var parameters = new[]
                {
                    new SqlParameter("@iIdParentid", iIdParentId)
                };
                return ctx.FromSqlRaw<ThanhToanQuaKhoBacChiTietQuery>(sql, parameters).ToList();
            }
        }
    }
}
