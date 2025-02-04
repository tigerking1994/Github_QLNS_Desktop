using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class ImpQuyetToanService : IImpQuyetToanService
    {
        private readonly IImpQuyetToanRepository _impQuyetToanRepository;

        public ImpQuyetToanService(IImpQuyetToanRepository impQuyetToanRepository)
        {
            _impQuyetToanRepository = impQuyetToanRepository;
        }

        public void AddRange(List<ImpQuyetToan> quyetToans)
        {
            _impQuyetToanRepository.AddRange(quyetToans);
        }
    }
}
