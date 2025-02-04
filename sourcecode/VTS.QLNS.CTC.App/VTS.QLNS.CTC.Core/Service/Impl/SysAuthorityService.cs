using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SysAuthorityService : IService<HtQuyen>, ISysAuthorityService
    {
        private ISysAuthorityRepository _sysAuthorityRepository;

        public SysAuthorityService(ISysAuthorityRepository sysAuthorityRepository)
        {
            _sysAuthorityRepository = sysAuthorityRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<HtQuyen> listEntities, AuthenticationInfo authenticationInfo)
        {
            _sysAuthorityRepository.UpdateAuthorities(listEntities);
        }

        public override IEnumerable<HtQuyen> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _sysAuthorityRepository.FindAllWithFunction();
        }

        public IEnumerable<HtLoaiQuyen> FindAllAuthorTypes()
        {
            return _sysAuthorityRepository.FindAllAuthorTypes();
        }

    }
}
