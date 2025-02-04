using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmDonViRepository : Repository<TlDmDonVi>, ITlDmDonViRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmDonViRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmDonVi FindByMaDonVi(string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmDonVis.Where(x => x.MaDonVi == maDonVi).FirstOrDefault();
            }
        }

        public IEnumerable<TlDmDonVi> FindAllDonVi()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmDonVis.Include(x => x.TlDmCanBos).Include(x => x.TlDsCapNhapBangLuongs).ToList();
            }
        }

        public IEnumerable<TlDmDonVi> FindAllDonViNq104()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmDonVis.Include(x => x.TlDmCanBos).Include(x => x.TlDsCapNhapBangLuongs).ToList();
            }
        }

        public IEnumerable<TlDmDonVi> FindAllDonViQuanSo(int? thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += "SELECT  * FROM TL_DM_DonVi ";
                sql += "WHERE ";
                sql += "    Ma_DonVi IN (SELECT Parent FROM TL_DM_CanBo WHERE (@Thang IS NULL OR Thang = @Thang) AND Nam = @Nam) ";
                sql += "    AND Ma_DonVi NOT IN (SELECT Ma_DonVi FROM TL_QS_ChungTu WHERE (@Thang IS NULL OR Thang = @Thang) AND Nam = @Nam)";
                var parameters = new[]
                {
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@Thang", thang == null ? DBNull.Value : (object)thang)
                };
                return ctx.Set<TlDmDonVi>().FromSql(sql, parameters).OrderBy(x => x.XauNoiMa).ToList();
            }
        }

        public IEnumerable<TlDmDonVi> FindAllDonViQuanSoNam(int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_get_donvi_quanso_nam @nam";
                var parameters = new[]
                {
                    new SqlParameter("@nam", nam)
                };
                return ctx.Set<TlDmDonVi>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmDonVi> FindDonViTaoBangLuong(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_get_donvi_tao_bangluong @nam, @thang, @cachTinhluong";
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
                return ctx.Set<TlDmDonVi>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmDonVi> FindDonViTaoBangLuongBHXH(int nam, int thang, string cachTinhLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_get_donvi_tao_bangluong_bhxh @nam, @thang, @cachTinhluong";
                var parameters = new[]
                {
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@cachTinhLuong", cachTinhLuong)
                };
                return ctx.Set<TlDmDonVi>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmDonVi> FindDonViPhuCap(int nam, int thang, string cachTinhLuong, bool isNew = false)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_get_donvi_bang_phucap @nam, @thang, @cachTinhluong";
                if (isNew)
                    sql = "EXECUTE sp_tl_get_donvi_bang_phucap_nq104 @nam, @thang, @cachTinhluong";
                var parameters = new[]
                {
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@cachTinhLuong", cachTinhLuong)
                };
                return ctx.Set<TlDmDonVi>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmDonVi> FindDonViBangLuongThang(int nam, int thang, string cachTinhLuong, bool isThuNopBhxh = false, bool isNew = false)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_get_donvi_bangluong_thang @nam, @thang, @cachTinhluong";
                if (isThuNopBhxh)
                    sql = "EXECUTE sp_tl_get_donvi_thu_nop_bhxh @nam, @thang, @cachTinhluong";
                if (isNew)
                    sql = "EXECUTE sp_tl_get_donvi_bangluong_thang_nq104 @nam, @thang, @cachTinhluong";

                var parameters = new[]
                {
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@cachTinhLuong", cachTinhLuong)
                };
                return ctx.Set<TlDmDonVi>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlDmDonVi> FindAllDonViBaoCao()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmDonVis.Include(x => x.TlDsCapNhapBangLuongs).Include(x => x.TlQtChungTus).OrderBy(x => x.XauNoiMa).ToList();
            }
        }

        public IEnumerable<TlDmDonVi> FindAllDonViBaoCaoNq104()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmDonVis.Include(x => x.TlDsCapNhapBangLuongs).Include(x => x.TlQtChungTusNq104).OrderBy(x => x.XauNoiMa).ToList();
            }
        }

        public IEnumerable<TlDmDonVi> FindDonViBaoCaoQuanSo(int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_get_donvi_baocao_quanso @nam";
                var parameters = new[]
                {
                    new SqlParameter("@nam", nam)
                };
                return ctx.Set<TlDmDonVi>().FromSql(sql, parameters).ToList();
            }
        }
    }
}
