using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using System.Data.Common;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class AppVersionRepository : Repository<HtAppVersion>, IAppVersionRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public AppVersionRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<AppVersionQuery> FindAllVersion()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "Select ID, VERSION, DESCRIPTION, null as FILESTREAM, STATUS, FILESIZE, FILENAME, CREATEDDATE, UPDATEDDATE FROM HT_APP_VERSION";
                return ctx.FromSqlRaw<AppVersionQuery>(sql).ToList();
            }
        }

        public void UpdateVersion(HtAppVersion appVersion)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                HtAppVersion entity = ctx.HtAppVersions.FirstOrDefault(t => t.Id.Equals(appVersion.Id));
                ctx.Entry(entity).CurrentValues.SetValues(appVersion);
                ctx.SaveChanges();
            }
        }

        public void UpdateVersion(AppVersionQuery selectedAppVersion, IEnumerable<AppVersionQuery> appVersions)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string updateToOldVersionSql = "Update HT_APP_VERSION set status = 2 where status = 1";
                string updateCurrentVersionSql = "Update HT_APP_VERSION set status = 1 where Id = @id";
                var id = new SqlParameter("@id", selectedAppVersion.Id);
                ctx.Database.ExecuteSqlCommand(updateToOldVersionSql);
                ctx.Database.ExecuteSqlCommand(updateCurrentVersionSql, id);
            }
        }

        public AppVersionQuery FindCurrentVersion()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "Select ID, VERSION, DESCRIPTION, null as FILESTREAM, STATUS, FILESIZE, FILENAME, CREATEDDATE, UPDATEDDATE FROM HT_APP_VERSION WHERE STATUS = 1";
                return ctx.FromSqlRaw<AppVersionQuery>(sql).FirstOrDefault();
            }
        }

        public void DeleteVersion(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "DELETE FROM HT_APP_VERSION WHERE Id = @id";
                var parameters = new[]
                {
                    new SqlParameter("@id", id)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public AppVersionQuery GetDbInfo()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "SELECT SUBSTRING(MigrationId, 30, 10) AS Version from __EFMigrationsHistory";
                var rs = ctx.FromSqlRaw<AppVersionQuery>(sql).LastOrDefault();
                return rs == null ? new AppVersionQuery() : rs;
            }
        }
    }
}
