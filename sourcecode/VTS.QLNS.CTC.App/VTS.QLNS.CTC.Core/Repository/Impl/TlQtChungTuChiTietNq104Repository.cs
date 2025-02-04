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

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlQtChungTuChiTietNq104Repository : Repository<TlQtChungTuChiTietNq104>, ITlQtChungTuChiTietNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlQtChungTuChiTietNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<TlQtChungTuChiTietNq104> FindByChungTuId(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQtChungTuChiTietsNq104.Where(x => x.IdChungTu.Equals(id)).ToList();
            }
        }

        public IEnumerable<TlQtChungTuChiTietNq104Query> FindByCondition(string maDonVi, int thang, int nam, string maCachTl)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_chungtu_chitiet_nq104 @maDonVi, @thang, @nam, @maCachTl";
                var parameters = new[]
                {
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maCachTl", maCachTl)
                };
                return ctx.FromSqlRaw<TlQtChungTuChiTietNq104Query>(sql, parameters).ToList();
            }
        }

        public int DeleteByChungTuId(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand($"DELETE FROM TL_QT_ChungTuChiTiet_Nq104 WHERE Id_ChungTu = '{id}'");
            }
        }

        public IEnumerable<TlQtChungTuChiTietNq104Query> FindByNamKeHoach(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_chungtu_chitiet_namkehoach_nq104 @maDonVi, @thang, @nam";
                var parameters = new[]
                {
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam)
                };
                return ctx.FromSqlRaw<TlQtChungTuChiTietNq104Query>(sql, parameters).ToList();
            }
        }

        public void AddAggregate(TlQuyetToanChiTietTongHopNq104Criteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_quyettoan_chungtu_chitiet_tao_tonghop_nq104 @ListIdChungTuTongHop, @IdChungTu, @IdDonVi, @TenDonVi, @NamLamViec, @NamNganSach, @NguonNganSach";
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

        public IEnumerable<ReportQttxTheoCachTinhLuongNq104Query> FindReportQttxTheoCachTinhLuongNq104(string maDonVi, int thang, int nam, string cachTinhLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.rpt_tl_qttx_chitiet_theo_cach_tinh_luong_nq104 @maDonVi, @thang, @nam, @cachTinhLuong";
                var parameters = new[]
                {
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@cachTinhLuong", cachTinhLuong)
                };
                return ctx.FromSqlRaw<ReportQttxTheoCachTinhLuongNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlQtChungTuChiTietNq104Query> GetDataChungTuChiTietNq104(string idChungTu, int nam, string cachTinhLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_data_ct_chi_tiet_nq104 @idChungTu, @nam, @maCachTl";
                var parameters = new[]
                {
                    //new SqlParameter("@idChungTu", "a1544f69-a428-4136-97d5-ba80a1eac5d0,f611a712-4cf8-40d0-93a9-2c60554263c0" ),
                    new SqlParameter("@idChungTu", idChungTu ),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maCachTl", cachTinhLuong)
                };
                return ctx.FromSqlRaw<TlQtChungTuChiTietNq104Query>(sql, parameters).ToList();
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

        public IEnumerable<TlQtChungTuChiTietNq104Query> GetDataChungTuChiTietNq104Export(string lstId, int nam, string maDonViTongHop, bool isSummary, string cachTL)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_data_ct_chi_tiet_export_nq104 @lstId, @lstCach, @nam, @isSummary, @maDonViTongHop";
                var parameters = new[]
                {
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@lstCach", cachTL),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@isSummary", isSummary),
                    new SqlParameter("@maDonViTongHop", maDonViTongHop),
                    
                };
                return ctx.FromSqlRaw<TlQtChungTuChiTietNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TlQtChungTuChiTietNq104Query> GetDataGiaiThichBangSoNq104(string lstId, int nam, string maDonViTongHop, bool isSummary, string cachTL)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_data_giaithich_bangso_export_nq104 @lstId, @lstCach, @nam, @isSummary, @maDonViTongHop";
                var parameters = new[]
                {
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@lstCach", cachTL),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@isSummary", isSummary),
                    new SqlParameter("@maDonViTongHop", maDonViTongHop),
                };
                return ctx.FromSqlRaw<TlQtChungTuChiTietNq104Query>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportQttxTheoCotNq104Query> GetDataChungTuChiTietTheoCotNq104(string idChungTu, int nam, string cachTinhLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_tl_get_data_ct_chi_tiet_theo_cot_nq104 @idChungTu, @nam, @maCachTl";
                var parameters = new[]
                {
                    new SqlParameter("@idChungTu", idChungTu),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maCachTl", cachTinhLuong)
                };
                return ctx.FromSqlRaw<ReportQttxTheoCotNq104Query>(sql, parameters).ToList();
            }
        }
    }
}
