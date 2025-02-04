using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmMapPcDetailService : ITlDmMapPcDetailService
    {
        private readonly ITlDmMapPcDetailRepository _tlDmMapPcDetailRepository;

        public TlDmMapPcDetailService(ITlDmMapPcDetailRepository tlDmMapPcDetailRepository)
        {
            _tlDmMapPcDetailRepository = tlDmMapPcDetailRepository;
        }

        public int AddRange(List<TlDmMapPcDetail> lstPcMapAdd)
        {
            return _tlDmMapPcDetailRepository.AddRange(lstPcMapAdd);
        }

        public int Delete(Guid id)
        {
            return _tlDmMapPcDetailRepository.Delete(id);
        }

        public IEnumerable<TlDmMapPcDetail> FindAll()
        {
            return _tlDmMapPcDetailRepository.FindAll();
        }

        public int Update(TlDmMapPcDetail tlDmMapPcDetail)
        {
            return _tlDmMapPcDetailRepository.Update(tlDmMapPcDetail);
        }
    }
}
