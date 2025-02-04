using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlQtChungTuChiTietRepository : Repository<TlQtChungTuChiTiet>, ITlQtChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlQtChungTuChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<TlQtChungTuChiTiet> FindByChungTuId(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQtChungTuChiTiets.Where(x => x.IdChungTu.Equals(id)).ToList();
            }
        }

        public IEnumerable<TlQtChungTuChiTietQuery> FindByCondition(string maDonVi, int thang, int nam, string maCachTl)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_chungtu_chitiet @maDonVi, @thang, @nam, @maCachTl";
                if (maCachTl == CachTinhLuong.CACH2) sql = "EXECUTE dbo.sp_tl_chungtu_chitiet_bhxh_cach2 @maDonVi, @thang, @nam, @maCachTl";
                else if (maCachTl == CachTinhLuong.CACH3) sql = "EXECUTE dbo.sp_tl_chungtu_chitiet_bhxh_cach3 @maDonVi, @thang, @nam, @maCachTl";
                else if (maCachTl == CachTinhLuong.CACH4) sql = "EXECUTE dbo.sp_tl_chungtu_chitiet_bhxh_cach4 @maDonVi, @thang, @nam, @maCachTl";
                else if (maCachTl == CachTinhLuong.CACH1) sql = "EXECUTE dbo.sp_tl_chungtu_chitiet_cach1 @maDonVi, @thang, @nam, @maCachTl";
                var parameters = new[]
                {
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maCachTl", maCachTl)
                };
                return ctx.FromSqlRaw<TlQtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public int DeleteByChungTuId(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand($"DELETE FROM TL_QT_ChungTuChiTiet WHERE Id_ChungTu = '{id}'");
            }
        }

        public IEnumerable<TlQtChungTuChiTietQuery> FindByNamKeHoach(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_chungtu_chitiet_namkehoach @maDonVi, @thang, @nam";
                var parameters = new[]
                {
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam)
                };
                return ctx.FromSqlRaw<TlQtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public void AddAggregate(TlQuyetToanChiTietTongHopCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_quyettoan_chungtu_chitiet_tao_tonghop @ListIdChungTuTongHop, @IdChungTu, @IdDonVi, @TenDonVi, @NamLamViec, @NamNganSach, @NguonNganSach";
                var parameters = new[]
                {
                    new SqlParameter("@ListIdChungTuTongHop", creation.ListIdChungTuTongHop),
                    new SqlParameter("@IdChungTu", creation.IdChungTu),
                    new SqlParameter("@IdDonVi", creation.IdDonVi),
                    new SqlParameter("@TenDonVi", creation.TenDonVi),
                    new SqlParameter("@NamLamViec", creation.NamLamViec),
                    new SqlParameter("@NamNganSach", creation.NamNganSach),
                    new SqlParameter("@NguonNganSach", creation.NguonNganSach)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<ReportQttxTheoCachTinhLuongQuery> FindReportQttxTheoCachTinhLuong(string maDonVi, int thang, int nam, string cachTinhLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.rpt_tl_qttx_chitiet_theo_cach_tinh_luong @maDonVi, @thang, @nam, @cachTinhLuong";
                var parameters = new[]
                {
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@cachTinhLuong", cachTinhLuong)
                };
                return ctx.FromSqlRaw<ReportQttxTheoCachTinhLuongQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlQtChungTuChiTietQuery> GetDataChungTuChiTiet(string idChungTu, int nam, string cachTinhLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_data_ct_chi_tiet @idChungTu, @nam, @maCachTl";
                var parameters = new[]
                {
                    //new SqlParameter("@idChungTu", "a1544f69-a428-4136-97d5-ba80a1eac5d0,f611a712-4cf8-40d0-93a9-2c60554263c0" ),
                    new SqlParameter("@idChungTu", idChungTu ),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maCachTl", cachTinhLuong)
                };
                return ctx.FromSqlRaw<TlQtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }
        
        public IEnumerable<MucLucCheckQuery> GetDataMucLucNG(string sXauNoiMa)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "SELECT ml.sDuToanChiTietToi FROM NS_MucLucNganSach as ml where ml.sDuToanChiTietToi = 'NG' and ml.sXauNoiMa = @sXauNoiMa";
                var parameters = new[]
                {
                    new SqlParameter("@sXauNoiMa", sXauNoiMa)
                };
                return ctx.FromSqlRaw<MucLucCheckQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlQtChungTuChiTietQuery> GetDataChungTuChiTietExport(string lstId, int nam, string maDonViTongHop, bool isSummary, string cachTL)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_data_ct_chi_tiet_export @lstId, @lstCach, @nam, @isSummary, @maDonViTongHop";
                var parameters = new[]
                {
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@lstCach", cachTL),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@isSummary", isSummary),
                    new SqlParameter("@maDonViTongHop", maDonViTongHop),
                    
                };
                return ctx.FromSqlRaw<TlQtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlQtChungTuChiTietQuery> GetDataGiaiThichBangSo(string lstId, int nam, string maDonViTongHop, bool isSummary, string cachTL)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_data_giaithich_bangso_export @lstId, @lstCach, @nam, @isSummary, @maDonViTongHop";
                var parameters = new[]
                {
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@lstCach", cachTL),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@isSummary", isSummary),
                    new SqlParameter("@maDonViTongHop", maDonViTongHop),
                };
                return ctx.FromSqlRaw<TlQtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportQttxTheoCotQuery> GetDataChungTuChiTietTheoCot(string idChungTu, int nam, string cachTinhLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_tl_get_data_ct_chi_tiet_theo_cot @idChungTu, @nam, @maCachTl";
                var parameters = new[]
                {
                    new SqlParameter("@idChungTu", idChungTu),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maCachTl", cachTinhLuong)
                };
                return ctx.FromSqlRaw<ReportQttxTheoCotQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlQtChungTuChiTietQuery> GetQuyetToanChiTietBHXH(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_quyet_toan_chitiet_bhxh @MaDonVi, @Thang, @Nam";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam)
                };
                return ctx.FromSqlRaw<TlQtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }
    }
}
