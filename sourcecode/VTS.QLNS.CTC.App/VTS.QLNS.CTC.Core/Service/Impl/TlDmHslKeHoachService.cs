using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmHslKeHoachService : ITlDmHslKeHoachService
    {
        private readonly ITlDmHslKeHoachRepository _tlDmHslKeHoachRepository;

        public TlDmHslKeHoachService(ITlDmHslKeHoachRepository tlDmHslKeHoachRepository)
        {
            _tlDmHslKeHoachRepository = tlDmHslKeHoachRepository;
        }

        public IEnumerable<TlDmHslKeHoach> FindAll(Expression<Func<TlDmHslKeHoach, bool>> predicate)
        {
            return _tlDmHslKeHoachRepository.FindAll(predicate);
        }

        public IEnumerable<TlDmHslKeHoach> FindAll()
        {
            return _tlDmHslKeHoachRepository.FindAll();
        }

        public TlDmHslKeHoach FindById(Guid id)
        {
            return _tlDmHslKeHoachRepository.Find(id);
        }
    }
}
