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
    public class DmDeTaiService : IService<DmDeTai>, IDmDeTaiService
    {
        private readonly IDmDeTaiRepository _dmDeTaiRepository;

        public DmDeTaiService(IDmDeTaiRepository dmDeTaiRepository)
        {
            _dmDeTaiRepository = dmDeTaiRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<DmDeTai> listEntities, AuthenticationInfo authenticationInfo)
        {
            _dmDeTaiRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<DmDeTai> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _dmDeTaiRepository.FindAll();
        }
    }
}
