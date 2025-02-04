using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlPhuCapMlnsService : ITlPhuCapMlnsService
    {
        private readonly ITlPhuCapMlnsRepository _tlPhuCapMlnsRepository;

        public TlPhuCapMlnsService(ITlPhuCapMlnsRepository tlPhuCapMlnsRepository)
        {
            _tlPhuCapMlnsRepository = tlPhuCapMlnsRepository;
        }

        public int Add(TlPhuCapMln tlPhuCapMln)
        {
            return _tlPhuCapMlnsRepository.Add(tlPhuCapMln);
        }

        public int AddRange(IEnumerable<TlPhuCapMln> tlPhuCapMlns)
        {
            return _tlPhuCapMlnsRepository.AddRange(tlPhuCapMlns);
        }

        public int Delete(Guid id)
        {
            return _tlPhuCapMlnsRepository.Delete(id);
        }

        public IEnumerable<TlPhuCapMln> FindAll()
        {
            return _tlPhuCapMlnsRepository.FindAll();
        }

        public IEnumerable<TlPhuCapMln> FindAll(AuthenticationInfo authenticationInfo)
        {
            throw new NotImplementedException();
        }

        public int Update(TlPhuCapMln tlPhuCapMln)
        {
            return _tlPhuCapMlnsRepository.Update(tlPhuCapMln);
        }

        public int CountByYear(int year)
        {
            return _tlPhuCapMlnsRepository.CountByYear(year);
        }

        public IEnumerable<TlPhuCapMln> FindByCondition(Expression<Func<TlPhuCapMln, bool>> predicate)
        {
            return _tlPhuCapMlnsRepository.FindAll(predicate);
        }
    }
}
