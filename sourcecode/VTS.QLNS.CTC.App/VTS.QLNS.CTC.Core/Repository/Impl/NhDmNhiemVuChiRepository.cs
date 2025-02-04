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
    public class NhDmNhiemVuChiRepository : Repository<NhDmNhiemVuChi>, INhDmNhiemVuChiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmNhiemVuChiRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDmNhiemVuChi> FindAllByIdNhKhTongThe(Guid idNhKhTongThe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idKhTongTheParam = new SqlParameter("@IdKhTongThe", idNhKhTongThe);
                string sql = "select NH_DM_NhiemVuChi.* from NH_DM_NhiemVuChi " +
                    "join NH_KHTongThe_NhiemVuChi " +
                    "on NH_KHTongThe_NhiemVuChi.iID_NhiemVuChiID = NH_DM_NhiemVuChi.ID " +
                    "join NH_KHTongThe on NH_KHTongThe.ID = NH_KHTongThe_NhiemVuChi.iID_KHTongTheID " +
                    "where NH_KHTongThe.ID = @IdKhTongThe";
                return ctx.Set<NhDmNhiemVuChi>().FromSql(sql, idKhTongTheParam).ToList();
            }
        }

        public IEnumerable<NhDmNhiemVuChiQuery> FindTreeByIds(IEnumerable<Guid> ids)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_nhiemvuchi_findtree_by_ids @Ids";
                var parameters = new object[]
                {
                    new SqlParameter("@Ids", string.Join(",", ids.Select(x => x.ToString())))
                };

                return ctx.FromSqlRaw<NhDmNhiemVuChiQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDmNhiemVuChiQuery> FindByKhTongTheIdAndDonViId(Guid khTongTheId, Guid donViId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_nhiemvuchi_bykehoachtongthe_donvi @IdKhTongThe, @IdDonVi";
                var parameters = new object[]
                {
                    new SqlParameter("@IdKhTongThe", khTongTheId),
                    new SqlParameter("@IdDonVi", donViId)
                };
                
                return ctx.FromSqlRaw<NhDmNhiemVuChiQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDmNhiemVuChiQuery> FindAllFillter(Guid donViId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new object[]
               {
                    new SqlParameter("@IdDonVi", donViId),
                    new SqlParameter("@GuidEmpty", Guid.Empty)
               };
                string sql = "select nvc.ID as Id, " +
                    "nvc.sMaNhiemVuChi as SMaNhiemVuChi, " +
                    "nvc.sTenNhiemVuChi as STenNhiemVuChi, " +
                    "nvc.sMotaChiTiet as SMoTaChiTiet, " +
                    "nvc.iLoaiNhiemVuChi as ILoaiNhiemVuChi, " +
                    "nvc.sMaOrder as SMaOrder, " +
                    "nvc.iID_ParentID as IIdParentId, " +
                    "khtt.ID as IIdKHTTNhiemVuChiId " +
                    "from NH_DM_NhiemVuChi nvc " +
                    "join NH_KHTongThe_NhiemVuChi khtt " +
                    "on khtt.iID_NhiemVuChiID = nvc.ID " +
                    "where (khtt.iID_DonViThuHuongID = @IdDonVi or @IdDonVi is null or @IdDonVi = @GuidEmpty)";
                return ctx.FromSqlRaw<NhDmNhiemVuChiQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NhDmNhiemVuChiQuery> FindByDonViId(Guid donViId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", donViId);
                string sql = "select nvc.ID as Id, " +
                    "nvc.sMaNhiemVuChi as SMaNhiemVuChi, " +
                    "CAST( ROW_NUMBER() OVER(ORDER By nvc.sMaNhiemVuChi) AS nvarchar(max)) as STT, " +
                    "nvc.sTenNhiemVuChi as STenNhiemVuChi, " +
                    "nvc.sMotaChiTiet as SMoTaChiTiet, " +
                    "nvc.iLoaiNhiemVuChi as ILoaiNhiemVuChi, " +
                    "nvc.sMaOrder as SMaOrder, " +
                    "nvc.iID_ParentID as IIdParentId, " +
                    "khtt.ID as IIdKHTTNhiemVuChiId, " +
                    "nvcParent.sMaNhiemVuChi as SMaNhiemVuChiParent, " +
                    "khtt.fGiaTriKH_BQP as FKeHoachBqpUsd, " +
                    "khtt.fGiaTriKH_BQP_VND as FKeHoachBqpVnd " +
                    "from NH_DM_NhiemVuChi nvc " +
                    "join NH_KHTongThe_NhiemVuChi khtt " +
                    "on khtt.iID_NhiemVuChiID = nvc.ID " +
                    "LEFT join NH_DM_NhiemVuChi nvcParent " +
                    "on nvcParent.ID = nvc.iID_ParentID ";
                if (!donViId.IsNullOrEmpty())
                {
                    sql += " where khtt.iID_DonViThuHuongID = @IdDonVi";

                }
                return ctx.FromSqlRaw<NhDmNhiemVuChiQuery>(sql, idDonViParam).ToList();
            }
        }

        public IEnumerable<NhDmNhiemVuChiQuery> FindNhiemVuChiDuToanByDonViId(Guid idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"
                    SELECT 
	                    C.ID AS Id,
	                    C.iID_ParentID AS IIdParentId, 
	                    C.iLoaiNhiemVuChi AS ILoaiNhiemVuChi, 
	                    C.sMaNhiemVuChi AS SMaNhiemVuChi, 
	                    C.sMaOrder AS SMaOrder, 
	                    C.sMoTaChiTiet AS SMoTaChiTiet, 
	                    C.sTenNhiemVuChi AS STenNhiemVuChi,
	                    B.ID AS IIdKHTTNhiemVuChiId 
                    FROM NH_DA_DuToan A
                    LEFT JOIN NH_KHTongThe_NhiemVuChi B ON A.iID_KHTT_NhiemVuChiID = B.ID
                    LEFT JOIN NH_DM_NhiemVuChi C ON B.iID_NhiemVuChiID = C.ID
                    WHERE A.iID_DonViQuanLyID = @idDonVi";
                var param = new SqlParameter("@idDonVi", idDonVi);

                return ctx.FromSqlRaw<NhDmNhiemVuChiQuery>(sql, param).ToList();
            }
        }

        public IEnumerable<NhDmNhiemVuChiQuery> FindNhiemVuChiDuToanByDuToanId(Guid idDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"
                    SELECT 
	                    C.ID AS Id,
	                    C.iID_ParentID AS IIdParentId, 
	                    C.iLoaiNhiemVuChi AS ILoaiNhiemVuChi, 
	                    C.sMaNhiemVuChi AS SMaNhiemVuChi, 
	                    C.sMaOrder AS SMaOrder, 
	                    C.sMoTaChiTiet AS SMoTaChiTiet, 
	                    C.sTenNhiemVuChi AS STenNhiemVuChi,
	                    B.ID AS IIdKHTTNhiemVuChiId 
                    FROM NH_DA_DuToan A
                    LEFT JOIN NH_KHTongThe_NhiemVuChi B ON A.iID_KHTT_NhiemVuChiID = B.ID
                    LEFT JOIN NH_DM_NhiemVuChi C ON B.iID_NhiemVuChiID = C.ID
                    WHERE A.ID = @idDuToan";
                var param = new SqlParameter("@idDuToan", idDuToan);

                return ctx.FromSqlRaw<NhDmNhiemVuChiQuery>(sql, param).ToList();
            }
        }
    }
}
