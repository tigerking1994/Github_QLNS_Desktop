using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhHdnkCacQuyetDinhRepository : Repository<NhHdnkCacQuyetDinh>, INhHdnkCacQuyetDinhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhHdnkCacQuyetDinhRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhQuyetDinhDamPhamQuery> FindByCondition(int iLoai)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_hdnk_cacquyetdinh_index @iLoai";
                var parameters = new[]
                {
                    new SqlParameter("@iLoai", iLoai)
                };
                return ctx.FromSqlRaw<NhQuyetDinhDamPhamQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NhHdnkCacQuyetDinh> FindByIdNhiemVuChiILoaiQuyetDinh(Guid idNhiemVuChi, int iLoaiQuyetDinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhHdnkCacQuyetDinhs.Where(n => n.IIdKhTongTheNhiemVuChiId == idNhiemVuChi && n.ILoaiQuyetDinh == iLoaiQuyetDinh && n.BIsActive == true).ToList();
            }
        }

        public IEnumerable<NhHdnkCacQuyetDinh> FindByIdNhiemVuChi(Guid idNhiemVuChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhHdnkCacQuyetDinhs.Where(n => n.IIdKhTongTheNhiemVuChiId == idNhiemVuChi && n.BIsActive).ToList();
            }
        }

        public void DeleteQuyetDinh(Guid id, Guid? parentId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_hdnk_delete_quyetdinhdampham @id";
                var parameters = new[]
                {
                    new SqlParameter("@id", id),
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<NhCacQuyetDinhNhiemVuChiQuery> FindByNhiemVuChi(Guid idNhiemVuChi, int iLoaiQuyetDinh, Guid idKhTongThe)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += "SELECT ";
                sql += "    NH_HDNK_CacQuyetDinh.ID as IdCacQuyetDinh, ";
                sql += "    NH_DM_NhiemVuChi.ID as IdNhiemVuChi, ";
                sql += "    NH_HDNK_CacQuyetDinh.sSoQuyetDinh as SSoQuyetDinh, ";
                sql += "    NH_HDNK_CacQuyetDinh.iLoaiQuyetDinh as ILoaiQuyetDinh, ";
                sql += "    NH_DM_NhiemVuChi.sTenNhiemVuChi as STenNhiemVuChi ";
                sql += "FROM NH_KHTongThe_NhiemVuChi ";
                sql += "INNER JOIN NH_HDNK_CacQuyetDinh on NH_HDNK_CacQuyetDinh.iID_KHTongThe_NhiemVuChiID = NH_KHTongThe_NhiemVuChi.ID ";
                sql += "INNER JOIN NH_DM_NhiemVuChi on NH_DM_NhiemVuChi.ID = NH_KHTongThe_NhiemVuChi.iID_NhiemVuChiID ";
                sql += "INNER JOIN NH_KHTongThe on NH_KHTongThe_NhiemVuChi.iID_KHTongTheID = NH_KHTongThe.ID ";
                sql += "WHERE NH_DM_NhiemVuChi.ID = @IdNhiemVuChi ";
                sql += "    AND NH_HDNK_CacQuyetDinh.iLoaiQuyetDinh = @ILoaiQuyetDinh";
                sql += "    AND NH_KHTongThe.ID = @IdKhTongThe ";
                sql += "    AND NH_HDNK_CacQuyetDinh.bIsActive = 1 ";
                var parameters = new[]
                {
                    new SqlParameter("@IdNhiemVuChi", idNhiemVuChi),
                    new SqlParameter("@ILoaiQuyetDinh", iLoaiQuyetDinh),
                    new SqlParameter("@IdKhTongThe", idKhTongThe)
                };
                return ctx.FromSqlRaw<NhCacQuyetDinhNhiemVuChiQuery>(sql, parameters).ToList();
            }
        }
    }
}
