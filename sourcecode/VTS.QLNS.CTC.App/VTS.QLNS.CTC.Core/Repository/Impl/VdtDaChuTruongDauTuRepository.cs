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
    public class VdtDaChuTruongDauTuRepository : Repository<VdtDaChuTruongDauTu>, IVdtDaChuTruongDauTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaChuTruongDauTuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool CheckDuAnExistQDDauTu(Guid duAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                List<VdtDaQddauTu> listQdDauTu = ctx.VdtDaQddauTus.Where(x => x.IIdDuAnId == duAnId).ToList();
                if (listQdDauTu != null && listQdDauTu.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                List<VdtDaChuTruongDauTu> listChuTruongDauTu = new List<VdtDaChuTruongDauTu>();
                if (id != Guid.Empty)
                {
                    listChuTruongDauTu = ctx.VdtDaChuTruongDauTus.Where(x => x.SSoQuyetDinh == soQuyetDinh && x.Id != id).ToList();
                }
                else
                {
                    listChuTruongDauTu = ctx.VdtDaChuTruongDauTus.Where(x => x.SSoQuyetDinh == soQuyetDinh).ToList();
                }

                if (listChuTruongDauTu.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public void DeleteChuTruongDauTu(Guid id, Guid? parentId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_delete_chutruongdautu @id, @parentId";
                var parameters = new[]
                {
                    new SqlParameter("@id", id),
                    new SqlParameter("@parentId", parentId.IsNullOrEmpty() ? DBNull.Value : (object)parentId)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public VdtDaChuTruongDauTu FindByDuAnId(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaChuTruongDauTus.Where(x => x.IIdDuAnId == id && x.BActive).FirstOrDefault();
            }
        }

        public IEnumerable<ChuTruongDauTuQuery> FindByCondition(int namLamViec, string sUserLogin)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_chutruongdautu_index @YearOfWork, @UserName";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@UserName", sUserLogin)
                };
                return ctx.FromSqlRaw<ChuTruongDauTuQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ChuTruongDauTuQuery> FindByConditionUserLogin(string sUserLogin)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_chutruongdautu_index_update @UserName";
                var parameters = new[]
                {
                    new SqlParameter("@UserName", sUserLogin)
                };
                return ctx.FromSqlRaw<ChuTruongDauTuQuery>(sql, parameters).ToList();
            }
        }

        public ChuTruongDauTuQuery FindChuTruongById(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_chutruongdautu_by_id @id";
                var parameters = new[]
                {
                    new SqlParameter("@id", id)
                };
                return ctx.FromSqlRaw<ChuTruongDauTuQuery>(sql, parameters).FirstOrDefault();
            }
        }

        public IEnumerable<VdtDaDuAn> FindDuAnNotExistsInChuTruongDT(Guid chuTruongDT, string maDonVi, int namLV)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_duan_notexits_chutruongdautu @IdChuTruongDauTu, @MaDonVi, @NamLV";
                var parameters = new[]
                {
                    new SqlParameter("@IdChuTruongDauTu", chuTruongDT),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@NamLV", namLV)
                };
                return ctx.Set<VdtDaDuAn>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongDauTuNguonVonByDuAn(Guid duAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_get_all_duan_nguonvon_by_duan @duAnId";
                var parameters = new[]
                {
                    new SqlParameter("@duAnId", duAnId)
                };
                return ctx.FromSqlRaw<VdtDaChuTruongDauTuNguonVonQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVonDetail(Guid chuTruongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_chutruongdautu_nguonvon @IdChuTruongDauTu";
                var parameters = new[]
                {
                    new SqlParameter("@IdChuTruongDauTu", chuTruongId)
                };
                return ctx.FromSqlRaw<VdtDaChuTruongDauTuNguonVonQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVonDieuChinhAdd(Guid chuTruongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_chutruongdautu_nguonvon_dieuchinh_add @IdChuTruongDauTu";
                var parameters = new[]
                {
                    new SqlParameter("@IdChuTruongDauTu", chuTruongId)
                };
                return ctx.FromSqlRaw<VdtDaChuTruongDauTuNguonVonQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVonDieuChinhUpdate(Guid chuTruongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_chutruongdautu_nguonvon_dieuchinh_update @IdChuTruongDauTu";
                var parameters = new[]
                {
                    new SqlParameter("@IdChuTruongDauTu", chuTruongId)
                };
                return ctx.FromSqlRaw<VdtDaChuTruongDauTuNguonVonQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ChuTruongDauTuDetailQuery> FindListDetail(Guid chuTruongDT)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_chutruongdautuchitiet @chutruongDauTuId";
                var parameters = new[]
                {
                    new SqlParameter("@chutruongDauTuId", chuTruongDT)
                };
                return ctx.FromSqlRaw<ChuTruongDauTuDetailQuery>(sql, parameters).ToList();
            }
        }

        public VdtDaChuTruongDauTu FindCTDTDieuChinhByDuAn(Guid id, Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaChuTruongDauTus.Where(x => x.IIdDuAnId == duAnId && x.BActive && x.Id != id).FirstOrDefault();
            }
        }

        public IEnumerable<VdtDaDuToanQuery> GetChuTruongDauTuByDuAnInKhlcNhaThauScreen(Guid iIdDuAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeSql = "EXECUTE sp_vdt_khlcnt_get_chutruongdautu_by_duan @iIdDuAnId";
                var parameters = new[]
                {
                    new SqlParameter("@iIdDuAnId", iIdDuAnId)
                };
                return ctx.FromSqlRaw<VdtDaDuToanQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanQuery> GetChuTruongDauTuByIdInKhlcNhaThauScreen(Guid iIdChuTruongDauTuId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeSql = "EXECUTE sp_vdt_khlcnt_get_chutruongdautu_by_id @iIdChuTruongDauTu";
                var parameters = new[]
                {
                    new SqlParameter("@iIdChuTruongDauTu", iIdChuTruongDauTuId)
                };
                return ctx.FromSqlRaw<VdtDaDuToanQuery>(executeSql, parameters).ToList();
            }
        }

        public void DeleteChuTruongDauTuHangMuc(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_delete_chutruongdautu_hangmuc @id";
                var parameters = new[]
                {
                    new SqlParameter("@id", id)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
