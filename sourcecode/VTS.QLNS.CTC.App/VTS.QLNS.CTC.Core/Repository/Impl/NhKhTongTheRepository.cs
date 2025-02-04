using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhKhTongTheRepository : Repository<NhKhTongThe>, INhKhTongTheRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhKhTongTheRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public NhKhTongThe FindByPredicate(Expression<Func<NhKhTongThe, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                // return ctx.NhKhTongThes.Include(g => g.SSoKeHoachTtcp).ThenInclude(s => s.Union)
                // .SingleOrDefault(predicate);
                return ctx.NhKhTongThes.SingleOrDefault(predicate);
            }
        }

        public IEnumerable<NhKhTongThe> FindAllOrdered()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhKhTongThes.OrderByDescending(n => n.DNgayTao).ToList();
            }
        }
        public IEnumerable<NhKhTongTheQuery> FindAllOverview()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //string sql = "SELECT " +
                //                "t.ID id, " +
                //                "t.iNamKeHoach iNamKeHoach, " +
                //                "t.iGiaiDoanTu iGiaiDoanTu, " +
                //                "t.iGiaiDoanDen iGiaiDoanDen, " +
                //                "t.iGiaiDoanTu_TTCP iGiaiDoanTu_TTCP, " +
                //                "t.iGiaiDoanDen_TTCP iGiaiDoanDen_TTCP, " +
                //                "t.iGiaiDoanTu_BQP iGiaiDoanTu_BQP, " +
                //                "t.iGiaiDoanDen_BQP iGiaiDoanDen_BQP, " +
                //                "t.iID_ParentID iIdParentId, " +
                //                "t.iID_ParentAdjustID iIdParentAdjustId, " +
                //                "t.iID_GocID iIdGocId, " +
                //                "t.sSoKeHoachTTCP, " +
                //                "t.dNgayKeHoachTTCP, " +
                //                "t.sMoTaChiTiet_KHTTCP sMoTaChiTietKhttcp, " +
                //                "t.fTongGiaTri_KHTTCP fTongGiaTriKhttcp, " +
                //                "t.sSoKeHoachBQP, " +
                //                "t.dNgayKeHoachBQP, " +
                //                "t.sMoTaChiTiet_KHBQP sMoTaChiTietKhbqp, " +
                //                "t.fTongGiaTri_KHBQP fTongGiaTriKhbqp, " +
                //                "t.fTongGiaTri_KHBQP_VND fTongGiaTriKhbqpVnd, " +
                //                "t.dNgayTao, " +
                //                "t.sNguoiTao, " +
                //                "t.dNgaySua, " +
                //                "t.sNguoiSua, " +
                //                "t.dNgayXoa, " +
                //                "t.sNguoiXoa, " +
                //                "t.bIsActive, " +
                //                "t.bIsGoc, " +
                //                "t.bIsKhoa, " +
                //                "t.iLanDieuChinh," +
                //                "t.iLoai," +
                //                "(SELECT COUNT(Id) FROM Attachment WHERE ModuleType = 402 AND ObjectId = t.ID) AS TotalFiles, " +
                //                "CASE WHEN t.iID_ParentAdjustID is null " +
                //                "THEN '' " +
                //                "ELSE ( SELECT khpr.sSoKeHoachBQP " +
                //                "FROM NH_KHTongThe khpr WHERE khpr.ID = t.iID_ParentAdjustID ) END DieuChinhTu " +
                //            "FROM " +
                //                "NH_KHTongThe AS t " +
                //                "ORDER BY t.dNgayTao DESC";
                //return ctx.FromSqlRaw<NhKhTongTheQuery>(sql).ToList();
                string executeSql = "EXECUTE sp_nh_kehoachtongthe_index";
                return ctx.FromSqlRaw<NhKhTongTheQuery>(executeSql).ToList();
            }
        }

        public NhKhTongThe FindByParentIdAndNamKeHoach(Guid idParent, int iNamKeHoach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhKhTongThes.FirstOrDefault(n => n.IIdParentId == idParent && n.INamKeHoach == iNamKeHoach);
            }
        }

        public IEnumerable<NhKhTongThe> FindByParentId(Guid idParent)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhKhTongThes.Where(n => n.IIdParentId == idParent).ToList();
            }
        }
        public IEnumerable<NhKhTongThe> FindByDonViId(Guid idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"select distinct khtt.* from NH_KHTongThe khtt " +
                    "join NH_KHTongThe_NhiemVuChi khnvc  on khtt.ID = khnvc.iID_KHTongTheID " +
                    "join DonVi dv on khnvc.iID_DonViThuHuongID = dv.iID_DonVi " +
                    "where dv.iID_DonVi = @idDonVi ";
                var parameters = new[]
                {
                    new SqlParameter("@idDonVi", idDonVi.ToString())
                };
                return ctx.FromSqlRaw<NhKhTongThe>(sql, parameters).ToList();
            }
        }
    }
}
