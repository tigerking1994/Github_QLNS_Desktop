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
    public class VdtNcNhuCauChiRepository : Repository<VdtNcNhuCauChi>, IVdtNcNhuCauChiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtNcNhuCauChiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtNcNhuCauChiQuery> GetNhuCauChiIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtNcNhuCauChiQuery>("EXECUTE dbo.sp_vdt_get_nhucauchiquy_index").ToList();
            }
        }

        public IEnumerable<VdtNcNhuCauChiChiTietQuery> GetNhuCauChiDetail(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon, int iQuy, int? DonviTinh = null)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
				string sql = "EXECUTE dbo.sp_vdt_get_nhucauchiquy_detail @iIdMaDonVi, @iNamKeHoach, @iIdNguonVon, @iQuy,@DonviTinh";
				var parameters = new[]
                {
                    new SqlParameter("iIdMaDonVi", iIdMaDonVi),
                    new SqlParameter("iNamKeHoach", iNamKeHoach),
                    new SqlParameter("iIdNguonVon", iIdNguonVon),
                    new SqlParameter("iQuy", iQuy),
                    new SqlParameter("DonviTinh", DonviTinh)
                };
                return ctx.FromSqlRaw<VdtNcNhuCauChiChiTietQuery>(sql, parameters).ToList();
            }
        }

        public KinhPhiCucTaiChinhCapQuery GetKinhPhiCucTaiChinhCap(int iNamKeHoach, string iIdMaDonVi, int iIdNguonVon, int iQuy)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
				string sql = "EXECUTE dbo.get_vdt_nc_sokinhphicuctaichinhcap_chiquy @iNamKeHoach, @iIdMaDonVi, @iIdNguonVon, @iQuy";
				var parameters = new[]
                {
                    new SqlParameter("iNamKeHoach", iNamKeHoach),
                    new SqlParameter("iIdMaDonVi", iIdMaDonVi),
                    new SqlParameter("iIdNguonVon", iIdNguonVon),
                    new SqlParameter("iQuy", iQuy)
                };
                var data = ctx.FromSqlRaw<KinhPhiCucTaiChinhCapQuery>(sql, parameters).ToList();
                if (data == null) return new KinhPhiCucTaiChinhCapQuery();
                return data.FirstOrDefault();
            }
        }

        public bool IsExistSoDeNghi(VdtNcNhuCauChi dataInsert)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtNcNhuCauChis.Any(n => 
                        n.SSoDeNghi == dataInsert.SSoDeNghi
                    && (dataInsert.Id == Guid.Empty || n.Id != dataInsert.Id));
            }
        }
    }
}
