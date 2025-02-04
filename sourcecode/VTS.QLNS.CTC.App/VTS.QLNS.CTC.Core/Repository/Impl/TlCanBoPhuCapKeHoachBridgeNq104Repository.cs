using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlCanBoPhuCapKeHoachBridgeNq104Repository : Repository<TlCanBoPhuCapKeHoachBridgeNq104>, ITlCanBoPhuCapKeHoachBridgeNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlCanBoPhuCapKeHoachBridgeNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [TL_CanBo_PhuCap_KeHoach_Bridge_NQ104]");
            }
        }
    }
}
