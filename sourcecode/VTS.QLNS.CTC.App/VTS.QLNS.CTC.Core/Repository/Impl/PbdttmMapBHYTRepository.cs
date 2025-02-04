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

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class PbdttmMapBHYTRepository : Repository<BhPbdttmMapBHYT>, IPbdttmMapBHYTRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public PbdttmMapBHYTRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhPbdttmMapBHYT> FindByCondition(Expression<Func<BhPbdttmMapBHYT, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdttmMapBHYTs.Where(predicate).ToList();
            }
        }

        public bool IsExistEstimate(Guid idNhanPhanBo, Guid idPhanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdttmMapBHYTs.Any(c => c.IID_DTTM_BHYT_NhanPhanBo == idNhanPhanBo && c.IID_DTTM_BHYT_PhanBo == idPhanBo);
            }
        }

        public IEnumerable<BhPbdttmMapBHYT> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdttmMapBHYTs.Where(n => listIdNhanDuToan.Contains(n.IID_DTTM_BHYT_NhanPhanBo.ToString())).ToList();
            }
        }

    }
}
