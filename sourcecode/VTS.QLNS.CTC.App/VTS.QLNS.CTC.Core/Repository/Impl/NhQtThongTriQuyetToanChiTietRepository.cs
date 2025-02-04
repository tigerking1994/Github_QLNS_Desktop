using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhQtThongTriQuyetToanChiTietRepository : Repository<NhQtThongTriQuyetToanChiTiet>, INhQtThongTriQuyetToanChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhQtThongTriQuyetToanChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<NhQtThongTriQuyetToanChiTietQuery> GetCreateThongTriChiTiet(int iNamThongTri, Guid iID_DonViID, Guid iID_KHTT_NhiemVuChiID)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_get_create_thongtriquyettoan_chitiet @Id_DonVi, @Id_NhiemVuChi, @NamThongTri";
                var parameters = new[]
                {
                    new SqlParameter("@Id_DonVi", iID_DonViID),
                    new SqlParameter("@Id_NhiemVuChi", iID_KHTT_NhiemVuChiID),
                    new SqlParameter("@NamThongTri", iNamThongTri)
                };
                return ctx.FromSqlRaw<NhQtThongTriQuyetToanChiTietQuery>(executeSql, parameters);
            }
        }

        public IEnumerable<NhQtThongTriQuyetToanChiTietQuery> GetThongTriChiTietByTTQTId(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_get_thongtriquyettoan_chitiet @Id_ThongTriQuyetToanID";
                var parameters = new[]
                {
                    new SqlParameter("@Id_ThongTriQuyetToanID", id)
                };
                return ctx.FromSqlRaw<NhQtThongTriQuyetToanChiTietQuery>(executeSql, parameters);
            }
        }
    }
}
