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
    public class NhTtThongTriCapPhatChiTietRepository : Repository<NhTtThongTriCapPhatChiTiet>, INhTtThongTriCapPhatChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhTtThongTriCapPhatChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhTtThongTriCapPhatChiTietQuery> FindAllChiTiet()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_nh_thongtricapphat_dspheduyetthanhtoan";
                return ctx.FromSqlRaw<NhTtThongTriCapPhatChiTietQuery>(sql).ToList();
            }
        }
    }
}
