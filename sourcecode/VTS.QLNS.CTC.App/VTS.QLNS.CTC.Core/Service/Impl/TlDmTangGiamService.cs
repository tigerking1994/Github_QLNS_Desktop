using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmTangGiamService : IService<TlDmTangGiam>, ITlDmTangGiamService
    {
        private ITlDmTangGiamRepository _tlDmTangGiamRepository;

        public override void AddOrUpdateRange(IEnumerable<TlDmTangGiam> listEntities, AuthenticationInfo authenticationInfo)
        {
            _tlDmTangGiamRepository.AddOrUpdateRange(listEntities);
        }
        public TlDmTangGiamService(ITlDmTangGiamRepository tlDmTangGiamRepository)
        {
            _tlDmTangGiamRepository = tlDmTangGiamRepository;
        }
        public IEnumerable<TlDmTangGiam> FindAll()
        {
            return _tlDmTangGiamRepository.FindAll();
        }

        public TlDmTangGiam FindByMaTangGiam(string maTangGiam)
        {
            return _tlDmTangGiamRepository.FindByMaTangGiam(maTangGiam);
        }

        public override IEnumerable<TlDmTangGiam> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tlDmTangGiamRepository.FindAll().ToList();
        }
    }
}
