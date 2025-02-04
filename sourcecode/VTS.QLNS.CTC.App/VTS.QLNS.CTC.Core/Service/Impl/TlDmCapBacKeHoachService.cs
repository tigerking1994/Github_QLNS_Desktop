using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCapBacKeHoachService : ITlDmCapBacKeHoachService
    {
        private ITlDmCapBacKeHoachRepository _tlDmCapBacKeHoachRepository;

        public TlDmCapBacKeHoachService(ITlDmCapBacKeHoachRepository tlDmCapBacKeHoachRepository)
        {
            _tlDmCapBacKeHoachRepository = tlDmCapBacKeHoachRepository;
        }

        public int Add(TlDmCapBacKeHoach entity)
        {
            return _tlDmCapBacKeHoachRepository.Add(entity);
        }

        public int AddRang(List<TlDmCapBacKeHoach> entities)
        {
            return _tlDmCapBacKeHoachRepository.AddRange(entities);
        }

        public int Delete(Guid id)
        {
            return _tlDmCapBacKeHoachRepository.Delete(id);
        }

        public IEnumerable<TlDmCapBacKeHoach> FindAll()
        {
            return _tlDmCapBacKeHoachRepository.FindAll();
        }

        public TlDmCapBacKeHoach FindByMaCb(string maCb)
        {
            return _tlDmCapBacKeHoachRepository.FindByMaCb(maCb);
        }

        public TlDmCapBacKeHoach FindByMaCbAndHsl(string maCb, decimal? hsl)
        {
            return _tlDmCapBacKeHoachRepository.FindByMaCbAndHsl(maCb, hsl);
        }

        public int UpDate(TlDmCapBacKeHoach entity)
        {
            return _tlDmCapBacKeHoachRepository.Update(entity);
        }

        public int CountByYear(int year)
        {
            return _tlDmCapBacKeHoachRepository.CountByYear(year);
        }

        public TlDmCapBacKeHoach FindByCondition(Expression<Func<TlDmCapBacKeHoach, bool>> predicate)
        {
            return _tlDmCapBacKeHoachRepository.Find(predicate);
        }

        public TlDmCapBacKeHoach FindByMaCbAndHslAndNhom(string maCb, decimal? hsl, string nhom)
        {
            return _tlDmCapBacKeHoachRepository.FindByMaCbAndHslAndNhom(maCb, hsl, nhom);
        }
    }
}
