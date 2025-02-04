using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class ImpCpChungTuChiTietRepository : Repository<ImpCpChungTuChiTiet>, IImpCpChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public ImpCpChungTuChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
