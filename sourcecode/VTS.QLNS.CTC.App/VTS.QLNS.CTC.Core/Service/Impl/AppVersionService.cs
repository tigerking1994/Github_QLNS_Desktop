using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class AppVersionService : IAppVersionService
    {
        private IAppVersionRepository _appVersionRepository;

        public AppVersionService(IAppVersionRepository appVersionRepository)
        {
            _appVersionRepository = appVersionRepository;
        }

        public void Add(HtAppVersion appVersion)
        {
            _appVersionRepository.Add(appVersion);
        }

        public void ApplyVersion(AppVersionQuery selectedAppVersion, IEnumerable<AppVersionQuery> appVersions)
        {
            _appVersionRepository.UpdateVersion(selectedAppVersion, appVersions);
        }

        public IEnumerable<AppVersionQuery> FindAll()
        {
            return _appVersionRepository.FindAllVersion();
        }

        public HtAppVersion FindById(Guid id)
        {
            return _appVersionRepository.FirstOrDefault(t => t.Id.Equals(id));
        }

        public void Update(HtAppVersion appVersion)
        {
            _appVersionRepository.UpdateVersion(appVersion);
        }

        public HtAppVersion FindCurrentVersion()
        {
            return _appVersionRepository.FirstOrDefault(t => t.Status == 1);
        }

        public AppVersionQuery FindCurrentAppVersion()
        {
            return _appVersionRepository.FindCurrentVersion();
        }

        public void DeleteVersion(Guid id)
        {
            _appVersionRepository.DeleteVersion(id);
        }

        public AppVersionQuery GetDbInfo()
        {
            return _appVersionRepository.GetDbInfo();
        }
    }
}
