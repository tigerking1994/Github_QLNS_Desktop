using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class ErrorDatabaseLogRepository : Repository<ErrorDatabaseLog>, IErrorDatabaseLogRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public ErrorDatabaseLogRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
