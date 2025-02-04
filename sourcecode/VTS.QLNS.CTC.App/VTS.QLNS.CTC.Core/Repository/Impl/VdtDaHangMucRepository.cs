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
    public class VdtDaHangMucRepository : Repository<VdtDaDuAnHangMuc>, IVdtDaHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaHangMucRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteDuAnHangMucById(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnHangMucIdParam = new SqlParameter("@duAnHangMucId", id);
                return ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_delete_duanhangmuc @duAnHangMucId", duAnHangMucIdParam);
            }
        }

        public IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVon(Guid chuTruongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chuTruongIdParam = new SqlParameter("@chuTruongId", chuTruongId);
                return ctx.FromSqlRaw<VdtDaChuTruongDauTuNguonVonQuery>("EXECUTE dbo.sp_vdt_getall_chutruongchitiet_nguonvon @chuTruongId", chuTruongIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaHangMucQuery> FindListDAHangMucDetail(Guid duAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chuTruongIdParam = new SqlParameter("@duAnId", duAnId);
                return ctx.FromSqlRaw<VdtDaHangMucQuery>("EXECUTE dbo.sp_vdt_getall_chutruongchitiet_hangmuc @duAnId", chuTruongIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaHangMucQuery> FindListDAHangMucDetailAfterSaveChuTruong(Guid chuTruongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chuTruongIdParam = new SqlParameter("@chuTruongId", chuTruongId);
                return ctx.FromSqlRaw<VdtDaHangMucQuery>("EXECUTE dbo.sp_vdt_getall_chutruongchitiet_hangmuc_aftersave @chuTruongId", chuTruongIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaHangMucQuery> FindListChuTruongHangMucDieuChinhAdd(Guid chuTruongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chuTruongIdParam = new SqlParameter("@chuTruongId", chuTruongId);
                return ctx.FromSqlRaw<VdtDaHangMucQuery>("EXECUTE dbo.sp_vdt_getall_chutruong_hangmuc_dieuchinh_add @chuTruongId", chuTruongIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaHangMucQuery> FindListChuTruongHangMucDieuChinhUpdate(Guid chuTruongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chuTruongIdParam = new SqlParameter("@chuTruongId", chuTruongId);
                return ctx.FromSqlRaw<VdtDaHangMucQuery>("EXECUTE dbo.sp_vdt_getall_chutruong_hangmuc_dieuchinh_update @chuTruongId", chuTruongIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuAnHangMuc> GetAllHangMuc()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuAnHangMucs.ToList();
            }
        }
    }
}
