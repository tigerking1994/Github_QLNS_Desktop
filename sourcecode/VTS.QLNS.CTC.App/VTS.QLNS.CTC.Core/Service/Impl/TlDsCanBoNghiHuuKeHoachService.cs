using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDsCanBoNghiHuuKeHoachService : ITlDsCanBoNghiHuuKeHoachService
    {
        private readonly ITlDsCanBoNghiHuuKeHoachRepository _tlDsCanBoNghiHuuKeHoachRepository;

        public TlDsCanBoNghiHuuKeHoachService(ITlDsCanBoNghiHuuKeHoachRepository tlDsCanBoNghiHuuKeHoachRepository)
        {
            _tlDsCanBoNghiHuuKeHoachRepository = tlDsCanBoNghiHuuKeHoachRepository;
        }

        public int Delete(Guid id)
        {
            return _tlDsCanBoNghiHuuKeHoachRepository.Delete(id);
        }

        public IEnumerable<TlDsCanBoNghiHuuKeHoachQuery> FinAllCanBoNghiHuuKeHoach()
        {
            return _tlDsCanBoNghiHuuKeHoachRepository.FinAllCanBoNghiHuuKeHoach();
        }
    }
}
