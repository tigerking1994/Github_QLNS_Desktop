using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDsCanBoNghiHuuKeHoachRepository : Repository<TlDsCanBoNghiHuuKeHoach>, ITlDsCanBoNghiHuuKeHoachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public TlDsCanBoNghiHuuKeHoachRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<TlDsCanBoNghiHuuKeHoachQuery> FinAllCanBoNghiHuuKeHoach()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<TlDsCanBoNghiHuuKeHoachQuery>("EXECUTE dbo.sp_tl_DM_CanBo_NghiHuu_KeHoach").ToList();
            }
        }
    }
}
