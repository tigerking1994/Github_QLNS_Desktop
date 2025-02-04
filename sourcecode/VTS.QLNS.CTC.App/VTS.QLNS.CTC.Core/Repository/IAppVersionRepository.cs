using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IAppVersionRepository : IRepository<HtAppVersion>
    {
        void UpdateVersion(HtAppVersion appVersion);
        IEnumerable<AppVersionQuery> FindAllVersion();
        void UpdateVersion(AppVersionQuery selectedAppVersion, IEnumerable<AppVersionQuery> appVersions);
        AppVersionQuery FindCurrentVersion();
        void DeleteVersion(Guid id);
        AppVersionQuery GetDbInfo();
    }
}
