using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhKhChiTietRepository : Repository<NhKhChiTiet>, INhKhChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhKhChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhKhChiTietQuery> FindAllNhkHChiTietHasSoKeHoachTTBQP()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += "SELECT DISTINCT ct.*,tt.sSoKeHoachBQP,tt.ID iID_KHTongTheID, nvc.iID_DonViThuHuongID FROM NH_KHChiTiet ct ";
                sql += "JOIN NH_KHChiTiet_HopDong hd ON ct.id = hd.iID_KHChiTietID ";
                sql += "JOIN NH_KHTongThe_NhiemVuChi nvc ON hd.iID_KHTongThe_NhiemVuChiID = nvc.ID ";
                sql += "JOIN NH_KHTongThe tt ON nvc.iID_KHTongTheID = tt.ID ";
                return ctx.FromSqlRaw<NhKhChiTietQuery>(sql).OrderByDescending(x => x.DNgayTao).ToList();
            }
        }
        public IEnumerable<NhKhChiTietQuery> FindByCondition(int namLamViec)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_khchitiet_index @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec)
                };
                return ctx.FromSqlRaw<NhKhChiTietQuery>(sql, parameters).ToList();
            }
        }
    }
}
