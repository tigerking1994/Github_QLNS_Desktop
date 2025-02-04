using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCapBacLuongNq104Repository : Repository<TlDmCapBacLuongNq104>, ITlDmCapBacLuongNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCapBacLuongNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmCapBacLuongNq104 FindByMaCapBac(string maCapBac, int? nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacLuongNq104s.Where(x => x.MaDm == maCapBac && x.Nam == nam).FirstOrDefault();
            }
        }

        public IEnumerable<TlDmCapBacLuongNq104> FindByNote()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacLuongNq104s.OrderBy(x => x.MaDm).ToList();
            }
        }

        public TlDmCapBacLuongNq104 FindByXauNoiMa(string xauNoiMa, int? nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacLuongNq104s.Where(x => x.XauNoiMa == xauNoiMa && x.Nam == nam).FirstOrDefault();
            }
        }

        public IEnumerable<TlDmCapBacLuongNq104> FindParent()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacLuongNq104s.ToList();
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

        public IEnumerable<TlDmCapBacLuongNq104> FindAllByXauNoiMa(string xauNoiMa, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCapBacLuongNq104s.Where(x => x.XauNoiMa.StartsWith(xauNoiMa) && x.Nam == nam).OrderBy(x => x.XauNoiMa).ToList();
            }
        }
    }
}
