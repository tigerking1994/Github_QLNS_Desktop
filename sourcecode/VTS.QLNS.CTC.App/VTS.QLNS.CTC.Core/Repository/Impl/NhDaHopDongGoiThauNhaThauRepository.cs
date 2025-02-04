using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaHopDongGoiThauNhaThauRepository : Repository<NhDaHopDongGoiThauNhaThau>, INhDaHopDongGoiThauNhaThauRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaHopDongGoiThauNhaThauRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaHopDongGoiThauNhaThauQuery> FindByIdHopDong(Guid? idHopDong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += "SELECT ";
                sql += " hopdongnhathau.Id AS Id, ";
                sql += " hopdongnhathau.iID_HopDongID AS IIdHopDongId, ";
                sql += " hopdongnhathau.isCheck AS IsCheck, ";
                sql += " hopdongnhathau.iID_GoiThauID AS IIdGoiThauId, ";
                sql += " hopdongnhathau.iID_NhaThauID AS IIdNhaThauId, ";
                sql += " hopdongnhathau.fGiaTriUSD AS FGiaTriUsd, ";
                sql += " hopdongnhathau.fGiaTriVND AS FGiaTriVnd, ";
                sql += " hopdongnhathau.fGiaTriEUR AS FGiaTriEur, ";
                sql += " hopdongnhathau.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac, ";
                sql += " hopdongnhathau.fGiaTriHopDong_USD AS FGiaTriHopDong_Usd, ";
                sql += " hopdongnhathau.fGiaTriHopDong_VND AS FGiaTriHopDong_Vnd, ";
                sql += " hopdongnhathau.fGiaTriHopDong_EUR AS FGiaTriHopDong_Eur, ";
                sql += " hopdongnhathau.fGiaTriHopDong_NgoaiTeKhac AS FGiaTriHopDong_NgoaiTeKhac, ";
                sql += " goithau.fGiaGoiThauUSD as FGiaTriGoiThauUsd, ";
                sql += " goithau.fGiaGoiThauVND as FGiaTriGoiThauVnd, ";
                sql += " goithau.fGiaGoiThauEUR as FGiaTriGoiThauEur, ";
                sql += " goithau.fGiaGoiThauNgoaiTeKhac as FGiaTriGoiThauNgoaiTeKhac, ";
                sql += " goithau.sTenGoiThau as STenGoiThau, ";
                sql += " dmnhathau.sTenNhaThau as STenNhaThau, ";
                sql += " hopdongnhathau.iThoiGianThucHien AS IThoiGianThucHien, ";
                sql += " hopdongnhathau.iID_DonViTienTeID AS IIdDonViTienTeId ";
                sql += " FROM NH_DA_HopDong_GoiThau_NhaThau hopdongnhathau ";
                sql += " INNER JOIN NH_DA_GoiThau goithau ";
                sql += " ON hopdongnhathau.iID_GoiThauID = goithau.iID_GoiThauID ";
                sql += " LEFT JOIN NH_DM_NhaThau dmnhathau ";
                sql += " ON hopdongnhathau.iID_NhaThauID = dmnhathau.Id ";
                sql += " WHERE hopdongnhathau.iID_HopDongID = @idhopdong ";
                var parameters = new object[]
                {
                    new SqlParameter("@idhopdong", idHopDong)
                };
                return ctx.FromSqlRaw<NhDaHopDongGoiThauNhaThauQuery>(sql, parameters).ToList();
            }
        }
    }
}
