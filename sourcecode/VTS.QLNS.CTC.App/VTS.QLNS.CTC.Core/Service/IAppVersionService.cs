using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IAppVersionService
    {
        IEnumerable<AppVersionQuery> FindAll();
        void ApplyVersion(AppVersionQuery selectedAppVersion, IEnumerable<AppVersionQuery> appVersions);
        void Add(HtAppVersion appVersion);
        void Update(HtAppVersion appVersion);
        HtAppVersion FindById(Guid id);
        HtAppVersion FindCurrentVersion();
        AppVersionQuery FindCurrentAppVersion();
        void DeleteVersion(Guid id);
        AppVersionQuery GetDbInfo();
    }
}
