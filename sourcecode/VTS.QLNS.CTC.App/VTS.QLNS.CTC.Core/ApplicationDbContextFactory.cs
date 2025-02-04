using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core
{
    public class ApplicationDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public ApplicationDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public ApplicationDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>();
            _configureDbContext(options);

            return new ApplicationDbContext(options.Options);
        }

    }
}
