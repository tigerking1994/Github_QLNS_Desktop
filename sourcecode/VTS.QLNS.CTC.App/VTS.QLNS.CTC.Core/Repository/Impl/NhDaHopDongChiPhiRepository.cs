using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaHopDongChiPhiRepository : Repository<NhDaHopDongChiPhi>, INhDaHopDongChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaHopDongChiPhiRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<NhDaHopDongChiPhi> FindByIdHopDong(Guid idHopDong)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var data = from hdChiPhi in ctx.NhDaHopDongChiPhis.Where(x => x.IIdHopDongId == idHopDong)
                           join chiPhi in ctx.NhDmChiPhis
                           on hdChiPhi.IIdChiPhiId equals chiPhi.IIdChiPhi
                           select new NhDaHopDongChiPhi
                           {
                               Id = hdChiPhi.Id,
                               IIdChiPhiId = hdChiPhi.IIdChiPhiId,
                               IIdHopDongId = hdChiPhi.IIdHopDongId,
                               IIdParentId = hdChiPhi.IIdParentId,
                               IIdCacQuyetDinhChiPhiID = hdChiPhi.IIdCacQuyetDinhChiPhiID,
                               IIdCacQuyetDinhId = hdChiPhi.IIdCacQuyetDinhId,
                               IIdGoiThauChiPhiId = hdChiPhi.IIdGoiThauChiPhiId,
                               FTienHopDongEUR = hdChiPhi.FTienHopDongEUR,
                               FTienHopDongVND = hdChiPhi.FTienHopDongVND,
                               FTienHopDongUSD = hdChiPhi.FTienHopDongUSD,
                               FTienHopDongNgoaiTeKhac = hdChiPhi.FTienHopDongNgoaiTeKhac,
                               FGiaTriEur = hdChiPhi.FGiaTriEur,
                               FGiaTriUsd = hdChiPhi.FGiaTriUsd,
                               FGiaTriVnd = hdChiPhi.FGiaTriVnd,
                               FGiaTriNgoaiTeKhac = hdChiPhi.FGiaTriNgoaiTeKhac,
                               STenChiPhi = chiPhi.STenChiPhi,
                               SMaOrder = hdChiPhi.SMaOrder,
                               IIdHopDongGoiThauNhaThauId = hdChiPhi.IIdHopDongGoiThauNhaThauId,
                               IIdHopDongNguonVonId = hdChiPhi.IIdHopDongNguonVonId
                           };
                return data.ToList();
            }
        }
        public IEnumerable<NhDaHopDongChiPhi> FindByHopDongIdQuyetDinhId(Guid? idHopDong, Guid? idQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaHopDongChiPhis.Where(x => x.IIdHopDongId == idHopDong && x.IIdCacQuyetDinhChiPhiID == idQuyetDinh).ToList();
            }
        }
        public IEnumerable<NhDaHopDongChiPhi> FindByHopDongIdGoiThauChiPhiId(Guid? idHopDong, Guid? idGoiThauChiPhi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaHopDongChiPhis.Where(x => x.IIdHopDongId == idHopDong && x.IIdGoiThauChiPhiId == idGoiThauChiPhi).ToList();
            }
        }

        public void DeleteChiphiHopDongTrongNuoc(Guid? IIdHopDongGoiThauNhaThauId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += $" DELETE NH_DA_HopDong_HangMuc WHERE iID_HopDong_ChiPhiID IN(SELECT Id FROM NH_DA_HopDong_ChiPhi chiphi WHERE chiphi.iID_HopDongGoiThauNhaThauID = @iIdHopDongGoiThauNhaThauId) ";
                sql += $" DELETE NH_DA_HopDong_ChiPhi WHERE iID_HopDongGoiThauNhaThauID = @iIdHopDongGoiThauNhaThauId ";
                var parameters = new object[]
                {
                    new SqlParameter("@iIdHopDongGoiThauNhaThauId", IIdHopDongGoiThauNhaThauId)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<NhDaHopDongChiPhi> FindByHopDongGoiThauNhaThauID(Guid? idHopDongGoiThauNhaThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaHopDongChiPhis.Where(x => x.IIdHopDongGoiThauNhaThauId == idHopDongGoiThauNhaThau).ToList();
            }
        }

        public void DeleteByHdNguonVonId(Guid idNguonVon)
        {
            var lstDeleted = this.FindAll(x => x.IIdHopDongNguonVonId == idNguonVon);
            if (lstDeleted.Any())
            {
                this.RemoveRange(lstDeleted);
            }
        }
        public void SaveListHangMuc(NhDaHopDongHangMuc listhangmuc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "UPDATE NH_DA_hopdong_HangMuc ";
                sql += $" SET fTienHopDong_EUR=@fTienHopDong_EUR,fTienHopDong_USD=@fTienHopDong_USD,fTienHopDong_VND=@fTienHopDong_VND,fTienHopDong_NgoaiTeKhac=@fTienHopDong_NgoaiTeKhac ";
                sql += $" WHERE iID_GoiThau_HangMucID=@IIdGoiThauHangMucId";
                var parameters = new object[]
                {
                    new SqlParameter("@fTienHopDong_EUR", listhangmuc.FGiaTriEur),
                    new SqlParameter("@fTienHopDong_USD", listhangmuc.FGiaTriUsd),
                    new SqlParameter("@fTienHopDong_VND", listhangmuc.FGiaTriVnd),
                    new SqlParameter("@fTienHopDong_NgoaiTeKhac", listhangmuc.FGiaTriNgoaiTeKhac),
                    new SqlParameter("@IIdGoiThauHangMucId", listhangmuc.IIdGoiThauHangMucId)
                    };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<NhDaGoiThauChiPhiHangMucQuery> FindHopDongChiPhihangMucById(Guid idHopDongID)
        {
            using (var conn = _contextFactory.CreateDbContext())
            {
                string sql = @"SELECT 
                               hd.ID,
                               hd.sTenHopDong,
                               hd.sSoHopDong,
                               hdcp.ID as IIdHopDongChiPhiId,
                               hdcp.iID_ChiPhiID,
                               hdcp.iID_GoiThau_ChiPhiID,
                               hdcp.iID_ParentID,
                               (CASE
                               	WHEN hdhm.fTienHopDong_USD is NULL THEN hdcp.fTienHopDong_USD
                               	ELSE hdhm.fTienHopDong_USD
                               END) as FTienHopDongUsd,
                               (CASE
                               	WHEN hdhm.fTienHopDong_VND is NULL THEN hdcp.fTienHopDong_VND
                               	ELSE hdhm.fTienHopDong_VND
                               END) as FTienHopDongVnd,
                               (CASE
                               	WHEN hdhm.fTienHopDong_EUR is NULL THEN hdcp.fTienHopDong_EUR
                               	ELSE hdhm.fTienHopDong_EUR
                               END) as FTienHopDongEur,
                               (CASE
                               	WHEN hdhm.fTienHopDong_NgoaiTeKhac is NULL THEN hdcp.fTienHopDong_NgoaiTeKhac
                               	ELSE hdhm.fTienHopDong_NgoaiTeKhac
                               END) as FTienHopDongNgoaiTeKhac,
                               hdhm.Id,
                               hdhm.iID_GoiThau_HangMucID
                               FROM NH_DA_HopDong hd
                               LEFT JOIN NH_DA_HopDong_ChiPhi hdcp ON hd.ID = hdcp.iID_HopDongID
                               LEFT JOIN NH_DA_HopDong_HangMuc hdhm ON hdhm.iID_HopDong_ChiPhiID = hdcp.ID
                               WHERE hd.ID = @idHopDongID";
                var parameters = new[]
                {
                    new SqlParameter("@idHopDongID", idHopDongID)
                };
                return conn.FromSqlRaw<NhDaGoiThauChiPhiHangMucQuery>(sql, parameters);
            }
        }

        public void DeleteChiphiHangMucHopDongByIdHopDong(Guid? IIdHopDongId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += $" DELETE NH_DA_HopDong_HangMuc WHERE iID_HopDong_ID = @iID_HopDong_ID ;";
                sql += $" DELETE NH_DA_HopDong_ChiPhi WHERE iID_HopDongID = @iID_HopDong_ID ";
                var parameters = new object[]
                {
                    new SqlParameter("@iID_HopDong_ID", IIdHopDongId)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
