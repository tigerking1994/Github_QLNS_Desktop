using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaGoiThauHangMucRepository : Repository<NhDaGoiThauHangMuc>, INhDaGoiThauHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaGoiThauHangMucRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaDetailHangMucQuery> GetGoiThauHangMucByKhlcntId(Guid iIdKhlcnt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcnt)
                };
                string executeSql = "EXECUTE dbo.sp_nh_goithau_hangmuc_by_khlcnt @iIdKhlcnt";
                return ctx.FromSqlRaw<NhDaDetailHangMucQuery>(executeSql, parameters).ToList();
            }
        }
          public IEnumerable<NhDaDetailHangMucQuery> GetGoiThauHangMucByGoiThautId(Guid iIdKhlcnt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcnt)
                };
                string executeSql = "EXECUTE dbo.sp_nh_goithau_hangmuc_by_goithau @iIdKhlcnt";
                return ctx.FromSqlRaw<NhDaDetailHangMucQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaGoiThauHangMucQuery> FindByChiPhiId(Guid idChiPhi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_hangmuc_bychiphiid @idChiPhi";
                var parameters = new[]
                {
                    new SqlParameter("@idChiPhi", idChiPhi)
                };
                return ctx.FromSqlRaw<NhDaGoiThauHangMucQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaGoiThauHangMucQuery> FindByGoiThauId(Guid idGoiThau)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_hangmuc_bygoithauid @idGoiThau";
                var parameters = new[]
                {
                    new SqlParameter("@idGoiThau", idGoiThau)
                };
                return ctx.FromSqlRaw<NhDaGoiThauHangMucQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaGoiThauHangMuc> FindDataByGoiThauID(Guid idGoiThauID)
        {
            using(var conn = _contextFactory.CreateDbContext())
            {
                string sql = @" SELECT hm.iID_GoiThau_HangMucID , 
	                            hm.fTienGoiThau_EUR as FTienGoiThauEur, 
	                            hm.fTienGoiThau_NgoaiTeKhac as FTienGoiThauNgoaiTeKhac, 
	                            hm.fTienGoiThau_USD as FTienGoiThauUsd, 
	                            hm.fTienGoiThau_VND as FTienGoiThauVnd,
	                            hm.iID_CacQuyetDinh_HangMucID as IIdCacQuyetDinhHangMucId, 
	                            hm.iID_DuToan_HangMucID as IIdDuToanHangMucId, 
	                            hm.iID_GoiThau_ChiPhiID as IIdGoiThauChiPhiId, 
	                            hm.iID_ParentID as IIdParentId, 
	                            hm.iID_QDDauTu_HangMucID as IIdQdDauTuHangMucId, 
	                            hm.sMaHangMuc as SMaHangMuc, 
	                            hm.sMaOrder as SMaOrder,
	                            hm.sTenHangMuc as STenHangMuc, 
	                            hm.iIDGoiThauCheck as IIDGoiThauCheck, 
	                            hm.isCheck as IsCheck
	                            FROM NH_DA_GoiThau as tbl	
	                            INNER JOIN NH_DA_GoiThau_ChiPhi as cp on tbl.iID_GoiThauID = cp.iID_GoiThauID
	                            INNER JOIN NH_DA_GoiThau_HangMuc as hm on cp.Id = hm.iID_GoiThau_ChiPhiID
	                            WHERE tbl.iID_GoiThauID = @iID_GoiThauID;";
                var parameters = new[]
                {
                    new SqlParameter("@iID_GoiThauID", idGoiThauID)
                };
                return conn.FromSqlRaw<NhDaGoiThauHangMuc>(sql, parameters);
            }
        }

        public NhDaGoiThauHangMuc FindHangMucById(Guid idHangMuc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = " SELECT HangMuc.iID_GoiThau_HangMucID,";
                sql += "HangMuc.isCheck as IsCheck,";
                sql += "HangMuc.iID_GoiThau_ChiPhiID as IIdGoiThauChiPhiId,";
                sql += "HangMuc.iIDGoiThauCheck as IIDGoiThauCheck,";
                sql += "HangMuc.iID_QDDauTu_HangMucID as IIdQDDauTuHangMucId,";
                sql += "HangMuc.iID_DuToan_HangMucID as IIdDuToanHangMucId,";
                sql += "HangMuc.fTienGoiThau_USD as FTienGoiThauUsd,";
                sql += "HangMuc.fTienGoiThau_VND as FTienGoiThauVnd,";
                sql += "HangMuc.fTienGoiThau_EUR as FTienGoiThauEur,";
                sql += "HangMuc.fTienGoiThau_NgoaiTeKhac as FTienGoiThauNgoaiTeKhac,";
                sql += "HangMuc.sTenHangMuc as STenHangMuc,";
                sql += "HangMuc.sMaOrder as SMaOrder,";
                sql += "HangMuc.sMaHangMuc as SMaHangMuc,";
                sql += "HangMuc.iID_ParentID as IIdParentId,";
                sql += "HangMuc.iID_CacQuyetDinh_HangMucID as IIdCacQuyetDinhHangMucId ";
                sql += "FROM NH_DA_GoiThau_HangMuc HangMuc ";
                sql += "WHERE HangMuc.iID_GoiThau_HangMucID = @IdHangMuc"; 


                var parameters = new[]
                {
                    new SqlParameter("@IdHangMuc", idHangMuc)
                };
                return ctx.FromSqlRaw<NhDaGoiThauHangMuc>(sql, parameters).FirstOrDefault();
            }
        }

        public IEnumerable<NhDaHangMucGoiThauQuery> FindByChiPhi(Guid idChiPhi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += "SELECT ";
                sql += " gthangmuc.fTienGoiThau_USD AS FGiaTriUSDGoiThau, ";
                sql += " gthangmuc.fTienGoiThau_VND AS FGiaTriVNDGoiThau, ";
                sql += " gthangmuc.fTienGoiThau_EUR AS FGiaTriEURGoiThau, ";
                sql += " gthangmuc.fTienGoiThau_NgoaiTeKhac AS FGiaTriNgoaiTeKhacGoiThau, ";
                sql += " cphangmuc.fGiaTriUSD AS FGiaTriUsd, ";
                sql += " cphangmuc.fGiaTriVND AS FGiaTriVnd, ";
                sql += " cphangmuc.fGiaTriEUR AS FGiaTriEur, ";
                sql += " cphangmuc.fGiaTriNgoaiTeKhac AS FGiaTriNgoaiTeKhac, ";
                sql += " gthangmuc.iID_GoiThau_HangMucID AS Id, ";
                sql += " cphangmuc.sTenHangMuc AS STenHangMuc, ";
                sql += " cphangmuc.sMaHangMuc AS SMaHangMuc, ";
                sql += " cphangmuc.sMaOrder AS SMaOrder ";
                sql += " FROM NH_DA_GoiThau_HangMuc gthangmuc ";
                sql += " LEFT JOIN NH_HDNK_CacQuyetDinh_ChiPhi_HangMuc cphangmuc ";
                sql += " ON gthangmuc.iID_CacQuyetDinh_HangMucID = cphangmuc.ID ";
                sql += " WHERE gthangmuc.iID_GoiThau_ChiPhiID  = @idChiPhi ";
                var parameters = new[]
                {
                    new SqlParameter("@idChiPhi", idChiPhi)
                };
                return ctx.FromSqlRaw<NhDaHangMucGoiThauQuery>(sql, parameters).ToList();
            }
        }

        public void AddOrUpdate(Guid chiPhiId, IEnumerable<NhDaGoiThauHangMuc> items)
        {
            if (!items.Any()) return;

            var listAdded = items.Where(s => s.IsAdded && !s.IsDeleted).ToList();
            if (!listAdded.IsEmpty())
            {
                foreach (var item in listAdded)
                {
                    item.IIdGoiThauChiPhiId = chiPhiId;
                }
                this.AddRange(listAdded);
            }

            var listModified = items.Where(s => s.IsModified && !s.IsAdded && !s.IsDeleted).ToList();
            if (!listModified.IsEmpty())
            {
                foreach (var item in listModified)
                {
                    item.IIdGoiThauChiPhiId = chiPhiId;
                }
                this.UpdateRange(listModified);
            }

            var listDeleted = items.Where(s => s.IsDeleted).ToList();
            if (!listDeleted.IsEmpty())
            {
                this.RemoveRange(listDeleted);
            }
        }

        public IEnumerable<NhDaGoiThauChiPhiHangMucQuery> FindGoiThauChiPhiByGoiThauId(Guid idGoiThauID)
        {
            using (var conn = _contextFactory.CreateDbContext())
            {
                string sql = @"SELECT 
                              gt.iID_GoiThauID,
                              cp.Id as iID_GoiThau_ChiPhiID,
                              cp.iID_DuToan_ChiPhiID,
                              dmcp.iID_ChiPhi,
                              dmcp.sMaChiPhi as SMaChiPhi,
                              dmcp.sTenChiPhi as STenChiPhi,
                              dmcp.sTenVietTat as STenChiPhiVietTat,
                              hm.iID_DuToan_HangMucID,
                              dthm.sMaOrder as SMaOrder,
                              dthm.sMaHangMuc as SMaHangMuc,
                              dthm.sTenHangMuc as STenHangMucDT,
                              (CASE
                              	WHEN hm.fTienGoiThau_USD is NULL THEN cp.fTienGoiThau_USD
                              	ELSE hm.fTienGoiThau_USD
                              END) as FTienGoiThauUsd,
                              (CASE
                              	WHEN hm.fTienGoiThau_VND is NULL THEN cp.fTienGoiThau_VND
                              	ELSE hm.fTienGoiThau_VND
                              END) as FTienGoiThauVnd,
                              (CASE
                              	WHEN hm.fTienGoiThau_EUR is NULL THEN cp.fTienGoiThau_EUR
                              	ELSE hm.fTienGoiThau_EUR
                              END) as FTienGoiThauEur,
                              (CASE
                              	WHEN hm.fTienGoiThau_NgoaiTeKhac is NULL THEN cp.fTienGoiThau_NgoaiTeKhac
                              	ELSE hm.fTienGoiThau_NgoaiTeKhac
                              END) as FTienGoiThauNgoaiTeKhac,
                              (CASE
                              	WHEN hdcphm.ID is NULL AND hm.iID_DuToan_HangMucID is null THEN hdcp.ID
                              	ELSE hdcphm.ID
                              END) as IIdHopDongChiPhiId,
							  (CASE
                              	WHEN hdcphm.ID is NULL AND hm.iID_DuToan_HangMucID is null  THEN hdcp.iID_HopDongID
                              	ELSE hdcphm.iID_HopDongID
                              END) as IIdHopDongId,
                               (CASE
                              	WHEN hdcphm.ID is NULL AND hm.iID_DuToan_HangMucID is null  THEN hdcp.iID_HopDongGoiThauNhaThauID
                              	ELSE hdcphm.iID_HopDongGoiThauNhaThauID
                              END) as IIdHopDongGoiThauNhaThauId,
                              hdhm.Id as IIdHopDongHangMucId,
		                      hm.iIDGoiThauCheck as IIDGoiThauCheck,
							  hm.iID_GoiThau_HangMucID,
		                      hm.iID_ParentID as IIdParentId,
							  cp.iID_ParentID as IIdParentChiPhiId
                              FROM NH_DA_GoiThau gt
                              INNER JOIN NH_DA_GoiThau_ChiPhi cp ON gt.iID_GoiThauID =   cp.iID_GoiThauID
                              lEFT JOIN NH_DA_GoiThau_HangMuc hm ON cp.Id = hm.iID_GoiThau_ChiPhiID
                              LEFT JOIN NH_DA_DuToan_ChiPhi dtcp ON cp.iID_DuToan_ChiPhiID = dtcp.ID
                              LEFT JOIN NH_DM_ChiPhi dmcp ON dtcp.iID_ChiPhiID = dmcp.iID_ChiPhi
                              LEFT JOIN NH_DA_DuToan_HangMuc dthm ON dthm.ID = hm.iID_DuToan_HangMucID
                              LEFT JOIN NH_DA_HopDong_ChiPhi hdcp ON hdcp.iID_GoiThau_ChiPhiID = cp.Id
                              LEFT JOIN NH_DA_HopDong_HangMuc hdhm ON hdhm.iID_GoiThau_HangMucID =   hm.iID_GoiThau_HangMucID
                              LEFT JOIN NH_DA_HopDong_ChiPhi hdcphm on hdhm.iID_HopDong_ChiPhiID = hdcphm.ID
                              where cp.iID_GoiThauID = @iID_GoiThauID";

                var parameters = new[]
                {
                    new SqlParameter("@iID_GoiThauID", idGoiThauID)
                };
                return conn.FromSqlRaw<NhDaGoiThauChiPhiHangMucQuery>(sql, parameters);
            }
        }
    }
}
