using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCanBoKeHoachService : ITlDmCanBoKeHoachService
    {
        private readonly ITlDmCanBoKeHoachRepository _tlDmCanBoKeHoachRepository;

        public TlDmCanBoKeHoachService(ITlDmCanBoKeHoachRepository tlDmCanBoKeHoachRepository)
        {
            _tlDmCanBoKeHoachRepository = tlDmCanBoKeHoachRepository;
        }

        public int Add(TlDmCanBoKeHoach tlDmCanBoKeHoach)
        {
            return _tlDmCanBoKeHoachRepository.Add(tlDmCanBoKeHoach);
        }

        public int AddRange(IEnumerable<TlDmCanBoKeHoach> tlDmCanBoKeHoach)
        {
            return _tlDmCanBoKeHoachRepository.AddRange(tlDmCanBoKeHoach);
        }

        public int Delete(Guid id)
        {
            return _tlDmCanBoKeHoachRepository.Delete(id);
        }

        public int DeleteByYear(int year)
        {
            return _tlDmCanBoKeHoachRepository.DeleteByYear(year);
        }

        public TlDmCanBoKeHoach Find(Guid id)
        {
            return _tlDmCanBoKeHoachRepository.Find(id);
        }

        public IEnumerable<TlDmCanBoKeHoach> FindAll()
        {
            return _tlDmCanBoKeHoachRepository.FindAll();
        }

        public IEnumerable<TlDmCanBoKeHoach> FindAllCanBo()
        {
            return _tlDmCanBoKeHoachRepository.FindAllCanBo();
        }

        public IEnumerable<TlDmCanBoKeHoach> FindByCondition(Expression<Func<TlDmCanBoKeHoach, bool>> predicate)
        {
            return _tlDmCanBoKeHoachRepository.FindAll(predicate);
        }

        public TlDmCanBoKeHoach FindByMaCanBo(string maCanbo)
        {
            return _tlDmCanBoKeHoachRepository.FindByMaCanBo(maCanbo);
        }

        public IEnumerable<TlDmCanBoKeHoach> FindByYear(int year)
        {
            return _tlDmCanBoKeHoachRepository.FindByYear(year);
        }

        public IEnumerable<TlDmCanBoKeHoach> FindLoadIndex()
        {
            return _tlDmCanBoKeHoachRepository.FindLoadIndex();
        }

        public int Update(TlDmCanBoKeHoach tlDmCanBo)
        {
            return _tlDmCanBoKeHoachRepository.Update(tlDmCanBo);
        }
    }
}
