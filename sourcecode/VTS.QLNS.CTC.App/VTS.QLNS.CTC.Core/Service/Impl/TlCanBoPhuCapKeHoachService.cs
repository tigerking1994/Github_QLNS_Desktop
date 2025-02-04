using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlCanBoPhuCapKeHoachService : ITlCanBoPhuCapKeHoachService
    {
        private readonly ITlCanBoPhuCapKeHoachRepository _tlCanBoPhuCapKeHoachRepository;

        public TlCanBoPhuCapKeHoachService(ITlCanBoPhuCapKeHoachRepository tlCanBoPhuCapKeHoachRepository)
        {
            _tlCanBoPhuCapKeHoachRepository = tlCanBoPhuCapKeHoachRepository;
        }

        public int AddRange(IEnumerable<TlCanBoPhuCapKeHoach> tlCanBoPhuCapKeHoaches)
        {
            return _tlCanBoPhuCapKeHoachRepository.AddRange(tlCanBoPhuCapKeHoaches);
        }

        public void BulkInsert(IEnumerable<TlCanBoPhuCapKeHoach> tlCanBoPhuCapKeHoaches)
        {
            _tlCanBoPhuCapKeHoachRepository.BulkInsert(tlCanBoPhuCapKeHoaches);
        }

        public void BulkUpdate(IEnumerable<TlCanBoPhuCapKeHoach> tlCanBoPhuCapKeHoaches)
        {
            _tlCanBoPhuCapKeHoachRepository.BulkUpdate(tlCanBoPhuCapKeHoaches);
        }

        public int DeleteByMaCanBo(string maCanBo)
        {
            return _tlCanBoPhuCapKeHoachRepository.DeleteByMaCanBo(maCanBo);
        }

        public int DeleteManyMaCanBo(string lstMaCanBo)
        {
            return _tlCanBoPhuCapKeHoachRepository.DeleteManyMaCanBo(lstMaCanBo);
        }

        public int DeleteByYear(int year)
        {
            return _tlCanBoPhuCapKeHoachRepository.DeleteByYear(year);
        }

        public IEnumerable<TlCanBoPhuCapKeHoach> FindByCondition(Expression<Func<TlCanBoPhuCapKeHoach, bool>> predicate)
        {
            return _tlCanBoPhuCapKeHoachRepository.FindByCondition(predicate);
        }
        public IEnumerable<TlCanBoPhuCapKeHoach> FindByMaCanBo(string maCanBo)
        {
            return _tlCanBoPhuCapKeHoachRepository.FindByMaCanBo(maCanBo);
        }

        public IEnumerable<TlPhuCapQuery> FindCanBoPhuCap(string maCanBo)
        {
            return _tlCanBoPhuCapKeHoachRepository.FindCanBoPhuCap(maCanBo);
        }

        public IEnumerable<TlPhuCapNq104Query> FindCanBoPhuCapNq104(string maCanBo)
        {
            return _tlCanBoPhuCapKeHoachRepository.FindCanBoPhuCapNq104(maCanBo);
        }

        public int DeleteByUnitYear(int year, string unit)
        {
            return _tlCanBoPhuCapKeHoachRepository.DeleteByUnitYear(year, unit);
        }

        public IEnumerable<TlCanBoPhuCapKeHoachQuery> FindCanBoPhuCapKeHoachByMaCanBo(string maCanBo)
        {
            return _tlCanBoPhuCapKeHoachRepository.FindCanBoPhuCapKeHoachByMaCanBo(maCanBo);
        }
    }
}
