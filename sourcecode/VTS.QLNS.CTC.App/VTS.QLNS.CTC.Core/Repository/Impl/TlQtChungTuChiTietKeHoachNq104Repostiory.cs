using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlQtChungTuChiTietKeHoachNq104Repostiory : Repository<TlQtChungTuChiTietKeHoachNq104>, ITlQtChungTuChiTietKeHoachNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlQtChungTuChiTietKeHoachNq104Repostiory(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByNamAndMaDonVi(string maDonVi, int Nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand($"DELETE FROM TL_QT_ChungTuChiTiet_KeHoach_Nq104 WHERE Id_DonVi = '{maDonVi}' and NamLamViec = {Nam}");
            }
        }

        public IEnumerable<TlQtChungTuChiTietKeHoachNq104Query> GetDataByMonth(string maDonVi, int thang, int nam, int pcMlnsNam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<TlQtChungTuChiTietKeHoachNq104Query>("EXECUTE dbo.sp_tl_chungtu_chitiet_kehoach_nq104 @maDonVi, @thang, @nam, @pcMlnsNam",
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@pcMlnsNam", pcMlnsNam)).ToList();
            }
        }

        public IEnumerable<ReportChiTietCanBoKeHoachNq104Query> GetDataChiTietCanBoKeHoach(int namKh, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ReportChiTietCanBoKeHoachNq104Query>("EXECUTE dbo.sp_tl_rpt_kehoach_chitiet_canbo_nq104 @NamKeHoach, @MaDonVi",
                    new SqlParameter("@NamKeHoach", namKh),
                    new SqlParameter("@MaDonVi", maDonVi)).ToList();
            }
        }

        public IEnumerable<TlChungTuChiTietKeHoachNq104Query> GetDataChungTuChiTiet(int namNs, int nam, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<TlChungTuChiTietKeHoachNq104Query>("EXECUTE dbo.sp_tl_get_data_ct_chitiet_kehoach_nq104 @namNs, @nam, @idDonVi",
                    new SqlParameter("@namNs", namNs),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@idDonVi", idDonVi)).ToList();
            }
        }
    }
}
