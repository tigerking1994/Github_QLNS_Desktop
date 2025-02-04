using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDmCoSoYTeService : IService<BhDmCoSoYTe>,IBhDmCoSoYTeService
    {
        private readonly IBhDmCoSoYTeRepository _repository;

        public BhDmCoSoYTeService(IBhDmCoSoYTeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BhDmCoSoYTe> FindByCondition(Expression<Func<BhDmCoSoYTe, bool>> predicate)
        {
            return _repository.FindByCondition(predicate);
        }

        public List<BhDmCoSoYTe> GetListCoSoYTe(int namLamViec)
        {
            return _repository.GetListCoSoYTe(namLamViec);
        }

        public BhDmCoSoYTe GetCSYTByMa(string maCSYT, int namLamViec)
        {
            return _repository.GetCSYTByMa(maCSYT, namLamViec);
        }

        public bool ExistCSYT(string maCSYT, int namLamViec)
        {
            return _repository.ExistCSYT(maCSYT, namLamViec);
        }


        public override IEnumerable<BhDmCoSoYTe> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll(authenticationInfo);
        }

        public override void AddOrUpdateRange(IEnumerable<BhDmCoSoYTe> listEntities, AuthenticationInfo authenticationInfo)
        {
            _repository.AddOrUpdateRange(listEntities, authenticationInfo);
        }

        public IEnumerable<BhDmCoSoYTe> FindAll()
        {
            return _repository.FindAll();
        }
    }
}
