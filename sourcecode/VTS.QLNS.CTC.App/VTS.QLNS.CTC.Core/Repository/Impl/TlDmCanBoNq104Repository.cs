using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCanBoNq104Repository : Repository<TlDmCanBoNq104>, ITlDmCanBoNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCanBoNq104Repository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCanBoNq104 FindByMaCanbo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.Where(x => x.MaCanBo == maCanBo).FirstOrDefault();
            }
        }

        public TlDmCanBoNq104 FindByMaHieuCanbo(string maHieuCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.Where(x => x.MaHieuCanBo == maHieuCanBo).FirstOrDefault();
            }
        }

        public IEnumerable<TlDmCanBoNq104> FindByMonth(int month)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.Where(x => x.Thang == month).ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104> FindByMonthAndDonVi(int month, string parent)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.Where(x => x.Thang == month && x.Parent == parent).ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104> FindByConditionInsurance(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.Where(x => x.Parent == maDonVi && x.Thang == thang && x.Nam == nam).ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104Query> FindCanBoQuyetToanQuanSo(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_find_canbo_quyettoan_quanso_nq104 @maDonVi, @thang, @nam";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<TlDmCanBoNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104Query> FindCanBoQuyetToanQuanSoGiam(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_find_canbo_quyettoan_quanso_giam_nq104 @maDonVi, @thang, @nam";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<TlDmCanBoNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104> FindByMaHieuCanBo(string maHieuCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.Where(x => x.MaHieuCanBo == maHieuCanBo).ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104> FindLoadIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.Where(x => x.IsDelete == true).OrderBy(x => x.Parent)
                    .ThenByDescending(x => x.MaCv).ThenByDescending(x => x.MaCb).ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104> FindUpdateMultiCanBo()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.Include(x => x.TlCanBoPhuCapsNq104)
                    .Where(x => x.IsDelete == true && x.IsLock == false).OrderByDescending(x => x.MaHieuCanBo).ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104> FindUpdateMultiCanBoNq104()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.Include(x => x.TlCanBoPhuCapsNq104)
                    .Where(x => x.IsDelete == true && x.IsLock == false).OrderByDescending(x => x.MaHieuCanBo).ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104> FindCanBoXoa()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s
                    .Where(x => x.IsDelete.HasValue && !x.IsDelete.Value)
                    .OrderBy(x => x.Parent)
                    .ThenByDescending(x => int.Parse(x.MaHieuCanBo)).ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104> FindAllCanBo()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104> FindByMaCanboIn(List<string> MaCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.Where(t => MaCanBo.Contains(t.MaCb104))
                    .OrderByDescending(t => t.MaCanBo)
                    .ThenByDescending(t => t.MaCb).ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104Query> FindCanBoDieuChinh(int? thang, int? nam, string maDonVi, string maCapBac, decimal? hskv, string maTangGiam, string maChucVu, decimal? tienAn, DateTime? ngayNhapNgu, bool isHsq)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_find_canbo_dieuchinh_nq104 @thang, @nam, @maDonVi, @maCapBac, @hskv, @maTangGiam, @maChucVu, @tienAn, @ngayNhapNgu, @isHsq";
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
                return ctx.FromSqlRaw<TlDmCanBoNq104Query>(sql, parameters).ToList();
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
                string sql = "dbo.sp_tl_rpt_chitiet_quanso_tanggiam_nq104";
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
                string sql = "EXECUTE dbo.sp_tl_find_danhsach_canbo_nq104";
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
                string sql = "EXECUTE dbo.sp_tl_find_danhsach_canbo_by_condition_nq104 @thang, @nam, @maDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<TlDanhSachCanBoQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmCanBoNq104> FindByMonthYear(int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoNq104s.Where(x => x.Thang == thang && x.Nam == nam).ToList();
            }
        }
    }
}
