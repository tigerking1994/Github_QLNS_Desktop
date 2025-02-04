using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class ImpTnQtThuNopService : IImpTnQtThuNopService
    {
        private readonly IImpTnQtThuNopRepository _impTnQtThuNopRepository;

        public ImpTnQtThuNopService(IImpTnQtThuNopRepository impTnQtThuNopRepository)
        {
            _impTnQtThuNopRepository = impTnQtThuNopRepository;
        }

        public void AddRange(List<ImpTnQtThuNop> thuNops)
        {
            _impTnQtThuNopRepository.AddRange(thuNops);
        }
    }
}
