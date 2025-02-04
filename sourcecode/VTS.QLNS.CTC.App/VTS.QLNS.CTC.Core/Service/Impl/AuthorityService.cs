using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class AuthorityService : IService<HtQuyen>, IAuthorityService
    {
        private readonly IAuthorityRepository _authorityRepository;

        public AuthorityService(IAuthorityRepository authorityRepository)
        {
            _authorityRepository = authorityRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<HtQuyen> listEntities, AuthenticationInfo authenticationInfo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HtQuyen> FindAll()
        {
            return _authorityRepository.FindAll();
        }

        public override IEnumerable<HtQuyen> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _authorityRepository.FindAll();
        }
    }
}
