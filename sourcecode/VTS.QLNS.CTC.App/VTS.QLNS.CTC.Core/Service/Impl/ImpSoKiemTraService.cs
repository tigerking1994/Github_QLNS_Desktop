using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class ImpSoKiemTraService : IImpSoKiemTraService
    {
        private readonly IImpSoKiemTraRepository _impSoKiemTraRepository;

        public ImpSoKiemTraService(IImpSoKiemTraRepository impSoKiemTraRepository)
        {
            _impSoKiemTraRepository = impSoKiemTraRepository;
        }

        public void AddRange(List<ImpSoKiemTra> soKiemTrass)
        {
            _impSoKiemTraRepository.AddRange(soKiemTrass);
        }
    }
}
