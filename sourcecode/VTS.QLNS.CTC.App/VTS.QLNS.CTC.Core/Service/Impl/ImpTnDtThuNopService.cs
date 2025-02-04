using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class ImpTnDtThuNopService : IImpTnDtThuNopService
    {
        private readonly IImpTnDtThuNopRepository _impTnDtThuNopRepository;

        public ImpTnDtThuNopService(IImpTnDtThuNopRepository impTnDtThuNopRepository)
        {
            _impTnDtThuNopRepository = impTnDtThuNopRepository;
        }

        public void AddRange(List<ImpTnDtThuNop> thuNops)
        {
            _impTnDtThuNopRepository.AddRange(thuNops);
        }
    }
}
