using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmDonViNq104Repository : Repository<TlDmDonViNq104>, ITlDmDonViNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmDonViNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmDonViNq104 FindByMaDonVi(string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmDonViNq104s.Where(x => x.MaDonVi == maDonVi).FirstOrDefault();
            }
        }

        public IEnumerable<TlDmDonViNq104> FindAllDonVi()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmDonViNq104s.Include(x => x.TlDmCanBoNq104s).Include(x => x.TlDsCapNhapBangLuongsNq104).ToList();
            }
        }

        public IEnumerable<TlDmDonViNq104> FindAllDonViNq104()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmDonViNq104s.Include(x => x.TlDmCanBoNq104s).Include(x => x.TlDsCapNhapBangLuongsNq104).ToList();
            }
        }

        public IEnumerable<TlDmDonViNq104> FindAllDonViQuanSo(int? thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += "SELECT * FROM TL_DM_DonVi_NQ104 ";
                sql += "WHERE ";
                sql += "    Ma_DonVi IN (SELECT Parent FROM TL_DM_CanBo_NQ104 WHERE (@Thang IS NULL OR Thang = @Thang) AND Nam = @Nam) ";
                sql += "    AND Ma_DonVi NOT IN (SELECT Ma_DonVi FROM TL_QS_ChungTu_NQ104 WHERE (@Thang IS NULL OR Thang = @Thang) AND Nam = @Nam)";
                var parameters = new[]
                {
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@Thang", thang == null ? DBNull.Value : (object)thang)
                };
                return ctx.Set<TlDmDonViNq104>().FromSql(sql, parameters).OrderBy(x => x.XauNoiMa).ToList();
            }
        }

        public IEnumerable<TlDmDonViNq104> FindAllDonViQuanSoNam(int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += "SELECT * FROM TL_DM_DonVi_NQ104 ";
                sql += "WHERE ";
                sql += "    Ma_DonVi IN (SELECT Parent FROM TL_DM_CanBo_NQ104 WHERE Nam = @Nam) ";
                sql += "    AND Ma_DonVi NOT IN (SELECT Ma_DonVi FROM TL_QS_ChungTu_NQ104 WHERE Nam = @Nam)";
                var parameters = new[]
                {
                    new SqlParameter("@Nam", nam)
                };
                return ctx.Set<TlDmDonViNq104>().FromSql(sql, parameters).OrderBy(x => x.XauNoiMa).ToList();
            }
        }

        public IEnumerable<TlDmDonViNq104> FindDonViTaoBangLuong(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_get_donvi_tao_bangluong_nq104 @nam, @thang, @cachTinhluong";
                if (isThuNopBhxh)
                    sql = "sp_tl_get_donvi_tao_QuanLyThuNop_bhxh @nam, @thang, @cachTinhluong";
                if (isNew)
                    sql = "EXECUTE sp_tl_get_donvi_tao_bangluong_nq104 @nam, @thang, @cachTinhluong";
                var parameters = new[]
                {
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@cachTinhLuong", cachTinhLuong)
                };
                return ctx.Set<TlDmDonViNq104>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmDonViNq104> FindDonViPhuCap(int nam, int thang, string cachTinhLuong, bool isNew = false)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_get_donvi_bang_phucap_nq104 @nam, @thang, @cachTinhluong";
                var parameters = new[]
                {
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@cachTinhLuong", cachTinhLuong)
                };
                return ctx.Set<TlDmDonViNq104>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmDonViNq104> FindDonViBangLuongThang(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_get_donvi_bangluong_thang_nq104 @nam, @thang, @cachTinhluong";
                if (isThuNopBhxh)
                    sql = "EXECUTE sp_tl_get_donvi_thu_nop_bhxh @nam, @thang, @cachTinhluong";

                var parameters = new[]
                {
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@cachTinhLuong", cachTinhLuong)
                };
                return ctx.Set<TlDmDonViNq104>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmDonViNq104> FindAllDonViBaoCao()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmDonViNq104s.Include(x => x.TlDsCapNhapBangLuongsNq104).Include(x => x.TlQtChungTusNq104).OrderBy(x => x.XauNoiMa).ToList();
            }
        }

        public IEnumerable<TlDmDonViNq104> FindAllDonViBaoCaoNq104()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmDonViNq104s.Include(x => x.TlDsCapNhapBangLuongsNq104).Include(x => x.TlQtChungTusNq104).OrderBy(x => x.XauNoiMa).ToList();
            }
        }

        public IEnumerable<TlDmDonViNq104> FindDonViBaoCaoQuanSo(int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_get_donvi_baocao_quanso_nq104 @nam";
                var parameters = new[]
                {
                    new SqlParameter("@nam", nam)
                };
                return ctx.Set<TlDmDonViNq104>().FromSql(sql, parameters).ToList();
            }
        }
    }
}
