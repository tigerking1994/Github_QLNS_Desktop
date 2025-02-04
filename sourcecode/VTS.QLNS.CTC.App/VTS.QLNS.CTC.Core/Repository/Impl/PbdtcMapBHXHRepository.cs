using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class PbdtcMapBHXHRepository : Repository<BhdtcnpbMapBHXH>, IPbdtcMapBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public PbdtcMapBHXHRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhdtcnpbMapBHXH> FindByCondition(Expression<Func<BhdtcnpbMapBHXH, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhdtcnpbMapBHXHs.Where(predicate).ToList();
            }
        }

        public bool IsExistEstimate(Guid idNhanPhanBo, Guid idPhanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhdtcnpbMapBHXHs.Any(c => c.iID_BHDTC_NhanPhanBo == idNhanPhanBo && c.iID_BHDTC_PhanBo == idPhanBo);
            }
        }
        public IEnumerable<BhdtcnpbMapBHXH> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhdtcnpbMapBHXHs.Where(n => listIdNhanDuToan.Contains(n.iID_BHDTC_NhanPhanBo.ToString())).ToList();
            }
        }

    }
}
