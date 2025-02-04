using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCapBacNq104Repository : Repository<TlDmCapBacNq104>, ITlDmCapBacNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCapBacNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCapBacNq104 FindByMaCapBac(string maCapBac)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacNq104s.Where(x => x.MaCb == maCapBac).FirstOrDefault();
            }
        }

        public IEnumerable<TlDmCapBacNq104> FindByNote()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacNq104s.Where(x => x.Note != "").OrderBy(x => x.MaCb).ToList();
            }
        }

        public IEnumerable<RptGiayGTTaiChinhLoaiNhomQuery> FindByTenLoaiAndTenNhom(int nam, int thang, string maCanBo, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_tl_find_tenloai_tennhom_canbo_nq104 @thang, @nam, @maDonVi, @maCanBo";
                var parameters = new[]
                {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@maCanBo", maCanBo)
                };
                return ctx.FromSqlRaw<RptGiayGTTaiChinhLoaiNhomQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<TlDmCapBacNq104> FindParent()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacNq104s.Where(x => string.IsNullOrEmpty(x.Parent)).ToList();
            }
        }

        public void UpdateCanBoPhuCapWhenChangeCapBac(int iThang, int iNam, List<Guid> lstIdCapBac, bool bIsDelete, string sMaPhuCapChange)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_tl_update_dmcapbac_canbo @nam, @thang, @bIsDelete, @capbacIds, @sMaPhuCapChange";

                DataTable dt = DBExtension.ConvertDataToGuidTable(lstIdCapBac);
                SqlParameter dtDetailParam = new SqlParameter("capbacIds", SqlDbType.Structured);
                dtDetailParam.TypeName = "t_tbl_uniqueidentifier";
                dtDetailParam.Value = dt;
                var parameters = new[]
                {
                    new SqlParameter("nam", iNam),
                    new SqlParameter("thang", iThang),
                    new SqlParameter("bIsDelete", bIsDelete),
                    dtDetailParam,
                    new SqlParameter("sMaPhuCapChange", sMaPhuCapChange),
                };

                ctx.Database.ExecuteSqlCommand(executeQuery, parameters);
            }
        }
    }
}
