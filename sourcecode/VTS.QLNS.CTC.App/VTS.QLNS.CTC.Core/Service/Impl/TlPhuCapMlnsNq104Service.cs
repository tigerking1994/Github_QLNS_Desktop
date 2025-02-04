using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlPhuCapMlnsNq104Service : ITlPhuCapMlnsNq104Service
    {
        private readonly ITlPhuCapMlnsNq104Repository _tlPhuCapMlnsRepository;

        public TlPhuCapMlnsNq104Service(ITlPhuCapMlnsNq104Repository tlPhuCapMlnsRepository)
        {
            _tlPhuCapMlnsRepository = tlPhuCapMlnsRepository;
        }

        public int Add(TlPhuCapMlnNq104 tlPhuCapMln)
        {
            return _tlPhuCapMlnsRepository.Add(tlPhuCapMln);
        }

        public int AddRange(IEnumerable<TlPhuCapMlnNq104> tlPhuCapMlns)
        {
            return _tlPhuCapMlnsRepository.AddRange(tlPhuCapMlns);
        }

        public int Delete(Guid id)
        {
            return _tlPhuCapMlnsRepository.Delete(id);
        }

        public IEnumerable<TlPhuCapMlnNq104> FindAll()
        {
            return _tlPhuCapMlnsRepository.FindAll();
        }

        public IEnumerable<TlPhuCapMlnNq104> FindAll(AuthenticationInfo authenticationInfo)
        {
            throw new NotImplementedException();
        }

        public int Update(TlPhuCapMlnNq104 tlPhuCapMln)
        {
            return _tlPhuCapMlnsRepository.Update(tlPhuCapMln);
        }

        public int CountByYear(int year)
        {
            return _tlPhuCapMlnsRepository.CountByYear(year);
        }

        public IEnumerable<TlPhuCapMlnNq104> FindByCondition(Expression<Func<TlPhuCapMlnNq104, bool>> predicate)
        {
            return _tlPhuCapMlnsRepository.FindAll(predicate);
        }
    }
}
