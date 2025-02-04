using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmTangGiamNq104Service : IService<TlDmTangGiamNq104>, ITlDmTangGiamNq104Service
    {
        private ITlDmTangGiamNq104Repository _tlDmTangGiamRepository;

        public override void AddOrUpdateRange(IEnumerable<TlDmTangGiamNq104> listEntities, AuthenticationInfo authenticationInfo)
        {
            _tlDmTangGiamRepository.AddOrUpdateRange(listEntities);
        }
        public TlDmTangGiamNq104Service(ITlDmTangGiamNq104Repository tlDmTangGiamRepository)
        {
            _tlDmTangGiamRepository = tlDmTangGiamRepository;
        }
        public IEnumerable<TlDmTangGiamNq104> FindAll()
        {
            return _tlDmTangGiamRepository.FindAll();
        }

        public TlDmTangGiamNq104 FindByMaTangGiam(string maTangGiam)
        {
            return _tlDmTangGiamRepository.FindByMaTangGiam(maTangGiam);
        }

        public override IEnumerable<TlDmTangGiamNq104> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tlDmTangGiamRepository.FindAll().ToList();
        }
    }
}
