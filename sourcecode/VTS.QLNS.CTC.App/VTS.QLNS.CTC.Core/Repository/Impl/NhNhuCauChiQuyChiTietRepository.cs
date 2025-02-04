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
    public class NhNhuCauChiQuyChiTietRepository : Repository<NhNhuCauChiQuyChiTiet>, INhNhuCauChiQuyChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhNhuCauChiQuyChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhuCauChiQuyNhiemVuChiQuery> FindNhiemVuChiByIdDonVi(Guid? idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += "SELECT ";
                sql += " KHTTNhiemVuChi.ID AS Id, ";
                sql += " KHTTNhiemVuChi.iID_NhiemVuChiID AS IIdNhiemVuChiId, ";
                //sql += " KHTTNhiemVuChi.iID_DuAnID AS IIdDuAnId, ";
                sql += " DmNhiemVuChi.sTenNhiemVuChi AS STenNhiemVuChi ";
                sql += " FROM NH_KHTongThe_NhiemVuChi KHTTNhiemVuChi ";
                sql += " INNER JOIN NH_DM_NhiemVuChi DmNhiemVuChi ";
                sql += " ON KHTTNhiemVuChi.iID_NhiemVuChiID = DmNhiemVuChi.ID ";
                sql += " WHERE KHTTNhiemVuChi.iID_DonViThuHuongID = @idDonVi ";
                var parameters = new[]
                {
                    new SqlParameter("@idDonVi", idDonVi)
                };
                return ctx.FromSqlRaw<NhuCauChiQuyNhiemVuChiQuery>(sql, parameters).ToList();
            }
        }

        public NhNhuCauChiQuyChiTiet FindByIdHopDong(Guid? idHopDong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_nhucauchiquy_chitiet_byIDHopDong @idHopDong";
                var parameters = new[]
                {
                    new SqlParameter("@idHopDong", idHopDong)
                };
                return ctx.FromSqlRaw<NhNhuCauChiQuyChiTiet>(sql, parameters).FirstOrDefault();
            }
        }

        public IEnumerable<NhNhuCauChiQuyKinhPhiDaChiQuery> KinhPhiDaChi(Guid idHopDong, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += " SELECT thanhtoan.ID as Id, SUM(chitiet.fPheDuyetCapKyNay_USD) AS Usd, ";
                sql += "  SUM(chitiet.fPheDuyetCapKyNay_VND) AS Vnd, ";
                sql += "  SUM(chitiet.fPheDuyetCapKyNay_EUR) AS Eur, ";
                sql += "  Sum(chitiet.fPheDuyetCapKyNay_NgoaiTeKhac) AS NgoaiTe ";
                sql += " FROM NH_TT_ThanhToan thanhtoan INNER JOIN NH_TT_ThanhToan_ChiTiet chitiet ON thanhtoan.ID = chitiet.iID_DeNghiThanhToanID ";
                sql += " WHERE thanhtoan.iTrangThai = 2 ";
                sql += "  AND thanhtoan.iNamKeHoach = @nam ";
                sql += "  AND thanhtoan.iID_HopDongID = @idHopDong ";
                sql += " GROUP BY thanhtoan.ID ";
                var parameters = new[]
                {
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@idHopDong", idHopDong)
                };
                return ctx.FromSqlRaw<NhNhuCauChiQuyKinhPhiDaChiQuery>(sql, parameters).ToList();
            }
        }
    }
}
