using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDsCBNHKeHoachService : ITlDsCBNHKeHoachService
    {
        private readonly ITlDsCBNHKeHoachRepository _tlDsCBNHKeHoachRepository;

        public TlDsCBNHKeHoachService(ITlDsCBNHKeHoachRepository tlDsCBNHKeHoachRepository)
        {
            _tlDsCBNHKeHoachRepository = tlDsCBNHKeHoachRepository;
        }

        public int Add(TlDsCBNHKeHoach tlDsCBNHKeHoach)
        {
            return _tlDsCBNHKeHoachRepository.Add(tlDsCBNHKeHoach);
        }

        public int AddRange(IEnumerable<TlDsCBNHKeHoach> tlDsCBNHKeHoach)
        {
            return _tlDsCBNHKeHoachRepository.AddRange(tlDsCBNHKeHoach);
        }

        public int Delete(Guid id)
        {
            return _tlDsCBNHKeHoachRepository.Delete(id);
        }

        public int DeleteByYear(int year)
        {
            return _tlDsCBNHKeHoachRepository.DeleteByYear(year);
        }

        public IEnumerable<TlDsCBNHKeHoach> FinAllCanBoNghiHuu()
        {
            return _tlDsCBNHKeHoachRepository.FindAllCanBoNghiHuu();
        }

        public TlDsCBNHKeHoach Find(Guid id)
        {
            return _tlDsCBNHKeHoachRepository.Find(id);
        }

        public IEnumerable<TlDsCBNHKeHoach> FindAll()
        {
            return _tlDsCBNHKeHoachRepository.FindAll();
        }

        public IEnumerable<TlDsCBNHKeHoach> FindAllCanBoNghiHuu()
        {
            return _tlDsCBNHKeHoachRepository.FindAllCanBoNghiHuu();
        }

        public IEnumerable<TlDsCBNHKeHoach> FindByCondition(Expression<Func<TlDsCBNHKeHoach, bool>> predicate)
        {
            return _tlDsCBNHKeHoachRepository.FindAll(predicate);
        }

        public TlDsCBNHKeHoach FindByMaCanBo(string maCanbo)
        {
            return _tlDsCBNHKeHoachRepository.FindByMaCanBo(maCanbo);
        }

        public IEnumerable<TlDsCBNHKeHoach> FindByYear(int year)
        {
            return _tlDsCBNHKeHoachRepository.FindByYear(year);
        }

        public IEnumerable<TlDsCBNHKeHoach> FindLoadIndex()
        {
            return _tlDsCBNHKeHoachRepository.FindLoadIndex();
        }

        public int Update(TlDsCBNHKeHoach tlDsCBNHKeHoach)
        {
            return _tlDsCBNHKeHoachRepository.Update(tlDsCBNHKeHoach);
        }
    }
}
