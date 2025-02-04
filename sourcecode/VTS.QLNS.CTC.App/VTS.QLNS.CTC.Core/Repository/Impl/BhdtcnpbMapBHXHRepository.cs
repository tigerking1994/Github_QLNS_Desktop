using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhdtcnpbMapBHXHRepository :Repository<BhdtcnpbMapBHXH>, IBhdtcnpbMapBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhdtcnpbMapBHXHRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhdtcnpbMapBHXH> FindByIdNhanDuToan(Guid idNhanDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhdtcnpbMapBHXHs.Where(n => n.iID_BHDTC_NhanPhanBo == idNhanDuToan).ToList();
            }
        }
    }
}
