using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class ImpCpChungTuChiTietService : IImpCpChungTuChiTietService
    {
        private readonly IImpCpChungTuChiTietRepository _cpChungTuChiTietRepository;

        public ImpCpChungTuChiTietService(IImpCpChungTuChiTietRepository chungTuChiTietRepository)
        {
            _cpChungTuChiTietRepository = chungTuChiTietRepository;
        }

        public int AddRange(IEnumerable<ImpCpChungTuChiTiet> entities)
        {
            return _cpChungTuChiTietRepository.AddRange(entities);
        }
    }
}
