using VTS.QLNS.CTC.Core.Domain.Query;
﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaDuToanRepository : Repository<NhDaDuToan>, INhDaDuToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaDuToanRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var listDuToan = ctx.NhDaDuToans.Where(x => x.SSoQuyetDinh == soQuyetDinh && x.Id != id).ToList();

                if (listDuToan.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public void DeleteDuToanChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_dutoan_delete_dutoan @id";
                var parameters = new[]
                {
                    new SqlParameter("id", id)
                };
                ctx.Database.ExecuteSqlCommand(executeSql, parameters);
            }
        }

        public IEnumerable<NhDaDuToanQuery> FindIndex(int namLamViec, int iLoai)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_dutoan_index @YearOfWork, @ILoai";
                var parameters = new[]
                {
                    new SqlParameter("YearOfWork", namLamViec),
                    new SqlParameter("ILoai", iLoai)
                };
                return ctx.FromSqlRaw<NhDaDuToanQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaDuToan> FindDuToanByIdDonVi(string maDonVi, int iLoai)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "SELECT *, iID_KHTT_NhiemVuChiID AS IIdKHTTNhiemVuChiId FROM NH_DA_DUTOAN  " +
                    "WHERE iID_MaDonViQuanLy = @maDonVi AND iLoai = @iLoai";
                var parameters = new[]
                {
                    new SqlParameter("maDonVi", maDonVi),
                    new SqlParameter("iLoai", iLoai)
                };
                return ctx.FromSqlRaw<NhDaDuToan>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaDuToan> FindDuToanMoTaMSByIdDonVi(string maDonVi, int iLoai)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "SELECT *, iID_KHTT_NhiemVuChiID AS IIdKHTTNhiemVuChiId,iLoaiDuToan as IdLoaiDuToan, " +
                    "Case When iLoaiDuToan = 1 then Concat(N'Dự toán mua sắm: ',sSoQuyetDinh) " +
                    "When iLoaiDuToan = 2 then Concat(N'Dự toán đặt hàng: ',sSoQuyetDinh) else sSoQuyetDinh end as sSoQuyetDinh, iID_TiGiaID AS IIdTiGiaId  FROM NH_DA_DUTOAN  " +
                    "WHERE iID_MaDonViQuanLy = @maDonVi AND iLoai = @iLoai and bIsActive=1";
                var parameters = new[]
                {
                    new SqlParameter("maDonVi", maDonVi),
                    new SqlParameter("iLoai", iLoai)
                };
                return ctx.FromSqlRaw<NhDaDuToan>(executeSql, parameters).ToList();
            }
        }
        
        public IEnumerable<NhDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuAn(Guid duAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_dutoan_get_all_dutoan_chiphi_by_duan @duAnId";
                var parameters = new[]
                {
                    new SqlParameter("duAnId", duAnId)
                };
                return ctx.FromSqlRaw<NhDaDuToanChiPhiQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuToanId(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_dutoan_get_all_dutoan_chiphi_by_dutoan_id @duToanId";
                var parameters = new[]
                {
                    new SqlParameter("duToanId", duToanId)
                };
                return ctx.FromSqlRaw<NhDaDuToanChiPhiQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuAn(Guid duAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_dutoan_get_all_dutoan_nguonvon_by_duanid @duAnId";
                var parameters = new[]
                {
                    new SqlParameter("duAnId", duAnId)
                };
                return ctx.FromSqlRaw<NhDaDuToanNguonVonQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuToanId(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_dutoan_get_all_dutoan_nguonvon @duToanId";
                var parameters = new[]
                {
                    new SqlParameter("duToanId", duToanId)
                };
                return ctx.FromSqlRaw<NhDaDuToanNguonVonQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaDuToanHangMucQuery> FindListHangMucAllDetail(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_vdt_getall_dutoanchitiet_hangmuc_viewdetail @duToanId";
                var parameters = new[]
                {
                    new SqlParameter("duToanId", duToanId)
                };
                return ctx.FromSqlRaw<NhDaDuToanHangMucQuery>(executeSql, parameters).ToList();
            }
        }

        /// <summary>
        /// get list qddau tu hang muc by qddautu chi phi
        /// </summary>
        /// <param name="qdDauTuChiPhiId"></param>
        /// <returns></returns>
        public IEnumerable<NhDaDuToanHangMucQuery> GetQdDauTuHangMucByQdDautuChiPhiId(Guid qdDauTuChiPhiId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_dutoan_get_qddautu_hangmuc_by_qddautu_chiphi @qdDautuChiPhiId";
                var parameters = new[]
                {
                    new SqlParameter("qdDautuChiPhiId", qdDauTuChiPhiId)
                };
                return ctx.FromSqlRaw<NhDaDuToanHangMucQuery>(executeSql, parameters).ToList();
            }
        }

        /// <summary>
        /// get list du toan tu hang muc by du toan chi phi
        /// </summary>
        /// <param name="duToanChiPhiId"></param>
        /// <returns></returns>
        public IEnumerable<NhDaDuToanHangMucQuery> GetDuToanTuHangMucByDuToanChiPhiId(Guid duToanChiPhiId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_dutoan_get_dutoan_hangmuc_by_dutoan_chiphi @duToanChiPhiId";
                var parameters = new[]
                {
                    new SqlParameter("duToanChiPhiId", duToanChiPhiId)
                };
                return ctx.FromSqlRaw<NhDaDuToanHangMucQuery>(executeSql, parameters).ToList();
            }
        }
        
        public List<NhDaDuToan> FindDuAnInKhlcNhaThau(Guid iIdKhlcntId, Guid iIdDuAnID)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
             
                string executeSql = "EXECUTE sp_nh_dutoan_in_khlcnt @iId, @iIdDuAnId";
                var parameters = new[] {
                    new SqlParameter("@iId", iIdKhlcntId),
                    new SqlParameter("@iIdDuAnId", iIdDuAnID)
                };
                return ctx.FromSqlRaw<NhDaDuToan>(executeSql, parameters).ToList();
            }
        }


        public List<NhDaDuToan> FindDuAnInKhlcNhaThauID(Guid iIdDuAnID)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "select * from NH_DA_DUTOAN where iID_DuAnID=@iIdDuAnId";
                var parameters = new[] {
                    new SqlParameter("@iIdDuAnId", iIdDuAnID)
                };
                return ctx.Set<NhDaDuToan>().FromSql(sql, parameters).ToList();
            }
        }
    }
}
