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
    public class NhHdnkCacQuyetDinhNguonVonRepository : Repository<NhHdnkCacQuyetDinhNguonVon>, INhHdnkCacQuyetDinhNguonVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhHdnkCacQuyetDinhNguonVonRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhHdnkCacQuyetDinhNguonVon> FindByIdQuyetDinh(Guid? idQuyetDinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhHdnkCacQuyetDinhNguonVons.Where(n => n.IIdCacQuyetDinhId == idQuyetDinh).ToList();
            }
        }

        public IEnumerable<NhThongTinNGuonVonQuery> FindByThongTinNguonVon(Guid idQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += " SELECT NH_HDNK_CacQuyetDinh_NguonVon.ID as Id, ";
                sql += " NH_HDNK_CacQuyetDinh_NguonVon.iID_CacQuyetDinhID as IIdCacQuyetDinhId, ";
                sql += " NH_HDNK_CacQuyetDinh_NguonVon.iID_NguonVonID as IIdNguonVonId, ";
                sql += " NH_HDNK_CacQuyetDinh_NguonVon.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac, ";
                sql += " NH_HDNK_CacQuyetDinh_NguonVon.fGiaTriUSD as FGiaTriUSD, ";
                sql += " NH_HDNK_CacQuyetDinh_NguonVon.fGiaTriVND as FGiaTriVND, ";
                sql += " NguonNganSach.sTen as STenNguonVon,  ";
                sql += " NH_HDNK_CacQuyetDinh_NguonVon.fGiaTriEUR as fGiaTriEUR ";
                sql += " FROM NH_HDNK_CacQuyetDinh_NguonVon INNER JOIN NguonNganSach ";
                sql += "    ON NH_HDNK_CacQuyetDinh_NguonVon.iID_NguonVonID = NguonNganSach.iID_MaNguonNganSach ";
                sql += " WHERE NH_HDNK_CacQuyetDinh_NguonVon.iID_CacQuyetDinhID = @IdQuyetDinh ";
                var parameters = new[]
                {
                    new SqlParameter("@IdQuyetDinh", idQuyetDinh)
                };
                return ctx.FromSqlRaw<NhThongTinNGuonVonQuery>(sql, parameters).ToList();
            }
        }
        public IEnumerable<NhThongTinNGuonVonQuery> FindByIdKhttNhiemVuChi(Guid idKhttNhiemVuChi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                String sql = "";
                sql += "SELECT cqd_nv.ID as Id, cqd_nv.iID_CacQuyetDinhID as IIdCacQuyetDinhId, cqd_nv.iID_NguonVonID as IIdNguonVonId, cqd_nv.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhac, ";
                sql += "cqd_nv.fGiaTriUSD as FGiaTriUsd, cqd_nv.fGiaTriEur as FGiaTriEur, cqd_nv.fGiaTriVND as FGiaTriVnd, ns.STen as STenNguonVon ";
                sql += "FROM NH_HDNK_CacQuyetDinh_NguonVon cqd_nv ";
                sql += "LEFT JOIN NH_HDNK_CacQuyetDinh cqd ON cqd_nv.iID_CacQuyetDinhID = cqd.ID ";
                sql += "LEFT JOIN NguonNganSach ns ON cqd_nv.iID_NguonVonID = ns.iID_MaNguonNganSach ";
                sql += "WHERE ";
                sql += "    cqd.iID_KHTongThe_NhiemVuChiID = @IdKhttNhiemVuChi ";

                SqlParameter idKhttNhiemVuChiParam = new SqlParameter("@IdKhttNhiemVuChi", idKhttNhiemVuChi);

                return ctx.FromSqlRaw<NhThongTinNGuonVonQuery>(sql, idKhttNhiemVuChiParam).ToList();
            }
        }
    }
}
