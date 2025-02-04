using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class ImpDuToanService : IImpDuToanService
    {
        private readonly IImpDuToanRepository _impDuToanRepository;

        public ImpDuToanService(IImpDuToanRepository impDuToanRepository)
        {
            _impDuToanRepository = impDuToanRepository;
        }

        public void AddRange(List<ImpDuToan> duToans)
        {
            _impDuToanRepository.AddRange(duToans);
        }
    }
}
