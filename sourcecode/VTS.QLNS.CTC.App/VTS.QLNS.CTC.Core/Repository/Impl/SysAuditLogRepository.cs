using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SysAuditLogRepository : Repository<HtNhatKyCapNhatDuLieu>, ISysAuditLogRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SysAuditLogRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
