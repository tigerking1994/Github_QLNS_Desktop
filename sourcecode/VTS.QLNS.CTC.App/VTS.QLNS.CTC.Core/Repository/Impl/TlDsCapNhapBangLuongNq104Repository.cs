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
    public class TlDsCapNhapBangLuongNq104Repository : Repository<TlDsCapNhapBangLuongNq104>, ITlDsCapNhapBangLuongNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDsCapNhapBangLuongNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDsCapNhapBangLuongNq104 FindByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCapNhapBangLuongsNq104.Where(x => x.MaCachTl == maCachTinhLuong && x.MaCbo == maDonVi && x.Thang == thang && x.Nam == nam).FirstOrDefault();
            }
        }

        public TlDsCapNhapBangLuongNq104 FindByConditionStatus(string maCachTinhLuong, string maDonVi, int thang, int nam, bool status)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCapNhapBangLuongsNq104.FirstOrDefault(x => x.MaCachTl == maCachTinhLuong && x.MaCbo == maDonVi && x.Thang == thang && x.Nam == nam && x.Status == status);
            }
        }

        public IEnumerable<TlDsCapNhapBangLuongNq104> FindByMaCachTinhluong(string maCachTinhLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCapNhapBangLuongsNq104.Where(x => x.MaCachTl == maCachTinhLuong).ToList();
            }
        }

        public IEnumerable<TlDsCapNhapBangLuongNq104> FindByMonth(int month)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCapNhapBangLuongsNq104.Where(x => x.Thang == month).ToList();
            }
        }

        public int DeleteBangLuong(int thang, int nam, string maDonVi, string maCachTl)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_delete_bang_luong_nq104 @thang, @nam, @maDonVi, @maCachTl";
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

        public IEnumerable<TlDsCapNhapBangLuongNq104> FindBangLuongThangByNam(int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsCapNhapBangLuongsNq104.Include(x => x.TlDmDonViNq104).Where(x => x.Nam == nam && !true.Equals(x.IsTongHop) && x.MaCachTl.Equals(CachTinhLuong.CACH0)).OrderBy(x => x.MaCbo).ToList();
            }
        }

        public int DeleteCapNhatBangLuong(string idBangLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_delete_capnhat_nq104 @idDsbangLuong";
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
                var executeQuery = "EXECUTE dbo.sp_tl_update_bangluongthang_hsqcs_nq104 @iThang, @inam, @iIdMaDonVi";
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

        public IEnumerable<TlDsCapNhapBangLuongNq104> FindHaveDataByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"
                                SELECT 
                                		[Id]
                                      ,[Den_Ngay] DenNgay
                                      ,[IsTongHop] IsTongHop
                                      ,[KhoaBangLuong] KhoaBangLuong
                                      ,[Loai_DS_CNBLuong] LoaiDsCnbluong
                                      ,[Ma_CachTL] MaCachTl
                                      ,[Ma_CBo] MaCbo
                                      ,[Ma_PBan] MaPban
                                      ,[Nam] Nam
                                      ,[NgayTao_BL] NgayTaoBL
                                      ,[NguoiTao] NguoiTao
                                      ,[Note] Note
                                      ,[So_TT] SoTt
                                      ,[Status] Status
                                      ,[Ten_DS_CNBLuong] TenDsCnbluong
                                      ,[Thang] Thang
                                      ,[Tu_Ngay] TuNgay
                                FROM TL_DS_CapNhap_BangLuong_NQ104
                                WHERE 
                                id in (SELECT parent FROM TL_BangLuong_Thang_NQ104)
                                AND (@Thang IS NULL OR @Thang = 0 OR Thang = @Thang)
                                AND (@Nam IS NULL OR @Nam = 0 OR Nam = @Nam)
                                AND Ma_CachTL = @MaCachTinhLuong
                                AND (@MaDonVi  IS NULL OR @MaDonVi = '' OR Ma_CBo = @MaDonVi)";
                var parameters = new[]
                {
                    new SqlParameter("@MaCachTinhLuong", maCachTinhLuong),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@MaDonVi", maDonVi),
                };
                return ctx.FromSqlRaw<TlDsCapNhapBangLuongNq104>(sql, parameters).ToList();
            }
        }
    }

}
