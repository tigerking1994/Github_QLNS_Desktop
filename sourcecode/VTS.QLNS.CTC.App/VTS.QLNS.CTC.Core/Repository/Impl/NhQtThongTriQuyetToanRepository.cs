using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhQtThongTriQuyetToanRepository : Repository<NhQtThongTriQuyetToan>, INhQtThongTriQuyetToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhQtThongTriQuyetToanRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhQtThongTriQuyetToanQuery> GetAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_qt_thong_tri_quyet_toan_index";
                return ctx.FromSqlRaw<NhQtThongTriQuyetToanQuery>(executeSql, new { });
            }
        }

        public IEnumerable<LookupQuery<Guid, string>> GetLookupNhiemVuChi()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = @"SELECT DISTINCT NVC.ID AS Id, DM_NVC.sTenNhiemVuChi AS DisplayName
                    FROM NH_KHTongThe_NhiemVuChi NVC
                    LEFT JOIN NH_DM_NhiemVuChi DM_NVC ON NVC.iID_NhiemVuChiID = DM_NVC.ID";
                return ctx.FromSqlRaw<LookupQuery<Guid, string>>(executeSql, new { });
            }
        }

        public IEnumerable<LookupQuery<Guid, string>> GetLookupNhiemVuChiByDonVi(Guid iID_DonViID)
        {
            if (iID_DonViID == null || iID_DonViID == Guid.Empty)
            {
                return null;
            }

            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = @"SELECT DISTINCT DM_NVC.ID AS Id, DM_NVC.sTenNhiemVuChi AS DisplayName
                    FROM NH_KHTongThe_NhiemVuChi NVC
                    LEFT JOIN NH_DM_NhiemVuChi DM_NVC ON NVC.iID_NhiemVuChiID = DM_NVC.ID
                    WHERE iID_DonViThuHuongID = @id";
                var parameters = new[]
                {
                    new SqlParameter("@id", iID_DonViID)
                };
                return ctx.FromSqlRaw<LookupQuery<Guid, string>>(executeSql, parameters);
            }
        }

        public NhQtThongTriQuyetToanQuery GetThongTriById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = @"SELECT TTQT.*, DM_NVC.sTenNhiemVuChi, CONCAT(DV.iID_MaDonVi, IIF(DV.sTenDonVi IS NULL OR DV.sTenDonVi = '', '', CONCAT(' - ', DV.sTenDonVi))) AS sTenDonVi
                    FROM NH_QT_ThongTriQuyetToan TTQT
                    LEFT JOIN NH_KHTongThe_NhiemVuChi NVC ON TTQT.iID_KHTT_NhiemVuChiID = NVC.ID
					LEFT JOIN NH_DM_NhiemVuChi DM_NVC ON NVC.iID_NhiemVuChiID = DM_NVC.ID
                    LEFT JOIN DonVi DV ON TTQT.iID_DonViID = DV.iID_DonVi AND TTQT.iID_MaDonVi = DV.iID_MaDonVi
					WHERE TTQT.ID = @Id";
                var parameters = new[]
                {
                    new SqlParameter("@Id", id)
                };
                return ctx.FromSqlRaw<NhQtThongTriQuyetToanQuery>(executeSql, parameters).FirstOrDefault();
            }
        }

        public Guid SaveAndGetIdThongTriQuyetToan(NhQtThongTriQuyetToan input)
        {
            using var ctx = _contextFactory.CreateDbContext();
            try
            {
                ctx.NhQtThongTriQuyetToans.Add(input);
                ctx.SaveChanges();
                ctx.Dispose();
                return input.Id;
            }
            catch (Exception ex)
            {
                ctx.Dispose();
                return Guid.Empty;
            }
        }
    }
}