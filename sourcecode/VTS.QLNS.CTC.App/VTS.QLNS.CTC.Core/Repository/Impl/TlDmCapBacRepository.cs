using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCapBacRepository : Repository<TlDmCapBac>, ITlDmCapBacRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCapBacRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCapBac FindByMaCapBac(string maCapBac)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacs.Where(x => x.MaCb == maCapBac).FirstOrDefault();
            }
        }

        public IEnumerable<TlDmCapBac> FindByNote()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacs.Where(x => x.Note != "").OrderBy(x => x.MaCb).ToList();
            }
        }

        public IEnumerable<TlDmCapBac> FindParent()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacs.Where(x => string.IsNullOrEmpty(x.Parent)).ToList();
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
