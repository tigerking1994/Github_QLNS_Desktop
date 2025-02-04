using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCanBoRepository : Repository<TlDmCanBo>, ITlDmCanBoRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCanBoRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCanBo FindByMaCanbo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Include(x => x.TlDmCapBac).Where(x => x.MaCanBo == maCanBo).FirstOrDefault();
            }
        }

        public TlDmCanBo FindByMaHieuCanbo(string maHieuCanBo, string maCb)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Include(x => x.TlDmCapBac).Where(x => x.MaHieuCanBo == maHieuCanBo && x.MaCb == maCb).FirstOrDefault();
            }
        }

        public IEnumerable<TlDmCanBo> FindByMonth(int month)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Where(x => x.Thang == month).ToList();
            }
        }

        public IEnumerable<TlDmCanBo> FindByMonthAndDonVi(int month, string parent)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Where(x => x.Thang == month && x.Parent == parent).ToList();
            }
        }

        public IEnumerable<TlDmCanBo> FindByConditionInsurance(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Where(x => x.Parent == maDonVi && x.Thang == thang && x.Nam == nam).ToList();
            }
        }

        public IEnumerable<TlDmCanBoQuery> FindCanBoQuyetToanQuanSo(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_find_canbo_quyettoan_quanso @maDonVi, @thang, @nam";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<TlDmCanBoQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmCanBoQuery> FindCanBoQuyetToanQuanSoGiam(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_find_canbo_quyettoan_quanso_giam @maDonVi, @thang, @nam";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<TlDmCanBoQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmCanBo> FindByMaHieuCanBo(string maHieuCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Where(x => x.MaHieuCanBo == maHieuCanBo).ToList();
            }
        }

        public IEnumerable<TlDmCanBo> FindLoadIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Include(x => x.TlDmCapBac).Include(x => x.TlDmChucVu).Where(x => x.IsDelete == true).OrderBy(x => x.Parent)
                    .ThenByDescending(x => x.MaCv).ThenByDescending(x => x.MaCb).ToList();
            }
        }

        public IEnumerable<TlDmCanBo> FindUpdateMultiCanBo()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Include(x => x.TlCanBoPhuCaps).Include(x => x.TlDmCapBac).Include(x => x.TlDmChucVu)
                    .Where(x => x.IsDelete == true && x.IsLock == false).OrderByDescending(x => x.MaHieuCanBo).ToList();
            }
        }

        public IEnumerable<TlDmCanBo> FindUpdateMultiCanBoNq104()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Include(x => x.TlCanBoPhuCaps).Include(x => x.TlDmCapBac).Include(x => x.TlDmChucVu)
                    .Where(x => x.IsDelete == true && x.IsLock == false).OrderByDescending(x => x.MaHieuCanBo).ToList();
            }
        }

        public IEnumerable<TlDmCanBo> FindCanBoXoa()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Include(x => x.TlDmCapBac).Include(x => x.TlDmChucVu)
                    .Where(x => x.IsDelete.HasValue && !x.IsDelete.Value)
                    .OrderBy(x => x.Parent)
                    .ThenByDescending(x => int.Parse(x.MaHieuCanBo)).ToList();
            }
        }

        public IEnumerable<TlDmCanBo> FindAllCanBo()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Include(x => x.TlDmCapBac).Include(x => x.TlDmChucVu).ToList();
            }
        }

        public IEnumerable<TlDmCanBo> FindByMaCanboIn(List<string> MaCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBos.Include(x => x.TlDmChucVu).Where(t => MaCanBo.Contains(t.MaCanBo))
                    .OrderByDescending(t => t.MaCanBo)
                    .ThenByDescending(t => t.TlDmChucVu == null ? 0 : t.TlDmChucVu.HeSoCv)
                    .ThenByDescending(t => t.MaCb).ToList();
            }
        }

        public IEnumerable<TlDmCanBoQuery> FindCanBoDieuChinh(int? thang, int? nam, string maDonVi, string maCapBac, decimal? hskv, string maTangGiam, string maChucVu, decimal? tienAn, DateTime? ngayNhapNgu, bool isHsq)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_find_canbo_dieuchinh @thang, @nam, @maDonVi, @maCapBac, @hskv, @maTangGiam, @maChucVu, @tienAn, @ngayNhapNgu, @isHsq";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@maCapBac", maCapBac),
                    new SqlParameter("@hskv", hskv),
                    new SqlParameter("@maTangGIam", maTangGiam),
                    new SqlParameter("@maChucVu", maChucVu),
                    new SqlParameter("@tienAn", tienAn),
                    new SqlParameter("@ngayNhapNgu", ngayNhapNgu),
                    new SqlParameter("@isHsq", isHsq)
                };
                return ctx.FromSqlRaw<TlDmCanBoQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlCanBoThueTncnQuery> FindCanBoThueTncn(bool isNew = false)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_data_thue_tncn";
                if (isNew)
                    sql = "EXECUTE dbo.sp_tl_get_data_thue_tncn_nq104";
                return ctx.FromSqlRaw<TlCanBoThueTncnQuery>(sql).ToList();
            }
        }

        public IEnumerable<TlCanBoRaQuanQuery> FindCanBoRaQuan(bool isNew = false)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_data_ra_quan";
                if (isNew)
                    sql = "EXECUTE dbo.sp_tl_get_data_ra_quan_nq104";
                return ctx.FromSqlRaw<TlCanBoRaQuanQuery>(sql).ToList();
            }
        }

        public DataTable ReportChiTietQsTangGiam(string maDonVi, int thang, int nam, string sM, int thangTruoc, int namTruoc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_chitiet_quanso_tanggiam";
                var parameter = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@thangTruoc", thangTruoc),
                    new SqlParameter("@namTruoc", namTruoc),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@sM", sM)
                };
                return ctx.FromSqlCommand(sql, parameter);
            }
        }

        public IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBo()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_find_danhsach_canbo";
                return ctx.FromSqlRaw<TlDanhSachCanBoQuery>(sql).ToList();
            }
        }

        public IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBoNq104()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_find_danhsach_canbo_nq104";
                return ctx.FromSqlRaw<TlDanhSachCanBoQuery>(sql).ToList();
            }
        }

        public IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBoByCondition(int thang, int nam, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_find_danhsach_canbo_by_condition @thang, @nam, @maDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<TlDanhSachCanBoQuery>(sql, parameters).ToList();
            }
        }
    }
}
