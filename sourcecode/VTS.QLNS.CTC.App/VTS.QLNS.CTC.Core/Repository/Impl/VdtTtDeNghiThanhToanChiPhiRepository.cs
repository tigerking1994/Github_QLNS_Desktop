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
    public class VdtTtDeNghiThanhToanChiPhiRepository : Repository<VdtTtDeNghiThanhToanChiPhi>, IVdtTtDeNghiThanhToanChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtTtDeNghiThanhToanChiPhiRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtTtDeNghiThanhToanChiPhiIndexQuery> GetDeNghiThanhToanChiPhiIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_vdt_tt_denghithanhtoanchiphi_index";
                return ctx.FromSqlRaw<VdtTtDeNghiThanhToanChiPhiIndexQuery>(executeSql).ToList();
            }
        }
    }
}
