using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDsCapNhapBangLuongRepository : Repository<TlDsCapNhapBangLuong>, ITlDsCapNhapBangLuongRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDsCapNhapBangLuongRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDsCapNhapBangLuong FindByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCapNhapBangLuongs.Where(x => x.MaCachTl == maCachTinhLuong && x.MaCbo == maDonVi && x.Thang == thang && x.Nam == nam).FirstOrDefault();
            }
        }

        public TlDsCapNhapBangLuong FindByConditionStatus(string maCachTinhLuong, string maDonVi, int thang, int nam, bool status)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCapNhapBangLuongs.FirstOrDefault(x => x.MaCachTl == maCachTinhLuong && x.MaCbo == maDonVi && x.Thang == thang && x.Nam == nam && x.Status == status);
            }
        }

        public IEnumerable<TlDsCapNhapBangLuong> FindByMaCachTinhluong(string maCachTinhLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCapNhapBangLuongs.Where(x => x.MaCachTl == maCachTinhLuong).ToList();
            }
        }

        public IEnumerable<TlDsCapNhapBangLuong> FindByMonth(int month)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCapNhapBangLuongs.Where(x => x.Thang == month).ToList();
            }
        }

        public int DeleteBangLuong(int thang, int nam, string maDonVi, string maCachTl)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_delete_bang_luong @thang, @nam, @maDonVi, @maCachTl";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@maCachTl", maCachTl)
                };
                return ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public int DeleteBangLuongTruyThu(int thang, int nam, string maDonVi, string maCachTl)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_delete_bang_luong_truy_thu @thang, @nam, @maDonVi, @maCachTl";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@maCachTl", maCachTl)
                };
                return ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<TlDsCapNhapBangLuong> FindBangLuongThangByNam(int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCapNhapBangLuongs.Include(x => x.TlDmDonVi).Where(x => x.Nam == nam && !true.Equals(x.IsTongHop) && x.MaCachTl.Equals(CachTinhLuong.CACH0)).OrderBy(x => x.MaCbo).ToList();
            }
        }

        public int DeleteCapNhatBangLuong(string idBangLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_delete_capnhat @idDsbangLuong";
                var parameters = new[]
                {
                    new SqlParameter("@idDsbangLuong", idBangLuong)
                };
                return ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void UpdateBangLuongBhxhTheoCapBac(int iThang, int iNam, List<string> lstMaDonVi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_tl_update_bangluongthang_hsqcs @iThang, @inam, @iIdMaDonVi";
                DataTable dt = DBExtension.ConvertDataToStringTable(lstMaDonVi);
                SqlParameter dtDetailParam = new SqlParameter("iIdMaDonVi", SqlDbType.Structured);
                dtDetailParam.TypeName = "t_tbl_string";
                dtDetailParam.Value = dt;
                var parameters = new[]
                {
                    new SqlParameter("@iThang",iThang),
                    new SqlParameter("@inam",iNam),
                    new SqlParameter("@iIdMaDonVi", string.Join(",", lstMaDonVi))
                };
                ctx.Database.ExecuteSqlCommand(executeQuery, parameters);
            }
        }

        public void CreateSummaryVoucher(Guid idParent, string lstidChungTus, string idMaDonVi, string donViTongHop, decimal NamLamViec, decimal Thang)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_tao_bang_Luong_bhxh_tonghop @IdParent, @ListIdChungTus, @MaDonVi, @MaDonViTongHop, @NamLamViec, @Thang";
                var parameters = new[]
                {
                     new SqlParameter("@IdParent", idParent),
                    new SqlParameter("@ListIdChungTus", lstidChungTus),
                    new SqlParameter("@MaDonVi", idMaDonVi),
                    new SqlParameter("@MaDonViTongHop", donViTongHop),
                    new SqlParameter("@NamLamViec", NamLamViec),
                    new SqlParameter("@Thang", Thang)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
