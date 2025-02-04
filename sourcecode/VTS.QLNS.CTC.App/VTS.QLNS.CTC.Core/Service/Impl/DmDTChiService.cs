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
    public class DmDTChiService : IService<VdtDmDuToanChi>, IDmDTChiService
    {
        private IDmDTChiRepository _repository;

        public DmDTChiService(IDmDTChiRepository tlDmDonViRepository)
        {
            _repository = tlDmDonViRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<VdtDmDuToanChi> listEntities, AuthenticationInfo authenticationInfo)
        {
            _repository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<VdtDmDuToanChi> FindAll(AuthenticationInfo authenticationInfo)
        {
            IEnumerable<VdtDmDuToanChi> results = _repository.FindAllDTChi().ToList();
            return results;
        }

        public IEnumerable<VdtDmDuToanChi> GetAllDuToanChi()
        {
            return _repository.FindAll();
        }
    }
}
