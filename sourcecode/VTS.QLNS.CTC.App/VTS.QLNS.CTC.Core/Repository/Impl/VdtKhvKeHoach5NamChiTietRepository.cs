using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKhvKeHoach5NamChiTietRepository : Repository<VdtKhvKeHoach5NamChiTiet>, IVdtKhvKeHoach5NamChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvKeHoach5NamChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void CreateSettlementVoucherDetail(MidiumTermPlanCriteria creation)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_ke_hoach_5_nam_creation @iID_KeHoach5Nam, @iID_KeHoach5F";
                var parameters = new[]
                {
                    new SqlParameter("@iID_KeHoach5Nam", creation.VocherIDL),
                    new SqlParameter("@iID_KeHoach5F", creation.VocherIDF)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamChiTiet> FindByIdKeHoach5Nam(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoach5NamChiTiets.Where(x => x.IIdKeHoach5NamId == id).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamReportQuery> FindByReportKeHoachTrungHan(string id, string lct, int idNguonVon, int type, double donViTinh, string lstDonViThucHienDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_khv_kehoach_5_nam_duoc_duyet_export @Id, @lct, @IdNguonVon, @type, @MenhGiaTienTe, @lstDonViThucHienDuAn";
                var parameters = new[]
                {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@lct", lct),
                    new SqlParameter("@IdNguonVon", idNguonVon),
                    new SqlParameter("@type", type),
                    new SqlParameter("@MenhGiaTienTe", donViTinh),
                    new SqlParameter("@lstDonViThucHienDuAn", lstDonViThucHienDuAn)
                };
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamReportQuery>(sql, parameters).ToList();
            }

        }

        public IEnumerable<VdtKhvKeHoach5NamChiTietQuery> FindByKeHoach5NamChiTiet(string id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_kehoach5nam_chitiet @IdKh5nam";
                var parameters = new[]
                {
                    new SqlParameter("@IdKh5nam", id)
                };
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamChiTietQuery> FindChiTietDuAnChuyenTiep(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_kehoach5nam_chitiet_chuyentiep @Id";
                var parameters = new[]
                {
                    new SqlParameter("@Id", id.ToString())
                };
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamChuyenTiepReportQuery> FindByReportKeHoachTrungHanChuyenTiep(string lstId, string lstBudget, string lstUnit, int type, double donViTinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_kehoach5nam_chitiet_chuyentiep_report @lstId, @lstBudget, @lstUnit, @type, @DonViTienTe";
                var parameters = new[]
                {
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@lstBudget", lstBudget),
                    new SqlParameter("@lstUnit", lstUnit),
                    new SqlParameter("@type", type),
                    new SqlParameter("@DonViTienTe", donViTinh)
                };
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamChuyenTiepReportQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamExportQuery> GetDataExportKeHoachTrungHan(string id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_kehoachtrunghan_export @id";
                var parameters = new[]
                {
                    new SqlParameter("@id", id)
                };
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamExportQuery>(sql, parameters).ToList();
            }
        }
    }
}
