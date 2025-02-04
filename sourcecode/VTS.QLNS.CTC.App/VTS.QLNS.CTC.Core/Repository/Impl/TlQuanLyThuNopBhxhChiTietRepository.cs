using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlQuanLyThuNopBhxhChiTietRepository : Repository<TlQuanLyThuNopBhxhChiTiet>, ITlQuanLyThuNopBhxhChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public TlQuanLyThuNopBhxhChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public DataTable GetDataQlThuNopBhxhDetails(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_quanly_thunop_bhxh_chitiet";
                var parameters = new[]
                {
                    new SqlParameter("@Id", id)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }

        public void CreateSummaryDetails(Guid iIdParent, string lstidChungTus, string idMaDonVi, int iNamLamViec, int iThang)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_insert_qlthunop_bhxh_chitiet_tonghop @iIdParent, @ListIdChungTus, @MaDonVi, @NamLamViec, @Thang";
                var parameters = new[]
                {
                    new SqlParameter("@iIdParent", iIdParent),
                    new SqlParameter("@ListIdChungTus", lstidChungTus),
                    new SqlParameter("@MaDonVi", idMaDonVi),
                    new SqlParameter("@NamLamViec", iNamLamViec),
                    new SqlParameter("@Thang", iThang)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public DataTable ReportThuNopBhxhTongHopTheoDonVi(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong)
        {
            {
                using (var ctx = _contextFactory.CreateDbContext())
                {
                    string sql = "dbo.sp_tl_rpt_thu_nop_bhxh_TongHop_theoDonvi";
                    var parameters = new[]
                    {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                    new SqlParameter("@IsGiaTriAm", isGiaTriAm),
                    new SqlParameter("@IsCheckedMaHuongLuong", isCheckedMaHuongLuong)
                };
                    return ctx.FromSqlCommand(sql, parameters);
                }
            }
        }

        public DataTable GetDataReportThuNopBhxh(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isTongHop, bool isCheckedMaHuongLuong, bool isInCanBoMoi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "dbo.sp_tl_rpt_bangthanhtoan_thunop_bhxh";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IsOrderChucVu", isOrderChucVu),
                    new SqlParameter("@IsTongHop", isTongHop),
                    new SqlParameter("@IsCheckedMaHuongLuong", isCheckedMaHuongLuong),
                    new SqlParameter("@IsNew", isInCanBoMoi)
                };
                return ctx.FromSqlCommand(sql, parameters);
            }
        }
    }
}
