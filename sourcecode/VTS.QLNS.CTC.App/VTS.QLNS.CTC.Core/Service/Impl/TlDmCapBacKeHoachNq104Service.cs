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
    public class TlDmCapBacKeHoachNq104Service : ITlDmCapBacKeHoachNq104Service
    {
        private ITlDmCapBacKeHoachNq104Repository _tlDmCapBacKeHoachRepository;

        public TlDmCapBacKeHoachNq104Service(ITlDmCapBacKeHoachNq104Repository tlDmCapBacKeHoachRepository)
        {
            _tlDmCapBacKeHoachRepository = tlDmCapBacKeHoachRepository;
        }

        public int Add(TlDmCapBacKeHoachNq104 entity)
        {
            return _tlDmCapBacKeHoachRepository.Add(entity);
        }

        public int AddRang(List<TlDmCapBacKeHoachNq104> entities)
        {
            return _tlDmCapBacKeHoachRepository.AddRange(entities);
        }

        public int Delete(Guid id)
        {
            return _tlDmCapBacKeHoachRepository.Delete(id);
        }

        public IEnumerable<TlDmCapBacKeHoachNq104> FindAll()
        {
            return _tlDmCapBacKeHoachRepository.FindAll();
        }

        public TlDmCapBacKeHoachNq104 FindByMaCb(string maCb)
        {
            return _tlDmCapBacKeHoachRepository.FindByMaCb(maCb);
        }

        public TlDmCapBacKeHoachNq104 FindByMaCbAndHsl(string maCb, decimal? hsl)
        {
            return _tlDmCapBacKeHoachRepository.FindByMaCbAndHsl(maCb, hsl);
        }

        public int UpDate(TlDmCapBacKeHoachNq104 entity)
        {
            return _tlDmCapBacKeHoachRepository.Update(entity);
        }

        public int CountByYear(int year)
        {
            return _tlDmCapBacKeHoachRepository.CountByYear(year);
        }

        public TlDmCapBacKeHoachNq104 FindByCondition(Expression<Func<TlDmCapBacKeHoachNq104, bool>> predicate)
        {
            return _tlDmCapBacKeHoachRepository.Find(predicate);
        }

        public TlDmCapBacKeHoachNq104 FindByMaCbAndHslAndNhom(string maCb, decimal? hsl, string nhom)
        {
            return _tlDmCapBacKeHoachRepository.FindByMaCbAndHslAndNhom(maCb, hsl, nhom);
        }
    }
}
