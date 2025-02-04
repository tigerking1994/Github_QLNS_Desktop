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
    public class NhHdnkCacQuyetDinhChiPhiRepository : Repository<NhHdnkCacQuyetDinhChiPhi>, INhHdnkCacQuyetDinhChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhHdnkCacQuyetDinhChiPhiRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhHdnkCacQuyetDinhChiPhi> FindByIdQuyetDinh(Guid? idQuyetDinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhHdnkCacQuyetDinhChiPhis.Where(n => n.IIdCacQuyetDinhId == idQuyetDinh).ToList();
            }
        }

        public IEnumerable<NhHdnkCacQuyetDinhChiPhiQuery> FindByIdKhttNhiemVuChi(Guid idKhttNhiemVuChi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                String sql = "";
                sql += "SELECT cqd_cp.ID as Id, cqd_cp.iID_CacQuyetDinhID as IIdCacQuyetDinhId, cqd_cp.iID_ChiPhiID as IIdChiPhiId, cqd_cp.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac, cqd_cp.iID_ParentID as IIDParentId, ";
                sql += "cqd_cp.fGiaTriUSD as FGiaTriUsd, cqd_cp.fGiaTriEur as FGiaTriEur, cqd_cp.fGiaTriVND as FGiaTriVnd, cqd_cp.STenChiPhi as STenChiPhi, cqd_cp.SMaOrder as SMaOrder ";
                sql += "FROM NH_HDNK_CacQuyetDinh_ChiPhi cqd_cp ";
                sql += "LEFT JOIN NH_HDNK_CacQuyetDinh cqd ON cqd_cp.iID_CacQuyetDinhID = cqd.ID ";
                sql += "WHERE ";
                sql += "    cqd.iID_KHTongThe_NhiemVuChiID = @IdKhttNhiemVuChi ";
                var parameters = new[]
                {
                    new SqlParameter("@IdKhttNhiemVuChi", idKhttNhiemVuChi)
                };
                return ctx.FromSqlRaw<NhHdnkCacQuyetDinhChiPhiQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NhHdnkCacQuyetDinhChiPhiDmChiPhiQuery> FindByIdQuyetDinhGoiThau(Guid? idQuyetDinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                String sql = "";
                sql += "SELECT cqd_cp.ID as Id, cqd_cp.iID_CacQuyetDinhID as IIdCacQuyetDinhId, cqd_cp.iID_ChiPhiID as IIdChiPhiId, cqd_cp.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac, ";
                sql += "cqd_cp.fGiaTriUSD as FGiaTriUsd, cqd_cp.fGiaTriEur as FGiaTriEur, cqd_cp.fGiaTriVND as FGiaTriVnd, cqd_cp.STenChiPhi as STenChiPhi, cqd_cp.SMaOrder as SMaOrder, ";
                sql += "dmchiphi.sTenChiPhi as STenDanhMucChiPhi, cqd_cp.iID_ParentID as IIdParentId ";
                sql += "FROM NH_HDNK_CacQuyetDinh_ChiPhi cqd_cp ";
                sql += "LEFT JOIN NH_DM_ChiPhi dmchiphi ON cqd_cp.iID_ChiPhiID = dmchiphi.iID_ChiPhi ";
                sql += "WHERE ";
                sql += "    cqd_cp.iID_CacQuyetDinhID = @IdQuyetDinh ";
                var parameters = new[]
                {
                    new SqlParameter("@IdQuyetDinh", idQuyetDinh)
                };
                return ctx.FromSqlRaw<NhHdnkCacQuyetDinhChiPhiDmChiPhiQuery>(sql, parameters).ToList();
            }
        }
    }
}
