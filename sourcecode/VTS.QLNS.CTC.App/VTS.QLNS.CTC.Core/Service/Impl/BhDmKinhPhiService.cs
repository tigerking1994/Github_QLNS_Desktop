using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDmKinhPhiService : IService<BhDmKinhPhi>, IBhDmKinhPhiService
    {
        private readonly IBhDmKinhPhiRepository _repository;

        public BhDmKinhPhiService(IBhDmKinhPhiRepository repository)
        {
            _repository = repository;
        }

        public override IEnumerable<BhDmKinhPhi> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll(authenticationInfo);
        }

        public IEnumerable<BhDmKinhPhi> FindAll()
        {
            return _repository.FindAll();
        }

        public override void AddOrUpdateRange(IEnumerable<BhDmKinhPhi> listEntities, AuthenticationInfo authenticationInfo)
        {
            _repository.AddOrUpdateRange(listEntities, authenticationInfo);
        }
    }
}
