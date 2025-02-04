using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaChenhLechTiGiaService : INhDaChenhLechTiGiaService
    {
        private readonly INhDaChenhLechTiGiaRepository _chenhLechTiGiaRepository;

        public NhDaChenhLechTiGiaService(INhDaChenhLechTiGiaRepository chenhLechTiGiaRepository)
        {
            _chenhLechTiGiaRepository = chenhLechTiGiaRepository;
        }

        public IEnumerable<NhDaChenhLechTiGiaQuery> FindAllExchangeRateDifference(ExchangeRateDifferenceCriteria condition)
        {
            return _chenhLechTiGiaRepository.FindAllExchangeRateDifference(condition);
        }
    }
}
