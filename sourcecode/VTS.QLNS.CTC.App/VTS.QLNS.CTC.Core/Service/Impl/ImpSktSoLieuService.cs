using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class ImpSktSoLieuService : IImpSktSoLieuService
    {
        private readonly IImpSktSoLieuRepository _sktSoLieuRepository;

        public ImpSktSoLieuService(IImpSktSoLieuRepository sktSoLieuRepository)
        {
            _sktSoLieuRepository = sktSoLieuRepository;
        }
        public int AddRange(IEnumerable<ImpSktSoLieuChiTiet> entities)
        {
            return _sktSoLieuRepository.AddRange(entities);
        }
    }
}
